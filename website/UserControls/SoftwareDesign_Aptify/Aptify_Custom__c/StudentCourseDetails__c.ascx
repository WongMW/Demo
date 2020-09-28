<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/StudentCourseDetails__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_StudentCourseDetails__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="EBizUser" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="ucRecordAttachment"
    TagName="RecordAttachments__c" %>

<div class="cai-form">
    <span class="form-title">Course Details</span>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfClassRegId" runat="server" Value="0" />
            <asp:HiddenField ID="hfIsFAE" runat="server" Value="0" />
            <div>
                <div class="form-section-half-border">
                    <div class="field-group">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <asp:Label ID="lblFirstLast" runat="server" Text="Student Name:" CssClass="label-title-inline"></asp:Label>
                        <asp:Label ID="lblFirstLastValue" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <asp:Label ID="lblStudentNumber" runat="server" Text="Student Number:" CssClass="label-title-inline"></asp:Label>
                        <asp:Label ID="lblStudentNumberValue" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <asp:Label ID="lblAcademicCycle" runat="server" Text="Academic Cycle:" CssClass="label-title-inline"></asp:Label>
                        <asp:Label ID="lblAcademicCycleValue" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <asp:Label ID="lblCourseName" runat="server" Text="Subject :" CssClass="label-title-inline"></asp:Label>
                        <asp:Label ID="lblCourseNameValue" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <span class="label-title-inline">Start Date:</span>
                        <asp:Label ID="lblStartDateValue" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <span class="label-title-inline">End Date:</span>
                        <asp:Label ID="lblEndDateValue" runat="server" Text=""></asp:Label></b>
                    </div>
                </div>
                <div class="form-section-half-border">
                    <div class="field-group">
                        <asp:Label ID="lblTimeTable" runat="server" Text="Time Table :" CssClass="label-title-inline"></asp:Label>
                        <asp:Label ID="lblTimeTableValue" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <asp:Label ID="lblSubGroup1" runat="server" Text="Sub Group 1 :" CssClass="label-title-inline"></asp:Label>
                        <asp:Label ID="lblSubGroup1Value" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <asp:Label ID="lblSubGroup2" runat="server" Text="Sub Group 2 :" CssClass="label-title-inline"></asp:Label>
                        <asp:Label ID="lblSubGroup2Value" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <asp:Label ID="lblSubGroup3" runat="server" Text="Sub Group 3 :" CssClass="label-title-inline"></asp:Label>
                        <asp:Label ID="lblSubGroup3Value" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <asp:Label ID="lblSubGroup4" runat="server" Text="Sub Group 4 :" CssClass="label-title-inline"></asp:Label>
                        <asp:Label ID="lblSubGroup4Value" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <div class="cai-form-content">
                <div class="cai-table mobile-table">
                    <asp:HiddenField ID="hfPartStatusID" runat="server" Value="0" />
                    <telerik:RadGrid ID="gvCourseDetails" runat="server" AllowPaging="True" AllowSorting="True"
                        OnNeedDataSource="gvCourseDetails_NeedDataSource" AllowFilteringByColumn="False"
                        CellSpacing="0" GridLines="None" AutoGenerateColumns="false">
                        <PagerStyle CssClass="sd-pager" />
                        <MasterTableView>
                            <Columns>
                                <telerik:GridTemplateColumn SortExpression="Lesson" HeaderText="Lesson" HeaderButtonType="TextButton" DataField="Lesson">
                                    <ItemTemplate>
                                        <span class="mobile-label">Lesson:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Lesson") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="Type" HeaderText="Type" HeaderButtonType="TextButton"
                                    DataField="Type">
                                    <ItemTemplate>
                                        <span class="mobile-label">Type:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="StartDate" HeaderText="Start Date" HeaderButtonType="TextButton"
                                    DataField="StartDate">
                                    <ItemTemplate>
                                        <span class="mobile-label">Start Date:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("StartDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="End Date" HeaderText="End Date" HeaderButtonType="TextButton"
                                    DataField="EndDate">
                                    <ItemTemplate>
                                        <span class="mobile-label">Type:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("EndDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="Duration" HeaderText="Duration" HeaderButtonType="TextButton"
                                    DataField="Duration">
                                    <ItemTemplate>
                                        <span class="mobile-label">Duration:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Duration")%>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="Status" HeaderText="Status" HeaderButtonType="TextButton"
                                    DataField="Status">
                                    <ItemTemplate>
                                        <span class="mobile-label">Status:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="Instructor" HeaderText="Instructor" HeaderButtonType="TextButton"
                                    DataField="Instructor">
                                    <ItemTemplate>
                                        <span class="mobile-label">Instructor:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Instructor")%>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="Venue" HeaderText="Venue" HeaderButtonType="TextButton"
                                    DataField="Venue">
                                    <ItemTemplate>
                                        <span class="mobile-label">Venue:</span>
                                        <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Venue")%>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="My Notes">
                                    <ItemTemplate>
                                        <span class="mobile-label">My Notes:</span>
                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="Click here" CommandName="AddNotes"
                                            CommandArgument='<%# Eval("ClassRegPartStatusID")%>' CssClass="cai-table-data"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Course Materials">
                                    <ItemTemplate>
                                        <span class="mobile-label">Download:</span>
                                        <asp:LinkButton CssClass="cai-table-data" ID="lnkDownload" runat="server" Text="Download" CommandName="Download"
                                            CommandArgument='<%# Eval("CoursePartID")%>'
                                            Visible='<%# IIf(Eval("IsAssignment")=true,false,true) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Assignment">
                                    <ItemTemplate>
                                        <span class="mobile-label">Assignment:</span>
                                        <asp:LinkButton CssClass="cai-table-data" ID="lnkAssignment" runat="server" Text="Click here" CommandName="Assignment"
                                            CommandArgument='<%# Eval("EndDate") & ";" & Eval("ClassRegPartStatusID") & ";" & Eval("CoursePartID")%>'
                                            Visible='<%# IIf(Eval("IsAssignment")=true,true,false) %>' Enabled='<%# IIf((Eval("IsAvailable"))=1,true,false) %>'
                                            ForeColor='<%# IIf((Eval("IsAvailable"))=1, System.Drawing.Color.FromName("Black"), System.Drawing.Color.FromName("Gray")) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn SortExpression="AssignmentScore" HeaderText="Assignment Scores"
                                    HeaderButtonType="TextButton" DataField="AssignmentScore">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <telerik:RadWindow ID="radDownloadDocuments" runat="server" Width="500px" Height="350px"
                        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="Download Documents" Behavior="None" Skin="Default">
                        <ContentTemplate>
                            <div>
                                <span class="label-title">Documents</span>
                                <asp:Panel ID="pnlDownloadDocuments" runat="Server">
                                    <ucRecordAttachment:RecordAttachments__c ID="ucDownload" runat="server" AllowView="True"
                                        AllowAdd="false" AllowDelete="false" ViewDescription="false" />
                                </asp:Panel>

                                <div class="actions">
                                    <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="submitBtn" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>

                    <telerik:RadWindow ID="radAddNotes" runat="server" Width="450px" Height="350px" Modal="true"
                        BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="My Notes" Behavior="None" Skin="Default">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="lblAddNoteMessage" runat="server"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblNote" CssClass="label-title" runat="server">Notes:</asp:Label>
                                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="100%" Height="120px"></asp:TextBox>
                            </div>

                            <div class="actions">
                                <asp:Button ID="btnAddNotes" Text="Add" runat="server" CssClass="submitBtn" />
                                <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="submitBtn" />
                                <asp:Button ID="btnCloseAddNotes" Text="Cancel" runat="server" CssClass="submitBtn" />
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>

                    <telerik:RadWindow ID="radAssignments" runat="server" Width="620px" Height="580px"
                        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="True" Behaviors="None" ForeColor="#BDA797"
                        Title="Download Documents" Behavior="None" Skin="Default">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="lblAssignmentMessage" runat="server"></asp:Label>
                            </div>
                            <div class="info-data">
                                <div class="row-div clearfix">
                                    <div class="label-align-left-div">
                                    </div>
                                    <div class="field-div1 w99">
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <div class="label-align-left-div">
                                                    <asp:Label ID="lblbAssignmentDueDate" runat="server" Text="Assignment Due Date:"
                                                        Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblbAssignmentDueDateValue" runat="server" Text="[Date]" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <div class="label-align-left-div">
                                                    <asp:Label ID="lblDaysRemaining" runat="server" Text="Day Remaining To Submission:"
                                                        Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblDaysRemainingValue" runat="server" Text="[Days Cout]" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-align-center-div">
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-align-left-div">
                                                    <asp:Label ID="lbldlAssignments" runat="server" Text="Download Assignments" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="label-align-left-div">
                                                    <ucRecordAttachment:RecordAttachments__c ID="ucAssignmentDownload" runat="server"
                                                        AllowView="True" AllowAdd="false" AllowDelete="False" ViewDescription="false" />
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-align-left-div">
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-align-left-div">
                                                    <asp:Label ID="lblUploadAssignments" runat="server" Text="Upload Assignments" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="label-align-left-div">
                                                    <ucRecordAttachment:RecordAttachments__c ID="ucAssignmentUpload" runat="server" AllowView="True"
                                                        AllowAdd="True" AllowDelete="True" />
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-div">
                                                </div>
                                                <div class="field-div1" align="right">
                                                    <asp:Button ID="btnCloseAssignment" Text="Cancel" runat="server" CssClass="submit-Btn" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-align-left-div">
                                    </div>
                                    <asp:Label ID="lblAssgnmtNote" runat="server" Text="Note :" Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblAssgnmtValue" runat="server" Text="[Note]" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div>
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>
                </div>

                <div class="info-data">
                    <div class="actions">
                        <asp:Button ID="btnBack" runat="server" CssClass="submitBtn" Text="Back" />
                    </div>
                </div>
            </div>
            <EBizUser:User ID="LoggedInUser" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
