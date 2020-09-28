<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AssignMentorTM__c.ascx.vb"
    Inherits="AssignMentorTM__c" %>
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
                                            <asp:Label ID="lblMent" runat="server" Width="146px" Style="font-weight: bold;" Text="Mentors"></asp:Label>
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
                                            <telerik:RadListBox ID="radMentors" Height="340px" SelectionMode="Single" Width="150px"
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
                                            <asp:Label ID="lblmsg" ForeColor="Blue" runat="server" Visible="True" />
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <b>Students</b>
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <asp:Button ID="btnMentor" runat="server" Style="font-size: 8pt;" Text="Mentor Only"
                                                Width="90px" class="submitBtn" />
                                            Please click to show students assigned just to this Mentor
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <asp:Button ID="btnShowAll" runat="server" Style="font-size: 8pt;" Text="Show All"
                                                Width="90px" class="submitBtn" />
                                            Please click to show all students
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <telerik:RadGrid ID="radStudent" HeaderStyle-CssClass="GridViewHeader" runat="server" AllowPaging="false" AllowSorting="True"
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
                                                        <telerik:GridTemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false"
                                                            AllowFiltering="false" HeaderStyle-Width="3%" UniqueName="SelectAll">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAllStudent" runat="server" AutoPostBack="true" OnCheckedChanged="ToggleSelectedState" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkStudent" runat="server" />
                                                                <asp:HiddenField runat="server" ID="hdnMentorID" Value='<%# Eval("MentorID")%>' />
                                                                <asp:HiddenField runat="server" ID="hdnCompanyID" Value='<%# Eval("CompanyID")%>' />
                                                                <asp:HiddenField runat="server" ID="hdnStudentID" Value='<%# Eval("StudentNo")%>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                            DataField="OldID" FilterControlWidth="100%" HeaderText="Student No." ItemStyle-HorizontalAlign="Left"
                                                            ShowFilterIcon="false" SortExpression="OldID" HeaderStyle-HorizontalAlign="left">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            DataField="FirstLast" FilterControlWidth="100%" HeaderText="Student Name" ItemStyle-HorizontalAlign="Left"
                                                            ShowFilterIcon="false" SortExpression="FirstLast" HeaderStyle-HorizontalAlign="left">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            DataField="email1" FilterControlWidth="100%" HeaderText="Email" ItemStyle-HorizontalAlign="Left"
                                                            ShowFilterIcon="false" SortExpression="email1" HeaderStyle-HorizontalAlign="left">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            DataField="CompName" FilterControlWidth="100%" HeaderText="Office" ItemStyle-HorizontalAlign="Left"
                                                            ShowFilterIcon="false" SortExpression="CompName" HeaderStyle-HorizontalAlign="left">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            DataField="BusinessUnit" FilterControlWidth="100%" HeaderText="Bus. Unit" ItemStyle-HorizontalAlign="Left"
                                                            ShowFilterIcon="false" SortExpression="BusinessUnit" HeaderStyle-HorizontalAlign="left">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Mentor" DataField="MentorName" SortExpression="MentorName"
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                            AllowFiltering="true" HeaderStyle-Width="28%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlMentor" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdnMentor" Value='<%# DataBinder.Eval(Container.DataItem,"MentorID") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Start Date" AllowFiltering="false" DataField="StartDate"
                                                            SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                            ShowFilterIcon="false" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <telerik:RadDatePicker ID="txtGvStartDate" MaxDate ="01-01-2999" MinDate ="01-01-1800" Width="110px" SelectedDate='<%# If((Eval("StartDate") IsNot Nothing AndAlso TypeOf Eval("StartDate") Is DateTime), Convert.ToDateTime(Eval("StartDate")), CType(Nothing, System.Nullable(Of DateTime))) %>'
                                                                    runat="server">
                                                                    <DatePopupButton ToolTip="" />
                                                                </telerik:RadDatePicker>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="s" runat="server"
                                                                    ControlToValidate="txtGvStartDate" ErrorMessage="Start Date Required" Display="Dynamic"
                                                                    CssClass="required-label"></asp:RequiredFieldValidator>
                                                                <telerik:RadToolTip ID="RadToolTipGv" runat="server" IsClientID="true" Text="" AutoCloseDelay="20000" />
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="End Date" AllowFiltering="false" HeaderStyle-Width="15%"
                                                            DataField="EndDate" SortExpression="EndDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                            ShowFilterIcon="false">
                                                            <ItemTemplate>
                                                                <telerik:RadDatePicker ID="txtGvEndDate" MaxDate ="01-01-2999" MinDate ="01-01-1800" Width="110px" SelectedDate='<%# If((Eval("EndDate") IsNot Nothing AndAlso TypeOf Eval("EndDate") Is DateTime), Convert.ToDateTime(Eval("EndDate")), CType(Nothing, System.Nullable(Of DateTime))) %>'
                                                                    runat="server">
                                                                    <DatePopupButton ToolTip="" />
                                                                </telerik:RadDatePicker>
                                                                <telerik:RadToolTip ID="RadToolTipGv2" runat="server" IsClientID="true" Text="" AutoCloseDelay="20000" />
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </telerik:GridTemplateColumn>
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
