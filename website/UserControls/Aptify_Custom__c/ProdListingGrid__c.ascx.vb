'Aptify e-Business 5.5.1, July 2013
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date Created/Modified               Summary
'Rajesh Kardile             03/20/2014                          Change inline Query to Store procedure SpGetProductForPreferredCurrency__c
'Rajesh Kardile             07/10/2014                          Replace Hard code "Pound" value from entity attribute
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.Application
Imports System.Data
Imports Aptify.Applications.OrderEntry
Imports SoftwareDesign.GTM
Imports SoftwareDesign.GTM.Model
Imports SitefinityWebApp
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.ProductCatalog
    Partial Class ProdListingGrid__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_SHOW_HEADER_IF_EMPTY As String = "ShowHeaderIfEmpty"
        Protected Const ATTRIBUTE_HEADER_TEXT_PAGE As String = "HeaderText"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "ProdListingGrid"
        'Suvarna D IssueID-12745 to implement webImage feature to display images, IssueId 12735- to add header for product category navbar on Jan 19, 2012     
        Protected Const ATTRIBUTE_HEADER_PRODCAT_PAGE As String = "ProdCatHeader"
        Protected Const ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL As String = "ImageNotAvailable"
        'End of addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
        Protected Const ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME As String = "ShowMeetingsLinkToClass"
        Protected Const ATTRIBUTE_VIEW_CLASS_PAGE As String = "ViewClassPage"
        Dim oOrder As OrdersEntity

        Private _products As ICollection(Of ProductDto) = New List(Of ProductDto)
        Private _clickPosition As Integer = 0
        Private _prodNavigatorHelper As ProdNavigatorHelper



#Region "ProdListingGrid Specific Properties"
        ''' <summary>
        ''' If set to False(default), the header is not shown if there are no records in the product listing grid, if set to True, the header is always shown
        ''' </summary>
        Public Property ShowHeaderIfEmpty() As Boolean
            Get
                If ViewState(ATTRIBUTE_SHOW_HEADER_IF_EMPTY) IsNot Nothing Then
                    Return CBool(ViewState(ATTRIBUTE_SHOW_HEADER_IF_EMPTY))
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                ViewState(ATTRIBUTE_SHOW_HEADER_IF_EMPTY) = value
            End Set
        End Property
        ''' <summary>
        ''' Displays text on the top of the control in a header
        ''' </summary>
        Public Property HeaderText() As String
            Get
                If ViewState(ATTRIBUTE_HEADER_TEXT_PAGE) IsNot Nothing Then
                    Return ViewState(ATTRIBUTE_HEADER_TEXT_PAGE).ToString
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_HEADER_TEXT_PAGE) = value
            End Set
        End Property

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

        Public Property ProdCatHeader() As String
            Get
                If ViewState(ATTRIBUTE_HEADER_PRODCAT_PAGE) IsNot Nothing Then
                    Return ViewState(ATTRIBUTE_HEADER_PRODCAT_PAGE).ToString
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_HEADER_PRODCAT_PAGE) = value
            End Set
        End Property

        ''' <summary>
        ''' 'Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
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
        'Nalini issue 11290
        Protected Overridable ReadOnly Property ShowMeetingsLinkToClass() As Boolean
            Get
                If Not ViewState.Item(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME) Is Nothing Then
                    Return CBool(ViewState.Item(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME)
                    If Not String.IsNullOrEmpty(value) Then
                        Select Case UCase(value)
                            Case "TRUE", "FALSE", "0", "1"
                                ViewState.Item(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME) = CBool(value)
                            Case Else
                                ViewState.Item(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME) = False
                        End Select
                    Else
                        ViewState.Item(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME) = False
                    End If
                End If
                Return False
            End Get
        End Property
        'End of addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012

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

            Try
                If ShowHeaderIfEmpty = False Then
                    'since value is the 'default' check the XML file for possible custom setting
                    ShowHeaderIfEmpty = CBool(Me.GetPropertyValueFromXML(ATTRIBUTE_SHOW_HEADER_IF_EMPTY))
                End If
            Catch ex As Exception
                If TypeOf ex Is InvalidCastException Then
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(InvalidCastExceptionForBooleanProperties(ATTRIBUTE_SHOW_HEADER_IF_EMPTY, ex.Message))
                Else
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End If
            End Try
            If String.IsNullOrEmpty(HeaderText) Then
                'since value is the 'default' check the XML file for possible custom setting
                HeaderText = Me.GetLinkValueFromXML(ATTRIBUTE_HEADER_TEXT_PAGE)
            End If

            'Suvarna D IssueID-12745 to implement webImage feature to display images, IssueId 12735- to add header for product category navbaron Jan 19, 2012
            If String.IsNullOrEmpty(ProdCatHeader) Then
                'since value is the 'default' check the XML file for possible custom setting
                ProdCatHeader = Me.GetPropertyValueFromXML(ATTRIBUTE_HEADER_PRODCAT_PAGE)
            End If
            If String.IsNullOrEmpty(ImageNotAvailable) Then
                'since value is the 'default' check the XML file for possible custom setting
                ImageNotAvailable = Me.GetLinkValueFromXML(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL)
            End If
            'End of addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            'set control properties from XML file if needed
            SetProperties()

            If Not IsPostBack Then
                hdnPerson.Value = CStr(User1.PersonID) ' Peresh <Performance>
                Session("ShoppingCart") = ShoppingCart1 ' Peresh <Performance>
                Me.SetControlRecordIDFromParam()
                LoadAllGrids()
            End If
            ''Added by Paresh for Performance
            ScriptManager.RegisterStartupScript(UpdatePanelgrdMain, UpdatePanelgrdMain.GetType(), "CallMyFunction", "Sys.Application.add_load(GetProductPrice);", True)

        End Sub

        Public Function GetGridRowCount() As Long
            'Navin Prasad Issue 11032
            GetGridRowCount = grdMain.Items.Count
        End Function

        Public Function GetProductPrice(ByVal lProductID As Long) As Aptify.Applications.OrderEntry.IProductPrice.PriceInfo
            ' Implement This function
            Return ShoppingCart1.GetUserProductPrice(lProductID, 1)
        End Function

        Public Sub LoadGrid(ByVal CategoryID As Long,
                            Optional ByVal ExcludeProductID As Long = -1)
            Dim sSQL As String
            Dim dt As System.Data.DataTable
            Try
                'Suvarna Deshmukh IssueID-12433,12430 and 12434 On Dec 19,2011
                'commented and added new code to get a product code from SQL
                ''sSQL = "SELECT ID,WebName,WebDescription, ProductCategory" & _
                'Rajesh K - 03/20/2014
                '************************** Start ************************************
                ' sSQL = "SELECT ID,WebName,WebDescription, ProductCategory, code, WebImage " & _
                '"FROM " & AptifyApplication.GetEntityBaseDatabase("Products") & _
                '"..vwProducts " & _
                '"WHERE ID<>" & ExcludeProductID & " AND " & _
                '"IsSold=1 AND WebEnabled=1 AND TopLevelItem=1 AND " & _
                '"ISNULL(ParentID,-1)=-1 AND CategoryID=" & CategoryID

                'If Not ShowMeetingsLinkToClass Then
                '    sSQL &= "  AND  ISNULL(ClassID ,-1) <=0 "
                'End If

                'sSQL &= " ORDER BY Name ASC"
                'dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

                dt = BindLoadGrid(CategoryID, ExcludeProductID)

                '************************************* End ********************************************

                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        'Navin Prasad Issue 11032
                        Dim arr() As String
                        arr = New String() {"ID"}
                        grdMain.DataSource = dt
                        grdMain.Visible = True
                        If Len(Me.HeaderText) > 0 Then
                            lblHeader.Visible = True
                        Else
                            lblHeader.Visible = False
                        End If
                    Else
                        grdMain.Visible = False
                        ''Code Commetented by Suvarna D IssueID-12735 Product Category Mouse hover should display sub categories. on Jan 18, 2012
                        'Product list grid and feature proudct has been added to same dive hence code commented
                        'divMain.Visible = False
                        ''End of Code Commetented by Suvarna D IssueID-12735 Product Category Mouse hover should display sub categories. on Jan 18, 2012
                        Me.lblHeader.Visible = Me.ShowHeaderIfEmpty
                    End If
                Else
                    grdMain.Visible = False
                    ''Code Commetented by Suvarna D IssueID-12735 Product Category Mouse hover should display sub categories. on Jan 18, 2012
                    'Product list grid and feature proudct has been added to same dive hence code commented
                    'divMain.Visible = False
                    ''End of Code Commetented by Suvarna D IssueID-12735 Product Category Mouse hover should display sub categories. on Jan 18, 2012
                    Me.lblHeader.Visible = Me.ShowHeaderIfEmpty
                End If
                lblHeader.InnerText = GenerateHeaderText(dt, Me.HeaderText)

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overridable Function GenerateHeaderText(ByVal dt As DataTable, ByVal HeaderTextFormat As String) As String
            Try
                Dim bDone As Boolean = False, sTemp As String = HeaderTextFormat
                Dim iStart As Integer, iEnd As Integer, sField As String, sVal As String

                While Not bDone
                    If sTemp.Contains("{") Then
                        iStart = sTemp.IndexOf("{")
                        iEnd = sTemp.IndexOf("}", iStart + 1)
                        sField = sTemp.Substring(iStart + 1, iEnd - iStart - 1)
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            Try
                                sVal = CStr(dt.Rows(0).Item(sField))
                            Catch ex As Exception
                                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                                sVal = ex.Message
                            End Try
                        Else
                            sVal = ""
                        End If
                        If iStart > 1 Then
                            sTemp = sTemp.Substring(0, iStart - 1) & sVal & sTemp.Substring(iEnd + 1)
                        Else
                            sTemp = sVal & sTemp.Substring(iEnd + 1)
                        End If
                    Else
                        bDone = True
                    End If
                End While
                Return sTemp
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return ex.Message
            End Try
        End Function

        Protected Sub grdMain_RowDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMain.ItemDataBound

            Try
                If (Not IsDBNull(e)) And (Not IsDBNull(e.Item)) And TypeOf e.Item Is Telerik.Web.UI.GridDataItem Then

                    Dim dataItem As Telerik.Web.UI.GridDataItem = CType(e.Item, Telerik.Web.UI.GridDataItem)
                    Dim lblItemCode As Label = CType(e.Item.FindControl("lblItemCode"), Label)

                    With DirectCast(e.Item.FindControl("lnkProduct"), System.Web.UI.WebControls.HyperLink)

                        If Not String.IsNullOrEmpty(NavigateURLFormatField) Then
                            '.NavigateUrl = _prodNavigatorHelper.FormatNavigationUrl(CType(e.Item.DataItem, DataRowView))
                            '--------------by vaishali
                            Dim lPersonID As Long = -1
                            Dim sCampaignURL As String = String.Empty
                            If User1.UserID > 0 Then
                                lPersonID = Me.User1.PersonID
                            End If
                            If Request.QueryString.Count > 0 AndAlso Not String.IsNullOrEmpty(Request.QueryString("cID")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("PersonID")) Then
                                Dim lCampaignID As Long = Convert.ToInt64(Request.QueryString("cID"))
                                lPersonID = Convert.ToInt64(Request.QueryString("PersonID"))
                                'sCampaignURL = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ID")) + " &cID=" + Request.QueryString("cID") + "&PersonID=" + Request.QueryString("PersonID") + "&lID=" + Request.QueryString("lID") + "&Source=" + Request.QueryString("Source")
                                sCampaignURL = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ID")) + " &cID=" + Request.QueryString("cID") + "&PersonID=" + Request.QueryString("PersonID") + "&lID=" + Request.QueryString("lID") + "&Source=" + Request.QueryString("Source") + "&WebClickCreated=1" 'RS
                                '.NavigateUrl = String.Format(NavigateURLFormatField, sCampaignURL.ToString)
                            End If
                            '--------------end by vaishali
                        Else
                            grdMain.Enabled = False
                            grdMain.ToolTip = "NavigateURLFormatField property not set."
                        End If
                    End With

                    With DirectCast(e.Item.FindControl("lblItemCodeVal"), Label)
                        If Not String.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "Code").ToString()) Then
                            .Text = DataBinder.Eval(e.Item.DataItem, "Code").ToString()
                            .Visible = True
                            If lblItemCode IsNot Nothing Then
                                lblItemCode.Visible = True
                            End If
                        Else
                            .Visible = False
                            If lblItemCode IsNot Nothing Then
                                lblItemCode.Visible = False

                            End If
                        End If
                    End With

                    '' Paresh <Performance> Moved to client side
                    'With DirectCast(e.Item.FindControl("lblPriceForYouVal"), Label)
                    '    Dim oPrice As Aptify.Applications.OrderEntry.IProductPrice.PriceInfo = Me.GetProductPrice(CLng(DataBinder.Eval(e.Item.DataItem, "ID").ToString()))
                    '    If Not String.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "ID").ToString()) Then
                    '        Dim ProductID = Convert.ToInt64(DataBinder.Eval(e.Item.DataItem, "ID"))
                    '        .Text = Format(oPrice.Price, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                    '    End If
                    '    '----By Vaishali
                    '    If Request.QueryString.Count > 0 AndAlso Not String.IsNullOrEmpty(Request.QueryString("cID")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("PersonID")) Then
                    '        Dim dPrice As Decimal
                    '        Dim ProductID = Convert.ToInt64(DataBinder.Eval(e.Item.DataItem, "ID"))
                    '        Dim lCampaignID As Long = Convert.ToInt64(Request.QueryString("cID"))
                    '        Dim lPersonID As Long = Convert.ToInt64(Request.QueryString("PersonID"))
                    '        If User1.UserID > 0 Then
                    '            dPrice = GetProductPriceWithCampaign(ProductID, lCampaignID, User1.PersonID, User1.PersonID)
                    '        Else
                    '            dPrice = GetProductPriceWithCampaign(ProductID, lCampaignID, lPersonID, lPersonID)
                    '        End If
                    '        .Text = Format(dPrice, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                    '    Else
                    '        .Text = Format(oPrice.Price, ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID))
                    '    End If
                    '    '-----------end 
                    'End With

                    'Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
                    With DirectCast(e.Item.FindControl("ImgProd"), Image)
                        If Not IsDBNull(DataBinder.Eval(e.Item.DataItem, "WebImage")) AndAlso
                   Len(DataBinder.Eval(e.Item.DataItem, "WebImage")) > 0 Then
                            .ImageUrl = DataBinder.Eval(e.Item.DataItem, "WebImage").ToString
                        Else
                            .ImageUrl = ImageNotAvailable
                        End If
                    End With
                    'End of addition by Suvarna D IssueID-12745 to implement webImage feature to display images on Jan 19, 2012
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub grdMain_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
            LoadAllGrids()
        End Sub


        ''' <summary>
        ''' Nalini issue 12436 date:1/12/2011
        ''' </summary>Here is the problem for paging because the control record id is comes from product category page and on paging event the control record is not found.
        ''' <remarks></remarks>
        Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdMain.PageIndexChanged
            LoadAllGrids()
        End Sub

#Region "Custom Methods"
        'To get price using campaign added by asmita
        Private Function GetProductPriceWithCampaign(ByVal ProductID As Long, ByVal CampaignID As Long, ByVal shipToID As Long, ByVal billToID As Long) As Decimal

            Dim dPrice As Decimal = 0
            Try
                oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                oOrder.BillToID = billToID
                oOrder.ShipToID = shipToID
                oOrder.SetValue("CampaignCodeID", CampaignID)
                'oOrder.CampaignCodeID = CampaignID
                oOrder.AddProduct(ProductID)
                dPrice = Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended"))
                oOrder = Nothing
            Catch ex As Exception
                oOrder = Nothing
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return dPrice
        End Function

        ''' <summary>
        ''' Bind the Products to grid as per the person preferred currency
        ''' </summary>
        ''' <param name="CategoryID"></param>
        ''' <param name="ExcludeProductID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function BindLoadGrid(CategoryID As Long, ExcludeProductID As Long) As DataTable
            Try
                Dim CurrencyType As String = "Euro"
                If User1.UserID > 0 Then
                    If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                        CurrencyType = "Euro"
                        'RajeshK -071014
                    ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                        CurrencyType = AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c")
                    End If
                End If
                Dim sProdctsList As String = Database & "..SpGetProductForPreferredCurrency__c"
                Dim params(3) As IDataParameter
                params(0) = DataAction.GetDataParameter("@CurrencyType", SqlDbType.VarChar, CurrencyType)
                params(1) = DataAction.GetDataParameter("@ExcludeProductID", SqlDbType.Int, ExcludeProductID)
                params(2) = DataAction.GetDataParameter("@CategoryID", SqlDbType.Int, CategoryID)
                params(3) = DataAction.GetDataParameter("@ShowMeetingsLinkToClass", SqlDbType.Bit, ShowMeetingsLinkToClass)
                Dim dtProductList As DataTable = DataAction.GetDataTableParametrized(sProdctsList, CommandType.StoredProcedure, params)
                Return dtProductList
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return Nothing
            End Try
        End Function
#End Region
#Region " CAI Custom Methods ADD TO CART button"

        ' Code added by Govind on 6May2016
        Protected Sub grdMain_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdMain.ItemCommand
            Try
                If e.CommandName = "AddToCart" Then
                    If Not String.IsNullOrEmpty(NavigateURLFormatField) Then

                        Dim lPersonID As Long = -1
                        Dim sCampaignURL As String = String.Empty
                        If User1.UserID > 0 Then
                            lPersonID = Me.User1.PersonID
                        End If
                        If Request.QueryString.Count > 0 AndAlso Not String.IsNullOrEmpty(Request.QueryString("cID")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("PersonID")) Then
                            Dim lCampaignID As Long = Convert.ToInt64(Request.QueryString("cID"))
                            lPersonID = Convert.ToInt64(Request.QueryString("PersonID"))
                            sCampaignURL = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ID")) + " &cID=" + Request.QueryString("cID") + "&PersonID=" + Request.QueryString("PersonID") + "&lID=" + Request.QueryString("lID") + "&Source=" + Request.QueryString("Source") + "&WebClickCreated=1" 'RS
                            Response.Redirect(String.Format(NavigateURLFormatField, sCampaignURL.ToString), False)
                        Else
                            Response.Redirect(GetUrlFromLinkControl(e.Item))
                        End If
                    Else
                        grdMain.Enabled = False
                        grdMain.ToolTip = "NavigateURLFormatField property not set."
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Function GetUrlFromLinkControl(item As GridItem) As String
            With DirectCast(item.FindControl("lnkProduct"), System.Web.UI.WebControls.HyperLink)
                Return .NavigateUrl
            End With

        End Function
        'code added by Govind 06/05/2016
#End Region

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
            impressionDto.Products = GetProducts()

            impression = New Impression(Me.Page, impressionDto)
            impression.Render()

        End Sub

        Private Sub AddGoogleTagClicks()
            Dim gtmClick As GtmClick

            gtmClick = New GtmClick(Me.Page)

            gtmClick.Render()
        End Sub

        Private Function GetProducts() As ICollection(Of ProductDto)

            Dim dt As DataTable = CType(grdMain.DataSource, DataTable)
            Dim position As Integer = 0

            If (dt Is Nothing) Then
                Return _products
            End If

            For Each row As DataRow In dt.Rows

                _products.Add(New ProductDto With
                             {
                             .Name = row("WebName").ToString,
                             .Id = row("ID").ToString,
                             .Price = Nothing,
                             .Brand = "Chartered Accountants",
                             .Category = row("ProductCategory").ToString,
                             .Variant = "",
                             .List = "",
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


            url = String.Format(NavigateURLFormatField, dtRow("ID").ToString)

            If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                currencyType = "EUR"
            ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                currencyType = "GBP"
            End If

            Return "trackGTMClick('" +
                System.Web.HttpUtility.HtmlEncode(dtRow("WebName").ToString) + "','" +
                   dtRow("ID").ToString + "','" +
                   "" + "','" +
                   "Chartered Accountants" + "','" +
                   dtRow("ProductCategory").ToString + "','" +
                   "" + "','" +
                   _clickPosition.ToString + "','" +
                   url + "','" +
                   currencyType + "', event" +
                   ")"

        End Function

        Protected Sub LoadAllGrids()
            Me._prodNavigatorHelper = New ProdNavigatorHelper(NavigateURLFormatField, ViewClassPage)
            If Me.ControlRecordID > 0 Then
                LoadGrid(ControlRecordID)
                AddGoogleTagImpressions()
                AddGoogleTagClicks()
            End If
            lblProdCatHeader.InnerText = Me.ProdCatHeader
        End Sub
    End Class
End Namespace
