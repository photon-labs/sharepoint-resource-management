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
package com.photon.phresco.uiconstants;


import java.lang.reflect.Field;

public class UIConstants {
	

	private ReadXMLFile readXml;
	
	public String TELEVISION = "televisiontab";
	public String COMPUTERS = "computerstab";
	public String MOBILE = "mobiletab";
	/*Share point Ids */	
	public String AUDIO_DEVICES = "audioDevicestab";
	public String PROD1_DETAILS="prod1Details";
	public String DET_ADDTOCART="prod1DetailAddToCart";
	public String NEW="new";
	public String CHECKOUT="checkout";
	public String VMALLOCATION_NEW_SYSTEM_ID="vmallocation_new_systemname_id";
	public String VMALLOCATION_NEW_OPERATINGSYSTEM_ID="vmallocation_new_operatingsystem_id";
	public String SYSTEM_CONFIGURATION_ID="System_Configuration";
	public String VMALLOCATION_NEW_HARDWARETYPE_ID ="vmallocation_new_hardwaretype_id";
	public String VMALLOCATION_NEW_SERIALNUMBER_ID="vmallocation_new_serialnumber_id";
	public String VMALLOCATION_NEW_OWNER_ID="vmallocation_new_owner_id";
	public String VMALLOCATION_NEW_STATUS_ID="vmallocation_new_status_id";
	public String VMALLOCATION_NEW_OK_ID="vmallocation_new_ok_id";
	public String VMALLOCATION_NEW_PRIVATEIP_ID="vmallocation_new_privateip_id";
	public String CANCEL="cancel";
	//test case IV
    public String VIEWALL_SITE="listcreation_viewallsitecontents_link";
	public String CREATE="listcreation_Create_link";
	public String PLIST_DEF="listcreation_phrescolistdef_link";
	public String NAME="listcreation_listname_id";
	public String DESCRIPTION="listcreation_listdescription_id";
    public String CREATEBUTTON="listcreation_createbutton_id";
    //test case v
    public String LIST="listid";
    public String SAMPLE_CRT_DEMO="samplecreate";
    public String LISTNEW="listnew";
    public String TITLE="titlevalue";
    public String SINGLE_LINE_TEXT="singletextvalue";
    public String MUL_CHOICE="mulchoice";
    public String MUL_CHOICE_SELECT="mulcheslet";
    public String RADIO_CHOICE_SELECT="rdselect";
    public String NUMBERI="numberi";
    public String NUMBERII="number2";
    public String CUR_TUK="certuk";
    public String CUR_ITA="curita";
    public String DATE="date";
    //public String URLI="urli";
    public String OK="clickok";
    //TEST CASE VI
    
    public String MASTERPAGE="masterpage";
    public String SITE_ACTION="site_action";
    public String SITE_SETTINGS="site_settings";
    public String MASTER_PAGELINK="masterpage_link";
    public String RADIOBUTTON1="radio1";
    public String MASTER_DROPDOWN="master_dropdownlist";
    public String MASTER_DROPDOWN_SELECT="master_select";
    public String RADIOBUTTON2="radio2";
    public String MASTER_DROPDOWN1="mas_dropdown";
    public String MASTER_DROPDOWN_SELECT1="mast_drop_sclect";
    public String MASTER_OK="master_ok";
    public String PHRESCO="phresco_page";
    // OBJ for HOME_search
    public String HOMESEARCH_CLICK="homesearch_click";
   // public String HOMESEARCH_CLICK_SELECT="homesearch_click_select";
    public String SEARCH_SELECT="search_select";
   // public String SEARCH_SELECT_VALUE="search_select_value";
    public String SEARCH_HOME="search_home";
    public String HOMESEARCH_CLICK_THIS="click_this";
    //public String HOMESEARCH_CLICK_THIS_SITE="site_value";
    public String SEARCH_FIELD_CLICK="field_value";
    //public String HOMESEARCH_CLICK_POST="post_value";
    public String SEARCH_IMG="image";
    
    //TEST CASE 8
    public String THIS_SITE_VMALL="this_site_vmall";
    public String VM_SEARCH_FIELD="vm_search_field";
    //end of the Sharepoint
	public UIConstants() {
		try {
			readXml = new ReadXMLFile();
			readXml.loadUIConstants();
			Field[] arrayOfField1 = super.getClass().getFields();
			Field[] arrayOfField2 = arrayOfField1;
			int i = arrayOfField2.length;
			for (int j = 0; j < i; ++j) {
				Field localField = arrayOfField2[j];
				Object localObject = localField.get(this);
				if (localObject instanceof String)
					localField
							.set(this, readXml.getValue((String) localObject));

			}
		} catch (Exception localException) {
			throw new RuntimeException("Loading "
					+ super.getClass().getSimpleName() + " failed",
					localException);
		}
	}
}
