<%@ Control Language="VB" AutoEventWireup="false" CodeFile="OrderConfirmation.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.OrderConfirmationControl" %>
<%@ Register TagPrefix="uc1" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="javascript" type="text/javascript">
    window.history.forward(1);
    <%--Suraj Issue 15210 ,2/41/13 ifemail validation fire the sendmail label message display none --%>
    function clearLabelValue() {
        if (Page_ClientValidate("OrderconformationEmail")) {
            return true;
        }
        else 
        {
            document.getElementById("<%=SendEmailLabel.ClientID%>").style.display = 'none';
            return false;

        }
    }
</script>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="order-confirmation">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            To email this Order Confirmation, enter the email address below and click the button.
                            (Multiple email addresses should be separated by commas.)
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%-- Nalini Issue 12734--%>
                            <asp:TextBox ID="EmailOrderTextBox" runat="server" Width="250px">
                            </asp:TextBox>
                            <%--Suraj Issue 15210 ,2/41/13 if email validation fire the sendmail label message display none --%>
                            <asp:Button CssClass="submitBtn" ID="EmailOrderButton" ValidationGroup="OrderconformationEmail"
                                runat="server" Text="Send" OnClientClick="return clearLabelValue();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--Suraj Issue 15210 ,2/11/13 RegularExpressionValidator validator --%>
                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
                                ValidationExpression="^([A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}(,|$))+"
                                ControlToValidate="EmailOrderTextBox" ErrorMessage="Invalid Email Format" ValidationGroup="OrderconformationEmail"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:Label ID="SendEmailLabel" runat="server"></asp:Label><br />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <!-- End Changes for Issue 4893 -->
        <tr id="tblRowMain" runat="server">
            <td width="100%">
                <table width="100%">
                    <tr>
                        <td class="header">
                            <table width="100%">
                                <%--<tr>
                                    <td colspan="3" class="OrderConfirmationHeader">
                                        Order Confirmation
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td width="75px" class="OrderConfirmationNoFontHeader">
                                        <%-- Amruta IssueID:14841 Remove the Tooltip That Appears With the Company Logo on the order History Page--%>
                                        <img runat="server" src="" id="companyLogo" />
                                    </td>
                                    <td width="300px" class="OrderConfirmationNoFontHeader">
                                        <p>
                                            <b>
                                                <asp:Label ID="lblcompanyAddress" runat="server" Text="" Font-Size="Small"> </asp:Label></b>
                                        </p>
                                    </td>
                                    <td align="right" width="150px" class="OrderConfirmationNoFontHeader">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;<b> Phone:</b>
                                                </td>
                                                <td style="text-align: center;">
                                                    (202)<span style="display: none;">_</span>555-1234
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;<b> Fax:</b>
                                                </td>
                                                <td style="text-align: center;">
                                                    (202)<span style="display: none;">_</span>555-4321
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 1px;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="bordercolor">
                            <table style="padding-left: 10px;">
                                <tr>
                                    <td>
                                        <b>Order Number:</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOrderID" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Order Type:</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOrderType" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Status:</b>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblOrderStatus"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Payment Method:</b>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPayType"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Bill To:</b>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td style="padding-left: 11px; text-align: left">
                                        <uc1:nameaddressblock id="blkBillTo" runat="server"></uc1:nameaddressblock>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 1px;">
                            &nbsp;
                        </td>
                        <td class="bordercolor">
                            <table style="padding-left: 10px;">
                                <tr>
                                    <td>
                                        <b>Customer Number:</b>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblBillToID"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Shipment Method:</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblShipType" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblShipTrackingNum" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Date Shipped:</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblShipDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Ship To:</b>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="padding-left: 9px;">
                                        <uc1:nameaddressblock id="blkShipTo" runat="server"></uc1:nameaddressblock>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td colspan="2">
                                        <%--  Navin Prasad Issue 11032--%>
                                        <%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%>
                                        <asp:UpdatePanel ID="updPanelGrid" runat="server">
                                            <ContentTemplate>
                                                <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False">
                                                    <mastertableview>
                                                        <Columns>
                                                            <rad:GridTemplateColumn HeaderText="Product">
                                                                <ItemTemplate>
                                                                    <b>
                                                                     <%--  'Anil B for issue 15341 on 20-03-2013
                                                                         'Set Product name as Web Name--%>
                                                                        <asp:HyperLink runat="server" NavigateUrl="" ID="link" Text='<%# DataBinder.Eval(Container, "DataItem.WebName") %>'></asp:HyperLink></b>
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                            <rad:GridBoundColumn DataField="Description" HeaderText="Description" />
                                                            <rad:GridTemplateColumn HeaderText="Quantity">
                                                                <HeaderStyle HorizontalAlign="Left" Width="57px"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                            <rad:GridTemplateColumn HeaderText="Unit Price">
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <HeaderStyle CssClass="gridcolumnwidthorderprice" Width="60px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrice" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                            <rad:GridTemplateColumn HeaderText="Total Price">
                                                                <HeaderStyle HorizontalAlign="Right" CssClass="gridcolumnwidthorderTotalprice" Width="65px">
                                                                </HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExtended" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                        </Columns>
                                                    </mastertableview>
                                                </rad:RadGrid>
                                                <%--<asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Product">
                                                                <ItemTemplate>
                                                    <b>
                                                        <asp:HyperLink runat="server" NavigateUrl="" ID="link" Text='<%# DataBinder.Eval(Container, "DataItem.Product") %>'></asp:HyperLink></b>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                                            <asp:TemplateField HeaderText="Quantity">
                                                            <HeaderStyle HorizontalAlign="Left" Width="57px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit Price">
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            <HeaderStyle CssClass="gridcolumnwidthorderprice" Width="60px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrice" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Price">
                                                            <HeaderStyle HorizontalAlign="Right" CssClass="gridcolumnwidthorderTotalprice" Width="65px"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExtended" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#F7F7DE" />
                                                        <PagerSettings Mode="Numeric" />
                                                    </asp:GridView>--%>
                                            </ContentTemplate>
                                            <%--  <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID = "grdMain" EventName="PageIndexChanging" />
                                                        </Triggers>--%>
                                        </asp:UpdatePanel>
                                        <%--End of addition by Suvarna D IssueID: 12436 --%>
                                        <%--<asp:DataGrid id="grdMain" runat="server" AutoGenerateColumns="False">
                                  <Columns>
                                    --HP Issue#8621:  set HyperLinkColumn to regular HyperLink so it can be set individually--
                                      <asp:TemplateColumn HeaderText="Product">
                                          <ItemTemplate> 
                                              <asp:HyperLink runat="server" NavigateUrl="" id="link"                                                  
                                                  Text='<%# DataBinder.Eval(Container, "DataItem.Product") %>'></asp:HyperLink>
                                          </ItemTemplate>
                                      </asp:TemplateColumn>
                                   --End Issue#8621--
                                    <asp:BoundColumn DataField="Description" HeaderText="Description"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Quantity" HeaderText="Quantity">
                                      <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Price">
                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label id="lblPrice" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Extended">
                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label id="lblExtended" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                  </Columns>
                                  <PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#F7F7DE" Mode="NumericPages"></PagerStyle>
                                </asp:DataGrid>--%><font size="2"></font>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftMoneyColumn" style="width: 94%;">
                                        <b>Sub-Total:</b>
                                    </td>
                                    <td class="RightMoneyColumn" style="padding-right: 9px;">
                                        <asp:Label runat="server" ID="lblSubTotal"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftMoneyColumn" style="width: 94%;">
                                        <b>Shipping/Handling:</b>
                                    </td>
                                    <td class="RightMoneyColumn" style="padding-right: 9px;">
                                        <asp:Label runat="server" ID="lblShipCharge"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftMoneyColumn" style="width: 94%;">
                                        <b>Sales Tax:</b>
                                    </td>
                                    <td class="RightMoneyColumn" style="padding-right: 9px;">
                                        <asp:Label runat="server" ID="lblSalesTax"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftMoneyColumn" style="width: 94%;">
                                        <b>Grand Total:</b>
                                    </td>
                                    <td class="RightMoneyColumn" style="padding-right: 9px;">
                                        <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftMoneyColumn" style="width: 94%;">
                                        <b>Payments:</b>
                                    </td>
                                    <td class="RightMoneyColumn" style="padding-right: 9px;">
                                        <asp:Label ID="lblPayments" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftMoneyColumn" style="width: 94%;">
                                        <b>Balance:</b>
                                    </td>
                                    <td class="RightMoneyColumn" style="padding-right: 9px;">
                                        <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <cc1:User runat="server" ID="User1" />
</div>
