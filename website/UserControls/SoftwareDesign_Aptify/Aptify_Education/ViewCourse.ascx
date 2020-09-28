<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/ViewCourse.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.ViewCourseControl" %>
<%@ Register Src="ClassScheduleControl.ascx" TagName="ClassScheduleControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <%--Nalini Issue 12436 date:01/12/2011--%>
    <asp:UpdatePanel ID="UppanelGrid" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblError" Visible="false">Security Access Error</asp:Label>
            <div id="tblMain" runat="server" class="data-form">

                <asp:HyperLink runat="server" ID="lnkCategory" Text="" CssClass="MeetingName" ToolTip="View other courses in this category" />
                <asp:Label runat="server" ID="lblName" CssClass="MeetingName" />
                <asp:Label runat="server" ID="lblDescription" />


                <div class="aptify-category-inner-side-nav">
                    <h6>Menu</h6>
                    <ul>
                        <li>
                            <img runat="server" id="imgGenInfoSmall" src="" alt="General Info" border="0" align="absmiddle" />
                            <asp:HyperLink runat="server" ID="lnkGeneral" Text="General" CssClass="aptify-category-link" ToolTip="View general information about the course" />
                        </li>
                        <li>
                            <img runat="server" id="imgScheduleSmall" src="" alt="Class Schedule" border="0"
                                align="absmiddle" />
                            <asp:HyperLink runat="server" ID="lnkSchedule" Text="Class Schedule" ToolTip="View upcoming classes offered" /></li>
                        <li>
                            <img runat="server" id="imgSyllabusSmall" src="" alt="Syllabus" border="0" align="absmiddle" />
                            <asp:HyperLink runat="server" ID="lnkSyllabus" Text="Syllabus" ToolTip="View a standard syllabus for the course" /></li>
                        <li>
                            <img runat="server" id="imgPrereqSmall" src="" alt="Prerequisites" border="0" align="absmiddle" />
                            <asp:HyperLink runat="server" ID="lnkPrerequisites" Text="Prerequisites" ToolTip="View a list of prerequisite courses that are required to be completed before registering for this course." />
                        </li>
                        <li runat="server" id="trInstructors">
                            <img runat="server" id="imgInstructorSmall" src="" alt="Instructors" border="0" align="absmiddle" />
                            <asp:HyperLink runat="server" ID="lnkInstructors" Text="Instructors" ToolTip="View a list of instructors who actively teach this course" />
                        </li>
                        <li>
                            <img runat="server" id="imgLocationsSmall" src="" alt="Locations" border="0" align="absmiddle" />
                            <asp:HyperLink runat="server" ID="lnkLocations" Text="Locations" ToolTip="View a list of locations where this course is taught" />
                        </li>
                    </ul>
                </div>

                <div runat="server" id="tdExtContent" class="EducationFormRightArea ">
                    <div class="cai-form">
                        <img runat="server" id="imgTitle" src="" alt="Course Information" border="0" align="absmiddle" style="display: none;" />
                        <asp:Label runat="server" CssClass="form-title" ID="lblTitle" />
                        <div class="cai-form-content cai-table mobile-table">
                            <asp:Label runat="server" ID="lblDetails" Text="No information" />
                            <rad:RadGrid ID="grdSyllabus" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                AllowFilteringByColumn="true" AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sorted Ascending">
                                <PagerStyle CssClass="sd-pager" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="Item" DataField="WebName" SortExpression="WebName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <span class="mobile-label">Item:</span>
                                                <asp:Label ID="lnkWebName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" SortExpression=""
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <span class="mobile-label">Description:</span>
                                                <asp:Label ID="lblWebDescription" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Type" DataField="Type" SortExpression="Type"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <span class="mobile-label">Type:</span>
                                                <asp:Label ID="lblType" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <%-- Suraj S Issue 4/30/13 14452, remove the sorting and filtering--%>
                                        <rad:GridTemplateColumn DataField="Duration" HeaderText="Duration"
                                            AllowSorting="false" AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <span class="mobile-label">Duration:</span>
                                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Duration", "{0:F0} min")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                            <rad:RadGrid ID="grdPrerequisites" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                AllowFilteringByColumn="true" AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sorted Ascending">
                                <PagerStyle CssClass="sd-pager" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="Prerequisite" DataField="WebName" SortExpression="WebName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <span class="mobile-label">Prerequisite:</span>
                                                <asp:HyperLink ID="lnkWebName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"IDUrl") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" SortExpression="" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <span class="mobile-label">Description:</span>
                                                <asp:Label ID="ltDescription" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                            <rad:RadGrid ID="grdInstructors" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                AllowFilteringByColumn="true" AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sorted Ascending">
                                <GroupingSettings CaseSensitive="false" />
                                <PagerStyle CssClass="sd-pager" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn DataField="Name" HeaderText="Instructor" SortExpression="Name"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" >
                                            <ItemTemplate>
                                                <span class="mobile-label">Name:</span>
                                                <asp:HyperLink CssClass="cai-table-data" runat="server" Text='<%# Eval("Name")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="Location" HeaderText="Location" SortExpression="Location"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" >
                                            <ItemTemplate>
                                                <span class="mobile-label">Location:</span>
                                                <asp:HyperLink CssClass="cai-table-data" runat="server" Text='<%# Eval("Location")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Email" DataField="Email1" SortExpression="Email1"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <span class="mobile-label">Email:</span>
                                                <asp:HyperLink ID="lnkEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email1") %>'
                                                    CssClass="cai-table-data" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"Email1Url") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                            <rad:RadGrid ID="grdLocations" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                AllowFilteringByColumn="true" AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sorted Ascending">
                                <PagerStyle CssClass="sd-pager" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn DataField="Name" HeaderText="Name" SortExpression="Name" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" >
                                              <ItemTemplate>
                                                <span class="mobile-label">Name:</span>
                                                <asp:HyperLink CssClass="cai-table-data" runat="server" Text='<%# Eval("Name")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="Location" HeaderText="Location" SortExpression="Location"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" >
                                              <ItemTemplate>
                                                <span class="mobile-label">Location:</span>
                                                <asp:HyperLink CssClass="cai-table-data" runat="server" Text='<%# Eval("Location")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                            <asp:Panel runat="server" ID="pnlSchedule">
                                <uc1:ClassScheduleControl ID="ClassScheduleControl" runat="server" CourseVisible="false"
                                    CategoryVisible="false" InstructorVisible="false" LocationVisible="true" />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <cc3:User runat="server" ID="User1" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
