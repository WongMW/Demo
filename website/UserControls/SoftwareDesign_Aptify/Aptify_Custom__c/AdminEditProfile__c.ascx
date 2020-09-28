<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AdminEditProfile__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.AdminEditProfile__c" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" TagName="Topiccode" Src="~/UserControls/Aptify_General/TopicCodeViewer.ascx" %>

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
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="leftDiv" class="ProfileLeftDiv">
                <div id="UpperDiv" class="ProfileUpperDiv">
                    <table id="tblProfileImg">
                        <tr>
                            <td class="tdImage AdminProfileImage">
                                <asp:Image ID="imgProfile" runat="server" ClientIDMode="AutoID" CssClass="AdminProfileImageBorder" />
                            </td>
                        </tr>

                        <tr>
                            <td>
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
        </Triggers>
    </asp:UpdatePanel>
    <div id="RightDiv" class="ProfileRightDiv">
        <div id="tblMain" runat="server" class="data-form">
            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="#d07b0c"></asp:Label><br/>
            <asp:Label ID="lblemail" runat="server" Text="Click the Send Notification button to notify this person that you have updated his or her profile"></asp:Label>

            <div class="actions">
                <asp:Button ID="btnsend" runat="server" Text="Send Notification" class="submitBtn" />
            </div>

            <div class="cai-form">
                <span class="form-title">Personal Information</span>

                <div class="cai-form-content">
                    <div class="edit-content-button">
                        <asp:Image ID="EditImage1" runat="server" ImageUrl="~/Images/Edit.png" />
                        <asp:LinkButton ID="btnOpenPopup" ForeColor="#d07b0c" runat="server" Text="Edit"
                            OnClick="btnOpenPopup_Click" />
                    </div>
                    <div class="">
                        <asp:Label ID="lblName" runat="server" CssClass="label-title">First Name:</asp:Label>
                        <asp:Label ID="lblEditFirstName" runat="server"></asp:Label>
                    </div>
                    <div class="">
                        <asp:Label ID="Label4" runat="server" CssClass="label-title">Last Name:</asp:Label>
                        <asp:Label ID="lblEditLastName" runat="server"></asp:Label>
                    </div>
                    <div class="">
                        <asp:Label ID="lblCompany" runat="server" CssClass="label-title">Company:</asp:Label>
                        <asp:Label ID="lblEditCompany" runat="server"></asp:Label>
                    </div>
                    <div class="">
                        <div id="trTitle" runat="server">
                            <asp:Label ID="lblTitle" runat="server" CssClass="label-title">Title:</asp:Label>
                            <asp:Label ID="lblJobFunction" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="">
                        <asp:Label ID="Label1" runat="server" CssClass="label-title">Make Mentor:</asp:Label>
                        <asp:Label ID="lblMakeMentor" runat="server"></asp:Label>
                    </div>
                    <div class="">
                        <div id="trEmail" runat="server">
                            <asp:Label ID="lblEmailID" runat="server" CssClass="label-title">Email:</asp:Label>
                            <asp:Label ID="lblEmailAddress" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="cai-form">
                <span class="form-title">Contact Information</span>
                <div class="cai-form-content">
                    <div class="edit-content-button">
                        <asp:Image ID="EditImage2" runat="server" ImageUrl="~/Images/Edit.png" Style="width: 12px" />
                        <asp:LinkButton ID="contact" ForeColor="#d07b0c" runat="server" Text="Edit"></asp:LinkButton>
                    </div>
                    <div class=""
                        <span id="lblPerAddressTitle" runat="server" class="label-title">Preferred Address:</span>
                        <asp:Label ID="lblPerferredAddress" runat="server"></asp:Label>
                    </div>
                    <div class="">
                        <span id="tdBusinessAdd" runat="server" class="label-title">Business Address:</span>
                        <div id="tdBusinessAddVal" runat="server">
                            <asp:Label ID="BusinessAddressval" runat="server"></asp:Label>
                            <asp:Label ID="BusinessAdd1" runat="server"></asp:Label>
                            <asp:Label ID="BusinessAdd2" runat="server"></asp:Label>
                            <asp:Label ID="BusinessAdd3" runat="server"></asp:Label>
                            <asp:Label ID="BusinessCityState" runat="server"></asp:Label>
                            <asp:Label ID="BusinessCountry" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="">
                        <span id="tdHomeAdd" runat="server" class="label-title">Home Address:</span>
                        <div id="tdHomeAddVal" runat="server">
                            <asp:Label ID="HomeAddressval" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="HomeAdd1" runat="server"></asp:Label>
                            <asp:Label ID="HomeAdd2" runat="server"></asp:Label>
                            <asp:Label ID="HomeAdd3" runat="server"></asp:Label>
                            <asp:Label ID="HomeCityState" runat="server"></asp:Label>
                            <asp:Label ID="HomeCountry" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="">
                        <span id="tdBillingAdd" runat="server" class="label-title">Billing Address:</span>
                        <div id="tdBillingAddVal" runat="server">
                            <asp:Label ID="BillingAddressval" Visible="false" runat="server"></asp:Label>
                            <asp:Label ID="BillingAdd1" runat="server"></asp:Label>
                            <asp:Label ID="BillingAdd2" runat="server"></asp:Label>
                            <asp:Label ID="BillingAdd3" runat="server"></asp:Label>
                            <asp:Label ID="BillingCityState" runat="server"></asp:Label>
                            <asp:Label ID="BillingAddCountry" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="">
                        <span id="tdPoboxAdd" runat="server" class="label-title">PO Box Address: </span>
                        <div id="tdPoboxAddVal" runat="server">
                            <asp:Label ID="PoBoxAddress" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="PoBoxAdd1" runat="server"></asp:Label>
                            <asp:Label ID="PoBoxAdd2" runat="server"></asp:Label>
                            <asp:Label ID="PoBoxAdd3" runat="server"></asp:Label>
                            <asp:Label ID="PoBoxCityState" runat="server"></asp:Label>
                            <asp:Label ID="PoBoxCountry" runat="server"></asp:Label>
                        </div>
                    </div>
                             

                  
                    
                    
                    

                    <span id="lblphonetitle" runat="server" class="label-title">(Area Code) Phone:</span>
                    <asp:Label ID="lblphoneVal" runat="server"></asp:Label>

                    <span id="lblFaxtitle" runat="server" class="label-title">(Area Code) Fax:</span>
                    <asp:Label ID="lblFaxVal" runat="server"></asp:Label>
                </div>
            </div>

            <div class="cai-form" runat="server" id="trWebAccount">
                <span class="form-title">Membership Information</span>
                <div class="cai-form-content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblmembershipType" CssClass="label-title" runat="server">Membership Type:</asp:Label>

                            <asp:Label ID="lblMemberTypeVal" runat="server"></asp:Label>

                            <asp:Label ID="lblStartDate" CssClass="label-title" runat="server">Start Date:</asp:Label>

                            <asp:Label ID="lblStartDateVal" runat="server"></asp:Label>

                            <asp:Label ID="lblEndDate" CssClass="label-title" runat="server">End Date:</asp:Label>

                            <asp:Label ID="lblEndDateVal" runat="server"></asp:Label>

                            <asp:Label ID="lblStatus" CssClass="label-title" runat="server">Status:</asp:Label>

                            <asp:Label ID="lblStatusVal" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="cai-form">
                <span class="form-title">Topics of Interest</span>

                <div class="cai-form-content">
                    <div class="edit-content-button">
                        <asp:Image runat="server" ID="EditImage3" ImageUrl="~/Images/Edit.png" />
                        <asp:LinkButton ID="btnTopicIntrest" runat="server" ForeColor="#d07b0c" Text="Edit" OnClick="btnTopicIntrest_Click" />
                    </div>

                    <asp:Label ID="lblTopicIntrest" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</div>

<cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
<cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" TotalItemsRemovedByRemoveItem="0" />

<rad:RadWindow ID="radwindowPopup" runat="server" Width="400px" Height="450px" Modal="True"
    BackColor="#DADADA" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
    Title="Personal Information" Behavior="None" Skin="Default" IconUrl="~/Images/personal-icon.png">
    <ContentTemplate>
        <panel id="PersonalInfoPanel" runat="server">
            <asp:Label ID="lblFirstName" CssClass="label-title" runat="server"><span class="RequiredField">*</span>First Name:</asp:Label>
       
            <asp:TextBox ID="txtEditFirstName" runat="server" ></asp:TextBox>
                 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEditFirstName" ValidationGroup="EditProfileControl" ErrorMessage="First Name Required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            <asp:Label ID="lblLastName" runat="server" CssClass="label-title"><span class="RequiredField">*</span>Last Name:</asp:Label>
      
            <asp:TextBox ID="txtEditLastName"  runat="server" ></asp:TextBox>
               
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEditLastName" ValidationGroup="EditProfileControl" ErrorMessage="Last Name Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
               
            <asp:Label ID="lblEditJobFunction" Text="Title:"  runat="server" CssClass="label-title"></asp:Label>
            
            <asp:TextBox ID="txtEditJobFunction"  runat="server"></asp:TextBox>
            <br />
            
            <asp:Label ID="lblEditMakeMentor" runat="server" >
                <asp:CheckBox ID="chkMakeMentor" runat="server" Text="Make Mentor"></asp:CheckBox>
            </asp:Label>            
            
            <div class="actions">
                <asp:Button ID="btnOk" class="submitBtn"   runat="server"  Text="Save"  OnClick="btnOk_Click" ValidationGroup="EditProfileControl"  />
	            <asp:Button ID="Button1" runat="server" Text="Cancel" class="submitBtn" OnClick="btnCancel_Click"  />
            </div>
	   </panel>
    </ContentTemplate>
</rad:RadWindow>


<rad:RadWindow ID="radNonMemberMsg" runat="server" Width="350px" Height="150px" Modal="True"
    BackColor="#DADADA" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
    Title="Personal Information" Behavior="None" Skin="Default" IconUrl="~/Images/personal-icon.png">
    <ContentTemplate>
        <asp:Label ID="lblNonMemberMsg" runat="server" Text=""></asp:Label>
        <asp:Button ID="btnMakeMentorYes" runat="server" Text="Yes" class="submitBtn" />
        <asp:Button ID="btnMakeMentorNo" runat="server" Text="No" class="submitBtn" />
    </ContentTemplate>
</rad:RadWindow>

<rad:RadWindow ID="radwindowcontact" runat="server" Height="500px" Width="563px"
    Modal="True" Skin="Default" VisibleStatusbar="False" Behaviors="None"
    ForeColor="#BDA797" Title="Contact Information" Behavior="None" IconUrl="~/Images/contact-icon.png">
    <ContentTemplate>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <span class="label-title">Address Type: </span>
                    <div>
                        <asp:DropDownList ID="ddlAddressType" CssClass="cmbUserProfileBussinessAdress" runat="server"
                            AutoPostBack="True">
                            <asp:ListItem>Business Address</asp:ListItem>
                            <asp:ListItem>Home Address</asp:ListItem>
                            <asp:ListItem>Billing Address</asp:ListItem>
                            <asp:ListItem>PO Box Address</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkPrefAddress" CssClass="cb" runat="server" Text="Preferred Address" AutoPostBack="True" />
                    </div>
                    <div id="trAddressLine1" runat="server">
                        <div id="Td1" runat="server">
                            <asp:Label ID="lblAddress" runat="server" class="label-title">Address:</asp:Label>
                        </div>
                        <div id="Td2" runat="server">
                            <asp:TextBox ID="txtAddressLine1" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trHomeAddressLine1" runat="server" visible="False">
                        <div id="Td3" runat="server">
                            <asp:Label ID="Label2" runat="server" class="label-title">Address:</asp:Label>
                        </div>
                        <div id="Td4" runat="server">
                            <asp:TextBox ID="txtHomeAddressLine1" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trBillingAddressLine1" runat="server" visible="False">
                        <div id="Td5" runat="server">
                            <asp:Label ID="Label3" runat="server" class="label-title">Address:</asp:Label>
                        </div>
                        <div id="Td6" runat="server">
                            <asp:TextBox ID="txtBillingAddressLine1" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trPOBoxAddressLine1" runat="server" visible="False">
                        <div id="Td7" runat="server">
                            <asp:Label ID="Label8" runat="server" class="label-title">Address:</asp:Label>
                        </div>
                        <div id="Td8" runat="server">
                            <asp:TextBox ID="txtPOBoxAddressLine1" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trAddressLine2" runat="server">
                        <div id="Td9" runat="server"></div>
                        <div id="Td10" runat="server">
                            <asp:TextBox ID="txtAddressLine2" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trHomeAddressLine2" runat="server" visible="False">
                        <div id="Td11" runat="server"></div>
                        <div id="Td12" runat="server">
                            <asp:TextBox ID="txtHomeAddressLine2" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trBillingAddressLine2" runat="server" visible="False">
                        <div id="Td13" runat="server"></div>
                        <div id="Td14" runat="server">
                            <asp:TextBox ID="txtBillingAddressLine2" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trPOBoxAddressLine2" runat="server" visible="False">
                        <div id="Td15" runat="server"></div>
                        <div id="Td16" runat="server">
                            <asp:TextBox ID="txtPOBoxAddressLine2" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trAddressLine3" runat="server">
                        <div id="Td17" runat="server"></div>
                        <div id="Td18" runat="server">
                            <asp:TextBox ID="txtAddressLine3" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trHomeAddressLine3" runat="server" visible="False">
                        <div id="Td19" runat="server"></div>
                        <div id="Td20" runat="server">
                            <asp:TextBox ID="txtHomeAddressLine3" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trBillingAddressLine3" runat="server" visible="False">
                        <div id="Td21" runat="server"></div>
                        <div id="Td22" runat="server">
                            <asp:TextBox ID="txtBillingAddressLine3" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trPOBoxAddressLine3" runat="server" visible="False">
                        <div id="Td23" runat="server"></div>
                        <div id="Td24" runat="server">
                            <asp:TextBox ID="txtPOBoxAddressLine3" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trCity" runat="server">
                        <td id="Td25" runat="server">
                            <asp:Label ID="lblCityStateZip" runat="server" class="label-title">City:</asp:Label>
                        </td>
                        <td id="Td26" runat="server">
                            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                            <span class="SpanState">
                                <asp:Label ID="Label5" runat="server" class="label-title">State:</asp:Label>
                            </span>
                            <asp:DropDownList ID="cmbState" runat="server">
                            </asp:DropDownList>
                        </td>
                    </div>
                    <div id="trHomeCity" runat="server" visible="False">
                        <div id="Td27" runat="server">
                            <asp:Label ID="Label9" runat="server" class="label-title">City:</asp:Label>
                        </div>
                        <div id="Td28" runat="server">
                            <asp:TextBox ID="txtHomeCity" runat="server"></asp:TextBox>
                            <span class="SpanState">
                                <asp:Label ID="Label111" runat="server" class="label-title">State:</asp:Label>
                            </span>
                            <asp:DropDownList ID="cmbHomeState"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="trBillingCity" runat="server" visible="False">
                        <div id="Td29" runat="server">
                            <asp:Label ID="Label10" runat="server" class="label-title">City:</asp:Label>
                        </div>
                        <div id="Td30" runat="server">
                            <asp:TextBox ID="txtBillingCity" runat="server"></asp:TextBox>
                            <span class="SpanState">
                                <asp:Label ID="Label22" runat="server" class="label-title">State:</asp:Label>
                            </span>
                            <asp:DropDownList ID="cmbBillingState"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="trPOBoxCity" runat="server" visible="False">
                        <div id="Td31" runat="server">
                            <asp:Label ID="Label11" runat="server" class="label-title">City:</asp:Label>
                        </div>
                        <div id="Td32" runat="server">
                            <asp:TextBox ID="txtPOBoxCity" runat="server"></asp:TextBox>
                            <span class="SpanState">
                                <asp:Label ID="Label14" runat="server" class="label-title">State:</asp:Label>
                            </span>
                            <asp:DropDownList ID="cmbPOBoxState" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="trCountry" runat="server">
                        <div id="Td33" runat="server">
                            <asp:Label ID="lblCountry" runat="server" class="label-title">Country:</asp:Label>
                        </div>
                        <div id="Td34" runat="server">
                            <asp:DropDownList ID="cmbCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                              <asp:RequiredFieldValidator ControlToValidate="cmbCountry" ID="RequiredFieldValidator1"
                              ErrorMessage="Please select a country"
                              InitialValue="0" runat="server" ForeColor="Red" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <span class="SpanZipCode">
                                <asp:Label ID="Label23" runat="server" class="label-title">ZIP Code:</asp:Label>
                            </span>
                            <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trHomeCountry" runat="server" visible="False">
                        <div id="Td35" runat="server">
                            <asp:Label ID="Label13" runat="server" class="label-title">Country:</asp:Label>
                        </div>
                        <div id="Td36" runat="server">
                            <asp:DropDownList ID="cmbHomeCountry"
                                runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ControlToValidate="cmbHomeCountry" ID="RequiredFieldValidator4"
                               ErrorMessage="Please select a country"
                               InitialValue="0" runat="server" ForeColor="Red" Display="Dynamic">
                             </asp:RequiredFieldValidator>
                            <span class="SpanZipCode">
                                <asp:Label ID="Label24" runat="server" class="label-title">ZIP Code:</asp:Label>
                            </span>
                            <asp:TextBox ID="txtHomeZipCode" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trBillingCountry" runat="server" visible="False">
                        <div id="Td37" runat="server">
                            <asp:Label ID="Label16" runat="server" class="label-title">Country:</asp:Label>
                        </div>
                        <div id="Td38" runat="server">
                            <asp:DropDownList ID="cmbBillingCountry"
                                runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                              <asp:RequiredFieldValidator ControlToValidate="cmbBillingCountry" ID="RequiredFieldValidator6"
                               ErrorMessage="Please select a country"
                               InitialValue="0" runat="server" ForeColor="Red" Display="Dynamic">
                             </asp:RequiredFieldValidator>
                            <span class="SpanZipCode">
                                <asp:Label ID="Label19" runat="server" class="label-title">ZIP Code:</asp:Label>
                            </span>
                            <asp:TextBox ID="txtBillingZipCode" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="trPOBoxCountry" runat="server" visible="False">
                        <div id="Td39" runat="server">
                            <asp:Label ID="Label17" runat="server" class="label-title">Country:</asp:Label>
                        </div>
                        <div id="Td40" runat="server">
                            <asp:DropDownList ID="cmbPOBoxCountry"
                                runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                              <asp:RequiredFieldValidator ControlToValidate="cmbPOBoxCountry" ID="RequiredFieldValidator5"
                               ErrorMessage="Please select a country"
                               InitialValue="0" runat="server" ForeColor="Red" Display="Dynamic">
                             </asp:RequiredFieldValidator>
                            <span class="SpanZipCode">
                                <asp:Label ID="Label20" runat="server" class="label-title">ZIP Code:</asp:Label>
                            </span>
                            <asp:TextBox ID="txtPOBoxZipCode" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div>
                        <div>
                            <asp:Label ID="lblPhone" runat="server" class="label-title">(Area Code) Phone:</asp:Label>
                        </div>
                        <%--Neha Issue 14750 ,02/26/13, added css for phoneareacode--%>
                        <div class="RightColumn tdRightColumnAreacodephone">
                            <rad:RadMaskedTextBox ID="txtPhoneAreaCode"
                                runat="server" Mask="(####)" Width="70px">
                            </rad:RadMaskedTextBox>
                            <rad:RadMaskedTextBox ID="txtPhone" runat="server"
                                Mask="###-####" Width="230px">
                            </rad:RadMaskedTextBox>
                            <asp:Label ID="lblphonemsg" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblFax" runat="server" class="label-title">(Area Code) Fax:</asp:Label>

                        <rad:RadMaskedTextBox ID="txtFaxAreaCode"
                            runat="server" Mask="(####)" Width="70px">
                        </rad:RadMaskedTextBox>
                        <rad:RadMaskedTextBox ID="txtFaxPhone" runat="server"
                            Mask="###-####" Width="230px">
                        </rad:RadMaskedTextBox>
                        <asp:Label ID="lblFaxMsg" runat="server" Text="Please Enter Fax Phone"
                            Visible="false" ForeColor="Red"></asp:Label>

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
        <div class="actions">
            <asp:Button ID="btncontactsave" class="submitBtn" OnClientClick="return validatephone();"
                runat="server" Text="Save" OnClick="btncontactsave_Click" />
            <asp:Button ID="btncontactcancel" runat="server" Text="Cancel" class="submitBtn"
                OnClick="btncontactcancel_Click" CausesValidation="false" />
        </div>
    </ContentTemplate>
</rad:RadWindow>

<rad:RadWindow ID="radtopicintrest" runat="server" Width="500px" Height="350px" Modal="True"
    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
    Title="Topics of Interest" Behavior="None" IconUrl="~/Images/topic-of-int.png"
    Skin="Default">
    <ContentTemplate>
        <cc2:Topiccode ID="TopiccodeViewer" runat="server" />
        <asp:Button ID="btnSaveIntrest" runat="server" Text="Save" class="submitBtn"
            OnClick="btnSaveIntrest_Click" CausesValidation="false"  />
        <asp:Button ID="btnCancelIntrest" runat="server" Text="Cancel" class="submitBtn"
            OnClick="btnCancelIntrest_Click" CausesValidation="false"  />
    </ContentTemplate>
</rad:RadWindow>

<rad:RadWindow ID="radwindowProfileImage" runat="server" Width="500px" Height="600px"
    Modal="True" BackColor="#DADADA" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
    Title="Profile Image" Behavior="None" Skin="Default" IconUrl="~/Images/personal-icon.png"
    MaxWidth="500px" CssClass="ProfileWindow">
    <ContentTemplate>
        <panel id="ProfileImagePanel" runat="server">
            <asp:Label ID="LableImageUploadText" runat="server" > </asp:Label>
            
            <rad:RadUpload runat="server" id="radUploadProfilePhoto" ControlObjectsVisibility="None" OnClientFileSelected="UploadImage" Localization-Select="Browse..." AllowedFileExtensions=".gif, .jpg, .bmp, png"  />
            <asp:Button ID="btnRemovePhoto" runat="server" CausesValidation="False" Text="Remove" CssClass="submitBtn" />
            <asp:Button ID="btnUpload" runat="server" CssClass="submitBtn" CausesValidation="False" Text="Upload" Style="display: none" />
            
            <rad:RadImageEditor ID="radImageEditor" runat="server" Width="450px" Height="400px" AllowedSavingLocation="Server" OnClientImageLoad="ZoomBestFit" OnClientToolsDialogClosed="OnClientToolsDialogClosed" OnClientImageChanging="OnClientImageChanging" OnClientCommandExecuted="OnClientCommandExecuted" OnClientImageChanged="EnabledImageSaveButton" CanvasMode="No">    
                <Tools>
                    <rad:ImageEditorToolGroup>
                         <rad:ImageEditorToolStrip CommandName="Undo"></rad:ImageEditorToolStrip>
                        <rad:ImageEditorToolStrip CommandName="Redo"></rad:ImageEditorToolStrip>
                        <telerik:ImageEditorTool Text="Reset" CommandName="Reset" />
                        <rad:ImageEditorToolSeparator></rad:ImageEditorToolSeparator>
                        <rad:ImageEditorTool Text="ZoomIn" CommandName="ZoomIn" />
                        <rad:ImageEditorTool Text="ZoomOut" CommandName="ZoomOut" />
                        <rad:ImageEditorToolSeparator></rad:ImageEditorToolSeparator>
                        <rad:ImageEditorTool CommandName="Crop"></rad:ImageEditorTool> 
                    </rad:ImageEditorToolGroup>
                </Tools>
            </rad:RadImageEditor>
            <asp:Label ID="lblCropTip" runat="server"  Text="After cropping a photo, click Crop and then Save."></asp:Label>
            <asp:Label ID="lblIEUserMsg" runat="server"  Text="Internet Explorer users may need to refresh the image before cropping."></asp:Label>
            <div class="actions">
                <asp:Button ID="btnCropImage" class="submitBtn" runat="server"  Text="Crop" OnClientClick="return CropImage();"  CausesValidation="false" />
                <asp:Button ID="btnSaveProfileImage" class="submitBtn" runat="server"  Text="Save"  CausesValidation="false"  />
                <asp:Button ID="btnCancelProfileImage" runat="server" Text="Cancel" class="submitBtn" OnClick="btnCancelProfileImage_Click"  CausesValidation="false"  />
            </div>
        </panel>
    </ContentTemplate>
</rad:RadWindow>
