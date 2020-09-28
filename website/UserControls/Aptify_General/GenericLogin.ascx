<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GenericLogin.ascx.vb"
    Inherits="UserControls_Aptify_General_GenericLogin" %>
<%@ Register Src="~/UserControls/Aptify_General/LoginSF4.ascx" TagName="LoginSF4"
    TagPrefix="uc2" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div style="float: left;">
    <table width="100%">
        <tr>
            <td style="width: 100%;">
                <asp:Label ID="lblstat" runat="server" Text="Currently You are not logged in! Please Login Now."></asp:Label>
            </td>
            <td style="width: 33%;">
            </td>
            <td style="width: 33%;">
                &nbsp;
            </td>
        </tr>
    </table>
</div>
<div>
    <table width="100%">
        <tr>
            <td style="width: 33%;">
                &nbsp;
            </td>
            <td style="width: 33%;">
            </td>
            <td style="width: 33%;">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 33%;">
                &nbsp;
            </td>
            <td style="width: 33%;">
                <uc2:LoginSF4 ID="LoginSF4" runat="server" />
            </td>
            <td style="width: 33%;">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 40%;">
                &nbsp;
            </td>
        </tr>
    </table>
    <cc2:User runat="Server" ID="User1" />
</div>
