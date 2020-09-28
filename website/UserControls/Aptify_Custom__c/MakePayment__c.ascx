<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MakePayment__c.ascx.vb"
    Debug="true" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.MakePaymentControl__c" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<script type="text/javascript">
    window.history.forward(-1);
</script>
<div class="dvUpdateProgress"  style="overflow:visible;"> 
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server"
        DisplayAfter="0"> 
        <ProgressTemplate> 
            <div class="dvProcessing" style="height:1000px;"> 
                <table class="tblFullHeightWidth"> 
                    <tr> 
                        <td class="tdProcessing" style="vertical-align:middle" > 
                            Please wait... 
                        </td> 
                    </tr> 
                </table> 
            </div> 
        </ProgressTemplate> 
    </asp:UpdateProgress> 
</div>
<div class="content-container clearfix">
<asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server">
        <ContentTemplate>
    <%--Nalini Issue 12734--%>
    <div>
        <%--Suraj issue 14450 2/7/13  removed three step sorting ,added tooltip and added date column--%>
        <asp:button cssclass="submitBtn" id="btnAbatement" runat="server" text="Abatement"></asp:button><br />
        <asp:Label id="lblNote" Visible="False" Text="Note: The donation product amount can not be changed if the order is shipped." runat="server"></asp:Label>
        <rad:radgrid id="grdMain" autogeneratecolumns="False" runat="server" validationsettings-validationgroup="Od"
            enablelinqexpressions="false" sortingsettings-sorteddesctooltip="Sorted Descending"
            sortingsettings-sortedasctooltip="Sorted Ascending" allowfilteringbycolumn="true"
            allowpaging="true" pagesize="15">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="true" AllowNaturalSort="false" NoMasterRecordsText="No Payment Available.">
                <Columns>
                    <rad:GridTemplateColumn AllowFiltering="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAllMakePayment" runat="server" OnCheckedChanged="ToggleSelectedState"
                                AutoPostBack="True" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkMakePayment" OnCheckedChanged="chkMakePayment_CheckedChanged"
                                AutoPostBack="true"></asp:CheckBox>
                            <asp:Label ID="lblOrderLineID" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineID") %>'
                                runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridHyperLinkColumn DataNavigateUrlFields="ID" DataTextField="ID" HeaderText="Order #"
                        FilterControlWidth="80%" SortExpression="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false"  Target="_new" />
                    <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                        HeaderStyle-CssClass="CenterAlign" HeaderText="Date" HeaderStyle-Width="180px"
                        SortExpression="OrderDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        DataType="System.DateTime" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                    <rad:GridBoundColumn HeaderText="Product" DataField="Product" ItemStyle-CssClass="CenterAlign"
                        HeaderStyle-Width="180px" HeaderStyle-CssClass="CenterAlign" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                    <rad:GridBoundColumn HeaderText="Category" DataField="ProductCategory" ItemStyle-CssClass="CenterAlign"
                        HeaderStyle-Width="100px" HeaderStyle-CssClass="CenterAlign" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                          <rad:GridBoundColumn HeaderText="Pay Type" DataField="PayType" ItemStyle-CssClass="CenterAlign"
                        HeaderStyle-Width="70px" HeaderStyle-CssClass="CenterAlign" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                    <rad:GridTemplateColumn HeaderText="Price" DataField="Extended" SortExpression="Extended"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%#GetFormattedCurrency(Container, "Extended")%>'>
                            </asp:Label>
                        </ItemTemplate>
                       <%-- <EditItemTemplate>
                            <asp:TextBox ID="txtPrice" runat="server" Text='<%#GetFormattedCurrency(Container, "Extended")%>'>
                            </asp:TextBox>
                        </EditItemTemplate>--%>
                        <ItemStyle HorizontalAlign="Left" CssClass="griditempaddingRight" />
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="VAT" SortExpression="VAT" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            <ItemTemplate>
                               <asp:Label ID="lblVAT" size="15" runat="server" 
                                    Text='<%#GetFormattedCurrency(Container,"VAT")%>' ></asp:Label>
                            </ItemTemplate>
                            </rad:GridTemplateColumn>
                    <%--Suraj issue 14450 4/17/14, apply HeaderStyle--%>
                    <rad:GridTemplateColumn HeaderText="Pay Amount" DataField="PayAmount" SortExpression="PayAmount"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                        <ItemStyle HorizontalAlign="Center" CssClass="rightAlign gridAlign" Width="130px">
                                        </ItemStyle>
                                        <HeaderStyle Width="100px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCurrencySymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                            <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="CenterAlign TextboxStylePayment"
                                Text='<%#GetFormattedCurrencyNoSymbol(Container,"PayAmount")%>' OnTextChanged="txtPayAmt_TextChanged"
                                AutoPostBack="true" Enabled="false"></asp:TextBox>
                            <%--      Enabled='<%# DataBinder.Eval(Container.DataItem,"EnableFunds") %>'--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPayAmt"
                                ErrorMessage="*" runat="server" Font-Size="8pt"></asp:RequiredFieldValidator>
                            <Ajax:FilteredTextBoxExtender ID="txtPayAmt_Extender" runat="server" Enabled="True"
                                TargetControlID="txtPayAmt" FilterType="custom" ValidChars="1234567890.">
                            </Ajax:FilteredTextBoxExtender>
                        </ItemTemplate>
                       
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Currency Digits" Visible="false" DataField="NumDigitsAfterDecimal"
                        SortExpression="NumDigitsAfterDecimal" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Label ID="lblEnableFunds" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EnableFunds") %>'></asp:Label>
                            <asp:Label ID="lblNumDigitsAfterDecimal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Actual Balance" Visible="false" DataField="Balance"
                        SortExpression="Balance" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Label ID="lblBalance" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PayAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Balance" HeaderStyle-CssClass="rightAlign" Visible="false">
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                        <%-- <HeaderStyle Width="60px" />--%>
                        <ItemTemplate>
                            <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#GetFormattedCurrency(Container, "PayAmount")%>'></asp:Label>
                            <asp:Label ID="lblOrderLineNumber" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineNumber") %>'
                                runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </rad:radgrid>
        <div style="text-align: right;">
            <asp:label id="lblTotal" text="Total of selected order lines: " forecolor="#333333" font-size="13px" font-bold="true"
                runat="server"></asp:label>
            <asp:label id="txtTotal" runat="server" font-bold="True"></asp:label>
        </div>
        <asp:label id="lblNoRecords" runat="server" visible="False"><strong>No unpaid orders exist for this account.</strong></asp:label>
    </div>
    <table>
        <tr>
            <td>
                <asp:button cssclass="submitBtn" id="btnNext" runat="server" text="Next"></asp:button>
                <asp:label id="lblError" runat="server" visible="False"></asp:label>
            </td>
        </tr>
    </table>
     </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnNext" />           
        </Triggers>
    </asp:UpdatePanel>

    <cc2:user id="User1" runat="server" />
    <%--'Anil B Issue 10254 on 20/04/2013
             Added reference for shoping cart--%>
    <cc1:aptifyshoppingcart runat="Server" id="ShoppingCart1" />
</div>
