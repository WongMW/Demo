<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ExemptionApplication__c.ascx.vb"
    Inherits="ExemptionApplication__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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

    function SetContextKey() {
        // $find('<%=autoCompany.ClientID%>').set_contextKey($get("<%=ddlRoute.ClientID %>").value);
        document.getElementById('<%=autoCompany.ClientID%>').set_contextKey(document.getElementById("<%=ddlRoute.ClientID %>").value);
    }

    function ClientItemSelected(sender, e) {
        document.getElementById("<%=hdnCompanyId.ClientID %>").value = e.get_value();
    }
    function ClientSelectedAwardingBody(sender, e) {
        document.getElementById("<%=hdnAwardingBody.ClientID %>").value = e.get_value();
    }
    function ClientSelectedQualification(sender, e) {
        document.getElementById("<%=hdnQualification.ClientID %>").value = e.get_value();
    }
    function fnClearHidden() {
        document.getElementById("<%=hdnCompanyId.ClientID %>").value = '';

    }
    $(document).ready(function () {
        var PanelState1 = $("#<%=hdnQualificationDetailsState.ClientID%>").val();
        var PanelState2 = $("#<%=hdnEducationRouteState.ClientID%>").val();
        var PanelState3 = $("#<%=hdnExemptionsGrantedState.ClientID%>").val();

        if (PanelState3 == '1') {
            $('#ExemptionsGranted').removeClass("collapse").addClass("active");
        }
        if (PanelState2 == '1') {
            $('#Eligibility').removeClass("collapse").addClass("active");
        }
        if (PanelState1 == '1') {
            $('#EducationDetails').removeClass("collapse").addClass("active");
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
        if (HiddenPanelState == 'hdnQualificationDetailsState') {
            $("#<%=hdnQualificationDetailsState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnEducationRouteState') {
            $("#<%=hdnEducationRouteState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnExemptionsGrantedState') {
            $("#<%=hdnExemptionsGrantedState.clientID %>").val(StateValue);
        }
    }
</script>
<script type="text/javascript">



    //--><!]]>
    
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
    /*The Code Section added by Govind Mande 2016-05-26 For Redmine Bug #13694*/
    .autocomplete_completionListElement
    {
        visibility: hidden;
        margin: 0px !important;
        background-color: white;
        color: windowtext;
        border: buttonshadow;
        border-width: 1px;
        border-style: solid;
        cursor: 'default';
        overflow: scroll;
        height: 300px;
        text-align: left;
        list-style-type: none;
    }
</style>
<div id="divContent" runat="server">
    <asp:HiddenField ID="hdnCompanyId" runat="server" Value="" />
    <asp:HiddenField ID="hdnAwardingBody" runat="server" Value="0" />
    <asp:HiddenField ID="hdnQualification" runat="server" Value="0" />
    <table id="MainTable" style="width: 100%">
        <tr>
            <td>
                <asp:Panel ID="pnlData" runat="server">
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <asp:Button ID="btnNew" runat="server" Width="20%" Text="Start New Application" />
                        </div>
                        <asp:Label ID="lblExemptionsNotFound" runat="server"></asp:Label>
                        <div class="row-div clearfix">
                            <rad:RadGrid ID="grdData" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sorted Ascending">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="ID" DataField="ID" SortExpression="ID" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkSubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                        <rad:GridBoundColumn DataField="RouteOfEntry" HeaderText="Route Of Entry" SortExpression="RouteOfEntry"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                        <rad:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                        <rad:GridBoundColumn DataField="AcademicCycle" HeaderText="Academic Cycle" SortExpression="AcademicCycle"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                        <rad:GridBoundColumn DataField="Comments" HeaderText="Comments" SortExpression="Comments"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                        <rad:GridTemplateColumn HeaderText="Date Submitted" DataField="SubmissionDate" SortExpression="SubmissionDate"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                            FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubmissionDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SubmissionDate", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlDetails" runat="server">
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-div-left-align">
                                Name: <b>
                                    <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w16">
                                Student Number: <b>
                                    <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w16">
                                Academic Cycle:<b>
                                    <asp:Label ID="lblAcademicCycle" runat="server" Text=""></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w16">
                                Status: <b>
                                    <asp:Label ID="lblStatus" runat="server" Text="With Student"></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w26">
                                Comments: <b>
                                    <asp:Label ID="lblComments" runat="server" Text=""></asp:Label></b>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div-left-align w28">
                                <b>
                                    <asp:Label ID="lblRejectReason" runat="server" Text=""></asp:Label></b>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <h2 class="expand" id="HeadEducationRoute" onclick="CollapseExpand('Eligibility','hdnEducationRouteState')">
                        Education Route</h2>
                    <div id="Eligibility" class="collapse">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        &nbsp;</div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div class="label-div w25">
                                                Route of Entry:
                                            </div>
                                            <div class="field-div1 w75">
                                                <asp:DropDownList ID="ddlRoute" runat="server" AutoPostBack="true" Width="102%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="label-div-left-align w58">
                                            <b>
                                                <asp:Label ID="lblRoutesMessage" runat="server" Text=""></asp:Label></b>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div class="label-div w25">
                                                Firm:
                                            </div>
                                            <div class="field-div1 w75">
                                                <asp:TextBox ID="txtFirm" runat="server" placeholder="Please start typing..." CssClass="textbox"
                                                    MaxLength="100" Width="88%" AutoPostBack="true" AutoComplete="off" AutoCompleteType="Disabled"
                                                    onFocus="fnClearHidden()"  />
                                                <Ajax:AutoCompleteExtender ID="autoCompany" runat="server" TargetControlID="txtFirm"
                                                    BehaviorID="auto1" ServicePath="~/WebServices/GetCompanyDetails__c.asmx" EnableCaching="false"
                                                    MinimumPrefixLength="1" FirstRowSelected="false" ServiceMethod="GetCompanyDetailsForEEApp"
                                                    UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientItemSelected" />
                                            </div>
                                        </div>
                                        <div class="label-div-left-align w58">
                                            <div class="label-div w20">
                                                Firm Address:
                                            </div>
                                            <div class="field-div1 w78">
                                                <%--<asp:TextBox ID="txtFirmAddress" runat="server" Style="resize: none;" TextMode="MultiLine"
                                                    Width="80%" Height="60%"></asp:TextBox>--%>
                                                <div class="info-data">
                                                    <div class="row-div clearfix">
                                                        <asp:TextBox ID="txtAddrsLine1" runat="server" Width="80%"></asp:TextBox>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <asp:TextBox ID="txtAddrsLine2" runat="server" Width="80%"></asp:TextBox>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <asp:TextBox ID="txtAddrsLine3" runat="server" Width="80%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div class="label-div w20">
                                                &nbsp;
                                                <%--<asp:CheckBox ID="chkInfoToFirm" runat="server" Text="Display my information to my firm" />--%>
                                            </div>
                                            <div class="field-div1 w75">
                                                <asp:CheckBox ID="chkInfoToFirm" runat="server" Text="Display my information to my firm" />
                                                <%--<asp:Label runat="server" Style="vertical-align: text-top" Text="Display my information to my firm" />--%>
                                            </div>
                                        </div>
                                        <div class="label-div-left-align w58">
                                            <div class="label-div w20">
                                                City:
                                            </div>
                                            <div class="field-div1 w78">
                                                <div class="info-data">
                                                    <div class="row-div clearfix">
                                                        <asp:TextBox ID="txtCity" runat="server" Width="80%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <h2 class="expand" id="HeadQualificationDetails" onclick="CollapseExpand('EducationDetails','hdnQualificationDetailsState')">
                        Education/Qualification Details</h2>
                    <div id="EducationDetails" class="collapse">
                        <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div runat="server" id="EduDetails" class="info-data">
                                                <div class="row-div clearfix">
                                                    &nbsp;</div>
                                                <%-- ''Commented By Pradip  2016-05-16 For Bug #13178
                                                <%--<div class="row-div clearfix">
                                                    &nbsp;</div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Education Level:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:DropDownList ID="ddlEducationLevel" runat="server" Width="98%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                                <%--  ''Commented By Pradip  2016-05-16 For Bug #13178--%>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Awarding Body:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtAwardingBody" placeholder="Please start typing..." runat="server"
                                                            CssClass="textbox" AutoComplete="off" AutoCompleteType="Disabled" Width="98%"
                                                            Height="50%" />
                                                        <Ajax:AutoCompleteExtender ID="extAwardingBody" runat="server" TargetControlID="txtAwardingBody"
                                                            BehaviorID="auto3" ServicePath="~/WebServices/GetCompanyDetails__c.asmx" EnableCaching="false"
                                                            MinimumPrefixLength="1" FirstRowSelected="false" ServiceMethod="GetAwardingBodies"
                                                            UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientSelectedAwardingBody"
                                                            CompletionListCssClass="autocomplete_completionListElement" />
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Qualification:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtQualifications" runat="server" placeholder="Please start typing..."
                                                            CssClass="textbox" AutoComplete="off" AutoCompleteType="Disabled" Width="98%" />
                                                        <Ajax:AutoCompleteExtender ID="extQualifications" runat="server" TargetControlID="txtQualifications"
                                                            BehaviorID="auto2" ServicePath="~/WebServices/GetCompanyDetails__c.asmx" EnableCaching="false"
                                                            MinimumPrefixLength="1" FirstRowSelected="false" ServiceMethod="GetQualificationsForEEApp"
                                                            UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientSelectedQualification" />
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Year Received:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:DropDownList ID="ddlReceivedYear" runat="server" Width="98%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Overall Grade:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtGrade" runat="server" Width="98%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="field-div1 w100">
                                                        <asp:Button ID="btnAddQualification" runat="server" Text="Add Qualification" /><%--OnClientClick="return AddQualificationClick();" />--%>
                                                        <br />
                                                        <asp:Label ID="lblErrorQualification" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="label-div w58">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    &nbsp;</div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div-left-align w100">
                                                        <asp:GridView ID="grdEducationDetails" runat="server" AutoGenerateColumns="false"
                                                            AllowPaging="true" Width="100%" GridLines="Vertical">
                                                            <Columns>
                                                                <%--  ''Commented By Pradip  2016-05-16 For Bug #13178--%>
                                                                <%-- <asp:BoundField HeaderText="Education Level" DataField="EducationLevel" />--%>
                                                                <%--  ''Commented By Pradip  2016-05-16 For Bug #13178--%>
                                                                <asp:BoundField HeaderText="Awarding Body" DataField="Institute" />
                                                                <asp:BoundField HeaderText="Qualification" DataField="Degree" />
                                                                <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField HeaderText="Grade" DataField="GPA" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:TemplateField HeaderText="Remove" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRemove" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Added by Abhishek 2-3-2015--%>
                                <telerik:RadWindow ID="radWindowAddQualification" runat="server" Width="350px" Height="150px"
                                    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                                    Title="Education/Qualification Details" Behavior="None">
                                    <ContentTemplate>
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <b>
                                                    <asp:Label ID="lblStudentEducationDocuments" runat="server" Text=""></asp:Label></b>
                                                <br />
                                            </div>
                                            <div class="row-div clearfix" align="center">
                                                <asp:Button ID="btnOk" runat="server" Text="Ok" Width="20%" class="submitBtn" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </telerik:RadWindow>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <h2 id="HeadExemptionsGranted" class="expand" onclick="CollapseExpand('ExemptionsGranted','hdnExemptionsGrantedState')">
                        Exemptions Granted</h2>
                    <div id="ExemptionsGranted" class="collapse">
                        <div class="info-data">
                            <div class="row-div clearfix">
                                &nbsp;</div>
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
                                                <telerik:GridBoundColumn DataField="ExpirationDate" HeaderText="Expiry Date" SortExpression="ExpirationDate"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <asp:Label ID="lblGrantExemptedMsg" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="label-div-left-align w15">
                                    <asp:Button ID="btnPrint" runat="server" Text="Print Granted Exemptions" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="info-data">
                        <div class="row-div clearfix">
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div-left-align w80">
                                <asp:Button ID="btnBack" runat="server" Text="Back" />&nbsp;
                                <asp:Button ID="btnSaveExit" runat="server" Text="Save and Exit" />&nbsp;
                                <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return SubmitClick();" />--%>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                            </div>
                        </div>
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
    <asp:HiddenField ID="hdnQualificationDetailsState" runat="server" Value="1" />
    <asp:HiddenField ID="hdnEducationRouteState" runat="server" Value="1" />
    <asp:HiddenField ID="hdnExemptionsGrantedState" runat="server" Value="0" />
    <%--Added by Abhishek 2-3-2015--%>
    <telerik:RadWindow ID="radwindowSubmit" runat="server" VisibleOnPageLoad="false"
        Height="170px" Title="Education/Qualification Details" Width="350px" BackColor="#f4f3f1"
        VisibleStatusbar="false" Behaviors="None" ForeColor="#BDA797">
        <ContentTemplate>
            <div class="info-data">
                <div class="row-div clearfix">
                    <b>
                        <asp:Label ID="lblSubmitMessage" runat="server" Text=""></asp:Label></b>
                    <br />
                </div>
                <div class="row-div clearfix" align="center">
                    <asp:Button ID="btnSubmitOk" runat="server" Text="Yes" class="submitBtn" Width="20%" />
                    <asp:Button ID="btnNo" runat="server" Text="No" class="submitBtn" Width="20%" />
                </div>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
</div>
<div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
