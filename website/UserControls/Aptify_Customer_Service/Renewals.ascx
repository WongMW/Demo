<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Renewals.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.RenewalsControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                Use this page to renew your subscriptions and memberships
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSelections" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <%--  <tr>
            <td>
            <%--Nalini Issue 12734--%>
        <%--<asp:Button ID="Button1" runat="server" Text="Renew Selected Items" Width="135px"></asp:Button>
                <asp:Button ID="Button2" runat="server" Text="Cancel" Width="60px"></asp:Button> 
            </td>
        </tr>--%>--%>
        <tr>
            <td>
                <%-- Navin Prasad Issue 11032--%>
                <%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%>
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <%--Suraj issue 14450 2/7/13  removed three step sorting ,added tooltip and added date column--%>
                        <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="true">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowSorting="true" AllowFilteringByColumn="true" AllowNaturalSort="false">
               <%-- 'Suraj Issue 15287 4/9/13, if the grid dont have any record then grid should visible and it should show "No recors " msg--%>
                            <NoRecordsTemplate>
                                    No Renew Memberships and Subscriptions Available.
                                </NoRecordsTemplate>
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Renewal" AllowFiltering="false" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRenewal" runat="server" />
                                            <%-- Suraj Issue 14450 3/23/13 ,take a lable for maintain the SubscriptionID  --%>
                                            <asp:Label ID="lblSubscriptionID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="ProductID" Visible="False" SortExpression="ProductID"
                                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductID" Text='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="PurchaseType" Visible="False" SortExpression=" "
                                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseType" Text='<%# DataBinder.Eval(Container.DataItem,"PurchaseType") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Product" Visible="False" SortExpression="Product"
                                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem,"Product") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn HeaderText="ProductID" Visible="False" SortExpression="ProductID"
                                        FilterControlWidth="80%" />
                                    <rad:GridBoundColumn DataField="PurchaseType" HeaderText="Purchase Type" SortExpression="PurchaseType"
                                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" />
                                    <rad:GridBoundColumn DataField="Product" HeaderText="Product" SortExpression="Product"
                                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" />
                                    <rad:GridDateTimeColumn DataField="EndDate" UniqueName="GridDateTimeColumnEndDate"
                                        HeaderText="Paid Through" FilterControlWidth="170px" HeaderStyle-Width="170px"
                                        SortExpression="EndDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        DataType="System.DateTime" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button CssClass="submitBtn" ID="RenewButton" runat="server" Text="Renew Selected Items">
                </asp:Button>
                <asp:Button CssClass="submitBtn" ID="CancelButton" runat="server" Text="Cancel">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAdded" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <cc1:User ID="User1" runat="server" />
    <cc3:AptifyShoppingCart ID="ShoppingCart1" runat="Server" />
</div>
