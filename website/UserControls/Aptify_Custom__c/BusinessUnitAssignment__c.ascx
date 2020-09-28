<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BusinessUnitAssignment__c.ascx.vb"
    Inherits="BusinessUnitAssignment__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<cc1:User runat="server" ID="User1" />
<script src="../../Ebusiness/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<style type="text/css">
    .active
    {
        display: block;
    }
    .inactive
    {
        display: none;
    }
    .collapse
    {
        display: none;
    }
    .expand
    {
        cursor: pointer;
    }
    .main-container
    {
        margin: 0 1% 0 1%;
        width: 98%;
        word-wrap: break-word;
        overflow: hidden;
    }
    .container-left
    {
        float: left;
        overflow: hidden;
        width: 54%;
    }
    .container-right
    {
        float: right;
        overflow: hidden;
        width: 46%;
    }
    .lable-c
    {
        float: left;
        text-align: left;
        margin-right: 1%;
        font-weight: bold;
        font-size: 12px;
        overflow: hidden;
    }
    .w42
    {
        width: 42.5%;
    }
    
    .RadListBox .rlbItem
    {
        cursor: default;
        padding: 2px 5px;
        white-space: nowrap;
        border: 1px solid #555 !important;
    }
    
    RadListBox_Sunset .rlbGroup .rlbSelected
    {
        background: Red !important;
    }
</style>
<script type="text/javascript">

    $(document).ready(function () {
        var PanelState1 = $('#<%= hdnBUState.ClientID %>').val();
        //$("#hdnBUState").val();
        if (PanelState1 == '1') {
            $('#PannelFieldsList').removeClass("collapse").addClass("active");
            $('#' + 'divSearchbtn').removeClass("collapse").addClass("active");
            $('#' + 'divCollapsebtn').removeClass("collapse").addClass("active");
            // $('#<%= ImgHiddenFieldsList.ClientID %>').hide();
            //$("#div1").css("display","block");
        }

    });

    function Clear() {
        if ($('#<%= txtSearch.ClientID %>').val() == "Search...") {
            $('#<%= txtSearch.ClientID %>').val("");
        }
    }
    function SetText() {

        if ($('#<%= txtSearch.ClientID %>').val() == "") {
            $('#<%= txtSearch.ClientID %>').val("Search...");
        }
    }

    function CollapseExpand(me, HiddenPanelState) {
        var Panelstate = $('#' + me).attr("class");
        $('#' + me).slideToggle('slow');

        if (Panelstate == "collapse") {
            $('#' + me).removeClass("collapse").addClass("active");
            $('#' + 'divSearchbtn').removeClass("collapse").addClass("active");
            $('#' + 'divExpandbtn').removeClass("active").addClass("collapse");
            $('#' + 'divCollapsebtn').removeClass("collapse").addClass("active");
            SetPanelState(HiddenPanelState, 1)
        }
        else {

            $('#' + me).removeClass("active").addClass("collapse");
            $('#' + 'divSearchbtn').removeClass("active").addClass("collapse");
            $('#' + 'divCollapsebtn').removeClass("active").addClass("collapse");
            $('#' + 'divExpandbtn').removeClass("collapse").addClass("active");
            SetPanelState(HiddenPanelState, 0)
        }
    }
    function SetPanelState(HiddenPanelState, StateValue) {
        if (HiddenPanelState == 'hdnBUState') {
            $('#<%= hdnBUState.ClientID %>').val(StateValue);
        }
    }
    
</script>
<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>
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
<div>
    <div class="main-container" style="width: 1000px;">
        <div>
            <div class="row-div">
                <div class="row-div clearfix">
                    <div style="width: 16%;" class="field-div1 w200">
                        <div id="PannelFieldsList" style="background-color: #eeeeee; border: 1px solid #e4cda6;
                            height: 460px;" class="collapse">
                            <asp:UpdatePanel ID="UpAreasOfExp" UpdateMode="Always" runat="server">
                                <ContentTemplate>
                                    <div style="width: 100%; background-color: #eeeeee;">
                                        <br />
                                        <div style="text-align: center;">
                                            <asp:Label ID="Label1" runat="server" Width="146px" Style="font-weight: bold;" Text="Business Unit"></asp:Label>
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div style="text-align: center;">
                                            <asp:Button runat="server" ID="lnkShowAll" Style="font-size: 8pt;" CssClass="submitBtn"
                                                Text="Show All" />
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtSearch" Text="Search..." Style="border: 1px solid #555;" AutoPostBack="false"
                                                onblur="SetText();" onclick="Clear();" Width="148px" runat="server"></asp:TextBox>
                                            <telerik:RadListBox ID="radBusinessUnit" Height="340px" SelectionMode="Single" Width="150px"
                                                runat="server">
                                            </telerik:RadListBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div style="background-color: #eeeeee; border: 1px solid #e4cda6; height: 460px;"
                        class="field-div1 w650">
                        <div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                        <div id="divSearchbtn" class="collapse">
                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSearch" runat="server" Style="font-size: 8pt;" Text="Search" Width="55px"
                                        class="submitBtn" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div>
                            <br />
                        </div>
                        <div>
                            <div id="divExpandbtn" class="collapse">
                                <asp:Image ID="ImgHiddenFieldsList" runat="server" onclick="CollapseExpand('PannelFieldsList','hdnBUState')"
                                    ImageUrl="~/Images/CAI/Expand.png" />
                            </div>
                            <div id="divCollapsebtn" class="collapse">
                                <asp:Image ID="ImgHiddenFieldsList2" runat="server" onclick="CollapseExpand('PannelFieldsList','hdnBUState')"
                                    ImageUrl="~/Images/CAI/Collapse.png" />
                            </div>
                            <%--<img alt="" id="ImgHiddenFieldsList" src="~/Images/CAI/Expand.png" />--%>
                            <asp:HiddenField ID="hdnBUState" Value="1" runat="server" />
                        </div>
                    </div>
                    <div style="width: 75%;" class="field-div1 w650">
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                                <ContentTemplate>
                                    <div style="width: 100%;">
                                        <div>
                                            <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <b>Students</b></div>
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <asp:Button ID="btnThisBU" runat="server" Style="font-size: 8pt;" Text="This BU"
                                                Width="72px" class="submitBtn" />
                                            &nbsp;Please click to show students assigned just to this Business Unit
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <asp:Button ID="btnShowAll" runat="server" Style="font-size: 8pt;" Text="Show All"
                                                Width="72px" class="submitBtn" />
                                            &nbsp;Please click to show all students
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <telerik:RadGrid ID="radStudent" runat="server" AllowPaging="false" AllowSorting="True"
                                                AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AutoGenerateColumns="false"
                                                Visible="true" Style="margin-top: 13px; overflow: auto; width: 450px;" Height="300px"
                                                ShowHeadersWhenNoRecords="true">
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                                                    </Scrolling>
                                                </ClientSettings>
                                                <MasterTableView AllowSorting="true" AllowNaturalSort="false" EnableNoRecordsTemplate="true"
                                                    AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" TableLayout="Fixed">
                                                    <NoRecordsTemplate>
                                                        <asp:Label ID="lblNoRecord" runat="server" Text="No Record Found" Font-Bold="true"
                                                            ForeColor="Red"></asp:Label>
                                                    </NoRecordsTemplate>
                                                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column"
                                                        Visible="True">
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="3%" HeaderText="" ItemStyle-HorizontalAlign="Left"
                                                            ShowFilterIcon="false" AllowFiltering="false" UniqueName="SelectAll">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAllStudent" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                                    AutoPostBack="true" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" OnCheckedChanged="ToggleRowSelection"
                                                                    Checked='<%#IIf(Eval("Ischeck")=1,true,false) %>' />
                                                                <asp:HiddenField runat="server" ID="hdBLID" Value='<%# Eval("BusinessUnitLinkID")%>' />
                                                                <asp:HiddenField runat="server" ID="hidCompanyID" Value='<%# Eval("CompanyID")%>' />
                                                                <asp:HiddenField runat="server" ID="hidStudentID" Value='<%# Eval("StudentNo")%>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="3%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                            DataField="OldID" FilterControlWidth="100%" HeaderText="Student No." ItemStyle-HorizontalAlign="Left"
                                                            ShowFilterIcon="false" SortExpression="OldID" HeaderStyle-Width="13%" HeaderStyle-HorizontalAlign="left">
                                                            <ItemStyle HorizontalAlign="left" Width="13%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            DataField="FirstLast" FilterControlWidth="100%" HeaderStyle-Width="20%" HeaderText="Student Name"
                                                            ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="FirstLast"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemStyle HorizontalAlign="left" Width="20%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            DataField="email1" FilterControlWidth="100%" HeaderStyle-Width="20%" HeaderText="Email"
                                                            ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="email1"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            DataField="CompName" FilterControlWidth="100%" HeaderText="Office" ItemStyle-HorizontalAlign="Left"
                                                            ShowFilterIcon="false" SortExpression="CompName" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="left">
                                                            <ItemStyle HorizontalAlign="left" Width="20%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Bus. Unit" DataField="BusinessUnit" SortExpression="BusinessUnit"
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                            AllowFiltering="true" HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="cmbBusinessUnit" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hidBusinessUnit" Value='<%# DataBinder.Eval(Container.DataItem,"BusinessUnitID") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="left" Width="30%" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            DataField="MentorName" FilterControlWidth="100%" HeaderText="Mentor" HeaderStyle-Width="20%"
                                                            ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="MentorName"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemStyle HorizontalAlign="left" Width="20%" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                        </EditColumn>
                                                    </EditFormSettings>
                                                </MasterTableView>
                                                <FilterMenu EnableImageSprites="False">
                                                </FilterMenu>
                                            </telerik:RadGrid>
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div align="right">
                                            <asp:Button runat="server" ID="btnMainBack" CssClass="submitBtn" Text="Back" />
                                            <asp:Button runat="server" ID="btnSubmit" CssClass="submitBtn" Text="Submit" />
                                        </div>
                                    </div>
                                    <div>
                                        <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Modal="True"
                                            BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                                            Title="Business Unit Assignment" Behavior="None" Height="150px">
                                            <ContentTemplate>
                                                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                                                    padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblValidationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <div>
                                                                <br />
                                                            </div>
                                                            <div>
                                                                <asp:Button ID="btnValidationOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </telerik:RadWindow>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <br />
            <br />
        </div>
    </div>
</div>
