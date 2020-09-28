'Aptify e-Business 5.5.1, July 2013
'Updated By Rajesh for Firm Potal Config

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


Namespace Aptify.Framework.Web.eBusiness.ProductCatalog

    Partial Class AdminOrderDetail__c
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
        Protected Const ATTRIBUTE_ADMIN_PAYMENT_DETAILSUMMARY As String = "AdminOrderDetailSummary" 'Added by Rajesh 
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


        'Added by Sandeep for Issue 15051 on 12/03/2013
        Public Overridable Property AdminOrderDetailSummary() As String
            Get
                If Not ViewState(ATTRIBUTE_ADMIN_PAYMENT_DETAILSUMMARY) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ADMIN_PAYMENT_DETAILSUMMARY))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ADMIN_PAYMENT_DETAILSUMMARY) = Me.FixLinkForVirtualPath(value)
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

            'Added by Rajesh
            If String.IsNullOrEmpty(AdminOrderDetailSummary) Then
                'since value is the 'default' check the XML file for possible custom setting
                AdminOrderDetailSummary = Me.GetLinkValueFromXML(ATTRIBUTE_ADMIN_PAYMENT_DETAILSUMMARY)
            End If

        End Sub

        Private Class PayInfo
            Public OrderID As Long
            Public PayAmount As Decimal
            Public Balance As Decimal
        End Class

        Private Sub LoadOrders()
            Dim dtOrder As Data.DataTable
            Dim dtProducts As DataTable
            Dim sSQL As String
            Dim sProducts As String = String.Empty
            Dim i As Integer, j As Integer
            Dim icount As Integer = 0
            Dim iProdCnt As Integer = 0
            Try
                If ViewState("orderdt") Is Nothing Then
                    'Dim ParentID As Long = -1
                    'ParentID = Me.DataAction.ExecuteScalar("Select ISNULL(ParentID, -1) as ParentID from " & Database & "..Company where ID=" & User1.CompanyID.ToString)
                    'add contion when redirecting from group admin dashboard
                    Dim sOrderStatus As String = Request.QueryString("OrderStatus")
                    Dim sWhere As String = ""

                    'Rajesh - Code change to use Stored procedure instead of Query.
                    Dim oDate As Date = Nothing

                    Dim params(3) As System.Data.IDataParameter
                    params(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, CInt(User1.CompanyID))
                    params(1) = DataAction.GetDataParameter("@CurrencyTypeID", SqlDbType.Int, CInt(radcurrency.SelectedValue().Trim()))


                    'If String.IsNullOrEmpty(sOrderStatus) = False Then
                    '    sOrderStatus = sOrderStatus.ToUpper.Trim

                    '    If sOrderStatus = "PARTLYPAID" Then
                    '        sWhere = " AND o.Balance < o.GrandTotal "
                    '    ElseIf sOrderStatus = "UNPAID" Then
                    '        sWhere = " AND o.Balance = o.GrandTotal "
                    '    End If
                    '    Dim sDate As String = Request.QueryString("Date")

                    '    If String.IsNullOrEmpty(sDate) = False AndAlso IsDate(sDate) Then
                    '        oDate = CDate(sDate)
                    '        params(2) = DataAction.GetDataParameter("@OrderDate", SqlDbType.Date, oDate)
                    '    Else
                    '        params(2) = DataAction.GetDataParameter("@OrderDate", SqlDbType.Date, Nothing)
                    '    End If
                    'Else
                    '    params(2) = DataAction.GetDataParameter("@OrderDate", SqlDbType.Date, Nothing)
                    'End If
                    'params(3) = DataAction.GetDataParameter("@OrderStatus", SqlDbType.VarChar, sOrderStatus)
                    params(2) = DataAction.GetDataParameter("@OrderDate", SqlDbType.Date, Nothing)
                    params(3) = DataAction.GetDataParameter("@OrderStatus", SqlDbType.VarChar, Nothing)
                    sSQL = Database & "..spGetOrderDetailsforAdmin__c"
                    dtOrder = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                    If dtOrder IsNot Nothing Then
                        Dim dcolUrl As DataColumn = New DataColumn()
                        dcolUrl.Caption = "OrderConfirmationURL"
                        dcolUrl.ColumnName = "OrderConfirmationURL"
                        dtOrder.Columns.Add(dcolUrl)
                        If dtOrder.Rows.Count > 0 Then
                            For Each rw As DataRow In dtOrder.Rows
                                rw("OrderConfirmationURL") = OrderConfirmationURL + "?ID=" + rw("ID").ToString
                            Next
                        End If
                        If dtOrder.Rows.Count > 0 Then
                            'sSQL = "SELECT CurrencySymbol FROM " & _
                            '    Database & "..vwCurrencyTypes where ID=" & radcurrency.SelectedValue().Trim()
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
                            grdOrderDetails.DataSource = dtOrder
                            ViewState("orderdt") = dtOrder
                            lblTotal.Visible = True
                            txtTotal.Visible = True
                            lblError.Text = ""
                            lblError.Visible = False
                            btnMove.Visible = True
                        Else
                            grdOrderDetails.Visible = (dtOrder.Rows.Count > 0)
                            btnMove.Visible = (dtOrder.Rows.Count > 0)
                            lblTotal.Visible = False
                            txtTotal.Visible = False
                            lblError.Text = "No open invoices available for my company."
                            lblError.Visible = True
                            'Anil B change for 10737 on 13/03/2013
                            'Set Credit Card ID to load property form Navigation Config
                            'radcurrency.Visible = False
                            'lblfilter.Visible = False
                            'payoffdiv.Visible = False
                            'lblrecmsg.Visible = True
                            btnMove.Visible = False
                        End If
                        grdOrderDetails.Visible = (dtOrder.Rows.Count > 0)
                        ' btnMove.Visible = (dtOrder.Rows.Count > 0)
                        spnNote.Visible = (dtOrder.Rows.Count > 0)
                    Else
                        'grdOrderDetails.DataSource = Nothing
                        lblError.Text = "No open invoices available for my company."
                        lblError.Visible = True
                        grdOrderDetails.Visible = False
                        btnMove.Visible = False
                        spnNote.Visible = False
                    End If
                Else
                    grdOrderDetails.DataSource = ViewState("orderdt")
                End If
                ViewState("OrderNo") = Nothing
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdOrderDetails_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdOrderDetails.PageIndexChanged
            SaveCheckedValues()
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
                    ' Rajesh - If Session had values code to select those Orderline.
                    If Session("CHECKED_ITEMS") IsNot Nothing Then
                        Dim orderdetails As New Dictionary(Of Integer, Decimal)
                        orderdetails = DirectCast(Session("CHECKED_ITEMS"), Dictionary(Of Integer, Decimal))
                        ViewState("CHECKED_ITEMS") = orderdetails
                        Session("CHECKED_ITEMS") = Nothing
                    End If
                    LoadCurrency()
                    ViewState.Remove("orderdt")
                    AddExpression()
                    lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BenevolentDonationNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
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

        Protected Sub chkMakePayment_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            'Dim flag As Boolean = False
            Dim chk1 As CheckBox = CType(sender, CheckBox)
            'check header value for select all functionality
            Dim header As GridHeaderItem = TryCast(grdOrderDetails.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
            Dim chkAllMakePayment As CheckBox = DirectCast(header.FindControl("chkAllMakePayment"), CheckBox)
            Dim grow As GridDataItem = DirectCast(chk1.NamingContainer, GridDataItem)
            Dim i As Integer = grow.DataSetIndex Mod grdOrderDetails.PageSize
            Dim txtPaymentaMT As TextBox = (DirectCast(grdOrderDetails.Items(i).FindControl("txtPayAmt"), TextBox))
            Dim sSymCurrancy As String = ViewState("sCuurSymbol").ToString().Trim()
            Dim IsDonation As Integer = CInt(DirectCast(grdOrderDetails.Items(i).FindControl("IsDonation"), Label).Text)
            Dim OrderStatus As String = DirectCast(grdOrderDetails.Items(i).FindControl("OrderStatus"), Label).Text.Trim
            If chk1.Checked = True Then
                Dim textBoxText As String = (DirectCast(grdOrderDetails.Items(i).FindControl("lblBalanceAmount"), Label)).Text
                'txtPaymentaMT.Text = textBoxText.ToString().Replace(sSymCurrancy, "")
                txtPaymentaMT.Focus()
                If IsDonation = 1 And OrderStatus.ToLower <> "shipped" Then
                    txtPaymentaMT.Enabled = True
                End If
            Else
                txtPaymentaMT.Enabled = False
                'txtPaymentaMT.Text = "0.00"
            End If
            Dim iSumTotalPayAmmount As Decimal
            Dim TotalPayAmmount As Decimal
            Dim chkflag As Boolean = True
            For Each row As GridDataItem In grdOrderDetails.Items
                chk1 = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                If chk1.Checked = True Then
                    Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                    TotalPayAmmount = getTotal(row)
                    iSumTotalPayAmmount += TotalPayAmmount
                    setTotal(iSumTotalPayAmmount, sSymCurr)
                Else
                    chkflag = False
                    If chkAllMakePayment.Checked = True Then
                        chkAllMakePayment.Checked = False
                    End If
                End If
            Next

            CalculateTotal(iSumTotalPayAmmount)
            'check flag for selce all checked or not
            If chkflag Then
                chkAllMakePayment.Checked = True
            End If
            For Each row As GridDataItem In grdOrderDetails.Items
                chk1 = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                SaveCheckedValues()
            Next
        End Sub

        Dim iTotleAmmount As Decimal
        Dim iTotalBalance As Decimal

        Protected Sub grdOrderDetails_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdOrderDetails.ItemDataBound
            Dim sFormDec As String
            Dim ImgFlag As New Image
            'Suraj issue 14877 2/27/13 date time filtering
            Dim dateColumns As New List(Of String)
            Try
                'Add datecolumn uniqueName in list for Date format
                dateColumns.Add("GridDateTimeColumnOrderDate")
                CommonMethods.FormatedDateOnGrid(dateColumns, e.Item)
                If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
                    'If DataBinder.Eval(e.Item.DataItem, "CompanyAdministratorComments").Equals(System.DBNull.Value) = False AndAlso String.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "CompanyAdministratorComments").ToString().Trim) = False Then
                    If String.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "LineComment").ToString.Trim) = False Then
                        ImgFlag = CType(e.Item.FindControl("ImgFlag"), Image)
                        If ImgFlag IsNot Nothing Then
                            If String.IsNullOrEmpty(PaymentNotificationImage) = False Then
                                ImgFlag.ImageUrl = PaymentNotificationImage
                                ImgFlag.Visible = True
                            End If
                        End If
                    End If
                End If

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

        Protected Sub txtPayAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim flag As Boolean = False
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                Dim txtPaymentaMT As TextBox = CType(sender, TextBox)

                Dim header As GridHeaderItem = TryCast(grdOrderDetails.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
                Dim chkAllMakePayment As CheckBox = DirectCast(header.FindControl("chkAllMakePayment"), CheckBox)
                Dim grow As GridDataItem = DirectCast(txtPaymentaMT.NamingContainer, GridDataItem)
                'Dim i As Integer = grow.DataSetIndex
                Dim i As Integer = grow.DataSetIndex Mod grdOrderDetails.PageSize
                Dim chk1 As CheckBox = (DirectCast(grdOrderDetails.Items(i).FindControl("chkMakePayment"), CheckBox))
                If Not txtPaymentaMT.Text = "" Then
                    'check for payment value is negative or less than 0
                    If Convert.ToDecimal(txtPaymentaMT.Text) = 0 Or Convert.ToDecimal(txtPaymentaMT.Text) < 0 Then
                        chk1.Checked = False
                        txtPaymentaMT.Text = "0.00"
                    ElseIf Convert.ToDecimal(txtPaymentaMT.Text) > 0 Then
                        chk1.Checked = True
                    End If
                Else
                    chk1.Checked = False
                    txtPaymentaMT.Text = "0.00"
                End If

                Dim iSumTotalPayAmmount As Decimal
                Dim TotalPayAmmount As Decimal
                Dim chkflag As Boolean = True
                For Each row As GridDataItem In grdOrderDetails.Items
                    chk1 = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                    If chk1.Checked = True Then
                        sSymCurr = ViewState("sCuurSymbol").ToString().Trim()
                        TotalPayAmmount = getTotal(row)
                        iSumTotalPayAmmount += TotalPayAmmount
                        setTotal(iSumTotalPayAmmount, sSymCurr)
                    Else
                        chkflag = False
                        If chkAllMakePayment.Checked = True Then
                            chkAllMakePayment.Checked = False
                        End If
                    End If
                Next
                CalculateTotal(iSumTotalPayAmmount)
                If chkflag Then
                    chkAllMakePayment.Checked = True
                End If
                For Each row As GridDataItem In grdOrderDetails.Items
                    chk1 = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                    If chk1.Checked = True Then
                        flag = True
                    End If
                Next
                SaveCheckedValues()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub btnok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
            radpaymentmsg.VisibleOnPageLoad = False
        End Sub

        Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim headerCheckBox As CheckBox = CType(sender, CheckBox)
                If Not headerCheckBox.Checked Then
                    For Each dataItem As GridDataItem In grdOrderDetails.MasterTableView.Items
                        CType(dataItem.FindControl("chkMakePayment"), CheckBox).Checked = headerCheckBox.Checked
                        dataItem.Selected = headerCheckBox.Checked
                    Next
                End If
                Dim txtPayAmt As TextBox
                Dim iSumTotalPayAmmount As Decimal
                Dim txtPayAmmount As String
                Dim sTotleAmmount As String
                Dim iTotalAmmount As Decimal
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                For Each row As GridDataItem In grdOrderDetails.Items
                    Dim chk1 As CheckBox = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                    Dim IsDonation As Integer = CInt(DirectCast(row.FindControl("IsDonation"), Label).Text)
                    Dim OrderStatus As String = DirectCast(row.FindControl("OrderStatus"), Label).Text.Trim
                    txtPayAmt = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                    If chk1.Checked = True Then
                        Dim textBoxText As String = (DirectCast(row.FindControl("txtPayAmt"), TextBox)).Text
                        txtPayAmt.Text = textBoxText.ToString().Replace(sSymCurr, "")
                        txtPayAmmount = txtPayAmt.Text
                        sTotleAmmount = txtPayAmmount.ToString().Replace(sSymCurr, "")
                        iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")
                        iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
                        'suraj S Issue 16036 5/2/13, add comma for amount
                        txtTotal.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPayAmmount)
                        If IsDonation = 1 And OrderStatus.ToLower <> "shipped" Then
                            txtPayAmt.Enabled = True
                        End If
                    Else
                        If headerCheckBox.Checked Then
                            CType(row.FindControl("chkMakePayment"), CheckBox).Checked = headerCheckBox.Checked
                            row.Selected = headerCheckBox.Checked
                            'Dim textBoxText As String = (DirectCast(row.FindControl("lblBalanceAmount"), Label)).Text
                            'txtPayAmt.Text = textBoxText.ToString().Replace(sSymCurr, "")
                            txtPayAmmount = txtPayAmt.Text
                            sTotleAmmount = txtPayAmmount.ToString().Replace(sSymCurr, "")
                            iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")
                            iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
                            'suraj S Issue 16036 5/2/13, add comma for amount
                            txtTotal.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPayAmmount)
                            If IsDonation = 1 And OrderStatus.ToLower <> "shipped" Then
                                txtPayAmt.Enabled = True
                            End If
                        Else
                            txtPayAmt = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                            'txtPayAmt.Text = "0.00"
                            txtPayAmt.Enabled = False
                        End If
                    End If
                Next
                SaveCheckedValues()
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
                If String.IsNullOrEmpty(sOrderStatus) = False Then
                    sOrderStatus = sOrderStatus.ToUpper.Trim
                    If sOrderStatus = "PARTLYPAID" Then
                        sWhere = " AND o.Balance < o.GrandTotal "
                    ElseIf sOrderStatus = "UNPAID" Then
                        sWhere = " AND o.Balance = o.GrandTotal "
                    End If
                    Dim sDate As String = Request.QueryString("Date")
                    If String.IsNullOrEmpty(sDate) = False AndAlso IsDate(sDate) Then
                        sWhere = sWhere & " AND Month(o.OrderDate)=" & CType(sDate, Date).Month() & " AND Year(o.OrderDate)=" & CType(sDate, Date).Year() & " "
                    End If
                End If

                sSQL = "SELECT Distinct CurrencySymbol,o.CurrencyType,o.CurrencyTypeID FROM " & _
                          Database & "..vwOrders o " & _
                        "INNER JOIN " & Database & "..vwCurrencyTypes ct ON o.CurrencyTypeID=ct.ID " & _
                        " WHERE o.BillToCompanyID = " & User1.CompanyID & _
                        " AND o.Balance > 0 AND o.OrderStatus IN ('Taken', 'Shipped','Back-Ordered') And o.OrderType IN ('Regular', 'Quotation','Back-Order') " & sWhere & ""

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

        Protected Sub grdOrderDetails_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdOrderDetails.ItemCommand
            Dim sCommand As String
            Dim lOrderID As Long, lOrderDetails As String, lOrderLineID As Long
            sCommand = Convert.ToString(e.CommandName)

            If sCommand = "ADDEditComments" Then
                lOrderDetails = CStr(e.CommandArgument)
                lOrderID = Convert.ToInt64(lOrderDetails.Split("_")(0))
                lOrderLineID = CLng(lOrderDetails.Split("_")(1))
                'lOrderID = Convert.ToInt64(e.CommandArgument)
                If lOrderID > 0 Then
                    BindComments(lOrderLineID)
                    'lblOrderID.Text = Convert.ToString(lOrderID)
                    lblOrderID.Text = Convert.ToString(lOrderDetails)
                    Hidden.Value = "true"
                    radGAReviewComments.VisibleOnPageLoad = True
                End If
            End If
        End Sub

        Protected Sub BindComments(ByVal lOrderID As Long)
            Try
                Dim sSQL As String
                Dim sGAReviewComments As String

                'sSQL = "SELECT CompanyAdministratorComments FROM " & Database & "..vwOrders O where ID=" & lOrderID
                sSQL = "SELECT Comments FROM " & Database & "..vwOrderDetails O where ID=" & lOrderID

                sGAReviewComments = Convert.ToString(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If Not String.IsNullOrEmpty(sGAReviewComments) Then
                    txtGAReviewComments.Text = sGAReviewComments
                Else
                    txtGAReviewComments.Text = ""
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
            Dim lOrderID As Long, lOrderLineID As Long
            Dim oOrderGE As AptifyGenericEntityBase
            Dim lPersonID As Long, lOrderDetails As String, oLine As String
            radGAReviewComments.VisibleOnPageLoad = False
            lOrderDetails = lblOrderID.Text
            oLine = CStr(lOrderDetails.Split("_")(1))
            lOrderID = Convert.ToInt32(lOrderDetails.Split("_")(0))
            lOrderLineID = Convert.ToInt32(lOrderDetails.Split("_")(1))
            oOrderGE = AptifyApplication.GetEntityObject("Orders", lOrderID)

            'Changes by Rajesh
            Dim sCommentB As New StringBuilder, sComment As String = ""
            If oOrderGE IsNot Nothing Then
                With oOrderGE
                    With oOrderGE.SubTypes("OrderLines").Find("ID", lOrderLineID)
                        If txtGAReviewComments.Text.Trim <> "" Then
                            If String.IsNullOrEmpty(.GetValue("Comments")) Then
                                sComment = "Payment Comment - " & txtGAReviewComments.Text.Trim
                            Else
                                sComment = txtGAReviewComments.Text.Trim
                            End If
                        End If
                        .SetValue("Comments", sComment)
                    End With

                    'lPersonID = Convert.ToInt64(.GetValue("BillToID"))
                    'sComment = CStr(.GetValue("CompanyAdministratorComments"))
                    'sCommentB.Append(sComment)
                    'sCommentB.Append(Environment.NewLine)
                    ' sCommentB.Append(oLine & " - " & txtGAReviewComments.Text.Trim)
                    '.SetValue("CompanyAdministrator", User1.PersonID)
                    '.SetValue("CompanyAdministratorComments", sCommentB.ToString)
                End With

                If oOrderGE.Save Then
                    With oOrderGE
                        With oOrderGE.SubTypes("OrderLines").Find("ID", lOrderLineID)
                            If (String.IsNullOrEmpty(.GetValue("Comments").ToString()) = False) Then
                                'Turn on Flag
                                EnableNotificationFlag(lOrderLineID)
                                'Send Email
                                SendTaskMail(lPersonID, lOrderID)
                            Else
                                'Turn on Flag
                                DisableNotificationFlag(lOrderLineID)
                            End If
                        End With
                    End With

                    'Rajesh Commented for Firm Portal Config
                    ''If String.IsNullOrEmpty(oOrderGE.GetValue("CompanyAdministratorComments").ToString()) = False Then
                    ''        'Turn on Flag
                    ''        EnableNotificationFlag(lOrderLineID)
                    ''        'Send Email
                    ''        SendTaskMail(lPersonID, lOrderID)
                    ''    Else
                    ''        'Turn on Flag
                    ''        DisableNotificationFlag(lOrderLineID)
                    ''    End If
                    ''End If
                End If
            End If
            Hidden.Value = "true"
            radGAReviewComments.VisibleOnPageLoad = False
        End Sub
        'Neha changes for Issue:14972,04/29/13
        Private Sub EnableNotificationFlag(ByVal lOrderID As Long)
            Dim index As Long = -1
            Dim lblOrderID As Label = New Label
            For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                ' Rajesh -  Notoficaiton from OrderLine
                lblOrderID = CType(item.FindControl("OrderLineID"), Label)
                index = lblOrderID.Text
                If index = lOrderID Then
                    Dim ImgFlag As Image = DirectCast(item.FindControl("ImgFlag"), Image)
                    ImgFlag.ImageUrl = PaymentNotificationImage
                    ImgFlag.Visible = True
                    Exit Sub
                End If
            Next
        End Sub
        'Neha changes for Issue:14972,04/29/13
        Private Sub DisableNotificationFlag(ByVal lOrderID As Long)
            Dim index As Long = -1
            Dim lblOrderID As Label = New Label
            For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                ' Rajesh -  Notoficaiton from OrderLine
                lblOrderID = CType(item.FindControl("OrderLineID"), Label)
                index = lblOrderID.Text
                If index = lOrderID Then
                    Dim ImgFlag As Image = DirectCast(item.FindControl("ImgFlag"), Image)
                    ImgFlag.ImageUrl = PaymentNotificationImage
                    If ImgFlag.Visible = True Then
                        ImgFlag.Visible = False
                        Exit Sub
                    End If

                End If
            Next
        End Sub

        Protected Sub SendTaskMail(ByVal lPersonID As Long, ByVal oOrderID As Long)
            Try
                Dim lProcessFlowID As Long
                Dim sProcessFlow As String = "Notification to Review Order Comments"
                'Get the Process Flow ID to be used for sending the Downloadable Order Confirmation Email
                Dim sSQL As String = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='" & sProcessFlow & "'"
                Dim oProcessFlowID As Object = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache)
                If oProcessFlowID IsNot Nothing AndAlso IsNumeric(oProcessFlowID) Then
                    lProcessFlowID = CLng(oProcessFlowID)
                    Dim context As New AptifyContext
                    context.Properties.AddProperty("OrderID", oOrderID)
                    context.Properties.AddProperty("AssignedByID", EBusinessGlobal.WebEmployeeID(Page.Application))
                    sSQL = "Select Top 1 Email1 from " & Database & "..vwEmployees where ID = " & Convert.ToInt64(AssignedTOID)
                    Dim sAssignedTOEmailID As String = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache)
                    If String.IsNullOrEmpty(sAssignedTOEmailID) = True Then
                        ExceptionManagement.ExceptionManager.Publish(New Exception("Process flow Failed to send Notification. EmailID does not exists for AssignedTO Employee."))
                    End If
                    context.Properties.AddProperty("AssignedToID", Convert.ToInt64(AssignedTOID))
                    context.Properties.AddProperty("AssignedToEmailID", sAssignedTOEmailID)
                    context.Properties.AddProperty("TaskStatus", TaskStatus)
                    context.Properties.AddProperty("TaskPriority", TaskPriority)
                    context.Properties.AddProperty("TaskTypeID", TaskType)
                    context.Properties.AddProperty("TaskDescription", TaskDescription)
                    Dim oResult As ProcessFlowResult
                    oResult = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
                    If Not oResult.IsSuccess Then
                        ExceptionManagement.ExceptionManager.Publish(New Exception("Process flow Failed to send Notification. Please refer event handler for more details."))
                    End If
                Else
                    ExceptionManagement.ExceptionManager.Publish(New Exception("Message Template to send remove company Linkage Email is not found in the system."))
                End If

            Catch ex As ArgumentException
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
            radGAReviewComments.VisibleOnPageLoad = False
        End Sub


        Protected Sub grdOrderDetails_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdOrderDetails.ItemCreated
            Dim chkMakePayment As CheckBox = DirectCast(e.Item.FindControl("chkMakePayment"), CheckBox)
            Dim lblOrderNo As System.Web.UI.WebControls.HyperLink = DirectCast(e.Item.FindControl("lblOrderNo"), System.Web.UI.WebControls.HyperLink)
            Dim txtPayAmt As TextBox = DirectCast(e.Item.FindControl("txtPayAmt"), TextBox)

            Dim lblMemberName As Label = DirectCast(e.Item.FindControl("lblMemberName"), Label)
            'Dim lblMemberName As Label = DirectCast(e.Item.FindControl("lblMemberName"), Label)


            If chkMakePayment IsNot Nothing Then
                'Check in the ViewState
                If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                    Dim orderdetails As Dictionary(Of Integer, Decimal) = DirectCast(ViewState("CHECKED_ITEMS"), Dictionary(Of Integer, Decimal))

                    Dim dataItem As DataRowView = DirectCast(e.Item.DataItem, System.Data.DataRowView)

                    If dataItem IsNot Nothing Then
                        ' Rajesh -  Take OrderLineID instead of OrderID
                        'Dim OrderNo As Long = dataItem("ID")
                        Dim OrderNo As Long = dataItem("OrderLineID")
                        If orderdetails.ContainsKey(OrderNo) = True Then
                            chkMakePayment.Checked = True
                            txtPayAmt.Text = orderdetails(OrderNo)

                        Else
                            chkMakePayment.Checked = False
                        End If
                    End If
                End If
            End If

        End Sub
        'Neha changes for Issue:14972,04/29/13
        Private Sub SaveCheckedValues()
            'Dim orderdetails As New ArrayList
            Dim orderdetails As New Dictionary(Of Integer, Decimal)
            Dim total As Decimal = 0
            Dim index As Long = -1
            Dim lTotAmt As Decimal = 0.0
            Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
            Dim lblOrderID As Label = New Label
            For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                ' Rajesh -  Take OrderLineID instead of OrderID
                'lblOrderID = CType(item.FindControl("ID"), Label)
                lblOrderID = CType(item.FindControl("OrderLineID"), Label)
                index = lblOrderID.Text
                Dim result As Boolean = DirectCast(item.FindControl("chkMakePayment"), CheckBox).Checked
                Dim txtPayAmt As TextBox = DirectCast(item.FindControl("txtPayAmt"), TextBox)

                'Check in the ViewState
                If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                    orderdetails = DirectCast(ViewState("CHECKED_ITEMS"), Dictionary(Of Integer, Decimal))
                End If

                'If result Then 'if checkbox is checked.
                'remove any existing entry
                If orderdetails.ContainsKey(index) Then
                    orderdetails.Remove(index)
                End If
                If result Then
                    Dim lPay As Decimal = Convert.ToDecimal(txtPayAmt.Text)
                    orderdetails.Add(index, lPay)
                End If
            Next
            For i = 0 To orderdetails.Count - 1
                total += orderdetails.Values(i)
            Next
            setTotal(total, sSymCurr)
            If orderdetails IsNot Nothing AndAlso orderdetails.Count > 0 Then
                ViewState("CHECKED_ITEMS") = orderdetails
            End If
        End Sub
        'There would be another method to loop through the current grid object and get currentPageTotal Value
        ' This method will be called from two places. One from checkbox and one from textbox lost focus event
        ' This method will call below method at last statement

        '#3 step
        'Optimization 
        ' Showing busy indicator
        'Neha changes for Issue:14972,04/29/13
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
                        ' Rajesh -  Take OrderLineID instead of OrderID
                        ' lblOrderID = CType(item.FindControl("ID"), Label)
                        lblOrderID = CType(item.FindControl("OrderLineID"), Label)
                        index = lblOrderID.Text
                        Dim result As Boolean = DirectCast(item.FindControl("chkMakePayment"), CheckBox).Checked
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
            End If
        End Sub
        'Neha changes for Issue:14972,04/29/13
        Protected Sub grdOrderDetails_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOrderDetails.DataBound
            Dim index As Long = -1
            Dim flag As Boolean = True
            'Rajesh - Added for Issue
            If grdOrderDetails.MasterTableView.Items.Count > 0 Then
                Dim header As GridHeaderItem = TryCast(grdOrderDetails.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
                Dim chkAllMakePayment As CheckBox = DirectCast(header.FindControl("chkAllMakePayment"), CheckBox)
                Dim lblOrderID As Label = New Label
                Dim IsDonation As Label = New Label
                Dim OrderStatus As Label = New Label
                For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                    ' Rajesh -  Take OrderLineID instead of OrderID
                    'lblOrderID = CType(item.FindControl("ID"), Label)
                    lblOrderID = CType(item.FindControl("OrderLineID"), Label)
                    IsDonation = CType(item.FindControl("IsDonation"), Label)
                    OrderStatus = CType(item.FindControl("OrderStatus"), Label)
                    index = lblOrderID.Text
                    Dim result As Boolean = DirectCast(item.FindControl("chkMakePayment"), CheckBox).Checked
                    If Not result Then
                        flag = False
                    Else
                        If (CInt(IsDonation.Text) = 1) And OrderStatus.Text.Trim.ToLower <> "shipped" Then
                            DirectCast(item.FindControl("txtPayAmt"), TextBox).Enabled = True
                        End If
                    End If
                Next


                If flag = False Then
                    chkAllMakePayment.Checked = False
                Else
                    If grdOrderDetails.Items.Count <= 0 Then
                        chkAllMakePayment.Checked = False
                    Else
                        chkAllMakePayment.Checked = True
                    End If

                End If

                For Each item As GridDataItem In grdOrderDetails.MasterTableView.Items
                    ' Rajesh -  Take OrderLineID instead of OrderID
                    'lblOrderID = CType(item.FindControl("ID"), Label)
                    Dim lblOrderNo As System.Web.UI.WebControls.HyperLink = DirectCast(item.FindControl("lblOrderNo"), System.Web.UI.WebControls.HyperLink)
                    Dim lblMemberName As Label = DirectCast(item.FindControl("lblMemberName"), Label)
                    Dim lblGrandTotal As Label = DirectCast(item.FindControl("lblGrandTotal"), Label)
                    Dim lblCompanyTotal As Label = DirectCast(item.FindControl("lblCompanyTotal"), Label)

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
                Next
                SaveCheckedValues()
            End If
        End Sub

        Private Sub setTotal(ByVal iSumTotalPayAmmount As Decimal, ByVal sSymCurr As String)

            If Not iSumTotalPayAmmount = 0 Then
                'suraj S Issue 16036 5/2/13, add comma for amount
                txtTotal.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPayAmmount)
            Else
                txtTotal.Text = sSymCurr & "0.00"
            End If
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
            Dim sSQL As String = "SELECT CurrencySymbol FROM " & _
                                      Database & "..vwCurrencyTypes where ID=" & radcurrency.SelectedValue().Trim()
            Dim dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                sCuurSymbol = dt.Rows(0)("CurrencySymbol").ToString().Trim
                ViewState("Currency") = sCuurSymbol
            End If
            ViewState.Remove("orderdt")
            LoadOrders()
            grdOrderDetails.DataBind()
            ViewState("CHECKED_ITEMS") = Nothing
            setTotal("00.0", ViewState("Currency"))
        End Sub

        Protected Sub btnMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMove.Click
            Dim orderdetails As New Dictionary(Of Integer, Decimal)
            Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
            Dim newDataTable As DataTable
            Dim dtorder As DataTable = ViewState("orderdt")
            If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                orderdetails = DirectCast(ViewState("CHECKED_ITEMS"), Dictionary(Of Integer, Decimal))
            Else
                lblError.Text = "You must select one or more orders to make a payment"
                lblError.Visible = True
                Exit Sub
            End If
            If orderdetails.Count = 0 Then
                lblError.Text = "You must select one or more orders to make a payment"
                lblError.Visible = True
                Exit Sub
            End If
            newDataTable = GetSelectedOrderDataTable(orderdetails)
            'Dim x As Integer
            'For Each pair In orderdetails
            '    For x = 0 To newDataTable.Rows.Count - 1
            '        If newDataTable.Rows(x)("OrderLineID") = pair.Key Then
            '            newDataTable.Rows(x)("PayAmount") = pair.Value
            '        End If
            '    Next
            'Next
            Session("oCurrencySymbol") = ViewState("sCuurSymbol").ToString().Trim()
            Session("orderdt") = newDataTable
            'Response.Redirect("~/OrdersManagement/AdminOrderDetailSummary.aspx")
            Response.Redirect(AdminOrderDetailSummary)
        End Sub

        ''' <summary>
        ''' Function which returns Datatable of selected OrderLine Data for Payment.
        ''' </summary>
        ''' <param name="orderdetails"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSelectedOrderDataTable(ByVal orderdetails As Dictionary(Of Integer, Decimal)) As DataTable
            Dim dtorder As DataTable = ViewState("orderdt")
            Dim newDataTable As DataTable = dtorder.Clone
            Dim pair As KeyValuePair(Of Integer, Decimal), sOrderLineID As String, selectFilter As String, sbOrderLineID As New StringBuilder
            For Each pair In orderdetails
                sbOrderLineID.Append(pair.Key)
                sbOrderLineID.Append(",")
            Next
            'sOrderLineID = sbOrderLineID.ToString.Remove(sbOrderLineID.ToString.Length - 1)
            'selectFilter = " OrderLineID IN (" & sOrderLineID & ")"
            'Dim dataRows As DataRow() = dtorder.Select(selectFilter)
            'Dim typeDataRow As DataRow
            'For Each typeDataRow In dataRows
            '    newDataTable.ImportRow(typeDataRow)
            'Next

            sOrderLineID = sbOrderLineID.ToString.Remove(sbOrderLineID.ToString.Length - 1)
            selectFilter = " OrderLineID IN (" & sOrderLineID & ")"
            Dim dv As DataView = New DataView(dtorder)
            dv.RowFilter = selectFilter
            newDataTable = dv.ToTable
            Return newDataTable
        End Function
    End Class
End Namespace
