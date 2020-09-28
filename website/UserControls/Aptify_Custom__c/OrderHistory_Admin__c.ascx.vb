'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande              17/03/2015                    Changes on stock Order History admin page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports Telerik.Web.UI
Imports System.Data
Imports System.Collections.Generic
Imports Aptify.Framework.DataServices


Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class OrderHistory_Admin__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_ORDER_CONFIRMATION_PAGE As String = "OrderConfirmationURLAdmin"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "OrderHistory_Admin__c"
        Protected Const ATTRIBUTE_ORDERHISTORYADMIN_VIEWSTATE As String = "OrderHistoryAdminsdt"
        Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage" 'Added by Sandeep for Issue 15051 on 12/03/2013
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage"
        Dim flag As Boolean = False
        Dim iOldIndex As Integer = -1
        Dim iNewindex As Integer = -1
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
#End Region

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
            'Added by Sandeep for Issue 15051 on 12/03/2013
            If String.IsNullOrEmpty(LoginPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_LOGIN_PAGE)
            End If
 If String.IsNullOrEmpty(ReportPage) Then
                ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
            End If

        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                ''LoadGrid()
                SetProperties()
                'flag = False
                If Not IsPostBack Then
                    'Suraj issue 14877 3/1/13 ,this method use to apply the odrering of rad grid first column
                    'AddExpression()
                    ' Added code govind 17/03/2015
                    LoadSubsidiariesCompanyList()
                    'End code govind
                    LoadGrid()
                End If
                If User1.UserID < 0 Then
                    Response.Redirect(LoginPage) 'Added by Sandeep for Issue 15051 on 12/03/2013
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub
        ' Govind Code started 17/3/2015
        Private Sub LoadSubsidiariesCompanyList()
            Try
                Dim sSqlCompanyID As String = "..spCompanyIDForPerson @PersonID=" & User1.PersonID
                Dim lCompanyID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlCompanyID, IAptifyDataAction.DSLCacheSetting.BypassCache))
                Dim sSql As String = Database & "..spGetAllSubsidiaries__c @ParentCompanyId=" & lCompanyID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ddlCompanies.ClearSelection()
                    ddlCompanies.DataSource = dt
                    ddlCompanies.DataTextField = "Name"
                    ddlCompanies.DataValueField = "ID"
                    ddlCompanies.DataBind()
                End If
                SetComboValue(ddlCompanies, Convert.ToString(AptifyApplication.GetEntityRecordName("Companies", lCompanyID)))
                If Not dt Is Nothing AndAlso dt.Rows.Count > 1 Then
                    ddlCompanies.Visible = True
                    lblCompany.Visible = True
                Else
                    ddlCompanies.Visible = False
                    lblCompany.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList, _
                                  ByVal sValue As String)
            Dim i As Integer

            Try
                cmb.ClearSelection()
                For i = 0 To cmb.Items.Count - 1
                    If String.Compare(cmb.Items(i).Value, sValue, True) = 0 Then
                        cmb.Items(i).Selected = True
                        Exit Sub
                    End If
                    '11/27/07,Added by Tamasa,Issue 5222.
                    If String.Compare(cmb.Items(i).Text, sValue, True) = 0 Then
                        cmb.Items(i).Selected = True
                        Exit Sub
                    End If
                    'End
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ' Govind Code End 17/3/2015

        Private Sub LoadGrid()
            'Anil B for issue 14343 on 03/04/2013
            'Set Sorting option for detail grid
            AddDetailExpression()
            ' load the grid with the user's past ordere history
            Dim sSQL As String, dt As Data.DataTable
            Dim sOrderStatus As String = ""
            Dim sDate As String = ""
            Try
                'Suraj issue 14877 2/20/13 , check the view state is nothing or not if the page load first time viewstate will be nothing but after bostback view state will conatin the datatable
                ''If ViewState(ATTRIBUTE_ORDERHISTORYADMIN_VIEWSTATE) Is Nothing Then
                Dim sWhere As String = ""
                If Request.QueryString("OrderStatus") IsNot Nothing AndAlso Request.QueryString("OrderStatus").ToString() <> "" Then
                    sOrderStatus = Request.QueryString("OrderStatus")
                End If
                If String.IsNullOrEmpty(sOrderStatus) = False Then
                    sOrderStatus = sOrderStatus.ToUpper.Trim

                    If sOrderStatus = "PAID" Then
                        sWhere = " AND o.Balance = 0 "
                        If Request.QueryString("Date") IsNot Nothing AndAlso Request.QueryString("Date").ToString() <> "" Then
                            sDate = Request.QueryString("Date")
                        End If
                        If String.IsNullOrEmpty(sDate) = False AndAlso IsDate(sDate) Then
                            'sWhere = sWhere & " AND Month(o.OrderDate)=" & CType(sDate, Date).Month() & " AND Year(o.OrderDate)=" & CType(sDate, Date).Year() & " AND o.OrderStatus IN ('Taken', 'Shipped') AND o.OrderType IN ('Regular', 'Quotation') "
				''cHANGE bY pradip 2016-08-16 to display only shipped orders
                            sWhere = sWhere & " AND Month(o.OrderDate)=" & CType(sDate, Date).Month() & " AND Year(o.OrderDate)=" & CType(sDate, Date).Year() & "  AND o.OrderType IN ('Regular', 'Quotation') "                        


End If
                    End If

                End If
                ' added sp govind by 18/03/2015
                sSQL = Database & "..spGetFirmOrderHistoryDetails__c @CompanyID=" & ddlCompanies.SelectedValue & ",@Where='" & sWhere & "'"
                dt = DataAction.GetDataTable(sSQL)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim dcolUrl As DataColumn = New DataColumn()
                    dcolUrl.Caption = "LinkUrl"
                    dcolUrl.ColumnName = "LinkUrl"
                    dt.Columns.Add(dcolUrl)
                    If dt.Rows.Count > 0 Then
Dim IsFirm As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(1)
                        For Each rw As DataRow In dt.Rows
                            rw("LinkUrl") = Me.OrderConfirmationURL + "?ID=" + rw("ID").ToString & "&ISFirm=" & System.Web.HttpUtility.UrlEncode(IsFirm)
                        Next
                    End If
                End If
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    grdMain.DataSource = dt
                    grdMain.DataBind()
                    grdMain.Visible = True
		    btnPrint.Visible = True
                Else
		    btnPrint.Visible = False
                    grdMain.Visible = False
                    grdMain.DataSource = Nothing
                End If
                ViewState(ATTRIBUTE_ORDERHISTORYADMIN_VIEWSTATE) = dt
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub grdMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemDataBound
            'Anil B for issue 14343 on 03/04/2013
            'If open detail grid do not need to call date formate function
            If TypeOf e.Item Is GridDataItem AndAlso e.Item.OwnerTableView.Name <> "ChildGrid" Then
                Dim dateColumns As New List(Of String)
                'Add datecolumn uniqueName in list for Date format
                dateColumns.Add("GridDateTimeColumnOrderDate")
                CommonMethods.FormatedDateOnGrid(dateColumns, e.Item)
            End If
        End Sub


        Protected Sub grdMain_DetailTableDataBind(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles grdMain.DetailTableDataBind
            AddDetailExpression()
            Dim sSQL As String, dtDeatil As Data.DataTable
            Dim dataItem As Telerik.Web.UI.GridDataItem = CType(e.DetailTableView.ParentItem, Telerik.Web.UI.GridDataItem)
            Dim ss As Boolean = e.DetailTableView.ParentItem.Expanded
            Dim Id As Integer = Convert.ToInt32(dataItem.GetDataKeyValue("ID"))
            ' Added Sp Govind 18/03/2015
            sSQL = Database & "..spGetFirmOrderHistoryDetailsAsPerOrder__c @OrderID=" & Id & ",@CompanyID=" & ddlCompanies.SelectedValue
            dtDeatil = DataAction.GetDataTable(sSQL)
            If Not dtDeatil Is Nothing AndAlso dtDeatil.Rows.Count > 0 Then
                e.DetailTableView.DataSource = dtDeatil
            End If
        End Sub
        Protected Sub RadComboBox1_ItemDataBound(ByVal sender As Object, ByVal e As RadComboBoxItemEventArgs)
            'set the Text and Value property of every item
            'here you can set any other properties like Enabled, ToolTip, Visible, etc.
            AddDetailExpression()
            e.Item.Text = (DirectCast(e.Item.DataItem, DataRowView))("FirstLast").ToString()
            e.Item.Value = (DirectCast(e.Item.DataItem, DataRowView))("ID").ToString()
        End Sub
        Protected Function GetFormattedCurrency(ByVal Container As Object, ByVal sField As String) As String
            Dim sCurrencySymbol As String
            Dim iNumDecimals As Integer
            Dim sCurrencyFormat As String
            Dim sCurrencyValue As String
            Dim sCurrencyFormateForNegative As String

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

                'Anil B for 14343 on 20-03-2013
                'Add condition to handle negative value
                ' format the string using the currency format created
                If IsNumeric(Container.DataItem(sField)) AndAlso Container.DataItem(sField) >= 0 Then
                    Return String.Format(sCurrencyFormat, Container.DataItem(sField))
                Else
                    sCurrencyValue = Convert.ToString((Container.DataItem(sField)))
                    sCurrencyValue = sCurrencyValue.Replace("-", "")
                    sCurrencyFormateForNegative = "(" & String.Format(sCurrencyFormat, sCurrencyValue) & ")"
                    Return sCurrencyFormateForNegative
                End If

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
        'Suraj issue 14877 2/20/13 ,if the grid load first time By default the sorting will be Ascending for column Order #
        Private Sub AddExpression()
            Dim expression1 As New GridSortExpression
            expression1.FieldName = "ID"
            expression1.SetSortOrder("Ascending")
            grdMain.MasterTableView.SortExpressions.AddSortExpression(expression1)
        End Sub
        'Anil B for issue 14343 on 03/04/2013
        'Add function to enable sorting on detail grid
        Private Sub AddDetailExpression()
            Dim DetailExpression As New GridSortExpression
            DetailExpression.FieldName = "Product"
            DetailExpression.SetSortOrder("Ascending")
            grdMain.MasterTableView.DetailTables.Item(0).SortExpressions.AddSortExpression(DetailExpression)
        End Sub
        ''Issue 16807, Rashmi P
        Protected Sub grdMain_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
            If ViewState(ATTRIBUTE_ORDERHISTORYADMIN_VIEWSTATE) IsNot Nothing Then
                grdMain.DataSource = CType(ViewState(ATTRIBUTE_ORDERHISTORYADMIN_VIEWSTATE), DataTable)
            End If
        End Sub

        Protected Sub grdMain_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdMain.PageIndexChanged
            If ViewState(ATTRIBUTE_ORDERHISTORYADMIN_VIEWSTATE) IsNot Nothing Then
                grdMain.DataSource = CType(ViewState(ATTRIBUTE_ORDERHISTORYADMIN_VIEWSTATE), DataTable)
                grdMain.CurrentPageIndex = e.NewPageIndex
            End If
        End Sub

        Protected Sub grdMain_PageSizeChanged(sender As Object, e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles grdMain.PageSizeChanged
            If ViewState(ATTRIBUTE_ORDERHISTORYADMIN_VIEWSTATE) IsNot Nothing Then
                grdMain.DataSource = CType(ViewState(ATTRIBUTE_ORDERHISTORYADMIN_VIEWSTATE), DataTable)
            End If
        End Sub
        ''' <summary>
        ''' Added Govind code 17/03/2015
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub ddlCompanies_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompanies.SelectedIndexChanged
            Try
                LoadGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
            Try
                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "InvoiceFirmDetails"))
                Dim rptParam As New AptifyCrystalReport__c
                rptParam.ReportID = ReportID
                rptParam.Param1 = Convert.ToString(ddlCompanies.SelectedValue)
                rptParam.SubParam1 = Convert.ToString(ddlCompanies.SelectedValue)
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace
