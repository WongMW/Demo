<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FirmCourseEnrollment__c.ascx.vb"
    Inherits="FirmCourseEnrollment__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ajax" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<script type="text/javascript">
    //hdnFAEUpdate
    jQuery(function ($) {
        var PanelState1 = document.getElementById('hdnTimeTable').value;
        var PanelState2 = document.getElementById('hdnFAEUpdate').value;
        if (PanelState1 == '1') {
            $('#divTimeTable').removeClass("collapse").addClass("active");
        }
        if (PanelState2 == '1') {
            $('#divFAEUpdate').removeClass("collapse").addClass("active");
        }
        //var filterButton = document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_FirmCourseEnrollment__c_gvFirmCourseEnrollment_ctl02_RowZone10_rc_OfficeLocation_OfficeLocation_FilterPictButton");
        // if (filterButton != undefined)
        //{ filterButton.style.display = "none"; }
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

    function LocationFilterHide() {
        var filterButton = document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_FirmCourseEnrollment__c_gvFirmCourseEnrollment_ctl02_RowZone10_rc_OfficeLocation_OfficeLocation_FilterPictButton");
        if (filterButton != undefined) {
            // filterButton.style.display = "none";
            filterButton.className = "collapse";
            //filterButton.removeClass("rpgFilter").addClass("collapse");

        }
    }
    function HideValidationPopup() {
        var radwindow1 = $find('ctl00_ctl00_baseTemplatePlaceholder_content_FirmCourseEnrollment__c_rwValidation');
        radwindow1.hide();
        radwindow1.set_modal(false);
        var hid = document.getElementById('baseTemplatePlaceholder_content_FirmCourseEnrollment__c_hidValidation');
        hid.value = "Close";
    }

    $(document).ready(function () {
        //getGridCell();
    });
    // Code added by Paresh for Performance issue
    function getGridCell() {
        //Added below if condition for page loading issue if filter returns nothing
        var lblNoRecord = document.getElementById("<%=lblNoRecords.ClientID%>").innerText;
        if (lblNoRecord.length == 0) {
            var rows = document.getElementById("<%=gvFirmCourseEnrollment.ClientID%>").getElementsByClassName("rpgDataCell");
            for (var i = 0; i < rows.length; i++) {
                var hiddenVal = $(rows[i]).find("input:hidden").val();

                if (parseInt(hiddenVal) < -1) {
                    //var stageID = parseInt(hiddenVal) * -1;
                    //getStudentGroup(stageID);
                } else {

                    switch (hiddenVal) {
                        case "1":
                            $(rows[i]).css("background-color", "DarkGreen");
                            break;
                        case "-1":
                            $(rows[i]).css("background-color", "DarkSeaGreen");
                            break;
                        case "11":
                            $(rows[i]).css("background-color", "LightSalmon");
                            break;
                        case "2":
                            $(rows[i]).css("background-color", "Yellow");
                            break;
                        case "22":
                            $(rows[i]).css("background-color", "DeepSkyBlue");
                            break;
                        case "44":
                        case "88":
                            $(rows[i]).css("background-color", "DarkRed");
                            break;
                        case "4":
                            $(rows[i]).css("background-color", "Gray");
                            break;
                        case "8":
                            $(rows[i]).css("background-color", "MediumPurple");
                            break;
                    }
                }
            }
        }
    }

</script>
<style type="text/css">
    .ui-draggable .ui-dialog-titlebar {
        background-image: none;
        background-color: rgb(231, 210, 182);
        color: Black;
        border: 1px solid #F4F3F1;
    }

    .ui-widget-content {
        border: 1px solid #F4F3F1;
    }

        .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default {
            background-image: none;
            background-color: blue;
            color: white;
            border: none;
        }

            .ui-state-default:hover {
                background-image: none;
                background-color: blue;
                color: white;
                border: none;
                font-weight: bolder;
            }

    .ui-dialog-content ui-widget-content {
        border: 1px solid #F4F3F1;
    }

    .ui-icon:hover {
        background-color: Blue;
    }

    .ui-dialog-buttonset {
        margin-right: 50%;
    }
</style>
<style type="text/css">
    input[type="checkbox"] {
        margin-top: 5px;
        margin-right: 5px;
        vertical-align: middle;
    }

    .chkBox label {
        position: absolute;
        display: none;
    }

    .RadPivotGrid td {
        padding: 0;
    }

    div.qsf-right-content .qsf-col-wrap {
        position: static;
    }

    .RadPivotGrid_Metro .rpgContentZoneDiv td {
        white-space: wrap;
    }

    .popup_Buttons {
        margin: 10px;
    }

    .gv_already_enrolled {
        width: 40px;
        height: 25px;
        background-color: #FFFF66;
    }

    .gv_passed {
        width: 40px;
        height: 25px;
        background-color: #006400;
    }

    .gv_failed {
        width: 40px;
        height: 25px;
        background-color: #890F0F;
    }

    .gv_can_enroll {
        width: 40px;
        height: 25px;
        background-color: #00BFFF;
    }

    .gv_notavailable {
        width: 40px;
        height: 25px;
        background-color: Gray;
    }


    .GridDock {
        overflow-x: auto;
        overflow-y: hidden;
        width: 900px;
        padding: 0 0 17px 0;
        border-color: Black;
    }

    /*Added BY Pradip 2016-04-20 For BFP Scroll Bar Issue*/
    #page {
        width: 100% !important;
        padding: 0px;
        background: white; /*margin: 0px auto 0px auto;*/
    }

    #page-inner {
        width: 100% !important;
        margin: 0 auto;
        background-color: White; /*overflow: auto;*/
    }

    #home #content {
        background: -webkit-linear-gradient(top, #f0f0f0 100%,#ffffff 100%);
        width: 100% !important;
        padding: 10px;
        vertical-align: top;
        background-color: White;
        margin-top: 10px;
    }

    .RadMenu .rmRootGroup {
        width: 100% !important;
    }

    .rpgRowHeaderField.rpgRowHeader .rpgCollapse {
        display: none;
    }
</style>
<%--BFP Performance--%>
<script type="text/javascript" language="javascript">
    function callUpdatePriceService() {
        //Removed part of code which gets port 
        // below code commented by GM for Redmine #19732
        //var webmethod = "http" + "://" + document.location.host + "/webservices/CourseEnrolments__c.asmx/UpdatePriceInStaging";
        var webmethod = window.location.protocol + "//" + window.location.host + "/webservices/CourseEnrolments__c.asmx/UpdatePriceInStaging";
        // END  Redmine #19732
        var vParam = $('#hdnCompanyID').val();
        vParam = vParam.replace('&quot;', '&quot;&quot;');
        var vParam1 = $('#hdnCompanyPersonID').val();
        vParam1 = vParam1.replace('&quot;', '&quot;&quot;');
        var parmeter = JSON.stringify({ 'Status': 'Pending', 'CompanyID': vParam, 'BillToID': vParam1 });
        $.ajax({
            type: "POST",
            url: webmethod,
            data: parmeter,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //Added timeout and complete options for stalled service issue
            timeout: 300000,
            error: function (jqXhr, textStatus, errorMessage) {
                alert(errorMessage);
            },
            complete: function (jqXhr, textStatus) {
                if (textStatus == 'timeout') {
                    $('#hdnTimeout').val() = "1";
                }
            },
            success: function (response) {
                var sResponse = JSON.parse(response.d);
                if (sResponse.length > 0) {
                    $('#hdnPriceUpdateStatus').val(sResponse);

                }
            },
            failure: function (msg) {
                $('#hdnPriceUpdateStatus').val(msg);
            }
        });
        return true;
    }
</script>

<%--Added Div By Pradip 2016-05-20--%>
<%--<div class="maindiv">--%>
    &nbsp;
<div class="cai-form">
    <asp:HiddenField ID="hfStudentID" runat="server" Value="0" />
    <asp:HiddenField ID="hfAcademicCycleID" runat="server" Value="0" />
    <asp:HiddenField ID="hidValidation" runat="server" />
    <%--BFP Performance--%>
    <asp:HiddenField ID="hdnCompanyID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnCompanyPersonID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnPriceUpdateStatus" runat="server" ClientIDMode="Static" />
	<%--Added new hidden field for stalled serice issue--%>
	<asp:HiddenField ID="hdnTimeout" runat="server" ClientIDMode="Static" />
    
    <%--Commented RadAjaxPanel and added Update Panel to resolve Postback issue--%>
    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>Loading</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
        <asp:Label ID="lblMessage" Style="color: Red;" runat="server"></asp:Label>

        <div class="data-form">
            <div>
                <%-- Susan 13-Oct-2017 changes for 18470 --%>
                <div class="form-title">Step 1: filter to select students for course enrolment</div>
                <div class="form-section-half-border">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblAcademicYear" runat="server" Text="Academic year: " CssClass="label-title"></asp:Label></td>

                            <td>
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" Width="200px">
                                </asp:DropDownList>
                                <input type="hidden" name="hdnFAEUpdate" id="hdnFAEUpdate" value="0" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label17" runat="server" Text="Enrolment type: " CssClass="label-title"></asp:Label></td>

                            <td>
                                <asp:DropDownList ID="ddlEnrollmentType" runat="server" Width="200px">
                                    <asp:ListItem Text="All" Value="All">
                                
                                    </asp:ListItem>
                                    <asp:ListItem Text="New" Value="New">
                                
                                    </asp:ListItem>
                                    <asp:ListItem Text="Existing" Value="Existing">
                                
                                    </asp:ListItem>
                                </asp:DropDownList></td>
                            <tr>
                                <td>
                                    <asp:Label ID="Label19" runat="server" Text="Route of entry: " CssClass="label-title"></asp:Label></td>

                                <td>
                                    <asp:TextBox ID="txtcontract" runat="server" Enabled="false" Width="200px" Text="Contract"></asp:TextBox>
                                    <asp:DropDownList ID="ddlRouteOfEntry" runat="server" Width="200px" Visible="false">
                                    </asp:DropDownList></td>
                            </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label20" runat="server" Text="Current stage: " CssClass="label-title"></asp:Label></td>

                            <td>
                                <asp:DropDownList ID="ddlCurrentStage" AutoPostBack="true" runat="server" Width="200px">
                                </asp:DropDownList></td>
                        </tr>
			<tr>
                            <td>
                                <asp:Label ID="Label21" runat="server" Text="Result status: " CssClass="label-title"></asp:Label></td>

                            <td>
                                <asp:DropDownList ID="ddlSubjectsFailed" runat="server" Width="200px">
                                    <asp:ListItem Text="Not awaiting results" Value="No">
                                
                                    </asp:ListItem>
                                    <asp:ListItem Text="Awaiting results" Value="Yes">
                                
                                    </asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </div>
                <div class="form-section-half-border">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblCodes" runat="server" Text="Filter by: " CssClass="label-title"></asp:Label></td>

                            <td>
                                <asp:DropDownList ID="ddlCodesList" runat="server" Width="200px">
                                </asp:DropDownList></td>
                        </tr>
                       <%--<tr>
                            <td>
                                <asp:Label ID="Label21" runat="server" Text="Subjects Failed: " CssClass="label-title"></asp:Label></td>

                            <td>
                                <asp:DropDownList ID="ddlSubjectsFailed" runat="server" Width="200px">
                                    <asp:ListItem Text="No" Value="No">
                                
                                    </asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="Yes">
                                
                                    </asp:ListItem>
                                    <asp:ListItem Text="Ignore" Value="Ignore">
                                
                                    </asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Location: " CssClass="label-title"></asp:Label></td>

                            <td>
                                <asp:DropDownList ID="ddlLocation" runat="server" Width="200px">
                                </asp:DropDownList></td>
                            <tr>
                                <td></td>
                                <td>
                                    <div class="actions">
                                        <asp:Button ID="btnDisplay" runat="server" CssClass="submitBtn" Text="Display / Refresh" />
                                    </div>
                                </td>
                            </tr>
                    </table>
                </div>
            </div>
            <%-- Susan 13-Oct-2017 changes for 18470 --%>
            <div class="form-title" style="background:none;color:#000">Legend</div>
            <div class="cai-form-content">
                <div class="form-section-half-border">
                    <div class="color-code">
                        <div class="passed"></div>
                        <asp:Label ID="lblPassed" runat="server" Text="Passed"></asp:Label>
                    </div>

                    <div class="color-code">
                        <div class="passed_as_external"></div>
                        <asp:Label ID="lblPassedAsExternal" runat="server"
                            Text="Passed as external"></asp:Label>
                    </div>

                    <div class="color-code">
                        <div class="exemption_granted"></div>
                        <asp:Label ID="lblExemptionGranted" runat="server"
                            Text="Exemption granted"></asp:Label>
                    </div>

                    <div class="color-code">
                        <div class="already_enrolled"></div>
                        <asp:Label ID="lblAlreadyEnrolled" runat="server"
                            Text="Already enrolled"></asp:Label>
                    </div>
                </div>
                <div class="form-section-half-border">
                    <div class="color-code">
                        <div class="available"></div>
                        <asp:Label ID="lblAvailable" runat="server" Text="Current options for enrolment"></asp:Label>
                    </div>

                    <div class="color-code">
                        <%--BFP Performace- Commentted below as we will not show check box--%>
                        <div class="request_exists">
                            <%--<asp:CheckBox ID="chkRequestEnrolled" runat="server" Enabled="false" Checked="true" />--%>
                        </div>

                        <asp:Label ID="lblEnrollmentExists" runat="server"
                            Text="Enrolment request exists"></asp:Label>
                    </div>

                    <div class="color-code">
                        <div class="not_available"></div>
                        <asp:Label ID="lblNotAvailable" runat="server" Text="Not available"></asp:Label>
                    </div>

                    <div class="color-code">
                        <div class="alternate_location"></div>
                        <asp:Label ID="lblAlternateLocation" runat="server"
                            Text="Alternate location exists"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <div class="cai-form">
            <input type="hidden" name="hdnTimeTable" id="hdnTimeTable" value="0" />
            <%-- Susan 13-Oct-2017 changes for 18470 --%>
            <%--<h2 class="expand form-title" id="HeadTimeTableUpdate" onclick="CollapseExpand('divTimeTable','hdnTimeTable')">Step 2: Assign your students to a timetable</h2>--%>
            <div class="form-title" id="HeadTimeTableUpdate">Step 2: assign your students to a timetable</div>
            <div id="divTimeTable" class="collapse cai-form-content" style="display:block;">
                <asp:UpdatePanel ID="upTimeTable" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblTTMessage" Style="color: Red;" runat="server"></asp:Label>
                        <asp:Label ID="Label16" runat="server" CssClass="label-title" Text="Curriculum: "></asp:Label>

                        <asp:DropDownList ID="ddlCurriculumList" Enabled="false" runat="server" AutoPostBack="true" Width="100%">
                        </asp:DropDownList>

                        <asp:Label ID="Label18" runat="server" CssClass="label-title" Text="Timetable:"></asp:Label>

                        <asp:DropDownList ID="ddlTimeTable" runat="server" Width="100%">
                        </asp:DropDownList>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="actions">
                    <asp:Button ID="btnSubmitTimeTable" Text="Submit" runat="server" CssClass="submitBtn" />

                </div>
                <telerik:RadWindow ID="rwValidation" VisibleOnPageLoad="false" ReloadOnShow="false" runat="server" Width="400px" Height="200px"
                    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="True" Behaviors="None" ForeColor="#BDA797"
                    Title="Firm course enrolment" Behavior="None">
                    <ContentTemplate>
                        &nbsp;
                        <div style="width: 100%; text-align: center;">
                            <asp:Label ID="lblWarning" runat="server"></asp:Label>
                        </div>
                        &nbsp;
                        <div style="width: 100%; text-align: center;">
                            <%--<asp:Button ID="btnOk" runat="server" Text="Ok" class="submitBtn" />--%>
                            <input type="button" value="Ok" onclick="HideValidationPopup();" class="submitBtn">
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </div>

        </div>

        <div runat="server" id="divFAEUpdateMain" visible="false" class="cai-form">

            <div class="form-title" id="HeadFAEUpdate">Step 2a: FAE elective update</div>
            <div id="divFAEUpdate">

                <asp:Label ID="lblFUMessage" Style="color: Red;" runat="server"></asp:Label>

                <asp:Label ID="Label22" Visible="false" runat="server" CssClass="label-title" Text="FAE courses: "></asp:Label>
                <div class="cai-form-content sfContentBlock">
                <asp:LinkButton ID="lnkFAEEnrollment" runat="server" CssClass="cai-btn cai-btn-red-inverse">Choose elective</asp:LinkButton></div>
                <asp:DropDownList ID="ddlFAEElectives" Visible="false" runat="server" Width="100%">
                </asp:DropDownList>
                <div class="actions">
                    <asp:Button ID="btnApplyFAE" Text="Submit" Visible="false" runat="server" CssClass="submitBtn" />
                </div>
                <telerik:RadWindow ID="rwFAEMessage" VisibleOnPageLoad="false" ReloadOnShow="false" runat="server" Width="400px" Height="150px"
                    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="True" Behaviors="None" ForeColor="#BDA797"
                    Title="Firm course enrollment" Behavior="None">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: center;">
                            <asp:Label ID="lblFAEUpdatedMsg" runat="server"></asp:Label>
                        </div>

                        <div style="width: 100%; text-align: center;">
                            <asp:Button ID="btnFAEOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>

            </div>
        </div>

        <div>
            <%-- Susan 13-Oct-2017 changes for 18470 --%>
            <div class="form-title">Step 3: deselect students you do not wish to enrol</div>
 <telerik:RadPivotGrid runat="server" ID="gvFirmCourseEnrollment" Skin="Default" Width="100%"
                AllowPaging="false" AllowSorting="true" AllowFiltering="false" ColumnHeaderZoneText="ColumnHeaderZone"
                ColumnGroupsDefaultExpanded="true" Culture="en-US" LocalizationPath="~/App_GlobalResources/"
                TotalsSettings-RowsSubTotalsPosition="None" TotalsSettings-ColumnsSubTotalsPosition="None"
                TotalsSettings-ColumnGrandTotalsPosition="None" ClientSettings-EnableFieldsDragDrop="false" TotalsSettings-GrandTotalsVisibility="None"
                Visible="false">
                <%--  PageSize="10" height="400px"--%>
                <%-- <PagerStyle CssClass="sd-pager"  />--%>

                <ClientSettings EnableFieldsDragDrop="false">
                    <Scrolling SaveScrollPosition="true"></Scrolling>
                    <%--<ClientEvents  OnCommand="RaiseCommand" />--%>
                </ClientSettings>
                <Fields>
                    <telerik:PivotGridRowField DataField="LastNameNew" Caption="Last" CellStyle-Width="10%">
                        <CellTemplate>
                            <asp:Label ID="lblLastName" runat="server" Text='<%#(Container.DataItem).Substring(0, (Container.DataItem).IndexOf(";") )%>'></asp:Label>
                        </CellTemplate>
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="FirstName" Caption="First" CellStyle-Width="10%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="StudentNumber" Caption="#" CellStyle-Width="4%">
                        <CellTemplate>
                            <asp:Label ID="lblStudentNumber" runat="server" Text='<%#(Container.DataItem )%>'></asp:Label>
                        </CellTemplate>
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="StudentIDTest" CellStyle-Width="2%" Caption="#">
                        <CellTemplate>
                            <asp:CheckBox ID="chkRecord" AutoPostBack="true" OnCheckedChanged="chkRecord_OnCheckedChanged"
                                CommandArgument='<%#(Container.DataItem)%>' runat="server" Checked='<%# IIf((Container.DataItem).Substring((Container.DataItem).lastIndexOf(";") + 1,1) = "Y", True , False)%>' />
                        </CellTemplate>
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="StudentIDTest" CellStyle-Width="2%" Caption="#">
                        <CellTemplate>
                            <!-- M / Modify Link -->
                            <asp:LinkButton ID="lnkM" runat="server" Text="" CommandName="CourseEnrollmentEditM"
                                Font-Underline="true" Visible='<%# IIf((Container.DataItem).Substring((Container.DataItem).IndexOf(";") + 1,1) = "Y", False, True)%>' CommandArgument='<%#(Container.DataItem )%>'
                                ToolTip="Edit course enrollment"><i class="fa fa-pencil" aria-hidden="true" style="line-height: 1.4em;font-size: 20px;"></i></asp:LinkButton>
                        </CellTemplate>
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="StudentID" CellStyle-Width="2%" Caption="#">
                        <CellTemplate>
                            <!-- R / Report Link -->
                            <asp:LinkButton ID="lnkStudentNumber" runat="server" Text="" CommandName="BreakDownEdit"
                                Font-Underline="true" CommandArgument='<%#(Container.DataItem )%>' ToolTip="Breakdown"><i class="fa fa-file-text-o" aria-hidden="true" style="line-height: 1.4em;font-size: 20px;"></i></asp:LinkButton>
                        </CellTemplate>
                    </telerik:PivotGridRowField>




                    <telerik:PivotGridReportFilterField DataField="IntakeYear" Caption="Intake year"
                        CellStyle-Width="8%">
                    </telerik:PivotGridReportFilterField>
                    <telerik:PivotGridReportFilterField DataField="RouteOfEntry" Caption="Route" CellStyle-Width="6%">
                    </telerik:PivotGridReportFilterField>
                    <telerik:PivotGridReportFilterField DataField="Stage" Caption="Current stage" UniqueName="CurrentStage"
                        CellStyle-Width="9%">
                    </telerik:PivotGridReportFilterField>
                    <telerik:PivotGridRowField DataField="RegEndDate" Caption="Reg. end date" CellStyle-Width="10%"
                        DataFormatString="{0:dd/MM/yyyy}">
                    </telerik:PivotGridRowField>
                    <%-- <telerik:PivotGridRowField DataField="OutstadingSubjects" Caption="Outstanding" UniqueName="OutstadingSubjects"
                        CellStyle-Width="10%">
                    </telerik:PivotGridRowField>--%>
                    <%--<telerik:PivotGridRowField DataField="FailedSubject" Caption="Failed Subject(s)"
                        CellStyle-Width="11%">
                    </telerik:PivotGridRowField>--%>
                    <%--BFP Performace- Commentted below as now we are displaying results for single location--%>
                    <%--<telerik:PivotGridRowField DataField="OfficeLocation" Caption="Location" CellStyle-Width="8%">
                    </telerik:PivotGridRowField>--%>
                    <%--BFP Performace- Added student group column to left panel of grid--%>
                    <telerik:PivotGridRowField DataField="StudentGroupName" Caption="Timetable" CellStyle-Width="10%">
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
                    <telerik:PivotGridAggregateField DataField="IsEnrolled">
                        <CellTemplate>
                            <div style="vertical-align: middle;">
                                <%--BFP Performace- Commentted below as we will not show check box--%>
                                <%--<asp:CheckBox ID="chkIsEnrolled" runat="server" Enabled="false" Checked='<%# IIf((Container.DataItem)=44 OrElse (Container.DataItem)=88,true,false) %>'
                                    Visible='<%# IIf((Container.DataItem)=22 OrElse (Container.DataItem)=44 OrElse (Container.DataItem)=88 OrElse (Container.DataItem)=8,true,false) %>' />--%>
                                <%-- Added By Paresh For Performance --%>
                                <asp:HiddenField ID="hdnEnrollCode" runat="server" Value ="<%# Container.DataItem%>" />
                                <%--BFP Performance: Commented by Paresh, as we have shifted time tables to left panel--%>
                                <%--<asp:Label ID="lblTimeTable" runat="server" CssClass="lblTimeTable"></asp:Label>--%>
                            </div>
                        </CellTemplate>
                    </telerik:PivotGridAggregateField>
                </Fields>
            </telerik:RadPivotGrid>
           
        </div>

        <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
        <asp:Label ID="lblNumberOfStudents" runat="server"></asp:Label>

        <div class="actions">
			<%--BFP Performance--%>
            <asp:Label runat="server" CssClass="label-title">Clicking on enrol means you accept the <a href="/Current-Student/Student-Centre/Enrolment-Requirements" target="_blank">terms and conditions</a> of enrolment.</asp:Label>
            <asp:Button ID="btnSummaryPage" Text="Enrol Selected Students" runat="server" CssClass="submitBtn" OnClientClick="callUpdatePriceService()"/>
            <asp:Button ID="btnBack" runat="server" CssClass="submitBtn" Text="Back" Width="8%" />
        </div>

        <%--Added Div By Pradip 2016-05-20--%>
        <div>
            <telerik:RadWindow ID="rwCourseEnrollment" ReloadOnShow="false" VisibleOnPageLoad="false" runat="server" Width="970px" Height="650px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="false" Behaviors="None" Title="Firm course enrolment"
                Behavior="None">
                <ContentTemplate>
                    <div>
                        <asp:UpdatePanel ID="upCourseEnrollment" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div>
                                    <asp:Label ID="lblPopupCourseEnrollmentMessage" runat="server"></asp:Label>
                                </div>
                                <div>
                                    <table>
                                        <tr>
                                            <td>Student number: <b><asp:Label ID="lblStudentNo" Width="" runat="server" Text="" Style="text-align: left;"></asp:Label></b></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>First name: <b><asp:Label ID="lblFirstName" Width="" runat="server" Text="" Style="text-align: left;"></asp:Label> <asp:Label ID="lblLastName" Width="" runat="server" Text="" Style="text-align: left;"></asp:Label></b></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Route of entry: <b><asp:Label ID="lblRouteOfEntry" Width="110px" runat="server" Text="" Style="text-align: left;"></asp:Label></b></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>

                                <div style="overflow: scroll; height: 450px;">
                                    <table style="margin-top: 10px;">
						<tr>
                                        		<td style="width: 50%">Timetable:</td>
                                            		<td style="width: 50%"><asp:DropDownList ID="ddlTimeTableList" Visible="false" runat="server"></asp:DropDownList>
                                           		<b><asp:Label ID="lblTimeTablePopup" runat="server" Text=""></asp:Label></b></td>
						</tr>
                                    </table>
                                    <div>
                                        <div>
                                            <table style="margin-top: 10px;">
                                                <tr>
                                                        <td style="width: 50%"><b><asp:Label ID="lblSummerCourse" runat="server" Text="SC : summer course"></asp:Label></b></td>
                                                        <td style="width: 50%"><b><asp:Label ID="Label4" runat="server" Text="RC : Revision Course"></asp:Label></b></td>
						</tr>
                                                <tr>
                                                        <td style="width: 50%"><b><asp:Label ID="Label3" runat="server" Text="IA : interim assessment"></asp:Label></b></td>
                                                        <td style="width: 50%"><b><asp:Label ID="Label5" runat="server" Text="RRC : repeat revision course"></asp:Label></b></td>
						</tr>
                                                <tr>
                                                        <td style="width: 50%"><b><asp:Label ID="Label1" runat="server" Text="ME : mock exam"></asp:Label></b></td>
                                                        <td style="width: 50%"><b><asp:Label ID="Label6" runat="server" Text="RIA : resit interim assessment"></asp:Label></b></td>
						</tr>
                                                <tr>
                                                        <td style="width: 50%"><b><asp:Label ID="Label2" runat="server" Text="SE : summer exam"></asp:Label></b></td>
                                                        <td style="width: 50%"><b><asp:Label ID="Label7" runat="server" Text="AE : autumn exam"></asp:Label></b></td>
						</tr>
					</table>
					<table style="margin-top: 10px;">
                                                <tr>
                                                        <td style="width: 33%"><div class="passed"></div><asp:Label ID="Label8" runat="server" Text="Passed"></asp:Label></td>
                                                        <td style="width: 34%"><div class="available"></div><asp:Label ID="Label12" runat="server" Text="Current options for enrolment"></asp:Label></td>
                                                        <td style="width: 33%"><div class="passed_as_external"></div><asp:Label ID="Label9" runat="server" Text="Passed as external"></asp:Label></td>
						</tr>
                                                <tr>
                                                        <td style="width: 33%">
								<div class="available">
                                                        		<asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked="true" />
								</div><asp:Label ID="Label13" runat="server" Text="Enrolment request exists"></asp:Label></td>
                                                        <td style="width: 34%"><div class="exemption_granted"></div><asp:Label ID="Label10" runat="server" Text="Exemption granted"></asp:Label></td>
                                                        <td style="width: 33%"><div class="not_available"></div><asp:Label ID="Label14" runat="server" Text="Not available"></asp:Label></td>
						</tr>
                                                <tr>
                                                        <td style="width: 33%"><div class="already_enrolled"></div><asp:Label ID="Label11" runat="server" Text="Already enrolled"></asp:Label></td>
                                                        <td style="width: 34%"><div class="alternate_location"></div><asp:Label ID="Label15" runat="server" Text="Alternate location exists"></asp:Label></td>
                                                        <td style="width: 33%"></td>
						</tr>

                                            </table>
                                        </div>
                                        <div>
                                            <div>

                                                <telerik:RadGrid ID="gvCurriculumCourse" runat="server" PageSize="10" AllowSorting="True"
                                                    AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                                    ShowGroupPanel="False" GridLines="Both" Skin="Default" AutoGenerateColumns="false">
                                                    <%--Added missing data keys to resolve pop-up submit click issue--%>
                                                    <PagerStyle CssClass="sd-pager" />
                                                    <MasterTableView Width="100%" DataKeyNames="CurriculumID,CutOffUnits,SubjectID,CourseUnits,AlternativeGroupID,IsCore,IsValidTimeSpan,IsFAEElective,IsCourseJurisdiction,DeSelect,checkFAE,Isfailed,FailedUnits,FirstAttempt,caMinimumUnits">
                                                        <NoRecordsTemplate>
                                                            No record(s)
                                                        </NoRecordsTemplate>
                                                        <ColumnGroups>
                                                            <telerik:GridColumnGroup HeaderText="Autumn" Name="AutumnSession" />
                                                            <telerik:GridColumnGroup HeaderText="Summer" Name="SummerSession" />
                                                        </ColumnGroups>
                                                        <Columns>
                                                            <telerik:GridTemplateColumn DataField="IsFAEElective" UniqueName="IsFAEElective"
                                                                HeaderText="FAE elective" SortExpression="IsFAEElective">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:CheckBox ID="chkIsFAEElective" runat="server" CssClass="chkBox" Enabled='<%#IIf(Eval("IsFAEElective")=1,true,false) %>'
                                                                            AutoPostBack="true" OnCheckedChanged="chkIsFAEElective_CheckedChanged" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                                                AutoPostBackOnFilter="true" />
                                                            <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject" SortExpression="Subject"
                                                                AutoPostBackOnFilter="true" />
                                                            <telerik:GridBoundColumn DataField="SubjectID" HeaderText="SubjectID" SortExpression="SubjectID"
                                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                FilterControlWidth="100%" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="IsFAEElective" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="IsCourseJurisdiction" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="IsValidTimeSpan" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="IsCore" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="AlternativeGroupID" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="AlternativeGroup" HeaderText="Alternate timetable"
                                                                SortExpression="AlternativeGroup" FilterControlWidth="100%" />
                                                            <telerik:GridTemplateColumn DataField="ClassRoom" HeaderText="SC" SortExpression="ClassRoom"
                                                                AutoPostBackOnFilter="true" HeaderStyle-Width="7%"
                                                                ItemStyle-VerticalAlign="Middle" ColumnGroupName="SummerSession" UniqueName="ClassRoom">
                                                                <ItemTemplate>
                                                                    <div class='<%#SetColorCode(Eval("ClassRoom"))%>'>
                                                                        <asp:CheckBox ID="chkClassRoom" runat="server" CssClass="chkBox" Text='<%#Eval("ClassRoom")%>'
                                                                            AutoPostBack="true" OnCheckedChanged="chkClassRoom_CheckedChanged" Enabled='<%#IsAllowToEnroll(Eval("ClassRoom")) %>'
                                                                            Checked='<%#IsEnrolled(Eval("ClassRoom"))%>' Visible='<%#IsVisible(Eval("ClassRoom"))%>' />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="InterimAssessment" HeaderText="IA" SortExpression="InterimAssessment"
                                                                HeaderStyle-Width="7%" ColumnGroupName="SummerSession">
                                                                <ItemTemplate>
                                                                    <div class='<%#SetColorCode(Eval("InterimAssessment"))%>'>
                                                                        <asp:CheckBox ID="chkInterimAssessment" runat="server" CssClass="chkBox" Text='<%#Eval("InterimAssessment")%>'
                                                                            AutoPostBack="true" Enabled="false" Checked='<%#IsEnrolled(Eval("InterimAssessment"))%>'
                                                                            OnCheckedChanged="chkInterimAssessment_CheckedChanged" Visible='<%#IsVisible(Eval("InterimAssessment"))%>' />

                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="MockExam" HeaderText="ME" SortExpression="MockExam"
                                                                AutoPostBackOnFilter="true" HeaderStyle-Width="7%"
                                                                ColumnGroupName="SummerSession">
                                                                <ItemTemplate>
                                                                    <div class='<%#SetColorCode(Eval("MockExam"))%>'>
                                                                        <asp:CheckBox ID="chkMockExam" runat="server" CssClass="chkBox" Text='<%#Eval("MockExam")%>'
                                                                            AutoPostBack="true" Enabled="false" Checked='<%#IsEnrolled(Eval("MockExam"))%>'
                                                                            Visible='<%#IsVisible(Eval("MockExam"))%>' />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="SummerExam" HeaderText="SE" SortExpression="SummerExam"
                                                                AutoPostBackOnFilter="true" HeaderStyle-Width="7%"
                                                                ColumnGroupName="SummerSession">
                                                                <ItemTemplate>
                                                                    <div class='<%#SetColorCode(Eval("SummerExam"))%>'>
                                                                        <asp:CheckBox ID="chkSummerExam" runat="server" CssClass="chkBox" Text='<%#Eval("SummerExam")%>'
                                                                            AutoPostBack="true" Enabled="false" Checked='<%#IsEnrolled(Eval("SummerExam"))%>'
                                                                            OnCheckedChanged="chkSummerExam_CheckedChanged" Visible='<%#IsVisible(Eval("SummerExam"))%>' />

                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Revision" HeaderText="RC" SortExpression="Name"
                                                                AutoPostBackOnFilter="true" HeaderStyle-Width="7%"
                                                                ColumnGroupName="SummerSession">
                                                                <ItemTemplate>
                                                                    <div class='<%#SetColorCode(Eval("Revision"))%>'>
                                                                        <asp:CheckBox ID="chkRevision" runat="server" CssClass="chkBox" Text='<%#Eval("Revision")%>'
                                                                            AutoPostBack="true" OnCheckedChanged="chkRevision_CheckedChanged" Enabled='<%#IsAllowToEnroll(Eval("Revision")) %>'
                                                                            Checked='<%#IsEnrolled(Eval("Revision"))%>' Visible='<%#IsVisible(Eval("Revision"))%>' />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="RepeatRevision" HeaderText="RRC" SortExpression="RepeatRevision"
                                                                FilterControlWidth="100%" HeaderStyle-Width="7%"
                                                                ColumnGroupName="AutumnSession">
                                                                <ItemTemplate>
                                                                    <div class='<%#SetColorCode(Eval("RepeatRevision"))%>'>
                                                                        <asp:CheckBox ID="chkRepeatRevision" runat="server" CssClass="chkBox" Text='<%#Eval("RepeatRevision")%>'
                                                                            AutoPostBack="true" OnCheckedChanged="chkRepeatRevision_CheckedChanged" Enabled='<%#IsAllowToEnroll(Eval("RepeatRevision")) %>'
                                                                            Checked='<%#IsEnrolled(Eval("RepeatRevision"))%>' Visible='<%#IsVisible(Eval("RepeatRevision"))%>' />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="ResitInterimAssessment" HeaderText="RIA" SortExpression="ResitInterimAssessment"
                                                                HeaderStyle-Width="7%" ColumnGroupName="AutumnSession">
                                                                <ItemTemplate>
                                                                    <div class='<%#SetColorCode(Eval("ResitInterimAssessment"))%>'>
                                                                        <asp:CheckBox ID="chkResitInterimAssessment" runat="server" CssClass="chkBox" Text='<%#Eval("ResitInterimAssessment")%>'
                                                                            AutoPostBack="true" Enabled='<%#IsAllowToEnroll(Eval("ResitInterimAssessment")) %>'
                                                                            OnCheckedChanged="chkResitInterimAssessment_CheckedChanged" Checked='<%#IsEnrolled(Eval("ResitInterimAssessment"))%>'
                                                                            Visible='<%#IsVisible(Eval("ResitInterimAssessment"))%>' />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="AutumnExam" HeaderText="AE" SortExpression="AutumnExam"
                                                                AutoPostBackOnFilter="true" HeaderStyle-Width="7%"
                                                                ColumnGroupName="AutumnSession">
                                                                <ItemTemplate>
                                                                    <div class='<%#SetColorCode(Eval("AutumnExam"))%>'>
                                                                        <asp:CheckBox ID="chkAutumnExam" runat="server" CssClass="chkBox" Text='<%#Eval("AutumnExam")%>'
                                                                            AutoPostBack="true" Enabled="false" Checked='<%#IsEnrolled(Eval("AutumnExam"))%>'
                                                                            OnCheckedChanged="chkAutumnExam_CheckedChanged" Visible='<%#IsVisible(Eval("AutumnExam"))%>' />

                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </div>

                                        <div>
                                            <telerik:RadWindow ID="radwindowIAOpt" ReloadOnShow="false" runat="server" VisibleOnPageLoad="false" Height="170px"
                                                Title="Firm course enrollment" Width="350px" BackColor="#f4f3f1" VisibleStatusbar="false"
                                                Behaviors="None" Behavior="None" Modal="True" ForeColor="#BDA797">
                                                <ContentTemplate>
                                                    <div class="info-data">
                                                        <div style="height: 70px;" class="row-div clearfix">
                                                            <b>
                                                                <asp:Label ID="lblSubmitMessage" runat="server" Text=""></asp:Label></b>
                                                            <br />
                                                        </div>
                                                        <div class="row-div clearfix" align="center">

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
                                                    <td class="tdProcessing" style="vertical-align: middle">Please wait...
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
                        <div>
                        </div>
                        <div>
                            <div class="label-div w98">
                                <div>
                                </div>
                                <div>
                                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="submitBtn" />
                                    <asp:Button ID="btnCloseCourseEnrollment" Text="Cancel" runat="server" CssClass="submitBtn" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>

        </div>
        <div>
            <telerik:RadWindow ID="rwStudentBreakDown" VisibleOnPageLoad="false" ReloadOnShow="false" runat="server" Width="800px" Height="600px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="True" Behaviors="None" ForeColor="#BDA797"
                Title="Student break down" Behavior="None">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upStudentBreakDown" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="lblPopupStudentBreakDownMessage" runat="server"></asp:Label>
                            </div>
                            <div class="cai-form">
                                
                                    
                                        
                                            <div class="form-section-half-border">
                                                <div class="field-group">
                                                    <asp:Label ID="lblSBLastName" runat="server" Text="Last name " width="100%" Style="text-align: left;"></asp:Label>

                                                    <asp:TextBox ID="txtSBLastName" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                       
                                    
                                    
                                      <div class="form-section-half-border">
                                        <div class="field-group">
                                            <asp:Label ID="lblSBFirstName" runat="server" Text="First name " width="100%" Style="text-align: left;"></asp:Label>

                                            <asp:TextBox ID="txtSBFirstName" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                        </div>
                                      </div>
                                    
                                
                            </div>
                            <div>
                                <div>
                                    <div class="label-div w60">
                                        <telerik:RadGrid ID="gvCompletedCurriculum" runat="server" PageSize="4" AllowSorting="True"
                                            AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                            ShowGroupPanel="False" GridLines="none" AutoGenerateColumns="false">
                                            <PagerStyle CssClass="sd-pager" />
                                            <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                                <NoRecordsTemplate>
                                                    No record(s)
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Curriculum" HeaderText="Completed curriculum"
                                                        SortExpression="Curriculum" FilterControlWidth="80%" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" HeaderStyle-Width="50%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="CurrentStage" HeaderText="Current curriculum"
                                                        SortExpression="CurrentStage" FilterControlWidth="80%" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" HeaderStyle-Width="50%" HeaderStyle-Font-Bold="true"/>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                                <div>
                                    <div class="label-div w98">
                                        <telerik:RadGrid ID="gvCompletedCourses" runat="server" PageSize="4" AllowSorting="True"
                                            AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                            ShowGroupPanel="False" GridLines="none" AutoGenerateColumns="false">
                                            <PagerStyle CssClass="sd-pager" />
                                            <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                                <NoRecordsTemplate>
                                                    No record(s)
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="CourseName" HeaderText="Completed courses" SortExpression="CourseName" HeaderStyle-Width="25%"
                                                        ShowFilterIcon="false" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                                        HeaderStyle-Width="25%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="ExamSat" HeaderText="Exam sat" SortExpression="ExamSat"  HeaderStyle-Width="40%" HeaderStyle-Font-Bold="true"/>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                                <div>
                                    <div class="label-div w98">
                                        <telerik:RadGrid ID="gvFailedCourses" runat="server" PageSize="4" AllowSorting="True"
                                            AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                            ShowGroupPanel="False" GridLines="none" AutoGenerateColumns="false">
                                            <PagerStyle CssClass="sd-pager" />
                                            <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                                <NoRecordsTemplate>
                                                    No record(s)
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="CourseName" HeaderText="Failed courses" SortExpression="CourseName" HeaderStyle-Width="25%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                                        HeaderStyle-Width="25%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="ExamSat" HeaderText="Exam sat" SortExpression="ExamSat" HeaderStyle-Width="40%" HeaderStyle-Font-Bold="true"/>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                                <div>
                                    <div class="label-div w98">
                                        <telerik:RadGrid ID="gvCurrentCourses" runat="server" PageSize="4" AllowSorting="True"
                                            AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                            GridLines="none" AutoGenerateColumns="false">
                                            <PagerStyle CssClass="sd-pager" />
                                            <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                                <NoRecordsTemplate>
                                                    No record(s)
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="CourseName" HeaderText="Current courses" SortExpression="CourseName" HeaderStyle-Width="25%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName" HeaderStyle-Width="25%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="ExamSat" HeaderText="Exam sat" SortExpression="ExamSat" HeaderStyle-Width="20%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridTemplateColumn DataField="IsRevision" HeaderText="Revision" SortExpression="IsRevision" HeaderStyle-Width="20%" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <div>
                                                                <asp:CheckBox ID="chkCurrentExamsIsResit" runat="server" Enabled="false" Checked='<%# IIf(Eval("IsRevision")=1,true,false) %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="TotalCost" HeaderText="Total cost" SortExpression="TotalCost" visible="false"/>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                                <div>
                                    <div class="label-div w98">
                                        <telerik:RadGrid ID="gvCurrentExams" runat="server" PageSize="4" AllowSorting="True"
                                            AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                            GridLines="none" AutoGenerateColumns="false">
                                            <PagerStyle CssClass="sd-pager" />
                                            <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                                <NoRecordsTemplate>
                                                    No record(s)
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="CourseName" HeaderText="Exams" SortExpression="CourseName" HeaderStyle-Width="25%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName" HeaderStyle-Width="25%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="ExamSat" HeaderText="Exam sat" SortExpression="ExamSat" HeaderStyle-Width="20%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridTemplateColumn DataField="IsResit" HeaderText="Resit" SortExpression="IsResit" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <div>
                                                                <asp:CheckBox ID="chkCurrentExamsIsResit" runat="server" Enabled="false" Checked='<%# IIf(Eval("IsResit")=1,true,false) %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn DataField="IsMock" HeaderText="Mock" SortExpression="IsMock" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <div>
                                                                <asp:CheckBox ID="chkIsMock" runat="server" Enabled="false" Checked='<%# IIf(Eval("IsMock")=1,true,false) %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridNumericColumn DataField="TotalCost" HeaderText="Total cost" SortExpression="TotalCost" visible="false" HeaderStyle-Font-Bold="true"/>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <div class="label-div w98">
                                        <telerik:RadGrid ID="gvIAResits" runat="server" PageSize="4" AllowSorting="True"
                                            AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                            GridLines="none" AutoGenerateColumns="false">
                                            <PagerStyle CssClass="sd-pager" />
                                            <MasterTableView Width="100%" ShowHeadersWhenNoRecords="true">
                                                <NoRecordsTemplate>
                                                    No record(s)
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="CourseName" HeaderText="IA resits" SortExpression="CourseName" HeaderStyle-Width="25%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName" HeaderStyle-Width="25%" HeaderStyle-Font-Bold="true"/>

                                                    <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridBoundColumn DataField="ExamSat" HeaderText="Exam sat" SortExpression="ExamSat" HeaderStyle-Width="20%" HeaderStyle-Font-Bold="true"/>
                                                    <telerik:GridTemplateColumn DataField="IsResit" HeaderText="Resit" SortExpression="IsResit" HeaderStyle-Width="20%" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <div>
                                                                <asp:CheckBox ID="chkIAIsResit" runat="server" Enabled="false" Checked='<%# IIf(Eval("IsResit")=1,true,false) %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridNumericColumn DataField="TotalCost" HeaderText="Total cost" SortExpression="TotalCost" visible="false"/>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div>
                        <div>
                            <div class="label-div w24">
                                <asp:Label ID="lblTotalCost" runat="server"  visible="false"></asp:Label>
                            </div>
                            <div class="field-div w98">
                                <asp:Button ID="btnCloseBreakDown" Text="Cancel" runat="server" CssClass="submitBtn" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </div>
        <cc1:User ID="loggedInUser" runat="server" />
        <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
        </ContentTemplate>        
    </asp:UpdatePanel>
    <%--</telerik:RadAjaxPanel>--%>
</div>
