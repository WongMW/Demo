<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Default.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.MarketPlace._Default" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                The MarketPlace is an interactive community that will allow you to access information
                about products and services available from the leaders in the industry. We work
                with a large number of industry leaders to provide this MarketPlace to simplify
                your search for vendors of the products and services that you need.
                <hr />
                <p>
                    <%--Nalini Issue 12734--%>
                    <asp:Button CssClass="submitBtn" ID="cmdNewLisitng" runat="server" Text="Create New Listing">
                    </asp:Button>
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tblDisplay" runat="server" width="100%">
                    <tr>
                        <td>
                            Browse By:
                            <br />
                            <asp:HyperLink ID="lnkBrowseAll" runat="server">All</asp:HyperLink><br />
                            <asp:HyperLink ID="lnkBrowseVendor" runat="server">Vendor</asp:HyperLink><br />
                            <asp:HyperLink ID="lnkBrowseListing" runat="server">Listing</asp:HyperLink><br />
                            <br />
                            <asp:Label ID="lblDisplayTitle" runat="server">Browse MarketPlace Listings</asp:Label><asp:ListBox
                                ID="lstBrowse" runat="server" Rows="1" AutoPostBack="True"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNoResults" runat="server" Visible="False">No Records match the search criteria.</asp:Label>
                            <asp:UpdatePanel ID="UppanelGrid" runat="server">
                                <ContentTemplate>
                                    <%--Suraj issue 14454 2/8/13  removed three step sorting ,added tooltip --%>
                                    <rad:RadGrid ID="grdListings" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                        AllowFilteringByColumn="true" AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                                        SortingSettings-SortedAscToolTip="Sorted Ascending">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                            <Columns>
                                                <rad:GridBoundColumn DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor"
                                                    FilterControlWidth="80%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" />
                                                 <%--Suraj issue 14454 2/12/13  use GridTemplateColumn instead of GridHyperLinkColumn due to the filteration problem--%>
                                                <rad:GridTemplateColumn HeaderText="Listing" DataField="Listing" SortExpression="Listing"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    FilterControlWidth="80%">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Listing") %>'
                                                            NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ListingURL") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </rad:GridTemplateColumn>
                                                 <%--Suraj issue 14454 4/5/13 ,removed HeaderTooltip and 4/24/13 ,remove the space between HeaderTooltip  --%>
                                                <rad:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression=" " AllowSorting="false" HeaderTooltip=""
                                                    FilterControlWidth="80%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" />
                                            </Columns>
                                        </MasterTableView>
                                    </rad:RadGrid>
                                </ContentTemplate>
                                <%-- <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="grdListings" EventName="PageIndexChanging" />
                                </Triggers>--%>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <cc1:WebUserActivity ID="WebUserActivity1" runat="server" WebModule="MarketPlace" />
</div>
