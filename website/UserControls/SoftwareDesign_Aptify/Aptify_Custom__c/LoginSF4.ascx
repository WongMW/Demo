<%@ Register Src="Sitefinity4xSSO.ascx" TagName="Sitefinity4xSSO" TagPrefix="uc2" %>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/LoginSF4.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.WebLogin" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="uc1" TagName="SocialNetworkingIntegrationControlSF4" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/SocialNetworkingIntegrationControlSF4.ascx" %>
<META Http-Equiv="Cache-Control" Content="no-cache">
<META Http-Equiv="Pragma" Content="no-cache">
<META Http-Equiv="Expires" Content="0">
<script type = "text/javascript">
    window.onload = function SetSession() {        
//        var timeZoneName = jstz.determine_timezone()
//        var tz = document.getElementById("<%=clientOffSet.clientId %>");
//        tz.value = timeZoneName;
    }
    </script>


<asp:Panel ID="pnllogin" runat="server" DefaultButton="cmdLogin">
    <div id="loginTop" class="news-list" runat="server">
        <%-- Nalini 12429 13/12/2011--%>
        <table style="width: 100%">
            <tr>
                <td>
               
                 <asp:HiddenField ID="clientOffSet" runat="server" />
                 <input type="hidden" runat="server" id="hdOffset" name="hdOffset" />
                    <asp:Image ID="img" runat="server" ImageUrl="~/Images/ICE Login Icon.png" />
                </td>
                <td class="ICELoginHeader">
                    ICE Login
                </td>
                <td style="text-align: right">
                    <asp:CheckBox ID="chkAutoLogin" runat="server" ToolTip="Check this box if you would like the site to automatically log you in next time you visit."
                        Text=" Keep me signed in" AutoPostBack="true"></asp:CheckBox>
                </td>
            </tr>
        </table>
        <div class="content-container-Home clearfix" style="width:100%">
            <asp:Literal runat="server" ID="litLoginLabel" />
            <table id="tblLogin" cellspacing="0" cellpadding="0" border="0" runat="server">
                <tr>
                    <td valign="top" class="style1">
                     
                     
                        <asp:Label ID="lblError" ForeColor="Crimson" runat="server"></asp:Label>
                        <table id="tblData" border="0" runat="server" cellspacing="3" cellpadding="3">
                            <tr >
                                <td align="right" valign="top" class="tablecontrolsfontLogin" style="text-align: left;
                                    padding-left: 10px;">
                                    <%--  Dilip issue 12717  --%>
                                    <asp:Label ID="lblUserID" runat="server">Username:</asp:Label>&nbsp;
                                </td>
                                <%--   <td valign="top">--%>
                                <td>
                                    <asp:TextBox ID="txtUserID" runat="server" Width="175px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="tablecontrolsfontLogin" style="text-align: left; padding-left: 10px;
                                    padding-top: 5px;">
                                    <asp:Label ID="lblPassword" runat="server">Password:</asp:Label>&nbsp;
                                </td>
                                <%--   <td valign="top">--%>
                                <td style="padding-top: 2px;">
                                    <asp:TextBox ID="txtPassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="display:none">
                                <td>
                                    &nbsp;
                                </td>
                                <td class="tablecontrolsfontLogin">
                                    <asp:UpdatePanel ID="pnl1" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl" runat="server" Visible="false"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkAutoLogin" EventName="" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr >
                                <td>
                                    &nbsp;
                                </td>
                                <td style="padding-top:10px">
                                    <%--  Dilip issue 12717--%>
                                    <asp:Button ID="cmdLogin" runat="server"  CssClass="submitBtn" Text="Sign In"></asp:Button>
                                    <span>
                                        <asp:HyperLink ID="hlinkForgotUID" runat="server"><font color="#fd4310" size="1.9px">Forgot User Name or Password?</font></asp:HyperLink></span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top: 15px; padding-bottom: 15px; padding-left: 8px">
                                    <div class="BetweenDiv">
                                        <span class="BetweenSpan">OR </span>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td class="tablecontrolsfontLogin" style="text-align: left; padding-left: 10px; width: 70px">
                                    <asp:Label ID="Label1" runat="server">Sign in with</asp:Label>
                                </td>
                                <td>
                                    <uc1:SocialNetworkingIntegrationControlSF4 ID="SocialNetworkingIntegrationControlSF4"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:LinkButton ID="cmdNewUser" runat="server" Text="New User Signup!" CssClass="ButtonLink"
                                        SkinID="Test"></asp:LinkButton>
                                </td>
                            </tr>
                            <cc1:AptifyWebUserLogin ID="WebUserLogin1" runat="server" Width="175px" Height="9px"
                                Visible="False"></cc1:AptifyWebUserLogin>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="tblWelcome" cellspacing="3" cellpadding="3" border="0" runat="server"
                style="width: 262px">
                <tr>
                    <%--Nalini Issue 12734--%>
                    <td valign="top" style="padding-left: 50px;">
                        <asp:Label ID="lblWelcome" runat="server">Welcome,</asp:Label>
                        <asp:Button ID="cmdLogOut" runat="server" Width="60px" CausesValidation="False" Text="Logout" />
                    </td>
                </tr>
            </table>
            <cc2:AptifyShoppingCart ID="ShoppingCartLogin" runat="server"></cc2:AptifyShoppingCart>
        </div>
    </div>
</asp:Panel>
<uc2:Sitefinity4xSSO ID="Sitefinity4xSSO1" runat="server" />
<%--<cc1:User ID="User1" runat="server" />--%>
