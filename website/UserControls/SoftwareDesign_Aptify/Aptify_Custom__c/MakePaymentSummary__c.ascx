<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/MakePaymentSummary__c.ascx.vb"
    Debug="true" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.MakePaymentSummary__c" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/CreditCard__c.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<script type="text/javascript">
    window.history.forward(-1);
    
    function DisableBtn(event) {

        if ($('#baseTemplatePlaceholder_content_MakePaymentSummary_cmdPay').hasClass('DisablePayBtn')) {
            event.preventDefault();
            event.stopPropagation();
        } else if (Page_ClientValidate("")) {
            document.getElementById("baseTemplatePlaceholder_content_MakePaymentSummary_cmdPay").value = "Please Wait..";
            document.getElementById("baseTemplatePlaceholder_content_MakePaymentSummary_cmdPay").setAttribute("class", "DisablePayBtn");
			 <%--Added by Sheela - Redmine #20995 To disable Make Payment button --%>
            document.getElementById("baseTemplatePlaceholder_content_MakePaymentSummary_cmdPay").disabled = true;
        }
    }
</script>

<style type="text/css">
    .DisablePayBtn {
        background-color: gray !important;
        color: white !important;
        padding: 8px 20px !important;
        height: 40px !important;
        display: inline-block !important;
        text-transform: uppercase !important;
        border: 2px solid transparent !important;
        margin-right: 5px !important;
    }
</style>
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

<div class="content-container clearfix cai-table make-payment">    
            <table id="tblMain" runat="server" class="data-form">
                <tr id="paymentMade" visible="false" runat="server">
                    <td style="border-bottom: solid 2px gray" class="auto-style6">
                    </td>
                    <td style="border-bottom: solid 2px gray" class="auto-style3">
                        <%--<asp:Label ID="Label3" runat="server"></asp:Label>--%>
                        <asp:Label ID="lblMessage" runat="server" CssClass="info-success"></asp:Label>
                    </td>
                </tr>
            </table>
            <div>
                <div>
                    <%--Suraj issue 14450 2/7/13  removed three step sorting ,added tooltip and added date column--%>
                    <%-- <asp:Label ID="lblMainGrid" runat="server" Visible="false" Font-Bold="true">Selected Open Invoices To Be Paid:</asp:Label>--%>
                    <rad:radgrid id="grdMain" autogeneratecolumns="False" runat="server" validationsettings-validationgroup="Od"
                        enablelinqexpressions="false" sortingsettings-sorteddesctooltip="Sorted Descending"
                        sortingsettings-sortedasctooltip="Sorted Ascending" allowfilteringbycolumn="true"
                        cssclass="payment-data">
                        <GroupingSettings CaseSensitive="false" />

                        <MasterTableView AllowFilteringByColumn="false" AllowNaturalSort="false" NoMasterRecordsText="No Payment Available.">
                            <Columns>
                                <rad:GridHyperLinkColumn DataNavigateUrlFields="ID" DataTextField="ID" HeaderText="Order #" HeaderStyle-Width="120px"
                                    FilterControlWidth="80%" SortExpression="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false" Target="_new" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                                <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                                    HeaderText="Date" HeaderStyle-Width="160px" SortExpression="OrderDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    DataType="System.DateTime" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" HeaderStyle-CssClass="no-mob"
                                    ItemStyle-CssClass="no-mob" />
                                <rad:GridBoundColumn HeaderText="Product" DataField="Product" HeaderStyle-Width="180px" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                                <rad:GridBoundColumn HeaderText="Category" DataField="ProductCategory"
                                    HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                    HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" visible="false"/>
                                <rad:GridBoundColumn HeaderText="Pay type" DataField="PayType" HeaderStyle-Width="120px"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                    HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />

                                <rad:GridTemplateColumn HeaderText="Price" HeaderStyle-Width="140px" DataField="Extended" SortExpression="Extended"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <!-- Order Id -->
                                        <asp:Label ID="Label1" runat="server" Text="Order #:" CssClass="mobile-label"></asp:Label>
                                        <a class="cai-table-data no-desktop" href="<%= Me.OrderConfirmationURL + "?ID=" %><%# DataBinder.Eval(Container.DataItem, "ID")%>"><%# DataBinder.Eval(Container.DataItem, "ID")%></a>
                                        <!-- Date -->
                                        <asp:Label ID="Label2" runat="server" Text="Order date:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label class="cai-table-data no-desktop" ID="OrderDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:MMMM d, yyyy}")%>'></asp:Label>
                                        <!-- Product -->
                                        <asp:Label ID="Label3" runat="server" Text="Product name:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label class="cai-table-data no-desktop" ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product")%>'></asp:Label>
                                        <!-- Category -->
                                        <asp:Label ID="Label5" runat="server" Text="Category:" CssClass="mobile-label" visible="false"></asp:Label>
                                        <asp:Label class="cai-table-data no-desktop" ID="Label6" visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategory")%>'></asp:Label>
                                        <!-- Pay Type -->
                                        <asp:Label ID="Label7" runat="server" Text="Pay type:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label class="cai-table-data no-desktop" ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PayType")%>'></asp:Label>
                                        <!-- Price -->
                                        <asp:Label ID="Label9" runat="server" Text="Price:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="Label10" class="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container, "Extended")%>'>
                                        </asp:Label>
                                        <asp:Label ID="lblOrderLineID" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineID") %>'
                                            runat="server" class="cai-table-data" Visible="false"></asp:Label>
                                        <asp:Label ID="lblOrderLineNumber" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineNumber") %>'
                                            runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%#GetFormattedCurrency(Container, "Extended")%>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" CssClass="griditempaddingRight" />
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="VAT" HeaderStyle-Width="120px" SortExpression="VAT" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <!-- Vat -->
                                        <asp:Label ID="Label11" runat="server" Text="VAT:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblVAT" class="cai-table-data" size="15" runat="server"
                                            Text='<%#GetFormattedCurrency(Container,"VAT")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%--Suraj issue 14450 4/17/14, apply HeaderStyle--%>
                                <%-- Pay Amount --%>
                                <rad:GridTemplateColumn HeaderText="Pay amount" SortExpression="PayAmount" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Text="Currency:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblCurrencySymbol" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                        <asp:Label ID="Label13" runat="server" Text="Pay amount:" CssClass="mobile-label"></asp:Label>
                                        <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="cai-table-data TextboxStylePayment"
                                            Text='<%#GetFormattedCurrencyNoSymbol(Container,"PayAmount")%>' Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" CssClass="griditempaddingRight" />
                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                </rad:GridTemplateColumn>

                                <rad:GridTemplateColumn HeaderText="Currency Digits" Visible="false" DataField="NumDigitsAfterDecimal"
                                    SortExpression="NumDigitsAfterDecimal" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEnableFunds" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EnableFunds") %>'></asp:Label>
                                        <asp:Label ID="lblNumDigitsAfterDecimal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                                        <%--added to update order for making chartered support price 0--%>
                                        <asp:Label ID="lblCharteredOrderLineID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CharteredOrderLineID")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Balance" HeaderStyle-CssClass="rightAlign" Visible="false">
                                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                                    <%-- <HeaderStyle Width="60px" />--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#GetFormattedCurrency(Container, "Extended")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:radgrid>
                </div>
                <div style="text-align: right;">
                    <asp:Label ID="lblTotal" Text="Order line's total: " Font-Bold="true" runat="server"></asp:Label>
                    <asp:Label ID="txtTotal" runat="server" Font-Bold="True"></asp:Label>
                    <asp:HiddenField ID="hidShippingTotal" Value="0" runat="server" />
                    <asp:HiddenField ID="hidPaymentTotal" Value="0" runat="server" />
                </div>
                <div id="dvShipping" runat="server">
                    <asp:Label ID="lblShipping" runat="server" Visible="false" Font-Bold="true">Shipping & Handling Charges:</asp:Label>
                    <rad:radgrid id="radgShippingCharges" runat="server" autogeneratecolumns="False"
                        sortingsettings-sorteddesctooltip="Sorted Descending" sortingsettings-sortedasctooltip="Sorted Ascending"
                        pagerstyle-pagesizelabeltext="Records Per Page" allowpaging="True" pagesize="10">
                        <PagerStyle CssClass="sd-pager" />
                        <MasterTableView AllowFilteringByColumn="false">
                            <Columns>
                                <rad:GridHyperLinkColumn DataNavigateUrlFields="ID" DataTextField="ID" HeaderText="Order #"
                                    SortExpression="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false" />
                                <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                                    HeaderStyle-CssClass="CenterAlign" HeaderText="Date" HeaderStyle-Width="180px"
                                    SortExpression="OrderDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    DataType="System.DateTime" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                                <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="Balance"
                                    SortExpression="Balance" DataField="Balance" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <ItemStyle CssClass="rightAlign gridAlign" Width="80px" />
                                    <HeaderStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Pay amount" HeaderStyle-CssClass="rightAlign"
                                    FilterControlWidth="40%" AllowFiltering="false">
                                    <ItemStyle HorizontalAlign="Center" CssClass="rightAlign gridAlign" Width="130px"></ItemStyle>
                                    <HeaderStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumDigitsAfterDecimal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                                        <asp:Label ID="lblCurrSymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                        <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="rightAlign TextboxStylePayment"
                                            Text='<%# GetFormattedCurrencyNoSymbol(Container,"PayAmount")%>' Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblOrderShipmentID" Text='<%# DataBinder.Eval(Container.DataItem,"OrderShipmentID") %>'
                                            runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:radgrid>
                </div>
                <div class="total-amount-details">
                    <div class="total-amount-label">
                        <asp:Label ID="lblShippingTotal" Text="Shipping Total: " Font-Bold="true" runat="server"
                            Visible="false"></asp:Label>
                        <asp:Label ID="txtShippingTotal" runat="server" Font-Bold="True"></asp:Label>
                    </div>
                    <div class="total-amount">
                        <asp:Label ID="lblPayTotallabel" Text="Total payment amount :" runat="server"></asp:Label>
                        <asp:Label ID="lblTotalPayment" runat="server" Font-Bold="True"></asp:Label>
                    </div>
                </div>
            </div>
    <%--Performance- Shifted Update Panel here, as other controls except credit card control, does not need to be updated --%>
    <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <%--Shifted Error label up to accomodate long message--%>
            <asp:Label ID="lblError" runat="server" CssClass="error-message marg-top-btm-20px"
                    Visible="False" ForeColor="red"></asp:Label>
            <div class="credit-card-options">
                <%--  'Anil B change for 10737 on 13/03/2013
        'Set Credit Card ID to load property form Navigation Config--%>
                <uc1:creditcard id="CreditCard" runat="server"></uc1:creditcard>                
            </div>
             <div class="payment-actions">
                <asp:Button CssClass="submitBtn" ID="btnBack" runat="server" Text="Back" CausesValidation="false"></asp:Button>
                <%--  Commented and added By Pradip 2017-04-30 To avoid  button double click--%>
                <%-- <asp:Button CssClass="submitBtn" ID="cmdPay" runat="server" Text="Make Payment"></asp:Button>--%>
                <asp:Button CssClass="submitBtn" ID="cmdPay" OnClick="cmdPay_Click" runat="server" UseSubmitBehavior="false" 
                    OnClientClick="if ($('#baseTemplatePlaceholder_content_MakePaymentSummary_cmdPay').hasClass('DisablePayBtn')) {return false;} javascript:DisableBtn(event);"
                    Text="Make Payment"></asp:Button>
                <%--End here  Commented and added By Pradip 2017-04-30 To avoid  button double click--%>
                <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="false" Visible="false"
                    OnClientClick="javascript:window.print();" CssClass="submitBtn" />
                <asp:Button CssClass="submitBtn" ID="btnReceipt" runat="server" CausesValidation="false" Visible="false" Text="Print Receipt"></asp:Button>

            </div>
           <%-- <div class="payment-actions">
                <asp:Button CssClass="submitBtn" ID="btnBack" runat="server" Text="Back" CausesValidation="false">
                </asp:Button>
                <asp:Button CssClass="submitBtn" ID="cmdPay" runat="server" Text="Make Payment">
                </asp:Button>
                <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="false" Visible="false"
                    OnClientClick="javascript:window.print();" CssClass="submitBtn" />
                <asp:Button CssClass="submitBtn" ID="btnReceipt" runat="server" CausesValidation="false"
                    Visible="false" Text="Print Receipt"></asp:Button>
            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc2:user id="User1" runat="server" />
    <%--'Anil B Issue 10254 on 20/04/2013
             Added reference for shoping cart--%>
    <cc1:aptifyshoppingcart runat="Server" id="ShoppingCart1" />
</div>
