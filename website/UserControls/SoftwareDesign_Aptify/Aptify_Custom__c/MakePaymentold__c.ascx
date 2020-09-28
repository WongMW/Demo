<%@ Control Language="VB" Debug="True" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/MakePaymentold__c.ascx.vb"
    Inherits="MakePaymentControlOld__c" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<div class="content-container clearfix">
    <%--Nalini Issue 12734--%>
    <table id="tblMain" runat="server" style="height: 1px" class="data-form">
        <tr id="paymentMade" visible="false" runat="server">
            <td style="height: 30px; border-bottom: solid 2px gray">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div>
        <asp:GridView ID="grdMain" AutoGenerateColumns="False" runat="server" AllowPaging="false"
            SkinID="test" Width="75%" GridLines="Horizontal" BorderColor="#CCCCCC" BorderWidth="1px"
            AlternatingRowStyle-BackColor="White">
            <HeaderStyle CssClass="GridViewHeader" Height="28px" HorizontalAlign="Center" Font-Bold="true" />
            <RowStyle CssClass="GridItemStyle" BackColor="#e5e2dd" />
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="ID" DataTextField="ID" HeaderText="Order #"  />

               <%--  <asp:TemplateField HeaderText="Order #">
                    <ItemTemplate>
                       <asp:HyperLink ID="hlink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' ></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%-- <asp:BoundField DataField="ID" HeaderText="Order #" />--%>
                <asp:BoundField DataField="Product" HeaderText="Product" />
                <%-- <asp:BoundField DataField="Price" HeaderText="Amount/Balance" />--%>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%#GetFormattedCurrency(Container, "Price")%>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:BoundField DataField="OrderDate" HeaderText="Date"  />


                <asp:TemplateField HeaderText="Total" >
                 <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%#GetFormattedCurrency(Container, "GrandTotal")%>'>
                            </asp:Label>
                                      </ItemTemplate>
                              </asp:TemplateField>

                <asp:TemplateField HeaderText="GrandTotal" Visible =" false" >
                 <ItemTemplate>
               <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GrandTotal")%>'>
                            </asp:Label>
                       </ItemTemplate>
                              </asp:TemplateField>

                 <asp:TemplateField HeaderText="Balance">
                <ItemTemplate>
                            <asp:Label ID="Lblbalances" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'>
                            </asp:Label>
                        </ItemTemplate>
                  </asp:TemplateField>

                 <asp:TemplateField HeaderText="Pay Amount">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPay" runat="server" Text='<%#GetFormattedCurrency(Container, "Price")%>' AutoPostBack="true" OnTextChanged="txtPay_TextChanged"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPay"
                            ErrorMessage="Pay amount required" runat="server" Font-Size="8pt"></asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Currency Digits" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblNumDigitsAfterDecimal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Balance" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblBalance" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Price") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <%--  <PagerSettings Mode="Numeric" />--%>
        </asp:GridView>
        <asp:UpdatePanel ID="updPanelGrid" runat="server" UpdateMode="Always">
            <ContentTemplate>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdMain" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Label ID="lblNoRecords" runat="server" Visible="False"><strong>No unpaid orders exist for this account.</strong></asp:Label>

         <table>
                <tr>
                            <td style="width: 150px"></td>
                               
                              <td class="auto-style1">
                                <asp:Label ID="lblTotal" Text="Total: " ForeColor="#333333" Font-Size="13px" Font-Bold="true"
                                    runat="server"></asp:Label></td>
                                   &nbsp; &nbsp; 
                              <td>€</td>
                                 <td>
                                <asp:Label ID="txtTotal" runat="server" Font-Bold="True"></asp:Label>
                            </td>

                                <td style="width: 20px"></td>
                                   <td>&nbsp; &nbsp;</td>
                                 <td class="auto-style1">
                                <asp:Label ID="LblgrandTotal" Text="Order Total: " ForeColor="#333333" Font-Size="13px" Font-Bold="true"
                                    runat="server"></asp:Label>
                                &nbsp; &nbsp; </td>
                    <td>€</td>
                    <td>
                                <asp:Label ID="txtGrandtotal" runat="server" Font-Bold="True"></asp:Label>
                            </td>

                                  <td style="width: 20px"></td>
                                    <td>€</td>
                                  <td class="auto-style1">
                                <%--<asp:Label ID="Lblbaltotal" Text="Balance Total: " ForeColor="#333333" Font-Size="13px" Font-Bold="true"
                                    runat="server"></asp:Label>
                                &nbsp; &nbsp; --%>
                                <asp:Label ID="txtbalanceTotal" runat="server" Font-Bold="True"></asp:Label>
                            </td>

                                <td style="width: 20px"></td>
                                        <td>€</td>
                                  <td class="auto-style1">
                                <%--<asp:Label ID="Lblbaltotal" Text="Balance Total: " ForeColor="#333333" Font-Size="13px" Font-Bold="true"
                                    runat="server"></asp:Label>
                                &nbsp; &nbsp; --%>
                                   
                                <asp:Label ID="txtPaytotal" runat="server" Font-Bold="True"></asp:Label>
                            </td>


                        </tr>
               
                
                 </table>
    </div>

    <table>
        <tr>
            <td>
                <uc1:CreditCard ID="CreditCard1" runat="server" CompanyCreditStatus="0" UserCreditStatu="0" CreditCheckLimit="true" >
                </uc1:CreditCard>
            </td>
        </tr>
        <tr>
            <td>
                <%-- Nalini Issue 12734--%>
                <asp:Button ID="cmdPay" runat="server" Text="Make Payment"></asp:Button>&nbsp&nbsp;<asp:Label
                    ID="lblError" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
      <cc2:User ID="User1" runat="server" />
      <%--'Anil B Issue 10254 on 20/04/2013
             Added reference for shoping cart--%>
    <cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
</div>
