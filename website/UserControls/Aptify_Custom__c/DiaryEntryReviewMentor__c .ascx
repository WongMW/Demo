<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DiaryEntryReviewMentor__c .ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.DiaryEntryReviewMentor__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>
<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../../Scripts/jquery-ui-1.11.2.min.js" type="text/javascript"></script>
<script src="../../Scripts/expand.js" type="text/javascript"></script>
<script src="../../Scripts/JScript.min.js" type="text/javascript"></script>
<style type="text/css">
    .confirmBox
    {
        display: none;
        background-color: #ccc;
        border: 2px solid #aaa;
        position: absolute;
        width: 500px;
        left: 30%;
        top: 20%;
        padding: 15px 15px 15px;
        box-sizing: border-box;
        text-align: center;
        z-index: 100;
    }
    
    #confirmOverlay
    {
        display: none;
        width: 100%;
        height: 100%;
        position: fixed;
        top: 0;
        left: 0; /* background-color: #bbbccc;*/
        z-index: 0; /* filter: alpha(opacity = 30);*/
    }
    .CssStyle1
    {
        display: block;
        text-align: center;
    }
    
    .cssEditor
    {
        height: 50%; /* width :572px;
        min-height:70px;
        min-width :250px;  */
    }
    
    .panel-title a
    {
        background: url('plus.png') no-repeat 100% 50%;
        padding-right: 20px;
    }
    .panel-title a:hover
    {
        background: url('minus.png') no-repeat 100% 50%;
    }
    .panel-title a.active
    {
        background: url('minus.png') no-repeat 100% 50%;
    }
    .MyGrid .rgDataDiv
    {
        overflow: auto;
        width: 100%;
        height: 80px !important;
    }
    .MyGridNew .rgRow
    {
        height: 20px !important;
    }
</style>
<script type="text/javascript">

    $(document).ready(function () {
        var PanelState1 = $("#<%=hdnTimeFrameState.ClientID%>").val();
        var PanelState2 = $("#<%=hdnDiaryEntryState.ClientID%>").val();
        var PanelState3 = $("#<%=hdnExemptionsGrantedState.ClientID%>").val();

        var PanelState4 = $("#<%=hdnStatusReasonState.ClientID%>").val();


        var PanelState5 = $("#<%=hdnEligibilityState.ClientID%>").val();
        var PanelState6 = $("#<%=hdnStudentInfo.ClientID%>").val();

        if (PanelState3 == '1') {
            $('#ExemptionsGranted').removeClass("collapse").addClass("active");
        }
        if (PanelState2 == '1') {
            $('#Employement').removeClass("collapse").addClass("active");
        }
        if (PanelState1 == '1') {
            $('#TimeFrame').removeClass("collapse").addClass("active");
        }


        if (PanelState4 == '1') {
            $('#StatusReason').removeClass("collapse").addClass("active");
        }

        if (PanelState5 == '1') {
            $('#Eligibility').removeClass("collapse").addClass("active");
        }

        if (PanelState6 == '1') {
            $('#StudentInfo').removeClass("collapse").addClass("active");
        }

    });

    function ShowTermsandcondtionPopup() {
        $("#saveMessage").show();
        $("#confirmOverlay").css("display", "block");
        return true;
    }


    function closePopup() {
        $("#saveMessage").hide();
        $("#confirmOverlay").css("display", "none");
        return true;
    }
    $('[id$=hlTermsandconditions]').live('click', function (event) {
        ShowTermsandcondtionPopup();

        return false;

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
        if (HiddenPanelState == 'hdnTimeFrameState') {
            $("#<%=hdnTimeFrameState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnDiaryEntryState') {
            $("#<%=hdnDiaryEntryState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnExemptionsGrantedState') {
            $("#<%=hdnExemptionsGrantedState.clientID %>").val(StateValue);
        }

        if (HiddenPanelState == 'hdnStatusReasonState') {
            $("#<%=hdnStatusReasonState.clientID %>").val(StateValue);
        }

        if (HiddenPanelState == 'hdnEligibilityState') {
            $("#<%=hdnEligibilityState.clientID %>").val(StateValue);
        }

        if (HiddenPanelState == 'hdnStudentInfo') {
            $("#<%=hdnStudentInfo.clientID %>").val(StateValue);
        }

    }

    function HideLabel(sender, args) {
        var input = args.get_fileName();
        var n = input.indexOf(".");
        var fileExtension = input.substr(n + 1);
        var extensionArrayList = new Array("png", "jpg", "txt", "doc", "docx", "pdf", "bmp", "gif");
        for (var i = 0; i < extensionArrayList.length; i++) {
            if (fileExtension == extensionArrayList[i]) {
                if (document.getElementById('<%=lblErrorFile.ClientID%>'))
                    document.getElementById('<%=lblErrorFile.ClientID%>').style.display = 'none';
                return false;
            }
        }
    }

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
<asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    </ContentTemplate>
</asp:UpdatePanel>
<div id="divContent" runat="server">
    <asp:HiddenField ID="hdnStudentInfo" runat="server" Value="1" />
    <asp:HiddenField ID="hdnTimeFrameState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnDiaryEntryState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnExemptionsGrantedState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnStatusReasonState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEligibilityState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCADiaryRecordID" runat="server" Value="-1" />
    <asp:HiddenField ID="HFMentorID" runat="server" Value="-1" />
    <asp:HiddenField runat="server" ID="hdStudentID" Value="0" />
    <table id="MainTable" style="width: 100%">
        <asp:Label ID="lblMessage1" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        <tr>
            <td>
                <asp:Panel ID="pnlMain" runat="server">
                    <div class="row-div clearfix" style="text-align: center; color: Black; font-weight: bold">
                        <h1 style="text-align: center; color: Black">
                            Detailed Diary Entry
                        </h1>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelStudInfo" runat="server">
                    <h2 class="expand" id="HeadStudentInfo" align="center" onclick="CollapseExpand('StudentInfo','hdnStudentInfo')">
                        Student Info</h2>
                    <div id="StudentInfo" class="collapse">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w30">
                                            Student Name :
                                            <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                        </div>
                                        <div class="label-div-left-align w30">
                                            Company :
                                            <asp:Label ID="lblCompany" runat="server"></asp:Label>
                                        </div>
                                        <div class="label-div-left-align w30">
                                            Business Unit:
                                            <asp:Label ID="lblBusiness" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w30">
                                            Route of Entry :
                                            <asp:Label ID="lblRouteofEntry" runat="server"></asp:Label>
                                        </div>
                                        <div class="label-div-left-align w30">
                                            Mentor :
                                            <asp:Label ID="lblMentorName" runat="server"></asp:Label>
                                        </div>
                                        <div class="label-div-left-align w30">
                                            Entry Type :
                                            <asp:Label ID="lblEntryType" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                </div> </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlDetails" runat="server">
                    <h2 class="expand" id="HeadTimeFrame" align="center" onclick="CollapseExpand('TimeFrame','hdnTimeFrameState')">
                        Time Frame</h2>
                    <div id="TimeFrame" class="collapse">
                        <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w20">
                                            <asp:Label ID="Label3" Width="300px" runat="server" Text="Diary Entry pertains to the following timeframe"
                                                Font-Underline="true"></asp:Label></b>
                                            <div runat="server" id="EduDetails" class="info-data">
                                                <div class="row-div clearfix">
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="field-div1 w75">
                                                        <span class="RequiredField">*</span>Start Date:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <rad:RadDatePicker ID="txtStartDate" runat="server" AutoPostBack="true" Width="100px">
                                                        </rad:RadDatePicker>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="label-div w58">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    &nbsp;</div>
                                                <div class="row-div clearfix">
                                                    <div runat="server" id="Div1" class="info-data">
                                                        <div class="row-div clearfix">
                                                            <div class="field-div1 w75">
                                                                <span class="RequiredField">*</span>End Date:
                                                            </div>
                                                            <div class="field-div1 w75">
                                                                <rad:RadDatePicker ID="txtEndDate" runat="server" AutoPostBack="true" Width="100px">
                                                                </rad:RadDatePicker>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <asp:Label ID="Label4" Width="250px" runat="server" Text="Out of Office" Font-Underline="true"></asp:Label></b>
                                            <div>
                                                &nbsp;</div>
                                            <div runat="server" id="Div6" class="info-data">
                                                <div class="row-div clearfix">
                                                    <asp:Panel ID="PanelLeave" runat="server">
                                                        <b>Leave Added: </b>
                                                        <br />
                                                        <rad:RadGrid ID="grdLeave" CssClass="MyGrid" runat="server" AutoGenerateColumns="False"
                                                            AllowPaging="true" AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                                                            AllowSorting="false" SortingSettings-SortedAscToolTip="Sorted Ascending" Visible="true">
                                                            <GroupingSettings CaseSensitive="false" />
                                                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                                                                <Columns>
                                                                    <rad:GridTemplateColumn HeaderText="Leave Type" DataField="LeaveType" AutoPostBackOnFilter="true"
                                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLeaveType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LeaveType") %>'></asp:Label><asp:HiddenField
                                                                                ID="hdnLeaveTypeID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LeaveTypeID") %>' />
                                                                        </ItemTemplate>
                                                                    </rad:GridTemplateColumn>
                                                                    <rad:GridTemplateColumn HeaderText="Days" DataField="Days" AutoPostBackOnFilter="true"
                                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Days") %>'></asp:Label></ItemTemplate>
                                                                    </rad:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView></rad:RadGrid>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <asp:Label ID="lblExperience" Width="300px" runat="server" Text="Days of Relevant Experience Within Timeframe"
                                                Font-Underline="true"></asp:Label></b>
                                            <div runat="server" id="Div7" class="info-data">
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row-div clearfix">
                                            <h3 style="text-align: left; font-size: small; color: Black; font-weight: 500">
                                                <span class="RequiredField">*</span>Please enter the number of days of relevant
                                                experience pertaining to this diary entry:&nbsp;<asp:TextBox ID="txtExperience" runat="server"
                                                    Width="100px" onkeypress="return AllowNumericOnly(event)" MaxLength="8"></asp:TextBox>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlDiaryEntry" runat="server">
                    <h2 class="expand" id="HeadDiaryEntry" align="center" onclick="CollapseExpand('DiaryEntry','hdnHeadDiaryEntryState')">
                        Diary Entry</h2>
                    <div id="DiaryEntry" class="collapse">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div>
                                            <br />
                                        </div>
                                        <div class="label-div-left-align w13">
                                            <b>
                                                <asp:Label ID="lblTitle" runat="server"><span class="RequiredField">*</span>Title:</asp:Label></b>
                                        </div>
                                        <div class="field-div1 w60">
                                            <asp:TextBox ID="txtTitle" Width="400px" runat="server" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<br />--%>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w13">
                                            <b>
                                                <asp:Label ID="lbldes" runat="server"><span class="RequiredField">*</span>Description:</asp:Label></b>
                                        </div>
                                        <div class="field-div1 w60">
                                            <asp:Label ID="lblDesc" runat="server" Visible="True"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w13">
                                            <b>
                                                <asp:Label ID="lblll" runat="server"><span class="RequiredField">*</span>Learning Level:</asp:Label></b>
                                        </div>
                                        <div class="field-div1 w60">
                                            <asp:Label ID="lblLearningLevel" runat="server" Visible="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%-- <asp:AsyncPostBackTrigger ControlID="btnSavenExit" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSubmitToMentor" EventName="Click" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlEligibility" runat="server">
                    <h2 id="H1" class="expand" align="center" onclick="CollapseExpand('Eligibility','hdnEligibilityState')">
                        Compentencies/Area of Experience Achieved
                    </h2>
                    <div id="Eligibility" style="vertical-align: top;" class="collapse">
                        <asp:UpdatePanel ID="UpdatePaneExperience" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div style="width: 60%;" class="field-div1 w225">
                                            <asp:Panel ID="PanelCompetency" Style="width: 760;" runat="server">
                                                <div runat="server" id="Div8" class="info-data">
                                                    <div id="divcmp" class="row-div clearfix" runat="server">
                                                        <b>Competencies/Area of Experience Added:</b></div>
                                                    <div class="row-div clearfix">
                                                        <rad:RadGrid ID="gvCompetency" CssClass="MyGrid" runat="server" AutoGenerateColumns="False"
                                                            AllowPaging="false" AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                                                            AllowSorting="false" SortingSettings-SortedAscToolTip="Sorted Ascending" Visible="true"
                                                            ShowHeadersWhenNoRecords="true">
                                                            <ClientSettings>
                                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                            </ClientSettings>
                                                            <GroupingSettings CaseSensitive="false" />
                                                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="true"
                                                                AllowNaturalSort="false" ShowHeadersWhenNoRecords="true">
                                                                <NoRecordsTemplate>
                                                                    <div>
                                                                        No Data to Display
                                                                    </div>
                                                                </NoRecordsTemplate>
                                                                <Columns>
                                                                    <rad:GridTemplateColumn HeaderText="Competency/Area of Experience category" AutoPostBackOnFilter="true"
                                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="28%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExpCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ComptencyCategory") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnCompetencyID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"ComptencyID") %>' />
                                                                            <asp:HiddenField ID="hdnCID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"cID") %>' />
                                                                        </ItemTemplate>
                                                                    </rad:GridTemplateColumn>
                                                                    <rad:GridTemplateColumn HeaderText="Competency/Area of Experience" AutoPostBackOnFilter="true"
                                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="22%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExperience" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Experience") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </rad:GridTemplateColumn>
                                                                </Columns>
                                                                <ItemStyle CssClass="MyGridNew" Height="20px" />
                                                            </MasterTableView>
                                                        </rad:RadGrid>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="width: 5%;" class="field-div1 w225">
                                            &nbsp;
                                        </div>
                                        <div style="width: 25%;" class="field-div1 w225">
                                            <asp:Panel ID="PanelRegExp" Style="width: 250%;" runat="server" Visible="true">
                                                <%--   <div runat="server" id="Div3" class="info-data" >--%>
                                                <div style="width: 280px;" class="field-div1 w250">
                                                    <div class="row-div clearfix">
                                                        <b>Regulated Experience in a Practice Environment : </b>
                                                    </div>
                                                    <div>
                                                        <asp:Panel ID="Panel2" BorderWidth="1" Width="250px" Style="border: 1; background: white;"
                                                            runat="server">
                                                            <div class="row-div clearfix">
                                                                <div class="field-div1 w5">
                                                                    <div>
                                                                        &nbsp;
                                                                    </div>
                                                                </div>
                                                                <div class="field-div1 w40">
                                                                    <div>
                                                                        <b><u>Audit Type</u></b>
                                                                    </div>
                                                                </div>
                                                                <div class="field-div1 w15">
                                                                    <div style="text-align: right;">
                                                                        <b><u>Days<u></b>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row-div clearfix">
                                                                <div class="field-div1 w5">
                                                                    <div>
                                                                        &nbsp;
                                                                    </div>
                                                                </div>
                                                                <div class="field-div1 w40">
                                                                    <div>
                                                                        Company Audit
                                                                    </div>
                                                                    <br />
                                                                    <div>
                                                                        Other Audit
                                                                    </div>
                                                                </div>
                                                                <div class="field-div1 w15">
                                                                    <div style="text-align: right;">
                                                                        <%-- <asp:Label ID="lblCompanyAuditday" runat="server" Text="0"></asp:Label>--%>
                                                                        <asp:TextBox ID="txtCompanyDays" runat="server" Width="180%" MaxLength="8" onkeypress="return AllowNumericOnly(event)"></asp:TextBox>
                                                                    </div>
                                                                    <br />
                                                                    <div style="text-align: right;">
                                                                        <%--<asp:Label ID="lblOtherAuditday" runat="server" Text="0"></asp:Label>--%>
                                                                        <asp:TextBox ID="txtOtherDays" runat="server" Width="180%" MaxLength="8" onkeypress="return AllowNumericOnly(event)"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div w15" style="text-align: left">
                                            <div runat="server" id="Div9" class="info-data">
                                                <div class="row-div clearfix">
                                                </div>
                                                <div class="row-div clearfix">
                                                    Engagement Numbers:&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                        <div class="label-div w75" style="text-align: left">
                                            <asp:TextBox ID="txtEngagmentNumber" runat="server" Width="400px" Height="50px"></asp:TextBox><br />
                                            <asp:Label ID="lblEngagement" Width="450px" Font-Bold="false" runat="server"></asp:Label>
                                        </div>
                                        <br />
                                        <div class="row-div clearfix">
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="btnSavenExit" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSubmitToMentor" EventName="Click" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PriorExp" runat="server" Visible="false">
                    <h2 id="HeadDeclaration" class="expand" align="center" onclick="CollapseExpand('ExemptionsGranted','hdnExemptionsGrantedState')">
                        Prior Year's Experience
                    </h2>
                    <div id="ExemptionsGranted" style="vertical-align: top;" class="collapse">
                        <asp:Panel BorderWidth="0" Style="padding-left: 30pt; vertical-align: top;" ID="pnlCotractDeclaration"
                            runat="server">
                            <div class="row-div clearfix">
                                <div>
                                    <br />
                                </div>
                                <div runat="server" id="divAttachments" style="font-size: 8pt; color: Black; font-style: normal;
                                    font-weight: normal; width: 80%;">
                                    <div>
                                        <span class="Error">
                                            <asp:Label ID="lblErrorFile" Visible="false" runat="server">Error</asp:Label></span></div>
                                    <div>
                                        <uc2:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                                            AllowAdd="True" AllowDelete="false" />
                                    </div>
                                </div>
                                <div>
                                    <br />
                                </div>
                            </div>
                            <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Status" runat="server">
                    <div class="row-div clearfix">
                        <div class="label-div-left-align">
                            <b>
                                <asp:Label ID="lblsts" runat="server" Text="Status:"></asp:Label>
                                <asp:Label ID="lblStatus" runat="server" Text="With Student"></asp:Label></b>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row-div clearfix">
                    <h1 style="text-align: right; color: Black">
                        <asp:Button ID="btnCancelnBack" runat="server" Text="Cancel & Back" />&nbsp;&nbsp;
                        <asp:Button ID="btnLocknApprove" runat="server" Text="Lock & Approve" />&nbsp;&nbsp;
                        <asp:Button ID="btnUnlockEntry" runat="server" Text="Unlock Entry" Visible="false" />&nbsp;&nbsp;
                    </h1>
                </div>
            </td>
        </tr>
    </table>
    <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Modal="True"
        BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="CA Diary Entry" Behavior="None" Height="150px">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblValidationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div>
                            <br />
                        </div>
                        <div>
                            <asp:Button ID="btnValidationOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
</div>
<div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
