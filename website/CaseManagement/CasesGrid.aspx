<%@ Page Title="" Language="VB"  AutoEventWireup="true" CodeFile="CasesGrid.aspx.vb" Inherits="ProductCatalog_CasesGrid" %>
<%@ Register TagPrefix = "uc1" TagName = "CasesGrid" Src = "~/UserControls/Aptify_Case_Management/CasesGrid.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentArea" Runat="Server">
<table width="100%" border = "0" cellpadding="0" cellspacing="0" >
    <tr style="height:40px;"  >
    <td valign="middle" class = "TDTitle"     >
			    <p align="center">
			    		    			    			    			    			    
			    <asp:label id="lbl1" runat="server" BackColor="Transparent" forecolor="White" font-size="14pt"
					    Font-Bold="True" Text = "List Of All Cases">
			     </asp:label>
					    <br />	    
				</p>
		    </td>
    </tr>
    <tr>
        <td>
        <uc1:CasesGrid ID = "CasesGrid" runat = "server" />
        </td>
    </tr>
</table>
    <cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
</asp:Content>

