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

public class SharepointData {

	private ReadXMLFile readXml;

	
	public String COMMENTS_VALUE="billInfoCommentsValue";
	public String EMAIL_VALUE="billInfoEmailValue";
	public String NAMEONCARD_VALUE="cardInfoNameOnCardValue";
	public String SECURITYNUMBER_VALUE="cardInfoSecurityNumberValue";
	public String CARDNUMBER_VALUE="cardInfoCardNumberValue";
	public String PHONENUMBER_VALUE="billInfoPhoneNumberValue";
	public String POSTALCODE_VALUE="billInfoPostCodeValue";
	public String STATE_VALUE="billInfoStateValue";
	public String CITY_VALUE="billInfoCityValue";
	public String ADDRESS2_VALUE="billInfoAddress2Value";
	public String ADDRESS1_VALUE="billInfoAddress1Value";
	public String COMPANY_VALUE="billInfoCompanyValue";
	public String LASTNAME_VALUE="billInfoLastNameValue";
	public String FIRSTNAME_VALUE="billInfoFirstNameValue";
	public String TITLE_NAME="titlename";
	public String VMALLOCATION_NEW_SYSTEM_ID_VALUE="vmallocation_new_systemname_value";
	public String VMALLOCATION_NEW_OPERATINGSYSTEM_ID_VALUE="vmallocation_new_operatingsystem_value";
	public String SYSTEM_CONFIGURATION_ID_VALUE="System_Configuration_value";
	public String VMALLOCATION_NEW_HARDWARETYPE_ID_VALUE="vmallocation_new_hardwaretype_id_value";
	public String VMALLOCATION_NEW_SERIALNUMBER_VALUE="vmallocation_new_serialnumber_value";
	public String VMALLOCATION_NEW_OWNER_VALUE="vmallocation_new_owner_value";
	public String   VMALLOCATION_NEW_STATUS_VALUE="vmallocation_new_status_value";
	public String VMALLOCATION_NEW_PRIVATEIP_ID_VALUE="vmallocation_new_private_value";
    public String VMALLOCATION_NEW_PRIVATEIP_ID_VALUE_INVALID="invalid_value";
    //test case IV
    public String NAME_VALUE="listcreation_listname_value";
    public String DEC_VALUE="listcreation_listdescription_value";
   //test case V
    public String TITLE_VALUE="titlevalue";
    public String SINGLE_LINE_TEXT_VALUE="singletextvalues"; 
    public String NUMBERI_VALUE="numberival";
    public String NUMBERII_VALUE="numberiival";
    public String CUR_TUK_VALUE="curtukval";
    public String CUR_ITA_VALUE="curitaval";
    public String DATE_VALUE="dateval";
    //public String URLI_VALUE="urlival";
    public String ALL_SITES="allsites";
    public String SEARCH_SELECT_TEXT="search_select_value";
    public String HOMESEARCH_CLICK_POST_VALUE="homesearch_click_post";
    public String VM_SEARCH_FIELD_VALUE="vm_search_field_value";

	public SharepointData() {
		try {
			readXml = new ReadXMLFile();
			readXml.loadYuiSharepointData();
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
