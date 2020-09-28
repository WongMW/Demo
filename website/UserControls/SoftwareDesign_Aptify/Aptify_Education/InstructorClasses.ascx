<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/InstructorClasses.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.InstructorClassesControl" %>
<%@ Register Src="InstructorValidator.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--Suraj Issue 14452,5/7/13 Remove the in line css which is not used any where --%>
<div class="content-container clearfix">
    <div id="tblMain" runat="server" class="data-form">

        <asp:DropDownList runat="server" ID="cmbType" AutoPostBack="True">
            <asp:ListItem>Current Classes</asp:ListItem>
            <asp:ListItem>Future Classes</asp:ListItem>
            <asp:ListItem>Past Classes</asp:ListItem>
            <asp:ListItem>All Classes</asp:ListItem>
        </asp:DropDownList>


        <asp:UpdatePanel ID="updPanelGrid" runat="server">
            <ContentTemplate>
                <rad:RadGrid ID="grdMyClasses" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                    AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                    SortingSettings-SortedAscToolTip="Sorted Ascending" CssClass="cai-table mobile-table">
                    <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowSorting="true" NoMasterRecordsText="No records to display."
                        AllowNaturalSort="false">
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Course" DataField="WebName" SortExpression="WebName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Course:</span>
                                    <asp:HyperLink ID="lnkWebName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ClassUrl") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Starts" DataField="StartDate" SortExpression="StartDate"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataType="System.DateTime">
                                <ItemTemplate>
                                    <span class="mobile-label">Starts:</span>
                                    <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:MMMM d, yyyy hh:mm tt}")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Ends" DataField="EndDate" SortExpression="EndDate"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataType="System.DateTime">
                                <ItemTemplate>
                                    <span class="mobile-label">Ends:</span>
                                    <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EndDate", "{0:MMMM d, yyyy hh:mm tt}")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>

                            <rad:GridTemplateColumn HeaderText="Type" DataField="Type" SortExpression="Type"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Type:</span>
                                    <asp:Label ID="lblType" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
                <asp:Label runat="server" ID="lblEmptyMsg" Text="No records to display." Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
</div>
