<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AdminNonDuesOrderDetail__c.ascx.vb"  Debug="true" 
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.AdminNonDuesOrderDetail__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>--%>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/NonDuesFirmbillingPaymentControl__c.ascx" %>
<div class="maindiv">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table id="tblmember" runat="server" width="100%" class="data-form">
                <tr>
                    <td id="Td1" runat="server" style="font-weight: bold; font-size: 13px">
                        Pay Open Invoices for My Company
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="prnLink" runat="server">Print</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rad:RadGrid ID="grdOrderDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" Enabled="true"
                            ValidationSettings-ValidationGroup="Od" Skin="Sunset" HeaderStyle-Width="100%">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                            </ClientSettings>
                            <MasterTableView>
                                <Columns>
                                    <rad:GridBoundColumn HeaderText="ID" DataField="ID" Visible="false" ItemStyle-CssClass="IdWidth" />
                                    <rad:GridTemplateColumn >
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllMakePayment" runat="server" OnCheckedChanged="ToggleSelectedState"CssClass="chkAllMakePayment chkAllCheckBoxes" AutoPostBack="True" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox Enabled="true" runat="server" ID="chkMakePayment" OnCheckedChanged="chkMakePayment_CheckedChanged"  AutoPostBack="true"></asp:CheckBox>
                                            <asp:HiddenField ID="hdnFldId" runat="server" Value='<%# Eval("PersonID") %>' />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>

                                    
                                    <rad:GridTemplateColumn HeaderText="BillToID" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign" Width="120px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBillToID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PersonID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Name">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign" Width="100px"></ItemStyle>
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemberName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Company Name">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign" Width="100px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="CompanyName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Company") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn HeaderText="City" DataField="City" ItemStyle-Width="100px" ItemStyle-CssClass="LeftAlign"
                                        HeaderStyle-Width="80px">
                                    </rad:GridBoundColumn>
                                    <rad:GridTemplateColumn HeaderText="Order #">
                                        <ItemStyle Width="60px" />
                                        <HeaderStyle Width="60px" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblOrderNo" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"OrderConfirmationURL") %>'
                                                runat="server" CssClass="namelink" Target="_blank" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn HeaderText="Date" DataField="OrderDate" ItemStyle-CssClass="LeftAlign"
                                        ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                                    <rad:GridBoundColumn HeaderText="Product(s)" DataField="Product" ItemStyle-CssClass="LeftAlign"
                                        HeaderStyle-Width="180px" />
                                    <rad:GridTemplateColumn HeaderText="Total" HeaderStyle-CssClass="rightAlign">
                                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrandTotal" runat="server" Text='<%#GetFormattedCurrency(Container, "GrandTotal")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Balance" HeaderStyle-CssClass="rightAlign">
                                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Currency Symbol" Visible="false" DataField="CurrencySymbol">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrencySymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Pay Amount" HeaderStyle-CssClass="rightAlign"
                                        Visible="true">
                                        <ItemStyle HorizontalAlign="Center" CssClass="rightAlign" Width="150px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumDigitsAfterDecimal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                                            <asp:Label ID="lblCurrSymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                            <%-- <asp:TextBox ID="txtPayAmt" Width="120px" runat="server" CssClass="rightAlign" Text='<%#GetFormattedCurrency(Container, "PayAmount")%>'
                                                OnTextChanged="txtPayAmt_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="rightAlign TextboxStylePayment"
                                                Text="0.00" OnTextChanged="txtPayAmt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </td>
                </tr>
                <tr class="rightAlign">
                    <td align="right">
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblTotal" Text="Total: " ForeColor="#333333" Font-Size="13px" Font-Bold="true"
                                        runat="server"></asp:Label>
                                </td>
                                <td style="padding-right: 14px" align="right">
                                    <asp:Label ID="txtTotal" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table valign="top" width="100%" class="bordercolor" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                        <asp:Label ID="lblPaymentError" runat="server" Visible="False" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgcolorshipping infohead" style="padding-left: 8px">
                        <strong><font size="2">Payment Information</font></strong>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 5px;">
                        <uc1:CreditCard ID="CreditCard11" runat="server" />
                    </td>
                </tr>
            </table>
            <rad:RadWindow ID="radpaymentmsg" runat="server" Width="260px" Height="120px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Order Payment" Behavior="None">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radpaymentmsg" />
        </Triggers>
    </asp:UpdatePanel>
    <table>
        <tr>
            <td valign="top" align="left" style="padding-left: 8px">
                <br />
                <asp:Button CssClass="submitBtn" ID="cmdMakePayment" TabIndex="1" runat="server"
                    Height="26px" Text="Make Payment"></asp:Button>
            </td>
        </tr>
    </table>
</div>
<cc2:User runat="Server" ID="User1" />
<%--  <rad:RadMaskedTextBox ID="PhoneNumber"  runat="server"   
    Width="120"   
    Mask="(###) ###-####">  
</rad:RadMaskedTextBox>--%>
