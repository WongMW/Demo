<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/Curriculum.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.CurriculumControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>

<div class="content-container clearfix cai-table">
    <asp:Label ID="lblError" runat="server" Text="Error" Visible="False"></asp:Label><br />
    <div id="tblMain" runat="server">
        <span class="label-title">Select from the lists below and click "Show Curriculum" to view your status against the requirements.</span>

        <div class="double-select with-button">
            <asp:DropDownList runat="server" ID="cmbCategory" AutoPostBack="true" ToolTip="Select a category from this list to filter the course catalog" />
            <asp:DropDownList runat="server" ID="cmbCurriculum" AutoPostBack="false" ToolTip="Select a Curriculum from this list to display the course requirements" />
            <asp:Button ID="btnLoadCurriculum" runat="server" Text="Show Curriculum" CssClass="submitBtn" />
        </div>

        <div class="cai-form headerform">
            <span class="form-title">Curriculum</span>
            <asp:Table ID="tblCurriculum" runat="server">
            </asp:Table>
        </div>

    </div>
    <cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server" Height="9px" Visible="False" Width="175px"></cc3:AptifyWebUserLogin>
</div>

<script type="text/javascript">
    jQuery(function ($) {
        if ($('#<%=tblCurriculum.ClientID%> tr').length <= 0) {
            $('.headerform').hide();
        }
    });
</script>