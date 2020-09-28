'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                 13/10/2015                         create LLL enrollment application for member and student record
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
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
    Partial Class LLLEnrollmentApplication__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "LLLEnrollmentApplication__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Protected Const ATTRIBUTE_CONTROL_ReturnToURL_NAME As String = "ReturnToURL"
        Protected Const ATTRIBUTE_CONTROL_ViewcartPage_NAME As String = "ViewcartPage"
        Protected Const ATTRIBUTE_SECURITYERROR_PAGE As String = "securityErrorPage"
        Protected Const ATTRIBUTE_CONTROL_ChangeAddressPage_NAME As String = "ChangeAddress"
        Protected Const ATTRIBUTE_CONTORL_LoginPage_NAME As String = "LoginPage"
        Dim ClassID As Integer
        Dim iVenueID As Integer
        Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
        Dim oOL As Aptify.Applications.OrderEntry.OrderLinesEntity
        Dim dTaxAmount As Double
        Dim sPrice As String
#Region "Page Load"
        Public Overloads Property LoginPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTORL_LoginPage_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTORL_LoginPage_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTORL_LoginPage_NAME) = Me.FixLinkForVirtualPath(value)
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
        Public Overloads Property ReturnToURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_ReturnToURL_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_ReturnToURL_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_ReturnToURL_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Overloads Property ChangeAddressURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_ChangeAddressPage_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_ChangeAddressPage_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_ChangeAddressPage_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
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
        Public Overloads Property RedirectViewCartURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_ViewcartPage_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_ViewcartPage_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_ViewcartPage_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            SetProperties()
            If User1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            End If
            chkTermsAndConditions.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.TermsCondition__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblAccounatcyBodytxt.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.AccountancyBodyMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            If Not IsPostBack Then
                'ClassID = Convert.ToInt32(Request.QueryString("cid"))
                ' Get Class ID from Query String
                ClassID = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("cid")))
                ViewState("ClassID") = ClassID
                GetPrefferedCurrency()
                CreditCard.LoadCreditCardInfo()
                NewCompanyAddress()
                LoadRecord()
                autoCompanyName.ContextKey = CStr(User1.CompanyID)
                raUploadDocs.AllowAdd = True
                raUploadDocs.AllowDelete = True
                raUploadDocs.AllowView = True
                raUploadDocs.ValidationGroup = "VGGUploadbtn"
                raUploadDocs.EntityID = AptifyApplication.GetEntityID("LLLEnrolmentTracking__c")
                raUploadDocs.CategoryID = AptifyApplication.GetEntityRecordIDFromRecordName(AptifyApplication.GetEntityID("Attachment Categories"), "Enrolment Tracking")
                LoadClassDetails(ClassID)
                LoadCompanyAddress()
                CreateOrder()
                LoadPaymentSumary()
                ' if user clicks on Change address button and page load then set below values
                If Convert.ToInt32(Request.QueryString("CA")) = 1 Then
                    cmbWhoPays.SelectedValue = 3
                    LoadDataAsPerWhoPays()
                    hdnCompanyId.Value = Session("CACompanyID")
                    txtFirmName.Text = Session("CACompany")
                    hdnAwardingBody.Value = Session("CAAccountanyBodyID")
                    txtAccountancyBody.Text = Session("CAAccountancyBody")
                End If
            End If
        End Sub
        Protected Overrides Sub SetProperties()
            Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                Me.LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTORL_LoginPage_NAME)
            End If
            If String.IsNullOrEmpty(RedirectURL) Then
                Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(ReturnToURL) Then
                Me.ReturnToURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_ReturnToURL_NAME)
            End If
            If String.IsNullOrEmpty(RedirectViewCartURL) Then
                Me.RedirectViewCartURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_ViewcartPage_NAME)
            End If
            If String.IsNullOrEmpty(ChangeAddressURL) Then
                Me.ChangeAddressURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_ChangeAddressPage_NAME)
            End If
            '
        End Sub
        Private Sub GetPrefferedCurrency()
            Try
                Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("CurrencySymbol") = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                    lblCurrency.Text = ViewState("CurrencySymbol")
                End If


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadRecord()
            Try
                ' If Person Has Member or Student
                Dim sSql As String
                Dim dt As DataTable
                sSql = Database & "..spLLLEnrolmentPersonDetails__c @PersonID=" & User1.PersonID
                dt = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    If ((Convert.ToBoolean(dt.Rows(0)("IsMember")) AndAlso Convert.ToInt32(dt.Rows(0)("status")) = 1) AndAlso Convert.ToString(dt.Rows(0)("Status__c")).Trim.ToLower = "approved") Or (Convert.ToString(dt.Rows(0)("MemberType")).Trim.ToLower = "student" AndAlso Convert.ToString(dt.Rows(0)("Status__c")).Trim.ToLower = "approved") Then
                        txtApplicantName.Text = Convert.ToString(dt.Rows(0)("FirstLast"))
                        txtMemberType.Text = Convert.ToString(dt.Rows(0)("MemberType"))
                        ViewState("MemberTypeID") = Convert.ToInt32(dt.Rows(0)("MemberTypeID"))
                        txtFirmName.Text = Convert.ToString(dt.Rows(0)("Company"))
                        hdnCompanyId.Value = Convert.ToString(Convert.ToInt32(dt.Rows(0)("FirmID")))
                        txtAccountancyBody.Text = "Chartered Accountants Ireland"
                        txtAccountancyBody.Enabled = False
                        divFirstName.Visible = False
                        divLastName.Visible = False
                        divApplicantName.Visible = True
                        divAccBodytxtID.Visible = False
                        divAccountancyBody.Visible = False

                        'redmine 17907
                    ElseIf Convert.ToBoolean(dt.Rows(0)("IsMember")) Then
                        divUpload.Visible = False
                    Else
                        txtApplicantName.Text = Convert.ToString(dt.Rows(0)("FirstLast"))
                        txtFirstName.Text = Convert.ToString(dt.Rows(0)("FirstName"))
                        txtMiddleName.Text = Convert.ToString(dt.Rows(0)("MiddleName"))
                        txtLastName.Text = Convert.ToString(dt.Rows(0)("LastName"))
                        txtMemberType.Text = Convert.ToString(dt.Rows(0)("MemberType"))
                        ViewState("MemberTypeID") = Convert.ToInt32(dt.Rows(0)("MemberTypeID"))
                        txtFirmName.Text = Convert.ToString(dt.Rows(0)("Company"))
                        hdnCompanyId.Value = Convert.ToString(Convert.ToInt32(dt.Rows(0)("FirmID")))
                        ' ViewState("FirmID") = Convert.ToInt32(dt.Rows(0)("FirmID"))

                        divFirstName.Visible = True
                        divLastName.Visible = True
                        divApplicantName.Visible = False
                        txtFirmName.Enabled = True
                        txtAccountancyBody.Enabled = True
                        divAccBodytxtID.Visible = True
                        divUpload.Visible = True
                        'If Non-Members have an approved Enrollment Request application in the system, they will follow the members enrollment process for all future enrolments on LLL qualifications 
                        If EnrolmentStatusApproved() Then
                            divFirstName.Visible = False
                            divLastName.Visible = False
                            divApplicantName.Visible = True
                            txtFirmName.Enabled = False
                            txtAccountancyBody.Enabled = False
                            txtAccountancyBody.Text = "Chartered Accountants Ireland"
                            divAccBodytxtID.Visible = False
                            divUpload.Visible = False
                        End If
                    End If
                End If

                If hdnCompanyId.Value > 0 Then
                    cmbWhoPays.Enabled = True
                Else
                    cmbWhoPays.SelectedValue = 2
                    cmbWhoPays.Enabled = False
                    LoadDataAsPerWhoPays()
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadClassDetails(ByVal ClassID As Integer)
            Try
                Dim sSql As String = Database & "..spGetLLLClassDetails__c @ClassID=" & ClassID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    txtApplicationType.Text = Convert.ToString(dt.Rows(0)("Course"))
                    txtClass.Text = Convert.ToString(dt.Rows(0)("Class"))
                    ViewState("ProductID") = Convert.ToInt32(dt.Rows(0)("ProductID"))
                    ViewState("CourseID") = Convert.ToInt32(dt.Rows(0)("CourseID"))
                    ViewState("AcademicCycleID") = Convert.ToInt32(dt.Rows(0)("AcademicCycleID"))
                    ViewState("CourseCatagoryID") = Convert.ToInt32(dt.Rows(0)("CourseCatagoryID"))

                    TermsAndCondition()
                    ViewState("EnrolType") = Convert.ToString(dt.Rows(0)("EnrolType"))
                    If Convert.ToString(ViewState("EnrolType")).Trim.ToLower = "ctc integrated tax" Then
                        ChkStageCourses()
                    End If
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
                    'lblWebDesc.Text = Convert.ToString(dt.Rows(0)("WebDescription"))
                    ViewState("TermsAndConditionID") = Convert.ToInt32(dt.Rows(0)("ID"))
                Else
                    divTermsCondition.Visible = False
                    ViewState("TermsAndConditionID") = 0
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub NewCompanyAddress()
            Try
                LoadCombos()
                SetComboValue(cmbCountry, "Ireland")
                PopulateStateAsCountry(cmbState, cmbCountry)
                SetComboValue(cmbState, "")
            Catch ex As Exception

            End Try
        End Sub
        Private Sub LoadCombos()
            Dim sSQL As String
            Try
                sSQL = Database & "..spGetCountryList"
                cmbCountry.DataSource = DataAction.GetDataTable(sSQL)
                cmbCountry.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        '11/27/07,Added by Tamasa,Issue 5222.
        Private Sub PopulateState()
            Try
                Dim sSQL As String
                sSQL = Database & "..spGetStateList @CountryID=" & cmbCountry.SelectedValue.ToString
                cmbState.DataSource = DataAction.GetDataTable(sSQL)
                cmbState.DataTextField = "State"
                cmbState.DataValueField = "State"
                cmbState.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        '11/27/07,Added by Tamasa,Issue 5222.
        Protected Sub cmbCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCountry.SelectedIndexChanged
            PopulateState()
        End Sub
        Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList, _
                                ByVal sValue As String)
            Dim i As Integer

            Try
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
        Private Sub PopulateStateAsCountry(ByRef cmbState As DropDownList, ByRef cmbCurrentCountry As DropDownList)
            Try
                Dim sSQL As String
                sSQL = Database & "..spGetStateList @CountryID=" & cmbCurrentCountry.SelectedValue.ToString
                cmbState.DataSource = DataAction.GetDataTable(sSQL)
                cmbState.DataTextField = "State"
                cmbState.DataValueField = "State"
                cmbState.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' If User get CTC Stage 2 Coursethen check below validation
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ChkStageCourses()
            Try
                Dim sSql As String = Database & "..spChkStudentCTCStageCertificate__c @StudentID=" & User1.PersonID
                Dim bCTCCertificate As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If bCTCCertificate = False Then
                    lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.CTCCertificationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radWindow.VisibleOnPageLoad = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub DisableFields()
            Try
                txtFirmName.Enabled = False
                txtAccountancyBody.Enabled = False
                cmbWhoPays.Enabled = False
                txtComments.Enabled = False
                btnSave.Visible = False
                chkTermsAndConditions.Enabled = False
                txtPONumber.Enabled = False
                btnCheckOut.Enabled = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Function EnrolmentStatusApproved() As Boolean
            Try
                Dim sSql As String = Database & "..spCheckLLLEnrolmentApproved__c @PersonID=" & User1.PersonID & ",@ClassID=" & ViewState("ClassID")
                Dim bApplicationApproved As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If bApplicationApproved Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
        Private Sub CreateOrder()
            Try
                Dim bNewProduct As Boolean = True

                If Convert.ToInt32(Request.QueryString("CA")) = 1 Then
                    'redmine 17373
                    'oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                    oOrder = AptifyApplication.GetEntityObject("Orders", -1)
                    oOrder.SetValue("ShipToID", User1.PersonID)
                    oOrder.SetValue("BillToID", User1.PersonID)
                    If Convert.ToInt32(Request.QueryString("CA")) = 1 Then
                        With oOrder
                            lblAddressLine1.Text = CStr(.GetValue("ShipToAddrLine1"))
                            lblAddressLine2.Text = CStr(.GetValue("ShipToAddrLine2"))
                            lblAddressLine3.Text = CStr(.GetValue("ShipToAddrLine3"))
                            lblCity.Text = CStr(.GetValue("ShipToCity"))
                            lblState.Text = CStr(.GetValue("ShipToState"))
                            lblZipCode.Text = CStr(.GetValue("ShipToZipCode"))
                            lblCountry.Text = CStr(.GetValue("ShipToCountry"))
                            AddressLoaded()
                        End With
                    End If
                    If oOrder.SubTypes("OrderLines").Count > 0 Then
                        oOL = TryCast(oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1), OrderLinesEntity)
                        With oOL
                            .ExtendedOrderDetailEntity.SetValue("ClassID", ViewState("ClassID"))
                            .ExtendedOrderDetailEntity.SetValue("Status", "Pending")
                            .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                        End With
                        Dim dPaymentAmount As Decimal
                        'oOL = TryCast(oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1), OrderLinesEntity)

                        'With oOL
                        '    .ExtendedOrderDetailEntity.SetValue("ClassID", ViewState("ClassID"))
                        '    .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                        '    .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                        'End With

                        'ShoppingCart1.SaveCart(Session)
                        If Convert.ToBoolean(oOL.GetValue("TaxIncludedInPrice")) = True AndAlso oOL.SubTypes("OrderLineTaxes").Count > 0 Then
                            dPaymentAmount = Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount"))
                        Else
                            dPaymentAmount = oOL.Extended
                        End If

                        dTaxAmount = Convert.ToDouble(oOrder.GetValue("CALC_SalesTax"))

                        If dTaxAmount > 0 Then
                            lblTaxAmount.Visible = True
                            lblTax.Visible = True
                            ViewState("dTaxamt") = Convert.ToDecimal(ViewState("dTaxamt")) + dTaxAmount
                            lblTaxAmount.Text = ViewState("CurrencySymbol") & Format(Convert.ToDecimal(ViewState("dTaxamt")), "0.00")
                        End If
                        sPrice = ViewState("CurrencySymbol") & Format(Convert.ToDecimal(dPaymentAmount), "0.00")
                    End If
                    'ShoppingCart1.SaveCart(Session)
                Else
                    'redmine 17373
                    oOrder = AptifyApplication.GetEntityObject("Orders", -1)
                    oOrder.SetValue("ShipToID", User1.PersonID)
                    oOrder.SetValue("BillToID", User1.PersonID)

                    'oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                    'If oOrder.SubTypes("OrderLines").Count > 0 Then
                    '    For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                    '        ShoppingCart1.RemoveItem(oOrder, i)
                    '    Next

                    '    ShoppingCart1.SaveCart()
                    'End If

                    'If ShoppingCart1.AddToCart(ViewState("ProductID"), 1) Then
                    If oOrder.AddProduct(ViewState("ProductID")).Count > 0 Then
                        Dim dPaymentAmount As Decimal
                        oOL = TryCast(oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1), OrderLinesEntity)

                        With oOL
                            .ExtendedOrderDetailEntity.SetValue("ClassID", ViewState("ClassID"))
                            .ExtendedOrderDetailEntity.SetValue("Status", "Pending")
                            .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                        End With
                        'ShoppingCart1.SaveCart(Session)
                        If Convert.ToBoolean(oOL.GetValue("TaxIncludedInPrice")) = True AndAlso oOL.SubTypes("OrderLineTaxes").Count > 0 Then
                            dPaymentAmount = Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount"))
                        Else
                            dPaymentAmount = oOL.Extended
                        End If

                        dTaxAmount = Convert.ToDouble(oOrder.GetValue("CALC_SalesTax"))

                        If dTaxAmount > 0 Then
                            lblTaxAmount.Visible = True
                            lblTax.Visible = True
                            ViewState("dTaxamt") = Convert.ToDecimal(ViewState("dTaxamt")) + dTaxAmount
                            lblTaxAmount.Text = ViewState("CurrencySymbol") & Format(Convert.ToDecimal(ViewState("dTaxamt")), "0.00")
                        End If
                        sPrice = ViewState("CurrencySymbol") & Format(Convert.ToDecimal(dPaymentAmount), "0.00")

                    End If

                End If

                Session("OrderGE") = oOrder



            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadPaymentSumary()
            Try
                lblAmount.Visible = True
                lblTotalAmount.Visible = True
                Dim dAmt As Decimal = 0
                lblStudentPaidLabel.Visible = True
                lblAmountPaidStudent.Visible = True
                lblCurrency.Visible = True
                lblIntialAmount.Visible = True
                lblCurrency.Visible = True
                lblIntialAmt.Visible = True

                lblTaxAmount.Visible = True
                lblTax.Visible = True
                Dim dAmountPaidByStudent As Decimal = 0
                Dim dAmountPaidByFirm As Decimal = 0
                Dim dPaymentPlanAmount As Decimal = 0
                Dim dTaxAmount As Decimal = 0
                If dAmt = 0 Then
                    dAmt = Convert.ToDecimal(sPrice.Substring(1, sPrice.Length - 1))
                    dAmountPaidByStudent = Convert.ToDecimal(sPrice.Substring(1, sPrice.Length - 1))
                End If
                Dim sProductID As String = String.Empty
                Dim sProductPlanSql As String = Database & "..spCheckProductPaymentPlans__c @ProductID=" & Convert.ToInt32(ViewState("ProductID"))
                Dim iProductPaymentPlan As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sProductPlanSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If iProductPaymentPlan > 0 Then
                    sProductID = ViewState("ProductID")
                    dPaymentPlanAmount = Convert.ToDecimal(sPrice.Substring(1, sPrice.Length - 1))
                End If
                lblStagePaymentTotal.Text = ViewState("CurrencySymbol") & Format(Convert.ToDecimal(dPaymentPlanAmount), "0.00")

                lblTotalAmount.Text = ViewState("CurrencySymbol") & Format(Convert.ToDecimal(dAmt), "0.00")
                lblAmountPaidStudent.Text = ViewState("CurrencySymbol") & Format(Convert.ToDecimal(dAmountPaidByStudent), "0.00")
                lblAmountPaidFirm.Text = ViewState("CurrencySymbol") & Format(Convert.ToDecimal(dAmountPaidByFirm), "0.00")
                Dim dTotalAmtPaidWithTax As Decimal = (dAmountPaidByStudent + dTaxAmount)
                lblIntialAmount.Text = Format(dTotalAmtPaidWithTax, "0.00")

                lblTaxAmount.Text = ViewState("CurrencySymbol") & Format(Convert.ToDecimal(dTaxAmount), "0.00")
                CreditCard.Visible = True
                If sProductID <> "" Then
                    LoadPaymentPlan(sProductID)
                    ddlPaymentPlan.Visible = True
                    lblPaymentPlan.Visible = True
                Else
                    ddlPaymentPlan.Visible = False
                    lblPaymentPlan.Visible = False
                End If
                If dAmountPaidByStudent > 0 Then
                    CreditCard.Visible = True
                Else
                    CreditCard.Visible = False
                    ddlPaymentPlan.Visible = False
                    lblPaymentPlan.Visible = False

                    lblAmount.Visible = False
                    lblTotalAmount.Visible = False
                    lblStagePaymentTotal.Visible = False
                    lblTax.Visible = False
                    lblTaxAmount.Visible = False
                    lblIntialAmt.Visible = False
                    lblCurrency.Visible = False
                    lblIntialAmount.Visible = False
                    lblPaymentSummery.Visible = False
                    divPaySummary.Visible = False
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
                Dim sSql As String = Database & "..spGetPaymentPlanForStudentEnrollment__c @ProductIds='" & sProduct & "'"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ddlPaymentPlan.DataSource = dt
                    ddlPaymentPlan.DataTextField = "Name"
                    ddlPaymentPlan.DataValueField = "ID"
                    ddlPaymentPlan.DataBind()
                End If
                ddlPaymentPlan.Items.Insert(0, "Select payment plan")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Public Function SetStagePaymentAmount(ByVal Percentage As Decimal, ByVal days As Integer) As String
            Try
                Dim dStageAmt As Decimal = Convert.ToDecimal(Convert.ToString(lblStagePaymentTotal.Text).Substring(1, Convert.ToString(lblStagePaymentTotal.Text).Length - 1))
                Dim dStudentPayAmt As Decimal = Convert.ToDecimal(Convert.ToString(lblTotalAmount.Text).Substring(1, Convert.ToString(lblTotalAmount.Text).Length - 1))
                Dim dPercentageAmt As Decimal = (dStageAmt * Percentage) / 100
                If days = 0 Then
                    Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                    lblIntialAmount.Text = Format(Convert.ToDecimal(dintialStageAmount + dPercentageAmt), "0.00")
                Else
                    Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                    lblIntialAmount.Text = Format(Convert.ToDecimal(dintialStageAmount), "0.00")
                End If
                Return ViewState("CurrencySymbol") & Format(Convert.ToDecimal(dPercentageAmt), "0.00")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return ""
            End Try
        End Function
        ''' <summary>
        ''' if company billing address then display billing address or display street address
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadCompanyAddress()
            Try
                Dim sSqlCompanyID As String = "..spCompanyIDForPerson @PersonID=" & User1.PersonID
                Dim lCompanyID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlCompanyID, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lCompanyID > 0 Then
                    CompanyAddress(lCompanyID)
                End If


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub CompanyAddress(ByVal CompanyID As Integer)
            Try
                Dim sSQl As String = Database & "..spGetCompanyBillingAddress__c @CompanyID=" & CompanyID
                Dim dt As DataTable = DataAction.GetDataTable(sSQl, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    txtAddressLine1.Text = Convert.ToString(dt.Rows(0)("Line1"))
                    txtAddressLine2.Text = Convert.ToString(dt.Rows(0)("Line2"))
                    txtAddressLine3.Text = Convert.ToString(dt.Rows(0)("Line3"))
                    txtCity.Text = Convert.ToString(dt.Rows(0)("City"))
                    txtZipCode.Text = Convert.ToString(dt.Rows(0)("PostalCode"))
                    LoadCombos()
                    SetComboValue(cmbCountry, Convert.ToString(dt.Rows(0)("Country")))
                    PopulateStateAsCountry(cmbState, cmbCountry)
                    SetComboValue(cmbState, Convert.ToString(dt.Rows(0)("StateProvince")))

                    ''lblAddressLine1.Text = Convert.ToString(dt.Rows(0)("Line1"))
                    ''lblAddressLine2.Text = Convert.ToString(dt.Rows(0)("Line2"))
                    ''lblAddressLine3.Text = Convert.ToString(dt.Rows(0)("Line3"))
                    ''lblCity.Text = Convert.ToString(dt.Rows(0)("City"))
                    ''lblState.Text = Convert.ToString(dt.Rows(0)("StateProvince"))
                    ''lblZipCode.Text = Convert.ToString(dt.Rows(0)("PostalCode"))
                    ''lblCountry.Text = Convert.ToString(dt.Rows(0)("Country"))
                    AddressLoaded()
                Else
                    AddressLoaded()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub AddressLoaded()
            If lblAddressLine1.Text = "" Then
                dvadd1.Visible = False
            Else
                dvadd1.Visible = True
            End If
            If lblAddressLine2.Text = "" Then
                dvadd2.Visible = False
            Else
                dvadd2.Visible = True
            End If
            If lblAddressLine3.Text = "" Then
                dvadd3.Visible = False
            Else
                dvadd3.Visible = True
            End If
            If lblCity.Text = "" AndAlso lblState.Text = "" AndAlso lblZipCode.Text = "" Then
                dvCityStateZip.Visible = False
            Else
                dvCityStateZip.Visible = True
            End If

            If lblCountry.Text = "" Then
                dvCountry.Visible = False
            Else
                dvCountry.Visible = True
            End If
        End Sub
#End Region

#Region "Drop-Down Events"
        Protected Sub cmbWhoPays_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbWhoPays.SelectedIndexChanged
            Try
                LoadDataAsPerWhoPays()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadDataAsPerWhoPays()
            Try
                If cmbWhoPays.SelectedItem.Text.ToLower.Trim <> "bill to firm" Then
                    divPO.Visible = False
                    btnSave.Visible = False
                    divPaySummary.Visible = True
                    divCreditCard.Visible = True
                    btnCheckOut.Visible = True
                    lblPaymentSummery.Visible = True
                    If cmbWhoPays.SelectedItem.Text.ToLower.Trim = "pay now with firm credit card" Then
                        If hdnCompanyId.Value > 0 Then
                            ''divCompanyAddressID.Visible = True
                            CompanyAddress(hdnCompanyId.Value)
                            divCompanyAddressID.Visible = False
                            divAdd1.Visible = True
                            divAdd2.Visible = True
                            divAdd3.Visible = True
                            divCity.Visible = True
                            divCountry.Visible = True
                        End If
                    Else
                        divCompanyAddressID.Visible = False
                        divAdd1.Visible = False
                        divAdd2.Visible = False
                        divAdd3.Visible = False
                        divCity.Visible = False
                        divCountry.Visible = False
                    End If
                    If Convert.ToDecimal(lblAmountPaidStudent.Text.Substring(1, lblAmountPaidStudent.Text.Length - 1)) > 0 Then
                        CreditCard.Visible = True

                        lblAmount.Visible = True
                        lblTotalAmount.Visible = True
                        lblStagePaymentTotal.Visible = False
                        lblTax.Visible = True
                        lblTaxAmount.Visible = True
                        lblIntialAmt.Visible = True
                        lblCurrency.Visible = True
                        lblIntialAmount.Visible = True
                        lblPaymentSummery.Visible = True
                        divPaySummary.Visible = True
                    Else
                        CreditCard.Visible = False
                        ddlPaymentPlan.Visible = False
                        lblPaymentPlan.Visible = False

                        lblAmount.Visible = False
                        lblTotalAmount.Visible = False
                        lblStagePaymentTotal.Visible = False
                        lblTax.Visible = False
                        lblTaxAmount.Visible = False
                        lblIntialAmt.Visible = False
                        lblCurrency.Visible = False
                        lblIntialAmount.Visible = False
                        lblPaymentSummery.Visible = False
                        divPaySummary.Visible = False
                    End If
                Else
                    divPO.Visible = True
                    btnSave.Visible = True
                    divPaySummary.Visible = False
                    divCreditCard.Visible = False
                    btnCheckOut.Visible = False
                    lblPaymentSummery.Visible = False
                    divCompanyAddressID.Visible = False

                    divAdd1.Visible = False
                    divAdd2.Visible = False
                    divAdd3.Visible = False
                    divCity.Visible = False
                    divCountry.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub ddlPaymentPlan_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPaymentPlan.SelectedIndexChanged
            Try
                Dim iPaymentPlan As Integer = 0
                If ddlPaymentPlan.SelectedValue <> "Select payment plan" Then
                    iPaymentPlan = ddlPaymentPlan.SelectedValue
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
                radPaymentPlanDetails.Visible = True
                radPaymentPlanDetails.DataSource = dt
                radPaymentPlanDetails.DataBind()
                ViewState("SelectedPaymentPlan") = dt
            Else
                radPaymentPlanDetails.Visible = False
                ViewState("SelectedPaymentPlan") = Nothing

            End If

            Dim dtPaymentPlan As DataTable = TryCast(ViewState("SelectedPaymentPlan"), DataTable)
            If Not dtPaymentPlan Is Nothing AndAlso dtPaymentPlan.Rows.Count > 0 Then
                For Each drP As DataRow In dtPaymentPlan.Rows
                    Dim dStageAmt As Decimal = Convert.ToDecimal(Convert.ToString(lblStagePaymentTotal.Text).Substring(1, Convert.ToString(lblStagePaymentTotal.Text).Length - 1))
                    Dim dStudentPayAmt As Decimal = Convert.ToDecimal(Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1))
                    Dim dPercentageAmt As Decimal = (dStageAmt * Convert.ToDecimal(drP("Percentage"))) / 100
                    If CInt(drP("days")) = 0 Then
                        Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                        lblIntialAmount.Text = Format(Convert.ToDecimal(dintialStageAmount + dPercentageAmt), "0.00")
                    Else
                        Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                        lblIntialAmount.Text = Format(Convert.ToDecimal(dintialStageAmount), "0.00")
                    End If
                    Exit For
                Next

                Dim taxAmt As Decimal = Convert.ToString(lblTaxAmount.Text).Substring(1, Convert.ToString(lblTaxAmount.Text).Length - 1).Trim
                lblIntialAmount.Text = Format((Convert.ToDecimal(lblIntialAmount.Text) + taxAmt), "0.00")
            Else
                Dim dAmtPaidByStudent As Decimal = Convert.ToDecimal(Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1).Trim)
                Dim taxAmt As Decimal = Convert.ToString(lblTaxAmount.Text).Substring(1, Convert.ToString(lblTaxAmount.Text).Length - 1).Trim
                lblIntialAmount.Text = dAmtPaidByStudent + taxAmt
            End If

            CreditCard.Visible = True

        End Sub
#End Region

#Region "Button Click"
        ''' <summary>
        ''' Create Enrollment Request Trakin grecord
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
            Try

                'Added by Prachi on 08/29/2018 to Check the Company Dupe check for Redmine #15496

                Dim iCompanyID As Long = 0
                If hdnCompanyId.Value <> "" AndAlso hdnCompanyId.Value <> "0" Then
                    iCompanyID = hdnCompanyId.Value

                End If
                If iCompanyID <= 0 Then
                    Dim oComapnyGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Companies", -1)
                    oComapnyGE.SetValue("Name", txtFirmName.Text.Trim)
                    Dim oAddress As AptifyGenericEntityBase
                    oAddress = oComapnyGE.Fields("AddressID").EmbeddedObject
                    oAddress.SetValue("Line1", txtAddressLine1.Text.Trim)

                    If Not oComapnyGE Is Nothing AndAlso oComapnyGE.IsDirty Then
                        Dim oCompanyDupeCheck As New Aptify.Consulting.Plugin.CompanyDupCheck__c.CompanyDupeCheck


                        Dim duplicateID() As Long = Nothing

                        If oCompanyDupeCheck.CheckForDuplicates(oComapnyGE, duplicateID) = DuplicateCheckResult.Exact Then
                            oComapnyGE = Nothing
                            'duplicateCompanyError.Style.Add("display", "block")
                            'iCompanyID = duplicateID(0)
                            lblCompanyDupeCheck.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterCorrectCompanyNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radCompanyDupeCheck.VisibleOnPageLoad = True

                        Else
                            If ViewState("TermsAndConditionID") > 0 Then
                                If chkTermsAndConditions.Checked = False Then
                                    lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.TermsConditionErrorMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                    radAcceptTerms.VisibleOnPageLoad = True
                                Else
                                    If cmbWhoPays.SelectedItem.Text.Trim.ToLower = "bill to firm" AndAlso txtFirmName.Text.Trim = "" Then
                                        lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterFirmNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                        radAcceptTerms.VisibleOnPageLoad = True


                                    ElseIf DoSave() Then
                                        lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.SuccessMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                        radWindow.VisibleOnPageLoad = True
                                    End If
                                End If
                            Else
                                If cmbWhoPays.SelectedItem.Text.Trim.ToLower = "bill to firm" AndAlso txtFirmName.Text.Trim = "" Then
                                    lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterFirmNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                    radWindow.VisibleOnPageLoad = True
                                ElseIf DoSave() Then
                                    lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.SuccessMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                    radWindow.VisibleOnPageLoad = True
                                End If
                            End If
                        End If
                    End If
                Else
                    If ViewState("TermsAndConditionID") > 0 Then
                        If chkTermsAndConditions.Checked = False Then
                            lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.TermsConditionErrorMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radAcceptTerms.VisibleOnPageLoad = True
                        Else
                            If cmbWhoPays.SelectedItem.Text.Trim.ToLower = "bill to firm" AndAlso txtFirmName.Text.Trim = "" Then
                                lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterFirmNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                radAcceptTerms.VisibleOnPageLoad = True


                            ElseIf DoSave() Then
                                lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.SuccessMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                radWindow.VisibleOnPageLoad = True
                            End If
                        End If
                    Else
                        If cmbWhoPays.SelectedItem.Text.Trim.ToLower = "bill to firm" AndAlso txtFirmName.Text.Trim = "" Then
                            lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterFirmNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radWindow.VisibleOnPageLoad = True
                        ElseIf DoSave() Then
                            lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.SuccessMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radWindow.VisibleOnPageLoad = True
                        End If
                    End If
                End If
                'End Redmine #15496
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Function DoSave() As Boolean
            Try
                '  System should generate/create an "Enrollment Request Tracking" record in smart client for CAI staff to process and approve
                Dim oGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("LLLEnrolmentTracking__c", -1)
                With oGE
                    .SetValue("ApplicantID", User1.PersonID)
                    .SetValue("FirmID", CreateNewCompany)
                    .SetValue("AccountancyBodyID", CreateAccounatncyBody)
                    .SetValue("ApplicationTypeID", ViewState("CourseID"))
                    .SetValue("ClassID", ViewState("ClassID"))
                    .SetValue("CourseCategoryID", ViewState("CourseCatagoryID"))
                    .SetValue("WhoPays", cmbWhoPays.SelectedItem.Text.Trim)
                    .SetValue("PONumber", txtPONumber.Text.Trim)
                    .SetValue("Comments", txtComments.Text.Trim)
                    If Convert.ToString(ViewState("EnrolType")).Trim.ToLower <> "ctc" Then
                        .SetValue("Status", "Submitted to CAI")
                    Else
                        .SetValue("Status", "Approved")
                    End If


                    If ViewState("TermsAndConditionID") > 0 Then
                        .SetValue("TermsAndConditionID", ViewState("TermsAndConditionID"))
                    End If
                    Dim sError As String = String.Empty
                    If .Save(False, sError) Then
                        SaveAttachment(.RecordID)
                    End If

                End With
                Return True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' popup ok button close popup and redirect home page
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnOK_Click(sender As Object, e As System.EventArgs) Handles btnOK.Click
            Try
                radWindow.VisibleOnPageLoad = False
                lblMsg.Text = ""
                Response.Redirect(RedirectURL, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub btnCheckOut_Click(sender As Object, e As System.EventArgs) Handles btnCheckOut.Click
            Try
                'Added by Prachi on 08/29/2018 to Check the Company Dupe check for Redmine #15496
                Dim iCompanyID As Long = 0
                If hdnCompanyId.Value <> "" AndAlso hdnCompanyId.Value <> "0" Then
                    iCompanyID = hdnCompanyId.Value

                End If
                If iCompanyID <= 0 Then
                    Dim oComapnyGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Companies", -1)
                    oComapnyGE.SetValue("Name", txtFirmName.Text.Trim)
                    Dim oAddress As AptifyGenericEntityBase
                    oAddress = oComapnyGE.Fields("AddressID").EmbeddedObject
                    oAddress.SetValue("Line1", txtAddressLine1.Text.Trim)

                    If Not oComapnyGE Is Nothing AndAlso oComapnyGE.IsDirty Then
                        Dim oCompanyDupeCheck As New Aptify.Consulting.Plugin.CompanyDupCheck__c.CompanyDupeCheck


                        Dim duplicateID() As Long = Nothing

                        If oCompanyDupeCheck.CheckForDuplicates(oComapnyGE, duplicateID) = DuplicateCheckResult.Exact Then
                            oComapnyGE = Nothing

                            lblCompanyDupeCheck.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterCorrectCompanyNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radCompanyDupeCheck.VisibleOnPageLoad = True
                        Else
                            If Convert.ToDecimal(lblTotalAmount.Text.Substring(1, lblTotalAmount.Text.Length - 1)) > 0 Then
                                If CreditCard.PaymentTypeID <= 0 Then
                                    lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.CreditCardErrorMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                    radAcceptTerms.VisibleOnPageLoad = True
                                ElseIf cmbWhoPays.SelectedValue = 3 Then
                                    If Convert.ToInt32(Request.QueryString("CA")) = 1 Then
                                        If txtFirmName.Text.Trim = "" Then
                                            lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterFirmNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                            radAcceptTerms.VisibleOnPageLoad = True
                                        Else
                                            DoSaveWithPayments()
                                        End If

                                    Else
                                        If cmbCountry.SelectedValue <= 0 AndAlso hdnCompanyId.Value > 0 Then
                                            lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.ChangeAddressErrorMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                            radAcceptTerms.VisibleOnPageLoad = True
                                        ElseIf txtFirmName.Text.Trim = "" Then
                                            lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterFirmNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                            radAcceptTerms.VisibleOnPageLoad = True
                                        Else
                                            DoSaveWithPayments()
                                        End If
                                    End If

                                Else
                                    DoSaveWithPayments()
                                End If
                            End If
                        End If
                    End If
                Else
                    If Convert.ToDecimal(lblTotalAmount.Text.Substring(1, lblTotalAmount.Text.Length - 1)) > 0 Then
                        If CreditCard.PaymentTypeID <= 0 Then
                            lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.CreditCardErrorMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radAcceptTerms.VisibleOnPageLoad = True
                        ElseIf cmbWhoPays.SelectedValue = 3 Then
                            If Convert.ToInt32(Request.QueryString("CA")) = 1 Then
                                If txtFirmName.Text.Trim = "" Then
                                    lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterFirmNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                    radAcceptTerms.VisibleOnPageLoad = True
                                Else
                                    DoSaveWithPayments()
                                End If

                            Else
                                If cmbCountry.SelectedValue <= 0 AndAlso hdnCompanyId.Value > 0 Then
                                    lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.ChangeAddressErrorMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                    radAcceptTerms.VisibleOnPageLoad = True
                                ElseIf txtFirmName.Text.Trim = "" Then
                                    lblAcceptTermsCondition.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterFirmNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                    radAcceptTerms.VisibleOnPageLoad = True
                                Else
                                    DoSaveWithPayments()
                                End If
                            End If

                        Else
                            DoSaveWithPayments()
                        End If
                    End If
                End If

                ' End Redmine #15496

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub DoSaveWithPayments()
            Try
                'redmine 17373
                Dim lOrderID As Long
                ' oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                oOrder = TryCast(Session("OrderGE"), Aptify.Applications.OrderEntry.OrdersEntity)
                If OrderCreation(lOrderID) > 0 Then
                    Dim oGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("LLLEnrolmentTracking__c", -1)
                    With oGE
                        .SetValue("ApplicantID", User1.PersonID)
                        .SetValue("FirmID", CreateNewCompany)
                        .SetValue("AccountancyBodyID", CreateAccounatncyBody)
                        .SetValue("ApplicationTypeID", ViewState("CourseID"))
                        .SetValue("ClassID", ViewState("ClassID"))
                        .SetValue("CourseCategoryID", ViewState("CourseCatagoryID"))
                        .SetValue("WhoPays", cmbWhoPays.SelectedItem.Text.Trim)
                        .SetValue("OrderID", lOrderID)
                        Dim sSql As String = Database & "..spGetClassRegistrationIDFromOrderID__c @OrderID=" & lOrderID
                        Dim iClassRegID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If iClassRegID > 0 Then
                            .SetValue("ClassRegistrationID", iClassRegID)
                        End If
                        .SetValue("Comments", txtComments.Text.Trim)

                        .SetValue("Status", "Submitted to CAI")
                        If ViewState("TermsAndConditionID") > 0 Then
                            .SetValue("TermsAndConditionID", ViewState("TermsAndConditionID"))
                        End If
                        Dim sError As String = String.Empty
                        If .Save(False, sError) Then
                            SaveAttachment(.RecordID)
                            lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.SuccessMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radWindow.VisibleOnPageLoad = True
                        End If
                    End With
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub SaveAttachment(ByVal RecordID As Long)
            Try
                raUploadDocs.AllowAdd = True
                raUploadDocs.AllowDelete = True
                raUploadDocs.AllowView = True
                raUploadDocs.EntityID = AptifyApplication.GetEntityID("LLLEnrolmentTracking__c")
                raUploadDocs.CategoryID = AptifyApplication.GetEntityRecordIDFromRecordName(AptifyApplication.GetEntityID("Attachment Categories"), "Enrolment Tracking")
                raUploadDocs.RecordID = RecordID
                raUploadDocs.SaveAttachment()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnAcceptTerms_Click(sender As Object, e As System.EventArgs) Handles btnAcceptTerms.Click
            Try
                radAcceptTerms.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Orders"
        Private Function OrderCreation(ByRef lOrderID As Long) As Long
            Try

                'redmine 17373
                'oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                oOrder = TryCast(Session("OrderGE"), Aptify.Applications.OrderEntry.OrdersEntity)
                oOrder.SetValue("ShipToID", User1.PersonID)
                oOrder.SetValue("BillToID", User1.PersonID)
                oOrder.SetValue("ShipTo" & "AddrLine1", txtAddressLine1.Text)
                oOrder.SetValue("ShipTo" & "AddrLine2", txtAddressLine2.Text)
                oOrder.SetValue("ShipTo" & "AddrLine3", txtAddressLine3.Text)
                oOrder.SetValue("ShipTo" & "City", txtCity.Text)
                oOrder.SetValue("ShipTo" & "State", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")))
                oOrder.SetValue("ShipTo" & "ZipCode", txtZipCode.Text)
                oOrder.SetValue("ShipTo" & "Country", cmbCountry.SelectedItem.Text)
                Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If iFirmAdmin > 0 Then
                    oOrder.SetValue("FirmPay__c", 1)
                End If
                ''If oOrder.SubTypes("OrderLines").Count > 0 Then
                ''    User1.CompanyID = hdnCompanyId.Value
                ''    User1.SetValue("AddressLine1", txtAddressLine1.Text)
                ''    User1.SetValue("AddressLine2", txtAddressLine2.Text)
                ''    User1.SetValue("AddressLine3", txtAddressLine3.Text)
                ''    User1.SetValue("City", txtCity.Text)
                ''    User1.SetValue("State", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")))
                ''    User1.SetValue("ZipCode", txtZipCode.Text)
                ''    User1.SetValue("Country", cmbCountry.SelectedItem.Text)
                ''    User1.SetAddValue("CountryCodeID", cmbCountry.SelectedValue.ToString)
                ''    User1.Save()
                ''    Dim iOrderAddrType As AptifyShoppingCart.OrderAddressType = AptifyShoppingCart.OrderAddressType.Shipping
                ''    Dim iPersonAddrType As AptifyShoppingCart.PersonAddressType = AptifyShoppingCart.PersonAddressType.Billing
                ''    Dim iSequence As Integer = 0
                ''    ShoppingCart1.UpdateOrderAddress(iOrderAddrType, iPersonAddrType)
                ''    ShoppingCart1.SaveCart(Session)
                ''End If
                'ShoppingCart1.SaveCart(Session)
                Session("OrderGE") = oOrder

                oOrder = CType(Session("OrderGE"), Aptify.Applications.OrderEntry.OrdersEntity)

                If oOrder.SubTypes("OrderLines").Count > 0 Then
                    oOrder.SetAddValue("_xBypassCreditCheck", 1) 'bypass credit check
                    If ddlPaymentPlan.SelectedValue <> "Select payment plan" AndAlso Not ViewState("SelectedPaymentPlan") Is Nothing Then
                        Dim oOrderPayInfo As PaymentInformation
                        Dim sErrorString As String = ""
                        Dim RefTranse As String = ""

                        Dim sLastError As String = ""
                        Dim oPersonGE As Aptify.Applications.Persons.PersonsEntity = TryCast(Me.AptifyApplication.GetEntityObject("Persons", User1.PersonID), Aptify.Applications.Persons.PersonsEntity)

                        Dim cmbSavedPaymentMethod As DropDownList = DirectCast(CreditCard.FindControl("cmbSavedPaymentMethod"), DropDownList)
                        If cmbSavedPaymentMethod.SelectedValue <> "" Then
                            With oPersonGE.SubTypes("PersonSavedPaymentMethods").Find("ID", cmbSavedPaymentMethod.SelectedValue)
                                oOrder.SetValue("SavedPaymentMethodID__c", cmbSavedPaymentMethod.SelectedValue)
                                oOrder.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                                oOrder.SetValue("InitialPaymentAmount", lblIntialAmount.Text)
                            End With
                        Else
                            ' Create new Save payment Method
                            'Commented by Govind: For decliened staged payments issue
                            'With oPersonGE.SubTypes("PersonSavedPaymentMethods").Add()
                            '    .SetValue("PersonID", User1.PersonID)
                            '    .SetValue("StartDate", Today.Date)
                            '    .SetValue("IsActive", 1)
                            '    .SetValue("Name", "SPM " & Today.Date)
                            '    .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                            '    .SetValue("CCAccountNumber", CreditCard.CCNumber)
                            '    .SetValue("CCExpireDate", CreditCard.CCExpireDate)
                            '    .SetValue("EndDate", CreditCard.CCExpireDate)
                            '    If .Fields("PaymentInformationID").EmbeddedObjectExists Then
                            '        oOrderPayInfo = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                            '        If oOrderPayInfo IsNot Nothing Then
                            '            oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCNumber
                            '            .SetValue("CCPartial", oOrderPayInfo.GetCCPartial(CStr(CreditCard.CCNumber)))
                            '        End If
                            '    End If

                            'End With
                        End If
                        'Commented by Govind: For decliened staged payments issue
                        'Dim sPersonSaveError As String = String.Empty
                        'If oPersonGE.Save(False, sPersonSaveError) Then
                        '    oOrder.SetValue("SavedPaymentMethodID__c", oPersonGE.SubTypes("PersonSavedPaymentMethods").Item(oPersonGE.SubTypes("PersonSavedPaymentMethods").Count - 1).RecordID)
                        '    oOrder.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                        '    oOrder.SetValue("InitialPaymentAmount", (lblIntialAmount.Text))
                        '    oOrder.SetValue("OrderTypeID", Aptify.Applications.OrderEntry.OrderType.Regular)   ' Regular Order 
                        '    oOrder.OrderStatus = OrderStatus.Taken
                        '    oOrder.PayTypeID = AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order")
                        '    oOrder.PONumber = "Stage Payment"
                        'End If

                        'Added by Govind: For decliened staged payments issue
                        If cmbWhoPays.SelectedValue <> 3 Then ' if condition added by GM for Redmine #20327
                            'Added for Redmine #20327
                            oOrder.SetValue("BillToSameAsShipTo", 0)
                            oOrder.SetValue("BillToCompanyID", -1)
                            'End Redmine #20327
                        End If
                        oOrder.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                        oOrder.SetValue("InitialPaymentAmount", (lblIntialAmount.Text))
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
                            oOrderPayInfo = DirectCast(oOrder.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                            oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                            ''RashmiP, Issue 10254, SPM
                            oOrderPayInfo.SetValue("SaveForFutureUse", 1)
                            If cmbWhoPays.SelectedValue = 3 Then ' Added by GM for Redmine #20327
                                oOrderPayInfo.SetValue("SaveToTypes", "Company")
                            End If ' End Redmine #20327
                            'Ansar Shaikh - Issue 11986 - 12/27/2011
                            'Ani B for issue 10254 on 22/04/2013
                            'Set CC Partial for credit cart type is reference transaction 
                            If CreditCard.CCNumber = "-Ref Transaction-" Then
                                oOrderPayInfo.SetValue("CCPartial", CreditCard.CCPartial)
                            End If
                        End If
                        If cmbWhoPays.SelectedValue = 3 Then ' Added by GM for Redmine #20327
                            oOrder.SetValue("FirmPay__c", 1)
                        End If ' End Redmine #20327

                    Else
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
                            If cmbWhoPays.SelectedValue = 3 Then ' Added by GM for Redmine #20327
                                oOrderPayInfo.SetValue("SaveToTypes", "Company")
                            End If
                            'Ansar Shaikh - Issue 11986 - 12/27/2011
                            'Ani B for issue 10254 on 22/04/2013
                            'Set CC Partial for credit cart type is reference transaction 
                            If CreditCard.CCNumber = "-Ref Transaction-" Then
                                oOrderPayInfo.SetValue("CCPartial", CreditCard.CCPartial)
                            End If
                        End If
                        If cmbWhoPays.SelectedValue = 3 Then ' Added by GM for Redmine #20327
                            oOrder.SetValue("FirmPay__c", 1)
                        End If
                    End If
                    Dim sError As String = String.Empty

                    'lOrderID = ShoppingCart1.PlaceOrder(Session, Application, Page.User, sError)
                    If cmbWhoPays.SelectedValue = 3 Then
                        oOrder.ShipToCompanyID = hdnCompanyId.Value
                        If oOrder.SubTypes("OrderLines") IsNot Nothing AndAlso oOrder.SubTypes("OrderLines").Count > 0 Then
                            For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                                oOrder.SubTypes("OrderLines").Item(i).SetValue("BillToCompanyID__c", hdnCompanyId.Value)
                            Next
                        End If
                    End If
                    oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web"))
                    Dim sError1 As String = String.Empty
                    If oOrder.Save(False, sError1) Then
                        lOrderID = oOrder.RecordID
                    End If
                    If lOrderID <= 0 Then
                        'lblMessage.Visible = True
                        'lblMessage.Text = "<ui><li>" + sError + "</li></ui>"

                        lblMessage.Text = sError1 + "Credit Card Verification Failed"
                        lblMessage.ForeColor = Drawing.Color.Red
                        lblMessage.Style.Add("font-size", "medium")
                        lblMessage.Visible = True
                        lOrderID = -1
                    Else

                        If ddlPaymentPlan.SelectedValue <> "Select payment plan" AndAlso Not ViewState("SelectedPaymentPlan") Is Nothing Then
                        Else
                            ''  PostPayment(lOrderID, dTaxAmount,oOrder)
                            ''  If oOrder.RecordID > 0 Then
                            ''  lOrderID = -1
                            ''End If
                        End If
                        ShoppingCart1.Clear(Nothing)
                    End If
                Else
                    lOrderID = 1
                End If

                Return lOrderID
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return -1
            End Try
        End Function
        Private Sub PostPayment(ByVal OrderID As Long, ByVal TaxAmount As Decimal, ByRef OrderGE As OrdersEntity)
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
                    If .Save(False, errorMessage) Then
                        lblCreditFailed.Visible = False
                    Else
                        lblCreditFailed.Text = "Credit Card Verification Failed"
                        lblCreditFailed.Visible = True
                        OrderGE.Delete()
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New Exception(errorMessage))
                    End If
                End With


            Catch ex As Exception
                OrderGE.Delete()
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region

#Region "Save Methods"
        ''' <summary>
        ''' Create new comapny
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function CreateNewCompany() As Long
            Try
                Dim iCompanyID As Long = 0
                If hdnCompanyId.Value <> "" AndAlso hdnCompanyId.Value <> "0" Then
                    iCompanyID = hdnCompanyId.Value

                End If
                If iCompanyID <= 0 Then
                    Dim oComapnyGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Companies", -1)
                    With oComapnyGE
                        .SetValue("Name", txtFirmName.Text.Trim)
                        .SetValue("Status__c", "Pending")
                        .SetValue("CreditStatusID", AptifyApplication.GetEntityRecordIDFromRecordName("Credit Status", "Approved"))
                        .SetValue("CreditLimit", AptifyApplication.GetEntityAttribute("Companies", "FirmCreditLimit__c"))
                        Dim oAddress As AptifyGenericEntityBase
                        oAddress = oComapnyGE.Fields("AddressID").EmbeddedObject
                        With oAddress
                            .SetValue("Line1", txtAddressLine1.Text)
                            .SetValue("Line2", txtAddressLine2.Text)
                            .SetValue("Line3", txtAddressLine3.Text)
                            .SetValue("City", txtCity.Text)
                            .SetValue("StateProvince", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")))
                            .SetValue("PostalCode", txtZipCode.Text)
                            .SetValue("CountryCodeID", cmbCountry.SelectedValue.ToString)
                            .SetValue("Country", cmbCountry.SelectedItem.Text)
                        End With
                        Dim sError As String = String.Empty
                        If .Save(False, sError) Then

                            iCompanyID = .RecordID
                            Dim oPersonGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                            oPersonGE.SetValue("CompanyID", iCompanyID)
                            oPersonGE.Save(False)
                        End If
                    End With
                End If
                Return iCompanyID
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        Protected Function CreateAccounatncyBody() As Long
            Try
                Dim iAccountancyBodyID As Long = 0
                If hdnAwardingBody.Value <> "" AndAlso hdnAwardingBody.Value <> "0" Then
                    iAccountancyBodyID = hdnAwardingBody.Value
                Else
                    iAccountancyBodyID = AptifyApplication.GetEntityRecordIDFromRecordName("Accountancybodys__c", txtAccountancyBody.Text.Trim)
                End If

                If iAccountancyBodyID <= 0 Then
                    Dim oAccountancyBodyGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Accountancybodys__c", -1)
                    With oAccountancyBodyGE
                        .SetValue("Name", txtAccountancyBody.Text.Trim)
                        .SetValue("Status", "Pending")
                        Dim sError As String = String.Empty
                        If .Save(False, sError) Then
                            iAccountancyBodyID = .RecordID
                        End If
                    End With
                End If
                Return iAccountancyBodyID
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        Protected Sub btnChangeAddress_Click(sender As Object, e As System.EventArgs) Handles btnChangeAddress.Click
            Session("CACompanyID") = hdnCompanyId.Value
            Session("CACompany") = txtFirmName.Text.Trim
            Session("CAAccountanyBodyID") = hdnAwardingBody.Value
            Session("CAAccountancyBody") = txtAccountancyBody.Text.Trim
            'Response.Redirect(ChangeAddressURL & "?Type=Billing&ReturnToPage=/EBusiness/LLL/LLLEnrolmentApplication.aspx?cid=" & Request.QueryString("cid") & "&CA=1", False)
            Response.Redirect(ChangeAddressURL & "?Type=Shipping&ReturnToPage=" & ReturnToURL & "?cid=" & Request.QueryString("cid") & "&CA=1", False)
        End Sub
#End Region

#Region "Change Event"
        Protected Sub txtFirmName_TextChanged(sender As Object, e As System.EventArgs) Handles txtFirmName.TextChanged
            Try
                Dim sFirmName() As String = txtFirmName.Text.Trim.Split(CChar("/"))
                Dim sFirmWCity As String
                If sFirmName.Length >= 2 Then
                    sFirmWCity = sFirmName(0) + "/" + sFirmName(1)
                Else
                    sFirmWCity = sFirmName(0)
                End If
                Dim iCompanyID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Companies", sFirmWCity))
                If iCompanyID <= 0 Then
                    hdnCompanyId.Value = "0"
                    If txtFirmName.Text.Trim <> "" Then
                        divAdd1.Visible = True
                        divAdd2.Visible = True
                        divAdd3.Visible = True
                        divCity.Visible = True
                        divCountry.Visible = True
                        txtAddressLine1.Text = ""
                        txtAddressLine2.Text = ""
                        txtAddressLine3.Text = ""
                        txtCity.Text = ""
                        txtZipCode.Text = ""

                        NewCompanyAddress()
                    Else
                        divAdd1.Visible = False
                        divAdd2.Visible = False
                        divAdd3.Visible = False
                        divCity.Visible = False
                        divCountry.Visible = False
                    End If

                    divCompanyAddressID.Visible = False
                Else

                    divAdd1.Visible = False
                    divAdd2.Visible = False
                    divAdd3.Visible = False
                    divCity.Visible = False
                    divCountry.Visible = False

                    If cmbWhoPays.SelectedItem.Text.ToLower.Trim = "pay now with firm credit card" Then
                        If hdnCompanyId.Value > 0 Then
                            CompanyAddress(iCompanyID)
                            divAdd1.Visible = True
                            divAdd2.Visible = True
                            divAdd3.Visible = True
                            divCity.Visible = True
                            divCountry.Visible = True
                            'divCompanyAddressID.Visible = True
                            divCompanyAddressID.Visible = False
                        End If
                    Else
                        divCompanyAddressID.Visible = False
                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region

        ''' <summary>
        '''  Code added by Prachi for Redmine #15496
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Protected Sub btnCopanyDupeCheck_Click(sender As Object, e As System.EventArgs) Handles btnCopanyDupeCheck.Click
            Try
                radCompanyDupeCheck.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''End Redmine #15496

    End Class
End Namespace
