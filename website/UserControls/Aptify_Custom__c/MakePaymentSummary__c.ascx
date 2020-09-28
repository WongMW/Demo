<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MakePaymentSummary__c.ascx.vb"
    Debug="true" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.MakePaymentSummary__c" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<script type="text/javascript">
    window.history.forward(-1);
</script>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1000px;">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">Please wait... 
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<div class="content-container clearfix">
    <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server">
        <ContentTemplate>
            <table id="tblMain" runat="server" class="data-form">
                <tr id="paymentMade" visible="false" runat="server">
                    <td style="border-bottom: solid 2px gray" class="auto-style6"></td>
                    <td style="border-bottom: solid 2px gray" class="auto-style3">
                        <%--<asp:Label ID="Label3" runat="server"></asp:Label>--%>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

            <div>
                <div>
                    <%--Suraj issue 14450 2/7/13  removed three step sorting ,added tooltip and added date column--%>
                    <%-- <asp:Label ID="lblMainGrid" runat="server" Visible="false" Font-Bold="true">Selected Open Invoices To Be Paid:</asp:Label>--%>
                    <rad:RadGrid ID="grdMain" AutoGenerateColumns="False" runat="server" ValidationSettings-ValidationGroup="Od"
                        EnableLinqExpressions="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                        SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn="true">
                        <GroupingSettings CaseSensitive="false" />

                        <MasterTableView AllowFilteringByColumn="true" AllowNaturalSort="false" NoMasterRecordsText="No Payment Available.">
                            <Columns>
                                <rad:GridHyperLinkColumn DataNavigateUrlFields="ID" DataTextField="ID" HeaderText="Order #"
                                    FilterControlWidth="80%" SortExpression="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false" Target="_new" />
                                <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                                    HeaderStyle-CssClass="CenterAlign" HeaderText="Date" HeaderStyle-Width="180px"
                                    SortExpression="OrderDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    DataType="System.DateTime" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                                <rad:GridBoundColumn HeaderText="Product" DataField="Product" ItemStyle-CssClass="CenterAlign"
                                    HeaderStyle-Width="180px" HeaderStyle-CssClass="CenterAlign" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                                <rad:GridBoundColumn HeaderText="Category" DataField="ProductCategory" ItemStyle-CssClass="CenterAlign"
                                    HeaderStyle-Width="100px" HeaderStyle-CssClass="CenterAlign" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                                <rad:GridBoundColumn HeaderText="Pay Type" DataField="PayType" ItemStyle-CssClass="CenterAlign"
                                    HeaderStyle-Width="70px" HeaderStyle-CssClass="CenterAlign" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" />

                                <rad:GridTemplateColumn HeaderText="Price" DataField="Extended" SortExpression="Extended"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#GetFormattedCurrency(Container, "Extended")%>'>
                                        </asp:Label>
                                        <asp:Label ID="lblOrderLineID" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineID") %>'
                                            runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblOrderLineNumber" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineNumber") %>'
                                            runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%#GetFormattedCurrency(Container, "Extended")%>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" CssClass="griditempaddingRight" />
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="VAT" SortExpression="VAT" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVAT" size="15" runat="server"
                                            Text='<%#GetFormattedCurrency(Container,"VAT")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%--Suraj issue 14450 4/17/14, apply HeaderStyle--%>
                                <rad:GridTemplateColumn HeaderText="Pay Amount" SortExpression="PayAmount" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                    <ItemTemplate>

                                        <asp:Label ID="lblCurrencySymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                        <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="CenterAlign TextboxStylePayment"
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
                    </rad:RadGrid>
                </div>
                <div style="text-align: right;">
                    <asp:Label ID="lblTotal" Text="Order line's Total: " Font-Bold="true"
                        runat="server"></asp:Label>
                    <asp:Label ID="txtTotal" runat="server" Font-Bold="True"></asp:Label>
                </div>
                <div id="dvShipping" runat="server">
                    <asp:Label ID="lblShipping" runat="server" Visible="false" Font-Bold="true">Shipping & Handling Charges:</asp:Label>
                    <rad:RadGrid ID="radgShippingCharges" runat="server" AutoGenerateColumns="False"
                        SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                        PagerStyle-PageSizeLabelText="Records Per Page" AllowPaging="True" PageSize="10">
                        <MasterTableView AllowFilteringByColumn="true">
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
                                <rad:GridTemplateColumn HeaderText="Pay Amount" HeaderStyle-CssClass="rightAlign"
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
                    </rad:RadGrid>
                </div>
                <div style="text-align: right;">
                    <asp:Label ID="lblShippingTotal" Text="Shipping Total: " Font-Bold="true" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="txtShippingTotal" runat="server" Font-Bold="True"></asp:Label>
                </div>
                <div style="text-align: right;">
                    <asp:Label ID="lblPayTotallabel" Text="Total payment amount : " ForeColor="#333333" Font-Size="13px"
                        Font-Bold="true" runat="server"></asp:Label>
                    <asp:Label ID="lblTotalPayment" runat="server" Font-Bold="True"></asp:Label>
                </div>

            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
    <table>
        <tr>
            <td>
                <%--  'Anil B change for 10737 on 13/03/2013
                'Set Credit Card ID to load property form Navigation Config--%>
                <uc1:CreditCard ID="CreditCard" runat="server"></uc1:CreditCard>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button CssClass="submitBtn" ID="btnBack" runat="server" Text="Back" CausesValidation="false"></asp:Button>
                <asp:Button CssClass="submitBtn" ID="cmdPay" runat="server" Text="Make Payment"></asp:Button>
                <asp:Button CssClass="submitBtn" ID="btnReceipt" runat="server" Text="Print Receipt"></asp:Button>
                <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="false" Visible="false"
                    OnClientClick="javascript:window.print();" CssClass="submitBtn" />
                <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
    <cc2:User ID="User1" runat="server" />
    <%--'Anil B Issue 10254 on 20/04/2013
             Added reference for shoping cart--%>
    <cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
</div>
