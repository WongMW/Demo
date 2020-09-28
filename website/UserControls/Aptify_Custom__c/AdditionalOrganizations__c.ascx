<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdditionalOrganizations__c.ascx.vb"
    Inherits="AdditionalOrganizations__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .auto-style1
    {
        width: 100%;
    }
</style>
<div class="content-container clearfix">
    <table runat="server" id="tblMain" class="data-form" width="100%">
        <tr>
            <td>
                <table  class="auto-style1">
                    <tr>
                        <td align="right" style="text-align: right;">
                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Primary District Society : "></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblPrimaryDistrictSociety" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="text-align: right;">
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Select Organization: " Width="120px"></asp:Label>
                        </td>
                        <td align="left" style="text-align: left;">
                            <asp:DropDownList ID="cmbOrganization" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            <asp:Button ID="btnAdd" CausesValidation="false" Text="Add" runat="server" Width="70px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <rad:RadGrid ID="grdOrganization" AutoGenerateColumns="False" runat="server" SortingSettings-SortedDescToolTip="Sorted Descending"
                    SortingSettings-SortedAscToolTip="Sorted Ascending" PageSize="5" Skin="Sunset"
                    Width="400px" PagerStyle-PageSizeLabelText="Records Per Page">
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowSorting="true" AllowNaturalSort="false" DataKeyNames="ID" EnableNoRecordsTemplate="true"
                        ShowHeadersWhenNoRecords="false">
                        <NoRecordsTemplate>
                            <div>
                                No Data to Display
                            </div>
                        </NoRecordsTemplate>
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Organization" DataField="Name" SortExpression="Name">
                                <HeaderStyle HorizontalAlign="Center" Width="80%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOrganization" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Remove">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnRemove" CausesValidation="false" runat="server" CommandName="Delete"
                                        Text="Remove" ImageUrl="~/Images/crossdelete.png" CommandArgument='<%# Eval("ID") %>' />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 5%; text-align: right;">
                <asp:Button ID="btnSave" runat="server" Text="Save" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
</div>
<cc1:User ID="User1" runat="server" />
