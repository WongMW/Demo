'Aptify e-Business 5.5.1, July 2013
'Updated By Rajesh for Firm Potal Config
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Rajesh W.             5/07/2014                         Firm Potal Config
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Applications.OrderEntry.Payments
Imports Aptify.Applications.Accounting
Imports Aptify.Consulting.Entity.OrderEntry
Imports Aptify.Framework.BusinessLogic.ProcessPipeline
Imports Telerik.Web.UI
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.AttributeManagement
Imports System.Data
Imports Aptify.Framework.Web.eBusiness
Imports System.ComponentModel
Imports System.Collections.Generic
Imports Aptify.Applications.OrderEntry


Namespace Aptify.Framework.Web.eBusiness.ProductCatalog

    Partial Class AdminOrderDetailSummary__c
        Inherits BaseUserControlAdvanced
        Protected Const ATTRIBUTE_ORDER_CONFIRMATION_PAGE As String = "OrderConfirmationURL"
        Protected Const ATTRIBUTE_ADMIN_PAYMENT_SUMMARY As String = "AdminPaymentSummary"
        Protected Const ATTRIBUTE_NOTIFICATION_IMAGE As String = "PaymentNotificationImage"
        Protected Const ATTRIBUTE_NOTIFICATION_TASKTYPE As String = "TaskType"
        Protected Const ATTRIBUTE_NOTIFICATION_TASKASSIGNEDTOID As String = "AssignedTOID"
        Protected Const ATTRIBUTE_NOTIFICATION_TASKDESCRIPTION As String = "TaskDescription"
        Protected Const ATTRIBUTE_NOTIFICATION_TASKSTATUS As String = "TaskStatus"
        Protected Const ATTRIBUTE_NOTIFICATION_TASKPRIORITY As String = "TaskPriority"
        Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage" 'Added by Sandeep for Issue 15051 on 12/03/2013
        Dim sCuurSymbol As String
        Dim sTrans As String = Nothing
        Protected Const ATTRIBUTE_BILL_ME_LATER As String = "BillMeLaterDisplayText"
        Protected Const ATTRIBUTE_ADMIN_PAYMENT_DETAIL As String = "AdminOrderDetail"

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

        Public Overridable Property AdminPaymentSummary() As String
            Get
                If Not ViewState(ATTRIBUTE_ADMIN_PAYMENT_SUMMARY) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ADMIN_PAYMENT_SUMMARY))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ADMIN_PAYMENT_SUMMARY) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' PaymentNotificationImage property
        ''' </summary>
        ''' <remarks></remarks>
        Public Overridable Property PaymentNotificationImage() As String
            Get
                If Not ViewState(ATTRIBUTE_NOTIFICATION_IMAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_NOTIFICATION_IMAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_NOTIFICATION_IMAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property TaskType() As String
            Get
                If Not ViewState(ATTRIBUTE_NOTIFICATION_TASKTYPE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_NOTIFICATION_TASKTYPE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_NOTIFICATION_TASKTYPE) = value
            End Set
        End Property

        Public Overridable Property AssignedTOID() As String
            Get
                If Not ViewState(ATTRIBUTE_NOTIFICATION_TASKASSIGNEDTOID) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_NOTIFICATION_TASKASSIGNEDTOID))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_NOTIFICATION_TASKASSIGNEDTOID) = value
            End Set
        End Property

        Public Overridable Property TaskDescription() As String
            Get
                If Not ViewState(ATTRIBUTE_NOTIFICATION_TASKDESCRIPTION) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_NOTIFICATION_TASKDESCRIPTION))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_NOTIFICATION_TASKDESCRIPTION) = value
            End Set
        End Property

        Public Overridable Property TaskStatus() As String
            Get
                If Not ViewState(ATTRIBUTE_NOTIFICATION_TASKSTATUS) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_NOTIFICATION_TASKSTATUS))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_NOTIFICATION_TASKSTATUS) = value
            End Set
        End Property

        Public Overridable Property TaskPriority() As String
            Get
                If Not ViewState(ATTRIBUTE_NOTIFICATION_TASKPRIORITY) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_NOTIFICATION_TASKPRIORITY))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_NOTIFICATION_TASKPRIORITY) = value
            End Set
        End Property
        'Added by Sandeep for Issue 15051 on 12/03/2013
        Public Overridable Property LoginPage() As String
            Get
                If Not ViewState(ATTRIBUTE_LOGIN_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_LOGIN_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_LOGIN_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        Private Property orderUniqueID() As String
            Get
                If Not ViewState("orderUniqueID") Is Nothing Then
                    Return CStr(ViewState("orderUniqueID"))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState("orderUniqueID") = value
            End Set
        End Property

        Private Property TotalPayAmount() As Decimal
            Get
                If Not ViewState("TotalPayAmount") Is Nothing Then
                    Return CDbl(ViewState("TotalPayAmount"))
                Else
                    Return 0
                End If
            End Get
            Set(ByVal value As Decimal)
                ViewState("TotalPayAmount") = value
            End Set
        End Property

        Private Property TotalOrderAmount() As Decimal
            Get
                If Not ViewState("TotalOrderAmount") Is Nothing Then
                    Return CDbl(ViewState("TotalOrderAmount"))
                Else
                    Return 0
                End If
            End Get
            Set(ByVal value As Decimal)
                ViewState("TotalOrderAmount") = value
            End Set
        End Property

        Private Property BenevolentDonationProductID() As Long
            Get
                If Not ViewState("BenevolentDonationProductID") Is Nothing Then
                    Return CLng(ViewState("BenevolentDonationProductID"))
                Else
                    Return 0
                End If
            End Get
            Set(ByVal value As Long)
                ViewState("BenevolentDonationProductID") = value
            End Set
        End Property

        ''RashmiP, issue 6781
        Public Overridable ReadOnly Property BillMeLaterDisplayText() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_BILL_ME_LATER) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_BILL_ME_LATER))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_BILL_ME_LATER)
                    If Not String.IsNullOrEmpty(value) Then
                        ViewState.Item(ATTRIBUTE_BILL_ME_LATER) = value
                    End If
                    Return value
                End If
            End Get
        End Property

        Public Overridable Property AdminOrderDetail() As String
            Get
                If Not ViewState(ATTRIBUTE_ADMIN_PAYMENT_DETAIL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ADMIN_PAYMENT_DETAIL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ADMIN_PAYMENT_DETAIL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(OrderConfirmationURL) Then
                'since value is the 'default' check the XML file for possible custom setting
                OrderConfirmationURL = Me.GetLinkValueFromXML(ATTRIBUTE_ORDER_CONFIRMATION_PAGE)
                If String.IsNullOrEmpty(OrderConfirmationURL) Then
                    Me.grdOrderDetails.Enabled = False
                    Me.grdOrderDetails.ToolTip = "OrderConfirmationURL property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(AdminPaymentSummary) Then
                'since value is the 'default' check the XML file for possible custom setting
                AdminPaymentSummary = Me.GetLinkValueFromXML(ATTRIBUTE_ADMIN_PAYMENT_SUMMARY)
                If String.IsNullOrEmpty(AdminPaymentSummary) Then
                    Me.grdOrderDetails.Enabled = False
                    Me.grdOrderDetails.ToolTip = "AdminPaymentSummary property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(PaymentNotificationImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                PaymentNotificationImage = Me.GetLinkValueFromXML(ATTRIBUTE_NOTIFICATION_IMAGE)
                If String.IsNullOrEmpty(PaymentNotificationImage) Then
                    Me.grdOrderDetails.Enabled = False
                    Me.grdOrderDetails.ToolTip = "PaymentNotificationImage property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(TaskType) Then
                'since value is the 'default' check the XML file for possible custom setting
                TaskType = Me.GetPropertyValueFromXML(ATTRIBUTE_NOTIFICATION_TASKTYPE)
                If String.IsNullOrEmpty(TaskType) Then
                    Me.grdOrderDetails.Enabled = False
                    Me.grdOrderDetails.ToolTip = "TaskType property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(AssignedTOID) Then
                'since value is the 'default' check the XML file for possible custom setting
                AssignedTOID = Me.GetPropertyValueFromXML(ATTRIBUTE_NOTIFICATION_TASKASSIGNEDTOID)
                If String.IsNullOrEmpty(AssignedTOID) Then
                    Me.grdOrderDetails.Enabled = False
                    Me.grdOrderDetails.ToolTip = "AssignedTOID property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(TaskDescription) Then
                'since value is the 'default' check the XML file for possible custom setting
                TaskDescription = Me.GetPropertyValueFromXML(ATTRIBUTE_NOTIFICATION_TASKDESCRIPTION)
                If String.IsNullOrEmpty(TaskDescription) Then
                    Me.grdOrderDetails.Enabled = False
                    Me.grdOrderDetails.ToolTip = "TaskDescription property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(TaskStatus) Then
                'since value is the 'default' check the XML file for possible custom setting
                TaskStatus = Me.GetPropertyValueFromXML(ATTRIBUTE_NOTIFICATION_TASKSTATUS)
                If String.IsNullOrEmpty(TaskStatus) Then
                    Me.grdOrderDetails.Enabled = False
                    Me.grdOrderDetails.ToolTip = "TaskStatus property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(TaskPriority) Then
                'since value is the 'default' check the XML file for possible custom setting
                TaskPriority = Me.GetPropertyValueFromXML(ATTRIBUTE_NOTIFICATION_TASKPRIORITY)
                If String.IsNullOrEmpty(TaskPriority) Then
                    Me.grdOrderDetails.Enabled = False
                    Me.grdOrderDetails.ToolTip = "TaskPriority property has not been set."
                End If
            End If
            'Added by Sandeep for Issue 15051 on 12/03/2013
            If String.IsNullOrEmpty(LoginPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_LOGIN_PAGE)
            End If

            If String.IsNullOrEmpty(AdminOrderDetail) Then
                'since value is the 'default' check the XML file for possible custom setting
                AdminOrderDetail = Me.GetLinkValueFromXML(ATTRIBUTE_ADMIN_PAYMENT_DETAIL)
            End If
            Dim hShippinglink As Telerik.Web.UI.GridHyperLinkColumn = CType(grdShipping.Columns(0), Telerik.Web.UI.GridHyperLinkColumn)
            hShippinglink.DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
        End Sub

        Private Class PayInfo
            Public OrderID As Long
            Public OrderLineID As Long
            Public LineNumber As Long
            Public PayAmount As Decimal
            Public Balance As Decimal
            Public IsDonation As Integer
        End Class

        Private Class PayShippingInfo
            Public OrderID As Long
            Public OrderShipmentID As Long
            Public PayAmount As Decimal
            Public Balance As Decimal
        End Class

        Private Sub LoadOrders()
            Dim sSQL As String
            Dim sProducts As String = String.Empty
            Dim i As Integer, j As Integer
            Dim icount As Integer = 0
            Dim iProdCnt As Integer = 0
            Try
                If ViewState("orderdt") Is Nothing Then

                Else
                    'sSQL = "SELECT CurrencySymbol FROM " & _
                    '      Database & "..vwCurrencyTypes where ID=" & radcurrency.SelectedValue().Trim()
                    sSQL = Database & "..spGetCurrencySymbol__c @CurrencyID=" & radcurrency.SelectedValue().Trim()
                    Dim dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        sCuurSymbol = dt.Rows(0)("CurrencySymbol").ToString().Trim
                        ViewState("sCuurSymbol") = sCuurSymbol
                    End If
                    'Add selected order in dictionary
                    Dim orderdetails As New Dictionary(Of Integer, Decimal)
                    If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                        orderdetails = DirectCast(ViewState("CHECKED_ITEMS"), Dictionary(Of Integer, Decimal))
                    End If
                    If orderdetails Is Nothing Or orderdetails.Count <= 0 Then
                        txtTotal.Text = sCuurSymbol & "0.00"
                    End If
                    grdOrderDetails.DataSource = ViewState("orderdt")

                    LoadShippingGrid()
                End If
                ViewState("OrderNo") = Nothing
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadShippingGrid()
            Dim sSQL As String
            Dim i As Integer, index As Integer = 0
            Dim OrderID As String = ""
            Dim dtOrder As Data.DataTable
            Try
                If ViewState("ordershippingdt") Is Nothing Then
                    Dim ordernos As New Dictionary(Of Integer, Decimal)
                    If ViewState("OrderNo") IsNot Nothing Then
                        ordernos = DirectCast(ViewState("OrderNo"), Dictionary(Of Integer, Decimal))
                    Else
                        For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                            Dim lblOrderNo As System.Web.UI.WebControls.HyperLink = DirectCast(item.FindControl("lblOrderNo"), System.Web.UI.WebControls.HyperLink)
                            Dim OrderNo As Long = CLng(lblOrderNo.Text.Trim)
                            If ordernos.ContainsKey(OrderNo) = False Then
                                ordernos.Add(OrderNo, OrderNo)
                            End If
                        Next
                        ViewState("OrderNo") = ordernos
                    End If

                    If ordernos.Count > 0 Then
                        For index = 0 To ordernos.Count - 1
                            If OrderID.Trim <> "" Then
                                OrderID = OrderID + "," + CStr(ordernos.Values(index))
                            Else
                                OrderID = CStr(ordernos.Values(index))
                            End If
                        Next

                        Dim params(1) As System.Data.IDataParameter
                        params(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, CInt(User1.CompanyID))
                        params(1) = DataAction.GetDataParameter("@OrderID", SqlDbType.VarChar, OrderID)
                        sSQL = Database & "..spGetOrderShippingDetails__c"
                        dtOrder = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                        Dim dcolUrl As DataColumn = New DataColumn()
                        dcolUrl.Caption = "OrderConfirmationURL"
                        dcolUrl.ColumnName = "OrderConfirmationURL"
                        dtOrder.Columns.Add(dcolUrl)
                        If dtOrder.Rows.Count > 0 Then
                            For Each rw As DataRow In dtOrder.Rows
                                rw("OrderConfirmationURL") = OrderConfirmationURL + "?ID=" + rw("ID").ToString
                            Next
                        End If
                        grdShipping.DataSource = dtOrder
                        ViewState("ordershippingdt") = dtOrder
                        grdShipping.Visible = (dtOrder.Rows.Count > 0)
                        lblShipping.Visible = (dtOrder.Rows.Count > 0)
                        trShiiping.Visible = (dtOrder.Rows.Count > 0)
                    End If
                Else
                    grdShipping.DataSource = ViewState("ordershippingdt")
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdOrderDetails_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdOrderDetails.PageIndexChanged
            ''SaveCheckedValues()
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

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Hidden.Value = "true"
            SetProperties()
            Try
                If Not IsPostBack Then
                    'Anil B change for 10737 on 13/03/2013
                    'Set Credit Card ID to load property form Navigation Config
                    If Session("orderdt") IsNot Nothing Then
                        ViewState("orderdt") = Session("orderdt")
                        Dim column2Values As EnumerableRowCollection(Of Integer) = CType(ViewState("orderdt"), DataTable).AsEnumerable().Select(Function(x) x.Field(Of Integer)("ID"))
                        Dim OrderID As String = ""
                        For index = 0 To column2Values.Count - 1
                            If (OrderID <> "") Then
                                OrderID = OrderID + "," + column2Values(index).ToString
                            Else
                                OrderID = column2Values(index).ToString
                            End If
                        Next

                        Dim uniqueGuid As String = GetWebRemittanceNumber()
                        orderUniqueID = uniqueGuid
                        ' lblReceiptNo.Text = orderUniqueID
                        Session("OrderuniqueGuid") = orderUniqueID
                        If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Orders", "BenevolentDonationProductID__c")) Then
                            BenevolentDonationProductID = CLng(AptifyApplication.GetEntityAttribute("Orders", "BenevolentDonationProductID__c"))
                        End If
                        'txtDonation.Text = String.Format("{0:n2}", CDbl(AptifyApplication.GetEntityAttribute(969, "BenevolentDonationAmount")))
                        txtDonation.Text = String.Format("{0:n2}", GetBenevolentDonationAmount(BenevolentDonationProductID))
                    Else
                        ' Response.Redirect("~/OrdersManagement/AdminOrderDetail.aspx")
                        Response.Redirect(AdminOrderDetail)
                    End If
                    CreditCard.LoadCreditCardInfo()
                    LoadCurrency()
                    'ViewState.Remove("orderdt")
                    AddExpression()
                    Session("IsBillMeLater") = Nothing
                End If
                lblError.Text = ""
                If User1.UserID < 0 Then
                    Response.Redirect(LoginPage) 'Added by Sandeep for Issue 15051 on 12/03/2013
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdOrderDetails_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdOrderDetails.NeedDataSource
            LoadOrders()
        End Sub

        Protected Sub btnok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
            radpaymentmsg.VisibleOnPageLoad = False
        End Sub

        ' code added for splitting orser
        Private Sub checkBillingCompany()
            Try
                If Session("orderdt") IsNot Nothing Then
                    Dim dt As DataTable = CType(Session("orderdt"), DataTable)
                    Dim view As New DataView(dt)
                    'Dim distinctValues As DataTable = view.ToTable(True, "ID")

                    Dim TobeDistinct As String() = {"ID", "OrderType"}
                    Dim distinctValues As DataTable = view.ToTable(True, TobeDistinct)

                    Dim sOrderID As String = String.Empty
                    For Each dr As DataRow In distinctValues.Rows
                        ' only Quatation type orders

                        Dim drOrderLine() As DataRow = dt.Select("ID=" & Convert.ToInt32(dr("ID")))
                        Dim sOrderlineID As String = String.Empty
                        If drOrderLine.Length > 0 Then
                            For i As Integer = 0 To drOrderLine.Length - 1
                                If sOrderlineID = "" Then
                                    sOrderlineID = Convert.ToString(drOrderLine(i)(17))
                                Else

                                    sOrderlineID = sOrderlineID + "," + Convert.ToString(drOrderLine(i)(17))
                                End If
                            Next
                        End If
                        'Dim sSqlQuotation As String = Database & "..spCheckOrderIsFirmQuotation__c @OrderID='" & Convert.ToString(dr("ID")) & "',@OrderLine='" & sOrderlineID & "'"
                        'Dim dtNewOrder As DataTable = DataAction.GetDataTable(sSqlQuotation, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        Dim dtNewOrder As DataTable = Nothing
                        If Convert.ToString(dr("OrderType")).Trim = "Quotation" Then
                            Dim sSqlQuotation As String = Database & "..spCheckOrderIsFirmQuotation__c @OrderID='" & Convert.ToString(dr("ID")) & "',@OrderLine='" & sOrderlineID & "'"
                            dtNewOrder = DataAction.GetDataTable(sSqlQuotation, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        End If
                        Dim oOrderGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                        oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(dr("ID")))

                        If Not dtNewOrder Is Nothing AndAlso dtNewOrder.Rows.Count > 0 Then
                            'Dim oOrderGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                            'oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(dr("ID")))
                            Dim oNewOrderGE As Aptify.Applications.OrderEntry.OrdersEntity
                            oNewOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                            oNewOrderGE.ShipToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                            oNewOrderGE.BillToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                            ' Redmin issue #16086
                            Dim sSQLMem As String = Database & "..spGetMemAppSplitOrder__c @OrderID=" & Convert.ToInt32(dr("ID"))
                            Dim iMemApp As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQLMem, IAptifyDataAction.DSLCacheSetting.BypassCache))

                            Dim bAssignAccountPayble As Boolean = False
                            For Each drRow As DataRow In dtNewOrder.Rows
                                If Convert.ToInt32(drRow("ProductID")) = Convert.ToInt32(AptifyApplication.GetEntityAttribute("Orders", "BenevolentDonationProductID__c")) Then
                                    ' not add BenevolentDonationProductID in new order
                                    If rdbYes.Checked = True Then
                                        ' don't add to person 
                                    Else
                                        oNewOrderGE.AddProduct(CLng(drRow("ProductID")))
                                    End If
                                Else
                                    oNewOrderGE.AddProduct(CLng(drRow("ProductID")))
                                End If
                                oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(drRow("Sequence")) - 1).SetValue("Quantity", 0)

                                'oNewOrderGE.SubTypes("OrderLines").Item(oNewOrderGE.SubTypes("OrderLines").Count - 1).SetValue("BillToCompanyID__c", oOrderGE.GetValue(("ShipToCompanyID")))
                                oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(drRow("Sequence")) - 1).SetAddValue("_xDeletedOrderlines", 1)
                                oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(drRow("Sequence")) - 1).Delete()

                                ''Commented by Pradip 2017-04-11
                                'oOrderGE.SetAddValue("_xDeletedOrderlines", 1)
                                'If oOrderGE.Save() Then
                                '    oOrderGE.SetAddValue("_xDeletedOrderlines", 0)
                                'End If

                            Next
                            oOrderGE.SetAddValue("_xDeletedOrderlines", 1)
                            Dim sError As String = String.Empty
                            Dim sSQL As String = "..spGetBillingContactID__c @CompanyID=" & Convert.ToInt32(oOrderGE.GetValue("ShipToCompanyID"))
                            Dim iBillingContactID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                            If iBillingContactID > 0 Then
                                oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                oOrderGE.SetValue("BillToID", iBillingContactID)
                                'oOrderGE.SetValue("OrderTypeID", 4)
                                oOrderGE.SetValue("PayTypeID", 1)
                            End If

                            If oOrderGE.Save(False, sError) Then
                                'oOrderGE.SetAddValue("_xDeletedOrderlines", 0)
                                lblError.Text = oOrderGE.LastUserError
                            End If
                            'If oOrderGE.Save() Then

                            'oOrderGE.SetAddValue("_xDeletedOrderlines", 1)
                            'If oOrderGE.Save() Then

                            'End If
                            'End If

                            ' Dim sError As String = String.Empty
                            sError = String.Empty
                            If oNewOrderGE.SubTypes("OrderLines").Count > 0 Then
                                oNewOrderGE.OrderType = OrderType.Quotation
                                oNewOrderGE.SetValue("PayTypeID", 1)
                                If oNewOrderGE.Save(False, sError) Then
                                    lblError.Text = oNewOrderGE.LastUserError
                                    If iMemApp > 0 Then
                                        'Dim oMemAppGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                                        'oMemAppGE = AptifyApplication.GetEntityObject("MembershipApplication__c", iMemApp)
                                        'oMemAppGE.SetValue("OrderID2", oNewOrderGE.RecordID)
                                        'oMemAppGE.Save()
                                        Dim param(1) As IDataParameter
                                        param(0) = DataAction.GetDataParameter("@iMemApp", SqlDbType.Int, iMemApp)
                                        param(1) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, CInt(oNewOrderGE.RecordID))
                                        sSQL = ""
                                        sSQL = Database & "..spUpdateMembershipApplicationOrerID__c"
                                        Dim recordupdate2 As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param, 180)
                                    End If
                                End If
                            End If
                        Else
                            oOrderGE.SetAddValue("_xDeletedOrderlines", 1)
                            Dim sError As String = String.Empty
                            Dim sSQL As String = "..spGetBillingContactID__c @CompanyID=" & Convert.ToInt32(oOrderGE.GetValue("ShipToCompanyID"))
                            Dim iBillingContactID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                            If iBillingContactID > 0 Then
                                oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                oOrderGE.SetValue("BillToID", iBillingContactID)
                                oOrderGE.SetValue("PayTypeID", 1)
                            End If

                            If oOrderGE.Save(False, sError) Then
                                'oOrderGE.SetAddValue("_xDeletedOrderlines", 0)
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
        Protected Sub cmdMakePayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMakePayment.Click
            ' Me.cmdMakePayment.Attributes.Add("onClick", "validatePage();")
            checkBillingCompany()
            ' Save a payment to the database with the information the user provided
            Dim i As Integer
            Dim orderdetails As New Dictionary(Of Integer, Decimal)
            Dim arPay() As PayInfo
            Dim arShippingPay() As PayShippingInfo
            Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
            Dim lblPay As New Label
            Dim OrderID As System.Web.UI.WebControls.HyperLink
            Dim OrderLineID As Label, OrderLineNumber As Label, IsDonation As Label
            Dim txtPay As New TextBox
            Dim dtorder As DataTable = ViewState("orderdt")
            'If ViewState("CHECKED_ITEMS") IsNot Nothing Then
            '    orderdetails = DirectCast(ViewState("CHECKED_ITEMS"), Dictionary(Of Integer, Decimal))
            'End If

            Try
                ReDim arPay(0)
                ReDim arShippingPay(0)
                If Page.IsValid Then
                    sTrans = DataAction.BeginTransaction(IsolationLevel.ReadCommitted, True)
                    If rdbYes.Checked Then
                        Dim m_oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                        m_oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), Aptify.Applications.OrderEntry.OrdersEntity)
                        With m_oOrder
                            .SetValue("ShipToID", User1.PersonID)
                            .SetValue("BillToID", User1.PersonID)
                            .SetValue("BillToCompanyID", User1.CompanyID)
                            .FlowdownCustomerAddresses(Aptify.Applications.OrderEntry.OrdersEntity.AddressFlowDownTypes.Both)
                            .SetValue("EmployeeID", System.Configuration.ConfigurationManager.AppSettings("AptifyEBusinessWebEmployeeID"))
                            .AddProduct(BenevolentDonationProductID)
                            With m_oOrder.SubTypes("OrderLines")(0)
                                .SetValue("UserPricingOverride", True)
                                .SetValue("Price", CLng(txtDonation.Text))
                            End With
                            .CalculateOrderTotals()
                            .SetValue("OrderSourceID", 4)
                            .SetValue("OrderTypeID", 4)
                            .SetValue("Comments", "Benevolent Donation Product")
                            .SetValue("OrderTypeID", 4)
                            .SetValue("FirmPay__c", 1)
                            .SetValue("PayTypeID", 1)
                            Dim sError1 As String = Nothing
                            If m_oOrder.Save(False, sError1, sTrans) Then
                                ViewState("DonationOrderID") = m_oOrder.RecordID
                            Else
                                DataAction.RollbackTransaction(sTrans)
                                'lblError.Text = "An error took place while processing your payment"
                                lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AdminOrderDetailSummary.PaymentFailed")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                lblError.Visible = True
                                Exit Sub
                            End If
                        End With
                    End If


                    For i = 0 To grdOrderDetails.Items.Count - 1
                        lblPay = grdOrderDetails.Items(i).FindControl("lblBalanceAmount")
                        txtPay = grdOrderDetails.Items(i).FindControl("txtPayAmt")

                        If txtPay.Text.Trim.Length > 0 Then
                            If IsNumeric(txtPay.Text) Then
                                If CDec(txtPay.Text) > 0 Then
                                    ReDim Preserve arPay(UBound(arPay) + 1)
                                    arPay(UBound(arPay) - 1) = New PayInfo
                                    With arPay(UBound(arPay) - 1)

                                        Dim lblNumDigits As Label = grdOrderDetails.Items(i).FindControl("lblNumDigitsAfterDecimal")
                                        Dim lblBal As Label = grdOrderDetails.Items(i).FindControl("lblBalanceAmount")
                                        OrderID = grdOrderDetails.Items(i).FindControl("lblOrderNo")
                                        OrderLineID = grdOrderDetails.Items(i).FindControl("OrderLineID")
                                        OrderLineNumber = grdOrderDetails.Items(i).FindControl("OrderLineNumber")
                                        IsDonation = grdOrderDetails.Items(i).FindControl("IsDonation")
                                        .OrderID = OrderID.Text
                                        .OrderLineID = OrderLineID.Text
                                        .PayAmount = Math.Round(CDec(txtPay.Text), _
                                                                  CInt(lblNumDigits.Text))
                                        .LineNumber = OrderLineNumber.Text
                                        .Balance = Convert.ToDecimal(lblBal.Text.ToString().Replace(sSymCurr, ""))
                                        .IsDonation = CInt(IsDonation.Text.Trim)
                                    End With
                                End If
                            Else
                                ' lblError.Text = "Values entered must be valid currency quantities."
                                lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AdminOrderDetailSummary.ValidCurrencyQuantities")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                lblError.Visible = True
                                Exit Sub
                            End If
                        End If
                    Next

                    If grdShipping.Visible = True And grdShipping.Items.Count > 0 Then
                        For i = 0 To grdShipping.Items.Count - 1
                            txtPay = grdShipping.Items(i).FindControl("txtPayAmt")

                            If txtPay.Text.Trim.Length > 0 Then
                                If IsNumeric(txtPay.Text) Then
                                    If CDec(txtPay.Text) > 0 Then
                                        ReDim Preserve arShippingPay(UBound(arShippingPay) + 1)
                                        arShippingPay(UBound(arShippingPay) - 1) = New PayShippingInfo
                                        With arShippingPay(UBound(arShippingPay) - 1)
                                            Dim lblBal As Label = grdShipping.Items(i).FindControl("lblBalanceAmount")
                                            Dim lblOrderShipmentID As Label = grdShipping.Items(i).FindControl("lblOrderShipmentID")
                                            OrderID = grdOrderDetails.Items(i).FindControl("lblOrderNo")
                                            .OrderID = OrderID.Text
                                            .PayAmount = CDec(txtPay.Text)
                                            .Balance = CDec(txtPay.Text)
                                            .OrderShipmentID = CLng(lblOrderShipmentID.Text.Trim)
                                        End With
                                    End If
                                Else
                                    'lblError.Text = "Values entered must be valid currency quantities."
                                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AdminOrderDetailSummary.ValidCurrencyQuantities")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                    lblError.Visible = True
                                    Exit Sub
                                End If
                            End If
                        Next

                    End If


                    lblError.Visible = False
                    If UBound(arPay) > 0 Then
                        For i = 0 To UBound(arPay) - 1
                            With arPay(i)
                                If .PayAmount <= 0 Then
                                    'lblError.Text = "You must select one or more Orders to make a Payment"
                                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AdminOrderDetailSummary.SelectPayment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                    lblError.Visible = True
                                    Exit Sub
                                End If
                            End With
                        Next
                        'Anil B change for 10737 on 13/03/2013
                        'Set Credit Card ID to load property form Navigation Config

                        If PostPayment(arPay, sTrans, arShippingPay) Then
                            'Anil B change for 10737 on 13/03/2013
                            'Set Credit Card ID to load property form Navigation Config
                            DataAction.CommitTransaction(sTrans)
                            CreditCard.CCNumber = ""
                            CreditCard.CCExpireDate = ""
                            CreditCard.CCSecurityNumber = ""
                            LoadOrders()
                            radpaymentmsg.VisibleOnPageLoad = True
                            cmdMakePayment.Enabled = False
                            If CreditCard.BillMeLaterChecked Then
                                Session("IsBillMeLater") = 1
                            Else
                                Session("IsBillMeLater") = Nothing
                            End If
                            ViewState.Remove("orderdt")
                            ''Added By Pradip  2016-07-06 For Red-Mine Issue https://redmine.softwaredesign.ie/issues/13287
                            MyBase.Response.Redirect(AdminPaymentSummary & "?PaymentID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(ViewState("PaymentID"))), False)

                            'MyBase.Response.Redirect(AdminPaymentSummary)
                            'MyBase.Response.Redirect("/OrdersManagement/adminpaymentsummary.aspx", False)

                        Else
                            'lblError.Text = "An error took place while processing your payment"
                            lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AdminOrderDetailSummary.PaymentFailed")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            lblError.Visible = True
                        End If
                    Else
                        lblError.Visible = True
                        ' lblError.Text = "You must select one or more Orders to make a Payment"
                        lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AdminOrderDetailSummary.SelectPayment")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

                    End If
                End If

            Catch ex As Exception
                'DataAction.RollbackTransaction(sTrans)
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Function PostPayment(ByVal arPay() As PayInfo, ByVal sTrans As String, ByVal arShippingPay() As PayShippingInfo) As Boolean
            ' post the payment to the database using the CGI GE
            Dim oPayment As AptifyGenericEntityBase
            Dim i As Integer
            Dim bPaymentSaved As Boolean
            Try
                Dim iCurrencyID As Integer = Convert.ToInt32(radcurrency.SelectedValue().Trim())
                If CreditCard.BillMeLaterChecked Then
                    'If CreditCard.PaymentTypeID = CInt(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID")) Then
                    Dim m_oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                    For i = 0 To UBound(arPay) - 1
                        m_oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", arPay(i).OrderID), Aptify.Applications.OrderEntry.OrdersEntity)
                        With m_oOrder.SubTypes("OrderLines").Find("ID", CLng(arPay(i).OrderLineID))
                            .SetValue("WebRemittanceNo__c", CStr(orderUniqueID))
                            If (arPay(i).IsDonation = 1) Then
                                If (arPay(i).Balance <> arPay(i).PayAmount) Then
                                    .SetValue("UserPricingOverride", True)
                                    .SetValue("Price", CLng(arPay(i).PayAmount))
                                End If
                            End If
                        End With
                        Dim sError As String = Nothing
                        If Not m_oOrder.Save(False, sError, sTrans) Then
                            bPaymentSaved = False
                            DataAction.RollbackTransaction(sTrans)
                        End If
                    Next
                    If grdShipping.Visible = True And grdShipping.Items.Count > 0 Then
                        Dim m_oOrderShipment As AptifyGenericEntityBase
                        For i = 0 To UBound(arShippingPay) - 1
                            m_oOrderShipment = AptifyApplication.GetEntityObject("OrderShipments", CLng(arShippingPay(i).OrderShipmentID))
                            m_oOrderShipment.SetValue("WebRemittanceNo__c", CStr(orderUniqueID))
                            Dim sError As String = Nothing
                            If Not m_oOrderShipment.Save(False, sError, sTrans) Then
                                bPaymentSaved = False
                                DataAction.RollbackTransaction(sTrans)
                                Return False
                            End If
                        Next
                    End If
                    If rdbYes.Checked Then
                        m_oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", CLng(ViewState("DonationOrderID"))), Aptify.Applications.OrderEntry.OrdersEntity)
                        With m_oOrder.SubTypes("OrderLines").Find("Sequence", 1)
                            .SetValue("WebRemittanceNo__c", CStr(orderUniqueID))
                            .SetValue("BillToCompanyID__c", User1.CompanyID)
                        End With
                        Dim sError As String = Nothing
                        If Not m_oOrder.Save(False, sError, sTrans) Then
                            bPaymentSaved = False
                            DataAction.RollbackTransaction(sTrans)
                        End If
                    End If
                    Dim sPayOrderID As String = String.Empty
                    For i = 0 To UBound(arPay) - 1
                        If i = 0 Then
                            sPayOrderID = CStr(arPay(i).OrderLineID)
                        Else
                            sPayOrderID = sPayOrderID + "," + CStr(arPay(i).OrderLineID)
                        End If
                    Next
                    If rdbYes.Checked Then
                        Dim LineId As Long
                        m_oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", ViewState("DonationOrderID")), Aptify.Applications.OrderEntry.OrdersEntity)
                        With m_oOrder.SubTypes("OrderLines").Find("Sequence", 1)
                            LineId = CLng(.GetValue("ID"))
                        End With
                        DeleteBenevolentProductsfromOrders(sPayOrderID)
                        sPayOrderID = sPayOrderID + "," + CStr(LineId)
                    End If
                    Session("sPayOrderID") = sPayOrderID
                    bPaymentSaved = True
                Else
                    If Len(CreditCard.CCNumber) = 0 Or _
                       Len(CreditCard.CCExpireDate) = 0 Then
                        lblError.Text = "Credit Card Information Required"
                        lblError.Visible = True
                        Return False
                    End If

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
                    oPayment.SetValue("PaymentLevelID", User1.GetValue("GLPaymentLevelID"))
                    oPayment.SetValue("Comments", "Created through the CGI e-Business Suite")
                    oPayment.SetValue("CurrencyTypeID", iCurrencyID)
                    ' to have an automatic conversion of quotes to regular
                    ' orders, set the Convert flag to true.
                    oPayment.SetAddValue("_xConvertQuotesToRegularOrder", "1")
                    If oPayment.Fields("PaymentInformationID").EmbeddedObjectExists Then
                        Dim oOrderPayInfo As PaymentInformation = DirectCast(oPayment.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                        oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                        oOrderPayInfo.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                        'Anil B for issue 15442 on 22/04/2013
                        'Set CC Partial Number for payment
                        oOrderPayInfo.SetValue("CCPartial", CreditCard.CCPartial)
                    End If
                    Dim sPayOrderID As String = String.Empty
                    For i = 0 To UBound(arPay) - 1
                        With oPayment.SubTypes("PaymentLines").Add
                            .SetValue("AppliesTo", "Order Line")
                            .SetValue("Amount", arPay(i).PayAmount)
                            .SetValue("OrderID", arPay(i).OrderID)
                            .SetValue("OrderDetailID", arPay(i).OrderLineID)
                            .SetValue("LineNumber", arPay(i).LineNumber)
                            .SetValue("BillToCompanyID", User1.CompanyID)
                        End With

                        If i = 0 Then
                            sPayOrderID = CStr(arPay(i).OrderLineID)
                        Else
                            sPayOrderID = sPayOrderID + "," + CStr(arPay(i).OrderLineID)
                        End If

                    Next
                    If rdbYes.Checked Then
                        With oPayment.SubTypes("PaymentLines").Add
                            .SetValue("AppliesTo", "Order Line")
                            .SetValue("Amount", CDbl(txtDonation.Text))
                            .SetValue("OrderID", CLng(ViewState("DonationOrderID")))
                            .SetValue("OrderDetailID", GetOrderDetail(CLng(ViewState("DonationOrderID")), 1))
                            .SetValue("LineNumber", 1)
                            .SetValue("BillToCompanyID", User1.CompanyID)
                        End With
                    End If

                    ' For Shipping 
                    If grdShipping.Visible = True And grdShipping.Items.Count > 0 Then
                        For i = 0 To UBound(arShippingPay) - 1
                            With oPayment.SubTypes("PaymentLines").Add
                                .SetValue("AppliesTo", "Shipping")
                                .SetValue("Amount", arShippingPay(i).PayAmount)
                                .SetValue("OrderID", arShippingPay(i).OrderID)
                                .SetValue("BillToCompanyID", User1.CompanyID)
                            End With
                        Next
                    End If
                    Dim sError1 As String = ""
                    bPaymentSaved = oPayment.Save(False, sError1, sTrans)
                    If bPaymentSaved Then
                        ''Added By Pradip  2016-07-06 For Red-Mine Issue https://redmine.softwaredesign.ie/issues/13287
                        ViewState("PaymentID") = Convert.ToString(oPayment.RecordID)
                        Dim m_oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                        For i = 0 To UBound(arPay) - 1
                            m_oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", arPay(i).OrderID), Aptify.Applications.OrderEntry.OrdersEntity)
                            With m_oOrder.SubTypes("OrderLines").Find("ID", CLng(arPay(i).OrderLineID))
                                .SetValue("WebRemittanceNo__c", CStr(orderUniqueID))
                                If (arPay(i).IsDonation = 1) Then
                                    If (arPay(i).Balance <> arPay(i).PayAmount) Then
                                        .SetValue("UserPricingOverride", True)
                                        .SetValue("Price", CLng(arPay(i).PayAmount))
                                    End If
                                End If
                            End With
                            Dim sError As String = Nothing
                            If Not m_oOrder.Save(False, sError, sTrans) Then
                                bPaymentSaved = False
                                DataAction.RollbackTransaction(sTrans)
                                Return False
                            End If
                        Next
                        If grdShipping.Visible = True And grdShipping.Items.Count > 0 Then
                            Dim m_oOrderShipment As AptifyGenericEntityBase
                            For i = 0 To UBound(arShippingPay) - 1
                                m_oOrderShipment = AptifyApplication.GetEntityObject("OrderShipments", CLng(arShippingPay(i).OrderShipmentID))
                                m_oOrderShipment.SetValue("WebRemittanceNo__c", CStr(orderUniqueID))
                                Dim sError As String = Nothing
                                If Not m_oOrderShipment.Save(False, sError, sTrans) Then
                                    bPaymentSaved = False
                                    DataAction.RollbackTransaction(sTrans)
                                    Return False
                                End If
                            Next
                        End If

                        If rdbYes.Checked Then
                            Dim LineId As Long
                            m_oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", CLng(ViewState("DonationOrderID"))), Aptify.Applications.OrderEntry.OrdersEntity)
                            With m_oOrder.SubTypes("OrderLines").Find("Sequence", 1)
                                .SetValue("WebRemittanceNo__c", CStr(orderUniqueID))
                                .SetValue("BillToCompanyID__c", User1.CompanyID)
                                LineId = CLng(.GetValue("ID"))
                            End With
                            Dim sError As String = Nothing
                            If Not m_oOrder.Save(False, sError, sTrans) Then
                                bPaymentSaved = False
                                DataAction.RollbackTransaction(sTrans)
                                Return False
                            End If
                            DeleteBenevolentProductsfromOrders(sPayOrderID)
                            sPayOrderID = sPayOrderID + "," + CStr(LineId)
                        End If
                        Session("sPayOrderID") = sPayOrderID
                    Else
                        lblError.Text = ""
                        lblError.Text = sError1 + "Credit Card Verification Failed"
                        lblError.ForeColor = Drawing.Color.Red
                        lblError.Style.Add("font-size", "medium")
                        lblError.Visible = True
                        DataAction.RollbackTransaction(sTrans)
                        Return False
                    End If
                End If

                Return bPaymentSaved
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                DataAction.RollbackTransaction(sTrans)
                Return False

            End Try
        End Function

        Dim iTotleAmmount As Decimal
        Dim iTotalBalance As Decimal

        Protected Sub txtDonation_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                setTotal(Convert.ToDecimal(TotalOrderAmount), sSymCurr)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Private Sub LoadCurrency()

            Dim sSQL As String, dtcurrency As Data.DataTable

            Try
                'sSQL = " select distinct CurrencySymbol, CurrencyType from " & Database & "..vwOrders INNER JOIN " & Database & "..vwCurrencyTypes ct ON CurrencyTypeID=ct.ID   WHERE ShipToID IN (SELECT distinct ID FROM " & Database & "..vwPersons WHERE CompanyID IN ( SELECT ID FROM vwCompanies WHERE ID=" & User1.CompanyID & ")) AND Balance > 0 AND OrderStatus IN ('Taken', 'Shipped') And OrderType IN ('Regular', 'Quotation')"
                Dim sOrderStatus As String = Request.QueryString("OrderStatus")
                Dim sWhere As String = ""
                'If String.IsNullOrEmpty(sOrderStatus) = False Then
                '    sOrderStatus = sOrderStatus.ToUpper.Trim
                '    If sOrderStatus = "PARTLYPAID" Then
                '        sWhere = " AND o.Balance < o.GrandTotal "
                '    ElseIf sOrderStatus = "UNPAID" Then
                '        sWhere = " AND o.Balance = o.GrandTotal "
                '    End If
                '    Dim sDate As String = Request.QueryString("Date")
                '    If String.IsNullOrEmpty(sDate) = False AndAlso IsDate(sDate) Then
                '        sWhere = sWhere & " AND Month(o.OrderDate)=" & CType(sDate, Date).Month() & " AND Year(o.OrderDate)=" & CType(sDate, Date).Year() & " "
                '    End If
                'End If

                'sSQL = "SELECT Distinct CurrencySymbol,o.CurrencyType,o.CurrencyTypeID FROM " & _
                '          Database & "..vwOrders o " & _
                '        "INNER JOIN " & Database & "..vwCurrencyTypes ct ON o.CurrencyTypeID=ct.ID " & _
                '        " WHERE o.BillToCompanyID = " & User1.CompanyID & _
                '        " AND o.Balance > 0 AND o.OrderStatus IN ('Taken', 'Shipped','Back-Ordered') And o.OrderType IN ('Regular', 'Quotation','Back-Order') " & sWhere & ""
                sSQL = Database & "..spGetCurrencyForFirmAdminPayment__c @BillToId=" & Convert.ToInt32(User1.CompanyID)
                dtcurrency = DataAction.GetDataTable(sSQL)

                radcurrency.DataSource = dtcurrency
                radcurrency.DataBind()
                Dim sSymbol As String = "$"
                If Session("oCurrencySymbol") IsNot Nothing Then
                    sSymbol = Session("oCurrencySymbol")
                End If

                For j = 0 To dtcurrency.Rows.Count - 1
                    If dtcurrency(j)("CurrencySymbol").ToString.Trim() = sSymbol Then
                        radcurrency.SelectedIndex = j
                        Exit For
                    Else
                        radcurrency.SelectedIndex = 0
                    End If
                Next

                If dtcurrency.Rows.Count > 1 Then
                    lblfilter.Visible = True
                    radcurrency.Visible = True
                Else
                    lblfilter.Visible = False
                    radcurrency.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub CalculateTotal(ByVal currentPageTotal As Decimal)
            Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
            Dim index As Long = -1
            Dim orderdetails As New Dictionary(Of Integer, Decimal)
            Dim total As Decimal = 0.0
            Dim lblOrderID As Label = New Label
            If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                orderdetails = DirectCast(ViewState("CHECKED_ITEMS"), Dictionary(Of Integer, Decimal))
            End If
            If orderdetails IsNot Nothing Then
                Dim iTotal As Int64 = orderdetails.Count
                For i = iTotal - 1 To 0 Step -1
                    For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                        ' lblOrderID = CType(item.FindControl("ID"), Label)
                        lblOrderID = CType(item.FindControl("OrderLineID"), Label)
                        index = lblOrderID.Text
                        'Dim result As Boolean = DirectCast(item.FindControl("chkMakePayment"), CheckBox).Checked
                        If index = orderdetails.Keys(i) Then
                            orderdetails.Remove(index)
                        End If
                    Next
                Next
                For index = 0 To orderdetails.Count - 1
                    total += orderdetails.Values(index)
                Next
                total = total + currentPageTotal
                setTotal(total, sSymCurr)
                lblTotCurrSymbol.Text = sSymCurr
                lblDonCurrSymbol.Text = sSymCurr
            End If
        End Sub
        'Neha changes for Issue:14972,04/29/13
        Protected Sub grdOrderDetails_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOrderDetails.DataBound
            Dim index As Long = -1
            Dim iSumTotalPayAmmount As Decimal
            Dim TotalPayAmmount As Decimal
            Dim chkflag As Boolean = True
            Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
            For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                'lblOrderID = CType(item.FindControl("ID"), Label)
                Dim lblOrderNo As System.Web.UI.WebControls.HyperLink = DirectCast(item.FindControl("lblOrderNo"), System.Web.UI.WebControls.HyperLink)
                Dim lblMemberName As Label = DirectCast(item.FindControl("lblMemberName"), Label)
                Dim lblGrandTotal As Label = DirectCast(item.FindControl("lblGrandTotal"), Label)
                Dim lblCompanyTotal As Label = DirectCast(item.FindControl("lblCompanyTotal"), Label)
                Dim lblOrderLineID As Label = DirectCast(item.FindControl("OrderLineID"), Label)
                Dim txtPayAmt As TextBox = DirectCast(item.FindControl("txtPayAmt"), TextBox)

                Dim ordernos As New Dictionary(Of Integer, Decimal)
                Dim OrderNo As Long = CLng(lblOrderNo.Text.Trim)
                If ViewState("OrderNo") IsNot Nothing Then
                    ordernos = DirectCast(ViewState("OrderNo"), Dictionary(Of Integer, Decimal))
                    If ordernos.ContainsKey(OrderNo) = True Then
                        lblOrderNo.Visible = False
                        lblMemberName.Visible = False
                        lblGrandTotal.Visible = False
                        lblCompanyTotal.Visible = False
                        item.Item("GridDateTimeColumnOrderDate").Text = ""
                        item.Item("City").Text = ""
                    Else
                        ordernos.Add(OrderNo, OrderNo)
                        ViewState("OrderNo") = ordernos
                    End If
                Else
                    ordernos.Add(OrderNo, OrderNo)
                    ViewState("OrderNo") = ordernos
                End If

                TotalPayAmmount = getTotal(item)
                iSumTotalPayAmmount += TotalPayAmmount
                Dim orderdetails As New Dictionary(Of Integer, Decimal)
                Dim OrderLineID As Long = CLng(lblOrderLineID.Text.Trim)
                Dim lPay As Decimal = Convert.ToDecimal(txtPayAmt.Text)
                If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                    orderdetails = DirectCast(ViewState("CHECKED_ITEMS"), Dictionary(Of Integer, Decimal))
                    If orderdetails.ContainsKey(OrderLineID) = False Then
                        orderdetails.Add(OrderLineID, lPay)
                        ViewState("CHECKED_ITEMS") = orderdetails
                    End If
                Else
                    orderdetails.Add(OrderLineID, lPay)
                    ViewState("CHECKED_ITEMS") = orderdetails
                End If
            Next
            setTotal(iSumTotalPayAmmount, sSymCurr)
            CalculateTotal(iSumTotalPayAmmount)
            'SaveCheckedValues()
        End Sub

        Private Sub setTotal(ByVal iSumTotalPayAmmount As Decimal, ByVal sSymCurr As String)
            Dim iSumTotalPay As Decimal = 0

            If Not iSumTotalPayAmmount = 0 Then
                'suraj S Issue 16036 5/2/13, add comma for amount
                txtTotal.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPayAmmount)
            Else
                txtTotal.Text = sSymCurr & "0.00"
            End If
            TotalOrderAmount = iSumTotalPayAmmount
            If rdbYes.Checked = True Then
                iSumTotalPay = CDbl(iSumTotalPayAmmount) + CDbl(txtDonation.Text)
            ElseIf rdbNo.Checked = True Then
                iSumTotalPay = iSumTotalPayAmmount
            End If
            TotalPayAmount = iSumTotalPay
            If Not iSumTotalPay = 0 Then
                'suraj S Issue 16036 5/2/13, add comma for amount
                lblTotalPay.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPay)
            Else
                lblTotalPay.Text = sSymCurr & "0.00"
            End If
            ShowBillMeLater(TotalPayAmount)
        End Sub

        Private Function getTotal(ByRef row As GridDataItem) As Decimal
            Dim iSumTotalPayAmmount As Decimal
            Dim iTotalAmmount As Decimal
            Dim txtPayAmt As TextBox = DirectCast(row.FindControl("txtPayAmt"), TextBox)
            iTotalAmmount = Decimal.Parse(txtPayAmt.Text).ToString("0.00")
            iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
            Return iSumTotalPayAmmount
        End Function

        Private Sub AddExpression()
            Dim ExpOrderSort As New GridSortExpression
            ExpOrderSort.FieldName = "Name"
            ExpOrderSort.SetSortOrder("Ascending")
            grdOrderDetails.MasterTableView.SortExpressions.AddSortExpression(ExpOrderSort)
        End Sub

        Protected Sub radcurrency_selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles radcurrency.SelectedIndexChanged
            'Dim sSQL As String = "SELECT CurrencySymbol FROM " & _
            '                          Database & "..vwCurrencyTypes where ID=" & radcurrency.SelectedValue().Trim()
            Dim sSQL As String = Database & "..spGetCurrencySymbol__c @CurrencyID=" & radcurrency.SelectedValue().Trim()
            Dim dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                sCuurSymbol = dt.Rows(0)("CurrencySymbol").ToString().Trim
                ViewState("Currency") = sCuurSymbol
            End If
            ViewState.Remove("orderdt")
            LoadOrders()
            grdOrderDetails.DataBind()
            'ViewState("CHECKED_ITEMS") = Nothing
            setTotal("00.0", ViewState("Currency"))
        End Sub

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Dim orderdetails As New Dictionary(Of Integer, Decimal)
            For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                Dim lblOrderLineID As Label = DirectCast(item.FindControl("OrderLineID"), Label)
                Dim txtPayAmt As TextBox = DirectCast(item.FindControl("txtPayAmt"), TextBox)
                Dim OrderLineID As Long = CLng(lblOrderLineID.Text.Trim)
                Dim lPay As Decimal = Convert.ToDecimal(txtPayAmt.Text)
                orderdetails.Add(OrderLineID, lPay)
            Next
            Session("CHECKED_ITEMS") = orderdetails
            'Response.Redirect("~/OrdersManagement/AdminOrderDetail.aspx")
            Response.Redirect(AdminOrderDetail, False)
        End Sub

        Protected Sub rdbYes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbYes.CheckedChanged
            Try
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                setTotal(Convert.ToDecimal(TotalOrderAmount), sSymCurr)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rdbNo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbNo.CheckedChanged
            Try
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                setTotal(Convert.ToDecimal(TotalOrderAmount), sSymCurr)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#Region "Custom Functions/Subroutine"

        ''' <summary>
        ''' procedure set properties of credit card, if Company and User's credit Status is approved and credit limit is availabe 
        ''' contion check if payment type is Bill Me Later. 
        ''' </summary>
        Private Sub ShowBillMeLater(ByVal TotalAmount As Decimal)
            Dim iPOPaymentType As Integer = 0
            Try
                If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID")) Then
                    iPOPaymentType = CInt(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID"))
                End If
                Dim dr As Data.DataRow = User1.CompanyDataRow
                'CreditCard.UserCreditStatus = CInt(User1.GetValue("CreditStatusID"))
                'CreditCard.UserCreditLimit = CLng(User1.GetValue("CreditLimit"))
                If iPOPaymentType > 0 Then
                    If dr IsNot Nothing Then
                        CreditCard.CompanyCreditStatus = CInt(dr.Item("CreditStatusID"))
                        CreditCard.CompanyCreditLimit = CLng(dr.Item("CreditLimit"))
                        If CreditCard.CompanyCreditStatus = 2 Then
                            If CreditCard.CompanyCreditLimit < TotalAmount Then
                                CreditCard.DisableBillMeLater = True
                                CreditCard.CreditCheckLimit = False
                            Else
                                CreditCard.DisableBillMeLater = False
                                CreditCard.CreditCheckLimit = True
                            End If
                        End If
                    End If
                    CreditCard.ShowHideBillMeLater()
                End If
            Catch ex As Exception
            Finally
            End Try
        End Sub

        ''' <summary>
        ''' Get Order Detail ID
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <param name="OrderLine"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        Private Function GetOrderDetail(ByVal OrderID As Long, ByVal OrderLine As Long) As Long
            Try
                Dim sSQL As String
                Dim dt As DataTable
                Dim OrderDetailID As Integer
                sSQL = AptifyApplication.GetEntityBaseDatabase("Orders") & "..spGetOrderDetail__c @OrderID=" & OrderID & ",@Sequence=" & OrderLine
                dt = Me.DataAction.GetDataTable(sSQL)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        OrderDetailID = Convert.ToInt32(dr("ID"))
                    Next
                    Return OrderDetailID
                End If
            Catch ex As Exception
                Return 0
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        ''' <summary>
        ''' Get BenevolentDonation Amount from Product Attribute
        ''' </summary>
        ''' <param name="ProductID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        Private Function GetBenevolentDonationAmount(ByVal ProductID As Integer) As Double
            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
            Dim oOL As Aptify.Applications.OrderEntry.OrderLinesEntity
            Try
                'Dim sSQL As String
                Dim dBenevolentDonationAmount As Double = 100

                'Dim params(1) As System.Data.IDataParameter
                'params(0) = DataAction.GetDataParameter("@ProductId", SqlDbType.Int, ProductID)
                'params(1) = DataAction.GetDataParameter("@AttributeName", SqlDbType.VarChar, "DonationSuggestedAmt__c")

                'sSQL = Database & "..spGetProductAttributeValue__c"
                'dBenevolentDonationAmount = CDbl(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, params))
                'Return dBenevolentDonationAmount


                'Here get the Top 1 Person ID whose MemberTypeID = 1 
                oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                oOrder.ShipToID = User1.PersonID
                oOrder.BillToID = User1.PersonID
                oOrder.ShipToCompanyID = User1.CompanyID
                oOrder.BillToCompanyID = User1.CompanyID
                oOrder.AddProduct(ProductID, 1)
                If oOrder.SubTypes("OrderLines").Count > 0 Then
                    oOL = TryCast(oOrder.SubTypes("OrderLines").Item(0), OrderLinesEntity)
                    dBenevolentDonationAmount = CDbl(oOL.Price)
                End If
                Return dBenevolentDonationAmount
            Catch ex As Exception
                Return 100
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Finally
                oOL = Nothing
                oOrder = Nothing
            End Try
        End Function
#End Region

        Protected Sub grdShipping_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdShipping.DataBound
            Dim index As Long = -1
            Dim iSumTotalPayAmmount As Decimal
            Dim TotalPayAmmount As Decimal
            Dim chkflag As Boolean = True
            Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
            For Each item As GridDataItem In grdShipping.MasterTableView.Items
                'lblOrderID = CType(item.FindControl("ID"), Label)
                TotalPayAmmount = getTotal(item)
                iSumTotalPayAmmount += TotalPayAmmount
            Next
            iSumTotalPayAmmount = iSumTotalPayAmmount + CDec(Replace(txtTotal.Text.Trim, sSymCurr, ""))
            setTotal(iSumTotalPayAmmount, sSymCurr)
            CalculateTotal(iSumTotalPayAmmount)
        End Sub

        Protected Sub grdShipping_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdShipping.NeedDataSource
            LoadShippingGrid()
        End Sub

        Private Function GetWebRemittanceNumber() As String
            Dim sWebRemittance As String = "WEB"
            Dim iReturn As Integer
            Dim sSQL As String
            Try
                Dim params(0) As System.Data.IDataParameter
                params(0) = DataAction.GetDataParameter("@UserID", SqlDbType.Int, CInt(User1.UserID))

                sSQL = Database & "..spGetUniqueWebRemittance__c"
                iReturn = DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, params)
                sWebRemittance = sWebRemittance + CStr(iReturn)
                Return sWebRemittance
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return sWebRemittance = System.Guid.NewGuid.ToString
            End Try
        End Function

        'Rajesh - To delete the Donation Product from Order.
        ''' <summary>
        ''' Subroutine to fetch the OrderID and OrderLineID for Benevolent Product and delete them if are not Paid and Donation product is selected by Admin.
        ''' </summary>
        ''' <param name="sOrderLineID"></param>
        ''' <remarks></remarks>
        Private Sub DeleteBenevolentProductsfromOrders(ByVal sOrderLineID As String)
            Dim sSQL As String, dtOrder As DataTable, lOrderId As Long, lOrderLineID As Long
            Dim params(0) As System.Data.IDataParameter
            Dim m_oOrder As Aptify.Applications.OrderEntry.OrdersEntity
            params(0) = DataAction.GetDataParameter("@OrderLineID", SqlDbType.VarChar, sOrderLineID)
            sSQL = Database & "..spGetDonationOrderId__c"
            dtOrder = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
            If dtOrder IsNot Nothing Then
                If dtOrder.Rows.Count > 0 Then
                    For index = 0 To dtOrder.Rows.Count - 1
                        lOrderId = CLng(dtOrder.Rows(index)("OrderID"))
                        lOrderLineID = CLng(dtOrder.Rows(index)("OrderlineID"))

                        m_oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", lOrderId), Aptify.Applications.OrderEntry.OrdersEntity)
                        With m_oOrder.SubTypes("OrderLines").Find("ID", lOrderLineID)
                            .Delete()
                        End With
                        Dim sError As String = Nothing
                        If Not m_oOrder.Save(False, sError) Then
                            'DataAction.RollbackTransaction(sTrans)
                        End If
                    Next
                End If
            End If
        End Sub
    End Class
End Namespace
