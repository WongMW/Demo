<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ClassRegistration.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.ClassRegistrationControl" %>
<%@ Register Src="../Aptify_General/CreditCard.ascx" TagName="CreditCard" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
 <%--Amruta , Issue 15457, 4/24/2013, Function to hide label on click delete button of grid --%>
<script type="text/javascript" language="javascript">
    function HideLabel() {
        document.getElementById('<%=lblMsg.ClientID%>').style.display = 'none';
    }
</script>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td colspan="2" style="border-right: gray 1px solid; border-top: gray 1px solid;
                vertical-align: top; border-left: gray 1px solid; border-bottom: gray 1px solid">
                Class Registration
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border-right: gray 1px solid; border-top: gray 1px solid;
                vertical-align: top; border-left: gray 1px solid; border-bottom: gray 1px solid">
                <asp:Label ID="lblInstructions" runat="server">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            Class
                        </td>
                        <td>
                            <asp:HyperLink Style="text-decoration: underline" runat="server" ID="lnkClassNum">
                                <asp:Label ID="lblClassNum" runat="server" /></asp:HyperLink><asp:HiddenField ID="lblProductID"
                                    runat="Server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Course </td><td>
                            <asp:Label ID="lblCourse" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Type </td><td>
                            <asp:Label ID="lblType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Start Date </td><td>
                            <asp:Label ID="lblStartDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            End Date </td><td>
                            <asp:Label ID="lblEndDate" runat="server" />
                        </td>
                    </tr>
                    <tr runat="server" id="trInstructor">
                        <td>
                            Instructor </td><td>
                            <asp:Label ID="lblInstructor" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Location </td><td>
                            <asp:Label ID="lblLocation" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Price</b> </td><td style="border-bottom: solid 1px gray;">
                            <asp:Label ID="lblPrice" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>           
            <td style="vertical-align: top; border: solid 1px gray;">
                <asp:Panel ID="CreaditCardInfo" runat="server">
                    Enter class registation payment information below <br /><uc1:CreditCard ID="CreditCard" runat="server" ShowPaymentTypeSelection="true" />
                    <br />
                </asp:Panel>
                <asp:Panel ID="RegistrationGrid" runat="server">
                    <b>Registrants</b> <%-- Navin Prasad Issue 11032--%><%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%><%--CSS apply by Amruta IssueID: 13883 --%><asp:UpdatePanel
                        ID="updPanelGrid" runat="server">
                        <ContentTemplate>
                            <%-- <asp:GridView ID="grdStudents" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="GridViewHeaderStyle" >
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" CssClass="submitBtn" runat="server" Text="Delete" CommandName="Delete"  CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name" HeaderStyle-CssClass="GridViewHeaderStyle">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFirstName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Last Name" HeaderStyle-CssClass="GridViewHeaderStyle">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLastName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'>
                                    </asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Title" HeaderStyle-CssClass="GridViewHeaderStyle">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTitle" Width="120px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'>
                                    </asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="GridViewHeaderStyle">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEmail" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'>
                                    </asp:TextBox></ItemTemplate></asp:TemplateField></Columns><PagerSettings Mode="Numeric" />
                    </asp:GridView>--%>
                            <%-- RashmiP, Issue 14452, 1/10/13, Used Rad Grid instead of GridView--%>
                            <%-- Prasad D, Issue 15155, 01/02/2013, removed pagging of rad grid  --%>
                            <%--Neha Changes for Issue 14452--%>
                            <rad:RadGrid ID="grdStudents" runat="server" AutoGenerateColumns="false">
                                <MasterTableView>
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="First Name" DataField="FirstName" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFirstName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:TextBox></ItemTemplate></rad:GridTemplateColumn><rad:GridTemplateColumn HeaderText="Last Name" DataField="LastName" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLastName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:TextBox></ItemTemplate></rad:GridTemplateColumn><rad:GridTemplateColumn HeaderText="Title" DataField="Title" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTitle" Width="120px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:TextBox></ItemTemplate></rad:GridTemplateColumn><rad:GridTemplateColumn HeaderText="Email" DataField="Email" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEmail" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'></asp:TextBox></ItemTemplate></rad:GridTemplateColumn><rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                                            <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Delete.png" CommandName="Delete"  OnClientClick="HideLabel()"
                            CommandArgument="<%# CType(Container, GridDataItem ).RowIndex %>" />
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid></ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnMoreRows" runat="server" CssClass="submitBtn" Text="Add New Row" />
                    <asp:Button runat="server" ID="btnSaveRegistration" Text="Submit Registration" CssClass="submitBtn" />
                </asp:Panel>
                <center>
                    <asp:Button runat="server" ID="btnSaveRegistrationPaid" Text="Submit Registration"
                        CssClass="submitBtn" /></center>
                       <%-- Amruta, IssueID 15457,4/24/2013, Label to display message if EmailID is invaild or user not entered data in registration grid --%>
                        <br />                               
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
            </td>
        </tr>
    </table>
    <!--<asp:Label ID="lblError" runat="server" ForeColor="Maroon" Visible="False" />-->
    <cc3:User runat="server" ID="User1" />
    <cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
</div>
