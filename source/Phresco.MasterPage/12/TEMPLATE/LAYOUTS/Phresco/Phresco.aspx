<%@ Page language="C#" MasterPageFile="~masterurl/Phresco.master"    Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage,Microsoft.SharePoint,Version=12.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" meta:webpartpageexpansion="full" meta:progid="SharePoint.WebPartPage.Document" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Import Namespace="Microsoft.SharePoint" %> <%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
	<SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="<%$Resources:wss,multipages_homelink_text%>" EncodeMethod="HtmlEncode"/> 
	- <SharePoint:ProjectProperty ID="ProjectProperty1" Property="Title" runat="server"/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageImage" runat="server"><IMG SRC="/_layouts/images/blank.gif" width=1 height=1 alt=""></asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
			<label class="ms-hidden">
			<SharePoint:ProjectProperty ID="ProjectProperty2" Property="Title" runat="server"/></label>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleBreadcrumb" runat="server"/>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleAreaClass" runat="server">
<style type="text/css">
TD.ms-titleareaframe, .ms-pagetitleareaframe {
	height: 10px;
}
Div.ms-titleareaframe {
	height: 100%;
}
.ms-pagetitleareaframe table {
	background: none;
	height: 10px;
}
</style>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderAdditionalPageHead" runat="server">
	<META Name="CollaborationServer" Content="SharePoint Team Web Site">
	<script type="text/javascript">
	    var navBarHelpOverrideKey = "wssmain";
	</script>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderSearchArea" runat="server">
	<SharePoint:DelegateControl ID="DelegateControl1" runat="server"
		ControlId="SmallSearchInputBox"/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderLeftActions" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageDescription" runat="server"/>
<asp:Content ContentPlaceHolderId="PlaceHolderBodyAreaClass" runat="server">
<style type="text/css">
.ms-bodyareaframe {
	padding: 0px;
}
</style>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
</asp:Content>