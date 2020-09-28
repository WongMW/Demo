<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.EducationDetails__c"
    CodeFile="~/UserControls/Aptify_Custom__c/EducationDetails__c.ascx.vb" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<script src="../../Scripts/expand.js" type="text/javascript"></script>
<script type="text/javascript">
    function CollapseExpand(me, HiddenPanelState) {
        var Panelstate = $('#' + me).attr("class");
        $('#' + me).slideToggle('slow');
        if (Panelstate == "collapse") {
            $('#' + me).removeClass("collapse").addClass("active");
            SetPanelState(HiddenPanelState, 1)
        }
        else {
            $('#' + me).removeClass("active").addClass("collapse");
            SetPanelState(HiddenPanelState, 0)
        }
    }
    function SetPanelState(HiddenPanelState, StateValue) {
        if (HiddenPanelState == 'hdnfirst') {
            $("#<%=hdnfirst.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnEnrolledRevision') {
            $("#<%=hdnEnrolledRevision.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnEnrolledInterim') {
            $("#<%=hdnEnrolledInterim.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnEnrolledMockExams') {
            $("#<%=hdnEnrolledMockExams.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnEnrolledExams') {
            $("#<%=hdnEnrolledExams.clientID %>").val(StateValue);
        }
    }

    function CollapseAll(hidnID) {
        //For First Div
        var FirstPanel = $("#first").attr("class");
        $("#first").show('slow');
        $("#first").removeClass("collapse").addClass("active");
        SetPanelState('hdnfirst', 1)

        //For second Div
        var SecondPanel = $("#divEnrolledRevision").attr("class");
        $("#divEnrolledRevision").show('slow');
        $("#divEnrolledRevision").removeClass("collapse").addClass("active");
        SetPanelState('hdnEnrolledRevision', 1)

        //For Third Div
        var ThirdPanel = $("#divEnrolledInterim").attr("class");
        $("#divEnrolledInterim").show('slow');
        $("#divEnrolledInterim").removeClass("collapse").addClass("active");
        SetPanelState('hdnEnrolledInterim', 1)

        //For Third Div
        var FourthPanel = $("#divEnrolledMockExams").attr("class");
        $("#divEnrolledMockExams").show('slow');
        $("#divEnrolledMockExams").removeClass("collapse").addClass("active");
        SetPanelState('hdnEnrolledMockExams', 1)

        //For Fourth Div
        var FourthPanel = $("#divEnrolledExams").attr("class");
        $("#divEnrolledExams").show('slow');
        $("#divEnrolledExams").removeClass("collapse").addClass("active");
        SetPanelState('hdnEnrolledExams', 1)
    }

    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }

</script>

<div class="content-container clearfix">
    <div>
        <span class="label-title-inline">Name:</span>
        <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label>
        <span class="label-title-inline">Student Number: </span>
        <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label>

        <div class="actions">
            <asp:RadioButton ID="rdoCurrentAcademicCycle" runat="server" GroupName="AcadmicCycle"
                Text="Current Academic Cycle: " AutoPostBack="true" Checked="true" />
            <asp:RadioButton ID="rdoNextAcadmicCycle" runat="server" GroupName="AcadmicCycle"
                Text="Next Academic Cycle: " AutoPostBack="true" />
        </div>

        <div class="actions">
            <asp:HyperLink ID="HyperLink1" runat="server" class="expand btn" onclick="CollapseAll(this)">Expand All</asp:HyperLink>
        </div>

         <div align="right">
            <asp:LinkButton ID="lnkClassSchedule" runat="server" Text="Class Schedule"></asp:LinkButton>
        </div>

        <div class="demo cai-form">
            <h2 class="expand form-title" onclick="CollapseExpand('first','hdnfirst')">Enrolled Courses</h2>
            <div id="first" class="collapse cai-form-content cai-table mobile-table">
                <asp:Repeater ID="rpEnrolledCourses" runat="server">
                    <ItemTemplate>
                        <span class="label-title">
                            <span>Curriculum:</span>
                            <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                            <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server" Visible="false" />
                        </span>

                        <table>
                            <asp:Repeater ID="rpEnrolledCoursesDetails" runat="server" OnItemCommand="rptSession_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>Course</th>
                                            <th>Location</th>
                                            <th>Group</th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <span class="mobile-label">Course:</span>
                                            <asp:LinkButton ID="lbtnCourse" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                CommandArgument='<%# Eval("ClassID") %>' CssClass="cai-table-data" CommandName="CourseClick" Font-Underline="true"></asp:LinkButton>
                                            <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Location:</span>
                                            <asp:Label ID="Label1" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Group:</span>
                                            <asp:Label ID="Label2" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>

                <telerik:RadGrid ID="grdEnrolledCourses" runat="server" AutoGenerateColumns="False"
                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                    AllowFilteringByColumn="false" Visible="false">
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="Curriculum"></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="Curriculum"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression="ID"
                                DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" Visible="False" />
                            <telerik:GridBoundColumn DataField="Name" HeaderText="Course" SortExpression="Name"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Venue" HeaderText="Location" SortExpression="Venue"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Group" HeaderText="Group" SortExpression="Group"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:Label ID="lblErrorEnrolledCourse" runat="server" Text=""></asp:Label>
            </div>

            <h2 class="expand form-title" onclick="CollapseExpand('divEnrolledRevision','hdnEnrolledRevision')">Enrolled Revision Courses</h2>
            <div class="collapse cai-form-content cai-table mobile-table" id="divEnrolledRevision">
                <table>
                    <asp:Repeater ID="rptRevisionsCourses" runat="server">
                        <HeaderTemplate>
                            <thead>
                                <tr>
                                    <th>Course</th>
                                    <th>Location</th>
                                    <th>Group</th>
                                </tr>
                            </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <th colspan="3">
                                <span>Curriculum:</span>
                                <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server" Visible="false" />
                            </th>

                            <asp:Repeater ID="rpRevisionSummer" runat="server" OnItemCommand="rptSession_ItemCommand">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                            <span>Session:</span>
                                            <asp:Label ID="lblSummerSession" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'
                                                runat="server" />
                                            <asp:Label ID="lblSessionIDItem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SessionID") %>'
                                                Visible="false"></asp:Label>
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <span class="mobile-label">Course:</span>
                                            <asp:LinkButton ID="lbtnCourse" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                CommandArgument='<%# Eval("ID") %>' CssClass="cai-table-data" CommandName="CourseClick" Font-Underline="true"></asp:LinkButton>
                                            <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Location:</span>
                                            <asp:Label ID="Label1" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Venue") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Group:</span>
                                            <asp:Label ID="Label2" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                            <asp:Repeater ID="rpRepeatRevision" runat="server" OnItemCommand="rptSession_ItemCommand">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                            <span>Session:</span>
                                            <asp:Label ID="lblH1" Text='<%# Eval("Session") %>' runat="server" />
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <span class="mobile-label">Course:</span>
                                            <asp:LinkButton ID="lbtnCourse" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                CommandArgument='<%# Eval("ID") %>' CommandName="CourseClick" Font-Underline="true"></asp:LinkButton>
                                            <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Venue:</span>
                                            <asp:Label ID="Label1" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Venue") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Group:</span>
                                            <asp:Label ID="Label2" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Label ID="lblRevisionError" runat="server" Text=""></asp:Label>
            </div>

            <h2 class="expand form-title" onclick="CollapseExpand('divEnrolledInterim','hdnEnrolledInterim')">Enrolled Interim Assessments</h2>
            <div class="collapse cai-form-content cai-table" id="divEnrolledInterim">
                <table class="clearfix">
                    <asp:Repeater ID="rpIntrimAssessment" runat="server">
                        <HeaderTemplate>
                            <thead>
                                <tr>
                                    <th>Course</th>
                                    <th>Location</th>
                                    <th>Group</th>
                                    <th>Start Time</th>
                                    <th>Exam Number</th>
                                    <th>Change Location</th>
                                </tr>
                            </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th colspan="6">
                                    <span>Curriculum:</span>
                                    <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                    <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                        Visible="false" />
                                </th>
                            </tr>
                            <asp:Repeater ID="rpIntrimAssessmentDetails" runat="server" OnItemCommand="rpIntrimAssessmentDetails_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th colspan="6">
                                                <span>Session:</span>
                                                <asp:Label ID="lblSummerSession" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'
                                                    runat="server" />
                                                <asp:Label ID="lblSessionIDItem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SessionID") %>'
                                                    Visible="false"></asp:Label>
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <span class="mobile-label">Course:</span>
                                            <asp:Label ID="lblChapterLocation" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                Visible="True"></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Location:</span>
                                            <asp:Label ID="Label1" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Group:</span>
                                            <asp:Label ID="Label2" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Start Time:</span>
                                            <asp:Label ID="Label3" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, " StartTime") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Exam Number:</span>
                                            <asp:Label ID="lblIntrimExamNumber" runat="server" CssClass="cai-table-data" Text='<%# Eval("ExamNumber") %>'></asp:Label>
                                            <asp:Label ID="lblIntrimCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblIntrimSessionID" runat="server" Text='<%# Eval("SessionID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblStudentGroup" runat="server" Text='<%# Eval("StudentGroupID") %>'
                                                Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Change Location:</span>
                                            <asp:TextBox ID="txtDisplayLinkField" runat="server" Text='<%# Eval("ISDisplayChangeLocation") %>'
                                                Visible="false" />
                                            <asp:Label ID="lblIsBigFirm" runat="server" Text='<%# Eval("IsBigFirm") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSessionName" runat="server" Text='<%# Eval("Session") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lnkInterimUpdateGroup" runat="server" CssClass="cai-table-data" Text="Change Location"
                                                CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("ID")  & ","  & Eval("CourseID") %>'
                                                OnClientClick="CustomToggle();"> </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>


                            <asp:Repeater ID="rpResitAssessmentDetails" runat="server" OnItemCommand="rpResitAssessmentDetails_ItemCommand">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <th>
                                                <span>Session:</span>
                                                <asp:Label ID="lblH1" Text='<%# Eval("Session") %>' runat="server" />
                                            </th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <span class="mobile-label">Course:</span>
                                                <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>' Visible="True"></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Location:</span>
                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Group:</span>
                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Start Time:</span>
                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, " StartTime") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Exam Number:</span>
                                                <asp:Label ID="lblResitExamNumber" runat="server" Text='<%# Eval("ExamNumber") %>'></asp:Label>
                                                <asp:Label ID="lblResitCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblResitSessionID" runat="server" Text='<%# Eval("SessionID") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblStudentGroup" runat="server" Text='<%# Eval("StudentGroupID") %>'
                                                    Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Change Location:</span>
                                                <asp:LinkButton ID="lnkInterimUpdateGroup" runat="server" Text="Change Location"
                                                    CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("ID")  & ","  & Eval("CourseID") %>'> </asp:LinkButton>
                                                <asp:Label ID="lblIsBigFirm" runat="server" Text='<%# Eval("IsBigFirm") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblISDisplayChangeLocation" runat="server" Text='<%# Eval("ISDisplayChangeLocation") %>'
                                                    Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                    <telerik:RadGrid ID="grdRegisteredInterimAssessments" runat="server" AutoGenerateColumns="False"
                        AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                        AllowFilteringByColumn="false" Visible="false">
                        <PagerStyle CssClass="sd-pager" />
                        <GroupingSettings CaseSensitive="false" ShowUnGroupButton="false" />
                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="Curriculum"></telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="Curriculum"></telerik:GridGroupByField>
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Course" HeaderText="Course" SortExpression="Course"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <telerik:GridBoundColumn DataField="Venue" HeaderText="Location" SortExpression="Venue"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <telerik:GridBoundColumn DataField="Group" HeaderText="Group" SortExpression="Group"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <telerik:GridBoundColumn DataField="StartTime" HeaderText="Start Time" SortExpression="StartTime"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <telerik:GridTemplateColumn DataField="ExamNumber" SortExpression="ExamNumber" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIntrimExamNumber" runat="server" Text='<%# Eval("ExamNumber") %>'></asp:Label>
                                        <asp:Label ID="lblIntrimCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblIntrimSessionID" runat="server" Text='<%# Eval("SessionID") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="StudentGroupID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentGroup" runat="server" Text='<%# Eval("StudentGroupID") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ID" HeaderText="Change Location">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDisplayLinkField" runat="server" Text='<%# Eval("ISDisplayChangeLocation") %>'
                                            Visible="false" />
                                        <asp:Label ID="lblSessionName" runat="server" Text='<%# Eval("Session") %>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lnkInterimUpdateGroup" runat="server" Text="Change Location"
                                            CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("ID")    %>'
                                            OnClientClick="CustomToggle();"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:Label ID="lblRagisteredInterimAssessments" runat="server" Text=""></asp:Label>
                </table>
            </div>

            <h2 class="expand form-title" onclick="CollapseExpand('divEnrolledMockExams','hdnEnrolledMockExams')">Enrolled Mock Exams</h2>
            <div class="collapse cai-form-content cai-table" id="divEnrolledMockExams">
                <asp:Repeater ID="rpMockExam" runat="server">
                    <ItemTemplate>
                        <table class="clearfix">
                            <tr>
                                <th><span>Curriculum:</span>
                                    <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                    <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server" Visible="false" />
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Repeater ID="rpMockExamDetails" runat="server">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <th>Course</th>
                                                    <th>Location</th>
                                                    <th>Group </th>
                                                    <th>Start Time </th>
                                                    <th>Exam Number </th>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <span class="mobile-label">Course:</span>
                                                        <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                            Visible="True"> </asp:Label>
                                                    </td>
                                                    <td>
                                                        <span class="mobile-label">Location:</span>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <span class="mobile-label">Group:</span>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <span class="mobile-label">Start Time:</span>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, " StartTime") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <span class="mobile-label">Exam Number:</span>
                                                        <asp:Label ID="lblMockExamNumber" runat="server" Text='<%# Eval("ExamNumber") %>'></asp:Label>
                                                        <asp:Label ID="lblMockCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblMockSessionID" runat="server" Text='<%# Eval("SessionID") %>' Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                    <SeparatorTemplate>
                        <hr />
                    </SeparatorTemplate>
                </asp:Repeater>
                <telerik:RadGrid ID="grdMockExam" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                    AllowFilteringByColumn="false" Visible="false">
                    <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="Curriculum"></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="Curriculum"></telerik:GridGroupByField>
                                </GroupByFields>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="Course"></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="Course"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression="ID"
                                DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" Visible="false" />
                            <telerik:GridBoundColumn DataField="Course" HeaderText="Course" SortExpression="Course"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Venue" HeaderText="Location" SortExpression="Venue"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Group" HeaderText="Group" SortExpression="Group"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="StartTime" HeaderText="Start Time" SortExpression="StartTime"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridTemplateColumn DataField="ExamNumber" SortExpression="ExamNumber" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMockExamNumber" runat="server" Text='<%# Eval("ExamNumber") %>'></asp:Label>
                                    <asp:Label ID="lblMockCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblMockSessionID" runat="server" Text='<%# Eval("SessionID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:Label ID="lblErrorMockExam" runat="server" Text=""></asp:Label>
            </div>
            <h2 class="expand form-title" onclick="CollapseExpand('divEnrolledExams','hdnEnrolledExams')">Enrolled Exams</h2>
            <div class="collapse cai-form-content cai-table" id="divEnrolledExams">
                <table class="clearfix">
                    <asp:Repeater ID="rptEnrolledExams" runat="server" OnItemCommand="rptEnrolledExams_ItemCommand">
                        <HeaderTemplate>
                            <thead>
                                <tr>
                                    <th>Course</th>
                                    <th>Location</th>
                                    <th>Group</th>
                                    <th>Start Time</th>
                                    <th>ExamNumber</th>
                                </tr>
                            </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <th colspan="5">Curriculum:
                                    <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                        <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                            Visible="false" />
                                    </th>
                                </tr>

                                <asp:Repeater ID="rptSummerSession" runat="server" OnItemCommand="rptSummerSession_ItemCommand">
                                    <HeaderTemplate>
                                        <tr>
                                            <th colspan="5">Session:
                                                        <asp:Label ID="lblSummerSession" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'
                                                            runat="server" />
                                                <asp:Label ID="lblSessionIDItem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SessionID") %>'
                                                    Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:LinkButton ID="lnkUpdateGroup" runat="server" Text="Change Session" EnableViewState="True"
                                                    CommandName="ChangeGroupByCurriculum" Font-Underline="true" CommandArgument='<%# Eval("CurriculumID") %>'></asp:LinkButton>

                                                <asp:LinkButton ID="lnkChangeLocation" runat="server" Text="Change Location" CommandName="ChangeLocationBySession"
                                                    Font-Underline="true" CommandArgument='<%# Eval("CurriculumID") %>' EnableViewState="True"
                                                    OnClientClick="CustomToggle();"></asp:LinkButton>
                                                <asp:Label ID="lCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurriculumID") %>'
                                                    Visible="false"></asp:Label>

                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <span class="mobile-label">Course:</span>
                                                <asp:Label ID="lblCourse" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                    Visible="True"></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Venue:</span>
                                                <asp:Label ID="Label1" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Venue") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Group:</span>
                                                <asp:Label ID="Label2" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Start Time:</span>
                                                <asp:Label ID="lblChapterStartDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartTime") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Exam Number:</span>
                                                <asp:Label ID="lblSummerEnrolledExam" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExamNumber") %>'></asp:Label>
                                                <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblClassID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("OrderID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblBillToComany" runat="server" Text='<%# Eval("BilltoCompany") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                                    Visible="false" />
                                                <asp:Label ID="lblIsBigFirm" runat="server" Text='<%# Eval("IsBigFirm") %>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>


                                <asp:Repeater ID="rptAutumnSession" runat="server" OnItemCommand="rptAutumnLocation_ItemCommand">
                                    <HeaderTemplate>
                                        <tr>
                                            <th colspan="5">Session:
                                                        <asp:Label ID="lblH1" Text='<%# Eval("Session") %>' runat="server" />

                                                <asp:LinkButton ID="lnkUpdateGroupAutumn" runat="server" Text="Change Location" CommandName="ChangeLocationByAutumn"
                                                    Font-Underline="true" CommandArgument='<%# Eval("Curriculum") %>' EnableViewState="True"
                                                    OnClientClick="CustomToggle();"></asp:LinkButton>

                                                <asp:Label ID="lAutumCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurriculumID") %>'
                                                    Visible="false"></asp:Label>
                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <span class="mobile-label">Course:</span>
                                                <asp:Label ID="lblCourse" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                    Visible="True"></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Venue:</span>
                                                <asp:Label ID="Label1" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Venue") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Group:</span>
                                                <asp:Label ID="Label2" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Start Time:</span>
                                                <asp:Label ID="lblChapterStartDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartTime") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Exam Number:</span>
                                                <asp:Label ID="lblAutumnEnrolledExam" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExamNumber") %>'></asp:Label>
                                                <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblClassID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("OrderID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblBillToComany" runat="server" Text='<%# Eval("BilltoCompany") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                                    Visible="false" />
                                                <asp:Label ID="lblClassRegistrationID" Text='<%# Eval("ClassRegistrationID") %>'
                                                    runat="server" Visible="false" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Label ID="lblErrorRagisteredExams" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <telerik:RadWindow ID="radMockTrial" runat="server" Width="400px" Height="200px"
            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
            Title="Change Location" Behavior="None">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMsgClassRegistration" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <h1>Change Location</h1>

                    change location to:
                    <asp:DropDownList ID="cmbGroupNames" runat="server">
                    </asp:DropDownList>

                </div>
                <div>

                    <asp:Button ID="btnSave" runat="server" Text="Submit" class="submitBtn" OnClientClick="ShowProgress()" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="submitBtn" OnClientClick="ShowProgress()" />
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="radExamWindow" runat="server" Width="400px" Height="200px"
            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
            Title="Change Location" Behavior="None">
            <ContentTemplate>
                <div>

                    <asp:Label ID="lblExamMsg" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <h1>Change Location</h1>

                    <asp:Label ID="lblChangeLocation" runat="server" Text="Change Location of all Autumn Exams to"></asp:Label>
                    <asp:DropDownList ID="drpExamLocation" runat="server">
                    </asp:DropDownList>

                </div>
                <div>

                    <asp:Button ID="btnExamLocationSave" runat="server" Text="Save" class="submitBtn"
                        OnClientClick="ShowProgress()" />
                    <asp:Button ID="btnExamLocationCancel" runat="server" Text="Cancel" class="submitBtn"
                        OnClientClick="ShowProgress()" />
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <asp:UpdateProgress ID="updProgress" runat="server">
            <ProgressTemplate>
                <div id="loading" style="width: 100px; height: 50px; display: none; text-align: center; margin: auto;">
                    loading...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <telerik:RadWindow ID="radExamChangeSession" runat="server" Width="900" Height="350"
                    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                    Title="Change Session" Behavior="None">
                    <ContentTemplate>
                        <div>
                            <p>
                                <h1>Change Session</h1>
                                <asp:Label ID="lblChangeSessionText" runat="server" Text=""></asp:Label>
                                <telerik:RadGrid ID="RadChangeExamSession" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AllowFilteringByColumn="True" ShowGroupPanel="false">
                                    <PagerStyle CssClass="sd-pager" />
                                    <GroupingSettings CaseSensitive="false" GroupByFieldsSeparator="" ShowUnGroupButton="false" />
                                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                        <Columns>
                                            <telerik:GridTemplateColumn DataField="CourseID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowIndex" runat="server" Text='<%# CType(Container, GridDataItem).RowIndex %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>'></asp:Label>
                                                    <asp:Label ID="lblClassID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    <asp:Label ID="lblCurriculumID" runat="server" Text='<%# Eval("CurriculumID") %>'></asp:Label>
                                                    <asp:Label ID="lblClassRegistrationID" runat="server" Text='<%# Eval("ClassRegistrationID") %>'></asp:Label>
                                                    <asp:Label ID="lblRouteOfEntryID" runat="server" Text='<%# Eval("RouteOfEntryID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression="ID"
                                                DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" Visible="false" />
                                            <telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                            <telerik:GridBoundColumn DataField="Course" HeaderText="Course" SortExpression="Course"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                            <telerik:GridBoundColumn DataField="Venue" HeaderText="Location" SortExpression="Venue"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />

                                            <telerik:GridBoundColumn DataField="Group" HeaderText="Group" SortExpression="Group"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                            <telerik:GridBoundColumn DataField="StartTime" HeaderText="Current  Exam" SortExpression="StartTime"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                            <telerik:GridBoundColumn DataField="AutumnExam" HeaderText="Autumn Exam" SortExpression="AutumnExam"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                            <telerik:GridTemplateColumn DataField="SessionID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSessionID" runat="server" Text='<%# Eval("SessionID") %>'></asp:Label>
                                                    <asp:Label ID="lblAcademickCycle" runat="server" Text='<%# Eval("AcademicCycleID__c") %>'></asp:Label>
                                                    <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("Orderid") %>'></asp:Label>
                                                    <asp:Label ID="lblSummerProductID" runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                                                    <asp:Label ID="lblAutumnClassProductID" runat="server" Text='<%# Eval("AutumnClassProductID") %>'></asp:Label>
                                                    <asp:Label ID="lblAutumnClassID" runat="server" Text='<%# Eval("AutumnClassID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Change">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkExamChecked" runat="server" OnCheckedChanged="chkSelect_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </p>
                        </div>
                        <div>

                            <asp:Label ID="lblExamSessionMsg" runat="server" Text=""></asp:Label>
                            Please Select the Location of your Autumn Exams:

                            Change Location of All Selected Exams to :
                            <asp:DropDownList ID="drpAutumnSession" runat="server">
                            </asp:DropDownList>

                        </div>
                        <div>
                            <asp:Button ID="btnExamSessionSave" runat="server" Text="Submit" class="submitBtn" OnClientClick="ShowProgress()" />
                            <asp:Button ID="btnBack" runat="server" Text="Cancel" class="submitBtn" OnClientClick="ShowProgress()" />
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </ContentTemplate>
        </asp:UpdatePanel>
        <telerik:RadWindow ID="radAlternativeLocation" runat="server" Width="600px" Height="150px"
            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
            Title="Alternate Location" Behavior="None">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblAlternativeMessage" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblClassGroupMsg" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:Button ID="btnAlternativeOk" runat="server" Text="Ok" class="submitBtn" Width="80px" />
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <asp:Label ID="lblError" runat="server" Visible="False" />
        <cc1:User ID="User1" runat="server" />
    </div>
    <asp:HiddenField ID="hdnfirst" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEnrolledRevision" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEnrolledInterim" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEnrolledMockExams" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEnrolledExams" runat="server" Value="0" />
</div>
