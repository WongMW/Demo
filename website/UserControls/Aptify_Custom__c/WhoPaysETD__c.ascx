<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WhoPaysETD__c.ascx.vb"
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
<style type="text/css">
    .GridH
    {
        text-align: left;
        font-weight: bold;
        padding-left: 0px;
        
    }
    .h1
    {
        color:Black;
    }
</style>
<div class="dvUpdateProgress"  style="overflow:visible;"> 
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server"
        DisplayAfter="0"> 
        <ProgressTemplate> 
            <div class="dvProcessing" style="height:1000px;"> 
                <table class="tblFullHeightWidth"> 
                    <tr> 
                        <td class="tdProcessing" style="vertical-align:middle" > 
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
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblSucMsg" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <h1>Company Pay:</h1>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlCompanyPay" runat="Server" Style="border: 1px Solid #000000;" Width="100%">
                                    <table>
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
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" align="right" style="padding-left: 5px;">
                                                Product Category:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbProductCategory" runat="server" AutoPostBack="True" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="padding-left: 5px;">
                                                Product:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbProduct" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" nowrap="nowrap" style="padding-left: 5px;" >
                                                Route Of Entry:
                                            </td>
                                            <td align="left" style="width: 340px; height: 0px;">
                                                <asp:DropDownList ID="cmbRouteOfEntry" runat="server" Width="200px">
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
                                            <td nowrap="nowrap" align="right" style="padding-left: 5px;">
                                                Member Type:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbMemberType" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="padding-left: 5px;">
                                                Role:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbRole" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="padding-left: 5px;">
                                                &nbsp;&nbsp;&nbsp;Attempts:
                                            </td>
                                            <td align="left" style="width: 340px; height: 0px;">
                                                <asp:DropDownList ID="cmbCompanyAttempts" runat="server" Width="200px">
                                                    <asp:ListItem>-- Select Attempt --</asp:ListItem>
                                                    <asp:ListItem>First Attempt</asp:ListItem>
                                                    <asp:ListItem>Re-Sit Attempt</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" align="right" valign="top" style="padding-left: 5px;">
                                                Company:
                                            </td>
                                            <td valign="top">
                                                <%-- <uc1:DatePicker ID="StartDate" runat="server" />
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBoxEditProfile" Width="150px"></asp:TextBox>--%>
                                                <asp:DropDownList ID="cmbSubsidarisCompney" runat="server" Width="200px">
                                                </asp:DropDownList>
                                                <%--  <Ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate">
                                        </Ajax:CalendarExtender>--%>
                                            </td>
                                            <td nowrap="nowrap" align="right" valign="top" style="padding-left: 5px;">
                                                &nbsp; Start Date:
                                            </td>
                                            <td valign="top">
                                                <telerik:RadDatePicker ID="txtStartDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td nowrap="nowrap" align="right" valign="top" style="padding-left: 5px;">
                                                End Date:
                                            </td>
                                            <td valign="top">
                                                <telerik:RadDatePicker ID="txtEndDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" align="right" valign="top">
                                            </td>
                                            <td valign="top">
                                                <%-- <uc1:DatePicker ID="StartDate" runat="server" />
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBoxEditProfile" Width="150px"></asp:TextBox>--%>
                                                <%--  <Ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate">
                                        </Ajax:CalendarExtender>--%>
                                            </td>
                                            <td nowrap="nowrap" align="right" valign="top">
                                            </td>
                                            <td valign="bottom">
                                                <asp:Button ID="btnADD" runat="server" Text="ADD" ValidationGroup="VGG" OnClientClick="ClearMsg()" />
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:GridView ID="grdCompanyPay" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    DataKeyNames="ProductID" AllowPaging="true" PageSize="10" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Product Category"  HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                             <asp:Label ID="lblRecordID" runat="server" Text='<%# Eval("ID")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblProductCategoryID" runat="server" Text='<%# Eval("ProductCategoryID")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblProductCategory" runat="server" Text='<%# Eval("ProductCategory")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product" ItemStyle-Width="100px" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("Product")%>'></asp:Label>
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
                                                        <asp:TemplateField HeaderText="Company" ItemStyle-Width="100px" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompanyID" runat="server" Text='<%# Eval("CompanyID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
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
                                                        <asp:TemplateField HeaderText="Route Of Entry" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRouteOfEntryID" runat="server" Text='<%# Eval("RouteOfEntryID")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblRouteOfEntry" runat="server" Text='<%# Eval("RouteOfEntry")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attempt" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAttempts" runat="server" Text='<%# Eval("Attempts")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                &nbsp;
                                                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                                    Visible='<%# IIf(Eval("NonEditableForFirm") = "False", true, false)%>' Text="Delete"
                                                                    CommandArgument='<%# CType(Container,GridViewRow).RowIndex & ","  & Eval("ProductCategoryID") & "," & Eval("ProductID") & "," & Eval("StartDate") & "," & Eval("EndDate")& "," & Eval("MemberTypeID") & "," & Eval("RoleID")  & "," & Eval("RouteOfEntryID") & "," & Eval("Attempts") & "," & Eval("CompanyID") & "," & Eval("ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <%-- <HeaderStyle co />--%>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:RequiredFieldValidator InitialValue="-- Select Product Category--" ID="Req_ID"
                                                    Display="Dynamic" ValidationGroup="VGG" runat="server" ControlToValidate="cmbProductCategory"
                                                    Text="" ErrorMessage="Please Select Product Category" ForeColor="White"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator InitialValue="-- Select Route Of Entry --" ID="RequiredFieldValidator4"
                                                    Display="Dynamic" ValidationGroup="VGG" runat="server" ControlToValidate="cmbRouteOfEntry"
                                                    Text="" ErrorMessage="Please Select Route Of Entry" ForeColor="White"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator InitialValue="-- Select Attempt --" ID="RequiredFieldValidator5"
                                                    Display="Dynamic" ValidationGroup="VGG" runat="server" ControlToValidate="cmbCompanyAttempts"
                                                    Text="" ErrorMessage="Please Select Attempt" ForeColor="White"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator runat="server" ID="cmpCalenders" ControlToValidate="txtEndDate"
                                                    ControlToCompare="txtStartDate" Operator="GreaterThan" Type="Date" ErrorMessage=""
                                                    ValidationGroup="VGG" ForeColor="White" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Start Date"
                                                    ForeColor="White" ControlToValidate="txtStartDate" ValidationGroup="VGG"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>       
                        </tr>
                        <tr>
                            <td colspan="2">
                                <h1>Individual Pay:</h1>
                                    <br />
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlIndividual" runat="Server" Style="border: 1px Solid #000000;" Width="100%">
                                    <table>
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
                                            <td nowrap="nowrap" align="right" style="padding-left: 5px;">
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
                                            <td align="right" nowrap="nowrap" style="padding-left: 5px;">
                                                Product Category:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbProductCat" runat="server" AutoPostBack="True" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="padding-left: 5px;" nowrap="nowrap">
                                                Route Of Entry:
                                            </td>
                                            <td align="left" style="width: 340px; height: 0px;">
                                                <asp:DropDownList ID="cmbIndividualRouteOfEntry" runat="server" Width="200px" AutoPostBack="True">
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
                                            <td nowrap="nowrap" align="right" style="padding-left: 5px;">
                                                Product:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbProductList" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="padding-left: 5px;">
                                                Member Type:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbMemberTypeIndi" runat="server" Width="200px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="padding-left: 5px;">
                                                &nbsp;&nbsp;&nbsp;Attempts:
                                            </td>
                                            <td align="left" style="width: 340px; height: 0px;">
                                                <asp:DropDownList ID="cmbIndivisualAttempts" runat="server" Width="200px">
                                                    <asp:ListItem>-- Select Attempt --</asp:ListItem>
                                                    <asp:ListItem>First Attempt</asp:ListItem>
                                                    <asp:ListItem>Re-Sit Attempt</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                    <td colspan="6">
                                    </td>
                                </tr>--%>
                                        <tr>
                                            <td align="right" style="padding-left: 5px;">
                                                Role:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbRoleIndi" runat="server" Width="200px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="padding-left: 5px;">
                                                Company:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbIndividualCompany" runat="server" Width="200px" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="padding-left: 5px;">
                                                Person:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbPersons" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                            </td>
                                        </tr>
                                        <%--  <tr>
                                    <td colspan="4">
                                        &nbsp;
                                    </td>
                                </tr>--%>
                                        <tr>
                                            <td nowrap="nowrap" align="right" valign="top" style="padding-left: 5px;">
                                                Start Date:
                                            </td>
                                            <td valign="top">
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
                                            <td nowrap="nowrap" align="right" valign="top" style="padding-left: 5px;">
                                                End Date:
                                            </td>
                                            <td valign="top">
                                                <telerik:RadDatePicker ID="txtIndiEndDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnIndiAdd" runat="server" Text="ADD" ValidationGroup="VGI" OnClientClick="ClearFields()" />
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:GridView ID="grdIndividualPay" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    AllowPaging="true" PageSize="10">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Pay Type" ItemStyle-Width="100px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridH" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayType" runat="server" Text='<%# Eval("PayType")%>'></asp:Label>
                                                                <asp:Label ID="lblRecordID" runat="server" Text='<%# Eval("ID")%>'
                                                                    Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Person" ItemStyle-Width="100px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPersonID" runat="server" Text='<%# Eval("PersonID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblPerson" runat="server" Text='<%# Eval("Person")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Category" ItemStyle-Width="70px"  HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductCategoryID" runat="server" Text='<%# Eval("ProductCategoryID")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblProductCategory" runat="server" Text='<%# Eval("ProductCategory")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      

                                                        <asp:TemplateField HeaderText="Product" ItemStyle-Width="100px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("Product")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Member Type" ItemStyle-Width="100px" HeaderStyle-Wrap="true" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMemberTypeID" runat="server" Text='<%# Eval("MemberTypeID")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblMemberType" runat="server" Text='<%# Eval("MemberType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Role" HeaderStyle-Wrap="false" ItemStyle-Width="100px" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRoleID" runat="server" Text='<%# Eval("RoleID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Company" ItemStyle-Width="100px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompanyID" runat="server" Text='<%# Eval("CompanyID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="100px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate", "{0:d}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="End Date"  ItemStyle-Width="100px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate", "{0:d}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Route Of Entry" ItemStyle-Width="100px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRouteOfEntryID" runat="server" Text='<%# Eval("RouteOfEntryID")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblRouteOfEntry" runat="server" Text='<%# Eval("RouteOfEntry")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attempt" ItemStyle-Width="100px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridH">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAttempts" runat="server" Text='<%# Eval("Attempts")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                                    Visible='<%# IIf(Eval("NonEditableForFirm") = "False", true, false)%>' Text="Delete"
                                                                    CommandArgument='<%# CType(Container,GridViewRow).RowIndex & "," & Eval("PersonID") & "," & Eval("ProductCategoryID") & "," & Eval("ProductID") & "," & Eval("StartDate") & "," & Eval("EndDate") &"," & Eval("PayType") &"," & Eval("MemberTypeID") &"," & Eval("RoleID")& "," & Eval("RouteOfEntryID") & "," & Eval("Attempts") & "," & Eval("CompanyID") &"," & Eval("ID")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtIndiEndDate"
                                                    ControlToCompare="txtIndiStartDate" Operator="GreaterThan" Type="Date" ErrorMessage=""
                                                    ValidationGroup="VGI" ForeColor="White" />
                                                <asp:RequiredFieldValidator InitialValue="-- Select Product Category--" ID="RequiredFieldValidator6"
                                                    Display="Dynamic" ValidationGroup="VGI" runat="server" ControlToValidate="cmbProductCat"
                                                    Text="" ErrorMessage="Please Select Product Category" ForeColor="White"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator InitialValue="-- Select Route Of Entry --" ID="RequiredFieldValidator7"
                                                    Display="Dynamic" ValidationGroup="VGI" runat="server" ControlToValidate="cmbIndividualRouteOfEntry"
                                                    Text="" ErrorMessage="Please Select Route Of Entry" ForeColor="White"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator InitialValue="-- Select Attempt --" ID="RequiredFieldValidator8"
                                                    Display="Dynamic" ValidationGroup="VGI" runat="server" ControlToValidate="cmbIndivisualAttempts"
                                                    Text="" ErrorMessage="Please Select Attempt" ForeColor="White"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="White"
                                                    ErrorMessage="Please Select Person" ControlToValidate="cmbPersons" ValidationGroup="VGI"
                                                    InitialValue="-- Select Person --"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="White"
                                                    ErrorMessage="Please Select Start Date" ControlToValidate="txtIndiStartDate"
                                                    ValidationGroup="VGI"></asp:RequiredFieldValidator>
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
                        <%-- <tr>
                    <td align="right">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                    </td>
                </tr>--%>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <%--<tr>
        <td align="right">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
        </td>
    </tr>--%>
</table>
<cc1:User ID="User1" runat="server"></cc1:User>
<cc3:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc3:AptifyWebUserLogin>
