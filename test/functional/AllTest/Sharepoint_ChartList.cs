/*
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

namespace ChartList_Create_Delete
{
    [TestFixture]
    public class Chart_Create_Delete
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
        public void ChartListPrepTest()
        {
            DataSet phresco = new DataSet();
            phresco.ReadXml(ConfigurationSettings.AppSettings["XmlPath"].ToString());
            Directory.CreateDirectory(phresco.Tables[0].Rows[0].ItemArray[98].ToString());
            try
            {
                //Opens Phresco Home Page.   
                driver.Navigate().GoToUrl(baseUrl);
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[4].ToString())).Click();
                //Opens VM_allocation List Web Page.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[5].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //Opens New VM_Allocation List Definition Page.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[6].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[7].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[8].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[9].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[10].ToString());
                IWebElement selectWindows = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[11].ToString()));
                SelectElement clickThis = new SelectElement(selectWindows);
                clickThis.SelectByText(phresco.Tables[0].Rows[0].ItemArray[12].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[13].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[14].ToString());
                IWebElement hardwareType = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[15].ToString()));
                SelectElement clickHT = new SelectElement(hardwareType);
                clickHT.SelectByText(phresco.Tables[0].Rows[0].ItemArray[16].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[17].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[18].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[19].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[20].ToString());
                IWebElement statusType = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[21].ToString()));
                SelectElement clickActive = new SelectElement(statusType);
                clickActive.SelectByText(phresco.Tables[0].Rows[0].ItemArray[22].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[23].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
               
            }
            catch (Exception e)
            {
                TakeScreenshot(driver, phresco.Tables[0].Rows[0].ItemArray[24].ToString());
                throw e;
            }
        }
        [Test]
        public void RegularExpressionTest()
        {
            DataSet phresco = new DataSet();
            phresco.ReadXml(ConfigurationSettings.AppSettings["XmlPath"].ToString());
            try
            {
                //Opens Phresco Home Page.   
                driver.Navigate().GoToUrl(baseUrl);
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[4].ToString())).Click();
                //Opens VM_allocation List Web Page.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[5].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //Opens New VM_Allocation List Definition Page.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[6].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[7].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[8].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[9].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[10].ToString());
                IWebElement selectWindows = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[11].ToString()));
                SelectElement clickThis = new SelectElement(selectWindows);
                clickThis.SelectByText(phresco.Tables[0].Rows[0].ItemArray[12].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[13].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[14].ToString());
                IWebElement hardwareType = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[15].ToString()));
                SelectElement clickHT = new SelectElement(hardwareType);
                clickHT.SelectByText(phresco.Tables[0].Rows[0].ItemArray[16].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[17].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[18].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[19].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[20].ToString());
                IWebElement statusType = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[21].ToString()));
                SelectElement clickActive = new SelectElement(statusType);
                clickActive.SelectByText(phresco.Tables[0].Rows[0].ItemArray[22].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[25].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[26].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[23].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
            }
            catch (Exception e)
            {
                TakeScreenshot(driver, phresco.Tables[0].Rows[0].ItemArray[27].ToString());
                throw e;
            }
        }
        [Test]
        public void InvalidRegularExpressionTest()
        {
            DataSet phresco = new DataSet();
            phresco.ReadXml(ConfigurationSettings.AppSettings["XmlPath"].ToString());
            try
            {
                //Opens Phresco Home Page.   
                driver.Navigate().GoToUrl(baseUrl);
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[4].ToString())).Click();
                //Opens VM_allocation List Web Page.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[5].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //Opens New VM_Allocation List Definition Page.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[6].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[7].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[8].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[9].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[10].ToString());
                IWebElement selectWindows = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[11].ToString()));
                SelectElement clickThis = new SelectElement(selectWindows);
                clickThis.SelectByText(phresco.Tables[0].Rows[0].ItemArray[12].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[13].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[14].ToString());
                IWebElement hardwareType = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[15].ToString()));
                SelectElement clickHT = new SelectElement(hardwareType);
                clickHT.SelectByText(phresco.Tables[0].Rows[0].ItemArray[16].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[17].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[18].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[19].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[20].ToString());
                IWebElement statusType = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[21].ToString()));
                SelectElement clickActive = new SelectElement(statusType);
                clickActive.SelectByText(phresco.Tables[0].Rows[0].ItemArray[22].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[25].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[28].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[23].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
            }
            catch (Exception e)
            {
                TakeScreenshot(driver, phresco.Tables[0].Rows[0].ItemArray[30].ToString());
                throw e;
            }

        }
        


    }
}