<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/GroupAdminDashBoard__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_GroupAdminDashBoard__c" %>

<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/navbar.ascx" TagName="navbar" TagPrefix="uc5" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Group_Admin/MembershipExpireStatus.ascx" TagName="MembershipExpireStatus" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Group_Admin/UpcomingEventsRegistrationChart.ascx" TagName="UpcomingEventsRegistrationChart" TagPrefix="uc3" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Group_Admin/EventCalendar.ascx" TagName="EventCalendar" TagPrefix="uc4" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="group-admin-dashboard clearfix">
    
    <div class="group-admin-menu">
       <uc5:navbar ID="navbar1" runat="server" />
    </div>

    <div class="group-admin-content clearfix">
        <uc2:MembershipExpireStatus ID="MembershipExpireStatus1" runat="server" />

        <div class="group-admin-content-bottom">
            <uc3:UpcomingEventsRegistrationChart ID="UpcomingEventsRegistrationChart1" runat="server" />
        </div>
    </div>

    <div class="group-admin-calendar calendar">
        <uc4:EventCalendar ID="EventCalendar1" runat="server" />
    </div>

    <cc2:User ID="User1" runat="server" />

</div>