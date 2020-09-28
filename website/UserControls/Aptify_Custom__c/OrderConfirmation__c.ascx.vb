'Aptify e-Business 5.5.1, July 2013
Option Explicit On
Option Strict On


Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.Security
Imports Aptify.Framework.BusinessLogic.ProcessPipeline
Imports System.Data
Imports System.Collections
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports SoftwareDesign.GTM
Imports SoftwareDesign.GTM.Model

Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class OrderConfirmationControl__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_COMPANY_LOGO_IMAGE_URL As String = "CompanyLogoImage"
        Protected Const ATTRIBUTE_COMPANY_ADDRESS As String = "CompanyAddress"
        Protected Const ATTRIBUTE_PRODUCT_PAGE As String = "ProductURL"
        Protected Const ATTRIBUTE_CLASS_VIEW_PAGE As String = "ClassViewURL"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "OrderConfirmation"
        Protected Const ATTRIBUTE_MESSAGESYSTEM As String = "MessageSystem"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        'Issue 15215 Sachin 02/27/2013
        Protected Const ATTRIBUTE_SECURITYERROR_PAGE As String = "securityErrorPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage"
        Private m_lCustomerID As Long 'used to query Customer's Email
        Dim sCurrencyFormat As String
        Dim CurrencyCache As CurrencyTypeCache
        Dim arPrice As ArrayList
        Dim arExtended As ArrayList
        Dim orderHeader As Data.DataTable = Nothing
        Dim orderDetail As Data.DataTable = Nothing

#Region "OrderConfirmation Specific Properties"
        Public Overridable ReadOnly Property MessageSystem() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_MESSAGESYSTEM) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_MESSAGESYSTEM))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_MESSAGESYSTEM)
                    If Not String.IsNullOrEmpty(value) Then
                        ViewState.Item(ATTRIBUTE_MESSAGESYSTEM) = value
                        Return value
                    Else
                        Return ""
                    End If
                End If
            End Get
        End Property

        Public Overridable Property RedirectURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' CompanyLogoImage url
        ''' </summary>
        Public Overridable Property CompanyLogoImage() As String
            Get
                If Not ViewState(ATTRIBUTE_COMPANY_LOGO_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_COMPANY_LOGO_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_COMPANY_LOGO_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' RashmiP, Issue 9974, 09/14/10
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Property CompanyAddress() As String
            Get
                If Not ViewState(ATTRIBUTE_COMPANY_ADDRESS) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_COMPANY_ADDRESS))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_COMPANY_ADDRESS) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' Product page url
        ''' </summary>
        Public Overridable Property ProductURL() As String
            Get
                If Not ViewState(ATTRIBUTE_PRODUCT_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PRODUCT_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PRODUCT_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ClassView page url
        ''' </summary>
        Public Overridable Property ClassViewURL() As String
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
        ' Issue 15215 02/27/2013 by Sachin
        ''' <summary>
        ''' Security Error Page page url
        ''' </summary>
        Public Overridable ReadOnly Property SecurityErrorPage() As String
            Get
                If Not ViewState(ATTRIBUTE_SECURITYERROR_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_SECURITYERROR_PAGE))
                Else
                    Dim value As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("SecurityErrorPageURL"))
                    If Not String.IsNullOrEmpty(value) Then
                        ViewState(ATTRIBUTE_SECURITYERROR_PAGE) = value
                        Return value
                    Else
                        Return String.Empty
                    End If
                End If
            End Get
        End Property

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
            'set control properties from XML file if needed
            SetProperties()
            'Put user code to initialize the page here
            ''Added By Pradip 2016-06-30 For Redmine Issue https://redmine.softwaredesign.ie/issues/14557
            regexEmailValid.ValidationExpression = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.EmailRegularExpressionValidator__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            If Not IsPostBack Then
                LoadOrder()
            End If
        End Sub

        Protected Overrides Sub SetProperties()
            Try
                If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
                'call base method to set parent properties
                MyBase.SetProperties()

                If String.IsNullOrEmpty(ProductURL) Or String.IsNullOrEmpty(ClassViewURL) Then
                    ProductURL = Me.GetLinkValueFromXML(ATTRIBUTE_PRODUCT_PAGE)
                    ClassViewURL = Me.GetLinkValueFromXML(ATTRIBUTE_CLASS_VIEW_PAGE)
                    If String.IsNullOrEmpty(ProductURL) Or String.IsNullOrEmpty(ClassViewURL) Then
                        Me.grdMain.Enabled = False
                        Me.grdMain.ToolTip = "ProductURL and/or ClassViewURL property has not been set."
                    End If
                End If

                If String.IsNullOrEmpty(CompanyLogoImage) Then
                    CompanyLogoImage = Me.GetLinkValueFromXML(ATTRIBUTE_COMPANY_LOGO_IMAGE_URL)
                    Me.companyLogo.Src = CompanyLogoImage
                End If
                'RashmiP Issue 9974, 09/14/10 
                'Company address abstracted, moved in Navigation File.
                If String.IsNullOrEmpty(CompanyAddress) Then
                    Dim strVirtualPath As String = Request.ApplicationPath.ToString & "/"
                    CompanyAddress = Me.GetLinkValueFromXML(ATTRIBUTE_COMPANY_ADDRESS)
                    Me.lblcompanyAddress.Text = CompanyAddress.Substring(strVirtualPath.Length, CompanyAddress.Length - strVirtualPath.Length)
                End If
                If String.IsNullOrEmpty(RedirectURL) Then
                    Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
                End If

                If String.IsNullOrEmpty(ReportPage) Then
                    ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
                End If

            Catch ex As Exception
            End Try
        End Sub
        Private Sub LoadOrder()
            If Not IsNumeric(Request.QueryString("ID")) Then
                If Request.QueryString("ID") IsNot Nothing Then
                    Throw New ArgumentException("Parameter must be numeric.", "ID")
                End If
            Else
                'Issue 15215 02/27/2013 by the Sachin
                If DoesUserHaveAccessOnOrder(User1.PersonID, Convert.ToInt32(Request.QueryString("ID"))) Then
                    LoadOrderHeader()
                    LoadOrderDetails()
                    'Commented by dipali 10/04/2017
                    'LoadEmailOrder()
                    ' Added by Govind M for Redmine #17252
                    AddGTMPurchase()
                    If Convert.ToInt32(Request.QueryString("IsBilling")) = 1 Then
                        idConfirmEmail.Visible = True
                    Else
                        idConfirmEmail.Visible = False
                    End If
                  
                Else
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("virtualdir") & SecurityErrorPage & "?Message=Access to this Order is unauthorized.")
                End If
            End If
        End Sub
        Private Sub LoadOrderHeader()
            Dim sSQL As String, dt As Data.DataTable
              Dim bal As Decimal
            Try
                If Not IsNumeric(Request.QueryString("ID")) Then
                    Throw New ArgumentException("Parameter must be numeric.", "ID")
                End If



                'Commented by dipali 10/04/2017
                'sSQL = "SELECT o.*,ost.Name OrderStatus,p.OldID FROM " & _
                '       Database & _
                '       "..vwOrders o INNER JOIN " & _
                '       Database & _
                '       "..vwOrderStatusTypes ost ON " & _
                '       "o.OrderStatusID=ost.ID  " & _
                '       " inner join ..vwpersons p on o.billtoid=p.ID WHERE o.ID = " & Request.QueryString("ID")
                'dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

                'Added by dipali 10/04/2017
                sSQL = Me.Database & "..spGetOrderMasterDetailsOnOrderConfirmation__c"
                Dim param(0) As IDataParameter

                param(0) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, Convert.ToInt32(Request.QueryString("ID")))
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                '--End Dipali
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    orderHeader = dt

                    With dt.Rows(0)

                        If .Item("ID") IsNot Nothing AndAlso Not IsDBNull(.Item("ID")) Then
                            lblOrderID.Text = CStr(.Item("ID"))
                        Else
                            lblOrderID.Text = "-1"
                        End If
                        If .Item("OrderType") IsNot Nothing AndAlso Not IsDBNull(.Item("OrderType")) Then
                            lblOrderType.Text = CStr(.Item("OrderType"))
                        Else
                            lblOrderType.Text = ""
                        End If
                        If .Item("OrderStatus") IsNot Nothing AndAlso Not IsDBNull(.Item("OrderStatus")) Then
                            lblOrderStatus.Text = CStr(.Item("OrderStatus"))
                        Else
                            lblOrderStatus.Text = ""
                        End If
                        If .Item("PayType") IsNot Nothing AndAlso Not IsDBNull(.Item("PayType")) Then
                            lblPayType.Text = CStr(.Item("PayType"))
                        Else
                            lblPayType.Text = ""
                        End If
                        If .Item("OldID") IsNot Nothing AndAlso Not IsDBNull(.Item("OldID")) Then
                            lblBillToID.Text = CStr(.Item("OldID"))
                        Else
                            lblBillToID.Text = "-1"
                        End If
                        If .Item("ShipType") IsNot Nothing AndAlso Not IsDBNull(.Item("ShipType")) Then
                            lblShipType.Text = CStr(.Item("ShipType"))
                        Else
                            lblShipType.Text = ""
                        End If
                        If .Item("ShipTrackingNum") IsNot Nothing AndAlso Not IsDBNull(.Item("ShipTrackingNum")) Then
                            lblShipTrackingNum.Text = CStr(.Item("ShipTrackingNum"))
                        Else
                            lblShipTrackingNum.Text = ""
                        End If
                        If .Item("ShipDate") Is Nothing _
                                OrElse IsDBNull(.Item("ShipDate")) _
                                OrElse Not IsDate(.Item("ShipDate")) _
                                OrElse CDate(.Item("ShipDate")) = DateSerial(1900, 1, 1) Then
                            'The ShipDate is not valid
                            lblShipDate.Text = "Not Shipped"
                        Else
                            lblShipDate.Text = CStr(.Item("ShipDate"))
                        End If

                        CurrencyCache = CurrencyTypeCache.Instance(Me.AptifyApplication)

                        If IsNumeric(.Item("CurrencyTypeID")) Then
                            sCurrencyFormat = CurrencyCache.CurrencyType(CInt(.Item("CurrencyTypeID"))).FormatString
                        Else
                            'If not provided in order, user logged in User's Preferred CurrencyTypeID
                            sCurrencyFormat = CurrencyCache.CurrencyType(CInt(Me.User1.PreferredCurrencyTypeID)).FormatString
                        End If

                        If sCurrencyFormat.Length = 0 Then
                            sCurrencyFormat = "Currency"
                        End If

                        lblSubTotal.Text = Format$(.Item("CALC_SubTotal"), sCurrencyFormat)

                        Dim dShippingAndHandlingCharges As Decimal = 0
                        If IsNumeric(.Item("ShipCharge")) Then
                            dShippingAndHandlingCharges = CDec(.Item("ShipCharge"))
                        End If

                        'If IsNumeric(.Item("ShipCharge")) Then
                        If IsNumeric(.Item("CALC_HandlingCharge")) Then
                            dShippingAndHandlingCharges += CDec(.Item("CALC_HandlingCharge"))
                        End If

                        lblShipCharge.Text = Format$(dShippingAndHandlingCharges, sCurrencyFormat)

                        lblSalesTax.Text = Format$(.Item("CALC_SalesTax"), sCurrencyFormat)
                        lblGrandTotal.Text = Format$(.Item("CALC_GrandTotal"), sCurrencyFormat)
                        lblBalance.Text = Format$(.Item("Balance"), sCurrencyFormat)
                        lblPayments.Text = Format$(.Item("CALC_PaymentTotal"), sCurrencyFormat)
                        Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                        ' Redmine #16424
                        oOrder = CType(AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(.Item("ID"))), Applications.OrderEntry.OrdersEntity)
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
                            lblSalesTax.Text = Format$((CDec(.Item("CALC_SalesTax")) - dSalesTaxAmt), sCurrencyFormat)
                            lblShipCharge.Text = Format$((dShippingAndHandlingCharges + dSalesTaxAmt), sCurrencyFormat)
                        End If
                        ' Below coded added for web shopping cart changes
                        ''Dim sSQLTax As String = Database & "..spCheckCompanyExemptTax__c @CompanyID=" & User1.CompanyID
                        ''Dim bCompanyExempt As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSQLTax, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        ''If bCompanyExempt Then
                        ''lblShipCharge.Text = Format$((dShippingAndHandlingCharges + Convert.ToDecimal(.Item("CALC_SalesTax"))), sCurrencyFormat)
                        ''lblSalesTax.Text = Format$(0, sCurrencyFormat)
                        ''End If
                        'Code Added Start by Govind 1/2/2014
                        If Not Session("IsMembApp") Is Nothing AndAlso Convert.ToInt32(Session("IsMembApp")) > 0 Then
                            ' checking this page called by membership application and set the order id same membership application
                            Dim oGE As AptifyGenericEntityBase
                            oGE = AptifyApplication.GetEntityObject("MembershipApplication__c", Convert.ToInt32(Session("IsMembApp")))
                            oGE.SetValue("OrderID", Convert.ToInt32(.Item("ID")))
                            oGE.SetValue("MembershipApplicationStatusID", Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("MembershipApplicationStatus__c", "Submitted")))
                            If oGE.Save Then
                                Session("IsMembApp") = Nothing
                            End If
                        End If
                        If Convert.ToDecimal(.Item("Balance")) > 0 Then
                            bal = Convert.ToDecimal(.Item("Balance"))
                        End If
                        'Code Added END by Govind 1/2/2014
                    End With
                    SetAddress(blkShipTo, "ShipTo", dt.Rows(0))
                    SetAddress(blkBillTo, "BillTo", dt.Rows(0))

                    'Set m_CustomerID to be used later
                    If IsNumeric(dt.Rows(0).Item("ShipToID")) Then
                        m_lCustomerID = CLng(dt.Rows(0).Item("ShipToID"))
                        Me.EmailOrderTextBox.Text = Convert.ToString(dt.Rows(0).Item("Email")).Trim
                        'Reset Email message to nothing
                        SendEmailLabel.Text = ""
                    Else
                        m_lCustomerID = -1
                    End If
                    ''Added By Pradip  2016-07-06 For Red-Mine Issue https://redmine.softwaredesign.ie/issues/13287
                    If lblOrderStatus.Text.Trim.ToUpper = "SHIPPED" Then
                        btnReceipt.Visible = True
                    Else
                        btnReceipt.Visible = False
                    End If
                    If bal > 0 Then
                        btnReceipt.Visible = False
                    End If
                Else
                    'Order wasn't found
                End If

            Catch ae As ArgumentException
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ae)
            End Try
        End Sub
        Private Sub SetAddress(ByVal oBlock As NameAddressBlock, _
                               ByVal sPrefix As String, _
                               ByRef dr As System.Data.DataRow)

            With oBlock
                .Name = CStr(dr.Item(sPrefix & "Name"))
                If Not dr.IsNull(sPrefix & "Company") Then
                    .Name = .Name & "/" & CStr(dr.Item(sPrefix & "Company"))
                End If

                'Issue 5053 - Only display customer's address if it is valid/complete
                Dim sLine1 As String = ""
                Dim sLine2 As String = ""
                Dim sLine3 As String = ""
                Dim sCity As String = ""
                Dim sState As String = ""
                Dim sZip As String = ""
                Dim sCountry As String = ""

                'Check if AddressLine1 exists
                If Not dr.IsNull(sPrefix & "AddrLine1") Then
                    sLine1 = CStr(dr.Item(sPrefix & "AddrLine1"))
                End If
                'Check if AddressLine2 exists
                If Not dr.IsNull(sPrefix & "AddrLine2") Then
                    sLine2 = CStr(dr.Item(sPrefix & "AddrLine2"))
                End If
                'Check if AddressLine3 exists
                If Not dr.IsNull(sPrefix & "AddrLine3") Then
                    sLine3 = CStr(dr.Item(sPrefix & "AddrLine3"))
                End If
                'Check if City exists
                If Not dr.IsNull(sPrefix & "City") Then
                    sCity = CStr(dr.Item(sPrefix & "City"))
                End If
                'Check if State exists
                If Not dr.IsNull(sPrefix & "State") Then
                    sState = CStr(dr.Item(sPrefix & "State"))
                End If
                'Check if ZipCode exists
                If Not dr.IsNull(sPrefix & "ZipCode") Then
                    sZip = CStr(dr.Item(sPrefix & "ZipCode"))
                End If
                'Check if Country exists
                If Not dr.IsNull(sPrefix & "Country") Then
                    sCountry = CStr(dr.Item(sPrefix & "Country"))
                End If

                'Only populate AddressBlock of the address is valid/complete
                '2/5/08 RJK - State is no longer required for the valid address test.
                'Several international addresses do not have States.  The new rule
                'is that at least one of the City, State or PostalCode (ZipCode) is provided.
                If sLine1.Length > 0 _
                        AndAlso (sCity.Length > 0 _
                        OrElse sState.Length > 0 _
                        OrElse sZip.Length > 0) Then
                    .AddressLine1 = sLine1
                    .AddressLine2 = sLine2
                    .AddressLine3 = sLine3
                    .City = sCity
                    .State = sState
                    .ZipCode = sZip
                    .Country = sCountry
                Else
                    .AddressLine1 = ""
                    .AddressLine2 = "Address Not Provided"
                    .AddressLine3 = ""
                    .City = ""
                    .State = ""
                    .ZipCode = ""
                    .Country = ""
                End If
            End With

        End Sub
        Private Sub LoadOrderDetails()
            Dim sSQL As String, dt As Data.DataTable
            'Anil B for issue 15341 on 20-03-2013
            'Set Product name as Web Name
            Dim sOrderDetailView As String = AptifyApplication.GetEntityBaseView("OrderLines")
            Dim sProductView As String = AptifyApplication.GetEntityBaseView("Products")
            Try
                ''Commented By Pradip Chavhan 2015-12-15 To Resolve Time Out Issue
                'sSQL = "SELECT *, case when (P.WebName is null or P.WebName = '') 	then P.Name 	Else P.WebName End as WebName FROM " & _
                '       AptifyApplication.GetEntityBaseDatabase("OrderLines") & ".." & sOrderDetailView & " OD inner join " & AptifyApplication.GetEntityBaseDatabase("Products") & ".." & sProductView & " P on OD.ProductID=P.id " & _
                '       "WHERE OrderID=" & _
                '        Request.QueryString("ID") & " ORDER BY Sequence"

                sSQL = Me.Database & "..spGetDetailsForOrderConfirmation__c"
                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, Convert.ToInt32(Request.QueryString("ID")))
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                '11/23/07 Tamasa, Added the two column values with the correct currency format to the array list from datatable. Issue 5371.
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    orderDetail = dt
                    arPrice = New ArrayList
                    arExtended = New ArrayList
                    For Each row As DataRow In dt.Rows
                        arPrice.Add(Format$(row.Item("Price"), sCurrencyFormat))
                        arExtended.Add(Format$(row.Item("Extended"), sCurrencyFormat))
                    Next
                End If
                'End
                grdMain.DataSource = dt
                grdMain.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadEmailOrder()
            Try
                'Get ShipTo Person's Email address to pre-populate the Email textbox
                Dim sSQL As String = "SELECT Email FROM " & Database & "..vwPersons WHERE ID=" & m_lCustomerID.ToString
                Dim sEmail As String
                sEmail = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache).ToString
                ''Change By Pradip 2016-09-30 For https://redmine.softwaredesign.ie/issues/13426
                ' Me.EmailOrderTextBox.Text = sEmail
                Me.EmailOrderTextBox.Text = sEmail.Trim
                'Reset Email message to nothing
                SendEmailLabel.Text = ""
            Catch ex As ArgumentException
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub EmailOrderButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmailOrderButton.Click
            Try
                'Suraj Issue 15210 , 3/15/13, check multiple emails are valid or not if valid then send a mail
                If CheckMulipleEmailISValid() Then
                    'Get the Process Flow ID to be used for sending the Order Confirmation Email
                    Dim sSQL As String = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='Send eBusiness Order Confirmation Email'"
                    Dim lProcessFlowID As Long = CLng(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache))

                    Dim context As New AptifyContext
                    context.Properties.AddProperty("OrderID", CLng(Request.QueryString("ID")))
                    context.Properties.AddProperty("EmailAddresses", Me.EmailOrderTextBox.Text)
                    'Added by Dipali Issue No:13305
                    context.Properties.AddProperty("MessageSystem", MessageSystem)
                    Dim result As ProcessFlowResult
                    result = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
                    If result.IsSuccess Then
                        SendEmailLabel.ForeColor = Drawing.Color.Blue
                        SendEmailLabel.Text = "Order Confirmation has been sent."
                    Else
                        SendEmailLabel.ForeColor = Drawing.Color.Red
                        SendEmailLabel.Text = "Email failed. Contact Customer Service for help."
                    End If
                End If

            Catch ex As ArgumentException
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Navin Prasad Issue 11032
        '11/23/07 Tamasa, Added to bound the Item with correct currency format.Issue 5371
        Dim Count As Integer = 0

        'HP Issue#8621:  for products which are of type 'Class', extract ClassID from the class registration entity and set link url accordingly 
        Private Function SetURLPerProductType(ByVal productId As Integer, ByVal extId As Integer) As String
            Dim url As String = String.Empty
            Dim classId As Integer
            Dim sql As String
            Dim p As Aptify.Applications.ProductSetup.ProductObject
            p = CType(Me.AptifyApplication.GetEntityObject("Products", productId), Aptify.Applications.ProductSetup.ProductObject)

            If Not IsNothing(p.GetValue("ProductType")) AndAlso p.GetValue("ProductType").ToString.ToUpper = "CLASS" Then
                ''Commented and added By Pradip To Get Class ID 2017-04-19
                'sql = "SELECT TOP 1 ClassID FROM " & AptifyApplication.GetEntityBaseDatabase("ClassRegistrations") & _
                '          "..ClassRegistration WHERE ID=" & extId
                'classId = CInt(DataAction.ExecuteScalar(sql))

                sql = String.Empty
                sql = Database & "..spGetProductRelatedClass__c @ProductID=" & Convert.ToInt32(productId)
                classId = Convert.ToInt32(DataAction.ExecuteScalar(sql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If classId > 0 Then
                    url = ClassViewURL & "?ClassID=" & classId
                End If
            Else
                url = ProductURL & "?ID=" & productId
            End If

            Return url
        End Function

        Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As GridPageChangedEventArgs) Handles grdMain.PageIndexChanged
            LoadOrder()
        End Sub
        'Navin Prasad Issue 11032
        Protected Sub grdMain_RowDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles grdMain.ItemDataBound
            If (TypeOf (e.Item) Is GridDataItem) Then
                Dim labelPrice As Label
                Dim labelExtended As Label

                labelPrice = New Label
                labelExtended = New Label

                labelPrice = DirectCast(e.Item.FindControl("lblPrice"), Label)
                labelExtended = DirectCast(e.Item.FindControl("lblExtended"), Label)

                labelPrice.Text = CStr(arPrice.Item(Count))
                labelExtended.Text = CStr(arExtended.Item(Count))

                ' examine each orderline in order to properly set product links
                DirectCast(e.Item.FindControl("link"), HyperLink).NavigateUrl = _
                SetURLPerProductType(CInt(DataBinder.Eval(e.Item.DataItem, "ProductID")), CInt(DataBinder.Eval(e.Item.DataItem, "ExtendedAttributeID")))
                Count += 1
            End If
        End Sub
        ''' <summary>
        ''' Check web user IsValidUser if yes then allow his to see orderconfirmation details else redirect his login page 
        ''' </summary>
        ''' <param name="UserId">Is the Id of current login user </param>
        ''' <param name="OrderID">Is the id of order which user want to see</param>
        ''' <remarks></remarks>
        Private Function DoesUserHaveAccessOnOrder(ByVal UserId As Long, ByVal OrderID As Long) As Boolean
            Try
                Dim sSQL As String
                'This code for issue 13280 sachin 
                'Get value form persons view in 0 position datable
                If IsNothing(Request.QueryString("ISFirm")) Then


                    sSQL = Me.Database & "..spCheckValidOrderForPerson__c"
                    Dim param(1) As IDataParameter

                    param(0) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, Convert.ToInt32(Request.QueryString("ID")))
                    param(1) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, Convert.ToInt32(UserId))
                    ' dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)


                    'sSQL = "SELECT COUNT(O.ID)  from " & _
                    '       Database & ".." & AptifyApplication.GetEntityBaseView("Persons") & " P " & _
                    '            ", " & Database & ".." & AptifyApplication.GetEntityBaseView("Orders") & " O " & _
                    '          " Where ((P.IsGroupAdmin = 1 AND O.ShipToCompanyID = P.CompanyID AND P.ID = " & UserId & ") " & _
                    '              " OR ( O.ShipToID = " & UserId & "))" & _
                    '                 " AND O.ID =" & OrderID
                    Return CBool(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, param))
                End If
                Return True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
        'Suraj Issue 15210 , 3/15/13, check multiple emails are valid or not 
        Private Function CheckMulipleEmailISValid() As Boolean
            Try
                Dim emailAddress As String() = EmailOrderTextBox.Text.Split(New Char() {","c})
                Dim bIsValidEmail As Boolean = False
                Dim email As String
                For Each email In emailAddress
                    'Suraj S Issue 15210 ,3/15/13 use Comman Function for email validation
                    bIsValidEmail = CommonMethods.EmailAddressCheck(email)
                    If (bIsValidEmail) = False Then
                        SendEmailLabel.Text = "Invalid Email Format"
                        SendEmailLabel.ForeColor = Drawing.Color.Red
                        Return False
                        Exit For
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return True
        End Function

        'Protected Sub cmdRequestRefund_Click(sender As Object, e As EventArgs) Handles cmdRequestRefund.Click
        '    If Not IsNumeric(Request.QueryString("ID")) Then
        '        If Request.QueryString("ID") IsNot Nothing Then
        '            Throw New ArgumentException("Parameter must be numeric.", "ID")
        '        End If
        '    Else
        '        'ID = Request.QueryString("ID")
        '        'Response.Write(Me.RedirectURL + " " + Request.QueryString("ID"))
        '        Response.Redirect(Me.RedirectURL & "?ID=" + Request.QueryString("ID"))

        '    End If
        'End Sub

          Protected Sub btnReceipt_Click(sender As Object, e As EventArgs) Handles btnReceipt.Click

            Try
                Dim dt As DataTable
                Dim sSql As String
                sSql = Database & "..spGetProductCategoryFromOrder__c"
                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, Request.QueryString("ID"))
                dt = Me.DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ''Commented By Pradip For red mine https://redmine.softwaredesign.ie/issues/16471 report not getting printg for non ETD product
                    'If CInt(dt.Rows(0)(0)) = 0 Or CInt(dt.Rows(0)(0)) = 1 Then
                    If CInt(dt.Rows(0)(0)) = 1 Then
                        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "ETD Payment Receipt Report"))
                        Dim rptParam As New AptifyCrystalReport__c
                        rptParam.ReportID = ReportID
                        rptParam.Param1 = Convert.ToString(Request.QueryString("ID"))
                        rptParam.Param2 = "O"
                        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                    Else
                        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Payment Receipt Report"))
                        Dim rptParam As New AptifyCrystalReport__c
                        rptParam.ReportID = ReportID
                        rptParam.Param1 = Convert.ToString(Request.QueryString("ID"))
                        rptParam.Param2 = "O"
                        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                    End If
                    ''Commented By Pradip For red mine https://redmine.softwaredesign.ie/issues/16471 report not getting printg for non ETD product
                    'Else
                    '    Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Payment Receipt Report"))
                    '    Dim rptParam As New AptifyCrystalReport__c
                    '    rptParam.ReportID = ReportID
                    '    rptParam.Param1 = Convert.ToString(Request.QueryString("ID"))
                    '    rptParam.Param2 = "O"
                    '    Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                    '    ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
            Try
                Dim OrderID As String = Convert.ToString(Request.QueryString("ID"))
                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "InvoiceStandardWEB__cai"))
                Dim rptParam As New AptifyCrystalReport__c
                'Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
                rptParam.ReportID = ReportID
                rptParam.Param1 = OrderID
                rptParam.SubParam1 = OrderID
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub AddGTMPurchase()
            Dim gtmPurchase As GtmPurchase, purchaseDto As PurchaseDto

            If (orderDetail Is Nothing Or orderHeader Is Nothing) Then
                Return
            End If

            Dim headerRow As Data.DataRow = orderHeader.Rows(0)

            Dim dShippingAndHandlingCharges As Decimal = 0
            If IsNumeric(headerRow("ShipCharge")) Then
                dShippingAndHandlingCharges = CDec(headerRow("ShipCharge"))
            End If

            'If IsNumeric(.Item("ShipCharge")) Then
            If IsNumeric(headerRow("CALC_HandlingCharge")) Then
                dShippingAndHandlingCharges += CDec(headerRow("CALC_HandlingCharge"))
            End If

            Dim currencyType As String = "EUR"
            If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                currencyType = "EUR"
            ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                currencyType = "GBP"
            End If



            purchaseDto = New PurchaseDto() With {
                .TransactionId = headerRow("ID").ToString,
                .Affiliattion = "",
                .Revenue = CType(headerRow("CALC_GrandTotal").ToString, Decimal),
                .Tax = CType(headerRow("CALC_SalesTax").ToString, Decimal),
                .Shipping = dShippingAndHandlingCharges,
                .Coupon = "",
                .Currency = currencyType
                }

            Dim position As Integer = 0
            For Each detailRow As Data.DataRow In orderDetail.Rows

                purchaseDto.Products.Add(New ProductDto With {
                                            .Id = detailRow("ProductID").ToString,
                                            .Brand = "Chartered Accountants",
                                            .Category = "Product",
                                            .List = "",
                                            .Name = detailRow("WebName").ToString,
                                            .position = position,
                                            .Price = CType(detailRow("Price").ToString, Decimal) / CType(detailRow("Quantity").ToString, Integer),
                                            .Quantity = CType(detailRow("Quantity").ToString, Decimal),
                                            .Variant = ""
                                            })
                position += 1
            Next



            gtmPurchase = New GtmPurchase(Me.Page, purchaseDto)
            gtmPurchase.Render()

        End Sub
    End Class
End Namespace
