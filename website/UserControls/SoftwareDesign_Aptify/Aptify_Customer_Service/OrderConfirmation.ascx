<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Customer_Service/OrderConfirmation.ascx.vb"
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
        else {
            document.getElementById("<%=SendEmailLabel.ClientID%>").style.display = 'none';
            return false;

        }
    }
</script>
<div class="content-container clearfix  marg-top-40px">
    <div runat="server" class="order-confirmation">
        <h1>Order Confirmation</h1>
        <div class="confirmation-message">
            <p>
                To email this Order Confirmation, enter the email address234234 below and click the button.
            (Multiple email addresses should be separated by commas.)
            </p>
        </div>
        <div class="email-confirmation">
            <%-- Nalini Issue 12734--%>
            <asp:TextBox ID="EmailOrderTextBox" runat="server">
            </asp:TextBox>
            <%--Suraj Issue 15210 ,2/41/13 if email validation fire the sendmail label message display none --%>
            <asp:Button CssClass="submitBtn" ID="EmailOrderButton" ValidationGroup="OrderconformationEmail"
                runat="server" Text="Send" OnClientClick="return clearLabelValue();" />
        </div>
        <%--Suraj Issue 15210 ,2/11/13 RegularExpressionValidator validator --%>
        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
            ValidationExpression="^([A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}(,|$))+"
            ControlToValidate="EmailOrderTextBox" ErrorMessage="Invalid Email Format" ValidationGroup="OrderconformationEmail"
            ForeColor="Red"></asp:RegularExpressionValidator>
        <asp:Label ID="SendEmailLabel" runat="server"></asp:Label><br />
    </div>
    <!-- End Changes for Issue 4893 -->
    <table id="tblMain" runat="server" class="order-confirmation">
        <tr id="tblRowMain" class="cai-table order-confirmation" runat="server">
            <td width="100%">
                <table width="100%">
                    <tr>
                        <td class="header" style="display: none;">
                            <table width="100%">
                                <tr>
                                    <td width="75px" class="OrderConfirmationNoFontHeader">
                                        <img runat="server" src="" id="companyLogo" />
                                    </td>
                                    <td width="300px" class="OrderConfirmationNoFontHeader">
                                        <p>
                                            <b>
                                                <asp:Label ID="lblcompanyAddress" runat="server" Text="" Font-Size="Small"> </asp:Label></b>
                                        </p>
                                    </td>
                                    <td align="right" style="display: none;" width="150px" class="OrderConfirmationNoFontHeader">
                                        <table width="100%">
                                            <tr>
                                                <td>&nbsp;<b> Phone:</b>
                                                </td>
                                                <td style="text-align: center;">(202)<span style="display: none;">_</span>555-1234
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;<b> Fax:</b>
                                                </td>
                                                <td style="text-align: center;">(202)<span style="display: none;">_</span>555-4321
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
               
                <table width="100%">
                    <tr class="order-details-wrapper">
                        <td class="order-details">
                            <table style="padding-left: 10px;">
                                <tr>
                                    <td class="order-details-title">
                                        <h2>Order Details</h2>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Order Number:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="lblOrderID" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Order Type:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="lblOrderType" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Status:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label runat="server" ID="lblOrderStatus"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Payment Method:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label runat="server" ID="lblPayType"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Bill To:</b>
                                    </td>
                                    <td class="detail-data">
                                        <uc1:NameAddressBlock ID="blkBillTo" runat="server"></uc1:NameAddressBlock>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 1px;">&nbsp;
                        </td>
                    </tr>
                    <tr class="order-customer-wrapper">
                        <td class="order-customer-details">
                            <table style="padding-left: 10px;">
                                <tr>
                                    <td class="order-details-title">
                                        <h2>Customer Details</h2>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Customer Number:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label runat="server" ID="lblBillToID"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Shipment Method:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="lblShipType" runat="server"></asp:Label>
                                        <asp:Label ID="lblShipTrackingNum" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Date Shipped:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="lblShipDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Ship To:</b>
                                    </td>
                                    <td class="detail-data">
                                        <uc1:NameAddressBlock ID="blkShipTo" runat="server"></uc1:NameAddressBlock>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="order-price-details cai-table">
        <div>
            <asp:UpdatePanel ID="updPanelGrid" runat="server">
                <ContentTemplate>
                    <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False">
                        <MasterTableView>
                            <Columns>
                                <rad:GridTemplateColumn HeaderText="Product">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label runat="server" Text="Product:" CssClass="cartFieldLabel"></asp:Label>
                                            <asp:HyperLink runat="server" CssClass="cai-table-data" ID="link" Text='<%# DataBinder.Eval(Container, "DataItem.WebName") %>'></asp:HyperLink></b>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridBoundColumn DataField="Description" HeaderText="Description" />

                                <rad:GridTemplateColumn HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Quantity:" CssClass="cartFieldLabel"></asp:Label>
                                        <asp:Label ID="lblQuantity" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Unit Price">
                                    <ItemStyle></ItemStyle>
                                    <HeaderStyle CssClass="gridcolumnwidthorderprice" Width="60px" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Unit Price:" CssClass="cartFieldLabel no-desktop"></asp:Label>
                                        <asp:Label ID="lblPrice" CssClass="cai-table-data" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Total Price">
                                    <HeaderStyle CssClass="gridcolumnwidthorderTotalprice" Width="65px"></HeaderStyle>
                                    <ItemStyle></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Total Price:" CssClass="cartFieldLabel no-desktop"></asp:Label>
                                        <asp:Label ID="lblExtended" CssClass="cai-table-data" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="order-summary">
            <div>
                <span class="billing-label">
                    <b>Sub-Total:</b>
                </span>
                <div class="cai-table-data right">
                    <asp:Label runat="server" ID="lblSubTotal"></asp:Label>
                </div>
            </div>
            <div>
                <span class="billing-label">
                    <b>Shipping/Handling:</b>
                </span>
                <div class="caiu-table-data right">
                    <asp:Label runat="server" ID="lblShipCharge"></asp:Label>
                </div>
            </div>
            <div>
                <span class="billing-label">
                    <b>Sales Tax:</b>
                </span>
                <div class="cai-table-data right">
                    <asp:Label runat="server" ID="lblSalesTax"></asp:Label>
                </div>
            </div>
            <div>
                <span class="billing-label">
                    <b>Grand Total:</b>
                </span>
                <div class="cai-table-data right">
                    <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <span class="billing-label">
                    <b>Payments:</b>
                </span>
                <div class="cai-table-data right">
                    <asp:Label ID="lblPayments" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <span class="billing-label">
                    <b>Balance:</b>
                </span>
                <div class="cai-table-data right">
                    <asp:Label ID="lblBalance" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <cc1:User runat="server" ID="User1" />
    <div class="actions right">
    </div>
</div>
