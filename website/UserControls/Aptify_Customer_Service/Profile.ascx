<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Profile.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.ProfileControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagName="SyncProfile" TagPrefix="uc1" Src="~/UserControls/Aptify_Customer_Service/SynchProfile.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style2
    {
        width: 225px;
    }
</style>
<script type="text/javascript" language="javascript">
    //Anil  for Issue 6640
    window.history.forward(1);
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

</script>
<asp:Literal ID="ltlImageEditorStyle" runat="server"></asp:Literal>
<div id="Container" class="ProfileMainDiv">
    <div id="TitleDiv" class="ProfileTitleDiv">
        <table class="ProfileTitleTable">
            <tr>
                <td class="PageTitle">
                    <%-- <asp:Label ID="lblPageTitle" Text="New User Signup" runat="server"></asp:Label>--%>
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="Label12" runat="server" ForeColor="Red" Font-Size="8pt">* designates required fields</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="leftDiv" class="ProfileLeftDiv">
                <div id="UpperDiv" class="ProfileUpperDiv">
                    <table id="tblProfileImg" cellpadding="0px" cellspacing="0px">
                        <%-- Anil Changess for issue 12718 27-jan-2012--%>
                        <tr>
                            <td class="empty">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tdImage">  <%-- Neha Changess for issue 16177 05/14/13--%>
                                <asp:Image ID="imgProfile" runat="server" ImageUrl="" 
                                    ClientIDMode="AutoID" CssClass="ProfileImageBorder" />
                                <%-- <rad:RadBinaryImage ID="imgProfile" runat="server" Height="142px" Width="142px" ClientIDMode="AutoID" AutoAdjustImageControlSize="true" ResizeMode="Fill" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Image ID="imgEditProfileImage" runat="server" ImageUrl="~/Images/Edit.png" Style="width: 12px" />
                                <asp:LinkButton ID="lbtnOpenProfileImage" ForeColor="#d07b0c" runat="server" Text="Edit"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                        <%--<tr>
                    <td style="font-weight: normal;">
                        <div class="ProfileLinkedInDiv">
                            <asp:CheckBox ID="chkUseSocialMediaPhoto" runat="server" Text="Use my LinkedIn Photo for this Profile."
                                CssClass="cb" />
                            <br />                           
                        </div>
                    </td>
                </tr>--%>
                    </table>
                </div>
                <div style="height: 5px">
                </div>
                <div id="MiddleDiv" class="ProfileMiddleDiv">
                    <uc1:SyncProfile runat="server" ID="SyncProfile" />
                </div>
                <div id="LowerDiv" class="ProfileLowerDiv">
                    <table id="Table1" cellpadding="0px" cellspacing="0px">
                        <tr style="height: 5px;">
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div id="divMembership" style="padding-top: 10px;">
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
    <div id="RightDiv" class="ProfileRightDiv">
        <table id="tblMain" runat="server" width="100%" class="data-form">
            <tr>
                <td>
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
                </td>
            </tr>
            <%-- <tr>
		    <td colspan="2">
			    <p class="RequiredField">*<asp:label id="Label7" runat="server"> designates required fields</asp:label></p>
		    </td>
	    </tr>--%>
            <tr>
                <td>
                    <div class="BorderDiv">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <%--  Anil Changess for issue 12718--%>
                                <td class="tdPersonalInfo" colspan="2">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                Personal Information
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--  end--%>
                            </tr>
                            <tr runat="server" id="trUserID" visible="false">
                                <td class="LeftColumnEditProfile">
                                    <span class="RequiredField">*</span><asp:Label ID="Label7" runat="server">User ID:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:Label ID="lblUserID" runat="server"></asp:Label>
                                    &nbsp;
                                    <asp:LinkButton ID="lnkChangePwd" runat="server" Text="Change Password?" CausesValidation="false"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <span class="RequiredField">*</span><asp:Label ID="lblName" runat="server">First Name:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox SkinID="RequiredTextBox" CssClass="txtBoxEditProfile" ID="txtFirstName"
                                        runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFirstName"
                                        ValidationGroup="ProfileControl" ErrorMessage="First Name Required" Display="Dynamic"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <span class="RequiredField">*</span><asp:Label ID="Label4" runat="server">Last Name:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox SkinID="RequiredTextBox" ID="txtLastName" CssClass="txtBoxEditProfile"
                                        runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                                        ValidationGroup="ProfileControl" ErrorMessage="Last Name Required" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <span class="RequiredField">*</span><asp:Label ID="lblEmail" runat="server">Email:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtBoxEditProfile" SkinID="RequiredTextBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Email Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                    <%--Suraj Issue 15210 ,1/7/13 RegularExpressionValidator validator --%>
                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ValidationExpression="[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+(?:[A-Z]{2}|com|COM|org|ORG|net|NET|edu|EDU|gov|GOV|mil|MIL|biz|BIZ|info|INFO|mobi|MOBI|name|NAME|aero|AERO|asia|ASIA|jobs|JOBS|museum|MUSEUM|in|IN|co|CO)\b" ControlToValidate="txtEmail"
                                     ErrorMessage="Invalid Email Format" ValidationGroup="ProfileControl" ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblCompany" runat="server">Company:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtCompany" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblTitle" runat="server">Title:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtTitle" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblPrimaryFunction" runat="server">Primary Job Function:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="cmbPrimaryFunction" CssClass="txtBoxEditProfileForDropdown"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblPhoto" runat="server">Photo</asp:Label>
                                </td>
                                <td>
                                   
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="trWebAccount">
                <td>
                    <div class="BorderDiv">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <%--  Anil Changess for issue 12718--%>
                                        <td class="tdWebAccountInfo" colspan="2">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="5%">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        Web Account Information
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftColumnEditProfile">
                                            <asp:Label ID="lblWebUID" runat="server"><span class="RequiredField">*</span>User ID:</asp:Label></span>
                                        </td>
                                        <td class="RightColumn">
                                            <asp:TextBox ID="txtUserID" runat="server" CssClass="txtBoxPasswordProfile" SkinID="RequiredTextBox"
                                                AutoPostBack="True"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUserID"
                                                ValidationGroup="ProfileControl" ErrorMessage="User ID Required" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <%-- <asp:LinkButton ID="lnkCheckAvailable" ValidationGroup="ss" runat="server" Text="Check Availability"></asp:LinkButton>--%>
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftColumnEditProfile">
                                            <asp:Label ID="lblPWD" runat="server"><span class="RequiredField">*</span>Password:</asp:Label>
                                        </td>
                                        <td class="RightColumn">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtBoxPasswordProfile" SkinID="RequiredTextBox"
                                                TextMode="Password"></asp:TextBox>
                                            <asp:CompareValidator ID="valPWDMatch" runat="server" ControlToValidate="txtRepeatPWD"
                                                ErrorMessage="Passwords Must Match" ControlToCompare="txtPassword" ForeColor="Red"
                                                Display="Dynamic"></asp:CompareValidator>
                                            <%--Aparna issue no  12964 Add new label--%>
                                            <asp:Label ID="lblpasswordlengthError" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftColumnEditProfile">
                                            <asp:Label ID="lblRepeatPWD" runat="server"><span class="RequiredField">*</span>Repeat Password:</asp:Label>
                                        </td>
                                        <td class="RightColumn">
                                            <asp:TextBox ID="txtRepeatPWD" runat="server" CssClass="txtBoxPasswordProfile" TextMode="Password"
                                                SkinID="RequiredTextBox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="valPWDRequired" runat="server" ControlToValidate="txtPassword"
                                                ErrorMessage="Password Required" ValidationGroup="ProfileControl" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftColumnEditProfile">
                                            <asp:Label ID="lblPasswordHintQuestion" runat="server"><span class="RequiredField">*</span>Hint Question:</asp:Label>
                                        </td>
                                        <td class="RightColumn">
                                            <asp:DropDownList ID="cmbPasswordQuestion" runat="server" CssClass="cmbBoxChoosColour">
                                                <asp:ListItem Value="My favorite color is?" Selected="True">My favorite color is?</asp:ListItem>
                                                <asp:ListItem Value="My mother's maiden name is?">My mother's maiden name is?</asp:ListItem>
                                                <asp:ListItem Value="I went to which high school?">I went to which high school?</asp:ListItem>
                                                <asp:ListItem Value="I was born in which city?">I was born in which city?</asp:ListItem>
                                                <asp:ListItem Value="My pet's name is?">My pet's name is?</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftColumnEditProfile">
                                            <asp:Label ID="lblPasswordHintAnswer" runat="server"><span class="RequiredField">*</span>Password Hint Answer:</asp:Label>
                                        </td>
                                        <td class="RightColumn">
                                            <asp:TextBox ID="txtPasswordHintAnswer" runat="server" SkinID="RequiredTextBox" CssClass="txtBoxPasswordProfile"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="valPasswordHintRequired" runat="server" ControlToValidate="txtPasswordHintAnswer" ValidationGroup="ProfileControl" ErrorMessage="Hint Answer Required" ForeColor="Red"   
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <%--  end --%>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtUserID" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="BorderDiv">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <%--  Anil Changess for issue 12718--%>
                                        <td class="tdContactInfo" colspan="3">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="5%">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        Contact Information
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--  end--%>
                                    </tr>
                                    <tr>
                                        <td class="LeftColumnEditProfile">
                                            Select Address Type:
                                        </td>
                                        <td class="RightColumn">
                                            <asp:DropDownList ID="ddlAddressType" CssClass="cmbUserProfileBussinessAdress" runat="server"
                                                AutoPostBack="true">
                                                <asp:ListItem>Business Address</asp:ListItem>
                                                <asp:ListItem>Home Address</asp:ListItem>
                                                <asp:ListItem>Billing Address</asp:ListItem>
                                                <asp:ListItem>PO Box Address</asp:ListItem>
                                            </asp:DropDownList>
                                            <img runat="server" style="visibility: hidden;" id="imgProcessing" alt="Animated processing image URL not set" />
                                            <asp:CheckBox ID="chkPrefAddress" CssClass="cb" runat="server" Text="Preferred Address" AutoPostBack="True" />
                                            </td>
                                        <td class="RightColumn">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <!-- Address Line  Rows -->
                                    <tr id="trAddressLine1" runat="server">
                                        <td class="LeftColumnEditProfile">
                                            <asp:Label ID="lblAddress" runat="server">Address:</asp:Label>
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trHomeAddressLine1" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile">
                                            <asp:Label ID="Label2" runat="server">Address:</asp:Label>
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtHomeAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trBillingAddressLine1" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile">
                                            <asp:Label ID="Label3" runat="server">Address:</asp:Label>
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtBillingAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trPOBoxAddressLine1" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile">
                                            <asp:Label ID="Label8" runat="server">Address:</asp:Label>
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtPOBoxAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <!-- Address Line 2 Rows -->
                                    <tr id="trAddressLine2" runat="server">
                                        <td class="LeftColumnEditProfile">
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trHomeAddressLine2" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile">
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtHomeAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trBillingAddressLine2" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile">
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtBillingAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trPOBoxAddressLine2" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile">
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtPOBoxAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <!-- Address Line 3 Rows -->
                                    <tr id="trAddressLine3" runat="server">
                                        <td class="LeftColumnEditProfile">
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trHomeAddressLine3" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile">
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtHomeAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trBillingAddressLine3" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile">
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtBillingAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trPOBoxAddressLine3" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile">
                                        </td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtPOBoxAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%--  Anil Changess for issue 12718--%>
                                    <tr id="trCity" runat="server">
                                        <td class="LeftColumnEditProfile">
                                            <asp:Label ID="lblCityStateZip" runat="server">City:</asp:Label>
                                        </td>
                                        <td class="RigthColumnContactBold">
                                            <asp:TextBox ID="txtCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                            <span class="SpanState">
                                                <asp:Label ID="Label5" runat="server">State:</asp:Label>
                                            </span>
                                            <asp:DropDownList ID="cmbState" CssClass="cmbUserProfileState" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trHomeCity" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile" colspan="1">
                                            <asp:Label ID="Label9" runat="server">City:</asp:Label>
                                        </td>
                                        <td class="RigthColumnContactBold">
                                            <asp:TextBox ID="txtHomeCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                            <span class="SpanState">
                                                <asp:Label ID="Label111" runat="server">State:</asp:Label>
                                            </span>
                                            <asp:DropDownList ID="cmbHomeState" CssClass="cmbUserProfileState" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trBillingCity" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile" colspan="1">
                                            <asp:Label ID="Label10" runat="server">City:</asp:Label>
                                        </td>
                                        <td class="RigthColumnContactBold">
                                            <asp:TextBox ID="txtBillingCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                            <span class="SpanState">
                                                <asp:Label ID="Label22" runat="server">State:</asp:Label>
                                            </span>
                                            <asp:DropDownList ID="cmbBillingState" CssClass="cmbUserProfileState" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trPOBoxCity" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile" colspan="1">
                                            <asp:Label ID="Label11" runat="server">City:</asp:Label>
                                        </td>
                                        <td class="RigthColumnContactBold">
                                            <asp:TextBox ID="txtPOBoxCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                            <span class="SpanState">
                                                <asp:Label ID="Label14" runat="server">State:</asp:Label>
                                            </span>
                                            <asp:DropDownList ID="cmbPOBoxState" CssClass="cmbUserProfileState" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trCountry" runat="server">
                                        <td class="LeftColumnEditProfile" colspan="1">
                                            <asp:Label ID="lblCountry" runat="server">Country:</asp:Label>
                                        </td>
                                        <td align="left" class="RigthColumnContactBold">
                                            <asp:DropDownList ID="cmbCountry" CssClass="cmbUserProfileCountry" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <span class="SpanZipCode">
                                                <asp:Label ID="Label23" runat="server">ZIP Code:</asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trHomeCountry" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile" colspan="1">
                                            <asp:Label ID="Label13" runat="server">Country:</asp:Label>
                                        </td>
                                        <td class="RigthColumnContactBold">
                                            <asp:DropDownList ID="cmbHomeCountry" CssClass="cmbUserProfileCountry" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <span class="SpanZipCode">
                                                <asp:Label ID="Label24" runat="server">ZIP Code:</asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtHomeZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trBillingCountry" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile" colspan="1">
                                            <asp:Label ID="Label16" runat="server">Country:</asp:Label>
                                        </td>
                                        <td class="RigthColumnContactBold">
                                            <asp:DropDownList ID="cmbBillingCountry" CssClass="cmbUserProfileCountry" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <span class="SpanZipCode">
                                                <asp:Label ID="Label19" runat="server">ZIP Code:</asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtBillingZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trPOBoxCountry" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile" colspan="1">
                                            <asp:Label ID="Label17" runat="server">Country:</asp:Label>
                                        </td>
                                        <td class="RigthColumnContactBold">
                                            <asp:DropDownList ID="cmbPOBoxCountry" CssClass="cmbUserProfileCountry" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <span class="SpanZipCode">
                                                <asp:Label ID="Label20" runat="server">ZIP Code:</asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtPOBoxZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
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
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblPhone" runat="server">(Area Code) Phone:</asp:Label>
                                </td>
                                <%--Neha Issue 14750 ,02/26/13, added css for phoneareacode and faxAreacode--%>
                                <td class="RightColumn tdRightColumnAreacodephone">
                                    <rad:RadMaskedTextBox ID="txtPhoneAreaCode" CssClass="txtUserProfileAreaCodeSmall" runat="server" Mask="(###)" Width="48px"></rad:RadMaskedTextBox>
                                     <rad:RadMaskedTextBox ID="txtPhone" CssClass="txtUserProfileAreaCode" runat="server" Mask="###-####" Width="65px"></rad:RadMaskedTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblFax" runat="server">(Area Code) Fax:</asp:Label>
                                </td>
                                <td class="RightColumn tdRightColumnAreacodephone">
                                    <rad:RadMaskedTextBox ID="txtFaxAreaCode" CssClass="txtUserProfileAreaCodeSmall" runat="server" Mask="(###)" Width="48px"> </rad:RadMaskedTextBox>
                                    <rad:RadMaskedTextBox ID="txtFaxPhone" runat="server" CssClass="txtUserProfileAreaCode" Mask="###-####" Width="65px"></rad:RadMaskedTextBox>
                                   </td>
                            </tr>
                            <%-- end--%>
                            <%--   <tr>
                                <td class="LeftColumnEditProfile">
                                    <p>
                                        &nbsp;</p>
                                </td>
                            </tr>--%>
                            <%--   <tr>
                        <td class="SecAccount Information">
                        </td>
                    </tr>
                        <tr>
                        <td class="LeftColumnEditProfile">
                            <asp:Label ID="lblWebUID" runat="server"><span class="RequiredField">*</span>User ID</asp:Label></span>
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtUserID" runat="server" SkinID="RequiredTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUserID"
                                ErrorMessage="User ID Required" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnEditProfile">
                            <asp:Label ID="lblPWD" runat="server"><span class="RequiredField">*</span>Password</asp:Label>
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" SkinID="RequiredTextBox"></asp:TextBox>
                            <asp:CompareValidator ID="valPWDMatch" runat="server" ControlToValidate="txtRepeatPWD"
                                ErrorMessage="Passwords Must Match" ControlToCompare="txtPassword" Display="Dynamic"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnEditProfile">
                            <asp:Label ID="lblRepeatPWD" runat="server"><span class="RequiredField">*</span>Repeat Password</asp:Label>
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtRepeatPWD" runat="server" TextMode="Password" SkinID="RequiredTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valPWDRequired" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Password Required" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnEditProfile">
                            <asp:Label ID="lblPasswordHintQuestion" runat="server"><span class="RequiredField">*</span>Password Hint Question</asp:Label>
                        </td>
                        <td class="RightColumn">
                            <asp:DropDownList ID="cmbPasswordQuestion" runat="server">
                                <asp:ListItem Value="My favorite color is?" Selected="True">My favorite color is?</asp:ListItem>
                                <asp:ListItem Value="My mother's maiden name is?">My mother's maiden name is?</asp:ListItem>
                                <asp:ListItem Value="I went to which high school?">I went to which high school?</asp:ListItem>
                                <asp:ListItem Value="I was born in which city?">I was born in which city?</asp:ListItem>
                                <asp:ListItem Value="My pet's name is?">My pet's name is?</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnEditProfile">
                            <asp:Label ID="lblPasswordHintAnswer" runat="server"><span class="RequiredField">*</span>Password Hint Answer</asp:Label>
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtPasswordHintAnswer" runat="server" SkinID="RequiredTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valPasswordHintRequired" runat="server" ControlToValidate="txtPasswordHintAnswer"
                                ErrorMessage="Hint Answer Required" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="font-weight: bold;">
                            <asp:CheckBox ID="chkUseSocialMediaPhoto" runat="server" Text="Use my LinkedIn Photo for this Profile" />
                            <br />
                            <asp:CheckBox ID="chkSynchronizeProfile" runat="server" Text="I agree to synchronize my social network profile with Aptify"
                                TextAlign="right" /><br />
                            <asp:HyperLink ID="hypSocialNetworkSynchText" runat="server" Target="_new" ForeColor="Red"></asp:HyperLink>
                        </td>
                    </tr>--%>
                        </table>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="trmemberinfo">
                <td>
                    <div class="BorderDiv">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" id="memberinfo" runat="server">
                                    <tr>
                                        <td class="tdMemberAccountInfo" colspan="2">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="5%">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
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
                                        <td class="LeftMemberInfo" align="right" width="137px">
                                            <asp:Label ID="lblmembershipType" runat="server">Membership Type:</asp:Label>
                                        </td>
                                        <td class="RightColumnMemberInfo">
                                            <asp:Label ID="lblMemberTypeVal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftMemberInfo" align="right" width="137px">
                                            <asp:Label ID="lblStartDate" runat="server">Start Date:</asp:Label>
                                        </td>
                                        <td class="RightColumnMemberInfo">
                                            <asp:Label ID="lblStartDateVal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftMemberInfo" align="right" width="137px">
                                            <asp:Label ID="lblEndDate" runat="server">End Date:</asp:Label>
                                        </td>
                                        <td class="RightColumnMemberInfo">
                                            <asp:Label ID="lblEndDateVal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftMemberInfo" align="right" width="137px">
                                            <asp:Label ID="lblStatus" runat="server">Status:</asp:Label>
                                        </td>
                                        <td class="RightColumnMemberInfo">
                                            <asp:Label ID="lblStatusVal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="txtUserID" EventName="TextChanged" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="BorderDiv">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="tdTopicofInterestInfo">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                Select Topics of Interest to You
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="cblTopicofInterest" CssClass="test" runat="server" RepeatColumns="4"
                                        RepeatDirection="Horizontal" Width="100%">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="submitBtn" Text="Submit" ValidationGroup="ProfileControl"
                                    Height="23px" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="submitBtn" Text="Cancel" CausesValidation="False" Height="23px" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="content-container clearfix">
</div>
<rad:RadWindow ID="radwinPassword" runat="server" Width="350px" Modal="True" BackColor="#DADADA"
    VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Change Password" Skin="Default">
   
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
                                            <td align="right" valign="top" class="tablecontrolsfontLogin" style="text-align: left; padding-left: 18px; padding-top: 5px;">  <font color="red">*</font>
                                            <asp:Label ID="Label15" runat="server">Current Password:</asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtoldpassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="tablecontrolsfontLogin" style="text-align: left; padding-left: 4px; padding-left: 32px; padding-top: 5px;"> <font color="red">*</font>
                                            <asp:Label ID="lblPassword" runat="server">New Password:</asp:Label>&nbsp;
                                            </td>
                                            <td style="padding-top: 2px;">
                                                <asp:TextBox ID="txtNewPassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="tablecontrolsfontLogin" style="text-align: left; padding-left: 4px; padding-left: 20px; padding-top: 5px;">  <font color="red">*</font>
                                            <asp:Label ID="Label18" runat="server">Repeat Password:</asp:Label>&nbsp;
                                            </td>
                                            <td style="padding-top: 2px;">
                                                <asp:TextBox ID="txtRepeat" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="tablecontrolsfontLogin" style="text-align: left; padding-left: 4px; padding-left: 182px; padding-top: 10px;">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return ShowvalidationErrorMsg()" CssClass="submitBtn" Height="23px" />&nbsp;
                                <asp:Button ID="btnCancelpop" runat="server" Text="Cancel" CssClass="submitBtn" CausesValidation="false" Height="23px" />
                            </td>
                            </tr>
                        </table>
                    </span>
                </div>
                <div>
                    <table>
                        <tr>
                            <%--Neha issue no 12591 Updated ErrorMessage--%>
                            <td class="tdValidationErrormessage"">
                                <asp:Label ID="lblErrormessage" runat="server" Text=""></asp:Label>
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
<rad:RadWindow ID="radDuplicateUser" runat="server" Width="650px" Height="120px" Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
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
                    <asp:Button ID="btnok" runat="server" Text="OK" Width="50px" class="submitBtn" OnClick="btnok_Click" ValidationGroup="ok" Height="23px" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </ContentTemplate>
</rad:RadWindow>
<rad:RadWindow ID="radwindowProfileImage" runat="server" Width="500px" Height="600px"  Modal="True" BackColor="#DADADA" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Profile Image" Behavior="None" Skin="Default" IconUrl="~/Images/personal-icon.png" MaxWidth="500px" CssClass="ProfileWindow">
   <ContentTemplate>
        <panel id="ProfileImagePanel" runat="server">
            <table  style="padding-left:10px;padding-right:5px; padding-top:10px;background-color:#f4f3f1;height:100%; width:100%;">
                <tr>
                    <td colspan="2">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="LableImageUploadText" runat="server" Font-Size="8pt" Font-Names="Segoe UI"> </asp:Label>
                                    <br />
                                    <div style="display: none">
                                        <asp:Label ID="LableImageSaveIndicator" runat="server" Font-Size="8pt" Font-Names="Segoe UI" Visible="false"></asp:Label>
                                    </div>
                                </td>
                            </tr> 
                            <tr style="height: 30px">
                                <td nowrap="nowrap">
                                    <table>
                                        <tr>
                                            <td valign="bottom">
                                                <rad:RadUpload runat="server" id="radUploadProfilePhoto" ControlObjectsVisibility="None" OnClientFileSelected="UploadImage" Localization-Select="Browse..." AllowedFileExtensions=".gif, .jpg, .bmp, png" Width="100%" /> 
                                            </td>
                                            <td valign="bottom">
                                                &nbsp;&nbsp;<asp:Button ID="btnRemovePhoto" runat="server" CausesValidation="False" Text="Remove"
                                                    Width="70px" CssClass="submitBtn" Height="23px" />&nbsp;
                                                <asp:Button ID="btnUpload" runat="server" CssClass="submitBtn" CausesValidation="False"
                                                    Width="60px" Text="Upload" Style="display: none" Height="23px" />
                                                <%--<asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="radUploadProfilePhoto"
                                                    ErrorMessage="Invalid Image File" Display="Dynamic" ForeColor="Red" ValidationGroup="ProfileControl"
                                                    ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])$)"></asp:RegularExpressionValidator>--%> 
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table> 
                    </td>
                </tr> 
                <tr>
                    <td colspan="2" valign="top" style="height: 400px">
                      <%--Neha issue no 14722,Added property for cropping issue--%>
                        <rad:RadImageEditor ID="radImageEditor" runat="server" Width="450px" Height="400px" AllowedSavingLocation="Server" OnClientImageChanged="EnabledImageSaveButton"  
                            OnClientImageLoad="ZoomBestFit" OnClientToolsDialogClosed="OnClientToolsDialogClosed" OnClientImageChanging="OnClientImageChanging" OnClientCommandExecuted="OnClientCommandExecuted" CanvasMode="No" >    
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
                        <asp:Label ID="lblCropTip" runat="server" Font-Size="8pt" Font-Names="Segoe UI" Text="After cropping a photo, click Crop and then Apply."></asp:Label>
                        <%--Suraj S Issue 16495 ,if the browser is"Microsoft Internet Explorer thenwe provide the following message.--%>
                          <asp:Label ID="lblIEUserMsg" runat="server" Font-Size="8pt" Font-Names="Segoe UI" Text="Internet Explorer users may need to refresh the image before cropping."></asp:Label>
                    </td>
                </tr>  
                <tr>
                    <td></td>
                    <td align="right" style="padding-right:7px;padding-bottom:7px;" nowrap="nowrap" > 
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnCropImage" class="submitBtn" runat="server" Width="70px" Text="Crop" OnClientClick="return CropImage();" Height="23px" />&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnSaveProfileImage" class="submitBtn" runat="server" Width="70px" Text="Apply" CausesValidation="false" Height="23px" UseSubmitBehavior="false"/>&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelProfileImage" Width="70px" runat="server" Text="Cancel" class="submitBtn" OnClick="btnCancelProfileImage_Click" CausesValidation="false" Height="23px"  />
                                </td>
                            </tr>
                        </table>                            
	                    
                    </td>            
                </tr>
            </table>
	   </panel>
    </ContentTemplate>
</rad:RadWindow>
<cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
<cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
