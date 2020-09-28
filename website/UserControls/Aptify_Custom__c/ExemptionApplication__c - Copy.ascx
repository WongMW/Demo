<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/ExemptionApplication__c.ascx.vb"
    Inherits="ExemptionApplication__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript">
    function ClientItemSelected(sender, e) {
        document.getElementById("<%=hdnCompanyId.ClientID %>").value = e.get_value();
    }
    function ClientSelectedAwardingBody(sender, e) {
        document.getElementById("<%=hdnAwardingBody.ClientID %>").value = e.get_value(); actios
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
        var PanelState4 = $("#<%=hdnPersonalDetails.ClientID%>").val();
        if (PanelState3 == '1') {
            $('#ExemptionsGranted').removeClass("collapse").addClass("active");
        }
        if (PanelState2 == '1') {
            $('#Eligibility').removeClass("collapse").addClass("active");
        }
        if (PanelState1 == '1') {
            $('#EducationDetails').removeClass("collapse").addClass("active");
        }
        if (PanelState4 == '1') {
            $('#PersonalDetails').removeClass("collapse").addClass("active");
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
        if (HiddenPanelState == 'hdnPersonalDetails') {
            $("#<%=hdnPersonalDetails.clientID %>").val(StateValue);
        }
    }


 
</script>
<style type="text/css" >
    /*The Code Section added by Govind Mande 2016-05-26 For Redmine Bug #13694*/
    .autocomplete_completionListElement {
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
<script type="text/javascript">

    //The Code Section From here is deleted by pradip 2016-05-16 For Redmine Bug #13178
</script>

<div id="divContent" runat="server" class="content">
    <asp:HiddenField ID="hdnCompanyId" runat="server" Value="" />
    <asp:HiddenField ID="hdnAwardingBody" runat="server" Value="0" />
    <asp:HiddenField ID="hdnQualification" runat="server" Value="0" />
    <asp:HiddenField ID="hdnPersonalDetails" runat="server" Value="1" />
    <table id="MainTable" style="width: 100%; border: 0px"">
        <tr>
            <td>
                <asp:Panel ID="pnlData" runat="server">
                    <div class="info-data">
                        <div class="actions">
                            <asp:Button ID="btnNew" runat="server" CssClass="submitBtn" Text="Start New Application" />
                        </div>
                        <asp:Label ID="lblExemptionsNotFound" runat="server"></asp:Label>
                        <div class="row-div clearfix cai-table mobile-table">
                            <rad:RadGrid ID="grdData" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sorted Ascending">
                                <GroupingSettings CaseSensitive="false" />
                                <PagerStyle CssClass="sd-pager" />
                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="ID" DataField="ID" SortExpression="ID" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">ID:</span>
                                                <asp:HyperLink ID="lnkSubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'  CssClass="cai-table-data"></asp:HyperLink>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                             <ItemTemplate>
                                                <span class="mobile-label">Status:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="RouteOfEntry" HeaderText="Route Of Entry" SortExpression="RouteOfEntry"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">Route Of Entry:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "RouteOfEntry")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">Company Name:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="AcademicCycle" HeaderText="Academic Cycle" SortExpression="AcademicCycle"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                             <ItemTemplate>
                                                <span class="mobile-label">Academic Cycle:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "AcademicCycle")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="Comments" HeaderText="Comments" SortExpression="Comments"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                             <ItemTemplate>
                                                <span class="mobile-label">Comments:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Date Submitted" DataField="SubmissionDate" SortExpression="SubmissionDate"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">Date Submitted:</span>
                                                <asp:Label CssClass="cai-table-data" ID="lblSubmissionDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SubmissionDate", "{0:d}") %>'></asp:Label>
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
				&nbsp;
			  <div><span style="font-weight: bold;">Important information: </span><span>If your degree is accredited please post original transcripts of each years results to Chartered Accountants Ireland. If you are applying for exemption from CAP1 and your degree is not formally accredited please post original transcripts of each years results along with syllabi and sample examination papers for each relevant subject. In each case please quote your exemption application ID number.</span></div>
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
                    <div class="cai-form"> 
                   <h2 class="expand form-title" id="HeadPersonalDetails" onclick="CollapseExpand('PersonalDetails','hdnPersonalDetails')">
                        Personal Details</h2>
                    <div id="PersonalDetails" class="collapse cai-form-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        &nbsp;</div>
                                      <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div class="label-div w20">
                                               <asp:LinkButton ID="lnkUpdate" runat="server" >Edit</asp:LinkButton>
                                            </div>
                                            <div class="field-div1 w75">
                                              &nbsp;
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div class="label-div w20">
                                              Salutation:
                                            </div>
                                            <div class="field-div1 w75">
                                              <asp:TextBox ID="txtPreferredSalutation" runat="server" Enabled="false" ></asp:TextBox>
                                                <%--<asp:Label ID="lblSalutation" runat="server" Text="Dear"></asp:Label>--%>
                                            </div>
                                        </div>
                                    </div>
                                      <div class="row-div clearfix">
                                        <div class="label-div-left-align w29">
                                            <div class="label-div w40">
                                                Gender:
                                            </div>
                                            <div class="field-div1 w75">
                                                 <asp:DropDownList ID="cmbGender" CssClass="txtBoxEditProfileForDropdown" runat="server" Enabled="false" >
                                                    <asp:ListItem Value="0">Male</asp:ListItem>
                                                    <asp:ListItem Value="1">Female</asp:ListItem>
                                                    <asp:ListItem Value="2" Selected="True">Unknown</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w58">
                                            <div class="label-div w20">
                                                Home Address:
                                            </div>
                                            <div class="field-div1 w78">
                                                <div class="info-data">
                                                    <div class="row-div clearfix">
                                                       Line 1: <asp:TextBox ID="txtHomeAddressLine1" CssClass="txtBoxEditProfile" runat="server" Enabled="false" ></asp:TextBox>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        Line 2: <asp:TextBox ID="txtHomeAddressLine2" CssClass="txtBoxEditProfile" runat="server" Enabled="false" ></asp:TextBox>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                       Line 3: <asp:TextBox ID="txtHomeAddressLine3" CssClass="txtBoxEditProfile" runat="server" Enabled="false" ></asp:TextBox>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                      City: <asp:TextBox ID="txtHomeCity" CssClass="txtUserProfileCity" runat="server" Enabled="false" ></asp:TextBox>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                       State:  <asp:DropDownList ID="cmbHomeState" CssClass="cmbUserProfileState" runat="server" Enabled="false" >
                                                         </asp:DropDownList>
                                                    </div>
                                                     <div class="row-div clearfix">
                                                      County: <asp:TextBox ID="txtHomeCounty" CssClass="txtUserProfileCity" runat="server" Enabled="false" ></asp:TextBox>
                                                    </div>
                                                     <div class="row-div clearfix">
                                                      Postal Code: <asp:TextBox ID="txtHomeZipCode" CssClass="txtUserProfileZipCode" runat="server" Enabled="false" ></asp:TextBox>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                     Country:  <asp:DropDownList ID="cmbHomeCountry" CssClass="cmbUserProfileCountry" runat="server" Enabled="false" 
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                      <div class="row-div clearfix">
                                        <div class="label-div-left-align w29">
                                            <div class="label-div w40">
                                               (Intl Code)(Area Code) Phone:
                                            </div>
                                            <div class="field-div1 w75">
                                                  <rad:RadMaskedTextBox ID="txtIntlCode" CssClass="txtUserProfileAreaCodeSmall" runat="server"
                                                        Mask="(###)" Width="25%" Enabled="false">
                                                    </rad:RadMaskedTextBox>
                                                    <rad:RadMaskedTextBox ID="txtPhoneAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                                        runat="server" Mask="(####)" Width="25%" Enabled="false">
                                                    </rad:RadMaskedTextBox>
                                                    <rad:RadMaskedTextBox ID="txtPhone" CssClass="txtUserProfileAreaCode" runat="server"
                                                        Mask="###-####" Width="45%" Enabled="false">
                                                    </rad:RadMaskedTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                     <div class="cai-form"> 
                     <h2 class="expand form-title" id="HeadEducationRoute" onclick="CollapseExpand('Eligibility','hdnEducationRouteState')">Education Route</h2>
                 
                       <div id="Eligibility" class="collapse cai-form-content">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        &nbsp;
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div class="label-div w25">
                                                Route of Entry:
                                            </div>
                                            <div class="field-div1 w75">
                                                <asp:DropDownList ID="ddlRoute" runat="server" AutoPostBack="true" Width="99%">
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
                                                    MaxLength="100" Width="99%" AutoPostBack="true" AutoComplete="off" AutoCompleteType="Disabled"
                                                    onFocus="fnClearHidden()" />
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
                                                        <asp:TextBox ID="txtAddrsLine1" runat="server" Width="99%"></asp:TextBox>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <asp:TextBox ID="txtAddrsLine2" runat="server" Width="99%"></asp:TextBox>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <asp:TextBox ID="txtAddrsLine3" runat="server" Width="99%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w58">
                                            <div class="label-div w20">
                                                City:
                                            </div>
                                            <div class="field-div1 w78">
                                                <div class="info-data">
                                                    <div class="row-div clearfix">
                                                        <asp:TextBox ID="txtCity" runat="server" Width="99%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
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
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                   </div> 
                    <div class="cai-form"> 
                    <h2 class="expand form-title" id="HeadQualificationDetails" onclick="CollapseExpand('EducationDetails','hdnQualificationDetailsState')">Education/Qualification Details</h2>
                   
                        <div id="EducationDetails" class="collapse cai-form-content">
                        <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div runat="server" id="EduDetails" class="info-data">
                                                <div class="row-div clearfix">
                                                    &nbsp;
                                                </div>
                                                <%-- ''Commented By Pradip  2016-05-16 For Bug #13178
                                               <%-- <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Education Level:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:DropDownList ID="ddlEducationLevel" runat="server" Width="98%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                                  <%--  ''Commented By Pradip  2016-05-16 For Bug #13178--%>
                                                <div class="row-div clearfix cai-form-content">
                                                    <div class="label-div w25">
                                                        Awarding Body:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtAwardingBody" placeholder="Please start typing..." runat="server"
                                                            CssClass="textbox" AutoComplete="off" AutoCompleteType="Disabled" Width="98%" />
                                                        <Ajax:AutoCompleteExtender ID="extAwardingBody" runat="server" TargetControlID="txtAwardingBody"
                                                            BehaviorID="auto3" ServicePath="~/WebServices/GetCompanyDetails__c.asmx" EnableCaching="false"
                                                            MinimumPrefixLength="1" FirstRowSelected="false" ServiceMethod="GetAwardingBodies"
                                                            UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientSelectedAwardingBody" CompletionListCssClass="autocomplete_completionListElement" />
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix cai-form-content">
                                                    <div class="label-div w25">
                                                        Qualification:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtQualifications" runat="server" placeholder="Please start typing..."
                                                            CssClass="textbox" AutoComplete="off" AutoCompleteType="Disabled" Width="98%" />
                                                        <%--ServiceMethod="GetQualifications" chencge To ServiceMethod="GetQualificationsForEEApp" by Pradip 2016-05-16 --%>
                                                        <Ajax:AutoCompleteExtender ID="extQualifications" runat="server" TargetControlID="txtQualifications"
                                                            BehaviorID="auto2" ServicePath="~/WebServices/GetCompanyDetails__c.asmx" EnableCaching="false"
                                                            MinimumPrefixLength="1" FirstRowSelected="false" ServiceMethod="GetQualificationsForEEApp"
                                                            UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientSelectedQualification" CompletionListCssClass="autocomplete_completionListElement"/>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix cai-form-content">
                                                    <div class="label-div w25">
                                                        Year Received:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:DropDownList ID="ddlReceivedYear" runat="server" Width="98%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix  cai-form-content">
                                                    <div class="label-div w25">
                                                        Overall Grade:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtGrade" runat="server" Width="98%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix cai-form-content">
                                                    <div class="field-div1 w100">
                                                        <asp:Button ID="btnAddQualification" runat="server" Text="Add Qualification" Class="submitBtn"/><%--OnClientClick="return AddQualificationClick();" />--%>
                                                        <br />
                                                        <asp:Label ID="lblErrorQualification" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="label-div w58">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    &nbsp;
                                                </div>
                                                <div class="row-div clearfix cai-table">
                                                    <div class="label-div-left-align w100">
                                                        <asp:GridView ID="grdEducationDetails" runat="server" AutoGenerateColumns="false"
                                                            AllowPaging="true" Width="100%" GridLines="Vertical">
                                                            <PagerStyle CssClass="sd-pager" />
                                                            <Columns>
                                                                <%--  ''Commented By Pradip  2016-05-16 For Bug #13178--%>
                                                                <%--<asp:BoundField HeaderText="Education Level" DataField="EducationLevel" />--%>
                                                                <%--  ''Commented By Pradip  2016-05-16 For Bug #13178--%>
                                                                <asp:BoundField HeaderText="Awarding Body" DataField="Institute" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField HeaderText="Qualification" DataField="Degree" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField HeaderText="Grade" DataField="GPA" ItemStyle-HorizontalAlign="Center"/>
                                                                <asp:TemplateField HeaderText="Remove" ItemStyle-HorizontalAlign="Center" >
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRemove" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
						&nbsp;
                                                <div class="row-div clearfix">
                                                        &nbsp;
                                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" Class="submitBtn"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Added by Abhishek 2-3-2015--%>
                                <telerik:RadWindow ID="radWindowAddQualification" runat="server" Width="500px" Height="300px"
                                    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                                    Title="Education/Qualification Details" Behavior="None" Visible="false">
                                    <ContentTemplate>
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <b>
                                                    <asp:Label ID="lblStudentEducationDocuments" runat="server" Text=""></asp:Label></b>
                                                <br />
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
                    </div></div>

<div>

                        <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Height="180px"
                                    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                                    Title="Eligibility & Exemption Application" Behavior="None" VisibleOnPageLoad ="false">
                                    <ContentTemplate>
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <b>
                                                    <asp:Label ID="lblValidation" runat="server" Text=""></asp:Label></b>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="row-div clearfix" align="center">
                                                <asp:Button ID="btnValidationOK" runat="server" Text="Ok" Width="20%" class="submitBtn" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </telerik:RadWindow>
                    </div>

                   <div class="cai-form">  
                    <h2 id="HeadExemptionsGranted" class="expand form-title" onclick="CollapseExpand('ExemptionsGranted','hdnExemptionsGrantedState')">Exemptions Granted</h2>
                    <div id="ExemptionsGranted" class="collapse cai-form-content">
                        <div class="info-data">
                            <div class="row-div clearfix">
                                &nbsp;
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div-left-align w80">
                                    <telerik:RadGrid ID="grdGrantedExempts" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        AllowFilteringByColumn="True" Visible="false">
                                        <GroupingSettings CaseSensitive="false" />
                                        <PagerStyle CssClass="sd-pager" />
                                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
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
					&nbsp;
                                <div class="label-div-left-align w15">
                                    <asp:Button ID="btnPrint" runat="server" Text="Print Granted Exemptions" Visible="false" Class="submitBtn"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="info-data">
                        <div class="row-div clearfix">
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div-left-align w80">
                                <asp:Button ID="btnBack" runat="server" Text="Back" Class="submitBtn"/>&nbsp;
                                <asp:Button ID="btnSaveExit" runat="server" Text="Save and Exit" Class="submitBtn"/>&nbsp;
                                <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return SubmitClick();" />--%>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Class="submitBtn"/>
                            </div>
                        </div>
                    </div>
                    <div id="SubmitDialog" style="height: 150px; width: 250px; background-color: #f4f3f1; color: #BDA797; display: none">
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
        Height="250px" Title="Education/Qualification Details" Width="450px" BackColor="#f4f3f1"
        VisibleStatusbar="false" Behaviors="None" ForeColor="#BDA797">
        <ContentTemplate>
            <div class="info-data">
                <div class="row-div clearfix">
                    <b>
                        <asp:Label ID="lblSubmitMessage" runat="server" Text=""></asp:Label></b>
                    <br />
                </div>
			&nbsp;
                <div class="row-div clearfix" align="center">
                    <asp:Button ID="btnSubmitOk" runat="server" Text="OK" class="submitBtn" Width="30%" />
                    <asp:Button ID="btnNo" runat="server" Text="Cancel" class="submitBtn" Width="30%" />
                </div>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
</div>
<div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
