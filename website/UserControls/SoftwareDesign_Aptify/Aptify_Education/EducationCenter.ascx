<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/EducationCenter.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.EducationCenter" %>
<%@ Register Src="InstructorValidator.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register Src="InstructorCenter.ascx" TagName="InstructorCenter" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix">
    <div class="data-form">
        <img runat="server" id="imgMyCouses" alt="My Courses" src="" align="absMiddle" border="0" style="display: none;" />
        <asp:HyperLink runat="server" ID="lnkMyCourses" Font-Bold="true" Text="My Courses" /><br />
        View courses that you have registered for, are in process of completing, or have previously completed
        <br /><br />

        <img runat="server" id="imgMyCerts" alt="My Certifications" src="" align="absMiddle" border="0" style="display: none;" />
        <asp:HyperLink runat="server" ID="lnkMyCerts" Font-Bold="true" Text="My Certifications" /><br />
        View current and past certifications that you have earned
        <br /><br />

        <img runat="server" id="imgCatalog" alt="Course Catalog" src="" align="absMiddle" border="0" style="display: none;" />
        <asp:HyperLink runat="server" ID="lnkCatalog" Font-Bold="true" Text="Course Catalog" /><br />
        Browse the course catalog to find the right educational opportunities for your needs
        <br /><br />

        <img runat="server" id="imgClassSchedule" alt="Class Schedule" src="" align="absMiddle" border="0" style="display: none;" />
        <asp:HyperLink runat="server" ID="lnkClassSchedule" Font-Bold="true" Text="Class Schedule" /><br />
        Browse a complete list of current and future scheduled classes
        
        <br /><br />
        <img runat="server" id="imgCurricula" alt="Curricula" src="" align="absMiddle" border="0" style="display: none;" />
        <asp:HyperLink runat="server" ID="lnkCurricula" Font-Bold="true" Text="Curricula" /><br />
        Browse the available Curricula provided by this institution and compare to your current course progress
        <br />
        <br />

        <div runat="server" id="trInstructorCenter">
            <uc1:InstructorCenter runat="server" ID="InstructorCenter" />
        </div>
    </div>
    <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
</div>
