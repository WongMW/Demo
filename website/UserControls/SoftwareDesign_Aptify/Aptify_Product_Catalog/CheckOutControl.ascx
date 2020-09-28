<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/CheckoutControl.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.CheckoutControl" %>
<%@ Register TagPrefix="uc1" TagName="CartGrid" Src="CartGrid.ascx" %>
<%-- Changes by Ganesh I on 15/04/2014 --%>
<%@ Register TagPrefix="uc1" TagName="OrderSummary" Src="../Aptify_Custom__c/OrderSummary__c.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShippingControl" Src="ShippingControl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--Nalini Issue#12578--%>
 
<div class="content-container clearfix">
    <table id="tblMain" runat="server">
        <tr>
            <td>
                <asp:Label ID="lblGotItems" runat="server">
			        <p><strong>Please review and submit your order</strong></p>
			        <p><font size="2"><asp:Label ID="lblcheckoutMsg" runat="server" ></asp:Label> 
                </asp:Label>
                <asp:Label ID="lblNoItems" runat="server" Font-Size="12pt" Visible="False" ForeColor="Maroon"
                    Font-Bold="True">There are no items in your shopping cart.</asp:Label></font>
                      <br />
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"  ></asp:Label>
                    </p>
                <p>
                    <font size="2">
                        <cc2:User ID="User1" runat="server" />
                    </font>
                </p>
            </td>
        </tr>
        <tr id="tblRowMain" runat="server">
            <td valign="top" align="left">
                <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td valign="top" style="width: 50%;">
                            <uc1:ShippingControl ID="ShippingControl" runat="server" />
                        </td>
                        <td style="width: 1%;">
                            &nbsp;
                        </td>
                        <td valign="top"  class="bordercolor">
                            <table cellspacing="0" cellpadding="2" width="100%">
                                <tr>
                                    <td class="tdbgcolorshipping">
                                        <strong><font size="2" color="white">Order Summary</font></strong>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td style="padding-left:5px;">
                                        <uc1:OrderSummary ID="OrderSummary1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td valign="top" align="left" >
                            <asp:Image ID="imgShoppingCart" runat="server" ImageUrl="~/Images/shoppingCartShipping.gif" />
                            <strong><font size="2">Items in shopping cart</font></strong>
                        </td>
                          <%--Rashmi P, Issue 5133, Add ShipmentType Selection --%>
                          <td align="right" id = "tdShipment" runat = "server"  >
                          <strong><font size="2">Shipping Method:</font></strong>&nbsp;<asp:DropDownList runat = "server" ID = "ddlShipmentType" AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" colspan="2"  width="100%">
                            <uc1:CartGrid ID="CartGrid" runat="server"></uc1:CartGrid>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right" colspan="2">
                            <p>
                            </p>
                            <asp:Button ID="cmdUpdateCart" runat="server" Text="Update" CssClass="submitBtn"  Height="26px">
                            </asp:Button>
                            <asp:Button ID="cmdNextStep" runat="server"  Height="26px" CssClass="submitBtn" Text="Next Step >>">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <cc1:AptifyShoppingCart ID="ShoppingCart" runat="server" />
</div>
