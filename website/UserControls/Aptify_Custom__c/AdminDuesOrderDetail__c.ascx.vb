
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

    Partial Class AdminDuesOrderDetail__c
        Inherits BaseUserControlAdvanced
        Protected Const ATTRIBUTE_ORDER_CONFIRMATION_PAGE As String = "OrderConfirmationURL"
        Protected Const ATTRIBUTE_ORDER_DUEPRINT_PAGE As String = "OrderDuesPrintURL"
        Protected Const ATTRIBUTE_Login_PAGE As String = "LoginPageURL"
        Dim sCuurSymbol As String
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
        ''' 'Order  Dues Print url
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Property OrderDuesPrintURL() As String
            Get
                If Not ViewState(ATTRIBUTE_ORDER_DUEPRINT_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ORDER_DUEPRINT_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ORDER_DUEPRINT_PAGE) = Me.FixLinkForVirtualPath(value)
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

            If String.IsNullOrEmpty(OrderDuesPrintURL) Then
                'since value is the 'default' check the XML file for possible custom setting
                OrderDuesPrintURL = Me.GetLinkValueFromXML(ATTRIBUTE_ORDER_DUEPRINT_PAGE)
                If String.IsNullOrEmpty(OrderDuesPrintURL) Then
                    prnLink.Enabled = False
                Else
                    prnLink.Enabled = True
                    prnLink.NavigateUrl = OrderDuesPrintURL
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
                sSQL = Database & "..spGetDuesOrderDetails__c"
                param(0) = Me.DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, User1.CompanyID)
                dt = Me.DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)

                Session("dt") = dt
                Dim dcolUrl As DataColumn = New DataColumn()
                dcolUrl.Caption = "OrderConfirmationURL"
                dcolUrl.ColumnName = "OrderConfirmationURL"
                'CMH 1/29/2013 Added column to display message if a promise has been made to pay for an order.

                dt.Columns.Add(dcolUrl)

                If dt.Rows.Count > 0 Then
                    For Each rw As DataRow In dt.Rows
                        rw("OrderConfirmationURL") = OrderConfirmationURL & "?ID=" + rw("ID").ToString & "&PersonID=" & User1.PersonID.ToString
                    Next
                End If

                For i = 0 To dt.Rows.Count - 1
                    sSQL = "SELECT P.Name FROM " & Database & "..vwOrderDetail OD INNER JOIN VwProducts P ON P.ID = OD.ProductID WHERE p.DefaultDuesProduct=1 and  OrderID = " & dt.Rows(i)("ID")
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
                    '  grdOrderDetails.DataBind()
                    'AssignDefaultPayment()
                Else
                    lblTotal.Visible = False
                    txtTotal.Visible = False
                    CreditCard1.Visible = False
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
                    CreditCard1.LoadPaymentTypeInfo()

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
            ' Dim lblPay As New Label
            Dim OrderID As HyperLink
            Dim txtPay As New TextBox
            Try
                ReDim arPay(0)
                If Page.IsValid Then

                    For i = 0 To grdOrderDetails.Items.Count - 1
                        'lblPay = grdOrderDetails.Items(i).FindControl("lblBalanceAmount")
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
                        'If Len(CreditCard1.CCNumber) = 0 Or _
                        '   Len(CreditCard1.CCExpireDate) = 0 Then
                        '    lblError.Text = "Credit Card Information Required"
                        '    lblError.Visible = True
                        'End If

                        If PostPayment(arPay) Then
                            CreditCard1.Clear()

                            LoadOrders()
                            lblPaymentError.Text = String.Empty
                            radpaymentmsg.VisibleOnPageLoad = True
                            grdOrderDetails.Rebind()

                            'Response.Redirect(Request.Path & "?msg=Payment was made!  Your updated order information is shown below.", False)
                        Else

                            lblPaymentError.Text = "An error took place while processing your payment"
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
        Private Function PostPayment(ByVal arPay() As PayInfo) As Boolean
            ' post the payment to the database using the CGI GE
            Dim oPayment As AptifyGenericEntityBase
            Dim i As Integer
            Dim bFlag As Boolean = False
            Dim bNotification As Boolean = False
            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity = Nothing
            Dim strError As String = String.Empty
            Dim strPersonList As String = String.Empty
            Try


                oPayment = AptifyApplication.GetEntityObject("Payments", -1)
                Dim param1(0) As IDataParameter
                Dim sql As String = "spGetPaymentLevelID__c"
                Dim PaymentLevelID As Integer
                'Dim k As Integer = Convert.ToInt16(DataAction.ExecuteScalarParametrized(sql, CommandType.StoredProcedure, param1))
                Dim k As Integer = Convert.ToInt16(DataAction.ExecuteScalar(sql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If k > 0 Then
                    PaymentLevelID = k
                End If
                oPayment.SetValue("PaymentLevelID", PaymentLevelID)
                oPayment.SetValue("EmployeeID", EBusinessGlobal.WebEmployeeID(Page.Application))
                oPayment.SetValue("PersonID", User1.PersonID)
                oPayment.SetValue("CompanyID", User1.CompanyID)
                oPayment.SetValue("PaymentDate", Date.Today)
                oPayment.SetValue("DepositDate", Date.Today)
                oPayment.SetValue("EffectiveDate", Date.Today)

                Dim sqlStr As String = "spGetPayType"
                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("ID", SqlDbType.Int, CreditCard1.PaymentTypeID)
                Dim PayTye As String = Convert.ToString(DataAction.ExecuteScalarParametrized(sqlStr, CommandType.StoredProcedure, param))
                If PayTye.ToLower = "wire transfer" Then
                    trPaymentError.Visible = True
                    oPayment.SetValue("PaymentTypeID", CreditCard1.PaymentTypeID)
                    oPayment.SetValue("CheckNumber", CreditCard1.TransactionNumber)
                    oPayment.SetValue("Bank", CreditCard1.BankName)
                    oPayment.SetValue("AccountNumber", CreditCard1.AccountNumber)
                    '  oPayment.SetValue("RoutingNumber__c", CreditCard1.RoutingNumber)
                    oPayment.SetValue("AccountName", CreditCard1.NameOfAccount)
                    oPayment.SetValue("BranchName", CreditCard1.BranchName)
                    oPayment.SetValue("ABA", CreditCard1.ABA)

                    For i = 0 To UBound(arPay) - 1
                        With oPayment.SubTypes("PaymentLines").Add
                            .SetValue("Amount", arPay(i).PayAmount)
                            .SetValue("OrderID", arPay(i).OrderID)
                        End With

                        '11/29/2012
                        'If oPayment.RecordID > 0 Then
                        oOrder = AptifyApplication.GetEntityObject("Orders", CLng(arPay(i).OrderID))
                        oOrder.SetValue("IsDuesAmountPaid__c", 1)
                        oOrder.Save(False, strError)
                        'End If
                    Next

                    If oPayment.Save(False, strError) Then
                        For i = 0 To UBound(arPay) - 1
                            If CheckStudentMemberType(arPay(i).OrderID) Then
                                If CheckProductsForAttorenyMember(arPay(i).OrderID).Rows.Count > 0 Then
                                    strPersonList = strPersonList & CheckProductsForAttorenyMember(arPay(i).OrderID).Rows(i)("PersonName")
                                    bNotification = True
                                End If
                            End If
                        Next
                        If bNotification Then
                            SendPaymentNotification(strPersonList, User1.PersonID)
                        End If

                        strPersonList = String.Empty
                        lblpayment.Text = " Your payment was made successfully!"
                        Return oPayment.Save(False, strError)
                        bFlag = True
                    Else
                        Throw New System.Exception(strError)
                    End If
                ElseIf PayTye.ToLower = "purchase order" Then
                    If ChechCreditLimit(User1.CompanyID) Then
                        For i = 0 To UBound(arPay) - 1
                            If CheckStudentMemberType(arPay(i).OrderID) Then
                                If CheckProductsForAttorenyMember(arPay(i).OrderID).Rows.Count > 0 Then
                                    strPersonList = strPersonList & CheckProductsForAttorenyMember(arPay(i).OrderID).Rows(i)("PersonName")
                                    bNotification = True
                                End If
                            End If

                            oOrder = AptifyApplication.GetEntityObject("Orders", CLng(arPay(i).OrderID))
                            If Convert.ToString(oOrder.GetValue("OrderType")).ToUpper() = "REGULAR" OrElse Convert.ToString(oOrder.GetValue("OrderType")).ToUpper() = "QUOTATION" Then
                                If oOrder.GetValue("PayTypeID") IsNot Nothing AndAlso Convert.ToUInt32(oOrder.GetValue("PayTypeID")) > 0 Then
                                    oOrder.SetValue("IsDuesAmountPaid__c", 1)
                                    oOrder.SetValue("Comments", String.Format("{0:C}", arPay(i).PayAmount) & " " & "is paid by firm admin against the purchase order")
                                End If
                            Else
                                oOrder.SetValue("IsDuesAmountPaid__c", 1)
                                oOrder.SetValue("OrderTypeID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Types", "Regular"))
                                oOrder.SetValue("PayTypeID", AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order"))
                                oOrder.SetValue("PONumber", "NA")
                                'oOrder.SetValue("IsDuesAmtPaidForPurchaseOrder__c", 1)
                                oOrder.SetValue("Comments", String.Format("{0:C}", arPay(i).PayAmount) & " " & "is paid by firm admin against the purchase order")
                            End If
                            If oOrder.Save(False, strError) Then
                                bFlag = True
                                lblpayment.Text = " Your payment was made successfully!"
                            End If
                        Next
                        If bNotification Then
                            SendPaymentNotification(strPersonList, User1.PersonID)
                        End If
                    Else
                        trPaymentError.Visible = False
                        lblpayment.Text = "This company has a base credit status of 'Not Approved'."
                        radpaymentmsg.VisibleOnPageLoad = True
                        bFlag = True
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
            Return bFlag
        End Function
        ''' <summary>
        ''' Send Email for Payment Notification To MSBA Staff
        ''' </summary>
        ''' <param name="PesrsonList"></param>
        ''' <param name="PersonID"></param>
        ''' <remarks></remarks>
        Protected Sub SendPaymentNotification(ByVal PesrsonList As String, ByVal PersonID As Long)
            Try
                Dim lProcessFlowID As Long
                Dim msbstaffEmail As String = AptifyApplication.GetEntityAttribute("Persons", "MSBAStaffEmailAddress")
                Dim strUrl As String = String.Empty
                Dim sSQL As String = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='Send Notification To MSBA Staff For Order Payment__c'"
                Dim oProcessFlowID As Object = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache)
                If oProcessFlowID IsNot Nothing AndAlso IsNumeric(oProcessFlowID) Then

                    lProcessFlowID = CLng(oProcessFlowID)
                    Dim context As New AptifyContext
                    context.Properties.AddProperty("Email1", msbstaffEmail)
                    context.Properties.AddProperty("PersonID", PersonID)
                    context.Properties.AddProperty("PesrsonList", PesrsonList)

                    Dim oResult As ProcessFlowResult
                    oResult = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
                    If Not oResult.IsSuccess Then
                        ExceptionManagement.ExceptionManager.Publish(New Exception("Process flow to send Web Credentials through Email. Please refer event handler for more details."))
                        ' lblWarning.Text = "Failed to Send Invitation."
                        '  radWarning.VisibleOnPageLoad = True
                    Else
                        '  lblWarning.Text = "Invitation Sent Successfully."
                        ' radWarning.VisibleOnPageLoad = True
                    End If



                End If
            Catch ex As ArgumentException
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' For checking company has CreditStatus has Approved or not 
        ''' </summary>
        ''' <param name="CompanyID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function ChechCreditLimit(ByVal CompanyID As Integer) As Boolean
            Dim bFlag As Boolean = False
            Dim sSQL As String = String.Empty
            Dim dt As DataTable = Nothing
            Dim param(0) As IDataParameter
            Try
                sSQL = Database & "..spGetCreditStatusOfCompanyByID__c"
                param(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.BigInt, CompanyID)
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If dt.Rows(0)("CreditStatus").ToString().ToLower = "approved" Then
                        bFlag = True
                    Else
                        bFlag = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return bFlag
            End Try
            Return bFlag
        End Function
        ''' <summary>
        ''' Get membertype of ShipToId/Person
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function CheckStudentMemberType(ByVal OrderID As Integer) As Boolean
            Dim sSQL As String = String.Empty
            Dim dt As DataTable = Nothing
            Dim bFlag As Boolean = False
            Dim param(0) As IDataParameter
            Try
                sSQL = Database & "..spGetmembershipTypeByOrderID__c"
                param(0) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, OrderID)
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If dt.Rows(0)("MemberType").ToString.ToLower = "student" Then
                        bFlag = True
                    Else
                        bFlag = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return bFlag
            End Try
            Return bFlag
        End Function
        ''' <summary>
        ''' Check Products For Attoreny Member
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function CheckProductsForAttorenyMember(ByVal OrderID As Integer) As DataTable
            Dim sSQL As String = String.Empty
            Dim dt As DataTable = Nothing
            Dim bFlag As Boolean = False
            Dim param(0) As IDataParameter
            Try
                sSQL = Database & "..spGetMemberShipDuesProductByOorderID__c"
                param(0) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, OrderID)
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
               
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return dt
            End Try
            Return dt
        End Function
        Protected Sub chkMakePayment_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim flag As Boolean = False
            Dim chk1 As CheckBox = CType(sender, CheckBox)

            Dim header As GridHeaderItem = TryCast(grdOrderDetails.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
            Dim chkAllMakePayment As CheckBox = DirectCast(header.FindControl("chkAllMakePayment"), CheckBox)

            Dim grow As GridDataItem = DirectCast(chk1.NamingContainer, GridDataItem)
            Dim i As Integer = grow.DataSetIndex
            Dim txtPaymentaMT As TextBox = (DirectCast(grdOrderDetails.Items(i).FindControl("txtPayAmt"), TextBox))
            Dim sSymCurrancy As String = ViewState("sCuurSymbol").ToString().Trim()
            If chk1.Checked = True Then
                Dim textBoxText As String = (DirectCast(grdOrderDetails.Items(i).FindControl("lblBalanceAmount"), Label)).Text
                txtPaymentaMT.Text = textBoxText.ToString().Replace(sSymCurrancy, "")

            Else
                txtPaymentaMT.Text = "0.00"
            End If
            Dim iSumTotalPayAmmount As Decimal
            Dim chkflag As Boolean = True
            For Each row As GridDataItem In grdOrderDetails.Items
                chk1 = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                If chk1.Checked = True Then
                    Dim txtPayAmt As TextBox = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                    Dim txtPayAmmount As String = txtPayAmt.Text
                    Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                    Dim sTotleAmmount As String = txtPayAmmount.ToString().Replace(sSymCurr, "")
                    Dim iTotalAmmount As Decimal
                    iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")
                    iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
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
        Protected Sub btnok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
            radpaymentmsg.VisibleOnPageLoad = False
            'LoadOrders()
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
        End Sub
    End Class
End Namespace
