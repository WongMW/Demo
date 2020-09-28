<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/ChangeAddress.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ChangeAddressControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
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
<asp:UpdatePanel ID="Updatepnl" runat="server" >
    <ContentTemplate>
<div class="content-container clearfix">
    <div id="tblMain" runat="server" class="cai-form">
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>

        <span class="form-title">Choose a
            <asp:Label ID="lblType" runat="server">Shipping/Billing</asp:Label>&nbsp;Address
        </span>

        <div class="cai-form-content">
            <h2>Address book</h2>
            <asp:DataList ID="lstAddress" runat="server"  CssClass="address-table" RepeatColumns="3" Width="100%" >
                <ItemTemplate>
                    <div style="vertical-align: bottom;" class="divstylechangeAddress">
                        <span class="label-title" style="display:none;"><%# DataBinder.Eval(Container.DataItem, "Type")%></span>
                        <%# DataBinder.Eval(Container.DataItem,"AddressLine1") %>
                        <%# DataBinder.Eval(Container.DataItem,"AddressLine2") %>
                        <%# DataBinder.Eval(Container.DataItem,"AddressLine3") %>
                        <%# DataBinder.Eval(Container.DataItem,"City") %>&nbsp;
                      <%--Commented by dipali pande to hide state from checkout page 3/31/2017--%>
                       <%-- &nbsp;<%# DataBinder.Eval(Container.DataItem,"State") %>&nbsp;--%>
                            <%# DataBinder.Eval(Container.DataItem,"ZipCode") %>
                        <br>
                        <%# DataBinder.Eval(Container.DataItem,"Country") %>
                    </div>
                    <div class="actions">
                        <asp:Button ID="cmdUseAddress" AlternateText="Use this address" runat="server" CssClass="submitBtn cai-btn-red-inverse" Text="Use this Address" />
                        <asp:Button ID="cmdEditAddress" AlternateText="Edit Address" runat="server" Text="Edit" CssClass="submitBtn" Visible="false" />
		                <asp:Button ID="cmdDeleteAddress" runat="server" Text="Delete" CssClass="submitBtn" CommandArgument ='<%# Eval("AddressID") %>' CommandName="cmdDelete"  /> <%--Added by Harish Date. 07102019 Redmine#20884--%>
                          			           
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <br />
             <asp:Button ID="btnNewAddress" OnClick="btnNewAddress_Click" runat="server" CssClass="submitBtn" Text="Click here to add a new address" />
            <asp:HyperLink ID="lnkNewAddress" runat="server" Visible="false" ></asp:HyperLink>
        </div>
    </div>
    <cc2:User runat="Server" ID="User1" />
    <cc1:AptifyShoppingCart runat="server" ID="ShoppingCart1" />
</div>
    </ContentTemplate>
</asp:UpdatePanel>
