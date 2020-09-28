<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SubscriptionProductDetails.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.SubscriptionProductDetails" %>
    <%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<div class="content-container clearfix">
    <asp:Label ID="lblError" runat="server" Text="Label" Visible="False"></asp:Label>

    <table id="tblMain" class="data-form" runat="server">
       <%-- <tr>
       
            <td class="LeftColumn">
                Startdate:
            </td>
            <td class="RightColumn">
                <asp:Label ID="lblSubscriptionval" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                Enddate:
            </td>
            <td class="RightColumn">
                <asp:Label ID="lblSubscriptionvalEnd" runat="server" />
            </td>
        </tr>--%>
         <tr>
            <td class="LeftColumn">
                Product:
            </td>
            <td class="RightColumn">
                <asp:Label ID="lblproduct" runat="server" />
            </td>
        </tr>
         <%--<tr>
            <td class="LeftColumn">
                Status:
            </td>
            <td class="RightColumn">
                <asp:Label ID="lblstatus" runat="server" />
            </td>
        </tr>--%>
        <tr>
            <td class="LeftColumn">
                Auto Renew:
            </td>
            <td class="RightColumn">
                <asp:CheckBox ID="chkAutoRenew" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="RightColumn">
                <asp:Button ID="btnUpdate" runat="server" Text="Ok" CssClass="submitBtn" />
            </td>
        </tr>
    </table>

</div>
<cc1:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
