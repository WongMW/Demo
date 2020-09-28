<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/EditAddress.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.EditAddressControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%--Nalini Issue#12578--%>
<div class="content-container clearfix new-address-form">
    <div id="tblMain" runat="server" class="data-form">
        <h1>
            <asp:Label ID="lblAddressHeader" runat="server" Text=" Add new address"></asp:Label>
        </h1>
        <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>

        <div class="cai-form">
            <div class="cai-form-content">
                <div id="trType" runat="server" class="new-address-type">
                    <asp:Label runat="server" ID="lblType">Type</asp:Label></b>              
                    <asp:DropDownList runat="server" ID="cmbType" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                </div>

                <div class="new-address-address">
                    <asp:Label runat="server" ID="lblName">Address name</asp:Label></b>
                <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                    <asp:Label runat="server" ID="lblAddress"> Address line 1</asp:Label></b>
                <asp:TextBox runat="server" ID="txtAddressLine1"></asp:TextBox>
                    <asp:Label runat="server" ID="Label1"> Address line 2</asp:Label>
                    <asp:TextBox runat="server" ID="txtAddressLine2"></asp:TextBox>
                    <asp:Label runat="server" ID="Label2"> Address line 3</asp:Label></b>                     
                <asp:TextBox runat="server" ID="txtAddressLine3"></asp:TextBox>
                </div>
                <div class="new-address-country">
                    <asp:Label runat="server" ID="lblCityStateZip">City, state ZIP</asp:Label>

                    <asp:TextBox runat="server" ID="txtCity" Width="145px"></asp:TextBox>
                    <asp:DropDownList runat="server" ID="cmbState" DataTextField="State" DataValueField="State"
                        Width="50px">
                    </asp:DropDownList>
                    <asp:TextBox runat="server" ID="txtZipCode" Width="135px"></asp:TextBox>

                    <asp:Label runat="server" ID="lblCountry">Country</asp:Label>
                    <asp:DropDownList runat="server" ID="cmbCountry" DataTextField="Country" DataValueField="ID"
                        Width="340px" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="actions">
                    <asp:Button runat="server" ID="cmdSave" Text="Add" CssClass="submitBtn"></asp:Button>
                    <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="submitBtn"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <cc2:User runat="Server" ID="User1" />
    <cc1:AptifyShoppingCart runat="server" ID="ShoppingCart1" />
</div>
