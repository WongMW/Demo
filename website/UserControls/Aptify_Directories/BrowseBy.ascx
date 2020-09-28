<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Directories.BrowseByControl" CodeFile="BrowseBy.ascx.vb" debug= "true" %>
<%@ Register Src="PersonDirectoryGrid.ascx" TagName="PersonDirectoryGrid" TagPrefix="uc2" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="CompanyDirectoryGrid" Src="CompanyDirectoryGrid.ascx" %>

<div class="content-container clearfix">
    <table runat="server" id="tblMain" class="data-form" cellpadding="10">
	    <tr>
	        <td colspan="2">
	            <table width="100%" runat="server" id="innerTable" class="data-form">
		            <tr>
		                <td style="white-space:nowrap" class="MeetingDates">
			                Browse By: <asp:Label Runat="server" ID="lblBrowseBy"></asp:Label>
		                </td>
		                <%--<td class="MeetingDates" align="left">
			               Results
		                </td>--%>
		            </tr>
		        </table>
		    </td>
	    </tr>
	    <tr>
		    <td width="5%">
			    <asp:ListBox id="lstBrowse" runat="server" AutoPostBack="True" Rows="15"></asp:ListBox>
		    </td>
		    <td valign="top" align="left">
		        <b><u>Results</u></b>
		        <br />
                <uc2:PersonDirectoryGrid ID="PersonDirectoryGrid" runat="server" />
				<uc1:CompanyDirectoryGrid id="CompanyDirectoryGrid" runat="server" />
            </td>
	    </tr>
    </table>
</div>