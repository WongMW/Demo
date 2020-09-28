﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ManageMyGroup__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_ManageMyGroup__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div style="overflow: scroll;" class="ColorDivMGAdmin">
    <table>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="imgAddMember" runat="server" ImageUrl="~/Images/Add_member.png" />
            </td>
            <td style="padding-bottom: 3px; padding-top: 5px;">
                <asp:HyperLink ID="lnkAddMember" runat="server" Text="Add Members" CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="imghr" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image13" runat="server" ImageUrl="~/Images/Purchase_membership.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkPurchaseMembership" runat="server" Text="Purchase Membership"
                    CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image14" runat="server" ImageUrl="~/Images/Renew_membership.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkRenewMembership" runat="server" Text="Renew Membership" CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image12" runat="server" ImageUrl="~/Images/Company_Profile.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkCompanyProfile" runat="server" Text="Company Info" CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image16" runat="server" ImageUrl="~/Images/Company Directory.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkCmpDirectory" runat="server" Text="Company Directory" CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image15" runat="server" ImageUrl="~/Images/Order_history.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkOrderHistory" runat="server" Text="Order History" CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image20" runat="server" ImageUrl="~/Images/Pay off orders.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkPayOffOrder" runat="server" Text="Pay Off Orders " CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image17" runat="server" ImageUrl="~/Images/Event registration.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkEventRegistration" runat="server" Text="Event Registration"
                    CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image18" runat="server" ImageUrl="~/Images/Meeting_Transfer.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkMeetingAttendee" runat="server" Text="Substitute Attendee "
                    CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image19" runat="server" ImageUrl="~/Images/Meeting_Transfer1.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkMeetingTransfer" runat="server" Text="Meeting Transfer " CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image23" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/WhoPays.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkWhoPays" runat="server" Text="Who Pays " CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image24" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/BigFirm.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkBigFirm" runat="server" Text="Course Enrollment" CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image25" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="imgStudentResultPage" runat="server" ImageUrl="~/Images/FirmResult.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkFirmResult" runat="server" Text="Student Result Page" CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image26" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="imgFirmChangeSessionToAutumn" runat="server" ImageUrl="~/Images/FirmChangeSessionToAutumn.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkFirmChangeSessionToAutumn" runat="server" Text="Change Exam Session"
                    CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <%--  <tr>
            <td colspan="2">
                <asp:Image ID="Image21" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="imgFirmCont" runat="server" ImageUrl="~/Images/FirmChangeSessionToAutumn.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkFirmContractRegistration" runat="server" Text="Firm Contract Registration"
                    CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <%--  <tr>
            <td colspan="2">
                <asp:Image ID="Image22" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image21" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="imgQuotaApp" runat="server" ImageUrl="~/Images/quota.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkQuotaApp" runat="server" Text="Quota Applications" CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image27" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="imgFirmAdminDashboard" runat="server" ImageUrl="~/Images/FirmChangeSessionToAutumn.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkFirmAdminDashboard" runat="server" Text="CA Diary Training Manager Dashboard"
                    CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image22" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 3px; padding-top: 5px; padding-left: 12px;">
                <asp:Image ID="imgBU" runat="server" ImageUrl="~/Images/FirmChangeSessionToAutumn.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkBU" runat="server" Text="Business Unit" CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image31" runat="server" ImageUrl="~/Images/Seperator1.png" Width="100%"
                    Height="2px" />
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <cc2:User ID="User1" runat="server" />
                </div>
            </td>
        </tr>
        <%-- <tr>
            <td style="padding-bottom: 3px; padding-top: 5px;">
            <asp:Image ID="Image21" runat="server" ImageUrl="~/Images/Add_member.png" />
            </td>
            <td style="padding-top: 3px; padding-bottom: 3px;">
                <asp:HyperLink ID="lnkRemoveperson" runat="server" Text="Remove Person " CssClass="colorLinkAdmin"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
            <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/Seperator1.png" 
                    Height="2px" Width="36px" />
            </td>
            <td>
                <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/Seperator1.png" Width="151px"
                    Height="2px" />
            </td>
        </tr>--%>
    </table>
</div>
