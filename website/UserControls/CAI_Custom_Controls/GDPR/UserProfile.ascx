<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.UserProfile" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div style="vertical-align: text-top;">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</div>
<div class="user-profile">
    <p style="margin-top: 20px" id="txtUserProfileText" runat="server"></p>
<div class="gdpr form-section-half-border">
    <div class="field-group">
        <asp:Label runat="server" AssociatedControlID="FullName" CssClass="label-title">Full name</asp:Label>
        <asp:Label ID="FullName" runat="server"></asp:Label>
    </div>

    <div class="field-group" style="display:none">
        <asp:Label runat="server" AssociatedControlID="DOB" CssClass="label-title">Date of birth</asp:Label>
        <asp:Label ID="DOB" runat="server"></asp:Label>
    </div>

    <div class="field-group">
        <asp:Label runat="server" AssociatedControlID="HomeAddress" CssClass="label-title">Home address</asp:Label>
        <asp:Label ID="HomeAddress" runat="server" CssClass="comma-space"></asp:Label>
    </div>
</div>
<div class="gdpr form-section-half-border">
    <div class="field-group" style="display: none">
        <asp:Label runat="server" AssociatedControlID="EmploymentStatus" CssClass="label-title">Employment Status</asp:Label>
        <asp:Label ID="EmploymentStatus" runat="server"></asp:Label>
    </div>

    <div class="field-group">
        <asp:Label runat="server" AssociatedControlID="JobFunction" CssClass="label-title">Job function</asp:Label>
        <asp:Label ID="JobFunction" runat="server"></asp:Label>
    </div>

    <div class="field-group">
        <asp:Label runat="server" AssociatedControlID="JobTitle" CssClass="label-title">Job title</asp:Label>
        <asp:Label ID="JobTitle" runat="server"></asp:Label>
    </div>

    <div class="field-group">
        <asp:Label runat="server" AssociatedControlID="FirmAddress" CssClass="label-title">Firm details</asp:Label>
        <asp:Label ID="FirmAddress" runat="server" CssClass="comma-space"></asp:Label>
    </div>
</div>
</div>
<cc1:User runat="server" ID="User1" />
<script>
    function pageLoad() {
        var delay = 10;
        setTimeout(function () {
                $('.comma-space').each(function () {
                    var string = $(this).html();
                    $(this).html(string.replace(/,/g, ', '));
                });
        }, delay);
    };
    
</script>
