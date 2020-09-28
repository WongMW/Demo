<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SDNewPasswordControl.ascx.cs" Inherits="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.SDNewPasswordControl" %>

<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript" src="/Scripts/password-validate.js"></script>

<div class="content-container clearfix forgot-password-wrapper">
    <div class="forget-password-content">
        <h2 class="main-title">Set a new password</h2>
        <div class="reset-message">
            <p>
               Passwords must include at least eight characters, one capital letter and one number.
            </p>
        </div>
        <asp:Label ID="lblError" runat="server" class="error-message"></asp:Label>
        <div class="reset-form">
            <div id="tblMain" class="data-form">
                <asp:HiddenField ID="savedUserField" runat="server" />
                <br />
                <span id="label1" class="required">New password</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field" ControlToValidate="txtPassword1" CssClass="error-message" ValidationGroup="validateChangePass"></asp:RequiredFieldValidator>
                <asp:TextBox name="txtPassword1" type="password" ID="txtPassword1" runat="server" class="form-control passwordToValidate" />
                <br />
                <span id="label2" class="required">Confirm password</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required Field" ControlToValidate="txtPassword2" CssClass="error-message" ValidationGroup="validateChangePass"></asp:RequiredFieldValidator>
                <asp:TextBox name="txtPassword2" type="password" ID="txtPassword2" runat="server" class="form-control" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords do not match" ControlToCompare="txtPassword1" ControlToValidate="txtPassword2" ValidationGroup="validateChangePass" CssClass="error-message"></asp:CompareValidator>
                <br />

                <div class="password-info">
                    <span>Password needs to include:</span>
                    <ul>
                        <li id="letter" class="password-invalid">At least 1 lower-case letter</li>
                        <li id="capital" class="password-invalid">At least 1 capital letter</li>
                        <li id="number" class="password-invalid">At least 1 number</li>
                        <li id="length" class="password-invalid">Minimum length 8 characters</li>
                    </ul>
                </div>
            </div>
        </div>
        <br />

        <div class="reset-actions">
            <asp:Button ID="confirmResetBtn" runat="server" CausesValidsation="True" ValidationGroup="validateChangePass" CssClass="submitBtn" Text="Save New Password" />
        </div>
    </div>
</div>

<rad:radwindow id="SuccessWindow" runat="server" width="350px" height="300px"
    modal="True" skin="Default" backcolor="#f4f3f1" visiblestatusbar="False"
    forecolor="#BDA797" iconurl="~/Images/Alert.png" title="Success!" behavior="None">
        <ContentTemplate>
            <div class="password-reset-popup">
                 <p>Your password has been reset!</p>
                <p>Thank You.</p>
                <br/>
                <p>Please login with your new credentials</p>
                <br/>
                <asp:Button ID="successBtn" runat="server" CausesValidation="False" CssClass="submitBtn" Text="Login" OnClick="successBtn_OnClick" />
            </div>
           
        </ContentTemplate>
    </rad:radwindow>

<cc1:aptifywebuserlogin id="WebUserLogin1" runat="server" />
