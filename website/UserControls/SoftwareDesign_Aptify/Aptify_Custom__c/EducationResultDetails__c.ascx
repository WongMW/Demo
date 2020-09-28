<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.EducationResultDetails__c"
    CodeFile="~/UserControls/Aptify_Custom__c/EducationResultDetails__c.ascx.vb" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<script type="text/javascript">

    function CollapseExpand(me, HiddenPanelState, header) {
        $('#' + me).slideToggle('slow');
    }

    function SetPanelState(HiddenPanelState, StateValue) {
        if (HiddenPanelState == 'hdnCurricula') {
            $("#<%=hdnCurricula.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnInterim') {
            $("#<%=hdnInterim.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnMock') {
            $("#<%=hdnMock.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnCurrent') {
            $("#<%=hdnCurrent.clientID %>").val(StateValue);
        }
    }

    function CollapseAll(hidnID) {

        //For First Div
        var FirstPanel = $("#first").attr("class");
        $("#first").show('slow');
        $("#first").removeClass("collapse").addClass("active");
        SetPanelState('hdnCurricula', 1)


        //For second Div
        var SecondPanel = $("#divInterim").attr("class");
        $("#divInterim").show('slow');
        $("#divInterim").removeClass("collapse").addClass("active");
        SetPanelState('hdnInterim', 1)

        //For Third Div
        var ThirdPanel = $("#divMock").attr("class");
        $("#divMock").show('slow');
        $("#divMock").removeClass("collapse").addClass("active");
        SetPanelState('hdnMock', 1)

        //For Third Div
        var FourthPanel = $("#divCurrent").attr("class");
        $("#divCurrent").show('slow');
        $("#divCurrent").removeClass("collapse").addClass("active");
        SetPanelState('hdnCurrent', 1)
    }
</script>
<div class="content-container clearfix">
    <d>
        <div>
            <span class="label-title-inline">Name:</span>
            <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label><br />
            <span class="label-title-inline">Student number: </span>
            <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label>
        </div>

        <div class="actions" id="rdoAcademicCycle" runat="server" visible="false">
            <asp:RadioButton ID="rdoCurrentAcademicCycle" runat="server" GroupName="AcadmicCycle"
                Text="Current academic cycle: " AutoPostBack="true" Checked="true" />
            <asp:RadioButton ID="rdoNextAcadmicCycle" runat="server" GroupName="AcadmicCycle"
                Text="Next academic cycle: " AutoPostBack="true" />
        </div>
        <div class="actions">
          Academic year: <asp:DropDownList ID="ddlAcademicYear" 
          			        runat="server" AutoPostBack="true" Width="8%" >
			             </asp:DropDownList>     
            </div>
       <asp:HyperLink ID="HyperLink1" runat="server" class="expand submitBtn" onclick="CollapseAll(this)">Expand all</asp:HyperLink><br />
            <asp:Label ID="lblStatusMessage" runat="server" Text="" Font-Bold="true"></asp:Label><br />
            <asp:Label ID="lblCommentsMessage" runat="server" Text="" visible="false"></asp:Label>
        


        <div class="demo cai-form">
		<div style="display:none">
            <h2 id="firstHead" class="expand form-title" onclick="CollapseExpand('first','hdnCurricula',this)" >Completed and attempted curricula</h2>
            <div id="first" class="collapse cai-form-content">
                <div class="cai-table mobile-table">
                    <asp:Repeater ID="rpEducationResult" runat="server">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th class="rgHeader">Curriculum</th>
                                    <th class="rgHeader">Historical result</th>
                                    <th class="rgHeader">Location</th>
                                    <th class="rgHeader" style='display:none;'>Academic year started</th>
                                    <th class="rgHeader" style='display:none;'>Academic year completed</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <tr>
                                <td>
                                    <span class="mobile-label">Curriculum:</span>
                                    <asp:Label CssClass="cai-table-data" ID="lblCurriculum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Curriculum") %>'></asp:Label>
                                    <asp:Label CssClass="cai-table-data" ID="lblCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>' Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <span class="mobile-label">Historical result:</span>
                                    <asp:Label CssClass="cai-table-data" ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'></asp:Label>
                                </td>
                                <td>
                                    <span class="mobile-label">Location:</span>
                                    <asp:Label CssClass="cai-table-data" ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                </td>
                                <td style='display:none;'>
                                    <span class="mobile-label">Academic year started:</span>
                                    <asp:Label CssClass="cai-table-data" ID="lblAcademicYearStarted" runat="server" Text='<%# Eval("AcademicYearStarted") %>'></asp:Label>

                                </td>
                                <td style='display:none;'>
                                    <span class="mobile-label">Academic year completed:</span>
                                    <asp:Label CssClass="cai-table-data" ID="lblAcademicYearCompleted" runat="server" Text='<%# Eval("AcademicYearCompleted") %>'></asp:Label>
                                </td>
                            </tr>

                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <br />
                <asp:Label ID="lblErrorCourseCompleted" runat="server" Text=""></asp:Label>
            </div>
			</div>

            <h2 class="expand form-title" onclick="CollapseExpand('divInterim','hdnInterim',this)">Interim assessments</h2>
            <div class="collapse cai-form-content" id="divInterim">
                <div class="cai-table mobile-table">
                    <asp:Repeater ID="rptInterimAssessments" runat="server">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th colspan="8">Curriculum:
                                    <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                        <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                            Visible="false" />
                                    </th>
                                </tr>

                                <asp:Repeater ID="rpInterimDetails" runat="server">
                                    <HeaderTemplate>
                                        <tr>
                                            <th class="rgHeader">Course</th>
                                            <th class="rgHeader">Location</th>
                                            <th class="rgHeader">Session</th>
                                            <th id="ScorResult" runat="server" class="rgHeader">Score</th>
                                            <th id="FAEResult" runat="server" visible="false" class="rgHeader">Result</th>
												<%--added by Harish Redmine 20672 - added column to get purchase re-sit link for FA Interim Assessment if the student failed --%>
											 <th id="Results" runat="server" class="rgHeader">Result(Pass/Failed)</th>
												<%--End here -Added by Harish Redmine 20672 - added column to get purchase re-sit link for FA Interim Assessment if the student failed --%>
                                            <th id="topScore" runat="server" visible="false" class="rgHeader">Top scorer</th>
                                            <th id="thIntrimTopScorerBlank" runat="server" visible="false" class="rgHeader"></th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <span class="mobile-label">Course:</span>
                                                <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Location:</span>
                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Session:</span>
                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'></asp:Label>
                                            </td>
                                            <td id="tdScore" visible="false" runat="server">
                                                <span class="mobile-label">Score:</span>
                                                <asp:Label ID="lblScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                            </td>
                                            <td runat="server" id="tdFAEResult" visible="false">
                                                <span class="mobile-label">Result:</span>
                                                 <asp:Label ID="lblFAEStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FAEIAPublishedScore__c") %>' Tooltip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                            </td>
											<%--added by Harish Redmine 20672 - added column to get purchase re-sit link for FA Interim Assessment if the student failed --%>
											<td runat="server" id="tdResults">
                                                <span class="mobile-label">Result(Pass/Failed)</span>
                                                <asp:Label ID="lblStatus" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IsReport") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                                <asp:Label ID="lblCourseID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>'
                                                    ></asp:Label>
												<b><asp:LinkButton ID="lnkEassessment" Font-Size="13px" runat="server" Text="" visible="false" ></asp:LinkButton></b> 
                                            </td>
											<%--Code end here by Harish Redmine 20672 added column to get purchase re-sit link for FA Interim Assessment if the student failed--%>
                                            <td id="tdTopScoreComments" runat="server" visible="false">
                                                <span class="mobile-label">Top scorer:</span>
                                                <asp:Label ID="lblTopScorere" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Top Scorer") %>'></asp:Label>
                                            </td>
                                            <td id="tdTopScoreBlank" runat="server" visible="false"></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <br />
                <asp:Label ID="lblInterimErrorMsg" runat="server" Text=""></asp:Label>
            </div>

            <h2 class="expand form-title" onclick="CollapseExpand('divMock','hdnMock',this)">Mock exam</h2>
            <div class="collapse cai-form-content" id="divMock">
                <div class="cai-table mobile-table">
                    <asp:Repeater ID="rpMockExam" runat="server">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th colspan="8">Curriculum:
                                        <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                        <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                            Visible="false" />
                                    </th>
                                </tr>

                                <asp:Repeater ID="rpMockExamDetails" runat="server">
                                    <HeaderTemplate>
                                        <tr>
                                            <th class="rgHeader">Course</th>
                                            <th class="rgHeader">Location</th>
                                            <th  class="rgHeader" id="ScorResult" runat="server">Score</th>
                                            <th  class="rgHeader" id="FAEResult" runat="server" visible="false">Result</th>
                                            <th  class="rgHeader" id="topScore" runat="server" visible="false">Top scorer</th>
                                            <th  class="rgHeader" id="thMockTopScorerBlank" runat="server" visible="false"></th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <span class="mobile-label">Course:</span>
                                                <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Location:</span>
                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                            </td>
                                            <td id="tdScore" visible="false" runat="server">
                                                <span class="mobile-label">Score:</span>
                                                <asp:Label ID="lblScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                            </td>
                                            <td runat="server" id="tdFAEResult" visible="false">
                                                <span class="mobile-label">Result:</span>
                                                <asp:Label ID="lblFAEStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FAEStatus") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                            </td>
                                            <td id="tdTopScoreComments" runat="server" visible="false">
                                                <span class="mobile-label">Top scorer:</span>
                                                <asp:Label ID="lblTopScorere" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Top Scorer") %>'></asp:Label>
                                            </td>
                                            <td id="tdMockTopScoreBlank" runat="server" visible="false"></td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <br />
                <asp:Label ID="lblErrorMockExam" runat="server" Text=""></asp:Label>
            </div>

            <h2 class="expand form-title" onclick="CollapseExpand('divCurrent','hdnCurrent',this)">Current exam</h2>
            <div class="collapse cai-form-content" id="divCurrent">

                <div class="cai-table mobile-table">
                    <telerik:RadGrid ID="grdCurrentExam" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                        SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                        AllowFilteringByColumn="True" Visible="false">
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <telerik:GridBoundColumn DataField="Course" HeaderText="Course" SortExpression="Course"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <telerik:GridBoundColumn DataField="Location" HeaderText="Location" SortExpression="Location"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <telerik:GridBoundColumn DataField="Session" HeaderText="Session" SortExpression="Session"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <telerik:GridTemplateColumn HeaderText="Result" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        <asp:Label ID="lblIsReport" runat="server" Text='<%# Eval("IsReport") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkInterimUpdateGroup" runat="server" Text="Request"
                                            CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("CourseID")  & "," & Eval("ClassID") & "," & Eval("ClassRegistrationID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="Select" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" HeaderText="Report">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpReports" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="ReportDownload" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" HeaderText="Report download">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Report download"
                                            CommandName="DownloadReport" Font-Underline="true"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <asp:Repeater ID="rpCurrentExam" runat="server">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th colspan="9">Curriculum:
                                        <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                        <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server" Visible="false" />
                                    </th>
                                </tr>
                                <tr id="trSummerCommentsID" visible='<%# IIf(Eval("OverallHighScore") = "", false, true)%>'>
                                    <th colspan="9">
                                        <asp:Label ID="lblSummerSessionComment" runat="server" Text='<%# Eval("OverallHighScore") %>'></asp:Label>
                                    </th>
                                </tr>
                                <tr id="trAutumnCommentsID" visible='<%# IIf(Eval("AutumnComments") = "", false, true)%>'>
                                    <th colspan="9">
                                        <asp:Label ID="lblAutumnComment" Text='<%# Eval("AutumnComments") %>' runat="server" />
                                    </th>
                                </tr>
                                <asp:Repeater ID="rpCurrentExamDetails" runat="server" OnItemCommand="rpCurrentExamDetails_ItemCommand">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th class="rgHeader">Course</th>
                                                <th class="rgHeader">Location</th>
                                                <th class="rgHeader">Session</th>
                                                <th class="rgHeader" id="ScorResult" runat="server">Score</th>
                                                <th class="rgHeader" id="thBlank" runat="server" visible="false"></th>
                                                <th class="rgHeader" id="FAEResult" runat="server" visible="false">Result</th>
                                                <th class="rgHeader" id="ExamComments" runat="server" visible="false">Comments</th>
                                                <th class="rgHeader" id="topScore" runat="server" visible="false">Top scorer</th>
                                                <th class="rgHeader" id="thExamTopScorerBlank" runat="server" visible="false"></th>
                                                <th class="rgHeader">Status</th>
                                                <th class="rgHeader">Information scheme report</th>
                                                 <%-- commented by GM for Redmine #19841 and added for Grounds   --%>
                                               <%-- <th class="rgHeader">Extenuating circumstances</th>--%>
                                                <th class="rgHeader">Grounds (i) & (ii)</th>
                                                <%-- End Redmine #19841 --%>
                                                <th class="rgHeader">Clerical recheck</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <span class="mobile-label">Course:</span>
                                                <asp:Label ID="lblRowID" runat="server" Text='<%# Container.ItemIndex + 1 %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblCourse" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'></asp:Label>
                                                <asp:Label ID="lblCourseID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassID") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblClassRegistrationID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassRegistrationID") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblSessionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SessionID") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblExamNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExamNumber") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderID") %>'
                                                    Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Location:</span>
                                                <asp:Label ID="Label1" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Session:</span>
                                                <asp:Label ID="Label2" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'></asp:Label>
                                            </td>
                                            <td id="tdScore" visible="false" runat="server">
                                                <span class="mobile-label">Score:</span>
                                                <asp:Label ID="lblScore" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                            </td>
                                            <td id="tdBlank" visible="false" runat="server">
                                                <span class="mobile-label">Score:</span>
                                                <asp:Label ID="Label3" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td runat="server" id="tdFAEResult" visible="false">
                                                <span class="mobile-label">FAE result:</span>
                                                <asp:Label ID="lblFAEStatus" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FAEStatus") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                            </td>
                                            <td id="tdComments" runat="server" visible="false">
                                                <span class="mobile-label">Comments:</span>
                                                <asp:Label ID="lblComments" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                            </td>
                                            <td visible="false" runat="server" id="tdTopScoreComments">
                                                <span class="mobile-label">Top scorer:</span>
                                                <asp:Label ID="lblTopScorere" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Top Scorer") %>'></asp:Label>
                                            </td>
                                            <td id="tdExamTopScoreBlank" runat="server" visible="false"></td>
                                            <td>
                                                <span class="mobile-label">Status:</span>
                                                <asp:Label ID="lblStatus" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                                <b><asp:LinkButton ID="lnkFAEDEtails" ForeColor="Blue" Font-Underline="True" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>' Tooltip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' Visible="false"  CommandName="FAEStatus" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ClassRegistrationID") %>' > </asp:LinkButton></b>
                                                <b><asp:LinkButton ID="lnkEassessment" Font-Size="13px" runat="server" Text="" Visible="false" ></asp:LinkButton></b> <%--Added as part of #20672--%>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Information scheme report:</span>
                                                <asp:DropDownList ID="drpSchemeReport" CssClass="cai-table-data" runat="server">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblSchemeReportStatus" runat="server" Text="" Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lnkSchemeReportStatus" runat="server" Text="" CommandName="Scheme Report Status"
                                                    Visible="false"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkDownload" runat="server" CommandName="Download" Visible="false">Download report</asp:LinkButton>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Extenuating circumstances:</span>
                                                <asp:LinkButton ID="lnkCircumstances" CssClass="cai-table-data" runat="server" Text="Request" OnClick="lnkCircumstances_OnClick"
                                                    Visible="false"></asp:LinkButton>
                                                <%-- commented by GM for Redmine #19841 and added for Grounds   --%>
                                              <%--  <asp:LinkButton ID="lnkCircumstancess" runat="server" CommandName="Extenuating Circumstances"
                                                    Text="Request" Visible="false" CssClass="cai-table-data"></asp:LinkButton>--%>
                                                 <asp:LinkButton ID="lnkCircumstancess" runat="server" CommandName="Grounds (i) & (ii)"
                                                    Text="Request" Visible="false" CssClass="cai-table-data"></asp:LinkButton>
                                                 <%-- End Redmine #19841    --%>
                                                <asp:Label ID="lblCircumstancess" CssClass="cai-table-data" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <span class="mobile-label">Clerical recheck:</span>
                                                <asp:LinkButton ID="lnkClericalRecheck" CssClass="cai-table-data" runat="server" Text="Request" CommandName="Clerical Recheck"></asp:LinkButton>
                                                <asp:Label ID="lblClericalRecheck" CssClass="cai-table-data" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </table>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <SeparatorTemplate>
                            <hr />
                        </SeparatorTemplate>
                    </asp:Repeater>
                </div>

              <div class="actions">
                    <asp:Label ID="lblDownloadFormatMessage" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblRagisteredInterimAssessments" runat="server" Text=""></asp:Label>
              </div>
              <%--	Added by GM for Redmine log #19143--%>
				<div class="actions">
					<asp:LinkButton ID="lnkReport" runat="server" Font-Bold="true"  style ="color:#00AFA9" >Exam result letter </asp:LinkButton>
				</div>
			<%--	end by GM for Redmine log #19143--%>
            </div>
        </div>

        <div class="actions"> <asp:LinkButton ID="lbtnCourseMaterial" runat="server" Text="Course material" Font-Bold="true" Font-Underline="true"></asp:LinkButton></div>
        <div class="actions">           
            <asp:Button ID="btnSubmitPay" runat="server" Text="Submit/Pay" CssClass="submitBtn" />
        </div>
        <asp:Label ID="lblError" runat="server" Visible="False" />
        <cc1:User ID="User1" runat="server" />
        <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
    </div>
    <asp:HiddenField ID="hdnCurricula" runat="server" Value="0" />
    <asp:HiddenField ID="hdnInterim" runat="server" Value="0" />
    <asp:HiddenField ID="hdnMock" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCurrent" runat="server" Value="0" />
</div>
