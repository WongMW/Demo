Option Explicit On
Option Strict On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.Security
Imports System.Data
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class OrderHistoryControl
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_ORDER_CONFIRMATION_PAGE As String = "OrderConfirmationURL"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "OrderHistory"
Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage"
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
#Region "OrderHistory Specific Properties"
        ''' <summary>
        ''' OrderConfirmation page url
        ''' </summary>
        Public Overridable Property OrderConfirmationURL() As String
            Get
                If Not ViewState(ATTRIBUTE_ORDER_CONFIRMATION_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ORDER_CONFIRMATION_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ORDER_CONFIRMATION_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not IsPostBack Then
                'Anil B for issue 15302 on 23/04/2013
                LoadGrid()
                'Suraj Issue 14450 3/22/13 ,this method use to apply the odrering of rad grid first column
                'AddExpressionOrderHistory()
            End If
        End Sub

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(OrderConfirmationURL) Then
                OrderConfirmationURL = Me.GetLinkValueFromXML(ATTRIBUTE_ORDER_CONFIRMATION_PAGE)
                If String.IsNullOrEmpty(OrderConfirmationURL) Then
                    Me.grdMain.Enabled = False
                    Me.grdMain.ToolTip = "OrderConfirmationURL property has not been set."
                End If
            End If
If String.IsNullOrEmpty(ReportPage) Then
                ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
            End If
        End Sub

        Private Sub LoadGrid()
            ' load the grid with the user's past ordere history
            Dim sSQL As String, dt As Data.DataTable
            Try
                'Suraj S Issue 15195 ,4/8/13 ,amount field provide a comma eg:1000 to 1,000
                sSQL = "SELECT o.ID,OrderDate,ShipDate,CurrencyType,CALC_GrandTotal=CONVERT(VARCHAR,CALC_GrandTotal,1),ShipTrackingNum,ShipType,ost.Name OrderStatus " & _
                       "FROM " & Database & _
                       "..vwOrders o INNER JOIN " & _
                       Database & _
                       "..vwOrderStatusTypes ost ON o.OrderStatusID=ost.ID  " & _
                       "WHERE BillToID=" & User1.PersonID
                If CLng(cmbOrderType.SelectedItem.Value) > 0 Then
                    sSQL = sSQL & " AND OrderTypeID=" & cmbOrderType.SelectedItem.Value
                End If
                'sSQL = sSQL & " ORDER BY o.OrderDate DESC, o.ID DESC"
		sSQL = sSQL & " AND RTRIM(ost.Name) = 'Shipped' AND OrderDate >= DATEADD(YEAR, -3, GETDATE()) " & " ORDER BY o.OrderDate DESC, o.ID DESC"

                dt = DataAction.GetDataTable(sSQL)
                'Navin Prasad Issue 11032
                Dim hlink As GridHyperLinkColumn = CType(grdMain.Columns(0), GridHyperLinkColumn)
                hlink.DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
                'DirectCast(grdMain.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"

                grdMain.DataSource = dt
                '' grdMain.DataBind()
		''Sachin Sathe Added below code
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        btnPrint.Visible = True
                    Else
                        btnPrint.Visible = False
                    End If
                End If
                ''End
		
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub cmbOrderType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOrderType.SelectedIndexChanged
            ''LoadGrid()
            grdMain.Rebind()
            'Suraj Issue 14450 3/22/13 ,this method use to apply the odrering of rad grid first column
            AddExpressionOrderHistory()
        End Sub
        'Suraj issue 14450 2/12/13 date time filtering
        Protected Sub grdMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemDataBound
            Dim dateColumns As New List(Of String)
            'Add datecolumn uniqueName in list for Date format
            dateColumns.Add("GridDateTimeColumnOrderDate")
            CommonMethods.FormatedDateOnGrid(dateColumns, e.Item)
            'Suraj Issue 14450 3/22/13 ,we provide a tool tip for DatePopupButton as well as the GridDateTimeColumnStartDate textbox   
            If TypeOf e.Item Is GridFilteringItem Then
                Dim filterItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
                Dim gridDateTimeColumnStartDate As RadDatePicker = DirectCast(filterItem("GridDateTimeColumnOrderDate").Controls(0), RadDatePicker)
                gridDateTimeColumnStartDate.ToolTip = "Enter a filter date"
                gridDateTimeColumnStartDate.DatePopupButton.ToolTip = "Select a filter date"
            End If
	    'Sachin Sathe added below code
            If TypeOf e.Item Is GridDataItem Then
                Dim hl As HyperLink = DirectCast(DirectCast(e.Item, GridDataItem)("OrderID").Controls(0), HyperLink)
                If Not String.IsNullOrEmpty(hl.NavigateUrl) Then
                    hl.Enabled = False

                End If
            End If
            'End

        End Sub

        Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As GridPageChangedEventArgs) Handles grdMain.PageIndexChanged
            ''grdMain.PageIndex = e.NewPageIndex
            LoadGrid()
        End Sub
        Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As GridPageSizeChangedEventArgs) Handles grdMain.PageSizeChanged
            ''grdMain.PageIndex = e.NewPageIndex
            LoadGrid()
        End Sub
        Protected Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
            LoadGrid()
            ''grdStudents.Rebind()
        End Sub
        'Suraj Issue 14450 3/22/13 ,if the grid load first time By default the sorting will be Ascending for column Forum 
        Private Sub AddExpressionOrderHistory()
            Dim expression1 As New GridSortExpression
            expression1.FieldName = "ID"
            expression1.SetSortOrder("Ascending")
            grdMain.MasterTableView.SortExpressions.AddSortExpression(expression1)
        End Sub
 
Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
            Try
                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "InvoiceStandardWeb__cai"))
                Dim rptParam As New AptifyCrystalReport__c
                rptParam.ReportID = ReportID
                rptParam.Param1 = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID"))
                ''rptParam.SubParam1 = Me.OrderID
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


	  Protected Sub grdMain_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdMain.ItemCommand
            Try
                Dim item As GridDataItem = grdMain.Items(CInt(e.Item.ItemIndex.ToString()))
                Dim oOrderID As Integer = CInt(CType(item("OrderID").Controls(CInt("0")), HyperLink).Text)
                Select Case e.CommandName
                    Case "Invoicereport"
                        If oOrderID > 0 Then
                            ShowInvoiceReport(oOrderID)
                        End If

                    Case "Paymentreceipt"

                        If oOrderID > 0 Then
                            ShowPaymentReceiptReport(oOrderID)
                        End If
                End Select
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub ShowInvoiceReport(oOrderID As Integer)
            Try
                If oOrderID > 0 Then
                    Dim dt As DataTable
                    Dim sSql As String
                    sSql = Database & "..spGetProductCategoryFromOrder__c"
                    Dim param(0) As IDataParameter
                    param(0) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, oOrderID)
                    dt = Me.DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        If dt.Rows.Count > 0 Then
                            If CInt(dt.Rows(0)(0)) = 1 Then

                                Dim sInvoiceSql As String = Database & "..spGetInvoiceNumberByOrderID__c @OrderID=" & Convert.ToInt32(oOrderID)
                                Dim InvoiceNo As String = Convert.ToString(DataAction.ExecuteScalar(sInvoiceSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
                                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "InvoiceStandardWeb__cai"))
                                Dim rptParam As New AptifyCrystalReport__c
                                rptParam.ReportID = ReportID
                                rptParam.Param1 = ID
                                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPageURL & "' )", True)
                             Else

                                ShowStdInvoiceRpt(oOrderID)
                            End If
                        End If
			Else
                        ShowStdInvoiceRpt(oOrderID)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub ShowPaymentReceiptReport(oOrderID As Integer)
            Try
                If oOrderID > 0 Then
                    Dim dt As DataTable
                    Dim sSql As String
                    sSql = Database & "..spGetProductCategoryFromOrder__c"
                    Dim param(0) As IDataParameter
                    param(0) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, oOrderID)
                    dt = Me.DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        If dt.Rows.Count > 0 Then
                            If CInt(dt.Rows(0)(0)) = 0 Or CInt(dt.Rows(0)(0)) = 1 Then
                                 ETDPaymentReceiptRpt(oOrderID)
                               ' PaymentReceiptRpt(oOrderID)
                            End If
                        End If
 		    Else
                       PaymentReceiptRpt(oOrderID)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
	
  Private Sub ETDPaymentReceiptRpt(oOrderID As Integer)
            Try
                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "ETD Payment Receipt Report"))
                Dim rptParam As New AptifyCrystalReport__c
                Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
                rptParam.ReportID = ReportID
                rptParam.Param1 = Convert.ToString(oOrderID)
                rptParam.Param2 = "O"
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
End Sub
 Private Sub PaymentReceiptRpt(oOrderID As Integer)
            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Payment Receipt Report"))
            Dim rptParam As New AptifyCrystalReport__c
            rptParam.ReportID = ReportID
            rptParam.Param1 = Convert.ToString(oOrderID)
            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
        End Sub

  Private Sub ShowStdInvoiceRpt(oOrderID As Integer)
            Try
                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Invoice Statement"))
                Dim rptParam As New AptifyCrystalReport__c
                rptParam.ReportID = ReportID
                rptParam.Param1 = Convert.ToString(oOrderID)
                rptParam.Param2 = "O"
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
           
        End Sub
    End Class
End Namespace
