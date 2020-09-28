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

Partial Class UserControls_Aptify_Custom__c_PivotGrid__c
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
    ''' <summary>
    ''' Control load event
    ''' </summary>
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If Not IsPostBack Then
        'LoadData()
        'End If
        LoadAcademicYear()
        LoadVenues()

        ddlVenue.DataSource = _venuesList
        ddlVenue.DataValueField = "ID"
        ddlVenue.DataTextField = "Name"
        ddlVenue.DataBind()

        LoadExams()
        'LoadTimeTable()
        Getdata()
    End Sub

    Private Sub LoadAcademicYear()
        Dim sql As String
        sql = Database & "..spGetAllAcademicYears__c"
        ddlAcademicYear.DataSource = DataAction.GetDataTable(sql)
        ddlAcademicYear.DataValueField = "Id"
        ddlAcademicYear.DataTextField = "Name"
        ddlAcademicYear.DataBind()
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
        gvFirmEnrollment.FilterWindow.Localization.OK = "OK"
        gvFirmEnrollment.FilterWindow.Localization.Cancel = "Cancel"

        Dim sSQL As String, ds As DataSet
        sSQL = Database & "..spGetPivotGridData__c"
        Dim params(0) As System.Data.IDataParameter
        params(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, User1.CompanyID)
        ' params(1) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, 220)
        ds = DataAction.GetDataSetParametrized(sSQL, CommandType.StoredProcedure, params)
        ' ds.Relations.Add(New DataRelation("Students_Enrollment", ds.Tables(0).Columns("ID"), ds.Tables(1).Columns("StudentID"), False))
        'Dim _studentsTable As DataTable = GetStudentTable()
        '_studentsTable = ds.Tables(0)
        '_enrollmentTable = (GetEnrollmentTable())
        _enrollmentTable = ds.Tables(0)
        _studentsDetailsTable = ds.Tables(1)

        'sSQL = Database & "..spGetRuleEngineData__c"
        'ds = DataAction.GetDataSet(sSQL)

        'For Each rw As DataRow In ds.Tables(0).Rows
        '    _studentsList.Add(New Students(Convert.ToInt32(rw(0)), rw(0) & ";" & rw(1).ToString(), rw(2).ToString(), rw(3).ToString(), 1, _venuesList))
        'Next
        'For Each rw As DataRow In ds.Tables(1).Rows
        '    _enrollmentList.Add(New Caps(1, rw(0).ToString(), rw(2).ToString(), rw(3).ToString(), rw(5).ToString(), Convert.ToInt32(rw(6))))
        'Next
        '_studentsDetailsTable = ds.Tables(2)


        'For Each table As DataTable In ds.Tables
        '    If table.TableName = "Table" Then
        '        'If table.Rows.Count > 0 Then
        '        For Each rw As DataRow In ds.Tables(0).Rows
        '            _studentsList.Add(New Students(Convert.ToInt32(rw(0)), rw(1).ToString(), rw(2).ToString(), rw(3).ToString(), 1, _venuesList))
        '        Next
        '        'End If
        '    Else
        '        If table.Rows.Count > 0 Then
        '            For Each rw As DataRow In table.Rows
        '                _enrollmentList.Add(New Caps(1, rw(0).ToString(), rw(2).ToString(), rw(3).ToString(), rw(5).ToString(), Convert.ToInt32(rw(6))))
        '            Next
        '        End If
        '        '_enrollmentList.AddRange(table)
        '        '_enrollmentTable = table
        '        '(New Caps(1, rw(0).ToString(), rw(2).ToString(), rw(3).ToString(), rw(5).ToString(), Convert.ToInt32(rw(6))))
        '    End If
        'Next
        '_enrollmentList.Add(New Caps(1, "Boris Baldwin", "CAP1", "Course", "FIN", 0))
    End Sub

    Function GetStudentTable() As DataTable
        ' Create new DataTable instance.
        Dim table As New DataTable
        ' Create four typed columns in the DataTable.
        table.Columns.Add("ID", GetType(Integer))
        table.Columns.Add("LastName", GetType(String))
        table.Columns.Add("FirstName", GetType(String))
        table.Columns.Add("Route", GetType(String))
        table.Columns.Add("VenueID", GetType(String))
        table.Columns.Add("VenuesList", GetType(Venues))
        Return table
    End Function

    Function GetEnrollmentTable() As DataTable
        ' Create new DataTable instance.
        Dim table As New DataTable
        ' Create four typed columns in the DataTable.
        table.Columns.Add("StudentID", GetType(Integer))
        table.Columns.Add("StudentName", GetType(String))
        table.Columns.Add("Name", GetType(String))
        table.Columns.Add("CapParts", GetType(String))
        table.Columns.Add("CourseID", GetType(Integer))
        table.Columns.Add("CapSubParts", GetType(String))
        table.Columns.Add("IsCompleted", GetType(Integer))
        Return table
    End Function


    Protected Sub gvFirmEnrollment_CellDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.PivotGridCellDataBoundEventArgs) Handles gvFirmEnrollment.CellDataBound

        If e.Cell.FindControl("ddlVenueList") IsNot Nothing Then
            Dim VenueList As DropDownList = CType(e.Cell.FindControl("ddlVenueList"), DropDownList)
            VenueList.DataSource = _venuesList
            VenueList.DataValueField = "Id"
            VenueList.DataTextField = "Name"
            VenueList.DataBind()
        End If
        If TypeOf e.Cell Is PivotGridDataCell Then
            If e.Cell.FindControl("ddlExamsList") IsNot Nothing Then
                Dim examList As DropDownList = CType(e.Cell.FindControl("ddlExamsList"), DropDownList)
                examList.DataSource = _examsList
                examList.DataValueField = "Id"
                examList.DataTextField = "Name"
                examList.DataBind()
            End If
            If e.Cell.FindControl("ddlTimeTable") IsNot Nothing Then
                Dim timeTableList As DropDownList = CType(e.Cell.FindControl("ddlTimeTable"), DropDownList)
                'LoadTimeTable(Convert.ToInt32(CType(e.Cell.FindControl("ddlTimeTable"), DropDownList).SelectedValue))
                'timeTableList.DataSource = _timeTableList
                timeTableList.DataSource = DataAction.GetDataTable(Database & "..spGetTimeTableLocation__c")
                timeTableList.DataValueField = "Id"
                timeTableList.DataTextField = "Name"
                timeTableList.DataBind()
            End If
            If Not e.Cell.DataItem Is Nothing Then
                If e.Cell.DataItem.ToString().Length > 0 Then
                    Dim isComplete As Integer = Convert.ToInt32(e.Cell.DataItem)
                    If isComplete = 2 Then
                        e.Cell.BackColor = Color.FromName("Green")
                    ElseIf isComplete = 3 Then
                        e.Cell.BackColor = Color.FromName("Gray")
                    End If
                End If
            End If
            'If Not TypeOf e.Cell Is PivotGridDataCell And Not TypeOf e.Cell Is PivotGridColumnHeaderCell And TypeOf e.Cell Is PivotGridRowHeaderCell And Not TryCast(e.Cell.Controls(0), Button) Is Nothing Then
        ElseIf TypeOf e.Cell Is PivotGridRowHeaderCell Then
            If e.Cell.Controls.Count > 0 Then
                If e.Cell.Controls(0) IsNot Nothing Then
                    TryCast(e.Cell.Controls(0), Button).Visible = False
                End If
            End If
        ElseIf TypeOf e.Cell Is PivotGridColumnHeaderCell Then
            If e.Cell.Controls.Count > 0 Then
                If e.Cell.Controls(0) IsNot Nothing Then
                    TryCast(e.Cell.Controls(0), Button).CssClass.Remove(1)
                End If
            End If
        End If
    End Sub

    Protected Sub gvFirmEnrollment_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.PivotGridCommandEventArgs) Handles gvFirmEnrollment.ItemCommand
        If e.CommandName = "Edit" Then
            radStundentEnrollment.VisibleOnPageLoad = True
            LoadPopupData()
            Dim lastNameString As String = e.CommandArgument
            Dim nameData As String() = lastNameString.Split(New Char() {";"c})
            _selectedStudentId = CType(nameData(0), Integer)

            'Dim student As Students = _studentsList.Where(Function(p) p.ID = _selectedStudentId).First()
            'txtLastName.Text = nameData(1)
            'txtFirstName.Text = student.FirstName
            'txtRoute.Text = student.Route
            Dim StudentInfo As DataRow = _enrollmentTable.Select("StudentID = " & _selectedStudentId).First()
            txtLastName.Text = nameData(1)
            txtFirstName.Text = StudentInfo("FirstName")
            txtRoute.Text = StudentInfo("Route")
            _classCap1List.Clear()
            Dim resultCap1() As DataRow = _studentsDetailsTable.Select("StudentID = " & _selectedStudentId & " and Curriculum = '" & _Cap1 & "'")
            For Each rw As DataRow In resultCap1
                '_classCap1List.Add(New Classes(Convert.ToInt32(rw(0)), Convert.ToInt32(rw(1)), rw(3).ToString(), rw(4).ToString(), Convert.ToInt32(rw(5)), Convert.ToInt32(rw(6)), Convert.ToInt32(rw(7)), Convert.ToInt32(rw(8))))
                _classCap1List.Add(New Classes(Convert.ToInt32(rw(0)), rw(2).ToString(), rw(3).ToString(), Convert.ToInt32(rw(4)), Convert.ToInt32(rw(5)), Convert.ToInt32(rw(6)), Convert.ToInt32(rw(7))))
            Next

            _classCap2List.Clear()
            Dim resultCap2() As DataRow = _studentsDetailsTable.Select("StudentID = " & _selectedStudentId & " and Curriculum = '" & _Cap2 & "'")
            For Each rw As DataRow In resultCap2
                '_classCap2List.Add(New Classes(Convert.ToInt32(rw(0)), Convert.ToInt32(rw(1)), rw(3).ToString(), rw(4).ToString(), Convert.ToInt32(rw(5)), Convert.ToInt32(rw(6)), Convert.ToInt32(rw(7)), Convert.ToInt32(rw(8))))
                _classCap2List.Add(New Classes(Convert.ToInt32(rw(0)), rw(2).ToString(), rw(3).ToString(), Convert.ToInt32(rw(4)), Convert.ToInt32(rw(5)), Convert.ToInt32(rw(6)), Convert.ToInt32(rw(7))))
            Next

            _classFaeList.Clear()
            Dim resultFAE() As DataRow = _studentsDetailsTable.Select("StudentID = " & _selectedStudentId & " and Curriculum = '" & _FAE & "'")
            For Each rw As DataRow In resultFAE
                '_classFaeList.Add(New Classes(Convert.ToInt32(rw(0)), Convert.ToInt32(rw(1)), rw(3).ToString(), rw(4).ToString(), Convert.ToInt32(rw(5)), Convert.ToInt32(rw(6)), Convert.ToInt32(rw(7)), Convert.ToInt32(rw(8))))
                _classFaeList.Add(New Classes(Convert.ToInt32(rw(0)), rw(2).ToString(), rw(3).ToString(), Convert.ToInt32(rw(4)), Convert.ToInt32(rw(5)), Convert.ToInt32(rw(6)), Convert.ToInt32(rw(7))))
            Next

            gvCap1.DataSource = _classCap1List
            gvCap1.DataBind()

            gvCap2.DataSource = _classCap2List
            gvCap2.DataBind()

            gvFAE.DataSource = _classFaeList
            gvFAE.DataBind()
        End If
    End Sub

    Private Sub LoadPopupData()
        ddlVenuesList.DataSource = _venuesList
        ddlVenuesList.DataValueField = "ID"
        ddlVenuesList.DataTextField = "Name"
        ddlVenuesList.DataBind()

        LoadTimeTable(1)
        ddlCap2TimeTableList.DataSource = _timeTableList
        ddlCap2TimeTableList.DataValueField = "ID"
        ddlCap2TimeTableList.DataTextField = "Name"
        ddlCap2TimeTableList.DataBind()

        ddlCap1TimetableList.DataSource = _timeTableList
        ddlCap1TimetableList.DataValueField = "ID"
        ddlCap1TimetableList.DataTextField = "Name"
        ddlCap1TimetableList.DataBind()


    End Sub

    Protected Sub gvFirmEnrollment_NeedDataSource(ByVal sender As Object, ByVal e As PivotGridNeedDataSourceEventArgs) Handles gvFirmEnrollment.NeedDataSource
        'Dim entrollDetails = (From p In _studentsList Join d In _enrollmentList On p.ID Equals d.StudentID Select d.ID, d.StudentID, p.LastName, p.FirstName, p.Route, p.VenueID, d.Name, d.CapParts, d.CapSubParts, d.IsCompleted).ToList()
        'gvFirmEnrollment.DataSource = entrollDetails
        'Dim entrollDetails = (From p In _studentsList Join d In _enrollmentTable.AsEnumerable() On p.ID Equals d.StudentID Select d.StudentID, p.LastName, p.FirstName, p.Route, p.VenueID, p.VenuesList, d.Name, d.CapParts, d.CapSubParts, d.IsCompleted).ToList()
        'Dim sSQL As String, ds As DataSet
        'sSQL = Database & "..spGetRuleEngineData__c"
        'ds = DataAction.GetDataSet(sSQL)
        If _enrollmentTable.Rows.Count > 0 Then
            gvFirmEnrollment.DataSource = _enrollmentTable
        Else
            gvFirmEnrollment.DataSource = Nothing
            gvFirmEnrollment.Visible = False
            lblMessage.Visible = True
        End If

    End Sub

    Protected Sub gvFAE_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvFAE.ItemDataBound
        If e.Item.FindControl("ddlExamsList") IsNot Nothing And e.Item.FindControl("lblExamData") IsNot Nothing Then
            Dim examData As Label = CType(e.Item.FindControl("lblExamData"), Label)
            Dim examList As DropDownList = CType(e.Item.FindControl("ddlExamsList"), DropDownList)
            examList.DataSource = _examsList
            examList.DataValueField = "Id"
            examList.DataTextField = "Name"
            examList.DataBind()
            examList.SelectedValue = 2
            If Convert.ToInt32(examData.Text) = 4 Then
                examList.Visible = True
            Else
                examList.Visible = False
            End If
        End If
    End Sub

    Protected Sub gvCap1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvCap1.ItemDataBound
        If e.Item.FindControl("ddlExamsList") IsNot Nothing And e.Item.FindControl("lblExamData") IsNot Nothing Then
            Dim examData As Label = CType(e.Item.FindControl("lblExamData"), Label)
            Dim examList As DropDownList = CType(e.Item.FindControl("ddlExamsList"), DropDownList)
            examList.DataSource = _examsList
            examList.DataValueField = "Id"
            examList.DataTextField = "Name"
            examList.DataBind()
            examList.SelectedValue = 2
            If Convert.ToInt32(examData.Text) = 4 Then
                examList.Visible = True
            Else
                examList.Visible = False
            End If
        End If
    End Sub

    Protected Sub gvCap2_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvCap2.ItemDataBound
        If e.Item.FindControl("ddlExamsList") IsNot Nothing And e.Item.FindControl("lblExamData") IsNot Nothing Then
            Dim examData As Label = CType(e.Item.FindControl("lblExamData"), Label)

            Dim examList As DropDownList = CType(e.Item.FindControl("ddlExamsList"), DropDownList)
            examList.DataSource = _examsList
            examList.DataValueField = "Id"
            examList.DataTextField = "Name"
            examList.DataBind()
            If Convert.ToInt32(examData.Text) = 4 Then
                examList.Visible = True
            Else
                examList.Visible = False
            End If
        End If
    End Sub

    Protected Sub gvCap1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCap1.NeedDataSource
        gvCap1.DataSource = _classCap1List

    End Sub

    Protected Sub gvCap2_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCap2.NeedDataSource
        gvCap2.DataSource = _classCap2List
    End Sub

    ''' <summary>
    ''' Handles the ok/save button click of the modal-popup
    ''' </summary>
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Dim table As New DataTable()
        'Dim oRuleEngineResultGE As AptifyGenericEntityBase
        'For Each row As GridDataItem In gvCap1.Items
        '    oRuleEngineResultGE = AptifyApplication.GetEntityObject("RuleEngineResult__c", Convert.ToInt64(row.Item("ID")))
        '    Dim checkBox As CheckBox = row.Item("CAP1Courses").FindControl("chkIsCoursesCompleted")
        '    If checkBox.Visible = True Then
        '        oRuleEngineResultGE.SetValue("IsCompleted", checkBox.Checked)
        '    End If
        'Next
        'For Each row As GridDataItem In gvCap2.Items
        '    oRuleEngineResultGE.SetValue("", "")
        'Next
        'For Each row As GridDataItem In gvFAE.Items
        '    oRuleEngineResultGE.SetValue("", "")
        'Next
        '' oRuleEngineResultGE.Save()

        UpdateVenueNameData(ddlVenuesList.SelectedItem.ToString())
        For Each row As GridDataItem In gvCap1.MasterTableView.Items
            Dim courseId = row("Name").Text
            Dim isCoursesCompleted As CheckBox = row.Item("CAP1Courses").FindControl("chkIsCoursesCompleted")
            If isCoursesCompleted.Visible = True Then
                UpdatePivodGridData("CAP1", "2;Course", courseId, isCoursesCompleted.Checked)
            End If

            Dim isRevisionCompleted As CheckBox = row.Item("CAP1Revision").FindControl("chkIsRevisionCompleted")
            If isRevisionCompleted.Visible = True Then
                UpdatePivodGridData("CAP1", "Revision", courseId, isRevisionCompleted.Checked)
            End If

            Dim isResitInterimCompleted As CheckBox = row.Item("CAP1ResitInterim").FindControl("chkIsResitInterimCompleted")
            If isRevisionCompleted.Visible = True Then
                UpdatePivodGridData("CAP1", "Resit Interim Assessment", courseId, isResitInterimCompleted.Checked)
            End If
        Next

        For Each row As GridDataItem In gvCap2.Items
            Dim courseId = row("Name").Text
            Dim isCoursesCompleted As CheckBox = row.Item("CAP2Courses").FindControl("chkIsCoursesCompleted")
            If isCoursesCompleted.Visible = True Then
                UpdatePivodGridData("CAP2", "2;Course", courseId, isCoursesCompleted.Checked)
            End If

            Dim isRevisionCompleted As CheckBox = row.Item("CAP2Revision").FindControl("chkIsRevisionCompleted")
            If isRevisionCompleted.Visible = True Then
                UpdatePivodGridData("CAP2", "Revision", courseId, isRevisionCompleted.Checked)
            End If

            Dim isResitInterimCompleted As CheckBox = row.Item("CAP2ResitInterim").FindControl("chkIsResitInterimCompleted")
            If isRevisionCompleted.Visible = True Then
                UpdatePivodGridData("CAP2", "Resit Interim Assessment", courseId, isResitInterimCompleted.Checked)
            End If
        Next

        For Each row As GridDataItem In gvFAE.Items
            Dim courseId = row("Name").Text
            Dim isCoursesCompleted As CheckBox = row.Item("FAECourses").FindControl("chkIsCoursesCompleted")
            If isCoursesCompleted.Visible = True Then
                UpdatePivodGridData("FAE", "2;Course", courseId, isCoursesCompleted.Checked)
            End If

            Dim isRevisionCompleted As CheckBox = row.Item("FAERevision").FindControl("chkIsRevisionCompleted")
            If isRevisionCompleted.Visible = True Then
                UpdatePivodGridData("FAE", "Revision", courseId, isRevisionCompleted.Checked)
            End If

            Dim isResitInterimCompleted As CheckBox = row.Item("FAEResitInterim").FindControl("chkIsResitInterimCompleted")
            If isRevisionCompleted.Visible = True Then
                UpdatePivodGridData("FAE", "Resit Interim Assessment", courseId, isResitInterimCompleted.Checked)
            End If
        Next

        gvFirmEnrollment.DataSource = _enrollmentTable

    End Sub

    ''' <summary>
    ''' To update the data table 
    ''' </summary>
    Private Sub UpdatePivodGridData(ByVal cap As String, ByVal capspart As String, ByVal courseId As String, ByVal isCompleted As Boolean)
        Dim query = (From stud In _enrollmentTable.AsEnumerable()
                    Where stud.Field(Of Integer)("StudentID") = _selectedStudentId AndAlso
                    stud.Field(Of String)("Name") = cap AndAlso
                    stud.Field(Of String)("CapParts") = capspart AndAlso
                    stud.Field(Of String)("CapSubParts") = courseId
                    Select stud).ToList()
        For Each row As DataRow In query
            Dim data As Integer = 0
            If isCompleted = True Then
                data = 1
            End If
            row.SetField(Of Integer)("IsCompleted", data)
        Next
    End Sub

    ''' <summary>
    ''' To update the data table 
    ''' </summary>
    Private Sub UpdateVenueNameData(ByVal venueName As String)
        Dim query = (From stud In _enrollmentTable.AsEnumerable()
                    Where stud.Field(Of Integer)("StudentID") = _selectedStudentId
                    Select stud).ToList()
        For Each row As DataRow In query
            row.SetField("VenueName", venueName)
        Next
    End Sub

    Protected Sub ddlVenuesList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlVenuesList.SelectedIndexChanged
        LoadTimeTable(ddlVenuesList.SelectedValue)
        ddlCap2TimeTableList.DataSource = _timeTableList
        ddlCap2TimeTableList.DataValueField = "ID"
        ddlCap2TimeTableList.DataTextField = "Name"
        ddlCap2TimeTableList.DataBind()

        ddlCap1TimetableList.DataSource = _timeTableList
        ddlCap1TimetableList.DataValueField = "ID"
        ddlCap1TimetableList.DataTextField = "Name"
        ddlCap1TimetableList.DataBind()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        radStundentEnrollment.VisibleOnPageLoad = False
    End Sub
End Class
