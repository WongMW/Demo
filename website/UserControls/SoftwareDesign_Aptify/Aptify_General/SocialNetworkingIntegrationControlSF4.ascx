<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/SocialNetworkingIntegrationControlSF4.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.SocialNetworkingIntegrationControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register src="Sitefinity4xSSO.ascx" tagname="Sitefinity4xSSO" tagprefix="uc1" %>

<script>
  window.fbAsyncInit = function() {
    FB.init({
        appId: 'ae446c7f667124e901d2cb504c4232a8',
        xfbml      : true,
        version    : 'v2.5'
    });
  };

  (function(d, s, id){
     var js, fjs = d.getElementsByTagName(s)[0];
     if (d.getElementById(id)) {return;}
     js = d.createElement(s); js.id = id;
     js.src = "//connect.facebook.net/en_US/sdk.js";
     fjs.parentNode.insertBefore(js, fjs);
   }(document, 'script', 'facebook-jssdk'));
</script>


    <div id ="tblSocialNetwork" runat ="server">
        <a id="lnkLinkedIn" class="linkedin-btn" runat="server" >
                    Sign in with LinkedIn
                    <img style="display: none" alt="Click here to login to the site using your LinkedIn credentials." id="imgSocialNetwork"  src="#"   runat="server" /></a>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        </div>
    <cc1:AptifyWebUserLogin id="WebUserLogin1" runat="server" Visible="False" 
    Height="94px" Width="235px"></cc1:AptifyWebUserLogin>


<uc1:Sitefinity4xSSO ID="Sitefinity4xSSO1" runat="server" />



