'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Pradip Chavhan               09/07/2015                        Six Month Mentor Create Page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Namespace Aptify.Framework.Web.eBusiness
    Partial Class SixMonthlySummaryMentor__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage As String = "MentorDashboardPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CreateSixMonthlyMentorReviewPage As String = "SixMonthlyReviewPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage As String = "SixMonthlySummaryPage"
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

        Public Overridable Property CreateSixMonthlyMentorReviewPage() As String
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

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(MentorDashboardPage) Then
                MentorDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage)
            End If
            If String.IsNullOrEmpty(CreateSixMonthlyMentorReviewPage) Then
                CreateSixMonthlyMentorReviewPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_CreateSixMonthlyMentorReviewPage)
            End If
        End Sub

#End Region

#Region "Page Events"
        Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
            radStudentReview.Columns(0).CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.EqualTo

        End Sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()
                If User1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If
                If Not IsPostBack Then
                    LoadPersonDetails()
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "LoadData"
        ''' <summary>
        ''' Load Student Mentor review data 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadPersonDetails()
            Try
                Dim sSql As String = Database & "..spGetPersonSixMonthlySummaryMentor__c @MentorID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If dt IsNot Nothing Then
                    'AndAlso dt.Rows.Count > 0 Then
                    radStudentReview.DataSource = dt
                    radStudentReview.DataBind()
                    lblErrorMsg.Visible = False
                    radStudentReview.Visible = True
                    'Else
                    '    lblErrorMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.NoRecord__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    '    lblErrorMsg.Visible = True
                    '    radStudentReview.Visible = False
                End If
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
        Protected Sub radStudentReview_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radStudentReview.ItemCommand
            Try
                If e.CommandName = "PersonName" Then
                    Dim sPageName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("sm")
                    Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                    Dim strFilePath As String = CreateSixMonthlyMentorReviewPage & "?Page=" & System.Web.HttpUtility.UrlEncode(sPageName) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID)
                    Response.Redirect(strFilePath, False)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub radStudentReview_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radStudentReview.NeedDataSource
            Try
                LoadPersonDetails()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "ButtonEvent"
        ''' <summary>
        ''' CLick on back redirect to the Mentor dashobard page
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Response.Redirect(MentorDashboardPage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

    End Class
End Namespace
