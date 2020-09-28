'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                      Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                   12/16/2014                          If student has approved web material access then only display course material 
'Milind Sutar                   03/19/2015                              
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports Aptify.Framework.BusinessLogic.GenericEntity

Imports System.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Drawing

Imports Telerik.Web.UI
Imports Telerik.Web.UI.PivotGrid
Imports Telerik.Web.UI.PivotGrid.Core

Partial Class StudentWebCourseDetails__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

    Private _courseDetails As New DataTable()

    ''' <summary>
    ''' Handles page load event
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If HasWebMaterialAccess() Then
                lblErrorMsg.Visible = False
                pnlCourseDetails.Visible = True
                LoadHeaderText()
                LoadCurriculum()
                LoadCourses()
                LoadStudentGroup()
            Else
                pnlCourseDetails.Visible = False
                lblErrorMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebMaterialAccess.AccessMsg")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Load Header
    ''' </summary>
    Private Sub LoadHeaderText()
        Try
            lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Consulting.WebMaterialAccessWizard.Note")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblDownloadFormat.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.DownloadLinkFormat")), _
                Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            Dim sql As New StringBuilder()
            'Siddharth: Commented below line and used 'spCommonCurrentAcadmicCycle__c' SP to get current academic cycle
            'sql.AppendFormat("{0}..spGetAcademicCycleYearAsPerCurrentDate__c @Year={1}", _
            '                Me.Database, Today.Year)
            sql.AppendFormat("{0}..spCommonCurrentAcadmicCycle__c", Me.Database)
            Dim dt As DataTable = DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ViewState("AcademicCycleID") = Convert.ToString(dt.Rows(0)(0))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' if Web Material Status is Approved then only display courses and download link
    ''' </summary>
    Private Function HasWebMaterialAccess() As Boolean
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetWebMaterialStatusApproved__c @PersonID={1}", _
                            Me.Database, Me.User1.PersonID)
            Dim lWebMaterialApproved As Long = Convert.ToInt32(DataAction.ExecuteScalar(sql.ToString(), _
                    Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            If lWebMaterialApproved > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function

    ''' <summary>
    ''' Load Curriculum
    ''' </summary>
    Private Sub LoadCurriculum()
        Try
            Dim sSql As String = Database & "..spGetCurriculumDefinationDetails__c"
            Dim dt As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlCurriculumList.DataSource = dt
            ddlCurriculumList.DataTextField = "Name"
            ddlCurriculumList.DataValueField = "ID"
            ddlCurriculumList.DataBind()
            ddlCurriculumList.Items.Insert(0, "-- Select Curriculum --")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load drop down
    ''' </summary>
    Private Sub LoadCourses()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spCommonGetCurriculumCourses__c @CurriculumID={1}", _
                   Me.Database, Convert.ToInt32(ddlCurriculumList.SelectedValue))
            Dim dt As DataTable = DataAction.GetDataTable(sql.ToString, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlCourseList.DataSource = dt
            ddlCourseList.DataTextField = "CourseName"
            ddlCourseList.DataValueField = "CourseID"
            ddlCourseList.DataBind()
            ddlCourseList.Items.Insert(0, "-- Select Course --")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load student group
    ''' </summary>
    Private Sub LoadStudentGroup()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetCurriculumStudentGroup__c @StudentID={1},@AcademicCycleID={2},@CourseID={3},@CurriculumID={4}", _
                             Me.Database, Me.User1.PersonID, Convert.ToInt32(ViewState("AcademicCycleID")), Me.ddlCourseList.SelectedValue, Me.ddlCurriculumList.SelectedValue)
            Dim dt As DataTable = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlGroupList.DataSource = dt
            ddlGroupList.DataTextField = "Name"
            ddlGroupList.DataValueField = "ID"
            ddlGroupList.DataBind()
            ddlGroupList.Items.Insert(0, "-- Select Student Group --")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load the student course details 
    ''' </summary>
    Private Sub LoadStudentCourseDetails()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetStudentCourseGroupDetails__c @StudentID={1},@AcademicCycleID={2},@CourseID={3},@GroupID={4}", _
                             Me.Database, User1.PersonID, Convert.ToInt32(ViewState("AcademicCycleID")), ddlCourseList.SelectedValue, ddlGroupList.SelectedValue)
            _courseDetails = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load Document as per Course Material Category
    ''' </summary>
    Protected Sub gvCourseDetails_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvCourseDetails.ItemCommand
        Try
            If e.CommandName = "Download" Then
                radDownloadDocuments.VisibleOnPageLoad = True
                Dim data As String = e.CommandArgument.ToString
                Dim recordID As Integer = CInt(data)
                Dim entityID As Integer = CInt(Me.AptifyApplication.GetEntityID("Course Parts"))
                Me.ucDownload.AttachmentCategory = AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Course Material")
                ucDownload.LoadAttachments(entityID, recordID)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles close button click
    ''' </summary>
    Protected Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
        radDownloadDocuments.VisibleOnPageLoad = False
    End Sub

    ''' <summary>
    ''' Load Courses as per curriculum
    ''' </summary>
    Protected Sub ddlCurriculumList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCurriculumList.SelectedIndexChanged
        Try
            LoadCourses()
            gvCourseDetails.Rebind()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load Student group as per courses
    ''' </summary>
    Protected Sub ddlCourseList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCourseList.SelectedIndexChanged
        Try
            LoadStudentGroup()
            gvCourseDetails.Rebind()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load Course details as per course and student group
    ''' </summary>
    Protected Sub ddlGroupList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlGroupList.SelectedIndexChanged
        Try
            gvCourseDetails.Rebind()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles need data source to load/reload grid 
    ''' </summary>
    Protected Sub gvCourseDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCourseDetails.NeedDataSource
        LoadStudentCourseDetails()
        If Not _courseDetails Is Nothing Then
            gvCourseDetails.DataSource = _courseDetails
            lblCourseDetailMsg.Text = String.Empty
        Else
            lblCourseDetailMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebMaterialAccess.CourseMaterialNotFound")), _
                Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        End If
    End Sub

End Class
