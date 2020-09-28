<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LLLExemptionRequests__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLExemptionRequests__c" %>
<%@ Register TagPrefix="rad1" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/BeforeSaveRecordAttachments__c.ascx"
    TagName="SMAARecordAttachments" TagPrefix="uc2" %>
<style type="text/css">
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
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 600px;">
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
<div class="info-data">
    <asp:Panel ID="pnl" runat="server">
        <div class="row-div clearfix">
            <div>
                <asp:Label ID="lblDesc" runat="server"></asp:Label>
            </div>
            <div>
                <br />
            </div>
            <div class="row-div clearfix" id="div4" runat="server">
                <div style="text-align: left;" class="field-div1 w17">
                    <b>Exemption Request :</b>
                </div>
                <div class="field-div1 w12" style="text-align: left;">
                    <asp:RadioButton ID="rdException" runat="server" AutoPostBack="true" GroupName="grp2">
                    </asp:RadioButton>
                </div>
                <div class="row-div clearfix" id="div6" runat="server">
                    <div style="text-align: left;" class="field-div1 w17">
                        <b>Enrollment Request:</b>
                    </div>
                    <div class="field-div1 w12" style="text-align: left;">
                        <asp:RadioButton ID="rdEnroll" runat="server" AutoPostBack="true" GroupName="grp2">
                        </asp:RadioButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlExemption" runat="server">
        <div class="row-div clearfix" id="div5" runat="server">
            <div style="text-align: left;" class="field-div1 w20">
                <span style="color: Red;">*</span> <b>Type :</b>
            </div>
            <div class="field-div1 w60" style="text-align: left;">
                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" Width="30%">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Please select a Type"
                    ValueToCompare="Select" ControlToValidate="ddlType" class="RequiredField" Operator="NotEqual"
                    ValidationGroup="VGG"></asp:CompareValidator>
            </div>
            <%-- <asp:Label ID="lbltypecomp" Visible="false" runat="server" ForeColor="Red"></asp:Label>   --%>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDetails" runat="server">
        <div id="Eligibility" runat="server">
            <div class="row-div clearfix">
                <div class="field-div1 w100">
                    <div id="divFirstName" runat="server" class="row-div clearfix">
                        <div class="field-div1 w20" style="text-align: left;">
                            <b>Student Number :</b>
                        </div>
                        <div class="field-div1 w17" style="text-align: left;">
                            <asp:Label ID="lblStudentno" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblStudentID" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div id="div2" runat="server" class="row-div clearfix">
                        <div class="field-div1 w20" style="text-align: left;">
                            <b>Student Name:</b>
                        </div>
                        <div class="field-div1 w17" style="text-align: left;">
                            <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div id="divLastName" runat="server" class="row-div clearfix">
                        <div class="field-div1 w20" style="text-align: left;" visible="false">
                            <b>Qualification:</b>
                        </div>
                        <div class="field-div1 w60" style="text-align: left;">
                            <asp:Label ID="lblQualification" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblQualificationID" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="row-div clearfix">
                        <div class="field-div1 w20" style="text-align: left;">
                            <b>Comments:</b>
                        </div>
                        <div class="field-div1 w60" style="text-align: left;">
                            <asp:TextBox ID="txtcomment" runat="server" Height="150" MaxLength="50" Style="resize: none"
                                TextMode="MultiLine" Width="350px"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divIsnonsmember" runat="server" class="row-div clearfix">
                        <div class="field-div1 w20" style="text-align: left;">
                        </div>
                        <div class="field-div1 w60" style="text-align: left;">
                            <asp:CheckBox ID="chkIsOtherTaxationBodyMemberFull_Non" runat="server" Visible="false" />
                        </div>
                        <div id="div3" runat="server" class="row-div clearfix">
                            <div class="field-div1 w20" style="text-align: left;">
                                <b>Member of a recognized accountancy body:</b>
                            </div>
                            <div class="field-div1 w17" style="text-align: left;">
                                <asp:DropDownList ID="drpAccountancy" runat="server" AutoPostBack="true" Width="102%">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div id="divIsmember" runat="server" class="row-div clearfix">
                        <div id="Div7" runat="server" class="field-div1 w20" style="text-align: left;">
                            &nbsp;
                        </div>
                        <div class="field-div1 w60" style="text-align: left;">
                            <asp:CheckBox ID="chkIsOtherTaxationBodyMember" runat="server" />
                            &nbsp; &nbsp; <b>Are you Member of other taxation bodies</b>
                        </div>
                    </div>
                    <div class="row-div clearfix">
                    </div>
                </div>
            </div>
        </div>
        <div class="row-div clearfix" id="divupload" runat="server">
            <div style="text-align: left;" class="field-div1 w20">
                <b>Upload Document:</b>
            </div>
            <div class="field-div1 w60" style="text-align: left;">
                <div runat="server" id="divAttachments" style="font-size: 8pt; color: Black; font-style: normal;
                    font-weight: normal; width: 80%;">
                    <div>
                        <span class="Error">
                            <asp:Label ID="lblErrorFile" Visible="false" runat="server">Error</asp:Label></span></div>
                    <div>
                        <uc2:SMAARecordAttachments ID="raSupportLetter" Visible="true" runat="server"></uc2:SMAARecordAttachments>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-div clearfix" id="div1" runat="server">
            <div style="text-align: left;" class="field-div1 w12">
                &nbsp;
            </div>
            <div class="field-div1 w60" style="text-align: right;">
                <asp:Button ID="btnBack" runat="server" Text="Back" />&nbsp;
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="VGG" />&nbsp<asp:Label
                    ID="lblsubmit" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="info-data">
            <div class="row-div clearfix">
            </div>
            <div class="row-div clearfix">
                <div class="label-div-left-align w80">
                </div>
            </div>
            <div class="row-div clearfix">
                <div class="label-div-left-align w80">
                </div>
            </div>
        </div>
        <%--<rad1:RadWindow ID="RadAlert" runat="server" Width="250px" Height="150px" Modal="True"
    Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
    ForeColor="#BDA797"  Title="Success" Behavior="None"><ContentTemplate><table width="100%"><tr ><td align="center" height="25">Exemption Request submitted successfully ! </td></tr><tr ><td align="center" height="25"><asp:Button ID="btnOK" runat="server" Text="Ok" CssClass="submitBtn"
                        CausesValidation="false" Height="23px" Width="60px" /></td></tr></table></ContentTemplate></rad1:RadWindow>--%>
        <telerik:RadWindow ID="RadAlert" runat="server" Width="350px" Modal="True" BackColor="#f4f3f1"
            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Life Long Learning Request Application"
            Behavior="None" Height="150px">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                    padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblsuccess" runat="server" Font-Bold="true" Text=""></asp:Label>
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
    </asp:Panel>
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
</div>
