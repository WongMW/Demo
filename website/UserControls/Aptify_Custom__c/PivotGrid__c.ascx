<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PivotGrid__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_PivotGrid__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<style type="text/css">
    div.qsf-right-content .qsf-col-wrap
    {
        position: static;
    }
    
    .RadPivotGrid_Metro .rpgContentZoneDiv td
    {
        white-space: nowrap;
    }
    .cpHeader
    {
        color: white;
        background-color: #719DDB;
        font: bold 11px auto "Trebuchet MS" , Verdana;
        font-size: 12px;
        cursor: pointer;
        width: 500px;
        height: 18px;
        padding: 4px;
    }
    .cpBody
    {
        background-color: #DCE4F9;
        font-family: Arial;
        font-weight: bold;
        font-size: 12px;
        border: 1px gray;
        overflow-x: hidden;
        width: 500px;
        height: 0px;
        padding: 4px;
        padding-top: 7px;
    }
    body
    {
        background-color: #ECF5FB;
        background-image: url(/Ebusiness/Images/CAI/Stage_BG_btm.png);
        background-position: center bottom;
        background-repeat: repeat-x;
        font-family: Tahoma,Verdana,Segoe,sans-serif;
        font-size: 70%;
        padding-bottom: 20px;
    }
    
    .Container
    {
        margin: auto;
        min-height: 400px;
        background: #ffffff;
        max-width: 500px;
        min-width: 500px;
        border: solid 1px #d4d4d4;
        padding: 0 20px 20px 20px;
    }
    
    .ToolBar
    {
        border: solid 1px #d4d4d4;
        padding: 10px;
        margin-bottom: 20px;
    }
    
    .GridContainer
    {
        background: #ECF5FB;
        min-height: 300px;
        border: solid 1px #d4d4d4;
    }
    
    
    .ModalPopupBG
    {
        background-color: #666699;
        filter: alpha(opacity=50);
        opacity: 0.7;
    }
    
    .popup_Container
    {
        background-color: White;
        border: 2px solid #000000;
        padding: 0px 0px 0px 0px;
    }
    
    .popupConfirmation
    {
        width: 600px;
        height: 600px;
    }
    
    .popup_Titlebar
    {
        background: url(/Ebusiness/Images/CAI/titlebar_bg.jpg);
        height: 29px;
    }
    
    .popup_Body
    {
        padding: 15px 15px 15px 15px;
        font-family: Arial;
        font-weight: bold;
        font-size: 12px;
        color: #000000;
        line-height: 15pt;
        clear: both;
        height: 500px;
        max-height: 500px;
        overflow: auto;
    }
    
    .TitlebarLeft
    {
        float: left;
        padding-left: 5px;
        padding-top: 5px;
        font-family: Arial, Helvetica, sans-serif;
        font-weight: bold;
        font-size: 12px;
        color: #FFFFFF;
    }
    .TitlebarRight
    {
        background: url(/Ebusiness/Images/CAI/cross_icon_normal.png);
        background-position: right;
        background-repeat: no-repeat;
        height: 15px;
        width: 16px;
        float: right;
        cursor: pointer;
        margin-right: 5px;
        margin-top: 5px;
    }
    .popup_Buttons
    {
        margin: 10px;
    }
    .completed
    {
        width: 20px;
        height: 15px;
        background-color: Green;
    }
    .current
    {
        width: 20px;
        height: 15px;
        background-color: White;
    }
    .unavailable
    {
        width: 20px;
        height: 15px;
        background-color: Gray;
    }
</style>
<div>
    <table width="100%">
        <tr>
            <td width="10%">
            </td>
            <td width="30%">
                <asp:Label ID="lblAcademicYear" runat="server" Font-Bold="True" Font-Size="8" Text="Academic Year: "></asp:Label>
                <asp:DropDownList ID="ddlAcademicYear" runat="server">
                </asp:DropDownList>
            </td>
            <td width="30%">
                <asp:Label ID="lblVenue" runat="server" Font-Bold="True" Font-Size="8" Text="Venue: "></asp:Label>
                <asp:DropDownList ID="ddlVenue" runat="server">
                </asp:DropDownList>
            </td>
            <td width="20%">
            </td>
            <td width="5%">
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" class="submitBtn" />
            </td>
            <td width="5%">
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
    </table>
    <table width="50%">
        <tr>
            <td width="20%">
            </td>
            <td width="10%">
                <div class="completed">
                </div>
            </td>
            <td>
                <asp:Label ID="lblCompleted" runat="server" Font-Bold="True" Font-Size="8" Text="Completed"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="10%">
            </td>
            <td width="10%">
                <div class="current">
                </div>
            </td>
            <td>
                <asp:Label ID="lblCurrent" runat="server" Font-Bold="True" Font-Size="8" Text="Current Options for Enrollment"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <div class="unavailable">
                </div>
            </td>
            <td>
                <asp:Label ID="lblNotAvailable" runat="server" Font-Bold="True" Font-Size="8" Text="Not yet eligible to Enroll or Not Available"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
    </table>
</div>
<div class="maindiv">
    <asp:Label ID="lblMessage" runat="server" Visible="false" Text="No records found"></asp:Label>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadSkinManager ID="QsfSkinManager" runat="server" ShowChooser="true" />
    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All"
        EnableRoundedCorners="false" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadPivotGrid runat="server" ID="gvFirmEnrollment" AllowPaging="True" PageSize="10"
            AccessibilitySettings-OuterTableCaption="" Width="100%" AllowFiltering="True"
            ShowFilterHeaderZone="true" ShowColumnHeaderZone="True" ShowDataHeaderZone="True"
            ColumnGroupsDefaultExpanded="true" ColumnHeaderZoneText="ColumnHeaderZone" TotalsSettings-RowsSubTotalsPosition="None"
            TotalsSettings-ColumnsSubTotalsPosition="None" TotalsSettings-ColumnGrandTotalsPosition="None"
            TotalsSettings-GrandTotalsVisibility="None" Culture="en-GB">
            <%-- <SortExpressions>
                <telerik:PivotGridSortExpression FieldName="CapSubParts" SortOrder="Descending" />
                <telerik:PivotGridSortExpression FieldName="CapParts" SortOrder="Descending" />
            </SortExpressions> --%>
            <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" ChangePageSizeLabelText="Page Size:">
            </PagerStyle>
            <ClientSettings EnableFieldsDragDrop="true">
                <Scrolling AllowVerticalScroll="true"></Scrolling>
            </ClientSettings>
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
        </telerik:RadPivotGrid>
        <telerik:RadWindow ID="radStundentEnrollment" runat="server" Width="600px" Height="620px"
            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
            Title="Firm Enrollment" Behavior="None">
            <ContentTemplate>
                <div class="popup_Body">
                    <table width="100%">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td width="5%">
                            </td>
                            <td width="12%">
                                <asp:Label ID="lblLastName" runat="server" Text="Last Name:"></asp:Label>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="txtLastName" Enabled="false" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td width="5%">
                            </td>
                            <td width="12%">
                                <asp:Label ID="lblFirstName" runat="server" Text="First Name:"></asp:Label>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="txtFirstName" Enabled="false" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td width="5%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblRoute" runat="server" Text="Route:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRoute" Enabled="false" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblVenues" runat="server" Text="Venue:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVenuesList" runat="server" AutoPostBack="true" Width="100%">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="5">
                                <ajax:CollapsiblePanelExtender ID="cpeCap1" runat="server" TargetControlID="pnlCap1"
                                    CollapseControlID="pnlCAP1Header" ExpandControlID="pnlCAP1Header" Collapsed="False"
                                    TextLabelID="lblCap1" CollapsedText="CAP1" ExpandedText="CAP1" CollapsedSize="0"
                                    ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                                    ImageControlID="Image1">
                                </ajax:CollapsiblePanelExtender>
                                <asp:Panel ID="pnlCAP1Header" runat="server" CssClass="cpHeader">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCap1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/uparrow.jpg" CausesValidation="false" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlCap1" runat="server" CssClass="cpBody">
                                    <table width="100%">
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td width="15%">
                                                <asp:Label ID="lblCap1Timetable" runat="server" Text="Time Table:"></asp:Label>
                                            </td>
                                            <td width="75%">
                                                <asp:DropDownList ID="ddlCap1TimetableList" runat="server" Width="94%">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="5%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="2">
                                                <telerik:RadGrid ID="gvCap1" runat="server" AllowPaging="False" AllowSorting="True"
                                                    Skin="Outlook" OnNeedDataSource="gvCap1_NeedDataSource" AllowFilteringByColumn="False"
                                                    CellSpacing="0" GridLines="None" AutoGenerateColumns="false" Width="95%">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn SortExpression="StudentID" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="StudentID" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="Name" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="Name">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP1Courses" HeaderText="Courses">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsCoursesChecked" BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("CourseData")) = 2, True, IIf((Eval("CourseData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsCoursesCompleted" runat="server" Checked='<%# IIf((Eval("CourseData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("CourseData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP1Exam" HeaderText="Exam">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExamData" runat="server" Visible="false" Text='<%# Eval("ExamData")%>'></asp:Label>
                                                                    <asp:Label ID="lblIsExamChecked" Visible='<%# IIf((Eval("ExamData")) = 2, True, IIf((Eval("ExamData")) = 3,true,false ))%>'
                                                                        BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:DropDownList ID="ddlExamsList" runat="server" Font-Size="Smaller">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP1Revision" HeaderText="Revision Course">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsRevisionChecked" BackColor='<%# IIf((Eval("RevisionData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("RevisionData")) = 2, True, IIf((Eval("RevisionData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsRevisionCompleted" runat="server" Checked='<%# IIf((Eval("RevisionData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("RevisionData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP1ResitInterim" HeaderText="Resit Interim Assessment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsResitInterimChecked" BackColor='<%# IIf((Eval("ResitInterimData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData")) = 2, True, IIf((Eval("ResitInterimData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsResitInterimCompleted" runat="server" Checked='<%# IIf((Eval("ResitInterimData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="5">
                                <ajax:CollapsiblePanelExtender ID="cpeCap2" runat="server" TargetControlID="pnlCap2"
                                    CollapseControlID="pnalCap2Header" ExpandControlID="pnalCap2Header" Collapsed="False"
                                    TextLabelID="lblCap2" CollapsedText="CAP2" ExpandedText="CAP2" CollapsedSize="0"
                                    ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                                    ImageControlID="Image2">
                                </ajax:CollapsiblePanelExtender>
                                <asp:Panel ID="pnalCap2Header" runat="server" CssClass="cpHeader">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCap2" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/uparrow.jpg" CausesValidation="false" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlCap2" runat="server" CssClass="cpBody">
                                    <table width="100%">
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td width="15%">
                                                <asp:Label ID="lblCap2TimeTable" runat="server" Text="Time Table:"></asp:Label>
                                            </td>
                                            <td width="75%">
                                                <asp:DropDownList ID="ddlCap2TimeTableList" runat="server" Width="94%">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="5%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="2">
                                                <telerik:RadGrid ID="gvCap2" runat="server" AllowPaging="False" AllowSorting="True"
                                                    Skin="Outlook" OnNeedDataSource="gvCap1_NeedDataSource" AllowFilteringByColumn="False"
                                                    CellSpacing="0" GridLines="None" AutoGenerateColumns="false" Width="95%">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn SortExpression="ID" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="ID" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="Name" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="Name">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP2Courses" HeaderText="Courses">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsCoursesChecked" BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("CourseData")) = 2, True, IIf((Eval("CourseData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsCoursesCompleted" runat="server" Checked='<%# IIf((Eval("CourseData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("CourseData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP2Exam" HeaderText="Exam">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExamData" runat="server" Visible="false" Text='<%# Eval("ExamData")%>'></asp:Label>
                                                                    <asp:Label ID="lblIsExamChecked" Visible='<%# IIf((Eval("ExamData")) = 2, True, IIf((Eval("ExamData")) = 3,true,false ))%>'
                                                                        BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:DropDownList ID="ddlExamsList" runat="server" Font-Size="Smaller">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP2Revision" HeaderText="Revision Course">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsRevisionChecked" BackColor='<%# IIf((Eval("RevisionData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("RevisionData")) = 2, True, IIf((Eval("RevisionData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsRevisionCompleted" runat="server" Checked='<%# IIf((Eval("RevisionData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("RevisionData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP2ResitInterim" HeaderText="Resit Interim Assessment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsResitInterimChecked" BackColor='<%# IIf((Eval("ResitInterimData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData")) = 2, True, IIf((Eval("ResitInterimData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsResitInterimCompleted" runat="server" Checked='<%# IIf((Eval("ResitInterimData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="5">
                                <ajax:CollapsiblePanelExtender ID="cpeFAE" runat="server" TargetControlID="pnlFAE"
                                    CollapseControlID="pnalFAEHeader" ExpandControlID="pnalFAEHeader" Collapsed="False"
                                    TextLabelID="lblFAE" CollapsedText="FAE" ExpandedText="FAE" CollapsedSize="0"
                                    ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                                    ImageControlID="Image3">
                                </ajax:CollapsiblePanelExtender>
                                <asp:Panel ID="pnalFAEHeader" runat="server" CssClass="cpHeader">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFAE" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/uparrow.jpg" CausesValidation="false" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlFAE" runat="server" CssClass="cpBody">
                                    <table width="100%">
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td width="15%">
                                            </td>
                                            <td width="75%">
                                            </td>
                                            <td width="5%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="2">
                                                <telerik:RadGrid ID="gvFAE" runat="server" AllowPaging="False" AllowSorting="True"
                                                    Skin="Outlook" AllowFilteringByColumn="False" CellSpacing="0" GridLines="None"
                                                    AutoGenerateColumns="false" Width="95%">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn SortExpression="ID" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="ID" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="Name" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="Name">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn UniqueName="FAECourses" HeaderText="Courses">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsCoursesChecked" BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("CourseData")) = 2, True, IIf((Eval("CourseData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsCoursesCompleted" runat="server" Checked='<%# IIf((Eval("CourseData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("CourseData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="FAEExam" HeaderText="Exam">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExamData" runat="server" Visible="false" Text='<%# Eval("ExamData")%>'></asp:Label>
                                                                    <asp:Label ID="lblIsExamChecked" Visible='<%# IIf((Eval("ExamData")) = 2, True, IIf((Eval("ExamData")) = 3,true,false ))%>'
                                                                        BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:DropDownList ID="ddlExamsList" runat="server" Font-Size="Smaller">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="FAERevision" HeaderText="Revision Course">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsRevisionChecked" BackColor='<%# IIf((Eval("RevisionData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("RevisionData")) = 2, True, IIf((Eval("RevisionData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsRevisionCompleted" runat="server" Checked='<%# IIf((Eval("RevisionData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("RevisionData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="FAEResitInterim" HeaderText="Resit Interim Assessment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsResitInterimChecked" BackColor='<%# IIf((Eval("ResitInterimData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData")) = 2, True, IIf((Eval("ResitInterimData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsResitInterimCompleted" runat="server" Checked='<%# IIf((Eval("ResitInterimData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="5">
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="popup_Buttons">
                    <asp:Button ID="btnSave" runat="server" Text="Ok" Class="submitBtn" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submitBtn" />
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <%-- <ajax:ModalPopupExtender ID="mpeStudentEnrollment" BackgroundCssClass="ModalPopupBG"
            runat="server" CancelControlID="btnCancel" OkControlID="btnSave" TargetControlID="btnClick"
            PopupControlID="divStudentEnrollment" Drag="true" PopupDragHandleControlID="PopupHeader">
        </ajax:ModalPopupExtender>
        <asp:Button ID="btnClick" runat="server" />
       <div id="divStudentEnrollment" style="display: none;" class="popupConfirmation">
            <div class="popup_Container">
                <div class="popup_Titlebar" id="PopupHeader">
                    <div class="TitlebarLeft">
                        Firm Enrollment</div>
                    <div class="TitlebarRight" onclick="$get('btnCancel').click();">
                    </div>
                </div>
                <div class="popup_Body">
                    <table width="100%">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td width="5%">
                            </td>
                            <td width="12%">
                                <asp:Label ID="lblLastName" runat="server" Text="Last Name:"></asp:Label>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="txtLastName" Enabled="false" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td width="5%">
                            </td>
                            <td width="12%">
                                <asp:Label ID="lblFirstName" runat="server" Text="First Name:"></asp:Label>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="txtFirstName" Enabled="false" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td width="5%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblRoute" runat="server" Text="Route:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRoute" Enabled="false" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblVenues" runat="server" Text="Venue:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVenuesList" runat="server" AutoPostBack="true" Width="100%">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="5">
                                <ajax:CollapsiblePanelExtender ID="cpeCap1" runat="server" TargetControlID="pnlCap1"
                                    CollapseControlID="pnlCAP1Header" ExpandControlID="pnlCAP1Header" Collapsed="False"
                                    TextLabelID="lblCap1" CollapsedText="CAP1" ExpandedText="CAP1" CollapsedSize="0"
                                    ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                                    ImageControlID="Image1">
                                </ajax:CollapsiblePanelExtender>
                                <asp:Panel ID="pnlCAP1Header" runat="server" CssClass="cpHeader">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCap1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/uparrow.jpg" CausesValidation="false" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlCap1" runat="server" CssClass="cpBody">
                                    <table width="100%">
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td width="15%">
                                                <asp:Label ID="lblCap1Timetable" runat="server" Text="Time Table:"></asp:Label>
                                            </td>
                                            <td width="75%">
                                                <asp:DropDownList ID="ddlCap1TimetableList" runat="server" Width="94%">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="5%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="2">
                                                <telerik:RadGrid ID="gvCap1" runat="server" AllowPaging="False" AllowSorting="True"
                                                    Skin="Outlook" OnNeedDataSource="gvCap1_NeedDataSource" AllowFilteringByColumn="False"
                                                    CellSpacing="0" GridLines="None" AutoGenerateColumns="false" Width="95%">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn SortExpression="StudentID" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="StudentID" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="Name" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="Name">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP1Courses" HeaderText="Courses">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsCoursesChecked" BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("CourseData")) = 2, True, IIf((Eval("CourseData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsCoursesCompleted" runat="server" Checked='<%# IIf((Eval("CourseData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("CourseData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP1Exam" HeaderText="Exam">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExamData" runat="server" Visible="false" Text='<%# Eval("ExamData")%>'></asp:Label>
                                                                    <asp:Label ID="lblIsExamChecked" Visible='<%# IIf((Eval("ExamData")) = 2, True, IIf((Eval("ExamData")) = 3,true,false ))%>'
                                                                        BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:DropDownList ID="ddlExamsList" runat="server" Font-Size="Smaller">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP1Revision" HeaderText="Revision Course">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsRevisionChecked" BackColor='<%# IIf((Eval("RevisionData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("RevisionData")) = 2, True, IIf((Eval("RevisionData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsRevisionCompleted" runat="server" Checked='<%# IIf((Eval("RevisionData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("RevisionData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP1ResitInterim" HeaderText="Resit Interim Assessment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsResitInterimChecked" BackColor='<%# IIf((Eval("ResitInterimData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData")) = 2, True, IIf((Eval("ResitInterimData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsResitInterimCompleted" runat="server" Checked='<%# IIf((Eval("ResitInterimData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="5">
                                <ajax:CollapsiblePanelExtender ID="cpeCap2" runat="server" TargetControlID="pnlCap2"
                                    CollapseControlID="pnalCap2Header" ExpandControlID="pnalCap2Header" Collapsed="False"
                                    TextLabelID="lblCap2" CollapsedText="CAP2" ExpandedText="CAP2" CollapsedSize="0"
                                    ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                                    ImageControlID="Image2">
                                </ajax:CollapsiblePanelExtender>
                                <asp:Panel ID="pnalCap2Header" runat="server" CssClass="cpHeader">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCap2" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/uparrow.jpg" CausesValidation="false" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlCap2" runat="server" CssClass="cpBody">
                                    <table width="100%">
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td width="15%">
                                                <asp:Label ID="lblCap2TimeTable" runat="server" Text="Time Table:"></asp:Label>
                                            </td>
                                            <td width="75%">
                                                <asp:DropDownList ID="ddlCap2TimeTableList" runat="server" Width="94%">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="5%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="2">
                                                <telerik:RadGrid ID="gvCap2" runat="server" AllowPaging="False" AllowSorting="True"
                                                    Skin="Outlook" OnNeedDataSource="gvCap1_NeedDataSource" AllowFilteringByColumn="False"
                                                    CellSpacing="0" GridLines="None" AutoGenerateColumns="false" Width="95%">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn SortExpression="ID" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="ID" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="Name" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="Name">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP2Courses" HeaderText="Courses">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsCoursesChecked" BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("CourseData")) = 2, True, IIf((Eval("CourseData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsCoursesCompleted" runat="server" Checked='<%# IIf((Eval("CourseData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("CourseData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP2Exam" HeaderText="Exam">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExamData" runat="server" Visible="false" Text='<%# Eval("ExamData")%>'></asp:Label>
                                                                    <asp:Label ID="lblIsExamChecked" Visible='<%# IIf((Eval("ExamData")) = 2, True, IIf((Eval("ExamData")) = 3,true,false ))%>'
                                                                        BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:DropDownList ID="ddlExamsList" runat="server" Font-Size="Smaller">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP2Revision" HeaderText="Revision Course">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsRevisionChecked" BackColor='<%# IIf((Eval("RevisionData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("RevisionData")) = 2, True, IIf((Eval("RevisionData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsRevisionCompleted" runat="server" Checked='<%# IIf((Eval("RevisionData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("RevisionData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="CAP2ResitInterim" HeaderText="Resit Interim Assessment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsResitInterimChecked" BackColor='<%# IIf((Eval("ResitInterimData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData")) = 2, True, IIf((Eval("ResitInterimData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsResitInterimCompleted" runat="server" Checked='<%# IIf((Eval("ResitInterimData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="5">
                                <ajax:CollapsiblePanelExtender ID="cpeFAE" runat="server" TargetControlID="pnlFAE"
                                    CollapseControlID="pnalFAEHeader" ExpandControlID="pnalFAEHeader" Collapsed="False"
                                    TextLabelID="lblFAE" CollapsedText="FAE" ExpandedText="FAE" CollapsedSize="0"
                                    ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                                    ImageControlID="Image3">
                                </ajax:CollapsiblePanelExtender>
                                <asp:Panel ID="pnalFAEHeader" runat="server" CssClass="cpHeader">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFAE" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/uparrow.jpg" CausesValidation="false" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlFAE" runat="server" CssClass="cpBody">
                                    <table width="100%">
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td width="15%">
                                            </td>
                                            <td width="75%">
                                            </td>
                                            <td width="5%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="2">
                                                <telerik:RadGrid ID="gvFAE" runat="server" AllowPaging="False" AllowSorting="True"
                                                    Skin="Outlook" AllowFilteringByColumn="False" CellSpacing="0" GridLines="None"
                                                    AutoGenerateColumns="false" Width="95%">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn SortExpression="ID" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="ID" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="Name" HeaderText="Class" HeaderButtonType="TextButton"
                                                                DataField="Name">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn UniqueName="FAECourses" HeaderText="Courses">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsCoursesChecked" BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("CourseData")) = 2, True, IIf((Eval("CourseData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsCoursesCompleted" runat="server" Checked='<%# IIf((Eval("CourseData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("CourseData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="FAEExam" HeaderText="Exam">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExamData" runat="server" Visible="false" Text='<%# Eval("ExamData")%>'></asp:Label>
                                                                    <asp:Label ID="lblIsExamChecked" Visible='<%# IIf((Eval("ExamData")) = 2, True, IIf((Eval("ExamData")) = 3,true,false ))%>'
                                                                        BackColor='<%# IIf((Eval("CourseData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:DropDownList ID="ddlExamsList" runat="server" Font-Size="Smaller">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="FAERevision" HeaderText="Revision Course">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsRevisionChecked" BackColor='<%# IIf((Eval("RevisionData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("RevisionData")) = 2, True, IIf((Eval("RevisionData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsRevisionCompleted" runat="server" Checked='<%# IIf((Eval("RevisionData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("RevisionData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="FAEResitInterim" HeaderText="Resit Interim Assessment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsResitInterimChecked" BackColor='<%# IIf((Eval("ResitInterimData")) = 2, System.Drawing.Color.FromName("Green"), System.Drawing.Color.FromName("Gray"))%>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData")) = 2, True, IIf((Eval("ResitInterimData")) = 3,true,false ))%>'
                                                                        Height="100%" Width="100%" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chkIsResitInterimCompleted" runat="server" Checked='<%# IIf((Eval("ResitInterimData"))=1,true,false) %>'
                                                                        Visible='<%# IIf((Eval("ResitInterimData"))>1,false,true) %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="5">
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="popup_Buttons">
                    <asp:Button ID="btnSave" runat="server" Text="Ok" Class="submitBtn"  />
                    <input id="btnCancel" value="Cancel" type="button" Class="submitBtn" />
                </div>
            </div>
        </div>--%>
    </telerik:RadAjaxPanel>
</div>
<cc1:User ID="User1" runat="server" />
