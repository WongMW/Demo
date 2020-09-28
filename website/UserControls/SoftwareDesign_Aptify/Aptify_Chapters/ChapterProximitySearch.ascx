<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Chapters/ChapterProximitySearch.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Chapters.ChapterProximitySearchControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="PageSecurity" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Panel ID="pnlsearchchapterproximity" runat="server" DefaultButton="cmdSearch">
    <div class="content-container clearfix">
        <table id="Table1" cellspacing="1" cellpadding="1" border="0">
            <tr>
                <td align="center" colspan="2">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" width="150px" style="padding-left: 10px;">
                    <%-- <font size="2"><b>Locate Chapters Within</b></font>--%>
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Locate Chapters Within"></asp:Label></b>
                </td>
                <td align="left">
                    <asp:DropDownList ID="cmbMiles" DataValueField="ID" DataTextField="WebName" runat="server"
                        Width="192px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" width="150px" style="padding-top: 3px; padding-left: 10px;">
                    <%--<font size="2"><b>Miles of Zip Code</b></font>--%>
                    <b>
                        <asp:Label ID="Label2" runat="server" Text="Miles of Zip Code"></asp:Label></b>
                </td>
                <td align="left" style="padding-top: 3px;">
                    <asp:TextBox ID="txtZipCode" runat="server" Width="188px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td align="left" style="width: 28px; padding-top: 5px;">
                    <asp:Button ID="cmdSearch" runat="server" Text="Find Chapters" CssClass="submitBtn"></asp:Button>
                </td>
            </tr>
            <tr>
                <td style="width: 30%; height: 3px" align="right"></td>
                <td style="height: 3px">
                    <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="#C00000" Font-Size="9pt"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr id="trResults" runat="server">
                <td colspan="2">
                    <asp:UpdatePanel ID="updPanelGrid" runat="server">
                        <ContentTemplate>
                            <rad:RadGrid ID="grdProxResults" Width="80%" runat="server" AutoGenerateColumns="False"
                                AllowPaging="true" AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending">
                                <GroupingSettings CaseSensitive="false" />
                                <PagerStyle CssClass="sd-pager" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="Chapter" DataField="Name" SortExpression="Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:HyperLink Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>' ID="lnkName"
                                                    runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>' />
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridBoundColumn DataField="CompanyType" HeaderText="Chapter Type" SortExpression="CompanyType" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" />
                                        <rad:GridNumericColumn DataField="Distance" HeaderText="Distance" SortExpression="Distance" AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="false" />
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <cc2:PageSecurity ID="PageSecurity1" runat="server" WebModule="Chapter Management" />
        <cc1:WebUserActivity ID="WebUserActivity1" runat="server" WebModule="Chapter Management" />
        <cc3:User ID="User1" runat="server" />
    </div>
</asp:Panel>
