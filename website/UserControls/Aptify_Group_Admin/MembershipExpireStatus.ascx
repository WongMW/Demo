<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MembershipExpireStatus.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MembershipExpireStatus" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .RadChart
    {
        border: none !important;
    }
</style>
<asp:UpdatePanel ID="updatepnl1" runat="server">
    <ContentTemplate><%--Neha Changes for Groupadmin dashboard safari issue 15231,05/16/13--%>
        <div style="float: left; margin-left: 20px; width: 275px;">
           <table width="100%" style="border: 1px solid #E0E0E0;">
                <tr style="background-color: #e0e0e0;">
                    <td colspan="2" class="ChartTitle">
                        <asp:Label ID="Label1" runat="server" Text="Membership Expiration Status" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
               
                    <td colspan="2" style="background-color: #fafafa; padding-top: 10px; padding-left: 6px; width:275px;">
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="Coming 3 Months" Value="90">

                            </asp:ListItem>
                            <asp:ListItem Text="Coming 6 Months" Value="180">

                            </asp:ListItem>
                        </asp:DropDownList>
                    </td>
     
                </tr>
                <tr>
                    <td style="background-color: #fafafa;">
                        <rad:RadChart ID="RadChart1" runat="server" AutoLayout="true" ChartTitle-Appearance-Border-PenStyle="Dash"
                            ChartTitle-Appearance-Border-Visible="false" ChartTitle-Appearance-CompositionType="RowImageText"
                            ChartTitle-Appearance-Dimensions-Width="50px" ChartTitle-Marker-ActiveRegion-Tooltip="Hello"
                            Skin="WebBlue" PlotArea-Appearance-Border-Width="0" ChartTitle-Appearance-FillStyle-FillType="Gradient"
                            Legend-Visible="false" Legend-ActiveRegion-Attributes="hello" ChartTitle-Marker-Appearance-FillStyle-MainColor="Gray"
                            Legend-ActiveRegion-Tooltip="hh" ChartTitle-Appearance-FillStyle-MainColor="Green"
                            PlotArea-Appearance-FillStyle-MainColor="#fafafa" PlotArea-Appearance-FillStyle-SecondColor="#fafafa"
                            PlotArea-EmptySeriesMessage-TextBlock-Text="No Results Found">
                            <ChartTitle Visible="false" Appearance-Dimensions-Width="10" TextBlock-Appearance-TextProperties-Font="bold"
                                Appearance-FillStyle-MainColor="#e0e0e0" Appearance-FillStyle-SecondColor="#e0e0e0">
                            </ChartTitle>
                            <Appearance Border-Visible="false" FillStyle-MainColor="white" FillStyle-SecondColor="white">
                            </Appearance>
                            <Series>
                            </Series>
                        </rad:RadChart>
                    </td>
                    <td style="background-color: #fafafa; position: absolute;">
                        <div><%--Neha Changes for Groupadmin dashboard issue 15231,05/16/13--%>
                            <table style="margin-left: -125px; margin-top:40px;">
                                <tr>
                                    <td>
                                        <div id="div1" runat="server" style="width: 8px; height: 8px; border: 1px solid orange;
                                            float: left; background-color: orange;">
                                        </div>
                                    </td>
                                    <td >
                                        <div style="float: left;">
                                            <asp:HyperLink ID="A1" runat="server" CssClass="LeftChartHyperLink"></asp:HyperLink>
                                            <%-- <a id="A1" href="#" runat="server">Going to Expire</a>--%></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="div2" runat="server" style="width: 8px; height: 8px; border: 1px solid Green;
                                            float: left; background-color: Green;">
                                        </div>
                                    </td>
                                    <td >
                                        <div style="float: left;">
                                            <asp:HyperLink ID="A2" runat="server" CssClass="LeftChartHyperLink"></asp:HyperLink>
                                            <%--<a id="A2" href="#" runat="server">Remains Active</a>--%></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div><%--Neha Changes for Groupadmin dashboard issue,05/16/13--%>
        <div style="float: left; margin-left: 20px; width: 275px;">
            <table width="100%" style="border: 1px solid #E0E0E0;">
                <tr style="background-color: #e0e0e0;">
                    <td colspan="2" class="ChartTitle">
                        <asp:Label ID="Label2" runat="server" Text="Order Status Summary" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="background-color: #fafafa; padding-top: 10px; padding-left: 6px;">
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #fafafa;">
                        <rad:RadChart ID="RadChart2" runat="server" AutoLayout="true" ChartTitle-Appearance-Border-PenStyle="Dash"
                            ChartTitle-Appearance-Border-Visible="false" ChartTitle-Appearance-CompositionType="RowImageText"
                            ChartTitle-Appearance-Dimensions-Width="50px" ChartTitle-Marker-ActiveRegion-Tooltip="Hello"
                            Skin="WebBlue" PlotArea-Appearance-Border-Width="0" ChartTitle-Appearance-FillStyle-FillType="Gradient"
                            Legend-Visible="false" Legend-ActiveRegion-Attributes="hello" ChartTitle-Marker-Appearance-FillStyle-MainColor="Gray"
                            Legend-ActiveRegion-Tooltip="hh" ChartTitle-Appearance-FillStyle-MainColor="Green"
                            PlotArea-Appearance-FillStyle-MainColor="#fafafa" PlotArea-Appearance-FillStyle-SecondColor="#fafafa"
                            PlotArea-EmptySeriesMessage-TextBlock-Text="No Results Found">
                            <ChartTitle Visible="false" Appearance-Dimensions-Width="10" TextBlock-Appearance-TextProperties-Font="bold"
                                Appearance-FillStyle-MainColor="#e0e0e0" Appearance-FillStyle-SecondColor="#e0e0e0">
                            </ChartTitle>
                            <Appearance Border-Visible="false" FillStyle-MainColor="white" FillStyle-SecondColor="white">
                            </Appearance>
                            <Series>
                            </Series>
                        </rad:RadChart>
                    </td>
                    <td style="background-color: #fafafa; position: absolute;">
                        <div >
                         <%--Neha Style and css for Groupadmin dashboard issue,05/16/13--%>
                            <table style="margin-left: -125px; margin-top: 35px;">
                                <tr>
                                    <td>
                                        <div id="div3" runat="server" style="width: 8px; height: 8px; border: 1px solid orange;
                                            float: left; background-color: orange;">
                                        </div>
                                    </td>
                                    <td>
                                        <div style="float: left;">
                                            <asp:HyperLink ID="lnkParty" runat="server" CssClass="RightChartHyperLink"></asp:HyperLink>
                                            <%-- <a id="A1" href="#" runat="server">Going to Expire</a>--%></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="div4" runat="server" style="width: 8px; height: 8px; border: 1px solid Green;
                                            float: left; background-color: Green;">
                                        </div>
                                    </td>
                                    <td>
                                        <div style="float: left;">
                                            <asp:HyperLink ID="lnkPaid" runat="server" CssClass="RightChartHyperLink"></asp:HyperLink>
                                            <%--<a id="A2" href="#" runat="server">Remains Active</a>--%></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="div5" runat="server" style="width: 8px; height: 8px; border: 1px solid red;
                                            float: left; background-color: red;">
                                        </div>
                                    </td>
                                    <td>
                                        <div style="float: left;">
                                            <asp:HyperLink ID="lnkUnpaid" runat="server" CssClass="RightChartHyperLink" ></asp:HyperLink>
                                            <%--<a id="A2" href="#" runat="server">Remains Active</a>--%></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<cc1:User ID="user1" runat="server" />
