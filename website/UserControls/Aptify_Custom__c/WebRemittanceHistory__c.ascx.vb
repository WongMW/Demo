'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
'Developer                  Date created/modified               Comments 
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Milind Sutar               24/03/2015                  Web Remittance History Page -  To show remittance of orders
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Option Explicit On
Option Strict On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.Security
Imports System.Data
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class WebRemittanceHistory__c
        Inherits BaseUserControlAdvanced

#Region "Properties"

        Private _webRemittanceHistory As New DataTable()
        Private _subsidiariesList As New DataTable()
        Private _companyId As Integer

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "WebRemittanceHistory__c"
        Protected Const ATTRIBUTE_WEBREMITTANCEDETAILS_PAGE As String = "WebRemittanceDetailsURL"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"

        ''' <summary>
        ''' To get/set web remittance details page url 
        ''' </summary>
        Public Overridable Property WebRemittanceDetailsURL() As String
            Get
                If Not ViewState(ATTRIBUTE_WEBREMITTANCEDETAILS_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_WEBREMITTANCEDETAILS_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_WEBREMITTANCEDETAILS_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' To get/set company id
        ''' </summary>
        Public Property CompanyID() As Integer
            Get
                _companyId = CInt(hfCompanyID.Value)
                Return _companyId
            End Get
            Set(ByVal value As Integer)
                _companyId = value
                hfCompanyID.Value = CStr(_companyId)
            End Set
        End Property


#End Region

#Region "Protected Methods"

        ''' <summary>
        ''' Handles page load event
        ''' </summary>
        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            SetProperties()
            If Not IsPostBack Then
                LoadSubsidiaries()
            End If
        End Sub

        ''' <summary>
        ''' To set properties
        ''' </summary>
        Protected Overrides Sub SetProperties()
            MyBase.SetProperties()
            Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            If String.IsNullOrEmpty(WebRemittanceDetailsURL) Then
                Me.WebRemittanceDetailsURL = Me.GetLinkValueFromXML(ATTRIBUTE_WEBREMITTANCEDETAILS_PAGE)
            End If
        End Sub

        ''' <summary>
        ''' To handle item bound
        ''' Code updated by Saurabh for 21277
        ''' </summary>
        Protected Sub gvWebRemittanceHistory_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvWebRemittanceHistory.ItemDataBound
            Try
                If TypeOf e.Item Is GridDataItem Then
                    Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                    Dim rowView As DataRowView = DirectCast(item.DataItem, DataRowView)
                    ' Dim msg As String = rowView("WebRemittanceNo__c").ToString().Substring(8, rowView("WebRemittanceNo__c").ToString().Length)
                    Dim webRemittanceNumber As String = rowView("WebRemittanceNo__c").ToString().Substring(9)
                    Dim link As HyperLink = DirectCast(item("WebRemittanceNo__c").Controls(0), HyperLink)
                    Dim url As New StringBuilder()
                    url.AppendFormat("{0}?Number={1}&CId={2}", Me.WebRemittanceDetailsURL, webRemittanceNumber.ToString(), Me.CompanyID)
                    link.NavigateUrl = url.ToString()
                End If
            Catch ex As Exception
                lblMessage.Text = ex.Message
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Visible = True
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' Code updated by Saurabh for 21277 --End end here

        ''' <summary>
        ''' Handles need data sourse to bind data
        ''' </summary>
        Protected Sub gvWebRemittanceHistory_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvWebRemittanceHistory.NeedDataSource
            LoadRemittanceHistoryData()
            gvWebRemittanceHistory.DataSource = _webRemittanceHistory
        End Sub

        ''' <summary>
        ''' Hanles company dropdown selected index changed event
        ''' </summary>
        Protected Sub ddlSubsidiariesList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSubsidiariesList.SelectedIndexChanged
            If ddlSubsidiariesList.SelectedValue IsNot Nothing Then
                Me.CompanyID = CInt(ddlSubsidiariesList.SelectedValue)
                gvWebRemittanceHistory.Rebind()
            End If
        End Sub

#End Region

#Region "Private Methods"

        ''' <summary>
        ''' To load subsidiaries 
        ''' </summary>
        Private Sub LoadSubsidiaries()
            Try
                Dim sql As New StringBuilder()
                Me.CompanyID = CInt(User1.CompanyID)
               
 		''Commented BY Pradip 2016-08-30 For To Display Currency wise Subsidiaries
                ' sql.AppendFormat("{0}..spGetAllSubsidiaries__c @ParentCompanyId={1}", Me.Database, Me.CompanyID.ToString())
                sql.AppendFormat("{0}..spGetLocationsDetailsForBFP__c @CompanyID={1}", Me.Database, Me.CompanyID.ToString())
                _subsidiariesList = Me.DataAction.GetDataTable(sql.ToString())
                ddlSubsidiariesList.DataSource = _subsidiariesList
                ddlSubsidiariesList.DataValueField = "ID"
                ddlSubsidiariesList.DataTextField = "Name"
                ddlSubsidiariesList.DataBind()
                If _subsidiariesList IsNot Nothing And _subsidiariesList.Rows.Count > 0 Then
                    ddlSubsidiariesList.SelectedValue = Me.CompanyID.ToString()
                    If _subsidiariesList.Rows.Count = 1 Then
                        ddlSubsidiariesList.Visible = False
                        lblSubsidiaries.Visible = False
                    End If
                End If
            Catch ex As Exception
                lblMessage.Text = ex.Message
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Visible = True
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' To load web remittance history data by logged in compnay id
        ''' </summary>
        Private Sub LoadRemittanceHistoryData()
            Try
                Dim sql As New StringBuilder()
                sql.AppendFormat("{0}..spGetWebRemittanceHistory__c @LoggedInUserCompanyId={1}", Me.Database, Me.CompanyID)
                ''' Added by Saurabh for #21277 Start Here
                Dim dcolUrl As DataColumn = New DataColumn()
                dcolUrl.Caption = "InvoiceReport"
                dcolUrl.ColumnName = "Report"
                _webRemittanceHistory.Columns.Add(dcolUrl)
                ''End Here
                _webRemittanceHistory = Me.DataAction.GetDataTable(sql.ToString(), 600)
            Catch ex As Exception
                lblMessage.Text = ex.Message
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Visible = True
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region
        ''' Added by Saurabh for #21277 Start Here
        Private Sub GetReport(ByVal WebRemittanceNumber As String)
            Try
                Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "BFP Firm Invoice Report"))
                Dim rptParam As New AptifyCrystalReport__c
                rptParam.ReportID = ReportID
                rptParam.Param1 = WebRemittanceNumber
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPageURL & "' )", True)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub gvWebRemittanceHistory_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gvWebRemittanceHistory.ItemCommand
            Try
                If TypeOf e.Item Is GridDataItem Then
                    Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                    If e.CommandName = "InvoiceReport" Then

                        Dim webRemittanceNumber As String = e.CommandArgument.ToString().Substring(9)
                        Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
                        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "BFP Firm Invoice Report"))
                        Dim rptParam As New AptifyCrystalReport__c
                        rptParam.ReportID = ReportID
                        rptParam.Param1 = webRemittanceNumber
                        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPageURL & "' )", True)

                    End If
                End If
            Catch ex As Exception
                lblMessage.Text = ex.Message
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Visible = True
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub
        ''Saurabh for #21277 End Here 
        Protected Sub gvWebRemittanceHistory_RowDrop(sender As Object, e As GridDragDropEventArgs) Handles gvWebRemittanceHistory.RowDrop

        End Sub

    End Class

End Namespace

