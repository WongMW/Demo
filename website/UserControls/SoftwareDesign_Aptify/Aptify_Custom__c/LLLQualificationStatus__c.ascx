<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/LLLQualificationStatus__c.ascx.vb"
    Inherits="LLLQualificationStatus__c" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="EBizUser" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="ucRecordAttachment"
    TagName="RecordAttachments__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>

<style type="text/css">
    .active
    {
        display: block;
    }
    .inactive
    {
        display: none;
    }
    .collapse
    {
        display: none;
    }
    .expand
    {
        cursor: pointer;
    }
</style>
<script type="text/javascript">

    $(document).ready(function () {

        var PanelState1 = $("#<%=hdnTimeTableState.ClientID%>").val();

        var PanelState2 = $("#<%=hdnAttendanceState.ClientID%>").val();
        var PanelState3 = $("#<%=hdnAssignmentState.ClientID%>").val();
        var PanelState4 = $("#<%=hdnExamState.ClientID%>").val();
        var PanelState5 = $("#<%=hdnQualificationState.ClientID%>").val();


        if (PanelState1 == '1') {
            $('#divTimeTable').removeClass("collapse").addClass("active");
        }

        if (PanelState2 == '1') {
            $('#divAttendance').removeClass("collapse").addClass("active");
        }

        if (PanelState3 == '1') {
            $('#divAssignment').removeClass("collapse").addClass("active");
        }
        if (PanelState4 == '1') {
            $('#divExam').removeClass("collapse").addClass("active");
        }

        if (PanelState5 == '1') {
            $('#divQualification').removeClass("collapse").addClass("active");
        }

    });

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
            $("#<%=" + panel.clientID + "%>").val("0");
        }

    }
    function SetPanelState(HiddenPanelState, StateValue) {
        if (HiddenPanelState == 'hdnTimeTableState') {
            $("#<%=hdnTimeTableState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnAttendanceState') {
            $("#<%=hdnAttendanceState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnAssignmentState') {
            $("#<%=hdnAssignmentState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnExamState') {
            $("#<%=hdnExamState.clientID %>").val(StateValue);
        }

        if (HiddenPanelState == 'hdnQualificationState') {
            $("#<%=hdnQualificationState.clientID %>").val(StateValue);
        }
    }

   
</script>
<%--<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>--%>
<asp:HiddenField ID="hfClassRegId" runat="server" Value="0" />
<asp:HiddenField ID="hfIsFAE" runat="server" Value="0" />
<%--<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1000px;">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>--%>
<div class="content">
<div class="content-container clearfix">
    <div class="cai-table">
    <asp:Panel ID="pnlData" runat="server">
        <div class="info-data">
            <asp:Label ID="lblClassRegNotFound" runat="server"></asp:Label>
            <div class="row-div clearfix">
                <rad:RadGrid ID="grdClassReg" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                    AllowSorting="false" SortingSettings-SortedAscToolTip="Sorted Ascending">
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Course" DataField="CourseName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkCourse" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CourseName") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Class Start Date" DataField="StartDate" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StartDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Class End Date" DataField="EndDate" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EndDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Status" DataField="Status" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDetails" runat="server">
        <div>
            <div>
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                <asp:HiddenField ID="hdnTimeTableState" runat="server" Value="1" />
                <asp:HiddenField ID="hdnAttendanceState" runat="server" Value="0" />
                <asp:HiddenField ID="hdnAssignmentState" runat="server" Value="0" />
                <asp:HiddenField ID="hdnExamState" runat="server" Value="0" />
                <asp:HiddenField ID="hdnQualificationState" runat="server" Value="0" />
            </div>
            <div align="center" style="background-color: GrayText">
                <div class="info-data">
                    <div class="row-div clearfix">
                        <div class="label-div w5">
                            &nbsp;
                        </div>
                        <div class="label-div w160">
                            <asp:Label ID="lblStudentNumber" runat="server" Text="Person/Exam Number:" Font-Bold="true"
                                ForeColor="White"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblStudentNumberValue" runat="server" Text="" ForeColor="White"></asp:Label>
                        </div>
                        <div class="label-div w15">
                            &nbsp;
                        </div>
                        <div class="label-div w160">
                            <asp:Label ID="lblFirstLast" runat="server" Text="Name:" Font-Bold="true" ForeColor="White"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblFirstLastValue" runat="server" Text="" ForeColor="White"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="info-data">
                <div class="row-div clearfix">
                    <div class="label-div-left-align w50">
                        <div class="info-data">
                            <div class="row-div clearfix">
                                <div style="text-align: left;" class="label-div w10">
                                    <asp:Label ID="lblCourseName" runat="server" Text="Subject:" Font-Bold="true"></asp:Label></b>
                                </div>
                                <div class="field-div w80">
                                    <asp:Label ID="lblCourseNameValue" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div style="text-align: left;" class="label-div w10">
                                    <asp:Label ID="lblType" runat="server" Text="Type:" Font-Bold="true"></asp:Label></b>
                                </div>
                                <div class="field-div w80">
                                    <asp:Label ID="lblTypeName" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="field-div1 w40">
                        <div class="info-data">
                            <div class="row-div clearfix">
                                <div class="label-div w30">
                                </div>
                                <div style="text-align: left;" class="label-div w20">
                                    Start Date:
                                </div>
                                <div class="field-div w50">
                                    <asp:Label ID="lblStartDateValue" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w30">
                                </div>
                                <div style="text-align: left;" class="label-div w20">
                                    End Date:
                                </div>
                                <div class="field-div w50">
                                    <asp:Label ID="lblEndDateValue" runat="server" Text=""></asp:Label></b>
                                </div>
                                <div class="field-div w50">
                                    <asp:LinkButton ID="lnkEnrollChangesReq" Style="text-decoration: underline;" runat="server">Enrolment Change Request</asp:LinkButton></div>
                                <%-- <asp:HyperLink ID="lblFileImage" Text="Open" runat="server"></asp:HyperLink>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <h2 class="expand" id="HeaddivTimeTable" onclick="CollapseExpand('divTimeTable','hdnTimeTableState')">
            Time Table:</h2>
        <div id="divTimeTable" class="collapse">
            <div>
                <br />
            </div>
            <div>
                <asp:HiddenField ID="hfPartStatusID" runat="server" Value="0" />
                <telerik:RadGrid ID="gvCourseDetails" runat="server" AllowPaging="True" AllowSorting="false"
                    AllowFilteringByColumn="False" CellSpacing="0" GridLines="None" AutoGenerateColumns="false"
                    Width="99%">
                    <MasterTableView AllowSorting="false">
                        <Columns>
                            <telerik:GridBoundColumn SortExpression="Module" HeaderText="" HeaderButtonType="TextButton"
                                DataField="Module" HeaderStyle-Width="20%" ItemStyle-Width="20%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Type" HeaderText="Type" HeaderButtonType="TextButton"
                                DataField="Type" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Start Date" AllowFiltering="false" DataField="StartDate"
                                SortExpression="" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false" HeaderStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StartDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="8%" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn SortExpression="StartTime" HeaderText="Start Time" HeaderButtonType="TextButton"
                                DataField="StartTime" HeaderStyle-Width="8%" ItemStyle-Width="8%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="End Date" AllowFiltering="false" DataField="EndDate"
                                SortExpression="" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false" HeaderStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EndDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="8%" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn SortExpression="EndTime" HeaderText="End Time" HeaderButtonType="TextButton"
                                DataField="EndTime" HeaderStyle-Width="8%" ItemStyle-Width="8%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Status" HeaderText="Status" HeaderButtonType="TextButton"
                                DataField="Status" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Venue" HeaderText="Venue" HeaderButtonType="TextButton"
                                DataField="Venue" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="My Notes" HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAdd" runat="server" Text="Click here" CommandName="AddNotes"
                                        CommandArgument='<%# Eval("ClassRegPartStatusID")%>'></asp:LinkButton>
                                    <asp:HiddenField ID="hidIsExam" runat="server" Value='<%# Eval("IsFinalExam")%>' />
                                    <asp:HiddenField ID="hidIsAssignment" runat="server" Value='<%# Eval("IsAssignment")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Course Material" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkGvDownload" runat="server" Text="Download" CommandName="Download"
                                        CommandArgument='<%# Eval("CoursePartID")%>'></asp:LinkButton>
                                </ItemTemplate>
                                <%-- Visible='<%# IIf(Eval("IsAssignment")=true,true,false) %>'--%>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Assignment" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAssignment" runat="server" Text="Click here" CommandName="Assignment"
                                        CommandArgument='<%# Eval("EndDate") & ";" & Eval("ClassRegPartStatusID") & ";" & Eval("CoursePartID")%>'
                                        Visible='<%# IIf(Eval("IsAssignment")=true,true,false) %>' Enabled='<%# IIf((Eval("IsAvailable"))=1,true,false) %>'
                                        ForeColor='<%# IIf((Eval("IsAvailable"))=1, System.Drawing.Color.FromName("Black"), System.Drawing.Color.FromName("Gray")) %>'></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div>
                <br />
            </div>
        </div>
        <div id="divAttendanceStatus" runat="server">
            <h2 class="expand" id="HeaddivAttendance" onclick="CollapseExpand('divAttendance','hdnAttendanceState')">
                Attendance:</h2>
            <div id="divAttendance" class="collapse">
                <div>
                    <br />
                </div>
                <div class="info-data">
                    <div class="row-div clearfix">
                        <div class="label-div w125">
                            <div runat="server" style="font-weight: bold;" id="divAttendenceReq">
                            </div>
                        </div>
                        <div class="label-div w8">
                            &nbsp;
                        </div>
                        <div style="display:none;" class="label-div w160">
                            Status : &nbsp;<asp:Label ID="lblAttendenceStatus" runat="server" Text=""></asp:Label>
                            <%--<asp:Image ID="imgAttendenceStatus" runat="server" ImageUrl="~/Images/Help.png" />--%>
                            &nbsp;<asp:LinkButton ID="lnkAttendenceStatushelp" ToolTip="Help" Style="font-size: 12pt;
                                font-weight: bold;" runat="server">?</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <telerik:RadGrid ID="gvAttendanceStatus" runat="server" AllowPaging="false" AllowSorting="false"
                    AllowFilteringByColumn="False" CellSpacing="0" GridLines="None" AutoGenerateColumns="false"
                    Width="99%">
                    <MasterTableView AllowSorting="false">
                        <Columns>
                            <telerik:GridBoundColumn SortExpression="Attendance" HeaderText="" HeaderButtonType="TextButton"
                                DataField="Attendance" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-Font-Bold="true"
                                AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Target" HeaderText="Target" HeaderButtonType="TextButton"
                                DataField="Target" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Actual" HeaderText="Actual" HeaderButtonType="TextButton"
                                DataField="Actual" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="OutStanding" HeaderText="Outstanding" HeaderButtonType="TextButton"
                                DataField="OutStanding" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div>
                    <br />
                </div>
            </div>
        </div>
        <div id="divAssignmentMain" runat="server">
            <h2 class="expand" id="HeaddivAssignment" onclick="CollapseExpand('divAssignment','hdnAssignmentState')">
                Assignment:</h2>
            <div id="divAssignment" class="collapse">
                <div>
                    <br />
                </div>
                <div class="info-data">
                    <div class="row-div clearfix">
                        <div class="label-div w50">
                            &nbsp;
                        </div>
                        <div style="display:none;" class="label-div w185">
                            Status : &nbsp;<asp:Label ID="lblAssignmentStatus" runat="server" Text=""></asp:Label>
                            &nbsp;<asp:LinkButton ID="lnkAssignmenthelp" ToolTip="Help" Style="font-size: 12pt;
                                font-weight: bold;" runat="server">?</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <telerik:RadGrid ID="gvAssignmentStatus" runat="server" AllowPaging="false" AllowSorting="false"
                    AllowFilteringByColumn="False" CellSpacing="0" GridLines="None" ShowFooter="false"
                    AutoGenerateColumns="false" Width="99%">
                    <MasterTableView AllowSorting="false">
                        <Columns>
                            <telerik:GridBoundColumn SortExpression="Module" HeaderText="" HeaderButtonType="TextButton"
                                DataField="Module" HeaderStyle-Width="20%" ItemStyle-Width="5%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Percentage" HeaderText="Result(%)" HeaderButtonType="TextButton"
                                DataField="Percentage" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="AssignWeightPer__c" HeaderText="Assignment Weighting(%)"
                                HeaderButtonType="TextButton" DataField="AssignWeightPer__c" HeaderStyle-Width="15%"
                                ItemStyle-Width="15%" AllowSorting="false">
                            </telerik:GridBoundColumn>
<telerik:GridBoundColumn SortExpression="WeightPer__c" HeaderText="Weighted Result(%)"
                                HeaderButtonType="TextButton" DataField="WeightPer__c" HeaderStyle-Width="15%"
                                ItemStyle-Width="15%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Status" AllowSorting="false" HeaderText="Status"
                                HeaderButtonType="TextButton" DataField="Status" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Feedback" HeaderText="Feedback" HeaderButtonType="TextButton"
                                DataField="Feedback" HeaderStyle-Width="15%" ItemStyle-Width="15%" AllowSorting="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <FooterStyle Font-Bold="true" />
                    </MasterTableView>
                </telerik:RadGrid>
                <div>
                    <br />
                </div>
            </div>
        </div>
        <h2 class="expand" id="HeaddivExam" onclick="CollapseExpand('divExam','hdnExamState')">
            Exam:</h2>
        <div id="divExam" class="collapse">
            <div>
                <br />
            </div>
            <div class="info-data">
                <div class="row-div clearfix">
                    <div class="label-div w50">
                        &nbsp;
                    </div>
                    <div style="display:none;" class="label-div w185">
                        Status :&nbsp;<asp:Label ID="lblExamStatus" runat="server" Text=""></asp:Label>
                        &nbsp;<asp:LinkButton ID="lnkExamStatushelp" ToolTip="Help" Style="font-size: 12pt;
                            font-weight: bold;" runat="server">?</asp:LinkButton>
                    </div>
                </div>
            </div>
            <telerik:RadGrid ID="gvExamStatus" runat="server" AllowPaging="false" AllowSorting="false"
                AllowFilteringByColumn="False" CellSpacing="0" GridLines="None" AutoGenerateColumns="false"
                Width="99%">
                <MasterTableView AllowSorting="false">
                    <Columns>
                        <telerik:GridBoundColumn SortExpression="Module" HeaderText="" HeaderButtonType="TextButton"
                            DataField="Module" HeaderStyle-Width="20%" ItemStyle-Width="5%" AllowSorting="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn SortExpression="Percentage" HeaderText="Result(%)" HeaderButtonType="TextButton"
                            DataField="Percentage" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                        </telerik:GridBoundColumn>

<telerik:GridBoundColumn SortExpression="ExamWeightPer" HeaderText="Exam Weighting(%)" HeaderButtonType="TextButton"
                            DataField="ExamWeightPer" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn SortExpression="WeightResultPer" HeaderText="Weighted Result(%)" HeaderButtonType="TextButton"
                            DataField="WeightResultPer" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                        </telerik:GridBoundColumn>

                        <%--  <telerik:GridBoundColumn SortExpression="Status" HeaderText="Status" HeaderButtonType="TextButton"
                            DataField="Status" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn HeaderText="Status" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Information Scheme Report" HeaderStyle-Width="10%"
                            ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSchemeReportStatus" runat="server" Text="Request" CommandName="Tutorial Report"
                                    CommandArgument='<%# Eval("CoursePartID")%>' Visible='<%# IIf(Eval("IsFailed")=1,true,false) %>'></asp:LinkButton>
                                <asp:Label ID="lblRowID" runat="server" Text='<%# Container.ItemIndex + 1 %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblSchemeReportStatus" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblCourse" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Module") %>'></asp:Label>
                                <asp:Label ID="lblCourseID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassID") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblClassRegistrationID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassRegistrationID") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblExamNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExamNumber") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderID") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblSessionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SessionID") %>'
                                    Visible="false"></asp:Label>
                                <asp:LinkButton ID="lnkDownload" runat="server" CommandName="Download" Visible='<%# IIf(Eval("IsFailed")=1,true,false) %>'>Download Report</asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Extenuating Circumstances" HeaderStyle-Width="10%"
                            ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCircumstancess" runat="server" Text="Request" CommandName="Extenuating Circumstances"
                                    CommandArgument='<%# Eval("CoursePartID")%>' Visible='<%# IIf(Eval("IsFailed")=1,true,false) %>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Clerical Recheck" HeaderStyle-Width="10%"
                            ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkClericalRecheck" runat="server" Text="Request" CommandName="Clerical Recheck"
                                    CommandArgument='<%# Eval("CoursePartID")%>' Visible='<%# IIf(Eval("IsFailed")=1,true,false) %>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Book Request Exam" HeaderStyle-Width="10%"
                            ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBRERequest" runat="server" Text="Request" CommandName="Book Repeat Exam"
                                    CommandArgument='<%# Eval("CoursePartID")%>' Visible='<%# IIf(Eval("IsFailed")=1,true,false) %>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div>
                <br />
            </div>
        </div>
        <div id="divQualificationMain" visible="false" runat="server">
            <h2 class="expand" id="HeaddivQualification" style="height: 10px;" onclick="CollapseExpand('divQualification','hdnQualificationState')">
                Overall:</h2>
            <div id="divQualification" class="collapse">
                <div>
                    <br />
                </div>
                <div class="info-data">
                    <div class="row-div clearfix">
                        <div class="label-div w50">
                            &nbsp;
                        </div>
                        <div style="display:none;" class="label-div w160">
                            Status :&nbsp;<asp:Label ID="lblQualStatus" runat="server" Text=""></asp:Label>
                            <%-- <asp:Image ID="imgQualStatus" runat="server" ImageUrl="~/Images/Help.png" />--%>
                            &nbsp;<asp:LinkButton ID="lnkQualStatushelp" ToolTip="Help" Style="font-size: 12pt;
                                font-weight: bold;" runat="server">?</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div>
                    <b>Programme Status :</b>
                </div>
                <div>
                    <telerik:RadGrid ID="gvQualificationStatus" runat="server" AllowPaging="false" AllowSorting="false"
                        AllowFilteringByColumn="False" CellSpacing="0" ShowFooter="true" GridLines="None"
                        AutoGenerateColumns="false" Width="99%">
                        <MasterTableView AllowSorting="false">
                            <Columns>
                                <telerik:GridBoundColumn SortExpression="" HeaderText="" HeaderButtonType="TextButton"
                                    DataField="Module" HeaderStyle-Width="20%" ItemStyle-Width="5%" AllowSorting="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="" HeaderText="Weighted Result(%)" HeaderButtonType="TextButton"
                                    DataField="Percentage" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="" HeaderText="Overall Weighting(%)" HeaderButtonType="TextButton"
                                    DataField="AttendanceWeight__c" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                    AllowSorting="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="" HeaderText="Status" HeaderButtonType="TextButton"
                                    DataField="Status" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="" HeaderText="Placed" HeaderButtonType="TextButton"
                                    DataField="Placed" HeaderStyle-Width="10%" ItemStyle-Width="10%" AllowSorting="false">
                                </telerik:GridBoundColumn>
    <telerik:GridTemplateColumn HeaderText="Extenuating Circumstances" HeaderStyle-Width="10%" Visible="false"
                                    ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCircumstancess" runat="server" Text="Request" CommandName="Extenuating Circumstances"
                                            CommandArgument='<%# Eval("CoursePartID")%>' Visible='<%# IIf(Eval("IsFailed")=1,true,false) %>'></asp:LinkButton>
                                        <asp:Label ID="lblStatus" runat="server" Visible ="false" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'></asp:Label>
                                        <asp:Label ID="lblCourseID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassID") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblClassRegistrationID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassRegistrationID") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Clerical Recheck" HeaderStyle-Width="10%" Visible="false"
                                    ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkClericalRecheck" runat="server" Text="Request" CommandName="Clerical Recheck"
                                            CommandArgument='<%# Eval("CoursePartID")%>' Visible='<%# IIf(Eval("IsFailed")=1,true,false) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Book Request Exam" HeaderStyle-Width="10%" Visible="false"
                                    ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBRERequest" runat="server" Text="Request" CommandName="Book Repeat Exam"
                                            CommandArgument='<%# Eval("CoursePartID")%>' Visible='<%# IIf(Eval("IsFailed")=1,true,false) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>


                            </Columns>
                        </MasterTableView>
                        <FooterStyle Font-Bold="true" />
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-div w20">
                    <br />
                </div>
            </div>
        </div>
    </asp:Panel>
    <div style="text-align: right;">
        <asp:Button ID="btnSubmitPay" Visible="false" runat="server" Text="Submit" />
        <asp:Button ID="btnBack" Visible="false" runat="server" Text="Back" />
    </div>
    </div>
</div>
    </div>
<telerik:RadWindow ID="radDownloadDocuments" runat="server" Width="500px" Height="300px"
    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
    Title="Download Documents" Behavior="None">
    <ContentTemplate>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td width="5%">
                    </td>
                    <td width="90%">
                        <b>Documents</b><br />
                        <asp:Panel ID="pnlDownloadDocuments" runat="Server" Style="border: 1px Solid #000000;">
                            <table class="data-form" width="100%">
                                <tr>
                                    <td class="RightColumn">
                                        <ucRecordAttachment:RecordAttachments__c ID="ucDownload" runat="server" AllowView="True"
                                            AllowAdd="false" AllowDelete="False" ViewDescription="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td width="5%">
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="submitBtn" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindow ID="radAddNotes" runat="server" Width="400px" Height="250px" Modal="true"
    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" 
    Title="My Notes" Behavior="None">
    <ContentTemplate>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-div w20" align="left">
                    &nbsp;
                </div>
                <div class="field-div1 w60" align="left">
                    <asp:Label ID="lblAddNoteMessage" runat="server"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row-div clearfix">
                <div class="label-div w20" align="left">
                    <asp:Label ID="lblNote" runat="server">Notes:</asp:Label>
                </div>
                <div class="field-div1 w60" align="left">
                    <asp:TextBox ID="txtNotes" runat="server" Style="resize: none;" TextMode="MultiLine"
                        Width="110%"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="info-data">
            <div class="row-div
clearfix">
                <div class="label-div w60" align="left">
                    <asp:Button ID="btnAddNotes" Text="Ok" runat="server" CssClass="submit-Btn" />
                    <asp:Button ID="btnCloseAddNotes" Text="Cancel" runat="server" CssClass="submit-Btn" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindow ID="radAssignments" runat="server" Width="620px" Height="580px"
    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="True" Behaviors="None" ForeColor="#BDA797"
    Title="Download Documents" Behavior="None">
    <ContentTemplate>
        <div>
            <asp:Label ID="lblAssignmentMessage" runat="server"></asp:Label>
        </div>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-align-left-div
w20">
                </div>
                <div class="field-div1 w99">
                    <div class="info-data">
                        <div class="row-div
clearfix">
                            <div class="label-align-left-div w50">
                                <asp:Label ID="lblbAssignmentDueDate" runat="server" Text="Assignment Due Date:"
                                    Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblbAssignmentDueDateValue" runat="server" Text="[Date]" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-align-left-div w50">
                                <asp:Label ID="lblDaysRemaining" runat="server" Text="Days Remaining To Submission:"
                                    Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblDaysRemainingValue" runat="server" Text="[Days Cout]" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-align-center-div w50">
                                <br />
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-align-left-div w50">
                                <asp:Label ID="lbldlAssignments" runat="server" Text="Download Assignments" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="label-align-left-div w90">
                                <ucRecordAttachment:RecordAttachments__c ID="ucAssignmentDownload" runat="server"
                                    AllowView="True" AllowAdd="false" AllowDelete="False" ViewDescription="false" />
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-align-left-div
w30">
                                <br />
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-align-left-div
w50">
                                <asp:Label ID="lblUploadAssignments" runat="server" Text="Upload Assignments" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="label-align-left-div w90">
                                <ucRecordAttachment:RecordAttachments__c ID="ucAssignmentUpload" runat="server" AllowView="True"
                                    AllowAdd="True" AllowDelete="True" />
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w10">
                            </div>
                            <div class="field-div1 w90" align="right">
                                <asp:Button ID="btnCloseAssignment" Text="Cancel" runat="server" CssClass="submit-Btn" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-div clearfix">
                <div class="label-align-left-div w20">
                </div>
                <asp:Label ID="lblAssgnmtNote" runat="server" Text="Note :" Font-Bold="true"></asp:Label>
                <asp:Label ID="lblAssgnmtValue" runat="server" Text="[Note]" Font-Bold="true"></asp:Label>
            </div>
        </div>
       <%--  </div>
       </div> --%>
    </ContentTemplate>
</telerik:RadWindow>
<EBizUser:User ID="LoggedInUser" runat="server" />
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
