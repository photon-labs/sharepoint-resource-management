using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Web.UI.WebControls;

namespace Phresco.FusionCharts
{
    /// <summary>
    /// Displays a Fusion Chart Chart with data coming from
    /// an XML file (or content)
    /// </summary>
    [Guid("e884ae9e-3b17-435e-b002-854e90ac94b5")]
    public class ChartFromXml : WebPart
    {
        #region PrivateMembers

        private bool _error = false;
        private string _chartId = null; //  null by default (important)
        private ChartType _chartType;
        private string _chartXmlUrl = ""; // empty by default
        private string _chartXmlContent = ""; // empty by default
        private int _chartHeight = Utils.DefaultHeight; // default height
        private int _chartWidth = Utils.DefaultWidth; // default width
        private bool _xmlExport = false; // display/hide the custom button to export chart to xml file

        #endregion

        #region CustomProperties

        /// <summary>
        /// The type of chart to display
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Chart Type")]
        [WebDescription("The type of chart that will be displayed")]
        public ChartType ChartType
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
        /// The XML Url that contains the data to generate the chart
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Chart Xml Url")]
        [WebDescription("The Url of the Xml file that contains the data to generate the chart")]
        public string ChartXmlUrl
        {
            get { return _chartXmlUrl; }
            set { _chartXmlUrl = value; }
        }

        /// <summary>
        /// The XML Content (inline) that contains data to generate the chart
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("Fusion Charts")]
        [WebDisplayName("Chart Xml Content")]
        [WebDescription("The Xml content that contains the data to generate the chart")]
        public string ChartXmlContent
        {
            get { return _chartXmlContent; }
            set { _chartXmlContent = value; }
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
                if (_chartId == null)
                {
                    // We generate a ID for the chart
                    _chartId = "chart" + Utils.GenerateRandomId().ToString();
                }
                return _chartId;
            }
            set { _chartId = value; }
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

        /// <summary>
        /// Constructor
        /// </summary>
        public ChartFromXml()
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

                    // We create literal control that will display the HTML needed to chart
                    string chartHtml = Utils.RenderChartHTML(ChartType, ChartXmlUrl, ChartXmlContent, ChartID.ToString(), ChartWidth.ToString(), ChartHeight.ToString(), false);
                    LiteralControl lc = new LiteralControl(chartHtml);
                    this.Controls.Add(lc);

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
        /// When the user click on the export to XML button
        /// so we get to xml, and make it download
        /// </summary>
        protected void ExportXmlButtonClick(object sender, EventArgs e)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/octet-stream";
            Context.Response.AppendHeader("Content-Disposition", "attachment; filename=XMLExport.xml");
            Context.Response.AddHeader("Content-Length", ChartXmlContent.Length.ToString());
            Context.Response.Write(ChartXmlContent);
            Context.Response.End();
        }

        #endregion
    }
}
