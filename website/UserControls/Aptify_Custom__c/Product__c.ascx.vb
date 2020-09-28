'Aptify e-Business 5.5.1, July 2013
''Modified by asmita ghodke  On 4-sep-2013  for terms and conditions check page load and lnkaddtocart click event on product requirement 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date Created/Modified       Summary
'Rajesh Kardile           03/20/2014                      Display Web Description according to currency type
'Rajesh Kardile           04/19/2014                      Display Attachment Records.
'Rajesh Kardile           07/09/2014                      Replace Hard code "Pound" value from entity attribute
'
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Option Explicit On
Option Strict On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Applications.OrderEntry
Imports System.Data
Imports Telerik.Web.UI
Imports SoftwareDesign.GTM
Imports SoftwareDesign.GTM.Model

Namespace Aptify.Framework.Web.eBusiness.ProductCatalog
    Partial Public Class Product__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_PRODUCT_CATEGORY_LINK_STRING_NON_ROOT_PAGE As String = "ProductCategoryLinkStringNonRootPage"
        Protected Const ATTRIBUTE_PRODUCT_CATEGORY_LINK_STRING_ROOT_PAGE As String = "ProductCategoryLinkStringRootPage"
        Protected Const ATTRIBUTE_PRODUCT_GROUPING_CONTENTS_VIEW_PRODUCT_PAGE As String = "GroupingContentsViewProductPage"
        Protected Const ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL As String = "ImageNotAvailable"
        Protected Const ATTRIBUTE_ITEM_NOT_AVAILABLE_IMAGE_URL As String = "ItemNotAvailableImage"
        Protected Const ATTRIBUTE_ADD_TO_CART_IMAGE_URL As String = "AddToCartImage"
        Protected Const ATTRIBUTE_VIEW_CART_IMAGE_URL As String = "ViewmycartImage" 'RashmiP, Issue 9989, 09/15/10
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "Product"
        Protected Const ATTRIBUTE_VIEW_CART_PAGE As String = "ViewcartPage"
        Protected Const ATTRIBUTE_VIEW_CLASS_PAGE As String = "ViewClassPage" 'RashmiP, Issue 11290 [Meeting/Class Integration]
        Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage"
        Private objOrder As OrdersEntity
        Private isAutoRenew As Boolean
        Dim CampaignListID As Long
        Dim _foundProduct As DataRow = Nothing
        Dim dtProduct As DataTable = Nothing
        Protected sSQL As String


#Region "Product Specific Properties"
        ''' <summary>
        ''' ProductCategoryLinkStringNonRoot page url
        ''' </summary>
        Public Overridable Property ProductCategoryLinkStringNonRootPage() As String
            Get
                If Not ViewState(ATTRIBUTE_PRODUCT_CATEGORY_LINK_STRING_NON_ROOT_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PRODUCT_CATEGORY_LINK_STRING_NON_ROOT_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PRODUCT_CATEGORY_LINK_STRING_NON_ROOT_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ProductCategoryLinkStringRoot page url
        ''' </summary>
        Public Overridable Property ProductCategoryLinkStringRootPage() As String
            Get
                If Not ViewState(ATTRIBUTE_PRODUCT_CATEGORY_LINK_STRING_ROOT_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PRODUCT_CATEGORY_LINK_STRING_ROOT_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PRODUCT_CATEGORY_LINK_STRING_ROOT_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' GroupingContentsViewProduct page url
        ''' </summary>
        Public Overridable Property GroupingContentsViewProductPage() As String
            Get
                If Not ViewState(ATTRIBUTE_PRODUCT_GROUPING_CONTENTS_VIEW_PRODUCT_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PRODUCT_GROUPING_CONTENTS_VIEW_PRODUCT_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PRODUCT_GROUPING_CONTENTS_VIEW_PRODUCT_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ImageNotAvailable url
        ''' </summary>
        Public Overridable Property ImageNotAvailable() As String
            Get
                If Not ViewState(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ItemNotAvailableImage url
        ''' </summary>
        Public Overridable Property ItemNotAvailableImage() As String
            Get
                If Not ViewState(ATTRIBUTE_ITEM_NOT_AVAILABLE_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ITEM_NOT_AVAILABLE_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ITEM_NOT_AVAILABLE_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' AddToCartImage url
        ''' </summary>
        Public Overridable Property AddToCartImage() As String
            Get
                If Not ViewState(ATTRIBUTE_ADD_TO_CART_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ADD_TO_CART_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ADD_TO_CART_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' RashmiP, Issue 9989, 09/15/10
        ''' ViewCart Image url
        ''' </summary>
        Public Overridable Property ViewmycartImage() As String
            Get
                If Not ViewState(ATTRIBUTE_VIEW_CART_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_VIEW_CART_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_VIEW_CART_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property TotalCartQty() As Integer
            Get
                If Context.Items("TotalCartQty") IsNot Nothing Then
                    Return Convert.ToInt32(Context.Items("TotalCartQty"))
                Else
                    Return 0
                End If

            End Get
            Set(ByVal value As Integer)
                Context.Items("TotalCartQty") = value
            End Set
        End Property

        Private Property ProductID() As Long
            Get
                If ViewState.Item("ProductID") IsNot Nothing Then
                    Return CLng(ViewState.Item("ProductID"))
                Else
                    Return -1
                End If
            End Get
            Set(ByVal value As Long)
                ViewState.Item("ProductID") = value
            End Set
        End Property

        Private Property NewProductVersionID() As Long
            Get
                If Not ViewState.Item("NewProductVersionID") Is Nothing Then
                    Return CLng(ViewState.Item("NewProductVersionID"))
                Else
                    Return -1
                End If
            End Get
            Set(ByVal value As Long)
                ViewState.Item("NewProductVersionID") = value
            End Set
        End Property

        Public Overridable Property ViewcartPage() As String
            Get
                If Not ViewState(ATTRIBUTE_VIEW_CART_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_VIEW_CART_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_VIEW_CART_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''RashmiP, Issue 11290 [Meeting/Class Integration]
        Public Overridable Property ViewClassPage() As String
            Get
                If Not ViewState(ATTRIBUTE_VIEW_CLASS_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_VIEW_CLASS_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_VIEW_CLASS_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' Login page url
        ''' </summary>
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
            If String.IsNullOrEmpty(LoginPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_LOGIN_PAGE)
            End If
            If String.IsNullOrEmpty(ProductCategoryLinkStringRootPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ProductCategoryLinkStringRootPage = Me.GetLinkValueFromXML(ATTRIBUTE_PRODUCT_CATEGORY_LINK_STRING_ROOT_PAGE)
                If String.IsNullOrEmpty(ProductCategoryLinkStringRootPage) Then
                    ProductCategoryLinkString1.Enabled = False
                    Me.ProductCategoryLinkString1.ToolTip = "ProductCategoryLinkStringRootPage property has not been set."
                Else
                    Me.ProductCategoryLinkString1.RootCategoryURL = ProductCategoryLinkStringRootPage
                End If
            Else
                Me.ProductCategoryLinkString1.RootCategoryURL = ProductCategoryLinkStringRootPage
            End If
            If String.IsNullOrEmpty(ProductCategoryLinkStringNonRootPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ProductCategoryLinkStringNonRootPage = Me.GetLinkValueFromXML(ATTRIBUTE_PRODUCT_CATEGORY_LINK_STRING_NON_ROOT_PAGE)
            End If
            If String.IsNullOrEmpty(GroupingContentsViewProductPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                GroupingContentsViewProductPage = Me.GetLinkValueFromXML(ATTRIBUTE_PRODUCT_GROUPING_CONTENTS_VIEW_PRODUCT_PAGE)
            End If
            If String.IsNullOrEmpty(ImageNotAvailable) Then
                'since value is the 'default' check the XML file for possible custom setting
                ImageNotAvailable = Me.GetLinkValueFromXML(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL)
            End If
            If String.IsNullOrEmpty(ItemNotAvailableImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ItemNotAvailableImage = Me.GetLinkValueFromXML(ATTRIBUTE_ITEM_NOT_AVAILABLE_IMAGE_URL)
                imgNotAvailable.Src = ItemNotAvailableImage
            End If
            If String.IsNullOrEmpty(AddToCartImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                AddToCartImage = Me.GetLinkValueFromXML(ATTRIBUTE_ADD_TO_CART_IMAGE_URL)
                'imgAddToCart.Src = AddToCartImage
            End If
            'if values are not provide directly or from the XML file, set default values for inherited properties since
            'control requires them to properly function
            If String.IsNullOrEmpty(Me.QueryStringRecordIDParameter) Then Me.QueryStringRecordIDParameter = "ID"

            'RashmiP, Issue 9989, 09/15/10
            If String.IsNullOrEmpty(ViewmycartImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ViewmycartImage = Me.GetLinkValueFromXML(ATTRIBUTE_VIEW_CART_IMAGE_URL)
                'Nalini Issue 12734
                'imgViewCart.Src = ViewmycartImage
            End If
            If String.IsNullOrEmpty(ViewcartPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ViewcartPage = Me.GetLinkValueFromXML(ATTRIBUTE_VIEW_CART_PAGE)
            End If
            ''RashmiP, Issue 11290 [Meeting/Class Integration]
            If String.IsNullOrEmpty(ViewClassPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ViewClassPage = Me.GetLinkValueFromXML(ATTRIBUTE_VIEW_CLASS_PAGE)
            End If
            'RashmiP issue 9990, 09/24/10
            Dim oOrder As OrdersEntity
            oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
            If oOrder IsNot Nothing Then
                If oOrder.SubTypes("OrderLines").Count > 0 Then
                    If Not String.IsNullOrEmpty(Request.QueryString("Val2")) Then
                        lnkViewCart.Visible = True
                        lblAdded.Visible = True
                        lblAdded.Text = Request.QueryString("Val2")
                        lblAdded.ForeColor = Drawing.Color.Blue
                        lblAdded.Attributes.Add("class", "product_added")
                    Else
                        lnkViewCart.Visible = False
                        lblAdded.Visible = False
                    End If
                Else
                    lblAdded.Visible = False
                    lnkViewCart.Visible = False
                End If
            Else
                lblAdded.Visible = False
                lnkViewCart.Visible = False
            End If


            'Added by dipali 11/04/2017 to change inline to sp
            sSQL = Me.Database & "..spGetProductDetailsOnProductPage__c"

            If Me.SetControlRecordIDFromParam() Then
                Me.ProductID = Me.ControlRecordID
            End If

            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@ProductID", Me.ProductID)

            If dtProduct Is Nothing Then
                dtProduct = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
            ElseIf Not (dtProduct Is Nothing) And dtProduct.Rows.Count() = 0 Then
                dtProduct = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
            End If

            If _foundProduct Is Nothing And dtProduct.Rows().Count() > 0 Then
                _foundProduct = dtProduct.Rows(0)
            Else
                LoadWithNotFoundLayoutSetup()
                Num(Request.QueryString("ID").ToString())
                'Throw New HttpException(404, "The requested product " & Request.QueryString("ID") & " was not found.")
            End If

        End Sub

        Protected Overridable Sub SetDetailVisible(ByVal bFlag As Boolean)
            productdetailpanel.Visible = bFlag
            lblSummary.Visible = bFlag
            lblDescription.Visible = bFlag
            lblprodDesc.Visible = bFlag
            lblLongDescription.Visible = bFlag
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try

                'set control properties from XML file if needed
                SetProperties()

                If Not IsPostBack Then
                    'RashmiP, Issue 9511, 09/08/10
                    AddHandler lnkAddToCart.Click, AddressOf lnkAddToCart_ServerClick

                    If Me.SetControlRecordIDFromParam() Then

                        ''Paresh Performance
                        hdnPerson.Value = CStr(User1.PersonID)
                        hdnProduct.Value = CStr(Me.ProductID)
                        Session("ShoppingCart") = ShoppingCart1
                        Session("PageUser") = Me.Page.User

                        'Commented by Dipali 09/08/10
                        'sSQL = "SELECT * FROM " & _
                        '       AptifyApplication.GetEntityBaseDatabase("Products") & _
                        '       "..vwProducts WHERE ID=" & Me.ProductID
                        'dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

                        ''Nalini 
                        If (dtProduct.Rows.Count() > 0) Then

                            If CheckProductAndRedirect(dtProduct) Then
                                Return
                            End If

                            'If _foundProduct("IsSubscription").ToString() = "True" Then
                            '    
                            '    lblChkAutoRenew.Visible = True
                            '    lblChkAutoRenew.Visible = True
                            'Else
                            '    ChkAutoRenew.Visible = False
                            '    lblChkAutoRenew.Visible = False
                            'End If
                            'Asmita Ghodke modification 

                            ''Added BY Pradip 2016-07-07 For Issue https://redmine.softwaredesign.ie/issues/14670
                            divISBN.Visible = False
                            'Added as part of log #20141
                            divEdition.Visible = False
                            divEducationalUse.Visible = False
                            divFormat.Visible = False
                            divCopyrightDate.Visible = False
                            divDatePublished.Visible = False
                            divKeywords.Visible = False
                            If _foundProduct("Type").ToString().Trim = "Publication" Then
                                ViewState("IsPublicationProduct") = 1
                                divISBN.Visible = True
                                divEdition.Visible = True
                                divEducationalUse.Visible = True
                                divFormat.Visible = True
                                divCopyrightDate.Visible = True
                                divDatePublished.Visible = True
                                divKeywords.Visible = True
                                'Added as part of #20442
                                divWeight.Visible = True
                                divDimension.Visible = True
                                sSQL = ""
                                sSQL = Database & "..spGetProductISBN__c @ProductID=" & Me.ProductID
                                'Dim param(0) As IDataParameter
                                'param(0) = DataAction.GetDataParameter("@ProductID", SqlDbType.Int, Me.ProductID)
                                Dim sISBN As String = Convert.ToString(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If Not String.IsNullOrEmpty(sISBN) Then
                                    lblISBN.Text = sISBN 'Convert.ToString(dtISBN.Rows(0)("ISBN")).Trim
                                Else
                                    lblISBN.Text = "--"
                                End If
                                'Format
                                Dim format As String = _foundProduct("Format").ToString().Trim()
                                If Not String.IsNullOrEmpty(format) Then
                                    lblFormat.Text = format
                                Else
                                    lblFormat.Text = "--"
                                End If
                                'Edition
                                Dim Edition As String = _foundProduct("Edition").ToString().Trim()
                                If Not String.IsNullOrEmpty(Edition) Then
                                    lblEdition.Text = Edition
                                Else
                                    lblEdition.Text = "--"
                                End If
                                'Educational Use
                                Dim EducationalUse As String = _foundProduct("EducationalUse").ToString().Trim()
                                If Not String.IsNullOrEmpty(EducationalUse) Then
                                    lblEducationalUse.Text = EducationalUse
                                Else
                                    lblEducationalUse.Text = "--"
                                End If
                                'CopyrightDate
                                Dim CopyrightDate As String = _foundProduct("DateCopyright").ToString().Trim()
                                If Not String.IsNullOrEmpty(CopyrightDate) Then
                                    lblCopyrightDate.Text = CopyrightDate
                                Else
                                    lblCopyrightDate.Text = "--"
                                End If
                                'PublishedDate
                                Dim PublishedDate As String = _foundProduct("DatePublished").ToString().Trim()
                                If Not String.IsNullOrEmpty(PublishedDate) Then
                                    lblDatePublished.Text = PublishedDate
                                Else
                                    lblDatePublished.Text = "--"
                                End If
                                'Keywords
                                Dim Keywords As String = _foundProduct("Keywords").ToString().Trim()
                                If Not String.IsNullOrEmpty(Keywords) Then
                                    lblKeywords.Text = Keywords
                                Else
                                    lblKeywords.Text = "--"
                                End If
                                'End
                                'Added for Log #20442
                                'Weight
                                Dim Weight As String = _foundProduct("Weight").ToString().Trim()
                                If Not String.IsNullOrEmpty(Weight) Then
                                    lblWeight.Text = Weight
                                Else
                                    lblWeight.Text = "--"
                                End If
                                'Dimension
                                Dim Dimension As String = _foundProduct("Dimension").ToString().Trim()
                                If Not String.IsNullOrEmpty(Dimension) Then
                                    lblDimension.Text = Dimension
                                Else
                                    lblDimension.Text = "--"
                                End If
                                'End of #20442
                            Else
                                divISBN.Visible = False
                                ViewState("IsPublicationProduct") = 0
                            End If

                            'Added as part of #20594
                            Dim CategoryWebName As String = _foundProduct("CategoryWebName").ToString().Trim()

                            If Not String.IsNullOrEmpty(CategoryWebName) Then
                                lblProductType.Text = CategoryWebName
                            Else
                                lblProductType.Text = "--"
                            End If

                            Dim sProdSQL As String = String.Empty
                            sProdSQL = Me.Database & "..spGetProdMultiCategoriesWithNoSubCategory__c @ProductID=" & Me.ControlRecordID
                            Dim sCategories As String = Convert.ToString(DataAction.ExecuteScalar(sProdSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If Not String.IsNullOrEmpty(sCategories) Then
                                lblCategory.Text = sCategories
                            Else
                                lblCategory.Text = "--"
                            End If
                            'End Of #20594
                            If _foundProduct("IsRequiredAgreement__c").ToString() = "True" Then
                                'TCLable.Visible = True
                                lblTicketCondtion.Visible = True
                                ChkRequiredAgreement.Visible = True
                                'Label1.Text = _foundProduct("TermsAndConditionWebDescription__c").ToString()


                                Dim termsId As Long = 0
                                Dim sSQL1 As String
                                If IsDBNull(_foundProduct("TermsAndConditionID__c")) = False Then
                                    termsId = Convert.ToInt32(_foundProduct("TermsAndConditionID__c"))
                                    ''Paresh Performance -- selecting WebDescription instead of *
                                    sSQL1 = "SELECT WebDescription FROM " &
                                    AptifyApplication.GetEntityBaseDatabase("TermsAndConditions__c") &
                                  "..vwTermsAndConditions__c WHERE ID=" & termsId
                                    lblTicketCondtion.Text = CStr(DataAction.ExecuteScalar(sSQL1, IAptifyDataAction.DSLCacheSetting.BypassCache)) ' Paresh Performance Changed datatable to scalar
                                End If

                            Else
                                'TCLable.Visible = False
                                lblTicketCondtion.Visible = False
                                ChkRequiredAgreement.Visible = False
                            End If

                            displayRecordAttachment(ProductID) 'Rajesh Kardile - 04/19/2014 - Display Attachment Records.

                            ''Paresh PErformance removed duplication condition of checking dt row  count

                            ViewState("AvailableForSale") = True

                            If CheckProduct(dtProduct) Then

                                LoadControlValues(_foundProduct)

                                'Load grid for product's subproducts
                                If CInt(_foundProduct.Item("ProductKitTypeID")) <> 1 Then
                                    ProductGroupingContentsGrid.ShoppingCart = ShoppingCart1
                                    ProductGroupingContentsGrid.NavigateURLFormatField = GroupingContentsViewProductPage & "?ID={0}"
                                    'Neha, Issue 14456,3/18/13 , set property true,declared on productgroupingcontentsgrid control for rad grid first column Ascending order sorting   
                                    ProductGroupingContentsGrid.CheckAddExpressionForProduct = True
                                    ProductGroupingContentsGrid.LoadGrid(Me.ProductID, CStr(_foundProduct.Item("WebName")))
                                    ProductGroupingContentsGrid.Visible = True
                                    'If CInt(_foundProduct.Item("ProductKitTypeID")) = 3 Then
                                    'Product Grouping is calculated differently (cumulative total of the product*qty)
                                    'so Get the price that was calculated from the Product Group listing

                                    'CLng(Request.QueryString("lID")

                                    ''Added By Pradip 2016-08-17 for Redmine issue https://redmine.softwaredesign.ie/issues/13990
                                    ' lblPrice.Text = ProductGroupingContentsGrid.FormattedGroupPrice
                                    If CInt(_foundProduct.Item("ProductKitTypeID")) = 3 Then
                                        'Product Grouping is calculated differently (cumulative total of the product*qty)
                                        'so Get the price that was calculated from the Product Group listing
                                        'lblPrice.Text = ProductGroupingContentsGrid.FormattedGroupPrice
                                        'Added as part of #20508
                                        If User1.UserID <= 0 Then
                                            lblMemberPrice.Visible = True
                                            'lblMemberPrice.Text = ProductGroupingContentsGrid.FormattedGroupMemberPrice
                                        End If
                                    End If
                                End If

                                'Aparna issue 9025,9042 for product detail and AddToCart button show for web enabled product
                                SetDetailVisible(True)

                                If Not (CBool(_foundProduct("TopLevelItem")) AndAlso CBool(_foundProduct("IsSold"))) Then
                                    LoadWithNotAvaliableSetup()
                                End If

                            Else
                                lblMsg.Text = "The requested product is not available on the web."
                            End If


                            '' Paresh Performance brought code from Finally here
                            ' Code added by govind mande on 30/4/2014 for displaying Training Ticket Points
                            GetTrainingPointsdetails(ProductID)
                            'code End by Govind

                            AddGoogleTagImpression()
                            AddGoogleTagAddToCartClick()
                        Else
                            LoadWithNotFoundLayoutSetup()
                            Num(Request.QueryString("ID").ToString())
                            'Throw New HttpException(404, "The requested product " & Request.QueryString("ID") & " was not found.")
                        End If
                    End If

                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                'ExceptionManagement.ExceptionManager.Publish(ex)
                'lblMsg.Text = "Error Loading Product ID: " & Request.QueryString("ID") & ". " & ex.Message
                ProductCategoryLinkString1.Visible = False
                'ProdListingGrid1.Visible = False
                'lblOtherProdsInCat.Visible = False
                'Me.table3.Visible = False
                Me.imgProduct.Visible = False
                'Me.ProductTopicCodesGrid1.Visible = False
                'Me.RelatedProductsGrid1.Visible = False
            Finally
                'Aparna issue 9025,9042 for product detail and AddToCart button hide for non-web enabled product

                '######EDUARDO 23-01-2020
                UnloadImage()

                ''---------------Added by Asmita 
                'If Not Request.QueryString("CampaignID") Is Nothing Then
                '    ' lblPrice.Text = Format(oPrice.Price, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                '    getProductCampaignApplied(ProductID)
                'End If
                ''-----------end 


            End Try

            Me.lblPrice.Visible = True
            Me.lblMemSavings.Visible = False

        End Sub

        Private Sub Num(ByVal value As String)

            Try
                value = value.Split(Char.Parse("'"))(0)
            Catch ex As Exception
                'value = Request.QueryString("ID").ToString().Split("'")(0)
            End Try

            Dim returnVal As String = String.Empty
            Dim collection As MatchCollection = Regex.Matches(value, "\d+")
            For Each m As Match In collection
                returnVal += m.ToString()
            Next
            Dim newval As Integer = 0
            Try
                Try
                    newval = Convert.ToInt32(returnVal)
                Catch ex As Exception
                    Throw New System.Exception("Url error: " & HttpContext.Current.Request.Url.AbsoluteUri & " \n IP Adrees:" & System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(1).ToString())
                End Try
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        ''' <summary>
        ''' if Training Ticket points > 0 then only display message
        ''' </summary>
        ''' <param name="ProductID"></param>
        ''' <remarks></remarks>
        Private Sub GetTrainingPointsdetails(ByVal ProductID As Long)
            Try
                Dim sTrainingPoint As String = Database & "..spGetProductsTrainingTicketPoints__c @ProductID=" & ProductID
                Dim dTrainingPoint As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sTrainingPoint, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If dTrainingPoint > 0 Then
                    lblTrainingPoints.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.TrainingTicketPointsMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & dTrainingPoint
                    trIdTrainingPoint.Visible = True
                Else
                    trIdTrainingPoint.Visible = False
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' This method is responsible for checking to see if the product is web enabled and also determining the
        ''' appropriate page url to use for the product. To determine the appropriate URL, a stored procedure is
        ''' called that returns either the appropriate URL to redirect to or a blank value. If a blank value is
        ''' returned that means this generic product page is to be used, otherwise the URL returned should be redirected
        ''' to for viewing the product.
        ''' </summary>
        ''' <param name="dtProduct"></param>
        ''' <remarks></remarks>
        Private Function CheckProduct(ByVal dtProduct As DataTable) As Boolean
            Dim sSQL As String
            Try
                With dtProduct.Rows(0)
                    If CBool(.Item("WebEnabled")) Then
                        Return True
                    Else
                        Return False
                    End If
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        Private Function CheckProductAndRedirect(ByVal dtProduct As DataTable) As Boolean
            Dim sURL As Object
            With dtProduct.Rows(0)
                If CBool(.Item("WebEnabled")) Then
                    sURL = dtProduct.Rows(0).Item("ProductURLToUse")

                    If Not IsDBNull(dtProduct.Rows(0).Item("ClassID")) Then
                        sURL = ViewClassPage & "?ClassID=" & CStr(dtProduct.Rows(0).Item("ClassID"))
                        Response.RedirectPermanent(CStr(sURL).Trim)
                        Me.Dispose()
                        Return True
                    End If

                    If Not sURL Is Nothing AndAlso Len(sURL) > 0 Then
                        Response.RedirectPermanent(CStr(sURL).Trim & Me.Request.Url.Query)
                        Me.Dispose()
                        Return True
                    End If
                    Return False
                End If
                Return False
            End With
        End Function

        Private Sub LoadControlValues(ByVal dr As DataRow)
            Dim sReason As String = ""
            Dim sWebErrorMsg As String = ""
            Try
                lblName.Text = CStr(dr.Item("WebName"))
                Page.Title = Convert.ToString(dr.Item("WebName"))    ' Swapnil 17/10/2016

                'Rajesh K -03/20/2014
                '*************************** Start *************************
                'If Not IsDBNull(dr("WebDescription")) Then
                '    lblDescription.Text = CStr(dr.Item("WebDescription"))
                'Else
                '    lblDescription.Text = ""
                'End If

                'If Not IsDBNull(dr("WebLongDescription")) Then
                '    lblLongDescription.Text = CStr(dr.Item("WebLongDescription"))
                'Else
                '    lblLongDescription.Text = ""
                'End If

                DisplayWebDescription(dr)

                '*************************** End ******************************
                If Not IsDBNull(dr("Code")) AndAlso CStr(dr("Code")) <> "" Then
                    lblCode.Text = CStr(dr.Item("Code"))
                Else
                    lblCode.Text = ""
                    trProductCode.Visible = False
                End If

                If Not IsDBNull(dr("WebImage")) AndAlso
                   Len(dr("WebImage")) > 0 Then
                    imgProduct.ImageUrl = CStr(dr.Item("WebImage"))
                Else
                    imgProduct.ImageUrl = ImageNotAvailable
                End If
                'imgProduct.Visible = Len(imgProduct.ImageUrl) > 0

                'Dim oPrice As IProductPrice.PriceInfo = Me.GetProductPrice(CLng(dr.Item("ID")))

                ''' Paresh <Performance> Moved to client side
                '''----By Vaishali
                ''If Request.QueryString.Count > 0 AndAlso Not String.IsNullOrEmpty(Request.QueryString("cID")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("PersonID")) Then
                ''    Dim dPrice As Decimal
                ''    Dim lCampaignID As Long = Convert.ToInt64(Request.QueryString("cID"))
                ''    Dim lPersonID As Long = Convert.ToInt64(Request.QueryString("PersonID"))
                ''    If User1.UserID > 0 Then
                ''        dPrice = GetProductPriceWithCampaign(ProductID, lCampaignID, User1.PersonID, User1.PersonID)
                ''    Else
                ''        dPrice = GetProductPriceWithCampaign(ProductID, lCampaignID, lPersonID, lPersonID)
                ''    End If
                ''    lblPrice.Text = Format(dPrice, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                ''Else
                ''    lblPrice.Text = Format(oPrice.Price, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                ''End If
                '''-----------end 

                '' lblPrice.Text = Format(oPrice.Price, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))

                ''Suvarna D IssueID-12720 to remove the label on Jan 19, 2012
                ''Suvarna Deshmukh IssueID-12433,12430 and 12434 On Dec 16,2011
                ''commented and added to assign and display value of label "Price For you"
                ''lblPriceForYouVal.Text = Format(oPrice.Price, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                '''End of addtion by Suvarna Deshmukh IssueID-12433,12430 and 12434 On Dec 16,2011
                ''End of addition by Suvarna D IssueID-12720 to remove the label on Jan 19, 2012

                ''Display Member Savings if > 0
                'If Me.User1.PersonID > 0 Then
                '    '20090126 MAS: using different logic for calculating Member Savings since the original way was not properly
                '    '              calculating complex pricing rules.
                '    '              NOTE: member savings can only be calculated for a User that is logged into the website, 
                '    '                    since pricing may be based on the User's address.
                '    'Dim dSavings As Decimal = Me.ShoppingCart1.GetProductMemberSavings(Me.Page.User, CLng(dr.Item("ID")))
                '    Dim dSavings As Decimal
                '    'Dim memCost As Decimal = ShoppingCart1.GetSingleProductMemberCost(Me.Page.User, CLng(dr.Item("ID")))
                '    Dim nonmemCost As Decimal = ShoppingCart1.GetSingleProductNonMemberCost(Me.Page.User, CLng(dr.Item("ID")))
                '    dSavings = nonmemCost - oPrice.Price
                '    'If dSavings > 0 Then
                '    '    Me.lblMemSavings.Text = "(" & Format$(dSavings, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                '    '    Me.lblMemSavings.Text &= " member savings)"
                '    '    Me.lblMemSavings.Visible = True
                '    'Else
                '    '    Me.lblMemSavings.Visible = False
                '    'End If
                '    If dSavings > 0 Then
                '        Me.lblPrice.Text = Format$(nonmemCost, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                '        Me.lblMemberPrice.Text = "(" & Format$(oPrice.Price, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                '        Me.lblMemberPrice.Text &= " Member Price)"
                '        Me.lblMemberPrice.Visible = True
                '    Else
                '        Me.lblMemSavings.Visible = False
                '        Me.lblMemberPrice.Visible = False
                '    End If
                'Else
                '    Me.lblPrice.Text = Format$(oPrice.Price, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                '    Me.lblPrice.Visible = True
                '    Me.lblMemSavings.Visible = False
                'End If

                If Not IsDBNull(dr("MinSellingUnits")) AndAlso
                   CLng(dr("MinSellingUnits")) > 1 Then
                    lblSellingUnits.Text = "Min. Order Qty: " & CLng(dr("MinSellingUnits"))
                    lblSellingUnits.Visible = True
                Else
                    lblSellingUnits.Visible = False
                End If


                If ProductAvailable(dr, sReason) Then
                    'HP - Default text is sufficient only need to make sure label is visble
                    'lblAvailable.Text = "Yes"
                    lblAvailable.Visible = True
                    'Suvarna IssueId 12720 Dispaly msg when product available
                    lblavailval.Text = "In stock"

                    lnkAddToCart.Visible = True
                    'lnkAddToCart.HRef = lnkAddToCart.HRef & "?ID=" & CLng(dr.Item("ID"))
                    'imgAddToCart.Visible = True
                    imgNotAvailable.Visible = False

                    ''RashmiP issue 9511, 11/1/2010, 
                    ' Check for Web Prerequisite Message before product added to cart. 
                    'Function return true if Web Pre-Rquisite error msg exist
                    If GetWebPrerequisiteMsg(dr, sWebErrorMsg) Then
                        lnkAddToCart.Visible = False
                        'imgAddToCart.Visible = False
                        lblAdded.ForeColor = Drawing.Color.Red
                        lblAdded.Visible = True
                        lblAdded.Text = sWebErrorMsg
                        lblAdded.Attributes.Add("class", "product_error")
                    Else
                        lblAvailable.Visible = True
                        lnkAddToCart.Visible = True
                        'imgAddToCart.Visible = True
                        'Suvarna IssueId 12720 Dispaly msg when product available
                        lblavailval.Text = "In stock"
                    End If
                Else
                    'HP - Label text is irrelevant since using an image indicating non-availability, make sure label is not visible
                    'lblAvailable.Text = "No"
                    lblAvailable.Visible = False
                    'suvarna Deshmukh IssueID-12433,12430 and 12434 On Dec 16,2011
                    'Code added to not to display the value of the label "Available" when product is not available.
                    lblavailval.Visible = False
                    'End by suvarna Deshmukh IssueID-12433,12430 and 12434 On Dec 16,2011
                    lnkAddToCart.Visible = False
                    'imgAddToCart.Visible = False
                    'Set to false for not to dispaly Image as per Rakhi's suggestion
                    imgNotAvailable.Visible = False
                    lblProductMessage.Text = sReason
                    lblProductMessage.Visible = True
                    If Not String.IsNullOrEmpty(sReason) Then
                        lblNote.Visible = True
                    End If
                    'Suvarna IssueId 12720 Dispaly msg when product available
                    lblavailval.Text = "Not in stock"
                    lblavailval.ForeColor = System.Drawing.Color.Red
                    imgNotAvailable.Visible = False
                    lblavailval.Visible = True
                    lblAvailable.Visible = True
                End If

                'Change for 17419 by Shweta on 29/05/2017
                'check to see if product category is District Society and user is Firm admin then hide Add To Cart

                Dim sSQlDistrictProductCategory As String = Database & "..spGetDistrictSocietyProductCategory__c @ProductID=" & CLng(dr("ID"))
                Dim iDistrictProductCategory As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlDistrictProductCategory, IAptifyDataAction.DSLCacheSetting.BypassCache))

                Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If iFirmAdmin > 0 AndAlso iDistrictProductCategory > 0 Then
                    lnkAddToCart.Visible = False
                End If

                'check to see if a newer version of this product can be offered
                Dim sSQL As String
                'Dim dt As DataTable
                Dim lNewerProductID As Long
                Dim oNewerProductID As Object
                Dim lCurrentProductID As Long = CLng(dr.Item("ID"))
                sSQL = "SELECT " & AptifyApplication.GetEntityBaseDatabase("Products") &
                ".dbo.fnGetLatestVersionProductID(" & lCurrentProductID & ")"
                oNewerProductID = DataAction.ExecuteScalar(sSQL)
                If IsNumeric(oNewerProductID) Then
                    lNewerProductID = CLng(oNewerProductID)
                Else
                    lNewerProductID = -1
                End If


                'display link to the latest valid version of this product if one exists
                If lNewerProductID > 0 AndAlso lNewerProductID <> lCurrentProductID Then

                    'Added by dipali 10/04/2017
                    'EDUARDO 23-01-2020 - REVOMED ALL
                    'sSQL = Me.Database & "..spGetNewProductDetails__c"
                    'Dim param(0) As IDataParameter

                    'param(0) = DataAction.GetDataParameter("@ProductID", SqlDbType.Int, Convert.ToInt32(Request.QueryString("ID")))
                    'dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)

                    'sSQL = "SELECT Name, WebName, WebEnabled FROM " & _
                    '        AptifyApplication.GetEntityBaseDatabase("Products") & _
                    '        ".dbo.vwProducts WHERE ID=" & lNewerProductID
                    'dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If CBool(_foundProduct.Item("WebEnabled")) Then
                        'a newer product has been found that can be linked to
                        If Not IsDBNull(_foundProduct.Item("WebName")) _
                           AndAlso CStr(_foundProduct.Item("WebName")) <> "" Then
                            btnNewVersion.Text = CStr(_foundProduct.Item("WebName"))
                        Else
                            btnNewVersion.Text = CStr(_foundProduct.Item("Name"))
                        End If
                        Me.NewProductVersionID = lNewerProductID
                        'lnkNewerProduct.PostBackUrl = "http://www.google.com" '../ProductCatalog/Product.aspx?ID=" & lNewerProductID
                        lblNewerProduct.Visible = True
                        btnNewVersion.Visible = True
                        'lnkNewerProduct.Visible = True
                    End If
                End If


                With Me.ProductCategoryLinkString1
                    .CategoryID = CLng(dr("CategoryID"))
                    If Not String.IsNullOrEmpty(ProductCategoryLinkStringNonRootPage) Then
                        .URL = ProductCategoryLinkStringNonRootPage
                    Else
                        .Enabled = False
                        .ToolTip = "ProductCategoryLinkStringNonRootPage property not set."
                    End If
                    .Separator = ":"
                    .HyperlinkLastCategory = True
                    .Visible = True
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Strips the HTML. Added By Swati for issue #16796 Product - Strip web description html tags
        ''' </summary>
        ''' <param name="HTMLText">The HTML text.</param>
        ''' <returns></returns>
        Public Shared Function GetBodyHTML(HTMLText As String) As String
            ' Dim reg = New Regex("<[^>]+>", RegexOptions.IgnoreCase)
            'Dim reg = New Regex("<[^>]+>", RegexOptions.IgnoreCase)
            'Return reg.Replace(HTMLText, "")

            Dim options As RegexOptions = RegexOptions.IgnoreCase Or RegexOptions.Singleline
            Dim regx As New Regex("<body>(?<theBody>.*)</body>", options)

            Dim match As Match = regx.Match(HTMLText)

            If match.Success Then
                Return match.Groups("theBody").Value
            End If
        End Function


        ''' <summary>
        ''' This will display web description as per the person preferred
        ''' </summary>
        ''' <param name="dr">The dr.</param>
        Sub DisplayWebDescription(ByVal dr As DataRow)
            If User1.UserID > 0 Then
                If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                    If Not IsDBNull(dr("WebDescription")) Then
                        ''Commented and added by pradip 2016-06-29 for Issue https://redmine.softwaredesign.ie/issues/14046
                        lblDescription.Text = CStr(dr.Item("WebDescription"))

                        'Commented and added by Swati for issue #16796 Product - Strip web description html tags
                        'If System.Text.RegularExpressions.Regex.Replace(lblDescription.Text, "<[^>]*>", "").Trim.Length > 350 Then
                        '    lblDescription.Text = System.Text.RegularExpressions.Regex.Replace(lblDescription.Text, "<[^>]*>", "").Trim.Substring(0, 350).Trim + "..."
                        'End If
                        If Not String.IsNullOrEmpty(lblDescription.Text) Then
                            If GetBodyHTML(lblDescription.Text).Trim.Length > 350 Then
                                lblDescription.Text = GetBodyHTML(lblDescription.Text).Trim.Substring(0, 350).Trim + "..."
                            Else
                                lblDescription.Text = GetBodyHTML(lblDescription.Text).Trim
                            End If
                        Else
                            lblDescription.Text = ""
                        End If

                    Else
                        lblDescription.Text = ""
                    End If
                    If Not IsDBNull(dr("WebLongDescription")) Then
                        ''Commented and added by pradip 2016-06-29 for Issue https://redmine.softwaredesign.ie/issues/14046
                        ' lblLongDescription.Text = CStr(dr.Item("WebLongDescription"))
                        lblLongDescription.Text = CStr(dr.Item("WebLongDescription"))
                        'If System.Text.RegularExpressions.Regex.Replace(lblLongDescription.Text, "<[^>]*>", "").Trim.Length > 350 Then
                        '    lblLongDescription.Text = System.Text.RegularExpressions.Regex.Replace(lblLongDescription.Text, "<[^>]*>", "").Trim.Substring(0, 350).Trim + "..."
                        'End If

                    Else
                        lblLongDescription.Text = ""
                    End If
                    'RajeshK -070914
                ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                    If Not IsDBNull(dr("WebDescription__c")) Then
                        ''Commented and added by pradip 2016-06-29 for Issue https://redmine.softwaredesign.ie/issues/14046
                        ' lblDescription.Text = CStr(dr.Item("WebDescription__c"))
                        lblDescription.Text = CStr(dr.Item("WebDescription__c"))
                        'Commented and added by Swati for issue #16796 Product - Strip web description html tags
                        'If System.Text.RegularExpressions.Regex.Replace(lblDescription.Text, "<[^>]*>", "").Trim.Length > 350 Then
                        '    lblDescription.Text = System.Text.RegularExpressions.Regex.Replace(lblDescription.Text, "<[^>]*>", "").Trim.Substring(0, 350).Trim + "..."
                        'End If
                        If Not String.IsNullOrEmpty(lblDescription.Text) Then
                            If GetBodyHTML(lblDescription.Text).Trim.Length > 350 Then
                                lblDescription.Text = GetBodyHTML(lblDescription.Text).Trim.Substring(0, 350).Trim + "..."
                            Else
                                lblDescription.Text = GetBodyHTML(lblDescription.Text).Trim
                            End If
                        Else
                            lblDescription.Text = ""
                        End If
                    Else
                        lblDescription.Text = ""
                    End If
                    If Not IsDBNull(dr("WebLongDescription__c")) Then
                        ''Commented and added by pradip 2016-06-29 for Issue https://redmine.softwaredesign.ie/issues/14046
                        ' lblLongDescription.Text = CStr(dr.Item("WebLongDescription__c"))
                        lblLongDescription.Text = CStr(dr.Item("WebLongDescription__c"))
                        'If System.Text.RegularExpressions.Regex.Replace(lblLongDescription.Text, "<[^>]*>", "").Trim.Length > 350 Then
                        '    lblLongDescription.Text = System.Text.RegularExpressions.Regex.Replace(lblLongDescription.Text, "<[^>]*>", "").Trim.Substring(0, 350).Trim + "..."
                        'End If

                    Else
                        lblLongDescription.Text = ""
                    End If
                Else
                    If Not IsDBNull(dr("WebDescription")) Then
                        ''Commented and added by pradip 2016-06-29 for Issue https://redmine.softwaredesign.ie/issues/14046
                        ' lblDescription.Text = CStr(dr.Item("WebDescription"))
                        lblDescription.Text = CStr(dr.Item("WebDescription"))

                        'Commented and added by Swati for issue #16796 Product - Strip web description html tags
                        'If System.Text.RegularExpressions.Regex.Replace(lblDescription.Text, "<[^>]*>", "").Trim.Length > 350 Then
                        '    lblDescription.Text = System.Text.RegularExpressions.Regex.Replace(lblDescription.Text, "<[^>]*>", "").Trim.Substring(0, 350).Trim + "..."
                        'End If

                        If Not String.IsNullOrEmpty(lblDescription.Text) Then
                            If GetBodyHTML(lblDescription.Text).Trim.Length > 350 Then
                                lblDescription.Text = GetBodyHTML(lblDescription.Text).Trim.Substring(0, 350).Trim + "..."
                            Else
                                lblDescription.Text = GetBodyHTML(lblDescription.Text).Trim
                            End If
                        Else
                            lblDescription.Text = ""
                        End If

                    Else
                        lblDescription.Text = ""
                    End If
                    If Not IsDBNull(dr("WebLongDescription")) Then
                        ''Commented and added by pradip 2016-06-29 for Issue https://redmine.softwaredesign.ie/issues/14046
                        'lblLongDescription.Text = CStr(dr.Item("WebLongDescription"))
                        lblLongDescription.Text = CStr(dr.Item("WebLongDescription"))
                        'If System.Text.RegularExpressions.Regex.Replace(lblLongDescription.Text, "<[^>]*>", "").Trim.Length > 350 Then
                        '    lblLongDescription.Text = System.Text.RegularExpressions.Regex.Replace(lblLongDescription.Text, "<[^>]*>", "").Trim.Substring(0, 350).Trim + "..."
                        'End If

                    Else
                        lblLongDescription.Text = ""
                    End If
                End If

            Else
                If Not IsDBNull(dr("WebDescription")) Then
                    lblDescription.Text = CStr(dr.Item("WebDescription"))

                    'Commented and added by Swati for issue #16796 Product - Strip web description html tags
                    'If System.Text.RegularExpressions.Regex.Replace(lblDescription.Text, "<[^>]*>", "").Trim.Length > 350 Then
                    '    lblDescription.Text = System.Text.RegularExpressions.Regex.Replace(lblDescription.Text, "<[^>]*>", "").Trim.Substring(0, 350).Trim + "..."
                    'End If
                    If Not String.IsNullOrEmpty(lblDescription.Text) Then
                        If GetBodyHTML(lblDescription.Text).Trim.Length > 350 Then
                            lblDescription.Text = GetBodyHTML(lblDescription.Text).Trim.Substring(0, 350).Trim + "..."
                        Else
                            lblDescription.Text = GetBodyHTML(lblDescription.Text).Trim
                        End If
                    Else
                        lblDescription.Text = ""
                    End If

                Else
                    lblDescription.Text = ""
                End If
                If Not IsDBNull(dr("WebLongDescription")) Then
                    lblLongDescription.Text = CStr(dr.Item("WebLongDescription"))
                    ' lblLongDescription.Text = System.Text.RegularExpressions.Regex.Replace(lblLongDescription.Text, "<[^>]*>", "").Trim.Substring(0, 350).Trim + "..."
                Else
                    lblLongDescription.Text = ""
                End If
            End If
        End Sub

        Private Function ProductAvailable(ByVal dr As DataRow,
                                          ByRef Reason As String) As Boolean
            Dim lProductID As Long

            Try
                ' Richard Bowman - 1/5/2004
                ' Added support for the business logic of the Housing Module.
                ' If a particular housing product has been added to an order,
                ' it does not make sense for another order line to be added
                ' to the order. If the housing product is already associated
                ' with the order, it will cause ProductAvailable to return
                ' false so it cannot be ordered again.

                ' Also added functionality to pass back a "reason" string,
                ' which offers additional explanation about the return value.
                If Not IsNumeric(Request.QueryString("ID")) Then
                    Throw New ArgumentException("Parameter must be numeric.", "ID")
                End If
                'lProductID = CLng(Request.QueryString("ID"))
                lProductID = CLng(Me.ProductID)
                'Dim oOrderGE As AptifyGenericEntityBase
                'Dim i As Integer
                'If String.Compare(ShoppingCart1.GetProductType(lProductID), "Housing", True) = 0 Then
                '    ' check to see if a housing product already exists in the cart
                '    oOrderGE = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                '    For i = 0 To oOrderGE.SubTypes("OrderLines").Count - 1
                '        ' search each order line
                '        If CLng(oOrderGE.SubTypes("OrderLines").Item(i).GetValue("ProductID")) = lProductID Then
                '            ' found the product already
                '            ' return false to specify the housing product
                '            ' is not available to add to the cart again
                '            Reason = "A housing product cannot be added to your cart multiple times. To make multiple reservations, use the Details button of the current Housing Registration from your cart to add additional registrations."
                '            Return False
                '        End If
                '    Next
                'End If

                '8/30/06 RJK - Temporarily make Housing Products unavailable.
                'This was done because there were issues with Housing purchases not
                'creating Housing Reservation Detail records.
                If String.Compare(ShoppingCart1.GetProductType(lProductID), "Housing", True) = 0 Then
                    Reason = "Housing Products are not supported in this build."
                    Return False
                End If

                '2006/12/14 MAS
                'Expanded the product availability logic
                'Properties checked to determine product availability:
                '1. Product currently sold (product.IsSold)
                '2. Date product is available (product.DateAvailable)
                '3. Date product is available until (product.AvailableUntil)
                '4. Product Inventory required (product.RequireInventory)
                '5. Product Quantity on Hand (product.QuantityOnHand)
                'IF all of the above conditions pass, then this product is available for purchase
                Dim bAvailable As Boolean = True
                Dim dToday As Date = Today()

                With dr
                    '1. Product currently sold (product.IsSold)
                    If Not CBool(.Item("IsSold")) Then
                        Reason = "This Product is Not Available."
                        bAvailable = False

                    Else
                        '2. Date product is available (product.DateAvailable)
                        If Not IsDBNull(.Item("DateAvailable")) _
                           AndAlso CStr(.Item("DateAvailable")) <> "" _
                           AndAlso CDate(.Item("DateAvailable")) > dToday Then
                            Reason = "This product is not availble until " &
                                             CDate(.Item("DateAvailable")).ToLongDateString & "."
                            bAvailable = False

                            '3. Date product is available until (product.AvailableUntil)
                        ElseIf Not IsDBNull(.Item("AvailableUntil")) _
                               AndAlso CStr(.Item("AvailableUntil")) <> "" _
                               AndAlso CDate(.Item("AvailableUntil")) < dToday _
                                AndAlso CDate(.Item("AvailableUntil")) <> CDate("1/1/1900") Then  ''RashmiP, Issue 14938
                            'Reason = "This product's availability ended on " & _
                            '                 CDate(.Item("DateAvailable")).ToLongDateString & "."
                            bAvailable = False

                            '4. Product Inventory required (product.RequireInventory)
                            '5. Product Quantity on Hand (product.QuantityOnHand)

                            'HP Issue#8283: make availability based on QuantityAvailable instead of QuantityOnHand
                            'ElseIf CBool(.Item("RequireInventory")) _
                            '       AndAlso Not IsDBNull(.Item("QuantityOnHand")) _
                            '       AndAlso CInt(.Item("QuantityOnHand")) < 1 Then
                            '    Reason = "No more units of this product are available."
                            '    bAvailable = False
                        ElseIf CBool(.Item("RequireInventory")) _
                          AndAlso Not IsDBNull(.Item("QuantityAvailable")) _
                          AndAlso CInt(.Item("QuantityAvailable")) < 1 Then
                            Reason = "No more units of this product are available."
                            bAvailable = False

                        Else
                            'Else this product is available
                            bAvailable = True
                        End If
                    End If
                End With
                Return bAvailable

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Reason = "An error occured." & vbNewLine & ex.Message
                Return False
            End Try
        End Function

        Public Function GetProductPrice(ByVal lProductID As Long) As IProductPrice.PriceInfo
            ' Implement This function
            Return ShoppingCart1.GetUserProductPrice(lProductID, 1)
        End Function
        Public Function GetNonMemberProductPrice() As Decimal
            ' Implement This function
            Return 0
        End Function

        ''RashmiP issue 9511,11/1/2010, to show web pre-requisite message before product added to cart.
        Private Function GetWebPrerequisiteMsg(ByVal dr As DataRow, ByRef sWebErrorMsg As String) As Boolean
            Dim oOrder As New Aptify.Applications.OrderEntry.OrdersEntity
            oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
            Dim lProductID As Long = CLng(Me.ProductID)
            Dim PrerequisitesOverridePromptResult As Microsoft.VisualBasic.MsgBoxResult
            Try
                If Not oOrder.ValidateProductPrerequisites(lProductID, CInt(dr.Item("QuantityAvailable")), PrerequisitesOverridePromptResult) Then
                    sWebErrorMsg = CStr(oOrder.WebProdPreRequisiteErrMsg)
                    Return True
                Else
                    sWebErrorMsg = Nothing
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Private Sub lnkAddToCart_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAddToCart.Click
            ' Code AddProductOnCart Sub routne created by Govind
            Try
                If Convert.ToBoolean(ViewState("IsPublicationProduct")) Then
                    WarningMessgaeOnTaxcession()
                Else

                    AddProductOnCart()
                    ' Code Copy on Add To Cart 
                End If

                ' WarningMessgaeOnTaxcession()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub AddProductOnCart()
            Dim sProductPage As String = ""
            Dim sOrderPage As String = ""
            Dim oOrder As OrdersEntity
            Dim bCombineLines As Boolean
            Dim iQty As Integer
            Dim lstProductID As List(Of Long) = Nothing
            Dim bFound As Boolean = False
            Dim iItemForUpdate As Integer = 0
            Try
                ''code added by asmita
                If ValidateTermsAndCondtion(ProductID) = False Then
                    Exit Sub
                End If
                ''end by asmita

                Dim strProductType As String = ShoppingCart1.GetProductType(Me.ProductID)
                If String.Compare(strProductType, "Housing", True) = 0 Then
                    lblAdded.Text = "Housing products are not supported in this build."
                    lblAdded.Visible = True
                Else
                    If String.Compare(strProductType, "Meeting", True) = 0 Then '' Peresh <Performance> - Removed condition OrElse String.Compare(strProductType, "Housing", True) = 0 as it is already checked above

                        bCombineLines = False
                    Else
                        bCombineLines = True
                    End If

                    If IsNumeric(txtQuantity.Text) Then
                        iQty = CInt(txtQuantity.Text)
                    Else
                        lblAdded.Text = "Quantity must be numeric."
                        lblAdded.Visible = True
                        Exit Sub
                    End If
                    'Added by Sandeep for performance Issue 
                    oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                    lstProductID = ShoppingCart1.CreateProductIDList(oOrder)
                    If lstProductID Is Nothing Then
                        bFound = False
                        iItemForUpdate = 0
                    Else
                        If lstProductID.Contains(Me.ProductID) Then
                            bFound = True
                            For i As Integer = 0 To lstProductID.Count - 1
                                If lstProductID(i) = Me.ProductID Then
                                    iItemForUpdate = i
                                    Exit For
                                End If
                            Next
                        Else
                            If lstProductID.Count = 1 Or lstProductID Is Nothing Then
                                bFound = False
                                iItemForUpdate = lstProductID.Count
                            Else
                                bFound = False
                                iItemForUpdate = lstProductID.Count - 1
                            End If

                        End If
                    End If
                    'Anil Issue 14302
                    '-----------------------------By Vaishali---------------------
                    Dim lPersonID As Long = -1
                    Dim sCampaignURL As String = String.Empty
                    If User1.UserID > 0 Then
                        lPersonID = Me.User1.PersonID
                    End If
                    If Request.QueryString.Count > 0 AndAlso Not String.IsNullOrEmpty(Request.QueryString("cID")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("PersonID")) Then
                        Dim lCampaignID As Long = Convert.ToInt64(Request.QueryString("cID"))
                        lPersonID = Convert.ToInt64(Request.QueryString("PersonID"))
                        Dim lcampaignListID As Long = Convert.ToInt64(Request.QueryString("lID"))
                        oOrder.SetValue("CampaignCodeID", lCampaignID)
                        sCampaignURL = "&cID=" + Request.QueryString("cID") + "&PersonID=" + Request.QueryString("PersonID") + "&lID=" + Request.QueryString("lID") + "&Source=" + Request.QueryString("Source")
                    End If
                    '-----------end ----------------------------------
                    'If ShoppingCart1.AddToCart(oOrder, Me.ProductID, Me.User1.PersonID, bFound, iItemForUpdate, bCombineLines, , iQty) Then
                    If ShoppingCart1.AddToCart(oOrder, Me.ProductID, Me.User1.PersonID, bFound, iItemForUpdate, bCombineLines, , iQty) Then
                        '---------------------------ENd------------------------------
                        lstProductID = ShoppingCart1.CreateProductIDList(oOrder)
                        If ShoppingCart1.GetProductTypeWebPages(Me.ProductID, sProductPage, sOrderPage) Then
                            'oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                            Session("ProductID") = Me.ProductID
                            If oOrder.SubTypes("OrderLines").Count = 1 Then
                                Dim bIncludeInShipping As Boolean
                                Dim oShipmentTypes As New Aptify.Framework.Web.eBusiness.CommonMethods(DataAction, AptifyApplication, User1, Database)
                                Dim dtCC As DataTable
                                bIncludeInShipping = oShipmentTypes.IncludeInShipping(Me.ProductID)
                                If bIncludeInShipping Then
                                    dtCC = oShipmentTypes.LoadShipmentType(CInt(oOrder.GetValue("ShipToCountryCodeID")))
                                    'Suraj Issue 16262, 5/13/13 , check the datatable rows is greater than zero or not
                                    If dtCC.Rows.Count > 0 Then
                                        oOrder.SetValue("ShipTypeID", dtCC.Rows(0)("ID"))
                                    End If
                                End If
                            End If
                            isAutoRenew = Convert.ToBoolean(oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1).GetValue("AutoRenew"))
                            If Len(sOrderPage) > 0 Then
                                ShoppingCart1.SaveCart(Me.ProductID, isAutoRenew, Page.Session)

                                Try
                                    Response.Redirect(sOrderPage & "?OL=" &
                                                      oOrder.SubTypes("OrderLines").Count - 1)
                                    Exit Sub
                                Catch ex As Exception
                                    'Sorry
                                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                                End Try
                            Else
                                Me.SetTotalCartQty()
                                Session("ProductID") = Nothing
                            End If
                        End If
                        Session("ProductID") = Nothing

                        lblAdded.Text = "This product has been added to your cart, to checkout please proceced to your shopping cart."
                        lblAdded.ForeColor = Drawing.Color.Blue
                        lblAdded.Visible = True
                        lnkViewCart.Visible = True
                        lblAdded.Attributes.Add("class", "product_added")
                        '-----------------------------------Start by Asmita
                        'Dim sSQL = "SELECT TermsAndConditionID__c FROM " &
                        '    AptifyApplication.GetEntityBaseDatabase("Products") &
                        '     "..vwProducts WHERE IsRequiredAgreement__c =1 and ID=" & Me.ProductID
                        ''dt1 = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                        'Dim obj As Object = DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                        'If dtValidate.Rows.Count() > 0 AndAlso System.Convert.ToInt64(dtValidate.Rows(0)("TermsAndConditionID__c")) > 0 Then
                        '    Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity = Nothing
                        '    'oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                        '    oOrderLine = CType(oOrder.SubTypes("OrderLines").Find("ProductID", ProductID), Aptify.Applications.OrderEntry.OrderLinesEntity)
                        '    If Not oOrderLine Is Nothing Then
                        '        oOrderLine.SetValue("IsRequiredAgreement__c", 1)
                        '        oOrderLine.SetValue("TermsAndConditionID__c", System.Convert.ToInt64(obj))
                        '    End If
                        '    'ShoppingCart1.SaveCart()
                        'End If

                        '#21000
                        If dtProduct.Rows.Count() > 0 AndAlso dtProduct.Rows(0)("TermsAndConditionID__c") Is Nothing Then
                            If Convert.ToInt64(dtProduct.Rows(0)("TermsAndConditionID__c")) > 0 Then
                                '#21000
                                '###EDUARDO 23-01-2020
                                '    Dim sSQL = "SELECT TermsAndConditionID__c FROM " &
                                'AptifyApplication.GetEntityBaseDatabase("Products") &
                                ' "..vwProducts WHERE IsRequiredAgreement__c =1 and ID=" & Me.ProductID
                                '    'dt1 = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                                '    Dim obj As Object = DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                                Dim oOrderLine As OrderLinesEntity = Nothing
                                'oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                                oOrderLine = CType(oOrder.SubTypes("OrderLines").Find("ProductID", ProductID), OrderLinesEntity)
                                If Not oOrderLine Is Nothing Then
                                    oOrderLine.SetValue("IsRequiredAgreement__c", 1)
                                    oOrderLine.SetValue("TermsAndConditionID__c", Convert.ToInt64(dtProduct.Rows(0)("TermsAndConditionID__c")))
                                End If
                                'ShoppingCart1.SaveCart()
                            End If
                        End If
                        '-----------------------------------------------end by asmita
                        isAutoRenew = Convert.ToBoolean(oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1).GetValue("AutoRenew"))
                        'IsProductAutoRenew(Me.ProductID)
                        ShoppingCart1.SaveCart(Me.ProductID, isAutoRenew, Page.Session)
                        '--------------by vaishali
                        Try
                            Response.Redirect(GroupingContentsViewProductPage + "?ID=" + Me.ProductID.ToString + "&Val2=" + "product added to cart" + sCampaignURL + "&WebClickCreated=1")
                            Exit Sub
                        Catch ex As Exception
                            'Sorry
                            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                        End Try
                        '-----------------End
                    Else
                        Session("ProductID") = Nothing
                        lblAdded.ForeColor = Drawing.Color.Red
                        lblAdded.Visible = True
                        lblAdded.Attributes.Add("class", "product_error")
                        ''RashmiP, Issue 9511, 09/08/10
                        RemoveHandler lnkAddToCart.Click, AddressOf lnkAddToCart_ServerClick
                        If ShoppingCart1.WebProdPreRequisiteErrMsg = String.Empty Then
                            lblAdded.Text = "Unable to add product"
                        Else
                            lblAdded.Text = ShoppingCart1.WebProdPreRequisiteErrMsg
                        End If
                        lnkViewCart.Visible = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''''''''''''''''''' Added code by Govind Mande ''''''''''''''''''''''
        ''' <summary>
        ''' tEST
        ''' </summary>
        ''' <remarks></remarks>
        ''' 

        Private Sub WarningMessgaeOnTaxcession()
            Try

                ' Check Taxation Publication Product if yes then chk if product subscription start on Next Month then give a Warning Message
                Dim sIsTaxationPublicationsProductSQL As String = Database & "..spCheckIsTaxationPublicationsProduct__c @ProductID=" & Me.ProductID
                Dim lProductID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sIsTaxationPublicationsProductSQL))
                If lProductID > 0 Then

                    ' Found Taxation Product


                    'Dim sSqlTodaysDate As String = Database & "..spGetTodaysDate__c"
                    '  Dim sTodayDate As Date = Convert.ToDateTime(DataAction.ExecuteScalar(sSqlTodaysDate))

                    Dim sTodayDate As Date = Date.Now

                    Dim sSqlIssueDay As String = Me.Database & "..spCheckIssuePublishDayonDate__c"
                    Dim param(0) As IDataParameter

                    param(0) = DataAction.GetDataParameter("@ProductID", SqlDbType.Int, Convert.ToInt32(Me.ProductID))
                    Dim lMonthsStartSub As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSqlIssueDay, CommandType.StoredProcedure, param))

                    'Dim sSqlIssueDay As String = Database & "..spCheckIssuePublishDay__c @ProductID=" & Me.ProductID & ",@OrderDate='" & sTodayDate & "'"
                    'Dim lMonthsStartSub As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlIssueDay))
                    If lMonthsStartSub > 0 Then

                        'If sTodayDate.Month <> lMonthsStartSub Then
                        If sTodayDate.Day > lMonthsStartSub Then
                            Dim sSubscriptionSQL As String = Database & "..spGetSubscriptionDetails__c @PersonID=" &
                                                              User1.PersonID & ",@ProductID=" & Me.ProductID
                            Dim lSubscriptionID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSubscriptionSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If lSubscriptionID <= 0 Then
                                lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Products.TaxationWarningMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                radMockTrial.VisibleOnPageLoad = True
                            Else
                                AddProductOnCart()
                            End If

                        Else
                            AddProductOnCart()
                        End If
                    Else
                        AddProductOnCart()
                    End If
                Else
                    AddProductOnCart()
                    ' Code Copy on Add To Cart 
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' If user select Yes for Subscription Start Date will be on Next Month first day
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnYes_Click(sender As Object, e As System.EventArgs) Handles btnYes.Click
            Try
                radMockTrial.VisibleOnPageLoad = False
                AddProductOnCart()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Protected Sub btnNo_Click(sender As Object, e As System.EventArgs) Handles btnNo.Click
        '    Try
        '        ' If user select no it meanse product not added to shopping cart and close the warning window
        '        radMockTrial.VisibleOnPageLoad = False
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub
        ''''''''''''''''' END BY GOVIND CODE..................................
        Private Sub SetTotalCartQty()

            Dim oOrder As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
            oOrder = Me.ShoppingCart1.GetOrderObject(Session, Page.User, Page.Application)
            Dim iItemForUpdate As Integer = 0
            Dim i As Integer = 0
            Dim qty As Integer = 0

            'By Milind - added the code in entity plug in 
            'With oOrder.SubTypes("OrderLines")
            '    For i = 0 To .Count - 1
            '        qty = qty + CInt(.Item(i).GetValue("Quantity"))
            '        If CLng(.Item(i).GetValue("ProductID")) = CLng(Session("ProductID")) AndAlso _
            '                               CLng(.Item(i).GetValue("ParentSequence")) <= 0 Then
            '            oOrder.SubTypes("OrderLines").Item(i).SetValue("AutoRenew", isAutoRenew)
            '        End If
            '    Next
            'End With

            ''If oOrder IsNot Nothing AndAlso oOrder.SubTypes("OrderLines") IsNot Nothing Then
            ''    With oOrder.SubTypes("OrderLines").Item(iItemForUpdate)

            ''    End With
            ''End If

            Me.TotalCartQty = qty
        End Sub

        Protected Sub btnNewVersion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewVersion.Click
            Response.Redirect(Me.FixTemplateSourceDirectoryPath(Me.Request.Path) & "?" & Me.QueryStringRecordIDParameter & "=" & Me.NewProductVersionID)
            Return
        End Sub

        'Protected Sub lnkViewCart_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewCart.Click
        '    Response.Redirect(ViewcartPage)
        'End Sub

        Protected Sub lnkViewCart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewCart.Click
            'By Vaishali
            'redirect the person to login page if not already logged in
            If User1.PersonID <= 0 AndAlso Request.QueryString.Count > 0 AndAlso Not String.IsNullOrEmpty(Request.QueryString("cID")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("PersonID")) Then
                Session("ReturnToPage") = ViewcartPage
                ' Suraj S Issue 15370, 8/1/13 here we are getting the ReturnToPageURL in "URL" QueryString and passing on login page. 
                Try
                    Response.Redirect(LoginPage)
                    Exit Sub
                Catch ex As Exception
                    'Sorry
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            Else
                Try
                    Response.Redirect(ViewcartPage)
                Catch ex As Exception
                    'Sorry
                End Try
            End If
        End Sub


#Region "Custom Methods"

        ''' <summary>
        ''' 'To validate Terms and conditions on Product 
        ''' </summary>
        ''' <param name="ProductID"></param>
        ''' <remarks></remarks>
        Private Function ValidateTermsAndCondtion(ByVal ProductID As Long) As Boolean
            Dim sSQL As String = String.Empty, bRetVal As Boolean = True
            Try
                '###EDUARDO 23-01-2020
                'sSQL = "SELECT IsRequiredAgreement__c, TermsAndConditionID__c FROM " &
                '              AptifyApplication.GetEntityBaseDatabase("Products") &
                '              "..vwProducts WHERE ID=" & Me.ControlRecordID
                'dtValidate = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)


                If (dtProduct.Rows.Count() > 0) Then
                    If dtProduct.Rows(0)("IsRequiredAgreement__c").ToString().ToUpper() = "TRUE" Then
                        If Me.ChkRequiredAgreement.Checked = False Then
                            lblAdded.Visible = True
                            'lblAdded.Text = "Please check terms and conditions "
                            lblAdded.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.TermsAndConditions")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            bRetVal = False
                            lblAdded.Attributes.Add("class", "product_error")
                        Else
                            'Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity = Nothing
                            'Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity = Nothing
                            'oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                            'oOrderLine = CType(oOrder.SubTypes("OrderLines").Find("ProductID", ProductID), Aptify.Applications.OrderEntry.OrderLinesEntity)
                            'oOrderLine.SetValue("IsRequiredAgreement__c", 1)
                            'oOrderLine.SetValue("TermsAndConditionID__c", CLng(_foundProduct("TermsAndConditionID__c")))
                            'ShoppingCart1.SaveCart()
                            bRetVal = True

                        End If
                        ' bRetVal = True
                    End If
                    ' bRetVal = True
                End If
                Return bRetVal
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return bRetVal
            End Try
        End Function

        'Rajesh Kardile - 04/19/2014 - Display Attachment Records.
        ''' <summary>
        ''' Display Attachment as per Products
        ''' </summary>
        ''' <param name="productID"></param>
        ''' <remarks></remarks>
        Protected Sub displayRecordAttachment(ByVal productID As Long)
            Try

                Dim lEntityId As Long
                lEntityId = CLng(Me.AptifyApplication.GetEntityID("Products"))
                'LoadDataFromGE(Me.AptifyApplication.GetEntityObject("Products", productID))
                Me.RecordAttachments__c.AllowAdd = False
                Me.RecordAttachments__c.AttachmentCategory = AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "PrePurchase Downloads")
                Me.RecordAttachments__c.LoadAttachments(lEntityId, productID)
                Dim grdAttachments As RadGrid = TryCast(Me.RecordAttachments__c.FindControl("grdAttachments"), RadGrid)
                If Not grdAttachments Is Nothing AndAlso grdAttachments.Items.Count > 0 Then
                    trRecordAttachment.Visible = True
                    Me.RecordAttachments__c.Visible = True
                Else
                    trRecordAttachment.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ' ''' <summary>
        ' ''' 'To Apply campaign on Product if user gets an email for campaign applied with web link for campaignURL__c
        ' ''' </summary>
        ' ''' <param name="ProductID"></param>
        ' ''' <remarks></remarks>
        ' ''' 
        'Private Function getProductCampaignApplied(ByVal ProductID As Long) As Decimal


        '    Try
        '        Dim campaignID As Long = Convert.ToInt32(Request.QueryString("CampaignID"))


        '        Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
        '        Dim oOL As Aptify.Applications.OrderEntry.OrderLinesEntity
        '        'Here get the Top 1 Person ID whose MemberTypeID = 1 
        '        oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
        '        oOrder.ShipToID = User1.PersonID
        '        oOrder.BillToID = User1.PersonID
        '        oOrder.CampaignCodeID = campaignID
        '        oOrder.AddProduct(ProductID)
        '        If oOrder.SubTypes("OrderLines").Count > 1 Then
        '            oOL = TryCast(oOrder.SubTypes("OrderLines").Item(1), OrderLinesEntity)

        '            Return CDec(Convert.ToString(oOL.Price))
        '        Else
        '            Return CDec(Convert.ToString(0))
        '        End If


        '    Catch ex As Exception
        '        Return CDec(Convert.ToString(0))
        '    End Try
        'End Function

        ''' <summary>
        ''' Check if product-subscription is set to auto renew
        ''' </summary>
        Private Sub IsProductAutoRenew(productId As Long)
            Try
                Dim sql As String = "..spIsProductAutoRenew__c @RecordID=" & productId.ToString()
                Dim result = DataAction.ExecuteScalar(sql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not IsNothing(result) Then
                    isAutoRenew = Convert.ToBoolean(DataAction.ExecuteScalar(sql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub



        Private Sub AddGoogleTagImpression()
            Dim impression As DetailImpression, productDto As ProductDto

            productDto = GetProductDto()
            impression = New DetailImpression(Me.Page, productDto)

            impression.Render()

        End Sub

        Private Function GetProductDto() As ProductDto
            Dim price As String,
                currencyType As String = "EUR"


            price = Regex.Match(lblPrice.Text, "[,\d]+(\.\d{1,2})?").Value

            Dim oPrice As IProductPrice.PriceInfo = Me.GetProductPrice(CLng(_foundProduct("ID")))

            If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                currencyType = "EUR"
            ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                currencyType = "GBP"
            End If

            Dim dPrice As Decimal = 0
            If Not String.IsNullOrWhiteSpace(price) Then
                dPrice = CType(price, Decimal)
            End If


            Return New ProductDto With
                {
                .Name = _foundProduct("WebName").ToString,
                .Id = _foundProduct("ID").ToString,
                .price = dPrice,
                .Brand = "Chartered Accountants",
                .Category = "",
                .Variant = "",
                .List = _foundProduct("Type").ToString,
                .Currency = currencyType,
                .Position = 1
                }

        End Function

        Private Sub AddGoogleTagAddToCartClick()
            Dim addedToShoppingCart As AddedToShoppingCart,
                impressionDto As ImpressionDto,
                oOrder As OrdersEntity,
                price As String,
                currencyType As String = "EUR"

            If String.IsNullOrEmpty(Request.QueryString("Val2")) Then
                Return
            End If



            If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                currencyType = "EUR"
            ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                currencyType = "GBP"
            End If



            oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
            price = Regex.Match(lblPrice.Text, "[,\d]+(\.\d{1,2})?").Value

            impressionDto = New ImpressionDto With {
                .Currency = currencyType
                }

            Dim dPrice As Decimal = 0
            If Not String.IsNullOrWhiteSpace(price) Then
                dPrice = CType(price, Decimal)
            End If

            For Each orderLine As Aptify.Consulting.Entity.OrderLines.OrderLinesEntity__c In oOrder.SubTypes("OrderLines")

                If (orderLine.ProductID = Me.ProductID()) Then

                    impressionDto.Products.Add(New ProductDto With {
                .Id = Me.ProductID.ToString,
                .Name = lblName.Text,
                .price = dPrice,
                .Brand = "Chartered Accountants",
                .Category = "",
                .List = orderLine.ProductType,
                .Position = 1,
                .Quantity = orderLine.Quantity,
                .Variant = ""
                })

                End If
            Next



            addedToShoppingCart = New AddedToShoppingCart(Me.Page, impressionDto)
            addedToShoppingCart.Render()
        End Sub

        Private Sub UnloadImage()
            Dim s As String = "Error Loading Product ID: " & Request.QueryString("ID") & ". "

            If imgProduct.ImageUrl Is Nothing Or imgProduct.ImageUrl = "" Or lblMsg.Text.ToString().Contains("The requested product is not available on the web.") Or lblMsg.Text.ToString().Contains("The requested product could not found.") Then
                Me.imgProduct.ImageUrl = ImageNotAvailable
                Me.imgProduct.Visible = True
            End If

        End Sub

        Private Sub LoadWithNotFoundLayoutSetup()

            ViewState("AvailableForSale") = False
            lblMsg.Text = "The requested product could not found."
            ProductCategoryLinkString1.Visible = False
            ChkRequiredAgreement.Visible = False

            lnkAddToCart.Visible = False
            txtQuantity.Text = "0"
            lblPrice.Text = "N/A"
            'lblProductMessage.Text = "The requested product could be not found."
            'lblProductMessage.Visible = True

            'If Not String.IsNullOrEmpty("The requested product could be not found.") Then
            lblNote.Visible = True
            'End If
            lblName.Text = "Product not available!"
            lblName.Visible = True

            lblavailval.Text = "Not in stock"
            lblavailval.ForeColor = System.Drawing.Color.Red
            lblavailval.Visible = True
            lblAvailable.Visible = True

            If IsDBNull(lblDescription.Text) Or lblDescription.Text.Contains("content to be confirmed") Then
                lblDescription.Visible = False
            End If

            btnBack.Visible = True

        End Sub

        Private Sub LoadWithNotAvaliableSetup()

            ViewState("AvailableForSale") = False
            lblMsg.Text = "The requested product is not available for sale."
            lnkAddToCart.Visible = False
            lblSummary.Visible = False
            lblavailval.Visible = False
            imgNotAvailable.Visible = True
            Me.imgProduct.Visible = True

            If IsDBNull(lblDescription.Text) Or lblDescription.Text.Contains("content to be confirmed") Then
                lblDescription.Visible = False
            End If

        End Sub
#End Region

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Response.Redirect("~/productcatalog/default.aspx")
                Return
            Catch ex As Exception
                ex.Message.ToString()
            End Try
        End Sub

    End Class

End Namespace

