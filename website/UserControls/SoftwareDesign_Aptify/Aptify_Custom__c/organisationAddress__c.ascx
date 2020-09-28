<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/organisationAddress__c.ascx.vb" Inherits="organisationAddress__c" Debug="true" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>


<style type="text/css">
    .auto-style1 {
        width: 472px;
    }
    .auto-style2 {
        width: 153px;
        height: 23px;
    }
    .auto-style3
    {
        width: 93px;
    }
</style>


<table>
    <tr>
        <td class="auto-style3">
             <asp:Label ID ="lblAddress"  runat ="server" />

        </td>
        <td class="auto-style1">
             &nbsp;</td>
    </tr>
      <tr>
        <td class="auto-style2" colspan="2"><strong>Or by email to:</strong>&nbsp;
            

        
              <asp:HyperLink ID="email" runat="server"></asp:HyperLink>
          </td>
    </tr>

</table>