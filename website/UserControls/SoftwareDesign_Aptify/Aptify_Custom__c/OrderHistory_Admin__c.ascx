<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/OrderHistory_Admin__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.OrderHistory_Admin__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix" id="divTop" runat="server">
  <p>
        <asp:Button ID="btnPrint" runat="server" CssClass="submitBtn" Text="Get Statement" Visible="false" />
    </p>
    <asp:UpdatePanel ID="updPanelGrid" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblCompany" runat="server" Text="Company:" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddlCompanies" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <br />
            <br />

            <telerik:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                AllowMultiRowSelection="False" AllowPaging="True" GridLines="None" SortingSettings-SortedDescToolTip="Sorted Descending"
                SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn="true" CssClass="cai-table mobile-table">
                <PagerStyle CssClass="sd-pager" />

                <MasterTableView DataKeyNames="ID" AllowMultiColumnSorting="false" AllowNaturalSort="false" AllowFilteringByColumn="false">
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="ID" Name="ChildGrid" Width="100%" runat="server" AllowNaturalSort="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                            AllowFilteringByColumn="false" NoDetailRecordsText="Nothing to display" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowPaging="false">
                            <Columns>
                                <rad:GridTemplateColumn HeaderText="Product" SortExpression="Product" DataField="Product">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product:</span>
                                        <asp:Label CssClass="cai-table-data" ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Product") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn DataField="ProductType" HeaderText="Type" SortExpression="ProductType">
                                    <ItemTemplate>
                                        <span class="mobile-label">Type:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("ProductType")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn DataField="Description" HeaderText="Description">
                                    <ItemTemplate>
                                        <span class="mobile-label">Description:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn DataField="Quantity" HeaderText="Quantity"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <span class="mobile-label">Quantity:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>

                                <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="Discount" SortExpression="Discount" DataField="Discount"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <span class="mobile-label">Discount:</span>
                                        <asp:Label ID="lblDetailPrice" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container,"Discount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle Width="100px" />
                                </rad:GridTemplateColumn>

                                <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="Price" SortExpression="Price" DataField="Price"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <span class="mobile-label">Price:</span>
                                        <asp:Label ID="lblDetailPrice" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container,"Price") %>'></asp:Label>
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
                            FilterControlWidth="60px" HeaderStyle-Width="60px" ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">Order#:</span>
                                <asp:HyperLink ID="Product" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"LinkUrl") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                            HeaderText="Date" FilterControlWidth="90px" HeaderStyle-Width="90px" SortExpression="OrderDate"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                            ShowFilterIcon="false" >
                              <ItemTemplate>
                                <span class="mobile-label">Date:</span>
                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("OrderDate")%>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="BillToName" HeaderText="Bill to person" AutoPostBackOnFilter="true"
                            FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                            <ItemTemplate>
                                <span class="mobile-label">Bill to person:</span>
                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("BillToName")%>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="ShipToName" HeaderText="Ship to person" AutoPostBackOnFilter="true"
                            FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                             <ItemTemplate>
                                <span class="mobile-label">Ship to person:</span>
                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("ShipToName")%>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn Visible="false" DataField="CurrencyType" HeaderText="Currency"
                            FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" >
                            <ItemTemplate>
                                <span class="mobile-label">Currency type:</span>
                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("CurrencyType")%>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="OrderStatus" HeaderText="Shipping status" AutoPostBackOnFilter="true"
                            FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                            <ItemTemplate>
                                <span class="mobile-label">Order status:</span>
                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("OrderStatus")%>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="ShipType" HeaderText="Shipment method" AutoPostBackOnFilter="true"
                            FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false" Visible="false">
                              <ItemTemplate>
                                <span class="mobile-label">Shipment method:</span>
                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("ShipType")%>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="PayType" HeaderText="Payment method" AutoPostBackOnFilter="true"
                            FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                              <ItemTemplate>
                                <span class="mobile-label">Payment method:</span>
                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("PayType")%>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn FilterControlWidth="60px" HeaderStyle-HorizontalAlign="right"
                            HeaderText="Total" SortExpression="CALC_GrandTotal" DataField="CALC_GrandTotal"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">Total:</span>
                                <asp:Label ID="lblCALC_GrandTotal" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container,"CALC_GrandTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                            <HeaderStyle Width="60px" />
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn FilterControlWidth="60px" HeaderStyle-HorizontalAlign="right"
                            HeaderText="Balance" SortExpression="balance" DataField="balance" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">Balance:</span>
                                <asp:Label ID="lblbalance" runat="server" CssClass="cai-table-data" Text='<%#GetFormattedCurrency(Container,"balance") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                            <HeaderStyle Width="60px" />
                        </rad:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<cc1:User runat="server" ID="User1" />
