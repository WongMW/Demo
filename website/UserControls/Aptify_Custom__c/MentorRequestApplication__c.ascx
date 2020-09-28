<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MentorRequestApplication__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MentorRequestApplication__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>
<div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</div>
<div class="info-data">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                <div class="row-div clearfix">
                    <div>
                        <br />
                    </div>
                    <div class="info-data" style="margin: 0 5% 0 15% !important;">
                        <div class="row-div clearfix">
                            <div class="field-div1 w25">
                                <span style="color: Red;">*</span> <strong>Professional status: &nbsp;</strong>
                            </div>
                            <div class="field-div1 w60">
                                <asp:DropDownList ID="ddlProfessionalStatus" Width="155px" runat="server">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlProfessionalStatus"
                                    ErrorMessage="Please select professional status" CssClass="required-label" Operator="NotEqual"
                                    ValidationGroup="S" ValueToCompare="Select" SetFocusOnError="true" style="color: Red;"></asp:CompareValidator>
                            </div>
                            <br />
                        </div>
                        <div class="row-div clearfix">
                            <div class="field-div1 w25">
                                <span style="color: Red;">*</span> <strong>Membership number: &nbsp;</strong>
                            </div>
                            <div class="field-div1 w60">
                                <asp:TextBox ID="txtMemberNumber" Width="150px" MaxLength="50" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMemberNumber"
                                    ErrorMessage="Please enter membership number" ValidationGroup="S" SetFocusOnError="true"
                                    Display="Dynamic" CssClass="required-label" style="color: Red;"></asp:RequiredFieldValidator>
                            </div>
                            <br />
                        </div>
                        <div class="row-div clearfix">
                            <div class="field-div1 w25">
                                <span style="color: Red;">*</span><strong>Qualification year: &nbsp;</strong>
                            </div>
                            <div style="text-align: left;" class="field-div1 w60">
                                <asp:DropDownList ID="ddlYear" Width="155px" runat="server">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlYear"
                                    ErrorMessage="Please select qualification year" CssClass="required-label" Operator="NotEqual"
                                    ValidationGroup="S" ValueToCompare="Select" SetFocusOnError="true" style="color: Red;"></asp:CompareValidator>
                            </div>
                            <br />
                        </div>
                        <div class="row-div clearfix">
                            <div class="field-div1 w25">
                                <span style="color: Red;">*</span> <strong>Please enter the student's number: &nbsp;</strong>
                            </div>
                            <div class="field-div1 w60">
                                <asp:TextBox ID="txtStudentNumber" Width="150px" AutoPostBack="true" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="S" runat="server"
                                    ControlToValidate="txtStudentNumber" ErrorMessage="Please enter student number"
                                    Display="Dynamic" CssClass="required-label" style="color: Red;"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hidStudentID" Value="0" runat="server" />
                            </div>
                            <br />
                        </div>
                        <div class="row-div clearfix">
                            <div class="field-div1 w25">
                                <strong>Name of student:&nbsp;</strong>
                            </div>
                            <div class="field-div1 w60">
                                <asp:Label ID="lblStudentName" Style="font-weight: bold;" runat="server" Text=""></asp:Label>
                            </div>
                            <br />
                        </div>
                        <div class="row-div clearfix">
                            <div class="field-div1 w45">
                                <asp:CheckBox ID="chkTerms" Text="I agree with the terms and conditions of this application"
                                    TextAlign="Right" runat="server" />
                            </div>
                            <div class="field-div1 w50">
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="hlTermsandconditions" Style="color: Blue; text-decoration: underline;"
                                    runat="server">Terms & conditions</asp:LinkButton>
                            </div>
                            <br />
                        </div>
                        <br />
                        <div>
                            <asp:Button ID="btnSubmit" ValidationGroup="S" runat="server" class="submitBtn" Height="40px" Text="Submit" />
                        </div>
                        <br />
                    </div>
                </div>
                <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Modal="True"
                    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                    Title="Request for mentor status application form" Behavior="None" Height="150px">
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
                                        <asp:Button ID="btnOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <cc3:User ID="User1" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtStudentNumber" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </telerik:RadAjaxPanel>
</div>
