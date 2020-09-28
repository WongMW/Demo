'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande               07/16/2015                        Submit Quota Application
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Namespace Aptify.Framework.Web.eBusiness.CompanyAdministrator
    Partial Class QuotaApplication__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_QUOTAAPPLICATION As String = "QuotaApplication"
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
                lblFirmName.Text = User1.Company
                lblRequestedBy.Text = User1.FirstName + " " + User1.LastName
                If Request.QueryString("QuotaAppID") Is Nothing Then
                    Dim sSql As String = Database & "..spGetCurrentActiveQuotaAsPerFirm__c @CompanyID=" & User1.CompanyID
                    Dim iCurrentQuota As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If iCurrentQuota > 0 Then
                        lblCurrentQuota.Text = Convert.ToString(iCurrentQuota)
                    End If
                Else
                    Dim sSql As String = Database & "..spGetRTOQuotaAppDetails__c @QuotaAppID=" & Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("QuotaAppID"))
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        lblApplicationStatus.Text = Convert.ToString(dt.Rows(0)("Status"))
                        lblCurrentQuota.Text = Convert.ToString(dt.Rows(0)("CurrentQuota"))
                        txtRequestedQuota.Text = Convert.ToString(dt.Rows(0)("RequestedQuota"))

                    End If
                    btnSubmit.Visible = False
                    txtRequestedQuota.Enabled = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Button Click"
        ''' <summary>
        ''' Create a Quota Application on button click
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>

        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                Dim oGE As AptifyGenericEntityBase
                If Convert.ToInt32(txtRequestedQuota.Text.Trim) > 0 Then
                    If Request.QueryString("QuotaAppID") Is Nothing Then
                        oGE = AptifyApplication.GetEntityObject("QuotaApplications__c", -1)
                        With oGE
                            .SetValue("TrainingFirmID", User1.CompanyID)
                            .SetValue("RequestedByID", User1.PersonID)
                            .SetValue("SubmittedDate", Now.Date)
                            .SetValue("Status", "Submitted to CAI")
                            .SetValue("RequestedQuota", Convert.ToInt32(txtRequestedQuota.Text.Trim))
                        End With
                        Dim sError As String = String.Empty
                        If oGE.Save(False, sError) Then
                            lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.QuotaApplication.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radwindowSubmit.VisibleOnPageLoad = True
                        End If
                    End If
                Else
                    lblQuataAppError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.QuotaApplication.MoreThanZeroRequestedQuota")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblQuataAppError.Visible = True
                End If
               
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Response.Redirect(QuotaApplicationPage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnSubmitOk_Click(sender As Object, e As System.EventArgs) Handles btnSubmitOk.Click
            Try
                radwindowSubmit.VisibleOnPageLoad = False
                Response.Redirect(QuotaApplicationPage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
    End Class
End Namespace
