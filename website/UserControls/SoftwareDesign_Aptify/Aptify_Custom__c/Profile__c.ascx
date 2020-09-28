<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/Profile__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.ProfileTest__c" %>
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


<!-- Bootstrap 4 -->

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
<!-- Jquery UI  CSS -->
<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<link href="../../../CSS/bootstrap-override.min.css" rel="stylesheet" />
<!-- jQuery and Bootstrap JS -->
<%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
<script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<%-- %><script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>--%>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>





<script type="text/javascript" >

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
        window.document.getElementById('<%= lblErrormessage.ClientID %>').innerHTML = "";
        window.document.getElementById('<%= lblErrormessage.ClientID %>').value = "";
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

        window.document.getElementById('<%= lblErrorHomeCountry.ClientID %>').innerHTML = "";
        window.document.getElementById('<%= lblErrorHomeCountry.ClientID %>').value = "";
        var radwindow1 = $find('<%=radUpdateHomeAddress.ClientID%>');
        radwindow1.set_modal(true);
        radwindow1.show();
    }

    function HideUpdateHomeAddress() {
        window.document.getElementById('<%= lblErrorHomeCountry.ClientID %>').innerHTML = "";
        window.document.getElementById('<%= lblErrorHomeCountry.ClientID %>').value = "";
        var radwindow1 = $find('<%=radUpdateHomeAddress.ClientID%>');
        radwindow1.hide();
        radwindow1.set_modal(false);
    }

    function ShowChangeCompany() {
        var hid = document.getElementById("baseTemplatePlaceholder_content_Profile__c_hidCheckFlag");
        hid.value = "Edit";
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
        document.getElementById("<%=txtAddCompName.ClientID%>").value = ""
        document.getElementById("<%=txtAddCompLine1.ClientID%>").value = ""
        document.getElementById("<%=txtAddCompLine2.ClientID%>").value = ""
        document.getElementById("<%=txtAddCompLine3.ClientID%>").value = ""

        document.getElementById("<%=txtAddCompCity.ClientID%>").value = ""
        document.getElementById("<%=txtAddCompCounty.ClientID%>").value = ""
        document.getElementById("<%=txtAddCompIntlCode.ClientID%>").value = ""
        document.getElementById("<%=txtAddCompPhoneAreaCode.ClientID%>").value = ""
        document.getElementById("<%=txtAddCompPhone.ClientID%>").value = ""

        var radwindow1 = $find('<%=radAddNewCompany.ClientID%>');
        radwindow1.set_modal(true);
        radwindow1.show();
    }

    function HideAddNewCompany() {
        var radwindow1 = $find('<%=radAddNewCompany.ClientID%>');
        var radwindow2 = $find('<%=radChangeCompany.ClientID%>');
        radwindow1.hide();
        radwindow1.set_modal(false);
        radwindow2.set_modal(true);
        radwindow2.show();
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


    function HideEditBusinessAddress() {
        var radwindow1 = document.getElementById("btnEditPrimarybusiness");
        radwindow1.style.display = "none";

    }

    function AddAnotherBusiness() {
        var addNewB = document.getElementById("Removethiscompanyrecord");
        addNewB.style.display = "none";
        var hid = document.getElementById("baseTemplatePlaceholder_content_Profile__c_hidCheckFlag");
        hid.value = "Add";

        var radwindow1 = $find('<%=radChangeCompany.ClientID%>');
        radwindow1.set_modal(true);
        radwindow1.show();
    }

    //Added by Deepika on 12/12/2017 for log #18655
    //To avoid double click on the submit changes button when adding new company.
    function DisableBtn() {
        if (Page_ClientValidate("")) {
            document.getElementById('<%=btnAddNewCompany.ClientID%>').value = "Please Wait..";
            document.getElementById('<%=btnAddNewCompany.ClientID%>').setAttribute("class", "DisablePayBtn");
            document.getElementById('<%=btnAddNewCompany.ClientID%>').disabled = true;
        }
    }
    //End here 


</script>

<style type="text/css">
/*Jim code begins :#20057*/
	
.ui-autocomplete { z-index:2147483647; }

.ui-autocomplete { height: 200px; overflow-y: scroll; overflow-x: hidden;}
/*Jim code begins :#20057*/

    .watermarked {
        background-color: #FFFFFF;
        border: 0px solid #BEBEBE;
        color: #484848;
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

<%--Added by Deepika on 12/12/2017 for log #18655
  To avoid double click on the submit changes button when adding new company.--%>
<style type="text/css">
    .DisablePayBtn {
        background-color: gray !important;
        color: white !important;
        padding: 8px 20px !important;
        height: 40px !important;
        display: inline-block !important;
        text-transform: uppercase !important;
        border: 2px solid transparent !important;
        margin-right: 5px !important;
    }
</style>
<%--End here --%>


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
	.maroon-info{background-color:#8C1D40!important;color:#FFF!important;}
	.maroon-info:before{content: "NOTE: "!important;}
</style>
<%-- Susan Wong, Ticket #18528 [PHASE 1] improve usability start - ADD LOADING SCREEN --%>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="updatePanelButton">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>LOADING...<br />
                   Institute approval needed for changes to: <br />Prefix | Last name | Email | Employment status | Addresses<br />
                    All other changes will take affect immediately.
                </span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="updatePanelButton" runat="server" ChildrenAsTriggers="True">
    <ContentTemplate>
<%-- Susan Wong, Ticket #18528 [PHASE 1] improve usability end - ADD LOADING SCREEN --%>
<div id="Container">
    <div id="RightDiv">
        <div id="tblMain" runat="server" width="100%" class="data-form">
            <%-- Susan Wong, Ticket #18528 [PHASE 1] improve usability start - PENDING CHANGES SECTION --%>
            <p class="info-note">Changes to any of the below will require Institute approval, once approved they will be updated.<br /><strong>Approval needed:</strong> Prefix | Last Name | Email | Employment Status | Addresses section<br /><strong>ALL</strong> other changes will be immediate</p>
            <div class="cai-form">
                <div id="divPendingChange" runat="server" class="pending-changes">
                    <div class="trigger-title">
                        <h2>Your pending changes<span class="minus"></span></h2> 
                        <p class="pending-num-para">You have <span class="pending-num"></span> changes pending approval</p>
                    </div>
                    <div class="trigger-section default-expanded">
                        <div class="cai-form-content">
                            <uc4:PendingChangesDetailsControl ID="PendingChangesDetails1" runat="server" />
                        </div>
                        <div class="profile-help-info">
                            <p class="info-tip">Scroll up/down on the table above to see more</p>
                            <%-- Susan Wong, Ticket #18528 [PHASE 1] improve usability end - PENDING CHANGES SECTION --%>
                        </div>
                    </div>
                </div>
                <%-- Susan Wong, Ticket #20072 START --%>
                <div>
                    <div>
                        <asp:Label ID="lblPasswordsuccess" runat="server"></asp:Label>
                        <asp:HiddenField ID="hidCheckFlag" Value="" runat="server" />
                    </div>
                    <div class="personal-info-title trigger-title"><span class="form-title">Personal information<span class="plus"></span></span></div>
                    <div class="personal-info-section profile-sections trigger-section">
                        <div class="form-section-full-border">
	                        <div runat="server" id="trUserID" visible="false" class="field-group">
		                        <asp:HiddenField ID="hfCompanyID" Value="-1" runat="server" ClientIDMode="Static"  />
		                        <asp:HiddenField ID="hidCompanyIdOld" Value="-1" runat="server" />
		                        <asp:Label ID="Label7" runat="server" CssClass="profile-id">User ID: </asp:Label>
		                        <asp:Label ID="lblUserID" Text="" CssClass="profile-id profile-user" runat="server"></asp:Label>
	                        </div>
                        </div>
                        <div class="form-section-full-border">
                            <div runat="server" id="divStatus" visible="false" class="field-group">
	                            <asp:Label ID="lblTextStatus" runat="server"  CssClass="profile-id">Status:</asp:Label>
	                            <asp:Label ID="lblStatus" Text="" Style="font-weight: bolder;" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="form-section-half-border">
	                        <div class="field-group">
		                        <asp:Label ID="lblSalutation" runat="server" CssClass="label-title">Prefix<span class="required"></span></asp:Label>
                                <asp:DropDownList ID="cmbSalutation" CssClass="txtBoxEditProfileForDropdown" ToolTip ="Changes to prefix requires supporting documentation" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="cmbSalutation" ID="reqPrefix" CssClass="reqValidators" ValidationGroup="ProfileControl" ForeColor="Red" ErrorMessage="Prefix required" InitialValue="" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
	                        </div>
                        </div>
                        <div class="form-section-half-border">
	                        <div class="field-group">
		                        <asp:Label ID="lblPreferredSalutation" runat="server" CssClass="label-title">Preferred first name</asp:Label>
		                        <asp:TextBox SkinID="RequiredTextBox" CssClass="txtBoxEditProfile" ID="txtPreferredSalutation" runat="server"></asp:TextBox>
	                        </div>
                        </div>
                        <div class="form-section-half-border">
	                        <div class="field-group">
		                        <asp:Label ID="lblName" runat="server" CssClass="label-title">First name(s)</asp:Label>
                                <asp:TextBox SkinID="RequiredTextBox" Enabled="false" CssClass="txtBoxEditProfile" ID="txtFirstName" runat="server"></asp:TextBox>
	                        </div>
                        </div>
                        <div class="form-section-half-border">
	                        <div class="field-group">
		                        <asp:Label ID="Label4" runat="server" CssClass="requiredLabel label-title">Last name <span class="required"></span></asp:Label>
		                        <asp:TextBox SkinID="RequiredTextBox" ID="txtLastName" ToolTip ="Changes to a surname requires supporting documentation" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
		                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="reqValidators" runat="server" ControlToValidate="txtLastName" ValidationGroup="ProfileControl" ErrorMessage="Last name required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
	                        </div>
                        </div>
                        <div class="form-section-half-border">
	                        <div class="field-group">
		                        <asp:Label ID="lblDob" CssClass="label-title" runat="server">Date of birth</asp:Label>
		                        <asp:TextBox SkinID="RequiredTextBox" ID="txtDob" Enabled="false" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
	                        </div>
                        </div>
                        <div class="form-section-half-border">
	                        <div class="field-group">
		                        <asp:Label ID="lblGender" runat="server" CssClass="label-title">Gender</asp:Label>
                                <asp:TextBox SkinID="RequiredTextBox" ID="txtGender" Enabled="false" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
		                        <%-- <asp:DropDownList ID="cmbGender" CssClass="txtBoxEditProfileForDropdown" runat="server">
			                        <asp:ListItem Value="0">Male</asp:ListItem>
			                        <asp:ListItem Value="1">Female</asp:ListItem>
			                        <asp:ListItem Value="2" Selected="True">Unknown</asp:ListItem>
		                        </asp:DropDownList>--%>
	                        </div>
                        </div>
                        <div class="form-section-half-border">
	                        <div class="field-group">
		                        <asp:Label ID="lblCountryoforigin" runat="server" CssClass="label-title">Country of origin</asp:Label>
		                        <asp:TextBox SkinID="RequiredTextBox" ID="txtCountryoforigin" Enabled="false" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
	                        </div>
                        </div>
                        <div class="form-section-half-border">
	                        <div class="field-group">
		                        <asp:Label ID="lblEmail" runat="server" CssClass="label-title">Email<span class="required"></span></asp:Label>
		                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtBoxEditProfile" ValidationGroup="ProfileControl" SkinID="RequiredTextBox"></asp:TextBox>
		                        <span class="txtbox-help-text">Ensure your email is correct for correspondence use</span>
		                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="reqValidators" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email required" ForeColor="Red" Display="Dynamic" ValidationGroup="ProfileControl"></asp:RequiredFieldValidator>
		                        <asp:RegularExpressionValidator ID="regexEmailValid" CssClass="reqValidators" runat="server" Display="Dynamic" ControlToValidate="txtEmail" ErrorMessage="Invalid email format" ForeColor="Red" ValidationGroup="ProfileControl"></asp:RegularExpressionValidator>
	                        </div>
                        </div>
                        <div class="form-section-full-border" style="padding:0px"></div>
                        <div class="form-section-half-border">
	                        <div class="field-group">
		                        <asp:Label ID="lblMobile" runat="server" CssClass="label-title">Mobile<span class="required"></span></asp:Label>
                                <asp:TextBox ID="txtmobileCountryCode" CssClass="txtUserProfileAreaCodeSmall" runat="server" Width="20%"></asp:TextBox>
		                        <asp:TextBox ID="txtMobileAreaCode" CssClass="txtUserProfileAreaCodeSmall" runat="server" Width="20%"></asp:TextBox>
		                        <asp:TextBox ID="txtMobileNumber" CssClass="txtUserProfileAreaCodeSmall" runat="server" Width="56.55%"></asp:TextBox>
                                <span class="txtbox-help-text">Country code | mob prefix | number</span>
                                <span class="reqValidators error-req-numbers" style="color:red;display:none;">You must have at least 1 phone number</span>
	                        </div>
                        </div>
                        <div class="form-section-half-border">
	                        <div class="field-group">
		                        <asp:Label ID="lblLandline" runat="server" CssClass="label-title">Landline</asp:Label>
                                <asp:TextBox ID="txtlandlineCountryCode" CssClass="txtUserProfileAreaCodeSmall" runat="server" Width="20%"></asp:TextBox>
		                        <asp:TextBox ID="txtlandlineAreaCode" CssClass="txtUserProfileAreaCodeSmall" runat="server" Width="20%"></asp:TextBox>
		                        <asp:TextBox ID="txtlandlineNumber" CssClass="txtUserProfileAreaCodeSmall" runat="server" Width="56.55%"></asp:TextBox>
                                <span class="txtbox-help-text">Country code | area prefix | number</span>
                                <span class="reqValidators error-req-numbers" style="color:red;display:none;">You must have at least 1 phone number</span>
	                        </div>
                        </div>
                        <div class="form-section-full-border">
                            <div class="field-group">
	                            <a class="cai-btn cai-btn-navy-inverse btn-open-pw" style="font-weight: 600;  cursor: pointer;" onclick="ShowChangePasswordWindow();">Change my password</a>
                            </div>
                            <div class="field-group">
	                            <span class="required"></span> These fields are required
                            </div>
                        </div>
                    </div>
                    <div class="employment-info-title trigger-title"><span class="form-title">Your employment details<span class="plus"></span></span></div>
                    <div class="employment-info-section profile-sections trigger-section">
                        <%-- Susan Wong, #20099 START --%>
	                    <div class="for-student">
		                    <p class="info-note">If you are currently under a formal training contract, you cannot submit changes to your firm details through this form. Please contact <a href="mailto:studentqueries@charteredaccountants.ie">studentqueries@charteredaccountants.ie</a> to make any such changes.</p>
	                    </div>
	                    <div class="for-reg-user">
                            <p class="info-note">If you are applying to become a student please contact <a href="mailto:studentqueries@charteredaccountants.ie">studentqueries@charteredaccountants.ie</a> to make any such changes.</p>
                            <p>Registered users who are not members/students of the Institute, do not need to provide employment/business details.</p>
	                    </div>
                        <%-- Susan Wong, #20099 END --%>
                        <div class="form-section-half-border for-member">
	                        <div class="field-group">
		                        <asp:Label ID="lblEmploymentStatus" runat="server" CssClass="label-title">Employment status</asp:Label>
		                        <asp:DropDownList ID="cmbempstatus" CssClass="txtBoxEditProfileForDropdown" runat="server"></asp:DropDownList>
	                        </div>
                        </div>
                        <div class="form-section-half-border for-member-reg-user"><%-- Susan Wong, #20099 --%>
	                        <div class="field-group">
		                        <asp:Label ID="lblJobfunction" runat="server" CssClass="label-title">Job function</asp:Label>
		                        <asp:DropDownList ID="cmbJobfunction" title="Please select the closest to your job role" CssClass="txtBoxEditProfileForDropdown" runat="server"></asp:DropDownList>
	                        </div>
                        </div>
                        <div class="form-section-half-border for-member-reg-user"><%-- Susan Wong, #20099 --%>
	                        <div class="field-group">
		                        <asp:Label ID="lblTitle" runat="server" CssClass="label-title">Job title</asp:Label>
		                        <asp:TextBox SkinID="RequiredTextBox" title="Your personal job title." ID="txtJobTitle" CssClass="txtBoxEditProfile" runat="server"></asp:TextBox>
	                        </div>
                        </div>
                    </div>
                    <div class="address-info-title trigger-title"><span class="form-title">Home & work addresses<span class="plus"></span></span></div>
                    <div class="address-info-section profile-sections trigger-section">
                        <%-- Susan Wong, #20099 start--%>
                        <div class="for-student">
                            <p class="info-note">If you are a student in contract please contact <a href="mailto:StudentQueries@charteredaccountants.ie">StudentQueries@charteredaccountants.ie</a> to update your business contact details.</p>
                        </div>
                        <%-- Susan Wong, #20099 end--%>
                        <p class="info-warning">You have not selected a preferred address yet.<br />Please select your preferred address now (used for correspondences from the Institute).</p>
                        <div class="form-section-full-border">
		                        <asp:RadioButtonList ID="rblPAddress" CssClass="profile-address-tab" runat="server" RepeatDirection="Horizontal">
			                        <asp:ListItem>Home</asp:ListItem>
			                        <asp:ListItem>Work</asp:ListItem>
		                        </asp:RadioButtonList>
                        </div>
                        <div class="home-address-section">
                            <div class="form-section-full-border">
	                            <div class="field-group add-border-space addresses-panel" style="border-bottom:0px">
		                            <asp:Label ID="Label2" runat="server" Style="font-size:18px;font-weight:bolder">Set as preferred mailing address</asp:Label>
                                    <span class="prefered-address"><span class="address-icon address-icon-off"></span></span>
                                    <span class="whats-this-icon address-tooltip-text">
                                        <span class="tooltip-click-box">Click to set/unset this as your prefered</span> ?
                                    </span>
	                            </div>
                            </div>
                            <div class="form-section-full-border">
	                            <div class="field-group add-border-space addresses-panel">
                                    <div class="addresses-half-section">
                                        <asp:Label ID="lblPrefHome" Visible ="false" runat="server" CssClass="preferred-address" Text="This is your preferred mailing address"></asp:Label>
		                                <span class="label-title">Home address</span>
                                        <div id="txtHomeAddress" runat="server"></div>
                                    </div>
                                    <div class="addresses-half-section center">
                                        <a id="btnHomeEdit" onclick="ShowUpdateHomeAddress()" class="cai-btn cai-btn-navy"><i class="fa fa-pencil" aria-hidden="true"></i> &nbsp;Edit home address</a>
                                    </div>
	                            </div>
	                            <%--<div class="field-group"><table><tr><td></td><td></td></tr></table></div>
	                            <div class="field-group sfContentBlock">
		                            <input id="btnHomeEdit" type="button" onclick="ShowChangeCompany()" class="cai-btn cai-btn-navy-inverse" value="Edit" />
	                            </div>--%>
                            </div>
                        </div> 
                        <div class="bus-address-section">
                            <div class="form-section-full-border">
	                            <div class="field-group add-border-space addresses-panel" style="border-bottom:0px">
		                            <asp:Label ID="Label1" runat="server" Style="font-size:18px;font-weight:bolder">Set as preferred mailing address</asp:Label>
                                    <span class="prefered-address"><span class="address-icon address-icon-off"></span></span>
                                    <span class="whats-this-icon address-tooltip-text">
                                        <span class="tooltip-click-box">Click to set/unset this as your prefered</span>
                                        ?
                                    </span>
	                            </div>
                            </div>
                            <div class="form-section-full-border">
	                            <div class="field-group add-border-space addresses-panel">
                                    <div class="addresses-half-section">
                                        <asp:Label ID="lblPrefBusiness" Visible ="false"  runat="server" CssClass="preferred-address" Text="This is your preferred mailing address"></asp:Label>
                                        <span class="label-title">Primary business address</span>
                                        <div id="divPrimarybusiness" runat="server"></div>
                                    </div>
                                    <div class="addresses-half-section center">
                                        <a id="btnEditPrimarybusiness" onclick="ShowChangeCompany()" class="cai-btn cai-btn-navy"><i class="fa fa-pencil" aria-hidden="true"></i> &nbsp;Edit my company</a>
                                    </div>
                                    <p class="info-note mem-prac">The above is your company name; your firm trading name is referenced in the <a href="https://www.charteredaccountants.ie/Find-a-Firm#firm" target="_blank">firms directory</a>.</p>
	                            </div>
	                            <%--<div class="field-group"><table><tr><td></td><td></td></tr></table></div>
	                            <div class="field-group sfContentBlock">
		                            <input id="btnEditPrimarybusiness" type="button" onclick="ShowChangeCompany()" class="cai-btn cai-btn-navy-inverse" value="Edit" />
	                            </div>--%>
                            </div>
                            <%-- Susan Wong, #20067 Don't need this
                            <div class="form-section-half-border">
	                            <div class="field-group">
		                            <table style="width:100%;">
			                            <tr><td style="width:13%;" ><img alt=""  title="" src="/Images/ProfileContractAlert.png" /></td><td></td></tr>
		                            </table>
	                            </div>
                            </div>
                            --%>
                            <div class="form-section-full-border">
	                            <div class="field-group add-border-space addresses-panel">
			                        <span class="label-title">Other business address</span>
			                        <asp:DataList ID="lstAddress" runat="server" RepeatColumns="2" Width="100%">
				                        <SelectedItemStyle></SelectedItemStyle>
				                        <HeaderTemplate><font class="label-title"></font></HeaderTemplate>
                                        <AlternatingItemStyle></AlternatingItemStyle>
                                        <ItemStyle Width="350px" VerticalAlign="Bottom" HorizontalAlign="NotSet"></ItemStyle>
                                        <ItemTemplate>
					                        <div style="vertical-align: bottom;" class="divstylechangeAddress">
                                                <font size="2">
							                        <b><%# DataBinder.Eval(Container.DataItem, "CompnayName")%></b>
							                        <br />
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
						                        <asp:Button ID="cmdEditAddress"  Visible ="false" AlternateText="Edit" runat="server" Text="Edit" CssClass="submitBtn" />
						                        <asp:Button ID="btnDeleteAddress" AlternateText="Delete"  CommandArgument='<%# Eval("ID") %>' CommandName ="btnDeleteAddress"  runat="server" Text="Delete" CssClass="submitBtn" />
					                        </div>
                                        </ItemTemplate>
                                        <FooterStyle>
                                        </FooterStyle>
                                        <HeaderStyle></HeaderStyle>
			                        </asp:DataList>
	                            </div>
                            </div>
                            <div class="sfContentBlock center" style="margin-top:18px;">
			                    <%--<input id="btnAddanotherbusiness" type="button" onclick="AddAnotherBusiness()" class="cai-btn cai-btn-navy-inverse" value="Add another business address" />--%>
			                    <a id="btnAddanotherbusiness" type="button" onclick="AddAnotherBusiness()" class="cai-btn cai-btn-navy-inverse"><i class="fa fa-plus" aria-hidden="true"></i> &nbsp;Add another business address</a>
		                    </div>
                        </div>
                    </div>
                    <div style="text-align:center;padding-bottom: 50px;">
                        <%-- Susan Wong, Ticket #18528 [PHASE 1] start - ERROR MESSAGES --%>
                        <div class="missing-req"></div>
                        <p class="info-error errorNums" style="display:none;">You must have at least 1 phone number</p>
                        <asp:Button ID="btnSubmit" runat="server" width="60%" CssClass="submitBtn prof-update-btn" style="margin-top:10px" Text="Submit my changes" ValidationGroup="ProfileControl" />
                        <%-- Susan Wong, Ticket #18528 [PHASE 1] end - ERROR MESSAGES --%>
                        <br />
                    </div>
                    <%--<asp:TextBox class="form-control" id="tb" runat="server" ClientIDMode="Static" placeholder="Company"/>--%>
                </div>
                <%-- Susan Wong, Ticket #20072 END --%>
                <cc1:User ID="User1" runat="server"></cc1:User>
                <cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
                <cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
            </div>
        </div>
    </div>
    <div>
        <rad:RadWindow ID="radwinPassword" runat="server" Width="350px" Modal="True" BackColor="#DADADA"
            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Change password"
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
                                            <%-- Susan Wong, Ticket #18528 [PHASE 1] start - PASSWORD MESSAGE --%>
                                            <tr>
                                                <td colspan="2" class="tdCompairvalidationErrormessage">
                                                    <asp:Label ID="lblerrorLength" runat="server"></asp:Label>                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td colspan="2" class="tdValidationcolor tdValidationErrormessage">
                                                    <asp:CompareValidator ID="CompareValidator" runat="server" ControlToValidate="txtRepeat"
                                                    Display="Dynamic" ControlToCompare="txtNewPassword" ErrorMessage="The new passwords must match. Please try again."></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="tdValidationErrormessage">
                                                    <asp:Label ID="lblErrormessage" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                           <%-- Susan Wong, Ticket #18528 [PHASE 1] end - PASSWORD MESSAGE --%>
                                            <tr>
                                                <td align="right" valign="top" class="tablecontrolsfontLogin">
                                                    <font color="red">*</font>
                                                    <asp:Label ID="Label15" runat="server">Current password</asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtoldpassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="tablecontrolsfontLogin">
                                                    <font color="red">*</font>
                                                    <asp:Label ID="lblPassword" runat="server">New password</asp:Label>&nbsp;
                                                </td>
                                                <td style="padding-top: 2px;">
                                                    <asp:TextBox ID="txtNewPassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="tablecontrolsfontLogin">
                                                    <font color="red">*</font>
                                                    <asp:Label ID="Label18" runat="server">Repeat password</asp:Label>&nbsp;
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
                            <div class="tablecontrolsfontLogin actions-pop-up marg-top-20px" style="margin-bottom:10px;">
                                <%-- Susan Wong, Ticket #18528 [PHASE 1] start - PASSWORD MESSAGE --%>
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return ShowvalidationErrorMsg()"
                                    CssClass="submitBtn btn-save-pw" />
                                <%-- Susan Wong, Ticket #18528 [PHASE 1] end - PASSWORD MESSAGE --%>
                                <input type="submit" value="Cancel" onclick="HideChangePasswordWindow();" class="submitBtn">
                            </div>
                        </div>
                        <div style="clear:both;"></div>
                        <div>
                            <table>
                                <tr>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 1] start - PASSWORD MESSAGE --%>
                                    <%-- <td class="tdValidationErrormessage">
                                        <asp:Label ID="lblErrormessage" runat="server" Text=""></asp:Label>
                                    </td> --%>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 1] end - PASSWORD MESSAGE --%>
                                </tr>
                                <tr>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 1] start - PASSWORD MESSAGE --%>
                                    <p class="info-tip">Password must contain at least 8 characters with at least 1 uppercase, 1 lowercase and 1 number</p>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 1] end - PASSWORD MESSAGE --%>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 1] start - PASSWORD MESSAGE --%>
                                    <%-- <td class="tdValidationcolor tdValidationErrormessage">
                                        <asp:CompareValidator ID="CompareValidator" runat="server" ControlToValidate="txtRepeat"
                                            Display="Dynamic" ControlToCompare="txtNewPassword" ErrorMessage="The new passwords must match. Please try again."></asp:CompareValidator>
                                    </td> --%>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 1] end - PASSWORD MESSAGE --%>
                                </tr>
                                <tr>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 1] start - PASSWORD MESSAGE --%>
                                    <%--<td class="tdCompairvalidationErrormessage">
                                        <asp:Label ID="lblerrorLength" runat="server"></asp:Label>
                                    </td>--%>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 1] end - PASSWORD MESSAGE --%>
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
        <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT HOME ADDRESS --%>
        <rad:RadWindow ID="radUpdateHomeAddress" runat="server" Width="450px" Height="600px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Edit home address (changes require approval)" Behavior="None">
        <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT HOME ADDRESS --%>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <div class="cai-form">
                        <table class="tblEditAtendee">
                            <div class="field-group"></div>
                            <tr>
                                <div class="field-group">
                                <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT HOME ADDRESS --%>
                                <%--<td colspan="2">
                                    <asp:Label ID="lblheader" style="font-size: 11pt;font-weight:bold;" runat="server" Text="Update your home address:"  />
                                </td>--%>
                                <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT HOME ADDRESS --%>
                                </div>
                            </tr>

                            <tr>
                             <div class="field-group">
                                <td colspan="2">
                                    <asp:Label ID="lblErrorHomeCountry" runat="server" Text="" ForeColor="Red" />
                                </td>
                                </div>
                            </tr>

                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label38" runat="server" Text="Address line 1"></asp:Label><span class="required"></span><%-- Susan Wong, #20057 --%>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server" ControlToValidate="txtHomeAddLine1" ValidationGroup="Altok" ErrorMessage="Blank value not allowed for address line 1" Display="Dynamic"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtHomeAddLine1" runat="server" Width="250px"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="Label39" runat="server" Style="text-align: left">Address line 2</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtHomeAddLine2" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="Label3" runat="server" Style="text-align: left">Address line 3</asp:Label></td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtHomeAddLine3" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="Label5" runat="server" Style="text-align: left">Town/city</asp:Label></td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtHomeAddCity" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label40" runat="server" Style="text-align: left" >Post code</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtHomeAddPostalCode" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Style="text-align: left">County</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtHomeAddCounty" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label41" runat="server" Style="text-align: left">Country</asp:Label><span class="required"></span><%-- Susan Wong, #20057 --%>
                                </td>
                                <td class="RightColumn">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="red"
                                    ErrorMessage="Please add a country" ControlToValidate="ddlHomeAddCountry" InitialValue="Select country"  Display="Dynamic"
                                    ValidationGroup="Altok"></asp:RequiredFieldValidator>
                                   <%-- Updated by GM for #18656--%>
                                    <asp:DropDownList ID="ddlHomeAddCountry" AutoPostBack="false" CssClass="cmbUserProfileState" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr style="display:none;">
                                <td>
                                    <asp:Label ID="Label8" runat="server" Style="text-align: left">State</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="ddlHomeAddState" CssClass="cmbUserProfileState" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--</div>--%>
                        </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table class="center">
                    <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT HOME ADDRESS --%>
                    <%--<tr>
                        <div class="cai-form">
                       <div class="field-group">
                        <td colspan="2" style="font-size: 11pt; padding: 5px"><p>Note: changes to home address must be approved by the Institute, because they can impact on billing.
This can take up to  2 business days.</p></td>
                        </div>
                            </div>
                    </tr>--%>
                    <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT HOME ADDRESS --%>
                    <tr>
                        <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT HOME ADDRESS --%>
                        <%--<td style="width: 150px">&nbsp
                        </td>--%>
                        <td class="RightColumn actions">
                            <asp:Button ID="btnUpdateHomeAdd" runat="server" Text="Submit change" class="submitBtn cai-btn cai-btn-red" ValidationGroup="Altok" />
                            <asp:Label ID="Label45" runat="server" Style="text-align: left" Visible="false"></asp:Label>
                            <input type="button" value="Cancel" onclick="HideUpdateHomeAddress();" class="cai-btn cai-btn-red-inverse">
                        </td>
                        <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT HOME ADDRESS --%>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>

    </div>



    <div>
        <%-- Susan Wong, #20057 Make edit company easier to use --%>
        <rad:RadWindow ID="radChangeCompany" runat="server" Height="700px" Modal="True" CssClass="edit-business-address-modal"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Edit company details (changes require approval)" Behavior="Close"  OnClientClose="OnClientCloseReset">
        <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS --%>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <table class="tblEditAtendee">
                            <tr>
                                <td>
                                    <div runat="server" id="div6" class="form-section-full-border">
                                        <%-- Susan Wong, #20057 change style to match Barbaras UI --%>
                                        <h2 class="modal-h2" style="margin-top:20px">Current company:</h2>                      
                                        <asp:Label ID="lblCurrentCompanyName" Text="" runat="server" CssClass="current-comp"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <div runat="server" id="div2" class="form-section-full-border" style="margin-bottom:0px">
                                        <%-- Susan Wong, #20057 change style to match Barbaras UI --%>
                                        <h2 class="modal-h2">Update my company to:</h2>
                                   </div>
                                </td>
                            </tr>
                            <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS --%>
                            <%--<tr>
                                <td>
                                    <div class="field-group"><span style="font-weight:300; text-align: left;"> 
                                        <p>Start by typing in the box below to pick a name from the Institutes's list of companies</p>
                                        <p>OR</p>
                                        <p>Use the button to create a new company record</p>
                                        </span>

                                    </div>                                                     
                                </td>
                            </tr>--%>
                            <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS --%>
                            <tr>
                                <td align="left">
                                  <%--  <div class="center" id="div4" runat="server">
                                        <asp:RequiredFieldValidator ID="reqtxtCompany" runat="server" ControlToValidate="idcselect"
                                        ValidationGroup="CreateCompany" ErrorMessage="Company name required" CssClass="info-error add-existing"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblChooseCompany" runat="server" Text="You must click a company from the dropdown list" Visible="false" CssClass="info-error add-existing" />
                                    </div>--%>
                                     <%--Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS --%>
                                    <div class="field-group center" id="div3" runat="server">
                                  <%--  <p style="font-size:16px;margin-bottom:10px" class="add-existing"><strong>Search the registry!</strong> Type in the textbox below to check if your new company is already on the list.</p>
                                    <asp:TextBox ID="txtCompany" placeholder="Start typing now to search"  CssClass="txtBoxEditProfile add-existing" ValidationGroup="CreateCompany" runat="server" style="display: none; border: 1px solid #000000;"></asp:TextBox>
                                    <Ajax:AutoCompleteExtender ID="extCompany" runat="server" BehaviorID="autoComplete"
                                        CompletionInterval="10" CompletionListElementID="divwidth" CompletionSetCount="10"
                                        EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetCompanyList1"
                                        ServicePath="~/GetCompanyList__c.asmx" TargetControlID="txtCompany" OnClientItemSelected="ClientItemSelected"
                                        OnClientPopulating="companyitemselecting" CompletionListCssClass="autocomplete_completionListElement">
                                    </Ajax:AutoCompleteExtender>--%>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS 
                                     <Ajax:TextBoxWatermarkExtender ID="WatermarkExtender1" runat="server" TargetControlID="txtCompany"
                                        WatermarkText="Search existing company registry" WatermarkCssClass="watermarked add-existing" /> --%>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS --%>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS --%>
                                    <%--Ashwini Junagade #18654 add Tooltip and label start--%>
                                    <%--Jim Code Begin:#20057--%>
                                     <form class="form-inline" autocomplete="off">
                                        <%-- Susan Wong, #20057 layout change START --%>
                                         <div style="text-align:left">
	                                        <p class="info-note">Please fill in <strong>country</strong> first, then <strong>city/town</strong> then search for <strong>company name</strong></p>
                                        </div>	 
                                        <div class="form-row">
                                             <div class="form-group col-sm-12 col-md-3">
                                                <span class="label-title">Country<span class="required"></span></span>
                                                <select id="idcountry" class="form-control" title="Select a country">
                                                    <option selected disabled hidden>Country</option> <%-- Susan Wong, #20057 required field --%>
                                                </select>
                                                <div id="countrycount"></div>
                                             </div>
                                             <div class="form-group col-sm-12 col-md-3">
                                                 <span class="label-title">City/town<span class="required"></span></span>
                                                 <asp:TextBox id="idcity" runat="server" class="form-control" ClientIDMode="Static" title="City or county (min 3 characters)" placeholder="City or county (min 3 characters)" />
                                                 <span class="clearer glyphicon glyphicon-remove-circle form-control-feedback"></span>
                                                 <div id="citycount"></div>
                                             </div>
                                            <div class="form-group col-sm-12 col-md-6">
                                                <span class="label-title">Company name<span class="required"></span></span>
                                                <asp:TextBox class="form-control" id="idcselect" runat="server" ClientIDMode="Static" title="Search by company name (min 3 characters)" placeholder="Search by company name (min 3 characters)"/>
											    <%-- <span class="form-control-clear glyphicon glyphicon-remove form-control-feedback hidden"></span>--%>
                                                <div id="companycount"></div> 
                                            </div>
                                         </div>
                                         <%-- Susan Wong, #20057 layout change END --%>
                                         <%-- Aleksei Jusev, zendesk #7993, disable enter key for city and company fields START --%>
                                             <script type="text/javascript">
                                                 jQuery(function () {
                                                     var idcity = "<%= idcity.ClientID %>";
                                                     var idcselect = "<%= idcselect.ClientID %>";
                                                     function td_disableKeyOnEnter(e) {
                                                         if (event.keyCode === 10 || event.keyCode === 13) {
                                                             event.preventDefault();
                                                             event.stopPropagation();
                                                         }
                                                     }

                                                     jQuery("#" + idcity).keypress(td_disableKeyOnEnter);
                                                     jQuery("#" + idcselect).keypress(td_disableKeyOnEnter);
                                                 });
                                             </script>
                                         <%-- Aleksei Jusev, zendesk #7993, disable enter key for city and company fields END --%>
                                     </form>
                                    <%--Jim Code Ends:#20057--%>
                                    <%-- Susan Wong, #20057 change style to match Barbaras UI --%>
									<div class="form-row button-set">
                                        <div class="col-sm-12 col-md-6">
                                            <asp:Button ID="btnEditCompSave" runat="server" Text="Update company details" ToolTip="Select a pre-existing company from the search results above" class="cai-btn cai-btn-red add-existing"  ValidationGroup="CreateCompany" />
                                        </div>
                                        <div class="col-sm-12 col-md-6">
                                            <input type="button" value="Add new company to registry" title="Can't find your company in the search above? Add a new one here." class="cai-btn cai-btn-navy add-new-comp" onclick="ShowAddNewCompany();">
                                        </div>
									</div>				
                                     <%--Ashwini Junagade #18654 add Tooltip and label end--%> 
                                    <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS --%>
                                    </div>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS --%>
                                </td>
                            </tr>
                           <tr>
                                <td colspan="2">
                                    <div class="field-group center add-company-section">
                                    <p style="font-size:16px;margin-bottom:10px"><strong>Can't find your company on the registry?</strong> Use the button below to add a new company to the registry.</p>
                                    <span style="font-size:16px;margin-bottom:10px"><strong>OR&nbsp;</strong></span>
                                    <a id="try-registry" class="cai-btn cai-btn-navy-inverse">LOOK UP REGISTRY AGAIN</a>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS --%>
                                    <%--<input type="button" id="Removethiscompanyrecord" title="If you've left this company - let us know." value="Remove this company" onclick="ShowDeleteCompany();" class="submitBtn">--%>
                                    <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS --%>
                                    </div>
                                </td>
                            </tr>
                            <%-- Susan Wong, #20057 change style to match Barbaras UI --%>
                            <tr>
                                <td>
                                    <div runat="server" id="div1" class="field-group leave-employment">
                                        <div runat="server" id="div5">
                                            <%-- Susan Wong, #20057 change style to match Barbaras UI --%>
                                            <h2 class="modal-h2">Leaving employment?</h2>
                                        </div>
                                        <p id="LeaveEmploymentText" style="font-size:16px;margin-bottom:10px"><strong>No longer employed?</strong> You can remove the company linked to your account by clicking on the "Remove current company" button.</p>
                                        <input type="button" id="Removethiscompanyrecord" title="If you've left this company - let us know." value="Remove my current company" onclick="ShowDeleteCompany();" class="cai-btn cai-btn-navy-inverse">
                                        <p class="info-note mem-prac">"Members in Practice" must have a company linked to their account, please contact <a href="mailto:ProfessionalStandards@charteredaccountants.ie?subject=Remove company from Profile page for member in practice">ProfessionalStandards@charteredaccountants.ie</a> if you need to remove your company. To change company, use the below form.</p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--<table>                       
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <div class="field-group">
                            <asp:Button ID="btnEditCompSave" runat="server" Text="submit change" class="submitBtn" ValidationGroup="CreateCompany" />
                        </div>
                        </td>
                        <td width="60%">
                            <div class="field-group">
                            <input type="button" value="Cancel" onclick="HideChangeCompany();" class="submitBtn">
                            </div>
                        </td>
                    </tr>
                </table>--%>
            </ContentTemplate>
        </rad:RadWindow>
    <div>
        <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS --%>
        <rad:RadWindow ID="radAddNewCompany" runat="server" Width="500px" Height="620px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Add a new company"
            Behavior="None">
        <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS --%>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <div class="field-group">
                        <table class="tblEditAtendee">
                            <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS --%>
                            <%--<tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label13" style="font-size: 11pt;font-weight:bold;" runat="server" Text="The company may be new to us, or you might find it faster to type the details here:"  />
                                </td>
                            </tr>--%>
                            <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS --%>
                            <tr>
                                <td colspan="2">
                                    <br />
                                    <p class="info-note" style="text-align:left"><strong>Only</strong> add a company if you could't find it in the search</p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblcomp" runat="server" Text="Company name"></asp:Label><span class="required"></span> <%-- Susan Wong, #20057 add required style --%>

                                </td>

                                <td>
                                    <asp:TextBox ID="txtAddCompName" ValidationGroup="AddNewCompany" runat="server" Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqAddCompName" runat="server" ControlToValidate="txtAddCompName"
                                        ValidationGroup="AddNewCompany" ErrorMessage="Company name required" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>

                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Style="text-align: left">Address line 1</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddCompLine1" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Style="text-align: left">Address line 2</asp:Label></td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddCompLine2" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Style="text-align: left">Address line 3</asp:Label></td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddCompLine3" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Style="text-align: left">Town/city<span class="required"></span></asp:Label> <%-- Susan Wong, #20057 add required style --%>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddCompCity" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Style="text-align: left">Post Code</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddCompPostalCode" Width="250px" runat="server"></asp:TextBox>
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
                            <tr style ="display:none;">
                                <td>
                                    <asp:Label ID="Label29" runat="server" Style="text-align: left">State</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:DropDownList ID="cmbAddCompState" CssClass="cmbUserProfileState" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label32" runat="server" Style="text-align: left">County</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddCompCounty" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label27" runat="server" Style="text-align: left">Country<span class="required"></span></asp:Label> <%-- Susan Wong, #20057 add required style --%>
                                </td>
                                <td class="RightColumn">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="red"
                                    ErrorMessage="Please add a country" ControlToValidate="cmbAddCompCountry" InitialValue="Select country"  Display="Dynamic"
                                    ValidationGroup="AddNewCompany"></asp:RequiredFieldValidator>
                                  <%--  changes for log #18745 start--%>
                                    <asp:DropDownList ID="cmbAddCompCountry" CssClass="cmbUserProfileCountry" runat="server"
                                        AutoPostBack="false" Width="250px">
                                      <%--  changes for log #18745 end--%>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="Label25" runat="server" Style="text-align: left">(Intl Code)(Area Code) Phone</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <rad:RadMaskedTextBox ID="txtAddCompIntlCode" CssClass="txtUserProfileAreaCodeSmall"
                                        runat="server" Mask="(####)" Width="15%">
                                    </rad:RadMaskedTextBox>
                                    <rad:RadMaskedTextBox ID="txtAddCompPhoneAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                        runat="server" Mask="(####)" Width="15%">
                                    </rad:RadMaskedTextBox>
                                    <rad:RadMaskedTextBox ID="txtAddCompPhone" CssClass="txtUserProfileAreaCode" runat="server"
                                        Mask="(#######)" Width="40%">
                                    </rad:RadMaskedTextBox>
                                </td>
                            </tr>
                            <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS --%>
                            <%--<tr>
                                <td class="style3"></td>
                                <td class="RightColumn actions"></td>
                            </tr>--%>
                            <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS --%>
                            </div>
                        </table>
                        <%--Changes for #17847 start--%>
                        <table>
                            <tr>
                                <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS --%>
                                <%--<td style="width: 170px">&nbsp
                                </td>--%>
                                <td class="RightColumn actions center">

                                <%--Added by Deepika on 12/12/2017 for log #18655
                                     To avoid double click on the submit changes button when adding new company.--%>
                                    <%-- Susan Wong, #20057 add button classes --%>
                                    <asp:Button ID="btnAddNewCompany" runat="server" Text="Submit change" class="submitBtn cai-btn cai-btn-red" ValidationGroup="AddNewCompany" 
                                     OnClick="btnAddNewCompany_Click" UseSubmitBehavior="false" OnClientClick="javascript:DisableBtn();"/>
                                    <%--End here --%>

                                    <input type="button" value="Cancel" onclick="HideAddNewCompany();" class="cai-btn cai-btn-red-inverse">
                                </td>
                                <%-- Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS --%>
                            </tr>
                        </table>
                        <%--Changes for #17847 end--%>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbAddCompCountry" EventName="SelectedIndexChanged" />
                        <%--Changes for #17847 start--%>
                        <asp:PostBackTrigger ControlID="btnAddNewCompany" />
                        <%--Changes for #17847 end--%>
                    </Triggers>
                </asp:UpdatePanel>
                <%--Changes for #17847 start--%>
                <%--<table>
                    <tr>
                        <td style="width: 170px">&nbsp
                        </td>
                        <td class="RightColumn actions">
                            <asp:Button ID="btnAddNewCompany" runat="server" Text="Submit change" class="submitBtn" ValidationGroup="AddNewCompany" />
                            <input type="button" value="Cancel" onclick="HideAddNewCompany();" class="submitBtn">
                        </td>
                    </tr>
                </table>--%>
                <%--Changes for #17847 end--%>
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
	<%-- Added by Prachi for Redmine #15496--%>
    	<div>
		               <telerik:RadWindow ID="radCompanyDupeCheck" runat="server" Width="350px" Modal="True" BackColor="#f4f3f1"
                VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblCompanyDupeCheck" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div>
                                    <br />
                                </div>
                                <div>
                                    <asp:Button ID="btnCopanyDupeCheck" runat="server" Text="Ok" Width="70px" class="submitBtn" OnClick="btnCopanyDupeCheck_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
	</div>
	<%-- Added by Prachi for Redmine #15496--%>
</div>
<%-- Susan Wong, Ticket #18528 [PHASE 1] improve usability start - ADD LOADING SCREEN --%>
    </ContentTemplate>
</asp:UpdatePanel>
<%-- Susan Wong, Ticket #18528 [PHASE 1] improve usability end - ADD LOADING SCREEN --%>

<script>
    //Susan Wong, Ticket #18528 [PHASE 1] improve usability start - ADD LOADING SCREEN
    function pageLoad() {
        var delay = 10;
        setTimeout(function () {
            //Susan Wong, Ticket #21272 Fix Staging stye issue
            //Susan Wong, Ticket #18528 [PHASE 1] improve usability end - ADD LOADING SCREEN
            function errorMsgPw() {
                if (!$('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radwinPassword_C_lblErrormessage').children(span).text().length === 0) {
                    $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radwinPassword_C_lblErrormessage').addClass('info-error');
                }
                if (!$('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radwinPassword_C_CompareValidator').children(span).text().length === 0) {
                    $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radwinPassword_C_CompareValidator').addClass('info-error');
                    $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radwinPassword_C_CompareValidator').css('display', 'block');
                }
                if (!$('#baseTemplatePlaceholder_content_Profile__c_lblPasswordsuccess').children(span).text().length === 0) {
                    $('#baseTemplatePlaceholder_content_Profile__c_lblPasswordsuccess').addClass('info-success');
                }
                if (!$('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radwinPassword_C_lblerrorLength').children(span).text().length === 0) {
                    $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radwinPassword_C_lblerrorLength').addClass('info-error');
                }
                $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radwinPassword_C').css('height', 'auto');
            }
            function checkMemStatus() {
                // Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS
                var memStatus = document.getElementById("baseTemplatePlaceholder_content_Profile__c_cmbempstatus");
                if (memStatus.value == 1) {
                    $("#Removethiscompanyrecord").css('display', 'none');
                    $("#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_div5").css('display', 'none');
                    $("#LeaveEmploymentText").css('display', 'none');
                    $(".mem-prac").css('display', 'block');
                }
                else {
                    $("#Removethiscompanyrecord").css('display', 'inline-block');
                    $(".mem-prac").css('display', 'none');
                }
                if ($("#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_lblCurrentCompanyName").text().length == 0) {
                    $("#Removethiscompanyrecord").css('display', 'none');
                    $("#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_lblCurrentCompanyName").append('N/A <p class="info-note">You have no company, you can use the below form to add one</p>');
                }
                // Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS 
            };

            $(document).ready(function () {
                $('.add-company-section').hide();
                if (document.getElementById("baseTemplatePlaceholder_content_Profile__c_lblStatus").innerHTML.indexOf('Student') >= 0)
                {
                    $('.for-student').show();
                    $('.for-member').hide();
                    $('.for-reg-user').hide();
                    $('.for-member-reg-user').hide(); //Susan Wong, #20099
                }
                else if (document.getElementById("baseTemplatePlaceholder_content_Profile__c_lblStatus").innerHTML.indexOf('Non-Member') >= 0)
                {
                    $('.for-student').hide();
                    $('.for-member').hide();
                    $('.for-reg-user').show();
                    $('.for-member-reg-user').show(); //Susan Wong, #20099
                }
                else
                {
                    $('.for-student').hide();
                    $('.for-member').show();
                    $('.for-reg-user').hide();
                    $('.for-member-reg-user').show(); //Susan Wong, #20099
                }
                // Susan Wong, Ticket #18528 [PHASE 1] improve usability start - PENDING CHANGES SECTION
                $('.profile-help-info').css('display', 'none');
                var rowCount = $('#baseTemplatePlaceholder_content_Profile__c_PendingChangesDetails1_rgvPendingchange tr').length - 1;
                if (rowCount == -1) {
                    rowCount = 0;
                    $('.pending-num').html(rowCount);
                }
                else {
                    $('.pending-num').html(rowCount);
                }
                if (rowCount > 4)
                {
                    $('#baseTemplatePlaceholder_content_Profile__c_PendingChangesDetails1_rgvPendingchange tbody').css('overflow-y', 'scroll');
                    $('.profile-help-info').css('display', 'block');
                }
                else {
                    $('#baseTemplatePlaceholder_content_Profile__c_PendingChangesDetails1_rgvPendingchange tbody').css('overflow-y', 'hidden');
                }
                var tableReverse = $('#baseTemplatePlaceholder_content_Profile__c_PendingChangesDetails1_rgvPendingchange tbody');
                $('tr:first', tableReverse).after($('tr', tableReverse).not(':first').get().reverse());

                // Susan Wong, Ticket #18528 [PHASE 1] improve usability end - PENDING CHANGES SECTION
                $('.info-warning').hide();
                //var prefHome = $('#baseTemplatePlaceholder_content_Profile__c_lblPrefHome');
                //var prefBus = $('#baseTemplatePlaceholder_content_Profile__c_lblPrefBusiness');
                errorMsgPw();
            });

            checkMemStatus();

            // Susan Wong, Ticket #18528 [PHASE 2] improve usability start - EDIT COMPANY ADDRESS
            $('#btnEditPrimarybusiness').click(function () {
                $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C').css('height', 'auto');
            });
            $('#btnAddanotherbusiness').click(function () {
                $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C').css('height', 'auto');
            });
            function checkValChange() {
                if ($('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_txtCompany').data("lastval") != $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_txtCompany').val()) { // IF VALUE IS NOT SAME AS BFORE
                    checkValEmpty();
                };
            };
            function checkValEmpty() {
                if ($('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_txtCompany').val() !== "") {
                    $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_btnEditCompSave')
                        .removeClass('cai-btn-red-inverse')
                        .addClass('cai-btn-red');
                }
                else if ($('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_txtCompany').val() == "") {
                    $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_btnEditCompSave')
                        .addClass('cai-btn-red-inverse')
                        .removeClass('cai-btn-red');
                }
            };
            $('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_txtCompany').on('input', function (e) {
                checkValChange();
            });
            // Susan Wong, Ticket #18528 [PHASE 2] improve usability end - EDIT COMPANY ADDRESS
            // Susan, Ticket #19962 - Only require mobile OR landine start
            function CheckPhonePopulated() {
                var SubmitButton = document.getElementById('baseTemplatePlaceholder_content_Profile__c_btnSubmit');
                var MobArea = document.getElementById('baseTemplatePlaceholder_content_Profile__c_txtMobileAreaCode');
                var MobNum = document.getElementById('baseTemplatePlaceholder_content_Profile__c_txtMobileNumber');
                var LandArea = document.getElementById('baseTemplatePlaceholder_content_Profile__c_txtlandlineAreaCode');
                var LandNum = document.getElementById('baseTemplatePlaceholder_content_Profile__c_txtlandlineNumber');
                if (($(MobArea).val() == "" && $(MobNum).val() == "") && ($(LandArea).val() == "" && $(LandNum).val() == "")) {
                    $('.submitBtn.prof-update-btn').addClass('disabled-btn');
                    SubmitButton.disabled = true;
                    $('.error-req-numbers').css('display', 'inline');
                    $('.info-error.errorNums').css('display', 'block');
                }
                else {
                    $('.submitBtn.prof-update-btn').removeClass('disabled-btn');
                    SubmitButton.disabled = false;
                    $('.error-req-numbers').css('display', 'none');
                    $('.info-error.errorNums').css('display', 'none');
                }
            };
            CheckPhonePopulated();
            $('#baseTemplatePlaceholder_content_Profile__c_txtMobileAreaCode').on('input', function (e) {
                CheckPhonePopulated();
            });
            $('#baseTemplatePlaceholder_content_Profile__c_txtMobileNumber').on('input', function (e) {
                CheckPhonePopulated();
            });
            $('#baseTemplatePlaceholder_content_Profile__c_txtlandlineAreaCode').on('input', function (e) {
                CheckPhonePopulated();
            });
            $('#baseTemplatePlaceholder_content_Profile__c_txtlandlineNumber').on('input', function (e) {
                CheckPhonePopulated();
            });
            // Susan, Ticket #19962 - Only require mobile OR landine end
            // Susan Wong, Ticket #18528 [PHASE 1] improve usability start - ERROR MESSAGES
            $('.btn-save-pw').click(function () {
                errorMsgPw();
            });
            $('.btn-open-pw').click(function () {
                errorMsgPw();
            });
            $('.prof-update-btn').click(function () {
                checkErrMsg();
                function printErrMsg(ele) {
                    var errValID = ele;
                    var errValElement = $('#' + errValID);
                    var errText = errValElement.text();
                    if ($('p.' + errValID).is(':visible')) {
                        // DO NOTHING
                    }
                    else {
                        $(".missing-req").append('<p class="info-error ' + errValID + '">' + errText + '</p>');
                    }
                }
                function deleteErrMsg(ele) {
                    var errValID = ele;
                    var errValElement = $('#' + errValID);
                    var errText = errValElement.text();
                    if ($('p.' + errValID).is(':visible')) {
                        $('p.' + errValID).remove();
                    }
                }
                function checkErrMsg() {
                    $('.reqValidators').each(function () {
                        if ($(this).is(':visible')) {
                            var ele = $(this).attr("id");
                            printErrMsg(ele);
                        }
                        else {
                            var ele = $(this).attr("id");
                            deleteErrMsg(ele);
                        }
                    });
                }
            });
            // Susan Wong, Ticket #18528 [PHASE 1] improve usability end - ERROR MESSAGES
            // Susan Wong, Ticket #18991 [PHASE 3] improve usability start - EDIT COMPANY ADDRESS
            $("#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_txtCompany").blur(function() {
                if($('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_reqtxtCompany').css('display') === 'inline')
                {$('#ctl00_ctl00_baseTemplatePlaceholder_content_Profile__c_radChangeCompany_C_reqtxtCompany').css('display', "block");}
            });
            $('#no-result-btn').click(function () {
                $('.add-company-section').show();
                $('.add-existing').hide();
            });
            $('#try-registry').click(function () {
                $('.add-existing').show();
                $('.add-company-section').hide();
                $('.info-error').css("display", "none");
            });
            $('#btnAddanotherbusiness').click(function () {
                $('.leave-employment').hide();
            });
            $('#btnEditPrimarybusiness').click(function () {
                $('.leave-employment').show();
                $('#Removethiscompanyrecord').show();
                checkMemStatus();
            });
            // Susan Wong, Ticket #18991 [PHASE 3] improve usability end - EDIT COMPANY ADDRESS
            //Susan Wong, Ticket #20072 START - REPLACE RADIO BUTTON
            CheckSelectedAddress();
            $('.profile-address-tab input[type="radio"]').each(function () {
                if ($(this).is(":checked")) {
                    $(this).closest('td').addClass("selected-address-tab");
                    $(this).closest('td').siblings().removeClass("selected-address-tab");
                }
            });
            $('.profile-address-tab input[type="radio"]').change(function () {
                if ($(this).is(":checked")) {
                    $(this).closest('td').addClass("selected-address-tab");
                    $(this).closest('td').siblings().removeClass("selected-address-tab");
                }
                CheckSelectedAddress();
            });
            function CheckSelectedAddress() {
                var selectedAddress = document.querySelector('input[name="ctl00$ctl00$baseTemplatePlaceholder$content$Profile__c$rblPAddress"]:checked').value;
                if (selectedAddress == "Work") {
                    $(".home-address-section").css("display", "none");
                    $(".bus-address-section").css("display", "block");
                } else
                    if (selectedAddress == "Home") {
                        $(".home-address-section").css("display", "block");
                        $(".bus-address-section").css("display", "none");
                    }
            }
            // PROFILE START
            $(document).ready(function () {
                var prefHome = $('#baseTemplatePlaceholder_content_Profile__c_lblPrefHome');
                if (prefHome.is(':visible')) {
                    $(".home-address-section .prefered-address > span").addClass("address-icon-on");
                    $(".home-address-section .prefered-address > span").removeClass("address-icon-off");
                } else
                    if (prefHome.is(':hidden')) {
                        $(".home-address-section .prefered-address > span").removeClass("address-icon-on");
                        $(".home-address-section .prefered-address > span").addClass("address-icon-off");
                    }
                var prefBus = $('#baseTemplatePlaceholder_content_Profile__c_lblPrefBusiness');
                if (prefBus.is(':visible')) {
                    $(".bus-address-section .prefered-address > span").addClass("address-icon-on");
                    $(".bus-address-section .prefered-address > span").removeClass("address-icon-off");
                } else
                    if (prefBus.is(':hidden')) {
                        $(".bus-address-section .prefered-address > span").removeClass("address-icon-on");
                        $(".bus-address-section .prefered-address > span").addClass("address-icon-off");
                    }
                $('.default-expanded').addClass("show-it-transition");
                $('.trigger-title .form-title').css("margin-bottom", "1px");
                var prefHome = $('#baseTemplatePlaceholder_content_Profile__c_lblPrefHome');
                var prefBus = $('#baseTemplatePlaceholder_content_Profile__c_lblPrefBusiness');
                if (prefHome.is(':hidden') && prefBus.is(':hidden')) {
                    $(".address-info-section .info-warning").show();
                    $(".home-address-section .prefered-address > span").removeClass("address-icon-on")
                    .addClass("address-icon-off");
                    $(".bus-address-section .prefered-address > span").removeClass("address-icon-on")
                    .addClass("address-icon-off");
                }
            });
            $('.address-icon').on('click', function () {
                if ($(this).hasClass("address-icon-on")) //Currently on to turn off
                {
                    $('.address-icon').addClass("address-icon-on")
                    .removeClass("address-icon-off");
                    $(this).addClass("address-icon-off")
                    .removeClass("address-icon-on");
                } else
                    if ($(this).hasClass("address-icon-off")) //Currently off to turn on
                    {
                        $('.address-icon').addClass("address-icon-off")
                        .removeClass("address-icon-on");
                        $(this).addClass("address-icon-on")
                        .removeClass("address-icon-off");
                    }
            });
            $(".prof-update-btn").on('click', function () {
                if ($('.address-icon-on').parent().parent().parent().parent().hasClass("home-address-section")) {
                    $('input[name="ctl00$ctl00$baseTemplatePlaceholder$content$Profile__c$rblPAddress"][value="Home"]').attr("checked", "checked");
                    $('input[name="ctl00$ctl00$baseTemplatePlaceholder$content$Profile__c$rblPAddress"][value="Work"]').attr("checked", false);
                } else
                    if ($('.address-icon-on').parent().parent().parent().parent().hasClass("bus-address-section")) {
                        $('input[name="ctl00$ctl00$baseTemplatePlaceholder$content$Profile__c$rblPAddress"][value="Work"]').attr("checked", "checked");
                        $('input[name="ctl00$ctl00$baseTemplatePlaceholder$content$Profile__c$rblPAddress"][value="Home"]').attr("checked", false);
                    }
            });
            // PROFILE END
            //Susan Wong, Ticket #20072 END - REPLACE RADIO BUTTON
            //Susan Wong, #20169 START
            $(document).ready(function () {
                document.getElementById("idcselect").disabled = true;
                document.getElementById("idcity").disabled = true;
            });
            var inputFirm = document.getElementById("idcselect");
            var inputCountry = document.getElementById("idcountry");
            var inputCity = document.getElementById("idcity");
            $('#idcountry').change(function () {
                if (inputCountry.selectedIndex == 0) {
                    inputCity.disabled = true;
                    inputFirm.disabled = true;
                }
                else if (inputCity.value == '' && !inputCountry.selectedIndex == 0) {
                    inputCity.disabled = false;
                    inputFirm.disabled = true;
                }
                else {
                    inputCity.disabled = false;
                    inputFirm.disabled = true;
                }
            });

            inputCity.addEventListener("keyup", function (event) {
                if (inputCity.value == '') {
                    inputFirm.disabled = true;
                }
                else if (inputCity.value.length >= 3) {
                    inputFirm.disabled = false;
                }
                else
                { inputFirm.disabled = true; }
            });
            //Susan Wong, #20169 END
        }, delay);
    };

//*Jim code begins :#20057*/
 
    function BindCompanyWithEmpty() {
    	var val = $('#idcountry :selected').text();

    	$("#idcselect").autocomplete({
    		source: function (request, response) {
    			//alert(request.term);
    			$.ajax({
    				url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetCompany") %>',
    			    data: JSON.stringify({
    			        country : val
    			    }),
    				dataType: "json",
    				type: "POST",
    				contentType: "application/json; charset=utf-8",
    				dataFilter: function (data) { return data; },
    				success: function (data) {
    					response($.map(data.d, function (item) {
    						return {
    							value: item.Id, label: item.Name
    						}
    					}
						))

    				},
    				error: function (XMLHttpRequest, textStatus, errorThrown) {
    					alert("error" + errorThrown);
    				}
    			});
    		},
    		minLength: 3 ,
    		select: function (event, ui) {
    			$(this).val(ui.item.Name);
    		}
    		
    	});
    	
    }



	  function BindCompanyWithValue() {
    	var val = $('#idcountry :selected').text();


    	$("#idcselect").autocomplete({
    		source: function (request, response) {
    			$.ajax({
    				url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetCountryCompany") %>',
    			    data: JSON.stringify({
    			        country : $('#idcountry :selected').text(),
    			        city : $("#idcity").val().trim(),
    			        name : request.term 
    			    }),
    				dataType: "json",
    				type: "POST",
    				contentType: "application/json; charset=utf-8",
    				dataFilter: function (data) { return data; },
    				success: function (data) {
    					response($.map(data.d, function (item) {
    						return {
    							label: item.Name,
								value:item.Id
    						}
    					}
						))

    				},
    				error: function (XMLHttpRequest, textStatus, errorThrown) {
    					alert("error" + errorThrown);
    				}
    			});
    		},
    		minLength: 3 ,   
    		focus: function (event, ui) {

    			$("#idcselect").val(ui.item.label);
    			return false;
    		},

    		select: function (event, ui) {

    			

    			$("#idcselect").val(ui.item.label);
    			$("#hfCompanyID").val(ui.item.value);    			
    			return false;
    		},
    		response: function (event, ui) {
    			var len = ui.content.length;
    			$('#companycount').html('Found :' + len + ' results');
    		}

    	});
    }






	
	
    GetDropDownData();
    function GetDropDownData() {

        $.ajax({
            type: "POST",
            url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetCountryDetails") %>',
            data: "{}",
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                $.each(data.d, function (data,value) {
                	$("#idcountry").append($("<option></option>").val(value.CountryId).html(value.CountryName));
                })
            }

        });

    }


	  function BindCity() {
	  	var val = $('#idcountry :selected').text();
    	$("#idcity").autocomplete({
    		source: function (request, response) {
    			$.ajax({
    				url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetCountyCityByCountry") %>',
    			    data: JSON.stringify({
    			        country : $('#idcountry :selected').text(),
    			        city : request.term
    			    }),
    				dataType: "json",
    				type: "POST",
    				contentType: "application/json; charset=utf-8",
    				dataFilter: function (data) { return data; },
    				success: function (data) {
    					response($.map(data.d, function (item) {
    						return {
    							label: item.Name,
								//value:item.Id
    						}
    					}
						))

    				},
    				error: function (XMLHttpRequest, textStatus, errorThrown) {
    					alert("error" + errorThrown);
    				}
    			});
    		},
    		minLength: 1  ,  
    		response: function (event, ui) {
    			var len = ui.content.length;
    			$('#citycount').html('Found :' + len + ' results');
    		}
    	});
    }



	$('#idcountry').change(function () {
		$("#citycount").empty();
		$("#companycount").empty();
		$("#idcselect").val('');
		$("#idcity").val('');
		$('#countrycount').html('Found: 1 result');
		if ($(this).val() === "") {

			$("#idcselect").prop('disabled', true);
		}
		else { $("#idcselect").prop('disabled', false); }
		

		BindCompanyWithEmpty();
		BindCity()
		
	});
		
	$('#idcselect').on('input', function () {
	//	//BindCompanyWithValue()
		if ($("#idcselect").val().length > 2) {
			//		//loaded = false;
			//		//BindCompanyWithEmpty();
			//		alert($("#idcselect").val().length);
			BindCompanyWithValue();

		}
		else { $("#companycount").empty(); }
	});

	// change event for city text box
	$('#idcity').on('input', function () {

		$("#idcselect").val('');
		$("#companycount").empty();

		if ($("#idcity").val().length < 1) {

			$("#citycount").empty();

		}

		if ($("#idcountry").val().length > 0) {
			BindCity()
		}
		else
		{
			
		}
	});

	//  End of change event for city text box


	function OnClientCloseReset()
	{
		$("#count").empty();
		$("#idcselect").val('');
		$("#idcity").val('');
		$('#idcountry').prop('selectedIndex', 0);
		$("#citycount").empty();
		$("#companycount").empty();
		$('#countrycount').empty();
	}

	//*Jim code ends :#20057*/
	
	
</script>

