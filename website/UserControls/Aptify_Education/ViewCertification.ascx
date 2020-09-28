<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ViewCertification.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.ViewCertificationControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix">
    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
    <table runat="server" id="TABLE1" onclick="return TABLE1_onclick()" class="data-form"  >
        <tr>
            <td colspan="2">
                <asp:Label runat="server" ID="lblTitle" Font-Size="16pt" />
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                Certificate #
            </td>
            <td>
                <asp:Label runat="server" ID="lblID" /> 
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                Certificant
            </td>
            <td>
                <asp:Label runat="server" ID="lblCertificant" /> 
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                Certification Type 
            </td>
            <td>
                <asp:Label runat="server" ID="lblType" /><br />
                <asp:HyperLink runat="server" ID="lnkType"><asp:Label runat="server" ID="lblTypeDetails" /></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                Granted On
            </td>
            <td>
                <asp:Label runat="server" ID="lblDateGranted" /> 
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                Expires On 
            </td>
            <td>
                <asp:Label runat="server" ID="lblDateExpires" /> 
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                Status
            </td>
            <td>
                <asp:Label runat="server" ID="lblStatus" /> 
            </td>
        </tr>
    </table>
	<cc1:User runat="server" ID="User1" />
</div>
