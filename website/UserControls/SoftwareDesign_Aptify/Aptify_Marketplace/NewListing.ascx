<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Marketplace/NewListing.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.MarketPlace.NewListing"%>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %> 
<%@ Register TagPrefix="uc1" TagName="ListingEdit" Src="ListingEdit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopicCodeViewer" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/TopicCodeViewer.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc5" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="uc2" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>

<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <uc1:ListingEdit ID="ListingEdit" runat="server"></uc1:ListingEdit>
            </td>
        </tr>
    </table>
    <fieldset id="fieldsetListing" runat="server" style="border: 1px solid gray; width: 700px;">
        <legend><b>Select Topic Codes:</b></legend>
        <table style="width:100%;">
            <tr>
                <td>
                    <uc1:TopicCodeViewer ID="TopicCodeViewer" runat="server"></uc1:TopicCodeViewer>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="border: 1px solid gray; width: 700px;">
        <legend><b>Enter Payment Information:</b></legend>
        <table>
            <tr>
                <td>
               <%-- 'Anil B change for 10737 on 13/03/2013
                     'Set Credit Card ID to load property form Navigation Config--%>
                    <uc2:CreditCard ID="CreditCard" runat="server"></uc2:CreditCard>
                </td>
            </tr>
        </table>
        </fieldset>
        <table>
            <tr>
                <td>
                    <%--Nalini Issue 12734--%>
                    <asp:Button ID="cmdSubmit" runat="server" Text="Submit New Listing" CssClass="submitBtn"></asp:Button>
                    &nbsp&nbsp
                    <asp:Label ID="lblSubmitError" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <cc1:WebUserActivity ID="WebUserActivity1" runat="server" WebModule="MarketPlace" />
        <cc3:User ID="User1" runat="server" />
        <cc5:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
</div>
