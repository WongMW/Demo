<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ViewCourse.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.ViewCourseControl" %>
<%@ Register Src="ClassScheduleControl.ascx" TagName="ClassScheduleControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <%--Nalini Issue 12436 date:01/12/2011--%>
    <asp:UpdatePanel ID="UppanelGrid" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblError" Visible="false">Security Access Error</asp:Label>
            <table id="tblMain" runat="server" class="data-form">
                <tr>
                    <td colspan="2">
                        <asp:HyperLink runat="server" ID="lnkCategory" Text="" CssClass="MeetingName" ToolTip="View other courses in this category" />
                        <asp:Label runat="server" ID="lblName" CssClass="MeetingName" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblDescription" />
                        <br />
                        <asp:Label runat="server" ID="lblPrice" />
                    </td>
                </tr>
                <tr>
                    <td class="EducationFormActionArea">
                        <table>
                            <tr>
                                <td>
                                    <img runat="server" id="imgGenInfoSmall" src="" alt="General Info" border="0" align="absmiddle" />
                                    <asp:HyperLink runat="server" ID="lnkGeneral" Text="General" ToolTip="View general information about the course" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img runat="server" id="imgScheduleSmall" src="" alt="Class Schedule" border="0"
                                        align="absmiddle" />
                                    <asp:HyperLink runat="server" ID="lnkSchedule" Text="Class Schedule" ToolTip="View upcoming classes offered" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img runat="server" id="imgSyllabusSmall" src="" alt="Syllabus" border="0" align="absmiddle" />
                                    <asp:HyperLink runat="server" ID="lnkSyllabus" Text="Syllabus" ToolTip="View a standard syllabus for the course" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img runat="server" id="imgPrereqSmall" src="" alt="Prerequisites" border="0" align="absmiddle" />
                                    <asp:HyperLink runat="server" ID="lnkPrerequisites" Text="Prerequisites" ToolTip="View a list of prerequisite courses that are required to be completed before registering for this course." /><br />
                                </td>
                            </tr>
                            <tr runat="server" id="trInstructors">
                                <td>
                                    <img runat="server" id="imgInstructorSmall" src="" alt="Instructors" border="0" align="absmiddle" />
                                    <asp:HyperLink runat="server" ID="lnkInstructors" Text="Instructors" ToolTip="View a list of instructors who actively teach this course" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img runat="server" id="imgLocationsSmall" src="" alt="Locations" border="0" align="absmiddle" />
                                    <asp:HyperLink runat="server" ID="lnkLocations" Text="Locations" ToolTip="View a list of locations where this course is taught" /><br />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td runat="server" id="tdExtContent" class="EducationFormRightArea">
                        <img runat="server" id="imgTitle" src="" alt="Course Information" border="0" align="absmiddle" />
                        <asp:Label runat="server" CssClass="MeetingDates" ID="lblTitle" /><br />
                        <asp:Label runat="server" ID="lblDetails" /><br />
                        <%--  Navin Prasad Issue 11032--%>
                        <%--  Neha Chnages for  Issue 14452--%>
                        <rad:RadGrid ID="grdSyllabus" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true" AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Item" DataField="WebName" SortExpression="WebName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" SortExpression=""
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWebDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" VerticalAlign="Top" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Type" DataField="Type" SortExpression="Type"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" VerticalAlign="Top" />
                                    </rad:GridTemplateColumn>
                                     <%-- Suraj S Issue 4/30/13 14452, remove the sorting and filtering--%>
                                    <rad:GridBoundColumn DataField="Duration" DataFormatString="{0:F0} min" HeaderText="Duration"
                                        AllowSorting="false"  AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" FilterControlWidth="80%" />
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                        <rad:RadGrid ID="grdPrerequisites" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true" AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Prerequisite" DataField="WebName" SortExpression="WebName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%">
                                        <HeaderStyle CssClass="radViewCoursesPrerequisitesColumn" />
                                        <ItemStyle CssClass="radViewCoursesPrerequisitesColumn" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"IDUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" SortExpression="" ShowFilterIcon="false" FilterControlWidth="80%">
                                        <HeaderStyle CssClass="radViewCoursesPrerequisitesDescColumn" />
                                        <ItemStyle CssClass="radViewCoursesPrerequisitesDescColumn" />
                                        <ItemTemplate>
                                            <asp:Literal ID="ltDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                        <rad:RadGrid ID="grdInstructors" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true" AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridBoundColumn DataField="Name" HeaderText="Instructor" SortExpression="Name"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%" />
                                    <rad:GridBoundColumn DataField="Location" HeaderText="Location" SortExpression="Location"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%" />
                                    <rad:GridTemplateColumn HeaderText="Email" DataField="Email1" SortExpression="Email1"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email1") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"Email1Url") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                        <rad:RadGrid ID="grdLocations" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true" AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" />
                                    <rad:GridBoundColumn DataField="Location" HeaderText="Location" SortExpression="Location"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%" />
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                        <asp:Panel runat="server" ID="pnlSchedule">
                            <uc1:ClassScheduleControl ID="ClassScheduleControl" runat="server" CourseVisible="false"
                                CategoryVisible="false" InstructorVisible="false" LocationVisible="true" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <cc3:User runat="server" ID="User1" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
