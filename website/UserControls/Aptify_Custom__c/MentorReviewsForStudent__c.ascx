<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MentorReviewsForStudent__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MentorReviewsForStudent__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="info-data">
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    <div class="row-div clearfix" align="right">
        <%-- <asp:HyperLink ID="lnkHelp" Text="Help" runat="server" Target="_blank"></asp:HyperLink>--%>
    </div>
    <div class="row-div clearfix">
        <div class="field-div1 w100">
            <telerik:RadGrid ID="radStudentReview" runat="server" AutoGenerateColumns="False"
                AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                AllowFilteringByColumn="false" AllowSorting="false" Width="150px" PageSize="5">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ReviewDate" HeaderText="Review Date" SortExpression="ReviewDate"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderText="Review Title" AllowFiltering="false" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkMentorReviewTitle" runat="server" ForeColor="Blue" CommandName="MentorReviewTitle"
                                    Font-Underline="true" CommandArgument='<%# Eval("ID")%>' Text='<%# Eval("MentorReviewTitle")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Mentor" HeaderText="Mentor" SortExpression="Mentor"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Company" HeaderText="Company" SortExpression="Company"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Type" HeaderText="Review Type" SortExpression="Type"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                               <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <br />
            <asp:Button ID="btnBack" runat="server" Text="Back" Height="25px" CausesValidation="false" />
        </div>
    </div>
</div>
<cc3:User ID="User1" runat="server" />
<telerik:RadWindow ID="radwindow" runat="server" VisibleOnPageLoad="false" Height="500px"
    Title="Detailed Mentor Review" Width="900px" BackColor="#f4f3f1" VisibleStatusbar="false"
    Behaviors="None" ForeColor="#BDA797">
    <ContentTemplate>
        <div class="info-data">
           <div class="row-div clearfix">
                <div class="label-div w30">
                    &nbsp;<br />
                </div>
                <div class="field-div1 w60">
                   &nbsp;
                   <br />
                </div>
            </div>
            <div class="row-div clearfix">
                <div class="label-div w30">
                    Student Name:
                </div>
                <div class="field-div1 w60">
                    <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row-div clearfix">
                <div class="label-div w30">
                    Trainee Student Number:
                </div>
                <div class="field-div1 w60">
                    <asp:Label ID="lblTraineeStudentNumber" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row-div clearfix">
                <div class="label-div w30">
                    Business Unit:
                </div>
                <div class="field-div1 w60">
                    <asp:Label ID="lblBueinessUnit" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row-div clearfix">
                <div class="label-div w30">
                    Route of Entry:
                </div>
                <div class="field-div1 w60">
                    <asp:Label ID="lblRouteOfEntry" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row-div clearfix">
                <div class="label-div w30">
                    Mentor Name:
                </div>
                <div class="field-div1 w60">
                    <asp:Label ID="lblMentorName" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row-div clearfix" >
                <div class="label-div w30">
                    Review Type:
                </div>
                <div class="field-div1 w60">
                    <asp:Label ID="lblReviewType" runat="server" Text=""></asp:Label>
                </div>
            </div>
          
            <div class="row-div clearfix" id="dvReviewPeriodID" runat="server">
                <div class="label-div w30">
                    Six monthly review period starting:
                </div>
                <div class="field-div1 w60">
                    <asp:Label ID="lblReviewStartDate" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row-div clearfix" id="dvMentorTitle" runat="server">
                <div class="label-div w30">
                    Mentor Evaluation Title:
                </div>
                <div class="field-div1 w60">
                 <asp:Label ID="lblMentorReviewTitle" runat="server" Text=""></asp:Label>
                 <%-- <asp:TextBox ID="txtMentorReviewTitle" runat="server" Enabled="false" Width="500px"></asp:TextBox>--%>
                </div>
            </div>
         
            <div class="row-div clearfix">
                <div class="label-div w30">
                    Mentor Evaluation Description:
                </div>
                <div class="field-div1 w60">
                 <asp:Label ID="lblMentorEvaluationDescription" runat="server" Text=""></asp:Label>
                <%-- <asp:TextBox ID="txtMentorEvaluationDescription" runat="server" TextMode="MultiLine"
                        Style="resize: none" Enabled="false" Width="500px"></asp:TextBox>--%>
                </div>
            </div>
          
            <div class="row-div clearfix" >
            <div class="label-div w30">
                   &nbsp;
                </div>
                <div class="field-div1 w60">
                   <asp:Button ID="btnPopupBack" runat="server" Text="Back" class="submitBtn" Width="10%" />
                </div>
             
            </div>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
