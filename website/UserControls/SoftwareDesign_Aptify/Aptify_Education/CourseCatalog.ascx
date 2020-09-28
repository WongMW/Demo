<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/CourseCatalog.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.CourseCatalogControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <div id="tblMain" runat="server" class="data-form cai-table">

        <asp:DropDownList runat="server" ID="cmbCategory" AutoPostBack="true" ToolTip="Select a category from this list to filter the course catalog" />
        &nbsp;
                <asp:CheckBox ID="chkSubCat" runat="server" AutoPostBack="True" Text="Include Sub-Categories"
                    ToolTip="Check this box to include sub-categories of the selected category" />

        <%--   'Navin Prasad Issue 11032--%>
        <%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--Neha Changes for Issue 14452--%>
                <rad:RadGrid ID="grdFilteredCourses" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" CssClass="mobile-table">
                    <PagerStyle CssClass="sd-pager" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Category" DataField="Category" SortExpression="Category"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Category:</span>
                                    <asp:HyperLink ID="lnkCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Category") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CategoryUrl") %>' CssClass="cai-table-data"></asp:HyperLink>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Course" DataField="WebName" SortExpression="WebName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Course:</span>
                                    <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CourseUrl") %>' CssClass="cai-table-data"></asp:HyperLink>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Scope" DataField="Scope" SortExpression="Scope"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Scope:</span>
                                    <asp:Label ID="lblScope" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Scope") %>' CssClass="cai-table-data"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" SortExpression=""
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Description:</span>
                                    <asp:Label ID="lblWebDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>' CssClass="cai-table-data"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Units" DataField="Units" SortExpression="Units"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Units:</span>
                                    <asp:Label ID="lblUnits" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Units") %>' CssClass="cai-table-data"></asp:Label>
                                </ItemTemplate>

                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="TotalPartDuration" HeaderText="Duration" AllowFiltering="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Duration:</span>
                                    <asp:Label ID="lblDuration" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalPartDuration", "{0:F0} min") %>' CssClass="cai-table-data"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>

        <%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%>
        <asp:UpdatePanel ID="updPanelGrid" runat="server">
            <ContentTemplate>
                <rad:RadGrid ID="grdCourses" runat="server" AutoGenerateColumns="False" AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending">
                    <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Category" DataField="Category" SortExpression="Category"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Category") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CategoryUrl") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" Font-Italic="True" BackColor="Silver" />
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Course" DataField="WebName" SortExpression="WebName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CourseUrl") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" />
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" SortExpression=""
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblWebDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" />
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Units" DataField="Units" SortExpression="Units"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnits" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Units") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" />
                            </rad:GridTemplateColumn>
                            <rad:GridBoundColumn DataField="TotalPartDuration" HeaderText="Duration" DataFormatString="{0:F0} min"
                                ItemStyle-Font-Size="12px" SortExpression="TotalPartDuration" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" />
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

        <asp:Label ID="lblError" runat="server" Text="Error" Visible="False"></asp:Label>

    </div>
    <cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server" Height="9px" Visible="False"></cc3:AptifyWebUserLogin>
</div>
