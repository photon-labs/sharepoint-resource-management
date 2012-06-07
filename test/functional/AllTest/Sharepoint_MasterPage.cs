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

namespace DefaultMasterPage
{
    [TestFixture]
    public class Default_Phresco_MasterPage
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
        public void DefaultMasterPageTest()
        {
            DataSet phresco = new DataSet();
            phresco.ReadXml(ConfigurationSettings.AppSettings["XmlPath"].ToString());
            try
            {
                //Opens Phresco Home Page.   
                driver.Navigate().GoToUrl(baseUrl);
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //View Master Page.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[64].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //Opens Site Actions Menu.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[65].ToString())).Click();
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[66].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[67].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[68].ToString())).Click();
                IWebElement DMaster = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[69].ToString()));
                SelectElement clickDMaster = new SelectElement(DMaster);
                clickDMaster.SelectByText(phresco.Tables[0].Rows[0].ItemArray[70].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[71].ToString())).Click();
                IWebElement DMasterPage = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[72].ToString()));
                SelectElement clickDMasterPage = new SelectElement(DMasterPage);
                clickDMasterPage.SelectByText(phresco.Tables[0].Rows[0].ItemArray[73].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[74].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[64].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
             
                
             }
            catch (Exception e)
            {
                TakeScreenshot(driver, phresco.Tables[0].Rows[0].ItemArray[75].ToString());
                throw e;
            }

        }
        [Test]
        public void PhrescoMasterPageTest()
        {
            DataSet phresco = new DataSet();
            phresco.ReadXml(ConfigurationSettings.AppSettings["XmlPath"].ToString());
            try
            {
                //Opens Phresco Home Page.   
                driver.Navigate().GoToUrl(baseUrl);
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //View Master Page.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[64].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                //Opens Site Actions Menu.
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[65].ToString())).Click();
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[66].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[67].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[68].ToString())).Click();
                IWebElement DMaster = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[69].ToString()));
                SelectElement clickDMaster = new SelectElement(DMaster);
                clickDMaster.SelectByText(phresco.Tables[0].Rows[0].ItemArray[76].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[71].ToString())).Click();
                IWebElement DMasterPage = driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[72].ToString()));
                SelectElement clickDMasterPage = new SelectElement(DMasterPage);
                clickDMasterPage.SelectByText(phresco.Tables[0].Rows[0].ItemArray[77].ToString());
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[74].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
                driver.FindElement(By.XPath(phresco.Tables[0].Rows[0].ItemArray[64].ToString())).Click();
                Thread.Sleep(Convert.ToInt32(phresco.Tables[0].Rows[0].ItemArray[2].ToString()));
             
            }
            catch (Exception e)
            {
                TakeScreenshot(driver, phresco.Tables[0].Rows[0].ItemArray[78].ToString());
                throw e;
            }
        }
   
        
    }
}