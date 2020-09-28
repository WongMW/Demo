<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/BillingControl__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.BillingControl__c" %>
<%@ Register TagPrefix="uc1" TagName="CartGrid2" Src="../Aptify_Product_Catalog/CartGrid2.ascx" %>
<%--   <%@ Register TagPrefix="uc2" TagName="OrderSummary" Src="../Aptify_Product_Catalog/OrderSummary.ascx" %>--%>
<%@ Register TagPrefix="uc5" TagName="OrderSummary__c" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/OrderSummaryBillingPage__c.ascx" %>
<%@ Register TagPrefix="uc3" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="uc4" TagName="CreditCard" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/CreditCard__c.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<script language="javascript" type="text/javascript">
    window.history.forward(1);
    
    function DisableBtn(event) {

        if ($('#baseTemplatePlaceholder_content_BillingControl__c_cmdPlaceOrder').hasClass('DisablePayBtn')) {
            event.preventDefault();
            event.stopPropagation();
        } else if (Page_ClientValidate("")) {
            document.getElementById("baseTemplatePlaceholder_content_BillingControl__c_cmdPlaceOrder").value = "Please Wait..";
            document.getElementById("baseTemplatePlaceholder_content_BillingControl__c_cmdPlaceOrder").setAttribute("class", "DisablePayBtn");
        }
    }
</script>
<%--Susan - Issue #18343--%>

<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>LOADING...<br /><br />
                   Please do not leave or close this window while payment is processing.</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="content-container clearfix cai-table checkout-billing">
    <div id="lblGotItems" runat="server" class="cart-message">
        <p><strong>Please review and submit your order</strong></p>
        <p>
            Your default shipping address and other settings are shown below. Use 
					        the buttons to make any changes. When you're done, click the "Complete Order" 
					        button.
        </p>
        <asp:Label ID="lblNoItems" runat="server" Font-Size="12pt" Visible="False" ForeColor="Maroon" Font-Bold="True">
                    There are no items in your shopping cart.
        </asp:Label>
    </div>
    <%--Added/Modified/Commented By Kavita Z Issue#17719--%>
    <%-- <b>
        <asp:Label ID="lblError" runat="server" Text="lblError" Visible="False"></asp:Label></b>--%>
    <div>
        <asp:UpdatePanel ID="updpanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <b>
                    <asp:Label ID="lblError" runat="server" Text="lblError" Visible="false"></asp:Label>
                </b>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdPlaceOrder" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

    </div>	
    <cc2:User ID="User2" runat="server" />
    <div id="tblMain" runat="server" width="100%">
        <div class="">
            <asp:Label ID="lblPaymentPlan" runat="server" CssClass="label" Text="Payment plan:" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddlPaymentPlan" runat="server" AutoPostBack="true" Width="200px">
            </asp:DropDownList>
            <asp:HiddenField ID="hfProductIDs" Value="0" runat="server" />
            <asp:HiddenField ID="hidIsFirmAdmin" Value="0" runat="server" />
            <asp:HiddenField ID="hidSubscriberID" Value="" runat="server" />
            <telerik:RadGrid ID="radPaymentPlanDetails" runat="server" AutoGenerateColumns="False"
                AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                AllowFilteringByColumn="false" Visible="false" AllowSorting="false" CssClass="payment-table">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Days" AllowFiltering="false" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text="Days:" CssClass="mobile-label"></asp:Label>
                                <asp:Label ID="lbldays" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "days") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Schedule date" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text="Scheduale date" CssClass="mobile-label"></asp:Label>
                                <asp:Label ID="lblScheduleDate" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "ScheduleDate") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Percentage" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text="Percentage:" CssClass="mobile-label"></asp:Label>
                                <asp:Label ID="lblPercentage" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Percentage") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Amount" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text="Amount:" CssClass="mobile-label"></asp:Label>
                                <asp:Label ID="lblAmt" runat="server" CssClass="cai-table-data" Text='<%# SetStagePaymentAmount(Eval("Percentage"),(Eval("days"))) %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div id="tblRowMain" runat="server" class="checkout-billing-details">
            <uc5:OrderSummary__c ID="OrderSummary__c" runat="server" />
            <div class="tdbgcolorshipping" visible="false" id="iBillingDetails" runat="server">
                <strong>Billing details</strong>
            </div>
            <%-- Nalini issue 12577--%>
            <div id="ilblBill" runat="server" visible="false">
                <strong>Billing to:</strong>
            </div>
            <div class="address-block" id="iNameAdd" runat="server" visible="false">
                <uc3:NameAddressBlock ID="NameAddressBlock" runat="server" />
                <br>
            </div>
            <div class="tdchangebuttonshipping" id="iChnageAdd" runat="server" visible="false">
                <%-- <asp:HyperLink ID="lnkChangeAddress" runat="server">--%>
                <%--<img id="imgChangeAddress" runat="server" alt="Change Bill Address" src="" border="0" />--%>
                <%--  </asp:HyperLink>--%>
                <asp:Button CssClass="submitBtn" ID="lnkChangeAddress" runat="server" Text="Change Address"
                    CausesValidation="false" />
            </div>
        </div>
        <div class="checkout-payment-info">
            <div class="tdbgcolorshipping">
                <strong>Payment information</strong>
            </div>
            <div class="credit-card-details">
                <uc4:CreditCard ID="CreditCard" runat="server"></uc4:CreditCard>
            </div>
        </div>

    </div>
</div>
<div class="cart-label-link">
    <i class="fa fa-shopping-cart fa-2x cart-icon" id="search-icon"></i>
    <strong>Items in shopping cart</strong>
</div>
<%--Rashmi P, Issue 5133, Add ShipmentType Selection --%>
<div id="tdShipment" runat="server" visible="false">
    <strong>Shipping method:</strong>&nbsp;<asp:DropDownList runat="server"
        ID="ddlShipmentType" AutoPostBack="true">
    </asp:DropDownList>
</div>
<div class="cai-table">
    <uc1:CartGrid2 ID="CartGrid2" runat="server"></uc1:CartGrid2>
</div>
<div>
    <table class="tableBillingClass">
        <tr>
            <td>
                <%--  <uc2:OrderSummary ID="OrderSummary" runat="server" /> --%>
                <%-- Changes by Ganesh I on 03/24/2014 --%>
               
            </td>
        </tr>
    </table>
</div>
<div>
    <table id="tblStageDetails" runat="server" visible="false">
        <tr>
            <td>
                <div class="data-form order-summary cai-table">
                    <div>
                        <span class="billing-label">Stage summary
                        </span>
                    </div>
                    <div class="stage-amount">
                        <asp:Label ID="lblStageAmt" CssClass="billing-label" runat="server" Text="Total stage amount:"></asp:Label>

                        <asp:Label ID="lblCurrency" CssClass="cai-table-data right" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblTotalAmount" CssClass="cai-table-data right" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblStagePaymentTotal" CssClass="cai-table-data right" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblStageZeroDaysAmt" CssClass="cai-table-data right" runat="server" Text=""></asp:Label>

                    </div>
                    <div class="without-stage-amount">
                        <asp:Label ID="Label5" runat="server" CssClass="billing-label" Text="Without stage amount:"></asp:Label>
                        <asp:Label ID="lblWithoutStageAmountCurrency" CssClass="cai-table-data right" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblWithoutStageAmt" CssClass="cai-table-data right" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="total-pay-amount">
                        <asp:Label ID="lblTotalAmountPaid" CssClass="billing-label" runat="server" Text="Total pay amount:"></asp:Label>

                        <asp:Label ID="lblTotalAmtCurrency" CssClass="cai-table-data right" runat="server" Text="">
                        </asp:Label>
                        <asp:Label CssClass="cai-table-data right" ID="lblIntialAmount" runat="server"></asp:Label>
                    </div>
                    <div class="total-tax-amount">
                        <asp:Label ID="lblTaxStageAmount" CssClass="billing-label" runat="server" Text="Tax amount:" Visible="false"></asp:Label>

                        <asp:Label ID="lblTaxAmount" runat="server" CssClass="cai-table-data right" Text="" Visible="false"></asp:Label>
                    </div>
                    <div class="total-shipping-amount">
                        <asp:Label ID="lblShippingStage" CssClass="billing-label" runat="server" Text="Shipping amount:" Visible="false"></asp:Label>

                        <asp:Label ID="lblShiipingStageAmount" CssClass="cai-table-data right" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                    <div class="total-handling-amount">
                        <asp:Label ID="lblHandlingStage" CssClass="billing-label" runat="server" Text="Shipping amount:" Visible="false"></asp:Label>

                        <asp:Label ID="lblHandlingStageAmount" CssClass="cai-table-data right" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div>
            </td>
        </tr>
    </table>

</div>

<div style="padding:10px;background-color:#cde9db;float:left;">
	<!-- Begin DigiCert site seal HTML and JavaScript -->
	<p style="display:inline-block;float:left;font-weight:bold">We support secure payment &nbsp;</p>
	<div id="DigiCertClickID_7WwNtfwv" data-language="en" style="display:inline-block;float:left">
		<a href="https://www.digicert.com/wildcard-ssl-certificates/">Wildcard Certificate</a>
	</div>

	<script type="text/javascript">
		var __dcid = __dcid || [];__dcid.push(["DigiCertClickID_7WwNtfwv", "3", "m", "black", "7WwNtfwv"]);(function(){var cid=document.createElement("script");cid.async=true;cid.src="//seal.digicert.com/seals/cascade/seal.min.js";var s = document.getElementsByTagName("script");var ls = s[(s.length - 1)];ls.parentNode.insertBefore(cid, ls.nextSibling);}());
	</script>
	<!-- End DigiCert site seal HTML and JavaScript -->
</div>

<div class="cai-table marg-top-btm-20px right">
    <asp:UpdatePanel ID="UpdatePanelPayment" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="actions">
                <asp:Button CssClass="submitBtn" ID="cmdBack" runat="server" Text="<< Back"
                    CausesValidation="False" />
                <asp:Button CssClass="submitBtn" ID="cmdPlaceOrder" 
                    OnClientClick="if ($('#baseTemplatePlaceholder_content_BillingControl__c_cmdPlaceOrder').hasClass('DisablePayBtn')) {return false;} javascript:DisableBtn(event);"
                    TabIndex="1" runat="server"
                    Text="Complete Order"></asp:Button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<table style="display:none;">
    <tr>
        <td valign="top" align="left" colspan="2">
            <asp:Label ID="lblPaymentMsg" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
<cc2:User runat="Server" ID="User1" />
