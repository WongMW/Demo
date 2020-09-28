<%@ Control Language="VB" AutoEventWireup="false"   Inherits="Aptify.Framework.Web.eBusiness.Education.ClassAttendance__c" CodeFile="~/UserControls/Aptify_Custom__c/ClassAttendance__c.ascx.vb" %>
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
        <div id="divContent" runat="server">
            <asp:Panel ID="pnlData" runat="server">
                <div class="info-data">
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    <br />
                    <div class="row-div clearfix">
                        <div>
                            <asp:Label ID="lblClassDate" runat="server" Width="15%"><span class="RequiredField">*</span>Class Date:</asp:Label>

                            <rad:RadDatePicker ID="radClassDate" runat="server" AutoPostBack="true" Width="18%">
                            </rad:RadDatePicker>


                            <asp:Label ID="lblCourse" runat="server" Style="margin-left: 35px" Width="10%"><span class="RequiredField">*</span>Course:</asp:Label>

                            <asp:DropDownList ID="ddlCourseList" runat="server" AutoPostBack="true" Width="25%" Style="margin-left: 5px" >
                            </asp:DropDownList>
                        </div>

                        <div>
                            <asp:Label ID="lblType" runat="server" Width="15%"><span class="RequiredField">*</span>Type:</asp:Label>

                            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" Width="15%">
                                <asp:ListItem Text="Select Type" Value="Select Type"></asp:ListItem>
                                <asp:ListItem Text="Classroom" Value="Classroom"></asp:ListItem>
                                <asp:ListItem Text="Internet" Value="Internet"></asp:ListItem>
                                <asp:ListItem Text="Exam" Value="Exam"></asp:ListItem>
                                <asp:ListItem Text="Interim Assessment" Value="Interim Assessment"></asp:ListItem>
                                <asp:ListItem Text="Independent Study" Value="Independent Study"></asp:ListItem>
                                <asp:ListItem Text="Mock Exam" Value="Mock Exam"></asp:ListItem>
                                <asp:ListItem Text="Revision" Value="Revision"></asp:ListItem>
                                <asp:ListItem Text="Repeat Revision" Value="Repeat Revision"></asp:ListItem>
                                <asp:ListItem Text="Distance" Value="Distance"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlType" runat="server" ControlToValidate="ddlType"
                                ValidationGroup="CompanyControl" ErrorMessage="Type Required" Display="Dynamic"
                                CssClass="required-label"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblStudGroup" runat="server" Style="margin-left: 70px" Width="10%"><span class="RequiredField">*</span>Student Group:</asp:Label>
                            <asp:DropDownList ID="ddlStudGroup" runat="server" AutoPostBack="true" Width="25%"
                                Style="margin-left: 5px">
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:Label ID="lblLesson" runat="server" Width="15%"><span class="RequiredField">*</span>Lesson:</asp:Label>
                            <asp:DropDownList ID="ddlLesson" runat="server" AutoPostBack="true" Width="57%">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row-div clearfix" align="center">
                        <asp:Button ID="btnDisplay" runat="server" Text="Display" Width="15%" Height="30px" />
                    </div>
                </div>
                <br />
            </asp:Panel>
            <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                <div>
                    <asp:Label ID="lblAttdMsg" runat="server"></asp:Label>
                </div>
                <br />
                <div class="cai-table mobile-table">
                    <telerik:radgrid id="gvStudDetails" runat="server" autogeneratecolumns="False" allowpaging="false"
                        sortingsettings-sorteddesctooltip="Sorted Descending" sortingsettings-sortedasctooltip="Sorted Ascending"
                        allowfilteringbycolumn="false" allowsorting="false" pagesize="5" showheaderswhennorecords="true">
                        <PagerStyle CssClass="sd-pager" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false"
                            EnableNoRecordsTemplate="true" ShowHeadersWhenNoRecords="true" DataKeyNames="ClassRegPartStatusID">
                            <!--Begin: Jim Code for sorting records using firstLast in ascending order -->
                            <SortExpressions>
                                <telerik:GridSortExpression FieldName="FirstLast" SortOrder ="Ascending" />
                            </SortExpressions>
                            <!--End: Jim Code for sorting records using firstLast in ascending order -->
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
                                <telerik:GridTemplateColumn HeaderStyle-Width="5%" HeaderText="Student Number" ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" AllowFiltering="false">

                                    <ItemTemplate>

                                        <asp:Label runat="server" ID="lblStudentNo" Text='<%# Eval("OldID")%>' />

                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderStyle-Width="5%" HeaderText="Student Name" ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" AllowFiltering="false" >

                                    <ItemTemplate>

                                        <asp:Label runat="server" ID="lblStudName" Text='<%# Eval("FirstLast")%>' />

                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>


                                      <telerik:GridTemplateColumn HeaderStyle-Width="5%" HeaderText="Exam Number" ItemStyle-HorizontalAlign="Left"
                                    ShowFilterIcon="false" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblExamNo" Text='<%# Eval("ExamNumber__c")%>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderStyle-Width="5%" HeaderText="Attended" ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" AllowFiltering="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblAttended" runat="server" Text="Attended"></asp:Label>
                                        <br />
                                        <asp:CheckBox ID="chkAllStudent" runat="server" AutoPostBack="true" OnCheckedChanged="ToggleSelectedState" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Checked='<%#IIf(Eval("Status__c") = "1", True, False)%>' />
                                        <asp:HiddenField runat="server" ID="hfPartStatusID" Value='<%# Eval("ClassRegPartStatusID")%>' />
                                        <asp:HiddenField runat="server" ID="hfClassRegID" Value='<%# Eval("ClassRegistrationID")%>' />
                                        <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("Status")%>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderStyle-Width="5%" HeaderText="Comments" ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" AllowFiltering="false">

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAddEdit" runat="server" Text="Add/Edit" ForeColor="Blue" OnClick="btnEdit_Click" CommandArgument='<%# Eval("ClassRegPartStatusID")%>'></asp:LinkButton>
                                        <asp:HiddenField runat="server" ID="hfinstcomment" Value='<%# Eval("InstructorComments")%>' />

                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                </EditColumn>
                            </EditFormSettings>
                            <PagerStyle PageSizeControlType="RadComboBox" />
                        </MasterTableView>
                        <%-- <PagerStyle PageSizeControlType="RadComboBox" />
                        <FilterMenu EnableImageSprites="False">
                        </FilterMenu>--%>
                    </telerik:radgrid>
                    <br />
                </div>
                <div class="row-div clearfix" align="center">
                    <div>
                        <asp:Button ID="btnBack" runat="server" Text="Back" Width="10%" Height="30px"></asp:Button>
                        <asp:Button ID="cmdSubmit" runat="server" Text="Submit" Width="10%" Height="30px">
                        </asp:Button>
                    </div>
                </div>
                <br />
            </asp:Panel>
        </div>
        <telerik:radwindow id="radWinInstructorComment" runat="server" modal="true" backcolor="#f4f3f1"
            visiblestatusbar="False" cssclass="pop-up" behaviors="None" forecolor="#BDA797"
            title="Instructor Comment" behavior="None" width="400px" height="250px">
            <ContentTemplate>
                <div class="cai-form">
                    <div class="cai-form-content">

                        <asp:Label ID="lblClassRegPartID" runat="server" Visible="false"></asp:Label><br />

                        <div>
                            Comment: 
                        </div>
                        <div class="field-div1 w375">
                            <asp:TextBox ID="txtComment" Style="resize: none;" TextMode="MultiLine" Width="350px" Height="150px"
                                runat="server"></asp:TextBox>
                        </div>

                        <div>
                            <br />
                        </div>
                        <div style="text-align: center;">
                            <asp:Button ID="btnOk" runat="server" Text="OK"
                                Width="70px" class="submitBtn" />
                            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" Width="80px" class="submitBtn" />
                        </div>

                        <div>
                            <br />
                        </div>

                    </div>
                </div>

            </ContentTemplate>
        </telerik:radwindow>
        <telerik:radwindow id="radConfirmation" runat="server" modal="true" backcolor="#f4f3f1"
            visiblestatusbar="False" cssclass="pop-up" behaviors="None" forecolor="#BDA797"
            title="Attendance Confirmation" behavior="None" height="250px">
            <ContentTemplate>
                <div class="cai-form">
                    <div class="cai-form-content">

                        <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
                        <div>
                            <br />
                        </div>
                        <div style="text-align: center;">
                            <asp:Button ID="btnYes" runat="server" Text="Yes"
                                Width="70px" class="submitBtn" />
                            <asp:Button ID="btnNo" runat="server" Text="No" Width="70px" class="submitBtn" />
                        </div>

                        <div>
                            <br />
                        </div>

                    </div>
                </div>

            </ContentTemplate>
        </telerik:radwindow>
        <telerik:radwindow id="radSuccMsg" runat="server" modal="true" backcolor="#f4f3f1"
            visiblestatusbar="False" cssclass="pop-up" behaviors="None" forecolor="#BDA797"
            title="Success Confirmation" behavior="None" height="250px">
            <ContentTemplate>
                <div class="cai-form">
                    <div class="cai-form-content">

                        <asp:Label ID="lblSuccMsg" runat="server"></asp:Label><br />
                        <div>
                            <br />
                        </div>
                        <div style="text-align: center;">
                            <asp:Button ID="btnSuccess" runat="server" Text="OK" Width="70px" class="submitBtn" />

                        </div>

                        <div>
                            <br />
                        </div>

                    </div>
                </div>

            </ContentTemplate>
        </telerik:radwindow>
        <div>
            <cc1:user id="User1" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
