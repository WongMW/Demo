<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CreditCard__c" Debug="true" %>
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
<asp:UpdatePanel ID="upnlCreditCard" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <%--Sandeep issue#14671 20/02/2013--%>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="cmbSavedPaymentMethod" EventName="" />
        <asp:AsyncPostBackTrigger ControlID="txtCCNumber" EventName="" />
        <asp:AsyncPostBackTrigger ControlID="chkBillMeLater" EventName="" />
        <%--Sandeep issue#14671 20/02/2013--%>
    </Triggers>
    <ContentTemplate>

        <div class="credit-card-payment-info">
            <div id="Table1" runat="server" class="data-form bill-me-later" style="display:none;">
                        <asp:Label ID="lblBillMelater" CssClass="billing-label" runat="server"> : </asp:Label>
                    <div class="tdCreditCardBillMeLater">
                        <asp:CheckBox ID="chkBillMeLater" runat="server" Text="" TextAlign="Left" OnCheckedChanged="chkBillMeLater_CheckedChanged"
                            AutoPostBack="true" />
                    </div>
                    <div align="right" runat="server" id="tdReceiptlbl" visible="false">
                        <asp:Label ID="lblReceipt" Text="Receipt No :" ForeColor="#333333" Font-Size="13px"
                             runat="server"></asp:Label>
                    </div>
                    <div class="Paddingrightpayment" align="left" runat="server" id="tdReceiptNo" visible="false">
                        <asp:Label ID="lblReceiptNo" runat="server" Width="60px" ></asp:Label>
                    </div>
            </div>
            <%--Anil B Issue 10254 on 07-03-2013
    Set the design thrugh css--%>
            <div id="tblMain" runat="server" class="data-form credit-info">
                <div runat="server" id="trSMP">
                    <div>
                        <asp:Label ID="lblSavedPayment" runat="server" Text="Select your Card:"></asp:Label>
                    </div>
                    <div>
                        <asp:DropDownList ID="cmbSavedPaymentMethod" CssClass="cai-table-data" runat="server" AutoPostBack="true" Width="157px">
                        </asp:DropDownList>
                    </div>
                </div>
                <div runat="server" id="PaymentTypeSelection">                   
                    <div class="tblCreditCartSetPadding">
                        <%--Rashmi P, Issue 10737, 1/24/13--%>
                         <span class="tdCreditCardBillMeLater billing-label" style="width:150px; height: 50px;">
                            Credit card:
                        </span>
                        <div class="credit-card-icons" >
                           
                            <rad:RadBinaryImage ID="ImgAmex" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                            </rad:RadBinaryImage>
                                
                            <rad:RadBinaryImage ID="ImgDelta" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                            </rad:RadBinaryImage>
                                
                            <rad:RadBinaryImage ID="ImgDiner" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                            </rad:RadBinaryImage>
                               
                            <rad:RadBinaryImage ID="ImgJcb" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                            </rad:RadBinaryImage>
                                   
                            <rad:RadBinaryImage ID="ImgLaser" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false" visible="false">
                            </rad:RadBinaryImage>
                                   
                            <rad:RadBinaryImage ID="ImgMasterCard" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                            </rad:RadBinaryImage>
                            <rad:RadBinaryImage ID="ImgVisa" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                            </rad:RadBinaryImage>
                                    
                        </div>                       
                    </div>
                    <div class="tblCreditCartSetPadding" style="display:none;">
                     <asp:Label ID="lbl1" CssClass="billing-label" Text="Accepted Cards:" runat="server"></asp:Label>                                    
                        <div class="credit-card-icons">
                               
                            <rad:RadBinaryImage ID="ImgSolo" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                            </rad:RadBinaryImage>
                                   
                            <rad:RadBinaryImage ID="ImgSwitch" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                            </rad:RadBinaryImage>

                            <rad:RadBinaryImage ID="RadImage1" runat="server" CssClass="creditcardImg" AutoAdjustImageControlSize="false">
                            </rad:RadBinaryImage>
                        </div>
                    </div>
                </div>
                        <asp:DropDownList ID="cmbCreditCard" runat="server" AppendDataBoundItems="True" Width="154px"
                            Visible="false">
                        </asp:DropDownList>
             
                    <div id="trCardNum" runat="server">
                        <div>
                            <%--Anil B, Issue 10254, 20/04/2013--%>
                            <asp:UpdatePanel ID="upnlError" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <b><asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label></b>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <span class="billing-label" style="width:150px; height: 50px;">
                            <em class="red">*</em>
                            <asp:Label ID="lblCardNo" runat="server" Text="Card number:"></asp:Label>
                        </span>
                        <div runat="server" id="CardNumber" class="card-number">
                            <asp:TextBox ID="txtCCNumber" runat="server" AutoComplete="Off" AutoPostBack="true" Height="50px"
                                OnTextChanged="txtCCNumber_TextChanged" EnableViewState="False" Width=""></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCCNumber"
                                Enabled="True" ErrorMessage="# Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div id="trSecurity" runat="server">
                        <span class="billing-label" style="width:150px; height: 50px;">
                            <em class="red">*</em>Security number:
                        </span>
                        <div class="security-code">
                            <%--Neha, Issue 16770,set property maxlenght=4 for supproting 4 digit Security Code for Security Number Field,06/13/2013--%>
                            <asp:TextBox ID="txtCCSecurityNumber" runat="server" EnableViewState="false" Height="50px" width="50px"
                                AutoComplete="Off" MaxLength="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="# Required"
                                ControlToValidate="txtCCSecurityNumber" Enabled="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div id="trExpiryDate" runat="server">                       
                        <div class="expiry-date">
                             <span class="billing-label expire" style="width:150px; height: 50px;">
                                Expiration date :

                             </span>
                            <asp:DropDownList ID="dropdownMonth" CssClass="cai-table-data"   runat="server" Width="170px" Height="50px">
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

                            <asp:DropDownList ID="dropdownYear" CssClass="cai-table-data"  runat="server" Width="90px" Height="50px">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="vldExpirationDate" runat="server" ControlToValidate="dropdownDay"
                                ErrorMessage="The date selected is not valid." Display="Dynamic" ForeColor="Red"></asp:CustomValidator>
                            <div class="save-for-future" style="display:none;">
                                <asp:HiddenField ID="hfCCNumber" runat="server"  />
                                <asp:CheckBox ID="chkSaveforFutureUse" Text="   Save for Future Use" runat="server" class="padd-left-4px" />
                            </div>
                        </div>
                    </div>                
                    <div id="trError" runat="server">
                        <div>
                            &nbsp;<asp:Button ID="btnUpload" CssClass="submitBtn" runat="server" CausesValidation="False"
                                Width="60px" Text="" Style="display: none" />
                        </div>
                        
                    </div>
           
                <div id="tblPONum" runat="server" class="data-form">
                    <%--Sandeep issue#14671 20/02/2013--%>
                    <div id="trPONum" runat="server">
                        <td class="tdPoNumberCreditCard">
                            PO Number:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPONumber" runat="server" EnableViewState="False" AutoComplete="Off"
                                MaxLength="25"></asp:TextBox>
                        </td>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnCCPartialNumber" ViewStateMode="Enabled" runat="server" />
            <cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
            <cc2:User runat="Server" ID="User1" />   
    </ContentTemplate>
</asp:UpdatePanel>
