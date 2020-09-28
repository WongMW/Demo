<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CreditCard.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CreditCard"
    Debug="true" %>
<%@ Register Src="../Aptify_Product_Catalog/CartGrid2.ascx" TagName="CartGrid2" TagPrefix="uc1" %>
<%@ Register Assembly="EBusinessShoppingCart" Namespace="Aptify.Framework.Web.eBusiness"
    TagPrefix="cc1" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!--RashmiP issue 6781 -->
<script type="text/javascript">
    function ShowHideControls() {
        var chkBillMeLater = document.getElementById('<%= chkBillMeLater.ClientID %>');
        var Validator1 = document.getElementById('<%= RequiredFieldValidator1.ClientID %>');
        var Validator2 = document.getElementById('<%= RequiredFieldValidator2.ClientID %>');

        var tblMain = document.getElementById('<%= tblMain.ClientID %>');
        var tblPO = document.getElementById('<%= tblPONum.ClientID %>');


        if (chkBillMeLater.checked == true) {
            ValidatorEnable(Validator1, false);
            ValidatorEnable(Validator2, false);
            tblMain.style.display = "none";
            tblPO.style.display = "block";
        }
        else {
            tblMain.style.display = "block";
            tblPO.style.display = "none";
            ValidatorEnable(Validator1, true);
            ValidatorEnable(Validator2, true);
        }
    }

    //    Anil B Issue 10254 on 07-03-2013
    //    Set the Sequirity textbox desabled on a leave event of the Card Number
    function SecurityDesabled() {
        var txtSecurity = document.getElementById('<%= txtCCNumber.ClientID %>');
        var hdnCCPartialNumber = document.getElementById('<%= hdnCCPartialNumber.ClientID %>');
        var txtCCSecurityNumber = document.getElementById('<%= txtCCSecurityNumber.ClientID %>');
        if (txtSecurity.value != hdnCCPartialNumber.value && txtSecurity.value != "") {
            txtCCSecurityNumber.disabled = true;
        }

    }
    //    Anil B Issue 10254 on 07-03-2013
    //    Set the Sequirity textbox enabled on a Change event of the Card Number
    function SecurityEnabled() {
        var txtSecurity = document.getElementById('<%= txtCCNumber.ClientID %>');
        var hdnCCPartialNumber = document.getElementById('<%= hdnCCPartialNumber.ClientID %>');
        hdnCCPartialNumber.value = txtSecurity.value;
        txtCCSecurityNumber.disabled = false;
    }     
</script>
<%--Nalini issue#12578--%>
<asp:UpdatePanel ID="upnlCreditCard" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional"><%--Sandeep issue#14671 20/02/2013--%>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="cmbSavedPaymentMethod" EventName="" />
        <asp:AsyncPostBackTrigger ControlID = "txtCCNumber" EventName ="" />
        <asp:AsyncPostBackTrigger ControlID = "chkBillMeLater" EventName ="" /><%--Sandeep issue#14671 20/02/2013--%>
    </Triggers>
    <ContentTemplate>
    <div style="width=90%; float: bottom">
<table id="Table1" runat="server" class="data-form">
    <tr>
        <td style="width: 117px; padding-left: 5px;" align="right">
            <b>
                <asp:Label ID="lblBillMelater" runat="server"></asp:Label></b>
        </td>
        <td  class="tdCreditCardBillMeLater">
           <asp:CheckBox ID="chkBillMeLater" runat="server" Text="" TextAlign="Left" OnCheckedChanged="chkBillMeLater_CheckedChanged" AutoPostBack="true"  />
        </td>
    </tr>
</table>
    <%--Anil B Issue 10254 on 07-03-2013
    Set the design thrugh css--%>
        <table id="tblMain" runat="server" class="data-form">
            <tr runat="server" id="trSMP">
                <td style="width: 117px; padding-left: 4px;" align="right">
                    <b>
                        <asp:Label ID="lblSavedPayment" runat="server" Text="Select your Card:"></asp:Label></b>
                </td>
                <td  valign="middle">
                    <asp:DropDownList ID="cmbSavedPaymentMethod" runat="server" AutoPostBack="true" Width="157px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="PaymentTypeSelection">
                <td class="tdCreditCardBillMeLater" align="right">
                    <b>Credit Card:</b>
                </td>
                <td class="tblCreditCartSetPadding">
                    <%--Rashmi P, Issue 10737, 1/24/13--%>
                    <table  cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td align="left">
                                <rad:RadBinaryImage ID="ImgVisa" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                                </rad:RadBinaryImage>
                            </td>
                            <td>
                                <rad:RadBinaryImage ID="ImgMasterCard" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                                </rad:RadBinaryImage>
                            </td>
                            <td>
                                <rad:RadBinaryImage ID="ImgAmericanExpress" runat="server" CssClass="creditcardImg"
                                    AutoAdjustImageControlSize="false"></rad:RadBinaryImage>
                            </td>
                            <td>
                                <rad:RadBinaryImage ID="ImgDiscover" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                                </rad:RadBinaryImage>
                            </td>
                            <td>
                                <asp:Label ID="lbl1" Text="Accepted Cards" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:DropDownList ID="cmbCreditCard" runat="server" AppendDataBoundItems="True" Width="154px"
                        Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trCardNum" runat="server">
              
                <td align="right">
                    <em class="red">*</em> <b>
                        <asp:Label ID="lblCardNo" runat="server" Text="Card Number:"></asp:Label>
                    </b>
                </td>
                <td runat="server" id="CardNumber">
                    <asp:TextBox ID="txtCCNumber" runat="server" AutoComplete="Off" AutoPostBack="true" OnTextChanged="txtCCNumber_TextChanged" 
                        EnableViewState="False" Width="155px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCCNumber"
                        Enabled="True" ErrorMessage="Credit Card # Required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
           
            <tr id="trSecurity" runat="server">
              
                <td align="right">
                    <em class="red">*</em> <b>Security # :</b>
                </td>
                <td><%--Neha, Issue 16770,set property maxlenght=4 for supproting 4 digit Security Code for Security Number Field,06/13/2013--%>
                    <asp:TextBox ID="txtCCSecurityNumber" runat="server" Width="35px" EnableViewState="false"
                        AutoComplete="Off" MaxLength="4"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Security # Required"
                        ControlToValidate="txtCCSecurityNumber" Enabled="True" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trExpiryDate" runat="server">
                <td align="right">
                    <b>Expiration Date:</b>
                </td>
                <td>
                    <asp:DropDownList ID="dropdownMonth" runat="server" Width="90px">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropdownDay" runat="server" Visible="False">
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropdownYear" runat="server" Width="63px">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="vldExpirationDate" runat="server" ControlToValidate="dropdownDay"
                        ErrorMessage="The date selected is not valid." Display="Dynamic" ForeColor="Red"></asp:CustomValidator>
                </td>
            </tr>
            <tr align="left">
                <td>
                   <asp:HiddenField ID = "hfCCNumber" runat ="server" />
                </td>
                <td>
                    <asp:CheckBox ID="chkSaveforFutureUse" Text="   Save for Future Use" runat="server" />
                </td>
            </tr>
            <tr id="trError" runat="server">
                <td>
                    &nbsp;<asp:Button ID="btnUpload" CssClass="submitBtn" runat="server" CausesValidation="False"
                        Width="60px" Text="" Style="display: none" />
                </td>
                <td>
                <%--Anil B, Issue 10254, 20/04/2013--%>
                    <asp:UpdatePanel ID="upnlError" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        
   
<table id="tblPONum" runat="server" class="data-form"><%--Sandeep issue#14671 20/02/2013--%>
    <tr id="trPONum" runat="server">
        <td class="tdPoNumberCreditCard">
            PO Number:
        </td>
        <td>
            <asp:TextBox ID="txtPONumber" runat="server" EnableViewState="False" AutoComplete="Off"
                MaxLength="25"></asp:TextBox>
        </td>
    </tr>
</table>
 <asp:HiddenField ID="hdnCCPartialNumber" ViewStateMode="Enabled" runat="server" />
<cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
<cc2:User runat="Server" ID="User1" />
</div>
 </ContentTemplate>
</asp:UpdatePanel>


