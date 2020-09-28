<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LLLExpressInterest__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLExpressInterest__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="dvUpdateProgress"  style="overflow:visible;"> 
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server"
        DisplayAfter="0"> 
        <ProgressTemplate> 
            <div class="dvProcessing" style="height:550px;"> 
                <table class="tblFullHeightWidth"> 
                    <tr> 
                        <td class="tdProcessing" style="vertical-align:middle" > 
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
                            <div style="text-align: left;" class="field-div1 w10">
                                <span style="color: Red;">*</span><b>First Name:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtFirstName" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                                    ErrorMessage="First Name Required" Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w10">
                                <span style="color: Red;">*</span><b>Last Name:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtLastName" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                                    ErrorMessage="Last Name Required" Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w10">
                                <span style="color: Red;">*</span><b>Email:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtEmail" Width="350px" MaxLength="50" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Email Required" Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
                                    
                                    ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w10">
                                <span style="color: Red;">*</span><b>Qualification:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:DropDownList ID="cmbLLLCourseCategory" runat="server"  Width="350px" />
                                <asp:RequiredFieldValidator InitialValue="Select Qualification" ID="Req_ID"
                                    Display="Dynamic" runat="server" ControlToValidate="cmbLLLCourseCategory"
                                    Text="" ErrorMessage="Qualification Required" CssClass="required-label"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                           
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left; font-weight: normal;" class="label-div w10">
                              
                                   <b>Comments:</b>
                               
                            </div>
                              <div class="field-div1 w60" style="text-align: left;">
                                 <asp:TextBox ID="txtComments" Width="350px" MaxLength="50" runat="server" Height="150" TextMode="MultiLine" Style="resize: none;margin-left: -10px"></asp:TextBox>
                            </div>
                             
                        </div>
                  
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left; margin-left: 100px;" class="field-div1 w60">
                           <asp:Button ID="btnSubmit" runat="server" Text="Submit"  Height="25px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <telerik:RadWindow ID="radWindow" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Expression of Interest " Behavior="None" Height="150px">
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
