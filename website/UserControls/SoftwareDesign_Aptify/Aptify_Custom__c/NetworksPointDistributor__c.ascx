<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/NetworksPointDistributor__c.ascx.vb"
    Inherits="NetworksPointDistributor__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="javascript" type="text/javascript">

    function AllowNumericOnly(evt)//[0..9]
    {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode >= 48 && charCode <= 57)
            return true;
        else
            return false;
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="content-container clearfix">

            <asp:Panel runat="Server" ID="idPnl">

                <table>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rdoMyCompany" runat="server" GroupName="grpManage" Text="My Company"
                                AutoPostBack="True" Checked="true" />
                            <asp:RadioButton ID="rdoMyNetwork" runat="server" GroupName="grpManage" Text="My Networks"
                                AutoPostBack="True" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <table runat="server" id="tblMain" class="data-form">


                    <tr>
                        <td align="justify">Training Ticket Coupon’s :
                    <asp:DropDownList ID="cmbCampaign" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table align="justify" border="1">
                                <tr>
                                    <td colspan="2">Training Ticket Summary
                                    </td>
                                </tr>
                                <tr>
                                    <td>Purchased Points :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotalPoints" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Distributed Points To Company Members :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDistributedPoints" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Distributed Points To Network Members :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDistributedNW" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Quantity Used by Owner :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOwnerUsedQty" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Remaining Points :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRemainingPoints" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <%--  <tr>
                <td>
                    <asp:RadioButton ID="rdoMyCompany" runat="server" GroupName="grpManage" Text="My Company"
                        AutoPostBack="True" Checked="true" />
                    <asp:RadioButton ID="rdoMyNetwork" runat="server" GroupName="grpManage" Text="My Networks"
                        AutoPostBack="True" />
                </td>
            </tr>--%>
                    <tr id="trNW" runat="server" visible="false">
                        <td>Select Network:
                    <asp:DropDownList ID="cmbCommittees" runat="server" Visible="false" AutoPostBack="True">
                    </asp:DropDownList>
                            <br />
                            <b>
                                <asp:Label ID="lblNetworkPoint" runat="server" Text=""></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblErrorNotFound" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <telerik:RadGrid ID="grdCompanyPersonList" runat="server" AutoGenerateColumns="False"
                                AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                AllowFilteringByColumn="true" Visible="false">
                                <PagerStyle CssClass="sd-pager" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                            <ItemTemplate>
                                                <%--  <asp:RadioButton runat="server" ID="rdoSelect" AutoPostBack="true" OnCheckedChanged="rdSelect_CheckedChanged" />--%>
                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                <asp:Label ID="lblPersonID" runat="server" Text='<%# Eval("PersonID") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <%-- <Telerik:GridHyperLinkColumn Text="MeetingID" DataTextField="MeetingID" HeaderText="Meeting ID" SortExpression= "MeetingID" DataNavigateUrlFields="MeetingID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />--%>
                                        <telerik:GridBoundColumn DataField="FirstLast" HeaderText="Name" SortExpression="FirstLast"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <telerik:GridBoundColumn DataField="Department" HeaderText="Department" SortExpression="Department"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <telerik:GridTemplateColumn HeaderText="Quantity Assigned" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAssignedQty" runat="server" Text='<%# Eval("MaxQty") %>'></asp:Label>
                                                <%-- <asp:TextBox ID="txtQuantityAssigned" runat="server" Text='<%# Eval("MaxQty") %>'
                                            OnTextChanged="txtQuantityAssigned_Change" AutoPostBack="true" onkeypress="return AllowNumericOnly(event)"></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Quantity Used" AllowFiltering="false">
                                            <ItemTemplate>
                                                <%--  <asp:RadioButton runat="server" ID="rdoSelect" AutoPostBack="true" OnCheckedChanged="rdSelect_CheckedChanged" />--%>
                                                <asp:Label ID="lblQtyUsed" runat="server" Text='<%# Eval("QtyUsed") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Quantity Assign" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQuantityAssigned" runat="server" Text="0" Enabled="false"
                                                    onkeypress="return AllowNumericOnly(event)"></asp:TextBox>
                                                <%--    <asp:TextBox ID="txtQuantityAssigned" runat="server" text="0" Enabled="false"
                                            OnTextChanged="txtQuantityAssigned_Change" AutoPostBack="true" onkeypress="return AllowNumericOnly(event)" ></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                        </td>
                    </tr>
                    <tr id="idNote" runat="server">
                        <td>
                            <b>Note:
                        <asp:Label ID="lblNote" runat="server" Text=""></asp:Label></b><br />
                            <asp:Button ID="btnSubmit" runat="server" Text="Allocate" />
                            &nbsp;
                            <asp:Button ID="btnDeallocate" runat="server" Text="De-Allocate" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
            </asp:Panel>
        </div>
        <cc1:User ID="User1" runat="server" />
        <telerik:RadWindow ID="radMockTrial" runat="server" Width="350px" Height="120px"
            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
            Title="Point Distribution" Behavior="None">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblWarning" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </telerik:RadWindow>
    </ContentTemplate>

</asp:UpdatePanel>
