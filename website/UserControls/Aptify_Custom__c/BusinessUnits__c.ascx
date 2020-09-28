<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BusinessUnits__c.ascx.vb"
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
                <div class="main-container" style="width: 1000px;">
                    <div>
                        <div class="row-div">
                            <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
                        </div>
                        <div class="row-div">
                            <div class="row-div clearfix">
                                <div style="width: 10%;" class="field-div1 w650">
                                    &nbsp;
                                </div>
                                <div style="width: 75%;" class="field-div1 w650">
                                    <div>
                                        <div style="width: 100%;">
                                            <div>
                                                <br />
                                            </div>
                                            <div id="divText" style="font-weight: bold;" runat="server">
                                            </div>
                                            <div id="divRemoveText" style="font-weight: bold; color: Red;" runat="server">
                                            </div>
                                            <div>
                                                <br />
                                            </div>
                                            <div>
                                                <telerik:RadGrid ID="radBusinessUnit" runat="server" AllowPaging="true" PageSize="10"
                                                    AllowSorting="False" AllowFilteringByColumn="False" CellSpacing="0" GridLines="None"
                                                    AutoGenerateColumns="false" Visible="true" Style="margin-top: 13px; overflow: auto"
                                                    Height="350px" Width="50%" ShowHeadersWhenNoRecords="true">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                                                        </Scrolling>
                                                    </ClientSettings>
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
                                                            <telerik:GridTemplateColumn HeaderText="Business Unit" DataField="BusinessUnit" AutoPostBackOnFilter="false"
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                                                HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBU" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BusinessUnit") %>'></asp:Label>
                                                                    <asp:TextBox ID="txtBU" MaxLength="50" Width="250px" ValidationGroup="s" runat="server"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="s" runat="server"
                                                                        ControlToValidate="txtBU" ErrorMessage="Business Unit Required" Display="Dynamic"
                                                                        CssClass="required-label"></asp:RequiredFieldValidator>
                                                                    <asp:HiddenField ID="hidBusinessUnit" Value='<%# DataBinder.Eval(Container.DataItem,"BusinessUnitID") %>'
                                                                        runat="server" />
                                                                    <asp:HiddenField ID="hidstdAssign" Value='<%# DataBinder.Eval(Container.DataItem,"stdAssign") %>'
                                                                        runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                                DataField="stdAssign" FilterControlWidth="100%" HeaderText="Student Assigned"
                                                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="stdAssign"
                                                                HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="left">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Add/Remove" AutoPostBackOnFilter="false"
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="ColRemove"
                                                                HeaderStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnRemove" ValidationGroup="s" runat="server" Text='<%# Eval("Command")%>'
                                                                        CommandName='<%# Eval("Command")%>' OnClick="btnAddRemove_Click" CommandArgument='<%# Eval("Command")%>'
                                                                        CssClass="submitBtn" />
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
                                            <div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <br />
                        <br />
                    </div>
                    <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Modal="True"
                        BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="View/Edit Business Unit" Behavior="None" Height="150px">
                        <ContentTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                                padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblValidationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                                        <asp:HiddenField ID="hidBU" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <asp:Button ID="btnValidationOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" class="submitBtn" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </telerik:RadWindow>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </telerik:RadAjaxPanel>
</div>
