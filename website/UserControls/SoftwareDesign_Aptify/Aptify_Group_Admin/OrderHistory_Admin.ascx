<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/OrderHistory_Admin.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.OrderHistory_Admin" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix cai-table order-history-admin" id="divTop" runat="server">
    <asp:UpdatePanel ID="updPanelGrid" runat="server">
        <ContentTemplate>
            <%-- <div>
                Filter By:
                <rad:RadComboBox ID="RadComboBox1" runat="server" Height="190px" Width="420px" EmptyMessage="Select a Person"
                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnClientItemsRequested="UpdateItemCountField"
                    OnDataBound="RadComboBox1_DataBound" OnItemDataBound="RadComboBox1_ItemDataBound"
                    OnItemsRequested="RadComboBox1_ItemsRequested" AutoPostBack="true" MarkFirstMatch="true">
                    <HeaderTemplate>
                        <ul>
                            <li class="col1">Name</li>
                            <li class="col2">ID</li>
                        </ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <ul>
                            <li class="col1">
                                <%# DataBinder.Eval(Container.DataItem, "FirstLast")%><br />
                                <%# DataBinder.Eval(Container.DataItem, "Title")%></li>
                            <li class="col2">
                                <%# DataBinder.Eval(Container.DataItem, "ID")%></li>
                        </ul>
                    </ItemTemplate>
                    <FooterTemplate>
                        A total of
                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                        items
                    </FooterTemplate>
                </rad:RadComboBox>
                <rad:RadComboBox ID="RadComboBox2" runat="server" Height="190px" Width="420px" EmptyMessage="Select a Product Category"
                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnClientItemsRequested="UpdateItemCountField"
                    OnDataBound="RadComboBox2_DataBound" OnItemDataBound="RadComboBox2_ItemDataBound"
                    OnItemsRequested="RadComboBox2_ItemsRequested" AutoPostBack="true" MarkFirstMatch="true">
                    <HeaderTemplate>
                        <ul>
                            <li class="col1">ProductCategory</li>
                        </ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <ul>
                            <li class="col1">
                                <%# DataBinder.Eval(Container.DataItem, "ProductCategory")%></li>
                        </ul>
                    </ItemTemplate>
                    <FooterTemplate>
                        A total of
                        <asp:Literal runat="server" ID="RadComboItemsCount1" />
                        items
                    </FooterTemplate>
                </rad:RadComboBox>
            </div>--%>
            <br />
            <%--Suraj issue 14877 2/27/13  removed three step sorting ,added tooltip and added date column--%>
            <%--Amruta,Issue 15349 ,3/25/2013,Changed message from "No child records to display" to "Nothing to display" for child grid--%>
            <telerik:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                AllowMultiRowSelection="False" AllowPaging="True" GridLines="None" SortingSettings-SortedDescToolTip="Sorted Descending"
                SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn="true" CssClass="admin-history-inner-table">
                <PagerStyle CssClass="sd-pager" />
                <MasterTableView DataKeyNames="ID" AllowMultiColumnSorting="false" AllowNaturalSort="false" AllowFilteringByColumn="true">
                    <DetailTables>
                        <%--  Anil B for issue 14343 on 03/04/2013
                    Set Sorting option--%>
                        <telerik:GridTableView DataKeyNames="ID" Name="ChildGrid" Width="100%" runat="server" AllowNaturalSort="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                            AllowFilteringByColumn="false" NoDetailRecordsText="Nothing to display" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowPaging="false">
                            <Columns>
                                <rad:GridTemplateColumn HeaderText="Product" SortExpression="Product" DataField="Product">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Order:" CssClass="mobile-label no-desktop main-title"></asp:Label>
                                        <asp:Label runat="server" Text="Product:" CssClass="mobile-label no-desktop"></asp:Label>
                                        <asp:Label ID="Product" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Product") %>'></asp:Label>
                                        <asp:Label runat="server" Text="Product Type:" CssClass="mobile-label no-desktop"></asp:Label>
                                        <asp:Label ID="Label6" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType")%>'></asp:Label>
                                        <asp:Label runat="server" Text="Description :" CssClass="mobile-label no-desktop"></asp:Label>
                                        <asp:Label ID="Label7" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>'></asp:Label>
                                        <asp:Label runat="server" Text="Quantity:" CssClass="mobile-label no-desktop"></asp:Label>
                                        <asp:Label ID="Label8" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridBoundColumn DataField="ProductType" HeaderText="Type" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" SortExpression="ProductType" />
                                <rad:GridBoundColumn DataField="Description" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" HeaderText="Description" />
                                <%-- Anil B for 14343 on 20-03-2013
                                    Remove Dataformat string--%>
                                <rad:GridBoundColumn DataField="Quantity" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" HeaderText="Quantity"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="Price" SortExpression="Price" DataField="Price"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Price:" CssClass="mobile-label no-desktop"></asp:Label>
                                        <asp:Label ID="lblDetailPrice" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container,"Price") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle Width="100px" />
                                </rad:GridTemplateColumn>

                                <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="Discount" SortExpression="Discount" DataField="Discount"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Discount:" CssClass="mobile-label no-desktop"></asp:Label>
                                        <asp:Label ID="lblDetailPrice" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container,"Discount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle Width="100px" />
                                </rad:GridTemplateColumn>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                    <Columns>
                        <rad:GridTemplateColumn HeaderText="Order#" HeaderButtonType="TextButton" SortExpression="ID"
                            DataField="ID" UniqueName="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                            FilterControlWidth="" HeaderStyle-Width="60px" ShowFilterIcon="false">
                            <ItemTemplate>
                                <asp:Label runat="server" Text="Order #:" CssClass="mobile-label no-desktop"></asp:Label>
                                <a class="cai-table-data" href="<%= Me.OrderConfirmationURL + "?ID=" %><%# DataBinder.Eval(Container.DataItem, "ID")%>"><%# DataBinder.Eval(Container.DataItem, "ID")%></a>
                                <asp:Label runat="server" Text="Order Date:" CssClass="mobile-label"></asp:Label>
                                <asp:Label class="cai-table-data no-desktop" ID="OrderDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:MMMM d, yyyy}")%>'></asp:Label>
                                <asp:Label runat="server" Text="Bill To Person:" CssClass="mobile-label"></asp:Label>
                                <asp:Label class="cai-table-data no-desktop" ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillToName")%>'></asp:Label>
                                <asp:Label runat="server" Text="Ship To Person:" CssClass="mobile-label"></asp:Label>
                                <asp:Label class="cai-table-data no-desktop" ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ShipToName")%>'></asp:Label>
                                <asp:Label runat="server" Text="Shipping Status:" CssClass="mobile-label"></asp:Label>
                                <asp:Label class="cai-table-data no-desktop" ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>'></asp:Label>
                                <asp:Label runat="server" Text="Shipping Method:" CssClass="mobile-label"></asp:Label>
                                <asp:Label class="cai-table-data no-desktop" ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ShipType")%>'></asp:Label>
                                <asp:Label runat="server" Text="Payment Method:" CssClass="mobile-label"></asp:Label>
                                <asp:Label class="cai-table-data no-desktop" ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PayType")%>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <%--   <telerik:GridHyperLinkColumn SortExpression="ID" HeaderText="Order #" HeaderButtonType="TextButton"
                            DataTextField="ID" UniqueName="ID"></telerik:GridHyperLinkColumn>--%>
                        <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                            HeaderText="Date" FilterControlWidth="" HeaderStyle-Width="90px" SortExpression="OrderDate"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                            ShowFilterIcon="false" EnableTimeIndependentFiltering="true" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                        <rad:GridBoundColumn DataField="BillToName" HeaderText="Bill to Person" AutoPostBackOnFilter="true"
                            FilterControlWidth="" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                        <rad:GridBoundColumn DataField="ShipToName" HeaderText="Ship to Person" AutoPostBackOnFilter="true"
                            FilterControlWidth="" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                        <rad:GridBoundColumn Visible="false" DataField="CurrencyType" HeaderText="Currency"
                            FilterControlWidth="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" />
                        <rad:GridBoundColumn DataField="OrderStatus" HeaderText="Shipping Status" AutoPostBackOnFilter="true"
                            FilterControlWidth="" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                        <%-- Anil B for 14343 on 20-03-2013
                                    Change Header--%>
                        <rad:GridBoundColumn DataField="ShipType" HeaderText="Shipment Method" AutoPostBackOnFilter="true"
                            FilterControlWidth="" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                        <rad:GridBoundColumn DataField="PayType" HeaderText="Payment Method" AutoPostBackOnFilter="true"
                            FilterControlWidth="" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                        <rad:GridTemplateColumn FilterControlWidth="" HeaderStyle-HorizontalAlign="right"
                            HeaderText="Total" SortExpression="CALC_GrandTotal" DataField="CALC_GrandTotal"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <asp:Label runat="server" Text="Total:" CssClass="mobile-label no-desktop"></asp:Label>
                                <asp:Label ID="lblCALC_GrandTotal" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container,"CALC_GrandTotal") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn FilterControlWidth="" HeaderStyle-HorizontalAlign="right"
                            HeaderText="Balance" SortExpression="balance" DataField="balance" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <asp:Label runat="server" Text="Balance:" CssClass="mobile-label no-desktop"></asp:Label>
                                <asp:Label ID="lblbalance" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container,"balance") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <%--<rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                AllowFilteringByColumn="true">
                <MasterTableView AllowFilteringByColumn="true">
                    <Columns>
                        <rad:GridHyperLinkColumn Text="ID" DataNavigateUrlFields="ID" DataTextField="ID"
                            HeaderText="Order #" />
                        <rad:GridBoundColumn DataField="OrderDate" HeaderText="Date" DataFormatString="{0:d}" />
                        <rad:GridBoundColumn DataField="CurrencyType" HeaderText="Currency" />
                        <rad:GridTemplateColumn HeaderText="Total" DataField="CALC_GrandTotal"> 
                            <ItemTemplate>
                                <asp:Label ID="lblCALC_GrandTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CALC_GrandTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Right" Width="60px" />
                        </rad:GridTemplateColumn>
                        <rad:GridBoundColumn DataField="OrderStatus" HeaderText="Shipping Status" />
                        <rad:GridBoundColumn DataField="ShipType" HeaderText="Shipping Type" />
                        <rad:GridBoundColumn DataField="ShipTrackingNum" HeaderText="Tracking #" />
                    </Columns>
                </MasterTableView>
            </rad:RadGrid>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<cc1:User runat="server" ID="User1" />
