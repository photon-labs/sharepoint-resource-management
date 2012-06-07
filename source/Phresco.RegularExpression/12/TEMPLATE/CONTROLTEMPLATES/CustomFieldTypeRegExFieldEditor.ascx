<%@ Control Language="C#" Inherits="Phresco.SharePoint.CFT_RegEx.CustomFieldTypeRegExFieldEditor, Phresco.SharePoint.CFT_RegEx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a880ba84c9912337"    compilationMode="Always" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>

<wssuc:InputFormControl runat="server" LabelText="Regular Expression">
	<Template_Control>
		<asp:TextBox id="txtRegularExpression" runat="server"></asp:TextBox>
	</Template_Control>
</wssuc:InputFormControl>

<wssuc:InputFormControl runat="server" LabelText="Error Message">
	<Template_Control>
		<asp:TextBox id="txtErrorMessage" runat="server"></asp:TextBox>
	</Template_Control>
</wssuc:InputFormControl>

