<%@ Register Src="~/UserControls/Aptify_Custom__c/CompanyContact__c.ascx" TagName="CompanyContact__c"
    TagPrefix="uc3" %>
<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Profile__c2016-11-11.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.ProfileControl__c" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagName="SyncProfile" TagPrefix="uc1" Src="~/UserControls/Aptify_Customer_Service/SynchProfile.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/TopicCodeControl__c.ascx" TagName="TopicCodeControl"
    TagPrefix="uc2" %>

<%@ Register Src="~/UserControls/Aptify_Custom__c/PendingChangesDetails__c.ascx"
    TagName="PendingChangesDetailsControl" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/AdditionalOrganizations__c.ascx"
    TagName="AdditionalOrganization" TagPrefix="uc4" %>
<script type="text/javascript" language="javascript">
    //Anil  for Issue 6640
    window.history.forward(1);

    function ClientItemSelected(sender, e) {
        document.getElementById("<%=hfCompanyID.ClientID %>").value = e.get_value();
        getAddress();
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
        var webmethod = "http://<%=Request.Url.Host.ToString() %>" + ":" + "<%= Request.Url.Port.ToString() %><%= System.Web.Configuration.WebConfigurationManager.AppSettings("virtualdir").ToString() %>webservices/PopulatecompanyBusinessAddress.asmx/PopulateCompanyStreetAddress";
        var vParam = $('#<%= hfCompanyID.ClientID%>').val();
        vParam = vParam.replace('&quot;', '&quot;&quot;');
        var parmeter = JSON.stringify({ 'CompanyID': vParam });
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

     function companyitemselecting(sender, e) {

         document.getElementById('<%=hfCompanyID11.ClientID%>').value = "-1"
    }
    function myShowFunction() {

        var radwindow1 = $find('<%=RadAddNew.ClientID%>');

        radwindow1.show();

    }



  

</script>
<style type="text/css">
    .test .ajax__calendar_container {
        padding: 0px;
        margin: 0px;
    }

    .watermarked {
        background-color: #FFFFFF;
        border: 1px solid #A9A9A9;
        color: #BEBEBE;
        padding: 2px 0 0 2px;
        -moz-box-sizing: border-box;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
    }
</style>
<asp:HiddenField ID="hfCompanyID11" Value="-1" runat="server" />
<asp:HiddenField ID="hfCompanyID" Value="-1" runat="server" />
<asp:Literal ID="ltlImageEditorStyle" runat="server"></asp:Literal>
<asp:Label ID="lblValidAddress" ForeColor="Red" runat="server"></asp:Label>
<asp:Label ID="lblValidCity" ForeColor="Red" runat="server"></asp:Label>
<asp:Label ID="lblValidCountry" ForeColor="Red" runat="server"></asp:Label>
<asp:Label ID="lblValidPostal" ForeColor="Red" runat="server"></asp:Label>
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
                            <%--Added PendingChangesDetails grid by Ganesh I on 03/23/2014   --%>
                            <div id="divPendingChange" runat="server">
                                <tr>
                                    <td class="tdPersonalInfo" colspan="2">
                                        <table width="100%" cellpadding="0" cellspacing="0" style="height: auto">
                                            <tr>
                                                <td width="5%">&nbsp;
                                                </td>
                                                <td align="left">Pending Changes Details
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" cellpadding="0" cellspacing="0" style="height: auto">
                                            <tr>
                                                <td>
                                                    <uc4:PendingChangesDetailsControl ID="PendingChangesDetails1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <asp:Button ID="btnSubmit2" runat="server" CssClass="submitBtn" Text="Submit" ValidationGroup="ProfileControl" />
                                <asp:Button ID="btnCancel2" runat="server" CssClass="submitBtn" Text="Cancel" CausesValidation="False" />
                            </div>

                            <tr>
                                <%--  Anil Changess for issue 12718--%>
                                <td class="tdPersonalInfo" colspan="2">
                                    <table width="100%" cellpadding="0" cellspacing="0" style="height: auto">
                                        <tr>
                                            <td width="5%">&nbsp;
                                            </td>
                                            <td align="left">Personal Information
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
                                    <asp:Label ID="lblSalutation" runat="server">Salutation:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="cmbSalutation" CssClass="txtBoxEditProfileForDropdown" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblPreferredSalutation" runat="server">Preferred Salutation:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox SkinID="RequiredTextBox" CssClass="txtBoxEditProfile" ID="txtPreferredSalutation"
                                        runat="server"></asp:TextBox>
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
                            <!-- Field added for other name-->
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblOtherName" runat="server">Other Name:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox SkinID="RequiredTextBox" ID="txtOtherName" CssClass="txtBoxEditProfile"
                                        runat="server"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOtherName"
                                        ValidationGroup="ProfileControl" ErrorMessage="Other Name Required" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <!-- Field added for other name-->
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <span class="RequiredField">*</span><asp:Label ID="lblDob" runat="server">Date of Birth:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <%--  <asp:TextBox ID="txtDob" runat="server" CssClass="txtBoxEditProfile" Width="150px" ></asp:TextBox>--%>
                                    <telerik:RadDatePicker ID="txtDob" runat="server" Calendar-ShowOtherMonthsDays="false"
                                        MinDate="01/01/1777" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                    </telerik:RadDatePicker>
                                    <%--  <Ajax:CalendarExtender ID="CalendarExtender2" runat="server"  TargetControlID="txtDob">
                                         </Ajax:CalendarExtender>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationExpression="^[0-9m]{1,2}/[0-9d]{1,2}/[0-9y]{4}$"
                                        ForeColor="Red" ErrorMessage="" ControlToValidate="txtDob" SetFocusOnError="True"
                                        runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDob"
                                        Display="Static" ErrorMessage="Date of birth required" Font-Size="X-Small" ForeColor="Red"
                                        ValidationGroup="ProfileControl"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblGender" runat="server">Gender:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="cmbGender" CssClass="txtBoxEditProfileForDropdown" runat="server">
                                        <asp:ListItem Value="0">Male</asp:ListItem>
                                        <asp:ListItem Value="1">Female</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">Unknown</asp:ListItem>
                                    </asp:DropDownList>
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
                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
                                        ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ValidationGroup="ProfileControl"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblCompany" runat="server">Company:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtCompany" CssClass="txtBoxEditProfile" runat="server" onchange="getAddress();"></asp:TextBox>
                                    <Ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="autoComplete"
                                        CompletionInterval="10" CompletionListElementID="divwidth" CompletionSetCount="12"
                                        EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetCompanyList" ServicePath="~/GetCompanyList__c.asmx"
                                        TargetControlID="txtCompany" OnClientItemSelected="ClientItemSelected">
                                    </Ajax:AutoCompleteExtender>
                                    <Ajax:TextBoxWatermarkExtender ID="WatermarkExtender1" runat="server"
                                        TargetControlID="txtCompany" WatermarkText="Type Company Name Here" WatermarkCssClass="watermarked" />

                                </td>
                                <td class="RightColumn">&nbsp;
                                </td>
                            </tr>


                            <tr>
                                <td class="LeftColumnEditProfile"></td>
                                <td class="RightColumn">
                                    <asp:LinkButton ID="LinkBtnpopup" runat="server" Text="New Company?" CausesValidation="false"></asp:LinkButton>
                                </td>
                            </tr>
                            <%--+++++++++++++++++++++++++++++++++++++++++++++--%>
                            <tr id="trAddressLine1" runat="server">
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblAddress" runat="server">Address:</asp:Label>
                                </td>
                                <td class="RightColumn" colspan="2">
                                    <asp:TextBox ID="txtAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trAddressLine2" runat="server">
                                <td class="LeftColumnEditProfile"></td>
                                <td class="RightColumn" colspan="2">
                                    <asp:TextBox ID="txtAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr id="trAddressLine3" runat="server">
                                <td class="LeftColumnEditProfile"></td>
                                <td class="RightColumn" colspan="2">
                                    <asp:TextBox ID="txtAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                </td>
                            </tr>

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
                            <%--+++++++++++++++++++++++++++++++++++++++++++++--%>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblTitle" runat="server">Job Title:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="cmbJobTitle" CssClass="txtBoxEditProfile" runat="server"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblEmploymentStatus" runat="server">Employment Status:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="cmbempstatus" CssClass="txtBoxEditProfileForDropdown" runat="server">
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
                            <%--   <tr>
                                <td class="LeftColumnEditProfile" colspan="2">
                                    <uc3:CompanyContact__c ID="CompanyContact__c1" runat="server" />
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
                                                    <td width="5%">&nbsp;
                                                    </td>
                                                    <td align="left">Web Account Information
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
                                            <asp:RequiredFieldValidator ID="valPasswordHintRequired" runat="server" ControlToValidate="txtPasswordHintAnswer"
                                                ValidationGroup="ProfileControl" ErrorMessage="Hint Answer Required" ForeColor="Red"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <%--  end --%>
                                    <tr>
                                        <td></td>
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
                                                    <td width="5%">&nbsp;
                                                    </td>
                                                    <td align="left">Contact Information
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--  end--%>
                                    </tr>
                                    <tr>
                                        <td class="LeftColumnEditProfile">Select Address Type:
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
                                            <asp:UpdatePanel ID="UpdatePanelCommodity" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:RadioButtonList ID="rblPAddress" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                        <asp:ListItem>Home Address</asp:ListItem>
                                                        <asp:ListItem>Business Address</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:Label ID="Label34" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:Label ID="lblPAdressError" runat="server" Text=""></asp:Label>
                                            <div id="output">
                                            </div>
                                        </td>
                                        <td class="RightColumn">&nbsp;
                                        </td>
                                    </tr>
                                    <!-- Address Line  Rows -->

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

                                    <tr id="trHomeAddressLine2" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile"></td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtHomeAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trBillingAddressLine2" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile"></td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtBillingAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trPOBoxAddressLine2" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile"></td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtPOBoxAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <!-- Address Line 3 Rows -->

                                    <tr id="trHomeAddressLine3" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile"></td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtHomeAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trBillingAddressLine3" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile"></td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtBillingAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trPOBoxAddressLine3" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile"></td>
                                        <td class="RightColumn" colspan="2">
                                            <asp:TextBox ID="txtPOBoxAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%--  Anil Changess for issue 12718--%>

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

                                    <tr id="trHomeCountry" runat="server" visible="false">
                                        <td class="LeftColumnEditProfile" colspan="1">
                                            <asp:Label ID="Label13" runat="server">Country:</asp:Label>
                                        </td>
                                        <td class="RigthColumnContactBold">
                                            <asp:DropDownList ID="cmbHomeCountry" CssClass="cmbUserProfileCountry" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblErrorHomeCountry" runat="server" Text="" />
                                            <span class="SpanZipCode">
                                                <asp:Label ID="Label24" runat="server">ZIP Code:</asp:Label>
                                                <asp:Label Class="label-title" ID="Label37" runat="server">County</asp:Label>
                                                <asp:TextBox ID="txtBillingCountry" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtHomeZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label33" runat="server">County:</asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtHomeCounty" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
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
                                <asp:AsyncPostBackTrigger ControlID="rblPAddress" EventName="SelectedIndexChanged" />
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
                                    <rad:RadMaskedTextBox ID="txtIntlCode" CssClass="txtUserProfileAreaCodeSmall" runat="server"
                                        Mask="(###)" Width="30%">
                                    </rad:RadMaskedTextBox>
                                    <rad:RadMaskedTextBox ID="txtPhoneAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                        runat="server" Mask="(###)" Width="48px">
                                    </rad:RadMaskedTextBox>
                                    <rad:RadMaskedTextBox ID="txtPhone" CssClass="txtUserProfileAreaCode" runat="server"
                                        Mask="###-####" Width="65px">
                                    </rad:RadMaskedTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblFax" runat="server">(Area Code) Fax:</asp:Label>
                                </td>
                                <td class="RightColumn tdRightColumnAreacodephone">
                                    <rad:RadMaskedTextBox ID="txtFaxAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                        runat="server" Mask="(###)" Width="48px">
                                    </rad:RadMaskedTextBox>
                                    <rad:RadMaskedTextBox ID="txtFaxPhone" runat="server" CssClass="txtUserProfileAreaCode"
                                        Mask="###-####" Width="65px">
                                    </rad:RadMaskedTextBox>
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
            <tr>

                <td>
                    <div class="cai-form">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div id="Div1" runat="server">
                                    <span class="form-title">Additional Company Information</span>

                                    <div class="form-section-half-border">

                                        <div class="field-group">
                                            <asp:Label ID="Label35" runat="server"><span class="RequiredField">*</span>Company Name</asp:Label>
                                            <asp:TextBox ID="txtCompany11" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                                            <Ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" BehaviorID="autoComplete1"
                                                CompletionInterval="10" CompletionListElementID="divwidth" CompletionSetCount="12"
                                                EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetCompanyList" ServicePath="~/GetCompanyList__c.asmx"
                                                TargetControlID="txtCompany11" OnClientItemSelected="ClientItemSelected">
                                            </Ajax:AutoCompleteExtender>
                                            <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                TargetControlID="txtCompany11" WatermarkText="Type Company Name Here" WatermarkCssClass="watermarked" />
                                            <asp:Button ID="btnAddNew" Text="Add New Company" runat="server" OnClientClick="myShowFunction()" CssClass="submitBtn" />
                                        </div>
                                        <div class="field-group">
                                            <asp:Label ID="Label36" runat="server"><span class="RequiredField">*</span>Job Title</asp:Label>
                                            <asp:DropDownList ID="cmbJobTitle11" CssClass="txtBoxEditProfile" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="field-group">
                                            <asp:Button ID="btnAdd" Text="Add" runat="server" ValidationGroup="ColleagueInfo" CssClass="submitBtn" />
                                            <asp:Label ID="lblDateError" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-section-half">
                                        <asp:GridView ID="grvCompany" runat="server" DataKeyNames="ID" AllowSorting="False" AutoGenerateColumns="False" Width="100%" GridLines="Horizontal" AllowPaging="true" BorderColor="#CCCCCC" BorderWidth="1px" PageSize="10">
                                            <HeaderStyle BackColor="#f58844" ForeColor="black" HorizontalAlign="Left" />
                                            <AlternatingRowStyle BackColor="#fce2d2" ForeColor="black" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                            CssClass="submitBtn" Text="Delete" CommandArgument='<%# Eval("ID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ID" HeaderText="" ReadOnly="true" Visible="false" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ReadOnly="true" />
                                                <asp:BoundField DataField="JobTitle" HeaderText="Job Title" ReadOnly="true" />
                                                <asp:BoundField DataField="EntID" HeaderText="" ReadOnly="true" Visible="false" />
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="txtUserID" EventName="TextChanged" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
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
                                                    <td width="5%">&nbsp;
                                                    </td>
                                                    <td align="left">Membership Information
                                                    </td>
                                                    <td align="right">&nbsp;
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
                                            <asp:Label ID="lblStartDate" runat="server" Visible="false">Start Date:</asp:Label>
                                        </td>
                                        <td class="RightColumnMemberInfo">
                                            <asp:Label ID="lblStartDateVal" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftMemberInfo" align="right" width="137px">
                                            <asp:Label ID="lblEndDate" runat="server" Visible="false">End Date:</asp:Label>
                                        </td>
                                        <td class="RightColumnMemberInfo">
                                            <asp:Label ID="lblEndDateVal" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftMemberInfo" align="right" width="137px">
                                            <asp:Label ID="lblStatus" runat="server" Visible="false">Status:</asp:Label>
                                        </td>
                                        <td class="RightColumnMemberInfo">
                                            <asp:Label ID="lblStatusVal" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftMemberInfo" align="right" width="137px">
                                            <asp:Label ID="Label30" runat="server">Membership Grade:</asp:Label>
                                        </td>
                                        <td class="RightColumnMemberInfo">
                                            <asp:Label ID="lblmembershipGradeval" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftMemberInfo" align="right" width="137px">
                                            <asp:Label ID="Label31" runat="server">Membership Number:</asp:Label>
                                        </td>
                                        <td class="RightColumnMemberInfo">
                                            <asp:Label ID="lblMembershipNumber" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
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
                        <%--  <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="tdTopicofInterestInfo">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="5%">&nbsp;
                                            </td>
                                            <td align="left">Select Topics of Interest to You
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
                                    <uc2:TopicCodeControl ID="TopicCodeControl1" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <%--  </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="TopicCodeControl1" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </div>
                </td>
            </tr>
            <%--added by Rajesh K--%>
            <tr id="trAdddOrganization" runat="server" visible="false">
                <td>
                    <div class="BorderDiv">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="tdTopicofInterestInfo">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="5%">&nbsp;
                                                    </td>
                                                    <td align="left">Select Additional Organization
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table width="70%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <uc4:AdditionalOrganization ID="ucAdditionalOrganization" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <%--<div class="demo-container">
                    <rad:RadEditor runat="server" ID="REWebDescriptionTest" 
                    SkinID="DefaultSetOfTools" Height="475px">
                    
                    </rad:RadEditor>
                </div>--%>
            </tr>
            <tr>
                <td align="center">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="submitBtn" Text="Submit" ValidationGroup="ProfileControl"
                                    Height="23px" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="submitBtn" Text="Cancel"
                                    CausesValidation="False" Height="23px" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Label ID="lblPendingChangesMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
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
                                            <td align="right" valign="top" class="tablecontrolsfontLogin" style="text-align: left; padding-left: 18px; padding-top: 5px;">
                                                <font color="red">*</font>
                                                <asp:Label ID="Label15" runat="server">Current Password:</asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtoldpassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="tablecontrolsfontLogin" style="text-align: left; padding-left: 4px; padding-left: 32px; padding-top: 5px;">
                                                <font color="red">*</font>
                                                <asp:Label ID="lblPassword" runat="server">New Password:</asp:Label>&nbsp;
                                            </td>
                                            <td style="padding-top: 2px;">
                                                <asp:TextBox ID="txtNewPassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="tablecontrolsfontLogin" style="text-align: left; padding-left: 4px; padding-left: 20px; padding-top: 5px;">
                                                <font color="red">*</font>
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
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return ShowvalidationErrorMsg()"
                                        CssClass="submitBtn" Height="23px" />&nbsp;
                                    <asp:Button ID="btnCancelpop" runat="server" Text="Cancel" CssClass="submitBtn" CausesValidation="false"
                                        Height="23px" />
                                </td>
                            </tr>
                        </table>
                    </span>
                </div>
                <div>
                    <table>
                        <tr>
                            <%--Neha issue no 12591 Updated ErrorMessage--%>
                            <td class="tdValidationErrormessage">
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
                    <asp:Button ID="btnok" runat="server" Text="OK" Width="50px" class="submitBtn" OnClick="btnok_Click"
                        ValidationGroup="ok" Height="23px" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </ContentTemplate>
</rad:RadWindow>
<rad:RadWindow ID="radwindowProfileImage" runat="server" Width="500px" Height="600px"
    Modal="True" BackColor="#DADADA" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
    Title="Profile Image" Behavior="None" Skin="Default" IconUrl="~/Images/personal-icon.png"
    MaxWidth="500px" CssClass="ProfileWindow">
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
                <td class="style3"></td>
                <td class="RightColumn">
                    <asp:TextBox ID="txtCompanyAddress3" runat="server" Width="250px"></asp:TextBox>
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
                    <asp:DropDownList ID="cmbStateNew" CssClass="cmbUserProfileState" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label27" runat="server" Style="text-align: left">Country</asp:Label>
                </td>
                <td class="RightColumn">
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="cmbCountryNew" CssClass="cmbUserProfileCountry" runat="server" AutoPostBack="true">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label32" runat="server" Style="text-align: left">County</asp:Label>
                </td>
                <td class="RightColumn">
                    <asp:TextBox ID="txtFirmCounty" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="style3">
                    <asp:Label ID="Label25" runat="server" Style="text-align: left">(Intl Code)(Area Code) Phone</asp:Label>
                </td>
                <td class="RightColumn">
                    <asp:TextBox ID="txtcode" runat="server" CssClass="txtUserProfileAreaCodeSmall" Style="margin-left: 0px"></asp:TextBox>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="txtUserProfileAreaCode"></asp:TextBox>
                    <rad:RadMaskedTextBox ID="txtIntlCodeNew" CssClass="txtUserProfileAreaCodeSmall" runat="server" Mask="(###)" Width="25%"></rad:RadMaskedTextBox>

                    <rad:RadMaskedTextBox ID="txtPhoneAreaCodeNew" CssClass="txtUserProfileAreaCodeSmall" runat="server" Mask="(###)" Width="25%"></rad:RadMaskedTextBox>

                    <rad:RadMaskedTextBox ID="txtPhoneNew" CssClass="txtUserProfileAreaCode" runat="server" Mask="###-####" Width="45%"></rad:RadMaskedTextBox>

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



<rad:RadWindow ID="radAlternateCompany" runat="server" Width="560px" Height="550px" Modal="True"
    Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="New Company Record" Behavior="None">
    <ContentTemplate>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <table class="tblEditAtendee">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label38" runat="server" Text="Company Name"></asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="txtAlternateCompName" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label39" runat="server" Style="text-align: left">Address</asp:Label>
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtAlternateCmpLine1" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3"></td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtAlternateCmpLine2" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3"></td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtAlternateCmpLine3" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label40" runat="server" Style="text-align: left">City</asp:Label>
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtAlternateCmpCity" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label41" runat="server" Style="text-align: left">State</asp:Label>
                        </td>
                        <td class="RightColumn">
                            <asp:DropDownList ID="ddlAlternateCmpState" CssClass="cmbUserProfileState" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label42" runat="server" Style="text-align: left">County</asp:Label>
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtAlternateCmpCounty" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label43" runat="server" Style="text-align: left">Country</asp:Label>
                        </td>
                        <td class="RightColumn">
                            <asp:DropDownList ID="ddlAlternateCmpCountry" CssClass="cmbUserProfileCountry" runat="server"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label44" runat="server" Style="text-align: left">(Intl Code)(Area Code) Phone</asp:Label>
                        </td>
                        <td class="RightColumn">
                            <rad:RadMaskedTextBox ID="txtAlternateCmpIntlCode" CssClass="txtUserProfileAreaCodeSmall"
                                runat="server" Mask="(###)" Width="25%">
                            </rad:RadMaskedTextBox>
                            <rad:RadMaskedTextBox ID="txtAlternateCmpAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                runat="server" Mask="(####)" Width="25%">
                            </rad:RadMaskedTextBox>
                            <rad:RadMaskedTextBox ID="txtAlternateCmpPhone" CssClass="txtUserProfileAreaCode" runat="server"
                                Mask="###-####" Width="45%">
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
                <asp:AsyncPostBackTrigger ControlID="cmbCountryNew" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td style="width: 170px">&nbsp
                </td>
                <td class="RightColumn actions">
                    <asp:Button ID="btnAltCmpOK" runat="server" Text="OK" class="submitBtn" ValidationGroup="ok" />
                    <asp:Label ID="Label45" runat="server" Style="text-align: left" Visible="false"></asp:Label>
                    <asp:Button ID="btnAltCmpCancel" runat="server" Text="Cancel" class="submitBtn" ValidationGroup="ok" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</rad:RadWindow>


<rad:RadWindow ID="RadAddNew" runat="server" Width="350px" Height="160px" Modal="True"
    Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Add New Colleague" Behavior="None">
    <ContentTemplate>
        <div>
            <div>&nbsp; </div>
            <div>
                <div height="35">
                    Your Company :
                    <asp:TextBox ID="txtAddNew" onkeydown="javascript:return false" Style="background-color: #d8d8bc;" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="AddNew" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAddNew"
                        ErrorMessage="*" Display="Dynamic"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div>&nbsp; </div>
            <div>
                <div style="margin-left: 100px;">
                    <asp:Button ID="btnComAddNew" runat="server" Text="Add" CssClass="submitBtn"
                        CausesValidation="false" ValidationGroup="AddNew" Height="23px" Width="60px" />
                    <span width="5"></span>
                    <asp:Button ID="btnComCanlNew" runat="server" Text="Cancel" CssClass="submitBtn"
                        CausesValidation="false" Height="23px" Width="60px" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</rad:RadWindow>
<cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
<cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
