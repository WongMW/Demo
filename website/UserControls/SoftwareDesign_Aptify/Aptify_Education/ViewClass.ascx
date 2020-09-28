<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/ViewClass.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.ViewClassControl" %>
<%@ Register Src="../Aptify_General/RecordAttachments.ascx" TagName="RecordAttachments"
    TagPrefix="uc3" %>
<%@ Register Src="InstructorValidator.ascx" TagName="InstructorValidator" TagPrefix="uc2" %>
<%@ Register Src="../Aptify_Forums/SingleForum.ascx" TagName="SingleForum" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--<asp:UpdatePanel ID="UppanelGrid" runat="server">
    <ContentTemplate>--%>
<div class="content-container clearfix">
    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
    <script type="text/javascript">
        function _do_open_content(url) {
            playerWindow = window.open(url, '_aptify_e_learning_content', 'toolbar=no,menubar=no,location=no,directories=no,status=no,resizable=yes,scrollbars=no');
        }
    </script>
    <%-- Suraj S Issue 4/30/13, 14452 add update panel because after page index changing if i press the via F5 or CTRL +R. page get refresh--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="tblMain" runat="server">
                <div class="cai-form">
                    <asp:Label runat="server" CssClass="form-title" ID="lblName" />
                    <div class="cai-form-content">

                        <div id="trDescription" runat="server">
                            <div class="field-group">g
                                <asp:Label runat="server" ID="lblDescription" />
                            </div>

                            <div class="field-group">
                                <img id="imgSchedule" runat="server" alt="Schedule" src="" border="0" align="absmiddle" style="display: none;" />
                                <span class="label-title">Schedule</span>
                            </div>

                            <div class="field-group">
                                <span class="label-title-inline">Starts:</span>
                                <asp:Label CssClass="MeetingDates" runat="server" ID="lblStartDate" />
                            </div>

                            <div class="field-group">
                                <span class="label-title-inline">Ends:</span>
                                <asp:Label CssClass="MeetingDates" runat="server" ID="lblEndDate" />
                            </div>

                            <div class="field-group">
                                <div runat="server" id="trStudentStatus" visible="false">
                                    <asp:Label runat="server" ID="lblStudentStatus" />
                                    <asp:Label runat="server" ID="lblRegisterDates" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="trContent" runat="server">
                    <div class="aptify-category-inner-side-nav">
                        <h6>Menu</h6>
                        <ul>
                            <li>
                                <img runat="server" id="imgGenInfoSmall" src="" alt="General Info" border="0" align="absmiddle" />
                                <asp:HyperLink runat="server" ID="lnkGeneral" Text="General" ToolTip="View general information about the class" />
                            </li>

                            <li id="trInstructors" runat="server">
                                <img runat="server" id="imgInstructorSmall" src="" alt="Instructor Info" border="0"
                                    align="absmiddle" />
                                <asp:HyperLink runat="server" ID="lnkInstructorInfo" Text="Instructor Info"></asp:HyperLink>
                            </li>

                            <li>
                                <img runat="server" id="imgSyllabusSmall" src="" alt="Syllabus" border="0" align="absmiddle" />
                                <asp:HyperLink
                                    runat="server" ID="lnkSyllabus" Text="Syllabus" ToolTip="View details about the class" />
                            </li>
                            <li id="trNotes" runat="server">
                                <img runat="server" id="imgNotesSmall" src="" alt="Notes" border="0" align="absmiddle" />
                                <asp:HyperLink
                                    runat="server" ID="lnkNotes" Text="My Notes" ToolTip="View your own notes about the class" />
                            </li>
                            <li id="trForum" runat="server">
                                <img runat="server" id="imgForumSmall" src="" alt="Discussion Forum" border="0" align="absmiddle" />
                                <asp:HyperLink
                                    runat="server" ID="lnkForum" Text="Discussion" ToolTip="Discussion Forum with instructor and other students" />
                            </li>
                            <li id="trDocuments" runat="server">
                                <img runat="server" id="imgDocumentSmall" src="" alt="Documents" border="0" align="absmiddle" />
                                <asp:HyperLink
                                    runat="server" ID="lnkDocuments" Text="Documents" ToolTip="View documents posted by the instructor" />
                            </li>
                            <li id="trStudents" runat="server" visible="false">
                                <img runat="server" id="imgStudentSmall" src="" alt="Students" border="0" align="absmiddle" />
                                <asp:HyperLink runat="server" ID="lnkStudents" Text="Students"
                                    ToolTip="View list of all students registered for this class (Instructors Only)" />
                            </li>
                            <li id="trRegister" runat="server">
                                <img runat="server" id="imgRegisterSmall" src="" alt="Register for Class" border="0"
                                    align="absmiddle" />
                                <asp:HyperLink runat="server" ID="lnkRegister" Text="Register!"
                                    ToolTip="Register for this class now by clicking on this link..." />
                            </li>
                            <li id="trRegisterMeeting" runat="server">
                                <img runat="server" id="imgRegisterSmall2" src="" alt="Register for Class" border="0"
                                    align="absmiddle" />
                                <asp:LinkButton ID="lnkRegisterMeeting" runat="server">Register</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                    <div runat="server" id="tdExtContent" class="EducationFormRightArea cai-form">
                        <asp:Image runat="server" ID="imgTitle" align="absmiddle" AlternateText="Class Information"
                            ImageUrl="" Style="display: none;" />
                        <asp:Label runat="server" ID="lblTitle" CssClass="form-title" />
                        <div class="cai-form-content">
                            <asp:Label runat="server" ID="lblDetails" />
                            <rad:RadGrid ID="grdSyllabus" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending" AllowSorting="false"
                                SortingSettings-SortedAscToolTip="Sorted Ascending">
                                <GroupingSettings CaseSensitive="false" />
                                <PagerStyle CssClass="sd-pager" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="Item" DataField="WebName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"WebURLUrl") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWebDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Type" DataField="Type"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </rad:GridTemplateColumn>
                                        <%-- Suraj S Issue 4/30/13 14452, remove the sorting and filtering and add the "GridBoundColumn" instead of "GridTemplateColumn" --%>
                                        <rad:GridBoundColumn DataField="Duration" DataFormatString="{0:F0} min" HeaderText="Duration"
                                            AllowSorting="false" AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" FilterControlWidth="80%" />
                                        <rad:GridBoundColumn DataField="CourseStatus" HeaderText="Status" AllowSorting="false"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                            <rad:RadGrid ID="grdStudents" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                AllowFilteringByColumn="true">
                                <PagerStyle CssClass="sd-pager" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn DataField="LastName" HeaderText="Last" SortExpression="LastName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="70px">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'
                                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"LastNameUrl") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="FirstName" HeaderText="First" SortExpression="FirstName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="70px">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'
                                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"FirstNameUrl") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </rad:GridTemplateColumn>
                                        <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnDateRegistered" AllowSorting="true"
                                            Visible="True" HeaderText="DateRegistered" DataField="DateRegistered" SortExpression="DateRegistered"
                                            ReadOnly="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                            DataType="System.DateTime" EnableTimeIndependentFiltering="true" ItemStyle-Width="100px"
                                            FilterControlWidth="100px">
                                        </rad:GridDateTimeColumn>
                                        <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnDateCompleted" AllowSorting="true"
                                            Visible="True" HeaderText="DateCompleted" DataField="DateCompleted" SortExpression="DateCompleted"
                                            ReadOnly="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                            DataType="System.DateTime" EnableTimeIndependentFiltering="true" ItemStyle-Width="100px"
                                            FilterControlWidth="100px">
                                        </rad:GridDateTimeColumn>
                                        <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnDateAvailable" AllowSorting="true"
                                            Visible="True" HeaderText="DateAvailable" DataField="DateAvailable" SortExpression="DateAvailable"
                                            ReadOnly="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                            DataType="System.DateTime" EnableTimeIndependentFiltering="true" ItemStyle-Width="100px"
                                            FilterControlWidth="100px">
                                        </rad:GridDateTimeColumn>
                                        <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnDateExpires" AllowSorting="true"
                                            Visible="True" HeaderText="DateExpires" DataField="DateExpires" SortExpression="DateExpires"
                                            ReadOnly="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                            DataType="System.DateTime" EnableTimeIndependentFiltering="true" ItemStyle-Width="110px"
                                            FilterControlWidth="100px">
                                        </rad:GridDateTimeColumn>
                                        <rad:GridTemplateColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="90px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn DataField="Score" HeaderText="Score" SortExpression="ScoreUrl"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                            FilterControlWidth="90px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ScoreUrl") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                            <asp:Panel ID="pnlForum" runat="server">
                                <uc1:SingleForum ID="SingleForum" runat="server" />
                                <asp:Button CssClass="submitBtn" runat="server" ID="btnCreateForum" Text="Create Forum" />
                            </asp:Panel>
                            <asp:Panel ID="pnlDocuments" runat="server">
                                <uc3:RecordAttachments ID="RecordAttachments" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlNotes" Visible="false" runat="server">

                                <div runat="server" id="divStudentNotes">
                                    <asp:Button CssClass="submitBtn" runat="server" ID="btnEditStudentNotes" Text="Edit" />
                                    <asp:Button CssClass="submitBtn" runat="server" Visible="false" ID="btnSaveStudentNotes"
                                        Text="Save" />
                                    <asp:Button CssClass="submitBtn" runat="server" Visible="false" ID="btnCancelStudentNotes"
                                        Text="Cancel" />
                                    <asp:Label runat="server" ID="lblStudentNotesMessage" Visible="false"></asp:Label>
                                    <asp:TextBox runat="server" Visible="false" ID="txtStudentNotes" TextMode="multiLine"></asp:TextBox>

                                </div>

                                <asp:Literal runat="server" ID="lblStudentNotes"></asp:Literal>
                            </asp:Panel>
                            <div runat="server" id="tblInstructor">

                                <asp:Label runat="server" ID="lblInstructor" />

                                Location
                                
                                    <asp:Label runat="server" ID="lblInstructorLocation" />

                                Email
                                
                                    <a href="" id="lnkInstructorEmail" runat="server">
                                        <asp:Label runat="server" ID="lblInstructorEmail" /></a>


                                <div runat="server" id="trInstructorNotes">

                                    <img runat="server" id="imgInstrutorNotes" src="" alt="Notes Icon" align="absmiddle"
                                        border="0"  style="display:none;"/>
                                    <b>Instructor Notes</b>
                                    The course instructor has recorded the following notes for the students of this
                                    class.<hr noshade="noshade" />
                                    <div runat="server" visible="false" id="divEditInstructorNotes">
                                        <asp:Button CssClass="submitBtn" runat="server" ID="btnEditInstructorNotes" Text="Edit" />
                                        <asp:Button CssClass="submitBtn" runat="server" Visible="false" ID="btnSaveInstructorNotes"
                                            Text="Save" />
                                        <asp:Button CssClass="submitBtn" runat="server" Visible="false" ID="btnCancelInstructorNotes"
                                            Text="Cancel" />
                                        <asp:Label runat="server" ID="lblInstructorNotesMessage" Visible="false"></asp:Label>
                                        <asp:TextBox runat="server" Visible="false" ID="txtInstructorNotes" TextMode="multiLine"
                                            Width="400px" Height="200px"></asp:TextBox>

                                    </div>

                                    <asp:Literal runat="server" ID="lblInstructorNotes"></asp:Literal>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc3:User ID="User1" runat="server" />
    <uc2:InstructorValidator ID="InstructorValidator1" runat="server" />
    <uc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
</div>
