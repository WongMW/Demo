<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Customer_Service/Renewals.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.RenewalsControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <h6 class="mainTitle">Use this page to renew your subscriptions and memberships</h6>
    <div id="tblMain" runat="server" class="data-form cai-table">

        <asp:Label ID="lblSelections" runat="server" Visible="False"></asp:Label>

        <asp:UpdatePanel ID="updPanelGrid" runat="server">
            <ContentTemplate>
                <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                    AllowFilteringByColumn="false" CssClass="mobile-table">
                    <PagerStyle CssClass="sd-pager" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowSorting="true" AllowFilteringByColumn="false" AllowNaturalSort="false">
                        <NoRecordsTemplate>
                            No Renew Memberships and Subscriptions Available.
                        </NoRecordsTemplate>
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Renewal" AllowFiltering="false" FilterControlWidth="100%">
                                <ItemTemplate>
                                    <span class="mobile-label">Renewal:</span>
                                    <asp:CheckBox ID="chkRenewal" runat="server" CssClass="cai-table-data" />
                                    <asp:Label ID="lblSubscriptionID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="ProductID" Visible="False" SortExpression="ProductID"
                                FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">ProductID:</span>
                                    <asp:Label ID="lblProductID" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="PurchaseType" Visible="False" SortExpression=" "
                                FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">PurchaseType:</span>
                                    <asp:Label ID="lblPurchaseType" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"PurchaseType") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Product" Visible="False" SortExpression="Product"
                                FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Product:</span>
                                    <asp:Label ID="lblProductName" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Product") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="ProductID" Visible="False" SortExpression="ProductID"
                                FilterControlWidth="100%">
                                <ItemTemplate>
                                    <span class="mobile-label">ProductID:</span>
                                    <asp:Label CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="PurchaseType" HeaderText="Purchase Type" SortExpression="PurchaseType"
                                FilterControlWidth="100%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">PurchaseType:</span>
                                    <asp:Label CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"PurchaseType") %>'
                                        runat="server" />
                                    <span class="mobile-label">Paid Through:</span>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>

                            <rad:GridDateTimeColumn DataField="EndDate" UniqueName="GridDateTimeColumnEndDate"
                                HeaderText="Paid Through"
                                SortExpression="EndDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                DataType="System.DateTime" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" ItemStyle-CssClass="cai-table-data" />

                            <rad:GridTemplateColumn DataField="Product" HeaderText="Product" SortExpression="Product"
                                FilterControlWidth="100%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Product:</span>
                                    <asp:Label CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Product") %>'
                                        runat="server" />

                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Label ID="lblAdded" runat="server" Visible="False"></asp:Label>

    </div>

    <div class="actions">
        <asp:Button CssClass="submitBtn" ID="RenewButton" runat="server" Text="Renew Selected Items"></asp:Button>
        <asp:Button CssClass="submitBtn" ID="CancelButton" runat="server" Text="Cancel"></asp:Button>
    </div>

    <cc1:User ID="User1" runat="server" />
    <cc3:AptifyShoppingCart ID="ShoppingCart1" runat="Server" />
</div>
