<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BillingControl__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.BillingControl__c" %>
<%@ Register TagPrefix="uc1" TagName="CartGrid2" Src="../Aptify_Product_Catalog/CartGrid2.ascx" %>
<%--   <%@ Register TagPrefix="uc2" TagName="OrderSummary" Src="../Aptify_Product_Catalog/OrderSummary.ascx" %>--%>
<%@ Register TagPrefix="uc5" TagName="OrderSummary__c" Src="OrderSummary__c.ascx" %>
<%@ Register TagPrefix="uc3" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="uc4" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
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
<%--Nalini Issue#12578--%>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" width="100%">
        <tr runat="server">
            <td>
                <asp:Label ID="lblError" runat="server" Text="lblError" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                    <font size="2">
                        <asp:Label ID="lblGotItems" runat="server">
			        <p><strong>Please review and submit your order</strong></p>
			        Your default shipping address and other settings are shown below. Use 
					        the buttons to make any changes. When you're done, click the "Complete Order" 
					        button.
                        </asp:Label>
                        <asp:Label ID="lblNoItems" runat="server" Font-Size="12pt" Visible="False" ForeColor="Maroon"
                            Font-Bold="True">
                    There are no items in your shopping cart.
                        </asp:Label></font>&nbsp;</p>
                <p>
                    <font size="2">&nbsp;
                        <cc2:User ID="User2" runat="server" />
                    </font>
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td valign="top">
                          <b>   <asp:Label ID="lblPaymentPlan" runat="server" Text="Payment Plan:" Visible="false"></asp:Label></b>
                        </td>
                        <td valign="top">
                            <asp:DropDownList ID="ddlPaymentPlan" runat="server" AutoPostBack="true" Width="200px">
                            </asp:DropDownList>
                           
                        </td>
                        <td  valign="top">
                            <telerik:RadGrid ID="radPaymentPlanDetails" runat="server" AutoGenerateColumns="False"
                                AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                AllowFilteringByColumn="false" Visible="false" AllowSorting="false" Width="200px">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Days" AllowFiltering="false" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "days") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Schedule Date" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblScheduleDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ScheduleDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Percentage" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPercentage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Percentage") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Amount" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmt" runat="server" Text='<%# SetStagePaymentAmount(Eval("Percentage"),(Eval("days"))) %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        <td>&nbsp;</td>
        </tr>
        <tr id="tblRowMain" runat="server">
            <td valign="top" align="left">
                <table id="Table2" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td valign="top" width="49%">
                            <table cellspacing="0" cellpadding="0" valign="top" class="bordercolorbilling" width="100%">
                                <tr>
                                    <td class="tdbgcolorshipping">
                                        <strong><font size="2" color="white">Billing Details</font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <%-- Nalini issue 12577--%>
                                    <td style="padding-left: 5px;">
                                        <strong><font size="2">Billing To: </font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left" style="padding-left: 5px;">
                                        <font size="2">
                                            <uc3:NameAddressBlock ID="NameAddressBlock" runat="server" />
                                            <br>
                                        </font>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdchangebuttonshipping">
                                        <%-- <asp:HyperLink ID="lnkChangeAddress" runat="server">--%>
                                        <%--<img id="imgChangeAddress" runat="server" alt="Change Bill Address" src="" border="0" />--%>
                                        <%--  </asp:HyperLink>--%>
                                        <asp:Button CssClass="submitBtn" ID="lnkChangeAddress" runat="server" Text="Change Address"
                                            CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 1%;">
                            &nbsp;
                        </td>
                        <td valign="top" width="49%" class="bordercolor">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td colspan="2" class="tdbgcolorshipping">
                                        <strong><font size="2" color="white">Payment Information</font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px;">
                                        <uc4:CreditCard ID="CreditCard" runat="server"></uc4:CreditCard>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" colspan="2">
                            <asp:Image ID="imgShoppingCart" runat="server" ImageUrl="~/Images/shoppingCartShipping.gif" />
                            <strong><font size="2">Items in shopping cart</font></strong>
                        </td>
                        <%--Rashmi P, Issue 5133, Add ShipmentType Selection --%>
                        <td align="right" id="tdShipment" runat="server">
                            <strong><font size="2">Shipping Method:</font></strong>&nbsp;<asp:DropDownList runat="server"
                                ID="ddlShipmentType" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td valign="top" align="left" colspan="2">
                            <uc1:CartGrid2 ID="CartGrid2" runat="server"></uc1:CartGrid2>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div style="float: right; width: 163px;">
    <table class="tableBillingClass">
        <tr>
            <td>
                <%--  <uc2:OrderSummary ID="OrderSummary" runat="server" /> --%>
                <%-- Changes by Ganesh I on 03/24/2014 --%>
                <uc5:OrderSummary__c ID="OrderSummary__c" runat="server" />
            </td>
        </tr>
    </table>
</div>
<table id="tblStageDetails" runat="server" visible="false">
    <tr>
    </tr>
    <tr>
        <td>
            <b>
                <asp:Label ID="lblStageAmt" runat="server" Text="Total Stage Amount:"></asp:Label></b>
        </td>
        <td>
            <asp:Label ID="lblCurrency" runat="server" Text=""  Visible="false"></asp:Label>
            <asp:Label ID="lblTotalAmount" runat="server" Text="" Visible="false"></asp:Label>
           <b> <asp:Label ID="lblStagePaymentTotal" runat="server" Text=""  Visible="false"></asp:Label>
           <asp:Label ID="lblStageZeroDaysAmt" runat="server" Text=""  ></asp:Label></b>
           
        </td>
    </tr>
    <tr>
        <td>
            <b>
                <asp:Label ID="Label1" runat="server" Text="Without Stage Amount:"></asp:Label></b>
        </td>
        <td>
           <b> <asp:Label ID="lblWithoutStageAmountCurrency" runat="server" Text=""></asp:Label><asp:Label ID="lblWithoutStageAmt" runat="server" Text=""  ></asp:Label></b>
        </td>
    </tr>
    <tr>
    <td>
    <b><asp:Label ID="lblTotalAmountPaid" runat="server" Text="Total Pay Amount:"></asp:Label></b>
    </td>
    <td>
   <b>  <asp:Label ID="lblTotalAmtCurrency" runat="server" Text=""></asp:Label><asp:Label ID="lblIntialAmount" runat="server"></asp:Label></b>
    </td>
    </tr>
    <tr>
        <td>
            <b>
                <asp:Label ID="lblTaxStageAmount" runat="server" Text="Tax Amount:" Visible="false"></asp:Label></b>
        </td>
        <td>
            <b>
                <asp:Label ID="lblTaxAmount" runat="server" Text="" Visible="false"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td>
            <b>
                <asp:Label ID="lblShippingStage" runat="server" Text="Shipping Amount:" Visible="false"></asp:Label></b>
        </td>
        <td>
            <b>
                <asp:Label ID="lblShiipingStageAmount" runat="server" Text="" Visible="false"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td>
            <b>
                <asp:Label ID="lblHandlingStage" runat="server" Text="Shipping Amount:" Visible="false"></asp:Label></b>
        </td>
        <td>
            <b>
                <asp:Label ID="lblHandlingStageAmount" runat="server" Text="" Visible="false"></asp:Label></b>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td valign="top" align="left" colspan="2">
            <p>
            </p>
            <asp:Button CssClass="submitBtn" ID="cmdBack" runat="server" Text="<< Back" Height="26px"
                CausesValidation="False" />
            <asp:Button CssClass="submitBtn" ID="cmdPlaceOrder" TabIndex="1" runat="server" Height="26px"
                OnClientClick="if ($('#baseTemplatePlaceholder_content_BillingControl__c_cmdPlaceOrder').hasClass('DisablePayBtn')) {return false;} javascript:DisableBtn(event);"
                Text="Complete Order"></asp:Button>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td valign="top" align="left" colspan="2">
            <asp:Label ID="lblPaymentMsg" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
<cc2:User runat="Server" ID="User1" />
