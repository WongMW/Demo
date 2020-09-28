Option Explicit On
Option Strict On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
'Developer                  Date modified               Comments 
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Milind Sutar               01/04/2015                  Firm Page for student course enrollment
'Siddharth Kavitake         10-Jul-2015                 Made changes for change requests
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports System.Data
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports System.Linq
Imports System.Drawing
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI.PivotGrid
Imports Telerik.Web.UI.PivotGrid.Core
Imports System.Windows.Threading
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Applications.OrderEntry

#Region "Model"

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
                   cutOffUnits As Double, units As Double, classType As String, isUnerolled As Boolean, isFailed As Boolean, FailedUnits As Double, FirstAttempt As Double, caMinimumUnits As Double)
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
        Me.caMinimumUnits = caMinimumUnits
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


    Private _caMinUnits As Double
    Public Property caMinimumUnits() As Double
        Get
            Return _caMinUnits
        End Get
        Set(ByVal value As Double)
            _caMinUnits = value
        End Set
    End Property
End Class

#End Region

#Region "Main Class"

Partial Class FirmCourseEnrollment__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

#Region "Properties"

    Dim CHECKCOUNT As Integer = 0
    Private _courseEnrollmentDetails As New DataTable()
    Private Shared _studentEnrollmentDetails As New DataTable()
    Private Shared _studentGroupList As New DataTable()
    Dim _studentGroupListNew As New DataTable()
    Private _studentBreakDownDetails As New DataSet()
    Private _ruleEngine As Aptify.Consulting.RuleEngine__c
    Private _ruleEngineResult As Boolean
    Private _EnrollOnLoad As Boolean = False

    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "FirmCourseEnrollment__c"
    Protected Const ATTRIBUTE_SUMMARY_PAGE_URL As String = "FirmCourseEnrollmentSummayURL__c"
    Protected Const ATTRIBUTE_MANAGE_MYGROUP_URL_PAGE_URL As String = "ManageMyGroupURL"
    'BFP Performance
    Protected Const ATTRIBUTE_ENROLMENT_LANDING_PAGE_URL As String = "FirmCourseEnrollmentLandingURL__c"

    Protected Const ATTRIBUTE_SUMMARY_FAEENROLLPAGE_URL As String = "FAEEnrollment__c"

    Private _isFirmRTO As Integer
    Private _selectedStudentId As Integer
    Private _routeOfEntryId As Integer
    Private _academicCycleId As Integer
    Private _studentGroupId As Integer

    Private _completedCurriculum As New DataTable()
    Private _completedCourses As New DataTable()
    Private _failedCourses As New DataTable()
    Private _currentCourses As New DataTable()
    Private _currentExams As New DataTable()
    Private _currentIAResits As New DataTable()
    Private _academicCycleList As New DataTable()
    Private _clearStagingDetails As New DataTable()
    Private _enrollmentList As New List(Of Enrollment)


    ''' <summary>
    ''' Get sets manage my groups page url
    ''' </summary>
    Public Overridable Property CourseEnrollmentSummaryURL() As String
        Get
            If Not ViewState(ATTRIBUTE_SUMMARY_PAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_SUMMARY_PAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_SUMMARY_PAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property


    Public Overridable Property FAEEnrollment() As String
        Get
            If Not ViewState(ATTRIBUTE_SUMMARY_FAEENROLLPAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_SUMMARY_FAEENROLLPAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_SUMMARY_FAEENROLLPAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    ''' <summary>
    ''' Get sets manage my groups page url
    ''' </summary>
    Public Overridable Property ManageMyGroupURL() As String
        Get
            If Not ViewState(ATTRIBUTE_MANAGE_MYGROUP_URL_PAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_MANAGE_MYGROUP_URL_PAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_MANAGE_MYGROUP_URL_PAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    'BFP Performance: Addded to redirect to landing page
    ''' <summary>
    ''' Get sets manage my groups page url
    ''' </summary>
    Public Overridable Property FirmCourseEnrollmentLandingURL() As String
        Get
            If Not ViewState(ATTRIBUTE_ENROLMENT_LANDING_PAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_ENROLMENT_LANDING_PAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_ENROLMENT_LANDING_PAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    ''' <summary>
    ''' To get or set if firm is RTO
    ''' </summary>
    Public Property IsFirmRTO() As Integer
        Get
            _isFirmRTO = CInt(ViewState("IsFirmRTO"))
            Return _isFirmRTO
        End Get
        Set(ByVal value As Integer)
            _isFirmRTO = value
            ViewState("IsFirmRTO") = _isFirmRTO.ToString()
        End Set
    End Property

    ''' <summary>
    ''' To get or set selected student id
    ''' </summary>
    Public Property SelectedStudentID() As Integer
        Get
            _selectedStudentId = CInt(ViewState("SelectedStudentID"))
            Return _selectedStudentId
        End Get
        Set(ByVal value As Integer)
            _selectedStudentId = value
            ViewState("SelectedStudentID") = _selectedStudentId.ToString()
        End Set
    End Property

    ''' <summary>
    ''' To get/set route of entry id
    ''' </summary>
    Public Property RouteOfEntryID As Integer
        Get
            _routeOfEntryId = CInt(ViewState("RouteOfEntryID"))
            Return _routeOfEntryId
        End Get
        Set(ByVal value As Integer)
            _selectedStudentId = value
            ViewState("RouteOfEntryID") = _selectedStudentId.ToString()
        End Set
    End Property

    ''' <summary>
    ''' To get set academic cycyle id
    ''' </summary>
    Public Property AcademicCycleID() As Integer
        Get
            _academicCycleId = CInt(ddlAcademicYear.SelectedValue)
            Return _academicCycleId
        End Get
        Set(ByVal value As Integer)
            _academicCycleId = value
        End Set
    End Property

    ''' <summary>
    ''' To get set academic cycyle id
    ''' </summary>
    Public Property StudentGroupID() As Integer
        Get
            If Not String.IsNullOrEmpty(ddlTimeTableList.SelectedValue) Then
                _studentGroupId = CInt(ddlTimeTableList.SelectedValue)
            End If
            Return _studentGroupId
        End Get
        Set(ByVal value As Integer)
            _studentGroupId = value
        End Set
    End Property


#End Region

#Region "Protected Methods"

#Region "Page Events"

    ''' <summary>
    ''' To set properties
    ''' </summary>
    Protected Overrides Sub SetProperties()
        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        MyBase.SetProperties()
        If String.IsNullOrEmpty(Me.CourseEnrollmentSummaryURL) Then
            Me.CourseEnrollmentSummaryURL = Me.GetLinkValueFromXML(ATTRIBUTE_SUMMARY_PAGE_URL)
        End If
        If String.IsNullOrEmpty(Me.ManageMyGroupURL) Then
            Me.ManageMyGroupURL = Me.GetLinkValueFromXML(ATTRIBUTE_MANAGE_MYGROUP_URL_PAGE_URL)
        End If
        If String.IsNullOrEmpty(Me.FAEEnrollment) Then
            Me.FAEEnrollment = Me.GetLinkValueFromXML(ATTRIBUTE_SUMMARY_FAEENROLLPAGE_URL)
        End If
        'BFP Performance
        If String.IsNullOrEmpty(Me.FirmCourseEnrollmentLandingURL) Then
            Me.FirmCourseEnrollmentLandingURL = Me.GetLinkValueFromXML(ATTRIBUTE_ENROLMENT_LANDING_PAGE_URL)
        End If
    End Sub

    'Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
    '    Dim js As HtmlGenericControl = New HtmlGenericControl("script")
    '    js.Attributes.Add("type", "text/javascript")
    '    js.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-1.7.1.min.js"))
    '    Me.Page.Header.Controls.Add(js)
    '    Dim js1 As HtmlGenericControl = New HtmlGenericControl("script")
    '    js1.Attributes.Add("type", "text/javascript")
    '    js1.Attributes.Add("src", ResolveUrl("~/Scripts/expand.js"))
    '    Me.Page.Header.Controls.Add(js1)
    '    Dim css As HtmlGenericControl = New HtmlGenericControl("style")
    '    css.Attributes.Add("type", "text/css")
    '    css.Attributes.Add("src", ResolveUrl("~/CSS/StyleSheet.css"))
    '    Me.Page.Header.Controls.Add(css)
    '    Dim js2 As HtmlGenericControl = New HtmlGenericControl("script")
    '    js2.Attributes.Add("type", "text/javascript")
    '    js2.Attributes.Add("src", ResolveUrl("~/Scripts/jquery.min.js"))
    '    Me.Page.Header.Controls.Add(js2)
    'End Sub

    ''' <summary>
    ''' Handles page pre-render event to set default setting
    ''' </summary>
    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If Not IsPostBack Then
            'LoadDefaultSetting()
            'Enroll student on eligible courses i.e check all checkboxes
            _EnrollOnLoad = True
            gvFirmCourseEnrollment.Rebind()
        End If
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "script", "$(function () { LocationFilterHide(); });", True)
    End Sub

    ''' <summary>
    ''' Handles page load event
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If hidValidation.Value = "Close" Then
            rwValidation.VisibleOnPageLoad = False
        End If
        lblMessage.Text = ""
        Dim _ScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        _ScriptManager.AsyncPostBackTimeout = 36000
        'Code added by Siddharth to redirect user to security page if firm is not RTO or user is not Admin.
        If Not IsPageAccessible() Then
            Response.Redirect("~/" + ConfigurationManager.AppSettings.GetValues("SecurityErrorPageURL")(0) + "?Message=" + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.General.NoAccessToPage__c")),
                Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), Me.DataAction.UserCredentials))
            Return
        End If

        'BFP Performance: setting logged in users company id and person id for web service
        hdnCompanyID.Value = loggedInUser.CompanyID.ToString
        hdnCompanyPersonID.Value = loggedInUser.PersonID.ToString
        If Not IsPostBack Then
            LoadAcademicYears()
            FillCurriculum()
            FillRouteOfEntry()
            'LoadFAEElectiveCourses() ' Commented by GM for Redmine #19432
            'Commented to resolve duplication of 'Select' option in dropdown
            'ddlTimeTable.Items.Insert(0, "Select")
            LoadEnrollmentCodes()
            ''Added BY Pradip 2015-10-19 To Fill Location Dropdown
            FillLocation()
            ''Added By Pradip To Set Default Selected Value = "New"
            ''BFP Performance: Commented below as to set default value to 'All', as per CAI request
            'If ddlEnrollmentType.Items.Count > 0 Then
            '    ddlEnrollmentType.SelectedValue = ddlEnrollmentType.Items.FindByText("New").Value()
            'End If
            'If ddlRouteOfEntry.Items.Count > 1 Then
            '   ddlRouteOfEntry.SelectedValue = ddlRouteOfEntry.Items.FindByText("Contract").Value()
            'End If
            'BFP Performance: commented setting current stage as we are passing it from landing page
            'If ddlCurrentStage.Items.Count > 1 Then
            '    ddlCurrentStage.SelectedValue = ddlCurrentStage.Items.FindByText("CAP1- CA Proficiency 1").Value()
            'End If
            '10/1/2016 - Vaishali changed the default value from ignore to Not Awaiting Results as we removed the ignore list item
            If ddlSubjectsFailed.Items.Count > 1 Then
                ddlSubjectsFailed.SelectedValue = ddlSubjectsFailed.Items.FindByText("Not awaiting results").Value()
            End If
            If ddlCodesList.Items.Count > 1 Then
                ddlCodesList.SelectedValue = ddlCodesList.Items.FindByText("Enrolment Request Exists").Value()
            End If
            If Request.QueryString("EventName") Is Nothing Then
                'BFP Performance
                'RunRuleEngine()
                'SavePersonsStageDetails()
                LoadPrePendingDataToStaging()
                'LoadStudentCourseEnrollmentData()
            Else
                'False
                'keep users checkboxes selection
                _EnrollOnLoad = False
                If ddlAcademicYear.Items.Count > 0 Then
                    ddlAcademicYear.SelectedValue = Request.QueryString("AcademicYear").ToString()
                End If
                If ddlEnrollmentType.Items.Count > 0 Then
                    ddlEnrollmentType.SelectedValue = Request.QueryString("EnrollMentType").ToString()
                End If
                If ddlRouteOfEntry.Items.Count > 1 Then
                    ddlRouteOfEntry.SelectedValue = Request.QueryString("RouteOfEntry").ToString()
                End If
                If ddlCurrentStage.Items.Count > 1 Then
                    ddlCurrentStage.SelectedValue = Request.QueryString("CurrentStage").ToString()
                End If
                If ddlSubjectsFailed.Items.Count > 1 Then
                    ddlSubjectsFailed.SelectedValue = Request.QueryString("SubjectFailed").ToString()
                End If
                If ddlCodesList.Items.Count > 1 Then
                    ddlCodesList.SelectedValue = Request.QueryString("FilterBy").ToString()
                End If
                If ddlLocation.Items.Count > 1 Then
                    ddlLocation.SelectedValue = Request.QueryString("Loc").ToString()
                End If
                'BFP Performance
                ddlCurriculumList.SelectedValue = ddlCurriculumList.Items.FindByText(ddlCurrentStage.SelectedValue).Value()
                FillTimeTable(CInt(ddlAcademicYear.SelectedValue), CInt(ddlCurriculumList.SelectedValue))
                gvFirmCourseEnrollment.Rebind()
            End If
            SetProperties()

            If Not ddlCurriculumList.SelectedValue <> "0" Then
                'BFP Performance: commented setting current stage as we are passing it from landing page, commented if part and now checking negation for else part
                'ddlCurriculumList.SelectedValue = ddlCurriculumList.Items.FindByText(ddlCurrentStage.SelectedValue).Value()
                'FillTimeTable(CInt(ddlAcademicYear.SelectedValue), CInt(ddlCurriculumList.SelectedValue))
                'lblTTMessage.Text = ""
                'Else
                ddlTimeTable.Items.Clear()
                ddlTimeTable.Items.Insert(0, "Select")
            End If
        End If

        ''Added by Paresh for Performance
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "Sys.Application.add_load(getGridCell);", True)
    End Sub

#End Region

#Region "Main Big Firm Page Events"






    ''Added By Pradip 2015-07-14 To Fill Time Table
    Protected Sub ddlCurriculumList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCurriculumList.SelectedIndexChanged
        Try
            'BFP Performane
            If ddlCurriculumList.SelectedValue <> "0" Then
                FillTimeTable(CInt(ddlAcademicYear.SelectedValue), CInt(ddlCurriculumList.SelectedValue))
                lblTTMessage.Text = ""
            Else
                ddlTimeTable.Items.Clear()
                ddlTimeTable.Items.Insert(0, "Select")
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles academic year dropdown selected index change event
    ''' </summary>
    'Protected Sub ddlAcademicYear_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAcademicYear.SelectedIndexChanged
    '    gvFirmCourseEnrollment.Rebind()
    'End Sub

    ''' <summary>
    ''' Handles codes - dropdown selected index change event
    ''' </summary>
    'Protected Sub ddlCodesList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCodesList.SelectedIndexChanged
    '    gvFirmCourseEnrollment.Rebind()
    'End Sub

    ''' <summary>
    ''' Handles course enrollment - pivot grid need data sourse event to bind/rebind data
    ''' </summary>
    Protected Sub gvFirmCourseEnrollment_NeedDataSource(sender As Object, e As Telerik.Web.UI.PivotGridNeedDataSourceEventArgs) Handles gvFirmCourseEnrollment.NeedDataSource
        lblNumberOfStudents.Text = ""
        'AndAlso hdn_EnrollOnLoad.Value = ""
        If _EnrollOnLoad Then
            ' SavePersonsStageDetails()
            EnrollStudentOnCourseData()
            _EnrollOnLoad = False
        End If
        'hdn_EnrollOnLoad.Value = ""
        'Added below line to resolve concurrent login issue
        _courseEnrollmentDetails = LoadStudentCourseEnrollmentData()
        If _courseEnrollmentDetails.Rows.Count > 0 Then



            Dim DistinctDt As DataTable = _courseEnrollmentDetails.DefaultView.ToTable(True, "Curriculum")
            If DistinctDt.Rows.Count = _courseEnrollmentDetails.Rows.Count Then
                gvFirmCourseEnrollment.ColumnGroupsDefaultExpanded = True
            End If
            Dim query = _courseEnrollmentDetails.AsEnumerable().Select(Function(p) p.Field(Of Integer)("StudentID")).Distinct().ToList()
            Dim number As New StringBuilder()
            number.AppendFormat("Total Number of Students : {0}", query.Count.ToString())
            lblNumberOfStudents.Text = number.ToString()
            ''Added By Pradip 2016-06-15 For Vertical Scroll Bar Issue No-13856
            If query.Count > 10 Then
                gvFirmCourseEnrollment.ClientSettings.Scrolling.AllowVerticalScroll = True
                Try
                    gvFirmCourseEnrollment.ClientSettings.Scrolling.ScrollHeight = 500
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            Else
                gvFirmCourseEnrollment.ClientSettings.Scrolling.AllowVerticalScroll = False
                gvFirmCourseEnrollment.ClientSettings.Scrolling.ScrollHeight = 0
            End If

            gvFirmCourseEnrollment.Visible = True
            gvFirmCourseEnrollment.DataSource = _courseEnrollmentDetails

            Dim TobeDistinct As String() = {"StudentNumber", "StudentID", "LastName", "FirstName", "OfficeLocation", "Curriculum"}
            Dim dtFAE As DataTable = _courseEnrollmentDetails.DefaultView.ToTable(True, TobeDistinct)
            Dim dv As DataView = New DataView(dtFAE)
            dv.RowFilter = "Curriculum = 'FAE- Final Admitting Exam'"
            dtFAE = dv.ToTable
            Session("FAEStudent") = dtFAE
            If dtFAE.Rows.Count > 0 AndAlso (ddlCurrentStage.SelectedItem.Text.Trim = "FAE- Final Admitting Exam" Or ddlCurrentStage.SelectedItem.Text.Trim = "All") Then
                divFAEUpdateMain.Visible = True
            End If
        Else
            gvFirmCourseEnrollment.DataSource = Nothing
            gvFirmCourseEnrollment.Visible = False
            lblNoRecords.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.PivotGrid.NoRecords")), _
                Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), Me.DataAction.UserCredentials)
        End If
    End Sub


    '' Commented by Paresh for Performance

    ''' <summary>
    ''' Handles course enrollment - pivot grid cell data bound event
    ''' </summary>
    'Protected Sub gvFirmCourseEnrollment_CellDataBound(sender As Object, e As Telerik.Web.UI.PivotGridCellDataBoundEventArgs) Handles gvFirmCourseEnrollment.CellDataBound

    '    ' If TypeOf e.Cell Is PivotGridRowHeaderCell Then
    '    '   If e.Cell.Controls.Count > 0 Then
    '    '    If e.Cell.Controls(0) IsNot Nothing Then
    '    '       TryCast(e.Cell.Controls(0), Button).Visible = False
    '    '   End If
    '    'End If
    '    ' End If

    '    If TypeOf e.Cell Is PivotGridDataCell Then
    '        If Not e.Cell.DataItem Is Nothing Then
    '            If e.Cell.DataItem.ToString().Length > 0 Then
    '                Dim IsEnrolled As Integer = Convert.ToInt32(e.Cell.DataItem)
    '                Dim sCurruculum As String
    '                sCurruculum = TryCast(e.Cell, PivotGridDataCell).ParentColumnIndexes(0).ToString()
    '                If e.Cell.FindControl("lblTimeTable") IsNot Nothing Then
    '                    Dim lbltimeTable As Label = CType(e.Cell.FindControl("lblTimeTable"), Label)
    '                    lbltimeTable.Visible = False
    '                    If IsEnrolled < -1 Then
    '                        lbltimeTable.Visible = True
    '                        lbltimeTable.Text = ""
    '                        Me.SelectedStudentID = (-1) * (IsEnrolled)
    '                        ' LoadStudentGroups(sCurruculum)
    '                        If _studentGroupListNew.Rows.Count = 0 Then
    '                            LoadStudentGroupsNew()
    '                        End If
    '                        Dim filterRow() As DataRow = _studentGroupListNew.Select("StudentID='" & Convert.ToString(Me.SelectedStudentID) & "' AND CurriculumName='" & Convert.ToString(sCurruculum) & "' ")

    '                        If filterRow.Count > 0 Then

    '                            lbltimeTable.Text = Convert.ToString(filterRow(0)("StudentGroupName"))
    '                        Else
    '                            lbltimeTable.Text = ""
    '                            'e.Cell.Text = Convert.ToString(filterRow(0)("StudentGroupName"))
    '                        End If
    '                        'If Me.SelectedStudentID = 126141 Then
    '                        '    Dim s As String = lbltimeTable.Text
    '                        'End If
    '                        'LoadDefaultSelectedGroup(lbltimeTable.Text, False)
    '                    End If
    '                End If


    '                ''Commented by Paresh for Performance issue

    '                'If IsEnrolled = 1 Then
    '                '    e.Cell.BackColor = System.Drawing.Color.DarkGreen
    '                'ElseIf IsEnrolled = -1 Then
    '                '    e.Cell.BackColor = System.Drawing.Color.DarkSeaGreen
    '                'ElseIf IsEnrolled = 11 Then
    '                '    e.Cell.BackColor = System.Drawing.Color.LightSalmon
    '                'ElseIf IsEnrolled = 2 Then
    '                '    e.Cell.BackColor = System.Drawing.Color.Yellow
    '                '    'BFP Performance: Commented below and added new colour for code 44 that is enrolment request exists
    '                'ElseIf IsEnrolled = 22 Then 'OrElse IsEnrolled = 44
    '                '    e.Cell.BackColor = System.Drawing.Color.DeepSkyBlue
    '                'ElseIf IsEnrolled = 44 OrElse IsEnrolled = 88 Then
    '                '    e.Cell.BackColor = System.Drawing.Color.DarkRed
    '                'ElseIf IsEnrolled = 4 Then
    '                '    e.Cell.BackColor = System.Drawing.Color.Gray
    '                '    'BFP Performance: Commented below and moved code 88 under dark red, as we have removed checkboxes it became difficult for user to differentiate
    '                'ElseIf IsEnrolled = 8 Then 'OrElse IsEnrolled = 88
    '                '    e.Cell.BackColor = System.Drawing.Color.MediumPurple
    '                'End If

    '                '''''''
    '            End If
    '        End If
    '    End If
    '    Dim C As Integer = CHECKCOUNT
    'End Sub

    ''' <summary>
    '''  Handles course enrollment - pivot grid item command event to open pop-up windows
    ''' </summary>
    Protected Sub gvFirmCourseEnrollment_ItemCommand(sender As Object, e As Telerik.Web.UI.PivotGridCommandEventArgs) Handles gvFirmCourseEnrollment.ItemCommand
        If e.CommandName = "CourseEnrollmentEdit" Then
            lblPopupCourseEnrollmentMessage.Text = String.Empty
            rwCourseEnrollment.VisibleOnPageLoad = True
            Dim lastNameString As String = CStr(e.CommandArgument)
            Dim nameData As String() = lastNameString.Split(New Char() {";"c})
            Me.SelectedStudentID = CType(nameData(0), Integer)
            'txtLastName.Text = CStr(nameData(1))
            lblStudentNo.Text = Me.SelectedStudentID.ToString()
            lblLastName.Text = CStr(nameData(1))
            LoadStudentEnrollment()
            ''Added By Pradip 2015-07-06
        ElseIf e.CommandName = "CourseEnrollmentEditM" Then
            lblPopupCourseEnrollmentMessage.Text = String.Empty
            rwCourseEnrollment.VisibleOnPageLoad = True
            Dim lastNameString As String = CStr(e.CommandArgument)
            Dim nameData As String() = lastNameString.Split(New Char() {";"c})
            ' Me.SelectedStudentID = CType(Convert.ToString(e.CommandArgument).Split(";")(0), Integer)
            Me.SelectedStudentID = CType(nameData(0), Integer)
            lblStudentNo.Text = Me.SelectedStudentID.ToString()
            'txtLastName.Text = CStr(nameData(1))
            'lblLastName.Text = CStr(nameData(1))
            LoadStudentEnrollment()

        ElseIf e.CommandName = "BreakDownEdit" Then
            Dim studentNumberString As String = CStr(e.CommandArgument)
            Dim data As String() = studentNumberString.Split(New Char() {";"c})
            Me.SelectedStudentID = CType(data(0), Integer)
            rwStudentBreakDown.VisibleOnPageLoad = True
            LoadStudentBreakDownData()
            gvCompletedCurriculum.Rebind()
            gvCompletedCourses.Rebind()
            gvFailedCourses.Rebind()
            gvCurrentCourses.Rebind()
            gvCurrentExams.Rebind()
            gvIAResits.Rebind()

        ElseIf e.CommandName = "CheckBoxChecked" Then




        End If
    End Sub

    Protected Sub gvFirmCourseEnrollment_PageIndexChanged(sender As Object, e As Telerik.Web.UI.PivotGridPageChangedEventArgs) Handles gvFirmCourseEnrollment.PageIndexChanged
        _EnrollOnLoad = False
    End Sub

    Protected Sub gvFirmCourseEnrollment_PageSizeChanged(sender As Object, e As Telerik.Web.UI.PivotGridPageSizeChangedEventArgs) Handles gvFirmCourseEnrollment.PageSizeChanged
        _EnrollOnLoad = False
    End Sub

    ''' <summary>
    ''' Handles summary page - button click 
    ''' </summary>
    Protected Sub btnSummaryPage_Click(sender As Object, e As System.EventArgs) Handles btnSummaryPage.Click
        Dim c As Integer = 0
        Dim canEnroll As Boolean = True

        Dim sSQL As String
        sSQL = Database & "..spGetEnrollmentStagingByOrderStatus__c"
        Dim param(1) As IDataParameter
        param(0) = DataAction.GetDataParameter("@Status", SqlDbType.VarChar, "Pending")
        param(1) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, CInt(loggedInUser.CompanyID))
        Dim dtPending As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
        If dtPending.Rows.Count > 0 Then

            Dim isCap1 As Boolean = False
            Dim isCap2 As Boolean = False
            Dim isFAE As Boolean = False
            Dim chkDebkFailed As Boolean = False
            'Get Curriculum IDs 
            Dim sCurriculumIDSQL As String = Database & "..spGetCurriculumsID__c"
            Dim dtCurriculumIDs As DataTable = DataAction.GetDataTable(sCurriculumIDSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

            Dim studentIDs = dtPending.AsEnumerable().Select(Function(p) p.Field(Of Integer)("StudentID")).Distinct().ToList()
            'BFP Performance: Initializing session variable
            If Session("UpdatePriceStatus") Is Nothing Then
                Session("UpdatePriceStatus") = ""
            End If
            For Each stud As Integer In studentIDs
                Dim sid As Integer = stud
                Dim curriculumIds = (From t In dtPending.AsEnumerable() Where (t.Field(Of Int32)("StudentID") = sid)
                                     Select t.Field(Of Int32)("CurriculumID")).Distinct()
                'Start- Siddharth: Added for redmine log #17839
                Dim iSelectedCount As Double = 0
                'End
                For Each item As Integer In curriculumIds
                    Dim cid As Integer = item
                    Dim IsDebkQuery = (From t In dtPending.AsEnumerable() Where (t.Field(Of Int32)("StudentID") = sid And t.Field(Of Int32)("CurriculumID") = cid And t.Field(Of Double)("Units") = 1 And t.Field(Of String)("ClassType").Trim().ToLower() = "exam")
                                        Select t.Field(Of Int32)("CourseID"))
                    If IsDebkQuery IsNot Nothing And IsDebkQuery.ToList().Count > 0 And IsDebkQuery.ToList().Count = 1 Then
                        Dim CourseID As Integer = IsDebkQuery(0)
                        Dim sSqlDEBK As String = Database & "..spGetDEBKFailed__c @CourseID=" & CourseID & ",@StudentID=" & sid.ToString()
                        Dim isDebkFailed As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlDEBK, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If isDebkFailed Then
                            chkDebkFailed = True
                        End If
                    End If
                    If chkDebkFailed = False Then
                        'Start- Siddharth: Updated all below queries to include caMinimumUnits, to consider already enrolled courses in validation  for redmine log #17839
                        'Start- Siddharth: Added for redmine log #17839
                        Dim query = (From cr In dtPending.AsEnumerable() Where (cr.Field(Of Int32)("StudentID") = sid And cr.Field(Of Int32)("CurriculumID") = cid And cr.Field(Of String)("ClassType").Trim().ToLower() = "exam")
                                    Group By CurriculumID = cr("CurriculumID"), CutOffUnits = cr("CutOffUnits"), FailedUnits = cr("FailedUnits"), FirstAttempt = cr("FirstAttempt"), caMinimumUnits = cr("caMinimumUnits") Into Total = Sum(cr.Field(Of Double)("Units")) Select Total, CutOffUnits, CurriculumID, FirstAttempt, FailedUnits, caMinimumUnits)
                        'End
                        Dim Failedquery = (From cr In dtPending.AsEnumerable() Where (cr.Field(Of Int32)("StudentID") = sid And cr.Field(Of Int32)("CurriculumID") = cid And cr.Field(Of String)("ClassType").Trim().ToLower() = "exam" And cr.Field(Of Int32)("IsFailed") = 1)
                                    Group By CurriculumID = cr("CurriculumID"), CutOffUnits = cr("CutOffUnits"), FailedUnits = cr("FailedUnits"), FirstAttempt = cr("FirstAttempt"), caMinimumUnits = cr("caMinimumUnits") Into Total = Sum(cr.Field(Of Double)("Units")) Select Total, CutOffUnits, CurriculumID, FirstAttempt, FailedUnits, caMinimumUnits)

                        Dim FirstAttemptquery = (From cr In dtPending.AsEnumerable() Where (cr.Field(Of Int32)("StudentID") = sid And cr.Field(Of Int32)("CurriculumID") = cid And cr.Field(Of String)("ClassType").Trim().ToLower() = "exam" And cr.Field(Of Int32)("IsFailed") = 0)
                                    Group By CurriculumID = cr("CurriculumID"), CutOffUnits = cr("CutOffUnits"), FailedUnits = cr("FailedUnits"), FirstAttempt = cr("FirstAttempt"), caMinimumUnits = cr("caMinimumUnits") Into Total = Sum(cr.Field(Of Double)("Units")) Select Total, CutOffUnits, CurriculumID, FirstAttempt, FailedUnits, caMinimumUnits)

                        Dim FirstSelectedUnit As Double = 0
                        Dim minCutOffUnits As Double = 0
                        Dim CurriculumID2 As Integer = 0
                        Dim firstAttemptremaining As Double = 0

                        'Start- Siddharth: Added for redmine log #17839
                        Dim FailedSelectedUnit As Double = 0
                        Dim FailedCurriculumID As Integer = 0
                        Dim Failedremaining As Double = 0

                        'Added below code for Bridge case with at least 1 fresh course, so that it will be mandetory for user to select cap1 courses. 
                        If query IsNot Nothing And query.ToList().Count > 0 And cid = Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)) AndAlso ddlCurriculumList.SelectedValue = "-1" Then
                            firstAttemptremaining = Convert.ToDouble(query.ToList().First().FirstAttempt)
                            Failedremaining = Convert.ToDouble(query.ToList().First().FailedUnits)
                            minCutOffUnits = Convert.ToDouble(query.ToList().First().caMinimumUnits)
                        End If

                        'End
                        If FirstAttemptquery IsNot Nothing And FirstAttemptquery.ToList().Count > 0 Then
                            FirstSelectedUnit = FirstAttemptquery.ToList().First().Total
                            minCutOffUnits = Convert.ToDouble(FirstAttemptquery.ToList().First().caMinimumUnits)
                            CurriculumID2 = Convert.ToInt32(FirstAttemptquery.ToList().First().CurriculumID)
                            firstAttemptremaining = Convert.ToDouble(FirstAttemptquery.ToList().First().FirstAttempt)
                            'Start- Siddharth: Added for redmine log #17839
                            'Commented for other minimum units scenarioes
                            'Failedremaining = Convert.ToDouble(FirstAttemptquery.ToList().First().FailedUnits)
                            'End
                            If CurriculumID2 = Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)) Then
                                isCap1 = True
                            End If
                            If CurriculumID2 = Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)) Then
                                isCap2 = True
                            End If
                            If CurriculumID2 = Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)) Then
                                isFAE = True
                            End If
                        End If

                        'Dim FailedSelectedUnit As Double = 0
                        'Dim FailedCurriculumID As Integer = 0
                        'Dim Failedremaining As Double = 0

                        If Failedquery IsNot Nothing And Failedquery.ToList().Count > 0 Then
                            FailedSelectedUnit = Failedquery.ToList().First().Total
                            minCutOffUnits = Convert.ToDouble(Failedquery.ToList().First().caMinimumUnits)
                            FailedCurriculumID = Convert.ToInt32(Failedquery.ToList().First().CurriculumID)
                            Failedremaining = Convert.ToDouble(Failedquery.ToList().First().FailedUnits)
                            'Start- Siddharth: Added for redmine log #17839
                            'Commented for other minimum units scenarioes
                            '    firstAttemptremaining = Convert.ToDouble(Failedquery.ToList().First().FirstAttempt)
                            'End
                            If FailedCurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)) Then
                                isCap1 = True
                            End If
                            If FailedCurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)) Then
                                isCap2 = True
                            End If
                            If FailedCurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)) Then
                                isFAE = True
                            End If
                        End If

                        'Siddharth: added code and changed comparing values for redmine #17651
                        'Start
                        iSelectedCount = iSelectedCount + FailedSelectedUnit + FirstSelectedUnit
                        Dim minCutOffToCompare As Double = 0
                        If firstAttemptremaining >= minCutOffUnits Then
                            minCutOffToCompare = minCutOffUnits
                        Else
                            minCutOffToCompare = firstAttemptremaining
                        End If

                        If (firstAttemptremaining >= minCutOffToCompare And FirstSelectedUnit < minCutOffToCompare) Then
                            canEnroll = False
                            Exit For
                        End If

                        minCutOffToCompare = minCutOffUnits
                        If Failedremaining < minCutOffUnits Then
                            minCutOffToCompare = Failedremaining
                        End If
                        'Siddharth: Added condition for FAE enrolment, if student fails on CORE & Elective and for enrolment select different elective than failed
                        'Start
                        If FailedSelectedUnit < minCutOffUnits AndAlso isFAE Then
                            minCutOffToCompare = minCutOffToCompare - FirstSelectedUnit
                        End If
                        'End
                        If (Failedremaining >= minCutOffToCompare And FailedSelectedUnit < minCutOffToCompare) Then
                            canEnroll = False
                            Exit For
                        End If
                        'End
                        ''Commented By Pradip 2017-03-21
                        ' ''If query IsNot Nothing And query.ToList().Count > 0 Then
                        ' ''    Dim totalUnits As Double = query.ToList().First().Total
                        ' ''    Dim cutOffDates As Double = Convert.ToDouble(query.ToList().First().CutOffUnits)
                        ' ''    Dim CurriculumID As Integer = Convert.ToInt32(query.ToList().First().CurriculumID)
                        ' ''    If CurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)) Then
                        ' ''        isCap1 = True
                        ' ''    End If
                        ' ''    If CurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)) Then
                        ' ''        isCap2 = True
                        ' ''    End If
                        ' ''    If CurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)) Then
                        ' ''        isFAE = True
                        ' ''    End If

                        ' ''    ''Added by Pradip 2016-11-15 for Issue https://redmine.softwaredesign.ie/issues/16028
                        ' ''    Dim sSqlIsEnroll As String = Database & "..spCheckIsEnrolledByCurriculum__c @CurriculumID=" & CurriculumID & ",@StudentID=" & sid.ToString()
                        ' ''    Dim isEnroll As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlIsEnroll, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        ' ''    If isEnroll = False Then
                        ' ''        If totalUnits < cutOffDates Then 'Added totalUnits <> dAvailableUnits, because if only 1 unit is available to taek then cutoffunits check is not valid
                        ' ''            canEnroll = False
                        ' ''            Exit For
                        ' ''        End If
                        ' ''    End If

                        ' ''End If
                    End If

                Next
                If chkDebkFailed = False Then
                    If Not dtPending Is Nothing AndAlso dtPending.Rows.Count > 0 Then
                        Dim viewCap1 As New DataView(dtPending)
                        Dim distinctValuesCap1 As DataTable = viewCap1.ToTable(True, "CurriculumID")
                        Dim drCap1() As DataRow = distinctValuesCap1.Select("CurriculumID= " & Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)))
                        If drCap1.Length > 0 Then

                            If isCap1 = False Then
                                canEnroll = False
                            End If
                        End If
                        Dim drCap2() As DataRow = distinctValuesCap1.Select("CurriculumID=" & Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)))
                        If drCap2.Length > 0 Then
                            If isCap2 = False Then
                                Dim sCheckCap2CourseNotEnrolledSql As String = Database & "..spCheckStudentNotEnrolledCAP2__c @StudentID=" & sid
                                Dim bIsCap2EnrollFirstTime As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sCheckCap2CourseNotEnrolledSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If bIsCap2EnrollFirstTime Then
                                Else
                                    canEnroll = False

                                End If
                            End If
                        End If
                        Dim drFAE() As DataRow = distinctValuesCap1.Select("CurriculumID=" & Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)))
                        If drFAE.Length > 0 Then
                            If isFAE = False Then
                                canEnroll = False
                            End If
                        End If
                    End If
                    'Start- Siddharth: Added for redmine log #17839
                    If iSelectedCount <= 0 Then
                        canEnroll = False
                    End If
                    'End
                End If
            Next


            If canEnroll Then
                'BFP Performance: Moved this price update code in CourseEnrolments__c web service and setting session variable value to compare below
                'Added By Pradip 2016-06-08 
                'Dim TobeDistinct As String() = {"StudentGroupID", "AlternativeGroupID", "ClassID", "ProductID", "RouteOfEntryID"}
                'Dim dtDistPending As DataTable = dtPending.DefaultView.ToTable(True, TobeDistinct)
                'dtDistPending.Columns.Add("Price")
                '' dtDistPending.Columns.Add("ParentID")
                'For Each dr As DataRow In dtDistPending.Rows
                '    dr("Price") = GetPrice(CLng(dr("StudentGroupID").ToString()), CLng(dr("ProductID").ToString()), CLng(dr("AlternativeGroupID").ToString()), CLng(dr("ClassID").ToString()), CLng(dr("RouteOfEntryID").ToString()))
                '    ' dr("ParentID") = LoadParentStudentGroup(CInt(dr("StudentGroupID").ToString()))
                'Next
                'sSQL = Database & "..spUpdateEnrollmentStagingPriceNew__c"
                'Dim param2(0) As IDataParameter
                'For Each dr As DataRow In dtPending.Rows
                '    Dim ParentId As Integer = 0
                '    Dim filterRow() As DataRow = dtDistPending.Select("StudentGroupID='" & Convert.ToString(dr("StudentGroupID").ToString()) & "' AND AlternativeGroupID='" & Convert.ToString(dr("AlternativeGroupID")) & "'   AND ProductID='" & Convert.ToString(dr("ProductID")) & "' AND ClassID='" & Convert.ToString(dr("ClassID")) & "' AND RouteOfEntryID='" & Convert.ToString(dr("RouteOfEntryID")) & "' ")
                '    Dim price As Double = Convert.ToDecimal(filterRow(0)("Price"))
                '    dr("Price") = price
                '    If dr("ClassType").ToString().Trim().ToLower() = "exam" Or dr("ClassType").ToString().Trim().ToLower() = "interim assessment" Then
                '        If CInt(dr("StudentGroupID").ToString()) > 0 Then
                '            ParentId = CInt(Convert.ToString(dr("ParentID")))
                '            If ParentId > 0 Then
                '                dr("StudentGroupID") = ParentId
                '                dr("AlternativeGroupID") = 0
                '            Else
                '            End If
                '        ElseIf CInt(dr("AlternativeGroupID").ToString()) > 0 Then
                '            ParentId = CInt(Convert.ToString(dr("ParentID")))
                '            If ParentId > 0 Then
                '                dr("StudentGroupID") = 0
                '                dr("AlternativeGroupID") = ParentId
                '            Else
                '            End If
                '        End If
                '    Else
                '    End If
                '    '  Dim recordupdate As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param2, 180)
                'Next

                'Dim TobeDistinct2 As String() = {"IDEstage", "StudentID", "StudentGroupID", "AlternativeGroupID", "Price"}
                'Dim dtDistPending2 As DataTable = dtPending.DefaultView.ToTable(True, TobeDistinct2)

                'param2(0) = DataAction.GetDataParameter("@Pendingdetail", SqlDbType.Structured, dtDistPending2)
                'Dim recordupdate As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param2, 180)
                'Dim s As Integer = recordupdate

                'BFP Performance: Until session variable does not have value we will keep running while loop
                Dim iCnt As Integer = 0
                'BFP Performace: Added new condition to check hidden field value, which we are setting if there is a timeout for service call, if length is greater than 0 then stop while loop
                While (Session("UpdatePriceStatus").ToString.Length <= 0 AndAlso Request.Form("hdnTimeout").Length <= 0)
                    iCnt = iCnt + 1
                End While

                'BFP Performance: As soon as session variable gets a value will compare and proceed
                'BFP Performace: Added new condition to check hidden field value, which we are setting if there is a timeout for service call, if length is greater than 0 then show error
                If String.Compare(Session("UpdatePriceStatus").ToString, "Success", True) = 0 OrElse Request.Form("hdnTimeout").Length <= 0 Then
                    If Validate() Then
                        Dim url As New StringBuilder()
                        url.AppendFormat("{0}?AcademicYear={1}&EnrollMentType={2}&RouteOfEntry={3}&CurrentStage={4}&FilterBy={5}&SubjectFailed={6}&Loc={7}", Me.CourseEnrollmentSummaryURL, Me.AcademicCycleID, ddlEnrollmentType.SelectedValue, ddlRouteOfEntry.SelectedValue, ddlCurrentStage.SelectedValue, ddlCodesList.SelectedValue, ddlSubjectsFailed.SelectedValue, ddlLocation.SelectedValue)
                        Response.Redirect(url.ToString())
                        Return
                    Else
                        lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.ValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        rwValidation.VisibleOnPageLoad = True
                        hidValidation.Value = "Open"
                    End If
                Else
                    lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.PriceValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    rwValidation.VisibleOnPageLoad = True
                    hidValidation.Value = "Open"
                End If

            Else
                lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.MinimumUnits__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                rwValidation.VisibleOnPageLoad = True
                hidValidation.Value = "Open"
            End If
        Else
            ''Added By Pradip 2016-02-08 for RTO Firm
            If Validate() Then
                If IsRTOFirm() = True Then
                    Dim url As New StringBuilder()
                    url.AppendFormat("{0}?AcademicYear={1}&EnrollMentType={2}&RouteOfEntry={3}&CurrentStage={4}&FilterBy={5}&SubjectFailed={6}&Loc={7}", Me.CourseEnrollmentSummaryURL, Me.AcademicCycleID, ddlEnrollmentType.SelectedValue, ddlRouteOfEntry.SelectedValue, ddlCurrentStage.SelectedValue, ddlCodesList.SelectedValue, ddlSubjectsFailed.SelectedValue, ddlLocation.SelectedValue)
                    Response.Redirect(url.ToString())
                    Return
                End If
            Else
                lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.ValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                rwValidation.VisibleOnPageLoad = True
                hidValidation.Value = "Open"
            End If
        End If
        'Else

        '    lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.ValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        '    rwValidation.VisibleOnPageLoad = True

        'End If

    End Sub


    'Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
    '    Try
    '        rwValidation.VisibleOnPageLoad = False
    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    'End Sub


    ''' <summary>
    ''' Handles Back button to go back to manage my group url
    ''' </summary>
    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        'BFP Performance
        Response.Redirect(Me.FirmCourseEnrollmentLandingURL + "?Source=Enrolment")
        Return
    End Sub


    Protected Sub chkRecord_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim chkRecordGv As CheckBox = CType(sender, CheckBox)
            Dim sSQL As String
            sSQL = Database & "..spUpdateStudentsEnrollmentStagingOrderStatus__c"
            Dim param(6) As IDataParameter
            Dim sTUDENTid As String = Convert.ToString(chkRecordGv.Attributes("CommandArgument"))
            Dim nameData As String() = sTUDENTid.Split(New Char() {";"c})
            'param(0) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, CInt(chkRecordGv.Attributes("CommandArgument")))
            param(0) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, CInt(nameData(0)))

            If chkRecordGv.Checked Then
                param(1) = DataAction.GetDataParameter("@Status", SqlDbType.VarChar, "PrePending")
                param(2) = DataAction.GetDataParameter("@SetStatus", SqlDbType.VarChar, "Pending")
                param(5) = DataAction.GetDataParameter("@IsSelected", SqlDbType.Bit, 1)
            Else
                param(1) = DataAction.GetDataParameter("@Status", SqlDbType.VarChar, "Pending")
                param(2) = DataAction.GetDataParameter("@SetStatus", SqlDbType.VarChar, "PrePending")
                param(5) = DataAction.GetDataParameter("@IsSelected", SqlDbType.Bit, 0)
            End If
            param(3) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, CInt(loggedInUser.CompanyID))
            param(4) = DataAction.GetDataParameter("@CompanyAdminID", SqlDbType.Int, CInt(loggedInUser.PersonID))
            param(6) = DataAction.GetDataParameter("@AcademicCycleID", SqlDbType.Int, CInt(ddlAcademicYear.SelectedValue))
            Dim recordupdate As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param, 180)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnSubmitTimeTable_Click(sender As Object, e As System.EventArgs) Handles btnSubmitTimeTable.Click
        Try
            Dim sSQL As String
            If ValidateBeforeUpdateTimetable() Then
                Dim param(5) As IDataParameter
                param(0) = DataAction.GetDataParameter("@AcademicCyleId", SqlDbType.Int, CInt(ddlAcademicYear.SelectedValue))
                'param(1) = DataAction.GetDataParameter("@CurriculumName", SqlDbType.VarChar, ddlCurriculumList.SelectedItem.Text)
                param(1) = DataAction.GetDataParameter("@TimeTableId", SqlDbType.Int, CInt(ddlTimeTable.SelectedValue))
                param(2) = DataAction.GetDataParameter("@Status", SqlDbType.VarChar, "Pending")
                param(3) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, CInt(loggedInUser.CompanyID))
                param(4) = DataAction.GetDataParameter("@CurriculumNameID", SqlDbType.Int, CInt(ddlCurriculumList.SelectedValue))
                param(5) = DataAction.GetDataParameter("@Curriculum", SqlDbType.VarChar, ddlCurriculumList.SelectedItem.Text)
                ''Added BY Pradip 2016-04-13 For UAT Tracker item G3-P 28
                sSQL = ""
                sSQL = Database & "..spUpdateRoiNiStatusEnrollStaging__c"
                Dim recordupdate2 As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param, 180)
                sSQL = ""
                sSQL = Database & "..spUpdateEnrollmentStagingTimeTable__c"
                Dim recordupdate As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param, 180)
                'BFP Performance: Siddharth: Added below OR condition for scenario where on optional subjects are pending it was not showing success message inspite of updating correctly
                If recordupdate > 0 Or recordupdate2 > 0 Then
                    If Validate() Then
                        lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.TimeTableMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        rwValidation.VisibleOnPageLoad = True
                        hidValidation.Value = "Open"
                    Else
                        lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.PartialyUpdateTimetableMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        rwValidation.VisibleOnPageLoad = True
                        hidValidation.Value = "Open"
                    End If
                Else
                    lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.NoRecordUpdateTimetableMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    rwValidation.VisibleOnPageLoad = True
                    hidValidation.Value = "Open"
                End If
                ddlTimeTable.SelectedIndex = 0
                'ddlCurriculumList.SelectedIndex = 0
                'ddlTimeTable.Items.Clear()
                'ddlTimeTable.Items.Insert(0, "Select")
                'False
                'Keep users checkboxes selection
                _EnrollOnLoad = False
                _studentGroupListNew = New DataTable
                gvFirmCourseEnrollment.Rebind()

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnDisplay_Click(sender As Object, e As System.EventArgs) Handles btnDisplay.Click
        Try
            Dim sql As New StringBuilder()
            Sql.AppendFormat("{0} ..spCommonGetAllAcademicCycles__c", Me.Database)
            _academicCycleList = DataAction.GetDataTable(Sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            LoadPrePendingDataToStaging()
            _studentGroupListNew = New DataTable
            Dim param2(2) As IDataParameter
            Dim sSQL As String = Database & "..spUpdateEnrollmentStagingIsSelectedOrderStatus__c"
            param2(0) = DataAction.GetDataParameter("@Status", SqlDbType.VarChar, "PrePending")
            param2(1) = DataAction.GetDataParameter("@IsSelected", SqlDbType.Bit, 0)
            param2(2) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, CInt(loggedInUser.CompanyID))
            Dim recordupdate As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param2, 180)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        'true
        'Enroll student on eligible courses i.e check all checkboxes
        _EnrollOnLoad = True
        gvFirmCourseEnrollment.Rebind()
    End Sub

    Protected Sub btnApplyFAE_Click(sender As Object, e As System.EventArgs) Handles btnApplyFAE.Click
        If ddlFAEElectives.SelectedIndex = 0 Then
            lblFUMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.FAEElectiveValidation__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        Else
            lblFUMessage.Text = ""
            Dim recordupdate As Integer = 0
            Dim sSQL As String
            _courseEnrollmentDetails = CType(ViewState("_courseEnrollmentDetails"), DataTable)
            Dim dtStudents As DataTable = _courseEnrollmentDetails.DefaultView.ToTable(True, "StudentID")
            For Each drStudent As DataRow In dtStudents.Rows
                sSQL = String.Format("{0}..spUpdateFAEElectiveCourseSelection__c @StudentID={1}, @CourseID={2}, @CompanyId={3}, @CompanyAdminID={4}, @AcademicCycleID={5}", Database, drStudent("StudentID"), ddlFAEElectives.SelectedValue, loggedInUser.CompanyID, loggedInUser.PersonID, ddlAcademicYear.SelectedValue)
                recordupdate = recordupdate + DataAction.ExecuteNonQuery(sSQL, 180)
            Next
            If recordupdate > 0 Then
                lblFAEUpdatedMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.FAEElectiveSuccess__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.FAEElectiveSuccess__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                rwFAEMessage.VisibleOnPageLoad = True
                _EnrollOnLoad = False
                gvFirmCourseEnrollment.Rebind()

            Else
                lblFAEUpdatedMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.FAEElectiveFailed__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                rwFAEMessage.VisibleOnPageLoad = True
            End If
            ddlFAEElectives.SelectedIndex = 0
        End If
    End Sub

    Protected Sub btnFAEOK_Click(sender As Object, e As System.EventArgs) Handles btnFAEOK.Click
        Try
            rwFAEMessage.VisibleOnPageLoad = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

#Region "Course Enrollment Popup Events"

    ''' <summary>
    ''' Handles time table selected index changes event to reload grid
    ''' </summary>
    'Protected Sub ddlTimeTableList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTimeTableList.SelectedIndexChanged
    '    ''Function LoadStudentCourseEnrollmentStagingData Commented By Pradip 20150-07-28 For Below Change
    '    ' 1.When Time Table Change on Popup Window.
    '    'Then Update Orderstatus = 'Deselect' for order status in('Pending','PrePending')
    '    'Next Update
    '    'For Selected TimeTable Venue = Course Juridiction and if Isselected = 1 then Orderstatus = 'Pending'
    '    'if Isselected = 0 then orderstatus = 'Prepending'
    '    ' LoadStudentCourseEnrollmentStagingData()
    '    Try
    '        Dim sSql As String
    '        Dim param2(3) As IDataParameter
    '        sSql = Database & "..spUpdateEnrollmentStagingOnTimeTableChange__c"
    '        param2(0) = DataAction.GetDataParameter("@AcademicCycleID", SqlDbType.Int, CInt(Me.AcademicCycleID.ToString()))
    '        param2(1) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, CInt(Me.SelectedStudentID.ToString()))
    '        param2(2) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, CInt(loggedInUser.CompanyID.ToString()))
    '        param2(3) = DataAction.GetDataParameter("@TimeTableID", SqlDbType.Int, CInt(ddlTimeTableList.SelectedValue.ToString()))
    '        Dim recordupdate As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSql, CommandType.StoredProcedure, param2, 180)
    '    Catch ex As Exception
    '        lblPopupCourseEnrollmentMessage.Text = ex.Message
    '        lblPopupCourseEnrollmentMessage.ForeColor = Color.Red
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    '    LoadStudentCourseEnrollmentDataByStudent()
    '    gvCurriculumCourse.Rebind()
    'End Sub

    ''' <summary>
    ''' Handles item data bound event
    ''' </summary>
    Protected Sub gvCurriculumCourse_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvCurriculumCourse.ItemDataBound
        gvCurriculumCourse.MasterTableView.ColumnGroups.FindGroupByName("AutumnSession").HeaderText = "Autumn - " + ddlAcademicYear.SelectedItem.Text
        gvCurriculumCourse.MasterTableView.ColumnGroups.FindGroupByName("SummerSession").HeaderText = "Summer - " + ddlAcademicYear.SelectedItem.Text
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim isCore As Boolean = CBool(item.GetDataKeyValue("IsCore"))
            Dim CheckFAE As Boolean = CBool(item.GetDataKeyValue("checkFAE"))
            Dim isCourseJurisdiction As Boolean = CBool(item.GetDataKeyValue("IsCourseJurisdiction"))
            Dim isFAEElective As Boolean = CBool(item.GetDataKeyValue("IsFAEElective"))
            Dim isValidTimeSpan As Boolean = CBool(item.GetDataKeyValue("IsValidTimeSpan"))
            Dim bIsDeselect As Boolean = CBool(item.GetDataKeyValue("DeSelect"))

            If isCore = True Or isValidTimeSpan = False Then
                For i As Integer = 0 To item.Controls.Count - 1
                    TryCast(item.Controls(i), GridTableCell).Enabled = False
                Next
            End If
            If isFAEElective = True Then
                For i As Integer = 3 To item.Controls.Count - 1
                    TryCast(item.Controls(i), GridTableCell).Enabled = False
                Next
            End If
            ' Code added by govind
            ' below code for if FAE Elective course checked then FAEElective checkbox should true 
            Dim chkIsFAEElective As CheckBox = DirectCast(item.FindControl("chkIsFAEElective"), CheckBox)

            If CheckFAE Then
                chkIsFAEElective.Checked = True
            End If
            ' below code if fae elective check box disabled if chkIsFAEElective.Enabled true
            If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False Then
                For i As Integer = 0 To item.Controls.Count - 1
                    TryCast(item.Controls(i), GridTableCell).Enabled = False
                Next
            End If
            Dim AutumnExam As String = Convert.ToString(DataBinder.Eval(item.DataItem, "AutumnExam"))
            Dim chkRAutumnExamTxt() As String = AutumnExam.Split(CChar(";"))
            Dim RepeatRevisiontxt As String = Convert.ToString(DataBinder.Eval(item.DataItem, "RepeatRevision"))
            Dim chkRepeatRevisionTxt() As String = RepeatRevisiontxt.Split(CChar(";"))
            Dim ResitInterimAssessment As String = Convert.ToString(DataBinder.Eval(item.DataItem, "ResitInterimAssessment"))
            Dim chkResitInterimAssessmentTxt() As String = ResitInterimAssessment.Split(CChar(";"))
            Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkClassRoom As CheckBox = DirectCast(item.FindControl("chkClassRoom"), CheckBox)
            Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)
            Dim Revision As String = Convert.ToString(DataBinder.Eval(item.DataItem, "Revision"))
            Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)
            Dim chkRevisionTxt() As String = Revision.Split(CChar(";"))
            Dim ClassRoom As String = Convert.ToString(DataBinder.Eval(item.DataItem, "ClassRoom"))
            Dim chkClassRoomTxt() As String = ClassRoom.Split(CChar(";"))
            Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
            Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)
            Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)
            'If Convert.ToInt32(chkRevisionTxt(1)) > 0 Then
            '    chkRevision.Checked = False
            'End If
            If (Convert.ToInt32(chkRAutumnExamTxt(1)) > 0 AndAlso Convert.ToInt32(chkRAutumnExamTxt(0)) > 0) OrElse (Convert.ToInt32(chkRepeatRevisionTxt(1)) > 0 AndAlso Convert.ToInt32(chkRepeatRevisionTxt(0)) > 0) OrElse (Convert.ToInt32(chkResitInterimAssessmentTxt(1)) > 0 AndAlso Convert.ToInt32(chkResitInterimAssessmentTxt(0)) > 0) Then
                If chkClassRoom.Checked Then
                Else
                    chkInterimAssessment.Enabled = True
                End If

            End If
            Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
            If Convert.ToInt32(chkResitInterimAssessmentTxt(1)) > 0 AndAlso Convert.ToInt32(chkRepeatRevisionTxt(1)) <= 0 AndAlso Convert.ToInt32(chkRAutumnExamTxt(1)) <= 0 Then
                ' chkResitInterimAssessment.Checked = True
                If chkRepeatRevision.Checked Then
                    'Commented BY Pradip 2016-04-04 For Issue No 5524
                    'chkResitInterimAssessment.Enabled = False

                Else
                    chkResitInterimAssessment.Enabled = True

                End If
            End If
            If chkRepeatRevision.Checked Then
                'Commented BY Pradip 2016-04-04 For Issue No 5524
                'chkResitInterimAssessment.Enabled = False
            End If
            If Convert.ToInt32(chkRAutumnExamTxt(1)) > 0 OrElse Convert.ToInt32(chkRepeatRevisionTxt(1)) > 0 OrElse Convert.ToInt32(chkResitInterimAssessmentTxt(1)) > 0 Then
                chkClassRoom.Enabled = True
            End If

            If Convert.ToInt32(chkRevisionTxt(1)) > 0 AndAlso Convert.ToInt32(chkClassRoomTxt(1)) > 0 AndAlso chkClassRoom.Checked = False AndAlso chkRevision.Checked = False Then
                chkInterimAssessment.Enabled = True
                chkSummerExam.Enabled = True
            End If
            If chkRepeatRevision.Checked = False AndAlso Convert.ToInt32(chkRepeatRevisionTxt(1)) > 0 Then
                chkAutumnExam.Enabled = True
                chkResitInterimAssessment.Enabled = True
            End If

            If Convert.ToInt32(chkRevisionTxt(1)) > 0 And chkRevision.Checked = False Then
                chkMockExam.Enabled = True
            End If
            'If isFAEElective Then
            '    chkClassRoom.Checked = False
            '    chkInterimAssessment.Checked = False
            '    chkMockExam.Checked = False
            '    chkSummerExam.Checked = False
            'End If
            'item.Display = isCourseJurisdiction
            item.Display = bIsDeselect
        End If
    End Sub

    ''' <summary>
    ''' Handles curriculum courses grid - need data sourse event to bind student course enrollment data
    ''' </summary>
    Protected Sub gvCurriculumCourse_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCurriculumCourse.NeedDataSource
        gvCurriculumCourse.DataSource = _studentEnrollmentDetails
        'Siddharth: Added code to hide FAE Elective column if enrolment is not for FAE courses Redmine #15044
        If _studentEnrollmentDetails IsNot Nothing And _studentEnrollmentDetails.Rows.Count > 0 Then
            gvCurriculumCourse.MasterTableView.GetColumn("IsFAEElective").Display = False
            Dim drFAE() As DataRow = _studentEnrollmentDetails.Select("Curriculum='FAE- Final Admitting Exam'")
            If Not drFAE Is Nothing AndAlso drFAE.Length > 0 Then
                gvCurriculumCourse.MasterTableView.GetColumn("IsFAEElective").Display = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles submit button click event to save all enrollment requests
    ''' </summary>
    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        SubmitEnrollmentRequests()
    End Sub

    ''' <summary>
    ''' Handles the close button event to close course enrollment pop-up
    ''' </summary>
    Protected Sub btnCloseCourseEnrollment_Click(sender As Object, e As System.EventArgs) Handles btnCloseCourseEnrollment.Click
        rwCourseEnrollment.VisibleOnPageLoad = False
    End Sub

    ''' <summary>
    ''' Handles FAE elective check box checked event
    ''' </summary>
    Protected Sub chkIsFAEElective_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkSelectedIsFAEElective As CheckBox = DirectCast(sender, CheckBox)
        For Each item As GridItem In gvCurriculumCourse.MasterTableView.Items
            If TypeOf item Is GridDataItem Then
                Dim chkIsFAEElective As CheckBox = CType(item.FindControl("chkIsFAEElective"), CheckBox)
                If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = True AndAlso chkSelectedIsFAEElective.Checked = True Then
                    For i As Integer = 0 To item.Controls.Count - 1
                        TryCast(item.Controls(i), GridTableCell).Enabled = True
                    Next
                End If
                If chkIsFAEElective.Enabled = True AndAlso chkIsFAEElective.Checked = False AndAlso chkSelectedIsFAEElective.Checked = False Then
                    TryCast(item.Controls(2), GridTableCell).Enabled = True
                    For i As Integer = 3 To item.Controls.Count - 1
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

    ''' <summary>
    ''' Handles course enrollment window - chekbox checked event to save the request to enroll
    ''' </summary>
    Protected Sub chkClassRoom_CheckedChanged(sender As Object, e As EventArgs)
        Dim isEnrolled As CheckBox = DirectCast(sender, CheckBox)
        If Not String.IsNullOrEmpty(isEnrolled.Text) Then
            Dim nameData As String() = isEnrolled.Text.Split(New Char() {";"c})
            Dim item As GridDataItem = DirectCast(isEnrolled.NamingContainer, GridDataItem)
            Dim chkClassRoom As CheckBox = CType(item.FindControl("chkClassRoom"), CheckBox)
            Dim chkInterimAssessment As CheckBox = CType(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkMockExam As CheckBox = CType(item.FindControl("chkMockExam"), CheckBox)
            Dim chkSummerExam As CheckBox = CType(item.FindControl("chkSummerExam"), CheckBox)
            Dim chkRevision As CheckBox = CType(item.FindControl("chkRevision"), CheckBox)
            Dim chkRepeatRevision As CheckBox = CType(item.FindControl("chkRepeatRevision"), CheckBox)
            Dim chkResitInterimAssessment As CheckBox = CType(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim chkAutumnExam As CheckBox = CType(item.FindControl("chkAutumnExam"), CheckBox)
            ''Added BY Pradip 2016-04-04 for Issue 5371
            chkResitInterimAssessment.Enabled = True
            If chkClassRoom.Checked = True Then
                'Check mandatory exam classes section
                chkInterimAssessment.Checked = CBool(IIf(chkInterimAssessment.Visible = True, True, chkInterimAssessment.Checked))
                chkMockExam.Checked = CBool(IIf(chkMockExam.Visible = True, True, chkMockExam.Checked))
                chkSummerExam.Checked = CBool(IIf(chkSummerExam.Visible = True, True, chkSummerExam.Checked))

                'Uncheck autumn/revision section
                chkRepeatRevision.Checked = CBool(IIf(chkRepeatRevision.Visible = True, False, chkRepeatRevision.Checked))
                chkResitInterimAssessment.Checked = CBool(IIf(chkResitInterimAssessment.Visible = True, False, chkResitInterimAssessment.Checked))
                chkAutumnExam.Checked = CBool(IIf(chkAutumnExam.Visible = True, False, chkResitInterimAssessment.Checked))

                chkRevision.Checked = CBool(IIf(chkRevision.Visible = True, False, chkRevision.Checked))
                'Disable autumn/revision section
                chkRevision.Enabled = CBool(IIf(chkRevision.Visible = True, True, chkRevision.Enabled))
                chkRepeatRevision.Enabled = CBool(IIf(chkRepeatRevision.Visible = True, True, chkRepeatRevision.Enabled))
                'Commented By Pradip 2016-04-04 For Issue No.5371
                'chkResitInterimAssessment.Enabled = CBool(IIf(chkResitInterimAssessment.Visible = True, False, chkResitInterimAssessment.Enabled))
                chkAutumnExam.Enabled = CBool(IIf(chkAutumnExam.Visible = True, False, chkAutumnExam.Enabled))
                chkInterimAssessment.Enabled = False
                chkSummerExam.Enabled = False
                ''Added by pradip 2016-04-04 for G3-53 Tracker Item
                chkMockExam.Enabled = False
            ElseIf chkClassRoom.Checked = False Then

                'Uncheck mandatory exam classes section
                chkInterimAssessment.Checked = CBool(IIf(chkInterimAssessment.Visible = True, False, chkInterimAssessment.Checked))
                chkMockExam.Checked = CBool(IIf(chkMockExam.Visible = True, False, chkMockExam.Checked))
                chkSummerExam.Checked = CBool(IIf(chkSummerExam.Visible = True, False, chkSummerExam.Checked))

                'Enable autumn/revision section
                chkRevision.Enabled = CBool(IIf(chkRevision.Visible = True, True, chkRevision.Enabled))
                chkRepeatRevision.Enabled = CBool(IIf(chkRepeatRevision.Visible = True, True, chkRepeatRevision.Enabled))
                chkResitInterimAssessment.Enabled = CBool(IIf(chkResitInterimAssessment.Visible = True, True, chkResitInterimAssessment.Enabled))
                chkAutumnExam.Enabled = CBool(IIf(chkAutumnExam.Visible = True, True, chkAutumnExam.Enabled))

                If chkRevision.Enabled = True Then
                    chkInterimAssessment.Enabled = True
                    chkSummerExam.Enabled = True
                    ' ''Added By Pradip 2016-04-04 For Issue No 5371 made True
                    chkMockExam.Enabled = True
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles course enrollment window - chekbox checked event to save the request to enroll
    ''' </summary>
    Protected Sub chkRevision_CheckedChanged(sender As Object, e As EventArgs)
        Dim isEnrolled As CheckBox = DirectCast(sender, CheckBox)
        If Not String.IsNullOrEmpty(isEnrolled.Text) Then
            Dim nameData As String() = isEnrolled.Text.Split(New Char() {";"c})
            Dim item As GridDataItem = DirectCast(isEnrolled.NamingContainer, GridDataItem)
            Dim chkClassRoom As CheckBox = CType(item.FindControl("chkClassRoom"), CheckBox)
            Dim chkInterimAssessment As CheckBox = CType(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkMockExam As CheckBox = CType(item.FindControl("chkMockExam"), CheckBox)
            Dim chkSummerExam As CheckBox = CType(item.FindControl("chkSummerExam"), CheckBox)
            Dim chkRevision As CheckBox = CType(item.FindControl("chkRevision"), CheckBox)
            Dim chkRepeatRevision As CheckBox = CType(item.FindControl("chkRepeatRevision"), CheckBox)
            Dim chkResitInterimAssessment As CheckBox = CType(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim chkAutumnExam As CheckBox = CType(item.FindControl("chkAutumnExam"), CheckBox)
            If chkRevision.Checked = True Then
                'Check mandatory exam
                chkSummerExam.Checked = CBool(IIf(chkSummerExam.Visible = True, True, chkSummerExam.Checked))
                chkInterimAssessment.Checked = CBool(IIf(chkInterimAssessment.Visible = True, True, chkInterimAssessment.Checked))

                'Uncheck summary fresh section
                chkClassRoom.Checked = CBool(IIf(chkClassRoom.Visible = True, False, chkClassRoom.Checked))
                chkRepeatRevision.Checked = CBool(IIf(chkRepeatRevision.Visible = True, False, chkRepeatRevision.Checked))
                chkMockExam.Checked = CBool(IIf(chkMockExam.Visible = True, False, chkMockExam.Checked))

                'Uncheck autumn section
                chkResitInterimAssessment.Checked = CBool(IIf(chkResitInterimAssessment.Visible = True, False, chkResitInterimAssessment.Checked))
                chkAutumnExam.Checked = CBool(IIf(chkAutumnExam.Visible = True, False, chkAutumnExam.Checked))

                'Disable summary fresh section
                chkClassRoom.Enabled = CBool(IIf(chkClassRoom.Visible = True, True, chkClassRoom.Enabled))
                chkSummerExam.Enabled = CBool(IIf(chkSummerExam.Visible = True, False, chkSummerExam.Enabled))

                'Enable IA
                chkInterimAssessment.Enabled = CBool(IIf(chkInterimAssessment.Visible = True, False, chkInterimAssessment.Enabled))

                'Disable autumn section
                'chkRepeatRevision.Enabled = CBool(IIf(chkRepeatRevision.Visible = True, False, chkRepeatRevision.Enabled))
                chkResitInterimAssessment.Enabled = CBool(IIf(chkResitInterimAssessment.Visible = True, False, chkResitInterimAssessment.Enabled))
                chkAutumnExam.Enabled = CBool(IIf(chkAutumnExam.Visible = True, False, chkAutumnExam.Enabled))
                ''Added By Pradip 2016-04-04 For G3 Tracker G3-53
                chkMockExam.Checked = True
                chkMockExam.Enabled = False

                'added Pradip 2016-03-23 For MidFeb-9
                If nameData(0) = "22" Then
                    'chkInterimAssessment.Checked = True
                    ''Commented BY  and added Pradip 2016-03-23 For MidFeb-9
                    chkInterimAssessment.Enabled = True
                    btnIAWarning.Visible = True
                    lblSubmitMessage.Text = ""
                    lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.OptOutIA")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radwindowIAOpt.VisibleOnPageLoad = True
                End If
            ElseIf chkRevision.Checked = False Then

                chkSummerExam.Checked = CBool(IIf(chkSummerExam.Visible = True, False, chkSummerExam.Checked))
                chkInterimAssessment.Checked = CBool(IIf(chkInterimAssessment.Visible = True, False, chkInterimAssessment.Checked))

                'Enable summer section
                chkClassRoom.Enabled = CBool(IIf(chkClassRoom.Visible = True, True, chkClassRoom.Enabled))

                'Disable IA
                chkInterimAssessment.Enabled = CBool(IIf(chkInterimAssessment.Visible = True, True, chkInterimAssessment.Enabled))
                chkSummerExam.Enabled = CBool(IIf(chkSummerExam.Visible = True, True, chkSummerExam.Enabled))
                'Enable autumn section
                chkRepeatRevision.Enabled = CBool(IIf(chkRepeatRevision.Visible = True, True, chkRepeatRevision.Enabled))
                chkResitInterimAssessment.Enabled = CBool(IIf(chkResitInterimAssessment.Visible = True, True, chkResitInterimAssessment.Enabled))
                chkAutumnExam.Enabled = CBool(IIf(chkAutumnExam.Visible = True, True, chkAutumnExam.Enabled))
                ''Added By Pradip 2016-04-04 For G3 Tracker G3-53
                chkMockExam.Checked = False
                chkMockExam.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles course enrollment window - chekbox checked event to save the request to enroll
    ''' </summary>
    Protected Sub chkRepeatRevision_CheckedChanged(sender As Object, e As EventArgs)
        Dim isEnrolled As CheckBox = DirectCast(sender, CheckBox)
        If Not String.IsNullOrEmpty(isEnrolled.Text) Then
            Dim nameData As String() = isEnrolled.Text.Split(New Char() {";"c})
            Dim item As GridDataItem = DirectCast(isEnrolled.NamingContainer, GridDataItem)
            Dim chkClassRoom As CheckBox = CType(item.FindControl("chkClassRoom"), CheckBox)
            Dim chkInterimAssessment As CheckBox = CType(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkMockExam As CheckBox = CType(item.FindControl("chkMockExam"), CheckBox)
            Dim chkSummerExam As CheckBox = CType(item.FindControl("chkSummerExam"), CheckBox)
            Dim chkRevision As CheckBox = CType(item.FindControl("chkRevision"), CheckBox)
            Dim chkRepeatRevision As CheckBox = CType(item.FindControl("chkRepeatRevision"), CheckBox)
            Dim chkResitInterimAssessment As CheckBox = CType(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim chkAutumnExam As CheckBox = CType(item.FindControl("chkAutumnExam"), CheckBox)
            If chkRepeatRevision.Checked = True Then

                'Check mandatory exams
                chkResitInterimAssessment.Checked = True
                chkAutumnExam.Checked = True

                'Uncheck all summer classes
                chkClassRoom.Checked = False
                chkRevision.Checked = False
                chkInterimAssessment.Checked = False
                chkMockExam.Checked = False
                chkSummerExam.Checked = False

                'Disable all summer classes
                '  chkClassRoom.Enabled = False
                '  chkRevision.Enabled = False
                chkInterimAssessment.Enabled = False
                chkMockExam.Enabled = False
                chkSummerExam.Enabled = False

                'Disable autumn exams
                ' chkResitInterimAssessment.Enabled = False
                'If chkResitInterimAssessmentTxt(0) > 0 AndAlso chkRepeatRevisionTxt(0) <= 0 AndAlso chkRAutumnExamTxt(0) <= 0 Then
                '    ' chkResitInterimAssessment.Checked = True
                '    chkResitInterimAssessment.Enabled = True
                'End If
                chkAutumnExam.Enabled = False
                chkResitInterimAssessment.Enabled = False
                'added Pradip 2016-03-23 For MidFeb-9
                If nameData(0) = "22" Then
                    'chkInterimAssessment.Checked = True
                    ''Commented BY  and added Pradip 2016-03-23 For MidFeb-9
                    chkResitInterimAssessment.Enabled = True
                    btnIAWarning.Visible = True
                    lblSubmitMessage.Text = ""
                    lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.OptOutIA")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radwindowIAOpt.VisibleOnPageLoad = True
                End If

            ElseIf chkRepeatRevision.Checked = False Then
                'Uncheck mandatory exams
                chkResitInterimAssessment.Checked = False
                chkAutumnExam.Checked = False

                'Enable all summer classes
                chkClassRoom.Enabled = True
                chkRevision.Enabled = True
                chkInterimAssessment.Enabled = True
                chkSummerExam.Enabled = True

                'Enable autumn exams
                chkResitInterimAssessment.Enabled = True
                chkAutumnExam.Enabled = True

            End If
        End If
    End Sub


    Protected Sub chkResitInterimAssessment_CheckedChanged(sender As Object, e As EventArgs)
        Dim isEnrolled As CheckBox = DirectCast(sender, CheckBox)
        If Not String.IsNullOrEmpty(isEnrolled.Text) Then
            Dim nameData As String() = isEnrolled.Text.Split(New Char() {";"c})
            Dim item As GridDataItem = DirectCast(isEnrolled.NamingContainer, GridDataItem)

            Dim chkRepeatRevision As CheckBox = CType(item.FindControl("chkRepeatRevision"), CheckBox)
            Dim chkResitInterimAssessment As CheckBox = CType(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim chkAutumnExam As CheckBox = CType(item.FindControl("chkAutumnExam"), CheckBox)
            If chkResitInterimAssessment.Checked = True Then

                'Check mandatory exams
                chkResitInterimAssessment.Checked = True
                chkAutumnExam.Checked = True
                chkAutumnExam.Enabled = False

            ElseIf chkResitInterimAssessment.Checked = False Then
                'Uncheck mandatory exams
                chkResitInterimAssessment.Checked = False
                chkAutumnExam.Checked = False
                'Enable autumn exams
                chkResitInterimAssessment.Enabled = True
                chkAutumnExam.Enabled = True

            End If
            ''Added By Pradip 2016-04-05
            If chkRepeatRevision.Checked = False And chkResitInterimAssessment.Checked = False Then
                ' If Convert.ToInt32(nameData(1)) > 0 Then
                chkAutumnExam.Checked = False
                chkAutumnExam.Enabled = True
                'End If
            ElseIf chkRepeatRevision.Checked = True And chkResitInterimAssessment.Checked = False Then
                chkAutumnExam.Checked = True
                chkAutumnExam.Enabled = False
            End If

        End If
    End Sub

    Protected Sub chkSummerExam_CheckedChanged(sender As Object, e As EventArgs)
        Dim isEnrolled As CheckBox = DirectCast(sender, CheckBox)
        If Not String.IsNullOrEmpty(isEnrolled.Text) Then
            Dim nameData As String() = isEnrolled.Text.Split(New Char() {";"c})
            Dim item As GridDataItem = DirectCast(isEnrolled.NamingContainer, GridDataItem)

            Dim chkInterimAssessment As CheckBox = CType(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkRevision As CheckBox = CType(item.FindControl("chkRevision"), CheckBox)
            If isEnrolled.Checked = True Then
                chkRevision.Checked = False

            ElseIf isEnrolled.Checked = False Then
                If chkInterimAssessment.Enabled = True Then
                    chkInterimAssessment.Checked = False
                End If
            End If
        End If

    End Sub
    Protected Sub chkAutumnExam_CheckedChanged(sender As Object, e As EventArgs)
        Dim isEnrolled As CheckBox = DirectCast(sender, CheckBox)
        If Not String.IsNullOrEmpty(isEnrolled.Text) Then
            Dim nameData As String() = isEnrolled.Text.Split(New Char() {";"c})
            Dim item As GridDataItem = DirectCast(isEnrolled.NamingContainer, GridDataItem)

            Dim chkResitInterimAssessment As CheckBox = CType(item.FindControl("chkResitInterimAssessment"), CheckBox)

            If isEnrolled.Checked = True Then
            ElseIf isEnrolled.Checked = False Then
                If chkResitInterimAssessment.Enabled = True Then
                    chkResitInterimAssessment.Checked = False
                End If
            End If
        End If

    End Sub

    Protected Sub chkInterimAssessment_CheckedChanged(sender As Object, e As EventArgs)
        Dim isEnrolled As CheckBox = DirectCast(sender, CheckBox)
        If Not String.IsNullOrEmpty(isEnrolled.Text) Then
            Dim nameData As String() = isEnrolled.Text.Split(New Char() {";"c})
            Dim item As GridDataItem = DirectCast(isEnrolled.NamingContainer, GridDataItem)
            Dim chkSummerExam As CheckBox = CType(item.FindControl("chkSummerExam"), CheckBox)
            Dim chkRevision As CheckBox = CType(item.FindControl("chkRevision"), CheckBox)
            If isEnrolled.Checked = True Then
                'Check mandatory exams
                chkSummerExam.Checked = True
                chkSummerExam.Enabled = False
                chkRevision.Checked = False
            ElseIf isEnrolled.Checked = False Then
                'Uncheck mandatory exams
                isEnrolled.Checked = False
                chkSummerExam.Checked = False
                'Enable autumn exams
                isEnrolled.Enabled = True
                chkSummerExam.Enabled = True
            End If
            ''Added By Pradip 2016-04-04 for Issue No 5371
            Dim chkMockExam As CheckBox = DirectCast(item.FindControl("chkMockExam"), CheckBox)
            If chkMockExam.Checked = True Then
                chkSummerExam.Checked = True
                chkSummerExam.Enabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Added BY Pradip Chavhan 2016-03-04 for G3-55 Tracker Item
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkMockExam_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkMockExam As CheckBox = DirectCast(sender, CheckBox)
            Dim item As GridDataItem = DirectCast(chkMockExam.NamingContainer, GridDataItem)
            Dim chkRevision As CheckBox = DirectCast(item.FindControl("chkRevision"), CheckBox)
            ''Added BY Pradip 2016-01-09 For Group 3 Tracker G3-53
            Dim chkSummerExam As CheckBox = DirectCast(item.FindControl("chkSummerExam"), CheckBox)
            ''Added BY Pradip 2016-02-12 For Issue NO 5371
            Dim chkInterimAssessment As CheckBox = DirectCast(item.FindControl("chkInterimAssessment"), CheckBox)
            Dim chkRepeatRevision As CheckBox = DirectCast(item.FindControl("chkRepeatRevision"), CheckBox)
            Dim chkResitInterimAssessment As CheckBox = DirectCast(item.FindControl("chkResitInterimAssessment"), CheckBox)
            Dim chkAutumnExam As CheckBox = DirectCast(item.FindControl("chkAutumnExam"), CheckBox)
            If chkMockExam.Checked Then
                If chkRevision.Checked Then
                    chkRevision.Checked = False
                End If
                ''Added BY Pradip 2016-01-09 For Group 3 Tracker G3-53
                chkSummerExam.Checked = True
                chkSummerExam.Enabled = False
                ''Added BY Pradip 2016-02-12 for Issue 5371
                chkResitInterimAssessment.Enabled = True
                'If chkAutumnExam.Checked Then
                '    If Convert.ToInt32(chkAutumnExamtxt(1)) > 0 Then
                '        chkAutumnExam.Checked = False
                '        chkAutumnExam.Enabled = True

                '    End If
                'End If
            Else
                If chkInterimAssessment.Checked = True Then
                    chkSummerExam.Checked = True
                    chkSummerExam.Enabled = False
                Else
                    chkSummerExam.Checked = False
                    chkSummerExam.Enabled = True
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''Added BY Pradip 2016-04-04 for MidFeb-9
    Protected Sub btnIAWarning_Click(sender As Object, e As System.EventArgs) Handles btnIAWarning.Click
        radwindowIAOpt.VisibleOnPageLoad = False
    End Sub
#End Region

#Region "Break Down Popup Events"


    ''' <summary>
    ''' Handles the close button event to close course enrollment pop-up
    ''' </summary>
    Protected Sub btnCloseBreakDown_Click(sender As Object, e As System.EventArgs) Handles btnCloseBreakDown.Click
        rwStudentBreakDown.VisibleOnPageLoad = False
    End Sub

    ''' <summary>
    ''' Hanles completed curriculum grid - need data sourse event to bind data
    ''' </summary>
    Protected Sub gvCompletedCurriculum_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCompletedCurriculum.NeedDataSource
        _studentBreakDownDetails = CType(ViewState("_studentBreakDownDetails"), DataSet)
        If _studentBreakDownDetails IsNot Nothing AndAlso _studentBreakDownDetails.Tables.Count > 0 Then
            _completedCurriculum = _studentBreakDownDetails.Tables(0)
            gvCompletedCurriculum.DataSource = _completedCurriculum
        End If
    End Sub

    ''' <summary>
    ''' Handles completed courses grid - need data sourse event to bind data
    ''' </summary>
    Protected Sub gvCompletedCourses_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCompletedCourses.NeedDataSource
        _studentBreakDownDetails = CType(ViewState("_studentBreakDownDetails"), DataSet)
        If _studentBreakDownDetails IsNot Nothing AndAlso _studentBreakDownDetails.Tables.Count > 0 Then
            _completedCourses = _studentBreakDownDetails.Tables(1)
            gvCompletedCourses.DataSource = _completedCourses
        End If
    End Sub

    ''' <summary>
    ''' Handles failed courses grid - need data sourse event to bind data
    ''' </summary>
    Protected Sub gvFailedCourses_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvFailedCourses.NeedDataSource
        _studentBreakDownDetails = CType(ViewState("_studentBreakDownDetails"), DataSet)
        If _studentBreakDownDetails IsNot Nothing AndAlso _studentBreakDownDetails.Tables.Count > 0 Then
            _failedCourses = _studentBreakDownDetails.Tables(2)
            gvFailedCourses.DataSource = _failedCourses
        End If
    End Sub

    ''' <summary>
    ''' Handles current courses grid - need data sourse event to bind data
    ''' </summary>
    Protected Sub gvCurrentCourses_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCurrentCourses.NeedDataSource
        _studentBreakDownDetails = CType(ViewState("_studentBreakDownDetails"), DataSet)
        If _studentBreakDownDetails IsNot Nothing AndAlso _studentBreakDownDetails.Tables.Count > 0 Then
            _currentCourses = _studentBreakDownDetails.Tables(3)
            gvCurrentCourses.DataSource = _currentCourses
        End If
    End Sub

    ''' <summary>
    ''' Handles current exams grid - need data sourse event to bind data
    ''' </summary>
    Protected Sub gvCurrentExams_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCurrentExams.NeedDataSource
        _studentBreakDownDetails = CType(ViewState("_studentBreakDownDetails"), DataSet)
        If _studentBreakDownDetails IsNot Nothing AndAlso _studentBreakDownDetails.Tables.Count > 0 Then
            _currentExams = _studentBreakDownDetails.Tables(4)
            gvCurrentExams.DataSource = _currentExams
        End If
    End Sub

    ''' <summary>
    ''' Handles IA Resit courses grid - need data sourse event to bind data
    ''' </summary>
    Protected Sub gvIAResits_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvIAResits.NeedDataSource
        _studentBreakDownDetails = CType(ViewState("_studentBreakDownDetails"), DataSet)
        If _studentBreakDownDetails IsNot Nothing AndAlso _studentBreakDownDetails.Tables.Count > 0 Then
            _currentIAResits = _studentBreakDownDetails.Tables(5)
            gvIAResits.DataSource = _currentIAResits
        End If
    End Sub

#End Region

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Run the run engine
    ''' </summary>
    Private Sub RunRuleEngine()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spClearRuleEngineData__c @WebUserID={1},@CompanyID={2}", Me.Database, loggedInUser.PersonID, loggedInUser.CompanyID)
            Me.DataAction.ExecuteNonQuery(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            _ruleEngine = New Aptify.Consulting.RuleEngine__c(Me.DataAction, Me.AptifyApplication)
            '_ruleEngineResult = _ruleEngine.CheckRuleForFirm(Me.AcademicCycleID, CInt(loggedInUser.CompanyID), CInt(loggedInUser.PersonID))
            _ruleEngineResult = _ruleEngine.CheckRuleForFirm(LoadPreviousAcademicCycleID(), CInt(loggedInUser.CompanyID), CInt(loggedInUser.PersonID))
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load all academic cycles 
    ''' </summary>
    Private Sub LoadAcademicYears()
        Try
            Dim sql As New StringBuilder()
            Dim currentAcademicCycleId As Integer
            sql.AppendFormat("{0} ..spCommonGetAllAcademicCycles__c", Me.Database)
            _academicCycleList = DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlAcademicYear.DataSource = _academicCycleList
            ddlAcademicYear.DataValueField = "Id"
            ddlAcademicYear.DataTextField = "Name"
            ddlAcademicYear.DataBind()
            currentAcademicCycleId = LoadCurrentAcademicCycleID()
            If currentAcademicCycleId <> 0 Then
                ddlAcademicYear.SelectedValue = CStr(currentAcademicCycleId)
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load all academic cycles 
    ''' </summary>
    Private Sub LoadEnrollmentCodes()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetEnrollmentCodes__c", Me.Database)
            ddlCodesList.DataSource = DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlCodesList.DataValueField = "Number"
            ddlCodesList.DataTextField = "Code"
            ddlCodesList.DataBind()
            ''Added By Pradip 2015-07-15 To Set "Current Options for Enrollment" as default value
            'If ddlCodesList.Items.Count > 1 Then
            '    ddlCodesList.SelectedValue = ddlCodesList.Items.FindByText("Current Options for Enrolment").Value()
            'End If

        Catch ex As Exception
            lblMessage.Text = ex.Message
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load student course enrollment data
    ''' </summary>
    Private Function LoadStudentCourseEnrollmentData() As DataTable
        Try
            lblNoRecords.Text = String.Empty
            lblMessage.Text = String.Empty
            Dim sql As New StringBuilder()
            'sql.AppendFormat("{0} ..spGetStudentCourseEnrollment__c @AcademicCycleID={1},@CompanyID={2},@FilerById={3}", _
            '                 Me.Database, ddlAcademicYear.SelectedValue.ToString(), loggedInUser.CompanyID.ToString(), ddlCodesList.SelectedValue.ToString())
            sql.AppendFormat("{0} ..spGetStudentCourseEnrollmentFiltered__c @CompanyID={1},@CompanyAdminID={2},@AcademicCycleID={3},@EnrollmentType='{4}',@RouteOfEntry='{5}'," & _
                             "@CurrentStage='{6}',@FilerById='{7}',@SubjectFailed='{8}',@LocationID='{9}'", _
                             Me.Database, loggedInUser.CompanyID.ToString(), loggedInUser.PersonID.ToString(), ddlAcademicYear.SelectedValue.ToString(), ddlEnrollmentType.SelectedValue, _
                             ddlRouteOfEntry.SelectedValue, ddlCurrentStage.SelectedValue, ddlCodesList.SelectedValue, ddlSubjectsFailed.SelectedValue, ddlLocation.SelectedValue)
            _courseEnrollmentDetails = Me.DataAction.GetDataTable(sql.ToString(), 600)
            ViewState("_courseEnrollmentDetails") = _courseEnrollmentDetails
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return _courseEnrollmentDetails
    End Function

    ''' <summary>
    ''' Set OrderStatus as 'Pending' and Select checkbox for filtered data
    ''' </summary>
    Private Sub EnrollStudentOnCourseData()
        Try
            lblMessage.Text = String.Empty
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spEnrollStudentFilteredCourse__c @CompanyID={1},@CompanyAdminID={2},@AcademicCycleID={3},@EnrollmentType='{4}',@RouteOfEntry='{5}'," & _
                             "@CurrentStage='{6}',@FilerById='{7}',@SubjectFailed='{8}',@LocationID='{9}'", _
                             Me.Database, loggedInUser.CompanyID.ToString(), loggedInUser.PersonID.ToString(), ddlAcademicYear.SelectedValue.ToString(), ddlEnrollmentType.SelectedValue, _
                             ddlRouteOfEntry.SelectedValue, ddlCurrentStage.SelectedValue, ddlCodesList.SelectedValue, ddlSubjectsFailed.SelectedValue, ddlLocation.SelectedValue)
            Dim iCnt As Integer = DataAction.ExecuteNonQuery(sql.ToString(), 600)
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''Added BY Pradip 2016-08-11 for saving Persons Current Stage

    Private Sub SavePersonsStageDetails()
        Try
            lblMessage.Text = String.Empty
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spSavePersonsStageDetails__c @CompanyID={1},@AcademicCycleID={2},@CompanyAdminID={3}", _
                             Me.Database, loggedInUser.CompanyID.ToString(), ddlAcademicYear.SelectedValue.ToString(), loggedInUser.PersonID.ToString())
            Dim iCnt As Integer = DataAction.ExecuteNonQuery(sql.ToString(), 680)
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Load student course enrollment data - for selected student
    ''' </summary>
    Private Sub LoadStudentCourseEnrollmentStagingData()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetStudentCourseEnrollmentStaging__c @AcademicCycleID={1},@StudentID={2},@CompanyID={3}", Me.Database, Me.AcademicCycleID.ToString(), Me.SelectedStudentID, loggedInUser.CompanyID.ToString())
            _clearStagingDetails = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            For Each row As DataRow In _clearStagingDetails.Rows
                Dim enrollmentStaging As AptifyGenericEntityBase = Me.AptifyApplication.GetEntityObject("EnrollmentStaging__c", CInt(row("RecordID")))
                enrollmentStaging.Delete()
            Next
        Catch ex As Exception
            lblPopupCourseEnrollmentMessage.Text = ex.Message
            lblPopupCourseEnrollmentMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load student course enrollment data - for selected student
    ''' </summary>
    Private Sub LoadStudentCourseEnrollmentDataByStudent()
        Try
            lblNoRecords.Text = String.Empty
            _studentEnrollmentDetails = Nothing
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetStudentCourseEnrollmentByStudent__c @AcademicCycleID={1},@CompanyID={2},@StudentID={3},@StudentGroupID={4}", Me.Database, Me.AcademicCycleID.ToString(), loggedInUser.CompanyID.ToString(), Me.SelectedStudentID, Me.StudentGroupID)
            _studentEnrollmentDetails = Me.DataAction.GetDataTable(sql.ToString(), 600)
            ViewState("SelectedStudentID") = Me.SelectedStudentID
        Catch ex As Exception
            lblPopupCourseEnrollmentMessage.Text = ex.Message
            lblPopupCourseEnrollmentMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load student course enrollment data
    ''' </summary>
    Private Sub LoadStudentEnrollment()
        Try
            Dim canChangeStudentGroup As Boolean = True
            _courseEnrollmentDetails = CType(ViewState("_courseEnrollmentDetails"), DataTable)
            Dim studentInfo As DataRow = _courseEnrollmentDetails.Select("StudentID = " & Me.SelectedStudentID).First()
            ' txtFirstName.Text = CStr(studentInfo("FirstName"))
            lblFirstName.Text = CStr(studentInfo("FirstName"))
            ' txtRoute.Text = CStr(studentInfo("RouteOfEntry"))
            lblLastName.Text = CStr(studentInfo("LastName"))
            lblRouteOfEntry.Text = CStr(studentInfo("RouteOfEntry"))
            Me.RouteOfEntryID = CInt(studentInfo("RouteOfEntryID"))
            lblStudentNo.Text = CStr(studentInfo("StudentNumber"))

            'Added BY Pradip 2015-08-26 / Changed by Siddharth to get exact curriculum ID
            LoadStudentCourseEnrollmentDataByStudent()
            'Dim drFilterarray() As DataRow
            'Dim filterExp As String = "IsEnrolled In (44,22,88,8) AND StudentID = " & Me.SelectedStudentID
            'drFilterarray = _courseEnrollmentDetails.Select(filterExp, Nothing, DataViewRowState.CurrentRows)
            ''Commented By Pradip 2015-08-26 To Curriculum from Enollment Staging
            ' LoadStudentGroups(studentInfo("Curriculum").ToString())
            'Added by Siddharth for other curriculums
            If _studentEnrollmentDetails.Rows.Count > 0 Then
                LoadStudentGroups(_studentEnrollmentDetails.Rows(0)("Curriculum").ToString().Trim())
            Else
                LoadStudentGroups(studentInfo("Curriculum").ToString())
            End If

            ddlTimeTableList.DataSource = _studentGroupList
            ddlTimeTableList.DataValueField = "ID"
            ddlTimeTableList.DataTextField = "Name"
            ddlTimeTableList.DataBind()
            ''Added Bt Pradip 2015-08-06 To Set Default Value
            ddlTimeTableList.Items.Insert(0, New System.Web.UI.WebControls.ListItem("--Select--", "0"))
            Dim studentGroupId As Integer = LoadDefaultSelectedGroup(String.Empty, canChangeStudentGroup)
            ddlTimeTableList.Enabled = canChangeStudentGroup
            ''Added By Pradip 2015-07-30 To Set Time Table From EnrollmentStaging__c
            'Dim idGroup As Integer = 0
            'Dim sql As New StringBuilder()
            'sql.AppendFormat("{0} ..spGetStudentGroupFromEnrollmentStaging__c @AcademicCyleId={1},@CompanyID={2},@StudentId={3}", Me.Database, Me.AcademicCycleID.ToString(), loggedInUser.CompanyID.ToString(), Me.SelectedStudentID)
            'idGroup = CInt(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            'Dim lstItem As System.Web.UI.WebControls.ListItem = ddlTimeTableList.Items.FindByValue(idGroup.ToString())
            'If Not lstItem Is Nothing Then
            '    If idGroup > 0 Then
            '        ddlTimeTableList.SelectedValue = idGroup.ToString()
            '    Else
            '        'If studentGroupId <> 0 Then
            '        '    ddlTimeTableList.SelectedValue = CStr(studentGroupId)
            '        'End If
            '        ddlTimeTableList.SelectedValue = CStr(studentGroupId)
            '    End If
            'Else
            '    'If studentGroupId <> 0 Then
            '    '    ddlTimeTableList.SelectedValue = CStr(studentGroupId)
            '    'End If
            '    ddlTimeTableList.SelectedValue = CStr(studentGroupId)
            'End If
            If studentGroupId <> 0 Then
                ddlTimeTableList.SelectedValue = CStr(studentGroupId)
                lblTimeTablePopup.Text = ddlTimeTableList.SelectedItem.Text.Trim
            End If
            ''Added BY Pradip  2016-04-14 For UAT Tracker Item G3P 28
            ddlTimeTableList.Enabled = False
            Me.StudentGroupID = studentGroupId
            LoadStudentCourseEnrollmentDataByStudent()
            gvCurriculumCourse.Rebind()
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load student course enrollment data
    ''' </summary>
    Private Sub LoadStudentGroups(ByVal sCurriculum As String)
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetStudentGroupEnrollment__c @CompanyID={1},@StudentID={2},@AcademicCycleID={3},@CurriculumName='{4}'", Me.Database, loggedInUser.CompanyID.ToString(), Me.SelectedStudentID, Me.AcademicCycleID, sCurriculum)
            _studentGroupList = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Set default select student group
    ''' </summary>
    Private Function LoadDefaultSelectedGroup(ByRef name As String, ByRef canChangeStudentGroup As Boolean) As Integer
        If _studentGroupList IsNot Nothing And _studentGroupList.Rows.Count > 0 Then
            canChangeStudentGroup = CBool(_studentGroupList.Rows.Item(0).Item("CanChangeStudentGroup"))
        End If
        Dim groupId As Integer = 0
        Dim rowStage = _studentGroupList.AsEnumerable().FirstOrDefault(Function(p) p.Field(Of Integer)("StagingGroupID") > 0)
        If rowStage IsNot Nothing Then
            groupId = CInt(rowStage.Field(Of Integer)("StagingGroupID").ToString())
            name = AptifyApplication.GetEntityRecordName("StudentGroups__c", rowStage.Field(Of Integer)("StagingGroupID"))
        Else
            If canChangeStudentGroup = False Then
                Dim row = _studentGroupList.AsEnumerable().SingleOrDefault(Function(p) p.Field(Of Integer)("IsCurriculumAppGroup") = 1)
                If row IsNot Nothing Then
                    groupId = CInt(row.Field(Of Integer)("Id").ToString())
                    name = CStr(row.Field(Of String)("Name").ToString())
                End If
            Else
                Dim row = _studentGroupList.AsEnumerable().SingleOrDefault(Function(p) p.Field(Of Integer)("IsLinkedGroup") = 1)
                If row IsNot Nothing Then
                    groupId = CInt(row.Field(Of Integer)("Id").ToString())
                    name = CStr(row.Field(Of String)("Name").ToString())
                End If
            End If
        End If
        Return groupId
    End Function

    ''' <summary>
    ''' Load parent studnet group id
    ''' </summary>
    Private Function LoadParentStudentGroup(studentGroupId As Integer) As Integer
        Dim groupId As Integer = 0
        Dim row = _studentGroupList.AsEnumerable().SingleOrDefault(Function(p) p.Field(Of Integer)("Id") = studentGroupId)
        If row IsNot Nothing Then
            groupId = CInt(row.Field(Of Integer)("ParentID").ToString())
        End If
        Return groupId
    End Function

    ''' <summary>
    ''' To load current academic cycle id by current date
    ''' </summary>

    Private Function LoadCurrentAcademicCycleID() As Integer
        Dim id As Integer = 0
        Dim sql As New StringBuilder()
        ''Commented By Pradip 2015-07-17 Added New Logic To Set Value
        'sql.AppendFormat("{0} ..spCommonCurrentAcadmicCycle__c", Me.Database)
        'id2 = CInt(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
        'sql.Clear()
        sql.AppendFormat("{0} ..spGetDefaultAcademicCycleToSet__c", Me.Database)
        id = CInt(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
        Return id
    End Function

    ''Added By Pradip 2015-08-19 To Get Previous Academic Cycle ID To Pass In Run Rule Engine
    Private Function LoadPreviousAcademicCycleID() As Integer
        Dim id As Integer = 0
        Dim sql As New StringBuilder()
        sql.AppendFormat("{0} ..spCommonCurrentAcadmicCycle__c", Me.Database)
        id = CInt(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
        Return id
    End Function



    ''' <summary>
    ''' Load student break down data 
    ''' </summary>
    Private Sub LoadStudentBreakDownData()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetStudentBreakDown__c @StudentID={1},@CompanyID={2}", Me.Database, Me.SelectedStudentID, loggedInUser.CompanyID)
            _studentBreakDownDetails = Me.DataAction.GetDataSet(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ViewState("_studentBreakDownDetails") = _studentBreakDownDetails
            If _studentBreakDownDetails IsNot Nothing And _studentBreakDownDetails.Tables(0).Rows.Count > 0 Then
                Dim studentInfo As DataRow = _studentBreakDownDetails.Tables(0).Select().First()
                txtSBLastName.Text = CStr(studentInfo("LastName"))
                txtSBFirstName.Text = CStr(studentInfo("FirstName"))
            End If
            If _studentBreakDownDetails IsNot Nothing And _studentBreakDownDetails.Tables(6).Rows.Count > 0 Then
                Dim studentInfo As DataRow = _studentBreakDownDetails.Tables(6).Select().First()
                lblTotalCost.Text = "Total Cost : " + CStr(If(IsDBNull(studentInfo("TotalCost")), 0.0, studentInfo("TotalCost")))
            End If
        Catch ex As Exception
            lblPopupStudentBreakDownMessage.Text = ex.Message
            lblPopupStudentBreakDownMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' to do the default settings on reload
    ''' </summary>
    Private Sub LoadDefaultSetting()
        Try
            _courseEnrollmentDetails = CType(ViewState("_courseEnrollmentDetails"), DataTable)
            If _courseEnrollmentDetails.Rows.Count > 0 Then
                gvFirmCourseEnrollment.ColumnGroupsDefaultExpanded = True
                Dim curriculumTable As DataTable = _courseEnrollmentDetails.DefaultView.ToTable(True, "Curriculum")
                Dim curriculums As New List(Of String)()
                ''Commented By Pradip Chavhan 2015-07-07 To Allow CAP1, CAP2, and FAE, Curriculum to be defaulted as open. 
                'Dim cout As Integer = 0
                'For Each row As DataRow In curriculumTable.Rows
                '    If cout <> 0 Then
                '        gvFirmCourseEnrollment.CollapsedColumnIndexes.Add(New String() {row("Curriculum").ToString()})
                '    End If
                '    cout = cout + 1
                'Next
                Dim query = _courseEnrollmentDetails.AsEnumerable().Select(Function(p) p.Field(Of Integer)("StudentID")).Distinct().ToList()
                Dim number As New StringBuilder()
                number.AppendFormat("Total Number of students : {0}", query.Count.ToString())
                lblNumberOfStudents.Text = number.ToString()
            End If
            'Enroll student on eligible courses i.e check all checkboxes
            _EnrollOnLoad = True
            gvFirmCourseEnrollment.Rebind()
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Validate enrollment request(s) and submit enrollment
    ''' </summary>
    Private Sub SubmitEnrollmentRequests()
        Try
            Dim errorMessage As String = String.Empty
            Dim canEnroll As Boolean = True
            Dim dAvailableUnits As Double = 0.0
            For Each item As GridItem In gvCurriculumCourse.MasterTableView.Items
                Dim dataitem As GridDataItem = DirectCast(item, GridDataItem)
                Dim curriculumId As Integer = CInt(dataitem.GetDataKeyValue("CurriculumID").ToString())
                Dim subjectId As Integer = CInt(dataitem.GetDataKeyValue("SubjectID").ToString())
                Dim alternateGroupId As String = dataitem.GetDataKeyValue("AlternativeGroupID").ToString()
                Dim cutOffUnits As Double = CDbl(dataitem.GetDataKeyValue("CutOffUnits").ToString())
                Dim units As Double = CDbl(dataitem.GetDataKeyValue("CourseUnits").ToString())

                Dim chkClassRoom As CheckBox = CType(dataitem.FindControl("chkClassRoom"), CheckBox)
                Dim chkInterimAssessment As CheckBox = CType(item.FindControl("chkInterimAssessment"), CheckBox)
                Dim chkMockExam As CheckBox = CType(item.FindControl("chkMockExam"), CheckBox)
                Dim chkSummerExam As CheckBox = CType(item.FindControl("chkSummerExam"), CheckBox)
                Dim chkRevision As CheckBox = CType(item.FindControl("chkRevision"), CheckBox)
                Dim chkRepeatRevision As CheckBox = CType(item.FindControl("chkRepeatRevision"), CheckBox)
                Dim chkResitInterimAssessment As CheckBox = CType(item.FindControl("chkResitInterimAssessment"), CheckBox)
                Dim chkAutumnExam As CheckBox = CType(item.FindControl("chkAutumnExam"), CheckBox)


                Dim isFailed As Boolean = CBool(dataitem.GetDataKeyValue("Isfailed").ToString())
                Dim FailedUnits As Double = CDbl(dataitem.GetDataKeyValue("FailedUnits").ToString())
                Dim FirstAttempt As Double = CDbl(dataitem.GetDataKeyValue("FirstAttempt").ToString())
                Dim caMinimumUnits As Double = CDbl(dataitem.GetDataKeyValue("caMinimumUnits").ToString())

                'Added this to check how many units are available to enroll.
                If chkSummerExam.Visible Or chkAutumnExam.Visible Then
                    dAvailableUnits = dAvailableUnits + units
                End If

                If chkClassRoom.Checked And chkClassRoom.Visible = True Then
                    Dim data As String() = chkClassRoom.Text.ToString().Split(New Char() {";"c})
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, "classroom", True, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                ElseIf chkClassRoom.Checked = False And chkClassRoom.Visible = True Then
                    Dim data As String() = chkClassRoom.Text.ToString().Split(New Char() {";"c})
                    Dim code As Integer = CInt(data(0))
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    If code = 44 Or code = 55 Then
                        _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, String.Empty, False, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                    End If
                End If
                If chkInterimAssessment.Checked And chkInterimAssessment.Visible = True Then
                    Dim data As String() = chkInterimAssessment.Text.ToString().Split(New Char() {";"c})
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, String.Empty, True, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                ElseIf chkInterimAssessment.Checked = False And chkInterimAssessment.Visible = True Then
                    Dim data As String() = chkInterimAssessment.Text.ToString().Split(New Char() {";"c})
                    Dim code As Integer = CInt(data(0))
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    If code = 44 Or code = 55 Then
                        _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, String.Empty, False, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                    End If
                End If
                If chkMockExam.Checked And chkMockExam.Visible = True Then
                    Dim data As String() = chkMockExam.Text.ToString().Split(New Char() {";"c})
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, String.Empty, True, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                ElseIf chkMockExam.Checked = False And chkMockExam.Visible = True Then
                    Dim data As String() = chkMockExam.Text.ToString().Split(New Char() {";"c})
                    Dim code As Integer = CInt(data(0))
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    If code = 44 Or code = 55 Then
                        _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, String.Empty, False, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                    End If
                End If
                If chkSummerExam.Checked And chkSummerExam.Visible = True Then
                    Dim data As String() = chkSummerExam.Text.ToString().Split(New Char() {";"c})
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, "exam", True, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                ElseIf chkSummerExam.Checked = False And chkSummerExam.Visible = True Then
                    Dim data As String() = chkSummerExam.Text.ToString().Split(New Char() {";"c})
                    Dim code As Integer = CInt(data(0))
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    If code = 44 Or code = 55 Then
                        'Start- Siddharth: Made changes for redmine log #17839
                        _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, "exam", False, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                        'End
                    End If
                End If
                If chkRevision.Checked And chkRevision.Visible = True Then
                    Dim data As String() = chkRevision.Text.ToString().Split(New Char() {";"c})
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, "Revision", True, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                ElseIf chkRevision.Checked = False And chkRevision.Visible = True Then
                    Dim data As String() = chkRevision.Text.ToString().Split(New Char() {";"c})
                    Dim code As Integer = CInt(data(0))
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    If code = 44 Or code = 55 Then
                        _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, String.Empty, False, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                    End If
                End If
                If chkRepeatRevision.Checked And chkRepeatRevision.Visible = True Then
                    Dim data As String() = chkRepeatRevision.Text.ToString().Split(New Char() {";"c})
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    ''_enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, String.Empty, True))
                    _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, "RepeatRevision", True, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                ElseIf chkRepeatRevision.Checked = False And chkRepeatRevision.Visible = True Then
                    Dim data As String() = chkRepeatRevision.Text.ToString().Split(New Char() {";"c})
                    Dim code As Integer = CInt(data(0))
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    If code = 44 Or code = 55 Then
                        _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, String.Empty, False, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                    End If
                End If
                If chkResitInterimAssessment.Checked And chkResitInterimAssessment.Visible = True Then
                    Dim data As String() = chkResitInterimAssessment.Text.ToString().Split(New Char() {";"c})
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, String.Empty, chkResitInterimAssessment.Checked, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                ElseIf chkResitInterimAssessment.Checked = False And chkResitInterimAssessment.Visible = True Then
                    Dim data As String() = chkResitInterimAssessment.Text.ToString().Split(New Char() {";"c})
                    Dim code As Integer = CInt(data(0))
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    If code = 44 Or code = 55 Then
                        _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, String.Empty, False, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                    End If
                End If
                If chkAutumnExam.Checked And chkAutumnExam.Visible = True Then
                    Dim data As String() = chkAutumnExam.Text.ToString().Split(New Char() {";"c})
                    Dim code As Integer = CInt(data(0))
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, "exam", chkAutumnExam.Checked, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                ElseIf chkAutumnExam.Checked = False And chkAutumnExam.Visible = True Then
                    Dim data As String() = chkAutumnExam.Text.ToString().Split(New Char() {";"c})
                    Dim code As Integer = CInt(data(0))
                    Dim recordId As Integer = CInt(data(1))
                    Dim classId As Integer = CInt(data(2))
                    Dim productId As Integer = CInt(data(3))
                    If code = 44 Or code = 55 Then
                        'Start- Siddharth: Made changes for redmine log #17839
                        _enrollmentList.Add(New Enrollment(recordId, curriculumId, subjectId, classId, productId, alternateGroupId, cutOffUnits, units, "exam", False, isFailed, FailedUnits, FirstAttempt, caMinimumUnits))
                        'End
                    End If
                End If
            Next
            If _enrollmentList.Count > 0 Then
                Dim isCap1 As Boolean = False
                Dim isCap2 As Boolean = False
                Dim isFAE As Boolean = False
                Dim chkDebkFailed As Boolean = False
                'Get Curriculum IDs 
                Dim sCurriculumIDSQL As String = Database & "..spGetCurriculumsID__c"
                Dim dtCurriculumIDs As DataTable = DataAction.GetDataTable(sCurriculumIDSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

                Dim curriculumIds = _studentEnrollmentDetails.AsEnumerable().Select(Function(p) p.Field(Of Integer)("CurriculumID")).Distinct().ToList()
                'Start- Siddharth: Added for redmine log #17839
                Dim iSelectedCount As Double = 0
                'End
                For Each item As Integer In curriculumIds
                    Dim cid As Integer = item

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
                        'Dim query = (From c In _enrollmentList.Where(Function(p) p.CurriculumID = cid And p.ClassType = "classroom" Or p.ClassType = "Revision" Or p.ClassType = "RepeatRevision" And p.IsChecked = True)
                        '  Group By c.CurriculumID, c.CutOffUnits Into Total = Sum(c.Units) Select Total, CutOffUnits, CurriculumID)
                        'Start- Siddharth: Made changes for redmine log #17839
                        Dim query = (From c In _enrollmentList.Where(Function(p) p.CurriculumID = cid And p.ClassType = "exam")
                                    Group By c.CurriculumID, c.CutOffUnits, c.FailedUnits, c.FirstAttempt, c.caMinimumUnits Into Total = Sum(c.Units) Select Total, CutOffUnits, CurriculumID, FailedUnits, FirstAttempt, caMinimumUnits)
                        'End

                        '''Added BY Pradip 2017-03-20
                        Dim Failedquery = (From c In _enrollmentList.Where(Function(p) p.CurriculumID = cid And p.ClassType = "exam" And p.IsChecked = True And p.IsFailed = True)
                                   Group By c.CurriculumID, c.CutOffUnits, c.FailedUnits, c.FirstAttempt, c.caMinimumUnits Into Total = Sum(c.Units) Select Total, CutOffUnits, CurriculumID, FailedUnits, FirstAttempt, caMinimumUnits)

                        Dim FirstAttemptquery = (From c In _enrollmentList.Where(Function(p) p.CurriculumID = cid And p.ClassType = "exam" And p.IsChecked = True And p.IsFailed = False)
                                   Group By c.CurriculumID, c.CutOffUnits, c.FailedUnits, c.FirstAttempt, c.caMinimumUnits Into Total = Sum(c.Units) Select Total, CutOffUnits, CurriculumID, FailedUnits, FirstAttempt, caMinimumUnits)
                        'Added for minimum units scenarioes
                        Dim FirstAttemptUncheckedquery = (From c In _enrollmentList.Where(Function(p) p.CurriculumID = cid And p.ClassType = "exam" And p.IsChecked = False And p.IsFailed = False)
                                   Group By c.CurriculumID, c.CutOffUnits, c.FailedUnits, c.FirstAttempt, c.caMinimumUnits Into Total = Sum(c.Units) Select Total, CutOffUnits, CurriculumID, FailedUnits, FirstAttempt, caMinimumUnits)

                        'If query IsNot Nothing And query.ToList().Count > 0 Then
                        '    Dim totalUnits As Double = query.ToList().First().Total
                        '    Dim cutOffDates As Double = query.ToList().First().CutOffUnits
                        '    Dim CurriculumID As Integer = query.ToList().First().CurriculumID
                        '    If CurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)) Then
                        '        isCap1 = True
                        '    End If
                        '    If CurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)) Then
                        '        isCap2 = True
                        '    End If
                        '    If CurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)) Then
                        '        isFAE = True
                        '    End If
                        '    If totalUnits < cutOffDates AndAlso totalUnits <> dAvailableUnits Then 'Added totalUnits <> dAvailableUnits, because if only 1 unit is available to taek then cutoffunits check is not valid
                        '        canEnroll = False
                        '        Exit For
                        '    End If

                        'End If

                        Dim FirstSelectedUnit As Double = 0
                        Dim minCutOffUnits As Double = 0
                        Dim CurriculumID2 As Integer = 0
                        Dim firstAttemptremaining As Double = 0

                        'Start- Siddharth: Added for redmine log #17839
                        Dim FailedSelectedUnit As Double = 0
                        Dim FailedCurriculumID As Integer = 0
                        Dim Failedremaining As Double = 0

                        'Added below code for Bridge case with at least 1 fresh course, so that it will be mandetory for user to select cap1 courses. 
                        If FirstAttemptUncheckedquery IsNot Nothing And FirstAttemptUncheckedquery.ToList().Count > 0 And cid = Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)) AndAlso ddlCurriculumList.SelectedValue = "-1" Then
                            firstAttemptremaining = FirstAttemptUncheckedquery.ToList().First().FirstAttempt
                            Failedremaining = FirstAttemptUncheckedquery.ToList().First().FailedUnits
                            minCutOffUnits = FirstAttemptUncheckedquery.ToList().First().caMinimumUnits
                        End If
                        'End
                        If FirstAttemptquery IsNot Nothing And FirstAttemptquery.ToList().Count > 0 Then
                            FirstSelectedUnit = FirstAttemptquery.ToList().First().Total
                            minCutOffUnits = FirstAttemptquery.ToList().First().caMinimumUnits
                            CurriculumID2 = FirstAttemptquery.ToList().First().CurriculumID
                            firstAttemptremaining = FirstAttemptquery.ToList().First().FirstAttempt
                            'Start- Siddharth: Added for redmine log #17839
                            'Commented for other minimum units scenarioes
                            'Failedremaining = FirstAttemptquery.ToList().First().FailedUnits                         
                            'End

                            If CurriculumID2 = Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)) Then
                                isCap1 = True
                            End If
                            If CurriculumID2 = Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)) Then
                                isCap2 = True
                            End If
                            If CurriculumID2 = Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)) Then
                                isFAE = True
                            End If
                        End If

                        'Dim FailedSelectedUnit As Double = 0
                        'Dim FailedCurriculumID As Integer = 0
                        'Dim Failedremaining As Double = 0

                        If Failedquery IsNot Nothing And Failedquery.ToList().Count > 0 Then
                            FailedSelectedUnit = Failedquery.ToList().First().Total
                            minCutOffUnits = Failedquery.ToList().First().caMinimumUnits
                            FailedCurriculumID = Failedquery.ToList().First().CurriculumID
                            Failedremaining = Failedquery.ToList().First().FailedUnits
                            'Start- Siddharth: Added for redmine log #17839
                            'Commented for other minimum units scenarioes
                            'firstAttemptremaining = Failedquery.ToList().First().FirstAttempt
                            'End

                            If FailedCurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)) Then
                                isCap1 = True
                            End If
                            If FailedCurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)) Then
                                isCap2 = True
                            End If
                            If FailedCurriculumID = Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)) Then
                                isFAE = True
                            End If
                        End If

                        'Siddharth: added code and changed comparing values for redmine #17651
                        'Start
                        iSelectedCount = iSelectedCount + FailedSelectedUnit + FirstSelectedUnit
                        Dim minCutOffToCompare As Double = 0
                        If firstAttemptremaining >= minCutOffUnits Then
                            minCutOffToCompare = minCutOffUnits
                        Else
                            minCutOffToCompare = firstAttemptremaining
                        End If

                        If (firstAttemptremaining >= minCutOffToCompare And FirstSelectedUnit < minCutOffToCompare) Then
                            canEnroll = False
                            Exit For
                        End If

                        minCutOffToCompare = minCutOffUnits
                        If Failedremaining < minCutOffUnits Then
                            minCutOffToCompare = Failedremaining
                        End If
                        'Siddharth: Added condition for FAE enrolment, if student fails on CORE & Elective and for enrolment select different elective than failed
                        'Start
                        If FailedSelectedUnit < minCutOffUnits AndAlso isFAE Then
                            minCutOffToCompare = minCutOffToCompare - FirstSelectedUnit
                        End If
                        'End
                        If (Failedremaining >= minCutOffToCompare And FailedSelectedUnit < minCutOffToCompare) Then
                            canEnroll = False
                            Exit For
                        End If
                        'End
                        ' If query IsNot Nothing And query.ToList().Count > 0 Then
                        If cid = Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)) Then
                            isCap1 = True
                        End If
                        If cid = Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)) Then
                            isCap2 = True
                        End If
                        If cid = Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)) Then
                            isFAE = True
                        End If
                    End If
                Next
                'If chkDebkFailed = False Then
                '    If Not _studentEnrollmentDetails Is Nothing AndAlso _studentEnrollmentDetails.Rows.Count > 0 Then
                '        Dim viewCap1 As New DataView(_studentEnrollmentDetails)
                '        Dim distinctValuesCap1 As DataTable = viewCap1.ToTable(True, "CurriculumID")
                '        Dim drCap1() As DataRow = distinctValuesCap1.Select("CurriculumID= " & Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)))
                '        If drCap1.Length > 0 Then

                '            If isCap1 = False Then
                '                canEnroll = False
                '            End If
                '        End If
                '        Dim drCap2() As DataRow = distinctValuesCap1.Select("CurriculumID=" & Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)))
                '        If drCap2.Length > 0 Then
                '            If isCap2 = False Then
                '                Dim sCheckCap2CourseNotEnrolledSql As String = Database & "..spCheckStudentNotEnrolledCAP2__c @StudentID=" & Me.SelectedStudentID
                '                Dim bIsCap2EnrollFirstTime As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sCheckCap2CourseNotEnrolledSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                '                If bIsCap2EnrollFirstTime Then
                '                Else
                '                    canEnroll = False

                '                End If
                '            End If
                '        End If
                '        Dim drFAE() As DataRow = distinctValuesCap1.Select("CurriculumID=" & Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)))
                '        If drFAE.Length > 0 Then
                '            If isFAE = False Then
                '                canEnroll = False
                '            End If
                '        End If
                '    End If
                'End If


                If chkDebkFailed = False Then
                    Dim isEnroll As Boolean
                    Dim sSqlIsEnroll As String = String.Empty
                    If Not _studentEnrollmentDetails Is Nothing AndAlso _studentEnrollmentDetails.Rows.Count > 0 Then
                        Dim viewCap1 As New DataView(_studentEnrollmentDetails)
                        Dim distinctValuesCap1 As DataTable = viewCap1.ToTable(True, "CurriculumID")
                        Dim drCap1() As DataRow = distinctValuesCap1.Select("CurriculumID= " & Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)))
                        If drCap1.Length > 0 Then
                            ''Added by Pradip 2016-11-15 for Issue https://redmine.softwaredesign.ie/issues/16028
                            'sSqlIsEnroll = Database & "..spCheckIsEnrolledByCurriculum__c @CurriculumID=" & Convert.ToInt32(dtCurriculumIDs.Rows(0)(0)) & ",@StudentID=" & Me.SelectedStudentID
                            'isEnroll = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlIsEnroll, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            ' If isCap1 = False AndAlso isEnroll = False Then
                            If isCap1 = False Then
                                canEnroll = False
                            End If
                        End If
                        Dim drCap2() As DataRow = distinctValuesCap1.Select("CurriculumID=" & Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)))
                        If drCap2.Length > 0 Then
                            'sSqlIsEnroll = Database & "..spCheckIsEnrolledByCurriculum__c @CurriculumID=" & Convert.ToInt32(dtCurriculumIDs.Rows(1)(0)) & ",@StudentID=" & Me.SelectedStudentID
                            'isEnroll = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlIsEnroll, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If isCap2 = False Then
                                Dim sCheckCap2CourseNotEnrolledSql As String = Database & "..spCheckStudentNotEnrolledCAP2__c @StudentID=" & Me.SelectedStudentID
                                Dim bIsCap2EnrollFirstTime As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sCheckCap2CourseNotEnrolledSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If bIsCap2EnrollFirstTime Then
                                Else
                                    ' If isEnroll = False Then
                                    canEnroll = False
                                    'End If
                                End If
                            End If
                        End If
                        Dim drFAE() As DataRow = distinctValuesCap1.Select("CurriculumID=" & Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)))
                        If drFAE.Length > 0 Then
                            ' sSqlIsEnroll = Database & "..spCheckIsEnrolledByCurriculum__c @CurriculumID=" & Convert.ToInt32(dtCurriculumIDs.Rows(2)(0)) & ",@StudentID=" & Me.SelectedStudentID
                            'isEnroll = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlIsEnroll, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            'If isFAE = False AndAlso isEnroll = False Then
                            If isFAE = False Then
                                canEnroll = False
                            End If
                        End If
                    End If
                    'Start- Siddharth: Added for redmine log #17839
                    If iSelectedCount <= 0 Then
                        canEnroll = False
                    End If
                    'End
                End If


                If canEnroll Then
                    For Each item In _enrollmentList
                        EnrollRequest(item, errorMessage)
                    Next
                    ''Added By Pradip 2015-08-25 To Update Time Table For PrePending Entry
                    Dim sSQL As String = Database & "..spUpdateEnrollmentStagingTimeTableForPrePending__c"
                    Dim param(3) As IDataParameter
                    param(0) = DataAction.GetDataParameter("@AcademicCycleID", SqlDbType.Int, CInt(ddlAcademicYear.SelectedValue))
                    param(1) = DataAction.GetDataParameter("@TimeTableId", SqlDbType.Int, CInt(ddlTimeTableList.SelectedValue))
                    param(2) = DataAction.GetDataParameter("@BillToCompanyID", SqlDbType.Int, CInt(loggedInUser.CompanyID))
                    param(3) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, Me.SelectedStudentID)
                    Dim recordupdate As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param, 180)
                Else
                     If isFAE Then 'this if condition added by GM for redmine #19967
                        lblPopupCourseEnrollmentMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                  Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.MinimumUnitsFAE")),
                  Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), Me.DataAction.UserCredentials)
                        ''ÉND redmine #19967
                    Else
                        lblPopupCourseEnrollmentMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                   Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.MinimumUnits")),
                   Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), Me.DataAction.UserCredentials)
                    End If
                    lblPopupCourseEnrollmentMessage.ForeColor = Color.Red
                End If
            End If
            If canEnroll Then
                rwCourseEnrollment.VisibleOnPageLoad = False
                'False
                'Keep users checkboxes selection
                _EnrollOnLoad = False
                gvFirmCourseEnrollment.Rebind()
            End If
        Catch ex As Exception
            lblPopupCourseEnrollmentMessage.Text = ex.Message
            lblPopupCourseEnrollmentMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To save enrollment requst to staging
    ''' </summary>
    Private Function EnrollRequest(enrollment As Enrollment, ByRef errorMessage As String) As Boolean
        Try
            Dim enrollmentStaging As AptifyGenericEntityBase = Me.AptifyApplication.GetEntityObject("EnrollmentStaging__c", enrollment.RecordID)
            Dim ParentId As Integer = 0
            Dim sSQL As String
            sSQL = Database & "..spGetEnrollmentStagingWithAlternateGroup__c"
            Dim param(2) As IDataParameter
            param(0) = DataAction.GetDataParameter("@E_StageId", SqlDbType.Int, CInt(enrollment.RecordID))
            param(1) = DataAction.GetDataParameter("@TimeTableId", SqlDbType.Int, CInt(ddlTimeTableList.SelectedValue))
            param(2) = DataAction.GetDataParameter("@ClassId", SqlDbType.Int, CInt(enrollment.ClassID))
            Dim dtE_StageAlternativeGroup As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
            If enrollment.RecordID > 0 Then
                If enrollment.IsChecked = False Then
                    enrollmentStaging.SetValue("OrderStatus", "PrePending")
                    enrollmentStaging.SetValue("IsSelected", "0")
                    sSQL = String.Format("{0}..spUpdateEnrollmentStagingOrderStatusOnSubmit__c @RecordID={1}, @Status='{2}', @SetStatus='{3}'", Database, enrollment.RecordID, _
                                         "Deselect", "PrePending")
                    DataAction.ExecuteNonQuery(sSQL, 240)
                Else
                    enrollmentStaging.SetValue("OrderStatus", "Pending")
                    enrollmentStaging.SetValue("IsSelected", "1")
                    sSQL = String.Format("{0}..spUpdateEnrollmentStagingOrderStatusOnSubmit__c @RecordID={1}, @Status='{2}', @SetStatus='{3}'", Database, enrollment.RecordID, _
                                        "PrePending", "Pending")
                    DataAction.ExecuteNonQuery(sSQL, 240)
                    Try
                        If dtE_StageAlternativeGroup.Rows.Count > 0 Then
                            If CInt(dtE_StageAlternativeGroup.Rows(0)("CheckCount").ToString()) > 0 Then
                                enrollmentStaging.SetValue("StudentGroupID", CInt(ddlTimeTableList.SelectedValue))
                                enrollmentStaging.SetValue("AlternativeGroupID", Nothing)
                            ElseIf CInt(dtE_StageAlternativeGroup.Rows(0)("CheckCount").ToString()) <= 0 And CInt(dtE_StageAlternativeGroup.Rows(0)("ALID").ToString()) > 0 Then
                                enrollmentStaging.SetValue("AlternativeGroupID", CInt(dtE_StageAlternativeGroup.Rows(0)("AlternativeGroupIDAL").ToString()))
                                enrollmentStaging.SetValue("StudentGroupID", Nothing)
                            End If
                        End If
                    Catch ex As Exception
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    End Try
                End If
                Return enrollmentStaging.Save(errorMessage)
            ElseIf enrollment.IsChecked = True Then
                enrollmentStaging.SetValue("OrderStatus", "Pending")
                enrollmentStaging.SetValue("IsSelected", "1")
                Try
                    If dtE_StageAlternativeGroup.Rows.Count > 0 Then
                        If CInt(dtE_StageAlternativeGroup.Rows(0)("CheckCount").ToString()) > 0 Then
                            enrollmentStaging.SetValue("StudentGroupID", CInt(ddlTimeTableList.SelectedValue))
                            enrollmentStaging.SetValue("AlternativeGroupID", Nothing)
                        ElseIf CInt(dtE_StageAlternativeGroup.Rows(0)("CheckCount").ToString()) <= 0 And CInt(dtE_StageAlternativeGroup.Rows(0)("ALID").ToString()) > 0 Then
                            enrollmentStaging.SetValue("AlternativeGroupID", CInt(dtE_StageAlternativeGroup.Rows(0)("AlternativeGroupIDAL").ToString()))
                            enrollmentStaging.SetValue("StudentGroupID", Nothing)
                        End If
                    End If
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
                Return enrollmentStaging.Save(errorMessage)
            End If
        Catch ex As Exception
            lblPopupCourseEnrollmentMessage.Text = ex.Message
            lblPopupCourseEnrollmentMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Get product price
    ''' </summary>
    Private Function GetPrice(ByVal enrollment As Enrollment) As Double
        Try
            Dim groupId As Integer
            Dim order As Aptify.Applications.OrderEntry.OrdersEntity
            Dim orderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
            order = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
            order.ShipToID = Me.SelectedStudentID
            order.BillToSameAsShipTo = False
            order.BillToID = loggedInUser.PersonID
            order.AddProduct(enrollment.ProductID, 1)
            If Not String.IsNullOrEmpty(enrollment.AlternateGroupID) Then
                groupId = CInt(enrollment.AlternateGroupID)
            ElseIf ddlTimeTableList IsNot Nothing Then
                If CDbl(ddlTimeTableList.SelectedValue) <> 0 Then
                    groupId = CInt(ddlTimeTableList.SelectedValue)
                End If
            End If
            If order.SubTypes("OrderLines").Count > 0 Then
                orderLine = TryCast(order.SubTypes("OrderLines").Item(0), OrderLinesEntity)
                With orderLine
                    .ExtendedOrderDetailEntity.SetValue("ClassID", enrollment.ClassID)
                    .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                    .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", 1)
                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", groupId)
                    .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", Me.RouteOfEntryID)
                    .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                End With
                Return orderLine.Price
            Else
                Return 0.0
            End If
        Catch ex As Exception
            lblPopupCourseEnrollmentMessage.Text = ex.Message
            lblPopupCourseEnrollmentMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return Nothing
        End Try
    End Function


    Private Function GetPrice(ByVal groupId As Long, ByVal ProductID As Long, ByVal AlternateGroupID As Long, ByVal ClassID As Long, ByVal RouteOfEntryID As Long) As Double
        Try
            Dim groupIdNew As Long
            Dim order As Aptify.Applications.OrderEntry.OrdersEntity
            Dim orderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
            order = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
            order.ShipToID = Me.SelectedStudentID
            order.BillToSameAsShipTo = False
            order.BillToID = loggedInUser.PersonID
            order.AddProduct(ProductID, 1)
            If AlternateGroupID > 0 Then
                groupIdNew = AlternateGroupID
            ElseIf groupId > 0 Then
                groupIdNew = groupId
            End If
            If order.SubTypes("OrderLines").Count > 0 Then
                orderLine = TryCast(order.SubTypes("OrderLines").Item(0), OrderLinesEntity)
                With orderLine
                    .ExtendedOrderDetailEntity.SetValue("ClassID", ClassID)
                    .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                    '.ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", 1)
                    .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", 1)
                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", groupId)
                    .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", RouteOfEntryID)
                    .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                End With
                Return orderLine.Price
            Else
                Return 0.0
            End If
        Catch ex As Exception
            lblPopupCourseEnrollmentMessage.Text = ex.Message
            lblPopupCourseEnrollmentMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Load Pre-Pending Data To Staging
    ''' </summary>
    Private Sub LoadPrePendingDataToStaging()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spClearPrePendingDataFromStaging__c @CompanyID={1},@CompanyAdminID={2}", _
                             Me.Database, loggedInUser.CompanyID.ToString(), loggedInUser.PersonID.ToString())
            Me.DataAction.ExecuteNonQuery(sql.ToString(), 180)
            sql.Clear()
            For Each dr As DataRow In _academicCycleList.Rows
                'BFP Performance: Added extra parameter to SP for filtering based on location
                sql.AppendFormat("{0} ..spEnrollStudentCoursePrePending__c @AcademicCycleID={1},@CompanyID={2},@CompanyAdminID={3},@CurrentStage={4},@LocationID={5}", _
                             Me.Database, dr("ID").ToString(), loggedInUser.CompanyID.ToString(), loggedInUser.PersonID.ToString(), "'" + ddlCurrentStage.SelectedItem.Text.Trim + "'", ddlLocation.SelectedValue)
                Me.DataAction.ExecuteNonQuery(sql.ToString(), 600)
                sql.Clear()
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    '' added by Pradip 2015-07-10 To Chekc Records with Status = "Pending" and StudentGroupId= 0 in EnrollmentStaging__c Entity
    Private Function Validate() As Boolean
        Dim sSQL As String
        Try
            sSQL = Database & "..spEnrollmentStagingTimeTableCheck__c"
            Dim param(1) As IDataParameter
            'User1.PersonID
            param(0) = DataAction.GetDataParameter("@ParentCompanyId", SqlDbType.Int, loggedInUser.CompanyID)
            param(1) = DataAction.GetDataParameter("@AcademicCyleId", SqlDbType.Int, CInt(ddlAcademicYear.SelectedValue))
            ' dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
            Dim checkcount As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, param))
            If checkcount > 0 Then
                Return False
            End If
            Return True

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function

    ''Added By Pradip 2015-07-14 To Fill Curriculum
    Private Sub FillCurriculum()
        Dim sSql As String = Database & "..spGetCurriculumDefinationDetailsWithBridging__c"
        Dim dt As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        ddlCurriculumList.DataSource = dt
        ddlCurriculumList.DataTextField = "Name"
        ddlCurriculumList.DataValueField = "ID"
        ddlCurriculumList.DataBind()
        'ddlCurriculumList.Items.Insert(0, "Select")
        ''Code To Fill Current Stage
        ddlCurrentStage.DataSource = dt
        ddlCurrentStage.DataTextField = "Name"
        ddlCurrentStage.DataValueField = "Name"
        ddlCurrentStage.DataBind()
        'ddlCurrentStage.Items.Insert(0, "All")

        'BFP Performance: Siddharth: Now we have landing page in between so now disabeling drop downs
        If Request.QueryString("CurrentState") IsNot Nothing Then
            Dim sCurrentStateID As String = Request.QueryString("CurrentState")
            ddlCurriculumList.SelectedValue = sCurrentStateID
            ddlCurrentStage.SelectedValue = ddlCurriculumList.Items.FindByValue(sCurrentStateID).Text
            ddlCurriculumList_SelectedIndexChanged(ddlCurriculumList, Nothing)
        End If
        ddlCurrentStage.Enabled = False
        ddlCurriculumList.Enabled = False
    End Sub

    Private Sub FillTimeTable(ByVal AcademicCycleID As Integer, ByVal CurriculumID As Integer)
        Dim sSql As String = Database & "..spGetTimeTableByCurriculumandAcademicYear__c"
        'Commented By Pradip 2016-04-12 For G3P 38 UAT Tracker Item
        'Dim param(1) As IDataParameter
        'param(0) = DataAction.GetDataParameter("@AcademicCycleID", SqlDbType.Int, AcademicCycleID)
        'param(1) = DataAction.GetDataParameter("@CurriculumID", SqlDbType.Int, CurriculumID)
        Dim param(2) As IDataParameter
        param(0) = DataAction.GetDataParameter("@AcademicCycleID", SqlDbType.Int, AcademicCycleID)
        param(1) = DataAction.GetDataParameter("@CurriculumID", SqlDbType.Int, CurriculumID)
        param(2) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, loggedInUser.CompanyID)
        Dim dt As DataTable = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
        ddlTimeTable.DataSource = dt
        ddlTimeTable.DataTextField = "Name"
        ddlTimeTable.DataValueField = "ID"
        ddlTimeTable.DataBind()
        ddlTimeTable.Items.Insert(0, "Select")
    End Sub

    ''Addedb By Pradip 2015-07-15 T Fill Route Of Entry
    Private Sub FillRouteOfEntry()
        Dim sSql As String = Database & "..spGetContractElevationApplicationType__c"
        Dim dt As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        ddlRouteOfEntry.DataSource = dt
        ddlRouteOfEntry.DataTextField = "Name"
        ddlRouteOfEntry.DataValueField = "ID"
        ddlRouteOfEntry.DataBind()
        ddlRouteOfEntry.Items.Insert(0, New System.Web.UI.WebControls.ListItem("All", "0"))
        ''Uncommented below as per comment on log #18857 dated 7-June-2018
        ''BFP Performance: Commented below as to set default value to 'All', as per CAI request
        If ddlRouteOfEntry.Items.Count > 1 Then
            Dim dv As DataView = New DataView(dt)
            dv.RowFilter = "RouteName = 'Contract'"
            If dv.ToTable.Rows.Count > 0 Then
                ddlRouteOfEntry.SelectedValue = Convert.ToString(dv.ToTable.Rows(0)("ID"))
            End If
            ''ddlRouteOfEntry.Items.FindByText("Contract").Value()
        End If
    End Sub

    Private Sub SetDefaultAcademicCycle()
        Dim sSql As String = Database & "..spGetDefaultAcademicCycleToSet__c"
        Dim dt As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        If dt.Rows.Count > 0 Then
            Dim EnrolledCutoffDate As DateTime = Convert.ToDateTime(dt.Rows(0)("EnrolledCutoffDate").ToString())
            If New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) < EnrolledCutoffDate Then
                ddlAcademicYear.SelectedValue = dt.Rows(0)("ID").ToString()
            Else

            End If

        End If
    End Sub

    ''' <summary>
    ''' Load FAE elective courses
    ''' </summary>
    Private Sub LoadFAEElectiveCourses()
        Dim sSql As String = Database & "..spGetFAEElectiveCourses__c"
        Dim dtFAE As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        ddlFAEElectives.DataSource = dtFAE
        ddlFAEElectives.DataTextField = "CourseName"
        ddlFAEElectives.DataValueField = "CourseID"
        ddlFAEElectives.DataBind()
        ddlFAEElectives.Items.Insert(0, "Select")
    End Sub

    ''' <summary>
    ''' Check is the firm RTO and user is Admin
    ''' </summary>
    Private Function IsPageAccessible() As Boolean
        Dim bResult As Boolean = False
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spIsBigFirmCompanyRTO__c @CompanyID={1}", Database, loggedInUser.CompanyID.ToString())
            Dim companyId As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            If companyId > 1 Then
                sql.Clear()
                sql.AppendFormat("{0}..spCheckFirmAdminForBigFirmPage__c @PersonID={1}", Database, loggedInUser.PersonID)
                Dim isCompanyAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                If isCompanyAdmin > 0 Then
                    bResult = True
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return bResult
    End Function

    ''Addedb By Pradip 2015-10-19 To Fill Location
    Private Sub FillLocation()
        Dim sSql As String = Database & "..spGetLocationsDetailsForBFP__c"
        Dim param(0) As IDataParameter
        param(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, loggedInUser.CompanyID.ToString())
        Dim dt As DataTable = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
        ddlLocation.DataSource = dt
        ddlLocation.DataTextField = "Name"
        ddlLocation.DataValueField = "ID"
        ddlLocation.DataBind()
        ddlLocation.Items.Insert(0, New System.Web.UI.WebControls.ListItem("All", "0"))
        'BFP Performance: setting location dropdown value through query string value and disable
        If Request.QueryString("Loc") IsNot Nothing Then
            Dim sLocationID As String = Request.QueryString("Loc")
            ddlLocation.SelectedValue = sLocationID
            ddlLocation.Enabled = False
        End If
    End Sub

    ''added BY Pradip 2016-07-19 for display Student Group In Grid

    Private Sub LoadStudentGroupsNew()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetStudentGroupEnrollmentNew__c @CompanyID={1},@AcademicCycleID={2}", Me.Database, loggedInUser.CompanyID.ToString(), Me.AcademicCycleID)
            _studentGroupListNew = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


#End Region

#Region "Public Methods"

    ''' <summary>
    ''' To set the color to the cell template div according to color codes
    ''' </summary>
    Public Function SetColorCode(value As Object) As String
        Dim data As String() = value.ToString().Split(New Char() {";"c})
        If data(0) = "2" Then
            Return "gv_already_enrolled"
        ElseIf data(0) = "1" Then
            Return "gv_passed"
        ElseIf data(0) = "-1" Then
            Return "gv_failed"
        ElseIf data(0) = "4" Or data(0) = "0" Then
            Return "gv_notavailable"
        ElseIf data(0) = "11" Or data(0) = "22" Or data(0) = "33" Or data(0) = "44" Or data(0) = "55" Or data(0) = "66" Then
            Return "gv_can_enroll"
        End If
        Return "gv_can_notavailable"
    End Function

    ''' <summary>
    ''' To enable/disable check box to enroll based on codes
    ''' </summary>
    Public Function IsAllowToEnroll(value As Object) As Boolean
        Dim data As String() = value.ToString().Split(New Char() {";"c})
        If data(0) = "22" Or data(0) = "44" Or data(0) = "55" Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Set checkbox visible 
    ''' </summary>
    Public Function IsVisible(value As Object) As Boolean
        Dim data As String() = value.ToString().Split(New Char() {";"c})
        If data(0) = "11" Or data(0) = "22" Or data(0) = "33" Or data(0) = "44" Or data(0) = "55" Or data(0) = "66" Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' To check if request to enroll is already exists or is course id core
    ''' </summary>
    Public Function IsEnrolled(value As Object) As Boolean
        Dim data As String() = value.ToString().Split(New Char() {";"c})
        If data(0) = "44" OrElse data(0) = "55" OrElse data(0) = "66" Then
            'Siddharth: Added this below to keep checkboxes unchecked if student is not allowed to enroll.
            If Not IsAllowToEnroll(value) Then
                Return False
            End If
            Return True
        End If
        Return False
    End Function

    Public Function IsRTOFirm() As Boolean
        Try
            Dim result As Boolean = False
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spCommonCheckCompanyRTO__c @CompanyID={1}", _
                             Me.Database, loggedInUser.CompanyID.ToString())
            result = CBool(Me.DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            Return result
        Catch ex As Exception
            Return True
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function


    ''Added By Pradip 2015-07-15 To Validate Before Time Table Update
    Public Function ValidateBeforeUpdateTimetable() As Boolean

        'If ddlCurriculumList.SelectedIndex = 0 Then
        'lblTTMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.CurriculumValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        ' ddlCurriculumList.Focus()
        ' Return False
        'End If

        If ddlTimeTable.SelectedIndex = 0 Then
            lblTTMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirmTimeTableValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            ddlTimeTable.Focus()
            Return False
        End If
        Return True
    End Function

    Public Function IsSelected(ByVal StudentID As String) As Boolean
        Dim result As Boolean = False
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spCheckIsStudentSelected__c @StudentID={1}, @CompanyID={2}, @AcademicCycleID={3}", _
                             Me.Database, StudentID, loggedInUser.CompanyID, ddlAcademicYear.SelectedValue)
            result = CBool(Me.DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return result
    End Function
#End Region

    Protected Sub lnkFAEEnrollment_Click(sender As Object, e As EventArgs) Handles lnkFAEEnrollment.Click
        Try
            Dim url As New StringBuilder()
            url.AppendFormat("{0}?AcademicYear={1}&EnrollMentType={2}&RouteOfEntry={3}&CurrentStage={4}&FilterBy={5}&SubjectFailed={6}&Loc={7}", Me.FAEEnrollment, Me.AcademicCycleID, ddlEnrollmentType.SelectedValue, ddlRouteOfEntry.SelectedValue, ddlCurrentStage.SelectedValue, ddlCodesList.SelectedValue, ddlSubjectsFailed.SelectedValue, ddlLocation.SelectedValue)
            Response.Redirect(url.ToString())
            Return
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub ddlCurrentStage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCurrentStage.SelectedIndexChanged
        Try
            '  If ddlCurrentStage.SelectedIndex <> 0 Then
            'FillTimeTable(CInt(ddlAcademicYear.SelectedValue), CInt(ddlCurrentStage.SelectedValue))
            'ddlCurriculumList.SelectedValue = ddlCurrentStage.SelectedValue
            ddlCurriculumList.SelectedValue = ddlCurriculumList.Items.FindByText(ddlCurrentStage.SelectedValue).Value()
            FillTimeTable(CInt(ddlAcademicYear.SelectedValue), CInt(ddlCurriculumList.SelectedValue))
            lblTTMessage.Text = ""
            'Else
            'ddlTimeTable.Items.Clear()
            'ddlTimeTable.Items.Insert(0, "Select")
            'End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class

#End Region
