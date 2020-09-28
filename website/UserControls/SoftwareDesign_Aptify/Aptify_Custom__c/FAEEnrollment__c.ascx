<%--Aptify e-Business 5.5.1 SR1, June 2014--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FAEEnrollment__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.FAEEnrollment" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%-- Adding progress bar for this Redmine #19432--%>
<div class="raDiv" style="overflow: visible;">
       <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>Loading</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="table-div">
    <div class="row-div">
        <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    </div>
    <div class="row-div">
       <telerik:RadGrid ID="grdFAEEnrollment" runat="server" AllowPaging="false" AllowSorting="false"
            PageSize="10" AllowFilteringByColumn="false" ClientSettings-Scrolling-UseStaticHeaders="true"  Style="overflow :auto;" Height="450px"   Width="100%" CellSpacing="0" GridLines="None"
            AutoGenerateColumns="false" Visible="true" CssClass="cai-table mobile-table" ClientSettings-Scrolling-AllowScroll="true">
            <ClientSettings Scrolling-AllowScroll="true" Scrolling-UseStaticHeaders="true" Scrolling-ScrollHeight="350px" Scrolling-EnableColumnClientFreeze ="true"   Scrolling-SaveScrollPosition="true"></ClientSettings>
            <MasterTableView ShowHeadersWhenNoRecords="true"  Height="350px" TableLayout="Auto">
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

                    <telerik:GridTemplateColumn HeaderText="Location" ShowFilterIcon="false" DataField="OfficeLocation"
                        SortExpression="OfficeLocation" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                        <ItemTemplate>
                            <span class="mobile-label">OfficeLocation:</span>
                            <asp:Label ID="lblOfficeLocation" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"OfficeLocation") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderText="FAE Elective" DataField=""
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
   <%-- Adding update panel foor this Redmine #19432--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="actions">
                <asp:Button runat="server" ID="btnBack" CssClass="submitBtn" Text="Back" />
                <asp:Button runat="server" ID="btnSave" CssClass="submitBtn" Text="Submit" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<cc3:User ID="User1" runat="server" />

