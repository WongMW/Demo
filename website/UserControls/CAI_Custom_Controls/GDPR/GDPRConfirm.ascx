<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GDPRConfirm.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.GDPRConfirm" %>
<div class="fixed-btn-position-outter">
    <div class="gdpr-user-pref fixed-btn-position">
        <div class="field-group italic">
            <i><asp:Label ID="lblMessage" runat="server" Text="Are these details correct?"></asp:Label></i>
        </div>
        <div class="gdpr field-group">
            <asp:Button runat="server" ID="btnConfirm" Text="Confirm" OnClick="btnConfirm_Click" CssClass="cai-btn cai-btn-red" />
        </div>
        <%--<div class="gdpr field-group">
            <asp:Button runat="server" ID="btnUpdateLater" Text="Update later" OnClick="btnUpdateLater_Click" CssClass="cai-btn cai-btn-navy-inverse btn-line-break" />
        </div>--%>
        <div class="gdpr field-group">
            <asp:Button runat="server" ID="btnUpdateNow" Text="Update now" OnClick="btnUpdateNow_Click" CssClass="cai-btn cai-btn-navy btn-line-break" />
        </div>
    </div>
</div>
