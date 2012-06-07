using System;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Phresco.FusionCharts;

namespace Phresco.UnitTesting
{
    /// <summary>
    /// Summary description for FusionChartsTest
    /// </summary>
    [TestFixture]
    public class FusionChartsTest
    {
        public FusionChartsTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        [Test]
        public void GetChart_ValidParameter_ReturnType()
        {
            //Fusion Chart: Chart from list class unit test.
            ChartFromList cf = new ChartFromList();
            ChartFromXml cfx = new ChartFromXml();

            //Check the properties values 
            Assert.AreEqual("FCF_Area2D", cf.ChartType.ToString());
            //Assert.AreEqual("", cfx.ChartType.ToString());
        }

    }
}
