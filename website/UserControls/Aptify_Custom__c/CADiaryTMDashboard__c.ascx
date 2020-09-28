<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CADiaryTMDashboard__c.ascx.vb"
    Inherits="CADiaryTMDashboard__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<cc1:User runat="server" ID="User1" />
<%--<link href="../../CSS/Div_Data_form.css" rel="stylesheet" type="text/css" />--%>
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
</style>
<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>
<div>
    <div>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
    </div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:UpdatePanel ID="UpAreasOfExp" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="main-container" style="width: 1000px;">
                    <div>
                        <div class="row-div">
                            <div class=" container-left">
                                &nbsp;
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
                                        <div style="width: 240px;" class="field-div1 w225">
                                            <div>
                                                <asp:LinkButton ID="lnkAssignmentofMentors" Style="text-decoration: underline;" runat="server">Assignment of Mentors</asp:LinkButton></div>
                                            <div>
                                                <asp:LinkButton ID="lnkAssignaBusinessUnit" Style="text-decoration: underline;" runat="server">Assign a Business Unit to a Student</asp:LinkButton></div>
                                            <div>
                                                <asp:LinkButton ID="lnkCADiaryGuidelines" Style="text-decoration: underline;" runat="server">CA Diary Guidelines</asp:LinkButton>
                                            </div>
                                            <div>
                                                <asp:LinkButton ID="lnkCAIUpdates" Style="text-decoration: underline;" runat="server">CAI Information Updates</asp:LinkButton>
                                            </div>
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
                                        <b>Mentor Information </b>
                                    </div>
                                    <div>
                                        Mentor:
                                        <asp:DropDownList ID="ddlMentor" AutoPostBack="true" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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
                                                            <%-- <div>
                                                            <asp:Label ID="lblGainingExp" Visible="false" runat="server" Font-Bold="true" ForeColor="Red"
                                                                Text=""></asp:Label>
                                                        </div>--%>
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
                                                                            <%-- <telerik:GridBoundColumn DataField="InProgress" HeaderText="In Progress" AllowSorting="false"
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
                                                                                    <asp:LinkButton ID="lnkSubmittedForReview" runat="server" ForeColor="Blue" Enabled='<%#IIf(Eval("SubmittedForReview")=0,false,true) %>'
                                                                                        CommandName="SubmittedForReview" Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>'
                                                                                        Text='<%# Eval("SubmittedForReview")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Reviewed (Locked)" AllowFiltering="false"
                                                                                SortExpression="Reviewed" AutoPostBackOnFilter="true" DataField="Reviewed" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="4%" ColumnGroupName="DiaryEntries">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkReviewedLocked" runat="server" ForeColor="Blue" CommandName="ReviewedLocked"
                                                                                        Enabled='<%#IIf(Eval("Reviewed")=0,false,true) %>' Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>'
                                                                                        Text='<%# Eval("Reviewed")%>'></asp:LinkButton>
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
                                                                            <telerik:GridBoundColumn DataField="NextReviewDate" HeaderText="Next Review Date"
                                                                                AllowSorting="false" SortExpression="NextReviewDate" AutoPostBackOnFilter="false"
                                                                                AllowFiltering="false" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                                FilterControlWidth="100%" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Left"
                                                                                ColumnGroupName="MentorReviews" />
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
                                                            <%-- <div>
                                                            <div>
                                                                <asp:Label ID="lblGainedExp" Visible="false" runat="server" Font-Bold="true" ForeColor="Red"
                                                                    Text=""></asp:Label>
                                                            </div>
                                                        </div>--%>
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
                                                                            <%-- <telerik:GridBoundColumn DataField="InProgress" HeaderText="In Progress" SortExpression="InProgress"
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
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Enabled='<%#IIf(Eval("SubmittedForReview")=0,false,true) %>' Text='<%# Eval("SubmittedForReview")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Reviewed (Locked)" AllowFiltering="true"
                                                                                SortExpression="Reviewed" AutoPostBackOnFilter="true" DataField="Reviewed" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="6%" ColumnGroupName="DiaryEntries">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkReviewedLocked" runat="server" ForeColor="Blue" CommandName="ReviewedLocked"
                                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Enabled='<%#IIf(Eval("Reviewed")=0,false,true) %>' Text='<%# Eval("Reviewed")%>'></asp:LinkButton>
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
    </telerik:RadAjaxPanel>
</div>
