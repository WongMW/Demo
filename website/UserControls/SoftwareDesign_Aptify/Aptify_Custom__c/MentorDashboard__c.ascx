<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/MentorDashboard__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_MentorDashboard__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<cc1:User runat="server" ID="User1" />

<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>
<div>
    <asp:UpdatePanel ID="UpAreasOfExp" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="main-container clearfix">
                <div class="aptify-category-inner-side-nav">
                    <div id="divMenu" runat="server">
                        <h6>MENU:</h6>
                        <ul>
                            <li>
                                <asp:LinkButton ID="lnkCreateMentorReview" runat="server">Create a 6 monthly mentor review for a student</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkSubmitQuerytoCAI" runat="server" visible="false">Submit query to the Institute</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkSubmitFinalReview" runat="server">Submit final review</asp:LinkButton>
                            </li>
                            <li>
                             <asp:LinkButton ID="lnkCADiaryGuidelines" runat="server">User guides</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkCAIUpdates" runat="server">Institute updates</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="container-left ">
				    <div class="warning-msg-box">
                        <span>
                            Can't open PDFs? Ensure your browser's popup blocker is disabled. See <a href="https://www.charteredaccountants.ie/Prospective-Students/browser-popups.aspx" target="_blank">browser popups</a> for more info.
                        </span>
                    </div>
                    <div class="cai-form">
                        <span class="form-title">Profile information:</span>

                        <div class="field-group">
                            <span class="label-title-inline">Mentor#:</span>
                            <asp:Label ID="lblMentorName" Width="150px" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hidMetorID" Value="0" runat="server" />
                        </div>
                    </div>

                    <div class="cai-form">
                        <span class="form-title">Student information:</span>
                        <div class="cai-form-content">
                            <asp:Panel ID="PanelSTudInfo" runat="server">
                                <span class="label label-title-inline">Statistics:</span>
                                <table>
                                    <tr>
                                        <td>
                                            <span class="label-title-inline">Gaining experience:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblGainingExperience" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><span class="label-title-inline">Experience gained:</span>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblExperienceGained" runat="server" Text="0"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="label-title-inline">Final review:</span>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblFinalReview" runat="server" Text="0"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="label-title-inline">Submitted request to be admitted to membership:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAdmittedtoMembership" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><span class="label-title-inline">Total assigned students:</span></td>
                                        <td><asp:Label ID="lblTotalAssignedStudents" runat="server" Text="0"></asp:Label></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <div class="cai-form">
                <div runat="server" visible="false" id="divGainingExperience">
                    <span class="form-title">Gaining experience: </span>
                    <div class="cai-form-content">
                        <asp:Panel ID="panel2" runat="server">
                            <div class="cai-table mobile-table">
                                <telerik:RadGrid ID="radGainingExperience" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AllowFilteringByColumn="false" AllowSorting="false" PageSize="5"
                                    ShowHeadersWhenNoRecords="true">
                                    <PagerStyle CssClass="sd-pager" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false"
                                        EnableNoRecordsTemplate="true" ShowHeadersWhenNoRecords="true">
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup HeaderText="Diary entries" Name="DiaryEntries">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup HeaderText="Mentor reviews" Name="MentorReviews">
                                            </telerik:GridColumnGroup>
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridTemplateColumn DataField="StudentNo" HeaderText="Student#" SortExpression="StudentNo"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Student#:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("StudentNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Name" AllowFiltering="true" SortExpression="FirstLast"
                                                AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Student name:</span>
                                                    <asp:LinkButton ID="lnkStudentName" runat="server" CommandName="PersonName"
                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' CssClass="cai-table-data" Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Company name:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("CompanyName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="BusinessUnit" HeaderText="Business unit" SortExpression="BusinessUnit"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Business unit</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("BusinessUnit")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="RouteOfEntry" HeaderText="Route of entry" SortExpression="RouteOfEntry"
                                                AutoPostBackOnFilter="false" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Route of entry:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("RouteOfEntry")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CompletionDate" HeaderText="Completion date"
                                                SortExpression="CompletionDate" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Completion date:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("CompletionDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="In progress" AllowFiltering="false" SortExpression="InProgress"
                                                AutoPostBackOnFilter="true" DataField="InProgress" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">In progress:</span>
                                                    <asp:LinkButton ID="lnkInProgress" runat="server" Enabled='<%#IIf(Eval("InProgress")=0,false,true) %>'
                                                        CommandName="InProgress" Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' CssClass="cai-table-data"
                                                        Text='<%# Eval("InProgress")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Submitted for review" AllowFiltering="false"
                                                SortExpression="SubmittedForReview" AutoPostBackOnFilter="true" DataField="SubmittedForReview"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                ColumnGroupName="DiaryEntries">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Submitted for review:</span>
                                                    <asp:LinkButton ID="lnkSubmittedForReview" runat="server" CommandName="SubmittedForReview"
                                                        Font-Underline="true" CssClass="cai-table-data" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("SubmittedForReview")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Reviewed (locked)" AllowFiltering="false"
                                                SortExpression="Reviewed" AutoPostBackOnFilter="true" DataField="Reviewed" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" ColumnGroupName="DiaryEntries">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Reviewed (locked):</span>
                                                    <asp:LinkButton ID="lnkReviewedLocked" runat="server" CommandName="ReviewedLocked"
                                                        Font-Underline="true" CssClass="cai-table-data" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("Reviewed")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="NoOfReviews" HeaderText="Number of reviews" SortExpression="NoOfReviews"
                                                AutoPostBackOnFilter="false" AllowFiltering="false" AllowSorting="false" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false"
                                                ColumnGroupName="MentorReviews">
                                                <ItemTemplate>
                                                    <span class="mobile-label">No of reviews:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("NoOfReviews")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="DateLastReview" HeaderText="Date of last review"
                                                SortExpression="DateLastReview" AutoPostBackOnFilter="false" AllowSorting="false"
                                                AllowFiltering="false" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                ColumnGroupName="MentorReviews">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Date of last review:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("DateLastReview")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="NextReviewDate" HeaderText="Review date" AllowSorting="false"
                                                SortExpression="NextReviewDate" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                ColumnGroupName="MentorReviews">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Review date:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("NextReviewDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="CA Diary" AllowFiltering="false" SortExpression="CADiary"
                                                AutoPostBackOnFilter="true" DataField="CADiary" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">CA Diary:</span>
                                                    <asp:LinkButton ID="lnkCADiary" runat="server" CommandName="PDF1"
                                                        Font-Underline="true" CssClass="cai-table-data" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("CADiary")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="lblNoGainingExp" runat="server" Text="No record found"></asp:Label>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <%--relocation  this below div dashboard-split mentor-dashboard-split for this Redmine log #20349--%>
            <div class="dashboard-split mentor-dashboard-split clearfix">  <%--added clearfix css for issue #20430--%>
                <div class="dashboard-section-half">
                    <div class="cai-form">
                        <div runat="server" visible="false" id="divsixmonthlyreview">
                            <span class="form-title">Student request for 6 monthly review</span>
                            <div class="cai-form-content">
                                <asp:Panel ID="PanelSixMonthlReview" runat="server">
                                    <div class="cai-table mobile-table">
                                        <telerik:RadGrid ID="radStudentSixMonthlReview" runat="server" AutoGenerateColumns="False"
                                            AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                            AllowFilteringByColumn="false" AllowSorting="true"  PageSize="10" ShowHeadersWhenNoRecords="true">
                                            <PagerStyle CssClass="sd-pager" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false" EnableNoRecordsTemplate="true" ShowHeadersWhenNoRecords="true">
                                                <Columns>
                                                    <telerik:GridTemplateColumn DataField="StudentNo" HeaderText="Student#" SortExpression="StudentNo"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                        AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <span class="mobile-label">Student#:</span>
                                                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("StudentNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Name" AllowFiltering="false" SortExpression="FirstLast"
                                                        AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <span class="mobile-label">Name:</span>
                                                            <asp:LinkButton ID="lnkStudentName" runat="server" CommandName="PersonName"
                                                                Font-Underline="true" CssClass="cai-table-data" CommandArgument='<%# Eval("DiaryID")%>' Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <NoRecordsTemplate>
                                                    <div>
                                                        <asp:Label ID="lblsixmonthlyReview" Visible="true" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </NoRecordsTemplate>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="dashboard-section-half">
                    <div class="cai-form">
                        <div runat="server" visible="true" id="divFinalReview">
                            <span class="form-title">Student request for final review</span>
                            <div class="cai-form-content">
                                <asp:Panel ID="PanelFinalReview" runat="server">
                                    <div class="cai-table mobile-table">
                                        <telerik:RadGrid ID="radStudentFinalReview" runat="server" AutoGenerateColumns="False"
                                            AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                            AllowFilteringByColumn="false" AllowSorting="true" PageSize="10" ShowHeadersWhenNoRecords="true">
                                            <PagerStyle CssClass="sd-pager" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false" EnableNoRecordsTemplate="true" ShowHeadersWhenNoRecords="true">
                                                <Columns>
                                                    <telerik:GridTemplateColumn DataField="StudentNo" HeaderText="Student#" SortExpression="StudentNo"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                        AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <span class="mobile-label">Student#:</span>
                                                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("StudentNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Name" AllowFiltering="false" SortExpression="FirstLast"
                                                        AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <span class="mobile-label">Name:</span>
                                                            <asp:LinkButton ID="lnkStudentName" runat="server" CommandName="PersonName"
                                                                Font-Underline="true" CssClass="cai-table-data" CommandArgument='<%# Eval("DiaryID")%>' Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <NoRecordsTemplate>
                                                    <%--<div> No Records Found</div>--%>
                                                    <div>
                                                        <asp:Label ID="lblFinalReviewError" Visible="true" runat="server"
                                                            Text=""></asp:Label>
                                                    </div>
                                                </NoRecordsTemplate>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

           <%-- END Redmine log #20349--%>
            <div class="cai-form clearfix"> <%--added clearfix css and removed style tag for issue #20430--%>
                <div runat="server" visible="false" id="divGainedExperience">
                    <span class="form-title">Experience gained:</span>
                    <div class="cai-form-content">
                        <asp:Panel ID="panel1" runat="server">
                            <div class="cai-table mobile-table">
                                <telerik:RadGrid ID="radGainedExperience" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AllowFilteringByColumn="false" AllowSorting="false" PageSize="5"
                                    ShowHeadersWhenNoRecords="true">
                                    <PagerStyle CssClass="sd-pager" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false"
                                        EnableNoRecordsTemplate="true" ShowHeadersWhenNoRecords="true">
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup HeaderText="Diary entries" Name="DiaryEntries">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup HeaderText="Mentor reviews" Name="MentorReviews">
                                            </telerik:GridColumnGroup>
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridTemplateColumn DataField="StudentNo" HeaderText="Student#" SortExpression="StudentNo"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Student#:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("StudentNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Status:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Name" AllowFiltering="true" SortExpression="FirstLast"
                                                AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Name:</span>
                                                    <asp:LinkButton ID="lnkStudentName" runat="server" CommandName="PersonName"
                                                        Font-Underline="true" CssClass="cai-table-data" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Company:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("CompanyName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="BusinessUnit" HeaderText="Business unit" SortExpression="BusinessUnit"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Business unit:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("BusinessUnit")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="RouteOfEntry" HeaderText="Route of entry" SortExpression="RouteOfEntry"
                                                AutoPostBackOnFilter="false" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Route of entry:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("RouteOfEntry")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CompletionDate" HeaderText="Completion date"
                                                SortExpression="CompletionDate" AutoPostBackOnFilter="false" AllowFiltering="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Completion date:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("CompletionDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="In progress" AllowFiltering="false" SortExpression="InProgress"
                                                AutoPostBackOnFilter="true" DataField="InProgress" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">In progress:</span>
                                                    <asp:LinkButton ID="lnkInProgress" runat="server" Enabled='<%#IIf(Eval("InProgress")=0,false,true) %>'
                                                        CommandName="InProgress" CssClass="cai-table-data" Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>'
                                                        Text='<%# Eval("InProgress")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Submitted for review" AllowFiltering="true"
                                                SortExpression="SubmittedForReview" AutoPostBackOnFilter="true" DataField="SubmittedForReview"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                ColumnGroupName="DiaryEntries">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Submitted for review:</span>
                                                    <asp:LinkButton ID="lnkSubmittedForReview" runat="server" CommandName="SubmittedForReview"
                                                        Font-Underline="true" CssClass="cai-table-data" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("SubmittedForReview")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Reviewed (locked)" AllowFiltering="true"
                                                SortExpression="Reviewed" AutoPostBackOnFilter="true" DataField="Reviewed" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" ColumnGroupName="DiaryEntries">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Reviewed (locked):</span>
                                                    <asp:LinkButton ID="lnkReviewedLocked" runat="server" CommandName="ReviewedLocked"
                                                        Font-Underline="true" CssClass="cai-table-data" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("Reviewed")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="NoOfReviews" HeaderText="Number of reviews" SortExpression="NoOfReviews"
                                                AutoPostBackOnFilter="false" AllowFiltering="false" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false"
                                                ColumnGroupName="MentorReviews">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Number of reviews:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("NoOfReviews")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="DateLastReview" HeaderText="Date of last review"
                                                SortExpression="DateLastReview" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                ColumnGroupName="MentorReviews">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Date of last review:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("DateLastReview")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="NextReviewDate" HeaderText="Next review date"
                                                SortExpression="NextReviewDate" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                ColumnGroupName="MentorReviews">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Next review date:</span>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("NextReviewDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="CA Diary" AllowFiltering="true" SortExpression="CADiary"
                                                AutoPostBackOnFilter="true" DataField="CADiary" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">CA Diary:</span>
                                                    <asp:LinkButton ID="lnkCADiary" runat="server" CommandName="PDF1"
                                                        Font-Underline="true" CssClass="cai-table-data" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("CADiary")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Admission to membership" AllowFiltering="true"
                                                SortExpression="CADiary2" AutoPostBackOnFilter="true" DataField="CADiary2" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Admission to membership:</span>
                                                    <asp:LinkButton ID="lnkCADiary2" runat="server" CommandName="PDF2"
                                                        Font-Underline="true" CssClass="cai-table-data" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("CADiary2")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="lblNoGainedExp" runat="server" Text=""></asp:Label>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            

            <telerik:RadWindow ID="radWindowSubmitaQuerytoCAI" runat="server" Width="450px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" CssClass="pop-up" Behaviors="None" ForeColor="#BDA797"
                Title="Submit a query to the Institute" Behavior="None">
                <ContentTemplate>
                    <div class="cai-form">
                        <div class="cai-form-content">
                            <div>

                                <div class="row-div clearfix">
                                    <div class="lable-c w30">
                                        &nbsp;
                                    </div>
                                    <div class="field-div1 w175">
                                        <div style="text-align: left;">
                                            <%--<asp:Label ID="lblValidationQuerytoCAI" runat="server" Style="color: Red;" Text=""></asp:Label>--%>
                                            <div>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlQuestionCategory"
                                                    ErrorMessage="Please select question category" CssClass="required-label" Operator="NotEqual"
                                                    ValidationGroup="S" ValueToCompare="Select" SetFocusOnError="true"></asp:CompareValidator>
                                            </div>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuestion"
                                                    ErrorMessage="Please enter question" ValidationGroup="S" SetFocusOnError="true"
                                                    Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label">
                                        <span style="color: Red;">*</span> Question category:
                                    </div>
                                    <div class="field-div1 w175">
                                        <asp:DropDownList ID="ddlQuestionCategory" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label">
                                        <span style="color: Red;">*</span>Question:
                                    </div>
                                    <div class="field-div1 w375">
                                        <asp:TextBox ID="txtQuestion" Style="resize: none;" TextMode="MultiLine" Width="250px"
                                            runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                </div>
                                <div class="actions">
                                    <asp:Button ID="btnSubmitQuestion" ValidationGroup="S" runat="server" Text="Submit"
                                       class="submitBtn" />
                                    <asp:Button ID="btnCancelQuestion" runat="server" Text="Cancel" class="submitBtn" />
                                </div>
                                <div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblValidationMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                </div>
                                <div>
                                    <asp:Button ID="btnValidationOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
