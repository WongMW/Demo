Option Explicit On
Option Strict On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.Security
Imports System.Data
Imports Aptify.Framework.Web.eBusiness
Imports SoftwareDesign.GTM
Imports SoftwareDesign.GTM.Model
Imports System.Collections.Generic

Namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c
    Partial Class FeaturedProductsControl
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_VIEW_PRODUCT_PAGE As String = "ViewProductPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "FeaturedProducts"
        'Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
        Protected Const ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL As String = "ImageNotAvailable"
        'End of addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
        Protected Const ATTRIBUTE_VIEW_CLASS_PAGE As String = "ViewClassPage"

        Private _products As ICollection(Of ProductDto) = New List(Of ProductDto)
        Private _clickPosition As Integer = 0
        Private _prodNavigatorHelper As ProdNavigatorHelper



#Region "FeaturedProducts Specific Properties"
        ''' <summary>
        ''' ViewProduct page url
        ''' </summary>
        Public Overridable Property ViewProductPage() As String
            Get
                If Not ViewState(ATTRIBUTE_VIEW_PRODUCT_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_VIEW_PRODUCT_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_VIEW_PRODUCT_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' 'addition by Suvarna D IssueID-12745 to implement webImage feature to display images
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
        ''End of addition by Suvarna D IssueID-12745 to implement webImage feature to display images

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

#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(ViewProductPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ViewProductPage = Me.GetLinkValueFromXML(ATTRIBUTE_VIEW_PRODUCT_PAGE)
                If String.IsNullOrEmpty(ViewProductPage) Then
                    Me.grdFeaturedProducts.Enabled = False
                    Me.grdFeaturedProducts.ToolTip = "ViewProductPage property has not been set."
                End If
            End If

            ''Addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
            If String.IsNullOrEmpty(ImageNotAvailable) Then
                'since value is the 'default' check the XML file for possible custom setting
                ImageNotAvailable = Me.GetLinkValueFromXML(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL)
            End If
            'End of addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
        End Sub


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


            Try
                If Not IsPostBack() Then
                    'set control properties from XML file if needed
                    SetProperties()
                    Me._prodNavigatorHelper = New ProdNavigatorHelper(ViewProductPage & "?ID={0}", ViewClassPage)

                    'Dim sSQL As String
                    'Dim dt As DataTable

                    '' changed to use Config Settings for virtual directory location
                    'sSQL = " Exec " + Database.ToString + "..spGetFeaturedCategoryProductsWeb__cai @WebUserID = " & User1.UserID

                    'dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    ''Navin Prasad Issue 11032

                    '' DirectCast(grdFeaturedProducts.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = ViewProductPage & "?ID={0}"

                    ''HP - Per issue 8222, do not display grid if empty 
                    'If dt.Rows.Count > 0 Then
                    '    grdFeaturedProducts.DataSource = dt
                    '    grdFeaturedProducts.DataBind()

                    '    'Navin Prasad Issue 11032

                    '    For Each row As GridViewRow In grdFeaturedProducts.Rows
                    '        Dim lnk As HyperLink = CType(row.FindControl("lnkName"), HyperLink)
                    '        lnk.NavigateUrl = String.Format(ViewProductPage & "?ID={0}", dt.Rows(row.RowIndex)("ID").ToString)
                    '    Next

                    '    grdFeaturedProducts.Visible = True
                    '    divGrid.Visible = True
                    '    noData.Visible = False
                    'Else
                    '    divGrid.Visible = False
                    '    noData.Visible = True
                    'End If
                    FeaturedProductsFill()
                    AddGoogleTagImpressions()
                    AddGoogleTagClicks()
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Added FeaturedProductsFill() function for grdFeaturedProducts fill,Updated By Nalini 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub FeaturedProductsFill()
            Dim sSQL As String
            Dim dt As DataTable
            Dim position As Integer = 0

            Try
                ' changed to use Config Settings for virtual directory location  spGetFeaturedCategoryProductsWeb__cai
                ' sSQL = " Exec " + Database.ToString + "..spGetFeaturedCategoryProductsWeb @WebUserID = " & User1.UserID
                sSQL = " Exec " + Database.ToString + "..spGetFeaturedCategoryProductsWeb__cai @WebUserID = " & User1.UserID

                ' checking if categoryID is present
                If Not (Request.QueryString("ID") Is Nothing) Then

                    Dim num As Integer = Nothing
                    If Not Integer.TryParse(Request.QueryString("ID"), num) Then
                        sSQL += " ,@CategoryID = NULL"
                    Else
                        num = Integer.Parse(Request.QueryString("ID"))
                        sSQL += " ,@CategoryID = " + num.ToString()
                    End If

                Else
                    sSQL += " ,@CategoryID = NULL"
                End If

                dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                'Navin Prasad Issue 11032

                ' DirectCast(grdFeaturedProducts.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = ViewProductPage & "?ID={0}"

                'HP - Per issue 8222, do not display grid if empty 
                If dt.Rows.Count > 0 Then
                    grdFeaturedProducts.DataSource = dt
                    grdFeaturedProducts.DataBind()

                    'Suvarna Deshmukh IssueID-12433,12430 and 12434 On Dec 13,2011
                    'commented and added new code to provide navigation url in datalist item
                    'Navin Prasad Issue 11032
                    'For Each row As GridViewRow In grdFeaturedProducts.Rows
                    '    Dim lnk As HyperLink = CType(row.FindControl("lnkName"), HyperLink)
                    'lnk.NavigateUrl = String.Format(ViewProductPage & "?ID={0}", dt.Rows(row.RowIndex)("ID").ToString)
                    'Next

                    For Each item As DataListItem In grdFeaturedProducts.Items
                        Dim itemID As Integer = item.ItemIndex
                        'Dim itemID As Integer = CInt(grdFeaturedProducts.DataKeys(index).ToString())

                        Dim lnk As HyperLink = CType(item.FindControl("lnkName"), HyperLink)
                        ''addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
                        Dim iImg As Image = CType(item.FindControl("ImgProd"), Image)
                        ''End of addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
                        'lnk.NavigateUrl = _prodNavigatorHelper.FormatNavigationUrl(dt.Rows(itemID))

                        'Addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
                        If Not IsDBNull(dt.Rows(itemID)("ProdImageURL")) AndAlso
                                Len(dt.Rows(itemID)("ProdImageURL")) > 0 Then
                            iImg.ImageUrl = CStr(dt.Rows(itemID)("ProdImageURL"))
                        Else
                            iImg.ImageUrl = ImageNotAvailable
                        End If
                        'End of addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012

                        Dim anchorViewProduct As HtmlAnchor = CType(item.FindControl("anchorViewProduct"), HtmlAnchor)
                        'anchorViewProduct.HRef = _prodNavigatorHelper.FormatNavigationUrl(dt.Rows(itemID))

                        _products.Add(New ProductDto With
                                     {
                                     .Name = dt.Rows(itemID)("Name").ToString,
                                     .Id = dt.Rows(itemID)("ID").ToString,
                                     .Price = Nothing,
                                     .Brand = "Chartered Accountants",
                                     .Category = dt.Rows(itemID)("ProductCategory").ToString,
                                     .Variant = "",
                                     .List = dt.Rows(itemID)("ProductType").ToString,
                                     .Position = ++position
                                  })

                    Next
                    'End of Addition by Suvarna Deshmukh IssueID-12433,12430 and 12434 On Dec 13,2011


                    grdFeaturedProducts.Visible = True
                    divGrid.Visible = True
                    'noData.Visible = False
                    H1.Visible = True
                Else
                    divGrid.Visible = False
                    'noData.Visible = True
                    H1.Visible = False
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub AddGoogleTagImpressions()
            Dim impression As Impression, impressionDto As ImpressionDto,
                currencyType As String = "EUR"

            If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                currencyType = "EUR"
            ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                currencyType = "GBP"
            End If

            impressionDto = New ImpressionDto
            impressionDto.Currency = currencyType
            'impressionDto.Products = GetProducts()

            impression = New Impression(Me.Page, impressionDto)
            impression.Render()

        End Sub

        Private Sub AddGoogleTagClicks()
            Dim gtmClick As GtmClick

            gtmClick = New GtmClick(Me.Page)

            gtmClick.Render()
        End Sub

        Private Function GetProducts() As ICollection(Of ProductDto)

            Dim dt As DataTable = CType(grdFeaturedProducts.DataSource, DataTable)
            Dim position As Integer = 0

            If (dt Is Nothing) Then
                Return _products
            End If

            For Each row As DataRow In dt.Rows

                _products.Add(New ProductDto With
                             {
                             .Name = row("Name").ToString,
                             .Id = row("ID").ToString,
                             .Price = Nothing,
                             .Brand = "Chartered Accountants",
                             .Category = row("ProductCategory").ToString,
                             .Variant = "",
                             .List = row("ProductType").ToString,
                             .Position = ++position
                          })
            Next

            Return _products
        End Function

        Protected Function GetGtmObject(dataItem As Object) As String
            Dim dtRow As DataRowView, url As String,
                currencyType As String = "EUR"


            dtRow = CType(dataItem, DataRowView)
            _clickPosition = _clickPosition + 1

            url = String.Format(ViewProductPage & "?ID={0}", dtRow("ID").ToString)

            If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                currencyType = "EUR"
            ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                currencyType = "GBP"
            End If

            Return "trackGTMClick('" +
                dtRow("Name").ToString + "','" +
                   dtRow("ID").ToString + "','" +
                   "" + "','" +
                   "Chartered Accountants" + "','" +
                   dtRow("ProductCategory").ToString + "','" +
                   "" + "','" +
                   _clickPosition.ToString + "','" +
                   url + "','" +
                   currencyType + "'" +
                   ")"

        End Function


    End Class
End Namespace
