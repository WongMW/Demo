<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AdminPaymentSummary__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.AdminPaymentSummary__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--<div style="min-height: 25px">
    <asp:Label ID="lblshowmessage" Font-Bold="true" Text="Your payment was made successfully!"
        runat="server" ForeColor="blue"></asp:Label>
</div>--%>
<table style="width: 300px">
    <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr id="trReceiptNumber" runat="server" visible="false">
        <td align="left">
            <asp:Label ID="lblReceipt" Text="Receipt No :" ForeColor="#333333" Font-Size="13px" Font-Bold="true"
                runat="server"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblReceiptNo" runat="server" Font-Bold="true"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="lblPay" Text="Total Payment Amount :" ForeColor="#333333" Font-Size="13px" Font-Bold="true"
                runat="server"></asp:Label>
        </td>
        <td class="Paddingrightpayment" align="left">
            <asp:Label ID="lblTotalPay" runat="server" Text="" Font-Bold="true"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>

</table>

<%--Neha, issue 14456,03/15/13, added filtering for the all column--%>
<asp:UpdatePanel ID="UpdatePanelgrdOrderSummary" runat="server">
    <ContentTemplate>
        <rad:RadGrid ID="grdOrderSummary" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowSorting="true" PagerStyle-PageSizeLabelText="Records Per Page" SortingSettings-SortedDescToolTip="Sorted Descending"
            SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn="true">
            <PagerStyle CssClass="sd-pager" />
            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                <Columns>
                    <rad:GridBoundColumn HeaderText="ID" DataField="ID" Visible="false" ItemStyle-CssClass="IdWidth" />
                    <rad:GridTemplateColumn HeaderText="Name" DataField="Name" SortExpression="Name" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" AutoPostBackOnFilter="true">
                        <HeaderStyle Width="120px" />
                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign" Width="120px"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblMemberName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Order #" DataField="ID" SortExpression="ID" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" FilterControlWidth="100%" AutoPostBackOnFilter="true">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" CssClass="LeftAlign" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" Target="_blank" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridBoundColumn HeaderText="Product(s)" HeaderStyle-Width="100px" ItemStyle-Wrap="true"
                        FilterControlWidth="80%" ItemStyle-Width="100px" DataField="Product" SortExpression="Product"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                        ItemStyle-CssClass="LeftAlign gridAlign" />
                    <rad:GridBoundColumn HeaderText="Category" HeaderStyle-Width="100px" ItemStyle-Wrap="true"
                        FilterControlWidth="80%" ItemStyle-Width="100px" DataField="ProductCategory" SortExpression="ProductCategory"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                        ItemStyle-CssClass="LeftAlign gridAlign" />
                    <rad:GridTemplateColumn HeaderText="Total" HeaderStyle-CssClass="rightAlign" DataField="GrandTotal" SortExpression="GrandTotal" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" FilterControlWidth="100%" AutoPostBackOnFilter="true">
                        <ItemStyle CssClass="rightAlign" Width="60px" />
                        <HeaderStyle Width="60px" />
                        <ItemTemplate>
                            <asp:Label ID="lblGrandTotal" runat="server" Text='<%#GetFormattedCurrency(Container, "GrandTotal")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Balance" HeaderStyle-CssClass="rightAlign" DataField="Balance" SortExpression="Balance" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" FilterControlWidth="100%" AutoPostBackOnFilter="true">
                        <ItemStyle CssClass="rightAlign" Width="60px" />
                        <HeaderStyle Width="60px" />
                        <ItemTemplate>
                            <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Pay Amount" HeaderStyle-CssClass="rightAlign" DataField="Balance" SortExpression="PayAmount" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" FilterControlWidth="100%" AutoPostBackOnFilter="true">
                        <ItemStyle CssClass="rightAlign" Width="60px" />
                        <HeaderStyle Width="60px" />
                        <ItemTemplate>
                            <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#GetFormattedCurrency(Container, "PayAmount")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Currency Symbol" Visible="false" DataField="CurrencySymbol">
                        <ItemStyle Width="10px" />
                        <HeaderStyle Width="10px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCurrencySymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </rad:RadGrid>
        <span id="spnNote" runat="server" visible="false">
            <asp:Label ID="lblNote" runat="server" Text="Please Take Print if you choose Bill Me Later."></asp:Label></span>
    </ContentTemplate>
</asp:UpdatePanel>
<table>
    <tr>
        <td valign="top" align="left" style="padding-left: 8px">
            <br />
            <asp:Button CssClass="submitBtn" ID="cmdback" TabIndex="1" runat="server" Height="26px"
                Text="Back"></asp:Button>
            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="javascript:window.print();" Height="26px" CssClass="submitBtn" />
   <asp:Button CssClass="submitBtn" ID="btnReceipt" runat="server"  Text="Print Receipt"></asp:Button>
        </td>
    </tr>
</table>
<cc2:User runat="Server" ID="User1" />
