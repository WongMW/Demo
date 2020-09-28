<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/MakePayment__c.ascx.vb"
    Debug="true" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.MakePaymentControl__c" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/CreditCard__c.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<script type="text/javascript">
    window.history.forward(-1);
</script>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatepnlorderDetail">
        <ProgressTemplate>
            <div class="dvProcessing">
                <div class="loading-bg">
                    <img src="/Images/CAITheme/bx_loader.gif" />
                    <span>LOADING...<br /><br />Please do not leave or close this window while the request is processing.</span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<div class="content-container clearfix cai-table make-payment">
    <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server">
        <ContentTemplate>
            <div>
		<asp:Label id="lblMessage" Visible="false" Text="" runat="server"></asp:Label>
                <asp:Label ID="lblNote" CssClass="note" Visible="False" Text="Note: the donation product amount can not be changed if the order is shipped." runat="server"></asp:Label>
                <%--<asp:Label id="lblSupport" Text="Contributions to Chartered Support are optional. You can enter the amount you wish to contribute." CssClass="info-note" runat="server" visible="false"></asp:Label>--%>
                <%--<asp:Label id="lblViewOrder" Text="To view your invoice, click on the 'View Order' link." CssClass="info-tip" runat="server"></asp:Label>--%>
                <p style="margin-bottom:18px;font-weight:bold;">Select the items you wish to pay for below.</p>
                <rad:RadGrid ID="grdMain" AutoGenerateColumns="False" runat="server" ValidationSettings-ValidationGroup="Od"
                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                    EnableLinqExpressions="false" AllowPaging="true" PageSize="15" CssClass="payment-data">
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowSorting="true" AllowNaturalSort="false" CssClass="payment-table-data" NoMasterRecordsText="No payment available.">
                        <SortExpressions>
                            <telerik:GridSortExpression FieldName="ID" SortOrder="Descending" />
                        </SortExpressions>
                          <Columns>
                            <rad:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <%--Performance- Make AutoPostBack false and removed server side change event--%>
                                    <asp:CheckBox ID="chkAllMakePayment" runat="server" AutoPostBack="false" CssClass="chkAllMakePayment chkAllCheckBoxes" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text="Select Order:" CssClass="mobile-label"></asp:Label>
                                    <%--Performance- Make AutoPostBack false--%>
                                    <asp:CheckBox class="cai-table-data" runat="server" ID="chkMakePayment" OnCheckedChanged="chkMakePayment_CheckedChanged"
                                        AutoPostBack="false"></asp:CheckBox>
                                    <%--Performance- Replaced visible=false to display:none--%>
                                    <asp:Label ID="lblOrderLineID" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineID") %>'
                                        runat="server" style="display:none"></asp:Label>
                                    <br />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                             <%--Below code commented by GM for Redmine #20283 and added new column without hyperlink >
                           <%-- <rad:GridHyperLinkColumn DataNavigateUrlFields="ID" DataTextField="ID" HeaderText="Order #" SortExpression="ID"
                                Target="_new" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob tooltip" DataTextFormatString="View Order {0}"  />--%>

							  <rad:GridTemplateColumn HeaderText="Order #" SortExpression="ID">
                                <ItemTemplate>
                                    <asp:Label class="no-mob" ID="OrderID"  runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                     
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                                HeaderText="Date" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob"
                                SortExpression="OrderDate"
                                DataType="System.DateTime" Visible="false" />

                            <rad:GridDateTimeColumn DataField="OrderDate"
                                HeaderText="Date" HeaderStyle-Height="100%" SortExpression="OrderDate"
                                DataType="System.DateTime" HeaderStyle-CssClass="no-mob"
                                ItemStyle-CssClass="no-mob" DataFormatString="{0:dd/MM/yyyy}" />



                            <rad:GridBoundColumn HeaderText="Product" DataField="Product"
                                HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                            <rad:GridBoundColumn HeaderText="Category" DataField="ProductCategory" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob"
                                Visible="false" />
                            <rad:GridBoundColumn HeaderText="Pay Type" DataField="PayType" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob"
                                Visible="false" />
                            <rad:GridTemplateColumn HeaderText="Price" DataField="Extended" SortExpression="Extended">
                                <ItemTemplate>

                                    <asp:Label class="cai-table-data no-mob" ID="Label4" runat="server" Text='<%#GetFormattedCurrency(Container, "Extended")%>'>
                                    </asp:Label>

                                    <asp:Label runat="server" Text="Order #:" CssClass="mobile-label"></asp:Label>
                                    <a class="cai-table-data no-desktop" href="<%= Me.OrderConfirmationURL + "?ID=" %><%# DataBinder.Eval(Container.DataItem, "ID")%>">View Order <%# DataBinder.Eval(Container.DataItem, "ID")%></a>
                                    <asp:Label runat="server" Text="Order Date:" CssClass="mobile-label"></asp:Label>
                                    <asp:Label class="cai-table-data no-desktop" ID="OrderDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:MMMM d, yyyy}")%>'></asp:Label>
                                    <asp:Label runat="server" Text="Product Name:" CssClass="mobile-label"></asp:Label>
                                    <asp:Label class="cai-table-data no-desktop" ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product")%>'></asp:Label>

                                    <asp:Label runat="server" Text="Price:" CssClass="mobile-label"></asp:Label>
                                    <asp:Label class="cai-table-data no-desktop" ID="lblPrice" runat="server" Text='<%#GetFormattedCurrency(Container, "Extended")%>'>
                                    </asp:Label>

                                </ItemTemplate>
                                <%-- <EditItemTemplate>
                            <asp:TextBox ID="txtPrice" runat="server" Text='<%#GetFormattedCurrency(Container, "Extended")%>'>
                            </asp:TextBox>
                        </EditItemTemplate>--%>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="VAT" SortExpression="VAT">
                                <ItemTemplate>
                                    <asp:Label class="no-mob" ID="Label5" size="15" runat="server"
                                        Text='<%#GetFormattedCurrency(Container,"VAT")%>'></asp:Label>
                                    <asp:Label runat="server" Text="VAT:" CssClass="mobile-label"></asp:Label>
                                    <asp:Label class="cai-table-data no-desktop" ID="lblVAT" size="15" runat="server"
                                        Text='<%#GetFormattedCurrency(Container,"VAT")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <%--Suraj issue 14450 4/17/14, apply HeaderStyle--%>
                            <rad:GridTemplateColumn HeaderText="Total" DataField="PayAmount" SortExpression="PayAmount">
                                <ItemTemplate>
                                    <asp:Label CssClass="mobile-label" ID="lblCurrencySymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                    <%--Performance- Make AutoPostBack false--%>
                                    <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="cai-table-data TextboxStylePayment"
                                        Text='<%#GetFormattedCurrencyNoSymbol(Container,"PayAmount")%>' OnTextChanged="txtPayAmt_TextChanged"
                                        AutoPostBack="false" Enabled="false"></asp:TextBox>
                                    <%--      Enabled='<%# DataBinder.Eval(Container.DataItem,"EnableFunds") %>'--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPayAmt"
                                        ErrorMessage="*" runat="server" Font-Size="8pt"></asp:RequiredFieldValidator>
                                    <%--Performance- Moved below label here for accesing its value client side--%>
                                    <asp:Label ID="lblEnableFunds" runat="server" style="display:none" Text='<%# DataBinder.Eval(Container.DataItem,"EnableFunds") %>'></asp:Label>
                                </ItemTemplate>

                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Currency Digits" Visible="false" DataField="NumDigitsAfterDecimal"
                                SortExpression="NumDigitsAfterDecimal">
                                <ItemTemplate>
                                    <%--Performance- Moved below label above for accesing its value client side--%>
                                    <%--<asp:Label ID="lblEnableFunds" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EnableFunds") %>'></asp:Label>--%>
                                    <asp:Label ID="lblNumDigitsAfterDecimal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Actual Balance" Visible="false" DataField="Balance"
                                SortExpression="Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblBalance" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PayAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Balance" HeaderStyle-CssClass="rightAlign" Visible="false" ItemStyle-CssClass="order-now">
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#GetFormattedCurrency(Container, "PayAmount")%>'></asp:Label>
                                    <asp:Label ID="lblOrderLineNumber" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineNumber") %>'
                                        runat="server" Visible="false"></asp:Label>
                                        <%--added below 2 labels for Redmine #20452--%>
									 <asp:Label ID="lblIsMembership" Text='<%# DataBinder.Eval(Container.DataItem, "IsMembership") %>'
                                        runat="server" Visible="false"></asp:Label>
									 <asp:Label ID="lblLateFeeProduct" Text='<%# DataBinder.Eval(Container.DataItem, "LateFeeProduct") %>'
                                        runat="server" Visible="false"></asp:Label>
									<%--End Redmine #20452--%>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>

                             <%-- below column added by GM for Redmine #20283--%>
							     <rad:GridTemplateColumn HeaderText="Print invoice" DataField="ID" ItemStyle-Height="60px" ItemStyle-Font-Underline="false" >
                                    <ItemTemplate>
										  <asp:LinkButton ID="lnkPrintInvoice" runat="server" Text="print" CssClass="cai-btn cai-btn-red-inverse"
                                            CommandName="Invoicereport" Font-Underline="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")  %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
                <div class="payment-message">
                    <asp:Label ID="lblTotal" Text="Total of selected order lines: "
                        runat="server"></asp:Label>
                    <asp:Label ID="txtTotal" runat="server" Font-Bold="True"></asp:Label>
                </div>
                <div class="payment-actions">
                <%--Added CausesValidation attribute as part of #20582--%>
                    <asp:Button CssClass="submitBtn" ID="btnAbatement" CausesValidation="false" runat="server" Text="Abatement"></asp:Button>
                    <asp:Button CssClass="submitBtn" ID="btnNext" runat="server" Text="Next"></asp:Button>  
           
                </div>
                <div style="clear:both;">
                    <asp:Label ID="lblError" runat="server" Visible="False" CssClass="info-error"></asp:Label>
                </div>
                <asp:Label ID="lblNoRecords" runat="server" Visible="False"><strong>No unpaid individual orders on this account.</strong></asp:Label>
            </div>
        </ContentTemplate>
	<%--Shifted Triggers tag at right location--%>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnNext" />
        </Triggers>
    </asp:UpdatePanel>
       <div style="display:none;"> <p>To make an alternative contribution to Chartered Accountants Support go to the                    
           <asp:LinkButton id="btnDonate" runat="server" Font-Underline="true">Chartered Accountants Support page</asp:LinkButton>, where there are contact details and a donation form available. </p></div>
    <cc2:User ID="User1" runat="server" />
    <%--'Anil B Issue 10254 on 20/04/2013
             Added reference for shoping cart--%>
    <cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
</div>
<%--Performance- Client side code--%>
<script type="text/javascript">
    jQuery(function ($) {
        $(document).ready(function () {

        var gridView = document.getElementById("<%=grdMain.ClientID %>");// Code added by GM for Redmine #20114
        	if (gridView != null) { // Code added by GM for Redmine #20114
        		var rows = gridView.getElementsByTagName("tr")// Code added by GM for Redmine #20114
        		if (rows.length > 0) // Code added by GM for Redmine #20114
        		{
        		    //Calling below function on page load check/uncheck all payment checkbox and calculate total, in case of Back button click of summary page
        		    checkUncheckAll();
        		    calculateTotal();
        		    $("[id*=grdMain][id*=txtPayAmt]").each(function (index, value) {
        		        if ($("[id*=grdMain][id*=lblEnableFunds]")[index].innerText == "0") {
        		            $(this).addClass("label-look");
        		            $("[id*=grdMain][id*=txtPayAmt]")[index].disabled = true;
        		        }
        		        else {
        		            $("[id*=grdMain][id*=txtPayAmt]")[index].disabled = false;
        		        }
        		    });

        		    $("[id*=grdMain][id*=chkAllMakePayment]").change(function () {
        		        if ($(this).is(":checked")) {
        		            $("[id*=grdMain][id*=txtPayAmt]").each(function (index, value) {
        		                $("[id*=grdMain][id*=chkMakePayment]")[index].checked = true;
        		                $($($("[id*=grdMain][id*=chkMakePayment]")[index]).parent().find("span")[0]).removeClass("checkbox");
        		                $($($("[id*=grdMain][id*=chkMakePayment]")[index]).parent().find("span")[0]).addClass("checkbox checked");
        		                //if ($("[id*=grdMain][id*=lblEnableFunds]")[index].innerText == "1") {
        		                //    $("[id*=grdMain][id*=txtPayAmt]")[index].disabled = false;}
        		                //else {$("[id*=grdMain][id*=txtPayAmt]")[index].disabled = true;}
        		            });
        		        }
        		        else {
        		            $("[id*=grdMain][id*=txtPayAmt]").each(function (index, value) {
        		                $("[id*=grdMain][id*=chkMakePayment]")[index].checked = false;
        		                $($($("[id*=grdMain][id*=chkMakePayment]")[index]).parent().find("span")[0]).removeClass("checkbox checked");
        		                $($($("[id*=grdMain][id*=chkMakePayment]")[index]).parent().find("span")[0]).addClass("checkbox");
        		                //$("[id*=grdMain][id*=txtPayAmt]")[index].disabled = true;
        		                $('.checked').parent().parent().parent().removeClass("checked-item");
        		                $('.checked').parent().parent().removeClass("checked-item");
        		            });
        		        }
        		        calculateTotal();
        		    });

        		    $("[id*=grdMain][id*=chkMakePayment]").change(function () {
        		        if ($(this).is(":checked")) {
        		            //debugger;
        		            //if ($($(this).closest("tr").find("[id*=lblEnableFunds]"))[0].innerText == "1") {
        		            //$($(this).closest("tr").find("[id*=txtPayAmt]"))[0].disabled = false;}
        		            checkUncheckAll();
        		        }
        		        else {
        		            //$($(this).closest("tr").find("[id*=txtPayAmt]"))[0].disabled = true;
        		            $("[id*=grdMain][id*=chkAllMakePayment]")[0].checked = false;
        		            $($("[id*=grdMain][id*=chkAllMakePayment]").parent().children()[0]).removeClass("checkbox checked");
        		            $($("[id*=grdMain][id*=chkAllMakePayment]").parent().children()[0]).addClass("checkbox");
        		        }
        		        calculateTotal();
        		    });

        		    $("[id*=grdMain][id*=txtPayAmt]").change(function () {
        		        if ($(this)[0].value == "") {
        		            var price = $($(this).closest("tr").find("[id*=lblPrice]"))[0].innerText.substring(1);
        		            var vat = $($(this).closest("tr").find("[id*=lblVAT]"))[0].innerText.substring(1);
        		            $(this)[0].value = formatTotal(parsePotentiallyGroupedFloat(price) + parsePotentiallyGroupedFloat(vat));
        		        }
        		        calculateTotal();
        		    });
        		} // Code end by GM for Redmine #20114
      } // Code end by GM for Redmine #20114


            

            function calculateTotal() {

                var txtTotal = 0.00;
                var txtAmt = 0.00;
                cSymbole = $("[id*=grdMain][id*=lblVAT]")[0].innerText.charAt(0);

                $("[id*=grdMain][id*=txtPayAmt]").each(function (index, value) {
                    if ($("[id*=grdMain][id*=chkMakePayment]")[index].checked) {
                        txtAmt = parseFloat(value.value);
                        txtTotal += parsePotentiallyGroupedFloat(txtAmt.toFixed(2));
                    }

                });
                document.getElementById("<%=txtTotal.ClientID%>").innerText = cSymbole + formatTotal(txtTotal);
            }

            function parsePotentiallyGroupedFloat(stringValue) {
                stringValue = stringValue.trim();
                var result = stringValue.replace(/[^0-9]/g, '');
                if (/[,\.]\d{2}$/.test(stringValue)) {
                    result = result.replace(/(\d{2})$/, '.$1');
                }
                return parseFloat(result);
            }

            function formatTotal(objTotalAmt) {
                var parts = objTotalAmt.toFixed(2).toString().split(".");
                var result = parts[0].replace(/\B(?=(\d{3})+(?=$))/g, ",") + (parts[1] ? "." + parts[1] : "");
                return result;
            }

            function checkUncheckAll() {
                var bChecked = true;
                $("[id*=grdMain][id*=txtPayAmt]").each(function (index, value) {
                    if ($("[id*=grdMain][id*=chkMakePayment]")[index].checked == false) {
                        bChecked = false;
                    }
                    else {
                        if ($("[id*=grdMain][id*=lblEnableFunds]")[index].innerText == "1") {
                            $("[id*=grdMain][id*=txtPayAmt]")[index].disabled = false;
                        }
                    }
                });
                $("[id*=grdMain][id*=chkAllMakePayment]")[0].checked = bChecked;
                if (bChecked == true) {
                    $($("[id*=grdMain][id*=chkAllMakePayment]").parent().children()[0]).removeClass("checkbox");
                    $($("[id*=grdMain][id*=chkAllMakePayment]").parent().children()[0]).addClass("checkbox checked");
                }
                else {
                    $($("[id*=grdMain][id*=chkAllMakePayment]").parent().children()[0]).removeClass("checkbox checked");
                    $($("[id*=grdMain][id*=chkAllMakePayment]").parent().children()[0]).addClass("checkbox");
                }
                /*Susan Wong #19029 Highlight selected rows*/
                $("[id*=grdMain][id*=chkMakePayment]").change(function () {
                    if ($(this).is(":checked")) {
                        checkUncheckAll();
                        $(this).closest('td').addClass("checked-item");
                        $(this).closest('td').siblings().addClass("checked-item");
                        $(this).closest('tr').addClass("checked-item");
                    }
                    else {
                        $("[id*=grdMain][id*=chkAllMakePayment]")[0].checked = false;
                        $($("[id*=grdMain][id*=chkAllMakePayment]").parent().children()[0]).removeClass("checkbox checked")
                        .addClass("checkbox");
                        $(this).closest('td').removeClass("checked-item");
                        $(this).closest('td').siblings().removeClass("checked-item");
                        $(this).closest('tr').removeClass("checked-item");
                    }
                    calculateTotal();
                });
            }
        });
    });
</script>

<script>
    function pageLoad() {
        var delay = 10;
        setTimeout(function () {
            $(document).ready(function () {
                var logolink = $('.tooltip').attr("alt");
                $(".tooltip").attr("alt", 'Click to view invoice');
            });
        }, delay);
    };
</script>
