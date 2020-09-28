<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/CADiaryTMDashboard__c.ascx.vb"
    Inherits="CADiaryTMDashboard__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<cc1:User runat="server" ID="User1" />

<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>

<div>
    <div>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
    </div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:UpdatePanel ID="UpAreasOfExp" UpdateMode="Conditional" runat="server">
               <%--Added for https://redmine.softwaredesign.ie/issues/16739--%>
             <Triggers> 
        <asp:PostBackTrigger ControlID="lnkCADairyStatus" />
    </Triggers>
               <%--End for https://redmine.softwaredesign.ie/issues/16739--%>
            <ContentTemplate>
                <div class="main-container clearfix">
                    <div class="aptify-category-inner-side-nav">
                        <div id="divMenu" runat="server">
                            <h6>MENU:</h6>
                            <ul>
                                <li>
                                    <asp:LinkButton ID="lnkAssignmentofMentors" runat="server">Assignment of mentors</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkAssignaBusinessUnit" runat="server">Assign a business unit to a student</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkCADiaryGuidelines" runat="server">CA Diary Handbook</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkCAIUpdates" runat="server">Institute information updates</asp:LinkButton>
                                </li>
   <%--Added for https://redmine.softwaredesign.ie/issues/16739--%>
                                 <li>
                                    <asp:LinkButton ID="lnkCADairyStatus" runat="server">CA Diary Status</asp:LinkButton>
                                </li>
                                 <%--End for https://redmine.softwaredesign.ie/issues/16739--%>
                            </ul>
                        </div>
                    </div>

                    <div class="container-left ">
                        <div class="cai-form">
                            <span class="form-title">Mentor information </span>

                            <div class="cai-form-content">
                                <div class="field-group">
                                    <span class="label-title">Mentor:</span>
                                    <asp:DropDownList ID="ddlMentor" AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
<br />
                        <div runat="server" visible="false" id="divGainingExperience" class="cai-form">

                            <span class="form-title">Gaining experience: </span>
                            <div class="cai-form-content">
                                <asp:Panel ID="panel2" BorderWidth="1" Width="1170px" Style="border: 0;" runat="server">
                                    <div>
                                        <div class="row-div">
                                            <div class="row-div clearfix">
                                                <div class="field-div1 w450">
                                                    <%-- <div>
                                                            <asp:Label ID="lblGainingExp" Visible="false" runat="server" Font-Bold="true" ForeColor="Red"
                                                                Text=""></asp:Label>
                                                        </div>--%>
                                                    <div class="cai-table mobile-table">
                                                        <telerik:RadGrid ID="radGainingExperience" runat="server" AutoGenerateColumns="False"
                                                            AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                            AllowFilteringByColumn="false" AllowSorting="false" Width="100%" PageSize="5"
                                                            ShowHeadersWhenNoRecords="true">
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
                                                                    <telerik:GridBoundColumn DataField="BusinessUnit" HeaderText="Business unit" SortExpression="BusinessUnit"
                                                                        AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left" />
                                                                    <telerik:GridBoundColumn DataField="RouteOfEntry" HeaderText="Route of entry" SortExpression="RouteOfEntry"
                                                                        AutoPostBackOnFilter="false" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left" />
                                                                    <telerik:GridBoundColumn DataField="CompletionDate" HeaderText="Completion date"
                                                                        SortExpression="CompletionDate" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                        HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left" />
                                                                    <%-- <telerik:GridBoundColumn DataField="InProgress" HeaderText="In Progress" AllowSorting="false"
                                                                                SortExpression="InProgress" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                                HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />--%>
                                                                    <telerik:GridTemplateColumn HeaderText="In progress" AllowFiltering="false" SortExpression="InProgress"
                                                                        AutoPostBackOnFilter="true" DataField="InProgress" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkInProgress" runat="server" Enabled='<%#IIf(Eval("InProgress")=0,false,true) %>'
                                                                                ForeColor="Blue" CommandName="InProgress" Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>'
                                                                                Text='<%# Eval("InProgress")%>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Submitted for review" AllowFiltering="false"
                                                                        SortExpression="SubmittedForReview" AutoPostBackOnFilter="true" DataField="SubmittedForReview"
                                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                        ItemStyle-Width="6%" ColumnGroupName="DiaryEntries">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkSubmittedForReview" runat="server" ForeColor="Blue" Enabled='<%#IIf(Eval("SubmittedForReview")=0,false,true) %>'
                                                                                CommandName="SubmittedForReview" Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>'
                                                                                Text='<%# Eval("SubmittedForReview")%>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Reviewed (locked)" AllowFiltering="false"
                                                                        SortExpression="Reviewed" AutoPostBackOnFilter="true" DataField="Reviewed" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="4%" ColumnGroupName="DiaryEntries">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkReviewedLocked" runat="server" ForeColor="Blue" CommandName="ReviewedLocked"
                                                                                Enabled='<%#IIf(Eval("Reviewed")=0,false,true) %>' Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>'
                                                                                Text='<%# Eval("Reviewed")%>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="NoOfReviews" HeaderText="Number of reviews" SortExpression="NoOfReviews"
                                                                        AutoPostBackOnFilter="false" AllowFiltering="false" AllowSorting="false" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="MentorReviews" />
                                                                    <telerik:GridBoundColumn DataField="DateLastReview" HeaderText="Date of last review"
                                                                        SortExpression="DateLastReview" AutoPostBackOnFilter="false" AllowSorting="false"
                                                                        AllowFiltering="false" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                        FilterControlWidth="100%" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="MentorReviews" />
                                                                    <telerik:GridBoundColumn DataField="NextReviewDate" HeaderText="Next review date"
                                                                        AllowSorting="false" SortExpression="NextReviewDate" AutoPostBackOnFilter="false"
                                                                        AllowFiltering="false" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                        FilterControlWidth="100%" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Left"
                                                                        ColumnGroupName="MentorReviews" />
                                                                    <telerik:GridTemplateColumn HeaderText="CA Diary" AllowFiltering="false" SortExpression="CADiary"
                                                                        AutoPostBackOnFilter="true" DataField="CADiary" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkCADiary" runat="server" ForeColor="Blue" CommandName="Report"
                                                                                Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("CADiary")%>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                                <NoRecordsTemplate>
                                                                    <asp:Label ID="lblNoGainingExp" runat="server" Text="No record found" Font-Bold="true"
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

                <div runat="server" visible="false" id="divGainedExperience" class="cai-form">
                    <span class="form-title">Experience gained:</span>
                    <div class="cai-form-content">
                        <asp:Panel Width="1170px" ID="panel1" BorderWidth="1" Style="border: 0;" runat="server">
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
                                            <div class="cai-table mobile table">
                                                <telerik:RadGrid ID="radGainedExperience" runat="server" AutoGenerateColumns="False"
                                                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                    AllowFilteringByColumn="false" AllowSorting="false" Width="100%" PageSize="5"
                                                    ShowHeadersWhenNoRecords="true">
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
                                                            <telerik:GridBoundColumn DataField="BusinessUnit" HeaderText="Business unit" SortExpression="BusinessUnit"
                                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                            <telerik:GridBoundColumn DataField="RouteOfEntry" HeaderText="Route of entry" SortExpression="RouteOfEntry"
                                                                AutoPostBackOnFilter="false" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left" />
                                                            <telerik:GridBoundColumn DataField="CompletionDate" HeaderText="Completion date"
                                                                SortExpression="CompletionDate" AutoPostBackOnFilter="false" AllowFiltering="true"
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left" />
                                                            <%-- <telerik:GridBoundColumn DataField="InProgress" HeaderText="In progress" SortExpression="InProgress"
                                                                                AutoPostBackOnFilter="false" AllowFiltering="false" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left"
                                                                                ColumnGroupName="DiaryEntries" />--%>
                                                            <telerik:GridTemplateColumn HeaderText="In progress" AllowFiltering="false" SortExpression="InProgress"
                                                                AutoPostBackOnFilter="true" DataField="InProgress" CurrentFilterFunction="Contains"
                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkInProgress" runat="server" Enabled='<%#IIf(Eval("InProgress")=0,false,true) %>'
                                                                        ForeColor="Blue" CommandName="InProgress" Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>'
                                                                        Text='<%# Eval("InProgress")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Submitted for review" AllowFiltering="true"
                                                                SortExpression="SubmittedForReview" AutoPostBackOnFilter="true" DataField="SubmittedForReview"
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                ItemStyle-Width="7%" ColumnGroupName="DiaryEntries">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSubmittedForReview" runat="server" ForeColor="Blue" CommandName="SubmittedForReview"
                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Enabled='<%#IIf(Eval("SubmittedForReview")=0,false,true) %>' Text='<%# Eval("SubmittedForReview")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Reviewed (locked)" AllowFiltering="true"
                                                                SortExpression="Reviewed" AutoPostBackOnFilter="true" DataField="Reviewed" CurrentFilterFunction="Contains"
                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="6%" ColumnGroupName="DiaryEntries">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkReviewedLocked" runat="server" ForeColor="Blue" CommandName="ReviewedLocked"
                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Enabled='<%#IIf(Eval("Reviewed")=0,false,true) %>' Text='<%# Eval("Reviewed")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="NoOfReviews" HeaderText="Number of reviews" SortExpression="NoOfReviews"
                                                                AutoPostBackOnFilter="false" AllowFiltering="false" CurrentFilterFunction="Contains"
                                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left"
                                                                ColumnGroupName="MentorReviews" />
                                                            <telerik:GridBoundColumn DataField="DateLastReview" HeaderText="Date of last review"
                                                                SortExpression="DateLastReview" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left" ColumnGroupName="MentorReviews" />
                                                            <telerik:GridBoundColumn DataField="NextReviewDate" HeaderText="Next review date"
                                                                SortExpression="NextReviewDate" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                                HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left" ColumnGroupName="MentorReviews" />
                                                            <telerik:GridTemplateColumn HeaderText="CA Diary" AllowFiltering="true" SortExpression="CADiary"
                                                                AutoPostBackOnFilter="true" DataField="CADiary" CurrentFilterFunction="Contains"
                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="4%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkCADiary" runat="server" ForeColor="Blue" CommandName="Report"
                                                                        Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("CADiary")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Admission to membership" AllowFiltering="true"
                                                                SortExpression="CADiary2" AutoPostBackOnFilter="true" DataField="CADiary2" CurrentFilterFunction="Contains"
                                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="4%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkCADiary2" runat="server" ForeColor="Blue" CommandName="Application"
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

                <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Modal="True"
                    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                    Title="" Behavior="None" Height="150px">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblValidationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
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
