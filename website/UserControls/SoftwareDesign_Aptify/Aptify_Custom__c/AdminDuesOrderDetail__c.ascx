<%@ Control Language="VB" Debug="true" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AdminDuesOrderDetail__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.AdminDuesOrderDetail__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/FirmBillingPaymentControl__c.ascx" %>
<div class="maindiv">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="tblmember" runat="server" width="100%" class="data-form">
                <h1>Pay Open Invoices for My Company</h1>

                <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="red"></asp:Label>

                <div class="actions clearfix">
                    <asp:HyperLink ID="prnLink" runat="server">Print</asp:HyperLink>
                </div>

                <div class="cai-table mobile-table">
                    <rad:RadGrid ID="grdOrderDetails" runat="server" AutoGenerateColumns="False" PagerStyle-PageSizeLabelText="Records Per Page">
                        <MasterTableView>
                            <Columns>
                                <rad:GridBoundColumn HeaderText="ID" DataField="ID" Visible="false" />
                                <rad:GridTemplateColumn>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllMakePayment" runat="server" OnCheckedChanged="ToggleSelectedState" CssClass="chkAllMakePayment chkAllCheckBoxes" AutoPostBack="True" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkMakePayment" OnCheckedChanged="chkMakePayment_CheckedChanged"
                                            AutoPostBack="true"></asp:CheckBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="BillToID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillToID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PersonID") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Name">

                                    <ItemTemplate>
                                        <span class="mobile-label">Name:</span>
                                        <asp:Label ID="lblMemberName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Company Name">
                                    <ItemTemplate>
                                        <span class="mobile-label">Company Name:</span>
                                        <asp:Label ID="CompanyName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Company") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="City">
                                    <ItemTemplate>
                                        <span class="mobile-label">City:</span>
                                        <asp:Label ID="City" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "City")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Order #">
                                    <ItemTemplate>
                                        <span class="mobile-label">Order #:</span>
                                        <asp:HyperLink ID="lblOrderNo" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"OrderConfirmationURL") %>'
                                            runat="server" CssClass="namelink cai-table-data" Target="_blank" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Date">
                                    <ItemTemplate>
                                        <span class="mobile-label">Date:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Product(s)">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product(s):</span>
                                       <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>

                                <rad:GridTemplateColumn HeaderText="Total">
                                    <ItemTemplate>
                                        <span class="mobile-label">Total:</span>
                                        <asp:Label CssClass="cai-table-data" ID="lblGrandTotal" runat="server" Text='<%#GetFormattedCurrency(Container, "GrandTotal")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Balance">

                                    <ItemTemplate>
                                        <span class="mobile-label">Balance:</span>
                                        <asp:Label CssClass="cai-table-data" ID="lblBalanceAmount" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Currency Symbol" Visible="false" DataField="CurrencySymbol">
                                    <ItemTemplate>
                                        <span class="mobile-label">Currency Symbol:</span>
                                        <asp:Label CssClass="cai-table-data" ID="lblCurrencySymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Pay Amount" Visible="TRUE">

                                    <ItemTemplate>
                                        <span class="mobile-label">Pay Amount:</span>
                                        <asp:Label CssClass="no-mob" ID="lblNumDigitsAfterDecimal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                                        <asp:Label CssClass="no-mob" ID="lblCurrSymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                        <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="rightAlign TextboxStylePayment cai-table-data"
                                            Text="0.00" OnTextChanged="txtPayAmt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>
                </div>


            </div>

            <div class="cai-form">
                <label class="form-title">Payment Information</label>

                <div class="cai-form-content">

                    <asp:Label ID="lblTotal" Text="Total: " CssClass="label-title-inline" runat="server"></asp:Label>
                    <asp:Label ID="txtTotal" runat="server" Width="100px"></asp:Label>

                    <div id="trPaymentError" runat="server">
                        <asp:Label ID="lblPaymentError" runat="server" Visible="False" ForeColor="red"></asp:Label>
                    </div>

                    <uc1:CreditCard ID="CreditCard1" runat="server" />

                    <rad:RadWindow ID="radpaymentmsg" runat="server" Height="120px" Modal="True"
                        BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="Order Payment" Behavior="None">
                        <ContentTemplate>

                            <asp:Label ID="lblpayment" runat="server"></asp:Label>

                            <asp:Button ID="btnok" runat="server" Text="OK" Width="70px" class="submitBtn" OnClick="btnok_Click"
                                ValidationGroup="ok" />&nbsp;&nbsp;
                           
                        </ContentTemplate>
                    </rad:RadWindow>

                    <div class="actions">
                        <asp:Button CssClass="submitBtn" ID="cmdMakePayment" TabIndex="1" runat="server" Text="Make Payment"></asp:Button>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radpaymentmsg" />
        </Triggers>
    </asp:UpdatePanel>


</div>
<cc2:User runat="Server" ID="User1" />
