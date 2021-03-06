
'-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
'DEVELOPER                              DATE                                        Comments
'Govind Mande                           24/12/2014                        For Training Ticket campaign product dones not change the Quantity
'----------------------------------------------------------------------------------------------------------------------------------------------------------------------
Option Explicit On

Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports System.Data
Imports Telerik.Web.UI
Imports SoftwareDesign.GTM
Imports SoftwareDesign.GTM.Model


Namespace Aptify.Framework.Web.eBusiness.ProductCatalog
    Partial Class CartGrid__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_PRODUCT_VIEW_PAGE As String = "ProductViewPage"
        Protected Const ATTRIBUTE_CLASS_VIEW_PAGE As String = "ClassViewPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "CartGrid"
        Protected Const ATTRIBUTE_VIEW_PRODUCT_SUBSCRIPTIONPRODUCTDETAILSLINK_PAGE As String = "SubscriptionProductDetailsLinkCart"

#Region "CartGrid Specific Properties"
        ''' <summary>
        ''' ProductView page url
        ''' </summary>
        Public Overridable Property ProductViewPage() As String
            Get
                If Not ViewState(ATTRIBUTE_PRODUCT_VIEW_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PRODUCT_VIEW_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PRODUCT_VIEW_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ClassView page url
        ''' </summary>
        Public Overridable Property ClassViewPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CLASS_VIEW_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CLASS_VIEW_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CLASS_VIEW_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' Page ID
        ''' </summary>
        Public Overridable ReadOnly Property PageID() As String
            Get
                If Request.Url.ToString.ToUpper().Contains("CHECKOUT") Then
                    Return "CheckOut"
                ElseIf Request.Url.ToString.ToUpper().Contains("VIEWCART") Then
                    Return "ViewCart"
                Else
                    Return ""
                End If
            End Get
        End Property

#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(ProductViewPage) Or String.IsNullOrEmpty(ClassViewPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ProductViewPage = Me.GetLinkValueFromXML(ATTRIBUTE_PRODUCT_VIEW_PAGE)
                ClassViewPage = Me.GetLinkValueFromXML(ATTRIBUTE_CLASS_VIEW_PAGE)
                If String.IsNullOrEmpty(ProductViewPage) Or String.IsNullOrEmpty(ClassViewPage) Then
                    'Me.grdMain.Columns.RemoveAt(2)
                    'grdMain.Columns.AddAt(2, New BoundColumn())
                    'With DirectCast(grdMain.Columns(2), BoundColumn)
                    '    .DataField = "Product"
                    '    .HeaderText = "Product"
                    '    .ItemStyle.ForeColor = Drawing.Color.Blue
                    '    .ItemStyle.Font.Underline = True
                    'End With
                    Me.grdMain.Enabled = False
                    Me.grdMain.ToolTip = "ProductViewPage and/or ClassViewPage properties has not been set."
                End If
            End If
            If String.IsNullOrEmpty(SubscriptionProductDetailsLinkCart) Then
                'since value is the 'default' check the XML file for possible custom setting
                SubscriptionProductDetailsLinkCart = Me.GetLinkValueFromXML(ATTRIBUTE_VIEW_PRODUCT_SUBSCRIPTIONPRODUCTDETAILSLINK_PAGE)
                If String.IsNullOrEmpty(SubscriptionProductDetailsLinkCart) Then

                    'grdResults.ToolTip = "ViewProductCatagoryPage property has not been set."
                Else
                    ' DirectCast(grdResults.Columns(2), HyperLinkColumn).DataNavigateUrlFormatString = ViewProductCatagoryPage & "?ID={0}"
                End If
            Else
                ' DirectCast(grdResults.Columns(2), HyperLinkColumn).DataNavigateUrlFormatString = ViewProductCatagoryPage & "?ID={0}"
            End If
        End Sub
        Public Overridable Property SubscriptionProductDetailsLinkCart() As String
            Get
                If Not ViewState(ATTRIBUTE_VIEW_PRODUCT_SUBSCRIPTIONPRODUCTDETAILSLINK_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_VIEW_PRODUCT_SUBSCRIPTIONPRODUCTDETAILSLINK_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_VIEW_PRODUCT_SUBSCRIPTIONPRODUCTDETAILSLINK_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

#Region "Databound Template Fields"
        ' GetRowQuantityEnabled ---------------------------------------------
        '   Author:     Richard Bowman
        '   Date:       7/1/2003
        '
        ' This property is used by the grid to assess whether to make a
        ' row's quantity field enabled or disabled. The quantity column
        ' functions as a databound template column in the grid, so for
        ' each row in the grid, this method will be called.
        '
        '   Parameters: Container : the row of the grid this call references
        '   Returns:    Boolean   : True, if the quantity field should
        '                           be enabled
        '
        Public ReadOnly Property GetRowQuantityEnabled(ByVal Container As Object) As Boolean
            Get
                Dim sSQL As String
                Dim dt As DataTable
                Dim sProductType As String
                Try
                    ' find the ProductTypeID for the product in question
                    sSQL = "SELECT ProductType FROM " & _
                           AptifyApplication.GetEntityBaseDatabase("Products") & _
                           "..vwProducts WHERE " & _
                           "ID=" & CLng(Container.DataItem("ProductID"))
                    dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    sProductType = CStr(dt.Rows(0).Item("ProductType"))

                    ' if the control is a child of a kit or is a meeting item (type=1)
                    ' then this returns false, otherwise it returns true
                    Return (CLng(Container.DataItem("ParentSequence")) <= 0) _
                            And (sProductType <> "Meeting")
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Finally
                    dt = Nothing
                End Try
            End Get
        End Property

        ' GetQtyBorderStyle -----------------------------------------------
        '   Author:     Mark Smith
        '   Date:       11/30/2006
        '
        ' This property is used by the grid to assess whether to make 
        ' the borderstyle of a row's quantity field 3d or flat. The quantity 
        ' column functions as a databound template column in the grid, so for
        ' each row in the grid, this method will be called.
        '
        ' The TextBox BorderStyle is the enumerated value of the following list:
        '        1  = NotSet  - The border style is not set.
        '        2  = None    - No border
        '        3  = Dotted  - A dotted line border.
        '        4  = Dashed  - A dashed line border.
        '        5  = Solid   - A solid line border.
        '        6  = Double  - A solid double line border.
        '        7  = Groove  - A grooved border for a sunken border appearance.
        '        8  = Ridge   - A ridged border for a raised border appearance.
        '        9  = Inset   - An inset border for a sunken control appearance.
        '        10 = Outset  - An outset border for a raised control appearance. 
        '
        '   Parameters: Container : The web control being altered
        '   Returns:    Byte:   The enumerated value of the desired borderstyle
        '
        Public ReadOnly Property GetQtyBorderStyle(ByVal Container As Object) As Byte
            Get
                If GetRowQuantityEnabled(Container) Then
                    Return 0 'borderstyle = NotSet (default = Inset)
                Else
                    Return 1 'borderstyle = None
                End If
            End Get
        End Property

#End Region
#Region "Application and DataAction Properties"
        Private m_oApp As AptifyApplication
        Private m_oDA As DataAction
        Public Overridable ReadOnly Property AptifyApplication() As AptifyApplication
            Get
                If m_oApp Is Nothing Then
                    Dim g As New EBusinessGlobal
                    m_oApp = g.GetAptifyApplication(Page.Application, Page.User)
                End If
                Return m_oApp
            End Get
        End Property
        Public Overridable ReadOnly Property DataAction() As DataAction
            Get
                If m_oDA Is Nothing Then
                    m_oDA = New DataAction(AptifyApplication.UserCredentials)
                End If
                Return m_oDA
            End Get
        End Property
#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not IsPostBack Then
                RefreshGrid()
            End If
        End Sub
        Public Sub RefreshGrid()
            Dim ItemList As New ArrayList()
            Dim dt As New System.Data.DataTable()
            Try

 ''Added BY Pradip 2016-01-23 for issue https://redmine.softwaredesign.ie/issues/15798
                If Session("IsFromLogin") IsNot Nothing AndAlso Convert.ToString(Session("IsFromLogin")) = "YES" Then
                    Dim oOrderNew As Aptify.Applications.OrderEntry.OrdersEntity
                    oOrderNew = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                    Dim i As Integer = 0
                    Dim ProdData(oOrderNew.SubTypes("OrderLines").Count - 1, 1) As Integer
                    For Each oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrderNew.SubTypes("OrderLines")
                        ProdData(i, 0) = CInt(oOrderLine.GetValue("ProductID"))
                        ProdData(i, 1) = CInt(oOrderLine.GetValue("Quantity"))
                        ShoppingCart1.RemoveItem(oOrderNew, i)
                        i = i + 1
                    Next
                    oOrderNew.ShipToID = -1
                    oOrderNew.ShipToID = User1.PersonID
                    For r As Integer = 0 To i - 1
                        ShoppingCart1.AddToCart(ProdData(r, 0), Me.User1.PersonID, False, , ProdData(r, 1))
                    Next
                    ShoppingCart1.SaveCart(Page.Session)
                    Session("IsFromLogin") = "NO"
                End If

                ShoppingCart1.SaveCart(Session)
                ShoppingCart1.FillCart(dt)
                grdMain.DataSource = dt
                grdMain.DataBind()
                grdMain.AllowPaging = False
                ViewState("dtCart") = dt
                'Anil B Issue 15441 on 13 May 2013
                'Commented below code for performance

                'Aparna for issue 9142.update code for showing meeting related message dynamically.
                'For k = 0 To grdMain.Items.Count - 1
                '    If (ShoppingCart1.CheckProductType(k)) Then
                '        For j = 0 To grdMain.Items.Count - 1
                '            ItemList = ShoppingCart1.CheckSessionAvailable(k)
                '            For i = 0 To ItemList.Count - 1
                '                lblshowmessage.Visible = True
                '                imgwarning.Visible = True
                '            Next
                '        Next
                '    End If
                'Next

                'Anil B Issue 15441 on 13 May 2013
                'Updated this code with above commented code for meeting registration performance
                If IsMeetingProduct() Then
                    If IsMeetingSession() Then
                        lblshowmessage.Visible = True
                        imgwarning.Visible = True
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Anil B Issue 15441 on 13 May 2013
        'Check wether product on shopping cart is a Meeting product
        Private Function IsMeetingProduct() As Boolean
            Dim lblProductType As Label
            Try
                For Each row As GridDataItem In grdMain.Items
                    lblProductType = DirectCast(row.FindControl("lblProductType"), Label)
                    If lblProductType.Text.Trim = "Meeting" Then
                        Return True
                    End If
                Next
                Return False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
        'Anil B Issue 15441 on 13 May 2013
        'Check wether product on shopping cart is a Meeting product as well Session of meeting product is available on shopping cart
        Private Function IsMeetingSession() As Boolean
            Dim lblProductID As Label
            Dim lblProductType As Label
            Dim lblParentProductID As Label
            Dim row As GridDataItem
            Dim itemrow As GridDataItem

            Try
                For Each row In grdMain.Items
                    lblProductID = DirectCast(row.FindControl("lblProductID"), Label)
                    lblProductType = DirectCast(row.FindControl("lblProductType"), Label)
                    If lblProductType.Text.Trim = "Meeting" Then
                        For Each itemrow In grdMain.Items
                            lblParentProductID = DirectCast(itemrow.FindControl("lblProductParentID"), Label)
                            If lblParentProductID.Text <> String.Empty AndAlso lblParentProductID.Text <> "" Then
                                If lblProductID.Text.Trim = lblParentProductID.Text.Trim Then
                                    Return True
                                End If
                            End If
                        Next
                    End If
                Next

                Return False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        'Navin Prasad Issue 11032
        Public ReadOnly Property Grid() As RadGrid
            Get
                Return grdMain
            End Get
        End Property
        Public ReadOnly Property Cart() As Aptify.Framework.Web.eBusiness.AptifyShoppingCart
            Get
                Return ShoppingCart1
            End Get
        End Property

        Private Property ShoppingCart() As Aptify.Framework.Web.eBusiness.AptifyShoppingCart
            Get
                If Context.Items("ShoppingCartCtrl") IsNot Nothing Then
                    Return TryCast(Context.Items("ShoppingCartCtrl"), Aptify.Framework.Web.eBusiness.AptifyShoppingCart)
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal value As Aptify.Framework.Web.eBusiness.AptifyShoppingCart)
                Context.Items("ShoppingCartCtrl") = value
            End Set
        End Property

        Public Sub UpdateCart()
            Dim i As Integer, iOffset As Integer = 0
            Try
                lblError.Visible = False
                Dim oOrder As AptifyGenericEntityBase
                oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                'Navin Prasad Issue 11032
                For i = 0 To grdMain.Items.Count - 1
                    ' Obtain references to row's controls
                    Dim txtQuantity As TextBox = CType(grdMain.Items(i).FindControl("txtQuantity"), TextBox)
                    Dim chkRemove As CheckBox = CType(grdMain.Items(i).FindControl("chkRemove"), CheckBox)
                    Dim chkAutoRenewal As CheckBox = CType(grdMain.Items(i).FindControl("chkRenew"), CheckBox)
                    ' Wrap in try/catch block to catch errors in the event that someone types in
                    ' an invalid value for quantity
                    Dim dQty As Decimal
                    Try
                        Dim lblProductID As Label
                        dQty = CDec(txtQuantity.Text)
                        lblProductID = CType(grdMain.Items(i).FindControl("lblProductID"), Label)

                        'Anil Issue 14409
                        If chkRemove.Checked = True Then

                            AddGMTRemoveFromCart(oOrder, i - iOffset)

                            ShoppingCart1.RemoveItem(oOrder, i - iOffset)
                            'HP Issue#9144: updated the Offset removal based on the total actually removed provided by new property in ShoppingCart
                            'iOffset = iOffset + 1 ' offset tracks how many
                            iOffset = iOffset + ShoppingCart1.TotalItemsRemovedByRemoveItem
                            ' items removed
                        Else
                            'HP Issue#9144: only update those rows which are not part of a kit and therefore their checkboxes visible
                            'ShoppingCart1.UpdateQuantity(i, dQty)
                            If Not chkRemove.Visible = False Then
                                ShoppingCart1.UpdateQuantity(i - iOffset, dQty)
                            End If
                            If oOrder IsNot Nothing AndAlso oOrder.SubTypes("OrderLines") IsNot Nothing Then
                                With oOrder.SubTypes("OrderLines").Item(i)
                                    .SetValue("AutoRenew", chkAutoRenewal.Checked)
                                End With
                            End If
                        End If



                    Catch
                        lblError.Visible = True
                        lblError.Text = "There has been a problem with one or more of your inputs."
                    End Try
                Next
                ' update campaign code if necessary
                If ShoppingCart1.CampaignCodeID > 0 Then
                    ShoppingCart1.SetOrderCampaign(ShoppingCart1.CampaignCodeName, Page.Session)
                End If

                Me.ShoppingCart = ShoppingCart1
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub AddGMTRemoveFromCart(oOrder As Aptify.Applications.OrderEntry.OrdersEntity, index As Integer)
            Dim orderLine As Aptify.Consulting.Entity.OrderLines.OrderLinesEntity__c,
                impressionDto As ImpressionDto,
                removedFromShoppingCart As RemovedFromShoppingCart,
                currencyType As String = "EUR"

            Dim User1 As User = ShoppingCart1.User

            If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                currencyType = "EUR"
            ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                currencyType = "GBP"
            End If


            impressionDto = New ImpressionDto() With {
                .Currency = currencyType
            }

            If Not orderLine Is Nothing Then

                impressionDto.Products.Add(New ProductDto With {
                                          .Id = orderLine.ProductID,
                                          .Name = orderLine.Product,
                                          .Price = orderLine.Price,
                                          .Brand = "Chartered Accountants",
                                          .Category = "",
                                          .List = orderLine.ProductType,
                                          .Position = 1,
                                          .Quantity = orderLine.Quantity,
                                          .Variant = ""
                                          })
            End If

            removedFromShoppingCart = New RemovedFromShoppingCart(Me.Page, impressionDto)
            removedFromShoppingCart.RenderAsync(Me.UpdatePanel1)

        End Sub

        Public Function SaveShoppingCart(Optional ByVal Name As String = "",
                                             Optional ByVal Description As String = "",
                                             Optional ByVal lCartID As Long = 0) As Long
            Return ShoppingCart1.SaveShoppingCart(Name, Description, lCartID)
        End Function

        Public Sub LoadCart(ByVal lCartID As Long)
            ShoppingCart1.LoadShoppingCart(lCartID)
        End Sub
        Public Sub SaveCart()
            ShoppingCart1.SaveCart(Me.Session)
        End Sub
        'Navin Prasad Issue 11032

        'Private Sub grdMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdMain.ItemCommand
        '    Dim iLine As Integer, lProductID As Long
        '    Dim sPage As String = ""
        '    Dim oOrder As AptifyGenericEntityBase
        '    iLine = e.Item.ItemIndex
        '    oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
        '    lProductID = CLng(oOrder.SubTypes("OrderLines").Item(iLine).GetValue("ProductID"))

        '    ' redirect to the appropriate page based on the product types
        '    ' table
        '    ShoppingCart1.GetProductTypeWebPages(lProductID, "", sPage)
        '    If Len(sPage) > 0 Then
        '        sPage = sPage & "?OL=" & iLine
        '        Response.Redirect(sPage)
        '    End If
        'End Sub
        'Navin Prasad Issue 11032

        'HP Issue#8621:  examine each orderline in order to properly set product links
        'Protected Sub grdMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdMain.ItemDataBound

        '    If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
        '        DirectCast(e.Item.FindControl("link"), HyperLink).NavigateUrl = _
        '        SetURLPerProductType(ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application), e.Item.ItemIndex)
        '    End If
        'End Sub

        'HP Issue#8621:  if the product type is of 'Class' then set the product link within the cart to open the appropriate class associated with the product
        Private Function SetURLPerProductType(ByVal orderGE As Aptify.Applications.OrderEntry.OrdersEntity, ByVal orderLine As Integer) As String
            Dim url As String = String.Empty
            Dim classId As Integer
            'Dim sql As String
            Dim prodId = CLng(orderGE.SubTypes("OrderLines").Item(orderLine).GetValue("ProductID"))

            If String.Compare(ShoppingCart1.GetProductType(prodId), "Class", True) = 0 Then

                Dim ole As Aptify.Applications.OrderEntry.OrderLinesEntity = orderGE.SubTypes("OrderLines")(orderLine)
                'load class information from object data
                ole.ExtendedOrderDetailEntity.Load("|" & CStr(ole.GetValue("__ExtendedAttributeObjectData")))
                classId = CInt(ole.ExtendedOrderDetailEntity.GetValue("ClassID"))

                If classId > 0 Then
                    url = ClassViewPage & "?ClassID=" & classId
                End If
            Else
                url = ProductViewPage & "?ID=" & prodId
            End If

            Return url
        End Function
        'Navin Prasad Issue 11032
        Protected Sub grdMain_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMain.DataBound
            Dim rowcounter As Integer = 0
            Dim lblid As New Label
            Dim chkautorenew As New CheckBox
            Dim sSQL As String
            Dim dt As New DataTable
            Dim oOrder As AptifyGenericEntityBase
            oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
            For Each row As GridDataItem In grdMain.Items
                '' If (TypeOf (e.Item) Is GridDataItem) Then
                DirectCast(row.FindControl("link"), System.Web.UI.WebControls.HyperLink).NavigateUrl = _
            SetURLPerProductType(oOrder, rowcounter)
                'DirectCast(row.FindControl("cmdDetails"), LinkButton).CommandArgument = rowcounter
                'DirectCast(row.FindControl("hdnRowCount"), HiddenField).Value = rowcounter
                'Amruta Issue 14949 16/11/2012 
                Dim hlnkDetails As HyperLink = row.FindControl("hlnkDetails")
                Dim hdnRowCount As HiddenField = row.FindControl("hdnRowCount")

                If hlnkDetails IsNot Nothing AndAlso hdnRowCount IsNot Nothing Then
                    hdnRowCount.Value = rowcounter
                    hlnkDetails.NavigateUrl = ""

                    Dim iLine As Integer, lProductID As Long
                    Dim sPage As String = ""

                    iLine = Convert.ToInt32(hdnRowCount.Value)
                    lProductID = CLng(oOrder.SubTypes("OrderLines").Item(iLine).GetValue("ProductID"))

                    ' redirect to the appropriate page based on the product types
                    ' table
                    ShoppingCart1.GetProductTypeWebPages(lProductID, "", sPage)
                    If Len(sPage) > 0 Then

                        sPage = Me.FixLinkForVirtualPath(sPage) & "?OL=" & iLine

                        If String.IsNullOrEmpty(PageID) = False Then
                            sPage = sPage & "&PrevPage=" & PageID
                        End If

                        hlnkDetails.NavigateUrl = sPage

                        ' sPage = sPage & "?OL=" & iLine
                        'Response.Redirect(sPage)
                        ' Response.Write("<script language='javascript' type='text/javascript'>window.location.href ='" + sPage + "';</script>")

                    End If

                End If


                rowcounter = rowcounter + 1
                ''End If

                lblid = DirectCast(row.FindControl("lblProductID"), Label)
                lblid = DirectCast(row.FindControl("lblProductID"), Label)
                chkautorenew = DirectCast(row.FindControl("chkRenew"), CheckBox)
                sSQL = "SELECT * FROM " & _
                              AptifyApplication.GetEntityBaseDatabase("Products") & _
                              "..vwProducts WHERE ID=" & lblid.Text
                dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

                ''Nalini 
                If (dt.Rows.Count() > 0) Then
                    If dt.Rows(0)("IsSubscription").ToString() = "True" Then
                        DirectCast(row.FindControl("hlnkDetails"), HyperLink).Text = "Details..."
                        DirectCast(row.FindControl("hlnkDetails"), HyperLink).Visible = True
                        DirectCast(row.FindControl("hlnkDetails"), HyperLink).NavigateUrl = SubscriptionProductDetailsLinkCart + "?ID=" + lblid.Text.ToString() + "&Autorenew=" + chkautorenew.Checked.ToString() + "&Index=" + row.ItemIndex.ToString()

                        If String.IsNullOrEmpty(PageID) = False Then
                            DirectCast(row.FindControl("hlnkDetails"), HyperLink).NavigateUrl = DirectCast(row.FindControl("hlnkDetails"), HyperLink).NavigateUrl & "&PrevPage=" & PageID
                        End If

                        'Else
                        '    DirectCast(row.FindControl("hlnkDetails"), HyperLink).Visible = False
                    End If

                End If
                ' Added below code Govind Mande
                Dim lblProductID As Label = DirectCast(row.FindControl("lblProductID"), Label)
                Dim sSqlCheckProdCat As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductCategoryForTrainingTicket__c @ProductID=" & CLng(lblProductID.Text)
                Dim lProdCategoryID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSqlCheckProdCat, IAptifyDataAction.DSLCacheSetting.BypassCache))
                Dim txtQuantity As TextBox = DirectCast(row.FindControl("txtQuantity"), TextBox)
                If lProdCategoryID > 0 Then
                    txtQuantity.Enabled = False
                    txtQuantity.ToolTip = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.CheckOut.ToolTip")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                Else
                    txtQuantity.Enabled = True
                    txtQuantity.ToolTip = ""
                End If

		' Added below Sachin Sathe
                If dt.Rows(0)("Type").ToString() = "Meeting" Then
                    txtQuantity.Enabled = False
                Else
                    txtQuantity.Enabled = True
                End If
            Next
        End Sub


        ''' <summary>
        ''' Nalini 12436 date:1/12/2011
        ''' </summary>
        ''' <remarks></remarks>
        'Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        '    grdMain.PageIndex = e.NewPageIndex
        '    RefreshGrid()
        'End Sub
        'Navin Prasad Issue 11032
        'Nalini Nanda Issue 12436 :Adding the commandname condition Details
        'commented on 11/16/2012
        'Protected Sub grdMain_RowCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles grdMain.ItemCommand
        '    If e.CommandName = "Detail" Then
        '        Dim iLine As Integer, lProductID As Long
        '        Dim sPage As String = ""
        '        Dim oOrder As AptifyGenericEntityBase
        '        iLine = Convert.ToInt32(e.CommandArgument)
        '        oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
        '        lProductID = CLng(oOrder.SubTypes("OrderLines").Item(iLine).GetValue("ProductID"))

        '        ' redirect to the appropriate page based on the product types
        '        ' table
        '        ShoppingCart1.GetProductTypeWebPages(lProductID, "", sPage)
        '        If Len(sPage) > 0 Then

        '            sPage = Me.FixLinkForVirtualPath(sPage) & "?OL=" & iLine
        '            ' sPage = sPage & "?OL=" & iLine
        '            Response.Redirect(sPage)
        '            ' Response.Write("<script language='javascript' type='text/javascript'>window.location.href ='" + sPage + "';</script>")



        '        End If
        '    End If
        'End Sub
        Protected Sub chkRemove_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            'Aparna issue no. 9142 Adding function for check all session checkbox if there related Meeting checked for remove from shoping cart

            Dim currindex As Integer
            Dim ItemList As New ArrayList()
            Dim chkRemove As CheckBox = CType(sender, CheckBox)
            Dim gr1 As GridDataItem = CType(chkRemove.NamingContainer, GridDataItem)
            Dim lblProductType As Label
            Dim lblProductParentID As Label
            Dim strProductID As String = String.Empty
            Dim strAttendeeId As String = String.Empty
            Dim strCAttendee As String
            'Dim e_orderDetail As ExtendedOrderDetailGE
            If (chkRemove.Checked = True) Then
                currindex = gr1.DataSetIndex
            Else
                currindex = gr1.DataSetIndex
            End If

            If DirectCast(gr1.FindControl("lblProductType"), Label) IsNot Nothing And DirectCast(gr1.FindControl("lblProductType"), Label).Text = "Meeting" Then
                lblProductParentID = DirectCast(gr1.FindControl("lblProductParentID"), Label)
                strProductID = DirectCast(gr1.FindControl("lblProductID"), Label).Text
                strAttendeeId = DirectCast(gr1.FindControl("hdAttendeeID"), HiddenField).Value.ToString()
                strAttendeeId = GetAttendeeID(strAttendeeId)
                If lblProductParentID.Text = String.Empty Then
                    For i As Integer = currindex + 1 To grdMain.Items.Count - 1
                        If DirectCast(grdMain.Items(i).FindControl("hdAttendeeID"), HiddenField).Value.ToString() IsNot String.Empty Then
                            If strProductID = DirectCast(grdMain.Items(i).FindControl("lblProductParentID"), Label).Text And DirectCast(grdMain.Items(i).FindControl("hdAttendeeID"), HiddenField).Value.ToString() <> String.Empty And strAttendeeId = GetAttendeeID(DirectCast(grdMain.Items(i).FindControl("hdAttendeeID"), HiddenField).Value.ToString()) Then
                                DirectCast(grdMain.Items(i).FindControl("chkRemove"), CheckBox).Checked = chkRemove.Checked
                                DirectCast(grdMain.Items(i).FindControl("chkRemove"), CheckBox).Enabled = Not (chkRemove.Checked)
                            Else
                                Exit For
                            End If
                        End If

                    Next
                End If
            End If

        End Sub

        Private Function GetAttendeeID(ByVal strAttendeeID As String) As String
            If strAttendeeID IsNot String.Empty And strAttendeeID.Contains("<") Then
                strAttendeeID = strAttendeeID.Substring(strAttendeeID.IndexOf("AttendeeID") + 12)
                strAttendeeID = strAttendeeID.Remove(strAttendeeID.IndexOf("<"))
            End If
            Return strAttendeeID
        End Function

        'Protected Sub grdMain_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemCreated
        '    Dim hlnkDetails As HyperLink = e.Item.FindControl("hlnkDetails")
        '    Dim hdnRowCount As HiddenField = e.Item.FindControl("hdnRowCount")

        '    If hlnkDetails IsNot Nothing AndAlso hdnRowCount IsNot Nothing Then
        '        hlnkDetails.NavigateUrl = ""

        '        Dim iLine As Integer, lProductID As Long
        '        Dim sPage As String = ""
        '        Dim oOrder As AptifyGenericEntityBase
        '        iLine = Convert.ToInt32(hdnRowCount.Value)
        '        oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
        '        lProductID = CLng(oOrder.SubTypes("OrderLines").Item(iLine).GetValue("ProductID"))

        '        ' redirect to the appropriate page based on the product types
        '        ' table
        '        ShoppingCart1.GetProductTypeWebPages(lProductID, "", sPage)
        '        If Len(sPage) > 0 Then

        '            sPage = Me.FixLinkForVirtualPath(sPage) & "?OL=" & iLine
        '            hlnkDetails.NavigateUrl = sPage

        '            ' sPage = sPage & "?OL=" & iLine
        '            'Response.Redirect(sPage)
        '            ' Response.Write("<script language='javascript' type='text/javascript'>window.location.href ='" + sPage + "';</script>")



        '        End If

        '    End If
        'End Sub

        Protected Sub grdMain_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles grdMain.ItemDataBound
            If TypeOf (e.Item) Is GridDataItem Then
                Dim chkautorenew As New CheckBox
                chkautorenew = CType(e.Item.FindControl("chkRenew"), CheckBox)
                Dim sSQL As String
                Dim dt As New DataTable
                sSQL = "SELECT * FROM " & _
                              AptifyApplication.GetEntityBaseDatabase("Products") & _
                              "..vwProducts WHERE ID=" & DataBinder.Eval(e.Item.DataItem, "ProductID")
                dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

                ''Nalini 
                If (dt.Rows.Count() > 0) Then
                    If dt.Rows(0)("IsSubscription").ToString() = "True" Then
                        chkautorenew.Enabled = True
                    Else
                        chkautorenew.Enabled = False
                    End If

                End If

                If (DataBinder.Eval(e.Item.DataItem, "AutoRenew").ToString() = "1") Then
                    chkautorenew.Checked = True
                Else
                    chkautorenew.Checked = False
                End If
            End If
        End Sub
        'Neha, issue 14456, 03/15/13,for databinding
        Protected Sub grdMain_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
            ''RefreshGrid()

            If ViewState("dtCart") IsNot Nothing Then
                grdMain.DataSource = CType(ViewState("dtCart"), DataTable)
            End If
        End Sub
    End Class
End Namespace
