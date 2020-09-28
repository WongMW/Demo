<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Marketplace/EditListing.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.MarketPlace.EditListing"  %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="PageSecurity" %>
<%@ Register TagPrefix="uc1" TagName="ListingDisplay" Src="ListingDisplay.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc2" TagName="TopicCodeViewer" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/TopicCodeViewer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ListingEdit" Src="ListingEdit.ascx" %>

<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>    
                <uc1:listingedit id="ListingEdit" runat="server"></uc1:listingedit>
            </td>
        </tr>
    </table>
    <fieldset style="border: 1px solid gray; width: 700px;">
        <legend>Select Topic Codes:</legend>
        <table  style="width:100%;">
        <tr>
            <td>
                    <uc2:TopicCodeViewer ID="TopicCodeViewer" runat="server"></uc2:TopicCodeViewer>
	                <asp:HyperLink runat="server" ID="lnkTopicCodes"></asp:HyperLink >
	          </td>
        </tr>
        </table>
    </fieldset>
    <table>
        <tr>
    <td style="width:1px;">
    &nbsp;
    </td>
    </tr>
        <tr>
            <td>
                <asp:Button ID="cmdSubmit" runat="server" Text="Submit Changes" CssClass="submitBtn"></asp:Button>
            </td>
        </tr>
    </table>
    <cc3:User ID="User1" runat="server" />
    <cc1:WebUserActivity ID="WebUserActivity1" runat="server" WebModule="MarketPlace" />
</div>
