<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Customer_Service/MakePayment.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.MakePaymentControl" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<script type="text/javascript">
    window.history.forward(-1);    
</script>
<div class="content-container clearfix cai-table make-payment">
    <%--Nalini Issue 12734--%>
    <table id="tblMain" runat="server" style="height: 1px" class="data-form">
        <tr id="paymentMade" visible="false" runat="server">
            <td style="height: 30px; border-bottom: solid 2px gray">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
    </table>
    <div>
        <%--Suraj issue 14450 2/7/13  removed three step sorting ,added tooltip and added date column--%>
        <rad:RadGrid ID="grdMain" AutoGenerateColumns="False" runat="server" AllowPaging="false" EnableLinqExpressions="false"
            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
            AllowFilteringByColumn="true" CssClass="payment-data">
            <HeaderStyle CssClass="GridViewHeader" Font-Bold="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false" CssClass="payment-table-data" NoMasterRecordsText="No Payment Available.">
                <PagerStyle AlwaysVisible="true" />
                <Columns>
                    <rad:GridHyperLinkColumn DataNavigateUrlFields="ID" DataTextField="ID" HeaderText="Order #"
                        FilterControlWidth="80%" SortExpression="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />                                    
                    <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                        HeaderText="Date"  FilterControlWidth="270px" HeaderStyle-Width="270px" SortExpression="OrderDate" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="EqualTo" DataType="System.DateTime" ShowFilterIcon="false"
                        EnableTimeIndependentFiltering="true" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                    <rad:GridTemplateColumn HeaderText="Total" DataField="GrandTotal" SortExpression="GrandTotal"
                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob">
                        <ItemTemplate>                            
                            <asp:Label runat="server" Text="Order #:" CssClass="mobile-label"></asp:Label>
                            <a class="cai-table-data no-desktop" href="<%= Me.OrderConfirmationURL + "?ID=" %><%# DataBinder.Eval(Container.DataItem, "ID")%>"><%# DataBinder.Eval(Container.DataItem, "ID")%></a>
                            <br />
                            <asp:Label runat="server" Text="Order Date:" CssClass="mobile-label"></asp:Label>
                            <asp:Label class="cai-table-data no-desktop" ID="OrderDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:MMMM d, yyyy}")%>'></asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Total:" CssClass="mobile-label"></asp:Label>
                            <asp:Label ID="Label1" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container, "GrandTotal")%>'>
                            </asp:Label>
                            <br />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%#GetFormattedCurrency(Container, "GrandTotal")%>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle CssClass="griditempaddingRight" />
                        <HeaderStyle  Width="150px" />
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Balance" DataField="Balance" SortExpression="Balance"
                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Label runat="server" Text="Balance:" CssClass="mobile-label"></asp:Label>
                            <asp:Label CssClass="cai-table-data" ID="Label2" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>                           
                            <asp:TextBox ID="TextBox2" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle CssClass="griditempaddingRight" />
                        <HeaderStyle Width="150px" />
                    </rad:GridTemplateColumn>
                    <%--Suraj issue 14450 4/17/14, apply HeaderStyle--%>
                    <rad:GridTemplateColumn HeaderText="Pay Amount" DataField="Balance" SortExpression="Balance"
                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false">
                        <ItemTemplate>
                             <asp:Label runat="server" Text="Pay Amount:" CssClass="mobile-label"></asp:Label>
                            <input id="txtPayAmt" class="cai-table-data" type="text" size="23" runat="server"
                                value='<%#GetFormattedCurrency(Container, "Balance")%>' />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPayAmt"
                                ErrorMessage="Pay amount required" runat="server" Font-Size="8pt"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                          <ItemStyle CssClass="griditempaddingRight" />
                         <HeaderStyle  Width="200px" />
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Currency Symbol" Visible="false" DataField="CurrencySymbol"
                        FilterControlWidth="80%" SortExpression="CurrencySymbol" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCurrencySymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Currency Digits" Visible="false" DataField="NumDigitsAfterDecimal"
                        FilterControlWidth="80%" SortExpression="NumDigitsAfterDecimal" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Label ID="lblNumDigitsAfterDecimal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Actual Balance" Visible="false" DataField="Balance"
                        FilterControlWidth="80%" SortExpression="Balance" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" ItemStyle-CssClass="order-now">
                        <ItemTemplate>
                            <asp:Label ID="lblBalance" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Balance") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </rad:RadGrid>
        <asp:UpdatePanel ID="updPanelGrid" runat="server" UpdateMode="Always">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Label ID="lblNoRecords" runat="server" Visible="False"><strong>No unpaid orders exist for this account.</strong></asp:Label>
    </div>
    <table>
        <tr>
            <td>
          <%--  'Anil B change for 10737 on 13/03/2013
                'Set Credit Card ID to load property form Navigation Config--%>
                <uc1:CreditCard ID="CreditCard" runat="server"></uc1:CreditCard>
            </td>
        </tr>
        <tr>
            <td class="payment-submit">
                <asp:Button CssClass="submitBtn" ID="cmdPay" runat="server" Text="Make Payment">
                </asp:Button>&nbsp&nbsp;<asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <cc2:User ID="User1" runat="server" />
      <%--'Anil B Issue 10254 on 20/04/2013
             Added reference for shoping cart--%>
    <cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
</div>
