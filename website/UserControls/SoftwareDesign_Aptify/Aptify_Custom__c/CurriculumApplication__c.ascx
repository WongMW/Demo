<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/CurriculumApplication__c.ascx.vb"
    Inherits="CurriculumApplication__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="Login" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div id="divContent" runat="server">
    <div>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    <div id="MainTable">
        <div id="Curriculum" runat="server" class="cai-form">
            <asp:Panel ID="pnlCurriculum" runat="server">

                <span class="form-title">Curriculum
                        <span style="float: right;">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/downarrow.jpg"
                                AlternateText="(Show Details...)" />
                        </span>
                </span>
            </asp:Panel>
            <asp:Panel ID="pnlCurriculumData" runat="server">
                <div class="cai-form-content">
                    <span class="label-title">Curriculum Details</span>
                    <span class="label-title">Select Curriculum</span>
                    <asp:DropDownList ID="cmbCurriculum" runat="server">
                    </asp:DropDownList>
                </div>
            </asp:Panel>
            <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server" TargetControlID="pnlCurriculumData"
                ExpandControlID="pnlCurriculum" CollapseControlID="pnlCurriculum" Collapsed="true"
                ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                SuppressPostBack="true" />
        </div>

        <div id="Exemptions" runat="server" class="cai-form">
            <asp:Panel ID="pnlExemptions" runat="server">
                <span class="form-title">
                    <span>Exemptions</span>
                    <span style="float: right;">
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/downarrow.jpg"
                            AlternateText="(Show Details...)" />
                    </span>
                </span>
            </asp:Panel>
            <asp:Panel ID="pnlExemptionsData" runat="server">
                <div class="cai-form-content">
                    <span class="label-title">Details</span>
                    <asp:GridView ID="grdGrantedExempts" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Curriculum" HeaderText="Curriculum" />
                            <asp:BoundField DataField="Course" HeaderText="Course" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="Server" TargetControlID="pnlExemptionsData"
                ExpandControlID="pnlExemptions" CollapseControlID="pnlExemptions" Collapsed="true"
                ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                SuppressPostBack="true" />
        </div>

        <div id="ProgressInfo" runat="server" class="cai-form">
            <asp:Panel ID="pnlProgressInfo" runat="server">
                <span class="form-title">
                    <span>Curriculum Progress Information </span>
                    <span style="float: right;">
                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/downarrow.jpg"
                            AlternateText="(Show Details...)" />
                    </span>
                </span>
            </asp:Panel>
            <asp:Panel ID="pnlProgressInfoData" runat="server">
                <div class="cai-form-content">
                    <span class="label-title">ProgressInfo Details</span>
                    <span class="label-title">Select Route of Entry</span>
                    <asp:DropDownList ID="cmbRoutes" runat="server">
                    </asp:DropDownList>
                    <span class="label-title">Select Course Location
                    </span>
                    <asp:DropDownList ID="cmbLocation" runat="server">
                    </asp:DropDownList>
                </div>
            </asp:Panel>
            <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="Server" TargetControlID="pnlProgressInfoData"
                ExpandControlID="pnlProgressInfo" CollapseControlID="pnlProgressInfo" Collapsed="true"
                ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                SuppressPostBack="true" />
        </div>
        <div id="Courses" runat="server" class="cai-form">
            <asp:Panel ID="pnlCourses" runat="server">
                <span class="form-title"><span>Courses</span>
                    <span style="float: right;">
                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/downarrow.jpg"
                            AlternateText="(Show Details...)" />
                    </span>
                </span>
            </asp:Panel>
            <asp:Panel ID="pnlCoursesData" runat="server">
                <div class="cai-form-content">
                    <span class="label-title">Courses Details</span>
                    <rad:RadGrid ID="grdResults" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                        AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                        SortingSettings-SortedAscToolTip="Sorted Ascending">
                        <PagerStyle CssClass="sd-pager" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                            <Columns>
                                <rad:GridBoundColumn HeaderText="Courses" DataField="Courses" SortExpression="Courses"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn HeaderText="Location" DataField="Location" SortExpression="Location"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn HeaderText="Exempted" DataField="Exempted" SortExpression="Exempted"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                                </rad:GridBoundColumn>
                                <rad:GridCheckBoxColumn HeaderText="Register" DataField="Register">
                                </rad:GridCheckBoxColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>
                </div>
            </asp:Panel>
            <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="Server" TargetControlID="pnlCoursesData"
                ExpandControlID="pnlCourses" CollapseControlID="pnlCourses" Collapsed="true"
                ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                SuppressPostBack="true" />
        </div>
        <div id="CourseOptions" runat="server" class="cai-form">
            <asp:Panel ID="pnlCourseOptions" runat="server">
                <span class="form-title"><span>Course Options </span>
                    <span style="float: right;">
                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/downarrow.jpg"
                            AlternateText="(Show Details...)" />
                    </span>
                </span>
            </asp:Panel>
            <asp:Panel ID="pnlCourseOptionsData" runat="server">
                <div class="cai-form-content">
                    <span class="label-title">Course Options Details</span>

                    <span class="label-title">Select Time Table
                    </span>

                    <asp:DropDownList ID="cmbTimeTable" runat="server">
                    </asp:DropDownList>

                    <span class="label-title">Select Main Exam Location
                    </span>

                    <asp:DropDownList ID="cmbExamLocation" runat="server">
                    </asp:DropDownList>

                    <span class="label-title">Select Interim Assessment Location
                    </span>

                    <asp:DropDownList ID="cmbAssessmentLocation" runat="server">
                    </asp:DropDownList>

                    <span class="label-title">Please provide any further information
                    </span>

                    <asp:TextBox ID="txtFurtherInfo" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
            </asp:Panel>
            <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="Server" TargetControlID="pnlCourseOptionsData"
                ExpandControlID="pnlCourseOptions" CollapseControlID="pnlCourseOptions" Collapsed="true"
                ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                SuppressPostBack="true" />
        </div>

    </div>
</div>
<div class="actions">
    <asp:Button ID="btnPlaceOrder" runat="server" CssClass="submitBtn" Text="Place Order" />
</div>
<div>
    <Login:AptifyWebUserLogin ID="ucWebUserLogin" runat="server" Visible="False"></Login:AptifyWebUserLogin>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
