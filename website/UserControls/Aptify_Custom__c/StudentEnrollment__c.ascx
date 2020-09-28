<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StudentEnrollment__c.ascx.vb"
    Inherits="StudentEnrollment__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.8.9.js" type="text/javascript"></script>
<script src="../Scripts/expand.js" type="text/javascript"></script>
<script type="text/javascript">
    function ClientItemSelected(sender, e) {
        document.getElementById("<%=hdnCompanyId.ClientID %>").value = e.get_value();
    }
    function fnClearHidden() {
        document.getElementById("<%=hdnCompanyId.ClientID %>").value = '';

    }
    $(document).ready(function () {
        var PanelState1 = $("#<%=hdnEnrollmentDetailsState.ClientID%>").val();
        var PanelState2 = $("#<%=hdnExemptionsGrantedState.ClientID%>").val();
        var PanelState3 = $("#<%=hdnEnrollmentOptionsState.ClientID%>").val();
        var PanelState4 = $("#<%=hdnCreditCard.ClientID%>").val();
        if (PanelState4 == '1') {
            $('#divCreditCardDetails').removeClass("collapse").addClass("active");
        }
        if (PanelState3 == '1') {
            $('#EnrollmentOptions').removeClass("collapse").addClass("active");
        }
        if (PanelState2 == '1') {
            $('#ExemptionsGranted').removeClass("collapse").addClass("active");
        }
        if (PanelState1 == '1') {
            $('#EnrollmentDetails').removeClass("collapse").addClass("active");
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
        if (HiddenPanelState == 'hdnEnrollmentDetailsState') {
            $("#<%=hdnEnrollmentDetailsState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnEnrollmentOptionsState') {
            $("#<%=hdnEnrollmentOptionsState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnExemptionsGrantedState') {
            $("#<%=hdnExemptionsGrantedState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnCreditCard') {
            $("#<%=hdnCreditCard.clientID %>").val(StateValue);
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
</style>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
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
</div>
<div id="divContent" runat="server">
    <asp:HiddenField ID="hdnCompanyId" runat="server" Value="" />
    <asp:HiddenField ID="hdnAwardingBody" runat="server" Value="0" />
    <asp:HiddenField ID="hdnQualification" runat="server" Value="0" />
    <table id="MainTable" style="width: 100%">
        <tr>
            <td>
                <asp:Panel ID="pnlDetails" runat="server">
                    <div class="info-data">
                        <div class="row-div clearfix">
                             <div class="label-div-left-align">
                                 <asp:Button ID="btnPrint" visible="false"  runat="server" Text="PRINT INVOICE" class="submitBtn" />
                            </div>
                            <div class="label-div-left-align">
                                Name: <b>
                                    <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w16">
                                Student Number: <b>
                                    <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w16">
                                Status: <b>
                                    <asp:Label ID="lblStatus" runat="server" Text="With Student"></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w26">
                                <%--  Comments: <b>
                                    <asp:Label ID="lblComments" runat="server" Text=""></asp:Label></b>--%>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <h2 class="expand" id="HeadEnrollmentDetails" onclick="CollapseExpand('EnrollmentDetails','hdnEnrollmentDetailsState')">
                        Enrollment Details</h2>
                    <div id="EnrollmentDetails" class="collapse">
                        <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        &nbsp;</div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    <div class="label-div w40">
                                                        Route of Entry:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:DropDownList ID="ddlRoute" runat="server" AutoPostBack="true" Width="102%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w40">
                                                        Firm/Organization:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtFirm" runat="server" placeholder="Please start typing..." CssClass="textbox"
                                                            MaxLength="100" Width="88%" AutoPostBack="true" AutoComplete="off" AutoCompleteType="Disabled" />
                                                        <Ajax:AutoCompleteExtender ID="autoCompany" runat="server" TargetControlID="txtFirm"
                                                            BehaviorID="auto1" ServicePath="~/WebServices/GetCompanyDetails__c.asmx" EnableCaching="false"
                                                            MinimumPrefixLength="1" FirstRowSelected="false" ServiceMethod="GetCompanyDetailsForStudEnroll"
                                                            UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientItemSelected" />
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w40">
                                                        Address Line 1:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtAddrLine1" runat="server" CssClass="textbox" Width="88%" AutoComplete="off"
                                                            AutoCompleteType="Disabled" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w40">
                                                        Address Line 2:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtAddrLine2" runat="server" CssClass="textbox" Width="88%" AutoComplete="off"
                                                            AutoCompleteType="Disabled" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w40">
                                                        Postal Code:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="textbox" Width="88%" AutoComplete="off"
                                                            AutoCompleteType="Disabled" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="label-div-left-align w50">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    <div class="field-div1 w99">
                                                        Please provide any further information
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="field-div1 w99">
                                                        <asp:TextBox ID="txtFurtherInfo" runat="server" CssClass="textbox" Width="88%" TextMode="MultiLine"
                                                            AutoComplete="off" AutoCompleteType="Disabled" Style="resize: none" />
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w5">
                                                        <asp:CheckBox ID="chkInfoToFirm" runat="server" Text="" AutoPostBack="true" />
                                                    </div>
                                                    <div class="field-div1 w74">
                                                        <asp:Label ID="Label2" runat="server" Style="vertical-align: middle" Text="Please tick to share your information with the firm" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w60">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    <div class="label-div w30">
                                                        <asp:CheckBox ID="chkInfoToFirm" runat="server" Text="" AutoPostBack="true" />
                                                    </div>
                                                    <div class="field-div1 w55">
                                                        <asp:Label ID="Label2" runat="server" Style="vertical-align: middle" Text="Please tick to share your information with the firm" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <asp:Label ID="lblEEMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="ddlRoute" />--%>
                                <%--<asp:PostBackTrigger ControlID="ddlRoute" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <h2 id="HeadExemptionsGranted" class="expand" onclick="CollapseExpand('ExemptionsGranted','hdnExemptionsGrantedState')">
                        Exemptions Granted</h2>
                    <div id="ExemptionsGranted" class="collapse">
                        <div class="info-data">
                            <div class="row-div clearfix">
                                &nbsp;</div>
                            <div class="row-div clearfix">
                                <b>Exemptions Granted</b></div>
                            <div class="row-div clearfix">
                                <div class="label-div-left-align w80">
                                    <telerik:RadGrid ID="grdGrantedExempts" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        AllowFilteringByColumn="True" Visible="false">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                <telerik:GridBoundColumn DataField="CourseName" HeaderText="Course" SortExpression="CourseName"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                <telerik:GridDateTimeColumn DataField="ExpirationDate" HeaderText="Expiry Date" SortExpression="ExpirationDate"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                    FilterControlWidth="60%" EnableTimeIndependentFiltering="true">
                                                </telerik:GridDateTimeColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <asp:Label ID="lblGrantExemptedMsg" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <b>Passed as External</b></div>
                            <div class="row-div clearfix">
                                <div class="label-div-left-align w80">
                                    <telerik:RadGrid ID="grdExternalPassed" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        AllowFilteringByColumn="True" Visible="false">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                <telerik:GridBoundColumn DataField="CourseName" HeaderText="Course" SortExpression="CourseName"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                <telerik:GridDateTimeColumn DataField="ExpirationDate" HeaderText="Expiry Date" SortExpression="ExpirationDate"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                    FilterControlWidth="60%" EnableTimeIndependentFiltering="true">
                                                </telerik:GridDateTimeColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <asp:Label ID="lblExternalPassedMsg" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h2 class="expand" id="HeadEnrollmentOptions" onclick="CollapseExpand('EnrollmentOptions','hdnEnrollmentOptionsState')" runat="server">
                        Enrollment Options</h2>
                    <div id="EnrollmentOptions" class="collapse"  >
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                <ContentTemplate>
                        <div class="info-data" id="idEnroleddiv" runat="server">
                            <div class="row-div clearfix">
                                &nbsp;</div>
                         
                                    <div class="row-div clearfix">
                                        <%-- <h2 class="expand" id="HeadCAP1" onclick="CollapseExpand('CAP1','hdnCAP1State')" style="background-color: Gray" >
                                        CAP 1</h2>
                                    <div id="CAP1" class="collapse">--%>
                                        <div class="info-data" runat="server" id="Div1">
                                            <div class="row-div clearfix">
                                                &nbsp;</div>
                                            <div class="row-div clearfix">
                                                <div class="label-div w30" align="left">
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
                                                                <div class="not_available">
                                                                </div>
                                                            </div>
                                                            <div class="field-div1 w60" align="left">
                                                                <asp:Label ID="lblNotAvailable" runat="server" Font-Bold="True" Font-Size="8" Text="Not Available"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="label-div w30" align="left">
                                                    <div class="info-data">
                                                        <div class="row-div clearfix">
                                                            <div class="label-div w15">
                                                                <div class="available">
                                                                    <asp:CheckBox ID="chkRequestEnrolled" runat="server" Enabled="false" Checked="true" />
                                                                </div>
                                                            </div>
                                                            <div class="field-div1 w80" align="left">
                                                                <asp:Label ID="lblEnrollmentExists" runat="server" Font-Bold="True" Font-Size="8"
                                                                    Text="Current Option for Core Course"></asp:Label>
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
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-div w40" align="left">
                                                    <div class="info-data">
                                                        <div class="row-div clearfix">
                                                            <%-- <div class="label-div-left-align w80">--%>
                                                            <div class="label-div w25">
                                                                <asp:Label ID="lblStudentGroup" runat="server" Text="Student Group:"></asp:Label>
                                                            </div>
                                                            <div class="field-div1 w75">
                                                                <asp:DropDownList ID="ddlCAP1StudGrp" runat="server" AutoPostBack="true" Width="102%">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <%-- </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
<div class="field-group">
 <asp:Label ID="lblMsgWarning" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblCentrallyMngError" runat="server" Text="" Visible="false"></asp:Label>
</div>
                                            <div class="row-div clearfix">
                                                <div class="label-div-left-align w150">
                                                    <div style="overflow-x: auto; width: 100%">
                                                        <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
                                                        <telerik:RadGrid ID="gvCurriculumCourse" runat="server" PageSize="10" AllowSorting="True"
                                                            AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                                            ShowGroupPanel="True" GridLines="none" AutoGenerateColumns="false">
                                                            <MasterTableView Width="100%" DataKeyNames="CurriculumID,SubjectID,IsCourseJurisdiction,IsFAEElective">
                                                                <NoRecordsTemplate>
                                                                    No record(s)
                                                                </NoRecordsTemplate>
                                                                <ColumnGroups>
                                                                    <%--<telerik:GridColumnGroup HeaderText="Autumn" Name="AutumnSession" />
                                                                    <telerik:GridColumnGroup HeaderText="Summer" Name="SummerSession" />--%>
                                                                    <rad:GridColumnGroup HeaderText="Autumn" Name="Current" HeaderStyle-HorizontalAlign="Center">
                                                                    </rad:GridColumnGroup>
                                                                    <rad:GridColumnGroup HeaderText="Summer" Name="Next" HeaderStyle-HorizontalAlign="Center">
                                                                    </rad:GridColumnGroup>
                                                                </ColumnGroups>
                                                                <Columns>
                                                                    <telerik:GridTemplateColumn DataField="IsFAEElective" UniqueName="IsFAEElective"
                                                                        HeaderText="FAE Elective" SortExpression="IsFAEElective" HeaderStyle-Width="5%"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <div align="center">
                                                                                <asp:CheckBox ID="chkIsFAEElective" runat="server" CssClass="chkBox" Enabled='<%#IIf(Eval("IsFAEElective")=1,true,false) %>'
                                                                                    OnCheckedChanged="chkIsFAEElective_CheckedChanged" AutoPostBack="true" />
                                                                                <asp:Label ID="lblIsFAEElective" runat="server" Text='<%# Eval("IsFAEElective") %>'
                                                                                    Visible="false"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                        FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                                    <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject" SortExpression="Subject"
                                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                        FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                                    <telerik:GridBoundColumn DataField="SubjectID" HeaderText="SubjectID" SortExpression="SubjectID"
                                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                        FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                        Visible="false" />
                                                                    <telerik:GridBoundColumn DataField="AlternativeGroupID" HeaderText="Alternate Timetable"
                                                                        SortExpression="AlternativeGroupID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="8%" ItemStyle-Width="8%"
                                                                        ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                                    <telerik:GridBoundColumn DataField="AlternativeGroup" HeaderText="Alternate Timetable"
                                                                        SortExpression="AlternativeGroup" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-Width="8%"
                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                    <telerik:GridTemplateColumn DataField="RepeatRevision" HeaderText="Revision Course"
                                                                        SortExpression="RepeatRevision" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="Current">
                                                                        <ItemTemplate>
                                                                            <div align="center" class='<%#SetColorCode(Eval("RepeatRevision"))%>'>
                                                                                <asp:Label ID="lblIsCourseJurisdiction" runat="server" Text='<%# Eval("IsCourseJurisdiction") %>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblCurriculumID" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblCutOffUnits" runat="server" Text='<%# Eval("CutOffUnits") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblIsCore__c" runat="server" Text='<%# Eval("IsCore__c") %>' Visible="false"></asp:Label>
                                                                                <%--<asp:Label ID="lblAcademicCycleID" runat="server" Text='<%# Eval("AcademicCycleID") %>'
                                                                                    Visible="false"></asp:Label>--%>
                                                                                <asp:Label ID="lblMinimumUnits" runat="server" Text='<%# Eval("MinimumUnits") %>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblUnits" runat="server" Text='<%# Eval("Units") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblRepeatRevision" runat="server" Text='<%# Eval("RepeatRevision") %>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblAlternativeGroup" runat="server" Text='<%# Eval("AlternativeGroup") %>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:CheckBox ID="chkRepeatRevision" runat="server" AutoPostBack="true" OnCheckedChanged="chkRepeatRevision_CheckedChanged"
                                                                                    Enabled='<%#IsAllowToEnroll(Eval("RepeatRevision")) %>' Checked='<%#IsEnrolled(Eval("RepeatRevision"))%>'
                                                                                    Visible='<%#IsVisible(Eval("RepeatRevision"))%>' />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn DataField="ResitInterimAssessment" HeaderText="Resit Interim"
                                                                        SortExpression="ResitInterimAssessment" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="Current">
                                                                        <ItemTemplate>
                                                                            <div align="center" class='<%#SetColorCode(Eval("ResitInterimAssessment"))%>'>
                                                                                <asp:Label ID="lblResitInterimAssessment" runat="server" Text='<%# Eval("ResitInterimAssessment") %>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:CheckBox ID="chkResitInterimAssessment" runat="server" AutoPostBack="true" OnCheckedChanged="chkResitInterimAssessment_CheckedChanged"
                                                                                    Enabled='<%#IsAllowToEnroll(Eval("ResitInterimAssessment")) %>' Checked='<%#IsEnrolled(Eval("ResitInterimAssessment"))%>'
                                                                                    Visible='<%#IsVisible(Eval("ResitInterimAssessment"))%>' />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn DataField="AutumnExam" HeaderText="Autumn Exam" SortExpression="AutumnExam"
                                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                        FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="Current">
                                                                        <ItemTemplate>
                                                                            <div align="center" class='<%#SetColorCode(Eval("AutumnExam"))%>'>
                                                                                <asp:Label ID="lblAutumnExam" runat="server" Text='<%# Eval("AutumnExam") %>' Visible="false"></asp:Label>
                                                                                <asp:CheckBox ID="chkAutumnExam" runat="server" AutoPostBack="true" OnCheckedChanged="chkAutumnExam_CheckedChanged"
                                                                                    Enabled='<%#IsAllowToEnroll(Eval("AutumnExam")) %>' Checked='<%#IsEnrolled(Eval("AutumnExam"))%>'
                                                                                    Visible='<%#IsVisible(Eval("AutumnExam"))%>' />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn DataField="ClassRoom" HeaderText="Summer Course" SortExpression="ClassRoom"
                                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                        FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="Next">
                                                                        <ItemTemplate>
                                                                            <div align="center" class='<%#SetColorCode(Eval("ClassRoom"))%>'>
                                                                                <asp:Label ID="lblClsroom" runat="server" Text='<%# Eval("ClassRoom") %>' Visible="false"></asp:Label>
                                                                                <asp:CheckBox ID="chkClassRoom" runat="server" AutoPostBack="true" OnCheckedChanged="chkClassRoom_CheckedChanged"
                                                                                    Enabled='<%#IsAllowToEnroll(Eval("ClassRoom")) %>' Checked='<%#IsEnrolled(Eval("ClassRoom"))%>'
                                                                                    Visible='<%#IsVisible(Eval("ClassRoom"))%>' />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn DataField="MockExam" HeaderText="Mock Exam" SortExpression="MockExam"
                                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                        FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="Next">
                                                                        <ItemTemplate>
                                                                            <div align="center" class='<%#SetColorCode(Eval("MockExam"))%>'>
                                                                                <asp:Label ID="lblMockExam" runat="server" Text='<%# Eval("MockExam") %>' Visible="false"></asp:Label>
                                                                                <asp:CheckBox ID="chkMockExam" runat="server" AutoPostBack="true" OnCheckedChanged="chkMockExam_CheckedChanged"
                                                                                    Enabled='<%#IsAllowToEnroll(Eval("MockExam")) %>' Checked='<%#IsEnrolled(Eval("MockExam"))%>'
                                                                                    Visible='<%#IsVisible(Eval("MockExam"))%>' />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn DataField="InterimAssessment" HeaderText="Summer Interim"
                                                                        SortExpression="InterimAssessment" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="Next">
                                                                        <ItemTemplate>
                                                                            <div align="center" class='<%#SetColorCode(Eval("InterimAssessment"))%>'>
                                                                                <asp:Label ID="lblInterimAssessment" runat="server" Text='<%# Eval("InterimAssessment") %>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:CheckBox ID="chkInterimAssessment" runat="server" AutoPostBack="true" OnCheckedChanged="chkInterimAssessment_CheckedChanged"
                                                                                    Enabled='<%#IsAllowToEnroll(Eval("InterimAssessment")) %>' Checked='<%#IsEnrolled(Eval("InterimAssessment"))%>'
                                                                                    Visible='<%#IsVisible(Eval("InterimAssessment"))%>' />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn DataField="SummerExam" HeaderText="Summer Exam" SortExpression="SummerExam"
                                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                        FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="Next">
                                                                        <ItemTemplate>
                                                                            <div align="center" class='<%#SetColorCode(Eval("SummerExam"))%>'>
                                                                                <asp:Label ID="lblSummerExam" runat="server" Text='<%# Eval("SummerExam") %>' Visible="false"></asp:Label>
                                                                                <asp:CheckBox ID="chkSummerExam" runat="server" AutoPostBack="true" OnCheckedChanged="SummerExam_CheckedChanged"
                                                                                    Enabled='<%#IsAllowToEnroll(Eval("SummerExam")) %>' Checked='<%#IsEnrolled(Eval("SummerExam"))%>'
                                                                                    Visible='<%#IsVisible(Eval("SummerExam"))%>' />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn DataField="Revision" HeaderText="Revision Course (Summer)"
                                                                        SortExpression="Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="Next">
                                                                        <ItemTemplate>
                                                                            <div align="center" class='<%#SetColorCode(Eval("Revision"))%>'>
                                                                                <asp:Label ID="lblRevision" runat="server" Text='<%# Eval("Revision") %>' Visible="false"></asp:Label>
                                                                                <asp:CheckBox ID="chkRevision" runat="server" AutoPostBack="true" OnCheckedChanged="chkRevision_CheckedChanged"
                                                                                    Enabled='<%#IsAllowToEnroll(Eval("Revision")) %>' Visible='<%#IsVisible(Eval("Revision"))%>' />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </div>
                                                    <asp:Label ID="lblEnrollmentMsg" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                             
                        </div>
                           </ContentTemplate>
                            </asp:UpdatePanel>
                    </div>
                    <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnDisplayPaymentSummey" runat="server" Text="Payment Summery" class="submitBtn" />
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <%-- <h2 class="expand" onclick="CollapseExpand('divCreditCardDetails','hdnCreditCard',this)" runat="server" id="idCreditInfoTitle" visible="false"  >
                        Credit Card Information</h2>--%>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanelPayment" runat="server" UpdateMode="Always" >
                            <ContentTemplate>
                                <div>
                                    <asp:Button ID="btnDisplayPaymentSummey" runat="server" Text="Enroll" class="submitBtn" />
                                    <telerik:RadWindow ID="radwindowSubmit" runat="server" VisibleOnPageLoad="false"
                                    Height="170px" Title="Student Enrollment" Width="350px" BackColor="#f4f3f1" VisibleStatusbar="false"
                                    Behaviors="None" ForeColor="#BDA797">
                                    <ContentTemplate>
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <b>
                                                    <asp:Label ID="lblSubmitMessage" runat="server" Text=""></asp:Label></b>
                                                <br />
                                            </div>
                                            <div class="row-div clearfix" align="center">
                                                <asp:Button ID="btnSubmitOk" runat="server" Text="Yes" class="submitBtn" Width="20%" />
                                                <asp:Button ID="btnNo" runat="server" Text="No" class="submitBtn" Width="20%" Visible="false" />
                                                <asp:Button ID="btnSuccess" runat="server" Text="Ok" class="submitBtn" Width="20%"
                                                    Visible="false" />
                                                  <%-- Begin: Added BY Pradip For MidFeb-9 2016-05-11--%>
                                                    <asp:Button ID="btnIAWarning" runat="server" Text="Ok" class="submitBtn" Width="20%"
                                                        Visible="false" />
                                                   <%-- End: Added BY Pradip For MidFeb-9 2016-05-11--%>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </telerik:RadWindow>
                                </div>
                                <div id="divCreditCardDetails" runat="server" visible="false">
                                    <asp:Label ID="lblPaymentSummery" runat="server" Text="Payment Summary" Visible="false"
                                        Font-Bold="true"></asp:Label>
                                    <asp:GridView ID="GV" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Key" HeaderText="Subject" />
                                            <asp:BoundField DataField="Value" HeaderText="Price" />
                                        </Columns>
                                    </asp:GridView>
                                    <telerik:RadGrid ID="radSummerPaymentSummery" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        AllowFilteringByColumn="false" Visible="false" AllowSorting="false" Width="150px">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Subject" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurriculumID") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblSubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Subject") %>'></asp:Label>
                                                        <asp:Label ID="lblPaymentProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblIsProductPaymentPlan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IsProductPaymentPlan") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassID") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblAlternateTimeTableOnOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AlternateTimeTable") %>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Academic Cycle ID" AllowFiltering="false"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAcademicCycleID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AcademicCycleID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Type" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Who Pay" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWhoPay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WhoPay") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Price" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaymentPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Price") %>'></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="lblTaxAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TaxAmount") %>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <div class="info-data">
                                        <div class="row-div clearfix">
                                            &nbsp;</div>
                                        <div class="row-div clearfix">
                                            <div class="label-div-left-align w40">
                                                <div class="info-data">
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w55">
                                                            <b>
                                                                <asp:Label ID="lblAmount" runat="server" Text="Total Amount:" Visible="false"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <b>
                                                                <asp:Label ID="lblTotalAmount" runat="server" Text="" Visible="false"></asp:Label></b>
                                                            <asp:Label ID="lblStagePaymentTotal" runat="server" Text="" Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w55">
                                                            <b>
                                                                <asp:Label ID="lblStudentPaidLabel" runat="server" Text="Amount To Be Paid By Student:"
                                                                    Visible="false"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <b>
                                                                <asp:Label ID="lblAmountPaidStudent" runat="server" Text="" Visible="false"></asp:Label></b>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w55">
                                                            <b>
                                                                <asp:Label ID="lblFirmPaidLabel" runat="server" Text="Amount To Be Paid By Firm:"
                                                                    Visible="false"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <b>
                                                                <asp:Label ID="lblAmountPaidFirm" runat="server" Text="" Visible="false"></asp:Label></b>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w55">
                                                            <b>
                                                                <asp:Label ID="lblTax" runat="server" Text="Tax Amount:" Visible="false"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <b>
                                                                <asp:Label ID="lblTaxAmount" runat="server" Text="" Visible="false"></asp:Label></b>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w55">
                                                            <asp:Label ID="lblIntialAmt" runat="server" Text="Intial Amount:"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <asp:Label ID="lblCurrency" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtIntialAmount" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="info-data">
                                        <div class="row-div clearfix">
                                            <div class="field-div1 w200">
                                                <b>
                                                    <uc1:CreditCard ID="CreditCard" runat="server" />
                                                </b>
                                            </div>
                                            <div class="label-div-left-align w50">
                                                <div class="info-data">
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w50">
                                                            <asp:Label ID="lblPaymentPlan" runat="server" Text="Payment Plan:" Visible="false"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w40">
                                                            <asp:DropDownList ID="ddlPaymentPlan" runat="server" AutoPostBack="true" Width="200px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w25">
                                                            &nbsp;
                                                        </div>
                                                        <div class="field-div1 w70">
                                                            <telerik:RadGrid ID="radPaymentPlanDetails" runat="server" AutoGenerateColumns="False"
                                                                AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                                AllowFilteringByColumn="false" Visible="false" AllowSorting="false" Width="200px">
                                                                <GroupingSettings CaseSensitive="false" />
                                                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn HeaderText="Days" AllowFiltering="false" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "days") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Schedule Date" AllowFiltering="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblScheduleDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ScheduleDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Percentage" AllowFiltering="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPercentage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Percentage") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Amount" AllowFiltering="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmt" runat="server" Text='<%# SetStagePaymentAmount(Eval("Percentage"),(Eval("days"))) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="row-div clearfix">
                                        <div class="label-div-left-align w800">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    <div class="label-div w800">
                                                        
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <b>
                                                            <asp:Label ID="Label4" runat="server" Text="" Visible="false"></asp:Label></b>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    </div>
                                    <p>
                                    </p>
                                </div>

                                 <div class="info-data" id="divSubmitBtnID" runat="server" visible="false">
                       <%-- <asp:UpdatePanel ID="UpdatePanelBtn" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                <div class="row-div clearfix">
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-div-left-align w80">
                                        <%--<asp:Button ID="btnSaveExit" runat="server" Text="Save and Exit" />&nbsp;--%>
                                        <asp:Button ID="btnBack" runat="server" Text="Back" Visible="false" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                                    </div>
                                </div>
                                
                           <%-- </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnDisplayPaymentSummey" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                   
                    <div id="SubmitDialog" style="height: 150px; width: 250px; background-color: #f4f3f1;
                        color: #BDA797; display: none">
                        <div class="label-div-left-align w100">
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnEnrollmentDetailsState" runat="server" Value="1" />
    <asp:HiddenField ID="hdnEnrollmentOptionsState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnExemptionsGrantedState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCreditCard" runat="server" Value="0" />
    <asp:UpdatePanel ID="updatePnlPopup" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
        
        </ContentTemplate>
    </asp:UpdatePanel>
    
</div>
<div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
