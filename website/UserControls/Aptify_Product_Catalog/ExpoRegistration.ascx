<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ExpoRegistration.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ExpoRegistrationControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="javascript" type="text/javascript">
    //Anil B for issue 15012 
    // Dynamically handle blank value for Area code and Telephone
    function EnableDisablePhoneValidation() {      
        var txtAreaCode = document.getElementById('<%= txtAreaCode.ClientID %>');
        var txtTelephone = document.getElementById('<%= txtTelephone.ClientID %>');
        var rfvAreaCode = document.getElementById('<%= rfvAreaCode.ClientID %>');
        var rfvTelephone = document.getElementById('<%= rfvTelephone.ClientID %>');
        var areaCodeValue = txtAreaCode.value;
        var telephoneValue = txtTelephone.value;

        var iAreaCodeTextCount = 0;
        var iTelephoneTextCount = 0;
        for (var i = 0; i < areaCodeValue.length; i++) {
            if (!isNaN(areaCodeValue[i])) {
                iAreaCodeTextCount++;
            }
        }

        for (var i = 0; i < telephoneValue.length; i++) {
            if (!isNaN(telephoneValue[i])) {
                iTelephoneTextCount++;
            }
        }

        if (iTelephoneTextCount == 0 && iAreaCodeTextCount == 0) {
            ValidatorEnable(rfvAreaCode, false);
            ValidatorEnable(rfvTelephone, false);            
        }
        else {
            if (iAreaCodeTextCount == 0) {
                ValidatorEnable(rfvAreaCode, true);
                ValidatorEnable(rfvTelephone, false);               
            }
            else {
                ValidatorEnable(rfvAreaCode, false);
                ValidatorEnable(rfvTelephone, true);                
            }
        }
    }

     <%--Suraj Issue 15012 ,2/25/13 ifthe user enter secondary email validation fire, then sendmail label message display none and ifthe user enter Personal Information forSecondary Contact then validation will be enable   --%>
    function ChkSecondaryInfoIsValid() 
    {
    if ( document.getElementById("<%=txtSCFName.ClientID%>").value !="" || document.getElementById("<%=txtSCLName.ClientID%>").value !="" || document.getElementById("<%=txtSCEmail.ClientID%>").value !="" )
       {
         ValidatorEnable(document.getElementById('<%= RequiredFieldValidator4.ClientID %>'), true);
         ValidatorEnable(document.getElementById('<%= RequiredFieldValidator5.ClientID %>'), true);
         ValidatorEnable(document.getElementById('<%= RequiredFieldValidator6.ClientID %>'), true);
   
       }
    else
      {
         ValidatorEnable(document.getElementById('<%= RequiredFieldValidator4.ClientID %>'), false);
         ValidatorEnable(document.getElementById('<%= RequiredFieldValidator5.ClientID %>'), false);
         ValidatorEnable(document.getElementById('<%= RequiredFieldValidator6.ClientID %>'), false);
    
      }
<%--  Suraj Issue 15210,2/8/13 ifthe page is not validate then lblError display none --%>
    if (Page_ClientValidate("VldSaveBooth"))
        {
            return true;
        }
        else 
        {
            document.getElementById("<%=lblError.ClientID%>").style.display = 'none';
            return false;
        }
    }
        
</script>
<div class="clearfix dvExpoPagePadding">
    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
    <table class="data-form" runat="server" id="tblMain">
        <tr>
            <td colspan="2">
                <%--Suraj Issue 15012 ,2/22/13 Add a validation Group and add css class ExpoRegistrationerror for lblError --%>
                <asp:ValidationSummary ID="ValSummary" ValidationGroup="VldSaveBooth" runat="server" />
                <asp:Label ID="lblError" runat="server" Visible="False" CssClass="ExpoRegistrationerror"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <%--Suraj Issue 15012 ,2/22/13 Add row to disply ExpoRegProductName --%>
                    <tr>
                        <td align="left" class="ExpoRegProductName" colspan="2">
                            <asp:Label ID="lblProductName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" class="lblExpoTopLabel">
                            Exhibitor Information
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblExh" runat="server" Text="Company:"></asp:Label>
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtExhibitor" runat="server" CssClass="txtExpoStyle" Width="300px"></asp:TextBox>
                            <%--Suraj Issue 15012 ,2/22/13 Add ValidationGroup --%>
                            <asp:RequiredFieldValidator ID="ReqtxtExhibitor" ValidationGroup="VldSaveBooth" runat="server"
                                Text="*" ControlToValidate="txtExhibitor" ErrorMessage="Please fill in the Exhibitor"
                                Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2" align="left" class="lblExpoTopLabel">
                            Personal Information for Primary Contact
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            <span class="RequiredField">*</span> First Name:
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtPCFName" runat="server" CssClass="txtExpoStyle" Width="300px"></asp:TextBox>
                            <%--Suraj Issue 15012 ,2/22/13 Add ValidationGroup --%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="VldSaveBooth"
                                runat="server" Text="*" ControlToValidate="txtPCFName" ErrorMessage="Primary contact First Name Required"
                                Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            <span class="RequiredField">*</span> Last Name:
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtPCLName" runat="server" Width="300px" CssClass="txtExpoStyle"></asp:TextBox>
                            <%--Suraj Issue 15012 ,2/22/13 Add ValidationGroup --%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="VldSaveBooth"
                                runat="server" Text="*" ControlToValidate="txtPCLName" ErrorMessage="Primary contact last name required"
                                Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            <span class="RequiredField">*</span> Email:
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtPCEmail" runat="server" Width="300px" CssClass="txtExpoStyle"></asp:TextBox>
                            <%--Suraj Issue 15012 ,2/22/13 Add ValidationGroup --%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="VldSaveBooth"
                                runat="server" Text="*" ControlToValidate="txtPCEmail" ErrorMessage="Primary contact email name required"
                                Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            <%-- Suraj Issue 15210, 2/6/13 validate email id  --%>
                            <asp:RegularExpressionValidator ID="regexPCEmailValid" ValidationGroup="VldSaveBooth"
                                runat="server" ValidationExpression="[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+(?:[A-Z]{2}|com|COM|org|ORG|net|NET|edu|EDU|gov|GOV|mil|MIL|biz|BIZ|info|INFO|mobi|MOBI|name|NAME|aero|AERO|asia|ASIA|jobs|JOBS|museum|MUSEUM|in|IN|co|CO)\b"
                                ControlToValidate="txtPCEmail" Display="None" ErrorMessage="Invalid Primary contact email "
                                Font-Bold="True" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2" align="left" class="lblExpoTopLabel">
                            Personal Information for Secondary Contact
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            First Name:
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtSCFName" runat="server" Width="300px" CssClass="txtExpoStyle"></asp:TextBox>
                            <%--Suraj Issue 15012 ,2/22/13 Add RequiredFieldValidator for Secondary FName  --%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="VldSaveBooth"
                                runat="server" Text="*" ControlToValidate="txtSCFName" ErrorMessage="Secondary contact First Name Required"
                                Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            Last Name:
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtSCLName" runat="server" Width="300px" CssClass="txtExpoStyle"></asp:TextBox>
                            <%--Suraj Issue 15012 ,2/22/13 Add RequiredFieldValidator for Secondary LName  --%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="VldSaveBooth"
                                runat="server" Text="*" ControlToValidate="txtSCLName" ErrorMessage="Secondary contact last name required"
                                Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            Email:
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtSCEmail" runat="server" Width="300px" CssClass="txtExpoStyle"></asp:TextBox>
                            <%-- Suraj Issue 15012, 2/6/13 asterisk mark if the secondary email is required  --%>
                            <asp:Label ID="lblasterisk" runat="server" Text="*" Visible="false" CssClass="RequiredField"></asp:Label>
                            <%--Suraj Issue 15012 ,2/25/13 Add RequiredFieldValidator for Secondary Email  --%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="VldSaveBooth"
                                runat="server" Text="*" ControlToValidate="txtSCEmail" ErrorMessage="Secondary contact email name required"
                                Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            <%-- Suraj Issue 15210, 2/6/13 validate email id  --%>
                            <asp:RegularExpressionValidator ID="regexSCEmailValid" ValidationGroup="VldSaveBooth"
                                runat="server" ValidationExpression="[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+(?:[A-Z]{2}|com|COM|org|ORG|net|NET|edu|EDU|gov|GOV|mil|MIL|biz|BIZ|info|INFO|mobi|MOBI|name|NAME|aero|AERO|asia|ASIA|jobs|JOBS|museum|MUSEUM|in|IN|co|CO)\b"
                                ControlToValidate="txtSCEmail" Display="None" ErrorMessage="Invalid Secondary contact email "
                                Font-Bold="True" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2" align="left" class="lblExpoTopLabel">
                                Contact Information
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftColumnExpo">
                                (Area Code) Phone:
                            </td>
                            <td class="RightColumn">
                                <rad:RadMaskedTextBox ID="txtAreaCode" CssClass="SmallWidthControl txtExpoStyle"
                                    Width="40px" runat="server" Mask="(###)">
                                    <ClientEvents OnValueChanged="EnableDisablePhoneValidation" />
                                </rad:RadMaskedTextBox>
                                <rad:RadMaskedTextBox ID="txtTelephone" Width="70px" CssClass="SmallWidthControl txtExpoStyle"
                                    runat="server" Mask="###-####">
                                    <ClientEvents OnValueChanged="EnableDisablePhoneValidation" />
                                </rad:RadMaskedTextBox>
                                <asp:RequiredFieldValidator ID="rfvAreaCode" runat="server" Text="*" ControlToValidate="txtAreaCode"
                                    ErrorMessage="Area Code Required" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                <%--Suraj Issue 15012 ,2/25/13 Add ValidationGroup  --%>
                                <asp:RequiredFieldValidator ID="rfvTelephone" ValidationGroup="VldSaveBooth" runat="server"
                                    Text="*" ControlToValidate="txtTelephone" ErrorMessage="Phone Number Required"
                                    Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2" align="left" class="lblExpoTopLabel">
                            Booth Information
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            Booth #:
                        </td>
                        <td class="RightColumn">
                            <asp:DropDownList ID="cmbBooth" runat="server" CssClass="txtExpoStyle" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Label ID="lblBooth" runat="server" Text="label not used" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            Booth Name:
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtBoothName" runat="server" Width="300px" CssClass="txtExpoStyle"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            Booth Description:
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtBoothDescription" runat="server" Width="300px" TextMode="MultiLine"
                                CssClass="txtDescExpoStyle" Style="font-family: Segoe UI, Regular;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            Weight Required:
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtWeightRequired" runat="server" CssClass="txtExpoStyle"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftColumnExpo">
                            Booth Options:
                        </td>
                        <td class="RightColumn">
                            <table>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkElectric" runat="server" DESIGNTIMEDRAGDROP="141" Text="Requires Electricity"
                                            CssClass="cb"></asp:CheckBox><br>
                                        <asp:CheckBox ID="chkWater" runat="server" Text="Requires Water" CssClass="cb"></asp:CheckBox><br>
                                        <asp:CheckBox ID="chkAir" runat="server" Text="Requires Compressed Air" CssClass="cb">
                                        </asp:CheckBox><br>
                                        <asp:CheckBox ID="chkGas" runat="server" Text="Requires Gas Hookup" CssClass="cb">
                                        </asp:CheckBox><br>
                                        <asp:CheckBox ID="chkDrain" runat="server" Text="Requires Drain" CssClass="cb"></asp:CheckBox>
                                    </td>
                                    <td>
                                        <table id="table3" runat="server">
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Your Price:
                                                </td>
                                                <td style="font-weight: bold">
                                                    <asp:Label ID="lblPrice" runat="server">Price</asp:Label>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="font-weight: bold">
                                                    <asp:Label ID="lblSurcharge" Visible="False" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--Suraj Issue 15012 ,2/25/13 Add ValidationGroup and add OnClientClick="return ChkSecondaryInfoIsValid();" function for checking the validation  --%>
                                        <asp:Button ID="cmdSave" runat="server" ValidationGroup="VldSaveBooth" CssClass="submitBtn"
                                            Text="Save" OnClientClick="return ChkSecondaryInfoIsValid();"></asp:Button>
                                    </td>
                                    <td align="left">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
    <table class="data-form" runat="server" id="tblMain1">
        <tr>
            <td>
                <asp:Label ID="lblExhibitorID" runat="server" Visible="False" DESIGNTIMEDRAGDROP="37"></asp:Label>
                <asp:Label ID="lblExhibitorName" runat="server" DESIGNTIMEDRAGDROP="37" Visible="False"></asp:Label>
                <asp:Label ID="lblContactID" runat="server" DESIGNTIMEDRAGDROP="139" Visible="False"></asp:Label>
                <asp:Label ID="lblContactName" runat="server" DESIGNTIMEDRAGDROP="139" Visible="False"></asp:Label>
                <asp:Label ID="lblPrimaryEmail" runat="server" DESIGNTIMEDRAGDROP="139" Visible="False"></asp:Label>
                <asp:Label ID="lblSecondaryContactID" runat="server" Visible="False" Width="98px"></asp:Label>
                <asp:Label ID="lblSecondaryContactName" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSecondaryEmail" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblBoothQty" runat="server" Text="BoothQty" Visible="False"></asp:Label>
                <asp:Label ID="lblUnitPrice" runat="server" Text="UnitPrice" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <rad:RadWindow ID="radDuplicateUser" runat="server" Width="650px" Height="120px"
        Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
        ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblAlert" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnok" runat="server" Text="OK" Width="70px" class="submitBtn" OnClick="btnok_Click"
                            ValidationGroup="ok" />&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </rad:RadWindow>
    <cc3:User ID="User1" runat="server" />
</div>
