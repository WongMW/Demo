'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                 13/10/2015                         create CTC Designation Application
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Applications.OrderEntry
Imports Aptify.Applications.Accounting

Namespace Aptify.Framework.Web.eBusiness
    Partial Class CTCDesignationApp__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "CTCDesignationApp__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"

#Region "Page Load"
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

        Public Overloads Property RedirectURL() As String
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

        Protected Overrides Sub SetProperties()
            Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(RedirectURL) Then
                Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            SetProperties()
            If User1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            End If
            lblScheduleText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.CTCDesignation.CouncilDateText__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            If Not IsPostBack Then
                GetPrefferedCurrency()
                CreditCard.LoadCreditCardInfo()
                LoadRecord()
                ExistCTCAPPRecord()
            End If
        End Sub

#End Region

#Region "Load Data"
        ''' <summary>
        ''' Load Person Detail with Member or Non Member Product with Price
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overridable Sub LoadRecord()
            Try
                Dim sSql As String = Database & "..spGetCTCDesignationPersonDetails__c @StudentID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblStudentNumber.Text = Convert.ToString(dt.Rows(0)("StudentNumber"))
                    lblStudentName.Text = Convert.ToString(dt.Rows(0)("Name"))
                    lblEmail.Text = Convert.ToString(dt.Rows(0)("Email"))
                    'lblProductName.Text = AptifyApplication.GetEntityRecordName("Products", Convert.ToInt32(dt.Rows(0)("ProductID")))
                    If Convert.ToString(dt.Rows(0)("CTCEligibleToJoin")) <> "" AndAlso IsDBNull(dt.Rows(0)("CTCEligibleToJoin")) = False Then
                        ViewState("CTCEligibleToJoin") = Convert.ToString(dt.Rows(0)("CTCEligibleToJoin"))
                    Else
                        ViewState("CTCEligibleToJoin") = ""
                    End If
                    ViewState("ProductID") = Convert.ToInt32(dt.Rows(0)("ProductID"))
                    lblProductPrice.Text = GetPrice(ViewState("ProductID"))
                    ViewState("MemberTypeID") = Convert.ToInt32(dt.Rows(0)("MemberTypeID"))
                    If CBool(dt.Rows(0)("IsMember")) Then
                        lblRequest.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.CTCDesignation.CTCMemberText__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Else
                        idProfessionalBodyDiv.Visible = True
                        lblRequest.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.CTCDesignation.CTCNonMemberText__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                    TermsAndCondition()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub TermsAndCondition()
            Try
                Dim sSql As String = Database & "..spGetTermsConditionsDetails__c @ProductID=" & ViewState("ProductID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    divTermsCondition.Visible = True
                    chkTermsAndConditions.Text = Convert.ToString(dt.Rows(0)("Name"))
                    lblWebDesc.Text = Convert.ToString(dt.Rows(0)("WebDescription"))
                    ViewState("TermsAndConditionID") = Convert.ToInt32(dt.Rows(0)("ID"))
                Else
                    divTermsCondition.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub GetPrefferedCurrency()
            Try
                Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("CurrencyTypeID") = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                    lblCurrency.Text = ViewState("CurrencyTypeID")
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Function GetPrice(ByVal lProductID As Long) As String
            Try
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity

                'Here get the Top 1 Person ID whose MemberTypeID = 1 
                oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                oOrder.ShipToID = User1.PersonID
                oOrder.BillToID = User1.PersonID
                oOrder.AddProduct(lProductID, 1)


                If oOrder.SubTypes("OrderLines").Count > 0 Then
                    Dim dPrice As Double
                    Dim dtCTCOrderSummery As New DataTable
                    dtCTCOrderSummery.Columns.Add("Product")
                    dtCTCOrderSummery.Columns.Add("Qty", GetType(Integer))
                    dtCTCOrderSummery.Columns.Add("Price")


                    For Each oOL As OrderLinesEntity In oOrder.SubTypes("OrderLines")
                        'If ViewState("CTCEligibleToJoin") <> "" Then
                        '    oOL.SubscriptionStartDate = ViewState("CTCEligibleToJoin")
                        'End If

                        Dim dExtendedPrice As Decimal
                        If Convert.ToBoolean(oOL.GetValue("TaxIncludedInPrice")) = True AndAlso oOL.SubTypes("OrderLineTaxes").Count > 0 Then
                            dPrice = dPrice + oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount")
                            dExtendedPrice = oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount")
                        Else
                            dPrice = dPrice + oOL.Extended
                            dExtendedPrice = oOL.Extended
                        End If
                        Dim drCTCOrderSummery As DataRow = dtCTCOrderSummery.NewRow()
                        drCTCOrderSummery("Product") = oOL.Product
                        drCTCOrderSummery("Qty") = oOL.Quantity
                        drCTCOrderSummery("Price") = dExtendedPrice
                        dtCTCOrderSummery.Rows.Add(drCTCOrderSummery)
                    Next
                    radCTCDesignationSummery.DataSource = dtCTCOrderSummery
                    radCTCDesignationSummery.DataBind()
                    Return dPrice + Convert.ToDouble(oOrder.GetValue("CALC_SalesTax"))
                Else

                    Return "0"
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return "0"
            End Try
        End Function

        ''' <summary>
        ''' if CTC APP Record already exists then Payment Control not display
        ''' </summary>
        ''' <remarks></remarks>
        ''' 
        Private Sub ExistCTCAPPRecord()
            Try
                Dim SSql As String = Database & "..spGetExistsCTSAppByStudent__c @StudentID=" & User1.PersonID
                Dim dtCTCAppDetail As DataTable = DataAction.GetDataTable(SSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtCTCAppDetail Is Nothing AndAlso dtCTCAppDetail.Rows.Count > 0 Then
                    CreditCard.Visible = False
                    ViewState("CTCAppID") = dtCTCAppDetail.Rows(0)("ID")
                    lblStatus.Text = Convert.ToString(dtCTCAppDetail.Rows(0)("Status"))
                    chkProfessionalBody.Checked = Convert.ToBoolean(dtCTCAppDetail.Rows(0)("IsRecognizedProfessionalBody"))
                    'txtComments.Text = Convert.ToString(dtCTCAppDetail.Rows(0)("Comments"))
                Else
                    ViewState("CTCAppID") = 0
                    CreditCard.Visible = True
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region

#Region "Button Click"
        ''' <summary>
        ''' Create CTC Designation record
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                ' On submission an create CTC Designation record  
                Dim oGE As AptifyGenericEntityBase
                If Convert.ToInt32(ViewState("CTCAppID")) > 0 Then
                    oGE = AptifyApplication.GetEntityObject("CTCDesignationApps__c", ViewState("CTCAppID"))
                Else
                    oGE = AptifyApplication.GetEntityObject("CTCDesignationApps__c", -1)
                End If
                Dim lorderID As Integer = 0
                With oGE
                    .SetValue("StudentID", User1.PersonID)
                    .SetValue("CTCDesignation", lblRequest.Text.Trim)
                    .SetValue("SubmissionDate", New Date(Today.Year, Today.Month, Today.Day))
                    .SetValue("StatusID", AptifyApplication.GetEntityRecordIDFromRecordName("MembershipApplicationStatus__c", "Submitted"))
                    .SetValue("IsRecognizedProfessionalBody", chkProfessionalBody.Checked)
                    '.SetValue("Comments", txtComments.Text.Trim)
                    If ViewState("CTCAppID") <= 0 Then

                        .SetValue("OrderID", CreateOrder(lorderID))
                    End If
                    Dim sError As String = String.Empty
                    If lorderID > -1 Then
                        If .Save(False, sError) Then
                            If ViewState("CTCAppID") > 0 Then
                                lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.CTCDesignation.UpdateMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            Else
                                lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.CTCDesignation.SuccessMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            End If
                            radWindow.VisibleOnPageLoad = True

                        End If
                    End If
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' popup ok button close popup and redirect home page
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' 
        Protected Sub btnOK_Click(sender As Object, e As System.EventArgs) Handles btnOK.Click
            Try
                radWindow.VisibleOnPageLoad = False
                lblMsg.Text = ""
                Response.Redirect(RedirectURL, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Close Terms and Condition popup
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnAcceptTerms_Click(sender As Object, e As System.EventArgs) Handles btnAcceptTerms.Click
            Try
                radAcceptTerms.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' CLick on cancel should redirect to Home Page
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' 
        Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
            Try
                Response.Redirect(RedirectURL, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' While creating new CTC designation record then will create order 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        Private Function CreateOrder(ByRef lorderID As Long) As Long
            Try
                'Dim lOrderID As Long = 0
                Dim dTaxAmount As Decimal = 0
                Dim dShippingCharges As Decimal = 0
                Dim dHandlingCharges As Decimal = 0
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                oOrder = AptifyApplication.GetEntityObject("Orders", -1)

                oOrder.ShipToID = User1.PersonID
                oOrder.BillToID = User1.PersonID
                oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
                oOrder.SetValue("BillToSameAsShipTo", 1)
                oOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
                Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If iFirmAdmin > 0 Then
                    oOrder.SetValue("FirmPay__c", 1)
                End If
                oOrder.AddProduct(ViewState("ProductID"), 1)
                oOrder.SetValue("PayTypeID", CreditCard.PaymentTypeID)
                oOrder.SetValue("CCAccountNumber", CreditCard.CCNumber)
                oOrder.SetValue("CCExpireDate", CreditCard.CCExpireDate)
                If CreditCard.CCNumber = "-Ref Transaction-" Then
                    oOrder.SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                    oOrder.SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                End If
                If Len(CreditCard.CCSecurityNumber) > 0 Then
                    oOrder.SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber) 'Neha changes for Issue 16675, 06/05/2013,Added CCSecurityNumber as a temperory field for not storing in record history.
                End If
                If oOrder.Fields("PaymentInformationID").EmbeddedObjectExists Then
                    Dim oOrderPayInfo As PaymentInformation = DirectCast(oOrder.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
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
                oOrder.SetAddValue("_xBypassCreditCheck", 1) 'bypass credit check
                dTaxAmount = Convert.ToDecimal(oOrder.GetValue("CALC_SalesTax"))
                dShippingCharges = Convert.ToDecimal(oOrder.GetValue("CALC_ShippingCharge"))
                dHandlingCharges = Convert.ToDecimal(oOrder.GetValue("CALC_HandlingCharge"))
                Dim sError As String = String.Empty
                If oOrder.Save(False, sError) Then
                    lOrderID = oOrder.RecordID
                    'PostPayment(lOrderID, dTaxAmount, dShippingCharges, dHandlingCharges)
                Else
                    lblErrorMsg.Text = sError + "Credit Card Verification Failed"
                    lblErrorMsg.ForeColor = Drawing.Color.Red
                    lblErrorMsg.Style.Add("font-size", "medium")
                    lblErrorMsg.Visible = True
                    lOrderID = -1
                End If
                Return lOrderID
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return -1
            End Try
        End Function

        ''' <summary>
        ''' Code Added By Govind  on 22 May 2015 For Order Line wise Payment
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <param name="TaxAmount"></param>
        ''' <remarks></remarks>
        ''' 
        Private Sub PostPayment(ByVal OrderID As Long, ByVal TaxAmount As Decimal, ByVal ShippingCharges As Decimal, ByVal HandlingCharge As Decimal)
            Try
                Dim oPayment As AptifyGenericEntityBase

                Dim bFlag As Boolean = False
                Dim bNotification As Boolean = False
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity = Nothing
                Dim sql As String = "spGetPaymentLevelID__c"
                Dim PaymentLevelID As Integer
                PaymentLevelID = Convert.ToInt16(DataAction.ExecuteScalar(sql, IAptifyDataAction.DSLCacheSetting.BypassCache))

                oPayment = AptifyApplication.GetEntityObject("Payments", -1)
                With oPayment
                    .SetValue("PaymentLevelID", PaymentLevelID)
                    .SetValue("EmployeeID", EBusinessGlobal.WebEmployeeID(Page.Application))
                    .SetValue("PersonID", User1.PersonID)
                    .SetValue("PaymentDate", Date.Today)
                    .SetValue("DepositDate", Date.Today)
                    .SetValue("EffectiveDate", Date.Today)
                    .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                    If CreditCard.CCNumber = "-Ref Transaction-" Then
                        .SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                        .SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                    End If
                    .SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber)
                    Dim sSql As String = Database & "..spGetOrderDetailsForPayments__c @OrderID=" & OrderID
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    Dim bIsFirmPay As Boolean = False
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        bIsFirmPay = Convert.ToBoolean(dt.Rows(0)("FirmPay__c"))
                        For Each dr As DataRow In dt.Rows
                            If Convert.ToDecimal(dr("Amount")) > 0 Then
                                With .SubTypes("PaymentLines").Add()
                                    .SetValue("AppliesTo", "Order Line")
                                    .SetValue("OrderID", dr("OrderID"))
                                    .SetValue("OrderDetailID", dr("OrderDetailID"))
                                    .SetValue("LineNumber", dr("Sequence"))
                                    .SetValue("Amount", dr("Amount"))
                                    .SetValue("BillToPersonID", User1.PersonID)
                                End With
                            End If
                        Next
                        ' For Tax Payment 
                        If TaxAmount > 0 Then
                            With .SubTypes("PaymentLines").Add()
                                .SetValue("AppliesTo", "Tax")
                                .SetValue("OrderID", OrderID)
                                .SetValue("Amount", TaxAmount)
                                .SetValue("BillToPersonID", User1.PersonID)
                            End With
                        End If

                        ' For Shipping Charges
                        If ShippingCharges > 0 Then
                            With .SubTypes("PaymentLines").Add()
                                .SetValue("AppliesTo", "Shipping")
                                .SetValue("OrderID", OrderID)
                                .SetValue("Amount", ShippingCharges)
                                .SetValue("BillToPersonID", User1.PersonID)
                            End With
                        End If
                        ' For Handling Charges
                        If HandlingCharge > 0 Then
                            With .SubTypes("PaymentLines").Add()
                                .SetValue("AppliesTo", "HandlingCharge")
                                .SetValue("OrderID", OrderID)
                                .SetValue("Amount", ShippingCharges)
                                .SetValue("BillToPersonID", User1.PersonID)
                            End With
                        End If
                    End If
                    'Siddharth: Added to check if Firm is Paying for firm admin order cc payment fail issue.
                    If bIsFirmPay Then
                        .SetValue("CompanyID", User1.CompanyID)
                    End If
                    .SetValue("CCAccountNumber", CreditCard.CCNumber)
                    .SetValue("CCExpireDate", CreditCard.CCExpireDate)
                    If .Fields("PaymentInformationID").EmbeddedObjectExists Then
                        Dim payInformation As PaymentInformation = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                        payInformation.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                        payInformation.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                        payInformation.SetValue("CCPartial", CreditCard.CCPartial)
                    End If
                    Dim errorMessage As String = String.Empty
                    If Not .Save(False, errorMessage) Then
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New Exception(errorMessage))
                    End If
                End With

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

    End Class
End Namespace
