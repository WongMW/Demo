<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/ClassRegistration.ascx.vb"
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
<div class="content-container clearfix cai-form">
    <div id="tblMain" runat="server">
        <span class="form-title">Class Registration</span>

        <div class="field-group">
            <asp:Label ID="lblInstructions" runat="server" CssClass="message"></asp:Label>
        </div>

        <div>
            <div class="form-section-half-border">
                <div class="field-group">
                    <span class="label-title">Class</span>
                    <asp:HyperLink Style="text-decoration: underline" runat="server" ID="lnkClassNum">
                        <asp:Label ID="lblClassNum" runat="server" />
                    </asp:HyperLink><asp:HiddenField ID="lblProductID" runat="Server" />
                </div>

                <div class="field-group">
                    <span class="label-title">Course</span>
                    <asp:Label ID="lblCourse" runat="server" />
                </div>

                <div class="field-group">
                    <span class="label-title">Type</span>
                    <asp:Label ID="lblType" runat="server" />
                </div>
                <div runat="server" id="trInstructor" class="field-group">
                    <span class="label-title">Instructor</span>
                    <asp:Label ID="lblInstructor" runat="server" />
                </div>
            </div>

            <div class="form-section-half-border">
                <div class="field-group">
                    <span class="label-title">Start Date</span>
                    <asp:Label ID="lblStartDate" runat="server" />
                </div>

                <div class="field-group">
                    <span class="label-title">End Date</span>
                    <asp:Label ID="lblEndDate" runat="server" />
                </div>


                <div class="field-group">
                    <span class="label-title">Location</span>
                    <asp:Label ID="lblLocation" runat="server" />
                </div>

                <div class="field-group">
                    <span class="label-title">Price</span>
                    <asp:Label ID="lblPrice" runat="server" />
                </div>

            </div>

        </div>

        <div class="field-group">
            <asp:Panel ID="CreaditCardInfo" runat="server">
                Enter class registation payment information below
                <br />
                <uc1:CreditCard ID="CreditCard" runat="server" ShowPaymentTypeSelection="true" />
                <br />
            </asp:Panel>
        </div>
    </div>
    <div class="cai-table cai-form-content">
        <asp:Panel ID="RegistrationGrid" runat="server">
            <span class="label-title">Registrants</span> <%-- Navin Prasad Issue 11032--%><%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%><%--CSS apply by Amruta IssueID: 13883 --%><asp:UpdatePanel
                ID="updPanelGrid" runat="server">
                <ContentTemplate>
                    <rad:RadGrid ID="grdStudents" runat="server" AutoGenerateColumns="false" CssClass="mobile-table">
                        <MasterTableView>
                            <Columns>
                                <rad:GridTemplateColumn HeaderText="First Name" DataField="FirstName" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <span class="mobile-label">First Name:</span>
                                        <asp:TextBox ID="txtFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Last Name" DataField="LastName" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <span class="mobile-label">Last Name:</span>
                                        <asp:TextBox ID="txtLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Title" DataField="Title" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <span class="mobile-label">Title:</span>
                                        <asp:TextBox ID="txtTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Email" DataField="Email" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <span class="mobile-label">Email:</span>
                                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Delete.png" CommandName="Delete" OnClientClick="HideLabel()"
                                            CommandArgument="<%# CType(Container, GridDataItem ).RowIndex %>" CssClass="delete-btn"/>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="actions">
                <asp:Button ID="btnMoreRows" runat="server" CssClass="submitBtn" Text="Add New Row" />
                <asp:Button runat="server" ID="btnSaveRegistration" Text="Submit Registration" CssClass="submitBtn" />
            </div>
        </asp:Panel>
        <asp:Button runat="server" ID="btnSaveRegistrationPaid" Text="Submit Registration"
            CssClass="submitBtn" /></center>
                           <%-- Amruta, IssueID 15457,4/24/2013, Label to display message if EmailID is invaild or user not entered data in registration grid --%>
    </div>
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
</div>
<!--<asp:Label ID="lblError" runat="server" ForeColor="Maroon" Visible="False" />-->
<cc3:User runat="server" ID="User1" />
<cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
</div>
