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


<%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<link rel="stylesheet" href="CSS/bootstrap-override.min.css">
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<script type="text/javascript" src="/Scripts/password-validate.js" />--%>
<script type="text/javascript" language="javascript">
    //Anil  for Issue 6640
    //window.history.forward(1);

</script>

<!-- 'Begin of code for duplicate webuser #19819-->
<%-- <div id="duModal" class="modal fade">
    <div class="modal-dialog" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-content">
               <div class="modal-header modal-header-primary">
				        <p class="action-success-msg no-icon">You already have an account!</p>
                </div>
            <div id="modalBody" class="modal-body">
				<p>You already have an account. Please use the <strong>reset password</strong> or the <strong>forgot user ID</strong> functionality to retrieve login details.</p>
				<p>If you are a new user please contact&nbsp;&nbsp;<a class="cai-btn cai-btn-red-inverse" href="mailto:studentqueries@charteredaccountants.ie?subject=Duplicate%user%account"> student queries</a></p>
            </div>
            <div class="modal-footer sfContentBlock" >
                   <div class="text-center">       
                <button type="button" class="cai-btn cai-btn-red" data-dismiss="modal">CLOSE</button> 
					   </div>         
            </div>
        </div>
    </div>
</div>--%>

<!-- End of code for duplicate webuser #19819-->
  



<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>LOADING...<br /><br />
                    Please do not leave or close this window while the request is processing.</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="updpanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
<asp:panel id="pnlregister" runat="server" defaultbutton="btnSubmit">
<div class="reg-form " id="registerForm" runat="server">
    <div class="reg-form-message" style="margin-top:0px;">
        <div class="main-title">
            New account</div>
        <p class="new-account-welcome-message account-create">
            Note: your email will automatically become your user ID.
        </p>
    </div>
    <div>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div class="account-create">
        <asp:Label ID="Label33" CssClass="required" runat="server">First name</asp:Label>
        <asp:TextBox tabindex="5" SkinID="RequiredTextBox" CssClass="txtBoxEditProfile" ID="txtFirstName"
            ValidationGroup="Profile" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFirstName"
            ErrorMessage="First name required" Display="Dynamic" ForeColor="Red" ValidationGroup="Profile"></asp:RequiredFieldValidator>
    </div>
    <div class="account-create">
        <asp:Label ID="Label34" runat="server" CssClass="required">Last name</asp:Label>
        <asp:TextBox tabindex="6" SkinID="RequiredTextBox" ID="txtLastName" CssClass="txtBoxEditProfile"
            runat="server" ValidationGroup="Profile"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLastName"
            ErrorMessage="Last name required" ValidationGroup="Profile" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
     <div class="account-create">
        <asp:Label ID="Label41" runat="server" CssClass="required">Country of residence:</asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="red"
            ErrorMessage="Please select a country" ControlToValidate="ddlHomeAddCountry" InitialValue="Select Country"
            Display="Dynamic" ValidationGroup="Profile"></asp:RequiredFieldValidator>
        <asp:DropDownList  tabindex="7" ID="ddlHomeAddCountry" AutoPostBack="true" CssClass="cmbUserProfileState"
            runat="server" >
        </asp:DropDownList>
        </div>
    <div class="LeftColumnEditProfile account-create">
        <asp:Label ID="lblDob" CssClass="required" runat="server">Date of birth</asp:Label>
    </div>
    <div class="RightColumn date-picker account-create">
        <telerik:raddatepicker tabindex="8" cssclass="rcCalPopup" id="txtDob" runat="server" calendar-showothermonthsdays="false"
            mindate="01/01/1777" maxdate="01/01/9999" calendar-showrowheaders="false">
        </telerik:raddatepicker>
        <asp:RegularExpressionValidator CssClass="rcCalPopup" ID="RegularExpressionValidator4"
            ValidationExpression="^[0-9m]{1,2}/[0-9d]{1,2}/[0-9y]{4}$" ForeColor="Red" ErrorMessage=""
            ControlToValidate="txtDob" SetFocusOnError="True" runat="server" ValidationGroup="Profile" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDob"
            Display="Static" ErrorMessage="Date of birth required" ValidationGroup="Profile"
            ForeColor="Red">
        </asp:RequiredFieldValidator>
    </div>
   
      <div class="account-create">
            <asp:Label ID="Label32" CssClass="required" runat="server">Email</asp:Label>
            <asp:TextBox tabindex="9" ID="txtEmail" runat="server" CssClass="txtBoxEditProfile" SkinID="RequiredTextBox"
                ValidationGroup="Profile"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Email required" ForeColor="Red" Display="Dynamic" ValidationGroup="Profile"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
                ControlToValidate="txtEmail" ErrorMessage="Invalid email format" ForeColor="Red"
                ValidationGroup="Profile"></asp:RegularExpressionValidator>
        </div>
        <div class="account-create">
            <asp:Label ID="lblPWD" runat="server" CssClass="required">Password</asp:Label><div
                id="capsLockWarning1" style="float: right; font-weight: normal; color: red; display: none;">
                Caps Lock is on</div>
            <asp:TextBox tabindex="10" ID="txtPassword2" runat="server" CssClass="txtBoxPasswordProfile passwordToValidate"
                SkinID="RequiredTextBox" TextMode="Password" ValidationGroup="Profile"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valPWDRequired" SetFocusOnError="true" runat="server"
                ControlToValidate="txtPassword2" ErrorMessage="Password required" ForeColor="Red"
                Display="Dynamic" ValidationGroup="Profile"></asp:RequiredFieldValidator>
        </div>
        <div class="password-info">
            <span>Password needs to include:</span>
            <ul>
                <li id="letter" class="password-invalid">At least 1 lower-case letter</li>
                <li id="capital" class="password-invalid">At least 1 capital letter</li>
                <li id="number" class="password-invalid">At least 1 number</li>
                <li id="length" class="password-invalid">Minimum length 8 characters</li>
            </ul>
        </div>
        <div class="account-create">
            <asp:Label ID="lblRepeatPWD" CssClass="required" runat="server">Confirm password</asp:Label><div
                id="capsLockWarning2" style="float: right; font-weight: normal; color: red; display: none;">
                Caps Lock is on</div>
            <asp:Label ID="lblpasswordlengthError" runat="server" ForeColor="Red"></asp:Label>
            <asp:TextBox tabindex="11" ID="txtRepeatPWD2" runat="server" CssClass="txtBoxPasswordProfile" TextMode="Password"
                SkinID="RequiredTextBox" ValidationGroup="Profile"></asp:TextBox>

			 <asp:RequiredFieldValidator ID="cprf" SetFocusOnError="true" runat="server"
                ControlToValidate="txtRepeatPWD2" ErrorMessage=" Confirm password required" ForeColor="Red"
                Display="Dynamic" ValidationGroup="Profile"></asp:RequiredFieldValidator>

            <asp:CompareValidator ID="valPWDMatch" runat="server" ControlToValidate="txtRepeatPWD2"
                ErrorMessage="Passwords must match" ControlToCompare="txtPassword2" ForeColor="Red"
                Display="Dynamic" ValidationGroup="Profile"></asp:CompareValidator>
        </div>
        <div class="account-create">
            <asp:CheckBox ID="con" CssClass="" runat="server" Text="Please contact me about related products and services of the Institute (you can opt-out at any time via myaccount)."></asp:CheckBox>
        </div>
        <div class="form-actions account-create">
            <asp:Button ID="btnSubmit" tabindex="12" runat="server" CssClass="register-btn" CausesValidation="true"
                Text="Create account now" />
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
        <div class="account-create">
		    <h4>Use and protection of your personal information. </h4>
		    <p class="new-account-welcome-message account-create">
		    The Institute will use the information which you have provided in this form to respond to your request or process your transaction and will hold and protect it in accordance with the Institute's <a href="https://www.charteredaccountants.ie/Privacy-policy">privacy statement</a>, which explains your rights in relation to your personal data.</p>
		    <p><strong>By clicking on create account you acknowledge you have read our <a href="https://www.charteredaccountants.ie/Privacy-policy">privacy statement</a>.</strong></p>
		</div>
    </div>
</asp:panel>
                </ContentTemplate>
</asp:UpdatePanel>
    <rad:radwindow id="radDuplicateUser" runat="server" width="650px" height="350px"
        modal="True" skin="Default" backcolor="#f4f3f1" visiblestatusbar="False" behaviors="None"
        forecolor="#BDA797" iconurl="~/Images/Alert.png" title="Alert" behavior="None">
        <contenttemplate>
        <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
            <tr>
                <td align="left">
                    <asp:Label ID="lblAlert" runat="server" 
                        Font-Bold="true">There is a duplicate name and date of birth on our systems. If you have previously created an account please use the login credentials attached to the original login. If you are a new user please contact <a href="mailto:studentqueries@charteredaccountants.ie?subject=Duplicate%user%account"> student queries</a> for assistance.</asp:Label> <%-- Modified by Kavita Z - Label (lblAlert) Text for #16277 --%>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnok" runat="server" Text="OK"  class="submitBtn" ValidationGroup="ok"
                        Height="40px" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </contenttemplate>
    </rad:radwindow>
    <cc1:user id="User1" runat="server">
    </cc1:user>
    <cc3:aptifywebuserlogin id="WebUserLogin1" runat="server">
    </cc3:aptifywebuserlogin>
    <cc4:aptifyshoppingcart id="ShoppingCart1" runat="server" />
    <script language="javascript">
    function pageLoad() {
        var delay=10;
        setTimeout(function () {

        function isCapsLockOn(e) {
            var keyCode = e.keyCode ? e.keyCode : e.which;
            var shiftKey = e.shiftKey ? e.shiftKey : ((keyCode == 16) ? true : false);
            return (((keyCode >= 65 && keyCode <= 90) && !shiftKey) || ((keyCode >= 97 && keyCode <= 122) && shiftKey))
        }
        $(document).ready(function () {
            $("#baseTemplatePlaceholder_content_RegisterNow__c_txtPassword2").keypress(function (e) {
                if (isCapsLockOn(e))
                    $("#capsLockWarning1").show();
                else
                    $("#capsLockWarning1").hide();
            });
            $("#baseTemplatePlaceholder_content_RegisterNow__c_txtRepeatPWD2").keypress(function (e) {
                if (isCapsLockOn(e))
                    $("#capsLockWarning2").show();
                else
                    $("#capsLockWarning2").hide();
            });
        });
        }, delay);
    };
    </script>
