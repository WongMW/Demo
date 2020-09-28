<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/MemberCEUSubmission.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MemberCEUSubmission" %>
<%@ Register Src="../Aptify_General/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc2" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../Aptify_General/RecordAttachments.ascx" TagName="RecordAttachments"
    TagPrefix="uc1" %>
<%@ Register Assembly="AptifyEBusinessUser" Namespace="Aptify.Framework.Web.eBusiness"
    TagPrefix="cc1" %>
<div class="clearfix topPaddingSet">
    <div class="cai-form ceu-page clearfix">
        <div id="tblMain" runat="server" class="data-form">
            <span class="form-title">Submit New CEU Record</span>

            <div class="field-group">
                <span class="label-title">Type:</span>
                <span>CEU Submitted Via Member Portal</span>
            </div>

            <div class="field-group">
                <span class="label-title">Member:</span>
                <asp:Label ID="txtMember" runat="server" Enabled="False"></asp:Label>
                <span class="Error">
                    <asp:Label ID="lblErrorMember" runat="server">Error</asp:Label>
                </span>
            </div>

            <div class="field-group">
                <span class="label-title">Date Started:</span>
                <telerik:RadDatePicker ID="dtpDateStarted" CssClass="datePickerCEU" runat="server"></telerik:RadDatePicker>
            </div>

            <div class="field-group">
                <span class="label-title">Date Granted:</span>
                <telerik:RadDatePicker ID="dtpDateGranted" CssClass="datePickerCEU" runat="server"></telerik:RadDatePicker>
            </div>

            <div class="field-group">
                <span class="label-title">Units Earned:</span>
                <asp:TextBox ID="txtUnitsEarned" runat="server" CssClass="txtCUESubmission"></asp:TextBox>
                <span class="Error">
                    <asp:Label ID="lblErrorUnitsEarned" runat="server">Error</asp:Label></span>
            </div>

            <div class="field-group">
                <span class="label-title">Title:</span>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="txtCUESubmission"></asp:TextBox>
                <span class="Error">
                    <asp:Label ID="lblErrorTitle" runat="server">Error</asp:Label></span>
            </div>

            <div class="field-group">
                <span class="label-title">CEU Type:</span>
                <asp:DropDownList ID="drpTitle" runat="server" DataTextField="Name" CssClass="txtCUESubmission"
                    DataValueField="ID">
                </asp:DropDownList>
                <span class="Error">
                    <asp:Label ID="lblErrorCEUType" runat="server">Error</asp:Label>
                </span>
            </div>

            <div class="field-group">
                <span class="label-title">Status:</span>
                <asp:Label ID="lblStatus" runat="server" Text="Declared"></asp:Label>
                <span class="Error">
                    <asp:Label ID="lblErrorStatus" runat="server">Error</asp:Label>
                </span>
            </div>

            <div class="field-group">
                <span class="label-title">Expiration Date:
                </span>
                <telerik:RadDatePicker ID="dtpExpirationDate" CssClass="datePickerCEU" runat="server">
                </telerik:RadDatePicker>
            </div>

            <div class="field-group">
                <span class="label-title">Document:</span>
                <telerik:RadAsyncUpload ID="radCEUDocumentUpload" ViewStateMode="Enabled" Localization-Select="Browse..." runat="server"
                    Localization-Remove="Remove" MaxFileInputsCount="1" CssClass="radFileUploadCEUSubmission">
                </telerik:RadAsyncUpload>
                <span class="Error">
                    <asp:Label ID="lblErrorFile" runat="server">Error</asp:Label>
                </span>
            </div>

            <div class="field-group actions">
                <asp:Button runat="server" ID="inptSubmit" name="tstButton" Text="Submit" CssClass="submitBtn" />
                <asp:Button ID="lnkGoBack" runat="server" name="" CssClass="submitBtn" text="Return to Member Certifications" />
            </div>

            <div class="field-group">
                <span class="Error">
                    <asp:Label ID="lblErrorSubmit" runat="server" Visible="false">Error</asp:Label></span>
                <asp:Label ID="lblSubmitSuccess" runat="server" Visible="False">Success</asp:Label>
            </div>
        </div>
    </div>
    <cc1:User ID="User1" runat="server" />
</div>
