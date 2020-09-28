
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
    Partial Class AdminNonDuesOrderDetail__c
        Inherits BaseUserControlAdvanced
        Protected Const ATTRIBUTE_ORDER_CONFIRMATION_PAGE As String = "OrderConfirmationURL"
        Protected Const ATTRIBUTE_ORDER_NONDUEPRINT_PAGE As String = "OrderNonDuesPrintURL"
        Protected Const ATTRIBUTE_Login_PAGE As String = "LoginPageURL"
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
        ''' 'Order Non Dues Print url
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Property OrderNonDuesPrintURL() As String
            Get
                If Not ViewState(ATTRIBUTE_ORDER_NONDUEPRINT_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ORDER_NONDUEPRINT_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ORDER_NONDUEPRINT_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' Login Pagee Url
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Property LoginPageURL() As String
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
#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(OrderConfirmationURL) Then
                'since value is the 'default' check the XML file for possible custom setting
                OrderConfirmationURL = Me.GetLinkValueFromXML(ATTRIBUTE_ORDER_CONFIRMATION_PAGE)
                If String.IsNullOrEmpty(OrderConfirmationURL) Then
                    'Me.grdOrderDetails.Enabled = False
                    Me.grdOrderDetails.ToolTip = "OrderConfirmationURL property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(OrderNonDuesPrintURL) Then
                'since value is the 'default' check the XML file for possible custom setting
                OrderNonDuesPrintURL = Me.GetLinkValueFromXML(ATTRIBUTE_ORDER_NONDUEPRINT_PAGE)
                If String.IsNullOrEmpty(OrderNonDuesPrintURL) Then
                    prnLink.Enabled = False
                Else
                    prnLink.Enabled = True
                    prnLink.NavigateUrl = OrderNonDuesPrintURL
                End If
            End If
            If String.IsNullOrEmpty(LoginPageURL) Then
                'since value is the 'default' check the XML file for possible custom setting
                LoginPageURL = Me.GetLinkValueFromXML(ATTRIBUTE_Login_PAGE)

            End If

        End Sub

        Private Class PayInfo
            Public OrderID As Long
            Public PayAmount As Decimal
            Public Balance As Decimal
        End Class
        Private Sub LoadOrders()
            Dim dt As Data.DataTable
            Dim dtProducts As DataTable
            Dim sSQL As String
            Dim sProducts As String = String.Empty
            'Dim txtPay As HtmlControls.HtmlInputText
            Dim i As Integer, j As Integer
            Dim iUserGroup As Integer = 0
            Dim param(0) As IDataParameter
            Try

                Dim ParentID As Long = -1
                ParentID = Me.DataAction.ExecuteScalar("Select ISNULL(ParentID, -1) as ParentID from " & Database & "..vwCompanies where ID=" & User1.CompanyID.ToString)
                sSQL = Database & "..spGetNonDuesOrderDetails__c"
                param(0) = Me.DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, User1.CompanyID)
                dt = Me.DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)


                Session("dt") = dt
                Dim dcolUrl As DataColumn = New DataColumn()
                dcolUrl.Caption = "OrderConfirmationURL"
                dcolUrl.ColumnName = "OrderConfirmationURL"

                dt.Columns.Add(dcolUrl)
                If dt.Rows.Count > 0 Then
                    For Each rw As DataRow In dt.Rows
                        rw("OrderConfirmationURL") = OrderConfirmationURL & "?ID=" + rw("ID").ToString & "&PersonID=" & rw("PersonID").ToString
                    Next
                End If

                For i = 0 To dt.Rows.Count - 1
                    sSQL = "SELECT P.Name FROM " & Database & "..vwOrderDetail OD INNER JOIN VwProducts P ON P.ID = OD.ProductID WHERE  ( p.DefaultDuesProduct=0 ) and OrderID = " & dt.Rows(i)("ID")
                    dtProducts = DataAction.GetDataTable(sSQL)
                    For j = 0 To dtProducts.Rows.Count - 1
                        sProducts = sProducts + dtProducts.Rows(j)(0).ToString() + ".</br>"
                    Next
                    If sProducts.Length > 0 Then sProducts = sProducts.Substring(0, sProducts.Length - 1)
                    dt.Rows(i)("Product") = sProducts
                    sProducts = String.Empty
                    j = 0
                Next

                Dim sortfield As String = ""
                If Not Session("Sortfield") Is Nothing Then
                    sortfield = Session("Sortfield").ToString()
                End If

                i = 0
                If dt.Rows.Count > 0 Then
                    sCuurSymbol = dt.Rows(0).Item("CurrencySymbol").ToString()
                    ViewState("sCuurSymbol") = sCuurSymbol
                    txtTotal.Text = sCuurSymbol & "0.00"
                    grdOrderDetails.DataSource = dt
                    ' grdOrderDetails.DataBind()
                    'AssignDefaultPayment()

                    Dim iSumTotalPayAmmount As Decimal
                    For Each row As GridDataItem In grdOrderDetails.Items
                        Dim chk1 As CheckBox = DirectCast(row.FindControl("chkMakePayment"), CheckBox)

                        If chk1.Checked = True Then
                            Dim txtPayAmt As TextBox = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                            Dim txtPayAmmount As String = txtPayAmt.Text
                            Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                            Dim sTotleAmmount As String = txtPayAmmount.ToString().Replace(sSymCurr, "")
                            Dim iTotalAmmount As Decimal
                            iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")
                            iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
                            txtTotal.Text = String.Format("{0:n2}", sSymCurr & iSumTotalPayAmmount)
                        End If
                    Next
                Else
                    lblTotal.Visible = False
                    txtTotal.Visible = False
                    CreditCard11.Visible = False
                End If

                grdOrderDetails.Visible = (dt.Rows.Count > 0)

                'txtTotal.Text = sCuurSymbol & "0.00"
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

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            SetProperties()
            If User1.UserID < 0 Then

                Response.Redirect(LoginPageURL)
            End If
            Try
                If Not IsPostBack Then
                    CreditCard11.LoadCreditCardInfo()

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Protected Sub grdOrderDetails_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdOrderDetails.NeedDataSource
            LoadOrders()
        End Sub

        Protected Sub cmdMakePayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMakePayment.Click
            ' Save a payment to the database with the information the user provided
            Dim i As Integer
            Dim arPay() As PayInfo
            Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
            Dim lblPay As New Label
            Dim OrderID As HyperLink
            Dim txtPay As New TextBox
            Try
                ReDim arPay(0)
                If Page.IsValid Then

                    For i = 0 To grdOrderDetails.Items.Count - 1
                        lblPay = grdOrderDetails.Items(i).FindControl("lblBalanceAmount")
                        txtPay = grdOrderDetails.Items(i).FindControl("txtPayAmt")
                        Dim chkMakePayment As CheckBox = CType(grdOrderDetails.Items(i).FindControl("chkMakePayment"), CheckBox)
                        If chkMakePayment.Checked = True Then
                            If txtPay.Text.Trim.Length > 0 Then
                                If IsNumeric(txtPay.Text) Then

                                    If CDec(txtPay.Text) > 0 Then
                                        ReDim Preserve arPay(UBound(arPay) + 1)
                                        arPay(UBound(arPay) - 1) = New PayInfo
                                        With arPay(UBound(arPay) - 1)

                                            Dim lblNumDigits As Label = grdOrderDetails.Items(i).FindControl("lblNumDigitsAfterDecimal")
                                            Dim lblBal As Label = grdOrderDetails.Items(i).FindControl("lblBalanceAmount")
                                            OrderID = grdOrderDetails.Items(i).FindControl("lblOrderNo")
                                            .OrderID = OrderID.Text
                                            .PayAmount = Math.Round(CDec(txtPay.Text), _
                                                                      CInt(lblNumDigits.Text))
                                            .Balance = lblBal.Text


                                        End With
                                    End If
                                Else
                                    lblError.Text = "Values entered must be valid currency quantities."
                                    lblError.Visible = True
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next

                    ' Now, go through and make sure that each item is validated correctly
                    lblError.Visible = False
                    If UBound(arPay) > 0 Then
                        For i = 0 To UBound(arPay) - 1
                            With arPay(i)
                                If .PayAmount <= 0 Then
                                    lblError.Text = "Select a person to make payment"
                                    lblError.Visible = True
                                    Exit Sub
                                End If
                                If .PayAmount > .Balance Then
                                    lblError.Text = "Payments must be less than the balance due"
                                    lblError.Visible = True
                                    Exit Sub
                                End If
                            End With
                        Next
                        'If Len(CreditCard11.CCNumber) = 0 Or _
                        '   Len(CreditCard11.CCExpireDate) = 0 Then
                        '    lblError.Text = "Credit Card Information Required"
                        '    lblError.Visible = True
                        'End If

                        If PostPayment(arPay) Then
                            CreditCard11.clear()
                            CreditCard11.CCNumber = ""
                            CreditCard11.CCExpireDate = ""
                            CreditCard11.CCSecurityNumber = ""
                            LoadOrders()
                            radpaymentmsg.VisibleOnPageLoad = True
                            grdOrderDetails.Rebind()
                            'Response.Redirect(Request.Path & "?msg=Payment was made!  Your updated order information is shown below.", False)
                        Else
                            If String.IsNullOrEmpty(lblPaymentError.Text) Then
                                lblPaymentError.Text = "An error took place while processing your payment"
                            End If

                            lblPaymentError.Visible = True
                        End If
                    Else
                        lblError.Visible = True
                        lblError.Text = "Select a person to make payment"
                    End If


                    txtTotal.Text = sSymCurr & "0.00"
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Make Payments
        ''' </summary>
        ''' <param name="arPay"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function PostPayment(ByVal arPay() As PayInfo) As Boolean
            ' post the payment to the database using the CGI GE
            Dim oPayment As AptifyGenericEntityBase
            Dim i As Integer
            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity = Nothing
            Dim strError As String = String.Empty
            Try

                'If ShoppingCart1.GetOrderObject(Session, Page.User, Application).Fields("PaymentInformationID").EmbeddedObjectExists Then
                '    Dim oOrderPayInfo As PaymentInformation = DirectCast(ShoppingCart1.GetOrderObject(Session, Page.User, Application).Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                '    oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCSecurityNumber

                '    'Ansar Shaikh - Issue 11986 - 12/27/2011
                '    oOrderPayInfo.SetValue("CCPartial", oOrderPayInfo.GetCCPartial(CreditCard.CCNumber))
                'End If

                oPayment = AptifyApplication.GetEntityObject("Payments", -1)
                'Dim param1(0) As IDataParameter
                ' param1(0) = DataAction.GetDataParameter("ID", SqlDbType.Int)
                'param1(0).Direction = ParameterDirection.Output
                Dim sql As String = "spGetPaymentLevelID__c"
                Dim PaymentLevelID As Integer
                'Dim k As Integer = Convert.ToInt16(DataAction.ExecuteScalarParametrized(sql, CommandType.StoredProcedure, param1))
                Dim k As Integer = Convert.ToInt16(DataAction.ExecuteScalar(sql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If k > 0 Then
                    PaymentLevelID = k
                End If
                'If (Not param1(0).Value Is Nothing) AndAlso (Not IsDBNull(param1(0).Value)) Then
                '    PaymentLevelID = Convert.ToInt32(param1(0).Value)
                'End If
                oPayment.SetValue("PaymentLevelID", PaymentLevelID)
                oPayment.SetValue("EmployeeID", EBusinessGlobal.WebEmployeeID(Page.Application))
                oPayment.SetValue("PersonID", User1.PersonID)
                oPayment.SetValue("PersonID", User1.PersonID)
                oPayment.SetValue("CompanyID", User1.CompanyID)
                oPayment.SetValue("PaymentDate", Date.Today)
                oPayment.SetValue("DepositDate", Date.Today)
                oPayment.SetValue("EffectiveDate", Date.Today)

                Dim sqlStr As String = "spGetPayType"
                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("ID", SqlDbType.Int, CreditCard11.PaymentTypeID)
                Dim PayTye As String = Convert.ToString(DataAction.ExecuteScalarParametrized(sqlStr, CommandType.StoredProcedure, param))
                If PayTye.ToLower = "wire transfer" Then
                    oPayment.SetValue("PaymentTypeID", CreditCard11.PaymentTypeID)
                    oPayment.SetValue("CheckNumber", CreditCard11.TransactionNumber)
                    oPayment.SetValue("Bank", CreditCard11.BankName)
                    oPayment.SetValue("AccountNumber", CreditCard11.AccountNumber)
                    '  oPayment.SetValue("RoutingNumber__c", CreditCard11.RoutingNumber)
                    oPayment.SetValue("AccountName", CreditCard11.NameOfAccount)
                    oPayment.SetValue("BranchName", CreditCard11.BranchName)
                    oPayment.SetValue("ABA", CreditCard11.ABA)

                    For i = 0 To UBound(arPay) - 1
                        With oPayment.SubTypes("PaymentLines").Add
                            .SetValue("Amount", arPay(i).PayAmount)
                            .SetValue("OrderID", arPay(i).OrderID)
                        End With

                        '11/29/2012
                        'If oPayment.RecordID > 0 Then
                        oOrder = AptifyApplication.GetEntityObject("Orders", CLng(arPay(i).OrderID))
                        oOrder.SetValue("IsNonDuesAmountPaid__c", 1)
                        oOrder.Save(False, strError)
                        'End If
                    Next

                    If oPayment.Save(False) Then
                        lblPaymentError.Text = String.Empty
                        Return oPayment.Save(False, strError)

                    Else
                        lblPaymentError.Text = strError
                        Throw New System.Exception(strError)
                    End If

                ElseIf PayTye.ToLower = "check" Then
                    oPayment.SetValue("PaymentTypeID", CreditCard11.PaymentTypeID)
                    'oPayment.SetValue("CheckNumber", CreditCard11.CheckNumber)
                    oPayment.SetValue("Bank", CreditCard11.BankName)
                    oPayment.SetValue("AccountNumber", CreditCard11.AccountNumber)
                    oPayment.SetValue("BranchName", CreditCard11.BranchName)
                    oPayment.SetValue("ABA", CreditCard11.ABA)
                    For i = 0 To UBound(arPay) - 1
                        With oPayment.SubTypes("PaymentLines").Add
                            .SetValue("Amount", arPay(i).PayAmount)
                            .SetValue("OrderID", arPay(i).OrderID)
                        End With

                        '11/29/2012
                        'If oPayment.RecordID > 0 Then
                        oOrder = AptifyApplication.GetEntityObject("Orders", CLng(arPay(i).OrderID))
                        oOrder.SetValue("IsNonDuesAmountPaid__c", 1)
                        oOrder.Save(False, strError)
                        'End If
                    Next
                    If Not oPayment.Save(False, strError) Then
                        lblPaymentError.Text = strError
                        Throw New System.Exception(strError)
                    Else

                        Return oPayment.Save(False)
                    End If
                ElseIf PayTye.ToLower = "credit card" Then
                    oPayment.SetValue("PaymentTypeID", CreditCard11.PaymentTypeID)
                    oPayment.SetValue("CCAccountNumber", CreditCard11.CCNumber)
                    oPayment.SetValue("CCSecurityNumber", CreditCard11.CCSecurityNumber)
                    oPayment.SetValue("CCExpireDate", CreditCard11.CCExpireDate)
                    For i = 0 To UBound(arPay) - 1
                        With oPayment.SubTypes("PaymentLines").Add
                            .SetValue("Amount", arPay(i).PayAmount)
                            .SetValue("OrderID", arPay(i).OrderID)
                        End With

                        '11/29/2012
                        'If oPayment.RecordID > 0 Then
                        oOrder = AptifyApplication.GetEntityObject("Orders", CLng(arPay(i).OrderID))
                        oOrder.SetValue("IsNonDuesAmountPaid__c", 1)
                        oOrder.Save(False, strError)
                        'End If
                    Next
                    If Not oPayment.Save(False, strError) Then
                        lblPaymentError.Text = strError
                        Throw New System.Exception(strError)
                    Else

                        Return oPayment.Save(False)
                    End If

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
        Protected Sub chkMakePayment_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim flag As Boolean = False
            Dim chk1 As CheckBox = CType(sender, CheckBox)

            Dim header As GridHeaderItem = TryCast(grdOrderDetails.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
            Dim chkAllMakePayment As CheckBox = DirectCast(header.FindControl("chkAllMakePayment"), CheckBox)

            Dim grow As GridDataItem = DirectCast(chk1.NamingContainer, GridDataItem)
            Dim i As Integer = grow.DataSetIndex
            'Dim txtPaymentaMT As TextBox = (DirectCast(grdOrderDetails.Items(i).FindControl("txtPayAmt"), TextBox))
            Dim sSymCurrancy As String = ViewState("sCuurSymbol").ToString().Trim()
            'If chk1.Checked = True Then
            '    Dim textBoxText As String = (DirectCast(grdOrderDetails.Items(i).FindControl("lblBalanceAmount"), Label)).Text
            '    txtPaymentaMT.Text = textBoxText.ToString().Replace(sSymCurrancy, "")

            'Else
            '    txtPaymentaMT.Text = "0.00"
            'End If
            Dim iSumTotalPayAmmount As Decimal
            Dim chkflag As Boolean = True

            For Each row As GridDataItem In grdOrderDetails.Items

                chk1 = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                If chk1.Checked = True Then
                    Dim txtPayAmt As TextBox = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                    Dim textBoxText As String = DirectCast(row.FindControl("lblBalanceAmount"), Label).Text
                    txtPayAmt.Text = textBoxText
                    Dim txtPayAmmount As String = txtPayAmt.Text
                    Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                    Dim sTotleAmmount As String = txtPayAmmount.ToString().Replace(sSymCurr, "")
                    Dim iTotalAmmount As Decimal
                    iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")

                    iSumTotalPayAmmount = Convert.ToDecimal(ViewState("Total")) + iSumTotalPayAmmount + iTotalAmmount


                    txtTotal.Text = String.Format("{0:n2}", sSymCurr & iSumTotalPayAmmount)

                Else
                    chkflag = False
                    If chkAllMakePayment.Checked = True Then
                        chkAllMakePayment.Checked = False
                    End If
                End If
            Next
            If chkflag Then
                chkAllMakePayment.Checked = True
            End If
            For Each row As GridDataItem In grdOrderDetails.Items
                chk1 = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                If chk1.Checked = True Then
                    flag = True

                End If
            Next
            If Not flag Then
                txtTotal.Text = sSymCurrancy & "0.00"
            End If

            'Dim [Date] As [String] = DirectCast(DirectCast(sender, CheckBox).Parent.FindControl("lblDate"), Label).Text
        End Sub
        'Protected Sub grdOrderDetails_GridItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs)
        '    'SaveCheckedValues()
        '    'PopulateCheckedValues()
        '    Dim chkMakePayment As CheckBox = DirectCast(e.Item.FindControl("chkMakePayment"), CheckBox)
        '    Dim lblPersonID As Label = DirectCast(e.Item.FindControl("lblPersonID"), Label)
        '    If chkMakePayment IsNot Nothing Then
        '        'Check in the ViewState
        '        If ViewState("CHECKED_ITEMS") IsNot Nothing Then
        '            Dim userdetails As ArrayList = DirectCast(ViewState("CHECKED_ITEMS"), ArrayList)
        '            Dim dataItem As DataRowView = DirectCast(e.Item.DataItem, System.Data.DataRowView)
        '            If dataItem IsNot Nothing Then
        '                Dim OrderID As Long = dataItem("ID")
        '                If userdetails.Contains(OrderID) = True Then
        '                    chkMakePayment.Checked = True

        '                Else
        '                    chkMakePayment.Checked = False
        '                End If
        '            End If
        '        End If
        '    End If
        'End Sub
        Dim iTotleAmmount As Decimal
        Dim iTotalBalance As Decimal
        Protected Sub grdOrderDetails_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdOrderDetails.ItemDataBound
            Dim sFormDec As String
            Try

                If e.Item.ItemType = DataControlRowType.DataRow Then
                    iTotleAmmount += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "GrandTotal"))

                End If
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                sFormDec = String.Format("{0:n2}", iTotleAmmount)
                Dim txtPayAmt As TextBox = DirectCast(e.Item.FindControl("txtPayAmt"), TextBox)
                If txtPayAmt IsNot Nothing Then
                    sFormDec = String.Format("{0:n2}", Convert.ToDecimal(txtPayAmt.Text))
                    txtPayAmt.Text = sFormDec
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub AssignDefaultPayment()
            Try
                For Each row As GridDataItem In grdOrderDetails.Items
                    Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                    Dim txtPayAmmount As TextBox = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                    txtPayAmmount.Text = String.Format("{0:n2}", sSymCurr & "00.00")
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub txtPayAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim flag As Boolean = False
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                Dim txtPaymentaMT As TextBox = CType(sender, TextBox)
                Dim grow As GridDataItem = DirectCast(txtPaymentaMT.NamingContainer, GridDataItem)
                Dim i As Integer = grow.DataSetIndex
                Dim chk1 As CheckBox = (DirectCast(grdOrderDetails.Items(i).FindControl("chkMakePayment"), CheckBox))
                If Not txtPaymentaMT.Text = "" Then
                    If Convert.ToDecimal(txtPaymentaMT.Text) = 0 Or Convert.ToDecimal(txtPaymentaMT.Text) < 0 Then
                        chk1.Checked = False
                    ElseIf Convert.ToDecimal(txtPaymentaMT.Text) > 0 Then
                        chk1.Checked = True
                    End If
                Else

                    chk1.Checked = False
                End If

                Dim iSumTotalPayAmmount As Decimal
                For Each row As GridDataItem In grdOrderDetails.Items
                    chk1 = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                    If chk1.Checked = True Then
                        Dim txtPayAmt As TextBox = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                        Dim txtPayAmmount As String = txtPayAmt.Text
                        Dim sTotleAmmount As String = txtPayAmmount.ToString().Replace(sSymCurr, "")
                        Dim iTotalAmmount As Decimal
                        iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")
                        iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount

                        txtTotal.Text = String.Format("{0:n2}", sSymCurr & iSumTotalPayAmmount)
                    End If
                Next

                For Each row As GridDataItem In grdOrderDetails.Items
                    chk1 = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                    If chk1.Checked = True Then
                        flag = True
                    End If
                Next
                If Not flag Then
                    txtTotal.Text = sSymCurr & "0.00"
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try



        End Sub
        'This method is used to populate the saved checkbox values
        Private Sub PopulateCheckedValues()
            Dim userdetails As ArrayList = DirectCast(ViewState("CHECKED_ITEMS"), ArrayList)
            If userdetails IsNot Nothing AndAlso userdetails.Count > 0 Then
                For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                    Dim index As Long = CLng(item("ID").Text)
                    If userdetails.Contains(index) Then
                        Dim myCheckBox As CheckBox = DirectCast(item.FindControl("chkRmvCompLink"), CheckBox)
                        myCheckBox.Checked = True
                    End If
                Next
            End If
        End Sub

        'This method is used to save the checkedstate of values
        Private Sub SaveCheckedValues()
            Dim userdetails As New ArrayList()
            Dim index As Long = -1
            For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                index = item("ID").Text
                Dim result As Boolean = DirectCast(item.FindControl("chkMakePayment"), CheckBox).Checked
                'Check in the ViewState
                If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                    userdetails = DirectCast(ViewState("CHECKED_ITEMS"), ArrayList)
                End If
                If result Then
                    If Not userdetails.Contains(index) Then
                        userdetails.Add(index)
                    End If
                Else
                    userdetails.Remove(index)
                End If
            Next

            If userdetails IsNot Nothing AndAlso userdetails.Count > 0 Then
                ViewState("CHECKED_ITEMS") = userdetails
            End If
        End Sub

        Protected Sub btnok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
            radpaymentmsg.VisibleOnPageLoad = False
            ' LoadOrders()

        End Sub
        Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim headerCheckBox As CheckBox = CType(sender, CheckBox)
                For Each dataItem As GridDataItem In grdOrderDetails.MasterTableView.Items
                    CType(dataItem.FindControl("chkMakePayment"), CheckBox).Checked = headerCheckBox.Checked
                    dataItem.Selected = headerCheckBox.Checked

                Next
                Dim txtPayAmt As TextBox
                Dim iSumTotalPayAmmount As Decimal
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                For Each row As GridDataItem In grdOrderDetails.Items
                    Dim chk1 As CheckBox = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                    If chk1.Checked = True Then
                        Dim textBoxText As String = (DirectCast(row.FindControl("lblBalanceAmount"), Label)).Text
                        txtPayAmt = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                        txtPayAmt.Text = textBoxText.ToString().Replace(sSymCurr, "")
                        Dim txtPayAmmount As String = txtPayAmt.Text
                        Dim sTotleAmmount As String = txtPayAmmount.ToString().Replace(sSymCurr, "")
                        Dim iTotalAmmount As Decimal
                        iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")
                        iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
                        txtTotal.Text = String.Format("{0:n2}", sSymCurr & iSumTotalPayAmmount)

                    Else
                        txtPayAmt = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                        txtPayAmt.Text = "0.00"
                        txtTotal.Text = sSymCurr & "0.00"
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdOrderDetails_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles grdOrderDetails.PageSizeChanged
            LoadOrders()
            '  SaveCheckedValues()


        End Sub

        Protected Sub grdOrderDetails_PreRender(sender As Object, e As EventArgs) Handles grdOrderDetails.PreRender
            'For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items

            '    Dim orderID As String = item.Cells(3).Text
            '    If CInt(orderID) = iOrderId Then
            '        'item.Cells(3).Text = ""
            '        item.Cells(4).Text = ""
            '        item.Cells(3).Text = ""
            '        item.Cells(2).Text = ""
            '        item.Cells(1).Text = ""
            '    Else
            '        iOrderId = CInt(orderID)
            '    End If
            'Next
        End Sub
    End Class
End Namespace
