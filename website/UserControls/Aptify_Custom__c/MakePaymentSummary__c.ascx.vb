''Aptify e-Business 5.5.1, July 2013
''Updated By Asmita and Vaishali for member protal
Option Explicit On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports System.Data
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports Aptify.Applications.Accounting
Imports Aptify.Applications.OrderEntry
Imports Aptify.Framework.Web.Common.RSS
Imports System.Collections

Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class MakePaymentSummary__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_ORDER_CONFIRMATION_PAGE As String = "OrderConfirmationURL"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "MakePaymentSummary"
        Protected Const ATTRIBUTE_MAKE_PAYMENT_PAGE As String = "MakePaymentPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage"
        Private total As Decimal = 0
        Dim sCuurSymbol As String
        Dim iOrderId As Integer = 0

#Region "MakePayment Specific Properties"
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

        ''' <summary>
        ''' MakePayment page url
        ''' </summary>
        Public Overridable Property MakePaymentPage() As String
            Get
                If Not ViewState(ATTRIBUTE_MAKE_PAYMENT_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_MAKE_PAYMENT_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_MAKE_PAYMENT_PAGE) = Me.FixLinkForVirtualPath(value)
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1))
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoStore()
            'set control properties from XML file if needed
            SetProperties()
            'Start Added By GM 16/02/2018 for Duplicate payment issue #18731
            Dim _ScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            _ScriptManager.AsyncPostBackTimeout = "2000" ' Updated as part of 20995
            ' End GM
            Try
                If Not IsPostBack Then
                    'By Vaishali
                    If Not String.IsNullOrEmpty(Request.QueryString("msg")) And Request.QueryString("msg") = "1" AndAlso IsNothing(Session("IsError")) Then
                        Session("orderdt") = Nothing
                        btnPrint.Visible = True
                        btnReceipt.Visible = True
                        CreditCard.Visible = False
                        cmdPay.Visible = False
                        lblMessage.Text = "Payment was made!  Your updated order information is shown below."
                        'lblMessage.ForeColor = Drawing.Color.DarkGreen
                        lblMessage.Visible = True
                        paymentMade.Visible = True
                        btnReceipt.Visible = True
                    Else
                        Session("ShippingOrders") = Nothing
                        Session("PaidOrderIDs") = Nothing
                        Session("PaymentID") = Nothing
                    End If
                    If Session("orderdt") IsNot Nothing Then
                        ViewState("orderdt") = Session("orderdt")
                    End If
                    If Session("PaidOrderIDs") Is Nothing And Session("orderdt") Is Nothing Then
                        Response.Redirect(MakePaymentPage, False)
                        Return
                    End If
                    '--------------------------------------------
                    CreditCard.LoadCreditCardInfo()
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
            If String.IsNullOrEmpty(ReportPage) Then
                ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
            End If

            If String.IsNullOrEmpty(OrderConfirmationURL) Then
                OrderConfirmationURL = Me.GetLinkValueFromXML(ATTRIBUTE_ORDER_CONFIRMATION_PAGE)
                If String.IsNullOrEmpty(OrderConfirmationURL) Then
                    'Navin Prasad Issue 11032
                    Me.grdMain.Columns.RemoveAt(0)
                    grdMain.Columns.Insert(0, New Telerik.Web.UI.GridBoundColumn())
                    With DirectCast(grdMain.Columns(0), Telerik.Web.UI.GridBoundColumn)
                        .UniqueName = "ID"
                        .DataField = "ID"
                        .HeaderText = "Order #"
                        .ItemStyle.ForeColor = Drawing.Color.Blue
                        .ItemStyle.Font.Underline = True
                    End With
                    Me.grdMain.ToolTip = "OrderConfirmationURL property has not been set."
                    Me.radgShippingCharges.ToolTip = "OrderConfirmationURL property has not been set."
                Else
                    Dim hlink As Telerik.Web.UI.GridHyperLinkColumn = CType(grdMain.Columns(0), Telerik.Web.UI.GridHyperLinkColumn)
                    hlink.DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"

                    Dim hShippinglink As Telerik.Web.UI.GridHyperLinkColumn = CType(grdMain.Columns(0), Telerik.Web.UI.GridHyperLinkColumn)
                    hShippinglink.DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"

                    'DirectCast(grdMain.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
                End If
            Else
                Dim hlink As Telerik.Web.UI.GridHyperLinkColumn = CType(grdMain.Columns(0), Telerik.Web.UI.GridHyperLinkColumn)
                hlink.DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
                Dim hShippinglink As Telerik.Web.UI.GridHyperLinkColumn = CType(grdMain.Columns(0), Telerik.Web.UI.GridHyperLinkColumn)
                hShippinglink.DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
                'DirectCast(grdMain.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
            End If
            If String.IsNullOrEmpty(MakePaymentPage) Then
                MakePaymentPage = Me.GetLinkValueFromXML(ATTRIBUTE_MAKE_PAYMENT_PAGE)
                If String.IsNullOrEmpty(MakePaymentPage) Then
                    Me.btnBack.ToolTip = "Payment Summary URL property has not been set."
                End If
            End If
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

        Protected Function GetFormattedCurrencyNoSymbol(ByVal Container As Object, ByVal sField As String) As String
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
                sCurrencyFormat = "{0:" & "0." & _
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

        Private Sub LoadOrders()
            'Dim dtOrder As Data.DataTable
            'Dim dtProducts As DataTable
            'Dim sSQL As String
            Dim sProducts As String = String.Empty
            ' Dim i As Integer, j As Integer
            Dim icount As Integer = 0
            Dim iProdCnt As Integer = 0
            Dim dtOrders As DataTable
            Try
                If Not Session("PaidOrderIDs") Is Nothing Then
                    Dim sOrderLines As String = CType(Session("PaidOrderIDs"), String)
                    dtOrders = DataAction.GetDataTable(Database + "..spGetPaidOrdersByOrderLines__c @OrderlineID='" + sOrderLines + "'", IAptifyDataAction.DSLCacheSetting.BypassCache)
                    ViewState("orderdt") = dtOrders
                ElseIf Not ViewState("orderdt") Is Nothing Then
                    'Add selected order in dictionary
                    Dim orderdetails As New Dictionary(Of Integer, Decimal)
                    If ViewState("CHECKED_PAYMENTS") IsNot Nothing Then
                        orderdetails = DirectCast(ViewState("CHECKED_PAYMENTS"), Dictionary(Of Integer, Decimal))
                    End If
                    If orderdetails Is Nothing Or orderdetails.Count <= 0 Then
                        txtTotal.Text = sCuurSymbol & "0.00"
                    End If
                    dtOrders = ViewState("orderdt")
                End If

                If dtOrders IsNot Nothing AndAlso dtOrders.Rows.Count > 0 Then
                    sCuurSymbol = dtOrders.Rows(0).Item("CurrencySymbol").ToString()
                    ViewState("sCuurSymbol") = sCuurSymbol
                    txtTotal.Text = sCuurSymbol & "0.00"
                    grdMain.DataSource = dtOrders
                    LoadShippingGrid()
                    hidPaymentTotal.Value = dtOrders.Compute("SUM(PayAmount)", String.Empty)
                    txtTotal.Text = sCuurSymbol & String.Format("{0:n2}", Convert.ToDecimal(hidPaymentTotal.Value))
                    txtTotal.Visible = True
                Else
                    hidPaymentTotal.Value = "0"
                    lblTotal.Visible = False
                    txtTotal.Visible = False
                    CreditCard.Visible = False
                End If
                Dim sSymCurr As String = "€    "

                If Not ViewState("sCuurSymbol") Is Nothing Then
                    sSymCurr = ViewState("sCuurSymbol").ToString().Trim()
                End If

                lblTotalPayment.Text = sSymCurr & String.Format("{0:n2}", Convert.ToDecimal(hidPaymentTotal.Value.Trim) + Convert.ToDecimal(hidShippingTotal.Value.Trim))
                If Convert.ToDecimal(hidShippingTotal.Value.Trim) = 0 Then
                    txtTotal.Visible = False
                    lblTotal.Visible = False
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Class PayInfo
            Public OrderID As Long
            Public PayAmount As Decimal
            Public Balance As Decimal
            Public OrderLineID As Long
            Public LineNumber As Integer
            Public IsDonation As Integer
            Public CharteredOrderLineID As Long
        End Class

        Private Class PayShippingInfo
            Public OrderID As Long
            Public OrderShipmentID As Long
            Public PayAmount As Decimal
            Public Balance As Decimal
        End Class
        ' code added for splitting orser
        Private Sub checkBillingCompany()
            Try
                If Session("orderdt") IsNot Nothing Then
                    Dim dt As DataTable = CType(Session("orderdt"), DataTable)
                    Dim view As New DataView(dt)

                    Dim TobeDistinct As String() = {"ID", "OrdetType"}

                    ' Dim distinctValues As DataTable = view.ToTable(True, "ID")
                    Dim distinctValues As DataTable = view.ToTable(True, TobeDistinct)

                    Dim sOrderID As String = String.Empty
                    For Each dr As DataRow In distinctValues.Rows
                        ' only Quatation type orders

                        Dim drOrderLine() As DataRow = dt.Select("ID=" & Convert.ToInt32(dr("ID")))
                        Dim sOrderlineID As String = String.Empty
                        If drOrderLine.Length > 0 Then
                            For i As Integer = 0 To drOrderLine.Length - 1
                                If sOrderlineID = "" Then
                                    sOrderlineID = Convert.ToString(drOrderLine(i)(3))
                                Else

                                    sOrderlineID = sOrderlineID + "," + Convert.ToString(drOrderLine(i)(3))
                                End If
                            Next
                        End If
                        'Dim sSqlQuotation As String = Database & "..spCheckOrderIsQuotation__c @OrderID='" & Convert.ToString(dr("ID")) & "',@OrderLine='" & sOrderlineID & "'"
                        'Dim dtNewOrder As DataTable = DataAction.GetDataTable(sSqlQuotation, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        Dim dtNewOrder As DataTable = Nothing
                        ''Added By Pradip 2017-04-06 to check quatation order
                        If Convert.ToString(dr("OrdetType")).Trim = "Quotation" Then
                            Dim sSqlQuotation As String = Database & "..spCheckOrderIsQuotation__c @OrderID='" & Convert.ToString(dr("ID")) & "',@OrderLine='" & sOrderlineID & "'"
                            dtNewOrder = DataAction.GetDataTable(sSqlQuotation, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        End If
                        Dim oOrderGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                        oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(dr("ID")))
                        If Not dtNewOrder Is Nothing AndAlso dtNewOrder.Rows.Count > 0 Then

                            Dim oNewOrderGE As Aptify.Applications.OrderEntry.OrdersEntity
                            oNewOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                            oNewOrderGE.ShipToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                            oNewOrderGE.BillToID = Convert.ToInt32(oOrderGE.GetValue("BillToID"))
                            ' Redmin issue #16086
                            Dim sSQL As String = Database & "..spGetMemAppSplitOrder__c @OrderID=" & Convert.ToInt32(dr("ID"))
                            Dim iMemApp As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            ' Dim sZeroQuantityOrderLineIDs As String = String.Empty

                            For Each drRow As DataRow In dtNewOrder.Rows
                                oNewOrderGE.AddProduct(CLng(drRow("ProductID")))
                                ' oNewOrderGE.SubTypes("OrderLines").Item(oNewOrderGE.SubTypes("OrderLines").Count - 1).SetValue("BillToCompanyID__c", oOrderGE.GetValue(("ShipToCompanyID")))
                                If Convert.ToInt32(Convert.ToString(drRow("BillToCompanyID__c"))) > 0 Then
                                    oNewOrderGE.SubTypes("OrderLines").Item(oNewOrderGE.SubTypes("OrderLines").Count - 1).SetValue("BillToCompanyID__c", Convert.ToInt32(Convert.ToString(drRow("BillToCompanyID__c"))))
                                End If
                                oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(drRow("Sequence")) - 1).SetValue("Quantity", 0)

                                oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(drRow("Sequence")) - 1).SetAddValue("_xDeletedOrderlines", 1)
                                oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(drRow("Sequence")) - 1).Delete()

                                'If sZeroQuantityOrderLineIDs = "" Then
                                '    sZeroQuantityOrderLineIDs = Convert.ToString(CLng(drRow("Sequence")))
                                'Else
                                '    sZeroQuantityOrderLineIDs = sZeroQuantityOrderLineIDs + "," + Convert.ToString(CLng(drRow("Sequence")))
                                'End If
                                ' oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(drRow("Sequence")) - 1).Delete()
                            Next
                            oOrderGE.SetAddValue("_xDeletedOrderlines", 1)
                            If Convert.ToInt32(oOrderGE.GetValue("BillToCompanyID")) > 0 Then
                                oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                'oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                oOrderGE.SetValue("BillToCompanyID", -1)
                                ' oOrderGE.SetValue("OrderTypeID", 4)
                                oOrderGE.SetValue("PayTypeID", 1)
                                oOrderGE.SetValue("FirmPay__c", 0)
                            End If
                            'redmine 16921
                            If Session("EID") IsNot Nothing AndAlso Convert.ToInt32(Session("EID")) > 0 Then
                                oOrderGE.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(Session("EID")))
                            End If
                            Dim sError As String = String.Empty
                            If oOrderGE.Save(False, sError) Then
                                'If sZeroQuantityOrderLineIDs <> "" Then
                                '    Dim ZeroQuanity() As String = sZeroQuantityOrderLineIDs.Split(",")
                                '    For i As Integer = 0 To ZeroQuanity.Length - 1
                                '        oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(ZeroQuanity(i)) - 1).Delete()
                                '    Next
                                'End If
                                'oOrderGE.SetAddValue("_xDeletedOrderlines", 1)
                                'If oOrderGE.Save(False, sError) Then
                                '    lblError.Text = oOrderGE.LastUserError
                                'End If

                            Else
                                lblError.Text = sError
                            End If
                            sError = String.Empty
                            If oNewOrderGE.SubTypes("OrderLines").Count > 0 Then
                                oNewOrderGE.OrderType = OrderType.Quotation
                                oNewOrderGE.SetValue("PayTypeID", 1)
                                'redmine 16921
                                If Session("EID") IsNot Nothing AndAlso Convert.ToInt32(Session("EID")) > 0 Then
                                    oNewOrderGE.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(Session("EID")))
                                End If
                                If oNewOrderGE.Save(False, sError) Then
                                    lblError.Text = oNewOrderGE.LastUserError
                                    'Redmine #16086
                                    If iMemApp > 0 Then

                                        Dim param(1) As IDataParameter
                                        param(0) = DataAction.GetDataParameter("@iMemApp", SqlDbType.Int, iMemApp)
                                        param(1) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, CInt(oNewOrderGE.RecordID))
                                        sSQL = ""
                                        sSQL = Database & "..spUpdateMembershipApplicationOrerID__c"
                                        Dim recordupdate2 As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param, 180)
                                        ''commented By Pradip 2017-04-07 to update order on membership for performance reason
                                        'Dim oMemAppGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                                        'oMemAppGE = AptifyApplication.GetEntityObject("MembershipApplication__c", iMemApp)
                                        'oMemAppGE.SetValue("OrderID2", oNewOrderGE.RecordID)
                                        'oMemAppGE.Save()
                                    End If
                                End If
                            End If
                        Else
                            For i As Integer = 0 To oOrderGE.SubTypes("OrderLines").Count - 1
                                If oOrderGE.SubTypes("OrderLines").Item(i).GetValue("BillToCompanyID__c") > 0 Then
                                    If sOrderlineID.IndexOf(oOrderGE.SubTypes("OrderLines").Item(i).RecordID.ToString) >= 0 Then
                                        oOrderGE.SubTypes("OrderLines").Item(i).SetValue("BillToCompanyID__c", -1)
                                    End If
                                End If
                            Next
                            oOrderGE.SetValue("BillToSameAsShipTo", 0)
                            oOrderGE.SetValue("BillToCompanyID", -1)
                            oOrderGE.SetValue("PayTypeID", 1)
                            oOrderGE.SetValue("FirmPay__c", 0)
                            'redmine 16921
                            If Session("EID") IsNot Nothing AndAlso Convert.ToInt32(Session("EID")) > 0 Then
                                oOrderGE.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(Session("EID")))
                            End If
                            Dim sError As String = String.Empty
                            If oOrderGE.Save(False, sError) Then
                                lblError.Text = oOrderGE.LastUserError
                            End If
                        End If
                    Next
                    ' check these Quotation has paid all the orderlines
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Commented and added By Pradip 2017-04-30 To avoid  button double click
        'Private Sub cmdPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPay.Click
        Protected Sub cmdPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            ' Save a payment to the database with the information the user provided
            'additional check added By Pradip 2017-04-30 To excute this only on cmdPay
            If Page.Request.Params.Get("__EVENTTARGET").Contains("cmdPay") Then
                checkBillingCompany()
                Dim i As Integer
                Dim orderdetails As New Dictionary(Of Integer, Decimal)
                Dim arPay() As PayInfo
                Dim arShippingPay() As PayShippingInfo
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                Dim lblPay As New Label
                Dim hlOrderID As System.Web.UI.WebControls.HyperLink
                Dim OrderLineID As Label, OrderLineNumber As Label, lblCharteredOrderLineID As Label
                Dim txtPay As New TextBox
                Dim dtorder As DataTable = ViewState("orderdt")
                Dim lblEnableFunds As Label
                'Dim sTrans As String = Nothing

                Try
                    'Anil B change for 10254 on 29/03/2013
                    ReDim arPay(0)
                    lblMessage.Visible = False
                    ' sTrans = DataAction.BeginTransaction(IsolationLevel.ReadCommitted, True)
                    'Load order line payments
                    For i = 0 To grdMain.Items.Count - 1
                        lblPay = grdMain.Items(i).FindControl("lblBalanceAmount")
                        txtPay = grdMain.Items(i).FindControl("txtPayAmt")
                        lblEnableFunds = grdMain.Items(i).FindControl("lblEnableFunds")
                        lblPay.Text = lblPay.Text.Replace(sSymCurr, "")
                        txtPay.Text = txtPay.Text.Replace(sSymCurr, "")
                        If txtPay.Text.Trim.Length > 0 Then
                            If IsNumeric(txtPay.Text) And Convert.ToDecimal(txtPay.Text) > 0 Then
                                ReDim Preserve arPay(UBound(arPay) + 1)
                                arPay(UBound(arPay) - 1) = New PayInfo
                                With arPay(UBound(arPay) - 1)
                                    Dim lblNumDigits As Label = grdMain.Items(i).FindControl("lblNumDigitsAfterDecimal")
                                    Dim lblBal As Label = grdMain.Items(i).FindControl("lblBalanceAmount")
                                    hlOrderID = CType(grdMain.Items(i)("ID").Controls(0), System.Web.UI.WebControls.HyperLink)
                                    OrderLineID = grdMain.Items(i).FindControl("lblOrderLineID")
                                    OrderLineNumber = grdMain.Items(i).FindControl("lblOrderLineNumber")
                                    'Siddharth: Added below for Chartered Support product
                                    lblCharteredOrderLineID = grdMain.Items(i).FindControl("lblCharteredOrderLineID")
                                    .OrderID = hlOrderID.Text ' Convert.ToInt64(grdMain.Items(i)("ID").Text)
                                    .OrderLineID = OrderLineID.Text
                                    .PayAmount = Math.Round(CDec(txtPay.Text), _
                                                              CInt(lblNumDigits.Text))
                                    .LineNumber = OrderLineNumber.Text
                                    .Balance = Math.Round(CDec(txtPay.Text), _
                                                              CInt(lblNumDigits.Text))
                                    .IsDonation = CInt(lblEnableFunds.Text)
                                    ' Convert.ToDecimal(lblBal.Text.ToString().Replace(sSymCurr, ""))
                                    'Siddharth: Added below for Chartered Support product
                                    .CharteredOrderLineID = lblCharteredOrderLineID.Text
                                End With
                            Else
                                lblError.Text = "Values entered must be valid currency quantities."
                                lblError.Visible = True
                                Exit Sub
                            End If
                        End If
                    Next

                    'Load shipping charges for payment
                    ReDim arShippingPay(0)
                    If radgShippingCharges.Visible And radgShippingCharges.Items.Count > 0 Then
                        For i = 0 To radgShippingCharges.Items.Count - 1
                            txtPay = radgShippingCharges.Items(i).FindControl("txtPayAmt")
                            If txtPay.Text.Trim.Length > 0 Then
                                If IsNumeric(txtPay.Text) Then
                                    If CDec(txtPay.Text) > 0 Then
                                        ReDim Preserve arShippingPay(UBound(arShippingPay) + 1)
                                        arShippingPay(UBound(arShippingPay) - 1) = New PayShippingInfo
                                        With arShippingPay(UBound(arShippingPay) - 1)
                                            Dim lblBal As Label = radgShippingCharges.Items(i).FindControl("lblBalanceAmount")
                                            Dim lblOrderShipmentID As Label = radgShippingCharges.Items(i).FindControl("lblOrderShipmentID")
                                            hlOrderID = CType(radgShippingCharges.Items(i)("ID").Controls(0), System.Web.UI.WebControls.HyperLink)
                                            .OrderID = hlOrderID.Text
                                            .PayAmount = CDec(txtPay.Text)
                                            .Balance = CDec(txtPay.Text)
                                            .OrderShipmentID = CLng(lblOrderShipmentID.Text.Trim)
                                        End With
                                    End If
                                Else
                                    lblError.Text = "Values entered must be valid currency quantities."
                                    lblError.Visible = True
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If
                    ' Now, go through and make sure that each item is validated correctly
                    lblError.Visible = False
                    If UBound(arPay) > 0 Then
                        For i = 0 To UBound(arPay) - 1
                            With arPay(i)
                                If .PayAmount <= 0 Then
                                    lblError.Text = "All payments must be greater than zero"
                                    lblError.Visible = True
                                    Exit Sub
                                End If
                                If .PayAmount > .Balance Then
                                    lblError.Text = "All payments must be less than the balance due"
                                    lblError.Visible = True
                                    Exit Sub
                                End If
                            End With
                        Next
                        'Anil B change for 10737 on 13/03/2013
                        'Set Credit Card ID to load property form Navigation Config
                        If Len(CreditCard.CCNumber) = 0 Or _
                           Len(CreditCard.CCExpireDate) = 0 Then
                            lblError.Text = "Credit Card Information Required"
                            lblError.Visible = True
                        End If
                        'Added error string parameter for Redmine #18746
                        Dim sError As String = String.Empty
                        If PostPayment(arPay, "", arShippingPay, sError) Then
                            'Set Credit Card ID to load property form Navigation Config
                            CreditCard.CCNumber = ""
                            CreditCard.CCExpireDate = ""
                            CreditCard.CCSecurityNumber = ""
                            LoadOrders()
                            Session("IsError") = Nothing '  Added by Govind for Load Testing
                            'HP Issue#9092: need to bypass page form re-posting when refresh button is pressed causing a payment each time
                            Response.Redirect(Request.Path & "?msg=1", False)
                        Else
                            Session("IsError") = 1 '  Added by Govind for Load Testing
                            'Replaced error message for lable
                            'lblError.Text = "An error took place while processing your payment"
                            lblError.Text = sError & "- Credit Card Verification Failed - There was a problem with your card, please consult your bank"
                            lblError.Visible = True
                            ''Added By Pradip 2017-04-30 to make btn cmdPay Enabled
                            cmdPay.Attributes.Add("disabled", False)
                            cmdPay.Attributes.Remove("disabled")
                            cmdPay.Attributes.Add("class", "submitBtn")
                        End If
                    Else
                        Session("IsError") = 1 '  Added by Govind for Load Testing
                        lblError.Visible = True
                        lblError.Text = "All payments must be greater than zero"
                        ''Added By Pradip 2017-04-30 to make btn cmdPay Enabled
                        cmdPay.Attributes.Add("disabled", False)
                        cmdPay.Attributes.Remove("disabled")
                        cmdPay.Attributes.Add("class", "submitBtn")
                    End If
                Catch ex As Exception
                    Session("IsError") = 1 '  Added by Govind for Load Testing
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    lblError.Text = "An error took place while processing your payment"
                    lblError.Visible = True
                    ''Added By Pradip 2017-04-30 to make btn cmdPay Enabled
                    cmdPay.Attributes.Add("disabled", False)
                    cmdPay.Attributes.Remove("disabled")
                    cmdPay.Attributes.Add("class", "submitBtn")
                End Try
            End If
        End Sub

        Private Function PostPayment(ByVal arPay() As PayInfo, ByVal sTrans As String, ByVal arShippingPay() As PayShippingInfo, ByRef sError As String) As Boolean
            'Added error string parameter for Redmine #18746
            ' post the payment to the database using the CGI GE
            Dim oPayment As AptifyGenericEntityBase
            Dim i As Integer
            Dim bSaveOrder As Boolean = False
            'Commented below to return error
            'Dim sError As String = Nothing
            Dim bPaymentSaved As Boolean = True
            Dim sPayOrderID As String = String.Empty
            Try
                'update the donation amount on orderline if change and remove billtocompnay ID if the line is firm pay
                Dim m_oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                For i = 0 To UBound(arPay) - 1
                    m_oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", arPay(i).OrderID), Aptify.Applications.OrderEntry.OrdersEntity)
                    If i = 0 Then
                        sPayOrderID = CStr(arPay(i).OrderLineID)
                    Else
                        sPayOrderID = sPayOrderID + "," + CStr(arPay(i).OrderLineID)
                    End If
                    If m_oOrder IsNot Nothing AndAlso m_oOrder.OrderStatusID <> 2 Then
                        With m_oOrder.SubTypes("OrderLines").Find("ID", CLng(arPay(i).OrderLineID))
                            If (arPay(i).IsDonation = 1) And (arPay(i).PayAmount <> CDec(.GetValue("Extended"))) Then
                                .SetValue("UserPricingOverride", True)
                                .SetValue("Price", CLng(arPay(i).PayAmount))
                                bSaveOrder = True
                            End If
                            If Not String.IsNullOrEmpty(.GetValue("BillToCompanyID__c")) AndAlso .GetValue("BillToCompanyID__c") > 0 Then
                                .SetValue("BillToCompanyID__c", -1)
                                bSaveOrder = True
                            End If
                        End With
                        sError = String.Empty
                        If bSaveOrder Then
                            If Not m_oOrder.Save(False, sError) Then
                                bPaymentSaved = False
                                'DataAction.RollbackTransaction(sTrans)
                            End If
                        End If
                    End If
                Next
                Session("PaidOrderIDs") = sPayOrderID

                'Siddharth: Added below function to update chartered support price to 0
                UpdateOrdersForCharteredProdFree(arPay)

                'Create payment and payment lines for orderlines
                If bPaymentSaved Then
                    oPayment = AptifyApplication.GetEntityObject("Payments", -1)
                    oPayment.SetValue("EmployeeID", EBusinessGlobal.WebEmployeeID(Page.Application))
                    oPayment.SetValue("PersonID", User1.PersonID)
                    oPayment.SetValue("CompanyID", User1.CompanyID)
                    oPayment.SetValue("PaymentDate", Date.Today)
                    oPayment.SetValue("DepositDate", Date.Today)
                    oPayment.SetValue("EffectiveDate", Date.Today)
                    'Anil B change for 10737 on 13/03/2013
                    'Set Credit Card ID to load property form Navigation Config
                    oPayment.SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                    oPayment.SetValue("CCAccountNumber", CreditCard.CCNumber)
                    oPayment.SetValue("CCExpireDate", CreditCard.CCExpireDate)
                    'Anil B change for 10254 on 29/03/2013
                    'Set reference transaction for payment
                    If CreditCard.CCNumber = "-Ref Transaction-" Then
                        oPayment.SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                        oPayment.SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                    End If

                    ''RashmiP, issue 9976, (Unable to Make A Payment When CVV Is Checked) 09/30/10
                    oPayment.SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber)
                    'Anil B Issue 10254 on 20/04/2013
                    'Set Sequirity number to payment object
                    'oPayment.SetValue("CCSecurityNumber", CreditCard.CCSecurityNumber)
                    oPayment.SetValue("PaymentLevelID", User1.GetValue("GLPaymentLevelID"))
                    oPayment.SetValue("Comments", "Created through the CGI e-Business Suite")
                    ' to have an automatic conversion of quotes to regular
                    ' orders, set the Convert flag to true.
                    oPayment.SetAddValue("_xConvertQuotesToRegularOrder", "1")
                    'Anil B Issue 10254 on 20/04/2013
                    'creates SPM
                    If oPayment.Fields("PaymentInformationID").EmbeddedObjectExists Then
                        Dim oOrderPayInfo As PaymentInformation = DirectCast(oPayment.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                        oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                        oOrderPayInfo.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                        oOrderPayInfo.SetValue("CCPartial", CreditCard.CCPartial)
                    End If
                    oPayment.SetValue("CurrencyTypeID", ShoppingCart1.CurrencyTypeID) ' line added by Govind M for Redmine  #18682
                    For i = 0 To UBound(arPay) - 1
                        With oPayment.SubTypes("PaymentLines").Add
                            'Updated by Vaishali to set payment on orderline
                            .SetValue("AppliesTo", "Order Line")
                            .SetValue("Amount", arPay(i).PayAmount)
                            .SetValue("OrderID", arPay(i).OrderID)
                            .SetValue("OrderDetailID", arPay(i).OrderLineID)
                            .SetValue("LineNumber", arPay(i).LineNumber)
                            .SetValue("BillToPersonID", User1.PersonID)
                            '------------------------------------------------
                            ' code added by Govind For Redmine #18682
                            If .GetValue("ConversionCurrencySpotRateID") > 0 Then
                                Dim sSql As String = Database & "..spGetCurrencySpotRateAsPerID__c @ID=" & .GetValue("ConversionCurrencySpotRateID")
                                Dim iSpotRate As Decimal = DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                                If iSpotRate > 0 Then
                                    Dim dPayAmt As Decimal = Math.Round((Convert.ToDecimal(arPay(i).PayAmount) / iSpotRate), 2)
                                    .SetValue("PaymentAmount", dPayAmt)
                                End If
                            End If
                            'End Redmine #18682
                        End With
                    Next
                    'create paymnet lines For Shipping 
                    If radgShippingCharges.Visible And radgShippingCharges.Items.Count > 0 Then
                        For i = 0 To UBound(arShippingPay) - 1
                            With oPayment.SubTypes("PaymentLines").Add
                                .SetValue("AppliesTo", "Shipping")
                                .SetValue("Amount", arShippingPay(i).PayAmount)
                                .SetValue("OrderID", arShippingPay(i).OrderID)
                                .SetValue("BillToPersonID", User1.PersonID)
                            End With
                        Next
                    End If
                    'If there is any error while saving payment then it will return through this
                    sError = String.Empty
                    If oPayment.Save(False, sError) Then
                        'DataAction.CommitTransaction(sTrans)
                        bPaymentSaved = True
                        ''Added By Pradip 2016-07-06 for Payment Receipt Report Print
                        Session("PaymentID") = Convert.ToString(oPayment.RecordID)
                    Else
                        ' DataAction.RollbackTransaction(sTrans)
                        bPaymentSaved = False
                    End If
                End If
                Return bPaymentSaved
            Catch ex As Exception
                'DataAction.RollbackTransaction(sTrans)
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
        'Suraj issue 14450 2/12/13 date time filtering

        'Protected Sub grdMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemDataBound
        '    Dim dateColumns As New List(Of String)
        '    Dim index As Long = -1
        '    'Add datecolumn uniqueName in list for Date format
        '    dateColumns.Add("GridDateTimeColumnOrderDate")
        '    CommonMethods.FormatedDateOnGrid(dateColumns, e.Item)
        '    Dim iSumTotalPayAmmount As Decimal
        '    Dim TotalPayAmmount As Decimal
        '    Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
        '    For Each item As GridDataItem In grdMain.MasterTableView.Items
        '        'lblOrderID = CType(item.FindControl("ID"), Label)
        '        'Dim lblOrderNo As HyperLink = DirectCast(item.FindControl("lblOrderNo"), HyperLink)
        '        'Dim lblOrderLineID As Label = DirectCast(item.FindControl("OrderLineID"), Label)
        '        'Dim txtPayAmt As TextBox = DirectCast(item.FindControl("txtPayAmt"), TextBox)
        '        'Suraj Issue 14450 3/22/13 ,we provide a tool tip for DatePopupButton as well as the GridDateTimeColumnStartDate textbox   
        '        If TypeOf e.Item Is GridFilteringItem Then
        '            Dim filterItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
        '            Dim gridDateTimeColumnStartDate As RadDatePicker = DirectCast(filterItem("GridDateTimeColumnOrderDate").Controls(0), RadDatePicker)
        '            gridDateTimeColumnStartDate.ToolTip = "Enter a filter date"
        '            gridDateTimeColumnStartDate.DatePopupButton.ToolTip = "Select a filter date"
        '        End If
        '        TotalPayAmmount = getTotal(item, sSymCurr)
        '        iSumTotalPayAmmount += TotalPayAmmount
        '        'Dim orderdetails As New Dictionary(Of Integer, Decimal)
        '        'Dim OrderLineID As Long = CLng(lblOrderLineID.Text.Trim)
        '        'Dim lPay As Decimal = Convert.ToDecimal(txtPayAmt.Text)
        '        'If ViewState("CHECKED_PAYMENTS") IsNot Nothing Then
        '        '    orderdetails = DirectCast(ViewState("CHECKED_PAYMENTS"), Dictionary(Of Integer, Decimal))
        '        '    If orderdetails.ContainsKey(OrderLineID) = False Then
        '        '        orderdetails.Add(OrderLineID, lPay)
        '        '        ViewState("CHECKED_PAYMENTS") = orderdetails
        '        '    End If
        '        'Else
        '        '    orderdetails.Add(OrderLineID, lPay)
        '        '    ViewState("CHECKED_PAYMENTS") = orderdetails
        '        'End If
        '    Next
        '    setTotal(iSumTotalPayAmmount, sSymCurr)
        '    CalculateTotal(iSumTotalPayAmmount)

        'End Sub

        Protected Sub grdMain_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
            LoadOrders()
        End Sub

        Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdMain.PageIndexChanged
            ''grdMain.PageIndex = e.NewPageIndex
            LoadOrders()
        End Sub
        'Suraj Issue 14450 3/22/13 ,if the grid load first time By default the sorting will be Ascending for column Forum 
        Private Sub AddExpression()
            Dim expression1 As New GridSortExpression
            expression1.FieldName = "ID"
            expression1.SetSortOrder("Ascending")
            grdMain.MasterTableView.SortExpressions.AddSortExpression(expression1)
        End Sub

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Dim orderdetails As New Dictionary(Of Integer, Decimal)
                For Each item As GridDataItem In grdMain.MasterTableView.Items
                    Dim lblOrderLineID As Label = DirectCast(item.FindControl("lblOrderLineID"), Label)
                    Dim txtPayAmt As TextBox = DirectCast(item.FindControl("txtPayAmt"), TextBox)
                    Dim OrderLineID As Long = CLng(lblOrderLineID.Text.Trim)
                    Dim lPay As Decimal = Convert.ToDecimal(txtPayAmt.Text)
                    orderdetails.Add(OrderLineID, lPay)
                Next
                Session("CHECKED_PAYMENTS") = orderdetails
                Session("PaidOrderIDs") = Nothing
                Response.Redirect(MakePaymentPage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub CalculateTotal(ByVal currentPageTotal As Decimal)
            Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
            Dim index As Long = -1
            Dim orderdetails As New Dictionary(Of Integer, Decimal)
            Dim total As Decimal = 0.0
            Dim lblOrderLineID As Label = New Label
            If ViewState("CHECKED_PAYMENTS") IsNot Nothing Then
                orderdetails = DirectCast(ViewState("CHECKED_PAYMENTS"), Dictionary(Of Integer, Decimal))
            End If
            If orderdetails IsNot Nothing Then
                ' Dim iTotal As Int64 = orderdetails.Count
                'For i = iTotal - 1 To 0 Step -1
                '    For Each item As GridDataItem In grdMain.MasterTableView.Items
                '        ' lblOrderID = CType(item.FindControl("ID"), Label)
                '        lblOrderLineID = CType(item.FindControl("OrderLineID"), Label)
                '        If Not lblOrderLineID Is Nothing Then
                '            index = lblOrderLineID.Text
                '            Dim result As Boolean = DirectCast(item.FindControl("chkMakePayment"), CheckBox).Checked
                '            If index = orderdetails.Keys(i) Then
                '                orderdetails.Remove(index)
                '            End If
                '        End If
                '    Next
                'Next
                For index = 0 To orderdetails.Count - 1
                    total += orderdetails.Values(index)
                Next
                total = total + currentPageTotal
                setTotal(total, sSymCurr)
            End If
        End Sub

        Private Sub setTotal(ByVal iSumTotalPayAmmount As Decimal, ByVal sSymCurr As String, Optional ByVal bIsShipping As Boolean = 0)
            Try
                If Not iSumTotalPayAmmount = 0 Then
                    'suraj S Issue 16036 5/2/13, add comma for amount
                    If bIsShipping Then
                        txtShippingTotal.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPayAmmount)
                    Else
                        txtTotal.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPayAmmount)
                    End If
                Else
                    If bIsShipping Then
                        txtShippingTotal.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPayAmmount)
                    Else
                        txtTotal.Text = sSymCurr & "0.00"
                    End If
                End If
                Dim amount As String = txtTotal.Text.Replace(sSymCurr.Trim(), "")
                Dim Shipmentamount As String = String.Empty
                If radgShippingCharges.Visible Then
                    Shipmentamount = txtShippingTotal.Text.Replace(sSymCurr.Trim(), "")
                End If
                If Not String.IsNullOrEmpty(Shipmentamount) Then
                    lblTotalPayment.Text = sSymCurr & String.Format("{0:n2}", Convert.ToDecimal(amount) + Convert.ToDecimal(Shipmentamount))
                    txtTotal.Visible = True
                    lblTotal.Visible = True
                Else
                    lblTotalPayment.Text = sSymCurr & String.Format("{0:n2}", Convert.ToDecimal(amount))
                    txtTotal.Visible = False
                    lblTotal.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Function getTotal(ByRef row As GridDataItem, ByVal sSymCurr As String) As Decimal
            Dim iSumTotalPayAmmount As Decimal = 0
            Try
                Dim iTotalAmmount As Decimal
                Dim txtPayAmt As TextBox = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                If Not String.IsNullOrEmpty(txtPayAmt.Text) Then
                    Dim amount As String = txtPayAmt.Text.Replace(sSymCurr.Trim(), "")
                    iTotalAmmount = Convert.ToDecimal(amount) 'Decimal.Parse().ToString("0.00")
                    iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
                End If
            Catch ex As Exception
            End Try
            Return iSumTotalPayAmmount
        End Function

        Private Sub LoadShippingGrid()
            Dim sSQL As String
            Dim index As Integer = 0
            Dim dtShippingOrders As Data.DataTable = Nothing
            Dim sOrderID As String = String.Empty
            Try
                If Not Session("ShippingOrders") Is Nothing Then
                    dtShippingOrders = CType(Session("ShippingOrders"), DataTable)
                Else
                    Dim OrderIDs As EnumerableRowCollection(Of Integer) = CType(ViewState("orderdt"), DataTable).AsEnumerable().Select(Function(x) x.Field(Of Integer)("ID"))
                    For index = 0 To OrderIDs.Count - 1
                        If (sOrderID <> "") Then
                            sOrderID = sOrderID + "," + OrderIDs(index).ToString
                        Else
                            sOrderID = OrderIDs(index).ToString
                        End If
                    Next
                    Dim params(0) As System.Data.IDataParameter
                    params(0) = DataAction.GetDataParameter("@OrderID", SqlDbType.VarChar, sOrderID)
                    sSQL = Database & "..spGetOrderShippingDetails__c"
                    dtShippingOrders = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                End If

                If Not dtShippingOrders Is Nothing AndAlso dtShippingOrders.Rows.Count > 0 Then
                    radgShippingCharges.DataSource = dtShippingOrders
                    radgShippingCharges.Visible = (dtShippingOrders.Rows.Count > 0)
                    lblShipping.Visible = (dtShippingOrders.Rows.Count > 0)
                    txtShippingTotal.Visible = (dtShippingOrders.Rows.Count > 0)
                    lblShippingTotal.Visible = (dtShippingOrders.Rows.Count > 0)
                    dvShipping.Visible = (dtShippingOrders.Rows.Count > 0)
                    Session("ShippingOrders") = dtShippingOrders
                    'PayAmount
                    'Performance- Getting total amount in a variable and then assigning 
                    Dim dShippingTotal As Decimal = dtShippingOrders.Compute("SUM(PayAmount)", String.Empty)
                    Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                    txtShippingTotal.Text = sSymCurr & String.Format("{0:n2}", dShippingTotal)
                    txtShippingTotal.Visible = True
                    hidShippingTotal.Value = dShippingTotal
                Else
                    dvShipping.Visible = False
                    hidShippingTotal.Value = "0"
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Protected Sub radgShippingCharges_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles radgShippingCharges.ItemDataBound
        '    Try
        '        Dim dateColumns As New List(Of String)
        '        Dim index As Long = -1
        '        'Add datecolumn uniqueName in list for Date format
        '        dateColumns.Add("GridDateTimeColumnOrderDate")
        '        CommonMethods.FormatedDateOnGrid(dateColumns, e.Item)
        '        Dim iSumTotalPayAmmount As Decimal
        '        Dim TotalPayAmmount As Decimal
        '        Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
        '        Dim txtPayAmt As TextBox
        '        For Each item As GridDataItem In radgShippingCharges.MasterTableView.Items
        '            If TypeOf e.Item Is GridFilteringItem Then
        '                Dim filterItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
        '                Dim gridDateTimeColumnStartDate As RadDatePicker = DirectCast(filterItem("GridDateTimeColumnOrderDate").Controls(0), RadDatePicker)
        '                gridDateTimeColumnStartDate.ToolTip = "Enter a filter date"
        '                gridDateTimeColumnStartDate.DatePopupButton.ToolTip = "Select a filter date"
        '            End If
        '            ' TotalPayAmmount = getTotal(item, sSymCurr)
        '            iSumTotalPayAmmount += TotalPayAmmount
        '            txtPayAmt = DirectCast(item.FindControl("txtPayAmt"), TextBox)
        '            If Not txtPayAmt Is Nothing AndAlso Not String.IsNullOrEmpty(txtPayAmt.Text) Then
        '                Dim amount As String = txtPayAmt.Text.Replace(sSymCurr.Trim(), "")
        '                TotalPayAmmount = Convert.ToDecimal(amount) 'Decimal.Parse().ToString("0.00")
        '                iSumTotalPayAmmount = iSumTotalPayAmmount + TotalPayAmmount
        '            End If
        '        Next
        '        setTotal(iSumTotalPayAmmount, sSymCurr, 1)
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        ''Added By Pradip  2016-07-06 For Red-Mine Issue https://redmine.softwaredesign.ie/issues/13287
        Protected Sub btnReceipt_Click(sender As Object, e As EventArgs) Handles btnReceipt.Click
            Try
                'below code added by Govind M
                Dim sSQl As String = Database & "..spGetISETDPaymentOrder__c @PaymentID=" & Convert.ToUInt32(Session("PaymentID"))
                Dim isETDPayment As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSQl))
                If isETDPayment Then
                    Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "ETD Payment Receipt Report"))
                    Dim rptParam As New AptifyCrystalReport__c
                    rptParam.ReportID = ReportID
                    rptParam.Param1 = Convert.ToUInt32(Session("PaymentID"))
                    rptParam.Param2 = "P"
                    Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                Else
                    Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Payment Receipt Report"))
                    Dim rptParam As New AptifyCrystalReport__c
                    rptParam.ReportID = ReportID
                    rptParam.Param1 = Convert.ToUInt32(Session("PaymentID"))
                    rptParam.Param2 = "P"
                    Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Function added to update order for making chartered support price 0
        Private Sub UpdateOrdersForCharteredProdFree(ByVal arPay() As PayInfo)
            Try
                Dim orderdetails As New Dictionary(Of Long, Long)
                'Orders which needs to update price for chartered support will be added in dictionary
                For i = 0 To UBound(arPay) - 1
                    If arPay(i).CharteredOrderLineID > 0 AndAlso Not orderdetails.ContainsKey(arPay(i).OrderID) Then
                        orderdetails.Add(arPay(i).OrderID, arPay(i).CharteredOrderLineID)
                    End If
                Next
                'Process dictionary to update price
                If orderdetails.Count > 0 Then
                    Dim m_oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                    Dim sError As String = String.Empty
                    For i = 0 To orderdetails.Count - 1
                        m_oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", orderdetails.Keys(i)), Aptify.Applications.OrderEntry.OrdersEntity)
                        With m_oOrder.SubTypes("OrderLines").Find("ID", orderdetails.Values(i))
                            .SetValue("UserPricingOverride", True)
                            .SetValue("Price", 0)
                        End With
                        sError = String.Empty
                        If Not m_oOrder.Save(False, sError) Then
                            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New Exception(sError))
                        End If
                    Next
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace
