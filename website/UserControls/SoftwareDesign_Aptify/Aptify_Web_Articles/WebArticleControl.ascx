<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Web_Articles/WebArticleControl.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.WebArticleControl"  %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %>

<div class="content-container clearfix">
    <asp:Label id="lblWebArticleName" runat="server" Font-Bold="True" Font-Size="Medium">Article Name</asp:Label>
    <br/>
    <div id="author" runat="server"><asp:Label id="lblAuthor" runat="server" Font-Size="Small">lblAuthor</asp:Label></div>
    <asp:Label id="lblDateWritten" runat="server" Font-Size="X-Small">lblDateWritten</asp:Label>
    <p>
    <asp:Label id="lblWebArticle" runat="server">lblWebArticle</asp:Label></p>
    <cc1:WebUserActivity WebModule="General" id="WebUserActivity1" runat="server"></cc1:WebUserActivity>
</div>
