<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Marketplace/ViewListing.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.MarketPlace.ViewListing"  %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %>
<%@ Register TagPrefix="uc1" TagName="ListingDisplay" Src="ListingDisplay.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix">
    <table runat="server" id="tblMain" class="data-form">
        <tr>
	        <td>
            <uc1:ListingDisplay id="ListingDisplay" runat="server"></uc1:ListingDisplay>
           </td>
        </tr>
    </table>
    <cc1:WebUserActivity  WebModule="Marketplace" id="WebUserActivity1" runat="server" /> 
    <cc3:User id="User1" runat="server" />
</div>