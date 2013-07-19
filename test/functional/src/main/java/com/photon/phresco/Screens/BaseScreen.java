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
package com.photon.phresco.Screens;

import java.awt.AWTException;
import java.awt.Robot;
import java.awt.event.KeyEvent;
import java.io.File;
import java.io.IOException;

import org.apache.commons.io.FileUtils;
import org.apache.commons.lang.StringUtils;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.junit.Assert;
import org.openqa.selenium.By;
import org.openqa.selenium.OutputType;
import org.openqa.selenium.TakesScreenshot;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeDriverService;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.remote.Augmenter;
import org.openqa.selenium.support.ui.WebDriverWait;

import com.google.common.base.Function;
import com.photon.phresco.selenium.util.Constants;
import com.photon.phresco.selenium.util.GetCurrentDir;
import com.photon.phresco.selenium.util.ScreenException;
import com.photon.phresco.uiconstants.UIConstants;
import com.photon.phresco.uiconstants.SharepointData;

public class BaseScreen {

	private WebDriver driver;
	private ChromeDriverService chromeService;
	private static Log log = LogFactory.getLog("BaseScreen");
	private WebElement element;
	private SharepointData yuiWidgetConstants;
	private UIConstants uiConstants;

	// private Log log = LogFactory.getLog(getClass());

	public BaseScreen() {

	}

	public BaseScreen(String selectedBrowser, String applicationURL,
			String applicationContext, SharepointData yuiWidgetConstants,
			UIConstants uiConstants) throws ScreenException {

		this.yuiWidgetConstants = yuiWidgetConstants;
		this.uiConstants = uiConstants;
		instantiateBrowser(selectedBrowser, applicationURL, applicationContext);
	}

	public void instantiateBrowser(String selectedBrowser,
			String applicationURL, String applicationContext)
			throws ScreenException {

		if (selectedBrowser.equalsIgnoreCase(Constants.BROWSER_CHROME)) {

			try {
				// "D:/Selenium-jar/chromedriver_win_19.0.1068.0/chromedriver.exe"
				chromeService = new ChromeDriverService.Builder()
						.usingDriverExecutable(new File(getChromeLocation()))

						.usingAnyFreePort().build();
				log.info("-------------***LAUNCHING GOOGLECHROME***--------------");
				driver = new ChromeDriver(chromeService);
				driver.manage().window().maximize();
				driver.navigate().to(applicationURL + applicationContext);

			} catch (Exception e) {
				e.printStackTrace();
			}

		} else if (selectedBrowser.equalsIgnoreCase(Constants.BROWSER_IE)) {
			log.info("---------------***LAUNCHING INTERNET EXPLORE***-----------");
			driver = new InternetExplorerDriver();
			driver.navigate().to(applicationURL + applicationContext);

		} else if (selectedBrowser.equalsIgnoreCase(Constants.BROWSER_FIREFOX)) {
			log.info("-------------***LAUNCHING FIREFOX***--------------");
			driver = new FirefoxDriver();
			driver.manage().window().maximize();
			driver.navigate().to(applicationURL + applicationContext);

		}

		else if (selectedBrowser.equalsIgnoreCase(Constants.BROWSER_OPERA)) {
			log.info("-------------***LAUNCHING OPERA***--------------");
			// WebDriver driver = new OperaDriver();

			System.out.println("******entering window maximize********");
			Robot robot;
			try {
				robot = new Robot();
				robot.keyPress(KeyEvent.VK_ALT);
				robot.keyPress(KeyEvent.VK_SPACE);
				robot.keyRelease(KeyEvent.VK_ALT);
				robot.keyRelease(KeyEvent.VK_SPACE);
				robot.keyPress(KeyEvent.VK_X);
				robot.keyRelease(KeyEvent.VK_X);
			} catch (AWTException e) {
				e.printStackTrace();
			}
			driver.navigate().to(applicationURL + applicationContext);

		} else {
			throw new ScreenException(
					"------Only FireFox,InternetExplore,Chrome and Opera  works-----------");
		}
	}

	/*
	 * public static void windowMaximizeFirefox() {
	 * driver.manage().window().setPosition(new Point(0, 0)); java.awt.Dimension
	 * screenSize = java.awt.Toolkit.getDefaultToolkit() .getScreenSize();
	 * Dimension dim = new Dimension((int) screenSize.getWidth(), (int)
	 * screenSize.getHeight()); driver.manage().window().setSize(dim); }
	 */

	public void closeBrowser() {
		log.info("-------------***BROWSER CLOSING***--------------");
		if (driver != null) {

			driver.quit();
		}
		if (chromeService != null) {
			chromeService.stop();
		}
	}

	public static String getChromeLocation() {

		log.info("getChromeLocation:*****CHROME TARGET LOCATION FOUND***");
		String directory = System.getProperty("user.dir");
		String targetDirectory = getChromeFile();
		String location = directory + targetDirectory;
		return location;
	}

	public static String getChromeFile() {
		if (System.getProperty("os.name").startsWith(Constants.WINDOWS_OS)) {
			log.info("*******WINDOWS MACHINE FOUND*************");

			return Constants.WINDOWS_DIRECTORY + "/chromedriver.exe";
		} else if (System.getProperty("os.name").startsWith(Constants.LINUX_OS)) {
			log.info("*******LINUX MACHINE FOUND*************");
			return Constants.LINUX_DIRECTORY_64 + "/chromedriver";
		} else if (System.getProperty("os.name").startsWith(Constants.MAC_OS)) {
			log.info("*******MAC MACHINE FOUND*************");
			return Constants.MAC_DIRECTORY + "/chromedriver";
		} else {
			throw new NullPointerException("******PLATFORM NOT FOUND********");
		}

	}

	public WebElement getXpathWebElement(String xpath) throws Exception {
		log.info("Entering:-----getXpathWebElement-------");
		try {

			element = driver.findElement(By.xpath(xpath));

		} catch (Throwable t) {
			log.info("Entering:---------Exception in getXpathWebElement()-----------");
			t.printStackTrace();

		}
		return element;
	}

	public void getIdWebElement(String id) throws ScreenException {
		log.info("Entering:---getIdWebElement-----");
		try {
			element = driver.findElement(By.id(id));

		} catch (Throwable t) {
			log.info("Entering:---------Exception in getIdWebElement()----------");
			t.printStackTrace();

		}

	}

	public void getcssWebElement(String selector) throws ScreenException {
		log.info("Entering:----------getIdWebElement----------");
		try {
			element = driver.findElement(By.cssSelector(selector));

		} catch (Throwable t) {
			log.info("Entering:---------Exception in getIdWebElement()--------");

			t.printStackTrace();

		}

	}

	public void waitForElementPresent(String locator, String methodName)
			throws IOException, Exception {
		try {
			log.info("Entering:--------waitForElementPresent()--------");
			By by = By.xpath(locator);
			WebDriverWait wait = new WebDriverWait(driver, 10);
			log.info("Waiting:--------One second----------");
			wait.until(presenceOfElementLocated(by));
		}

		catch (Exception e) {
			captureScreenShot(methodName);
			
		}
	}
	

	 public void captureScreenShot(String methodName) {
	            log.info("ENTERING IN CAPTURE SCREENSHOT ");
	            WebDriver augmentedDriver = new Augmenter().augment(driver);
	            File screenshot = ((TakesScreenshot) augmentedDriver)
	                            .getScreenshotAs(OutputType.FILE);
	            try {

	                    FileUtils.copyFile(screenshot,
	                                    new File(GetCurrentDir.getCurrentDirectory()
	                                                    + File.separator + methodName + ".png"));
	            } catch (Exception e1) {
	                    log.info("EXCEPTION IN CAPTURE SCREENSHOT " + e1.getMessage());
	            }
	    }

	Function<WebDriver, WebElement> presenceOfElementLocated(final By locator) {
		log.info("Entering:------presenceOfElementLocated()-----Start");
		return new Function<WebDriver, WebElement>() {
			public WebElement apply(WebDriver driver) {
				log.info("Entering:*********presenceOfElementLocated()******End");
				return driver.findElement(locator);

			}

		};

	}
	
//Test case I
	public void VM_Allocation(String methodName) throws Exception {

		if (StringUtils.isEmpty(methodName)) {
			methodName = Thread.currentThread().getStackTrace()[1]
					.getMethodName();
			;
		}
		
		//waitForElementPresent(this.uiConstants.AUDIO_DEVICES, methodName);
		getXpathWebElement(this.uiConstants.AUDIO_DEVICES);
		element.click();
		
		waitForElementPresent(this.uiConstants.PROD1_DETAILS, methodName);
		getXpathWebElement(this.uiConstants.PROD1_DETAILS);
		element.click();
	
		waitForElementPresent(this.uiConstants.DET_ADDTOCART, methodName);
		getXpathWebElement(this.uiConstants.DET_ADDTOCART);
		element.click();
		
		waitForElementPresent(this.uiConstants.CHECKOUT, methodName);
		getXpathWebElement(this.uiConstants.CHECKOUT);
		element.click();
		
		element.sendKeys(yuiWidgetConstants.TITLE_NAME);
		waitForElementPresent(this.uiConstants. VMALLOCATION_NEW_SYSTEM_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_SYSTEM_ID);
		element.click();
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_SYSTEM_ID_VALUE);
		waitForElementPresent(this.uiConstants. VMALLOCATION_NEW_OPERATINGSYSTEM_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_OPERATINGSYSTEM_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_OPERATINGSYSTEM_ID_VALUE);
		Thread.sleep(1000);
		waitForElementPresent(this.uiConstants.SYSTEM_CONFIGURATION_ID, methodName);
		getXpathWebElement(this.uiConstants. SYSTEM_CONFIGURATION_ID);
		element.sendKeys(yuiWidgetConstants.SYSTEM_CONFIGURATION_ID_VALUE);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_HARDWARETYPE_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_HARDWARETYPE_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_HARDWARETYPE_ID_VALUE);
		Thread.sleep(1000);	
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_SERIALNUMBER_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_SERIALNUMBER_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_SERIALNUMBER_VALUE);
		Thread.sleep(1000);		
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_OWNER_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_OWNER_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_OWNER_VALUE);
		Thread.sleep(1000);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_STATUS_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_STATUS_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_STATUS_VALUE);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_OK_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_OK_ID);
		element.click();
		
		
		
		
	}	
//test case II
	public void VM_Allocation_VALID_IP(String methodName) throws Exception {

		if (StringUtils.isEmpty(methodName)) {
			methodName = Thread.currentThread().getStackTrace()[1]
					.getMethodName();
			;
		}
		waitForElementPresent(this.uiConstants.PROD1_DETAILS, methodName);
		getXpathWebElement(this.uiConstants.PROD1_DETAILS);
		element.click();
		
		waitForElementPresent(this.uiConstants.NEW, methodName);
		getXpathWebElement(this.uiConstants.NEW);
		element.click();
		
		waitForElementPresent(this.uiConstants.CHECKOUT, methodName);
		getXpathWebElement(this.uiConstants.CHECKOUT);
		element.click();
		
		element.sendKeys(yuiWidgetConstants.TITLE_NAME);
		waitForElementPresent(this.uiConstants. VMALLOCATION_NEW_SYSTEM_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_SYSTEM_ID);
		element.click();
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_SYSTEM_ID_VALUE);
		waitForElementPresent(this.uiConstants. VMALLOCATION_NEW_OPERATINGSYSTEM_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_OPERATINGSYSTEM_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_OPERATINGSYSTEM_ID_VALUE);
		Thread.sleep(1000);
		waitForElementPresent(this.uiConstants.SYSTEM_CONFIGURATION_ID, methodName);
		getXpathWebElement(this.uiConstants. SYSTEM_CONFIGURATION_ID);
		element.sendKeys(yuiWidgetConstants.SYSTEM_CONFIGURATION_ID_VALUE);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_HARDWARETYPE_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_HARDWARETYPE_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_HARDWARETYPE_ID_VALUE);
		Thread.sleep(1000);	
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_SERIALNUMBER_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_SERIALNUMBER_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_SERIALNUMBER_VALUE);
		Thread.sleep(1000);		
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_OWNER_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_OWNER_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_OWNER_VALUE);
		Thread.sleep(1000);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_STATUS_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_STATUS_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_STATUS_VALUE);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_PRIVATEIP_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_PRIVATEIP_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_PRIVATEIP_ID_VALUE);
		Thread.sleep(1000);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_OK_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_OK_ID);
		element.click();
		Thread.sleep(1000);
		
	}
//test case III
	public void VM_Allocation_Invalid_ip(String methodName) throws Exception {

		if (StringUtils.isEmpty(methodName)) {
			methodName = Thread.currentThread().getStackTrace()[1]
					.getMethodName();
			;
		}
		Thread.sleep(5000);
		
		waitForElementPresent(this.uiConstants.PROD1_DETAILS, methodName);
		getXpathWebElement(this.uiConstants.PROD1_DETAILS);
		element.click();
		
		waitForElementPresent(this.uiConstants.DET_ADDTOCART, methodName);
		getXpathWebElement(this.uiConstants.DET_ADDTOCART);
		element.click();
		
		waitForElementPresent(this.uiConstants.CHECKOUT, methodName);
		getXpathWebElement(this.uiConstants.CHECKOUT);
		element.click();
		
		element.sendKeys(yuiWidgetConstants.TITLE_NAME);
		waitForElementPresent(this.uiConstants. VMALLOCATION_NEW_SYSTEM_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_SYSTEM_ID);
		element.click();
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_SYSTEM_ID_VALUE);
		waitForElementPresent(this.uiConstants. VMALLOCATION_NEW_OPERATINGSYSTEM_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_OPERATINGSYSTEM_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_OPERATINGSYSTEM_ID_VALUE);
		Thread.sleep(1000);
		waitForElementPresent(this.uiConstants.SYSTEM_CONFIGURATION_ID, methodName);
		getXpathWebElement(this.uiConstants. SYSTEM_CONFIGURATION_ID);
		element.sendKeys(yuiWidgetConstants.SYSTEM_CONFIGURATION_ID_VALUE);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_HARDWARETYPE_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_HARDWARETYPE_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_HARDWARETYPE_ID_VALUE);
		Thread.sleep(1000);	
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_SERIALNUMBER_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_SERIALNUMBER_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_SERIALNUMBER_VALUE);
		Thread.sleep(1000);		
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_OWNER_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_OWNER_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_OWNER_VALUE);
		Thread.sleep(1000);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_STATUS_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_STATUS_ID);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_STATUS_VALUE);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_PRIVATEIP_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_PRIVATEIP_ID).click();
		Thread.sleep(2000);
		element.sendKeys(yuiWidgetConstants.VMALLOCATION_NEW_PRIVATEIP_ID_VALUE_INVALID);
		waitForElementPresent(this.uiConstants.VMALLOCATION_NEW_OK_ID, methodName);
		getXpathWebElement(this.uiConstants. VMALLOCATION_NEW_OK_ID);
		element.click();
		Thread.sleep(2000);
		isTextPresent("Please Enter a valid IP",methodName);
		waitForElementPresent(this.uiConstants.CANCEL, methodName);
		getXpathWebElement(this.uiConstants. CANCEL);
		element.click();
		

	}
	
// test case IV 
	public void create_list(String methodName) throws Exception {

		if (StringUtils.isEmpty(methodName)) {
			methodName = Thread.currentThread().getStackTrace()[1]
					.getMethodName();
			;
		}
		waitForElementPresent(this.uiConstants.VIEWALL_SITE, methodName);
		getXpathWebElement(this.uiConstants.VIEWALL_SITE);
		element.click();
		
		waitForElementPresent(this.uiConstants.CREATE, methodName);
		getXpathWebElement(this.uiConstants.CREATE);
		element.click();
		waitForElementPresent(this.uiConstants.PLIST_DEF, methodName);
		getXpathWebElement(this.uiConstants.PLIST_DEF);
		element.click();
		
		waitForElementPresent(this.uiConstants.NAME, methodName);
		getXpathWebElement(this.uiConstants.NAME);
		element.sendKeys(yuiWidgetConstants.NAME_VALUE);
		
		waitForElementPresent(this.uiConstants.DESCRIPTION, methodName);
		getXpathWebElement(this.uiConstants.DESCRIPTION);
		element.sendKeys(yuiWidgetConstants.DEC_VALUE);
		
		waitForElementPresent(this.uiConstants.CREATEBUTTON, methodName);
		getXpathWebElement(this.uiConstants.CREATEBUTTON);
		element.click();
		


	}

	public void LIST(String methodName) throws Exception {

		if (StringUtils.isEmpty(methodName)) {
			methodName = Thread.currentThread().getStackTrace()[1]
					.getMethodName();
			;
		}
		waitForElementPresent(this.uiConstants.LIST, methodName);
		getXpathWebElement(this.uiConstants.LIST);
		element.click();
		waitForElementPresent(this.uiConstants.SAMPLE_CRT_DEMO, methodName);
		getXpathWebElement(this.uiConstants.SAMPLE_CRT_DEMO);
		element.click();
		waitForElementPresent(this.uiConstants.LISTNEW, methodName);
		getXpathWebElement(this.uiConstants.LISTNEW);
		element.click();
		waitForElementPresent(this.uiConstants.TITLE, methodName);
		getXpathWebElement(this.uiConstants.TITLE);
		element.sendKeys(yuiWidgetConstants.TITLE_VALUE);
		
		waitForElementPresent(this.uiConstants.SINGLE_LINE_TEXT, methodName);
		getXpathWebElement(this.uiConstants.SINGLE_LINE_TEXT).clear();
		element.sendKeys(yuiWidgetConstants.SINGLE_LINE_TEXT_VALUE);
		
		waitForElementPresent(this.uiConstants.MUL_CHOICE, methodName);
		getXpathWebElement(this.uiConstants.MUL_CHOICE);
		element.click();
		
		waitForElementPresent(this.uiConstants.MUL_CHOICE_SELECT, methodName);
		getXpathWebElement(this.uiConstants.MUL_CHOICE_SELECT);
		element.click();
		

		waitForElementPresent(this.uiConstants.RADIO_CHOICE_SELECT, methodName);
		getXpathWebElement(this.uiConstants.RADIO_CHOICE_SELECT);
		element.click();
		
		waitForElementPresent(this.uiConstants.NUMBERI, methodName);
		getXpathWebElement(this.uiConstants.NUMBERI);
		element.sendKeys(yuiWidgetConstants.NUMBERI_VALUE);
		
		waitForElementPresent(this.uiConstants.NUMBERII, methodName);
		getXpathWebElement(this.uiConstants.NUMBERII);
		element.sendKeys(yuiWidgetConstants.NUMBERII_VALUE);
		
		waitForElementPresent(this.uiConstants.CUR_TUK, methodName);
		getXpathWebElement(this.uiConstants.CUR_TUK);
		element.sendKeys(yuiWidgetConstants.CUR_TUK_VALUE);
	      
		waitForElementPresent(this.uiConstants.CUR_ITA, methodName);
		getXpathWebElement(this.uiConstants.CUR_ITA);
		element.sendKeys(yuiWidgetConstants.CUR_ITA_VALUE);
		
		waitForElementPresent(this.uiConstants.DATE, methodName);
		getXpathWebElement(this.uiConstants.DATE);
		element.sendKeys(yuiWidgetConstants.DATE_VALUE);
		
		waitForElementPresent(this.uiConstants.OK, methodName);
		getXpathWebElement(this.uiConstants.OK);
		element.click();
		
		}
	
	

	public void MostersPage(String methodName) throws Exception {

		if (StringUtils.isEmpty(methodName)) {
			methodName = Thread.currentThread().getStackTrace()[1]
					.getMethodName();
			;
		}
		waitForElementPresent(this.uiConstants.MASTERPAGE, methodName);
		getXpathWebElement(this.uiConstants.MASTERPAGE);
		element.click();
		waitForElementPresent(this.uiConstants.SITE_ACTION, methodName);
		getXpathWebElement(this.uiConstants.SITE_ACTION);
		element.click();
		waitForElementPresent(this.uiConstants.SITE_SETTINGS, methodName);
		getXpathWebElement(this.uiConstants.SITE_SETTINGS);
		element.click();
		waitForElementPresent(this.uiConstants.MASTER_PAGELINK, methodName);
		getXpathWebElement(this.uiConstants.MASTER_PAGELINK);
		element.click();
		waitForElementPresent(this.uiConstants.RADIOBUTTON1, methodName);
		getXpathWebElement(this.uiConstants.RADIOBUTTON1);
		element.click();
		waitForElementPresent(this.uiConstants.MASTER_DROPDOWN, methodName);
		getXpathWebElement(this.uiConstants.MASTER_DROPDOWN);
		element.click();
		
		waitForElementPresent(this.uiConstants.MASTER_DROPDOWN_SELECT, methodName);
		getXpathWebElement(this.uiConstants.MASTER_DROPDOWN_SELECT);
		element.click();
		
		waitForElementPresent(this.uiConstants.RADIOBUTTON2, methodName);
		getXpathWebElement(this.uiConstants.RADIOBUTTON2);
		element.click();
		
		waitForElementPresent(this.uiConstants.MASTER_DROPDOWN1, methodName);
		getXpathWebElement(this.uiConstants.MASTER_DROPDOWN1);
		element.click();
		
		waitForElementPresent(this.uiConstants.MASTER_DROPDOWN_SELECT1, methodName);
		getXpathWebElement(this.uiConstants.MASTER_DROPDOWN_SELECT1);
		element.click();
		
		waitForElementPresent(this.uiConstants.MASTER_OK, methodName);
		getXpathWebElement(this.uiConstants.MASTER_OK);
		element.click();
		
		waitForElementPresent(this.uiConstants.PHRESCO, methodName);
		getXpathWebElement(this.uiConstants.PHRESCO);
		element.click();
	}

	public void HOME_SEARCH_ALL(String methodName) throws Exception {

		if (StringUtils.isEmpty(methodName)) {
			methodName = Thread.currentThread().getStackTrace()[1]
					.getMethodName();
			;
		}
		waitForElementPresent(this.uiConstants.HOMESEARCH_CLICK, methodName);
		getXpathWebElement(this.uiConstants.HOMESEARCH_CLICK).click();
		element.sendKeys("All Sites");
		//Thread.sleep(2000);
		//isTextPresent("All sites");
		/*waitForElementPresent(this.uiConstants.HOMESEARCH_CLICK_SELECT, methodName);
		getXpathWebElement(this.uiConstants.HOMESEARCH_CLICK_SELECT);
		element.click();*/
		waitForElementPresent(this.uiConstants.SEARCH_SELECT, methodName);
		getXpathWebElement(this.uiConstants.SEARCH_SELECT).click();
		/*element.click();
		waitForElementPresent(this.uiConstants.SEARCH_SELECT_VALUE, methodName);
		getXpathWebElement(this.uiConstants.SEARCH_SELECT_VALUE);*/
		element.sendKeys(yuiWidgetConstants.SEARCH_SELECT_TEXT);
	
		
		waitForElementPresent(this.uiConstants.SEARCH_HOME, methodName);
		getXpathWebElement(this.uiConstants.SEARCH_HOME);
		element.click();
		waitForElementPresent(this.uiConstants.PHRESCO, methodName);
		getXpathWebElement(this.uiConstants.PHRESCO);
		element.click();
		
		waitForElementPresent(this.uiConstants.HOMESEARCH_CLICK_THIS, methodName);
		getXpathWebElement(this.uiConstants.HOMESEARCH_CLICK_THIS);
		element.sendKeys("This Site: Phresco");
		
		//waitForElementPresent(this.uiConstants.HOMESEARCH_CLICK_THIS_SITE, methodName);
		//getXpathWebElement(this.uiConstants.HOMESEARCH_CLICK_THIS_SITE);
		
		waitForElementPresent(this.uiConstants.SEARCH_FIELD_CLICK, methodName);
		getXpathWebElement(this.uiConstants.SEARCH_FIELD_CLICK);
		/*element.click();
		
		waitForElementPresent(this.uiConstants.HOMESEARCH_CLICK_POST, methodName);
		getXpathWebElement(this.uiConstants.HOMESEARCH_CLICK_POST);*/
		element.sendKeys(yuiWidgetConstants.HOMESEARCH_CLICK_POST_VALUE);
	 	
		waitForElementPresent(this.uiConstants.SEARCH_IMG, methodName);
		getXpathWebElement(this.uiConstants.SEARCH_IMG);
		element.click();
		
		
		waitForElementPresent(this.uiConstants.PHRESCO, methodName);
		getXpathWebElement(this.uiConstants.PHRESCO);
		element.click();
		
	}
	
	public void VM_ALLOCATION_LIST(String methodName) throws Exception {

		if (StringUtils.isEmpty(methodName)) {
			methodName = Thread.currentThread().getStackTrace()[1]
					.getMethodName();
			;
		}
		waitForElementPresent(this.uiConstants.LIST, methodName);
		getXpathWebElement(this.uiConstants.LIST);
		element.click();
		waitForElementPresent(this.uiConstants.PROD1_DETAILS, methodName);
		getXpathWebElement(this.uiConstants.PROD1_DETAILS);
		element.click();
		waitForElementPresent(this.uiConstants.THIS_SITE_VMALL, methodName);
		getXpathWebElement(this.uiConstants.THIS_SITE_VMALL);
		element.click();
		element.sendKeys("This List: VM_Allocation");
		Thread.sleep(2000);
		waitForElementPresent(this.uiConstants.VM_SEARCH_FIELD, methodName);
		getXpathWebElement(this.uiConstants.VM_SEARCH_FIELD);
		element.sendKeys(yuiWidgetConstants.VM_SEARCH_FIELD_VALUE);
		
		waitForElementPresent(this.uiConstants.SEARCH_IMG, methodName);
		getXpathWebElement(this.uiConstants.SEARCH_IMG);
		element.click();

		waitForElementPresent(this.uiConstants.PHRESCO, methodName);
		getXpathWebElement(this.uiConstants.PHRESCO);
		element.click();
		
	}

	public void click() throws ScreenException {
		log.info("Entering:********click operation start********");
		try {
			element.click();
		} catch (Throwable t) {
			t.printStackTrace();
		}
		log.info("Entering:********click operation end********");

	}

	public void clear() throws ScreenException {
		log.info("Entering:********clear operation start********");
		try {
			element.clear();
		} catch (Throwable t) {
			t.printStackTrace();
		}
		log.info("Entering:********clear operation end********");

	}

	public void sendKeys(String text) throws ScreenException {
		log.info("Entering:********enterText operation start********");
		try {
			clear();
			element.sendKeys(text);

		} catch (Throwable t) {
			t.printStackTrace();
		}
		log.info("Entering:********enterText operation end********");
	}

	public void submit() throws ScreenException {
		log.info("Entering:********submit operation start********");
		try {
			element.submit();
		} catch (Throwable t) {
			t.printStackTrace();
		}
		log.info("Entering:********submit operation end********");

	}

	public void isTextPresent(String text,String methodName) {
		if (text!= null){
			log.info("ENTERING TEXT PRESENT");
		boolean value=driver.findElement(By.tagName("body")).getText().contains(text);	
		if(!value){
			captureScreenShot(methodName);
		}
		Assert.assertTrue(value);   
	    
	    }
		else
		{
			
			throw new RuntimeException("---- Text not existed----");
		}
	}
	public void isElementPresent(String element) throws Exception {

		WebElement testElement = getXpathWebElement(element);
		if (testElement.isDisplayed() && testElement.isEnabled()) {

			log.info("---Element found---");
		} else {
			throw new RuntimeException("--Element not found---");
			// Assert.fail("--Element is not present--"+testElement);

		}

	}

}
