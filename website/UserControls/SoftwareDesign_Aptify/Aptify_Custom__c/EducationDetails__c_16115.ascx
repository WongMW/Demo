<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.EducationDetails__c"
    CodeFile="~/UserControls/Aptify_Custom__c/EducationDetails__c.ascx.vb" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-ui-1.11.2.min.js" type="text/javascript"></script>
<script src="../../Scripts/expand.js" type="text/javascript"></script>
<script src="../../Scripts/JScript.min.js" type="text/javascript"></script>
<script type="text/javascript">
  
<!--    //--><![CDATA[//><!--
    $(document).ready(function () {
        /*var firstDIv = $("#first");
        firstDIv.removeClass("collapse");
        firstDIv.addClass("expand");*/
    });
    $(function () {
        // --- Using the default options:
        /*   $("#firstHead").bind("click", function (ev) {

        var firstDIv = $("#first");
        firstDIv.removeClass("expand");
        firstDIv.addClass("collapse");
        });*/
        $("h2.expand").toggler();


        // --- Other options:
        //$("h2.expand").toggler({method: "toggle", speed: 0});
        //$("h2.expand").toggler({method: "toggle"});
        //$("h2.expand").toggler({speed: "fast"});
        //$("h2.expand").toggler({method: "fadeToggle"});
        //$("h2.expand").toggler({method: "slideFadeToggle"});    
        $("#content").expandAll({ trigger: "h2.expand", ref: "div.demo", localLinks: "p.top a" });
    });
    //--><!]]>


    function ShowMessage() {

        $('#IAdiv').dialog({

            modal: false,
            resizable: false,
            title: "Change Location",
            buttons: {
                Save: function () {
                    $(this).dialog('close');
                    $("[id$='btnSave']").trigger('click');
                },
                Cancel: function () {
                    $(this).dialog('close');
                }
            }
        });
        return true;
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="content-container clearfix">
            <div>
                <div align="center">
                    Name: <b>
                        <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label></b> Student
                    Number: <b>
                        <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label></b>
                    <%--  Academic Cycle:<b>
                        <asp:Label ID="lblAcademicCycle" runat="server" Text=""></asp:Label></b></div>--%>
                    <div>
                        <asp:RadioButton ID="rdoCurrentAcademicCycle" runat="server" GroupName="AcadmicCycle"
                            Text="Current Academic Cycle: " AutoPostBack="true" Checked="true" />
                        <asp:RadioButton ID="rdoNextAcadmicCycle" runat="server" GroupName="AcadmicCycle"
                            Text="Next Academic Cycle: " AutoPostBack="true" />
                    </div>
                    <%--  <div>
                    Intake Year:
                    <asp:DropDownList ID="cmbYear" runat="server" AutoPostBack="True" Enabled="false">
                    </asp:DropDownList>
                </div>--%>
                    <div class="demo" align="left">
                        <h2 class="expand">Enrolled Courses</h2>
                        <div id="first" class="collapse">
                            <p>
                                <asp:Repeater ID="rpEnrolledCourses" runat="server">
                                    <ItemTemplate>
                                        <table width="100%" cellpadding="2" cellspacing="0" style="background-color: white;">
                                            <tr>
                                                <th style="width: 15%" align="left">Curriculum:
                                                    <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                                    <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                                        Visible="false" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%;">
                                                    <asp:Repeater ID="rpEnrolledCoursesDetails" runat="server" OnItemCommand="rptSession_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table width="100%" cellpadding="4" cellspacing="10" style="border: solid 1px gray; background-color: #e9e8e8">
                                                                <tr>
                                                                    <th style="width: 10%" align="center" valign="middle">Course
                                                                    </th>
                                                                    <th style="width: 10%" align="center" valign="middle">Location
                                                                    </th>
                                                                    <th style="width: 10%" align="center" valign="middle">Group
                                                                    </th>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green; background-color: #f0fff0">
                                                                <tr>
                                                                    <td style="width: 10%;">
                                                                        <asp:LinkButton ID="lbtnCourse" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>' CommandArgument='<%# Eval("ClassID") %>' CommandName="CourseClick"></asp:LinkButton>
                                                                        <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                                            Visible="False"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 10%;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 10%;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
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
                                <telerik:RadGrid ID="grdEnrolledCourses" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
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
                                            </telerik:GridGroupByExpression>
                                        </GroupByExpressions>
                                        <Columns>
                                            <telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression="ID"
                                                DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" Visible="False" />
                                            <%-- <Telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />--%>
                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Course" SortExpression="Name"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                            <telerik:GridBoundColumn DataField="Venue" HeaderText="Location" SortExpression="Venue"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                            <telerik:GridBoundColumn DataField="Group" HeaderText="Group" SortExpression="Group"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid><br />
                                <asp:Label ID="lblErrorEnrolledCourse" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </p>
                        </div>
                        <h2 class="expand">Enrolled Interim Assessments</h2>
                        <div class="collapse">
                            <asp:Repeater ID="rpIntrimAssessment" runat="server">
                                <ItemTemplate>
                                    <table width="100%" cellpadding="2" cellspacing="0" style="background-color: white;">
                                        <tr>
                                            <th style="width: 15%" align="left">Curriculum:
                                                <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                                <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                                    Visible="false" />
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%;">
                                                <asp:Repeater ID="rpIntrimAssessmentDetails" runat="server" OnItemCommand="rpIntrimAssessmentDetails_ItemCommand">
                                                    <HeaderTemplate>
                                                        <table width="100%" cellpadding="4" cellspacing="10" style="border: solid 1px gray; background-color: #e9e8e8">
                                                            <tr>
                                                                <th style="width: 10%" align="center" valign="middle">Course
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Location
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Group
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Start Time
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Exam Number
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Change Location
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green; background-color: #f0fff0">
                                                            <tr>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                                        Visible="True"></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, " StartTime") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="lblIntrimExamNumber" runat="server" Text='<%# Eval("ExamNumber") %>'></asp:Label>
                                                                    <asp:Label ID="lblIntrimCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                                        Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblIntrimSessionID" runat="server" Text='<%# Eval("SessionID") %>'
                                                                        Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblStudentGroup" runat="server" Text='<%# Eval("StudentGroupID") %>'
                                                                        Visible="false"></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:TextBox ID="txtDisplayLinkField" runat="server" Text='<%# Eval("ISDisplayChangeLocation") %>'
                                                                        Visible="false" />
                                                                    <asp:Label ID="lblSessionName" runat="server" Text='<%# Eval("Session") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lnkInterimUpdateGroup" runat="server" Text="Change Location"
                                                                        ForeColor="Blue" CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("ID")  & ","  & Eval("CourseID") %>'> </asp:LinkButton>
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
                                                    ForeColor="Blue" CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("ID")    %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid><br />
                            <asp:Label ID="lblRagisteredInterimAssessments" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <h2 class="expand">Enrolled Resit Assessments</h2>
                        <div class="collapse">
                            <asp:Repeater ID="rpResitAssessment" runat="server">
                                <ItemTemplate>
                                    <table width="100%" cellpadding="2" cellspacing="0" style="background-color: white;">
                                        <tr>
                                            <th style="width: 15%" align="left">Curriculum:
                                                <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                                <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                                    Visible="false" />
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%;">
                                                <asp:Repeater ID="rpResitAssessmentDetails" runat="server">
                                                    <HeaderTemplate>
                                                        <table width="100%" cellpadding="4" cellspacing="10" style="border: solid 1px gray; background-color: #e9e8e8">
                                                            <tr>
                                                                <th style="width: 10%" align="center" valign="middle">Course
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Location
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Group
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Start Time
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Exam Number
                                                                </th>
                                                                <%-- <th style="width: 10%" align="center" valign="middle">
                                                            Change Location
                                                        </th>--%>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green; background-color: #f0fff0">
                                                            <tr>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                                        Visible="True"></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, " StartTime") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="lblResitExamNumber" runat="server" Text='<%# Eval("ExamNumber") %>'></asp:Label>
                                                                    <asp:Label ID="lblResitCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                                        Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblResitSessionID" runat="server" Text='<%# Eval("SessionID") %>'
                                                                        Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblStudentGroup" runat="server" Text='<%# Eval("StudentGroupID") %>'
                                                                        Visible="false"></asp:Label>
                                                                </td>
                                                                <%--<td style="width: 10%;">
                                                            <asp:TextBox ID="txtDisplayLinkField" runat="server" Text='<%# Eval("ISDisplayChangeLocation") %>'
                                                                Visible="false" />
                                                            <asp:Label ID="lblSessionName" runat="server" Text='<%# Eval("Session") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkInterimUpdateGroup" runat="server" Text="Change Location"
                                                                ForeColor="Blue" CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("ID")    %>'></asp:LinkButton>
                                                        </td>--%>
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
                            <telerik:RadGrid ID="grdResitAssessment" runat="server" AutoGenerateColumns="False"
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
                                        <%--<Telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression= "ID" DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />--%>
                                        <telerik:GridBoundColumn DataField="Course" HeaderText="Course" SortExpression="Course"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <telerik:GridBoundColumn DataField="Venue" HeaderText="Location" SortExpression="Venue"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <%-- <Telerik:GridBoundColumn DataField="Session" HeaderText="Session" SortExpression="Session"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />--%>
                                        <telerik:GridBoundColumn DataField="Group" HeaderText="Group" SortExpression="Group"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <telerik:GridBoundColumn DataField="StartTime" HeaderText="Start Time" SortExpression="StartTime"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <telerik:GridTemplateColumn DataField="ExamNumber" SortExpression="ExamNumber" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResitExamNumber" runat="server" Text='<%# Eval("ExamNumber") %>'></asp:Label>
                                                <asp:Label ID="lblResitCurriculum" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblResitSessionID" runat="server" Text='<%# Eval("SessionID") %>'
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
                                                    ForeColor="Blue" CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("ID")    %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid><br />
                            <asp:Label ID="lblErrorResit" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <h2 class="expand">Enrolled Mock Exams</h2>
                        <div class="collapse">
                            <asp:Repeater ID="rpMockExam" runat="server">
                                <ItemTemplate>
                                    <table width="100%" cellpadding="2" cellspacing="0" style="background-color: white;">
                                        <tr>
                                            <th style="width: 15%" align="left">Curriculum:
                                                <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                                <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                                    Visible="false" />
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%;">
                                                <asp:Repeater ID="rpMockExamDetails" runat="server">
                                                    <HeaderTemplate>
                                                        <table width="100%" cellpadding="4" cellspacing="10" style="border: solid 1px gray; background-color: #e9e8e8">
                                                            <tr>
                                                                <th style="width: 10%" align="center" valign="middle">Course
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Location
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Group
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Start Time
                                                                </th>
                                                                <th style="width: 10%" align="center" valign="middle">Exam Number
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green; background-color: #f0fff0">
                                                            <tr>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                                        Visible="True"> </asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, " StartTime") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;">
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
                                <PagerStyle CssClass="sd-pager" />
                                <GroupingSettings CaseSensitive="false" />
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
                            </telerik:RadGrid><br />
                            <asp:Label ID="lblErrorMockExam" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <h2 class="expand">Enrolled Exams</h2>
                        <div class="collapse">
                            <asp:Repeater ID="rptEnrolledExams" runat="server" OnItemCommand="rptEnrolledExams_ItemCommand">
                                <HeaderTemplate>
                                    <table width="100%" cellpadding="4" cellspacing="10" style="border: solid 1px gray; background-color: #e9e8e8">
                                        <tr>
                                            <th style="width: 20%" align="center" valign="middle">Course
                                            </th>
                                            <th style="width: 20%" align="center" valign="middle">Location
                                            </th>
                                            <th style="width: 20%" align="center" valign="middle">Group
                                            </th>
                                            <th style="width: 20%;" align="center" valign="middle">Start Time
                                            </th>
                                            <th style="width: 20%" align="center" valign="middle">ExamNumber
                                            </th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%" cellpadding="2" cellspacing="0" style="background-color: white;">
                                        <tr>
                                            <th style="width: 15%" align="left">Curriculum:
                                                <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                                <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                                    Visible="false" />
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%;">
                                                <asp:Repeater ID="rptSummerSession" runat="server" OnItemCommand="rptSummerSession_ItemCommand">
                                                    <HeaderTemplate>
                                                        <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green; background-color: #f0fff0"
                                                            align="left">
                                                            <tr>
                                                                <th style="width: 15%" align="left">Session:
                                                                    <asp:Label ID="lblSummerSession" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'
                                                                        runat="server" />
                                                                    <asp:Label ID="lblSessionIDItem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SessionID") %>'
                                                                        Visible="false"></asp:Label>
                                                                </th>
                                                                <th>


                                                                    <asp:LinkButton ID="lnkUpdateGroup" runat="server" Text="Change Session" ForeColor="Blue"
                                                                        EnableViewState="True" CommandName="ChangeGroupByCurriculum" Font-Underline="true"
                                                                        CommandArgument='<%# Eval("CurriculumID") %>'></asp:LinkButton>
                                                                    <%-- <asp:LinkButton ID="lnkUpdateGroup" runat="server" Text="Change Session" ForeColor="Blue"
                                                                        EnableViewState="True" CommandName="ChangeGroupByCurriculum" Font-Underline="true"
                                                                        CommandArgument='<%# Eval("CurriculumID") %>' OnClick="lnkUpdateGroup_OnClick"></asp:LinkButton>--%>
                                                                    &nbsp;&nbsp;
                                                                       <asp:LinkButton ID="lnkChangeLocation" runat="server" Text="Change Location" ForeColor="Blue"
                                                                           CommandName="ChangeLocationBySession" Font-Underline="true" CommandArgument='<%# Eval("CurriculumID") %>'
                                                                           EnableViewState="True"></asp:LinkButton>
                                                                    <asp:Label ID="lCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurriculumID") %>'
                                                                        Visible="false"></asp:Label>
                                                                    <%--   <asp:LinkButton ID="lnkChangeLocation" runat="server" Text="Change Location" ForeColor="Blue"
                                                                        CommandName="ChangeLocationBySession" Font-Underline="true" CommandArgument='<%# Eval("CurriculumID") %>'
                                                                        EnableViewState="True" OnClick="lnkChangeLocation_OnClick"></asp:LinkButton>--%>
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green; background-color: #f0fff0">
                                                            <tr>
                                                                <td style="width: 12%;">
                                                                    <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                                        Visible="True"></asp:Label>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Venue") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="lblChapterStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartTime") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="lblSummerEnrolledExam" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExamNumber") %>'></asp:Label>
                                                                    <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblClassID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("OrderID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblBillToComany" runat="server" Text='<%# Eval("BilltoCompany") %>'
                                                                        Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                                                        Visible="false" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%;">
                                                <asp:Repeater ID="rptAutumnSession" runat="server" OnItemCommand="rptAutumnLocation_ItemCommand">
                                                    <HeaderTemplate>
                                                        <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green; background-color: #f0fff0"
                                                            align="left">
                                                            <tr>
                                                                <th style="width: 15%" align="left">Session:
                                                                    <asp:Label ID="lblH1" Text='<%# Eval("Session") %>' runat="server" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                     <asp:LinkButton ID="lnkUpdateGroupAutumn" runat="server" Text="Change Location" ForeColor="Blue"
                                                                         CommandName="ChangeLocationByAutumn" Font-Underline="true" CommandArgument='<%# Eval("Curriculum") %>'
                                                                         EnableViewState="True"></asp:LinkButton>
                                                                    <%-- <asp:LinkButton ID="lnkUpdateGroupAutumn" runat="server" Text="Change Location" ForeColor="Blue"
                                                                        CommandName="ChangeGroupByCurriculum" Font-Underline="true" CommandArgument='<%# Eval("Curriculum") %>'
                                                                        EnableViewState="True" OnClick="lnkAutumChangeLocation_OnClick"></asp:LinkButton>--%>
                                                                    <asp:Label ID="lAutumCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurriculumID") %>'
                                                                        Visible="false"></asp:Label>
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green; background-color: #f0fff0">
                                                            <tr>
                                                                <td style="width: 12%;">
                                                                    <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'
                                                                        Visible="True"></asp:Label>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Venue") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Group") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="lblChapterStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartTime") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="lblAutumnEnrolledExam" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExamNumber") %>'></asp:Label>
                                                                    <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblClassID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("OrderID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblBillToComany" runat="server" Text='<%# Eval("BilltoCompany") %>'
                                                                        Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                                                        Visible="false" />
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
                            <asp:Label ID="lblErrorRagisteredExams" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <telerik:RadWindow ID="radMockTrial" runat="server" Width="400px" Height="200px"
                        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="Change Location" Behavior="None">
                        <ContentTemplate>
                            <%-- <div id="IAdiv" style="height: 150px; width: 250px; background-color: #f4f3f1;
                                color: #BDA797; border: 2px solid black; display: none">--%>
                            <div>
                                <b>
                                    <asp:Label ID="lblMsgClassRegistration" runat="server" Text=""></asp:Label></b><br />
                            </div>
                            <div>
                                <h1>Change Location</h1>
                                <br />
                                change location to:
                                <asp:DropDownList ID="cmbGroupNames" runat="server">
                                </asp:DropDownList>
                                <br />
                            </div>
                            <div align="right">
                                <br />
                                <asp:Button ID="btnSave" runat="server" Text="Submit" class="submitBtn" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="submitBtn" />
                            </div>
                            <%-- </div>--%>
                        </ContentTemplate>
                    </telerik:RadWindow>
                    <telerik:RadWindow ID="radExamWindow" runat="server" Width="400px" Height="200px"
                        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="Change Location" Behavior="None">
                        <ContentTemplate>
                            <div>
                                <b>
                                    <asp:Label ID="lblExamMsg" runat="server" Text=""></asp:Label></b><br />
                            </div>
                            <div>
                                <h1>Change Location</h1>
                                <br />
                                <asp:Label ID="lblChangeLocation" runat="server" Text="Change Location of all Autumn Exams to"></asp:Label>
                                <asp:DropDownList ID="drpExamLocation" runat="server">
                                </asp:DropDownList>
                                <br />
                            </div>
                            <div align="right">
                                <br />
                                <asp:Button ID="btnExamLocationSave" runat="server" Text="Save" class="submitBtn" />
                                <asp:Button ID="btnExamLocationCancel" runat="server" Text="Cancel" class="submitBtn" />
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>
                    <telerik:RadWindow ID="radExamChangeSession" runat="server" Width="700" Height="300"
                        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="Change Session" Behavior="None">
                        <ContentTemplate>
                            <div>
                                <p>
                                    <h1>Change Session</h1>
                                    <br />
                                    Please check the boxes aginst relevent Courses in order to change your registration
                                    to the Autumn Exam Session.
                                    <br />
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
                                                <%--  <Telerik:GridBoundColumn DataField="Session" HeaderText="Session" SortExpression="Session"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />--%>
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
                                    </telerik:RadGrid><br />
                                </p>
                            </div>
                            <div>
                                <b>
                                    <asp:Label ID="lblExamSessionMsg" runat="server" Text=""></asp:Label></b><br />
                                <b>Please Select the Location of your Autumn Exams:</b>
                                <br />
                                Change Location of All Selected Exams to :
                                <asp:DropDownList ID="drpAutumnSession" runat="server">
                                </asp:DropDownList>
                                <br />
                            </div>
                            <div align="right">
                                <br />
                                <asp:Button ID="btnExamSessionSave" runat="server" Text="Submit" class="submitBtn" />
                                <asp:Button ID="btnBack" runat="server" Text="Cancel" class="submitBtn" />
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>
                    <telerik:RadWindow ID="radAlternativeLocation" runat="server" Width="400px" Height="100px"
                        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="Change Location" Behavior="None">
                        <ContentTemplate>
                            <%-- <div id="IAdiv" style="height: 150px; width: 250px; background-color: #f4f3f1;
                                color: #BDA797; border: 2px solid black; display: none">--%>
                            <div>
                                <b>
                                    <asp:Label ID="lblAlternativeMessage" runat="server" Text=""></asp:Label></b><br />
                            </div>
                            <div align="right">
                                <br />
                                <asp:Button ID="btnAlternativeOk" runat="server" Text="Ok" class="submitBtn" />
                            </div>
                            <%-- </div>--%>
                        </ContentTemplate>
                    </telerik:RadWindow>
                    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
                    <cc1:User ID="User1" runat="server" />
                </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
