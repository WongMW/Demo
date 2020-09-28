<%@ Control Language="VB" AutoEventWireup="false" CodeFile=" ~/UserControls/Aptify_Custom__c/Chariot__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.Chariot__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>--%>
<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<%--<script src="../../Scripts/jquery-ui-1.8.9.js" type="text/javascript"></script>--%>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-ui-1.11.2.min.js" type="text/javascript"></script>
<script src="../../Scripts/expand.js" type="text/javascript"></script>
<script src="../../Scripts/JScript.min.js" type="text/javascript"></script>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>
<div id="divContent" runat="server">
    <div class="content-container clearfix">
        <div class="row-div clearfix">
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
