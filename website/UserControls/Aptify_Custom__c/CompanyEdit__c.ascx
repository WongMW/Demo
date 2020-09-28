<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CompanyEdit__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.CompanyEdit__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" TagName="Topiccode" Src="~/UserControls/Aptify_General/TopicCodeViewer.ascx" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/PendingChangesDetails__c.ascx"
    TagName="PendingChangesDetailsControl" TagPrefix="uc4" %>
<%-- 'Anil B for issue 14320 on 09/04/2013
      Used update panel to avaid full loading of full control--%>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div>
            <table width="100%" cellpadding="0" cellspacing="0">
                <div id="divPendingChange" runat="server">
                    <tr>
                        <td class="tdPersonalInfo" colspan="3">
                            <table width="100%" cellpadding="0" cellspacing="0" style="height: auto">
                                <tr>
                                    <td width="5%">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        Pending Changes Details
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table width="100%" cellpadding="0" cellspacing="0" style="height: auto">
                                <tr>
                                    <td>
                                        <uc4:PendingChangesDetailsControl ID="PendingChangesDetails1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </div>
                <tr>
                    <td class="tdEditContactInfoCompany" colspan="3">
                        <table width="100%" cellpadding="0" cellspacing="0" height="30px">
                            <tr>
                                <td width="4%">
                                    &nbsp;
                                </td>
                                <td align="left" class="tdCompanyEditHeader">
                                   Company Address jim lobo
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Edit.png" />
                                    <asp:LinkButton ID="contact" ForeColor="#d07b0c" runat="server" Text="Edit" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <%--  end--%>
                </tr>
                <tr>
                    <td id="tdBusinessAdd" runat="server" style="font-weight: bold; padding-left: 10px;
                        padding-top: 5px;">
                        Street Address Jim Lobo:
                    </td>
                    <td id="tdHomeAdd" runat="server" class="LeftColumnAdminEditProfile" style="font-weight: bold;">
                        Billing Address:
                    </td>
                    <td id="tdPoboxAdd" runat="server" class="LeftColumnAdminEditProfile" style="font-weight: bold;">
                        PO Box Address:
                    </td>
                </tr>
                <tr>
                    <td id="tdStreetAddVal" runat="server" style="padding-left: 10px; vertical-align: top;">
                        <asp:Label ID="StreetAddressval" runat="server"></asp:Label>
                    </td>
                    <td class="RightColumn tdCompanyAddressAlign" id="tdPoboxAddVal" runat="server">
                        <asp:Label ID="BillingAddressval" runat="server"></asp:Label>
                    </td>
                    <td class="RightColumn tdCompanyAddressAlign" id="tdBillingAddVal" runat="server">
                        <asp:Label ID="PoboxAddressval" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="padding-left: 10px;" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="font-weight: bold;" runat="server" id="tdPhone" class="LeftColumnAdminEditCompany">
                        (Area Code) Phone:
                    </td>
                    <td class="RightColumn">
                        <asp:Label ID="lblphoneVal" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold" runat="server" id="tdFax" class="LeftColumnAdminEditCompany">
                        (Area Code) Fax:
                    </td>
                    <td class="RightColumn">
                        <asp:Label ID="lblFaxVal" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="tremail" runat="server">
                    <td style="font-weight: bold" runat="server" id="tdemail" class="LeftColumnAdminEditCompany">
                        Email:
                    </td>
                    <td class="RightColumn">
                        <asp:Label ID="lblPrimaryEmail" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trWebSite" runat="server">
                    <td style="font-weight: bold;" runat="server" id="tdWebsite" class="LeftColumnAdminEditCompany">
                        Website:
                    </td>
                    <td class="RightColumn">
                        <asp:Label ID="lblWebsite" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="width: 20px;">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width: 100%;">
                <tr runat="server" id="trWebAccount">
                    <td>
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="tdMemberInfoCompany" colspan="2">
                                                <table width="100%" cellpadding="0" cellspacing="0" height="30px">
                                                    <tr>
                                                        <td width="5%">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdCompanyEditHeader">
                                                            Membership Information
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LeftColumnAdminEditCompany" style="padding-left: 10px;">
                                                <asp:Label ID="lblmembershipType" runat="server">Membership Type:</asp:Label>
                                            </td>
                                            <td class="RightColumn">
                                                <asp:Label ID="lblMemberTypeVal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LeftColumnAdminEditCompany" style="padding-left: 10px;">
                                                <asp:Label ID="lblStartDate" runat="server">Start Date:</asp:Label>
                                            </td>
                                            <td class="RightColumn">
                                                <asp:Label ID="lblStartDateVal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LeftColumnAdminEditCompany" style="padding-left: 10px;">
                                                <asp:Label ID="lblEndDate" runat="server">End Date:</asp:Label>
                                            </td>
                                            <td class="RightColumn">
                                                <asp:Label ID="lblEndDateVal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LeftColumnAdminEditCompany" style="padding-left: 10px;">
                                                <asp:Label ID="lblStatus" runat="server">Status:</asp:Label>
                                            </td>
                                            <td class="RightColumn">
                                                <asp:Label ID="lblStatusVal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="tdEditTopicofInterestInfo">
                                        <table width="100%" cellpadding="0" cellspacing="0" height="30px">
                                            <tr>
                                                <td width="4%">
                                                    &nbsp;
                                                </td>
                                                <%--Amruta IssueID 14320--%>
                                                <td align="left" class="tdCompanyEditHeader">
                                                    <asp:Label ID="lbltopicofInterest" runat="server" Text="Topics of Interest" ToolTip="This area displays the top level topics currently associated with your company. To see a complete list of topics or to change your company's selections, click Edit to load the topic tree view."></asp:Label>
                                                </td>
                                                <td align="right" style="padding-right: 5px;">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Edit.png" />
                                                    <asp:LinkButton ID="btnTopicIntrest" runat="server" ForeColor="#d07b0c" Text="Edit"
                                                        Style="margin-left: 0px" ToolTip="This area displays the top level topics currently associated with your company. To see a complete list of topics or to change your company's selections, click Edit to load the topic tree view." />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;">
                                        <asp:Label ID="lblTopicIntrest" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <rad:RadWindow ID="RadWindow1" runat="server" Width="600px" Height="450px" Modal="True"
                    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                    Title="Address Information" Skin="Default">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updatepnl" runat="server">
                            <ContentTemplate>
                                <div style="background-color: #f4f3f1;">
                                    <table width="100%" cellpadding="0" cellspacing="0" style="height: 100%; padding-left: 5px;
                                        padding-right: 5px;">
                                        <tr>
                                            <td colspan="3">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="5%">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <asp:Label ID="lblPendingChangesMsg" runat="server" align="center"></asp:Label>
                                        </tr>
                                        <tr id="trCompanyName" runat="server">
                                            <td id="TdCmpname" class="rightAlign" runat="server">
                                                <asp:Label ID="lblCompanyName" runat="server" Font-Bold="true">Company Name:</asp:Label>
                                            </td>
                                            <td id="TdCompname" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                                <asp:TextBox ID="txtCompanyName" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="rightAlign" style="font-weight: bold">
                                                Address Type:
                                            </td>
                                            <td class="RightColumn" align="left" style="padding-bottom: 5px">
                                                <asp:DropDownList ID="ddlAddressType" CssClass="cmbUserProfileBussinessAdress" runat="server"
                                                    AutoPostBack="True" OnSelectedIndexChanged="selectedindex">
                                                    <asp:ListItem Text="Street Address" Value="Street Address" runat="server"></asp:ListItem>
                                                    <asp:ListItem Text="Billing Address" Value="Billing Address" runat="server"></asp:ListItem>
                                                    <asp:ListItem Text="PO Box Address" Value="PObox Adress" runat="server"></asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;<asp:CheckBox ID="chkPrefAddress" CssClass="cb" runat="server" Text="Preferred Address"
                                                    AutoPostBack="True" Visible="false" />
                                            </td>
                                            <td class="RightColumn">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <!-- Address Line  Rows -->
                                        <tr id="trAddressLine1" runat="server">
                                            <td id="Td1" class="rightAlign" runat="server">
                                                <asp:Label ID="lblAddress" runat="server" Font-Bold="true">Address:</asp:Label>
                                            </td>
                                            <td id="Td2" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                                <asp:TextBox ID="txtAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%-- Amruta , Issue No 14320,used separate controls for each addressline--%>
                                        <tr id="trBillingAddressLine1" runat="server" visible="False">
                                            <td class="rightAlign" runat="server">
                                                <asp:Label ID="lblBillingAddress" runat="server" Font-Bold="true">Address:</asp:Label>
                                            </td>
                                            <td class="RightColumn tdRightColumnCompanyEdit" colspan="2" runat="server">
                                                <asp:TextBox ID="txtBillingAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trPOBoxAddressLine1" runat="server" visible="False">
                                            <td class="rightAlign" runat="server">
                                                <asp:Label ID="lblPOBoxAddress" runat="server" Font-Bold="true">Address:</asp:Label>
                                            </td>
                                            <td class="RightColumn tdRightColumnCompanyEdit" colspan="2" runat="server">
                                                <asp:TextBox ID="txtPOBoxAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <!-- Address Line 2 Rows -->
                                        <tr id="trAddressLine2" runat="server">
                                            <td id="Td9" class="rightAlign" runat="server">
                                            </td>
                                            <td id="Td10" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px;
                                                padding-left: 2px;">
                                                <asp:TextBox ID="txtAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trBillingAddressLine2" runat="server" visible="False">
                                            <td class="rightAlign" runat="server">
                                            </td>
                                            <td class="RightColumn tdRightColumnCompanyEdit" colspan="2" runat="server">
                                                <asp:TextBox ID="txtBillingAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trPOBoxAddressLine2" runat="server" visible="False">
                                            <td class="rightAlign" runat="server">
                                            </td>
                                            <td class="RightColumn tdRightColumnCompanyEdit" colspan="2" runat="server">
                                                <asp:TextBox ID="txtPOBoxAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <!-- Address Line 3 Rows -->
                                        <tr id="trAddressLine3" runat="server">
                                            <td id="Td17" class="rightAlign" runat="server">
                                            </td>
                                            <td id="Td18" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px;
                                                padding-left: 2px;">
                                                <asp:TextBox ID="txtAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trBillingAddressLine3" runat="server" visible="False">
                                            <td class="rightAlign" runat="server">
                                            </td>
                                            <td class="RightColumn tdRightColumnCompanyEdit" colspan="2" runat="server">
                                                <asp:TextBox ID="txtBillingAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trPOBoxAddressLine3" runat="server" visible="False">
                                            <td class="rightAlign" runat="server">
                                            </td>
                                            <td class="RightColumn tdRightColumnCompanyEdit" colspan="2" runat="server">
                                                <asp:TextBox ID="txtPOBoxAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trCity" runat="server">
                                            <td id="Td25" class="rightAlign" runat="server">
                                                <asp:Label ID="lblCityStateZip" runat="server" Font-Bold="true">City:</asp:Label>
                                            </td>
                                            <td id="Td26" class="RigthColumnContactBold" runat="server" style="padding-bottom: 5px;
                                                padding-left: 2px;">
                                                <asp:TextBox ID="txtCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                                <span class="SpanState">
                                                    <asp:Label ID="Label5" runat="server" Font-Bold="true">State:</asp:Label>
                                                </span>
                                                <asp:DropDownList Width="163px" ID="cmbState" CssClass="cmbUserProfileState" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trBillingCity" runat="server" visible="False">
                                            <td class="rightAlign tdLeftColumnCompanyEdit" colspan="1" runat="server">
                                                <asp:Label ID="lblBillingCity" runat="server" Font-Bold="true">City:</asp:Label>
                                            </td>
                                            <td class="RigthColumnContactBold tdRightColumnCompanyEdit" runat="server">
                                                <asp:TextBox ID="txtBillingCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                                <span class="SpanState">
                                                    <asp:Label ID="lblBillingState" runat="server" Font-Bold="true">State:</asp:Label>
                                                </span>
                                                <asp:DropDownList ID="cmbBillingState" CssClass="cmbUserProfileState" runat="server"
                                                    Width="163px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trPOBoxCity" runat="server" visible="False">
                                            <td class="rightAlign tdLeftColumnCompanyEdit" colspan="1" runat="server">
                                                <asp:Label ID="lblPOBoxCity" runat="server" Font-Bold="true">City:</asp:Label>
                                            </td>
                                            <td class="RigthColumnContactBold tdRightColumnCompanyEdit" runat="server">
                                                <asp:TextBox ID="txtPOBoxCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                                <span class="SpanState">
                                                    <asp:Label ID="lblPOBoxState" runat="server" Font-Bold="true">State:</asp:Label>
                                                </span>
                                                <asp:DropDownList ID="cmbPOBoxState" CssClass="cmbUserProfileState" runat="server"
                                                    Width="163px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trCountry" runat="server">
                                            <td id="Td33" class="rightAlign" colspan="1" runat="server">
                                                <asp:Label ID="lblCountry" runat="server" Font-Bold="true">Country:</asp:Label>
                                            </td>
                                            <td id="Td34" align="left" class="RigthColumnContactBold" runat="server" style="padding-bottom: 5px;
                                                padding-left: 2px;">
                                                <asp:DropDownList ID="cmbCountry" Width="163px" CssClass="cmbUserProfileCountry"
                                                    runat="server" AutoPostBack="True" EnableScreenBoundaryDetection="false" OnSelectedIndexChanged="cmbCountry_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <span class="SpanZipCode">
                                                    <asp:Label ID="Label23" runat="server" Font-Bold="true">ZIP Code:</asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtzip" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trBillingCountry" runat="server" visible="False">
                                            <td class="rightAlign" colspan="1" runat="server">
                                                <asp:Label ID="lblBillingCountry" runat="server" Font-Bold="true">Country:</asp:Label>
                                            </td>
                                            <td class="RigthColumnContactBold tdRightColumnCompanyEdit" runat="server">
                                                <asp:DropDownList ID="cmbBillingCountry" Width="163px" CssClass="cmbUserProfileCountry"
                                                    runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <span class="SpanZipCode">
                                                    <asp:Label ID="lblBillingZipCode" runat="server" Font-Bold="true">ZIP Code:</asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtBillingZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trPOBoxCountry" runat="server" visible="False">
                                            <td class="rightAlign" colspan="1" runat="server">
                                                <asp:Label ID="lblPOBoxCountry" runat="server" Font-Bold="true">Country:</asp:Label>
                                            </td>
                                            <td class="RigthColumnContactBold tdRightColumnCompanyEdit" runat="server">
                                                <asp:DropDownList ID="cmbPOBoxCountry" Width="163px" CssClass="cmbUserProfileCountry"
                                                    runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <span class="SpanZipCode">
                                                    <asp:Label ID="lblPOBoxZipCode" runat="server" Font-Bold="true">ZIP Code:</asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtPOBoxZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server">
                                            <td id="Td3" class="rightAlign" colspan="1" runat="server">
                                                <asp:Label ID="lblEmail" runat="server" Font-Bold="true">Email:</asp:Label>
                                            </td>
                                            <td id="Td4" align="left" class="RigthColumnContactBold" runat="server" style="padding-bottom: 5px;
                                                padding-left: 2px;">
                                                <asp:TextBox ID="txtEmail" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                                <span class="SpanZipCode">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="websiteRadwindow">Website:</asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtWebsite" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="left" class="RigthColumnContactBold" runat="server" style="padding-bottom: 0px;
                                                padding-left: 2px;">
                                                <%--Suraj Issue 15210 ,2/19/13 RegularExpressionValidator validator --%>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                    
                                                    ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ValidationGroup="VldAdressInfo"
                                                    ForeColor="Red"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="rightAlign">
                                                <asp:Label ID="lblPhone" runat="server" Font-Bold="true">(Area Code) Phone:</asp:Label>
                                            </td>
                                            <%--Neha Issue 14750 ,02/26/13, added css for phoneareacode--%>
                                            <td class="RightColumn tdRightColumnAreacodephone">
                                                <rad:RadMaskedTextBox ID="txtPhoneAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                                    runat="server" Mask="(####)" Width="48px">
                                                </rad:RadMaskedTextBox>
                                                <rad:RadMaskedTextBox ID="txtPhone" CssClass="txtUserProfileAreaCode" runat="server"
                                                    Mask="###-####" Width="65px">
                                                </rad:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="rightAlign">
                                                <asp:Label ID="lblFax" runat="server" Font-Bold="true">(Area Code) Fax:</asp:Label>
                                            </td>
                                            <td class="RightColumn">
                                                <rad:RadMaskedTextBox ID="txtFaxAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                                    runat="server" Mask="(####)" Width="48px">
                                                </rad:RadMaskedTextBox>
                                                <rad:RadMaskedTextBox ID="txtFaxPhone" runat="server" CssClass="txtUserProfileAreaCode"
                                                    Mask="###-####" Width="65px">
                                                </rad:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <%-- Amruta IssueID : 14320 --%>
                                <div class="divAddInfoPopup" align="right">
                                    <asp:Button ID="btnsave" Width="70px" Text="Save" runat="server" CssClass="submitBtn"
                                        OnClick="btnsave_Click" ValidationGroup="VldAdressInfo" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" Text="Cancel" Width="70px" runat="server" CssClass="submitBtn"
                                        OnClick="btnCancel_Click" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlAddressType" />
                                <asp:AsyncPostBackTrigger ControlID="cmbCountry" />
                                <asp:AsyncPostBackTrigger ControlID="cmbState" />
                                <asp:PostBackTrigger ControlID="btnsave" />
                                <asp:PostBackTrigger ControlID="btnCancel" />
                                <asp:AsyncPostBackTrigger ControlID="cmbBillingCountry" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cmbPOBoxCountry" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </rad:RadWindow>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <rad:RadWindow ID="radtopicintrest" runat="server" Width="500px" Height="350px" Modal="True"
                    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                    Title="Topics of Interest" Behavior="None" IconUrl="~/Images/topic-of-int.png"
                    Skin="Default">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                            height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                            <tr>
                                <td>
                                    <cc2:Topiccode ID="TopiccodeViewer" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="tdButtonPadding">
                                    <asp:Button ID="btnSaveIntrest" runat="server" Text="Save" Width="70px" class="submitBtn"
                                        OnClick="btnSaveIntrest_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelIntrest" runat="server" Width="70px" Text="Cancel" class="submitBtn"
                                        OnClick="btnCancelIntrest_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </rad:RadWindow>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ContentTemplate>
</asp:UpdatePanel>
<cc1:User ID="User1" runat="server"></cc1:User>
