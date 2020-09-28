<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UpcomingEvents__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.UpcomingEvents__c" %>
       <%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div>
    <asp:Repeater ID="repEvents" runat="server" >
        <HeaderTemplate>
            <table width="90%">
                <tr class="tableHeader">
                    <td class="tableHeaderFont" colspan="2">
                        <asp:Image runat="server" ID="img2" ImageUrl="~/Images/event-icon.png" CssClass="MiddleImage" />
                        <asp:Label runat="server" ID="Label1" Text="Upcoming Events" />
                    </td>
                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table width="90%">
                <tr id="trEventImage" runat="server">
                    <td style="padding-top: 10px">
                        <asp:Image ID="EventImage" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="UpcomingMeetingTitle">
                        <asp:HyperLink ID="lnkEventName" CommandName="EventName"   CommandArgument='<% #Eval("ProductID") %>' runat="server"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="UpcomingMeetingDatePlace">
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="UpcomingMeetingDatePlace">
                        <asp:Label ID="lblPlace" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trEventdesc" runat="server">
                    <td class="UpcomingMeetingDescription">
                        <asp:Literal ID="ltrdescription" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr class="GrayLine" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <FooterTemplate>
            <table width="90%">
                <tr>
                    <td class="tablecontrolsfont">
                        <asp:HyperLink ID="linkViewAll" CommandArgument="ViewAllLink"  runat="server"><div class="ViewAllLink">View All</div></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <br />
       <cc2:User runat="Server" ID="User1" />
</div>
