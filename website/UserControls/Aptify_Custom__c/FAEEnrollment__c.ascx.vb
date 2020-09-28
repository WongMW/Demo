''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Pradip Chavhan        11/09/2016                      FAE Enrollment Page
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Strict On
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI
Imports Aptify.Framework.Application


Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class FAEEnrollment
        Inherits BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE As String = "LoginPage"
        Protected Const ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL As String = "FirmCourseEnrollmentURL__c"
        Dim _studentGroupListNew As New DataTable()
        Dim _FAEElective As New DataTable()

#Region "Property Setting"
        Public Overridable Property LoginPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property CourseEnrollmentURL() As String
            Get
                If Not ViewState(ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE)
            End If

            If String.IsNullOrEmpty(Me.CourseEnrollmentURL) Then
                Me.CourseEnrollmentURL = Me.GetLinkValueFromXML(ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL)
            End If

        End Sub

#End Region
#Region "Page Events"
        ''' <summary>
        ''' Handles page load event
        ''' </summary>
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()


                If Request.QueryString("PageName") IsNot Nothing Then
                    btnBack.Visible = True
                End If

                If Not IsPostBack Then
                    LoadFAEElectiveCourses()
                    FillTimeTable()
                    LoadGrid()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
#Region "Private Functions"
        ''' <summary>
        ''' Load Student Grid
        ''' </summary>
        Public Sub LoadGrid()
            Try
                If Session("FAEStudent") IsNot Nothing Then
                    Dim dt As DataTable = CType(Session("FAEStudent"), DataTable) ' DataAction.GetDataTable(sSql, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        Me.grdFAEEnrollment.DataSource = dt
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Private Sub LoadFAEElectiveCourses()
            Dim sSql As String = Database & "..spGetFAEElectiveCourses__c @AcademicCycleID=" & Convert.ToInt32(Request.QueryString("AcademicYear")) ' Code updated GM for Redmine #19432
            Dim dtFAE As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtFAE Is Nothing AndAlso dtFAE.Rows.Count > 0 Then
                ViewState("FAEElective") = dtFAE
            Else
                ViewState("FAEElective") = Nothing
            End If

        End Sub

        Private Sub FillTimeTable()
            Dim sSql As String = Database & "..spGetTimeTableByCurriculumandAcademicYear__c"
            Dim param(2) As IDataParameter
            param(0) = DataAction.GetDataParameter("@AcademicCycleID", SqlDbType.Int, CInt(Request.QueryString("AcademicYear")))
            param(1) = DataAction.GetDataParameter("@CurriculumID", SqlDbType.Int, 5)
            param(2) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, User1.CompanyID)
            Dim dt As DataTable = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ViewState("TimeTable") = dt
            Else
                ViewState("TimeTable") = dt
            End If
        End Sub
        Function validateBeforeSave() As Boolean
            Dim CheckCount As Integer = 0
            lblError.Text = ""
            lblError.Visible = False
            Try
                For i As Integer = 0 To grdFAEEnrollment.Items.Count - 1
                    Dim cmbElectivegv As DropDownList = DirectCast(grdFAEEnrollment.Items(i).FindControl("cmbElective"), DropDownList)
                    Dim cmbTimetablegv As DropDownList = DirectCast(grdFAEEnrollment.Items(i).FindControl("cmbTimetable"), DropDownList)
                    If cmbElectivegv.SelectedIndex > 0 Or cmbTimetablegv.SelectedIndex > 0 Then
                        CheckCount = CheckCount + 1
                        Return True
                    End If
                Next
                If CheckCount = 0 Then
                    lblError.Text = "Please select FAE Elective or Time table to save"
                    lblError.Visible = True
                    Return False
                End If
                Return True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return True
        End Function

        Private Sub LoadStudentGroupsNew()
            Try
                Dim sql As New StringBuilder()
                sql.AppendFormat("{0} ..spGetStudentGroupFAEEnrollment__c @CompanyID={1},@AcademicCycleID={2}", Me.Database, User1.CompanyID.ToString(), Request.QueryString("AcademicYear"))
                _studentGroupListNew = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadFAEElectiveAssign()
            Try
                Dim sql As New StringBuilder()
                sql.AppendFormat("{0} ..spGetFAEElectiveEnrollment__c @CompanyID={1},@AcademicCycleID={2}", Me.Database, User1.CompanyID.ToString(), Request.QueryString("AcademicYear"))
                _FAEElective = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region


        Protected Sub grdFAEEnrollment_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFAEEnrollment.DataBound
            Try
                If _studentGroupListNew.Rows.Count = 0 Then
                    LoadStudentGroupsNew()
                End If
                If _FAEElective.Rows.Count = 0 Then
                    LoadFAEElectiveAssign()
                End If

                For Each item As Telerik.Web.UI.GridDataItem In grdFAEEnrollment.MasterTableView.Items

                    Dim cmbElective As DropDownList = DirectCast(item.FindControl("cmbElective"), DropDownList)
                    Dim cmbTimetable As DropDownList = DirectCast(item.FindControl("cmbTimetable"), DropDownList)
                    Dim hidStudentNo As HiddenField = DirectCast(item.FindControl("hidStudentNo"), HiddenField)
                    If Not ViewState("FAEElective") Is Nothing Then
                        cmbElective.ClearSelection()
                        cmbElective.DataSource = CType(ViewState("FAEElective"), DataTable)
                        cmbElective.DataTextField = "CourseName"
                        cmbElective.DataValueField = "CourseID"
                        cmbElective.DataBind()
                    End If
                    If Not ViewState("TimeTable") Is Nothing Then
                        cmbTimetable.ClearSelection()
                        cmbTimetable.DataSource = CType(ViewState("TimeTable"), DataTable)
                        cmbTimetable.DataTextField = "Name"
                        cmbTimetable.DataValueField = "ID"
                        cmbTimetable.DataBind()
                    End If
                    cmbTimetable.Items.Insert(0, New ListItem("Select", "0").ToString)
                    cmbElective.Items.Insert(0, New ListItem("Select", "0").ToString)

                    Dim filterRow() As DataRow = _studentGroupListNew.Select("StudentID='" & Convert.ToString(hidStudentNo.Value) & "' AND CurriculumName='FAE- Final Admitting Exam' ")
                    If filterRow.Count > 0 Then
                        cmbTimetable.SelectedValue = Convert.ToString(filterRow(0)("SGID"))
                    End If
                    Dim filterRow1() As DataRow = _FAEElective.Select("StudentID='" & Convert.ToString(hidStudentNo.Value) & "' ")
                    If filterRow1.Count > 0 Then
                        cmbElective.SelectedValue = Convert.ToString(filterRow1(0)("CourseID"))
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdFAEEnrollment_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdFAEEnrollment.NeedDataSource
            Try
                LoadGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Dim eventName As String = String.Empty
                eventName = "Back"
                Dim events As String = System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(eventName))
                Dim url As New StringBuilder()
                url.AppendFormat("{0}?AcademicYear={1}&EnrollMentType={2}&RouteOfEntry={3}&CurrentStage={4}&FilterBy={5}&SubjectFailed={6}&EventName={7}&Loc={8}", Me.CourseEnrollmentURL, Request.QueryString("AcademicYear").ToString(), Request.QueryString("EnrollMentType").ToString(), Request.QueryString("RouteOfEntry").ToString(), Request.QueryString("CurrentStage").ToString(), Request.QueryString("FilterBy").ToString(), Request.QueryString("SubjectFailed").ToString(), events, Request.QueryString("Loc").ToString())
                Response.Redirect(url.ToString())
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            Try                If validateBeforeSave() Then
                    Dim recordupdate As Integer = 0
                    Dim sSQL As String
                    For i As Integer = 0 To grdFAEEnrollment.Items.Count - 1
                        Dim hidStudentID As HiddenField = DirectCast(grdFAEEnrollment.Items(i).FindControl("hidStudentNo"), HiddenField)
                        Dim cmbElectivegv As DropDownList = DirectCast(grdFAEEnrollment.Items(i).FindControl("cmbElective"), DropDownList)
                        Dim cmbTimetablegv As DropDownList = DirectCast(grdFAEEnrollment.Items(i).FindControl("cmbTimetable"), DropDownList)

                        If cmbElectivegv.SelectedIndex > 0 Then
                            'Siddharth:  Added timetable id condition in below stored procedure for redmine log #18443
                            sSQL = String.Format("{0}..spUpdateFAEElectiveCourseSelection__c @StudentID={1}, @CourseID={2}, @CompanyId={3}, @CompanyAdminID={4}, @AcademicCycleID={5}, @TimeTableID={6}", Database, hidStudentID.Value, cmbElectivegv.SelectedValue, User1.CompanyID, User1.PersonID, Request.QueryString("AcademicYear").ToString(), cmbTimetablegv.SelectedValue)
                            recordupdate = recordupdate + DataAction.ExecuteNonQuery(sSQL, 180)
                        End If
                        If cmbTimetablegv.SelectedIndex > 0 Then
                            Dim param(4) As IDataParameter
                            param(0) = DataAction.GetDataParameter("@AcademicCyleId", SqlDbType.Int, Request.QueryString("AcademicYear").ToString())
                            param(1) = DataAction.GetDataParameter("@TimeTableId", SqlDbType.Int, CInt(cmbTimetablegv.SelectedValue))
                            param(2) = DataAction.GetDataParameter("@Status", SqlDbType.VarChar, "Pending")
                            param(3) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, CInt(User1.CompanyID))
                            param(4) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, CInt(hidStudentID.Value))
                            sSQL = ""
                            sSQL = Database & "..spUpdateFAETimeTable__c"
                            Dim recordupdate2 As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param, 180)
                        End If
                    Next
                    Session("FAEStudent") = Nothing
                    Dim url As New StringBuilder()
                    url.AppendFormat("{0}?AcademicYear={1}&EnrollMentType={2}&RouteOfEntry={3}&CurrentStage={4}&FilterBy={5}&SubjectFailed={6}&EventName={7}&Loc={8}", Me.CourseEnrollmentURL, Request.QueryString("AcademicYear").ToString(), Request.QueryString("EnrollMentType").ToString(), Request.QueryString("RouteOfEntry").ToString(), Request.QueryString("CurrentStage").ToString(), Request.QueryString("FilterBy").ToString(), Request.QueryString("SubjectFailed").ToString(), Events, Request.QueryString("Loc").ToString())
                    Response.Redirect(url.ToString())
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


    End Class
End Namespace


