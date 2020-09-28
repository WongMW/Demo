'Aptify e-Business 5.5.1, July 2013
' Developer               Date Created/Modified           Description
' Asmita Ghodke           4-Sep-2013                      On page load displayed terms and conditions. Validate it on the lnkRegister click event. Flowed down the information
'                                                         on the order line.
'Rajesh Kardile           04/07/2014                      Change inline Query to Store procedure spGetMeetingProductDetails__c. add messge in Culture Strings
'Rajesh Kardile           04/19/2014                      Display Attachment Records.
'Rajesh Kardile           07/10/2014                      Replace Hard code "Pound" value from entity attribute
'Sheela Jarali            29/05/2018                      Task #19380: Extension of Task #19167 - Shopping process performance
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Applications.ProductSetup
Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports Aptify.Framework.ItemRatings
Imports Aptify.Framework.Application
Imports SoftwareDesign.GTM
Imports SoftwareDesign.GTM.Model
Imports Aptify.Framework.DataServices
Imports Aptify.Applications.OrderEntry
Imports System.ComponentModel
Imports System
Imports System.Net.Cache

Namespace Aptify.Framework.Web.eBusiness.Meetings
    Partial Class Meeting__c
        Inherits eBusiness.BaseUserControlAdvanced

        Private m_sView As String = ""
        'for back button page
        Protected Const ATTRIBUTE_MEETINGS_PAGE As String = "MeetingsButtonPage"

        Protected Const ATTRIBUTE_GENERAL_TITLE_MAGE_URL As String = "GeneralTitleImage"
        Protected Const ATTRIBUTE_TRAVEL_TITLE_IMAGE_URL As String = "TravelTitleImage"
        Protected Const ATTRIBUTE_SCHEDULE_TITLE_IMAGE_URL As String = "ScheduleTitleImage"
        Protected Const ATTRIBUTE_SPEAKERS_TITLE_IMAGE_URL As String = "SpeakersTitleImage"
        Protected Const ATTRIBUTE_FORUMS_TITLE_IMAGE_URL As String = "ForumTitleImage"
        Protected Const ATTRIBUTE_GRID_SCHEDULE_IMAGE_URL As String = "ScheduleGridHeaderImage"
        Protected Const ATTRIBUTE_GRID_SCHEDULE_MEETING_PAGE As String = "ScheduleGridMeetingPage"
        Protected Const ATTRIBUTE_PARENT_MEETING_PAGE As String = "ParentMeetingPage"
        'control name
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "Meeting"
        'SKB 12/1/2010 Issue 7294: People You May know functionality
        Protected Const ATTRIBUTE_PEOPLEYOUMAYKNOW_TITLE_IMAGE_URL As String = "PeopleYouMayKnowTitleImage"
        Protected Const ATTRIBUTE_PERSON_LISTING_PAGE As String = "PersonListingPage"
        Protected Const ATTRIBUTE_COMPANY_LISTING_PAGE As String = "CompanyListingPage"

        'Navin Prasad
        Dim m_lRecordID As String
        Protected Const ATTRIBUTE_PERSON_IMAGE_URL As String = "PersonImageURL"
        Protected Const ATTRIBUTE_PERSON_BLANK_IMG As String = "BlankImage"
        Protected Const ATTRIBUTE_PERSON_RDBLANK_IMG As String = "RadBlankImage"
        Protected Const ATTRIBUTE_PRODUCT_DISPLAY_PAGE As String = "ProductDisplayPage"
        Protected Const ATTRIBUTE_MEETING_DIRECTION_URL As String = "MeetingDirectionURL"
        ''Rashmi issue 14326
        Protected Const ATTRIBUTE_SELECT_REGISTRANTS As String = "MeetingRegistrationSelectRegistrant"

        Protected Const ATTRIBUTE_PROFILE_THUMBNAIL_WIDTH As String = "ProfileThumbNailWidth"
        Protected Const ATTRIBUTE_PROFILE_THUMBNAIL_HEIGHT As String = "ProfileThumbNailHeight"
        Protected Const ATTRIBUTE_ITEMRATINGURI As String = "ItemRatingURI"
        Protected Const ATTRIBUTE_MEETINGSOCIALSHARETEXT As String = "MeetingSocialShareText"
        'Sandeep Yadav Issue 13879
        Protected Const ATTRIBUTE_SESSION_DT As String = "dtSessionItems"

        'Sachin Kalyankar for Meetinglanding page 
        Protected Const ATTRIBUTE_EVENT_PAGE As String = "EventButtonPage"
        'Amruta Issue 14380
        Dim dtMyMeetingSession As DataTable
        Protected Const ATTRIBUTE_PRINT_MEETING_SESSIONS_PAGE As String = "MeetingSessionsPage"
        Protected Const ATTRIBUTE_CHECKED_SESSION As String = "MeetingSessionsCheckedvalues"
        Protected Const ATTRIBUTE_ALLSESSION = "Sessiondt"
        Protected Const ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL As String = "ImageNotAvailable"
        Protected Const ATTRIBUTE_ITEM_NOT_AVAILABLE_IMAGE_URL As String = "ItemNotAvailableImage"

        Dim _foundProduct As DataRow = Nothing
        Dim _foundPrice As Decimal = 0
        Dim _foundCurrency As String = "EUR"


        Protected dtMeeting As DataTable = New DataTable()


        Enum PeopleYouMayKnowColumn
            Person
            Company
            PersonDirExclude
            CompanyDirExclude
        End Enum
#Region "Meeting specific properties"

        Public Overridable Property MeetingDirectionURL() As String
            Get
                If Not ViewState(ATTRIBUTE_MEETING_DIRECTION_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_MEETING_DIRECTION_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_MEETING_DIRECTION_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        'Navin Prasad 

        ''' <summary>
        ''' ProductDisplay page url
        ''' </summary>
        Public Overridable Property ProductDisplayPage() As String
            Get
                If Not ViewState(ATTRIBUTE_PRODUCT_DISPLAY_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PRODUCT_DISPLAY_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PRODUCT_DISPLAY_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Property PersonImageURL() As String
            Get
                If ViewState.Item("PersonImageURL") IsNot Nothing Then
                    Return ViewState.Item("PersonImageURL").ToString()
                Else
                    Return ""
                End If

            End Get
            Set(ByVal value As String)
                ViewState.Item("PersonImageURL") = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Property BlankImage() As String
            Get
                If ViewState.Item("BlankImage") IsNot Nothing Then
                    Return ViewState.Item("BlankImage").ToString()
                Else
                    Return ""
                End If

            End Get
            Set(ByVal value As String)
                ViewState.Item("BlankImage") = value
            End Set
        End Property
        Public Property RadBlankImage() As String
            Get
                If ViewState.Item("RadBlankImage") IsNot Nothing Then
                    Return ViewState.Item("RadBlankImage").ToString()
                Else
                    Return ""
                End If

            End Get
            Set(ByVal value As String)
                ViewState.Item("RadBlankImage") = value
            End Set
        End Property

        ''' <summary>
        ''' GeneralTitleImage url
        ''' </summary>
        Public Overridable Property GeneralTitleImage() As String
            Get
                If Not ViewState(ATTRIBUTE_GENERAL_TITLE_MAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_GENERAL_TITLE_MAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_GENERAL_TITLE_MAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' TravelTitleImage url
        ''' </summary>
        Public Overridable Property TravelTitleImage() As String
            Get
                If Not ViewState(ATTRIBUTE_TRAVEL_TITLE_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_TRAVEL_TITLE_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_TRAVEL_TITLE_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ScheduleTitleImage url
        ''' </summary>
        Public Overridable Property ScheduleTitleImage() As String
            Get
                If Not ViewState(ATTRIBUTE_SCHEDULE_TITLE_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_SCHEDULE_TITLE_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_SCHEDULE_TITLE_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' SpeakersTitleImage url
        ''' </summary>
        Public Overridable Property SpeakersTitleImage() As String
            Get
                If Not ViewState(ATTRIBUTE_SPEAKERS_TITLE_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_SPEAKERS_TITLE_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_SPEAKERS_TITLE_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ForumTitleImage url
        ''' </summary>
        Public Overridable Property ForumTitleImage() As String
            Get
                If Not ViewState(ATTRIBUTE_FORUMS_TITLE_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_FORUMS_TITLE_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_FORUMS_TITLE_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' PeopleYouMayKnowImage url
        ''' </summary>
        Public Overridable Property PeopleYouMayKnowTitleImage() As String
            Get
                If Not ViewState(ATTRIBUTE_PEOPLEYOUMAYKNOW_TITLE_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PEOPLEYOUMAYKNOW_TITLE_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PEOPLEYOUMAYKNOW_TITLE_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' ScheduleGridHeaderImage url
        ''' </summary>
        Public Overridable Property ScheduleGridHeaderImage() As String
            Get
                If Not ViewState(ATTRIBUTE_GRID_SCHEDULE_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_GRID_SCHEDULE_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_GRID_SCHEDULE_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ScheduleGridMeeting Page url
        ''' </summary>
        Public Overridable Property ScheduleGridMeetingPage() As String
            Get
                If Not ViewState(ATTRIBUTE_GRID_SCHEDULE_MEETING_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_GRID_SCHEDULE_MEETING_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_GRID_SCHEDULE_MEETING_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ParentMeeting page url
        ''' </summary>
        Public Overridable Property ParentMeetingPage() As String
            Get
                If Not ViewState(ATTRIBUTE_PARENT_MEETING_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PARENT_MEETING_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PARENT_MEETING_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        ''' <summary>
        ''' ProfileThumbNailWidth
        ''' </summary>
        Public Overridable ReadOnly Property ProfileThumbNailWidth() As Integer
            Get
                Try
                    If Not String.IsNullOrEmpty(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_WIDTH)) Then
                        If Not IsNumeric(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_WIDTH)) Then
                            Throw New Exception("Incorrect entry for <Global>...<" & ATTRIBUTE_PROFILE_THUMBNAIL_WIDTH & ">, a numeric value is required. " &
                                                "Please confirm the entry is correctly input in the 'Aptify_UC_Navigation.config' file.")
                        Else
                            Return CInt(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_WIDTH))
                        End If
                    Else
                        Return 0
                    End If
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    Return 0
                End Try
            End Get
        End Property


        ''' <summary>
        ''' ProfileThumbNailHeight
        ''' </summary>
        Public Overridable ReadOnly Property ProfileThumbNailHeight() As Integer
            Get
                Try
                    If Not String.IsNullOrEmpty(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_HEIGHT)) Then
                        If Not IsNumeric(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_HEIGHT)) Then
                            Throw New Exception("Incorrect entry for <Global>...<" & ATTRIBUTE_PROFILE_THUMBNAIL_HEIGHT & ">, a numeric value is required. " &
                                                "Please confirm the entry is correctly input in the 'Aptify_UC_Navigation.config' file.")
                        Else
                            Return CInt(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_HEIGHT))
                        End If
                    Else
                        Return 0
                    End If
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    Return 0
                End Try
            End Get
        End Property

        ''' <summary>
        ''' Print Meeting Sessions Page url
        ''' </summary>
        Public Overridable Property MeetingSessionsPage() As String
            Get
                If Not ViewState(ATTRIBUTE_PRINT_MEETING_SESSIONS_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PRINT_MEETING_SESSIONS_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PRINT_MEETING_SESSIONS_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' ImageNotAvailable url
        ''' </summary>
        Public Overridable Property ImageNotAvailable() As String
            Get
                If Not ViewState(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ItemNotAvailableImage url
        ''' </summary>
        Public Overridable Property ItemNotAvailableImage() As String
            Get
                If Not ViewState(ATTRIBUTE_ITEM_NOT_AVAILABLE_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ITEM_NOT_AVAILABLE_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ITEM_NOT_AVAILABLE_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            If String.IsNullOrEmpty(Me.QueryStringRecordIDParameter) Then Me.QueryStringRecordIDParameter = "ID"
            If String.IsNullOrEmpty(Me.IsQueryStringEncrypted) Then Me.IsQueryStringEncrypted = False
            If String.IsNullOrEmpty(Me.SetControlRecordIDFromQueryString) Then Me.SetControlRecordIDFromQueryString = True

            MyBase.SetProperties()

            'Dilip Changes

            If String.IsNullOrEmpty(MeetingsButtonPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                MeetingsButtonPage = Me.GetLinkValueFromXML(ATTRIBUTE_MEETINGS_PAGE)
                If String.IsNullOrEmpty(MeetingsButtonPage) Then
                    Me.btnBack.Enabled = False
                    Me.btnBack.ToolTip = "Meetings Page property has not been set."
                End If

            End If
            'Sachin Kalyankar Meeting landing page 

            If String.IsNullOrEmpty(EventButtonPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                EventButtonPage = Me.GetLinkValueFromXML(ATTRIBUTE_EVENT_PAGE)
                If String.IsNullOrEmpty(EventButtonPage) Then
                    Me.btnBack.Enabled = False
                    Me.btnBack.ToolTip = "Event Page property has not been set."
                End If

            End If


            If String.IsNullOrEmpty(MeetingDirectionURL) Then
                MeetingDirectionURL = Me.GetLinkValueFromXML(ATTRIBUTE_MEETING_DIRECTION_URL)
            End If
            If String.IsNullOrEmpty(ProductDisplayPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ProductDisplayPage = Me.GetLinkValueFromXML(ATTRIBUTE_PRODUCT_DISPLAY_PAGE)
                If String.IsNullOrEmpty(ProductDisplayPage) Then
                    lnkNewMeeting.Enabled = False
                    lnkNewMeeting.ToolTip = "ProductDisplayPage property not set."
                End If
            End If
            If String.IsNullOrEmpty(PersonImageURL) Then
                PersonImageURL = Me.GetLinkValueFromXML(ATTRIBUTE_PERSON_IMAGE_URL)
            End If
            If String.IsNullOrEmpty(BlankImage) Then
                BlankImage = Me.GetLinkValueFromXML(ATTRIBUTE_PERSON_BLANK_IMG)
            End If
            If String.IsNullOrEmpty(RadBlankImage) Then
                RadBlankImage = Me.GetLinkValueFromXML(ATTRIBUTE_PERSON_RDBLANK_IMG)
            End If

            '(GRID DISPLAY)
            If String.IsNullOrEmpty(GeneralTitleImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                GeneralTitleImage = Me.GetLinkValueFromXML(ATTRIBUTE_GENERAL_TITLE_MAGE_URL)
            End If
            If String.IsNullOrEmpty(TravelTitleImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                TravelTitleImage = Me.GetLinkValueFromXML(ATTRIBUTE_TRAVEL_TITLE_IMAGE_URL)
            End If
            If String.IsNullOrEmpty(ScheduleTitleImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ScheduleTitleImage = Me.GetLinkValueFromXML(ATTRIBUTE_SCHEDULE_TITLE_IMAGE_URL)
            End If
            If String.IsNullOrEmpty(SpeakersTitleImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                SpeakersTitleImage = Me.GetLinkValueFromXML(ATTRIBUTE_SPEAKERS_TITLE_IMAGE_URL)
            End If
            If String.IsNullOrEmpty(ForumTitleImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ForumTitleImage = Me.GetLinkValueFromXML(ATTRIBUTE_FORUMS_TITLE_IMAGE_URL)
            End If
            If String.IsNullOrEmpty(ScheduleGridHeaderImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ScheduleGridHeaderImage = Me.GetLinkValueFromXML(ATTRIBUTE_GRID_SCHEDULE_IMAGE_URL)
            End If
            If String.IsNullOrEmpty(ScheduleGridMeetingPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ScheduleGridMeetingPage = Me.GetLinkValueFromXML(ATTRIBUTE_GRID_SCHEDULE_MEETING_PAGE)
                If String.IsNullOrEmpty(ScheduleGridMeetingPage) Then
                    grdSchedule.Enabled = False
                    grdSchedule.ToolTip = "ScheduleGridMeetingPage property not set."
                End If
            End If
            If String.IsNullOrEmpty(ParentMeetingPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ParentMeetingPage = Me.GetLinkValueFromXML(ATTRIBUTE_PARENT_MEETING_PAGE)
                If String.IsNullOrEmpty(ParentMeetingPage) Then
                    lnkParent.Enabled = False
                    lnkParent.ToolTip = "ParentMeetingPage propery not set."
                End If
            End If
            'if values are not provide directly or from the XML file, set default values for inherited properties since 
            'control requires them to properly function
            If String.IsNullOrEmpty(Me.QueryStringRecordIDParameter) Then Me.QueryStringRecordIDParameter = "ID"

            'SKB 12/1/2010 Issue 7294: People You May know functionality
            If String.IsNullOrEmpty(PeopleYouMayKnowTitleImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                PeopleYouMayKnowTitleImage = Me.GetLinkValueFromXML(ATTRIBUTE_PEOPLEYOUMAYKNOW_TITLE_IMAGE_URL)
            End If
            If String.IsNullOrEmpty(PersonListingPage) Then
                PersonListingPage = Me.GetLinkValueFromXML(ATTRIBUTE_PERSON_LISTING_PAGE)
                If String.IsNullOrEmpty(PersonListingPage) Then
                    'Navin Prasad 
                    '  Me.grdPeopleYouMayKnow.ToolTip = "PersonListingPage property has not been set."

                Else
                    'Navin Prasad Issue 11032
                    ' DirectCast(grdPeopleYouMayKnow.Columns(PeopleYouMayKnowColumn.Person), HyperLinkColumn).DataNavigateUrlFormatString = Me.PersonListingPage & "?ID={0}"
                End If
            End If
            If String.IsNullOrEmpty(CompanyListingPage) Then
                CompanyListingPage = Me.GetLinkValueFromXML(ATTRIBUTE_COMPANY_LISTING_PAGE)
                If String.IsNullOrEmpty(CompanyListingPage) Then
                    '  Me.grdPeopleYouMayKnow.ToolTip = "CompanyListingPage property has not been set."
                Else
                    'Navin Prasad Issue 11032
                    '  DirectCast(grdPeopleYouMayKnow.Columns(PeopleYouMayKnowColumn.Company), HyperLinkColumn).DataNavigateUrlFormatString = Me.CompanyListingPage & "?ID={0}"
                End If
            End If
            If String.IsNullOrEmpty(MeetingRegistrationSelectRegistrant) Then
                MeetingRegistrationSelectRegistrant = Me.GetLinkValueFromXML(ATTRIBUTE_SELECT_REGISTRANTS)
            End If
            'Amruta Issue 14380
            If String.IsNullOrEmpty(MeetingSessionsPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                MeetingSessionsPage = Me.GetLinkValueFromXML(ATTRIBUTE_PRINT_MEETING_SESSIONS_PAGE)
            End If
            If String.IsNullOrEmpty(ImageNotAvailable) Then
                'since value is the 'default' check the XML file for possible custom setting
                ImageNotAvailable = Me.GetLinkValueFromXML(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL)
            End If
            If String.IsNullOrEmpty(ItemNotAvailableImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ItemNotAvailableImage = Me.GetLinkValueFromXML(ATTRIBUTE_ITEM_NOT_AVAILABLE_IMAGE_URL)
                imgNotAvailable.Src = ItemNotAvailableImage
            End If

        End Sub

        Public ReadOnly Property SessionGrid() As Telerik.Web.UI.RadGrid
            Get
                Return Me.grdSchedule
            End Get
        End Property

        ''Rashmi P, issue 14326
        Public Overridable Property MeetingRegistrationSelectRegistrant() As String
            Get
                If Not ViewState(ATTRIBUTE_SELECT_REGISTRANTS) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_SELECT_REGISTRANTS))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_SELECT_REGISTRANTS) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overridable ReadOnly Property MeetingSocialShareText() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_MEETINGSOCIALSHARETEXT) Is Nothing Then
                    Return (ViewState.Item(ATTRIBUTE_MEETINGSOCIALSHARETEXT))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_MEETINGSOCIALSHARETEXT)

                    Return value
                End If
            End Get
        End Property

        Protected Overridable ReadOnly Property ItemRatingURI() As String

            Get
                If Not ViewState.Item(ATTRIBUTE_ITEMRATINGURI) Is Nothing Then
                    Return (ViewState.Item(ATTRIBUTE_ITEMRATINGURI))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_ITEMRATINGURI)

                    Return value
                End If
            End Get
        End Property
        'Navin Prasad Issue 11032

        'Protected Sub grdPeopleYouMayKnow_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdPeopleYouMayKnow.ItemDataBound
        '    ' Changes made for to allow encrypting and decrypting the URL.

        '    Try
        '        Dim type As ListItemType = e.Item.ItemType
        '        If (e.Item.ItemType = ListItemType.Item Or _
        '        e.Item.ItemType = ListItemType.AlternatingItem) Then
        '            If Not CBool(e.Item.Cells(PeopleYouMayKnowColumn.PersonDirExclude).Text) Then
        '                Dim lnkPerson As HyperLink
        '                Dim tempURL As String
        '                Dim index As Integer
        '                Dim sValue As String
        '                Dim separator As String()

        '                lnkPerson = CType(e.Item.Cells(PeopleYouMayKnowColumn.Person).Controls(0), HyperLink)
        '                tempURL = lnkPerson.NavigateUrl
        '                index = tempURL.IndexOf("=")
        '                sValue = tempURL.Substring(index + 1)
        '                separator = lnkPerson.NavigateUrl.Split(CChar("="))
        '                lnkPerson.NavigateUrl = separator(0)
        '                lnkPerson.NavigateUrl = lnkPerson.NavigateUrl & "="
        '                lnkPerson.NavigateUrl = lnkPerson.NavigateUrl & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
        '            Else
        '                'remove Hyperlink if exclude from Directory
        '                Dim lnk As HyperLink
        '                lnk = CType(e.Item.Cells(PeopleYouMayKnowColumn.Person).Controls(0), HyperLink)
        '                lnk.NavigateUrl = ""
        '                lnk.ForeColor = Drawing.Color.Black
        '                lnk.Font.Underline = False
        '            End If
        '            If Not CBool(e.Item.Cells(PeopleYouMayKnowColumn.CompanyDirExclude).Text) Then
        '                Dim lnkCompanies As HyperLink
        '                Dim tempURL As String
        '                Dim index As Integer
        '                Dim sValue As String
        '                Dim separator As String()

        '                lnkCompanies = CType(e.Item.Cells(PeopleYouMayKnowColumn.Company).Controls(0), HyperLink)
        '                tempURL = lnkCompanies.NavigateUrl
        '                index = tempURL.IndexOf("=")
        '                sValue = tempURL.Substring(index + 1)
        '                separator = lnkCompanies.NavigateUrl.Split(CChar("="))
        '                lnkCompanies.NavigateUrl = separator(0)
        '                lnkCompanies.NavigateUrl = lnkCompanies.NavigateUrl & "="
        '                lnkCompanies.NavigateUrl = lnkCompanies.NavigateUrl & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
        '            Else
        '                'remove Hyperlink if exclude from Directory
        '                Dim lnk As HyperLink
        '                lnk = CType(e.Item.Cells(PeopleYouMayKnowColumn.Company).Controls(0), HyperLink)
        '                lnk.NavigateUrl = ""
        '                lnk.ForeColor = Drawing.Color.Black
        '                lnk.Font.Underline = False
        '            End If
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try


        'End Sub
        ''' <summary>
        ''' PersonListing page url
        ''' </summary>
        Public Overridable Property PersonListingPage() As String
            Get
                If Not ViewState(ATTRIBUTE_PERSON_LISTING_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PERSON_LISTING_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PERSON_LISTING_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        'Dilip changes
        ''' <summary>
        ''' Meetings default page url
        ''' </summary>
        Public Overridable Property MeetingsButtonPage() As String
            Get
                If Not ViewState(ATTRIBUTE_MEETINGS_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_MEETINGS_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                'ViewState(ATTRIBUTE_MEETINGS_PAGE) = Me.FixLinkForVirtualPath(value)
                ViewState(ATTRIBUTE_MEETINGS_PAGE) = value
            End Set
        End Property
        'Sachin changes
        ''' <summary>
        ''' Event default page url
        ''' </summary>
        Public Overridable Property EventButtonPage() As String
            Get
                If Not ViewState(ATTRIBUTE_EVENT_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_EVENT_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                'ViewState(ATTRIBUTE_MEETINGS_PAGE) = Me.FixLinkForVirtualPath(value)
                ViewState(ATTRIBUTE_EVENT_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' Company page url
        ''' </summary>
        Public Overridable Property CompanyListingPage() As String
            Get
                If Not ViewState(ATTRIBUTE_COMPANY_LISTING_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_COMPANY_LISTING_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_COMPANY_LISTING_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
            LoadIdFromQuerysting()

            Dim sSQL As String = Database & "..spGetMeetingProductWebURL__c @ProductID=" & Convert.ToInt32(Me.ProductID)
            Dim sWebURL As String = Convert.ToString(DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            If sWebURL <> "" Then
                imgProduct.ImageUrl = "~" & sWebURL
            End If
            'LoadMeeting()
            'imgProduct.ImageUrl = "~/sf_images/default-source/Shop-Images/Shop-CPD-Courses/cpd-courses-shutterstock_242408485.jpg?Status=Temp&sfvrsn=6"
        End Sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'set control properties from XML file if needed
            ''RashmiP, Issue 14644, 11/20/12
            Try
                If Not IsPostBack Then
                    Session("MeetingSessions") = Nothing
                    SetProperties()
                    'Suraj issue 14457 3/5/13 ,this method use to apply the odrering of rad grid for speaker and schedule
                    AddExpression()
                    If Me.SetControlRecordIDFromParam() Then
                        Me.ProductID = Me.ControlRecordID
                    End If
                    If Me.ProductID > 0 Then
                        LoadMeeting()

                        'LoadSchedule()
                        'Commented by Sheela as part of Task #19380
                        'LoadForum()
                        'LoadTravel()
                        'LoadPeopleYouMayKnow()
                        If Not dtMeeting Is Nothing AndAlso dtMeeting.Rows.Count > 0 Then
                            CheckGroupAdmin()
                            SetupRegisterButton()
                            'LoadRating()
                            displayRecordAttachment(Me.ControlRecordID) 'Rajesh Kardile - 04/19/2014 - Display Attachment Records.
                        Else
                            LoadLayoutNotFound()
                            Exit Sub
                        End If
                    ElseIf Request.QueryString("ID") IsNot Nothing Then
                        ' only do this if query string was provided, otherwise we are in design time
                        'Throw New Exception("Security Validation Error - Invalid ID parameter")
                        LoadLayoutNotFound()
                        'Throw New HttpException(404, "Security Validation Error - Invalid ID parameter " & Request.QueryString("ID"))
                    End If
                    'Amruta IssueId 14380,19/3/2013, To get url of MyMeetingSessionPage
                    Dim url As String = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.ApplicationPath)) & MeetingSessionsPage & "?ID=" & Me.ProductID
                    btnMySessionCalendar.OnClientClick = "javascript: openNewWin('" & url & "'); return false;"
                    ' Code added by govind mande on 30/4/2014 for displaying Training Ticket Points
                    GetTrainingPointsdetails(Me.ProductID)
                    'code End by Govind
                    ''Added BY Pradip 2017-01-03 to display CPD Hours
                    GetCPDHoursdetails(Me.ProductID)

                    AddGoogleTagImpression()

                    Session("ShoppingCart") = ShoppingCart1

                    hdnPerson.Value = CStr(User1.PersonID)
                    hdnProduct.Value = CStr(Me.ProductID)
                    Me.lblTotalPrice.Visible = True
                    Me.lblMemSavings.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                LoadLayoutNotFound()
            End Try
            Me.Dispose()
        End Sub
        ''' <summary>
        ''' if Training Ticket points > 0 then only display message
        ''' </summary>
        ''' <param name="ProductID"></param>
        ''' <remarks></remarks>
        Private Sub GetTrainingPointsdetails(ByVal ProductID As Long)
            Try
                Dim sTrainingPoint As String = Database & "..spGetProductsTrainingTicketPoints__c @ProductID=" & ProductID
                Dim dTrainingPoint As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sTrainingPoint, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                If dTrainingPoint > 0 Then
                    lblTrainingPoints.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.TrainingTicketPointsMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & Math.Round(Convert.ToDecimal(dTrainingPoint), 2)
                    trIdTrainingPoint.Visible = True
                Else
                    trIdTrainingPoint.Visible = False
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadIdFromQuerysting()
            Me.ProductID = Num(Request.QueryString("ID").ToString())
        End Sub

        Private Sub LoadLayoutNotFound()
            Me.btnRegister.Visible = False
            Me.btnRegisterGroup.Visible = False
            Me.imgProduct.ImageUrl = ImageNotAvailable
            Me.lblError.Visible = True
            Me.lblName.Text = "Event not available!"
            Me.lblTotalPrice.Text = "N/A"
            'Me.ProdDetails.Visible = False
        End Sub

        Private Shared Function Num(ByVal value As String) As Integer
            Try
                value = value.Split("'")(0)
            Catch ex As Exception
                'value = Request.QueryString("ID").ToString().Split("'")(0)
            End Try

            Dim returnVal As String = String.Empty
            Dim collection As MatchCollection = Regex.Matches(value, "\d+")
            For Each m As Match In collection
                returnVal += m.ToString()
            Next

            Try
                Try
                    Return Convert.ToInt32(returnVal)
                Catch ex As Exception
                    Throw New System.Exception("Url error: " & HttpContext.Current.Request.Url.AbsoluteUri & " \n IP Adrees:" & System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(1).ToString())
                End Try
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return 0
            End Try

        End Function

        Private Sub GetCPDHoursdetails(ByVal ProductID As Long)
            Try
                Dim sCPDHours As String = Database & "..spGetMeetingCPDHours__c @ProductID=" & ProductID
                Dim dsCPDHours As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sCPDHours, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                If dsCPDHours > 0 Then
                    trCPDHours.Visible = True
                    lblCpdHours.Text = dsCPDHours.ToString
                Else
                    trCPDHours.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Nalini Nanda, Item Rating
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub LoadRating()
            Dim itemratinginputobj As New ItemRatingInput()
            itemratinginputobj.RatingValue = Convert.ToDecimal(RadmeetingRate.Value)
            itemratinginputobj.ItemRatingURI = ItemRatingURI.ToString() + Me.ProductID
            itemratinginputobj.PersonID = User1.PersonID
            itemratinginputobj.ItemRatingTypeURI = ""
            itemratinginputobj.RatedItemURI = ""
            Dim ItemRatingServiceInformationobj As New ItemRatingServiceInformation()
            ItemRatingServiceInformationobj = GetItemRating(itemratinginputobj)
            If ItemRatingServiceInformationobj.ItemRatingDetails IsNot Nothing Then
                If ItemRatingServiceInformationobj.ItemRatingDetails.PersonEntry Is Nothing Then
                    If ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage Is Nothing Then
                        RadRatingTotal.Value = 0
                        totalrating.Text = "(Total: " + "0" + ")"
                        RadmeetingRate.Value = 0
                    Else
                        If (ItemRatingServiceInformationobj.ItemRatingDetails.PersonEntry Is Nothing) Then
                            RadmeetingRate.Value = 0
                        Else
                            RadmeetingRate.Value = ItemRatingServiceInformationobj.ItemRatingDetails.PersonEntry.RecordedValue
                        End If

                        RadRatingTotal.Value = ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage
                        totalrating.Text = "(Total: " + Math.Round(Decimal.Parse(ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage), 1).ToString() + ")"
                    End If
                Else
                    If Not ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage Is Nothing Then
                        RadRatingTotal.Value = ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage
                        totalrating.Text = "(Total: " + Math.Round(Decimal.Parse(ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage), 1).ToString() + ")"
                        RadmeetingRate.Value = ItemRatingServiceInformationobj.ItemRatingDetails.PersonEntry.RecordedValue
                    Else
                        RadRatingTotal.Value = 0
                        totalrating.Text = "(Total: " + "0" + ")"
                        RadmeetingRate.Value = 0
                    End If
                End If
            End If


            If (User1.PersonID > 0) Then
                'Dim SSql As String = "select enddate from Meeting where ProductID=" + Request.QueryString("ID")
                'Dim dtSQL As DataTable = DataAction.GetDataTable(SSql, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If dtMeeting.Rows.Count > 0 Then
                    If dtMeeting.Rows(0).Item(0) < Date.Now Then
                        ''RashmiP, issue 14391 3/22/13 Fixed Scenarios where User is able to rate the Upcoming Events
                        Dim SSqlCountAttendee As String = "select AttendeeID  from vwordermeetdetail where productID=" + Me.ProductID.ToString() + " and AttendeeID=" + User1.PersonID.ToString() + "  and statusID in(select ID from vwAttendeeStatus where Name='Registered' or Name='Attended')"
                        Dim dtSSqlCountAttendee As DataTable = DataAction.GetDataTable(SSqlCountAttendee, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                        If (dtSSqlCountAttendee.Rows.Count() > 0) Then
                            RadmeetingRate.Visible = True
                            SpanRate.Visible = True
                        Else
                            RadmeetingRate.Visible = False
                            SpanRate.Visible = False
                        End If
                    Else
                        RadmeetingRate.Visible = False
                        SpanRate.Visible = False
                    End If
                Else
                    LoadLayoutNotFound()
                    lblError.Text = "Meeting not found " & Request.QueryString("ID").ToString()
                End If

            Else
                RadmeetingRate.Visible = False
                SpanRate.Visible = False
            End If

            ''If (User1.PersonID > 0) Then
            ''    'Dim SSql As String = "select enddate from vwMeetings where ProductID=" + Request.QueryString("ID")
            ''    Dim SSqlCountAttendee As String = "select AttendeeID  from vwordermeetdetail where productID=" + Request.QueryString("ID").ToString() + " and AttendeeID=" + User1.PersonID.ToString() + "  and statusID in(select ID from vwAttendeeStatus where Name='Registered' or Name='Attended')"
            ''    Dim dtSSqlCountAttendee As DataTable = DataAction.GetDataTable(SSqlCountAttendee, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ''    If (dtSSqlCountAttendee.Rows.Count() > 0) Then
            ''        RadmeetingRate.Visible = True
            ''        SpanRate.Visible = True
            ''    Else
            ''        RadmeetingRate.Visible = False
            ''        SpanRate.Visible = False
            ''    End If
            ''Else
            ''    RadmeetingRate.Visible = False
            ''    SpanRate.Visible = False
            ''End If

        End Sub

        Protected Sub grdSchedule_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdSchedule.ItemCreated
            Dim chkSubscription As CheckBox = DirectCast(e.Item.FindControl("chkSession"), CheckBox)
            Dim chkAllSessions As CheckBox
            Dim dicSubscriptionDetails As New Dictionary(Of Integer, Boolean)
            Dim iSessionID As Integer
            Dim dataItem As DataRowView
            If IsPostBack Then
                chkAllSessions = DirectCast(e.Item.FindControl("chkAllSession"), CheckBox)
                If chkAllSessions IsNot Nothing Then
                    If ViewState("CheckAll") IsNot Nothing And CBool(ViewState("CheckAll")) And chkAllSessions IsNot Nothing Then
                        chkAllSessions.Checked = True
                    Else
                        chkAllSessions.Checked = False
                    End If
                End If

            End If

            If chkSubscription IsNot Nothing Then
                If ViewState(ATTRIBUTE_CHECKED_SESSION) IsNot Nothing Then 'Added by Sandeep For Issue 14671 on 27/02/2013
                    dicSubscriptionDetails = DirectCast(ViewState(ATTRIBUTE_CHECKED_SESSION), Dictionary(Of Integer, Boolean))
                    dataItem = DirectCast(e.Item.DataItem, System.Data.DataRowView)
                    If dataItem IsNot Nothing Then
                        iSessionID = CInt(dataItem("ProductID"))
                        If dicSubscriptionDetails.ContainsKey(iSessionID) Then
                            chkSubscription.Checked = dicSubscriptionDetails.Item(iSessionID)
                        End If
                    End If
                End If
            End If
        End Sub

        Protected Sub grdSchedule_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdSchedule.NeedDataSource
            LoadSchedule()
        End Sub

        Protected Sub grdSpeakers_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdSpeakers.NeedDataSource
            LoadSpeakers()
        End Sub

        Private Sub LoadMeeting()
            ' load information for the meeting onto the screen
            Dim dt As DataTable, sSQL As String
            Dim strCurrencySymbol As String = String.Empty
            Dim strPrice As String = String.Empty
            Try
                ' Rajesh K - 04/07/2014
                '************************** Start ************************************
                'sSQL = "SELECT m.*,ISNULL(p.ParentID,-1) ParentID,p.IsRequiredAgreement__c,p.TermsAndConditionID__c, par.WebName ParentWebName, CASE ISNULL(m.State,'') WHEN '' THEN " & _
                '       "m.City + ', ' + m.Country ELSE m.City + ', ' + " & _
                '       "m.State + ' ' + m.Country END Location, p.WebName, " & _
                '       "p.WebDescription,p.WebLongDescription,p.WebImage, " & _
                '       "p.WebEnabled, p.DateAvailable, p.AvailableUntil, " & _
                '       "p.RequireInventory FROM " & _
                '       AptifyApplication.GetEntityBaseDatabase("Products") & _
                '       "..vwProducts p INNER JOIN " & _
                '       AptifyApplication.GetEntityBaseDatabase("Meetings") & _
                '       "..vwMeetings m ON p.ID=m.ProductID LEFT OUTER JOIN " & _
                '       AptifyApplication.GetEntityBaseDatabase("Products") & _
                '       "..vwProducts par on p.ParentID=par.ID  " & _
                '       " WHERE p.WebEnabled=1 AND m.ProductID=" & _
                '       Me.ControlRecordID 'must be parameterized
                'dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

                '#21000
                Dim msSQL = "Select * from " & AptifyApplication.GetEntityBaseDatabase("Products") & "..vwMeetings WHERE ProductID = " & CInt(Me.ControlRecordID)
                dtMeeting = Me.DataAction.GetDataTable(msSQL)

                If Not dtMeeting Is Nothing AndAlso dtMeeting.Rows.Count > 0 Then
                    Dim CurrencyType As String = "Euro"
                    If User1.UserID > 0 Then
                        If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                            CurrencyType = "Euro"
                            _foundCurrency = "EUR"
                            'RajeshK -071014
                        ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                            CurrencyType = AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c")
                            _foundCurrency = "GBP"
                        End If
                    End If
                    Dim sProdctsList As String = Database & "..spGetMeetingProductDetails__c"
                    Dim params(1) As System.Data.IDataParameter
                    params(0) = DataAction.GetDataParameter("@CurrencyType", SqlDbType.VarChar, CurrencyType)
                    params(1) = DataAction.GetDataParameter("@ProductID", SqlDbType.Int, Me.ControlRecordID)

                    dt = DataAction.GetDataTableParametrized(sProdctsList, CommandType.StoredProcedure, params)
                    '************************************* End ********************************************

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        _foundProduct = dt.Rows(0)


                        'Navin Prasad 

                        Dim dcol As DataColumn = New DataColumn()
                        dcol.Caption = "Price"
                        dcol.ColumnName = "Price"
                        dt.Columns.Add(dcol)
                        'Added as part of #20508
                        Dim dMembercol As DataColumn = New DataColumn()
                        dMembercol.Caption = "MemberPrice"
                        dMembercol.ColumnName = "MemberPrice"
                        dt.Columns.Add(dMembercol)
                        'If dt.Rows.Count > 0 Then
                        '    Dim oPrice As New Aptify.Applications.OrderEntry.IProductPrice.PriceInfo
                        '    For Each rw As DataRow In dt.Rows
                        '        oPrice = Me.ShoppingCart1.GetUserProductPrice(CLng(rw("ProductID")))
                        '        _foundPrice = oPrice.Price
                        '        rw("Price") = Format(oPrice.Price, User1.PreferredCurrencyFormat)
                        '        'Added as part of #20508
                        '        If User1.UserID <= 0 Then
                        '            Dim icheck As Integer = 0
                        '            Dim ssSQL As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductCategory__c @ProductID=" & CLng(rw("ProductID"))
                        '            icheck = CInt(DataAction.ExecuteScalar(ssSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        '            If icheck = 1 Then
                        '                Dim sFormat As String = ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID)
                        '                Dim shipToID As Long, billToID As Long
                        '                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                        '                Dim sMemberPrice As String = String.Empty
                        '                Dim MemberPersonId As Long = Convert.ToInt64(AptifyApplication.GetEntityAttribute("Persons", "MemberPersonID__c"))
                        '                billToID = MemberPersonId
                        '                shipToID = MemberPersonId
                        '                If billToID > 0 And shipToID > 0 Then
                        '                    oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), Aptify.Applications.OrderEntry.OrdersEntity)
                        '                    oOrder.BillToID = billToID
                        '                    oOrder.ShipToID = shipToID
                        '                    'oOrder.SetValue("CampaignCodeID", CampaignID)
                        '                    oOrder.AddProduct(CLng(dt.Rows(0).Item("ProductID")))
                        '                    sMemberPrice = Format(Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended")), sFormat)

                        '                    oOrder = Nothing
                        '                End If
                        '                rw("MemberPrice") = sMemberPrice
                        '            End If
                        '        End If

                        '    Next
                        'End If


                        With dt.Rows(0)

                            'Member Savings:


                            ViewState("MeetingStartDate") = .Item("StartDate")
                            ViewState("MeetingEndDate") = .Item("EndDate")
                            '  SetupViewType()

                            If CLng(.Item("ParentID")) > 0 Then
                                lblParent.Text = CStr(.Item("ParentWebName"))
                                lnkParent.NavigateUrl = ParentMeetingPage & "?View=Schedule&ID=" & CStr(.Item("ParentID"))
                                trSessionParent.Visible = True
                                lblStartDate.Text = CDate(.Item("StartDate")).ToLongDateString & " - " &
                                                CDate(.Item("StartDate")).ToShortTimeString & " to " &
                                                CDate(.Item("EndDate")).ToShortTimeString
                            Else
                                trSessionParent.Visible = False
                                '#20141: Displaying start date and end date as seperate labels 
                                lblStartDate.Text = CDate(.Item("StartDate")).ToLongDateString & " " & CDate(.Item("StartDate")).ToShortTimeString
                                lblEndDate.Text = CDate(.Item("EndDate")).ToLongDateString & " " & CDate(.Item("EndDate")).ToShortTimeString
                            End If
                            'Added by sandeep for 15132 on 7/2/2013
                            'Changes made for removing currency symbol so that it will not create any problem while conversion
                            'sSQL = "SELECT CurrencySymbol  FROM " & AptifyApplication.GetEntityBaseDatabase("Currency Types") & ".." & AptifyApplication.GetEntityBaseView("Currency Types") & " WHERE ID=" & User1.PreferredCurrencyTypeID & ""
                            'strCurrencySymbol = DataAction.ExecuteScalar(sSQL)
                            'strPrice = Convert.ToString(.Item("Price"))
                            'If strCurrencySymbol IsNot Nothing AndAlso strCurrencySymbol.Trim().Length > 0 Then
                            '    strPrice = strPrice.Replace(strCurrencySymbol.Trim(), "")
                            'End If
                            'If CLng(strPrice) = 0 Then
                            '    lblTotalPrice.Text = "Free"
                            'Else
                            '    If .Item("MemberPrice") <> .Item("Price") Then
                            '        lblTotalPrice.Text = Format(.Item("Price"), ShoppingCart1.GetCurrencyFormat(2))
                            '        'Added as part of #20508
                            '        lblMemberPrice.Text = "(" + .Item("MemberPrice") + " Member price)"
                            '        lblMemberPrice.Visible = True
                            '    Else
                            '        lblTotalPrice.Text = Convert.ToString(.Item("Price"))
                            '    End If
                            'End If

                            Page.Title = Convert.ToString(.Item("WebName"))    'Swapnil 17/10/2016
                            lblName.Text = .Item("WebName").ToString.Trim
                            lblProductName.Text = lblName.Text

                            'Commented and added by Swati for issue #16796 Product - Strip web description html tags
                            'lblWebDescription.Text = .Item("WebDescription").ToString.Trim
                            lblWebDescription.Text = .Item("WebDescription").ToString.Trim
                            lblWebDescription.Text = GetBodyHTML(lblWebDescription.Text).Trim()

                            lblPlace.Text = .Item("Place").ToString.Trim
                            lblLocation.Text = .Item("Location").ToString.Trim
                            linkVenueDirection.NavigateUrl = MeetingDirectionURL + "?ID=" + CStr(.Item("ProductID"))

                            'Commented and added by Swati for issue #16796 Product - Strip web description html tags
                            'lblWebLongDescription.Text = .Item("WebLongDescription").ToString.Trim
                            lblWebLongDescription.Text = .Item("WebLongDescription").ToString.Trim
                            lblWebLongDescription.Text = GetBodyHTML(lblWebLongDescription.Text).Trim()

                            lblvenue.Text = .Item("Venue").ToString.Trim + CStr(", ")
                            lblAddress1.Text = .Item("AddressLine1").ToString.Trim + CStr(", ")
                            lblcity.Text = .Item("City").ToString.Trim
                            'Added as part of #20491
                            Dim CategoryWebName As String = .Item("CategoryWebName").ToString().Trim()

                            If Not String.IsNullOrEmpty(CategoryWebName) Then
                                lblProductType.Text = CategoryWebName
                            Else
                                lblProductType.Text = "--"
                            End If
                            Dim sProdSQL As String = String.Empty
                            sProdSQL = Me.Database & "..spGetProdMultiCategoriesWithNoSubCategory__c @ProductID=" & Me.ControlRecordID
                            Dim sCategories As String = Convert.ToString(DataAction.ExecuteScalar(sProdSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If Not String.IsNullOrEmpty(sCategories) Then
                                lblCategory.Text = sCategories
                            Else
                                lblCategory.Text = "--"
                            End If
                            'End Of #20491
                            ' SetupView(dt.Rows(0))
                            RadSocialShareMeetings.UrlToShare = Request.RawUrl.ToString()
                            RadSocialShareMeetings.TitleToShare = MeetingSocialShareText.ToString() + .Item("WebName").ToString.Trim + " on " + CDate(.Item("StartDate")).ToLongDateString

                            ' Added by Asmita Ghodke on 1/4/14
                            'LoadTermsAndCondtion()
                            '

                            'Sachin Sathe added below code for redmine #16115
                            If Not IsDBNull(.Item("WebImage")) AndAlso
                                Len(.Item("WebImage")) > 0 Then
                                imgProduct.ImageUrl() = "~" & CStr(.Item("WebImage"))

                            Else
                                imgProduct.ImageUrl = ImageNotAvailable
                            End If
                            If imgProduct.ImageUrl Is Nothing Or imgProduct.ImageUrl = "" Or imgProduct.Visible = False Then
                                Me.imgProduct.ImageUrl = ImageNotAvailable
                                Me.imgProduct.Visible = True
                            End If



                            'End
                        End With
                    Else
                        LoadLayoutNotFound()
                        dtMeeting = New DataTable()
                        'Throw New HttpException(404, "The requested Meeting " & Request.QueryString("ID") & " was not found.")
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                LoadLayoutNotFound()
            End Try
        End Sub
        'Navin Prasad
        Private Sub SetupView(ByVal dr As DataRow)

            'pnlGeneral.Visible = False
            'pnlForum.Visible = False
            'pnlSchedule.Visible = False
            'pnlSpeakers.Visible = False
            'pnlTravel.Visible = False
            'pnlPeopleYouMayKnow.Visible = False
            'Select Case m_sView
            '    Case "General"

            '        lblWebLongDescription.Text = dr("WebLongDescription").ToString
            '        pnlGeneral.Visible = True
            '        lblTitle.Text = "General Information"
            '        imgTitle.ImageUrl = GeneralTitleImage
            '    Case "Schedule"

            '        pnlSchedule.Visible = True
            '        LoadSchedule()
            '        lblTitle.Text = "Schedule"
            '        imgTitle.ImageUrl = ScheduleTitleImage
            '    Case "Speakers"

            '        pnlSpeakers.Visible = True
            '        LoadSpeakers()
            '        lblTitle.Text = "Speakers"
            '        imgTitle.ImageUrl = SpeakersTitleImage
            '    Case "Travel"

            '        pnlTravel.Visible = True
            '        LoadTravel()
            '        lblTitle.Text = "Travel Information"
            '        imgTitle.ImageUrl = TravelTitleImage
            '    Case "Forum"

            '        pnlForum.Visible = True
            '        LoadForum()
            '        lblTitle.Text = "Discussion Forum for " & dr("WebName").ToString
            '        imgTitle.ImageUrl = ForumTitleImage

            '    Case "PeopleYouMayKnow"
            '        pnlPeopleYouMayKnow.Visible = True
            '        LoadPeopleYouMayKnow()
            '        lblTitle.Text = "People You May Know at " & dr("WebName").ToString
            '        imgTitle.ImageUrl = PeopleYouMayKnowTitleImage
            'End Select


        End Sub
        Private Sub SetupViewType()
            If Request.QueryString("View") IsNot Nothing Then
                Select Case Request.QueryString("View").Trim.ToUpper
                    Case "SCHEDULE"
                        m_sView = "Schedule"
                    Case "SPEAKERS"
                        m_sView = "Speakers"
                    Case "TRAVEL"
                        m_sView = "Travel"
                    Case "FORUM"
                        If Me.User1.PersonID > 0 Then
                            m_sView = "Forum"
                        Else
                            m_sView = "General"
                        End If
                    Case "PEOPLEYOUMAYKNOW"
                        m_sView = "PeopleYouMayKnow"
                    Case Else
                        m_sView = "General"
                End Select
            Else
                m_sView = "General"
            End If
        End Sub
        Private Sub LoadSchedule()
            Dim dt As DataTable, sSQL As String, sDB As String, lID As Long
            Dim sMeetingView As String = ""
            Dim sProductView As String = ""
            Try
                lID = CLng(Me.ProductID)
                ''Modified By Sheela as part of Task #19380 (Replaced inline query to SP)
                'sDB = AptifyApplication.GetEntityBaseDatabase("Meetings")
                'Anil B for Issue 14381
                'Change query to display web name
                'sMeetingView = AptifyApplication.GetEntityBaseView("Meetings")
                'sProductView = AptifyApplication.GetEntityBaseView("Products")
                'Anil B for issue 15384 on 08/04/2013
                'Add condition for Web Enabled

                'sSQL = "SELECT P.WebName WebName,m.ProductID,m.StartDate StartDate,m.EndDate EndDate ,m.Place Location FROM " & _
                '       sDB & ".." & sMeetingView & " m inner join " & sDB & ".." & sProductView & " P on m.ProductID=P.id " & _
                '       "WHERE " & _
                '       " dbo.fnProductLevelsBelow(m.ProductID," & _
                '       lID & ")>0 AND p.WebEnabled=1 ORDER BY m.StartDate,m.EndDate,m.MeetingTitle "
                'dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                sSQL = AptifyApplication.GetEntityBaseDatabase("Meetings") & "..spGetMeetingSchedule__c"
                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("@lID", SqlDbType.Int, lID)
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)

                'Navin Prasad Issue 12428

                Dim dcol As DataColumn = New DataColumn()
                Dim dcolStartDate As DataColumn = New DataColumn()
                Dim dcolEndDate As DataColumn = New DataColumn()
                Dim dcolUrl As DataColumn = New DataColumn()
                dcol.Caption = "Price"
                dcol.ColumnName = "Price"
                dcolUrl.Caption = "MeetingUrl"
                dcolUrl.ColumnName = "MeetingUrl"
                dt.Columns.Add(dcol)
                dt.Columns.Add(dcolStartDate)
                dt.Columns.Add(dcolEndDate)
                dt.Columns.Add(dcolUrl)
                dcolStartDate.Caption = "nStartDate"
                dcolStartDate.ColumnName = "nStartDate"
                dcolEndDate.Caption = "nEndDate"
                dcolEndDate.ColumnName = "nEndDate"
                If dt.Rows.Count > 0 Then
                    Dim oPrice As New Aptify.Applications.OrderEntry.IProductPrice.PriceInfo
                    For Each rw As DataRow In dt.Rows
                        oPrice = Me.ShoppingCart1.GetUserProductPrice(CLng(rw("ProductID")))
                        If oPrice.Price = 0 Then
                            rw("Price") = "Free "
                        Else
                            rw("Price") = Format(oPrice.Price, User1.PreferredCurrencyFormat)
                        End If
                        rw("nStartDate") = String.Format("{0:MMMM dd, yyyy hh:mm tt }", rw("StartDate"))
                        rw("nEndDate") = String.Format("{0:MMMM dd, yyyy hh:mm tt }", rw("EndDate"))
                        If String.Format("{0:hh:mm tt}", rw("StartDate")) = "12:00 AM" Then
                            rw("nStartDate") = Convert.ToString(String.Format("{0:MMMM dd, yyyy}", rw("StartDate")))
                        End If
                        If String.Format("{0:hh:mm tt}", rw("EndDate")) = "12:00 AM" Then
                            rw("nEndDate") = Convert.ToString(String.Format("{0:MMMM dd, yyyy}", rw("EndDate")))
                        End If
                        rw("MeetingUrl") = ScheduleGridMeetingPage + "?ID=" + rw("ProductID").ToString
                    Next
                    Me.grdSchedule.DataSource = dt
                    If ViewState(ATTRIBUTE_ALLSESSION) Is Nothing Then
                        ViewState(ATTRIBUTE_ALLSESSION) = dt
                    End If
                    lblSchedule.Visible = False
                    grdSchedule.Visible = True
                Else
                    lblSchedule.Visible = True
                    lblSchedule.Text = "No sessions have been associated with this event."
                    grdSchedule.Visible = False
                End If

                'Amruta Issue 14380,19/3/2013,Query to get users meetings and session details
                Dim iProductID As Integer
                If Request.QueryString("ID") IsNot Nothing Then
                    iProductID = Me.ProductID
                End If

                sSQL = Nothing
                ''Modified By Sheela as part of Task #19380 (Replaced inline query to SP)
                'sSQL = "SELECT OM.ProductID,OM.ID,OM.OrderID,OM.AttendeeStatus_Name,OM.AttendeeID_Name,P.ID from " & AptifyApplication.GetEntityBaseDatabase("OrderMeetingDetail") & ".." & AptifyApplication.GetEntityBaseView("OrderMeetingDetail") & " OM INNER JOIN " & AptifyApplication.GetEntityBaseDatabase("Products") & ".." & AptifyApplication.GetEntityBaseView("Products") & " P ON OM.ProductID = P.ID " &
                '      " WHERE OM.AttendeeID=" & User1.PersonID & " and P.ID = " & iProductID & " UNION " & _
                '      " SELECT OM.ProductID,OM.ID,OM.OrderID,OM.AttendeeStatus_Name,OM.AttendeeID_Name,P.ID from " & AptifyApplication.GetEntityBaseDatabase("OrderMeetingDetail") & ".." & AptifyApplication.GetEntityBaseView("OrderMeetingDetail") & " OM INNER JOIN " & AptifyApplication.GetEntityBaseDatabase("Products") & ".." & AptifyApplication.GetEntityBaseView("Products") & " P ON OM.ProductID = P.ID " &
                '      " WHERE OM.AttendeeID=" & User1.PersonID & " and P.ParentID = " & iProductID

                'dtMyMeetingSession = DataAction.GetDataTable(sSQL)

                Dim AttendeeID As Long = -1
                AttendeeID = User1.PersonID
                sSQL = AptifyApplication.GetEntityBaseDatabase("OrderMeetingDetail") & "..spGetMeetingSessionDetails__c"
                Dim param1(1) As IDataParameter
                param1(0) = DataAction.GetDataParameter("@AttendeeID", SqlDbType.Int, AttendeeID)
                param1(1) = DataAction.GetDataParameter("@productID", SqlDbType.Int, iProductID)
                dtMyMeetingSession = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param1)


                If dtMyMeetingSession IsNot Nothing Then
                    For Each rw As DataRow In dtMyMeetingSession.Rows
                        If Request.QueryString("ID") IsNot Nothing And Me.ProductID = rw("ProductID").ToString() Then
                            trMetingRegStatus.Visible = True
                            lblStatus.Text = rw("AttendeeStatus_Name").ToString()
                            If lblStatus.Text = "Cancelled" And ViewState("MeetingEndDate") >= System.DateTime.Now().ToShortDateString() Then
                                btnRegister.Visible = True
                            ElseIf lblStatus.Text = "Registered" And ViewState("MeetingEndDate") >= System.DateTime.Now().ToShortDateString() Then
                                btnRegister.Visible = True
                                btnRegister.Text = "Register Another Person"
                                btnMySessionCalendar.Visible = True
                            ElseIf lblStatus.Text = "Attended" And ViewState("MeetingEndDate") >= System.DateTime.Now().ToShortDateString() Then
                                btnRegister.Visible = False
                                btnMySessionCalendar.Visible = True
                            ElseIf lblStatus.Text = "Waiting" And ViewState("MeetingEndDate") >= System.DateTime.Now().ToShortDateString() Then
                                btnRegister.Visible = True
                                btnRegister.Text = "Register Another Person"
                                btnMySessionCalendar.Visible = True
                            End If
                        End If
                    Next
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadTravel()
            Dim dt As DataTable, sSQL As String, sDB As String, mID As Long
            Try
                mID = CInt(dtMeeting.Rows(0)("MeetingID"))
                sDB = AptifyApplication.GetEntityBaseDatabase("Meetings")
                sSQL = "SELECT * FROM " & sDB & "..vwMeetingHotels WHERE MeetingID=" & mID & " ORDER BY HotelID_Name"
                dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    'Navin Prasad Issue 
                    Me.repTravelDiscounts.DataSource = dt
                    Me.repTravelDiscounts.DataBind()
                    lblTravel.Visible = False
                    repTravelDiscounts.Visible = True
                Else
                    lblTravel.Visible = True
                    lblTravel.Text = "No hotels have been associated with this event."
                    repTravelDiscounts.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        '11/30/2010 SKB Issue 7294 people you may know grid functionality
        Private Sub LoadPeopleYouMayKnow()
            Dim dt As DataTable, sSQL As String, sDB As String, lID As Long
            Try
                lID = Me.ControlRecordID
                sDB = AptifyApplication.GetEntityBaseDatabase("Meetings")
                sSQL = "Exec " & sDB & ".dbo.spPeopleYouMayKnow " & User1.PersonID & ", " & lID & " "
                dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    'Navin Prasad
                    'Me.grdPeopleYouMayKnow.DataSource = dt
                    'Me.grdPeopleYouMayKnow.DataBind()
                    'lblPeopleYouMayKnow.Visible = False
                    'grdPeopleYouMayKnow.Visible = True
                    repPeopleYouMayKnow.DataSource = dt
                    repPeopleYouMayKnow.DataBind()
                    lblPeopleYouMayKnow.Visible = False
                    repPeopleYouMayKnow.Visible = True
                Else
                    lblPeopleYouMayKnow.Visible = True
                    lblPeopleYouMayKnow.Text = "People you know have not been associated with this event."
                    'grdPeopleYouMayKnow.Visible = False
                    repPeopleYouMayKnow.Visible = False

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadSpeakers()
            ''Modified By Sheela as part of Task #19380 (Replaced inline query to SP) 
            Dim dt As DataTable, sSQL As String, sDB As String, lID As Long, Parent As String
            Try
                lID = Me.ControlRecordID
                'sDB = AptifyApplication.GetEntityBaseDatabase("Meetings")
                'sSQL = "SELECT m.MeetingTitle Session,m.StartDate,m.EndDate,ms.Title,ms.Description,ms.Type," & _
                '       "p.LastName,p.FirstName FROM " & sDB & _
                '       "..vwMeetingSpeakers ms INNER JOIN " & sDB & _
                '       "..vwPersons p ON ms.SpeakerID=p.ID INNER JOIN " & _
                '       sDB & "..vwMeetings m ON ms.MeetingID=m.ID " & _
                '       "WHERE ms.Status IN ('Accepted','Completed') AND "
                ''HP Issue#8516:  when at the top level meeting extract all speakers for all child sessions, 
                ''                otherwise when at a session only extract speakers for that session.
                'If String.IsNullOrEmpty(lblParent.Text) Then
                '    sSQL &= " dbo.fnProductLevelsBelow(m.ProductID," & lID & ")>=0 "
                'Else
                '    sSQL &= "m.ProductID =" & lID & " "
                'End If
                'sSQL &= "ORDER BY m.MeetingTitle,p.LastName,p.FirstName"
                Parent = lblParent.Text
                sSQL = AptifyApplication.GetEntityBaseDatabase("Meetings") & "..spGetSpeakersForMeeting__c"
                Dim param(1) As IDataParameter
                param(0) = DataAction.GetDataParameter("@Parent", SqlDbType.VarChar, Parent)
                param(1) = DataAction.GetDataParameter("@lID", SqlDbType.Int, lID)
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Me.grdSpeakers.DataSource = dt
                    ''Me.grdSpeakers.DataBind()
                    lblSpeakers.Visible = False
                    grdSpeakers.Visible = True
                Else
                    lblSpeakers.Visible = True
                    lblSpeakers.Text = "No speakers have been associated with this event."
                    grdSpeakers.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadForum()
            Dim lForumID As Long = Me.SingleForum.GetLinkedForumID("Products", Me.ControlRecordID)
            If lForumID > 0 Then
                SingleForum.LoadForum(lForumID)
            Else
                Dim sError As String = ""
                If SingleForum.CreateNewLinkedForum("Meeting Forum - " &
                                                     lblName.Text,
                                                     "Forum for Meeting, " &
                                                     "Product ID: " &
                                                     Me.ControlRecordID,
                                                     Now, CDate(ViewState("MeetingEndDate")),
                                                     "Products",
                                                     Me.ControlRecordID,
                                                     lForumID, sError) Then
                    SingleForum.LoadForum(lForumID)
                Else
                    lblForum.Text = "No discussion forum is currently set up for this meeting. An attempt was made to establish a new forum for this meeting but it did not succeed. " & sError
                    lblForum.Visible = True
                    SingleForum.Visible = False
                End If
            End If
        End Sub
        ''RashmiP, issue 14326
        Sub CheckGroupAdmin()

            If User1.PersonID <= 0 Then
                btnRegisterGroup.Visible = False
                Exit Sub
            End If

            Dim sSQL As String
            Dim IsGroupAdmin As Boolean

            Try
                'Modified By Sheela as part of Task #19380 (Replaced inline query to SP)
                'sSQL = "SELECT IsGroupAdmin FROM VWPERSONS WHERE ID = " & User1.UserID
                'IsGroupAdmin = CBool(DataAction.ExecuteScalar(sSQL))

                'Susan Wong, Ticket #20231 - new user group
                sSQL = AptifyApplication.GetEntityBaseDatabase("WebUser") & "..spCheckIsGroupRegistrationAdmin__c"
                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("@UserID", SqlDbType.Int, User1.UserID)
                IsGroupAdmin = CBool(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, param))

                If IsGroupAdmin Then
                    btnRegisterGroup.Visible = True
                Else
                    btnRegisterGroup.Visible = False
                End If

            Catch ex As Exception

            End Try


        End Sub

        ''' <summary>
        ''' Nalini issue 12436,date:1/12/2011
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub grdSchedule_PageIndexChanging(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdSchedule.PageIndexChanged

            LoadSchedule()
            SaveCheckedValues()
        End Sub

        Protected Sub grdSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles grdSchedule.PageSizeChanged
            LoadSchedule()
            SaveCheckedValues()
        End Sub
        Private Sub SaveCheckedValues()
            Dim dicMeetingSessions As New Dictionary(Of Integer, Boolean)
            Dim index As Integer = -1
            Dim result As Boolean

            For Each item As Telerik.Web.UI.GridDataItem In grdSchedule.MasterTableView.Items
                index = CInt(DirectCast(item.FindControl("lblProductID"), Label).Text)
                result = DirectCast(item.FindControl("chkSession"), CheckBox).Checked
                If ViewState(ATTRIBUTE_CHECKED_SESSION) IsNot Nothing Then
                    dicMeetingSessions = DirectCast(ViewState(ATTRIBUTE_CHECKED_SESSION), Dictionary(Of Integer, Boolean))
                End If
                If dicMeetingSessions.ContainsKey(index) Then
                    dicMeetingSessions.Remove(index)
                End If
                dicMeetingSessions.Add(index, result)
            Next
            If dicMeetingSessions IsNot Nothing AndAlso dicMeetingSessions.Count > 0 Then
                ViewState(ATTRIBUTE_CHECKED_SESSION) = dicMeetingSessions
            End If
        End Sub


        ''' <summary>
        ''' Nalini issue 12436,date:1/12/2011
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub grdSpeakers_PageIndexChanging(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdSpeakers.PageIndexChanged

            LoadSpeakers()
        End Sub

        Protected Sub grdSpeakers_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles grdSpeakers.PageSizeChanged
            LoadSpeakers()
        End Sub


        ' ''' <summary>
        ' ''' Nalini issue 12436 date:1/12/2011
        ' ''' </summary>
        ' ''' <remarks></remarks>
        'Protected Sub grdPeopleYouMayKnow_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPeopleYouMayKnow.PageIndexChanging
        '    grdPeopleYouMayKnow.PageIndex = e.NewPageIndex
        '    LoadPeopleYouMayKnow()
        'End Sub
        'Navin Prasad
        Protected Sub repTravelDiscounts_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repTravelDiscounts.ItemDataBound
            If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then

                Dim lbl As Label
                lbl = CType(e.Item.FindControl("lblHotelName"), Label)
                lbl.Text = DataBinder.Eval(e.Item.DataItem, "HotelID_Name").ToString

                lbl = CType(e.Item.FindControl("lblGroupOffer"), Label)
                lbl.Text = DataBinder.Eval(e.Item.DataItem, "SpecialOffering").ToString

                lbl = CType(e.Item.FindControl("lblStartDate"), Label)
                lbl.Text = DataBinder.Eval(e.Item.DataItem, "OfferStartDate", "{0:MM/dd/yyyy}")

                lbl = CType(e.Item.FindControl("lblEndDate"), Label)
                lbl.Text = DataBinder.Eval(e.Item.DataItem, "OfferEndDate", "{0:MM/dd/yyyy}")

            End If

        End Sub

        Protected Sub repPeopleYouMayKnow_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repPeopleYouMayKnow.ItemDataBound

            Try
                If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then
                    'Dim img As Image
                    Dim lnk, lnkRelatedPersonCompanyName As System.Web.UI.WebControls.HyperLink
                    Dim chbox, chkCompanyDirExclude As CheckBox
                    Dim radbinaryiamge As Telerik.Web.UI.RadBinaryImage
                    If e.Item Is Nothing OrElse e.Item.FindControl("imgProfileRad") Is Nothing Then
                        Exit Sub
                    End If
                    radbinaryiamge = CType(e.Item.FindControl("imgProfileRad"), Telerik.Web.UI.RadBinaryImage)
                    radbinaryiamge.ImageUrl = Me.FixLinkForVirtualPath(RadBlankImage)
                    radbinaryiamge.DataBind()
                    'Resizes the passed Image according to the specified width and height and returns the resized Image
                    If Not IsDBNull(DataBinder.Eval(e.Item.DataItem, "Photo")) Then
                        Dim commonMethods As New Aptify.Framework.Web.eBusiness.CommonMethods()
                        Dim profileImage As Drawing.Image = Nothing
                        Dim width As Integer = ProfileThumbNailWidth
                        Dim height As Integer = ProfileThumbNailHeight
                        Dim aspratioWidth As Integer

                        Dim profileImageByte As Byte() = DirectCast(DataBinder.Eval(e.Item.DataItem, "Photo"), Byte())
                        If profileImageByte IsNot Nothing AndAlso profileImageByte.Length > 0 Then
                            commonMethods.getResizedImageHeightandWidth(profileImage, profileImageByte, ProfileThumbNailWidth, ProfileThumbNailHeight, aspratioWidth)
                            profileImage = commonMethods.byteArrayToImage(profileImageByte)
                            profileImageByte = commonMethods.resizeImageAndGetAsByte(profileImage, aspratioWidth, height)
                            radbinaryiamge.DataValue = profileImageByte
                            radbinaryiamge.DataBind()
                        Else
                            radbinaryiamge.ImageUrl = Me.FixLinkForVirtualPath(RadBlankImage)
                            radbinaryiamge.DataBind()
                        End If
                    End If
                    'img = CType(e.Item.FindControl("imgProfile"), Image)
                    'LoadProfilePicture(CType(DataBinder.Eval(e.Item.DataItem, "RelatedPersonID").ToString, Long), img)
                    lnk = CType(e.Item.FindControl("lnkName"), System.Web.UI.WebControls.HyperLink)
                    lnk.Text = DataBinder.Eval(e.Item.DataItem, "Name").ToString
                    lnk.NavigateUrl = String.Format(Me.PersonListingPage & "?ID={0}", System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(DataBinder.Eval(e.Item.DataItem, "RelatedPersonID").ToString)))
                    lnkRelatedPersonCompanyName = CType(e.Item.FindControl("RelatedPersonCompanyName"), System.Web.UI.WebControls.HyperLink)
                    lnkRelatedPersonCompanyName.Text = DataBinder.Eval(e.Item.DataItem, "RelatedPersonCompanyName").ToString
                    lnkRelatedPersonCompanyName.NavigateUrl = String.Format(Me.CompanyListingPage & "?ID={0}", DataBinder.Eval(e.Item.DataItem, "CompanyID").ToString)
                    chbox = CType(e.Item.FindControl("chkPersonDirExclude"), CheckBox)
                    chbox.Checked = CType(DataBinder.Eval(e.Item.DataItem, "PersonDirExclude").ToString, Boolean)
                    chkCompanyDirExclude = CType(e.Item.FindControl("chkCompanyDirExclude"), CheckBox)
                    chkCompanyDirExclude.Checked = CType(DataBinder.Eval(e.Item.DataItem, "CompanyDirExclude").ToString, Boolean)
                    If Not CBool(chbox.Checked) Then
                        Dim lnkPerson As System.Web.UI.WebControls.HyperLink
                        Dim tempURL As String
                        Dim index As Integer
                        Dim sValue As String
                        Dim separator As String()
                        lnkPerson = CType(e.Item.FindControl("lnkName"), System.Web.UI.WebControls.HyperLink)
                        tempURL = lnkPerson.NavigateUrl
                        index = tempURL.IndexOf("=")
                        sValue = tempURL.Substring(index + 1)
                        separator = lnkPerson.NavigateUrl.Split(CChar("="))
                        lnkPerson.NavigateUrl = separator(0)
                        lnkPerson.NavigateUrl = lnkPerson.NavigateUrl & "="
                        'Aparna issue 14923. Add code for remove double encryption
                        lnkPerson.NavigateUrl = lnkPerson.NavigateUrl & sValue
                    Else
                        'remove Hyperlink if exclude from Directory
                        '  Dim lnk As HyperLink
                        lnk = CType(e.Item.FindControl("lnkName"), System.Web.UI.WebControls.HyperLink)
                        lnk.NavigateUrl = ""
                        lnk.ForeColor = Drawing.Color.Black
                        lnk.Font.Underline = False
                    End If
                    If Not CBool(chkCompanyDirExclude.Checked) Then
                        Dim lnkCompanies As System.Web.UI.WebControls.HyperLink
                        Dim tempURL As String
                        Dim index As Integer
                        Dim sValue As String
                        Dim separator As String()

                        lnkCompanies = CType(e.Item.FindControl("RelatedPersonCompanyName"), System.Web.UI.WebControls.HyperLink)
                        tempURL = lnkCompanies.NavigateUrl
                        index = tempURL.IndexOf("=")
                        sValue = tempURL.Substring(index + 1)
                        separator = lnkCompanies.NavigateUrl.Split(CChar("="))
                        lnkCompanies.NavigateUrl = separator(0)
                        lnkCompanies.NavigateUrl = lnkCompanies.NavigateUrl & "="
                        lnkCompanies.NavigateUrl = lnkCompanies.NavigateUrl & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
                    Else

                        lnk = CType(e.Item.FindControl("RelatedPersonCompanyName"), System.Web.UI.WebControls.HyperLink)
                        lnk.NavigateUrl = ""
                        lnk.ForeColor = Drawing.Color.Black
                        lnk.Font.Underline = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Private Sub LoadProfilePicture(ByVal lPersonID As Long, ByVal img As Image)

            Dim sSQL As String = ""
            Dim dt As DataTable
            m_lRecordID = User1.PersonID.ToString
            sSQL = "Select Photo from vwPersons Where ID = " & lPersonID
            dt = DataAction.GetDataTable(sSQL)
            If Not dt Is Nothing Then
                If Not IsDBNull(dt.Rows(0)("Photo")) Then

                    Dim ImagePath As String = Server.MapPath(PersonImageURL) & lPersonID.ToString & "_" & m_lRecordID & ".jpg"

                    Dim ImageData(), newImgData() As Byte

                    ImageData = DirectCast(dt.Rows(0)("Photo"), [Byte]())
                    If ImageData.Length > 0 Then
                        Dim client As New System.Net.WebClient
                        client.UploadData(ImagePath, "POST", ImageData)
                        img.ImageUrl = PersonImageURL & lPersonID.ToString & "_" & m_lRecordID & ".jpg"
                        newImgData = ConvertImagetoByte(ImagePath)
                    Else
                        img.ImageUrl = PersonImageURL & BlankImage

                    End If
                Else
                    img.ImageUrl = PersonImageURL & BlankImage

                End If

            Else
                img.ImageUrl = PersonImageURL & BlankImage

            End If

        End Sub
        Private Function ConvertImagetoByte(ByVal spath As String) As Byte()
            Try
                Dim sFile As String
                sFile = spath
                Dim fInfo As New FileInfo(sFile)
                Dim len As Long = fInfo.Length
                Dim imgData() As Byte
                Using Stream As New FileStream(sFile, FileMode.Open)
                    imgData = New Byte(Convert.ToInt32(len - 1)) {}
                    Stream.Read(imgData, 0, CInt(len))
                End Using
                Return imgData
            Catch ex As Exception
            End Try
        End Function
        ''Added GetPrice function as part of log #20183
        Protected Function GetPrice(ByVal lProductID As Long) As Double
            Try
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                Dim oOL As Aptify.Applications.OrderEntry.OrderLinesEntity
                oOrder = AptifyApplication.GetEntityObject("Orders", -1)
                oOrder.SetValue("ShipToID", User1.PersonID)
                oOrder.SetValue("BillToID", User1.PersonID)
                oOrder.BillToID = User1.PersonID
                oOrder.AddProduct(lProductID, 1)
                If oOrder.SubTypes("OrderLines").Count > 0 Then
                    oOL = oOrder.SubTypes("OrderLines").Item(0)
                    Return oOL.Price
                End If
                Return 0

            Catch ex As Exception
                Return 0
            End Try
        End Function

        Private Sub SetupRegisterButton()
            ''Dim oPrice As New Aptify.Applications.OrderEntry.IProductPrice.PriceInfo
            '' oPrice = Me.ShoppingCart1.GetUserProductPrice(CLng(Request.QueryString("ID")))
            Dim oPrice As IProductPrice.PriceInfo = ShoppingCart1.GetUserProductPrice(CLng(Me.ControlRecordID), 1)
            ''lblTotalPrice.Text = Format(oPrice.Price, User1.PreferredCurrencyFormat)
            ''If User1.PersonID > 0 Then
            ''    'redmine #16473 - not showning formatted price when user is logged in.
            ''    lblTotalPrice.Text = Format(oPrice, User1.PreferredCurrencyFormat) 'Convert.ToString(oPrice.Price)
            ''Else
            ''    lblTotalPrice.Text = Format(oPrice, ShoppingCart1.GetCurrencyFormat(2))
            ''End If
            'If Me.User1.PersonID > 0 Then
            '    Dim dSavings As Decimal
            '    Dim nonmemCost As Decimal = ShoppingCart1.GetSingleProductNonMemberCost(Me.Page.User, CLng(Me.ControlRecordID))
            '    dSavings = nonmemCost - oPrice.Price

            '    'Dim dSavings As Decimal = oNonMemberPrice.Price - oPrice
            '    'If dSavings > 0 Then
            '    '    Me.lblMemSavings.Text = "(" & Format(dSavings, User1.PreferredCurrencyFormat)
            '    '    Me.lblMemSavings.Text &= " member savings)"
            '    '    Me.lblMemSavings.Visible = True
            '    'Else
            '    '    Me.lblMemSavings.Visible = False
            '    'End If
            '    Me.lblTotalPrice.Text = Format$(nonmemCost, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))

            '    If dSavings > 0 Then
            '        Me.lblMemberPrice.Text = "(" & Format$(oPrice.Price, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
            '        Me.lblMemberPrice.Text &= " Member Price)"
            '        Me.lblMemberPrice.Visible = True
            '    Else
            '        Me.lblMemSavings.Visible = False
            '        Me.lblMemberPrice.Visible = False
            '    End If
            'Else
            '    Dim nonmemCost As Decimal = ShoppingCart1.GetSingleProductNonMemberCost(Me.Page.User, CLng(Me.ControlRecordID))
            '    Me.lblTotalPrice.Text = Format$(nonmemCost, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
            '    Me.lblTotalPrice.Visible = True
            '    Me.lblMemSavings.Visible = False
            'End If
            'Modified By Sheela as part of Task #19380 (Replaced inline query to SP)
            Dim productID As Integer
            productID = Me.ControlRecordID
            'Dim sSQL As String = "Select m.ID,m.ProductID,p.WebEnabled,p.ID,ParentID = isnull(p.ParentID,-1),m.StatusID,m.EndDate,p.DateAvailable,p.AvailableUntil," & _
            '        " p.RequireInventory, m.AvailSpace FROM " & _
            '               AptifyApplication.GetEntityBaseDatabase("Products") & _
            '               "..vwProducts p INNER JOIN " & _
            '               AptifyApplication.GetEntityBaseDatabase("Meetings") & _
            '               "..vwMeetings m ON p.ID=m.ProductID " & _
            '               " WHERE p.WebEnabled=1 AND p.IsSold =1 AND m.ProductID=" & Me.ControlRecordID
            Dim sSQL As String = AptifyApplication.GetEntityBaseDatabase("Meetings") & "..spSetupRegisterButton__c"
            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@productID", SqlDbType.Int, productID)
            Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    With dt.Rows(0)
                        Dim sError As String = "" 'Error string used to display reason product is unavailable to user
                        Dim bLnkRegisterEnabled As Boolean = True 'flag to determine if Registration Link should be enabled
                        ValidateProductAvailability(dt.Rows(0), bLnkRegisterEnabled, sError)

                        ' If conditition added by GM for Redmine #20467
                        If CLng(.Item("ISLEINSTERSOCIETYPRODUCT")) > 0 AndAlso oPrice.Price > 0 Then
                            trRegister.Visible = False
                            btnRegister.Visible = False
                        Else
                            trRegister.Visible = True
                            btnRegister.Visible = True
                        End If
                        ' End Redmine #20467 

                        'Diilp changes for visibility of register link button
                        'set btnRegister based on product availability
                        If Not bLnkRegisterEnabled Then
                            btnRegister.Enabled = False
                            btnRegister.Visible = False
                            btnRegisterGroup.Visible = False
                            lblMemSavings.Visible = False
                        End If

                        'display error message if product is unavailable
                        If sError <> "" Then
                            sError &= "<br>" 'add line break
                            lblMeetingStatus.Text = sError
                            lblMeetingStatus.Visible = True
                        End If

                        NewProductAvailable(dt)

                        If Me.User1.PersonID > 0 Then
                            ForumDiv.Visible = True
                        Else
                            ForumDiv.Visible = False
                        End If
                        If CLng(.Item("ParentID")) > 0 Then
                            trRegister.Visible = False
                        Else
                            trRegister.Visible = True
                        End If
                    End With
                Else
                    'changed by Deepika on 16/10/2017 for Bug #18437
                    'Make register button invisible if currently sold=0 and web enabled=1 for product
                    btnRegister.Visible = False
                End If
            End If

        End Sub
        Private Sub NewProductAvailable(ByVal dt2 As DataTable)
            'check to see if a newer version of this product can be offered
            Dim lNewerProductID As Long
            Dim oNewerProductID As Object
            Dim sSQL As String
            Dim dt As DataTable
            Dim lCurrentProductID As Long
            With dt2.Rows(0)
                lCurrentProductID = CLng(.Item("ProductID"))
                sSQL = "SELECT " & AptifyApplication.GetEntityBaseDatabase("Products") &
                ".dbo.fnGetLatestVersionProductID(" & lCurrentProductID & ")"
                oNewerProductID = DataAction.ExecuteScalar(sSQL)
                If IsNumeric(oNewerProductID) Then
                    lNewerProductID = CLng(oNewerProductID)
                Else
                    lNewerProductID = -1
                End If
            End With
            'display link to the latest valid version of this product if one exists
            If lNewerProductID > 0 AndAlso lNewerProductID <> lCurrentProductID Then

                'Modified By Sheela as part of Task #19380 (Replaced inline query to SP)
                'sSQL = "select p.ID,p.Name , p.WebName,p.WebEnabled , p.ProductCategory ,p.Description,m.Place,m.StartDate,m.EndDate  from " & _
                ' AptifyApplication.GetEntityBaseDatabase("Products") & _
                '"..vwproducts p ,..vwMeetings m where p.ID=m.ProductID and p.ID=" & lNewerProductID


                'sSQL = "SELECT * FROM " & _
                '        AptifyApplication.GetEntityBaseDatabase("Products") & _
                '        "..vwProducts WHERE ID=" & lNewerProductID
                'dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                sSQL = AptifyApplication.GetEntityBaseDatabase("Products") & "..spGetMeetingProductInfo__c"
                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("@lNewerProductID", SqlDbType.Int, lNewerProductID)
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)

                Dim dcol As DataColumn = New DataColumn()
                dcol.Caption = "Price"
                dcol.ColumnName = "Price"
                dt.Columns.Add(dcol)
                If dt.Rows.Count > 0 Then
                    Dim oPrice As New Aptify.Applications.OrderEntry.IProductPrice.PriceInfo
                    For Each rw As DataRow In dt.Rows
                        oPrice = Me.ShoppingCart1.GetUserProductPrice(CLng(rw("ID")))
                        If oPrice.Price = 0 Then
                            rw("Price") = "Free "
                        Else
                            rw("Price") = Format(oPrice.Price, User1.PreferredCurrencyFormat)
                        End If

                    Next
                End If

                If CBool(dt.Rows(0).Item("WebEnabled")) Then
                    repRelatedEvents.DataSource = dt
                    repRelatedEvents.DataBind()
                    repRelatedEvents.Visible = True
                    lblRelatedEvents.Visible = False


                    'Aparna issue no.13414 for show related event header for data  available
                    RelatedEventsHeader.Visible = True
                    tdRelatedEventsHeader.Visible = True
                    RightPaneBorder.Visible = True
                Else
                    lblRelatedEvents.Text = "Related events not available for this meeting."
                    lblRelatedEvents.Visible = True
                    repRelatedEvents.Visible = False
                End If

                'Aparna issue no.13414 for hide related event header for data not available
            Else
                RelatedEventsHeader.Visible = False
                tdRelatedEventsHeader.Visible = False
                RightPaneBorder.Visible = False
            End If

        End Sub

        '12/05/06 MAS
        Private Sub ValidateProductAvailability(ByRef dr As DataRow, ByRef bEnableLink As Boolean, ByRef sErrorMessage As String)
            'Properties checked to determine meeting availability:
            '1. Meeting Status (meeting.StatusID)
            '2. Date Meeting ends (meeting.EndDate)
            '3. Date Product is Available for purchase (product.DateAvailable)
            '4. Date Registration expires (product.AvailableUntil)
            '5. Meeting space limited (product.RequireInventory)
            '6. Meeting has open seats (meeting.AvailSpace)
            'IF all of the above conditions pass, then this meeting product is available for purchase
            'Precondition: bEnableLink should be passed in as true on the first call to ValidateProductAvailability()
            'Postcondition: bEnableLink = true if the current product can be purchased; false if not
            '               sError = message to user regarding this product's availability

            Dim dToday As Date = Today()

            With dr
                '1. meeting status?
                '   meeting.StatusID : 1 = Planned, 2 = Occurred, 3 = Cancelled
                If (CInt(.Item("StatusID")) = 3) Then
                    sErrorMessage = "This event has been cancelled."
                    bEnableLink = False

                    '2.Has the meeting already occurred? 
                    'NOTE: meeting.EndDate is a required field. If the meeting entity is changed
                    '      to not require this field be specified, this logic will fail.
                ElseIf (CInt(.Item("StatusID")) = 2) Or (CDate(.Item("EndDate")) < dToday) Then
                    'sErrorMessage = "This meeting has already occurred."
                    bEnableLink = False
                Else 'StatusID = 1 and meeting still valid
                    '3. Is this meeting availble for purchase yet?
                    If Not IsDBNull(.Item("DateAvailable")) _
                       AndAlso CStr(.Item("DateAvailable")) <> "" _
                       AndAlso CDate(.Item("DateAvailable")) > dToday Then
                        sErrorMessage = "Registration for this event is not availble until " &
                                         CDate(.Item("DateAvailable")).ToLongDateString & "."
                        bEnableLink = False

                        '4. Has Registration for this meeting expired?
                    ElseIf Not IsDBNull(.Item("AvailableUntil")) _
                           AndAlso CStr(.Item("AvailableUntil")) <> "" _
                           AndAlso CDate(.Item("AvailableUntil")) < dToday Then
                        'sErrorMessage = "Registration for this meeting ended on " & _
                        '                 CDate(.Item("DateAvailable")).ToLongDateString & "."
                        bEnableLink = False
                        btnRegister.Visible = False
                        lblStatus.Visible = True
                        trMetingRegStatus.Visible = True
                        lblStatus.Text = "Not Avaliable"

                        '5 and 6. Is there space available for another registrant?
                    ElseIf CBool(.Item("RequireInventory")) _
                           AndAlso Not IsDBNull(.Item("AvailSpace")) _
                           AndAlso CInt(.Item("AvailSpace")) < 1 Then
                        'Rajesh K- 04/07/2014
                        'sErrorMessage = "Registration is full. If you choose to continue, you will be added to the Waiting List."
                        sErrorMessage = Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Meeting.RegistrationIsFull")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                        'Swapnil D - 15/10/2017 Added to visible off the button in case registration is full redmine #18439
                        bEnableLink = False
                    Else
                        'Else this meeting is available for Registration
                    End If
                End If
            End With
        End Sub
        Dim sConflictionOption As String = "", sProductPage As String = "", sOrderPage As String = ""
        Dim ProductID As Long

        Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
            Try
                'Added by Asmita
                Dim req As Integer = -1
                Dim Term As Long = -1
                'If ValidateTermsAndCondtionForIndividual(Term, req) = False Then
                '    Exit Sub
                'End If
                'end by asmita

                sProductPage = ""
                sOrderPage = ""
                sConflictionOption = ""
                SaveCheckedValues()
                Dim ConflictFlag As Boolean = False
                If ShoppingCart1.GetProductTypeWebPages(CLng(Me.ProductID), sProductPage, sOrderPage) Then
                    ProductID = Me.ProductID
                    sConflictionOption = GetConflictionOptionByProductID(ProductID)
                    FindMeetingSessionForRegistration(ConflictFlag, sConflictionOption.ToUpper()) 'Changes Made By Sandeep for Issue 13879

                    Select Case sConflictionOption.ToUpper()
                        Case "NO CONFLICT VALIDATION"
                            MyBase.Response.Redirect(sOrderPage & "?ID=" & ProductID & "&term= " & Term & "&req= " & req, False)
                            Exit Sub
                        Case "CONFLICT WARNING"
                            If ConflictFlag Then
                                radErrorMessage.VisibleOnPageLoad = True
                            Else
                                MyBase.Response.Redirect(sOrderPage & "?ID=" & ProductID & "&term= " & Term & "&req= " & req, False)
                                Exit Sub
                            End If

                        Case "CONFLICT PROHIBITED"
                            If Not ConflictFlag Then
                                MyBase.Response.Redirect(sOrderPage & "?ID=" & ProductID & "&term= " & Term & "&req= " & req, False)
                                Exit Sub
                            Else
                                radErrorMessage.VisibleOnPageLoad = True
                            End If

                    End Select
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnRegisterGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegisterGroup.Click
            Try
                'Added by Asmita
                'If ValidateTermsAndCondtionForGroup() = False Then
                '    Exit Sub
                'End If
                '

                Dim ConflictFlag As Boolean = False
                sProductPage = ""
                sOrderPage = ""
                sConflictionOption = ""
                SaveCheckedValues()

                If ShoppingCart1.GetProductTypeWebPages(CLng(Me.ProductID), sProductPage, sOrderPage) Then
                    ProductID = Me.ProductID
                End If
                sConflictionOption = GetConflictionOptionByProductID(ProductID)
                FindMeetingSessionForRegistration(ConflictFlag, sConflictionOption) 'Changes Made By Sandeep for Issue 13879
                Select Case sConflictionOption.ToUpper()
                    Case "NO CONFLICT VALIDATION"
                        MyBase.Response.Redirect(MeetingRegistrationSelectRegistrant & "?ID=" & Me.ProductID, False)
                        Exit Sub

                    Case "CONFLICT WARNING"
                        If ConflictFlag Then
                            radErrorMessage.VisibleOnPageLoad = True
                        Else
                            MyBase.Response.Redirect(MeetingRegistrationSelectRegistrant & "?ID=" & Me.ProductID, False)
                            Exit Sub
                        End If

                    Case "CONFLICT PROHIBITED"
                        If Not ConflictFlag Then
                            MyBase.Response.Redirect(MeetingRegistrationSelectRegistrant & "?ID=" & Me.ProductID, False)
                            Exit Sub
                        Else
                            radErrorMessage.VisibleOnPageLoad = True
                        End If
                End Select
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub repRelatedEvents_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repRelatedEvents.ItemDataBound
            Try
                If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then
                    Dim lnkbtn As LinkButton
                    lnkbtn = CType(e.Item.FindControl("lnkNewProductName"), LinkButton)
                    If Not IsDBNull(DataBinder.Eval(e.Item.DataItem, "WebName")) AndAlso CStr(DataBinder.Eval(e.Item.DataItem, "WebName")) <> "" Then
                        lnkbtn.Text = CStr(DataBinder.Eval(e.Item.DataItem, "WebName"))
                    Else
                        lnkbtn.Text = CStr(DataBinder.Eval(e.Item.DataItem, "Name"))
                    End If
                    lnkbtn.PostBackUrl = ProductDisplayPage & "?ID=" & CStr(DataBinder.Eval(e.Item.DataItem, "ID"))
                    Dim lbl As Label
                    lbl = CType(e.Item.FindControl("lblCategory"), Label)
                    lbl.Text = DataBinder.Eval(e.Item.DataItem, "ProductCategory").ToString

                    lbl = CType(e.Item.FindControl("lblDescription"), Label)
                    lbl.Text = DataBinder.Eval(e.Item.DataItem, "Description").ToString

                    lbl = CType(e.Item.FindControl("lblLocation"), Label)
                    lbl.Text = DataBinder.Eval(e.Item.DataItem, "Place").ToString

                    lbl = CType(e.Item.FindControl("lblStartDate"), Label)
                    'lbl.Text = DataBinder.Eval(e.Item.DataItem, "StartDate", "{0:MM/dd/yyyy}")
                    lbl.Text = DataBinder.Eval(e.Item.DataItem, "StartDate").ToString

                    Dim strTime As String = CDate(lbl.Text).TimeOfDay.ToString
                    Dim strSecond As String = CDate(lbl.Text).TimeOfDay.Seconds.ToString

                    If strTime = "00:00:00" Then
                        lbl.Text = FormatDateTime(CDate(lbl.Text), DateFormat.ShortDate)
                    ElseIf strSecond = "0" Then
                        lbl.Text = String.Format("{0:MM/dd/yyyy h:mm tt}", Convert.ToDateTime(lbl.Text))
                    End If

                    lbl = CType(e.Item.FindControl("lblEndDate"), Label)
                    lbl.Text = DataBinder.Eval(e.Item.DataItem, "EndDate").ToString

                    strTime = CDate(lbl.Text).TimeOfDay.ToString
                    strSecond = CDate(lbl.Text).TimeOfDay.Seconds.ToString

                    If strTime = "00:00:00" Then
                        lbl.Text = FormatDateTime(CDate(lbl.Text), DateFormat.ShortDate)
                    ElseIf strSecond = "0" Then
                        lbl.Text = String.Format("{0:MM/dd/yyyy h:mm tt}", Convert.ToDateTime(lbl.Text))
                    End If
                    lbl = CType(e.Item.FindControl("lblRegPrice"), Label)
                    lbl.Text = DataBinder.Eval(e.Item.DataItem, "Price").ToString
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            'Issue 12721 Dilip 
            Try
                If Not IsNothing(Request.QueryString("back")) Then
                    Response.Redirect(EventButtonPage, False)
                    Exit Sub
                Else
                    Response.Redirect(MeetingsButtonPage, False)
                    Exit Sub
                End If
            Catch ex As Exception
                ex.Message.ToString()
            End Try
        End Sub

        Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim dicMeetingSessions As New Dictionary(Of Integer, Boolean)
                Dim headerCheckBox As CheckBox = CType(sender, CheckBox)
                Dim dtAllSession As DataTable
                For Each dataItem As GridDataItem In grdSchedule.MasterTableView.Items
                    CType(dataItem.FindControl("chkSession"), CheckBox).Checked = headerCheckBox.Checked
                    dataItem.Selected = headerCheckBox.Checked

                Next
                If ViewState(ATTRIBUTE_ALLSESSION) IsNot Nothing Then
                    dtAllSession = DirectCast(ViewState(ATTRIBUTE_ALLSESSION), DataTable)
                End If
                If dtAllSession IsNot Nothing AndAlso dtAllSession.Rows.Count > 0 Then
                    If headerCheckBox.Checked = True Then
                        ViewState("CheckAll") = True
                        For Each dr As DataRow In dtAllSession.Rows
                            If ViewState(ATTRIBUTE_CHECKED_SESSION) IsNot Nothing Then
                                dicMeetingSessions = DirectCast(ViewState(ATTRIBUTE_CHECKED_SESSION), Dictionary(Of Integer, Boolean))
                            End If
                            If dicMeetingSessions.ContainsKey(dr("ProductID")) Then
                                dicMeetingSessions.Remove(dr("ProductID"))
                            End If
                            dicMeetingSessions.Add(dr("ProductID"), True)
                        Next
                    Else
                        ViewState("CheckAll") = False
                        For Each dr As DataRow In dtAllSession.Rows
                            If ViewState(ATTRIBUTE_CHECKED_SESSION) IsNot Nothing Then
                                dicMeetingSessions = DirectCast(ViewState(ATTRIBUTE_CHECKED_SESSION), Dictionary(Of Integer, Boolean))
                            End If
                            If dicMeetingSessions.ContainsKey(dr("ProductID")) Then
                                dicMeetingSessions.Remove(dr("ProductID"))
                            End If
                            dicMeetingSessions.Add(dr("ProductID"), False)
                        Next
                    End If
                    dtAllSession = Nothing
                    ViewState(ATTRIBUTE_CHECKED_SESSION) = dicMeetingSessions

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub FindMeetingSessionForRegistration(ByRef ConflictFlag As Boolean, ByVal sConflictionOption As String) 'Changes Made By Sandeep for Issue 13879 
            Try
                Dim SessionList As New List(Of Integer)
                Dim dtItems As New DataTable
                Dim dtConfictSessions As DataTable = Nothing
                Dim dcColumn As DataColumn
                Dim dicMeetingSessions As New Dictionary(Of Integer, Boolean)
                If ViewState(ATTRIBUTE_SESSION_DT) Is Nothing Then
                    dcColumn = New DataColumn()
                    dcColumn.DataType = Type.GetType("System.String")
                    dcColumn.ColumnName = "ErrorMessage"
                    dtItems.Columns.Add(dcColumn)
                    ViewState(ATTRIBUTE_SESSION_DT) = dtItems
                End If
                If ViewState(ATTRIBUTE_CHECKED_SESSION) IsNot Nothing Then
                    dicMeetingSessions = DirectCast(ViewState(ATTRIBUTE_CHECKED_SESSION), Dictionary(Of Integer, Boolean))
                End If
                If dicMeetingSessions IsNot Nothing AndAlso dicMeetingSessions.Count > 0 Then
                    For Each MeetingSesion As KeyValuePair(Of Integer, Boolean) In dicMeetingSessions
                        If MeetingSesion.Value = True Then
                            Select Case sConflictionOption.ToUpper()
                                Case "NO CONFLICT VALIDATION"
                                    SessionList.Add(MeetingSesion.Key)

                                Case "CONFLICT WARNING"
                                    If Not CheckMeetingSessionConflict(MeetingSesion.Key.ToString(), ConflictFlag, dicMeetingSessions) Then
                                        SessionList.Add(MeetingSesion.Key)
                                    End If

                                Case "CONFLICT PROHIBITED"
                                    If Not CheckMeetingSessionConflict(MeetingSesion.Key.ToString(), ConflictFlag, dicMeetingSessions) Then
                                        SessionList.Add(MeetingSesion.Key)
                                    End If
                            End Select
                        End If
                    Next
                End If
                If ViewState(ATTRIBUTE_SESSION_DT) IsNot Nothing Then
                    dtConfictSessions = ViewState(ATTRIBUTE_SESSION_DT)
                End If
                If dtConfictSessions.Rows.Count > 0 Then
                    lstErrorMessage.DataSource = dtConfictSessions
                    lstErrorMessage.DataBind()
                End If

                ViewState(ATTRIBUTE_SESSION_DT) = Nothing
                Session("MeetingSessions") = SessionList
                Session("SessionStatus") = dicMeetingSessions
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Changes Made By Sandeep for Issue 13879
        Private Sub GetMeetingDates(ByVal vProductID As String, ByRef Startdate As DateTime, ByRef Enddate As DateTime)
            '#21000
            'Dim dtGetMeeting As New DataTable
            'Dim sSQL As String
            'sSQL = "Select ID, StartDate, EndDate from " & AptifyApplication.GetEntityBaseDatabase("Products") & "..Meeting WHERE ProductID = " & CInt(vProductID)
            'dtGetMeeting = Me.DataAction.GetDataTable(sSQL)
            If dtMeeting.Rows.Count > 0 Then
                Startdate = DirectCast(dtMeeting.Rows(0)("StartDate"), DateTime)
                Enddate = DirectCast(dtMeeting.Rows(0)("EndDate"), DateTime)
            End If
        End Sub
        'Changes Made By Sandeep for Issue 13879
        Private Function CheckSessionConfliction(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal StartDate_Compare As DateTime, ByVal EndDate_Compare As DateTime) As Boolean
            If StartDate = StartDate_Compare Then
                Return True
            End If
            If StartDate > StartDate_Compare Then
                If EndDate_Compare > StartDate Then
                    Return True
                End If
            Else
                If EndDate > StartDate_Compare Then
                    Return True
                End If
            End If
            Return False
        End Function
        Private Function GetConflictionOptionByProductID(ByVal lproductID As Long) As String
            Dim sSQL As String
            Dim strConflictChecker As String = String.Empty
            'Modified By Sheela as part of Task #19380 (Replaced inline query to SP)
            'sSQL = "SELECT MeetingConflictionChecker FROM " & Database &
            '"..vwMeeting WHERE ProductID = " & lproductID
            'strConflictChecker = DataAction.ExecuteScalar(sSQL)
            sSQL = AptifyApplication.GetEntityBaseDatabase("Meetings") & "..spCheckForMeetingConfliction__c"
            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@productID", SqlDbType.Int, lproductID)
            strConflictChecker = CStr(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, param))
            Return strConflictChecker
        End Function
        'Added by sandeep for Issue no 13879 on 07/02/2013
        'Check for conflict in meeting sessions
        Private Function CheckMeetingSessionConflict(ByVal strProduct As String, ByRef ConflictFlag As Boolean, ByVal dicMeetingSessions As Dictionary(Of Integer, Boolean)) As Boolean
            Dim dtItems As DataTable = Nothing
            'Dim dtGetMeeting As DataTable = Nothing
            Dim drFirstRow, drRow As DataRow
            Dim sSQL As String = String.Empty
            Dim errorMessage As String = String.Empty
            Dim newStartdate, newEndDate, nextStartdate, nextEndDate As DateTime
            Dim vDuplicateMessage As Boolean
            Dim bErrorFlag As Boolean = False
            If ViewState(ATTRIBUTE_SESSION_DT) IsNot Nothing Then
                dtItems = ViewState(ATTRIBUTE_SESSION_DT)
            End If
            For Each MeetingSession As KeyValuePair(Of Integer, Boolean) In dicMeetingSessions
                If MeetingSession.Value = True And strProduct.Trim() <> MeetingSession.Key.ToString() Then
                    GetMeetingDates(strProduct.Trim(), newStartdate, newEndDate)
                    GetMeetingDates(MeetingSession.Key.ToString().Trim(), nextStartdate, nextEndDate)
                    If CheckSessionConfliction(newStartdate, newEndDate, nextStartdate, nextEndDate) Then
                        bErrorFlag = True
                        ConflictFlag = True
                        If dtItems IsNot Nothing AndAlso dtItems.Rows.Count = 0 Then
                            drFirstRow = dtItems.NewRow()
                            drFirstRow("ErrorMessage") = "You cannot continue the registration process because a scheduling conflict exists between two or more sessions. Please modify your selections and try again. We detected a conflict for the following sessions:"
                            dtItems.Rows.Add(drFirstRow)
                        End If
                        'sSQL = "Select ID,ProductID_Name, StartDate, EndDate from " & AptifyApplication.GetEntityBaseDatabase("Products") & "..vwMeetings WHERE ProductID = " & CInt(strProduct)
                        'dtGetMeeting = Me.DataAction.GetDataTable(sSQL)
                        errorMessage = Convert.ToString(dtMeeting.Rows(0)("ProductID_Name"))
                        drRow = dtItems.NewRow()
                        vDuplicateMessage = False
                        For Each dr As DataRow In dtItems.Rows
                            If dr("ErrorMessage").ToString() = errorMessage Then
                                vDuplicateMessage = True
                            End If
                        Next

                        If Not vDuplicateMessage Then
                            drRow("ErrorMessage") = errorMessage
                            dtItems.Rows.Add(drRow)
                        End If

                    End If
                End If
            Next
            Return bErrorFlag
        End Function


        Protected Sub RadmeetingRate_Rate(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadmeetingRate.Rate
            Dim itemratinginputobj As New ItemRatingInput()
            itemratinginputobj.RatingValue = Convert.ToDecimal(RadmeetingRate.Value)
            If Request.QueryString("ID") IsNot Nothing Then
                itemratinginputobj.ItemRatingURI = ItemRatingURI.ToString() + Me.ProductID
            End If
            itemratinginputobj.PersonID = User1.PersonID
            itemratinginputobj.ItemRatingTypeURI = ""
            itemratinginputobj.RatedItemURI = ""
            ' Dim a As Integer = RadmeetingRate.Value
            If (RadmeetingRate.Value = 0) Then
                RemoveItemRating(itemratinginputobj)
            Else
                SubmitItemRating(itemratinginputobj)
            End If

            Dim ItemRatingServiceInformationobj As New ItemRatingServiceInformation()
            ItemRatingServiceInformationobj = GetItemRating(itemratinginputobj)
            If (ItemRatingServiceInformationobj.ItemRatingDetails.PersonEntry Is Nothing) Then
                If Not ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage Is Nothing Then
                    RadRatingTotal.Value = ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage
                    If (ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage >= 1) Then
                        totalrating.Text = "(Total: " + Math.Round(Decimal.Parse(ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage), 1).ToString() + ")"
                    Else
                        totalrating.Text = "(Total: " + Decimal.Parse(ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage).ToString("0.#") + ")"
                    End If
                Else
                    RadRatingTotal.Value = 0
                    totalrating.Text = "(Total: " + "0" + ")"
                End If


            Else
                If Not ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage Is Nothing Then
                    RadRatingTotal.Value = ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage
                    If (ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage >= 1) Then
                        totalrating.Text = "(Total: " + Math.Round(Decimal.Parse(ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage), 1).ToString() + ")"
                    Else
                        totalrating.Text = "(Total: " + Decimal.Parse(ItemRatingServiceInformationobj.ItemRatingDetails.RecordedValueAverage).ToString("0.#") + ")"
                    End If

                Else
                    RadRatingTotal.Value = 0
                    totalrating.Text = "(Total: " + "0" + ")"
                End If
            End If

        End Sub

        Protected Sub grdSchedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdSchedule.ItemDataBound
            Try
                Dim dateColumns As New List(Of String)
                'Add datecolumn uniqueName in list for Date format
                dateColumns.Add("GridDateTimeColumnEndDate")
                dateColumns.Add("GridDateTimeColumnStartDate")
                CommonMethods.FormatedDateOnGrid(dateColumns, e.Item)
                'Suraj Issue 14457 3/12/13 ,we provide a tool tip for DatePopupButton as well as the  textbox   
                If TypeOf e.Item Is GridFilteringItem Then
                    Dim filterItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
                    Dim gridDateTimeColumnStartDate As RadDatePicker = DirectCast(filterItem("GridDateTimeColumnStartDate").Controls(0), RadDatePicker)
                    gridDateTimeColumnStartDate.ToolTip = "Enter a filter date"
                    gridDateTimeColumnStartDate.DatePopupButton.ToolTip = "Select a filter date"
                    Dim gridDateTimeColumnEndDate As RadDatePicker = DirectCast(filterItem("GridDateTimeColumnEndDate").Controls(0), RadDatePicker)
                    gridDateTimeColumnEndDate.ToolTip = "Enter a filter date"
                    gridDateTimeColumnEndDate.DatePopupButton.ToolTip = "Select a filter date"

                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Public Function GetItemRating(ByVal inputObjArgs As ItemRatingInput) As ItemRatingServiceInformation
            Dim objItemRatingServiceInfo As New ItemRatingServiceInformation()

            Try
                'Dim uc As UserCredentials = DALHelper.Helper.UserContext()
                Dim objApplication As New AptifyApplication
                Dim objRatingInfo As New ItemRatingInformation()

                Dim objRatings As New Ratings(objApplication)

                'check for required input arguments and raise exception if it is not available
                If (inputObjArgs.ItemRatingURI Is Nothing) AndAlso (inputObjArgs.ItemRatingTypeURI Is Nothing OrElse inputObjArgs.RatedItemURI Is Nothing) Then
                    'throw new Exception("The required input arguments are not provided.");
                    'objErrorInfo.IsError = True
                    'objErrorInfo.Message = "The required input arguments are not provided."
                    'objErrorInfo.Stack = ""
                    'objErrorInfo.InnerException = ""
                    'Throw New WebFaultException(Of ErrorInfo)(objErrorInfo, System.Net.HttpStatusCode.BadRequest)
                End If

                'Call various overload methods based on the input arguments provided
                'If inputObjArgs.ItemRatingTypeURI IsNot Nothing AndAlso inputObjArgs.RatedItemURI IsNot Nothing AndAlso inputObjArgs.PersonID > 0 Then
                '    objRatingInfo = objRatings.GetItemRating(inputObjArgs.ItemRatingTypeURI, inputObjArgs.RatedItemURI, inputObjArgs.PersonID)
                'ElseIf inputObjArgs.ItemRatingTypeURI IsNot Nothing AndAlso inputObjArgs.RatedItemURI IsNot Nothing Then
                '    objRatingInfo = objRatings.GetItemRating(inputObjArgs.ItemRatingTypeURI, inputObjArgs.RatedItemURI)
                If inputObjArgs.ItemRatingURI IsNot Nothing AndAlso inputObjArgs.PersonID >= 0 Then
                    objRatingInfo = objRatings.GetItemRating(inputObjArgs.ItemRatingURI, inputObjArgs.PersonID)
                Else
                    objRatingInfo = objRatings.GetItemRating(inputObjArgs.ItemRatingURI)
                End If

                objItemRatingServiceInfo.ItemRatingDetails = objRatingInfo
                'objItemRatingServiceInfo.ErrorInformation = objErrorInfo
                'Catch wEx As WebFaultException
                ' Throw New WebFaultException(Of ErrorInfo)(wEx.Detail, System.Net.HttpStatusCode.BadRequest)
            Catch ex As Exception

            End Try

            Return objItemRatingServiceInfo

        End Function

        Public Function SubmitItemRating(ByVal inputObjArgs As ItemRatingInput) As ItemRatingServiceInformation
            Dim objItemRatingServiceInfo As New ItemRatingServiceInformation()
            'Dim objErrorInfo As New ErrorInfo()
            'objErrorInfo.IsError = False
            Try
                'Dim uc As UserCredentials = DALHelper.Helper.UserContext()

                Dim objApplication As New AptifyApplication()
                Dim objRatingInfo As New ItemRatingInformation()

                Dim objRatings As New Ratings(objApplication)

                'check for required input arguments and raise exception if it is not available
                ' Modified By PradipJ
                ' To reset the rating value by passing RatingValue = 0
                ''RashmiP, Issue 14391.3/22/13, Check condition if Rating value is greater then zero then save record.
                If inputObjArgs.RatingValue > 0 Then
                    If inputObjArgs.ItemRatingURI IsNot Nothing AndAlso inputObjArgs.PersonID >= 0 AndAlso inputObjArgs.RatingValue >= 0 Then
                        objRatingInfo = objRatings.SubmitItemRatingEntry(inputObjArgs.ItemRatingURI, inputObjArgs.PersonID, inputObjArgs.RatingValue)
                    Else
                        objRatingInfo = objRatings.SubmitItemRatingEntry(inputObjArgs.ItemRatingURI, inputObjArgs.RatingValue)
                    End If

                    objItemRatingServiceInfo.ItemRatingDetails = objRatingInfo
                End If

                'Call various overload methods based on the input arguments provided
                'If inputObjArgs.ItemRatingTypeURI IsNot Nothing AndAlso inputObjArgs.RatedItemURI IsNot Nothing AndAlso inputObjArgs.PersonID > 0 AndAlso inputObjArgs.RatingValue > 0 Then
                '    objRatingInfo = objRatings.SubmitItemRatingEntry(inputObjArgs.ItemRatingTypeURI, inputObjArgs.RatedItemURI, inputObjArgs.PersonID, inputObjArgs.RatingValue)
                'ElseIf inputObjArgs.ItemRatingTypeURI IsNot Nothing AndAlso inputObjArgs.RatedItemURI IsNot Nothing AndAlso inputObjArgs.RatingValue > 0 Then
                '    objRatingInfo = objRatings.SubmitItemRatingEntry(inputObjArgs.ItemRatingTypeURI, inputObjArgs.RatedItemURI, inputObjArgs.RatingValue)
                ' Modified By AmitS on 05/24/2012
                ' Change the Rating value condtion to allow if the rating value is 0


                'objItemRatingServiceInfo.ErrorInformation = objErrorInfo
                'Catch wEx As WebFaultException
                '    Throw New WebFaultException(Of ErrorInfo)(wEx.Detail, System.Net.HttpStatusCode.BadRequest)
            Catch ex As Exception
                'ExceptionManager.Publish(ex)
                'objErrorInfo.IsError = True
                'objErrorInfo.Message = ex.Message
                'objErrorInfo.Stack = If((ex.StackTrace IsNot Nothing), ex.StackTrace, "")
                'objErrorInfo.InnerException = If((ex.InnerException IsNot Nothing), ex.InnerException.Message, "")
                'Throw New WebFaultException(Of ErrorInfo)(objErrorInfo, System.Net.HttpStatusCode.BadRequest)
            End Try
            Return objItemRatingServiceInfo
        End Function

        Public Function RemoveItemRating(ByVal inputObjArgs As ItemRatingInput) As ItemRatingServiceInformation
            Dim objItemRatingServiceInfo As New ItemRatingServiceInformation()

            Try

                Dim objApplication As New AptifyApplication()
                Dim objRatingInfo As New ItemRatingInformation()

                Dim objRatings As New Ratings(objApplication)

                'check for required input arguments and raise exception if it is not available
                If inputObjArgs.PersonID <= 0 Then
                    'objErrorInfo.IsError = True
                    'objErrorInfo.Message = "The required input argument PersonID is not provided."
                    'objErrorInfo.Stack = ""
                    'objErrorInfo.InnerException = ""
                    'Throw New WebFaultException(Of ErrorInfo)(objErrorInfo, System.Net.HttpStatusCode.BadRequest)
                End If

                If inputObjArgs.ItemRatingURI IsNot Nothing AndAlso inputObjArgs.PersonID > 0 Then
                    objRatings.RemoveItemRatingEntry(inputObjArgs.ItemRatingURI, inputObjArgs.PersonID)
                End If

                objItemRatingServiceInfo.ItemRatingDetails = objRatingInfo


            Catch ex As Exception
                'ExceptionManager.Publish(ex)
                'objErrorInfo.IsError = True
                'objErrorInfo.Message = ex.Message
                'objErrorInfo.Stack = If((ex.StackTrace IsNot Nothing), ex.StackTrace, "")
                'objErrorInfo.InnerException = If((ex.InnerException IsNot Nothing), ex.InnerException.Message, "")
                'Throw New WebFaultException(Of ErrorInfo)(objErrorInfo, System.Net.HttpStatusCode.BadRequest)
            End Try
            Return objItemRatingServiceInfo
        End Function


        Protected Sub btnPopUpOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopUpOk.Click
            radErrorMessage.VisibleOnPageLoad = False
        End Sub

        'Suraj Issue 14457  3/5/13 ,if the grid load first time By default the sorting will be Ascending for grid  speaker
        ' Suraj Issue 16115  5/6/13 ,The Meetings Center page sub grid on default.aspx  The grid's sorting should default to Start Date in ascending order.
        Private Sub AddExpression()
            Dim SessionSort As New GridSortExpression
            Dim SpeakerSort As New GridSortExpression
            SessionSort.FieldName = "StartDate"
            SessionSort.SetSortOrder("Ascending")
            grdSchedule.MasterTableView.SortExpressions.AddSortExpression(SessionSort)
            SpeakerSort.FieldName = "FirstName"
            SpeakerSort.SetSortOrder("Ascending")
            grdSpeakers.MasterTableView.SortExpressions.AddSortExpression(SpeakerSort)
        End Sub

        'Amruta IssueId 14380,19/3/2013,To redirect on MyMeetingSession Page
        Protected Sub btnMySessionCalendar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMySessionCalendar.Click
            Dim url As String = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.ApplicationPath)) & MeetingSessionsPage
            btnMySessionCalendar.OnClientClick = "javascript: openNewWin('" & url & "'); return false;"
        End Sub

#Region "Custom Subroutine/Methods"
        ''' <summary>
        ''' This subroutine is added by Asmita. Load the terms and condtion flag and it's description if it's configured on the Product.
        ''' </summary>
        ''' <remarks></remarks>
        'Private Sub LoadTermsAndCondtion()
        '    Dim dt As DataTable = Nothing, sSQL As String = String.Empty, bRetVal As Boolean = True
        '    Try
        '        'sSQL = "SELECT IsRequiredAgreement__c, TermsAndConditionID__c FROM " & _
        '        '             AptifyApplication.GetEntityBaseDatabase("Products") & _
        '        '             "..vwProducts WHERE ID=" & Me.ControlRecordID
        '        'Siddharth replaced inline query by SP
        '        sSQL = Database & "..spGetTermsConditionsForProduct__c @ProductID=" & Me.ControlRecordID
        '        dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

        '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso CBool(dt.Rows(0)("IsRequiredAgreement__c")) = True Then
        '            lblTicketCondtion.Visible = True
        '            ChkRequiredAgreement.Visible = True

        '            Dim lTermsId As Long = -1
        '            sSQL = String.Empty

        '            If Not String.IsNullOrEmpty(System.Convert.ToString(dt.Rows(0)("TermsAndConditionID__c"))) AndAlso CLng(dt.Rows(0)("TermsAndConditionID__c")) > 0 Then
        '                lTermsId = CLng(dt.Rows(0)("TermsAndConditionID__c"))

        '                Dim dtTerms As DataTable
        '                '  sSQL = "SELECT WebDescription FROM " & _
        '                '  AptifyApplication.GetEntityBaseDatabase("TermsAndConditions__c") & _
        '                '"..vwTermsAndConditions__c WHERE ID=" & lTermsId
        '                'Siddharth replaced inline query by SP
        '                sSQL = Database & "..spGetTermsConditionsByID__c @TermsConditionsID=" & lTermsId
        '                dtTerms = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        '                lblTicketCondtion.Text = System.Convert.ToString(dtTerms.Rows(0)("WebDescription"))
        '                lblTicketCondtion.Visible = True
        '            End If
        '        Else
        '            lblTicketCondtion.Visible = False
        '            ChkRequiredAgreement.Visible = False
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        ''' <summary>
        ''' 'To validate Terms and conditions on Product 
        ''' </summary>
        ''' <param name="ProductID"></param>
        ''' <remarks></remarks>
        'Private Function ValidateTermsAndCondtionForGroup() As Boolean
        '    Dim dt As DataTable = Nothing, sSQL As String = String.Empty, bRetVal As Boolean = True
        '    Try
        '        'sSQL = "SELECT IsRequiredAgreement__c, TermsAndConditionID__c FROM " & _
        '        '              AptifyApplication.GetEntityBaseDatabase("Products") & _
        '        '              "..vwProducts WHERE ID=" & Me.ControlRecordID
        '        'Siddharth replaced inline query by SP
        '        sSQL = Database & "..spGetTermsConditionsForProduct__c @ProductID=" & Me.ControlRecordID
        '        dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso CBool(dt.Rows(0)("IsRequiredAgreement__c")) = True Then
        '            If Me.ChkRequiredAgreement.Checked = False Then
        '                lblAdded.Visible = True
        '                lblAdded.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(System.Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.TermsAndConditions")), System.Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        '                bRetVal = False
        '            Else
        '                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity = Nothing
        '                Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity = Nothing
        '                oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
        '                oOrderLine = CType(oOrder.SubTypes("OrderLines").Find("ProductID", ProductID), Aptify.Applications.OrderEntry.OrderLinesEntity)

        '                oOrderLine.SetValue("IsRequiredAgreement__c", 1)
        '                oOrderLine.SetValue("TermsAndConditionID__c", CLng(dt.Rows(0)("TermsAndConditionID__c")))
        '                ShoppingCart1.SaveCart()
        '                bRetVal = True
        '            End If
        '            bRetVal = True
        '        End If
        '        bRetVal = True
        '        Return bRetVal
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '        Return bRetVal
        '    End Try
        'End Function

        ''' <summary>
        ''' To validate Terms and conditions on Product
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        'Private Function ValidateTermsAndCondtionForIndividual(ByRef Term As Long, ByRef req As Integer) As Boolean
        '    Dim dttermandconditions As DataTable = Nothing
        '    Dim sSQL As String = String.Empty, bRetVal As Boolean = True
        '    Try
        '        'sSQL = "SELECT IsRequiredAgreement__c, TermsAndConditionID__c FROM " & _
        '        '              AptifyApplication.GetEntityBaseDatabase("Products") & _
        '        '              "..vwProducts WHERE ID=" & Me.ControlRecordID
        '        'Siddharth replaced inline query by SP
        '        sSQL = Database & "..spGetTermsConditionsForProduct__c @ProductID=" & Me.ControlRecordID
        '        dttermandconditions = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

        '        If dttermandconditions IsNot Nothing AndAlso dttermandconditions.Rows.Count > 0 AndAlso CBool(dttermandconditions.Rows(0)("IsRequiredAgreement__c")) = True Then
        '            If Me.ChkRequiredAgreement.Checked = False Then
        '                req = 0
        '                Term = Nothing
        '                lblAdded.Visible = True
        '                lblAdded.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.TermsAndConditions")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        '                bRetVal = False
        '            Else
        '                req = 1
        '                Term = CLng(dttermandconditions.Rows(0)("TermsAndConditionID__c"))
        '                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity = Nothing
        '                Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity = Nothing
        '                oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
        '                oOrderLine = CType(oOrder.SubTypes("OrderLines").Find("ProductID", Me.ControlRecordID), Aptify.Applications.OrderEntry.OrderLinesEntity)

        '                oOrderLine.SetValue("IsRequiredAgreement__c", 1)
        '                oOrderLine.SetValue("TermsAndConditionID__c", CLng(dttermandconditions.Rows(0)("TermsAndConditionID__c")))
        '                ShoppingCart1.SaveCart()
        '                bRetVal = True
        '            End If
        '        End If
        '        Return bRetVal
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '        Return bRetVal
        '    End Try
        'End Function
#End Region

        Private Sub AddGoogleTagImpression()
            Dim impression As DetailImpression, productDto As ProductDto

            If (_foundProduct Is Nothing) Then
                Return
            End If

            productDto = GetProductDto()
            impression = New DetailImpression(Me.Page, productDto)

            impression.Render()

        End Sub

        Private Function GetProductDto() As ProductDto

            Return New ProductDto With
                {
                .Name = _foundProduct("WebName").ToString,
                .Id = _foundProduct("ProductID").ToString,
                .Price = _foundPrice,
                .Brand = "Chartered Accountants",
                .Category = "",
                .Variant = "",
                .List = _foundProduct("ParentWebName").ToString,
                .Currency = _foundCurrency,
                .Position = 1
                }

        End Function


        Public Class ItemRatingInput
            Private m_ItemRatingURI As String
            Private m_ItemRatingTypeURI As String
            Private m_RatedItemURI As String
            Private m_RatingValue As Decimal
            Private m_PersonID As Integer
            Public Property ItemRatingURI As String
                Get
                    Return m_ItemRatingURI
                End Get
                Set(ByVal value As String)
                    m_ItemRatingURI = value
                End Set
            End Property
            Public Property ItemRatingTypeURI As String
                Get
                    Return m_ItemRatingTypeURI
                End Get
                Set(ByVal value As String)
                    m_ItemRatingTypeURI = value
                End Set
            End Property
            Public Property RatedItemURI As String
                Get
                    Return m_RatedItemURI
                End Get
                Set(ByVal value As String)
                    m_RatedItemURI = value
                End Set
            End Property
            Public Property RatingValue As Decimal
                Get
                    Return m_RatingValue
                End Get
                Set(ByVal value As Decimal)
                    m_RatingValue = value
                End Set
            End Property
            Public Property PersonID As Integer
                Get
                    Return m_PersonID
                End Get
                Set(ByVal value As Integer)
                    m_PersonID = value
                End Set
            End Property

        End Class


        Public Class ItemRatingServiceInformation
            Private m_ItemRatingDetails As New ItemRatingInformation
            Public Property ItemRatingDetails As ItemRatingInformation
                Get
                    Return m_ItemRatingDetails
                End Get
                Set(ByVal value As ItemRatingInformation)
                    m_ItemRatingDetails = value
                End Set
            End Property
        End Class

#Region "Custom Methods"
        'Rajesh Kardile - 04/19/2014 - Display Attachment Records.

        ''' <summary>
        ''' Display Attachment as per Meeting
        ''' </summary>
        ''' <param name="productID"></param>
        ''' <remarks></remarks>
        Protected Sub displayRecordAttachment(ByVal productID As Long)
            Try
                trRecordAttachment.Visible = True
                Me.RecordAttachments__c.Visible = True
                Dim lEntityId As Long
                lEntityId = CLng(Me.AptifyApplication.GetEntityID("Products"))
                LoadDataFromGE(Me.AptifyApplication.GetEntityObject("Products", productID))
                Me.RecordAttachments__c.AllowAdd = False
                Me.RecordAttachments__c.AttachmentCategory = AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "PrePurchase Downloads")
                Me.RecordAttachments__c.LoadAttachments(lEntityId, productID)
                Dim grdAttachments As RadGrid = TryCast(Me.RecordAttachments__c.FindControl("grdAttachments"), RadGrid)
                If Not grdAttachments Is Nothing AndAlso grdAttachments.Items.Count > 0 Then
                Else
                    trRecordAttachment.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        ''' <summary>
        ''' Strips the HTML. Added By Swati for issue #16796 Product - Strip web description html tags
        ''' </summary>
        ''' <param name="HTMLText">The HTML text.</param>
        ''' <returns></returns>
        Public Shared Function GetBodyHTML(HTMLText As String) As String
            ' Dim reg = New Regex("<[^>]+>", RegexOptions.IgnoreCase)
            'Dim reg = New Regex("<[^>]+>", RegexOptions.IgnoreCase)
            'Return reg.Replace(HTMLText, "")

            Dim options As RegexOptions = RegexOptions.IgnoreCase Or RegexOptions.Singleline
            Dim regx As New Regex("<body>(?<theBody>.*)</body>", options)

            Dim match As Match = regx.Match(HTMLText)

            If match.Success Then
                Return match.Groups("theBody").Value
            End If
        End Function
#End Region
    End Class
End Namespace
