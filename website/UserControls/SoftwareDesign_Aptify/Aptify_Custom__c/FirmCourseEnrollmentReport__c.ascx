<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FirmCourseEnrollmentReport__c.ascx.vb" Inherits="FirmCourseEnrollmentReport__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="maindiv">
    <br />
        <div class="info-data" style="align-content:center">    
              <div class="row-div clearfix">
                 <div class="label-div w60"> 
    <asp:DropDownList ID="ddlCurrentStage" runat="server" Width="250px">
       <asp:ListItem>CAP1- CA Proficiency 1</asp:ListItem>
         <asp:ListItem>CAP2- CA Proficiency 2</asp:ListItem>
          <asp:ListItem>Bridge</asp:ListItem>
          <asp:ListItem>FAE- Final Admitting Exam</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" CssClass="submitBtn"/>
                     </div>
                  </div>
            </div> 
    <cc1:User ID="loggedInUser" runat="server" />
</div>
     