<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AdvRateCards__c.ascx.vb"
    Inherits="AdvRateCards__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .auto-style1 {
        width: 100%;
    }
</style>
<div class="content-container clearfix">
    <div runat="server" id="tblMain">

        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Advertising Product :  "></asp:Label>

        <asp:DropDownList ID="cmbAdvertise" Visible="false" runat="server" Width="276px" Height="26px" AutoPostBack="true">
        </asp:DropDownList>
        <asp:Label ID="lblAdvertise" runat="server" Visible="false" Text=""></asp:Label>

        <asp:Panel ID="pnlDetails" Visible="false" runat="server">

            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Description :  "></asp:Label>

            <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>

            <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Start Date :  "></asp:Label>

            <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>

            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="End Date :  "></asp:Label>

            <asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label>

            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Agency Discount % :  "></asp:Label>

            <asp:Label ID="lblAgencyDiscount" runat="server" Text=""></asp:Label>

        </asp:Panel>

        <rad:RadGrid ID="grdAdvertising" AutoGenerateColumns="False" runat="server" SortingSettings-SortedDescToolTip="Sorted Descending"
            SortingSettings-SortedAscToolTip="Sorted Ascending" PageSize="5" Skin="Sunset" AllowFilteringByColumn="true"
            PagerStyle-PageSizeLabelText="Records Per Page">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowSorting="true" AllowNaturalSort="false" DataKeyNames="ID" EnableNoRecordsTemplate="true" AllowFilteringByColumn="true"
                ShowHeadersWhenNoRecords="false">
                <NoRecordsTemplate>
                    <div>
                        No Data to Display
                    </div>
                </NoRecordsTemplate>
                <Columns>
                    <rad:GridTemplateColumn HeaderText="Colour" DataField="Colour" SortExpression="Colour" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblColour" runat="server" Text='<%# Eval("Colour") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Size" DataField="Size" SortExpression="Size" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Frequency" DataField="Frequency" SortExpression="Frequency" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblFrequency" runat="server" Text='<%# Eval("Frequency") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Position" DataField="Position" SortExpression="Position" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Description" DataField="Description" SortExpression="Description" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Rate" DataField="Rate" SortExpression="Rate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblRate" runat="server" Text='<%# GetFormattedCurrency(Container, "Rate") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Base Units" DataField="BaseUnits" SortExpression="BaseUnits" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblBaseUnits" runat="server" Text='<%# Eval("BaseUnits") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Rate Per Add Unit" DataField="RatePerAddUnit"
                        SortExpression="RatePerAddUnit" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblRatePerAddUnit" runat="server" Text='<%# String.Format("{0:0.00}",Eval("RatePerAddUnit")) %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Unit Definition" DataField="UnitDefinition" SortExpression="UnitDefinition" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblUnitDefinition" runat="server" Text='<%# Eval("UnitDefinition") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Currency Symbol" Visible="false" DataField="CurrencySymbol"
                        FilterControlWidth="80%">
                        <ItemStyle Width="10px" />
                        <HeaderStyle Width="10px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCurrencySymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </rad:RadGrid>

    </div>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
</div>
<cc1:User ID="User1" runat="server" />
