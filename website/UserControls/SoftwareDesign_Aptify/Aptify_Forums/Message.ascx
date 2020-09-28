<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Forums/Message.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.Message" %>

<%@ Register Src="../Aptify_General/RecordAttachments.ascx" TagName="RecordAttachments" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%-- Nalini 12458--%>
<div class="content-container clearfix">
    <div id="tblMain" runat="server" class="data-form content-padding-top">
        <div class="field-group">
            <asp:Label ID="Label3" runat="server"><b>Subject:</b></asp:Label>
            <asp:Label ID="lblSubject" runat="server"></asp:Label>
        </div>

        <div class="field-group">
            <asp:Label ID="Label1" runat="server"><b>Sent:</b></asp:Label>
            <asp:Label ID="lblSent" runat="server"></asp:Label>
        </div>

        <div class="field-group">
            <asp:Label ID="Label4" runat="server"><b>From:</b></asp:Label>
            <asp:Label ID="lblFrom" runat="server"></asp:Label>
        </div>

        <div class="field-group">
            <asp:Label ID="Label2" runat="server"><b>Attachments:</b></asp:Label>
            <asp:Label ID="lblAttachments" runat="server"></asp:Label>
            <asp:Button ID="btnAttachments" runat="server" CssClass="submitBtn" Text="View/Add" Visible="false" />
        </div>

        <div class="field-group">
            <asp:Label ID="lblBody" runat="server"></asp:Label>
            <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
        </div>

        <div class="field-group" runat="server" id="trAttachments" visible="false">
            <uc1:RecordAttachments ID="RecordAttachments" runat="server" AllowView="true" />
        </div>
    </div>
    <cc2:User runat="server" ID="User1" />
</div>
