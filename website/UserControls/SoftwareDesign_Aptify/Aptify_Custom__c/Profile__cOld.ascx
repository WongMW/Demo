<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/Profile__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.ProfileControl__c" %>
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
    //Anil  for Issue 6640
    window.history.forward(1);

    window.onload = function (e) {
        var currentURL = window.location.href;
        if (currentURL.toLowerCase().indexOf("login") == -1) {
            window.document.getElementById('<%= profilePanel.ClientID%>').style.display = "block";
            setSelected();
        } else {
            window.document.getElementById('<%= registerForm.ClientID%>').style.display = "block";
        }
    };

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_pageLoaded(pageLoaded);
    function pageLoaded() {
        setSelected();
    }

    function setSelected() {
        var selectedIndex = $('#<%= ddlAddressType.ClientID %> option:selected').index();
        var title = $('#<%= ddlAddressType.ClientID %> option:selected').val();
        var buttonSelector = '.address-buttons input:eq(' + selectedIndex + ')';
        $("#address-type-title").text(title + " Details");
        $(buttonSelector).addClass('selected');
    }

    function submitDropdown(index) {
        var selector = '#<%= ddlAddressType.ClientID %>>option:eq(' + index + ')';
        $(selector).prop('selected', true);
        __doPostBack('', '');
        setSelected();
        return false;
    }

    function ClientItemSelected(sender, e) {
        document.getElementById("<%=hfCompanyID.ClientID %>").value = e.get_value();
    }

    //Neha, issue 12591 Added function for Issue 12591
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
            //Neha, Issue 12591,03/06/13, added condition for Passwordvalidation
            if (window.document.getElementById('<%= txtNewPassword.ClientID %>').value != window.document.getElementById('<%= txtRepeat.ClientID %>').value) {
                ValidatorEnable(cmpValidator, true);
                window.document.getElementById('<%= lblErrormessage.ClientID %>').style.display = "none";
                return false;
            }
        }
    }
    //End 

    function ShowImage(ImageURL) {
        if (document.getElementById(ImageURL) != undefined) {
            document.getElementById(ImageURL).style.visibility = "visible";
        }
    }

    //Anil Add function for Issue 12835
    function UploadImage() {
        //Neha issue no 14430,03/01/13, disable Apply button 
        var button = document.getElementById('<%= btnSaveProfileImage.ClientID %>');
        button.disabled = true;
        button.value = 'Submitting...';
        var result = false;
        var upfile = document.getElementById('<%= radUploadProfilePhoto.ClientID%>' + 'file0').value;

        if (upfile != "") {
            var accept = "png,gif,jpg,jpeg".split(',');
            var getExtention = upfile.split('.');
            var extention = getExtention[getExtention.length - 1];
            for (i = 0; i < accept.length; i++) {
                if (accept[i].toUpperCase() == extention.toUpperCase()) {
                    result = true;
                    break;
                }
            }
            if (!result) {
                //alert("allowed file extention are png,gif,jpg,jpeg");
            }
            else {
                document.getElementById('<%= btnUpload.ClientID%>').click();
            }
        }
        else {
            alert("select image to Upload");
        }
        return result;
    }

    function ZoomBestFit() {
        var $ = $telerik.$;
        var imEditor = $find("<%= radImageEditor.ClientID %>");
        var imgProfile = imEditor.getImage();
        if (imgProfile.height > 320 || imgProfile.width > 400) {
            imEditor.zoomBestFit();
        }
        else {
            imEditor.zoomImage(100, true);
        }
    }

    function CropImage() {
        var $ = $telerik.$;
        var imageEditor = $find("<%= radImageEditor.ClientID %>");

        if (typeof (imageEditor._currentToolWidget) != "undefined") {
            if (typeof (imageEditor._currentToolWidget._cropBtn) != "undefined") {
                imageEditor._currentToolWidget._cropBtnClick();
                imageEditor._currentToolWidget._cancelBtnClick();
                imageEditor._currentToolWidget.close();
            }
        }

        return false;
    }

    function ShowCropButton(isVisible) {
        var btnCropImage = document.getElementById("<%= btnCropImage.ClientID %>");
        if (isVisible == true) {
            btnCropImage.style.display = "inline";
        }
        else {
            btnCropImage.style.display = "none";
        }
    }

    function OnClientToolsDialogClosed(sender, eventArgs) {
        ShowCropButton(false);
    }

    function OnClientImageChanging(sender, eventArgs) {
        if (eventArgs.get_commandName() == "Crop") {
            ShowCropButton(false);

        }
        else if (eventArgs.get_commandName() == "Reset") {
            ShowCropButton(false);

        }
    }

    function OnClientCommandExecuted(sender, eventArgs) {
        //Suraj S Issue 16495 , here we find the browse if the browser is Google crome it will return the 36 index for other ie and mozill it will return -1
        //this is for crome browser
        var browserName = navigator.userAgent.indexOf("AppleWebKit");
        if (eventArgs.get_commandName() == "Crop") {
            if (browserName > -1) {
                ZoomBestFit();
            }
            ShowCropButton(true);
        }
        //Suraj S Issue 16495 ,  if the browser is crome then call  the reset code here 
        if (browserName > -1) {
            if (eventArgs.get_commandName() == "Reset") {
                ShowCropButton(true);
            }
        }

    }


    // Neha issue no 14430, 03/01/13, added Function(enable apply button and applied class)
    function EnabledImageSaveButton() {
        var button = document.getElementById('<%= btnSaveProfileImage.ClientID %>');
        button.className = "submitBtn";
        button.disabled = false;
    }
    //End  



    function getAddress() {
        debugger;
        var webmethod = "http://<%=Request.Url.Host.ToString() %>" + ":" + "<%= Request.Url.Port.ToString() %><%= System.Web.Configuration.WebConfigurationManager.AppSettings("virtualdir").ToString() %>webservices/PopulatecompanyBusinessAddress.asmx/PopulateCompanyBusinessAddress";
        var vParam = $('#<%= txtCompany.ClientID %>').val();
        vParam = vParam.replace('&quot;', '&quot;&quot;');
        var parmeter = JSON.stringify({ 'Company': vParam });
        $.ajax({
            type: "POST",
            url: webmethod,
            data: parmeter,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var Address = JSON.parse(response.d);

                if (Address.length > 0) {
                    $('#<%= txtAddressLine1.ClientID %>').val(Address[0].AddressLine1);
                    $('#<%= txtAddressLine2.ClientID %>').val(Address[0].AddressLine2);
                    $('#<%= txtAddressLine3.ClientID %>').val(Address[0].AddressLine3);
                    $('#<%= txtCity.ClientID %>').val(Address[0].City);
                    $('#<%= cmbState.ClientID %>').val(Address[0].State);
                    $('#<%= cmbCountry.ClientID %>').val(Address[0].CountryCodeID);
                    $('#<%= txtZipCode.ClientID %>').val(Address[0].ZipCode);
                    $('#<%= txtPhoneAreaCode.ClientID %>').val(Address[0].MainAreaCode);
                    $('#<%= txtPhone.ClientID %>').val(Address[0].MainPhone);
                    $('#<%= txtFaxAreaCode.ClientID %>').val(Address[0].MainFaxAreaCode);
                    $('#<%= txtFaxPhone.ClientID %>').val(Address[0].MainFaxNumber);

                }
            },
            failure: function (msg) {
                $('#output').text(msg);
            }
        });
    }
</script>
<script type="text/javascript">
    jQuery(function ($) {
        var getData = true;

        $('#<%= btnSubmit.ClientID%>').on('click', function () {
            if (getData) {
                $('#<%= txtFirstName.ClientID%>').val($('#<%= firstNameTextHolder.ClientID%>').val());
                $('#<%= txtLastName.ClientID%>').val($('#<%= lastNameTextHolder.ClientID%>').val());
                $('#<%= txtEmail.ClientID()%>').val($('#<%= emailTextHolder.ClientID()%>').val());
                $('#<%= txtUserID.ClientID()%>').val($('#<%= emailTextHolder.ClientID()%>').val());
                $('#<%= txtPasswordHintAnswer.ClientID()%>').val($('#<%= emailTextHolder.ClientID()%>').val());
            } else {
                getData = true;
            }

            return true;
        });


        $('#<%= btnSubmit2.ClientID%>').on('click', function () {
            getData = false;
            $('#<%= btnSubmit.ClientID%>').click();
        });
    });
</script>

<div class="reg-form " id="registerForm" runat="server" style="display: none;">
    <div class="reg-form-message">
        <h1 class="main-title">No Account?</h1>
        <p class="new-account-welcome-message">
            Create an account if you are new and would like to access<br />
            course, resources or imformation about future events.
        </p>
    </div>
    <!-- First Name Field -->
    <asp:Label ID="Label33" CssClass="required" runat="server">First Name</asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="firstNameTextHolder"
        ValidationGroup="ProfileControl2" ErrorMessage="First Name Required" Display="Dynamic"
        ForeColor="Red" CssClass="error-message"></asp:RequiredFieldValidator>
    <asp:TextBox SkinID="RequiredTextBox" ValidationGroup="ProfileControl2" CssClass="txtBoxEditProfile" ID="firstNameTextHolder"
        runat="server"></asp:TextBox>

    <!-- Last Name Field -->
    <asp:Label ID="Label34" runat="server" CssClass="required">Last Name</asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="lastNameTextHolder"
        ValidationGroup="ProfileControl2" ErrorMessage="Last Name Required" ForeColor="Red"
        Display="Dynamic" CssClass="error-message"></asp:RequiredFieldValidator>
    <asp:TextBox SkinID="RequiredTextBox" ID="lastNameTextHolder" CssClass="txtBoxEditProfile"
        runat="server" ValidationGroup="ProfileControl2"></asp:TextBox>

    <!-- Date of Birth -->
    <div class="LeftColumnEditProfile">
        <asp:Label ID="lblDob" CssClass="required" runat="server">Date of Birth</asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDob"
            Display="Static" ErrorMessage="Date of birth required" ForeColor="Red" ValidationGroup="ProfileControl2" CssClass="error-message">
        </asp:RequiredFieldValidator>
    </div>
    <div class="RightColumn date-picker">
        <%--  <asp:TextBox ID="txtDob" runat="server" CssClass="txtBoxEditProfile" Width="150px" ></asp:TextBox>--%>
        <telerik:RadDatePicker CssClass="rcCalPopup" ID="txtDob" runat="server" Calendar-ShowOtherMonthsDays="false" MinDate="01/01/1777" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
        </telerik:RadDatePicker>
        <%--  <Ajax:CalendarExtender ID="CalendarExtender2" runat="server"  TargetControlID="txtDob">
        </Ajax:CalendarExtender>--%>
        <asp:RegularExpressionValidator CssClass="rcCalPopup" ID="RegularExpressionValidator4" ValidationExpression="^[0-9m]{1,2}/[0-9d]{1,2}/[0-9y]{4}$"
            ForeColor="Red" ErrorMessage="" ValidationGroup="ProfileControl2" ControlToValidate="txtDob" SetFocusOnError="True" runat="server" />

    </div>

    <!-- email -->
    <asp:Label ID="Label32" CssClass="required" runat="server">Email</asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="emailTextHolder" ErrorMessage="Email Required" ValidationGroup="ProfileControl2" ForeColor="Red"
        Display="Dynamic" CssClass="error-message"></asp:RequiredFieldValidator>
    <asp:TextBox ID="emailTextHolder" runat="server" CssClass="txtBoxEditProfile" ValidationGroup="ProfileControl2" SkinID="RequiredTextBox"></asp:TextBox>


    <%--Suraj Issue 15210 ,1/7/13 RegularExpressionValidator validator --%>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationExpression="[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+(?:[A-Z]{2}|com|COM|org|ORG|net|NET|edu|EDU|gov|GOV|mil|MIL|biz|BIZ|info|INFO|mobi|MOBI|name|NAME|aero|AERO|asia|ASIA|jobs|JOBS|museum|MUSEUM|in|IN|co|CO)\b"
        ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ValidationGroup="ProfileControl2"
        ForeColor="Red" CssClass="error-message"></asp:RegularExpressionValidator>

    <!-- passwords -->
    <asp:Label ID="lblPWD" runat="server" CssClass="required">Password</asp:Label>
    <asp:RequiredFieldValidator ID="valPWDRequired" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password Required" ValidationGroup="ProfileControl2" ForeColor="Red"
        Display="Dynamic" CssClass="error-message"></asp:RequiredFieldValidator>
    <asp:TextBox ID="txtPassword" runat="server" CssClass="txtBoxPasswordProfile" SkinID="RequiredTextBox" ValidationGroup="ProfileControl2" TextMode="Password"></asp:TextBox>


    <%--Aparna issue no  12964 Add new label--%>
    <asp:Label ID="lblRepeatPWD" CssClass="required" runat="server">Confirm Password</asp:Label>
    <asp:CompareValidator ID="valPWDMatch" runat="server" CssClass="error-message" ControlToValidate="txtRepeatPWD" ValidationGroup="ProfileControl2" ErrorMessage="Passwords Must Match" ControlToCompare="txtPassword" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
    <asp:Label ID="lblpasswordlengthError" runat="server" ForeColor="Red" CssClass="error-message"></asp:Label>
    <asp:TextBox ID="txtRepeatPWD" runat="server" CssClass="txtBoxPasswordProfile" TextMode="Password" ValidationGroup="ProfileControl2" SkinID="RequiredTextBox"></asp:TextBox>
    <!-- Submit Buttons Area -->
    <div class="form-actions">
        <%-- CausesValidation="false" added by pradip 2016-05-14 for issue no Bug #13395--%>
        <asp:Button ID="btnSubmit" runat="server" CssClass="register-btn" CausesValidation="false" Text="Register Now" ValidationGroup="ProfileControl2" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <div class="seperator" style="display: none;">
            <div class="separator-text">
                OR
            </div>
        </div>
        <div class="sign-in-linkedin" style="display: none;">
            <uc1:SocialNetworkingIntegrationControlSF4 ID="SocialNetworkingIntegrationControlSF4" runat="server" />
        </div>
    </div>
</div>


<asp:Panel runat="server" ID="profilePanel" Style="display: none;">
    <asp:HiddenField ID="hfCompanyID" runat="server" Value="" />
    <asp:Literal ID="ltlImageEditorStyle" runat="server"></asp:Literal>
    <div id="Container" class="ProfileMainDiv">
        <div id="TitleDiv" class="ProfileTitleDiv">
            <table class="ProfileTitleTable">
                <tr>
                    <td class="PageTitle">
                        <%-- <asp:Label ID="lblPageTitle" Text="New User Signup" runat="server"></asp:Label>--%>
                    </td>
                </tr>
            </table>
        </div>
        <div class="ProfileLeftDiv">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div id="leftDiv">
                        <div id="UpperDiv" class="ProfileUpperDiv">
                            <table id="tblProfileImg" cellpadding="0px" cellspacing="0px">
                                <%-- Anil Changess for issue 12718 27-jan-2012--%>
                                <tr>
                                    <td class="empty">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdImage">
                                        <%-- Neha Changess for issue 16177 05/14/13--%>
                                        <asp:Image ID="imgProfile" runat="server" ImageUrl="" ClientIDMode="AutoID" CssClass="ProfileImageBorder" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Image ID="imgEditProfileImage" runat="server" ImageUrl="~/Images/Edit.png" Style="width: 12px" />
                                        <asp:LinkButton ID="lbtnOpenProfileImage" runat="server" Text="Edit" CausesValidation="false" />
                                    </td>
                                </tr>

                            </table>
                        </div>
                        <div id="MiddleDiv" class="ProfileMiddleDiv" style="display: none;">
                            <uc1:SyncProfile runat="server" ID="SyncProfile" />
                        </div>
                        <div id="LowerDiv" class="ProfileLowerDiv">
                            <table id="Table1" cellpadding="0px" cellspacing="0px">
                                <tr style="height: 5px;">
                                    <td align="left"></td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <div id="divMembership" style="padding-top: 10px;" class="no-mob">
                                            <asp:Image ID="ImgMembershipe" runat="server" ImageUrl="" CssClass="imgMembership" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lbtnOpenProfileImage" />
                    <%--<asp:AsyncPostBackTrigger ControlID="btnRemovePhoto" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnUpload" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="RightDiv" class="ProfileRightDiv profile-page">
            <div id="tblMain" runat="server" width="70%" class="data-form">
                <table width="100%">
                    <tr>
                        <td style="padding-right: 0.5em; padding-left: 1.5em; padding-bottom: 0.5em; padding-top: 0.5em"
                            valign="top">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Visible="false"></asp:ValidationSummary>
                            <asp:Label ID="Label1" runat="server" BackColor="Transparent" ForeColor="Red" Visible="False"></asp:Label>
                            <asp:Label ID="lblPasswordsuccess" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                </table>

                <div>
                    <h2 class="profile-page-title">My Profile</h2>
                </div>

                <div class="profile-note">
                    <asp:Label ID="Label12" runat="server">* All fields marked mandatory</asp:Label>
                </div>

                <div class="cai-form">
                    <div id="divPendingChange" runat="server" class="pending-changes">
                        <span class="form-title">Pending Changes Details
                        </span>
                        <div class="cai-form-content">
                            <uc4:PendingChangesDetailsControl ID="PendingChangesDetails1" runat="server" />
                        </div>
                    </div>
                </div>

                <div class="cai-form">
                    <span class="form-title">Personal Information</span>

                    <div runat="server" id="trUserID" visible="false" class="field-group top-section">
                        <asp:Label ID="Label7" runat="server" CssClass="label-title"><span class="RequiredField">*</span>User ID</asp:Label>
                        <asp:Label ID="lblUserID" runat="server"></asp:Label>
                    </div>

                    <div class="form-section-half-border">

                        <div class="field-group">
                            <asp:Label ID="lblSalutation" runat="server" CssClass="label-title">Salutation</asp:Label>
                            <asp:DropDownList ID="cmbSalutation" CssClass="txtBoxEditProfileForDropdown" runat="server">
                            </asp:DropDownList>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblPreferredSalutation" runat="server" CssClass="label-title">Preferred Salutation</asp:Label>
                            <asp:TextBox SkinID="RequiredTextBox" CssClass="txtBoxEditProfile" ID="txtPreferredSalutation"
                                runat="server"></asp:TextBox>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblName" runat="server" CssClass="label-title">First Name</asp:Label>
                            <asp:TextBox SkinID="RequiredTextBox" CssClass="txtBoxEditProfile" ID="txtFirstName"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFirstName"
                                ValidationGroup="ProfileControl" ErrorMessage="First Name Required" Display="Dynamic"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="Label4" runat="server" CssClass="requiredLabel label-title">Last Name</asp:Label>
                            <asp:TextBox SkinID="RequiredTextBox" ID="txtLastName" CssClass="txtBoxEditProfile"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                                ValidationGroup="ProfileControl" ErrorMessage="Last Name Required" ForeColor="Red"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblOtherName" runat="server" CssClass="label-title">Other Name</asp:Label>
                            <asp:TextBox SkinID="RequiredTextBox" ID="txtOtherName" CssClass="txtBoxEditProfile"
                                runat="server"></asp:TextBox>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblGender" runat="server" CssClass="label-title">Gender</asp:Label>
                            <asp:DropDownList ID="cmbGender" CssClass="txtBoxEditProfileForDropdown" runat="server">
                                <asp:ListItem Value="0">Male</asp:ListItem>
                                <asp:ListItem Value="1">Female</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">Unknown</asp:ListItem>
                            </asp:DropDownList>
                        </div>


                        <div class="field-group">
                            <asp:LinkButton ID="lnkChangePwd" runat="server" Text="Change Password?" CausesValidation="false" CssClass="submitBtn"></asp:LinkButton>
                        </div>
                    </div>


                    <div class="form-section-half-border">



                        <div class="field-group">
                            <asp:Label ID="lblEmail" runat="server" CssClass="label-title"><span class="RequiredField">*</span>Email</asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtBoxEditProfile" SkinID="RequiredTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Email Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            <%--Suraj Issue 15210 ,1/7/13 RegularExpressionValidator validator --%>
                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
                                ValidationExpression="[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+(?:[A-Z]{2}|com|COM|org|ORG|net|NET|edu|EDU|gov|GOV|mil|MIL|biz|BIZ|info|INFO|mobi|MOBI|name|NAME|aero|AERO|asia|ASIA|jobs|JOBS|museum|MUSEUM|in|IN|ie|IE|co|CO)\b"
                                ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ValidationGroup="ProfileControl"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblCompany" runat="server" CssClass="label-title">Company</asp:Label>
                            <asp:TextBox ID="txtCompany" CssClass="txtBoxEditProfile" runat="server" onchange="getAddress();"></asp:TextBox>
                            <Ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="autoComplete"
                                CompletionInterval="10" CompletionListElementID="divwidth" CompletionSetCount="12"
                                EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetCompanyList" ServicePath="~/GetCompanyList__c.asmx"
                                TargetControlID="txtCompany" OnClientItemSelected="ClientItemSelected">
                            </Ajax:AutoCompleteExtender>
                        </div>


                        <div class="field-group">
                            <asp:Label ID="lblTitle" runat="server" CssClass="label-title">Job Title</asp:Label>
                            <asp:TextBox ID="txtTitle" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblPrimaryFunction" runat="server" CssClass="label-title">Primary Job Function</asp:Label>
                            <asp:DropDownList ID="cmbPrimaryFunction" CssClass="txtBoxEditProfileForDropdown"
                                runat="server">
                            </asp:DropDownList>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblEmploymentStatus" runat="server" CssClass="label-title">Employment Status</asp:Label>
                            <asp:DropDownList ID="cmbempstatus" CssClass="txtBoxEditProfileForDropdown" runat="server">
                            </asp:DropDownList>
                        </div>

                        <div id="trPractice" runat="server" visible="false" class="field-group">
                            <asp:Label ID="lblPractice" runat="server" CssClass="label-title">Practice Status</asp:Label>
                            <asp:TextBox ID="txtPractice" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                        </div>

                        <div class="field-group">
                            <asp:LinkButton ID="LinkBtnpopup" CssClass="submitBtn" runat="server" Text="New Company?" CausesValidation="false" ></asp:LinkButton>
                        </div> 
                   </div>
                </div>

                <div runat="server" id="trWebAccount">
                    <div class="cai-form">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <span class="form-title">Web Account Information</span>

                                <div class="field-group">
                                    <asp:Label ID="lblWebUID" runat="server" class="label-title"><span class="RequiredField">*</span>User ID</asp:Label>
                                    <asp:TextBox ID="txtUserID" runat="server" CssClass="txtBoxPasswordProfile" SkinID="RequiredTextBox"
                                        AutoPostBack="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUserID"
                                        ValidationGroup="ProfileControl" ErrorMessage="User ID Required" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                    <%-- <asp:LinkButton ID="lnkCheckAvailable" ValidationGroup="ss" runat="server" Text="Check Availability"></asp:LinkButton>--%>
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                </div>

                                <div class="field-group">
                                    <asp:Label ID="lblPasswordHintQuestion" runat="server" Class="label-title"><span class="RequiredField">*</span>Hint Question</asp:Label>
                                    <asp:DropDownList ID="cmbPasswordQuestion" runat="server" CssClass="cmbBoxChoosColour">
                                        <asp:ListItem Value="My favorite color is?" Selected="True">My favorite color is?</asp:ListItem>
                                        <asp:ListItem Value="My mother's maiden name is?">My mother's maiden name is?</asp:ListItem>
                                        <asp:ListItem Value="I went to which high school?">I went to which high school?</asp:ListItem>
                                        <asp:ListItem Value="I was born in which city?">I was born in which city?</asp:ListItem>
                                        <asp:ListItem Value="My pet's name is?">My pet's name is?</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="field-group">
                                    <asp:Label ID="lblPasswordHintAnswer" runat="server" Class="label-title"><span class="RequiredField">*</span>Password Hint Answer</asp:Label>
                                    <asp:TextBox ID="txtPasswordHintAnswer" runat="server" SkinID="RequiredTextBox" CssClass="txtBoxPasswordProfile"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="valPasswordHintRequired" runat="server" ControlToValidate="txtPasswordHintAnswer"
                                        ValidationGroup="ProfileControl" ErrorMessage="Hint Answer Required" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtUserID" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div>
                    <div class="cai-form">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <span class="form-title">Contact Information</span>

                                <div class="cai-form-content">

                                    <div class="field-group" style="display: none;">
                                        <span class="label-title">Select Address Type</span>

                                        <asp:DropDownList ID="ddlAddressType" CssClass="cmbUserProfileBussinessAdress" runat="server" AutoPostBack="true">
                                            <asp:ListItem>Business Address</asp:ListItem>
                                            <asp:ListItem>Home Address</asp:ListItem>
                                            <asp:ListItem>Billing Address</asp:ListItem>
                                            <asp:ListItem>PO Box Address</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="field-group address-buttons">
                                        <input type="submit" class="submitBtn" onclick="submitDropdown(0)" value="Business Address" />
                                        <input type="submit" class="submitBtn" onclick="submitDropdown(1)" value="Home Address" />
                                        <input type="submit" class="submitBtn" onclick="submitDropdown(2)" value="Billing Address" />
                                        <input type="submit" class="submitBtn" onclick="submitDropdown(3)" value="PO Box Address" />
                                    </div>

                                    <div class="field-group">
                                        <h2 id="address-type-title">Address Details</h2>
                                    </div>


                                    <!-- Address Line  Rows -->
                                    <div class="field-group" id="trAddressLine1" runat="server">
                                        <asp:Label ID="lblAddress" runat="server" Class="label-title">Address</asp:Label>
                                        <asp:TextBox ID="txtAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="field-group" id="trHomeAddressLine1" runat="server" visible="false">
                                        <asp:Label ID="Label2" runat="server" Class="label-title">Address</asp:Label>
                                        <asp:TextBox ID="txtHomeAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="field-group" id="trBillingAddressLine1" runat="server" visible="false">
                                        <asp:Label ID="Label3" runat="server" Class="label-title">Address</asp:Label>
                                        <asp:TextBox ID="txtBillingAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="field-group" id="trPOBoxAddressLine1" runat="server" visible="false">
                                        <asp:Label Class="label-title" ID="Label8" runat="server">Address</asp:Label>
                                        <asp:TextBox ID="txtPOBoxAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>
                                    <!-- Address Line 2 Rows -->
                                    <div class="field-group" id="trAddressLine2" runat="server">
                                        <asp:TextBox ID="txtAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group" id="trHomeAddressLine2" runat="server" visible="false">
                                        <asp:TextBox ID="txtHomeAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group" id="trBillingAddressLine2" runat="server" visible="false">
                                        <asp:TextBox ID="txtBillingAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group" id="trPOBoxAddressLine2" runat="server" visible="false">
                                        <asp:TextBox ID="txtPOBoxAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>
                                    <!-- Address Line 3 Rows -->

                                    <div class="field-group" id="trAddressLine3" runat="server">
                                        <asp:TextBox ID="txtAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="field-group" id="trHomeAddressLine3" runat="server" visible="false">
                                        <asp:TextBox ID="txtHomeAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="field-group" id="trBillingAddressLine3" runat="server" visible="false">
                                        <asp:TextBox ID="txtBillingAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="field-group" id="trPOBoxAddressLine3" runat="server" visible="false">
                                        <asp:TextBox ID="txtPOBoxAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                    </div>


                                    <div class="form-section-half">
                                        <%--  Anil Changess for issue 12718--%>
                                        <div class="field-group" id="trCity" runat="server">
<span class="RequiredField">*</span>
                                            <asp:Label ID="lblCityStateZip" Class="label-title" runat="server">City</asp:Label>
                                            <asp:TextBox ID="txtCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator67" runat="server" ControlToValidate="txtCity"
                                                ErrorMessage="City Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>

                                            <asp:Label ID="Label5" Class="label-title" runat="server">State</asp:Label>
                                            <asp:DropDownList ID="cmbState" CssClass="cmbUserProfileState" runat="server">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="field-group" id="trHomeCity" runat="server" visible="false">
<span class="RequiredField">*</span>
                                            <asp:Label ID="Label9" Class="label-title" runat="server">City</asp:Label>
                                            <asp:TextBox ID="txtHomeCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtHomeCity"
                                                ErrorMessage="City Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label111" Class="label-title" runat="server">State</asp:Label>
                                            <asp:DropDownList ID="cmbHomeState" CssClass="cmbUserProfileState" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="field-group" id="trBillingCity" runat="server" visible="false">
<span class="RequiredField">*</span>
                                            <asp:Label ID="Label10" Class="label-title" runat="server">City</asp:Label>
                                            <asp:TextBox ID="txtBillingCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator167" runat="server" ControlToValidate="txtBillingCity"
                                                ErrorMessage="City Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label22" Class="label-title" runat="server">State</asp:Label>
                                            <asp:DropDownList ID="cmbBillingState" CssClass="cmbUserProfileState" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                        </div>

                                        <div class="field-group" id="trPOBoxCity" runat="server" visible="false">
<span class="RequiredField">*</span>
                                            <asp:Label ID="Label11" Class="label-title" runat="server">City</asp:Label>
                                            <asp:TextBox ID="txtPOBoxCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator68" runat="server" ControlToValidate="txtPOBoxCity"
                                                ErrorMessage="City Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label14" Class="label-title" runat="server">State</asp:Label>
                                            <asp:DropDownList ID="cmbPOBoxState" CssClass="cmbUserProfileState" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                        </div>

                                    </div>

                                    <div class="form-section-half">
                                        <div class="field-group" id="trCountry" runat="server">
                                            <span class="RequiredField">*</span> <asp:Label Class="label-title" ID="lblCountry" runat="server">Country</asp:Label>
                                            <asp:DropDownList ID="cmbCountry" CssClass="cmbUserProfileCountry" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator InitialValue="Select Country" ID="Req_ID" Display="Dynamic"
                                                runat="server" ControlToValidate="cmbCountry" Text="" ErrorMessage="Country Required"
                                                CssClass="required-label" ValidationGroup="ProfileControl" ></asp:RequiredFieldValidator>
                                           &nbsp; <span class="SpanZipCode">
                                                 <span class="RequiredField">*</span>

                                            <asp:Label Class="label-title" ID="Label23" runat="server">ZIP Code</asp:Label>
                                            <asp:TextBox ID="txtZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator69" runat="server" ControlToValidate="txtZipCode"
                                                ErrorMessage="Zip Code Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="field-group" id="trHomeCountry" runat="server" visible="false">
                                            <span class="RequiredField">*</span><asp:Label ID="Label13" Class="label-title" runat="server">Country</asp:Label>
                                            <asp:DropDownList ID="cmbHomeCountry" CssClass="cmbUserProfileCountry" runat="server"
                                                AutoPostBack="true">
 
                                            </asp:DropDownList>
<asp:RequiredFieldValidator InitialValue="Select Country" ID="RequiredFieldValidator110"
                                                Display="Dynamic" runat="server" ControlToValidate="cmbHomeCountry" Text="" ErrorMessage="Country Required"
                                                CssClass="required-label" ValidationGroup="ProfileControl"></asp:RequiredFieldValidator>
<span class="RequiredField">*</span>
                                            <asp:Label ID="Label24" Class="label-title" runat="server">ZIP Code</asp:Label>
                                            <asp:TextBox ID="txtHomeZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator70" runat="server" ControlToValidate="txtHomeZipCode"
                                                ErrorMessage="Zip Code Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="field-group" id="trBillingCountry" runat="server" visible="false">
                                            <asp:Label ID="Label16" runat="server" Class="label-title">Country</asp:Label>
                                            <asp:DropDownList ID="cmbBillingCountry" CssClass="cmbUserProfileCountry" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator InitialValue="Select Country" ID="RequiredFieldValidator170" Display="Dynamic"
                                                runat="server" ControlToValidate="cmbHomeCountry" Text="" ErrorMessage="Country Required"
                                                CssClass="required-label" ValidationGroup="ProfileControl" ></asp:RequiredFieldValidator>
                                             
<span class="RequiredField">*</span>
                                            <asp:Label ID="Label19" runat="server" Class="label-title">ZIP Code</asp:Label>
                                            <asp:TextBox ID="txtBillingZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator115" runat="server" ControlToValidate="txtBillingZipCode"
                                                ErrorMessage="Zip Code Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="field-group" id="trPOBoxCountry" runat="server" visible="false">
                                             <span class="RequiredField">*</span><asp:Label ID="Label17" runat="server" Class="label-title">Country</asp:Label>
                                            <asp:DropDownList ID="cmbPOBoxCountry" CssClass="cmbUserProfileCountry" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator InitialValue="Select Country" ID="RequiredFieldValidator171" Display="Dynamic"
                                                runat="server" ControlToValidate="cmbPOBoxCountry" Text="" ErrorMessage="Country Required"
                                                CssClass="required-label" ValidationGroup="ProfileControl" ></asp:RequiredFieldValidator>
						 <span class="RequiredField">*</span>												
                                            <asp:Label ID="Label20" runat="server" Class="label-title">ZIP Code</asp:Label>
                                            <asp:TextBox ID="txtPOBoxZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator116" runat="server" ControlToValidate="txtPOBoxZipCode"
                                                ErrorMessage="Zip Code Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>


                                    <div class="field-group">
                                        <hr />
                                    </div>

                                    <div class="field-group">
                                        <h2>Contact Details</h2>
                                    </div>


                                    <div class="form-section-half">
                                        <div class="field-group">
                                            <asp:Label ID="lblPhone" CssClass="label-title" runat="server">(Intl Code)(Area Code) Phone</asp:Label>
                                            <%--Neha Issue 14750 ,02/26/13, added css for phoneareacode and faxAreacode--%>
                                            <rad:RadMaskedTextBox ID="txtIntlCode" CssClass="txtUserProfileAreaCodeSmall" runat="server"
                                            Mask="(###)" Width="30%">
                                           </rad:RadMaskedTextBox>
                                            <rad:RadMaskedTextBox ID="txtPhoneAreaCode" CssClass="txtUserProfileAreaCodeSmall" runat="server" Mask="(###)" Width="30%">
                                            </rad:RadMaskedTextBox>
                                            <rad:RadMaskedTextBox ID="txtPhone" CssClass="txtUserProfileAreaCode" runat="server" Mask="###-####" Width="50%">
                                            </rad:RadMaskedTextBox>
                                        </div>
                                    </div>

                                    <div class="form-section-half">
                                        <div class="field-group">
                                            <asp:Label ID="lblFax" CssClass="label-title" runat="server" visible="false">(Area Code) Fax</asp:Label>
                                            <rad:RadMaskedTextBox ID="txtFaxAreaCode" CssClass="txtUserProfileAreaCodeSmall" runat="server" Mask="(###)" Width="30%" visible="false">
                                            </rad:RadMaskedTextBox>
                                            <rad:RadMaskedTextBox ID="txtFaxPhone" runat="server" CssClass="txtUserProfileAreaCode" Mask="###-####" Width="68%" visible="false">
                                            </rad:RadMaskedTextBox>
                                        </div>
                                    </div>

                                    <div class="field-group">
                                        <asp:CheckBox ID="chkPrefAddress" CssClass="cb" runat="server" Text="Preferred Address"
                                            AutoPostBack="True" />
                                        <img runat="server" style="visibility: hidden;" id="imgProcessing" alt="Animated processing image URL not set" />
                                        <div id="output">
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlAddressType" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cmbCountry" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chkPrefAddress" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cmbBillingCountry" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cmbHomeCountry" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cmbPOBoxCountry" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>

                <div runat="server" id="trmemberinfo">
                    <div class="cai-form">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div id="memberinfo" runat="server">
                                    <span class="form-title">Membership Information</span>

                                    <div class="form-section-half-border">

                                        <div class="field-group">
                                            <asp:Label ID="lblmembershipType" runat="server" CssClass="label-title">Membership Type</asp:Label>
                                            <asp:Label ID="lblMemberTypeVal" runat="server"></asp:Label>

                                        </div>
                                        <div class="field-group">
                                            <asp:Label ID="lblStartDate" runat="server" CssClass="label-title">Start Date</asp:Label>
                                            <asp:Label ID="lblStartDateVal" runat="server"></asp:Label>
                                        </div>
                                        <div class="field-group">
                                            <asp:Label ID="lblEndDate" runat="server" CssClass="label-title">End Date</asp:Label>
                                            <asp:Label ID="lblEndDateVal" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-section-half-border">
                                        <div class="field-group">
                                            <asp:Label ID="lblStatus" runat="server" CssClass="label-title">Status</asp:Label>
                                            <asp:Label ID="lblStatusVal" runat="server"></asp:Label>
                                        </div>
                                        <div class="field-group">
                                            <asp:Label ID="Label30" runat="server" CssClass="label-title">Membership Grade</asp:Label>
                                            <asp:Label ID="lblmembershipGradeval" runat="server"></asp:Label>
                                        </div>
                                        <div class="field-group">
                                            <asp:Label ID="Label31" runat="server" CssClass="label-title">Membership Number</asp:Label>
                                            <asp:Label ID="lblMembershipNumber" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="txtUserID" EventName="TextChanged" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <%-- hiding  "Select Topics of Interest to You" using style="display:none;" 29/04/2016-LornaDaly --%>
                <div class="cai-form" style="display: none;">

                    <span class="form-title">Select Topics of Interest to You</span>
                    <div class="cai-form-content">

                        <div class="field-group">
                            <asp:CheckBoxList ID="cblTopicofInterest" CssClass="test" runat="server" RepeatColumns="4"
                                RepeatDirection="Horizontal" Width="100%">
                            </asp:CheckBoxList>
                            <uc2:TopicCodeControl ID="TopicCodeControl1" runat="server" />
                        </div>
                    </div>
                </div>
                <%--added by Rajesh K--%>
                <%-- hiding  "Select Additional Organization" using style="display:none;" 29/04/2016-LornaDaly --%>
                <div id="trAdddOrganization" runat="server" visible="true" style="display: none;">
                    <div class="cai-form">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                            <ContentTemplate>
                                <span class="form-title">Select Additional Organization</span>
                                <div class="cai-form-content">
                                    <uc4:AdditionalOrganization ID="ucAdditionalOrganization" runat="server" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>


            <div>
                <div>
                    <asp:Button ID="btnSubmit2" runat="server" CssClass="submitBtn" Text="Submit" ValidationGroup="ProfileControl" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="submitBtn" Text="Cancel" CausesValidation="False" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                </div>
                <div>
                    <asp:Label ID="lblPendingChangesMsg" runat="server" CssClass="label-title"></asp:Label>
                </div>
            </div>
        </div>
    </div>



    <rad:RadWindow ID="radwinPassword" runat="server" Width="350px" Modal="True" BackColor="#DADADA"
        VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Change Password"
        Skin="Default">
        <ContentTemplate>
            <asp:UpdatePanel ID="updatepnl" runat="server">
                <ContentTemplate>
                    <div style="padding-top: 10px;">
                        <span style="margin-top: 30px;">
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
                        </span>
                        <div class="tablecontrolsfontLogin actions-pop-up marg-top-20px">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return ShowvalidationErrorMsg()"
                                CssClass="submitBtn" />
                            <asp:Button ID="btnCancelpop" runat="server" Text="Cancel" CssClass="submitBtn" CausesValidation="false" />
                        </div>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <%--Neha issue no 12591 Updated ErrorMessage--%>
                                <td class="tdValidationErrormessage">
                                    <asp:Label ID="lblErrormessage" class="marg-top-20px" ForeColor="red" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                                <%--Neha issue no 12591 Updated ErrorMessage--%>
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
                    <asp:PostBackTrigger ControlID="btnCancelpop" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
    </rad:RadWindow>

    <rad:RadWindow ID="radDuplicateUser" runat="server" Width="650px" Height="150px"
        Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
        ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
        <ContentTemplate>
            <table width="100%" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblAlert" runat="server" Text="The system has detected a conflict between the name and email address you entered and existing information we have on file. Please use a different email address or contact Customer Service for assistance."
                            Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnok" runat="server" Text="OK" Width="50px" class="submitBtn" OnClick="btnok_Click"
                            ValidationGroup="ok" Height="23px" />&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </rad:RadWindow>
    <rad:RadWindow ID="radwindowProfileImage" runat="server" Width="500px"
        Modal="True" BackColor="#DADADA" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Profile Image" Behavior="None" Skin="Default" IconUrl="~/Images/personal-icon.png"
        MaxWidth="500px" CssClass="ProfileWindow">
        <ContentTemplate>
            <panel id="ProfileImagePanel" runat="server">
                <div>
                    <div>
                        <asp:Label ID="LableImageUploadText" runat="server" > </asp:Label>
                        <br />
                        <div style="display: none">
                            <asp:Label ID="LableImageSaveIndicator" runat="server" Visible="false"></asp:Label>

                        </div>

                    </div> 
                    <div>
                        <rad:RadUpload runat="server" id="radUploadProfilePhoto" ControlObjectsVisibility="None" OnClientFileSelected="UploadImage" Localization-Select="Browse..." AllowedFileExtensions=".gif, .jpg, .bmp, png" />
                        <asp:Button ID="btnRemovePhoto" runat="server" CausesValidation="False" Text="Remove" class="submitBtn" />
                        <asp:Button ID="btnUpload" runat="server" CssClass="submitBtn" CausesValidation="False" Width="60px" Text="Upload" Style="display: none" Height="23px" />
                    </div>

                </div> 
                <div>
                    <rad:RadImageEditor ID="radImageEditor" runat="server" Width="450px" AllowedSavingLocation="Server" OnClientImageChanged="EnabledImageSaveButton"  OnClientImageLoad="ZoomBestFit" OnClientToolsDialogClosed="OnClientToolsDialogClosed" OnClientImageChanging="OnClientImageChanging" OnClientCommandExecuted="OnClientCommandExecuted" CanvasMode="No" >    
                        <Tools>
                                <rad:ImageEditorToolGroup>
                                    <rad:ImageEditorToolStrip CommandName="Undo"></rad:ImageEditorToolStrip>
                                    <rad:ImageEditorToolStrip CommandName="Redo"></rad:ImageEditorToolStrip>
                                    <rad:ImageEditorTool Text="Reset" CommandName="Reset" />
                                    <rad:ImageEditorToolSeparator></rad:ImageEditorToolSeparator>
                                    <%--<rad:ImageEditorTool Text="Zoom" CommandName="Zoom" />--%> 
                                    <rad:ImageEditorTool Text="ZoomIn" CommandName="ZoomIn" /> 
                                    <rad:ImageEditorTool Text="ZoomOut" CommandName="ZoomOut" />
                                    <rad:ImageEditorToolSeparator></rad:ImageEditorToolSeparator>
                                    <rad:ImageEditorTool CommandName="Crop"></rad:ImageEditorTool> 
                                </rad:ImageEditorToolGroup>
                        </Tools>
                    </rad:RadImageEditor>
                   <br />
                    <asp:Label ID="lblCropTip" runat="server" Text="After cropping a photo, click Crop and then Apply."></asp:Label>
                    <asp:Label ID="lblIEUserMsg" runat="server" Text="Internet Explorer users may need to refresh the image before cropping."></asp:Label>

                </div>
                <div>
                    <asp:Button ID="btnCropImage" class="submitBtn" runat="server" Text="Crop" OnClientClick="return CropImage();"  />
                    <asp:Button ID="btnSaveProfileImage" class="submitBtn" runat="server" Text="Apply" CausesValidation="false"  UseSubmitBehavior="false"/>
                    <asp:Button ID="btnCancelProfileImage"  runat="server" Text="Cancel" class="submitBtn" OnClick="btnCancelProfileImage_Click" CausesValidation="false" />          
                </div>

            </panel>
        </ContentTemplate>
    </rad:RadWindow>
    <rad:RadWindow ID="radAlert" runat="server" Width="560px" Height="450px" Modal="True"
        Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
        ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="New Company Record" Behavior="None">
        <ContentTemplate>
            <table class="tblEditAtendee">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblcomp" runat="server" Text="Company Name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtComp" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label21" runat="server" Style="text-align: left">Address</asp:Label>
                    </td>
                    <td class="RightColumn">
                        <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3"></td>
                    <td class="RightColumn">
                        <asp:TextBox ID="TextBox5" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label26" runat="server" Style="text-align: left">City</asp:Label>
                    </td>
                    <td class="RightColumn">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label29" runat="server" Style="text-align: left">State</asp:Label>
                    </td>
                    <td class="RightColumn">
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label27" runat="server" Style="text-align: left">Country</asp:Label>
                    </td>
                    <td class="RightColumn">
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label25" runat="server" Style="text-align: left">(Area Code) Phone</asp:Label>
                    </td>
                    <td class="RightColumn">
                        <asp:TextBox ID="txtcode" runat="server" CssClass="txtUserProfileAreaCodeSmall" Style="margin-left: 0px"></asp:TextBox>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="txtUserProfileAreaCode"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3"></td>
                    <td class="RightColumn actions">
                        <asp:Button ID="Button1" runat="server" Text="OK" class="submitBtn" ValidationGroup="ok" />
                        <asp:Label ID="Label28" runat="server" Style="text-align: left" Visible="false"></asp:Label>
                        <asp:Button ID="Button2" runat="server" Text="Cancel" class="submitBtn" ValidationGroup="ok" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </rad:RadWindow>
    <cc1:User ID="User1" runat="server"></cc1:User>
    <cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
    <cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
</asp:Panel>
