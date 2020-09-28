<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AdditionalOrganizations__c.ascx.vb"
    Inherits="AdditionalOrganizations__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="clearfix">
    <div class="field-group">
        <asp:Label ID="Label2" class="label-title" runat="server" Text="Primary District Society : "></asp:Label>
        <asp:Label ID="lblPrimaryDistrictSociety" runat="server" Text=""></asp:Label>
    </div>

    <div class="field-group">
        <asp:Label ID="Label1" class="label-title" runat="server" Text="Select Organization: "></asp:Label>
        <asp:DropDownList ID="cmbOrganization" runat="server" Width="90%" />
        <asp:Button ID="btnAdd" CausesValidation="false" Text="Add" runat="server" CssClass="submitBtn"/>
    </div>

    <div class="field-group orgnization-table">
        <rad:RadGrid ID="grdOrganization" AutoGenerateColumns="False" runat="server" SortingSettings-SortedDescToolTip="Sorted Descending"
            SortingSettings-SortedAscToolTip="Sorted Ascending" PageSize="5" PagerStyle-PageSizeLabelText="Records Per Page">
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
                        <HeaderStyle HorizontalAlign="Center" />
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
    </div>

    <div class="field-group">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="submitBtn" />
        <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    </div>
</div>
<cc1:User ID="User1" runat="server" />
