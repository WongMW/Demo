<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AdminOrderDetailSummary__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.AdminOrderDetailSummary__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>--%>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/CreditCard__c.ascx" %>
<script language="javascript" type="text/javascript">

    function validatePage() {
        var isValid = Page_ClientValidate();
        var button = document.getElementById('<%= cmdMakePayment.ClientID %>');
        var buttonText = button.value;
        button.disabled = true;
        button.value = 'Submitting...';

        button.disabled = isValid;

        if (isValid == false) {
            button.value = buttonText;
        }

    }

    function validatePageBack() {
        //var isValid = Page_ClientValidate();
        var button = document.getElementById('<%= btnBack.ClientID %>');
        button.click();
        return true;
    }

    function UpdateItemCountField(sender, args) {
        //set the footer text
        sender.get_dropDownElement().lastChild.innerHTML = "A total of " + sender.get_items().get_count() + " items";
    }


    <%--Anil Issue 6640--%>
    window.history.forward(1);


    function validatenumerics(obj, e) {
        var keycode = (e.which) ? e.which : e.keyCode;
        var fieldval = (obj.value);
        var dots = fieldval.split(".").length;
        if (keycode == 46) {
            if (dots > 1) {
                return false;
            } else {
                return true;
            }
        }
        if (keycode == 8 || keycode == 9 || keycode == 46 || keycode == 13 || keycode == 37 || keycode == 39) // back space, tab, delete, enter 
        {
            return true;
        }
        if ((keycode >= 32 && keycode <= 45) || keycode == 47 || (keycode >= 58 && keycode <= 127)) {
            return false;
        }

        else return true;
    }

</script>

<%-- Susan Wong, Ticket #18739 - ADD LOADING SCREEN start --%>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>This process can take a few minutes.<br />
                    WARNING: Please do not leave or close this window while payment is processing.
                </span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<%-- Susan Wong, Ticket #18739 - ADD LOADING SCREEN end --%>


<div class="cai-table ceu-page">
    <asp:Label ID="lblrecmsg" runat="server" Visible="False" Text="Item not found" ForeColor="red"></asp:Label>
    <div id="payoffdiv" runat="server" class="maindiv cai-table">
        <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" >
            <ContentTemplate>
                <div id="tblmember" runat="server" width="100%" class="data-form">
                    <div id="trCurrency" runat="server" visible="false">
                        <asp:Label ID="lblfilter" Text=" Currency Type:" runat="server"></asp:Label>
                        <asp:DropDownList ID="radcurrency" AutoPostBack="true" DataTextField="CurrencyType" DataValueField="CurrencyTypeID" runat="server">
                        </asp:DropDownList>
                    </div>

                    <div>
                        <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="red"></asp:Label>
                    </div>

                    <div class="mobile-table">
                        <rad:RadGrid ID="grdOrderDetails" runat="server" AutoGenerateColumns="False" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending" PagerStyle-PageSizeLabelText="Records Per Page"
                            AllowPaging="True">
                            <PagerStyle CssClass="sd-pager" />
                            <MasterTableView AllowFilteringByColumn="false">
                                <Columns>
                                    <rad:GridTemplateColumn AllowFiltering="false" Display="false">
                                        <ItemTemplate>
                                            <asp:Label ID="ID" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="OrderLineID" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineID") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="CategoryID" Text='<%# DataBinder.Eval(Container.DataItem,"CategoryID") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="OrderLineNumber" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineNumber") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="OrderLine" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLine") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="IsDonation" Text='<%# DataBinder.Eval(Container.DataItem,"IsDonation") %>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Name" DataField="Name" SortExpression="Name"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Name:</span>
                                            <asp:Label ID="lblMemberName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="City" SortExpression="City" AutoPostBackOnFilter="true" UniqueName="City"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        DataField="City" Visible="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">City:</span>
                                            <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "City")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Order #" DataField="ID" SortExpression="ID" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Order #:</span>
                                            <asp:HyperLink ID="lblOrderNo" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"OrderConfirmationURL") %>'
                                                runat="server" CssClass="cai-table-data" Target="_blank" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                                        HeaderText="Date" SortExpression="OrderDate"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                                        ShowFilterIcon="false" Visible="false">
                                         <ItemTemplate>
                                            <span class="mobile-label">Order Date:</span>
                                            <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"OrderDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Product(s)"
                                        DataField="Product" SortExpression="Product"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                                          <ItemTemplate>
                                            <span class="mobile-label">Product(s):</span>
                                            <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Category" ItemStyle-Wrap="true"
                                        DataField="ProductCategory" SortExpression="ProductCategory"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" Visible="false">
                                          <ItemTemplate>
                                            <span class="mobile-label">Category:</span>
                                            <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProductCategory") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="Total" SortExpression="GrandTotal"
                                        DataField="GrandTotal" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Total:</span>
                                            <asp:Label ID="lblGrandTotal"  CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container, "GrandTotal")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="CompanyTotal" SortExpression="CompanyTotal"
                                        DataField="CompanyTotal" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" Visible="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Company Total:</span>
                                            <asp:Label ID="lblCompanyTotal"  CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container, "CompanyTotal")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="Balance"
                                        SortExpression="Balance" DataField="Balance" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Balance:</span>
                                            <asp:Label ID="lblBalanceAmount" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Currency Symbol" DataField="CurrencySymbol" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrencySymbol" Visible="false" CssClass="no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Pay Amount" HeaderStyle-CssClass="rightAlign"
                                        AllowFiltering="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Pay Amount:</span>
                                            <asp:Label ID="lblNumDigitsAfterDecimal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                                            <asp:Label ID="lblCurrSymbol" CssClass="no-desktop no-mob" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                            <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="cai-table-data"
                                                Text='<%# GetFormattedCurrencyNoSymbol(Container,"PayAmount")%>' Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </div>

                    <div id="trShiiping" runat="server" class="profile-page">
                        <asp:Label ID="lblShipping" runat="server" class="profile-title">Shipping and Handling Details:</asp:Label>
                        <div class="field-group">
                            <rad:RadGrid ID="grdShipping" runat="server" AutoGenerateColumns="False" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sorted Ascending" PagerStyle-PageSizeLabelText="Records Per Page"
                                AllowPaging="True">
                                <PagerStyle CssClass="sd-pager" />
                                <MasterTableView AllowFilteringByColumn="true">
                                    <Columns>
                                        <rad:GridHyperLinkColumn DataNavigateUrlFields="ID" DataTextField="ID" HeaderText="Order #"
                                            SortExpression="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                            ShowFilterIcon="false" />
                                        <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                                            HeaderText="Date" SortExpression="OrderDate"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                                            ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                                        <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="Balance"
                                            SortExpression="Balance" DataField="Balance" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Pay Amount" HeaderStyle-CssClass="rightAlign"
                                            AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumDigitsAfterDecimal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                                                <asp:Label ID="lblCurrSymbol" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                                <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="rightAlign TextboxStylePayment"
                                                    Text='<%# GetFormattedCurrencyNoSymbol(Container,"PayAmount")%>' Enabled="false"></asp:TextBox>
                                                <asp:Label ID="lblOrderShipmentID" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"OrderShipmentID") %>' runat="server" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                        </div>
                    </div>
                <div class="payment-summary">
                    <div class="field-group">
                        <asp:Label ID="lblTotal" Text="Total: " class="billing-label" runat="server"></asp:Label>
                        <asp:Label ID="txtTotal" runat="server" CssClass="cai-table-data"></asp:Label>
                    </div>

                    <div class="field-group">
                        <asp:Label ID="Label1" Text="Would you like to pay Benevolent Donation " class="billing-label" runat="server"></asp:Label>
                        <asp:RadioButton ID="rdbYes" runat="server" Checked="false"  CssClass="cai-table-data tooltip"  Text="Yes" GroupName="YesNo" AutoPostBack="true" alt="Selecting 'Yes' would add £80 / €100 to the total" />
                        <asp:RadioButton ID="rdbNo" runat="server" Checked="true" CssClass="cai-table-data" Text="No" GroupName="YesNo" AutoPostBack="true" />
                    </div>

                    <div class="field-group" style="display:none;">
                        <asp:Label ID="Label2" Text="Benevolent Donation Suggested amount " class="billing-label" runat="server"></asp:Label>
                        <asp:Label ID="lblDonCurrSymbol" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txtDonation" runat="server" CssClass="cai-table-data" Text="1000" OnTextChanged="txtDonation_TextChanged" AutoPostBack="true" onKeyPress="return validatenumerics(this, event);"></asp:TextBox>
                    </div>

                    <div class="field-group">
                        <asp:Label ID="lblPay" Text="Total Amount To Pay :" class="billing-label" runat="server"></asp:Label>
                        <asp:Label ID="lblTotCurrSymbol" CssClass="cai-table-data" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblTotalPay" CssClass="cai-table-data" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                    <div class="field-group payment-summary">
                        <span class="label-title">Payment Information</span>
                        <uc1:CreditCard ID="CreditCard" runat="server" />
                    </div>

                    <div class="field-group">
                        <rad:RadWindow ID="radpaymentmsg" runat="server" Width="260px" Height="120px" Modal="True"
                            BackColor="#f4f3f1" Skin="Default" VisibleStatusbar="False" Behaviors="None"
                            ForeColor="#BDA797" Title="Order Payment" Behavior="None">
                            <ContentTemplate>
                                <div>
                                    <div>
                                        <asp:Label ID="lblpayment" runat="server" Text=" Your payment was made successfully!"
                                            Font-Bold="true"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnok" runat="server" Text="OK" Width="70px" class="submitBtn" OnClick="btnok_Click"
                                            ValidationGroup="ok" />&nbsp;&nbsp;
                                    </div>
                                </div>
                            </ContentTemplate>
                        </rad:RadWindow>
                    </div>
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="radpaymentmsg" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>
                <div class="payment-actions">
                    <asp:Button CssClass="submitBtn" ID="btnBack" runat="server" CausesValidation="false" Text="Back" UseSubmitBehavior="false" OnClientClick="validatePageBack(); " Visible="false"></asp:Button>
                    <asp:Button CssClass="submitBtn" ID="cmdMakePayment" runat="server" Text="Make Payment" UseSubmitBehavior="false" OnClientClick="validatePage();"></asp:Button>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<cc2:User runat="Server" ID="User1" />
<asp:HiddenField runat="server" ID="Hidden" Value="true" />
