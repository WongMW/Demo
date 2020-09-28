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

   Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "CasesGrid"
   Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
       Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
           If Not IsPostBack Then
               SetProperties()
               LoadGrid()
           End If
       End Sub

       Protected Overrides Sub SetProperties()
       If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
         MyBase.SetProperties()
         If String.IsNullOrEmpty(Me.RedirectURL) Then
            Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
         End If
         If String.IsNullOrEmpty(Me.RedirectURL) Then
           Me.grdMain.Enabled = False
           Me.grdMain.ToolTip = "property has not been set."
         End If
           If String.IsNullOrEmpty(Me.RedirectIDParameterName) Then Me.RedirectIDParameterName = "ReportID"
      End Sub
       Protected Overridable Sub LoadGrid()
           Try
               Dim sSQL As String, dt as DataTable
               sSQL = "SELECT ID,Title,Parent,CaseReporter,CaseCategory,CaseType,CasePriority,CaseStatus,Summary,Contact,Company,DateStarted,DateStartEstimated,DateClosed,DateCompleteEstimated,ReviewedBy,CaseResult,PrimaryAssignee,AssignedToExternalCompany,AssignedToExternalContact,AssignedToType,DateAssignedExternal,DateAssignedInternal,CaseReportMethod,DateReported,DateRecorded,RecordedBy,DateFirstResponded,ResponseTime,HoursEstimated,HoursActual,HoursVariance,CostBudgeted,CostActual,CostVariance,ClosestCrossStreet FROM APTIFY..vwCases"
               dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
               CType(grdMain.Columns(0), GridHyperLinkColumn).DataNavigateUrlFormatString=Me.RedirectURL & "?" & Me.RedirectIDParameterName & "={0}"
               grdMain.DataSource = dt
           Catch ex As Exception
               Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
           End Try
       End Sub
       Protected Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
         LoadGrid()
       End Sub
       Protected Overridable Sub NewRecord(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewRecord.Click
           Response.Redirect(Me.RedirectURL)
       End Sub
    End Class
End Namespace
