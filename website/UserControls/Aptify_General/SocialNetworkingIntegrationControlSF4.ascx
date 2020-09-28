<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SocialNetworkingIntegrationControlSF4.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.SocialNetworkingIntegrationControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register src="Sitefinity4xSSO.ascx" tagname="Sitefinity4xSSO" tagprefix="uc1" %>
<script src="http://static.ak.connect.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php" type="text/javascript">
    FB.init("ae446c7f667124e901d2cb504c4232a8", "xd_receiver.htm");
</script>
 <div>
    <table id ="tblSocialNetwork" cellpadding ="3" cellspacing ="3" border ="0" runat ="server">
        <tr>    
            <td>      
                <a id="lnkLinkedIn" runat="server" ><img alt="Click here to login to the site using your LinkedIn credentials." id="imgSocialNetwork"  src="#"   runat="server" /></a>
            </td>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        </table>
</div>
    <cc1:AptifyWebUserLogin id="WebUserLogin1" runat="server" Visible="False" 
    Height="94px" Width="235px"></cc1:AptifyWebUserLogin>


<uc1:Sitefinity4xSSO ID="Sitefinity4xSSO1" runat="server" />



