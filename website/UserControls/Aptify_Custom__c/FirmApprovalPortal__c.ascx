<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FirmApprovalPortal__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.FirmApprovalPortal__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<style type="text/css">
    .HeaderStyle th
    {
        text-align: left;
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
<div class="content-container clearfix" id="divTop" runat="server">
    <%--<asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server">
        <ContentTemplate>--%>
    <p>
        &nbsp;<rad:RadGrid ID="radGrdFirmApproval" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
            AllowFilteringByColumn="true" HeaderStyle-CssClass="HeaderStyle">
            <HeaderStyle HorizontalAlign="Left" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                <%-- 'Suraj Issue 15287 4/9/13, if the grid dont have any record then grid should visible and it should show "No recors " msg--%>
                <Columns>
                    <rad:GridDateTimeColumn DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                        AutoPostBackOnFilter="false" />
                    <rad:GridBoundColumn DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" />
                    <rad:GridBoundColumn DataField="StudentNumber" HeaderText="Student Number" SortExpression="StudentNumber"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" />
                    <rad:GridBoundColumn DataField="DateOfBirth" HeaderText="Date Of Birth" SortExpression="DateOfBirth"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" />
                    <rad:GridBoundColumn DataField="RouteOfEntry" HeaderText="Route Of Entry" SortExpression="RouteOfEntry"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" />
                    <rad:GridBoundColumn DataField="EmailAddress" HeaderText="Email Address" SortExpression="EmailAddress"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" />
                        <%--Govind Mande 04May2016--%>
                          <rad:GridBoundColumn DataField="Office" HeaderText="Office" SortExpression="Office"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" />
                    <rad:GridBoundColumn DataField="Status" HeaderText="Application Status" SortExpression="Status"
                        AutoPostBackOnFilter="false" ShowFilterIcon="false" />
                    <rad:GridTemplateColumn HeaderText="Approval Status" AutoPostBackOnFilter="false"
                        ShowFilterIcon="false">
                        <ItemTemplate>
                            <%-- <rad:RadComboBox ID="radApproval" runat="server" OnSelectedIndexChanged="radApproval_SelectedIndexChanged" AutoPostBack="true"></rad:RadComboBox>--%>
                            <asp:DropDownList ID="ddlApproval" runat="server" OnSelectedIndexChanged="ddlApproval_SelectedIndexChanged"
                                AutoPostBack="true">
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
    </p>
    <%--  </ContentTemplate>
       
    </asp:UpdatePanel>--%>
    <div align="Right">
        <asp:Button ID="btnApprove" runat="server" Text="Approve All" />
    </div>
    <br />
    <div align="Right">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" />
    </div>
    <cc1:User runat="server" ID="User1" />
</div>
