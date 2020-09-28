'Aptify e-Business 5.5.1, July 2013
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer              Date Created/Modified               Summary
'Rajesh Kardile           04/09/2014                checking if Product Category is Training Ticket or not
'Govind Mande             05/22/2015                Payment as per Order Line
'Govind  Mande            06/25/2015                Added code for staged payment 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports System.Data
Imports Aptify.Applications.OrderEntry.Payments
Imports Aptify.Applications.Accounting
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic.ProcessPipeline
Imports Aptify.Framework.DataServices
Imports Telerik.Web.UI
Imports Aptify.Applications.OrderEntry
Imports SoftwareDesign.GTM
Imports SoftwareDesign.GTM.Model


Namespace Aptify.Framework.Web.eBusiness.ProductCatalog
    Partial Class BillingControl__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_BILLING_CHANGE_IMAGE_URL As String = "BillingChangeImage"
        Protected Const ATTRIBUTE_BILLING_CHANGE_PAGE As String = "BillingChangePage"
        Protected Const ATTRIBUTE_CONFIRMATION_PAGE As String = "OrderConfirmationPage"
        Protected Const ATTRIBUTE_BACK_BUTTON_PAGE As String = "BackButtonPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "BillingControl__c"
        Protected Const ATTRIBUTE_BILL_ME_LATER As String = "BillMeLaterDisplayText"
        Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage"


#Region "BillingControl Specific Properties"
        ''' <summary>
        ''' BillingChangeImage url
        ''' </summary>
        Public Overridable Property BillingChangeImage() As String
            Get
                If Not ViewState(ATTRIBUTE_BILLING_CHANGE_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_BILLING_CHANGE_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_BILLING_CHANGE_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
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
        ''' <summary>
        ''' BackButton page url
        ''' </summary>
        Public Overridable Property BackButtonPage() As String
            Get
                If Not ViewState(ATTRIBUTE_BACK_BUTTON_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_BACK_BUTTON_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_BACK_BUTTON_PAGE) = Me.FixLinkForVirtualPath(value)
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
        ''RashmiP, issue 6781
        Public Overridable ReadOnly Property BillMeLaterDisplayText() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_BILL_ME_LATER) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_BILL_ME_LATER))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_BILL_ME_LATER)
                    If Not String.IsNullOrEmpty(value) Then
                        ViewState.Item(ATTRIBUTE_BILL_ME_LATER) = value
                    End If
                    Return value
                End If
            End Get
        End Property
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
        '<%--Nalini Issue#12578--%>
        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(BillingChangeImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                BillingChangeImage = Me.GetLinkValueFromXML(ATTRIBUTE_BILLING_CHANGE_IMAGE_URL)
            End If
            If String.IsNullOrEmpty(BillingChangePage) Then
                'since value is the 'default' check the XML file for possible custom setting
                BillingChangePage = Me.GetLinkValueFromXML(ATTRIBUTE_BILLING_CHANGE_PAGE)
                'Me.lnkChangeAddress.ImageUrl = BillingChangeImage
                If String.IsNullOrEmpty(BillingChangePage) Then
                    Me.lnkChangeAddress.Enabled = False
                    Me.lnkChangeAddress.ToolTip = "BillingChangePage property has not been set."
                Else
                    Me.lnkChangeAddress.PostBackUrl = BillingChangePage & "?Type=Billing&ReturnToPage=" & Request.Path
                End If
            Else
                Me.lnkChangeAddress.PostBackUrl = BillingChangePage & "?Type=Billing&ReturnToPage=" & Request.Path
            End If
            If String.IsNullOrEmpty(BackButtonPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                BackButtonPage = Me.GetLinkValueFromXML(ATTRIBUTE_BACK_BUTTON_PAGE)
                If String.IsNullOrEmpty(BackButtonPage) Then
                    Me.cmdBack.Enabled = False
                    Me.cmdBack.ToolTip = "BackButtonPage property has not been set."
                End If
            End If
            If String.IsNullOrEmpty(OrderConfirmationPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                OrderConfirmationPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONFIRMATION_PAGE)
                If String.IsNullOrEmpty(OrderConfirmationPage) Then
                    Me.cmdPlaceOrder.Enabled = False
                    Me.cmdPlaceOrder.ToolTip = "OrderConfirmationPage property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(LoginPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_LOGIN_PAGE)
            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


            'set control properties from XML file if needed
            Dim grdTempCart As RadGrid
            SetProperties()
            ''Added as part of #20026
            Dim _ScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            _ScriptManager.AsyncPostBackTimeout = "1000"
            ''end of #20026
            If Not IsPostBack Then
                If User1.PersonID > 0 Then
                    RefreshGrid()
                    LoadControls()
                    GetPrefferedCurrency()
                    'Added by sandeep for issue 15423 on 12/4/2013
                    grdTempCart = TryCast(CartGrid2.FindControl("grdMain"), RadGrid)
                    grdTempCart.MasterTableView.GetColumn("AutoRenew").Visible = False
                    '(Siddharth) Added to display payment message.
                    If Not CreditCard.CreditCheckLimit Then
                        lblPaymentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.CreditCheckRule.PaymentMessage")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                    'Code change end
                    ' code added by govind
                    ''Commented By Pradip 2017-04-10 not required here
                    'DisplayPaymentSchedule(0)
                    radPaymentPlanDetails.Visible = False
                    tblStageDetails.Visible = False
                    ViewState("SelectedPaymentPlan") = Nothing
                    AddGtmCheckout()
                Else
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage + "?ReturnURL=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Request.RawUrl)))
                End If
            Else
                lblError.Text = ""
            End If
        End Sub

        Private Sub CheckOrderDetails(ByVal oOrder As AptifyGenericEntityBase)
            Try
                If oOrder.SubTypes("OrderLines").Count > 0 Then
                    If Not String.IsNullOrEmpty(OrderConfirmationPage) Then
                        cmdPlaceOrder.Enabled = True
                        'HP Issue#8972: based on the grandTotal of all orderlines toggle validators requiring credit card information
                        SetCreditCardValidators(ShoppingCart1.GrandTotal)
                    End If
                    tblRowMain.Visible = True
                    lblNoItems.Visible = False
                    lblGotItems.Visible = True
                Else
                    cmdPlaceOrder.Enabled = False
                    tblRowMain.Visible = False
                    lblNoItems.Visible = True
                    lblGotItems.Visible = False
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadControls()
            Dim oOrder As OrdersEntity
            Dim arrlstSubscriberID As New StringBuilder
            Try
                Dim bIncludeInShipping As Boolean = False
                oOrder = ShoppingCart1.GetOrderObject(Session, Page.User, Application)
                ' oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
                'Added by Vaishali/Pardip - 4/10/2017
                Dim iCount As Integer
                Dim sProductIDs As New StringBuilder
                Dim oShipmentTypes As New Aptify.Framework.Web.eBusiness.CommonMethods(DataAction, AptifyApplication, User1, Database)
                Dim strSubscriberID As String = String.Empty
                Dim strRenewalStatus As String = String.Empty
                For iCount = 0 To oOrder.SubTypes("OrderLines").Count - 1
                    sProductIDs.Append(oOrder.SubTypes("OrderLines").Item(iCount).GetValue("ProductID"))
                    sProductIDs.Append(",")
                    bIncludeInShipping = oShipmentTypes.IncludeInShipping(CLng(oOrder.SubTypes("OrderLines").Item(iCount).GetValue("ProductID")))
                    If oOrder.SubTypes("OrderLines").Item(iCount).GetValue("SubscriberID") IsNot Nothing AndAlso oOrder.SubTypes("OrderLines").Item(iCount).GetValue("Comments") IsNot Nothing Then
                        strSubscriberID = oOrder.SubTypes("OrderLines").Item(iCount).GetValue("SubscriberID").ToString().Trim
                        strRenewalStatus = oOrder.SubTypes("OrderLines").Item(iCount).GetValue("Comments").ToString().Trim
                        'ToDo Check for Renewal Condition 
                        If IsNumeric(strSubscriberID) = True AndAlso CInt(strSubscriberID) > 0 AndAlso (strRenewalStatus.ToUpper() = "RENEWMEMBERSHIP" OrElse strRenewalStatus.ToUpper() = "RENEWSUBSCRIPTION") Then
                            ' arrlstRecipientID.Add(CInt(strSubscriberID))
                            ' sendRenewalMail = True
                            arrlstSubscriberID.Append(strSubscriberID.ToString)
                            arrlstSubscriberID.Append(",")
                        End If
                    End If

                Next
                If arrlstSubscriberID.Length > 0 Then
                    arrlstSubscriberID.Remove(arrlstSubscriberID.Length - 1, 1)
                    hidSubscriberID.Value = arrlstSubscriberID.ToString
                End If

                sProductIDs.Remove(sProductIDs.Length - 1, 1)
                hfProductIDs.Value = sProductIDs.ToString()
                '------------------------------
                CheckOrderDetails(oOrder)
                LoadCreditCardInfo(oOrder, hfProductIDs.Value)
                LoadBillAddress(oOrder)
                LoadShipmentType(oOrder, bIncludeInShipping)
                'Commented by Vaishali - 4/10/2017
                'Added code for stage payment by govind 25/06/2015
                'CheckStagePayment(oOrder)
                LoadPaymentPlan(hfProductIDs.Value)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ' Code added by govind for stage payment
        Private Sub CheckStagePayment(ByVal oOrder As AptifyGenericEntityBase)
            Try
                If oOrder.SubTypes("OrderLines").Count > 0 Then
                    Dim sProductID As String = String.Empty
                    For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                        Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(oOrder.SubTypes("OrderLines").Item(i).GetValue("ProductID"))
                        Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If iProductPaymentPlan > 0 Then
                            If sProductID = "" Then
                                sProductID = Convert.ToString(oOrder.SubTypes("OrderLines").Item(i).GetValue("ProductID"))
                            Else
                                sProductID = sProductID + "," + Convert.ToString(oOrder.SubTypes("OrderLines").Item(i).GetValue("ProductID"))
                            End If
                        End If
                    Next
                    If sProductID <> "" Then
                        LoadPaymentPlan(sProductID)
                        ddlPaymentPlan.Visible = True
                        lblPaymentPlan.Visible = True
                    Else
                        ddlPaymentPlan.Visible = False
                        lblPaymentPlan.Visible = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' For Load Stage Payment drop down
        ''' </summary>
        ''' <param name="sProduct"></param>
        ''' <remarks></remarks>
        Private Sub LoadPaymentPlan(ByVal sProduct As String)
            Try
                ' Dim sSql As String = Database & "..spGetPaymentPlanForStudentEnrollment__c @ProductIds='" & sProduct & "'"
                Dim sSql As String = Database & "..spGetPaymentPlansByProducts__c @ProductIds='" & sProduct & "'"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ddlPaymentPlan.DataSource = dt
                    ddlPaymentPlan.DataTextField = "Name"
                    ddlPaymentPlan.DataValueField = "ID"
                    ddlPaymentPlan.DataBind()
                    radPaymentPlanDetails.Visible = True
                    ddlPaymentPlan.Visible = True
                    lblPaymentPlan.Visible = True
                Else
                    radPaymentPlanDetails.Visible = False
                    ddlPaymentPlan.Visible = False
                    lblPaymentPlan.Visible = False
                End If
                ddlPaymentPlan.Items.Insert(0, "Select Payment Plan")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub ddlPaymentPlan_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPaymentPlan.SelectedIndexChanged
            Try
                Dim iPaymentPlan As Integer = 0
                If ddlPaymentPlan.SelectedValue <> "Select Payment Plan" Then
                    iPaymentPlan = Convert.ToInt32(ddlPaymentPlan.SelectedValue)
                End If
                DisplayPaymentSchedule(iPaymentPlan)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub DisplayPaymentSchedule(ByVal PaymentPlanID As Integer)
            Dim sSql As String = Database & "..spGetPaymentPlansDetailsAsPerPlan__c @PaymentPlanID=" & PaymentPlanID
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ViewState("SelectedPaymentPlan") = dt
                Dim oOrder As AptifyGenericEntityBase
                oOrder = ShoppingCart1.GetOrderObject(Session, Page.User, Application)
                Dim dAmt As Decimal = 0
                Dim dPaymentPlanAmount As Decimal = 0
                Dim oOL As Aptify.Applications.OrderEntry.OrderLinesEntity

                For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                    oOL = TryCast(oOrder.SubTypes("OrderLines").Item(i), OrderLinesEntity)

                    If dAmt = 0 Then
                        If Convert.ToBoolean(oOL.GetValue("TaxIncludedInPrice")) = True AndAlso oOL.SubTypes("OrderLineTaxes").Count > 0 Then
                            dAmt = CDec(Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount")))
                        Else
                            dAmt = Convert.ToDecimal(oOL.GetValue("Extended"))
                        End If
                    Else
                        If Convert.ToBoolean(oOL.GetValue("TaxIncludedInPrice")) = True AndAlso oOL.SubTypes("OrderLineTaxes").Count > 0 Then
                            dAmt = dAmt + CDec(Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount")))
                        Else
                            dAmt = dAmt + Convert.ToDecimal(oOL.GetValue("Extended"))
                        End If
                    End If
                    Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(oOL.ProductID)
                    Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If iProductPaymentPlan > 0 Then
                        If dPaymentPlanAmount = 0 Then
                            If Convert.ToBoolean(oOL.GetValue("TaxIncludedInPrice")) = True AndAlso oOL.SubTypes("OrderLineTaxes").Count > 0 Then
                                dPaymentPlanAmount = CDec(Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount")))
                            Else
                                dPaymentPlanAmount = Convert.ToDecimal(oOL.GetValue("Extended"))
                            End If
                        Else
                            If Convert.ToBoolean(oOL.GetValue("TaxIncludedInPrice")) = True AndAlso oOL.SubTypes("OrderLineTaxes").Count > 0 Then
                                dPaymentPlanAmount = dPaymentPlanAmount + CDec(Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount")))
                            Else
                                dPaymentPlanAmount = dPaymentPlanAmount + Convert.ToDecimal(oOL.GetValue("Extended"))
                            End If
                        End If
                    End If
                Next
                lblStagePaymentTotal.Text = ViewState("CurrencyTypeID") & Format(CDec(dPaymentPlanAmount), "0.00")
                lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
                lblTaxAmount.Text = ViewState("CurrencyTypeID") & Format(Convert.ToDouble(oOrder.GetValue("CALC_SalesTax")), "0.00")
                lblShiipingStageAmount.Text = ViewState("CurrencyTypeID") & Format(Convert.ToDouble(oOrder.GetValue("CALC_ShippingCharge")), "0.00")
                lblHandlingStageAmount.Text = ViewState("CurrencyTypeID") & Format(Convert.ToDouble(oOrder.GetValue("CALC_HandlingCharge")), "0.00")
                Dim dShippingCHarges As Decimal = oOrder.GetValue("CALC_ShippingCharge")
                Dim dHandlingCharges As Decimal = oOrder.GetValue("CALC_HandlingCharge")
                radPaymentPlanDetails.Visible = True
                radPaymentPlanDetails.DataSource = dt
                radPaymentPlanDetails.DataBind()
                For Each drP As DataRow In dt.Rows
                    Dim dStageAmt As Decimal = CDec(Convert.ToString(lblStagePaymentTotal.Text).Substring(1, Convert.ToString(lblStagePaymentTotal.Text).Length - 1))
                    Dim dStudentPayAmt As Decimal = dAmt
                    Dim dPercentageAmt As Decimal = (dStageAmt * CDec(drP("Percentage"))) / 100
                    If CInt(drP("days")) = 0 Then
                        Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                        lblIntialAmount.Text = Format(CDec(dintialStageAmount + dPercentageAmt), "0.00")
                        lblStageZeroDaysAmt.Text = ViewState("CurrencyTypeID") & Format(dPercentageAmt, "0.00")
                        lblWithoutStageAmt.Text = Format(CDec(dintialStageAmount), "0.00")
                    Else
                        Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                        lblIntialAmount.Text = Format(CDec(dintialStageAmount), "0.00")
                        lblWithoutStageAmt.Text = Format(CDec(dintialStageAmount), "0.00")
                        lblStageZeroDaysAmt.Text = ViewState("CurrencyTypeID") & Format(0, "0.00")
                    End If
                    Exit For
                Next

                Dim taxAmt As Decimal = Convert.ToString(lblTaxAmount.Text).Substring(1, Convert.ToString(lblTaxAmount.Text).Length - 1).Trim
                lblIntialAmount.Text = Format((CDec(lblIntialAmount.Text) + taxAmt + dShippingCHarges + dHandlingCharges), "0.00")
                lblWithoutStageAmt.Text = Format((CDec(lblWithoutStageAmt.Text) + taxAmt + dShippingCHarges + dHandlingCharges), "0.00")
                tblStageDetails.Visible = True
            Else
                radPaymentPlanDetails.Visible = False
                tblStageDetails.Visible = False
                ViewState("SelectedPaymentPlan") = Nothing
            End If
        End Sub
        Public Function SetStagePaymentAmount(ByVal Percentage As Decimal, ByVal days As Integer) As String
            Try
                Dim dStageAmt As Decimal = CDec(Convert.ToString(lblStagePaymentTotal.Text).Substring(1, Convert.ToString(lblStagePaymentTotal.Text).Length - 1))
                Dim dStudentPayAmt As Decimal = CDec(Convert.ToString(lblTotalAmount.Text).Substring(1, Convert.ToString(lblTotalAmount.Text).Length - 1))
                Dim dPercentageAmt As Decimal = (dStageAmt * Percentage) / 100
                If days = 0 Then
                    Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                    lblIntialAmount.Text = Format(CDec(dintialStageAmount + dPercentageAmt), "0.00")
                Else
                    Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                    lblIntialAmount.Text = Format(CDec(dintialStageAmount), "0.00")
                End If
                Return ViewState("CurrencyTypeID") & Format(CDec(dPercentageAmt), "0.00")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return ""
            End Try
        End Function
        Private Sub GetPrefferedCurrency()
            Try
                'Commented by Vaishali - to check if we can correct currency from shopping cart 4/10/2017
                'Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & User1.PersonID
                'Dim dt As DataTable = DataAction.GetDataTable(sSql)
                'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                '    ViewState("CurrencyTypeID") = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                '    lblCurrency.Text = ViewState("CurrencyTypeID")
                '    lblWithoutStageAmountCurrency.Text = ViewState("CurrencyTypeID")
                '    lblTotalAmtCurrency.Text = ViewState("CurrencyTypeID")
                'End If
                Dim sSQl As String = "..spGetCurrencySymbol__c @CurrencyID=" & ShoppingCart1.CurrencyTypeID
                Dim sCurrecnySymbol As String = DataAction.ExecuteScalar(sSQl, IAptifyDataAction.DSLCacheSetting.BypassCache)
                ViewState("CurrencyTypeID") = sCurrecnySymbol.Trim
                lblCurrency.Text = ViewState("CurrencyTypeID")
                lblWithoutStageAmountCurrency.Text = ViewState("CurrencyTypeID")
                lblTotalAmtCurrency.Text = ViewState("CurrencyTypeID")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadDefaultAddress(ByVal sPrefix As String,
                               ByRef oOrder As AptifyGenericEntityBase)
            Try
                With User1
                    oOrder.SetValue(sPrefix & "AddrLine1", .GetValue("AddressLine1"))
                    oOrder.SetValue(sPrefix & "AddrLine2", .GetValue("AddressLine2"))
                    oOrder.SetValue(sPrefix & "AddrLine3", .GetValue("AddressLine3"))
                    oOrder.SetValue(sPrefix & "City", .GetValue("City"))
                    oOrder.SetValue(sPrefix & "State", .GetValue("State"))
                    oOrder.SetValue(sPrefix & "ZipCode", .GetValue("ZipCode"))
                    oOrder.SetValue(sPrefix & "Country", .GetValue("Country"))
                    oOrder.SetValue(sPrefix & "AreaCode", .GetValue("PhoneAreaCode"))
                    oOrder.SetValue(sPrefix & "Phone", .GetValue("Phone"))
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadDefaultAddress(ByVal sPrefix As String,
                                     ByRef oOrder As AptifyGenericEntityBase,
                                     ByVal PrefAddress As String)
            Try
                With User1
                    Select Case PrefAddress
                        Case "Home"
                            oOrder.SetValue(sPrefix & "AddrLine1", .GetValue("HomeAddressLine1"))
                            oOrder.SetValue(sPrefix & "AddrLine2", .GetValue("HomeAddressLine2"))
                            oOrder.SetValue(sPrefix & "AddrLine3", .GetValue("HomeAddressLine3"))
                            oOrder.SetValue(sPrefix & "City", .GetValue("HomeCity"))
                            oOrder.SetValue(sPrefix & "State", .GetValue("HomeState"))
                            oOrder.SetValue(sPrefix & "ZipCode", .GetValue("HomeZipCode"))
                            oOrder.SetValue(sPrefix & "Country", .GetValue("HomeCountry"))
                        Case "Business"
                            oOrder.SetValue(sPrefix & "AddrLine1", .GetValue("AddressLine1"))
                            oOrder.SetValue(sPrefix & "AddrLine2", .GetValue("AddressLine2"))
                            oOrder.SetValue(sPrefix & "AddrLine3", .GetValue("AddressLine3"))
                            oOrder.SetValue(sPrefix & "City", .GetValue("City"))
                            oOrder.SetValue(sPrefix & "State", .GetValue("State"))
                            oOrder.SetValue(sPrefix & "ZipCode", .GetValue("ZipCode"))
                            oOrder.SetValue(sPrefix & "Country", .GetValue("Country"))
                        Case "Billing"
                            oOrder.SetValue(sPrefix & "AddrLine1", .GetValue("BillingAddressLine1"))
                            oOrder.SetValue(sPrefix & "AddrLine2", .GetValue("BillingAddressLine2"))
                            oOrder.SetValue(sPrefix & "AddrLine3", .GetValue("BillingAddressLine3"))
                            oOrder.SetValue(sPrefix & "City", .GetValue("BillingCity"))
                            oOrder.SetValue(sPrefix & "State", .GetValue("BillingState"))
                            oOrder.SetValue(sPrefix & "ZipCode", .GetValue("BillingZipCode"))
                            oOrder.SetValue(sPrefix & "Country", .GetValue("BillingCountry"))
                        Case "POBox"
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

        Private Sub LoadBillAddress(ByVal oOrder As AptifyGenericEntityBase)
            Try
                ''Dim oOrder As AptifyGenericEntityBase
                ''oOrder = CartGrid2.Cart.GetOrderObject(Session, Page.User, Application)
                '02/05/08 RJK - Added back the check to see if the Ship To and Bill To Addresses have
                'been set.  Otherwise, it is not possible to change the address to a non-default Address.
                'For the 5583 Issue related to User Profile changes, the Profile page has been updated to refresh
                'the Order's Ship To And Bill To Addresses if the Address is changed on the Profile.

                '//Vijay Sitlani - Changes made to partially resolve the bug reported by Alina for Issue 5583
                ' Changes made on 01-25-2008
                If Len(oOrder.GetValue("ShipToAddrLine1")) = 0 Then
                    'LoadDefaultAddress("ShipTo", oOrder)
                    LoadDefaultAddress("ShipTo", oOrder, User1.GetValue("PreferredShippingAddress"))

                End If
                ' Vijay Sitlani - Changes made to partially resolve the bug reported by Alina for Issue 5583
                ' Changes made on 01-25-2008
                If Len(oOrder.GetValue("BillToAddrLine1")) = 0 Then
                    'LoadDefaultAddress("BillTo", oOrder)
                    LoadDefaultAddress("BillTo", oOrder, User1.GetValue("PreferredBillingAddress"))
                End If
                With User1
                    NameAddressBlock.Name = .FirstName & " " & .LastName
                    If Len(.Company) > 0 Then
                        NameAddressBlock.Name = NameAddressBlock.Name & "/" & .Company
                    End If
                End With
                With oOrder
                    NameAddressBlock.AddressLine1 = CStr(.GetValue("BillToAddrLine1"))
                    NameAddressBlock.AddressLine2 = CStr(.GetValue("BillToAddrLine2"))
                    NameAddressBlock.AddressLine3 = CStr(.GetValue("BillToAddrLine3"))
                    NameAddressBlock.City = CStr(.GetValue("BillToCity"))
                    NameAddressBlock.State = CStr(.GetValue("BillToState"))
                    NameAddressBlock.ZipCode = CStr(.GetValue("BillToZipCode"))
                    NameAddressBlock.Country = CStr(.GetValue("BillToCountry"))
                End With

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadCreditCardInfo(ByVal oOrder As AptifyGenericEntityBase, ByVal sProductIDs As String)

            Try
                'RashmiP, Issue 6781, 09/20/10, to show bill me later option button in credit card uc
                ' By vaishali to check if the person is firm admin
                ''Commented By Pradip 2017-04-11 Not required
                'If IsUserFirmAdmin() Then
                If CBool(Session("IsFirmPay")) = True Then
                    'CreditCard.DisableBillMeLater = False
                    'ShowBillMeLater(oOrder)
                    If IsTrainingTicketProductCategory(sProductIDs) Then
                        CreditCard.DisableBillMeLater = True
                    Else
                        CreditCard.DisableBillMeLater = False
                        ShowBillMeLater(oOrder)
                    End If
                Else
                    CreditCard.DisableBillMeLater = True
                End If
                '------------------------
                CreditCard.LoadCreditCardInfo()
                CreditCard.PaymentTypeID = CLng(oOrder.GetValue("PayTypeID"))
                CreditCard.CCNumber = CStr(oOrder.GetValue("CCAccountNumber"))
                CreditCard.CCExpireDate = CStr(oOrder.GetValue("CCExpireDate"))
                ' Changes made to add Credit Card security number feature on e-business site.
                ' Change made by Vijay Sitlani for Issue 5369
                ' CreditCard1.CCSecurityNumber = CStr(oOrder.GetValue("CCSecurityNumber"))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Finally

            End Try
        End Sub

        Private Sub RefreshGrid()
            Try
                CartGrid2.RefreshGrid()
                'Navin Prasad Issue 11032
                If CartGrid2.Grid.Items.Count > 0 Then
                    'cmdUpdateCart.Visible = True
                    If Not String.IsNullOrEmpty(OrderConfirmationPage) Then
                        cmdPlaceOrder.Enabled = True
                    End If
                    tblRowMain.Visible = True
                    lblNoItems.Visible = False
                Else
                    'cmdUpdateCart.Visible = False
                    cmdPlaceOrder.Enabled = False
                    tblRowMain.Visible = False
                    lblNoItems.Visible = True
                End If
                With CartGrid2.Cart
                    Me.OrderSummary__c.Refresh()
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub cmdPlaceOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlaceOrder.Click
            Dim lOrderID As Long
            Dim sError As String = ""
            'Dim Status As String = ""
            Dim oOrders As OrdersEntity
            'Dim RecipientID As Integer
            ' Dim arrlstRecipientID As New ArrayList
            'Dim arrlstRecipientID() As String
            Dim sendRenewalMail As Boolean = False
            Dim strSubscriberID As String = ""
            Dim strRenewalStatus As String = ""
            Dim dt As DataTable = Nothing
            ' Dim sSql As String
            Dim iShipID As Integer = 0
            Dim dTaxAmount As Decimal = 0
            Dim dShippingCharges As Decimal = 0
            Dim dHandlingCharges As Decimal = 0
            Dim oShipmentTypes As New Aptify.Framework.Web.eBusiness.CommonMethods(DataAction, AptifyApplication, User1, Database)
            Try
                lblError.Visible = False
                Dim isPayment As Boolean = True
                ShoppingCart1.SaveCart(Session)
                oOrders = ShoppingCart1.GetOrderObject(Session, Page.User, Application)
                Dim errorMessage As String = ""
                With oOrders
                    If oOrders IsNot Nothing AndAlso hidSubscriberID.Value.Trim <> "" Then
                        oOrders = ResetOrderObject(oOrders)
                    End If

                    'Code added by Govind Mande for redmine issue 15092
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
                                oOL.ExtendedOrderDetailEntity.SetValue("AttendeeID", User1.PersonID)
                                oOL.SetAddValue("__ExtendedAttributeObjectData", oOL.ExtendedOrderDetailEntity.GetObjectData(False))
                            End If
                        End If
                    Next
                    ' End #18675
                    'RashmiP issue 6781, 09/20/10
                    If CreditCard.BillMeLaterChecked Then
                        If String.IsNullOrEmpty(CreditCard.PONumber) Then
                            .SetValue("PONumber", BillMeLaterDisplayText)
                        Else
                            .SetValue("PONumber", CreditCard.PONumber)
                        End If
                        .SetValue("PayTypeID", CreditCard.PaymentTypeID)
                        ShoppingCart1.SaveCart(Session)
                        lOrderID = ShoppingCart1.PlaceOrder(Session, Application, Page.User, sError)
                    Else
                        Page.Validate()
                        If ddlPaymentPlan.SelectedValue <> "Select Payment Plan" AndAlso Not ViewState("SelectedPaymentPlan") Is Nothing Then
                            Dim oOrderPayInfo As PaymentInformation
                            Dim oPersonGE As Aptify.Applications.Persons.PersonsEntity = DirectCast(Me.AptifyApplication.GetEntityObject("Persons", User1.PersonID), Aptify.Applications.Persons.PersonsEntity)

                            Dim cmbSavedPaymentMethod As DropDownList = DirectCast(CreditCard.FindControl("cmbSavedPaymentMethod"), DropDownList)
                            If cmbSavedPaymentMethod.SelectedValue <> "" Then
                                With oPersonGE.SubTypes("PersonSavedPaymentMethods").Find("ID", cmbSavedPaymentMethod.SelectedValue)
                                    oOrders.SetValue("SavedPaymentMethodID__c", cmbSavedPaymentMethod.SelectedValue)
                                    oOrders.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                                    oOrders.SetValue("InitialPaymentAmount", lblIntialAmount.Text)
                                End With
                            Else
                                ' Create new Save payment Method
                                'Commented by Govind: For decliened staged payments issue
                                ''With oPersonGE.SubTypes("PersonSavedPaymentMethods").Add()
                                ''    .SetValue("PersonID", User1.PersonID)
                                ''    .SetValue("StartDate", Today.Date)
                                ''    .SetValue("IsActive", 1)
                                ''    .SetValue("Name", "SPM " & Today.Date)
                                ''    .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                                ''    .SetValue("CCAccountNumber", CreditCard.CCNumber)
                                ''    .SetValue("CCExpireDate", CreditCard.CCExpireDate)
                                ''    .SetValue("EndDate", CreditCard.CCExpireDate)
                                ''    If .Fields("PaymentInformationID").EmbeddedObjectExists Then
                                ''        oOrderPayInfo = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                                ''        If oOrderPayInfo IsNot Nothing Then
                                ''            oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCNumber
                                ''            .SetValue("CCPartial", oOrderPayInfo.GetCCPartial(CStr(CreditCard.CCNumber)))
                                ''        End If
                                ''    End If

                                ''End With
                            End If
                            'Commented by Govind: For decliened staged payments issue
                            ''Dim sPersonSaveError As String = String.Empty
                            ''If oPersonGE.Save(False, sPersonSaveError) Then
                            ''    oOrders.SetValue("SavedPaymentMethodID__c", oPersonGE.SubTypes("PersonSavedPaymentMethods").Item(oPersonGE.SubTypes("PersonSavedPaymentMethods").Count - 1).RecordID)
                            ''    oOrders.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                            ''    oOrders.SetValue("InitialPaymentAmount", (lblIntialAmount.Text))
                            ''    ' .SetValue("OrderTypeID", Aptify.Applications.OrderEntry.OrderType.Regular)   ' Regular Order 
                            ''    ' .OrderStatus = OrderStatus.Taken
                            ''    oOrders.PayTypeID = AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order")
                            ''    oOrders.PONumber = "Stage Payment"

                            ''End If
                            'Added for Redmine #20327
                            oOrders.SetValue("BillToSameAsShipTo", 0)
                            oOrders.SetValue("BillToCompanyID", -1)
                            'End Redmine #20327
                            oOrders.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                            oOrders.SetValue("InitialPaymentAmount", (lblIntialAmount.Text))
                            oOrders.SetValue("PayTypeID", CreditCard.PaymentTypeID)
                            oOrders.SetValue("CCAccountNumber", CreditCard.CCNumber)
                            oOrders.SetValue("CCExpireDate", CreditCard.CCExpireDate)
                            If CreditCard.CCNumber = "-Ref Transaction-" Then
                                oOrders.SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                                oOrders.SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                            End If
                            If Len(CreditCard.CCSecurityNumber) > 0 Then
                                oOrders.SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber) 'Neha changes for Issue 16675, 06/05/2013,Added CCSecurityNumber as a temperory field for not storing in record history.
                            End If
                            If oOrders.Fields("PaymentInformationID").EmbeddedObjectExists Then
                                oOrderPayInfo = DirectCast(oOrders.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                                oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                                ''RashmiP, Issue 10254, SPM
                                oOrderPayInfo.SetValue("SaveForFutureUse", 1)
                                'Ansar Shaikh - Issue 11986 - 12/27/2011
                                'Ani B for issue 10254 on 22/04/2013
                                'Set CC Partial for credit cart type is reference transaction 
                                If CreditCard.CCNumber = "-Ref Transaction-" Then
                                    oOrderPayInfo.SetValue("CCPartial", CreditCard.CCPartial)
                                End If
                            End If
                            oOrders.SetAddValue("_xBypassCreditCheck", 1)

                            dTaxAmount = Convert.ToDecimal(.GetValue("CALC_SalesTax"))
                            dShippingCharges = Convert.ToDecimal(.GetValue("CALC_ShippingCharge"))
                            dHandlingCharges = Convert.ToDecimal(.GetValue("CALC_HandlingCharge"))
                            Dim str As String = String.Empty
                            oOrders.SetValue("OrderType", "Regular")
                            'lOrderID = ShoppingCart1.PlaceOrder(Session, Application, Page.User, sError)
                            If oOrders.Save(False, str) Then
                                lOrderID = oOrders.RecordID
                            End If
                            errorMessage = str
                        Else
                            oOrders.SetValue("PayTypeID", CreditCard.PaymentTypeID)
                            oOrders.SetValue("CCAccountNumber", CreditCard.CCNumber)
                            oOrders.SetValue("CCExpireDate", CreditCard.CCExpireDate)
                            If CreditCard.CCNumber = "-Ref Transaction-" Then
                                oOrders.SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                                oOrders.SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                            End If
                            If Len(CreditCard.CCSecurityNumber) > 0 Then
                                oOrders.SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber) 'Neha changes for Issue 16675, 06/05/2013,Added CCSecurityNumber as a temperory field for not storing in record history.
                            End If
                            If oOrders.Fields("PaymentInformationID").EmbeddedObjectExists Then
                                Dim oOrderPayInfo As PaymentInformation = DirectCast(oOrders.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                                oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                                ''RashmiP, Issue 10254, SPM
                                oOrderPayInfo.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                                'Ansar Shaikh - Issue 11986 - 12/27/2011
                                'Ani B for issue 10254 on 22/04/2013
                                'Set CC Partial for credit cart type is reference transaction 
                                If CreditCard.CCNumber = "-Ref Transaction-" Then
                                    oOrderPayInfo.SetValue("CCPartial", CreditCard.CCPartial)
                                End If
                            End If
                            Dim str As String = String.Empty
                            oOrders.SetValue("OrderType", "Regular")
                            'lOrderID = ShoppingCart1.PlaceOrder(Session, Application, Page.User, sError)
                            If oOrders.Save(False, str) Then
                                lOrderID = oOrders.RecordID
                            End If
                            errorMessage = str
                            ' lOrderID = ShoppingCart1.PlaceOrder(Session, Application, Page.User, sError)
                        End If
                    End If
                End With

                If lOrderID > 0 Then

                    'Dim sSQLPF As String = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='Send Order Confirmation Email'"
                    'Dim lProcessFlowID As Long = CLng(DataAction.ExecuteScalar(sSQLPF, IAptifyDataAction.DSLCacheSetting.UseCache))

                    'Dim context As New AptifyContext
                    'context.Properties.AddProperty("OrderGE", oOrders)
                    'context.Properties.AddProperty("OrderID", oOrders.RecordID)
                    ''Added by Dipali Issue No:13305
                    'context.Properties.AddProperty("ShipToPersonID", oOrders.ShipToID)
                    'context.Properties.AddProperty("BillToPersonID", oOrders.BillToID)
                    'context.Properties.AddProperty("OrderConfirmationEmailRecipient", "ShipToPersonID")
                    'Dim result As ProcessFlowResult
                    'result = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)

                    '' If sendRenewalMail = True Then
                    'If hidSubscriberID.Value.Trim <> "" Then
                    '    arrlstRecipientID = hidSubscriberID.Value.Trim.Split(",").ToArray
                    '    For i = 0 To CInt(arrlstRecipientID.Length) - 1
                    '        RecipientID = CInt(arrlstRecipientID(i))
                    '        SendMailToRecipient(RecipientID, strRenewalStatus)
                    '    Next
                    'End If


                    ''Suraj Issue 16516  we remove the Session("SubscriptionOrder") 
                    ''End If

                    ''cOMMENTED By Pradip 2017-04-13 for Prrformance Issue Now Directly Inserting data to table
                    'Dim oOrderGE As OrdersEntity = CType(AptifyApplication.GetEntityObject("Orders", lOrderID), OrdersEntity)
                    '' Code added by govind if appeal category product present then create appeal application
                    'If oOrderGE.SubTypes("OrderLines") IsNot Nothing AndAlso oOrderGE.SubTypes("OrderLines").Count > 0 Then
                    '    For i As Integer = 0 To oOrderGE.SubTypes("OrderLines").Count - 1
                    '        Dim sAppealProductSQL As String = Database & "..spGetAppealCategoryProduct__c @ProductID=" & Convert.ToInt32(oOrderGE.SubTypes("OrderLines").Item(i).GetValue("ProductID"))
                    '        Dim lAppealProductID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sAppealProductSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    '        If lAppealProductID > 0 Then
                    '            CreateAppealApplication(lAppealProductID, Convert.ToInt32(oOrderGE.SubTypes("OrderLines").Item(i).GetValue("ClassRegistrationID__c")), Convert.ToInt32(oOrderGE.SubTypes("OrderLines").Item(i).GetValue("AppealTypeResonID__c")), Convert.ToString(oOrderGE.SubTypes("OrderLines").Item(i).GetValue("Comments")), lOrderID)
                    '        End If

                    '    Next
                    'End If


                    Dim param(1) As Data.IDataParameter
                    param(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, User1.PersonID)
                    param(1) = DataAction.GetDataParameter("@OrderID", SqlDbType.Int, lOrderID)

                    Dim sSQL As String = String.Empty
                    sSQL = Database & "..spCreateAppealsAppFromBilling__c"
                    Dim recordInsert As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param, 180)

                    ShoppingCart1.Clear(Nothing)
                    Session("IsFirmPay") = False
                    Response.Redirect(OrderConfirmationPage & "?ID=" & lOrderID & "&IsBilling=1", False) ' Redmine log #18364
                    lblError.Visible = False
                Else
                    lblError.Visible = True
                    lblError.Text = errorMessage & "  Credit Card Verification Failed - There was a problem with your card, please consult your bank"
                    lblError.ForeColor = Drawing.Color.Red
                    lblError.Style.Add("font-size", "medium")
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Code Added By Govind  on 22 May 2015 For Order Line wise Payment
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <param name="TaxAmount"></param>
        ''' <remarks></remarks>
        'Private Function PostPayment(ByVal OrderID As Long, ByVal TaxAmount As Decimal, ByVal ShippingCharges As Decimal, ByVal HandlingCharge As Decimal, ByRef order1 As OrdersEntity) As Boolean ', ByVal sTrans As String) As Boolean
        '    Try
        '        Dim oPayment As AptifyGenericEntityBase
        '        Dim bFlag As Boolean = False
        '        Dim bNotification As Boolean = False
        '        Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity = Nothing
        '        Dim sql As String = "spGetPaymentLevelID__c"
        '        Dim PaymentLevelID As Integer
        '        PaymentLevelID = Convert.ToInt16(DataAction.ExecuteScalar(sql, IAptifyDataAction.DSLCacheSetting.BypassCache))
        '        oPayment = AptifyApplication.GetEntityObject("Payments", -1)
        '        With oPayment
        '            '06/04/17 - commenetd code get payment GL - Vaishali
        '            ''SetValue("PaymentLevelID", PaymentLevelID)

        '            .SetValue("EmployeeID", EBusinessGlobal.WebEmployeeID(Page.Application))
        '            .SetValue("PersonID", User1.PersonID)
        '            .SetValue("PaymentDate", Date.Today)
        '            .SetValue("DepositDate", Date.Today)
        '            .SetValue("EffectiveDate", Date.Today)
        '            .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
        '            If CreditCard.CCNumber = "-Ref Transaction-" Then
        '                .SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
        '                .SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
        '            End If
        '            .SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber)
        '            Dim sSql As String = Database & "..spGetOrderDetailsForPayments__c @OrderID=" & OrderID
        '            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache) ', sTrans)
        '            Dim bIsFirmPay As Boolean = False
        '            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
        '                bIsFirmPay = Convert.ToBoolean(dt.Rows(0)("FirmPay__c"))
        '                For Each dr As DataRow In dt.Rows
        '                    If Convert.ToDecimal(dr("Amount")) > 0 Then
        '                        With .SubTypes("PaymentLines").Add()
        '                            .SetValue("AppliesTo", "Order Line")
        '                            .SetValue("OrderID", dr("OrderID"))
        '                            .SetValue("OrderDetailID", dr("OrderDetailID"))
        '                            .SetValue("LineNumber", dr("Sequence"))
        '                            .SetValue("Amount", dr("Amount"))
        '                            .SetValue("BillToPersonID", User1.PersonID)
        '                        End With
        '                    End If
        '                Next
        '                ' For Tax Payment 
        '                If TaxAmount > 0 Then
        '                    With .SubTypes("PaymentLines").Add()
        '                        .SetValue("AppliesTo", "Tax")
        '                        .SetValue("OrderID", OrderID)
        '                        .SetValue("Amount", TaxAmount)
        '                        .SetValue("BillToPersonID", User1.PersonID)
        '                    End With
        '                End If

        '                ' For Shipping Charges
        '                If ShippingCharges > 0 Then
        '                    With .SubTypes("PaymentLines").Add()
        '                        .SetValue("AppliesTo", "Shipping")
        '                        .SetValue("OrderID", OrderID)
        '                        .SetValue("Amount", ShippingCharges)
        '                        .SetValue("BillToPersonID", User1.PersonID)
        '                    End With
        '                End If
        '                ' For Handling Charges
        '                If HandlingCharge > 0 Then
        '                    With .SubTypes("PaymentLines").Add()
        '                        .SetValue("AppliesTo", "HandlingCharge")
        '                        .SetValue("OrderID", OrderID)
        '                        .SetValue("Amount", ShippingCharges)
        '                        .SetValue("BillToPersonID", User1.PersonID)
        '                    End With
        '                End If
        '            End If
        '            'Siddharth: Added to check if Firm is Paying for firm admin order cc payment fail issue.
        '            If bIsFirmPay Then
        '                .SetValue("CompanyID", User1.CompanyID)
        '            End If
        '            .SetValue("CCAccountNumber", CreditCard.CCNumber)
        '            .SetValue("CCExpireDate", CreditCard.CCExpireDate)
        '            If .Fields("PaymentInformationID").EmbeddedObjectExists Then
        '                Dim payInformation As PaymentInformation = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
        '                payInformation.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
        '                payInformation.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
        '                payInformation.SetValue("CCPartial", CreditCard.CCPartial)
        '            End If
        '            Dim errorMessage As String = String.Empty
        '            If .Save(False, errorMessage) Then ', sTrans
        '                'ViewState("AddedTaxAmount") 

        '                Return True
        '            Else
        '                lblError.Visible = True
        '                lblError.Text = errorMessage + "Credit Card Verification Failed"
        '                lblError.ForeColor = Drawing.Color.Red
        '                lblError.Style.Add("font-size", "medium")
        '                order1.Delete()
        '            End If
        '        End With
        '        Return False
        '    Catch ex As Exception
        '        order1.Delete()
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '        Return False
        '    End Try
        'End Function

        'code added by Govind for creating Appeal Application
        Private Sub CreateAppealApplication(ByVal ProductID As Long, ByVal ClassRegistrationID As Long, ByVal AppealTypeResonID As Long, ByVal Description As String, ByVal lOrderID As Long)
            Try
                Dim sSql As String = Database & "..spGetClassRegistrationDetails__c @ClassRegistrationID=" & ClassRegistrationID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    Dim oGE As AptifyGenericEntityBase
                    oGE = AptifyApplication.GetEntityObject("AppealsApplicationDetail__c", -1)
                    With oGE
                        .SetValue("ApplicantID", User1.PersonID)

                        .SetValue("CourseID", Convert.ToInt32(dt.Rows(0)("CourseID")))
                        .SetValue("ClassID", Convert.ToInt32(dt.Rows(0)("ClassID")))
                        .SetValue("ClassRegistrationID", ClassRegistrationID)
                        .SetValue("AppealReasonID", Convert.ToInt32(AppealTypeResonID))
                        .SetValue("Description", Convert.ToString(Description))
                        .SetValue("OrderID", lOrderID)
                        .SetValue("Status", "Submitted to CAI")
                        .SetValue("AcademicCycleID", Convert.ToString(dt.Rows(0)("AcademicCycleID__c")))
                        'Dim sSqlAcademicCycle As String = "..spGetAcademicCycleYearAsPerCurrentDate__c @Year=" & Today.Year
                        'Dim dtAcademicCycle As DataTable = DataAction.GetDataTable(sSqlAcademicCycle, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        'If Not dtAcademicCycle Is Nothing AndAlso dtAcademicCycle.Rows.Count > 0 Then
                        '    .SetValue("AcademicCycleID", Convert.ToString(dtAcademicCycle.Rows(0)("ID")))
                        'End If

                        ''Added By Pradip 2015-11-09 To Save Source in  AppealsApplicationDetail__c Record
                        If Convert.ToInt32(dt.Rows(0)("IsLLL")) > 0 Then
                            .SetValue("Source", "LLL")
                        End If
                        If .Save() Then
                        End If
                    End With
                Else
                    Dim oGE As AptifyGenericEntityBase
                    oGE = AptifyApplication.GetEntityObject("AppealsApplicationDetail__c", -1)
                    With oGE
                        .SetValue("ApplicantID", User1.PersonID)
                        Dim sClassRegiID As String = Database & "..spGetClassRegistrationIDFromOrderID__c @OrderID=" & lOrderID
                        Dim lClassRegiID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sClassRegiID, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If lClassRegiID > 0 Then
                            .SetValue("ClassRegistrationID", lClassRegiID)
                            Dim sClassSql As String = Database & "..spGetClassRegistrationDetails__c @ClassRegistrationID=" & lClassRegiID
                            Dim dtClass As DataTable = DataAction.GetDataTable(sClassSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                            If Not dtClass Is Nothing AndAlso dtClass.Rows.Count > 0 Then
                                .SetValue("CourseID", Convert.ToInt32(dtClass.Rows(0)("CourseID")))
                                .SetValue("ClassID", Convert.ToInt32(dtClass.Rows(0)("ClassID")))
                                ''Added By Pradip 2015-11-09 To Add Source LLL For AppealsApplicationDetail__c
                                If Convert.ToInt32(dtClass.Rows(0)("IsLLL")) > 0 Then
                                    .SetValue("Source", "LLL")
                                End If
                            End If
                        End If
                        .SetValue("AppealReasonID", Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("AppealReason__c", "Comprehensive Student Reports")))
                        .SetValue("OrderID", lOrderID)
                        .SetValue("Status", "Submitted to CAI")
                        Dim sSqlAcademicCycle As String = "..spGetAcademicCycleYearAsPerCurrentDate__c @Year=" & Today.Year
                        Dim dtAcademicCycle As DataTable = DataAction.GetDataTable(sSqlAcademicCycle, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        If Not dtAcademicCycle Is Nothing AndAlso dtAcademicCycle.Rows.Count > 0 Then
                            .SetValue("AcademicCycleID", Convert.ToString(dtAcademicCycle.Rows(0)("ID")))
                        End If
                        If .Save() Then
                        End If
                    End With
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        '''RashmiP, Issue 14956, Reset Order object if Order is for Renew Membership or Subscription
        Private Function ResetOrderObject(ByVal oOrders As OrdersEntity) As OrdersEntity
            'Suraj Issue 16516  we remove the Session("SubscriptionOrder") from renew subscription and renew membership control because while we are changing
            'the session mode to state server so it will throuth the serialization problem also we remove the _xRenewalStatus temp fiels from order obj so insteade of this we are 
            'using the Comments field
            Dim oOrderSubscription As AptifyGenericEntityBase = ShoppingCart1.GetOrderObject(Me.Session, Me.Page.User, Me.Application)
            Dim strSubscriberID1 As String = ""
            Dim strProductID1 As String = ""

            Dim strSubscriberID2 As String = ""
            Dim strProductID2 As String = ""
            Try
                If oOrders.SubTypes("OrderLines") IsNot Nothing AndAlso oOrderSubscription IsNot Nothing Then
                    For i As Integer = 0 To oOrders.SubTypes("OrderLines").Count - 1
                        If oOrders.SubTypes("OrderLines").Item(i).GetValue("Comments") IsNot Nothing AndAlso oOrders.SubTypes("OrderLines").Item(i).GetValue("SubscriberID") IsNot Nothing Then

                            strSubscriberID1 = oOrders.SubTypes("OrderLines").Item(i).GetValue("SubscriberID").ToString().Trim
                            strProductID1 = oOrders.SubTypes("OrderLines").Item(i).GetValue("ProductID").ToString().Trim

                            For j As Integer = 0 To oOrderSubscription.SubTypes("OrderLines").Count - 1

                                If oOrderSubscription.SubTypes("OrderLines").Item(i).GetValue("SubscriberID") IsNot Nothing Then

                                    strSubscriberID2 = oOrderSubscription.SubTypes("OrderLines").Item(j).GetValue("SubscriberID").ToString().Trim
                                    strProductID2 = oOrderSubscription.SubTypes("OrderLines").Item(j).GetValue("ProductID").ToString().Trim

                                    If strSubscriberID1 = strSubscriberID2 And strProductID1 = strProductID2 Then
                                        ''RashmiP, If Quotation order exist for any subscription/membership then cancle that order.
                                        Dim ExistingOrderID As Long = CLng(oOrderSubscription.SubTypes("OrderLines").Item(j).GetValue("_xExistingOrderID"))
                                        If ExistingOrderID > 0 Then
                                            CancelSubscriptionOrder(ExistingOrderID)
                                        End If
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
                Return oOrders
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return Nothing
        End Function
        Private Function GetMessageTemplateID(ByVal sTemplateName As String) As Integer
            Try
                Dim sSql As String
                Dim dt As DataTable
                sSql = "Select ID from vwMessageTemplates where name  = '" & sTemplateName & "'"
                dt = DataAction.GetDataTable(sSql)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Return CInt(dt.Rows(0).Item("ID"))
                End If
                Return -1
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return -1
            End Try
        End Function

        Public Sub SendMailToRecipient(ByVal RecipientID As Integer, ByVal RenewalType As String)
            Try
                'Get the Process Flow ID to be used for sending the Order Confirmation Email
                Dim sSQL As String = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='Send Renew Subscriptions Notification Email'"
                Dim lProcessFlowID As Long = CLng(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache))
                Dim lMessageTemplateID As Long
                Dim context As New AptifyContext
                ''Rashmi P, Issue 14956, select Message template according to product Type.
                If RenewalType = "RENEWMEMBERSHIP" Then
                    lMessageTemplateID = GetMessageTemplateID("Membership Renewed by Admin")
                Else
                    lMessageTemplateID = GetMessageTemplateID("Renew Subscriptions Notification")
                End If


                If lMessageTemplateID <= 0 Then
                    lblError.Text = "Message Template does not exist."
                Else
                    context.Properties.AddProperty("MessageTemplateID", lMessageTemplateID)
                    context.Properties.AddProperty("PersonID", RecipientID)

                    Dim result As ProcessFlowResult
                    result = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
                    If result.IsSuccess Then
                        lblError.ForeColor = Drawing.Color.Blue
                        lblError.Text = "Notification sent to the members about their membership renewal."
                    Else
                        lblError.ForeColor = Drawing.Color.Red
                        lblError.Text = "Email Notification failed. Contact Customer Service for help."
                    End If
                End If
            Catch ex As ArgumentException
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'HP Issue#8972: based on the grandTotal of all orderlines toggle validators requiring credit card information
        Private Sub SetCreditCardValidators(ByVal totalDue As Decimal)
            With CreditCard
                If totalDue > 0 Then
                    .CCNumberValidatorSetting = True
                    .CCSecurityNumberValidatorSetting = True
                Else
                    .CCNumberValidatorSetting = False
                    .CCSecurityNumberValidatorSetting = False
                End If
            End With
        End Sub

        Private Sub OrderFailed(ByVal sError As String)
            lblError.Text = "<b>Order Failed To Save</b><BR /><hr />" & sError
            lblError.Visible = True
            Me.tblRowMain.Visible = False
            Me.cmdPlaceOrder.Visible = False
            lblGotItems.Visible = False
            lblNoItems.Visible = False
            Me.OrderSummary__c.Visible = False
            '(Siddharth) Hide Payment message on error
            lblPaymentMsg.Text = ""
            'Code change end
        End Sub
        'Navin Prasad Issue 9388
        Private Sub ProductDownloadFailed(ByVal sError As String)
            lblError.Text = "<b>Failed To Create Downloable Product Info</b><BR /><hr />" & sError
            lblError.Visible = True
        End Sub
        Private Sub cmdBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBack.Click
            Response.Redirect(BackButtonPage)
        End Sub

        ''' <summary>
        ''' RashmiP issue 6781, 09/20/10
        ''' procedure set properties of credit card, if Company and User's credit Status is approved and credit limit is availabe 
        ''' contion check if payment type is Bill Me Later. 
        ''' </summary>
        Private Sub ShowBillMeLater(ByVal a_oOrder As AptifyGenericEntityBase)
            Dim iPrevPaymentTypeID As Integer
            Dim iPOPaymentType As Integer = 0
            Dim sError As String
            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity

            Try
                If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID")) Then
                    iPOPaymentType = CInt(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID"))
                End If
                Dim dr As Data.DataRow = User1.CompanyDataRow
                CreditCard.UserCreditStatus = CInt(User1.GetValue("CreditStatusID"))
                CreditCard.UserCreditLimit = CLng(User1.GetValue("CreditLimit"))
                oOrder = CType(a_oOrder, OrdersEntity)
                If iPOPaymentType > 0 Then
                    If dr IsNot Nothing Then
                        CreditCard.CompanyCreditStatus = CInt(dr.Item("CreditStatusID"))
                        CreditCard.CompanyCreditLimit = CLng(dr.Item("CreditLimit"))
                    End If
                    If oOrder IsNot Nothing Then
                        iPrevPaymentTypeID = CInt(oOrder.GetValue("PayTypeID"))
                        oOrder.SetValue("PayTypeID", iPOPaymentType)
                        CreditCard.CreditCheckLimit = ShoppingCart1.CreditCheckObject.CheckCredit(CType(oOrder, Aptify.Applications.OrderEntry.OrdersEntity), sError)
                    End If
                End If
                oOrder.SubTypes("OrderLines").Item(0).GetValue("ParentSequence")
            Catch ex As Exception

            Finally
                oOrder.SetValue("PayTypeID", iPrevPaymentTypeID)
            End Try

        End Sub

        '''RashmiP, Issue 14326, Send Meeting Registration Mails to all the attendees
        ''' 11/5/12

        'Amruta , Issue 14381,5/8/2013 ,Commented function as we’re not going to send automatically any messages for meeting registrations.

        'Use this function to send an email automatically to every person who is registered for a meeting on this order. 
        'By default, this function is not called within this control. It is provided here as an example of how you can trigger a process flow to send an email to registrants as part of the meeting registration process. 
        'If you decide to send an email using this function, you should create a new process flow that sends an email based on a particular message template that your organization wants to use and identify that process flow in the function below.

        'Private Function SendMeetingRegistrationMail(ByVal lOrderID As Long) As Boolean
        '    Try
        '        Dim sSql, sEmail, sProductType As String, bSucess As Boolean
        '        Dim dt As DataTable
        '        'Amruta Issue 14381 ,18/4/2013,Query changes to send single mail to attendees
        '        sSql = "Select Distinct AttendeeID_Email, AttendeeID_FirstLast ,ProductType from " & Database & "..vwOrderMeetDetail vw inner join " & Database & "..vwProducts p on vw.ProductID= p.ID where OrderID = " & lOrderID

        '        dt = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)

        '        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
        '            For Each rw As DataRow In dt.Rows
        '                sProductType = CStr(rw.Item("ProductType"))
        '                If sProductType = "Meeting" Then
        '                    sEmail = CStr(rw.Item("AttendeeID_Email"))

        '                    sSql = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='Send eBusiness Order Confirmation Email'"
        '                    Dim lProcessFlowID As Long = CLng(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.UseCache))

        '                    Dim context As New AptifyContext
        '                    context.Properties.AddProperty("OrderID", lOrderID)
        '                    context.Properties.AddProperty("EmailAddresses", sEmail)
        '                    context.Properties.AddProperty("MessageSystem", "DOT NET MAIL")
        '                    Dim result As ProcessFlowResult
        '                    result = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
        '                    If result.IsSuccess Then
        '                        bSucess = True
        '                    Else
        '                        bSucess = False
        '                    End If

        '                End If
        '            Next
        '        End If
        '    Catch ex As Exception

        '    End Try
        'End Function

        ''' <summary>
        ''' Rashmi P, Issue 5133, 12/6/12 Add ShipmentType Selection.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadShipmentType(ByVal oOrder As OrdersEntity, ByVal bIncludeInShipping As Boolean)
            'Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
            ' oOrder = CType(a_oOrder, OrdersEntity)
            ''oOrder = CartGrid2.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
            Dim oShipmentTypes As New Aptify.Framework.Web.eBusiness.CommonMethods(DataAction, AptifyApplication, User1, Database)
            Dim dt As DataTable = Nothing
            'Dim bIncludeInShipping As Boolean
            Try
                If oOrder IsNot Nothing Then
                    If bIncludeInShipping Then
                        dt = oShipmentTypes.LoadShipmentType(CInt(oOrder.GetValue("ShipToCountryCodeID")))
                    End If
                End If

                If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                    tdShipment.Visible = False
                    Dim sSql As String = "SELECT Top 1 ID FROM " & AptifyApplication.GetEntityBaseDatabase("Shipment Types") & ".." & AptifyApplication.GetEntityBaseView("Shipment Types")
                    Dim iShipID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql))
                    If iShipID > 0 Then
                        oOrder.ShipTypeID = iShipID
                    End If
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
                oOrder = CartGrid2.Cart.GetOrderObject(Session, Page.User, Page.Application)
                oOrder.SetValue("ShipTypeID", ddlShipmentType.SelectedValue)

                oOrder.CalculateOrderTotals(True, True)
                CartGrid2.Cart.SaveCart(Session)
                RefreshGrid()
            End If
        End Sub
        ''' <summary>
        ''' Rashmi P, Issue 14318, Renew MemberShip/Subscription
        ''' Function handle the existing quotation order if one exists tied to that recipient/product , set the order status to Cancelled
        ''' Otherwise, the quotation order will show up in the Pay Off Orders grid and if the admin pays off the quotation order, 
        ''' then the admin will inadvertently renew the subscription/membership twice.
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <remarks></remarks>
        Private Sub CancelSubscriptionOrder(ByVal OrderID As Long)
            Try
                Dim oOrderGe As AptifyGenericEntityBase
                oOrderGe = Me.AptifyApplication.GetEntityObject("Orders", OrderID)
                oOrderGe.SetValue("OrderStatusID", OrderStatus.Cancelled)
                oOrderGe.Save(False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub AddGtmCheckout()
            Dim checkout As Checkout, checkoutDto As CheckoutDto

            checkoutDto = GetCheckoutDto()

            checkout = New Checkout(Me.Page, checkoutDto)
            checkout.Render()
        End Sub

        Private Function GetCheckoutDto() As CheckoutDto
            Dim checkoutDto As CheckoutDto

            Dim currencyType As String = "EUR"
            If User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro")) Then
                currencyType = "EUR"
            ElseIf User1.PreferredCurrencyTypeID = Convert.ToInt64(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c"))) Then
                currencyType = "GBP"
            End If

            checkoutDto = New CheckoutDto() With {
                .StepNumber = 2,
                .CallbackUrl = Request.RawUrl,
                .Currency = currencyType
                }

            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
            oOrder = CartGrid2.Cart.GetOrderObject(Session, Page.User, Page.Application)

            Dim position As Int32 = 0
            For Each orderLine As Aptify.Consulting.Entity.OrderLines.OrderLinesEntity__c In oOrder.SubTypes("OrderLines")
                checkoutDto.Products.Add(New ProductDto With {
                                            .Id = orderLine.ProductID.ToString,
                                            .Name = orderLine.Product,
                                            .Price = orderLine.Price,
                                            .Brand = "Chartered Accountants",
                                            .Category = "",
                                            .List = orderLine.ProductType,
                                            .position = position,
                                            .Quantity = orderLine.Quantity,
                                            .Variant = ""
                                            })
                position += 1
            Next

            Return checkoutDto
        End Function


#Region "Custom"
        Private Function IsUserFirmAdmin() As Boolean
            Try
                Dim param(0) As Data.IDataParameter
                param(0) = DataAction.GetDataParameter("PersonID", SqlDbType.Int, User1.PersonID)
                Dim objAdmin As Object = DataAction.ExecuteScalarParametrized("spCheckFirmAdmin__c", CommandType.StoredProcedure, param)
                If Not objAdmin Is Nothing AndAlso Convert.ToInt32(objAdmin) > 0 Then
                    'hidIsFirmAdmin.Value = "1"
                    Return True

                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        'Rajesh K - 04/09/2014
        Private Function IsTrainingTicketProductCategory(ByVal sProductIDs As String) As Boolean
            Dim result As Boolean = False
            Try
                Dim param(0) As Data.IDataParameter
                param(0) = DataAction.GetDataParameter("@ProductIDs", SqlDbType.VarChar, sProductIDs.ToString())
                result = Convert.ToBoolean(DataAction.ExecuteScalarParametrized("spIsTrainingTicketProductCategory__c", CommandType.StoredProcedure, param))
            Catch ex As Exception
                Return result
            End Try
            Return result
        End Function
#End Region

        Private Function sTrans() As Object
            Throw New NotImplementedException
        End Function

    End Class
End Namespace
