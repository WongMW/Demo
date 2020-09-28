<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/CADiaryEntries__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.CADiaryEntries__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<style type="text/css">
    .empty
    {
        width: 300px;
        height: 10px;
        background: #ff0000;
    }
</style>
<div class="info-data cai-table">
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    
    <div class="row-div clearfix">
        <div class="field-div1 w100">
<asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
    <ContentTemplate>
        
        <asp:Panel ID="pnlDetails" runat="server" Visible="true">
            <div class="row-div clearfix">
                
                <telerik:RadGrid ID="gvDiaryEntries" runat="server" AllowPaging="false" AllowSorting="True"
                    AllowFilteringByColumn="true" CellSpacing="0" GridLines="None" AutoGenerateColumns="false"
                    Width="99%" Visible="true"  Style="margin-top: 13px; overflow: auto" Height="350px"
                    ShowHeadersWhenNoRecords="true" >
                    <MasterTableView AllowSorting="true" AllowNaturalSort="false" DataKeyNames="StudentID"
                        EnableNoRecordsTemplate="true" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                        <NoRecordsTemplate>
                            <div>
                                 <asp:Label ID="lblNoRecord" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </div>
                        </NoRecordsTemplate>
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column"
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="StartDate" FilterControlWidth="100%" HeaderStyle-Width="10%" HeaderText="Start date"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="StartDate"
                                HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="EndDate" FilterControlWidth="100%" HeaderStyle-Width="10%" HeaderText="End date"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="EndDate"
                                HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="LearningLevel" FilterControlWidth="100%" HeaderStyle-Width="10%" HeaderText="Learning level"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="LearningLevel"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Status" FilterControlWidth="100%" HeaderStyle-Width="15%" HeaderText="Status"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Status"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Title" FilterControlWidth="100%" HeaderStyle-Width="15%" HeaderText="Title"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Title"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <rad:GridTemplateColumn HeaderStyle-Width="4%" HeaderText="Modify" ItemStyle-HorizontalAlign="Left"
                                        ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false">
                                        <ItemTemplate>
                                           <b> <asp:LinkButton ID="lnkEdit" Style="text-decoration: underline;" runat="server" Text="Edit" CommandName="Edit"
                                                CommandArgument='<%# Eval("DiaryID")%>' OnClick="btnEdit_Click"></asp:LinkButton> </b>
                                        </ItemTemplate>
                                        <HeaderStyle Width="4%" />
                                        <ItemStyle HorizontalAlign="center" />
                                    </rad:GridTemplateColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Mentor" FilterControlWidth="100%" HeaderStyle-Width="13%" HeaderText="Mentor"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Mentor"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="13%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Firm" FilterControlWidth="100%" HeaderStyle-Width="18%" HeaderText="Firm"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Firm"
                                HeaderStyle-HorizontalAlign="Center" AllowFiltering="true">
                                <HeaderStyle Width="18%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="EntryRoute" FilterControlWidth="100%" HeaderStyle-Width="15%"
                                HeaderText="Entry route" ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false"
                                SortExpression="EntryRoute" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Days" FilterControlWidth="100%" HeaderStyle-Width="10%" HeaderText="Days"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Days"
                                HeaderStyle-HorizontalAlign="Center" AllowFiltering="false">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                          
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
                <br />
            </div>
        </asp:Panel>
        <div class="row-div clearfix" align="left">
		<div class="button-block style-1">
            		<asp:Button ID="cmdBack" runat="server" Text="Back to CA Diary Dashboard" CssClass="btn-full-width btn"></asp:Button>
		</div>
        </div>
        &nbsp; &nbsp; </div>
        <div>
            <cc1:User ID="User1" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
 </div>
    </div>
</div>
