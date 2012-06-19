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
﻿using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.SharePoint;

namespace Phresco.XMLtoList
{
    public class Setup : SPFeatureReceiver
    {
        string fieldname = string.Empty;
        string fieldtype = string.Empty;
        string listName = string.Empty;
        string descName = string.Empty;
        string fielddesc = string.Empty;
        string maxCount = string.Empty;
        string required = string.Empty;
        string defaultValue = string.Empty;
        string textFormat = string.Empty;
        string NoOfLines = string.Empty;
        string displayChoicesUsing = string.Empty;
        string lookupList = string.Empty;
        string lookupfield = string.Empty;

        /// <summary>
        /// Get the file from '12 Hive' Features folder and Deserialize to a class.
        /// </summary>
        /// <returns></returns>
        private Lists GetListsInfo(SPFeatureReceiverProperties properties)
        {
            Lists lists = new Lists();
            TextReader textreader = new StreamReader(properties.Feature.Definition.RootDirectory + @"\" + Constants.CONFIG_PATH);
            XmlSerializer xmlSerial = new XmlSerializer(typeof(Lists));
            lists = (Lists)xmlSerial.Deserialize(textreader);
            textreader.Close();
            textreader.Dispose();
            return lists;
        }

        /// <summary>
        /// Check the existance of the List.
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listName"></param>
        /// <returns></returns>
        private bool CheckList(SPWeb web, string listName)
        {
            try
            {
                SPList lsttemp = web.Lists[listName];
                return false;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Clears all the flags
        /// </summary>
        private void ClearFlags()
        {
            fielddesc = string.Empty;
            fieldtype = string.Empty;
            fieldname = string.Empty;
            maxCount = string.Empty;
            required = string.Empty;
            defaultValue = string.Empty;
            NoOfLines = string.Empty;
            textFormat = string.Empty;
            displayChoicesUsing = string.Empty;
            lookupList = string.Empty;
            lookupfield = string.Empty;
        }

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {

            SPWeb web = (SPWeb)properties.Feature.Parent;
            SPSite site = web.Site;
            web.AllowUnsafeUpdates = true;
            Lists listRoot = GetListsInfo(properties);
            foreach (List listInfo in listRoot.Items)
            {
                SPList tempList = null;
                if (listInfo.name != null)
                    listName = listInfo.name.Trim();
                else
                    throw new Exception(Constants.LIST_NAME_ERROR);
                descName = listInfo.description;
                if (CheckList(web, listName))
                {
                    tempList = web.Lists[web.Lists.Add(listName, descName, SPListTemplateType.GenericList)];
                }
                else
                {
                    tempList = web.Lists[listName];
                }
                if (tempList != null)
                {
                    foreach (Field fields in listInfo.Fields)
                    {
                        SPFieldType fldType = SPFieldType.Text;
                        ClearFlags();

                        if (fields.name != null)
                            fieldname = fields.name.Trim();
                        else
                            throw new Exception(Constants.FIELD_NAME_ERROR + listName);

                        if (fields.datatype != null)
                            fieldtype = fields.datatype.Trim();

                        if (fields.required != null)
                            required = fields.required.Trim();

                        if (fields.defaultvalue != null)
                            defaultValue = fields.defaultvalue.Trim();

                        if (fields.textformat != null)
                            textFormat = fields.textformat.Trim();

                        if (fields.displaychoicesusing != null)
                            displayChoicesUsing = fields.displaychoicesusing.Trim();

                        if (fields.lookuplist != null)
                            lookupList = fields.lookuplist.Trim();

                        if (fields.lookupfield != null)
                            lookupfield = fields.lookupfield.Trim();

                        switch (fieldtype.ToLower())
                        {
                            case Constants.SINGLE_LINE_TEXT_TYPE:
                                fldType = SPFieldType.Text;
                                break;
                            case Constants.MULTIPLE_LINE_TEXT_TYPE:
                                fldType = SPFieldType.Note;
                                break;
                            case Constants.DATETIME_TYPE:
                                fldType = SPFieldType.DateTime;
                                break;
                            case Constants.NUMBER_TYPE:
                                fldType = SPFieldType.Number;
                                break;
                            case Constants.CURRENCY_TYPE:
                                fldType = SPFieldType.Currency;
                                break;
                            case Constants.YESNO_TYPE:
                                fldType = SPFieldType.Boolean;
                                break;
                            case Constants.PERSON_GROUP_TYPE:
                                fldType = SPFieldType.User;
                                break;
                            case Constants.HYPERLINK_PICTURE_TYPE:
                                fldType = SPFieldType.URL;
                                break;
                            case Constants.CHOICE_TYPE:
                                if (displayChoicesUsing.ToLower().Trim().Equals(Constants.MULTICHECKBOX))
                                    fldType = SPFieldType.MultiChoice;
                                else
                                    fldType = SPFieldType.Choice;
                                break;
                            case Constants.LOOKUP_TYPE:
                                fldType = SPFieldType.Lookup;
                                break;
                        }

                        SPField field = tempList.Fields.CreateNewField(fldType.ToString(), fieldname);
                        field.DefaultValue = defaultValue;
                        field.Description = fielddesc;
                        field.Required = (required.ToLower().Trim().Equals(Constants.YES) ? true : false);

                        if (!tempList.Fields.ContainsField(fieldname))
                            tempList.Fields.Add(field);

                        if ((tempList.Fields[fieldname] as SPFieldText) != null)
                        {
                            int maxLength;
                            int.TryParse(maxCount, out maxLength);
                            var fldText = tempList.Fields[fieldname] as SPFieldText;

                            if (fldText != null && maxLength > 0)
                            {
                                fldText.MaxLength = maxLength;
                                fldText.Update();
                                tempList.Update();
                            }

                            continue;
                        }

                        if ((tempList.Fields[fieldname] as SPFieldMultiLineText) != null)
                        {
                            int noOfLines;
                            int.TryParse(NoOfLines, out noOfLines);
                            SPFieldMultiLineText fldMultiText = tempList.Fields[fieldname] as SPFieldMultiLineText;
                            fldMultiText.NumberOfLines = (noOfLines > 0) ? noOfLines : fldMultiText.NumberOfLines;
                            fldMultiText.RichText = (textFormat.ToLower().Trim().Equals(Constants.RICHTEXT)) ? true : false;
                            fldMultiText.Update();
                            tempList.Update();

                            continue;
                        }

                        if ((tempList.Fields[fieldname] as SPFieldBoolean) != null)
                        {
                            SPFieldBoolean fldBoolean = (tempList.Fields[fieldname]) as SPFieldBoolean;
                            // useful in future
                            tempList.Update();

                            continue;
                        }

                        if ((tempList.Fields[fieldname] as SPFieldUser) != null)
                        {
                            SPFieldUser fldUser = tempList.Fields[fieldname] as SPFieldUser;
                            // useful in future
                            tempList.Update();

                            continue;
                        }

                        if (tempList.Fields[fieldname] as SPFieldMultiChoice != null)
                        {
                            SPFieldMultiChoice fldMultiChoice = tempList.Fields[fieldname] as SPFieldMultiChoice;

                            foreach (Choice choiceNodes in fields.Choices)
                            {
                                fldMultiChoice.Choices.Add(choiceNodes.Value);
                            }
                            fldMultiChoice.Update();
                            tempList.Update();

                            continue;
                        }

                        if ((tempList.Fields[fieldname] as SPFieldChoice) != null)
                        {
                            SPFieldChoice fldChoice = tempList.Fields[fieldname] as SPFieldChoice;

                            if (displayChoicesUsing.ToLower().Trim().Equals(Constants.DROPDOWNMENU))
                                fldChoice.EditFormat = SPChoiceFormatType.Dropdown;

                            if (displayChoicesUsing.ToLower().Trim().Equals(Constants.RADIOBUTTONS))
                                fldChoice.EditFormat = SPChoiceFormatType.RadioButtons;

                            // Add the choices
                            foreach (Choice choiceNodes in fields.Choices)
                            {
                                fldChoice.Choices.Add(choiceNodes.Value);
                            }
                            fldChoice.Update();
                            tempList.Update();

                            continue;
                        }

                        if ((tempList.Fields[fieldname] as SPFieldLookup) != null)
                        {
                            SPFieldLookup fldLookup = tempList.Fields[fieldname] as SPFieldLookup;
                            SPList parentList = web.Lists[lookupList];
                            fldLookup.LookupList = parentList.ID.ToString();
                            fldLookup.LookupField = parentList.Fields[lookupfield].InternalName;
                            fldLookup.Update();
                            tempList.Update();

                            continue;
                        }
                    }
                }
            }
            web.Update();
            web.AllowUnsafeUpdates = true;
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {

        }

        public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        {

        }

        public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        {

        }
    }
}
