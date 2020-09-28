<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/LoginSF4.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.WebLogin" %>
<%@ Register Src="Sitefinity4xSSO.ascx" TagName="Sitefinity4xSSO" TagPrefix="uc2" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="uc1" TagName="SocialNetworkingIntegrationControlSF4" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/SocialNetworkingIntegrationControlSF4.ascx" %>
<META Http-Equiv="Cache-Control" Content="no-cache">
<META Http-Equiv="Pragma" Content="no-cache">
<META Http-Equiv="Expires" Content="0">

<style type="text/css">
    .tooltip{
        display: inline;
        position: relative;

    }

    .tooltip:hover:after{
    background: #003D51;
    background: rgba(0,61,81,.9);
    border-radius: 5px;
    bottom: 26px;
    color: #fff;
    content: attr(title);
    left: 20%;
    padding: 5px 15px;
    position: absolute;
    z-index: 98;
    width: 220px;
    font-size: 14px;
}

    .tooltip:hover:before{
    border: solid;
    border-color: #003D51 transparent;
    border-width: 6px 6px 0 6px;
    bottom: 20px;
    content: "";
    left: 50%;
    position: absolute;
    z-index: 99;
}

    .qtext {
        font-size: 13px;
        font-weight: lighter;
    }
</style>


<asp:Panel ID="pnllogin" runat="server" DefaultButton="cmdLogin">
    <div id="loginTop" class="news-list" runat="server">
        <%-- Nalini 12429 13/12/2011--%>
        <div class="ICELoginHeader">
            <div class="main-title">Login to Your Account</div>
        </div>
        <div class="content-container-Home clearfix" style="width:100%">
            <asp:Literal runat="server" ID="litLoginLabel" />
            <div id="tblLogin" runat="server">
                    <div class="login-form">                     
                        <asp:Label ID="lblError" ForeColor="red" runat="server"></asp:Label>
                        <div id="tblData">
                                <div class="LeftColumnEditProfile">
                                    <%--  Dilip issue 12717  --%>
                                    <asp:Label ID="lblUserID" runat="server">Username<a href="#" title="Use your student or member number as your User ID. If you are not a student or member your email address is your User ID." class="tooltip"><span title="Tooltip" class="qtext"> What's this?</span></a></asp:Label>&nbsp;
                                </div>
                                <%--   <td valign="top">--%>
                                <div>
                                    <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                                </div>
                                <div class="LeftColumnEditProfile" >
                                    <asp:Label ID="lblPassword" runat="server">Password</asp:Label>&nbsp;<div id="capsLockWarning" style="float: right; font-weight: normal; color: red; display: none;">Caps Lock is on</div>
                                </div>
                                <%--   <td valign="top">--%>
                                <div style="padding-top: 2px;">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                           
                                <div class="tablecontrolsfontLogin">
                                    <asp:UpdatePanel ID="pnl1" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl" runat="server" Visible="false"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkAutoLogin" EventName="" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>                                   
                                <div class="remember-me-wrapper">
                                    <asp:CheckBox ID="chkAutoLogin" CssClass="" runat="server" ToolTip="Check this box if you would like the site to automatically log you in next time you visit."
                                    Text=" Remember Me" AutoPostBack="true"></asp:CheckBox>                                  
                                    </div>
                                <div class="forget-password">
                                     <span>
                                        <asp:HyperLink ID="hlinkForgotUID" runat="server"><span class="bold">Forgot Username</span> or <span class="bold">Password</span></asp:HyperLink>
                                     </span>
                                </div>
                                <div>
                                    <%--  Dilip issue 12717--%>
                                    <asp:Button ID="cmdLogin" runat="server"  CssClass="login-btn" Text="Login"></asp:Button>
                                </div>                                                          
                                <div class="seperator" style="display:none;">
                                    <div class="separator-text">
                                    OR
                                    </div>
                                </div>
                        </div>
                        <div style="display:none;">                          
                            <div class="tablecontrolsfontLogin">
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </div>
                            <div>
                                <uc1:SocialNetworkingIntegrationControlSF4 ID="SocialNetworkingIntegrationControlSF4"
                                runat="server" />
                            </div>                       
                            <div>
                                <asp:LinkButton ID="cmdNewUser" Style="display:none;" runat="server" Text="New User Signup!" CssClass="ButtonLink"
                                SkinID="Test"></asp:LinkButton>
                            </div>               
                                <cc1:AptifyWebUserLogin ID="WebUserLogin1" runat="server"
                                Visible="False"></cc1:AptifyWebUserLogin>
                        </div>
                    </div>
            </div>
            <div id="tblWelcome" runat="server" style="width: 100%" class="logged-in-message">
                <%--Nalini Issue 12734--%>
                <div>
                    <asp:Label ID="lblWelcome" runat="server">Welcome,</asp:Label>                       
                </div>
                <div class="logout">
                    <asp:Button ID="cmdLogOut" runat="server" CausesValidation="False" CssClass="logout-btn " Text="Logout" />
                </div>
            </div>
            <cc2:AptifyShoppingCart ID="ShoppingCartLogin" runat="server"></cc2:AptifyShoppingCart>
        </div>
    </div>
</asp:Panel>
<uc2:Sitefinity4xSSO ID="Sitefinity4xSSO1" runat="server" />
 
<asp:HiddenField ID="clientOffSet" runat="server" />
<input type="hidden" runat="server" id="hdOffset" name="hdOffset" />
<asp:Image ID="img" style="visibility:hidden;" runat="server" ImageUrl="~/Images/ICE Login Icon.png" />
<%--<cc1:User ID="User1" runat="server" />--%>



<script language="javascript">
  function isCapsLockOn(e) {
    var keyCode = e.keyCode ? e.keyCode : e.which;
    var shiftKey = e.shiftKey ? e.shiftKey : ((keyCode == 16) ? true : false);
    return (((keyCode >= 65 && keyCode <= 90) && !shiftKey) || ((keyCode >= 97 && keyCode <= 122) && shiftKey))
  }
  $(document).ready(function() {
    $("#baseTemplatePlaceholder_content_C025_LoginSF4_txtPassword").keypress(function(e) {
      if (isCapsLockOn(e))
        $("#capsLockWarning").show();
      else
        $("#capsLockWarning").hide();
    });                           
  });

window.onload = function() {
  document.getElementById("baseTemplatePlaceholder_content_C025_LoginSF4_txtUserID").focus();
};
</script>

