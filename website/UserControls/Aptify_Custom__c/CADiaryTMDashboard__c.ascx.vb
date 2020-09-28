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

Partial Class CADiaryTMDashboard__c
    Inherits eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentOfMentors As String = "AssignmentOfMentors"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentBusinessUnit As String = "AssignmentBusinessUnit"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CADIaryGuidelinesPage As String = "CADIaryGuidelinesPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CAIUpdatesPage As String = "CAIUpdatesPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MentorReviewsPage As String = "MentorReviewsPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage As String = "StudentDashboardPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_StudentsDiaryEntrySummaryPage As String = "StudentDiaryEntrySummaryPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage As String = "ReportPage"
    Dim sErrorMessage As String = String.Empty
    Dim sSQL As String = String.Empty
    Dim TotalLeave As Integer = 0
#Region "Firm Admin Dashboard Specific Properties"
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

    Public Overridable Property AssignmentOfMentors() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentOfMentors) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentOfMentors))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentOfMentors) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property AssignmentBusinessUnit() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentBusinessUnit) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentBusinessUnit))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentBusinessUnit) = Me.FixLinkForVirtualPath(value)
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
        If String.IsNullOrEmpty(AssignmentOfMentors) Then
            AssignmentOfMentors = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentOfMentors)
        End If
        If String.IsNullOrEmpty(AssignmentBusinessUnit) Then
            AssignmentBusinessUnit = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentBusinessUnit)
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
        If String.IsNullOrEmpty(StudentDashboardPage) Then
            StudentDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage)
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
        radGainingExperience.AllowPaging = True
        radGainingExperience.PageSize = 5
        radGainedExperience.AllowPaging = True
        radGainedExperience.PageSize = 5
        lnkCADiaryGuidelines.Attributes.Add("onClick", "javascript:window.open('" + CADIaryGuidelinesPage + "');return false;")
        lnkCAIUpdates.Attributes.Add("onClick", "javascript:window.open('" + CAIUpdatesPage + "');return false;")
        If User1.PersonID < 0 Then
            Session("ReturnToPage") = Request.RawUrl
            Response.Redirect(LoginPage, False)
        End If
        If Not IsPostBack Then
            fillMentor()
            If Session("MentorID") IsNot Nothing Then
                ddlMentor.SelectedValue = Convert.ToString(Session("MentorID"))
                If ddlMentor.SelectedIndex <> 0 Then
                    BindGainingExperience(ddlMentor.SelectedValue)
                    BindGainedExperience(ddlMentor.SelectedValue)
                    divGainedExperience.Visible = True
                    divGainingExperience.Visible = True
                Else
                    radGainingExperience.DataSource = Nothing
                    radGainingExperience.DataBind()
                    radGainedExperience.DataSource = Nothing
                    radGainedExperience.DataBind()
                    divGainedExperience.Visible = False
                    divGainingExperience.Visible = False
                End If
            End If
        End If
    End Sub
#End Region

#Region "Link Button Events"
    Protected Sub lnkAssignaBusinessUnit_Click(sender As Object, e As System.EventArgs) Handles lnkAssignaBusinessUnit.Click
        Dim sPageName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("fd")
        Response.Redirect(AssignmentBusinessUnit & "?Page=" & System.Web.HttpUtility.UrlEncode(sPageName), False)
    End Sub

    Protected Sub lnkAssignmentofMentors_Click(sender As Object, e As System.EventArgs) Handles lnkAssignmentofMentors.Click
        Response.Redirect(AssignmentOfMentors, False)
    End Sub

    Protected Sub lnkCADiaryGuidelines_Click(sender As Object, e As System.EventArgs) Handles lnkCADiaryGuidelines.Click
        Response.Redirect(CADIaryGuidelinesPage, False)
    End Sub

    Protected Sub lnkCAIUpdates_Click(sender As Object, e As System.EventArgs) Handles lnkCAIUpdates.Click
        Response.Redirect(CAIUpdatesPage, False)
    End Sub

#End Region

#Region "Popup Button Events"

    Protected Sub btnValidationOK_Click(sender As Object, e As System.EventArgs) Handles btnValidationOK.Click
        radWindowValidation.VisibleOnPageLoad = False
    End Sub

#End Region

#Region "Dropdown Event"
    ''' <summary>
    ''' Dropdown event to display GainingExperience and GainedExperience details grid
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ddlMentor_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMentor.SelectedIndexChanged
        Try
            If ddlMentor.SelectedIndex <> 0 Then
                BindGainingExperience(ddlMentor.SelectedValue)
                BindGainedExperience(ddlMentor.SelectedValue)
                divGainedExperience.Visible = True
                divGainingExperience.Visible = True
                Session("MentorID") = Convert.ToString(ddlMentor.SelectedValue)

            Else
                radGainingExperience.DataSource = Nothing
                radGainingExperience.DataBind()
                radGainedExperience.DataSource = Nothing
                radGainedExperience.DataBind()
                divGainedExperience.Visible = False
                divGainingExperience.Visible = False
                Session("MentorID") = Nothing
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region
#Region "Grid Event"

    Protected Sub radGainingExperience_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radGainingExperience.ItemCommand
        Try
            Dim lnkButton As LinkButton = e.Item.FindControl("lnkSubmittedForReview")
            Dim lnkButtonlkd As LinkButton = e.Item.FindControl("lnkReviewedLocked")

            Dim lnkButtonIP As LinkButton = e.Item.FindControl("lnkInProgress")
            Dim sPageName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("fd")
            Select Case e.CommandName
                Case "PersonName"
                    Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                    Dim strFilePath As String = StudentDashboardPage & "?Page=" & System.Web.HttpUtility.UrlEncode(sPageName) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID) '& "&MentorID=" & System.Web.HttpUtility.UrlEncode(sMentorID)
                    'Code added by Govind Mande 7 Jun 2016
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
                Case "Report"
                    Try
                        Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
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
                Case "Application"
                    Try
                        Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
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
            BindGainingExperience(ddlMentor.SelectedValue)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub radGainedExperience_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radGainedExperience.ItemCommand
        Try
            Dim lnkButton As LinkButton = e.Item.FindControl("lnkSubmittedForReview")
            Dim lnkButtonlkd As LinkButton = e.Item.FindControl("lnkReviewedLocked")
            Dim sPageName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("fd")
            Dim lnkButtonIP As LinkButton = e.Item.FindControl("lnkInProgress")
            Select Case e.CommandName
                Case "PersonName"
                    Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                    Dim strFilePath As String = StudentDashboardPage & "?Page=" & System.Web.HttpUtility.UrlEncode(sPageName) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID)
                    'Code added by Govind Mande 7 Jun 2016
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
                Case "Report"
                    Try
                        Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
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
                Case "Application"
                    Try
                        Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
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
            BindGainedExperience(ddlMentor.SelectedValue)
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

#Region "Private Functions"
    ''' <summary>
    ''' Method To Bind Gaining Experience Grid.
    ''' </summary>
    ''' <param name="MentorID"></param>
    ''' <remarks></remarks>
    Private Sub BindGainingExperience(ByVal MentorID As Integer)
        Dim dtGainingExp As Data.DataTable
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetGainingExperienceDetails__c @MentorID=" & MentorID
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
    ''' Method To Fill Mentor Dropdown
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fillMentor()
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetMentorDetailsForFirmAdminDashboard__c @CompanyId=" & User1.CompanyID
            Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlMentor.DataSource = dt
            ddlMentor.DataTextField = "Name"
            ddlMentor.DataValueField = "ID"
            ddlMentor.DataBind()
            ddlMentor.Items.Insert(0, "Select")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Meyhod To Bind Gained Experience Grid
    ''' </summary>
    ''' <param name="MentorID"></param>
    ''' <remarks></remarks>
    Private Sub BindGainedExperience(ByVal MentorID As Integer)
        Dim dtGainedExp As Data.DataTable
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetGainedExperienceDetails__c @MentorID=" & MentorID
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
    'Start Redmine log #16739
    Private Function AddExcelStyling() As String
        Dim sb As New StringBuilder()
        sb.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office'" +
        "xmlns:x='urn:schemas-microsoft-com:office:excel'" +
        "xmlns='http://www.w3.org/TR/REC-html40'>" +
        "<head>")

        sb.Append("<style>")

        sb.Append("@page")
        sb.Append("{margin:.5in .75in .5in .75in;")

        sb.Append("mso-header-margin:.5in;")
        sb.Append("mso-footer-margin:.5in;")

        sb.Append("mso-page-orientation:landscape;}")
        sb.Append("</style>")

        sb.Append("<!--[if gte mso 9]><xml>")
        sb.Append("<x:ExcelWorkbook>")

        sb.Append("<x:ExcelWorksheets>")
        sb.Append("<x:ExcelWorksheet>")

        sb.Append("<x:Name>Projects 3 </x:Name>")
        sb.Append("<x:WorksheetOptions>")

        sb.Append("<x:Print>")
        sb.Append("<x:ValidPrinterInfo/>")

        sb.Append("<x:PaperSizeIndex>9</x:PaperSizeIndex>")
        sb.Append("<x:HorizontalResolution>600</x:HorizontalResolution")

        sb.Append("<x:VerticalResolution>600</x:VerticalResolution")
        sb.Append("</x:Print>")

        sb.Append("<x:Selected/>")
        sb.Append("<x:DoNotDisplayGridlines/>")

        sb.Append("<x:ProtectContents>False</x:ProtectContents>")
        sb.Append("<x:ProtectObjects>False</x:ProtectObjects>")

        sb.Append("<x:ProtectScenarios>False</x:ProtectScenarios>")
        sb.Append("</x:WorksheetOptions>")

        sb.Append("</x:ExcelWorksheet>")
        sb.Append("</x:ExcelWorksheets>")

        sb.Append("<x:WindowHeight>12780</x:WindowHeight>")
        sb.Append("<x:WindowWidth>19035</x:WindowWidth>")

        sb.Append("<x:WindowTopX>0</x:WindowTopX>")
        sb.Append("<x:WindowTopY>15</x:WindowTopY>")

        sb.Append("<x:ProtectStructure>False</x:ProtectStructure>")
        sb.Append("<x:ProtectWindows>False</x:ProtectWindows>")

        sb.Append("</x:ExcelWorkbook>")
        sb.Append("</xml><![endif]-->")

        sb.Append("</head>")
        sb.Append("<body>")

        Return sb.ToString()

    End Function
    Protected Sub lnkCADairyStatus_Click(sender As Object, e As EventArgs) Handles lnkCADairyStatus.Click
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetCADairyStatusDetails__c @CompanyID=" & User1.CompanyID
            Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            Dim filename As String = "CADairyStatus.xls"
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim dgGrid As New DataGrid()
            dgGrid.DataSource = dt
            dgGrid.DataBind()
            dgGrid.RenderControl(hw)
            Response.ContentType = "application/vnd.ms-excel"
            ' Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"

            Response.AppendHeader("Pragma", "public")
            Response.AppendHeader("Expires", "0")
            Response.AppendHeader("Content-Transfer-Encoding", "binary")
            Response.AppendHeader("Content-Type", "application/force-download")
            Response.AppendHeader("Content-Type", "application/octet-stream")
            Response.AppendHeader("Content-Type", "application/download")
            Response.AppendHeader("Cache-Control", " must-revalidate, post-check=0, pre-check=0")
            Response.AppendHeader("Content-Disposition", (Convert.ToString("attachment; filename=") & filename) + "")
            Me.EnableViewState = False
            'Sachin added below line
            Response.Write(AddExcelStyling())
            Response.Write(tw.ToString())
            'End
            'Sachin added below line
            Response.Write("</body>")
            Response.Write("</html>")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    'End Redmine log #16739
#End Region




End Class
