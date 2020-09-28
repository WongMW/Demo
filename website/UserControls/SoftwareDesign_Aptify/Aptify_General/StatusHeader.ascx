<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/StatusHeader.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.HeaderControl" %>
<%@ Register TagPrefix="uc5" TagName="ItemsInCart" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Product_Catalog/ItemsInCart.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>


<div style="width: auto">
    <table border="0" align="right" id="tblHeaderMain" runat="server" width="100%">
        <tr>
            <td style="padding-right: 100px;" id="tdUserName" runat="server">
                <span class="HeaderFontStyle1">Welcome </span>
                <asp:Label ID="lblUserName" runat="server" Text="User Name!" Font-Bold="true" ></asp:Label>
                <asp:Label ID="lblGrpAdmin" runat="server"  Font-Bold="true" ></asp:Label>
                <asp:Label ID="lblCampany" runat="server"  Font-Bold="true" ></asp:Label>
            </td>
            <td style="padding-right: 100px;" runat="server" id="tdItemInCart">
                <table id="tblItemincart" runat="server" style="width: 100%">
                    <tr>
                        <td style="font-weight: bold">
                            <uc5:ItemsInCart ID="ItemsInCart" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: right;" id="tdSignout" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="padding-right: 5px;">
                            <asp:LinkButton ID="ImgLogout" runat="server" Text="Sign out"  CssClass="lnkChapterReportViewer" Font-Bold="True" CausesValidation="false" ></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <cc1:User ID="User1" runat="server" />
    <cc2:AptifyWebUserLogin ID="WebUserLogin" runat="server" />
    <cc2:AptifyShoppingCart ID="ShoppingCart" runat="server"></cc2:AptifyShoppingCart>
</div>