<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnhancedListingPaymentStep.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedListingPaymentStep" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="uc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="cai-form">
    <asp:Panel id="paymentStep" runat="server">
        <div class="form-section-full-border no-border no-margin">
            <h2>Pay for selected firm(s)</h2>
        </div>
        <div class="form-section-half-border no-border card-payment-control"> 
            <div class="form-group hide-it">
                <span class="label-title">Discount code:</span>
                <asp:Label runat="server" Visible="false" ID="lblIncorrectCode" CssClass="txtbox-help-text">Code you entered is incorrect</asp:Label>
                <asp:TextBox id="txtDiscountcode" runat="server" placeholder="Enter your discount code here" AutoPostBack="true" OnTextChanged="txtDiscountcode_TextChanged"/>
                <span class="txtbox-help-text">If you have a discount code, you can use it here.</span>
            </div>
            <uc1:CreditCard ID="CreditCard" runat="server" />
        </div>
        <div class="form-section-half-border no-border">
            <div class="form-group">
                <span class="main-label-title">Firm:</span>
                <asp:Label class="main-label-data" style="display:block" ID="lFirmName" runat="server">Chartered Accountants Ireland, 47 Pearse Street, Dublin 2, Ireland</asp:Label>
            </div>
            <%-- ONLY SHOW DISCOUNT LABEL IS DISCOUNT CODE HAS BEEN ENTERED --%>
            <div class="form-group no-margin" runat="server" id="discountHolder">
                <span class="label-title inline-label" style="width:50%">Discount savings:</span>
                <span runat="server" id="lblDiscount" class="price-bold" style="color:green">€100.00 (discount applied)</span>
            </div>
            <div class="form-group no-margin">
                <%-- Sub-total does not include VAT amount, but Sub-total includes the discount if any--%>
                <span class="label-title inline-label" style="width:50%">Sub -total:</span>
                <span runat="server" id="lblSubtotal" class="price-bold">€200.00</span>
            </div>
            <div class="form-group no-margin">
                <span class="label-title inline-label" style="width:50%">VAT:</span>
                <span runat="server" id="lblVAT" class="price-bold">€23.00</span>
            </div>
            <div class="form-group no-margin">
                <span class="label-title inline-label" style="width:50%">Total (inc. VAT):</span>
                <span runat="server" id="lblPaymentAmount" class="price-final"></span>
            </div>
            <div class="form-group" style="color: red" id="lblError" runat="server" visible="false"></div>
            <div class="form-group" runat="server" visible="false">
                <span class="label-title">Payment Type<span class="required"></span></span>
                <asp:DropDownList runat="server" ID="dropdownPaymentType"></asp:DropDownList>
            </div>
            <asp:Panel ID="pnlBillTo" runat="server">
                <div class="form-group">
                    <span class="label-title">Bill To<span class="required"></span></span>
                    <asp:DropDownList runat="server" ID="dropdownFirmList"></asp:DropDownList>
                </div>
            </asp:Panel>
            <div class="form-group" runat="server" visible="false">
                <span class="label-title">PO number<span class="required"></span></span>
                <asp:TextBox id="txtPOnumber" runat="server" placeholder="PO number"/>
            </div>
            <div class="form-group">
                <span class="label-title">Comments</span>
                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" width="100%" height="100px" Style="resize:vertical;"></asp:TextBox>
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
        </div>
        <div class="form-section-full-border center card-payment-control">
            <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="cai-btn cai-btn-red" OnClick="btnSave_Click" />
        </div>
    </asp:Panel>
    <asp:Panel ID="thankYouStep" runat="server" Visible="false">
        <div class="form-section-full-border no-border no-margin">
            <h1>Thank you!</h1>
            <h2>Purchase complete for premium listing(s)</h2>
        </div>
        <div class="form-section-full-border no-border sfContentBlock">
            <p>Thank you for your order, you have successfully purchased premium listing(s) on the Institute firm directory.</p>
            <p>Your order number is: <b runat="server" id="paymentOrderNumber"></b></p>
            <h2>What next?</h2>
            <p>Please continue to <a href="/Find-a-Firm/edit"><strong>edit the details</strong></a> for the premium listing(s). It is very straightforward, if you get stuck there is a handy <code><a href="https://www.charteredaccountants.ie/PremiumListing/Help" target="_blank">Help page</a></code> and you can also email or phone us for support.</p>
            <p>Once you <strong>submit your details</strong> all listings will be reviewed and once approved, you’ll receive a confirmation email and the premium listing will appear on the directory. The premium listing will then remain on the directory for the next 365 days.</p>
            <div class="sfContentBlock">
                <a href="/Find-a-Firm/edit" class="cai-btn cai-btn-red">Edit your premiums listings</a>
            </div>
            <div class="sfContentBlock" style="margin-top:18px">
                <p><strong>Contact:</strong> Orla Aherne / Rachel Pattison<br>
                    <strong>Phone:</strong> +353 1 523 3907 / 3927<br>
                    <strong>Email: </strong><a href="mailto:premiumlisting@charteredaccountants.ie">premiumlisting@charteredaccountants.ie</a><strong>&nbsp;</strong></p>
            </div>
        </div>
    </asp:Panel>


    <cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
</div>

<uc2:User id="User1" runat="server" />
