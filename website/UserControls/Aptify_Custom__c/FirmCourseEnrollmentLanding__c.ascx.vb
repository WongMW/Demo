Option Explicit On
Option Strict On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
'Developer                  Date modified               Comments 
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Siddharth Kavitake         05-Mar-2018                 Added landing page for big firm course enrolments 
'Govind Mande               21-May-2018                 Added Firm Dashboard Report button and redirect to reports page for Redmine #19049
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports System.Data
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports System.Linq
Imports System.Drawing
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI.PivotGrid
Imports Telerik.Web.UI.PivotGrid.Core
Imports System.Windows.Threading
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices

Partial Class FirmCourseEnrollmentLanding__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

#Region "Properties"
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "FirmCourseEnrollmentLanding__c"
    Protected Const ATTRIBUTE_ENROLMENT_PAGE_URL As String = "FirmCourseEnrollmentURL__c"
    Protected Const ATTRIBUTE_ENROLMENTREPORT_PAGE_URL As String = "FirmCourseEnrollmentReport__c"
    Private _isFirmRTO As Integer
    Private _CAP1ID As Integer
    Private _CAP2ID As Integer
    Private _Bridge As Integer
    Private _FAE As Integer

    Public Overridable Property CourseEnrollmentURL() As String
        Get
            If Not ViewState(ATTRIBUTE_ENROLMENT_PAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_ENROLMENT_PAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_ENROLMENT_PAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property CourseEnrollmentReportURL() As String
        Get
            If Not ViewState(ATTRIBUTE_ENROLMENTREPORT_PAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_ENROLMENTREPORT_PAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_ENROLMENTREPORT_PAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Property IsFirmRTO() As Integer
        Get
            _isFirmRTO = CInt(ViewState("IsFirmRTO"))
            Return _isFirmRTO
        End Get
        Set(ByVal value As Integer)
            _isFirmRTO = value
            ViewState("IsFirmRTO") = _isFirmRTO.ToString()
        End Set
    End Property
#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim _ScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        _ScriptManager.AsyncPostBackTimeout = 36000
        'Code added by Siddharth to redirect user to security page if firm is not RTO or user is not Admin.
        If Not IsPageAccessible() Then
            Response.Redirect("~/" + ConfigurationManager.AppSettings.GetValues("SecurityErrorPageURL")(0) + "?Message=" + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.General.NoAccessToPage__c")), _
                Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), Me.DataAction.UserCredentials))
        End If
        SetProperties()
        If Not IsPostBack Then
            FillLocation()
            If Request.QueryString("Source") Is Nothing Then
                RunRuleEngine()
                SavePersonsStageDetails()
            End If
        End If
        SetCurrentStageIDs()
    End Sub

#End Region

#Region "Protected Methods"
    Protected Overrides Sub SetProperties()
        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        MyBase.SetProperties()
        If String.IsNullOrEmpty(Me.CourseEnrollmentURL) Then
            Me.CourseEnrollmentURL = Me.GetLinkValueFromXML(ATTRIBUTE_ENROLMENT_PAGE_URL)
        End If
        If String.IsNullOrEmpty(Me.CourseEnrollmentReportURL) Then
            Me.CourseEnrollmentReportURL = Me.GetLinkValueFromXML(ATTRIBUTE_ENROLMENTREPORT_PAGE_URL)
        End If
    End Sub
#End Region

#Region "Private Methods"
    Private Function IsPageAccessible() As Boolean
        Dim bResult As Boolean = False
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spIsBigFirmCompanyRTO__c @CompanyID={1}", Database, loggedInUser.CompanyID.ToString())
            Dim companyId As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            If companyId > 1 Then
                sql.Clear()
                sql.AppendFormat("{0}..spCheckFirmAdminForBigFirmPage__c @PersonID={1}", Database, loggedInUser.PersonID)
                Dim isCompanyAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                If isCompanyAdmin > 0 Then
                    bResult = True
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return bResult
    End Function

    ''' <summary>
    ''' Run the run engine
    ''' </summary>
    Private Sub RunRuleEngine()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spClearRuleEngineData__c @WebUserID={1},@CompanyID={2}", Me.Database, loggedInUser.PersonID, loggedInUser.CompanyID)
            Me.DataAction.ExecuteNonQuery(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            Dim _ruleEngine As Aptify.Consulting.RuleEngine__c
            Dim _ruleEngineResult As Boolean
            _ruleEngine = New Aptify.Consulting.RuleEngine__c(Me.DataAction, Me.AptifyApplication)
            '_ruleEngineResult = _ruleEngine.CheckRuleForFirm(Me.AcademicCycleID, CInt(loggedInUser.CompanyID), CInt(loggedInUser.PersonID))
            _ruleEngineResult = _ruleEngine.CheckRuleForFirm(LoadPreviousAcademicCycleID(), CInt(loggedInUser.CompanyID), CInt(loggedInUser.PersonID))
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub SavePersonsStageDetails()
        Try
            lblMessage.Text = String.Empty
            Dim oParam(1) As System.Data.IDataParameter
            oParam(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, loggedInUser.CompanyID)
            oParam(1) = DataAction.GetDataParameter("@CompanyAdminID", SqlDbType.Int, loggedInUser.PersonID)
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spSavePersonsStageDetailsForCurrentNextCycle__c", _
                             Me.Database)
            Dim iCnt As Integer = DataAction.ExecuteNonQueryParametrized(sql.ToString(), CommandType.StoredProcedure, oParam, 680)
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Function LoadPreviousAcademicCycleID() As Integer
        Dim id As Integer = 0
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spCommonCurrentAcadmicCycle__c", Me.Database)
            id = CInt(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return id
    End Function

    Private Sub SetCurrentStageIDs()
        Try
            Dim sSql As String = Database & "..spGetCurriculumDefinationDetailsWithBridging__c"
            Dim dt As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Select Case dt.Rows(i)("Name").ToString()
                        Case "CAP1- CA Proficiency 1"
                            _CAP1ID = Convert.ToInt32(dt.Rows(i)("ID"))
                        Case "CAP2- CA Proficiency 2"
                            _CAP2ID = Convert.ToInt32(dt.Rows(i)("ID"))
                        Case "Bridge"
                            _Bridge = Convert.ToInt32(dt.Rows(i)("ID"))
                        Case "FAE- Final Admitting Exam"
                            _FAE = Convert.ToInt32(dt.Rows(i)("ID"))
                    End Select
                Next
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub FillLocation()
        Try
            Dim sSql As String = Database & "..spGetLocationsDetailsForBFP__c"
            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, loggedInUser.CompanyID.ToString())
            Dim dt As DataTable = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
            ddlLocation.DataSource = dt
            ddlLocation.DataTextField = "Name"
            ddlLocation.DataValueField = "ID"
            ddlLocation.DataBind()
            'ddlLocation.Items.Insert(0, New System.Web.UI.WebControls.ListItem("All", "0"))
            ddlLocation.SelectedValue = loggedInUser.CompanyID.ToString()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Other Events"

    Protected Sub lbtnCAP1_Click(sender As Object, e As EventArgs) Handles lbtnCAP1.Click
        Try
            Response.Redirect(CourseEnrollmentURL + "?CurrentState=" + _CAP1ID.ToString + "&Loc=" + ddlLocation.SelectedValue)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    Protected Sub lbtnCAP2_Click(sender As Object, e As EventArgs) Handles lbtnCAP2.Click
        Try
            Response.Redirect(CourseEnrollmentURL + "?CurrentState=" + _CAP2ID.ToString + "&Loc=" + ddlLocation.SelectedValue)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    Protected Sub lbtnBrigde_Click(sender As Object, e As EventArgs) Handles lbtnBrigde.Click
        Try
            Response.Redirect(CourseEnrollmentURL + "?CurrentState=" + _Bridge.ToString + "&Loc=" + ddlLocation.SelectedValue)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    Protected Sub lbtnFAE_Click(sender As Object, e As EventArgs) Handles lbtnFAE.Click
        Try
            Response.Redirect(CourseEnrollmentURL + "?CurrentState=" + _FAE.ToString + "&Loc=" + ddlLocation.SelectedValue)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Try
            Response.Redirect(CourseEnrollmentReportURL, False)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

End Class
