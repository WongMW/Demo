'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Siddharth Kavitake         16-Mar-2015                          Student enrollment page
'Govind Mande               08-April-2015                        Student enrollment page functionality
'Sheela Jarali              14-May-2018                          Exemption Section display logic(CNM-9)
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Imports System.Drawing
Imports Aptify.Applications.OrderEntry
Imports Aptify.Applications.Accounting
Imports Aptify.Applications.OrderEntry.Payments

Partial Class Enrollment

    Private _recordID As Integer
    Public Property RecordID() As Integer
        Get
            Return _recordID
        End Get
        Set(ByVal value As Integer)
            _recordID = value
        End Set
    End Property

    Private _curriculumId As Integer
    Public Property CurriculumID() As Integer
        Get
            Return _curriculumId
        End Get
        Set(ByVal value As Integer)
            _curriculumId = value
        End Set
    End Property

    Private _subjectId As Integer
    Public Property SubjectID() As Integer
        Get
            Return _subjectId
        End Get
        Set(ByVal value As Integer)
            _subjectId = value
        End Set
    End Property

    Private _classType As String
    Public Property ClassType() As String
        Get
            Return _classType
        End Get
        Set(ByVal value As String)
            _classType = value
        End Set
    End Property

    Private _classId As Integer
    Public Property ClassID() As Integer
        Get
            Return _classId
        End Get
        Set(ByVal value As Integer)
            _classId = value
        End Set
    End Property

    Private _productId As Integer
    Public Property ProductID() As Integer
        Get
            Return _productId
        End Get
        Set(ByVal value As Integer)
            _productId = value
        End Set
    End Property

    Private _alternateGroupId As String
    Public Property AlternateGroupID() As String
        Get
            Return _alternateGroupId
        End Get
        Set(ByVal value As String)
            _alternateGroupId = value
        End Set
    End Property

    Private _units As Double
    Public Property Units() As Double
        Get
            Return _units
        End Get
        Set(ByVal value As Double)
            _units = value
        End Set
    End Property

    Private _cutOffUnits As Double
    Public Property CutOffUnits() As Double
        Get
            Return _cutOffUnits
        End Get
        Set(ByVal value As Double)
            _cutOffUnits = value
        End Set
    End Property

    Private _isChecked As Boolean
    Public Property IsChecked() As Boolean
        Get
            Return _isChecked
        End Get
        Set(ByVal value As Boolean)
            _isChecked = value
        End Set
    End Property
    Private _selectedUnits As Double
    Public Property SelectedUnits() As Double
        Get
            Return _selectedUnits
        End Get
        Set(ByVal value As Double)
            _selectedUnits = value
        End Set
    End Property
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(record As Integer, curriculumId As Integer, subjectId As Integer, classId As Integer, _
                   productId As Integer, alternateGroupId As String, _
                   cutOffUnits As Double, units As Double, classType As String, isUnerolled As Boolean, isFailed As Boolean, FailedUnits As Double, FirstAttempt As Double, selectedUnits As Double, SessionWise As String)
        Me.RecordID = record
        Me.CurriculumID = curriculumId
        Me.SubjectID = subjectId
        Me.ClassID = classId
        Me.ProductID = productId
        Me.AlternateGroupID = alternateGroupId
        Me.CutOffUnits = cutOffUnits
        Me.Units = units
        Me.ClassType = classType
        Me.IsChecked = isUnerolled
        Me.IsFailed = isFailed
        Me.FailedUnits = FailedUnits
        Me.FirstAttempt = FirstAttempt
        Me.SelectedUnits = selectedUnits
        Me.Sessionwise = SessionWise
    End Sub


    Private _isFailed As Boolean
    Public Property IsFailed() As Boolean
        Get
            Return _isFailed
        End Get
        Set(ByVal value As Boolean)
            _isFailed = value
        End Set
    End Property


    Private _EnrolledUnits As Double
    Public Property EnrolledUnits() As Double
        Get
            Return _EnrolledUnits
        End Get
        Set(ByVal value As Double)
            _EnrolledUnits = value
        End Set
    End Property

    Private _FailedUnits As Double
    Public Property FailedUnits() As Double
        Get
            Return _FailedUnits
        End Get
        Set(ByVal value As Double)
            _FailedUnits = value
        End Set
    End Property

    Private _FirstAttempt As Double
    Public Property FirstAttempt() As Double
        Get
            Return _FirstAttempt
        End Get
        Set(ByVal value As Double)
            _FirstAttempt = value
        End Set
    End Property

    Private _Sessionwise As String
    Public Property Sessionwise() As String
        Get
            Return _Sessionwise
        End Get
        Set(ByVal value As String)
            _Sessionwise = value
        End Set
    End Property
End Class
Partial Class StudentEnrollment__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage As String = "ProfilePage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_PaymentSummeryPage As String = "PaymentSummeryPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_EEAppPage As String = "ExemptionApplicationPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_HomePage As String = "HomePage"
    Dim sDeletedID As String = String.Empty
    Dim _studentEnrollmentDetails As New DataTable()
    Dim bAddedStudentGrp As Boolean = False
    Dim bCheckWhoesAutoSelect As Boolean = False
    Dim sAutoEnrolledSelection As String
    Dim dtSesstionSummeryPaymentSummery As DataTable
    Private _enrollmentList As New List(Of Enrollment)

#Region "Property Setting"
    Public Overridable Property LoginPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property ProfilePage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property PaymentSummeryPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_PaymentSummeryPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_PaymentSummeryPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_PaymentSummeryPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property EEAppPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EEAppPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EEAppPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property HomePage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_HomePage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_HomePage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_HomePage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Protected Overrides Sub SetProperties()
        If String.IsNullOrEmpty(LoginPage) Then
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
        End If
        If String.IsNullOrEmpty(ProfilePage) Then
            ProfilePage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage)
        End If
        If String.IsNullOrEmpty(PaymentSummeryPage) Then
            PaymentSummeryPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_PaymentSummeryPage)
        End If
        If String.IsNullOrEmpty(EEAppPage) Then
            EEAppPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_EEAppPage)
        End If

        If String.IsNullOrEmpty(HomePage) Then
            HomePage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_HomePage)
        End If
        If Request.QueryString("Eid") IsNot Nothing AndAlso Convert.ToInt32(Request.QueryString("Eid")) > 0 Then
            Session("EID") = Request.QueryString("Eid")
        End If
    End Sub

#End Region

#Region "Page Events    "
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim s As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("1")
            lblMsgWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.Message")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblStdgrp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.TimeTable")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblCentrallyMngError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.CentrallyManagedErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

            lblAvailable.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.Current Options for EnrolmentMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblNotAvailable.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.Not AvailableMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblEnrollmentExists.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.Current Option for Core Course Msg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblAlreadyEnrolled.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.Already Enrolled Msg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

            SetProperties()
            If AptifyEbusinessUser1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            Else
                dtSesstionSummeryPaymentSummery = CType(Session("SummerPaymentSummery"), DataTable)
                If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                    hdnEnrollmentOptionsState.Value = 1
                End If
                'If CheckDisplayCourseEnrollLink() Then
                '    If Request.QueryString("PR") IsNot Nothing AndAlso Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PR")) = "1" Then
                '        Session("ReturnToPage") = Request.Path
                '        Response.Redirect(ProfilePage)
                '    End If
                If Not IsPostBack Then
                    If Not CheckStudentEligibility() Then
                        'If Request.QueryString("PR") IsNot Nothing AndAlso Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PR")) = "1" Then
                        '    Session("ReturnToPage") = Request.Path
                        '    Response.Redirect(ProfilePage)
                        'End If
                        'redirect to ee application

                        If Convert.ToBoolean(ViewState("FirmApproavlBlank")) Then
                            lblValidation.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusinessStudentEnrollment.msgFirmApprovalStatusBlank")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radWindowValidation.VisibleOnPageLoad = True
                            pnlDetails.Visible = False
                        Else
                            Response.Redirect(EEAppPage, False)
                        End If


                    End If
                    If CheckStudentResultPublishDate() Then
                        lblValidation.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusinessStudentEnrollment.msgResultPublishDate__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        radWindowValidation.VisibleOnPageLoad = True
                        pnlDetails.Visible = False
                    Else
                        pnlDetails.Visible = True
                        CheckIsFAEEnrolWithElective() 'Redmine #15540
                        Session("AlreadySubmit") = False
                        SummerPaymentSummery = Nothing
                        GetPrefferedCurrency()
                        CreditCard.LoadCreditCardInfo()
                        LoadCurrentAcademicCycle()
                        ' LoadNextAcademicCycle()
                        LoadRoutes()
                        '  LoadPaymentPlan()
                        LoadHeaderText()
                        LoadApplicationDetails()
                        LoadExemptionGranted()
                        LoadStudentGroup()
                        CheckAlreadyEnrolled(ViewState("AcademicCycleID"))
                        ''''Commented BY Pradip 2016-05-14 as CheckStudentEnrollFirstTime not used any where 
                        ''CheckStudentEnrollFirstTime()

                        ''This Code Needs to Comment Not Neccesary
                        ''This Fucnction is Commented By Pradip  2016-02-15 
                        'LoadCAP1Courses()

                        'CheckFirmCentrallyManaged()
                        If Not Session("PlaceOrder") Is Nothing AndAlso CBool(Session("PlaceOrder")) Then
                            btnPrint.Visible = True
                        End If
                        '  CODE added by Govind M for Redmine #16769
                        If Convert.ToBoolean(ViewState("FirmStatusIsRejected")) Then
                            HideEnrollmentDetails()
                        End If
                    End If
                End If
                '    Else
                '    'Not allowed
                '    Response.Redirect(ProfilePage, False)
                'End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Checks the is fae enrol with elective. Redmine Issue #15540
    ''' </summary>
    Private Sub CheckIsFAEEnrolWithElective()
        Try
            Dim sSQL As String = Database & "..spCheckFAEEnrollWithElective__c @StudentID=" & AptifyEbusinessUser1.PersonID
            Dim isFAEEnrolWithElective As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If isFAEEnrolWithElective Then
                ViewState("IsFAEElective") = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub CheckFirmCentrallyManaged()
        Try
            Dim bCentrallyManaged As Boolean = False
            If ViewState("CentrallyManaged") Then
                For Each row As Telerik.Web.UI.GridItem In gvCurriculumCourse.Items
                    Dim chkClassRoom As CheckBox = DirectCast(row.FindControl("chkClassRoom"), CheckBox)
                    Dim chkRevision As CheckBox = DirectCast(row.FindControl("chkRevision"), CheckBox)
                    Dim chkInterimAssessment As CheckBox = DirectCast(row.FindControl("chkInterimAssessment"), CheckBox)
                    Dim chkMockExam As CheckBox = DirectCast(row.FindControl("chkMockExam"), CheckBox)
                    Dim chkSummerExam As CheckBox = DirectCast(row.FindControl("chkSummerExam"), CheckBox)
                    Dim chkRepeatRevision As CheckBox = DirectCast(row.FindControl("chkRepeatRevision"), CheckBox)
                    Dim chkResitInterimAssessment As CheckBox = DirectCast(row.FindControl("chkResitInterimAssessment"), CheckBox)
                    Dim chkAutumnExam As CheckBox = DirectCast(row.FindControl("chkAutumnExam"), CheckBox)
                    If chkClassRoom.Enabled OrElse chkRevision.Enabled OrElse chkInterimAssessment.Enabled OrElse chkMockExam.Enabled OrElse chkSummerExam.Enabled OrElse chkRepeatRevision.Enabled OrElse chkResitInterimAssessment.Enabled OrElse chkAutumnExam.Enabled Then
                        bCentrallyManaged = True
                        Exit For
                    End If
                Next
                If bCentrallyManaged Then
                    btnDisplayPaymentSummey.Visible = True
                Else
                    btnDisplayPaymentSummey.Visible = False
                    lblEnrolmsg.Visible = False
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub CheckAlreadyEnrolled(ByVal AcademicCycleID As Integer)
        Try
            Dim sSql As String = Database & "..spCheckAlreadyStudentEnrolledCourses__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@AcadmicCycleID=" & AcademicCycleID
            Dim lStudentID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If lStudentID > 0 Then
                ddlCAP1StudGrp.Enabled = False
            Else
                ' Redmine issue #16489 ' for FAE enrolment if class registration is registered 
                Dim sSqlFAE As String = Database & "..spCheckAlreadyStudentEnrolledFAECourses__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@AcadmicCycleID=" & AcademicCycleID
                Dim lStudentFAEID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlFAE, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lStudentFAEID > 0 Then
                    ddlCAP1StudGrp.Enabled = False
                    btnDisplayPaymentSummey.Visible = False
                    lblEnrolmsg.Visible = False
                Else
                    ddlCAP1StudGrp.Enabled = True
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub CheckStudentEnrollFirstTime()
        Try
            Dim sSql As String = Database & "..spStudentEnrollFirstTime__c @StudentID=" & AptifyEbusinessUser1.PersonID
            Dim bIsFirstTimeEnroll As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If bIsFirstTimeEnroll Then
                ViewState("EnrollFirstTime") = True
            Else
                ViewState("EnrollFirstTime") = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub LoadCurrentAcademicCycle()
        Try
            Dim sSql As String = Database & "..spCommonCurrentAcadmicCycle__c"
            'spCommonCurrentAcadmicCycle__c
            Dim iCurrentAcademicCycleID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If iCurrentAcademicCycleID > 0 Then
                ViewState("AcademicCycleID") = iCurrentAcademicCycleID
            End If
            ViewState("CurrentAcademicCycle") = AptifyApplication.GetEntityRecordName("AcademicCycles__c", iCurrentAcademicCycleID)

            'Dim sNextAcademicCycle As String = Database & "..spGetNextAcademicCycleStudentEnrollment__c @AcademicCycleID=" & iCurrentAcademicCycleID
            'Dim iNextAcademicCycleID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sNextAcademicCycle, IAptifyDataAction.DSLCacheSetting.BypassCache))
            'lblNextAcademicCycleName.Text = AptifyApplication.GetEntityRecordName("AcademicCycles__c", iNextAcademicCycleID)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub LoadNextAcademicCycle()
        Try
            Dim sSql As String = "..spGetNextAcademicCycleDetails__c"
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                'rdoNextAcadmicCycle.Text = "Next Academic Cycle: " + Convert.ToString(dt.Rows(0)("Name"))
                ViewState("AcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList, _
                                ByVal sValue As String)
        Dim i As Integer

        Try
            For i = 0 To cmb.Items.Count - 1
                If String.Compare(cmb.Items(i).Value, sValue, True) = 0 Then
                    cmb.Items(i).Selected = True
                    Exit Sub
                End If
                '11/27/07,Added by Tamasa,Issue 5222.
                If String.Compare(cmb.Items(i).Text, sValue, True) = 0 Then
                    cmb.Items(i).Selected = True
                    Exit Sub
                End If
                'End
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Load Drop Downs"
    Private Sub LoadRoutes()
        Try
            Dim sSql As String = Database & "..spGetApplicationTypeForCACurriculumRoute__c"
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ddlRoute.DataSource = dt
                ddlRoute.DataTextField = "Name"
                ddlRoute.DataValueField = "ID"
                ddlRoute.DataBind()
                ddlRoute.Items.Insert(0, "Select One")
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub LoadPaymentPlan(ByVal sProduct As String)
        Try
            Dim sSql As String = Database & "..spGetPaymentPlanForStudentEnrollment__c @ProductIds='" & sProduct & "'"
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ddlPaymentPlan.DataSource = dt
                ddlPaymentPlan.DataTextField = "Name"
                ddlPaymentPlan.DataValueField = "ID"
                ddlPaymentPlan.DataBind()
                ' radPaymentPlanDetails.Visible = True
            Else
                ' radPaymentPlanDetails.Visible = False
            End If
            ddlPaymentPlan.Items.Insert(0, "Select Payment Plan")
            radPaymentPlanDetails.Visible = False

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub LoadStudentGroup()
        Try

            ' Redmine issue 15011
            Dim iCurrentAcademicCycle As Integer
            Dim iNextAcademicCycle As Integer
            Dim sSqlAcademicCycle As String = Database & "..spGetCurrentNextAcademicCycles__c"
            Dim dtAcademicDetails As DataTable = DataAction.GetDataTable(sSqlAcademicCycle, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtAcademicDetails Is Nothing AndAlso dtAcademicDetails.Rows.Count > 0 Then
                iCurrentAcademicCycle = Convert.ToInt32(dtAcademicDetails.Rows(0)("ID"))
                If dtAcademicDetails.Rows.Count >= 1 Then
                    iNextAcademicCycle = Convert.ToInt32(dtAcademicDetails.Rows(1)("ID"))
                Else
                    iNextAcademicCycle = iCurrentAcademicCycle
                End If
            End If
            'spGetTimeTableAsPerAcademicCycle__c
            Dim sSql As String = Database & "..spGetTimeTableAsPerAcademicCycle__c"
            'Dim sSql As String = Database & "..spGetStudentGroupForCourses__c"
            Dim param(3) As IDataParameter
            param(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, hdnCompanyId.Value)
            param(1) = DataAction.GetDataParameter("@CurrentAcademicCycle", SqlDbType.Int, iCurrentAcademicCycle)
            param(2) = DataAction.GetDataParameter("@NextAcademicCycle", SqlDbType.Int, iNextAcademicCycle)
            param(3) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, AptifyEbusinessUser1.PersonID)
            Dim dt As DataTable = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)


            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ddlCAP1StudGrp.Items.Clear()

                ddlCAP1StudGrp.DataSource = dt
                ddlCAP1StudGrp.DataTextField = "Name"
                ddlCAP1StudGrp.DataValueField = "ID"
                ddlCAP1StudGrp.DataBind()
                'Added BY Govind Mande 2016-05-16 For Redmine Issue Task #13216

                ' '' ''    'Commented and Added BY Pradip 2016-05-14 For Redmine Issue Task #13216
                ' '' ''    If Not dt Is Nothing Then
                ' '' ''        For Each dr As DataRow In dt.Rows
                ' '' ''            ddlCAP1StudGrp.Items.Add(New System.Web.UI.WebControls.ListItem(dr("Name").ToString.Trim, dr("ID").ToString))
                ' '' ''        Next
                ' '' ''    End If
            End If
            ddlCAP1StudGrp.Items.Insert(0, "Select Student Group")

            ' '' ''ddlCAP1StudGrp.Items.Insert(0, "")
            ' '' ''ddlCAP1StudGrp.SelectedIndex = 0
            ' '' '' ''End Here Commented and Added BY Pradip 2016-05-14 For Redmine Issue Task #13216
            ' Code commentd by govind 7 APril2016
            ''Dim sSqlExistsStudentGrp As String = Database & "..spGetStudentGrpEnrolled__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@AcademicCycleID=" & ViewState("AcademicCycleID")
            ''Dim sStudentGrp As String = Convert.ToString(DataAction.ExecuteScalar(sSqlExistsStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
            ''If sStudentGrp.Trim <> "" Then
            ''    SetComboValue(ddlCAP1StudGrp, sStudentGrp)
            ''End If

            'Set Student Group 
            'Added BY Govind Mande 2016-05-16 For Redmine Issue Task #13216

            Dim sSqlExistsStudentGrp As String = Database & "..spGetTopCurriculumAppStudentGrp__c @PersonID=" & AptifyEbusinessUser1.PersonID
            Dim dtExistingStudentGrp = DataAction.GetDataTable(sSqlExistsStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtExistingStudentGrp Is Nothing AndAlso dtExistingStudentGrp.Rows.Count > 0 Then
                SetComboValue(ddlCAP1StudGrp, Convert.ToString(dtExistingStudentGrp.Rows(0)("Name")))
                If ddlCAP1StudGrp.SelectedValue.Trim = "Select Student Group" Then
                    ddlCAP1StudGrp.Items.Clear()
                    ddlCAP1StudGrp.DataSource = dtExistingStudentGrp
                    ddlCAP1StudGrp.DataTextField = "Name"
                    ddlCAP1StudGrp.DataValueField = "ID"
                    ddlCAP1StudGrp.DataBind()
                    SetComboValue(ddlCAP1StudGrp, Convert.ToString(dtExistingStudentGrp.Rows(0)("Name")))

                End If
            End If
            ''Dim sStudentGrp As String = Convert.ToString(DataAction.ExecuteScalar(sSqlExistsStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
            '' If sStudentGrp.Trim <> "" Then
            ''  SetComboValue(ddlCAP1StudGrp, sStudentGrp)
            ''End If
            If Not Session("SelectedStuentGroupID") Is Nothing AndAlso Convert.ToString(Session("SelectedStuentGroupID")) <> "" Then
                SetComboValue(ddlCAP1StudGrp, Session("SelectedStuentGroupID"))
            End If
            ' Get Student Group for Interim and Exam Courses
            If ddlCAP1StudGrp.SelectedValue.Trim <> "Select Student Group" Then
                Dim sSqlStudentGrp As String = Database & "..spGetExamInterimStudentGrp__c @StudentGrpID=" & ddlCAP1StudGrp.SelectedValue
                Dim iExamIterimStudentGrp As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If iExamIterimStudentGrp > 0 Then
                    ViewState("ExamInterimStudentGrp") = iExamIterimStudentGrp
                End If
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Button Click"
    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
            If (ViewState("RouteChanged") IsNot Nothing AndAlso Convert.ToBoolean(ViewState("RouteChanged"))) Then
                If CBool(ViewState("OldRTO")) = False AndAlso CBool(ViewState("NewRTO")) Then
                    If EducationContract.Trim.ToLower = "elevation" _
                       Or (hdnCompanyId.Value <> "" AndAlso CLng(hdnCompanyId.Value) > 0) Then
                        SaveEEApplication()
                    End If
                ElseIf CBool(ViewState("OldRTO")) AndAlso CBool(ViewState("NewRTO")) = False Then
                    SaveEEApplication()
                ElseIf CBool(ViewState("OldRTO")) AndAlso CBool(ViewState("NewRTO")) Then
                    SaveEEApplication()
                ElseIf EducationContract.Trim.ToLower = "contract" OrElse (EducationContract.Trim.ToLower = "elevation" AndAlso chkInfoToFirm.Checked) Then
                    If (ViewState("RouteChanged") IsNot Nothing AndAlso Convert.ToBoolean(ViewState("RouteChanged"))) _
                      Or (hdnCompanyId.Value <> "" AndAlso CLng(hdnCompanyId.Value) > 0) Then
                        SaveEEApplication()
                    End If
                Else
                    SubmitBtn()
                End If
            Else
                SubmitBtn()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub SaveEEApplication()
        Try
            Dim oGEExmpApp As AptifyGenericEntityBase
            oGEExmpApp = AptifyApplication.GetEntityObject("ExemptionApplication__c", Convert.ToInt32(ViewState("ExemptionApp")))
            With oGEExmpApp
                .SetValue("RouteOfEntry", ddlRoute.SelectedValue)
                Dim strCompany As String() = txtFirm.Text.Trim.Split("/")

                ' commented below code by Govind and added new line after that #17841
                '.SetValue("CompanyID", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", strCompany(0)))
                .SetValue("CompanyID", hdnCompanyId.Value)
                'End #17841
                .SetValue("AddressLine1", txtAddrLine1.Text)
                .SetValue("AddressLine2", txtAddrLine2.Text)
                .SetValue("PostalCode", txtPostalCode.Text)
                If chkInfoToFirm.Checked Then
                    .SetValue("ShareMyInfoWithFirm", 1)
                Else
                    .SetValue("ShareMyInfoWithFirm", 0)
                End If
                'If txtFurtherInfo.Text <> "" Then
                '    .SetValue("EnrollmentComments", ViewState("EnrollmentComments").ToString() + " " + txtFurtherInfo.Text)
                'End If
                .SetValue("StatusID", AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Change Request"))
                .SetValue("FirmApprovalStatus", "")
                Dim sError As String = String.Empty
                If .Save(False, sError) Then
                    lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.EEUpdateSuucessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    btnSubmitOk.Visible = False
                    btnNo.Visible = True
                    btnNo.Text = "Ok"
                    radwindowSubmit.VisibleOnPageLoad = True
                    'updatePnlPopup.Update()
                End If
            End With
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub SubmitBtn()
        Try
            If Convert.ToDecimal(txtIntialAmount.Text.Trim) > 0 Then
                If CreditCard.PaymentTypeID <= 0 Then
                    Exit Sub
                End If
            End If

            If CheckGetMinimumCourses() Then
                ' Code added by Govind Mande 19th April 2016
                Session("SelectedStuentGroupID") = ddlCAP1StudGrp.SelectedItem.Text.Trim
                ' End code
                ' Check Elevation Student With RTO
                btnSubmit.Visible = True
                Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
                If EducationContract.Trim.ToLower = "elevation" AndAlso Convert.ToBoolean(ViewState("RTO")) Then
                    If Convert.ToDecimal(Convert.ToString(lblAmountPaidFirm.Text).Substring(1, Convert.ToString(lblAmountPaidFirm.Text).Length - 1).Trim) > 0 Then
                        lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ElevationWithRTOMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        btnNo.Visible = False
                        btnSubmitOk.Visible = True

                        radwindowSubmit.VisibleOnPageLoad = True
                        updatePnlPopup.Update()
                    Else
                        CreateOrderAndSaveData()
                    End If

                ElseIf EducationContract.Trim.ToLower = "contract" AndAlso Convert.ToBoolean(ViewState("RTO")) Then
                    lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ContractWithRTOMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    btnNo.Visible = True
                    btnNo.Text = "No"
                    btnSubmitOk.Visible = True

                    radwindowSubmit.VisibleOnPageLoad = True
                    updatePnlPopup.Update()
                Else
                    CreateOrderAndSaveData()
                End If
            Else

                lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.MinimumUnits")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '& " " & ViewState("MinimumUnitsRequired") & " full subjects in CAP1 or CAP2."
                btnSubmitOk.Visible = False
                btnNo.Visible = True
                btnNo.Text = "Ok"
                radwindowSubmit.VisibleOnPageLoad = True
                updatePnlPopup.Update()
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

        End Try
    End Sub
    Private Function CheckGetMinimumCourses() As Boolean
        Try
            Dim drCurriculumWiseUnits() As DataRow = SummerPaymentSummery.Select("Units > '0'")
            Dim view As New DataView(SummerPaymentSummery)
            '         

            Dim distinctValues As DataTable = view.ToTable(True, "Subject", "MinimumUnits", "Units", "CurriculumID", "CutOffUnits", "Isfailed", "FailedUnits", "FirstAttempt", "SessionType")
            Dim iCap1 As Integer = AptifyApplication.GetEntityRecordIDFromRecordName("Curriculum Definitions", "CAP1- CA Proficiency 1")
            Dim iCap2 As Integer = AptifyApplication.GetEntityRecordIDFromRecordName("Curriculum Definitions", "CAP2- CA Proficiency 2")
            Dim iFAE As Integer = AptifyApplication.GetEntityRecordIDFromRecordName("Curriculum Definitions", "FAE- Final Admitting Exam")
            Dim dMinimumcap1CutOffUnits As Decimal
            Dim dMinimumcap2CutOffUnits As Decimal
            Dim dMinimumFAECutOffUnits As Decimal
            Dim dcap1Sum As Decimal = 0
            Dim isCap1 As Boolean = False
            Dim dcap2Sum As Decimal = 0
            Dim isCap2 As Boolean = False
            Dim dFAESum As Decimal = 0
            Dim isFAE As Boolean = False
            Dim Isfailed As Integer = 0
            Dim FailedUnits1 As Decimal = 0
            Dim FirstAttempt1 As Decimal = 0
            Dim chkDebkFailed As Boolean = False
            Dim bIsAllowToProceed As Boolean = True
            If Not distinctValues Is Nothing AndAlso distinctValues.Rows.Count > 0 Then
                Dim IsSummer As Boolean
                Dim IsAutumn As Boolean
                Dim Cap1BothSession As Boolean
                Dim Cap2BothSession As Boolean
                Dim FAEBothSession As Boolean
                Dim BothSession As Boolean
                Dim cap1Summer As Decimal
                Dim cap1Autumn As Decimal
                Dim cap2Summer As Decimal
                Dim cap2Autumn As Decimal
                Dim FAESummer As Decimal
                Dim FAEAutumn As Decimal
                Dim IsCap1Summer As Boolean
                Dim IsCap1Autumn As Boolean
                Dim IsCap2Summer As Boolean
                Dim IsCap2Autumn As Boolean
                Dim IsFAESummer As Boolean
                Dim IsFAEAutumn As Boolean
                For Each dr As DataRow In distinctValues.Rows
                    If dr("SessionType") = "Summer" Then
                        IsSummer = True
                        If dr("CurriculumID") = 2 Then
                            cap1Summer += dr("Units")
                            IsCap1Summer = True
                        End If
                        If dr("CurriculumID") = 3 Then
                            cap2Summer += dr("Units")
                            IsCap2Summer = True
                        End If
                        If dr("CurriculumID") = 5 Then
                            FAESummer += dr("Units")
                            IsFAESummer = True
                            ViewState("FAEValidation") = True 'Redmine #17838
                        End If
                    ElseIf dr("SessionType") = "Autumn" Then
                        IsAutumn = True
                        If dr("CurriculumID") = 2 Then
                            cap1Autumn += dr("Units")
                            IsCap1Autumn = True
                        End If
                        If dr("CurriculumID") = 3 Then
                            cap2Autumn += dr("Units")
                            IsCap2Autumn = True
                        End If
                        If dr("CurriculumID") = 5 Then
                            FAEAutumn += dr("Units")
                            IsFAEAutumn = True
                            ViewState("FAEValidation") = True 'Redmine #17838
                        End If
                    End If


                    If IsSummer = True AndAlso IsAutumn = True Then
                        If IsCap1Autumn AndAlso IsCap1Summer Then
                            Cap1BothSession = True
                        End If
                        If IsCap2Autumn AndAlso IsCap2Summer Then
                            Cap2BothSession = True
                        End If
                        If IsFAEAutumn AndAlso IsFAESummer Then
                            FAEBothSession = True
                        End If
                        BothSession = True

                    End If
                Next

                Dim view1 As New DataView(distinctValues)
                Dim distinctValues1 As DataTable = view.ToTable(True, "CurriculumID")



                ' Dim curriculumIds = distinctValues.AsEnumerable().Select(Function(p) p.Field(Of Integer)("CurriculumID")).Distinct().ToList()
                For Each dr As DataRow In distinctValues1.Rows
                    Dim cid As Integer = Convert.ToInt32(dr("CurriculumID"))

                    Dim IsDebkQuery = (From t In _enrollmentList.Where(Function(p) p.CurriculumID = cid And p.ClassType = "exam" And p.IsChecked = True)
                                       Select t.SubjectID)
                    If IsDebkQuery IsNot Nothing And IsDebkQuery.ToList().Count > 0 And IsDebkQuery.ToList().Count = 1 Then
                        Dim CourseID As Integer = IsDebkQuery(0)
                        Dim sSqlDEBK As String = Database & "..spGetDEBKFailed__c @CourseID=" & CourseID & ",@StudentID=" & Convert.ToInt32(ViewState("SelectedStudentID"))
                        Dim isDebkFailed As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlDEBK, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If isDebkFailed Then
                            chkDebkFailed = True
                        End If
                    End If

                    If chkDebkFailed = False Then

                        Dim query = (From c In _enrollmentList.Where(Function(p) p.CurriculumID = cid And p.ClassType = "exam")
                                    Group By c.CurriculumID, c.CutOffUnits Into Total = Sum(c.SelectedUnits) Select Total, CutOffUnits, CurriculumID)

                        Dim Failedquery = (From c In _enrollmentList.Where(Function(p) p.CurriculumID = cid And p.ClassType = "Exam" And p.IsFailed = True)
                                   Group By c.CurriculumID, c.CutOffUnits, c.FailedUnits, c.FirstAttempt Into Total = Sum(c.SelectedUnits) Select Total, CutOffUnits, CurriculumID, FailedUnits, FirstAttempt)

                        Dim FirstAttemptquery = (From c In _enrollmentList.Where(Function(p) p.CurriculumID = cid And p.ClassType = "Exam" And p.IsFailed = False)
                                   Group By c.CurriculumID, c.CutOffUnits, c.FailedUnits, c.FirstAttempt Into Total = Sum(c.SelectedUnits) Select Total, CutOffUnits, CurriculumID, FailedUnits, FirstAttempt)
                        Dim FirstSelectedUnit As Double = 0
                        Dim minCutOffUnits As Double = 0
                        Dim CurriculumID2 As Integer = 0
                        Dim firstAttemptremaining As Double = 0

                        If FirstAttemptquery IsNot Nothing And FirstAttemptquery.ToList().Count > 0 Then
                            FirstSelectedUnit = FirstAttemptquery.ToList().First().Total
                            minCutOffUnits = FirstAttemptquery.ToList().First().CutOffUnits
                            CurriculumID2 = FirstAttemptquery.ToList().First().CurriculumID
                            firstAttemptremaining = FirstAttemptquery.ToList().First().FirstAttempt

                            If CurriculumID2 = 2 Then
                                isCap1 = True
                            End If
                            If CurriculumID2 = 3 Then
                                isCap2 = True
                            End If
                            If CurriculumID2 = 5 Then
                                isFAE = True
                            End If
                        End If

                        Dim FailedSelectedUnit As Double = 0
                        Dim FailedCurriculumID As Integer = 0
                        Dim Failedremaining As Double = 0
                        Dim SessionWise As String
                        If Failedquery IsNot Nothing And Failedquery.ToList().Count > 0 Then
                            FailedSelectedUnit = Failedquery.ToList().First().Total
                            minCutOffUnits = Failedquery.ToList().First().CutOffUnits
                            FailedCurriculumID = Failedquery.ToList().First().CurriculumID
                            Failedremaining = Failedquery.ToList().First().FailedUnits

                            If FirstSelectedUnit > 0 Then ' This if condition added by GM for Redmine #19946
                                FailedSelectedUnit = FailedSelectedUnit + FirstSelectedUnit
                            End If
                            If FailedCurriculumID = 2 Then
                                isCap1 = True
                            End If
                            If FailedCurriculumID = 3 Then
                                isCap2 = True
                            End If
                            If FailedCurriculumID = 5 Then
                                isFAE = True
                            End If
                        End If
                        If FirstSelectedUnit > 0 Then
                            If (firstAttemptremaining >= minCutOffUnits And FirstSelectedUnit < minCutOffUnits OrElse (FirstSelectedUnit < (minCutOffUnits - firstAttemptremaining))) Then
                                If CurriculumID2 <> 5 AndAlso FailedCurriculumID <> 5 Then
                                    bIsAllowToProceed = False
                                    Exit For
                                ElseIf firstAttemptremaining <= 0 Then
                                    If (Convert.ToDouble(FirstSelectedUnit) + Convert.ToDouble(FailedSelectedUnit)) < minCutOffUnits Then
                                        bIsAllowToProceed = False
                                        Exit For
                                    Else
                                        If IsFAEAutumn OrElse IsFAESummer Then
                                            If FAEAutumn < Failedremaining AndAlso FAEAutumn > 0 Then
                                                bIsAllowToProceed = False
                                                Exit For

                                            ElseIf FAESummer < Failedremaining Then
                                                If FirstSelectedUnit < firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                                    bIsAllowToProceed = False
                                                    Exit For
                                                ElseIf FirstSelectedUnit = firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                                Else
                                                    bIsAllowToProceed = False
                                                    Exit For
                                                End If
                                            End If
                                            'If FAESummer < Failedremaining OrElse FAEAutumn < Failedremaining Then
                                            '    bIsAllowToProceed = False
                                            '    Exit For
                                            'End If
                                        End If
                                    End If
                                Else
                                    bIsAllowToProceed = False
                                    Exit For
                                End If

                            End If
                        End If

                        If (Failedremaining >= minCutOffUnits And FailedSelectedUnit < minCutOffUnits) Then
                            If CurriculumID2 <> 5 AndAlso FailedCurriculumID <> 5 Then
                                bIsAllowToProceed = False
                                Exit For
                            ElseIf firstAttemptremaining <= 0 Then
                                If (Convert.ToDouble(FirstSelectedUnit) + Convert.ToDouble(FailedSelectedUnit)) < minCutOffUnits Then
                                    bIsAllowToProceed = False
                                    Exit For
                                Else
                                    If IsFAEAutumn OrElse IsFAESummer Then
                                        If FAEAutumn < Failedremaining AndAlso FAEAutumn > 0 Then
                                            bIsAllowToProceed = False
                                            Exit For

                                        ElseIf FAESummer < Failedremaining Then
                                            If FirstSelectedUnit < firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                                bIsAllowToProceed = False
                                                Exit For
                                            ElseIf FirstSelectedUnit = firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                            Else
                                                bIsAllowToProceed = False
                                                Exit For
                                            End If
                                        End If
                                        'If FAESummer < Failedremaining OrElse FAEAutumn < Failedremaining Then
                                        '    bIsAllowToProceed = False
                                        '    Exit For
                                        'End If
                                    End If
                                End If
                            Else
                                bIsAllowToProceed = False
                                Exit For
                            End If
                        ElseIf FailedSelectedUnit < Failedremaining Then
                            If minCutOffUnits <= FailedSelectedUnit Then

                                'need to logic here
                                If ViewState("IsSummerEnrolledcomeing") = True Then
                                    If IsCap1Autumn Then
                                        If cap1Autumn < Failedremaining Then
                                            bIsAllowToProceed = False
                                            Exit For
                                        End If
                                    End If
                                    If IsCap2Autumn Then
                                        If cap2Autumn < Failedremaining Then
                                            bIsAllowToProceed = False
                                            Exit For
                                        End If
                                    End If
                                    If IsFAEAutumn Then
                                        If FAEAutumn < Failedremaining Then
                                            bIsAllowToProceed = False
                                            Exit For
                                        End If
                                    End If
                                ElseIf ViewState("IsAutumEnrolledcomeing") = True Then
                                    If IsCap1Summer Then
                                        If cap1Summer < Failedremaining Then
                                            bIsAllowToProceed = False
                                            Exit For
                                        End If
                                    End If
                                    If IsCap2Summer Then
                                        If cap2Summer < Failedremaining Then
                                            bIsAllowToProceed = False
                                            Exit For
                                        End If
                                    End If
                                    If IsFAESummer Then
                                        If FAESummer < Failedremaining Then
                                            bIsAllowToProceed = False
                                            Exit For
                                        End If
                                    End If
                                Else
                                    If IsCap1Autumn OrElse IsCap1Summer Then
                                        If cap1Autumn < Failedremaining Then
                                            If (Failedremaining - FailedSelectedUnit) >= minCutOffUnits Then ' this codtion added by GM for Redmine log #19657
                                            Else
                                                bIsAllowToProceed = False
                                                Exit For
                                            End If

                                        ElseIf cap1Summer < Failedremaining Then
                                            If FirstSelectedUnit < firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                                bIsAllowToProceed = False
                                                Exit For
                                            ElseIf FirstSelectedUnit = firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                            Else
                                                bIsAllowToProceed = False
                                                Exit For
                                            End If
                                        End If
                                        'If cap1Summer < Failedremaining OrElse cap1Autumn < Failedremaining Then
                                        '    bIsAllowToProceed = False
                                        '    Exit For
                                        'End If
                                    End If
                                    If IsCap2Autumn OrElse IsCap2Summer Then
                                        If cap2Autumn < Failedremaining Then
                                            bIsAllowToProceed = False
                                            Exit For

                                        ElseIf cap2Summer < Failedremaining Then
                                            If FirstSelectedUnit < firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                                bIsAllowToProceed = False
                                                Exit For
                                            ElseIf FirstSelectedUnit = firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                            Else
                                                bIsAllowToProceed = False
                                                Exit For
                                            End If
                                        End If
                                        'If cap2Summer < Failedremaining OrElse cap2Autumn < Failedremaining Then
                                        '    bIsAllowToProceed = False
                                        '    Exit For
                                        'End If
                                    End If
                                    If IsFAEAutumn OrElse IsFAESummer Then
                                        If FAEAutumn < Failedremaining Then
                                            bIsAllowToProceed = False
                                            Exit For

                                        ElseIf FAESummer < Failedremaining Then
                                            If FirstSelectedUnit < firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                                bIsAllowToProceed = False
                                                Exit For
                                            ElseIf FirstSelectedUnit = firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                            Else
                                                bIsAllowToProceed = False
                                                Exit For
                                            End If
                                        End If
                                        'If FAESummer < Failedremaining OrElse FAEAutumn < Failedremaining Then
                                        '    bIsAllowToProceed = False
                                        '    Exit For
                                        'End If
                                    End If
                                End If

                            Else
                                bIsAllowToProceed = False
                                Exit For
                            End If

                        ElseIf BothSession = True Then
                            If Cap1BothSession Then

                                If (cap1Autumn + cap1Summer) < Failedremaining Then   'Updated condition for Redmine #20720
                                    bIsAllowToProceed = False
                                    Exit For

                                ElseIf (cap1Summer + cap1Autumn) < Failedremaining Then 'Updated condition for Redmine #20720
                                    If FirstSelectedUnit < firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                        bIsAllowToProceed = False
                                        Exit For
                                    ElseIf FirstSelectedUnit = firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                    Else
                                        bIsAllowToProceed = False
                                        Exit For
                                    End If
                                End If


                                'If cap1Summer < Failedremaining OrElse cap1Autumn < Failedremaining Then
                                '    bIsAllowToProceed = False
                                '    Exit For
                                'End If
                            End If
                            If Cap2BothSession Then
                                If (cap2Autumn + cap2Summer) < Failedremaining Then 'Updated condition for Redmine #20720
                                    bIsAllowToProceed = False
                                    Exit For

                                ElseIf (cap2Summer + cap2Autumn) < Failedremaining Then 'Updated condition for Redmine #20720
                                    If FirstSelectedUnit < firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                        bIsAllowToProceed = False
                                        Exit For
                                    ElseIf FirstSelectedUnit = firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                    Else
                                        bIsAllowToProceed = False
                                        Exit For
                                    End If
                                End If
                                'If cap2Summer < Failedremaining OrElse cap2Autumn < Failedremaining Then
                                '    bIsAllowToProceed = False
                                '    Exit For
                                'End If
                            End If
                            If FAEBothSession Then
                                If (FAEAutumn + FAESummer) < Failedremaining Then 'Updated condition for Redmine #20720
                                    bIsAllowToProceed = False
                                    Exit For

                                ElseIf (FAESummer + FAEAutumn) < Failedremaining Then 'Updated condition for Redmine #20720
                                    If FirstSelectedUnit < firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                        bIsAllowToProceed = False
                                        Exit For
                                    ElseIf FirstSelectedUnit = firstAttemptremaining AndAlso firstAttemptremaining > 0 Then
                                    Else
                                        bIsAllowToProceed = False
                                        Exit For
                                    End If
                                End If
                                'If FAESummer < Failedremaining OrElse FAEAutumn < Failedremaining Then
                                '    bIsAllowToProceed = False
                                '    Exit For
                                'End If
                            End If

                        End If
                    End If
                Next
            Else
                bIsAllowToProceed = False
            End If
            Return bIsAllowToProceed

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function
    'Private Function CheckGetMinimumCourses() As Boolean
    '    Try
    '        Dim drCurriculumWiseUnits() As DataRow = SummerPaymentSummery.Select("Units > '0'")
    '        Dim view As New DataView(SummerPaymentSummery)
    '        Dim distinctValues As DataTable = view.ToTable(True, "Subject", "MinimumUnits", "Units", "CurriculumID", "CutOffUnits", "Isfailed", "FailedUnits", "FirstAttempt")
    '        Dim iCap1 As Integer = AptifyApplication.GetEntityRecordIDFromRecordName("Curriculum Definitions", "CAP1- CA Proficiency 1")
    '        Dim iCap2 As Integer = AptifyApplication.GetEntityRecordIDFromRecordName("Curriculum Definitions", "CAP2- CA Proficiency 2")
    '        Dim iFAE As Integer = AptifyApplication.GetEntityRecordIDFromRecordName("Curriculum Definitions", "FAE- Final Admitting Exam")
    '        Dim dMinimumcap1CutOffUnits As Decimal
    '        Dim dMinimumcap2CutOffUnits As Decimal
    '        Dim dMinimumFAECutOffUnits As Decimal
    '        Dim dcap1Sum As Decimal = 0
    '        Dim isCap1 As Boolean = False
    '        Dim dcap2Sum As Decimal = 0
    '        Dim isCap2 As Boolean = False
    '        Dim dFAESum As Decimal = 0
    '        Dim isFAE As Boolean = False
    '        Dim Isfailed As Integer = 0
    '        Dim FailedUnits As Decimal = 0
    '        Dim FirstAttempt As Decimal = 0
    '        If Not distinctValues Is Nothing AndAlso distinctValues.Rows.Count > 0 Then

    '            For Each dr As DataRow In distinctValues.Rows
    '                If IsDBNull(dr("CurriculumID")) = False Then
    '                    If dr("CurriculumID") = 2 Then

    '                        FirstAttempt = Convert.ToDecimal(dr("FirstAttempt"))
    '                        FailedUnits = Convert.ToDecimal(dr("FailedUnits"))


    '                        dMinimumcap1CutOffUnits = Convert.ToDecimal(dr("CutOffUnits"))
    '                        isCap1 = True
    '                        If dMinimumcap1CutOffUnits > 0 Then
    '                            If dcap1Sum = 0 Then
    '                                dcap1Sum = Convert.ToDecimal(dr("Units"))
    '                            Else
    '                                dcap1Sum = dcap1Sum + Convert.ToDecimal(dr("Units"))
    '                            End If
    '                            ' chk DEBK Failed
    '                            Dim sSqlDEBK As String = Database & "..spGetDEBKFailed__c @CourseID=" & Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Courses", dr("Subject"))) & ",@StudentID=" & AptifyEbusinessUser1.PersonID
    '                            Dim isDebkFailed As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlDEBK, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '                            If isDebkFailed Then
    '                                dcap1Sum = dMinimumcap1CutOffUnits
    '                            End If
    '                        Else
    '                            ' Return True
    '                        End If

    '                    End If

    '                    If dr("CurriculumID") = 3 Then
    '                        dMinimumcap2CutOffUnits = Convert.ToDecimal(dr("CutOffUnits"))

    '                        isCap2 = True
    '                        If dMinimumcap2CutOffUnits > 0 Then
    '                            If dcap2Sum = 0 Then
    '                                dcap2Sum = Convert.ToDecimal(dr("Units"))
    '                            Else
    '                                dcap2Sum = dcap2Sum + Convert.ToDecimal(dr("Units"))
    '                            End If
    '                            ' chk DEBK Failed
    '                            Dim sSqlDEBK As String = Database & "..spGetDEBKFailed__c @CourseID=" & Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Courses", dr("Subject"))) & ",@StudentID=" & AptifyEbusinessUser1.PersonID
    '                            Dim isDebkFailed As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlDEBK, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '                            If isDebkFailed Then
    '                                dcap2Sum = dMinimumcap2CutOffUnits
    '                            End If
    '                        Else
    '                            ' Return True
    '                        End If

    '                    End If
    '                    If dr("CurriculumID") = 5 Then
    '                        dMinimumFAECutOffUnits = Convert.ToDecimal(dr("CutOffUnits"))

    '                        isFAE = True
    '                        If dMinimumFAECutOffUnits > 0 Then
    '                            If dFAESum = 0 Then
    '                                dFAESum = Convert.ToDecimal(dr("Units"))
    '                            Else
    '                                dFAESum = dFAESum + Convert.ToDecimal(dr("Units"))
    '                            End If
    '                            ' chk DEBK Failed
    '                            Dim sSqlDEBK As String = Database & "..spGetDEBKFailed__c @CourseID=" & Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Courses", dr("Subject"))) & ",@StudentID=" & AptifyEbusinessUser1.PersonID
    '                            Dim isDebkFailed As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlDEBK, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '                            If isDebkFailed Then
    '                                dFAESum = dMinimumFAECutOffUnits
    '                            End If
    '                        Else
    '                            '  Return True
    '                        End If

    '                    End If
    '                End If


    '            Next
    '        End If
    '        Dim bIsAllowToProceed As Boolean = True
    '        If isCap1 Then
    '            'Dim sSql As String = Database & "..spCheckMinimumUnitsAsPerCurriculum__c @CurriculumID=" & 2 & ",@StudentID=" & AptifyEbusinessUser1.PersonID
    '            'Dim dUnitCount As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))

    '            'If (dUnitCount + dcap1Sum) < dMinimumcap1Unit Then
    '            '    Return False
    '            'End If
    '            'Redmine log #16028
    '            Dim sSqlIsEnroll As String = Database & "..spCheckIsEnrolledByCurriculum__c @CurriculumID=" & 2 & ",@StudentID=" & AptifyEbusinessUser1.PersonID
    '            Dim isEnroll As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlIsEnroll, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '            If isEnroll = False Then
    '                If dMinimumcap1CutOffUnits > 0 Then
    '                    If dcap1Sum < dMinimumcap1CutOffUnits Then
    '                        ViewState("MinimumUnitsRequired") = dMinimumcap1CutOffUnits
    '                        Return False
    '                    Else
    '                        bIsAllowToProceed = True

    '                    End If
    '                Else
    '                    bIsAllowToProceed = True
    '                End If
    '            Else
    '                bIsAllowToProceed = True
    '            End If

    '            ' in else write another code
    '        End If
    '        If isCap2 Then
    '            Dim sSqlIsEnroll As String = Database & "..spCheckIsEnrolledByCurriculum__c @CurriculumID=" & 3 & ",@StudentID=" & AptifyEbusinessUser1.PersonID
    '            Dim isEnroll As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlIsEnroll, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '            If isEnroll = False Then
    '                If dMinimumcap2CutOffUnits > 0 Then
    '                    If dcap2Sum < dMinimumcap2CutOffUnits Then
    '                        ViewState("MinimumUnitsRequired") = dMinimumcap2CutOffUnits
    '                        Return False
    '                    Else
    '                        bIsAllowToProceed = True

    '                    End If
    '                Else
    '                    bIsAllowToProceed = True

    '                End If
    '            Else
    '                bIsAllowToProceed = True
    '            End If



    '        End If
    '        If isFAE Then
    '            Dim sSqlIsEnroll As String = Database & "..spCheckIsEnrolledByCurriculum__c @CurriculumID=" & 5 & ",@StudentID=" & AptifyEbusinessUser1.PersonID
    '            Dim isEnroll As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlIsEnroll, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '            If isEnroll = False Then
    '                If dMinimumFAECutOffUnits > 0 Then
    '                    If dFAESum < dMinimumFAECutOffUnits Then
    '                        ViewState("MinimumUnitsRequired") = dMinimumFAECutOffUnits
    '                        Return False
    '                    Else
    '                        bIsAllowToProceed = True
    '                    End If
    '                Else
    '                    bIsAllowToProceed = True
    '                End If
    '            Else
    '                bIsAllowToProceed = True
    '            End If

    '        End If
    '        Dim bISCap1Present As Boolean = False
    '        _studentEnrollmentDetails = CType(ViewState("_studentEnrollmentDetails"), DataTable)
    '        If Not _studentEnrollmentDetails Is Nothing AndAlso _studentEnrollmentDetails.Rows.Count > 0 AndAlso bIsAllowToProceed Then
    '            Dim viewCap1 As New DataView(_studentEnrollmentDetails)
    '            Dim distinctValuesCap1 As DataTable = viewCap1.ToTable(True, "Subject", "MinimumUnits", "Units", "CurriculumID", "CutOffUnits")
    '            Dim drCap1() As DataRow = distinctValuesCap1.Select("CurriculumID=2")
    '            If drCap1.Length > 0 Then
    '                bISCap1Present = True
    '                If isCap1 = False Then
    '                    Dim sSqlIsEnroll As String = Database & "..spCheckIsEnrolledByCurriculum__c @CurriculumID=" & 2 & ",@StudentID=" & AptifyEbusinessUser1.PersonID
    '                    Dim isEnroll As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlIsEnroll, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '                    If isEnroll = False Then
    '                        If Convert.ToDecimal(drCap1(0)("CutOffUnits")) > 0 Then
    '                            ViewState("MinimumUnitsRequired") = drCap1(0)("CutOffUnits")

    '                            Return False
    '                        Else
    '                            bIsAllowToProceed = True
    '                        End If
    '                    Else
    '                        bIsAllowToProceed = True
    '                    End If

    '                Else
    '                    'Return True
    '                End If

    '            Else
    '                '  Return True
    '            End If
    '            Dim drCap2() As DataRow = distinctValuesCap1.Select("CurriculumID=3")
    '            If drCap2.Length > 0 Then
    '                If isCap2 = False Then
    '                    Dim sSqlIsEnroll As String = Database & "..spCheckIsEnrolledByCurriculum__c @CurriculumID=" & 3 & ",@StudentID=" & AptifyEbusinessUser1.PersonID
    '                    Dim isEnroll As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlIsEnroll, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '                    If isEnroll = False Then
    '                        If Convert.ToDecimal(drCap2(0)("CutOffUnits")) > 0 Then
    '                            'need to check if this is briding case and before he can not taken cap2 courses 
    '                            Dim sCheckCap2CourseNotEnrolledSql As String = Database & "..spCheckStudentNotEnrolledCAP2__c @StudentID=" & AptifyEbusinessUser1.PersonID
    '                            Dim bIsCap2EnrollFirstTime As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sCheckCap2CourseNotEnrolledSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '                            If bIsCap2EnrollFirstTime Then
    '                                If bISCap1Present = False Then
    '                                    ViewState("MinimumUnitsRequired") = drCap2(0)("CutOffUnits")
    '                                    Return False
    '                                Else
    '                                    bIsAllowToProceed = True

    '                                End If
    '                            Else
    '                                ViewState("MinimumUnitsRequired") = drCap2(0)("CutOffUnits")
    '                                Return False
    '                            End If
    '                        Else
    '                            bIsAllowToProceed = True
    '                        End If
    '                    Else
    '                        bIsAllowToProceed = True
    '                    End If

    '                Else
    '                    ' Return True
    '                End If

    '            Else
    '                ' Return True
    '            End If

    '            Dim drFAE() As DataRow = distinctValuesCap1.Select("CurriculumID=5")
    '            If drFAE.Length > 0 Then
    '                If isFAE = False Then
    '                    Dim sSqlIsEnroll As String = Database & "..spCheckIsEnrolledByCurriculum__c @CurriculumID=" & 5 & ",@StudentID=" & AptifyEbusinessUser1.PersonID
    '                    Dim isEnroll As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlIsEnroll, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '                    If isEnroll = False Then
    '                        If Convert.ToDecimal(drFAE(0)("CutOffUnits")) > 0 Then
    '                            ViewState("MinimumUnitsRequired") = drFAE(0)("CutOffUnits")

    '                            Return False
    '                        Else
    '                            bIsAllowToProceed = True
    '                        End If
    '                    Else
    '                        bIsAllowToProceed = True
    '                    End If

    '                Else
    '                    '  Return True
    '                End If

    '            Else
    '                '  Return True
    '            End If
    '        Else
    '            ' Return True
    '        End If

    '        Return bIsAllowToProceed

    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '        Return False
    '    End Try
    'End Function
    Protected Sub btnNo_Click(sender As Object, e As System.EventArgs) Handles btnNo.Click
        radwindowSubmit.VisibleOnPageLoad = False
        updatePnlPopup.Update()
    End Sub
    Protected Sub btnSuccess_Click(sender As Object, e As System.EventArgs) Handles btnSuccess.Click
        radwindowSubmit.VisibleOnPageLoad = False
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), False)
    End Sub
    Protected Sub btnSubmitOk_Click(sender As Object, e As System.EventArgs) Handles btnSubmitOk.Click
        Try
            radwindowSubmit.VisibleOnPageLoad = False
            btnSubmitOk.Enabled = False
            If CBool(Session("AlreadySubmit")) = False Then
                Session("AlreadySubmit") = True
                ''Commented By Pradip 2016-02-03
                'CreateOrderAndSaveData()
                UpdateEEApplicationData()
                Response.Redirect(PaymentSummeryPage, False)
            End If
            '' UpdatePanelBtn.Update()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub CreateOrderAndSaveData()
        Try
            Dim lOrderID As Long
            'Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
            If CreateOrder(lOrderID) > 0 Then
                ' create a firm order 
                FirmPayOrder()
                If (ViewState("RouteChanged") IsNot Nothing AndAlso Convert.ToBoolean(ViewState("RouteChanged"))) _
               Or (hdnCompanyId.Value <> "" AndAlso CLng(hdnCompanyId.Value) > 0) Then
                    Dim oGEExmpApp As AptifyGenericEntityBase
                    oGEExmpApp = AptifyApplication.GetEntityObject("ExemptionApplication__c", Convert.ToInt32(ViewState("ExemptionApp")))
                    With oGEExmpApp
                        .SetValue("RouteOfEntry", ddlRoute.SelectedValue)
                        .SetValue("CompanyID", hdnCompanyId.Value)
                        If chkInfoToFirm.Checked Then
                            .SetValue("ShareMyInfoWithFirm", 1)
                        Else
                            .SetValue("ShareMyInfoWithFirm", 0)
                        End If
                        'If txtFurtherInfo.Text <> "" Then
                        '    .SetValue("EnrollmentComments", ViewState("EnrollmentComments").ToString() + " " + txtFurtherInfo.Text)
                        'End If
                        .SetValue("StatusID", AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Change Request"))
                        Dim sError As String = String.Empty
                        If .Save(False, sError) Then

                        End If
                    End With
                End If
                CreateCurriculumApplication()
                lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                btnSubmitOk.Visible = False
                btnNo.Visible = False
                btnSuccess.Visible = True
                radwindowSubmit.VisibleOnPageLoad = True
                updatePnlPopup.Update()
                ViewState("AlreadySubmit") = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub CreateCurriculumApplication()
        Try
            Dim drCurriculumWiseUnits() As DataRow = SummerPaymentSummery.Select("CurriculumID > '0'")
            Dim view As New DataView(SummerPaymentSummery)
            Dim distinctValues As DataTable = view.ToTable(True, "CurriculumID", "AcademicCycleID")
            If Not distinctValues Is Nothing AndAlso distinctValues.Rows.Count > 0 Then
                For Each dr As DataRow In distinctValues.Rows
                    ' Create Curriculum Application
                    ' First Check For this already curriculum Application or not for that person
                    Dim sSql As String = Database & "..spCheckCurriculumAppExists__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@CurriculumID=" & Convert.ToInt32(dr("CurriculumID"))
                    Dim iCurriculumAppID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If iCurriculumAppID <= 0 Then
                        ' CREATE APP
                        Dim oCurriculumGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Curriculum Applications", -1)
                        With oCurriculumGE
                            .SetValue("ApplicantID", AptifyEbusinessUser1.PersonID)
                            .SetValue("CurriculumCategoryID", AptifyApplication.GetEntityRecordIDFromRecordName("Curriculum Definition Categories", "CA-Chartered Accountant"))
                            .SetValue("CurriculumID", Convert.ToInt32(dr("CurriculumID")))
                            .SetValue("ApplicationDate", Today.Date)
                            .SetValue("Status", "Approved")
                            .SetValue("StartDate__c", Today.Date)
                            .SetValue("SubGroupID__c", ddlCAP1StudGrp.SelectedValue)
                            .SetValue("RounteOfEntryID__c", ddlRoute.SelectedValue)
                            .SetValue("AcademicCycleID__c", dr("AcademicCycleID"))
                            Dim sError As String = String.Empty
                            If .Save(False, sError) Then

                            Else

                            End If
                        End With
                    Else
                        Dim oCurriculumGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Curriculum Applications", iCurriculumAppID)
                        With oCurriculumGE
                            .SetValue("SubGroupID__c", ddlCAP1StudGrp.SelectedValue)
                            Dim sError As String = String.Empty
                            If .Save(False, sError) Then

                            Else

                            End If
                        End With

                    End If

                Next
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Events"
    Protected Sub ddlRoute_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRoute.SelectedIndexChanged
        Try
            If Convert.ToString(ViewState("OldRoute")) <> ddlRoute.SelectedItem.ToString Then
                btnDisplayPaymentSummey.Text = "Save"
                btnDisplayPaymentSummey.Visible = True
                lblEEMessage.Text = ""
                ViewState("RouteChanged") = True
                RoutesInformation()
                HideEnrollmentDetails() ' Code added by Govind for redmine  issue 13300
                ''  HideEnrollmentOnContractStudent() ' Code commented by Govind for redmine issue 13300
                '' UpdatePanelBtn.Update()
            Else
                btnDisplayPaymentSummey.Text = "Enroll"
                btnDisplayPaymentSummey.Visible = True
                RoutesInformation()
                UpdatePanelPayment.Update()
                ''UpdatePanel1.Update()
                '' UpdatePanelBtn.Update()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub txtFirm_TextChanged(sender As Object, e As System.EventArgs) Handles txtFirm.TextChanged
        Try
            Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
            If hdnCompanyId.Value <> "" AndAlso CDbl(hdnCompanyId.Value) > 0 Then
                Dim sSql As String = Database & "..spGetCompanyAddress__c @CompanyID=" & hdnCompanyId.Value
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    txtAddrLine1.Text = dt.Rows(0)("Line1").ToString()
                    txtAddrLine2.Text = dt.Rows(0)("Line2").ToString()
                    txtPostalCode.Text = dt.Rows(0)("PostalCode").ToString()
                End If
                If EducationContract.Trim.ToLower = "elevation" Then
                    '' ValidateFirm(hdnCompanyId.Value) ' Code commented by Govind for redmine issue 13300
                ElseIf EducationContract.Trim.ToLower = "contract" Then
                    '' ViewState("RouteChanged") = True  ' Code commented by Govind for redmine issue 13300
                    '' HideEnrollmentOnContractStudent() ' Code commented by Govind for redmine issue 13300
                    ''Added BY Pradip 2016-01-04 For Group 3 Tracker G3-51
                    If txtFirm.Text.Trim <> "" Then
                        chkInfoToFirm.Checked = True
                    Else
                        chkInfoToFirm.Checked = False
                    End If
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub ValidateFirm(ByVal CompanyID As Long)
        Try
            Dim sSqlCompanyRTOorCentrallyManaged As String = Database & "..spCheckCompanyRTOOrCentrallyManaged__c @CompanyID=" & CompanyID
            Dim dtCompanyDetails As DataTable = DataAction.GetDataTable(sSqlCompanyRTOorCentrallyManaged, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtCompanyDetails Is Nothing AndAlso dtCompanyDetails.Rows.Count > 0 Then
                ViewState("NewRTO") = dtCompanyDetails.Rows(0)("RTO__c")
                'ViewState("CentrallyManaged") = dtCompanyDetails.Rows(0)("CentrallyManaged__c")
            End If
            If CBool(ViewState("OldRTO")) = False AndAlso CBool(ViewState("NewRTO")) Then
                ViewState("RouteChanged") = True
                HideEnrollmentOnContractStudent()
            ElseIf CBool(ViewState("OldRTO")) AndAlso CBool(ViewState("NewRTO")) = False Then
                ViewState("RouteChanged") = True
                HideEnrollmentOnContractStudent()

            Else
                ViewState("RouteChanged") = True
                HideEnrollmentOnContractStudent()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub grdGrantedExempts_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdGrantedExempts.NeedDataSource
        Try
            LoadExemptionGranted()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub grdExternalPassed_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdExternalPassed.NeedDataSource
        Try
            LoadExemptionGranted()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Private Functions"

    Private Sub LoadHeaderText()
        Try
            lblFirstLast.Text = AptifyEbusinessUser1.FirstName + " " + AptifyEbusinessUser1.LastName
            lblStudentNumber.Text = CStr(AptifyEbusinessUser1.PersonID)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub LoadApplicationDetails()
        Try
            Dim sSql As String = Database & "..spGetExemptionAppDetailsForStudEnroll__c @StudentID=" & AptifyEbusinessUser1.PersonID
            Dim dtExmpDetails As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtExmpDetails Is Nothing AndAlso dtExmpDetails.Rows.Count > 0 Then
                ddlRoute.SelectedValue = Convert.ToString(dtExmpDetails.Rows(0)("RouteOfEntry"))
                ViewState("OldRoute") = dtExmpDetails.Rows(0)("Route")
                If Convert.ToString(ViewState("OldRoute")).Trim.ToLower = "elevation" Then
                    ViewState("OldRoute") = "Flexible Option"
                End If
                ViewState("ExemptionApp") = dtExmpDetails.Rows(0)("ID")
                ViewState("EnrollmentComments") = dtExmpDetails.Rows(0)("EnrollmentComments")
                lblStudentNumber.Text = CStr(dtExmpDetails.Rows(0)("OldID")) ' Redmine #16501
                ''Added BY Pradip 2016-02-12 for Issue no 5418
                ViewState("OldCompanyID") = dtExmpDetails.Rows(0)("CompanyID")
                hdnCompanyId.Value = dtExmpDetails.Rows(0)("CompanyID")
                ViewState("FirmApprovalStatus") = dtExmpDetails.Rows(0)("FirmApprovalStatus")
                txtFirm.Text = Convert.ToString(dtExmpDetails.Rows(0)("CompanyName"))
                RoutesInformation()
                txtAddrLine1.Text = dtExmpDetails.Rows(0)("Line1").ToString()
                txtAddrLine2.Text = dtExmpDetails.Rows(0)("Line2").ToString()
                txtPostalCode.Text = dtExmpDetails.Rows(0)("PostalCode").ToString()
                If txtFirm.Text <> "" Then
                    If Convert.ToBoolean(dtExmpDetails.Rows(0)("ShareMyInfoWithFirm")) = True Then
                        chkInfoToFirm.Checked = True
                    Else
                        chkInfoToFirm.Checked = False
                    End If
                    ''Added By Pradip 2016-01-04 For Group 3 Tracker G3-51
                Else
                    chkInfoToFirm.Checked = False
                End If
                ViewState("ShareMyInfoWithFirm") = dtExmpDetails.Rows(0)("ShareMyInfoWithFirm")
                ViewState("OldRTO") = dtExmpDetails.Rows(0)("RTO__c")
                ViewState("FirmApprovalStatus") = dtExmpDetails.Rows(0)("FirmApprovalStatus").ToString()
                LoadDataAsPerRouteOfEntry()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub LoadDataAsPerRouteOfEntry()
        Try
            If txtFirm.Text <> "" Then
                Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
                If ViewState("FirmApprovalStatus").ToString() = "" Or ViewState("FirmApprovalStatus").ToString().ToLower() = "pending" Then
                    lblEEMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.FirmApprovalStatusBlankOrPending")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    'btnSubmit.Visible = False
                ElseIf EducationContract.Trim.ToLower = "elevation" AndAlso chkInfoToFirm.Checked _
                        AndAlso ViewState("FirmApprovalStatus").ToString().ToLower() = "approved" _
                        AndAlso Convert.ToBoolean(ViewState("OldRTO")) = True Then
                    txtFirm.Enabled = False
                    ' btnSubmit.Visible = False
                    lblEEMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ElevationRouteFirmStatusApprovedWithRTO")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '"Must contact CAI to make the change"
                ElseIf EducationContract.Trim.ToLower = "elevation" AndAlso Not chkInfoToFirm.Checked _
                    AndAlso Convert.ToBoolean(ViewState("OldRTO")) = False Then
                    txtFirm.Enabled = True
                    btnSubmit.Enabled = True
                ElseIf EducationContract.Trim.ToLower = "contract" AndAlso txtFirm.Text = String.Empty Then
                    txtFirm.Enabled = True
                    btnSubmit.Enabled = True
                ElseIf EducationContract.Trim.ToLower = "elevation" AndAlso txtFirm.Text = String.Empty Then
                    txtFirm.Enabled = True
                    btnSubmit.Enabled = True
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RoutesInformation()
        Try
            Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
            chkInfoToFirm.Checked = False
            autoCompany.ContextKey = ddlRoute.SelectedItem.ToString()
            If Convert.ToString(EducationContract).Trim.ToLower = "contract" Then
                chkInfoToFirm.Enabled = False
                ''Commented By Pradip 2016-01-04 For Group 3 Tracker G3-51
                'chkInfoToFirm.Checked = True
                ''Added By Pradip 2016-01-04 For Group 3 Tracker G3-51
                If txtFirm.Text.Trim <> "" Then
                    chkInfoToFirm.Checked = True
                End If
                If ViewState("OldRoute") IsNot Nothing AndAlso ViewState("OldRoute").ToString().ToLower() = "undecided" Then
                    'changes ee app status to Change Request
                    'EnrollDetails.Visible = False
                    'EnrollDetails.Style.Item("Display") = "none"
                    btnSubmit.Visible = True
                ElseIf ViewState("OldRoute") IsNot Nothing AndAlso ViewState("OldRoute").ToString().ToLower() = "flexible option" Then
                    txtFirm.Enabled = True
                    '  EnrollDetails.Visible = False
                    btnSubmit.Visible = True
                    'changes ee app status to Change Request
                ElseIf ViewState("OldRoute") IsNot Nothing AndAlso ViewState("OldRoute").ToString().ToLower() = "contract" Then
                    If txtFirm.Text <> "" Then
                        ddlRoute.Enabled = False
                        txtFirm.Enabled = False
                        lblEEMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ContactRegistrationChanges")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Else
                        ViewState("RouteChanged") = False
                    End If
                    btnSubmit.Visible = True
                    'Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.ContractOrMasterRoutes")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            ElseIf Convert.ToString(EducationContract).Trim.ToLower = "undecided" Then
                chkInfoToFirm.Enabled = True
                btnSubmit.Visible = False
                lblEEMessage.Text = "Not allowed to submit"
                'Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.OtherrRoutes")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            ElseIf Convert.ToString(EducationContract).Trim.ToLower = "flexible option" Then
                chkInfoToFirm.Enabled = True
                btnSubmit.Visible = True
                If ViewState("OldRoute") IsNot Nothing AndAlso ViewState("OldRoute").ToString().ToLower() = "undecided" AndAlso chkInfoToFirm.Checked Then
                    'changes ee app status to Change Request
                    '  EnrollDetails.Visible = False
                    btnSubmit.Visible = True
                ElseIf ViewState("OldRoute") IsNot Nothing AndAlso ViewState("OldRoute").ToString().ToLower() = "undecided" AndAlso Not chkInfoToFirm.Checked Then
                    'changes ee app status to Change Request
                    '  EnrollDetails.Visible = True
                    btnSubmit.Visible = True
                ElseIf Convert.ToString(ViewState("OldRoute")) = ddlRoute.SelectedItem.ToString Then
                    LoadDataAsPerRouteOfEntry()
                    ShowEnrollmentDetails()
                    ViewState("RouteChanged") = False
                End If
            ElseIf Convert.ToString(EducationContract).Trim.ToLower = "select one" Then
                btnSubmit.Visible = False
            Else
                btnSubmit.Visible = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub LoadExemptionGranted()
        Try
            lblGrantExemptedMsg.Text = ""
            Dim sSql As String = Database & "..spGetCertificatesForStudEnroll__c @StudentID=" & AptifyEbusinessUser1.PersonID
            Dim dtCertDetails As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)

            If Not dtCertDetails Is Nothing AndAlso dtCertDetails.Rows.Count > 0 Then
                grdGrantedExempts.DataSource = dtCertDetails.Select("Type='Exemption'")
                grdGrantedExempts.DataBind()
                grdGrantedExempts.Visible = True
                Dim drExternal() As DataRow = dtCertDetails.Select("Type='External'")
                If Not drExternal Is Nothing AndAlso drExternal.Length > 0 Then
                    grdExternalPassed.DataSource = dtCertDetails.Select("Type='External'")
                    grdExternalPassed.DataBind()
                    grdExternalPassed.Visible = True
                End If

            Else
                grdGrantedExempts.Visible = False
                'Added by Sheela as part of CNM-9:Exemption Section display logic 
                dvExceptionGranted.Visible = False
                lblGrantExemptedMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.ExemptionGranted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                grdExternalPassed.Visible = True
                lblExternalPassedMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.ExemptionGranted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Function CheckDisplayCourseEnrollLink() As Boolean
        Dim bResult As Boolean = False
        Try
            Dim sSQL As String = "..spCheckAccessForStudEnrollPage__c @StudentID=" & AptifyEbusinessUser1.PersonID
            Dim lStudentID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
            If lStudentID > 0 Then
                Return True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return bResult
    End Function

    Private Sub LoadCAP1Courses()
        'Dim sSQL As String = "SELECT Name 'Course', Name 'CSummerCourse', Name 'CRevisionCourse', Name 'CRepeatCourse', Name 'CSummerIA'," _
        '                     + "Name 'CSummerExam', Name 'CAutumnIA', Name 'CAutumnExam', Name 'CAlternateGroup'," _
        '                    + "Name 'NSummerCourse', Name 'NRevisionCourse', Name 'NRepeatCourse', Name 'NSummerIA'," _
        '                    + "Name 'NSummerExam', Name 'NAutumnIA', Name 'NAutumnExam', Name 'NAlternateGroup' FROM vwCourses WHERE ID = 1"
        'Dim dt As DataTable = DataAction.GetDataTable(sSQL)
        'grdCAP1Courses.DataSource = dt
        'grdCAP1Courses.DataBind()
        LoadStudentCourseEnrollmentDataByStudent()
        gvCurriculumCourse.Rebind()

    End Sub

    ''' <summary>
    ''' Load student course enrollment data - for selected student
    ''' </summary>
    ''' 
    Private Sub LoadStudentCourseEnrollmentDataByStudent()
        Try
            lblNoRecords.Text = String.Empty
            ' Check OutStanding Result if yes dont allow to enrolled 
            Dim sSqlOutStanding As String = Database & "..spCheckOutstandingResult__c @CurrentAcademicCycleID=" & ViewState("AcademicCycleID") & ",@StudentID=" & AptifyEbusinessUser1.PersonID
            Dim bIsOutStanding As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlOutStanding, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If bIsOutStanding = False Then
                ' For Rule Name

                Dim oRuleEngine As Aptify.Consulting.RuleEngine__c = New Aptify.Consulting.RuleEngine__c(DataAction, AptifyApplication)
                Dim sRuleName As String = oRuleEngine.CheckRuleForStudent(ViewState("AcademicCycleID"), AptifyEbusinessUser1.PersonID)

                If Not ddlCAP1StudGrp Is Nothing AndAlso Not String.IsNullOrEmpty(ddlCAP1StudGrp.SelectedValue) Then

                    Dim sSql As String = Database & "..spGetStudentCourseEnrollmenByPerson__c @AcademicCycleID=" & ViewState("AcademicCycleID") & ",@StudentID=" & AptifyEbusinessUser1.PersonID & ",@StudentGroupID=" & ddlCAP1StudGrp.SelectedValue & ",@RuleName='" & sRuleName & "'"
                    _studentEnrollmentDetails = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                    ViewState("_studentEnrollmentDetails") = _studentEnrollmentDetails

                    If Not _studentEnrollmentDetails Is Nothing AndAlso _studentEnrollmentDetails.Rows.Count > 0 Then
                        For Each dr As DataRow In _studentEnrollmentDetails.Rows
                            If dr("IsFAEElective") = 1 Then
                                Dim classroom As String = Convert.ToString(dr("Classroom"))
                                Dim sClassRoom As String() = classroom.Split(New Char() {";"c})

                                Dim Revision As String = Convert.ToString(dr("Revision"))
                                Dim sRevision As String() = Revision.Split(New Char() {";"c})
                                Dim InterimAssessment As String = Convert.ToString(dr("InterimAssessment"))
                                Dim sInterimAssessment As String() = InterimAssessment.Split(New Char() {";"c})
                                Dim MockExam As String = Convert.ToString(dr("MockExam"))
                                Dim sMockExam As String() = MockExam.Split(New Char() {";"c})
                                Dim SummerExam As String = Convert.ToString(dr("SummerExam"))
                                Dim sSummerExam As String() = SummerExam.Split(New Char() {";"c})
                                Dim RepeatRevision As String = Convert.ToString(dr("RepeatRevision"))
                                Dim sRepeatRevision As String() = RepeatRevision.Split(New Char() {";"c})
                                Dim ResitInterimAssessment As String = Convert.ToString(dr("ResitInterimAssessment"))
                                Dim sResitInterimAssessment As String() = ResitInterimAssessment.Split(New Char() {";"c})
                                Dim AutumnExam As String = Convert.ToString(dr("AutumnExam"))
                                Dim sAutumnExam As String() = AutumnExam.Split(New Char() {";"c})
                                If sClassRoom(4) = "2" OrElse sRevision(4) = "2" OrElse sInterimAssessment(4) = "2" OrElse sMockExam(4) = "2" OrElse sSummerExam(4) = "2" OrElse sRepeatRevision(4) = "2" OrElse sResitInterimAssessment(4) = "2" OrElse sAutumnExam(4) = "2" Then
                                    ViewState("IsAlreadyFAEtaken") = True
                                    Exit For
                                End If
                            End If
                        Next
                    Else
                        lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ClosedEnrollment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        lblEnrollmentMsg.Visible = True
                    End If
                Else
                    lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ClosedEnrollment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblEnrollmentMsg.Visible = True
                    lblMessage.ForeColor = Color.Red
                End If

                Dim sNextAcademicCycle As String = Database & "..spGetNextAcademicCycleStudentEnrollment__c @AcademicCycleID=" & ViewState("AcademicCycleID") & ",@StudentID=" & AptifyEbusinessUser1.PersonID & ",@RuleName='" & sRuleName & "'"
                    Dim dtAcademicCycles As DataTable = DataAction.GetDataTable(sNextAcademicCycle, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If dtAcademicCycles IsNot Nothing AndAlso dtAcademicCycles.Rows.Count > 0 Then
                        If dtAcademicCycles.Rows.Count = 1 Then
                            ' lblNextAcademicCycleName.Text = dtAcademicCycles.Rows(0)("CalcName").ToString()
                            ViewState("NextAcademicCycle") = dtAcademicCycles.Rows(0)("CalcName").ToString()
                            ViewState("NextAcademicCycleID") = dtAcademicCycles.Rows(0)("ID").ToString()

                        Else
                            If Convert.ToInt32(dtAcademicCycles.Rows(0)("ExamSessionID")) = 1 Then
                                ViewState("NextAcademicCycle") = dtAcademicCycles.Rows(0)("CalcName").ToString()
                                ViewState("NextAcademicCycleID") = dtAcademicCycles.Rows(0)("ID").ToString()
                            ElseIf Convert.ToInt32(dtAcademicCycles.Rows(1)("ExamSessionID")) = 1 Then
                                ViewState("NextAcademicCycle") = dtAcademicCycles.Rows(1)("CalcName").ToString()
                                ViewState("NextAcademicCycleID") = dtAcademicCycles.Rows(1)("ID").ToString()


                            End If
                            If Convert.ToInt32(dtAcademicCycles.Rows(0)("ExamSessionID")) = 2 Then
                                ViewState("CurrentAcademicCycle") = dtAcademicCycles.Rows(0)("CalcName").ToString()
                                ViewState("AcademicCycleID") = dtAcademicCycles.Rows(0)("ID").ToString()
                            ElseIf Convert.ToInt32(dtAcademicCycles.Rows(1)("ExamSessionID")) = 2 Then
                                ViewState("CurrentAcademicCycle") = dtAcademicCycles.Rows(1)("CalcName").ToString()
                                ViewState("AcademicCycleID") = dtAcademicCycles.Rows(1)("ID").ToString()
                            End If
                        End If
                    End If
                Else
                    btnDisplayPaymentSummey.Visible = False
                lblEnrolmsg.Visible = False
            End If



        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Function GetPrice(ByVal lProductID As Long, ByVal classID As Long, ByVal StudentGroupID As Long, ByRef TaxAmount As Decimal) As String
        Try
            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
            Dim oOL As Aptify.Applications.OrderEntry.OrderLinesEntity
            'Here get the Top 1 Person ID whose MemberTypeID = 1 
            oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
            oOrder.ShipToID = AptifyEbusinessUser1.PersonID
            oOrder.BillToID = AptifyEbusinessUser1.PersonID
            oOrder.AddProduct(lProductID, 1)
            Dim sPrice As String
            If oOrder.SubTypes("OrderLines").Count > 0 Then
                oOL = TryCast(oOrder.SubTypes("OrderLines").Item(0), OrderLinesEntity)
                With oOL
                    .ExtendedOrderDetailEntity.SetValue("ClassID", classID)
                    .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                    .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", 1)
                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", StudentGroupID)
                    .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", Convert.ToInt32(ViewState("ExternalTypeID")))
                    .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                End With
                oOL.SetProductPrice()

                Dim dPaymentAmount As Decimal
                If Convert.ToBoolean(oOL.GetValue("TaxIncludedInPrice")) = True AndAlso oOL.SubTypes("OrderLineTaxes").Count > 0 Then
                    'Dim dTaxableAmt As Decimal = Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount"))
                    'dPaymentAmount = dTaxableAmt
                    dPaymentAmount = Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount"))
                    '  dPaymentAmount = ViewState("CurrencyTypeID") & Format(CDec(dTaxableAmt), "0.00")

                Else
                    dPaymentAmount = oOL.Extended
                    'If ViewState("AddedTaxAmount") Is Nothing Then
                    '    ViewState("AddedTaxAmount") = dTaxAmount
                    '    dPaymentAmount = oOL.Extended + dTaxAmount
                    'Else
                    '    dPaymentAmount = oOL.Extended
                    'End If

                    'If dTaxAmount > 0 Then

                    '    If ViewState("AddedTaxAmount") Is Nothing Then
                    '        ViewState("AddedTaxAmount") = dTaxAmount
                    '        dPaymentAmount = oOL.Extended + dTaxAmount
                    '    Else

                    '        dPaymentAmount = oOL.Extended
                    '    End If
                    'Else
                    '    dPaymentAmount = oOL.Extended
                    'End If

                End If

                'Dim dShippingAmount As Double = Convert.ToDouble(oOrder.GetValue("CALC_ShippingCharge"))
                'Dim dHandlingAmount As Double = Convert.ToDouble(oOrder.GetValue("CALC_HandlingCharge"))
                Dim dTaxAmount As Double = Convert.ToDouble(oOrder.GetValue("CALC_SalesTax"))
                TaxAmount = dTaxAmount
                Dim dTaxamt As Decimal
                If dTaxAmount > 0 Then
                    lblTaxAmount.Visible = True
                    lblTax.Visible = True
                    ViewState("dTaxamt") = CDec(ViewState("dTaxamt")) + dTaxAmount

                    lblTaxAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(ViewState("dTaxamt")), "0.00")
                End If
                sPrice = ViewState("CurrencyTypeID") & Format(CDec(dPaymentAmount), "0.00")
                Return sPrice

                '  Return Convert.ToString(oOL.Price)
            Else
                Return Convert.ToString(ViewState("CurrencyTypeID") & 0)
            End If
        Catch ex As Exception
            Return Convert.ToString(0)
        End Try
    End Function

    Private Sub GetPrefferedCurrency()
        Try
            Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & AptifyEbusinessUser1.PersonID
            Dim dt As DataTable = DataAction.GetDataTable(sSql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ViewState("CurrencyTypeID") = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                lblCurrency.Text = ViewState("CurrencyTypeID")
            End If


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub gvCurriculumCourse_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvCurriculumCourse.ItemDataBound
        Try
            Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))

            If TypeOf e.Item Is GridDataItem Then
                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim IsCourseJurisdiction As Label = DirectCast(item.FindControl("lblIsCourseJurisdiction"), Label)
                item.Display = CBool(IsCourseJurisdiction.Text)
                If item.Display = False Then
                    Exit Sub
                End If
                Dim ClassRoom As String = Convert.ToString(DataBinder.Eval(item.DataItem, "ClassRoom"))
                Dim chkClassRoomTxt() As String = ClassRoom.Split(CChar(";"))
                Dim RevisionDetails As String = Convert.ToString(DataBinder.Eval(item.DataItem, "Revision"))
                Dim chkRevisiontxt() As String = RevisionDetails.Split(CChar(";"))

                Dim RepeatRevisiontxt As String = Convert.ToString(DataBinder.Eval(item.DataItem, "RepeatRevision"))
                Dim chkRepeatRevisionTxt() As String = RepeatRevisiontxt.Split(CChar(";"))

                Dim ResitInterimAssessment As String = Convert.ToString(DataBinder.Eval(item.DataItem, "ResitInterimAssessment"))
                Dim chkResitInterimAssessmentTxt() As String = ResitInterimAssessment.Split(CChar(";"))

                Dim AutumnExam As String = Convert.ToString(DataBinder.Eval(item.DataItem, "AutumnExam"))
                Dim chkRAutumnExamTxt() As String = AutumnExam.Split(CChar(";"))

                Dim InterimAssessment As String = Convert.ToString(DataBinder.Eval(item.DataItem, "InterimAssessment"))
                Dim chkInterimAssessmenttxt() As String = InterimAssessment.Split(CChar(";"))

                Dim MockExam As String = Convert.ToString(DataBinder.Eval(item.DataItem, "MockExam"))
                Dim chkMockExamtxt() As String = MockExam.Split(CChar(";"))

                Dim SummerExam As String = Convert.ToString(DataBinder.Eval(item.DataItem, "SummerExam"))
                Dim chkSummerExamtxt() As String = SummerExam.Split(CChar(";"))

                Dim isFAEElective As Boolean = CBool(item.GetDataKeyValue("IsFAEElective"))
                Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)
                Dim chkClassRoom As CheckBox = DirectCast(item.FindControl("chkClassRoom"), CheckBox)

                If isFAEElective = True Then
                    If chkClassRoomTxt(4) = "2" OrElse chkRevisiontxt(4) = "2" OrElse chkRepeatRevisionTxt(4) = "2" OrElse chkResitInterimAssessmentTxt(4) = "2" OrElse chkRAutumnExamTxt(4) = "2" OrElse chkInterimAssessmenttxt(4) = "2" OrElse chkMockExamtxt(4) = "2" OrElse chkSummerExamtxt(4) = "2" Then
                        ViewState("IsAlreadyFAEtaken") = True
                    End If
                    For i As Integer = 3 To item.Controls.Count - 1

                        TryCast(item.Controls(i), GridTableCell).Enabled = False
                    Next
                    If ViewState("IsAlreadyFAEtaken") Then
                        For i As Integer = 0 To item.Controls.Count - 1

                            TryCast(item.Controls(i), GridTableCell).Enabled = False
                        Next
                    Else

                    End If

                End If
                If chkClassRoomTxt(4) = "2" OrElse chkRevisiontxt(4) = "2" OrElse chkInterimAssessmenttxt(4) = "2" OrElse chkMockExamtxt(4) = "2" OrElse chkSummerExamtxt(4) = "2" Then
                    ViewState("IsSummerEnrolledcomeing") = True
                End If
                If chkRepeatRevisionTxt(4) = "2" OrElse chkResitInterimAssessmentTxt(4) = "2" OrElse chkRAutumnExamTxt(4) = "2" Then
                    ViewState("IsAutumEnrolledcomeing") = True
                End If

                Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)
                Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
                Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)

                Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)

                Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)
                Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
                Dim chkIsFAEElective As CheckBox = DirectCast(item.FindControl("chkIsFAEElective"), CheckBox)
                Dim Curriculum As String = Convert.ToString(DataBinder.Eval(item.DataItem, "Curriculum"))
                Dim CourseID As Integer = Convert.ToInt32(DataBinder.Eval(item.DataItem, "SubjectID"))
                Dim lblIsCore__c As Label = DirectCast(item.FindControl("lblIsCore__c"), Label)
                Dim lblSubject As Label = DirectCast(item.FindControl("lblSubject"), Label)
                Dim lblCutOffUnits As Label = DirectCast(item.FindControl("lblCutOffUnits"), Label)
                Dim lblMinimumUnits As Label = DirectCast(item.FindControl("lblMinimumUnits"), Label)
                Dim lblUnits As Label = DirectCast(item.FindControl("lblUnits"), Label)
                Dim lblCurriculumID As Label = DirectCast(item.FindControl("lblCurriculumID"), Label)
                ' Dim lblAcademicCycleID As Label = DirectCast(item.FindControl("lblAcademicCycleID"), Label)
                Dim lblIsFAEElective As Label = DirectCast(item.FindControl("lblIsFAEElective"), Label)
                Dim lblAlternativeGroup As Label = DirectCast(item.FindControl("lblAlternativeGroup"), Label)

                If isFAEElective Then
                    If chkInterimAssessment.Checked AndAlso chkMockExam.Checked AndAlso chkSummerExam.Checked Then
                        If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                            Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkClassRoomTxt(0)) & "' AND ProductID='" & Convert.ToString(chkClassRoomTxt(1)) & "'")
                            If dr.Length > 0 Then
                                chkClassRoom.Checked = True
                                chkClassRoom.Enabled = True
                                chkIsFAEElective.Checked = True
                            End If
                        Else
                            ' chkClassRoom.Checked = True
                        End If

                    Else
                        If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                            chkIsFAEElective.Checked = False
                            chkIsFAEElective.Enabled = False  '  Code added for https://redmine.softwaredesign.ie/issues/17660 this redmine log
                        Else
                            chkIsFAEElective.Enabled = True
                        End If

                        'chkIsFAEElective
                        If chkAutumnExam.Enabled = True Then
                            chkIsFAEElective.Enabled = True
                            If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                                Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkRepeatRevisionTxt(0)) & "' AND ProductID='" & Convert.ToString(chkRepeatRevisionTxt(1)) & "'")
                                If dr.Length > 0 Then
                                    Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
                                    Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))
                                    If Convert.ToString(lblIsFAEElective.Text.Trim) = "1" Then
                                        chkIsFAEElective.Enabled = True
                                    End If

                                    If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = True AndAlso chkIsFAEElective.Checked = True Then
                                        For i As Integer = 0 To item.Controls.Count - 1
                                            TryCast(item.Controls(i), GridTableCell).Enabled = True
                                        Next
                                    End If
                                    If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False AndAlso chkIsFAEElective.Checked = False Then
                                        For i As Integer = 0 To item.Controls.Count - 1
                                            TryCast(item.Controls(i), GridTableCell).Enabled = True
                                        Next
                                    End If
                                    If chkIsFAEElective.Checked AndAlso chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = True Then
                                        If Convert.ToInt32(chkRepeatRevisionTxt(1) > 0) AndAlso chkRepeatRevision.Enabled = True Then
                                            chkRepeatRevision.Checked = True
                                            If Convert.ToInt32(chkAutumnExamtxt(1) > 0) Then
                                                chkAutumnExam.Checked = True
                                            End If
                                            If Convert.ToInt32(chkResitInterimAssessmentTxt(1) > 0) Then
                                                chkResitInterimAssessment.Checked = True
                                            End If
                                        ElseIf Convert.ToInt32(chkClassRoomTxt(1) > 0) AndAlso chkClassRoom.Enabled = True Then
                                            chkClassRoom.Checked = True
                                            If Convert.ToInt32(chkMockExamtxt(1) > 0) Then
                                                chkMockExam.Checked = True
                                            End If
                                            If Convert.ToInt32(chkInterimAssessmenttxt(1) > 0) Then
                                                chkInterimAssessment.Checked = True
                                            End If
                                            If Convert.ToInt32(chkSummerExamtxt(1) > 0) Then
                                                chkSummerExam.Checked = True
                                            End If

                                        End If
                                    End If
                                    If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False AndAlso chkIsFAEElective.Checked = False Then
                                        TryCast(item.Controls(2), GridTableCell).Enabled = True
                                        For i As Integer = 3 To item.Controls.Count - 1

                                            ''Commented By PRadip 2016-03-23 Not Neccessary
                                            'If Convert.ToInt32(chkRepeatRevisiontxt(1)) > 0 Then
                                            '    chkRepeatRevision.Checked = False
                                            'End If
                                            'If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                                            '    chkResitInterimAssessment.Checked = False
                                            'End If
                                            'If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                                            '    chkAutumnExam.Checked = False
                                            '    chkAutumnExam.Enabled = True
                                            'End If
                                            'If Convert.ToInt32(chkRevisiontxt(1)) > 0 Then
                                            '    chkRevision.Checked = False
                                            'End If
                                            'If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                                            '    chkResitInterimAssessment.Checked = False
                                            'End If
                                            'If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                                            '    chkAutumnExam.Checked = False
                                            'End If
                                            chkResitInterimAssessment.Enabled = True
                                            chkAutumnExam.Enabled = True
                                            TryCast(item.Controls(i), GridTableCell).Enabled = True
                                            TryCast(item.Controls(i).FindControl("chkClassRoom"), CheckBox).Checked = False
                                            CType(item.FindControl("chkInterimAssessment"), CheckBox).Checked = False
                                            CType(item.FindControl("chkMockExam"), CheckBox).Checked = False
                                            CType(item.FindControl("chkSummerExam"), CheckBox).Checked = False
                                            CType(item.FindControl("chkRevision"), CheckBox).Checked = False
                                            CType(item.FindControl("chkRepeatRevision"), CheckBox).Checked = False
                                            CType(item.FindControl("chkResitInterimAssessment"), CheckBox).Checked = False
                                            CType(item.FindControl("chkAutumnExam"), CheckBox).Checked = False
                                        Next
                                    End If
                                    If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False AndAlso chkIsFAEElective.Checked = True Then
                                        For i As Integer = 0 To item.Controls.Count - 1
                                            TryCast(item.Controls(i), GridTableCell).Enabled = False
                                        Next
                                    End If
                                Else
                                    Dim dr1() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkSummerExamtxt(0)) & "' AND ProductID='" & Convert.ToString(chkSummerExamtxt(1)) & "'")
                                    If dr1.Length > 0 Then
                                        chkIsFAEElective.Checked = True
                                        Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
                                        Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))
                                        If Convert.ToString(lblIsFAEElective.Text.Trim) = "1" Then
                                            chkIsFAEElective.Enabled = True
                                        End If

                                        If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = True AndAlso chkIsFAEElective.Checked = True Then
                                            For i As Integer = 0 To item.Controls.Count - 1
                                                TryCast(item.Controls(i), GridTableCell).Enabled = True
                                            Next
                                        End If
                                        If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False AndAlso chkIsFAEElective.Checked = False Then
                                            For i As Integer = 0 To item.Controls.Count - 1
                                                TryCast(item.Controls(i), GridTableCell).Enabled = True
                                            Next
                                        End If
                                        If chkIsFAEElective.Checked AndAlso chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = True Then
                                            If Convert.ToInt32(chkRepeatRevisionTxt(1) > 0) AndAlso chkRepeatRevision.Enabled = True Then
                                                chkRepeatRevision.Checked = False
                                                If Convert.ToInt32(chkAutumnExamtxt(1) > 0) Then
                                                    chkAutumnExam.Checked = False
                                                End If
                                                If Convert.ToInt32(chkResitInterimAssessmentTxt(1) > 0) Then
                                                    chkResitInterimAssessment.Checked = False
                                                End If
                                            ElseIf Convert.ToInt32(chkClassRoomTxt(1) > 0) AndAlso chkClassRoom.Enabled = True Then
                                                chkClassRoom.Checked = True
                                                If Convert.ToInt32(chkMockExamtxt(1) > 0) Then
                                                    chkMockExam.Checked = True
                                                End If
                                                If Convert.ToInt32(chkInterimAssessmenttxt(1) > 0) Then
                                                    chkInterimAssessment.Checked = True
                                                End If
                                                If Convert.ToInt32(chkSummerExamtxt(1) > 0) Then
                                                    chkSummerExam.Checked = True
                                                End If

                                            End If
                                        End If
                                        If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False AndAlso chkIsFAEElective.Checked = False Then
                                            TryCast(item.Controls(2), GridTableCell).Enabled = True
                                            For i As Integer = 3 To item.Controls.Count - 1

                                                ''Commented By PRadip 2016-03-23 Not Neccessary
                                                'If Convert.ToInt32(chkRepeatRevisiontxt(1)) > 0 Then
                                                '    chkRepeatRevision.Checked = False
                                                'End If
                                                'If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                                                '    chkResitInterimAssessment.Checked = False
                                                'End If
                                                'If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                                                '    chkAutumnExam.Checked = False
                                                '    chkAutumnExam.Enabled = True
                                                'End If
                                                'If Convert.ToInt32(chkRevisiontxt(1)) > 0 Then
                                                '    chkRevision.Checked = False
                                                'End If
                                                'If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                                                '    chkResitInterimAssessment.Checked = False
                                                'End If
                                                'If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                                                '    chkAutumnExam.Checked = False
                                                'End If
                                                chkResitInterimAssessment.Enabled = True
                                                chkAutumnExam.Enabled = True
                                                TryCast(item.Controls(i), GridTableCell).Enabled = True
                                                TryCast(item.Controls(i).FindControl("chkClassRoom"), CheckBox).Checked = False
                                                CType(item.FindControl("chkInterimAssessment"), CheckBox).Checked = False
                                                CType(item.FindControl("chkMockExam"), CheckBox).Checked = False
                                                CType(item.FindControl("chkSummerExam"), CheckBox).Checked = False
                                                CType(item.FindControl("chkRevision"), CheckBox).Checked = False
                                                CType(item.FindControl("chkRepeatRevision"), CheckBox).Checked = False
                                                CType(item.FindControl("chkResitInterimAssessment"), CheckBox).Checked = False
                                                CType(item.FindControl("chkAutumnExam"), CheckBox).Checked = False
                                            Next
                                        End If
                                        If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False AndAlso chkIsFAEElective.Checked = True Then
                                            For i As Integer = 0 To item.Controls.Count - 1
                                                TryCast(item.Controls(i), GridTableCell).Enabled = False
                                            Next
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If



                '  Dim sSqlCompanyRTOorCentrallyManaged As String = Database & "..spCheckCompanyRTOOrCentrallyManaged__c @CompanyID=" & AptifyApplication.GetEntityRecordIDFromRecordName("Companies", txtFirm.Text.Trim)
                Dim sSqlCompanyRTOorCentrallyManaged As String = Database & "..spCheckCompanyRTOOrCentrallyManagedOrExpired__c @CompanyID=" & hdnCompanyId.Value & ",@PersonID=" & AptifyEbusinessUser1.PersonID  ' added for https://redmine.softwaredesign.ie/issues/17083
                Dim dtCompanyDetails As DataTable = DataAction.GetDataTable(sSqlCompanyRTOorCentrallyManaged, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtCompanyDetails Is Nothing AndAlso dtCompanyDetails.Rows.Count > 0 Then
                    ViewState("RTO") = dtCompanyDetails.Rows(0)("RTO__c")
                    ViewState("CentrallyManaged") = dtCompanyDetails.Rows(0)("CentrallyManaged__c")
                End If

                ' Get Revision Product ID 

                Dim SummerRevisionProductID As Integer = Convert.ToInt32(chkRevisiontxt(1))

                'New code added for Whoes Auto Select on 26 Oct

                If bCheckWhoesAutoSelect = False Then
                    bCheckWhoesAutoSelect = True
                    Dim sAutoSelectSP As String = Database & "..spCheckWhosDefaultSessionAutoChecked__c @CurriculumID=" & lblCurriculumID.Text & _
                        ",@SummerAcademicCycleID=" & ViewState("NextAcademicCycleID") & ",@AutumnAcademicCycle=" & ViewState("AcademicCycleID")
                    sAutoEnrolledSelection = Convert.ToString(DataAction.ExecuteScalar(sAutoSelectSP, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If sAutoEnrolledSelection <> "" Then

                    End If
                End If




                ' Get Class Room Product ID 

                Dim SummerClassRoomProductID As Integer = Convert.ToInt32(chkClassRoomTxt(1))
                '' Get RepeatRevision Product ID 

                Dim SummerRepeatRevisionProductID As Integer = Convert.ToInt32(chkRepeatRevisionTxt(1))
                Dim bCentrallManageResitAssessment As Boolean
                Dim bCentrallManageResitExam As Boolean


                chkRepeatRevision.Enabled = True
                chkResitInterimAssessment.Enabled = True
                chkAutumnExam.Enabled = True

                ''Commented By Pradip 2016-02-03 For Issue Number 5371
                ' need to be verifyied student taken summer classes or not
                '' ''Dim sSqlRepeatRevision As String = Database & "..spCheckEnrolledOnSummerExam__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@Curriculum='" & Curriculum & "',@CourseID=" & CourseID & ",@Type='Revision'"
                '' ''Dim lClassRegRevisionID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlRepeatRevision, IAptifyDataAction.DSLCacheSetting.BypassCache))
                '' ''If lClassRegRevisionID > 0 Then
                '' ''Else
                '' ''    chkRepeatRevision.Enabled = False
                '' ''End If
                Dim sSqlResitInterim As String = Database & "..spCheckEnrolledOnSummerExam__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@AcademicCycleID=1" & ",@Curriculum='" & Curriculum & "',@CourseID=" & CourseID & ",@Type='Interim Assessment'"
                Dim lClassResitInterimID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlResitInterim, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lClassResitInterimID > 0 Then
                Else
                    chkResitInterimAssessment.Enabled = False
                End If
                Dim sSqlAutumnExam As String = Database & "..spCheckEnrolledOnSummerExam__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@Curriculum='" & Curriculum & "',@CourseID=" & CourseID & ",@Type='Exam'"
                Dim lClasssExamID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlResitInterim, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lClasssExamID > 0 Then
                Else
                    chkAutumnExam.Enabled = False
                End If

                '' Else
                ''Commented By Pradip 2016-02-03 For Issue Number 5371
                ''''chkRepeatRevision.Enabled = False
                chkResitInterimAssessment.Enabled = False
                chkAutumnExam.Enabled = False
                ''  End If



                If SummerRepeatRevisionProductID > 0 Then
                    ' Check the student route is elevation/contract and the firm is centrally managed and is paying for the courses using who pays then student cannot enroll on these courses
                    ViewState("IsAutumnCourseAvailable") = True
                    ' lblCurrentAcademicCycleName.Text = AptifyApplication.GetEntityRecordName("AcademicCycles__c", Convert.ToInt32(chkRepeatRevisionTxt(6)))

                    'If ddlRoute.SelectedItem.ToString().Trim.ToLower = "flexible option" OrElse ddlRoute.SelectedItem.ToString().Trim.ToLower = "contract" Then
                    If EducationContract.Trim.ToLower = "elevation" OrElse EducationContract.ToString().Trim.ToLower = "contract" Then
                        If WhoPaysForElevationStudent(SummerRepeatRevisionProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkRepeatRevisionTxt(0))) Then
                            If CBool(ViewState("CentrallyManaged")) Then
                                chkRepeatRevision.Enabled = False
                                chkRepeatRevision.Checked = False
                            ElseIf CBool(ViewState("RTO")) Then
                                chkRepeatRevision.Enabled = True
                            End If


                        Else
                            chkRepeatRevision.Enabled = True
                        End If
                    End If
                Else
                    chkRepeatRevision.Enabled = False
                End If
                ' Get ResitInterimAssessment Product ID 

                Dim SummerResitInterimAssessmentProductID As Integer = Convert.ToInt32(chkResitInterimAssessmentTxt(1))
                If SummerResitInterimAssessmentProductID > 0 Then
                    ' Check the student route is elevation/contract and the firm is centrally managed and is paying for the courses using who pays then student cannot enroll on these courses
                    ViewState("IsAutumnCourseAvailable") = True
                    'lblCurrentAcademicCycleName.Text = AptifyApplication.GetEntityRecordName("AcademicCycles__c", Convert.ToInt32(chkResitInterimAssessmentTxt(6)))

                    If EducationContract.Trim.ToLower = "elevation" OrElse EducationContract.Trim.ToLower = "contract" Then
                        If WhoPaysForElevationStudent(SummerResitInterimAssessmentProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkResitInterimAssessmentTxt(0))) Then
                            If CBool(ViewState("CentrallyManaged")) Then
                                chkResitInterimAssessment.Enabled = False
                                chkResitInterimAssessment.Checked = False
                                bCentrallManageResitAssessment = True
                            ElseIf CBool(ViewState("RTO")) Then
                                chkResitInterimAssessment.Enabled = True
                            End If

                        Else
                            chkResitInterimAssessment.Enabled = True
                        End If
                    End If
                Else
                    chkResitInterimAssessment.Enabled = False
                End If
                ' Get ResitInterimAssessment Product ID 

                Dim SummerAutumnExamProductID As Integer = Convert.ToInt32(chkRAutumnExamTxt(1))
                If SummerAutumnExamProductID > 0 Then
                    ' Check the student route is elevation/contract and the firm is centrally managed and is paying for the courses using who pays then student cannot enroll on these courses
                    ViewState("IsAutumnCourseAvailable") = True
                    'lblCurrentAcademicCycleName.Text = AptifyApplication.GetEntityRecordName("AcademicCycles__c", Convert.ToInt32(chkRAutumnExamTxt(6)))

                    If EducationContract.Trim.ToLower = "elevation" OrElse EducationContract.ToString().Trim.ToLower = "contract" Then
                        If WhoPaysForElevationStudent(SummerAutumnExamProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkRAutumnExamTxt(0))) Then
                            If CBool(ViewState("CentrallyManaged")) Then
                                chkAutumnExam.Enabled = False
                                chkAutumnExam.Checked = False
                                bCentrallManageResitExam = True
                            ElseIf CBool(ViewState("RTO")) Then
                                chkAutumnExam.Enabled = True
                            End If

                        Else
                            chkAutumnExam.Enabled = True
                        End If
                    End If
                    '' Check IS DEBK Checked
                    'Dim sDEBKSQL As String = Database & "..spCheckIsDEBKFailed__c @AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@PersonID=" & AptifyEbusinessUser1.PersonID & ",@CourseID=" & lblSubject.Text
                    'Dim lFailed As Long = Convert.ToInt32(DataAction.ExecuteScalar(sDEBKSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    'If lFailed > 0 Then
                    '    chkAutumnExam.Enabled = False
                    'End If
                Else
                    chkAutumnExam.Enabled = False
                End If


                ' Chk Autumn Available then 
                If chkRepeatRevisionTxt(0) > 0 Then
                    bAddedStudentGrp = True
                    ddlCAP1StudGrp.Enabled = False
                Else
                    If bAddedStudentGrp = False Then
                        CheckAlreadyEnrolled(ViewState("NextAcademicCycleID"))
                    End If
                End If

                Dim bClassRoomCentrallyManaged As Boolean = False
                Dim bRevisionCentrallyManaged As Boolean = False
                Dim bInterimCentrallyManaged As Boolean = False
                Dim bMockCentrallyManaged As Boolean = False
                Dim bExamCentrallyManaged As Boolean = False

                If SummerClassRoomProductID > 0 Then
                    ' Check the student route is elevation/contract and the firm is centrally managed and is paying for the courses using who pays then student cannot enroll on these courses
                    ' 
                    'lblNextAcademicCycleName.Text = AptifyApplication.GetEntityRecordName("AcademicCycles__c", Convert.ToInt32(chkClassRoomTxt(6)))

                    If chkClassRoom.Enabled = True AndAlso chkClassRoom.Checked = False Then
                        If EducationContract.Trim.ToLower = "elevation" OrElse EducationContract.Trim.ToLower = "contract" Then
                            If WhoPaysForElevationStudent(SummerClassRoomProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkClassRoomTxt(0))) Then
                                If CBool(ViewState("CentrallyManaged")) Then
                                    ' if FAE Elective
                                    If isFAEElective Then
                                        ' chk if FAE having already Registered and passed some elective then  bypass who pays
                                        Dim sSqlFAEElectiveByPassWhoPays As String = Database & "..spCheckFAEEnrolledCount__c @StudentID=" & AptifyEbusinessUser1.PersonID
                                        Dim bByPassWhoPays As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlFAEElectiveByPassWhoPays, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                        If bByPassWhoPays Then
                                            chkClassRoom.Enabled = True
                                        Else
                                            bClassRoomCentrallyManaged = True
                                            chkClassRoom.Enabled = False
                                            chkClassRoom.Checked = False
                                        End If
                                    Else
                                        bClassRoomCentrallyManaged = True
                                        chkClassRoom.Enabled = False
                                        chkClassRoom.Checked = False
                                    End If
                                ElseIf CBool(ViewState("RTO")) Then
                                    chkClassRoom.Enabled = True
                                End If

                            Else
                                If Convert.ToBoolean(lblIsCore__c.Text) Then
                                    chkClassRoom.Enabled = False
                                Else
                                    chkClassRoom.Enabled = True
                                End If
                            End If
                        End If
                    Else
                        If WhoPaysForElevationStudent(SummerClassRoomProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkClassRoomTxt(0))) Then
                            If CBool(ViewState("CentrallyManaged")) Then
                                ' if FAE Elective
                                If isFAEElective Then
                                    ' chk if FAE having already Registered and passed some elective then  bypass who pays
                                    Dim sSqlFAEElectiveByPassWhoPays As String = Database & "..spCheckFAEEnrolledCount__c @StudentID=" & AptifyEbusinessUser1.PersonID
                                    Dim bByPassWhoPays As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlFAEElectiveByPassWhoPays, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                    If bByPassWhoPays Then
                                        chkClassRoom.Enabled = True
                                    Else
                                        bClassRoomCentrallyManaged = True
                                        chkClassRoom.Enabled = False
                                        chkClassRoom.Checked = False
                                    End If
                                Else
                                    bClassRoomCentrallyManaged = True
                                    chkClassRoom.Enabled = False
                                    chkClassRoom.Checked = False
                                End If
                            ElseIf CBool(ViewState("RTO")) Then
                                chkClassRoom.Enabled = True
                            End If
                        Else
                            If Convert.ToBoolean(lblIsCore__c.Text) Then
                                chkClassRoom.Enabled = False

                            Else

                                chkClassRoom.Enabled = True
                            End If
                        End If
                    End If
                Else
                    chkClassRoom.Enabled = False
                End If

                If SummerRevisionProductID > 0 Then
                    ' Check the student route is elevation/contract and the firm is centrally managed and is paying for the courses using who pays then student cannot enroll on these courses
                    ' 
                    'lblNextAcademicCycleName.Text = AptifyApplication.GetEntityRecordName("AcademicCycles__c", Convert.ToInt32(chkRevisiontxt(6)))

                    If EducationContract.Trim.ToLower = "elevation" OrElse EducationContract.Trim.ToLower = "contract" Then
                        If WhoPaysForElevationStudent(SummerRevisionProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkRevisiontxt(0))) Then
                            If CBool(ViewState("CentrallyManaged")) Then
                                If isFAEElective Then
                                    ' chk if FAE having already Registered and passed some elective then  bypass who pays
                                    Dim sSqlFAEElectiveByPassWhoPays As String = Database & "..spCheckFAEEnrolledCount__c @StudentID=" & AptifyEbusinessUser1.PersonID
                                    Dim bByPassWhoPays As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlFAEElectiveByPassWhoPays, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                    If bByPassWhoPays Then
                                        chkRevision.Enabled = True
                                    Else
                                        bRevisionCentrallyManaged = True
                                        chkRevision.Enabled = False
                                        chkRevision.Checked = False
                                    End If
                                Else
                                    bRevisionCentrallyManaged = True
                                    chkRevision.Enabled = False
                                    chkRevision.Checked = False
                                End If
                            ElseIf CBool(ViewState("RTO")) Then
                                chkRevision.Enabled = True
                            End If

                        Else

                            chkRevision.Enabled = True
                        End If
                    End If
                Else
                    chkRevision.Enabled = False
                End If
                ' Get Revision Product ID 

                Dim SummerInterimAssessmentProductID As Integer = Convert.ToInt32(chkInterimAssessmenttxt(1))
                Dim bInterimCourseFirstTime As Boolean
                If SummerInterimAssessmentProductID > 0 Then
                    ' Check the student route is elevation/contract and the firm is centrally managed and is paying for the courses using who pays then student cannot enroll on these courses
                    ' 
                    '  lblNextAcademicCycleName.Text = AptifyApplication.GetEntityRecordName("AcademicCycles__c", Convert.ToInt32(chkInterimAssessmenttxt(6)))
                    ' Check Interim Exam taken first time 
                    Dim sSqlCourseFirstTime As String = Database & "..spStudentEnrollFirstTimeCourse__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@ClassID=" & chkClassRoomTxt(0)
                    bInterimCourseFirstTime = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlCourseFirstTime, IAptifyDataAction.DSLCacheSetting.BypassCache))

                    If chkInterimAssessment.Enabled = True AndAlso chkInterimAssessment.Checked = False Then

                        If EducationContract.Trim.ToLower = "elevation" OrElse EducationContract.Trim.ToLower = "contract" Then
                            If WhoPaysForElevationStudent(SummerInterimAssessmentProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkInterimAssessmenttxt(0))) Then
                                If CBool(ViewState("CentrallyManaged")) Then
                                    ' if FAE Elective
                                    If isFAEElective Then
                                        ' chk if FAE having already Registered and passed some elective then  bypass who pays
                                        Dim sSqlFAEElectiveByPassWhoPays As String = Database & "..spCheckFAEEnrolledCount__c @StudentID=" & AptifyEbusinessUser1.PersonID
                                        Dim bByPassWhoPays As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlFAEElectiveByPassWhoPays, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                        If bByPassWhoPays Then
                                            chkInterimAssessment.Enabled = False
                                            chkInterimAssessment.Checked = False
                                        Else
                                            bInterimCentrallyManaged = True
                                            chkInterimAssessment.Enabled = False
                                            chkInterimAssessment.Checked = False
                                        End If
                                    Else
                                        bInterimCentrallyManaged = True
                                        chkInterimAssessment.Enabled = False
                                        chkInterimAssessment.Checked = False
                                    End If
                                ElseIf CBool(ViewState("RTO")) Then
                                    chkInterimAssessment.Enabled = True
                                End If

                            Else
                                If Convert.ToBoolean(lblIsCore__c.Text) Then
                                    chkInterimAssessment.Enabled = False
                                Else
                                    'If Convert.ToBoolean(ViewState("EnrollFirstTime")) Then
                                    '    chkInterimAssessment.Enabled = False
                                    'Else
                                    '    chkInterimAssessment.Enabled = True
                                    'End If
                                    If bInterimCourseFirstTime Then
                                        chkInterimAssessment.Enabled = False
                                    Else
                                        chkInterimAssessment.Enabled = True
                                    End If
                                End If
                            End If
                        End If
                    Else
                        If WhoPaysForElevationStudent(SummerInterimAssessmentProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkInterimAssessmenttxt(0))) Then
                            If CBool(ViewState("CentrallyManaged")) Then
                                ' if FAE Elective
                                If isFAEElective Then
                                    ' chk if FAE having already Registered and passed some elective then  bypass who pays
                                    Dim sSqlFAEElectiveByPassWhoPays As String = Database & "..spCheckFAEEnrolledCount__c @StudentID=" & AptifyEbusinessUser1.PersonID
                                    Dim bByPassWhoPays As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlFAEElectiveByPassWhoPays, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                    If bByPassWhoPays Then
                                        chkInterimAssessment.Enabled = False
                                        chkInterimAssessment.Checked = False
                                    Else
                                        bInterimCentrallyManaged = True
                                        chkInterimAssessment.Enabled = False
                                        chkInterimAssessment.Checked = False
                                    End If
                                Else
                                    bInterimCentrallyManaged = True
                                    chkInterimAssessment.Enabled = False
                                    chkInterimAssessment.Checked = False
                                End If
                            ElseIf CBool(ViewState("RTO")) Then
                                chkInterimAssessment.Enabled = True
                            End If
                        Else
                            If Convert.ToBoolean(lblIsCore__c.Text) Then
                                chkInterimAssessment.Enabled = False
                            Else
                                If bInterimCourseFirstTime Then
                                    chkInterimAssessment.Enabled = False
                                Else
                                    chkInterimAssessment.Enabled = True
                                End If
                            End If
                        End If
                        '  chkInterimAssessment.Enabled = False
                    End If
                Else
                    chkInterimAssessment.Enabled = False
                End If

                ' Get MockExam Product ID 

                Dim SummerMockExamProductID As Integer = Convert.ToInt32(chkMockExamtxt(1))
                If SummerMockExamProductID > 0 Then
                    ' lblNextAcademicCycleName.Text = AptifyApplication.GetEntityRecordName("AcademicCycles__c", Convert.ToInt32(chkMockExamtxt(6)))

                    ' Check the student route is elevation/contract and the firm is centrally managed and is paying for the courses using who pays then student cannot enroll on these courses
                    If chkMockExam.Enabled = True AndAlso chkMockExam.Checked = False Then
                        ' 
                        If EducationContract.Trim.ToLower = "elevation" OrElse EducationContract.Trim.ToLower = "contract" Then
                            If WhoPaysForElevationStudent(SummerMockExamProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkMockExamtxt(0))) Then
                                If CBool(ViewState("CentrallyManaged")) Then
                                    bMockCentrallyManaged = True
                                    chkMockExam.Enabled = False
                                    chkMockExam.Checked = False
                                ElseIf CBool(ViewState("RTO")) Then
                                    chkMockExam.Enabled = True
                                End If

                            Else
                                If bInterimCourseFirstTime Then
                                    chkMockExam.Enabled = False
                                End If

                            End If
                        End If
                    Else
                        If WhoPaysForElevationStudent(SummerMockExamProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkMockExamtxt(0))) Then
                            If CBool(ViewState("CentrallyManaged")) Then
                                bMockCentrallyManaged = True
                                chkMockExam.Enabled = False
                                chkMockExam.Checked = False
                            ElseIf CBool(ViewState("RTO")) Then
                                chkMockExam.Enabled = True
                            End If
                        Else
                            If Convert.ToBoolean(lblIsCore__c.Text) Then

                                chkMockExam.Enabled = False
                            Else
                                If bInterimCourseFirstTime Then
                                    chkMockExam.Enabled = False
                                End If

                            End If
                        End If
                        chkMockExam.Enabled = False
                    End If
                Else
                    chkMockExam.Enabled = False
                End If

                ' Get MockExam Product ID 

                Dim SummerSummerExamProductID As Integer = Convert.ToInt32(chkSummerExamtxt(1))
                If SummerSummerExamProductID > 0 Then
                    ' Check the student route is elevation/contract and the firm is centrally managed and is paying for the courses using who pays then student cannot enroll on these courses
                    ' 
                    ' lblNextAcademicCycleName.Text = AptifyApplication.GetEntityRecordName("AcademicCycles__c", Convert.ToInt32(chkSummerExamtxt(6)))
                    ' Check Interim Exam taken first time 
                    Dim sSqlCourseFirstTime As String = Database & "..spStudentEnrollFirstTimeCourse__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@ClassID=" & chkClassRoomTxt(0)
                    Dim bExamCourseFirstTime As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlCourseFirstTime, IAptifyDataAction.DSLCacheSetting.BypassCache))

                    If chkSummerExam.Enabled = True AndAlso chkSummerExam.Checked = False Then

                        If EducationContract.Trim.ToLower = "elevation" OrElse EducationContract.Trim.ToLower = "contract" Then
                            If WhoPaysForElevationStudent(SummerSummerExamProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkSummerExamtxt(0))) Then
                                If CBool(ViewState("CentrallyManaged")) Then
                                    ' if FAE Elective
                                    If isFAEElective Then
                                        ' chk if FAE having already Registered and passed some elective then  bypass who pays
                                        Dim sSqlFAEElectiveByPassWhoPays As String = Database & "..spCheckFAEEnrolledCount__c @StudentID=" & AptifyEbusinessUser1.PersonID
                                        Dim bByPassWhoPays As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlFAEElectiveByPassWhoPays, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                        If bByPassWhoPays Then
                                            chkSummerExam.Enabled = False
                                            chkSummerExam.Checked = False
                                        Else
                                            bExamCentrallyManaged = True
                                            chkSummerExam.Enabled = False
                                            chkSummerExam.Checked = False
                                        End If
                                    Else
                                        bExamCentrallyManaged = True
                                        chkSummerExam.Enabled = False
                                        chkSummerExam.Checked = False
                                    End If
                                Else
                                    chkSummerExam.Enabled = True
                                End If

                            Else
                                If Convert.ToBoolean(lblIsCore__c.Text) Then
                                    chkSummerExam.Enabled = False
                                Else
                                    If bExamCourseFirstTime Then
                                        chkSummerExam.Enabled = False
                                    Else
                                        chkSummerExam.Enabled = True
                                    End If
                                End If
                            End If
                        End If
                    Else
                        If WhoPaysForElevationStudent(SummerSummerExamProductID, ddlRoute.SelectedValue, Convert.ToInt32(chkSummerExamtxt(0))) Then
                            If CBool(ViewState("CentrallyManaged")) Then
                                ' if FAE Elective
                                If isFAEElective Then
                                    ' chk if FAE having already Registered and passed some elective then  bypass who pays
                                    Dim sSqlFAEElectiveByPassWhoPays As String = Database & "..spCheckFAEEnrolledCount__c @StudentID=" & AptifyEbusinessUser1.PersonID
                                    Dim bByPassWhoPays As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlFAEElectiveByPassWhoPays, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                    If bByPassWhoPays Then
                                        chkSummerExam.Enabled = False
                                        chkSummerExam.Checked = False
                                    Else
                                        bExamCentrallyManaged = True
                                        chkSummerExam.Enabled = False
                                        chkSummerExam.Checked = False
                                    End If
                                Else
                                    bExamCentrallyManaged = True
                                    chkSummerExam.Enabled = False
                                    chkSummerExam.Checked = False
                                End If
                            ElseIf CBool(ViewState("RTO")) Then
                                chkSummerExam.Enabled = True
                            End If
                        Else
                            If Convert.ToBoolean(lblIsCore__c.Text) Then
                                chkSummerExam.Enabled = False
                            Else
                                If bExamCourseFirstTime Then 'redmine #16504
                                    If bExamCentrallyManaged Then
                                        chkSummerExam.Enabled = False
                                    Else
                                        chkSummerExam.Enabled = True
                                    End If
                                Else
                                    chkSummerExam.Enabled = True
                                End If
                            End If
                        End If
                        ' chkSummerExam.Enabled = False
                    End If
                Else
                    chkSummerExam.Enabled = False
                End If


                ' if Check subject is Elective or not 
                Dim IsClassRoomElective As Integer = Convert.ToInt32(chkClassRoomTxt(3))

                If chkClassRoom.Enabled = True AndAlso chkClassRoom.Checked = False Then
                    Dim IsCore As Boolean
                    ''  If IsClassRoomElective = 0 Then

                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkClassRoomTxt(0)) & "' AND ProductID='" & Convert.ToString(chkClassRoomTxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkClassRoom.Checked = True
                        End If
                    Else
                        chkClassRoom.Checked = True
                    End If
                    If Convert.ToBoolean(lblIsCore__c.Text) Then
                        chkClassRoom.Enabled = False
                    Else
                        If bClassRoomCentrallyManaged Then
                            chkClassRoom.Enabled = False
                        Else
                            chkClassRoom.Enabled = True
                        End If
                    End If
                    ''Else
                    ' chkClassRoom.Checked = True
                    '' End If
                Else
                    If IsClassRoomElective = 1 Then
                        If bClassRoomCentrallyManaged Then
                            chkClassRoom.Enabled = False
                        Else
                            '' Code commented by GM for Redmine #20382
                            'If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                            '    Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkClassRoomTxt(0)) & "' AND ProductID='" & Convert.ToString(chkClassRoomTxt(1)) & "'")
                            '    If dr.Length > 0 Then
                            '        chkClassRoom.Checked = True
                            '    End If
                            'Else
                            '    chkClassRoom.Checked = False
                            'End If
                            ' ENd Comment Redmine #20382
                            'Added if else condition for Redmine #20382
                            If Convert.ToBoolean(lblIsCore__c.Text) Then
                                chkClassRoom.Enabled = False
                            Else
                                If bClassRoomCentrallyManaged Then
                                    chkClassRoom.Enabled = False
                                Else
                                    chkClassRoom.Enabled = True
                                End If
                            End If

                            ''chkClassRoom.Enabled = True ' commnted line for Redmine #20382
                            'end redmine #20382 
                        End If

                    End If

                    If chkRAutumnExamTxt(0) > 0 OrElse chkRepeatRevisionTxt(0) > 0 OrElse chkResitInterimAssessmentTxt(0) > 0 Then

                        chkClassRoom.Enabled = True

                    End If
                End If
                Dim IsInterimElective As Integer = Convert.ToInt32(chkInterimAssessmenttxt(3))
                If chkInterimAssessment.Enabled = True AndAlso chkInterimAssessment.Checked = False Then
                    '' If IsInterimElective = 0 Then
                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkInterimAssessmenttxt(0)) & "' AND ProductID='" & Convert.ToString(chkInterimAssessmenttxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkInterimAssessment.Checked = True
                        End If
                    Else
                        chkInterimAssessment.Checked = True
                    End If

                    If Convert.ToBoolean(lblIsCore__c.Text) Then
                        chkInterimAssessment.Enabled = False
                    Else
                        chkInterimAssessment.Enabled = True
                    End If
                    ''Else

                    ''End If
                Else
                    If IsInterimElective = 1 Then
                        If bInterimCentrallyManaged Then
                            chkInterimAssessment.Enabled = False
                        Else

                            chkInterimAssessment.Checked = False
                            chkClassRoom.Enabled = True
                        End If

                    End If
                    If (chkRAutumnExamTxt(0) > 0 AndAlso chkRAutumnExamTxt(4) <> "2") OrElse (chkRepeatRevisionTxt(0) > 0 AndAlso chkRepeatRevisionTxt(4) <> "2") OrElse (chkResitInterimAssessmentTxt(0) > 0 AndAlso chkResitInterimAssessmentTxt(4) <> "2") Then

                        chkInterimAssessment.Enabled = True

                    End If
                End If
                Dim IsMockElective As Integer = Convert.ToInt32(chkMockExamtxt(3))
                If chkMockExam.Enabled = True AndAlso chkMockExam.Checked = False Then
                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkMockExamtxt(0)) & "' AND ProductID='" & Convert.ToString(chkMockExamtxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkMockExam.Checked = True
                        End If
                    Else
                        chkMockExam.Checked = True
                    End If


                    If IsMockElective = 0 Then

                        If Convert.ToBoolean(lblIsCore__c.Text) Then
                            chkMockExam.Enabled = False
                        Else
                            chkMockExam.Enabled = True
                        End If
                    End If
                Else
                    If bMockCentrallyManaged Then
                        chkMockExam.Enabled = False
                        chkMockExam.Checked = False
                    End If
                    ''If IsMockElective = 1 Then
                    ''    If bMockCentrallyManaged Then
                    ''        chkMockExam.Enabled = False
                    ''    Else
                    ''        chkMockExam.Checked = False
                    ''        chkClassRoom.Enabled = True
                    ''    End If
                    ''End If
                    If chkRAutumnExamTxt(0) > 0 OrElse chkRepeatRevisionTxt(0) > 0 OrElse chkResitInterimAssessmentTxt(0) > 0 Then

                        chkMockExam.Enabled = True
                        chkClassRoom.Enabled = True

                    End If
                End If
                Dim IsSummerElective As Integer = Convert.ToInt32(chkSummerExamtxt(3))
                If chkSummerExam.Enabled = True AndAlso chkSummerExam.Checked = False Then
                    ''If IsSummerElective = 0 Then
                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkSummerExamtxt(0)) & "' AND ProductID='" & Convert.ToString(chkSummerExamtxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkSummerExam.Checked = True
                        End If
                    Else
                        chkSummerExam.Checked = True
                    End If

                    If Convert.ToBoolean(lblIsCore__c.Text) Then
                        chkSummerExam.Enabled = False
                    Else
                        chkSummerExam.Enabled = True
                    End If
                    ''End If
                Else
                    If IsSummerElective = 1 Then
                        If bExamCentrallyManaged Then
                            chkSummerExam.Enabled = False
                        Else
                            chkSummerExam.Checked = False
                            If bClassRoomCentrallyManaged Then '  redmine 16504 
                            Else
                                ' added if else condition by GM for Redmine #20382
                                If Convert.ToBoolean(lblIsCore__c.Text) Then
                                    chkClassRoom.Enabled = False
                                Else
                                    chkClassRoom.Enabled = True
                                End If
                                'End Redmine #20382
                            End If
                        End If

                    End If
                    If chkRAutumnExamTxt(0) > 0 OrElse chkRepeatRevisionTxt(0) > 0 OrElse chkResitInterimAssessmentTxt(0) > 0 Then

                        chkSummerExam.Enabled = True


                    End If
                End If

                '2.	Centrally managed Contract or Elevation with RTO  these students cannot perform any function on this page and all options will be greyed out for them.
                'If ddlRoute.SelectedItem.ToString().Trim.ToLower = "elevation" OrElse ddlRoute.SelectedItem.ToString().Trim.ToLower = "contract" Then
                If EducationContract.Trim.ToLower = "contract" AndAlso Convert.ToBoolean(ViewState("CentrallyManaged")) Then
                    ' Check Alrady Enrolled or not
                    If chkRepeatRevision.Enabled = True Then
                        If SummerRepeatRevisionProductID > 0 Then
                        Else
                            chkRepeatRevision.Enabled = False
                        End If
                    End If
                    ' Chk Summer IA Exam Enrolled

                    If chkResitInterimAssessment.Enabled = True Then
                        If SummerResitInterimAssessmentProductID > 0 Then
                            If SummerInterimAssessmentProductID > 0 Then
                                Dim sChkClassRegisSql As String = Database & "..spCheckAlreadyEnrolledStudent__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@ProductID=" & SummerInterimAssessmentProductID & ",@NewClassID=" & chkResitInterimAssessmentTxt(0)
                                Dim bClassRegistration As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sChkClassRegisSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If bClassRegistration Then
                                    'Chk Who Pays
                                    If WhoPaysForElevationStudent(SummerResitInterimAssessmentProductID, ddlRoute.SelectedValue, chkResitInterimAssessmentTxt(0)) = False Then
                                        chkResitInterimAssessment.Enabled = True
                                    Else
                                        chkResitInterimAssessment.Enabled = False

                                    End If
                                Else
                                    chkResitInterimAssessment.Enabled = False
                                End If
                            Else
                                chkResitInterimAssessment.Enabled = False
                            End If
                        Else
                            chkResitInterimAssessment.Enabled = False
                        End If
                    End If
                    If chkAutumnExam.Enabled = True Then
                        If SummerAutumnExamProductID > 0 Then
                            If SummerSummerExamProductID > 0 Then
                                ' chk who pays
                                Dim sChkClassRegisSql As String = Database & "..spCheckAlreadyEnrolledStudent__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@ProductID=" & SummerSummerExamProductID & ",@NewClassID=" & chkRAutumnExamTxt(0)
                                Dim bClassRegistration As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sChkClassRegisSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If bClassRegistration Then
                                    'Chk Who Pays
                                    If WhoPaysForElevationStudent(SummerAutumnExamProductID, ddlRoute.SelectedValue, chkRAutumnExamTxt(0)) = False Then
                                        chkAutumnExam.Enabled = True
                                    Else
                                        chkAutumnExam.Enabled = False

                                    End If
                                Else
                                    chkAutumnExam.Enabled = False
                                End If
                            Else
                                chkAutumnExam.Enabled = False
                            End If
                        Else
                            chkAutumnExam.Enabled = False
                        End If
                    End If


                ElseIf EducationContract.Trim.ToLower = "elevation" AndAlso Convert.ToBoolean(ViewState("RTO")) Then
                    ' Check Alrady Enrolled or not
                    ' Check Alrady Enrolled or not
                    If chkRepeatRevision.Enabled = True Then
                        If SummerRepeatRevisionProductID > 0 Then
                        Else
                            chkRepeatRevision.Enabled = False
                        End If
                    End If
                    ' Chk Summer IA Exam Enrolled
                    If chkResitInterimAssessment.Enabled = True Then
                        If SummerResitInterimAssessmentProductID > 0 Then
                            If SummerInterimAssessmentProductID > 0 Then
                                Dim sChkClassRegisSql As String = Database & "..spCheckAlreadyEnrolledStudent__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@ProductID=" & SummerInterimAssessmentProductID & ",@NewClassID=" & chkResitInterimAssessmentTxt(0)
                                Dim bClassRegistration As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sChkClassRegisSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If bClassRegistration Then
                                    'Chk Who Pays
                                    If WhoPaysForElevationStudent(SummerResitInterimAssessmentProductID, ddlRoute.SelectedValue, chkResitInterimAssessmentTxt(0)) = False Then
                                        chkResitInterimAssessment.Enabled = True
                                    Else
                                        chkResitInterimAssessment.Enabled = False
                                    End If
                                Else
                                    chkResitInterimAssessment.Enabled = False
                                End If
                            Else
                                chkResitInterimAssessment.Enabled = False
                            End If
                        Else
                            chkResitInterimAssessment.Enabled = False
                        End If
                    End If
                    If chkAutumnExam.Enabled = True Then
                        If SummerAutumnExamProductID > 0 Then
                            If SummerSummerExamProductID > 0 Then
                                ' chk who pays
                                Dim sChkClassRegisSql As String = Database & "..spCheckAlreadyEnrolledStudent__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@ProductID=" & SummerSummerExamProductID & ",@NewClassID=" & chkRAutumnExamTxt(0)
                                Dim bClassRegistration As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sChkClassRegisSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If bClassRegistration Then
                                    'Chk Who Pays
                                    If WhoPaysForElevationStudent(SummerAutumnExamProductID, ddlRoute.SelectedValue, chkRAutumnExamTxt(0)) = False Then
                                        chkAutumnExam.Enabled = True
                                    Else
                                        chkAutumnExam.Enabled = False
                                    End If
                                Else
                                    chkAutumnExam.Enabled = False
                                End If
                            Else
                                chkAutumnExam.Enabled = False
                            End If
                        Else
                            chkAutumnExam.Enabled = False
                        End If
                    End If

                End If

                ' chk debk failed
                If chkResitInterimAssessmentTxt(0) > 0 AndAlso chkRepeatRevisionTxt(0) <= 0 AndAlso chkRAutumnExamTxt(0) <= 0 Then
                    ' chkResitInterimAssessment.Checked = True
                    chkResitInterimAssessment.Enabled = True
                End If
                If Convert.ToInt32(chkRevisiontxt(1)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(1)) > 0 AndAlso chkClassRoom.Checked = False AndAlso chkRevision.Checked = False Then

                    chkInterimAssessment.Enabled = True

                    chkSummerExam.Enabled = True

                Else
                    If chkClassRoom.Checked Then
                        chkInterimAssessment.Enabled = False
                        chkSummerExam.Enabled = False
                        chkInterimAssessment.Checked = True
                        chkSummerExam.Checked = True
                        If chkMockExamtxt(0) > 0 Then
                            If bMockCentrallyManaged Then '  redmine 16504 
                                chkMockExam.Checked = False
                            Else
                                chkMockExam.Checked = True
                            End If
                            ''Commented By Pradip 2016-02-03 For Issue 5371
                            '''' 'chkMockExam.Enabled = False
                        End If
                    End If

                End If
                ' Chk autumn registered or not
                If chkRAutumnExamTxt(4) = "2" Then
                    chkClassRoom.Checked = False

                    chkInterimAssessment.Checked = False
                    chkMockExam.Checked = False
                    chkSummerExam.Checked = False
                    chkRevision.Checked = False
                    chkClassRoom.Enabled = False
                    chkInterimAssessment.Enabled = False
                    chkMockExam.Enabled = False
                    chkSummerExam.Enabled = False
                    chkRevision.Enabled = False

                    Dim divClass As HtmlGenericControl = TryCast(item.FindControl("idivClassRoom"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    divClass.Attributes("class") = "not_available"
                    chkClassRoom.Visible = False
                    Dim idivMock As HtmlGenericControl = TryCast(item.FindControl("idivMock"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivMock.Attributes("class") = "not_available"
                    chkMockExam.Visible = False
                    Dim idivIA As HtmlGenericControl = TryCast(item.FindControl("idivIA"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivIA.Attributes("class") = "not_available"
                    chkInterimAssessment.Visible = False
                    Dim idivSummerExam As HtmlGenericControl = TryCast(item.FindControl("idivSummerExam"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivSummerExam.Attributes("class") = "not_available"
                    chkSummerExam.Visible = False
                    Dim idivSummerRevision As HtmlGenericControl = TryCast(item.FindControl("idivSummerRevision"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivSummerRevision.Attributes("class") = "not_available"
                    chkRevision.Visible = False
                End If

                ' Chk if revision course registered and if yes disable/uncheck the classroom
                If chkRevisiontxt(4) = "2" Then
                    chkMockExam.Checked = False
                    chkClassRoom.Checked = False
                    chkMockExam.Enabled = False
                    chkClassRoom.Enabled = False
                    'chkRevision.Enabled = False
                End If
                ' Redmine Issue 15540
                If ViewState("IsAlreadyFAEtaken") Then
                    If chkRepeatRevisionTxt(4) = "2" OrElse chkResitInterimAssessmentTxt(4) = "2" OrElse chkRAutumnExamTxt(4) = "2" Then
                        Dim divClass As HtmlGenericControl = TryCast(item.FindControl("idivClassRoom"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        divClass.Attributes("class") = "not_available"
                        chkClassRoom.Visible = False
                        Dim idivMock As HtmlGenericControl = TryCast(item.FindControl("idivMock"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivMock.Attributes("class") = "not_available"
                        chkMockExam.Visible = False
                        Dim idivIA As HtmlGenericControl = TryCast(item.FindControl("idivIA"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivIA.Attributes("class") = "not_available"
                        chkInterimAssessment.Visible = False
                        Dim idivSummerExam As HtmlGenericControl = TryCast(item.FindControl("idivSummerExam"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivSummerExam.Attributes("class") = "not_available"
                        chkSummerExam.Visible = False
                        Dim idivSummerRevision As HtmlGenericControl = TryCast(item.FindControl("idivSummerRevision"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivSummerRevision.Attributes("class") = "not_available"
                        chkRevision.Visible = False
                    ElseIf chkClassRoomTxt(4) = "2" OrElse chkRevisiontxt(4) = "2" OrElse chkRepeatRevisionTxt(4) = "2" OrElse chkResitInterimAssessmentTxt(4) = "2" OrElse chkRAutumnExamTxt(4) = "2" OrElse chkInterimAssessmenttxt(4) = "2" OrElse chkMockExamtxt(4) = "2" OrElse chkSummerExamtxt(4) = "2" Then
                        Dim idivAutumnRevision As HtmlGenericControl = TryCast(item.FindControl("idivAutumnRevision"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivAutumnRevision.Attributes("class") = "not_available"
                        chkRepeatRevision.Visible = False
                        Dim idivRIA As HtmlGenericControl = TryCast(item.FindControl("idivRIA"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivRIA.Attributes("class") = "not_available"
                        chkResitInterimAssessment.Visible = False
                        Dim idivAutumnExam As HtmlGenericControl = TryCast(item.FindControl("idivAutumnExam"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivAutumnExam.Attributes("class") = "not_available"
                        chkAutumnExam.Visible = False
                    End If
                End If

                If chkRevision.Enabled Then
                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkRevisiontxt(0)) & "' AND ProductID='" & Convert.ToString(chkRevisiontxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkRevision.Checked = True
                            chkInterimAssessment.Checked = True
                            chkInterimAssessment.Enabled = False
                            chkSummerExam.Checked = True
                            chkSummerExam.Enabled = False
                        End If
                    End If
                End If

                'aalo

                If chkRepeatRevision.Checked = False AndAlso Convert.ToInt32(chkRepeatRevisionTxt(1)) > 0 Then
                    If bCentrallManageResitAssessment Then
                    Else
                        chkResitInterimAssessment.Enabled = True
                    End If
                    If bCentrallManageResitExam Then
                    Else
                        chkAutumnExam.Enabled = True
                    End If
                End If


                If sAutoEnrolledSelection.Trim.ToLower = "autumn" Then

                    If chkAutumnExam.Enabled Then
                        If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                            Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkRAutumnExamTxt(0)) & "' AND ProductID='" & Convert.ToString(chkRAutumnExamTxt(1)) & "'")
                            If dr.Length > 0 Then
                                chkAutumnExam.Checked = True
                            End If
                        Else
                            If chkRAutumnExamTxt(4) > 0 Then
                                If chkSummerExam.Checked = False Then
                                    If isFAEElective Then
                                    Else
                                        chkAutumnExam.Checked = True
                                    End If

                                ElseIf chkRAutumnExamTxt(4) = "22" OrElse chkRAutumnExamTxt(4) = "44" Then
                                    chkSummerExam.Checked = False
                                    If isFAEElective Then
                                    Else
                                        chkAutumnExam.Checked = True
                                        'Added as part of Task #18786
                                        chkMockExam.Checked = False
                                        If chkResitInterimAssessment.Enabled = False Then
                                            chkClassRoom.Checked = False
                                        End If
                                        If chkResitInterimAssessment.Enabled = False Then
                                            chkInterimAssessment.Checked = False
                                        End If
                                    End If

                                    ''Added BY Pradip 2016-03-22
                                    If chkClassRoomTxt(4) = "2" OrElse chkInterimAssessmenttxt(4) = "2" OrElse chkMockExamtxt(4) = "2" OrElse chkSummerExamtxt(4) = "2" Then
                                        chkAutumnExam.Checked = False
                                    End If
                                End If
                            End If
                            chkSummerExam.Checked = False
                        End If

                    End If
                    If chkRepeatRevision.Enabled Then

                        If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                            Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkRepeatRevisionTxt(0)) & "' AND ProductID='" & Convert.ToString(chkRepeatRevisionTxt(1)) & "'")
                            If dr.Length > 0 Then
                                chkRepeatRevision.Checked = True
                                chkClassRoom.Checked = False
                                chkMockExam.Checked = False
                            End If
                        Else
                            If chkRepeatRevisionTxt(4) > 0 AndAlso (chkClassRoom.Checked = False OrElse chkRevision.Checked = False) Then
                                If isFAEElective Then
                                Else
                                    chkRepeatRevision.Checked = True
                                    ' Redmine #15690
                                    If chkRAutumnExamTxt(4) > 0 Then
                                        If chkSummerExam.Checked = False Then
                                            If isFAEElective Then
                                            Else
                                                chkAutumnExam.Checked = True
                                            End If

                                        ElseIf chkRAutumnExamTxt(4) = "22" OrElse chkRAutumnExamTxt(4) = "44" Then
                                            chkSummerExam.Checked = False
                                            If isFAEElective Then
                                            Else
                                                chkAutumnExam.Checked = True
                                                'Added as part of Task #18786
                                                chkMockExam.Checked = False
                                                If chkResitInterimAssessment.Enabled = False Then
                                                    chkClassRoom.Checked = False
                                                End If
                                                If chkResitInterimAssessment.Enabled = False Then
                                                    chkInterimAssessment.Checked = False
                                                End If
                                            End If

                                            ''Added BY Pradip 2016-03-22
                                            If chkClassRoomTxt(4) = "2" OrElse chkInterimAssessmenttxt(4) = "2" OrElse chkMockExamtxt(4) = "2" OrElse chkSummerExamtxt(4) = "2" Then
                                                chkAutumnExam.Checked = False
                                            End If
                                        End If
                                    End If
                                    chkSummerExam.Checked = False
                                End If

                            End If

                            chkClassRoom.Checked = False
                            chkMockExam.Checked = False
                        End If


                    End If
                    If chkResitInterimAssessment.Enabled Then
                        If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                            Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkResitInterimAssessmentTxt(0)) & "' AND ProductID='" & Convert.ToString(chkResitInterimAssessmentTxt(1)) & "'")
                            If dr.Length > 0 Then
                                chkResitInterimAssessment.Checked = True
                                chkAutumnExam.Checked = True
                                chkAutumnExam.Enabled = False
                            End If
                        Else
                            If chkResitInterimAssessmentTxt(4) > 0 Then
                                If isFAEElective Then
                                Else
                                    chkResitInterimAssessment.Checked = True
                                End If
                            End If


                            chkInterimAssessment.Checked = False

                        End If
                    ElseIf chkRepeatRevision.Checked = True And chkAutumnExam.Checked Then

                        If chkResitInterimAssessmentTxt(4) > 0 Then
                            If isFAEElective Then
                            Else
                                chkResitInterimAssessment.Checked = True
                            End If
                        End If


                        chkInterimAssessment.Checked = False
                    End If

                    If chkRepeatRevision.Checked Then
                        ''Commented BY Pradip 2016-03-30 For Issue No 5524
                        'chkResitInterimAssessment.Enabled = False
                        chkAutumnExam.Enabled = False
                    End If

                    ' if fae pre selected then auto tick elective check box
                    If isFAEElective Then
                        Dim bfirstItem As Boolean = False
                        If chkRepeatRevision.Checked = True OrElse chkResitInterimAssessment.Checked = True OrElse chkAutumnExam.Checked = True Then
                            chkIsFAEElective.Checked = True
                            ViewState("IsFAEElectiveChecked") = True
                            bfirstItem = True
                        Else
                            bfirstItem = False
                        End If
                        If ViewState("IsFAEElectiveChecked") = True AndAlso bfirstItem = False Then
                            ''Uncomment By Pradip 2016-03-22
                            chkIsFAEElective.Enabled = False
                        End If
                    End If
                End If
                ' Chk summer exam registered or not 
                If chkSummerExamtxt(4) = "2" Then
                    ViewState("SummerTaken") = True
                    chkAutumnExam.Checked = False
                    chkRepeatRevision.Checked = False
                    chkResitInterimAssessment.Checked = False
                    chkAutumnExam.Enabled = False
                    chkRepeatRevision.Enabled = False
                    chkResitInterimAssessment.Enabled = False

                    Dim idivAutumnRevision As HtmlGenericControl = TryCast(item.FindControl("idivAutumnRevision"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivAutumnRevision.Attributes("class") = "not_available"
                    chkRepeatRevision.Visible = False
                    Dim idivRIA As HtmlGenericControl = TryCast(item.FindControl("idivRIA"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivRIA.Attributes("class") = "not_available"
                    chkResitInterimAssessment.Visible = False
                    Dim idivAutumnExam As HtmlGenericControl = TryCast(item.FindControl("idivAutumnExam"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivAutumnExam.Attributes("class") = "not_available"
                    chkAutumnExam.Visible = False
                End If
                'Redmine #16463
                'If CBool(ViewState("SummerTaken")) = True Then
                '    chkAutumnExam.Checked = False
                '    chkRepeatRevision.Checked = False
                '    chkResitInterimAssessment.Checked = False
                '    chkAutumnExam.Enabled = False
                '    chkRepeatRevision.Enabled = False
                '    chkResitInterimAssessment.Enabled = False

                '    Dim idivAutumnRevision As HtmlGenericControl = TryCast(item.FindControl("idivAutumnRevision"), HtmlGenericControl)
                '    'divClass.Attributes.Add("background-color", "RED")
                '    idivAutumnRevision.Attributes("class") = "not_available"
                '    chkRepeatRevision.Visible = False
                '    Dim idivRIA As HtmlGenericControl = TryCast(item.FindControl("idivRIA"), HtmlGenericControl)
                '    'divClass.Attributes.Add("background-color", "RED")
                '    idivRIA.Attributes("class") = "not_available"
                '    chkResitInterimAssessment.Visible = False
                '    Dim idivAutumnExam As HtmlGenericControl = TryCast(item.FindControl("idivAutumnExam"), HtmlGenericControl)
                '    'divClass.Attributes.Add("background-color", "RED")
                '    idivAutumnExam.Attributes("class") = "not_available"
                '    chkAutumnExam.Visible = False
                'End If
                If Convert.ToInt32(chkRevisiontxt(1)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(1)) > 0 AndAlso chkClassRoom.Checked = False AndAlso chkRevision.Checked = False Then
                    If (chkRAutumnExamTxt(0) > 0 AndAlso chkRAutumnExamTxt(4) = "2") OrElse (chkRepeatRevisionTxt(0) > 0 AndAlso chkRepeatRevisionTxt(4) = "2") OrElse (chkResitInterimAssessmentTxt(0) > 0 AndAlso chkResitInterimAssessmentTxt(4) = "2") Then
                    Else
                        chkInterimAssessment.Enabled = True

                        chkSummerExam.Enabled = True
                    End If

                End If
                'Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
                If chkClassRoom.Checked = True AndAlso Convert.ToInt32(lblIsFAEElective.Text) = 0 Then

                ElseIf Convert.ToInt32(lblIsFAEElective.Text) = 1 Then
                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkClassRoomTxt(0)) & "' AND ProductID='" & Convert.ToString(chkClassRoomTxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkClassRoom.Checked = True
                        End If
                    Else
                        chkClassRoom.Checked = False
                    End If
                    'chkClassRoom.Checked = False
                End If

                ' Add Mock
                If chkMockExam.Checked = True AndAlso Convert.ToInt32(lblIsFAEElective.Text) = 0 Then

                ElseIf Convert.ToInt32(lblIsFAEElective.Text) = 1 Then
                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkMockExamtxt(0)) & "' AND ProductID='" & Convert.ToString(chkMockExamtxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkMockExam.Checked = True
                        End If
                    Else
                        chkMockExam.Checked = False
                    End If
                    ' chkMockExam.Checked = False
                End If

                ''Commented By Pradip 2016-02-03 For Issue 5371
                'If chkRepeatRevisionTxt(0) > 0 OrElse chkResitInterimAssessmentTxt(0) > 0 OrElse chkRAutumnExamTxt(0) > 0 Then
                '    chkMockExam.Enabled = False
                'End If

                If chkInterimAssessment.Checked AndAlso Convert.ToInt32(lblIsFAEElective.Text) = 0 Then

                ElseIf Convert.ToInt32(lblIsFAEElective.Text) = 1 Then
                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkInterimAssessmenttxt(0)) & "' AND ProductID='" & Convert.ToString(chkInterimAssessmenttxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkInterimAssessment.Checked = True
                        End If
                    Else
                        chkInterimAssessment.Checked = False
                    End If
                    'chkInterimAssessment.Checked = False
                End If

                If chkSummerExam.Checked AndAlso Convert.ToInt32(lblIsFAEElective.Text) = 0 Then

                ElseIf Convert.ToInt32(lblIsFAEElective.Text) = 1 Then
                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkSummerExamtxt(0)) & "' AND ProductID='" & Convert.ToString(chkSummerExamtxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkSummerExam.Checked = True
                        End If
                    Else
                        chkSummerExam.Checked = False
                    End If
                    'chkSummerExam.Checked = False
                End If
                If chkClassRoom.Checked AndAlso chkSummerExam.Checked Then
                    chkInterimAssessment.Enabled = False
                    chkMockExam.Enabled = False
                    chkSummerExam.Enabled = False
                End If
                If chkSummerExam.Enabled Then
                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkSummerExamtxt(0)) & "' AND ProductID='" & Convert.ToString(chkSummerExamtxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkSummerExam.Checked = True
                        End If
                    End If
                End If
                If chkInterimAssessment.Enabled Then
                    If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(chkInterimAssessmenttxt(0)) & "' AND ProductID='" & Convert.ToString(chkInterimAssessmenttxt(1)) & "'")
                        If dr.Length > 0 Then
                            chkInterimAssessment.Checked = True
                            chkSummerExam.Checked = True
                            chkSummerExam.Enabled = False
                        End If
                    End If
                End If
                ' Redmine Issue #15340
                If (chkClassRoomTxt(0) > 0 AndAlso chkClassRoomTxt(4) = "2" AndAlso chkSummerExamtxt(0) > 0 AndAlso chkSummerExamtxt("4") = "2") OrElse (chkRepeatRevisionTxt(0) AndAlso chkRepeatRevisionTxt(4) = "2" AndAlso chkRAutumnExamTxt(0) AndAlso chkRAutumnExamTxt(4) = "2") Then
                    If ViewState("EnrollBtn") Is Nothing AndAlso Convert.ToString(ViewState("EnrollBtn")) <> "yes" Then ' condition for redmine log 15710
                        btnDisplayPaymentSummey.Visible = False
                        lblEnrolmsg.Visible = False
                    End If
                ElseIf chkRepeatRevisionTxt(0) AndAlso chkRepeatRevisionTxt(4) = "2" Then
                    If ViewState("EnrollBtn") Is Nothing AndAlso Convert.ToString(ViewState("EnrollBtn")) <> "yes" Then ' condition for redmine log 15710
                        btnDisplayPaymentSummey.Visible = False
                        lblEnrolmsg.Visible = False
                    End If
                End If
                'if condition updated #20301
                If (chkClassRoom.Enabled AndAlso chkClassRoomTxt(0) > 0 AndAlso (chkClassRoomTxt(4) = "22" Or chkClassRoomTxt("4") = "44" Or chkClassRoomTxt("4") = "66") AndAlso chkSummerExamtxt(0) > 0 AndAlso (chkSummerExamtxt("4") = "22" Or chkSummerExamtxt("4") = "44" Or chkSummerExamtxt("4") = "66")) OrElse (chkRepeatRevision.Enabled AndAlso chkRepeatRevisionTxt(0) AndAlso (chkRepeatRevisionTxt(4) = "22" Or chkRepeatRevisionTxt(4) = "44" Or chkRepeatRevisionTxt(4) = "66") AndAlso chkRAutumnExamTxt(0) AndAlso (chkRAutumnExamTxt(4) = "22" Or chkRAutumnExamTxt(4) = "44" Or chkRAutumnExamTxt(4) = "66")) Then
                    If CBool(ViewState("IsFAEElective")) = True Then ' this condition added for https://redmine.softwaredesign.ie/issues/16489
                    Else
                        btnDisplayPaymentSummey.Visible = True
                        ViewState("EnrollBtn") = "yes"
                    End If
                End If
                'Redmine #16463
                If (chkClassRoomTxt(0) > 0 AndAlso chkClassRoomTxt(4) = "2" AndAlso chkSummerExamtxt(0) > 0 AndAlso chkSummerExamtxt("4") = "2") AndAlso (chkRepeatRevisionTxt(0) AndAlso chkRepeatRevisionTxt(4) = "22" AndAlso chkRAutumnExamTxt(0) AndAlso chkRAutumnExamTxt(4) = "22") Then
                    If ViewState("EnrollBtn") Is Nothing AndAlso Convert.ToString(ViewState("EnrollBtn")) <> "yes" Then ' condition for redmine log 15710
                        btnDisplayPaymentSummey.Visible = False
                        lblEnrolmsg.Visible = False
                    End If
                End If
                'Redmine #16029
                If chkResitInterimAssessment.Enabled Then
                    Dim sSQLDEBKNotPass As String = Database & "..spCheckDEBKNotPass__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@CourseID=" & CourseID
                    Dim bDEBKPass As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSQLDEBKNotPass, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If bDEBKPass = False Then
                        chkResitInterimAssessment.Enabled = False
                    End If
                End If
                ' Redmine issue #15351
                If CBool(ViewState("CentrallyManaged")) Then
                    If chkClassRoom.Enabled = False AndAlso chkSummerExam.Checked = False AndAlso chkRepeatRevision.Enabled = False AndAlso chkAutumnExam.Enabled = False Then

                    Else
                        ViewState("IsCentrallyManagedContractAutum") = "Yes"
                        If chkRAutumnExamTxt(0) > 0 AndAlso (chkRAutumnExamTxt(4) = "2") Then
                            ViewState("IsCentrallyManagedContractAutum_Enrol") = "No"
                        ElseIf chkRAutumnExamTxt(0) > 0 Then
                            ViewState("IsCentrallyManagedContractAutum_Enrol") = "Yes"
                        Else
                            ViewState("IsCentrallyManagedContractAutum_Enrol") = "No"
                        End If
                        If chkRepeatRevisionTxt(0) > 0 AndAlso (chkRepeatRevisionTxt(4) = "2") Then
                            ViewState("IsCentrallyManagedContractAutum_Enrol") = "No"
                        ElseIf chkRepeatRevisionTxt(0) > 0 Then
                            ViewState("IsCentrallyManagedContractAutum_Enrol") = "Yes"
                        Else
                            ViewState("IsCentrallyManagedContractAutum_Enrol") = "No"
                        End If
                        'If chkRepeatRevisionTxt(0) AndAlso (chkRepeatRevisionTxt(4) = "2") OrElse chkRAutumnExamTxt(0) AndAlso (chkRAutumnExamTxt(4) = "2") Then
                        'ViewState("IsCentrallyManagedContractAutum_Enrol") = "No"
                        'End If
                    End If
                End If

                'Redmine Issue #13743
                Dim lblIsExternal As Label = DirectCast(item.FindControl("lblIsExternal"), Label)
                If lblIsExternal.Text.Trim = "1" Then
                    Dim divClass As HtmlGenericControl = TryCast(item.FindControl("idivClassRoom"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    divClass.Attributes("class") = "not_available"
                    chkClassRoom.Visible = False
                    Dim idivMock As HtmlGenericControl = TryCast(item.FindControl("idivMock"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivMock.Attributes("class") = "not_available"
                    chkMockExam.Visible = False
                    Dim idivIA As HtmlGenericControl = TryCast(item.FindControl("idivIA"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivIA.Attributes("class") = "not_available"
                    chkInterimAssessment.Visible = False
                    Dim idivSummerExam As HtmlGenericControl = TryCast(item.FindControl("idivSummerExam"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivSummerExam.Attributes("class") = "not_available"
                    chkSummerExam.Visible = False
                    Dim idivSummerRevision As HtmlGenericControl = TryCast(item.FindControl("idivSummerRevision"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivSummerRevision.Attributes("class") = "not_available"
                    chkRevision.Visible = False
                    Dim idivAutumnRevision As HtmlGenericControl = TryCast(item.FindControl("idivAutumnRevision"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivAutumnRevision.Attributes("class") = "not_available"
                    chkRepeatRevision.Visible = False
                    Dim idivRIA As HtmlGenericControl = TryCast(item.FindControl("idivRIA"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivRIA.Attributes("class") = "not_available"
                    chkResitInterimAssessment.Visible = False
                    Dim idivAutumnExam As HtmlGenericControl = TryCast(item.FindControl("idivAutumnExam"), HtmlGenericControl)
                    'divClass.Attributes.Add("background-color", "RED")
                    idivAutumnExam.Attributes("class") = "not_available"
                    chkAutumnExam.Visible = False
                    btnDisplayPaymentSummey.Visible = False
                    lblEnrolmsg.Visible = False
                    ViewState("IsExternalUser") = 1
                End If
                'Redmine #15540
                If ViewState("IsFAEElective") = True Then
                    If chkClassRoomTxt(4) = "44" OrElse chkRevisiontxt(4) = "44" OrElse chkInterimAssessmenttxt(4) = "44" OrElse chkMockExamtxt(4) = "44" OrElse chkSummerExamtxt(4) = "44" Then
                        Dim divClass As HtmlGenericControl = TryCast(item.FindControl("idivClassRoom"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        divClass.Attributes("class") = "not_available"
                        chkClassRoom.Visible = False
                        Dim idivMock As HtmlGenericControl = TryCast(item.FindControl("idivMock"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivMock.Attributes("class") = "not_available"
                        chkMockExam.Visible = False
                        Dim idivIA As HtmlGenericControl = TryCast(item.FindControl("idivIA"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivIA.Attributes("class") = "not_available"
                        chkInterimAssessment.Visible = False
                        Dim idivSummerExam As HtmlGenericControl = TryCast(item.FindControl("idivSummerExam"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivSummerExam.Attributes("class") = "not_available"
                        chkSummerExam.Visible = False
                        Dim idivSummerRevision As HtmlGenericControl = TryCast(item.FindControl("idivSummerRevision"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivSummerRevision.Attributes("class") = "not_available"
                        chkRevision.Visible = False
                    ElseIf chkRepeatRevisionTxt(4) = "44" OrElse chkResitInterimAssessmentTxt(4) = "44" OrElse chkRAutumnExamTxt(4) = "44" Then
                        Dim idivAutumnRevision As HtmlGenericControl = TryCast(item.FindControl("idivAutumnRevision"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivAutumnRevision.Attributes("class") = "not_available"
                        chkRepeatRevision.Visible = False
                        Dim idivRIA As HtmlGenericControl = TryCast(item.FindControl("idivRIA"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivRIA.Attributes("class") = "not_available"
                        chkResitInterimAssessment.Visible = False
                        Dim idivAutumnExam As HtmlGenericControl = TryCast(item.FindControl("idivAutumnExam"), HtmlGenericControl)
                        'divClass.Attributes.Add("background-color", "RED")
                        idivAutumnExam.Attributes("class") = "not_available"
                        chkAutumnExam.Visible = False
                    End If
                End If

            End If
            gvCurriculumCourse.MasterTableView.ColumnGroups.FindGroupByName("Current").HeaderText = "Autumn " + ViewState("CurrentAcademicCycle")
            gvCurriculumCourse.MasterTableView.ColumnGroups.FindGroupByName("Next").HeaderText = "Summer " + ViewState("NextAcademicCycle")
            If CBool(ViewState("CentrallyManaged")) Then
                If ViewState("IsCentrallyManagedContractAutum") = "Yes" Then
                    If ViewState("IsCentrallyManagedContractAutum_Enrol") = "No" Then
                        If ViewState("EnrollBtn") Is Nothing AndAlso Convert.ToString(ViewState("EnrollBtn")) <> "yes" Then '  condition for #15710
                            btnDisplayPaymentSummey.Visible = False
                            lblEnrolmsg.Visible = False

                            If EducationContract.Trim.ToLower = "contract" Then
                                lblCentrallyMngError.Visible = True
                                gvCurriculumCourse.Visible = False
                                ddlCAP1StudGrp.Enabled = False
                                lblMsgWarning.Visible = False
                                lblEnrolmsg.Visible = False
                            Else
                                gvCurriculumCourse.Visible = True
                                lblEnrolmsg.Visible = True
                                lblMsgWarning.Visible = True
                            End If
                        Else ' else part added for redmine #18714
                            gvCurriculumCourse.Visible = True
                            lblEnrolmsg.Visible = True
                            lblMsgWarning.Visible = True
                        End If
                    Else
                        btnDisplayPaymentSummey.Visible = True
                        If _studentEnrollmentDetails IsNot Nothing And _studentEnrollmentDetails.Rows.Count > 0 Then
                            lblCentrallyMngError.Visible = False
                            gvCurriculumCourse.Visible = True
                            lblEnrolmsg.Visible = True
                        End If
                    End If
                    If gvCurriculumCourse.Visible Then
                        gvCurriculumCourse.Visible = True
                        lblEnrolmsg.Visible = True
                    End If
                Else
                    btnDisplayPaymentSummey.Visible = False
                    lblEnrolmsg.Visible = False
                    lblCentrallyMngError.Visible = True
                    If EducationContract.Trim.ToLower = "contract" Then
                        gvCurriculumCourse.Visible = False
                        ddlCAP1StudGrp.Enabled = False
                        lblMsgWarning.Visible = False
                        lblEnrolmsg.Visible = False
                    Else
                        gvCurriculumCourse.Visible = True
                        lblEnrolmsg.Visible = True
                        lblMsgWarning.Visible = True
                    End If
                End If
            Else
                gvCurriculumCourse.Visible = True
                lblEnrolmsg.Visible = True
                lblCentrallyMngError.Visible = False
                lblMsgWarning.Visible = True
                If btnDisplayPaymentSummey.Visible = False Then
                Else
                    btnDisplayPaymentSummey.Visible = True
                End If

            End If
            If Convert.ToInt32(ViewState("IsExternalUser")) = 1 Then
                btnDisplayPaymentSummey.Visible = False
                lblEnrolmsg.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Function WhoPaysForElevationStudent(ByVal ProductID As Integer, ByVal RouteOfEntryID As Integer, ByVal NewClassID As Integer) As Boolean
        Try
            'Dim sEducationContactCompanySQl As String = "..spGetCompanyInEducaionContract__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@RouteOfEntryID=" & RouteOfEntryID
            ''Change by pradip 2016-02-12 for Tracker G3-48 and Issue no 5417
            Dim sEducationContactCompanySQl As String = "..spGetCompanyInEEApplication__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@RouteOfEntryID=" & RouteOfEntryID
            Dim iCompanyID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sEducationContactCompanySQl, IAptifyDataAction.DSLCacheSetting.BypassCache))
            Dim oProductGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Products", ProductID)
            ' First Find this product who pay in persons who pay sub entity and if not found in person then check in Company who pay entity records
            'Dim sSqlPersonWhoPay As String = "..spGetPersonWhoPay__c @PersonID=" & BillToID & ",@OrderlineProduct=" & ProductID & ",@ProductCategoryID=" & Convert.ToInt32(oProductGE.GetValue("CategoryID")) & ",@OrderDate='" & Me.OrderDate & "'"
            Dim sSqlPersonWhoPay As String = "..spGetPersonWhoPayForElevationStudent__c @PersonID=" & Me.AptifyEbusinessUser1.PersonID & ",@OrderlineProduct=" & ProductID & ",@ProductCategoryID=" & Convert.ToInt32(oProductGE.GetValue("CategoryID")) & ",@OrderDate='" & New DateTime(Year(Today), Month(Today), Day(Today)) & "',@RouteOfEntryID=" & RouteOfEntryID & ",@NewClassID=" & NewClassID

            Dim dtWhopayonPerson As DataTable = DataAction.GetDataTable(sSqlPersonWhoPay, IAptifyDataAction.DSLCacheSetting.BypassCache)

            Dim WhoPay As String = Convert.ToString(dtWhopayonPerson.Rows(0)("WhoPay"))
            If Not String.IsNullOrEmpty(WhoPay) AndAlso WhoPay <> "" Then
                If WhoPay.Trim.ToLower = "firm pays" Then
                    Return True
                ElseIf WhoPay.Trim = "" Then
                    Dim sSqlCompanyParent As String = Database & "..spCheckCompanyRTOOrCentrallyManaged__c @CompanyID=" & iCompanyID
                    Dim dtCompanyParent As DataTable = DataAction.GetDataTable(sSqlCompanyParent, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dtCompanyParent Is Nothing AndAlso dtCompanyParent.Rows.Count > 0 Then
                        If dtCompanyParent.Rows(0)("ParentCompanyID") > 0 AndAlso CBool(dtCompanyParent.Rows(0)("CentrallyManaged__c")) Then
                            iCompanyID = dtCompanyParent.Rows(0)("ParentCompanyID")
                        End If
                    End If
                    Dim ssqlCompanyWhoPay As String = "..spGetCompanyWhoPayForElevationStudent__c @CompanyID=" & iCompanyID & ",@OrderlineProduct=" & ProductID & ",@ProductCategoryID=" & Convert.ToInt32(oProductGE.GetValue("CategoryID")) & ",@OrderDate='" & New DateTime(Year(Today), Month(Today), Day(Today)) & "',@PersonID=" & AptifyEbusinessUser1.PersonID & ",@RouteOfEntryID=" & RouteOfEntryID & ",@NewClassID=" & NewClassID
                    Dim CompayWhoPayID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(ssqlCompanyWhoPay, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If CompayWhoPayID > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Dim sSqlCompanyParent As String = Database & "..spCheckCompanyRTOOrCentrallyManaged__c @CompanyID=" & iCompanyID
                Dim dtCompanyParent As DataTable = DataAction.GetDataTable(sSqlCompanyParent, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtCompanyParent Is Nothing AndAlso dtCompanyParent.Rows.Count > 0 Then
                    If dtCompanyParent.Rows(0)("ParentCompanyID") > 0 AndAlso CBool(dtCompanyParent.Rows(0)("CentrallyManaged__c")) Then
                        iCompanyID = dtCompanyParent.Rows(0)("ParentCompanyID")
                    End If
                End If
                Dim ssqlCompanyWhoPay As String = "..spGetCompanyWhoPayForElevationStudent__c @CompanyID=" & iCompanyID & ",@OrderlineProduct=" & ProductID & ",@ProductCategoryID=" & Convert.ToInt32(oProductGE.GetValue("CategoryID")) & ",@OrderDate='" & New DateTime(Year(Today), Month(Today), Day(Today)) & "',@PersonID=" & AptifyEbusinessUser1.PersonID & ",@RouteOfEntryID=" & RouteOfEntryID & ",@NewClassID=" & NewClassID
                Dim CompayWhoPayID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(ssqlCompanyWhoPay, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If CompayWhoPayID > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function

    Private Function CheckFirmCentrallyManagedAndWhoPays(ByVal ProductID As Integer) As Boolean
        Try
            Dim ssqlCompanyWhoPay As String = "..spGetCompanyWhoPayEnrollmentPage__c @CompanyID=" & AptifyApplication.GetEntityRecordIDFromRecordName("Companies", txtFirm.Text.Trim) & ",@OrderlineProduct=" & ProductID & ",@PersonID=" & AptifyEbusinessUser1.PersonID
            Dim CompayWhoPayID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(ssqlCompanyWhoPay, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If CompayWhoPayID > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function

    Private Function AccessOnCurriculum(ByVal Curriculum As String) As Boolean
        Try
            Dim sSql As String
            sSql = Database & " ..spCheckCurriculumAccess__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@Curriculum='" & Curriculum & "'"
            Dim iAccessID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If iAccessID > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Handles curriculum courses grid - need data sourse event to bind student course enrollment data
    ''' </summary>
    ''' 
    Protected Sub gvCurriculumCourse_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCurriculumCourse.NeedDataSource

        If ddlCAP1StudGrp.SelectedValue.Trim <> "Select Student Group" Then
            LoadStudentCourseEnrollmentDataByStudent()
            ' lblStudentGrpMessage.Visible = False
            lblMsgWarning.Visible = True
        Else
            'lblStudentGrpMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.StudentGrp")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            'lblStudentGrpMessage.Visible = True
            lblMsgWarning.Visible = False
        End If

        If _studentEnrollmentDetails IsNot Nothing And _studentEnrollmentDetails.Rows.Count > 0 Then
            Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
            gvCurriculumCourse.MasterTableView.GetColumn("IsFAEElective").Display = False
            ''Added BY Pradip 2016-05-13 For Redmine Issue To Hide FAE Elective Column
            Dim drFae() As DataRow = _studentEnrollmentDetails.Select("Curriculum='FAE- Final Admitting Exam'")
            If Not drFae Is Nothing AndAlso drFae.Length > 0 Then
                gvCurriculumCourse.MasterTableView.GetColumn("IsFAEElective").Display = True
            End If

            Dim dr() As DataRow = _studentEnrollmentDetails.Select("RuleName='AC1' OR RuleName='AC2'")
            If Not dr Is Nothing AndAlso dr.Length > 0 Then
                gvCurriculumCourse.Visible = False
                lblEnrolmsg.Visible = False
                txtIntialAmount.Visible = False
                lblIntialAmt.Visible = False
                lblCurrency.Visible = False
                txtIntialAmount.Visible = False
                ddlPaymentPlan.Visible = False
                ddlCAP1StudGrp.Visible = False
                lblStudentGroup.Visible = False
                lblStdgrp.Visible = False
                CreditCard.Visible = False
                lblMsgWarning.Visible = True
                'lblCurrentAcademicCycle.Visible = False
                'lblCurrentAcademicCycleName.Visible = False
                'lblNextAcademicCycle.Visible = False
                ' lblNextAcademicCycleName.Visible = False
                If Convert.ToString(dr(0)("RuleName")).Trim.ToLower = "ac1" Then
                    lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.AC1Msg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblMsgWarning.Visible = False
                    lblEnrollmentMsg.Visible = True
                    btnDisplayPaymentSummey.Visible = False
                    lblEnrolmsg.Visible = False
                ElseIf Convert.ToString(dr(0)("RuleName")).Trim.ToLower = "ac2" Then
                    lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.AC2Msg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblEnrollmentMsg.Visible = True
                    btnDisplayPaymentSummey.Visible = False
                    lblEnrolmsg.Visible = False
                    lblMsgWarning.Visible = False
                End If
            Else
                dvEnrolmentOptions.Visible = True
                gvCurriculumCourse.DataSource = _studentEnrollmentDetails
                'redmine Issue #15351
                If CBool(ViewState("CentrallyManaged")) Then
                    If ViewState("IsCentrallyManagedContractAutum") = "Yes" Then
                        btnDisplayPaymentSummey.Visible = True
                        gvCurriculumCourse.Visible = True
                        lblEnrolmsg.Visible = True
                    Else
                        btnDisplayPaymentSummey.Visible = False
                        lblEnrolmsg.Visible = False
                        lblCentrallyMngError.Visible = True
                        If EducationContract.Trim.ToLower = "contract" Then
                            gvCurriculumCourse.Visible = False
                            lblMsgWarning.Visible = False
                            lblEnrolmsg.Visible = False
                        Else
                            gvCurriculumCourse.Visible = True
                            lblEnrolmsg.Visible = True
                            lblMsgWarning.Visible = True
                            btnDisplayPaymentSummey.Visible = True
                        End If
                    End If
                Else
                    gvCurriculumCourse.Visible = True
                    lblEnrolmsg.Visible = True
                    lblCentrallyMngError.Visible = False
                    lblMsgWarning.Visible = True
                    btnDisplayPaymentSummey.Visible = True
                End If
                ''txtIntialAmount.Visible = True
                ''lblIntialAmt.Visible = True
                ''txtIntialAmount.Visible = True
                ''lblCurrency.Visible = True
                ddlCAP1StudGrp.Visible = True
                lblStdgrp.Visible = True
                lblEnrollmentMsg.Text = ""
                lblStudentGroup.Visible = True
                'lblMsgWarning.Visible = True
                ''CreditCard.Visible = True
                ' lblCurrentAcademicCycle.Visible = True
                '  lblCurrentAcademicCycleName.Visible = True
                ' lblNextAcademicCycle.Visible = True
                '  lblNextAcademicCycleName.Visible = True
            End If

        Else
            gvCurriculumCourse.Visible = False
            lblEnrolmsg.Visible = False
            txtIntialAmount.Visible = False
            lblIntialAmt.Visible = False
            lblCurrency.Visible = False
            txtIntialAmount.Visible = False
            ddlPaymentPlan.Visible = False
            'ddlCAP1StudGrp.Visible = False
            lblStudentGroup.Visible = False
            CreditCard.Visible = False
            btnDisplayPaymentSummey.Visible = False
            lblEnrolmsg.Visible = False
            '  lblCurrentAcademicCycle.Visible = False
            'lblCurrentAcademicCycleName.Visible = False
            'lblNextAcademicCycle.Visible = False
            'lblNextAcademicCycleName.Visible = False
            'Susan Wong, #20033, Hide NoEnrollment culture string on lblEnrollmentMsg until next year
            'lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.NoEnrollment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        End If
    End Sub

    Protected Sub chkClassRoom_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkClassRoom As CheckBox = DirectCast(sender, CheckBox)
            '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
            Dim item As GridDataItem = DirectCast(chkClassRoom.NamingContainer, GridDataItem)
            Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)
            Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)
            Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
            Dim lblSubject As Label = DirectCast(item.FindControl("lblSubject"), Label)
            Dim lblClsroom As Label = DirectCast(item.FindControl("lblClsroom"), Label)
            Dim chkClassRoomTxt() As String = lblClsroom.Text.Split(CChar(";"))
            Dim lblMockExam As Label = DirectCast(item.FindControl("lblMockExam"), Label)
            Dim chkMockExamtxt() As String = lblMockExam.Text.Split(CChar(";"))
            Dim lblInterimAssessment As Label = DirectCast(item.FindControl("lblInterimAssessment"), Label)
            Dim chkInterimAssessmenttxt() As String = lblInterimAssessment.Text.Split(CChar(";"))
            Dim lblSummerExam As Label = DirectCast(item.FindControl("lblSummerExam"), Label)
            Dim chkSummerExamtxt() As String = lblSummerExam.Text.Split(CChar(";"))
            Dim lblRevision As Label = DirectCast(item.FindControl("lblRevision"), Label)
            Dim chkRevisiontxt() As String = lblRevision.Text.Split(CChar(";"))
            Dim lblCutOffUnits As Label = DirectCast(item.FindControl("lblCutOffUnits"), Label)
            Dim lblMinimumUnits As Label = DirectCast(item.FindControl("lblMinimumUnits"), Label)
            Dim lblUnits As Label = DirectCast(item.FindControl("lblUnits"), Label)
            Dim lblCurriculumID As Label = DirectCast(item.FindControl("lblCurriculumID"), Label)
            '  Dim lblAcademicCycleID As Label = DirectCast(item.FindControl("lblAcademicCycleID"), Label)
            ' if Repeate revision checked 
            Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)
            Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim lblResitInterimAssessment As Label = DirectCast(item.FindControl("lblResitInterimAssessment"), Label)
            Dim chkResitInterimAssessmenttxt() As String = lblResitInterimAssessment.Text.Split(CChar(";"))

            Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)
            Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
            Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))

            Dim lblRepeatRevision As Label = DirectCast(item.FindControl("lblRepeatRevision"), Label)
            Dim chkRepeatRevisiontxt() As String = lblRepeatRevision.Text.Split(CChar(";"))
            Dim lblAlternativeGroup As Label = DirectCast(item.FindControl("lblAlternativeGroup"), Label)

            If chkRepeatRevision.Checked Then
                If Convert.ToInt32(chkRepeatRevisiontxt(1)) > 0 Then
                    chkRepeatRevision.Checked = False
                End If
            End If
            If chkResitInterimAssessment.Checked Then
                If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                    chkResitInterimAssessment.Checked = False
                End If
            End If
            ''Added BY Pradip 2016-02-12 for Issue 5371
            chkResitInterimAssessment.Enabled = True

            If chkAutumnExam.Checked Then
                If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                    chkAutumnExam.Checked = False
                    chkAutumnExam.Enabled = True

                End If
            End If
            If chkClassRoom.Checked = True Then
                If chkMockExam.Checked Then
                    If Convert.ToInt32(chkMockExamtxt(1)) > 0 Then
                    End If
                End If
                If chkInterimAssessment.Checked Then
                    If Convert.ToInt32(chkInterimAssessmenttxt(1)) > 0 Then
                    End If
                End If
                If chkSummerExam.Checked Then
                    If Convert.ToInt32(chkSummerExamtxt(1)) > 0 Then

                    End If
                End If
                ' check if while selecting summer class room and also select Revison exam then unchecked revision
                If chkRevision.Checked Then
                    chkRevision.Checked = False
                    If Convert.ToInt32(chkRevisiontxt(1)) > 0 Then
                    End If
                End If
                ''change
                ''End If

                If Convert.ToInt32(chkRepeatRevisiontxt(1)) > 0 OrElse Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 OrElse Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                Else
                    chkInterimAssessment.Enabled = True
                    chkSummerExam.Enabled = True
                End If
                If chkInterimAssessment.Enabled = True Then
                    chkInterimAssessment.Checked = True
                    chkInterimAssessment.Enabled = False
                End If
                If chkMockExamtxt(0) > 0 Then
                    chkMockExam.Checked = True
                    chkMockExam.Enabled = False
                End If
                If chkSummerExam.Enabled = True Then
                    chkSummerExam.Checked = True
                    chkSummerExam.Enabled = False
                End If
            Else
                ' chkRevision.Checked = False
                chkInterimAssessment.Checked = False
                'chkInterimAssessment.Enabled = True
                chkMockExam.Checked = False

                'chkMockExam.Enabled = True
                chkSummerExam.Checked = False
                ' chkSummerExam.Enabled = True
                ''Uncommented By Pradip 2016-02-03 For Issue No 5371 made True
                chkMockExam.Enabled = True
                If Convert.ToInt32(chkRepeatRevisiontxt(1)) > 0 OrElse Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 OrElse Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                    chkInterimAssessment.Enabled = True
                    ''commented By Pradip 2016-02-03 For Issue No 5371 made True
                    ' chkMockExam.Enabled = False
                    chkMockExam.Enabled = True
                    chkSummerExam.Enabled = True
                Else
                    If Convert.ToInt32(chkRevisiontxt(1)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(1)) > 0 AndAlso chkClassRoom.Checked = False AndAlso chkRevision.Checked = False Then
                        chkInterimAssessment.Enabled = True
                        chkSummerExam.Enabled = True
                        ''Added By Pradip 2016-01-09 For Group3 Tracker G3-53
                        chkMockExam.Enabled = True
                    Else
                        chkInterimAssessment.Enabled = False
                        chkMockExam.Enabled = False
                        chkSummerExam.Enabled = False
                    End If
                End If
                '' Code Added by GM for Redmine #20032
                Dim lblIsFailed As Label = DirectCast(item.FindControl("lblIsFailed"), Label)
                If Convert.ToInt32(lblIsFailed.Text) = 1 Then
                    chkInterimAssessment.Enabled = True
                    chkSummerExam.Enabled = True
                    ''Added By Pradip 2016-01-09 For Group3 Tracker G3-53
                    chkMockExam.Enabled = True
                End If
                ' END Redmine #20032
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub chkRepeatRevision_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkRepeatRevision As CheckBox = DirectCast(sender, CheckBox)
            '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
            Dim item As GridDataItem = DirectCast(chkRepeatRevision.NamingContainer, GridDataItem)
            Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)
            Dim lblRevision As Label = DirectCast(item.FindControl("lblRevision"), Label)
            Dim chkRevisiontxt() As String = lblRevision.Text.Split(CChar(";"))


            Dim chkClassRoom As CheckBox = DirectCast(item.FindControl("chkClassRoom"), CheckBox)
            Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)
            Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
            Dim lblClsroom As Label = DirectCast(item.FindControl("lblClsroom"), Label)
            Dim chkClassRoomTxt() As String = lblClsroom.Text.Split(CChar(";"))
            Dim lblMockExam As Label = DirectCast(item.FindControl("lblMockExam"), Label)
            Dim chkMockExamtxt() As String = lblMockExam.Text.Split(CChar(";"))
            Dim lblInterimAssessment As Label = DirectCast(item.FindControl("lblInterimAssessment"), Label)
            Dim chkInterimAssessmenttxt() As String = lblInterimAssessment.Text.Split(CChar(";"))
            Dim lblSummerExam As Label = DirectCast(item.FindControl("lblSummerExam"), Label)
            Dim chkSummerExamtxt() As String = lblSummerExam.Text.Split(CChar(";"))
            ' Dim lblAcademicCycleID As Label = DirectCast(item.FindControl("lblAcademicCycleID"), Label)
            Dim lblCutOffUnits As Label = DirectCast(item.FindControl("lblCutOffUnits"), Label)
            Dim lblMinimumUnits As Label = DirectCast(item.FindControl("lblMinimumUnits"), Label)
            Dim lblUnits As Label = DirectCast(item.FindControl("lblUnits"), Label)
            Dim lblAlternativeGroup As Label = DirectCast(item.FindControl("lblAlternativeGroup"), Label)
            If chkClassRoom.Checked Then
                If Convert.ToInt32(chkClassRoomTxt(1)) > 0 Then
                    chkClassRoom.Checked = False
                End If
            End If
            ''Added BY Pradip 2016-02-16 for Issue no 5438
            chkMockExam.Enabled = True
            If chkMockExam.Checked Then
                If Convert.ToInt32(chkMockExamtxt(1)) > 0 Then
                    chkMockExam.Checked = False
                End If
            End If
            If chkInterimAssessment.Checked Then
                If Convert.ToInt32(chkInterimAssessmenttxt(1)) > 0 Then
                    chkInterimAssessment.Checked = False
                    chkInterimAssessment.Enabled = True
                End If
            End If
            If chkSummerExam.Checked Then
                If Convert.ToInt32(chkSummerExamtxt(1)) > 0 Then
                    chkSummerExam.Checked = False
                    chkSummerExam.Enabled = True
                End If
            End If
            ' check if while selecting summer class room and also select Revison exam then unchecked revision
            If chkRevision.Checked Then
                chkRevision.Checked = False
            End If

            If chkRevision.Checked Then
                chkRevision.Checked = False
            End If

            Dim lblSubject As Label = DirectCast(item.FindControl("lblSubject"), Label)
            Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim lblResitInterimAssessment As Label = DirectCast(item.FindControl("lblResitInterimAssessment"), Label)
            Dim chkResitInterimAssessmenttxt() As String = lblResitInterimAssessment.Text.Split(CChar(";"))
            Dim lblCurriculumID As Label = DirectCast(item.FindControl("lblCurriculumID"), Label)
            Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)
            Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
            Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))

            Dim lblRepeatRevision As Label = DirectCast(item.FindControl("lblRepeatRevision"), Label)
            Dim chkRepeatRevisiontxt() As String = lblRepeatRevision.Text.Split(CChar(";"))
            'If chkResitInterimAssessment.Checked Then
            'If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
            'chkResitInterimAssessment.Checked = False
            'End If
            'End If
            'If chkAutumnExam.Checked Then
            'If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
            'chkAutumnExam.Checked = False
            ' chkAutumnExam.Enabled = True
            'End If
            'End If
            If chkRepeatRevision.Checked Then
                Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
                If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                    ''Added By Pradip 2016-03-23 For MidFeb-9 Tracker Item From G3 MidFeb Tracker
                    If Convert.ToInt32(chkInterimAssessmenttxt(4)) = "22" Then
                        btnNo.Visible = False
                        btnSuccess.Visible = False
                        btnSubmitOk.Visible = False
                        btnIAWarning.Visible = True
                        lblSubmitMessage.Text = ""
                        lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.OptOutIA")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        radwindowSubmit.VisibleOnPageLoad = True
                        chkResitInterimAssessment.Checked = True
                        ''Commented By Pradip 2016-03-23 For MidFeb-9 Tracker Item From G3 MidFeb Tracker
                        'chkResitInterimAssessment.Enabled = False
                    End If
                End If

                If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                    chkAutumnExam.Checked = True
                    chkAutumnExam.Enabled = False
                End If
            Else

                If chkAutumnExam.Checked = False Then
                    chkAutumnExam.Checked = False
                Else
                    chkAutumnExam.Checked = True
                End If
                If chkResitInterimAssessment.Checked = False Then
                    chkResitInterimAssessment.Checked = False
                Else
                    chkResitInterimAssessment.Checked = True

                End If
                ' below code commented for Redmine #16029 and added uncommented code
                Dim lblSubjectID As Label = DirectCast(item.FindControl("lblSubjectID"), Label)

                If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                    'chkResitInterimAssessment.Checked = False
                    chkResitInterimAssessment.Enabled = True
                    'Redmine #16029
                    If chkResitInterimAssessment.Enabled Then
                        Dim sSQLDEBKNotPass As String = Database & "..spCheckDEBKNotPass__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@CourseID=" & lblSubjectID.Text
                        Dim bDEBKPass As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSQLDEBKNotPass, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If bDEBKPass = False Then
                            chkResitInterimAssessment.Enabled = False
                        End If
                    End If
                End If
                If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                    ' chkAutumnExam.Checked = False
                    chkAutumnExam.Enabled = True
                    'Redmine #16029
                    If chkAutumnExam.Enabled Then
                        Dim sSQLDEBKNotPass As String = Database & "..spCheckDEBKNotPass__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@CourseID=" & lblSubjectID.Text
                        Dim bDEBKPass As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSQLDEBKNotPass, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If bDEBKPass = False Then
                            chkAutumnExam.Enabled = False
                        End If
                    End If
                End If
                ''If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                'chkResitInterimAssessment.Checked = False
                '' chkResitInterimAssessment.Enabled = True
                ''End If
                ''If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                ' chkAutumnExam.Checked = False
                ''chkAutumnExam.Enabled = True
                ''End If
                ' Redmine #16029 end
            End If


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub chkResitInterimAssessment_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkResitInterimAssessment As CheckBox = DirectCast(sender, CheckBox)
            '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
            Dim item As GridDataItem = DirectCast(chkResitInterimAssessment.NamingContainer, GridDataItem)
            Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)
            If chkInterimAssessment.Checked Then
                'chkInterimAssessment.Checked = False
            End If
            Dim lblInterimAssessment As Label = DirectCast(item.FindControl("lblInterimAssessment"), Label)
            Dim chkInterimAssessmenttxt() As String = lblInterimAssessment.Text.Split(CChar(";"))
            Dim lblSubject As Label = DirectCast(item.FindControl("lblSubject"), Label)
            Dim lblResitInterimAssessment As Label = DirectCast(item.FindControl("lblResitInterimAssessment"), Label)
            Dim lblCurriculumID As Label = DirectCast(item.FindControl("lblCurriculumID"), Label)
            Dim lblCutOffUnits As Label = DirectCast(item.FindControl("lblCutOffUnits"), Label)
            Dim lblMinimumUnits As Label = DirectCast(item.FindControl("lblMinimumUnits"), Label)
            Dim lblUnits As Label = DirectCast(item.FindControl("lblUnits"), Label)
            'Dim lblAcademicCycleID As Label = DirectCast(item.FindControl("lblAcademicCycleID"), Label)
            Dim chkResitInterimAssessmenttxt() As String = lblResitInterimAssessment.Text.Split(CChar(";"))
            Dim chkClassRoom As CheckBox = DirectCast(item.FindControl("chkClassRoom"), CheckBox)
            Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)
            Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
            Dim lblClsroom As Label = DirectCast(item.FindControl("lblClsroom"), Label)
            Dim chkClassRoomTxt() As String = lblClsroom.Text.Split(CChar(";"))
            Dim lblMockExam As Label = DirectCast(item.FindControl("lblMockExam"), Label)
            Dim chkMockExamtxt() As String = lblMockExam.Text.Split(CChar(";"))
            Dim lblSummerExam As Label = DirectCast(item.FindControl("lblSummerExam"), Label)
            Dim chkSummerExamtxt() As String = lblSummerExam.Text.Split(CChar(";"))
            Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)
            Dim lblRevision As Label = DirectCast(item.FindControl("lblRevision"), Label)
            Dim chkRevisiontxt() As String = lblRevision.Text.Split(CChar(";"))
            Dim lblAlternativeGroup As Label = DirectCast(item.FindControl("lblAlternativeGroup"), Label)
            Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
            Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))
            Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)
            ''Added by PRadip 2016-03-23
            Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)
            If chkResitInterimAssessment.Checked Then
                If chkInterimAssessment.Checked Then
                    If Convert.ToInt32(chkInterimAssessmenttxt(1)) > 0 Then
                        chkInterimAssessment.Checked = False
                        chkInterimAssessment.Enabled = True
                    End If
                End If
                If chkClassRoom.Checked Then
                    If Convert.ToInt32(chkClassRoomTxt(1)) > 0 Then
                        chkClassRoom.Checked = False

                    End If
                End If
                ''Added BY Pradip 2016-02-16 for Issue no 5438
                chkMockExam.Enabled = True
                If chkMockExam.Checked Then
                    If Convert.ToInt32(chkMockExamtxt(1)) > 0 Then
                        chkMockExam.Checked = False
                    End If
                End If
                If chkSummerExam.Checked Then
                    If Convert.ToInt32(chkSummerExamtxt(1)) > 0 Then
                        chkSummerExam.Checked = False
                        chkSummerExam.Enabled = True
                    End If
                End If
                If chkRevision.Checked Then
                    chkRevision.Checked = False
                End If
                If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                    chkAutumnExam.Checked = True
                    chkAutumnExam.Enabled = False
                End If
            End If
            ''Added BY Pradip 2016-03-23
            If chkRepeatRevision.Checked = False And chkResitInterimAssessment.Checked = False Then
                If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                    ' redmine 15954
                    'chkAutumnExam.Checked = False
                    chkAutumnExam.Enabled = True
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub chkAutumnExam_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkAutumnExam As CheckBox = DirectCast(sender, CheckBox)
            '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
            Dim item As GridDataItem = DirectCast(chkAutumnExam.NamingContainer, GridDataItem)
            Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
            If chkSummerExam.Checked Then
                '  chkSummerExam.Checked = False
            End If
            Dim lblSummerExam As Label = DirectCast(item.FindControl("lblSummerExam"), Label)
            Dim chkSummerExamtxt() As String = lblSummerExam.Text.Split(CChar(";"))
            Dim lblSubject As Label = DirectCast(item.FindControl("lblSubject"), Label)
            Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
            Dim lblCurriculumID As Label = DirectCast(item.FindControl("lblCurriculumID"), Label)
            Dim lblCutOffUnits As Label = DirectCast(item.FindControl("lblCutOffUnits"), Label)
            Dim lblMinimumUnits As Label = DirectCast(item.FindControl("lblMinimumUnits"), Label)
            Dim lblUnits As Label = DirectCast(item.FindControl("lblUnits"), Label)
            'Dim lblAcademicCycleID As Label = DirectCast(item.FindControl("lblAcademicCycleID"), Label)
            Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))
            Dim chkClassRoom As CheckBox = DirectCast(item.FindControl("chkClassRoom"), CheckBox)
            Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)
            Dim lblClsroom As Label = DirectCast(item.FindControl("lblClsroom"), Label)
            Dim chkClassRoomTxt() As String = lblClsroom.Text.Split(CChar(";"))
            Dim lblMockExam As Label = DirectCast(item.FindControl("lblMockExam"), Label)
            Dim chkMockExamtxt() As String = lblMockExam.Text.Split(CChar(";"))
            Dim lblInterimAssessment As Label = DirectCast(item.FindControl("lblInterimAssessment"), Label)
            Dim chkInterimAssessmenttxt() As String = lblInterimAssessment.Text.Split(CChar(";"))
            Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)
            Dim lblRevision As Label = DirectCast(item.FindControl("lblRevision"), Label)
            Dim chkRevisiontxt() As String = lblRevision.Text.Split(CChar(";"))
            Dim lblAlternativeGroup As Label = DirectCast(item.FindControl("lblAlternativeGroup"), Label)

            Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)

            If chkAutumnExam.Checked Then
                If chkSummerExam.Checked Then
                    If Convert.ToInt32(chkSummerExamtxt(1)) > 0 Then
                        chkSummerExam.Checked = False
                        ''Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkSummerExamtxt(1)))
                        ''SummerPaymentSummery.Rows.Remove(dr(0))
                        ''SummerPaymentSummery.AcceptChanges()
                        chkSummerExam.Enabled = True
                    End If
                End If
                If chkClassRoom.Checked Then
                    If Convert.ToInt32(chkClassRoomTxt(1)) > 0 Then
                        chkClassRoom.Checked = False
                        ''Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkClassRoomTxt(1)))
                        ''SummerPaymentSummery.Rows.Remove(dr(0))
                        ''SummerPaymentSummery.AcceptChanges()
                    End If
                End If
                ''Added BY Pradip 2016-02-16 for Issue no 5438
                chkMockExam.Enabled = True
                If chkMockExam.Checked Then
                    If Convert.ToInt32(chkMockExamtxt(1)) > 0 Then
                        chkMockExam.Checked = False
                        ''Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkMockExamtxt(1)))
                        ''SummerPaymentSummery.Rows.Remove(dr(0))
                        ''SummerPaymentSummery.AcceptChanges()
                        'chkMockExam.Enabled = True
                    End If
                End If
                If chkInterimAssessment.Checked Then
                    If Convert.ToInt32(chkInterimAssessmenttxt(1)) > 0 Then
                        chkInterimAssessment.Checked = False
                        ''Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkInterimAssessmenttxt(1)))
                        ''SummerPaymentSummery.Rows.Remove(dr(0))
                        ''SummerPaymentSummery.AcceptChanges()
                        chkInterimAssessment.Enabled = True
                    End If
                End If
                If chkRevision.Checked Then
                    chkRevision.Checked = False
                    ''If Convert.ToInt32(chkRevisiontxt(1)) > 0 Then
                    ''    Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkRevisiontxt(1)))
                    ''    SummerPaymentSummery.Rows.Remove(dr(0))
                    ''    SummerPaymentSummery.AcceptChanges()
                    ''End If
                End If
                ''If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                ''    Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
                ''    Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                ''    drSummerPaymentSummery("Subject") = lblSubject.Text
                ''    drSummerPaymentSummery("Type") = "Autumn Exam"
                ''    drSummerPaymentSummery("ProductID") = chkAutumnExamtxt(1)
                ''    If WhoPaysForElevationStudent(CInt(chkAutumnExamtxt(1)), ddlRoute.SelectedValue, chkAutumnExamtxt(0)) Then
                ''        drSummerPaymentSummery("WhoPay") = "Firm Pay"
                ''        drSummerPaymentSummery("IsProductPaymentPlan") = 0

                ''    Else
                ''        drSummerPaymentSummery("WhoPay") = "Member Pay"
                ''        ' Check Product Having Payment Plan
                ''        Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkAutumnExamtxt(1))
                ''        Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                ''        If iProductPaymentPlan > 0 Then
                ''            drSummerPaymentSummery("IsProductPaymentPlan") = 1
                ''        Else
                ''            drSummerPaymentSummery("IsProductPaymentPlan") = 0
                ''        End If
                ''    End If
                ''    drSummerPaymentSummery("ClassID") = chkAutumnExamtxt(0)
                ''    Dim dAutumnExamTax As Decimal
                ''    drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkAutumnExamtxt(1)), chkAutumnExamtxt(0), ddlCAP1StudGrp.SelectedValue, dAutumnExamTax)
                ''    drSummerPaymentSummery("TaxAmount") = dAutumnExamTax
                ''    drSummerPaymentSummery("AcademicCycleID") = chkAutumnExamtxt(6)
                ''    drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                ''    drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                ''    drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                ''    drSummerPaymentSummery("Units") = lblUnits.Text
                ''    If lblAlternativeGroup.Text.Trim <> "" Then
                ''        drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                ''    End If
                ''    dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                ''    SummerPaymentSummery = dtSummerPaymentSummery
                ''End If
            Else
                ' Redmine #15954
                If chkResitInterimAssessment.Checked = True AndAlso chkResitInterimAssessment.Enabled Then
                    chkResitInterimAssessment.Checked = False
                End If
                ''If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                ''    Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkAutumnExamtxt(1)))
                ''    SummerPaymentSummery.Rows.Remove(dr(0))
                ''    SummerPaymentSummery.AcceptChanges()
                ''End If
            End If
            ''Dim dicSum As New Dictionary(Of String, Decimal)()
            ''For Each row As DataRow In SummerPaymentSummery.Rows
            ''    Dim group As String = row("Subject").ToString()
            ''    Dim rate As Decimal = CDec(Convert.ToString(row("Price")).Substring(1, Convert.ToString(row("Price")).Length - 1))
            ''    If dicSum.ContainsKey(group) Then
            ''        dicSum(group) += rate
            ''    Else
            ''        dicSum.Add(group, rate)
            ''    End If
            ''Next
            ''GV.DataSource = dicSum
            ''GV.DataBind()
            ''Dim sProductID As String = String.Empty
            ''If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
            ''    radSummerPaymentSummery.DataSource = SummerPaymentSummery
            ''    radSummerPaymentSummery.DataBind()
            ''    ''radSummerPaymentSummery.Visible = True
            ''    lblAmount.Visible = True
            ''    lblTotalAmount.Visible = True
            ''    Dim dAmt As Decimal = 0
            ''    lblStudentPaidLabel.Visible = True
            ''    lblAmountPaidStudent.Visible = True
            ''    lblFirmPaidLabel.Visible = True
            ''    lblAmountPaidFirm.Visible = True
            ''    lblCurrency.Visible = True
            ''    txtIntialAmount.Visible = True
            ''    lblCurrency.Visible = True
            ''    lblIntialAmt.Visible = True
            ''    Dim dAmountPaidByStudent As Decimal = 0
            ''    Dim dAmountPaidByFirm As Decimal = 0
            ''    Dim dPaymentPlanAmount As Decimal = 0
            ''    Dim dTaxAmount As Decimal = 0
            ''    For Each dr As DataRow In SummerPaymentSummery.Rows
            ''        If dAmt = 0 Then
            ''            dAmt = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
            ''            If Convert.ToString(dr("WhoPay")).Trim.ToLower = "member pay" Then
            ''                dAmountPaidByStudent = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
            ''            ElseIf Convert.ToString(dr("WhoPay")).Trim.ToLower = "firm pay" Then
            ''                dAmountPaidByFirm = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
            ''            End If

            ''        Else
            ''            dAmt = Convert.ToDecimal(dAmt + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
            ''            If Convert.ToString(dr("WhoPay")).Trim.ToLower = "member pay" Then
            ''                dAmountPaidByStudent = Convert.ToDecimal(dAmountPaidByStudent + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
            ''            ElseIf Convert.ToString(dr("WhoPay")).Trim.ToLower = "firm pay" Then
            ''                dAmountPaidByFirm = Convert.ToDecimal(dAmountPaidByFirm + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
            ''            End If
            ''        End If

            ''        If dr("IsProductPaymentPlan") = 1 Then
            ''            If sProductID = "" Then
            ''                sProductID = dr("ProductID")
            ''            Else
            ''                sProductID = sProductID + "," + dr("ProductID")
            ''            End If
            ''            If dPaymentPlanAmount = 0 Then
            ''                dPaymentPlanAmount = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
            ''            Else
            ''                dPaymentPlanAmount = Convert.ToDecimal(dPaymentPlanAmount + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
            ''            End If
            ''        End If
            ''        If dTaxAmount = 0 Then
            ''            dTaxAmount = CDec(dr("TaxAmount"))
            ''        Else
            ''            dTaxAmount = dTaxAmount + CDec(dr("TaxAmount"))
            ''        End If
            ''    Next
            ''    lblStagePaymentTotal.Text = ViewState("CurrencyTypeID") & Format(CDec(dPaymentPlanAmount), "0.00")
            ''    lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
            ''    lblAmountPaidStudent.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmountPaidByStudent), "0.00")
            ''    lblAmountPaidFirm.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmountPaidByFirm), "0.00")
            ''    lblTaxAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dTaxAmount), "0.00")
            ''    Dim dtPaymentPlan As DataTable = DirectCast(ViewState("SelectedPaymentPlan"), DataTable)
            ''    If Not dtPaymentPlan Is Nothing AndAlso dtPaymentPlan.Rows.Count > 0 Then
            ''        For Each drP As DataRow In dtPaymentPlan.Rows
            ''            Dim dStageAmt As Decimal = CDec(Convert.ToString(lblStagePaymentTotal.Text).Substring(1, Convert.ToString(lblStagePaymentTotal.Text).Length - 1))
            ''            Dim dStudentPayAmt As Decimal = CDec(Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1))
            ''            Dim dPercentageAmt As Decimal = (dStageAmt * CDec(drP("Percentage"))) / 100
            ''            If CInt(drP("days")) = 0 Then
            ''                Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
            ''                txtIntialAmount.Text = Format(CDec(dintialStageAmount + dPercentageAmt), "0.00")
            ''            Else
            ''                Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
            ''                txtIntialAmount.Text = Format(CDec(dintialStageAmount), "0.00")
            ''            End If
            ''        Next
            ''        txtIntialAmount.Text = Format((CDec(txtIntialAmount.Text) + dTaxAmount), "0.00")
            ''    Else
            ''        Dim dTotalAmtPaidWithTax As Decimal = (dAmountPaidByStudent + dTaxAmount)
            ''        txtIntialAmount.Text = Format(dTotalAmtPaidWithTax, "0.00")
            ''        'txtIntialAmount.Text = Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1).Trim
            ''    End If
            ''    lblPaymentSummery.Visible = True
            ''    If sProductID <> "" Then
            ''        LoadPaymentPlan(sProductID)
            ''        ddlPaymentPlan.Visible = True
            ''        lblPaymentPlan.Visible = True
            ''    Else
            ''        ddlPaymentPlan.Visible = False
            ''        lblPaymentPlan.Visible = False
            ''    End If
            ''    If dAmountPaidByStudent > 0 Then
            ''        CreditCard.Visible = True
            ''    Else
            ''        CreditCard.Visible = False
            ''        ddlPaymentPlan.Visible = False
            ''        lblPaymentPlan.Visible = False
            ''    End If
            ''Else
            ''    radSummerPaymentSummery.Visible = False
            ''    lblAmount.Visible = False
            ''    lblTotalAmount.Visible = False
            ''    lblStudentPaidLabel.Visible = False
            ''    lblAmountPaidStudent.Visible = False
            ''    lblFirmPaidLabel.Visible = False
            ''    lblAmountPaidFirm.Visible = False
            ''    lblPaymentSummery.Visible = False
            ''    CreditCard.Visible = False
            ''    txtIntialAmount.Visible = False
            ''    lblCurrency.Visible = False
            ''    lblIntialAmt.Visible = False
            ''    lblTaxAmount.Visible = False
            ''    lblTax.Visible = False
            ''    ddlPaymentPlan.Visible = False
            ''    lblPaymentPlan.Visible = False
            ''End If
            ''UpdatePanelPayment.Update()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub chkRevision_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkRevision As CheckBox = DirectCast(sender, CheckBox)
            '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
            Dim item As GridDataItem = DirectCast(chkRevision.NamingContainer, GridDataItem)
            Dim chkClassRoom As CheckBox = DirectCast(item.FindControl("chkClassRoom"), CheckBox)
            Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)
            Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
            Dim lblClsroom As Label = DirectCast(item.FindControl("lblClsroom"), Label)
            Dim chkClassRoomTxt() As String = lblClsroom.Text.Split(CChar(";"))
            Dim lblMockExam As Label = DirectCast(item.FindControl("lblMockExam"), Label)
            Dim chkMockExamtxt() As String = lblMockExam.Text.Split(CChar(";"))
            Dim lblInterimAssessment As Label = DirectCast(item.FindControl("lblInterimAssessment"), Label)
            Dim chkInterimAssessmenttxt() As String = lblInterimAssessment.Text.Split(CChar(";"))
            Dim lblSummerExam As Label = DirectCast(item.FindControl("lblSummerExam"), Label)
            Dim chkSummerExamtxt() As String = lblSummerExam.Text.Split(CChar(";"))

            Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)
            Dim lblCutOffUnits As Label = DirectCast(item.FindControl("lblCutOffUnits"), Label)
            Dim lblMinimumUnits As Label = DirectCast(item.FindControl("lblMinimumUnits"), Label)
            Dim lblUnits As Label = DirectCast(item.FindControl("lblUnits"), Label)
            Dim lblCurriculumID As Label = DirectCast(item.FindControl("lblCurriculumID"), Label)
            Dim lblRepeatRevision As Label = DirectCast(item.FindControl("lblRepeatRevision"), Label)
            Dim chkRepeatRevisiontxt() As String = lblRepeatRevision.Text.Split(CChar(";"))
            ' Dim lblAcademicCycleID As Label = DirectCast(item.FindControl("lblAcademicCycleID"), Label)
            If chkRepeatRevision.Checked Then
                chkRepeatRevision.Checked = False
            End If
            Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim lblResitInterimAssessment As Label = DirectCast(item.FindControl("lblResitInterimAssessment"), Label)
            Dim chkResitInterimAssessmenttxt() As String = lblResitInterimAssessment.Text.Split(CChar(";"))

            Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)
            Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
            Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))

            If chkResitInterimAssessment.Checked Then
                If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                    chkResitInterimAssessment.Checked = False
                    chkResitInterimAssessment.Enabled = True
                End If
            End If
            If chkAutumnExam.Checked Then
                If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                    chkAutumnExam.Checked = False
                    chkAutumnExam.Enabled = True
                End If
            End If
            Dim lblSubject As Label = DirectCast(item.FindControl("lblSubject"), Label)
            Dim lblRevision As Label = DirectCast(item.FindControl("lblRevision"), Label)
            Dim chkRevisiontxt() As String = lblRevision.Text.Split(CChar(";"))
            Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
            If chkRevision.Checked Then
                If Convert.ToInt32(chkInterimAssessmenttxt(1)) > 0 Then
                    If Convert.ToInt32(chkInterimAssessmenttxt(4)) = "22" Then
                        chkInterimAssessment.Checked = True
                        ''Commented BY  and added Pradip 2016-03-23 For MidFeb-9
                        ' chkInterimAssessment.Enabled = False
                        chkInterimAssessment.Enabled = True
                        btnNo.Visible = False
                        btnSuccess.Visible = False
                        btnSubmitOk.Visible = False
                        btnIAWarning.Visible = True
                        lblSubmitMessage.Text = ""
                        lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.OptOutIA")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        radwindowSubmit.VisibleOnPageLoad = True
                    End If



                End If
                If Convert.ToInt32(chkSummerExamtxt(1)) > 0 Then
                    If chkSummerExam.Checked Then
                        chkSummerExam.Enabled = False
                    Else
                        chkSummerExam.Checked = True
                        chkSummerExam.Enabled = False
                    End If
                End If
                ' if Revision Course Checked then unselected Classroom,Interim,Mock and Exam
                If chkClassRoom.Checked Then
                    chkClassRoom.Checked = False
                End If
                ''Commented By Pradip 2016-01-09 For G3 Tracker G3-53
                'If chkMockExam.Checked = True Then
                '    chkMockExam.Checked = False
                'End If
                chkMockExam.Checked = True
                'chkMockExam.Enabled = False
                chkMockExam.Enabled = True '  Redmine log #18730
            Else
                If Convert.ToInt32(chkInterimAssessmenttxt(1)) > 0 Then
                    ' chkInterimAssessment.Checked = False
                    chkInterimAssessment.Enabled = True
                End If
                If Convert.ToInt32(chkSummerExamtxt(1)) > 0 Then
                    ''chkSummerExam.Checked = False
                    chkSummerExam.Enabled = True
                End If
                ''Added By Pradip 2016-01-09 For G3 Tracker G3-53
                chkMockExam.Checked = False
                chkMockExam.Enabled = True
            End If


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub chkMockExam_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkMockExam As CheckBox = DirectCast(sender, CheckBox)
            Dim item As GridDataItem = DirectCast(chkMockExam.NamingContainer, GridDataItem)
            Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)
            Dim lblRevision As Label = DirectCast(item.FindControl("lblRevision"), Label)
            Dim chkRevisiontxt() As String = lblRevision.Text.Split(CChar(";"))
            Dim lblSubject As Label = DirectCast(item.FindControl("lblSubject"), Label)
            Dim lblMockExam As Label = DirectCast(item.FindControl("lblMockExam"), Label)
            Dim lblAlternativeGroup As Label = DirectCast(item.FindControl("lblAlternativeGroup"), Label)
            ' Dim lblAcademicCycleID As Label = DirectCast(item.FindControl("lblAcademicCycleID"), Label)
            Dim chkMockExamtxt() As String = lblMockExam.Text.Split(CChar(";"))
            ''Added BY Pradip 2016-01-09 For Group 3 Tracker G3-53
            Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
            ''Added BY Pradip 2016-02-12 For Issue NO 5371
            Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)

            Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)
            Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim lblResitInterimAssessment As Label = DirectCast(item.FindControl("lblResitInterimAssessment"), Label)
            Dim chkResitInterimAssessmenttxt() As String = lblResitInterimAssessment.Text.Split(CChar(";"))
            Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)
            Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
            Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))
            Dim lblRepeatRevision As Label = DirectCast(item.FindControl("lblRepeatRevision"), Label)
            Dim chkRepeatRevisiontxt() As String = lblRepeatRevision.Text.Split(CChar(";"))


            If chkMockExam.Checked Then
                If chkRevision.Checked Then
                    chkRevision.Checked = False
                End If
                ''Added BY Pradip 2016-01-09 For Group 3 Tracker G3-53
                chkSummerExam.Checked = True
                chkSummerExam.Enabled = False
                If chkRepeatRevision.Checked Then
                    If Convert.ToInt32(chkRepeatRevisiontxt(1)) > 0 Then
                        chkRepeatRevision.Checked = False
                    End If
                End If
                If chkResitInterimAssessment.Checked Then
                    If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                        chkResitInterimAssessment.Checked = False
                    End If
                End If
                ''Added BY Pradip 2016-02-12 for Issue 5371
                chkResitInterimAssessment.Enabled = True

                If chkAutumnExam.Checked Then
                    If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                        chkAutumnExam.Checked = False
                        chkAutumnExam.Enabled = True

                    End If
                End If

            Else
                If chkInterimAssessment.Checked = True Then
                    chkSummerExam.Checked = True
                    chkSummerExam.Enabled = False
                Else
                    '' chkSummerExam.Checked = False -- Commented by Harish #20944
                    chkSummerExam.Enabled = True
                    'Added as part of 20944
                    ''If chkRevision.Checked Then ''Commented by Harish Redmine #20944 - if Revision course (summer) checked. then  Summer exam & Mock exam was mandatory get checked and user want the Mock exam as optional if checked, Revision course (summer) should not be unchecked.
                    '' chkRevision.Checked = False ''Commented by Harish Redmine #20944 - if Revision course (summer) checked. then  Summer exam & Mock exam was mandatory get checked and user want the Mock exam as optional if checked, Revision course (summer) should not be unchecked. 
                    ''End If ''Commented by Harish Redmine #20944 - if Revision course (summer) checked. then  Summer exam & Mock exam was mandatory get checked and user want the Mock exam as optional if checked, Revision course (summer) should not be unchecked.
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub chkInterimAssessment_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkInterimAssessment As CheckBox = DirectCast(sender, CheckBox)
            '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
            Dim item As GridDataItem = DirectCast(chkInterimAssessment.NamingContainer, GridDataItem)
            Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
            If chkResitInterimAssessment.Checked Then
                ' chkResitInterimAssessment.Checked = False
            End If
            Dim lblSummerExam As Label = DirectCast(item.FindControl("lblSummerExam"), Label)
            Dim chkSummerExamtxt() As String = lblSummerExam.Text.Split(CChar(";"))
            Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)
            Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
            Dim lblRevision As Label = DirectCast(item.FindControl("lblRevision"), Label)
            Dim chkRevisiontxt() As String = lblRevision.Text.Split(CChar(";"))
            ' Dim lblAcademicCycleID As Label = DirectCast(item.FindControl("lblAcademicCycleID"), Label)
            Dim lblSubject As Label = DirectCast(item.FindControl("lblSubject"), Label)
            Dim lblInterimAssessment As Label = DirectCast(item.FindControl("lblInterimAssessment"), Label)
            Dim chkInterimAssessmenttxt() As String = lblInterimAssessment.Text.Split(CChar(";"))
            Dim lblAlternativeGroup As Label = DirectCast(item.FindControl("lblAlternativeGroup"), Label)
            Dim lblCutOffUnits As Label = DirectCast(item.FindControl("lblCutOffUnits"), Label)
            Dim lblMinimumUnits As Label = DirectCast(item.FindControl("lblMinimumUnits"), Label)
            Dim lblUnits As Label = DirectCast(item.FindControl("lblUnits"), Label)
            Dim lblCurriculumID As Label = DirectCast(item.FindControl("lblCurriculumID"), Label)
            Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)

            Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
            Dim chkRAutumnExamTxt() As String = lblAutumnExam.Text.Split(CChar(";"))
            Dim lblRepeatRevision As Label = DirectCast(item.FindControl("lblRepeatRevision"), Label)
            Dim chkRepeatRevisionTxt() As String = lblRepeatRevision.Text.Split(CChar(";"))
            Dim lblResitInterimAssessment As Label = DirectCast(item.FindControl("lblResitInterimAssessment"), Label)
            Dim chkResitInterimAssessmentTxt() As String = lblResitInterimAssessment.Text.Split(CChar(";"))
            Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)
            Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))
            If chkInterimAssessment.Checked Then
                If chkRepeatRevision.Checked Then
                    If Convert.ToInt32(chkRepeatRevisionTxt(1)) > 0 Then

                        chkRepeatRevision.Checked = False
                    End If
                End If
                If chkResitInterimAssessment.Checked Then
                    If Convert.ToInt32(chkResitInterimAssessmentTxt(1)) > 0 Then

                        chkResitInterimAssessment.Checked = False
                        chkResitInterimAssessment.Enabled = True
                    End If
                End If

                If chkAutumnExam.Checked Then
                    If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                        chkAutumnExam.Checked = False
                        chkAutumnExam.Enabled = True
                    End If
                End If
                If Convert.ToInt32(chkSummerExamtxt(1)) > 0 Then
                    chkSummerExam.Checked = True
                    chkSummerExam.Enabled = False
                End If
            Else
                If Convert.ToInt32(chkSummerExamtxt(1)) > 0 Then
                    'chkSummerExam.Checked = False
                    chkSummerExam.Enabled = True
                End If
            End If
            ''Added By Pradip 2016-02-12 for Issue No 5371
            Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)
            If chkMockExam.Checked = True Then
                chkSummerExam.Checked = True
                chkSummerExam.Enabled = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub SummerExam_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim SummerExam As CheckBox = DirectCast(sender, CheckBox)
            '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
            Dim item As GridDataItem = DirectCast(SummerExam.NamingContainer, GridDataItem)
            Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)
            If chkAutumnExam.Checked Then
                'chkAutumnExam.Checked = False
            End If
            Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)
            Dim lblRevision As Label = DirectCast(item.FindControl("lblRevision"), Label)
            Dim chkRevisiontxt() As String = lblRevision.Text.Split(CChar(";"))

            Dim lblSummerExam As Label = DirectCast(item.FindControl("lblSummerExam"), Label)
            Dim chkSummerExamtxt() As String = lblSummerExam.Text.Split(CChar(";"))
            Dim lblSubject As Label = DirectCast(item.FindControl("lblSubject"), Label)
            ' Dim lblAcademicCycleID As Label = DirectCast(item.FindControl("lblAcademicCycleID"), Label)

            Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
            Dim chkRAutumnExamTxt() As String = lblAutumnExam.Text.Split(CChar(";"))
            Dim lblRepeatRevision As Label = DirectCast(item.FindControl("lblRepeatRevision"), Label)
            Dim chkRepeatRevisionTxt() As String = lblRepeatRevision.Text.Split(CChar(";"))
            Dim lblResitInterimAssessment As Label = DirectCast(item.FindControl("lblResitInterimAssessment"), Label)
            Dim chkResitInterimAssessmentTxt() As String = lblResitInterimAssessment.Text.Split(CChar(";"))

            Dim chkClassRoom As CheckBox = DirectCast(item.FindControl("chkClassRoom"), CheckBox)
            Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)

            Dim lblClsroom As Label = DirectCast(item.FindControl("lblClsroom"), Label)
            Dim chkClassRoomTxt() As String = lblClsroom.Text.Split(CChar(";"))
            Dim lblMockExam As Label = DirectCast(item.FindControl("lblMockExam"), Label)
            Dim chkMockExamtxt() As String = lblMockExam.Text.Split(CChar(";"))
            Dim lblInterimAssessment As Label = DirectCast(item.FindControl("lblInterimAssessment"), Label)
            Dim chkInterimAssessmenttxt() As String = lblInterimAssessment.Text.Split(CChar(";"))
            Dim lblCutOffUnits As Label = DirectCast(item.FindControl("lblCutOffUnits"), Label)
            Dim lblMinimumUnits As Label = DirectCast(item.FindControl("lblMinimumUnits"), Label)
            Dim lblUnits As Label = DirectCast(item.FindControl("lblUnits"), Label)
            Dim lblCurriculumID As Label = DirectCast(item.FindControl("lblCurriculumID"), Label)
            Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
            Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)
            Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))
            Dim lblAlternativeGroup As Label = DirectCast(item.FindControl("lblAlternativeGroup"), Label)

            If SummerExam.Checked Then
                If chkRepeatRevision.Checked Then
                    If Convert.ToInt32(chkRepeatRevisionTxt(1)) > 0 Then
                        ''Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkRepeatRevisionTxt(1)))
                        ''SummerPaymentSummery.Rows.Remove(dr(0))
                        ''SummerPaymentSummery.AcceptChanges()
                        chkRepeatRevision.Checked = False
                    End If
                End If
                If chkResitInterimAssessment.Checked Then
                    If Convert.ToInt32(chkResitInterimAssessmentTxt(1)) > 0 Then
                        ''Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkResitInterimAssessmentTxt(1)))
                        ''If Not dr Is Nothing Then
                        ''    SummerPaymentSummery.Rows.Remove(dr(0))
                        ''    SummerPaymentSummery.AcceptChanges()
                        ''End If

                        chkResitInterimAssessment.Checked = False
                        chkResitInterimAssessment.Enabled = True
                    End If
                End If

                If chkAutumnExam.Checked Then
                    If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                        ''Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkAutumnExamtxt(1)))
                        ''If Not dr Is Nothing Then
                        ''    SummerPaymentSummery.Rows.Remove(dr(0))
                        ''    SummerPaymentSummery.AcceptChanges()
                        ''End If

                        chkAutumnExam.Checked = False
                        chkAutumnExam.Enabled = True

                    End If
                End If
                'If Convert.ToInt32(chkSummerExamtxt(1)) > 0 Then

                '    Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                '    drSummerPaymentSummery("Subject") = lblSubject.Text
                '    drSummerPaymentSummery("Type") = "Summer Exam"
                '    drSummerPaymentSummery("ProductID") = chkSummerExamtxt(1)
                '    If WhoPaysForElevationStudent(CInt(chkSummerExamtxt(1)), ddlRoute.SelectedValue, chkSummerExamtxt(0)) Then
                '        drSummerPaymentSummery("WhoPay") = "Firm Pay"
                '        drSummerPaymentSummery("IsProductPaymentPlan") = 0
                '    Else
                '        drSummerPaymentSummery("WhoPay") = "Member Pay"
                '        ' Check Product Having Payment Plan
                '        Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkSummerExamtxt(1))
                '        Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                '        If iProductPaymentPlan > 0 Then
                '            drSummerPaymentSummery("IsProductPaymentPlan") = 1
                '        Else
                '            drSummerPaymentSummery("IsProductPaymentPlan") = 0
                '        End If
                '    End If
                '    drSummerPaymentSummery("ClassID") = chkSummerExamtxt(0)
                '    Dim dSummerExamTax As Decimal
                '    drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkSummerExamtxt(1)), chkSummerExamtxt(0), ddlCAP1StudGrp.SelectedValue, dSummerExamTax)
                '    drSummerPaymentSummery("TaxAmount") = dSummerExamTax
                '    drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                '    drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                '    drSummerPaymentSummery("Units") = lblUnits.Text
                '    drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                '    drSummerPaymentSummery("AcademicCycleID") = chkSummerExamtxt(6)
                '    If lblAlternativeGroup.Text.Trim <> "" Then
                '        drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                '    End If
                '    dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                '    SummerPaymentSummery = dtSummerPaymentSummery
                'End If
                If chkRevision.Checked Then
                    chkRevision.Checked = False
                    If Convert.ToInt32(chkRevisiontxt(1)) > 0 Then
                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkRevisiontxt(1)))
                        SummerPaymentSummery.Rows.Remove(dr(0))
                        SummerPaymentSummery.AcceptChanges()
                    End If
                End If
                ''If chkRAutumnExamTxt(0) > 0 OrElse chkRepeatRevisionTxt(0) > 0 OrElse chkResitInterimAssessmentTxt(0) > 0 Then
                ''    If Convert.ToInt32(chkClassRoomTxt(1)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(4)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(4)) <> 2 Then
                ''        Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                ''        drSummerPaymentSummery("Subject") = lblSubject.Text
                ''        drSummerPaymentSummery("Type") = "ClassRoom"
                ''        drSummerPaymentSummery("ProductID") = chkClassRoomTxt(1)
                ''        If WhoPaysForElevationStudent(CInt(chkClassRoomTxt(1)), ddlRoute.SelectedValue, chkClassRoomTxt(0)) Then
                ''            drSummerPaymentSummery("WhoPay") = "Firm Pay"
                ''            drSummerPaymentSummery("IsProductPaymentPlan") = 0
                ''        Else
                ''            drSummerPaymentSummery("WhoPay") = "Member Pay"
                ''            ' Check Product Having Payment Plan
                ''            Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkClassRoomTxt(1))
                ''            Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                ''            If iProductPaymentPlan > 0 Then
                ''                drSummerPaymentSummery("IsProductPaymentPlan") = 1
                ''            Else
                ''                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                ''            End If
                ''        End If
                ''        drSummerPaymentSummery("ClassID") = chkClassRoomTxt(0)
                ''        Dim dClassRoomTax As Decimal
                ''        drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkClassRoomTxt(1)), chkClassRoomTxt(0), ddlCAP1StudGrp.SelectedValue, dClassRoomTax)
                ''        drSummerPaymentSummery("TaxAmount") = dClassRoomTax
                ''        drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                ''        drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                ''        drSummerPaymentSummery("Units") = lblUnits.Text
                ''        drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                ''        drSummerPaymentSummery("AcademicCycleID") = chkClassRoomTxt(6)
                ''        If lblAlternativeGroup.Text.Trim <> "" Then
                ''            drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                ''        End If
                ''        dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                ''        chkClassRoom.Checked = True
                ''    End If



                ''    If Convert.ToInt32(chkMockExamtxt(1)) > 0 AndAlso Convert.ToInt32(chkMockExamtxt(4)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(4)) <> 2 Then
                ''        Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                ''        drSummerPaymentSummery("Subject") = lblSubject.Text
                ''        drSummerPaymentSummery("Type") = "MockExam"
                ''        drSummerPaymentSummery("ProductID") = chkMockExamtxt(1)
                ''        If WhoPaysForElevationStudent(CInt(chkMockExamtxt(1)), ddlRoute.SelectedValue, chkMockExamtxt(0)) Then
                ''            drSummerPaymentSummery("WhoPay") = "Firm Pay"
                ''            drSummerPaymentSummery("IsProductPaymentPlan") = 0
                ''        Else
                ''            drSummerPaymentSummery("WhoPay") = "Member Pay"
                ''            ' Check Product Having Payment Plan
                ''            Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkMockExamtxt(1))
                ''            Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                ''            If iProductPaymentPlan > 0 Then
                ''                drSummerPaymentSummery("IsProductPaymentPlan") = 1
                ''            Else
                ''                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                ''            End If
                ''        End If
                ''        drSummerPaymentSummery("ClassID") = chkMockExamtxt(0)
                ''        Dim dMockExam As Decimal
                ''        drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkMockExamtxt(1)), chkMockExamtxt(0), ddlCAP1StudGrp.SelectedValue, dMockExam)
                ''        drSummerPaymentSummery("TaxAmount") = dMockExam
                ''        drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                ''        drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                ''        drSummerPaymentSummery("Units") = lblUnits.Text
                ''        drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                ''        drSummerPaymentSummery("AcademicCycleID") = chkMockExamtxt(6)
                ''        If lblAlternativeGroup.Text.Trim <> "" Then
                ''            drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                ''        End If
                ''        dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                ''        chkMockExam.Checked = True

                ''    End If
                ''    If Convert.ToInt32(chkInterimAssessmenttxt(1)) > 0 AndAlso Convert.ToInt32(chkInterimAssessmenttxt(4)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(4)) <> 2 Then
                ''        Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                ''        drSummerPaymentSummery("Subject") = lblSubject.Text
                ''        drSummerPaymentSummery("Type") = "Interim Assessment"
                ''        drSummerPaymentSummery("ProductID") = chkInterimAssessmenttxt(1)
                ''        If WhoPaysForElevationStudent(CInt(chkInterimAssessmenttxt(1)), ddlRoute.SelectedValue, chkInterimAssessmenttxt(0)) Then
                ''            drSummerPaymentSummery("WhoPay") = "Firm Pay"
                ''            drSummerPaymentSummery("IsProductPaymentPlan") = 0
                ''        Else
                ''            drSummerPaymentSummery("WhoPay") = "Member Pay"
                ''            ' Check Product Having Payment Plan
                ''            Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkInterimAssessmenttxt(1))
                ''            Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                ''            If iProductPaymentPlan > 0 Then
                ''                drSummerPaymentSummery("IsProductPaymentPlan") = 1
                ''            Else
                ''                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                ''            End If
                ''        End If
                ''        drSummerPaymentSummery("ClassID") = chkInterimAssessmenttxt(0)
                ''        Dim dInterimAssTax As Decimal
                ''        drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkInterimAssessmenttxt(1)), chkInterimAssessmenttxt(0), ddlCAP1StudGrp.SelectedValue, dInterimAssTax)
                ''        drSummerPaymentSummery("TaxAmount") = dInterimAssTax
                ''        drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                ''        drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                ''        drSummerPaymentSummery("Units") = lblUnits.Text
                ''        drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                ''        drSummerPaymentSummery("AcademicCycleID") = chkInterimAssessmenttxt(6)
                ''        If lblAlternativeGroup.Text.Trim <> "" Then
                ''            drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                ''        End If
                ''        dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                ''        chkInterimAssessment.Checked = True
                ''    End If

                ''End If
            Else
                If Convert.ToInt32(chkSummerExamtxt(1)) > 0 Then
                    If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkSummerExamtxt(1)))
                        SummerPaymentSummery.Rows.Remove(dr(0))
                        SummerPaymentSummery.AcceptChanges()
                    End If
                End If
                ' Added by Govind M for Redmine #15954
                If chkInterimAssessment.Checked AndAlso chkInterimAssessment.Enabled Then
                    chkInterimAssessment.Checked = False
                End If
                'Added as part of 20944
                If chkRevision.Checked Then
                    chkRevision.Checked = False
                End If
                If chkRAutumnExamTxt(0) > 0 OrElse chkRepeatRevisionTxt(0) > 0 OrElse chkResitInterimAssessmentTxt(0) > 0 Then

                    If Convert.ToInt32(chkClassRoomTxt(1)) > 0 Then
                        ''Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkClassRoomTxt(1)))
                        ''SummerPaymentSummery.Rows.Remove(dr(0))
                        ''SummerPaymentSummery.AcceptChanges()
                        chkClassRoom.Checked = False
                    End If
                    If Convert.ToInt32(chkMockExamtxt(1)) > 0 Then
                        ''Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkMockExamtxt(1)))
                        ''If dr.Length > 0 Then
                        ''    SummerPaymentSummery.Rows.Remove(dr(0))
                        ''    SummerPaymentSummery.AcceptChanges()
                        ''    chkMockExam.Checked = False
                        ''End If
                        chkMockExam.Checked = False
                    End If
                    If Convert.ToInt32(chkInterimAssessmenttxt(1)) > 0 Then
                        ''Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(chkInterimAssessmenttxt(1)))
                        ''SummerPaymentSummery.Rows.Remove(dr(0))
                        ''SummerPaymentSummery.AcceptChanges()
                        chkInterimAssessment.Checked = False
                    End If
                End If
            End If
            ''Dim dicSum As New Dictionary(Of String, Decimal)()
            ''For Each row As DataRow In SummerPaymentSummery.Rows
            ''    Dim group As String = row("Subject").ToString()
            ''    Dim rate As Decimal = CDec(Convert.ToString(row("Price")).Substring(1, Convert.ToString(row("Price")).Length - 1))
            ''    If dicSum.ContainsKey(group) Then
            ''        dicSum(group) += rate
            ''    Else
            ''        dicSum.Add(group, rate)
            ''    End If
            ''Next
            ''GV.DataSource = dicSum
            ''GV.DataBind()
            ''Dim sProductID As String = String.Empty
            ''If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
            ''    radSummerPaymentSummery.DataSource = SummerPaymentSummery
            ''    radSummerPaymentSummery.DataBind()
            ''    ' 'radSummerPaymentSummery.Visible = True
            ''    lblAmount.Visible = True
            ''    lblTotalAmount.Visible = True
            ''    Dim dAmt As Decimal = 0
            ''    lblStudentPaidLabel.Visible = True
            ''    lblAmountPaidStudent.Visible = True
            ''    lblFirmPaidLabel.Visible = True
            ''    lblAmountPaidFirm.Visible = True
            ''    lblCurrency.Visible = True
            ''    txtIntialAmount.Visible = True
            ''    lblCurrency.Visible = True
            ''    lblIntialAmt.Visible = True

            ''    Dim dAmountPaidByStudent As Decimal = 0
            ''    Dim dAmountPaidByFirm As Decimal = 0
            ''    Dim dPaymentPlanAmount As Decimal = 0
            ''    Dim dTaxAmount As Decimal = 0
            ''    For Each dr As DataRow In SummerPaymentSummery.Rows
            ''        If dAmt = 0 Then
            ''            dAmt = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
            ''            If Convert.ToString(dr("WhoPay")).Trim.ToLower = "member pay" Then
            ''                dAmountPaidByStudent = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
            ''            ElseIf Convert.ToString(dr("WhoPay")).Trim.ToLower = "firm pay" Then
            ''                dAmountPaidByFirm = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
            ''            End If

            ''        Else
            ''            dAmt = Convert.ToDecimal(dAmt + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
            ''            If Convert.ToString(dr("WhoPay")).Trim.ToLower = "member pay" Then
            ''                dAmountPaidByStudent = Convert.ToDecimal(dAmountPaidByStudent + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
            ''            ElseIf Convert.ToString(dr("WhoPay")).Trim.ToLower = "firm pay" Then
            ''                dAmountPaidByFirm = Convert.ToDecimal(dAmountPaidByFirm + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
            ''            End If
            ''        End If
            ''        If dr("IsProductPaymentPlan") = 1 Then
            ''            If sProductID = "" Then
            ''                sProductID = dr("ProductID")
            ''            Else
            ''                sProductID = sProductID + "," + dr("ProductID")
            ''            End If
            ''            If dPaymentPlanAmount = 0 Then
            ''                dPaymentPlanAmount = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
            ''            Else
            ''                dPaymentPlanAmount = Convert.ToDecimal(dPaymentPlanAmount + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
            ''            End If
            ''        End If
            ''        If dTaxAmount = 0 Then
            ''            dTaxAmount = CDec(dr("TaxAmount"))
            ''        Else
            ''            dTaxAmount = dTaxAmount + CDec(dr("TaxAmount"))
            ''        End If

            ''    Next
            ''    lblStagePaymentTotal.Text = ViewState("CurrencyTypeID") & Format(CDec(dPaymentPlanAmount), "0.00")
            ''    lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
            ''    lblAmountPaidStudent.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmountPaidByStudent), "0.00")
            ''    lblAmountPaidFirm.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmountPaidByFirm), "0.00")
            ''    lblTaxAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dTaxAmount), "0.00")
            ''    Dim dtPaymentPlan As DataTable = DirectCast(ViewState("SelectedPaymentPlan"), DataTable)
            ''    If Not dtPaymentPlan Is Nothing AndAlso dtPaymentPlan.Rows.Count > 0 Then
            ''        For Each drP As DataRow In dtPaymentPlan.Rows
            ''            Dim dStageAmt As Decimal = CDec(Convert.ToString(lblStagePaymentTotal.Text).Substring(1, Convert.ToString(lblStagePaymentTotal.Text).Length - 1))
            ''            Dim dStudentPayAmt As Decimal = CDec(Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1))
            ''            Dim dPercentageAmt As Decimal = (dStageAmt * CDec(drP("Percentage"))) / 100
            ''            If CInt(drP("days")) = 0 Then
            ''                Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
            ''                txtIntialAmount.Text = Format(CDec(dintialStageAmount + dPercentageAmt), "0.00")
            ''            Else
            ''                Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
            ''                txtIntialAmount.Text = Format(CDec(dintialStageAmount), "0.00")
            ''            End If
            ''        Next
            ''        txtIntialAmount.Text = Format((CDec(txtIntialAmount.Text) + dTaxAmount), "0.00")
            ''    Else
            ''        Dim dTotalAmtPaidWithTax As Decimal = (dAmountPaidByStudent + dTaxAmount)
            ''        txtIntialAmount.Text = Format(dTotalAmtPaidWithTax, "0.00")
            ''        ' txtIntialAmount.Text = Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1).Trim
            ''    End If
            ''    lblPaymentSummery.Visible = True

            ''    If sProductID <> "" Then
            ''        LoadPaymentPlan(sProductID)
            ''        ddlPaymentPlan.Visible = True
            ''        lblPaymentPlan.Visible = True
            ''    Else
            ''        ddlPaymentPlan.Visible = False
            ''        lblPaymentPlan.Visible = False
            ''    End If
            ''    If dAmountPaidByStudent > 0 Then
            ''        CreditCard.Visible = True
            ''    Else
            ''        CreditCard.Visible = False
            ''        ddlPaymentPlan.Visible = False
            ''        lblPaymentPlan.Visible = False
            ''    End If
            ''Else
            ''    'radSummerPaymentSummery.Visible = False
            ''    lblAmount.Visible = False
            ''    lblTotalAmount.Visible = False
            ''    lblStudentPaidLabel.Visible = False
            ''    lblAmountPaidStudent.Visible = False
            ''    lblFirmPaidLabel.Visible = False
            ''    lblAmountPaidFirm.Visible = False
            ''    lblPaymentSummery.Visible = False
            ''    CreditCard.Visible = False
            ''    txtIntialAmount.Visible = False
            ''    lblCurrency.Visible = False
            ''    lblIntialAmt.Visible = False
            ''    lblTaxAmount.Visible = False
            ''    lblTax.Visible = False
            ''    ddlPaymentPlan.Visible = False
            ''    lblPaymentPlan.Visible = False

            ''End If
            ''UpdatePanelPayment.Update()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    Private Sub HideEnrollmentOnContractStudent()
        Try
            If (ViewState("RouteChanged") IsNot Nothing AndAlso Convert.ToBoolean(ViewState("RouteChanged"))) Then
                Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
                If EducationContract.Trim.ToLower = "contract" OrElse (EducationContract.Trim.ToLower = "elevation" AndAlso chkInfoToFirm.Checked) Then
                    If EducationContract.Trim.ToLower = "elevation" AndAlso CBool(ViewState("ShareMyInfoWithFirm")) = chkInfoToFirm.Checked Then
                        ShowEnrollmentDetails()
                    Else
                        ' Code made chnages by Govind M for redmine issue #13300 (Comment the code)
                        '  HideEnrollmentDetails()
                    End If

                Else
                    ShowEnrollmentDetails()
                End If
                ' Code made chnages by Govind M for redmine issue #13300 (added And Condition)
                If ViewState("OldCompanyID") <> hdnCompanyId.Value And EducationContract.Trim.ToLower <> "flexible option" Then
                    HideEnrollmentDetails()
                Else
                    ShowEnrollmentDetails()
                End If
                ''UpdatePanelPayment.Update()
                ''UpdatePanel1.Update()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub ShowEnrollmentDetails()
        gvCurriculumCourse.Visible = True
        lblEnrolmsg.Visible = True
        ddlCAP1StudGrp.Visible = True
        lblStudentGroup.Visible = True
        lblStdgrp.Visible = True
        lblEnrollmentMsg.Visible = False
        lblNoRecords.Visible = False
        ''CreditCard.Visible = True
        'lblCurrentAcademicCycle.Visible = True
        'lblCurrentAcademicCycleName.Visible = True
        'lblNextAcademicCycle.Visible = True
        ' lblNextAcademicCycleName.Visible = True

        ''If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
        ''    lblPaymentSummery.Visible = True
        ''    'radSummerPaymentSummery.Visible = True
        ''    lblAmount.Visible = True
        ''    lblTotalAmount.Visible = True
        ''    lblStagePaymentTotal.Visible = True
        ''    lblStudentPaidLabel.Visible = True
        ''    lblAmountPaidStudent.Visible = True
        ''    lblFirmPaidLabel.Visible = True
        ''    lblAmountPaidFirm.Visible = True
        ''    lblIntialAmt.Visible = True
        ''    lblPaymentPlan.Visible = True
        ''    ddlPaymentPlan.Visible = True
        ''    radPaymentPlanDetails.Visible = True
        ''    txtIntialAmount.Visible = True
        ''    lblCurrency.Visible = True
        ''    lblTaxAmount.Visible = True
        ''    lblTax.Visible = True
        ''Else
        ''    lblPaymentSummery.Visible = False
        ''    radSummerPaymentSummery.Visible = False
        ''    lblAmount.Visible = False
        ''    lblTotalAmount.Visible = False
        ''    lblStagePaymentTotal.Visible = False
        ''    lblStudentPaidLabel.Visible = False
        ''    lblAmountPaidStudent.Visible = False
        ''    lblFirmPaidLabel.Visible = False
        ''    lblAmountPaidFirm.Visible = False
        ''    lblIntialAmt.Visible = False
        ''    lblPaymentPlan.Visible = False
        ''    ddlPaymentPlan.Visible = False
        ''    radPaymentPlanDetails.Visible = False
        ''    txtIntialAmount.Visible = False
        ''    lblCurrency.Visible = False
        ''    lblTaxAmount.Visible = False
        ''    lblTax.Visible = False
        ''End If
    End Sub

    Private Sub HideEnrollmentDetails()
        gvCurriculumCourse.Visible = False
        lblEnrolmsg.Visible = False
        ddlCAP1StudGrp.Visible = False
        lblStudentGroup.Visible = False
        lblStdgrp.Visible = False
        lblEnrollmentMsg.Visible = False
        'Susan Wong, #20033, Hide NoEnrollment culture string on lblEnrollmentMsg until next year
        'lblNoRecords.Visible = True
        'lblNoRecords.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.NoEnrollment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        'lblCurrentAcademicCycle.Visible = False
        ' lblCurrentAcademicCycleName.Visible = False
        'lblNextAcademicCycle.Visible = False
        ' lblNextAcademicCycleName.Visible = False
        lblPaymentSummery.Visible = False
        radSummerPaymentSummery.Visible = False
        lblAmount.Visible = False
        lblTotalAmount.Visible = False
        lblStagePaymentTotal.Visible = False
        lblStudentPaidLabel.Visible = False
        lblAmountPaidStudent.Visible = False
        lblFirmPaidLabel.Visible = False
        lblAmountPaidFirm.Visible = False
        lblIntialAmt.Visible = False
        CreditCard.Visible = False
        lblPaymentPlan.Visible = False
        ddlPaymentPlan.Visible = False
        radPaymentPlanDetails.Visible = False
        txtIntialAmount.Visible = False
        lblCurrency.Visible = False
        lblTaxAmount.Visible = False
        lblTax.Visible = False
        btnDisplayPaymentSummey.Text = "Save"
        btnDisplayPaymentSummey.CausesValidation = False
    End Sub

#Region "Private Functions"
    ''' <summary>
    ''' Create Payment Summery for Summer Exam and Summer Interim Exam
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SummerPaymentSummery() As DataTable
        Get

            If Not ViewState("SummerPaymentSummery") Is Nothing Then
                Return CType(ViewState("SummerPaymentSummery"), DataTable)
            Else
                Dim dtSummerPaymentSummery As New DataTable
                dtSummerPaymentSummery.Columns.Add("Subject")
                dtSummerPaymentSummery.Columns.Add("ClassID")
                dtSummerPaymentSummery.Columns.Add("Type")
                dtSummerPaymentSummery.Columns.Add("ProductID")
                dtSummerPaymentSummery.Columns.Add("WhoPay")
                dtSummerPaymentSummery.Columns.Add("Price")
                dtSummerPaymentSummery.Columns.Add("CurriculumID")
                dtSummerPaymentSummery.Columns.Add("MinimumUnits")
                dtSummerPaymentSummery.Columns.Add("Units")
                dtSummerPaymentSummery.Columns.Add("CutOffUnits")
                dtSummerPaymentSummery.Columns.Add("IsProductPaymentPlan")
                dtSummerPaymentSummery.Columns.Add("AcademicCycleID")
                dtSummerPaymentSummery.Columns.Add("TaxAmount")
                dtSummerPaymentSummery.Columns.Add("AlternateTimeTable")
                dtSummerPaymentSummery.Columns.Add("FirstAttempt")
                dtSummerPaymentSummery.Columns.Add("Isfailed")
                dtSummerPaymentSummery.Columns.Add("FailedUnits")
                dtSummerPaymentSummery.Columns.Add("SessionType")
                Return dtSummerPaymentSummery
            End If
        End Get
        Set(ByVal value As DataTable)
            ViewState("SummerPaymentSummery") = value
        End Set
    End Property

    Public Property FirmPaySummery() As DataTable
        Get

            If Not ViewState("FirmPaySummery") Is Nothing Then
                Return CType(ViewState("FirmPaySummery"), DataTable)
            Else
                Dim dtFirmPaySummery As New DataTable
                dtFirmPaySummery.Columns.Add("Subject")
                dtFirmPaySummery.Columns.Add("ClassID")
                dtFirmPaySummery.Columns.Add("Type")
                dtFirmPaySummery.Columns.Add("ProductID")
                dtFirmPaySummery.Columns.Add("WhoPay")
                dtFirmPaySummery.Columns.Add("Price")
                dtFirmPaySummery.Columns.Add("IsProductPaymentPlan")
                dtFirmPaySummery.Columns.Add("AcademicCycleID")
                dtFirmPaySummery.Columns.Add("AlternateTimeTable")
                Return dtFirmPaySummery
            End If
        End Get
        Set(ByVal value As DataTable)
            ViewState("FirmPaySummery") = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' Create a order 
    ''' </summary>
    ''' <param name="lOrderID"></param>
    ''' <param name="sTransID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CreateOrder(ByRef lOrderID As Long) As Long
        Try
            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
            Dim sError As String = Nothing
            oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
            oOrder.ShipToID = AptifyEbusinessUser1.PersonID
            oOrder.BillToID = AptifyEbusinessUser1.PersonID
            oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
            oOrder.SetValue("BillToSameAsShipTo", 1)
            oOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
            Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
            Dim dTaxAmount As Decimal = 0
            For Each row As Telerik.Web.UI.GridItem In radSummerPaymentSummery.Items
                Dim lblSubject As Label = DirectCast(row.FindControl("lblSubject"), Label)
                Dim lblPaymentProductID As Label = DirectCast(row.FindControl("lblPaymentProductID"), Label)
                Dim lblClassID As Label = DirectCast(row.FindControl("lblClassID"), Label)
                Dim lblType As Label = DirectCast(row.FindControl("lblType"), Label)
                Dim lblPaymentPrice As Label = DirectCast(row.FindControl("lblPaymentPrice"), Label)
                Dim lblAcademicCycleID As Label = DirectCast(row.FindControl("lblAcademicCycleID"), Label)
                Dim lblTaxAmount As Label = DirectCast(row.FindControl("lblTaxAmount"), Label)
                Dim lblAlternateTimeTableOnOrder As Label = DirectCast(row.FindControl("lblAlternateTimeTableOnOrder"), Label)
                If dTaxAmount = 0 Then
                    dTaxAmount = CDec(lblTaxAmount.Text)
                Else
                    dTaxAmount = dTaxAmount + CDec(lblTaxAmount.Text)
                End If
                ' Check Is Firm Pay 
                Dim lblWhoPay As Label = DirectCast(row.FindControl("lblWhoPay"), Label)
                If lblWhoPay.Text.Trim.ToLower = "firm pay" Then
                    ' if firm pay then add data in new data table
                    Dim dtFirmPaySummery As DataTable = FirmPaySummery

                    Dim drFirmPaySummery As DataRow = dtFirmPaySummery.NewRow()
                    drFirmPaySummery("Subject") = lblSubject.Text
                    drFirmPaySummery("Type") = lblType.Text
                    drFirmPaySummery("ProductID") = lblPaymentProductID.Text
                    drFirmPaySummery("WhoPay") = lblWhoPay.Text
                    drFirmPaySummery("ClassID") = lblClassID.Text
                    drFirmPaySummery("Price") = lblPaymentPrice.Text
                    drFirmPaySummery("IsProductPaymentPlan") = 1
                    drFirmPaySummery("AcademicCycleID") = lblAcademicCycleID.Text
                    If lblAlternateTimeTableOnOrder.Text.Trim <> "" Then
                        drFirmPaySummery("AlternateTimeTable") = lblAlternateTimeTableOnOrder.Text.Trim
                    End If
                    dtFirmPaySummery.Rows.Add(drFirmPaySummery)
                    FirmPaySummery = dtFirmPaySummery
                Else
                    oOrder.AddProduct(Convert.ToInt32(lblPaymentProductID.Text), 1)
                    oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                    With oOrderLine
                        .ExtendedOrderDetailEntity.SetValue("ClassID", lblClassID.Text)
                        .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                        .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(lblAcademicCycleID.Text))
                        If (lblType.Text.Trim.ToLower = "summer exam" OrElse lblType.Text.Trim.ToLower = "autumn exam") OrElse (lblType.Text.Trim.ToLower = "interim assessment" OrElse lblType.Text.Trim.ToLower = "resit interim assessment") Then
                            '  .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                            If lblAlternateTimeTableOnOrder.Text.Trim <> "" Then
                                Dim sSqlStudentGrp As String = Database & "..spGetExamInterimStudentGrp__c @StudentGrpID=" & AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", lblAlternateTimeTableOnOrder.Text.Trim)
                                Dim iExamIterimStudentGrp As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If iExamIterimStudentGrp > 0 Then
                                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", iExamIterimStudentGrp)
                                Else
                                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                                End If
                            Else
                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                            End If

                        Else
                            If lblAlternateTimeTableOnOrder.Text.Trim <> "" Then
                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", lblAlternateTimeTableOnOrder.Text.Trim))

                            Else
                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ddlCAP1StudGrp.SelectedValue)
                            End If
                        End If
                        .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", ddlRoute.SelectedValue)
                        .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                    End With
                    oOrderLine.SetProductPrice()
                End If
            Next
            ' check stage Payment
            If oOrder.SubTypes("OrderLines").Count > 0 Then
                If ddlPaymentPlan.SelectedValue <> "Select Payment Plan" AndAlso Not ViewState("SelectedPaymentPlan") Is Nothing Then
                    Dim oOrderPayInfo As PaymentInformation
                    Dim sErrorString As String = ""
                    Dim RefTranse As String = ""

                    Dim sLastError As String = ""
                    Dim oPersonGE As Aptify.Applications.Persons.PersonsEntity = DirectCast(Me.AptifyApplication.GetEntityObject("Persons", AptifyEbusinessUser1.PersonID), Aptify.Applications.Persons.PersonsEntity)

                    Dim cmbSavedPaymentMethod As DropDownList = DirectCast(CreditCard.FindControl("cmbSavedPaymentMethod"), DropDownList)
                    If cmbSavedPaymentMethod.SelectedValue <> "" Then
                        With oPersonGE.SubTypes("PersonSavedPaymentMethods").Find("ID", cmbSavedPaymentMethod.SelectedValue)
                            oOrder.SetValue("SavedPaymentMethodID__c", cmbSavedPaymentMethod.SelectedValue)
                            oOrder.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                            oOrder.SetValue("InitialPaymentAmount", txtIntialAmount.Text)
                        End With
                    Else
                        ' Create new Save payment Method
                        With oPersonGE.SubTypes("PersonSavedPaymentMethods").Add()
                            .SetValue("PersonID", AptifyEbusinessUser1.PersonID)
                            .SetValue("StartDate", Today.Date)
                            .SetValue("IsActive", 1)
                            .SetValue("Name", "SPM " & Today.Date)
                            .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                            .SetValue("CCAccountNumber", CreditCard.CCNumber)
                            .SetValue("CCExpireDate", CreditCard.CCExpireDate)
                            .SetValue("EndDate", CreditCard.CCExpireDate)
                            If .Fields("PaymentInformationID").EmbeddedObjectExists Then
                                oOrderPayInfo = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                                If oOrderPayInfo IsNot Nothing Then
                                    oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCNumber
                                    .SetValue("CCPartial", oOrderPayInfo.GetCCPartial(CStr(CreditCard.CCNumber)))
                                End If
                            End If

                        End With
                    End If
                    Dim sPersonSaveError As String = String.Empty
                    If oPersonGE.Save(False, sPersonSaveError) Then
                        oOrder.SetValue("SavedPaymentMethodID__c", oPersonGE.SubTypes("PersonSavedPaymentMethods").Item(oPersonGE.SubTypes("PersonSavedPaymentMethods").Count - 1).RecordID)
                        oOrder.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                        oOrder.SetValue("InitialPaymentAmount", (txtIntialAmount.Text))
                        oOrder.SetValue("OrderTypeID", Aptify.Applications.OrderEntry.OrderType.Regular)   ' Regular Order 
                        oOrder.OrderStatus = OrderStatus.Taken
                        oOrder.PayTypeID = AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order")
                        oOrder.PONumber = "Stage Payment"
                    End If
                Else
                    oOrder.SetValue("InitialPaymentAmount", txtIntialAmount.Text)
                    oOrder.PayTypeID = AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order")
                    oOrder.PONumber = "Normal Payment"
                End If
                ' oOrder.SetAddValue("_xBypassCreditCheck", 1) 'bypass credit check

                If Request.QueryString("Eid") IsNot Nothing AndAlso Convert.ToInt32(Request.QueryString("Eid")) > 0 Then
                    oOrder.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(Request.QueryString("Eid")))
                End If
                If Not oOrder.Save(False, sError) Then
                    lblMessage.Visible = True
                    lblMessage.Text = "<ui><li>" + sError + "</li></ui>"
                    If lblMessage.Text.Trim.ToLower.Contains("there must be at least one order line per") Then
                        lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalPage.NoSelection")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                    lblMessage.Visible = True
                    lOrderID = -1
                Else
                    lOrderID = oOrder.RecordID
                    If ddlPaymentPlan.SelectedValue <> "Select Payment Plan" AndAlso Not ViewState("SelectedPaymentPlan") Is Nothing Then
                    Else
                        PostPayment(lOrderID, dTaxAmount)
                    End If
                End If
            Else
                ' for firm pay retun true
                lOrderID = 1
            End If

            Return lOrderID
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return -1
        End Try
    End Function

    Private Sub PostPayment(ByVal OrderID As Long, ByVal TaxAmount As Decimal)
        Try
            Dim oPayment As AptifyGenericEntityBase

            Dim bFlag As Boolean = False
            Dim bNotification As Boolean = False
            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity = Nothing
            Dim sql As String = "spGetPaymentLevelID__c"
            Dim PaymentLevelID As Integer
            PaymentLevelID = Convert.ToInt16(DataAction.ExecuteScalar(sql, IAptifyDataAction.DSLCacheSetting.BypassCache))

            oPayment = AptifyApplication.GetEntityObject("Payments", -1)
            With oPayment
                .SetValue("PaymentLevelID", PaymentLevelID)
                .SetValue("EmployeeID", EBusinessGlobal.WebEmployeeID(Page.Application))
                .SetValue("PersonID", AptifyEbusinessUser1.PersonID)
                .SetValue("PaymentDate", Date.Today)
                .SetValue("DepositDate", Date.Today)
                .SetValue("EffectiveDate", Date.Today)
                .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                If CreditCard.CCNumber = "-Ref Transaction-" Then
                    .SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                    .SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                End If
                .SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber)
                Dim sSql As String = Database & "..spGetOrderDetailsForPayments__c @OrderID=" & OrderID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        With .SubTypes("PaymentLines").Add()
                            .SetValue("AppliesTo", "Order Line")
                            .SetValue("OrderID", dr("OrderID"))
                            .SetValue("OrderDetailID", dr("OrderDetailID"))
                            .SetValue("LineNumber", dr("Sequence"))
                            .SetValue("Amount", dr("Amount"))
                            .SetValue("BillToPersonID", AptifyEbusinessUser1.PersonID)
                        End With
                    Next
                    ' For Tax Payment 
                    If TaxAmount > 0 Then
                        With .SubTypes("PaymentLines").Add()
                            .SetValue("AppliesTo", "Tax")
                            .SetValue("OrderID", OrderID)
                            .SetValue("Amount", TaxAmount)
                            .SetValue("BillToPersonID", AptifyEbusinessUser1.PersonID)
                        End With
                    End If


                End If
                .SetValue("CCAccountNumber", CreditCard.CCNumber)
                .SetValue("CCExpireDate", CreditCard.CCExpireDate)
                If .Fields("PaymentInformationID").EmbeddedObjectExists Then
                    Dim payInformation As PaymentInformation = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                    payInformation.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                    payInformation.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                    payInformation.SetValue("CCPartial", CreditCard.CCPartial)
                End If
                Dim errorMessage As String = String.Empty
                If .Save(False, errorMessage) Then
                    'ViewState("AddedTaxAmount") 
                End If
            End With


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    'Check weather Payment type is reference transaction or not
    Private Function CheckForReferenceTransaction(ByRef lPaymentTypeID As Long) As Boolean
        Try
            Dim sSQL, sPaymentType As String
            sSQL = Database & "..spGetPayType @ID = " & lPaymentTypeID
            sPaymentType = CStr(DataAction.ExecuteScalar(sSQL))
            If sPaymentType = "Credit Card Reference Transaction" Then
                Return True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return False
    End Function

    Private Sub SetTransactionNumber(ByVal oGESPM As Aptify.Applications.Persons.PersonsEntity, ByRef RefTranse As String, ByRef RefExpirDate As Date)
        Dim bAllowGUI As Boolean = True
        Dim sErrorString As String = ""
        Dim ReferenceTransactionNumber As String = ""
        Dim ReferenceExpiration As Date
        Dim bSaveResult As Boolean = True
        Dim oApp As New Aptify.Framework.Application.AptifyApplication
        If PaymentsEntity.GetZDAReferenceTransactionDetails(oApp, _
                                                             bAllowGUI, _
                                                             sErrorString, _
                                                             " ", _
                                                             CLng(CreditCard.PaymentTypeID), _
                                                             AptifyEbusinessUser1.PersonID, _
                                                             AptifyEbusinessUser1.CompanyID, _
                                                             CLng(oGESPM.GetValue("PaymentInformationID")), _
                                                             CreditCard.CCNumber, _
                                                             CDate(oGESPM.GetValue("CCExpireDate")), _
                                                             Convert.ToString(oGESPM.GetValue("CCPartial")), _
                                                             ReferenceTransactionNumber, _
                                                             ReferenceExpiration) Then
        End If
        'Anil B Issue 10254 on 06/05/2013
        'Return reference transaction no. and reference expiration date
        RefTranse = ReferenceTransactionNumber
        ReferenceExpiration = RefExpirDate
    End Sub
    ' Create as Firm Pay Order
    Private Sub FirmPayOrder()
        Try
            If Not FirmPaySummery Is Nothing AndAlso FirmPaySummery.Rows.Count > 0 Then
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                Dim sError As String = Nothing
                oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                oOrder.ShipToID = AptifyEbusinessUser1.PersonID
                oOrder.BillToID = AptifyEbusinessUser1.PersonID
                oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
                oOrder.SetValue("BillToSameAsShipTo", 1)
                oOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
                Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
                For Each dr As DataRow In FirmPaySummery.Rows
                    oOrder.AddProduct(Convert.ToInt32(dr("ProductID")))
                    oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                    With oOrderLine
                        .ExtendedOrderDetailEntity.SetValue("ClassID", dr("ClassID"))
                        .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                        .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(dr("AcademicCycleID")))
                        If (Convert.ToString(dr("Type")).Trim.ToLower = "summer exam" OrElse Convert.ToString(dr("Type")).Trim.ToLower = "autumn exam") OrElse (Convert.ToString(dr("Type")).Trim.ToLower = "interim assessment" OrElse Convert.ToString(dr("Type")).Trim.ToLower = "resit interim assessment") Then
                            ' .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                            If Convert.ToString(dr("AlternateTimeTable")) <> "" Then
                                Dim sSqlStudentGrp As String = Database & "..spGetExamInterimStudentGrp__c @StudentGrpID=" & AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", Convert.ToString(dr("AlternateTimeTable")))
                                Dim iExamIterimStudentGrp As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If iExamIterimStudentGrp > 0 Then
                                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", iExamIterimStudentGrp)
                                Else
                                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                                End If
                            Else
                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                            End If

                        Else
                            '  .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ddlCAP1StudGrp.SelectedValue)
                            If Convert.ToString(dr("AlternateTimeTable")) <> "" Then
                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", Convert.ToString(dr("AlternateTimeTable"))))

                            Else
                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ddlCAP1StudGrp.SelectedValue)
                            End If
                        End If
                        .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", ddlRoute.SelectedValue)
                        .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                        'Added BY Pradip 2016-01-10 For Group 3 Tracker G3-55
                        ' .SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", txtFirm.Text.Trim))
                        If CheckApprovedEE() Then
                            .SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", txtFirm.Text.Trim))
                        Else
                            .SetValue("BillToCompanyID__c", -1)
                        End If
                    End With
                    oOrderLine.SetProductPrice()
                Next
                oOrder.SetValue("OrderTypeID", Aptify.Applications.OrderEntry.OrderType.Regular)   ' Regular Order 
                oOrder.OrderStatus = OrderStatus.Taken
                oOrder.PayTypeID = AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order")
                oOrder.PONumber = "Firm Pay Order"
                'oOrder.SetAddValue("_xBypassCreditCheck", 1) 'bypass credit check
                If Request.QueryString("Eid") IsNot Nothing AndAlso Convert.ToInt32(Request.QueryString("Eid")) > 0 Then
                    oOrder.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(Request.QueryString("Eid")))
                End If
                If Not oOrder.Save(False, sError) Then

                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

        End Try
    End Sub

    Private Sub ClearDataWithoutCore()
        Try
            If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                Dim dr1() As DataRow = SummerPaymentSummery.Select("ProductID>0")
                For i As Integer = 0 To dr1.Length - 1
                    SummerPaymentSummery.Rows.Remove(dr1(i))
                    SummerPaymentSummery.AcceptChanges()
                Next
                Dim sProductID As String
                If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                    radSummerPaymentSummery.DataSource = SummerPaymentSummery
                    radSummerPaymentSummery.DataBind()
                    'radSummerPaymentSummery.Visible = True
                    lblAmount.Visible = True
                    lblTotalAmount.Visible = True
                    Dim dAmt As Decimal = 0
                    lblStudentPaidLabel.Visible = True
                    lblAmountPaidStudent.Visible = True
                    lblFirmPaidLabel.Visible = True
                    lblAmountPaidFirm.Visible = True
                    lblCurrency.Visible = True
                    txtIntialAmount.Visible = True
                    lblCurrency.Visible = True
                    lblIntialAmt.Visible = True
                    Dim dAmountPaidByStudent As Decimal = 0
                    Dim dAmountPaidByFirm As Decimal = 0
                    Dim dPaymentPlanAmount As Decimal = 0
                    Dim dTaxAmount As Decimal = 0
                    For Each dr As DataRow In SummerPaymentSummery.Rows
                        If dAmt = 0 Then
                            dAmt = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                            If Convert.ToString(dr("WhoPay")).Trim.ToLower = "member pay" Then
                                dAmountPaidByStudent = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                            ElseIf Convert.ToString(dr("WhoPay")).Trim.ToLower = "firm pay" Then
                                dAmountPaidByFirm = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                            End If

                        Else
                            dAmt = Convert.ToDecimal(dAmt + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
                            If Convert.ToString(dr("WhoPay")).Trim.ToLower = "member pay" Then
                                dAmountPaidByStudent = Convert.ToDecimal(dAmountPaidByStudent + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
                            ElseIf Convert.ToString(dr("WhoPay")).Trim.ToLower = "firm pay" Then
                                dAmountPaidByFirm = Convert.ToDecimal(dAmountPaidByFirm + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
                            End If
                        End If
                        If dr("IsProductPaymentPlan") = 1 Then
                            If sProductID = "" Then
                                sProductID = dr("ProductID")
                            Else
                                sProductID = sProductID + "," + dr("ProductID")
                            End If
                            If dPaymentPlanAmount = 0 Then

                                dPaymentPlanAmount = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                            Else
                                dPaymentPlanAmount = Convert.ToDecimal(dPaymentPlanAmount + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
                            End If
                        End If
                        If dTaxAmount = 0 Then
                            dTaxAmount = CDec(dr("TaxAmount"))
                        Else
                            dTaxAmount = dTaxAmount + CDec(dr("TaxAmount"))
                        End If
                    Next
                    lblStagePaymentTotal.Text = ViewState("CurrencyTypeID") & Format(CDec(dPaymentPlanAmount), "0.00")
                    lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
                    lblAmountPaidStudent.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmountPaidByStudent), "0.00")
                    lblAmountPaidFirm.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmountPaidByFirm), "0.00")
                    Dim dDisplayTaxWithIntial As Decimal = dAmountPaidByStudent + dTaxAmount

                    txtIntialAmount.Text = Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1).Trim
                    lblTaxAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dTaxAmount), "0.00")
                    lblPaymentSummery.Visible = True
                    If sProductID <> "" Then
                        LoadPaymentPlan(sProductID)
                        ddlPaymentPlan.Visible = True
                        lblPaymentPlan.Visible = True

                    Else
                        ddlPaymentPlan.Visible = False
                        lblPaymentPlan.Visible = False
                    End If
                    If dAmountPaidByStudent > 0 Then
                        CreditCard.Visible = True
                    Else
                        CreditCard.Visible = False
                        ddlPaymentPlan.Visible = False
                        lblPaymentPlan.Visible = False
                    End If
                Else
                    radSummerPaymentSummery.Visible = False
                    lblAmount.Visible = False
                    lblTotalAmount.Visible = False
                    lblStudentPaidLabel.Visible = False
                    lblAmountPaidStudent.Visible = False
                    lblFirmPaidLabel.Visible = False
                    lblAmountPaidFirm.Visible = False
                    lblPaymentSummery.Visible = False
                    CreditCard.Visible = False
                    txtIntialAmount.Visible = False
                    lblCurrency.Visible = False
                    lblIntialAmt.Visible = False
                    lblTaxAmount.Visible = False
                    lblTax.Visible = False
                    ddlPaymentPlan.Visible = False
                    lblPaymentPlan.Visible = False
                End If
                UpdatePanelPayment.Update()
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

        End Try
    End Sub

    Private Sub FirmInfoTrueFromFalse()
        Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
        If CBool(ViewState("OldRTO")) = False AndAlso CBool(ViewState("NewRTO")) Then
            If EducationContract.Trim.ToLower = "elevation" Then
                HideEnrollmentDetails()
            End If
        ElseIf CBool(ViewState("OldRTO")) AndAlso CBool(ViewState("NewRTO")) = False Then
            If EducationContract.Trim.ToLower = "elevation" Then
                HideEnrollmentDetails()
            End If
        ElseIf CBool(ViewState("OldRTO")) AndAlso CBool(ViewState("NewRTO")) Then
            If EducationContract.Trim.ToLower = "elevation" Then
                HideEnrollmentDetails()
            End If
        End If
    End Sub

    Private Sub FirmInfoFalseFromTrue()
        Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
        If CBool(ViewState("OldRTO")) = False AndAlso CBool(ViewState("NewRTO")) Then
            If EducationContract.Trim.ToLower = "elevation" Then
                HideEnrollmentDetails()
            End If
        ElseIf CBool(ViewState("OldRTO")) AndAlso CBool(ViewState("NewRTO")) = False Then
            If EducationContract.Trim.ToLower = "elevation" Then
                HideEnrollmentDetails()
                btnSubmit.Visible = False
                lblEEMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ElevationRouteFirmChangeShareMyInfo")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '' UpdatePanelBtn.Update()
            End If
        ElseIf CBool(ViewState("OldRTO")) AndAlso CBool(ViewState("NewRTO")) Then
            If EducationContract.Trim.ToLower = "elevation" Then
                HideEnrollmentDetails()
                btnSubmit.Visible = False
                lblEEMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ElevationRouteFirmChangeShareMyInfo")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '' UpdatePanelBtn.Update()
            End If
        End If
    End Sub


    ''' <summary>
    ''' Added By Pradip 2016-01-10 to Check Approve EE For Person (For G3 Tracker G3-55)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckApprovedEE() As Boolean
        Try
            Dim oParams(0) As IDataParameter
            oParams(0) = Me.DataAction.GetDataParameter("@PersonId", SqlDbType.BigInt, AptifyEbusinessUser1.PersonID)
            Dim sSQL As String = Database & "..spCheckApprovedEE__c"
            Dim iCheck As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, oParams))
            If iCheck > 0 Then
                Return True
            Else
                Return False
            End If
            Return False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function
#End Region

#Region "Drop-Down Change"
    Protected Sub ddlPaymentPlan_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPaymentPlan.SelectedIndexChanged
        Try
            Dim iPaymentPlan As Integer = 0
            If ddlPaymentPlan.SelectedValue <> "Select Payment Plan" Then
                iPaymentPlan = ddlPaymentPlan.SelectedValue
            End If
            DisplayPaymentSchedule(iPaymentPlan)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub ddlCAP1StudGrp_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCAP1StudGrp.SelectedIndexChanged
        Try
            ' code added by Govind Mande 16 May 2016
            If ddlCAP1StudGrp.SelectedValue.Trim <> "Select Student Group" Then
                Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
                ''ClearDataWithoutCore()
                Dim oRuleEngine As Aptify.Consulting.RuleEngine__c = New Aptify.Consulting.RuleEngine__c(DataAction, AptifyApplication)
                Dim sRuleName As String = oRuleEngine.CheckRuleForStudent(ViewState("AcademicCycleID"), AptifyEbusinessUser1.PersonID)
                Dim sNextAcademicCycle As String = Database & "..spGetNextAcademicCycleStudentEnrollment__c @AcademicCycleID=" & ViewState("AcademicCycleID") & ",@StudentID=" & AptifyEbusinessUser1.PersonID & ",@RuleName='" & sRuleName & "'"
                Dim dtAcademicCycles As DataTable = DataAction.GetDataTable(sNextAcademicCycle, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If dtAcademicCycles IsNot Nothing AndAlso dtAcademicCycles.Rows.Count > 0 Then
                    If dtAcademicCycles.Rows.Count = 1 Then
                        ' lblNextAcademicCycleName.Text = dtAcademicCycles.Rows(0)("CalcName").ToString()
                        ViewState("NextAcademicCycle") = dtAcademicCycles.Rows(0)("CalcName").ToString()
                        ViewState("NextAcademicCycleID") = dtAcademicCycles.Rows(0)("ID").ToString()

                    Else
                        If Convert.ToInt32(dtAcademicCycles.Rows(0)("ExamSessionID")) = 1 Then
                            ViewState("NextAcademicCycle") = dtAcademicCycles.Rows(0)("CalcName").ToString()
                            ViewState("NextAcademicCycleID") = dtAcademicCycles.Rows(0)("ID").ToString()
                        ElseIf Convert.ToInt32(dtAcademicCycles.Rows(1)("ExamSessionID")) = 1 Then
                            ViewState("NextAcademicCycle") = dtAcademicCycles.Rows(1)("CalcName").ToString()
                            ViewState("NextAcademicCycleID") = dtAcademicCycles.Rows(1)("ID").ToString()


                        End If
                        If Convert.ToInt32(dtAcademicCycles.Rows(0)("ExamSessionID")) = 2 Then
                            ViewState("CurrentAcademicCycle") = dtAcademicCycles.Rows(0)("CalcName").ToString()
                            ViewState("AcademicCycleID") = dtAcademicCycles.Rows(0)("ID").ToString()
                        ElseIf Convert.ToInt32(dtAcademicCycles.Rows(1)("ExamSessionID")) = 2 Then
                            ViewState("CurrentAcademicCycle") = dtAcademicCycles.Rows(1)("CalcName").ToString()
                            ViewState("AcademicCycleID") = dtAcademicCycles.Rows(1)("ID").ToString()
                        End If
                    End If
                End If

                If Not ddlCAP1StudGrp Is Nothing AndAlso Not String.IsNullOrEmpty(ddlCAP1StudGrp.SelectedValue) Then
                    Dim sSql As String = Database & "..spGetStudentCourseEnrollmenByPerson__c @AcademicCycleID=" & ViewState("AcademicCycleID") & ",@StudentID=" & AptifyEbusinessUser1.PersonID & ",@StudentGroupID=" & ddlCAP1StudGrp.SelectedValue & ",@RuleName='" & sRuleName & "'"
                    _studentEnrollmentDetails = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                    ViewState("_studentEnrollmentDetails") = _studentEnrollmentDetails
                    If Not _studentEnrollmentDetails Is Nothing AndAlso _studentEnrollmentDetails.Rows.Count > 0 Then

                        gvCurriculumCourse.MasterTableView.GetColumn("IsFAEElective").Display = False
                        ''Added BY Pradip 2016-05-13 For Redmine Issue To Hide FAE Elective Column
                        Dim drFae() As DataRow = _studentEnrollmentDetails.Select("Curriculum='FAE- Final Admitting Exam'")
                        If Not drFae Is Nothing AndAlso drFae.Length > 0 Then
                            gvCurriculumCourse.MasterTableView.GetColumn("IsFAEElective").Display = True
                        End If

                        Dim dr() As DataRow = _studentEnrollmentDetails.Select("RuleName='AC1' OR RuleName='AC2'")
                        If Not dr Is Nothing AndAlso dr.Length > 0 Then
                            gvCurriculumCourse.Visible = False
                            lblEnrolmsg.Visible = False
                            txtIntialAmount.Visible = False
                            lblIntialAmt.Visible = False
                            lblCurrency.Visible = False
                            txtIntialAmount.Visible = False
                            ddlPaymentPlan.Visible = False
                            ddlCAP1StudGrp.Visible = False
                            lblStudentGroup.Visible = False
                            lblStdgrp.Visible = False
                            CreditCard.Visible = False
                            lblMsgWarning.Visible = True
                            'lblCurrentAcademicCycle.Visible = False
                            'lblCurrentAcademicCycleName.Visible = False
                            'lblNextAcademicCycle.Visible = False
                            ' lblNextAcademicCycleName.Visible = False
                            If Convert.ToString(dr(0)("RuleName")).Trim.ToLower = "ac1" Then
                                lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.AC1Msg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                btnDisplayPaymentSummey.Visible = False
                                lblEnrolmsg.Visible = False
                                lblMsgWarning.Visible = False
                                lblEnrollmentMsg.Visible = True
                            ElseIf Convert.ToString(dr(0)("RuleName")).Trim.ToLower = "ac2" Then
                                lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.AC2Msg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                lblEnrollmentMsg.Visible = True
                                btnDisplayPaymentSummey.Visible = False
                                lblEnrolmsg.Visible = False
                                lblMsgWarning.Visible = False
                            End If
                        Else
                            dvEnrolmentOptions.Visible = True
                            btnDisplayPaymentSummey.Visible = True
                            gvCurriculumCourse.DataSource = _studentEnrollmentDetails
                            gvCurriculumCourse.DataBind()
                            'redmine Issue #15351
                            If CBool(ViewState("CentrallyManaged")) Then
                                If ViewState("IsCentrallyManagedContractAutum") = "Yes" Then
                                    btnDisplayPaymentSummey.Visible = True
                                    gvCurriculumCourse.Visible = True
                                    lblEnrolmsg.Visible = True
                                Else
                                    btnDisplayPaymentSummey.Visible = False
                                    lblEnrolmsg.Visible = False
                                    lblCentrallyMngError.Visible = True
                                    If EducationContract.Trim.ToLower = "contract" Then
                                        gvCurriculumCourse.Visible = False
                                        lblMsgWarning.Visible = False
                                        lblEnrolmsg.Visible = False
                                    Else
                                        gvCurriculumCourse.Visible = True
                                        lblEnrolmsg.Visible = True
                                        lblMsgWarning.Visible = True
                                    End If
                                End If
                            Else
                                gvCurriculumCourse.Visible = True
                                lblEnrolmsg.Visible = True
                                lblCentrallyMngError.Visible = False
                                lblMsgWarning.Visible = True
                                If btnDisplayPaymentSummey.Visible = False Then 'Redmine #16489
                                Else
                                    btnDisplayPaymentSummey.Visible = True
                                End If
                            End If
                            ''txtIntialAmount.Visible = True
                            ''lblIntialAmt.Visible = True
                            ''txtIntialAmount.Visible = True
                            ''lblCurrency.Visible = True
                            ddlCAP1StudGrp.Visible = True
                            lblStdgrp.Visible = True
                            lblEnrollmentMsg.Text = ""
                            lblStudentGroup.Visible = True
                            ' lblMsgWarning.Visible = True
                            ''CreditCard.Visible = True
                            ' lblCurrentAcademicCycle.Visible = True
                            '  lblCurrentAcademicCycleName.Visible = True
                            ' lblNextAcademicCycle.Visible = True
                            '  lblNextAcademicCycleName.Visible = True
                            'Redmine Issue 13743
                            If Convert.ToInt32(ViewState("IsExternalUser")) = 1 Then
                                btnDisplayPaymentSummey.Visible = False
                                lblEnrolmsg.Visible = False
                            End If
                        End If
                    Else
                        lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ClosedEnrollment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        lblEnrollmentMsg.Visible = True
                    End If

                    ' Get Student Group for Interim and Exam Courses
                    Dim sSqlStudentGrp As String = Database & "..spGetExamInterimStudentGrp__c @StudentGrpID=" & ddlCAP1StudGrp.SelectedValue
                    Dim iExamIterimStudentGrp As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If iExamIterimStudentGrp > 0 Then
                        ViewState("ExamInterimStudentGrp") = iExamIterimStudentGrp
                    End If
                    'CheckFirmCentrallyManaged()
                Else
                    lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ClosedEnrollment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblEnrollmentMsg.Visible = True
                    lblMessage.ForeColor = Color.Red
                End If
            Else
                gvCurriculumCourse.Visible = False
                lblEnrolmsg.Visible = False
                txtIntialAmount.Visible = False
                lblIntialAmt.Visible = False
                lblCurrency.Visible = False
                txtIntialAmount.Visible = False
                ddlPaymentPlan.Visible = False
                'ddlCAP1StudGrp.Visible = False
                lblStudentGroup.Visible = False
                CreditCard.Visible = False
                'lblStudentGrpMessage.Visible = True
                lblMsgWarning.Visible = False
                '  lblCurrentAcademicCycle.Visible = False
                'lblCurrentAcademicCycleName.Visible = False
                'lblNextAcademicCycle.Visible = False
                'lblNextAcademicCycleName.Visible = False
                'Susan Wong, #20033, Hide NoEnrollment culture string on lblEnrollmentMsg until next year
                'lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.NoEnrollment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

#Region "Payment Plans"
    Private Function ShowPaymentPlan() As Boolean
        Try
            ' Check Elevation Student or not 
            If ddlRoute.SelectedItem.ToString().Trim.ToLower = "elevation" Then

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function

    Private Sub DisplayPaymentSchedule(ByVal PaymentPlanID As Integer)
        Dim sSql As String = Database & "..spGetPaymentPlansDetailsAsPerPlan__c @PaymentPlanID=" & PaymentPlanID
        Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            radPaymentPlanDetails.Visible = True
            radPaymentPlanDetails.DataSource = dt
            radPaymentPlanDetails.DataBind()
            ViewState("SelectedPaymentPlan") = dt
        Else
            radPaymentPlanDetails.Visible = False
            ViewState("SelectedPaymentPlan") = Nothing
            UpdatePanelPayment.Update()
        End If

        Dim dtPaymentPlan As DataTable = DirectCast(ViewState("SelectedPaymentPlan"), DataTable)
        If Not dtPaymentPlan Is Nothing AndAlso dtPaymentPlan.Rows.Count > 0 Then
            For Each drP As DataRow In dtPaymentPlan.Rows
                Dim dStageAmt As Decimal = CDec(Convert.ToString(lblStagePaymentTotal.Text).Substring(1, Convert.ToString(lblStagePaymentTotal.Text).Length - 1))
                Dim dStudentPayAmt As Decimal = CDec(Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1))
                Dim dPercentageAmt As Decimal = (dStageAmt * CDec(drP("Percentage"))) / 100
                If CInt(drP("days")) = 0 Then
                    Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                    txtIntialAmount.Text = Format(CDec(dintialStageAmount + dPercentageAmt), "0.00")
                Else
                    Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                    txtIntialAmount.Text = Format(CDec(dintialStageAmount), "0.00")
                End If
                Exit For
            Next

            Dim taxAmt As Decimal = Convert.ToString(lblTaxAmount.Text).Substring(1, Convert.ToString(lblTaxAmount.Text).Length - 1).Trim
            txtIntialAmount.Text = Format((CDec(txtIntialAmount.Text) + taxAmt), "0.00")
        Else
            Dim dAmtPaidByStudent As Decimal = CDec(Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1).Trim)
            Dim taxAmt As Decimal = Convert.ToString(lblTaxAmount.Text).Substring(1, Convert.ToString(lblTaxAmount.Text).Length - 1).Trim

            txtIntialAmount.Text = dAmtPaidByStudent + taxAmt

            'Dim taxAmt As Decimal = Convert.ToString(lblTaxAmount.Text).Substring(1, Convert.ToString(lblTaxAmount.Text).Length - 1).Trim
            'txtIntialAmount.Text = Format((CDec(txtIntialAmount.Text) + taxAmt), "0.00")
            ' txtIntialAmount.Text = Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1).Trim
        End If
    End Sub
#End Region

#Region "Public Function"
    ''' <summary>
    ''' To set the color to the cell template div according to color codes
    ''' </summary>
    ''' 

    Public Function SetColorCode(value As Object) As String
        Dim data As String() = value.ToString().Split(New Char() {";"c})
        If data(4) = "22" Or data(4) = "44" Or data(4) = "55" Or data(4) = "66" Then
            Return "available"
        ElseIf data(4) = "4" Then
            Return "not_available"
        ElseIf data(4) = "8" Then
            Return "alternate_location"
        ElseIf data(4) = "2" Then
            Return "already_enrolled"
        ElseIf data(4) = "4" Then
            Return "gv_can_notavailable"
        End If
        Return "not_available"
    End Function

    ''' <summary>
    ''' To enable/disable check box to enroll based on codes
    ''' </summary>
    ''' 
    Public Function IsAllowToEnroll(value As Object) As Boolean
        Dim data As String() = value.ToString().Split(New Char() {";"c})
        If data(4) = "22" Or data(4) = "44" Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Set checkbox visible 
    ''' </summary>
    ''' 
    Public Function IsVisible(value As Object) As Boolean
        Dim data As String() = value.ToString().Split(New Char() {";"c})
        If data(4) = "22" Or data(4) = "44" Or data(4) = "55" Or data(4) = "66" Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' To check if request to enroll is already exists or is course id core
    ''' </summary>
    ''' 
    Public Function IsEnrolled(value As Object) As Boolean
        Dim data As String() = value.ToString().Split(New Char() {";"c})
        If data(4) = "44" OrElse data(4) = "55" OrElse data(4) = "66" Then
            If Session("SummerPaymentSummery") Is Nothing Then
                If data(5) = "1" Then
                    Return True
                Else
                    Return True

                End If
            Else

                If Not dtSesstionSummeryPaymentSummery Is Nothing AndAlso dtSesstionSummeryPaymentSummery.Rows.Count > 0 Then
                    Dim dr() As DataRow = dtSesstionSummeryPaymentSummery.Select("ClassID='" & Convert.ToString(data(0)) & "' AND ProductID='" & Convert.ToString(data(1)) & "'")
                    If dr.Length > 0 Then
                        If data(5) = "1" Then
                            Return True
                        Else
                            Return True

                        End If
                    End If
                End If
            End If


        End If
        Return False
    End Function

    Public Function SetStagePaymentAmount(ByVal Percentage As Decimal, ByVal days As Integer) As String
        Try
            Dim dStageAmt As Decimal = CDec(Convert.ToString(lblStagePaymentTotal.Text).Substring(1, Convert.ToString(lblStagePaymentTotal.Text).Length - 1))
            Dim dStudentPayAmt As Decimal = CDec(Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1))
            Dim dPercentageAmt As Decimal = (dStageAmt * Percentage) / 100
            If days = 0 Then
                Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                txtIntialAmount.Text = Format(CDec(dintialStageAmount + dPercentageAmt), "0.00")
            Else
                Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                txtIntialAmount.Text = Format(CDec(dintialStageAmount), "0.00")
            End If
            Return ViewState("CurrencyTypeID") & Format(CDec(dPercentageAmt), "0.00")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return ""
        End Try
    End Function

    'Added by Sachin Sathe - to decide visibility of student enrollment link
    Private Function CheckStudentEligibility() As Boolean
        Dim bResult As Boolean = False
        Try
            Dim sSQL As String = "..spCheckAccessForStudEnrollPage__c @StudentID=" & AptifyEbusinessUser1.PersonID
            ' below code added by govind mande for redmine 16705
            Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                If Convert.ToInt32(dt.Rows(0)("ID")) > 0 Then
                    If Convert.ToInt32(dt.Rows(0)("FirmApprovalStatus")) = 1 Then
                        If Convert.ToInt32(dt.Rows(0)("FirmStatusIsRejected")) = 1 Then
                            ViewState("FirmStatusIsRejected") = True
                            'Return False

                        End If
                        Return True


                    Else
                        ViewState("FirmApproavlBlank") = True
                        If Convert.ToInt32(dt.Rows(0)("FirmStatusIsRejected")) = 1 Then
                            ViewState("FirmStatusIsRejected") = True
                        End If
                        Return False
                    End If

                End If
            End If
            'Dim lStudentID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
            'If lStudentID > 0 Then
            '    Return True
            'End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return bResult
    End Function

    'Added by Sachin Sathe -
    Private Function CheckStudentResultPublishDate() As Boolean
        Dim bResult As Boolean = False
        Try
            Dim sSQL As String = "..spChkResultPublishDate__c @StudentID=" & AptifyEbusinessUser1.PersonID
            Dim ResultPublishDateCnt As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
            If ResultPublishDateCnt > 0 Then
                Return True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return bResult
    End Function

    Protected Sub btnValidationOK_Click(sender As Object, e As EventArgs) Handles btnValidationOK.Click
        radWindowValidation.VisibleOnPageLoad = False
        Response.Redirect(HomePage, False)
    End Sub
    'End
#End Region

#Region "Check Box Changed"
    ' Code commented by Govind for redmine issue 13300
    'Protected Sub chkInfoToFirm_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkInfoToFirm.CheckedChanged
    '    Try

    '        ViewState("RouteChanged") = True
    '        'HideEnrollmentOnContractStudent()
    '        If Not ViewState("NewRTO") Is Nothing Then
    '            If CBool(ViewState("ShareMyInfoWithFirm")) = False AndAlso chkInfoToFirm.Checked Then
    '                FirmInfoTrueFromFalse()
    '                lblEEMessage.Text = ""
    '            ElseIf CBool(ViewState("ShareMyInfoWithFirm")) AndAlso chkInfoToFirm.Checked = False Then
    '                FirmInfoFalseFromTrue()
    '            ElseIf CBool(ViewState("NewRTO")) AndAlso chkInfoToFirm.Checked = False Then
    '                ShowEnrollmentDetails()
    '            ElseIf CBool(ViewState("ShareMyInfoWithFirm")) AndAlso chkInfoToFirm.Checked Then
    '                btnSubmit.Visible = True
    '                '' UpdatePanelBtn.Update()
    '                lblEEMessage.Text = ""
    '            End If
    '        Else
    '            If CBool(ViewState("OldRTO")) AndAlso chkInfoToFirm.Checked = False Then
    '                HideEnrollmentDetails()
    '            Else
    '                HideEnrollmentOnContractStudent()

    '            End If
    '        End If
    '        If ViewState("OldCompanyID") <> hdnCompanyId.Value Then
    '            HideEnrollmentDetails()
    '        Else
    '            ShowEnrollmentDetails()
    '        End If
    '        ''UpdatePanelPayment.Update()
    '        ''UpdatePanel1.Update()
    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    'End Sub

    ''' <summary>
    ''' Handles FAE elective check box checked event
    ''' </summary>
    ''' 
    Protected Sub chkIsFAEElective_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkSelectedIsFAEElective As CheckBox = DirectCast(sender, CheckBox)
        For Each item As GridItem In gvCurriculumCourse.MasterTableView.Items
            If TypeOf item Is GridDataItem Then
                Dim chkIsFAEElective As CheckBox = CType(item.FindControl("chkIsFAEElective"), CheckBox)
                ' Dim isFAEElective As Boolean = CBool(item.("IsFAEElective"))
                ''Added BY Pradip 2016-03-22 
                Dim lblIsFAEElective As Label = CType(item.FindControl("lblIsFAEElective"), Label)
                Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)
                Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
                Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)
                Dim chkClassRoom As CheckBox = DirectCast(item.FindControl("chkClassRoom"), CheckBox)
                Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)
                Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)
                Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)
                Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
                Dim lblClsroom As Label = DirectCast(item.FindControl("lblClsroom"), Label)
                Dim chkClassRoomTxt() As String = lblClsroom.Text.Split(CChar(";"))
                Dim lblMockExam As Label = DirectCast(item.FindControl("lblMockExam"), Label)
                Dim chkMockExamtxt() As String = lblMockExam.Text.Split(CChar(";"))
                Dim lblInterimAssessment As Label = DirectCast(item.FindControl("lblInterimAssessment"), Label)
                Dim chkInterimAssessmenttxt() As String = lblInterimAssessment.Text.Split(CChar(";"))
                Dim lblSummerExam As Label = DirectCast(item.FindControl("lblSummerExam"), Label)
                Dim chkSummerExamtxt() As String = lblSummerExam.Text.Split(CChar(";"))
                Dim lblRevision As Label = DirectCast(item.FindControl("lblRevision"), Label)
                Dim chkRevisiontxt() As String = lblRevision.Text.Split(CChar(";"))

                Dim lblResitInterimAssessment As Label = DirectCast(item.FindControl("lblResitInterimAssessment"), Label)
                Dim chkResitInterimAssessmenttxt() As String = lblResitInterimAssessment.Text.Split(CChar(";"))
                Dim lblAutumnExam As Label = DirectCast(item.FindControl("lblAutumnExam"), Label)
                Dim chkAutumnExamtxt() As String = lblAutumnExam.Text.Split(CChar(";"))
                Dim lblRepeatRevision As Label = DirectCast(item.FindControl("lblRepeatRevision"), Label)
                Dim chkRepeatRevisiontxt() As String = lblRepeatRevision.Text.Split(CChar(";"))
                If Convert.ToString(lblIsFAEElective.Text.Trim) = "1" Then
                    chkIsFAEElective.Enabled = True
                End If

                If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = True AndAlso chkSelectedIsFAEElective.Checked = True Then
                    For i As Integer = 0 To item.Controls.Count - 1
                        TryCast(item.Controls(i), GridTableCell).Enabled = True
                    Next
                End If
                If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False AndAlso chkSelectedIsFAEElective.Checked = False Then
                    For i As Integer = 0 To item.Controls.Count - 1
                        TryCast(item.Controls(i), GridTableCell).Enabled = True
                    Next
                End If
                If chkSelectedIsFAEElective.Checked AndAlso chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = True Then
                    If Convert.ToInt32(chkRepeatRevisiontxt(1) > 0) AndAlso chkRepeatRevision.Enabled = True Then
                        chkRepeatRevision.Checked = True
                        If Convert.ToInt32(chkAutumnExamtxt(1) > 0) Then
                            chkAutumnExam.Checked = True
                            chkAutumnExam.Enabled = False 'Added as part of #20944
                        End If
                        If Convert.ToInt32(chkResitInterimAssessmenttxt(1) > 0) Then
                            chkResitInterimAssessment.Checked = True
                        End If
                    ElseIf Convert.ToInt32(chkClassRoomTxt(1) > 0) AndAlso chkClassRoom.Enabled = True Then
                        chkClassRoom.Checked = True
                        If Convert.ToInt32(chkMockExamtxt(1) > 0) Then
                            chkMockExam.Checked = True
                        End If
                        If Convert.ToInt32(chkInterimAssessmenttxt(1) > 0) Then
                            chkInterimAssessment.Checked = True
                        End If
                        If Convert.ToInt32(chkSummerExamtxt(1) > 0) Then
                            chkSummerExam.Checked = True
                        End If

                    End If
                End If
                If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False AndAlso chkSelectedIsFAEElective.Checked = False Then
                    TryCast(item.Controls(2), GridTableCell).Enabled = True
                    For i As Integer = 3 To item.Controls.Count - 1

                        ''Commented By PRadip 2016-03-23 Not Neccessary
                        'If Convert.ToInt32(chkRepeatRevisiontxt(1)) > 0 Then
                        '    chkRepeatRevision.Checked = False
                        'End If
                        'If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                        '    chkResitInterimAssessment.Checked = False
                        'End If
                        'If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                        '    chkAutumnExam.Checked = False
                        '    chkAutumnExam.Enabled = True
                        'End If
                        'If Convert.ToInt32(chkRevisiontxt(1)) > 0 Then
                        '    chkRevision.Checked = False
                        'End If
                        'If Convert.ToInt32(chkResitInterimAssessmenttxt(1)) > 0 Then
                        '    chkResitInterimAssessment.Checked = False
                        'End If
                        'If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                        '    chkAutumnExam.Checked = False
                        'End If
                        chkResitInterimAssessment.Enabled = True
                        chkAutumnExam.Enabled = True
                        TryCast(item.Controls(i), GridTableCell).Enabled = False
                        TryCast(item.Controls(i).FindControl("chkClassRoom"), CheckBox).Checked = False
                        CType(item.FindControl("chkInterimAssessment"), CheckBox).Checked = False
                        CType(item.FindControl("chkMockExam"), CheckBox).Checked = False
                        CType(item.FindControl("chkSummerExam"), CheckBox).Checked = False
                        CType(item.FindControl("chkRevision"), CheckBox).Checked = False
                        CType(item.FindControl("chkRepeatRevision"), CheckBox).Checked = False
                        CType(item.FindControl("chkResitInterimAssessment"), CheckBox).Checked = False
                        CType(item.FindControl("chkAutumnExam"), CheckBox).Checked = False
                    Next
                End If
                If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False AndAlso chkSelectedIsFAEElective.Checked = True Then
                    For i As Integer = 0 To item.Controls.Count - 1
                        TryCast(item.Controls(i), GridTableCell).Enabled = False
                    Next
                End If
            End If
        Next
    End Sub
#End Region

    Protected Sub btnDisplayPaymentSummey_Click(sender As Object, e As System.EventArgs) Handles btnDisplayPaymentSummey.Click
        Try

            'UpdatePanelPayment.Update()
            'UpdatePanelBtn.Update()
            SummerPaymentSummery.Clear()
            txtIntialAmount.Visible = True
            lblIntialAmt.Visible = True
            txtIntialAmount.Visible = True
            lblCurrency.Visible = True
            Dim dtSummerPaymentSummery As DataTable

            '  HeadEnrollmentOptions.Attributes.Add("style", "display:none")
            'idEnroleddiv.Visible = False
            '#17651
            Dim sCourseID As String = String.Empty
            ViewState("Cap1Checked") = False
            For Each gvrow As GridItem In gvCurriculumCourse.MasterTableView.Items
                Dim IsCourseJurisdiction As Label = DirectCast(gvrow.FindControl("lblIsCourseJurisdiction"), Label)
                gvrow.Display = CBool(IsCourseJurisdiction.Text)
                Dim dataitem As GridDataItem = DirectCast(gvrow, GridDataItem)
                If gvrow.Display = True And ddlCAP1StudGrp.SelectedValue <> "Select Student Group" Then
                    Dim chkRepeatRevision As CheckBox = DirectCast(gvrow.FindControl("chkRepeatRevision"), CheckBox)
                    Dim chkResitInterimAssessment As CheckBox = DirectCast(gvrow.FindControl("chkResitInterimAssessment"), CheckBox)
                    Dim chkAutumnExam As CheckBox = DirectCast(gvrow.FindControl("chkAutumnExam"), CheckBox)
                    Dim chkClassRoom As CheckBox = DirectCast(gvrow.FindControl("chkClassRoom"), CheckBox)
                    Dim chkRevision As CheckBox = DirectCast(gvrow.FindControl("chkRevision"), CheckBox)
                    Dim chkInterimAssessment As CheckBox = DirectCast(gvrow.FindControl("chkInterimAssessment"), CheckBox)
                    Dim chkMockExam As CheckBox = DirectCast(gvrow.FindControl("chkMockExam"), CheckBox)
                    Dim chkSummerExam As CheckBox = DirectCast(gvrow.FindControl("chkSummerExam"), CheckBox)
                    Dim Curriculum As String = Convert.ToString(DataBinder.Eval(gvrow.DataItem, "Curriculum"))
                    Dim CourseID As Integer = Convert.ToInt32(DataBinder.Eval(gvrow.DataItem, "SubjectID"))
                    Dim lblIsCore__c As Label = DirectCast(gvrow.FindControl("lblIsCore__c"), Label)
                    Dim lblSubject As Label = DirectCast(gvrow.FindControl("lblSubject"), Label)
                    Dim lblCutOffUnits As Label = DirectCast(gvrow.FindControl("lblCutOffUnits"), Label)
                    Dim lblMinimumUnits As Label = DirectCast(gvrow.FindControl("lblMinimumUnits"), Label)
                    Dim lblUnits As Label = DirectCast(gvrow.FindControl("lblUnits"), Label)
                    Dim lblCurriculumID As Label = DirectCast(gvrow.FindControl("lblCurriculumID"), Label)
                    ' Dim lblAcademicCycleID As Label = DirectCast(item.FindControl("lblAcademicCycleID"), Label)
                    Dim lblIsFAEElective As Label = DirectCast(gvrow.FindControl("lblIsFAEElective"), Label)
                    Dim lblAlternativeGroup As Label = DirectCast(gvrow.FindControl("lblAlternativeGroup"), Label)
                    Dim lblRevision As Label = DirectCast(gvrow.FindControl("lblRevision"), Label)
                    Dim chkRevisiontxt() As String = lblRevision.Text.Split(CChar(";"))
                    Dim SummerRevisionProductID As Integer = Convert.ToInt32(chkRevisiontxt(1))
                    Dim lblClsroom As Label = DirectCast(gvrow.FindControl("lblClsroom"), Label)
                    Dim chkClassRoomTxt() As String = lblClsroom.Text.Split(CChar(";"))
                    Dim SummerClassRoomProductID As Integer = Convert.ToInt32(chkClassRoomTxt(1))
                    '' Get RepeatRevision Product ID 
                    Dim lblRepeatRevision As Label = DirectCast(gvrow.FindControl("lblRepeatRevision"), Label)
                    Dim chkRepeatRevisionTxt() As String = lblRepeatRevision.Text.Split(CChar(";"))
                    Dim SummerRepeatRevisionProductID As Integer = Convert.ToInt32(chkRepeatRevisionTxt(1))

                    Dim lblResitInterimAssessment As Label = DirectCast(gvrow.FindControl("lblResitInterimAssessment"), Label)
                    Dim chkResitInterimAssessmentTxt() As String = lblResitInterimAssessment.Text.Split(CChar(";"))
                    Dim SummerResitInterimAssessmentProductID As Integer = Convert.ToInt32(chkResitInterimAssessmentTxt(1))

                    Dim lblAutumnExam As Label = DirectCast(gvrow.FindControl("lblAutumnExam"), Label)
                    Dim chkRAutumnExamTxt() As String = lblAutumnExam.Text.Split(CChar(";"))
                    Dim SummerAutumnExamProductID As Integer = Convert.ToInt32(chkRAutumnExamTxt(1))

                    Dim lblInterimAssessment As Label = DirectCast(gvrow.FindControl("lblInterimAssessment"), Label)
                    Dim chkInterimAssessmenttxt() As String = lblInterimAssessment.Text.Split(CChar(";"))
                    Dim SummerInterimAssessmentProductID As Integer = Convert.ToInt32(chkInterimAssessmenttxt(1))

                    ' Get MockExam Product ID 
                    Dim lblMockExam As Label = DirectCast(gvrow.FindControl("lblMockExam"), Label)
                    Dim chkMockExamtxt() As String = lblMockExam.Text.Split(CChar(";"))
                    Dim SummerMockExamProductID As Integer = Convert.ToInt32(chkMockExamtxt(1))

                    Dim lblSummerExam As Label = DirectCast(gvrow.FindControl("lblSummerExam"), Label)
                    Dim chkSummerExamtxt() As String = lblSummerExam.Text.Split(CChar(";"))
                    Dim SummerSummerExamProductID As Integer = Convert.ToInt32(chkSummerExamtxt(1))
                    ''Commented BY Pradip 2016-02-04
                    'Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
                    dtSummerPaymentSummery = SummerPaymentSummery


                    Dim isFailed As Boolean = CBool(dataitem.GetDataKeyValue("Isfailed").ToString())
                    Dim FailedUnits As Double = CDbl(dataitem.GetDataKeyValue("FailedUnits").ToString())
                    Dim FirstAttempt As Double = CDbl(dataitem.GetDataKeyValue("FirstAttempt").ToString())

                    'https://redmine.softwaredesign.ie/issues/17651
                    Dim lblSubjectID As Label = DirectCast(gvrow.FindControl("lblSubjectID"), Label)
                    If chkClassRoom.Checked = True Then
                        If Convert.ToInt32(chkClassRoomTxt(1)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(4)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(4)) <> 2 Then
                            Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                            drSummerPaymentSummery("Subject") = lblSubject.Text
                            drSummerPaymentSummery("Type") = "ClassRoom"
                            drSummerPaymentSummery("ProductID") = chkClassRoomTxt(1)
                            If WhoPaysForElevationStudent(CInt(chkClassRoomTxt(1)), ddlRoute.SelectedValue, chkClassRoomTxt(0)) Then
                                drSummerPaymentSummery("WhoPay") = "Firm Pay"
                                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                            Else
                                drSummerPaymentSummery("WhoPay") = "Member Pay"
                                If Not chkClassRoomTxt(7) Is Nothing AndAlso Not chkClassRoomTxt(7) = "Select Student Group" AndAlso Convert.ToInt32(chkClassRoomTxt(7)) > 0 Then
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                Else
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                End If
                                '' Check Product Having Payment Plan
                                'Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkClassRoomTxt(1))
                                'Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                'If iProductPaymentPlan > 0 Then
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                'Else
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                'End If
                            End If
                            drSummerPaymentSummery("ClassID") = chkClassRoomTxt(0)
                            Dim dClassRoomTax As Decimal
                            drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkClassRoomTxt(1)), chkClassRoomTxt(0), ddlCAP1StudGrp.SelectedValue, dClassRoomTax)
                            drSummerPaymentSummery("TaxAmount") = dClassRoomTax
                            drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                            drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                            drSummerPaymentSummery("Units") = lblUnits.Text
                            drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                            drSummerPaymentSummery("AcademicCycleID") = chkClassRoomTxt(6)
                            If lblAlternativeGroup.Text.Trim <> "" Then
                                drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                            End If
                            drSummerPaymentSummery("FirstAttempt") = FirstAttempt
                            drSummerPaymentSummery("Isfailed") = isFailed
                            drSummerPaymentSummery("FailedUnits") = FailedUnits
                            drSummerPaymentSummery("SessionType") = "Summer"
                            dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)


                            _enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblCutOffUnits.Text, Convert.ToDouble(lblMinimumUnits.Text), Convert.ToString(drSummerPaymentSummery("Type")), False, isFailed, FailedUnits, FirstAttempt, Convert.ToDecimal(lblUnits.Text), "Summer"))


                        End If

                    End If
                    ' Add Mock
                    If chkMockExam.Checked = True Then
                        If Convert.ToInt32(chkMockExamtxt(1)) > 0 AndAlso Convert.ToInt32(chkMockExamtxt(4)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(4)) <> 2 Then
                            Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                            drSummerPaymentSummery("Subject") = lblSubject.Text
                            drSummerPaymentSummery("Type") = "MockExam"
                            drSummerPaymentSummery("ProductID") = chkMockExamtxt(1)
                            If WhoPaysForElevationStudent(CInt(chkMockExamtxt(1)), ddlRoute.SelectedValue, chkMockExamtxt(0)) Then
                                drSummerPaymentSummery("WhoPay") = "Firm Pay"
                                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                            Else
                                drSummerPaymentSummery("WhoPay") = "Member Pay"
                                If Convert.ToInt32(chkMockExamtxt(7)) > 0 Then
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                Else
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                End If
                                ' Check Product Having Payment Plan
                                'Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkMockExamtxt(1))
                                'Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                'If iProductPaymentPlan > 0 Then
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                'Else
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                'End If
                            End If
                            drSummerPaymentSummery("ClassID") = chkMockExamtxt(0)
                            Dim dMockExam As Decimal
                            drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkMockExamtxt(1)), chkMockExamtxt(0), ddlCAP1StudGrp.SelectedValue, dMockExam)
                            drSummerPaymentSummery("TaxAmount") = dMockExam
                            drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                            drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                            drSummerPaymentSummery("Units") = lblUnits.Text
                            drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                            drSummerPaymentSummery("AcademicCycleID") = chkMockExamtxt(6)
                            If lblAlternativeGroup.Text.Trim <> "" Then
                                drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                            End If
                            drSummerPaymentSummery("FirstAttempt") = FirstAttempt
                            drSummerPaymentSummery("Isfailed") = isFailed
                            drSummerPaymentSummery("FailedUnits") = FailedUnits
                            drSummerPaymentSummery("SessionType") = "Summer"
                            dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                            _enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblCutOffUnits.Text, Convert.ToDouble(lblMinimumUnits.Text), Convert.ToString(drSummerPaymentSummery("Type")), False, isFailed, FailedUnits, FirstAttempt, Convert.ToDecimal(lblUnits.Text), "Summer"))
                        End If

                    End If
                    If chkRepeatRevisionTxt(0) > 0 OrElse chkResitInterimAssessmentTxt(0) > 0 OrElse chkRAutumnExamTxt(0) > 0 Then
                        chkMockExam.Enabled = False
                    End If

                    If chkInterimAssessment.Checked Then
                        If Convert.ToInt32(chkInterimAssessmenttxt(1)) > 0 AndAlso Convert.ToInt32(chkInterimAssessmenttxt(4)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(4)) <> 2 Then
                            Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                            drSummerPaymentSummery("Subject") = lblSubject.Text
                            drSummerPaymentSummery("Type") = "Interim Assessment"
                            drSummerPaymentSummery("ProductID") = chkInterimAssessmenttxt(1)
                            If WhoPaysForElevationStudent(CInt(chkInterimAssessmenttxt(1)), ddlRoute.SelectedValue, chkInterimAssessmenttxt(0)) Then
                                drSummerPaymentSummery("WhoPay") = "Firm Pay"
                                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                            Else
                                drSummerPaymentSummery("WhoPay") = "Member Pay"
                                If Convert.ToInt32(chkInterimAssessmenttxt(7)) > 0 Then
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                Else
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                End If
                                ' Check Product Having Payment Plan
                                'Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkInterimAssessmenttxt(1))
                                'Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                'If iProductPaymentPlan > 0 Then
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                'Else
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                'End If
                            End If
                            drSummerPaymentSummery("ClassID") = chkInterimAssessmenttxt(0)
                            Dim dInterimAssTax As Decimal
                            drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkInterimAssessmenttxt(1)), chkInterimAssessmenttxt(0), ddlCAP1StudGrp.SelectedValue, dInterimAssTax)
                            drSummerPaymentSummery("TaxAmount") = dInterimAssTax
                            drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                            drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                            drSummerPaymentSummery("Units") = lblUnits.Text
                            drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                            drSummerPaymentSummery("AcademicCycleID") = chkInterimAssessmenttxt(6)
                            If lblAlternativeGroup.Text.Trim <> "" Then
                                drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                            End If
                            drSummerPaymentSummery("FirstAttempt") = FirstAttempt
                            drSummerPaymentSummery("Isfailed") = isFailed
                            drSummerPaymentSummery("FailedUnits") = FailedUnits
                            drSummerPaymentSummery("SessionType") = "Summer"
                            dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                            _enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblCutOffUnits.Text, Convert.ToDouble(lblMinimumUnits.Text), Convert.ToString(drSummerPaymentSummery("Type")), False, isFailed, FailedUnits, FirstAttempt, Convert.ToDecimal(lblUnits.Text), "Summer"))
                        End If

                    End If

                    If chkSummerExam.Checked Then
                        If Convert.ToInt32(chkSummerExamtxt(1)) > 0 AndAlso Convert.ToInt32(chkSummerExamtxt(4)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(4)) <> 2 Then
                            Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                            drSummerPaymentSummery("Subject") = lblSubject.Text
                            drSummerPaymentSummery("Type") = "Summer Exam"
                            drSummerPaymentSummery("ProductID") = chkSummerExamtxt(1)
                            If WhoPaysForElevationStudent(CInt(chkSummerExamtxt(1)), ddlRoute.SelectedValue, chkSummerExamtxt(0)) Then
                                drSummerPaymentSummery("WhoPay") = "Firm Pay"
                                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                            Else
                                drSummerPaymentSummery("WhoPay") = "Member Pay"
                                If Convert.ToInt32(chkSummerExamtxt(7)) > 0 Then
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                Else
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                End If
                                ' Check Product Having Payment Plan
                                'Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkSummerExamtxt(1))
                                'Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                'If iProductPaymentPlan > 0 Then
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                'Else
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                'End If
                            End If
                            drSummerPaymentSummery("ClassID") = chkSummerExamtxt(0)
                            Dim dSummerTax As Decimal
                            drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkSummerExamtxt(1)), chkSummerExamtxt(0), ddlCAP1StudGrp.SelectedValue, dSummerTax)
                            drSummerPaymentSummery("TaxAmount") = dSummerTax
                            drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                            drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                            drSummerPaymentSummery("Units") = lblUnits.Text
                            drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                            drSummerPaymentSummery("AcademicCycleID") = chkSummerExamtxt(6)
                            If lblAlternativeGroup.Text.Trim <> "" Then
                                drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                            End If
                            drSummerPaymentSummery("FirstAttempt") = FirstAttempt
                            drSummerPaymentSummery("Isfailed") = isFailed
                            drSummerPaymentSummery("FailedUnits") = FailedUnits
                            drSummerPaymentSummery("SessionType") = "Summer"
                            dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)

                            _enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblCutOffUnits.Text, Convert.ToDouble(lblMinimumUnits.Text), "Exam", False, isFailed, FailedUnits, FirstAttempt, Convert.ToDecimal(lblUnits.Text), "Summer"))
                            ''https://redmine.softwaredesign.ie/issues/17651
                            If lblCurriculumID.Text = 2 Then
                                ViewState("Cap1Checked") = True
                            End If
                        End If
                    Else
                        '_enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblCutOffUnits.Text, Convert.ToDouble(lblMinimumUnits.Text), "Exam", False, isFailed, FailedUnits, FirstAttempt))
                    End If

                    If chkRevision.Checked Then
                        If Convert.ToInt32(chkRevisiontxt(1)) > 0 AndAlso Convert.ToInt32(chkRevisiontxt(4)) > 0 AndAlso Convert.ToInt32(chkRevisiontxt(4)) <> 2 Then

                            Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                            drSummerPaymentSummery("Subject") = lblSubject.Text
                            drSummerPaymentSummery("Type") = "Revision"
                            drSummerPaymentSummery("ProductID") = chkRevisiontxt(1)
                            If WhoPaysForElevationStudent(CInt(chkRevisiontxt(1)), ddlRoute.SelectedValue, chkRevisiontxt(0)) Then
                                drSummerPaymentSummery("WhoPay") = "Firm Pay"
                                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                            Else
                                drSummerPaymentSummery("WhoPay") = "Member Pay"
                                If Convert.ToInt32(chkRevisiontxt(7)) > 0 Then
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                Else
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                End If
                                ' Check Product Having Payment Plan
                                'Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkRevisiontxt(1))
                                'Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                'If iProductPaymentPlan > 0 Then
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                'Else
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                'End If
                            End If
                            drSummerPaymentSummery("ClassID") = chkRevisiontxt(0)
                            Dim dRevisionTax As Decimal
                            drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkRevisiontxt(1)), chkRevisiontxt(0), ddlCAP1StudGrp.SelectedValue, dRevisionTax)
                            drSummerPaymentSummery("TaxAmount") = dRevisionTax
                            drSummerPaymentSummery("AcademicCycleID") = chkRevisiontxt(6)
                            'https://redmine.softwaredesign.ie/issues/17610
                            drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                            drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                            drSummerPaymentSummery("Units") = lblUnits.Text
                            drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                            drSummerPaymentSummery("FirstAttempt") = FirstAttempt
                            drSummerPaymentSummery("Isfailed") = isFailed
                            drSummerPaymentSummery("FailedUnits") = FailedUnits
                            drSummerPaymentSummery("SessionType") = "Summer"
                            dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                            _enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblCutOffUnits.Text, Convert.ToDouble(lblMinimumUnits.Text), Convert.ToString(drSummerPaymentSummery("Type")), False, isFailed, FailedUnits, FirstAttempt, Convert.ToDecimal(lblUnits.Text), "Summer"))
                        End If
                    End If

                    If chkRepeatRevision.Checked Then

                        If Convert.ToInt32(chkRepeatRevisionTxt(1)) > 0 AndAlso Convert.ToInt32(chkRepeatRevisionTxt(4)) > 0 AndAlso Convert.ToInt32(chkRepeatRevisionTxt(4)) <> 2 Then
                            Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                            drSummerPaymentSummery("Subject") = lblSubject.Text
                            drSummerPaymentSummery("Type") = "Repeat Revision"
                            drSummerPaymentSummery("ProductID") = chkRepeatRevisionTxt(1)
                            If WhoPaysForElevationStudent(CInt(chkRepeatRevisionTxt(1)), ddlRoute.SelectedValue, chkRepeatRevisionTxt(0)) Then
                                drSummerPaymentSummery("WhoPay") = "Firm Pay"
                                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                            Else
                                drSummerPaymentSummery("WhoPay") = "Member Pay"
                                If Convert.ToInt32(chkRepeatRevisionTxt(7)) > 0 Then
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                Else
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                End If
                                ' Check Product Having Payment Plan
                                'Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkRepeatRevisionTxt(1))
                                'Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                'If iProductPaymentPlan > 0 Then
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                'Else
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                'End If
                            End If
                            drSummerPaymentSummery("ClassID") = chkRepeatRevisionTxt(0)
                            Dim dRepeatRevisionTax As Decimal
                            drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkRepeatRevisionTxt(1)), chkRepeatRevisionTxt(0), ddlCAP1StudGrp.SelectedValue, dRepeatRevisionTax)
                            drSummerPaymentSummery("TaxAmount") = dRepeatRevisionTax
                            drSummerPaymentSummery("AcademicCycleID") = chkRepeatRevisionTxt(6)
                            drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                            drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                            drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                            drSummerPaymentSummery("Units") = lblUnits.Text
                            If lblAlternativeGroup.Text.Trim <> "" Then
                                drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                            End If
                            drSummerPaymentSummery("FirstAttempt") = FirstAttempt
                            drSummerPaymentSummery("Isfailed") = isFailed
                            drSummerPaymentSummery("FailedUnits") = FailedUnits
                            drSummerPaymentSummery("SessionType") = "Autumn"
                            dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                            '                            '_enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblMinimumUnits.Text, Convert.ToDouble(lblUnits.Text), Convert.ToString(drSummerPaymentSummery("Type")), False, isFailed, FailedUnits, FirstAttempt))
                            _enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblCutOffUnits.Text, Convert.ToDouble(lblMinimumUnits.Text), Convert.ToString(drSummerPaymentSummery("Type")), False, isFailed, FailedUnits, FirstAttempt, Convert.ToDecimal(lblUnits.Text), "Autumn"))
                        End If

                    End If

                    If chkResitInterimAssessment.Checked Then
                        If Convert.ToInt32(chkResitInterimAssessmentTxt(1)) > 0 AndAlso Convert.ToInt32(chkResitInterimAssessmentTxt(4)) > 0 AndAlso Convert.ToInt32(chkResitInterimAssessmentTxt(4)) <> 2 Then

                            Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                            drSummerPaymentSummery("Subject") = lblSubject.Text
                            drSummerPaymentSummery("Type") = "Resit Interim Assessment"
                            drSummerPaymentSummery("ProductID") = chkResitInterimAssessmentTxt(1)
                            If WhoPaysForElevationStudent(CInt(chkResitInterimAssessmentTxt(1)), ddlRoute.SelectedValue, chkResitInterimAssessmentTxt(0)) Then
                                drSummerPaymentSummery("WhoPay") = "Firm Pay"
                                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                            Else
                                drSummerPaymentSummery("WhoPay") = "Member Pay"
                                If Convert.ToInt32(chkResitInterimAssessmentTxt(7)) > 0 Then
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                Else
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                End If
                                ' Check Product Having Payment Plan
                                'Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkResitInterimAssessmentTxt(1))
                                'Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                'If iProductPaymentPlan > 0 Then
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                'Else
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                'End If
                            End If
                            drSummerPaymentSummery("ClassID") = chkResitInterimAssessmentTxt(0)
                            Dim dResitInterimAssessmentTax As Decimal
                            drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkResitInterimAssessmentTxt(1)), chkResitInterimAssessmentTxt(0), ddlCAP1StudGrp.SelectedValue, dResitInterimAssessmentTax)
                            drSummerPaymentSummery("TaxAmount") = dResitInterimAssessmentTax
                            drSummerPaymentSummery("AcademicCycleID") = chkResitInterimAssessmentTxt(6)
                            drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                            drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                            drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                            drSummerPaymentSummery("Units") = lblUnits.Text
                            If lblAlternativeGroup.Text.Trim <> "" Then
                                drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                            End If
                            drSummerPaymentSummery("FirstAttempt") = FirstAttempt
                            drSummerPaymentSummery("Isfailed") = isFailed
                            drSummerPaymentSummery("FailedUnits") = FailedUnits
                            drSummerPaymentSummery("SessionType") = "Autumn"
                            dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                            _enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblCutOffUnits.Text, Convert.ToDouble(lblMinimumUnits.Text), Convert.ToString(drSummerPaymentSummery("Type")), False, isFailed, FailedUnits, FirstAttempt, Convert.ToDecimal(lblUnits.Text), "Autumn"))
                        End If
                    End If


                    If chkAutumnExam.Checked Then
                        If Convert.ToInt32(chkRAutumnExamTxt(1)) > 0 AndAlso Convert.ToInt32(chkRAutumnExamTxt(4)) > 0 AndAlso Convert.ToInt32(chkRAutumnExamTxt(4)) <> 2 Then

                            Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                            drSummerPaymentSummery("Subject") = lblSubject.Text
                            drSummerPaymentSummery("Type") = "Autumn Exam"
                            drSummerPaymentSummery("ProductID") = chkRAutumnExamTxt(1)
                            If WhoPaysForElevationStudent(CInt(chkRAutumnExamTxt(1)), ddlRoute.SelectedValue, chkRAutumnExamTxt(0)) Then
                                drSummerPaymentSummery("WhoPay") = "Firm Pay"
                                drSummerPaymentSummery("IsProductPaymentPlan") = 0
                            Else
                                drSummerPaymentSummery("WhoPay") = "Member Pay"
                                If Convert.ToInt32(chkRAutumnExamTxt(7)) > 0 Then
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                Else
                                    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                End If
                                ' Check Product Having Payment Plan
                                'Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(chkRAutumnExamTxt(1))
                                'Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                'If iProductPaymentPlan > 0 Then
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 1
                                'Else
                                '    drSummerPaymentSummery("IsProductPaymentPlan") = 0
                                'End If
                            End If
                            drSummerPaymentSummery("ClassID") = chkRAutumnExamTxt(0)
                            Dim dAutumnExamTax As Decimal
                            drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(chkRAutumnExamTxt(1)), chkRAutumnExamTxt(0), ddlCAP1StudGrp.SelectedValue, dAutumnExamTax)
                            drSummerPaymentSummery("TaxAmount") = dAutumnExamTax
                            drSummerPaymentSummery("AcademicCycleID") = chkRAutumnExamTxt(6)
                            drSummerPaymentSummery("CurriculumID") = lblCurriculumID.Text
                            drSummerPaymentSummery("CutOffUnits") = lblCutOffUnits.Text
                            drSummerPaymentSummery("MinimumUnits") = lblMinimumUnits.Text
                            drSummerPaymentSummery("Units") = lblUnits.Text
                            If lblAlternativeGroup.Text.Trim <> "" Then
                                drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                            End If
                            drSummerPaymentSummery("FirstAttempt") = FirstAttempt
                            drSummerPaymentSummery("Isfailed") = isFailed
                            drSummerPaymentSummery("FailedUnits") = FailedUnits
                            drSummerPaymentSummery("SessionType") = "Autumn"
                            dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                            _enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblCutOffUnits.Text, Convert.ToDouble(lblMinimumUnits.Text), "Exam", False, isFailed, FailedUnits, FirstAttempt, Convert.ToDecimal(lblUnits.Text), "Autumn"))
                            ''https://redmine.softwaredesign.ie/issues/17651
                            If lblCurriculumID.Text = 2 Then
                                ViewState("Cap1Checked") = True
                            End If
                        End If
                    End If
                    SummerPaymentSummery = dtSummerPaymentSummery
                    Session("SummerPaymentSummery") = SummerPaymentSummery
                    'for https://redmine.softwaredesign.ie/issues/17651
                    If lblCurriculumID.Text = 2 Then
                        ViewState("Cap1") = True
                    End If
                    If lblCurriculumID.Text = 3 Then
                        ViewState("Cap2") = True
                    End If
                    If ViewState("Cap1Checked") = True Then
                        ViewState("selectedcap1") = True
                    End If
                    If Not ViewState("Cap1") Is Nothing AndAlso CBool(ViewState("Cap1Checked")) = False Then
                        ' above is briging case and cap1 courses not selected then we need to check if course is already taken or not and if first time then we restict student
                        If lblCurriculumID.Text = 2 Then ' get only cap1 course id's
                            If sCourseID = String.Empty Then
                                sCourseID = lblSubjectID.Text
                            Else
                                sCourseID = sCourseID + "," + lblSubjectID.Text
                            End If
                            ViewState("Cap1UnChecked") = True
                        End If

                    End If
                    ViewState("Cap1Checked") = False
                    'Check firm changes 
                    ''Commented By Pradip 2016-04-02 Not Neccesary here 
                    'CheckFirmChangeOrEnrolled()
                End If
            Next
            ' https://redmine.softwaredesign.ie/issues/17651

            If Not ViewState("Cap1") Is Nothing AndAlso Not ViewState("Cap2") Is Nothing AndAlso CBool(ViewState("Cap1UnChecked")) = True Then
                If sCourseID <> "" Then
                    Dim sSQl As String = Database & "..spCheckStudentEnrolOnCourses__c @CurriculumID=2,@StudentID=" & AptifyEbusinessUser1.PersonID & ",@CourseID=""" & sCourseID & """"
                    Dim icrID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQl, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If icrID <= 0 Then ' it meanse he his not enrol cap1 courses in remaining cap1 courses
                        lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.MinimumUnits")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '& " " & ViewState("MinimumUnitsRequired") & " full subjects in CAP1 or CAP2."
                        btnSubmitOk.Visible = False
                        btnNo.Visible = True
                        btnNo.Text = "Ok"
                        radwindowSubmit.VisibleOnPageLoad = True
                        updatePnlPopup.Update()
                        Exit Sub

                    End If

                End If

            End If

            ' Code added by Govind Mande 19th April 2016
            Session("SelectedStuentGroupID") = ddlCAP1StudGrp.SelectedItem.Text.Trim
            ' End code
            CheckFirmChangeOrEnrolled()

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub CheckFirmChangeOrEnrolled()
        Try
            If (ViewState("RouteChanged") IsNot Nothing AndAlso Convert.ToBoolean(ViewState("RouteChanged"))) Then
                '' code commented by Govind for  redmine issue 13300
                ''If CBool(ViewState("OldRTO")) = False AndAlso CBool(ViewState("NewRTO")) Then
                ''    If ddlRoute.SelectedItem.ToString().Trim.ToLower = "flexible option" _
                ''       Or (hdnCompanyId.Value <> "" AndAlso CLng(hdnCompanyId.Value) > 0) Then
                ''        SaveEEApplication()
                ''    End If
                ''ElseIf CBool(ViewState("OldRTO")) AndAlso CBool(ViewState("NewRTO")) = False Then
                ''    SaveEEApplication()
                ''ElseIf CBool(ViewState("OldRTO")) AndAlso CBool(ViewState("NewRTO")) Then
                ''    SaveEEApplication()
                ''ElseIf ddlRoute.SelectedItem.ToString().Trim.ToLower = "contract" OrElse (ddlRoute.SelectedItem.ToString().Trim.ToLower = "flexible option" AndAlso chkInfoToFirm.Checked) Then
                ''    If (ViewState("RouteChanged") IsNot Nothing AndAlso Convert.ToBoolean(ViewState("RouteChanged"))) _
                ''      Or (hdnCompanyId.Value <> "" AndAlso CLng(hdnCompanyId.Value) > 0) Then
                ''        SaveEEApplication()
                ''    End If
                ''Else
                ''    Validations()
                ''    ''SubmitBtn()
                ''End If
                SaveEEApplication()
            Else
                Validations()
                '' SubmitBtn()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Validations()
        Try
            If CheckGetMinimumCourses() Then
                ' Check Elevation Student With RTO
                btnSubmit.Visible = True
                Dim EducationContract As String = AptifyApplication.GetEntityRecordName("ApplicationTypes__c", Convert.ToInt32(ddlRoute.SelectedValue))
                If EducationContract.Trim.ToLower = "elevation" AndAlso Convert.ToBoolean(ViewState("RTO")) Then
                    If lblAmountPaidFirm.Text <> "" Then
                        If Convert.ToDecimal(Convert.ToString(lblAmountPaidFirm.Text).Substring(1, Convert.ToString(lblAmountPaidFirm.Text).Length - 1).Trim) > 0 Then
                            lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ElevationWithRTOMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            btnNo.Visible = False
                            btnSubmitOk.Visible = True

                            radwindowSubmit.VisibleOnPageLoad = True
                            updatePnlPopup.Update()
                        Else
                            UpdateEEApplicationData()
                            Response.Redirect(PaymentSummeryPage, False)
                            '' CreateOrderAndSaveData()
                        End If
                    Else
                        UpdateEEApplicationData()
                        Response.Redirect(PaymentSummeryPage, False)
                    End If

                    'ElseIf ddlRoute.SelectedItem.ToString().Trim.ToLower = "contract" AndAlso Convert.ToBoolean(ViewState("RTO")) Then
                    ''Added bY Pradip 2016-02-12 for Issue no 5418
                ElseIf EducationContract.Trim.ToLower = "contract" AndAlso Convert.ToBoolean(ViewState("RTO")) AndAlso Convert.ToString(ViewState("FirmApprovalStatus")).Trim.ToLower = "approved" Then
                    'ViewState("FirmApprovalStatus")
                    ' Redmine Issue 15687
                    ''lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.ContractWithRTOMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    ''btnNo.Visible = True
                    ''btnNo.Text = "No"
                    ''btnSubmitOk.Visible = True

                    ''radwindowSubmit.VisibleOnPageLoad = True
                    ''updatePnlPopup.Update()
                    UpdateEEApplicationData()
                    Response.Redirect(PaymentSummeryPage, False)
                Else
                    UpdateEEApplicationData()
                    Response.Redirect(PaymentSummeryPage, False)
                    '' CreateOrderAndSaveData()
                End If
            Else

                If CBool(ViewState("FAEValidation")) Then ' added FAE min validation mesage for redmine #17838
                    lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.FAEMinimumUnits")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '& " " & ViewState("MinimumUnitsRequired") & " full subjects in CAP1 or CAP2."
                Else
                    lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.MinimumUnits")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '& " " & ViewState("MinimumUnitsRequired") & " full subjects in CAP1 or CAP2."
                End If
                btnSubmitOk.Visible = False
                btnNo.Visible = True
                btnNo.Text = "Ok"
                radwindowSubmit.VisibleOnPageLoad = True
                updatePnlPopup.Update()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Next Page Get EE App Details
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateEEApplicationData()
        Try
            'If (ViewState("RouteChanged") IsNot Nothing AndAlso Convert.ToBoolean(ViewState("RouteChanged"))) _
            'Or (hdnCompanyId.Value <> "" AndAlso CLng(hdnCompanyId.Value) > 0) Then
            Dim dtUpdateEEApplication As New DataTable
            dtUpdateEEApplication.Columns.Add("RouteChanged")
            dtUpdateEEApplication.Columns.Add("ExemptionAppID")
            dtUpdateEEApplication.Columns.Add("RouteOfEntryID")
            dtUpdateEEApplication.Columns.Add("CompanyID")
            dtUpdateEEApplication.Columns.Add("ShareMyInfoWithFirm")
            dtUpdateEEApplication.Columns.Add("StudentGroupID")
            dtUpdateEEApplication.Columns.Add("ExamInterimStudentGrp")
            dtUpdateEEApplication.Columns.Add("EID")
            ''Added BY Pardip 2016-02-16
            dtUpdateEEApplication.Columns.Add("OldCompanyID")

            Dim drUpdateEEApp As DataRow = dtUpdateEEApplication.NewRow
            If (ViewState("RouteChanged") IsNot Nothing AndAlso Convert.ToBoolean(ViewState("RouteChanged"))) Then
                ViewState("RouteChanged") = True
            Else
                ViewState("RouteChanged") = False
            End If
            drUpdateEEApp("RouteChanged") = ViewState("RouteChanged")
            drUpdateEEApp("ExemptionAppID") = Convert.ToInt32(ViewState("ExemptionApp"))
            drUpdateEEApp("RouteOfEntryID") = ddlRoute.SelectedValue
            drUpdateEEApp("CompanyID") = hdnCompanyId.Value
            ''Added BY Pardip 2016-02-16
            drUpdateEEApp("OldCompanyID") = ViewState("OldCompanyID")
            If chkInfoToFirm.Checked Then
                drUpdateEEApp("ShareMyInfoWithFirm") = 1
            Else
                drUpdateEEApp("ShareMyInfoWithFirm") = 0
            End If

            drUpdateEEApp("StudentGroupID") = ddlCAP1StudGrp.SelectedValue
            drUpdateEEApp("ExamInterimStudentGrp") = ViewState("ExamInterimStudentGrp")
            If Request.QueryString("Eid") IsNot Nothing AndAlso Convert.ToInt32(Request.QueryString("Eid")) > 0 Then
                drUpdateEEApp("EID") = Request.QueryString("Eid")

            Else
                drUpdateEEApp("EID") = 0
            End If
            dtUpdateEEApplication.Rows.Add(drUpdateEEApp)
            Session("UpdateEEAppData") = dtUpdateEEApplication

            ''End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''Added BY Pradip 2016-03-23 for MidFeb-9
    Protected Sub btnIAWarning_Click(sender As Object, e As System.EventArgs) Handles btnIAWarning.Click
        btnIAWarning.Visible = False
        radwindowSubmit.VisibleOnPageLoad = False
    End Sub
    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim sInvoiceSql As String = Database & "..spGetInvoiceNumberByOrderID__c @OrderID=" & Convert.ToInt32(Session("OrderID"))
            Dim InvoiceNo As String = Convert.ToString(DataAction.ExecuteScalar(sInvoiceSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Flexible Student Invoice Report"))
            Dim rptParam As New AptifyCrystalReport__c
            rptParam.ReportID = ReportID
            rptParam.Param1 = InvoiceNo
            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPageURL & "' )", True)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class
