
 <%@ Control Language="VB" AutoEventWireup="false" CodeFile="ForumsHome.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.ForumsHome" %> 

 <%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %> 
 <%@ Register TagPrefix="uc1" TagName="Forums" Src="Forums.ascx" %> 

<div class="content-container clearfix">
     <table id="tblMain" runat="server">
         <tr>
           <td>
            <table id="tblInner">
              <tr>
                <td colspan="2">
                  <uc1:Forums id="Forums" runat="server"></uc1:Forums>
                  <br />
                </td>
              </tr>
              <tr>
                <td>
                   <img runat="server" id="imgSearch" src="" alt = "Search Forums" />
                   <asp:HyperLink runat="server" ID="lnkSearch" ><asp:Label runat="server" ID="Label1" Text="Search Forums..."></asp:Label></asp:HyperLink>
                </td>
                <td>
                   <img runat="server" id="imgSubscribe" src="" alt = "Subscribe To Forums" />
                   <asp:HyperLink runat="server" ID="lnkSubscribe"><asp:Label runat="server" ID="Label2" Text="Manage Email Subscriptions"></asp:Label></asp:HyperLink >
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <cc2:User ID="User1" runat="server" />
</div>