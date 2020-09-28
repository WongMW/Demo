<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FirmCourseEnrollment__c.ascx.vb"
    Inherits="FirmCourseEnrollment__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ajax" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<%--<script src="../../Scripts/jquery-ui-1.8.9.js" type="text/javascript"></script>--%>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-ui-1.11.2.min.js" type="text/javascript"></script>
<script src="../../Scripts/expand.js" type="text/javascript"></script>
<script src="../../Scripts/JScript.min.js" type="text/javascript"></script>
<script type="text/javascript">
    //hdnFAEUpdate
    $(document).ready(function () {
        var PanelState1 = document.getElementById('hdnTimeTable').value;
        var PanelState2 = document.getElementById('hdnFAEUpdate').value;
        if (PanelState1 == '1') {
            $('#divTimeTable').removeClass("collapse").addClass("active");
        }
        if (PanelState2 == '1') {
            $('#divFAEUpdate').removeClass("collapse").addClass("active");
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

        }

    }

    function SetPanelState(HiddenPanelState, StateValue) {
        if (HiddenPanelState == 'hdnTimeTable') {
            document.getElementById('hdnTimeTable').value = StateValue;
        }
        if (HiddenPanelState == 'hdnFAEUpdate') {
            document.getElementById('hdnFAEUpdate').value = StateValue;
        }
    }
    //    function a() {
    //        var temp = document.getElementById('hdnTimeTable');
    //        alert(temp.value);
    //    }
</script>
<style type="text/css">
    .style1
    {
        width: 142px;
    }
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
    .ui-draggable .ui-dialog-titlebar
    {
        background-image: none;
        background-color: rgb(231, 210, 182);
        color: Black;
        border: 1px solid #F4F3F1;
    }
    .ui-widget-content
    {
        border: 1px solid #F4F3F1;
    }
    .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default
    {
        background-image: none;
        background-color: blue;
        color: white;
        border: none;
    }
    .ui-state-default:hover
    {
        background-image: none;
        background-color: blue;
        color: white;
        border: none;
        font-weight: bolder;
    }
    .ui-dialog-content ui-widget-content
    {
        border: 1px solid #F4F3F1;
    }
    
    .ui-icon:hover
    {
        background-color: Blue;
    }
    
    .ui-dialog-buttonset
    {
        margin-right: 50%;
    }
</style>
<style type="text/css">
    input[type="checkbox"]
    {
        margin-top: 5px;
        margin-right: 5px;
        vertical-align: middle;
    }
    .chkBox label
    {
        position: absolute;
        display: none;
    }
    .RadPivotGrid td
    {
        padding: 0;
    }
    div.qsf-right-content .qsf-col-wrap
    {
        position: static;
    }
    .RadPivotGrid_Metro .rpgContentZoneDiv td
    {
        white-space: wrap;
    }
    .popup_Buttons
    {
        margin: 10px;
    }
    .gv_already_enrolled
    {
        width: 40px;
        height: 25px;
        background-color: #FFFF66;
    }
    .gv_passed
    {
        width: 40px;
        height: 25px;
        background-color: #006400;
    }
    .gv_failed
    {
        width: 40px;
        height: 25px;
        background-color: #890F0F;
    }
    .gv_can_enroll
    {
        width: 40px;
        height: 25px;
        background-color: Aqua;
    }
    .gv_notavailable
    {
        width: 40px;
        height: 25px;
        background-color: Gray;
    }
    .passed
    {
        width: 25px;
        height: 20px;
        background-color: #006400;
    }
    .passed_as_external
    {
        width: 25px;
        height: 20px;
        background-color: #8FBC8F;
    }
    .exemption_granted
    {
        width: 25px;
        height: 20px;
        background-color: #FFA07A;
    }
    .already_enrolled
    {
        width: 25px;
        height: 20px;
        background-color: #FFFF66;
    }
    .available
    {
        width: 25px;
        height: 20px;
        background-color: #00BFFF;
    }
    .not_available
    {
        width: 25px;
        height: 20px;
        background-color: #999999;
    }
    .alternate_location
    {
        width: 25px;
        height: 20px;
        background-color: #9370DB;
    }
    .GridDock
    {
        overflow-x: auto;
        overflow-y: hidden;
        width: 900px;
        padding: 0 0 17px 0;
        border-color: Black;
    }
    /*Added BY Pradip 2016-04-20 For BFP Scroll Bar Issue*/
    #page
    {
        width: 100% !important;
        padding: 0px;
        background: white; /*margin: 0px auto 0px auto;*/
    }
    
    #page-inner
    {
        width: 100% !important;
        margin: 0 auto;
        background-color: White; /*overflow: auto;*/
    }
    
    #home #content
    {
        background: -webkit-linear-gradient(top, #f0f0f0 100%,#ffffff 100%);
        width: 100% !important;
        padding: 10px;
        vertical-align: top;
        background-color: White;
        margin-top: 10px;
    }
    .RadMenu .rmRootGroup
    {
        width: 100% !important;
    }
</style>
<%--class="maindiv"--%>
<div>
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:HiddenField ID="hfStudentID" runat="server" Value="0" />
    <asp:HiddenField ID="hfAcademicCycleID" runat="server" Value="0" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:Label ID="lblMessage" Style="color: Red;" runat="server"></asp:Label>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-div w40" align="left">
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-div w30" align="left">
                                <asp:Label ID="lblAcademicYear" runat="server" Font-Bold="True" Font-Size="8" Text="Academic Year: "></asp:Label>
                            </div>
                            <div class="field-div1 w50" align="left">
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="false" Width="200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w30" align="left">
                                <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="8" Text="Enrollment Type: "></asp:Label>
                            </div>
                            <div class="field-div1 w50" align="left">
                                <asp:DropDownList ID="ddlEnrollmentType" runat="server" AutoPostBack="false" Width="200px">
                                    <asp:ListItem Text="All" Value="All">
                                
                                    </asp:ListItem>
                                    <asp:ListItem Text="New" Value="New">
                                
                                    </asp:ListItem>
                                    <asp:ListItem Text="Existing" Value="Existing">
                                
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w30" align="left">
                                <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="8" Text="Route Of Entry: "></asp:Label>
                            </div>
                            <div class="field-div1 w50" align="left">
                                <asp:DropDownList ID="ddlRouteOfEntry" runat="server" AutoPostBack="false" Width="200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w30" align="left">
                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="8" Text="Current Stage: "></asp:Label>
                            </div>
                            <div class="field-div1 w50" align="left">
                                <asp:DropDownList ID="ddlCurrentStage" runat="server" AutoPostBack="false" Width="200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w30" align="left">
                                <asp:Label ID="lblCodes" runat="server" Font-Bold="True" Font-Size="8" Text="Filter By: "></asp:Label>
                            </div>
                            <div class="field-div1 w50" align="left">
                                <asp:DropDownList ID="ddlCodesList" runat="server" Width="200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w30" align="left">
                                <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="8" Text="Subjects Failed: "></asp:Label>
                            </div>
                            <div class="field-div1 w50" align="left">
                                <div>
                                    <asp:DropDownList ID="ddlSubjectsFailed" runat="server" AutoPostBack="false" Width="200px">
                                        <asp:ListItem Text="No" Value="No">
                                
                                        </asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Yes">
                                
                                        </asp:ListItem>
                                        <asp:ListItem Text="Ignore" Value="Ignore">
                                
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w30" align="left">
                                <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Size="8" Text="Location: "></asp:Label>
                            </div>
                            <div class="field-div1 w50" align="left">
                                <div>
                                    <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="false" Width="200px">
                                    </asp:DropDownList>
                                </div>
                                <div>
                                    <br />
                                </div>
                                <div>
                                    <asp:Button ID="btnDisplay" runat="server" CssClass="submit-Btn" Text="Display / Refresh" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="label-div w30" align="left">
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="passed">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblPassed" runat="server" Font-Bold="True" Font-Size="8" Text="Passed"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="passed_as_external">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblPassedAsExternal" runat="server" Font-Bold="True" Font-Size="8"
                                    Text="Passed As External"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="exemption_granted">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblExemptionGranted" runat="server" Font-Bold="True" Font-Size="8"
                                    Text="Exemption Granted"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="already_enrolled">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblAlreadyEnrolled" runat="server" Font-Bold="True" Font-Size="8"
                                    Text="Already Enrolled"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="label-div w25" align="left">
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="available">
                                </div>
                            </div>
                            <div class="field-div1 w80" align="left">
                                <asp:Label ID="lblAvailable" runat="server" Font-Bold="True" Font-Size="8" Text="Current Options for Enrollment"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="available">
                                    <asp:CheckBox ID="chkRequestEnrolled" runat="server" Enabled="false" Checked="true" />
                                </div>
                            </div>
                            <div class="field-div1 w60" align="left">
                                <asp:Label ID="lblEnrollmentExists" runat="server" Font-Bold="True" Font-Size="8"
                                    Text="Enrollment Request Exists"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="not_available">
                                </div>
                            </div>
                            <div class="field-div1 w60" align="left">
                                <asp:Label ID="lblNotAvailable" runat="server" Font-Bold="True" Font-Size="8" Text="Not Available"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="alternate_location">
                                </div>
                            </div>
                            <div class="field-div1 w60" align="left">
                                <asp:Label ID="lblAlternateLocation" runat="server" Font-Bold="True" Font-Size="8"
                                    Text="Alternate Location Exists"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <%--<asp:HiddenField ID="hdnTimeTable" runat="server" Value="1" />--%>
            <input type="hidden" name="hdnTimeTable" id="hdnTimeTable" value="0" />
            <%--<asp:Panel ID="pnlTimeTable" runat="server">--%>
            <%-- onclick="CollapseExpand('divTimeTable','hdnTimeTable')"--%>
            <h2 class="expand" id="HeadTimeTableUpdate" onclick="CollapseExpand('divTimeTable','hdnTimeTable')">
                Timetable Update:</h2>
            <div id="divTimeTable" class="collapse">
                <br />
                <div class="row-div clearfix">
                    <asp:UpdatePanel ID="upTimeTable" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblTTMessage" Style="color: Red;" runat="server"></asp:Label>
                            <div class="label-div w40" align="left">
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div class="label-div w30" align="left">
                                            <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="8" Text="Curriculum: "></asp:Label>
                                        </div>
                                        <div class="field-div1 w50" align="left">
                                            <asp:DropDownList ID="ddlCurriculumList" runat="server" AutoPostBack="true" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="label-div w30" align="left">
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div class="label-div w30">
                                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="8" Text="Time Table:"></asp:Label>
                                        </div>
                                        <div class="field-div1 w50" align="left">
                                            <asp:DropDownList ID="ddlTimeTable" runat="server" AutoPostBack="false" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="label-div w25" align="left">
                        <div class="info-data">
                            <div class="row-div clearfix">
                                <div class="label-div w15">
                                    <div>
                                    </div>
                                </div>
                                <div class="field-div1 w80" align="left">
                                    <asp:Button ID="btnSubmitTimeTable" Text="Submit" runat="server" CssClass="submit-Btn" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <telerik:RadWindow ID="rwValidation" runat="server" Width="400px" Height="150px"
                    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="True" Behaviors="None" ForeColor="#BDA797"
                    Title="Firm Course Enrollment" Behavior="None">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center;">
                            <asp:Label ID="lblWarning" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div>
                            <br />
                        </div>
                        <div style="width: 100%; text-align: center;">
                            <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </div>
            <%--</asp:Panel>--%>
        </div>
        <br />
        <div>
            <input type="hidden" name="hdnFAEUpdate" id="hdnFAEUpdate" value="0" />
            <h2 class="expand" id="HeadFAEUpdate" onclick="CollapseExpand('divFAEUpdate','hdnFAEUpdate')">
                FAE Elective Update:</h2>
            <div id="divFAEUpdate" class="collapse">
                <br />
                <%-- <asp:UpdatePanel ID="upFAEUpdate" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <asp:Label ID="lblFUMessage" Style="color: Red;" runat="server"></asp:Label>
                <div class="row-div clearfix">
                    <div class="label-div w40" align="left">
                        <div class="info-data">
                            <div class="row-div clearfix">
                                <div class="label-div w30" align="left">
                                    <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="8" Text="FAE Courses: "></asp:Label>
                                </div>
                                <div class="field-div1 w50" align="left">
                                    <asp:DropDownList ID="ddlFAEElectives" runat="server" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="label-div w25" align="left">
                        <div class="info-data">
                            <div class="row-div clearfix">
                                <div class="label-div w15">
                                    <div>
                                    </div>
                                </div>
                                <div class="field-div1 w80" align="left">
                                    <asp:Button ID="btnApplyFAE" Text="Submit" runat="server" CssClass="submit-Btn" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <telerik:RadWindow ID="rwFAEMessage" runat="server" Width="400px" Height="150px"
                    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="True" Behaviors="None" ForeColor="#BDA797"
                    Title="Firm Course Enrollment" Behavior="None">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center;">
                            <asp:Label ID="lblFAEUpdatedMsg" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div>
                            <br />
                        </div>
                        <div style="width: 100%; text-align: center;">
                            <asp:Button ID="btnFAEOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
                <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
        <br />
        <div>
            <%--style="overflow-x: auto; width: 100%"--%>
            <telerik:RadPivotGrid runat="server" ID="gvFirmCourseEnrollment" Skin="Default" Width="100%"
                AllowPaging="True" AllowSorting="true" AllowFiltering="True" PageSize="10" ColumnHeaderZoneText="ColumnHeaderZone"
                ColumnGroupsDefaultExpanded="true" Culture="en-US" LocalizationPath="~/App_GlobalResources/"
                TotalsSettings-RowsSubTotalsPosition="None" TotalsSettings-ColumnsSubTotalsPosition="None"
                TotalsSettings-ColumnGrandTotalsPosition="None" TotalsSettings-GrandTotalsVisibility="None"
                Visible="false">
                <ClientSettings EnableFieldsDragDrop="true">
                    <Scrolling AllowVerticalScroll="false" SaveScrollPosition="true"></Scrolling>
                    <%--<ClientEvents  OnCommand="RaiseCommand" />--%>
                </ClientSettings>
                <Fields>
                    <telerik:PivotGridRowField DataField="StudentID" CellStyle-Width="2%" Caption="#">
                        <CellTemplate>
                            <%--<asp:CheckBox ID="chkRecord" AutoPostBack="true" OnCheckedChanged="chkRecord_OnCheckedChanged"
                                CommandArgument='<%#(Container.DataItem).Substring((Container.DataItem).IndexOf(";") + 1) %>'
                                runat="server" Checked='<%#(Container.DataItem).Substring(0, 1) %>' />--%>
                            <asp:CheckBox ID="chkRecord" AutoPostBack="true" OnCheckedChanged="chkRecord_OnCheckedChanged"
                                CommandArgument='<%#(Container.DataItem)%>' runat="server" Checked='<%#IsSelected(Container.DataItem)%>' />
                        </CellTemplate>
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="StudentID" CellStyle-Width="2%" Caption="#">
                        <CellTemplate>
                            <asp:LinkButton ID="lnkM" runat="server" Text="M" CommandName="CourseEnrollmentEditM"
                                Font-Underline="true" Visible='<%#IIf(IsRTOFirm()=true,false,true)%>' CommandArgument='<%#(Container.DataItem )%>'
                                ToolTip="Course Enrollment"></asp:LinkButton>
                        </CellTemplate>
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="StudentID" CellStyle-Width="2%" Caption="#">
                        <CellTemplate>
                            <%--<asp:LinkButton ID="lnkR" runat="server" Text="R" CommandName="CourseEnrollmentEditR"
                                Font-Underline="true" CommandArgument='<%#(Container.DataItem )%>' ToolTip="Course Enrollment"></asp:LinkButton>--%>
                            <%--<asp:LinkButton ID="lnkStudentNumber" runat="server" Text="R" CommandName="CourseEnrollmentEditM"
                                Font-Underline="true" CommandArgument='<%#(Container.DataItem )%>' ToolTip="Course Enrollment">
                            </asp:LinkButton>--%>
                            <%-- Visible='<%#IIf(IsRTOFirm()=true,false,true)%>'>--%>
                            <asp:LinkButton ID="lnkStudentNumber" runat="server" Text="R" CommandName="BreakDownEdit"
                                Font-Underline="true" CommandArgument='<%#(Container.DataItem )%>' ToolTip="Break Down"></asp:LinkButton>
                        </CellTemplate>
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="StudentNumber" Caption="#" CellStyle-Width="4%">
                        <CellTemplate>
                            <%-- <asp:LinkButton ID="lnkStudentNumber" runat="server" Text='<%#(Container.DataItem).Substring((Container.DataItem).IndexOf(";") + 1) %>'
                                CommandName="BreakDownEdit" Font-Underline="true" CommandArgument='<%#(Container.DataItem )%>'
                                ToolTip="Break Down" Visible='<%#IIf(IsRTOFirm()=true,false,true)%>'></asp:LinkButton>--%>
                            <%-- Text='<%#(Container.DataItem).Substring((Container.DataItem).IndexOf(";") + 1) %>'--%>
                            <asp:Label ID="lblStudentNumber" runat="server" Text='<%#(Container.DataItem )%>'></asp:Label>
                        </CellTemplate>
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="LastName" Caption="Last" CellStyle-Width="5%">
                        <CellTemplate>
                            <%-- <asp:LinkButton ID="lnkLastName" runat="server" Text='<%#(Container.DataItem).Substring((Container.DataItem).IndexOf(";") + 1) %>'
                                CommandName="CourseEnrollmentEdit" Font-Underline="true" CommandArgument='<%#(Container.DataItem )%>'
                                ToolTip="Course Enrollment" Visible='<%#IIf(IsRTOFirm()=true,false,true)%>'></asp:LinkButton>--%>
                            <%-- Text='<%#(Container.DataItem).Substring((Container.DataItem).IndexOf(";") + 1) %>'--%>
                            <asp:Label ID="lblLastName" runat="server" Text='<%#(Container.DataItem)%>'></asp:Label>
                        </CellTemplate>
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="FirstName" Caption="First" CellStyle-Width="5%">
                    </telerik:PivotGridRowField>
                    <%--<telerik:PivotGridRowField DataField="IntakeYear" Caption="Intake Year" CellStyle-Width="8%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="RouteOfEntry" Caption="Route" CellStyle-Width="6%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="Stage" Caption="Current Stage" UniqueName="CurrentStage"
                        CellStyle-Width="9%">
                    </telerik:PivotGridRowField>--%>
                    <telerik:PivotGridReportFilterField DataField="IntakeYear" Caption="Intake Year"
                        CellStyle-Width="8%">
                    </telerik:PivotGridReportFilterField>
                    <telerik:PivotGridReportFilterField DataField="RouteOfEntry" Caption="Route" CellStyle-Width="6%">
                    </telerik:PivotGridReportFilterField>
                    <telerik:PivotGridReportFilterField DataField="Stage" Caption="Current Stage" UniqueName="CurrentStage"
                        CellStyle-Width="9%">
                    </telerik:PivotGridReportFilterField>
                    <telerik:PivotGridRowField DataField="RegEndDate" Caption="Reg. End Date" CellStyle-Width="9%"
                        DataFormatString="{0:dd/MM/yyyy}">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="OutstadingSubjects" Caption="Outstading" UniqueName="OutstadingSubjects"
                        CellStyle-Width="8%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="FailedSubject" Caption="Failed Subject(s)"
                        CellStyle-Width="10%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="OfficeLocation" Caption="Location" CellStyle-Width="8%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridColumnField DataField="Curriculum" Caption="Curriculum" UniqueName="Curriculum">
                    </telerik:PivotGridColumnField>
                    <telerik:PivotGridColumnField DataField="Type" Caption="Type" UniqueName="Type">
                        <CellTemplate>
                            <asp:Label ID="lblCapPart" runat="server" Text='<%#(Container.DataItem).Substring((Container.DataItem).IndexOf(";") + 1) %>'></asp:Label>
                        </CellTemplate>
                    </telerik:PivotGridColumnField>
                    <telerik:PivotGridColumnField DataField="CourseName" Caption="Course" UniqueName="CourseName">
                    </telerik:PivotGridColumnField>
                    <%--<telerik:PivotGridColumnField Caption="CurriculumID" IsHidden="true">
                        <CellTemplate>
                            <asp:Label ID="lblCurruculumID" runat="server" Text='<%# Eval("CurriculumID") %>'></asp:Label>
                        </CellTemplate>
                    </telerik:PivotGridColumnField>--%>
                    <telerik:PivotGridAggregateField DataField="IsEnrolled">
                        <CellTemplate>
                            <div align="center" style="vertical-align: middle;">
                                <asp:CheckBox ID="chkIsEnrolled" runat="server" Enabled="false" Checked='<%# IIf((Container.DataItem)=44 OrElse (Container.DataItem)=88,true,false) %>'
                                    Visible='<%# IIf((Container.DataItem)=22 OrElse (Container.DataItem)=44 OrElse (Container.DataItem)=88 OrElse (Container.DataItem)=8,true,false) %>' />
                                <asp:Label ID="lblTimeTable" runat="server"></asp:Label>
                            </div>
                        </CellTemplate>
                    </telerik:PivotGridAggregateField>
                </Fields>
            </telerik:RadPivotGrid>
        </div>
        <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-div w20">
                    <br />
                </div>
            </div>
        </div>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-div w24" align="left">
                    <asp:Label ID="lblNumberOfStudents" runat="server"></asp:Label>
                </div>
                <div class="field-div w98" align="right">
                    <asp:Button ID="btnSummaryPage" Text="Enroll Selected  Students" runat="server" CssClass="submit-Btn" />
                    <asp:Button ID="btnBack" runat="server" CssClass="submit-Btn" Text="Back" Width="8%" />
                </div>
            </div>
        </div>
        <telerik:RadWindow ID="rwCourseEnrollment" runat="server" Width="970px" Height="610px"
            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="True" Behaviors="None" Title="Course Selection"
            Behavior="None">
            <ContentTemplate>
                <div>
                    <asp:UpdatePanel ID="upCourseEnrollment" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="lblPopupCourseEnrollmentMessage" runat="server"></asp:Label>
                            </div>
                            <div style="margin: 0 1% 0 1%; width: 100%;">
                                <div>
                                    <br />
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-div-left-align">
                                        Student Number: <b>
                                            <asp:Label ID="lblStudentNo" Width="50px" runat="server" Text=""></asp:Label></b>
                                    </div>
                                    <div class="label-div-left-align">
                                        First Name: <b>
                                            <asp:Label ID="lblFirstName" Width="110px" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="label-div-left-align w16">
                                        Last Name: <b>
                                            <asp:Label ID="lblLastName" Width="110px" runat="server" Text=""></asp:Label></b>
                                        </b>
                                    </div>
                                    <div class="label-div-left-align w16">
                                        Route Of Entry:<b>
                                            <asp:Label ID="lblRouteOfEntry" Width="110px" runat="server" Text=""></asp:Label></b>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <br />
                            </div>
                            <div style="overflow: scroll; height: 370px;" class="info-data">
                                <div style="font-weight: bold;">
                                    Time Table: <b>
                                        <asp:DropDownList ID="ddlTimeTableList" runat="server">
                                        </asp:DropDownList>
                                </div>
                                <div>
                                    <div class="row-div clearfix">
                                        <div class="label-div w50">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div-left-align w50">
                                                        <asp:Label ID="lblSummerCourse" runat="server" Text="SC : Summer Course"></asp:Label>
                                                    </div>
                                                    <div class="label-div-left-align w48">
                                                        <asp:Label ID="Label3" runat="server" Text="IA : Interim Assessment"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div-left-align w50">
                                                        <asp:Label ID="Label1" runat="server" Text="ME : Mock Exam"></asp:Label>
                                                    </div>
                                                    <div class="label-div-left-align w48">
                                                        <asp:Label ID="Label2" runat="server" Text="SE : Summer Exam"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div-left-align w50">
                                                        <asp:Label ID="Label4" runat="server" Text="RC : Revision Course"></asp:Label>
                                                    </div>
                                                    <div class="label-div-left-align w48">
                                                        <asp:Label ID="Label5" runat="server" Text="RRC : Repeat Revision Course"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div-left-align w50">
                                                        <asp:Label ID="Label6" runat="server" Text="RIA : Resit Interim Assessment"></asp:Label>
                                                    </div>
                                                    <div class="label-div-left-align w48">
                                                        <asp:Label ID="Label7" runat="server" Text="AE : Autumn Exam"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="label-div w20" align="left">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    <div class="label-div w15">
                                                        <div class="passed">
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w35">
                                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="8" Text="Passed"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w15">
                                                        <div class="passed_as_external">
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w35">
                                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="8" Text="Passed As External"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w15">
                                                        <div class="exemption_granted">
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w35">
                                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="8" Text="Exemption Granted"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w15">
                                                        <div class="already_enrolled">
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w35">
                                                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="8" Text="Already Enrolled"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="label-div w25" align="left">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    <div class="label-div w15">
                                                        <div class="available">
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w80" align="left">
                                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="8" Text="Current Options for Enrollment"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w15">
                                                        <div class="available">
                                                            <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked="true" />
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w60" align="left">
                                                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="8" Text="Enrollment Request Exists"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w15">
                                                        <div class="not_available">
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w60" align="left">
                                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="8" Text="Not Available"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w15">
                                                        <div class="alternate_location">
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w60" align="left">
                                                        <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="8" Text="Alternate Location Exists"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="info-data">
                                        <div>
                                            <%--  class="label-div w98"--%>
                                            <telerik:RadGrid ID="gvCurriculumCourse" runat="server" PageSize="10" AllowSorting="True"
                                                AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                                ShowGroupPanel="False" GridLines="Both" Skin="Default" AutoGenerateColumns="false">
                                                <MasterTableView Width="100%" DataKeyNames="CurriculumID,CutOffUnits,SubjectID,CourseUnits,AlternativeGroupID,IsCore,IsValidTimeSpan,IsFAEElective,IsCourseJurisdiction,DeSelect,checkFAE">
                                                    <NoRecordsTemplate>
                                                        No record(s)
                                                    </NoRecordsTemplate>
                                                    <ColumnGroups>
                                                        <telerik:GridColumnGroup HeaderText="Autumn" Name="AutumnSession" />
                                                        <telerik:GridColumnGroup HeaderText="Summer" Name="SummerSession" />
                                                    </ColumnGroups>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="IsFAEElective" UniqueName="IsFAEElective"
                                                            HeaderText="FAE Elective" SortExpression="IsFAEElective" HeaderStyle-Width="5%"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <div align="center">
                                                                    <asp:CheckBox ID="chkIsFAEElective" runat="server" CssClass="chkBox" Enabled='<%#IIf(Eval("IsFAEElective")=1,true,false) %>'
                                                                        AutoPostBack="true" OnCheckedChanged="chkIsFAEElective_CheckedChanged" />
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                                            AutoPostBackOnFilter="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject" SortExpression="Subject"
                                                            AutoPostBackOnFilter="true" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridBoundColumn DataField="SubjectID" HeaderText="SubjectID" SortExpression="SubjectID"
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                            FilterControlWidth="100%" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="IsFAEElective" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="IsCourseJurisdiction" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="IsValidTimeSpan" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="IsCore" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="AlternativeGroupID" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="AlternativeGroup" HeaderText="Alternate Timetable"
                                                            SortExpression="AlternativeGroup" FilterControlWidth="100%" HeaderStyle-Width="15%"
                                                            ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridTemplateColumn DataField="ClassRoom" HeaderText="SC" SortExpression="ClassRoom"
                                                            AutoPostBackOnFilter="true" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Middle" ColumnGroupName="SummerSession" UniqueName="ClassRoom">
                                                            <ItemTemplate>
                                                                <div align="center" class='<%#SetColorCode(Eval("ClassRoom"))%>'>
                                                                    <asp:CheckBox ID="chkClassRoom" runat="server" CssClass="chkBox" Text='<%#Eval("ClassRoom")%>'
                                                                        AutoPostBack="true" OnCheckedChanged="chkClassRoom_CheckedChanged" Enabled='<%#IsAllowToEnroll(Eval("ClassRoom")) %>'
                                                                        Checked='<%#IsEnrolled(Eval("ClassRoom"))%>' Visible='<%#IsVisible(Eval("ClassRoom"))%>' />
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="InterimAssessment" HeaderText="IA" SortExpression="InterimAssessment"
                                                            HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left" ColumnGroupName="SummerSession">
                                                            <ItemTemplate>
                                                                <div align="center" class='<%#SetColorCode(Eval("InterimAssessment"))%>'>
                                                                    <asp:CheckBox ID="chkInterimAssessment" runat="server" CssClass="chkBox" Text='<%#Eval("InterimAssessment")%>'
                                                                        AutoPostBack="true" Enabled="false" Checked='<%#IsEnrolled(Eval("InterimAssessment"))%>'
                                                                        OnCheckedChanged="chkInterimAssessment_CheckedChanged" Visible='<%#IsVisible(Eval("InterimAssessment"))%>' />
                                                                    <%--Enabled='<%#IsAllowToEnroll(Eval("InterimAssessment")) %>'--%>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="MockExam" HeaderText="ME" SortExpression="MockExam"
                                                            AutoPostBackOnFilter="true" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left"
                                                            ColumnGroupName="SummerSession">
                                                            <ItemTemplate>
                                                                <div align="center" class='<%#SetColorCode(Eval("MockExam"))%>'>
                                                                    <asp:CheckBox ID="chkMockExam" runat="server" CssClass="chkBox" Text='<%#Eval("MockExam")%>'
                                                                        AutoPostBack="true" Enabled="false" Checked='<%#IsEnrolled(Eval("MockExam"))%>'
                                                                        OnCheckedChanged="chkMockExam_CheckedChanged" Visible='<%#IsVisible(Eval("MockExam"))%>' />
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="SummerExam" HeaderText="SE" SortExpression="SummerExam"
                                                            AutoPostBackOnFilter="true" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left"
                                                            ColumnGroupName="SummerSession">
                                                            <ItemTemplate>
                                                                <div align="center" class='<%#SetColorCode(Eval("SummerExam"))%>'>
                                                                    <asp:CheckBox ID="chkSummerExam" runat="server" CssClass="chkBox" Text='<%#Eval("SummerExam")%>'
                                                                        AutoPostBack="true" Enabled="false" Checked='<%#IsEnrolled(Eval("SummerExam"))%>'
                                                                        OnCheckedChanged="chkSummerExam_CheckedChanged" Visible='<%#IsVisible(Eval("SummerExam"))%>' />
                                                                    <%--Enabled='<%#IsAllowToEnroll(Eval("SummerExam")) %>'--%>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Revision" HeaderText="RC" SortExpression="Name"
                                                            AutoPostBackOnFilter="true" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left"
                                                            ColumnGroupName="SummerSession">
                                                            <ItemTemplate>
                                                                <div align="center" class='<%#SetColorCode(Eval("Revision"))%>'>
                                                                    <asp:CheckBox ID="chkRevision" runat="server" CssClass="chkBox" Text='<%#Eval("Revision")%>'
                                                                        AutoPostBack="true" OnCheckedChanged="chkRevision_CheckedChanged" Enabled='<%#IsAllowToEnroll(Eval("Revision")) %>'
                                                                        Checked='<%#IsEnrolled(Eval("Revision"))%>' Visible='<%#IsVisible(Eval("Revision"))%>' />
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="RepeatRevision" HeaderText="RRC" SortExpression="RepeatRevision"
                                                            FilterControlWidth="100%" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left"
                                                            ColumnGroupName="AutumnSession">
                                                            <ItemTemplate>
                                                                <div align="center" class='<%#SetColorCode(Eval("RepeatRevision"))%>'>
                                                                    <asp:CheckBox ID="chkRepeatRevision" runat="server" CssClass="chkBox" Text='<%#Eval("RepeatRevision")%>'
                                                                        AutoPostBack="true" OnCheckedChanged="chkRepeatRevision_CheckedChanged" Enabled='<%#IsAllowToEnroll(Eval("RepeatRevision")) %>'
                                                                        Checked='<%#IsEnrolled(Eval("RepeatRevision"))%>' Visible='<%#IsVisible(Eval("RepeatRevision"))%>' />
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="ResitInterimAssessment" HeaderText="RIA" SortExpression="ResitInterimAssessment"
                                                            HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left" ColumnGroupName="AutumnSession">
                                                            <ItemTemplate>
                                                                <div align="center" class='<%#SetColorCode(Eval("ResitInterimAssessment"))%>'>
                                                                    <asp:CheckBox ID="chkResitInterimAssessment" runat="server" CssClass="chkBox" Text='<%#Eval("ResitInterimAssessment")%>'
                                                                        AutoPostBack="true" Enabled='<%#IsAllowToEnroll(Eval("ResitInterimAssessment")) %>'
                                                                        OnCheckedChanged="chkResitInterimAssessment_CheckedChanged" Checked='<%#IsEnrolled(Eval("ResitInterimAssessment"))%>'
                                                                        Visible='<%#IsVisible(Eval("ResitInterimAssessment"))%>' />
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="AutumnExam" HeaderText="AE" SortExpression="AutumnExam"
                                                            AutoPostBackOnFilter="true" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left"
                                                            ColumnGroupName="AutumnSession">
                                                            <ItemTemplate>
                                                                <div align="center" class='<%#SetColorCode(Eval("AutumnExam"))%>'>
                                                                    <asp:CheckBox ID="chkAutumnExam" runat="server" CssClass="chkBox" Text='<%#Eval("AutumnExam")%>'
                                                                        AutoPostBack="true" Enabled="false" Checked='<%#IsEnrolled(Eval("AutumnExam"))%>'
                                                                        OnCheckedChanged="chkAutumnExam_CheckedChanged" Visible='<%#IsVisible(Eval("AutumnExam"))%>' />
                                                                    <%-- Enabled='<%#IsAllowToEnroll(Eval("AutumnExam")) %>'--%>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                    <div>
                                        <telerik:RadWindow ID="radwindowIAOpt" runat="server" VisibleOnPageLoad="false" Height="170px"
                                            Title="Firm Course Enrollment" Width="350px" BackColor="#f4f3f1" VisibleStatusbar="false"
                                            Behaviors="None" Behavior="None" Modal="True" ForeColor="#BDA797">
                                            <ContentTemplate>
                                                <div class="info-data">
                                                    <div style="height: 70px;" class="row-div clearfix">
                                                        <b>
                                                            <asp:Label ID="lblSubmitMessage" runat="server" Text=""></asp:Label></b>
                                                        <br />
                                                    </div>
                                                    <div class="row-div clearfix" align="center">
                                                        <%--  Added BY Pradip For MidFeb-9--%>
                                                        <asp:Button ID="btnIAWarning" runat="server" Text="Ok" class="submitBtn" Width="20%"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </telerik:RadWindow>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="updateProcessingIndicator" AssociatedUpdatePanelID="upCourseEnrollment"
                                runat="server" DisplayAfter="0">
                                <ProgressTemplate>
                                    <div class="dvProcessing" style="height: 1760px;">
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div>
                    <div class="row-div clearfix">
                        <br />
                    </div>
                    <div class="row-div clearfix">
                        <div class="label-div w98" align="left">
                            <div>
                                <br />
                            </div>
                            <div>
                                <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="submit-Btn" />
                                <asp:Button ID="btnCloseCourseEnrollment" Text="Cancel" runat="server" CssClass="submit-Btn" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="rwStudentBreakDown" runat="server" Width="800px" Height="600px"
            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="True" Behaviors="None" ForeColor="#BDA797"
            Title="Student Break Down" Behavior="None">
            <ContentTemplate>
                <asp:UpdatePanel ID="upStudentBreakDown" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div>
                            <asp:Label ID="lblPopupStudentBreakDownMessage" runat="server"></asp:Label>
                        </div>
                        <div class="info-data">
                            <div class="row-div clearfix">
                                <div class="label-div w45">
                                    <div class="info-data">
                                        <div class="row-div clearfix">
                                        </div>
                                        <div class="row-div clearfix">
                                        </div>
                                        <div class="row-div clearfix">
                                        </div>
                                        <div class="row-div clearfix">
                                            <div class="label-div w25">
                                                <asp:Label ID="lblSBLastName" runat="server" Text="Last Name "></asp:Label>
                                            </div>
                                            <div class="field-div1 w35">
                                                <asp:TextBox ID="txtSBLastName" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="label-div w45">
                                    <div class="info-data">
                                        <div class="row-div clearfix">
                                        </div>
                                        <div class="row-div clearfix">
                                        </div>
                                        <div class="row-div clearfix">
                                        </div>
                                        <div class="row-div clearfix">
                                            <div class="label-div w25">
                                                <asp:Label ID="lblSBFirstName" runat="server" Text="First Name "></asp:Label>
                                            </div>
                                            <div class="field-div1 w35">
                                                <asp:TextBox ID="txtSBFirstName" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="info-data">
                            <div class="row-div clearfix">
                                <div class="label-div w60">
                                    <telerik:RadGrid ID="gvCompletedCurriculum" runat="server" PageSize="4" AllowSorting="True"
                                        AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                        ShowGroupPanel="False" GridLines="none" AutoGenerateColumns="false">
                                        <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                            <NoRecordsTemplate>
                                                No record(s)
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Curriculum" HeaderText="Completed Curriculum"
                                                    SortExpression="Curriculum" FilterControlWidth="80%" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" HeaderStyle-Width="50%"
                                                    ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="CurrentStage" HeaderText="Current Curriculum"
                                                    SortExpression="CurrentStage" FilterControlWidth="80%" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" HeaderStyle-Width="50%"
                                                    ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                            </div>
                            <div class="row-div clearfix">
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w98">
                                    <telerik:RadGrid ID="gvCompletedCourses" runat="server" PageSize="4" AllowSorting="True"
                                        AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                        ShowGroupPanel="False" GridLines="none" AutoGenerateColumns="false">
                                        <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                            <NoRecordsTemplate>
                                                No record(s)
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CourseName" HeaderText="Completed Courses" SortExpression="CourseName"
                                                    ShowFilterIcon="false" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                                    HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year"
                                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="ExamSat" HeaderText="Exam Sat" SortExpression="ExamSat"
                                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                            </div>
                            <div class="row-div clearfix">
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w98">
                                    <telerik:RadGrid ID="gvFailedCourses" runat="server" PageSize="4" AllowSorting="True"
                                        AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                        ShowGroupPanel="False" GridLines="none" AutoGenerateColumns="false">
                                        <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                            <NoRecordsTemplate>
                                                No record(s)
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CourseName" HeaderText="Failed Courses" SortExpression="CourseName"
                                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                                    HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year"
                                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="ExamSat" HeaderText="Exam Sat" SortExpression="ExamSat"
                                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                            </div>
                            <div class="row-div clearfix">
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w98">
                                    <telerik:RadGrid ID="gvCurrentCourses" runat="server" PageSize="4" AllowSorting="True"
                                        AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                        GridLines="none" AutoGenerateColumns="false">
                                        <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                            <NoRecordsTemplate>
                                                No record(s)
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CourseName" HeaderText="Current Courses" SortExpression="CourseName"
                                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                                    HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="ExamSat" HeaderText="Exam Sat" SortExpression="ExamSat"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridTemplateColumn DataField="IsRevision" HeaderText="Revision" SortExpression="IsRevision"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <div align="center">
                                                            <asp:CheckBox ID="chkCurrentExamsIsResit" runat="server" Enabled="false" Checked='<%# IIf(Eval("IsRevision")=1,true,false) %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="TotalCost" HeaderText="Total Cost" SortExpression="TotalCost"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                            </div>
                            <div class="row-div clearfix">
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w98">
                                    <telerik:RadGrid ID="gvCurrentExams" runat="server" PageSize="4" AllowSorting="True"
                                        AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                        GridLines="none" AutoGenerateColumns="false">
                                        <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                            <NoRecordsTemplate>
                                                No record(s)
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CourseName" HeaderText="Exams" SortExpression="CourseName"
                                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                                    HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="ExamSat" HeaderText="Exam Sat" SortExpression="ExamSat"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridTemplateColumn DataField="IsResit" HeaderText="Resit" SortExpression="IsResit"
                                                    HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <div align="center">
                                                            <asp:CheckBox ID="chkCurrentExamsIsResit" runat="server" Enabled="false" Checked='<%# IIf(Eval("IsResit")=1,true,false) %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="IsMock" HeaderText="Mock" SortExpression="IsMock"
                                                    HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <div align="center">
                                                            <asp:CheckBox ID="chkIsMock" runat="server" Enabled="false" Checked='<%# IIf(Eval("IsMock")=1,true,false) %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridNumericColumn DataField="TotalCost" HeaderText="Total Cost" SortExpression="TotalCost"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                            </div>
                            <div class="row-div clearfix">
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w98">
                                    <telerik:RadGrid ID="gvIAResits" runat="server" PageSize="4" AllowSorting="True"
                                        AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                        GridLines="none" AutoGenerateColumns="false">
                                        <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                            <NoRecordsTemplate>
                                                No record(s)
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CourseName" HeaderText="IA Resits" SortExpression="CourseName"
                                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                                    HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="ExamSat" HeaderText="Exam Sat" SortExpression="ExamSat"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridTemplateColumn DataField="IsResit" HeaderText="Resit" SortExpression="IsResit"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <div align="center">
                                                            <asp:CheckBox ID="chkIAIsResit" runat="server" Enabled="false" Checked='<%# IIf(Eval("IsResit")=1,true,false) %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridNumericColumn DataField="TotalCost" HeaderText="Total Cost" SortExpression="TotalCost"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="info-data">
                    <div class="row-div clearfix">
                    </div>
                    <div class="row-div clearfix">
                    </div>
                    <div class="row-div clearfix">
                        <div class="label-div w24" align="left">
                            <asp:Label ID="lblTotalCost" runat="server"></asp:Label>
                        </div>
                        <div class="field-div w98" align="right">
                            <asp:Button ID="btnCloseBreakDown" Text="Cancel" runat="server" CssClass="submit-Btn" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <cc1:User ID="loggedInUser" runat="server" />
        <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
        <%--<asp:HiddenField ID="hdn_EnrollOnLoad" runat="server" Value="" />--%>
    </telerik:RadAjaxPanel>
</div>
