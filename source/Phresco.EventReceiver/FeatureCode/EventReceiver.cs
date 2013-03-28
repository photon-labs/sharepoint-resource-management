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
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Collections.Specialized;

namespace Phresco.EventReceiver
{
    class EventReceiver : SPItemEventReceiver
    {
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            SPWeb web = properties.OpenWeb();
            string _listName = "Resource_Allocation";
            if (properties.ListTitle == "Resource_Allocation")
            {
                try
                {
                    if (SPUtility.IsEmailServerSet(web))
                    {
                        string body = GetParameter("UserContent", properties);
                        StringDictionary headers = new StringDictionary();
                        headers.Add("To", GetSPUserEmailID(_listName, properties, "Owner"));                        
                        headers.Add("Subject", GetParameter("UserSubject", properties));
                        headers.Add("Content-Type", "text/html; charset=\"UTF-8\"");
                        SPUtility.SendEmail(web, headers, body);

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
        }

        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
            SPWeb web = properties.OpenWeb();
            if (properties.ListTitle == "Resource_Allocation")
            {
                try
                {
                    if (SPUtility.IsEmailServerSet(web))
                    {
                        string body = GetParameter("SystemsContent", properties);
                        StringDictionary headers = new StringDictionary();
                        headers.Add("To", GetParameter("SystemsEmail", properties));
                        headers.Add("Subject", GetParameter("SystemsSubject", properties));
                        headers.Add("Content-Type", "text/html; charset=\"UTF-8\"");
                        SPUtility.SendEmail(web, headers, body);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);
        }
        /// <summary>
        /// Public Methods
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="properties"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string GetSPUserEmailID(string listName, SPItemEventProperties properties, string fieldName)
        {
            SPFieldUser userField = (SPFieldUser)properties.OpenWeb().Lists[listName].Fields.GetField(fieldName);
            SPFieldUserValue fieldValue = (SPFieldUserValue)userField.GetFieldValue(properties.ListItem[fieldName] + "");
            SPUser user = fieldValue.User;
            return user.Email;
        }
        public static string GetParameter(string parameter, SPItemEventProperties properties)
        {
            string outValue = string.Empty;
            SPWeb web = properties.OpenWeb();
            SPList list = web.Lists["PhrescoParams"];
            SPQuery query = new SPQuery();
            query.Query = string.Format("<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + parameter + "</Value></Eq></Where>");
            var items = list.GetItems(query);
            if (items != null && items.Count > 0)
            {
                outValue = items[0][list.Fields["Value"].InternalName].ToString();
            }

            return outValue;
        }


    }
}
