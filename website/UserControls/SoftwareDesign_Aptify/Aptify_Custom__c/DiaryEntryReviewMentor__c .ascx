<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/DiaryEntryReviewMentor__c .ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.DiaryEntryReviewMentor__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>
<script type="text/javascript">
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
    }

</script>

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
    <div id="MainTable">
        <asp:Label ID="lblMessage1" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        <asp:Panel ID="PanelStudInfo" runat="server" CssClass="cai-form">
            <h2 class="expand form-title" id="HeadStudentInfo" onclick="CollapseExpand('StudentInfo','hdnStudentInfo')">Student info</h2>
            <div id="StudentInfo" class="active">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class=" cai-form-content">
                            <div>
                                <div class="label-title">
                                    Student name :
                                </div>
                                <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                <div class="label-title">
                                    Company :
                                </div>
                                <asp:Label ID="lblCompany" runat="server"></asp:Label>
                                <div class="label-title">
                                    Business unit:
                                </div>
                                <asp:Label ID="lblBusiness" runat="server"></asp:Label>
                            </div>
                            <div>
                                <div class="label-title">
                                    Route of entry :
                                </div>
                                <asp:Label ID="lblRouteofEntry" runat="server"></asp:Label>
                                <div class="label-title">
                                    Mentor :
                                </div>
                                <asp:Label ID="lblMentorName" runat="server"></asp:Label>
                                <div class="label-title">
                                    Entry type :
                                </div>
                                <asp:Label ID="lblEntryType" runat="server"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlDetails" runat="server" CssClass="cai-form">
            <h2 class="expand form-title" id="HeadTimeFrame" onclick="CollapseExpand('TimeFrame','hdnTimeFrameState')">Time frame</h2>
            <div id="TimeFrame" class="collapse">
                <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="cai-form-content">
                            <div>
                                <asp:Label ID="Label3" runat="server" CssClass="label-title" Text="Diary entry pertains to the following timeframe" Font-Underline="true"></asp:Label>
                                <div runat="server" id="EduDetails">
                                    <span class="label-title">
                                        <span class="RequiredField">*</span>Start date:
                                    </span>
                                    <rad:RadDatePicker ID="txtStartDate" Width="300px" runat="server" AutoPostBack="true">
                                    </rad:RadDatePicker>
                                </div>
                            </div>
                            <div>
                                <div runat="server" id="Div1">
                                    <span class="label-title">
                                        <span class="RequiredField">*End date:</span>
                                    </span>
                                    <rad:RadDatePicker ID="txtEndDate" Width="300px" runat="server" AutoPostBack="true">
                                    </rad:RadDatePicker>
                                </div>
                            </div>

                            <div>
                                <asp:Label ID="Label4" runat="server" CssClass="label-title" Text="Out of office" Font-Underline="true"></asp:Label></b>
                                           
                                <div runat="server" id="Div6">
                                    <asp:Panel ID="PanelLeave" runat="server">
                                        <span class="label-title">Leave added: </span>
                                        <rad:RadGrid ID="grdLeave" CssClass="cai-table" runat="server" AutoGenerateColumns="False"
                                            AllowPaging="true" AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                                            AllowSorting="false" SortingSettings-SortedAscToolTip="Sorted Ascending" Visible="true">
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                                                <Columns>
                                                    <rad:GridTemplateColumn HeaderText="Leave type" DataField="LeaveType" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeaveType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LeaveType") %>'></asp:Label><asp:HiddenField
                                                                ID="hdnLeaveTypeID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LeaveTypeID") %>' />
                                                        </ItemTemplate>
                                                    </rad:GridTemplateColumn>
                                                    <rad:GridTemplateColumn HeaderText="Days" DataField="Days" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Days") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </rad:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </rad:RadGrid>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div>
                                <asp:Label ID="lblExperience" runat="server" CssClass="label-title" Text="Days of relevant experience within timeframe"
                                    Font-Underline="true"></asp:Label>
                                <div runat="server" id="Div7">
                                </div>


                                <span class="label-title">
                                    <span class="RequiredField">*</span>
                                    Please enter the number of days of relevant experience pertaining to this diary entry:
                                </span>
                                <asp:TextBox ID="txtExperience" Width="300px" runat="server" onkeypress="return AllowNumericOnly(event)" MaxLength="8"></asp:TextBox>
                                <%--Added/updated below field as part of #20530--%>
								<span class="label-title"> Overtime worked (whole days to be recorded):</span>
                                <asp:TextBox ID="txtToilDays" runat="server"  onkeypress="return AllowNumericOnly(event)"
                                MaxLength="8"></asp:TextBox>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlDiaryEntry" runat="server" CssClass="cai-form">
            <h2 class="expand form-title" id="HeadDiaryEntry" onclick="CollapseExpand('DiaryEntry','hdnHeadDiaryEntryState')">Diary entry</h2>
            <div id="DiaryEntry" class="collapse cai-form-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <div>
                            <asp:Label ID="lblTitle" runat="server" CssClass="label-title"><span class="RequiredField">*</span>Title:</asp:Label>

                            <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" Width="300px"></asp:TextBox>

                            <asp:Label ID="lbldes" runat="server" CssClass="label-title"><span class="RequiredField">*</span>Description:</asp:Label>

                            <asp:Label ID="lblDesc" runat="server" Visible="True"></asp:Label>

                            <asp:Label ID="lblll" runat="server" CssClass="label-title"><span class="RequiredField">*</span>Learning level:</asp:Label>

                            <asp:Label ID="lblLearningLevel" runat="server" Visible="True"></asp:Label>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlEligibility" runat="server" CssClass="cai-form">
            <h2 id="H1" class="expand form-title" onclick="CollapseExpand('Eligibility','hdnEligibilityState')">Competencies/area of experience achieved
            </h2>
            <div id="Eligibility" style="vertical-align: top;" class="collapse cai-form-content">
                <asp:UpdatePanel ID="UpdatePaneExperience" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="PanelCompetency" runat="server">
                            <div runat="server" id="Div8">
                                <div id="divcmp" runat="server">
                                    <span class="label-title">Competencies/area of experience added:</span>
                                </div>
                                <rad:RadGrid ID="gvCompetency" CssClass="cai-table" runat="server" AutoGenerateColumns="False"
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
                                                No data to display
                                            </div>
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Competency/area of experience category" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ComptencyCategory") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnCompetencyID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"ComptencyID") %>' />
                                                    <asp:HiddenField ID="hdnCID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"cID") %>' />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Competency/area of experience" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExperience" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Experience") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                        </Columns>
                                        <ItemStyle CssClass="MyGridNew" Height="20px" />
                                    </MasterTableView>
                                </rad:RadGrid>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PanelRegExp" runat="server" Visible="true">
                            <span class="label-title">Regulated experience in a practice environment : </span>
                            <asp:Panel ID="Panel2" runat="server">
                                <span class="label-title">Audit type</span>

                                <span class="label-title">Days</span>

									<div class="label-title">Statutory audit (ROI only) </div>
                                    <asp:TextBox ID="txtStatutoryDays" runat="server" MaxLength="8" onkeypress="return AllowNumericOnly(event)" Enabled="false"></asp:TextBox>

									<div class="label-title">Other audit (ROI only) </div>
                                    <asp:TextBox ID="txtOtherDays" runat="server" MaxLength="8"  onkeypress="return AllowNumericOnly(event)" Enabled="false"></asp:TextBox>

                                     <div class="label-title">Company audit (NI/UK only) </div>
                                     <asp:TextBox ID="txtCompanyDays" runat="server" MaxLength="8" onkeypress="return AllowNumericOnly(event)" Enabled="false"></asp:TextBox>

									 <div class="label-title">Other audit (NI/UK only) </div>
                                     <asp:TextBox ID="txtOtherNIDays" runat="server" MaxLength="8" onkeypress="return AllowNumericOnly(event)" Enabled="false"></asp:TextBox>
                                     <%--End of #20530--%>         
                            </asp:Panel>
                        </asp:Panel>
                        <div runat="server" id="Div9">
                            <span class="label-title">Engagement numbers:
                            </span>
                        </div>
                        <asp:TextBox ID="txtEngagmentNumber" Width="300px" runat="server" Height="50px"></asp:TextBox>
                        <asp:Label ID="lblEngagement" Font-Bold="false" runat="server"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
        <asp:Panel ID="PriorExp" runat="server" Visible="false" CssClass="cai-form">
            <h2 id="HeadDeclaration" class="expand form-title" onclick="CollapseExpand('ExemptionsGranted','hdnExemptionsGrantedState')">Prior year's experience
            </h2>
            <div id="ExemptionsGranted" style="vertical-align: top;" class="collapse">
                <asp:Panel ID="pnlCotractDeclaration" runat="server">
                    <div runat="server" id="divAttachments">
                        <span class="Error">
                            <asp:Label ID="lblErrorFile" Visible="false" runat="server">Error</asp:Label></span>
                        <uc2:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                            AllowAdd="True" AllowDelete="false" />
                    </div>
                    <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel ID="Status" runat="server">
            <div>
                <div class="label-div-left-align">
                    <b>
                        <asp:Label ID="lblsts" runat="server" Text="Status:"></asp:Label>
                        <asp:Label ID="lblStatus" runat="server" Text="With Student"></asp:Label></b>
                </div>
            </div>
        </asp:Panel>
        <div class="actions" style="text-align: right;">
            <asp:Button ID="btnCancelnBack" runat="server" Text="Cancel & Back" class="submitBtn" />
            <asp:Button ID="btnLocknApprove" runat="server" Text="Lock & Approve" class="submitBtn" />
            <asp:Button ID="btnUnlockEntry" runat="server" Text="Unlock Entry" Visible="false" class="submitBtn" />
        </div>
    </div>
    <telerik:RadWindow ID="radWindowValidation" runat="server" Width="300px" Modal="True"
        BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="CA Diary Entry" Behavior="None" Height="150px">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblValidationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
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
