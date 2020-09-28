<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/StudentDiaryEntry__c.ascx.vb"
    Inherits="UserControls_Aptify_Consulting_StudentDiaryEntry__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>
<style>
    iframe
    {
        height: 100% !important;
    }
</style>
<script type="text/javascript">
    //Added By Paresh to make iframe height 100%///
    $(document).ready(function () {
        $("#DiaryEntry").find("iframe").css("height", "100%");
    });
    ///////
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
        $('#' + me).slideToggle('slow');
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

    function AllowNumericOnly(evt)//[0..9]
    {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode >= 48 && charCode <= 57 || charCode == 46)
            return true;
        else
            return false;
    }



    //    	 Start of the code - Added javascript function by Harish - Redmine #20931 To disable Save & Exit button 
    function DisableButn(event) {
        if ($('#baseTemplatePlaceholder_content_StudentDiaryEntry__c_btnSavenExit').hasClass('disableBtn')) {
            event.preventDefault();
            event.stopPropagation();
        } else if (Page_ClientValidate("")) {
            document.getElementById("baseTemplatePlaceholder_content_StudentDiaryEntry__c_btnSavenExit").value = "Please Wait..";
            document.getElementById("baseTemplatePlaceholder_content_StudentDiaryEntry__c_btnSavenExit").setAttribute("class", "disableBtn");
            document.getElementById("baseTemplatePlaceholder_content_StudentDiaryEntry__c_btnSavenExit").disabled = true;
        }
    }
    //	     End of the Javascript code Added by Harish - Redmine #20931 To disable Save & Exit button


    //added as part of Redmine #21130 to disable buttons on click
    function DisableYesBtn(event) {
        if ($('#baseTemplatePlaceholder_content_StudentDiaryEntry__c_radWindowSave_C_btnSaveYes').hasClass('disableBtn')) {
            event.preventDefault();
            event.stopPropagation();
        } else if (Page_ClientValidate("")) {
            //document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_StudentDiaryEntry__c_radWindowSave_C_btnSaveYes").value = "Please Wait..";
            document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_StudentDiaryEntry__c_radWindowSave_C_btnSaveYes").setAttribute("class", "disableBtn");
            document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_StudentDiaryEntry__c_radWindowSave_C_btnSaveYes").disabled = true;
        }
    }
    function DisableReviewYesBtn(event) {
        if ($('#baseTemplatePlaceholder_content_StudentDiaryEntry__c_radWindowSubmiToMentor_C_btnReviewYes').hasClass('disableBtn')) {
            event.preventDefault();
            event.stopPropagation();
        } else if (Page_ClientValidate("")) {
            //document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_StudentDiaryEntry__c_radWindowSave_C_btnReviewYes").value = "Please Wait..";
            document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_StudentDiaryEntry__c_radWindowSubmiToMentor_C_btnReviewYes").setAttribute("class", "disableBtn");
            document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_StudentDiaryEntry__c_radWindowSubmiToMentor_C_btnReviewYes").disabled = true;
        }
    }
    function DisableMentorBtn(event) {
        if ($('#baseTemplatePlaceholder_content_StudentDiaryEntry__c_btnSubmitToMentor').hasClass('disableBtn')) {
            event.preventDefault();
            event.stopPropagation();
        } else if (Page_ClientValidate("")) {
            document.getElementById("baseTemplatePlaceholder_content_StudentDiaryEntry__c_btnSubmitToMentor").value = "Please Wait..";
            document.getElementById("baseTemplatePlaceholder_content_StudentDiaryEntry__c_btnSubmitToMentor").setAttribute("class", "disableBtn");
            document.getElementById("baseTemplatePlaceholder_content_StudentDiaryEntry__c_btnSubmitToMentor").disabled = true;
        }
    }
    //	     End Redmine 21130 To disable button on first click
</script>

<%--Start of the code - Added by Harish - Redmine #20931 added style to the button once clicked for first time --%> 
<style type="text/css">
    /*changed css class name as part of #21130*/
    .disableBtn { 
        background-color: gray !important;
        color: white !important;
        padding: 8px 20px !important;
        height: 40px !important;
        display: inline-block !important;
        text-transform: uppercase !important;
        border: 2px solid transparent !important;
        margin-right: 5px !important;
    }
</style>

<%--End of the code - Added by Harish - Redmine #20931 added style to the button once clicked for first time --%> 

<div class="dvUpdateProgress" style="overflow: visible;">
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
    <asp:HiddenField ID="hdnTimeFrameState" runat="server" Value="1" />
    <asp:HiddenField ID="hdnDiaryEntryState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnExemptionsGrantedState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnStatusReasonState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEligibilityState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCADiaryRecordID" runat="server" Value="-1" />
    <asp:HiddenField ID="HFMentorID" runat="server" Value="-1" />
    <div id="MainTable" style="width: 100%">
        <asp:Label ID="lblMessage1" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        <asp:Panel ID="pnlMain" runat="server">
            <h1>CA Diary entry
            </h1>
        </asp:Panel>
    </div>
    <!-- follow the style of this panel -->
    <asp:Panel ID="pnlDetails" runat="server" CssClass="cai-form">
        <span class="expand form-title" id="HeadTimeFrame" onclick="CollapseExpand('TimeFrame','hdnTimeFrameState')">Time frame</span>
        <div id="TimeFrame" class="active cai-form-content">
            <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div>
                        <asp:Label ID="Label3" runat="server" CssClass="label-title" Text="Diary entry pertains to the following timeframe"></asp:Label>
                        <div runat="server" id="EduDetails">
                            <span class="label-title"><span class="RequiredField">*</span>Start date:</span>

                            <rad:RadDatePicker ID="txtStartDate" runat="server" AutoPostBack="true">
                            </rad:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStartDate"
                                ValidationGroup="CompanyControl" ErrorMessage="Start date required" Display="Dynamic"
                                CssClass="required-label"></asp:RequiredFieldValidator>
                        </div>


                        <div runat="server" id="Div1">
                            <span class="label-title"><span class="RequiredField">*</span>End date:</span>
                            <div>
                                <rad:RadDatePicker ID="txtEndDate" runat="server" AutoPostBack="true">
                                </rad:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEndDate"
                                    ValidationGroup="CompanyControl" ErrorMessage="End date required" Display="Dynamic"
                                    CssClass="required-label"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <hr>

                    <div>
                        <asp:Label ID="Label1" runat="server" CssClass="label-title" Text="Out of office"></asp:Label>
                        <span>Please enter your leave information within this time period:</span>

                        <div class="label-title">
                            <asp:Label ID="lnkleavetype" Font-Underline="false" runat="server">Leave type:</asp:Label>
                        </div>
                        <div class="data-holder">
                            <asp:DropDownList ID="ddlLeaveType" runat="server" >
                            </asp:DropDownList>
                        </div>

                        <div>
                            <span class="label-title">Days:</span>
                            <asp:TextBox ID="txtDays" runat="server"  onkeypress="return AllowNumericOnly(event)"
                                MaxLength="8"></asp:TextBox>
                        </div>
                       
                        <div class="actions">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="submitBtn" Visible="true"></asp:Button>
                        </div>
                    </div>


                    <div runat="server" id="Div6">
                        <asp:Panel ID="PanelLeave" runat="server">
                            <div id="divLeave" runat="server">
                                <span class="label-title">Leave added</span>
                            </div>
                            <rad:RadGrid ID="grdLeave" CssClass="cai-table" runat="server" AutoGenerateColumns="False"
                                AllowPaging="false" AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                                AllowSorting="false" SortingSettings-SortedAscToolTip="Sorted Ascending" Visible="true"
                                ShowHeadersWhenNoRecords="true">
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                </ClientSettings>
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false"
                                    ShowHeadersWhenNoRecords="true">
                                    <NoRecordsTemplate>
                                        <div>
                                            No data to display
                                        </div>
                                    </NoRecordsTemplate>
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="Leave type" DataField="LeaveType" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeaveType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LeaveType") %>'></asp:Label>
                                                <asp:HiddenField ID="hdnLeaveTypeID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LeaveTypeID") %>' />
                                                <asp:HiddenField ID="hdnLID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "lID") %>' />
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Days" DataField="Days" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Days") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Remove?" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" FilterControlWidth="80%" UniqueName="ColRemove">
                                            <ItemTemplate>
                                                <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("LeaveType")%>'
                                                    OnClick="btnLeaveRemove_Click" />
                                                <asp:HiddenField ID="hdnsdlID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "sdlID") %>' />
                                                <asp:HiddenField ID="hdndlID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "dlID") %>' />
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                        </asp:Panel>
                    </div>

                    <div>
                        <div>
                            <asp:Label ID="lblExperience" runat="server" CssClass="label-title" Text="Days of relevant experience within timeframe"></asp:Label>
                            <div runat="server" id="Div7">
                            </div>

                            <span class="label-title"><span class="RequiredField">*</span>Please enter the number of days of relevant experience pertaining to this diary entry</span>
                            <asp:TextBox ID="txtExperience" runat="server" onkeypress="return AllowNumericOnly(event)" MaxLength="8"></asp:TextBox>

                             <br />
                                                <asp:Label ID="lblexperr" runat="server" ForeColor="Red" Visible="false"></asp:Label></b>


                            <span class="label-title">Max. number of experience days:</span>
                            <asp:TextBox ID="TxtTotalDays" runat="server" Enabled = "False" ></asp:TextBox> 
 
                            <span class="label-title">Overtime Worked (whole days to be recorded):</span>
                            <asp:TextBox ID="txtToilDays" runat="server"  onkeypress="return AllowNumericOnly(event)"
                                MaxLength="8"></asp:TextBox>
                         
                      </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSavenExit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSubmitToMentor" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <!-- end -->

    <asp:Panel ID="pnlDiaryEntry" runat="server" CssClass="cai-form">
        <h4 class="expand form-title" id="HeadDiaryEntry" onclick="CollapseExpand('DiaryEntry','hdnHeadDiaryEntryState')">Diary entry</h4>
        <div id="DiaryEntry" class="collapse cai-form-content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div runat="server" id="Div2">
                        <span class="label-title"><span class="RequiredField">*</span>Title:</span>
                        <asp:TextBox ID="txtTitle" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTitle"
                            ValidationGroup="CompanyControl" ErrorMessage="Title required" Display="Dynamic"
                            CssClass="required-label"></asp:RequiredFieldValidator>

                    </div>
                    <div>
                        <span class="label-title"><span class="RequiredField">*</span>Description:</span>
                        <rad:RadEditor ID="txtEiditorDescription" EnableResize="false" runat="server" CssClass="MyRadEdit clearfix">
                            <Tools>
                                <telerik:EditorToolGroup>
                                    <telerik:EditorTool Name="Undo" ShortCut="CTRL+Z" />
                                    <telerik:EditorTool Name="Redo" ShortCut="CTRL+R" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorTool Name="SelectAll" ShortCut="CTRL+A" />
                                    <telerik:EditorTool Name="Cut" ShortCut="CTRL+X" />
                                    <telerik:EditorTool Name="Copy" ShortCut="CTRL+C" />
                                    <telerik:EditorTool Name="Paste" ShortCut="CTRL+P" />
                                    <telerik:EditorTool Name="PasteStrip" />
                                    <telerik:EditorTool Name="PasteFromWord" />
                                    <telerik:EditorTool Name="PastePlainText" />
                                    <telerik:EditorTool Name="PasteAsHtml" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorTool Name="Zoom" />
                                    <telerik:EditorTool Name="ConvertToUpper" />
                                    <telerik:EditorTool Name="ConvertToLower" />
                                    <telerik:EditorTool Name="FormatStripper" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorTool Name="FormatBlock" />
                                    <telerik:EditorTool Name="FontName" />
                                    <telerik:EditorTool Name="FontSize" />
                                    <telerik:EditorTool Name="ForeColor" />
                                    <telerik:EditorTool Name="BackColor" />
                                    <telerik:EditorTool Name="Bold" ShortCut="CTRL+B" />
                                    <telerik:EditorTool Name="Italic" ShortCut="CTRL+I" />
                                    <telerik:EditorTool Name="Underline" ShortCut="CTRL+U" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorTool Name="JustifyLeft" />
                                    <telerik:EditorTool Name="JustifyCenter" />
                                    <telerik:EditorTool Name="JustifyRight" />
                                    <telerik:EditorTool Name="JustifyFull" />
                                    <telerik:EditorTool Name="JustifyNone" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorTool Name="Indent" />
                                    <telerik:EditorTool Name="Outdent" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorTool Name="InsertUnorderedList" />
                                    <telerik:EditorTool Name="InsertOrderedList" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorTool Name="InsertSymbol" />
                                </telerik:EditorToolGroup>
                            </Tools>
                        </rad:RadEditor>

                        <asp:Label ID="lblDesc" runat="server" Visible="false"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEiditorDescription"
                            ValidationGroup="CompanyControl" ErrorMessage="Description required" Display="Dynamic"
                            CssClass="required-label"></asp:RequiredFieldValidator>
                    </div>

                    <div>
                        <span class="label-title">
                            <span class="RequiredField">*</span>Learning level:
                        </span>

                        <asp:DropDownList ID="ddlLearningLevel" Width="200px" runat="server">
                            <asp:ListItem>Apply</asp:ListItem>
                            <asp:ListItem>Integrate</asp:ListItem>
                            <asp:ListItem>Understand</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlLearningLevel"
                            ValidationGroup="CompanyControl" ErrorMessage="Learning level required" Display="Dynamic"
                            CssClass="required-label"></asp:RequiredFieldValidator>
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSavenExit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSubmitToMentor" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>


    <asp:Panel ID="pnlEligibility" runat="server" CssClass="cai-form">
		<%--Corrected spelling for Competencies Redmine  log #18753--%>
        <%--Redmine #21062 Amended Heading to 'Competencies attained and Audit days'--%>
        <h2 id="H1" class="expand form-title" onclick="CollapseExpand('Eligibility','hdnEligibilityState')">Competencies attained and Audit days
        </h2>
        <div id="Eligibility" class="collapse cai-form-content">
            <asp:UpdatePanel ID="UpdatePaneExperience" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div>
                        <div>
                        </div>
                        <div>
                            <div>
                                <asp:Label ID="lnkcategory" Font-Underline="false" CssClass="label-title" runat="server"><span class="RequiredField">*</span>Competency/area of experience category:</asp:Label>
                            </div>
                            <div>
                                <asp:DropDownList ID="ddlExpCategory" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlExpCategory"
                                    ValidationGroup="CompanyControl" ErrorMessage="Experience category required"
                                    Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div>
                            <div>
                                <asp:Label ID="lnkAreaExp" Font-Underline="false" CssClass="label-title" runat="server"  Style="cursor: default;" >Competency/area of experience: </asp:Label>
                            </div>
                            <div>
                                <asp:DropDownList ID="ddlAreaofExperience" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlAreaofExperience"
                                    ValidationGroup="CompanyControl" ErrorMessage="Area of experience required" Display="Dynamic"
                                    CssClass="required-label"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="actions">
                            <div>
                                <asp:Button ID="btnAddExperience" runat="server" CssClass="submitBtn" Text="Add" />
                            </div>
                        </div>
                        <div>
                            <div>
                                <asp:Panel ID="PanelCompetency" class="cai-table" runat="server">
                                    <div runat="server" id="Div8">
                                        <div id="divcmp" class="label-title" runat="server">
                                            Competencies/area of experience added:
                                        </div>
                                        <div>
                                            <rad:RadGrid ID="gvCompetency" CssClass="MyGrid" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="false" AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                                                AllowSorting="false" SortingSettings-SortedAscToolTip="Sorted Ascending" Visible="true"
                                                ShowHeadersWhenNoRecords="true">
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                </ClientSettings>
                                                <GroupingSettings CaseSensitive="false" />
                                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false"
                                                    ShowHeadersWhenNoRecords="true">
                                                    <NoRecordsTemplate>
                                                        <div>
                                                            No data to display
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <Columns>
                                                        <rad:GridTemplateColumn HeaderText="Competency/area of experience category" AutoPostBackOnFilter="true"
                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="28%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExpCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ComptencyCategory") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdnCompetencyID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ComptencyID") %>' />
                                                                <asp:HiddenField ID="hdnCID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "cID") %>' />
                                                            </ItemTemplate>
                                                        </rad:GridTemplateColumn>
                                                        <rad:GridTemplateColumn HeaderText="Competency/area of experience" AutoPostBackOnFilter="true"
                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="22%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExperience" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Experience") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </rad:GridTemplateColumn>
                                                        <rad:GridTemplateColumn HeaderText="Remove?" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ShowFilterIcon="false" UniqueName="ColRemove" HeaderStyle-Width="7%">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnRemoveExperience" runat="server" Text="Remove" CommandName="Remove"
                                                                    CommandArgument='<%# Eval("ComptencyCategory")%>' OnClick="btnCompetencyRemove_Click" />
                                                                <asp:HiddenField ID="hdnsdcID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "sdcID") %>' />
                                                                <asp:HiddenField ID="hdndcID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "dcID") %>' />
                                                            </ItemTemplate>
                                                        </rad:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </rad:RadGrid>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div>
                                <asp:Panel ID="PanelRegExp" runat="server" Visible="true">
                                    <div>
                                        <div class="label-title">
                                            Regulated experience in a practice environment :
                                        </div>
                                        <div>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <div>
                                                    <span class="label-title">Audit type </span>
                                                    <span class="label-title">Days</span>
                                                </div>
                                                <div>
                                                       <div class="label-title">&nbsp;</div>
                                                    <asp:Label ID="lblAuditFieldMsg" runat="server" Text=""></asp:Label>
													<%--Added new field on form, redmine log #18753 and updated as per #20233--%>
													<div class="label-title">Statutory audit (ROI only) </div>
                                                    <asp:TextBox ID="txtStatutoryDays" runat="server" MaxLength="8" onkeypress="return AllowNumericOnly(event)" Enabled="false"></asp:TextBox>

													<div class="label-title">Other audit (ROI only) </div>
                                                    <asp:TextBox ID="txtOtherDays" runat="server" MaxLength="8"  onkeypress="return AllowNumericOnly(event)" Enabled="false"></asp:TextBox>

                                                    <div class="label-title">Company audit (NI/UK only) </div>
                                                    <asp:TextBox ID="txtCompanyDays" runat="server" MaxLength="8" onkeypress="return AllowNumericOnly(event)" Enabled="false"></asp:TextBox>

													<div class="label-title">Other audit (NI/UK only) </div>
                                                    <asp:TextBox ID="txtOtherNIDays" runat="server" MaxLength="8" onkeypress="return AllowNumericOnly(event)" Enabled="false"></asp:TextBox>
                                                   <%--End #20233--%>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                        <div>
                            <div>
                                <div runat="server" id="Div9">
                                    <div class="label-title">Engagement numbers: </div>
                                </div>

                                <asp:TextBox ID="txtEngagmentNumber" runat="server"  Height="50px"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblEngagement" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <telerik:RadWindow ID="radWindowSubmiToMentor" runat="server" Modal="True"
                        VisibleStatusbar="False" Behaviors="None"
                        Title="Submit to mentor" Behavior="None" Height="170px" width="500px"><%--update width 500 for Redmine #20871--%>
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMentor" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                        </div>
                                        <div style="text-align:center;">
										<%--updated as part of redmine log #21130--%>
                                            <asp:Button ID="btnReviewYes" runat="server" Text="Yes" Width="70px" class="submitBtn" OnClientClick="if($('#baseTemplatePlaceholder_content_StudentDiaryEntry__c_radWindowSubmiToMentor_C_btnReviewYes').hasClass('disableBtn')) {return true;} javascript:DisableReviewYesBtn(event);" UseSubmitBehavior="false" />
                                            <asp:Button ID="btnReviewNo" runat="server" Text="No" Width="70px" class="submitBtn" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </telerik:RadWindow>
					 <%--Redmine Enhancement #20871 - Start of the code - Added by Harish Date. 14/11/2019--%>
                    <telerik:RadWindow ID="radWindowSave" runat="server" Modal="True"
                        VisibleStatusbar="False" Behaviors="None"
                        Title="Save & Exit" Behavior="None" Height="170px" width="500px" > <%--update width 500 for Redmine #20871--%>
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSave" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                        </div>
                                        <div style="text-align:center;">
										<%--updated as part of redmine log #21130--%>
                                            <asp:Button ID="btnSaveYes" runat="server" Text="Yes" Width="70px" class="submitBtn"  OnClientClick="if($('#baseTemplatePlaceholder_content_StudentDiaryEntry__c_radWindowSave_C_btnSaveYes').hasClass('disableBtn')) {return true;} javascript:DisableYesBtn(event);" UseSubmitBehavior="false" /> 
                                            <asp:Button ID="btnSaveNo" runat="server" Text="No" Width="70px" class="submitBtn" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </telerik:RadWindow>
					 <%--Redmine Enhancement #20871 - End of the code - Added by Harish Date. 14/11/2019--%>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSavenExit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSubmitToMentor" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>


    <asp:Panel ID="PriorExp" runat="server" Visible="false">
        <h2 id="HeadDeclaration" class="expand" onclick="CollapseExpand('ExemptionsGranted','hdnExemptionsGrantedState')">Prior year's experience
        </h2>
        <div id="ExemptionsGranted">
            <asp:Panel BorderWidth="0" ID="pnlCotractDeclaration"
                runat="server">
                <div>
                    <div>
                    </div>
                    <div runat="server" id="divAttachments">
                        <div>
                            <span class="Error">
                                <asp:Label ID="lblErrorFile" Visible="false" runat="server">Error</asp:Label></span>
                        </div>
                        <div>
                            <uc2:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                                AllowAdd="True" AllowDelete="false" />
                        </div>
                    </div>
                    <div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>

    <asp:Panel ID="Status" runat="server">
        <div>
            <b>
                <asp:Label ID="lblsts" runat="server" Text="Status:"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" Text="with student"></asp:Label>
            </b>
        </div>
    </asp:Panel>
    <div class=" actions">
      <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Always">
       <ContentTemplate>
        <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red"></asp:Label> 
		   
      <%--Start of the code - Added by Harish - Redmine #20931 added onclientclick attribute to call disable button --%> 
        <asp:Button ID="btnSavenExit" runat="server" class="submitBtn" Text="Save & Exit" OnClick="btnSavenExit_Click" OnClientClick="if($('#baseTemplatePlaceholder_content_StudentDiaryEntry__c_btnSavenExit').hasClass('disableBtn')) {return true;} javascript:DisableButn(event);" UseSubmitBehavior="false" />       
      <%--End of the code - Added by Harish - Redmine #20931 added onclientclick  attribute to call disable button --%> 
	  <%--updated as part of redmine log #21130--%>
		      <asp:Button ID="btnSubmitToMentor" runat="server" class="submitBtn" Text="Submit to Mentor" OnClientClick="if($('#baseTemplatePlaceholder_content_StudentDiaryEntry__c_btnSubmitToMentor').hasClass('disableBtn')) {return true;} javascript:DisableMentorBtn(event);" UseSubmitBehavior="false"  />
      <div style="display:none;">  <asp:Button ID="btnBack" runat="server" Text="Back" class="submitBtn" /></div>
       <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
      </ContentTemplate>
      </asp:UpdatePanel>
    </div>

</div>


       
<div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
