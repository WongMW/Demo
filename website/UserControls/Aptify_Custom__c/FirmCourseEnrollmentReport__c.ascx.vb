Option Explicit On
Option Strict On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
'Developer                  Date modified               Comments 
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                21/05/2018                Added Firm Enrolment Report Dashboard 
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

Partial Class FirmCourseEnrollmentReport__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

#Region "Properties"
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "FirmCourseEnrollmentReport__c"
    Protected Const ATTRIBUTE_ENROLMENT_PAGE_URL As String = "FirmCourseEnrollmentURL__c"

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
#End Region

#Region "Page Events"
    
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
       
        'Code added by Siddharth to redirect user to security page if firm is not RTO or user is not Admin.
        SetProperties()
    End Sub
#End Region

#Region "Protected Methods"
    Protected Overrides Sub SetProperties()
        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        MyBase.SetProperties()
        If String.IsNullOrEmpty(Me.CourseEnrollmentURL) Then
            Me.CourseEnrollmentURL = Me.GetLinkValueFromXML(ATTRIBUTE_ENROLMENT_PAGE_URL)
        End If
    End Sub
#End Region
 

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Try
            Dim sSQL As String = Database & "..spETDFirmEnrolmentExportToExcel__c @CurrentStage='" & Trim(ddlCurrentStage.SelectedValue) & "',@CompanyID=" & loggedInUser.CompanyID
            Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Dim filename As String = "FirmEnrolment.xls"
                Dim tw As New System.IO.StringWriter()
                Dim hw As New System.Web.UI.HtmlTextWriter(tw)
                Dim dgGrid As New DataGrid()
                dgGrid.DataSource = dt
                dgGrid.DataBind()
                dgGrid.RenderControl(hw)
                Response.ContentType = "application/vnd.ms-excel"
                Response.AppendHeader("Content-Disposition", (Convert.ToString("attachment; filename=") & filename) + "")
                Me.EnableViewState = False
                Response.Write(tw.ToString())
                Response.Flush()
                Response.[End]()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
