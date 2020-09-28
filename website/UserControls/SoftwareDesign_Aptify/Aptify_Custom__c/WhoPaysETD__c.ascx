<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/WhoPaysETD__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.WhoPaysETD__c" %>
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
                <table class="tblFullHeightWidth">
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
            <span class="form-title">Company pays:</span>

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
                            <td><span>Product category:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbProductCategory" runat="server" AutoPostBack="True" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td><span>Product:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbProduct" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td><span>Route of entry:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbRouteOfEntry" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td><span>Member type:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbMemberType" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td><span>Role:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbRole" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td><span>Attempts:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbCompanyAttempts" runat="server" Width="200px">
                                    <asp:ListItem>-- Select attempt --</asp:ListItem>
                                    <asp:ListItem>First attempt</asp:ListItem>
                                    <asp:ListItem>Re-sit attempt</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td><span>Company:</span></td>
                            <td>

                                <asp:DropDownList ID="cmbSubsidarisCompney" runat="server" Width="200px">
                                </asp:DropDownList>

                            </td>
                            <td><span>Start date:</span></td>
                            <td>
                                <telerik:RadDatePicker ID="txtStartDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td><span>End date:</span></td>
                            <td>
                                <telerik:RadDatePicker ID="txtEndDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>

                    </table>

                    <div class="actions">
                        <asp:Button ID="btnADD" CssClass="submitBtn" runat="server" Text="ADD" ValidationGroup="VGG" OnClientClick="ClearMsg()" /></td>
                    </div>


                    <div class="cai-table mobile-table">
                        <asp:GridView ID="grdCompanyPay" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="ProductID" AllowPaging="true" PageSize="10">
                            <PagerStyle CssClass="sd-pager" />
                            <Columns>
                                <asp:TemplateField HeaderText="Product category" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product category:</span>
                                        <asp:Label ID="lblRecordID" runat="server" Text='<%# Eval("ID")%>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblProductCategoryID" runat="server" Text='<%# Eval("ProductCategoryID")%>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblProductCategory" CssClass="cai-table-data" runat="server" Text='<%# Eval("ProductCategory")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product:</span>
                                        <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblProduct" CssClass="cai-table-data" runat="server" Text='<%# Eval("Product")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Member type" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Member type:</span>
                                        <asp:Label ID="lblMemberTypeID" runat="server" Text='<%# Eval("MemberTypeID")%>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblMemberType" CssClass="cai-table-data" runat="server" Text='<%# Eval("MemberType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Role:</span>
                                        <asp:Label ID="lblRoleID" runat="server" Text='<%# Eval("RoleID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblRole" CssClass="cai-table-data" runat="server" Text='<%# Eval("Role")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Company:</span>
                                        <asp:Label ID="lblCompanyID" runat="server" Text='<%# Eval("CompanyID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblCompany" CssClass="cai-table-data" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start date" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Start date:</span>
                                        <asp:Label ID="lblStartDate" CssClass="cai-table-data" runat="server" Text='<%# Eval("StartDate", "{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End date" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">End date:</span>
                                        <asp:Label ID="lblEndDate" CssClass="cai-table-data" runat="server" Text='<%# Eval("EndDate", "{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Route of entry" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Route of entry:</span>
                                        <asp:Label ID="lblRouteOfEntryID" runat="server" Text='<%# Eval("RouteOfEntryID")%>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblRouteOfEntry" CssClass="cai-table-data" runat="server" Text='<%# Eval("RouteOfEntry")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attempt" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Attempt:</span>
                                        <asp:Label ID="lblAttempts" CssClass="cai-table-data" runat="server" Text='<%# Eval("Attempts")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                            Visible='<%# IIf(Eval("NonEditableForFirm") = "False", true, false)%>' Text="Delete"
                                            CommandArgument='<%# CType(Container,GridViewRow).RowIndex & ","  & Eval("ProductCategoryID") & "," & Eval("ProductID") & "," & Eval("StartDate") & "," & Eval("EndDate")& "," & Eval("MemberTypeID") & "," & Eval("RoleID")  & "," & Eval("RouteOfEntryID") & "," & Eval("Attempts") & "," & Eval("CompanyID") & "," & Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <%-- <HeaderStyle co />--%>
                        </asp:GridView>
                    </div>

                    <div>
                        <asp:RequiredFieldValidator InitialValue="-- Select product category--" ID="Req_ID"
                            Display="Dynamic" ValidationGroup="VGG" runat="server" ControlToValidate="cmbProductCategory"
                            Text="" ErrorMessage="Please select product category" ForeColor="red"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator InitialValue="-- Select route of entry --" ID="RequiredFieldValidator4"
                            Display="Dynamic" ValidationGroup="VGG" runat="server" ControlToValidate="cmbRouteOfEntry"
                            Text="" ErrorMessage="Please select route of entry" ForeColor="red"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator InitialValue="-- Select attempt --" ID="RequiredFieldValidator5"
                            Display="Dynamic" ValidationGroup="VGG" runat="server" ControlToValidate="cmbCompanyAttempts"
                            Text="" ErrorMessage="Please select attempt" ForeColor="red"></asp:RequiredFieldValidator>
                        <asp:CompareValidator runat="server" ID="cmpCalenders" ControlToValidate="txtEndDate"
                            ControlToCompare="txtStartDate" Operator="GreaterThan" Type="Date" ErrorMessage=""
                            ValidationGroup="VGG" ForeColor="red" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select start date"
                            ForeColor="red" ControlToValidate="txtStartDate" ValidationGroup="VGG"></asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <div class="cai-form">
            <span class="form-title">Individual pays:</span>

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
                            <td><span>Pay type:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbPayType" runat="server" Width="200px">
                                    <asp:ListItem Text="Member pays" Value="Member Pays">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Firm pays" Value="Firm Pays">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><span>Product category:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbProductCat" runat="server" AutoPostBack="True" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td><span>Route of entry:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbIndividualRouteOfEntry" runat="server" Width="200px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td><span>Product:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbProductList" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td><span>Member type:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbMemberTypeIndi" runat="server" Width="200px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td><span>Attempts:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbIndivisualAttempts" runat="server" Width="200px">
                                    <asp:ListItem>-- Select attempt --</asp:ListItem>
                                    <asp:ListItem>First attempt</asp:ListItem>
                                    <asp:ListItem>Re-sit attempt</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><span>Role:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbRoleIndi" runat="server" Width="200px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td><span>Company:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbIndividualCompany" runat="server" Width="200px" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td><span>Person:</span></td>
                            <td>
                                <asp:DropDownList ID="cmbPersons" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>


                        <tr>
                            <td><span>Start date:</span></td>
                            <td>

                                <telerik:RadDatePicker ID="txtIndiStartDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                </telerik:RadDatePicker>

                            </td>
                            <td><span>End date:</span></td>
                            <td>
                                <telerik:RadDatePicker ID="txtIndiEndDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>

                    </table>

                    <div class="actions">
                        <asp:Button ID="btnIndiAdd" runat="server" CssClass="submitBtn" Text="ADD" ValidationGroup="VGI" OnClientClick="ClearFields()" />
                    </div>


                    <div class="cai-table mobile-table">
                        <asp:GridView ID="grdIndividualPay" runat="server" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10">
                            <PagerStyle CssClass="sd-pager" />
                            <Columns>
                                <asp:TemplateField HeaderText="Pay type" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Pay type:</span>
                                        <asp:Label ID="lblPayType" CssClass="cai-table-data" runat="server" Text='<%# Eval("PayType")%>'></asp:Label>
                                        <asp:Label ID="lblRecordID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Person" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Person:</span>
                                        <asp:Label ID="lblPersonID" runat="server" Text='<%# Eval("PersonID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblPerson" CssClass="cai-table-data" runat="server" Text='<%# Eval("Person")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product category" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product category:</span>
                                        <asp:Label ID="lblProductCategoryID" runat="server" Text='<%# Eval("ProductCategoryID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblProductCategory" CssClass="cai-table-data" runat="server" Text='<%# Eval("ProductCategory")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product:</span>
                                        <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblProduct" CssClass="cai-table-data" runat="server" Text='<%# Eval("Product")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Member type" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Member type:</span>
                                        <asp:Label ID="lblMemberTypeID" runat="server" Text='<%# Eval("MemberTypeID")%>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblMemberType" CssClass="cai-table-data" runat="server" Text='<%# Eval("MemberType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Role" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Role:</span>
                                        <asp:Label ID="lblRoleID" runat="server" Text='<%# Eval("RoleID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblRole" CssClass="cai-table-data" runat="server" Text='<%# Eval("Role")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Company:</span>
                                        <asp:Label ID="lblCompanyID" runat="server" Text='<%# Eval("CompanyID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblCompany" CssClass="cai-table-data" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start date" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Start date:</span>
                                        <asp:Label ID="lblStartDate" CssClass="cai-table-data" runat="server" Text='<%# Eval("StartDate", "{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End date" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">End date:</span>
                                        <asp:Label ID="lblEndDate" CssClass="cai-table-data" runat="server" Text='<%# Eval("EndDate", "{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Route of entry" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Route of entry:</span>
                                        <asp:Label ID="lblRouteOfEntryID" runat="server" Text='<%# Eval("RouteOfEntryID")%>'
                                            Visible="false"></asp:Label>
                                        <asp:Label CssClass="cai-table-data" ID="lblRouteOfEntry" runat="server" Text='<%# Eval("RouteOfEntry")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attempt" HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Attempt:</span>
                                        <asp:Label CssClass="cai-table-data" ID="lblAttempts" runat="server" Text='<%# Eval("Attempts")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="rgHeader">
                                    <ItemTemplate>
                                        <span class="mobile-label">Product:</span>
                                        <asp:Button ID="btnDelete" runat="server" CssClass="cai-table-data" CausesValidation="False" CommandName="Delete"
                                            Visible='<%# IIf(Eval("NonEditableForFirm") = "False", true, false)%>' Text="Delete"
                                            CommandArgument='<%# CType(Container,GridViewRow).RowIndex & "," & Eval("PersonID") & "," & Eval("ProductCategoryID") & "," & Eval("ProductID") & "," & Eval("StartDate") & "," & Eval("EndDate") &"," & Eval("PayType") &"," & Eval("MemberTypeID") &"," & Eval("RoleID")& "," & Eval("RouteOfEntryID") & "," & Eval("Attempts") & "," & Eval("CompanyID") &"," & Eval("ID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle />
                        </asp:GridView>
                    </div>

                    <div>
                        <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtIndiEndDate"
                            ControlToCompare="txtIndiStartDate" Operator="GreaterThan" Type="Date" ErrorMessage=""
                            ValidationGroup="VGI" ForeColor="red" />
                        <asp:RequiredFieldValidator InitialValue="-- Select product category--" ID="RequiredFieldValidator6"
                            Display="Dynamic" ValidationGroup="VGI" runat="server" ControlToValidate="cmbProductCat"
                            Text="" ErrorMessage="Please select product category" ForeColor="red"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator InitialValue="-- Select route of entry --" ID="RequiredFieldValidator7"
                            Display="Dynamic" ValidationGroup="VGI" runat="server" ControlToValidate="cmbIndividualRouteOfEntry"
                            Text="" ErrorMessage="Please select route of entry" ForeColor="red"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator InitialValue="-- Select attempt --" ID="RequiredFieldValidator8"
                            Display="Dynamic" ValidationGroup="VGI" runat="server" ControlToValidate="cmbIndivisualAttempts"
                            Text="" ErrorMessage="Please select attempt" ForeColor="red"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="red"
                            ErrorMessage="Please select person" ControlToValidate="cmbPersons" ValidationGroup="VGI"
                            InitialValue="-- Select person --"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="red"
                            ErrorMessage="Please select start date" ControlToValidate="txtIndiStartDate"
                            ValidationGroup="VGI"></asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="actions">
    <asp:Button ID="btnSubmit" runat="server" CssClass="submitBtn" Text="Submit" />
</div>


<cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
