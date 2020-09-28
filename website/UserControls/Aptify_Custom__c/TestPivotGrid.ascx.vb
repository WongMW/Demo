Imports System.Data
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports System.Linq
Imports System.Drawing
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI.PivotGrid
Imports Telerik.Web.UI.PivotGrid.Core

Public Class Caps

    Property ID As Integer
    Property StudentID As Integer
    Property Name As String
    Property CapParts As String
    Property CapSubParts As String
    Property IsCompleted As Integer

    Public Sub New(ByVal id As Integer, ByVal studentId As Integer, ByVal name As String, ByVal cappart As String, ByVal capsubpart As String, ByVal isCompleted As Integer)
        Me.ID = id
        Me.StudentID = studentId
        Me.Name = name
        Me.CapParts = cappart
        Me.CapSubParts = capsubpart
        Me.IsCompleted = isCompleted
    End Sub

End Class

Public Class Students

    Property ID As Integer
    Property LastName As String
    Property FirstName As String
    Property Route As String
    Property VenueID As String
    Property VenuesList As List(Of Venues)

    Public Sub New(ByVal id As Integer, ByVal lastName As String, ByVal firstName As String, ByVal route As String, ByVal venueId As String, ByVal venuesList As List(Of Venues))
        Me.ID = id
        Me.LastName = lastName
        Me.FirstName = firstName
        Me.Route = route
        Me.VenueID = venueId
        Me.VenuesList = venuesList
    End Sub

End Class

Public Class Exams

    Property Id As Integer
    Property Name As String

    Public Sub New(ByVal id As Integer, ByVal name As String)
        Me.Id = id
        Me.Name = name
    End Sub

End Class

Public Class Venues

    Property ID As Integer
    Property Name As String

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(ByVal id As Integer, ByVal name As String)
        Me.ID = id
        Me.Name = name
    End Sub

End Class

Public Class TimeTable

    Property Id As Integer
    Property Name As String

    Public Sub New(ByVal id As Integer, ByVal name As String)
        Me.Id = id
        Me.Name = name
    End Sub

End Class

Public Class Classes

    ' Property Id As Integer
    Property StudentId As Integer
    Property Name As String
    Property CourseData As Integer
    Property ExamData As Integer
    Property RevisionData As Integer
    Property ResitInterimData As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal studentId As Integer, ByVal name As String, ByVal capname As String,
                   ByVal courseData As Integer, ByVal examData As Integer,
                   ByVal revisionData As Integer, ByVal resitInterimData As Integer)
        'Me.Id = id
        Me.StudentId = studentId
        Me.Name = name
        Me.CourseData = courseData
        Me.ExamData = examData
        Me.RevisionData = revisionData
        Me.ResitInterimData = resitInterimData
    End Sub

End Class
Partial Class UserControls_Aptify_Custom__c_TestPivotGrid
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

    Private _studentsList As New List(Of Students)
    Private _enrollmentList As New List(Of Caps)
    Private _examsList As New List(Of Exams)
    Private _timeTableList As New List(Of TimeTable)
    Private Shared _venuesList As New List(Of Venues)
    Private _classCap1List As New List(Of Classes)
    Private _classCap2List As New List(Of Classes)
    Private _classFaeList As New List(Of Classes)

    Private _dataTable As DataTable
    Private _enrollmentTable As DataTable
    Private _studentsTable As DataTable
    Private _studentsDetailsTable As DataTable

    Private Shared _selectedStudentId As Integer
    Private _Cap1 As String = "CAP1"
    Private _Cap2 As String = "CAP2"
    Private _FAE As String = "FAE"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadVenues()
        LoadExams()
        Getdata()
    End Sub

    Private Sub LoadVenues()
        _venuesList.Clear()
        Dim sql As String
        sql = Database & "..spGetVenues__c"
        _dataTable = DataAction.GetDataTable(sql)
        For Each row As DataRow In _dataTable.Rows
            _venuesList.Add(New Venues(row("Id"), row("Name")))
        Next
    End Sub

    Private Sub LoadExams()
        _examsList.Clear()
        Dim sql As String
        sql = Database & "..spGetCourseExams__c"
        _dataTable = DataAction.GetDataTable(sql)
        For Each row As DataRow In _dataTable.Rows
            _examsList.Add(New Exams(row("Id"), row("Name")))
        Next
    End Sub

    Private Sub LoadTimeTable(ByVal venuId As Integer)
        _timeTableList.Clear()
        Dim sql As String
        Dim params(0) As System.Data.IDataParameter
        params(0) = DataAction.GetDataParameter("@venueID", SqlDbType.Int, venuId)
        sql = Database & "..spGetTimeTableLocation__c"
        _dataTable = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, params)
        For Each row As DataRow In _dataTable.Rows
            _timeTableList.Add(New TimeTable(row("Id"), row("Name")))
        Next
    End Sub

    Private Sub Getdata()
        gvFirmEnrollment.FilterWindow.Title = "Filter Window"
        gvFirmEnrollment.FilterWindow.CancelButton.Text = "Cancel"
        gvFirmEnrollment.FilterWindow.OKButton.Text = "OK"
        If gvFirmEnrollment.FilterWindow.FilterMenu.Items.Count > 0 Then
            gvFirmEnrollment.FilterWindow.FilterMenu.Items.Item(0).Text = "Clear From Class"
            gvFirmEnrollment.FilterWindow.FilterMenu.Items.Item(1).Text = "Label Filters"
            gvFirmEnrollment.FilterWindow.FilterMenu.Items.Item(2).Text = "Value Filters"
            gvFirmEnrollment.FilterWindow.EnableEmbeddedBaseStylesheet = True
            gvFirmEnrollment.FilterWindow.EnableEmbeddedScripts = True
        End If




        'gvFirmEnrollment.FilterWindow.FilterMenu.
        Dim sSQL As String, ds As DataSet
        sSQL = Database & "..spGetPivotGridData__c"
        Dim params(0) As System.Data.IDataParameter
        params(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, User1.CompanyID)
        ds = DataAction.GetDataSetParametrized(sSQL, CommandType.StoredProcedure, params)
        _enrollmentTable = ds.Tables(0)
        _studentsDetailsTable = ds.Tables(1)
    End Sub


    Protected Sub gvFirmEnrollment_NeedDataSource(ByVal sender As Object, ByVal e As PivotGridNeedDataSourceEventArgs) Handles gvFirmEnrollment.NeedDataSource
        If _enrollmentTable.Rows.Count > 0 Then
            gvFirmEnrollment.DataSource = _enrollmentTable
        Else
            gvFirmEnrollment.DataSource = Nothing
            gvFirmEnrollment.Visible = False
        End If

    End Sub

End Class
