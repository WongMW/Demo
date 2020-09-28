<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Forums/Search.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.SearchControl"
    Debug="true" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc1" TagName="ForumTree" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/ForumTree.ascx" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="lnkForumsHome" Text="Return to Forums"></asp:HyperLink>
                <table id="tblInner" runat="server">
                    <tr>
                        <td colspan="2">
                            <%--   Search Discussion Forums&nbsp;--%>
                            <asp:Label ID="lblError" runat="server" Visible="False">Please enter at least one search criteria below</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Forum:</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkAllForums" AutoPostBack="True" runat="server" Text=" All Forums"
                                Checked="True" CssClass="txtfontfamily"></asp:CheckBox>
                            <uc1:ForumTree ID="ForumTree" runat="server"></uc1:ForumTree>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Subject&nbsp;Containing:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" Width="200px" CssClass="txtfontfamily"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Body Containing:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBody" runat="server" Width="200px" CssClass="txtfontfamily"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Search for Postings in the last:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbRecency" runat="server" CssClass="txtfontfamily">
                                <asp:ListItem Value="1">1 Day</asp:ListItem>
                                <asp:ListItem Value="2">2 Days</asp:ListItem>
                                <asp:ListItem Value="5">5 Days</asp:ListItem>
                                <asp:ListItem Value="7">1 Week</asp:ListItem>
                                <asp:ListItem Value="14">2 Weeks</asp:ListItem>
                                <asp:ListItem Value="30" Selected="True">1 Month</asp:ListItem>
                                <asp:ListItem Value="90">3 Months</asp:ListItem>
                                <asp:ListItem Value="182">6 Months</asp:ListItem>
                                <asp:ListItem Value="273">9 Months</asp:ListItem>
                                <asp:ListItem Value="365">12 Months</asp:ListItem>
                                <asp:ListItem Value="-1">All Postings</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <%--Nalini Issue 12734--%>
                            <asp:Button CssClass="submitBtn" ID="cmdSearch" runat="server" Text="Search"></asp:Button>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblResults" class="data-form">
                    <tr>
                        <td colspan="2">
                            <b>Search Results</b><br />
                            <asp:Button CssClass="submitBtn" ID="cmdChangeSearch" runat="server" Text="Change Search"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--  Navin Prasad Issue 11032--%>
                            <%--Nalini Issue 12436 date:01/12/2011--%>
                            <asp:UpdatePanel ID="UppanelGrid" runat="server">
                                <ContentTemplate>
                                    <%--Suraj issue 14455 2/20/13  removed three step sorting ,added tooltip and added date column--%>
                                    <rad:RadGrid ID="grdResults" runat="server" Visible="False" AutoGenerateColumns="False"
                                        SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        AllowPaging="true" AllowFilteringByColumn="true" AllowSorting="true">
                                        <PagerStyle CssClass="sd-pager" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowSorting="true" AllowNaturalSort="false" AllowFilteringByColumn="true">
                                            <Columns>
                                                <rad:GridTemplateColumn HeaderText="Forum" DataField="Forum" SortExpression="Forum"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="150px"
                                                    FilterControlWidth="80%">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkForum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Forum") %>'
                                                            NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </rad:GridTemplateColumn>
                                                <rad:GridDateTimeColumn DataField="DateEntered" UniqueName="GridDateTimeColumnDateEntered"
                                                    HeaderText="Date" SortExpression="DateEntered" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="EqualTo" DataType="System.DateTime" ShowFilterIcon="false"
                                                    EnableTimeIndependentFiltering="true" FilterListOptions="VaryByDataType" HeaderStyle-Width="170px" FilterControlWidth="170px" />
                                                <rad:GridTemplateColumn HeaderText="Subject" DataField="Subject" SortExpression="Subject" HeaderStyle-Width="250px"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    FilterControlWidth="80%">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkSubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subject") %>'
                                                            NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrlSub") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </rad:GridTemplateColumn>
                                                <rad:GridBoundColumn DataField="Body" HeaderText="Body" AutoPostBackOnFilter="true"
                                                    ShowFilterIcon="false" AllowSorting="false" FilterControlWidth="80%" />
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
    </table>
    <cc4:User runat="server" ID="User1" />
</div>
