<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FirmCourseEnrollmentLanding__c.ascx.vb" Inherits="FirmCourseEnrollmentLanding__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ajax" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="maindiv cai-form">
  <div class="form-title">Please select office and curriculum:</div>
	<div class="cai-form-content">
    <%--<telerik:RadScriptManager runat="server" ID="RadScriptManager1" />--%>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">        
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" EnableAJAX="true">
        <asp:Label ID="lblMessage" runat="server" Text="Please click on curriculum"></asp:Label>
        <div class="form-section-half-border">
		 <div class="field-group">
                 <div>
                     <asp:Label ID="Label23" runat="server" Text="Please select office: " CssClass="label"></asp:Label>
                 </div>
                 <div>
                    <asp:DropDownList ID="ddlLocation" runat="server" Width="300px">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       
                     <asp:Button ID="btnReport" runat="server" Text="Firm DashBoard Reports" CssClass="submitBtn" />
                 </div>
             </div>
         </div>
        <div class="form-section-half-border">
              <div class="field-group">
                     <asp:Label ID="Label1" runat="server" Text="Please click on curriculum: " CssClass="label"></asp:Label>
                 </div>
       <%-- <div class="info-data" style="align-content:center; text-align:center;">--%>
             <div class="field-group">
                 <div>
                     <asp:LinkButton ID="lbtnCAP1" runat="server" Text="CAP1- CA Proficiency 1" Width="300px" CssClass="submitBtn btn-full-width btn"  style="text-decoration:none;"></asp:LinkButton>
                 </div>
             </div>
			<div class="field-group">
                 <div>
                     <asp:LinkButton ID="lbtnBrigde" runat="server" Text="Bridge" Width="300px" CssClass="submitBtn btn-full-width btn"  style="text-decoration:none;"></asp:LinkButton>
                 </div>
             </div>
             <div class="field-group">
                 <div>
                     <asp:LinkButton ID="lbtnCAP2" runat="server" Text="CAP2- CA Proficiency 2" Width="300px" CssClass="submitBtn btn-full-width btn"  style="text-decoration:none;"></asp:LinkButton>
                 </div>
             </div>
			 <div class="field-group">
                 <div>
                     <asp:LinkButton ID="lbtnFAE" runat="server" Text="FAE- Final Admitting Exam" Width="300px" CssClass="submitBtn btn-full-width btn"  style="text-decoration:none;"></asp:LinkButton>
                 </div>
             </div>
            </div> <%--close div form section half border--%>
         <%--</div><%--close div info-data--%>
    </telerik:RadAjaxPanel>
    <cc1:User ID="loggedInUser" runat="server" />
  </div>	
</div>
