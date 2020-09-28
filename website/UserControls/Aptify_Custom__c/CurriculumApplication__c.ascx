<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CurriculumApplication__c.ascx.vb"
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
    <table id="MainTable" border="1" style="width: 90%">
        <tr>
            <td>
                <%--<div id="DvProfile" runat="server">
                    <asp:Panel ID="pnlProfile" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 5%">
                                            </td>
                                            <td>
                                                PROFILE
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/uparrow.jpg"
                                                        AlternateText="(Show Details...)" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlProfileData" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <uc1:Profile__c ID="usrControlProfile" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="Server" TargetControlID="pnlProfileData"
                        ExpandControlID="pnlProfile" CollapseControlID="pnlProfile" Collapsed="false"
                        ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                        ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                        SuppressPostBack="true" />
                </div>--%>
                <div id="Curriculum" runat="server">
                    <asp:Panel ID="pnlCurriculum" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 5%">
                                            </td>
                                            <td>
                                                Curriculum
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/downarrow.jpg"
                                                        AlternateText="(Show Details...)" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlCurriculumData" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    Curriculum Details
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                Select Curriculum
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbCurriculum" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server" TargetControlID="pnlCurriculumData"
                        ExpandControlID="pnlCurriculum" CollapseControlID="pnlCurriculum" Collapsed="true"
                        ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                        ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                        SuppressPostBack="true" />
                </div>
                <div id="Exemptions" runat="server">
                    <asp:Panel ID="pnlExemptions" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 5%">
                                            </td>
                                            <td>
                                                Exemptions
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/downarrow.jpg"
                                                        AlternateText="(Show Details...)" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlExemptionsData" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    Exemptions Details
                                    <asp:GridView ID="grdGrantedExempts" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Curriculum" HeaderText="Curriculum" />
                                            <asp:BoundField DataField="Course" HeaderText="Course" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="Server" TargetControlID="pnlExemptionsData"
                        ExpandControlID="pnlExemptions" CollapseControlID="pnlExemptions" Collapsed="true"
                        ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                        ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                        SuppressPostBack="true" />
                </div>
                <div id="ProgressInfo" runat="server">
                    <asp:Panel ID="pnlProgressInfo" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 5%">
                                            </td>
                                            <td>
                                                Curriculum Progress Information
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/downarrow.jpg"
                                                        AlternateText="(Show Details...)" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlProgressInfoData" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    ProgressInfo Details
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                Select Route of Entry
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbRoutes" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Select Course Location
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbLocation" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="Server" TargetControlID="pnlProgressInfoData"
                        ExpandControlID="pnlProgressInfo" CollapseControlID="pnlProgressInfo" Collapsed="true"
                        ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                        ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                        SuppressPostBack="true" />
                </div>
                <div id="Courses" runat="server">
                    <asp:Panel ID="pnlCourses" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 5%">
                                            </td>
                                            <td>
                                                Courses
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/downarrow.jpg"
                                                        AlternateText="(Show Details...)" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlCoursesData" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    Courses Details
                                    <rad:RadGrid ID="grdResults" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                        AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                                        SortingSettings-SortedAscToolTip="Sorted Ascending">
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
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="Server" TargetControlID="pnlCoursesData"
                        ExpandControlID="pnlCourses" CollapseControlID="pnlCourses" Collapsed="true"
                        ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                        ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                        SuppressPostBack="true" />
                </div>
                <div id="CourseOptions" runat="server">
                    <asp:Panel ID="pnlCourseOptions" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 5%">
                                            </td>
                                            <td>
                                                Course Options
                                            </td>
                                            <td>
                                                <div style="float: right;">
                                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/downarrow.jpg"
                                                        AlternateText="(Show Details...)" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlCourseOptionsData" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    Course Options Details
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                Select Time Table
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbTimeTable" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Select Main Exam Location
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbExamLocation" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Select Interim Assessment Location
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbAssessmentLocation" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Please provide any further information
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFurtherInfo" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="Server" TargetControlID="pnlCourseOptionsData"
                        ExpandControlID="pnlCourseOptions" CollapseControlID="pnlCourseOptions" Collapsed="true"
                        ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                        ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
                        SuppressPostBack="true" />
                </div>
            </td>
        </tr>
    </table>
</div>
<div>
    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" />
</div>
<div>
    <Login:AptifyWebUserLogin ID="ucWebUserLogin" runat="server" Width="175px" Height="9px"
        Visible="False"></Login:AptifyWebUserLogin>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
