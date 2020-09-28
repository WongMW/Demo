'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande              11/19/2011                         Display Cases on Grid
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
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
   ''' Generated ASP.NET Grid User Control for the Cases entity.
   ''' Description: Tracks individual cases as they go through various stages of resolution from initial entry through final closure
   ''' </summary>
   ''' <remarks></remarks>
   Partial Class CasesGridClass
       Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Public RecordId As Integer
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "CasesGrid"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Public Overridable Property RedirectURLs() As String
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
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                SetProperties()
                LoadGrid()
            End If
        End Sub
        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(Me.RedirectURLs) Then
                Me.RedirectURLs = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(Me.RedirectURLs) Then
                Me.grdMain.Enabled = True
                'Me.grdMain.ToolTip = "property has not been set."
            End If
            If String.IsNullOrEmpty(Me.RedirectIDParameterName) Then Me.RedirectIDParameterName = "ReportID"
        End Sub
        ''' <summary>
        ''' Load All cases created login person
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overridable Sub LoadGrid()
            Try
                Dim sSQL As String, dt As DataTable
                sSQL = "..spGetCaseListAsPerPerson__c @PersonID=" & User1.PersonID ' Added by Govind
                dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                CType(grdMain.Columns(0), GridHyperLinkColumn).DataNavigateUrlFormatString = Me.RedirectURL & "?" & Me.RedirectIDParameterName & "={0}"
                grdMain.DataSource = dt
                'grdMain.DataBind() ' Comment by Govind beacuse we use RAD control and for filter need to add grdMain_NeedDataSource
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub NewRecord(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewRecord.Click
            Response.Redirect(Me.RedirectURLs)
        End Sub
        Protected Sub grdMain_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
            LoadGrid()
        End Sub
    End Class
End Namespace
