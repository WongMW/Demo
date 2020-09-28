<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/ManageMyGroup__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_ManageMyGroup__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="ColorDivBGAdmin aptify-category-side-nav">
    <h6>Menu</h6>
    <table>
        <tr>
            <td>
                
                <asp:HyperLink ID="lnkAddMember" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-plus"></i>Add Members</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkPurchaseMembership" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-users"></i>Purchase Membership</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkRenewMembership" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-repeat"></i>Renew Membership</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkCompanyProfile" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-info"></i>Company Info</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkCmpDirectory" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-book"></i>Company Directory</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkOrderHistory" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-file-o"></i>Order History</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkPayOffOrder" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-cc-visa"></i>Pay Off Orders</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkEventRegistration" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-calendar"></i>Event Registration</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkMeetingAttendee" runat="server" CssClass="colorLinkAdmin"> <i class="fa fa-random"></i>Substitute Attendee</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkMeetingTransfer" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-exchange"></i>Meeting Transfer</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkWhoPays" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-user"></i>Who Pays</asp:HyperLink>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <asp:Image ID="ImgBigFirm" runat="server" ImageUrl="~/Images/BigFirm.png" />
            </td>
            <td>
                <asp:HyperLink ID="lnkBigFirm" runat="server" CssClass="colorLinkAdmin">Big Firm</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lnkFirmResult" runat="server" CssClass="colorLinkAdmin"><i class="fa fa-area-chart"></i>Student Result Page</asp:HyperLink>
                <asp:Image ID="imgStudentResultPage" runat="server" ImageUrl="~/Images/FirmResult.png" />
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td colspan="2">
                <asp:Image ID="Image26" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%" Height="2px" />
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <asp:HyperLink ID="lnkFirmChangeSessionToAutumn" runat="server" CssClass="colorLinkAdmin">Change Exam Session</asp:HyperLink>
                <asp:Image ID="imgFirmChangeSessionToAutumn" runat="server" ImageUrl="~/Images/FirmChangeSessionToAutumn.png" />
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td colspan="2">
                <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>

        <tr runat="server" visible="false">
            <td>
                <div>
                    <cc2:User ID="User1" runat="server" />
                </div>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript">
    jQuery(function ($) {
        $('.ColorDivBGAdmin table img').hide();
    });
</script>