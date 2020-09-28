'Aptify e-Business 5.5.1, July 2013
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Applications.OrderEntry.Payments
Imports Aptify.Applications.Accounting
Imports Aptify.Framework.BusinessLogic.ProcessPipeline
Imports Telerik.Web.UI
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.AttributeManagement
Imports System.Data
Imports Aptify.Framework.Web.eBusiness
Imports System.ComponentModel


Namespace Aptify.Framework.Web.eBusiness.ProductCatalog

    Partial Class AdminPaymentSummary__c
        Inherits BaseUserControlAdvanced
        Dim sCuurSymbol As String
        Protected Const ATTRIBUTE_ADMIN_ORDER_DETAIL As String = "AdminOrderDetail"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage"


        Public Overridable Property AdminOrderDetail() As String
            Get
                If Not ViewState(ATTRIBUTE_ADMIN_ORDER_DETAIL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ADMIN_ORDER_DETAIL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ADMIN_ORDER_DETAIL) = Me.FixLinkForVirtualPath(value)
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

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(AdminOrderDetail) Then
                'since value is the 'default' check the XML file for possible custom setting
                AdminOrderDetail = Me.GetLinkValueFromXML(ATTRIBUTE_ADMIN_ORDER_DETAIL)
                If String.IsNullOrEmpty(AdminOrderDetail) Then
                    Me.grdOrderSummary.Enabled = False
                    Me.grdOrderSummary.ToolTip = "OrderConfirmationURL property has not been set."
                End If
            End If
            If String.IsNullOrEmpty(ReportPage) Then
                ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
            End If
        End Sub
        Private Sub LoadOrders()

            If Session("sPayOrderID") IsNot Nothing AndAlso CStr(Session("sPayOrderID")) <> String.Empty Then
                
                Dim sSQL As String
                Dim sProducts As String = String.Empty
                Dim sPayOrderID As String = CStr(Session("sPayOrderID"))
                Dim params(1) As System.Data.IDataParameter
                params(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, CInt(User1.CompanyID))
                params(1) = DataAction.GetDataParameter("@OrderlineID", SqlDbType.VarChar, sPayOrderID)
              
                sSQL = Database & "..spGetPaymentDetailsforAdmin__c"
                Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                Session("dt") = dt
                If dt.Rows.Count > 0 Then
                    sCuurSymbol = dt.Rows(0).Item("CurrencySymbol").ToString()
                    ViewState("sCuurSymbol") = sCuurSymbol
                    grdOrderSummary.DataSource = dt
                End If
                Dim total As Decimal = 0.0
                For index = 0 To dt.Rows.Count - 1
                    total += CDbl(dt.Rows(index)("PayAmount"))
                Next
                setTotal(total, ViewState("sCuurSymbol"))
                  
            End If
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Neha, issue 14456, 03/15/13 ,added method for sort order(assending) for rad grid first column
            If Not IsPostBack Then
                If Session("OrderuniqueGuid") IsNot Nothing Then
                    lblReceiptNo.Text = Session("OrderuniqueGuid").ToString
                End If
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.FirmPortal.BillMeLaterNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                If Session("IsBillMeLater") IsNot Nothing Then
                    spnNote.Visible = True
                trReceiptNumber.visible=True
                Else
                    spnNote.Visible = False
                trReceiptNumber.visible=False
                End If
                AddExpression()
                LoadOrders()
            End If
            SetProperties()
        End Sub


        Protected Function GetFormattedCurrency(ByVal Container As Object, ByVal sField As String) As String
            Dim sCurrencySymbol As String
            Dim iNumDecimals As Integer
            Dim sCurrencyFormat As String

            Try
                ' get the appropriate currency data from the data row
                sCurrencySymbol = Container.DataItem("CurrencySymbol")
                iNumDecimals = Container.DataItem("NumDigitsAfterDecimal")

                ' build the string we'll use for formatting the currency
                ' it consists of the symbol followed by 0. and the appropriate
                ' number of decimals needed in the final string
                sCurrencyFormat = sCurrencySymbol.Trim & _
                                  "{0:" & "0." & _
                                  New String("0"c, iNumDecimals) & "}"

                ' format the string using the currency format created
                Return String.Format(sCurrencyFormat, Container.DataItem(sField))
            Catch ex As Exception
                Try
                    ' on failure, at least try and return the
                    ' data contents
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    Return Container.DataItem(sField)
                Catch ex2 As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex2)
                    Return "{ERROR}"
                End Try
            End Try
        End Function

        Private Class PayInfo
            Public OrderID As Long
            Public PayAmount As Decimal
            Public Balance As Decimal
        End Class

        Protected Sub cmdback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdback.Click
            MyBase.Response.Redirect(AdminOrderDetail, False)
        End Sub
        'Neha, issue 14456, 03/15/13, added needdatasource event for databinding, fitering,sorting rad grid
        Protected Sub grdOrderSummary_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdOrderSummary.NeedDataSource
            LoadOrders()
        End Sub
        'for assending order sorting on first time gridload(for first column) 
        Private Sub AddExpression()
            Dim expression1 As New GridSortExpression
            expression1.FieldName = "Name"
            expression1.SetSortOrder("Ascending")
            grdOrderSummary.MasterTableView.SortExpressions.AddSortExpression(expression1)
        End Sub

        Private Sub setTotal(ByVal iSumTotalPayAmmount As Decimal, ByVal sSymCurr As String)
            Dim iSumTotalPay As Decimal = 0

            If Not iSumTotalPayAmmount = 0 Then
                'suraj S Issue 16036 5/2/13, add comma for amount
                lblTotalPay.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPayAmmount)
            Else
                lblTotalPay.Text = sSymCurr & "0.00"
            End If
        End Sub

        Protected Sub btnReceipt_Click(sender As Object, e As EventArgs) Handles btnReceipt.Click
            Try
                Try
                    'below code added by Govind M
                    Dim sSQl As String = database & "..spGetISETDPaymentOrder__c @PaymentID=" & Convert.ToUInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Convert.ToString(Request.QueryString("PaymentID"))))
                    Dim isETDPayment As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSQl))
                    If isETDPayment Then
                        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "ETD Payment Receipt Report"))
                        Dim rptParam As New AptifyCrystalReport__c
                        rptParam.ReportID = ReportID
                        rptParam.Param1 = Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Convert.ToString(Request.QueryString("PaymentID"))))
                        rptParam.Param2 = "P"
                        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                    Else
                        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Payment Receipt Report"))
                        Dim rptParam As New AptifyCrystalReport__c
                        rptParam.ReportID = ReportID
                        rptParam.Param1 = Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Convert.ToString(Request.QueryString("PaymentID")))) 'Convert.ToString(Request.QueryString("ID"))
                        rptParam.Param2 = "P"
                        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                    End If
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace
