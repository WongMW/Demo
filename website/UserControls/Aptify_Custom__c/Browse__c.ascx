<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.BrowseControl__c" CodeFile="Browse__c.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
      <tr>
        <td>
          <asp:Label ID="lblHeader" Runat="server">Browse Directory</asp:Label>
        </td>
      </tr>
      <tr>
        <td>
                <b>
                    <asp:HyperLink ID="hrefBrowseName" runat="server">
              Browse By Name</asp:HyperLink></b>
        </td>
      </tr>
      <tr>
        <td>
                <b>
                    <asp:HyperLink ID="hrefBrowseState" runat="server">
              Browse By State</asp:HyperLink></b>
        </td>
      </tr>
      <tr runat="server" id="rowCompanyType">
        <td>
                <b>
                    <asp:HyperLink ID="hrefBrowseCompanyType" runat="server">
              Browse By Company Type</asp:HyperLink></b>
        </td>
      </tr>
      <tr>
        <td>
                <b>
                    <asp:HyperLink ID="hrefBrowseRegion" runat="server">
              Browse By Region</asp:HyperLink></b>
        </td>
      </tr>
      <tr>
        <td>
                <b>
                    <asp:HyperLink ID="hrefSearch" runat="server">
              Search...</asp:HyperLink></b>
        </td>
      </tr>
    </table>
</div>