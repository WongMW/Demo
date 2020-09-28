<%@ Control Language="vb" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Forums/CreateMessage.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.CreateMessage" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>

<div class="content-padding-top field-group">
    <h2>New Post</h2>

    <span class="label-title">Subject:</span>
    <asp:TextBox ID="txtSubject" runat="server" Width="99%" CssClass="txtfontfamily"></asp:TextBox>

    <span class="label-title">Message:</span>
    <asp:TextBox ID="txtBody" MultiLine="True" Wrap="true"
        runat="server" Width="99%" Height="200px" TextMode="MultiLine" CssClass="txtfontfamily txtRestrictResize" />

    <div class="actions">
        <asp:Button runat="server" ID="cmdSave" Text="Save" CssClass="submitBtn" />
        <asp:Button runat="server" ID="cmdCancel" Text="Cancel" CssClass="submitBtn" />
    </div>
</div>
<cc2:User runat="server" ID="User1" />
