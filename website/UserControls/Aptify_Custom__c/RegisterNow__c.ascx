<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/RegisterNow__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.RegisterNow__c" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/CompanyContact__c.ascx" TagName="CompanyContact__c"
    TagPrefix="uc3" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagName="SyncProfile" TagPrefix="uc1" Src="~/UserControls/Aptify_Customer_Service/SynchProfile.ascx" %>
<script type="text/javascript" language="javascript">
    //Anil  for Issue 6640
    //window.history.forward(1);

</script>
<div class="reg-form " id="registerForm" runat="server">
    <div class="reg-form-message">
        <h1 class="main-title">No Account?</h1>
        <p class="new-account-welcome-message">
            Create an account if you are new and would like to access<br />
            course, resources or information about future events.
        </p>
    </div>
    <div>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div>
        <asp:Label ID="Label33" CssClass="required" runat="server">First Name</asp:Label>

        <asp:TextBox SkinID="RequiredTextBox" CssClass="txtBoxEditProfile"
            ID="txtFirstName" ValidationGroup="Profile" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFirstName"
            ErrorMessage="First Name Required" Display="Dynamic"
            ForeColor="Red" ValidationGroup="Profile"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Label ID="Label34" runat="server" CssClass="required">Last Name</asp:Label>

        <asp:TextBox SkinID="RequiredTextBox" ID="txtLastName" CssClass="txtBoxEditProfile"
            runat="server" ValidationGroup="Profile"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLastName"
            ErrorMessage="Last Name Required" ValidationGroup="Profile" ForeColor="Red"
            Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
    <div class="LeftColumnEditProfile">
        <asp:Label ID="lblDob" CssClass="required" runat="server">Date of Birth</asp:Label>

    </div>
    <div class="RightColumn date-picker">
        <telerik:RadDatePicker CssClass="rcCalPopup" ID="txtDob" runat="server" Calendar-ShowOtherMonthsDays="false"
            MinDate="01/01/1777" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
        </telerik:RadDatePicker>
        <asp:RegularExpressionValidator CssClass="rcCalPopup" ID="RegularExpressionValidator4"
            ValidationExpression="^[0-9m]{1,2}/[0-9d]{1,2}/[0-9y]{4}$" ForeColor="Red" ErrorMessage=""
            ControlToValidate="txtDob" SetFocusOnError="True"
            runat="server" ValidationGroup="Profile" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDob"
            Display="Static" ErrorMessage="Date of birth required" ValidationGroup="Profile" ForeColor="Red">
        </asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Label ID="Label32" CssClass="required" runat="server">Email</asp:Label>

        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtBoxEditProfile"
            SkinID="RequiredTextBox" ValidationGroup="Profile"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail"
            ErrorMessage="Email Required" ForeColor="Red"
            Display="Dynamic" ValidationGroup="Profile"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
            ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format"
            ForeColor="Red" ValidationGroup="Profile"></asp:RegularExpressionValidator>
    </div>
    <div>
        <asp:Label ID="lblPWD" runat="server" CssClass="required">Password</asp:Label>

        <asp:TextBox ID="txtPassword2" runat="server" CssClass="txtBoxPasswordProfile" SkinID="RequiredTextBox"
            TextMode="Password" ValidationGroup="Profile"></asp:TextBox>

        <asp:RequiredFieldValidator ID="valPWDRequired" SetFocusOnError="true" runat="server" ControlToValidate="txtPassword2"
            ErrorMessage="Password Required" ForeColor="Red"
            Display="Dynamic" ValidationGroup="Profile"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Label ID="lblRepeatPWD" CssClass="required" runat="server">Confirm Password</asp:Label>

        <asp:Label ID="lblpasswordlengthError" runat="server" ForeColor="Red"></asp:Label>
        <asp:TextBox ID="txtRepeatPWD2" runat="server" CssClass="txtBoxPasswordProfile" TextMode="Password"
            SkinID="RequiredTextBox" ValidationGroup="Profile"></asp:TextBox>
        <asp:CompareValidator ID="valPWDMatch" runat="server" ControlToValidate="txtRepeatPWD2"
            ErrorMessage="Passwords Must Match" ControlToCompare="txtPassword2"
            ForeColor="Red" Display="Dynamic" ValidationGroup="Profile"></asp:CompareValidator>
    </div>
        <div class="password-info">
        <span>Password needs to include:</span>
        <ul>
            <li>Minimum length 7 characters</li>
            <li>1 lower-case letter</li>
            <li>1 upper-case letter</li>
            <li>1 number</li>
        </ul>
    </div>
    <div class="form-actions">
        <asp:Button ID="btnSubmit" runat="server" CssClass="register-btn" CausesValidation="true"
            Text="Register Now"  />

        
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <%-- <div class="seperator" style="display: none;">
            <div class="separator-text">
                OR
            </div>
        </div>--%>
        <%--<div class="sign-in-linkedin" style="display: none;">
            <uc1:socialnetworkingintegrationcontrolsf4 id="SocialNetworkingIntegrationControlSF4"
                runat="server" />
        </div>--%>
    </div>
</div>
<rad:RadWindow ID="radDuplicateUser" runat="server" Width="650px" Height="150px"
    Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
    <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
            <tr>
                <td align="left">
                    <asp:Label ID="lblAlert" runat="server" Text="The system has detected a conflict between the name and email address you entered and existing information we have on file. Please use a different email address or contact Customer Service for assistance."
                        Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnok" runat="server" Text="OK" Width="50px" class="submitBtn" ValidationGroup="ok"
                        Height="23px" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </ContentTemplate>
</rad:RadWindow>
<cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
<cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
