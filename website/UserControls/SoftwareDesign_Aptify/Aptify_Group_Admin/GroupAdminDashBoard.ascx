<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/GroupAdminDashBoard.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Admin.GroupAdminDashBoard" %>


<%@ Register src="MembershipExpireStatus.ascx" tagname="MembershipExpireStatus" tagprefix="uc2" %>
<%@ Register src="UpcomingEventsRegistrationChart.ascx" tagname="UpcomingEventsRegistrationChart" tagprefix="uc3" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register src="EventCalendar.ascx" tagname="EventCalendar" tagprefix="uc4" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<table width="100%" border="0" cellpadding="0px" cellspacing="0px">
    <tr>
        <td valign="top">
        </td>
        <td valign="top">
            <table width="100%" border="0" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td valign="top">
                        <uc2:MembershipExpireStatus ID="MembershipExpireStatus1" runat="server" />
                        
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding-top: 22px;">
                        <uc3:UpcomingEventsRegistrationChart ID="UpcomingEventsRegistrationChart1" 
                            runat="server" />
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top" style="padding-left: 10px;">
            <uc4:EventCalendar ID="EventCalendar1" runat="server" />
        </td>
    </tr>
</table>



  <cc2:User ID="User1" runat="server" />
