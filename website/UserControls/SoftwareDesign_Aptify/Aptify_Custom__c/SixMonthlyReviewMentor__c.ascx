<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/SixMonthlyReviewMentor__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.SixMonthlyReviewMentor__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--begin:17840--%>
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
<%--end:17840--%>
<div class="info-data">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="True">
        <ContentTemplate>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
            <div class="field-div1">
                <div class="info-data cai-form">
                    <div class="cai-form-content">
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1">
                                <span class="label-title-inline">Student name:</span>
                                <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hidCompanyID" Value="0" runat="server" />
                                <asp:HiddenField ID="hidRouteOfEntryID" Value="0" runat="server" />
                                <asp:HiddenField ID="hidDiaryID" Value="0" runat="server" />
                            </div>
                            <div class="field-div1">
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1">
                                <span class="label-title-inline">Student number:</span>
                                <asp:Label ID="lblTraineeStudentNumber" runat="server"
                                    Text=""></asp:Label>
                            </div>
                            <div class="field-div1">
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1">
                                <span class="label-title-inline">Business unit:</span>
                                <asp:Label ID="lblBueinessUnit" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="field-div1">
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1">
                                <span class="label-title-inline">Route of entry:</span>
                                <asp:Label ID="lblRouteOfEntry" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="field-div1">
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                                <div runat="server" id="divEvaluationSection">
                                </div>
                                <div runat="server" id="divSuggestedPoint">
                                </div>
                                <ul class="bullet-points">
                                    <li runat="server" id="divDevelopement1"></li>
                                    <li runat="server" id="divDevelopement2"></li>
                                    <li runat="server" id="divDevelopement3"></li>
                                </ul>

                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left; font-weight: normal;" class="label-div">
                                <span style="color: Red;">*</span> <span class="label-title-inline">Six monthly review period starting:</span>
                                <rad:RadDatePicker ID="txtStartDate" runat="server" AutoPostBack="false" Width="100px">
                                </rad:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="s" runat="server"
                                    ControlToValidate="txtStartDate" ErrorMessage="Start date required" Display="Dynamic"
                                    CssClass="required-label"></asp:RequiredFieldValidator>
                            </div>
                            <div style="text-align: left;">
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left; font-weight: normal;" class="label-div">
                                <span style="color: Red;">*</span><span class="label-title-inline">Mentor evaluation title:</span>
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1">
                                <asp:TextBox ID="txtEvaluationTitle" Width="350px" MaxLength="50" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="s" runat="server"
                                    ControlToValidate="txtEvaluationTitle" ErrorMessage="Mentor evaluation title required"
                                    Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left; font-weight: normal;" class="label-div">
                                <div>
                                    <span style="color: Red;">*</span><span class="label-title-inline">Mentor evaluation description:</span>
                                </div>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="s" runat="server"
                                ControlToValidate="txtEiditorDescription" ErrorMessage="Mentor evaluation description required"
                                Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                          
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

                            <div runat="server" id="divDeclaration">
                            </div>

                        </div>
                        <div class="actions">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="submitBtn" CausesValidation="false" />
                            <asp:Button ID="btnSave" runat="server" Text="Save draft" CssClass="submitBtn" ValidationGroup="s" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit review" ValidationGroup="s" CssClass="submitBtn" />
                        </div>
                    </div>
                </div>
            </div>
           <%-- Dipali--%>
          <%--  </div>--%>
            <telerik:RadWindow ID="rwValidation" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Create 6 monthly review" Behavior="None" Height="150px">
                <ContentTemplate>
                    <asp:Label ID="lblValidationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                    <asp:Button ID="btnValidationOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                
                </ContentTemplate>
            </telerik:RadWindow>

     <%--       radWindowValidation--%>
            <cc3:User ID="User1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
