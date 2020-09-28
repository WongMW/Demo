<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.NavBar"
    CodeFile="NavBar.ascx.vb" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<rad:RadMenu ID="RadMenu1" runat="server" EnableRoundedCorners="true" EnableShadows="true" OnItemClick="RadMenu1_click">
</rad:RadMenu>


    <cc1:User ID="User1" runat="server" /><%--'Added By Sandeep for Issue 15051 on 13/03/2013--%>
<%--<div class="contentbread">
    <telerik:RadSiteMap ID="BreadCrumbSiteMap" runat="server" DataTextField="Text" DataNavigateUrlField="NavigateUrl"
        >
        <DefaultLevelSettings ListLayout-RepeatDirection="Horizontal" SeparatorText="/" Layout="Flow" />
    </telerik:RadSiteMap>
   
</div>--%>
