<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EducationCenter__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.EducationCenter__c" %>
<%@ Register Src="InstructorValidator__c.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register Src="InstructorCenter__c.ascx" TagName="InstructorCenter" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="content-container clearfix">
    <table class="data-form">
        <tr>
            <td>
                <img runat="server" id="imgMyCouses" alt="My Courses" src="" align="absMiddle" border="0" />
                <asp:HyperLink runat="server" ID="lnkMyCourses" Font-Bold="true" Text="My Courses" /><br />
                View courses that you have registered for, are in process of completing, or have
                previously completed
                <br />
            </td>
            <td>
                <img runat="server" id="imgMyCerts" alt="My Certifications" src="" align="absMiddle"
                    border="0" />
                <asp:HyperLink runat="server" ID="lnkMyCerts" Font-Bold="true" Text="My Certifications" /><br />
                View current and past certifications that you have earned
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <img runat="server" id="imgCatalog" alt="Course Catalog" src="" align="absMiddle"
                    border="0" />
                <asp:HyperLink runat="server" ID="lnkCatalog" Font-Bold="true" Text="Course Catalog" /><br />
                Browse the course catalog to find the right educational opportunities for your needs
                <br />
            </td>
            <td>
                <img runat="server" id="imgClassSchedule" alt="Class Schedule" src="" align="absMiddle"
                    border="0" />
                <asp:HyperLink runat="server" ID="lnkClassSchedule" Font-Bold="true" Text="Class Schedule" /><br />
                Browse a complete list of current and future scheduled classes
            </td>
        </tr>
        <tr>
            <td>
                <img runat="server" id="imgCurricula" alt="Curricula" src="" align="absMiddle" border="0" />
                <asp:HyperLink runat="server" ID="lnkCurricula" Font-Bold="true" Text="Curricula" /><br />
                Browse the available Curricula provided by this institution and compare to your
                current course progress
                <br />
            </td>
            <td>
            </td>
        </tr>
        <tr id="trEducationResult" runat="server" visible="false">
            <td>
                <img runat="server" id="imgEducationResult" alt="EducationResult" src="" align="absMiddle"
                    border="0" />
                <asp:HyperLink runat="server" ID="lnkEducationResult" Font-Bold="true" Text="Education Result" /><br />
                Browse Education Result
                <br />
            </td>
        </tr>
        <tr id="trCourseMaterial" runat="server" visible="false">
            <td>
                <img runat="server" id="imgCourseMaterial" alt="CourseMaterial" src="" align="absMiddle"
                    border="0" />
                <asp:HyperLink runat="server" ID="lnkCourseMaterial" Font-Bold="true" Text="Course Material" /><br />
                Course Material
                <br />
            </td>
        </tr>
        <tr id="trCourseEnrollment" runat="server" visible="false">
            <td>
                <asp:HyperLink runat="server" ID="lnkCourseEnrollment" Font-Bold="true" Text="Course Enrollment" /><br />
                Enroll for courses
                <br />
            </td>
        </tr>
        <tr id="trFormalRegistration" runat="server" visible="false">
            <td>
                <img runat="server" id="imgFormalRegistration" alt="CourseMaterial" src="" align="absMiddle"
                    border="0" />
                <asp:HyperLink runat="server" ID="lnkFormalRegistration" Font-Bold="true" Text="Formal Registrations" /><br />
                Formal Registrations
                <br />
            </td>
        </tr>
        <tr id="trFirmContractRegistration" runat="server" visible="false">
            <td>
                <asp:HyperLink runat="server" ID="lnkFirmContractRegistration" Visible="false" Font-Bold="true"
                    Text="Firm Contract Registration" /><br />
                Firm Contract Registration
                <br />
            </td>
        </tr>
        <tr id="trAssignmentDetails" runat="server" visible="false">
            <td>
                <img runat="server" id="imgAssignment" alt="CourseMaterial" src="" align="absMiddle"
                    border="0" />
                <asp:HyperLink runat="server" ID="lnkAssignmentDetails" Font-Bold="true" Text="Assignment Details" /><br />
                AssignmentDetails
                <br />
            </td>
        </tr>

         <tr id="trMentorDashboard" runat="server" visible="false">
            <td>
                <img runat="server" id="imgMentorDashboard" alt="MentorDashboard" src="" align="absMiddle"
                    border="0" />
                <asp:HyperLink runat="server" ID="lnkMentorDashboard" Font-Bold="true" Text="Mentor Dashboard" /><br />
                Mentor Dashboard
                <br />
            </td>
        </tr>
        <tr runat="server" id="trInstructorCenter">
            <td colspan="2" class="TopBorder">
                <uc1:InstructorCenter runat="server" ID="InstructorCenter" />
            </td>
        </tr>
        <tr id="trStudentDashboard" runat="server" visible="false">
            <td>
                <img runat="server" id="imgStudentDashboard" alt="StudentDashboard" src="" align="absMiddle"
                    border="0" />
              <%--Begin: Added by Aptify 2016-05-11--%>
                     <asp:LinkButton ID="lnkCADairy" runat="server" CausesValidation="false" Font-Bold="true">CA Diary</asp:LinkButton>
                <%--End:Added by Aptify 2016-05-11 --%><br />
                Student Dashboard
                <br />
            </td>
        </tr>

        <tr id="trMentorApplication" visible="false" runat="server">
            <td>
                <img runat="server" id="imgMentorApplication" alt="Mentor Application" src="" align="absMiddle"
                    border="0" />
                <asp:HyperLink runat="server" ID="lnkMentorApplication" Font-Bold="true" Text="Mentor Request Application" /><br />
                Mentor Request Application
                <br />
            </td>
        </tr>
         <tr id="trQualificationStatus" visible="false" runat="server">
            <td>
                <img runat="server" id="imgQualificationStatus" alt="Qualification Status" src=""
                    align="absMiddle" border="0" />
                <asp:HyperLink runat="server" ID="lnkQualificationStatus" Font-Bold="true" Text="LLL Qualification Status" /><br />
                LLL Qualification Status
                <br />
            </td>
        </tr>
    </table>
    <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
    <cc1:User ID="User1" runat="server" />
</div>
