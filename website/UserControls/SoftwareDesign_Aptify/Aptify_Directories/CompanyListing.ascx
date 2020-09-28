<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Directories.CompanyListingControl" CodeFile="~/UserControls/Aptify_Directories/CompanyListing.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>

<div class="content-container clearfix">
      <table id="tblMain" runat="server" class="data-form">
          <tr>
              <td colspan="2" class="BottomBorder">
                  <asp:Label id="lblCompanyName" cssclass="MeetingName" runat="server">COMPANY NAME</asp:Label>
               </td>
          </tr>
        <tr>
          <td class="LeftColumn">Address:</td>
          <td>
            <asp:Label id="lblAddress" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td class="LeftColumn">Main Email:</td>
          <td>
            <asp:Label id="lblMainEmail" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td class="LeftColumn">Info Email:</td>
          <td>
            <asp:Label id="lblInfoEmail" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td class="LeftColumn">Jobs Email:</td>
          <td>
            <asp:Label id="lblJobsEmail" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td class="LeftColumn">Phone:</td>
          <td>
            <asp:Label id="lblPhone" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td class="LeftColumn">Fax:</td>
          <td>
            <asp:Label id="lblFax" runat="server"></asp:Label></td>
        </tr>
      </table>
      <asp:Label ID="lblError" runat="server" Text="No record available" Visible="False"></asp:Label>
</div>
