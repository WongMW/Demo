<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/OrderConfirmation__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.OrderConfirmationControl__c" %>
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
        <div class="form-section-half-border">
            <h1>Thank you for your order!</h1>
        </div>
        <div class="form-section-half-border" style="text-align:center;margin-bottom:10px">
            <div class="clearfix">
                <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="javascript:window.print();" />--%>
                <asp:Button ID="btnPrint" runat="server" Text="Print Invoice"/>
                <asp:Button ID="btnReceipt" runat="server" Text="Print Receipt"></asp:Button>
            </div>
        </div>
        <div class="confirmation-message" id="idConfirmEmail" Runat="Server">
            <%-- Susan Issue 18335--%>
            <p class="info-note">
                A confirmation receipt has been sent to your <strong>registered email</strong> address. The email might take up to one hour to reach your email inbox.
            </p>
        </div>
        <%-- Susan Issue 18335--%>
        <div class="email-confirmation" style="display:none;">
            <%-- Nalini Issue 12734--%>
            <asp:TextBox ID="EmailOrderTextBox" runat="server">
            </asp:TextBox>
            <%--Suraj Issue 15210 ,2/41/13 if email validation fire the sendmail label message display none --%>
            <asp:Button CssClass="submitBtn" ID="EmailOrderButton" ValidationGroup="OrderconformationEmail"
                runat="server" Text="Send" OnClientClick="return clearLabelValue();" />
        </div>

        <%--<asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
                                ValidationExpression="^([A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}(,|$))+"
                                ControlToValidate="EmailOrderTextBox" ErrorMessage="Invalid Email Format" ValidationGroup="OrderconformationEmail"
                                ForeColor="Red"></asp:RegularExpressionValidator>--%>
                              <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
                                ControlToValidate="EmailOrderTextBox" ErrorMessage="Invalid email format"
                                ForeColor="Red"></asp:RegularExpressionValidator>
        <asp:Label ID="SendEmailLabel" runat="server"></asp:Label>
    </div>
    <div class="cai-form" style="text-align:center;margin-top:18px;padding-top:10px;border:1px solid #868686;">
        <div class="form-section-full-border">
            <h3>Manage your account</h3>
        </div>
        <div class="sf_cols">
	        <div class="sf_colsOut sf_3cols_1_33">
        	    <div id="baseTemplatePlaceholder_content_C028_Col00" class="sf_colsIn sf_3cols_1in_33">
                    <a class="cai-btn cai-btn-red-inverse" href="/CustomerService/orderhistory.aspx" style="margin-bottom:10px">See my order history details</a>
                    <p style="margin:0px 0px 20px">See a full history of your purchases</p>
		        </div>
	        </div>
	        <div class="sf_colsOut sf_3cols_2_34">
        	    <div id="baseTemplatePlaceholder_content_C028_Col01" class="sf_colsIn sf_3cols_2in_34">
                    <a class="cai-btn cai-btn-red-inverse" href="/CustomerService/MyDownloads.aspx" style="margin-bottom:10px">See my online courses/downloads</a>
                    <p style="margin:0px 0px 20px">See your purchased online courses and downloads</p>
		        </div>
	        </div>
	        <div class="sf_colsOut sf_3cols_3_33">
        	    <div id="baseTemplatePlaceholder_content_C028_Col02" class="sf_colsIn sf_3cols_3in_33">
                    <a class="cai-btn cai-btn-red-inverse" href="/CustomerService/MyMeetings.aspx" style="margin-bottom:10px"><i class="far fa-calendar-plus"></i>&nbsp;Add event details to calendar</a>
                    <p style="margin:0px 0px 20px">Add your courses/events to your calendar</p>
		        </div>
	        </div>
        </div>
    </div>
    <table id="tblMain" runat="server" class="order-confirmation">
        <tr id="tblRowMain" class="cai-table order-confirmation" runat="server">
            <td width="100%">
                <table>
                    <tr>
                        <td class="header" style="display: none;">
                            <table>
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
                                        <table>
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
                <br />
                <table width="100%">
                    <tr class="order-details-wrapper">
                        <td class="order-details">
                            <table style="padding-left: 10px;">
                                <tr>
                                    <td class="order-details-title" >
                                        <h2>Order details</h2>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id ="iOrderNumber" runat="server" visible="false">
                                    <td>
                                        <b>Order number:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="lblOrderID" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id ="iOrderType" runat="server" visible="false">
                                    <td>
                                        <b>Order type:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="lblOrderType" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td>
                                        <b>Status:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label runat="server" ID="lblOrderStatus"></asp:Label>
                                    </td>
                                </tr>
                                <tr id ="iPaymentMethod" runat="server" visible="false">
                                    <td>
                                        <b>Payment method:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label runat="server" ID="lblPayType"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Bill to:</b>
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
                                        <h2>Customer details</h2>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Customer number:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label runat="server" ID="lblBillToID"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td>
                                        <b>Shipment method:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="lblShipType" runat="server"></asp:Label>
                                        <asp:Label ID="lblShipTrackingNum" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td>
                                        <b>Date shipped:</b>
                                    </td>
                                    <td class="detail-data">
                                        <asp:Label ID="lblShipDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Ship to:</b>
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

    <div class="order-price-details cai-table mobile-table">
        <asp:UpdatePanel ID="updPanelGrid" runat="server">
            <ContentTemplate>
                <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False">
                    <MasterTableView>
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Product">
                                <ItemTemplate>
                                    <span class="mobile-label">Product:</span>
                                        <asp:HyperLink runat="server" CssClass="cai-table-data" NavigateUrl="" ID="link" Text='<%# DataBinder.Eval(Container, "DataItem.WebName") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="Description" HeaderText="Description" >
                                 <ItemTemplate>
                                    <span class="mobile-label">Description:</span>
                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Quantity">
                                <ItemTemplate>
                                    <span class="mobile-label">Quantity:</span>
                                    <asp:Label ID="lblQuantity" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Unit price">
                                <ItemTemplate>
                                    <span class="mobile-label">Unit price:</span>
                                    <asp:Label ID="lblPrice" CssClass="cai-table-data" runat="server"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn>
                                <ItemTemplate>
                                    <span class="mobile-label">Total price:</span>
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
                <b>Sub-total:</b>
            </span>
            <div class="cai-table-data right">
                <asp:Label runat="server" ID="lblSubTotal"></asp:Label>
            </div>
        </div>
        <div>
            <span class="billing-label">
                <b>Shipping/handling:</b>
            </span>
            <div class="caiu-table-data right">
                <asp:Label runat="server" ID="lblShipCharge"></asp:Label>
            </div>
        </div>
        <div>
            <span class="billing-label">
                <b>Tax:</b>
            </span>
            <div class="cai-table-data right">
                <asp:Label runat="server" ID="lblSalesTax"></asp:Label>
            </div>
        </div>
        <div>
            <span class="billing-label">
                <b>Grand total:</b>
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

    <cc1:User runat="server" ID="User1" />
</div>
<br />
<script>
    $(document).ready(function () {
        if ($("#baseTemplatePlaceholder_content_OrderConfirmation_lblPayments").text().trim() === "€0.00") {
            $("#baseTemplatePlaceholder_content_OrderConfirmation_btnReceipt").parent().addClass('hide-it');
        }
        else if ($("#baseTemplatePlaceholder_content_OrderConfirmation_lblPayments").text().trim() === "£0.00") {
            $("#baseTemplatePlaceholder_content_OrderConfirmation_btnReceipt").parent().addClass('hide-it');
        }
        else {
            $("#baseTemplatePlaceholder_content_OrderConfirmation_btnReceipt").parent().removeClass('hide-it');
        }
    });
</script>
