<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ViewClass__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.ViewClassControl__c" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="../Aptify_General/RecordAttachments.ascx" TagName="RecordAttachments"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/Aptify_Education/InstructorValidator.ascx" TagName="InstructorValidator"
    TagPrefix="uc2" %>
<%@ Register Src="../Aptify_Forums/SingleForum.ascx" TagName="SingleForum" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc5"
    TagName="RecordAttachments__c" %>
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
            <table id="tblMain" runat="server" class="data-form">
                <tr>
                    <td colspan="2" class="BottomBorder">
                        <asp:Label runat="server" CssClass="MeetingName" ID="lblName" />
                    </td>
                </tr>
                <tr id="trDescription" runat="server">
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblDescription" />
                                </td>
                                <td rowspan="2">
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <img id="imgSchedule" runat="server" alt="Schedule" src="" border="0" align="absmiddle" />
                                                <b>Schedule</b><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i>Starts</i>
                                            </td>
                                            <td>
                                                <asp:Label CssClass="MeetingDates" runat="server" ID="lblStartDate" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i>Ends</i>
                                            </td>
                                            <td>
                                                <asp:Label CssClass="MeetingDates" runat="server" ID="lblEndDate" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="trStudentStatus" visible="false">
                                <td>
                                    <asp:Label runat="server" ID="lblStudentStatus" Font-Bold="true" />
                                    <br />
                                    <asp:Label runat="server" ID="lblRegisterDates" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trContent" runat="server">
                    <td class="EducationFormActionArea">
                        <table>
                            <tr>
                                <td>
                                    <img runat="server" id="imgGenInfoSmall" src="" alt="General Info" border="0" align="absmiddle" />
                                    <asp:HyperLink runat="server" ID="lnkGeneral" Text="General" Font-Size="10pt" ToolTip="View general information about the class" />
                                </td>
                            </tr>
                            <tr id="trInstructors" runat="server">
                                <td>
                                    <img runat="server" id="imgInstructorSmall" src="" alt="Instructor Info" border="0"
                                        align="absmiddle" />&nbsp;<asp:HyperLink runat="server" ID="lnkInstructorInfo" Text="Instructor Info"
                                            Font-Size="10pt" ToolTip="View information about the instructor of this class" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img runat="server" id="imgSyllabusSmall" src="" alt="Syllabus" border="0" align="absmiddle" />&nbsp;<asp:HyperLink
                                        runat="server" ID="lnkSyllabus" Text="Syllabus" Font-Size="10pt" ToolTip="View details about the class" />
                                </td>
                            </tr>
                            <tr id="trNotes" runat="server">
                                <td>
                                    <img runat="server" id="imgNotesSmall" src="" alt="Notes" border="0" align="absmiddle" />&nbsp;<asp:HyperLink
                                        runat="server" ID="lnkNotes" Text="My Notes" Font-Size="10pt" ToolTip="View your own notes about the class" />
                                </td>
                            </tr>
                            <tr id="trForum" runat="server">
                                <td>
                                    <img runat="server" id="imgForumSmall" src="" alt="Discussion Forum" border="0" align="absmiddle" />&nbsp;<asp:HyperLink
                                        runat="server" ID="lnkForum" Text="Discussion" Font-Size="10pt" ToolTip="Discussion Forum with instructor and other students" />
                                </td>
                            </tr>
                            <tr id="trDocuments" runat="server">
                                <td>
                                    <img runat="server" id="imgDocumentSmall" src="" alt="Documents" border="0" align="absmiddle" />&nbsp;<asp:HyperLink
                                        runat="server" ID="lnkDocuments" Text="Documents" Font-Size="10pt" ToolTip="View documents posted by the instructor" />
                                </td>
                            </tr>
                            <tr id="trStudents" runat="server" visible="false">
                                <td>
                                    <img runat="server" id="imgStudentSmall" src="" alt="Students" border="0" align="absmiddle" />&nbsp;<asp:HyperLink
                                        runat="server" ID="lnkStudents" Text="Students" Font-Size="10pt" ToolTip="View list of all students registered for this class (Instructors Only)" />
                                </td>
                            </tr>
                            <tr id="trRegister" runat="server">
                                <td>
                                    <img runat="server" id="imgRegisterSmall" src="" alt="Register for Class" border="0"
                                        align="absmiddle" />&nbsp;<asp:HyperLink runat="server" ID="lnkRegister" Text="Register!"
                                            Font-Size="10pt" ToolTip="Register for this class now by clicking on this link..." />
                                </td>
                            </tr>
                            <tr id="trRegisterMeeting" runat="server">
                                <td>
                                    <img runat="server" id="imgRegisterSmall2" src="" alt="Register for Class" border="0"
                                        align="absmiddle" />&nbsp;
                                    <%-- <asp:LinkButton ID="lnkRegisterMeeting" runat="server">Register!</asp:LinkButton>--%>
                                    <asp:LinkButton ID="lnkRegisterMeeting" runat="server">Register</asp:LinkButton>
                                </td>
                            </tr>
                            <tr id="trPreDownload" runat="server">
                                <td>
                                    <img runat="server" id="imgPreDocumentSmall" src="" alt="Download" border="0" align="middle" />&nbsp;<asp:HyperLink
                                        runat="server" ID="lnkPreDownload" Text="Pre-Download" Font-Size="10pt" ToolTip="Pre Purchase Downloads" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td runat="server" id="tdExtContent" class="EducationFormRightArea">
                        <asp:Image runat="server" ID="imgTitle" align="absmiddle" AlternateText="Class Information"
                            ImageUrl="" />
                        <asp:Label runat="server" ID="lblTitle" Font-Bold="True" Font-Italic="True" Font-Size="12pt" /><br />
                        <asp:Label runat="server" ID="lblDetails" Font-Size="11pt" />&nbsp;
                        <%--Navin Prasad Issue 11032--%>
                        <%--Nalini Issue 12436 date:01/12/2011--%>
                        <%--Neha Changes for Issue 14452--%>
                        <%-- Suraj S Issue 14452, 5/6/13, remove the sorting  --%>
                        <rad:RadGrid ID="grdSyllabus" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            AllowSorting="false" SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Upload">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandName="Download"
                                                CommandArgument='<%# Eval("RecordID")&";"&Eval("EntityID") %>' ForeColor='<%# IIf((Eval("IsDocAvaible"))=1, System.Drawing.Color.FromName("Black"), System.Drawing.Color.FromName("Gray")) %>'
                                                Enabled='<%# IIf((Eval("IsDocAvaible"))=1,true,false) %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Item" DataField="WebName" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"WebURLUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWebDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Type" DataField="Type" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" VerticalAlign="Top" />
                                    </rad:GridTemplateColumn>
                                    <%-- Suraj S Issue 4/30/13 14452, remove the sorting and filtering and add the "GridBoundColumn" instead of "GridTemplateColumn" --%>
                                    <rad:GridBoundColumn DataField="Duration" DataFormatString="{0:F0} min" HeaderText="Duration"
                                        AllowSorting="false" AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" FilterControlWidth="80%" />
                                    <rad:GridBoundColumn DataField="CourseStatus" HeaderText="Status" AllowSorting="false"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%" />
                                    <rad:GridTemplateColumn HeaderText="Upload">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkUpload" runat="server" Text="Upload" CommandName="Upload"
                                                CommandArgument='<%# Eval("RecordID")&";"&Eval("EntityID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <%--<rad:GridEditCommandColumn EditText="Upload" UniqueName="Upload" />--%>
                                </Columns>
                                <%--<EditFormSettings EditFormType="Template" ColumnNumber="2">
                                    <FormTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <b>Documents</b><br />
                                                    <asp:Panel ID="Panel2" runat="Server" Style="border: 1px Solid #000000;">
                                                        <table runat="server" id="Table1" class="data-form" width="100%">
                                                            <tr>
                                                                <td class="RightColumn">
                                                                    <uc5:RecordAttachments__c ID="ucRecordAttachment" runat="server" AllowAdd="True"
                                                                        AllowView="false" RecordID='<%# Bind("RecordID") %>' EntityID='<%# Bind("EntityID") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </FormTemplate>
                                </EditFormSettings>--%>
                            </MasterTableView>
                        </rad:RadGrid>
                        <rad:RadGrid ID="grdStudents" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true">
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn DataField="LastName" HeaderText="Last" SortExpression="LastName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="70px">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"LastNameUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn DataField="FirstName" HeaderText="First" SortExpression="FirstName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="70px">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"FirstNameUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
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
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn DataField="Score" HeaderText="Score" SortExpression="ScoreUrl"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        FilterControlWidth="90px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ScoreUrl") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                        <asp:Panel ID="pnlForum" runat="server">
                            <uc1:SingleForum ID="SingleForum" runat="server" />
                            <br />
                            <asp:Button CssClass="submitBtn" runat="server" ID="btnCreateForum" Text="Create Forum" />
                        </asp:Panel>
                        <asp:Panel ID="pnlDocuments" runat="server">
                            <uc3:RecordAttachments ID="RecordAttachments" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="pnlDownloads" runat="server">
                            <div>
                                <table>
                                    <tr id="trRecordAttachment" runat="server" visible="false">
                                        <td>
                                            <b>Documents</b><br />
                                            <asp:Panel ID="Panel1" runat="Server" Style="border: 1px Solid #000000;">
                                                <table runat="server" id="Table2" class="data-form" width="100%">
                                                    <tr>
                                                        <td class="RightColumn">
                                                            <uc5:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                                                                AllowAdd="True" AllowDelete="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlNotes" Visible="false" runat="server">
                            <br />
                            <div runat="server" id="divStudentNotes">
                                <asp:Button CssClass="submitBtn" runat="server" ID="btnEditStudentNotes" Text="Edit" />
                                <asp:Button CssClass="submitBtn" runat="server" Visible="false" ID="btnSaveStudentNotes"
                                    Text="Save" />
                                <asp:Button CssClass="submitBtn" runat="server" Visible="false" ID="btnCancelStudentNotes"
                                    Text="Cancel" />
                                <asp:Label runat="server" ID="lblStudentNotesMessage" Visible="false"></asp:Label><br />
                                <asp:TextBox runat="server" Visible="false" ID="txtStudentNotes" TextMode="multiLine"></asp:TextBox>
                                <br />
                            </div>
                            <pre>
<asp:Literal runat="server" ID="lblStudentNotes"></asp:Literal></pre>
                        </asp:Panel>
                        <table runat="server" id="tblInstructor">
                            <tr>
                                <td colspan="2">
                                    <asp:Label runat="server" ID="lblInstructor" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Location
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblInstructorLocation" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Email
                                </td>
                                <td>
                                    <a href="" id="lnkInstructorEmail" runat="server">
                                        <asp:Label runat="server" ID="lblInstructorEmail" /></a><br />
                                    <br />
                                </td>
                            </tr>
                            <tr runat="server" id="trInstructorNotes">
                                <td colspan="2">
                                    <img runat="server" id="imgInstrutorNotes" src="" alt="Notes Icon" align="absmiddle"
                                        border="0" />
                                    <b>Instructor Notes</b><br />
                                    The course instructor has recorded the following notes for the students of this
                                    class.<hr noshade="noshade" />
                                    <div runat="server" visible="false" id="divEditInstructorNotes">
                                        <asp:Button CssClass="submitBtn" runat="server" ID="btnEditInstructorNotes" Text="Edit" />
                                        <asp:Button CssClass="submitBtn" runat="server" Visible="false" ID="btnSaveInstructorNotes"
                                            Text="Save" />
                                        <asp:Button CssClass="submitBtn" runat="server" Visible="false" ID="btnCancelInstructorNotes"
                                            Text="Cancel" />
                                        <asp:Label runat="server" ID="lblInstructorNotesMessage" Visible="false"></asp:Label><br />
                                        <asp:TextBox runat="server" Visible="false" ID="txtInstructorNotes" TextMode="multiLine"
                                            Width="400px" Height="200px"></asp:TextBox>
                                        <br />
                                    </div>
                                    <pre>
                                        <asp:Literal runat="server" ID="lblInstructorNotes"></asp:Literal></pre>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="grdSyllabus" />
        </Triggers>
    </asp:UpdatePanel>
    <telerik:RadWindow ID="radUploadDocuments" runat="server" Width="400px" Height="250px"
        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Upload Documents" Behavior="None">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td width="5%">
                    </td>
                    <td width="90%">
                        <b>Documents</b><br />
                        <asp:Panel ID="pnlUploadDocuments" runat="Server" Style="border: 1px Solid #000000;">
                            <table runat="server" class="data-form" width="100%">
                                <tr>
                                    <td class="RightColumn">
                                        <uc5:RecordAttachments__c ID="ucUpload" runat="server" AllowView="False" AllowAdd="True"
                                            AllowDelete="False" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
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
                    <td align="right">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submitBtn" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="radDownloadDocuments" runat="server" Width="500px" Height="300px" Modal="True"
        BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Download Documents" Behavior="None">
        <ContentTemplate>
            <div>
            <table width="100%">
             <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td width="5%">
                    </td>
                    <td width="90%">
                        <b>Documents</b><br />
                        <asp:Panel ID="pnlDownloadDocuments" runat="Server" Style="border: 1px Solid #000000;">
                            <table class="data-form" width="100%">
                                <tr>
                                    <td class="RightColumn">
                                        <uc5:RecordAttachments__c ID="ucDownload" runat="server" AllowView="True" AllowAdd="False"
                                            AllowDelete="False" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td width="5%">
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="submitBtn" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>    
    <cc3:User ID="User1" runat="server" />
    <uc2:InstructorValidator ID="InstructorValidator1" runat="server" />
    <uc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
