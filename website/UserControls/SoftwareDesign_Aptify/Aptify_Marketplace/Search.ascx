<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Marketplace/Search.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.MarketPlace.Search"
    Debug="true" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblDisplay" runat="server" class="data-form">
        <tr id="trSearch" runat="server">
            <td>
                <table>
                    <tr>
                        <td>
                            <b>Listing Name Contains:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="txtfontfamily"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Vendor Name Contains:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVendor" runat="server" CssClass="txtfontfamily"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Description Contains:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="txtfontfamily"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Posted In The Last: </b>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbRecency" runat="server" Width="90px" CssClass="txtfontfamily">
                                <asp:ListItem Value="1">1 Day</asp:ListItem>
                                <asp:ListItem Value="2">2 Days</asp:ListItem>
                                <asp:ListItem Value="3">3 Days</asp:ListItem>
                                <asp:ListItem Value="5">5 Days</asp:ListItem>
                                <asp:ListItem Value="7">1 Week</asp:ListItem>
                                <asp:ListItem Value="14">2 Weeks</asp:ListItem>
                                <asp:ListItem Value="30">1 Month</asp:ListItem>
                                <asp:ListItem Value="60">2 Months</asp:ListItem>
                                <asp:ListItem Value="90">3 Months</asp:ListItem>
                                <asp:ListItem Value="180">6 Months</asp:ListItem>
                                <asp:ListItem Value="365">1 Year</asp:ListItem>
                                <asp:ListItem Value="730">2 Years</asp:ListItem>
                                <asp:ListItem Value="1095">3 Years</asp:ListItem>
                                <asp:ListItem Value="1460">4 Years</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button CssClass="submitBtn" ID="cmdSearch" Text="Search" runat="server"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trResults" runat="server">
            <td>
                <asp:Label ID="lblNoResults" runat="server" Visible="False">No Records match the search criteria.</asp:Label>

                <%--Rashmip Issue 14454--%>
                <asp:UpdatePanel ID="UppanelGrid" runat="server">
                    <ContentTemplate>
                        <%--Suraj issue 14454 2/8/13  removed three step sorting ,added tooltip --%>
                        <rad:RadGrid ID="grdListings" runat="server" AllowPaging="true" AutoGenerateColumns="False" AllowFilteringByColumn="True" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowSorting="true">
                            <PagerStyle CssClass="sd-pager" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowSorting="true" AllowFilteringByColumn="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Vendor" DataField="Company" SortExpression="Company" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkVendorURL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Company") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Listing" DataField="Name" SortExpression="Name" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <%--Suraj issue 14454 4/5/13  removed HeaderTooltip   --%>
                                    <rad:GridBoundColumn DataField="PlainTextDescription" HeaderText="Description" SortExpression=" " AllowSorting="false" HeaderTooltip="" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" FilterControlWidth="80%" />
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <cc1:WebUserActivity ID="WebUserActivity1" runat="server" WebModule="MarketPlace" />
</div>
