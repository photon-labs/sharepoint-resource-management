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
using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Microsoft.SharePoint;

/***********************************************************************
   Copyright 2011 Mark Powney

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 **************************************************************************/

namespace SPPopupWithPreferences
{
    [WebService(Namespace = "http://intranet.yarra.int/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UpdateUserPreference : System.Web.Services.WebService
    {

        public UpdateUserPreference()
        {

        }
        [WebMethod]
        public string ReturnPreference(string listName, string fieldName)
        {
            string operation = "";
            string preferenceValue = "";

            try
            {

                operation = "determining login name";
                string currentUsername = SPContext.Current.Web.CurrentUser.Name;

                operation = "finding the root web";
                SPWeb rootWeb = SPContext.Current.Site.RootWeb;

                operation = "finding list [" + listName + "]";
                SPList preferenceList = rootWeb.Lists[listName];

                SPQuery findThePreferenceQuery = new SPQuery();
                findThePreferenceQuery.Query = "<Where><Contains><FieldRef Name=\"Author\"/><Value Type=\"Text\">" + currentUsername + "</Value></Contains></Where>";

                operation = "invoking query [" + findThePreferenceQuery.Query + "] against list [" + listName + "]";
                SPListItemCollection findThePreferenceItems = preferenceList.GetItems(findThePreferenceQuery);

                if (findThePreferenceItems.Count > 0)
                {
                    preferenceValue = findThePreferenceItems[0][fieldName].ToString();
                }

                rootWeb.Dispose();

            }
            catch (Exception ex)
            {
            }

            return preferenceValue;
        }

        [WebMethod]
        public bool UpdatePreference(string listName, string fieldName, string preferenceValue)
        {

            string operation = "";

            try
            {

                bool success = false;

                operation = "determining login name";
                string currentUsername = SPContext.Current.Web.CurrentUser.Name;

                operation = "finding the root web";
                SPWeb rootWeb = SPContext.Current.Site.RootWeb;

                operation = "finding list [" + listName + "]";
                SPList preferenceList = rootWeb.Lists[listName];

                SPQuery findThePreferenceQuery = new SPQuery();
                findThePreferenceQuery.Query = "<Where><Contains><FieldRef Name=\"Author\"/><Value Type=\"Text\">" + currentUsername + "</Value></Contains></Where>";

                operation = "invoking query [" + findThePreferenceQuery.Query + "] against list [" + listName + "]";
                SPListItemCollection findThePreferenceItems = preferenceList.GetItems(findThePreferenceQuery);

                if (findThePreferenceItems.Count == 0)
                {
                    operation = "adding new preference item";
                    rootWeb.AllowUnsafeUpdates = true;
                    SPListItem newPreferenceItem = preferenceList.Items.Add();
                    newPreferenceItem["Title"] = currentUsername;
                    newPreferenceItem[fieldName] = preferenceValue;
                    newPreferenceItem.Update();
                    rootWeb.AllowUnsafeUpdates = false;
                    success = true;
                }
                else
                {

                    operation = "updating all existing preference items (count: " + findThePreferenceItems.Count + ")";
                    rootWeb.AllowUnsafeUpdates = true;
                    foreach (SPListItem updatePreferenceItem in findThePreferenceItems)
                    {
                        updatePreferenceItem[fieldName] = preferenceValue;
                        updatePreferenceItem.Update();
                    }
                    rootWeb.AllowUnsafeUpdates = false;
                    success = true;
                }

                rootWeb.Dispose();

                return success;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Problem when " + operation + ": [" + ex.Message + "].");
            }

        }

    }

}