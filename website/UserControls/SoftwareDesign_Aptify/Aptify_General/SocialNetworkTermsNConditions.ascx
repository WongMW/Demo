<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/SocialNetworkTermsNConditions.ascx.vb" Inherits="UserControls_Aptify_General_SocialNetworkTermsNConditions" %>

 <table border = "0" cellpadding="0" cellspacing="0" >
   <tr style="height:20px;"  >
    <td valign="middle">

  <%--  Anil Bisen Changess for issue 12732--%>

			    <%--<p align="left">
			    <asp:label id="lbl1" runat="server" BackColor="Transparent" forecolor="#172b6a" font-size="14pt"
					    Font-Bold="True" Font-Names="Arial" Text = "Social Network Integration - Terms and Conditions">
			     </asp:label>
					    <br /><br />
				</p>--%>
		    </td>
	    </tr>
	    <tr>
	    <td >
	    <div align="center" style="text-align: justify; padding-left: 10px;"> 
        <asp:Panel ID="Panet1" runat="server" Width="97%">
	    <font  style="font-family: segoe ui, arial, helvetica; color: #333000; font-size: 12px; padding-right: 10px;" >By clicking the <b>I agree to synchronize my social network profile</b> box, you grant this Web site access to your public social network profile. This may include information about your current employer, education history, employment history, and contacts (also referred to as “connections” or “friends” depending on the social network you selected).
            Please note that we will not share any of this information with third parties, 
            as defined in our privacy policy.
            <br>
            <br>
            However, you can also control what information is available in your public 
            profile from your social network account, including the ability to revoke this 
            site’s ability to access your profile. Note that revoking access will disable 
            the ability to sign in to this site using your social network account.<br>
       </font> 
       </asp:Panel>
    </div>

   <%-- Anil changess end--%>
	    </td>
	    </tr>
    </table>