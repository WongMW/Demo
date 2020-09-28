'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande               07/16/2015                         Quota Applications Details
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Namespace Aptify.Framework.Web.eBusiness.CompanyAdministrator
    Partial Class QuotaApplicationGrid__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_QUOTAAPPLICATION As String = "QuotaApplicationPage"
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
        Public Overridable Property QuotaApplicationPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_QUOTAAPPLICATION) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_QUOTAAPPLICATION))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_QUOTAAPPLICATION) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(QuotaApplicationPage) Then
                QuotaApplicationPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_QUOTAAPPLICATION)
            End If
        End Sub

#End Region

#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()
                If User1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If
                If Not IsPostBack Then
                    LoadQuotaApplication()
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Load Methods"
        Private Sub LoadQuotaApplication()
            Try
                Dim sSql As String = Database & "..spGetRTOQuotaApplications__c @CompanyID=" & User1.CompanyID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    radQuotaApplication.DataSource = dt
                    radQuotaApplication.DataBind()
                    radQuotaApplication.Visible = True
                    lblQuataAppError.Visible = False
                Else
                    radQuotaApplication.Visible = False
                    lblQuataAppError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.QuotaApplication.NoQuotaApplication")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblQuataAppError.Visible = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Button Click"
        ''' <summary>
        ''' To check if Quota App is Active 
        ''' </summary>
        ''' 
        Public Function IsChecked(IsActive As Boolean) As Boolean
            Try
                If IsActive = True Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
        Protected Sub btnNewQuotaApp_Click(sender As Object, e As System.EventArgs) Handles btnNewQuotaApp.Click
            Try
                Response.Redirect(QuotaApplicationPage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Grid Events"
        ''' <summary>
        ''' When click on Application id its redirect to the Quota App details page
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>

        Protected Sub radQuotaApplication_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radQuotaApplication.ItemCommand
            Try
                If e.CommandName = "QuotaApp" Then
                    Dim QuotaAppID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument)
                    Response.Redirect(QuotaApplicationPage & "?QuotaAppID=" & System.Web.HttpUtility.UrlEncode(QuotaAppID), False)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
       
      
    End Class
End Namespace
