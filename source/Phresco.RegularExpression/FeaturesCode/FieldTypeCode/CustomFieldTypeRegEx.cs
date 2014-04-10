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
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Phresco.SharePoint.CFT_RegEx
{
    public class CustomFieldTypeRegEx : SPFieldText
    {
        private static string[] CustomPropertyNames = new string[] { "propRegularExpression", "propErrorMessage" };

        public CustomFieldTypeRegEx(SPFieldCollection fields, string fieldName)
            : base(fields, fieldName)
        {
            InitProperties();
        }

        public CustomFieldTypeRegEx(SPFieldCollection fields, string typeName, string displayName)
            : base(fields, typeName, displayName)
        {
            InitProperties();
        }

        #region Property storage and bug workarounds - do not edit

        /// <summary>
        /// Indicates that the field is being created rather than edited. This is necessary to 
        /// work around some bugs in field creation.
        /// </summary>
        public bool IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }
        private bool _IsNew = false;

        /// <summary>
        /// Backing fields for custom properties. Using a dictionary to make it easier to abstract
        /// details of working around SharePoint bugs.
        /// </summary>
        private Dictionary<string, string> CustomProperties = new Dictionary<string, string>();

        /// <summary>
        /// Static store to transfer custom properties between instances. This is needed to allow
        /// correct saving of custom properties when a field is created - the custom property 
        /// implementation is not used by any out of box SharePoint features so is really buggy.
        /// </summary>
        private static Dictionary<string, string> CustomPropertiesForNewFields = new Dictionary<string, string>();

        /// <summary>
        /// Initialise backing fields from base property store
        /// </summary>
        private void InitProperties()
        {
            foreach (string propertyName in CustomPropertyNames)
            {
                CustomProperties[propertyName] = base.GetCustomProperty(propertyName) + "";
            }
        }

        /// <summary>
        /// Take properties from either the backing fields or the static store and 
        /// put them in the base property store
        /// </summary>
        private void SaveProperties()
        {
            foreach (string propertyName in CustomPropertyNames)
            {
                base.SetCustomProperty(propertyName, GetCustomProperty(propertyName));
            }
        }

        /// <summary>
        /// Get an identifier for the field being added/edited that will be unique even if
        /// another user is editing a property of the same name.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private string GetCacheKey(string propertyName)
        {
            return SPContext.Current.GetHashCode() + "_" + (ParentList == null ? "SITE" : ParentList.ID.ToString()) + "_" + propertyName;
        }

        /// <summary>
        /// Replace the buggy base implementation of SetCustomProperty
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        new public void SetCustomProperty(string propertyName, object propertyValue)
        {
            if (IsNew)
            {
                // field is being added - need to put property in cache
                CustomPropertiesForNewFields[GetCacheKey(propertyName)] = propertyValue + "";
            }

            CustomProperties[propertyName] = propertyValue + "";
        }

        /// <summary>
        /// Replace the buggy base implementation of GetCustomProperty
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        new public object GetCustomProperty(string propertyName)
        {
            if (!IsNew && CustomPropertiesForNewFields.ContainsKey(GetCacheKey(propertyName)))
            {
                string s = CustomPropertiesForNewFields[GetCacheKey(propertyName)];
                CustomPropertiesForNewFields.Remove(GetCacheKey(propertyName));
                CustomProperties[propertyName] = s;
                return s;
            }
            else
            {
                return CustomProperties[propertyName];
            }
        }

        /// <summary>
        /// Called when a field is created. Without this, update is not called and custom properties
        /// are not saved.
        /// </summary>
        /// <param name="op"></param>
        public override void OnAdded(SPAddFieldOptions op)
        {
            base.OnAdded(op);
            Update();
        }

        #endregion


        //public override BaseFieldControl FieldRenderingControl
        //{
        //    get
        //    {
        //        BaseFieldControl fieldControl = new CustomFieldTypeRegExControl(this);

        //        fieldControl.FieldName = InternalName;

        //        return fieldControl;
        //    }
        //}


        //converts the field type value into a validated serialised string.
        public override string GetValidatedString(object value)
        {
            string textValue = value.ToString();


            //Only compare RegEx if a value is present
            if (textValue.Length > 0)
            {
                //setup regex based on custom property
                Regex reg = new Regex(GetCustomProperty("propRegularExpression").ToString(), RegexOptions.IgnoreCase);


                //if the value entered does not match
                if (!reg.IsMatch(textValue))
                {
                    throw new SPFieldValidationException(GetCustomProperty("propErrorMessage").ToString());
                }
                else
                {
                    return textValue;
                }
            }
            else
            {
                return textValue;
            }
        }

        public override void Update()
        {
            SaveProperties();
            base.Update();
        }

        public string propRegularExpression
        {
            get { return this.GetCustomProperty("propRegularExpression") + ""; }
            set { this.SetCustomProperty("propRegularExpression", value); }
        }

        public string propErrorMessage
        {
            get { return this.GetCustomProperty("propErrorMessage") + ""; }
            set { this.SetCustomProperty("propErrorMessage", value); }
        }

    }

}
