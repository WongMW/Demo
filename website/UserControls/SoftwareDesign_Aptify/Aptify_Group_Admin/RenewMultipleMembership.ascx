<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/RenewMultipleMembership.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.RenewMultipleMembership" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix cai-table renew-multiple">
    <asp:UpdatePanel ID="upnlMain" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="page-message"></div>

            This page displays active memberships that are available for renewal. You can also
                        enable or disable Auto Renewal for a particular membership from this screen.
                        <br />
            Please note that you cannot renew memberships that currently have Auto Renewal enabled,
                        since these memberships will automatically renew on their expiration date.
                        <br />
            Also, you cannot enable Auto Renewal for a membership that is past due. To enable
                        auto renewal in this scenario, you should use the Renew Membership option to renew
                        the membership now and you can enable auto renewal for the future during the check-out
                        process.                 
                </div>
            <br />
            <div id="tblmember" runat="server" width="100%" class="mobile-table">
               
                        <telerik:RadGrid ID="radGridMembership" Width="95%" runat="server"
                            AutoGenerateColumns="False" GridLines="None" CellSpacing="0" AllowPaging="True"
                            AllowSorting="True" AllowFilteringByColumn="True" OnItemCreated="radGridMembership_GridItemCreated"
                            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <PagerStyle CssClass="sd-pager" />
                            <MasterTableView DataKeyNames="ID" AllowFilteringByColumn="true" AllowSorting="true"
                                NoMasterRecordsText="No Memberships Available." AllowNaturalSort="false" ClientDataKeyNames="ID">
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" Visible="false"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSubscriber" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="ID" HeaderText="ID" SortExpression="ID"
                                        FilterControlWidth="50px" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">ID:</span>
                                            <asp:Label ID="lblSubscriptionID" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Name" DataField="Recipient" SortExpression="Recipient"
                                        AutoPostBackOnFilter="true" FilterControlWidth="210px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="member-profile-td"></ItemStyle>
                                        <ItemTemplate>                                          
                                            <div class="imgmember">
                                                <asp:Label runat="server" Text="Profile Image:" CssClass="mobile-label"></asp:Label>
                                                <%-- Neha,issue 14810,03/09/13,added Radbinaryimage --%>
                                                <rad:RadBinaryImage ID="imgmember" runat="server" CssClass="PeopleImage cai-table-data" AutoAdjustImageControlSize="false" ResizeMode="Fill"></rad:RadBinaryImage>
                                            </div>

                                            <div class="member-profile-details">
                                                  <span class="mobile-label">Name:</span>
                                                <asp:Label ID="lblMember" CssClass="namelink cai-table-data member-name-label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Recipient") %>'></asp:Label>

                                                <asp:Label ID="lblMemberTitle" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Label>

                                                 <asp:Label runat="server" Text="Location:" CssClass="mobile-label "></asp:Label>
                                                <asp:Label ID="lblCity" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"City") %>'> </asp:Label>

                                                <asp:Label ID="lblCountry" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Country") %>'> </asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="Product" UniqueName="MembershipType" FilterControlWidth="160px"
                                        HeaderText="Membership Product" SortExpression="Product" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Membership Product:</span>
                                            <asp:Label ID="lblMembershipType" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Product") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Expiration Date" SortExpression="ExpiryDate"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        FilterControlWidth="100px" DataType="System.DateTime" DataField="ExpiryDate"
                                          UniqueName="GridDateTimeColumnStartDate" FilterControlToolTip="Enter a filter date" >
                                         <ItemTemplate>
                                            <span class="mobile-label">Expiration Date:</span>
                                            <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("ExpiryDate", "{0:MMMM dd, yyyy}")%>'> </asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="AutoRenewal" HeaderText="Auto Renewal" FilterControlWidth="100px"
                                        ShowFilterIcon="false" AllowFiltering="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Auto Renewal:</span>
                                            <asp:Label ID="lblOnOff" CssClass="cai-table-data" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="Status" UniqueName="Status" HeaderText="Status"
                                        AllowFiltering="false" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Status:</span>
                                            <table class="cai-table-data">
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="imgStatus" Font-Size="11px" Width="10px" BorderStyle="None" Height="10px"
                                                            runat="server" />
                                                        <asp:Label ID="lblstatus" runat="server"> </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <rad:GridTemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PurchaseType") %>'> </asp:Label>
                                            <asp:Label ID="lblProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'></asp:Label>
                                            <asp:Label ID="lblProduct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Product") %>'></asp:Label>
                                            <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'> </asp:Label>
                                            <asp:Label ID="lblPersonID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RecipientID") %>'></asp:Label>
                                            <asp:Label ID="lblExpiryDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ExpiryDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                <ItemStyle />
                                <NoRecordsTemplate>
                                    No Memberships Available.
                                </NoRecordsTemplate>
                            </MasterTableView>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                        </telerik:RadGrid>
                    
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="actions">
        <asp:Button runat="server" ID="btnRenew" Text="Renew Membership" class="submitBtn"
            CausesValidation="False" />
        <asp:Button ID="btnRenewalOn" Text="Turn Auto Renewal On" runat="server" class="submitBtn"
            CausesValidation="False" />
        <asp:Button ID="btnRenewalOff" Text="Turn Auto Renewal Off" runat="server" class="submitBtn"
            CausesValidation="False" />
    </div>

    <rad:RadWindow ID="radWindowPayment" runat="server" VisibleOnPageLoad="false" Modal="true"
        Skin="Default" Behaviors="Move" Width="435px" Height="325px" Title="Payment Information"
        BackColor="#DADADA" ForeColor="#bda797" VisibleStatusbar="false" IconUrl="">
        <ContentTemplate>
            <div>
                <div>
                    <asp:Label ID="lblMessage" runat="server" Text=" Please Enter Payment Information to Create Standing Orders."></asp:Label>
                    <br />
                    <br />
                    <uc1:CreditCard ID="CreditCard" runat="server" />
                </div>
                <div>
                    <div style="float: right; padding-right: 5px;">
                        <asp:CheckBox ID="chkMakePayment" runat="server" Visible="false" />
                        <asp:Button ID="btnMakePayment" runat="server" Text="Make Payment" CssClass="submitBtn" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submitBtn" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </rad:RadWindow>
    <rad:RadWindow ID="radWindowMessage" runat="server" VisibleOnPageLoad="false" Modal="true"
        Skin="Default" Behaviors="Move" Width="400px" Height="150px" Title="" BackColor="#DADADA"
        ForeColor="#bda797" VisibleStatusbar="false" IconUrl="">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                <tr>
                    <td>
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="submitBtn" CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </rad:RadWindow>
    <cc3:User ID="User1" runat="server" />
    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
</div>
