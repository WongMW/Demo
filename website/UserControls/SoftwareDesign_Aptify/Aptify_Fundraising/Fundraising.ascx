
 <%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Fundraising/Fundraising.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Fundraising.Fundraising" %> 

 <%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %> 
 <%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %> 
 <%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<script type="text/javascript">
    window.history.forward(-1);
    
    function DisableBtn(event) {

        if ($('#baseTemplatePlaceholder_content_MakePaymentSummary_cmdPay').hasClass('DisablePayBtn')) {
            event.preventDefault();
            event.stopPropagation();
        } else if (Page_ClientValidate("")) {
            document.getElementById("baseTemplatePlaceholder_content_MakePaymentSummary_cmdPay").value = "Please Wait..";
            document.getElementById("baseTemplatePlaceholder_content_MakePaymentSummary_cmdPay").setAttribute("class", "DisablePayBtn");
        }
    }
</script>

<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="PnlFundraising">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>LOADING...<br /><br />
                    Please do not leave or close this window while payment is processing.</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="PnlFundraising" runat="server">
    <ContentTemplate>
        <div class="form-section-full-border no-border no-margin">
	        <asp:Label ID="lblMsg" Visible="False" runat="server"></asp:Label>
        </div>
        <div class="form-section-half-border no-border" ID="tblInner" runat="server">
	        <div class="sfContentBlock">
		        <p>Thank you for your interest in supporting CA Support. CA Support is here to offer emotional, practical and financial support to Chartered Accountants, Accounting Technicians, students and their family members for life.</p>
		        <p>Please enter in the amount you wish to contribute below.</p>
	        </div>
	        <div class="form-group" style="display:none">
		        <span class="label-title">Project</span>
		        <asp:DropDownList ID="cmbFunds" runat="server" CssClass="ddlProjectfundrasing" DataValueField="ID" DataTextField="WebName">
								        </asp:DropDownList>
	        </div>
	        <div class="form-group">
		        <span class="label-title">Donation amount</span>
		        <asp:Label ID="lblCurrencySymbol" runat="server"></asp:Label>
		        <asp:TextBox ID="txtAmount" runat="server" CssClass="txtboxAmountFundraising" Width="100%"></asp:TextBox>
		        <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" Type="Double" ValueToCompare="0.00"
			        Operator="GreaterThan" ErrorMessage="Amount Must Be Greater Than 0" ControlToValidate="txtAmount"  ForeColor="Red" Width="100px" ></asp:CompareValidator>
		        <asp:RequiredFieldValidator ID="RequiredField" Display="Dynamic" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please Enter Amount" ForeColor="Red"></asp:RequiredFieldValidator>
	        </div>
        </div>
        <div class="form-section-half-border no-border card-payment-control" id="paymentarea" runat="server">
	        <uc1:CreditCard ID="CreditCard" runat="server"></uc1:CreditCard>
	        <div class="divSubmitBtnFundraising" >
		        <asp:Button ID="cmdSubmit" runat="server" Text="Submit Your Donation" CssClass="submitBtn" OnClientClick="if ($('#baseTemplatePlaceholder_content_Fundraising_cmdSubmit').hasClass('DisablePayBtn')) {return false;} javascript:DisableBtn(event);" />
	        </div>
	        <table>
		        <tr>
			        <td style="width: 1px;">
				        &nbsp;
			        </td>
		        </tr>
	        </table>
        </div>
        <cc1:User ID="User1" runat="server" />
        <cc3:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
     </ContentTemplate>
</asp:UpdatePanel>
