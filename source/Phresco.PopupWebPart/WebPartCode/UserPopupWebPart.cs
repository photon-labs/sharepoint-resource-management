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
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

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
    [Guid("2de6a0f2-2196-4ac0-831a-281b5918def3")]
    public class UserPopupWebPart : Microsoft.SharePoint.WebPartPages.WebPart
    {
        private bool _error = false;


        public UserPopupWebPart()
        {
            this.ExportMode = WebPartExportMode.All;
        }

        /// <summary>
        /// Create all your controls here for rendering.
        /// Try to avoid using the RenderWebPart() method.
        /// </summary>
        protected override void CreateChildControls()
        {
            if (!_error)
            {
                try
                {

                    base.CreateChildControls();

                    // Your code here...

                    HtmlLink cssLink = new HtmlLink();
                    cssLink.Href = "/_layouts/yarracity/SPPopupWithPreferences/SPPopupWithPreferences.css";
                    cssLink.Attributes.Add("rel", "stylesheet");
                    cssLink.Attributes.Add("type", "text/css");

                    LiteralControl jqueryScript = new LiteralControl("<script language=\"javascript\" src=\"/_layouts/yarracity/SPPopupWithPreferences/jquery-1.6.1.min.js\"></script>");
                    LiteralControl featureScript = new LiteralControl("<script language=\"javascript\" src=\"/_layouts/yarracity/SPPopupWithPreferences/SPPopupWithPreferences.js\"></script>");
                    LiteralControl changeLinksScript = new LiteralControl("<script language=\"javascript\" src=\"/_layouts/yarracity/SPPopupWithPreferences/ChangeSearchLinks.js\"></script>");

                    this.Page.Header.Controls.Add(cssLink);
                    this.Page.Header.Controls.Add(jqueryScript);
                    this.Page.Header.Controls.Add(featureScript);
                    this.Page.Header.Controls.Add(changeLinksScript);

                    string featureLayoutsFolder = SPUtility.GetGenericSetupPath(@"TEMPLATE\LAYOUTS\yarracity\SPPopupWithPreferences");

                    StreamReader htmlFileReader = File.OpenText(featureLayoutsFolder + @"\SPPopupWithPreferences.htm");
                    string htmlFileContents = htmlFileReader.ReadToEnd();
                    htmlFileReader.Close();
                    

                    this.Controls.Add(new LiteralControl(htmlFileContents));
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        /// <summary>
        /// Ensures that the CreateChildControls() is called before events.
        /// Use CreateChildControls() to create your controls.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (!_error)
            {
                try
                {
                    base.OnLoad(e);
                    this.EnsureChildControls();

                    // Your code here...
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        /// <summary>
        /// Clear all child controls and add an error message for display.
        /// </summary>
        /// <param name="ex"></param>
        private void HandleException(Exception ex)
        {
            this._error = true;
            this.Controls.Clear();
            this.Controls.Add(new LiteralControl("Error occurred while rendering the User Popup Web Part: [" + ex.Message + "]"));
        }
    }
}
