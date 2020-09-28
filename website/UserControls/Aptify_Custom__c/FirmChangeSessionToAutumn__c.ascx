<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FirmChangeSessionToAutumn__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_FirmChangeSessionToAutumn__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
<style type="text/css">
    .RadPivotGrid td
    {
        padding: 0;
    }
    .gray
    {
        width: 25px;
        height: 20px;
        background-color: LightGray;
    }
    .blue
    {
        width: 25px;
        height: 20px;
        background-color: LightBlue;
    }
    .RadPivotGrid td
    {
        padding: 0;
        height: 22px;
    }
    .RadPivotGrid .rpgFieldItem
    {
        padding: 0;
        width: 100%;
    }
</style>
<script type="text/javascript">


    function OnCheckBoxClick(ev) {
        var hdn = $('input[abc="hdnChecked"]');
        var hString = hdn.val();
        var rString = "";
        if (ev.checked) {
            if (hString == '') {
                hString = ev.parentElement.attributes["title"].value + ","
            }
            else {
                hString = hString + ev.parentElement.attributes["title"].value + ","
            }
            hdn.val(hString);
        }
        else {
            rString = hString.replace(ev.parentElement.attributes["title"].value + ",", "")
            hdn.val(rString);
        }
    }
</script>
<input type="hidden" id="hdnCheckedBoxes" abc="hdnChecked" runat="server" />
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
    <div class="info-data">
        <div class="row-div clearfix">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="8"></asp:Label>
        </div>
        <div class="row-div clearfix">
            &nbsp;
        </div>
        <div class="row-div clearfix">
            <div class="label-div w45">
                <div class="info-data">
                    <div class="row-div clearfix">
                        <div class="label-div w20">
                            <asp:Label ID="lblAcademicYear" runat="server" Font-Bold="True" Font-Size="8" Text="Academic Year: "></asp:Label>
                        </div>
                        <div class="field-div1 w25">
                            <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" Width="70%">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="label-div w50">
                <div class="info-data">
                    <div class="row-div clearfix">
                        <div class="label-div w10">
                            <div class="gray">
                            </div>
                        </div>
                        <div class="field-div1 w80" align="left">
                            <asp:Label ID="lblGroupMsg" runat="server" Font-Bold="True" Font-Size="8" Text="Student Group Not Available"></asp:Label>
                        </div>
                    </div>
                    <div class="row-div clearfix">
                        <div class="label-div w10">
                            <div class="blue">
                            </div>
                        </div>
                        <div class="field-div1 w80" align="left">
                            <asp:Label ID="lblAutumnMsg" runat="server" Font-Bold="True" Font-Size="8" Text="Enrolled On Autumn"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="info-data">
    </div>
    <div class="maindiv">
        <asp:Label ID="lblMessage" runat="server" Visible="false" Text="No records found"></asp:Label>
        <div style="overflow-x: auto; width: 100%">
            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div id="mygrid">
                <telerik:RadPivotGrid runat="server" ID="gvStudentResult" AllowPaging="True" AccessibilitySettings-OuterTableCaption=""
                    Width="120%" ColumnHeaderZoneText="ColumnHeaderZone" TotalsSettings-RowsSubTotalsPosition="None"
                    TotalsSettings-ColumnsSubTotalsPosition="None" TotalsSettings-ColumnGrandTotalsPosition="Last"
                    TotalsSettings-GrandTotalsVisibility="None" EnableToolTips="false" ShowColumnHeaderZone="false"
                    ShowFilterHeaderZone="false" ShowDataHeaderZone="false" ViewStateMode="Inherit"
                    Skin="Default" AllowSorting="true" AllowFiltering="true" Culture="en-US" LocalizationPath="~/App_GlobalResources/">
                    <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" ChangePageSizeLabelText="Page Size:">
                    </PagerStyle>
                    <ClientSettings EnableFieldsDragDrop="true">
                        <Scrolling AllowVerticalScroll="false"></Scrolling>
                        <%--Abhishek : Set AllowVerticalScroll="false" required for alignment of grid--%>
                    </ClientSettings>
                    <TotalsSettings GrandTotalsVisibility="None" />
                    <OlapSettings>
                        <%--<XmlaConnectionSettings Encoding="utf-8">
                </XmlaConnectionSettings>--%>
                    </OlapSettings>
                    <SortExpressions>
                        <telerik:PivotGridSortExpression FieldName="LastName" />
                    </SortExpressions>
                    <%-- <RowHeaderCellStyle Width="100px" /> --%>
                    <Fields>
                        <telerik:PivotGridRowField Caption="#" DataField="StudentID" SortOrder="Descending"
                            CellStyle-Width="7%">
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridRowField Caption="Last Name" DataField="LastName" CellStyle-Width="12%">
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridRowField Caption="First Name" DataField="FirstName" CellStyle-Width="12%">
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridRowField Caption="Route" DataField="Route" CellStyle-Width="9%">
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridRowField Caption="Current Stage" DataField="CurrentStage" CellStyle-Width="15%">
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridRowField Caption="Venue" DataField="Venue" UniqueName="Venue" CellStyle-Width="9%">
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridColumnField DataField="Curriculum" Caption="Curriculum" UniqueName="Curriculum">
                        </telerik:PivotGridColumnField>
                        <telerik:PivotGridColumnField DataField="Course" Caption="Course" UniqueName="Course">
                        </telerik:PivotGridColumnField>
                        <telerik:PivotGridAggregateField DataField="RegID">
                            <CellTemplate>
                                <div align="center" style="vertical-align:middle;">
                                    <asp:CheckBox ID="chkToAutumn" runat="server" Enabled='<%#IIf((Container.DataItem)<=0,false,true)%>'
                                        Visible='<%#IIf((Container.DataItem)=0,false,true)%>' Checked='<%#IIf((Container.DataItem)=-2,true,false)%>'
                                        OnClick="OnCheckBoxClick(this);" />
                                    <asp:Label ID="lblClassRegID" runat="server" Visible="false" Text='<%#(Container.DataItem)%>'></asp:Label>
                                </div>
                            </CellTemplate>
                        </telerik:PivotGridAggregateField>
                    </Fields>
                    <ConfigurationPanelSettings EnableOlapTreeViewLoadOnDemand="True" />
                </telerik:RadPivotGrid>
            </div>
        </telerik:RadAjaxPanel>
        </div>
    </div>
    <div>
        <table width="100%">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblStudCount" runat="server" Font-Bold="True" Font-Size="8"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="25%">
                </td>
                <td width="30%">
                    <asp:Button ID="btnBack" runat="server" Text="Back" class="submitBtn" />
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="submitBtn" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <telerik:RadWindow ID="radWindowSubmit" runat="server" Width="350px" Height="150px"
            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
            Title="Change Exam Session" Behavior="None">
            <ContentTemplate>
                <div class="info-data">
                    <div class="row-div clearfix">
                        <div class=".label-div-left-align w80">
                            <asp:Label ID="lblSubmitMsg" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row-div clearfix">
                        <div class="label-div1 w80" style="text-align: center">
                            <asp:Button ID="btnSubmitOk" runat="server" Text="Ok" class="submitBtn" />
                        </div>
                    </div>
                </div>
                <div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </div>
</telerik:RadAjaxPanel>
<cc1:User ID="loggedInUser" runat="server" />
