<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ViewCart__c.ascx.vb" Inherits="ViewCartControl__c" %>
<%@ Register TagPrefix="uc1" TagName="CartGrid" Src="../Aptify_Product_Catalog/CartGrid.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--Amruta, Issue 16769, Added ProcessIndicator.--%>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updatePanelMain" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="updatePanelButton">
        <ProgressTemplate>
            <div class="dvProcessing" style="position:fixed; top:0; right:0;" >
                <table class="tblFullHeightWidth">
                    <tr >
                        <td  class="tdProcessing" style="vertical-align: middle">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="content-container clearfix">
    <table Style="width:100%">
        <tr>
            <td>
                <uc1:CartGrid ID="CartGrid" runat="server"></uc1:CartGrid>
            </td>
        </tr>
    </table>
    <table>
        <tr runat="server" id="tblRowNoItems">
            <td>
                No Items In Cart
            </td>
        </tr>
    </table>
    <div style="width: 667px; float: left;" id="divCampaign" runat="server">
        <table>
        <tr>
            <td class="tablecontrolsfontLogin">
                <p class="campaignboxViewcart" style="padding-left: 7px; padding-top: 7px;">
                    <asp:Label ID="lblCampaignMsg" runat="server" Visible="False">[Msg]</asp:Label>
                    <asp:Label ID="lblCampaignInstructions" runat="server">If you have a campaign code, please enter:</asp:Label>&nbsp;
                    <asp:TextBox ID="txtCampaign" runat="server" Width="70px"></asp:TextBox>
                    <asp:Button CssClass="submitBtn" ID="cmdApplyCampaign" runat="server" Text="Apply"></asp:Button>
                    <asp:Button CssClass="submitBtn" ID="cmdRemoveCampaign" runat="server" Text="Remove Campaign" Visible="False">
                    </asp:Button>
                </p>
            </td>
            </tr>
        </table>
    </div>
    <div style="width: 300px; float: left;" id="divTotals" runat="server">
        <table>
            <tr>
            <td>
                <table>
                    <tr>
                            <td class="tablecontrolsfontLogin" style="width: 155px; text-align: right;">
                            <asp:Label ID="Label4" runat="server">Sub-Total:</asp:Label>
                        </td>
                            <td style="text-align: right; width: 110px;">
                            <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                            <td class="tablecontrolsfontLogin" style="width: 155px; text-align: right;">
                              <%--Changes by Ganesh on 15/04/2014 --%>
                            <asp:Label ID="Label3" runat="server">VAT:</asp:Label>
                        </td>
                            <td style="text-align: right; width: 110px;">
                            <asp:Label ID="lblTax" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                            <td class="tablecontrolsfontLogin" style="width: 155px; text-align: right;">
                            <asp:Label ID="Label2" runat="server">Shipping:</asp:Label>
                        </td>
                            <td style="text-align: right; width: 110px;">
                            <asp:Label ID="lblShipping" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                            <td class="tablecontrolsfontLogin" style="width: 155px; text-align: right;">
                                <asp:Label ID="Label1" runat="server">Total:</asp:Label>
                        </td>
                            <td style="text-align: right; width: 110px;">
                            <asp:Label ID="lblGrandTotal" runat="server" ForeColor="#fd4310"></asp:Label>
                        </td>
                    </tr>
                </table>
                <%--HP - Issue 6465, per Ravi the following verbiage should be used for member savings--%>
                <span runat="server" id="spnSavings" visible="false">
                    <asp:Label ID="lblTotalSavings" runat="server" ForeColor="Green">000</asp:Label>
                    <%--<span runat="server" id="spnSavings" visible="false">You will save 
			    <asp:Label id="lblTotalSavings" ForeColor="Green" Runat="server" >000</asp:Label>
                on this order since you are a valued member!--%>
                </span>
            </td>
                <td class="tdstyleViewcart">
                    &nbsp;
                </td>
        </tr>
    </table>
    </div>
   <%--   Issue Id # 12577 Nalini added Horrizontal Line
     <div id="divhr" runat="server" style="float: left;">
        <hr />
    </div> --%>
    <div id="tblbuttons" runat="server" style="float: left; width: 100%;">
        <table width="100%">
            <tr>
                <td style="width: 100%;">
                    <hr />
                </td>
            </tr>
        </table>
        <table width="100%">
            <%--Rashmi P, Issue 5133, Add ShipmentType Selection --%>
            <tr>
            <td runat = "server" id ="tdShipment" >
              <strong><font size="2">Shipping Method:</font></strong>&nbsp;<asp:DropDownList runat = "server" ID = "ddlShipmentType" AutoPostBack="true"></asp:DropDownList><%--Sandeep, Issue 5133, Add font size --%>
            </td>
        </tr>
            <tr >
                <td>
                    <asp:Button CssClass="submitBtn" ID="cmdShop" runat="server" Text="Continue Shopping" Height="26px"></asp:Button>
                </td>
                <%--   Issue Id # 12577 Nalini added Class tdButton
                <td class="tdstylewidthviewcart">
                    &nbsp;
                </td> --%>
                <td style="padding-right: 30px; text-align: right;">                  
                   <asp:UpdatePanel ID="updatePanelButton" runat="server" ChildrenAsTriggers="True">
                        <ContentTemplate>
                            <asp:Button CssClass="submitBtn" ID="cmdUpdateCart" runat="server" Text="Update" Height="26px"></asp:Button>
                            <asp:Button CssClass="submitBtn" ID="cmdSaveCart" runat="server" Text="Save Cart" Height="26px"></asp:Button>
                            <asp:Button CssClass="submitBtn" ID="cmdCheckOut" runat="server" Text="Check Out" Height="26px"></asp:Button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <cc1:User ID="User1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</div>
<p>
    &nbsp;</p>
