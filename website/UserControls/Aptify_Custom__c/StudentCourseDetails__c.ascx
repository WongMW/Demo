<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StudentCourseDetails__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_StudentCourseDetails__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="EBizUser" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="ucRecordAttachment"
    TagName="RecordAttachments__c" %>
<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/DivTag.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/Div_Data_form.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hfClassRegId" runat="server" Value="0" />
        <asp:HiddenField ID="hfIsFAE" runat="server" Value="0" />
        <div class="content-container clearfix">
            <div>
                <div>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
                <div align="center" style="background-color: GrayText">
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-div w20">
                            </div>
                            <div class="label-div w10">
                                <asp:Label ID="lblFirstLast" runat="server" Text="Student Name:" Font-Bold="true" ForeColor="White"></asp:Label>
                            </div>
                            <div class="label-div-left-align w10">
                                <asp:Label ID="lblFirstLastValue" runat="server" Text="" ForeColor="White"></asp:Label>
                            </div>
                            <div class="label-div w15">
                                <asp:Label ID="lblStudentNumber" runat="server" Text="Student Number:" Font-Bold="true" ForeColor="White"></asp:Label>
                            </div>
                            <div class="label-div-left-align w10">
                                <asp:Label ID="lblStudentNumberValue" runat="server" Text="" ForeColor="White"></asp:Label>
                            </div>
                            <div class="label-div w10">
                                <asp:Label ID="lblAcademicCycle" runat="server" Text="Academic Cycle:" Font-Bold="true" ForeColor="White"></asp:Label>
                            </div>
                            <div class="label-div-left-align w10">
                                <asp:Label ID="lblAcademicCycleValue" runat="server" Text="" ForeColor="White"></asp:Label>
                            </div>
                            <div class="label-div w20">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="info-data">
                    <div class="row-div clearfix">
                        <div class="label-div-left-align w50">
                            <div class="info-data">
                                <div class="row-div clearfix">
                                    <div class="label-div w20">
                                        <asp:Label ID="lblCourseName" runat="server" Text="Subject :" Font-Bold="true"></asp:Label></b>
                                    </div>
                                    <div class="field-div w80">
                                        <asp:Label ID="lblCourseNameValue" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-div w20">
                                        <asp:Label ID="lblTimeTable" runat="server" Text="Time Table :" Font-Bold="true"></asp:Label></b>
                                    </div>
                                    <div class="field-div w80">
                                        <asp:Label ID="lblTimeTableValue" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-div w20">
                                        <asp:Label ID="lblSubGroup1" runat="server" Text="Sub Group 1 :" Font-Bold="true"></asp:Label></b>
                                    </div>
                                    <div class="field-div w80">
                                        <asp:Label ID="lblSubGroup1Value" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-div w20">
                                        <asp:Label ID="lblSubGroup2" runat="server" Text="Sub Group 2 :" Font-Bold="true"></asp:Label></b>
                                    </div>
                                    <div class="field-div w80">
                                        <asp:Label ID="lblSubGroup2Value" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-div w20">
                                        <asp:Label ID="lblSubGroup3" runat="server" Text="Sub Group 3 :" Font-Bold="true"></asp:Label></b>
                                    </div>
                                    <div class="field-div w80">
                                        <asp:Label ID="lblSubGroup3Value" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-div w20">
                                        <asp:Label ID="lblSubGroup4" runat="server" Text="Sub Group 4 :" Font-Bold="true"></asp:Label></b>
                                    </div>
                                    <div class="field-div w80">
                                        <asp:Label ID="lblSubGroup4Value" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="field-div1 w40">
                            <div class="info-data">
                                <div class="row-div clearfix">
                                    <div class="label-div w30">
                                    </div>
                                    <div class="label-div w20">
                                        Start Date:
                                    </div>
                                    <div class="field-div w50">
                                        <asp:Label ID="lblStartDateValue" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-div w30">
                                    </div>
                                    <div class="label-div w20">
                                        End Date:
                                    </div>
                                    <div class="field-div w50">
                                        <asp:Label ID="lblEndDateValue" runat="server" Text=""></asp:Label></b>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <asp:HiddenField ID="hfPartStatusID" runat="server" Value="0" />
                    <telerik:RadGrid ID="gvCourseDetails" runat="server" AllowPaging="True" AllowSorting="True"
                        OnNeedDataSource="gvCourseDetails_NeedDataSource" AllowFilteringByColumn="False"
                        CellSpacing="0" GridLines="None" AutoGenerateColumns="false" Width="99%">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn SortExpression="Lesson" HeaderText="Lesson" HeaderButtonType="TextButton"
                                    DataField="Lesson" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="Type" HeaderText="Type" HeaderButtonType="TextButton"
                                    DataField="Type" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="StartDate" HeaderText="Start Date" HeaderButtonType="TextButton"
                                    DataField="StartDate"  HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="EndDate" HeaderText="End Date" HeaderButtonType="TextButton"
                                    DataField="EndDate"  HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="Duration" HeaderText="Duration" HeaderButtonType="TextButton"
                                    DataField="Duration"  HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="Status" HeaderText="Status" HeaderButtonType="TextButton"
                                    DataField="Status" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="Instructor" HeaderText="Instructor" HeaderButtonType="TextButton"
                                    DataField="Instructor" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="Venue" HeaderText="Venue" HeaderButtonType="TextButton"
                                    DataField="Venue" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="My Notes"  HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="Click here" CommandName="AddNotes"
                                            CommandArgument='<%# Eval("ClassRegPartStatusID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Course Materials"  HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandName="Download"
                                            CommandArgument='<%# Eval("CoursePartID")%>'
                                            Visible='<%# IIf(Eval("IsAssignment")=true,false,true) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Assignment"  HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAssignment" runat="server" Text="Click here" CommandName="Assignment"
                                            CommandArgument='<%# Eval("EndDate") & ";" & Eval("ClassRegPartStatusID") & ";" & Eval("CoursePartID") & ";" & Eval("Status") %>'
                                            Visible='<%# IIf(Eval("IsAssignment")=true,true,false) %>' Enabled='<%# IIf((Eval("IsAvailable"))=1,true,false) %>'
                                            ForeColor='<%# IIf((Eval("IsAvailable"))=1, System.Drawing.Color.FromName("Black"), System.Drawing.Color.FromName("Gray")) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn SortExpression="AssignmentScore" HeaderText="Assignment Scores"
                                    HeaderButtonType="TextButton" DataField="AssignmentScore"  HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <telerik:RadWindow ID="radDownloadDocuments" runat="server" Width="500px" Height="300px"
                        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
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
                                                            <ucRecordAttachment:RecordAttachments__c ID="ucDownload" runat="server" AllowView="True"
                                                                AllowAdd="false" AllowDelete="false" ViewDescription="false" />
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
                    <telerik:RadWindow ID="radAddNotes" runat="server" Width="400px" Height="250px" Modal="true"
                        BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="My Notes" Behavior="None">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="lblAddNoteMessage" runat="server"></asp:Label>
                            </div>
                            <div class="info-data">
                                <br />
                                <div class="row-div clearfix">
                                    <div class="label-div w20" align="left">
                                        <asp:Label ID="lblNote" runat="server">Notes:</asp:Label>
                                    </div>
                                    <div class="field-div1 w60" align="left">
                                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="110%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="info-data">
                                <div class="row-div clearfix">
                                    <div class="label-div w60" align="left">
                                        <asp:Button ID="btnAddNotes" Text="Add" runat="server" CssClass="submit-Btn" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="submit-Btn" />
                                        <asp:Button ID="btnCloseAddNotes" Text="Cancel" runat="server" CssClass="submit-Btn" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>
                    <telerik:RadWindow ID="radAssignments" runat="server" Width="800px" Height="580px"
                        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="True" Behaviors="None" ForeColor="#BDA797"
                        Title="Download Documents" Behavior="None">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="lblAssignmentMessage" runat="server"></asp:Label>
                            </div>
                            <div class="info-data">
                                <div class="row-div clearfix">
                                    <div class="label-align-left-div w20">
                                    </div>
                                    <div class="field-div1 w99">
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <div class="label-align-left-div w50">
                                                    <asp:Label ID="lblbAssignmentDueDate" runat="server" Text="Assignment Due Date:"
                                                        Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblbAssignmentDueDateValue" runat="server" Text="[Date]" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <div class="label-align-left-div w50">
                                                    <asp:Label ID="lblDaysRemaining" runat="server" Text="Days Remaining To Submission:"
                                                        Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblDaysRemainingValue" runat="server" Text="[Days Cout]" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-align-center-div w50">
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-align-left-div w50">
                                                    <asp:Label ID="lbldlAssignments" runat="server" Text="Download Assignment Question" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="label-align-left-div w500">
                                                    <ucRecordAttachment:RecordAttachments__c ID="ucAssignmentDownload" runat="server"
                                                        AllowView="True" AllowAdd="false" AllowDelete="False" ViewDescription="false" />
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-align-left-div w30">
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-align-left-div w50">
                                                    <asp:Label ID="lblUploadAssignments" runat="server" Text=" Upload Assignment Answer" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="label-align-left-div w500">
                                                    <ucRecordAttachment:RecordAttachments__c ID="ucAssignmentUpload" runat="server" AllowView="True"
                                                        AllowAdd="True" AllowDelete="True" />
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-div w10">
                                                </div>
                                                <div class="field-div1 w100" align="right">
                                                    <asp:Button ID="btnCloseAssignment" Text="Back" runat="server" CssClass="submit-Btn" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-align-left-div w20">
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
                    <div class="row-div clearfix">
                        <div class="label-div w20">
                            <br />
                        </div>
                    </div>
                </div>
                <div class="info-data">
                    <div class="row-div clearfix">
                        <div class="label-div w90">
                            <asp:Button ID="btnBack" runat="server" CssClass="submit-Btn" Text="Back" Width="8%" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <EBizUser:User ID="LoggedInUser" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
