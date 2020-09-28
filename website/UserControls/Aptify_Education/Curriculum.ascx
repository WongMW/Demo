<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Curriculum.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.CurriculumControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>

<div class="content-container clearfix">
    <asp:Label ID="lblError" runat="server" Text="Error" Visible="False"></asp:Label><br />
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td colspan="3">Select from the lists below and click "Show Curriculum" to view your status against the requirements.</td>
        </tr>
        <tr>
            <td>
    	      <asp:DropDownList runat="server" ID="cmbCategory" AutoPostBack="true" ToolTip="Select a category from this list to filter the course catalog" />
            </td>
            <td>
    	      <asp:DropDownList runat="server" ID="cmbCurriculum" AutoPostBack="false" ToolTip="Select a Curriculum from this list to display the course requirements" /></td>
            <td >
    	
    		<asp:Button ID="btnLoadCurriculum" runat="server" Text="Show Curriculum" CssClass="submitBtn" /></td>
        </tr>
        <tr>
            <td colspan="3">
    <asp:Table ID="tblCurriculum" runat="server">
    </asp:Table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
    </td>
        </tr>
    </table>
    <cc3:AptifyWebUserLogin id="WebUserLogin1" runat="server" Height="9px" Visible="False" Width="175px"></cc3:AptifyWebUserLogin>
</div>