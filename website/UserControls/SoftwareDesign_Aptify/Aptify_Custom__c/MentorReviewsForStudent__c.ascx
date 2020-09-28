<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/MentorReviewsForStudent__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MentorReviewsForStudent__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="info-data">
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
    <div align="right">
        <%-- <asp:HyperLink ID="lnkHelp" Text="Help" runat="server" Target="_blank"></asp:HyperLink>--%>
    </div>
    <div>
        <div class="cai-table">
            <telerik:RadGrid ID="radStudentReview" runat="server" AutoGenerateColumns="False"
                AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                AllowFilteringByColumn="false" AllowSorting="false" PageSize="5">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ReviewDate" HeaderText="Review date" SortExpression="ReviewDate"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                        <telerik:GridTemplateColumn HeaderText="Review title" AllowFiltering="false">
                            <ItemTemplate>
                                <span class="mobile-label">Review date:</span>
                                <asp:LinkButton CssClass="cai-table-data" ID="lnkMentorReviewTitle" runat="server" CommandName="MentorReviewTitle"
                                    CommandArgument='<%# Eval("ID")%>' Text='<%# Eval("MentorReviewTitle")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="Mentor" HeaderText="Mentor" SortExpression="Mentor"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">Mentor:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Mentor")%>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="Company" HeaderText="Company" SortExpression="Company"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">Company:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="Type" HeaderText="Review type" SortExpression="Type"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">Type:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">Status:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div class="actions">
		<div class="button-block style-1">
                	<asp:Button ID="btnBack" runat="server" Text="Back to CA Diary Dashboard" CssClass="btn-full-width btn" CausesValidation="false" ></asp:Button>
		</div>
        </div>
    </div>
</div>
<cc3:User ID="User1" runat="server" />
<telerik:RadWindow ID="radwindow" runat="server" VisibleOnPageLoad="false" Height="470px"
    Title="Detailed Mentor Review" Width="900px" BackColor="#f4f3f1" VisibleStatusbar="false"
    Behaviors="None" ForeColor="#BDA797" Modal="true">
    <ContentTemplate>
        <div class="info-data">
            <div>
                <span class="label-title-inline">Student Name:</span>
                <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <span class="label-title-inline">Trainee Student Number:</span>
                <asp:Label ID="lblTraineeStudentNumber" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <span class="label-title-inline">Business Unit:</span>
                <asp:Label ID="lblBueinessUnit" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <span class="label-title-inline">Route of Entry:</span>
                <asp:Label ID="lblRouteOfEntry" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <span class="label-title-inline">Mentor Name:</span>
                <asp:Label ID="lblMentorName" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <span class="label-title-inline">Review Type:</span>
                <asp:Label ID="lblReviewType" runat="server" Text=""></asp:Label>
            </div>

            <div id="dvReviewPeriodID" runat="server">
                <span class="label-title-inline">Six monthly review period starting:</span>
                <asp:Label ID="lblReviewStartDate" runat="server" Text=""></asp:Label>
            </div>

            <div id="dvMentorTitle" runat="server">
                <span class="label-title-inline">Mentor Evaluation Title: </span>
                <asp:Label ID="lblMentorReviewTitle" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <span class="label-title-inline">Mentor Evaluation Description:</span>
                <asp:Label ID="lblMentorEvaluationDescription" runat="server" Text=""></asp:Label>
            </div>

            <div class="actions">
                <asp:Button ID="btnPopupBack" runat="server" Text="Back" class="submitBtn" Width="10%" ></asp:Button>
            </div>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
