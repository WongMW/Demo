<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/RenewMultipleSubscription.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.RenewMultipleSubscription" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix cai-table">
    <asp:UpdatePanel ID="upnlMain" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" class="page-message">
                <p>
                    This page displays active subscriptions that are available for renewal. You can
                    also enable or disable Auto Renewal for a particular subscription from this screen.
                    <br />
                    Please note that you cannot renew subscriptions that currently have Auto Renewal
                    enabled, since these subscriptions will automatically renew on their expiration
                    date.
                    <br />
                    Also, you cannot enable Auto Renewal for a subscription that is past due. To enable
                    auto renewal in this scenario, you should use the Renew Subscription option to renew
                    the subscription now and you can enable auto renewal for the future during the check-out
                    process.
                    <br />
                </p>
            </div>
            <table id="tblmember" runat="server" width="100%" class="data-form">
                <tr>
                    <td>
                        <rad:RadGrid ID="grdmember" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            PagerStyle-PageSizeLabelText="Records Per Page" AllowFilteringByColumn="true"
                            OnItemCreated="grdmember_GridItemCreated" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending" CssClass="renew-subscriptions">
                            <GroupingSettings CaseSensitive="false" />
                            <PagerStyle CssClass="sd-pager" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" NoMasterRecordsText="No Subscriptions Available."
                                AllowNaturalSort="false" ClientDataKeyNames="ID">
                                <%-- Suraj issue 15287 4/5/13 , no record found msg displayed so we use "NoRecordsTemplate" --%>
                                <NoRecordsTemplate>
                                    No Subscriptions Available.
                                </NoRecordsTemplate>
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Renew" AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign gridpadding"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Renew:" CssClass="mobile-label"></asp:Label>
                                            <asp:CheckBox ID="chkSubscriber" CssClass="cai-table-data" runat="server" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <rad:GridTemplateColumn Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPersonID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RecipientID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Name" DataField="Recipient" SortExpression="Recipient"
                                        AutoPostBackOnFilter="true" FilterControlWidth="" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemStyle Width="" CssClass="member-profile-td"></ItemStyle>
                                        <ItemTemplate>
                                            <%-- Neha,issue 16001,5/07/13, added class for Div for image heightwidth and allignment of Name,Title,Adderess --%>
                                            <div class="imgmember">
                                                <asp:Label runat="server" Text="Profile Image:" CssClass="mobile-label"></asp:Label>
                                                <rad:RadBinaryImage ID="imgmember" runat="server" CssClass="PeopleImage cai-table-data"
                                                    AutoAdjustImageControlSize="false" ResizeMode="Fill"></rad:RadBinaryImage>
                                            </div>
                                            <div class="member-profile-details">
                                                <asp:Label runat="server" Text="Name:" CssClass="mobile-label"></asp:Label>
                                                <asp:Label ID="lblMember" CssClass="cai-table-data member-name-label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Recipient") %>'></asp:Label>
                                                <asp:Label runat="server" Text="Title:" CssClass="mobile-label no-desktop"></asp:Label>
                                                <asp:Label ID="lblMemberTitle" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Label>
                                                <asp:Label runat="server" Text="Location:" CssClass="mobile-label"></asp:Label>
                                                <asp:Label ID="lblCity" runat="server" CssClass="cai-table-data member-label" Text='<%# DataBinder.Eval(Container.DataItem,"City") %>'> </asp:Label>
                                                <asp:Label ID="lblCountry" runat="server" CssClass="cai-table-data no-desktop member-label" Text='<%# DataBinder.Eval(Container.DataItem,"Country") %>'> </asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Subscription Type" SortExpression="PurchaseType"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Custom" ShowFilterIcon="false"
                                        FilterControlWidth="" DataField="PurchaseType">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Subscription Type:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblPurchaseType" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"PurchaseType") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Subscription ID" SortExpression="ID" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Custom" ShowFilterIcon="false" FilterControlWidth=""
                                        DataField="ID">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign gridpadding"></ItemStyle>
                                        <ItemTemplate>

                                            <asp:Label runat="server" Text="Subscription ID:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblSubscriptionID" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'> </asp:Label>

                                            <asp:Label runat="server" Text="Subscription For:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="label6" runat="server" CssClass="cai-table-data no-desktop" Text='<%# DataBinder.Eval(Container.DataItem, "Product")%>'> </asp:Label>

                                            <asp:Label runat="server" Text="Subscription Expire On:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="label1" runat="server" CssClass="cai-table-data no-desktop" Text='<%# DataBinder.Eval(Container.DataItem, "EndDate")%>'> </asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn HeaderText="Subscribed For" AllowFiltering="true" SortExpression="Product"
                                        DataField="Product" UniqueName="Product" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </rad:GridBoundColumn>
                                    <rad:GridDateTimeColumn HeaderText="Subscription Expire On" SortExpression="EndDate"
                                        UniqueName="GridDateTimeColumnStartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" FilterControlToolTip="Enter a filter date" FilterControlWidth=""
                                        DataType="System.DateTime" DataField="EndDate" DataFormatString="{0:MMMM dd, yyyy }"
                                        PickerType="DatePicker" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </rad:GridDateTimeColumn>
                                    <rad:GridTemplateColumn HeaderText="Auto Renewal" AllowFiltering="false">
                                        <ItemTemplate>
                                           
                                                        <asp:Label runat="server" Text="Auto Renewal:" CssClass="mobile-label no-desktop"></asp:Label>
                                                        <asp:Label ID="lblOnOff" CssClass="cai-table-data" runat="server" />
                                                
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Subscription For:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblProduct" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Product") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'></asp:Label>
                                            <asp:Label ID="lblExpiryDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ExpiryDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="actions">
        <asp:Button runat="server" ID="btnRenew" Text="Renew Subscription" class="submitBtn"
            CausesValidation="False" />
        <asp:Button ID="btnRenewalOn" Text="Turn Auto Renewal On" runat="server" class="submitBtn"
            CausesValidation="False" />
        <asp:Button ID="btnRenewalOff" Text="Turn Auto Renewal Off" runat="server" class="submitBtn"
            CausesValidation="False" />
    </div>
    <rad:RadWindow ID="CreditcardWindow" runat="server" VisibleOnPageLoad="false" Modal="true"
        Behaviors="Move" Title="Payment Information" VisibleStatusbar="false" Skin="Default"
        IconUrl="" Width="435px" Height="325px" BackColor="#DADADA" ForeColor="#bda797">
        <ContentTemplate>
            <table class="data-form">
                <tr>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" Text=" Please Enter Payment Information to Create Standing Orders."></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:CreditCard ID="CreditCard" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:CheckBox ID="chkMakePayment" runat="server" Visible="false" />
                        <asp:Button ID="btnMakePayment" runat="server" Text="Make Payment" CssClass="submitBtn" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            CssClass="submitBtn" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </rad:RadWindow>
    <rad:RadWindow ID="radWindowMessage" runat="server" VisibleOnPageLoad="false" Modal="true"
        Skin="Default" Behaviors="Move" Width="400px" Height="20px" Title="Please Choose at least one Subscription" BackColor="#DADADA"
        ForeColor="#bda797" VisibleStatusbar="false" CssClass="style-1" IconUrl="">
        <ContentTemplate>
            <div class="modal-actions">
                <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label>
                <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="submitBtn" CausesValidation="false" />
            </div>
        </ContentTemplate>
    </rad:RadWindow>
    <cc3:User ID="User1" runat="server" />
    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
</div>
