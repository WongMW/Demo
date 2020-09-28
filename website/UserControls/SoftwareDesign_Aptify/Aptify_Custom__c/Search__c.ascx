<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/Search__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.MarketPlace.Search__c"
    Debug="true" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 105%;">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="UpPanel" runat="server">
    <ContentTemplate>
        <div class="content-container clearfix">
            <table id="tblDisplay" runat="server" class="data-form">
                <tr id="trSearch" runat="server">
                    <td>
                        <table width="72%">
                            <tr>
                                <td style="width: 20%">
                                    <b>County Contains:</b>
                                </td>
                                <td style="width: 80%">
                                    <asp:TextBox ID="txtCounty" runat="server" CssClass="txtfontfamily"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <b>Listing Name Contains:</b>
                                </td>
                                <td style="width: 80%">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="txtfontfamily"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Vendor Name Contains:</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendor" runat="server" CssClass="txtfontfamily"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Description Contains:</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="txtfontfamily"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Posted In The Last: </b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbRecency" runat="server" Width="90px" CssClass="txtfontfamily">
                                        <asp:ListItem Value="1">1 Day</asp:ListItem>
                                        <asp:ListItem Value="2">2 Days</asp:ListItem>
                                        <asp:ListItem Value="3">3 Days</asp:ListItem>
                                        <asp:ListItem Value="5">5 Days</asp:ListItem>
                                        <asp:ListItem Value="7">1 Week</asp:ListItem>
                                        <asp:ListItem Value="14">2 Weeks</asp:ListItem>
                                        <asp:ListItem Value="30">1 Month</asp:ListItem>
                                        <asp:ListItem Value="60">2 Months</asp:ListItem>
                                        <asp:ListItem Value="90">3 Months</asp:ListItem>
                                        <asp:ListItem Value="180">6 Months</asp:ListItem>
                                        <asp:ListItem Value="365">1 Year</asp:ListItem>
                                        <asp:ListItem Value="730">2 Years</asp:ListItem>
                                        <asp:ListItem Value="1095">3 Years</asp:ListItem>
                                        <asp:ListItem Value="1460">4 Years</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkAti" runat="server" Width="15%" Text="ATI?"></asp:CheckBox>
                                    <asp:CheckBox ID="chkGraduate" runat="server" Width="20%" Text="Graduate?" CssClass="cb">
                                    </asp:CheckBox>
                                    <asp:CheckBox ID="chkSchoolLeaver" runat="server" Width="50%" Text="School Leaver?"
                                        CssClass="cb"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button CssClass="submitBtn" ID="cmdSearch" Text="Search" runat="server"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trResults" runat="server">
                    <td>
                        <asp:Label ID="lblNoResults" runat="server" Visible="False">No Records match the search criteria.</asp:Label>
                        <%--Rashmip Issue 14454--%>
                        <asp:UpdatePanel ID="UppanelGrid" runat="server">
                            <ContentTemplate>
                                <%--Suraj issue 14454 2/8/13  removed three step sorting ,added tooltip --%>
                                <rad:RadGrid ID="grdListings" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                    AllowFilteringByColumn="True" SortingSettings-SortedDescToolTip="Sorted Descending"
                                    SortingSettings-SortedAscToolTip="Sorted Ascending" AllowSorting="true">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowSorting="true" AllowFilteringByColumn="true" AllowNaturalSort="false">
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Vendor" DataField="Company" SortExpression="Company"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                FilterControlWidth="80%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lnkVendorURL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Company") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Listing" DataField="Name" SortExpression="Name"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                FilterControlWidth="80%">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'
                                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <%--Suraj issue 14454 4/5/13  removed HeaderTooltip   --%>
                                            <rad:GridBoundColumn DataField="PlainTextDescription" HeaderText="Description" SortExpression=" "
                                                AllowSorting="false" HeaderTooltip="" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AutoPostBackOnFilter="true" FilterControlWidth="80%" />
                                            <%--Added By Kavita  --%>
                                            <rad:GridTemplateColumn HeaderText="ATI" DataField="ATI__c" SortExpression="ATI__c"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                                FilterControlWidth="80%" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkATI" runat="server" Enabled="false" Checked='<%#Eval("ATI__c") %>' />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Graduate" DataField="Graduate__c" SortExpression="Graduate__c"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                                FilterControlWidth="80%" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkGraduate__c" runat="server" Enabled="false" Checked='<%#Eval("Graduate__c") %>' />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="School Leaver" DataField="SchoolLeaver__c" SortExpression="SchoolLeaver__c"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                                FilterControlWidth="80%" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkSchoolLeaver__c" runat="server" Enabled="false" Checked='<%#Eval("SchoolLeaver__c")%>' />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </rad:RadGrid>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <cc1:WebUserActivity ID="WebUserActivity1" runat="server" WebModule="MarketPlace" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
