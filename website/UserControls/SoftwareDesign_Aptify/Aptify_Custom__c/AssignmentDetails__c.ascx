<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AssignmentDetails__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.AssignmentDetails__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1200px;">
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
<asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <%-- <script type="text/javascript">
        Sys.Application.add_load(CheckGrid);
    </script>--%>
        <div id="divContent" runat="server">
            <asp:Panel ID="pnlData" runat="server">
                <div class="info-data">
                    <asp:Label ID="lblExemptionsNotFound" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    <div class="row-div clearfix actions">
                        <asp:HyperLink ID="lnkHelp" Text="Help" runat="server" Target="_blank"></asp:HyperLink>
                    </div>
                    <div class="row-div clearfix">
                        <div class="row-div clearfix">
                            <div class="label-div">
                                <asp:Label ID="lblCurriculum" runat="server"><span class="RequiredField">*</span>Curriculum:</asp:Label>
                            </div>
                            <div class="field-div1">
                                <asp:DropDownList ID="ddlCurriculumList" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div ">
                                <asp:Label ID="lblCourse" runat="server"><span class="RequiredField">*</span>Course:</asp:Label>
                            </div>
                            <div class="field-div1 ">
                                <asp:DropDownList ID="ddlCourseList" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div ">
                                <asp:Label ID="lblAssignment" runat="server"><span class="RequiredField">*</span>Assignment Name:</asp:Label>
                            </div>
                            <div class="field-div1 ">
                                <asp:DropDownList ID="ddlAssignmentList" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="actions clearfix">
                        <asp:Button ID="btnDisplay" runat="server" CssClass="submitBtn" Text="Display" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlDetails" runat="server" Visible="true">
                <div c>
                    <telerik:RadGrid ID="gvAssignmentDetails" runat="server" AllowPaging="false" AllowSorting="True"
                        AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AutoGenerateColumns="false"
                        Visible="true" CssClass="cai-table clearfix mobile-table">
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
                                <telerik:GridTemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Left"
                                    ShowFilterIcon="false" AllowFiltering="false">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllStudent" runat="server" CssClass="cai-table-data" AutoPostBack="true" OnCheckedChanged="ToggleSelectedState" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Checkbox:" CssClass="mobile-label"></asp:Label>
                                        <asp:CheckBox ID="chkSelect" CssClass="cai-table-data" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />

                                        <asp:Label runat="server" Text="Student:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="label1" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StudentID") %>'></asp:Label>

                                        <asp:Label runat="server" Text="First Name:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="Label2" CssClass="cai-table-data  no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstName")%>'></asp:Label>

                                        <asp:Label runat="server" Text="Last Name:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="Label3" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastName")%>'></asp:Label>

                                        <asp:Label runat="server" Text="Lesson:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="Label4" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Lesson")%>'></asp:Label>

                                        <asp:Label runat="server" Text="Type:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="Label5" CssClass="cai-table-data  no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Type")%>'></asp:Label>

                                        <asp:HiddenField runat="server" ID="hdnPartStatusID" Value='<%# Eval("StudentID")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    DataField="StudentID" HeaderText="Student#" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob"
                                    ShowFilterIcon="false" SortExpression="StudentID">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="FirstName" HeaderText="First Name" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob"
                                    ShowFilterIcon="false" SortExpression="FirstName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="LastName" HeaderText="Last Name" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob"
                                    ShowFilterIcon="false" SortExpression="LastName"
                                    HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="Lesson" HeaderText="Lesson" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob"
                                    ShowFilterIcon="false" SortExpression="Lesson"
                                    HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="Type" HeaderText="Type" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob"
                                    ShowFilterIcon="false" SortExpression="Type"
                                    HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Score" ItemStyle-CssClass=""
                                    ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Score:" CssClass="mobile-label"></asp:Label>
                                        <asp:TextBox ID="txtScore" runat="server" CssClass="cai-table-data" ReadOnly="true" Visible="true"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="REV1" ControlToValidate="txtScore" ValidationExpression="\d+"
                                            Display="Static" EnableClientScript="true" CssClass="error-message " ErrorMessage="Please enter numbers only"
                                            ForeColor="Red" runat="server" />
                                        <asp:DropDownList ID="ddlscore" runat="server" Enabled="false" Visible="false">
                                            <asp:ListItem>NA (Not addressed)</asp:ListItem>
                                            <asp:ListItem>NC (Nominal Competence)</asp:ListItem>
                                            <asp:ListItem>RC (Reaching Competence)</asp:ListItem>
                                            <asp:ListItem>C (Competent)</asp:ListItem>
                                            <asp:ListItem>HC (Highly Competent)</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
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
                    </telerik:RadGrid><br />
                </div>
                <div class="row-div clearfix actions">
                    <asp:Button ID="cmdAdd" runat="server" CssClass="submitBtn" Text="Add"></asp:Button>
                </div>
            </asp:Panel>
            &nbsp;
        <asp:Panel ID="pnlAssignmnetDetails" runat="server" Visible="false">
            <div class="info-data">
                <div class="row-div clearfix">
                    <telerik:RadGrid ID="gvAssignment" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="10" AllowFilteringByColumn="False" CellSpacing="0" GridLines="None"
                        AutoGenerateColumns="false" Visible="true" Style="overflow: auto"
                        Height="100px">
                        <MasterTableView AllowSorting="true" AllowNaturalSort="false" DataKeyNames="StudentID"
                            EnableNoRecordsTemplate="true" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                            <NoRecordsTemplate>
                                <div>
                                    No Data to Display
                                </div>
                            </NoRecordsTemplate>
                            <Columns>
                                <rad:GridBoundColumn DataField="StudentID" HeaderText="Student#"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="StudentID"
                                    HeaderStyle-HorizontalAlign="Center">
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="FirstName" HeaderText="First Name"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="FirstName"
                                    HeaderStyle-HorizontalAlign="Center">
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="LastName" HeaderText="Last Name"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="LastName"
                                    HeaderStyle-HorizontalAlign="Center">
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="Lesson" HeaderText="Lesson"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Lesson"
                                    HeaderStyle-HorizontalAlign="Center">
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="Type" HeaderText="Type" ItemStyle-HorizontalAlign="Left"
                                    ShowFilterIcon="false" SortExpression="Type" HeaderStyle-HorizontalAlign="Center">
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="score" HeaderText="Score"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="score"
                                    HeaderStyle-HorizontalAlign="Center">
                                </rad:GridBoundColumn>
                                <rad:GridTemplateColumn HeaderText="Remove" ItemStyle-HorizontalAlign="Left"
                                    ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="Remove"
                                            CommandArgument='<%# Eval("ClassRegPartStatusID")%>' OnClick="btnRemove_Click"></asp:LinkButton>
                                        <asp:HiddenField runat="server" ID="hdnStudent" Value='<%# Eval("StudentID")%>' />
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid><br />
                </div>
            </div>
            <div class="row-div clearfix actions">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="cmdSubmit" runat="server" CssClass="submitBtn" Text="Submit"></asp:Button>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--<asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
    <ProgressTemplate>   
        <div class="modal">
            <div class="center">
                <img alt="Please Wait" src="../../Images/ajax-loader.gif" />
            </div>
        </div>                  
    </ProgressTemplate>
</asp:UpdateProgress> --%>
        </asp:Panel>
            &nbsp;
        </div>
        <div>
            <cc1:User ID="User1" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
