<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/SaveCart.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.SaveCartControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<style type="text/css">
    .style1 {
        /*height: 61px;*/
    }
</style>
<table id="tblMain" class="cai-table marg-btm-20px" runat="server" width="100%">
    <tr id="trInfo" runat="server">
        <td colspan="2" style="text-align:left;">
            <p style="margin-bottom:18px"><asp:Label ID="lblInfo" runat="server">Give your shopping cart a name and description. Once saved this will be available in your My Account area where you can easily access it for your future convenience.</asp:Label></p>
        </td>
    </tr>
    <tr id="trResult" runat="server">
        <td colspan="2">
            <asp:Label ID="lblResult" runat="server" Visible="False" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr id="trName" runat="server">
        <td valign="top" align="left" width="25%" style="text-align:left;">
            <asp:Label ID="lblName" runat="server"><b>Cart name:</b></asp:Label>
        </td>
        <td width="75%">
            <asp:TextBox ID="txtName" runat="server" TextMode="SingleLine" Width="100%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name Is Required"
                ControlToValidate="txtName" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
    </tr>
<%--    <tr style="width: 1px;">
        <td style="width: 1px;">&nbsp;
        </td>

    </tr>--%>
    <tr id="trDescription" runat="server">
        <td valign="top" align="left" class="style1" style="text-align:left;">
            <asp:Label ID="lblDescription" runat="server"><b>Description:</b>
            </asp:Label>
        </td>
        <td valign="top" class="style1">
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="100%"
                Height="70px" Style="margin-bottom: 10px;"     font-family="Arial,Verdana,Sans-serif"></asp:TextBox>
        </td>
    </tr>
    <tr id="trSave" runat="server">
        <td></td>
        <td style="text-align:right;">
            <asp:Button ID="cmdCancel" runat="server" CssClass="submitBtn cai-btn cai-btn-red-inverse" Text="Cancel" CausesValidation="false" />
            <asp:Button ID="cmdSaveCurrent" runat="server" CssClass="submitBtn cai-btn cai-btn-red-inverse" Text="Update Current Cart" Visible="False"></asp:Button>
            <asp:Button ID="cmdSave" runat="server" CssClass="submitBtn" Text="Save As New Cart" Visible="False" />
        </td>
    </tr>
</table>
<cc1:AptifyShoppingCart runat="server" ID="ShoppingCart1" />

