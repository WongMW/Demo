<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/EducationCenter__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.EducationCenter__c" %>
<%@ Register Src="InstructorValidator__c.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register Src="InstructorCenter__c.ascx" TagName="InstructorCenter" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<style>
    .education-center td div {
        max-width: 460px !important;
    }
</style>
<div class="content-container clearfix">
    <div runat="server" id="MOODLELinkLi" visible="false" class="button-block style-1">
        </br>
        <asp:HyperLink runat="server" ID="MOODLELinkA" Style="border-radius: 10px;text-decoration: none;font-weight: 700;" class="btn">Access your online learnig </asp:HyperLink>
    </div>
    <div class="button-block style-1" style="display: none;">
        <a style="text-decoration: none;" class="btn-full-width btn" href="/cacurriculum/ExemptionApplication.aspx">Eligibility & Exemption Application</a>
    </div>
    <table class="education-center">
        <tr>
            <td>
                <!-- HIDE AS NOT USING
                <div>
                    <img style="display: none;" runat="server" id="imgMyCouses" alt="My Courses" src="" align="absMiddle" border="0" />
                    <asp:HyperLink runat="server" ID="lnkMyCourses" Font-Bold="true" Text="My Courses" />
                    <br />
                    View courses that you have registered for, are in process of completing, or have
                previously completed
                <br />
                    <br />
                </div>
-->
                <!-- HIDE AS NOT USING
                <div>
                    <img style="display: none;" runat="server" id="imgMyCerts" alt="My Certifications" src="" align="absMiddle"
                        border="0" />
                    <asp:HyperLink runat="server" ID="lnkMyCerts" Font-Bold="true" Text="My Certifications" />
                    <br />
                    View current and past certifications that you have earned
                <br />
                    <br />
                </div>
-->
                <!-- HIDE AS NOT USING
                <div>
                    <img style="display: none;" runat="server" id="imgCatalog" alt="Course Catalog" src="" align="absMiddle"
                        border="0" />
                    <asp:HyperLink runat="server" ID="lnkCatalog" Font-Bold="true" Text="Course Catalog" />
                    <br />
                    Browse the course catalog to find the right educational opportunities for your needs
                <br />
                    <br />
                </div>
-->
                <!-- HIDE AS NOT USING
                <div>
                    <img style="display: none;" runat="server" id="imgClassSchedule" alt="Class Schedule" src="" align="absMiddle"
                        border="0" />
                    <asp:HyperLink runat="server" ID="lnkClassSchedule" Font-Bold="true" Text="Class Schedule" />
                    <br />
                    Browse a complete list of current and future scheduled classes<br />
                    <br />
                </div>
-->
                <!-- HIDE AS NOT USING
                <div>
                    <img style="display: none;" runat="server" id="imgCurricula" alt="Curricula" src="" align="absMiddle" border="0" />
                    <asp:HyperLink runat="server" ID="lnkCurricula" Font-Bold="true" Text="Curricula" />
                    <br />
                    Browse the available Curricula provided by this institution and compare to your
                current course progress
                <br />
                    <br />
                </div>
-->
                <!-- Changing to 1 column table
            </td>
            <td>
-->
                <div id="trEducationResult" runat="server" visible="false">

                    <img style="display: none;" runat="server" id="imgEducationResult" alt="EducationResult" src="" align="absMiddle"
                        border="0" />
                    <asp:HyperLink runat="server" ID="lnkEducationResult" Font-Bold="true" Text="Education result" />
                    <br />
                    Browse education result
                <br />
                    <br />

                </div>
                <div id="trCourseMaterial" runat="server" visible="false">
                    <img style="display: none;" runat="server" id="imgCourseMaterial" alt="CourseMaterial" src="" align="absMiddle"
                        border="0" />
                    <asp:HyperLink runat="server" ID="lnkCourseMaterial" Font-Bold="true" Text="Course material" />
                    <br />
                    Course material
                <br />
                    <br />
                </div>
                <div id="trCourseEnrollment" runat="server" visible="false">
                    <asp:HyperLink runat="server" ID="lnkCourseEnrollment" Font-Bold="true" Font-Underline="true" Text="Course enrolment" />
                    <br />
                    Enrol for courses
                <br />
                    <br />
                </div>
                <div id="trFormalRegistration" runat="server" visible="false">
                    <img style="display: none;" runat="server" id="imgFormalRegistration" alt="CourseMaterial" src="" align="absMiddle"
                        border="0" />
                    <asp:HyperLink runat="server" ID="lnkFormalRegistration" Font-Bold="true" Font-Underline="true" Text="Formal registrations" />
                    <br />
                    Formal registrations
                <br />
                    <br />
                </div>
                <div id="trFirmContractRegistration" runat="server" visible="false">
                    <asp:HyperLink runat="server" ID="lnkFirmContractRegistration" Visible="false" Font-Bold="true" Font-Underline="true"
                        Text="Firm contract registration" />
                    <br />
                    Firm contract registration
                <br />
                    <br />
                </div>
                <div id="trAssignmentDetails" runat="server" visible="false">
                    <img style="display: none;" runat="server" id="imgAssignment" alt="CourseMaterial" src="" align="absMiddle"
                        border="0" />
                    <asp:HyperLink runat="server" ID="lnkAssignmentDetails" Font-Bold="true" Font-Underline="true" Text="Assignment details" />
                    <br />
                    Assignment details
                <br />
                    <br />
                </div>

                <div id="trMentorDashboard" runat="server" visible="false">
                    <img style="display: none;" runat="server" id="imgMentorDashboard" alt="MentorDashboard" src="" align="absMiddle"
                        border="0" />
                    <asp:HyperLink runat="server" ID="lnkMentorDashboard" Font-Bold="true" Font-Underline="true" Text="CA Diary Mentor Dashboard" />

                    <br />
                    <br />
                </div>
                <div runat="server" id="trInstructorCenter">
                    <uc1:InstructorCenter runat="server" ID="InstructorCenter" />
                </div>
                <div id="trStudentDashboard" runat="server" visible="false">
                    <img style="display: none;" runat="server" id="imgStudentDashboard" alt="StudentDashboard" src="" align="absMiddle"
                        border="0" />
                    <%--Begin: Added by Aptify 2016-05-11--%>
                    <asp:LinkButton ID="lnkCADairy" runat="server" CausesValidation="false" Font-Bold="true" Font-Underline="true">CA Diary</asp:LinkButton>
                    <%--End:Added by Aptify 2016-05-11 --%>
                    <br />
                    <br />
                </div>

                <div id="trMentorApplication" visible="false" runat="server" style="display: none;">
                    <img style="display: none;" runat="server" id="imgMentorApplication" alt="Mentor Application" src="" align="absMiddle"
                        border="0" />
                    <asp:HyperLink runat="server" ID="lnkMentorApplication" Font-Bold="true" Text="Mentor request application" Font-Underline="true" /><br />
                    Mentor request application
                <br />
                    <br />
                </div>
                <div id="trQualificationStatus" visible="false" runat="server">
                    <img style="display: none;" runat="server" id="imgQualificationStatus" alt="Qualification Status" src=""
                        align="absMiddle" border="0" />
                    <asp:HyperLink runat="server" ID="lnkQualificationStatus" Font-Bold="true" Text="LLL qualification status" /><br />
                    LLL qualification status
                <br />
                    <br />
                </div>
                <%--Begin: Added by Kavita Zinage 2017-09-14 --%>
                <div id="trClassAttendance" visible="false" runat="server">
                    <img style="display: none;" runat="server" id="imgClassAttd" alt="ClassAttendance" src=""
                        align="absMiddle" border="0" />
                    <asp:LinkButton ID="lnkclassAttd" runat="server" CausesValidation="false" Font-Bold="true">Class attendance</asp:LinkButton>

                    <br />
                    <br />
                </div>
                <%--Begin: Added by LH 2018-01-18 --%>
                <div id="trRecordedSessions" runat="server" visible="false">
                    <asp:HyperLink ID="lnkRecordings" runat="server" CausesValidation="false" Font-Bold="true" Font-Underline="true" Text="Recorded sessions" Target="_blank" />

                    <br />
                    <br />
                </div>
                <%--added by LH -4-04-19 ticket #20473--%>
                <div id="trCharteredBooks" runat="server" visible="false">
                    <asp:HyperLink runat="server" ID="lnkCharteredBooks" Font-Bold="true" Font-Underline="true" Text="Chartered Textbooks Online" Target="_blank"></asp:HyperLink>
                    <br />
                    Access digital versions of the <strong>tax</strong> textbooks for CAP1, CAP2 and FAE 1
                <br />
                    <br />
                </div>
                <div id="trBooksLink" runat="server" visible="false">
                    <asp:HyperLink runat="server" ID="lnkBooksPage" Font-Bold="true" Font-Underline="true" Text="Chartered Textbooks Online" Target="_blank"></asp:HyperLink>
                    <br />
                    Access digital versions of the <strong>tax</strong> textbooks for CAP1, CAP2 and FAE 2
                <br />
                    <br />
                </div>
                <%--End:Added by Kavita Zinage 2017-09-14 --%>
                <%--added by EM 17-04-20 ticket #21219--%>
                <div id="trProQuestEBook" runat="server" visible="false">
                    <asp:HyperLink runat="server" ID="lnkProQuestEBook" Font-Bold="true" Font-Underline="true" Text="ProQuest e-book Central" Target="_blank"></asp:HyperLink>
                    <br />
                    Access to PDF versions of all textbooks for CAP1, CAP2 and FAE<br />
                    <br />
                </div>
            </td>
        </tr>
    </table>
    <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
    <cc1:User ID="User1" runat="server" />
</div>
