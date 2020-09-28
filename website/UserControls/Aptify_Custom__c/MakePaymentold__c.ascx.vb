Option Explicit On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports System.Data
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Applications.OrderEntry

Partial Class MakePaymentControlOld__c
    Inherits BaseUserControlAdvanced


    Protected Const ATTRIBUTE_Login_PAGE As String = "LoginPage"
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "MyDues__c"
    Protected Const ATTRIBUTE_ORDER_CONFIRMATION_PAGE As String = "OrderConfirmationURL"
    Dim iOrderId As Integer = 0

#Region "MyDues Specific Properties"

#End Region

    Public Overridable Property LoginPage() As String
        Get
            If Not ViewState(ATTRIBUTE_Login_PAGE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_Login_PAGE))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_Login_PAGE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

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

    Protected Overrides Sub SetProperties()

        If String.IsNullOrEmpty(LoginPage) Then
            'since value is the 'default' check the XML file for possible custom setting
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_Login_PAGE)
        End If
        If String.IsNullOrEmpty(OrderConfirmationURL) Then
            OrderConfirmationURL = Me.GetLinkValueFromXML(ATTRIBUTE_ORDER_CONFIRMATION_PAGE)
            If String.IsNullOrEmpty(OrderConfirmationURL) Then
                'Navin Prasad Issue 11032
                Me.grdMain.Columns.RemoveAt(0)
                grdMain.Columns.Insert(0, New BoundField())
                With DirectCast(grdMain.Columns(0), BoundField)
                    .DataField = "ID"
                    .HeaderText = "Order #"
                    .ItemStyle.ForeColor = Drawing.Color.Blue
                    .ItemStyle.Font.Underline = True
                End With
                Me.grdMain.ToolTip = "OrderConfirmationURL property has not been set."
            Else
                Dim hlink As HyperLinkField = CType(grdMain.Columns(0), HyperLinkField)
                hlink.DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
                'DirectCast(grdMain.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
            End If
        Else
            Dim hlink As HyperLinkField = CType(grdMain.Columns(0), HyperLinkField)
            hlink.DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
            'DirectCast(grdMain.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'set control properties from XML file if needed
        SetProperties()

        Try
            If Not IsPostBack Then
                If User1.PersonID > 0 Then
                    LoadOrders()
                Else
                    Response.Redirect(LoginPage)
                End If

                'HP Issue#9092: for page refresh purposes, display payment message
                If lblNoRecords.Visible Then
                    If Request.QueryString("msg") IsNot Nothing Then
                        lblMessage.Text = "Payment was made!"
                        lblMessage.ForeColor = Drawing.Color.DarkGreen
                        lblMessage.Visible = True
                        paymentMade.Visible = True
                    End If
                Else
                    If Request.QueryString("msg") IsNot Nothing Then
                        lblMessage.Text = Request.QueryString("msg").ToString
                        lblMessage.ForeColor = Drawing.Color.DarkGreen
                        lblMessage.Visible = True
                        paymentMade.Visible = True
                    End If
                End If

                CreditCard1.LoadCreditCardInfo()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
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

    Private Sub LoadOrders()
        Dim dt As Data.DataTable
        Dim sSQL As String
        Dim Totalsql As String
        Dim data As Data.DataTable
        Dim iOrderId As Integer = 0
        Try
            sSQL = "select o.ID,od.Product,od.Price,od.Extended ,ct.CurrencySymbol ,ct.NumDigitsAfterDecimal,o.OrderDate ,CONVERT(VARCHAR,o.GrandTotal,1)As GrandTotal,CONVERT(VARCHAR,o.Balance,1) As Balance," & _
                           " od.ID as 'OrderLineID' from vwOrders as o inner join vwOrderDetails as od " & _
                               " on o.ID=od.OrderID inner join vwproducts p on p.id=od.ProductID INNER JOIN vwCurrencyTypes ct " & _
                                "ON o.CurrencyTypeID=ct.ID  where o.BillToID=10 and balance>0 "
            ' get a list of the orders w/ a balance for the current user
            'sSQL = "..spGetDuesOrdersbyPersonID__c @PersonId='" & User1.PersonID & "'"
            dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                grdMain.DataSource = dt
                grdMain.DataBind()
                For Each gvr As GridViewRow In grdMain.Rows
                    'Dim hlink As HyperLink = CType(gvr.Cells(0).Controls(0), HyperLink)
                    Dim orderID As String = gvr.Cells(0).Text
                    'If CInt(hlink.Text) = iOrderId Then
                    If CInt(orderID) = iOrderId Then
                        'Dim hlink As HyperLinkField = CType(grdMain.Columns(0), HyperLinkField)
                        'hlink.Text = ""
                        'hlink.Visible = False
                        gvr.Cells(0).Text = ""
                        gvr.Cells(4).Text = ""
                        gvr.Cells(6).Text = ""
                        ' grdMain.Rows(i).Cells(0).Text = ""

                    Else
                        'iOrderId = CInt(grdMain.Rows(i).Cells(0).Text.ToString())
                        'iOrderId = CInt(hlink.Text)
                        iOrderId = CInt(orderID)
                        'Dim gstr As String = gvr.Cells(5).Text
                        'Gtotal += Convert.ToInt32(gvr.Cells(6).Text)


                    End If
                Next
                Totalsql = "..spGetTotal @Id='" & User1.PersonID & "'"
                data = DataAction.GetDataTable(Totalsql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not data Is Nothing AndAlso data.Rows.Count > 0 Then
                    Dim Gtotal As String
                    Dim Btotal As String
                    Gtotal = data.Rows(0).Item(0).ToString
                    txtGrandtotal.Text = String.Format("{0:C}", Gtotal)

                    Btotal = data.Rows(0).Item(1).ToString
                    txtbalanceTotal.Text = String.Format("{0:C}", Btotal)

                End If


                grdMain.Visible = True
                cmdPay.Visible = True
                CreditCard1.Visible = True
                lblNoRecords.Visible = False

            Else
                grdMain.Visible = False
                cmdPay.Visible = False
                CreditCard1.Visible = False
                lblNoRecords.Visible = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Class PayInfo
        Public OrderID As Long
        Public PayAmount As Decimal
        Public Balance As Decimal
    End Class

    Private Sub cmdPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPay.Click
        ' Save a payment to the database with the information the user provided
        Dim i As Integer
        Dim arPay() As PayInfo
        Dim iColPayAmt As Integer
        Dim iColOrderID As Integer
        Dim iColBalance As Integer
        Dim iColNumDigits As Integer
        Dim lblPay As New WebControls.Label
        Dim hlkOrderID As WebControls.HyperLink
        ' Dim txtPay As New HtmlControls.HtmlInputText
        Dim txtPay As New TextBox

        Try
            ReDim arPay(0)

            lblMessage.Visible = False

            If Page.IsValid Then
                For i = 0 To grdMain.Columns.Count - 1
                    If grdMain.Columns(i).HeaderText = "Pay Amount" Then
                        iColPayAmt = i
                    ElseIf grdMain.Columns(i).HeaderText = "Order #" Then
                        iColOrderID = i
                    ElseIf grdMain.Columns(i).HeaderText = "Actual Balance" Then
                        iColBalance = i
                    ElseIf grdMain.Columns(i).HeaderText = "Currency Digits" Then
                        iColNumDigits = i
                    End If
                Next
                'Navin Prasad Issue 11032
                For i = 0 To grdMain.Rows.Count - 1
                    ' loop through each item in the grid to see if it was checked,
                    ' and store the order id, and amount
                    'Navin Prasad Issue 11032
                    ''RashmiP, Issue 11147, remove currency symbol from string. Because IsNumeric is not validating Currency symbol except $.
                    lblPay = grdMain.Rows(i).Cells(iColBalance).Controls(1)
                    txtPay = grdMain.Rows(i).Cells(iColPayAmt).Controls(1)
                    If txtPay.Text.Trim.Length > 0 Then
                        If IsNumeric(txtPay.Text) Then
                            'HP Issue#8941: only add payments where the user has put a value greater than zero
                            '               allowing payment for selected items
                            'If CDec(txtPay.Text) > 0 Then
                            ReDim Preserve arPay(UBound(arPay) + 1)
                            arPay(UBound(arPay) - 1) = New PayInfo
                            With arPay(UBound(arPay) - 1)
                                'Navin Prasad Issue 11032
                                Dim lblNumDigits As Label = grdMain.Rows(i).FindControl("lblNumDigitsAfterDecimal")
                                Dim lblBal As Label = grdMain.Rows(i).FindControl("lblBalance")
                                hlkOrderID = grdMain.Rows(i).Cells(iColOrderID).Controls(0)
                                .OrderID = hlkOrderID.Text
                                .PayAmount = Math.Round(CDec(txtPay.Text), _
                                                          CInt(lblNumDigits.Text))
                                .Balance = lblBal.Text
                            End With
                            'End If
                        Else
                            lblError.Text = "Values entered must be valid currency quantities."
                            lblError.Visible = True
                            Exit Sub
                        End If
                    End If
                Next

                ' Now, go through and make sure that each item is validated correctly
                lblError.Visible = False
                If UBound(arPay) > 0 Then
                    'For i = 0 To UBound(arPay) - 1
                    '    With arPay(i)
                    '        'If .PayAmount <= 0 Then
                    '        '    lblError.Text = "All payments must be greater than zero"
                    '        '    lblError.Visible = True
                    '        '    Exit Sub
                    '        'End If
                    '        'If .PayAmount > .Balance Then
                    '        '    lblError.Text = "All payments must be less than the balance due"
                    '        '    lblError.Visible = True
                    '        '    Exit Sub
                    '        'End If
                    '    End With
                    'Next
                    If Len(CreditCard1.CCNumber) = 0 Or _
                       Len(CreditCard1.CCExpireDate) = 0 Then
                        lblError.Text = "Credit Card Information Required"
                        lblError.Visible = True
                    End If

                    If PostPayment(arPay) Then
                        'HP - Issue 8264, virbiage is invalid when no information is being listes
                        'lblMessage.Text = "Payment was made! Your updated order information is shown below."
                        'lblMessage.Text = "Payment successfull !"
                        'lblMessage.Visible = True
                        CreditCard1.CCNumber = ""
                        CreditCard1.CCExpireDate = ""
                        CreditCard1.CCSecurityNumber = ""
                        LoadOrders()
                        'HP Issue#9092: need to bypass page form re-posting when refresh button is pressed causing a payment each time
                        Response.Redirect(Request.Path & "?msg=Payment was made!  Your updated order information is shown below.", False)
                    Else
                        lblError.Text = "An error took place while processing your payment"
                        lblError.Visible = True
                    End If
                Else
                    lblError.Visible = True
                    'HP - Issue 8264, replace the default error message if payment amount is blank
                    'lblError.Text = "Please select a payment for at least one order"
                    lblError.Text = "All payments must be greater than zero"
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Function PostPayment(ByVal arPay() As PayInfo) As Boolean
        ' post the payment to the database using the CGI GE
        Dim oPayment As AptifyGenericEntityBase
        Dim i As Integer

        Try
            oPayment = AptifyApplication.GetEntityObject("Payments", -1)
            oPayment.SetValue("EmployeeID", EBusinessGlobal.WebEmployeeID(Page.Application))
            oPayment.SetValue("PersonID", User1.PersonID)
            oPayment.SetValue("CompanyID", User1.CompanyID)
            oPayment.SetValue("PaymentDate", Date.Today)
            oPayment.SetValue("DepositDate", Date.Today)
            oPayment.SetValue("EffectiveDate", Date.Today)
            oPayment.SetValue("PaymentTypeID", CreditCard1.PaymentTypeID)
            oPayment.SetValue("CCAccountNumber", CreditCard1.CCNumber)
            oPayment.SetValue("CCExpireDate", CreditCard1.CCExpireDate)
            ''RashmiP, issue 9976, (Unable to Make A Payment When CVV Is Checked) 09/30/10
            oPayment.SetAddValue("_xCCSecurityNumber", CreditCard1.CCSecurityNumber)
            oPayment.SetValue("PaymentLevelID", User1.GetValue("GLPaymentLevelID"))
            oPayment.SetValue("Comments", "Created through the CGI e-Business Suite")
            ' to have an automatic conversion of quotes to regular
            ' orders, set the Convert flag to true.
            oPayment.SetAddValue("_xConvertQuotesToRegularOrder", "1")
            Dim orderid As Long = -1
            Dim j As Integer = 0
            Dim oGE As OrdersEntity = Nothing
            Dim oOrderLine As OrderLinesEntity
            For i = 0 To UBound(arPay) - 1
                If orderid = arPay(i).OrderID Then
                    j = j + 1
                    oOrderLine = CType(oGE.SubTypes("OrderLines").Item(j), OrderLinesEntity)
                    With oOrderLine
                        .SetValue("UserPricingOverride", "1")
                        .SetValue("Price", arPay(i).PayAmount)
                    End With
                Else
                    If oGE IsNot Nothing Then
                        oGE.Save(False)
                    End If
                    orderid = arPay(i).OrderID
                    j = 0
                    oGE = AptifyApplication.GetEntityObject("Orders", orderid)
                    oOrderLine = CType(oGE.SubTypes("OrderLines").Item(j), OrderLinesEntity)
                    With oOrderLine
                        .SetValue("UserPricingOverride", "1")
                        .SetValue("Price", arPay(i).PayAmount)
                    End With
                End If
                If arPay(i).PayAmount > 0 Then
                    With oPayment.SubTypes("PaymentLines").Add
                        .SetValue("Amount", arPay(i).PayAmount)
                        .SetValue("OrderID", arPay(i).OrderID)
                    End With
                End If
            Next
            If oGE IsNot Nothing Then
                oGE.Save(False)
            End If
            Return oPayment.Save(False)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function

    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        grdMain.PageIndex = e.NewPageIndex
        LoadOrders()
    End Sub


    Protected Sub grdMain_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMain.DataBound
        'Dim iOrderId As Integer = 0

        'For i As Integer = 0 To grdMain.Rows.Count - 1
        '    'If CInt(grdMain.Rows..Item("ID").ToString) = iOrderId Then
        '    '    row.Item("ID") = ""
        '    'Else
        '    '    iOrderId = CInt(row.Item("ID").ToString)
        '    'End If
        '    iOrderId = CInt(grdMain.Rows(i).Cells(0).Text)
        '    For j As Integer = 0 To grdMain.Rows.Count - 1
        '        If CInt(grdMain.Rows(j).Cells(ID).Text) = iOrderId Then
        '            grdMain.Rows(j).Cells(ID).Text = 0

        '        Else
        '            iOrderId = grdMain.Rows(i).Cells(ID).Text
        '        End If
        '    Next

        'Next

    End Sub
    Dim total As Decimal = 0
    Dim Ptotal As Decimal = 0

    Protected Sub grdMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdMain.RowDataBound
       
        If e.Row.RowType = DataControlRowType.DataRow Then

            total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Price"))
            txtTotal.Text = total
            'String.Format("€", total)

            Dim txtpayment As TextBox = CType(e.Row.FindControl("txtPay"), TextBox)
            Dim strpay As String
            strpay = txtpayment.Text
            Dim spay As String = (strpay.Remove(0, 1))

            Ptotal += Convert.ToDecimal(spay.ToString())
            txtPaytotal.Text = Ptotal

           
        End If

    End Sub

    Protected Sub txtPay_TextChanged(ByVal sender As Object, ByVal e As EventArgs)




    End Sub
End Class
