<%--Aptify e-Business 5.5.1 SR1, June 2014--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FAEEnrollment__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.FAEEnrollment" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="table-div">
    <div class="row-div">
        <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    </div>
    <div class="row-div">
        <telerik:RadGrid ID="grdFAEEnrollment" runat="server" AllowPaging="false" AllowSorting="True"
            PageSize="10" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None"
            AutoGenerateColumns="false" Visible="true" CssClass="cai-table mobile-table">

            <MasterTableView ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="Student Number" DataField="StudentNumber" SortExpression=""
                        AutoPostBackOnFilter="true" FilterControlWidth="100%" DataType="System.String"
                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                        <ItemTemplate>
                            <span class="mobile-label">Student#:</span>
                            <asp:Label ID="lnkStudentNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StudentNumber") %>'>
                            </asp:Label>
                            <asp:HiddenField ID="hidStudentNo" Value='<%# DataBinder.Eval(Container.DataItem, "StudentID")%>'
                                runat="server" />
                        </ItemTemplate>
                        <ItemStyle />
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderText="Last Name" DataField="LastName" SortExpression="LastName"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <span class="mobile-label">Last Name:</span>
                            <asp:Label ID="lblLastName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderText="First Name" ShowFilterIcon="false" DataField="FirstName"
                        SortExpression="FirstName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                        <ItemTemplate>
                            <span class="mobile-label">First Name:</span>
                            <asp:Label ID="lblFirstName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Elective" DataField=""
                        SortExpression="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        ShowFilterIcon="false" AllowFiltering="false">
                        <ItemTemplate>
                            <span class="mobile-label">Elective:</span>
                            <asp:DropDownList ID="cmbElective" runat="server" CssClass="cai-table-data">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Time table" DataField=""
                        SortExpression="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        ShowFilterIcon="false" AllowFiltering="false">
                        <ItemTemplate>
                            <span class="mobile-label">Time table:</span>
                            <asp:DropDownList ID="cmbTimetable" runat="server" CssClass="cai-table-data">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle />
                    </telerik:GridTemplateColumn>

                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div class="actions">
        <asp:Button runat="server" ID="btnBack" CssClass="submitBtn" Text="Back" />
        <asp:Button runat="server" ID="btnSave" CssClass="submitBtn" Text="Submit" />
    </div>
</div>
<cc3:User ID="User1" runat="server" />
