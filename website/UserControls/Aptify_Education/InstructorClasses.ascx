<%@ Control Language="VB" AutoEventWireup="false" CodeFile="InstructorClasses.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.InstructorClassesControl" %>
<%@ Register Src="InstructorValidator.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--Suraj Issue 14452,5/7/13 Remove the in line css which is not used any where --%>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <asp:DropDownList runat="server" ID="cmbType" AutoPostBack="True">
                    <asp:ListItem>Current Classes</asp:ListItem>
                    <asp:ListItem>Future Classes</asp:ListItem>
                    <asp:ListItem>Past Classes</asp:ListItem>
                    <asp:ListItem>All Classes</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%-- 'Navin Prasad Issue 11032--%>
                <%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%>
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <rad:RadGrid ID="grdMyClasses" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                            AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowSorting="true" NoMasterRecordsText="No records to display."
                                AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Course" DataField="WebName" SortExpression="WebName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ClassUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" Width="250px" />
                                    </rad:GridTemplateColumn>
                                    <%--Suraj Issue 14452,5/7/13 change the "FilterControlWidth" and "ItemStyle-Width" to maintaining the width of filter box for GridDateTimeColumn column --%>
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                                        Visible="True" HeaderText="Starts" DataField="StartDate" SortExpression="StartDate"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        DataType="System.DateTime" EnableTimeIndependentFiltering="true" ItemStyle-Width="170px"
                                        FilterControlWidth="170px">
                                    </rad:GridDateTimeColumn>
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnEndDate" AllowSorting="true"
                                        Visible="True" HeaderText="Ends" DataField="EndDate" SortExpression="EndDate"
                                        ReadOnly="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        DataType="System.DateTime" EnableTimeIndependentFiltering="true" ItemStyle-Width ="170px" FilterControlWidth="170px" >
                                    </rad:GridDateTimeColumn>
                                    <rad:GridTemplateColumn HeaderText="Type" DataField="Type" SortExpression="Type"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" Width="150px" />
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                        <asp:Label runat="server" ID="lblEmptyMsg" Text="No records to display." Visible="false"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
</div>
