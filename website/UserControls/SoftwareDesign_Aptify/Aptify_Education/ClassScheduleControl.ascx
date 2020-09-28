<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/ClassScheduleControl.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.ClassScheduleControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="cai-table">
    <table runat="server" id="tblSchedule" class="data-form">
        <tbody>
            <tr>
                <th class="SmallHeader">Category</th>
                <th class="SmallHeader">Course</th>
                <th class="SmallHeader">Date</th>
                <th class="SmallHeader">Type</th>
                <th runat="server" id="tdLocationHeader" class="SmallHeader">Location</th>
                <th runat="server" id="tdInstructorHeader" class="SmallHeader">Instructor</th>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList Font-Size="9pt" ID="cmbCategory" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:DropDownList Font-Size="9pt" ID="cmbCourse" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:DropDownList Font-Size="9pt" ID="cmbStartDate" runat="server" AutoPostBack="true">
                        <asp:ListItem Selected="true" Value="0" Text="Anytime"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Next 30 Days"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Next 2 months"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Next 3 months"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Next 6 months"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Next 9 months"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Next 12 months"></asp:ListItem>
                        <asp:ListItem Value="18" Text="Next 18 months"></asp:ListItem>
                        <asp:ListItem Value="24" Text="Next 24 months"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList Font-Size="9pt" ID="cmbType" runat="server" AutoPostBack="true" />
                </td>
                <td runat="server" id="tdLocationCombo">
                    <asp:DropDownList Font-Size="9pt" ID="cmbLocation" runat="server" AutoPostBack="true" />
                </td>
                <td runat="server" id="tdInstructorCombo">
                    <asp:DropDownList Font-Size="9pt" ID="cmbInstructor" runat="server" AutoPostBack="true" />
                </td>
            </tr>
        </tbody>
    </table>
</div>
<cc1:User ID="User1" runat="server"></cc1:User>

<script type="text/javascript">
    jQuery(function ($) {
        $('#<%= tblSchedule.ClientID %> td').each(function () {
            $(this).attr('style', '');
            $(this).find('img').remove();

            if ($(this).attr('bgcolor') === "PaleGoldenrod") {
                $(this).attr('bgcolor', '');
                $(this).attr('style', 'background-color: #8C1D40; color: #fff; padding: 10px;');
            }
        });
    });
</script>
