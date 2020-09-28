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

            document.getElementById('hdnfirst').value = StateValue;
        }
        if (HiddenPanelState == 'hdnEnrolledRevision') {

            document.getElementById('hdnEnrolledRevision').value = StateValue;
        }
        if (HiddenPanelState == 'hdnEnrolledInterim') {

            document.getElementById('hdnEnrolledInterim').value = StateValue;
        }
        if (HiddenPanelState == 'hdnEnrolledMockExams') {

            document.getElementById('hdnEnrolledMockExams').value = StateValue;
        }
        if (HiddenPanelState == 'hdnEnrolledExams') {

            document.getElementById('hdnEnrolledExams').value = StateValue;
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
    <%-- <asp:HiddenField ID="hdnfirst" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEnrolledRevision" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEnrolledInterim" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEnrolledMockExams" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEnrolledExams" runat="server" Value="0" />--%>
    <input type="hidden" name="hdnfirst" id="hdnfirst" value="0" />
    <input type="hidden" name="hdnEnrolledRevision" id="hdnEnrolledRevision" value="0" />
    <input type="hidden" name="hdnEnrolledInterim" id="hdnEnrolledInterim" value="0" />
    <input type="hidden" name="hdnEnrolledMockExams" id="hdnEnrolledMockExams" value="0" />
    <input type="hidden" name="hdnEnrolledExams" id="hdnEnrolledExams" value="0" />
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </Telerik:RadAjaxLoadingPanel>
    <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <div>
            <span class="label-title-inline">Name:</span>
            <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label><br />
            <span class="label-title-inline">Student number: </span>
            <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label>
            <div class="actions">
                <asp:RadioButton ID="rdoCurrentAcademicCycle" runat="server" GroupName="AcadmicCycle"
                    Text="Academic cycle: " AutoPostBack="true" Checked="true" />
                <asp:RadioButton ID="rdoNextAcadmicCycle" runat="server" GroupName="AcadmicCycle"
                    Text="Academic cycle: " AutoPostBack="true" />
           <%--added as part of 20857--%>
			<div id="divProctorU"  runat="server">
			<br />
			<br />
			<asp:Label ID="lblproctorU" runat="server" Text="" class="label-title-inline" >
				</asp:Label>
			</div>		
			<%--End of 20857--%>
				</div>
			</div>
            <div class="actions">
                <asp:HyperLink ID="HyperLink1" runat="server" class="expand submitBtn" onclick="CollapseAll(this)">Expand all</asp:HyperLink>
            </div>
            <div align="right">
                <asp:LinkButton ID="lnkClassSchedule" runat="server" Text="Class Schedule" Visible="false"></asp:LinkButton>
            </div>
            <div class="demo cai-form">
                <h2 class="expand form-title" onclick="CollapseExpand('first','hdnfirst')">
                    Enrolled courses</h2>
                <div id="first" class="active cai-form-content cai-table mobile-table">
                    <asp:Repeater ID="rpEnrolledCourses" runat="server">
                        <ItemTemplate>
                            <span class="label-title"><span>Curriculum:</span>
                                <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                    Visible="false" />
                            </span>
                            <table>
                                <asp:Repeater ID="rpEnrolledCoursesDetails" runat="server" OnItemCommand="rptSession_ItemCommand">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th>
                                                    Course
                                                </th>
                                                <th>
                                                    Location
                                                </th>
                                                <th>
                                                    Group
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <span class="mobile-label">Course:</span>
                                                <asp:Label ID="lbtnCourse" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                    CommandArgument='<%# Eval("ClassID") %>' CssClass="cai-table-data" CommandName="CourseClick"
                                                    Font-Underline="false"></asp:Label>
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
                                    <FooterTemplate>
                                        </tbody>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                    <Telerik:RadGrid ID="grdEnrolledCourses" runat="server" AutoGenerateColumns="False"
                        AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                        AllowFilteringByColumn="false" Visible="false">
                        <PagerStyle CssClass="sd-pager" />
                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                            <GroupByExpressions>
                                <Telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <Telerik:GridGroupByField FieldName="Curriculum"></Telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <Telerik:GridGroupByField FieldName="Curriculum"></Telerik:GridGroupByField>
                                    </GroupByFields>
                                </Telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <Telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression="ID"
                                    DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" Visible="False" />
                                <Telerik:GridBoundColumn DataField="Name" HeaderText="Course" SortExpression="Name"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="Venue" HeaderText="Location" SortExpression="Venue"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="Group" HeaderText="Group" SortExpression="Group"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            </Columns>
                        </MasterTableView>
                    </Telerik:RadGrid>
                    <asp:Label ID="lblErrorEnrolledCourse" runat="server" Text=""></asp:Label>
                </div>
                <h2 class="expand form-title" onclick="CollapseExpand('divEnrolledRevision','hdnEnrolledRevision')">
                    Enrolled revision courses</h2>
                <div class="active cai-form-content cai-table mobile-table" id="divEnrolledRevision">
                    <asp:Repeater ID="rptRevisionsCourses" runat="server">
                        <HeaderTemplate>
                            <table>
                                <thead>
                                    <tr>
                                        <th>
                                            Course
                                        </th>
                                        <th>
                                            Location
                                        </th>
                                        <th>
                                            Group
                                        </th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <th colspan="3">
                                    <span>Curriculum:</span>
                                    <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                    <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                        Visible="false" />
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
                                                    CommandArgument='<%# Eval("ID") %>' CssClass="cai-table-data" CommandName="CourseClick"
                                                    Font-Underline="true"></asp:LinkButton>
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
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Label ID="lblRevisionError" runat="server" Text=""></asp:Label>
                </div>
                <h2 class="expand form-title" onclick="CollapseExpand('divEnrolledInterim','hdnEnrolledInterim')">
                    Enrolled interim assessments</h2>
                <div class="active cai-form-content cai-table mobile-table" id="divEnrolledInterim">
                    <asp:Repeater ID="rpIntrimAssessment" runat="server">
                        <HeaderTemplate>
                            <table>
                                <thead>
                                    <tr>
                                        <th>
                                            Course
                                        </th>
                                        <th>
                                            Location
                                        </th>
                                        <th>
                                            Group
                                        </th>
                                        <th>
                                            Start time
                                        </th>
                                        <%--<th style="display:none;">Exam number</th>--%>
                                        <th>
                                            Change location
                                        </th>
                                    <th>Exam Link
                                    </th>
                                    </tr>
                                </thead>
                                <tbody>
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
                                    <tr>
                                        <th colspan="6">
                                            <span>Session:</span>
                                            <asp:Label ID="lblSummerSession" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'
                                                runat="server" />
                                            <asp:Label ID="lblSessionIDItem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SessionID") %>'
                                                Visible="false"></asp:Label>
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="clearfix">
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
                                            <span class="mobile-label">Start time:</span>
                                            <asp:Label ID="Label3" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, " StartTime") %>'></asp:Label>
                                        </td>
                                        <td style="display: none;">
                                            <%--<span class="mobile-label" style="display:none;">Exam number:</span>--%>
                                            <%--<asp:Label ID="lblIntrimExamNumber" runat="server" CssClass="cai-table-data" style="display:none;" Text='<%# Eval("ExamNumber") %>'></asp:Label>--%>
                                            <%--added corrected commented end tag for Redmine #20116 --%>
                                            <asp:Label ID="lblIntrimCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblIntrimSessionID" runat="server" Text='<%# Eval("SessionID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblStudentGroup" runat="server" Text='<%# Eval("StudentGroupID") %>'
                                                Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Change location:</span>
                                            <asp:TextBox ID="txtDisplayLinkField" runat="server" Text='<%# Eval("ISDisplayChangeLocation") %>'
                                                Visible="false" />
                                            <asp:Label ID="lblIsBigFirm" runat="server" Text='<%# Eval("IsBigFirm") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSessionName" runat="server" Text='<%# Eval("Session") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lnkInterimUpdateGroup" runat="server" CssClass="cai-table-data"
                                                Text="Change Location" CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("ID")  & ","  & Eval("CourseID") %>'
                                                OnClientClick="CustomToggle();"> </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="rpResitAssessmentDetails" runat="server" OnItemCommand="rpResitAssessmentDetails_ItemCommand">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="6">
                                            <span>Session:</span>
                                            <asp:Label ID="lblH1" Text='<%# Eval("Session") %>' runat="server" />
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="clearfix">
                                        <td>
                                            <span class="mobile-label">Course:</span>
                                            <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                Visible="True"></asp:Label>
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
                                            <span class="mobile-label">Start time:</span>
                                            <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, " StartTime") %>'></asp:Label>
                                        </td>
                                        <td style="display: none;">
                                            <%--<span class="mobile-label" style="display:none;">Exam number:</span>
                                            <asp:Label ID="lblResitExamNumber" runat="server" style="display:none;" Text='<%# Eval("ExamNumber") %>'></asp:Label>--%>
                                            <%--added corrected commented end tag for Redmine #20116 --%>
                                            <asp:Label ID="lblResitCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblResitSessionID" runat="server" Text='<%# Eval("SessionID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblStudentGroup" runat="server" Text='<%# Eval("StudentGroupID") %>'
                                                Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <span class="mobile-label">Change location:</span>
                                            <asp:LinkButton ID="lnkInterimUpdateGroup" runat="server" Text="Change Location"
                                                CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("ID")  & ","  & Eval("CourseID") %>'> </asp:LinkButton>
                                            <asp:Label ID="lblIsBigFirm" runat="server" Text='<%# Eval("IsBigFirm") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblISDisplayChangeLocation" runat="server" Text='<%# Eval("ISDisplayChangeLocation") %>'
                                                Visible="false"></asp:Label>
                                        </td>
                                            <td>
                                                <asp:HyperLink ID="lnkEnrolledInterim" runat="server" Target="_blank" NavigateUrl="" Visible="true"> 
                                                        <span>Click here</span>
                                                </asp:HyperLink>
                                            </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <Telerik:RadGrid ID="grdRegisteredInterimAssessments" runat="server" AutoGenerateColumns="False"
                        AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                        AllowFilteringByColumn="false" Visible="false">
                        <PagerStyle CssClass="sd-pager" />
                        <GroupingSettings CaseSensitive="false" ShowUnGroupButton="false" />
                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                            <GroupByExpressions>
                                <Telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <Telerik:GridGroupByField FieldName="Curriculum"></Telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <Telerik:GridGroupByField FieldName="Curriculum"></Telerik:GridGroupByField>
                                    </GroupByFields>
                                </Telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <Telerik:GridBoundColumn DataField="Course" HeaderText="Course" SortExpression="Course"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="Venue" HeaderText="Location" SortExpression="Venue"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="Group" HeaderText="Group" SortExpression="Group"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="StartTime" HeaderText="Start time" SortExpression="StartTime"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridTemplateColumn DataField="ExamNumber" SortExpression="ExamNumber" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIntrimExamNumber" runat="server" Text='<%# Eval("ExamNumber") %>'></asp:Label>
                                        <asp:Label ID="lblIntrimCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblIntrimSessionID" runat="server" Text='<%# Eval("SessionID") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </Telerik:GridTemplateColumn>
                                <Telerik:GridTemplateColumn DataField="StudentGroupID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentGroup" runat="server" Text='<%# Eval("StudentGroupID") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </Telerik:GridTemplateColumn>
                                <Telerik:GridTemplateColumn DataField="ID" HeaderText="Change location">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDisplayLinkField" runat="server" Text='<%# Eval("ISDisplayChangeLocation") %>'
                                            Visible="false" />
                                        <asp:Label ID="lblSessionName" runat="server" Text='<%# Eval("Session") %>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lnkInterimUpdateGroup" runat="server" Text="Change Location"
                                            CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("ID")    %>'
                                            OnClientClick="CustomToggle();"></asp:LinkButton>
                                    </ItemTemplate>
                                </Telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </Telerik:RadGrid>
                    <asp:Label ID="lblRagisteredInterimAssessments" runat="server" Text=""></asp:Label>
                </div>
                <h2 class="expand form-title" onclick="CollapseExpand('divEnrolledMockExams','hdnEnrolledMockExams')">
                    Enrolled mock exams</h2>
                <div class="active cai-form-content cai-table mobile-table" id="divEnrolledMockExams">
                    <asp:Repeater ID="rpMockExam" runat="server">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th colspan="6">
                                        <%--colspan changes for Redmie #20518--%>
                                        <span>Curriculum:</span>
                                        <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                        <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                            Visible="false" />
                                        <%--  <asp:LinkButton ID="lnkChangeLocation" runat="server" Text="Change Location" CommandName="ChangeLocationBySession"
                                            Font-Underline="true" CommandArgument='<%# Eval("CurriculumID") %>' EnableViewState="True"
                                            OnClientClick="CustomToggle();"></asp:LinkButton>--%><%--code added and commented for Redmie #20518--%>
                                    </th>
                                </tr>
                                <asp:Repeater ID="rpMockExamDetails" runat="server" OnItemCommand="rpMockExamDetails_ItemCommand">
                                    <%--code added  for Redmie #20518--%>
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th>
                                                    Course
                                                </th>
                                                <th>
                                                    Location
                                                </th>
                                                <th>
                                                    Group
                                                </th>
                                                <th>
                                                    Start time
                                                </th>
                                                <%--<th style="display:none;">Exam number </th>--%>
                                                <th>
                                                    Change Location
                                                </th>
                                                <%--code added for Redmie #20518--%>
                                                <th>
                                                    Exam Link
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <span class="mobile-label">Course:</span>
                                                    <asp:Label ID="lblChapterLocation" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                        Visible="True"> </asp:Label>
                                                    <asp:Label ID="lblClassID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <%--code added for Redmie #20518--%>
                                                    <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>' Visible="false"></asp:Label>
                                                    <%--code added for Redmie #20518--%>
                                                    <asp:Label ID="lCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurriculumID") %>'
                                                        Visible="false"></asp:Label>
                                                    <%--code added for Redmie #20518--%>
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
                                                    <span class="mobile-label">Start time:</span>
                                                    <asp:Label ID="Label3" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, " StartTime") %>'></asp:Label>
                                                </td>
                                                <td style="display: none;">
                                                    <%--<span class="mobile-label">Exam number:</span>
                                                    <asp:Label ID="lblMockExamNumber" CssClass="cai-table-data" runat="server" Text='<%# Eval("ExamNumber") %>'></asp:Label>--%>
                                                    <asp:Label ID="lblMockCurriculum" CssClass="cai-table-data" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lblMockSessionID" CssClass="cai-table-data" runat="server" Text='<%# Eval("SessionID") %>'
                                                        Visible="false"></asp:Label>
                                                </td>
                                                <td><%--td code for Redmie #20518--%>
                                                    <asp:Label ID="lblStudentGroup" runat="server" Text='<%# Eval("StudentGroupID") %>'
                                                        Visible="false"></asp:Label><%--code added for Redmie #20518--%>
                                                    <asp:TextBox ID="txtDisplayLinkField" runat="server" Text='<%# Eval("ISDisplayChangeLocation") %>'
                                                        Visible="false" /><%--code added for Redmie #20518--%>
                                                    <asp:LinkButton ID="lnkChangeLocation" runat="server" Text="Change Location" CommandName="ChangeGroup"
                                                        Font-Underline="true" CommandArgument='<%# Eval("ID") & "," & Eval("CourseID") %>'
                                                        EnableViewState="True" OnClientClick="CustomToggle();"></asp:LinkButton>
                                                    <%--code added for Redmie #20518--%>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="lnkMockExamEnrolled" runat="server" Target="_blank" NavigateUrl="" Visible="true"> 
                                                        <span>Click here</span>
                                                    </asp:HyperLink>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <SeparatorTemplate>
                            <hr />
                        </SeparatorTemplate>
                    </asp:Repeater>
                    <Telerik:RadGrid ID="grdMockExam" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                        SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                        AllowFilteringByColumn="false" Visible="false">
                        <GroupingSettings CaseSensitive="false" />
                        <PagerStyle CssClass="sd-pager" />
                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                            <GroupByExpressions>
                                <Telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <Telerik:GridGroupByField FieldName="Curriculum"></Telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <Telerik:GridGroupByField FieldName="Curriculum"></Telerik:GridGroupByField>
                                    </GroupByFields>
                                    <SelectFields>
                                        <Telerik:GridGroupByField FieldName="Course"></Telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <Telerik:GridGroupByField FieldName="Course"></Telerik:GridGroupByField>
                                    </GroupByFields>
                                </Telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <Telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression="ID"
                                    DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" Visible="false" />
                                <Telerik:GridBoundColumn DataField="Course" HeaderText="Course" SortExpression="Course"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="Venue" HeaderText="Location" SortExpression="Venue"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="Group" HeaderText="Group" SortExpression="Group"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="StartTime" HeaderText="Start time" SortExpression="StartTime"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridTemplateColumn DataField="ExamNumber" SortExpression="ExamNumber" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMockExamNumber" runat="server" Text='<%# Eval("ExamNumber") %>'></asp:Label>
                                        <asp:Label ID="lblMockCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblMockSessionID" runat="server" Text='<%# Eval("SessionID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </Telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </Telerik:RadGrid>
                    <asp:Label ID="lblErrorMockExam" runat="server" Text=""></asp:Label>
                </div>
                <h2 class="expand form-title" onclick="CollapseExpand('divEnrolledExams','hdnEnrolledExams')">
                    Enrolled exams</h2>
                <div class="active cai-form-content cai-table mobile-table" id="divEnrolledExams">
                    <asp:Repeater ID="rptEnrolledExams" runat="server" OnItemCommand="rptEnrolledExams_ItemCommand">
                        <HeaderTemplate>
                            <table>
                                <thead class="no-mob">
                                    <tr>
                                        <th>
                                            Course
                                        </th>
                                        <th>
                                            Location
                                        </th>
                                        <th>
                                            Group
                                        </th>
                                        <th>
                                            Start time
                                        </th>
                                        <th>
                                            Exam Link
                                        </th>
                                        <%--<th>Exam number</th>--%>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th colspan="5">
                                    Curriculum:
                                    <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                    <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                        Visible="false" />
                                </th>
                            </tr>
                            <asp:Repeater ID="rptSummerSession" runat="server" OnItemCommand="rptSummerSession_ItemCommand">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="5">
                                            Session:
                                            <asp:Label ID="lblSummerSession" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'
                                                runat="server" />
                                            <asp:Label ID="lblSessionIDItem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SessionID") %>'
                                                Visible="false"></asp:Label>
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
                                    <tr class="clearfix">
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
                                            <span class="mobile-label">Start time:</span>
                                            <asp:Label ID="lblChapterStartDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartTime") %>'></asp:Label>
                                        </td>
                                        <td style="display: none;">
                                            <span class="mobile-label" style="display: none;">Exam number:</span>
                                            <asp:Label ID="lblSummerEnrolledExam" CssClass="cai-table-data" Style="display: none;"
                                                runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExamNumber") %>'></asp:Label>
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
                                        <td>
                                            <asp:HyperLink ID="lnkExamEnrolledSummer" runat="server" Target="_blank" NavigateUrl="" Visible="true"> 
                                                <span>Click here</span>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="rptAutumnSession" runat="server" OnItemCommand="rptAutumnLocation_ItemCommand">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="5">
                                            Session:
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
                                            <span class="mobile-label">Start time:</span>
                                            <asp:Label ID="lblChapterStartDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartTime") %>'></asp:Label>
                                        </td>
                                        <td style="display: none;">
                                            <span class="mobile-label" style="display: none;">Exam number:</span>
                                            <asp:Label ID="lblAutumnEnrolledExam" CssClass="cai-table-data" Style="display: none;"
                                                runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExamNumber") %>'></asp:Label>
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
                                        <td>
                                            <asp:HyperLink ID="lnkExamEnrolledAutum" runat="server" Target="_blank" NavigateUrl="" Visible="true"> 
                                                <span>Click here</span>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Label ID="lblErrorRagisteredExams" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <Telerik:RadWindow ID="radMockTrial" runat="server" Width="400px" Height="220px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Change location" Behavior="None" Skin="Default">
                <ContentTemplate>
                    <div>
                        <asp:Label ID="lblMsgClassRegistration" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        Change location to:
                        <asp:DropDownList ID="cmbGroupNames" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:Button ID="btnSave" runat="server" Text="Submit" class="submitBtn" OnClientClick="ShowProgress()" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="submitBtn" OnClientClick="ShowProgress()" />
                    </div>
                </ContentTemplate>
            </Telerik:RadWindow>
            <Telerik:RadWindow ID="radExamWindow" runat="server" Width="400px" Height="200px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Change Location" Behavior="None">
                <ContentTemplate>
                    <div>
                        <asp:Label ID="lblExamMsg" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <h1>
                            Change location</h1>
                        <asp:Label ID="lblChangeLocation" runat="server" Text="Change location of all autumn exams to"></asp:Label>
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
            </Telerik:RadWindow>
            <%-- <asp:UpdateProgress ID="updProgress" runat="server">
                <ProgressTemplate>
                    <div id="loading" style="width: 100px; height: 50px; text-align: center; margin: auto;">
                        loading...
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <Telerik:RadWindow ID="radExamChangeSession" runat="server" Width="900" Height="350"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Change Session" Behavior="None">
                <ContentTemplate>
                    <div>
                        <p>
                            <h1>
                                Change session</h1>
                            <asp:Label ID="lblChangeSessionText" runat="server" Text=""></asp:Label>
                            <Telerik:RadGrid ID="RadChangeExamSession" runat="server" AutoGenerateColumns="False"
                                AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                AllowFilteringByColumn="True" ShowGroupPanel="false">
                                <PagerStyle CssClass="sd-pager" />
                                <GroupingSettings CaseSensitive="false" GroupByFieldsSeparator="" ShowUnGroupButton="false" />
                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                    <Columns>
                                        <Telerik:GridTemplateColumn DataField="CourseID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowIndex" runat="server" Text='<%# CType(Container, GridDataItem).RowIndex %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>'></asp:Label>
                                                <asp:Label ID="lblClassID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                <asp:Label ID="lblCurriculumID" runat="server" Text='<%# Eval("CurriculumID") %>'></asp:Label>
                                                <asp:Label ID="lblClassRegistrationID" runat="server" Text='<%# Eval("ClassRegistrationID") %>'></asp:Label>
                                                <asp:Label ID="lblRouteOfEntryID" runat="server" Text='<%# Eval("RouteOfEntryID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </Telerik:GridTemplateColumn>
                                        <Telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression="ID"
                                            DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" Visible="false" />
                                        <Telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <Telerik:GridBoundColumn DataField="Course" HeaderText="Course" SortExpression="Course"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <Telerik:GridBoundColumn DataField="Venue" HeaderText="Location" SortExpression="Venue"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <Telerik:GridBoundColumn DataField="Group" HeaderText="Group" SortExpression="Group"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <Telerik:GridBoundColumn DataField="StartTime" HeaderText="Current exam" SortExpression="StartTime"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <Telerik:GridBoundColumn DataField="AutumnExam" HeaderText="Autumn exam" SortExpression="AutumnExam"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <Telerik:GridTemplateColumn DataField="SessionID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSessionID" runat="server" Text='<%# Eval("SessionID") %>'></asp:Label>
                                                <asp:Label ID="lblAcademickCycle" runat="server" Text='<%# Eval("AcademicCycleID__c") %>'></asp:Label>
                                                <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("Orderid") %>'></asp:Label>
                                                <asp:Label ID="lblSummerProductID" runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                                                <asp:Label ID="lblAutumnClassProductID" runat="server" Text='<%# Eval("AutumnClassProductID") %>'></asp:Label>
                                                <asp:Label ID="lblAutumnClassID" runat="server" Text='<%# Eval("AutumnClassID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </Telerik:GridTemplateColumn>
                                        <Telerik:GridTemplateColumn HeaderText="Change">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkExamChecked" runat="server" OnCheckedChanged="chkSelect_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </Telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </Telerik:RadGrid>
                        </p>
                    </div>
                    <div>
                        <asp:Label ID="lblExamSessionMsg" runat="server" Text=""></asp:Label>
                        Please select the location of your autumn exams: Change location of all selected
                        exams to :
                        <asp:DropDownList ID="drpAutumnSession" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:Button ID="btnExamSessionSave" runat="server" Text="Submit" class="submitBtn"
                            OnClientClick="ShowProgress()" />
                        <asp:Button ID="btnBack" runat="server" Text="Cancel" class="submitBtn" OnClientClick="ShowProgress()" />
                    </div>
                </ContentTemplate>
            </Telerik:RadWindow>
            <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
            <Telerik:RadWindow ID="radAlternativeLocation" runat="server" Width="600px" Height="150px"
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
            </Telerik:RadWindow>
            <%--	added RadWindow for Redmine #20518--%>
            <Telerik:RadWindow ID="radMockExamChangeLocation" runat="server" Width="400px" Height="200px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Change Location" Behavior="None">
                <ContentTemplate>
                    <div>
                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <h1>
                            Change location</h1>
                        <asp:Label ID="Label5" runat="server" Text="Change location of all mock exams to"></asp:Label>
                        <asp:DropDownList ID="drpMockLocation" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:Button ID="btnMockExamChangeLocation" runat="server" Text="Save" class="submitBtn"
                            OnClientClick="ShowProgress()" />
                        <asp:Button ID="btnMockExamLocationCancel" runat="server" Text="Cancel" class="submitBtn"
                            OnClientClick="ShowProgress()" />
                    </div>
                </ContentTemplate>
            </Telerik:RadWindow>
            <%--	End Redmine #20518--%>
            <asp:Label ID="lblError" runat="server" Visible="False" />
            <cc1:User ID="User1" runat="server" />
        </div>
    </Telerik:RadAjaxPanel>
</div>
