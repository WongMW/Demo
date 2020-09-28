'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Kavita Zinage               09/07/2015                        Mentor Dashboard Page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Imports Aptify.Framework.Web
Imports System.IO

Partial Class UserControls_Aptify_Custom__c_MentorDashboard__c
    Inherits eBusiness.BaseUserControlAdvanced

    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_SubmitFinalReviewPage As String = "SubmitFinalReview"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CreateFinalReviewPage As String = "CreateSubmitFinalReview"

    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CADIaryGuidelinesPage As String = "CADIaryGuidelinesPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CAIUpdatesPage As String = "CAIUpdatesPage"

    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CreateSixMonthlyMentorReviewPage As String = "SixMonthlyReview"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage As String = "StudentDashboardPage"

    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_StudentsDiaryEntrySummaryPage As String = "StudentDiaryEntrySummaryPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage As String = "ReportPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_AllDiaryEntriesPDF As String = "AllDiaryEntriesPDF"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CAISTUDENTFORMPDF As String = "CAIStudentFormPDF"
    ''Added By Pradip
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage As String = "SixMonthlySummaryPage"

    Dim sErrorMessage As String = String.Empty
    Dim sSQL As String = String.Empty
#Region "Mentor Dashboard Specific Properties"

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

    Public Overridable Property SixMonthlyReview() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CreateSixMonthlyMentorReviewPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CreateSixMonthlyMentorReviewPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CreateSixMonthlyMentorReviewPage) = Me.FixLinkForVirtualPath(value)
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

    Public Overridable Property SubmitFinalReview() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_SubmitFinalReviewPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_SubmitFinalReviewPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_SubmitFinalReviewPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property StudentDashboardPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property CreateSubmitFinalReview() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CreateFinalReviewPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CreateFinalReviewPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CreateFinalReviewPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property AllDiaryEntriesPDF() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AllDiaryEntriesPDF) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AllDiaryEntriesPDF))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AllDiaryEntriesPDF) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property CAIStudentFormPDF() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CAISTUDENTFORMPDF) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CAISTUDENTFORMPDF))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CAISTUDENTFORMPDF) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    ''Added BY Pradip Do Not Delete
    Public Overridable Property SixMonthlySummaryPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property StudentDiaryEntrySummaryPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentsDiaryEntrySummaryPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentsDiaryEntrySummaryPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentsDiaryEntrySummaryPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property ReportPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Protected Overrides Sub SetProperties()
        If String.IsNullOrEmpty(LoginPage) Then
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
        End If
        If String.IsNullOrEmpty(SixMonthlyReview) Then
            SixMonthlyReview = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_CreateSixMonthlyMentorReviewPage)
        End If
        If String.IsNullOrEmpty(CADIaryGuidelinesPage) Then
            CADIaryGuidelinesPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_CADIaryGuidelinesPage)
        End If
        If String.IsNullOrEmpty(CAIUpdatesPage) Then
            CAIUpdatesPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_CAIUpdatesPage)
        End If

        If String.IsNullOrEmpty(SixMonthlySummaryPage) Then
            SixMonthlySummaryPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage)
        End If
        If String.IsNullOrEmpty(SubmitFinalReview) Then
            SubmitFinalReview = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_SubmitFinalReviewPage)
        End If
        If String.IsNullOrEmpty(StudentDashboardPage) Then
            StudentDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage)
        End If
        If String.IsNullOrEmpty(CreateSubmitFinalReview) Then
            CreateSubmitFinalReview = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_CreateFinalReviewPage)
        End If
        If String.IsNullOrEmpty(AllDiaryEntriesPDF) Then
            AllDiaryEntriesPDF = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_AllDiaryEntriesPDF)
        End If
        If String.IsNullOrEmpty(CAIStudentFormPDF) Then
            CAIStudentFormPDF = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_CAISTUDENTFORMPDF)
        End If
        If String.IsNullOrEmpty(StudentDiaryEntrySummaryPage) Then
            StudentDiaryEntrySummaryPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_StudentsDiaryEntrySummaryPage)
        End If
        If String.IsNullOrEmpty(ReportPage) Then
            ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage)
        End If
    End Sub
#End Region

#Region "Page Event"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SetProperties()

        lnkCADiaryGuidelines.Attributes.Add("onClick", "javascript:window.open('" + CADIaryGuidelinesPage + "');return false;")
        lnkCAIUpdates.Attributes.Add("onClick", "javascript:window.open('" + CAIUpdatesPage + "');return false;")

        If User1.PersonID < 0 Then
            Session("ReturnToPage") = Request.RawUrl
            Response.Redirect(LoginPage)
        End If
        If Not IsPostBack Then
            divGainingExperience.Visible = True
            divGainedExperience.Visible = True
            divsixmonthlyreview.Visible = True
            lblMentorName.Text = Convert.ToString(User1.PersonID)
            LoadStudentFinalReviewDetails()
            LoadStudentSixMonthlyDetails()
            BindGainingExperience()
            BindGainedExperience()
            SetStatisticsValue()
        End If
    End Sub
#End Region

#Region "Link Button"
    Protected Sub lnkCreateMentorReview_Click(sender As Object, e As System.EventArgs) Handles lnkCreateMentorReview.Click
        Response.Redirect(SixMonthlySummaryPage)
    End Sub

    Protected Sub lnkSubmitFinalReview_Click(sender As Object, e As System.EventArgs) Handles lnkSubmitFinalReview.Click
        Response.Redirect(SubmitFinalReview)
    End Sub

#End Region

#Region "Private Functions"

    ''' <summary>
    ''' Method To Bind Gaining Experience Grid.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindGainingExperience()
        Dim dtGainingExp As Data.DataTable
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetGainingExperienceDetails__c @MentorID=" & User1.PersonID
            dtGainingExp = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If dtGainingExp IsNot Nothing Then
                radGainingExperience.DataSource = dtGainingExp
                radGainingExperience.DataBind()
                radGainingExperience.Visible = True
                '    lblGainingExp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FirmAdminDashBoard.NoGainingExp__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' Meyhod To Bind Gained Experience Grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindGainedExperience()
        Dim dtGainedExp As Data.DataTable
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetGainedExperienceDetails__c @MentorID=" & User1.PersonID
            dtGainedExp = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If dtGainedExp IsNot Nothing Then
                radGainedExperience.DataSource = dtGainedExp
                radGainedExperience.DataBind()
                '    lblGainedExp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FirmAdminDashBoard.NoGainedExp__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' Load Student 6 monthly review data 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStudentSixMonthlyDetails()
        Try
            Dim sSql As String = Database & "..spGetStudentReuestSixMonthlyReview__c @MentorID=" & Convert.ToInt32(User1.PersonID)
            Dim dtSixMonthlyReview As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtSixMonthlyReview Is Nothing Then

                radStudentSixMonthlReview.DataSource = dtSixMonthlyReview
                radStudentSixMonthlReview.DataBind()
                radStudentSixMonthlReview.Visible = True
                '    lblsixmonthlyReview.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorDashBoard.NoSixMontlyReview__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load Student Final review data 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStudentFinalReviewDetails()
        Try
            Dim sSql As String = Database & "..spGetStudentReuestFinalReview__c @MentorID=" & Convert.ToInt32(User1.PersonID)
            Dim dtFinalReview As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtFinalReview Is Nothing Then
                radStudentFinalReview.DataSource = dtFinalReview
                radStudentFinalReview.DataBind()
                radStudentFinalReview.Visible = True
                '    lblFinalReviewError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorDashBoard.NoFinalReview__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' To set Statitics Value
    ''' </summary>
    Private Sub SetStatisticsValue()
        Try
            Dim sSQL As String = Database & "..spGetCountforRquesttoMembership__c @MentorID=" & Convert.ToInt32(User1.PersonID)
            Dim iMembRequestCount As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))

            Dim sSQLGaineExp As String = Database & ".. spGetGainedExperienceCount__c @MentorID=" & Convert.ToInt32(User1.PersonID)
            Dim iGainedExpCount As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQLGaineExp))

            Dim sSQLFinalReview As String = Database & ".. spGetFinalReviewCount__c @MentorID=" & Convert.ToInt32(User1.PersonID)
            Dim iFinalReviewCount As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQLFinalReview))

            'Dim iGainedExpCount As Integer = (radGainedExperience.Items.Count() - iMembRequestCount)

            lblGainingExperience.Text = Convert.ToString(radGainingExperience.Items.Count())
            lblExperienceGained.Text = Convert.ToString(iGainedExpCount)
            lblFinalReview.Text = Convert.ToString(iFinalReviewCount)
            lblAdmittedtoMembership.Text = Convert.ToString(iMembRequestCount)
            lblTotalAssignedStudents.Text = Convert.ToString(radGainingExperience.Items.Count() + iGainedExpCount + iFinalReviewCount + iMembRequestCount)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Submit Query to CAI"
    ''' <summary>
    ''' Handles Button Click
    ''' </summary>
    Protected Sub lnkSubmitQuerytoCAI_Click(sender As Object, e As System.EventArgs) Handles lnkSubmitQuerytoCAI.Click
        Try
            fillQuestionCategory()
            radWindowSubmitaQuerytoCAI.VisibleOnPageLoad = True
            ddlQuestionCategory.SelectedIndex = 0
            txtQuestion.Text = ""
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Handles Button Click
    ''' </summary>
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
    ''' <summary>
    ''' Handles Button Click
    ''' </summary>
    Protected Sub btnCancelQuestion_Click(sender As Object, e As System.EventArgs) Handles btnCancelQuestion.Click
        Try
            radWindowSubmitaQuerytoCAI.VisibleOnPageLoad = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Validates Question
    ''' </summary>
    Private Function ValidateQuestion() As Boolean
        Try
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
                Return False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

        Return True
    End Function

    ''' <summary>
    ''' fills Question Category
    ''' </summary>
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

    ''' <summary>
    ''' Function To Add New Question To CAI
    ''' </summary> 
    ''' <remarks></remarks>

    Public Function AddCaiQuestion() As Boolean
        Try
            Dim oCAStudentQuestionsGE As AptifyGenericEntityBase
            oCAStudentQuestionsGE = Me.AptifyApplication.GetEntityObject("CAQuestions__c", -1)
            With oCAStudentQuestionsGE
                .SetValue("PersonID", User1.PersonID)
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

#Region "Popup Button Events"
    ''' <summary>
    ''' Handles Button Click
    ''' </summary>
    Protected Sub btnValidationOK_Click(sender As Object, e As System.EventArgs) Handles btnValidationOK.Click
        Try
            radWindowValidation.VisibleOnPageLoad = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

#Region "Grid Event"

    ''' <summary>
    ''' Redirect To Create 6 Monthly Page code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub radStudentSixMonthlReview_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radStudentSixMonthlReview.ItemCommand
        Try

            If e.CommandName = "PersonName" Then
                Dim sPageName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("md")
                Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                Dim strFilePath As String = SixMonthlyReview & "?Page=" & System.Web.HttpUtility.UrlEncode(sPageName) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID)
                Response.Redirect(strFilePath, False)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub radStudentSixMonthlReview_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radStudentSixMonthlReview.NeedDataSource
        Try
            LoadStudentSixMonthlyDetails()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub radStudentSixMonthlReview_PreRender(sender As Object, e As System.EventArgs) Handles radStudentSixMonthlReview.PreRender
        If radStudentSixMonthlReview.MasterTableView.GetItems(GridItemType.NoRecordsItem).Count > 0 Then
            Dim norecordItem As GridNoRecordsItem = radStudentSixMonthlReview.MasterTableView.GetItems(GridItemType.NoRecordsItem)(0)
            Dim lblnorecordItem As Label = norecordItem.FindControl("lblsixmonthlyReview")
            lblnorecordItem.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorDashBoard.NoSixMontlyReview__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        End If

    End Sub

    ''' <summary>
    ''' Redirect To Create Final Review Page code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub radStudentFinalReview_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radStudentFinalReview.ItemCommand
        Try
            If e.CommandName = "PersonName" Then
                Dim sPageName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("md")
                Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                Dim strFilePath As String = CreateSubmitFinalReview & "?Page=" & System.Web.HttpUtility.UrlEncode(sPageName) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID)
                Response.Redirect(strFilePath, False)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub radStudentFinalReview_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radStudentFinalReview.NeedDataSource
        Try
            LoadStudentFinalReviewDetails()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub radStudentFinalReview_PreRender(sender As Object, e As System.EventArgs) Handles radStudentFinalReview.PreRender
        If radStudentFinalReview.MasterTableView.GetItems(GridItemType.NoRecordsItem).Count > 0 Then
            Dim norecordItem As GridNoRecordsItem = radStudentFinalReview.MasterTableView.GetItems(GridItemType.NoRecordsItem)(0)
            Dim lblnorecordItem As Label = norecordItem.FindControl("lblFinalReviewError")
            lblnorecordItem.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorDashBoard.NoFinalReview__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        End If

    End Sub


    Protected Sub radGainingExperience_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radGainingExperience.ItemCommand
        Try
            Dim lnkButton As LinkButton = e.Item.FindControl("lnkSubmittedForReview")
            Dim lnkButtonlkd As LinkButton = e.Item.FindControl("lnkReviewedLocked")
            Dim lnkButtonIP As LinkButton = e.Item.FindControl("lnkInProgress")
            Dim sPageName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("md")
            Select Case e.CommandName
                Case "PersonName"
                    Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                    Dim strFilePath As String = StudentDashboardPage & "?Page=" & System.Web.HttpUtility.UrlEncode(sPageName) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID)
                    Dim aCookie As New HttpCookie("CADairyOnCenter")
                    aCookie.Value = "Yes"
                    Response.Cookies.Add(aCookie)
                    Response.Redirect(strFilePath, False)
                Case "SubmittedForReview"
                    If Convert.ToInt32(lnkButton.Text) <> 0 Then
                        Dim sStatus As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("STM")
                        Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                        Dim strFilePath As String = StudentDiaryEntrySummaryPage & "?Status=" & System.Web.HttpUtility.UrlEncode(sStatus) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID) & "&Page=" & System.Web.HttpUtility.UrlEncode(sPageName)
                        Response.Redirect(strFilePath, False)
                    End If
                Case "ReviewedLocked"
                    If Convert.ToInt32(lnkButtonlkd.Text) <> 0 Then
                        Dim sStatus As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("LKD")
                        Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                        Dim strFilePath As String = StudentDiaryEntrySummaryPage & "?Status=" & System.Web.HttpUtility.UrlEncode(sStatus) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID) & "&Page=" & System.Web.HttpUtility.UrlEncode(sPageName)
                        Response.Redirect(strFilePath, False)
                    End If
                Case "InProgress"
                    If Convert.ToInt32(lnkButtonIP.Text) <> 0 Then
                        Dim sStatus As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("IP")
                        Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                        Dim strFilePath As String = StudentDiaryEntrySummaryPage & "?Status=" & System.Web.HttpUtility.UrlEncode(sStatus) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID) & "&Page=" & System.Web.HttpUtility.UrlEncode(sPageName)
                        Response.Redirect(strFilePath, False)
                    End If
                Case "PDF1"
                    Try
                        'Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("pd1")
                        'Dim strFilePath As String = ReportPage & "?PersonID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(e.CommandArgument))) & "&sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName)
                        'ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & strFilePath & "' )", True)
                        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "CA Diary PD1 Report"))
                        Dim rptParam As New AptifyCrystalReport__c
                        rptParam.ReportID = ReportID
                        rptParam.Param1 = Convert.ToString(e.CommandArgument)
                        rptParam.SubParam1 = Convert.ToString(e.CommandArgument)
                        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                    Catch ex As Exception
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    End Try
                Case "PDF2"
                    Try
                        'Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("pd2")
                        'Dim strFilePath As String = ReportPage & "?PersonID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(e.CommandArgument))) & "&sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName)
                        'ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & strFilePath & "' )", True)
                        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "CA Diary PD2 Report"))
                        Dim rptParam As New AptifyCrystalReport__c
                        rptParam.ReportID = ReportID
                        rptParam.Param1 = Convert.ToString(e.CommandArgument)
                        rptParam.SubParam1 = Convert.ToString(e.CommandArgument)
                        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                    Catch ex As Exception
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    End Try
            End Select
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub radGainingExperience_PreRender(sender As Object, e As System.EventArgs) Handles radGainingExperience.PreRender
        If radGainingExperience.MasterTableView.GetItems(GridItemType.NoRecordsItem).Count > 0 Then
            Dim norecordItem As GridNoRecordsItem = radGainingExperience.MasterTableView.GetItems(GridItemType.NoRecordsItem)(0)
            Dim lblnorecordItem As Label = norecordItem.FindControl("lblNoGainingExp")
            lblnorecordItem.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FirmAdminDashBoard.NoGainingExp__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        End If
    End Sub

    Protected Sub radGainingExperience_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radGainingExperience.NeedDataSource
        Try
            BindGainingExperience()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub radGainedExperience_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radGainedExperience.ItemCommand
        Try

            Dim lnkButton As LinkButton = e.Item.FindControl("lnkSubmittedForReview")
            Dim lnkButtonlkd As LinkButton = e.Item.FindControl("lnkReviewedLocked")
            Dim lnkButtonIP As LinkButton = e.Item.FindControl("lnkInProgress")
            Dim sPageName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("md")
            Select Case e.CommandName
                Case "PersonName"
                    Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                    Dim strFilePath As String = StudentDashboardPage & "?Page=" & System.Web.HttpUtility.UrlEncode(sPageName) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID)
                    Dim aCookie As New HttpCookie("CADairyOnCenter")
                    aCookie.Value = "Yes"
                    Response.Cookies.Add(aCookie)
                    Response.Redirect(strFilePath, False)
                Case "SubmittedForReview"
                    If Convert.ToInt32(lnkButton.Text) <> 0 Then
                        Dim sStatus As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("STM")
                        Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                        Dim strFilePath As String = StudentDiaryEntrySummaryPage & "?Status=" & System.Web.HttpUtility.UrlEncode(sStatus) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID) & "&Page=" & System.Web.HttpUtility.UrlEncode(sPageName)
                        Response.Redirect(strFilePath, False)
                    End If
                Case "ReviewedLocked"
                    If Convert.ToInt32(lnkButtonlkd.Text) <> 0 Then
                        Dim sStatus As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("LKD")
                        Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                        Dim strFilePath As String = StudentDiaryEntrySummaryPage & "?Status=" & System.Web.HttpUtility.UrlEncode(sStatus) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID) & "&Page=" & System.Web.HttpUtility.UrlEncode(sPageName)
                        Response.Redirect(strFilePath, False)
                    End If
                Case "InProgress"
                    If Convert.ToInt32(lnkButtonIP.Text) <> 0 Then
                        Dim sStatus As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("IP")
                        Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                        Dim strFilePath As String = StudentDiaryEntrySummaryPage & "?Status=" & System.Web.HttpUtility.UrlEncode(sStatus) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID) & "&Page=" & System.Web.HttpUtility.UrlEncode(sPageName)
                        Response.Redirect(strFilePath, False)
                    End If
                Case "PDF1"
                    Try
                        'Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("pd1")
                        'Dim strFilePath As String = ReportPage & "?PersonID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(e.CommandArgument))) & "&sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName)
                        'ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & strFilePath & "' )", True)
                        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "CA Diary PD1 Report"))
                        Dim rptParam As New AptifyCrystalReport__c
                        rptParam.ReportID = ReportID
                        rptParam.Param1 = Convert.ToString(e.CommandArgument)
                        rptParam.SubParam1 = Convert.ToString(e.CommandArgument)
                        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                    Catch ex As Exception
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    End Try
                Case "PDF2"
                    Try
                        'Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("pd2")
                        'Dim strFilePath As String = ReportPage & "?PersonID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(e.CommandArgument))) & "&sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName)
                        'ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & strFilePath & "' )", True)
                        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "CA Diary PD2 Report"))
                        Dim rptParam As New AptifyCrystalReport__c
                        rptParam.ReportID = ReportID
                        rptParam.Param1 = Convert.ToString(e.CommandArgument)
                        rptParam.SubParam1 = Convert.ToString(e.CommandArgument)
                        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                    Catch ex As Exception
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    End Try
            End Select
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub radGainedExperience_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radGainedExperience.NeedDataSource
        Try
            BindGainedExperience()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub radGainedExperience_PreRender(sender As Object, e As System.EventArgs) Handles radGainedExperience.PreRender
        If radGainedExperience.MasterTableView.GetItems(GridItemType.NoRecordsItem).Count > 0 Then
            Dim norecordItem As GridNoRecordsItem = radGainedExperience.MasterTableView.GetItems(GridItemType.NoRecordsItem)(0)
            Dim lblnorecordItem As Label = norecordItem.FindControl("lblNoGainedExp")
            lblnorecordItem.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FirmAdminDashBoard.NoGainedExp__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        End If

    End Sub
#End Region

End Class
