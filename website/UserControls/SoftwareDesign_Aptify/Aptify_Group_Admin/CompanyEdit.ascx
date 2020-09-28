<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/CompanyEdit.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.CompanyEdit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" TagName="Topiccode" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/TopicCodeViewer.ascx" %>
<%-- 'Anil B for issue 14320 on 09/04/2013
      Used update panel to avaid full loading of full control--%>

<asp:UpdatePanel ID="UpdatePanel2" runat="server" class="edit-company-address">
    <ContentTemplate>
        <div class="cai-form">
            <span class="form-title">Company Address </span>

            <div class="cai-form-content">
                <div class="edit-content-button">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Edit.png" />
                    <asp:LinkButton ID="contact" ForeColor="#d07b0c" runat="server" Text="Edit" />
                </div>

                <span id="tdBusinessAdd" runat="server" class="label-title">Street Address: </span>
                <span id="tdHomeAdd" runat="server" class="label-title">Billing Address:</span>
                <span id="tdPoboxAdd" runat="server" class="label-title">PO Box Address:</span>


                <div id="tdStreetAddVal" runat="server" style="vertical-align: top;">
                    <asp:Label ID="StreetAddressval" runat="server"></asp:Label>
                </div>

                <div id="tdPoboxAddVal" runat="server">
                    <asp:Label ID="BillingAddressval" runat="server"></asp:Label>
                </div>

                <div id="tdBillingAddVal" runat="server">
                    <asp:Label ID="PoboxAddressval" runat="server"></asp:Label>
                </div>

                <span runat="server" id="tdPhone" class="label-title">(Area Code) Phone:</span>
                <asp:Label ID="lblphoneVal" runat="server"></asp:Label>

                <span runat="server" id="tdFax" class="label-title">(Area Code) Fax:</span>
                <asp:Label ID="lblFaxVal" runat="server"></asp:Label>

                <div id="tremail" runat="server">
                    <span runat="server" id="tdemail" class="label-title">Email:
                    </span>
                    <asp:Label ID="lblPrimaryEmail" runat="server"></asp:Label>

                </div>
                <div id="trWebSite" runat="server">
                    <div runat="server" id="tdWebsite" class="label-title">Website:</div>
                    <asp:Label ID="lblWebsite" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <div class="cai-form">
            <span class="form-title">Membership Information</span>
            <div class="cai-form-content">
                <div runat="server" id="trWebAccount">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="lblmembershipType" runat="server" CssClass="label-title">Membership Type:</asp:Label>
                                <asp:Label ID="lblMemberTypeVal" runat="server"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblStartDate" runat="server" CssClass="label-title">Start Date:</asp:Label>
                                <asp:Label ID="lblStartDateVal" runat="server"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblEndDate" runat="server" CssClass="label-title">End Date:</asp:Label>
                                <asp:Label ID="lblEndDateVal" runat="server"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblStatus" runat="server" CssClass="label-title">Status:</asp:Label>
                                <asp:Label ID="lblStatusVal" runat="server"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="Label31" runat="server" CssClass="label-title">Membership Number:</asp:Label>
                                <asp:Label ID="lblMembershipNumber" runat="server"></asp:Label>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="txtUserID" EventName="TextChanged" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div>
                    <asp:Label ID="lbltopicofInterest" runat="server" CssClass="label-title" Text="Topics of Interest" ToolTip="This area displays the top level topics currently associated with your company. To see a complete list of topics or to change your company's selections, click Edit to load the topic tree view."></asp:Label>

                    <div class="edit-content-button">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Edit.png" />
                        <asp:LinkButton ID="btnTopicIntrest" runat="server" ForeColor="#d07b0c" Text="Edit"
                            Style="margin-left: 0px" ToolTip="This area displays the top level topics currently associated with your company. To see a complete list of topics or to change your company's selections, click Edit to load the topic tree view." />
                    </div>

                    <asp:Label ID="lblTopicIntrest" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <rad:RadWindow ID="RadWindow1" runat="server" Modal="True"
                    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" Title="Address Information" Skin="Default" Width="400px" Height="500px">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updatepnl" runat="server">
                            <ContentTemplate>
                                <div class="cai-form">
                                    <span class="label-title">Address Type:</span>
                                    <asp:DropDownList ID="ddlAddressType" CssClass="cmbUserProfileBussinessAdress" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="selectedindex">
                                        <asp:ListItem Text="Street Address" Value="Street Address" runat="server"></asp:ListItem>
                                        <asp:ListItem Text="Billing Address" Value="Billing Address" runat="server"></asp:ListItem>
                                        <asp:ListItem Text="PO Box Address" Value="PObox Adress" runat="server"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="chkPrefAddress" CssClass="cb" runat="server" Text="Preferred Address"
                                        AutoPostBack="True" Visible="false" />


                                    <div id="trAddressLine1" runat="server">
                                        <span class="label-title">Address:</span>
                                        <div id="Td2" runat="server">
                                            <asp:TextBox ID="txtAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="trBillingAddressLine1" runat="server" visible="False">
                                        <asp:Label ID="lblBillingAddress" runat="server" class="label-title">Address:</asp:Label>
                                        <asp:TextBox ID="txtBillingAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>

                                    <div id="trPOBoxAddressLine1" runat="server" visible="False">
                                        <asp:Label ID="lblPOBoxAddress" runat="server" class="label-title">Address:</asp:Label>
                                        <asp:TextBox ID="txtPOBoxAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>
                                    <!-- Address Line 2 Rows -->
                                    <div id="trAddressLine2" runat="server">
                                        <div id="Td9" class="label-title" runat="server">
                                        </div>
                                        <div id="Td10" runat="server">
                                            <asp:TextBox ID="txtAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="trBillingAddressLine2" runat="server" visible="False">
                                        <asp:TextBox ID="txtBillingAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>

                                    <div id="trPOBoxAddressLine2" runat="server" visible="False">
                                        <asp:TextBox ID="txtPOBoxAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>

                                    <!-- Address Line 3 Rows -->
                                    <div id="trAddressLine3" runat="server">
                                        <div id="Td17" class="label-title" runat="server">
                                        </div>
                                        <div id="Td18" runat="server">
                                            <asp:TextBox ID="txtAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="trBillingAddressLine3" runat="server" visible="False">
                                        <asp:TextBox ID="txtBillingAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>

                                    <div id="trPOBoxAddressLine3" runat="server" visible="False">
                                        <asp:TextBox ID="txtPOBoxAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>

                                    <div id="trCity" runat="server">
                                        <div id="Td25" runat="server">
                                            <asp:Label ID="lblCityStateZip" runat="server" CssClass="label-title">City:</asp:Label>
                                        </div>
                                        <div id="Td26" runat="server">
                                            <asp:TextBox ID="txtCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                            <span class="SpanState">
                                                <asp:Label ID="Label5" runat="server" CssClass="label-title">State:</asp:Label>
                                            </span>
                                            <asp:DropDownList ID="cmbState" CssClass="cmbUserProfileState" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="trBillingCity" runat="server" visible="False">
                                        <asp:Label ID="lblBillingCity" runat="server" CssClass="label-title">City:</asp:Label>
                                        <asp:TextBox ID="txtBillingCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                        <span class="SpanState">
                                            <asp:Label ID="lblBillingState" runat="server" CssClass="label-title">State:</asp:Label>
                                        </span>
                                        <asp:DropDownList ID="cmbBillingState" CssClass="cmbUserProfileState" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                    <div id="trPOBoxCity" runat="server" visible="False">
                                        <asp:Label ID="lblPOBoxCity" runat="server" CssClass="label-title">City:</asp:Label>
                                        <asp:TextBox ID="txtPOBoxCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                        <span class="SpanState">
                                            <asp:Label ID="lblPOBoxState" runat="server" CssClass="label-title">State:</asp:Label>
                                        </span>
                                        <asp:DropDownList ID="cmbPOBoxState" CssClass="cmbUserProfileState" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                    <div id="trCountry" runat="server">
                                        <div id="Td33" class="rightAlign" colspan="1" runat="server">
                                            <asp:Label ID="lblCountry" runat="server" CssClass="label-title">Country:</asp:Label>
                                        </div>
                                        <div id="Td34" align="left" class="RigthColumnContactBold" runat="server" style="padding-bottom: 5px; padding-left: 2px;">
                                            <asp:DropDownList ID="cmbCountry" CssClass="cmbUserProfileCountry"
                                                runat="server" AutoPostBack="True" EnableScreenBoundaryDetection="false" OnSelectedIndexChanged="cmbCountry_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <span class="SpanZipCode">
                                                <asp:Label ID="Label23" runat="server" CssClass="label-title">ZIP Code:</asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtzip" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div id="trBillingCountry" runat="server" visible="False">
                                        <div class="rightAlign" colspan="1" runat="server">
                                            <asp:Label ID="lblBillingCountry" runat="server" CssClass="label-title">Country:</asp:Label>
                                        </div>
                                        <div class="RigthColumnContactBold tdRightColumnCompanyEdit" runat="server">
                                            <asp:DropDownList ID="cmbBillingCountry" CssClass="cmbUserProfileCountry"
                                                runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <span class="SpanZipCode">
                                                <asp:Label ID="lblBillingZipCode" runat="server" CssClass="label-title">ZIP Code:</asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtBillingZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="trPOBoxCountry" runat="server" visible="False">
                                        <div class="rightAlign" colspan="1" runat="server">
                                            <asp:Label ID="lblPOBoxCountry" runat="server" CssClass="label-title">Country:</asp:Label>
                                        </div>
                                        <div class="RigthColumnContactBold tdRightColumnCompanyEdit" runat="server">
                                            <asp:DropDownList ID="cmbPOBoxCountry" CssClass="cmbUserProfileCountry"
                                                runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <span class="SpanZipCode">
                                                <asp:Label ID="lblPOBoxZipCode" runat="server" CssClass="label-title">ZIP Code:</asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtPOBoxZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="tr1" runat="server">
                                        <div id="Td3" class="rightAlign" colspan="1" runat="server">
                                            <asp:Label ID="lblEmail" runat="server" class="label-title">Email:</asp:Label>
                                        </div>

                                        <div id="Td4" align="left" class="RigthColumnContactBold" runat="server" style="padding-bottom: 5px; padding-left: 2px;">
                                            <asp:TextBox ID="txtEmail" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                            <span class="SpanZipCode">
                                                <asp:Label ID="Label2" runat="server" CssClass="label-title">Website:</asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtWebsite" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <%--Suraj Issue 15210 ,2/19/13 RegularExpressionValidator validator --%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                        ValidationExpression="[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+(?:[A-Z]{2}|com|COM|org|ORG|net|NET|edu|EDU|gov|GOV|mil|MIL|biz|BIZ|info|INFO|mobi|MOBI|name|NAME|aero|AERO|asia|ASIA|jobs|JOBS|museum|MUSEUM|in|IN|co|CO)\b"
                                        ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ValidationGroup="VldAdressInfo"
                                        ForeColor="Red"></asp:RegularExpressionValidator>

                                    <asp:Label ID="lblPhone" runat="server" CssClass="label-title">(Area Code) Phone:</asp:Label>
                                    <rad:RadMaskedTextBox ID="txtPhoneAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                        runat="server" Mask="(####)">
                                    </rad:RadMaskedTextBox>
                                    <rad:RadMaskedTextBox ID="txtPhone" CssClass="txtUserProfileAreaCode" runat="server"
                                        Mask="###-####">
                                    </rad:RadMaskedTextBox>

                                    <asp:Label ID="lblFax" runat="server" CssClass="label-title">(Area Code) Fax:</asp:Label>
                                    <rad:RadMaskedTextBox ID="txtFaxAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                        runat="server" Mask="(####)">
                                    </rad:RadMaskedTextBox>
                                    <rad:RadMaskedTextBox ID="txtFaxPhone" runat="server" CssClass="txtUserProfileAreaCode"
                                        Mask="###-####">
                                    </rad:RadMaskedTextBox>

                                    <div class="actions">
                                        <asp:Button ID="btnsave" Text="Save" runat="server" CssClass="submitBtn"
                                            OnClick="btnsave_Click" ValidationGroup="VldAdressInfo" />
                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="submitBtn"
                                            OnClick="btnCancel_Click" />
                                    </div>
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
                <rad:RadWindow ID="radtopicintrest" runat="server" Width="400px" Height="350px" Modal="True"
                    BackColor="" VisibleStatusbar="False" Behaviors="None"
                    Title="Topics of Interest" Behavior="None" IconUrl="~/Images/topic-of-int.png"
                    Skin="Default">
                    <ContentTemplate>
                        <cc2:Topiccode ID="TopiccodeViewer" runat="server" />
                        <div class="tdButtonPadding">
                            <asp:Button ID="btnSaveIntrest" runat="server" Text="Save" class="submitBtn"
                                OnClick="btnSaveIntrest_Click" />
                            <asp:Button ID="btnCancelIntrest" runat="server" Text="Cancel" class="submitBtn"
                                OnClick="btnCancelIntrest_Click" />
                        </div>
                    </ContentTemplate>
                </rad:RadWindow>
            </ContentTemplate>
    </asp:UpdatePanel>
    </ContentTemplate>
</asp:UpdatePanel>
<cc1:User ID="User1" runat="server"></cc1:User>
