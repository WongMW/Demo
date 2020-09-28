'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                19/04/2014                     Display Room booking Information
'Rajesh Kardile              04/25/2014                     Change the Hard code page name
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
    ''' Generated ASP.NET Grid User Control for the RoomBookingApplications__c entity.
    ''' </summary>
    ''' <remarks></remarks>
    Partial Class RoomBookingApplications__cGridClass
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "RoomBookingApplications__cGrid"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"

#Region "Property"

        Public Overridable Property RedirdctPage() As String
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
            If String.IsNullOrEmpty(Me.RedirdctPage) Then
                Me.RedirdctPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(Me.RedirdctPage) Then
                Me.grdMain.Enabled = False
                Me.grdMain.ToolTip = "property has not been set."
            End If
            If String.IsNullOrEmpty(Me.RedirectIDParameterName) Then Me.RedirectIDParameterName = "ReportID"
        End Sub

#End Region

#Region "Page Event"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                If AptifyEbusinessUser1.UserID > 0 Then
                    If Not IsPostBack Then
                        SetProperties()
                        LoadGrid()
                    End If
                Else
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect("~/Login.aspx")
                    'Response.Redirect("~/Home.aspx" + "?ReturnURL=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Request.RawUrl)))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' This method use is to calling Teleric filter 
        ''' </summary>
        ''' <param name="source"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
            Try
                LoadGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Open room booking application form
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Overridable Sub NewRecord(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewRecord.Click
            Try
                Response.Redirect(RedirdctPage, False) 'Rajesh Kardile - 04/25/2014
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region
#Region "Sub Routine"

        Protected Overridable Sub LoadGrid()
            Try
                Dim sSQL As String, dt As DataTable
                sSQL = Database & "..spGetDetailsOnRoomBooking__c @PersonID=" & AptifyEbusinessUser1.PersonID
                dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                CType(grdMain.Columns(0), GridHyperLinkColumn).DataNavigateUrlFormatString = Me.RedirectURL & "?" & Me.RedirectIDParameterName & "={0}"
                grdMain.DataSource = dt

                If dt.Rows.Count > 0 Then
                    lblError.Text = "At present no room request you have made. If you want to do it, please create a create room booking button."
                Else
                    lblError.Text = ""
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
    End Class
End Namespace
