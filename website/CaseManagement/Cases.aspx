<%@ Page Title="" Language="VB"  AutoEventWireup="false" CodeFile="Cases.aspx.vb" Inherits="CaseManagement_Cases" %>
<%@ Register TagPrefix = "uc1" TagName = "CaseManagement" Src = "~/UserControls/Aptify_Case_Management/Cases.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentArea" Runat="Server">

<table width="100%" border = "0" cellpadding="0" cellspacing="0" >
    <tr style="height:40px;"  >
    <td valign="middle" class = "TDTitle"     >
			    <p align="center">
			    		    			    			    			    			    
			    <asp:label id="lbl1" runat="server" BackColor="Transparent" forecolor="White" font-size="14pt"
					    Font-Bold="True" Text = "Case Management">
			     </asp:label>
					    <br />	    
				</p>
		    </td>
    </tr>
    <tr>
        <td>
        <uc1:CaseManagement ID = "CaseManagement" runat = "server" />
        </td>
    </tr>
</table>

</asp:Content>

