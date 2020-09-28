<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/NetworkManagment__c.ascx.vb"
    Inherits="NetworkManagment__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div class="content-container clearfix">
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td>
                <telerik:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                    AllowFilteringByColumn="true">
                    <PagerStyle CssClass="sd-pager" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>

                            <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="25%" ItemStyle-Width="25%" />
                            <%-- <Telerik:GridBoundColumn DataField="TermName" HeaderText="Tearm Name" SortExpression="TermName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="25%" ItemStyle-Width="25%"  />--%>
                            <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="25%" ItemStyle-Width="25%" />
                            <telerik:GridTemplateColumn HeaderText="Manage" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Button ID="btnManage" class="submitBtn" runat="server" Text="Manage" AutoPostBack="true" OnClick="GotoManageNetwork"></asp:Button>
                                    <asp:Button ID="btnRemove" class="submitBtn" runat="server" Text="Remove" AutoPostBack="true" OnClick="RemoveMeFromNetwork"></asp:Button>
                                    <asp:Label ID="lblCommitteeID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblCTID" runat="server" Text='<%# Eval("CTID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblctmmID" runat="server" Text='<%# Eval("CTMID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />

</div>

<table id="Table7" runat="server" class="mytable">

    <tr>
        <td colspan="2" align="left">
            <asp:Button ID="btnAddNetwork" runat="server" Text="Add Network" />
        </td>
    </tr>




    <tr>
        <td>&nbsp;</td>
    </tr>
</table>

<telerik:RadWindow ID="radAlert" runat="server" Width="350px" Height="100px" Modal="True"
    Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Remove from Network" Behavior="None">
    <ContentTemplate>
        <table class="tblEditAtendee" width="100%" cellpadding="0" cellspacing="10">
            <tr>
                <td align="left">
                    <asp:Label ID="lblWarning" runat="server" Font-Bold="true" Text="Are you sure, you want to left this network?"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnOk" runat="server" Text="Yes" class="submitBtn" ValidationGroup="ok" Width="60px" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="No" class="submitBtn" ValidationGroup="ok" Width="60px" />
                    <asp:Label ID="lblComTID" runat="server" Text="-1" Visible="false"></asp:Label>
                    <asp:Label ID="lblCommID" runat="server" Text="-1" Visible="false"></asp:Label>
                    <asp:Label ID="lblCTMID" runat="server" Text="-1" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</telerik:RadWindow>



<cc1:User ID="User1" runat="server" />
