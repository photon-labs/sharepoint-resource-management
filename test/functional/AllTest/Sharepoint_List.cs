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
﻿/*
Author by {phresco} QA Automation Team
*/
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using Selenium;
using OpenQA.Selenium.Support.UI;

namespace List_Creation_Definition_Demo
{
    [TestFixture]
    public class List_Creation_definition
    {
        public IWebDriver driver;
        private StringBuilder verificationErrors;
        String baseUrl;
        [SetUp]
        public void SetupTest()
        {
            DataSet phresco = new DataSet();
            phresco.ReadXml(ConfigurationSettings.AppSettings["XmlPath"].ToString());
            baseUrl = phresco.Tables[1].Rows[0].ItemArray[0].ToString() + ":" + "//" + phresco.Tables[1].Rows[0].ItemArray[1].ToString() + ":" + phresco.Tables[1].Rows[0].ItemArray[2].ToString() + "/" + phresco.Tables[1].Rows[0].ItemArray[3].ToString();
            try
            {
                verificationErrors = new StringBuilder();
                driver = new InternetExplorerDriver();
                driver.Navigate().GoToUrl(baseUrl);
            }
            catch (Exception e)
            {
                Assert.AreEqual(phresco.Tables[0].Rows[0].ItemArray[3].ToString(), e.ToString());
            }
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {

                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        public void TakeScreenshot(IWebDriver driver, string path)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(path, System.Drawing.Imaging.ImageFormat.Png);
            screenshot.ToString();
        }

        [Test]
        public void ListCreationTest()
        {
            DataSet phresco = new DataSet();
            phresco.ReadXml(ConfigurationSettings.AppSettings["XmlPath"].ToString());
           try
            {
                //Opens Phresco Home Page.   
                driver.Navigate().GoToUrl(baseUrl);
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //Opens All Site Content.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[31].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[32].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //Opens Phresco List Creation Page.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[33].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[34].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[35].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[36].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[37].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[38].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
   
           }
           catch (Exception e)
           {
               TakeScreenshot(driver, phresco.Tables[0].Rows[0].ItemArray[39].ToString());
               throw e;
           }
        }
        [Test]
        public void ListDefinitionTest()
        {
            DataSet phresco = new DataSet();
            phresco.ReadXml(ConfigurationSettings.AppSettings["XmlPath"].ToString());
            try
            {   
                
                //Opens Phresco Home Page. 
                driver.Navigate().GoToUrl(baseUrl);
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //View All Lists in Site.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[40].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //Opens Sample List.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[41].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //Opens New Sample List Definition Page.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[42].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[43].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[44].ToString());
                //selenium.Select(phresco.Tables[0].Rows[0].ItemArray[64].ToString(), phresco.Tables[0].Rows[0].ItemArray[65].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[45].ToString())).Clear();
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[45].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[46].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[47].ToString())).Click();
                IWebElement multi = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[48].ToString()));
                SelectElement clickNo = new SelectElement(multi);
                clickNo.SelectByText(phresco.Tables[0].Rows[0].ItemArray[49].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[50].ToString())).Click();
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[51].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[52].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[53].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[54].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[55].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[56].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[57].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[58].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[59].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[60].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[61].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                
            }
            catch (Exception e)
            {
                TakeScreenshot(driver, phresco.Tables[0].Rows[0].ItemArray[62].ToString());
                throw e;
            }
        }
    }
}