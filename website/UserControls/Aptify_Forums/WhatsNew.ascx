<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WhatsNew.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.WhatsNewControl" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <table id="tblInner" width="100%">
                    <tr>
                        <td>
                            <span class="Title">
                                <asp:Label ID="lblDiscussionForum" runat="server"></asp:Label></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PnlPosting" runat="server">
                                Show postings in the last
                                <asp:DropDownList ID="cmbRecency" runat="server">
                                    <asp:ListItem Value="1" Selected="True">1 Day</asp:ListItem>
                                    <asp:ListItem Value="2">2 Days</asp:ListItem>
                                    <asp:ListItem Value="5">5 Days</asp:ListItem>
                                    <asp:ListItem Value="7">1 Week</asp:ListItem>
                                    <asp:ListItem Value="14">2 Weeks</asp:ListItem>
                                    <asp:ListItem Value="30">1 Month</asp:ListItem>
                                    <asp:ListItem Value="90">3 Months</asp:ListItem>
                                </asp:DropDownList>
                                <%--Nalini Issue 12734--%>
                                <asp:Button ID="cmdRefresh" runat="server" Text="Refresh" CssClass="submitBtn"></asp:Button></asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--Navin Prasad Issue 11032--%>
                            <%--Nalini Issue 12436 date:01/12/2011--%>
                            <%--Amruta Issue 14950 date:16/11/2012--%>
                            <asp:UpdatePanel ID="UppanelGrid" runat="server">
                                <ContentTemplate>
                                    <%--Suraj issue 14455 2/19/13  removed three step sorting ,added tooltip and added date column--%>
                                    <rad:RadGrid ID="grdResults" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                        SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        AllowFilteringByColumn="true">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowSorting="true" AllowNaturalSort="false" AllowFilteringByColumn="true">
                                            <Columns>
                                                <rad:GridTemplateColumn HeaderText="Forum" DataField="Forum" SortExpression="Forum"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    FilterControlWidth="80%" HeaderStyle-Width="350px">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkForum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Forum") %>'
                                                            NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </rad:GridTemplateColumn>
                                                <rad:GridDateTimeColumn DataField="MostRecentPostingDate" UniqueName="GridDateTimeColumnMostRecentPostingDate"
                                                    HeaderText="Most Recent" FilterControlWidth="170px" HeaderStyle-Width="170px"
                                                    SortExpression="MostRecentPostingDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                    DataType="System.DateTime" ShowFilterIcon="false" EnableTimeIndependentFiltering="true"
                                                    FilterListOptions="VaryByDataType" />
                                                <rad:GridBoundColumn DataField="NumNewPostings" HeaderText="# New Postings" FilterControlWidth="98%"  HeaderStyle-Width="180px"
                                                    ItemStyle-HorizontalAlign="Right" SortExpression="NumNewPostings" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Right" />
                                            </Columns>
                                        </MasterTableView>
                                    </rad:RadGrid>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <cc1:User ID="User1" runat="server" />
</div>
