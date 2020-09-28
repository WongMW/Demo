<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/BusinessUnits__c.ascx.vb"
    Inherits="BusinessUnits__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<cc1:User runat="server" ID="User1" />
<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>
<div>
    <%--<input id="hdnBUState" value="1" type="hidden" />--%>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</div>
<div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:UpdatePanel ID="UpAreasOfExp" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="main-container">
                    <div>
                        <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
                    </div>
                    <div>
                        <div id="divText" runat="server">
                        </div>
                        <div id="divRemoveText" style="color: Red;" runat="server">
                        </div>
                        <telerik:RadGrid ID="radBusinessUnit" runat="server" AllowPaging="true" PageSize="10"
                            AllowSorting="False" AllowFilteringByColumn="False" CellSpacing="0" GridLines="None"
                            AutoGenerateColumns="false" Visible="true" ShowHeadersWhenNoRecords="true" CssClass="cai-table mobile-table">
                          
                            <MasterTableView AllowSorting="false" AllowNaturalSort="false" EnableNoRecordsTemplate="true"
                                AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                <NoRecordsTemplate>
                                    <asp:Label ID="lblNoRecord" runat="server" Text="No Record Found" Font-Bold="true"
                                        ForeColor="Red"></asp:Label>
                                </NoRecordsTemplate>
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column"
                                    Visible="True">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridTemplateColumn headerText="Business Unit" DataField="BusinessUnit" AutoPostBackOnFilter="false"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Business Unit:</span>
                                            <asp:Label ID="lblBU" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"BusinessUnit") %>'></asp:Label>
                                            <asp:TextBox ID="txtBU" MaxLength="50" ValidationGroup="s" CssClass="cai-table-data" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="s" runat="server"
                                                ControlToValidate="txtBU" ErrorMessage="Business Unit Required" Display="Dynamic" ForeColor="Red"
                                                CssClass="required-label"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hidBusinessUnit" Value='<%# DataBinder.Eval(Container.DataItem,"BusinessUnitID") %>'
                                                runat="server" />
                                            <asp:HiddenField ID="hidstdAssign" Value='<%# DataBinder.Eval(Container.DataItem,"stdAssign") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        DataField="stdAssign" HeaderText="Student Assigned"
                                        ShowFilterIcon="false" SortExpression="stdAssign">
                                        <ItemTemplate>
                                            <span class="mobile-label">Student Assigned:</span>
                                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "stdAssign")%>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Add/Remove" AutoPostBackOnFilter="false"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="ColRemove">
                                        <ItemTemplate>
                                            <span class="cai-table-data">
                                                <asp:Button ID="btnRemove" ValidationGroup="s" runat="server" Text='<%# Eval("Command")%>'
                                                    CommandName='<%# Eval("Command")%>' OnClick="btnAddRemove_Click" CommandArgument='<%# Eval("Command")%>'
                                                    CssClass="submitBtn" />
                                            </span>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                        </telerik:RadGrid>
                    </div>
                    <telerik:RadWindow ID="radWindowValidation" runat="server" Width="400px" Modal="True"
                        VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="View/Edit Business Unit" Behavior="None" Height="200px">
                        <ContentTemplate>
                                <div class="field-group">
                                    <asp:Label ID="lblValidationMsg" runat="server" Text="" CssClass="label-title"></asp:Label>
                                    <asp:HiddenField ID="hidBU" runat="server" />

                                    <div class="actions">
                                        <asp:Button ID="btnValidationOK" runat="server" Text="Ok" class="submitBtn" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="submitBtn" />
                                    </div>
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </telerik:RadAjaxPanel>
</div>
