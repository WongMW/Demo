<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalControl.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.EAssessment.ModalControl" %>
<%@ Register TagPrefix="modal" TagName="Content" Src="~/UserControls/CAI_Custom_Controls/EAssessment/ModalContent.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<telerik:RadWindow runat="server" ID="RadWindow1"
    Title="Please take a moment to update your details" AutoSize="True" DestroyOnClose="True"
    CenterIfModal="True" Modal="True" VisibleOnPageLoad="False" MinWidth="800px"
    Behaviors="None" CssClass="flex-modal">
    <ContentTemplate>
        <modal:Content runat="server" ID="modalContent" />
    </ContentTemplate>
</telerik:RadWindow>

<cc1:User runat="server" ID="User1" />