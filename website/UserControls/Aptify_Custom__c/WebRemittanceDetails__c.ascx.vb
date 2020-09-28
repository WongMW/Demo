'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
'Developer                  Date modified               Comments 
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Milind Sutar               25/03/2015                  Page to display remittance details 
'Sheela Jarali              21/06/2018                  Bug #19168: Web Remmittance Page - Does Not shows payment confirmation message
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On

Imports System.Data
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports System.Linq
Imports System.Drawing
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI.PivotGrid
Imports Telerik.Web.UI.PivotGrid.Core
Imports System.Windows.Threading
Imports System.IO
Imports Aptify.Applications.Accounting

Partial Class WebRemittanceDetails__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

#Region "Properties"

    Private _webRemittanceDetails As System.Data.DataTable
    Private _webRemittanceDetailsPaymentDetails As System.Data.DataTable
    Private _webRemittanceNumber As String
    Private _companyId As Integer

    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "WebRemittanceDetails__c"
    Protected Const ATTRIBUTE_WEB_REMITTANCE_HISTORY_PAGE_URL_NAME As String = "WebRemittanceHistoryURL"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage"

    ''' <summary>
    ''' To get/set web remittance number
    ''' </summary>
    Public ReadOnly Property WebRemittanceNumber() As String
        Get
            _webRemittanceNumber = CStr(Request.QueryString("Number"))
            Return _webRemittanceNumber
        End Get
    End Property


    ''' <summary>
    ''' To get/set web remittance number
    ''' </summary>
    Public ReadOnly Property CompanyID() As Integer
        Get
            _companyId = CInt(Request.QueryString("CId"))
            Return _companyId
        End Get
    End Property


    ''' <summary>
    ''' To get/set web remittance history page url 
    ''' </summary>
    Public Overridable Property WebRemittanceHistoryURL() As String
        Get
            If Not ViewState(ATTRIBUTE_WEB_REMITTANCE_HISTORY_PAGE_URL_NAME) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_WEB_REMITTANCE_HISTORY_PAGE_URL_NAME))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_WEB_REMITTANCE_HISTORY_PAGE_URL_NAME) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property ReportPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
#End Region

#Region "Protected Methods"

    ''' <summary>
    ''' Handles page load event
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Start #18731
        Dim _ScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        _ScriptManager.AsyncPostBackTimeout = 1000
        ' End
        If Not IsPostBack Then
            CreditCard.LoadCreditCardInfo()
            Me.SetProperties()
        End If
    End Sub

    ''' <summary>
    ''' To set properties
    ''' </summary>
    Protected Overrides Sub SetProperties()
        MyBase.SetProperties()
        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        If String.IsNullOrEmpty(WebRemittanceHistoryURL) Then
            Me.WebRemittanceHistoryURL = Me.GetLinkValueFromXML(ATTRIBUTE_WEB_REMITTANCE_HISTORY_PAGE_URL_NAME)
        End If
        If String.IsNullOrEmpty(ReportPage) Then
            ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
        End If
    End Sub

    ''' <summary>
    ''' Handles cell databound event
    ''' </summary>
    Protected Sub gvWebRemittanceDetails_CellDataBound(sender As Object, e As Telerik.Web.UI.PivotGridCellDataBoundEventArgs) Handles gvWebRemittanceDetails.CellDataBound
        If TypeOf e.Cell Is PivotGridRowHeaderCell Then
            If e.Cell.Controls.Count > 0 Then
                If e.Cell.Controls(0) IsNot Nothing Then
                    TryCast(e.Cell.Controls(0), System.Web.UI.WebControls.Button).Visible = False
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles need data source to bind data to pivot grid
    ''' </summary>
    Protected Sub gvWebRemittanceDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.PivotGridNeedDataSourceEventArgs) Handles gvWebRemittanceDetails.NeedDataSource
        LoadWebRemittanceDetails()
        If _webRemittanceDetails.Rows.Count > 0 Then
            Dim DistinctDt As System.Data.DataTable = _webRemittanceDetails.DefaultView.ToTable(True, "Curriculum")
            If DistinctDt.Rows.Count = _webRemittanceDetails.Rows.Count Then
                gvWebRemittanceDetails.ColumnGroupsDefaultExpanded = True
            End If
            gvWebRemittanceDetails.Visible = True
            gvWebRemittanceDetails.DataSource = _webRemittanceDetails
            IsPaidAlready()
        Else
            gvWebRemittanceDetails.Visible = False
            lblNoRecords.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.PivotGrid.NoRecords")), _
                Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            btnPrintInvoice.Visible = False
        End If
        If _webRemittanceDetails IsNot Nothing Then
            gvWebRemittanceDetails.DataSource = _webRemittanceDetails
        End If
    End Sub

    ''' <summary>
    ''' Handles pre - render event to load default setting
    ''' </summary>
    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If Not IsPostBack Then
            LoadDefaultSetting()
            IsPaidAlready()
        End If
    End Sub

    ''' <summary>
    ''' Handles make payment button click event
    ''' </summary>
    Protected Sub btnMakePayment_Click(sender As Object, e As System.EventArgs) Handles btnMakePayment.Click
        MakePayment()
    End Sub

    ''' <summary>
    ''' Handles export to exel button click event
    ''' </summary>
    Protected Sub btnExportToExcel_Click(sender As Object, e As System.EventArgs) Handles btnExportToExcel.Click
        ' LoadWebRemittanceDetails()
        ExportToExcel()
    End Sub

    ''' <summary>
    ''' Hnaldes back button click event - to redirect to history page
    ''' </summary>
    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(Me.WebRemittanceHistoryURL, False)
    End Sub

    Protected Sub btnReceipt_Click(sender As Object, e As EventArgs) Handles btnReceipt.Click
        Try
            Try
                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Group Payment Receipt"))
                Dim rptParam As New AptifyCrystalReport__c
                rptParam.ReportID = ReportID
                rptParam.Param1 = Convert.ToString(Session("PaymentID"))
                rptParam.Param2 = "P"
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    'Added as part of log 19168: Web Remmittance Page - Does Not shows payment confirmation message 
    Protected Sub btnSuccessOK_Click(sender As Object, e As EventArgs) Handles btnSuccessOK.Click
        radWindowSuccess.VisibleOnPageLoad = False
        Response.Redirect(Request.RawUrl)
    End Sub
#End Region

#Region "Private Methods"


    ''' <summary>
    ''' To export data table to excel
    ''' </summary>
    Private Sub ExportToExcel()
        'Dim _webRemittanceDetailsExport As DataTable
        'Dim sql As New StringBuilder()
        'sql.AppendFormat("{0} ..spGetWebRemittanceDetailsExport__c @WebRemittanceNumber='{1}',@CompanyID={2}", Me.Database, Me.WebRemittanceNumber, Me.CompanyID)
        '_webRemittanceDetailsExport = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        'If _webRemittanceDetailsExport.Rows.Count > 0 Then
        '    Dim filename As String = "WebRemittanceDetails.xls"
        '    Dim tw As New System.IO.StringWriter()
        '    Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        '    Dim dgGrid As New DataGrid()
        '    dgGrid.DataSource = _webRemittanceDetailsExport
        '    dgGrid.DataBind()
        '    dgGrid.RenderControl(hw)
        '    Response.ContentType = "application/vnd.ms-excel"
        '    Response.AppendHeader("Content-Disposition", (Convert.ToString("attachment; filename=") & filename) + "")
        '    Me.EnableViewState = False
        '    Response.Write(tw.ToString())
        '    Response.[End]()
        'End If

        ' Dim alternateText As String = TryCast(sender, ImageButton).AlternateText
        '  gvWebRemittanceDetails.ExportSettings.Excel.Format = DirectCast([Enum].Parse(GetType(PivotGridExcelFormat), "xls"), PivotGridExcelFormat)
        gvWebRemittanceDetails.ExportSettings.IgnorePaging = True
        gvWebRemittanceDetails.ExpandAllColumnGroups(False)
        gvWebRemittanceDetails.ExportToExcel()
    End Sub

    ''' <summary>
    ''' To make a payment for all order line of the web remittance number
    ''' </summary>
    Private Sub MakePayment()
        Try
            lblPaymentMessage.Text = String.Empty
            Dim errorMessage As String = String.Empty
            Dim result As Boolean = True
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetWebRemittancePaymentDetails__c @WebRemittanceNumber='{1}',@CompanyID={2}", Me.Database, Me.WebRemittanceNumber, Me.CompanyID)
            _webRemittanceDetailsPaymentDetails = Me.DataAction.GetDataTable(sql.ToString(), _
                Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If _webRemittanceDetailsPaymentDetails IsNot Nothing Then
                If _webRemittanceDetailsPaymentDetails.Rows.Count > 0 Then
                    Dim billToId As Integer = CInt(_webRemittanceDetailsPaymentDetails.Rows.Item(0).Item("BillToID"))
                    Dim employeeId As Integer = CInt(_webRemittanceDetailsPaymentDetails.Rows.Item(0).Item("EmployeeID"))
                    Dim currencyId As Integer = CInt(_webRemittanceDetailsPaymentDetails.Rows.Item(0).Item("CurrencyTypeID"))
                    Dim paymentLevelId As Integer = CInt(_webRemittanceDetailsPaymentDetails.Rows.Item(0).Item("PaymentLevelID"))
                    Dim payment As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = _
                                     Me.AptifyApplication.GetEntityObject("Payments", -1)
                    With payment
                        .SetValue("PersonID", loggedInUser.PersonID)
                        .SetValue("CompanyID", loggedInUser.CompanyID)
                        .SetValue("EmployeeID", employeeId)
                        .SetValue("PaymentDate", Date.Today)
                        .SetValue("EffectiveDate", Date.Today)
                        .SetValue("DepositDate", Date.Today)
                        .SetValue("CurrencyTypeID", currencyId)
                        .SetValue("PaymentLevelID", paymentLevelId)
                        .SubTypes("PaymentLines").Clear()
                        .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                        If CreditCard.CCNumber = "-Ref Transaction-" Then
                            .SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                            .SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                        End If
                        .SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber)
                        For Each row As DataRow In _webRemittanceDetailsPaymentDetails.Rows
                            With .SubTypes("PaymentLines").Add()
                                .SetValue("BillToCompanyID", loggedInUser.CompanyID)
                                .SetValue("AppliesTo", "Order Line")
                                .SetValue("OrderID", row("OrderID"))
                                .SetValue("OrderDetailID", row("OrderDetailID"))
                                .SetValue("LineNumber", row("Sequence"))
                                .SetValue("Amount", row("Amount"))
                            End With
                        Next
                        .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                        .SetValue("CCAccountNumber", CreditCard.CCNumber)
                        .SetValue("CCExpireDate", CreditCard.CCExpireDate)
                        If .Fields("PaymentInformationID").EmbeddedObjectExists Then
                            Dim payInformation As PaymentInformation = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                            payInformation.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                            payInformation.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                            payInformation.SetValue("CCPartial", CreditCard.CCPartial)
                        End If
                        result = .Save(errorMessage)
                        If result = True Then
                            Session("PaymentID") = payment.RecordID
                            btnReceipt.Visible = True
                        End If
                    End With
                    If result = True Then
                        'Modified by Sheela as part of log #19168
                        lblPaymentMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                            Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebRemittanceDetails.Success")),
                            Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        lblPaymentMessage.ForeColor = Color.Green
                        lblPaymentMessage.Visible = True
                        CreditCard.CCSecurityNumber = String.Empty
                        CreditCard.CCNumber = String.Empty
                        lblValidation.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebRemittanceDetails.Success")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        radWindowSuccess.VisibleOnPageLoad = True
                        CreditCard.Visible = False
                        'Response.Redirect(Request.RawUrl)
                    ElseIf result = False Then
                        lblPaymentMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebRemittanceDetails.Failed")), _
                            Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        lblPaymentMessage.ForeColor = Color.Red
                        lblPaymentMessage.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            lblPaymentMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebRemittanceDetails.Failed")), _
                            Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblPaymentMessage.ForeColor = Color.Red
            lblPaymentMessage.Visible = True
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To load remittance details
    ''' </summary>
    Private Sub LoadWebRemittanceDetails()
        Try
            lblNoRecords.Text = String.Empty
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetWebRemittanceDetails__c @WebRemittanceNumber='{1}',@CompanyID={2}", Me.Database, Me.WebRemittanceNumber, Me.CompanyID)
            _webRemittanceDetails = Me.DataAction.GetDataTable(sql.ToString(), 1000, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To check if payment is already paid
    ''' </summary>
    Private Sub IsPaidAlready()
        Try
            btnExportToExcel.Visible = False
            If _webRemittanceDetails IsNot Nothing And _webRemittanceDetails.Rows.Count > 0 Then
                Dim isPaidAlready As Boolean = CBool(_webRemittanceDetails.Rows.Item(0).Item("AlreadyPaid"))
                paymentTable.Visible = True
                btnExportToExcel.Visible = True
                If isPaidAlready Then
                    paymentTable.Visible = False
                    btnReceipt.Visible = True
                    Session("PaymentID") = Convert.ToString(_webRemittanceDetails.Rows.Item(0).Item("PaymentID"))
                End If
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' to do the default settings on reload
    ''' </summary>
    Private Sub LoadDefaultSetting()
        Try
            If _webRemittanceDetails.Rows.Count > 0 Then
                gvWebRemittanceDetails.ColumnGroupsDefaultExpanded = True
                Dim curriculumTable As System.Data.DataTable = _webRemittanceDetails.DefaultView.ToTable(True, "Curriculum")
                Dim curriculums As New List(Of String)()
                Dim cout As Integer = 0
                For Each row As DataRow In curriculumTable.Rows
                    If cout <> 0 Then
                        gvWebRemittanceDetails.CollapsedColumnIndexes.Add(New String() {row("Curriculum").ToString()})
                    End If
                    cout = cout + 1
                Next
            End If
            gvWebRemittanceDetails.Rebind()
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            lblMessage.Visible = True
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

    Protected Sub btnPrintInvoice_Click(sender As Object, e As EventArgs) Handles btnPrintInvoice.Click
        Try
            Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "BFP Firm Invoice Report"))
            Dim rptParam As New AptifyCrystalReport__c
            rptParam.ReportID = ReportID
            rptParam.Param1 = Me.WebRemittanceNumber
            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPageURL & "' )", True)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub



    Protected Sub gvWebRemittanceDetails_PivotGridCellExporting(sender As Object, e As PivotGridCellExportingArgs) Handles gvWebRemittanceDetails.PivotGridCellExporting
        'AddBorders(e)

        Dim modelDataCell As PivotGridBaseModelCell = TryCast(e.PivotGridModelCell, PivotGridBaseModelCell)
        If modelDataCell IsNot Nothing Then
            '  AddStylesToDataCells(modelDataCell, e)
        End If

        If modelDataCell.TableCellType = PivotGridTableCellType.RowHeaderCell Then
            AddStylesToRowHeaderCells(modelDataCell, e)
        End If

        If modelDataCell.TableCellType = PivotGridTableCellType.ColumnHeaderCell Then
            AddStylesToColumnHeaderCells(modelDataCell, e)
        End If

        If modelDataCell.IsGrandTotalCell Then
            e.ExportedCell.Style.BackColor = Color.FromArgb(128, 128, 128)
            e.ExportedCell.Style.Font.Bold = True
        End If

        If IsTotalDataCell(modelDataCell) Then
            e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150)
            e.ExportedCell.Style.Font.Bold = True
            AddBorders(e)
        End If

        If IsGrandTotalDataCell(modelDataCell) Then
            e.ExportedCell.Style.BackColor = Color.FromArgb(128, 128, 128)
            e.ExportedCell.Style.Font.Bold = True
            AddBorders(e)
        End If
    End Sub

    Private Sub AddStylesToRowHeaderCells(modelDataCell As PivotGridBaseModelCell, e As PivotGridCellExportingArgs)
        If e.ExportedCell.Table.Columns(e.ExportedCell.ColIndex).Width = 0 Then
            e.ExportedCell.Table.Columns(e.ExportedCell.ColIndex).Width = 90.0
        End If
        If modelDataCell.IsTotalCell Then
            e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150)
            e.ExportedCell.Style.Font.Bold = True
        Else
            e.ExportedCell.Style.BackColor = Color.FromArgb(192, 192, 192)
        End If

        AddBorders(e)
    End Sub
    Private Function IsTotalDataCell(modelDataCell As PivotGridBaseModelCell) As Boolean
        Return modelDataCell.TableCellType = PivotGridTableCellType.DataCell AndAlso (modelDataCell.CellType = PivotGridDataCellType.ColumnTotalDataCell OrElse modelDataCell.CellType = PivotGridDataCellType.RowTotalDataCell OrElse modelDataCell.CellType = PivotGridDataCellType.RowAndColumnTotal)
    End Function

    Private Function IsGrandTotalDataCell(modelDataCell As PivotGridBaseModelCell) As Boolean
        Return modelDataCell.TableCellType = PivotGridTableCellType.DataCell AndAlso (modelDataCell.CellType = PivotGridDataCellType.ColumnGrandTotalDataCell OrElse modelDataCell.CellType = PivotGridDataCellType.ColumnGrandTotalRowTotal OrElse modelDataCell.CellType = PivotGridDataCellType.RowGrandTotalColumnTotal OrElse modelDataCell.CellType = PivotGridDataCellType.RowGrandTotalDataCell OrElse modelDataCell.CellType = PivotGridDataCellType.RowAndColumnGrandTotal)
    End Function

    Private Sub AddStylesToColumnHeaderCells(modelDataCell As PivotGridBaseModelCell, e As PivotGridCellExportingArgs)
        If e.ExportedCell.Table.Columns(e.ExportedCell.ColIndex).Width = 0 Then
            e.ExportedCell.Table.Columns(e.ExportedCell.ColIndex).Width = 200.0
        End If

        If modelDataCell.IsTotalCell Then
            e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150)
            e.ExportedCell.Style.Font.Bold = True
        Else
            e.ExportedCell.Style.BackColor = Color.FromArgb(192, 192, 192)
        End If
        AddBorders(e)
    End Sub


    Private Sub AddBorders(e As PivotGridCellExportingArgs)
        e.ExportedCell.Style.BorderBottomColor = Color.FromArgb(128, 128, 128)
        e.ExportedCell.Style.BorderBottomWidth = New Unit(1)
        e.ExportedCell.Style.BorderBottomStyle = BorderStyle.Solid

        e.ExportedCell.Style.BorderRightColor = Color.FromArgb(128, 128, 128)
        e.ExportedCell.Style.BorderRightWidth = New Unit(1)
        e.ExportedCell.Style.BorderRightStyle = BorderStyle.Solid

        e.ExportedCell.Style.BorderLeftColor = Color.FromArgb(128, 128, 128)
        e.ExportedCell.Style.BorderLeftWidth = New Unit(1)
        e.ExportedCell.Style.BorderLeftStyle = BorderStyle.Solid

        e.ExportedCell.Style.BorderTopColor = Color.FromArgb(128, 128, 128)
        e.ExportedCell.Style.BorderTopWidth = New Unit(1)
        e.ExportedCell.Style.BorderTopStyle = BorderStyle.Solid
    End Sub

End Class
