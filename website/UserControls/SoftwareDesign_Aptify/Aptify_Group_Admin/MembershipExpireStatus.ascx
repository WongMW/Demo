<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/MembershipExpireStatus.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MembershipExpireStatus" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<asp:UpdatePanel ID="updatepnl1" runat="server">
    <ContentTemplate>
        <%--Neha Changes for Groupadmin dashboard safari issue 15231,05/16/13--%>
        <div class="blue-content-block half-width">
            <div class="content-block-title">
                <asp:Label ID="Label1" runat="server" Text="Membership Expiration Status"></asp:Label>
            </div>
            <div class="content-block-content">
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Coming 3 Months" Value="90">

                    </asp:ListItem>
                    <asp:ListItem Text="Coming 6 Months" Value="180">

                    </asp:ListItem>
                </asp:DropDownList>

                <rad:RadChart ID="RadChart1" runat="server" ChartTitle-Appearance-Border-PenStyle="Dash"
                    ChartTitle-Appearance-Border-Visible="false" ChartTitle-Appearance-CompositionType="RowImageText"
                    ChartTitle-Appearance-Dimensions-Width="50px" ChartTitle-Marker-ActiveRegion-Tooltip="Hello"
                    Skin="WebBlue" PlotArea-Appearance-Border-Width="0" ChartTitle-Appearance-FillStyle-FillType="Gradient"
                    Legend-Visible="false" Legend-ActiveRegion-Attributes="hello" ChartTitle-Marker-Appearance-FillStyle-MainColor="Gray"
                    Legend-ActiveRegion-Tooltip="hh" ChartTitle-Appearance-FillStyle-MainColor="Green"
                    PlotArea-Appearance-FillStyle-MainColor="#fff" PlotArea-Appearance-FillStyle-SecondColor="#fff"
                    PlotArea-EmptySeriesMessage-TextBlock-Text="No Results Found">
                    <ChartTitle Visible="false" Appearance-Dimensions-Width="10" TextBlock-Appearance-TextProperties-Font="bold"
                        Appearance-FillStyle-MainColor="#fff" Appearance-FillStyle-SecondColor="#fff">
                    </ChartTitle>
                    <Appearance Border-Visible="false" FillStyle-MainColor="white" FillStyle-SecondColor="white">
                    </Appearance>
                    <Series>
                    </Series>
                </rad:RadChart>

                <div class="clearfix">
                    <div id="div1" runat="server" class="colour-chart orange">
                    </div>

                    <div style="float: left;">
                        <asp:HyperLink ID="A1" runat="server" CssClass="LeftChartHyperLink"></asp:HyperLink>
                        <%-- <a id="A1" href="#" runat="server">Going to Expire</a>--%>
                    </div>

                    <div id="div2" runat="server" class="colour-chart green"></div>

                    <div style="float: left;">
                        <asp:HyperLink ID="A2" runat="server" CssClass="LeftChartHyperLink"></asp:HyperLink>
                        <%--<a id="A2" href="#" runat="server">Remains Active</a>--%>
                    </div>
                </div>
            </div>
        </div>
        <%--Neha Changes for Groupadmin dashboard issue,05/16/13--%>
        <div class="blue-content-block half-width ">
            <div class="content-block-title">
                <asp:Label ID="Label2" runat="server" Text="Order Status Summary"></asp:Label>
            </div>

            <div class="content-block-content">
                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true">
                </asp:DropDownList>

                <rad:RadChart ID="RadChart2" runat="server" ChartTitle-Appearance-Border-PenStyle="Dash"
                    ChartTitle-Appearance-Border-Visible="false" ChartTitle-Appearance-CompositionType="RowImageText"
                    ChartTitle-Appearance-Dimensions-Width="50px" ChartTitle-Marker-ActiveRegion-Tooltip="Hello"
                    Skin="WebBlue" PlotArea-Appearance-Border-Width="0" ChartTitle-Appearance-FillStyle-FillType="Gradient"
                    Legend-Visible="false" Legend-ActiveRegion-Attributes="hello" ChartTitle-Marker-Appearance-FillStyle-MainColor="Gray"
                    Legend-ActiveRegion-Tooltip="hh" ChartTitle-Appearance-FillStyle-MainColor="Green"
                    PlotArea-Appearance-FillStyle-MainColor="#fff" PlotArea-Appearance-FillStyle-SecondColor="#fff"
                    PlotArea-EmptySeriesMessage-TextBlock-Text="No Results Found">
                    <ChartTitle Visible="false" Appearance-Dimensions-Width="10" TextBlock-Appearance-TextProperties-Font="bold"
                        Appearance-FillStyle-MainColor="#fff" Appearance-FillStyle-SecondColor="#fff">
                    </ChartTitle>
                    <Appearance Border-Visible="false" FillStyle-MainColor="white" FillStyle-SecondColor="white">
                    </Appearance>
                    <Series>
                    </Series>
                </rad:RadChart>


                <div class="clearfix">
                    <%--Neha Style and css for Groupadmin dashboard issue,05/16/13--%>

                    <div id="div3" runat="server" class="colour-chart orange">
                    </div>

                    <div style="float: left;">
                        <asp:HyperLink ID="lnkParty" runat="server" CssClass="RightChartHyperLink"></asp:HyperLink>
                        <%-- <a id="A1" href="#" runat="server">Going to Expire</a>--%>
                    </div>

                    <div id="div4" runat="server" class="colour-chart green">
                    </div>

                    <div style="float: left;">
                        <asp:HyperLink ID="lnkPaid" runat="server" CssClass="RightChartHyperLink"></asp:HyperLink>
                        <%--<a id="A2" href="#" runat="server">Remains Active</a>--%>
                    </div>

                    <div id="div5" runat="server" class="colour-chart red">
                    </div>

                    <div style="float: left;">
                        <asp:HyperLink ID="lnkUnpaid" runat="server" CssClass="RightChartHyperLink"></asp:HyperLink>
                        <%--<a id="A2" href="#" runat="server">Remains Active</a>--%>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<cc1:User ID="user1" runat="server" />