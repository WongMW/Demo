<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Customer_Service/SynchProfile.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.SynchProfile" %>
<%@ Register TagPrefix="uc1" TagName="SocialNetworkingIntegrationControlSF4" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/SocialNetworkingIntegrationControlSF4.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
 <%--“This version of SyncProfile is for use with Sitefinity”--%>
<div class="SyncProfileMainDiv">
    <div  class="ProfileLinkedInDiv">
         <asp:CheckBox ID="chkUseSocialMediaPhoto" runat="server" 
             Text="Use my LinkedIn Photo for this Profile."   CssClass="cb" 
             AutoPostBack="True" />
    </div>
    <div class="SyncProfileSyncMessageDiv">
        <asp:Label ID="lblSyncMessage" runat="server" 
            Text="Sync your profile with LinkedIn."></asp:Label>
    </div>
    <div class="SyncProfileLinkedInDiv">
        <uc1:SocialNetworkingIntegrationControlSF4 ID="SocialNetworkingIntegrationControlSF4"
            runat="server" />
    </div>
    <div class="SyncProfileSyncTableDiv">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate> 
        <table id="tblsync" runat="server">
            <tr>
                <td>
                    <asp:Label ID="lbl2" runat="server" Text="Account Sync:"></asp:Label>
                </td>
                <td>
                    &nbsp;
                       <%-- Navin Prasad Issue 12835--%>
                    <asp:Label ID="lblActivateStatus" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="lnkDeactivate" runat="server" Text="Deactivate" ValidationGroup="sync"></asp:LinkButton>
                </td>
            </tr>
        </table>
         </ContentTemplate>            
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkDeactivate" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="chkUseSocialMediaPhoto" 
                                    EventName="CheckedChanged" />
                            </Triggers>
            </asp:UpdatePanel> 
        <div class="SyncProfileSyncTextDiv">
            <asp:HyperLink ID="hypSocialNetworkSynchText" runat="server" Target="_new" CssClass="terms-link"></asp:HyperLink>
        </div>
    </div>
</div>
<cc1:AptifyWebUserLogin ID="WebUserLogin1" runat="server" Visible="False" />
<cc2:User ID="User1" runat="server" />
