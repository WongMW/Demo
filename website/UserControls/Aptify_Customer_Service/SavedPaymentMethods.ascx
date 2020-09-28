<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SavedPaymentMethods.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.SavedPaymentMethods" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<table width="100%" class="cai-table">
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td>       
            <%--Suraj issue 14450 2/7/13  removed three step sorting ,added tooltip for sorting,edit and delete button --%>
            <rad:RadGrid ID="grdSPM" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                PagerStyle-PageSizeLabelText="Records Per Page">
                <GroupingSettings CaseSensitive="false" />
               <%-- 'Suraj Issue 15287 4/9/13, if the grid dont have any record then grid should visible and it should show "No recors " msg--%>
                <MasterTableView AllowSorting="True" NoMasterRecordsText="No Saved Payment Methods Available."
                    AllowNaturalSort="false">
                    <%--<NoRecordsTemplate>
                        There isn't any data.</NoRecordsTemplate>--%>
                    <Columns>
                        <rad:GridTemplateColumn HeaderText="Payment Type" DataField="PaymentType" SortExpression="PaymentType">
                            <ItemTemplate>
                                <asp:Label Visible="false" ID="lblPaymentTypeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PaymentTypeID") %>'></asp:Label>
                                <asp:Label ID="lblPaymentType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PaymentType") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPersonID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PersonID") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                       <%-- Anil B Issue 15408 on 08-04-2013
                        Change Header Text--%>
                        <rad:GridTemplateColumn HeaderText="Card Nickname" DataField="Name" SortExpression="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblNameonCard" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn HeaderText="Card Number" DataField="CCPartial" SortExpression="CCPartial">
                            <ItemTemplate>
                                <asp:Label ID="lblCCPartial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CCPartial") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <%--'Anil B Issue 10254 on 07-05-2013
                        'Change sorting expression--%>
                        <rad:GridTemplateColumn HeaderText="Expires on" DataField="ExpireOnDate" SortExpression="ExpireOnDate">
                            <ItemTemplate>
                                <asp:Label ID="lblExpireOnDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ExpireOn") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCCAccountNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CCAccountNumber") %>'></asp:Label>
                                <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="ImgEdit" CommandName="Update" ToolTip="Edit"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>' CausesValidation="false" />
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgDelete" CommandName="Delete" ToolTip="Delete"
                                    CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>' />
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </rad:RadGrid>         
        </td>
    </tr>
    <tr align="right">
        <td>
            <br />
            <asp:Button class="submitBtn" runat="server" ID="btnAddNewCard" Text="Add New Card"
                CausesValidation="false" />
        </td>
    </tr>
</table>
<rad:RadWindow ID="CreditcardWindow" runat="server" VisibleOnPageLoad="false" Modal="true"
    Behaviors="Move" Title="Add New Card" Height="280" Width="360" BackColor="#DADADA"
    ForeColor="#bda797" VisibleStatusbar="false" IconUrl="">
    <ContentTemplate>
        <table id="tblAddCard" runat="server" class="data-form">
            <tr>
                <td colspan="2">
                    Enter your card information: 
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    Credit Card:
                </td>
                <td>
                    <asp:DropDownList ID="cmbCreditCard" runat="server" AppendDataBoundItems="True" Width="154px"
                        Visible="false" CausesValidation="false">
                    </asp:DropDownList>
                    <table style="margin:10px 0px 10px 0px;">
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
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <em class="red">*</em> Card Number: 
                </td>
                <td>
                    <asp:TextBox ID="txtCCNumber" runat="server" AutoComplete="Off" AutoPostBack="true"
                        EnableViewState="False" CausesValidation="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCCNumber"
                        Enabled="True" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                  <%-- Anil B Issue 15408 on 08-04-2013
                        Change Header Text--%>
                    <em class="red">*</em> Card Nickname: 
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName"
                        Enabled="True" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trSecurity" runat="server">
                <td align="left" valign="top">
                    <em class="red">*</em> Security # :
                </td>
                <td>
                    <asp:TextBox ID="txtCCSecurityNumber" runat="server" Width="50px" EnableViewState="false"
                        AutoComplete="Off" MaxLength="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required"
                        ControlToValidate="txtCCSecurityNumber" Enabled="True" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    Expiration Date:
                </td>
                <td>
                    <asp:DropDownList ID="dropdownMonth" runat="server" Width="110px">
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
                    <asp:DropDownList ID="dropdownYear" runat="server" Width="110px">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="vldExpirationDate" runat="server" ControlToValidate="dropdownDay"
                        ErrorMessage="Invalid Date" Display="Dynamic" ForeColor="Red"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;<asp:Label ID="lblError" ForeColor="Red" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <p>
                    </p>
                    <asp:HiddenField ID="SPMID" runat="server" />
                    <asp:Button class="submitBtn" runat="server" ID="btnAdd" Text="Add your card" OnClick="btnAdd_Click" />&nbsp;
                    <asp:Button class="submitBtn" runat="server" ID="btnCancel" Text="Cancel" CausesValidation="false" />
                </td>
            </tr>
        </table>       
    </ContentTemplate>
</rad:RadWindow>
<cc3:User ID="User1" runat="server" />
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
