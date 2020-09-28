<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/StudentEnrollment__c.ascx.vb"
    Inherits="StudentEnrollment__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>

<script type="text/javascript">
    function ClientItemSelected(sender, e) {
        document.getElementById("<%=hdnCompanyId.ClientID %>").value = e.get_value();
    }
    function fnClearHidden() {
        document.getElementById("<%=hdnCompanyId.ClientID %>").value = '';
    }

    function CollapseExpand(me, HiddenPanelState) {
        var Panelstate = $('#' + me).attr("class");
        $('#' + me).slideToggle('slow');
    }
</script>

<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>Loading</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<div id="divContent" runat="server">
    <asp:HiddenField ID="hdnCompanyId" runat="server" Value="" />
    <asp:HiddenField ID="hdnAwardingBody" runat="server" Value="0" />
    <asp:HiddenField ID="hdnQualification" runat="server" Value="0" />
    <div id="MainTable" class="cai-form">

        <asp:Panel ID="pnlDetails" runat="server">
            <div>
                  <div class="field-group">
                                 <asp:Button ID="btnPrint" visible="false"  runat="server" Text="PRINT INVOICE" class="submitBtn" />
                            </div> 
                <div class="field-group">
                    <span class="label-title-inline">Name: </span>
                    <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label>
                </div>
                <div class="field-group">
                    <span class="label-title-inline">Student number: </span>
                    <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label>
                </div>
                <div class="field-group">
                    <span class="label-title-inline">Status: </span>
                    <asp:Label ID="lblStatus" runat="server" Text="With student"></asp:Label>
                </div>
                <div class="field-group">
                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <h2 class="active form-title" id="HeadEnrollmentDetails" onclick="CollapseExpand('EnrollmentDetails','hdnEnrollmentDetailsState')">Enrolment details</h2>
            <div id="EnrollmentDetails" class="active">
                <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="cai-form-content">
                            <div>
                                <span class="label-title">Route of entry:</span>
                                <asp:DropDownList ID="ddlRoute" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="hide-from-flexible">
                                <span class="label-title">Firm/organization:</span>
                                <asp:TextBox ID="txtFirm" runat="server" placeholder="Please start typing..." CssClass="textbox"
                                    MaxLength="100" AutoPostBack="true" AutoComplete="off" AutoCompleteType="Disabled" />
                                <Ajax:AutoCompleteExtender ID="autoCompany" runat="server" TargetControlID="txtFirm"
                                    BehaviorID="auto1" ServicePath="~/WebServices/GetCompanyDetails__c.asmx" EnableCaching="false"
                                    MinimumPrefixLength="1" FirstRowSelected="false" ServiceMethod="GetCompanyDetailsForStudEnroll"
                                    UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientItemSelected" />
                            </div>
                            <div class="hide-from-flexible">
                                <span class="label-title">Address line 1:</span>
                                <asp:TextBox ID="txtAddrLine1" runat="server" CssClass="textbox" AutoComplete="off"
                                    AutoCompleteType="Disabled" Enabled="false" />
                            </div>
                            <div class="hide-from-flexible">
                                <span class="label-title">Address line 2:</span>

                                <asp:TextBox ID="txtAddrLine2" runat="server" CssClass="textbox" AutoComplete="off"
                                    AutoCompleteType="Disabled" Enabled="false" />
                            </div>
                            <div class="hide-from-flexible">
                                <span class="label-title">Postal code:</span>

                                <asp:TextBox ID="txtPostalCode" runat="server" CssClass="textbox" AutoComplete="off"
                                    AutoCompleteType="Disabled" Enabled="false" />
                            </div>

                            <div class="hide-from-flexible">
                                <asp:CheckBox ID="chkInfoToFirm" runat="server" Text="" AutoPostBack="true" />
                                <asp:Label ID="Label2" runat="server" Style="vertical-align: middle" Text="Please tick to share your information with the firm" />
                            </div>

                            <div>
                                <asp:Label ID="lblEEMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%-- Added by Sheela as part of CNM-9:Exemption Section display logic --%>
            <div id="dvExceptionGranted" runat="server">
            <h2 id="HeadExemptionsGranted" class="expand form-title" onclick="CollapseExpand('ExemptionsGranted','hdnExemptionsGrantedState')">Exemptions granted</h2>
            <div id="ExemptionsGranted" class="expand cai-form-content">
                <span>Exemptions granted</span>
                <div>
                    <telerik:RadGrid ID="grdGrantedExempts" runat="server" AutoGenerateColumns="False"
                        AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                        AllowFilteringByColumn="True" Visible="false" CssClass="cai-table mobile-table">
                        <PagerStyle CssClass="sd-pager" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                            <Columns>
                                <rad:GridTemplateColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <span class="mobile-label">Curriculum:</span>
                                        <asp:Label ID="Label1" runat="server" CssClass="cai-table-data" Text='<%# Eval("CurriculumName") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="CourseName" HeaderText="Course" SortExpression="CourseName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <span class="mobile-label">Course:</span>
                                        <asp:Label ID="Label2" runat="server" CssClass="cai-table-data" Text='<%# Eval("CourseName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ExpirationDate" HeaderText="Expiry date" SortExpression="ExpirationDate"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <span class="mobile-label">Expiry date:</span>
                                        <asp:Label ID="Label3" runat="server" CssClass="cai-table-data" Text='<%# Eval("ExpirationDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:Label ID="lblGrantExemptedMsg" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    Passed as external
                </div>
                <div>
                    <telerik:RadGrid ID="grdExternalPassed" runat="server" AutoGenerateColumns="False"
                        AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                        AllowFilteringByColumn="true" Visible="false" CssClass="cai-table">
                        <PagerStyle CssClass="sd-pager" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="CurriculumName" HeaderText="Curriculum" SortExpression="CurriculumName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="30%"/>
                                <telerik:GridBoundColumn DataField="CourseName" HeaderText="Course" SortExpression="CourseName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="40%"/>
                                <telerik:GridDateTimeColumn DataField="ExpirationDate" HeaderText="Expiry date" HeaderStyle-Width="30%" SortExpression="ExpirationDate"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                    EnableTimeIndependentFiltering="true">
                                </telerik:GridDateTimeColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:Label ID="lblExternalPassedMsg" runat="server" Text=""></asp:Label>
                </div>
            </div>
            </div>
            <h2 class="expand form-title" id="HeadEnrollmentOptions" onclick="CollapseExpand('EnrollmentOptions','hdnEnrollmentOptionsState')" runat="server">Enrolment options</h2>
            <div id="EnrollmentOptions" class="expand">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div id="idEnroleddiv" runat="server">
                            <div runat="server" id="Div1">
                             <div   runat="server" id="dvEnrolmentOptions" visible="false">
                                <div class="field-group">
                                    <div class="available">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false"  />
                                    </div>
                                    <asp:Label ID="lblAvailable" runat="server"  ></asp:Label>
                                </div>

                                <div class="field-group">
                                    <div class="not_available">
                                    </div>
                                    <asp:Label ID="lblNotAvailable" runat="server"  ></asp:Label>
                                </div>

                                <div class="field-group">
                                    <div class="available">
                                        <asp:CheckBox ID="chkRequestEnrolled" runat="server" Enabled="false" Checked="true" />
                                    </div>

                                    <asp:Label ID="lblEnrollmentExists" runat="server"
                                         ></asp:Label>
                                </div>

                                <div class="field-group">
                                    <div class="already_enrolled">
                                    </div>
                                    <asp:Label ID="lblAlreadyEnrolled" runat="server"
                                        ></asp:Label>
                                </div>
                                </div>
                                <div class="field-group">
                                     <asp:Label ID="lblStdgrp" runat="server" Text=""></asp:Label><br />
                                    <asp:Label ID="lblStudentGroup" runat="server" Text="Student group:"></asp:Label>

                                    <asp:DropDownList ID="ddlCAP1StudGrp" runat="server" AutoPostBack="true">
                                    </asp:DropDownList>

                                                       <asp:RequiredFieldValidator InitialValue="Select student group" ID="Req_ID" Display="Dynamic"
                                                                    runat="server" ControlToValidate="ddlCAP1StudGrp" Text="" ErrorMessage="Student group required"
                                                                    CssClass="error-message"></asp:RequiredFieldValidator>
                                </div>
 <div class="field-group">
 <asp:Label ID="lblMsgWarning" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblCentrallyMngError" runat="server" Text="" Visible="false"></asp:Label>
</div>

                                <div class="field-group">
                                    <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
                                    <telerik:RadGrid ID="gvCurriculumCourse" runat="server" PageSize="10" AllowSorting="True"
                                        AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                        ShowGroupPanel="True" GridLines="none" AutoGenerateColumns="false">
                                        <PagerStyle CssClass="sd-pager" />
					<ItemStyle VerticalAlign="Top" />
                                        <MasterTableView Width="100%" DataKeyNames="CurriculumID,SubjectID,IsCourseJurisdiction,IsFAEElective,Isfailed,FailedUnits,FirstAttempt">
                                            <NoRecordsTemplate>
                                                No record(s)
                                            </NoRecordsTemplate>
                                            <ColumnGroups>
                                                <rad:GridColumnGroup HeaderText="Autumn" Name="Current" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top">
                                                </rad:GridColumnGroup>
                                                <rad:GridColumnGroup HeaderText="Summer" Name="Next" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top">
                                                </rad:GridColumnGroup>
                                            </ColumnGroups>
                                            <Columns>
                                                <telerik:GridTemplateColumn DataField="IsFAEElective" UniqueName="IsFAEElective"
                                                    HeaderText="FAE elective" SortExpression="IsFAEElective" HeaderStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <div>
                                                            <asp:CheckBox ID="chkIsFAEElective" runat="server" CssClass="chkBox" Enabled='<%#IIf(Eval("IsFAEElective")=1,true,false) %>'
                                                                OnCheckedChanged="chkIsFAEElective_CheckedChanged" AutoPostBack="true" />
                                                            <asp:Label ID="lblIsFAEElective" runat="server" Text='<%# Eval("IsFAEElective") %>'
                                                                Visible="false"></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom"/>
                                                <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject" SortExpression="Subject"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom"/>
                                                <telerik:GridBoundColumn DataField="SubjectID" HeaderText="SubjectID" SortExpression="SubjectID"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    Visible="false" />
                                                <telerik:GridBoundColumn DataField="AlternativeGroupID" HeaderText="Alternate timetable"
                                                    SortExpression="AlternativeGroupID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" HeaderStyle-Width="8%" ItemStyle-Width="8%"
                                                    Visible="false"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top"/>
                                                <telerik:GridBoundColumn DataField="AlternativeGroup" HeaderText="Alternate timetable"
                                                    SortExpression="AlternativeGroup" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom"/>
                                                <telerik:GridTemplateColumn DataField="RepeatRevision" HeaderText="Revision course"
                                                    SortExpression="RepeatRevision" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false"
                                                    ColumnGroupName="Current">
                                                    <ItemTemplate>
                                                        <div class='<%#SetColorCode(Eval("RepeatRevision"))%>' id="idivAutumnRevision" runat="server">
                                                            <asp:Label ID="lblIsCourseJurisdiction" runat="server" Text='<%# Eval("IsCourseJurisdiction") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="lblCurriculumID" runat="server" Text='<%# Eval("CurriculumID") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="lblCutOffUnits" runat="server" Text='<%# Eval("CutOffUnits") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblIsCore__c" runat="server" Text='<%# Eval("IsCore__c") %>' Visible="false"></asp:Label>
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
                                                            <asp:Label ID="lblIsExternal" runat="server" Text='<%# Eval("IsExternal") %>'
                                                                Visible="false"></asp:Label>
                                                             <asp:Label ID="lblSubjectID" runat="server" Text='<%# Eval("SubjectID")%>'
                                                                Visible="false"></asp:Label>
                                                                 <%--Code adde by GM for Redmine #20032--%>
															 <asp:Label ID="lblIsFailed" runat="server" Text='<%# Eval("Isfailed")%>'
                                                                Visible="false"></asp:Label>
															 <%--END Redmine #20032--%>
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ResitInterimAssessment" HeaderText="Resit interim"
                                                    SortExpression="ResitInterimAssessment" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false"
                                                    ColumnGroupName="Current">
                                                    <ItemTemplate>
                                                        <div class='<%#SetColorCode(Eval("ResitInterimAssessment"))%>' id="idivRIA" runat="server">
                                                            <asp:Label ID="lblResitInterimAssessment" runat="server" Text='<%# Eval("ResitInterimAssessment") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkResitInterimAssessment" runat="server" AutoPostBack="true" OnCheckedChanged="chkResitInterimAssessment_CheckedChanged"
                                                                Enabled='<%#IsAllowToEnroll(Eval("ResitInterimAssessment")) %>' Checked='<%#IsEnrolled(Eval("ResitInterimAssessment"))%>'
                                                                Visible='<%#IsVisible(Eval("ResitInterimAssessment"))%>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="AutumnExam" HeaderText="Autumn exam" SortExpression="AutumnExam"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    ColumnGroupName="Current">
                                                    <ItemTemplate>
                                                        <div class='<%#SetColorCode(Eval("AutumnExam"))%>' id="idivAutumnExam" runat="server">
                                                            <asp:Label ID="lblAutumnExam" runat="server" Text='<%# Eval("AutumnExam") %>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkAutumnExam" runat="server" AutoPostBack="true" OnCheckedChanged="chkAutumnExam_CheckedChanged"
                                                                Enabled='<%#IsAllowToEnroll(Eval("AutumnExam")) %>' Checked='<%#IsEnrolled(Eval("AutumnExam"))%>'
                                                                Visible='<%#IsVisible(Eval("AutumnExam"))%>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ClassRoom" HeaderText="Summer course" SortExpression="ClassRoom"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    ColumnGroupName="Next">
                                                    <ItemTemplate>
                                                        <div class='<%#SetColorCode(Eval("ClassRoom"))%>' id="idivClassRoom" runat="server">
                                                            <asp:Label ID="lblClsroom" runat="server" Text='<%# Eval("ClassRoom") %>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkClassRoom" runat="server" AutoPostBack="true" OnCheckedChanged="chkClassRoom_CheckedChanged"
                                                                Enabled='<%#IsAllowToEnroll(Eval("ClassRoom")) %>' Checked='<%#IsEnrolled(Eval("ClassRoom"))%>'
                                                                Visible='<%#IsVisible(Eval("ClassRoom"))%>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="MockExam" HeaderText="Mock exam" SortExpression="MockExam"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    ColumnGroupName="Next">
                                                    <ItemTemplate>
                                                        <div class='<%#SetColorCode(Eval("MockExam"))%>' id="idivMock" runat="server">
                                                            <asp:Label ID="lblMockExam" runat="server" Text='<%# Eval("MockExam") %>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkMockExam" runat="server" AutoPostBack="true" OnCheckedChanged="chkMockExam_CheckedChanged"
                                                                Enabled='<%#IsAllowToEnroll(Eval("MockExam")) %>' Checked='<%#IsEnrolled(Eval("MockExam"))%>'
                                                                Visible='<%#IsVisible(Eval("MockExam"))%>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="InterimAssessment" HeaderText="Summer interim"
                                                    SortExpression="InterimAssessment" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false"
                                                    ColumnGroupName="Next">
                                                    <ItemTemplate>
                                                        <div class='<%#SetColorCode(Eval("InterimAssessment"))%>' id="idivIA" runat="server">
                                                            <asp:Label ID="lblInterimAssessment" runat="server" Text='<%# Eval("InterimAssessment") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkInterimAssessment" runat="server" AutoPostBack="true" OnCheckedChanged="chkInterimAssessment_CheckedChanged"
                                                                Enabled='<%#IsAllowToEnroll(Eval("InterimAssessment")) %>' Checked='<%#IsEnrolled(Eval("InterimAssessment"))%>'
                                                                Visible='<%#IsVisible(Eval("InterimAssessment"))%>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="SummerExam" HeaderText="Summer exam" SortExpression="SummerExam"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    ColumnGroupName="Next">
                                                    <ItemTemplate>
                                                        <div class='<%#SetColorCode(Eval("SummerExam"))%>' id="idivSummerExam" runat="server">
                                                            <asp:Label ID="lblSummerExam" runat="server" Text='<%# Eval("SummerExam") %>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkSummerExam" runat="server" AutoPostBack="true" OnCheckedChanged="SummerExam_CheckedChanged"
                                                                Enabled='<%#IsAllowToEnroll(Eval("SummerExam")) %>' Checked='<%#IsEnrolled(Eval("SummerExam"))%>'
                                                                Visible='<%#IsVisible(Eval("SummerExam"))%>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Revision" HeaderText="Revision course (summer)"
                                                    SortExpression="Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false"
                                                    ColumnGroupName="Next">
                                                    <ItemTemplate>
                                                        <div class='<%#SetColorCode(Eval("Revision"))%>' id="idivSummerRevision" runat="server">
                                                            <asp:Label ID="lblRevision" runat="server" Text='<%# Eval("Revision") %>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkRevision" runat="server" AutoPostBack="true" OnCheckedChanged="chkRevision_CheckedChanged"
                                                                Enabled='<%#IsAllowToEnroll(Eval("Revision")) %>' Visible='<%#IsVisible(Eval("Revision"))%>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <asp:Label ID="lblEnrollmentMsg" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div>
                <asp:UpdatePanel ID="UpdatePanelPayment" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="actions field-group">
                            <asp:Label runat="server" ID="lblEnrolmsg" CssClass="label-title">Clicking on enrol means you accept the <a href="/Current-Student/Student-Centre/Enrolment-Requirements" target="_blank">terms and conditions</a> of enrolment.</asp:Label>
                            <asp:Button ID="btnDisplayPaymentSummey" runat="server" Text="Enrol" class="submitBtn" />
                        </div>
                        <telerik:RadWindow ID="radwindowSubmit" runat="server" VisibleOnPageLoad="false"
                            Height="180px" Title="Student enrolment" Width="400px" BackColor="#f4f3f1" VisibleStatusbar="false"
                            Behaviors="None" ForeColor="#BDA797">
                            <ContentTemplate>
                                <div>
                                    <div>

                                        <asp:Label ID="lblSubmitMessage" runat="server" Text=""></asp:Label>
                                        <br />
					&nbsp;
                                    </div>
                                    <div align="center">
                                        <asp:Button ID="btnSubmitOk" runat="server" Text="Yes" class="submitBtn" Width="20%" CausesValidation="false"/>
                                        <asp:Button ID="btnNo" runat="server" Text="No" class="submitBtn" Width="20%" Visible="false" CausesValidation="false"/>
                                        <asp:Button ID="btnSuccess" runat="server" Text="Ok" class="submitBtn" Width="20%"
                                            Visible="false" CausesValidation="false"/>
                                        <%-- Begin: Added BY Pradip For MidFeb-9 2016-05-11--%>
                                        <asp:Button ID="btnIAWarning" runat="server" Text="Ok" class="submitBtn" Width="20%"
                                                        Visible="false" CausesValidation="false"/>
                                       <%-- End: Added BY Pradip For MidFeb-9 2016-05-11--%>                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadWindow>
                        <div id="divCreditCardDetails" runat="server" visible="false">
                            <asp:Label ID="lblPaymentSummery" runat="server" Text="Payment summary" Visible="false"
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
                                        <telerik:GridTemplateColumn HeaderText="Academic cycle ID" AllowFiltering="false"
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
                                        <telerik:GridTemplateColumn HeaderText="Who pays" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWhoPay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WhoPay") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Price" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Price") %>'></asp:Label>
                                                <asp:Label ID="lblTaxAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TaxAmount") %>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                            <div>
                                <div>
                                    <asp:Label ID="lblAmount" runat="server" Text="Total amount:" Visible="false"></asp:Label>
                                    <asp:Label ID="lblTotalAmount" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblStagePaymentTotal" runat="server" Text="" Visible="false"></asp:Label>
                                </div>

                                <div>
                                    <asp:Label ID="lblStudentPaidLabel" runat="server" Text="Amount to be paid by student:"
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblAmountPaidStudent" runat="server" Text="" Visible="false"></asp:Label>
                                </div>

                                <div>
                                    <asp:Label ID="lblFirmPaidLabel" runat="server" Text="Amount to be paid by firm:"
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblAmountPaidFirm" runat="server" Text="" Visible="false"></asp:Label>
                                </div>

                                <div>
                                    <asp:Label ID="lblTax" runat="server" Text="Tax amount:" Visible="false"></asp:Label>
                                    <asp:Label ID="lblTaxAmount" runat="server" Text="" Visible="false"></asp:Label>
                                </div>

                                <div>
                                    <asp:Label ID="lblIntialAmt" runat="server" Text="Intial amount:"></asp:Label>
                                    <asp:Label ID="lblCurrency" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtIntialAmount" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div>
                                <uc1:CreditCard ID="CreditCard" runat="server" />
                                <div>
                                    <asp:Label ID="lblPaymentPlan" runat="server" Text="Payment plan:" Visible="false"></asp:Label>

                                    <asp:DropDownList ID="ddlPaymentPlan" runat="server" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>

                                <telerik:RadGrid ID="radPaymentPlanDetails" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AllowFilteringByColumn="false" Visible="false" AllowSorting="false">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Days" AllowFiltering="false" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "days") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Schedule date" AllowFiltering="false">
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

                        <div id="divSubmitBtnID"  class="actions" runat="server" visible="false">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Visible="false" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div id="SubmitDialog" style="display: none">
            </div>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="hdnEnrollmentDetailsState" runat="server" Value="1" />
    <asp:HiddenField ID="hdnEnrollmentOptionsState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnExemptionsGrantedState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCreditCard" runat="server" Value="0" />
    <asp:UpdatePanel ID="updatePnlPopup" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
<div>

                        <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Height="200px"
                                    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                                    Title="Student Enrolment" Behavior="None" VisibleOnPageLoad ="false">
                                    <ContentTemplate>
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <b>
                                                    <asp:Label ID="lblValidation" runat="server" Text=""></asp:Label></b>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="row-div clearfix" align="center">
                                                <asp:Button ID="btnValidationOK" runat="server" Text="Ok" Width="20%" class="submitBtn" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </telerik:RadWindow>
                    </div>
</div>
<div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
<script>
    function pageLoad() {
            var selectRoute = document.getElementById("baseTemplatePlaceholder_content_StudentEnrollment__c_ddlRoute");
                var selectedVal = selectRoute.options[selectRoute.selectedIndex].value;
                if (selectedVal == "2") {
                    $('.hide-from-flexible').css('display', 'none');
                }
    };
</script>
