<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LoginAndEvent.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.LoginAndEvent" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register src="~/UserControls/Aptify_Meetings/UpcomingEvents.ascx" tagname="UpcomingEvents" tagprefix="uc1" %>
<%@ Register src="~/UserControls/Aptify_General/LoginSF4.ascx" tagname="LoginSF4" tagprefix="uc2" %>
<%--<asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwLogin" runat="server">
        <uc2:Login ID="Login" runat="server" />
          <uc1:UpcomingEvents ID="UpcomingEvents1" runat="server" />
    </asp:View>
        <asp:View ID="vwEvents" runat="server">
            <uc1:UpcomingEvents ID="UpcomingEvents" runat="server" />
    </asp:View>
</asp:MultiView>--%>
<%--Nalini Issue 12429--%>
<table width = "100%">
<tr id  ="trLogin" runat = "server">
    <td>
 <uc2:LoginSF4 ID="LoginSF4" runat="server" />
    </td>
</tr>
    <tr>
        <td style="width:40px;">
            &nbsp;&nbsp;
        </td>
    </tr>
<tr id = "trEvents" runat = "server">
    <td>
 <uc1:UpcomingEvents ID="UpcomingEvents" runat="server" />
    </td>
</tr>
</table>
<cc1:User ID = "User1" runat = "server" />