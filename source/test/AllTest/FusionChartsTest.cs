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
