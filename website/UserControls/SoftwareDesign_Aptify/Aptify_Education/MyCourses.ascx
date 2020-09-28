<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/MyCourses.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.MyCoursesControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <asp:DropDownList runat="server" ID="cmbType" AutoPostBack="True">
        <asp:ListItem>Current/Future Courses</asp:ListItem>
        <asp:ListItem>Past Courses</asp:ListItem>
        <asp:ListItem>All Courses</asp:ListItem>
    </asp:DropDownList>
    <%-- 'Navin Prasad Issue 11032--%>
    <%--Nalini Issue 12436 date:01/12/2011--%>
    <asp:UpdatePanel ID="UppanelGrid" runat="server">
        <ContentTemplate>
            <%--Neha Changes for Issue 14452--%>
            <rad:RadGrid ID="grdMyCourses" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                SortingSettings-SortedAscToolTip="Sorted Ascending" CssClass="mobile-table cai-table">
                <GroupingSettings CaseSensitive="false" />
                <PagerStyle CssClass="sd-pager" />
                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                    <Columns>
                        <rad:GridTemplateColumn HeaderText="Course" DataField="WebName" SortExpression="WebName" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px" AllowFiltering="true">
                            <ItemTemplate>
                                <span class="mobile-label">Course:</span>
                                <asp:HyperLink ID="lnkWebName" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ClassUrl") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn HeaderText="Status" DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100px">
                            <ItemTemplate>
                                <span class="mobile-label">Status:</span>
                                <asp:Label ID="lblStatus" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <%--Suraj Issue 14829 4/29/13,  Remove time from grid --%>
                        <rad:GridTemplateColumn DataField="DateRegistered" UniqueName="GridDateTimeColumnEndDate"
                            HeaderText="Date Registered" HeaderStyle-Width="270px" SortExpression="DateRegistered" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="EqualTo" DataType="System.DateTime">
                            <ItemTemplate>
                                <span class="mobile-label">Date Registered:</span>
                                <asp:Label ID="lblWebDescription" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateRegistered", "{0:MMMM dd, yyyy }")%>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="WebDescription" HeaderText="Description" SortExpression="" AutoPostBackOnFilter="true" HeaderStyle-Width="500px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="170px">
                            <ItemTemplate>
                                <span class="mobile-label">Description:</span>
                                <asp:Label ID="lblWebDescription" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </rad:RadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc1:User runat="server" ID="User1" />
</div>
