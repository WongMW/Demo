<%--Aptify e-Business 5.5.1 SR1, June 2014--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/MasterSchedule__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.MasterSchedule" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content">
    
<div class="table-div">
    <div class="row-div">
        <div class="align-left">
            <asp:Button CssClass="submitBtn" runat="server" ID="btnAddNewMasterSchedule" Text=" Create new schedule"
                CausesValidation="false" />
            <asp:Button CssClass="submitBtn" runat="server" ID="btnBack" Visible="false" Text="Back"
                CausesValidation="false" />
        </div>
    </div>
    &nbsp;
   <div class="cai-table">
    <div class="row-div">
        <rad:RadGrid ID="grdMasterSchedule" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
            PagerStyle-PageSizeLabelText="Records Per Page">
            <PagerStyle CssClass="sd-pager" />
            <GroupingSettings CaseSensitive="false" />
            
            <MasterTableView AllowSorting="True" NoMasterRecordsText="No firm contract approvals available."
                AllowNaturalSort="false" Width="100%">
                <Columns>
                    <rad:GridTemplateColumn HeaderText="Schedule number" DataField="ID" SortExpression="ID"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkID" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'
                                Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>' CssClass="cai-btn cai-btn-red-inverse">
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle />
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Status" DataField="Status" SortExpression="Status"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Training manager" DataField="FirstLast" SortExpression="FirstLast"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:DropDownList ID="cmbTrainingManager" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbTrainingManager_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle />
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Signed date" DataField="SignedDate" SortExpression="SignedDate"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSignedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SignedDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </rad:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </rad:RadGrid>
    </div>
</div>
    <telerik:RadWindow ID="radWindowConfirmation" runat="server" Width="400px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Firm contract approval" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblConfirmationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div>
                                    <br />
                                </div>
                                <div>
                                    <asp:Button ID="btnYes" runat="server" Text="Yes" Width="90px" class="submitBtn" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="90px" class="submitBtn" />
                                    <asp:Button ID="btnOK" runat="server" Visible = "false" Text="Ok" Width="70px" class="submitBtn" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>


</div>
<cc3:User ID="User1" runat="server" />
