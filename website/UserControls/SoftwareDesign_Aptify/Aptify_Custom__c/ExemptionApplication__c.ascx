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
    function ClientSelectedCity(sender, e) {
        document.getElementById("<%=hdnCity.ClientID %>").value = e.get_value();
    }
    function ClientSelectedCounty(sender, e) {
        document.getElementById("<%=hdnCounty.ClientID%>").value = e.get_value();
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
<style type="text/css">
   /*The Code Section added by Govind Mande 2016-05-26 For Redmine Bug #13694 and updated for redmine #14744*/ 
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
        height: 150px;
        text-align: left;
        list-style-type: none;
        font-size: 10pt;
        z-index:1;
    }
       
       .maroon-info{background-color:#8C1D40!important;color:#FFF!important;}
       .maroon-info:before{content: "NOTE: "!important;}
</style>

<script type="text/javascript">

    //The Code Section From here is deleted by pradip 2016-05-16 For Redmine Bug #13178
</script>
<div id="divContent" runat="server" class="content">
    <asp:HiddenField ID="hdnCompanyId" runat="server" Value="" />
    <asp:HiddenField ID="hdnAwardingBody" runat="server" Value="0" />
    <asp:HiddenField ID="hdnQualification" runat="server" Value="0" />
    <asp:HiddenField ID="hdnPersonalDetails" runat="server" Value="1" />
    <asp:HiddenField ID="hdnCity" runat="server" Value="" />
    <asp:HiddenField ID="hdnCounty" runat="server" Value="" />
    <%-- 'Added by sheela for Eligibility and Exemption Submisssion Message Change(CNM-10) --%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>Loading</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <table id="MainTable" style="width: 100%; border: 0px">
        <tr>
            <td>
                <asp:Panel ID="pnlData" runat="server">
                    <div class="info-data">
                        <div class="actions">
                            <asp:Button ID="btnNew" runat="server" CssClass="submitBtn" Text="Start new application" />
                        </div>
                        <%--<asp:Label ID="lblExemptionsNotFound" runat="server"></asp:Label>--%>
                        <div class="row-div clearfix cai-table mobile-table">
                            <asp:Label ID="lblaccessID" runat="server" Visible="false" Font-Bold="true">You can print your application form by clicking on the View button in the ID column below.</asp:Label>
                            <rad:radgrid id="grdData" runat="server" autogeneratecolumns="false" allowpaging="true"
                                allowfilteringbycolumn="true" sortingsettings-sorteddesctooltip="Sorted Descending"
                                sortingsettings-sortedasctooltip="Sorted Ascending">
                                <GroupingSettings CaseSensitive="false" />
                                <PagerStyle CssClass="sd-pager" />
                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="ID" DataField="ID" SortExpression="ID" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">ID:</span>
                                                <div class="button-block style-1">
                                                    <%--<asp:HyperLink ID="lnkSubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>' CssClass="cai-table-data"></asp:HyperLink>--%>
                                                    <asp:HyperLink ID="lnkSubject" runat="server" Text='View'
                                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>' CssClass="cai-table-data btn-full-width btn">View</asp:HyperLink>
                                                </div>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">Status:</span>
                                                <asp:Label ID="Label1" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="RouteOfEntry" HeaderText="Route of entry" SortExpression="RouteOfEntry"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">Route of entry:</span>
                                                <asp:Label ID="Label2" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "RouteOfEntry")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="CompanyName" HeaderText="Company name" SortExpression="CompanyName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">Company name:</span>
                                                <asp:Label ID="Label3" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="AcademicCycle" HeaderText="Academic cycle" SortExpression="AcademicCycle"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">Academic cycle:</span>
                                                <asp:Label ID="Label4" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "AcademicCycle")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="Comments" HeaderText="Comments" SortExpression="Comments"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">Comments:</span>
                                                <asp:Label ID="Label5" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Date submitted" DataField="SubmissionDate" SortExpression="SubmissionDate"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <span class="mobile-label">Date submitted:</span>
                                                <asp:Label CssClass="cai-table-data" ID="lblSubmissionDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SubmissionDate", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:radgrid>
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
                                Student number: <b>
                                    <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w16">
                                Academic cycle:<b>
                                    <asp:Label ID="lblAcademicCycle" runat="server" Text=""></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w16">
                                Status: <b>
                                    <asp:Label ID="lblStatus" runat="server" Text="With student"></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w26">
                                Comments: <b>
                                    <asp:Label ID="lblComments" runat="server" Text=""></asp:Label></b>
                            </div>
                            &nbsp;
                            <div>
                                <span style="font-weight: bold;">Important information: </span><span>If your degree
                                    is <span class="required">accredited</span> please post original transcripts of each years results to Chartered
                                    Accountants Ireland. If you are applying for exemption from CAP1 and your degree
                                    is not formally accredited please post original transcripts of each years results
                                    along with syllabi and sample examination papers for each relevant subject. In each
                                    case please quote your exemption application ID number.</span>
                                <div><asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                An accredited degree is a course or programme which has been offically recognised by the Institute to provide exam exemptions. 
                                This means exemptions may be awarded once a formal transcript of results is provided. No other documentation pertaining to the course or 
                                programme is required.</div>
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
                    <div class="cai-form">
                        <h2 class="expand form-title" id="HeadPersonalDetails" onclick="CollapseExpand('PersonalDetails','hdnPersonalDetails')">Personal details</h2>
                        <div id="PersonalDetails" class="collapse cai-form-content">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <div class="info-data">
                                        <div class="row-div clearfix">
                                            &nbsp;
                                        </div>
                                        <div class="row-div clearfix">
                                            <div class="label-div-left-align w40">
                                                <div class="label-div w20">
                                                    <asp:LinkButton ID="lnkUpdate" runat="server" Visible="false">PLEASE CLICK TO ADD MANDATORY INFORMATION</asp:LinkButton>
                                                </div>
                                                <div class="field-div1 w75">
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-div clearfix">
                                            <div class="label-div-left-align w40">
                                                <div class="label-div w20">
                                                    <asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    Prefix: (required)
                                                </div>
                                                <div class="field-div1 w75">
                                                    <%--<asp:TextBox ID="txtPreferredSalutation" runat="server"   ></asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPreferredSalutation"  ErrorMessage="Prefix required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                    <%--<asp:Label ID="lblSalutation" runat="server" Text="Dear"></asp:Label>--%>
                                                    <asp:DropDownList ID="cmbSalutation" CssClass="txtBoxEditProfileForDropdown" ToolTip="Changes to prefix requires supporting documentation"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ControlToValidate="cmbSalutation" ID="reqPrefix" ForeColor="Red"
                                                        ErrorMessage="Prefix required" InitialValue="" runat="server" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-div clearfix">
                                            <div class="label-div-left-align w29">
                                                <div class="label-div w40">
                                                    <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    Gender: (required)
                                                </div>
                                                <div class="field-div1 w75">
                                                    <asp:DropDownList ID="cmbGender" CssClass="txtBoxEditProfileForDropdown" runat="server">
                                                        <asp:ListItem Value="0">Male</asp:ListItem>
                                                        <asp:ListItem Value="1">Female</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">Undisclosed</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-div clearfix">
                                            <div class="label-div-left-align w58">
                                                <div class="label-div w20">
                                                    Home address:
                                                </div>
                                                <div class="field-div1 w78">
                                                    <div class="info-data">
                                                        <div class="row-div clearfix">
                                                            <asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                            Line 1: (required)
                                                            <asp:TextBox ID="txtHomeAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHomeAddressLine1"
                                                                ErrorMessage="Home address line 1 required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="row-div clearfix">
                                                            <%--<asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label> --%>
                                                            Line 2:
                                                            <asp:TextBox ID="txtHomeAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHomeAddressLine2"  ErrorMessage="Home address line 2 required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="row-div clearfix" id="divLine3" runat="Server" visible="false">
                                                            Line 3:
                                                            <asp:TextBox ID="txtHomeAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="row-div clearfix">
                                                            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                            Town/city: (required)
                                                            <asp:TextBox ID="txtHomeCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorHomeCity" runat="server" ControlToValidate="txtHomeCity" ErrorMessage="Home city required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
															 <Ajax:AutoCompleteExtender ID="aceCity" runat="server" TargetControlID="txtHomeCity"
                                                                BehaviorID="auto4" ServicePath="~/WebServices/GetCompanyDetails__c.asmx" EnableCaching="false"
                                                                MinimumPrefixLength="1" FirstRowSelected="false" ServiceMethod="GetCityList"
                                                                UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientSelectedCity" CompletionListCssClass="autocomplete_completionListElement" />
                                                        </div>
                                                        <div class="row-div clearfix" id="divState" runat="Server" visible="false">
                                                            State: 
                                                            <asp:DropDownList ID="cmbHomeState" CssClass="cmbUserProfileState" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="row-div clearfix">
                                                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                            County: (required)
                                                            <asp:TextBox ID="txtHomeCounty" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHomeCounty" ErrorMessage="Home county required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
															  <Ajax:AutoCompleteExtender ID="aceCounty" runat="server" TargetControlID="txtHomeCounty"
                                                                BehaviorID="auto5" ServicePath="~/WebServices/GetCompanyDetails__c.asmx" EnableCaching="false"
                                                                MinimumPrefixLength="1" FirstRowSelected="false" ServiceMethod="GetCountyList"
                                                                UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientSelectedCounty" CompletionListCssClass="autocomplete_completionListElement" />
                                                        </div>
                                                        <div class="row-div clearfix">
                                                            
                                                            Postal code:
                                                            <asp:TextBox ID="txtHomeZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtHomeZipCode" ErrorMessage="Home post code required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="row-div clearfix">
                                                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                            Country: (required)
                                                            <asp:DropDownList ID="cmbHomeCountry" CssClass="cmbUserProfileCountry" runat="server"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator InitialValue="Select country" ID="Req_ID" Display="Dynamic"
                                                                runat="server" ControlToValidate="cmbHomeCountry" Text="" ErrorMessage="Home country required"
                                                                CssClass="required-label" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="row-div clearfix">
                                                            <asp:Label ID="Label10" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                            Country of origin: (required)
                                                            <asp:DropDownList ID="cmbCountryofOrigin" CssClass="cmbUserProfileCountry" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator InitialValue="Select Country" ID="RequiredFieldValidator7"
                                                                Display="Dynamic" runat="server" ControlToValidate="cmbCountryofOrigin" Text=""
                                                                ErrorMessage="Country of origin required" CssClass="required-label" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-div clearfix">
                                            <div class="label-div-left-align w29">
                                                <div class="label-div w40">
                                                    <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    Mobile telephone number : (required)
                                                </div>
                                                <div class="field-div1 w75">
                                                    <span style="width: 25%; display: inline-block;">(Country code)</span> <span style="width: 25%;
                                                        display: inline-block;">(Mobile prefix/area code)</span> <span style="width: 48.666%;
                                                            display: inline-block;">(Number)</span>
                                                    <rad:radmaskedtextbox id="txtIntlCode" cssclass="txtUserProfileAreaCodeSmall" runat="server"
                                                        mask="(###)" width="25%">
                                                    </rad:radmaskedtextbox>
                                                    <rad:radmaskedtextbox id="txtPhoneAreaCode" cssclass="txtUserProfileAreaCodeSmall"
                                                        runat="server" mask="(####)" width="25%">
                                                    </rad:radmaskedtextbox>
                                                    <rad:radmaskedtextbox id="txtPhone" cssclass="txtUserProfileAreaCode" runat="server"
                                                        mask="###-#####" width="48.666%">
                                                    </rad:radmaskedtextbox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone"
                                                        ErrorMessage="Mobile telephone number required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="cai-form">
                        <h2 class="expand form-title" id="HeadEducationRoute" onclick="CollapseExpand('Eligibility','hdnEducationRouteState')">
                            Education route</h2>
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
                                                    Route of entry:
                                                </div>
                                                <div class="field-div1 w75">
                                                    <asp:DropDownList ID="ddlRoute" runat="server" AutoPostBack="true" Width="99%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="label-div-left-align w58 hide-from-flexible">
                                                <b>
                                                    <asp:Label ID="lblRoutesMessage" runat="server" Text=""></asp:Label></b>
                                            </div>
                                        </div>
                                        <div class="row-div clearfix hide-from-flexible">
                                            <div class="label-div-left-align w40 ">
                                                <div class="label-div w25">
                                                    Firm:
                                                </div>
                                                <div class="field-div1 w75">
                                                    <asp:Label ID="lblFirmText" runat="server" Text="" Visible="false"></asp:Label><br />
                                                    <%--updated placeholder text for Redmine #20675--%>
                                                    <asp:TextBox ID="txtFirm" runat="server" placeholder="Type 3 letters and select from dropdown" CssClass="textbox"
                                                        MaxLength="3" Width="99%" AutoPostBack="true" AutoComplete="off" AutoCompleteType="Disabled"
                                                        onFocus="fnClearHidden()" />
                                                      <Ajax:AutoCompleteExtender ID="autoCompany" runat="server" TargetControlID="txtFirm" CompletionListElementID="divwidth" 
                                                        BehaviorID="auto1" ServicePath="~/WebServices/GetCompanyDetails__c.asmx" EnableCaching="false"
                                                        MinimumPrefixLength="2" FirstRowSelected="false" ServiceMethod="GetCompanyDetailsForEEApp"
                                                        UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientItemSelected" CompletionListCssClass="autocomplete_completionListElement" />

                                                </div>
                                            </div>
                                            <div class="label-div-left-align w58">
                                                <div class="label-div w20">
                                                    Firm address:
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
                                            <div class="label-div-left-align w58 hide-from-flexible">
                                                <div class="label-div w20">
                                                    Town/city:
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
                        <h2 class="expand form-title" id="HeadQualificationDetails" onclick="CollapseExpand('EducationDetails','hdnQualificationDetailsState')">
                            Education/qualification details</h2>
                        <div id="EducationDetails" class="collapse cai-form-content">
                            <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="info-data">
                                        <div class="row-div clearfix">
                                            <div class="label-div-left-align w40">
                                                <div runat="server" id="EduDetails" class="info-data">
                                                    <div class="row-div clearfix">
                                                        <asp:Label ID="lblErrorQualification" runat="server" Text="" Style="font-weight: bold;"
                                                            ForeColor="Red"></asp:Label>
                                                        <br />
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
                                                            College/university:<br />
                                                            Please select the relevant awarding body from the list that appears once you commence
                                                            typing the awarding body name. If it does not appear please choose 'Not listed'.
                                                        </div>
                                                        <div class="field-div1 w75">
                                                        <%--updated placeholder text for Redmine #20675--%>
                                                            <asp:TextBox ID="txtAwardingBody" placeholder="Type 3 letters and select from dropdown" runat="server"
                                                                CssClass="textbox" AutoComplete="off" AutoCompleteType="Disabled" Width="98%"
                                                                MaxLength="3" />
                                                            <ajax:autocompleteextender id="extAwardingBody" runat="server" targetcontrolid="txtAwardingBody"
                                                                behaviorid="auto3" servicepath="~/WebServices/GetCompanyDetails__c.asmx" enablecaching="false"
                                                                minimumprefixlength="2" firstrowselected="false" servicemethod="GetAwardingBodies"
                                                                usecontextkey="true" completionsetcount="10" onclientitemselected="ClientSelectedAwardingBody"
                                                                completionlistcssclass="autocomplete_completionListElement" />
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix cai-form-content">
                                                        <div class="label-div w25">
                                                            Qualification:<br />
                                                            Please select the relevant qualification from the list that appears once you commence
                                                            typing the qualification name. If it does not appear please choose 'Not listed'.
                                                        </div>
                                                        <div class="field-div1 w75">
                                                        <%--updated placeholder text for Redmine #20675--%>
                                                            <asp:TextBox ID="txtQualifications" runat="server" placeholder="Type 3 letters and select from dropdown"
                                                                CssClass="textbox" AutoComplete="off" AutoCompleteType="Disabled" Width="98%"
                                                                MaxLength="3" />
                                                            <%--ServiceMethod="GetQualifications" chencge To ServiceMethod="GetQualificationsForEEApp" by Pradip 2016-05-16 --%>
                                                            <ajax:autocompleteextender id="extQualifications" runat="server" targetcontrolid="txtQualifications"
                                                                behaviorid="auto2" servicepath="~/WebServices/GetCompanyDetails__c.asmx" enablecaching="false"
                                                                minimumprefixlength="2" firstrowselected="false" servicemethod="GetQualificationsForEEApp"
                                                                usecontextkey="true" completionsetcount="10" onclientitemselected="ClientSelectedQualification"
                                                                completionlistcssclass="autocomplete_completionListElement" />
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix cai-form-content">
                                                        <div class="label-div w25">
                                                            Year received:
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <asp:DropDownList ID="ddlReceivedYear" runat="server" Width="98%">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix  cai-form-content">
                                                        <div class="label-div w25">
                                                            Overall grade:
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <asp:TextBox ID="txtGrade" runat="server" Width="98%"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix cai-form-content">
                                                        <div class="field-div1 w100">
                                                            <asp:Button ID="btnAddQualification" runat="server" CausesValidation="false" Text="Add qualification"
                                                                Class="submitBtn" /><%--OnClientClick="return AddQualificationClick();" />--%>
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
                                                                    <asp:BoundField HeaderText="Awarding body" DataField="Institute" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="Qualification" DataField="Degree" ItemStyle-HorizontalAlign="Center" />
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
                                                    &nbsp;
                                                <div class="row-div clearfix">
                                                    &nbsp;
                                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" Class="submitBtn" />
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
                        </div>
                    </div>
                    <%--Commented by sheela for Eligibility and Exemption Submisssion Message Change(CNM-10)--%>
                    <%-- <div>

                        <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Height="200px"
                            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                            Title="Eligibility & exemption application" Behavior="None" VisibleOnPageLoad="false">
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
                    </div>--%>
                    <%-- Modified by Sheela as part of CNM-9:Exemption Section display logic --%>
                    <div class="cai-form" id="dvExceptionGranted" runat="server">
                        <h2 id="HeadExemptionsGranted" class="expand form-title" onclick="CollapseExpand('ExemptionsGranted','hdnExemptionsGrantedState')">
                            Exemptions granted</h2>
                        <div id="ExemptionsGranted" class="cai-form-content active">
                            <div class="info-data">
                                <div class="row-div clearfix">
                                   <b> Exemptions granted</b>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-div-left-align w80 cai-table">
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
                                                    <telerik:GridBoundColumn DataField="ExpirationDate" HeaderText="Expiry date" SortExpression="ExpirationDate"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                        <asp:Label ID="lblGrantExemptedMsg" runat="server" Text=""></asp:Label>
                                          <%--Added by Deepika on 09/11/2017 for Bug #16901--%>  
                                        </br>
                                       
                                       <%-- End Here--%>
                                    </div>
                                    &nbsp;
                                <div class="label-div-left-align w15">
                                </div>
                                </div>
                                 <div class="row-div clearfix">
                                <b>Passed as external</b></div>
                            <div class="row-div clearfix">
                                <div class="label-div-left-align w80 cai-table">
                                    <telerik:RadGrid ID="grdExternalPassed" runat="server" AutoGenerateColumns="False"
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
                                                <telerik:GridDateTimeColumn DataField="ExpirationDate" HeaderText="Expiry Date" SortExpression="ExpirationDate"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                    FilterControlWidth="60%" EnableTimeIndependentFiltering="true">
                                                </telerik:GridDateTimeColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:radgrid>
                                        <asp:Label ID="lblExternalPassedMsg" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <asp:Label ID="lblExternalCertificateMsg" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <%--Modified by sheela for Eligibility and Exemption Submisssion Message Change(CNM-10)--%>
                    <asp:UpdatePanel ID="UpdatePanelbtn" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div>
                                <telerik:radwindow id="radWindowValidation" runat="server" width="350px" height="200px"
                                    modal="True" backcolor="#f4f3f1" visiblestatusbar="False" behaviors="None" forecolor="#BDA797"
                                    title="Eligibility & exemption application" behavior="None" visibleonpageload="false">
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
                        </telerik:radwindow>
                            </div>
                            <%--End div by Govind M #14335--%>
                            <div class="info-data">
                                <div class="row-div clearfix">
                                </div>
                                <div class="row-div clearfix">
                                    <div class="row-div clearfix">
                                        <asp:Label ID="lblPopups2" runat="server" Font-Underline="true"><a href="/Prospective-Students/browser-popups" target="_blank">Please disable pop-up blockers for charteredaccountants.ie before submitting your application</a></asp:Label><br />
                                    </div>
                                    <div>
                                        &nbsp;</div>
                                    <div class="label-div-left-align w80">
                                        <asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="Back" Class="submitBtn" />&nbsp;
                                        <asp:Button ID="btnSaveExit" runat="server" Text="Save and Exit" Class="submitBtn"
                                            OnClientClick="Validate();" Visible="false" />&nbsp;
                                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return SubmitClick();" />--%>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit Application" Class="submitBtn"
                                            OnClientClick="Validate();" />
                                        <asp:Label ID="lblSubmitWarning" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        <asp:Button ID="btnPrintForm" runat="server" CausesValidation="false" Text="Print"
                                            Class="submitBtn" /><asp:Label ID="lblPopups" runat="server"><a href="/Prospective-Students/browser-popups" target="_blank">Form not printing?</a></asp:Label>
                                        <div>
                                         <%-- below code commented by GM for Redmine #19800 and added below line--%>
                                   <%-- <asp:Button ID="btnPrint" CausesValidation="false" runat="server" Text="Print Granted Exemptions" Visible="false" Class="submitBtn" Style="display: none;" /></div>--%>
								    &nbsp;
									</br>
                                    <asp:Button ID="btnPrint" CausesValidation="false" runat="server" Text="Print Granted Exemptions" Visible="false" Class="submitBtn"   />
                                                 <%-- End Redmine #19800%>--%>
                                                </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
        Height="310px" Title="Confirmation message" Width="450px" BackColor="#f4f3f1"
        VisibleStatusbar="false" Behaviors="None" ForeColor="#BDA797" style="background-color: #f4f3f1">
        <ContentTemplate>
            <div class="info-data">
                <div class="row-div clearfix">
						<b><asp:Label ID="Label6" runat="server">Please note the following before you continue</asp:Label></b><br />
                    
                        <asp:Label ID="lblSubmitMessage" runat="server" Text=""></asp:Label>
										&nbsp;
						<div class="row-div clearfix"><br />
                            <%--Siddharth: Repalced label ID as it was duplicate--%>
							<asp:Label ID="Label12" runat="server" Font-Bold="true">Once you click Confirm the printable version of your application form should open. Please print, sign and send this to the Institute.</asp:Label>
						</div>

                </div>
                <div class="row-div clearfix" align="center"><br />

                     <%-- Added by Deepika on 06/12/2017 for log #17563 --%>
                     <%-- To allow  either of any button to click --%>
                    <asp:Button ID="btnSubmitOk" runat="server" Text="confirm" class="submitBtn" Width="30%" CausesValidation ="false"/>
                    <asp:Button ID="btnNo" runat="server" Text="back" class="submitBtn" Width="30%"  CausesValidation ="false"/>
                     <%--  End here--%>
                </div>

            </div>
        </ContentTemplate>
    </telerik:RadWindow>
</div>
<div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
<script>
    function pageLoad() {
        // Susan Wong, ticket #20317 Fix script error when ddl is not seen
        if (!$('#baseTemplatePlaceholder_content_ExemptionApplication__c_ddlRoute').length == 0) {
            var selectRoute = document.getElementById("baseTemplatePlaceholder_content_ExemptionApplication__c_ddlRoute");
            var selectedVal = selectRoute.options[selectRoute.selectedIndex].value;
            if (selectedVal == "2|ele") {
                $('.hide-from-flexible').css('display', 'none');
            }
        }
    };
</script>
