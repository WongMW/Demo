<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SDLoginSF.ascx.cs" Inherits="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.SDLoginSF" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/Sitefinity4xSSO.ascx" TagName="Sitefinity4xSSO" TagPrefix="uc2" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="uc1" TagName="SocialNetworkingIntegrationControlSF4" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/SocialNetworkingIntegrationControlSF4.ascx" %>
<meta http-equiv="Cache-Control" content="no-cache">
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="Expires" content="0">
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Panel ID="pnllogin" runat="server" DefaultButton="cmdLogin">
    <div id="loginTop" class="news-list" runat="server">
        <%-- Nalini 12429 13/12/2011--%>
        <div class="ICELoginHeader">
        </div>
        <div class="content-container-Home clearfix" style="width: 100%">
            <asp:Literal runat="server" ID="litLoginLabel" />
            <div id="tblLogin" runat="server">
                <div class="main-title">
                    Login to your account
                </div>
                <div class="login-form sfContentBlock">
                    <p>
                        <strong>Login to your account using your member number or student number to avail of member or student services.</strong>
                    </p>
                    <p>
                        If your user ID begins with a 0, you'll need to include the zero to login. if you are not a member or a student yet, your user ID is generally your email address.
                    </p>

                    <asp:Label ID="lblError" ForeColor="red" runat="server"></asp:Label>
                    <div id="tblData">
                        <div class="LeftColumnEditProfile">
                            <%--  Dilip issue 12717  --%>
                            <asp:Label ID="lblUserID" runat="server">User ID/member no/student no:</asp:Label>&nbsp;
                        </div>
                        <%--   <td valign="top">--%>
                        <div>
                            <asp:TextBox TabIndex="1" ID="txtUserID" runat="server"></asp:TextBox>
                        </div>
                        <div class="LeftColumnEditProfile">
                            <asp:Label ID="lblPassword" runat="server">Password:</asp:Label>&nbsp;<div id="capsLockWarning" style="float: right; font-weight: normal; color: red; display: none;">Caps Lock is on</div>
                        </div>
                        <%--   <td valign="top">--%>
                        <div style="padding-top: 2px;">
                            <asp:TextBox TabIndex="2" ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </div>

                        <div class="tablecontrolsfontLogin">
                            <asp:UpdatePanel ID="pnl1" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbl" runat="server" Visible="false"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger runat="server" ControlID="chkAutoLogin" EventName="" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="remember-me-wrapper">
                            <asp:CheckBox ID="chkAutoLogin" CssClass="" runat="server" ToolTip="Check this box if you would like the site to automatically log you in next time you visit."
                                Text=" Remember me" AutoPostBack="true"></asp:CheckBox>
                        </div>
                        <div id="ForgotDetails" runat="server" class="" style="float: right; display: inline-block;">
                            <span>Forgot </span>
                            <asp:LinkButton ID="ForgotUsername" runat="server" class="bold">user ID</asp:LinkButton>
                            <span>or</span>
                            <asp:LinkButton ID="ForgotPassword" runat="server" class="bold">Password</asp:LinkButton>
                        </div>
                        <div>
                            <%--  Dilip issue 12717--%>
                            <asp:Button TabIndex="3" ID="cmdLogin" runat="server" CssClass="login-btn" Text="Login" OnClick="cmdLogin_Click"></asp:Button>
                        </div>
                        <div class="seperator" style="display: none;">
                            <div class="separator-text">
                                OR
                            </div>
                        </div>
                    </div>
                    <div style="display: none;">
                        <div class="tablecontrolsfontLogin">
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </div>
                        <div>
                            <uc1:SocialNetworkingIntegrationControlSF4 ID="SocialNetworkingIntegrationControlSF4" runat="server" />
                        </div>
                        <div>
                            <asp:LinkButton ID="cmdNewUser" Style="display: none;" runat="server" Text="New User Signup!" CssClass="ButtonLink"
                                SkinID="Test"></asp:LinkButton>
                        </div>
                        <cc1:AptifyWebUserLogin ID="WebUserLogin1" runat="server"
                            Visible="False"></cc1:AptifyWebUserLogin>

                    </div>
                </div>
            </div>

            <div id="tblWelcome" runat="server" style="width: 100%" class="logged-in-message">
                <%--Nalini Issue 12734--%>
                <div>
                    <asp:Label ID="lblWelcome" runat="server">Welcome</asp:Label>
                    <br />
                    <asp:Label ID="lblMultiLogin" runat="server"></asp:Label>
                </div>
                &nbsp;
                <%--Added as part of #20448--%>
                <div>
                    <asp:DropDownList runat="server" ID="cmdWebUser" CssClass="txtBoxEditProfile" DataTextField="WebUserID" DataValueField="ID"
                        Width="200px" AutoPostBack="false">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button TabIndex="3" ID="btnLogin" runat="server" CssClass="submitBtn" Text="Login" OnClick="btnLogin_OnClick"></asp:Button>
                </div>
                <%--End of #20448--%>
                <div>
                    Here are some popular links on the site to aid your navigation:
                </div>
                &nbsp;
                <div style="margin-left: 20px">
                    <ul style="list-style-type: disc">
                        <li><a href="/customerservice/default.aspx" style="text-decoration: underline; font-weight: 700;">Myaccount</a> - make a payment, update your contact details or change your password</li>
                        <li><a href="/productcatalog/default.aspx" style="text-decoration: underline; font-weight: 700;">Shop</a> for CPD or publications</li>
                        <li><a href="/Current-Student/Student-Centre" style="text-decoration: underline; font-weight: 700;">Student centre</a> for exam results, course materials and CA Diary</li>
                        <li><a href="/chariot/lindex.html" style="text-decoration: underline; font-weight: 700;">Chariot</a></li>
                        <li><a href="/Prospective-Students/Apply-and-Join/Exemptions/Eligibility-and-Exemption-Application" style="text-decoration: underline; font-weight: 700;">Student application</a> to continue your application to become a student</li>
                        <li runat="server" id="MOODLELinkLi" visible="false"><asp:HyperLink runat="server" id="MOODLELinkA" style="text-decoration: underline; font-weight: 700;">Moodle</asp:HyperLink> text here</li>
                        <li><span class="label">Individual Annual Return:</span>if you are trying to reset your password for the IAR please <span style="font-weight: bold">logout</span> to view the forgot ‘Password’ link. </li>
                    </ul>
                </div>
                <%--Added by Harish Redmine #20968--%>
                <br />
                <div id="trMentorDashboard" runat="server" style="margin-left: 20px" visible="false">
                    <ul style="list-style-type: disc">
                        <li>
                            <a href="/Education/mentordashboard.aspx" style="text-decoration: underline; font-weight: 700;">CA Diary Mentor Dashboard</a>
                        </li>
                    </ul>
                </div>
                <%-- End Code Added by Harish Redmine #20968--%>
                <div class="logout">
                    <asp:Button ID="cmdLogOut" runat="server" CausesValidation="False" CssClass="logout-btn " Text="Logout" OnClick="cmdLogOut_ServerClick" />
                </div>
            </div>

            <asp:HiddenField ID="savedUserField" runat="server" />
            <cc2:AptifyShoppingCart ID="ShoppingCartLogin" runat="server"></cc2:AptifyShoppingCart>
        </div>
    </div>
</asp:Panel>

<rad:RadWindow ID="SecurityResetMsgModal" runat="server" Width="400px" Height="300px"
    Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False"
    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Reset password" Behavior="Close">
    <ContentTemplate>
        <div class="password-reset-popup">
            <p>Please reset your password.</p>
            <p>Due to a security upgrade, the first time you use the new website, a new password is required. Please reset your password now.</p>
            <br />
            <asp:Button ID="resetPasswordBtn" runat="server" CausesValidation="False" CssClass="submitBtn" Text="Reset My Password" OnClick="resetPasswordBtn_OnClick" />
        </div>

    </ContentTemplate>
</rad:RadWindow>

<rad:RadWindow ID="EmailSentMsgModal" runat="server" Width="400px" Height="380px"
    Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False"
    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Check your email" Behavior="None">
    <ContentTemplate>
        <div class="password-reset-popup">
            <p>Please check your email.</p>
            <p>
                <asp:Label ID="emailField" runat="server"></asp:Label>
            </p>
            <br />
            <asp:Button ID="confirmResetBtn" runat="server" CausesValidation="False" CssClass="submitBtn" Text="OK" OnClick="confirmResetBtn_OnClick" />
            <asp:Label ID="SmallPrint" runat="server" CssClass="small-print"> Still can't access your account? Check your <a href="http://www.charteredaccountants.ie/spam" target="_blank">spam filter</a> settings.<br/>Alternatively, contact us at webmaster@charteredaccountants.ie <br />with your student/member number or user ID. <br />Tel: +353 1 637 7200 or +44 28 90435840</asp:Label>
        </div>

    </ContentTemplate>
</rad:RadWindow>

<rad:RadWindow ID="MissingEmailMsgModal" runat="server" Width="400px" Height="350px"
    Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False"
    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Reset password" Behavior="None">
    <ContentTemplate>
        <div class="password-reset-popup">
            <p>If the user ID you entered is valid a password reset will be sent to your email.</p>
            <br />
            <p>If you do not receive an email please click below to email <a href="mailto:webmaster@charteredaccountants.ie?Subject=Missing%20password%20reset%20email"><strong>WEBMASTER</strong></a></p>
            <p>or tel: +353 1 637 7200 / +44 28 90435840</p>
            <br />
            <asp:Button ID="MissingEmailOKBtn" runat="server" CausesValidation="False" CssClass="submitBtn" Text="OK" OnClick="MissingEmailOKBtn_OnClick" />
        </div>
    </ContentTemplate>
</rad:RadWindow>


<rad:RadWindow ID="ForgotUsernameModal" runat="server" Width="400px" Height="530px"
    Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False"
    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Forgot user ID ?" Behavior="Close">
    <ContentTemplate>
        <div class="password-reset-popup forgot-modal">
            <p>If you are a member or student you can use your member or student number as your User ID.</p>
            <p>If you would like a reminder of your User ID, please enter the details below.</p>
            <br />
            <div class="modal-form">
                <span class="required">First name</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required field" ControlToValidate="ForgotUsernameFirstName" CssClass="error-message" ValidationGroup="validateEmail1"></asp:RequiredFieldValidator>
                <asp:TextBox name="firstName" type="text" ID="ForgotUsernameFirstName" runat="server" class="form-control" />

                <span class="required">Last name</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required field" ControlToValidate="ForgotUsernameLastName" CssClass="error-message" ValidationGroup="validateEmail1"></asp:RequiredFieldValidator>
                <asp:TextBox name="lastName" type="text" ID="ForgotUsernameLastName" runat="server" class="form-control" />


                <span class="required">Email address</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required field" ControlToValidate="ForgotUsernameEmail" CssClass="error-message" ValidationGroup="validateEmail1"></asp:RequiredFieldValidator>
                <asp:TextBox name="txtEmail1" type="text" ID="ForgotUsernameEmail" runat="server" class="form-control" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid email" ControlToValidate="ForgotUsernameEmail" ValidationGroup="validateEmail1" ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*" CssClass="error-message"></asp:RegularExpressionValidator>
                <br />

                <asp:Button ID="ForgotUsernameButton" runat="server" CausesValidation="True" ValidationGroup="validateEmail1" CssClass="submitBtn" Text="Remind Me" OnClick="ForgotUsernameButton_OnClick" />
            </div>
        </div>
    </ContentTemplate>
</rad:RadWindow>


<rad:RadWindow ID="ForgotPasswordModal" runat="server" Width="400px" Height="360px"
    Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False"
    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Forgot password?" Behavior="Close">
    <ContentTemplate>
        <div class="password-reset-popup forgot-modal">
            <p>Please enter your User ID. We will email you a link to reset your password.</br> Note: This email will go to your Institute-registered email address.</p>
            <br />
            <div class="modal-form">
                <span id="label1"><strong>User ID/member no/student no</strong></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required field" ControlToValidate="ForgotPasswordUsername" CssClass="error-message" ValidationGroup="validateForgotPas"></asp:RequiredFieldValidator>
                <asp:TextBox name="txtUsername" type="text" ID="ForgotPasswordUsername" runat="server" class="form-control" />
                <br />
                <asp:Button ID="ForgotPasswordButton" runat="server" CausesValidation="True" ValidationGroup="validateForgotPassword" CssClass="submitBtn" Text="Reset My Password" OnClick="ForgotPasswordButton_OnClick" />
            </div>
        </div>
    </ContentTemplate>
</rad:RadWindow>


<uc2:Sitefinity4xSSO ID="Sitefinity4xSSO1" runat="server" />

<asp:HiddenField ID="clientOffSet" runat="server" />
<input type="hidden" runat="server" id="hdOffset" name="hdOffset" />
<asp:Image ID="img" Style="visibility: hidden;" runat="server" ImageUrl="~/Images/ICE Login Icon.png" />
<%--<cc1:User ID="User1" runat="server" />--%>

<script language="javascript">
    function isCapsLockOn(e) {
        var keyCode = e.keyCode ? e.keyCode : e.which;
        var shiftKey = e.shiftKey ? e.shiftKey : ((keyCode == 16) ? true : false);
        return (((keyCode >= 65 && keyCode <= 90) && !shiftKey) || ((keyCode >= 97 && keyCode <= 122) && shiftKey))
    }
    $(document).ready(function () {
        $("#baseTemplatePlaceholder_content_C025_LoginSF4_txtPassword").keypress(function (e) {
            if (isCapsLockOn(e))
                $("#capsLockWarning").show();
            else
                $("#capsLockWarning").hide();
        });

        $('[tabindex="1"]').focus();
    });

</script>

