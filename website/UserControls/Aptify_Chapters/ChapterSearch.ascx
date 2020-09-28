<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChapterSearch.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Chapters.ChapterSearchControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="PageSecurity" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Panel ID="pnlserachchapter" runat="server" DefaultButton="cmdSearch">
    <div class="content-container clearfix">
        <table id="Table1" cellspacing="1" cellpadding="1" border="0">
            <tr>
                <td align="left" width="150px" style="padding-left: 10px;">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Chapter Name Contains"></asp:Label></b>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtName" runat="server" Width="190px" CssClass="txtfontfamily"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="150px" style="padding-top: 3px; padding-left: 10px;">
                    <b>
                        <asp:Label ID="Label2" runat="server" Text="Chapter Type"></asp:Label></b>
                </td>
                <td align="left" style="padding-top: 3px;">
                    <asp:DropDownList ID="cmbCategory" DataValueField="ID" Width="193px" DataTextField="WebName"
                        runat="server" CssClass="txtfontfamily">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td align="left" style="width: 28px; padding-top: 5px;">
                    <asp:Button ID="cmdSearch" runat="server" Text="Find Chapters" CssClass="submitBtn">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td style="width: 30%; height: 3px" align="right">
                </td>
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
                            <rad:RadGrid ID="grdResults" Width="80%" runat="server" AutoGenerateColumns="False"
                                AllowPaging="true" AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip ="Sorted Ascending">
                                 <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="Chapter" DataField="Name" SortExpression="Name"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:HyperLink Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>' ID="lnkName"
                                                    runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>' />
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridBoundColumn DataField="CompanyType" HeaderText="Chapter Type" SortExpression="CompanyType"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%"/>
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
