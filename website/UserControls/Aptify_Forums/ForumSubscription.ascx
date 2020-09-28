<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ForumSubscription.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Forums.ForumSubscription" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <asp:UpdatePanel ID="updatePanelHeader" runat="server">
        <ContentTemplate>
            <table width="100%" class="data-form">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkEnableAll" runat="server" OnCheckedChanged="chkEnableAll_CheckedChanged"
                            AutoPostBack="true" />
                        <asp:Label ID="lblEnable" runat="server" Font-Size="9pt" Text="Enable All Forum Subscriptions"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblResults" runat="server"></asp:Label>&nbsp;<asp:HyperLink runat="server"
                            ID="lnkForumsHome" Text="Forums Home" Visible="false"></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
    <div>
        <asp:UpdatePanel ID="updPanelGrid" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Button CssClass="submitBtn" ID="cmdSave" runat="server" Text="Save Changes">
                </asp:Button>
                <%--Suraj issue 14455 2/20/13  removed three step sorting ,added tooltip and added date column--%>
                <rad:RadGrid ID="grdForumSubscriptions" runat="server" AutoGenerateColumns="False" DataKeyNames="Name"
                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                    AllowPaging="true" CssClass="data-form" AllowFilteringByColumn="True" AllowSorting="true"
                    AutoPostBackOnFilter="true"> <%--Sandeep issue 14671 2/27/13 Allow Paging true--%>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowSorting="true" AllowNaturalSort="false" AllowFilteringByColumn="true">
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Subscription" AllowFiltering="false" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSubscription" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </rad:GridTemplateColumn>
                            <rad:GridBoundColumn DataField="Name" HeaderStyle-ForeColor="White" HeaderText="Name"
                                FilterControlWidth="80%" SortExpression="Name" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                            <rad:GridBoundColumn DataField="Parent" HeaderStyle-ForeColor="White" HeaderText="Category"
                                FilterControlWidth="80%" SortExpression="Parent" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                            <rad:GridTemplateColumn HeaderText="Delivery Type" AllowFiltering="false" FilterControlWidth="80%">
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDeliveryType" runat="server" Visible="false" Text='<%# Eval("DeliveryType") %>'></asp:Label>
                                    <asp:DropDownList ID="ddlDeliveryType" runat="server" Width="98px">
                                        <asp:ListItem>Daily Digest</asp:ListItem>
                                        <asp:ListItem>Realtime</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Forum Type" AllowFiltering="false" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <asp:Image ID="imgForumType" runat="server" Height="31px" Width="29px"></asp:Image>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="ID" SortExpression="ID" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <asp:Label ID="lblforumID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <table width="100%" class="data-form">
        <asp:Label ID="lblEndDate" runat="server" Font-Size="12pt" Font-Bold="true" Text="End Date (mm/dd/yyyy)(optional)"
            Visible="false"></asp:Label>
        <asp:TextBox ID="txtboxendDate" runat="server" Width="178px" Visible="false"></asp:TextBox>
        <cc1:User ID="User1" runat="server" />
    </table>
</div>
