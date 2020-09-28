<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/InstructorCenter__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.InstructorCenter__c" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/InstructorValidator__c.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div id="tblMain" runat="server">
    <asp:HyperLink runat="server" ID="lnkInstructorClasses" Text="My Class List" /><br />
    <%-- redmine 13806--%>
    <div style="display: none;">
        <asp:HyperLink runat="server" ID="lnkInstructorStudents" Text="My Student List" /><br />
        <asp:HyperLink runat="server" ID="lnkInstructorAuthCourses" Text="Authorized Courses" /><br />
        <asp:HyperLink runat="server" ID="lnkInstructorCalendar" Text="Lecturer Calendar" />
    </div>
</div>
<uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
