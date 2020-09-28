<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/BecomeMemberControl.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.BecomeMemberControl" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
</style>

<table >
    <tr>
        <td rowspan="2" style="padding-right:10px;">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Globe.png" />
            </td>
        <td style="font-size:16px;">
        <asp:Label ID="lblMemberMessage" runat="server" Text="Become an ICE Member"></asp:Label>

            </td>
    </tr>
    <tr>
        <td align="center" >
        <asp:Button ID="btnJoin" runat="server" Text="Join Us Now!" CssClass="submitBtn" />
            </td>
    </tr>
</table>

