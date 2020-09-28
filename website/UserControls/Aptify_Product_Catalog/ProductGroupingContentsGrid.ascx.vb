Option Explicit On
Option Strict On

Imports Aptify.Framework.DataServices
Imports Aptify.Framework.Application
Imports Aptify.Applications.OrderEntry
Imports Aptify.Framework.Web.eBusiness.ProductCatalog
Imports System.Web
Imports System.Data
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.ProductCatalog
    Partial Class ProductGroupingContentsGrid
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Private ShoppingCart1 As Aptify.Framework.Web.eBusiness.AptifyShoppingCart
        Private sTotalPrice As String
        Private sMemberTotalPrice As String
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "ProductGroupingContentsGrid"
        'Neha, Issue 14456, 3/18/13 , ATTRIBUTE_CONTORL_PRODUCT_GROUPING_CONTENTS used for set the property "CheckAddExpressionForProduct"
        Protected ATTRIBUTE_CONTORL_PRODUCT_GROUPING_CONTENTS As Boolean = False


#Region "ProductGroupingContentsGrid Specific Properties"
        Public Property NavigateURLFormatField() As String
            Get
                Dim o As Object
                o = ViewState.Item("NavigateURLFormatField")
                If o Is Nothing Then
                    Return ""
                Else
                    Return CStr(o)
                End If
            End Get
            Set(ByVal Value As String)
                ViewState.Add("NavigateURLFormatField", Value)
            End Set
        End Property

        Public Property ShoppingCart() As Aptify.Framework.Web.eBusiness.AptifyShoppingCart
            Get
                Return ShoppingCart1
            End Get
            Set(ByVal value As Aptify.Framework.Web.eBusiness.AptifyShoppingCart)
                ShoppingCart1 = value
            End Set
        End Property

        Public Property FormattedGroupPrice() As String
            Get
                Return sTotalPrice
            End Get
            Set(ByVal value As String)
                sTotalPrice = value
            End Set
        End Property
        'Added as pat of #20508
        Public Property FormattedGroupMemberPrice() As String
            Get
                Return sMemberTotalPrice
            End Get
            Set(ByVal value As String)
                sMemberTotalPrice = value
            End Set
        End Property
        'Neha, Issue 14456 3/18/13 , declare the prpoperty ,Grid load first time By default the sorting will be Ascending 
        Public Property CheckAddExpressionForProduct() As Boolean
            Get
                Return Me.ATTRIBUTE_CONTORL_PRODUCT_GROUPING_CONTENTS
            End Get
            Set(ByVal value As Boolean)
                Me.ATTRIBUTE_CONTORL_PRODUCT_GROUPING_CONTENTS = value
            End Set
        End Property

#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()


        End Sub
        'neha changes for Issue 14456
        Public Sub LoadGrid(ByVal ProductID As Long, ByVal ProductName As String)
            'Neha, Issue 14456, 3/18/13 , if CheckAddExpressionForProduct property is true then call the AddExpression and the value for this set on search user control
            If CheckAddExpressionForProduct = True Then
                AddExpression()
            End If

            Dim sSQL As String
            Dim dt As System.Data.DataTable
            Try
                If ViewState("ProductGroupingContentGridData") Is Nothing Then

                    ViewState("ProductName") = ProductName
                    ViewState("productID") = ProductID

                    'Query database to retrieve all data to enter into the datagrid
                    sSQL = "SELECT pa.SubProductID ProductID, p.WebName, " & _
                           "p.WebDescription, pa.Quantity " & _
                           "FROM " & AptifyApplication.GetEntityBaseDatabase("Products") & _
                           "..vwProductParts pa INNER JOIN " & _
                           AptifyApplication.GetEntityBaseDatabase("Products") & _
                           "..vwProducts p on pa.SubProductID=p.ID " & _
                           "WHERE pa.ProductID=" & ProductID & _
                           "ORDER BY pa.Sequence ASC"
                    dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

                    If dt.Rows.Count > 0 Then
                        'set table's title
                        lblTitle.Text = ProductName & " contains the below" 'WongS Modified as part of #20438
                        'Modified as pat of #20508
                        Dim icheck As Integer = 0
                        Dim ssSQL As String = AptifyApplication.GetEntityBaseDatabase("Products") &
                           "..spCheckProductCategory__c @ProductID=" & ProductID
                        icheck = CInt(DataAction.ExecuteScalar(ssSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        'Add Prices to the datatable And set cumulative price
                        Dim dTotalPrice As Decimal = 0
                        Dim dMemberTotalPrice As Decimal = 0
                        dt.Columns.Add("Price", Type.GetType("System.String"))
                        If icheck = 1 Then
                            dt.Columns.Add("MemberPrice", Type.GetType("System.String"))
                        End If
                        Dim oPrice As IProductPrice.PriceInfo
                        Dim User1 As System.Security.Principal.IPrincipal = CType(Session("PageUser"), System.Security.Principal.IPrincipal)
                        Dim ShoppingCart2 As Aptify.Framework.Web.eBusiness.AptifyShoppingCart = CType(Session("ShoppingCart"), Aptify.Framework.Web.eBusiness.AptifyShoppingCart)
                        For index As Integer = 0 To dt.Rows.Count - 1
                            Dim oProductID = CLng(dt.Rows(index).Item("ProductID"))

                            oPrice = ShoppingCart2.GetUserProductPrice(oProductID, CDec(dt.Rows(index).Item("Quantity")))
                            Dim sPrice As Decimal = oPrice.Price

                            If icheck = 1 Then
                                Dim sFormat As String = ShoppingCart2.GetCurrencyFormat(oPrice.CurrencyTypeID)
                                Dim shipToID As Long, billToID As Long
                                Dim oOrder As OrdersEntity
                                Dim sMemberPrice As Decimal = 0
                                Dim MemberPersonId As Long = Convert.ToInt64(AptifyApplication.GetEntityAttribute("Persons", "MemberPersonID__c"))

                                billToID = MemberPersonId
                                shipToID = MemberPersonId

                                If billToID > 0 And shipToID > 0 Then
                                    oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                                    oOrder.BillToID = billToID
                                    oOrder.ShipToID = shipToID
                                    oOrder.SetValue("CurrencyTypeID", oPrice.CurrencyTypeID)
                                    oOrder.AddProduct(oProductID)

                                    If oOrder.SubTypes("OrderLines").Count() > 0 AndAlso (oProductID = CLng(ProductID) And oPrice.price > Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended"))) Then

                                        sMemberPrice = Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended"))

                                    ElseIf oPrice.CurrencyTypeID <> 3 And oOrder.SubTypes("OrderLines").Count() > 0 AndAlso Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended")) <> Convert.ToDecimal(oPrice.Price) Then

                                        sMemberPrice = Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended"))

                                    ElseIf Not User1 Is Nothing And oOrder.SubTypes("OrderLines").Count() > 0 AndAlso ShoppingCart2.GetSingleProductNonMemberCost(User1, oProductID) > 0 AndAlso ShoppingCart2.GetSingleProductMemberCost(User1, oProductID) <> ShoppingCart2.GetSingleProductNonMemberCost(User1, oProductID) Then

                                        sMemberPrice = ShoppingCart2.GetSingleProductMemberCost(User1, oProductID)
                                        sPrice = ShoppingCart2.GetSingleProductNonMemberCost(User1, oProductID)

                                    ElseIf oPrice.price = 0 And oOrder.SubTypes("OrderLines").Count() = 1 Then
                                        sPrice = Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended"))
                                    Else

                                        Dim dtCart As New System.Data.DataTable()
                                        ShoppingCart2.AddToCart(oProductID)
                                        ShoppingCart2.FillCart(dtCart)

                                        Dim cartCount As Integer = dtCart.Rows.Count()

                                        If Not dtCart Is Nothing AndAlso cartCount > 0 Then

                                            Dim getCDecPrice As Decimal = 0
                                            Dim sGetDecPrice = String.Empty
                                            sGetDecPrice = dtCart.Rows(cartCount - 1).Item("Extended").ToString.Replace("€", "").Replace("£", "")
                                            getCDecPrice = CDec(sGetDecPrice)

                                            If Math.Round(oPrice.price, 2) > getCDecPrice Then
                                                sMemberPrice = getCDecPrice
                                            End If
                                        End If
                                    End If
                                    If Math.Round(CType(sMemberPrice, Double), 2) = Math.Round(CType(sPrice, Double), 2) Then
                                        sMemberPrice = 0
                                    End If

                                    oOrder = Nothing
                                End If

                                If sMemberPrice <> 0 Then
                                    dt.Rows(index).Item("MemberPrice") = Format(sMemberPrice, sFormat)
                                Else
                                    dt.Rows(index).Item("MemberPrice") = "--"
                                End If

                                dt.Rows(index).Item("Price") = Format(sPrice, sFormat)

                            End If
                            'End of #20508
                        Next
                        Me.FormattedGroupPrice = ""
                        Me.FormattedGroupPrice = Format(dTotalPrice, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                        'Added as pat of #20508
                        Me.FormattedGroupPrice = ""
                        Me.FormattedGroupMemberPrice = Format(dMemberTotalPrice, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))

                        Dim dcolUrl As DataColumn = New DataColumn()
                        dcolUrl.Caption = "WebNameUrl"
                        dcolUrl.ColumnName = "WebNameUrl"

                        dt.Columns.Add(dcolUrl)

                        Dim dcolUrlPrice As DataColumn = New DataColumn()
                        dcolUrlPrice.Caption = "PriceUrl"
                        dcolUrlPrice.ColumnName = "PriceUrl"

                        dt.Columns.Add(dcolUrlPrice)
                        'Added as pat of #20508
                        Dim dcolUrlMemberPrice As DataColumn = New DataColumn()
                        dcolUrlMemberPrice.Caption = "MemberPriceUrl"
                        dcolUrlMemberPrice.ColumnName = "MemberPriceUrl"
                        dt.Columns.Add(dcolUrlMemberPrice)




                        If Not String.IsNullOrEmpty(NavigateURLFormatField) Then
                            If dt.Rows.Count > 0 Then

                                For Each rw As DataRow In dt.Rows
                                    rw("WebNameUrl") = String.Format(NavigateURLFormatField, rw("ProductID").ToString)
                                    rw("PriceUrl") = String.Format("{0:C}", rw("Price").ToString)
                                    'Added as pat of #20508
                                    If icheck = 1 Then
                                        rw("MemberPriceUrl") = String.Format("{0:C}", rw("MemberPrice").ToString)
                                    End If
                                    ''lbl.Text = rw("PriceUrl")
                                Next
                            End If

                        End If
                        grdMain.DataSource = dt
                        ViewState("ProductGroupingContentGridData") = dt
                        'Navin Prasad Issue 11032

                        'With DirectCast(grdMain.Columns.Item(0), HyperLinkColumn)
                        '    If Not String.IsNullOrEmpty(NavigateURLFormatField) Then
                        '        .DataNavigateUrlFormatString = NavigateURLFormatField
                        '    Else
                        '        grdMain.Enabled = False
                        '        grdMain.ToolTip = "NavigateURLFormatField property not set via container control."
                        '    End If
                        'End With

                        grdMain.DataBind() 'fails on the Price, since no field named Price exists in the dt
                        'Added as pat of #20508
                        'If icheck = 1 Then
                        grdMain.Columns.Item(4).Visible = True
                        'End If

                        Dim rowcounter As Integer = 0

                        'Navin Prasad Issue 11032

                        If Not String.IsNullOrEmpty(NavigateURLFormatField) Then
                            'For Each row As GridViewRow In grdMain.Items
                            '    Dim lnk As System.Web.UI.WebControls.HyperLink = CType(row.FindControl("lnkWebName"), System.Web.UI.WebControls.HyperLink)
                            '    lnk.NavigateUrl = String.Format(NavigateURLFormatField, dt.Rows((grdMain.CurrentPageIndex * grdMain.PageSize) + rowcounter)("ProductID").ToString)
                            '    Dim lbl As Label = CType(row.FindControl("lblPrice"), Label)
                            '    lbl.Text = String.Format("{0:C}", dt.Rows((grdMain.CurrentPageIndex * grdMain.PageSize) + rowcounter)("Price").ToString)
                            '    rowcounter = rowcounter + 1
                            'Next
                        Else
                            grdMain.Enabled = False
                            grdMain.ToolTip = "NavigateURLFormatField property not set via container control."
                        End If


                    Else
                        Me.Visible = False
                    End If

                Else
                    grdMain.DataSource = ViewState("ProductGroupingContentGridData")

                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'neha changes for Issue 14456
        Protected Sub grdMain_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
            Dim Id As Long = CLng(ViewState("productID"))
            Dim str As String = ViewState("ProductName").ToString()
            Me.LoadGrid(Id, str)
        End Sub
        'Added method for sort order(assending) for rad grid first column
        Private Sub AddExpression()
            Dim expression1 As New GridSortExpression
            expression1.FieldName = "WebName"
            expression1.SetSortOrder("Ascending")
            grdMain.MasterTableView.SortExpressions.AddSortExpression(expression1)
        End Sub

    End Class
End Namespace
