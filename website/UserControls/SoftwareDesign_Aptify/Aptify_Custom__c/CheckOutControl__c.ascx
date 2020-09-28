<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/CheckoutControl__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.CheckoutControl__c" %>
<%@ Register TagPrefix="uc1" TagName="CartGrid" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/CartGrid__c.ascx" %>
<%-- Changes by Ganesh I on 24/03/2014 --%>
<%@ Register TagPrefix="uc1" TagName="OrderSummary" Src="OrderSummary__c.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShippingControl" Src="../Aptify_Product_Catalog/ShippingControl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--Nalini Issue#12578--%>

<%--Added by Deepika on 27/10/2017 to add the ProcessIndicator.--%>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>LOADING...<br /><br />
                    Please do not leave or close this window while the request is processing.</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<asp:UpdatePanel ID="updatePanelButton" runat="server" ChildrenAsTriggers="True">
    <ContentTemplate>
        <div class="content-container clearfix cai-table checkout-process-1 marg-btm-20px">
            <div>
                <asp:Label ID="lblGotItems" runat="server">
                    <p>
                        <strong>Please review and submit your order</strong></p>
                    <p>
                        <asp:Label ID="lblcheckoutMsg" runat="server"></asp:Label>
                    </p>
                </asp:Label>
                <asp:Label ID="lblNoItems" runat="server" Font-Size="12pt" Visible="False" ForeColor="Maroon"
                    Font-Bold="True">There are no items in your shopping cart.</asp:Label>
                <br />
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                <p>
                    <cc2:user id="User1" runat="server" />
                </p>
            </div>
            <div id="tblMain" runat="server">
                <div id="tblRowMain" runat="server">
                    <div>
                        <div class="shipping-address">
                            <%-- Nalini issue 12577--%>
                            <div class="tdbgcolorshipping" runat="server" visible="True" id="iBillingDetails">
                                <strong>Billing details</strong>
                            </div>
                            <div id="ilblBill" runat="server">
                                <strong>Billing to:</strong>
                            </div>
                            <div class="address-block" id="iNameAdd" runat="server" visible="True">
                                <div>
                                    <uc2:nameaddressblock id="NameAddressBlock" runat="server" />
                                </div>
                                <div>
                                    <asp:CheckBox ID="chkCompanyStatement" runat="server" Text="Tick if you want this purchase to appear on your company statement"
                                        Visible="false" AutoPostBack="true" />
                                    &nbsp;
                                </div>
                                <div>
                                    &nbsp;
                                </div>
                                <div>
                                    <asp:Button CssClass="submitBtn" ID="lnkChangeAddress" runat="server" Text="Change address"
                                        CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-section-half">
                        <div style="border: 1px solid #ccc; padding: 5px;">
                            <%-- Changes by Ganesh I on 24/03/2014 --%>
                            <uc1:shippingcontrol id="ShippingControl" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <div class="cart-label-link">
                    <i class="fa fa-shopping-cart fa-2x cart-icon" id="search-icon"></i><strong><font
                        size="2">Items in shopping cart</font></strong>
                </div>
                <div id="tdShipment" runat="server" align="right" visible="false">
                    <strong>Shipping method:</strong>&nbsp;<asp:DropDownList runat="server" ID="ddlShipmentType"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div>
                <uc1:cartgrid id="CartGrid" runat="server"></uc1:cartgrid>
            </div>
            <div class="checkout-process-actions" style="padding-right: 360px;">
                <p>
                </p>
                <asp:Button ID="cmdUpdateCart" runat="server" Text="Update" CssClass="submitBtn">
                </asp:Button>
            </div>
            <div id="divTotals" runat="server" class="checkout-total-table" style="clear: both;
                width: 26%;">
                <div class="tablecontrolsfontLogin cart-total-details ">
                    <asp:Label ID="Label4" CssClass="cart-total-label" runat="server">Sub-total:</asp:Label>
                    <asp:Label ID="lblSubTotal" CssClass="cart-total" runat="server"></asp:Label>
                </div>
                <div class="tablecontrolsfontLogin cart-total-details ">
                    <asp:Label ID="Label3" CssClass="cart-total-label" runat="server">VAT:</asp:Label>
                    <asp:Label ID="lblTax" CssClass="cart-total" runat="server"></asp:Label>
                </div>
                <div class="tablecontrolsfontLogin cart-total-details">
                    <asp:Label ID="Label2" CssClass="cart-total-label" runat="server">Shipping:</asp:Label>
                    <asp:Label ID="lblShipping" CssClass="cart-total" runat="server"></asp:Label>
                </div>
                <div class="tablecontrolsfontLogin cart-total-details ">
                    <asp:Label ID="Label1" CssClass="cart-total-label" runat="server">Total:</asp:Label>
                    <asp:Label ID="lblGrandTotal" CssClass="cart-total" runat="server" ForeColor="#fd4310"
                        Font-Bold="true" Font-Size="X-Large"></asp:Label>
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="checkout-process-actions">
                <p>
                </p>
                <p>
                </p>
                <asp:Button ID="cmdNextStep" runat="server" CssClass="submitBtn" Text="Next Step >>">
                </asp:Button>
            </div>
            <cc1:aptifyshoppingcart id="ShoppingCart" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
