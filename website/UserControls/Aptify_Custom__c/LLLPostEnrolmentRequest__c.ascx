<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LLLPostEnrolmentRequest__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLPostEnrolmentRequest__c" %>
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
    <br />
  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
            <asp:Panel ID="pnlDetails" runat="server">
                <div id="Eligibility">
                    <div class="row-div clearfix">
                        <div class="label-div w30">
                            &nbsp;
                        </div>
                        <div class="field-div1 w100" >
                           <div class="row-div clearfix" id="div5" runat="server">
                                <div style="text-align: left;" class="field-div1 w12">
                                    <b>Type :</b>
                                </div>
                                <div class="field-div1 w60" style="text-align: left;height:20px;">
                                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" width="350px" Height ="20px">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Please select a Type"
                                        ValueToCompare="Select" ControlToValidate="ddlType" class="RequiredField" Operator="NotEqual"
                                        ValidationGroup="VGG"></asp:CompareValidator>
                                </div>
                            </div>
                           
                            <div class="row-div clearfix">
                            </div>
                        </div>
                        <div id="div10" class="field-div1 w100"  runat="server">
                            
                            <div class="row-div clearfix" id="divFirstName" runat="server">
                                <div style="text-align: left;" class="field-div1 w12">
                                    <b>Student Number :</b>
                                </div>
                                <div class="field-div1 w17" style="text-align: left;">
                                    <asp:Label ID="lblStudentno" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblStudentID" runat="server" Visible="false" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row-div clearfix" id="div2" runat="server">
                                <div style="text-align: left;" class="field-div1 w12">
                                    <b>Student Name:</b>
                                </div>
                                <div class="field-div1 w17" style="text-align: left;">
                                    <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row-div clearfix" id="divLastName" runat="server">
                                <div style="text-align: left;" class="field-div1 w12">
                                    <b>Qualification:</b>
                                </div>
                                <div class="field-div1 w60" style="text-align: left;">
                                    <asp:Label ID="lblQualification" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblQualificationID" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="row-div clearfix" id="div3" runat="server">
                                <div style="text-align: left;" class="field-div1 w12">
                                    <b>Class:</b>
                                </div>
                                <div class="field-div1 w60" style="text-align: left;">
                                    <asp:Label ID="lblclass" runat="server" Text=""></asp:Label>
                                   
                                </div>
                            </div>
                            <div class="row-div clearfix" id="div4" runat="server">
                                <div style="text-align: left;" class="field-div1 w12">
                                    <b>Class Registration:</b>
                                </div>
                                <div class="field-div1 w60" style="text-align: left;">
                                    <asp:Label ID="lblclassregi" runat="server" Text=""></asp:Label>
                                   
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div style="text-align: left;" class="field-div1 w12">
                                    <b>Comments:</b>
                                </div>
                                <div class="field-div1 w60" style="text-align: left;">
                                    <asp:TextBox ID="txtcomment" Width="350px" MaxLength="50" runat="server" Height="150"
                                        TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row-div clearfix" id="div1" runat="server">
                    <div style="text-align: left;" class="field-div1 w12">
                        &nbsp;
                    </div>
                    <div class="field-div1 w60" style="text-align: right;">
                        <asp:Button ID="btnBack" runat="server" Text="Cancel" />&nbsp;
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="VGG" />&nbsp<asp:Label
                            ID="lblsubmit" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                

                <telerik:RadWindow ID="RadAlert" runat="server" Width="350px" Modal="True"
                            BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                            Title="Life Long Learning Request Application" Behavior="None" Height="150px">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                                    padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                                    <tr>
                                        <td align="center">
                                              <asp:Label ID="lblsuccess" runat="server" Text=""></asp:Label>
                                             
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
     <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
</div>
