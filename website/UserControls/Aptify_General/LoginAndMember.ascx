<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LoginAndMember.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LoginAndMember" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Src="~/UserControls/Aptify_General/BecomeMemberControl.ascx" TagName="BecomeMemberControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Aptify_General/LoginSF4.ascx" TagName="LoginSF4"
    TagPrefix="uc2" %>
<table width="100%">
    <tr id="trLogin" runat="server" >
        <td style="padding-bottom :10px">
            <uc2:LoginSF4 ID="LoginSF4" runat="server" />
        </td>
    </tr>
   
    <tr id="trEvents" runat="server">
        <td style="padding-bottom :10px">
            <uc1:BecomeMemberControl ID="BecomeMemberControl" runat="server" />
        </td>
    </tr>
</table>
<cc1:User ID="User1" runat="server" />
