<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MentorDashboard__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_MentorDashboard__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<cc1:User runat="server" ID="User1" />
<style type="text/css">
    .main-container
    {
        margin: 0 1% 0 1%;
        width: 98%;
        word-wrap: break-word;
        overflow: hidden;
    }
    .container-left
    {
        float: left;
        overflow: hidden;
        width: 54%;
    }
    .container-right
    {
        float: right;
        overflow: hidden;
        width: 46%;
    }
    .lable-c
    {
        float: left;
        text-align: left;
        margin-right: 1%;
        font-weight: bold;
        font-size: 12px;
        overflow: hidden;
    }
    .w42
    {
        width: 42.5%;
    }
    .w75
    {
        width: 161px;
    }
</style>
<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>
<div>
    <asp:UpdatePanel ID="UpAreasOfExp" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="main-container" style="width: 1000px;">
                <div>
                    <div class="row-div">
                        <div class=" container-left">
                            <div class="row-div clearfix">
                                <div class="lable-c w42">
                                    <div>
                                        Profile Information:
                                    </div>
                                    <div class="lable-c w9">
                                        Mentor #:
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="field-div1 w75">
                                            <asp:Label ID="lblMentorName" Width="150px" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hidMetorID" Value="0" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=" container-right">
                            <div class="row-div clearfix">
                                <div class="lable-c">
                                    MENU:
                                </div>
                                <div>
                                    <br />
                                </div>
                                <div class="row-div clearfix">
                                    <div style="width: 477px;" class="field-div1 w250">
                                        <div>
                                            <asp:LinkButton ID="lnkCreateMentorReview" Style="text-decoration: underline;" runat="server">Create a 6 monthly Mentor Review for a Student</asp:LinkButton></div>
                                        <div>
                                            <asp:LinkButton ID="lnkSubmitQuerytoCAI" Style="text-decoration: underline;" runat="server">Submit Query to CAI</asp:LinkButton></div>
                                        <div>
                                            <asp:LinkButton ID="lnkSubmitFinalReview" Style="text-decoration: underline;" runat="server">Submit Final Review</asp:LinkButton></div>
                                        <div>
                                            <asp:LinkButton ID="lnkCADiaryGuidelines" Style="text-decoration: underline;" runat="server">CA Diary Guidelines</asp:LinkButton>
                                        </div>
                                        <div>
                                            <asp:LinkButton ID="lnkCAIUpdates" Style="text-decoration: underline;" runat="server">CAI Updates</asp:LinkButton></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="main-container" style="width: 1000px;">
                <div>
                    <div class="row-div">
                        <div class="row-div clearfix">
                            <div style="width: 550px;" class="field-div1 w550">
                                <div>
                                    <b>&nbsp;Student Information :</b></div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="PanelSTudInfo" BorderWidth="1" Width="970px" Style="border: 1; background: white;"
                    runat="server">
                    <div>
                        <div class="row-div">
                            <div class="row-div clearfix">
                                <div style="width: 950px;" class="field-div1 w950">
                                    <div>
                                        <div class="row-div clearfix">
                                            <div class="field-div1 w5">
                                                <div>
                                                    &nbsp;&nbsp;
                                                </div>
                                            </div>
                                            <div class="field-div1 w50">
                                                <div>
                                                    <b>Statistics</b>
                                                </div>
                                                <div>
                                                    Gaining Experience
                                                </div>
                                                <div>
                                                    Experience Gained
                                                </div>
                                                <div>
                                                    Final Review
                                                </div>
                                                <div>
                                                    Submitted Request to be Admitted to Membership
                                                </div>
                                                <div>
                                                    Total Assigned Students
                                                </div>
                                            </div>
                                            <div class="field-div1 w15">
                                                <div>
                                                    Number of Students
                                                </div>
                                                <div style="text-align: center;">
                                                    <asp:Label ID="lblGainingExperience" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div style="text-align: center;">
                                                    <asp:Label ID="lblExperienceGained" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div style="text-align: center;">
                                                    <asp:Label ID="lblFinalReview" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div style="text-align: center;">
                                                    <asp:Label ID="lblAdmittedtoMembership" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div style="text-align: center;">
                                                    <b>
                                                        <asp:Label ID="lblTotalAssignedStudents" runat="server" Text="0"></asp:Label></b>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div>
                <br />
            </div>
            <div class="main-container" style="width: 1000px;">
                <div>
                    <div runat="server" visible="false" id="divGainingExperience">
                        <div class="row-div">
                            <div class="row-div clearfix">
                                <div class="field-div1 w550">
                                    <div>
                                        <b>Gaining Experience: </b>
                                    </div>
                                    <div>
                                        <asp:Panel ID="panel2" BorderWidth="1" Width="970px" Style="border: 1;" runat="server">
                                            <div>
                                                <div class="row-div">
                                                    <div class="row-div clearfix">
                                                        <div class="field-div1 w450">
                                                            <div>
                                                                <telerik:RadGrid ID="radGainingExperience" runat="server" AutoGenerateColumns="False"
                                                                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                                    AllowFilteringByColumn="false" AllowSorting="false" Width="150px" PageSize="5"
                                                                    ShowHeadersWhenNoRecords="true">
                                                                    <GroupingSettings CaseSensitive="false" />
                                                                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false"
                                                                        EnableNoRecordsTemplate="true" ShowHeadersWhenNoRecords="true">
                                                                        <ColumnGroups>
                                                                            <telerik:GridColumnGroup HeaderText="Diary Entries" Name="DiaryEntries">
                                                                            </telerik:GridColumnGroup>
                                                                            <telerik:GridColumnGroup HeaderText="Mentor Reviews" Name="MentorReviews">
                                                                            </telerik:GridColumnGroup>
                                                                        </ColumnGroups>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="StudentNo" HeaderText="Student#" SortExpression="StudentNo"
                                                                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                                                FilterControlWidth="100%" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                                                            <telerik:GridTemplateColumn HeaderText="Name" AllowFiltering="true" SortExpression="FirstLast"
                                                                                AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkStudentName" runat="server" ForeColor="Blue" CommandName="PersonName"
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName"
                                                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Left" />
                                                                            <telerik:GridBoundColumn DataField="BusinessUnit" HeaderText="Business Unit" SortExpression="BusinessUnit"
                                                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left" />
                                                                            <telerik:GridBoundColumn DataField="RouteOfEntry" HeaderText="Route Of Entry" SortExpression="RouteOfEntry"
                                                                                AutoPostBackOnFilter="false" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left" />
                                                                            <telerik:GridBoundColumn DataField="CompletionDate" HeaderText="Completion Date"
                                                                                SortExpression="CompletionDate" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                                HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left" />
                                                                            <%--<telerik:GridBoundColumn DataField="InProgress" HeaderText="In Progress" AllowSorting="false"
                                                                                SortExpression="InProgress" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                                HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />--%>
                                                                                <telerik:GridTemplateColumn HeaderText="In Progress" AllowFiltering="false" SortExpression="InProgress"
                                                                                AutoPostBackOnFilter="true" DataField="InProgress" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="5%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkInProgress" runat="server" Enabled='<%#IIf(Eval("InProgress")=0,false,true) %>'
                                                                                        ForeColor="Blue" CommandName="InProgress" Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>'
                                                                                        Text='<%# Eval("InProgress")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Submitted For Review" AllowFiltering="false"
                                                                                SortExpression="SubmittedForReview" AutoPostBackOnFilter="true" DataField="SubmittedForReview"
                                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                                ItemStyle-Width="6%" ColumnGroupName="DiaryEntries">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkSubmittedForReview" runat="server" ForeColor="Blue" CommandName="SubmittedForReview"
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("SubmittedForReview")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Reviewed (Locked)" AllowFiltering="false"
                                                                                SortExpression="Reviewed" AutoPostBackOnFilter="true" DataField="Reviewed" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="4%" ColumnGroupName="DiaryEntries">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkReviewedLocked" runat="server" ForeColor="Blue" CommandName="ReviewedLocked"
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("Reviewed")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn DataField="NoOfReviews" HeaderText="Number Of Reviews" SortExpression="NoOfReviews"
                                                                                AutoPostBackOnFilter="false" AllowFiltering="false" AllowSorting="false" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Left"
                                                                                ColumnGroupName="MentorReviews" />
                                                                            <telerik:GridBoundColumn DataField="DateLastReview" HeaderText="Date of Last Review"
                                                                                SortExpression="DateLastReview" AutoPostBackOnFilter="false" AllowSorting="false"
                                                                                AllowFiltering="false" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                                FilterControlWidth="100%" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Left"
                                                                                ColumnGroupName="MentorReviews" />
                                                                            <telerik:GridBoundColumn DataField="NextReviewDate" HeaderText="Review Date" AllowSorting="false"
                                                                                SortExpression="NextReviewDate" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                                HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Left" ColumnGroupName="MentorReviews" />
                                                                            <telerik:GridTemplateColumn HeaderText="CA Diary" AllowFiltering="false" SortExpression="CADiary"
                                                                                AutoPostBackOnFilter="true" DataField="CADiary" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="3%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkCADiary" runat="server" ForeColor="Blue" CommandName="PDF1"
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("CADiary")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                        <NoRecordsTemplate>
                                                                            <asp:Label ID="lblNoGainingExp" runat="server" Text="No Record Found" Font-Bold="true"
                                                                                ForeColor="Red"></asp:Label>
                                                                        </NoRecordsTemplate>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </div>
                                                            <div>
                                                                <br />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" visible="false" id="divGainedExperience">
                        <div class="row-div">
                            <div class="row-div clearfix">
                                <div class="field-div1 w550">
                                    <div>
                                        <b>Experience Gained + Submitted Request to be Admitted to Membership: </b>
                                    </div>
                                    <div>
                                        <asp:Panel Width="970px" ID="panel1" BorderWidth="1" Style="border: 1;" runat="server">
                                            <div>
                                                <div class="row-div">
                                                    <div class="row-div clearfix">
                                                        <div class="field-div1 w450">
                                                            <div>
                                                                <telerik:RadGrid ID="radGainedExperience" runat="server" AutoGenerateColumns="False"
                                                                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                                    AllowFilteringByColumn="false" AllowSorting="false" Width="150px" PageSize="5"
                                                                    ShowHeadersWhenNoRecords="true">
                                                                    <GroupingSettings CaseSensitive="false" />
                                                                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false"
                                                                        EnableNoRecordsTemplate="true" ShowHeadersWhenNoRecords="true">
                                                                        <ColumnGroups>
                                                                            <telerik:GridColumnGroup HeaderText="Diary Entries" Name="DiaryEntries">
                                                                            </telerik:GridColumnGroup>
                                                                            <telerik:GridColumnGroup HeaderText="Mentor Reviews" Name="MentorReviews">
                                                                            </telerik:GridColumnGroup>
                                                                        </ColumnGroups>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="StudentNo" HeaderText="Student#" SortExpression="StudentNo"
                                                                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                                                FilterControlWidth="100%" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left" />
                                                                            <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                                                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                                                FilterControlWidth="100%" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left" />
                                                                            <telerik:GridTemplateColumn HeaderText="Name" AllowFiltering="true" SortExpression="FirstLast"
                                                                                AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkStudentName" runat="server" ForeColor="Blue" CommandName="PersonName"
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName"
                                                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                                            <telerik:GridBoundColumn DataField="BusinessUnit" HeaderText="Business Unit" SortExpression="BusinessUnit"
                                                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                                            <telerik:GridBoundColumn DataField="RouteOfEntry" HeaderText="Route Of Entry" SortExpression="RouteOfEntry"
                                                                                AutoPostBackOnFilter="false" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left" />
                                                                            <telerik:GridBoundColumn DataField="CompletionDate" HeaderText="Completion Date"
                                                                                SortExpression="CompletionDate" AutoPostBackOnFilter="false" AllowFiltering="true"
                                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                                HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left" />
                                                                            <%--<telerik:GridBoundColumn DataField="InProgress" HeaderText="In Progress" SortExpression="InProgress"
                                                                                AutoPostBackOnFilter="false" AllowFiltering="false" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left"
                                                                                ColumnGroupName="DiaryEntries" />--%>
                                                                                <telerik:GridTemplateColumn HeaderText="In Progress" AllowFiltering="false" SortExpression="InProgress"
                                                                                AutoPostBackOnFilter="true" DataField="InProgress" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="5%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkInProgress" runat="server" Enabled='<%#IIf(Eval("InProgress")=0,false,true) %>'
                                                                                        ForeColor="Blue" CommandName="InProgress" Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>'
                                                                                        Text='<%# Eval("InProgress")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Submitted For Review" AllowFiltering="true"
                                                                                SortExpression="SubmittedForReview" AutoPostBackOnFilter="true" DataField="SubmittedForReview"
                                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                                ItemStyle-Width="7%" ColumnGroupName="DiaryEntries">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkSubmittedForReview" runat="server" ForeColor="Blue" CommandName="SubmittedForReview"
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("SubmittedForReview")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Reviewed (Locked)" AllowFiltering="true"
                                                                                SortExpression="Reviewed" AutoPostBackOnFilter="true" DataField="Reviewed" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="6%" ColumnGroupName="DiaryEntries">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkReviewedLocked" runat="server" ForeColor="Blue" CommandName="ReviewedLocked"
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("Reviewed")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn DataField="NoOfReviews" HeaderText="Number Of Reviews" SortExpression="NoOfReviews"
                                                                                AutoPostBackOnFilter="false" AllowFiltering="false" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left"
                                                                                ColumnGroupName="MentorReviews" />
                                                                            <telerik:GridBoundColumn DataField="DateLastReview" HeaderText="Date of Last Review"
                                                                                SortExpression="DateLastReview" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                                HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left" ColumnGroupName="MentorReviews" />
                                                                            <telerik:GridBoundColumn DataField="NextReviewDate" HeaderText="Next Review Date"
                                                                                SortExpression="NextReviewDate" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                                HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left" ColumnGroupName="MentorReviews" />
                                                                            <telerik:GridTemplateColumn HeaderText="CA Diary" AllowFiltering="true" SortExpression="CADiary"
                                                                                AutoPostBackOnFilter="true" DataField="CADiary" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="4%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkCADiary" runat="server" ForeColor="Blue" CommandName="PDF1"
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("CADiary")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Admission To Membership" AllowFiltering="true"
                                                                                SortExpression="CADiary2" AutoPostBackOnFilter="true" DataField="CADiary2" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="4%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkCADiary2" runat="server" ForeColor="Blue" CommandName="PDF2"
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("CADiary2")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                        <NoRecordsTemplate>
                                                                            <asp:Label ID="lblNoGainedExp" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                        </NoRecordsTemplate>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </div>
                                                            <div>
                                                                <br />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <br />
                </div>
                <%-- <div class="main-container" style="width: 300px;">--%>
                <div runat="server" visible="false" id="divsixmonthlyreview">
                    <div class="row-div">
                        <div class="row-div clearfix">
                            <div class="field-div1 w550">
                                <div>
                                    <b>Student Request For 6 Monthly Review</b>
                                </div>
                                <div>
                                    <asp:Panel ID="PanelSixMonthlReview" Width="290px" BorderWidth="1" Style="border: 1;"
                                        runat="server">
                                        <div>
                                         <%--<asp:Label ID="lblsixmonthlyReview" Visible="false" runat="server" Font-Bold="true"
                                                                            ForeColor="Red" Text=""></asp:Label>--%>
                                        </div>
                                        <div>
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    <div class="field-div1 w100">
                                                        <div>
                                                            <telerik:RadGrid ID="radStudentSixMonthlReview" runat="server" AutoGenerateColumns="False"
                                                                AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                                AllowFilteringByColumn="false" AllowSorting="true" Width="250px" PageSize="10" ShowHeadersWhenNoRecords="true">
                                                                <GroupingSettings CaseSensitive="false" />
                                                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false" EnableNoRecordsTemplate="true" ShowHeadersWhenNoRecords="true">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="StudentNo" HeaderText="Student#" SortExpression="StudentNo"
                                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                            AllowFiltering="false" />
                                                                        <telerik:GridTemplateColumn HeaderText="Name" AllowFiltering="false" SortExpression="FirstLast"
                                                                            AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkStudentName" runat="server" ForeColor="Blue" CommandName="PersonName"
                                                                                    Font-Underline="true" CommandArgument='<%# Eval("DiaryID")%>' Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                    <NoRecordsTemplate>
                                                                      <%-- <div> No Records Found</div>--%>
                                                                       <div><asp:Label ID="lblsixmonthlyReview" Visible="true" runat="server" Font-Bold="true"
                                                                            ForeColor="Red" Text=""></asp:Label></div>
                                                                    </NoRecordsTemplate>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <br />
                </div>
                <%--<div class="main-container" style="width: 300px;">--%>
                <div runat="server" visible="true" id="divFinalReview">
                    <div class="row-div">
                        <div class="row-div clearfix">
                            <div class="field-div1 w550">
                                <div>
                                    <b>Student Request For Final Review</b>
                                </div>
                                <div>
                                    <asp:Panel ID="PanelFinalReview" Width="290px" BorderWidth="1" Style="border: 1;"
                                        runat="server">
                                        <div>
                                         <%--<asp:Label ID="lblFinalReviewError" Visible="false" runat="server" Font-Bold="true"
                                                                            ForeColor="Red" Text=""></asp:Label>--%>
                                        </div>
                                        <div>
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    <div class="field-div1 w100">
                                                        <div>
                                                            <telerik:RadGrid ID="radStudentFinalReview" runat="server" AutoGenerateColumns="False"
                                                                AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                                AllowFilteringByColumn="false" AllowSorting="true" Width="250px" PageSize="10" ShowHeadersWhenNoRecords="true">
                                                                <GroupingSettings CaseSensitive="false" />
                                                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false" EnableNoRecordsTemplate="true"  ShowHeadersWhenNoRecords="true">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="StudentNo" HeaderText="Student#" SortExpression="StudentNo"
                                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                            AllowFiltering="false" />
                                                                        <telerik:GridTemplateColumn HeaderText="Name" AllowFiltering="false" SortExpression="FirstLast"
                                                                            AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkStudentName" runat="server" ForeColor="Blue" CommandName="PersonName"
                                                                                    Font-Underline="true" CommandArgument='<%# Eval("DiaryID")%>' Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                    <NoRecordsTemplate>
                                                                       <%--<div> No Records Found</div>--%>
                                                                       <div><asp:Label ID="lblFinalReviewError" Visible="true" runat="server" Font-Bold="true"
                                                                            ForeColor="Red" Text=""></asp:Label></div>
                                                                    </NoRecordsTemplate>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <br />
            </div>
            <telerik:RadWindow ID="radWindowSubmitaQuerytoCAI" runat="server" Width="450px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Submit a Query to CAI" Behavior="None" Height="320px">
                <ContentTemplate>
                    <div>
                        <div>
                            <div>
                                <br />
                                <div class="row-div clearfix">
                                    <div class="lable-c w30" style="text-align: right;">
                                        &nbsp;
                                    </div>
                                    <div class="field-div1 w175">
                                        <div style="text-align: left;">
                                            <%--<asp:Label ID="lblValidationQuerytoCAI" runat="server" Style="color: Red;" Text=""></asp:Label>--%>
                                            <div>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlQuestionCategory"
                                                    ErrorMessage="Please select Question Category" CssClass="required-label" Operator="NotEqual"
                                                    ValidationGroup="S" ValueToCompare="Select" SetFocusOnError="true"></asp:CompareValidator>
                                            </div>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuestion"
                                                    ErrorMessage="Please enter Question" ValidationGroup="S" SetFocusOnError="true"
                                                    Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="lable-c w30" style="text-align: right;">
                                        <span style="color: Red;">*</span> Question Category:
                                    </div>
                                    <div class="field-div1 w175">
                                        <asp:DropDownList ID="ddlQuestionCategory" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="lable-c w30" style="text-align: right;">
                                        <span style="color: Red;">*</span>Question:
                                    </div>
                                    <div class="field-div1 w375">
                                        <asp:TextBox ID="txtQuestion" Style="resize: none;" TextMode="MultiLine" Width="250px"
                                            runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                    <br />
                                </div>
                                <div style="text-align: center;">
                                    <asp:Button ID="btnSubmitQuestion" ValidationGroup="S" runat="server" Text="Submit"
                                        Width="70px" class="submitBtn" />
                                    <asp:Button ID="btnCancelQuestion" runat="server" Text="Cancel" Width="70px" class="submitBtn" />
                                </div>
                                <div>
                                    <br />
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
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblValidationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div>
                                    <br />
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
