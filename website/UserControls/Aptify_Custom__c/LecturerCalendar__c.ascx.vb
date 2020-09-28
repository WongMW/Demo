Option Explicit On
Option Strict On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
'Developer                  Date modified               Comments 
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Milind Sutar               29/08/2014                  Calendar view control for lecturer to add/edit/delete events
'Milind Sutar               19/01/2015                  Update the control as per new TDD
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports System.Data

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.GenericEntity

Partial Class UserControls_Aptify_Custom__c_LecturerCalendar__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

#Region "Properties"

    Private _calendarCategotyList As New DataTable()
    Private Shared _lecturerCalendarData As New DataTable()
    Private Const CalendarView As String = "0"
    Private Const ListView As String = "1"

    ''' <summary>
    ''' Gets/Sets event id
    ''' </summary>
    Public Property EventID() As Long
        Get
            Return CLng(ViewState.Item("EventID"))
        End Get
        Set(ByVal value As Long)
            ViewState.Item("EventID") = CStr(value)
        End Set
    End Property

#End Region

#Region "Protected Methods"

    ''' <summary>
    ''' Handles the page load event
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblPageMessage.Text = String.Empty
            pnlListView.Visible = False
            lbtnCalendar.Visible = False
            If IsLecturer() Then
                LoadEvents()
                LoadCalendarCategoryList()
            Else
                radSchedulerLecturer.Visible = False
                lblPageMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.LecturerCalendar.Authentication")), _
                    Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPageMessage.ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles appointment click event to show the cutom appointment popup
    ''' </summary>
    Protected Sub radSchedulerLecturer_AppointmentClick(sender As Object, e As Telerik.Web.UI.SchedulerEventArgs) Handles radSchedulerLecturer.AppointmentClick
        Try
            lblMessage.Text = String.Empty
            lblMessage.Style.Add("display", "none")
            btnSave.Style.Add("display", "inline")
            If e.Appointment.End <= DateTime.Now Then
                DisableSaveEvent()
            End If
            Me.EventID = CLng(e.Appointment.ID)
            LoadPopup(e.Appointment.Subject)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles appointment delete to to delete any event
    ''' </summary>
    Protected Sub radSchedulerLecturer_AppointmentDelete(sender As Object, e As Telerik.Web.UI.AppointmentDeleteEventArgs) Handles radSchedulerLecturer.AppointmentDelete
        Try
            radWindowPopup.VisibleOnPageLoad = True
            If e.Appointment.End > DateTime.Now Then
                Dim errorMessage As String = String.Empty
                Me.EventID = CType(e.Appointment.ID, Long)
                lblValidationMessage.Text = "Are you sure you want to delete this event?"
                btnDelete.Visible = True
                btnCancelDelete.Visible = True
                btnOk.Visible = False
            Else
                btnOk.Visible = True
                btnDelete.Visible = False
                btnCancelDelete.Visible = False
                lblValidationMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.LecturerCalendar.DeleteValidation")), _
                    Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
            LoadEvents()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles appointment update event to update the existing appointment/an event
    ''' </summary>
    Protected Sub radSchedulerLecturer_AppointmentUpdate(sender As Object, e As Telerik.Web.UI.AppointmentUpdateEventArgs) Handles radSchedulerLecturer.AppointmentUpdate
        Try
            
            Me.EventID = CLng(e.Appointment.ID)
            radStartDate.SelectedDate = e.ModifiedAppointment.Start
            radEndDate.SelectedDate = e.ModifiedAppointment.End
            Dim startTime As New TimeSpan(e.ModifiedAppointment.Start.Hour, e.ModifiedAppointment.Start.Minute, e.ModifiedAppointment.Start.Second)
            Dim endTime As New TimeSpan(e.ModifiedAppointment.End.Hour, e.ModifiedAppointment.End.Minute, e.ModifiedAppointment.End.Second)
            radStartTime.SelectedTime = startTime
            radEndTime.SelectedTime = endTime
            UpdateEvent()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles grid view item command event to edit event using popup 
    ''' </summary>
    Protected Sub gvHolidays_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvHolidays.ItemCommand
        Try
            If e.CommandName = "EditEvent" Then
                Dim data As String() = e.CommandArgument.ToString().Split(CChar(";"))
                Me.EventID = CInt(data(0))
                Dim subject As String = CStr(data(1))
                LoadPopup(subject)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles need data source event to bind data to grid view
    ''' </summary>
    Protected Sub gvHolidays_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvHolidays.NeedDataSource
        If _lecturerCalendarData IsNot Nothing Then
            gvHolidays.DataSource = _lecturerCalendarData
        End If
    End Sub

    ''' <summary>
    ''' Handles calendar view link button event to show calendar view
    ''' </summary>
    Protected Sub lbtnCalendar_Click(sender As Object, e As System.EventArgs) Handles lbtnCalendar.Click
        pnlCalendar.Visible = True
        pnlListView.Visible = False
        lbtnListView.Visible = True
        lbtnCalendar.Visible = False
    End Sub

    ''' <summary>
    ''' Handles list view link button event to show list view
    ''' </summary>
    Protected Sub lbtnListView_Click(sender As Object, e As System.EventArgs) Handles lbtnListView.Click
        pnlCalendar.Visible = False
        pnlListView.Visible = True
        lbtnListView.Visible = False
        lbtnCalendar.Visible = True
    End Sub

    ''' <summary>
    ''' Handles save button event to save event
    ''' </summary>
    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Try
            SaveEvent()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles ok button click - to close popup
    ''' </summary>
    Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        radWindowPopup.VisibleOnPageLoad = False
    End Sub


    ''' <summary>
    ''' Delete an event on confirm delete button on - confirmation dialog/poup
    ''' </summary>
    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim calendar As AptifyGenericEntityBase
            calendar = Me.AptifyApplication.GetEntityObject("AdminOrganizationCalendar__c", EventID)
            Dim result As Boolean = calendar.Delete()
            If result Then
                radWindowPopup.VisibleOnPageLoad = False
                ClearFields()
            Else
                lblMessage.Text = String.Empty
                lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.LecturerCalendar.DeleteFailed")), _
                Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblMessage.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            lblMessage.Text = String.Empty
            lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
            Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.LecturerCalendar.DeleteFailed")), _
            Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblMessage.ForeColor = Drawing.Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Handles cancel delete button event on - confirmation dialog/poup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCancelDelete_Click(sender As Object, e As System.EventArgs) Handles btnCancelDelete.Click
        radWindowPopup.VisibleOnPageLoad = False
    End Sub

    ''' <summary>
    ''' Handles the cancel button event 
    ''' </summary>
    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        radCalendarUpdate.VisibleOnPageLoad = False
        ClearFields()
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' To check if logged in user is a lecturer
    ''' </summary>
    Private Function IsLecturer() As Boolean
        Dim result As Boolean
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("..spIsPersonLecturer__c @LoggedInUser={0}", LoggedInUser.PersonID)
            result = CType(Me.DataAction.ExecuteScalar(sql.ToString(), IAptifyDataAction.DSLCacheSetting.BypassCache), Boolean)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return result
    End Function

    ''' <summary>
    ''' To load all event data
    ''' </summary>
    Private Sub LoadEvents()
        Try
            radSchedulerLecturer.TimeZoneID = Session("OffSetVal").ToString()
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetLecturerEvents__c @LoggedInUser={1}", Me.Database, LoggedInUser.PersonID)
            _lecturerCalendarData = Me.DataAction.GetDataTable(sql.ToString(), IAptifyDataAction.DSLCacheSetting.BypassCache)
            radSchedulerLecturer.DataSource = _lecturerCalendarData
            radSchedulerLecturer.DataBind()
            gvHolidays.Rebind()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To load calendar category list
    ''' </summary>
    Private Sub LoadCalendarCategoryList()
        Try
            Dim sql As String = "..spGetAllCalendarCategories__c"
            Dim dataTable As New DataTable()
            dataTable = Me.DataAction.GetDataTable(sql, IAptifyDataAction.DSLCacheSetting.BypassCache)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To clear popup field data
    ''' </summary>
    Private Sub ClearFields()
        txtName.Text = String.Empty
        txtDescription.Text = String.Empty
        chkAllDayEvent.Checked = False
        radStartTime.Enabled = True
        radEndTime.Enabled = True
        radStartDate.Clear()
        radEndDate.Clear()
        radStartTime.Clear()
        radEndTime.Clear()
        LoadEvents()
        Me.EventID = -1
    End Sub

    ''' <summary>
    ''' To save event to AdminOrganizationCalendar__c entity
    ''' </summary>
    Private Sub SaveEvent()
        Try
            Dim categoryID As Long = Me.AptifyApplication.GetEntityRecordIDFromRecordName("CalendarCategory__c", "Lecturer Blocked")
            Dim calendar As AptifyGenericEntityBase
            calendar = Me.AptifyApplication.GetEntityObject("AdminOrganizationCalendar__c", Me.EventID)
            calendar.SetValue("Name", txtName.Text.Trim())
            calendar.SetValue("Description", txtDescription.Text.Trim())
            calendar.SetValue("CategoryID", categoryID)

            Dim startDate As DateTime = radStartDate.SelectedDate.Value
            Dim startHr As Integer = 0
            Dim startMin As Integer = 0
            Dim startSec As Integer = 0
            If chkAllDayEvent.Checked = True Then
                startHr = 0
                startMin = 0
                startSec = 0
            ElseIf radStartTime.SelectedDate IsNot Nothing Then
                startHr = radStartTime.SelectedDate.Value.Hour
                startMin = radStartTime.SelectedDate.Value.Minute
                startSec = radStartTime.SelectedDate.Value.Second
            End If
            Dim startDateTime As New DateTime(startDate.Year, startDate.Month, startDate.Day, startHr, startMin, startSec)

            Dim endDate As DateTime = radEndDate.SelectedDate.Value
            Dim endHr As Integer = 0
            Dim endMin As Integer = 0
            Dim endSec As Integer = 0
            If chkAllDayEvent.Checked = True Then
                endHr = 23
                endMin = 59
                endSec = 59
            ElseIf radEndTime.SelectedDate IsNot Nothing Then
                endHr = radEndTime.SelectedDate.Value.Hour
                endMin = radEndTime.SelectedDate.Value.Minute
                endSec = radEndTime.SelectedDate.Value.Second
            End If
            Dim endDateTime As New DateTime(endDate.Year, endDate.Month, endDate.Day, endHr, endMin, endSec)

            calendar.SetValue("StartDate", startDateTime)
            calendar.SetValue("EndDate", endDateTime)
            calendar.SetValue("PersonID", LoggedInUser.PersonID)
            Dim errorMessage As String = String.Empty
            Dim result = calendar.Save(errorMessage)
            If result = True Then
                radCalendarUpdate.VisibleOnPageLoad = False
                LoadEvents()
            ElseIf result = False Then
                lblMessage.Text = String.Empty
                lblMessage.Text = errorMessage
                lblMessage.ForeColor = Drawing.Color.Red
            End If
            ClearFields()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Update the dates on appointment update event
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateEvent()
        Try

            Dim query = (From p In _lecturerCalendarData.AsEnumerable()
                Where p.Field(Of Integer)("ID") = CInt(Me.EventID))
            If query.Count() > 0 Then
                'radStartTime.SelectedDate = Convert.ToDateTime(CStr(query.First().Field(Of DateTime)("StartDateTime")))
                'radEndTime.SelectedDate = Convert.ToDateTime(CStr(query.First().Field(Of DateTime)("EndDateTime")))
                If radStartTime.SelectedTime.Value = TimeSpan.Parse("23:59:59") Or _
                    radEndTime.SelectedTime.Value = TimeSpan.Parse("23:59:59") Then
                    chkAllDayEvent.Checked = True
                End If
            End If

            Dim calendar As AptifyGenericEntityBase
            calendar = Me.AptifyApplication.GetEntityObject("AdminOrganizationCalendar__c", Me.EventID)

            Dim startDate As DateTime = radStartDate.SelectedDate.Value
            Dim startHr As Integer = 0
            Dim startMin As Integer = 0
            Dim startSec As Integer = 0
            If chkAllDayEvent.Checked = True Then
                startHr = 0
                startMin = 0
                startSec = 0
            ElseIf radStartTime.SelectedDate IsNot Nothing Then
                startHr = radStartTime.SelectedDate.Value.Hour
                startMin = radStartTime.SelectedDate.Value.Minute
            End If
            Dim startDateTime As New DateTime(startDate.Year, startDate.Month, startDate.Day, startHr, startMin, startSec)

            Dim endDate As DateTime = radEndDate.SelectedDate.Value
            Dim endHr As Integer = 0
            Dim endMin As Integer = 0
            Dim endSec As Integer = 0
            If chkAllDayEvent.Checked = True Then
                endHr = 23
                endMin = 59
                endSec = 59
            ElseIf radEndTime.SelectedDate IsNot Nothing Then
                endHr = radEndTime.SelectedDate.Value.Hour
                endMin = radEndTime.SelectedDate.Value.Minute
            End If
            Dim endDateTime As New DateTime(endDate.Year, endDate.Month, endDate.Day, endHr, endMin, endSec)

            calendar.SetValue("StartDate", startDateTime)
            calendar.SetValue("EndDate", endDateTime)
            Dim errorMessage As String = String.Empty
            Dim result = calendar.Save(errorMessage)
            If result = True Then
                LoadEvents()
            ElseIf result = False Then
                lblMessage.Text = String.Empty
                lblMessage.Text = errorMessage
                lblMessage.ForeColor = Drawing.Color.Red
            End If
            ClearFields()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To load popup data 
    ''' </summary>
    Private Sub LoadPopup(subject As String)
        Try
            Dim query = (From p In _lecturerCalendarData.AsEnumerable()
                   Where p.Field(Of Integer)("ID") = CInt(Me.EventID))
            If query.Count() > 0 Then
                txtName.Text = subject
                radStartDate.SelectedDate = CDate((query.First().Field(Of Date)("StartDateTime")))
                radEndDate.SelectedDate = CDate(query.First().Field(Of Date)("EndDateTime"))
                radStartTime.SelectedDate = Convert.ToDateTime(CStr(query.First().Field(Of DateTime)("StartDateTime")))
                radEndTime.SelectedDate = Convert.ToDateTime(CStr(query.First().Field(Of DateTime)("EndDateTime")))
                If radStartTime.SelectedTime.Value = TimeSpan.Parse("00:00:00") Or _
                    radEndTime.SelectedTime.Value = TimeSpan.Parse("23:59:59") Then
                    chkAllDayEvent.Checked = True
                    radStartTime.Enabled = False
                    radEndTime.Enabled = False
                End If
                txtDescription.Text = CStr(query.First().Field(Of String)("Description"))
                radCalendarUpdate.VisibleOnPageLoad = True
                If radEndTime.SelectedDate <= DateTime.Now Then
                    DisableSaveEvent()
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To disable or hide the save button - for past events
    ''' </summary>
    Private Sub DisableSaveEvent()
        Try
            btnSave.Style.Add("display", "none")
            lblMessage.Style.Add("display", "inline")
            lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.LecturerCalendar.EditValidation")), _
                Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblMessage.Font.Bold = True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

    
End Class
