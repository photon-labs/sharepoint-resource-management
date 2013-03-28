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
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;

namespace Phresco.FusionCharts
{
    [Guid("3dd2d2e7-b5a3-4165-b3d1-421774fafa0f")]
    public class ChartFromList : WebPart
    {
        #region PrivateMembers

        private bool                _error = false;
        private string              _chartId = null; //  null by default (important)
        private ChartTypeRestricted _chartType;
        private int                 _chartHeight = Utils.DefaultHeight; // default height
        private int                 _chartWidth = Utils.DefaultWidth; // default width
        private string              _listName = ""; // The name of the list to use
        private string              _viewName = ""; // The name of the view to use
        private string              _chartTitle = ""; // The title of the chart
        private string              _xValue = ""; // The name of the column of the list to use as horizontal value
        private string              _xTitle = ""; // The title of hortizontal axis
        private string              _yValue = ""; // The name of the column of the list to use as vertical value
        private string              _yTitle = ""; // The title of vertical axis
        private string              _colors = null; // null by default (important). Colors that will be used
        private bool                _excelExport = false; // display/hide the custom button to export chart to excel
        private bool                _xmlExport = false; // display/hide the custom button to export chart to xml file
        private GroupAction         _action;

        #endregion

        #region CustomProperties

        /// <summary>
        /// The title of the chart
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Chart Title")]
        [WebDescription("The title of the chart (caption).")]
        public string ChartTitle
        {
            get { return _chartTitle; }
            set { _chartTitle = value; }
        }

        /// <summary>
        /// The type of chart to display
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Chart Type")]
        [WebDescription("The type of chart that will be displayed")]
        public ChartTypeRestricted ChartType
        {
            get { return _chartType; }
            set { _chartType = value; }
        }

        /// <summary>
        /// The height of the chart that will be displayed
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Chart Height")]
        [WebDescription("The height of the chart to display")]
        public int ChartHeight
        {
            get { return _chartHeight; }
            set { _chartHeight = value; }
        }

        /// <summary>
        /// The width of the chart that will be displayed
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Chart Width")]
        [WebDescription("The width of the chart to display")]
        public int ChartWidth
        {
            get { return _chartWidth; }
            set { _chartWidth = value; }
        }

        /// <summary>
        /// The list to use to chart
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("List Name (SharePoint List)")]
        [WebDescription("The list to use to chart")]
        public string ListName
        {
            get { return _listName; }
            set { _listName = value; }
        }

        /// <summary>
        /// The view of the list to use to chart
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("View Name (SharePoint View). If empty the default view will be used.")]
        [WebDescription("The view (of the list) to use in order to generate a graph")]
        public string ViewName
        {
            get { return _viewName; }
            set { _viewName = value; }
        }

        /// <summary>
        /// Name of the column to use as the horizontal value
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Name of the column to use as horizontal (x) value")]
        [WebDescription("This should be a existing column name of a SharePoint List")]
        public string XValue
        {
            get { return _xValue; }
            set { _xValue = value; }
        }

        /// <summary>
        /// Name of the column to use as the vertical value
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Name of the column to use as vertical (y) value. Leave empty if you select a 'Count' action below")]
        [WebDescription("This should be a existing column name of a SharePoint List")]
        public string YValue
        {
            get { return _yValue; }
            set { _yValue = value; }
        }

        /// <summary>
        /// Group action to perform
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Action to perform over colmuns")]
        [WebDescription("")]
        public GroupAction Action
        {
            get { return _action; }
            set { _action = value; }
        }

        /// <summary>
        /// Title of the horizontal values
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Horizontal (x) title to display")]
        [WebDescription("Title of the horizontal values that will be displayed on the chart")]
        public string XTitle
        {
            get { return _xTitle; }
            set { _xTitle = value; }
        }

        /// <summary>
        /// Title of the vertical values
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Vertical (y) title to display")]
        [WebDescription("Title of the horizontal values that will be displayed on the chart")]
        public string YTitle
        {
            get { return _yTitle; }
            set { _yTitle = value; }
        }

        /// <summary>
        /// List of colors to use while charting
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Colors to use")]
        [WebDescription("List of colors to use when charting")]
        public string Colors
        {
            get
            {
                if (_colors == null)
                {
                    _colors = Utils.GetDefaultColors();
                }
                return _colors;
            }
            set { _colors = value; }
        }

        /// <summary>
        /// The HTML Id of the chart
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Chart HTML ID")]
        [WebDescription("The Html ID that will identify the chart")]
        public string ChartID
        {
            get
            {
                if (_chartId == null) // Chart is missing an HTML ID
                {
                    // We generate a ID for the chart
                    _chartId = "chart" + Utils.GenerateRandomId().ToString();
                }
                return _chartId;
            }
            set { _chartId = value; }
        }

        /// <summary>
        /// does we have to display custom button to export to excel ?
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Enable excel export")]
        [WebDescription("Display a custom button to export the chart to excel")]
        public bool ExcelExport
        {
            get { return _excelExport; }
            set { _excelExport = value; }
        }

        /// <summary>
        /// does we have to display custom button to export to xml ?
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Enable XML export")]
        [WebDescription("Display a custom button to export the chart to a xml file")]
        public bool XMLExport
        {
            get { return _xmlExport; }
            set { _xmlExport = value; }
        }

        #endregion

        #region Constructors

        public ChartFromList()
        {
            this.ExportMode = WebPartExportMode.All;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create all your controls here for rendering.
        /// Try to avoid using the RenderWebPart() method.
        /// </summary>
        protected override void CreateChildControls()
        {
            if (!_error)
            {
                try
                {

                    base.CreateChildControls();

                    // Your code here...

                    // We first check all settings
                    CheckSettings();

                    // We transform the list into XML Input
                    ListToXmlConverter conv = new ListToXmlConverter(_listName, _viewName);
                    string chartXmlContent = conv.GenerateXml(ChartTitle, XValue, XTitle, YValue, YTitle, Action, Colors);

                    // We generate the graph
                    string chartHtml = Utils.RenderChartHTML(ChartType, chartXmlContent, ChartID.ToString(), ChartWidth.ToString(), ChartHeight.ToString(), false);
                    LiteralControl lc = new LiteralControl(chartHtml);
                    this.Controls.Add(lc);

                    // We add a cariage return to display correctly export buttons
                    if (this._excelExport || this._xmlExport)
                    {
                        this.Controls.Add(new LiteralControl("<br />"));
                    }

                    // display (or not, depending of the user choice option) the button "export to excel"
                    if (this._excelExport)
                    {
                        Button btnExcel = new Button();
                        btnExcel.Text = "Export chart to Excel";
                        btnExcel.Click += new System.EventHandler(this.ExportExcelButtonClick);
                        btnExcel.Style.Add("font-size", "10");
                        btnExcel.Style.Add("margin", "10");
                        this.Controls.Add(btnExcel);
                    }

                    // display (or not, depending of the user choice option) the button "export to xml"
                    if (this._xmlExport)
                    {
                        Button btnXML = new Button();
                        btnXML.Text = "Export chart to XML file";
                        btnXML.Click += new System.EventHandler(this.ExportXmlButtonClick);
                        btnXML.Style.Add("font-size", "10");
                        btnXML.Style.Add("margin", "10");
                        this.Controls.Add(btnXML);
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        /// <summary>
        /// Clear all child controls and add an error message for display.
        /// </summary>
        /// <param name="ex"></param>
        private void HandleException(Exception ex)
        {
            this._error = true;
            this.Controls.Clear();
            this.Controls.Add(new LiteralControl("ERROR: " + ex.Message));
        }

        /// <summary>
        /// Checks the web part properties (settings) before trying to render the chart
        /// </summary>
        /// <returns></returns>
        private void CheckSettings()
        {
            if (_listName == String.Empty)
            {
                throw (new Exception("Please edit the webpart properties to provide a SharePoint List name to use."));
            }

            if (_xValue == String.Empty)
            {
                throw (new Exception("Please edit the webpart properties to provide a horizontal (x) column name to use."));
            }

            if (_yValue == String.Empty && Action != GroupAction.COUNT)
            {
                throw (new Exception("Please edit the webpart properties to provide a vertical (y) column name to use."));
            }
        }

        /// <summary>
        /// When the user click on the export to Excel button
        /// so we build a csv and then make it download with good header to make it opened by excel automatically
        /// </summary>
        protected void ExportExcelButtonClick(object sender, EventArgs e)
        {
            // We first check all settings
            CheckSettings();

            // We transform the list into XML Input
            ListToXmlConverter conv = new ListToXmlConverter(_listName, _viewName);
            
            // we build the csv string
            StringBuilder csv = new StringBuilder();
            csv.AppendLine(((XTitle != "") ? XTitle : "X") + ";" + ((YTitle != "") ? YTitle : "Y"));

            // We prepare the data in a nice hashtable
            Plots plots = conv.PrepareData(this._xValue, this._yValue, this._action);
            for (int i = 0; i < plots.Count(); i++)
            {
                csv.AppendLine(plots.GetX(i).Replace(",", " ").Replace(";", " ") + ";" + plots.GetY(i).ToString().Replace(",", " ").Replace(";", " "));
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/octet-stream";
            Context.Response.AddHeader("Content-Disposition", "attachment; filename=ExcelExport.csv");
            Context.Response.AddHeader("Content-Length", csv.Length.ToString());
            Context.Response.Write(csv.ToString());
            Context.Response.End();
        }

        /// <summary>
        /// When the user click on the export to XML button
        /// so we get to xml, and make it download
        /// </summary>
        protected void ExportXmlButtonClick(object sender, EventArgs e)
        {
            // We first check all settings
            CheckSettings();

            // We transform the list into XML Input
            ListToXmlConverter conv = new ListToXmlConverter(_listName, _viewName);
            string chartXmlContent = conv.GenerateXml(ChartTitle, XValue, XTitle, YValue, YTitle, Action, Colors);

            Context.Response.Clear();
            Context.Response.ContentType = "application/octet-stream";
            Context.Response.AddHeader("Content-Disposition", "attachment; filename=XMLExport.xml");
            Context.Response.AddHeader("Content-Length", chartXmlContent.Length.ToString());
            Context.Response.Write(chartXmlContent);
            Context.Response.End();
        }

        #endregion
    }
}
