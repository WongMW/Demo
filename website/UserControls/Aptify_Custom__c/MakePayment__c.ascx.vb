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
    Partial Class MakePaymentControl__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_ORDER_CONFIRMATION_PAGE As String = "OrderConfirmationURL"
        Protected Const ATTRIBUTE_PAYMENT_SUMMARY_PAGE As String = "PaymentSummaryPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "MakePayment__c"
        Protected Const ATTRIBUTE_Abatement_PAGE As String = "AbatementPage__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const DONATION_PAGE As String = "DonatePage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage" 'added for redmine #20283
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
        ''' Payment Summary Page
        ''' </summary>
        Public Overridable Property PaymentSummaryPage() As String
            Get
                If Not ViewState(ATTRIBUTE_PAYMENT_SUMMARY_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PAYMENT_SUMMARY_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PAYMENT_SUMMARY_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' Abatment page url
        ''' </summary>
        Public Overridable Property AbatementPageURL() As String
            Get
                If Not ViewState(ATTRIBUTE_Abatement_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_Abatement_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_Abatement_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property DonatePage() As String
            Get
                If Not ViewState(DONATION_PAGE) Is Nothing Then
                    Return CStr(ViewState(DONATION_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(DONATION_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''Added BY Pradip 2016-10-07
        Public Overridable Property LoginPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' Method added for Redmine #20283
        ''' </summary>
        ''' <returns></returns>
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
            'Added as part #20995
            Dim _ScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            _ScriptManager.AsyncPostBackTimeout = "2000" ' Updated as part of 20995
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1))
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoStore()
            'set control properties from XML file if needed
            SetProperties()
            If User1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            End If
            Try
                If Not IsPostBack Then
                    ' LoadCategory()
                    If Session("CHECKED_PAYMENTS") IsNot Nothing Then
                        Dim orderdetails As New Dictionary(Of Integer, Decimal)
                        orderdetails = DirectCast(Session("CHECKED_PAYMENTS"), Dictionary(Of Integer, Decimal))
                        ViewState("CHECKED_PAYMENTS") = orderdetails
                        Session("CHECKED_PAYMENTS") = Nothing
                    End If
                    Session.Remove("orderdt")
                    subDisplayAbatementPage()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

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
                Else
                    'Commented by GM for Redmine #20283
                    ' Dim hlink As Telerik.Web.UI.GridHyperLinkColumn = CType(grdMain.Columns(1), Telerik.Web.UI.GridHyperLinkColumn)
                    ' hlink.DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"


                    'DirectCast(grdMain.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
                End If
            Else
                'Commented by GM for Redmine #20283
                ' Dim hlink As Telerik.Web.UI.GridHyperLinkColumn = CType(grdMain.Columns(1), Telerik.Web.UI.GridHyperLinkColumn)
                ' hlink.DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
                'DirectCast(grdMain.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = Me.OrderConfirmationURL & "?ID={0}"
            End If
            If String.IsNullOrEmpty(PaymentSummaryPage) Then
                PaymentSummaryPage = Me.GetLinkValueFromXML(ATTRIBUTE_PAYMENT_SUMMARY_PAGE)
                If String.IsNullOrEmpty(PaymentSummaryPage) Then
                    Me.btnNext.ToolTip = "Payment Summary URL property has not been set."
                End If
            End If
            If String.IsNullOrEmpty(AbatementPageURL) Then
                AbatementPageURL = Me.GetLinkValueFromXML(ATTRIBUTE_Abatement_PAGE)
            End If

            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If

            'donate page set property
            If String.IsNullOrEmpty(DonatePage) Then
                DonatePage = Me.GetLinkValueFromXML(DONATION_PAGE)
                If String.IsNullOrEmpty(DonatePage) Then
                    Me.btnNext.Tooltip = "Donation Page URL property has not been set."
                End If
            End If

            ' added by Redmine #20283
            If String.IsNullOrEmpty(ReportPage) Then
                ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
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
            Dim dtOrders As Data.DataTable
            'Dim dtProducts As DataTable
            Dim sSQL As String
            Dim iOrderId As Integer = 0
            Dim sProducts As String = String.Empty
            'Dim i As Integer, j As Integer
            Dim sprice As String = String.Empty

            Try

                'Suraj Issue 15287 4/9/13, if the grid dont have any record then grid should visible and it should show "No records " msg

                ' ''For i = 0 To dt.Rows.Count - 1
                ' ''    sSQL = "SELECT P.Name ,OD.Price FROM " & Database & "..vwOrderDetail OD INNER JOIN VwProducts P ON P.ID = OD.ProductID WHERE  OrderID = " & dt.Rows(i)("ID")
                ' ''    dtProducts = DataAction.GetDataTable(sSQL)
                ' ''    For j = 0 To dtProducts.Rows.Count - 1
                ' ''        sProducts = sProducts + dtProducts.Rows(j)(0).ToString() + " -" + dt.Rows(0).Item("CurrencySymbol").ToString() + "" + dtProducts.Rows(j)(1).ToString() + ".</br> "

                ' ''    Next
                ' ''    If sProducts.Length > 0 Then sProducts = sProducts.Substring(0, sProducts.Length - 1)
                ' ''    dt.Rows(i)("Product") = sProducts
                ' ''    sProducts = String.Empty
                ' ''    j = 0
                ' ''Next
                'grdMain.DataSource = dt
                ''Dim hlink As HyperLinkColumn

                'grdMain.DataBind()
                'By Vaishali
                If ViewState("orderdt") Is Nothing Then
                    sSQL = Database & "..spGetPaymentsByPersonID__c @BillToID=" & User1.PersonID.ToString()
                    dtOrders = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dtOrders Is Nothing AndAlso dtOrders.Rows.Count > 0 Then
                        sCuurSymbol = dtOrders.Rows(0).Item("CurrencySymbol").ToString()
                        ViewState("sCuurSymbol") = sCuurSymbol
                        txtTotal.Text = sCuurSymbol & "0.00"
                        grdMain.DataSource = dtOrders
                        ' grdMain.DataBind()
                        ViewState("orderdt") = dtOrders
                        lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BenevolentDonationNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

                        'Performance- Siddharth- Shifted below for loop code here, to avoid for loop now we are checking these conditions in store procedure and comparing single column value
                        If dtOrders.Compute("SUM(IsMembershipSubs)", String.Empty) > 0 Then
                            lblMessage.Visible = True
                            lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.IsMembershipOrSubscription__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        End If
                    Else
                        lblTotal.Visible = False
                        txtTotal.Visible = False
                        '' CreditCard.Visible = False
                    End If
                    '---------------------------

                    'Performance- Siddharth- Made changes to get count flag in a variable and then apply
                    Dim bVisibleFlag As Boolean = (dtOrders.Rows.Count > 0)
                    grdMain.Visible = bVisibleFlag
                    btnNext.Visible = bVisibleFlag
                    'lblNote.Visible = (dtOrders.Rows.Count > 0)
                    lblNoRecords.Visible = Not bVisibleFlag
                    'lblViewOrder.Visible = bVisibleFlag
                    'lblSupport.Visible = bVisibleFlag
                    btnDonate.Visible = bVisibleFlag


                    'Performance- Siddharth- Shifted below code above, to avoid for loop now we are checking these conditions in store procedure and comparing single column value
                    ''Sachin Sathe added below code

                    'If Not dtOrders Is Nothing Then
                    '    If dtOrders.Rows.Count > 0 Then
                    '        ''Added By Pradip 2017-04-03 For Make Payment Issue CAI - 2 critical issues
                    '        '   For i As Integer = 0 To dtOrders.Rows.Count 
                    '        For i As Integer = 0 To dtOrders.Rows.Count - 1
                    '            If dtOrders.Rows(i)("WebRemittanceNo__c") <> "" AndAlso Convert.ToDecimal(dtOrders.Rows(i)("Balance")) > 0 AndAlso CBool(dtOrders.Rows(i)("IsMember")) = True AndAlso CBool(dtOrders.Rows(i)("IsActive")) = True AndAlso (CBool(dtOrders.Rows(i)("DefaultDuesProduct")) = True Or CBool(dtOrders.Rows(i)("IsSubscription")) = True) Then
                    '                lblMessage.Visible = True
                    '                lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.IsMembershipOrSubscription__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    '                Exit For
                    '            End If
                    '            'Dim strSQL As String = ""
                    '            'Dim dtIsMemberOrSubscription As DataTable
                    '            '' Need to update below code for performance
                    '            'strSQL = Database & "..spcheckProductIsMemberOrSub__c @ProductID=" & dtOrders.Rows(i)("ProductID")
                    '            'dtIsMemberOrSubscription = DataAction.GetDataTable(strSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

                    '            'Dim strSQLs As String = ""
                    '            'Dim dtIsMember As DataTable
                    '            '' need to update below code performance
                    '            'strSQLs = Database & "..spcheckPersonIsMember__c @PersonID=" & User1.PersonID
                    '            'dtIsMember = DataAction.GetDataTable(strSQLs, IAptifyDataAction.DSLCacheSetting.BypassCache)

                    '            'If Not dtIsMemberOrSubscription Is Nothing Then
                    '            '    If dtIsMemberOrSubscription.Rows.Count > 0 Then
                    '            '        If dtOrders.Rows(i)("WebRemittanceNo__c") <> "" AndAlso Convert.ToInt32(dtOrders.Rows(i)("Balance")) > 0 AndAlso Convert.ToInt32(dtIsMember.Rows(0)(0)) > 0 AndAlso (CBool(dtIsMemberOrSubscription.Rows(0)("DefaultDuesProduct")) = True Or CBool(dtIsMemberOrSubscription.Rows(0)("IsSubscription")) = True) Then
                    '            '            lblMessage.Visible = True
                    '            '            lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.IsMembershipOrSubscription__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    '            '        Else
                    '            '            'lblMessage.Visible = False
                    '            '        End If
                    '            '    End If
                    '            'End If
                    '        Next



                    '    End If
                    'End If
                    '' End of Sachin Sathe's code


                Else
                    grdMain.DataSource = ViewState("orderdt")
                End If

                'ViewState("OrderNo") = Nothing
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Class PayInfo
            Public OrderID As Long
            Public PayAmount As Decimal
            Public Balance As Decimal
        End Class

        Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
            Try
                'Dim orderdetails As New Dictionary(Of Integer, Decimal)
                'If ViewState("CHECKED_PAYMENTS") IsNot Nothing Then
                '    orderdetails = DirectCast(ViewState("CHECKED_PAYMENTS"), Dictionary(Of Integer, Decimal))
                'End If
                ''Call stock code method
                'CheckAndSavePayments()
                Dim orderdetails As New Dictionary(Of Integer, Decimal)
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                Dim lblPay As New Label
                Dim txtPay As New TextBox, sbOrderLineID As New StringBuilder, sOrderLineID As String, selectFilter As String
                Dim dtorder As DataTable = ViewState("orderdt")
                If ViewState("CHECKED_PAYMENTS") IsNot Nothing Then
                    orderdetails = DirectCast(ViewState("CHECKED_PAYMENTS"), Dictionary(Of Integer, Decimal))
                Else
                    lblError.Text = "You must select one or more Order lines to make a Payment"
                    lblError.Visible = True
                    Exit Sub
                End If
                If orderdetails.Count = 0 Then
                    lblError.Text = "You must select one or more Order lines to make a Payment"
                    lblError.Visible = True
                    Exit Sub
                End If
                Dim pair As KeyValuePair(Of Integer, Decimal)
                For Each pair In orderdetails
                    sbOrderLineID.Append(pair.Key)
                    sbOrderLineID.Append(",")
                Next

                sOrderLineID = sbOrderLineID.ToString.Remove(sbOrderLineID.ToString.Length - 1)
                Dim dtSelectedOrders As DataTable = dtorder.Clone
                selectFilter = " OrderLineID IN (" & sOrderLineID & ")"

                ' Dim dataRows As DataRow() = CType(grdMain.DataSource, DataTable).Select(selectFilter)
                ''Commented By Pradip 2017-04-07
                ''Dim dataRows As DataRow() = dtorder.Select(selectFilter)
                ''Dim typeDataRow As DataRow
                ''For Each typeDataRow In dataRows
                ''    dtSelectedOrders.ImportRow(typeDataRow)
                ''Next





                Dim dv As DataView = New DataView(dtorder)
                dv.RowFilter = selectFilter
                dtSelectedOrders = dv.ToTable
                Dim x As Integer
                For Each pair In orderdetails
                    For x = 0 To dtSelectedOrders.Rows.Count - 1
                        If dtSelectedOrders.Rows(x)("OrderLineID") = pair.Key Then
                            dtSelectedOrders.Rows(x)("PayAmount") = pair.Value
                        End If
                    Next
                Next
                'Code to add is chartered support product selected
                dtSelectedOrders.Columns.Add("CharteredOrderLineID")
                'Performance- Used SP to avoid DB calls in loop
                Dim sSQL As String = AptifyApplication.GetEntityBaseDatabase("Persons") + "..spGetCharteredSupportProdOrderLineIDs__c"
                Dim oParam(0) As IDataParameter
                oParam(0) = DataAction.GetDataParameter("@OrderlineID", SqlDbType.NVarChar, sOrderLineID)
                Dim dtCharteredProdLines As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, oParam)
                For x = 0 To dtSelectedOrders.Rows.Count - 1
                    For y = 0 To dtCharteredProdLines.Rows.Count - 1
                        If dtSelectedOrders.Rows(x)("OrderLineID") = dtCharteredProdLines.Rows(y)("OrderLineID") Then
                            dtSelectedOrders.Rows(x)("CharteredOrderLineID") = dtCharteredProdLines.Rows(y)("CharteredOrderLineID")
                            Exit For
                        End If
                    Next
                Next
                ' Below Code added by GM for Redmine #20452
                Dim isMembership As Boolean, IsLateFee As Boolean, lateFeeProductCnt As Integer = 0
                For x = 0 To dtorder.Rows.Count - 1
                    If dtorder.Rows(x)("IsMembership") = 1 Then
                        isMembership = True
                    End If
                    If dtorder.Rows(x)("LateFeeProduct") = 1 Then
                        IsLateFee = True
                        lateFeeProductCnt = lateFeeProductCnt + 1
                    End If
                Next
                Dim SelectedisMembership As Boolean, SelectedIsLateFee As Boolean, SelectedlateFeeProductCnt As Integer = 0
                For y = 0 To dtSelectedOrders.Rows.Count - 1
                    If dtSelectedOrders.Rows(y)("IsMembership") = 1 Then
                        SelectedisMembership = True
                    End If
                    If dtSelectedOrders.Rows(y)("LateFeeProduct") = 1 Then
                        SelectedIsLateFee = True
                        SelectedlateFeeProductCnt = SelectedlateFeeProductCnt + 1
                    End If
                Next

                If isMembership = True And SelectedisMembership = True AndAlso IsLateFee = True Then
                    If (SelectedisMembership = True And SelectedIsLateFee = False) OrElse SelectedlateFeeProductCnt <> lateFeeProductCnt Then
                        lblError.Visible = True
                        lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipLateFeeProduct__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        Exit Sub
                    End If
                End If
                ' End Redmine #20452
                Session("orderdt") = dtSelectedOrders
                Response.Redirect(PaymentSummaryPage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub btnDonate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDonate.Click

            If String.IsNullOrEmpty(DonatePage) Then
                DonatePage = "/Members/Your-Institute/Chartered-Accountants-Support.aspx"
                Response.Redirect(DonatePage, False)
            Else

                Response.Redirect(DonatePage, False)

            End If
        End Sub

        Protected Sub grdMain_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemCreated
            Try
                Dim chkMakePayment As CheckBox = DirectCast(e.Item.FindControl("chkMakePayment"), CheckBox)
                Dim txtPayAmt As TextBox = DirectCast(e.Item.FindControl("txtPayAmt"), TextBox)
                If chkMakePayment IsNot Nothing Then
                    'Check in the ViewState
                    If ViewState("CHECKED_PAYMENTS") IsNot Nothing Then
                        Dim orderdetails As Dictionary(Of Integer, Decimal) = DirectCast(ViewState("CHECKED_PAYMENTS"), Dictionary(Of Integer, Decimal))
                        Dim dataItem As DataRowView = DirectCast(e.Item.DataItem, System.Data.DataRowView)

                        If dataItem IsNot Nothing Then
                            'Dim OrderNo As Long = dataItem("ID")
                            Dim OrderLineID As Long = dataItem("OrderLineID")
                            If orderdetails.ContainsKey(OrderLineID) = True Then
                                chkMakePayment.Checked = True
                                txtPayAmt.Text = orderdetails(OrderLineID)
                                'Performance- Siddharth: Commented below line as we have written client side code
                                'chkMakePayment_CheckedChanged(chkMakePayment, Nothing)
                            Else
                                chkMakePayment.Checked = False
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Performance- Siddharth: Commented to improve performance, as grid filters are removed now
        'Protected Sub grdMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemDataBound
        '    Dim dateColumns As New List(Of String)
        '    'Add datecolumn uniqueName in list for Date format
        '    dateColumns.Add("GridDateTimeColumnOrderDate")
        '    CommonMethods.FormatedDateOnGrid(dateColumns, e.Item)
        '    'Suraj Issue 14450 3/22/13 ,we provide a tool tip for DatePopupButton as well as the GridDateTimeColumnStartDate textbox   
        '    If TypeOf e.Item Is GridFilteringItem Then
        '        Dim filterItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
        '        Dim gridDateTimeColumnStartDate As RadDatePicker = DirectCast(filterItem("GridDateTimeColumnOrderDate").Controls(0), RadDatePicker)
        '        gridDateTimeColumnStartDate.ToolTip = "Enter a filter date"
        '        gridDateTimeColumnStartDate.DatePopupButton.ToolTip = "Select a filter date"
        '    End If
        '    'SaveCheckedValues()
        'End Sub

        Protected Sub grdMain_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
            LoadOrders()
        End Sub

        Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdMain.PageIndexChanged
            ''grdMain.PageIndex = e.NewPageIndex
            ' LoadOrders()
            SaveCheckedValues()
        End Sub
        'Suraj Issue 14450 3/22/13 ,if the grid load first time By default the sorting will be Ascending for column Forum 
        Private Sub AddExpression()
            Dim expression1 As New GridSortExpression
            expression1.FieldName = "ID"
            expression1.SetSortOrder("Ascending")
            grdMain.MasterTableView.SortExpressions.AddSortExpression(expression1)
        End Sub

        Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim headerCheckBox As CheckBox = CType(sender, CheckBox)
                For Each dataItem As GridDataItem In grdMain.MasterTableView.Items
                    CType(dataItem.FindControl("chkMakePayment"), CheckBox).Checked = headerCheckBox.Checked
                    dataItem.Selected = headerCheckBox.Checked
                Next
                Dim txtPayAmt As TextBox
                Dim iSumTotalPayAmmount As Decimal
                Dim sTotleAmmount As String
                Dim txtPayAmmount As String
                Dim iTotalAmmount As Decimal
                Dim lblEnableFunds As Label
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                For Each row As GridDataItem In grdMain.Items
                    Dim chkMakePayment As CheckBox = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                    lblEnableFunds = DirectCast(row.FindControl("lblEnableFunds"), Label)
                    If chkMakePayment.Checked = True Then
                        txtPayAmt = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                        txtPayAmmount = txtPayAmt.Text
                        sTotleAmmount = txtPayAmmount.ToString().Replace(sSymCurr, "")
                        iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")
                        iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
                        txtTotal.Text = sSymCurr & String.Format("{0:n2}", sSymCurr & iSumTotalPayAmmount)
                        If lblEnableFunds.Text = "1" Then
                            txtPayAmt.Enabled = True
                        End If
                    Else
                        If headerCheckBox.Checked Then
                            CType(row.FindControl("chkMakePayment"), CheckBox).Checked = headerCheckBox.Checked
                            row.Selected = headerCheckBox.Checked
                            txtPayAmmount = txtPayAmt.Text
                            sTotleAmmount = txtPayAmmount.ToString().Replace(sSymCurr, "")
                            iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")
                            iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
                            'suraj S Issue 16036 5/2/13, add comma for amount
                            txtTotal.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPayAmmount)
                            If lblEnableFunds.Text = "1" Then
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

        Protected Sub chkMakePayment_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim flag As Boolean = False
            Dim chkMakePayment As CheckBox = CType(sender, CheckBox)

            Dim header As GridHeaderItem = TryCast(grdMain.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
            Dim chkAllMakePayment As CheckBox = DirectCast(header.FindControl("chkAllMakePayment"), CheckBox)

            Dim grow As GridDataItem = DirectCast(chkMakePayment.NamingContainer, GridDataItem)
            Try
                ' Dim i As Integer = grow.DataSetIndex
                Dim txtPaymentaMT As TextBox = (DirectCast(grow.FindControl("txtPayAmt"), TextBox))
                Dim sSymCurrancy As String = ViewState("sCuurSymbol").ToString().Trim()

                Dim lblEnableFunds As Label = DirectCast(grow.FindControl("lblEnableFunds"), Label)
                If chkMakePayment.Checked And lblEnableFunds.Text = "1" Then
                    txtPaymentaMT.Enabled = True
                    txtPaymentaMT.Focus()
                Else
                    txtPaymentaMT.Enabled = False
                End If

                Dim iSumTotalPayAmmount As Decimal
                Dim chkflag As Boolean = True
                For Each row As GridDataItem In grdMain.Items
                    chkMakePayment = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                    If chkMakePayment.Checked = True Then
                        Dim txtPayAmt As TextBox = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                        Dim txtPayAmmount As String = txtPayAmt.Text
                        Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                        Dim sTotleAmmount As String = txtPayAmmount.ToString().Replace(sSymCurr, "")
                        Dim iTotalAmmount As Decimal
                        iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")
                        iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
                        'By Vaishali
                        setTotal(iSumTotalPayAmmount, sSymCurr)
                        'txtTotal.Text = String.Format("{0:n2}", sSymCurr & iSumTotalPayAmmount)
                        '--------------------------------
                    Else
                        chkflag = False
                        If chkAllMakePayment.Checked = True Then
                            chkAllMakePayment.Checked = False
                        End If
                    End If
                Next

                'By Vaishali
                CalculateTotal(iSumTotalPayAmmount)
                '--------------------------------
                If chkflag Then
                    chkAllMakePayment.Checked = True
                End If
                For Each row As GridDataItem In grdMain.Items
                    chkMakePayment = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                    'By Vaishali
                    SaveCheckedValues()
                    'If chkMakePayment.Checked = True Then
                    '    flag = True
                    'End If
                    '--------------------------------
                Next
                'By Vaishali
                'If Not flag Then
                '    txtTotal.Text = sSymCurrancy & "0.00"
                'End If
                '--------------------------------

                'Dim [Date] As [String] = DirectCast(DirectCast(sender, CheckBox).Parent.FindControl("lblDate"), Label).Text
            Catch ex As Exception
            End Try
        End Sub

        Protected Sub txtPayAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim flag As Boolean = False
                Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
                Dim txtPaymentaMT As TextBox = CType(sender, TextBox)
                Dim header As GridHeaderItem = TryCast(grdMain.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
                Dim chkAllMakePayment As CheckBox = DirectCast(header.FindControl("chkAllMakePayment"), CheckBox)
                Dim grow As GridDataItem = DirectCast(txtPaymentaMT.NamingContainer, GridDataItem)
                ' Dim i As Integer = grow.DataSetIndex
                Dim chkMakePayment As CheckBox = (DirectCast(grow.FindControl("chkMakePayment"), CheckBox))
                ' Dim lblPrice As Label = (DirectCast(grow.FindControl("lblPrice"), Label))

                If txtPaymentaMT.Enabled Then
                    If Not txtPaymentaMT.Text = "" AndAlso Convert.ToDecimal(txtPaymentaMT.Text) > 0 Then
                        chkMakePayment.Checked = True
                        ' lblPrice.Text = sSymCurr + txtPaymentaMT.Text
                    Else
                        chkMakePayment.Checked = False

                    End If
                End If

                Dim iSumTotalPayAmmount As Decimal
                Dim chkflag As Boolean = True
                For Each row As GridDataItem In grdMain.Items
                    chkMakePayment = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                    If chkMakePayment.Checked = True Then
                        Dim txtPayAmt As TextBox = DirectCast(row.FindControl("txtPayAmt"), TextBox)
                        Dim txtPayAmmount As String = txtPayAmt.Text
                        Dim sTotleAmmount As String = txtPayAmmount.ToString().Replace(sSymCurr, "")
                        Dim iTotalAmmount As Decimal
                        iTotalAmmount = Decimal.Parse(sTotleAmmount).ToString("0.00")
                        iSumTotalPayAmmount = iSumTotalPayAmmount + iTotalAmmount
                        'setTotal(iSumTotalPayAmmount, sSymCurr)
                        txtTotal.Text = String.Format("{0:n2}", sSymCurr & iSumTotalPayAmmount)
                        flag = True
                    Else
                        chkflag = False
                        If chkAllMakePayment.Checked Then
                            chkAllMakePayment.Checked = False
                        End If
                    End If
                Next
                CalculateTotal(iSumTotalPayAmmount)
                If chkflag Then
                    chkAllMakePayment.Checked = True
                End If
                'For Each row As GridDataItem In grdMain.Items
                '    chkMakePayment = DirectCast(row.FindControl("chkMakePayment"), CheckBox)
                '    If chkMakePayment.Checked = True Then
                '        flag = True
                '        Exit For
                '    End If
                'Next
                If Not flag Then
                    txtTotal.Text = sSymCurr & "0.00"
                End If
                SaveCheckedValues()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub SaveCheckedValues()
            'Dim orderdetails As New ArrayList
            Dim orderdetails As New Dictionary(Of Integer, Decimal)
            Dim total As Decimal = 0
            Dim index As Long = -1
            Dim lTotAmt As Decimal = 0.0
            Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
            Dim lblOrderLineID As Label = New Label
            For Each item As GridDataItem In grdMain.MasterTableView.Items
                'lblOrderID = CType(item.FindControl("ID"), Label)
                lblOrderLineID = CType(item.FindControl("lblOrderLineID"), Label)
                index = lblOrderLineID.Text
                Dim result As Boolean = DirectCast(item.FindControl("chkMakePayment"), CheckBox).Checked
                Dim txtPayAmt As TextBox = DirectCast(item.FindControl("txtPayAmt"), TextBox)

                'Check in the ViewState
                If ViewState("CHECKED_PAYMENTS") IsNot Nothing Then
                    orderdetails = DirectCast(ViewState("CHECKED_PAYMENTS"), Dictionary(Of Integer, Decimal))
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
                ViewState("CHECKED_PAYMENTS") = orderdetails
            End If
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
                Dim iTotal As Int64 = orderdetails.Count
                For i = iTotal - 1 To 0 Step -1
                    For Each item As GridDataItem In grdMain.MasterTableView.Items
                        ' lblOrderID = CType(item.FindControl("ID"), Label)
                        lblOrderLineID = CType(item.FindControl("OrderLineID"), Label)
                        If Not lblOrderLineID Is Nothing Then
                            index = lblOrderLineID.Text
                            Dim result As Boolean = DirectCast(item.FindControl("chkMakePayment"), CheckBox).Checked
                            If index = orderdetails.Keys(i) Then
                                orderdetails.Remove(index)
                            End If
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

        Private Sub setTotal(ByVal iSumTotalPayAmmount As Decimal, ByVal sSymCurr As String)
            If Not iSumTotalPayAmmount = 0 Then
                'suraj S Issue 16036 5/2/13, add comma for amount
                txtTotal.Text = sSymCurr & String.Format("{0:n2}", iSumTotalPayAmmount)
            Else
                txtTotal.Text = sSymCurr & "0.00"
            End If
        End Sub

        'stock save payment method
        Private Sub CheckAndSavePayments()
            ' Save a payment to the database with the information the user provided
            Dim i As Integer
            Dim arPay() As PayInfo
            Dim iColPayAmt As Integer
            Dim iColOrderID As Integer
            Dim iColBalance As Integer
            Dim iColNumDigits As Integer
            Dim lblPay As New WebControls.Label
            Dim hlkOrderID As WebControls.HyperLink
            Dim txtPay As New HtmlControls.HtmlInputText


            Try
                'Anil B change for 10254 on 29/03/2013
                ReDim arPay(0)

                'lblMessage.Visible = False

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
                For i = 0 To grdMain.Items.Count - 1
                    ' loop through each item in the grid to see if it was checked,
                    ' and store the order id, and amount
                    'Navin Prasad Issue 11032
                    ''RashmiP, Issue 11147, remove currency symbol from string. Because IsNumeric is not validating Currency symbol except $.
                    lblPay = grdMain.Items(i).Cells(iColBalance - 2).Controls(1)
                    txtPay = grdMain.Items(i).Cells(iColPayAmt + 2).Controls(1)
                    If txtPay.Value.Trim.Length > 0 Then
                        If IsNumeric(txtPay.Value) Then
                            'HP Issue#8941: only add payments where the user has put a value greater than zero
                            '               allowing payment for selected items
                            If CDec(txtPay.Value) > 0 Then
                                ReDim Preserve arPay(UBound(arPay) + 1)
                                arPay(UBound(arPay) - 1) = New PayInfo
                                With arPay(UBound(arPay) - 1)
                                    'Navin Prasad Issue 11032
                                    Dim lblNumDigits As Label = grdMain.Items(i).FindControl("lblNumDigitsAfterDecimal")
                                    Dim lblBal As Label = grdMain.Items(i).FindControl("lblBalance")
                                    hlkOrderID = grdMain.Items(i).Cells(iColOrderID + 2).Controls(0)
                                    .OrderID = hlkOrderID.Text
                                    .PayAmount = Math.Round(CDec(txtPay.Value), _
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
                Next

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
                    'Commented By vaishali
                    ''Anil B change for 10737 on 13/03/2013
                    ''Set Credit Card ID to load property form Navigation Config
                    'If Len(CreditCard.CCNumber) = 0 Or _
                    '   Len(CreditCard.CCExpireDate) = 0 Then
                    '    lblError.Text = "Credit Card Information Required"
                    '    lblError.Visible = True
                    'End If
                    'If PostPayment(arPay) Then
                    '    'HP - Issue 8264, virbiage is invalid when no information is being listes
                    '    'lblMessage.Text = "Payment was made! Your updated order information is shown below."
                    '    'lblMessage.Text = "Payment successfull !"
                    '    'lblMessage.Visible = True
                    '    'Anil B change for 10737 on 13/03/2013
                    '    'Set Credit Card ID to load property form Navigation Config
                    '    CreditCard.CCNumber = ""
                    '    CreditCard.CCExpireDate = ""
                    '    CreditCard.CCSecurityNumber = ""
                    '    LoadOrders()
                    '    'HP Issue#9092: need to bypass page form re-posting when refresh button is pressed causing a payment each time
                    '    Response.Redirect(Request.Path & "?msg=Payment was made!  Your updated order information is shown below.", False)
                    'Else
                    '    lblError.Text = "An error took place while processing your payment"
                    '    lblError.Visible = True
                    'End If
                    '-----------------------------------------------
                Else
                    lblError.Visible = True
                    'HP - Issue 8264, replace the default error message if payment amount is blank
                    'lblError.Text = "Please select a payment for at least one order"
                    lblError.Text = "All payments must be greater than zero"
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Private Sub LoadCategory()

        '    Try
        '        Dim sSQL As String = String.Empty
        '        Dim dtProductCategory As System.Data.DataTable = New DataTable()
        '        If dtProductCategory.Rows.Count = 0 Then
        '            sSQL = AptifyApplication.GetEntityBaseDatabase("ProductCategory") & _
        '                  "..SpGetProductCategory__c"
        '            dtProductCategory = Me.DataAction.GetDataTable(sSQL)
        '            ddlProductCategory.Items.Insert(0, "--Select Category--")
        '            dtProductCategory = DataAction.GetDataTable(sSQL)
        '            ddlProductCategory.DataSource = dtProductCategory
        '            ddlProductCategory.DataTextField = "Name"
        '            ddlProductCategory.DataValueField = "ID"
        '            ddlProductCategory.DataBind()
        '            ddlProductCategory.Items.Insert(0, New System.Web.UI.WebControls.ListItem("--Select Category--", "-1"))
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        'Private Sub LoadCategoryWiseOrder()

        '    Try
        '        If ddlProductCategory.SelectedValue = -1 Then
        '            LoadOrders()
        '        Else
        '            Dim sSQL As String = String.Empty
        '            Dim param(1) As IDataParameter
        '            Dim dt As System.Data.DataTable = New DataTable()
        '            Dim oda As New DataAction
        '            param(0) = oda.GetDataParameter("@BillToId", SqlDbType.Int, AptifyEbusinessUser1.PersonID.ToString())
        '            param(1) = oda.GetDataParameter("@ProductType", SqlDbType.VarChar, ddlProductCategory.Text)
        '            sSQL = "..spMakePaymentCategoryWise__c"
        '            dt = oda.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
        '            grdMain.DataSource = dt
        '            grdMain.DataBind()
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        'Protected Sub grdMain_PreRender(sender As Object, e As EventArgs) Handles grdMain.PreRender
        '    For Each item As GridDataItem In grdMain.MasterTableView.Items
        '        Dim orderID As String = item.Cells(2).Text
        '        If CInt(orderID) = iOrderId Then
        '            'item("ID").Text = ""
        '            item.Cells(4).Text = ""
        '            item.Cells(5).Text = ""
        '            item.Cells(7).Text = ""
        '            item.Cells(6).Text = ""
        '        Else
        '            iOrderId = CInt(orderID)
        '        End If

        '        Dim EnableFunds As String = item.Cells(12).Text
        '        If Convert.ToInt32(EnableFunds) = 0 Then
        '            item.Cells(10).Enabled = False
        '            item.Cells(11).Visible = False
        '        Else
        '            item.Cells(11).Visible = False
        '        End If

        '    Next
        'End Sub

        'Protected Sub ddlProductCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProductCategory.SelectedIndexChanged
        '    LoadCategoryWiseOrder()
        '    Dim sSymCurr As String = ViewState("sCuurSymbol").ToString().Trim()
        '    txtTotal.Text = String.Format("{0:n2}", sSymCurr & "0.00")
        'End Sub

        ''' <summary>
        ''' Code added by govind for displaying Abatement btn or not
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub subDisplayAbatementPage()
            Try
                Dim sSql As String = Database & "..spDisplayAbatementOnMakePayment__c @PersonID=" & User1.PersonID
                Dim lOrderID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lOrderID > 0 Then
                    btnAbatement.Visible = True
                    btnAbatement.Text = "Abatement For Order " & lOrderID
                Else
                    btnAbatement.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnAbatement_Click(sender As Object, e As System.EventArgs) Handles btnAbatement.Click
            Try
                Session("EntityID") = AptifyApplication.GetEntityID("Orders")
                Response.Redirect(AbatementPageURL, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Added for Redmine #20283
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub grdMain_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdMain.ItemCommand
            Try
                Select Case e.CommandName
                    Case "Invoicereport"
                        Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                        Dim oOrderID As Long = commandArgs(0)
                        If oOrderID > 0 Then
                            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "InvoiceStandardWEB__cai"))
                            Dim rptParam As New AptifyCrystalReport__c
                            'Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
                            rptParam.ReportID = ReportID
                            rptParam.Param1 = oOrderID
                            rptParam.SubParam1 = oOrderID
                            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                        End If
                End Select
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace
