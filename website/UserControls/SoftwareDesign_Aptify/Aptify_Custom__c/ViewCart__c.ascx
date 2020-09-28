<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/ViewCart__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ViewCartControl__c" %>
<%@ Register TagPrefix="uc1" TagName="CartGrid" Src="../Aptify_Product_Catalog/CartGrid.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%-- Sue, style to make update button look like a link --%>
<%--<style>
.cai-table input[type="submit"], .updateBtn{display: inline;padding: 0px;border: 0px;color: #003D51;text-transform: capitalize;background: transparent;text-decoration: underline;}
.cai-table input[type="submit"]:hover, .updateBtn:hover{display: inline;padding: 0px;border: 0px;text-transform: capitalize;text-decoration: underline;}
</style>--%>
<%-- Susan, Issue 18331, changed ProcessIndicator --%>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>LOADING...<br /><br />
                    Please do not leave or close this window while the request is processing.</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="updatePanelButton" runat="server" ChildrenAsTriggers="True">
    <ContentTemplate>
        <div class="content-container clearfix cai-table">
            <table style="width: 100%" class="center">
                <tr>
                    <td>
                        <uc1:CartGrid ID="CartGrid" runat="server"></uc1:CartGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="vc-update-btn" style="text-align: right;">
                            <%-- Susan, Issue 18331, changed button to non-dominant colour --%>
                            <asp:Button CssClass="submitBtn updateBtn cai-btn-red-inverse" ID="cmdUpdateCart" runat="server" Text="Update"></asp:Button>
			            </div>
                    </td>
                </tr>
            </table>
            <table>
                <tr runat="server" id="tblRowNoItems">
                    <td>No items in cart
                    </td>
                </tr>
            </table>
            <div class="checkout-cart-total">
                <div id="divCampaign" runat="server" class="campaign-code">
                    <div class="tablecontrolsfontLogin" style="float:right; clear:both;">

                    <!--<div class="tablecontrolsfontLogin campaign-code-details">-->
                        <div class="campaign-box-Viewcart">
                            <asp:Label ID="lblCampaignMsg" runat="server" Visible="False">[Msg]</asp:Label>
                            <asp:Label ID="lblSuccessMsg" runat="server" Visible="False">>[Msg]</asp:Label> <%-- WongS, Modified as part of #20851 --%>
                            <%-- Susan, Issue 20002, Change position of text to show TT first --%>
                            <asp:Label ID="lblCampaignInstructions" runat="server"><strong>Do you have a training ticket/promo code?</strong></asp:Label>&nbsp;
                            <asp:Label ID="lblWarningMsg" runat="server" Text="" ForeColor="Red" Visible="False"></asp:Label> <%-- WongS, Modified as part of #20812 --%>
                    <asp:TextBox ID="txtCampaign" runat="server" CssClass="actions" style="width:100%;margin-top: 0px;" Placeholder="Enter training ticket/promo code"></asp:TextBox>
                            <div class="campaign-actions" style="">
                                <%-- Susan, Issue 18331, changed button to non-dominant colour --%>
                                <asp:Button CssClass="submitBtn input-btn cai-btn-red-inverse" style="width:100%" ID="cmdApplyCampaign" runat="server" Text="Apply"></asp:Button>
                                <asp:Button CssClass="submitBtn" ID="cmdRemoveCampaign" style="width:100%" runat="server" Text="Remove campaign" Visible="False"></asp:Button><br />
                            </div>
                        </div>
                    </div>
                    <div id="trToupCampaign" runat="server" visible="false">
                        <asp:Label ID="lblTopupWarningMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <br />
                        <asp:Label ID="lblTopupMsg" runat="server" Text="* Select topup campaign" ForeColor="Red"></asp:Label><br />
                        <telerik:RadGrid ID="grdTopupCampaignList" runat="server" AutoGenerateColumns="False"
                            AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="true">
                            <PagerStyle CssClass="sd-pager" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Topup campaign" AllowFiltering="false">
                                        <ItemTemplate>

                                            <asp:Label ID="lblCampaignID" runat="server" Text='<%# Eval("CampaignID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblMinQty" runat="server" Text='<%# Eval("MinQty__c") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblMaxQty" runat="server" Text='<%# Eval("MaxQty__c") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("CampaignID") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lnkCampaignName" runat="server" Text='<%# Eval("Name") %>' CommandName="CampaignName" CommandArgument='<%# Eval("Name") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>

                    <div id="divTotals" runat="server" class="checkout-total-table" style="clear:both;width:26%;">
                        <div class="tablecontrolsfontLogin cart-total-details ">
                            <asp:Label ID="Label4" CssClass="cart-total-label" runat="server">Sub-total:</asp:Label>
                            <asp:Label ID="lblSubTotal" CssClass="cart-total" runat="server"></asp:Label>
                        </div>

                        <div class="tablecontrolsfontLogin cart-total-details ">
                            <asp:Label ID="Label3" CssClass="cart-total-label" runat="server">VAT:</asp:Label>
                            <asp:Label ID="lblTax" CssClass="cart-total" runat="server"></asp:Label>
                        </div>

                        <div class="tablecontrolsfontLogin cart-total-details">
                            <asp:Label ID="Label2" CssClass="cart-total-label" runat="server">Shipping:</asp:Label>
                            <asp:Label ID="lblShipping" CssClass="cart-total" runat="server"></asp:Label>
                        </div>

                        <div class="tablecontrolsfontLogin cart-total-details ">
                            <asp:Label ID="Label1" CssClass="cart-total-label" runat="server">Total:</asp:Label>
                            <asp:Label ID="lblGrandTotal" CssClass="cart-total" runat="server" ForeColor="#fd4310" Font-Bold="true"   Font-Size="X-Large"></asp:Label>
                        </div>
                    </div>
                    <%--HP - Issue 6465, per Ravi the following verbiage should be used for member savings--%>
                    <span runat="server" id="spnSavings" visible="false" style="display:none;">
                        <asp:Label ID="lblTotalSavings" runat="server" ForeColor="Green">000</asp:Label>
                        <%--<span runat="server" id="spnSavings" visible="false">You will save 
			    <asp:Label id="lblTotalSavings" ForeColor="Green" Runat="server" >000</asp:Label>
                on this order since you are a valued member!--%>
                    </span>
                </div>
                <div class="tdstyleViewcart">
                    &nbsp;
                </div>
            </div>
            <%--   Issue Id # 12577 Nalini added Horrizontal Line
     <div id="divhr" runat="server" style="float: left;">
        <hr />
    </div> --%>
            <div id="tblbuttons" runat="server">
                <table width="100%">
                    <tr>
                        <td style="width: 100%;">
                            <hr />
                        </td>
                    </tr>
                </table>
                <div class="cart-actions cai-table">
                    <%--Rashmi P, Issue 5133, Add ShipmentType Selection --%>

                    <div runat="server" id="tdShipment" Visible="false">
                        <strong><font size="2">Shipping Method:</font></strong>&nbsp;<asp:DropDownList runat="server" ID="ddlShipmentType" AutoPostBack="true"></asp:DropDownList><%--Sandeep, Issue 5133, Add font size --%>
                    </div>
                    <div class="cart-continue">
                        <%-- Susan, Issue 18331, changed button to non-dominant colour --%>
                        <%-- Susan, Issue 20002, Make button a link instead and move save cart button --%>
                        <asp:Button CssClass="submitBtn no-button" ID="cmdShop" runat="server" Text="Continue shopping"></asp:Button>
                        <asp:Button CssClass="submitBtn cai-btn-red-inverse" ID="cmdSaveCart" runat="server" Text="Save cart"></asp:Button>
                    </div>
                    <%--   Issue Id # 12577 Nalini added Class tdButton
                <td class="tdstylewidthviewcart">
                    &nbsp;
                </td> --%>
                    <div class="cart-options">
                        <asp:Button CssClass="submitBtn" ID="cmdCheckOut" runat="server" Text="Checkout" CausesValidation="false"></asp:Button>
                    </div>
                    <cc1:User ID="User1" runat="server" />
                </div>
            </div>

        </div>
   <Telerik:RadWindow ID="radCampanign" runat="server" Width="500px" Height="150px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Shopping cart" Behavior="None">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblCampaignNotValidMsg" runat="server" Font-Bold="true" Text=""></asp:Label><br />
                              
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnOK" runat="server" Text="Ok" Width="70px" class="submitBtn" CausesValidation="false"/>
                                
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </Telerik:RadWindow>
    </ContentTemplate>
</asp:UpdatePanel>

<%-- Susan, Issue 18331, check if promocode textbox is empty --%>
<%-- Susan, Issue 16431, check if checkbox is ticked & load in code after update panel refresh --%>
<script>
    function pageLoad() {
        var delay = 10;
        setTimeout(function () {
            function BindControlEvents1() {
                if ($('.fa-check').css('display') == 'none') {
                    $('#baseTemplatePlaceholder_content_ViewCart_cmdUpdateCart')
                      .removeClass("cai-btn-red")
                      .addClass("cai-btn-red-inverse");
                }
                else if ($('.fa-check').css('display') == 'block') {
                    $('#baseTemplatePlaceholder_content_ViewCart_cmdUpdateCart')
                      .removeClass("cai-btn-red-inverse")
                      .addClass("cai-btn-red");
                }
            }
            BindControlEvents1();
            $('.checkbox').on('click', function () {
                BindControlEvents1();
            });
            function BindControlEvents2() {
                if ($('#baseTemplatePlaceholder_content_ViewCart_txtCampaign').data("lastval") != $('#baseTemplatePlaceholder_content_ViewCart_txtCampaign').val()) { // IF VALUE IS NOT SAME AS BFORE
                    BindControlEvents3();
                };
            };
            function BindControlEvents3() {
                if ($('#baseTemplatePlaceholder_content_ViewCart_txtCampaign').val() !== "") {
                    $('#baseTemplatePlaceholder_content_ViewCart_cmdApplyCampaign')
                .removeClass('cai-btn-red-inverse')
                        .addClass('cai-btn-red');
                }
                else {
                    $('#baseTemplatePlaceholder_content_ViewCart_cmdApplyCampaign')
                .addClass('cai-btn-red-inverse')
                        .removeClass('cai-btn-red');
                }
            };
            BindControlEvents3();
            $('#baseTemplatePlaceholder_content_ViewCart_txtCampaign').on('input', function (e) {
                BindControlEvents2();
            });
            //Susan, Issue 18381, check if QTY has changed
			function BindControlEvents4() {
                if ($('#ctl00_ctl00_baseTemplatePlaceholder_content_ViewCart_CartGrid_grdMain_ctl00_ctl04_txtQuantity').data("lastval") != $('#ctl00_ctl00_baseTemplatePlaceholder_content_ViewCart_CartGrid_grdMain_ctl00_ctl04_txtQuantity').val()) { // IF VALUE IS NOT SAME AS BFORE
                    $('#baseTemplatePlaceholder_content_ViewCart_cmdUpdateCart')
					.removeClass('cai-btn-red-inverse')
					.addClass('cai-btn-red');
                };
            };
			$('#ctl00_ctl00_baseTemplatePlaceholder_content_ViewCart_CartGrid_grdMain_ctl00_ctl04_txtQuantity').on('input', function (e) {
			    BindControlEvents4();               
			});
			if ($('#baseTemplatePlaceholder_content_ViewCart_lblWarningMsg').is(':visible')) {
			    $('#baseTemplatePlaceholder_content_ViewCart_lblWarningMsg').addClass('action-error-msg');
			    $('#baseTemplatePlaceholder_content_ViewCart_lblWarningMsg')
                    .css("float", "none")
                    .css("display", "block");
			}
			else {$('#baseTemplatePlaceholder_content_ViewCart_lblWarningMsg').removeClass('action-error-msg');
			}
            // WongS, Modified as part of #20812
			if ($('#baseTemplatePlaceholder_content_ViewCart_lblCampaignMsg').is(':visible')) {
			    $('#baseTemplatePlaceholder_content_ViewCart_lblCampaignMsg').addClass('action-error-msg');
			    $('#baseTemplatePlaceholder_content_ViewCart_lblCampaignMsg')
                    .css("float", "none")
                    .css("display", "block");
			}
			else {$('#baseTemplatePlaceholder_content_ViewCart_lblCampaignMsg').removeClass('action-error-msg');
			}

        }, delay);
    };
</script>
