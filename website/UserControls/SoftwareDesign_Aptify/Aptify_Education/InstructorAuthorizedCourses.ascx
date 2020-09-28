<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/InstructorAuthorizedCourses.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.InstructorAuthorizedCoursesControl" %>
<%@ Register Src="InstructorValidator.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
    <div id="tblMain" runat="server" class="clearfix">

        <asp:Label runat="server" ID="lblName" Text="List of Authorized Courses" />

        <asp:UpdatePanel ID="updPanelGrid" runat="server">
            <ContentTemplate>
                <rad:RadGrid ID="grdCourses" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                    SortingSettings-SortedAscToolTip="Sorted Ascending" CssClass="cai-table mobile-table">
                    <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <rad:GridHyperLinkColumn DataTextField="WebName" DataNavigateUrlFields="ID" HeaderText="Course" SortExpression="WebName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-CssClass="cai-table-data" >
                                <ItemStyle CssClass="no-mob" />
                            </rad:GridHyperLinkColumn>
                            <rad:GridTemplateColumn HeaderText="Instructor Status" DataField="Status" SortExpression="Status"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Course:</span>
                                    <asp:HyperLink runat="server" cssclass="cai-table-data" NavigateUrl='<%# Eval("ID", "~/Education/ViewCourse.aspx?CourseID={0}")%>' Text='<%# Eval("WebName")%>'></asp:HyperLink>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Instructor Status" DataField="Status" SortExpression="Status"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Instructor Status:</span>
                                    <asp:Label ID="lblstatus" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" SortExpression=""
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Description:</span>
                                    <asp:Label ID="lblWebDescription" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="# Units" DataField="Units" SortExpression="Units" HeaderStyle-HorizontalAlign="right"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rdCourseUnit">
                                <ItemTemplate>
                                    <span class="mobile-label">Units:</span>
                                    <asp:Label ID="lblUnits" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Units") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="TotalPartDuration" HeaderText="Duration (min)" HeaderStyle-HorizontalAlign="right"
                                SortExpression="TotalPartDuration" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rdCourseDuration">
                                <ItemTemplate>
                                    <span class="mobile-label">Duration (min):</span>
                                    <asp:Label ID="lblUnits" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"TotalPartDuration", "{0:F0}") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
</div>
