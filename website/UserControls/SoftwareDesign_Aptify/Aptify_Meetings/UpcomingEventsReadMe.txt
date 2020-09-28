
******* Read Me *******

This layout for upcoming events widget is for the homepage - please see ( upcoming-events-homepage.png)




<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Meetings/UpcomingEvents.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.UpcomingEvents" %>
       <%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

    <asp:Repeater ID="repEvents" runat="server" >
        <ItemTemplate>
            <div class="upcoming-event-wrapper style-1">
                <div class="upcoming-event">
                    <div class="event-details">
                        <div id="trEventImage" runat="server">

                            <asp:Image ID="EventImage" runat="server" />

                        </div>

                        <div runat="server" class="sf_cols">
                            <div runat="server" class="sf_colsOut sf_3cols_1_25">
                                <div runat="server" class="sf_colsIn sf_3cols_1in_25">
                                    <div class="event-date">
                                        19 May

                                        <!-- dynamic date hidden -->
                                        <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div class="event-location">
                                        <em class="fa fa-map-marker fa-2x"></em>
                                        ROI
                                    </div>
                                </div>
                            </div>
                            <div runat="server" class="sf_colsOut sf_3cols_2_50">
                                <div runat="server" class="sf_colsIn sf_3cols_2in_50">
                                    <div class="event-title">
                                        <asp:HyperLink ID="lnkEventName" CommandName="EventName" CommandArgument='<% #Eval("ProductID")%>' runat="server"></asp:HyperLink>
                                    </div>
                                    <div id="trEventdesc" runat="server">
                                        <p class="event-desc">

                                            Practice Managment
                                            <asp:Literal ID="ltrdescription" runat="server" Visible="false"></asp:Literal>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" class="sf_colsOut sf_3cols_3_25">
                                <div runat="server" class="sf_colsIn sf_3cols_3in_25">
                                    <div class="event-link">

                                        <em class="fa fa-chevron-right fa-2x"></em>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            <div class="view-all-events">
                    <btn class="btn-action">
                        <asp:HyperLink ID="linkViewAll" CommandArgument="ViewAllLink"  runat="server">See More Events</asp:HyperLink>
                    </btn>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <br />
       <cc2:User runat="Server" ID="User1" />

<asp:Panel runat="server" ID="title" Visible="false">
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
     
                    <div class="event-location">
                        <asp:Label ID="lblPlace" runat="server"></asp:Label>
                    </div>
</asp:Panel>