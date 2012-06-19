/*
 * ###
 * PHR_SharePointResourceManagement
 * %%
 * Copyright (C) 1999 - 2012 Photon Infotech Inc.
 * %%
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ###
 */
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.SharePoint;

namespace Phresco.FusionCharts
{
    /// <summary>
    /// Contains several very useful and required methods used to generate charts.
    /// Most the methods are statics.
    /// </summary>
    class Utils
    {
        public static string FeaturePath = "/FusionCharts/";
        private static string[] DefaultColors = { "AFD8F8", "F6BD0F", "8BBA00", "FF8E46", "008E8E", "D64646", "8E468E", "588526", "B3AA00", "008ED6", "9D080D", "A186BE" };
        public static int DefaultHeight = 400;
        public static int DefaultWidth = 600;


        /// <summary>
        /// Determines if the current connection is SSL based or not
        /// </summary>
        /// <returns>True if SSL, False if not SSL</returns>
        public static bool IsSSL()
        {
            return HttpContext.Current.Request.IsSecureConnection;
        }

        /// <summary>
        /// Generate a random number that will become the HTML ID of the chart
        /// </summary>
        /// <returns></returns>
        public static int GenerateRandomId()
        {
            Random random = new Random();
            return random.Next(0, 10000);
        }

        /// <summary>
        /// Converts a boolean into a numeric value
        /// </summary>
        /// <param name="value">the boolean value to convert</param>
        /// <returns>0 or 1 (integer)</returns>
        private static int boolToNum(bool value)
        {
            return (value ? 1 : 0);
        }

        /// <summary>
        /// Generates the Fusion Chart based on script
        /// </summary>
        /// <param name="chartSWF"></param>
        /// <param name="strURL"></param>
        /// <param name="strXML"></param>
        /// <param name="chartId"></param>
        /// <param name="chartWidth"></param>
        /// <param name="chartHeight"></param>
        /// <param name="debugMode"></param>
        /// <param name="registerWithJS"></param>
        /// <returns></returns>
        public static string RenderChart(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<!-- START Script Block for Chart {0} -->" + Environment.NewLine, chartId);
            builder.AppendFormat("<div id='{0}Div' align='center'>" + Environment.NewLine, chartId);
            builder.Append("Chart." + Environment.NewLine);
            builder.Append("</div>" + Environment.NewLine);
            builder.Append("<script type=\"text/javascript\">" + Environment.NewLine);
            builder.AppendFormat("var chart_{0} = new FusionCharts(\"{1}\", \"{0}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");" + Environment.NewLine, new object[] { chartId, chartSWF, chartWidth, chartHeight, boolToNum(debugMode), boolToNum(registerWithJS) });
            if (strXML.Length == 0)
            {
                builder.AppendFormat("chart_{0}.setDataURL(\"{1}\");" + Environment.NewLine, chartId, strURL);
            }
            else
            {
                builder.AppendFormat("chart_{0}.setDataXML(\"{1}\");" + Environment.NewLine, chartId, strXML);
            }
            builder.AppendFormat("chart_{0}.render(\"{1}Div\");" + Environment.NewLine, chartId, chartId);
            builder.Append("</script>" + Environment.NewLine);
            builder.AppendFormat("<!-- END Script Block for Chart {0} -->" + Environment.NewLine, chartId);
            return builder.ToString();
        }

        /// <summary>
        /// Generate the fusion chart based on HTML
        /// </summary>
        /// <param name="chartType"></param>
        /// <param name="strURL"></param>
        /// <param name="strXML"></param>
        /// <param name="chartId"></param>
        /// <param name="chartWidth"></param>
        /// <param name="chartHeight"></param>
        /// <param name="debugMode"></param>
        /// <returns></returns>
        public static string RenderChartHTML(ChartType chartType, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode)
        {
            StringBuilder builder = new StringBuilder();
            string str = string.Empty;

            // We convert the chart type to path
            string chartSWF = Utils.ConvertChartTypeToPath(chartType);

            // We get sample data in case no XML is specified
            if (strURL.Length == 0 && strXML.Length == 0)
            {
                strURL = Utils.GetXmlDataSample(chartType);
            }

            // Switch to use inline XML or external XML
            if (strXML.Length == 0)
            {
                str = string.Format("&chartWidth={0}&chartHeight={1}&debugMode={2}&dataURL={3}", new object[] { chartWidth, chartHeight, boolToNum(debugMode), strURL });
            }
            else
            {
                str = string.Format("&chartWidth={0}&chartHeight={1}&debugMode={2}&dataXML={3}", new object[] { chartWidth, chartHeight, boolToNum(debugMode), strXML });
            }

            // We determine if we should is http or https prefix
            string protocol = IsSSL() ? "https://" : "http://";

            // We build the HTML and returns it
            builder.AppendFormat("<!-- START Code Block for Chart {0} -->" + Environment.NewLine, chartId);
            builder.AppendFormat("<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"" + protocol + "fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0\" width=\"{0}\" height=\"{1}\" name=\"{2}\">" + Environment.NewLine, chartWidth, chartHeight, chartId);
            builder.Append("<param name=\"allowScriptAccess\" value=\"always\" />" + Environment.NewLine);
            builder.AppendFormat("<param name=\"movie\" value=\"{0}\"/>" + Environment.NewLine, chartSWF);
            builder.AppendFormat("<param name=\"FlashVars\" value=\"{0}\" />" + Environment.NewLine, str);
            builder.Append("<param name=\"quality\" value=\"high\" />" + Environment.NewLine);
            builder.AppendFormat("<embed src=\"{0}\" FlashVars=\"{1}\" quality=\"high\" width=\"{2}\" height=\"{3}\" name=\"{4}\"  allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" pluginspage=\"" + protocol + "www.macromedia.com/go/getflashplayer\" />" + Environment.NewLine, new object[] { chartSWF, str, chartWidth, chartHeight, chartId });
            builder.Append("</object>" + Environment.NewLine);
            builder.AppendFormat("<!-- END Code Block for Chart {0} -->" + Environment.NewLine, chartId);
            return builder.ToString();
        }

        /// <summary>
        /// Generate the fusion chart based on HTML
        /// </summary>
        /// <param name="chartType"></param>
        /// <param name="strURL"></param>
        /// <param name="strXML"></param>
        /// <param name="chartId"></param>
        /// <param name="chartWidth"></param>
        /// <param name="chartHeight"></param>
        /// <param name="debugMode"></param>
        /// <returns></returns>
        public static string RenderChartHTML(ChartTypeRestricted chartType, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode)
        {
            StringBuilder builder = new StringBuilder();
            string str = string.Empty;

            // We convert the chart type to path
            string chartSWF = Utils.ConvertChartTypeToPath(chartType);

            // We use the XML given as param
            str = string.Format("&chartWidth={0}&chartHeight={1}&debugMode={2}&dataXML={3}", new object[] { chartWidth, chartHeight, boolToNum(debugMode), strXML });

            // We determine if we should is http or https prefix
            string protocol = IsSSL() ? "https://" : "http://";

            // We build the HTML and returns it
            builder.AppendFormat("<!-- START Code Block for Chart {0} -->" + Environment.NewLine, chartId);
            builder.AppendFormat("<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\""+ protocol + "fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0\" width=\"{0}\" height=\"{1}\" name=\"{2}\">" + Environment.NewLine, chartWidth, chartHeight, chartId);
            builder.Append("<param name=\"allowScriptAccess\" value=\"always\" />" + Environment.NewLine);
            builder.AppendFormat("<param name=\"movie\" value=\"{0}\"/>" + Environment.NewLine, chartSWF);
            builder.AppendFormat("<param name=\"FlashVars\" value=\"{0}\" />" + Environment.NewLine, str);
            builder.Append("<param name=\"quality\" value=\"high\" />" + Environment.NewLine);
            builder.AppendFormat("<embed src=\"{0}\" FlashVars=\"{1}\" quality=\"high\" width=\"{2}\" height=\"{3}\" name=\"{4}\"  allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" pluginspage=\"" + protocol + "www.macromedia.com/go/getflashplayer\" />" + Environment.NewLine, new object[] { chartSWF, str, chartWidth, chartHeight, chartId });
            builder.Append("</object>" + Environment.NewLine);
            builder.AppendFormat("<!-- END Code Block for Chart {0} -->" + Environment.NewLine, chartId);
            return builder.ToString();
        }

        /// <summary>
        /// Converts a ChartType into a path that can be used with RenderChartHTML function
        /// </summary>
        /// <param name="chartType">The type of the chart</param>
        /// <returns></returns>
        public static string ConvertChartTypeToPath(ChartType chartType)
        {
            if (ChartType.FCF_Area2D == chartType)              return SPContext.Current.Site.Url + FeaturePath + "FCF_Area2D.swf";
            if (ChartType.FCF_Bar2D == chartType)               return SPContext.Current.Site.Url + FeaturePath + "FCF_Bar2D.swf";
            if (ChartType.FCF_Candlestick == chartType)         return SPContext.Current.Site.Url + FeaturePath + "FCF_Candlestick.swf";
            if (ChartType.FCF_Column2D == chartType)            return SPContext.Current.Site.Url + FeaturePath + "FCF_Column2D.swf";
            if (ChartType.FCF_Column3D == chartType)            return SPContext.Current.Site.Url + FeaturePath + "FCF_Column3D.swf";
            if (ChartType.FCF_Doughnut2D == chartType)          return SPContext.Current.Site.Url + FeaturePath + "FCF_Doughnut2D.swf";
            if (ChartType.FCF_Funnel == chartType)              return SPContext.Current.Site.Url + FeaturePath + "FCF_Funnel.swf";
            if (ChartType.FCF_Gantt == chartType)               return SPContext.Current.Site.Url + FeaturePath + "FCF_Gantt.swf";
            if (ChartType.FCF_Line == chartType)                return SPContext.Current.Site.Url + FeaturePath + "FCF_Line.swf";
            if (ChartType.FCF_MSArea2D == chartType)            return SPContext.Current.Site.Url + FeaturePath + "FCF_MSArea2D.swf";
            if (ChartType.FCF_MSBar2D == chartType)             return SPContext.Current.Site.Url + FeaturePath + "FCF_MSBar2D.swf";
            if (ChartType.FCF_MSColumn2D == chartType)          return SPContext.Current.Site.Url + FeaturePath + "FCF_MSColumn2D.swf";
            if (ChartType.FCF_MSColumn2DLineDY == chartType)    return SPContext.Current.Site.Url + FeaturePath + "FCF_MSColumn2DLineDY.swf";
            if (ChartType.FCF_MSColumn3D == chartType)          return SPContext.Current.Site.Url + FeaturePath + "FCF_MSColumn3D.swf";
            if (ChartType.FCF_MSColumn3DLineDY == chartType)    return SPContext.Current.Site.Url + FeaturePath + "FCF_MSColumn3DLineDY.swf";
            if (ChartType.FCF_MSLine == chartType)              return SPContext.Current.Site.Url + FeaturePath + "FCF_MSLine.swf";
            if (ChartType.FCF_Pie2D == chartType)               return SPContext.Current.Site.Url + FeaturePath + "FCF_Pie2D.swf";
            if (ChartType.FCF_Pie3D == chartType)               return SPContext.Current.Site.Url + FeaturePath + "FCF_Pie3D.swf";
            if (ChartType.FCF_StackedArea2D == chartType)       return SPContext.Current.Site.Url + FeaturePath + "FCF_StackedArea2D.swf";
            if (ChartType.FCF_StackedBar2D == chartType)        return SPContext.Current.Site.Url + FeaturePath + "FCF_StackedBar2D.swf";
            if (ChartType.FCF_StackedColumn2D == chartType)     return SPContext.Current.Site.Url + FeaturePath + "FCF_StackedColumn2D.swf";
            if (ChartType.FCF_StackedColumn3D == chartType)     return SPContext.Current.Site.Url + FeaturePath + "FCF_StackedColumn3D.swf";

            // Default charts in case there was no match
            string defaultChartType = SPContext.Current.Site.Url + FeaturePath + "FCF_Area2D.swf";
            return defaultChartType;
        }

        /// <summary>
        /// Converts a ChartType into a path that can be used with RenderChartHTML function
        /// </summary>
        /// <param name="chartType">The type of the chart</param>
        /// <returns></returns>
        public static string ConvertChartTypeToPath(ChartTypeRestricted chartType)
        {
            if (ChartTypeRestricted.FCF_Area2D == chartType) return SPContext.Current.Site.Url + FeaturePath + "FCF_Area2D.swf";
            if (ChartTypeRestricted.FCF_Bar2D == chartType) return SPContext.Current.Site.Url + FeaturePath + "FCF_Bar2D.swf";
            if (ChartTypeRestricted.FCF_Column2D == chartType) return SPContext.Current.Site.Url + FeaturePath + "FCF_Column2D.swf";
            if (ChartTypeRestricted.FCF_Column3D == chartType) return SPContext.Current.Site.Url + FeaturePath + "FCF_Column3D.swf";
            if (ChartTypeRestricted.FCF_Doughnut2D == chartType) return SPContext.Current.Site.Url + FeaturePath + "FCF_Doughnut2D.swf";
            if (ChartTypeRestricted.FCF_Funnel == chartType) return SPContext.Current.Site.Url + FeaturePath + "FCF_Funnel.swf";
            if (ChartTypeRestricted.FCF_Line == chartType) return SPContext.Current.Site.Url + FeaturePath + "FCF_Line.swf";
            if (ChartTypeRestricted.FCF_Pie2D == chartType) return SPContext.Current.Site.Url + FeaturePath + "FCF_Pie2D.swf";
            if (ChartTypeRestricted.FCF_Pie3D == chartType) return SPContext.Current.Site.Url + FeaturePath + "FCF_Pie3D.swf";

            // Default charts in case there was no match
            string defaultChartType = SPContext.Current.Site.Url + FeaturePath + "FCF_Area2D.swf";
            return defaultChartType;
        }

        /// <summary>
        /// Returns sample XML data to feed and generate charts
        /// </summary>
        /// <returns></returns>
        public static string GetXmlDataSample(ChartType type)
        {
            if (type == ChartType.FCF_Column2D ||
                type == ChartType.FCF_Column3D ||
                type == ChartType.FCF_Pie2D ||
                type == ChartType.FCF_Pie3D ||
                type == ChartType.FCF_Line ||
                type == ChartType.FCF_Bar2D ||
                type == ChartType.FCF_Area2D ||
                type == ChartType.FCF_Doughnut2D ||
                type == ChartType.FCF_Funnel)
                return SPContext.Current.Site.Url + FeaturePath + "DataSampleSS1.xml";

            if (type == ChartType.FCF_Candlestick)
                return SPContext.Current.Site.Url + FeaturePath + "DataSampleCandleStick1.xml";

            if (type == ChartType.FCF_MSArea2D || 
                type == ChartType.FCF_MSBar2D || 
                type == ChartType.FCF_MSColumn2D || 
                type == ChartType.FCF_MSColumn3D || 
                type == ChartType.FCF_MSLine)
                return SPContext.Current.Site.Url + FeaturePath + "DataSampleMS1.xml";

            if (type == ChartType.FCF_MSColumn2DLineDY ||
                type == ChartType.FCF_MSColumn3DLineDY)
                return SPContext.Current.Site.Url + FeaturePath + "DataSampleCombi1.xml";

            if (type == ChartType.FCF_StackedArea2D ||
                type == ChartType.FCF_StackedBar2D ||
                type == ChartType.FCF_StackedColumn2D ||
                type == ChartType.FCF_StackedColumn3D)
                return SPContext.Current.Site.Url + FeaturePath + "DataSampleStacked1.xml";

            if (type == ChartType.FCF_Gantt)
                return SPContext.Current.Site.Url + FeaturePath + "DataSampleGantt1.xml";

            // By default we return the Simple Serie sample #1
            return SPContext.Current.Site.Url + FeaturePath + "DataSampleSS1.xml";
        }

        /// <summary>
        /// Returns the default colors in a string. Colors are separated by semi columns (;)
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultColors()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string color in DefaultColors)
            {
                sb.Append(color + ";");
            }
            // We return the string and remove the last semi column
            return sb.ToString().Substring(0, sb.Length-1);
        }
    }

    /// <summary>
    /// All Type of charts available
    /// </summary>
    public enum ChartType
    {
        FCF_Area2D,
        FCF_Bar2D,
        FCF_Candlestick,
        FCF_Column2D,
        FCF_Column3D,
        FCF_Doughnut2D,
        FCF_Funnel,
        FCF_Gantt,
        FCF_Line,
        FCF_MSArea2D,
        FCF_MSBar2D,
        FCF_MSColumn2D,
        FCF_MSColumn2DLineDY,
        FCF_MSColumn3D,
        FCF_MSColumn3DLineDY,
        FCF_MSLine,
        FCF_Pie2D,
        FCF_Pie3D,
        FCF_StackedArea2D,
        FCF_StackedBar2D,
        FCF_StackedColumn2D,
        FCF_StackedColumn3D
    };

    /// <summary>
    /// Restricted type of charts that can be used with SharePoint Lists
    /// </summary>
    public enum ChartTypeRestricted
    {
        FCF_Area2D,
        FCF_Bar2D,
        FCF_Column2D,
        FCF_Column3D,
        FCF_Doughnut2D,
        FCF_Funnel,
        FCF_Line,
        FCF_Pie2D,
        FCF_Pie3D
    }
}
