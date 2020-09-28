<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdminEditProfile__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.AdminEditProfile__c" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" TagName="Topiccode" Src="~/UserControls/Aptify_General/TopicCodeViewer.ascx" %>
<style type="text/css">
    .RadWindow.rwIcon
    {
        width: 25px !important;
        height: 20px;
    }
</style>
<script type="text/javascript" language="javascript">
    function ShowImage(ImageURL) {
        if (document.getElementById(ImageURL) != undefined) {
            document.getElementById(ImageURL).style.visibility = "visible";
        }
    }

    function UploadImage() {
        //Neha issue no 14430,03/11/13, disable Apply button 
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
    // Neha issue no 14430, 03/11/13, added Function(enable apply button and applied class)
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
            <%-- <tr>
                <td class="PageTitle">
                   
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="Label12" runat="server" ForeColor="Red" Font-Size="8pt">* designates required fields</asp:Label>
                </td>
            </tr>--%>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="leftDiv" class="ProfileLeftDiv">
                <div id="UpperDiv" class="ProfileUpperDiv">
                    <table id="tblProfileImg" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="tdImage AdminProfileImage">
                                <asp:Image ID="imgProfile" runat="server" ClientIDMode="AutoID" CssClass="AdminProfileImageBorder" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="tdImage" align="center">
                                <asp:Label ID="LableImageUploadText" runat="server" Font-Size="8pt" Font-Names="Segoe UI"></asp:Label>
                                <br />
                                <asp:Label ID="LableImageSaveIndicator" runat="server" Font-Size="8pt" Font-Names="Segoe UI"
                                    Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:Button ID="btnUpload" runat="server" CausesValidation="False" Width="60px" Text="Upload"
                                Style="display: none" />
                        </td>
                        </tr>--%>
                        <tr>
                            <td align="right">
                                <asp:Image ID="imgEditProfileImage" runat="server" ImageUrl="~/Images/Edit.png" Style="width: 12px" />
                                <asp:LinkButton ID="lbtnOpenProfileImage" ForeColor="#d07b0c" runat="server" Text="Edit"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnOpenProfileImage" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnRemovePhoto"  EventName="Click" />
            <asp:PostBackTrigger ControlID="btnUpload" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <div id="RightDiv" class="ProfileRightDiv">
        <table id="tblMain" runat="server" class="data-form" style="width: 100%">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <%--<td style="padding-right: 0.5em; padding-left: 1.5em; padding-bottom: 0.5em; padding-top: 0.5em"
                                valign="top">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Visible="False"></asp:ValidationSummary>
                                <asp:Label ID="Label1" runat="server" BackColor="Transparent" ForeColor="Red" Visible="False"></asp:Label>
                            </td>--%>
                            <td align="right">
                                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="#d07b0c"></asp:Label>
                                <br />
                                <asp:Label ID="lblemail" runat="server" Text="Click the Send Notification button to notify this person that you have updated his or her profile"></asp:Label>&nbsp;
                                <%--  <asp:CheckBox ID="chkemail" Visible="false" runat="server" ToolTip="If you want to notify this person that you have modified his or her profile, check this box and click Send." />--%>
                                <asp:Button ID="btnsend" runat="server" Text="Send Notification" Width="112px" class="submitBtn"
                                    Height="23px" />
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
                    <div>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="tdEditPersonalInfo" colspan="2">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td align="left" class="infoheadedit">
                                                Personal Information
                                            </td>
                                            <td align="right">
                                                <asp:Image ID="EditImage1" runat="server" ImageUrl="~/Images/Edit.png" Style="width: 12px" />
                                                <asp:LinkButton ID="btnOpenPopup" ForeColor="#d07b0c" runat="server" Text="Edit"
                                                    OnClick="btnOpenPopup_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--  end--%>
                            </tr>
                            <tr>
                                <td class="LeftAdminPersonInfo">
                                    <asp:Label ID="lblName" runat="server">First Name:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:Label ID="lblEditFirstName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftAdminPersonInfo">
                                    <asp:Label ID="Label4" runat="server">Last Name:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:Label ID="lblEditLastName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftAdminPersonInfo">
                                    <asp:Label ID="lblCompany" runat="server">Company:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <%-- <asp:TextBox ID="txtCompany" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>--%>
                                    <asp:Label ID="lblEditCompany" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trTitle" runat="server">
                                <td class="LeftAdminPersonInfo">
                                    <asp:Label ID="lblTitle" runat="server">Title:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <%--<asp:TextBox ID="txtTitle" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>--%>
                                    <asp:Label ID="lblJobFunction" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftAdminPersonInfo">
                                    <asp:Label ID="Label1" runat="server">Make Mentor:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <%-- <asp:TextBox ID="txtCompany" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>--%>
                                    <asp:Label ID="lblMakeMentor" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <%--            <tr>
                                <td class="LeftColumnEditProfile">
                                    <asp:Label ID="lblPhoto" runat="server">Photo</asp:Label>
                                </td>
                                <td>
                                   
                                </td>
                            </tr>--%>
                            <%-- Amruta IssueID 14307--%>
                            <tr id="trEmail" runat="server">
                                <td class="LeftAdminPersonInfo">
                                    <asp:Label ID="lblEmailID" runat="server">Email:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:Label ID="lblEmailAddress" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>--%>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="tdEditContactInfo" colspan="4">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td align="left" class="infoheadedit">
                                                Contact Information
                                            </td>
                                            <td align="right">
                                                <asp:Image ID="EditImage2" runat="server" ImageUrl="~/Images/Edit.png" Style="width: 12px" />
                                                <asp:LinkButton ID="contact" ForeColor="#d07b0c" runat="server" Text="Edit"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--  end--%>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td id="lblPerAddressTitle" runat="server" class="LeftColumnAdminContactinfo lblAdminEditProfile">
                                                Preferred Address:
                                            </td>
                                            <td class="RightColumnPerferAddAdmin" colspan="3">
                                                <asp:Label ID="lblPerferredAddress" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="RightColumn">
                                    &nbsp;
                                </td>
                                <td class="RightColumn">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td id="tdBusinessAdd" runat="server" class="LeftColumnAdminEditProfile" style="font-weight: bold;">
                                    Business Address:
                                </td>
                                <td id="tdHomeAdd" runat="server" class="LeftColumnAdminEditProfile">
                                    Home Address:
                                </td>
                                <td id="tdBillingAdd" runat="server" class="LeftColumnAdminEditProfile">
                                    Billing Address:
                                </td>
                                <td id="tdPoboxAdd" runat="server" class="LeftColumnAdminEditProfile">
                                    PO Box Address:
                                </td>
                            </tr>
                            <tr>
                                <td id="tdBusinessAddVal" runat="server">
                                    <asp:Label ID="BusinessAddressval" runat="server"></asp:Label>
                                    <asp:Label ID="BusinessAdd1" runat="server"></asp:Label>
                                    <asp:Label ID="BusinessAdd2" runat="server"></asp:Label>
                                    <asp:Label ID="BusinessAdd3" runat="server"></asp:Label>
                                    <asp:Label ID="BusinessCityState" runat="server"></asp:Label>
                                    <asp:Label ID="BusinessCountry" runat="server"></asp:Label>
                                </td>
                                <td class="RightColumn" id="tdHomeAddVal" runat="server">
                                    <asp:Label ID="HomeAddressval" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="HomeAdd1" runat="server"></asp:Label>
                                    <asp:Label ID="HomeAdd2" runat="server"></asp:Label>
                                    <asp:Label ID="HomeAdd3" runat="server"></asp:Label>
                                    <asp:Label ID="HomeCityState" runat="server"></asp:Label>
                                    <asp:Label ID="HomeCountry" runat="server"></asp:Label>
                                </td>
                                <td class="RightColumn" id="tdBillingAddVal" runat="server">
                                    <asp:Label ID="BillingAddressval" Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="BillingAdd1" runat="server"></asp:Label>
                                    <asp:Label ID="BillingAdd2" runat="server"></asp:Label>
                                    <asp:Label ID="BillingAdd3" runat="server"></asp:Label>
                                    <asp:Label ID="BillingCityState" runat="server"></asp:Label>
                                    <asp:Label ID="BillingAddCountry" runat="server"></asp:Label>
                                </td>
                                <td class="RightColumn" id="tdPoboxAddVal" runat="server">
                                    <asp:Label ID="PoBoxAddress" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="PoBoxAdd1" runat="server"></asp:Label>
                                    <asp:Label ID="PoBoxAdd2" runat="server"></asp:Label>
                                    <asp:Label ID="PoBoxAdd3" runat="server"></asp:Label>
                                    <asp:Label ID="PoBoxCityState" runat="server"></asp:Label>
                                    <asp:Label ID="PoBoxCountry" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-bottom: 0px; padding-top: 0px">
                                    <table>
                                        <tr>
                                            <td id="lblphonetitle" runat="server" class="LeftColumnAdminContactinfo lblAdminEditProfile"
                                                style="font-weight: bold;">
                                                (Area Code) Phone:
                                            </td>
                                            <td class="RightColumnContactAdmin">
                                                <asp:Label ID="lblphoneVal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="RightColumnContactAdmin">
                                    &nbsp;
                                </td>
                                <td class="RightColumnContactAdmin">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-bottom: 0px; padding-top: 0px">
                                    <table>
                                        <tr>
                                            <td id="lblFaxtitle" runat="server" class="LeftColumnAdminContactinfo lblAdminEditProfile"
                                                style="font-weight: bold;">
                                                (Area Code) Fax:
                                            </td>
                                            <td class="RightColumnContactAdmin">
                                                <asp:Label ID="lblFaxVal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="RightColumnContactAdmin">
                                    &nbsp;
                                </td>
                                <td class="RightColumnContactAdmin">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <%--     </ContentTemplate>--%>
                        <%--  <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="contact" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="trWebAccount">
                <td>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="tdMemberInfo" colspan="2">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="5%">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left" class="infoheadedit">
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
                                        <td class="LeftAdminMemberInfo">
                                            <asp:Label ID="lblmembershipType" runat="server">Membership Type:</asp:Label>
                                        </td>
                                        <td class="RightColumn">
                                            <asp:Label ID="lblMemberTypeVal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftAdminMemberInfo">
                                            <asp:Label ID="lblStartDate" runat="server">Start Date:</asp:Label>
                                        </td>
                                        <td class="RightColumn">
                                            <asp:Label ID="lblStartDateVal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftAdminMemberInfo">
                                            <asp:Label ID="lblEndDate" runat="server">End Date:</asp:Label>
                                        </td>
                                        <td class="RightColumn">
                                            <asp:Label ID="lblEndDateVal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftAdminMemberInfo">
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
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="txtUserID" EventName="TextChanged" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="tdEditTopicofInterestInfo">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td align="left" class="infoheadedit">
                                                Topics of Interest
                                            </td>
                                            <td align="right">
                                                <%--  <telerik:RadButton ID="btnTopicIntrest" runat="server" BorderStyle="None" ButtonType="LinkButton"  Text="Edit"  OnClick="btnTopicIntrest_Click"   >
                                                    <Icon PrimaryIconUrl="~/Images/Edit.png"   />
                                                </telerik:RadButton>--%>
                                                <asp:Image runat="server" ID="EditImage3" ImageUrl="~/Images/Edit.png" />
                                                <asp:LinkButton ID="btnTopicIntrest" runat="server" ForeColor="#d07b0c" Text="Edit"
                                                    Style="margin-left: 0px" OnClick="btnTopicIntrest_Click" />
                                                <%--  <asp:Button ID="contactinformation" OnClick="contactinformation_Click" runat="server"
                                                            Text="Edit" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
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
</div>
<div class="content-container clearfix">
</div>
<cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
<cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" TotalItemsRemovedByRemoveItem="0" />
<rad:RadWindow ID="radwindowPopup" runat="server" Width="350px" Height="180px" Modal="True"
    BackColor="#DADADA" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
    Title="Personal Information" Behavior="None" Skin="Default" IconUrl="~/Images/personal-icon.png">
    <ContentTemplate>
        <panel id="PersonalInfoPanel" runat="server">
       <table  style="padding-left:10px;padding-right:45px; padding-top:10px;background-color:#f4f3f1;height:100%">
        <tr >
        
        <td style="width:100px;padding-right:5px"  align="right" ><span class="RequiredField">  *</span> <asp:Label ID="lblFirstName" Font-Size="12px" 
                Font-Bold="True" Text="First Name:" runat="server" 
                ></asp:Label></td>
        <td >  <asp:TextBox ID="txtEditFirstName"  runat="server" 
                Width="180px" ></asp:TextBox>
                 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEditFirstName"
                                        ValidationGroup="EditProfileControl" ErrorMessage="First Name Required" Display="Dynamic"
                                        ForeColor="Red"></asp:RequiredFieldValidator></td>
        </tr>
        <tr >
  
              <td style="width:100px;padding-right:5px" align="right"><span class="RequiredField">  *</span><asp:Label ID="lblLastName" Font-Bold="True" Text="Last Name:" 
                      runat="server" ></asp:Label></td>
        <td><asp:TextBox ID="txtEditLastName"  runat="server" Width="180px" 
                ></asp:TextBox>
               
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEditLastName"
                                        ValidationGroup="EditProfileControl" ErrorMessage="Last Name Required" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
        </tr>
               <tr>
                <td style="padding-right:5px" align="right">&nbsp;<asp:Label ID="lblEditJobFunction" Text="Title:" Font-Bold="True" 
                      runat="server" ></asp:Label></td>
        <td>   <asp:TextBox ID="txtEditJobFunction"  runat="server" Width="180px" 
                ></asp:TextBox></td>
        </tr>
         <tr>
                <td style="padding-right:5px" align="right">&nbsp;<asp:Label ID="lblEditMakeMentor" Text="Make Mentor:" Font-Bold="True" 
                      runat="server" ></asp:Label></td>
        <td>   
        <asp:CheckBox ID="chkMakeMentor" runat="server"></asp:CheckBox>
        </td>
        </tr>
           <tr >
          <td></td>
        <td align="right" class="profileradbutton" ><asp:Button ID="btnOk" class="submitBtn"  
                runat="server" Width="70px" Text="Save" 
                OnClick="btnOk_Click" ValidationGroup="EditProfileControl" Height="23px" />&nbsp;&nbsp;
	        <asp:Button ID="Button1" Width="70px" runat="server" Text="Cancel" class="submitBtn"
                OnClick="btnCancel_Click" Height="23px" /></td>
            
        </tr></table>
	   </panel>
    </ContentTemplate>
</rad:RadWindow>
<rad:RadWindow ID="radNonMemberMsg" runat="server" Width="350px" Height="150px" Modal="True"
    BackColor="#DADADA" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
    Title="Personal Information" Behavior="None" Skin="Default" IconUrl="~/Images/personal-icon.png">
    <ContentTemplate>
    <table>
    <tr>
    <td>
      <asp:Label ID="lblNonMemberMsg" runat="server" Text=""></asp:Label>
    </td>
    </tr>
     <tr>
    <td align="center">
    <br /><br />
       <asp:Button ID="btnMakeMentorYes" Width="70px" runat="server" Text="Yes" class="submitBtn"
                Height="23px" />
                <asp:Button ID="btnMakeMentorNo" Width="70px" runat="server" Text="No" class="submitBtn"
                Height="23px" />
    </td>
    </tr>
    </table>
      
    </ContentTemplate>
</rad:RadWindow>
<rad:RadWindow ID="radwindowcontact" runat="server" Height="300px" Width="563px"
    Modal="True" BackColor="#f4f3f1" Skin="Default" VisibleStatusbar="False" Behaviors="None"
    ForeColor="#BDA797" Title="Contact Information" Behavior="None" IconUrl="~/Images/contact-icon.png">
    <ContentTemplate>
        <div style="background-color: #f4f3f1; height: 232px; width: 565px">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
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
                            <td class="LeftColumnEditProfile" style="font-weight: bold; padding-right: 5px" align="right">
                                Address Type:
                            </td>
                            <td class="RightColumn" align="left" style="padding-bottom: 5px">
                                <asp:DropDownList ID="ddlAddressType" CssClass="cmbUserProfileBussinessAdress" runat="server"
                                    AutoPostBack="True">
                                    <asp:ListItem>Business Address</asp:ListItem>
                                    <asp:ListItem>Home Address</asp:ListItem>
                                    <asp:ListItem>Billing Address</asp:ListItem>
                                    <asp:ListItem>PO Box Address</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;<asp:CheckBox ID="chkPrefAddress" CssClass="cb" runat="server" Text="Preferred Address"
                                    AutoPostBack="True" />
                            </td>
                            <td class="RightColumn">
                                &nbsp;
                            </td>
                        </tr>
                        <!-- Address Line  Rows -->
                        <tr id="trAddressLine1" runat="server">
                            <td id="Td1" class="LeftColumnEditProfile" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="lblAddress" runat="server" Font-Bold="true">Address:</asp:Label>
                            </td>
                            <td id="Td2" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trHomeAddressLine1" runat="server" visible="False">
                            <td id="Td3" class="LeftColumnEditProfile" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="Label2" runat="server" Font-Bold="true">Address:</asp:Label>
                            </td>
                            <td id="Td4" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtHomeAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trBillingAddressLine1" runat="server" visible="False">
                            <td id="Td5" class="LeftColumnEditProfile" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="Label3" runat="server" Font-Bold="true">Address:</asp:Label>
                            </td>
                            <td id="Td6" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtBillingAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trPOBoxAddressLine1" runat="server" visible="False">
                            <td id="Td7" class="LeftColumnEditProfile" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="Label8" runat="server" Font-Bold="true">Address:</asp:Label>
                            </td>
                            <td id="Td8" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtPOBoxAddressLine1" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <!-- Address Line 2 Rows -->
                        <tr id="trAddressLine2" runat="server">
                            <td id="Td9" class="LeftColumnEditProfile" runat="server">
                            </td>
                            <td id="Td10" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trHomeAddressLine2" runat="server" visible="False">
                            <td id="Td11" class="LeftColumnEditProfile" runat="server">
                            </td>
                            <td id="Td12" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtHomeAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trBillingAddressLine2" runat="server" visible="False">
                            <td id="Td13" class="LeftColumnEditProfile" runat="server">
                            </td>
                            <td id="Td14" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtBillingAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trPOBoxAddressLine2" runat="server" visible="False">
                            <td id="Td15" class="LeftColumnEditProfile" runat="server">
                            </td>
                            <td id="Td16" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtPOBoxAddressLine2" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <!-- Address Line 3 Rows -->
                        <tr id="trAddressLine3" runat="server">
                            <td id="Td17" class="LeftColumnEditProfile" runat="server">
                            </td>
                            <td id="Td18" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trHomeAddressLine3" runat="server" visible="False">
                            <td id="Td19" class="LeftColumnEditProfile" runat="server">
                            </td>
                            <td id="Td20" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtHomeAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trBillingAddressLine3" runat="server" visible="False">
                            <td id="Td21" class="LeftColumnEditProfile" runat="server">
                            </td>
                            <td id="Td22" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtBillingAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trPOBoxAddressLine3" runat="server" visible="False">
                            <td id="Td23" class="LeftColumnEditProfile" runat="server">
                            </td>
                            <td id="Td24" class="RightColumn" colspan="2" runat="server" style="padding-bottom: 5px">
                                <asp:TextBox ID="txtPOBoxAddressLine3" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trCity" runat="server">
                            <td id="Td25" class="LeftColumnEditProfile" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="lblCityStateZip" runat="server" Font-Bold="true">City:</asp:Label>
                            </td>
                            <td id="Td26" class="RigthColumnContactBold EditContactPaddingLeft" runat="server"
                                style="padding-bottom: 5px;">
                                <asp:TextBox ID="txtCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                <span class="SpanState">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="true">State:</asp:Label>
                                </span>
                                <asp:DropDownList ID="cmbState" CssClass="cmbUserProfileState Editcontactstate" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trHomeCity" runat="server" visible="False">
                            <td id="Td27" class="LeftColumnEditProfile" colspan="1" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="Label9" runat="server" Font-Bold="true">City:</asp:Label>
                            </td>
                            <td id="Td28" class="RigthColumnContactBold EditContactPaddingLeft" runat="server"
                                style="padding-bottom: 5px;">
                                <asp:TextBox ID="txtHomeCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                <span class="SpanState">
                                    <asp:Label ID="Label111" runat="server" Font-Bold="true">State:</asp:Label>
                                </span>
                                <asp:DropDownList ID="cmbHomeState" CssClass="cmbUserProfileState Editcontactstate"
                                    runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trBillingCity" runat="server" visible="False">
                            <td id="Td29" class="LeftColumnEditProfile" colspan="1" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="Label10" runat="server" Font-Bold="true">City:</asp:Label>
                            </td>
                            <td id="Td30" class="RigthColumnContactBold EditContactPaddingLeft" runat="server"
                                style="padding-bottom: 5px;">
                                <asp:TextBox ID="txtBillingCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                <span class="SpanState">
                                    <asp:Label ID="Label22" runat="server" Font-Bold="true">State:</asp:Label>
                                </span>
                                <asp:DropDownList ID="cmbBillingState" CssClass="cmbUserProfileState Editcontactstate"
                                    runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trPOBoxCity" runat="server" visible="False">
                            <td id="Td31" class="LeftColumnEditProfile" colspan="1" runat="server" style="padding-right: 5px;
                                padding-left: 4px;" align="right">
                                <asp:Label ID="Label11" runat="server" Font-Bold="true">City:</asp:Label>
                            </td>
                            <td id="Td32" class="RigthColumnContactBold EditContactPaddingLeft" runat="server"
                                style="padding-bottom: 5px;">
                                <asp:TextBox ID="txtPOBoxCity" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>
                                <span class="SpanState">
                                    <asp:Label ID="Label14" runat="server" Font-Bold="true">State:</asp:Label>
                                </span>
                                <asp:DropDownList ID="cmbPOBoxState" CssClass="cmbUserProfileState Editcontactstate"
                                    runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trCountry" runat="server">
                            <td id="Td33" class="LeftColumnEditProfile" colspan="1" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="lblCountry" runat="server" Font-Bold="true">Country:</asp:Label>
                            </td>
                            <td id="Td34" align="left" class="RigthColumnContactBold EditContactPaddingLeft"
                                runat="server" style="padding-bottom: 5px;">
                                <asp:DropDownList ID="cmbCountry" Width="163px" CssClass="cmbUserProfileCountry"
                                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbCountry_SelectedIndexChanged">
                                </asp:DropDownList>
                                <span class="SpanZipCode">
                                    <asp:Label ID="Label23" runat="server" Font-Bold="true">ZIP Code:</asp:Label>
                                </span>
                                <asp:TextBox ID="txtZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trHomeCountry" runat="server" visible="False">
                            <td id="Td35" class="LeftColumnEditProfile" colspan="1" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="Label13" runat="server" Font-Bold="true">Country:</asp:Label>
                            </td>
                            <td id="Td36" class="RigthColumnContactBold EditContactPaddingLeft" runat="server"
                                style="padding-bottom: 5px;">
                                <asp:DropDownList ID="cmbHomeCountry" Width="163px" CssClass="cmbUserProfileCountry"
                                    runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <span class="SpanZipCode">
                                    <asp:Label ID="Label24" runat="server" Font-Bold="true">ZIP Code:</asp:Label>
                                </span>
                                <asp:TextBox ID="txtHomeZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trBillingCountry" runat="server" visible="False">
                            <td id="Td37" class="LeftColumnEditProfile" colspan="1" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="Label16" runat="server" Font-Bold="true">Country:</asp:Label>
                            </td>
                            <td id="Td38" class="RigthColumnContactBold EditContactPaddingLeft" runat="server"
                                style="padding-bottom: 5px;">
                                <asp:DropDownList ID="cmbBillingCountry" Width="163px" CssClass="cmbUserProfileCountry"
                                    runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <span class="SpanZipCode">
                                    <asp:Label ID="Label19" runat="server" Font-Bold="true">ZIP Code:</asp:Label>
                                </span>
                                <asp:TextBox ID="txtBillingZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trPOBoxCountry" runat="server" visible="False">
                            <td id="Td39" class="LeftColumnEditProfile" colspan="1" runat="server" style="padding-right: 5px"
                                align="right">
                                <asp:Label ID="Label17" runat="server" Font-Bold="true">Country:</asp:Label>
                            </td>
                            <td id="Td40" class="RigthColumnContactBold EditContactPaddingLeft" style="padding-bottom: 5px;"
                                runat="server">
                                <asp:DropDownList ID="cmbPOBoxCountry" Width="163px" CssClass="cmbUserProfileCountry"
                                    runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <span class="SpanZipCode">
                                    <asp:Label ID="Label20" runat="server" Font-Bold="true">ZIP Code:</asp:Label>
                                </span>
                                <asp:TextBox ID="txtPOBoxZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftColumnEditProfile" style="padding-right: 5px" align="right">
                                <asp:Label ID="lblPhone" runat="server" Font-Bold="true">(Area Code) Phone:</asp:Label>
                            </td>
                            <%--Neha Issue 14750 ,02/26/13, added css for phoneareacode--%>
                            <td class="RightColumn tdRightColumnAreacodephone">
                                <rad:RadMaskedTextBox ID="txtPhoneAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                    runat="server" Mask="(###)" Width="48px">
                                </rad:RadMaskedTextBox>
                                <rad:RadMaskedTextBox ID="txtPhone" CssClass="txtUserProfileAreaCode" runat="server"
                                    Mask="###-####" Width="65px">
                                </rad:RadMaskedTextBox>
                                <asp:Label ID="lblphonemsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftColumnEditProfile" style="padding-right: 5px" align="right">
                                <asp:Label ID="lblFax" runat="server" Font-Bold="true">(Area Code) Fax:</asp:Label>
                            </td>
                            <td class="RightColumn">
                                <rad:RadMaskedTextBox ID="txtFaxAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                    runat="server" Mask="(###)" Width="48px">
                                </rad:RadMaskedTextBox>
                                <rad:RadMaskedTextBox ID="txtFaxPhone" runat="server" CssClass="txtUserProfileAreaCode"
                                    Mask="###-####" Width="65px">
                                </rad:RadMaskedTextBox>
                                <asp:Label ID="lblFaxMsg" runat="server" Font-Bold="true" Text="Please Enter Fax Phone"
                                    Visible="false" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px">
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
        </div>
        <div style="background-color: #f4f3f1;" class="PaddingrightContact" align="right">
            <asp:Button ID="btncontactsave" class="submitBtn" OnClientClick="return validatephone();"
                Width="70px" runat="server" Text="Save" OnClick="btncontactsave_Click" Height="23px" />&nbsp;&nbsp;
            <asp:Button ID="btncontactcancel" runat="server" Text="Cancel" Width="70px" class="submitBtn"
                OnClick="btncontactcancel_Click" Height="23px" />&nbsp;
        </div>
    </ContentTemplate>
</rad:RadWindow>
<%--<rad:RadWindow ID="radtopicintrest" runat="server" Width="400px" Height="150px" Modal="True"
    BackColor="#f4f3f1" VisibleStatusbar="False" Skin="Default" Behaviors="None" ForeColor="#BDA797"
    Title="Topics of Interest" Behavior="None" IconUrl="~/Images/topic-of-int.png">
    <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
            height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
            <tr>
                <td>
                    <asp:CheckBoxList ID="listTopicofInterest" CssClass="test" runat="server" RepeatColumns="3"
                        RepeatDirection="Horizontal" Width="100%">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 16px;">
                    <asp:Button ID="btnSaveIntrest" runat="server" Text="Save" Width="70px" class="submitBtn"
                        OnClick="btnSaveIntrest_Click" Height="23px" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancelIntrest" runat="server" Width="70px" Text="Cancel" class="submitBtn"
                        OnClick="btnCancelIntrest_Click" Height="23px" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</rad:RadWindow>--%>
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
                        OnClick="btnSaveIntrest_Click" Height="23px" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancelIntrest" runat="server" Width="70px" Text="Cancel" class="submitBtn"
                        OnClick="btnCancelIntrest_Click" Height="23px" />
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
                                    <%--<asp:Label ID="LableImageSaveIndicator" runat="server" Font-Size="8pt" Font-Names="Segoe UI"
                                        Visible="false"></asp:Label>--%>
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
                                               <%-- <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="fleProfilePhoto"
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

                        <rad:RadImageEditor ID="radImageEditor" runat="server" Width="450px" Height="400px" AllowedSavingLocation="Server" 
                            OnClientImageLoad="ZoomBestFit" OnClientToolsDialogClosed="OnClientToolsDialogClosed" OnClientImageChanging="OnClientImageChanging" OnClientCommandExecuted="OnClientCommandExecuted" OnClientImageChanged="EnabledImageSaveButton" CanvasMode="No">    
                            <Tools>
                                <rad:ImageEditorToolGroup>
                                    <rad:ImageEditorToolStrip CommandName="Undo"></rad:ImageEditorToolStrip>
                                    <rad:ImageEditorToolStrip CommandName="Redo"></rad:ImageEditorToolStrip>
                                    <telerik:ImageEditorTool Text="Reset" CommandName="Reset" />
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
                        <asp:Label ID="lblCropTip" runat="server" Font-Size="8pt" Font-Names="Segoe UI" Text="After cropping a photo, click Crop and then Save."></asp:Label>
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
                                    <asp:Button ID="btnSaveProfileImage" class="submitBtn" runat="server" Width="70px" Text="Save" Height="23px" />&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelProfileImage" Width="70px" runat="server" Text="Cancel" class="submitBtn" OnClick="btnCancelProfileImage_Click" Height="23px"  />
                                </td>
                            </tr>
                        </table>                            
	                    
                    </td>            
                </tr>
            </table>
	   </panel>
    </ContentTemplate>
</rad:RadWindow>
