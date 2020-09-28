<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/WhoPay__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.WhoPay__c" %>
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

<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1000px;">
                <table class="tblFullHeightWidth  cai-table">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">Please wait... 
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="cai-form">
            <span class="form-title">Company Pay:</span>
            <div class="cai-form-content">
                <asp:Label ID="lblSucMsg" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Panel ID="pnlCompanyPay" runat="Server">
                    <table class="table-filters">
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
                            <td><span class="label">Product Category:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbProductCategory" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td><span class="label">Product:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbProduct" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td><span class="label">Company:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbSubsidarisCompney" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td><span class="label">Member Type:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbMemberType" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td><span class="label">Role:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbRole" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>


                        <tr>
                            <td><span class="label">Start Date:</span></td>
                            <td>
                                <telerik:RadDatePicker ID="txtStartDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td><span class="label">End Date:</span></td>
                            <td>
                                <telerik:RadDatePicker ID="txtEndDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Button ID="btnADD" runat="server" Text="ADD" ValidationGroup="VGG" OnClientClick="ClearMsg()" CssClass="submitBtn" />
                                <asp:CompareValidator runat="server" ID="cmpCalenders" ControlToValidate="txtEndDate"
                                    ControlToCompare="txtStartDate" Operator="GreaterThan" Type="Date" ErrorMessage=""
                                    ValidationGroup="VGG" ForeColor="red" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Start Date"
                                    ForeColor="red" ControlToValidate="txtStartDate" ValidationGroup="VGG"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>

                    <div class="cai-table mobile-table">
                        <asp:GridView ID="grdCompanyPay" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="ProductID" AllowPaging="true" PageSize="10">
                            <PagerStyle CssClass="sd-pager" />
                            <Columns>
                                <asp:TemplateField HeaderText="Product Category" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product Category:</span>
                                        <asp:Label ID="lblProductCategoryID"  CssClass="cai-table-data" runat="server" Text='<%# Eval("ProductCategoryID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblRecordID" CssClass="cai-table-data" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblProductCategory" CssClass="cai-table-data" runat="server" Text='<%# Eval("ProductCategory")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product:</span>
                                        <asp:Label ID="lblProductID" CssClass="cai-table-data" runat="server" Text='<%# Eval("ProductID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblProduct" CssClass="cai-table-data" runat="server" Text='<%# Eval("Product")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Company:</span>
                                        <asp:Label ID="lblCompanyID" CssClass="cai-table-data" runat="server" Text='<%# Eval("CompanyID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblCompany" CssClass="cai-table-data" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Member Type" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Member Type:</span>
                                        <asp:Label ID="lblMemberTypeID" CssClass="cai-table-data" runat="server" Text='<%# Eval("MemberTypeID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblMemberType" CssClass="cai-table-data" runat="server" Text='<%# Eval("MemberType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Role" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Role:</span>
                                        <asp:Label ID="lblRoleID" CssClass="cai-table-data" runat="server" Text='<%# Eval("RoleID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblRole" CssClass="cai-table-data" runat="server" Text='<%# Eval("Role")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Date" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Start Date:</span>
                                        <asp:Label ID="lblStartDate" CssClass="cai-table-data" runat="server" Text='<%# Eval("StartDate", "{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">End Date:</span>
                                        <asp:Label ID="lblEndDate" CssClass="cai-table-data" runat="server" Text='<%# Eval("EndDate", "{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label"></span>
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" CssClass="submitBtn cai-table-data" Text="Delete"
                                            CommandArgument='<%# CType(Container, GridViewRow).RowIndex & "," & Eval("ProductCategoryID") & ","& Eval("ProductID") & "," & Eval("StartDate") & "," & Eval("EndDate")& "," & Eval("MemberTypeID") & "," & Eval("RoleID")& "," & Eval("CompanyID") & "," & Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle />
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </div>

            <span class="form-title">Individual Pay:</span>
            <div class="cai-form-content">
                <asp:Panel ID="pnlIndividual" runat="Server">
                    <table class="table-filters">
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
                            <td></span>Pay Type:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbPayType" runat="server">
                                    <asp:ListItem Text="Member Pays" Value="Member Pays">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Firm Pays" Value="Firm Pays">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td></span>Product Category:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbProductCat" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td></span>Product:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbProductList" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td></span>Company:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbIndividualCompany" runat="server" AutoPostBack="true">
                                </asp:DropDownList>

                            </td>
                            <td></span>Member Type:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbMemberTypeIndi" runat="server"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></span>Role:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbRoleIndi" runat="server"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>

                            <td></span>Person:</span></td>
                            <td >
                                <asp:DropDownList ID="cmbPersons" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></span>Start Date:</span></td>
                            <td>
                                <telerik:RadDatePicker ID="txtIndiStartDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                </telerik:RadDatePicker>

                            </td>
                            <td></span>End Date:</span></td>
                            <td>
                                <telerik:RadDatePicker ID="txtIndiEndDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Button ID="btnIndiAdd" runat="server" Text="ADD" ValidationGroup="VGI" OnClientClick="ClearFields()" CssClass="submitBtn" />
                                <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtIndiEndDate"
                                    ControlToCompare="txtIndiStartDate" Operator="GreaterThan" Type="Date" ErrorMessage=""
                                    ValidationGroup="VGI" ForeColor="red" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="red"
                                    ErrorMessage="Please Select Person" ControlToValidate="cmbPersons"
                                    ValidationGroup="VGI"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="red"
                                    ErrorMessage="Please Select Start Date" ControlToValidate="txtIndiStartDate"
                                    ValidationGroup="VGI"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>

                    <div class="cai-table mobile-table">
                        <asp:GridView ID="grdIndividualPay" runat="server" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10">
                            <PagerStyle CssClass="sd-pager" />
                            <Columns>
                                <asp:TemplateField HeaderText="Pay Type" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Pay Type:</span>
                                        <asp:Label ID="lblPayType" CssClass="cai-table-data" runat="server" Text='<%# Eval("PayType")%>'></asp:Label>
                                        <asp:Label ID="lblRecordID" CssClass="cai-table-data" runat="server" Text='<%# Eval("ID")%>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Person" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Person:</span>
                                        <asp:Label ID="lblPersonID" CssClass="cai-table-data" runat="server" Text='<%# Eval("PersonID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblPerson" CssClass="cai-table-data" runat="server" Text='<%# Eval("Person")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Category" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product Category:</span>
                                        <asp:Label ID="lblProductCategoryID" CssClass="cai-table-data" runat="server" Text='<%# Eval("ProductCategoryID")%>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblProductCategory" CssClass="cai-table-data" runat="server" Text='<%# Eval("ProductCategory")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product:</span>
                                        <asp:Label ID="lblProductID" CssClass="cai-table-data" runat="server" Text='<%# Eval("ProductID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblProduct" CssClass="cai-table-data" runat="server" Text='<%# Eval("Product")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Company:</span>
                                        <asp:Label ID="lblCompanyID" CssClass="cai-table-data" runat="server" Text='<%# Eval("CompanyID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblCompany" CssClass="cai-table-data" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Member Type" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Member Type:</span>
                                        <asp:Label ID="lblMemberTypeID" CssClass="cai-table-data" runat="server" Text='<%# Eval("MemberTypeID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblMemberType" CssClass="cai-table-data" runat="server" Text='<%# Eval("MemberType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Role" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Role:</span>
                                        <asp:Label ID="lblRoleID" CssClass="cai-table-data" runat="server" Text='<%# Eval("RoleID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblRole" CssClass="cai-table-data" runat="server" Text='<%# Eval("Role")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Date" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Start Date:</span>
                                        <asp:Label ID="lblStartDate" CssClass="cai-table-data" runat="server" Text='<%# Eval("StartDate", "{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">End Date:</span>
                                        <asp:Label ID="lblEndDate" CssClass="cai-table-data" runat="server" Text='<%# Eval("EndDate", "{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label"></span>
                                        <asp:Button ID="btnDelete" CssClass="cai-table-data" runat="server" CausesValidation="False" CommandName="Delete"
                                            Text="Delete" CommandArgument='<%# CType(Container,GridViewRow).RowIndex & "," & Eval("PersonID") & "," & Eval("ProductCategoryID") & "," & Eval("ProductID") & "," & Eval("StartDate") & "," & Eval("EndDate") &"," & Eval("PayType") &"," & Eval("MemberTypeID") &"," & Eval("RoleID") &"," & Eval("ID") &"," & Eval("CompanyID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle />
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="actions">
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="submitBtn" />
</div>
<cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
