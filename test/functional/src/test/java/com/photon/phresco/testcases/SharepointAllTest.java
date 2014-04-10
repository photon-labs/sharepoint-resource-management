/**
 * PHR_SharePointResourceManagement
 *
 * Copyright (C) 1999-2014 Photon Infotech Inc.
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
package com.photon.phresco.testcases;

import java.io.IOException;


import org.testng.annotations.AfterTest;
import org.testng.annotations.BeforeTest;
import org.testng.annotations.Test;
import com.photon.phresco.Screens.WelcomeScreen;
import com.photon.phresco.uiconstants.PhrescoUiConstants;
import com.photon.phresco.uiconstants.UIConstants;
import com.photon.phresco.uiconstants.SharepointData;

public class SharepointAllTest {

	private static UIConstants uiConstants;
	private static PhrescoUiConstants phrescoUIConstants;
	private static WelcomeScreen welcomeScreen;
	private static String methodName;
	private static String selectedBrowser;
	private static SharepointData WidgetConstants;

	// private Log log = LogFactory.getLog(getClass());

	@BeforeTest
	public static void setUp() throws Exception {
		try {
			phrescoUIConstants = new PhrescoUiConstants();
			uiConstants = new UIConstants();
			//assertNotNull(uiConstants);
			WidgetConstants = new SharepointData();
			launchingBrowser();
			// menuScreen = welcomeScreen.menuScreen(uiConstants);
			methodName = Thread.currentThread().getStackTrace()[1]
					.getMethodName();
		} catch (Exception exception) {
			exception.printStackTrace();
		}
	}

	public static void launchingBrowser() throws Exception {
		try {
			String applicationURL = phrescoUIConstants.PROTOCOL + "://"
					+ phrescoUIConstants.HOST + ":" + phrescoUIConstants.PORT
					+ "/";
			selectedBrowser = phrescoUIConstants.BROWSER;
			welcomeScreen = new WelcomeScreen(selectedBrowser, applicationURL,
					phrescoUIConstants.CONTEXT, WidgetConstants, uiConstants);
		} catch (Exception exception) {
			exception.printStackTrace();

		}

	}

	@Test
	public void VM_Allocation()
			throws InterruptedException, IOException, Exception {
		try {

			System.out
					.println("---------testToVerifyTheVmAllocation()-------------");
			welcomeScreen.VM_Allocation(methodName);
			//welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}
	
	
	@Test
	public void VM_Allocation_VALID_IP()
			throws InterruptedException, IOException, Exception {
		try {

			System.out
					.println("---------testToVerifyVmvalidIP()-------------");
			welcomeScreen.VM_Allocation_VALID_IP(methodName);
			//welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}
	
	@Test
	public void VM_Allocation_Invalid_ip()
			throws InterruptedException, IOException, Exception {
		try {

			System.out
					.println("---------testToVerifyVmAllocationInvalidIP()-------------");
			welcomeScreen.VM_Allocation_Invalid_ip(methodName);
			//welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}
	
	@Test
	public void create_list()
			throws InterruptedException, IOException, Exception {
		try {

			System.out
					.println("---------testToVerifycreatelist()-------------");
			welcomeScreen.create_list(methodName);
			//welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}
	
	@Test
	public void LIST()
			throws InterruptedException, IOException, Exception {
		try {

			System.out
					.println("---------testToVerifycreatelist()-------------");
			welcomeScreen.LIST(methodName);
			//welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}
	
	@Test
	public void MostersPage()
			throws InterruptedException, IOException, Exception {
		try {

			System.out
					.println("---------testToVerifyMasterpage()-------------");
			welcomeScreen.MostersPage(methodName);
			//welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}
	
	
	@Test
	public void HOME_SEARCH_ALL()
			throws InterruptedException, IOException, Exception {
		try {

			System.out
					.println("---------testToVerifyhome_search()-------------");
			welcomeScreen.HOME_SEARCH_ALL(methodName);
			//welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}
	
	@Test
	public void VM_ALLOCATION_LIST()
			throws InterruptedException, IOException, Exception {
		try {

			System.out
					.println("---------testToVerifyvmallocationlist()-------------");
			welcomeScreen.VM_ALLOCATION_LIST(methodName);
			//welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}
	
	/*@Test
	public void testToVerifyTheMoviesAndMusicAddToCart()
			throws InterruptedException, IOException, Exception {
		try {

			System.out
					.println("---------testToVerifyTheMoviesAndMusicAddToCart()-------------");
			welcomeScreen.MoviesnMusic(methodName);
			welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}


	@Test
	public void testToVerifyTheMobilePhonesAddToCart()
			throws InterruptedException, IOException, Exception {
		try {

			System.out
					.println("---------testToVerifyTheMobilePhonesAddToCart()-------------");
			welcomeScreen.MobilePhones(methodName);
			welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}

	@Test
	public void testToVerifyTheAccessoriesAddToCart()
			throws InterruptedException, IOException, Exception {
		try {
			System.out
					.println("---------testToVerifyTheAccessoriesAddToCart()-------------");
			welcomeScreen.Accessories(methodName);
			welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}

	@Test
	public void testToVerifyTheComputersAddToCart()
			throws InterruptedException, IOException, Exception {
		try {
			System.out
					.println("---------testToVerifyTheComputersAddToCart()-------------");
			welcomeScreen.Computers(methodName);
			welcomeScreen.billingInfo(methodName);
		} catch (Exception t) {
			t.printStackTrace();

		}
	}*/

	@AfterTest
	public static void tearDown() {
		welcomeScreen.closeBrowser();
}
	
}