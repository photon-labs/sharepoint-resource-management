/**
 * PHR_SharePointResourceManagement
 *
 * Copyright (C) 1999-2013 Photon Infotech Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *         http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using System.Collections;

namespace Phresco.FusionCharts
{
    /// <summary>
    /// Converts a SharePoint List (SPList) into a nice XML string
    /// that can be directly used by fusion charts
    /// </summary>
    public class ListToXmlConverter
    {
        #region PrivateMembers
        private string _listName; // The name of the SPList to transform
        private string _viewName; // The name of the SPView to use
        private SPList _list; //The SPList to transform
        private SPView _view; // The SPView to use
        private SPListItemCollection _items; // Items in the SPList
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="list">The SharePoint list to tranform into XML for Fusion Charts</param>
        /// <param name="view">The SharePoint view to use with the list</param>
        public ListToXmlConverter(string listName, string viewName)
        {
            // We initialize the private members
            _listName = listName;
            _viewName = viewName;

            // We perform startup tasks
            GetListAndView();
            QueryList();
        }
        #endregion

        #region PrivateMethods

        /// <summary>
        /// Retrieves a SPList from its name
        /// </summary>
        private void GetListAndView()
        {
            // We open the SPList
            Guid siteID = SPContext.Current.Site.ID;
            Guid webID = SPContext.Current.Web.ID;
            using (SPSite site = new SPSite(siteID))
            {
                using (SPWeb web = site.OpenWeb(webID))
                {
                    // We get the list from its name
                    try
                    {
                        _list = web.Lists[_listName];
                    }
                    catch
                    {
                        throw (new Exception("The list name your provided does not exist or cannot be found"));
                    }

                    // We get the view from its name or get the default one
                    if (_viewName != String.Empty)
                    {
                        try
                        {
                            _view = _list.Views[_viewName];
                        }
                        catch
                        {
                            throw (new Exception("The view name your provided does not exist or cannot be found"));
                        }
                    }
                    else
                    {
                        _view = _list.DefaultView;
                    }
                }
            }
        }

        /// <summary>
        /// Get the list of items in the list based on the view
        /// </summary>
        private void QueryList()
        {
            _items = _list.GetItems(_view);
        }

        /// <summary>
        /// Transforms the list of items into something easily usable (a Plots object).
        /// Performs also operations such as SUM, COUNT over GROUPBY
        /// </summary>
        /// <param name="xValue">The name of the column to use as xValue</param>
        /// <param name="yValue">The nam of the column to use as yValue</param>
        /// <param name="action">The action to perform, if any...</param>
        /// <returns></returns>
        public Plots PrepareData(string xValue, string yValue, GroupAction action)
        {
            // We first check that xValue and yValue are correct and throw an exception if needed
            if (!_items.List.Fields.ContainsField(xValue))
            {
                string message = "The column \"" + xValue + "\" does not exist in the list \"" + _items.List.Title + "\" or in the specified view \"" + _view.Title + "\".<br /><br />Check the view you specified and make sure that the column exists.";
                message += ListValidColumns();
                throw (new Exception(message));
            }
            if (yValue != String.Empty && !_items.List.Fields.ContainsField(yValue))
            {
                string message = "The column \"" + yValue + "\" does not exist in the list \"" + _items.List.Title + "\" or in the specified view \"" + _view.Title + "\".<br /><br />Check the view you specified and make sure that the column exists.";
                message += ListValidColumns();
                throw (new Exception(message));
            }

            // We prepare our output
            Plots plots = new Plots();

            // We loop through our SPList
            foreach (SPListItem item in _items)
            {
                // We get the X and Y from
                string x = "";
                try
                {
                    if (item[xValue] == null)
                        continue;

                    x = item[xValue].ToString();
                }
                catch
                {
                    throw (new Exception("The column \"" + xValue + "\" does not exist in the view"));
                }

                double y = 0;
                if (yValue != String.Empty) Double.TryParse(item[yValue].ToString(), out y);

                // The value is already in our array
                if (plots.Contains(x))
                {
                    int pos = plots.XIndexOf(x);

                    if (action == GroupAction.COUNT)
                    {
                        // Count, we add +1 to the existing value
                        plots.SetY(pos, Double.Parse(plots.GetY(pos).ToString()) + 1);
                    }
                    if (action == GroupAction.SUM)
                    {
                        // Sum, we add the value to the existing value
                        plots.SetY(pos, Double.Parse(plots.GetY(pos).ToString()) + y);
                    }
                    if (action == GroupAction.NOTHING)
                    {
                        // No action, we simply add the value
                        plots.Add(x, y);
                    }
                }
                else // The value is a new one in our array
                {
                    if (action == GroupAction.COUNT)
                    {
                        // Count, the first yValue is 1.
                        plots.Add(x, 1);
                    }
                    else
                    {
                        // anything else than Count, we just add the plot
                        plots.Add(x, y);
                    }
                }
            }
            return plots;
        }

        /// <summary>
        /// Returns a list of valids column name that can be used.
        /// </summary>
        /// <returns></returns>
        private string ListValidColumns()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<br /><br />Valid columns are:<br />"); 
            foreach (string field in _view.ViewFields)
            {
                sb.Append(" - " + field + "<br />");
            }
            return sb.ToString();
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Generate the nice XML output for fusion charts
        /// </summary>
        /// <param name="chartTitle">Title of the chart</param>
        /// <param name="xValue">Column name to use as x value</param>
        /// <param name="xTitle">Title to give to x</param>
        /// <param name="yValue">Column name to use as y value</param>
        /// <param name="yTitle">Title to give to y</param>
        /// <param name="groupAction">Action to perform (sum, count, ...)</param>
        /// <param name="colors">List of colors to use (separated by ;)</param>
        /// <returns></returns>
        public string GenerateXml(string chartTitle, string xValue, string xTitle, string yValue, string yTitle, GroupAction groupAction, string colors)
        {
            // We first convert color (string) into a nice array
            string[] separator = {";"};
            string[] colorsArray = colors.Split(separator, StringSplitOptions.RemoveEmptyEntries);
           
            StringBuilder sb = new StringBuilder();
            // We open the graph tag with a few parameters
            sb.AppendLine("<graph caption='" + chartTitle + "' xAxisName='" + xTitle + "' yAxisName='" + yTitle + "'>");

            // We prepare the data in a nice hashtable
            Plots plots = PrepareData(xValue, yValue, groupAction);
            for (int i = 0; i < plots.Count(); i++)
            {              
                // We try to compute the color
                string color = "";
                try
                {
                    color = colorsArray[i % (colorsArray.Length)]; // computes to color to use
                }
                catch { } // if we fail we skip the error, default color will be used

                // We add a line to the XML
                sb.Append("<set name='" + plots.GetX(i) + "' value='" + plots.GetY(i).ToString() + "'");
                if (color != "") sb.Append(" color='" + color + "'");
                sb.AppendLine(" />");
            }

            // We close the graph tag opened previously
            sb.AppendLine("</graph>");

            // We finally return the xml data
            return sb.ToString();
        }

        #endregion

    }

    public enum GroupAction
    {
        NOTHING = 0,
        SUM,
        COUNT
    }

}
