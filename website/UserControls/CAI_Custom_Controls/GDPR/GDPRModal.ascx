<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GDPRModal.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.GdprModal" %>
<%@ Register TagPrefix="gdpr" TagName="GDPRWizard" Src="~/UserControls/CAI_Custom_Controls/GDPR/GDPRWizard.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<telerik:RadWindow runat="server" ID="RadWindow1"
    AutoSize="True" DestroyOnClose="True"
    CenterIfModal="True" Modal="True" VisibleOnPageLoad="False" MinWidth="800px"
    Behaviors="Close" CssClass="flex-modal">
    <ContentTemplate>
        <gdpr:GDPRWizard runat="server" ID="GDPRWizard1" />
    </ContentTemplate>
</telerik:RadWindow>

<cc1:User runat="server" ID="User1" />
