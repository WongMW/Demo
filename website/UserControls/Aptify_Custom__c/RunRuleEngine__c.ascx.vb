''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Kavita Zinage        05/20/2015                      Master Schedule page
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Strict On
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI
Imports Aptify.Framework.Application


Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class RunRuleEngine__c
        Inherits BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE As String = "LoginPage"
        Protected Const ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL As String = "FirmCourseEnrollmentURL__c"
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
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
#Region "Private Functions"

        Private Function LoadPreviousAcademicCycleID() As Integer
            Dim id As Integer = 0
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spCommonCurrentAcadmicCycle__c", Me.Database)
            id = CInt(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            Return id
        End Function
        ''' <summary>
        ''' Run the run engine
        ''' </summary>
        Private Sub RunRuleEngine()
            Try
                Dim _ruleEngine As Aptify.Consulting.RuleEngine__c
                Dim _ruleEngineResult As Boolean
                Dim sql As New StringBuilder()
                sql.AppendFormat("{0} ..spClearRuleEngineData__c @WebUserID={1},@CompanyID={2}", Me.Database, User1.PersonID, User1.CompanyID)
                Me.DataAction.ExecuteNonQuery(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                _ruleEngine = New Aptify.Consulting.RuleEngine__c(Me.DataAction, Me.AptifyApplication)
                '_ruleEngineResult = _ruleEngine.CheckRuleForFirm(Me.AcademicCycleID, CInt(loggedInUser.CompanyID), CInt(loggedInUser.PersonID))
                _ruleEngineResult = _ruleEngine.CheckRuleForFirm(LoadPreviousAcademicCycleID(), CInt(User1.CompanyID), CInt(User1.PersonID))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


#End Region

        ''' <summary>
        ''' Handles Display button click
        ''' </summary>
        Protected Sub btnRunruleengine_Click(sender As Object, e As System.EventArgs) Handles btnRunruleengine.Click
            Try
                RunRuleEngine()
                Response.Redirect(CourseEnrollmentURL)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace


