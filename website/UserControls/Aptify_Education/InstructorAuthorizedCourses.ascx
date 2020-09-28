<%@ Control Language="VB" AutoEventWireup="false" CodeFile="InstructorAuthorizedCourses.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.InstructorAuthorizedCoursesControl" %>
<%@ Register Src="InstructorValidator.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label><br />
    <table id="tblMain" runat="server">
        <tr>
            <td colspan="2" class="BottomBorder">
                <asp:Label runat="server" Font-Bold="true" Font-Italic="true" Font-Size="12pt" ID="lblName"
                    Text="List of Authorized Courses" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%--  'Navin Prasad Issue 11032--%>
                <%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%>
                <%-- Grid formate change by Amruta IssueId:13281 --%>
                <%-- Amruta IssueId:13281 Column change from description to webdescription --%>
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <%--Neha Changes for Issue 14452--%>
                        <rad:RadGrid ID="grdCourses" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridHyperLinkColumn DataTextField="WebName" DataNavigateUrlFields="ID" HeaderText="Course"
                                        DataNavigateUrlFormatString="~/Education/{0}" SortExpression="WebName" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="260px" />
                                    <rad:GridTemplateColumn HeaderText="Instructor Status" DataField="Status" SortExpression="Status"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="120px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" SortExpression=""
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="240px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWebDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="# Units"  DataField="Units" SortExpression="Units" HeaderStyle-HorizontalAlign="right"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="120px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rdCourseUnit">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnits" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Units") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn DataField="TotalPartDuration" HeaderText="Duration (min)" DataFormatString="{0:F0}" HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Font-Size="12px" SortExpression="TotalPartDuration" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" FilterControlWidth="120px"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rdCourseDuration" />
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
</div>
