<%@ Control Language="VB" AutoEventWireup="false" CodeFile="OrderHistory_Admin__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.OrderHistory_Admin__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<style type="text/css">
    .rcbHeader ul, .rcbFooter ul, .rcbItem ul, .rcbHovered ul, .rcbDisabled ul
    {
        width: 100%;
        display: inline-block;
        margin: 0;
        padding: 0;
        list-style-type: none;
    }
    
    .col1
    {
        float: left;
        width: 220px;
        margin: 0;
        padding: 0 5px 0 0;
        line-height: 14px;
    }
    .col2, .col3
    {
        float: left;
        width: 110px;
        margin: 0;
        padding: 0 5px 0 0;
        line-height: 14px;
    }
    
    label, .selection-result
    {
        font: 13px 'Segoe UI' , Arial, sans-serif;
        color: #4888a2;
    }
    
    label
    {
        padding: 0 10px 0 0;
    }
    
    .button
    {
        vertical-align: middle;
        margin-left: 10px;
    }
    
    .selection-result
    {
        padding: 10px 0 10px 0;
        display: block;
    }
    
    div.bigModuleBottom
    {
        padding-top: 25px;
    }
    .multipleRowsColumns .rcbItem, .multipleRowsColumns .rcbHovered
    {
        float: left;
        margin: 0 1px;
        min-height: 13px;
        overflow: hidden;
        padding: 2px 19px 2px 6px;
        width: 125px;
    }
    html.rfdButton a.rfdSkinnedButton
    {
        vertical-align: middle;
        margin: 0 0 0 5px;
    }
    label
    {
        display: inline-block;
        width: 200px;
        text-align: right;
        padding-right: 5px;
        margin-top: 10px;
    }
    * html.rfdButton a.rfdSkinnedButton, * html.rfdButton input.rfdDecorated
    {
        vertical-align: top;
    }
</style>
<div class="content-container clearfix" id="divTop" runat="server">
    <asp:UpdatePanel ID="updPanelGrid" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblCompany" runat="server" Text="Company:" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddlCompanies" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <br /><br />
            <%--Suraj issue 14877 2/27/13  removed three step sorting ,added tooltip and added date column--%>
            <%--Amruta,Issue 15349 ,3/25/2013,Changed message from "No child records to display" to "Nothing to display" for child grid--%>
            <telerik:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                AllowMultiRowSelection="False" AllowPaging="True" GridLines="None" SortingSettings-SortedDescToolTip="Sorted Descending"
                SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn="true">
                               <%--<PagerStyle Mode="NumericPages"></PagerStyle>--%>
                <MasterTableView DataKeyNames="ID" AllowMultiColumnSorting="false" AllowNaturalSort="false" AllowFilteringByColumn="true">
                    <DetailTables>
                  <%--  Anil B for issue 14343 on 03/04/2013
                    Set Sorting option--%>
                        <telerik:GridTableView DataKeyNames="ID" Name="ChildGrid" Width="100%" runat="server" AllowNaturalSort="false" SortingSettings-SortedDescToolTip="Sorted Descending" 
                        AllowFilteringByColumn="false" NoDetailRecordsText="Nothing to display" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowPaging="false">
                            <Columns>
                                <rad:GridTemplateColumn HeaderText="Product" SortExpression="Product" DataField="Product">
                                    <ItemTemplate>
                                        <asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Product") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridBoundColumn DataField="ProductType" HeaderText="Type" SortExpression="ProductType" />
                                <rad:GridBoundColumn DataField="Description" HeaderText="Description" />
                                 <%-- Anil B for 14343 on 20-03-2013
                                    Remove Dataformat string--%>
                                <rad:GridBoundColumn DataField="Quantity" HeaderText="Quantity"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />                              
                                <rad:GridTemplateColumn   HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                HeaderText="Price" SortExpression="Price" DataField="Price"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                <asp:Label ID="lblDetailPrice" runat="server" Text='<%#GetFormattedCurrency(Container,"Price") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle Width="100px" />
                            </rad:GridTemplateColumn>

                            <rad:GridTemplateColumn   HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                HeaderText="Discount" SortExpression="Discount" DataField="Discount"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                <asp:Label ID="lblDetailPrice" runat="server" Text='<%#GetFormattedCurrency(Container,"Discount") %>'></asp:Label>
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
                                <asp:HyperLink ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"LinkUrl") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                      
                        <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                            HeaderText="Date" FilterControlWidth="90px" HeaderStyle-Width="90px" SortExpression="OrderDate"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                            ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                        <rad:GridBoundColumn DataField="BillToName" HeaderText="Bill to Person" AutoPostBackOnFilter="true"
                            FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                        <rad:GridBoundColumn DataField="ShipToName" HeaderText="Ship to Person" AutoPostBackOnFilter="true"
                            FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                        <rad:GridBoundColumn Visible="false" DataField="CurrencyType" HeaderText="Currency"
                            FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" />
                        <rad:GridBoundColumn DataField="OrderStatus" HeaderText="Shipping Status" AutoPostBackOnFilter="true"
                            FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                              <%-- Anil B for 14343 on 20-03-2013
                                    Change Header--%>
                        <rad:GridBoundColumn DataField="ShipType" HeaderText="Shipment Method" AutoPostBackOnFilter="true"
                            FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                        <rad:GridBoundColumn DataField="PayType" HeaderText="Payment Method" AutoPostBackOnFilter="true"
                            FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                        <rad:GridTemplateColumn FilterControlWidth="60px" HeaderStyle-HorizontalAlign="right"
                            HeaderText="Total" SortExpression="CALC_GrandTotal" DataField="CALC_GrandTotal"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCALC_GrandTotal" runat="server" Text='<%#GetFormattedCurrency(Container,"CALC_GrandTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                            <HeaderStyle Width="60px" />
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn FilterControlWidth="60px" HeaderStyle-HorizontalAlign="right"
                            HeaderText="Balance" SortExpression="balance" DataField="balance" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <asp:Label ID="lblbalance" runat="server" Text='<%#GetFormattedCurrency(Container,"balance") %>'></asp:Label>
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
