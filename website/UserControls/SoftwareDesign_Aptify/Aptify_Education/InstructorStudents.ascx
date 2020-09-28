<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/InstructorStudents.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.InstructorStudentsControl" %>
<%@ Register Src="InstructorValidator.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <asp:DropDownList ID="cmbDate" runat="server" AutoPostBack="true">
                    <asp:ListItem Selected="true" Value="*" Text="Current Students"></asp:ListItem>
                    <asp:ListItem Value="-" Text="Prior Students"></asp:ListItem>
                    <asp:ListItem Value="+" Text="Future Students"></asp:ListItem>
                    <asp:ListItem Value="-6" Text="Last 6 Months"></asp:ListItem>
                    <asp:ListItem Value="-12" Text="Last 12 Months"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Next 6 Months"></asp:ListItem>
                    <asp:ListItem Value="12" Text="Next 12 Months"></asp:ListItem>
                    <asp:ListItem Value="" Text="All Dates"></asp:ListItem>
                </asp:DropDownList>
                <%-- Suraj Issue 14452,5/3/13 remove the Course dropdown --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="cai-table">
                <%--'Navin Prasad Issue 11032--%>
                <%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%>
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <%--Neha Changes for Issue 14452--%>
                        <rad:RadGrid ID="grdStudents" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowSorting="true" AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending" CssClass="instructor-table">
                            <PagerStyle CssClass="sd-pager" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Course" DataField="Course" AutoPostBackOnFilter="true"
                                        SortExpression="Course" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Course:</span>
                                            <asp:HyperLink ID="lnkCourse" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Course") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ClassUrl") %>' CssClass="cai-table-data"></asp:HyperLink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnEndDate" DataField="StartDate"
                                        AllowSorting="true" Visible="True" HeaderText="Date" SortExpression="StartDate"
                                        ReadOnly="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        DataType="System.DateTime" EnableTimeIndependentFiltering="true" ItemStyle-CssClass="no-mob">
                                    </rad:GridDateTimeColumn>
                                    <rad:GridTemplateColumn HeaderText="Last" DataField="LastName" AutoPostBackOnFilter="true"
                                        SortExpression="LastName" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Last:</span>
                                            <asp:Label ID="lblLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>' CssClass="cai-table-data"></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="First" DataField="FirstName" AutoPostBackOnFilter="true"
                                        SortExpression="FirstName" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">First:</span>
                                            <asp:Label ID="lblFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>' CssClass="cai-table-data"></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Email" DataField="Email1" AutoPostBackOnFilter="true"
                                        SortExpression="Email1" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Email:</span>
                                            <asp:HyperLink ID="lnkMail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email1") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MailUrl") %>' CssClass="cai-table-data"></asp:HyperLink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Status" DataField="Status" AutoPostBackOnFilter="true"
                                        SortExpression="Status" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Status:</span>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>' CssClass="cai-table-data"></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Score" DataField="Score" AutoPostBackOnFilter="true"
                                        SortExpression="Score" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <span class="mobile-label">Score:</span>
                                            <asp:Label ID="lblScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Score") %>' CssClass="cai-table-data"></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
        </tr>
    </table>
    <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
</div>
