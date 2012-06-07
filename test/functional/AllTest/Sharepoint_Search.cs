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

namespace searchText
{
    [TestFixture]
    public class SearchText_Home_List
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
        public void SearchInHomePageTest()
        {
            DataSet phresco = new DataSet();
            phresco.ReadXml(ConfigurationSettings.AppSettings["XmlPath"].ToString());
            try
            {
                //Opens Phresco Home Page.   
                driver.Navigate().GoToUrl(baseUrl);
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //Search for Text in All Sites.
                IWebElement searchHome = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[79].ToString()));
                SelectElement clickHome = new SelectElement(searchHome);
                clickHome.SelectByText(phresco.Tables[0].Rows[0].ItemArray[80].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[81].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[82].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[83].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[84].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                IWebElement searchPhresco = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[85].ToString()));
                SelectElement clickPhresco = new SelectElement(searchPhresco);
                clickPhresco.SelectByText(phresco.Tables[0].Rows[0].ItemArray[86].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[87].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[88].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[83].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                                
           }
            catch (Exception e)
            {
                TakeScreenshot(driver, phresco.Tables[0].Rows[0].ItemArray[89].ToString());
                throw e;
            }
        }
        [Test]
        public void SearchTextInListTest()
        {
            DataSet phresco = new DataSet();
            phresco.ReadXml(ConfigurationSettings.AppSettings["XmlPath"].ToString());
            try
            {//Opens Phresco Home Page.   
                driver.Navigate().GoToUrl(baseUrl);
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                // Opens All Lists.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[90].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                // Search Text in VM_Allocation.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[91].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                IWebElement searchList = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[92].ToString()));
                SelectElement clickList = new SelectElement(searchList);
                clickList.SelectByText(phresco.Tables[0].Rows[0].ItemArray[93].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[94].ToString())).SendKeys(phresco.Tables[0].Rows[0].ItemArray[95].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[96].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));

            }
            catch (Exception e)
            {
                TakeScreenshot(driver, phresco.Tables[0].Rows[0].ItemArray[97].ToString());
                throw e;
            }
        }
    }
}