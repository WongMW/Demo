<%@ Control Language="VB" AutoEventWireup="false" CodeFile="InstructorCenter__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.InstructorCenter__c" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/InstructorValidator__c.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<table id="tblMain" runat="server">
  <tr>
    <td colspan="2" class="MeetingDates">
      <strong>Instructor Center</strong>
    </td>
  </tr>
  <tr>
    <td colspan="2"> 
	    <asp:HyperLink runat="server" ID="lnkInstructorClasses" Font-Bold="true" Text="My Class List" />
    </td>
  </tr>
  <tr>
    <td colspan="2">
        <asp:HyperLink runat="server" ID="lnkInstructorStudents" Font-Bold="true" Text="My Student List" />
    </td>
  </tr>
  <tr>
    <td colspan="2">
        <asp:HyperLink runat="server" ID="lnkInstructorAuthCourses" Font-Bold="true" Text="Authorized Courses" />
    </td>
  </tr>
  <tr>
    <td colspan="2">
        <asp:HyperLink runat="server" ID="lnkInstructorCalendar" Font-Bold="true" Text="Lecturer Calendar" />
    </td>
  </tr>
</table>
<uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
