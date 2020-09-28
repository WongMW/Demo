<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EditAddress.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.EditAddressControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%--Nalini Issue#12578--%>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <b>
                  <h1>  <asp:Label ID="lblAddressHeader" runat="server" Text=" Add New Address"></asp:Label></b></h1>
                <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr id="trType" runat="server">
                        <td>
                            <b>
                                <asp:Label runat="server" ID="lblType">Type</asp:Label></b>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="cmbType" DataTextField="Name" DataValueField="ID"
                                Width="145px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label runat="server" ID="lblName">Address Name</asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtName" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label runat="server" ID="lblAddress"> Address1</asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAddressLine1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label runat="server" ID="Label1"> Address2</asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAddressLine2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label runat="server" ID="Label2"> Address3</asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAddressLine3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label runat="server" ID="lblCityStateZip">City, State ZIP</asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCity" Width="145px"></asp:TextBox>
                            <asp:DropDownList runat="server" ID="cmbState" DataTextField="State" DataValueField="State"
                                Width="50px">
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="txtZipCode" Width="135px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label runat="server" ID="lblCountry">Country</asp:Label></b>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="cmbCountry" DataTextField="Country" DataValueField="ID"
                                Width="340px" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="cmdSave" Text="Add"></asp:Button>
                            <asp:Button ID="cmdCancel" runat="server" Text="Cancel" ></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <cc2:User runat="Server" ID="User1" />
    <cc1:AptifyShoppingCart runat="server" ID="ShoppingCart1" />
</div>
