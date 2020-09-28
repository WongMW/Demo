
'-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
'DEVELOPER                              DATE                                        Comments
'Ganesh I                               23/03/2014                              For Putting Custom OrderSummery__c control
'----------------------------------------------------------------------------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On

Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic.ProcessPipeline
Imports Aptify.Applications.OrderEntry
Imports SoftwareDesign.GTM
Imports SoftwareDesign.GTM.Model

Namespace Aptify.Framework.Web.eBusiness.ProductCatalog
    Partial Class CheckOutControl__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage"
        Protected Const ATTRIBUTE_NEXT_BUTTON_PAGE As String = "NextButtonPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "CheckOutControl"
        Protected Const ATTRIBUTE_CONFIRMATION_PAGE As String = "OrderConfirmationPage"
        Protected Const ATTRIBUTE_BILLING_CHANGE_PAGE As String = "BillingChangePage"

#Region "CheckOutControl Specific Properties"
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
        ''' <summary>
        ''' NextButton page url
        ''' </summary>
        Public Overridable Property NextButtonPage() As String
            Get
                If Not ViewState(ATTRIBUTE_NEXT_BUTTON_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_NEXT_BUTTON_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_NEXT_BUTTON_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        'Nalini Issue 11858
        Public Overridable Property CheckOutControl() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTORL_DEFAULT_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTORL_DEFAULT_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTORL_DEFAULT_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' OrderConfirmation page url
        ''' </summary>
        Public Overridable Property OrderConfirmationPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONFIRMATION_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONFIRMATION_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONFIRMATION_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' BillingChange page url
        ''' </summary>
        Public Overridable Property BillingChangePage() As String
            Get
                If Not ViewState(ATTRIBUTE_BILLING_CHANGE_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_BILLING_CHANGE_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_BILLING_CHANGE_PAGE) = Me.FixLinkForVirtualPath(value)
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
            If String.IsNullOrEmpty(NextButtonPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                NextButtonPage = Me.GetLinkValueFromXML(ATTRIBUTE_NEXT_BUTTON_PAGE)
                If String.IsNullOrEmpty(NextButtonPage) Then
                    Me.cmdNextStep.Enabled = False
                    Me.cmdNextStep.ToolTip = "NextButtonPage property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(OrderConfirmationPage) Then
                OrderConfirmationPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONFIRMATION_PAGE)
            End If
            'Nalini Issue 11858
            If String.IsNullOrEmpty(CheckOutControl) Then
                CheckOutControl = Me.GetLinkValueFromXML(ATTRIBUTE_CONTORL_DEFAULT_NAME)
            End If
            If String.IsNullOrEmpty(BillingChangePage) Then
                'since value is the 'default' check the XML file for possible custom setting
                BillingChangePage = Me.GetLinkValueFromXML(ATTRIBUTE_BILLING_CHANGE_PAGE)
                'Me.lnkChangeAddress.ImageUrl = BillingChangeImage
                If String.IsNullOrEmpty(BillingChangePage) Then
                    Me.lnkChangeAddress.Enabled = False
                    Me.lnkChangeAddress.ToolTip = "BillingChangePage property has not been set."
                Else
                    If NameAddressBlock.AddressLine1.Trim <> "" Then
                        Me.lnkChangeAddress.PostBackUrl = BillingChangePage & "?Type=Billing&ReturnToPage=" & Request.Path
                    Else
                        Me.lnkChangeAddress.PostBackUrl = BillingChangePage & "?Type=Billing&IsHome=Y&ReturnToPage=" & Request.Path
                    End If
                End If
            Else
                If NameAddressBlock.AddressLine1.Trim <> "" Then
                    Me.lnkChangeAddress.PostBackUrl = BillingChangePage & "?Type=Billing&ReturnToPage=" & Request.Path
                Else
                    Me.lnkChangeAddress.PostBackUrl = BillingChangePage & "?Type=Billing&IsHome=Y&ReturnToPage=" & Request.Path
                End If
            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            Try
                If Not IsPostBack Then
                    If User1.UserID > 0 Then
                        ' There is a user logged in, go to the cart
                        LoadShipmentType()
                        'Added by dipali 04/07/2017

                        Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                        oOrder = ShoppingCart.GetOrderObject(Session, Page.User, Application)

                        Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                        Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If iFirmAdmin > 0 Then

                            oOrder.SetValue("FirmPay__c", 1)
                            Session("IsFirmPay") = True
                            chkCompanyStatement.Checked = True
                            'If oOrder.SubTypes("OrderLines") IsNot Nothing AndAlso oOrder.SubTypes("OrderLines").Count > 0 
                            '    For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                            '        oOrder.SubTypes("OrderLines").Item(i).SetValue("BillToCompanyID__c", oOrder.BillToCompanyID)
                            '    Next
                            'Else
                            '    oOrder.SetValue("BillToSameAsShipTo", 0)
                            '    oOrder.SetValue("BillToCompanyID", -1)
                            '    oOrder.SetValue("FirmPay__c", 0)
                            '    If oOrder.SubTypes("OrderLines") IsNot Nothing AndAlso oOrder.SubTypes("OrderLines").Count > 0 Then
                            '        For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                            '            oOrder.SubTypes("OrderLines").Item(i).SetValue("BillToCompanyID__c", -1)
                            '        Next
                            '    End If
                            'End If

                            CartGrid.UpdateCart()
                            ShoppingCart.SaveCart(Page.Session)
                            'ShoppingCart.SaveCart(Session)
                        Else
                            Dim iCompanyID As Long = User1.CompanyID
                            If iCompanyID > 0 Then
                                'end dipali
                                chkCompanyStatement.Visible = True
                                ViewState("CompanyID") = iCompanyID


                                ' If CBool(Session("IsFirmPay")) = True OrElse Convert.ToBoolean(oOrder.GetValue("FirmPay__c")) Then
                                'chkCompanyStatement.Checked = True
                                'End If
                                'Commented by dipali 04/07/2017
                                'Dim oOrder As AptifyGenericEntityBase
                                'oOrder = ShoppingCart.GetOrderObject(Session, Page.User, Application)

                                'If CBool(Session("IsFirmPay")) = True OrElse Convert.ToBoolean(oOrder.GetValue("FirmPay__c")) Then
                                '    chkCompanyStatement.Checked = True
                                'End If
                                'Else
                                '    'Commented by dipali 04/07/2017
                                '    'Dim oOrder As AptifyGenericEntityBase
                                '    'oOrder = ShoppingCart.GetOrderObject(Session, Page.User, Application)
                                '    If CBool(Session("IsFirmPay")) = True OrElse Convert.ToBoolean(oOrder.GetValue("FirmPay__c")) Then
                                '        chkCompanyStatement.Checked = True
                                '        chkCompanyStatement.Visible = True
                                '        Session("FirmPay__c") = 1
                                '        chkCompanyStatement.Enabled = False
                                '    End If
                            End If
                        End If


                        'commented by dipali
                        ' Added by Govind M for Shopping cart changes
                        'Dim sSQL As String = Database & "..spCheckIsIndividualPerson__c @PersonID=" & User1.PersonID
                        'Dim iCompanyID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        'Added by dipali 04/07/2017



                        LoadBillAddress()
                        RefreshGrid()
                        ' code added for #16223
                        If NameAddressBlock.AddressLine1.Trim <> "" Then
                            Me.lnkChangeAddress.PostBackUrl = BillingChangePage & "?Type=Billing&ReturnToPage=" & Request.Path
                        Else
                            Me.lnkChangeAddress.PostBackUrl = BillingChangePage & "?Type=Billing&IsHome=Y&ReturnToPage=" & Request.Path
                        End If

                        AddGtmCheckout()

                    Else
                        ' No User is logged in, redirect to the
                        ' Login Page in Customer Service
                        'Suraj S Issue 15370, 3/29/13 change session variale to application variable
                        Session("ReturnToPage") = Request.RawUrl
                        ' ''Added BY Pradip 2016-01-23 for issue https://redmine.softwaredesign.ie/issues/15798
                        Session("IsFromLogin") = "YES"
                        Response.Redirect(LoginPage)

                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadBillAddress()
            Try
                Dim oPerson As AptifyGenericEntityBase
                oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                Dim oOrder As AptifyGenericEntityBase
                oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
                If Len(oOrder.GetValue("ShipToAddrLine1")) = 0 Then
                    'LoadDefaultAddress("ShipTo", oOrder)
                    LoadDefaultAddress("ShipTo", oOrder, Convert.ToString(oPerson.GetValue("PreferredShippingAddress")))
                End If
                ' Vijay Sitlani - Changes made to partially resolve the bug reported by Alina for Issue 5583
                ' Changes made on 01-25-2008
                If Len(oOrder.GetValue("BillToAddrLine1")) = 0 Then
                    'LoadDefaultAddress("BillTo", oOrder)
                    LoadDefaultAddress("BillTo", oOrder, Convert.ToString(oPerson.GetValue("PreferredBillingAddress")))
                End If
                With User1
                    NameAddressBlock.Name = .FirstName & " " & .LastName
                    ''If Len(.Company) > 0 Then
                    ''    NameAddressBlock.Name = NameAddressBlock.Name & "/" & .Company
                    ''End If
                    If CBool(Session("IsFirmPay")) = True OrElse CBool(Session("FirmPay__c")) Then
                        NameAddressBlock.Name = .Company
                    End If
                End With
                If CBool(Session("IsFirmPay")) = True OrElse CBool(Session("FirmPay__c")) Then
                    Dim sSql As String = Database & "..spGetCompanyStreetAddress__c @CompanyID=" & User1.CompanyID
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        NameAddressBlock.AddressLine1 = Convert.ToString(dt.Rows(0)("AddressLine1"))
                        NameAddressBlock.AddressLine2 = Convert.ToString(dt.Rows(0)("AddressLine2"))
                        NameAddressBlock.AddressLine3 = Convert.ToString(dt.Rows(0)("AddressLine3"))
                        NameAddressBlock.City = Convert.ToString(dt.Rows(0)("City"))
                        NameAddressBlock.State = Convert.ToString(dt.Rows(0)("State"))
                        NameAddressBlock.ZipCode = Convert.ToString(dt.Rows(0)("ZipCode"))
                        NameAddressBlock.Country = Convert.ToString(dt.Rows(0)("Country"))
                    End If
                Else
                    With oOrder
                        NameAddressBlock.AddressLine1 = CStr(.GetValue("BillToAddrLine1"))
                        NameAddressBlock.AddressLine2 = CStr(.GetValue("BillToAddrLine2"))
                        NameAddressBlock.AddressLine3 = CStr(.GetValue("BillToAddrLine3"))
                        NameAddressBlock.City = CStr(.GetValue("BillToCity"))
                        NameAddressBlock.State = CStr(.GetValue("BillToState"))
                        NameAddressBlock.ZipCode = CStr(.GetValue("BillToZipCode"))
                        NameAddressBlock.Country = CStr(.GetValue("BillToCountry"))
                    End With
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadDefaultAddress(ByVal sPrefix As String, _
                                     ByRef oOrder As AptifyGenericEntityBase, _
                                     ByVal PrefAddress As String)
            Try
                Dim oPerson As AptifyGenericEntityBase
                oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                ' With User1
                With oPerson
                    Select Case PrefAddress.Trim
                        Case "Home Address"
                            oOrder.SetValue(sPrefix & "AddrLine1", .GetValue("HomeAddressLine1"))
                            oOrder.SetValue(sPrefix & "AddrLine2", .GetValue("HomeAddressLine2"))
                            oOrder.SetValue(sPrefix & "AddrLine3", .GetValue("HomeAddressLine3"))
                            oOrder.SetValue(sPrefix & "City", .GetValue("HomeCity"))
                            oOrder.SetValue(sPrefix & "State", .GetValue("HomeState"))
                            oOrder.SetValue(sPrefix & "ZipCode", .GetValue("HomeZipCode"))
                            oOrder.SetValue(sPrefix & "Country", .GetValue("HomeCountry"))
                        Case "Business Address"
                            oOrder.SetValue(sPrefix & "AddrLine1", .GetValue("AddressLine1"))
                            oOrder.SetValue(sPrefix & "AddrLine2", .GetValue("AddressLine2"))
                            oOrder.SetValue(sPrefix & "AddrLine3", .GetValue("AddressLine3"))
                            oOrder.SetValue(sPrefix & "City", .GetValue("City"))
                            oOrder.SetValue(sPrefix & "State", .GetValue("State"))
                            oOrder.SetValue(sPrefix & "ZipCode", .GetValue("ZipCode"))
                            oOrder.SetValue(sPrefix & "Country", .GetValue("Country"))
                        Case "Billing Address"
                            oOrder.SetValue(sPrefix & "AddrLine1", .GetValue("BillingAddressLine1"))
                            oOrder.SetValue(sPrefix & "AddrLine2", .GetValue("BillingAddressLine2"))
                            oOrder.SetValue(sPrefix & "AddrLine3", .GetValue("BillingAddressLine3"))
                            oOrder.SetValue(sPrefix & "City", .GetValue("BillingCity"))
                            oOrder.SetValue(sPrefix & "State", .GetValue("BillingState"))
                            oOrder.SetValue(sPrefix & "ZipCode", .GetValue("BillingZipCode"))
                            oOrder.SetValue(sPrefix & "Country", .GetValue("BillingCountry"))
                        Case "POBox Address"
                            oOrder.SetValue(sPrefix & "AddrLine1", .GetValue("POBox"))
                            oOrder.SetValue(sPrefix & "AddrLine2", .GetValue("POBoxAddressLine2"))
                            oOrder.SetValue(sPrefix & "AddrLine3", .GetValue("POBoxAddressLine3"))
                            oOrder.SetValue(sPrefix & "City", .GetValue("POBoxCity"))
                            oOrder.SetValue(sPrefix & "State", .GetValue("POBoxState"))
                            oOrder.SetValue(sPrefix & "ZipCode", .GetValue("POBoxZipCode"))
                            oOrder.SetValue(sPrefix & "Country", .GetValue("POBoxCountry"))
                        Case Else
                            oOrder.SetValue(sPrefix & "AddrLine1", .GetValue("AddressLine1"))
                            oOrder.SetValue(sPrefix & "AddrLine2", .GetValue("AddressLine2"))
                            oOrder.SetValue(sPrefix & "AddrLine3", .GetValue("AddressLine3"))
                            oOrder.SetValue(sPrefix & "City", .GetValue("City"))
                            oOrder.SetValue(sPrefix & "State", .GetValue("State"))
                            oOrder.SetValue(sPrefix & "ZipCode", .GetValue("ZipCode"))
                            oOrder.SetValue(sPrefix & "Country", .GetValue("Country"))
                    End Select
                    oOrder.SetValue(sPrefix & "AreaCode", .GetValue("PhoneAreaCode"))
                    oOrder.SetValue(sPrefix & "Phone", .GetValue("Phone"))
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub chkCompanyStatement_CheckedChanged(sender As Object, e As EventArgs) Handles chkCompanyStatement.CheckedChanged
            If chkCompanyStatement.Checked = True Then
                Session("IsFirmPay") = True
                With User1
                    NameAddressBlock.Name = .FirstName & " " & .LastName
                    If Len(.Company) > 0 Then
                        NameAddressBlock.Name = NameAddressBlock.Name & "/" & .Company

                    End If
                End With
                Dim oOrder As AptifyGenericEntityBase
                oOrder = ShoppingCart.GetOrderObject(Session, Page.User, Application)
                Dim CompanyID As Integer = Convert.ToInt32(ViewState("CompanyID"))
                Dim sSQl As String = Database & "..spGetCompanyStreetAddress__c @CompanyID=" & CompanyID
                Dim dt As DataTable = DataAction.GetDataTable(sSQl, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    NameAddressBlock.AddressLine1 = Convert.ToString(dt.Rows(0)("AddressLine1"))
                    NameAddressBlock.AddressLine2 = Convert.ToString(dt.Rows(0)("AddressLine2"))
                    NameAddressBlock.AddressLine3 = Convert.ToString(dt.Rows(0)("AddressLine3"))
                    NameAddressBlock.City = Convert.ToString(dt.Rows(0)("City"))
                    NameAddressBlock.State = Convert.ToString(dt.Rows(0)("State"))
                    NameAddressBlock.ZipCode = Convert.ToString(dt.Rows(0)("ZipCode"))
                    NameAddressBlock.Country = Convert.ToString(dt.Rows(0)("Country"))
                End If
                oOrder.SetValue("ShipToID", oOrder.GetValue("ShipToID)"))

                oOrder.SetValue("FirmPay__c", 1)
                oOrder.SetValue("BillToSameAsShipTo", 0)
                oOrder.SetValue("BillToID", oOrder.GetValue("BillToID)"))
                oOrder.SetValue("BillToCompanyID", CompanyID)

                If oOrder.SubTypes("OrderLines") IsNot Nothing AndAlso oOrder.SubTypes("OrderLines").Count > 0 Then
                    For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                        oOrder.SubTypes("OrderLines").Item(i).SetValue("BillToCompanyID__c", CompanyID)
                    Next
                End If
                ShoppingCart.SaveCart(Session)

            Else
                Session("IsFirmPay") = False
                Dim oOrder As AptifyGenericEntityBase
                oOrder = ShoppingCart.GetOrderObject(Session, Page.User, Application)
                With User1
                    NameAddressBlock.Name = .FirstName & " " & .LastName
                    ' If Len(.Company) > 0 Then
                    'NameAddressBlock.Name = NameAddressBlock.Name & "/" & .Company
                    'End If
                End With
                With oOrder
                    NameAddressBlock.AddressLine1 = CStr(.GetValue("ShipToAddrLine1"))
                    NameAddressBlock.AddressLine2 = CStr(.GetValue("ShipToAddrLine2"))
                    NameAddressBlock.AddressLine3 = CStr(.GetValue("ShipToAddrLine3"))
                    NameAddressBlock.City = CStr(.GetValue("ShipToCity"))
                    NameAddressBlock.State = CStr(.GetValue("ShipToState"))
                    NameAddressBlock.ZipCode = CStr(.GetValue("ShipToZipCode"))
                    NameAddressBlock.Country = CStr(.GetValue("ShipToCountry"))
                End With
                oOrder.SetValue("ShipToID", oOrder.GetValue("ShipToID)"))
                oOrder.SetValue("BillToSameAsShipTo", 0)
                oOrder.SetValue("BillToID", oOrder.GetValue("BillToID)"))
                oOrder.SetValue("BillToCompanyID", -1)
                oOrder.SetValue("FirmPay__c", 0)
                If oOrder.SubTypes("OrderLines") IsNot Nothing AndAlso oOrder.SubTypes("OrderLines").Count > 0 Then
                    For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                        oOrder.SubTypes("OrderLines").Item(i).SetValue("BillToCompanyID__c", -1)
                    Next
                End If
                ShoppingCart.SaveCart(Session)
                ''End If
            End If
            'MyBase.Response.Redirect(RedirectPage, False)

            'MyBase.Response.Redirect("~/ProductCatalog/CheckOut.aspx", False)
        End Sub
        Private Sub RefreshGrid()
            Try
                CartGrid.RefreshGrid()
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                oOrder = CartGrid.Cart.GetOrderObject(Session, Page.User, Page.Application)
                'Navin Prasad Issue 11032
                If CartGrid.Grid.Items.Count > 0 Then
                    cmdUpdateCart.Visible = True
                    cmdNextStep.Enabled = True
                    tblRowMain.Visible = True
                    lblGotItems.Visible = True
                    lblNoItems.Visible = False
                    divTotals.Visible = True
                Else
                    cmdUpdateCart.Visible = False
                    cmdNextStep.Enabled = False
                    tblRowMain.Visible = False
                    lblGotItems.Visible = False
                    lblNoItems.Visible = True
                    divTotals.Visible = False
                End If
                Dim sCurrencyFormat As String
                With CartGrid.Cart
                    sCurrencyFormat = .GetCurrencyFormat(.CurrencyTypeID)
                    lblSubTotal.Text = Format$(.SubTotal, sCurrencyFormat)
                    lblShipping.Text = Format$(.ShippingAndHandlingCharges, sCurrencyFormat)
                    lblTax.Text = Format$(.Tax, sCurrencyFormat)
                    lblGrandTotal.Text = Format$(.GrandTotal, sCurrencyFormat)
                    'Me.OrderSummary1.Refresh()
                    If CartGrid.Grid.Items.Count > 0 AndAlso ShoppingCart.GrandTotal = 0 Then
                        cmdNextStep.Text = "Complete Order"
                        lblcheckoutMsg.Text = "Your default shipping address and other settings are shown below. Use the buttons to make any changes. When you're done, click the " & "<b>Complete Order</b> button."
                    Else
                        lblcheckoutMsg.Text = "The default billing and shipping addresses are shown below. If you are logged in on behalf of a business or firm, you will have the option to tick the box to request a company receipt. If the company is VAT exempt, please tick the box to request this receipt by email."
                    End If
                    'Redmine #16424
                    Dim dSalesTaxAmt As Decimal
                    For i As Integer = 0 To oOrder.SalesTaxAmounts.Keys.Count - 1
                        Dim sssql As String = Database & "..spCheckTaxonShipping__c @SalesTaxID=" & Convert.ToInt32(oOrder.SalesTaxAmounts.Keys(i))
                        Dim istTaxOnShipping As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sssql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If istTaxOnShipping > 0 Then
                            If dSalesTaxAmt = 0 Then
                                dSalesTaxAmt = oOrder.SalesTaxAmounts.Values(i).TaxAmount
                            Else
                                dSalesTaxAmt += oOrder.SalesTaxAmounts.Values(i).TaxAmount
                            End If
                        End If

                    Next
                    If dSalesTaxAmt > 0 Then
                        lblTax.Text = Format$((.Tax - dSalesTaxAmt), sCurrencyFormat)
                        lblShipping.Text = Format$((.ShippingAndHandlingCharges + dSalesTaxAmt), sCurrencyFormat)
                    End If
                    ' Below coded added for web shopping cart changes
                    ''Dim sSQL As String = Database & "..spCheckCompanyExemptTax__c @CompanyID=" & User1.CompanyID
                    ''Dim bCompanyExempt As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    ''If bCompanyExempt Then
                    ''lblShipping.Text = Format$((.ShippingAndHandlingCharges + .Tax), sCurrencyFormat)
                    ''lblTax.Text = Format$(0, sCurrencyFormat)
                    ''End If
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub CompleteOrderforFreeProduct()
            Dim lOrderID As Long, sError As String
            Dim iPOPaymentType As Integer
            Try
                If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID")) Then
                    iPOPaymentType = CInt(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID"))
                Else
                    iPOPaymentType = 1
                End If
                'Code added by Govind M for Zero amount payment removed BilltoCompany
                Dim oOrders As OrdersEntity
                oOrders = ShoppingCart.GetOrderObject(Session, Page.User, Application)
                oOrders.SetValue("PayTypeID", iPOPaymentType)
                oOrders.SetValue("OrderType", "Regular")
                oOrders.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web"))

                'If Convert.ToString(hidIsFirmAdmin.Value) > 0 Then
                If CBool(Session("IsFirmPay")) = True Then
                    oOrders.SetValue("FirmPay__c", 1)
                End If
                'redmine 16921
                If Session("EID") IsNot Nothing AndAlso Convert.ToInt32(Session("EID")) > 0 Then
                    oOrders.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(Session("EID")))
                End If
                ' below code added by Govind M for Redmine Log #18675
                For Each oOL As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrders.SubTypes("OrderLines")
                    If oOL.ProductType = "Meeting" Then
                        If Convert.ToInt32(oOL.ExtendedOrderDetailEntity.GetValue("AttendeeID")) > 0 Then
                        Else
                            '' Commented code by Govind M for Redmine Log #18859
                            '' oOL.ExtendedOrderDetailEntity.SetValue("AttendeeID", User1.PersonID)
                            '' oOL.SetAddValue("__ExtendedAttributeObjectData", oOL.ExtendedOrderDetailEntity.GetObjectData(False))
                        End If
                    End If
                Next
                ' End #18675	
                ShoppingCart.SaveCart(Session)
                If oOrders.Save(False, sError) Then
                    lOrderID = oOrders.RecordID
                End If
                'With ShoppingCart.GetOrderObject(Session, Page.User, Application)
                '.SetValue("PayTypeID", iPOPaymentType)
                ' ShoppingCart.SaveCart(Session)
                'lOrderID = ShoppingCart.PlaceOrder(Session, Application, Page.User, sError)
                'End With
                If lOrderID > 0 Then
                    ' Navin Prasad Issue 9388
                    If PlaceDownloadProduct(lOrderID, sError) Then
                        SendReadyForDownloadEmail(lOrderID)
                    ElseIf Len(sError) > 0 Then
                        ProductDownloadFailed(sError)
                    End If
                    'Code added to ship entire order - with auto fulfillment check - for 0 payment - 4/12/2017 - Vaishali
                    ' check if web shopping cart entity auto shipment flag true
                    'Comment below line not to ship oredr as it ships order automatically 7/9/2017 - Vaishali
                    'AutoShipWebOrder(lOrderID)
                    '------------------------
                    ShoppingCart.Clear(Nothing)
                    Session("IsFirmPay") = False
                    Response.Redirect(OrderConfirmationPage & "?ID=" & lOrderID)
                End If

            Catch ex As Exception

            End Try
        End Sub

        Protected Overridable Sub AutoShipWebOrder(ByVal lOrderID As Long)
            Try
                Dim OrderGE As OrdersEntity = TryCast(AptifyApplication.GetEntityObject("Orders", lOrderID), OrdersEntity)
                If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "AutoShipNonFulfillmentProducts")) Then
                    Dim iAutoShip As Integer = Convert.ToInt32(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "AutoShipNonFulfillmentProducts"))
                    If iAutoShip = 1 Then
                        If OrderGE.AvailableForShipping(True) = Aptify.Applications.OrderEntry.OrdersEntity.AutoShippingAvailabilityTypes.FullOrder Then
                            'Order qualifies for Autoshipping, does the user need to be prompted?
                            If Not OrderGE.ShipEntireOrder(False) Then
                                'essentially, autoshipping failures are logged but don't prevent the save of the order
                                Dim sMessage As String = "Autoshipment of Order " & OrderGE.RecordID & " failed.  The Order has been saved but not shipped.  The Order can be manually shipped at some point in the future."
                                Dim sMoreMessage As String = OrderGE.LastError

                                If sMoreMessage IsNot Nothing AndAlso sMoreMessage.Length > 0 Then
                                    sMessage = sMessage & Environment.NewLine & "Error:  " & sMoreMessage
                                End If
                                lblError.Text = sMessage
                                lblError.Visible = True
                                ExceptionManagement.ExceptionManager.Publish(New Aptify.Applications.OrderEntry.OrderException(sMessage))
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub cmdUpdateCart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdateCart.Click
            ' update the shopping cart object
            ' Iterate through all rows within shopping cart list
            Try
                CartGrid.UpdateCart()
                RefreshGrid()
                'Nalini Issue 11858
                MyBase.Response.Redirect(CheckOutControl, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub cmdNextStep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNextStep.Click
            ' go to the billing page
            ''Added BY Pradip 2016-09-01 for Redmine issue https://redmine.softwaredesign.ie/issues/15073
            Dim lblAddLine1 As Label = DirectCast(ShippingControl.FindControl("NameAddressBlock").FindControl("lblAddressLine1"), Label)
            If lblAddLine1.Text.Trim <> "" Then
                If ShoppingCart.GrandTotal > 0 Then
                    Response.Redirect(NextButtonPage)
                Else
                    Me.CompleteOrderforFreeProduct()
                End If
            Else
                lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.CheckOutPage.ShippingAddressValidation")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblError.Visible = True
            End If
            'If ShoppingCart.GrandTotal > 0 Then
            '    Response.Redirect(NextButtonPage)
            'Else
            '    Me.CompleteOrderforFreeProduct()
            'End If
        End Sub

        'Navin Prasad Issue 9388
        Private Sub ProductDownloadFailed(ByVal sError As String)
            lblError.Text = "<b>Failed To Create Downloable Product Info</b><BR /><hr />" & sError
            lblError.Visible = True
        End Sub
        Protected Overridable Function PlaceDownloadProduct(ByVal lOrderID As Long, ByVal ErrorString As String) As Boolean
            Try
                Dim sErrorString As String = ""
                Dim OOrderGE As AptifyGenericEntityBase
                Dim oProductGE As AptifyGenericEntityBase
                Dim ODownloadItemGE As AptifyGenericEntityBase
                Dim oProductDownloadsGE As AptifyGenericEntityBase
                OOrderGE = AptifyApplication.GetEntityObject("Orders", lOrderID)
                If OOrderGE IsNot Nothing AndAlso OOrderGE.SubTypes("OrderLines") IsNot Nothing Then
                    For i As Integer = 0 To OOrderGE.SubTypes("OrderLines").Count - 1
                        With OOrderGE.SubTypes("OrderLines").Item(i)
                            oProductGE = AptifyApplication.GetEntityObject("Products", CLng(OOrderGE.SubTypes("OrderLines").Item(i).GetValue("ProductID")))
                            If oProductGE IsNot Nothing AndAlso oProductGE.GetValue("IncludesDownload") IsNot Nothing _
                                AndAlso Convert.ToBoolean(oProductGE.GetValue("IncludesDownload")) AndAlso oProductGE.GetValue("DownloadItemID") IsNot Nothing _
                                AndAlso CLng(oProductGE.GetValue("DownloadItemID")) > 0 Then
                                ODownloadItemGE = AptifyApplication.GetEntityObject("DownloadItems", CLng(oProductGE.GetValue("DownloadItemID")))
                                If ODownloadItemGE.GetValue("Active") IsNot Nothing AndAlso Convert.ToBoolean(ODownloadItemGE.GetValue("Active")) Then
                                    'Create a record in "ProductDownloads" entity
                                    oProductDownloadsGE = AptifyApplication.GetEntityObject("ProductDownloads", -1)
                                    With oProductDownloadsGE
                                        .SetValue("OrderID", lOrderID)
                                        '	PersonID (OrderLines.ShipToID or Orders.ShipToID if Orderline.ShipToID is blank
                                        If OOrderGE.SubTypes("OrderLines").Item(i).GetValue("ShipToID") IsNot Nothing AndAlso _
                                             CLng(OOrderGE.SubTypes("OrderLines").Item(i).GetValue("ShipToID")) > 0 Then
                                            .SetValue("PersonID", OOrderGE.SubTypes("OrderLines").Item(i).GetValue("ShipToID"))
                                        Else
                                            .SetValue("PersonID", OOrderGE.GetValue("ShipToID"))
                                        End If
                                        .SetValue("ProductID", OOrderGE.SubTypes("OrderLines").Item(i).GetValue("ProductID"))
                                        .SetValue("DownloadItemID", oProductGE.GetValue("DownloadItemID"))
                                        .SetValue("ShipDate", OOrderGE.GetValue("OrderDate"))
                                        If oProductGE.GetValue("DownloadExpirationDays") IsNot Nothing Then
                                            .SetValue("DownloadExpirationDays", oProductGE.GetValue("DownloadExpirationDays"))
                                            If Not String.IsNullOrEmpty(CStr(oProductGE.GetValue("DownloadExpirationDays"))) Then
                                                Dim ddexpDate As DateTime = CDate(OOrderGE.GetValue("OrderDate"))
                                                ddexpDate = ddexpDate.AddDays(CLng(oProductGE.GetValue("DownloadExpirationDays")))
                                                .SetValue("DownloadExpirationDate", ddexpDate)
                                            End If
                                        End If
                                        oProductDownloadsGE.SetValue("MaxNumDownload", oProductGE.GetValue("MaxNumDownload"))
                                        sErrorString = ""
                                        If oProductDownloadsGE.Save(sErrorString) Then
                                            ''sends the Product Downloads entity’s PersonID an email
                                            '' that states that there is an item ready for download, includes a 
                                            ''URL to the “My Downloads” page, and includes the Download Item password 
                                            ''if one is specified on the Download Items record
                                            'SendReadyForDownloadEmail(CLng(oProductDownloadsGE.GetValue("PersonID")), CLng(oProductDownloadsGE.GetValue("OrderID")))
                                            PlaceDownloadProduct = True ' send the mail
                                        Else
                                            ErrorString += sErrorString
                                        End If
                                    End With
                                End If
                            End If
                        End With
                    Next
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        Protected Overridable Sub SendReadyForDownloadEmail(ByVal lOrderID As Long)
            Try
                Dim lProcessFlowID As Long, lMessageSourceID As Long, lMessageTemplateID As Long
                'Get the Process Flow ID to be used for sending the Downloadable Order Confirmation Email
                Dim sSQL As String = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='Send Downloadable Order Confirmation Email'"
                Dim oProcessFlowID As Object = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache)
                If oProcessFlowID IsNot Nothing AndAlso IsNumeric(oProcessFlowID) Then
                    lProcessFlowID = CLng(oProcessFlowID)
                    ' Dim lProcessFlowID As Long = CLng(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache))
                    sSQL = "SELECT ID FROM " & Database & "..vwMessageSources WHERE Name='Orders'"
                    Dim oMessageSourceID As Object = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache)
                    If oMessageSourceID IsNot Nothing AndAlso IsNumeric(oMessageSourceID) Then
                        lMessageSourceID = CLng(oMessageSourceID)
                        sSQL = "SELECT ID FROM " & Database & "..vwMessageTemplates WHERE Name='Downloadable Order Confirmation Email'"
                        Dim oMessageTemplateID As Object = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache)
                        If oMessageTemplateID IsNot Nothing AndAlso IsNumeric(oMessageTemplateID) Then
                            lMessageTemplateID = CLng(oMessageTemplateID)
                            Dim context As New AptifyContext
                            context.Properties.AddProperty("MessageSourceID", lMessageSourceID)
                            context.Properties.AddProperty("MessageTemplateID", lMessageTemplateID)
                            context.Properties.AddProperty("OrderID", lOrderID)
                            Dim oResult As ProcessFlowResult
                            oResult = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
                            If Not oResult.IsSuccess Then
                                ExceptionManagement.ExceptionManager.Publish(New Exception("Process flow to send downloadable order confirmation email failed. Please refer event handler for more details."))
                            End If
                        Else
                            ExceptionManagement.ExceptionManager.Publish(New Exception("Message Template to send downloadable order confirmation email is not found in the system."))
                        End If
                    Else
                        ExceptionManagement.ExceptionManager.Publish(New Exception("Message Source to send downloadable order confirmation email is not found in the system."))
                    End If
                Else
                    ExceptionManagement.ExceptionManager.Publish(New Exception("Process flow to send downloadable order confirmation email is not found in the system."))
                End If

            Catch ex As ArgumentException
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Rashmi P, Issue 5133, 12/6/12 Add ShipmentType Selection.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadShipmentType()
            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
            oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
            Dim oShipmentTypes As New Aptify.Framework.Web.eBusiness.CommonMethods(DataAction, AptifyApplication, User1, Database)
            Dim bIncludeInShipping As Boolean
            Dim dt As DataTable = Nothing
            Try
                If oOrder IsNot Nothing Then
                    For Each oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrder.SubTypes("OrderLines")
                        bIncludeInShipping = oShipmentTypes.IncludeInShipping(CLng(oOrderLine.GetValue("ProductID")))
                        If bIncludeInShipping = True Then
                            Exit For
                        End If
                    Next
                    If bIncludeInShipping Then
                        dt = oShipmentTypes.LoadShipmentType(CInt(oOrder.GetValue("ShipToCountryCodeID")))
                    End If
                End If


                If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                    tdShipment.Visible = False
                    Exit Sub
                End If

                dt.Columns.Add("DisplayField")
                For Each dr As DataRow In dt.Rows
                    dr("DisplayField") = Convert.ToString(dr("Name")).Replace("&reg;", "®")
                Next

                ddlShipmentType.DataTextField = "DisplayField"
                ddlShipmentType.DataValueField = "ID"


                ddlShipmentType.DataSource = dt
                ddlShipmentType.DataBind()
                If ddlShipmentType.Items.Count > 0 Then
                    If Not oOrder Is Nothing Then
                        ddlShipmentType.SelectedValue = CStr(oOrder.ShipTypeID)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Rashmi P, Issue 5133, 12/6/12 Add ShipmentType Selection.
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub ddlShipmentType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlShipmentType.SelectedIndexChanged
            'This is how you set the Shipping Charge to show on Order
            If Not ddlShipmentType.SelectedItem Is Nothing AndAlso ddlShipmentType.SelectedItem.Text <> String.Empty Then
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                oOrder = CartGrid.Cart.GetOrderObject(Session, Page.User, Page.Application)
                oOrder.SetValue("ShipTypeID", ddlShipmentType.SelectedValue)

                oOrder.CalculateOrderTotals(True, True)
                CartGrid.Cart.SaveCart(Session)
                RefreshGrid()
            End If
        End Sub

        Private Sub AddGtmCheckout()
            Dim checkout As Checkout, checkoutDto As CheckoutDto

            checkoutDto = GetCheckoutDto()

            checkout = New Checkout(Me.Page, checkoutDto)
            checkout.Render()
        End Sub

        Private Function GetCheckoutDto() As CheckoutDto
            Dim checkoutDto As CheckoutDto,
                currencyType As String = "EUR"

            If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                currencyType = "EUR"
            ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                currencyType = "GBP"
            End If

            checkoutDto = New CheckoutDto() With {
                .StepNumber = 1,
                .CallbackUrl = Request.RawUrl,
                .Currency = currencyType
                }

            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
            oOrder = CartGrid.Cart.GetOrderObject(Session, Page.User, Page.Application)



            Dim position As Int32 = 0
            For Each orderLine As Aptify.Consulting.Entity.OrderLines.OrderLinesEntity__c In oOrder.SubTypes("OrderLines")
                checkoutDto.Products.Add(New ProductDto With {
                                              .Id = orderLine.ProductID.ToString,
                                              .Name = orderLine.Product,
                                              .Price = orderLine.Price,
                                              .Brand = "Chartered Accountants",
                                              .Category = "",
                                              .List = orderLine.ProductType,
                                              .Position = position,
                                              .Quantity = orderLine.Quantity,
                                              .Variant = ""
                                              })
                position += 1
            Next

            Return checkoutDto
        End Function


    End Class
End Namespace
