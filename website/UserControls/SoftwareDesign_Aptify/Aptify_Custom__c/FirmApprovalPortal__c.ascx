<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FirmApprovalPortal__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.FirmApprovalPortal__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<style type="text/css">
    .HeaderStyle th {
        text-align: left;
    }
</style>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
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
<div class="content-container clearfix" id="divTop" runat="server">
    <div class="cai-table mobile-table">
        <rad:RadGrid ID="radGrdFirmApproval" runat="server" AutoGenerateColumns="False"  
            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
            AllowFilteringByColumn="false" HeaderStyle-CssClass="HeaderStyle">
 <ClientSettings Scrolling-AllowScroll="true"></ClientSettings>
            <PagerStyle CssClass="sd-pager" />
            <HeaderStyle HorizontalAlign="Left" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">

                <Columns>
                    <rad:GridTemplateColumn DataField="LastName" HeaderText="Last name" SortExpression="LastName"
                        AutoPostBackOnFilter="false" >
                        <ItemTemplate>
                            <span class="mobile-label">Last name:</span>
                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "LastName")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn DataField="FirstName" HeaderText="First name" SortExpression="FirstName"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" >
                         <ItemTemplate>
                            <span class="mobile-label">First name:</span>
                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "FirstName")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn DataField="StudentNumber" HeaderText="Student number" SortExpression="StudentNumber"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" >
                          <ItemTemplate>
                            <span class="mobile-label">Student number:</span>
                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "StudentNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn DataField="DateOfBirth" HeaderText="DOB" SortExpression="DateOfBirth"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" >
                          <ItemTemplate>
                            <span class="mobile-label">Date of birth:</span>
                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "DateOfBirth")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn DataField="RouteOfEntry" HeaderText="Entry route" SortExpression="RouteOfEntry"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" >
                        <ItemTemplate>
                            <span class="mobile-label">Entry route:</span>
                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "RouteOfEntry")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn DataField="EmailAddress" HeaderText="Email address" SortExpression="EmailAddress"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" >
                        <ItemTemplate>
                            <span class="mobile-label">Email address:</span>
                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "EmailAddress")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
 <%--Govind Mande 04May2016--%>
		    <rad:GridTemplateColumn DataField="Office" HeaderText="Office" SortExpression="Office"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" >
                        <ItemTemplate>
                            <span class="mobile-label">Office:</span>
                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Office")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>


                    <rad:GridTemplateColumn DataField="Status" HeaderText="Eligibility status" SortExpression="Status"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false">
                        <ItemTemplate>
                            <span class="mobile-label">Application status:</span>
                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Approval status" AutoPostBackOnFilter="false"
                        ShowFilterIcon="false">
                        <ItemTemplate>
                            <span class="mobile-label">Approval status:</span>
                            <asp:DropDownList ID="ddlApproval" runat="server"  
                                  CssClass="cai-table-data">
                            </asp:DropDownList>
                            <asp:Label ID="lblFirstLast" runat="server" Text='<%# Eval("FirstName") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblExamptionID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblPersonID" runat="server" Text='<%# Eval("PersonID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblApproveStatus" runat="server" Text='<%# Eval("FirmApprovalStatus") %>'
                                Visible="false"></asp:Label>
                            <asp:Label ID="lblRowIndex" runat="server" Text='<%#  CType(Container, Telerik.Web.UI.GridDataItem).ItemIndex +1   %>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="left" Width="100px" />
                    </rad:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </rad:RadGrid>
        <br />
        <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:Label ID="lblSuccessMsg" runat="server" Text="" ForeColor="green"></asp:Label>
    </div>

    <div class="actions">
        <asp:Button ID="btnApprove" runat="server" CssClass="submitBtn" Text="Approve All" />
    </div>
    <div class="actions">
        <asp:Button ID="btnSubmit" runat="server" CssClass="submitBtn" Text="Submit" Visible="true" />
    </div>
    <cc1:User runat="server" ID="User1" />
</div>
