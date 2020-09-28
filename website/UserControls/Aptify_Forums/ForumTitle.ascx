
 <%@ Control Language="VB" AutoEventWireup="false" CodeFile="ForumTitle.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.ForumTitleControl" %> 

 <%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %> 
 <%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="PageSecurity" %> 
 <%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %> 
 <%@ Register TagPrefix="uc1" TagName="Forums"  Src="Forums.ascx" %> 

<div class="content-container clearfix">
			    <table class="data-form">
				    <tr>
					    <td>
					        <span class="Title"><asp:label id="lblDiscussionForum" runat="server"></asp:label></span><br />
					        <asp:label id="parForumLabel" runat="server" Text="Parent Forum: " />
					        <a runat="server" id="lnkParent"><asp:Label ID="lblParent" runat="Server" ></asp:Label></a>
					    </td>
					    <td>
                            <span class="SmallTitle"><asp:HyperLink runat="server" ID="lnkSubscribe" title="Click here to manage all Aptify Forums subscriptions">Manage Subscriptions</asp:HyperLink ></span>
                        </td>
                    </tr>
				    <tr>
				        <td colspan="2">
     						<asp:label id="lblDescription" runat="server"></asp:label>
				        </td>
				    </tr>
				    </table>
</div>		    
    <cc2:User ID="User1" runat="server" />
