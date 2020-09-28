''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Kavita Zinage        08/11/2017                      Class Attendance for Lecturer
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
    Partial Class ClassAttendance__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "ClassAttendance__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL As String = "StudentCenter"
        Protected Const ATTRIBUTE_SECURITYERROR_PAGE As String = "securityErrorPage"

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

        Public Overridable Property StudentCenter() As String
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
           
            If String.IsNullOrEmpty(StudentCenter) Then
                StudentCenter = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL)
            End If
        End Sub
        Public Overridable ReadOnly Property SecurityErrorPage() As String
            Get
                If Not ViewState(ATTRIBUTE_SECURITYERROR_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_SECURITYERROR_PAGE))
                Else
                    Dim value As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("SecurityErrorPageURL"))
                    If Not String.IsNullOrEmpty(value) Then
                        ViewState(ATTRIBUTE_SECURITYERROR_PAGE) = value
                        Return value
                    Else
                        Return String.Empty
                    End If
                End If
            End Get
        End Property
#End Region

#Region "Page Events"
        ''' <summary>
        ''' Handles page load event
        ''' </summary>
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()
                lblError.Text = ""
                If User1.PersonID <= 0 Then
                    Application("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                Else
                    If Not IsPostBack Then
                        ' new boolean function person to write sp
                        ' Added by Deepika - on 11/10/2017 #18401
                        If CheckForLecturer() = True Then
                            LoadCourses()
                            If ddlType.SelectedValue = "Select Type" AndAlso (ddlStudGroup.SelectedValue = "Select Group" Or ddlStudGroup.SelectedValue = "") AndAlso Convert.ToString(ViewState("ClassPartID")) = "" Then
                                gvStudDetails.DataSource = Nothing
                                gvStudDetails.DataBind()
                            End If
                        Else
                            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("virtualdir") & SecurityErrorPage & "?Message=Access to this Person is unauthorized.")

                        End If
                       

                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Checks for lecturer. Added by Deepika for 18401
        ''' </summary>
        ''' <returns></returns>
        Public Function CheckForLecturer() As Boolean
            Try
                Dim sSQL As String = "..spCheckAccessForClassAttendancePage__c @PersonID=" & User1.PersonID
                Dim lIsLecturer As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSQL))
                Return lIsLecturer
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return Nothing
            End Try
        End Function
#End Region

#Region "Methods"
        ''' <summary>
        ''' Load drop down
        ''' </summary>
        Private Sub LoadCourses()
            Try
                Dim iCurriculumID As Integer = 0
               
                Dim sSQL As String = String.Empty, params(0) As IDataParameter
                sSQL = Me.AptifyApplication.GetEntityBaseDatabase("Persons") & "..spGetCourseListbyCurriculumID__c"
                params(0) = DataAction.GetDataParameter("@CurriculumID", SqlDbType.Int, iCurriculumID)

                Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                ddlCourseList.DataSource = dt
                ddlCourseList.DataTextField = "CourseName"
                ddlCourseList.DataValueField = "CourseID"
                ddlCourseList.DataBind()
                ddlCourseList.Items.Insert(0, "Select Course")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Load drop down Student Group
        ''' </summary>
        Private Sub LoadStudGroups()
            Try
                Dim sSQL As String = String.Empty, params(2) As IDataParameter
                sSQL = Me.AptifyApplication.GetEntityBaseDatabase("Persons") & "..spGetStudentGroups__c"
                params(0) = DataAction.GetDataParameter("@Date", SqlDbType.Date, Convert.ToDateTime(radClassDate.SelectedDate))
                params(1) = DataAction.GetDataParameter("@Type", SqlDbType.NVarChar, Convert.ToString(ddlType.SelectedValue))
                params(2) = DataAction.GetDataParameter("@CourseID", SqlDbType.Int, Convert.ToInt32(ddlCourseList.SelectedValue))
                Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                ddlStudGroup.DataSource = dt
                ddlStudGroup.DataTextField = "GroupID_Name"
                ddlStudGroup.DataValueField = "GroupID"
                ddlStudGroup.DataBind()
                ddlStudGroup.Items.Insert(0, "Select Group")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Load drop down Lesson
        ''' </summary>
        ''' 
        Private Sub LoadLesson()
            Try
                Dim sSQL As String = String.Empty, params(3) As IDataParameter
                sSQL = Me.AptifyApplication.GetEntityBaseDatabase("Persons") & "..spGetLessons__c"
                params(0) = DataAction.GetDataParameter("@Date", SqlDbType.Date, Convert.ToDateTime(radClassDate.SelectedDate))
                params(1) = DataAction.GetDataParameter("@Type", SqlDbType.NVarChar, Convert.ToString(ddlType.SelectedValue))
                params(2) = DataAction.GetDataParameter("@CourseID", SqlDbType.Int, Convert.ToInt32(ddlCourseList.SelectedValue))
                params(3) = DataAction.GetDataParameter("@GroupID", SqlDbType.Int, Convert.ToInt32(ddlStudGroup.SelectedValue))
                Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                ddlLesson.DataSource = dt
                ddlLesson.DataTextField = "Lesson"
                ddlLesson.DataValueField = "LessonID"
                ddlLesson.DataBind()
                ddlLesson.Items.Insert(0, "Select Lesson")

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                 
                    ViewState("ClassID") = dt.Rows(0)("ClassID")
                    ViewState("ClassPartID") = dt.Rows(0)("ClassPartID")
                    ViewState("ClassTitle") = dt.Rows(0)("ClassTitle")

                Else
                    ViewState("ClassID") = Nothing
                    ViewState("ClassPartID") = Nothing
                    ViewState("ClassTitle") = Nothing

                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Public Function LoadStudGrid() As DataTable
            Try
                
                Dim sSQLNew As String = String.Empty, oParams(7) As IDataParameter
                sSQLNew = Me.AptifyApplication.GetEntityBaseDatabase("Persons") & "..spGetStudDetforClassAttendance__c"
                oParams(0) = DataAction.GetDataParameter("@Type", SqlDbType.NVarChar, Convert.ToString(ddlType.SelectedValue))
                oParams(1) = DataAction.GetDataParameter("@CoursePartID", SqlDbType.Int, Convert.ToInt32(ViewState("CoursePartID")))
                oParams(2) = DataAction.GetDataParameter("@GroupID", SqlDbType.Int, Convert.ToInt32(ddlStudGroup.SelectedValue))
                oParams(3) = DataAction.GetDataParameter("@ClassID", SqlDbType.Int, Convert.ToInt32(ViewState("ClassID")))
                oParams(4) = DataAction.GetDataParameter("@SubGrpID1", SqlDbType.Int, Convert.ToInt32(ViewState("SubGroupID1")))
                oParams(5) = DataAction.GetDataParameter("@SubGrpID2", SqlDbType.Int, Convert.ToInt32(ViewState("SubGroupID2")))
                oParams(6) = DataAction.GetDataParameter("@SubGrpID3", SqlDbType.Int, Convert.ToInt32(ViewState("SubGroupID3")))
                oParams(7) = DataAction.GetDataParameter("@SubGrpID4", SqlDbType.Int, Convert.ToInt32(ViewState("SubGroupID4")))
                Dim dtStudent As DataTable = DataAction.GetDataTableParametrized(sSQLNew, CommandType.StoredProcedure, oParams)

                Return dtStudent
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return Nothing
            End Try
        End Function

#End Region

        Protected Sub radClassDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles radClassDate.SelectedDateChanged
            Try
                Dim sSQL As String = String.Empty, params(0) As IDataParameter
                sSQL = Me.AptifyApplication.GetEntityBaseDatabase("Persons") & "..spValidateClassDateforAcademicCycle__c"
                params(0) = DataAction.GetDataParameter("@Date", SqlDbType.Date, Convert.ToDateTime(radClassDate.SelectedDate))
                Dim iValid As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, params))
                If iValid = 1 Then
                    lblError.Text = ""
                Else
                    lblError.Text = Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.ClassAttendance.DateValidationMsg__c")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
                If Convert.ToString(ddlType.SelectedValue) <> "Select Type" AndAlso Convert.ToString(radClassDate.SelectedDate) <> "" AndAlso Convert.ToString(ddlCourseList.SelectedValue) <> "Select Course" Then
                    LoadStudGroups()
                    'ddlLesson.Items.Insert(0, "Select Lesson")
                    'ElseIf Convert.ToString(ddlType.SelectedValue) <> "Select Type" AndAlso Convert.ToString(radClassDate.SelectedDate) <> "" AndAlso Convert.ToString(ddlCourseList.SelectedValue) <> "Select Course" AndAlso (Convert.ToString(ddlStudGroup.SelectedValue) = "Select Group" Or Convert.ToString(ddlStudGroup.SelectedValue) <> "Select Group" Or Convert.ToString(ddlStudGroup.SelectedValue) <> "") Then
                    'LoadLesson()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
            Try
                LoadStudGroups()
                'If Convert.ToString(ddlType.SelectedValue) <> "Select Type" AndAlso Convert.ToString(radClassDate.SelectedDate) <> "" AndAlso Convert.ToString(ddlCourseList.SelectedValue) <> "Select Course" AndAlso (Convert.ToString(ddlStudGroup.SelectedValue) = "Select Group" Or Convert.ToString(ddlStudGroup.SelectedValue) <> "Select Group" Or Convert.ToString(ddlStudGroup.SelectedValue) <> "") Then
                '    LoadLesson()
                'End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub ddlCourseList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseList.SelectedIndexChanged
            Try
                
                If Convert.ToString(ddlType.SelectedValue) <> "Select Type" AndAlso Convert.ToString(radClassDate.SelectedDate) <> "" AndAlso Convert.ToString(ddlCourseList.SelectedValue) <> "Select Course" Then
                    LoadStudGroups()
                    'ddlLesson.SelectedValue = "0"
                    'ddlLesson.Items.Insert(0, "Select Lesson")
                    'ElseIf Convert.ToString(ddlType.SelectedValue) <> "Select Type" AndAlso Convert.ToString(radClassDate.SelectedDate) <> "" AndAlso Convert.ToString(ddlCourseList.SelectedValue) <> "Select Course" AndAlso (Convert.ToString(ddlStudGroup.SelectedValue) = "Select Group" Or Convert.ToString(ddlStudGroup.SelectedValue) <> "Select Group" Or Convert.ToString(ddlStudGroup.SelectedValue) <> "") Then
                    'LoadLesson()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub ddlStudGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStudGroup.SelectedIndexChanged
            Try
                If Convert.ToString(ddlStudGroup.SelectedValue) = "Select Group" Then
                    ddlStudGroup.SelectedValue = "0"
                End If
                LoadLesson()

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnDisplay_Click(sender As Object, e As EventArgs) Handles btnDisplay.Click
            Try
                If ddlType.SelectedValue = "Select Type" Or Convert.ToString(radClassDate.SelectedDate) = "" Or ddlCourseList.SelectedValue = "Select Course" Or (ddlStudGroup.SelectedValue = "Select Group" Or ddlStudGroup.SelectedValue = "") Or ddlLesson.SelectedValue = "Select Lesson" Then
                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ClassAttendancePage.RequiredFieldMsg")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                Else
                    pnlDetails.Visible = True
                    gvStudDetails.DataSource = LoadStudGrid()
                    gvStudDetails.DataBind()

                    'Begin#18403Added by Deepika Gaikwad on 26/10/2017 to visible the exam number column only if the type is exam otherwise invisible it .
                    If (ddlType.SelectedItem.Text = "Exam") Then
                        gvStudDetails.Columns(2).Visible = True
                    Else
                        gvStudDetails.Columns(2).Visible = False
                    End If
                    'End#18403 

                    Dim sClassName As String = Convert.ToString(ViewState("ClassTitle"))
                    Dim sMsg As StringBuilder = New System.Text.StringBuilder()
                    sMsg = sMsg.AppendFormat(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                                            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.ClassAttendance.DisplayMsg__c")), _
                                            Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), sClassName)
                    lblAttdMsg.Text = Convert.ToString(sMsg)
                End If
                
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                Me.Response.Redirect(StudentCenter, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub cmdSubmit_Click(sender As Object, e As EventArgs) Handles cmdSubmit.Click
            Try

                lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ClassAttendancePage.ConfirmationMsg")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radConfirmation.VisibleOnPageLoad = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Protected Sub gvstuddetails_needdatasource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gvStudDetails.NeedDataSource
        '    Try
        '        gvstuddetails.datasource = loadstudgrid()
        '    Catch ex As exception
        '        aptify.framework.exceptionmanagement.exceptionmanager.publish(ex)
        '    End Try
        'End Sub

        Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
            Try
                For Each item As GridDataItem In gvStudDetails.MasterTableView.Items
                    If Convert.ToString(item.GetDataKeyValue("ClassRegPartStatusID")) = Convert.ToString(lblClassRegPartID.Text) Then
                        Dim Hfinstcomment As HiddenField = DirectCast(item.FindControl("Hfinstcomment"), HiddenField)
                        Hfinstcomment.Value = Convert.ToString(txtComment.Text)
                    End If
                Next
                radWinInstructorComment.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Try
                radWinInstructorComment.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Protected Sub gvStudDetails_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gvStudDetails.ItemCommand
        '    If e.CommandName = "Edit" Then
        '        radWinInstructorComment.VisibleOnPageLoad = True
        '    End If
        'End Sub

        'Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        '    Dim lnk As LinkButton = DirectCast(sender, LinkButton)
        '    Dim item As GridDataItem = DirectCast(lnk.NamingContainer, GridDataItem)
        '    Dim id As String = "1"
        '    Dim city As String = "2"
        '    win1.VisibleOnPageLoad = True
        '    Label1.Text = id
        '    Label2.Text = city
        'End Sub

        ''' <summary>
        ''' Handles Edit click
        ''' </summary>
        Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim lnkEdit As LinkButton = CType(sender, LinkButton)
                Dim item As GridDataItem = DirectCast(lnkEdit.NamingContainer, GridDataItem)
                Dim hfPartStatusID As HiddenField = DirectCast(lnkEdit.NamingContainer.FindControl("hfPartStatusID"), HiddenField)

                lblClassRegPartID.Text = Convert.ToString(hfPartStatusID.Value)

                Dim Hfinstcomment As HiddenField = DirectCast(lnkEdit.NamingContainer.FindControl("Hfinstcomment"), HiddenField)
                txtComment.Text = Convert.ToString(Hfinstcomment.Value)

                radWinInstructorComment.VisibleOnPageLoad = True

                

                'Catch taex As System.Threading.ThreadAbortException
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' To Select all check box
        ''' </summary>
        Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim headerCheckBox As CheckBox = CType(sender, CheckBox)
                For Each dataItem As GridDataItem In gvStudDetails.MasterTableView.Items
                    CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = headerCheckBox.Checked
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
            Try
                radConfirmation.VisibleOnPageLoad = False
                If updateAttendance() Then
                    lblError.Text = ""
                    lblSuccMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ClassAttendancePage.SuccessMsg")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radSuccMsg.VisibleOnPageLoad = True
                Else
                    lblError.Text = "Failed to update attendance"
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
            Try
                radConfirmation.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Updates the attendance.
        ''' </summary>
        ''' <returns></returns>
        Public Function updateAttendance() As Boolean
            Try
                Dim sSQLUpdate As String = String.Empty, params(2) As IDataParameter
                sSQLUpdate = Me.AptifyApplication.GetEntityBaseDatabase("Persons") & "..spUpdateClassRegPartStatusforclassAttd__c"
                Dim sStatus As String = String.Empty
                For Each item As GridDataItem In gvStudDetails.MasterTableView.Items
                    Dim chkSelect As CheckBox = DirectCast(item.FindControl("chkSelect"), CheckBox)
                    If chkSelect.Checked Then
                        sStatus = "Attended"
                    Else
                        sStatus = "Absent"
                    End If
                    Dim hfPartStatusID As HiddenField = DirectCast(item.FindControl("hfPartStatusID"), HiddenField)
                    Dim Hfinstcomment As HiddenField = DirectCast(item.FindControl("Hfinstcomment"), HiddenField)

                    params(0) = DataAction.GetDataParameter("@Status", SqlDbType.NVarChar, Convert.ToString(sStatus))
                    params(1) = DataAction.GetDataParameter("@InstructorComment", SqlDbType.NVarChar, Convert.ToString(Hfinstcomment.Value))
                    params(2) = DataAction.GetDataParameter("@ClassRegPartID", SqlDbType.Int, Convert.ToInt32(hfPartStatusID.Value))

                    Dim insertApp As Integer = DataAction.ExecuteNonQueryParametrized(sSQLUpdate, CommandType.StoredProcedure, params)

                Next

                Return True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        Protected Sub btnSuccess_Click(sender As Object, e As EventArgs) Handles btnSuccess.Click
            Try
                radSuccMsg.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub ddlLesson_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLesson.SelectedIndexChanged
            Try
                Dim str As String
                Dim strArr() As String

                str = Convert.ToString(ddlLesson.SelectedValue)
                strArr = str.Split(CChar("-"))
                ViewState("CoursePartID") = Convert.ToString(strArr(0))
                Dim Count As Integer = Convert.ToInt32(strArr.Length)
                If Count = 2 Then
                    ViewState("SubGroupID1") = "0"
                    ViewState("SubGroupID2") = "0"
                    ViewState("SubGroupID3") = "0"
                    ViewState("SubGroupID4") = "0"
                ElseIf Count = 3 Then
                    ViewState("SubGroupID1") = Convert.ToString(strArr(2))
                    ViewState("SubGroupID2") = "0"
                    ViewState("SubGroupID3") = "0"
                    ViewState("SubGroupID4") = "0"
                ElseIf Count = 4 Then
                    ViewState("SubGroupID1") = Convert.ToString(strArr(2))
                    ViewState("SubGroupID2") = Convert.ToString(strArr(3))
                    ViewState("SubGroupID3") = "0"
                    ViewState("SubGroupID4") = "0"
                ElseIf Count = 5 Then
                    ViewState("SubGroupID1") = Convert.ToString(strArr(2))
                    ViewState("SubGroupID2") = Convert.ToString(strArr(3))
                    ViewState("SubGroupID3") = Convert.ToString(strArr(4))
                    ViewState("SubGroupID4") = "0"
                ElseIf Count = 6 Then
                    ViewState("SubGroupID1") = Convert.ToString(strArr(2))
                    ViewState("SubGroupID2") = Convert.ToString(strArr(3))
                    ViewState("SubGroupID3") = Convert.ToString(strArr(4))
                    ViewState("SubGroupID4") = Convert.ToString(strArr(5))
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
		Protected Sub gvStudDetails_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvStudDetails.NeedDataSource
            Try
                gvStudDetails.DataSource = LoadStudGrid()
                gvStudDetails.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace
