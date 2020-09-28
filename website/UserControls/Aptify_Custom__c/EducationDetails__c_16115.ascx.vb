'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                07/29/2014                      Display Education details on web page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Imports Aptify.Applications.OrderEntry


Namespace Aptify.Framework.Web.eBusiness.Generated
    Partial Class EducationDetails__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Public RecordId As Integer
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "EducationDetails__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Protected Const ATTRIBUTE_COURSE_DETAILS_URL_NAME As String = "CourseDetailsURL"


        Public Overridable Property RedirectURLs() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property CourseDetailsURL() As String
            Get
                If Not ViewState(ATTRIBUTE_COURSE_DETAILS_URL_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_COURSE_DETAILS_URL_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_COURSE_DETAILS_URL_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            ''For Dynamic Script loading By Shiwendra
            Dim js As HtmlGenericControl = New HtmlGenericControl("script")
            js.Attributes.Add("type", "text/javascript")
            js.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-1.7.1.min.js"))
            Me.Page.Header.Controls.Add(js)
            Dim js1 As HtmlGenericControl = New HtmlGenericControl("script")
            js1.Attributes.Add("type", "text/javascript")
            js1.Attributes.Add("src", ResolveUrl("~/Scripts/expand.js"))
            Me.Page.Header.Controls.Add(js1)
            Dim css As HtmlGenericControl = New HtmlGenericControl("style")
            css.Attributes.Add("type", "text/css")
            css.Attributes.Add("src", ResolveUrl("~/CSS/StyleSheet.css"))
            Me.Page.Header.Controls.Add(css)
            Dim js2 As HtmlGenericControl = New HtmlGenericControl("script")
            js2.Attributes.Add("type", "text/javascript")
            js2.Attributes.Add("src", ResolveUrl("~/Scripts/jquery.min.js"))
            Me.Page.Header.Controls.Add(js2)
        End Sub
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            If Not IsPostBack Then
                SetProperties()
                'LoadYear()
                LoadHeaderText()
                DisplayNextAcademicCycle()
                LoadEnrolledCoursesGrid()
                LoadEnrolledMockExamGrid()
                'LoadRagisteredExamsGrid()
                LoadRagisteredInterimAssessmentsGrid()
                LoadRagisteredResitAssessmentsGrid()
                DisplayLocationOnIntrim()

                LoadCurriculumDetails()
            End If
        End Sub

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(Me.RedirectURLs) Then
                Me.RedirectURLs = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(Me.CourseDetailsURL) Then
                Me.CourseDetailsURL = Me.GetLinkValueFromXML(ATTRIBUTE_COURSE_DETAILS_URL_NAME)
            End If
        End Sub

        Private Sub LoadHeaderText()
            Try
                lblFirstLast.Text = User1.FirstName + " " + User1.LastName
                lblStudentNumber.Text = CStr(User1.PersonID)
                'Dim sSql As String = "..spGetAcademicCycleYearAsPerCurrentDate__c @Year=" & Today.Year
                Dim sSql As String = "..spGetCurrentAcademicYear__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ' lblAcademicCycle.Text = Convert.ToString(dt.Rows(0)("CalcName"))
                    rdoCurrentAcademicCycle.Text = "Current Academic Cycle: " + Convert.ToString(dt.Rows(0)("Name"))
                    ViewState("AcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))
                End If
                'lblAcademicCycle.Text = Convert.ToString(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub DisplayNextAcademicCycle()
            Try
                Dim sSql As String = "..spGetNextAcademicCycleDetails__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rdoNextAcadmicCycle.Text = "Next Academic Cycle: " + Convert.ToString(dt.Rows(0)("Name"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadNextAcademicCycle()
            Try
                Dim sSql As String = "..spGetNextAcademicCycleDetails__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rdoNextAcadmicCycle.Text = "Next Academic Cycle: " + Convert.ToString(dt.Rows(0)("Name"))
                    ViewState("AcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub LoadEnrolledCoursesGrid()
            Try
                Dim sSql As String = Database & "..spGetEnrolledCourses__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & ViewState("AcademicCycleID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    'grdEnrolledCourses.DataSource = dt
                    'grdEnrolledCourses.DataBind()
                    'grdEnrolledCourses.Visible = True
                    rpEnrolledCourses.DataSource = dt
                    rpEnrolledCourses.DataBind()
                    rpEnrolledCourses.Visible = True
                    lblErrorEnrolledCourse.Text = ""
                Else
                    grdEnrolledCourses.Visible = False
                    rpEnrolledCourses.Visible = False
                    lblErrorEnrolledCourse.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.ErrorMsgEnrolledCourses")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub LoadEnrolledMockExamGrid()
            Try
                Dim sSql As String = Database & "..spGetEnrolledMockExam__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & ViewState("AcademicCycleID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rpMockExam.DataSource = dt
                    rpMockExam.DataBind()
                    rpMockExam.Visible = True
                    'grdMockExam.DataSource = dt
                    'grdMockExam.DataBind()
                    'grdMockExam.Visible = True
                    lblErrorMockExam.Text = ""
                Else
                    grdMockExam.Visible = False
                    rpMockExam.Visible = False
                    lblErrorMockExam.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.ErrorMsgEnrolledMockExam")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
       
        Protected Overridable Sub LoadRagisteredInterimAssessmentsGrid()
            Try
                Dim sSql As String = Database & "..spGetRagisteredExams__c @PersonID=" & User1.PersonID & ",@Type='IntrimAssessments'" & ",@AcademicCycleID=" & ViewState("AcademicCycleID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    'grdRegisteredInterimAssessments.DataSource = dt
                    'grdRegisteredInterimAssessments.DataBind()
                    'grdRegisteredInterimAssessments.Visible = True
                    rpIntrimAssessment.DataSource = dt
                    rpIntrimAssessment.DataBind()
                    rpIntrimAssessment.Visible = True
                    lblRagisteredInterimAssessments.Text = ""
                Else
                    rpIntrimAssessment.Visible = False
                    grdRegisteredInterimAssessments.Visible = False
                    lblRagisteredInterimAssessments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.ErrorMsgContinuousAssessment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub LoadRagisteredResitAssessmentsGrid()
            Try
                Dim sSql As String = Database & "..spGetResitExam__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & ViewState("AcademicCycleID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    'grdResitAssessment.DataSource = dt
                    'grdResitAssessment.DataBind()
                    'grdResitAssessment.Visible = True
                    rpResitAssessment.DataSource = dt
                    rpResitAssessment.DataBind()
                    'Dim txtDisplayLinkField As TextBox
                    'Dim lnkInterimUpdateGroup As LinkButton
                    'Dim lblSessionName As Label
                    'For Each row As Telerik.Web.UI.GridItem In grdResitAssessment.Items
                    '    txtDisplayLinkField = DirectCast(row.FindControl("txtDisplayLinkField"), TextBox)
                    '    lnkInterimUpdateGroup = DirectCast(row.FindControl("lnkInterimUpdateGroup"), LinkButton)
                    '    lblSessionName = DirectCast(row.FindControl("lblSessionName"), Label)
                    '    If txtDisplayLinkField.Text.Trim = 0 Then
                    '        lnkInterimUpdateGroup.Visible = False
                    '    Else

                    '            lnkInterimUpdateGroup.Visible = True
                    '    End If
                    'Next
                    rpResitAssessment.Visible = True
                    lblErrorResit.Text = ""
                Else
                    rpResitAssessment.Visible = False
                    ' grdResitAssessment.Visible = False
                    lblErrorResit.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.ErrorMsgResitAssessment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub DisplayLocationOnIntrim()
            Try
                Dim txtDisplayLinkField As TextBox
                Dim lnkInterimUpdateGroup As LinkButton
                Dim lblSessionName As Label
                For Each row As Telerik.Web.UI.GridItem In grdRegisteredInterimAssessments.Items
                    txtDisplayLinkField = DirectCast(row.FindControl("txtDisplayLinkField"), TextBox)
                    lnkInterimUpdateGroup = DirectCast(row.FindControl("lnkInterimUpdateGroup"), LinkButton)
                    lblSessionName = DirectCast(row.FindControl("lblSessionName"), Label)
                    If txtDisplayLinkField.Text.Trim = 0 Then
                        lnkInterimUpdateGroup.Visible = False
                    Else
                        Dim sSqlIsAutumn As String = "..spCheckIsAutumnSession__c @SessionName='" & lblSessionName.Text & "'"
                        Dim lIsAutumn As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlIsAutumn, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If lIsAutumn > 0 Then
                            lnkInterimUpdateGroup.Visible = False
                        Else
                            lnkInterimUpdateGroup.Visible = True
                        End If


                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        
        'Protected Sub grdEnrolledCourses_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdEnrolledCourses.NeedDataSource
        '    LoadEnrolledCoursesGrid()
        'End Sub

        'Protected Sub grdRagisteredExams_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdRegisteredExams.ItemCommand
        '    Try
        '        If e.CommandName = "ChangeGroupByCurriculum" Then
        '            lblExamMsg.Text = ""
        '            radExamWindow.VisibleOnPageLoad = True
        '            'LoadGroupDetails(e.CommandArgument)
        '            Dim sSql As String = Database & "..spGetCurriculumIDFromName__c @Name='" & e.CommandArgument & "'"
        '            Dim CurriculumID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
        '            Dim lblSessionID As Label = TryCast(e.Item.FindControl("lblSessionID"), Label)

        '            LoadGroupDetailsByCurriculum(1, lblSessionID.Text)
        '        ElseIf e.CommandName = "ChangeGroupBySession" Then
        '            Dim sSql As String = Database & "..spGetCurriculumIDFromName__c @Name='" & e.CommandArgument & "'"
        '            Dim CurriculumID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
        '            ' Dim lblSessionID As Label = TryCast(e.Item.FindControl("lblSessionID"), Label)
        '            radExamChangeSession.VisibleOnPageLoad = True
        '            LoadAutumnSession(CurriculumID)
        '        End If

        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub
        'Protected Sub grdRagisteredExams_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdRegisteredExams.NeedDataSource
        '    LoadRagisteredExamsGrid()
        'End Sub
        'Protected Sub grdRagisteredInterimAssessments_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdRegisteredInterimAssessments.NeedDataSource
        '    LoadRagisteredInterimAssessmentsGrid()
        '    DisplayLocationOnIntrim()
        'End Sub
        Private Sub LoadGroupDetails(ByVal StudentGrpID As Integer, ByVal ClassID As Integer)
            Try
                ' Dim sSql As String = Database & "..spGetGroupAsPerClass__c @ClassID=" & ClassID
                Dim sSql As String = Database & "..spGetStudentGroupLocation__c @SubGroupID=" & StudentGrpID & ",@ClassID=" & ClassID
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    cmbGroupNames.DataSource = dt
                    cmbGroupNames.DataTextField = "Name"
                    cmbGroupNames.DataValueField = "ID"
                    cmbGroupNames.DataBind()
                End If
                cmbGroupNames.Items.Insert(0, "--------")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadGroupDetailsByCurriculum(ByVal SessionID As Integer, ByVal CurriculumID As Integer)
            Try

                Dim sClassIDs As String = String.Empty
                Dim lblClassID As Label
                For Each row As RepeaterItem In rptEnrolledExams.Items
                    Dim SessionName As String = AptifyApplication.GetEntityRecordName("ExamSessions__c", SessionID)
                    Dim ExamRepeter As Repeater
                    If SessionName.ToLower.Contains("summer") Then
                        ExamRepeter = DirectCast(row.FindControl("rptSummerSession"), Repeater)
                    Else
                        ExamRepeter = DirectCast(row.FindControl("rptAutumnSession"), Repeater)
                    End If

                    For Each Autumn As RepeaterItem In ExamRepeter.Items
                        lblClassID = DirectCast(Autumn.FindControl("lblClassID"), Label)
                        If sClassIDs = "" Then
                            sClassIDs = lblClassID.Text
                        Else
                            sClassIDs = sClassIDs + "," + lblClassID.Text
                        End If
                    Next
                Next


                'Dim sSql As String = Database & "..spGetExamLocationAsPerGroupAndSession__c @CurriculumID=" & CurriculumID & ",@ExamSessionID=" & SessionID & _
                '                      ",@PersonID=" & User1.PersonID
                Dim sSql As String = Database & "..spGetExamLocations__c @ClassID='" & sClassIDs & "'"
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    drpExamLocation.DataSource = dt
                    drpExamLocation.DataTextField = "Name"
                    drpExamLocation.DataValueField = "ID"
                    drpExamLocation.DataBind()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadAutumnSession(ByVal CurriculumID As Integer)
            Try
                Dim sSql As String = Database & "..spGetRegisterExamChangeSession__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@CurriculumID=" & CurriculumID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    RadChangeExamSession.DataSource = dt
                    RadChangeExamSession.DataBind()
                    RadChangeExamSession.Visible = True
                Else
                    RadChangeExamSession.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Public Property CurrentChangeSessionTable() As DataTable
        '    Get

        '        If Not Session("CurrentChangeSessionTable") Is Nothing Then
        '            Return CType(Session("CurrentChangeSessionTable"), DataTable)
        '        Else
        '            Dim dtCurrentChangeSessionTable As New DataTable
        '            dtCurrentChangeSessionTable.Columns.Add("RowID")
        '            dtCurrentChangeSessionTable.Columns.Add("OrderID")

        '            Return dtCurrentChangeSessionTable
        '        End If
        '    End Get
        '    Set(ByVal value As DataTable)
        '        Session("CurrentChangeSessionTable") = value
        '    End Set
        'End Property
        Public Sub chkSelect_CheckedChanged(sender As Object, e As EventArgs)
            Try
                Dim chkExamChecked As CheckBox
                Dim lblAcademickCycle As Label
                Dim lblCourseID As Label
                Dim slblAutumnClassID As String = String.Empty
                Dim sUnCheckCourseID As String = String.Empty
                Dim lblOrderID As Label
                Dim lblAutumnClassID As Label
                For Each row As Telerik.Web.UI.GridItem In RadChangeExamSession.Items

                    chkExamChecked = DirectCast(row.FindControl("chkExamChecked"), CheckBox)
                    lblAcademickCycle = DirectCast(row.FindControl("lblAcademickCycle"), Label)
                    lblCourseID = DirectCast(row.FindControl("lblCourseID"), Label)
                    lblOrderID = DirectCast(row.FindControl("lblOrderID"), Label)
                    lblAutumnClassID = DirectCast(row.FindControl("lblAutumnClassID"), Label)
                    If chkExamChecked.Checked Then
                        If slblAutumnClassID = "" Then
                            slblAutumnClassID = lblAutumnClassID.Text
                        Else
                            slblAutumnClassID = slblAutumnClassID & "," & lblAutumnClassID.Text
                        End If
                        ''Else
                        ''    If sUnCheckCourseID = "" Then
                        ''        sUnCheckCourseID = lblCourseID.Text
                        ''    Else
                        ''        sUnCheckCourseID = sUnCheckCourseID & "," & lblCourseID.Text
                        ''    End If
                        ''    '  dtCurrentChangeSessionTable.Rows.RemoveAt(lblRowIndex.Text)
                    End If

                Next

                If slblAutumnClassID <> "" Then
                    ' Dim sSql As String = Database & "..spGetExamSessionLocations__c @AcademicCycleID=" & lblAcademickCycle.Text & ",@INCounseID='" & sCourseID & "',@OutCourseID='" & sUnCheckCourseID & "'"
                    Dim sSql As String = Database & "..spGetExamLocations__c @ClassID='" & slblAutumnClassID & "'"
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        drpAutumnSession.DataSource = dt
                        drpAutumnSession.DataTextField = "Name"
                        drpAutumnSession.DataValueField = "ID"
                        drpAutumnSession.DataBind()
                        drpAutumnSession.Items.Insert(0, "--------")
                    Else
                        drpAutumnSession.Items.Clear()
                        drpAutumnSession.DataSource = Nothing
                        drpAutumnSession.DataBind()
                        drpAutumnSession.Items.Insert(0, "--------")
                    End If
                Else
                    drpAutumnSession.Items.Clear()
                    drpAutumnSession.DataSource = Nothing
                    drpAutumnSession.DataBind()
                    drpAutumnSession.Items.Insert(0, "--------")
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        
        Private Function CheckAlternativeGroupForIntrimAssesment(ByVal GroupID As Integer, ByVal CourseID As Integer) As Integer
            Try
                Dim sSql As String = Database & "..spGetCheckPrimaryOrAlternativeGroup__c @GroupID=" & GroupID & ",@CourseID=" & CourseID
                Dim lGroupID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lGroupID > 0 Then
                    If lGroupID = GroupID Then
                        ViewState("ISAlternativeGroup") = False
                    Else
                        ' it meanse get alternative group
                        ViewState("ISAlternativeGroup") = True
                    End If
                End If
                Return lGroupID
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
            Try
                Dim sSql As String = Database & "..spGetClassRegistrationID__c @ClassID=" & Convert.ToInt32(ViewState("ClassID")) & ",@StudentID=" & User1.PersonID
                Dim lRegister As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lRegister > 0 Then
                    Dim oClassRegistration As AptifyGenericEntityBase
                    oClassRegistration = AptifyApplication.GetEntityObject("Class Registrations", lRegister)
                    oClassRegistration.SetValue("StudentGroupID__c", cmbGroupNames.SelectedValue)
                    If Convert.ToString(oClassRegistration.GetValue("ExamNumber__c")) <> "" Then
                        oClassRegistration.SetValue("ExamNumber__c", "")
                    End If
                    Dim lGroupID As Integer = CheckAlternativeGroupForIntrimAssesment(cmbGroupNames.SelectedValue, Convert.ToInt32(ViewState("IntrimCourseID")))

                    Dim sGetClassPartStatus As String = Database & "..spGetClassPartAsPerStudentGroup__c @ClassID=" & Convert.ToInt32(ViewState("ClassID")) & ",@StudGrp=" & lGroupID
                    Dim dt As DataTable = DataAction.GetDataTable(sGetClassPartStatus, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        oClassRegistration.SubTypes("ClassRegistrationPartStatus").Clear()
                        For Each dr As DataRow In dt.Rows
                            With oClassRegistration.SubTypes("ClassRegistrationPartStatus").Add
                                .SetValue("CoursePartID", Convert.ToInt32(dr("CoursePartID")))
                                .SetValue("StartDate", Convert.ToDateTime(dr("StartDate")))
                                .SetValue("EndDate", Convert.ToDateTime(dr("EndDate")))
                            End With
                        Next
                    End If

                    If oClassRegistration.Save() Then
                        radMockTrial.VisibleOnPageLoad = False
                        lblMsgClassRegistration.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.ClassRegistrationSuccessfullMSG")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        If Convert.ToBoolean(ViewState("ISAlternativeGroup")) = True Then
                            lblAlternativeMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.AlternativeLocationMessage")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radAlternativeLocation.VisibleOnPageLoad = True
                        Else
                            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
                        End If
                    End If
                Else
                    lblMsgClassRegistration.Text = "Please select location"
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
            radMockTrial.VisibleOnPageLoad = False
            ' Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
        End Sub

        'Private Sub LoadYear()
        '    Try
        '        Dim year As Integer = 2000
        '        For i As Integer = 0 To 20
        '            Dim newyear As Integer = year + i
        '            cmbYear.Items.Add(New WebControls.ListItem(newyear.ToString()))
        '        Next
        '        cmbYear.ClearSelection()

        '        cmbYear.SelectedValue = cmbYear.Items.FindByText(Format(Now, "yyyy")).Value
        '        cmbYear.Items.FindByText(2014).Selected = True

        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        'Protected Sub cmbYear_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbYear.SelectedIndexChanged
        '    Try
        '        LoadEnrolledCoursesGrid()
        '        ' LoadRagisteredExamsGrid()
        '        LoadRagisteredInterimAssessmentsGrid()
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        Protected Sub grdRagisteredInterimAssessments_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdRegisteredInterimAssessments.ItemCommand
            Try
                If e.CommandName = "ChangeGroup" Then
                    lblMsgClassRegistration.Text = ""
                    radMockTrial.VisibleOnPageLoad = True
                    'LoadGroupDetails(e.CommandArgument)
                    ViewState("ClassID") = e.CommandArgument
                    Dim lblStudentGroup As Label = TryCast(e.Item.FindControl("lblStudentGroup"), Label)
                    LoadGroupDetails(lblStudentGroup.Text, Convert.ToInt32(ViewState("ClassID")))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnExamLocationCancel_Click(sender As Object, e As System.EventArgs) Handles btnExamLocationCancel.Click
            radExamWindow.VisibleOnPageLoad = False
        End Sub

        Protected Sub btnExamLocationSave_Click(sender As Object, e As System.EventArgs) Handles btnExamLocationSave.Click
            Try
                Dim lblCourseID As Label
                Dim lblClassID As Label
                Dim lblCurriculumID As Label
                Dim isSucess As Boolean = False
                For Each row As RepeaterItem In rptEnrolledExams.Items

                    If Convert.ToString(ViewState("SessionName")).ToLower.Contains("summer") Then
                        Dim rpSummer As Repeater = DirectCast(row.FindControl("rptSummerSession"), Repeater)

                        For Each summer As RepeaterItem In rpSummer.Items
                            lblCourseID = DirectCast(summer.FindControl("lblCourseID"), Label)
                            lblClassID = DirectCast(summer.FindControl("lblClassID"), Label)
                            lblCurriculumID = DirectCast(summer.FindControl("lblCurriculumID"), Label)
                            If Convert.ToInt32(ViewState("CurriculumID")) = Convert.ToInt32(lblCurriculumID.Text) Then
                                Dim sSql As String = Database & "..spGetClassRegistrationID__c @ClassID=" & Convert.ToInt32(lblClassID.Text) & ",@StudentID=" & User1.PersonID
                                Dim lRegister As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If lRegister > 0 Then
                                    Dim oClassRegistration As AptifyGenericEntityBase
                                    oClassRegistration = AptifyApplication.GetEntityObject("Class Registrations", lRegister)

                                    ' first check selected location this current exam available or not if not then add alternet exam
                                    Dim sSqlCheckGroup As String = Database & "..spGetCheckPrimaryOrAlternativeGroup__c @GroupID=" & drpExamLocation.SelectedValue & ",@CourseID=" & lblCourseID.Text
                                    Dim lGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlCheckGroup, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                    oClassRegistration.SetValue("StudentGroupID__c", lGroupID)
                                    If Convert.ToString(oClassRegistration.GetValue("ExamNumber__c")) <> "" Then
                                        oClassRegistration.SetValue("ExamNumber__c", "")
                                    End If

                                    Dim sGetClassPartStatus As String = Database & "..spGetClassPartAsPerStudentGroup__c @ClassID=" & Convert.ToInt32(lblClassID.Text) & ",@StudGrp=" & lGroupID
                                    Dim dt As DataTable = DataAction.GetDataTable(sGetClassPartStatus, IAptifyDataAction.DSLCacheSetting.BypassCache)
                                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                                        oClassRegistration.SubTypes("ClassRegistrationPartStatus").Clear()
                                        For Each dr As DataRow In dt.Rows
                                            oClassRegistration.SubTypes("ClassRegistrationPartStatus").Clear()
                                            With oClassRegistration.SubTypes("ClassRegistrationPartStatus").Add
                                                .SetValue("CoursePartID", Convert.ToInt32(dr("CoursePartID")))
                                                .SetValue("StartDate", Convert.ToDateTime(dr("StartDate")))
                                                .SetValue("EndDate", Convert.ToDateTime(dr("EndDate")))
                                            End With
                                        Next
                                    End If

                                    If oClassRegistration.Save() Then
                                        isSucess = True
                                    End If
                                End If
                            End If
                        Next
                        If isSucess = True Then
                            lblMsgClassRegistration.Text = " Class Registration record updated successfully."
                        End If
                    Else
                        Dim rpAutum As Repeater = DirectCast(row.FindControl("rptAutumnSession"), Repeater)

                        For Each summer As RepeaterItem In rpAutum.Items
                            lblCourseID = DirectCast(summer.FindControl("lblCourseID"), Label)
                            lblClassID = DirectCast(summer.FindControl("lblClassID"), Label)
                            lblCurriculumID = DirectCast(summer.FindControl("lblCurriculumID"), Label)
                            If Convert.ToInt32(ViewState("CurriculumID")) = Convert.ToInt32(lblCurriculumID.Text) Then
                                Dim sSql As String = Database & "..spGetClassRegistrationID__c @ClassID=" & Convert.ToInt32(lblClassID.Text) & ",@StudentID=" & User1.PersonID
                                Dim lRegister As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If lRegister > 0 Then
                                    Dim oClassRegistration As AptifyGenericEntityBase
                                    oClassRegistration = AptifyApplication.GetEntityObject("Class Registrations", lRegister)

                                    ' first check selected location this current exam available or not if not then add alternet exam
                                    Dim sSqlCheckGroup As String = Database & "..spGetCheckPrimaryOrAlternativeGroup__c @GroupID=" & drpExamLocation.SelectedValue & ",@CourseID=" & lblCourseID.Text
                                    Dim lGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlCheckGroup, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                    oClassRegistration.SetValue("StudentGroupID__c", lGroupID)
                                    If Convert.ToString(oClassRegistration.GetValue("ExamNumber__c")) <> "" Then
                                        oClassRegistration.SetValue("ExamNumber__c", "")
                                    End If

                                    Dim sGetClassPartStatus As String = Database & "..spGetClassPartAsPerStudentGroup__c @ClassID=" & Convert.ToInt32(lblClassID.Text) & ",@StudGrp=" & lGroupID
                                    Dim dt As DataTable = DataAction.GetDataTable(sGetClassPartStatus, IAptifyDataAction.DSLCacheSetting.BypassCache)
                                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                                        oClassRegistration.SubTypes("ClassRegistrationPartStatus").Clear()
                                        For Each dr As DataRow In dt.Rows
                                            oClassRegistration.SubTypes("ClassRegistrationPartStatus").Clear()
                                            With oClassRegistration.SubTypes("ClassRegistrationPartStatus").Add
                                                .SetValue("CoursePartID", Convert.ToInt32(dr("CoursePartID")))
                                                .SetValue("StartDate", Convert.ToDateTime(dr("StartDate")))
                                                .SetValue("EndDate", Convert.ToDateTime(dr("EndDate")))
                                            End With
                                        Next
                                    End If

                                    If oClassRegistration.Save() Then
                                        isSucess = True
                                    End If
                                End If
                            End If
                        Next
                        If isSucess = True Then
                            lblMsgClassRegistration.Text = " Class Registration record updated successfully."
                            radExamWindow.VisibleOnPageLoad = False
                        End If
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            radExamChangeSession.VisibleOnPageLoad = False
        End Sub

        Protected Sub btnExamSessionSave_Click(sender As Object, e As System.EventArgs) Handles btnExamSessionSave.Click

            If drpAutumnSession.SelectedValue > 0 Then
                Dim chkExamChecked As CheckBox
                Dim lblOrderID As Label
                Dim lblSummerProductID As Label
                Dim lblAutumnClassProductID As Label
                Dim lblCourseID As Label
                Dim lblAutumnClassID As Label
                Dim dtCurrentChangeSessionTable As New DataTable
                dtCurrentChangeSessionTable.Columns.Add("OrderID")
                dtCurrentChangeSessionTable.Columns.Add("SummerProductID")
                dtCurrentChangeSessionTable.Columns.Add("AutumProductID")
                dtCurrentChangeSessionTable.Columns.Add("AutumClassID")
                dtCurrentChangeSessionTable.Columns.Add("AutumCourseID")
                Dim drCurrentChangeSessionTable As DataRow
                For Each row As Telerik.Web.UI.GridItem In RadChangeExamSession.Items
                    chkExamChecked = DirectCast(row.FindControl("chkExamChecked"), CheckBox)
                    lblOrderID = DirectCast(row.FindControl("lblOrderID"), Label)
                    lblSummerProductID = DirectCast(row.FindControl("lblSummerProductID"), Label)
                    lblAutumnClassProductID = DirectCast(row.FindControl("lblAutumnClassProductID"), Label)
                    lblCourseID = DirectCast(row.FindControl("lblCourseID"), Label)
                    lblAutumnClassID = DirectCast(row.FindControl("lblAutumnClassID"), Label)
                    If chkExamChecked.Checked = True Then
                        drCurrentChangeSessionTable = dtCurrentChangeSessionTable.NewRow()
                        drCurrentChangeSessionTable("OrderID") = lblOrderID.Text
                        drCurrentChangeSessionTable("SummerProductID") = lblSummerProductID.Text
                        drCurrentChangeSessionTable("AutumProductID") = lblAutumnClassProductID.Text
                        drCurrentChangeSessionTable("AutumClassID") = lblAutumnClassID.Text
                        drCurrentChangeSessionTable("AutumCourseID") = lblCourseID.Text
                        dtCurrentChangeSessionTable.Rows.Add(drCurrentChangeSessionTable)
                    End If
                Next
                If Not dtCurrentChangeSessionTable Is Nothing AndAlso dtCurrentChangeSessionTable.Rows.Count > 0 Then
                    Dim DistinctDt As DataTable = dtCurrentChangeSessionTable.DefaultView.ToTable(True, "OrderID")
                    For Each drDistinctRow As DataRow In DistinctDt.Rows
                        Dim drDistinctRowsValue() As System.Data.DataRow
                        drDistinctRowsValue = dtCurrentChangeSessionTable.Select("OrderID=" & drDistinctRow("OrderID"))
                        Dim OrigOrderGE As OrdersEntity
                        OrigOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", drDistinctRow("OrderID")), OrdersEntity)
                        Dim CancelOrderGE As OrdersEntity
                        CancelOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                        Dim m_oCancelGE As OrdersEntity = CancelOrderGeDetails(OrigOrderGE, CancelOrderGE)

                        ' Create new Order 
                        Dim newOrder As OrdersEntity = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                        Dim newAutumnOrder As OrdersEntity = NewOrderGE(m_oCancelGE, newOrder)
                        For value As Integer = 0 To drDistinctRowsValue.Length - 1
                            '(drDistinctRowsValue(value).ItemArray(1)) ' Get Summer Product Items find
                            '(drDistinctRowsValue(value).ItemArray(2)) ' Get Autum Product

                            ''Dim CancelOrderID As Long
                            ''If CancellOrderLine(drDistinctRow("OrderID"), Convert.ToInt32(drDistinctRowsValue(value).ItemArray(1)), CancelOrderID) Then
                            ''    ' here cancellation new order
                            ''End If
                            CancelOrderProductsAdd(OrigOrderGE, m_oCancelGE, Convert.ToInt32(drDistinctRowsValue(value).ItemArray(1)))
                            AddAutumnProductDetails(newAutumnOrder, Convert.ToInt32(drDistinctRowsValue(value).ItemArray(2)), Convert.ToInt32(drDistinctRowsValue(value).ItemArray(3)), Convert.ToInt32(drDistinctRowsValue(value).ItemArray(4)))

                        Next
                        m_oCancelGE.Fields("PayTypeID").SetValue(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order")))
                        m_oCancelGE.Fields("PONumber").SetValue("N/a")
                        If m_oCancelGE.Save Then
                            newAutumnOrder.SetValue("PayTypeID", Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Credit Memo")))
                            newAutumnOrder.SetValue("CreditOrderID", m_oCancelGE.RecordID)
                            newAutumnOrder.OrderStatus = OrderStatus.Taken
                            newAutumnOrder.OrderType = OrderType.Regular
                            If newAutumnOrder.Save() Then

                            End If

                        End If
                    Next
                End If
                radExamChangeSession.VisibleOnPageLoad = False
                drpAutumnSession.DataSource = Nothing
                drpAutumnSession.DataBind()
            End If






            ''Try
            ''    lblExamSessionMsg.Text = ""
            ''    If drpAutumnSession.SelectedValue > 0 Then
            ''        Dim chkExamChecked As CheckBox
            ''        Dim lblOrderID As Label
            ''        Dim lblClassID As Label
            ''        Dim lblAutumnClassID As Label
            ''        Dim lblClassRegistrationID As Label
            ''        Dim lblCurriculumID As Label
            ''        Dim lblCourseID As Label
            ''        Dim err As String = String.Empty
            ''        Dim lOrderIDAdded As Long
            ''        For Each row As Telerik.Web.UI.GridItem In RadChangeExamSession.Items
            ''            chkExamChecked = DirectCast(row.FindControl("chkExamChecked"), CheckBox)
            ''            lblOrderID = DirectCast(row.FindControl("lblOrderID"), Label)
            ''            lblClassID = DirectCast(row.FindControl("lblClassID"), Label)
            ''            lblAutumnClassID = DirectCast(row.FindControl("lblAutumnClassID"), Label)
            ''            lblClassRegistrationID = DirectCast(row.FindControl("lblClassRegistrationID"), Label)
            ''            lblCurriculumID = DirectCast(row.FindControl("lblCurriculumID"), Label)
            ''            lblCourseID = DirectCast(row.FindControl("lblCourseID"), Label)
            ''            If chkExamChecked.Checked Then
            ''                Dim m_oCancelGE As OrdersEntity
            ''                m_oCancelGE = CancellOrderLine(lblOrderID.Text, lblClassID.Text)
            ''                m_oCancelGE.PayTypeID = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order"))
            ''                m_oCancelGE.PONumber = "N/A"
            ''                If m_oCancelGE.Save(False, err) Then
            ''                    CreateNewOrder(m_oCancelGE.RecordID, lblAutumnClassID.Text, lblClassRegistrationID.Text, lblCourseID.Text)
            ''                    ' below code for change location and update curriculum part
            ''                    'If ChangeLocationBySession(lblCurriculumID.Text, lblClassID.Text, lblCourseID.Text, lblCurriculumID.Text) Then

            ''                    'End If
            ''                End If
            ''            End If
            ''        Next

            ''        radExamChangeSession.VisibleOnPageLoad = False
            ''        drpAutumnSession.DataSource = Nothing
            ''        drpAutumnSession.DataBind()

            ''    Else
            ''        ' Error msg please select location 
            ''        lblExamSessionMsg.Text = "Please select Location"
            ''        lblExamSessionMsg.Visible = True
            ''    End If

            ''Catch ex As Exception
            ''    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            ''End Try
        End Sub
        Private Function CancelOrderGeDetails(ByVal OriginalOrderGE As OrdersEntity, ByVal CancelOrderGE As OrdersEntity) As OrdersEntity
            Try
                With CancelOrderGE
                    .Fields("ShipToID").SetValue(OriginalOrderGE.Fields("ShipToID").Value)
                    .Fields("ShipToCompanyID").SetValue(OriginalOrderGE.Fields("ShipToCompanyID").Value)
                    .Fields("ShipToAddressID").SetValue(OriginalOrderGE.Fields("ShipToAddressID").Value)
                    .Fields("ShipToPhoneID").SetValue(OriginalOrderGE.Fields("ShipToPhoneID").Value)

                    .Fields("BillToSameAsShipTo").SetValue(OriginalOrderGE.Fields("BillToSameAsShipTo").Value)

                    .Fields("BillToID").SetValue(OriginalOrderGE.Fields("BillToID").Value)
                    .Fields("BillToCompanyID").SetValue(OriginalOrderGE.Fields("BillToCompanyID").Value)
                    .Fields("BillToAddressID").SetValue(OriginalOrderGE.Fields("BillToAddressID").Value)
                    .Fields("BillToPhoneID").SetValue(OriginalOrderGE.Fields("BillToPhoneID").Value)

                    .Fields("EmployeeID").SetValue(CType(DataAction.UserCredentials.GetUserRelatedRecordID("Employees"), Integer))

                    .Fields("SalesRepID").SetValue(OriginalOrderGE.Fields("SalesRepID").Value)
                    .Fields("ReferredByID").SetValue(OriginalOrderGE.Fields("ReferredByID").Value)
                    .Fields("OrderSourceID").SetValue(OriginalOrderGE.Fields("OrderSourceID").Value)
                    .Fields("OrderLevelID").SetValue(OriginalOrderGE.Fields("OrderLevelID").Value)
                    .Fields("TaxJurisdictionID").SetValue(OriginalOrderGE.Fields("TaxJurisdictionID").Value)
                    .Fields("CurrencyTypeID").SetValue(OriginalOrderGE.Fields("CurrencyTypeID").Value)
                    .Fields("CampaignCodeID").SetValue(OriginalOrderGE.Fields("CampaignCodeID").Value)
                    .Fields("OpportunityID").SetValue(OriginalOrderGE.Fields("OpportunityID").Value)

                    'SR -Issue 10307- Copy Comments from Original Order to Cancellation
                    'Find index of <BODY> & <P> tag in Html tag & insert "Comments from Original Order:" in the Html tag.
                    If ((Convert.ToString(OriginalOrderGE.Fields("Comments").Value) IsNot Nothing) AndAlso (Convert.ToString(OriginalOrderGE.Fields("Comments").Value) <> "")) Then

                        Dim strOriginalComments As String
                        strOriginalComments = OriginalOrderGE.Fields("Comments").GetOldValue().ToString()
                    End If
                    .Fields("OriginalOrderID").SetValue(OriginalOrderGE.RecordID)
                    .SetValue("CancellationReasonID", 1)
                    .Fields("OrderStatus").SetValue(OrderStatus.Taken)
                    .Fields("OrderType").SetValue(OrderType.Cancellation)
                End With
                Return CancelOrderGE
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        Private Sub CancelOrderProductsAdd(ByVal OriginalOrderID As OrdersEntity, ByVal CancelOrderGE As OrdersEntity, ByVal SummerProduct As Long)
            Try
                For Each oOL As OrderLinesEntity In OriginalOrderID.SubTypes("OrderLines")
                    If SummerProduct = oOL.ProductID Then
                        CancelOrderGE.AddCancellationOrderLine(oOL.ID, oOL.Quantity)
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub AddAutumnProductDetails(ByVal AutumOrderGE As OrdersEntity, ByVal AutumProduct As Long, ByVal lAutumnClassID As Long, lCourseID As Long)
            Try
                Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
                AutumOrderGE.AddProduct(AutumProduct)
                oOrderLine = AutumOrderGE.SubTypes("OrderLines").Item(AutumOrderGE.SubTypes("OrderLines").Count - 1)
                '  oOrderLine = AutumOrderGE.AddProduct(AutumProduct).Item(AutumOrderGE.SubTypes("OrderLines").Count - 1)
                With oOrderLine
                    .ExtendedOrderDetailEntity.SetValue("ClassID", lAutumnClassID)
                    Dim sSqlCheckGroup As String = Database & "..spGetCheckPrimaryOrAlternativeGroup__c @GroupID=" & drpAutumnSession.SelectedValue & ",@CourseID=" & lCourseID
                    Dim lGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlCheckGroup, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", lGroupID)
                    .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Function NewOrderGE(ByVal CancelOrderGE As OrdersEntity, ByVal NewOrder As OrdersEntity) As OrdersEntity
            Try
                With NewOrder
                    .SetValue("ShipToID", CancelOrderGE.Fields("ShipToID").Value)
                    '.SetValue("ShipToID", m_App.UserCredentials.GetUserRelatedRecordID("Persons"))
                    '.SetValue("ShipToID", User1.PersonID)
                    .SetValue("EmployeeID", AptifyApplication.UserCredentials.GetUserRelatedRecordID("Employees"))
                    .SetValue("OrganizationID", CancelOrderGE.OrganizationID)
                End With
                Return NewOrder
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        Private Function CancellOrderLine(ByVal lOrderID As Long, ByVal lProductID As Long, ByRef CancelationOrderID As Long) As Boolean
            Try
                Dim OrigOrderGE As OrdersEntity
                OrigOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", lOrderID), OrdersEntity)
                Dim oOrderGE As OrdersEntity
                Dim bOrder As Boolean = False
                If ViewState("CancelOrderID") Is Nothing Then
                    oOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                Else
                    If lOrderID = Convert.ToInt32(ViewState("OrigOrderID")) Then
                        oOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                    Else
                        oOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(ViewState("CancelOrderID"))), OrdersEntity)
                    End If
                End If


                With oOrderGE
                    .Fields("ShipToID").SetValue(OrigOrderGE.Fields("ShipToID").Value)
                    .Fields("ShipToCompanyID").SetValue(OrigOrderGE.Fields("ShipToCompanyID").Value)
                    .Fields("ShipToAddressID").SetValue(OrigOrderGE.Fields("ShipToAddressID").Value)
                    .Fields("ShipToPhoneID").SetValue(OrigOrderGE.Fields("ShipToPhoneID").Value)

                    .Fields("BillToSameAsShipTo").SetValue(OrigOrderGE.Fields("BillToSameAsShipTo").Value)

                    .Fields("BillToID").SetValue(OrigOrderGE.Fields("BillToID").Value)
                    .Fields("BillToCompanyID").SetValue(OrigOrderGE.Fields("BillToCompanyID").Value)
                    .Fields("BillToAddressID").SetValue(OrigOrderGE.Fields("BillToAddressID").Value)
                    .Fields("BillToPhoneID").SetValue(OrigOrderGE.Fields("BillToPhoneID").Value)

                    .Fields("EmployeeID").SetValue(CType(DataAction.UserCredentials.GetUserRelatedRecordID("Employees"), Integer))

                    .Fields("SalesRepID").SetValue(OrigOrderGE.Fields("SalesRepID").Value)
                    .Fields("ReferredByID").SetValue(OrigOrderGE.Fields("ReferredByID").Value)
                    .Fields("OrderSourceID").SetValue(OrigOrderGE.Fields("OrderSourceID").Value)
                    .Fields("OrderLevelID").SetValue(OrigOrderGE.Fields("OrderLevelID").Value)
                    .Fields("TaxJurisdictionID").SetValue(OrigOrderGE.Fields("TaxJurisdictionID").Value)
                    .Fields("CurrencyTypeID").SetValue(OrigOrderGE.Fields("CurrencyTypeID").Value)
                    .Fields("CampaignCodeID").SetValue(OrigOrderGE.Fields("CampaignCodeID").Value)
                    .Fields("OpportunityID").SetValue(OrigOrderGE.Fields("OpportunityID").Value)

                    'SR -Issue 10307- Copy Comments from Original Order to Cancellation
                    'Find index of <BODY> & <P> tag in Html tag & insert "Comments from Original Order:" in the Html tag.
                    If ((Convert.ToString(OrigOrderGE.Fields("Comments").Value) IsNot Nothing) AndAlso (Convert.ToString(OrigOrderGE.Fields("Comments").Value) <> "")) Then

                        Dim strOriginalComments As String
                        strOriginalComments = OrigOrderGE.Fields("Comments").GetOldValue().ToString()
                    End If
                    .OriginalOrderID = lOrderID
                    .SetValue("CancellationReasonID", 1)
                    .OrderStatus = OrderStatus.Taken
                    .OrderType = OrderType.Cancellation
                End With


                For Each oOL As OrderLinesEntity In OrigOrderGE.SubTypes("OrderLines")
                    If lProductID = oOL.ProductID Then
                        oOrderGE.AddCancellationOrderLine(oOL.ID, oOL.Quantity)
                    End If
                Next

                oOrderGE.Fields("PayTypeID").SetValue(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order")))
                oOrderGE.Fields("PONumber").SetValue("N/a")
                If oOrderGE.Save Then
                    CancelationOrderID = oOrderGE.RecordID
                    ViewState("CancelOrderID") = oOrderGE
                    ViewState("OrigOrderID") = lOrderID
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        Public Function CreateNewOrder(ByVal CancellationOrderID As Long, ByVal lAutumnClassID As Long, ByVal lClassRegistrationID As Long, ByVal lCourseID As Long) As Boolean
            Try
                Dim oCancellationOrderGE As OrdersEntity
                oCancellationOrderGE = AptifyApplication.GetEntityObject("Orders", CancellationOrderID)
                Dim oNewOrderGE As OrdersEntity
                oNewOrderGE = AptifyApplication.GetEntityObject("Orders", -1)
                Dim sFlag As Boolean
                Dim oClassGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Classes", lAutumnClassID)
                Dim lProductID As Long = Convert.ToInt32(oClassGE.GetValue("ProductID"))
                With oNewOrderGE
                    .SetValue("ShipToID", oCancellationOrderGE.Fields("ShipToID").Value)
                    '.SetValue("ShipToID", m_App.UserCredentials.GetUserRelatedRecordID("Persons"))
                    '.SetValue("ShipToID", User1.PersonID)
                    .SetValue("EmployeeID", AptifyApplication.UserCredentials.GetUserRelatedRecordID("Employees"))
                    .SetValue("OrganizationID", oCancellationOrderGE.OrganizationID)
                    Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity

                    oOrderLine = oNewOrderGE.AddProduct(lProductID).Item(0)
                    With oOrderLine
                        .ExtendedOrderDetailEntity.SetValue("ClassID", lAutumnClassID)
                        Dim sSqlCheckGroup As String = Database & "..spGetCheckPrimaryOrAlternativeGroup__c @GroupID=" & drpAutumnSession.SelectedValue & ",@CourseID=" & lCourseID
                        Dim lGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlCheckGroup, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", lGroupID)
                        ''Dim sGetClassPartStatus As String = Database & "..spGetClassPartAsPerStudentGroup__c @ClassID=" & Convert.ToInt32(lAutumnClassID) & ",@StudGrp=" & lGroupID
                        ''Dim dt As DataTable = DataAction.GetDataTable(sGetClassPartStatus, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        ''If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        ''    .ExtendedOrderDetailEntity.SubTypes("ClassRegistrationPartStatus").Clear()
                        ''    For Each dr As DataRow In dt.Rows
                        ''        With .ExtendedOrderDetailEntity.SubTypes("ClassRegistrationPartStatus").Add
                        ''            .SetValue("CoursePartID", Convert.ToInt32(dr("CoursePartID")))
                        ''            .SetValue("StartDate", Convert.ToDateTime(dr("StartDate")))
                        ''            .SetValue("EndDate", Convert.ToDateTime(dr("EndDate")))
                        ''        End With
                        ''    Next
                        ''End If
                        .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                    End With
                    .SetValue("PayTypeID", Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Credit Memo")))
                    .SetValue("CreditOrderID", oCancellationOrderGE.RecordID)
                    .OrderStatus = OrderStatus.Taken
                    .OrderType = OrderType.Regular
                    Dim sErr As String = ""
                    If .Save(False, sErr) Then
                        sFlag = True
                        'If SetOrderOnClassRegistration(oNewOrderGE.RecordID, lClassRegistrationID) Then

                        'End If
                    Else
                        sFlag = sFlag
                    End If
                End With
                Return sFlag

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        Private Function SetOrderOnClassRegistration(ByVal lOrderID As Long, ByVal lClassRegistrationID As Long) As Boolean
            Try
                Dim sFlag As Boolean
                Dim oClassRegistrationID As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Class Registrations", lClassRegistrationID)
                oClassRegistrationID.SetValue("OrderID", lOrderID)
                If oClassRegistrationID.Save() Then
                    sFlag = True
                End If
                Return sFlag
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        Private Function ChangeLocationBySession(ByVal lCurriculumID As Long, ByVal lClassID As Long, ByVal lCourseID As Long, ByVal lClassRegistrationID As Long) As Boolean
            Try

                Dim oClassRegistration As AptifyGenericEntityBase
                oClassRegistration = AptifyApplication.GetEntityObject("Class Registrations", lClassRegistrationID)
                Dim sFlag As Boolean
                ' first check selected location this current exam available or not if not then add alternet exam
                Dim sSqlCheckGroup As String = Database & "..spGetCheckPrimaryOrAlternativeGroup__c @GroupID=" & drpExamLocation.SelectedValue & ",@CourseID=" & lCourseID
                Dim lGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlCheckGroup, IAptifyDataAction.DSLCacheSetting.BypassCache))
                oClassRegistration.SetValue("StudentGroupID__c", lGroupID)
                If Convert.ToString(oClassRegistration.GetValue("ExamNumber__c")) <> "" Then
                    oClassRegistration.SetValue("ExamNumber__c", "")
                End If

                Dim sGetClassPartStatus As String = Database & "..spGetClassPartAsPerStudentGroup__c @ClassID=" & Convert.ToInt32(lClassID) & ",@StudGrp=" & lGroupID
                Dim dt As DataTable = DataAction.GetDataTable(sGetClassPartStatus, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    oClassRegistration.SubTypes("ClassRegistrationPartStatus").Clear()
                    For Each dr As DataRow In dt.Rows
                        With oClassRegistration.SubTypes("ClassRegistrationPartStatus").Add
                            .SetValue("CoursePartID", Convert.ToInt32(dr("CoursePartID")))
                            .SetValue("StartDate", Convert.ToDateTime(dr("StartDate")))
                            .SetValue("EndDate", Convert.ToDateTime(dr("EndDate")))
                        End With
                    Next
                End If

                If oClassRegistration.Save() Then
                    sFlag = True
                Else
                    sFlag = False
                End If


                Return sFlag

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        ''Protected Sub grdRegisteredExams_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdRegisteredExams.ItemDataBound
        ''    Try
        ''        If TypeOf e.Item Is GridGroupHeaderItem Then
        ''            Dim oItem As GridGroupHeaderItem = DirectCast(e.Item, GridGroupHeaderItem)
        ''            Dim lnkAdd As New LinkButton()
        ''            lnkAdd.ID = "lnkAdd"
        ''            lnkAdd.CommandName = "CustomAddWorkRequestItemCost"
        ''            lnkAdd.CommandArgument = DirectCast(oItem.DataItem, DataRowView).Row("nWorkRequestItemID").ToString()
        ''            Dim tcPlaceholder As GridTableCell = DirectCast(oItem.Controls(1), GridTableCell)
        ''            Dim litText As New LiteralControl(String.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", tcPlaceholder.Text))
        ''            tcPlaceholder.Text = String.Empty
        ''            tcPlaceholder.Controls.Add(lnkAdd)
        ''            tcPlaceholder.Controls.Add(litText)





        ''        End If



        ''    Catch ex As Exception
        ''        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        ''    End Try
        ''End Sub

        Private Sub LoadCurriculumDetails()
            Try
                Dim sSql As String = Database & "..spGetCurriculumnBind__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & ViewState("AcademicCycleID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rptEnrolledExams.DataSource = dt
                    rptEnrolledExams.DataBind()
                    rptEnrolledExams.Visible = True
                    lblErrorRagisteredExams.Text = ""
                Else
                    rptEnrolledExams.Visible = False
                    lblErrorRagisteredExams.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.ErrorMsgRegisteredExams")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub rptSummerSession_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
            Try
                If e.CommandName = "ChangeGroupByCurriculum" Then
                    radExamChangeSession.VisibleOnPageLoad = True
                    LoadAutumnSession(e.CommandArgument)
                End If
                If e.CommandName = "ChangeLocationBySession" Then
                    Dim lSessionID As Long
                    Dim sSql As String = Database & "..spGetSessionID__c @Name='" & DirectCast(e.Item.FindControl("lblSummerSession"), System.Web.UI.WebControls.Label).Text & "'"
                    lSessionID = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    ViewState("CurriculumID") = DirectCast(e.Item.FindControl("lCurriculumID"), System.Web.UI.WebControls.Label).Text
                    ViewState("SessionName") = DirectCast(e.Item.FindControl("lblSummerSession"), System.Web.UI.WebControls.Label).Text
                    If ViewState("CurriculumID") > 0 AndAlso lSessionID > 0 Then
                        lblChangeLocation.Text = "Change location of all " & ViewState("SessionName") & " Exam to:"
                        radExamWindow.VisibleOnPageLoad = True
                        LoadGroupDetailsByCurriculum(lSessionID, ViewState("CurriculumID"))
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rptEnrolledExams_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptEnrolledExams.ItemCommand
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lbl As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)
                    Dim rpSummer As Repeater = DirectCast(e.Item.FindControl("rptSummerSession"), Repeater)
                    Dim rpAutumn As Repeater = DirectCast(e.Item.FindControl("rptAutumnSession"), Repeater)

                    If e.CommandName = "ChangeGroupByCurriculum" Then
                        lblExamMsg.Text = ""
                        radExamWindow.VisibleOnPageLoad = True
                        'LoadGroupDetails(e.CommandArgument)
                        Dim sSql As String = Database & "..spGetCurriculumIDFromName__c @Name='" & e.CommandArgument & "'"
                        Dim CurriculumID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        Dim lblSessionID As Label = TryCast(e.Item.FindControl("lblSessionID"), Label)

                        LoadGroupDetailsByCurriculum(lbl.Text, lblSessionID.Text)
                    ElseIf e.CommandName = "ChangeGroupBySession" Then
                        Dim sSql As String = Database & "..spGetCurriculumIDFromName__c @Name='" & e.CommandArgument & "'"
                        Dim CurriculumID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        ' Dim lblSessionID As Label = TryCast(e.Item.FindControl("lblSessionID"), Label)
                        radExamChangeSession.VisibleOnPageLoad = True
                        LoadAutumnSession(CurriculumID)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rptEnrolledExams_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptEnrolledExams.ItemDataBound
            Try
                Dim lblSummerSession As Label
                'If e.Item.ItemType = ListItemType.Header Then
                '    lblSummerSession = DirectCast(e.Item.FindControl("lblSummerSession"), Label)

                'End If
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lbl As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)

                    Dim lnkUpdateGroup As LinkButton
                    Dim rpSummer As Repeater = DirectCast(e.Item.FindControl("rptSummerSession"), Repeater)
                    Dim rpAutumn As Repeater = DirectCast(e.Item.FindControl("rptAutumnSession"), Repeater)
                    ' lblSummerSession = DirectCast(rpSummer.Controls(0).Controls(0).FindControl("lblSummerSession"), Label)

                    Dim sSummerSql As String = Database & "..spGetSummerSession__c @PersonID=" & User1.PersonID & ",@CurriculumID=" & lbl.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID")
                    Dim dtSummer As DataTable = DataAction.GetDataTable(sSummerSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dtSummer Is Nothing AndAlso dtSummer.Rows.Count > 0 Then
                        rpSummer.DataSource = dtSummer
                        rpSummer.DataBind()

                        DirectCast(rpSummer.Controls(0).Controls(0).FindControl("lblSummerSession"), Label).Text = dtSummer.Rows(0)("session").ToString
                        DirectCast(rpSummer.Controls(0).Controls(0).FindControl("lCurriculumID"), Label).Text = dtSummer.Rows(0)("CurriculumID").ToString
                        DirectCast(rpSummer.Controls(0).Controls(0).FindControl("lnkUpdateGroup"), LinkButton).CommandArgument = dtSummer.Rows(0)("CurriculumID").ToString
                        lnkUpdateGroup = DirectCast(rpSummer.Controls(0).Controls(0).FindControl("lnkUpdateGroup"), LinkButton)
                        'lnkUpdateGroup.cl += New EventHandler(lnkUpdateGroup_Click)
                        '' AddHandler lnkUpdateGroup.Click, AddressOf lnkUpdateGroup_OnClick
                        Dim sSql As String = Database & "..spGetSessionID__c @Name='" & DirectCast(rpSummer.Controls(0).Controls(0).FindControl("lblSummerSession"), Label).Text & "'"
                        Dim lSessionID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        Dim sSqlSessionDateExpire As String = "..spGetIsDisplayChangeLocationOnExam__c @ExamSessionID=" & lSessionID
                        Dim ISDisplayChangeLocation As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlSessionDateExpire, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If ISDisplayChangeLocation > 0 Then
                            rpSummer.Controls(0).Controls(0).FindControl("lnkChangeLocation").Visible = True
                        Else
                            rpSummer.Controls(0).Controls(0).FindControl("lnkChangeLocation").Visible = False
                        End If
                        Dim lBillToCompanyID As Long
                        For Each rpsum As RepeaterItem In rpSummer.Items
                            Dim lblBillToComany As Label = DirectCast(rpsum.FindControl("lblBillToComany"), Label)
                            If lblBillToComany.Text = 1 Then
                                lBillToCompanyID = lblBillToComany.Text
                                Exit For
                            End If
                        Next



                        If lBillToCompanyID > 0 Then
                            rpSummer.Controls(0).Controls(0).FindControl("lnkUpdateGroup").Visible = False

                        Else
                            rpSummer.Controls(0).Controls(0).FindControl("lnkUpdateGroup").Visible = True

                        End If
                        ' check if publish date + end days for displaying exam number 
                        For Each rpsum As RepeaterItem In rpSummer.Items
                            Dim lblSummerEnrolledExam As Label = DirectCast(rpsum.FindControl("lblSummerEnrolledExam"), Label)
                            Dim sDisplayExamNumber As String = Database & "..spDisplayExamNumberBeforePublishDate__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                                ",@CurriculumID=" & lbl.Text & ",@SessionID=" & lSessionID
                            Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sDisplayExamNumber, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If IsDisplayExamNumber = True Then
                                lblSummerEnrolledExam.Visible = True
                            Else
                                lblSummerEnrolledExam.Visible = False
                            End If
                        Next

                    End If

                    Dim sAutumnSql As String = Database & "..spGetAutumnSessions__c @PersonID=" & User1.PersonID & ",@CurriculumID=" & lbl.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID")
                    Dim dtAutumn As DataTable = DataAction.GetDataTable(sAutumnSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dtAutumn Is Nothing AndAlso dtAutumn.Rows.Count > 0 Then
                        rpAutumn.DataSource = dtAutumn
                        rpAutumn.DataBind()
                        'Session("CurriculumnID") = lbl.Text
                        DirectCast(rpAutumn.Controls(0).Controls(0).FindControl("lAutumCurriculumID"), Label).Text = dtAutumn.Rows(0)("CurriculumID").ToString
                        DirectCast(rpAutumn.Controls(0).Controls(0).FindControl("lnkUpdateGroupAutumn"), LinkButton).CommandArgument = dtAutumn.Rows(0)("CurriculumID").ToString
                        DirectCast(rpAutumn.Controls(0).Controls(0).FindControl("lblH1"), Label).Text = dtAutumn.Rows(0)("session").ToString
                        Dim sSql As String = Database & "..spGetSessionID__c @Name='" & DirectCast(rpAutumn.Controls(0).Controls(0).FindControl("lblH1"), Label).Text & "'"
                        Dim lSessionID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        Dim sSqlSessionDateExpire As String = "..spGetIsDisplayChangeLocationOnExam__c @ExamSessionID=" & lSessionID
                        Dim ISDisplayChangeLocation As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlSessionDateExpire, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If ISDisplayChangeLocation > 0 Then
                            rpAutumn.Controls(0).Controls(0).FindControl("lnkUpdateGroupAutumn").Visible = True
                        Else
                            rpAutumn.Controls(0).Controls(0).FindControl("lnkUpdateGroupAutumn").Visible = False
                        End If
                        ' check if publish date + end days for displaying exam number 
                        For Each rpAut As RepeaterItem In rpAutumn.Items
                            Dim lblAutumnEnrolledExam As Label = DirectCast(rpAut.FindControl("lblAutumnEnrolledExam"), Label)
                            Dim sDisplayExamNumber As String = Database & "..spDisplayExamNumberBeforePublishDate__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                                ",@CurriculumID=" & lbl.Text & ",@SessionID=" & lSessionID
                            Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sDisplayExamNumber, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If IsDisplayExamNumber = True Then
                                lblAutumnEnrolledExam.Visible = True
                            Else
                                lblAutumnEnrolledExam.Visible = False
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub rptAutumnLocation_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
            Try
                If e.CommandName = "ChangeLocationByAutumn" Then
                    Dim lSessionID As Long
                    Dim sSql As String = Database & "..spGetSessionID__c @Name='" & DirectCast(e.Item.FindControl("lblH1"), System.Web.UI.WebControls.Label).Text & "'"
                    lSessionID = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    ViewState("CurriculumID") = DirectCast(e.Item.FindControl("lAutumCurriculumID"), System.Web.UI.WebControls.Label).Text
                    ViewState("SessionName") = DirectCast(e.Item.FindControl("lblH1"), System.Web.UI.WebControls.Label).Text
                    If ViewState("CurriculumID") > 0 AndAlso lSessionID > 0 Then
                        lblChangeLocation.Text = "Change location of all " & ViewState("SessionName") & " Exam to:"
                        radExamWindow.VisibleOnPageLoad = True
                        LoadGroupDetailsByCurriculum(lSessionID, ViewState("CurriculumID"))
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub lnkUpdateGroup_OnClick(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim lblCurriculumnID As Label
                For Each ri As RepeaterItem In rptEnrolledExams.Items
                    Dim rpSummer As Repeater = DirectCast(ri.FindControl("rptSummerSession"), Repeater)
                    'For Each rp1 As RepeaterItem In rpSummer.Items
                    '    lblCurriculumnID = rp1.FindControl("lblCurriculumID")
                    '    Exit For
                    'Next
                    'lblCurriculumnID = DirectCast(DirectCast(rpSummer.Controls(0), System.Web.UI.WebControls.RepeaterItem).DataItemContainer, System.Web.UI.WebControls.RepeaterItem).Controls(3)
                    lblCurriculumnID = DirectCast(rpSummer.BindingContainer, System.Web.UI.WebControls.RepeaterItem).Controls(3)
                    ' lblCurriculumnID = Session("CurriculumnID")
                    Exit For
                Next
                If lblCurriculumnID.Text > 0 Then
                    radExamChangeSession.VisibleOnPageLoad = True
                    LoadAutumnSession(lblCurriculumnID.Text)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub lnkChangeLocation_OnClick(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim lblCurriculumnID As Label
                Dim lblSessionID As Label
                Dim lSessionID As Integer
                For Each ri As RepeaterItem In rptEnrolledExams.Items
                    Dim rpSummer As Repeater = DirectCast(ri.FindControl("rptSummerSession"), Repeater)
                    lblCurriculumnID = DirectCast(DirectCast(rpSummer.Controls(0), System.Web.UI.WebControls.RepeaterItem).DataItemContainer, System.Web.UI.WebControls.RepeaterItem).Controls(3)
                    lblSessionID = DirectCast(DirectCast(rpSummer.Controls(0), System.Web.UI.WebControls.RepeaterItem).DataItemContainer, System.Web.UI.WebControls.RepeaterItem).Controls(5).Controls(0).FindControl("lblSummerSession")
                    Dim sSql As String = Database & "..spGetSessionID__c @Name='" & lblSessionID.Text & "'"
                    lSessionID = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    ViewState("CurriculumID") = lblCurriculumnID.Text
                    ViewState("SessionName") = lblSessionID.Text
                    Exit For
                Next
                lblExamMsg.Text = ""

                If lblCurriculumnID.Text > 0 AndAlso lSessionID > 0 Then
                    lblChangeLocation.Text = "Change location of all " & ViewState("SessionName") & " Exam to:"
                    radExamWindow.VisibleOnPageLoad = True
                    LoadGroupDetailsByCurriculum(lSessionID, lblCurriculumnID.Text)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub lnkAutumChangeLocation_OnClick(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim lblCurriculumnID As Label
                Dim lblSessionID As Label
                Dim lSessionID As Integer
                For Each ri As RepeaterItem In rptEnrolledExams.Items
                    Dim rptAutumnSession As Repeater = DirectCast(ri.FindControl("rptAutumnSession"), Repeater)
                    lblCurriculumnID = DirectCast(DirectCast(rptAutumnSession.Controls(0), System.Web.UI.WebControls.RepeaterItem).DataItemContainer, System.Web.UI.WebControls.RepeaterItem).Controls(3)
                    lblSessionID = DirectCast(DirectCast(DirectCast(rptAutumnSession.Controls(0), System.Web.UI.WebControls.RepeaterItem).DataItemContainer, System.Web.UI.WebControls.RepeaterItem).Controls(7).Controls(0), System.Web.UI.WebControls.RepeaterItem).Controls(1)
                    Dim sSql As String = Database & "..spGetSessionID__c @Name='" & lblSessionID.Text & "'"
                    lSessionID = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))

                    Exit For
                Next
                lblExamMsg.Text = ""

                If lblCurriculumnID.Text > 0 AndAlso lSessionID > 0 Then
                    lblChangeLocation.Text = "Change location of all " & lblSessionID.Text & " Exam to:"
                    radExamWindow.VisibleOnPageLoad = True
                    ViewState("CurriculumID") = lblCurriculumnID.Text
                    LoadGroupDetailsByCurriculum(lblCurriculumnID.Text, lSessionID)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdRegisteredInterimAssessments_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdRegisteredInterimAssessments.ItemDataBound
            Try
                If TypeOf e.Item Is GridItem Then
                    For Each grItem As GridItem In grdRegisteredInterimAssessments.Items
                        Dim lblIntrimExamNumber As Label = DirectCast(grItem.FindControl("lblIntrimExamNumber"), Label)
                        Dim lblIntrimCurriculum As Label = DirectCast(grItem.FindControl("lblIntrimCurriculum"), Label)
                        Dim lblIntrimSessionID As Label = DirectCast(grItem.FindControl("lblIntrimSessionID"), Label)
                        Dim sDisplayExamNumber As String = Database & "..spDisplayExamNumberBeforePublishDate__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                            ",@CurriculumID=" & lblIntrimCurriculum.Text & ",@SessionID=" & lblIntrimSessionID.Text
                        Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sDisplayExamNumber, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If IsDisplayExamNumber = True Then
                            lblIntrimExamNumber.Visible = True
                        Else
                            lblIntrimExamNumber.Visible = False
                        End If
                    Next

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdResitAssessment_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdResitAssessment.ItemDataBound
            Try
                If TypeOf e.Item Is GridItem Then
                    For Each grItem As GridItem In grdResitAssessment.Items
                        Dim lblResitExamNumber As Label = DirectCast(grItem.FindControl("lblResitExamNumber"), Label)
                        Dim lblResitCurriculum As Label = DirectCast(grItem.FindControl("lblResitCurriculum"), Label)
                        Dim lblResitSessionID As Label = DirectCast(grItem.FindControl("lblResitSessionID"), Label)
                        Dim sDisplayExamNumber As String = Database & "..spDisplayExamNumberBeforePublishDate__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                            ",@CurriculumID=" & lblResitCurriculum.Text & ",@SessionID=" & lblResitSessionID.Text
                        Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sDisplayExamNumber, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If IsDisplayExamNumber = True Then
                            lblResitExamNumber.Visible = True
                        Else
                            lblResitExamNumber.Visible = False
                        End If
                    Next

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdMockExam_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdMockExam.ItemDataBound
            Try
                If TypeOf e.Item Is GridItem Then
                    For Each grItem As GridItem In grdMockExam.Items
                        Dim lblMockExamNumber As Label = DirectCast(grItem.FindControl("lblMockExamNumber"), Label)
                        Dim lblMockCurriculum As Label = DirectCast(grItem.FindControl("lblMockCurriculum"), Label)
                        Dim lblMockSessionID As Label = DirectCast(grItem.FindControl("lblMockSessionID"), Label)
                        Dim sDisplayExamNumber As String = Database & "..spDisplayExamNumberBeforePublishDate__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                            ",@CurriculumID=" & lblMockCurriculum.Text & ",@SessionID=" & lblMockSessionID.Text
                        Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sDisplayExamNumber, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If IsDisplayExamNumber = True Then
                            lblMockExamNumber.Visible = True
                        Else
                            lblMockExamNumber.Visible = False
                        End If
                    Next

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rpEnrolledCourses_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpEnrolledCourses.ItemDataBound
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lbl As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)
                    Dim rpEnrolledCoursesDetails As Repeater = DirectCast(e.Item.FindControl("rpEnrolledCoursesDetails"), Repeater)
                    Dim sSql As String = Database & "..spGetEnrolledCoursesDetails__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CuriculumID=" & lbl.Text
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        rpEnrolledCoursesDetails.DataSource = dt
                        rpEnrolledCoursesDetails.DataBind()
                        rpEnrolledCoursesDetails.Visible = True

                    Else
                        rpEnrolledCoursesDetails.Visible = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rpMockExam_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpMockExam.ItemDataBound
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lbl As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)
                    Dim rpMockExamDetails As Repeater = DirectCast(e.Item.FindControl("rpMockExamDetails"), Repeater)
                    Dim sSql As String = Database & "..spGetEnrolledMockExamDetails__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CuriculumID=" & lbl.Text
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        rpMockExamDetails.DataSource = dt
                        rpMockExamDetails.DataBind()
                        rpMockExamDetails.Visible = True

                        For Each rp As RepeaterItem In rpMockExamDetails.Items
                            Dim lblMockExamNumber As Label = DirectCast(rp.FindControl("lblMockExamNumber"), Label)
                            Dim lblMockCurriculum As Label = DirectCast(rp.FindControl("lblMockCurriculum"), Label)
                            Dim lblMockSessionID As Label = DirectCast(rp.FindControl("lblMockSessionID"), Label)
                            Dim sDisplayExamNumber As String = Database & "..spDisplayExamNumberBeforePublishDate__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                                ",@CurriculumID=" & lblMockCurriculum.Text & ",@SessionID=" & lblMockSessionID.Text
                            Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sDisplayExamNumber, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If IsDisplayExamNumber = True Then
                                lblMockExamNumber.Visible = True
                            Else
                                lblMockExamNumber.Visible = False
                            End If
                        Next
                    Else
                        rpMockExamDetails.Visible = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rpIntrimAssessment_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpIntrimAssessment.ItemDataBound
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lbl As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)
                    Dim rpIntrimAssessmentDetails As Repeater = DirectCast(e.Item.FindControl("rpIntrimAssessmentDetails"), Repeater)
                    Dim sSql As String = Database & "..spGetIntrimAssesmentDetails__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CuriculumID=" & lbl.Text
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        rpIntrimAssessmentDetails.DataSource = dt
                        rpIntrimAssessmentDetails.DataBind()
                        rpIntrimAssessmentDetails.Visible = True

                        For Each rp As RepeaterItem In rpIntrimAssessmentDetails.Items
                            Dim lblIntrimExamNumber As Label = DirectCast(rp.FindControl("lblIntrimExamNumber"), Label)
                            Dim lblIntrimCurriculum As Label = DirectCast(rp.FindControl("lblIntrimCurriculum"), Label)
                            Dim lblIntrimSessionID As Label = DirectCast(rp.FindControl("lblIntrimSessionID"), Label)
                            Dim sDisplayExamNumber As String = Database & "..spDisplayExamNumberBeforePublishDate__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                                ",@CurriculumID=" & lblIntrimCurriculum.Text & ",@SessionID=" & lblIntrimSessionID.Text
                            Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sDisplayExamNumber, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If IsDisplayExamNumber = True Then
                                lblIntrimExamNumber.Visible = True
                            Else
                                lblIntrimExamNumber.Visible = False
                            End If
                        Next
                    Else
                        rpIntrimAssessmentDetails.Visible = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Protected Sub rpIntrimAssessmentDetails_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    CourseClick(e)
                    If e.CommandName = "ChangeGroup" Then
                        lblMsgClassRegistration.Text = ""
                        radMockTrial.VisibleOnPageLoad = True
                        'LoadGroupDetails(e.CommandArgument)
                        Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                        'ViewState("ClassID") = e.CommandArgument
                        ViewState("ClassID") = commandArgs(0)
                        ViewState("IntrimCourseID") = commandArgs(1)
                        Dim lblStudentGroup As Label = TryCast(e.Item.FindControl("lblStudentGroup"), Label)
                        LoadGroupDetails(lblStudentGroup.Text, Convert.ToInt32(ViewState("ClassID")))
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rpResitAssessment_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpResitAssessment.ItemDataBound
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lbl As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)
                    Dim rpResitAssessmentDetails As Repeater = DirectCast(e.Item.FindControl("rpResitAssessmentDetails"), Repeater)
                    Dim sSql As String = Database & "..spGetResitExamDetails__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CuriculumID=" & lbl.Text
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        rpResitAssessmentDetails.DataSource = dt
                        rpResitAssessmentDetails.DataBind()
                        rpResitAssessmentDetails.Visible = True

                        For Each rp As RepeaterItem In rpResitAssessmentDetails.Items
                            Dim lblResitExamNumber As Label = DirectCast(rp.FindControl("lblResitExamNumber"), Label)
                            Dim lblResitCurriculum As Label = DirectCast(rp.FindControl("lblResitCurriculum"), Label)
                            Dim lblResitSessionID As Label = DirectCast(rp.FindControl("lblResitSessionID"), Label)
                            Dim sDisplayExamNumber As String = Database & "..spDisplayExamNumberBeforePublishDate__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                                ",@CurriculumID=" & lblResitCurriculum.Text & ",@SessionID=" & lblResitSessionID.Text
                            Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sDisplayExamNumber, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If IsDisplayExamNumber = True Then
                                lblResitExamNumber.Visible = True
                            Else
                                lblResitExamNumber.Visible = False
                            End If
                        Next
                    Else
                        rpResitAssessmentDetails.Visible = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rptSession_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    CourseClick(e)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rdoNextAcadmicCycle_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoNextAcadmicCycle.CheckedChanged
            Try
                LoadNextAcademicCycle()
                LoadEnrolledCoursesGrid()
                LoadEnrolledMockExamGrid()
                'LoadRagisteredExamsGrid()
                LoadRagisteredInterimAssessmentsGrid()
                LoadRagisteredResitAssessmentsGrid()
                DisplayLocationOnIntrim()

                LoadCurriculumDetails()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rdoCurrentAcademicCycle_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoCurrentAcademicCycle.CheckedChanged
            Try
                LoadHeaderText()
                LoadEnrolledCoursesGrid()
                LoadEnrolledMockExamGrid()
                'LoadRagisteredExamsGrid()
                LoadRagisteredInterimAssessmentsGrid()
                LoadRagisteredResitAssessmentsGrid()
                DisplayLocationOnIntrim()

                LoadCurriculumDetails()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnAlternativeOk_Click(sender As Object, e As System.EventArgs) Handles btnAlternativeOk.Click
            Try
                radAlternativeLocation.VisibleOnPageLoad = False
                Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Handles the course link click event
        ''' </summary>
        Private Sub CourseClick(ByVal e As RepeaterCommandEventArgs)
            If e.CommandName = "CourseClick" Then
                Dim classId As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument)
                Dim academicCycleId As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(ViewState("AcademicCycleID"))
                Dim data As String = classId & ";" & academicCycleId
                Response.Redirect(CourseDetailsURL + "?ClassID=" & System.Web.HttpUtility.UrlEncode(classId) & "&AcademicCycleID=" & System.Web.HttpUtility.UrlEncode(academicCycleId), True)
            End If
        End Sub


    End Class
End Namespace
