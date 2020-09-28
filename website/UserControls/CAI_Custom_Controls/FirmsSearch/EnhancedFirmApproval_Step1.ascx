<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnhancedFirmApproval_Step1.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedFirmApproval_Step1" %>
<%@ Register TagName="Two" TagPrefix="Step" Src="~/UserControls/CAI_Custom_Controls/FirmsSearch/EnhancedFirmApproval_Step2.ascx" %>

<asp:Panel ID="step1" runat="server" Visible="true">
    <asp:Repeater runat="server" ID="statusMessages" Visible="false">
        <ItemTemplate>
            <span class="approved-message" runat="server" visible='<%# ((Boolean)Eval("IsRejected")) ? false : true %>'>
                <%# Eval("FirmName") %> has been approved!
            </span>
            <span class="rejected-message" runat="server" visible='<%# !((Boolean)Eval("IsRejected")) ? false : true %>'>
                <%# Eval("FirmName") %> has been rejected!
            </span>
        </ItemTemplate>
    </asp:Repeater>
    <div id="noApprovalNeeded" runat="server" visible="false">
        <p>There are currently no approval required for enhanced firm's listing</p>
    </div>
    <asp:Repeater ID="approvalNeeded" runat="server">
        <HeaderTemplate>
            <h2>Staff Approval Needed</h2>
            <table>
                <thead>
                    <tr>
                        <td>Admin Name</td>
                        <td>Pending Approval</td>
                        <td>First Submitted On</td>
                        <td></td>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
                    <tr>
                        <td><%# Eval("AdminName").ToString() %></td>
                        <td><%# Eval("PendingCount").ToString() + " office" + (Eval("PendingCount").ToString().Equals("1") ? "" : "s") %></td>
                        <td><%# Eval("FirstSubmittedDate").ToString() %></td>
                        <td><asp:Button Text="View" runat="server" ID="btnView" CommandName="view" CommandArgument='<%# Eval("PersonId").ToString() %>' OnCommand="btnView_Command" /></td>
                    </tr>
        </ItemTemplate>
        <FooterTemplate>
                </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Panel>
<step:Two runat="server" ID="step2" Visible="false"></step:Two>
