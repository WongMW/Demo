<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Customer_Service/ForgotUID.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ForgotUIDControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>

<div class="content-container clearfix forgot-password-wrapper">
    <div class="forget-password-content">
        <h2 class="main-title">Reset Password</h2>
        <div class="reset-message">
            <p>
                To reset your password for
                <asp:HyperLink ID="HyperLink1" runat="server">chartereredaccountants.ie</asp:HyperLink>, enter your email
                adddress and we'll send you an email with instructions 
            </p>
        </div>
        <asp:Label ID="lblError" CssClass="error-message" runat="server"></asp:Label>
        <div class="reset-form">
            <div id="tblMain" runat="server" class="data-form">
                <div id="tblUserName" runat="server" visible="false">
                    <%--Neha issue Id:15163 Add CssClass for ErrorMessage--%>

                    <div class="field-group">
                        <asp:Label ID="Label1" runat="server" CssClass="required">User ID:</asp:Label>
                        <asp:RequiredFieldValidator ID="valUID" runat="server" CssClass="error-message" ControlToValidate="txtUID" ErrorMessage=" Required Field" />
                    </div>
                    <asp:TextBox ID="txtUID" runat="server" CssClass="" />
                </div>
            </div>
        </div>
        <div id="tblPWDHint" runat="server" visible="false">
            <div class="field-group">
                <asp:Label runat="server">Contact
                <asp:HyperLink ID="mailAddress" runat="server"></asp:HyperLink>
                    for assistance.&nbsp;
                </asp:Label>
            </div>
            <div class="field-group LeftColumnEditProfile">
                <asp:Label ID="Label2" runat="server">Password Hint:</asp:Label>
                <asp:Label ID="lblHint" runat="server"></asp:Label>
            </div>

            <div class="field-group">
                <asp:Label ID="Label3" runat="server" CssClass="required">Answer:</asp:Label>
                <asp:RequiredFieldValidator ID="valAnswer" runat="server" ControlToValidate="txtAnswer" CssClass="error-message" ErrorMessage=" Required Field"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
            </div>
        </div>
        <div id="tblPasswordChange" runat="server" visible="false">
            <div class="field-group LeftColumnEditProfile">
                <asp:Label ID="Label6" runat="server">User ID:</asp:Label>
                <asp:Label ID="lblUID" runat="server"></asp:Label>
            </div>

            <div class="field-group">
                <asp:Label ID="Label4" runat="server" CssClass="required">Password:</asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPWD" CssClass="error-message" ErrorMessage=" Required Field"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox>
            </div>

            <div class="field-group">
                <asp:Label ID="Label5" runat="server" CssClass="required">Repeat Password:</asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRepeatPWD" CssClass="error-message" ErrorMessage=" Required Field"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtRepeatPWD" runat="server" TextMode="Password"></asp:TextBox>
                <p></p>
                <cc2:WebUserActivity ID="WebUserActivity1" runat="server" />
                <cc1:AptifyWebUserLogin ID="WebUserLogin1" runat="server" />
            </div>
        </div>
        <div class="reset-actions">
            <asp:Button ID="cmdSubmit" runat="server" Text="Reset Password" CssClass="btn reset-btn" />
        </div>
    </div>
</div>



<asp:Panel runat="server" Visible="true">
</asp:Panel>

<script type="text/javascript">
    jQuery(document).ready(function ($) {
        if ($("#<%= lblError.ClientID()%>").is(":visible")) {
            $('.reset-actions').hide();
            $('.reset-message').hide();
        }
    });
</script>