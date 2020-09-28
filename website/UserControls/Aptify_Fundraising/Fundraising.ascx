
 <%@ Control Language="VB" AutoEventWireup="false" CodeFile="Fundraising.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Fundraising.Fundraising" %> 

 <%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %> 
 <%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %> 
 <%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_General/CreditCard.ascx"  %> 

<div class="content-container clearfix">
	<table id="tblMain" runat="server">
        <tr>
            <td>
                <asp:Label ID="lblMsg" Visible="False" runat="server"></asp:Label>
                <table id="tblInner" runat="server">
                    <tr>
                    <%-- Amruta IssueID 15019 --%>
                        <td colspan="2">
                            Thank you for your interest in supporting our organization. Please select your 
                            contribution amount and the project you want to support.
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2px;" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumn" style="padding-left: 9px;width:120px;">
                            <b><asp:Label ID="lblFund" runat="server">Project</asp:Label></b>
                        </td>
                         <%-- Amruta IssueID 15019 --%>
                         <td class="tdAmountFundraising">
                            <asp:DropDownList ID="cmbFunds" runat="server" CssClass="ddlProjectfundrasing" DataValueField="ID" DataTextField="WebName">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="padding-top: 5px;">
                        <td class="LeftColumn" style="padding-left: 9px; padding-right: 2px;">
                           <b><asp:Label ID="lblAmount" runat="server">Amount</asp:Label></b>
                            <div style="float:right;">
                            <asp:Label ID="lblCurrencySymbol" runat="server"></asp:Label>
                            </div>
                       </td>
                     
                       <%-- Amruta IssueID 15019--%>
                        <%-- Amruta IssueID 13077 ErrorMessage Change--%> 
                        <td  class="tdAmountFundraising">
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtboxAmountFundraising"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" Type="Double" ValueToCompare="0.00"
                                Operator="GreaterThan" ErrorMessage="Amount Must Be Greater Than 0" ControlToValidate="txtAmount"  ForeColor="Red" Width="100px" ></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredField" Display="Dynamic" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please Enter Amount" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
   <%-- Amruta IssueID 15019--%> 
   <%-- Amruta IssueID 13077 SubmitButton Alignment--%> 
<div id="paymentarea" runat="server" class="divFundPaymentFundraising" style="width: 490px; float: left;">
    <fieldset style="border: 1px solid grey; height: auto;">
        <legend><b>Payment Information</b></legend>
        <uc1:CreditCard ID="CreditCard" runat="server"></uc1:CreditCard>
        <div class="divSubmitBtnFundraising" >
            <asp:Button ID="cmdSubmit" runat="server" Text="Submit Your Donation" CssClass="submitBtn" />
        </div>
        <table>
            <tr>
                <td style="width: 1px;">
                    &nbsp;
                </td>
            </tr>
        </table>
    </fieldset>
</div>

 <cc1:User ID="User1" runat="server" />
    <cc3:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
  