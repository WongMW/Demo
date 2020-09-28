<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/ProfileTest__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.ProfileTest__c" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/CompanyContact__c.ascx" TagName="CompanyContact__c"
    TagPrefix="uc3" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagName="SyncProfile" TagPrefix="uc1" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Customer_Service/SynchProfile.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/TopicCodeControl__c.ascx" TagName="TopicCodeControl"
    TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/PendingChangesDetails__c.ascx"
    TagName="PendingChangesDetailsControl" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/AdditionalOrganizations__c.ascx"
    TagName="AdditionalOrganization" TagPrefix="uc4" %>

<%@ Register TagPrefix="uc1" TagName="SocialNetworkingIntegrationControlSF4" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/SocialNetworkingIntegrationControlSF4.ascx" %>
<script type="text/javascript" language="javascript">

    function ShowvalidationErrorMsg() {
        var cmpValidator = document.getElementById('<%=CompareValidator.ClientID%>');
        if (document.getElementById('<%= txtoldpassword.ClientID %>').value.length == 0 ||
            window.document.getElementById('<%= txtNewPassword.ClientID %>').value.length == 0
            || window.document.getElementById('<%= txtRepeat.ClientID %>').value.length == 0) {
            window.document.getElementById('<%= lblErrormessage.ClientID %>').innerHTML = "All the above fields are mandatory.";
            window.document.getElementById('<%= lblErrormessage.ClientID %>').style.display = "block";
            window.document.getElementById('<%= lblerrorLength.ClientID %>').style.display = "none";
            ValidatorEnable(cmpValidator, false);
            return false;
        }
        else {

            if (window.document.getElementById('<%= txtNewPassword.ClientID %>').value != window.document.getElementById('<%= txtRepeat.ClientID %>').value) {
                ValidatorEnable(cmpValidator, true);
                window.document.getElementById('<%= lblErrormessage.ClientID %>').style.display = "none";
                return false;
            }
        }
    }

    function ShowChangePasswordWindow() {
        var radwindow1 = $find('<%=radwinPassword.ClientID%>');
        radwindow1.set_modal(true);
        radwindow1.show();
    }

    function HideChangePasswordWindow() {
        var radwindow1 = $find('<%=radwinPassword.ClientID%>');
        radwindow1.hide();
        radwindow1.set_modal(false);
    }

    function ShowUpdateHomeAddress() {
        var radwindow1 = $find('<%=radUpdateHomeAddress.ClientID%>');
        radwindow1.set_modal(true);
        radwindow1.show();
    }

    function HideUpdateHomeAddress() {
        var radwindow1 = $find('<%=radUpdateHomeAddress.ClientID%>');
        radwindow1.hide();
        radwindow1.set_modal(false);
    }

    function ShowChangeCompany() {

        var radwindow1 = $find('<%=radChangeCompany.ClientID%>');
        radwindow1.set_modal(true);
        radwindow1.show();
    }

    function HideChangeCompany() {
        var radwindow1 = $find('<%=radChangeCompany.ClientID%>');
        radwindow1.hide();
        radwindow1.set_modal(false);
    }
    function ClientItemSelected(sender, e) {
        // alert(e.get_value());
        document.getElementById("<%=hfCompanyID.ClientID %>").value = e.get_value();
    }
    function companyitemselecting(sender, e) {

        document.getElementById('<%=hfCompanyID.ClientID%>').value = "-1"
    }


    function ShowAddNewCompany() {
        HideChangeCompany();
        var radwindow1 = $find('<%=radAddNewCompany.ClientID%>');
        radwindow1.set_modal(true);
        radwindow1.show();
    }

    function HideAddNewCompany() {
        var radwindow1 = $find('<%=radAddNewCompany.ClientID%>');
        radwindow1.hide();
        radwindow1.set_modal(false);
    }

    function ShowDeleteCompany() {
        //HideChangeCompany();
        var radwindow1 = $find('<%=radDeleteCompany.ClientID%>');
        radwindow1.set_modal(true);
        radwindow1.show();
    }
    function HideDeleteCompany() {
        var radwindow1 = $find('<%=radDeleteCompany.ClientID%>');
        radwindow1.hide();
        radwindow1.set_modal(false);
    }
</script>
<style type="text/css">
    .watermarked {
        background-color: #FFFFFF;
        border: 0px solid #BEBEBE;
        color: #BEBEBE;
        padding: 2px 0 0 2px;
    }

    .tooltip {
        display: inline;
        position: relative;
    }

        .tooltip:hover:after {
            background: #8f8e8e;
            background: rgba(143,142,142,.95);
            border-radius: 5px;
            bottom: 26px;
            color: #fff;
            content: attr(title);
            left: 20%;
            padding: 5px 15px;
            position: absolute;
            z-index: 98;
            width: 220px;
            font-size: 14px;
        }

        .tooltip:hover:before {
            border: solid;
            border-color: #8f8e8e transparent;
            border-width: 6px 6px 0 6px;
            bottom: 20px;
            content: "";
            left: 50%;
            position: absolute;
            z-index: 99;
        }

    .qtext {
        font-size: 13px;
        font-weight: lighter;
    }

    label-titleNew {
        padding: 10px 0px;
        font-weight: 600;
        font-size: 16px;
        display: block;
        text-align: left;
    }
</style>

<style type="text/css">
    .autocomplete_completionListElement {
        visibility: hidden;
        margin: 0px !important;
        background-color: white;
        color: windowtext;
        border: buttonshadow;
        border-width: 1px;
        border-style: solid;
        cursor: 'default';
        overflow: scroll;
        height: 300px;
        text-align: left;
        list-style-type: none;
        font-size: 10pt;
    }
</style>
<div id="Container">
    <div id="RightDiv">
        <div id="tblMain" runat="server" width="100%" class="data-form">

            <div class="cai-form">
                <div style="font-weight: bold;">
                    * marks mandatory fields
                </div>
                <div id="divPendingChange" runat="server" class="pending-changes">
                    <span class="form-title">Pending Changes Details </span>
                    <div class="cai-form-content">
                        <uc4:PendingChangesDetailsControl ID="PendingChangesDetails1" runat="server" />
                    </div>
                    <div style="font-weight: bold;">
                        If you change any of the following information, it will get updated after the Institute's approval. Prefix, last name, addresses, and employment status.
                    </div>
                </div>

                <div>
                    <br />
                    <div>
                        <asp:Label ID="lblPasswordsuccess" runat="server" ForeColor="Blue"></asp:Label>
                    </div>
                    <br />
                    <span class="form-title">Personal Information</span>


                    <div class="form-section-half-border">

                        <div runat="server" id="trUserID" visible="false" class="field-group">
                            <asp:HiddenField ID="hfCompanyID" Value="-1" runat="server" />
                            <asp:HiddenField ID="hidCompanyIdOld" Value="-1" runat="server" />
                            <asp:Label ID="Label7" runat="server" Style="font-weight: 600;">User ID:</asp:Label>
                            <asp:Label ID="lblUserID" Text="" Style="font-weight: 600;" runat="server"></asp:Label>
                            <span style="font-weight: 600; padding-left: 135px; cursor: pointer;" onclick="ShowChangePasswordWindow();"><u>Change Password</u></span>
                        </div>
                        <div class="field-group">
                            <asp:Label ID="lblSalutation" runat="server" CssClass="label-title">Prefix<span class="RequiredField">*</span></asp:Label>
                            <asp:DropDownList ID="cmbSalutation" CssClass="txtBoxEditProfileForDropdown" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="cmbSalutation" ID="reqPrefix"
                                ValidationGroup="ProfileControl" ForeColor="Red" ErrorMessage="Prefix Required"
                                InitialValue="" runat="server" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="field-group">
                            <asp:Label ID="lblPreferredSalutation" runat="server" CssClass="label-title">Preferred first name <a href="#" title="Your preferred 
                                first name is the name by which you are known. We will use this name on directory listings and mail or email communications." class="tooltip"><span title="Tooltip" class="qtext"> What's this?</span></a></asp:Label>
                            <asp:TextBox SkinID="RequiredTextBox" CssClass="txtBoxEditProfile" ID="txtPreferredSalutation"
                                runat="server"></asp:TextBox>
                        </div>
                        <div class="field-group">
                            <asp:Label ID="lblName" runat="server" CssClass="label-title">First name(s)</asp:Label>
                            <asp:TextBox SkinID="RequiredTextBox" Enabled="false" CssClass="txtBoxEditProfile" ID="txtFirstName"
                                runat="server"></asp:TextBox>

                        </div>
                        <div class="field-group">
                            <asp:Label ID="Label4" runat="server" CssClass="requiredLabel label-title">Last name <span class="RequiredField">*</span></asp:Label>
                            <asp:TextBox SkinID="RequiredTextBox" ID="txtLastName" CssClass="txtBoxEditProfile"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                                ValidationGroup="ProfileControl" ErrorMessage="Last Name Required" ForeColor="Red"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblTitle" runat="server" CssClass="label-title">Job title</asp:Label>
                            <asp:TextBox SkinID="RequiredTextBox" ID="txtJobTitle" CssClass="txtBoxEditProfile"
                                runat="server"></asp:TextBox>

                        </div>



                        <div class="field-group">
                            <asp:Label ID="lblGender" runat="server" CssClass="label-title">Gender</asp:Label>
                            <asp:TextBox SkinID="RequiredTextBox" ID="txtGender" Enabled="false" CssClass="txtBoxEditProfile"
                                runat="server"></asp:TextBox>
                            <%-- <asp:DropDownList ID="cmbGender" CssClass="txtBoxEditProfileForDropdown" runat="server">
                                <asp:ListItem Value="0">Male</asp:ListItem>
                                <asp:ListItem Value="1">Female</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">Unknown</asp:ListItem>
                            </asp:DropDownList>--%>
                        </div>



                        <div class="field-group">
                            <asp:Label ID="lblDob" CssClass="label-title" runat="server">Date of Birth</asp:Label>

                            <asp:TextBox SkinID="RequiredTextBox" ID="txtDob" Enabled="false" CssClass="txtBoxEditProfile"
                                runat="server"></asp:TextBox>

                        </div>



                    </div>
                    <div class="form-section-half-border">
                        <div runat="server" id="divStatus" visible="false" class="field-group">
                            <asp:Label ID="lblTextStatus" runat="server" Style="font-weight: 600;">Status:</asp:Label>
                            <asp:Label ID="lblStatus" Text="" Style="font-weight: 600;" runat="server"></asp:Label>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblEmploymentStatus" runat="server" CssClass="label-title">Employment status</asp:Label>
                            <asp:DropDownList ID="cmbempstatus" CssClass="txtBoxEditProfileForDropdown" runat="server">
                            </asp:DropDownList>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="Label1" runat="server" CssClass="label-title">Tell us where to contact you</asp:Label>
                        </div>
                        <div class="field-group">
                            <div>
                                <asp:Label ID="lblPhone" CssClass="label-title" Style="padding-left: 105px;" runat="server">(Country code)(Area code) Number</asp:Label>
                            </div>

                            <div>
                                <samp style="font-weight: 600;">Pre. mobile*</samp>
                                <rad:RadMaskedTextBox ID="txtmobileCountryCode" CssClass="txtUserProfileAreaCodeSmall" runat="server"
                                    Mask="(####)" Width="20%">
                                </rad:RadMaskedTextBox>
                                <rad:RadMaskedTextBox ID="txtMobileAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                    runat="server" Mask="(####)" Width="20%">
                                </rad:RadMaskedTextBox>
                                <rad:RadMaskedTextBox ID="txtMobileNumber" CssClass="txtUserProfileAreaCodeSmall" runat="server"
                                    Mask="(#######)" Width="28%">
                                </rad:RadMaskedTextBox>
                            </div>



                            <div>
                                <samp style="font-weight: 600;">Pre.landline</samp>
                                <rad:RadMaskedTextBox ID="txtlandlineCountryCode" CssClass="txtUserProfileAreaCodeSmall" runat="server"
                                    Mask="(####)" Width="20%">
                                </rad:RadMaskedTextBox>
                                <rad:RadMaskedTextBox ID="txtlandlineAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                    runat="server" Mask="(####)" Width="20%">
                                </rad:RadMaskedTextBox>
                                <rad:RadMaskedTextBox ID="txtlandlineNumber" CssClass="txtUserProfileAreaCodeSmall" runat="server"
                                    Mask="(#######)" Width="28%">
                                </rad:RadMaskedTextBox>
                            </div>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblEmail" runat="server" CssClass="label-title">Pre. Email<span class="RequiredField">*</span></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtBoxEditProfile" ValidationGroup="ProfileControl" SkinID="RequiredTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Email Required" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="ProfileControl"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
                                ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format"
                                ForeColor="Red" ValidationGroup="ProfileControl"></asp:RegularExpressionValidator>


                        </div>


                        <div class="field-group">
                            <asp:Label ID="lblJobfunction" runat="server" CssClass="label-title">Job function</asp:Label>
                            <asp:DropDownList ID="cmbJobfunction" CssClass="txtBoxEditProfileForDropdown" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="field-group">
                            <asp:Label ID="lblCountryoforigin" runat="server" CssClass="label-title">Country of origin</asp:Label>
                            <asp:TextBox SkinID="RequiredTextBox" ID="txtCountryoforigin" Enabled="false" CssClass="txtBoxEditProfile"
                                runat="server"></asp:TextBox>

                        </div>


                        <div class="field-group">
                            <br />
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="submitBtn" Text="Submit" ValidationGroup="ProfileControl" />
                        </div>

                    </div>
                    <div>
                        <span class="form-title">Addresses</span>

                        <div class="field-group">
                            <asp:Label ID="Label2" runat="server" Style="font-weight: 600;">Preferred mailing address</asp:Label>
                            <asp:RadioButtonList ID="rblPAddress" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Home </asp:ListItem>
                                <asp:ListItem>Work</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="field-group">
                        <span class="label-title">Home address</span>
                    </div>
                    <div class="form-section-half-border">
                        <div class="field-group">
                            <div id="txtHomeAddress" runat="server">
                            </div>
                        </div>
                    </div>
                    <div class="form-section-half-border">
                        <div class="field-group">
                            <input id="btnHomeEdit" type="button" onclick="ShowUpdateHomeAddress()" class="submitBtn" value="Edit" />
                        </div>
                    </div>



                    <div class="field-group">
                        <span class="label-title">Primary business</span>
                    </div>
                    <div class="form-section-half-border">
                        <div class="field-group">
                            <div id="divPrimarybusiness" runat="server">
                            </div>
                        </div>
                    </div>
                    <div class="form-section-half-border">
                        <div class="field-group">
                            <input id="btnEditPrimarybusiness" type="button" onclick="ShowChangeCompany()" class="submitBtn" value="Edit" />
                        </div>
                    </div>


                    <div class="field-group">

                        <div>
                            <span class="label-title">Other business address</span>
                            <asp:DataList ID="lstAddress" runat="server" RepeatColumns="2" Width="100%">
                                <SelectedItemStyle></SelectedItemStyle>
                                <HeaderTemplate>
                                    <font class="label-title"></font>
                                </HeaderTemplate>
                                <AlternatingItemStyle></AlternatingItemStyle>
                                <ItemStyle Width="350px" VerticalAlign="Bottom" HorizontalAlign="NotSet"></ItemStyle>
                                <ItemTemplate>
                                    <div style="vertical-align: bottom;" class="divstylechangeAddress">
                                        <font size="2">
                                <b><%# DataBinder.Eval(Container.DataItem, "CompnayName")%></b><br />
                            <%# DataBinder.Eval(Container.DataItem,"AddressLine1") %><br />
                            <%# DataBinder.Eval(Container.DataItem,"AddressLine2") %><br />
                            <%# DataBinder.Eval(Container.DataItem,"AddressLine3") %><br />
                                <%# DataBinder.Eval(Container.DataItem,"City") %>&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem,"State") %>&nbsp;
                            <%# DataBinder.Eval(Container.DataItem,"ZipCode") %>
                            <br>
                            <%# DataBinder.Eval(Container.DataItem,"Country") %>
                        </font>
                                    </div>
                                    <div>
                                        <asp:Button ID="cmdEditAddress" AlternateText="Edit" runat="server" Text="Edit" CssClass="submitBtn" />
                                    </div>
                                </ItemTemplate>
                                <FooterStyle></FooterStyle>
                                <HeaderStyle></HeaderStyle>
                            </asp:DataList>
                        </div>
                    </div>

                </div>
                <cc1:User ID="User1" runat="server"></cc1:User>
                <cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
                <cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
            </div>
        </div>
    </div>
    <div>
        <rad:RadWindow ID="radwinPassword" runat="server" Width="350px" Modal="True" BackColor="#DADADA"
            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Change Password"
            Skin="Default">
            <ContentTemplate>
                <asp:UpdatePanel ID="updatepnl" runat="server">
                    <ContentTemplate>

                        <div style="padding-top: 10px;">

                            <table id="tblLogin" cellspacing="0" cellpadding="0" border="0" runat="server">
                                <tr>
                                    <td valign="top" colspan="2" class="style1">
                                        <asp:Label ID="Label6" ForeColor="Crimson" runat="server"></asp:Label>
                                        <table id="tblData" border="0" runat="server" cellspacing="3" cellpadding="3">
                                            <tr>
                                                <td align="right" valign="top" class="tablecontrolsfontLogin">
                                                    <font color="red">*</font>
                                                    <asp:Label ID="Label15" runat="server">Current Password</asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtoldpassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="tablecontrolsfontLogin">
                                                    <font color="red">*</font>
                                                    <asp:Label ID="lblPassword" runat="server">New Password</asp:Label>&nbsp;
                                                </td>
                                                <td style="padding-top: 2px;">
                                                    <asp:TextBox ID="txtNewPassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="tablecontrolsfontLogin">
                                                    <font color="red">*</font>
                                                    <asp:Label ID="Label18" runat="server">Repeat Password</asp:Label>&nbsp;
                                                </td>
                                                <td style="padding-top: 2px;">
                                                    <asp:TextBox ID="txtRepeat" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                            </table>

                            <div class="tablecontrolsfontLogin actions-pop-up marg-top-20px">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return ShowvalidationErrorMsg()"
                                    CssClass="submitBtn" />

                                <input type="submit" value="Cancel" onclick="HideChangePasswordWindow();" class="submitBtn">
                            </div>
                        </div>
                        <div>
                            <table>
                                <tr>

                                    <td class="tdValidationErrormessage">
                                        <asp:Label ID="lblErrormessage" class="marg-top-20px" ForeColor="red" runat="server"
                                            Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                                <tr>

                                    <td class="tdValidationcolor tdValidationErrormessage">
                                        <asp:CompareValidator ID="CompareValidator" runat="server" ControlToValidate="txtRepeat"
                                            Display="Dynamic" ControlToCompare="txtNewPassword" ErrorMessage="The new passwords must match. Please try again."></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdCompairvalidationErrormessage">
                                        <asp:Label ID="lblerrorLength" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />

                    </Triggers>
                </asp:UpdatePanel>
            </ContentTemplate>
        </rad:RadWindow>
    </div>
    <div>

        <rad:RadWindow ID="radUpdateHomeAddress" runat="server" Width="560px" Height="570px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Update your home address" Behavior="None">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <table class="tblEditAtendee">
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblErrorHomeCountry" runat="server" Text="" ForeColor="Red" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label38" runat="server" Text="Address line 1:"></asp:Label>

                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server" ControlToValidate="txtHomeAddLine1" ValidationGroup="Altok" ErrorMessage="Blank Value not allow for Address line 1" Display="Dynamic"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtHomeAddLine1" runat="server" Width="250px"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="Label39" runat="server" Style="text-align: left">Address line 2:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtHomeAddLine2" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="Label3" runat="server" Style="text-align: left">Address line 3:</asp:Label></td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtHomeAddLine3" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="Label5" runat="server" Style="text-align: left">Town/city:</asp:Label></td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtHomeAddCity" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label40" runat="server" Style="text-align: left">Post code:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtHomeAddPostalCode" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label41" runat="server" Style="text-align: left">Country:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="ddlHomeAddCountry" CssClass="cmbUserProfileState" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Style="text-align: left">State:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="ddlHomeAddState" CssClass="cmbUserProfileState" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3"></td>
                                <td class="RightColumn actions"></td>
                            </tr>
                        </table>
                    </ContentTemplate>

                </asp:UpdatePanel>
                <table>
                    <tr>
                        <td colspan="2" style="font-size: 11pt">Note: changes to home address be approved by Registry and  Subscriptions, because they can impact on billing.
This can take up to  2 business days.</td>

                    </tr>
                    <tr>
                        <td style="width: 170px">&nbsp
                        </td>
                        <td class="RightColumn actions">
                            <asp:Button ID="btnUpdateHomeAdd" runat="server" Text="Submit change" class="submitBtn" ValidationGroup="Altok" />
                            <asp:Label ID="Label45" runat="server" Style="text-align: left" Visible="false"></asp:Label>
                            <input type="button" value="Cancel" onclick="HideUpdateHomeAddress();" class="submitBtn">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>

    </div>



    <div>

        <rad:RadWindow ID="radChangeCompany" runat="server" Width="560px" Height="400px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Edit Company Record" Behavior="None">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <table class="tblEditAtendee">
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label9" runat="server" Style="text-align: left" CssClass="label-title" Text="Company name:"></asp:Label>

                                    <asp:TextBox ID="txtCompany" CssClass="txtBoxEditProfile" ValidationGroup="CreateCompany" runat="server"></asp:TextBox>
                                    <Ajax:AutoCompleteExtender ID="extCompany" runat="server" BehaviorID="autoComplete"
                                        CompletionInterval="10" CompletionListElementID="divwidth" CompletionSetCount="10"
                                        EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetCompanyList1"
                                        ServicePath="~/GetCompanyList__c.asmx" TargetControlID="txtCompany" OnClientItemSelected="ClientItemSelected"
                                        OnClientPopulating="companyitemselecting" CompletionListCssClass="autocomplete_completionListElement">
                                    </Ajax:AutoCompleteExtender>
                                    <Ajax:TextBoxWatermarkExtender ID="WatermarkExtender1" runat="server" TargetControlID="txtCompany"
                                        WatermarkText="Type Company Name Here" WatermarkCssClass="watermarked" />
                                    <asp:RequiredFieldValidator ID="reqtxtCompany" runat="server" ControlToValidate="txtCompany"
                                        ValidationGroup="CreateCompany" ErrorMessage="Company Name Required" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>

                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table>

                    <tr>
                        <td>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>

                        <td>

                            <input type="button" value="Create a company record" onclick="ShowAddNewCompany();" class="submitBtn">

                            <input type="button" value="Remove this company record" onclick="ShowDeleteCompany();" class="submitBtn">
                            <input type="button" value="Cancel" onclick="HideChangeCompany();" class="submitBtn">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                        </td>
                    </tr>

                    <tr>

                        <td>

                            <asp:Button ID="btnEditCompSave" runat="server" Text="submit change" class="submitBtn" ValidationGroup="CreateCompany" />

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>

    </div>

    <div>

        <rad:RadWindow ID="radAddNewCompany" runat="server" Width="560px" Height="570px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Add Company Record"
            Behavior="None">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <table class="tblEditAtendee">
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblcomp" runat="server" Text="Company Name:"></asp:Label>

                                </td>

                                <td>
                                    <asp:TextBox ID="txtAddCompName" ValidationGroup="AddNewCompany" runat="server" Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqAddCompName" runat="server" ControlToValidate="txtAddCompName"
                                        ValidationGroup="AddNewCompany" ErrorMessage="Company Name Required" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>

                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Style="text-align: left">Address line 1:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddCompLine1" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Style="text-align: left">Address line 2:</asp:Label></td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddCompLine2" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Style="text-align: left">Address line 3:</asp:Label></td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddCompLine3" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Style="text-align: left">Town/city:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddCompCity" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td>
                                    Post code:
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddCompPostalCode" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <asp:Label ID="Label29" runat="server" Style="text-align: left">State</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="cmbAddCompState" CssClass="cmbUserProfileState" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label32" runat="server" Style="text-align: left">County:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddCompCounty" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label27" runat="server" Style="text-align: left">Country:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="cmbAddCompCountry" CssClass="cmbUserProfileCountry" runat="server"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="Label25" runat="server" Style="text-align: left">(Intl Code)(Area Code) Phone</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <rad:RadMaskedTextBox ID="txtAddCompIntlCode" CssClass="txtUserProfileAreaCodeSmall"
                                        runat="server" Mask="(####)" Width="25%">
                                    </rad:RadMaskedTextBox>
                                    <rad:RadMaskedTextBox ID="txtAddCompPhoneAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                        runat="server" Mask="(####)" Width="25%">
                                    </rad:RadMaskedTextBox>
                                    <rad:RadMaskedTextBox ID="txtAddCompPhone" CssClass="txtUserProfileAreaCode" runat="server"
                                        Mask="(#######)" Width="45%">
                                    </rad:RadMaskedTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3"></td>
                                <td class="RightColumn actions"></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbAddCompCountry" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <table>
                    <tr>
                        <td style="width: 170px">&nbsp
                        </td>
                        <td class="RightColumn actions">
                            <asp:Button ID="btnAddNewCompany" runat="server" Text="Submit change" class="submitBtn" ValidationGroup="AddNewCompany" />
                            <input type="button" value="Cancel" onclick="HideAddNewCompany();" class="submitBtn">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
    </div>



    <div>

        <rad:RadWindow ID="radDeleteCompany" runat="server" Width="400px" Height="300px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="No longer at this address?"
            Behavior="None">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <table class="tblEditAtendee">
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr>


                                <td colspan="2">
                                    <div style="font-weight: bold; font-size: 10pt;">
                                        You are about to delete this company from
your profile. This change request  requires
at least one active address on your profile.
                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="font-size: 11pt;">
                                        <b>Press</b> submit if you wish to continue.
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </ContentTemplate>

                </asp:UpdatePanel>
                <table>
                    <tr>
                        <td style="width: 170px">&nbsp
                        </td>
                        <td class="RightColumn actions">
                            <asp:Button ID="btnDeleteCompanyPopup" runat="server" Text="Submit" class="submitBtn" />
                            <input type="button" value="Cancel" onclick="HideDeleteCompany();" class="submitBtn">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
    </div>

</div>
