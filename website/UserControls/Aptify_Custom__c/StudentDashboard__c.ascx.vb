'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Pradip Chavhan              09/03/2015                        Student Dashboard Page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports Aptify.Framework.Web
Imports Telerik.Web.UI
Imports System.Data
Imports System.IO
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.ProcessPipeline ''Redmine 20996 Added by Harish 

Partial Class UserControls_Aptify_Custom__c_StudentDashboard__c
    Inherits eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_StudentDiaryEntryPage As String = "StudentDiaryEntryPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_StudentDiaryViewPage As String = "StudentDiaryViewPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CADIaryGuidelinesPage As String = "CADIaryGuidelinesPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CAIUpdatesPage As String = "CAIUpdatesPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MentorReviewsPage As String = "MentorReviewsPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MembershipPage As String = "MembershipPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_RequestFinalReviewPage As String = "RequestFinalReviewPage"

    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage As String = "MentorDashboardPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage As String = "FirmAdminDashboardPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage"

    Dim sErrorMessage As String = String.Empty
    Dim sSQL As String = String.Empty
    Dim TotalLeave As Integer = 0
    Dim StudentID As Integer = 0
#Region "Student Dashboard Specific Properties"
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

    Public Overridable Property StudentDiaryEntryPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDiaryEntryPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDiaryEntryPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDiaryEntryPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property


    Public Overridable Property StudentDiaryViewPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDiaryViewPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDiaryViewPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDiaryViewPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property CADIaryGuidelinesPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CADIaryGuidelinesPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CADIaryGuidelinesPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CADIaryGuidelinesPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property


    Public Overridable Property CAIUpdatesPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CAIUpdatesPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CAIUpdatesPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CAIUpdatesPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property


    Public Overridable Property MentorReviewsPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorReviewsPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorReviewsPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorReviewsPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property


    Public Overridable Property MembershipPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MembershipPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MembershipPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MembershipPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property RequestFinalReviewPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_RequestFinalReviewPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_RequestFinalReviewPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_RequestFinalReviewPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property


    Public Overridable Property MentorDashboardPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property FirmAdminDashboardPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property ReportPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Protected Overrides Sub SetProperties()
        If String.IsNullOrEmpty(LoginPage) Then
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
        End If
        If String.IsNullOrEmpty(StudentDiaryEntryPage) Then
            StudentDiaryEntryPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDiaryEntryPage)
        End If
        If String.IsNullOrEmpty(StudentDiaryViewPage) Then
            StudentDiaryViewPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDiaryViewPage)
        End If
        If String.IsNullOrEmpty(CADIaryGuidelinesPage) Then
            CADIaryGuidelinesPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_CADIaryGuidelinesPage)
        End If
        If String.IsNullOrEmpty(CAIUpdatesPage) Then
            CAIUpdatesPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_CAIUpdatesPage)
        End If
        If String.IsNullOrEmpty(MentorReviewsPage) Then
            MentorReviewsPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MentorReviewsPage)
        End If
        If String.IsNullOrEmpty(MembershipPage) Then
            MembershipPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MembershipPage)
        End If
        If String.IsNullOrEmpty(RequestFinalReviewPage) Then
            RequestFinalReviewPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_RequestFinalReviewPage)
        End If
        If String.IsNullOrEmpty(MentorDashboardPage) Then
            MentorDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage)
        End If

        If String.IsNullOrEmpty(FirmAdminDashboardPage) Then
            FirmAdminDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage)
        End If
        If String.IsNullOrEmpty(ReportPage) Then
            ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
        End If
        grdMandatoryCoreCompetencies.AllowPaging = False
        grdAreasofExperience.AllowPaging = False
        grdHistoryProfileInfo.AllowPaging = False
        grdOutofOffice.AllowPaging = False

        divCompetenciesGuidence1.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.CompetenciesGuidance1__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        divCompetenciesGuidence2.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.CompetenciesGuidance2__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        divCompetenciesGuidence3.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.CompetenciesGuidance3__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        ' divAreaofExpText.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.AreaofExpText__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        divAreaofExpGuidance.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.AreaofExpGuidance__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

    End Sub
#End Region
#Region "Page Event"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SetProperties()
        lnkCADiaryGuidelines.Attributes.Add("onClick", "javascript:window.open('" + CADIaryGuidelinesPage + "');return false;")
        lnkCAIUpdates.Attributes.Add("onClick", "javascript:window.open('" + CAIUpdatesPage + "');return false;")
        'reqtxtQuestion.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.QuestionValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        If (Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "md" Or Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "fd") And Request.QueryString("Page") IsNot Nothing Then
            StudentID = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")).ToString()
            divMenu.Visible = False
            lnkHistoryProfileInfo.Visible = False
            btnMainBack.Visible = True
        Else
            StudentID = User1.PersonID
        End If
        If User1.PersonID < 0 Then
            Session("ReturnToPage") = Request.RawUrl
            Response.Redirect(LoginPage)
        End If
        If Request.Cookies("CADairyOnCenter") Is Nothing Then
            Response.Redirect(LoginPage)
        End If
        If Not IsPostBack Then
            LoadProfileInfo()
            fillCompetancyCategory()
            BindMandatoryCoreCompetencies()
            LoadRegulatedandRequiredExperience()
            BindOutofOfficeDetails()
            LoadProgressSummaryDetails()
            CheckCreateNewDiaryLinkAccess()
            CheckMemberShipRequestLinkAccess()
            LoadOvertimeWorked() 'Added by Kavita Zinage #18048
        End If
    End Sub
#End Region

#Region "Grid Event"
    Protected Sub grdOutofOffice_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdOutofOffice.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            TotalLeave += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LeaveDay"))
            e.Row.CssClass = "GridItemStyleNew"
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = Convert.ToString(TotalLeave)
            'String.Format("{0:c}", TotalLeave)
        End If
    End Sub
#End Region

#Region "Link Button Events"
    Protected Sub lnkModifyDiaryEntry_Click(sender As Object, e As System.EventArgs) Handles lnkModifyDiaryEntry.Click
        Response.Redirect(StudentDiaryViewPage)
    End Sub

    Protected Sub lnkCreateNewDiaryEntry_Click(sender As Object, e As System.EventArgs) Handles lnkCreateNewDiaryEntry.Click
        Response.Redirect(StudentDiaryEntryPage)
    End Sub
    Protected Sub lnkHistoryProfileInfo_Click(sender As Object, e As System.EventArgs) Handles lnkHistoryProfileInfo.Click
        divStartEndDateNote.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.StartDateEndDateNote__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        LoadContractHistory()
        radwindowHistory.VisibleOnPageLoad = True
    End Sub

    Protected Sub ddlAreasOfExp_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAreasOfExp.SelectedIndexChanged
        Try
            lblNoAreasofExperienceCompetencies.Visible = False
            lblNoAreasofExperienceCompetencies.Text = ""
            If ddlAreasOfExp.SelectedIndex <> 0 Then
                BindAreasofExperience()
            Else
                grdAreasofExperience.DataSource = Nothing
                grdAreasofExperience.DataBind()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub lnkViewMentorReview_Click(sender As Object, e As System.EventArgs) Handles lnkViewMentorReview.Click
        'radwindowMentorReview.VisibleOnPageLoad = True
        'LoadMentorReview()
        Response.Redirect(MentorReviewsPage)
    End Sub

    Protected Sub lnkAdmissiontoMembership_Click(sender As Object, e As System.EventArgs) Handles lnkAdmissiontoMembership.Click
        Response.Redirect(MembershipPage)
    End Sub

    Protected Sub lnkCADiaryReport_Click(sender As Object, e As System.EventArgs) Handles lnkCADiaryReport.Click
        Try
            Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("cadiaryreport")
            If (Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "md" Or Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "fd") And Request.QueryString("Page") IsNot Nothing Then
                StudentID = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")).ToString()
            Else
                StudentID = User1.PersonID
            End If
            'Dim strFilePath As String = ReportPage & "?PersonID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(StudentID))) & "&sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName)
            'ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & strFilePath & "' )", True)

            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "CA Diary PD1 Report"))
            Dim rptParam As New AptifyCrystalReport__c
            rptParam.ReportID = ReportID
            rptParam.Param1 = StudentID
            rptParam.SubParam1 = StudentID
            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub lnkRequestforFinalReview_Click(sender As Object, e As System.EventArgs) Handles lnkRequestforFinalReview.Click
        Try
            If ValidateForFinalReviewRecord() Then
                lblReqFinalReview.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.MentorFinalReviewWarning__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radWindowReqFinalReview.VisibleOnPageLoad = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region


#Region "Popup Button Events"
    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        radwindowHistory.VisibleOnPageLoad = False
    End Sub

    Protected Sub btnValidationOK_Click(sender As Object, e As System.EventArgs) Handles btnValidationOK.Click
        radWindowValidation.VisibleOnPageLoad = False
    End Sub

    Protected Sub btnMainBack_Click(sender As Object, e As System.EventArgs) Handles btnMainBack.Click
        If Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "md" Then
            Response.Redirect(MentorDashboardPage, False)
        ElseIf Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "fd" Then
            Response.Redirect(FirmAdminDashboardPage, False)
        End If
    End Sub
#End Region


#Region "Mentor Review  Functionality Code"

    Public Function ValidateAddStudentDiaryRecord() As Boolean
        'If CInt(hidECCompanyID.Value) <= 0 Then
        '    lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.NoCompanyLinked__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        '    radWindowValidation.VisibleOnPageLoad = True
        '    radWindowReqMentorReview.VisibleOnPageLoad = False
        '    Return False
        'End If
        If CInt(hidMetorID.Value) <= 0 Then
            lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.NoMentorLinked__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            radWindowValidation.VisibleOnPageLoad = True
            radWindowReqMentorReview.VisibleOnPageLoad = False
            Return False
        End If
        Try
            Dim sSQL As String = Database & "..spValidationforRequestMonthlyreview__c @StudentId=" & StudentID
            Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
            If lID > 0 Then
                lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.MonthlyReviewvalidation__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radWindowValidation.VisibleOnPageLoad = True
                radWindowReqMentorReview.VisibleOnPageLoad = False
                Return False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        radWindowValidation.VisibleOnPageLoad = False
        Return True
    End Function

    Public Function AddStudentDiaryRecord() As Boolean
        Try

            Dim oStudentDiaryGE As AptifyGenericEntityBase
            oStudentDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", -1)
            With oStudentDiaryGE
                .SetValue("StudentID", StudentID)
                .SetValue("MentorID", Convert.ToInt32(hidMetorID.Value))
                .SetValue("Status", "Submitted to Mentor")
                .SetValue("Type", "6 Monthly Review")
                If Convert.ToInt32(hidECCompanyID.Value) > 0 Then
                    .SetValue("CompanyID", Convert.ToInt32(hidECCompanyID.Value))
                End If
                If Convert.ToInt32(hidRouteOfEntry.Value) > 0 Then
                    .SetValue("RouteOfEntry", Convert.ToInt32(hidRouteOfEntry.Value))
                End If
                If oStudentDiaryGE.Save(False, sErrorMessage) Then
                    'added by GM for Redmine #20073
                    Session("CallSubmittedToMentor") = "Yes"
                    'end Redmine #20073
                    Return True
                Else
                    Return False
                End If
            End With
            Return True
        Catch ex As System.Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
        Return True
    End Function

    Protected Sub lnkReqMentorReview_Click(sender As Object, e As System.EventArgs) Handles lnkReqMentorReview.Click
        If ValidateAddStudentDiaryRecord() Then
            lblReviewFromMentor.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.MentorReviewWarning__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            radWindowReqMentorReview.VisibleOnPageLoad = True
        End If
    End Sub

    Protected Sub btnReviewYes_Click(sender As Object, e As System.EventArgs) Handles btnReviewYes.Click
        Try
            'If ValidateAddStudentDiaryRecord() Then
            'Added by GM if condition for Redmine #20073
            If Session("CallSubmittedToMentor") Is Nothing Then
                If StudentDairyAlreadySubmitted("6 Monthly Review") Then 'If condition added by GM for Redmine #20168
                    If AddStudentDiaryRecord() Then
                        radWindowReqMentorReview.VisibleOnPageLoad = False
                        LoadProgressSummaryDetails()

                        SendEmailToMentor() ''Start of the code Added by Harish Redmine. #20996 Email triggered to Mentors 

                    End If
                Else 'else part added for Redmine #20168
                    lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.AlreadyExists")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radWindowValidation.VisibleOnPageLoad = True
                End If
            Else
                radWindowReqMentorReview.VisibleOnPageLoad = False 'Added by GM for Redmine #20073
                LoadProgressSummaryDetails() 'Added by GM for Redmine #20073
            End If

            ' End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''Start of the code Added by Harish Redmine. #20996 Email triggered to Mentors 
    Public Sub SendEmailToMentor()
        Try
            Dim lProcessFlowID As Long
            Dim sProcessFlow As String = "SendEmailToMentorReview__c"
            '' Dim sProcessFlow As String = "NotifyMentorSixMonthReview__c" --Dev

            Dim sSql As String = Database & "..spGetProcessFlowIDFromName__c @Name='" & sProcessFlow & "'"
            Dim oProcessFlowID As Object = DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.UseCache)
            If oProcessFlowID IsNot Nothing AndAlso IsNumeric(oProcessFlowID) Then
                lProcessFlowID = CLng(oProcessFlowID)
                Dim sSqlStudent As String = Database & "..spGetMentorIDbyStudentID__c @StudentID=" & StudentID
                Dim MentorID As Object = DataAction.ExecuteScalar(sSqlStudent)
                Dim context As New AptifyContext
                context.Properties.AddProperty("MentorID", Convert.ToInt32(MentorID))
                Dim oResult As ProcessFlowResult
                oResult = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
                If Not oResult.IsSuccess Then
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New Exception("Process flow To send Notification. Please refer event viewer for more details."))

                End If
            Else
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New Exception("Message Template to send the email to mentor is not found in the system."))
            End If

        Catch ex As ArgumentException
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''End of the code Added by Harish Redmine. #20996 Email triggered to Mentors

    Protected Sub btnReviewNo_Click(sender As Object, e As System.EventArgs) Handles btnReviewNo.Click
        radWindowReqMentorReview.VisibleOnPageLoad = False
    End Sub

#End Region


#Region "Submit Query to CAI"

    Protected Sub lnkSubmitQuerytoCAI_Click(sender As Object, e As System.EventArgs) Handles lnkSubmitQuerytoCAI.Click
        fillQuestionCategory()
        radWindowSubmitaQuerytoCAI.VisibleOnPageLoad = True
        ddlQuestionCategory.SelectedIndex = 0
        txtQuestion.Text = ""
    End Sub

    Protected Sub btnSubmitQuestion_Click(sender As Object, e As System.EventArgs) Handles btnSubmitQuestion.Click
        Try
            If ValidateQuestion() Then
                If AddCaiQuestion() Then
                    radWindowSubmitaQuerytoCAI.VisibleOnPageLoad = False
                    radWindowValidation.VisibleOnPageLoad = True
                    lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.QuestionSuccessMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnCancelQuestion_Click(sender As Object, e As System.EventArgs) Handles btnCancelQuestion.Click
        radWindowSubmitaQuerytoCAI.VisibleOnPageLoad = False
    End Sub

    Private Function ValidateQuestion() As Boolean
        If ddlQuestionCategory.SelectedIndex = 0 Then
            ddlQuestionCategory.Focus()
            radWindowValidation.VisibleOnPageLoad = True
            lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.QuestionCategoryValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            Return False
        End If
        If txtQuestion.Text.Trim() = "" Then
            txtQuestion.Focus()
            radWindowValidation.VisibleOnPageLoad = True
            lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.QuestionValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            'reqtxtQuestion.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.QuestionValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            Return False
        End If
        Return True
    End Function


    Private Sub fillQuestionCategory()
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetActiveCAQuestionCategories__c"
            Dim dt As DataTable = DataAction.GetDataTable(sSQL, CommandType.StoredProcedure)
            ddlQuestionCategory.DataSource = dt
            ddlQuestionCategory.DataTextField = "Name"
            ddlQuestionCategory.DataValueField = "ID"
            ddlQuestionCategory.DataBind()
            ddlQuestionCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Public Function AddCaiQuestion() As Boolean
        Try
            Dim oCAStudentQuestionsGE As AptifyGenericEntityBase
            oCAStudentQuestionsGE = Me.AptifyApplication.GetEntityObject("CAQuestions__c", -1)
            With oCAStudentQuestionsGE
                .SetValue("PersonID", StudentID)
                .SetValue("QuestionDescription", txtQuestion.Text.Trim())
                .SetValue("Status", "Pending")
                .SetValue("CAQuestionCategoryID", CInt(ddlQuestionCategory.SelectedValue))
                If oCAStudentQuestionsGE.Save(False, sErrorMessage) Then
                    Return True
                Else
                    Return False
                End If
            End With
            Return True
        Catch ex As System.Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
        Return True
    End Function

#End Region

#Region "Final Review Code"
    Public Function ValidateForFinalReviewRecord() As Boolean
        If CInt(hidMetorID.Value) <= 0 Then
            lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.NoMentorLinked__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            radWindowValidation.VisibleOnPageLoad = True
            radWindowReqMentorReview.VisibleOnPageLoad = False
            Return False
        End If
        Try
            Dim sSQL As String = Database & "..spValidationforRequestFinalreview__c @StudentId=" & StudentID
            Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
            If lID > 0 Then
                lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.FinalReviewvalidation__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radWindowValidation.VisibleOnPageLoad = True
                radWindowReqMentorReview.VisibleOnPageLoad = False
                Return False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        radWindowValidation.VisibleOnPageLoad = False
        Return True
    End Function


    Public Function AddFinalReviewRecord() As Boolean
        Try

            Dim oStudentDiaryGE As AptifyGenericEntityBase
            oStudentDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", -1)
            With oStudentDiaryGE
                .SetValue("StudentID", StudentID)
                .SetValue("MentorID", Convert.ToInt32(hidMetorID.Value))
                .SetValue("Status", "Submitted to Mentor")
                .SetValue("Type", "Final Review")
                If Convert.ToInt32(hidECCompanyID.Value) > 0 Then
                    .SetValue("CompanyID", Convert.ToInt32(hidECCompanyID.Value))
                End If
                If Convert.ToInt32(hidRouteOfEntry.Value) > 0 Then
                    .SetValue("RouteOfEntry", Convert.ToInt32(hidRouteOfEntry.Value))
                End If
                If oStudentDiaryGE.Save(False, sErrorMessage) Then
                    Return True
                Else
                    Return False
                End If
            End With
            Return True
        Catch ex As System.Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
        Return True
    End Function

    Protected Sub btnReqFinalReviewYes_Click(sender As Object, e As System.EventArgs) Handles btnReqFinalReviewYes.Click
        Try
            If StudentDairyAlreadySubmitted("Final Review") Then 'If condition added by GM for Redmine #20168
                If AddFinalReviewRecord() Then
                    radWindowReqFinalReview.VisibleOnPageLoad = False
                    LoadProgressSummaryDetails()
                End If
            Else
                radWindowReqFinalReview.VisibleOnPageLoad = False
                lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.AlreadyExists")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radWindowValidation.VisibleOnPageLoad = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnReqFinalReviewNo_Click(sender As Object, e As System.EventArgs) Handles btnReqFinalReviewNo.Click
        radWindowReqFinalReview.VisibleOnPageLoad = False
    End Sub
#End Region

#Region "Private Functions"
    'Code To Display Personal Details
    Private Sub LoadProfileInfo()
        Dim dtProfileInfo As Data.DataTable
        Dim d1 As DateTime
        Try
            sSQL = String.Empty
            sSQL = Database & "..spgetStudentProfileInfomationStudentDashboard__c @StudentId=" & StudentID
            dtProfileInfo = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If dtProfileInfo IsNot Nothing Then
                If dtProfileInfo.Rows.Count > 0 Then
                    lblStudentName.Text = dtProfileInfo.Rows(0)("StudentNo").ToString()
                    lblCompanyName.Text = dtProfileInfo.Rows(0)("CompanyName").ToString()
                    hidCompany.Value = dtProfileInfo.Rows(0)("CompanyID").ToString()
                    lblMentorName.Text = dtProfileInfo.Rows(0)("MentorName").ToString()
                    hidMetorID.Value = dtProfileInfo.Rows(0)("MentorID").ToString()
                    hidECCompanyID.Value = dtProfileInfo.Rows(0)("ECCompanyID").ToString()
                    If dtProfileInfo.Rows(0)("IntakeDate__c").ToString().Trim() <> "" Then
                        d1 = Convert.ToDateTime(dtProfileInfo.Rows(0)("IntakeDate__c").ToString().Trim())
                        lblRegDate.Text = (New Date(d1.Year, d1.Month, d1.Day).ToShortDateString())
                    End If

                    If dtProfileInfo.Rows(0)("CompletionDate__c").ToString().Trim() <> "" Then
                        d1 = Convert.ToDateTime(dtProfileInfo.Rows(0)("CompletionDate__c").ToString().Trim())
                        lblDateOfCompletion.Text = (New Date(d1.Year, d1.Month, d1.Day).ToShortDateString())
                    End If
                    lblRouteOfEntry.Text = dtProfileInfo.Rows(0)("RouteOfEntry").ToString()
                    hidRouteOfEntry.Value = dtProfileInfo.Rows(0)("RouteOfEntryID").ToString()
                    ' lblReqExperienceReqdays.Text = Convert.ToString(dtProfileInfo.Rows(0)("RequiredExperience"))
                    'lblTotalExperienceReqdays.Text = Convert.ToString(dtProfileInfo.Rows(0)("RequiredExperience"))
                    'Commented column for redmine log #18753
                    'lblMinimumCompanyAuditday.Text = Convert.ToString(dtProfileInfo.Rows(0)("MinimumCompanyAuditWeeks"))
                    'lblMinimumOtherAuditday.Text = Convert.ToString(dtProfileInfo.Rows(0)("MinimumAuditExpWeeks"))
                    If Convert.ToString(dtProfileInfo.Rows(0)("EligibleforFinalReview__c")) = "True" Then
                        lnkRequestforFinalReview.Visible = True
                    Else
                        lnkRequestforFinalReview.Visible = False
                    End If

                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        Finally
        End Try
    End Sub
    'Code to Bind Student Formal Registration
    Private Sub LoadContractHistory()
        Dim dtContractInfo As Data.DataTable
        Try
            sSQL = String.Empty
            sSQL = Database & "..spgetContractHistoryStudentDashboard__c"
            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@StudentId", SqlDbType.Int, StudentID)
            dtContractInfo = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
            If dtContractInfo IsNot Nothing Then
                If dtContractInfo.Rows.Count > 0 Then
                    grdHistoryProfileInfo.DataSource = dtContractInfo
                    grdHistoryProfileInfo.DataBind()
                Else
                    grdHistoryProfileInfo.DataSource = Nothing
                    grdHistoryProfileInfo.DataBind()
                    divStartEndDateNote.Visible = False
                    lblHistoryMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.NoFormalRegistration__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Else
                grdHistoryProfileInfo.DataSource = Nothing
                grdHistoryProfileInfo.DataBind()
                divStartEndDateNote.Visible = False
                lblHistoryMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.NoFormalRegistration__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        Finally
        End Try
    End Sub

    'Code To Bind MandatoryCoreCompetencies
    Private Sub BindMandatoryCoreCompetencies()
        Dim dtCoreCompetencies As Data.DataTable
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetMandatoryCoreCompetencies__c @StudentId=" & StudentID
            dtCoreCompetencies = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If dtCoreCompetencies IsNot Nothing Then
                If dtCoreCompetencies.Rows.Count > 0 Then
                    grdMandatoryCoreCompetencies.DataSource = dtCoreCompetencies
                    grdMandatoryCoreCompetencies.DataBind()
                    grdMandatoryCoreCompetencies.HeaderRow.CssClass = "GridViewHeaderNew"
                    'grdMandatoryCoreCompetencies.HeaderRow.Style.Add("padding-left", "2px")
                    grdMandatoryCoreCompetencies.GridLines = GridLines.Both
                    grdMandatoryCoreCompetencies.HeaderStyle.HorizontalAlign = HorizontalAlign.Left
                    grdMandatoryCoreCompetencies.HeaderRow.HorizontalAlign = HorizontalAlign.Left
                Else
                    grdMandatoryCoreCompetencies.DataSource = Nothing
                    grdMandatoryCoreCompetencies.DataBind()
                    lblNoMandatoryCoreCompetencies.Visible = True
                    lblNoMandatoryCoreCompetencies.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.NoMandatoryCoreCompetencies__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    divStartEndDateNote.Visible = False
                End If
            Else
                grdMandatoryCoreCompetencies.DataSource = Nothing
                grdMandatoryCoreCompetencies.DataBind()
                lblNoMandatoryCoreCompetencies.Visible = True
                lblNoMandatoryCoreCompetencies.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.NoMandatoryCoreCompetencies__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                divStartEndDateNote.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        Finally
        End Try
    End Sub
    'Code To Set Color for MandatoryCoreCompetencies 

    Public Sub OnMandatoryCoreCompetenciesDataBound(sender As Object, e As EventArgs)

        Try
            For rowIndex As Integer = grdMandatoryCoreCompetencies.Rows.Count - 2 To 0 Step -1
                Dim currentRow As GridViewRow = grdMandatoryCoreCompetencies.Rows(rowIndex)
                Dim previousRow As GridViewRow = grdMandatoryCoreCompetencies.Rows(rowIndex + 1)
                'For i As Integer = 0 To currentRow.Cells.Count - 1
                If currentRow.Cells(0).Text = previousRow.Cells(0).Text Then
                    If previousRow.Cells(0).RowSpan < 2 Then
                        currentRow.Cells(0).RowSpan = 2
                        ' currentRow.Cells(0).CssClass = "FirstCellText"
                    Else
                        currentRow.Cells(0).RowSpan = previousRow.Cells(0).RowSpan + 1
                        ' currentRow.Cells(0).CssClass = "FirstCellText"
                    End If
                    previousRow.Cells(0).Visible = False
                End If
                If CInt(currentRow.Cells(3).Text) > 0 Then
                    currentRow.Cells(3).BackColor = Drawing.Color.Green
                End If
                If CInt(currentRow.Cells(4).Text) > 0 Then
                    currentRow.Cells(4).BackColor = Drawing.Color.Green
                End If
                If CInt(currentRow.Cells(5).Text) > 0 Then
                    currentRow.Cells(5).BackColor = Drawing.Color.Green
                End If
                If previousRow.Cells(3).Text.Trim() <> "" Then
                    If CInt(previousRow.Cells(3).Text) > 0 Then
                        previousRow.Cells(3).BackColor = Drawing.Color.Green
                    End If
                End If

                If previousRow.Cells(4).Text.Trim() <> "" Then
                    If CInt(previousRow.Cells(4).Text) > 0 Then
                        previousRow.Cells(4).BackColor = Drawing.Color.Green
                    End If
                End If

                If previousRow.Cells(5).Text.Trim() <> "" Then
                    If CInt(previousRow.Cells(5).Text) > 0 Then
                        previousRow.Cells(5).BackColor = Drawing.Color.Green
                    End If
                End If
                currentRow.Cells(3).Text = ""
                currentRow.Cells(4).Text = ""
                currentRow.Cells(5).Text = ""
                previousRow.Cells(3).Text = ""
                previousRow.Cells(4).Text = ""
                previousRow.Cells(5).Text = ""
                currentRow.CssClass = "GridItemStyleNew"
                previousRow.CssClass = "GridItemStyleNew"
                currentRow.Cells(1).CssClass = "GridCellStyleNew"
                previousRow.Cells(1).CssClass = "GridCellStyleNew"
                currentRow.Cells(3).CssClass = "GridCellStyleNew"
                currentRow.Cells(4).CssClass = "GridCellStyleNew"
                previousRow.Cells(3).CssClass = "GridCellStyleNew"
                previousRow.Cells(4).CssClass = "GridCellStyleNew"
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    ''Code To Bind AreasofExperience Grid
    Private Sub BindAreasofExperience()
        Dim dtAreasofExperience As Data.DataTable
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetAreasofExperienceCompetencies__c @StudentId=" & StudentID & ",@CompetancyCategoryId=" & CInt(ddlAreasOfExp.SelectedValue).ToString()
            dtAreasofExperience = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If dtAreasofExperience IsNot Nothing Then
                If dtAreasofExperience.Rows.Count > 0 Then
                    grdAreasofExperience.DataSource = dtAreasofExperience
                    grdAreasofExperience.DataBind()
                    grdAreasofExperience.HeaderRow.CssClass = "GridViewHeaderNew"
                    grdAreasofExperience.GridLines = GridLines.Both
                    grdAreasofExperience.HeaderStyle.HorizontalAlign = HorizontalAlign.Left
                    grdAreasofExperience.HeaderRow.HorizontalAlign = HorizontalAlign.Left

                Else
                    grdAreasofExperience.DataSource = Nothing
                    grdAreasofExperience.DataBind()
                    lblNoAreasofExperienceCompetencies.Visible = True
                    lblNoAreasofExperienceCompetencies.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.NoAreaofExperiencCompetencies__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Else
                grdAreasofExperience.DataSource = Nothing
                grdAreasofExperience.DataBind()
                lblNoAreasofExperienceCompetencies.Visible = True
                lblNoAreasofExperienceCompetencies.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDashBoard.NoAreaofExperiencCompetencies__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        Finally
        End Try
    End Sub
    ''Code TO Set Color to cells for  AreasofExperience Grid
    Public Sub OnAreasofExperienceDataBound(sender As Object, e As EventArgs)
        Try
            For rowIndex As Integer = grdAreasofExperience.Rows.Count - 1 To 0 Step -1
                Dim currentRow As GridViewRow = grdAreasofExperience.Rows(rowIndex)
                If CInt(currentRow.Cells(2).Text) > 0 Then
                    currentRow.Cells(2).BackColor = Drawing.Color.Blue
                End If
                If CInt(currentRow.Cells(3).Text) > 0 Then
                    currentRow.Cells(3).BackColor = Drawing.Color.Blue
                End If
                If CInt(currentRow.Cells(4).Text) > 0 Then
                    currentRow.Cells(4).BackColor = Drawing.Color.Blue
                End If
                currentRow.Cells(2).Text = ""
                currentRow.Cells(3).Text = ""
                currentRow.Cells(4).Text = ""
                currentRow.CssClass = "GridItemStyleNew"
                currentRow.Cells(2).CssClass = "GridCellStyleNew"
                currentRow.Cells(3).CssClass = "GridCellStyleNew"
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    'Code To Fill Competancy Category
    Private Sub fillCompetancyCategory()
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetAreaOfExpertiseCompetancyCategory__c"
            Dim dt As DataTable = DataAction.GetDataTable(sSQL, CommandType.StoredProcedure)
            ddlAreasOfExp.DataSource = dt
            ddlAreasOfExp.DataTextField = "Name"
            ddlAreasOfExp.DataValueField = "ID"
            ddlAreasOfExp.DataBind()
            ddlAreasOfExp.Items.Insert(0, "Select")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    'Code To Display Regulated Experience Details
    Private Sub LoadRegulatedandRequiredExperience()
        Dim dtExperience As Data.DataTable
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetRegulatedExperienceInPracticeEnvironment__c @StudentId=" & StudentID
            dtExperience = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If dtExperience.Rows.Count > 0 AndAlso dtExperience IsNot Nothing Then
                'Added for redmine log #18753
                lblStatutoryAuditDay.Text = CInt(Convert.ToString(dtExperience.Rows(0)("StatutoryAuditDays"))).ToString()
                lblCompanyAuditday.Text = CInt(Convert.ToString(dtExperience.Rows(0)("CompanyAuditDays"))).ToString()
                lblOtherAuditday.Text = CInt(Convert.ToString(dtExperience.Rows(0)("OtherAuditDays"))).ToString()
                'Added for redmine log #18753
                'lblTotalAuditday.Text = (CInt(Convert.ToString(dtExperience.Rows(0)("CompanyAuditDays"))) + CInt(Convert.ToString(dtExperience.Rows(0)("OtherAuditDays"))) + CInt(Convert.ToString(dtExperience.Rows(0)("StatutoryAuditDays")))).ToString()
                ' above line commented by GM and added new line 20233
                lblTotalAuditday.Text = (CInt(Convert.ToString(dtExperience.Rows(0)("StatutoryAuditDays"))) + CInt(Convert.ToString(dtExperience.Rows(0)("OtherAuditDays"))))
                lblTotalAuditNIday.Text = (CInt(Convert.ToString(dtExperience.Rows(0)("CompanyAuditDays"))) + CInt(Convert.ToString(dtExperience.Rows(0)("OtherAuditNIDays"))))
                lblPriorWorkExperienceToDate.Text = Convert.ToString(dtExperience.Rows(0)("PriorExpDays"))
                lblReqExperienceToDate.Text = Convert.ToString(dtExperience.Rows(0)("ReqExpDays"))
                lblTotalExperienceToDate.Text = CInt(lblPriorWorkExperienceToDate.Text) + CInt(lblReqExperienceToDate.Text)
                ' lblPriorWorkExperienceReqdays.Text = Convert.ToString(dtExperience.Rows(0)("RequiredExperience"))
                lblReqExperienceReqdays.Text = Convert.ToString(dtExperience.Rows(0)("RequiredExperience"))
                lblTotalExperienceReqdays.Text = Convert.ToString(dtExperience.Rows(0)("RequiredExperience"))
                lblTotalExperienceReqdays.Text = Convert.ToString(dtExperience.Rows(0)("RequiredExperience"))
                'added by GM for Redmine #20233
                lblOtherAuditNIday.Text = CInt(Convert.ToString(dtExperience.Rows(0)("OtherAuditNIDays"))).ToString()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        Finally
        End Try
    End Sub

    'Code To Bind Leave Details
    Private Sub BindOutofOfficeDetails()
        Dim dtOutofOfficeDetails As Data.DataTable
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetOutofOfficeDetailsStudentDashboard__c"
            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@StudentId", SqlDbType.Int, StudentID)
            dtOutofOfficeDetails = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
            If dtOutofOfficeDetails.Rows.Count > 0 AndAlso dtOutofOfficeDetails IsNot Nothing Then
                grdOutofOffice.DataSource = dtOutofOfficeDetails
                grdOutofOffice.DataBind()
                grdOutofOffice.FooterStyle.CssClass = "GridFooterNew"
                grdOutofOffice.GridLines = GridLines.None
                grdOutofOffice.BorderColor = Drawing.Color.White
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        Finally
        End Try
    End Sub

    'Code To Display Progress Summary Details Section
    Private Sub LoadProgressSummaryDetails()
        Dim dtProgressDetails As Data.DataTable
        Dim d1 As DateTime
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetProgressSummaryDetailsStudentDashboard__c"
            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@StudentId", SqlDbType.Int, StudentID)
            dtProgressDetails = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
            If dtProgressDetails.Rows.Count > 0 AndAlso dtProgressDetails IsNot Nothing Then
                lblInProgressDiaryEntry.Text = Convert.ToString(dtProgressDetails.Rows(0)("InProgressCount"))
                lblMentorDiaryEntry.Text = Convert.ToString(dtProgressDetails.Rows(0)("MentorCount"))
                lblReviewDiaryEntry.Text = Convert.ToString(dtProgressDetails.Rows(0)("LockedCount"))
                lblTotalDiaryEntry.Text = Convert.ToString(dtProgressDetails.Rows(0)("TotalCount"))
                lblNumbeofMentorReviews.Text = Convert.ToString(dtProgressDetails.Rows(0)("MentorReviewCount"))
                If Convert.ToString(dtProgressDetails.Rows(0)("LastReviewDate")).Trim() <> "" Then
                    d1 = Convert.ToDateTime(dtProgressDetails.Rows(0)("LastReviewDate").ToString().Trim())
                    lblDateofLastReview.Text = (New Date(d1.Year, d1.Month, d1.Day).ToShortDateString())
                End If

                If Convert.ToString(dtProgressDetails.Rows(0)("NextReviewDate")).Trim() <> "" Then
                    d1 = Convert.ToDateTime(dtProgressDetails.Rows(0)("NextReviewDate").ToString().Trim())
                    lblDateofNextReview.Text = (New Date(d1.Year, d1.Month, d1.Day).ToShortDateString())
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        Finally
        End Try
    End Sub

    'Code To Create New Diary Entry
    Private Sub CheckCreateNewDiaryLinkAccess()
        Try
            sSQL = String.Empty
            sSQL = Database & "..spCheckNewDairyEntryLinkAccess__c @StudentId=" & StudentID
            Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
            If lID > 0 Then
                lnkCreateNewDiaryEntry.Visible = True
            Else
                lnkCreateNewDiaryEntry.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    'Code To Check Membership Link Access
    Private Sub CheckMemberShipRequestLinkAccess()
        Try
            sSQL = String.Empty
            sSQL = Database & "..spCheckMemberShipRequestLinkAccess__c @StudentId=" & StudentID
            Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
            If lID > 0 Then
                lnkAdmissiontoMembership.Visible = True
            Else
                lnkAdmissiontoMembership.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    'Code To Display Overtime Worked #18048
    Private Sub LoadOvertimeWorked()
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetAllDiaryOvertimeWorkedCADiaryReport__c @StudentId=" & StudentID
            Dim iOverWorked As Integer = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            lblOverTimeWorked.Text = iOverWorked
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        Finally
        End Try
    End Sub
    'Ends here

#End Region

    ''' <summary>
    ''' Redmine #20168/updated for Redmine #20302
    ''' </summary>
    ''' <returns></returns>
    Private Function StudentDairyAlreadySubmitted(ByVal Type As String) As Boolean
        Try
            Dim sSql As String = "..spCheckAlreadyCADairySubmitted__c @StudentID=" & StudentID & ",@Type='" & Type & "'"
            Dim iCnt As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql))
            If iCnt > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function

End Class
