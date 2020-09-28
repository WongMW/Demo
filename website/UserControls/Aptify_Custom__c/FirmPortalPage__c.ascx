<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FirmPortalPage__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.FirmPortalPage__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div id="divContent" runat="server">
            <asp:Panel ID="pnlData" runat="server">
                <div class="info-data">
                    <asp:Label ID="lblExemptionsNotFound" runat="server" ForeColor="Red" Font-Bold="true">
                    </asp:Label>
                    <div class="actions clearfix">
                        <asp:RadioButton ID="rdoAcademicCycle" runat="server" GroupName="AcadmicCycle" Text="By Academic Cycle " AutoPostBack="true" Checked="true" />
                        <asp:RadioButton ID="rdoStudent" runat="server" AutoPostBack="true" GroupName="AcadmicCycle" Text="By Student " />
                    </div>

                    <div class="row-div clearfix">
                        <div class="label-div w30">
                            <asp:Label ID="lblAcademicYear" runat="server"><span class="RequiredField">*</span>Academic Cycle:</asp:Label>
                        </div>
                        <div class="field-div1 w60">
                            <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" Width="30%">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row-div clearfix">
                        <div class="label-div w30">
                            <asp:Label ID="lblCurriculum" runat="server"><span class="RequiredField">*</span>Curriculum:</asp:Label>
                        </div>
                        <div class="field-div1 w60">
                            <asp:DropDownList ID="ddlCurriculumList" runat="server" AutoPostBack="true" Width="30%">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row-div clearfix">
                        <div class="label-div w30">
                            <asp:Label ID="lblCourse" runat="server"><span class="RequiredField">*</span>Course:</asp:Label>
                            <asp:Label ID="lblStudent" runat="server" Visible="false"><span class="RequiredField">*</span>Student Number:</asp:Label>
                        </div>
                        <div class="field-div1 w60">
                            <div>
                                <asp:DropDownList ID="ddlCourseList" runat="server" AutoPostBack="true" Width="30%">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtStudent" AutoPostBack="true" runat="server" Width="30%" Visible="false" />
                                <Ajax:AutoCompleteExtender ID="aceStudent" runat="server" BehaviorID="autoComplete"
                                    CompletionInterval="10" CompletionSetCount="1" EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetStudentDetails" ServicePath="~/WebServices/GetCompanyDetails__c.asmx"
                                    TargetControlID="txtStudent" UseContextKey="true">
                                </Ajax:AutoCompleteExtender>
                            </div>
                            <div>
                                <asp:Button ID="btnDisplay" runat="server" Text="Display" CssClass="submitBtn" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlDetails" runat="server" Visible="true">
            <div class="cai-table mobile-table clearfix">
                <telerik:RadGrid ID="gvAssignmentDetails" HeaderStyle-CssClass="GridViewHeader" runat="server"
                    AllowPaging="false" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0"
                    GridLines="None" AutoGenerateColumns="false" Width="99%" Visible="true" Style="margin-top: 13px; overflow: auto"
                    Height="190px" ShowHeadersWhenNoRecords="true">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                    </ClientSettings>
                    <MasterTableView AllowSorting="true" AllowNaturalSort="false" DataKeyNames="StudentID"
                        EnableNoRecordsTemplate="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true"
                        FilterItemStyle-HorizontalAlign="Center">
                        <NoRecordsTemplate>
                            <div>
                                No Data to Display
                            </div>
                        </NoRecordsTemplate>
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column"
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                DataField="StudentID" FilterControlWidth="100%" HeaderStyle-Width="15%" HeaderText="Student#"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="StudentID"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="LastName" FilterControlWidth="100%" HeaderStyle-Width="20%" HeaderText="Last Name"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="LastName"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="FirstName" FilterControlWidth="100%" HeaderStyle-Width="20%" HeaderText="First Name"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="FirstName"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="AcademicCycle" FilterControlWidth="100%" HeaderStyle-Width="20%" HeaderText="Academic Cycle"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="AcademicCycle"
                                HeaderStyle-HorizontalAlign="Center" Display="false">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Route" FilterControlWidth="100%" HeaderStyle-Width="15%" HeaderText="Route"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Route"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="CurrentStage" FilterControlWidth="100%" HeaderStyle-Width="30%" HeaderText="Current Stage"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="CurrentStage"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="30%" />
                                <ItemStyle HorizontalAlign="center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Course" FilterControlWidth="100%" HeaderStyle-Width="20%" HeaderText="Course Name"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Course"
                                HeaderStyle-HorizontalAlign="Center" AllowFiltering="false">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="AssignmentName" FilterControlWidth="100%" HeaderStyle-Width="25%"
                                HeaderText="Assignment Name" ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false"
                                SortExpression="AssignmentName" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="25%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Type" FilterControlWidth="100%" HeaderStyle-Width="25%" HeaderText="Type"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Type"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="25%" />
                                <ItemStyle HorizontalAlign="center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Status" FilterControlWidth="100%" HeaderStyle-Width="28%" HeaderText="Status"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Status"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="28%" />
                                <ItemStyle HorizontalAlign="center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Score" FilterControlWidth="100%" HeaderStyle-Width="20%"
                                HeaderText="Score" ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Score"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="center" />
                            </telerik:GridBoundColumn>

                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </div>
        </asp:Panel>
        <div class="actions">
            <asp:Button ID="cmdBack" runat="server" CssClass="submitBtn" Text="Back" />
        </div>
        <div>
            <cc1:User ID="User1" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
