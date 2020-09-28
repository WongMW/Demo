<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SixMonthlyReviewMentor__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.SixMonthlyReviewMentor__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="info-data">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="True">
        <ContentTemplate>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            <div class="row-div clearfix">
                <div class="label-div w30">
                    &nbsp;
                </div>
                <div class="field-div1 w100">
                    <div class="info-data" style="margin: 0 5% 0 6% !important;">
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w30">
                                <b>Student Name:</b> &nbsp;<asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hidCompanyID" Value="0" runat="server" />
                                <asp:HiddenField ID="hidRouteOfEntryID" Value="0" runat="server" />
                                <asp:HiddenField ID="hidDiaryID" Value="0" runat="server" />
                            </div>
                            <div class="field-div1 w60">
                                &nbsp;
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w30">
                                <b>Trainee Student Number:</b> &nbsp;<asp:Label ID="lblTraineeStudentNumber" runat="server"
                                    Text=""></asp:Label>
                            </div>
                            <div class="field-div1 w60">
                                &nbsp;
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w30">
                                <b>Business Unit:</b> &nbsp;<asp:Label ID="lblBueinessUnit" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="field-div1 w60">
                                &nbsp;
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w30">
                                <b>Route of Entry:</b> &nbsp;<asp:Label ID="lblRouteOfEntry" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="field-div1 w60">
                                &nbsp;
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w60">
                                <div runat="server" id="divEvaluationSection">
                                </div>
                                <div runat="server" id="divSuggestedPoint">
                                </div>
                                <div style="text-align: left;" class="row-div clearfix">
                                    <div style="text-align: left; vertical-align: middle; margin-top: -4px; font-size: large;
                                        font-weight: bold;" class="field-div1 w4">
                                        .</div>
                                    <div runat="server" id="divDevelopement1">
                                    </div>
                                </div>
                                <div style="text-align: left;" class="row-div clearfix">
                                    <div style="text-align: left; vertical-align: middle; margin-top: -4px; font-size: large;
                                        font-weight: bold;" class="field-div1 w4">
                                        .</div>
                                    <div runat="server" id="divDevelopement2">
                                    </div>
                                </div>
                                <div style="text-align: left;" class="row-div clearfix">
                                    <div style="text-align: left; vertical-align: middle; margin-top: -4px; font-size: large;
                                        font-weight: bold;" class="field-div1 w4">
                                        .</div>
                                    <div runat="server" id="divDevelopement3">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left; font-weight: normal;" class="label-div w60">
                                <span style="color: Red;">*</span> <b>Six monthly review period starting:</b>
                                <rad:RadDatePicker ID="txtStartDate" runat="server" AutoPostBack="false" Width="100px">
                                </rad:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="s" runat="server"
                                    ControlToValidate="txtStartDate" ErrorMessage="Start Date Required" Display="Dynamic"
                                    CssClass="required-label"></asp:RequiredFieldValidator>
                            </div>
                            <div style="text-align: left;">
                                &nbsp;
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left; font-weight: normal;" class="label-div w30">
                                <span style="color: Red;">*</span><b>Mentor Evaluation Title:</b>
                            </div>
                            <div>
                                &nbsp;
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w60">
                                <asp:TextBox ID="txtEvaluationTitle" Width="350px" MaxLength="50" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="s" runat="server"
                                    ControlToValidate="txtEvaluationTitle" ErrorMessage="Mentor Evaluation Title Required"
                                    Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left; font-weight: normal;" class="label-div w30">
                                <div>
                                    <span style="color: Red;">*</span><b>Mentor Evaluation Description:</b>
                                </div>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="s" runat="server"
                                ControlToValidate="txtEiditorDescription" ErrorMessage="Mentor Evaluation Description Required"
                                Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div class="field-div1 w60">
                                <div>
                                    <rad:RadEditor ID="txtEiditorDescription" EnableResize="false" runat="server" CssClass="cssEditor">
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
                                    <div>
                                        <br />
                                    </div>
                                    <div>
                                        <div runat="server" id="divDeclaration">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: right; margin-left: 122px;" class="field-div1 w60">
                                <asp:Button ID="btnBack" runat="server" Text="Back" Height="25px" CausesValidation="false" />
                                <asp:Button ID="btnSave" runat="server" Text="Submit" Height="25px" ValidationGroup="s" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Approve" ValidationGroup="s" Height="25px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Create 6 Monthly Review" Behavior="None" Height="150px">
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
            <cc3:User ID="User1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
