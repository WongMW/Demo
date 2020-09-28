<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ShippingControl__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ShippingControl__c" %>
<%@ Register TagPrefix="uc1" TagName="CartGrid" Src="~/UserControls/Aptify_Product_Catalog/CartGrid.ascx" %>
<%@ Register TagPrefix="uc2" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--Nalini Issue#12578--%>
<table cellspacing="0" width="100%" border="0">
</table>
<table cellspacing="0" width="100%" style="border: 1px solid #e7d2b6;">
    <tr>
        <td class="tdbgcolorshipping">
            <strong><font size="2" color="white">Shipping Details</font></strong>
        </td>
    </tr>
    <tr>
        <!-- Changes made for Issue 5131, Changes made by Vijay Sitlani on 11-22-2007-->
        <%--Nalini issue 12578--%>
        <td style="padding-left: 5px;">
            <strong><font size="2">Shipping To: </font></strong>
        </td>
    </tr>
    <tr>
        <!-- Changes made for Issue 5131, Changes made by Vijay Sitlani on 11-22-2007-->
        <td valign="top" align="left" style="padding-left: 5px;">
            <font size="2">
                <uc2:NameAddressBlock ID="NameAddressBlock" runat="server"></uc2:NameAddressBlock>
                <br />
            </font>
        </td>
    </tr>
    <tr>
        <td class="tdchangebuttonshipping">
            <%-- <asp:HyperLink ID="lnkChangeAddress" runat="server">--%>
            <%--<img runat="server" id="imgChangeAddress" alt="Change Ship Address" src="" border="0" />--%>
            <%-- chnages by neha, Added css as per Shipping Details’s Change Address button, issue 16425,05/17/13--%>
            <asp:Button ID="lnkChangeAddress" runat="server" Text="Change Address" CssClass="submitBtn" />
        </td>
    </tr>

</table>
<cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
<cc2:User ID="User1" runat="server" />
