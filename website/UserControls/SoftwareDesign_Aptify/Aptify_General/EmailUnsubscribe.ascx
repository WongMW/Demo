<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/EmailUnsubscribe.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Unsubscribe" %>
<div style="text-align: center">
    <table border="0" cellpadding="0" cellspacing="0" class="tbl_def_Main" style="width: 600px">
        <tr>
            <td valign="top">
                <div style="text-align: center">
                    <br />
                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="td_content_main_data">
                                <table id="tblRequest" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    width="100%">
                                    <tr>
                                        <td colspan="2" style="padding-bottom: 8px" align="left">
                                            <asp:Label ID="lblinfo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="form_lbl" style="height: 29px; width: 81px;">
                                            Email:
                                        </td>
                                        <td align="left" style="padding-bottom: 5px; height: 29px;">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form_textbox" Width="255px"></asp:TextBox>
                                            <span style="color: #ff0000">*</span>
                                            <asp:RequiredFieldValidator ID="validateEmail" runat="server" ControlToValidate="txtEmail"
                                                Display="Dynamic" Style="color: Red" ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblInvalidEmail" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="form_lbl" valign="top" style="width: 81px">
                                            Comments:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtComments" runat="server" CssClass="form_textbox" Height="50px"
                                                TextMode="MultiLine" Width="450px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="padding-top: 10px" colspan="2">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="cmdbutton" Text="Submit" />
                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="cmdbutton" />
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblresponse" border="0" runat="server" cellpadding="0" cellspacing="0"
                                    style="width: 100%">
                                    <tr id="trResponseHeader" runat="server">
                                        <td colspan="2" style="padding-bottom: 10px; height: 33px;" align="left">
                                            You will receive an email shortly with a link that must be clicked in order to complete
                                            the unsubscribe process.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="form_lbl" valign="top" style="width: 5px; height: 3px;">
                                            <asp:Label ID="lblEmail" runat="server" Text="Email:" Width="65px"></asp:Label>
                                        </td>
                                        <td align="left" style="padding-left: 4px; width: 85%; height: 3px;">
                                            <asp:Label ID="lblResEmail" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="form_lbl" valign="top" style="width: 5px; height: 3px">
                                            <asp:Label ID="lblComments" runat="server" Text="Comments:" Width="65px"></asp:Label>
                                        </td>
                                        <td align="left" style="padding-left: 4px; width: 85%; height: 3px;">
                                            <asp:Label ID="lblResComments" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td class="td_content_col_Right" align="right" valign="top">
                <br />
                &nbsp;
            </td>
        </tr>
    </table>
</div>
