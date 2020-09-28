<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CTCDesignationApp__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CTCDesignationApp__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1000px;">
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
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>Student Number:</b> 
                            </div>
                            <div class="field-div1 w60">
                               <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>Student Name:</b>
                            </div>
                            <div class="field-div1 w60">
                           <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>Email:</b>
                            </div>
                            <div class="field-div1 w60">
                               <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>CTC Designation Product Name:</b> 
                            </div>
                            <div class="field-div1 w60">
                               &nbsp;<asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>CTC Designation Product Price:</b> 
                            </div>
                            <div class="field-div1 w60">
                                &nbsp;<asp:Label ID="lblCurrency" runat="server"
                                    Text=""> </asp:Label><asp:Label ID="lblProductPrice" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>Status:</b> 
                            </div>
                            <div class="field-div1 w60">
                              <asp:Label ID="lblStatus" runat="server" Text="In Progress"></asp:Label>
                            </div>
                        </div>
                           <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>Comments:</b> 
                            </div>
                            <div class="field-div1 w60">
                              <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="100px"
                                        Style="resize: none" width="300px"></asp:TextBox>
                            </div>
                        </div>
                          <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w10">
                                &nbsp;
                            </div>
                            <div class="field-div1 w60">
                                <uc1:CreditCard ID="CreditCard" runat="server" />
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                &nbsp;
                            </div>
                            <div class="field-div1 w60">
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="25px" CausesValidation="false" />
                                 
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"   Height="25px" />
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
            <telerik:RadWindow ID="radWindow" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="CTC Designation Application" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
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
    </asp:UpdatePanel>
</div>
