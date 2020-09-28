<%--<%@ Register src="~/UserControls/SoftwareDesign_Aptify/Aptify_Sitefinity4/Sitefinity4xSSO.ascx" tagname="Sitefinity4xSSO" tagprefix="uc2" %>--%>
<%-- <%@ Control Language="VB" AutoEventWireup="false"  %> --%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/Login.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.WebLogin" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="uc1" TagName="SocialNetworkingIntegrationControl" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/SocialNetworkingIntegrationControlSF4.ascx" %>
<META Http-Equiv="Cache-Control" Content="no-cache">
<META Http-Equiv="Pragma" Content="no-cache">
<META Http-Equiv="Expires" Content="0">
<div id="loginTop" class="news-list" runat="server">
    <table style="width: 95%">
        <tr>
            <td style="font-weight:bold;font-size:14px;padding-left :5px;" >
                Log In
            </td>
            <td style="text-align: right">
                <asp:CheckBox ID="chkAutoLogin" runat="server" ToolTip="Check this box if you would like the site to automatically log you in next time you visit."
                    Text=" Keep me signed in" AutoPostBack="true"></asp:CheckBox>
            </td>
        </tr>
    </table>
    <div class="content-container-Home clearfix" style="width: 100%">
        <asp:Literal runat="server" ID="litLoginLabel" />
        <table id="tblLogin" cellspacing="0" cellpadding="0" border="0" runat="server">
            <tr>
                <td valign="top" colspan="2" class="style1">
                    <asp:Label ID="lblError" ForeColor="Crimson" runat="server"></asp:Label>
                    <table id="tblData" border="0" runat="server" cellspacing="3" cellpadding="3">
                        <tr>
                            <td align="right" valign="top" class="tablecontrolsfontLogin" style="text-align: left;
                                padding-left: 4px;">
                                <%--  Dilip issue 12717  --%>
                                <asp:Label ID="lblUserID" runat="server">User ID:</asp:Label>&nbsp;
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtUserID" runat="server" Width="175px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" class="tablecontrolsfontLogin" style="text-align: left;
                                padding-left: 4px;">
                                <asp:Label ID="lblPassword" runat="server">Password:</asp:Label>&nbsp;
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtPassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
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
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <%--  Dilip issue 12717--%>
                                <asp:Button ID="cmdLogin" runat="server" Text="Log In" Width="48px" Height="25px"
                                    CssClass="submitBtn"></asp:Button>
                                <span>
                                    <asp:HyperLink ID="hlinkForgotUID" runat="server"><font color="#fd4310" size="1px">Forgot User Name or Password?</font></asp:HyperLink></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td class="tablecontrolsfontLogin">
                                &nbsp; Or
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td class="tablecontrolsfontLogin" style="text-align: left; padding-left: 5px;">
                                <asp:Label ID="Label1" runat="server">Sign in With</asp:Label>
                            </td>
                            <td>
                                <uc1:SocialNetworkingIntegrationControl ID="SocialNetworkingIntegrationControl" runat="server" />
                            </td>
                            <td>
                                <%--<asp:HyperLink ID="linkNewUser" runat="server"></asp:HyperLink>--%>
                                <asp:LinkButton ID="cmdNewUser" runat="server" Text="New User Signup!" CssClass="ButtonLink"
                                    SkinID="Test"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <%--<asp:button id="cmdLogin" runat="server" Width="80px" Text="Login" ></asp:button> <asp:button id="cmdNewUser" runat="server" Width="80px" Text="New User?"></asp:button><br/>
                <asp:HyperLink ID="hlinkForgotUID" runat="server" width="200px">Forgot your User ID or Password?</asp:HyperLink>--%>
                    <%--<asp:label id="lblError" ForeColor="Crimson" Runat="server" ></asp:label>--%>
                    <cc1:AptifyWebUserLogin ID="WebUserLogin1" runat="server" Width="175px" Height="9px"
                        Visible="False"></cc1:AptifyWebUserLogin>
                </td>
            </tr>
        </table>
        <table id="tblWelcome" cellspacing="3" cellpadding="3" border="0" runat="server"
            style="width: 245px">
            <tr>
                <%--Nalini Issue 12734--%>
                <td valign="top" colspan="2" style="padding-left: 50px;">
                    <asp:Label ID="lblWelcome" runat="server">Welcome,</asp:Label>
                    <%--<input id="cmdLogOut" type="button" value="Logout" runat="server" width="80px" causesvalidation="False" />--%>
                    <asp:Button ID="cmdLogOut" runat="server" Width="60px" CausesValidation="False" Text="Logout" CssClass="submitBtn" />
                </td>
            </tr>
        </table>
        <%--<uc1:SocialNetworkingIntegrationControlSF4 ID ="SocialNetworkingIntegrationControlSF4" runat="server" />--%>
        <cc2:AptifyShoppingCart ID="ShoppingCartLogin" runat="server"></cc2:AptifyShoppingCart>
    </div>
</div>
