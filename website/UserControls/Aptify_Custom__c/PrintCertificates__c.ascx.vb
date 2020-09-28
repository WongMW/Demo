''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Pradip Chavhan        10/02/2016                      Authgorisation Certificate Page
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Strict On
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI
Imports Aptify.Framework.Application


Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class PrintCertificates
        Inherits BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage As String = "ReportPage"
        Dim IsFirm As Integer = 0
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
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE)
            End If

            If String.IsNullOrEmpty(ReportPage) Then
                ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage)
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
                If User1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If

                If Not IsPostBack Then
                    LoadGrid()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
#Region "Private Functions"
        ''' <summary>
        ''' Load Subscription Grid
        ''' </summary>
        Public Sub LoadGrid()
            Try
                If Request.QueryString("IsFirmPage") IsNot Nothing Then
                    IsFirm = Convert.ToInt32(Request.QueryString("IsFirmPage"))
                End If
                Dim sSql As String = Database & "..spGetAuthorisationCertificateList__c"
                Dim param(2) As IDataParameter
                param(0) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, User1.PersonID)
                param(1) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, User1.CompanyID)
                param(2) = DataAction.GetDataParameter("@IsFirmPage", SqlDbType.Int, IsFirm)
                Dim dtsubscription As DataTable = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
                If Not dtsubscription Is Nothing Then
                    Me.grdsubscription.DataSource = dtsubscription
                    Me.grdsubscription.DataBind()
                    Me.grdsubscription.Visible = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
        ''' <summary>
        ''' Handles need data source to load/reload grid 
        ''' </summary>
        Protected Sub grdsubscription_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdsubscription.NeedDataSource
            Try
                LoadGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdsubscription_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdsubscription.ItemCommand
            Try
                Dim btnprint As Button = DirectCast(e.Item.FindControl("btnPrint"), Button)
                Select Case e.CommandName
                    Case "Print"
                        'Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("Auth")
                        'Dim strFilePath As String = ReportPage & "?ID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(e.CommandArgument))) & "&sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName)
                        'ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & strFilePath & "' )", True)
                        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Authorisation Certificate Report"))
                        Dim rptParam As New AptifyCrystalReport__c
                        rptParam.ReportID = ReportID
                        rptParam.Param1 = Convert.ToString(e.CommandArgument)
                        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                End Select
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdsubscription_PreRender(sender As Object, e As System.EventArgs) Handles grdsubscription.PreRender
            If grdsubscription.MasterTableView.GetItems(GridItemType.NoRecordsItem).Count > 0 Then
                Dim norecordItem As GridNoRecordsItem = DirectCast(grdsubscription.MasterTableView.GetItems(GridItemType.NoRecordsItem)(0), GridNoRecordsItem)
                Dim lblnorecordItem As Label = DirectCast(norecordItem.FindControl("lblNoGainingExp"), Label)
                lblnorecordItem.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.AuthCertificate.NoRecord__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        End Sub

    End Class
End Namespace


