Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.Generated

   ''' <summary>
   ''' Generated ASP.NET Grid User Control for the OnlineRefundRequests__c entity.
   ''' </summary>
   ''' <remarks></remarks>
   Partial Class OnlineRefundRequests__cGridClass
       Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "OnlineRefundRequests__cGrid"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "OnlineRefundRedirectURL"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                'SetProperties()
                LoadGrid()
            End If
        End Sub
        Public Overridable Property OnlineRefundRedirectURL() As String
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

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(Me.OnlineRefundRedirectURL) Then
                Me.OnlineRefundRedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(Me.RedirectURL) Then
                Me.grdMain.Enabled = False
                Me.grdMain.ToolTip = "property has not been set."
            End If
            If String.IsNullOrEmpty(Me.RedirectIDParameterName) Then Me.RedirectIDParameterName = "ReportID"
        End Sub
       Protected Overridable Sub LoadGrid()
           Try
                Dim sSQL As String, dt As DataTable

                If Request.QueryString("ID") IsNot Nothing AndAlso IsNumeric(Request.QueryString("ID")) AndAlso CLng(Request.QueryString("ID")) > 0 Then
                    sSQL = "SELECT ID,OrderID,Request,Status,Amount,Response,CurrencyTypeID FROM APTIFY..vwOnlineRefundRequests__c WHERE OrderID = " & CLng(Request.QueryString("ID"))
                Else
                    sSQL = "SELECT ID,OrderID,Request,Status,Amount,Response,CurrencyTypeID FROM APTIFY..vwOnlineRefundRequests__c"
                End If

                dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                'CType(grdMain.Columns(0), GridHyperLinkColumn).DataNavigateUrlFormatString = Me.RedirectURL & "?" & Me.RedirectIDParameterName & "={0}"

                Dim hlink As GridHyperLinkColumn = CType(grdMain.Columns(0), GridHyperLinkColumn)
                hlink.DataNavigateUrlFormatString = "~/customerservice/requestrefund" & "?ID={0}"

                grdMain.DataSource = dt
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
       End Sub
       Protected Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
         LoadGrid()
       End Sub
       Protected Overridable Sub NewRecord(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewRecord.Click
            'Response.Redirect(Me.RedirectURL) just for DMEO purpose we are doing this hard code in actul build it should  not be.

            If Request.QueryString("ID") IsNot Nothing AndAlso IsNumeric(Request.QueryString("ID")) AndAlso CLng(Request.QueryString("ID")) > 0 Then
                Response.Redirect("~/customerservice/requestrefund" & "?OrderID=" & CLng(Request.QueryString("ID")))
            Else
                Response.Redirect("~/customerservice/requestrefund")
            End If

       End Sub
    End Class
End Namespace
