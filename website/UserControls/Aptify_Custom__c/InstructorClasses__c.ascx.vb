Option Explicit On
Option Strict On
Imports System.Data
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Applications.ProductSetup
Imports Aptify.Framework.DataServices
Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class InstructorClasses__c
        Inherits eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "InstructorClasses__c"
        Dim sAllTimeTable As String = String.Empty
        Dim IsCalenderDate As Boolean = False
        Dim lCurriculumID As Long
        Dim lTimeTableID As Long
        Dim dtClasses As DataTable

#Region "InstructorClasses Specific Properties"
        Dim GridDataItem As GridItem

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
        End Sub
#End Region

#Region "Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not IsPostBack Then                
                GetDataAndAssign()
                grdClasses.Rebind()                
                pnlList.Visible = False
                lnkCalendar.Visible = False
            End If
        End Sub

        Protected Sub cmbType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbType.SelectedIndexChanged            
            grdClasses.Rebind()
        End Sub

        Protected Sub lnkCalendar_Click(sender As Object, e As System.EventArgs) Handles lnkCalendar.Click
            pnlCalendar.Visible = True
            pnlList.Visible = False
            lnkCalendar.Visible = False
            lnkList.Visible = True
        End Sub

        Protected Sub lnkList_Click(sender As Object, e As System.EventArgs) Handles lnkList.Click
            pnlCalendar.Visible = False
            pnlList.Visible = True
            lnkCalendar.Visible = True
            lnkList.Visible = False
        End Sub

        Protected Sub grdClasses_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdClasses.NeedDataSource
            GetDataAndAssign()
            If dtClasses IsNot Nothing Then
                grdClasses.DataSource = dtClasses                
            End If
        End Sub
        'redmine 13806
        Protected Sub radSchedulerLecturer_AppointmentDataBound(sender As Object, e As SchedulerEventArgs) Handles radSchedulerLecturer.AppointmentDataBound
            e.Appointment.ToolTip = String.Empty
        End Sub

        Protected Sub grdClasses_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdClasses.ItemCommand
            Try

                If e.CommandName = "Download" Then
                    radDownloadDocuments.VisibleOnPageLoad = True
                    Dim data As String() = e.CommandArgument.ToString().Split(CChar(";"))
                    Dim recordID As Integer = CInt(data(0))
                    Dim entityID As Integer = CInt(Me.AptifyApplication.GetEntityID("Course Parts"))
                    RecordAttachments__c.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Course Material")
                    RecordAttachments__c.LoadAttachments(entityID, recordID, True)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Handles the cancel button event - download couse materials popup
        ''' </summary>
        Protected Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
            radDownloadDocuments.VisibleOnPageLoad = False
        End Sub
#End Region

#Region "Functions"        
        Private Sub CheckInstructor()
            ' determine if the individual logged in is an active instructor
            ' on any course. If so, show the InstructorCenter link.
            If Not InstructorValidator1.IsCurrentUserInstructor() Then
                Me.tabData.Visible = False
            End If
        End Sub

        Private Sub GetDataAndAssign()
            Dim sSql As String
            sSql = Database & "..spGetClassesForInstructor__c @InstructorID=" & InstructorValidator1.User.PersonID.ToString() & ", @AllClass='" & cmbType.SelectedValue.ToUpper.Trim & "'"
            dtClasses = Me.DataAction.GetDataTable(sSql)
            If Not dtClasses Is Nothing AndAlso dtClasses.Rows.Count > 0 Then
                ViewState("oDT") = dtClasses                
                radSchedulerLecturer.DataSource = dtClasses
                radSchedulerLecturer.DataBind()
                lblGridMsg.Visible = False
                'Calendar1.Visible = True
                pnlCalendar.Style.Item("Display") = "block"
                pnlList.Style.Item("Display") = "block"
            Else
                lblGridMsg.Visible = True
                'grdClasses.DataSource = Nothing
                'grdClasses.DataBind()                  
                'Calendar1.Visible = False
                pnlList.Style.Item("Display") = "none"
                pnlCalendar.Style.Item("Display") = "none"
            End If
        End Sub
#End Region
    End Class
End Namespace
