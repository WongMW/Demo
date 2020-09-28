<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/ViewCertification.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.ViewCertificationControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix">
    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
    <div runat="server" id="TABLE1" onclick="return TABLE1_onclick()" class="cai-form">
        <asp:Label runat="server" ID="lblTitle" CssClass="form-title" />

        <div class="cai-form-content">
            <div class="field-group">
                <label class="label-title-inline">Certificate #:</label>
                <asp:Label runat="server" ID="lblID" />
            </div>

            <div class="field-group">
                <label class="label-title-inline">Certificant:</label>
                <asp:Label runat="server" ID="lblCertificant" />
            </div>

            <div class="field-group">
                <label class="label-title-inline">Certification Type:</label>
                <asp:Label runat="server" ID="lblType" />
                <asp:HyperLink runat="server" ID="lnkType">
                    <asp:Label runat="server" ID="lblTypeDetails" />
                </asp:HyperLink>
            </div>

            <div class="field-group">
                <label class="label-title-inline">Granted On:</label>
                <asp:Label runat="server" ID="lblDateGranted" />
            </div>

            <div class="field-group">
                <label class="label-title-inline">Expires On:</label>
                <asp:Label runat="server" ID="lblDateExpires" />
            </div>

            <div class="field-group">
                <label class="label-title-inline">Status:</label>
                <asp:Label runat="server" ID="lblStatus" />
            </div>
        </div>
    </div>
    <cc1:User runat="server" ID="User1" />
</div>
