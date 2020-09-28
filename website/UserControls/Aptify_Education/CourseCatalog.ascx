<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CourseCatalog.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.CourseCatalogControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td colspan="2">
                <asp:DropDownList runat="server" ID="cmbCategory" AutoPostBack="true" ToolTip="Select a category from this list to filter the course catalog" />
                &nbsp;
                <asp:CheckBox ID="chkSubCat" runat="server" AutoPostBack="True" Text="Include Sub-Categories"
                    ToolTip="Check this box to include sub-categories of the selected category" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%--   'Navin Prasad Issue 11032--%>
                <%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                       <%--Neha Changes for Issue 14452--%>
                        <rad:RadGrid ID="grdFilteredCourses" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false"/>
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                               <Columns>
                                    <rad:GridTemplateColumn HeaderText="Category" DataField="Category" SortExpression="Category"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Category") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CategoryUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" Font-Italic="True" BackColor="Silver" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Course" DataField="WebName" SortExpression="WebName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CourseUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Scope" DataField="Scope" SortExpression="Scope"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblScope" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Scope") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="10pt"
                                            Font-Strikeout="False" Font-Underline="False" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" SortExpression=""
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                        <HeaderStyle Width="250px" />
                                        <ItemStyle Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblWebDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Units" DataField="Units" SortExpression="Units"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnits" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Units") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn DataField="TotalPartDuration" HeaderText="Duration" DataFormatString="{0:F0} min"
                                        ItemStyle-Font-Size="12px" AllowFiltering = "false" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%>
               <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <rad:RadGrid ID="grdCourses" runat="server" AutoGenerateColumns="False" AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false"/>
                             <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Category" DataField="Category" SortExpression="Category"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" >
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Category") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CategoryUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" Font-Italic="True" BackColor="Silver" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Course" DataField="WebName" SortExpression="WebName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CourseUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" SortExpression=""
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                         <HeaderStyle Width="250px" />
                                        <ItemStyle Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblWebDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Units" DataField="Units" SortExpression="Units"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnits" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Units") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn DataField="TotalPartDuration" HeaderText="Duration" DataFormatString="{0:F0} min"
                                        ItemStyle-Font-Size="12px" SortExpression="TotalPartDuration" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false"/>
                                    <rad:GridTemplateColumn HeaderText="Duration" DataField="TotalPartDuration" SortExpression="TotalPartDuration"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalPartDuration" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalPartDuration") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                  </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" Text="Error" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server" Height="9px" Visible="False"
        Width="175px"></cc3:AptifyWebUserLogin>
</div>
