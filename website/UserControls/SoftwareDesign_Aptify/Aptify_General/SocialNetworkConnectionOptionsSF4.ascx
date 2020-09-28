<%@ Register src="Sitefinity4xSSO.ascx" tagname="Sitefinity4xSSO" tagprefix="uc1" %>
<style type="text/css">
    .style4
    {
        width: 248px;
    }
    .style5
    {
        width: 1192px;
    }
    .style6
    {
        width: 400px;
        height:2px;
    }
    .style7
    {
        width: 523px;
        height: 11px;
    }
    .paddlabel
    {
        padding-left:5px;
    }
    .paddlabelremember
    {
        padding-left:7px;
    }
</style>
<%-- <%@ Control Language="VB" AutoEventWireup="false"  %> --%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/SocialNetworkConnectionOptionsSF4.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.SocialNetworkConnectionOptions"%>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="uc1" TagName="SocialNetworkingIntegrationControlSF4" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/SocialNetworkingIntegrationControlSF4.ascx" %>
<script type="text/javascript">
    function EnableDisableOptionControls(chk) {
        var txtUID = document.getElementById('<%= txtUserID.ClientID%>')
        var txtPWD = document.getElementById('<%= txtPassword.ClientID%>')
        var chkSynchronizeProfile = document.getElementById('<%= chkSynchronizeProfile.ClientID%>')
        if (chk.getAttribute("id") == '<%= rdoExistingUser.ClientID%>') {

            if (chk.checked) {
                txtUID.disabled = false;
                txtPWD.disabled = false;
                chkSynchronizeProfile.disabled = false;
            }
            else {
                txtUID.disabled = true;
                txtPWD.disabled = true;
                /* chkSynchronizeProfile.disabled = true; */
            }
        }
        else {
            txtUID.disabled = true;
            txtPWD.disabled = true;
            /*  chkSynchronizeProfile.disabled = true; */
        }
    }
    function EnableDisablePhotoOption(chk) {
        var chkUseSocialMediaPhoto = document.getElementById('<%= chkUseSocialMediaPhoto.ClientID%>')
            if (chk.getAttribute("id") == '<%=chkSynchronizeProfile.ClientID%>') {

            if (chk.checked) {

                chkUseSocialMediaPhoto.disabled = false;
            }
            else {
                chkUseSocialMediaPhoto.disabled = true;
                chkUseSocialMediaPhoto.checked = false;
            }
          }
       }
</script>
<div id="loginTop" class="news-list" runat="server">
<h1 runat="server" id="lblLogin">
    Login</h1>
<span class="content-container clearfix">
<table id="tblLogin" cellspacing="0" cellpadding="0" border="0" runat="server">
            <tr>
                <td valign="top" class="style6" style="padding-left:1px;">
                    &nbsp;<asp:RadioButton ID="rdoExistingUser" runat="server" 
                        GroupName="ConfirmationOption" Checked="true" /><asp:Label ID="LblrdoExistingUser" runat="server" CssClass="paddlabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" class="style6">
                    <table id="tblData" border="0" runat="server" cellspacing="3" cellpadding="3">
                        <tr>
                            <td  valign="top" style="padding-left:19px; float:left; width:50px;">
                                <asp:Label ID="lblUserID" runat="server"><b>User ID</b></asp:Label>&nbsp;
                            </td>
                            <td valign="top" class="style5" style="padding-bottom: 5px;">
                                <asp:TextBox ID="txtUserID" runat="server" Width="175px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td  valign="top" style="padding-left:19px; float:left;width:70px;">
                                <asp:Label ID="lblPassword" runat="server"><b>Password</b></asp:Label>&nbsp;
                            </td>
                            <td valign="top" class="style5">
                                <asp:TextBox ID="txtPassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="style7">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style6" colspan="2">
                                &nbsp;<asp:RadioButton ID="rdoNewUser" runat="server" 
                                    GroupName="ConfirmationOption" /><asp:Label ID="lblrdoNewUser" runat="server" CssClass="paddlabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style6" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                             <td class="style5" colspan ="2">
                                   &nbsp;<asp:CheckBox ID="chkSynchronizeProfile" runat="server" CssClass="cb" 
                                    TextAlign="right" /> <asp:Label ID="lblchkSynchronizeProfile" runat="server" CssClass="paddlabel"></asp:Label> <br />
                                    &nbsp;<asp:CheckBox  ID="chkUseSocialMediaPhoto" runat="server" CssClass="cb" 
                                       Enabled="False" /> <asp:Label ID="lblchkUseSocialMediaPhoto" runat="server" CssClass="paddlabel"></asp:Label><br />
                                       &nbsp;<asp:CheckBox 
                                       ID="chkRememberMe" runat="server"  CssClass="cb" AutoPostBack="True"  /><asp:Label ID="lblchkRememberMe" runat="server" CssClass="paddlabelremember" Text="Keep me signed in."></asp:Label>
                                       <br />
                                          <asp:UpdatePanel ID="pnl1" runat="server" >
                              <ContentTemplate >
                               <asp:Label ID="lbl" runat="server" Visible="false"  ></asp:Label>
                                    </ContentTemplate>
                                    <Triggers >
                                    <asp:AsyncPostBackTrigger ControlID="chkRememberMe" EventName ="" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                &nbsp;<asp:HyperLink ID="hypSocialNetworkSynchText" runat="server" Target="_new" ForeColor="Red"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                                <td class="style5" colspan ="2" style="padding-left:4px;">
                                Need Help?
                                <asp:HyperLink ID="hypContactUS" runat="server" Target="_new">Contact Us</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr>
                <td class="style6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style6" style="padding-left:4px;">
                    <asp:Button ID="btnContinue" runat="server"  CssClass="submitBtn" Text="Continue" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CssClass="submitBtn" />
                    <asp:Label ID="lblError" ForeColor="Crimson" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </span>
</div>
<span class="content-container clearfix">
 <cc1:AptifyWebUserLogin id="WebUserLogin1" runat="server" Width="175px" Height="9px" Visible="False"></cc1:AptifyWebUserLogin>
</span>
<uc1:Sitefinity4xSSO ID="Sitefinity4xSSO1" runat="server" />

<%--'Anil B for issue Issue 13316 on 16-03-2013 
 'Set Social Network user control to the connection option--%>
<div style=" display:none" >
        <uc1:SocialNetworkingIntegrationControlSF4 ID="SocialNetworkingIntegrationControlSF4" runat="server" />           
</div>




    








