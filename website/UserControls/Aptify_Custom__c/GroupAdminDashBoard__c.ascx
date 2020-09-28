<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GroupAdminDashBoard__c.ascx.vb" 
Inherits="UserControls_Aptify_Custom__c_GroupAdminDashBoard__c" %>

<%@ Register src="~/UserControls/Aptify_Custom__c/ManageMyGroup__c.ascx" tagname="ManageMyGroup" tagprefix="uc1" %>
<%@ Register src="~/UserControls/Aptify_Group_Admin/MembershipExpireStatus.ascx" tagname="MembershipExpireStatus" tagprefix="uc2" %>
<%@ Register src="~/UserControls/Aptify_Group_Admin/UpcomingEventsRegistrationChart.ascx" tagname="UpcomingEventsRegistrationChart" tagprefix="uc3" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register src="~/UserControls/Aptify_Group_Admin/EventCalendar.ascx" tagname="EventCalendar" tagprefix="uc4" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<table width="100%" border="0" cellpadding="0px" cellspacing="0px">
    <tr>
        <td valign="top">
            
            <uc1:ManageMyGroup ID="ManageMyGroup1" runat="server" />
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
