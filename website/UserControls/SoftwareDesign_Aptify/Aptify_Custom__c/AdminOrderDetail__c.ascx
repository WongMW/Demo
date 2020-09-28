<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AdminOrderDetail__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.AdminOrderDetail__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">Please wait... 
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>


<div class="page-message">
    <p runat="server" style="font-weight: bold;">
        Pay open invoices for my company
    </p>
    <span id="spnNote" runat="server">
        <asp:Label ID="lblNote" runat="server" Text="Benevolent donation amount is not editable if order is shipped." Visible="False"></asp:Label>
    </span>
</div>
<asp:Label ID="lblrecmsg" runat="server" Visible="False" Text="Item not found" ForeColor="red"></asp:Label>
<div id="payoffdiv" runat="server" class="maindiv cai-table">
    <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server">
        <ContentTemplate>
            <table id="tblmember" runat="server" width="100%" class="data-form admin-order-detail">

                <tr>
                    <td>
                        <asp:Label ID="lblfilter" Text=" Currency type:" runat="server" Font-Bold="true"></asp:Label>
                        <asp:DropDownList ID="radcurrency" AutoPostBack="true" DataTextField="CurrencyType" Style="width: 160px;" DataValueField="CurrencyTypeID" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="error-message">
                        <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rad:RadGrid ID="grdOrderDetails" runat="server" AutoGenerateColumns="False" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending" PagerStyle-PageSizeLabelText="Records per page"
                            AllowPaging="True" CssClass="unpaid-order">
                            <PagerStyle CssClass="sd-pager" />
                            <MasterTableView AllowFilteringByColumn="false" NoMasterRecordsText="No open invoices available.">
                                <Columns>
                                    <rad:GridTemplateColumn AllowFiltering="false" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="gridAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Image ID="ImgFlag" runat="server" Visible="true" ToolTip="Flagged for review" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <%--Neha Changes for Issue:14972,04/29/13--%>
                                    <rad:GridTemplateColumn AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="gridAlign"></ItemStyle>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllMakePayment" runat="server" OnCheckedChanged="ToggleSelectedState"  CssClass="chkAllMakePayment chkAllCheckBoxes" AutoPostBack="True" />
                                        </HeaderTemplate>
                                        <ItemStyle CssClass="mobile-row" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Select:" CssClass="mobile-label"></asp:Label>
                                            <asp:CheckBox runat="server" CssClass="cai-table-data" ID="chkMakePayment" OnCheckedChanged="chkMakePayment_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                            <asp:Label ID="ID" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>' runat="server" Visible="false">                                               
                                            </asp:Label>
                                            <asp:Label ID="OrderLineID" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineID") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="CategoryID" Text='<%# DataBinder.Eval(Container.DataItem,"CategoryID") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="OrderLineNumber" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineNumber") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="OrderLine" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLine") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="IsDonation" Text='<%# DataBinder.Eval(Container.DataItem,"IsDonation") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="OrderStatus" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Name" DataField="Name" SortExpression="Name"
                                        FilterControlWidth="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle />
                                        <ItemStyle></ItemStyle>
                                        <ItemTemplate>
                                            <!-- table data -->
                                            <asp:Label runat="server" Text="Name:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblMemberName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn HeaderText="City" SortExpression="City" AutoPostBackOnFilter="true" UniqueName="City"
                                        FilterControlWidth="" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        DataField="City" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" visible="false"/>
                                    <rad:GridTemplateColumn HeaderText="Order #" DataField="ID" SortExpression="ID" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false">

                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Order #:" CssClass="mobile-label"></asp:Label>
                                            <asp:HyperLink ID="lblOrderNo" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"OrderConfirmationURL") %>'
                                                runat="server" CssClass="namelink cai-table-data" Target="_blank" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:HyperLink>
                                            <asp:Label runat="server" Text="City:" CssClass="mobile-label" visible="false"></asp:Label>
                                            <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "City")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label runat="server" Text="Date" CssClass="mobile-label" Visible="false"></asp:Label>
                                            <asp:Label ID="Label2" CssClass="cai-table-data no-desktop " Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label runat="server" Text="Products" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="Label3" CssClass="cai-table-data no-desktop" Text='<%# DataBinder.Eval(Container.DataItem, "Product")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                                        HeaderText="Date" FilterControlWidth="100px" HeaderStyle-Width="100px" SortExpression="OrderDate"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                                        ShowFilterIcon="false" EnableTimeIndependentFiltering="true" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" Visible="false"/>
                                    <rad:GridBoundColumn HeaderText="Product(s)" HeaderStyle-Width="100px" ItemStyle-Wrap="true"
                                        FilterControlWidth="" ItemStyle-Width="100px" DataField="Product" SortExpression="Product"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                                    <rad:GridBoundColumn HeaderText="Category" HeaderStyle-Width="100px" ItemStyle-Wrap="true"
                                        FilterControlWidth="" ItemStyle-Width="100px" DataField="ProductCategory" SortExpression="ProductCategory"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" Visible="false"/>
                                    <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="Total" SortExpression="GrandTotal"
                                        FilterControlWidth="" DataField="GrandTotal" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob">

                                        <ItemTemplate>

                                            <asp:Label runat="server" Text="Total:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblGrandTotal" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container, "GrandTotal")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="Company total" SortExpression="CompanyTotal"
                                        FilterControlWidth="" DataField="CompanyTotal" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" Visible="false">
                                        <ItemStyle />
                                        <HeaderStyle />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Company total:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblCompanyTotal" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container, "CompanyTotal")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="Balance"
                                        FilterControlWidth="" SortExpression="Balance" DataField="Balance" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle CssClass="rightAlign gridAlign" Width="60px" />

                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Balance:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblBalanceAmount" CssClass="cai-table-data" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Currency symbol" Visible="false" DataField="CurrencySymbol"
                                        FilterControlWidth="">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Currency:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:Label ID="lblCurrencySymbol" CssClass="cai-table-data 1231" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Comments" AllowFiltering="false" FilterControlWidth="80%" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="gridAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Comments:" CssClass="mobile-label"></asp:Label>
                                            <asp:LinkButton ID="lnkAddComments" CssClass="cai-table-data" runat="server" Text="Add/Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"OrderLine") %>'
                                                CommandName="ADDEditComments" CausesValidation="false" Visible="true"></asp:LinkButton>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Pay amount" HeaderStyle-CssClass="rightAlign"
                                        FilterControlWidth="" AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label CssClas="cai-table-data" ID="lblNumDigitsAfterDecimal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                                            <asp:Label runat="server" Text="Currency:" CssClass="mobile-label no-desktop" Visible="false"></asp:Label>
                                            <asp:Label ID="lblCurrSymbol" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>' Visible="false"></asp:Label>
                                            <asp:Label runat="server" Text="Pay amount:" CssClass="mobile-label"></asp:Label>
                                            <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="cai-table-data"
                                                Text='<%# GetFormattedCurrencyNoSymbol(Container,"Balance")%>' OnTextChanged="txtPayAmt_TextChanged" AutoPostBack="true" Enabled="false" onKeyPress="return validatenumerics(this, event);"></asp:TextBox>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server" ID="UpEditBadgeInfo" UpdateMod="">
                            <ContentTemplate>
                                <telerik:RadWindow ID="radGAReviewComments" runat="server" Modal="True" Skin="Default" CssClass="dvPopUpOrderComments"
                                    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" Height="300px"
                                    Width="350px" ForeColor="#C59933" IconUrl="~/Images/Comments.png" Title="Review comments">
                                    <ContentTemplate>
                                        <%-- <asp:UpdatePanel runat="server" ID="UpEditBadgeInfo" >
                                        <ContentTemplate>--%>
                                        <table cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; width: 100%">
                                            <tr style="padding-bottom: 5px;">
                                                <td class="lblReviewComments" colspan="2" style="padding-bottom: 5px;">
                                                    <asp:Label ID="lblOrderID" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-bottom: 5px; vertical-align: top;" class="rightAlign">
                                                    <span class="label">Comments:</span>
                                                </td>
                                                <td style="padding-bottom: 5px; text-align:left;">&nbsp;
                                                    <asp:TextBox ID="txtGAReviewComments" runat="server" TextMode="MultiLine" CssClass="GAComments" width="95%" height="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                          </table>
                                            <div class="actions" style="text-align:center">
                                                    <asp:Button ID="BtnSave" class="submitBtn" runat="server" Text="Save" CausesValidation="false" ValidationGroup="grpAddComments" />                                                   
                                                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" class="submitBtn" CausesValidation="false" />
                                           </div>

                                        <%-- </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnSave" />     
                            </Triggers>
                        </asp:UpdatePanel>--%>
                                    </ContentTemplate>
                                </telerik:RadWindow>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="radGAReviewComments" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <rad:RadWindow ID="radpaymentmsg" runat="server" Width="260px" Height="120px" Modal="True"
                BackColor="#f4f3f1" Skin="Default" VisibleStatusbar="False" Behaviors="None"
                ForeColor="#BDA797" Title="Order payment" Behavior="None">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblpayment" runat="server" Text=" Your payment was made successfully!"
                                    Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnok" runat="server" Text="OK" Width="70px" class="submitBtn" OnClick="btnok_Click"
                                    ValidationGroup="ok" />&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </rad:RadWindow>
            <div class="payment-message">
                <asp:Label ID="lblTotal" Text="Total: " ForeColor="#333333" Font-Bold="true"
                    runat="server"></asp:Label>


                <asp:Label ID="txtTotal" runat="server" Width="100px" Font-Bold="true"></asp:Label>
            </div>
            <table>
                <tr style="float: right">
                    <%-- <td valign="top" align="left" style="padding-left: 8px;display:none;">
                <br />
                <asp:Button CssClass="submitBtn" ID="cmdMakePayment" TabIndex="1" runat="server"
                    Height="26px" Text="Make Payment" UseSubmitBehavior="false" OnClientClick="validatePage();">
                </asp:Button>
            </td>--%>
                    <td valign="top" align="right" style="float: right;" class="actions">
                        <%--  'Anil B change for 10737 on 13/03/2013
                'Set Credit Card ID to load property form Navigation Config--%>
                        <asp:Button ID="btnMove" runat="server" Text="Next" OnClientClick="return fnCheckPayment();" CssClass="submitBtn" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radpaymentmsg" />
            <asp:PostBackTrigger ControlID="btnMove" />
        </Triggers>
    </asp:UpdatePanel>


</div>
<cc2:User runat="Server" ID="User1" />
<asp:HiddenField runat="server" ID="Hidden" Value="true" />

<script type="text/javascript">
    function validatePageMove() {
        alert(isValid);
        var button = document.getElementById('<%= btnMove.ClientID %>');
       var buttonText = button.value;
       button.value = 'Submitting...';
       button.click();
   }

   function UpdateItemCountField(sender, args) {
       //set the footer text
       sender.get_dropDownElement().lastChild.innerHTML = "A total of " + sender.get_items().get_count() + " items";
   }

   function SubmittingSave() {
       var isValid = Page_ClientValidate();
       var button = document.getElementById('<%= BtnSave.ClientID %>');
         var hdnTemp = document.getElementById('<%= Hidden.ClientID %>');
         button.disabled = true;
         button.value = 'Submitting...';
         button.style.cursor = "auto";
         button.disabled = isValid;

         if (hdnTemp.value == "true") {
             hdnTemp.value = "false"
             button.click();
             hdnTemp.value = "true"
         }
     }

    window.history.forward(1);

    function fnCheckPayment() {
        var button = document.getElementById('<%= btnMove.ClientID %>');
        button.click();
        return true;
        //        }
    }
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