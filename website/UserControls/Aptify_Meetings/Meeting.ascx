<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Meeting.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Meetings.MeetingControl" %>
<%@ Register Src="../Aptify_Forums/SingleForum.ascx" TagName="SingleForum" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Src="MeetingActionControl.ascx" TagName="MeetingActionControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--Amruta,IssueID 14380,Code to open register meeting session calendar in new tab --%>
<script language="javascript" type="text/javascript">

    function openNewWin(url) {

        var x = window.open(url);

        x.focus();

    }
</script>

<div class="content-container clearfix">
    <%--Nalini Issue 12436 date:01/12/2011--%>
    <div class="MeetingMainDiv">
        <div class="MeetingLeftDiv">
            <asp:Label ID="lblRegistrationResult" runat="server"></asp:Label>
            <div id="MeetingTitle" class="MeetingTitleDiv">
                <img alt="Web Image" src="" runat="server" id="imgWebImage" visible="false" />
                <asp:Label runat="server" ID="lblName" />
                <br />
                <div>
                    <rad:RadRating ID="RadRatingTotal" runat="server" Skin="Default" Enabled="false"
                        Precision="Exact">
                    </rad:RadRating>
                    <asp:Label ID="totalrating" runat="server" CssClass="Rdaratinglabel"></asp:Label>
                </div>
            </div>
            <div style="width: 50px;">
                &nbsp;</div>
            <div id="MeetingDetail" class="MeetingDetailDiv">
                <table width="98%">
                    <tr runat="server" visible="false" id="trSessionParent">
                        <td class="meeting-label">
                            Part of:
                        </td>
                        <td>
                            <asp:HyperLink runat="server" ID="lnkParent">
                                <asp:Label runat="server" ID="lblParent" /></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="meeting-label">
                            Date:
                        </td>
                        <td>
                            <asp:Label ID="lblDates" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="meeting-label">
                            Venue:
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblPlace" />&nbsp;
                            <asp:Label runat="server" ID="lblLocation" />&nbsp; <span>
                                <asp:Image runat="server" ID="VenueDirection" ImageUrl="~/images/get-dir.png" CssClass="MiddleImage" />&nbsp;<asp:HyperLink
                                    ID="linkVenueDirection" runat="server" Target="_blank">Get Directions</asp:HyperLink></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="meeting-label">
                            Total Price:
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPrice" runat="server"></asp:Label><asp:Label ID="lblMemSavings"
                                runat="server"></asp:Label>
                        </td>
                    </tr>
                   <%-- Amruta,IssueID 14380,Label to display user meeting registration status--%>
                    <tr id="trMetingRegStatus" runat="server" visible="false">
                        <td class="meeting-label">
                            Status:
                        </td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </td>
                    </tr>                     
                </table>
                <div>
                  <%-- Amruta,IssueID 14381,Label to display meeting registration status message --%>
                  <asp:Label ID="lblMeetingStatus" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </div>
                <asp:LinkButton ID="lnkNewMeeting" runat="server"></asp:LinkButton><div class="MeetingWebDescriptionDiv">
                    <asp:Label runat="server" ID="lblWebDescription" />
 <asp:Label runat="server" ID="lblWebLongDescription" />
                </div>
                <div>
                    <span id="SpanRate" runat="server" class="tableHeaderFont" style="padding-left: 2px !important;">
                        Your Rating</span>
                    <rad:RadRating ID="RadmeetingRate" runat="server" Skin="Default" AutoPostBack="true"
                        Precision="Half">
                    </rad:RadRating>
                </div>
                <%--suraj Issue 14397, 3/7/13 Add  (limit 140 characters) text in heading --%>
                <span id="SpanShare" class="tableHeaderFont" style="padding-left: 2px !important;">Share
                    with your social networks (limit 140 characters)</span>
                <%--suraj Issue 14397, 3/7/13 Remove other social share which is not working properly theser are myspace ,Bloger ,Delicious,StumbleUpon,Reddit --%>
                <rad:RadSocialShare ID="RadSocialShareMeetings" runat="server" Skin="Default">
                    <MainButtons>
                        <rad:RadSocialButton SocialNetType="ShareOnFacebook"></rad:RadSocialButton>
                        <rad:RadSocialButton SocialNetType="ShareOnTwitter"></rad:RadSocialButton>
                        <rad:RadCompactButton></rad:RadCompactButton>
                    </MainButtons>
                    <CompactButtons>
                        <rad:RadSocialButton SocialNetType="LinkedIn"></rad:RadSocialButton>
                        <rad:RadSocialButton SocialNetType="GoogleBookmarks"></rad:RadSocialButton>
                        <rad:RadSocialButton SocialNetType="Tumblr"></rad:RadSocialButton>
                        <rad:RadSocialButton SocialNetType="MailTo"></rad:RadSocialButton>
                    </CompactButtons>
                </rad:RadSocialShare>
            </div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div id="SpeakerDetails">
                        &nbsp;
                        <table width="100%" class="data-form">
                            <tr>
                                <td class="SpeakerDetailHeader" style="padding-left: 50px;">
                                    Speaker Details
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div runat="server" id="pnlSpeakers">
                                        <asp:Label runat="server" ID="lblSpeakers" Visible="false"></asp:Label>
                                        <rad:RadGrid ID="grdSpeakers" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                            AllowFilteringByColumn="true">
                                            <GroupingSettings CaseSensitive="false" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                                <Columns>
                                                    <rad:GridTemplateColumn HeaderText="First Name" AllowFiltering="true" DataField="FirstName"
                                                        SortExpression="FirstName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Font-Size="10pt" />
                                                    </rad:GridTemplateColumn>
                                                    <rad:GridTemplateColumn HeaderText="Last Name" AllowFiltering="true" DataField="LastName"
                                                        SortExpression="LastName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Font-Size="10pt" />
                                                    </rad:GridTemplateColumn>
                                                    <rad:GridTemplateColumn HeaderText="Title" AllowFiltering="true" DataField="Title"
                                                        SortExpression="Title" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                                                    </rad:GridTemplateColumn>
                                                    <rad:GridBoundColumn DataField="Type" HeaderText="Type" AllowFiltering="true" SortExpression="Type"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                </Columns>
                                            </MasterTableView>
                                        </rad:RadGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="ScheduleDetails">
                        &nbsp;
                        <table width="100%" class="data-form">
                            <tr>
                                <td class="SheduleDetailHeader" style="padding-left: 50px;">
                                    Schedule Details
                                </td>
                            </tr>
                            <%--Amruta,IssueId 14380,Button to display user register session in calendar--%>
                            <tr>
                                <td>
                                    <asp:Label ID="lblInfo" runat="server" Text="Select session for meeting registration"
                                        CssClass="RegistrationInfo1"></asp:Label>
                                        <asp:Button runat="server" ID="btnMySessionCalendar" Text="Show My Session Calendar" CssClass="submitBtn" Visible="false"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div runat="server" id="pnlSchedule">
                                        <asp:Label runat="server" ID="lblSchedule" Visible="false"></asp:Label>
                                        <rad:RadGrid ID="grdSchedule" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                            AllowFilteringByColumn="true" AllowSorting="true">
                                            <GroupingSettings CaseSensitive="false" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                                                <Columns>
                                                    <%--anil Issue 14381--%>
                                                    <rad:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                        <HeaderStyle HorizontalAlign="left" Width="60px" />
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllSession" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                                AutoPostBack="True" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSession" runat="server" />
                                                        </ItemTemplate>
                                                    </rad:GridTemplateColumn>
                                                    <%-- <rad:GridTemplateColumn HeaderText="Session" AllowFiltering="true" DataField="WebName">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkWebName" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingUrl") %>'></asp:HyperLink></ItemTemplate>
                                                    <ItemStyle Font-Size="10pt" />
                                               </rad:GridTemplateColumn>--%>
                                                    <rad:GridTemplateColumn HeaderText="Session" AllowFiltering="true" DataField="WebName"
                                                        SortExpression="WebName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingUrl") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Font-Size="10pt" />
                                                    </rad:GridTemplateColumn>
                                                    <rad:GridDateTimeColumn HeaderText="Start Date" DataField="StartDate" AllowFiltering="true"
                                                        FilterControlWidth="100px" SortExpression="StartDate" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" DataType="System.DateTime"
                                                        EnableTimeIndependentFiltering="True" UniqueName="GridDateTimeColumnStartDate" />
                                                    <rad:GridDateTimeColumn HeaderText="End Date" DataField="EndDate" AllowFiltering="true"
                                                        FilterControlWidth="100px" SortExpression="EndDate" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" DataType="System.DateTime"
                                                        EnableTimeIndependentFiltering="True" UniqueName="GridDateTimeColumnEndDate" />
                                                    <rad:GridTemplateColumn HeaderText="Place" AllowFiltering="true" DataField="Location"
                                                        SortExpression="Location" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Location") %>'></asp:Label><asp:Label
                                                                ID="lblProductID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Font-Size="10pt" />
                                                    </rad:GridTemplateColumn>
                                                    <rad:GridBoundColumn DataField="Price" HeaderText="Price" HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" AllowFiltering="true"
                                                        SortExpression="Price" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false" />
                                                </Columns>
                                            </MasterTableView>
                                        </rad:RadGrid></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div>
                &nbsp;
                <table>
                    <tr runat="server" id="trRegister">
                        <td>
                            <%--   Dilip changes Issue 12721--%>
                            <%-- <asp:LinkButton ID="btnRegister" runat="server" Text="Register Individual" CausesValidation="false" CssClass="registerBtn" Font-Underline="false"
                                          />--%>
                            <%--RashmiP, Issue 14326 --%>
                            <%-- &nbsp;  &nbsp; <asp:LinkButton ID ="btnRegisterGroup" runat ="server" Text="Register Group" CausesValidation="false" CssClass="registerBtn" Font-Underline="false" >
                                        </asp:LinkButton>&nbsp;   &nbsp; <asp:LinkButton ID="btnBack" runat="server" Text="Back" Font-Bold="True" Font-Size="11pt" Font-Underline="true" /> --%>
                            <asp:Button runat="server" ID="btnRegister" Text="Register Individual" CssClass="submitBtn" />
                            <asp:Button runat="server" ID="btnRegisterGroup" Text="Register Group" CssClass="submitBtn" />
                            <asp:Button runat="server" ID="btnBack" Text="Back" CssClass="submitBtn" />
                        </td>
                    </tr>                    
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="ForumDiv" runat="server">
                        <%-- Dilip changes--%>
                        &nbsp;
                        <table width="100%">
                            <tr>
                                <td class="DiscussionForumHeader">
                                    Discussion Forum for&nbsp;<asp:Label ID="lblProductName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div runat="server" id="pnlForum">
                                        <asp:Label runat="server" ID="lblForum" Visible="false"></asp:Label><br />
                                        <uc1:SingleForum ID="SingleForum" runat="server" />
                                        <p>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="SingleForum" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="MeetingSepDiv">
            &nbsp;
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="MeetingRightDiv">
                    <div runat="server" id="Div1">
                        <table width="100%">
                            <tr class="PeopleAtMeetingHeader">
                                <td class="tdPeopleAtMeetingHeader">
                                    People at Meeting
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--  Dilip changes Issue 12721--%>
                                    <div class="RightPaneScrollBorder" style="padding-left: 5px;">
                                        <asp:Panel ID="pnl" runat="server">
                                            <asp:Label runat="server" ID="lblPeopleYouMayKnow" Visible="False"></asp:Label>
                                            <asp:Repeater ID="repPeopleYouMayKnow" runat="server">
                                                <ItemTemplate>
                                                    <div style="width: 85%; margin: 0.2em 0.1em 0.4em 0.2em;">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td><%--  Neha changes Issue 16001,05/07/13--%>
                                                                <div class="imgmember">
                                                                    <rad:RadBinaryImage ID="imgProfileRad" runat="server" AutoAdjustImageControlSize="false"
                                                                        ResizeMode="Fill" CssClass="PeopleImage" />
                                                                        </div>
                                                                </td>
                                                                <td valign="top">
                                                                <div style="padding-left: 10px;">
                                                                    <asp:HyperLink ID="lnkName" runat="server" CssClass="PeopleLink"></asp:HyperLink><asp:HyperLink
                                                                        ID="RelatedPersonCompanyName" runat="server" Visible="false"></asp:HyperLink><asp:CheckBox
                                                                            ID="chkPersonDirExclude" runat="server" Visible="false" />
                                                                    <asp:CheckBox ID="chkCompanyDirExclude" runat="server" Visible="false" />
                                                                      </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <hr class="PeopleSeparator" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="TravelDiscount" style="margin-top: 1em;">
                        <table width="100%">
                            <tr class="TravelDiscountHeader">
                                <td class="tdTravelDiscountHeader">
                                    Travel Discounts
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="RightPaneBorder" style="padding-left: 5px;">
                                        <asp:Label runat="server" ID="lblTravel" Visible="false"></asp:Label><asp:Repeater
                                            ID="repTravelDiscounts" runat="server">
                                            <ItemTemplate>
                                                <table width="98%" style="font-size: 11px; margin: 4px;">
                                                    <tr>
                                                        <td style="font-weight: bold;">
                                                            <%--  dilip issue 12721--%>
                                                            Hotel:
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblHotelName" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblGroupOffer" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%--  dilip issue 12721 For Decrease the length of from--%>
                                                        <%--<td style="font-weight: bold;">
                                                            From:</td>--%><td>
                                                                <b>From:</b>&nbsp;<asp:Label ID="lblStartDate" runat="server"></asp:Label>&nbsp;<b>To:</b>
                                                                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                                            </td>
                                                    </tr>
                                                </table>
                                                <hr class="PeopleSeparator" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="RelatedEvents">
                        <table width="100%" style="font-size: 11px;">
                            <tr class="RelatedEventsHeader" runat="server" id="RelatedEventsHeader">
                                <td class="tdRelatedEventsHeader" runat="server" id="tdRelatedEventsHeader">
                                    Related Events
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="RightPaneBorder" style="padding-left: 5px;" runat="server" id="RightPaneBorder">
                                        <asp:Label ID="lblRelatedEvents" runat="server"></asp:Label><asp:Repeater ID="repRelatedEvents"
                                            runat="server">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkNewProductName" runat="server"></asp:LinkButton><table>
                                                    <tr>
                                                        <td style="font-weight: bold; width: 43%">
                                                            Category
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCategory" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold; width: 43%">
                                                            Description
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold; width: 43%">
                                                            Location
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold; width: 43%">
                                                            Start Date & Time :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold; width: 43%">
                                                            End Date & Time :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold; width: 43%">
                                                            Registration Price :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRegPrice" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelSessionConflictError" runat="server">
            <ContentTemplate>
                <rad:RadWindow ID="radErrorMessage" runat="server" Modal="True" Height="175px" Width="500px" CssClass="winMeetingConflicts"
                    Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
                    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
                    <ContentTemplate>
                        <div style="height: 100px; overflow: auto">
                            <asp:ListView ID="lstErrorMessage" runat="server">
                                <ItemTemplate>
                                    <asp:Label ID="lblErrorMessage" runat="server" Text='<% #eval("ErrorMessage") %>'></asp:Label>
                                    <br />
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <div style="height: 4px">
                        </div>
                        <div class="btnOkMeetingPage">
                            <asp:Button ID="btnPopUpOk" runat="server" Text="OK" Width="50px" class="submitBtn"
                                ValidationGroup="ok" />&nbsp;&nbsp;
                        </div>
                    </ContentTemplate>
                </rad:RadWindow>
            </ContentTemplate>
        </asp:UpdatePanel>
        <cc1:User ID="User1" runat="server" />
        <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Width="47px" Height="14px"
            Visible="False"></cc2:AptifyShoppingCart>
    </div>
