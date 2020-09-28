''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Kavita Zinage        06/10/2015                      Firm Admin Student Assignment Details Page 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Strict On
Option Explicit On

Imports System.Data
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Framework.Application
Imports System.IO
Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class FirmPortalPage__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_FIRMPORTALPAGE As String = "FirmPortalPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL As String = "StudentResultUrl"

        
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

        Public Overridable Property FirmPortalPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FIRMPORTALPAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FIRMPORTALPAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FIRMPORTALPAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property StudentResultUrl() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(FirmPortalPage) Then
                FirmPortalPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_FIRMPORTALPAGE)
            End If
            If String.IsNullOrEmpty(StudentResultUrl) Then
                StudentResultUrl = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL)
            End If
        End Sub
#End Region

#Region "Page Events"
        ''' <summary>
        ''' Handles page load event
        ''' </summary>
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()
                If Not IsPostBack Then
                    ReLoadGrid()
                    lblExemptionsNotFound.Text = String.Empty
                    LoadAcademicYear()
                    LoadCurriculum()
                    LoadCourses()

                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Handles page PreRender event
        ''' </summary>
        Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
            gvAssignmentDetails.Columns(0).CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.EqualTo
        End Sub

#End Region

#Region "Private Functions"
        ''' <summary>
        ''' Load gvAssignmentDetails
        ''' </summary>
        Private Sub LoadGrid()
            Try
                Dim iStudent = 0, iCourse As Integer = 0
                If rdoAcademicCycle.Checked = True Then
                    iCourse = Convert.ToInt32(ddlCourseList.SelectedValue)
                End If
                If rdoStudent.Checked = True Then
                    iStudent = Convert.ToInt32(txtStudent.Text)
                End If

                Dim sSql As New StringBuilder()
                sSql.AppendFormat("{0}..spGetStudentAssignmentDetails__c @CompanyID={1},@AcademicCycleID={2},@CurriculumID={3},@CourseID={4},@StudentID={5}", _
                       Me.Database, User1.CompanyID, Convert.ToInt32(ddlAcademicYear.SelectedValue), Convert.ToInt32(ddlCurriculumList.SelectedValue), iCourse, iStudent)
                Dim dt As DataTable = DataAction.GetDataTable(sSql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    pnlDetails.Visible = True
                    ViewState("AssignmentDetails") = dt
                    Me.gvAssignmentDetails.Visible = True
                    Me.gvAssignmentDetails.Rebind()
                Else
                    ReLoadGrid()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Load all academic cycles 
        ''' </summary>
        Private Sub LoadAcademicYear()
            Try
                Dim sSql As New StringBuilder()
                If rdoAcademicCycle.Checked Then
                    sSql.AppendFormat("{0} ..spGetAllAcademicCycles__c", Database)
                Else
                    sSql.AppendFormat("{0} ..spGetAllAcademicYears__c", Database)
                End If
                ddlAcademicYear.DataSource = DataAction.GetDataTable(sSql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                ddlAcademicYear.DataValueField = "Id"
                ddlAcademicYear.DataTextField = "Name"
                ddlAcademicYear.DataBind()
                ddlAcademicYear.Items.Insert(0, "Select academic cycle")
            Catch ex As Exception
                lblExemptionsNotFound.Text = ex.Message
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Load Curriculum
        ''' </summary>
        Private Sub LoadCurriculum()
            Try
                Dim sSql As New StringBuilder()
                If rdoAcademicCycle.Checked Then
                    sSql.AppendFormat("{0} ..spGetCurriculumDefinationDetails__c", Database)
                Else
                    sSql.AppendFormat("{0} ..spGetAllCurriculumDefinationDetails__c", Database)
                End If
                Dim dt As DataTable = DataAction.GetDataTable(sSql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                ddlCurriculumList.DataSource = dt
                ddlCurriculumList.DataTextField = "Name"
                ddlCurriculumList.DataValueField = "ID"
                ddlCurriculumList.DataBind()
                ddlCurriculumList.Items.Insert(0, "Select curriculum")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Load drop down
        ''' </summary>
        Private Sub LoadCourses()
            Try
                Dim sSql As New StringBuilder()
                sSql.AppendFormat("{0}..spGetCoursesFromCurriculumID__c @ID={1}", _
                       Me.Database, Convert.ToInt32(ddlCurriculumList.SelectedValue))
                Dim dt As DataTable = DataAction.GetDataTable(sSql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                ddlCourseList.DataSource = dt
                ddlCourseList.DataTextField = "Name"
                ddlCourseList.DataValueField = "ID"
                ddlCourseList.DataBind()
                ddlCourseList.Items.Insert(0, "Select course")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
   
        ' ''' <summary>
        ' ''' Load Grid as per Assignment
        ' ''' </summary>
        Protected Sub ddlAcademicYear_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAcademicYear.SelectedIndexChanged
            Try
                LoadCurriculum()
                txtStudent.Text = ""
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Load Courses as per curriculum
        ''' </summary>
        Protected Sub ddlCurriculumList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCurriculumList.SelectedIndexChanged
            Try
                If ddlCurriculumList.SelectedValue = "Select curriculum" Then
                    ddlCourseList.Items.Clear()
                    txtStudent.Text = ""
                Else
                    LoadCourses()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

        ''' <summary>
        ''' Handles Display button click
        ''' </summary>
        Protected Sub btnDisplay_Click(sender As Object, e As System.EventArgs) Handles btnDisplay.Click
            Try
                If rdoAcademicCycle.Checked Then
                    If ddlAcademicYear.SelectedValue = "Select academic cycle" Or ddlCurriculumList.SelectedValue = "Select curriculum" Or ddlCourseList.SelectedValue = "Select course" Then
                        lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AssignmentDetailsPage.RequiredFieldMsg")),
                        Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Else
                        lblExemptionsNotFound.Text = String.Empty
                        LoadGrid()
                    End If
                Else
                    If ddlAcademicYear.SelectedValue = "Select academic cycle" Or ddlCurriculumList.SelectedValue = "Select curriculum" Or String.IsNullOrEmpty(txtStudent.Text) Then
                        lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AssignmentDetailsPage.RequiredFieldMsg")),
                        Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Else
                        lblExemptionsNotFound.Text = String.Empty
                        LoadGrid()
                    End If
                End If
                

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Handles need data source to load/reload grid 
        ''' </summary>
        Protected Sub gvAssignmentDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvAssignmentDetails.NeedDataSource
            Try
                If ViewState("AssignmentDetails") IsNot Nothing Then
                    gvAssignmentDetails.DataSource = CType(ViewState("AssignmentDetails"), DataTable)
                    InvisibleColumn()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Handles RadioButton Checked Changed
        ''' </summary>
        Protected Sub rdoAcademicCycle_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoAcademicCycle.CheckedChanged
            Try
                LoadAcademicYear()
                LoadCurriculum()
                ddlCourseList.Items.Clear()
                lblCourse.Visible = True
                ddlCourseList.Visible = True
                lblStudent.Visible = False
                txtStudent.Visible = False
                txtStudent.Text = ""
                lblExemptionsNotFound.Text = String.Empty
                ReLoadGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Handles RadioButton Checked Changed
        ''' </summary>
        Protected Sub rdoStudent_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoStudent.CheckedChanged
            Try
                LoadAcademicYear()
                LoadCurriculum()
                lblStudent.Visible = True
                txtStudent.Visible = True
                lblCourse.Visible = False
                ddlCourseList.Visible = False
                aceStudent.ContextKey = CStr(User1.CompanyID)
                lblExemptionsNotFound.Text = String.Empty
                ReLoadGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Handles Back button click
        ''' </summary>
        Protected Sub cmdBack_Click(sender As Object, e As System.EventArgs) Handles cmdBack.Click
            Try
                Response.Redirect(StudentResultUrl)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub txtStudent_TextChanged(sender As Object, e As System.EventArgs) Handles txtStudent.TextChanged
            Try
                Dim strID() As String
                strID = (txtStudent.Text).Split(CChar("|"))
                txtStudent.Text = strID(0)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' ReLoad Grid with No Records Message
        ''' </summary>
        Private Sub ReLoadGrid()
            Try
                Dim dtAssignment As New DataTable
                dtAssignment.Columns.Add("StudentID")
                gvAssignmentDetails.DataSource = dtAssignment
                gvAssignmentDetails.Rebind()
                gvAssignmentDetails.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Invisible Column and Disable Filter based on condition
        ''' </summary>
        Private Sub InvisibleColumn()
            Try
                If rdoStudent.Checked Then
                    If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                        TryCast(gvAssignmentDetails.MasterTableView.GetColumn("FAEPublishedScore"), GridBoundColumn).Display = True
                        TryCast(gvAssignmentDetails.MasterTableView.GetColumn("PublishedScore"), GridBoundColumn).Display = False
                    Else
                        TryCast(gvAssignmentDetails.MasterTableView.GetColumn("PublishedScore"), GridBoundColumn).Display = True
                        TryCast(gvAssignmentDetails.MasterTableView.GetColumn("FAEPublishedScore"), GridBoundColumn).Display = False
                    End If

                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("AcademicCycle"), GridBoundColumn).Display = True
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("StudentID"), GridBoundColumn).AllowFiltering = False
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("LastName"), GridBoundColumn).AllowFiltering = False
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("FirstName"), GridBoundColumn).AllowFiltering = False
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("AcademicCycle"), GridBoundColumn).AllowFiltering = False
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("CurrentStage"), GridBoundColumn).AllowFiltering = False
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("Course"), GridBoundColumn).AllowFiltering = True
                Else
                    If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                        TryCast(gvAssignmentDetails.MasterTableView.GetColumn("PublishedScore"), GridBoundColumn).Display = False
                        TryCast(gvAssignmentDetails.MasterTableView.GetColumn("FAEPublishedScore"), GridBoundColumn).Display = True

                    Else
                        TryCast(gvAssignmentDetails.MasterTableView.GetColumn("FAEPublishedScore"), GridBoundColumn).Display = False
                        TryCast(gvAssignmentDetails.MasterTableView.GetColumn("PublishedScore"), GridBoundColumn).Display = True
                    End If

                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("AcademicCycle"), GridBoundColumn).Display = False
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("StudentID"), GridBoundColumn).AllowFiltering = True
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("LastName"), GridBoundColumn).AllowFiltering = True
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("FirstName"), GridBoundColumn).AllowFiltering = True
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("CurrentStage"), GridBoundColumn).AllowFiltering = True
                    TryCast(gvAssignmentDetails.MasterTableView.GetColumn("Course"), GridBoundColumn).AllowFiltering = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace
