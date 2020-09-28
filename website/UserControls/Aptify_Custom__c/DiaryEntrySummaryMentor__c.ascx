<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DiaryEntrySummaryMentor__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.DiaryEntrySummaryMentor__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div id="divContent" runat="server">
            <asp:HiddenField ID="hdnStudentID" runat="server" Value="-1" />
            <asp:HiddenField ID="hdnStatus" runat="server" Value="" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            <asp:Panel ID="pnlDetails" runat="server" Visible="true">
                <div class="row-div clearfix">
                    <telerik:RadGrid ID="gvDiarySummary" runat="server" AllowPaging="true" AllowSorting="True"
                        AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AutoGenerateColumns="false"
                        Width="99%" Visible="true" Style="margin-top: 13px; overflow: auto" Height="350px"
                        ShowHeadersWhenNoRecords="true" PageSize="6">
                        <MasterTableView AllowSorting="true" AllowNaturalSort="false" DataKeyNames="DiaryID"
                            EnableNoRecordsTemplate="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                            <NoRecordsTemplate>
                                <asp:Label ID="lblNoRecord" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </NoRecordsTemplate>
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column"
                                Visible="True">
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="5%" HeaderText="" ItemStyle-HorizontalAlign="Left"
                                    ShowFilterIcon="false" AllowFiltering="false" UniqueName="SelectAll">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllDiary" runat="server" AutoPostBack="true" OnCheckedChanged="ToggleSelectedState" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelectDiary" runat="server" />
                                        <asp:HiddenField runat="server" ID="hdDiaryID" Value='<%# Eval("DiaryID")%>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Title" AllowFiltering="true" SortExpression="Title"
                                    AutoPostBackOnFilter="true" DataField="Title" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkTitle" runat="server" ForeColor="Blue" CommandName="Title"
                                            Font-Underline="true" CommandArgument='<%# Eval("DiaryID")%>' Text='<%# Eval("Title")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="StartDate" FilterControlWidth="100%" HeaderStyle-Width="9%" HeaderText="Start Date"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="StartDate"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="9%" />
                                    <ItemStyle HorizontalAlign="center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="EndDate" FilterControlWidth="100%" HeaderStyle-Width="9%" HeaderText="End Date"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="EndDate"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="9%" />
                                    <ItemStyle HorizontalAlign="center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="LearningLevel" FilterControlWidth="100%" HeaderStyle-Width="15%" HeaderText="Learning Level"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="LearningLevel"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle HorizontalAlign="center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="Status" FilterControlWidth="100%" HeaderStyle-Width="16%" HeaderText="Status"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Status"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="16%" />
                                    <ItemStyle HorizontalAlign="center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="ReviewDate" FilterControlWidth="100%" HeaderStyle-Width="9%" HeaderText="Review Date"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="ReviewDate"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="9%" />
                                    <ItemStyle HorizontalAlign="center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu EnableImageSprites="False">
                        </FilterMenu>
                    </telerik:RadGrid><br />
                </div>
                <div class="row-div clearfix" align="right">
                    <asp:Button ID="btnLocknApprove" runat="server" Text="Lock & Approve"></asp:Button>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="Back"></asp:Button>
                </div>
            </asp:Panel>
            &nbsp; &nbsp;
        </div>
        <div>
            <cc1:User ID="User1" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
