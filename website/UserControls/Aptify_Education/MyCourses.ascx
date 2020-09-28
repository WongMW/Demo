<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MyCourses.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.MyCoursesControl" %>
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
                SortingSettings-SortedAscToolTip="Sorted Ascending">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                    <Columns>
                        <rad:GridTemplateColumn HeaderText="Course" DataField="WebName"  SortExpression="WebName" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px" AllowFiltering="true">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ClassUrl") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Font-Size="10pt" />
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn HeaderText="Status"  DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="10pt" />
                        </rad:GridTemplateColumn>
                        <%--Suraj Issue 14829 4/29/13,  Remove time from grid --%>
                        <rad:GridDateTimeColumn DataField="DateRegistered" UniqueName="GridDateTimeColumnEndDate"
                        HeaderText="Date Registered"  FilterControlWidth="270px" HeaderStyle-Width="270px" SortExpression="DateRegistered" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="EqualTo" DataType="System.DateTime" ShowFilterIcon="false" DataFormatString="{0:MMMM dd, yyyy }"
                        EnableTimeIndependentFiltering="true" />
                        <rad:GridTemplateColumn  DataField="WebDescription" HeaderText="Description" SortExpression=""  AutoPostBackOnFilter="true" HeaderStyle-Width="500px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="170px" >
                            <ItemTemplate>
                                <asp:Label ID="lblWebDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="10pt" />
                        </rad:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </rad:RadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc1:User runat="server" ID="User1" />
</div>
