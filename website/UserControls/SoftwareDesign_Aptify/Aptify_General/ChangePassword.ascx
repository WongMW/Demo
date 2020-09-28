<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/ChangePassword.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ChangePassword" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%--Neha Issue 14408,02/19/13, added function for validation--%>
<script type="text/javascript" language="javascript">
    function ShowvalidationErrorMsg() {
        var cmpValidator = document.getElementById('<%=CompareValidator.ClientID%>');
        if (document.getElementById('<%= txtoldpassword.ClientID %>').value.length == 0 ||
            window.document.getElementById('<%= txtNewPassword.ClientID %>').value.length == 0
            || window.document.getElementById('<%= txtRepeat.ClientID %>').value.length == 0) {
            window.document.getElementById('<%= lblErrormessage.ClientID %>').innerHTML = "All the above fields are mandatory."
            window.document.getElementById('<%= lblErrormessage.ClientID %>').style.display = "Block";
            window.document.getElementById('<%= lblerrorLength.ClientID %>').style.display = "none"
            ValidatorEnable(cmpValidator, false);
            return false;
             }
        else {
        //Neha, Issue 14408,03/20/13, added condition for Passwordvalidation
        if (window.document.getElementById('<%= txtNewPassword.ClientID %>').value != window.document.getElementById('<%= txtRepeat.ClientID %>').value) {
            ValidatorEnable(cmpValidator, true);
            window.document.getElementById('<%= lblErrormessage.ClientID %>').style.display = "none";
            return false;
            }
        }
}
</script>
<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <div style="width: 300px;">
            <asp:Label ID="lblpwdmsg" runat="server"></asp:Label>
        </div>
        <div class="reg-form">
       <%-- <div style="padding-top: 10px; margin: 50px 50px 50px 50px;">--%>
            <%--<span height="140px" style="margin-top: 30px;">--%>
                <fieldset style="border: 1px solid gray; width: 400px; padding-bottom: 5px; margin-top: 50px;" >
                    <legend class="tdCompanyEditHeader"><h3>Change Password</h3></legend>
                    <table id="tblLogin" cellspacing="0" cellpadding="0" border="0" runat="server">
                        <tr>
                            <td valign="top" class="style1">
                                <asp:Label ID="Label6" ForeColor="Crimson" runat="server"></asp:Label>
                                <table id="tblData" border="0" runat="server" cellspacing="3" cellpadding="3">
                                    <tr>
                                        <td align="right" valign="top" class="tablecontrolsfontLogin" style="text-align: right;">
                                         <asp:Label ID="Label15" runat="server"><font color="red">*</font>Current Password:</asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtoldpassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="tablecontrolsfontLogin" style="text-align: right;">
                                         <asp:Label ID="lblPassword" runat="server"> <font color="red">*</font>New Password:</asp:Label>&nbsp;
                                        </td>
                                        <td style="padding-top: 2px;">
                                            <asp:TextBox ID="txtNewPassword" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="tablecontrolsfontLogin" style="text-align: right;">
                                          <asp:Label ID="Label18" runat="server"><font color="red">*</font>Repeat Password:</asp:Label>&nbsp;
                                        </td>
                                        <td style="padding-top: 2px;">
                                            <asp:TextBox ID="txtRepeat" runat="server" Width="175px" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="field-group" style="text-align: center;" >
                               <%--Neha Issue 14408,02/19/13, Called function on Clientclick --%>
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return ShowvalidationErrorMsg()" CssClass="submitBtn" width="40%" height="40px"/>
                                <asp:Button ID="btnCancelpop" runat="server" Text="Cancel" CssClass="submitBtn" CausesValidation="false" width="40%" height="40px"/>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </span>
        </div>
        <div>
            <table>
                <tr>
                    <td><%--Neha Issue 14408,01/24/13 added class for errormessage--%>
                        <asp:Label ID="lblErrormessage" runat="server" CssClass="lblChangePasswordErrormessage" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CompareValidator ID="CompareValidator" runat="server" CssClass="lblChangePasswordErrormessage" ControlToValidate="txtRepeat" ControlToCompare="txtNewPassword"  ErrorMessage="The new passwords must match. Please try again."></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblerrorLength" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnSave" />
        <asp:PostBackTrigger ControlID="btnCancelpop" />
    </Triggers>
</asp:UpdatePanel>
<cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
<cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
