
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UpcomingEventsRegistrationChart.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.UpcomingEventsRegistrationChart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="rad" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div style="margin-left: 21px; border: 1px solid #E0E0E0;border-radius: 5px 5px 2px 2px; width:569px;">
<table width="569px" style="border: 0px; background-color: #F9F9F9; border-collapse: collapse;"
    cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="ChartTitle">
             Upcoming Events - Registration Summary</td>
    </tr>
    <tr>
        <td style="background-color: #F9F9F9; height: 10px; ">
        </td>
    </tr>
    <tr>
        <td style="background-color: #F9F9F9; text-align: left;">
            <div style="text-align: left; padding:  0 0 0 0px; margin: 0 0 0 0px;">
                <div style="float:left; width: 132px; padding:  0 0 0 19px; margin: 0 0 0 19px;">
                    <asp:DropDownList ID="cmbDate" CssClass="txtBoxEditProfileForDropdown" runat="server"
                        AutoPostBack="True" Width="132px">
                    </asp:DropDownList> 
                </div>
                <div style="float:left; width: 191px; padding: 0 0 0 0; margin: 0 0 0 20px;">
                    <asp:DropDownList ID="cmbEvent" CssClass="txtBoxEditProfileForDropdown" runat="server"
                        AppendDataBoundItems="True" AutoPostBack="True" Width="191px">
                        <asp:ListItem Value="0">------Select Events------</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div> 
        </td>
    </tr>
    <tr>
        <td style="height: 13px"></td>
    </tr>
    <tr>
        <td style="background-color: #F9F9F9;">
            <rad:RadChart ID="radChart" runat="server" DefaultType="StackedBar" PlotArea-EmptySeriesMessage-TextBlock-Text="No Upcoming Events" 
                Skin="Telerik" Height="190px" Width="550px" PlotArea-YAxis-AxisMode="Extended">
                <Appearance Corners="Rectangle, Rectangle, Rectangle, Rectangle, 0">
                    <FillStyle FillType="Image" MainColor="249, 249, 249" SecondColor="249, 249, 249">
                        <FillSettings GradientMode="Horizontal">
                            <ComplexGradient>
                                <rad:GradientElement Color="236, 236, 236"></rad:GradientElement>
                                <rad:GradientElement Color="248, 248, 248" Position="0.5"></rad:GradientElement>
                                <rad:GradientElement Color="236, 236, 236" Position="1"></rad:GradientElement>
                            </ComplexGradient>
                        </FillSettings>
                    </FillStyle>
                    <Border Color="130, 130, 130" Visible="False" />
                </Appearance>
<Series>
<rad:ChartSeries Name="Series 1">
<Appearance>
<FillStyle FillType="ComplexGradient">
<FillSettings><ComplexGradient>
<rad:GradientElement Color="213, 247, 255"></rad:GradientElement>
<rad:GradientElement Color="193, 239, 252" Position="0.5"></rad:GradientElement>
<rad:GradientElement Color="157, 217, 238" Position="1"></rad:GradientElement>
</ComplexGradient>
</FillSettings>
</FillStyle>

<TextAppearance TextProperties-Color="51, 51, 51"></TextAppearance>
</Appearance>
</rad:ChartSeries>
<rad:ChartSeries Name="Series 2">
<Appearance>
<FillStyle FillType="ComplexGradient">
<FillSettings><ComplexGradient>
<rad:GradientElement Color="218, 254, 122"></rad:GradientElement>
<rad:GradientElement Color="198, 244, 80" Position="0.5"></rad:GradientElement>
<rad:GradientElement Color="153, 205, 46" Position="1"></rad:GradientElement>
</ComplexGradient>
</FillSettings>
</FillStyle>

<TextAppearance TextProperties-Color="51, 51, 51"></TextAppearance>

<Border Color="111, 174, 12"></Border>
</Appearance>
</rad:ChartSeries>
</Series>

                <Legend>
                    <Appearance Dimensions-Margins="0px, 3%, 1px, 1px" Dimensions-Paddings="0px, 8px, 6px, 3px"
                        Position-AlignedPosition="TopRight">
                        <ItemMarkerAppearance Figure="Square">
                            <Border Width="0" />
                        </ItemMarkerAppearance>
                        <FillStyle MainColor="">
                        </FillStyle>
                        <Border Width="0" />
                    </Appearance>
                </Legend>
                <PlotArea>
                    <EmptySeriesMessage>
                        <TextBlock Text="No Upcoming Events">
                        </TextBlock>
                    </EmptySeriesMessage>
                    <XAxis AutoScale="False" MaxValue="7" MinValue="1" Step="1">
                        <Appearance Color="182, 182, 182" MajorTick-Color="216, 216, 216" MajorTick-Visible="False">
                            <MajorGridLines Color="216, 216, 216" PenStyle="Solid" Visible="False" />
                            <TextAppearance TextProperties-Color="51, 51, 51" AutoTextWrap="True" 
                                Dimensions-AutoSize="False" Dimensions-Height="0px" Dimensions-Width="50px">
                            </TextAppearance>
                        </Appearance>
                        <AxisLabel>
                            <TextBlock>
                                <Appearance TextProperties-Color="51, 51, 51">
                                </Appearance>
                            </TextBlock>
                        </AxisLabel>
<Items>
<rad:ChartAxisItem Value="1"></rad:ChartAxisItem>
<rad:ChartAxisItem Value="2"></rad:ChartAxisItem>
<rad:ChartAxisItem Value="3"></rad:ChartAxisItem>
<rad:ChartAxisItem Value="4"></rad:ChartAxisItem>
<rad:ChartAxisItem Value="5"></rad:ChartAxisItem>
<rad:ChartAxisItem Value="6"></rad:ChartAxisItem>
<rad:ChartAxisItem Value="7"></rad:ChartAxisItem>
</Items>
                    </XAxis>
                    <YAxis>
                        <Appearance Color="182, 182, 182" MajorTick-Color="216, 216, 216" MinorTick-Color="223, 223, 223"
                            MajorTick-Visible="False" MinorTick-Visible="False">
                            <MajorGridLines Color="216, 216, 216" Visible="False" />
                            <MinorGridLines Color="223, 223, 223" Visible="False" />
                            <TextAppearance TextProperties-Color="51, 51, 51">
                            </TextAppearance>
                        </Appearance>
                        <AxisLabel>
                            <TextBlock>
                                <Appearance TextProperties-Color="51, 51, 51">
                                </Appearance>
                            </TextBlock>
                        </AxisLabel>
                    </YAxis>
                    <YAxis2 Visible="False">
                        <Appearance MajorTick-Visible="False" MinorTick-Visible="False">
                            <LabelAppearance Visible="False">
                            </LabelAppearance>
                        </Appearance>
                    </YAxis2>
                    <Appearance Dimensions-Margins="5px, 170px, 56px, 38px">
                        <FillStyle FillType="Solid" MainColor="White">
                        </FillStyle>
                        <Border Color="182, 182, 182" />
                    </Appearance>
                </PlotArea>
                <ChartTitle Visible="False">
                    <Appearance Visible="False">
                        <FillStyle MainColor="">
                        </FillStyle>
                    </Appearance>
                    <TextBlock Text="">
                        <Appearance TextProperties-Color="72, 174, 40" TextProperties-Font="Arial, 18pt">
                        </Appearance>
                    </TextBlock>
                </ChartTitle>
            </rad:RadChart>
        </td>
    </tr>
</table>
</div>
<cc1:User ID="user1" runat="server" />
