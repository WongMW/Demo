'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Abhishek Tapkir            1/23/2015                           Created control to add links
'Siddharth Kavitake         1/28/2015                           Added condition to check big firm 
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Namespace Aptify.Framework.Web.eBusiness
Partial Class UserControls_Aptify_Custom__c_ManageMyGroup__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_MANAGEMYGROUP_PAGE As String = "ManageMyGroup"
    Protected Const ATTRIBUTE_ADDMEMBER As String = "AddMember"
    Protected Const ATTRIBUTE_PURCHASEMEMBER As String = "PurchaseMember"
    Protected Const ATTRIBUTE_COMPANYPROFILE As String = "CompanyProfile"
    Protected Const ATTRIBUTE_RENEWMEMBER As String = "RenewMember"
    Protected Const ATTRIBUTE_ORDERHISTORY As String = "OrderHistory"
    Protected Const ATTRIBUTE_COMPANYDIRECTORY As String = "CompanyDirectory"

    Protected Const ATTRIBUTE_EVENTREGISTRATION As String = "EventRegistration"
    Protected Const ATTRIBUTE_SUBSTITUTEATTENDEE As String = "SubstituteAttendee"
    Protected Const ATTRIBUTE_MEETINGTRANSFER As String = "MeetingTransfer"

    Protected Const ATTRIBUTE_WHOPAYS As String = "WhoPays"
    Protected Const ATTRIBUTE_BIGFIRM As String = "BigFirm"
    Protected Const ATTRIBUTE_FIRMRESULT As String = "FirmResult"
    Protected Const ATTRIBUTE_FIRMCHANGESESSIONTOAUTUMN As String = "FirmChangeSessionToAutumn"

    Protected Const ATTRIBUTE_PAYOFFORDER As String = "PayOffOrder"
    Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage" 'Added by Sandeep for Issue 15051 on 12/03/2013
    ''Added BY Pradip 2015-07-13 For Firm Contract Registration Page Link
    Protected Const ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL As String = "FirmContractRegistrationPage"
    Protected Const ATTRIBUTE_QUOTAAPPLICATIONPAGE_URL As String = "QuotaApplicationPage"
    ''Added By Pradip 2015-09-17 For Firm Admin Dashboard 
    Protected Const ATTRIBUTE_FIRMADMINDASHBOARDPAGE_URL As String = "FirmAdminDashboardPage"
    ''Added By Pradip 2015-10-09 For Business Unit
    Protected Const ATTRIBUTE_BUPAGE_URL As String = "BusinessUnitPage"

#Region " Specific Properties"
    ''' <summary>
    ''' Manage My Group page url
    ''' </summary>
    Public Overridable Property ManageMyGroup() As String
        Get
            If Not ViewState(ATTRIBUTE_MANAGEMYGROUP_PAGE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_MANAGEMYGROUP_PAGE))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_MANAGEMYGROUP_PAGE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    ''' <summary>
    ''' Links for Menu Options
    ''' </summary>
    Public Overridable Property AddMember() As String
        Get
            If Not ViewState(ATTRIBUTE_ADDMEMBER) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_ADDMEMBER))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_ADDMEMBER) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property PurchaseMember() As String
        Get
            If Not ViewState(ATTRIBUTE_PURCHASEMEMBER) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_PURCHASEMEMBER))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_PURCHASEMEMBER) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property CompanyProfile() As String
        Get
            If Not ViewState(ATTRIBUTE_COMPANYPROFILE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_COMPANYPROFILE))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_COMPANYPROFILE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property RenewMember() As String
        Get
            If Not ViewState(ATTRIBUTE_RENEWMEMBER) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_RENEWMEMBER))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_RENEWMEMBER) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property OrderHistory() As String
        Get
            If Not ViewState(ATTRIBUTE_ORDERHISTORY) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_ORDERHISTORY))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_ORDERHISTORY) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property CompanyDirectory() As String
        Get
            If Not ViewState(ATTRIBUTE_COMPANYDIRECTORY) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_COMPANYDIRECTORY))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_COMPANYDIRECTORY) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property EventRegistration() As String
        Get
            If Not ViewState(ATTRIBUTE_EVENTREGISTRATION) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_EVENTREGISTRATION))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_EVENTREGISTRATION) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property MeetingTransfer() As String
        Get
            If Not ViewState(ATTRIBUTE_MEETINGTRANSFER) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_MEETINGTRANSFER))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_MEETINGTRANSFER) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property WhoPays() As String
        Get
            If Not ViewState(ATTRIBUTE_WHOPAYS) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_WHOPAYS))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_WHOPAYS) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property BigFirm() As String
        Get
            If Not ViewState(ATTRIBUTE_BIGFIRM) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_BIGFIRM))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_BIGFIRM) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property FirmResult() As String
        Get
            If Not ViewState(ATTRIBUTE_FIRMRESULT) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_FIRMRESULT))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_FIRMRESULT) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property FirmChangeSessionToAutumn() As String
        Get
            If Not ViewState(ATTRIBUTE_FIRMCHANGESESSIONTOAUTUMN) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_FIRMCHANGESESSIONTOAUTUMN))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_FIRMCHANGESESSIONTOAUTUMN) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property SubstituteAttendee() As String
        Get
            If Not ViewState(ATTRIBUTE_SUBSTITUTEATTENDEE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_SUBSTITUTEATTENDEE))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_SUBSTITUTEATTENDEE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property PayOffOrder() As String
        Get
            If Not ViewState(ATTRIBUTE_PAYOFFORDER) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_PAYOFFORDER))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_PAYOFFORDER) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    'Nalini issue 11290

    'Added by Sandeep for Issue 15051 on 12/03/2013
    Public Overridable Property LoginPage() As String
        Get
            If Not ViewState(ATTRIBUTE_LOGIN_PAGE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_LOGIN_PAGE))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_LOGIN_PAGE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property


    ''Added BY Pradip 2015-07-03
    Public Overridable Property FirmContractRegistrationPage() As String
        Get
            If Not ViewState(ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    ''Added BY Govind 2015-07-17
    Public Overridable Property QuotaApplicationPage() As String
        Get
            If Not ViewState(ATTRIBUTE_QUOTAAPPLICATIONPAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_QUOTAAPPLICATIONPAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_QUOTAAPPLICATIONPAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    ''Added BY Pradip 2015-07-03 for Firm Admin Dashboard
    Public Overridable Property FirmAdminDashboardPage() As String
        Get
            If Not ViewState(ATTRIBUTE_FIRMADMINDASHBOARDPAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_FIRMADMINDASHBOARDPAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_FIRMADMINDASHBOARDPAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    ''Added BY Pradip 2015-10-05
    Public Overridable Property BUPage() As String
        Get
            If Not ViewState(ATTRIBUTE_BUPAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_BUPAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_BUPAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
#End Region

    Protected Overrides Sub SetProperties()

        If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_MANAGEMYGROUP_PAGE
        'call base method to set parent properties
        MyBase.SetProperties()

        If String.IsNullOrEmpty(ManageMyGroup) Then
            'since value is the 'default' check the XML file for possible custom setting
            ManageMyGroup = Me.GetLinkValueFromXML(ATTRIBUTE_MANAGEMYGROUP_PAGE)
            If String.IsNullOrEmpty(ManageMyGroup) Then
                'Do Nothing
            End If
        End If
        If String.IsNullOrEmpty(AddMember) Then
            'since value is the 'default' check the XML file for possible custom setting
            AddMember = Me.GetLinkValueFromXML(ATTRIBUTE_ADDMEMBER)
            If String.IsNullOrEmpty(AddMember) Then
                Me.lnkAddMember.Enabled = False
                Me.lnkAddMember.ToolTip = "MeetingPage property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(PurchaseMember) Then
            'since value is the 'default' check the XML file for possible custom setting
            PurchaseMember = Me.GetLinkValueFromXML(ATTRIBUTE_PURCHASEMEMBER)
            If String.IsNullOrEmpty(PurchaseMember) Then
                Me.lnkPurchaseMembership.Enabled = False
                Me.lnkPurchaseMembership.ToolTip = "MeetingPage property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(CompanyProfile) Then
            'since value is the 'default' check the XML file for possible custom setting
            CompanyProfile = Me.GetLinkValueFromXML(ATTRIBUTE_COMPANYPROFILE)
            If String.IsNullOrEmpty(CompanyProfile) Then
                Me.lnkCompanyProfile.Enabled = False
                Me.lnkCompanyProfile.ToolTip = "MeetingPage property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(RenewMember) Then
            'since value is the 'default' check the XML file for possible custom setting
            RenewMember = Me.GetLinkValueFromXML(ATTRIBUTE_RENEWMEMBER)
            If String.IsNullOrEmpty(RenewMember) Then
                Me.lnkRenewMembership.Enabled = False
                Me.lnkRenewMembership.ToolTip = "MeetingPage property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(OrderHistory) Then
            'since value is the 'default' check the XML file for possible custom setting
            OrderHistory = Me.GetLinkValueFromXML(ATTRIBUTE_ORDERHISTORY)
            If String.IsNullOrEmpty(OrderHistory) Then
                Me.lnkOrderHistory.Enabled = False
                Me.lnkOrderHistory.ToolTip = "MeetingPage property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(CompanyDirectory) Then
            'since value is the 'default' check the XML file for possible custom setting
            CompanyDirectory = Me.GetLinkValueFromXML(ATTRIBUTE_COMPANYDIRECTORY)
            If String.IsNullOrEmpty(OrderHistory) Then
                Me.lnkCmpDirectory.Enabled = False
                Me.lnkCmpDirectory.ToolTip = "MeetingPage property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(EventRegistration) Then
            'since value is the 'default' check the XML file for possible custom setting
            EventRegistration = Me.GetLinkValueFromXML(ATTRIBUTE_EVENTREGISTRATION)
            If String.IsNullOrEmpty(EventRegistration) Then
                Me.lnkEventRegistration.Enabled = False
                Me.lnkEventRegistration.ToolTip = "MeetingPage property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(SubstituteAttendee) Then
            'since value is the 'default' check the XML file for possible custom setting
            SubstituteAttendee = Me.GetLinkValueFromXML(ATTRIBUTE_SUBSTITUTEATTENDEE)
            If String.IsNullOrEmpty(SubstituteAttendee) Then
                Me.lnkMeetingAttendee.Enabled = False
                Me.lnkMeetingAttendee.ToolTip = "MeetingPage property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(MeetingTransfer) Then
            'since value is the 'default' check the XML file for possible custom setting
            MeetingTransfer = Me.GetLinkValueFromXML(ATTRIBUTE_MEETINGTRANSFER)
            If String.IsNullOrEmpty(MeetingTransfer) Then
                Me.lnkMeetingTransfer.Enabled = False
                Me.lnkMeetingTransfer.ToolTip = "MeetingPage property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(WhoPays) Then
            'since value is the 'default' check the XML file for possible custom setting
            WhoPays = Me.GetLinkValueFromXML(ATTRIBUTE_WHOPAYS)
            If String.IsNullOrEmpty(WhoPays) Then
                Me.lnkWhoPays.Enabled = False
                Me.lnkWhoPays.ToolTip = "WhoPays property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(BigFirm) Then
            'since value is the 'default' check the XML file for possible custom setting
            BigFirm = Me.GetLinkValueFromXML(ATTRIBUTE_BIGFIRM)
            If String.IsNullOrEmpty(BigFirm) Then
                Me.lnkBigFirm.Enabled = False
                Me.lnkBigFirm.ToolTip = "BigFirm property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(FirmResult) Then
            'since value is the 'default' check the XML file for possible custom setting
            FirmResult = Me.GetLinkValueFromXML(ATTRIBUTE_FIRMRESULT)
            If String.IsNullOrEmpty(FirmResult) Then
                Me.lnkFirmResult.Enabled = False
                Me.lnkFirmResult.ToolTip = "FirmResult property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(FirmChangeSessionToAutumn) Then
            'since value is the 'default' check the XML file for possible custom setting
            FirmChangeSessionToAutumn = Me.GetLinkValueFromXML(ATTRIBUTE_FIRMCHANGESESSIONTOAUTUMN)
            If String.IsNullOrEmpty(FirmChangeSessionToAutumn) Then
                Me.lnkFirmChangeSessionToAutumn.Enabled = False
                Me.lnkFirmChangeSessionToAutumn.ToolTip = "FirmChangeSessionToAutumn property has not been set."
            End If
        End If
        If String.IsNullOrEmpty(PayOffOrder) Then
            'since value is the 'default' check the XML file for possible custom setting
            PayOffOrder = Me.GetLinkValueFromXML(ATTRIBUTE_PAYOFFORDER)
            If String.IsNullOrEmpty(PayOffOrder) Then
                Me.lnkPayOffOrder.Enabled = False
                Me.lnkPayOffOrder.ToolTip = "MeetingPage property has not been set."
            End If
        End If
        'Added by Sandeep for Issue 15051 on 12/03/2013
        If String.IsNullOrEmpty(LoginPage) Then
            'since value is the 'default' check the XML file for possible custom setting
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_LOGIN_PAGE)
        End If


        ''Added BY Pradip 2015-07-13 For Firm Contract Registration Page Link
        If String.IsNullOrEmpty(FirmContractRegistrationPage) Then
            FirmContractRegistrationPage = Me.GetLinkValueFromXML(ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL)
            Me.lnkFirmContractRegistration.NavigateUrl = FirmContractRegistrationPage
        Else
            Me.lnkFirmContractRegistration.NavigateUrl = FirmContractRegistrationPage
        End If

        ''Added BY Govind Mande 2015-07-17 For Quota Application Page Link
        If String.IsNullOrEmpty(QuotaApplicationPage) Then
            QuotaApplicationPage = Me.GetLinkValueFromXML(ATTRIBUTE_QUOTAAPPLICATIONPAGE_URL)
            Me.lnkQuotaApp.NavigateUrl = QuotaApplicationPage
        Else
            Me.lnkQuotaApp.NavigateUrl = QuotaApplicationPage
        End If


        ''Added BY Pradip 2015-09-08 For Firm Firm Admin Dashboard Page Link
        If String.IsNullOrEmpty(FirmAdminDashboardPage) Then
            FirmAdminDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_FIRMADMINDASHBOARDPAGE_URL)
            Me.lnkFirmAdminDashboard.NavigateUrl = FirmAdminDashboardPage
        Else
            Me.lnkFirmAdminDashboard.NavigateUrl = FirmAdminDashboardPage
        End If
        ''Added BY Pradip 2015-10-05 For Business Unit Assignment  Page Link
        If String.IsNullOrEmpty(BUPage) Then
            BUPage = Me.GetLinkValueFromXML(ATTRIBUTE_BUPAGE_URL)
            Me.lnkBU.NavigateUrl = BUPage
        Else
            Me.lnkBU.NavigateUrl = BUPage
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetProperties()
            lnkAddMember.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_ADDMEMBER)
            lnkCompanyProfile.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_COMPANYPROFILE)
            lnkOrderHistory.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_ORDERHISTORY)
            lnkPurchaseMembership.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_PURCHASEMEMBER)
            lnkRenewMembership.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_RENEWMEMBER)
            lnkCmpDirectory.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_COMPANYDIRECTORY)
            lnkEventRegistration.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_EVENTREGISTRATION)
            lnkMeetingAttendee.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_SUBSTITUTEATTENDEE)
            lnkMeetingTransfer.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_MEETINGTRANSFER)
            lnkWhoPays.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_WHOPAYS)
            lnkBigFirm.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_BIGFIRM)
            lnkFirmResult.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_FIRMRESULT)
            lnkFirmChangeSessionToAutumn.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_FIRMCHANGESESSIONTOAUTUMN)
            lnkPayOffOrder.NavigateUrl = Me.GetLinkValueFromXML(ATTRIBUTE_PAYOFFORDER)

            If User1.CompanyID > 0 Then
                Dim sSQL As String = Database & "..spCheckIsBigFirmCompany__c @CompanyID=" & User1.CompanyID.ToString()
                Dim iCompanyID As Integer = DataAction.ExecuteScalar(sSQL, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If iCompanyID > 0 Then
                    imgFirmChangeSessionToAutumn.Visible = True
                    lnkFirmChangeSessionToAutumn.Visible = True
                    Image8.Visible = True
                    
                Else
                    imgFirmChangeSessionToAutumn.Visible = False
                    lnkFirmChangeSessionToAutumn.Visible = False
                    Image8.Visible = False
                End If
                IsCompanyRTO()
                ''' Added By Govind 2015-07-17 To Check Company Is RTO Firm then only show Quota Application page link
                Session("MentorID") = Nothing
                Dim sSqlCompanyRTO As String = Database & "..spIsCompanyRTO__c @CompanyID=" & User1.CompanyID
                Dim iIsCompanyRTOFirm As Integer = DataAction.ExecuteScalar(sSqlCompanyRTO, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If iIsCompanyRTOFirm > 0 Then
                    lnkQuotaApp.Visible = True
                    imgQuotaApp.Visible = True
                Else
                    lnkQuotaApp.Visible = False
                    imgQuotaApp.Visible = False
                End If
            Else
                imgFirmChangeSessionToAutumn.Visible = False
                lnkFirmChangeSessionToAutumn.Visible = False
                Image8.Visible = False
                imgFirmChangeSessionToAutumn.Visible = False
                lnkFirmChangeSessionToAutumn.Visible = False
                Image8.Visible = False
            End If
            ''Added By Pradip 2015-07-13 To Check Logged in person is Firm Admin or have "Training Manager as primary function
            Dim ssSQL As String = Database & "..spCheckFirmAdminTrainingManager__c @PersonID=" & User1.PersonID.ToString()
            Dim iCheck As Integer = DataAction.ExecuteScalar(ssSQL, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If iCheck > 0 Then
                lnkFirmContractRegistration.Visible = True
                imgFirmCont.Visible = True
                ''Added By Pradip  2015-09-09 For Firm Admin Dashboard Link
                lnkFirmAdminDashboard.Visible = True
                imgFirmAdminDashboard.Visible = True
                ''Added By Pradip  for business unit link / page access
                CheckBUPageAccess()
            Else
                lnkFirmContractRegistration.Visible = False
                imgFirmCont.Visible = False
                ''Added By Pradip  2015-09-09 For Firm Admin Dashboard Link
                lnkFirmAdminDashboard.Visible = False
                imgFirmAdminDashboard.Visible = False
                imgBU.Visible = False
                lnkBU.Visible = False
            End If

        End If
        If User1.UserID < 0 Then
            Response.Redirect(LoginPage) 'Added by Sandeep for Issue 15051 on 12/03/2013
        End If
    End Sub

    Private Sub IsCompanyRTO()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spIsBigFirmCompanyRTO__c @CompanyID={1}", Database, User1.CompanyID.ToString())
            Dim companyId As Integer = DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If companyId > 0 Then
                imgStudentResultPage.Visible = True
                lnkFirmResult.Visible = True
                Image26.Visible = True
            Else
                imgStudentResultPage.Visible = False
                lnkFirmResult.Visible = False
                Image26.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Added By Pradip For BUsiness Unit Link / Page Access
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckBUPageAccess()
        Try
            Dim ssSQL As String = Database & "..spCheckBULinkAccess__c @CompanyID=" & User1.CompanyID.ToString()
            Dim iCheck As Integer = DataAction.ExecuteScalar(ssSQL, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If iCheck > 0 Then
                imgBU.Visible = True
                lnkBU.Visible = True
            Else
                imgBU.Visible = False
                lnkBU.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class
'End Namespace
