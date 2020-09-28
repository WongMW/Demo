<%@ Control Language="VB" AutoEventWireup="false"  CodeFile="~/UserControls/Aptify_Custom__c/EditAddress__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.EditAddressControl" %>
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
<div class="content-container clearfix new-address-form">
    <div id="tblMain" runat="server" class="data-form">
        <h1>
            <asp:Label ID="lblAddressHeader" runat="server" Text=" Add New Address"></asp:Label>
        </h1>
        <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>

        <div class="cai-form">
            <div class="cai-form-content">
                <div id="trType" runat="server" class="new-address-type" Visible="False">
                    <asp:Label runat="server" ID="lblType">Type</asp:Label></b>              
                    <asp:DropDownList runat="server" ID="cmbType" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                </div>

                <div class="new-address-address">
                    <asp:Label runat="server" ID="lblName" Visible="false">Address Name</asp:Label></b>
                     <asp:DropDownList runat="server" ID="cmbAddressName" DataTextField="Name" DataValueField="Name" Visible="false"></asp:DropDownList>
                <asp:TextBox runat="server" ID="txtName" Visible="false"></asp:TextBox>
                    <asp:Label runat="server" ID="lblAddress"> Address1</asp:Label></b>
                <asp:TextBox runat="server" ID="txtAddressLine1"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Address1"
                        ForeColor="red" ControlToValidate="txtAddressLine1"></asp:RequiredFieldValidator>
                    <asp:Label runat="server" ID="Label1"> Address2</asp:Label>
                    <asp:TextBox runat="server" ID="txtAddressLine2"></asp:TextBox>
                    <asp:Label runat="server" ID="Label2"> Address3</asp:Label></b>                     
                <asp:TextBox runat="server" ID="txtAddressLine3"></asp:TextBox>
                </div>
                <div class="new-address-country">
                     <asp:Label runat="server" ID="lblCityStateZip">Town/City</asp:Label>

                    <asp:TextBox runat="server" ID="txtCity"  ></asp:TextBox>
                    <asp:DropDownList runat="server" ID="cmbState" DataTextField="State" DataValueField="State"
                        Width="50px" Visible="false">
                    </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select City"
                        ForeColor="red" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                </div>
                 <div class="new-address-country">
                    <asp:Label runat="server" ID="lblPostalCode">Postal Code</asp:Label>
                    <asp:TextBox runat="server" ID="txtZipCode"></asp:TextBox>
                </div>
                <div class="new-address-country">
                      <asp:Label runat="server" ID="Label4">County</asp:Label>
                    <asp:TextBox runat="server" ID="txtCounty"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select County"
                        ForeColor="red" ControlToValidate="txtCounty"></asp:RequiredFieldValidator>
                </div>
                 <div class="new-address-country">
                    <asp:Label runat="server" ID="lblCountry">Country</asp:Label>
                    <asp:DropDownList runat="server" ID="cmbCountry" DataTextField="Country" DataValueField="ID"
                        Width="340px" AutoPostBack="false">
                    </asp:DropDownList>
 <asp:RequiredFieldValidator ControlToValidate="cmbCountry" ID="RequiredFieldValidator1"
                        ErrorMessage="Please select a country"
                      InitialValue="0"  runat="server" ForeColor="Red" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="actions">
                    <asp:Button runat="server" ID="cmdSave" Text="Add" CssClass="submitBtn"></asp:Button>
                    <asp:Button ID="cmdCancel" runat="server" CausesValidation ="false" Text="Cancel" CssClass="submitBtn cai-btn-red-inverse"></asp:Button>
                </div>
            

        </div>
    </div>
</div>
    <cc2:User runat="Server" ID="User1" />
    <cc1:AptifyShoppingCart runat="server" ID="ShoppingCart1" />
</div>
    </ContentTemplate>
</asp:UpdatePanel>
