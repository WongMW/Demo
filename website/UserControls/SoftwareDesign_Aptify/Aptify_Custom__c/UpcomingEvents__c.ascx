<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/UpcomingEvents__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.UpcomingEvents__c" %>
<%@ Import Namespace="DocumentFormat.OpenXml.Wordprocessing" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="sf_cols latest-news-header ">
    <div runat="server" class="sf_colsOut sf_2cols_1_50">
        <div runat="server" class="sf_colsIn sf_2cols_1in_50">
            <div class="main-title">
                <h2>Upcoming Events</h2>
            </div>
        </div>
    </div>
    <div runat="server" class="sf_colsOut sf_2cols_2_50">
        <div runat="server" class="sf_colsIn sf_2cols_2in_50">
            <div class="view-all-actions">
                <a href="#">
                    <div class="btn view-all">
                        view all
                    </div>
                </a>
            </div>
        </div>
    </div>
</div>
<asp:Repeater ID="repEvents" runat="server">
    <ItemTemplate>
        <div  Class="upcoming-event-wrapper">
        <span class="category-label" style="display: none"><%# Eval("WebCategoryName") %></span>
            <div class="upcoming-event">
                <div class="event-details">
                    <asp:HyperLink CssClass="full-link" ID="lnkEventName" CommandName="EventName" CommandArgument='<% #Eval("ProductID")%>' runat="server"></asp:HyperLink>

                    <div id="trEventImage" runat="server" class="event-image">
                        <asp:Image ID="EventImage" runat="server" />
                    </div>

                    <div runat="server" class="sf_cols">
                        <div class="event-date">
                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                        </div>
                        <div runat="server" class="event-info">
                            <div class="event-title">
                                <asp:HyperLink ID="HyperLink1" runat="server"><%# Eval("WebName")%></asp:HyperLink>
                            </div>
                            <div class="event-location">
                                <em class="fa fa-map-marker"></em>
                                <asp:Label ID="lblPlace" runat="server"></asp:Label>
                            </div>
                            <div id="trEventdesc" runat="server" class="event-description">
                                <p class="event-desc">
                                    <asp:Literal ID="ltrdescription" runat="server"></asp:Literal>
                                </p>
                                <p class="event-message">
                                </p>
                            </div>
                            <div class="event-location-2">
                                <span class="location-key">Location:</span><br>
                                <%# Eval("Venue")%>
                            </div>
                        </div>
                        <div class="event-link">
                            <em class="fa fa-chevron-right fa-2x"></em>
                        </div>
                    </div>

                    <div runat="server" class="sf_cols">
                        <div runat="server" class="sf_colsOut sf_1cols_1_100">
                            <div runat="server" class="sf_colsIn sf_3cols_1in_100">
                                <div class="event-date-2">
                                    <span class="label">Dates:</span>
                                    <!-- dynamic date hidden -->
                                    <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
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
            <div class="style-1 button-block">
                <%--<asp:HyperLink ID="linkViewAll" CommandArgument="ViewAllLink" runat="server" class="btn-full-width btn">See More Events</asp:HyperLink>--%>
                <a href="/Event-Calendar" class="btn-full-width btn">See more events</a>
            </div>
        </div>
    </FooterTemplate>
</asp:Repeater>
<br />
<cc2:user runat="Server" id="User1" />

<asp:Panel runat="server" ID="title" Visible="false">
    <headertemplate>
            <table width="90%">
                <tr class="tableHeader">
                    <td class="tableHeaderFont" colspan="2">
                        <asp:Image runat="server" ID="img2" ImageUrl="~/Images/event-icon.png" CssClass="MiddleImage" />
                        <asp:Label runat="server" ID="Label1" Text="Upcoming Events" />
                    </td>
                </tr>
            </table>
        </headertemplate>

    <div class="event-location">
        <asp:Label ID="lblPlace" runat="server"></asp:Label>
    </div>
</asp:Panel>

<script type="text/javascript">
    $(function() {
        $('.upcoming-event-wrapper').each(function () {
            var category = $(this).find('.category-label').text();
            if (category === "CPD Courses") {
                $(this).addClass('style-1');
            } else if (category === "Networking Events ") {
                $(this).addClass('style-2');
            } else {
                $(this).addClass('style-3');
            }

            $(this).find('.event-description p span').each(function() {
                if ($(this).text().length > 160) {
                    var text = $(this).text().substr(0, 160);
                    text += "...";
                    $(this).text(text);
                }
            });
        });

       
    });
</script>