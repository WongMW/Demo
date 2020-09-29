<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/Meeting__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Meetings.Meeting__c" %>
<%@ Register Src="../Aptify_Forums/SingleForum.ascx" TagName="SingleForum" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Meetings/MeetingActionControl.ascx" TagName="MeetingActionControl" TagPrefix="uc1" %>--%>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--Amruta,IssueID 14380,Code to open register meeting session calendar in new tab --%>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc3" TagName="RecordAttachments__c" %>
<%@ Register TagPrefix="uc4" TagName="RelatedProductsGrid" Src="../Aptify_Product_Catalog/RelatedProductsGrid.ascx" %>
<%@ Register TagPrefix="uc5" TagName="TopicCodesGrid" Src="../Aptify_Product_Catalog/ProductTopicCodesGrid.ascx" %>

<style>
    .ICETBLabelVal-price .product-price {
        line-height: 0.9 !important;
    }
    /* BAD PRACTICE TO PUT STYLE IN PAGE */
</style>

<%--Product Catlog Performance--%>
<asp:HiddenField ID="hdnPerson" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdnProduct" runat="server" ClientIDMode="Static" />

<%-- Susan Wong, Ticket #18964 start - Add load screen --%>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <div class="loading-bg">
                    <img src="/Images/CAITheme/bx_loader.gif" />
                    <span>LOADING...<br />
                        <br />
                        Please do not leave or close this window while the request is processing.</span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<%-- Susan Wong, Ticket #18964 end - Add load screen --%>
<%-- Susan Wong, Ticket #18964 start - Add load screen --%>
<asp:UpdatePanel ID="Updatepnl" runat="server">
    <ContentTemplate>
        <!-- START content-container -->
        <div id="outerdiv" class="product-wrapper">
            <div>
                <asp:Label ID="lblError" Style="font-weight: bold; color: red;" runat="server" Text=""></asp:Label>
            </div>
            <div id="ProdDetails" runat="server" class="product-container clearfix cai-table">
                <!-- Product Image -->
                <div class="product-image-section">
                    <div class="product-image">
                        <%-- Add backend code same as Product__c.ascx.vb to get this working  --%>
                        <asp:Image ID="imgProduct" runat="server" CssClass="Imgproduct schema-prod-img"></asp:Image>
                        <img alt="Item Not Available" id="imgNotAvailable" src="" visible="false" runat="server" style="margin-left: 3px;" />
                    </div>
                </div>
                <!-- Product Top Details -->
                <div class="product-details">
                    <div id="MeetingTitle" class="MeetingTitleDiv">
                        <h1>
                            <asp:Label runat="server" ID="lblName" CssClass="schema-prod-name" /></h1>
                    </div>
                    <div class="field-group">
                        <%-- Amruta,IssueID 14380,Label to display user meeting registration status--%>
                        <div id="trMetingRegStatus" runat="server" visible="false" class="ICETBLabel">
                            <span style="font-weight: bold;">Status:</span>
                            <asp:Label ID="lblStatus" runat="server" Style="font-weight: bold; color: green;" CssClass="schema-eventStatus"></asp:Label>
                        </div>
                    </div>
                    <div class="field-group">
                        <asp:Label ID="lblMeetingStatus" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                        <asp:LinkButton ID="lnkNewMeeting" runat="server"></asp:LinkButton>
                    </div>
                    <div class="field-group">
                        <div>
                            <%--Rating section made invisble by Sheela as part of Task #19380--%>
                            <span id="SpanRate" runat="server" class="tableHeaderFont" visible="false">Your rating</span>
                            <rad:RadRating ID="RadmeetingRate" runat="server" Skin="Default" AutoPostBack="true" Precision="Half" Visible="false"></rad:RadRating>
                        </div>
                    </div>
                    <div class="cai-form-content">
                        <div class="overview-label">Summary</div>
                        <div class="short-desc MeetingWebDescriptionDiv">
                            <asp:Label runat="server" ID="lblWebDescription" CssClass="schema-prod-description" />
                        </div>
                        <div style="clear: both; float: right;">
                            <!--Share on Social Media-->
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

                        <div id="MeetingTitle2" style="display: none;">
                            <img alt="Web Image" src="" runat="server" id="imgWebImage" visible="false" style="display: none;" />
                            <div class="overview-label" style="clear: both; float: left; display: none;">Ratings: &nbsp;</div>
                            <div>
                                <rad:RadRating ID="RadRatingTotal" runat="server" Skin="Default" Enabled="false" Precision="Exact" Style="float: left; line-height: 30px; display: none;"></rad:RadRating>
                                <asp:Label ID="totalrating" runat="server" CssClass="Rdaratinglabel" Style="float: left; line-height: 30px; display: none;"></asp:Label>
                            </div>
                        </div>
                        <div runat="server" visible="false" id="trSessionParent">
                            <span class="label-title">Part of:</span>
                            <asp:HyperLink runat="server" ID="lnkParent">
                                <asp:Label runat="server" ID="lblParent" />
                            </asp:HyperLink>
                        </div>
                        <div class="hide-from-thirdparty" style="clear: both;">
                            <div class="overview-label" style="float: left;">Venue details: &nbsp;</div>
                            <div style="line-height: 30px">
                                <asp:Label runat="server" ID="lblvenue" CssClass="schema-course-venue" Text="" />
                                <span>
                                    <asp:Label runat="server" ID="lblAddress1" Text="" CssClass="schema-course-streetAddress" />
                                    <asp:Label runat="server" ID="lblcity" Text="" CssClass="schema-course-addressRegion" />
                                </span>
                            </div>
                        </div>
                        <%--Modified as part of #20141 to seperate both start date and end date--%>
                        <div class="hide-from-thirdparty" style="clear: both;">
                            <div class="overview-label" style="float: left;">Start date & time: &nbsp;</div>
                            <div style="line-height: 30px">
                                <asp:Label ID="lblStartDate" runat="server" CssClass="schema-course-startDate" />
                            </div>
                        </div>
                        <div class="hide-from-thirdparty" style="clear: both;">
                            <div class="overview-label" style="float: left;">End date & time: &nbsp;</div>
                            <div style="line-height: 30px">
                                <asp:Label ID="lblEndDate" runat="server" CssClass="schema-course-endDate" />
                            </div>
                        </div>
                        <asp:Label ID="lblAdded" Visible="False" runat="server" ForeColor="Red"></asp:Label>
                        <!--	<div style="clear:both;" style="display:none;">
					<div class="overview-label" style="float: left;">Venue: &nbsp;</div>
					<div style="line-height: 30px">
						<asp:Label runat="server" ID="lblPlace" />
						<asp:Label runat="server" ID="lblLocation" />
						<div style="clear:both;padding-left:63px;">
							<i class="fa fa-map-marker" aria-hidden="true"></i>
							<asp:HyperLink ID="linkVenueDirection" runat="server" Target="_blank">Get directions</asp:HyperLink>
						</div>
					</div>
				</div> -->
                        <%--<div style="clear:both; ">
					<div class="overview-label" style="float: left; margin-bottom:20px;">
						<asp:CheckBox ID="ChkRequiredAgreement" runat="server" TextAlign="Left" Text=""/>
						<asp:Label ID="lblTicketCondtion" runat="server"></asp:Label>
					</div>
				</div>--%>
                    </div>
                    <div class="ICETBLabel-pricing">
                        <div class="ICETBLabelVal-price">
                            <span class="price-label">Price:</span>
                            <asp:Label ID="lblTotalPrice" CssClass="product-price schema-prod-price" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblMemSavings" runat="server" ForeColor="DarkGreen" Font-Bold="true"></asp:Label>
                            <%--Added as part of #20508--%>
                            <asp:Label ID="lblMemberPrice" runat="server" Text="" ForeColor="DarkGreen" CssClass="product-price schema-prod-mem-price"></asp:Label>
                        </div>
                        <div class="product-btnCart style-1">
                            <div runat="server" id="trRegister">
                                <%-- Amruta,IssueID 14381,Label to display meeting registration status message --%>
                                <%--   Dilip changes Issue 12721--%>
                                <%-- <asp:LinkButton ID="btnRegister" runat="server" Text="Register Individual" CausesValidation="false" CssClass="registerBtn" Font-Underline="false"/>--%>
                                <%--RashmiP, Issue 14326 --%>
                                <%-- &nbsp;  &nbsp; <asp:LinkButton ID ="btnRegisterGroup" runat ="server" Text="Register Group" CausesValidation="false" CssClass="registerBtn" Font-Underline="false" >
									</asp:LinkButton>&nbsp;   &nbsp; <asp:LinkButton ID="btnBack" runat="server" Text="Back" Font-Bold="True" Font-Size="11pt" Font-Underline="true" /> --%>
                                <asp:Button runat="server" ID="btnBack" Text="Back" CssClass="submitBtn cai-btn-red-inverse" />
                                <asp:Button runat="server" ID="btnRegister" Text="Book now" CssClass="submitBtn hide-from-thirdparty" />
                                <asp:Button runat="server" ID="btnRegisterGroup" Text="Register group" CssClass="submitBtn hide-from-thirdparty" />
                            </div>
                        </div>
                        <!-- Susan Wong: Ticket #19467 Move Ts and Cs to more noticible place -->
                        <div style="clear: both; text-align: right; padding-top: 5px;">
                            <asp:Label ID="Label1" runat="server"><span>By registering for this course you have accepted the <a href="/professional-development/Support-Services/CPD-access-and-cancellation-policy" target="_blank" style="font-weight:bold;">terms and conditions</a></span></asp:Label>
                        </div>
                    </div>
                    <div id="trIdTrainingPoint" runat="server" style="clear: both;" visible="false">
                        <div class="overview-label" style="float: left;">Training ticket cost: &nbsp;</div>
                        <div style="line-height: 30px">
                            <asp:Label ID="lblTrainingPoints" runat="server" Text="" Font-Bold="true"></asp:Label>
                            <%-- Susan Wong Ticket #21247 START --%>
                            <span class="tt-accepted"><i class="far fa-check-circle icon"></i>Training Tickets accepted</span>
                            <%-- Susan Wong Ticket #21247 END --%>
                        </div>
                    </div>
                    <div id="trCPDHours" runat="server" style="clear: both;" visible="false">
                        <div class="overview-label" style="float: left;">CPD hours: &nbsp;</div>
                        <div style="line-height: 30px">
                            <asp:Label ID="lblCpdHours" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <%-- Susan Wong, Ticket #18526 start - Moved product details up --%>
                    <!-- Product Bottom Details -->
                    <div class="product-full-details" style="width: 100%">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div id="ScheduleDetails" hidden="true">
                                    <span class="overview-label" style="border: none; padding: 0px; margin: 5px 0px;">Schedule details</span>
                                    <%--Amruta,IssueId 14380,Button to display user register session in calendar--%>
                                    <asp:Label ID="lblInfo" runat="server" Text="Select session for meeting registration" CssClass="RegistrationInfo1"></asp:Label>
                                    <div class="product-btnCart style-1">
                                        <asp:Button runat="server" ID="btnMySessionCalendar" Text="Show my session calendar" CssClass="submitBtn" Visible="false" />
                                    </div>
                                    <div runat="server" id="pnlSchedule">
                                        <asp:Label runat="server" ID="lblSchedule" Visible="false"></asp:Label>
                                        <rad:RadGrid ID="grdSchedule" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                            AllowFilteringByColumn="true" AllowSorting="true">
                                            <PagerStyle CssClass="sd-pager" />
                                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="true">
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
										    <ItemStyle/>
								       </rad:GridTemplateColumn>--%>
                                                    <rad:GridTemplateColumn HeaderText="Session" AllowFiltering="true" DataField="WebName"
                                                        SortExpression="WebName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WebName") %>'
                                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "MeetingUrl") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </rad:GridTemplateColumn>
                                                    <rad:GridDateTimeColumn HeaderText="Start date" DataField="StartDate" AllowFiltering="true"
                                                        FilterControlWidth="100px" SortExpression="StartDate" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" DataType="System.DateTime"
                                                        EnableTimeIndependentFiltering="True" UniqueName="GridDateTimeColumnStartDate" />
                                                    <rad:GridDateTimeColumn HeaderText="End date" DataField="EndDate" AllowFiltering="true"
                                                        FilterControlWidth="100px" SortExpression="EndDate" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" DataType="System.DateTime"
                                                        EnableTimeIndependentFiltering="True" UniqueName="GridDateTimeColumnEndDate" />
                                                    <rad:GridTemplateColumn HeaderText="Place" AllowFiltering="true" DataField="Location"
                                                        SortExpression="Location" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label><asp:Label
                                                                ID="lblProductID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle />
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
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div id="SpeakerDetails" style="width: 33%">
                                    <div>
                                        <span class="overview-label" style="border: none; padding: 0px; margin: 5px 0px;">Speaker details</span>
                                        <div runat="server" id="pnlSpeakers">
                                            <asp:Label runat="server" ID="lblSpeakers" Visible="false"></asp:Label>
                                            <rad:RadGrid ID="grdSpeakers" MasterTableView-AllowFilteringByColumn="false" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                                SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                AllowFilteringByColumn="true">
                                                <PagerStyle CssClass="sd-pager" />
                                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                                    <Columns>
                                                        <rad:GridTemplateColumn HeaderText="First name" ItemStyle-Width="20%" AllowFiltering="true" DataField="FirstName"
                                                            SortExpression="FirstName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ShowFilterIcon="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </rad:GridTemplateColumn>
                                                        <rad:GridTemplateColumn HeaderText="Last name" ItemStyle-Width="20%" AllowFiltering="true" DataField="LastName"
                                                            SortExpression="LastName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ShowFilterIcon="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </rad:GridTemplateColumn>
                                                        <rad:GridTemplateColumn HeaderText="Title" HeaderStyle-Width="220px" AllowFiltering="true" DataField="Title"
                                                            SortExpression="Title" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ShowFilterIcon="false" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </rad:GridTemplateColumn>
                                                        <rad:GridBoundColumn DataField="Type" HeaderText="Type" AllowFiltering="true" SortExpression="Type"
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" Visible="false" />
                                                    </Columns>
                                                </MasterTableView>
                                            </rad:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- START Product Long Description -->
                        <div class="overview-label">
                            <h2>Description</h2>
                            <%--Added as part of log #20491--%>
                            <div style="clear: both;">
                                <div class="overview-label" style="float: left;">Product type: &nbsp;</div>
                                <div style="line-height: 30px">
                                    <asp:Label ID="lblProductType" runat="server" />
                                </div>
                            </div>
                            <div style="clear: both;">
                                <div class="overview-label" style="float: left;">Category: &nbsp;</div>
                                <div style="line-height: 30px">
                                    <asp:Label ID="lblCategory" runat="server" CssClass="schema-prod-category" />
                                </div>
                            </div>
                            <%--end--%>
                            <div style="font-size: 16px !important; font-weight: normal; font-family: Source Sans Pro, sans-serif; display: block; clear: both">
                                <asp:Label runat="server" ID="lblWebLongDescription" />
                            </div>
                            <!-- #19451 Susan Wong: Add Topic codes to template -->
                            <uc5:TopicCodesGrid ID="TopicCodesGrid1" ShowHeaderIfEmpty="False" runat="server" />
                        </div>

                        <!-- END Product Long Description -->
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="MeetingRightDiv field-group" hidden="true">
                                    <div runat="server" id="Div1">
                                        <div class="PeopleAtMeetingHeader">
                                            <span class="label-title">People at meeting
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
                                                            <rad:RadBinaryImage ID="imgProfileRad" runat="server" AutoAdjustImageControlSize="false" ResizeMode="Fill" CssClass="PeopleImage" />
                                                        </div>
                                                        <div style="padding-left: 10px;">
                                                            <asp:HyperLink ID="lnkName" runat="server" CssClass="PeopleLink"></asp:HyperLink>
                                                            <asp:HyperLink ID="RelatedPersonCompanyName" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:CheckBox ID="chkPersonDirExclude" runat="server" Visible="false" />
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
                                        <asp:Label runat="server" ID="lblTravel" Visible="false"></asp:Label>
                                        <asp:Repeater ID="repTravelDiscounts" runat="server">
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
                                                Related events
                                            </div>
                                        </div>
                                        <div class="RightPaneBorder" style="padding-left: 5px;" runat="server" id="RightPaneBorder">
                                            <asp:Label ID="lblRelatedEvents" runat="server"></asp:Label>
                                            <asp:Repeater ID="repRelatedEvents" runat="server">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkNewProductName" runat="server"></asp:LinkButton>
                                                    <span style="font-weight: bold; width: 43%">Category</span>
                                                    <asp:Label ID="lblCategory" runat="server"></asp:Label>
                                                    <span style="font-weight: bold; width: 43%">Description</span>
                                                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                                    <span style="font-weight: bold; width: 43%">Location</span>
                                                    <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                                    <span style="font-weight: bold; width: 43%">Start date & time :</span>
                                                    <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                                                    <span style="font-weight: bold; width: 43%">End date & time :</span>
                                                    <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                                    <span style="font-weight: bold; width: 43%">Registration price :</span>
                                                    <asp:Label ID="lblRegPrice" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="clear: both;" id="trRecordAttachment" runat="server" visible="false">
                            <span class="overview-label" style="border: none; padding: 0px; margin: 5px 0px;">Documents</span>
                            <asp:Panel ID="Panel1" runat="Server">
                                <div runat="server" id="Table2" class="data-form" width="100%">
                                    <uc3:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True" AllowAdd="True" AllowDelete="false" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <%-- Susan Wong, Ticket #18526 end - Moved product details up --%>
                </div>

                <!-- Conflict Handling Update Panel -->
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">
                    <ContentTemplate>
                        <div class="cai-form">
                            <%-- Dilip changes--%>
                            <span class="form-title">Discussion forum for:
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
                <cc1:User ID="User1" runat="server" />
                <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Width="47px" Height="14px" Visible="False"></cc2:AptifyShoppingCart>
            </div>
        </div>
        <!-- END content-container -->
        <%-- Susan Wong, Ticket #18526 start - Moved featured product up --%>
        <div id="Products" class="vertical-layout">
            <div class="featured-products-slider-wrapper">
                <div class="featured-products-slider">
                    <uc4:RelatedProductsGrid ID="RelatedProductsGrid" HeaderText="{Related Product}" ShowHeaderIfEmpty="False" runat="server" />
                </div>
            </div>
        </div>
        <%-- Susan Wong, Ticket #18526 end - Moved featured product up --%>
    </ContentTemplate>
</asp:UpdatePanel>
<%-- Susan Wong, Ticket #18964 end - Add load screen --%>

<script language="javascript" type="text/javascript">

    function openNewWin(url) {

        var x = window.open(url);

        x.focus();

    }
    (function ($) {
        $('#baseTemplatePlaceholder_content_Meeting__c_lblWebDescription').each(function () {
            var text = $(this).text();
            if (text.length > 455) {
                text = text.substr(0, 455);
                text += "...";
                $(this).text(text);
            }
        });

        if ($('.schema-eventStatus').text() === "Not Avaliable") {
            $('.schema-eventStatus').css('color', '#8C1D40');
        }
    })(jQuery);
    // Susan Wong: Ticket #20232 If Aqua btn is visible then hide items
    if (!$('.product-details .cai-btn.cai-btn-aqua').length == 0) {
        $('.hide-from-thirdparty').css('display', 'none');
    }
    // media query event handler
    /*if (matchMedia) {
        var mq = window.matchMedia("(min-width: 801px)");
        mq.addListener(WidthChange);
        WidthChange(mq);
    }
    // media query change
    function WidthChange(mq) {
        if (mq.matches) {
            // window width is at least 900px
            //alert("Hello the window is 500px or bigger");
            $('.featured-products-2').css("display", "none");
            $('.featured-products-1').css("display", "block");
        } else {
            // window width is less than 900px
            $('.featured-products-2').css("display", "block");
            $('.featured-products-1').css("display", "none");
        }
    }*/
    // Susan Wong: Ticket #18740 hide related products when empty
    $(document).ready(function () {
        <% If btnRegister.Visible = True Then%>
        GetProductPrice();
        <% Else %>
        $('#<%= lblTotalPrice.ClientID%>').text("N/A");
        <% End If %>
        if ($('.featured-products-slider div.featured-products').length == 0) {
            $('.featured-products-slider-wrapper')
                .css("padding", "0px")
                .css("border", "0px");
        }
        //WongS, Modified for Ticket #20856
        if ($('#baseTemplatePlaceholder_content_Meeting__c_lblvenue').text().trim() === ",") {
            $('#baseTemplatePlaceholder_content_Meeting__c_lblvenue').text("");
        }
        if ($('#baseTemplatePlaceholder_content_Meeting__c_lblAddress1').text().trim() === ",") {
            $('#baseTemplatePlaceholder_content_Meeting__c_lblAddress1').text("");
        }
    });
    $('.skills-topiccodes>div>div>div>table>tbody>tr>td:nth-child(1)').each(function () {
        var Mycelltxt = $(this).text().trim();
        if (Mycelltxt.indexOf('1') >= 0) {
            $(this).html('<span class="skill-code skill-1-code">' + Mycelltxt + '</span>');
        }
        else if (Mycelltxt.indexOf('2') >= 0) {
            $(this).html('<span class="skill-code skill-2-code">' + Mycelltxt + '</span>');
        }
        else if (Mycelltxt.indexOf('3') >= 0) {
            $(this).html('<span class="skill-code skill-3-code">' + Mycelltxt + '</span>');
        }
        else if (Mycelltxt.indexOf('4') >= 0) {
            $(this).html('<span class="skill-code skill-4-code">' + Mycelltxt + '</span>');
        }
    });

    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }

    function GetProductPrice() {

        if ($('#baseTemplatePlaceholder_content_Meeting__c_lblName').html().toLowerCase() !== "event not available!") {
            var queryString = getUrlVars();
            var webmethod = "<%= Page.ResolveUrl("~/webservices/CourseEnrolments__c.asmx/GetProductPriceWithCampaign") %>";
            var personID = $('#hdnPerson').val();
            var productID = $('#hdnProduct').val();
            var campaignID = -1;
            if (queryString != null && queryString.length > 0) {
                if (typeof queryString["cID"] != 'undefined') {
                    campaignID = queryString["cID"];
                }
                if (typeof queryString["ID"] != 'undefined') {
                    productID = queryString["ID"];
                }
                if (typeof queryString["PersonID"] != 'undefined') {
                    personID = queryString["PersonID"];
                }
            }
            var parmeter = JSON.stringify({ 'ProductID': productID, 'CampaignID': campaignID, 'shipToID': personID, 'billToID': personID });

            $.ajax({
                type: "POST",
                url: webmethod,
                data: parmeter,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var sResponse = JSON.parse(response.d);

                    if (sResponse.length > 0) {
                        //Added as part of #20508
                        var returnPrice = sResponse.split("~")[0];
                        var returnProdID = sResponse.split("~")[1];
                        var returnMemberPrice = sResponse.split("~")[2];

                        if (typeof returnPrice !== "undefined") {
                            $('#<%= lblTotalPrice.ClientID%>').text(returnPrice);

                        }

                        if (typeof returnMemberPrice !== "undefined") {
                            $('#<%= lblMemberPrice.ClientID%>').text("(" + returnMemberPrice + " Member price)");
                            $('#<%= lblMemberPrice.ClientID%>').text($('#<%= lblMemberPrice.ClientID%>').text().replace('((', '(').replace(') M', ' M'))
                        }
                        else {
                            $('#<%= lblMemberPrice.ClientID%>').text("");
                        }

                    }
                },
                failure: function (msg) {
                    alert(msg);
                }
            });
        }
        else {
            $('#<%= lblTotalPrice.ClientID%>').text('N/A');
        }
    };

</script>
