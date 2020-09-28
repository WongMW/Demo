<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WhoPay__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.WhoPay__c" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<script type="text/javascript">
    function ClearFields() {
        var lblIndivisualMsg = document.getElementById("lblIndivisualMsg")
        lblIndivisualMsg.innerHTML = ""
    }
    function ClearMsg() {
        var lblMsg = document.getElementById("lblMsg")
        lblMsg.innerHTML = ""
    }
</script>
<style type="text/css">
    .GridH
    {
        text-align: left;
        font-weight: bold;
        padding-left: 0px;
    }
    .h1
    {
        color: Black;
    }
</style>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1000px;">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<table width="100%">
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lblSucMsg" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="6">
                                <%--style="padding-right:18px;"--%>
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <h1>
                                    Company Pay:</h1>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlCompanyPay" runat="Server" Style="border: 1px Solid #000000;" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="6">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:ValidationSummary ID="validationSummaryCompany" runat="server" ValidationGroup="VGG"
                                                    ForeColor="Red" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" align="right">
                                                Product Category:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbProductCategory" runat="server" AutoPostBack="True" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                Product:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbProduct" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td nowrap="nowrap" align="Left" valign="top">
                                                &nbsp; Company:
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cmbSubsidarisCompney" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" align="right">
                                                Member Type:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbMemberType" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                Role:
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="cmbRole" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" align="right">
                                                Start Date:
                                            </td>
                                            <td>
                                                <%-- <uc1:DatePicker ID="StartDate" runat="server" />
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBoxEditProfile" Width="150px"></asp:TextBox>--%>
                                                <telerik:RadDatePicker ID="txtStartDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                                </telerik:RadDatePicker>
                                                <%--  <Ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate">
                                        </Ajax:CalendarExtender>--%>
                                            </td>
                                            <td nowrap="nowrap" align="right" valign="top">
                                                End Date:
                                            </td>
                                            <td valign="top">
                                                <%--   <uc1:DatePicker ID="EndDate" runat="server" />--%>
                                                <%-- <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBoxEditProfile" Width="150px"></asp:TextBox>
                                          <Ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate">
                                        </Ajax:CalendarExtender>--%>
                                                <telerik:RadDatePicker ID="txtEndDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td colspan="2">
                                                <asp:Button ID="btnADD" runat="server" Text="ADD" ValidationGroup="VGG" OnClientClick="ClearMsg()" />
                                                <asp:CompareValidator runat="server" ID="cmpCalenders" ControlToValidate="txtEndDate"
                                                    ControlToCompare="txtStartDate" Operator="GreaterThan" Type="Date" ErrorMessage=""
                                                    ValidationGroup="VGG" ForeColor="White" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Start Date"
                                                    ForeColor="White" ControlToValidate="txtStartDate" ValidationGroup="VGG"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:GridView ID="grdCompanyPay" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    DataKeyNames="ProductID" AllowPaging="true" PageSize="10">
                                                    <Columns>
                                                        <%-- <asp:TemplateField HeaderText="Company">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAllCompany" runat="server" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCompanyID" runat="server" Text='<%# Eval("CompanyID")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Product Category" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductCategoryID" runat="server" Text='<%# Eval("ProductCategoryID")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblRecordID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblProductCategory" runat="server" Text='<%# Eval("ProductCategory")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("Product")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Company" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompanyID" runat="server" Text='<%# Eval("CompanyID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Member Type" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMemberTypeID" runat="server" Text='<%# Eval("MemberTypeID")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblMemberType" runat="server" Text='<%# Eval("MemberType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Role" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRoleID" runat="server" Text='<%# Eval("RoleID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Start Date" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate", "{0:d}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="End Date" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate", "{0:d}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                                    Text="Delete" CommandArgument='<%# CType(Container,GridViewRow).RowIndex & ","  & Eval("ProductCategoryID") & "," & Eval("ProductID") & "," & Eval("StartDate") & "," & Eval("EndDate")& "," & Eval("MemberTypeID") & "," & Eval("RoleID")& "," & Eval("CompanyID") & "," & Eval("ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <h1>
                                    Individual Pay:</h1>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlIndividual" runat="Server" Style="border: 1px Solid #000000;" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="6">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Label ID="lblIndivisualMsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:ValidationSummary ID="validationIndivisual" runat="server" ValidationGroup="VGI"
                                                    ForeColor="Red" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" align="right">
                                                Pay Type:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbPayType" runat="server" Width="200px">
                                                    <asp:ListItem Text="Member Pays" Value="Member Pays">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="Firm Pays" Value="Firm Pays">
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" nowrap="nowrap">
                                                &nbsp; Product Category:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbProductCat" runat="server" AutoPostBack="True" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td nowrap="nowrap" align="right">
                                                Product:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbProductList" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" align="right">
                                                Company:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbIndividualCompany" runat="server" Width="200px" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                Member Type:
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="cmbMemberTypeIndi" runat="server" Width="200px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Role:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbRoleIndi" runat="server" Width="200px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                Person:
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="cmbPersons" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" align="right" valign="top">
                                                Start Date:
                                            </td>
                                            <td>
                                                <%-- <uc1:DatePicker ID="StartDate" runat="server" />--%>
                                                <%-- <asp:TextBox ID="txtIndiStartDate" runat="server" CssClass="txtBoxEditProfile" Width="150px"></asp:TextBox>--%>
                                                <telerik:RadDatePicker ID="txtIndiStartDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                                </telerik:RadDatePicker>
                                                <%-- <asp:ImageButton ID="btnIndStartDateCalender" AlternateText="Calendar" runat="server" ImageAlign="AbsMiddle"
                                                CausesValidation="false" ImageUrl="~/Images/btn_calendar.gif" Height="19px" Width="25px" />--%>
                                                <%-- <Ajax:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtIndiStartDate">
                                        </Ajax:CalendarExtender>--%>
                                            </td>
                                            <td nowrap="nowrap" align="right">
                                                End Date:
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtIndiEndDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                                </telerik:RadDatePicker>
                                                <%--   <uc1:DatePicker ID="EndDate" runat="server" />--%>
                                                <%-- <asp:TextBox ID="txtIndiEndDate" runat="server" CssClass="txtBoxEditProfile" Width="150px"></asp:TextBox>--%>
                                                <%--  <asp:ImageButton ID="btnIndEndDateCalender" AlternateText="Calendar" runat="server" ImageAlign="AbsMiddle"
                                                CausesValidation="false" ImageUrl="~/Images/btn_calendar.gif" Height="19px" Width="25px" />--%>
                                                <%--<Ajax:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtIndiEndDate">
                                        </Ajax:CalendarExtender>--%>
                                            </td>
                                            <td align="left" colspan="2">
                                                <asp:Button ID="btnIndiAdd" runat="server" Text="ADD" ValidationGroup="VGI" OnClientClick="ClearFields()" />
                                                <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtIndiEndDate"
                                                    ControlToCompare="txtIndiStartDate" Operator="GreaterThan" Type="Date" ErrorMessage=""
                                                    ValidationGroup="VGI" ForeColor="White" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="White"
                                                    ErrorMessage="Please Select Person" ControlToValidate="cmbPersons" ValidationGroup="VGI"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="White"
                                                    ErrorMessage="Please Select Start Date" ControlToValidate="txtIndiStartDate"
                                                    ValidationGroup="VGI"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:GridView ID="grdIndividualPay" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    AllowPaging="true" PageSize="10">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Pay Type" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayType" runat="server" Text='<%# Eval("PayType")%>'></asp:Label>
                                                                <asp:Label ID="lblRecordID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Person" ItemStyle-Width="100px" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPersonID" runat="server" Text='<%# Eval("PersonID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblPerson" runat="server" Text='<%# Eval("Person")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Category" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductCategoryID" runat="server" Text='<%# Eval("ProductCategoryID")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblProductCategory" runat="server" Text='<%# Eval("ProductCategory")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product" ItemStyle-Width="120px" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("Product")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Company" ItemStyle-Width="100px" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompanyID" runat="server" Text='<%# Eval("CompanyID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Member Type" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMemberTypeID" runat="server" Text='<%# Eval("MemberTypeID")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblMemberType" runat="server" Text='<%# Eval("MemberType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Role" ItemStyle-Width="100px" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRoleID" runat="server" Text='<%# Eval("RoleID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Start Date" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate", "{0:d}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="End Date" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate", "{0:d}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                                    Text="Delete" CommandArgument='<%# CType(Container,GridViewRow).RowIndex & "," & Eval("PersonID") & "," & Eval("ProductCategoryID") & "," & Eval("ProductID") & "," & Eval("StartDate") & "," & Eval("EndDate") &"," & Eval("PayType") &"," & Eval("MemberTypeID") &"," & Eval("RoleID") &"," & Eval("ID") &"," & Eval("CompanyID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <%--<tr>
                    <td align="right" style="padding-right:18px;">
                    <br />
                        <asp:button id="btnSubmit" runat="server" text="Submit" />
                    </td>
                </tr>--%>
</table>
<cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
