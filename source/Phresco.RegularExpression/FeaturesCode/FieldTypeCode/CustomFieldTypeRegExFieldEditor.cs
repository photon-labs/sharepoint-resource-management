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
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Phresco.SharePoint.CFT_RegEx
{
    public class CustomFieldTypeRegExFieldEditor : UserControl, IFieldEditor
    {
        // Fields
        protected TextBox txtRegularExpression;
        protected TextBox txtErrorMessage;
        private CustomFieldTypeRegEx fldCustomFieldTypeRegEx;
        //protected Label LabelRegularExpression;

        public void InitializeWithField(SPField field)
        {
            this.fldCustomFieldTypeRegEx = field as CustomFieldTypeRegEx;

            if (this.Page.IsPostBack)
            {
                return;
            }

            txtRegularExpression.Text = "";
            txtErrorMessage.Text = "";


            if (field != null)
            {
                txtRegularExpression.Text = fldCustomFieldTypeRegEx.propRegularExpression;
                txtErrorMessage.Text = fldCustomFieldTypeRegEx.propErrorMessage;
                //this.txtRegularExpression.Visible = true;
            }
            else
            {
                txtRegularExpression.Text = "";
                txtRegularExpression.Text = "";
                //this.txtRegularExpression.Visible = true;
            }
        }

        public void OnSaveChange(SPField field, bool bNewField)
        {
            CustomFieldTypeRegEx lookup = (CustomFieldTypeRegEx)field;

            lookup.IsNew = bNewField;
            lookup.propRegularExpression = this.txtRegularExpression.Text;
            lookup.propErrorMessage = this.txtErrorMessage.Text;

        }


        // Properties
        public bool DisplayAsNewSection
        {
            get
            {
                return false;
            }
        }



    }

}