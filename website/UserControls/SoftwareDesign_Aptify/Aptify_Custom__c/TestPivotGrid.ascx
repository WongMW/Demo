<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/TestPivotGrid.ascx.vb" Inherits="UserControls_Aptify_Custom__c_TestPivotGrid" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<style type="text/css">
    div.qsf-right-content .qsf-col-wrap {
        position: static;
    }

    .RadPivotGrid_Metro .rpgContentZoneDiv td {
        white-space: nowrap;
    }
</style>
<link href="../../Include/style.css" rel="stylesheet" type="text/css" />
<div>
    <telerik:RadSkinManager ID="QsfSkinManager" runat="server" ShowChooser="True"
        Skin="Default" />
    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="ajaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadPivotGrid AllowSorting="True" ID="gvFirmEnrollment"
            AllowPaging="True" runat="server" EnableZoneContextMenu="True"
            FilterHeaderZoneText="Filtering">
            <PagerStyle CssClass="sd-pager" />
            <ClientSettings EnableFieldsDragDrop="true">
                <Scrolling AllowVerticalScroll="true"></Scrolling>
            </ClientSettings>
            <FieldsPopupSettings ColumnFieldsMinCount="3" FilterFieldsMinCount="1"
                RowFieldsMinCount="3" />
            <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" ChangePageSizeLabelText="Page Size:"></PagerStyle>
            <OlapSettings>
                <XmlaConnectionSettings Encoding="utf-8">
                </XmlaConnectionSettings>
            </OlapSettings>
            <Fields>
                <telerik:PivotGridRowField DataField="StudentID" IsHidden="true">
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="LastName" Caption="Last Name">
                    <CellTemplate>
                        <asp:LinkButton ID="lnkLastName" runat="server" Text='<%#(Container.DataItem).Substring((Container.DataItem).IndexOf(";") + 1) %>'
                            ForeColor="White" CommandName="Edit" Font-Underline="true" CommandArgument='<%#(Container.DataItem )%>'></asp:LinkButton>
                    </CellTemplate>
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="FirstName" Caption="First Name">
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="Route" Caption="Route">
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="VenueName" Caption="Venue" UniqueName="Venue">
                </telerik:PivotGridRowField>
                <telerik:PivotGridColumnField DataField="Name" Caption="Cap" UniqueName="Cap">
                </telerik:PivotGridColumnField>
                <telerik:PivotGridColumnField DataField="CapParts" Caption="Cap Parts" UniqueName="CapParts">
                    <CellTemplate>
                        <asp:Label ID="lblCapPart" runat="server" Text='<%#(Container.DataItem).Substring((Container.DataItem).IndexOf(";") + 1) %>'></asp:Label>
                    </CellTemplate>
                </telerik:PivotGridColumnField>
                <telerik:PivotGridColumnField DataField="CapSubParts" Caption="Cap Sub Parts" UniqueName="CapSubParts">
                </telerik:PivotGridColumnField>
                <telerik:PivotGridAggregateField DataField="IsCompleted">
                    <CellTemplate>
                        <asp:Label ID="lblIsChecked" Visible='<%# IIf((Container.DataItem) = 2, True, IIf((Container.DataItem) = 3,true,false ))%>'
                            Enabled="false" Height="100%" Width="100%" runat="server"></asp:Label>
                        <asp:CheckBox ID="chkIsCompleted" runat="server" Checked='<%# IIf((Container.DataItem)=1,true,false) %>'
                            Enabled="false" Visible='<%# IIf((Container.DataItem)>1,false,true) %>' />
                        <asp:DropDownList ID="ddlExamsList" runat="server" Visible='<%# IIf((Container.DataItem)=4,true,false) %>'
                            Font-Size="Smaller">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlTimeTable" runat="server" Visible='<%# IIf((Container.DataItem)=5,true,false) %>'
                            Font-Size="Smaller">
                        </asp:DropDownList>
                    </CellTemplate>
                </telerik:PivotGridAggregateField>
            </Fields>
            <ConfigurationPanelSettings EnableOlapTreeViewLoadOnDemand="True" />
        </telerik:RadPivotGrid>
    </telerik:RadAjaxPanel>
</div>
<cc1:User ID="User1" runat="server" />
