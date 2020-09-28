<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Meetings/Meeting.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Meetings.MeetingControl" %>
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

<%-- Susan , 8/8/16 Added divs. class and inline styles to style page like Product__c control --%>
<!--<div class="cai-form clearfix">-->
<div id="outerdiv" class="product-wrapper">
<div id="ProdDetails" class="product-container clearfix cai-table">

<%-- Susan , 8/8/16 Added image section --%>
	<div class="product-image-section">
		<div class="product-image">
			<asp:Image ID="imgProduct" runat="server"  CssClass="Imgproduct"></asp:Image>
                </div>
	</div>

<%-- Susan , 8/8/16 Added divs to style page like Product__c control --%>
	<div class="product-details"><!-- START Product-Details container -->
    		<h6 class="textfont"><asp:Label runat="server" ID="lblName"/></h6><br />
		<div class="ICETBLabel">
                    <div id="trMetingRegStatus" runat="server" visible="false">
                        <span style="font-weight:bold;">Status:</span>
                        <asp:Label ID="lblStatus" runat="server" style="font-weight:bold;color:green;"></asp:Label>
                    </div>
                </div>

    <div class="cai-form-content">
        <div class="MeetingLeftDiv">
		<div class="overview-label">Summary</div>
		<div class="short-desc MeetingWebDescriptionDiv">
			<asp:Label runat="server" ID="lblWebDescription" />
 
		</div>

<div class="overview-label">More about this course</div>
		<div class="short-desc MeetingWebDescriptionDiv">
			
 <asp:Label runat="server" ID="lblWebLongDescription" />
		</div>
                <div style="clear:both; float: right;">
                    <%--suraj Issue 14397, 3/7/13 Add  (limit 140 characters) text in heading --%>
                    <span id="SpanShare" style="font-weight: bold;">Share this &nbsp;</span>
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

		<asp:Label ID="lblRegistrationResult" runat="server"></asp:Label>

            <div id="MeetingTitle">
                <img alt="Web Image" src="" runat="server" id="imgWebImage" visible="false" />
		<div class="overview-label" style="clear:both;float: left;">Ratings: &nbsp;</div
                <div>
                    <rad:RadRating ID="RadRatingTotal" runat="server" Skin="Default" Enabled="false" Precision="Exact" style="float: left;line-height: 30px;"></rad:RadRating> <asp:Label ID="totalrating" runat="server" CssClass="Rdaratinglabel" style="float: left;line-height: 30px;"></asp:Label>
                </div>
            </div>

            <div id="MeetingDetail" class="MeetingDetailDiv">
                <div>
                    <div runat="server" visible="false" id="trSessionParent">
                        <span class="label-title">Part of:</span>
                        <asp:HyperLink runat="server" ID="lnkParent">
                            <asp:Label runat="server" ID="lblParent" />
                        </asp:HyperLink>
                    </div>
                </div>
                <div style="clear:both;">
                    <div class="overview-label" style="float: left;">Date: &nbsp;</div>
                    <div style="line-height: 30px"><asp:Label ID="lblDates" runat="server" /></div>
                </div>

                <div style="clear:both;" hidden="true">
                    <div class="overview-label" style="float: left;">Venue: &nbsp;</div>
                    <div style="line-height: 30px">
			<asp:Label runat="server" ID="lblPlace" />
			<asp:Label runat="server" ID="lblLocation" />
                    	<div style="clear:both;padding-left:63px;">
                        	<!--<asp:Image runat="server" ID="VenueDirection" ImageUrl="~/images/get-dir.png" CssClass="MiddleImage" />-->
				<i class="fa fa-map-marker" aria-hidden="true"></i>
                        	<asp:HyperLink ID="linkVenueDirection" runat="server" Target="_blank">Get Directions</asp:HyperLink>
                    	</div>
                    </div>
                </div>

                <div class="ICETBLabel-pricing">
                    <div class="ICETBLabelVal-price">
                    	<span class="price-label">Price:</span>
                    	<asp:Label ID="lblTotalPrice" runat="server" CssClass="product-price"></asp:Label>
			<asp:Label ID="lblMemSavings" runat="server"></asp:Label>
                    </div>
                    <div class="product-btnCart style-1">
                	<div runat="server" id="trRegister">
                    		<%--   Dilip changes Issue 12721--%>
                    		<%-- <asp:LinkButton ID="btnRegister" runat="server" Text="Register Individual" CausesValidation="false" CssClass="registerBtn" Font-Underline="false"
                                          />--%>
                    		<%--RashmiP, Issue 14326 --%>
                    		<%-- &nbsp;  &nbsp; <asp:LinkButton ID ="btnRegisterGroup" runat ="server" Text="Register Group" CausesValidation="false" CssClass="registerBtn" Font-Underline="false" >
                                </asp:LinkButton>&nbsp;   &nbsp; <asp:LinkButton ID="btnBack" runat="server" Text="Back" Font-Bold="True" Font-Size="11pt" Font-Underline="true" /> --%>
                    		<asp:Button runat="server" ID="btnRegister" Text="Register Individual" CssClass="submitBtn" />
                    		<asp:Button runat="server" ID="btnRegisterGroup" Text="Register Group" CssClass="submitBtn" />
                    		<asp:Button runat="server" ID="btnBack" Text="Back" CssClass="submitBtn" visible="false" />
                	</div>
                    </div>
                </div>


                <div class="field-group">
                    <asp:Label ID="lblMeetingStatus" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    <asp:LinkButton ID="lnkNewMeeting" runat="server"></asp:LinkButton>
                </div>

                <div class="field-group">
                    <div>
                        <span id="SpanRate" runat="server" class="tableHeaderFont">Your Rating</span>
                        <rad:RadRating ID="RadmeetingRate" runat="server" Skin="Default" AutoPostBack="true" Precision="Half">
                        </rad:RadRating>
                    </div>
                </div>
            </div>
	</div></div><!-- END Product-Details container -->


	<div class="product-full-details"><!-- START Product-Full-Details container -->
                <div class="field-group">
                    <span class="overview-label">Description:</span>
                </div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div id="SpeakerDetails">
                        <div class="field-group">
                            <span class="overview-label" style="border:none;padding:0px;margin:5px 0px;">Speaker Details</span>

                            <div runat="server" id="pnlSpeakers">
                                <asp:Label runat="server" ID="lblSpeakers" Visible="false"></asp:Label>
                                <rad:RadGrid ID="grdSpeakers" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AllowFilteringByColumn="false">
                                    <PagerStyle CssClass="sd-pager" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="First Name" AllowFiltering="true" DataField="FirstName"
                                                SortExpression="FirstName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Last Name" AllowFiltering="true" DataField="LastName"
                                                SortExpression="LastName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Title" AllowFiltering="true" DataField="Title"
                                                SortExpression="Title" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </rad:GridTemplateColumn>
                                            <rad:GridBoundColumn DataField="Type" HeaderText="Type" AllowFiltering="true" SortExpression="Type"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        </Columns>
                                    </MasterTableView>
                                </rad:RadGrid>
                            </div>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="ScheduleDetails" class="field-group">
                        <span class="overview-label" style="border:none;padding:0px;margin:5px 0px;">Schedule Details</span>

                        <asp:Label ID="lblInfo" runat="server" Text="Select session for meeting registration"
                            CssClass="RegistrationInfo1"></asp:Label>
                    	<div class="product-btnCart style-1">
                        	<asp:Button runat="server" ID="btnMySessionCalendar" Text="Show My Session Calendar" CssClass="submitBtn" Visible="false" />
                    	</div>



                        <div runat="server" id="pnlSchedule">
                            <asp:Label runat="server" ID="lblSchedule" Visible="false"></asp:Label>
                            <rad:RadGrid ID="grdSchedule" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                AllowFilteringByColumn="true" AllowSorting="true">
                                <PagerStyle CssClass="sd-pager" />
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
                            </rad:RadGrid>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="MeetingRightDiv field-group" hidden="true">
                        <div runat="server" id="Div1">
                            <div class="PeopleAtMeetingHeader">
                                <span class="label-title">People at Meeting
                            </div>
                        </div>

                        <%--  Dilip changes Issue 12721--%>
                        <div class="RightPaneScrollBorder">
                            <asp:Panel ID="pnl" runat="server">
                                <asp:Label runat="server" ID="lblPeopleYouMayKnow" Visible="False"></asp:Label>
                                <asp:Repeater ID="repPeopleYouMayKnow" runat="server">
                                    <ItemTemplate>
                                        <div>
                                            <div class="imgmember">
                                                <rad:RadBinaryImage ID="imgProfileRad" runat="server" AutoAdjustImageControlSize="false"
                                                    ResizeMode="Fill" CssClass="PeopleImage" />
                                            </div>

                                            <div style="padding-left: 10px;">
                                                <asp:HyperLink ID="lnkName" runat="server" CssClass="PeopleLink"></asp:HyperLink><asp:HyperLink
                                                    ID="RelatedPersonCompanyName" runat="server" Visible="false"></asp:HyperLink><asp:CheckBox
                                                        ID="chkPersonDirExclude" runat="server" Visible="false" />
                                                <asp:CheckBox ID="chkCompanyDirExclude" runat="server" Visible="false" />
                                            </div>
                                        </div>
                                        <hr class="PeopleSeparator" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </div>
                    </div>

                    <div id="TravelDiscount" class="field-group" hidden="true">
                        <div class="TravelDiscountHeader">
                            <span class="label-title">Travel Discounts</span>
                        </div>

                        <div class="RightPaneBorder">
                            <asp:Label runat="server" ID="lblTravel" Visible="false"></asp:Label><asp:Repeater
                                ID="repTravelDiscounts" runat="server">
                                <ItemTemplate>
                                    <span class="label-title">Hotel:</span>

                                    <asp:Label ID="lblHotelName" runat="server"></asp:Label>

                                    <asp:Label ID="lblGroupOffer" runat="server"></asp:Label>

                                    <b>From:</b>&nbsp;<asp:Label ID="lblStartDate" runat="server"></asp:Label>&nbsp;<b>To:</b>
                                    <asp:Label ID="lblEndDate" runat="server"></asp:Label>

                                    <hr class="PeopleSeparator" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                    <div id="RelatedEvents" class="field-group">
                        <div class="field-group">
                            <div class="RelatedEventsHeader" runat="server" id="RelatedEventsHeader">
                                <div class="tdRelatedEventsHeader" runat="server" id="tdRelatedEventsHeader">
                                    Related Events
                                </div>
                            </div>

                            <div class="RightPaneBorder" style="padding-left: 5px;" runat="server" id="RightPaneBorder">
                                <asp:Label ID="lblRelatedEvents" runat="server"></asp:Label><asp:Repeater ID="repRelatedEvents"
                                    runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkNewProductName" runat="server"></asp:LinkButton>
                                        <span style="font-weight: bold; width: 43%">Category
                                        </span>

                                        <asp:Label ID="lblCategory" runat="server"></asp:Label>

                                        <span style="font-weight: bold; width: 43%">Description
                                        </span>
                                        <asp:Label ID="lblDescription" runat="server"></asp:Label>

                                        <span style="font-weight: bold; width: 43%">Location</span>

                                        <asp:Label ID="lblLocation" runat="server"></asp:Label>

                                        <span style="font-weight: bold; width: 43%">Start Date & Time :</span>

                                        <asp:Label ID="lblStartDate" runat="server"></asp:Label>

                                        <span style="font-weight: bold; width: 43%">End Date & Time :
                                        </span>

                                        <asp:Label ID="lblEndDate" runat="server"></asp:Label>

                                        <span style="font-weight: bold; width: 43%">Registration Price :
                                        </span>
                                        <asp:Label ID="lblRegPrice" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
	</div><!-- END Product-Full-Details container -->
        </div>


        <asp:UpdatePanel ID="UpdatePanelSessionConflictError" runat="server">
            <ContentTemplate>
                <rad:RadWindow ID="radErrorMessage" runat="server" Modal="True" Height="175px" Width="500px" CssClass="winMeetingConflicts"
                    Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
                    ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
                    <ContentTemplate>
                        <div style="height: 100px; overflow: auto">
                            <asp:ListView ID="lstErrorMessage" runat="server">
                                <ItemTemplate>
                                    <asp:Label ID="lblErrorMessage" runat="server" Text='<% #Eval("ErrorMessage")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <div style="height: 4px">
                        </div>
                        <div class="btnOkMeetingPage">
                            <asp:Button ID="btnPopUpOk" runat="server" Text="OK" Width="50px" class="submitBtn" ValidationGroup="ok" />
                        </div>
                    </ContentTemplate>
                </rad:RadWindow>
            </ContentTemplate>
        </asp:UpdatePanel>
        <cc1:User ID="User1" runat="server" />
        <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Width="47px" Height="14px" Visible="False"></cc2:AptifyShoppingCart>
    </div>
</div>
</div>


<asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">
    <ContentTemplate>
        <div class="cai-form">
            <span class="form-title">Discussion Forum for:
                <asp:Label ID="lblProductName" runat="server"></asp:Label>
            </span>
            <div class="cai-form-content">
                <div id="ForumDiv" runat="server">
                    <div runat="server" id="pnlForum">
                        <asp:Label runat="server" ID="lblForum" Visible="false"></asp:Label>
                        <uc1:SingleForum ID="SingleForum" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="SingleForum" />
    </Triggers>
</asp:UpdatePanel>
