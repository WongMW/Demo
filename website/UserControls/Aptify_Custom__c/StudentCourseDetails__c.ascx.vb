
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Milind Sutar                17/12/2014                         Display course details
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports System.Linq
Imports System.Drawing
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI.PivotGrid
Imports Telerik.Web.UI.PivotGrid.Core

Partial Class UserControls_Aptify_Custom__c_StudentCourseDetails__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

    Private _courseDetails As DataTable
    Private _hearderDetails As DataTable
    Private _classRegId As Integer
    Private _studentId As Integer
    Private _classRegPartStatusId As Integer
    Private _academicCycleId As Integer
    Private _isFAE As Integer = 0
    Private _classId As Integer = 0
    Private _courseName As String
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "StudentCourseDetails__c"
    Protected Const ATTRIBUTE_EDUCATIONDETAILS_PAGE_URL_NAME As String = "EducationDetailsURL"

    Public Property ClassRegistrationID() As Integer
        Get
            _classRegId = CInt(hfClassRegId.Value)
            Return _classRegId
        End Get
        Set(ByVal value As Integer)
            _classRegId = value
            hfClassRegId.Value = _classRegId.ToString()
        End Set
    End Property

    Public Property IsFAE() As Integer
        Get
            _isFAE = CInt(hfIsFAE.Value)
            Return _isFAE
        End Get
        Set(ByVal value As Integer)
            _isFAE = value
            hfIsFAE.Value = _isFAE.ToString()
        End Set
    End Property

    Public Property StudentID() As Integer
        Get
            Return _studentId
        End Get
        Set(ByVal value As Integer)
            _studentId = value
        End Set
    End Property

    Public ReadOnly Property ClassID() As Integer
        Get
            Dim classIdString As String = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ClassID"))
            If Not String.IsNullOrEmpty(classIdString) Then
                _classId = CInt(classIdString)
            End If
            Return _classId
        End Get
    End Property

    Public Property AcademicCycleID() As Integer
        Get
            Dim academicCycleIDString As String = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("AcademicCycleID"))
            If Not String.IsNullOrEmpty(academicCycleIDString) Then
                _academicCycleId = CInt(academicCycleIDString)
            End If
            Return _academicCycleId
        End Get
        Set(ByVal value As Integer)
            _academicCycleId = value
        End Set
    End Property

    Public Property PartStatusID() As Integer
        Get
            Return CInt(hfPartStatusID.Value.ToString())
        End Get
        Set(ByVal value As Integer)
            hfPartStatusID.Value = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Get sets manage my groups page url
    ''' </summary>
    Public Overridable Property EducationDetailsPageURL() As String
        Get
            If Not ViewState(ATTRIBUTE_EDUCATIONDETAILS_PAGE_URL_NAME) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_EDUCATIONDETAILS_PAGE_URL_NAME))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_EDUCATIONDETAILS_PAGE_URL_NAME) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    ''' <summary>
    ''' Handles page load event
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetProperties()
            Me.StudentID = LoggedInUser.PersonID
            LoadHeaderDetails()
            LoadHeaderText()
            LoadStudentCourseDetails()
        End If
    End Sub

    ''' <summary>
    ''' To handle link button events to open the specific popups
    ''' </summary>
    Protected Sub gvCourseDetails_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvCourseDetails.ItemCommand
        Try
            If e.CommandName = "AddNotes" Then
                Dim data As String = e.CommandArgument.ToString
                Me.PartStatusID = CInt(data)
                Dim sql As New StringBuilder()
                sql.AppendFormat("{0}..spGetStudentCourseNotes__c @ClassRegPartStatusID={1}", _
                                 Me.Database, Me.PartStatusID.ToString())
                txtNotes.Text = Me.DataAction.ExecuteScalar(sql.ToString())
                radAddNotes.VisibleOnPageLoad = True
            End If
            If e.CommandName = "Download" Then
                radDownloadDocuments.VisibleOnPageLoad = True
                Dim data As String() = e.CommandArgument.ToString().Split(CChar(";"))
                Dim recordID As Integer = CInt(data(0))
                Dim entityID As Integer = CInt(Me.AptifyApplication.GetEntityID("Course Parts"))
                ucDownload.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Course Material")
                ucDownload.LoadAttachments(entityID, recordID, True)
            End If
            If e.CommandName = "Assignment" Then
                lblAssgnmtValue.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.CourseDetails.Note")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radAssignments.VisibleOnPageLoad = True
                Dim data As String() = e.CommandArgument.ToString().Split(CChar(";"))
                lblbAssignmentDueDateValue.Text = data(0).ToString()

                lblDaysRemainingValue.Text = (CType(data(0), Date) - DateTime.Now.Date).TotalDays.ToString()

                Dim uploadRecordId As Integer = CInt(data(1))
                Dim uploadEntityId As Integer = CInt(Me.AptifyApplication.GetEntityID("ClassRegistrationPartStatus"))
                ucAssignmentUpload.RecordID = uploadRecordId
                ucAssignmentUpload.EntityID = uploadEntityId
                ucAssignmentUpload.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Assignments Upload")
                ucAssignmentUpload.LoadAttachments(uploadEntityId, uploadRecordId, True)
                ' Code Added by govind for Upload document control should not be available  if status of respective part status is “With Corrector” or later statuses
                Dim sClassRegPartStatus As String = data(3).ToString()
                If sClassRegPartStatus.Trim.ToLower = "with corrector" Then
                    ucAssignmentUpload.Visible = False
                    lblUploadAssignments.Visible = False
                Else
                    ucAssignmentUpload.Visible = True
                    lblUploadAssignments.Visible = True
                End If
                Dim downloadRecordId As Integer = CInt(data(2))
                Dim downloadEntityId As Integer = CInt(Me.AptifyApplication.GetEntityID("Course Parts"))
                ucAssignmentDownload.RecordID = downloadRecordId
                ucAssignmentDownload.EntityID = downloadEntityId
                ucAssignmentDownload.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Assignments")
                ucAssignmentDownload.LoadAttachments(downloadEntityId, downloadRecordId, True)
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To bind the data source
    ''' </summary>
    Protected Sub gvCourseDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCourseDetails.NeedDataSource
        LoadStudentCourseDetails()
        If _courseDetails IsNot Nothing Then
            gvCourseDetails.DataSource = _courseDetails
        End If
    End Sub

    ''' <summary>
    ''' Handles the cancel button event - download couse materials popup
    ''' </summary>
    Protected Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
        radDownloadDocuments.VisibleOnPageLoad = False
    End Sub

    ''' <summary>
    ''' Handles the add note button event to add notes to the Student Comments -  tab
    ''' </summary>
    Protected Sub btnAddNotes_Click(sender As Object, e As System.EventArgs) Handles btnAddNotes.Click
        Try
            '' Added By Pradip 2015-11-25
            Dim oClassRegistrationGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
            oClassRegistrationGE = AptifyApplication.GetEntityObject("Class Registrations", Me.ClassRegistrationID)
            With oClassRegistrationGE.SubTypes("ClassRegistrationPartStatus").Find("ID", Me.PartStatusID)
                .SetValue("StudentComments", txtNotes.Text)
            End With
            Dim result As Boolean = oClassRegistrationGE.Save(lblAddNoteMessage.Text)
            If result = True Then
                radAddNotes.VisibleOnPageLoad = False
            Else
                lblAddNoteMessage.ForeColor = Color.Red
            End If

            ''Commented By Pradip 2015-11-25 To Avoid Direct Saving Subtype
            'Dim partStatusGE = Me.AptifyApplication.GetEntityObject("ClassRegistrationPartStatus", Me.PartStatusID)
            'partStatusGE.SetValue("StudentComments", txtNotes.Text)
            'lblAddNoteMessage.Text = String.Empty
            'Dim result As Boolean = partStatusGE.Save(lblAddNoteMessage.Text)
            'If result = True Then
            '    radAddNotes.VisibleOnPageLoad = False
            'Else
            '    lblAddNoteMessage.ForeColor = Color.Red
            'End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the cancel button event - Add notes popup
    ''' </summary>
    Protected Sub btnCloseAddNotes_Click(sender As Object, e As System.EventArgs) Handles btnCloseAddNotes.Click
        radAddNotes.VisibleOnPageLoad = False
    End Sub

    ''' <summary>
    ''' Handles the cancel button event - Assignment popup
    ''' </summary>
    Protected Sub btnCloseAssignment_Click(sender As Object, e As System.EventArgs) Handles btnCloseAssignment.Click
        radAssignments.VisibleOnPageLoad = False
    End Sub

    ''' <summary>
    ''' Handles the clear button event - Add notes popup
    ''' </summary>
    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        txtNotes.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Handle back button click event to go back to education details page
    ''' </summary>
    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(Me.EducationDetailsPageURL)
    End Sub

    ''' <summary>
    ''' To set the default properties
    ''' </summary>
    Protected Overrides Sub SetProperties()
        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        MyBase.SetProperties()
        If String.IsNullOrEmpty(Me.EducationDetailsPageURL) Then
            Me.EducationDetailsPageURL = Me.GetLinkValueFromXML(ATTRIBUTE_EDUCATIONDETAILS_PAGE_URL_NAME)
        End If
    End Sub

    ''' <summary>
    ''' Load header details from the page
    ''' </summary>
    Private Sub LoadHeaderDetails()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetStudentCourseHeaderDetails__c @StudentID={1},@ClassID={2},@AcademicCycleID={3}", _
                             Me.Database, Me.StudentID, Me.ClassID, Me.AcademicCycleID)
            _hearderDetails = Me.DataAction.GetDataTable(sql.ToString())
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Load the student course details 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStudentCourseDetails()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetStudentCourseDetails__c @ClassRegID={1},@IsFAE={2}", Me.Database, Me.ClassRegistrationID, Me.IsFAE.ToString())
            _courseDetails = Me.DataAction.GetDataTable(sql.ToString())
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Set header details of the page
    ''' </summary>
    Private Sub LoadHeaderText()
        Try
            If _hearderDetails IsNot Nothing And _hearderDetails.Rows.Count > 0 Then
                Me.ClassRegistrationID = _hearderDetails.Rows.Item(0).Item("ClassRegId")
                Me.IsFAE = _hearderDetails.Rows.Item(0).Item("IsFAE")
                lblFirstLastValue.Text = _hearderDetails.Rows.Item(0).Item("FirstLast")
                lblStudentNumberValue.Text = _hearderDetails.Rows.Item(0).Item("StudentNumber")
                lblAcademicCycleValue.Text = _hearderDetails.Rows.Item(0).Item("AcademicCycle")
                lblCourseNameValue.Text = _hearderDetails.Rows.Item(0).Item("Course")
                lblStartDateValue.Text = _hearderDetails.Rows.Item(0).Item("StartDate")
                lblEndDateValue.Text = _hearderDetails.Rows.Item(0).Item("EndDate")
                lblTimeTableValue.Text = _hearderDetails.Rows.Item(0).Item("TimeTable")
                lblSubGroup1Value.Text = _hearderDetails.Rows.Item(0).Item("SubGroup1")
                lblSubGroup2Value.Text = _hearderDetails.Rows.Item(0).Item("SubGroup2")
                lblSubGroup3Value.Text = _hearderDetails.Rows.Item(0).Item("SubGroup3")
                lblSubGroup4Value.Text = _hearderDetails.Rows.Item(0).Item("SubGroup4")
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    
End Class

