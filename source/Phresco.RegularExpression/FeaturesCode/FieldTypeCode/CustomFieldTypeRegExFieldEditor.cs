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