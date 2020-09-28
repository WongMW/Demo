' Rajesh :- Added for Firm Portal Config requirement of BillMelater display.
' Date   :- 06/02/2014

Option Explicit On
Option Strict On
Imports Aptify.Applications.Accounting
Imports System.Data
Imports System.Globalization

Namespace Aptify.Framework.Web.eBusiness

    ''' <summary>
    ''' Aptify Credit Card User Control for e-Business
    ''' </summary>
    Partial Class CreditCard__c
        Inherits BaseUserControlAdvanced
#Region "Properties"

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "CreditCard__c"
        Protected Const ATTRIBUTE_BILL_ME_LATER As String = "BillMeLaterDisplayText"

        Protected Const ATTRIBUTE_AMEX_ENABLED_IMG As String = "AmexEnabled"
        Protected Const ATTRIBUTE_AMEX_DISABLED_IMG As String = "AmexDisabled"

        Protected Const ATTRIBUTE_DELTA_ENABLED_IMG As String = "DeltaEnabled"
        Protected Const ATTRIBUTE_DELTA_DISABLED_IMG As String = "DeltaDisabled"

        Protected Const ATTRIBUTE_DINER_ENABLED_IMG As String = "DinerEnabled"
        Protected Const ATTRIBUTE_DINER_DISABLED_IMG As String = "DinerDisabled"

        Protected Const ATTRIBUTE_LASER_ENABLED_IMG As String = "LaserEnabled"
        Protected Const ATTRIBUTE_LASER_DISABLED_IMG As String = "LaserDisabled"

        Protected Const ATTRIBUTE_JCB_ENABLED_IMG As String = "JcbEnabled"
        Protected Const ATTRIBUTE_JCB_DISABLED_IMG As String = "JcbDisabled"

        Protected Const ATTRIBUTE_SOLO_ENABLED_IMG As String = "SoloEnabled"
        Protected Const ATTRIBUTE_SOLO_DISABLED_IMG As String = "SoloDisabled"

        Protected Const ATTRIBUTE_SWITCH_ENABLED_IMG As String = "SwitchEnabled"
        Protected Const ATTRIBUTE_SWITCH_DISABLED_IMG As String = "SwitchDisabled"

        Protected Const ATTRIBUTE_MASTERCARD_ENABLED_IMG As String = "MasterCardEnabled"
        Protected Const ATTRIBUTE_MASTERCARD_DISABLED_IMG As String = "MasterCardDisabled"

        Protected Const ATTRIBUTE_VISA_ENABLED_IMG As String = "VisaEnabled"
        Protected Const ATTRIBUTE_VISA_DISABLED_IMG As String = "VisaDisabled"


        'Added By Kavita Zinage as per #13429
        Protected Const ATTRIBUTE_VISADEBIT_ENABLED_IMG As String = "VisaDebitEnabled"
        Protected Const ATTRIBUTE_VISADEBIT_DISABLED_IMG As String = "VisaDebitDisabled"


        'Suraj Issue 15014 4/24/13 , ATTRIBUTE_CONTORL_IsBillMeLaterDisable used for set the property "DisableBillMeLater"
        Protected ATTRIBUTE_CONTORL_IsBillMeLaterDisable As Boolean = False
        'Anil B for Credit Card recognization Performance on 21/jun/2013
        Protected Const ATTRIBUTE_PAYMENT_INFO_OBJECT As String = "PaymentInfo"


        Dim m_iPOPaymentType As Integer = 0
        Dim m_iUserCreditStatus As Integer = 0
        Dim m_lUserCreditLimit As Long = 0
        Dim m_iCompanyCreditStatus As Integer = 0
        Dim m_lCompanyCreditLimit As Long = 0
        Dim m_bCreditChecklimit As Boolean = False
        Dim m_oPaymentInfo As Aptify.Applications.Accounting.PaymentInformation = Nothing
        Dim m_oGESPM As CommonMethods
        Dim bSaveForFuture As Boolean = True
        'Anil B Issue 10254 on 07-03-2013
        Dim bFromSavePaymentMethod As Boolean = False

        Dim m_CCNumber As String
        'Anil B Issue 10254 on 07-03-2013
        Dim m_ReferenceTransaction As String
        Dim m_ExpirationDate As Date


        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
            If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID")) Then
                m_iPOPaymentType = CInt(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID"))
            End If

            If String.IsNullOrEmpty(AmexEnabled) Then
                AmexEnabled = Me.GetLinkValueFromXML(ATTRIBUTE_AMEX_ENABLED_IMG)
            End If

            If String.IsNullOrEmpty(AmexDisabled) Then
                AmexDisabled = Me.GetLinkValueFromXML(ATTRIBUTE_AMEX_DISABLED_IMG)
            End If

            If String.IsNullOrEmpty(DeltaEnabled) Then
                DeltaEnabled = Me.GetLinkValueFromXML(ATTRIBUTE_DELTA_ENABLED_IMG)
            End If

            If String.IsNullOrEmpty(DeltaDisabled) Then
                DeltaDisabled = Me.GetLinkValueFromXML(ATTRIBUTE_DELTA_DISABLED_IMG)
            End If

            If String.IsNullOrEmpty(DinerEnabled) Then
                DinerEnabled = Me.GetLinkValueFromXML(ATTRIBUTE_DINER_ENABLED_IMG)
            End If

            If String.IsNullOrEmpty(DinerDisabled) Then
                DinerDisabled = Me.GetLinkValueFromXML(ATTRIBUTE_DINER_DISABLED_IMG)
            End If

            If String.IsNullOrEmpty(JcbEnabled) Then
                JcbEnabled = Me.GetLinkValueFromXML(ATTRIBUTE_JCB_ENABLED_IMG)
            End If

            If String.IsNullOrEmpty(JcbDisabled) Then
                JcbDisabled = Me.GetLinkValueFromXML(ATTRIBUTE_JCB_DISABLED_IMG)
            End If

            If String.IsNullOrEmpty(LaserEnabled) Then
                LaserEnabled = Me.GetLinkValueFromXML(ATTRIBUTE_LASER_ENABLED_IMG)
            End If

            If String.IsNullOrEmpty(LaserDisabled) Then
                LaserDisabled = Me.GetLinkValueFromXML(ATTRIBUTE_LASER_DISABLED_IMG)
            End If

            If String.IsNullOrEmpty(MasterCardEnabled) Then
                MasterCardEnabled = Me.GetLinkValueFromXML(ATTRIBUTE_MASTERCARD_ENABLED_IMG)
            End If

            If String.IsNullOrEmpty(MasterCardDisabled) Then
                MasterCardDisabled = Me.GetLinkValueFromXML(ATTRIBUTE_MASTERCARD_DISABLED_IMG)
            End If

            If String.IsNullOrEmpty(SoloEnabled) Then
                SoloEnabled = Me.GetLinkValueFromXML(ATTRIBUTE_SOLO_ENABLED_IMG)
            End If

            If String.IsNullOrEmpty(SoloDisabled) Then
                SoloDisabled = Me.GetLinkValueFromXML(ATTRIBUTE_SOLO_DISABLED_IMG)
            End If

            If String.IsNullOrEmpty(SwitchEnabled) Then
                SwitchEnabled = Me.GetLinkValueFromXML(ATTRIBUTE_SWITCH_ENABLED_IMG)
            End If

            If String.IsNullOrEmpty(SwitchDisabled) Then
                SwitchDisabled = Me.GetLinkValueFromXML(ATTRIBUTE_SWITCH_DISABLED_IMG)
            End If

            If String.IsNullOrEmpty(VisaEnabled) Then
                VisaEnabled = Me.GetLinkValueFromXML(ATTRIBUTE_VISA_ENABLED_IMG)
            End If

            If String.IsNullOrEmpty(VisaDisabled) Then
                VisaDisabled = Me.GetLinkValueFromXML(ATTRIBUTE_VISA_DISABLED_IMG)
            End If

            'Added By Kavita Zinage on 13/05/2016 as per #13429
            If String.IsNullOrEmpty(VisaDebitEnabled) Then
                VisaDebitEnabled = Me.GetLinkValueFromXML(ATTRIBUTE_VISADEBIT_ENABLED_IMG)
            End If

            If String.IsNullOrEmpty(VisaDebitDisabled) Then
                VisaDebitDisabled = Me.GetLinkValueFromXML(ATTRIBUTE_VISADEBIT_DISABLED_IMG)
            End If

        End Sub

        Public Property CCNumber() As String
            Get
                EnsureChildControls()
                If IsNumeric(txtCCNumber.Text) Then
                    Return txtCCNumber.Text
                Else
                    Return CStr(hfCCNumber.Value)
                End If
            End Get
            Set(ByVal Value As String)
                EnsureChildControls()
                If IsNumeric(txtCCNumber.Text) Then
                    txtCCNumber.Text = Value
                Else
                    hfCCNumber.Value = Value
                End If
            End Set
        End Property

        'Anil B Issue 10254 on 20/04/2013.Define property for CCPrtian number
        Public Property CCPartial() As String
            Get
                Return txtCCNumber.Text
            End Get
            Set(ByVal Value As String)
                txtCCNumber.Text = Value
            End Set
        End Property

        'Anil B Issue 10254 on 07-03-2013.Define property for Reference transaction number for perticuler SPM
        Public Property ReferenceTransactionNumber() As String
            Get
                If ViewState.Item("ReferenceTransactionNumber") IsNot Nothing Then
                    Return ViewState.Item("ReferenceTransactionNumber").ToString()
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item("ReferenceTransactionNumber") = value
            End Set
        End Property
        'Anil B Issue 10254 on 07-03-2013 Define property for  transaction Expiration date for perticuler SPM
        Public Property ReferenceExpiration() As Date
            Get
                If ViewState.Item("ReferenceExpiration") IsNot Nothing Then
                    Return CDate(ViewState.Item("ReferenceExpiration"))
                End If
            End Get
            Set(ByVal value As Date)
                ViewState.Item("ReferenceExpiration") = value
            End Set
        End Property

        ''' <summary>
        ''' Credit Card Security Number
        ''' </summary>
        Public Property CCSecurityNumber() As String
            '   Added property for Credit Card security number feature on e-business site.
            '   Change made by Vijay Sitlani for Issue 5369 
            Get
                EnsureChildControls()
                Return txtCCSecurityNumber.Text
            End Get
            Set(ByVal Value As String)
                EnsureChildControls()
                txtCCSecurityNumber.Text = Value
            End Set
        End Property

        'HP Issue#8972: need ability to disable validators if shopping cart total due is zero
        ''' <summary>
        ''' Credit Card Expiration validator setting
        ''' </summary>
        Public Property CCNumberValidatorSetting() As Boolean
            Get
                Return RequiredFieldValidator1.Enabled
            End Get
            Set(ByVal Value As Boolean)
                RequiredFieldValidator1.Enabled = Value
            End Set
        End Property

        'HP Issue#8972: need ability to disable validators if shopping cart total due is zero
        ''' <summary>
        ''' Credit Card Security Number validator setting
        ''' </summary>
        Public Property CCSecurityNumberValidatorSetting() As Boolean
            Get
                Return RequiredFieldValidator2.Enabled
            End Get
            Set(ByVal Value As Boolean)
                RequiredFieldValidator2.Enabled = Value
            End Set
        End Property

        ''' <summary>
        ''' Credit Card Expiration Date
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CCExpireDate() As String
            Get
                EnsureChildControls()
                ' Build the appropriate string of the date information
                ' to pass back to the parent
                Dim dExpDate As Date
                'Issue 20655 : Incorrect Expiry Date Shows Up On the Order And Paypal when setting UK culture
                Dim vExpDate As String
                vExpDate = dropdownMonth.SelectedValue & "/1/" &
                                    dropdownYear.SelectedValue
                ' dExpDate = CDate(dropdownMonth.SelectedValue & "/1/" & _
                '                  dropdownYear.SelectedValue)
                Dim usCulture As CultureInfo = New CultureInfo("en-US")
                dExpDate = DateTime.Parse(vExpDate, usCulture.DateTimeFormat)

                dExpDate = DateAdd(DateInterval.Month, 1, dExpDate)
                dExpDate = DateAdd(DateInterval.Day, -1, dExpDate)
                Return dExpDate.ToString

            End Get
            Set(ByVal Value As String)
                Dim d As DateTime

                Me.EnsureChildControls()
                ' Break apart the passed in date values
                ' and select the appropriate fields

                If IsDate(Value) Then
                    d = CDate(Value)
                    dropdownMonth.SelectedIndex = d.Month - 1
                    'dropdownDay.SelectedIndex = d.Day - 1
                    dropdownYear.SelectedIndex = d.Year - Now.Year
                Else
                    dropdownMonth.SelectedIndex = Today.Month - 1
                    'dropdownDay.SelectedIndex = Today.Day - 1
                    dropdownYear.SelectedIndex = (Today.Year + 1) - Now.Year
                End If
            End Set
        End Property

        'Commented as there as datetime format issue
        'Public Property CCExpireDate() As String
        '    Get
        '        EnsureChildControls()
        '        ' Build the appropriate string of the date information
        '        ' to pass back to the parent
        '        Dim dExpDate As Date
        '        dExpDate = CDate(dropdownMonth.SelectedValue & "/1/" & _
        '                           dropdownYear.SelectedValue)
        '        dExpDate = DateAdd(DateInterval.Month, 1, dExpDate)
        '        dExpDate = DateAdd(DateInterval.Day, -1, dExpDate)
        '        Return dExpDate.ToString

        '    End Get
        '    Set(ByVal Value As String)
        '        Dim d As DateTime

        '        Me.EnsureChildControls()
        '        ' Break apart the passed in date values
        '        ' and select the appropriate fields

        '        If IsDate(Value) Then
        '            d = CDate(Value)
        '            dropdownMonth.SelectedIndex = d.Month - 1
        '            'dropdownDay.SelectedIndex = d.Day - 1
        '            dropdownYear.SelectedIndex = d.Year - Now.Year
        '        Else
        '            dropdownMonth.SelectedIndex = Today.Month - 1
        '            'dropdownDay.SelectedIndex = Today.Day - 1
        '            dropdownYear.SelectedIndex = (Today.Year + 1) - Now.Year
        '        End If
        '    End Set
        'End Property

        Public Property PaymentTypeID() As Long
            Get
                EnsureChildControls()
                If chkBillMeLater.Checked Then
                    Return m_iPOPaymentType
                Else
                    If cmbCreditCard.SelectedItem Is Nothing Then
                        Return -1
                    Else
                        Return CLng(cmbCreditCard.SelectedItem.Value)
                    End If
                End If
            End Get
            Set(ByVal Value As Long)
                Try
                    EnsureChildControls()
                    Dim oItem As System.Web.UI.WebControls.ListItem
                    oItem = cmbCreditCard.Items.FindByValue(CStr(Value))
                    If Not oItem Is Nothing Then
                        oItem.Selected = True
                    End If
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            End Set
        End Property

        Public Property ShowPaymentTypeSelection() As Boolean
            Get
                Me.EnsureChildControls()
                Return PaymentTypeSelection.Visible
            End Get
            Set(ByVal Value As Boolean)
                Me.EnsureChildControls()
                PaymentTypeSelection.Visible = Value
            End Set
        End Property

        ''RashmiP Issue 6781
        Public Property PONumber() As String
            Get
                EnsureChildControls()
                Return txtPONumber.Text
            End Get
            Set(ByVal Value As String)
                EnsureChildControls()
                txtPONumber.Text = Value
            End Set
        End Property

        Public Property BillMeLaterChecked() As Boolean
            Get
                If chkBillMeLater.Checked Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                chkBillMeLater.Checked = value
            End Set
        End Property

        Public Property UserCreditStatus() As Integer
            Get
                Return m_iUserCreditStatus
            End Get
            Set(ByVal value As Integer)
                m_iUserCreditStatus = value
            End Set
        End Property

        Public Property UserCreditLimit() As Long
            Get
                Return m_lUserCreditLimit
            End Get
            Set(ByVal value As Long)
                m_lUserCreditLimit = value
            End Set
        End Property

        Public Property CompanyCreditStatus() As Integer
            Get
                Return m_iCompanyCreditStatus
            End Get
            Set(ByVal value As Integer)
                m_iCompanyCreditStatus = value
            End Set
        End Property

        Public Property CompanyCreditLimit() As Long
            Get
                Return m_lCompanyCreditLimit
            End Get
            Set(ByVal value As Long)
                m_lCompanyCreditLimit = value
            End Set
        End Property

        Public Property CreditCheckLimit() As Boolean
            Get
                Return m_bCreditChecklimit
            End Get
            Set(ByVal value As Boolean)
                m_bCreditChecklimit = value
                chkBillMeLater.Visible = m_bCreditChecklimit
                lblBillMelater.Visible = m_bCreditChecklimit
            End Set
        End Property

        'RashmiP, issue 6781
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

        Private Property AmexEnabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_AMEX_ENABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_AMEX_ENABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_AMEX_ENABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property AmexDisabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_AMEX_DISABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_AMEX_DISABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_AMEX_DISABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property DeltaEnabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_DELTA_ENABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_DELTA_ENABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_DELTA_ENABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property DeltaDisabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_DELTA_DISABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_DELTA_DISABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_DELTA_DISABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property DinerEnabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_DINER_ENABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_DINER_ENABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_DINER_ENABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property DinerDisabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_DINER_DISABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_DINER_DISABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_DINER_DISABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property JcbEnabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_JCB_ENABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_JCB_ENABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_JCB_ENABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property JcbDisabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_JCB_DISABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_JCB_DISABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_JCB_DISABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property LaserEnabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_LASER_ENABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_LASER_ENABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_LASER_ENABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property LaserDisabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_LASER_DISABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_LASER_DISABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_LASER_DISABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property MasterCardEnabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_MASTERCARD_ENABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_MASTERCARD_ENABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_MASTERCARD_ENABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property MasterCardDisabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_MASTERCARD_DISABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_MASTERCARD_DISABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_MASTERCARD_DISABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property SoloEnabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_SOLO_ENABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_SOLO_ENABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_SOLO_ENABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property SoloDisabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_SOLO_DISABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_SOLO_DISABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_SOLO_DISABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property SwitchEnabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_SWITCH_ENABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_SWITCH_ENABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_SWITCH_ENABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property SwitchDisabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_SWITCH_DISABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_SWITCH_DISABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_SWITCH_DISABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property VisaEnabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_VISA_ENABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_VISA_ENABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_VISA_ENABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property VisaDisabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_VISA_DISABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_VISA_DISABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_VISA_DISABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        'Added by Kavita Zinage to get image for visa debit - as per #13429

        Private Property VisaDebitEnabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_VISADEBIT_ENABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_VISADEBIT_ENABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_VISADEBIT_ENABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Property VisaDebitDisabled() As String
            Get
                If Not ViewState.Item(ATTRIBUTE_VISADEBIT_DISABLED_IMG) Is Nothing Then
                    Return CStr(ViewState.Item(ATTRIBUTE_VISADEBIT_DISABLED_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item(ATTRIBUTE_VISADEBIT_DISABLED_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        'Till here

        Public ReadOnly Property SaveCardforFutureUse As Boolean
            Get
                If chkSaveforFutureUse.Checked Then
                    Return True
                Else
                    Return False
                End If
            End Get

        End Property

        'Anil B for issue 15374 on 02/04/2013 Property for future use checkbox 
        Public Property SetchkSaveforFutureUse() As Boolean
            Get
                Return bSaveForFuture
            End Get
            Set(ByVal value As Boolean)
                bSaveForFuture = value
                chkSaveforFutureUse.Visible = bSaveForFuture
            End Set
        End Property

        'Suraj Issue 15014 4/24/13 , declare the prpoperty, this property set on fundraising page for disable the bill me later check box
        Public Property DisableBillMeLater() As Boolean
            Get
                Return Me.ATTRIBUTE_CONTORL_IsBillMeLaterDisable
            End Get
            Set(ByVal value As Boolean)
                Me.ATTRIBUTE_CONTORL_IsBillMeLaterDisable = value
            End Set
        End Property

        'Anil B for Credit Card recognization Performance on 21/jun/2013
        Public Property PaymentInfo() As Aptify.Applications.Accounting.PaymentInformation
            Get
                If Session(ATTRIBUTE_PAYMENT_INFO_OBJECT) Is Nothing Then
                    Session(ATTRIBUTE_PAYMENT_INFO_OBJECT) = DirectCast(ShoppingCart1.GetOrderObject(Session, Page.User, Application).Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                End If
                Return CType(Session(ATTRIBUTE_PAYMENT_INFO_OBJECT), PaymentInformation)
            End Get
            Set(ByVal value As Aptify.Applications.Accounting.PaymentInformation)
                Session(ATTRIBUTE_PAYMENT_INFO_OBJECT) = value
            End Set
        End Property

        'Define property for Reference transaction number for perticuler SPM
        Public Property BillmelaterCheck() As Boolean
            Get
                If ViewState.Item("BillmelaterCheck") IsNot Nothing Then
                    Return Convert.ToBoolean(ViewState.Item("BillmelaterCheck"))
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                ViewState.Item("BillmelaterCheck") = value
            End Set
        End Property

#End Region

        'Rajesh -  Added for Firm Portal Config requirement
        Public Sub ShowHideBillMeLater()
            Dim sError As String
            Dim bSetVisible As Boolean
            'Suraj Issue 15014 4/24/13, if ATTRIBUTE_CONTORL_IsBillMeLaterDisable is True then remove the bill me later check box for fundraising page
            Try
                If m_iPOPaymentType > 0 Then
                    If CompanyCreditStatus = 2 AndAlso CompanyCreditLimit > 0 AndAlso CreditCheckLimit AndAlso ATTRIBUTE_CONTORL_IsBillMeLaterDisable = False Then
                        bSetVisible = True
                    ElseIf UserCreditStatus = 2 AndAlso UserCreditLimit > 0 AndAlso CreditCheckLimit AndAlso ATTRIBUTE_CONTORL_IsBillMeLaterDisable = False Then
                        bSetVisible = True
                    Else
                        bSetVisible = False
                    End If
                Else
                    bSetVisible = False
                End If

                ShowHideControls()
                chkBillMeLater.Visible = bSetVisible
                lblBillMelater.Visible = bSetVisible
                If bSetVisible = False Then
                    chkBillMeLater.Checked = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        ''' <summary>
        ''' Load all credit cards
        ''' </summary>
        Public Sub LoadCreditCardInfo()
            Dim sSQL As Text.StringBuilder = New Text.StringBuilder
            Dim dt As Data.DataTable
            Dim sql As String = String.Empty
            Try
                'Ansar Shaikh - Issue 11986 - 12/27/2011
                'Anil B Issue 16167 on 07-05-2013
                'Changed query
                'sSQL.Append("SELECT PT.ID,CC.Name FROM " & Database & _
                '         "..vwPaymentTypes PT inner join " & Database & "..vwCreditCardTypes CC on PT.CreditCardTypeID= CC.ID WHERE PT.Active=1 AND PT.WebEnabled=1 AND " & _
                '          "(PT.Type='Credit Card' OR PT.Type='Credit Card Reference Transaction') " & "AND PT.ServiceChargeFlatFeeCurrencyTypeID =" & preferredCurrencyTypeID & _
                '          " UNION SELECT '-1',' ' ORDER BY CC.Name") ''Query changed by RashmiP, issue 9024

                'dt = DataAction.GetDataTable(sSQL.ToString)

                'Milind Sutar
                'Replaced inline query with a store procedure
                sql = "..spGetCreditCards__c"
                dt = DataAction.GetDataTable(sql)
                cmbCreditCard.DataSource = dt
                cmbCreditCard.DataTextField = "Name"
                cmbCreditCard.DataValueField = "ID"
                cmbCreditCard.DataBind()
                'ShowCreditCards(dt)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Show only those credit card those are configured in smart client
        ''' </summary>
        Private Sub ShowCreditCards(creditCards As Data.DataTable)
            ImgAmex.Visible = False
            ImgAmex.Visible = False
            'Commented by Kavita Zinage as per #13429
            'ImgDelta.Visible = False
            'ImgDiner.Visible = False
            'ImgJcb.Visible = False
            'ImgLaser.Visible = False
            ImgMasterCard.Visible = False
            'Commented by Kavita Zinage as per #13429
            'ImgSolo.Visible = False
            'ImgSwitch.Visible = False
            ImgVisa.Visible = False

            'Added by Kavita Zinage as per #13429
            ''ImgVisaDebit.Visible = False

            For i As Integer = 0 To creditCards.Rows.Count - 1
                Dim card = creditCards.Rows(i)("Name").ToString()
                Select Case (card)
                    Case "Amex"
                        ImgAmex.Visible = True
                        'Commented by Kavita Zinage as per #13429
                        'Case "Delta"
                        '    ImgDelta.Visible = True
                        'Case "Diners"
                        '    ImgDiner.Visible = True
                        'Case "Jcb"
                        '    ImgJcb.Visible = True
                        'Case "Laser"
                        '    ImgLaser.Visible = True
                    Case "MasterCard"
                        ImgMasterCard.Visible = True
		    Case "MC"
                        ImgMasterCard.Visible = True
                        'Commented by Kavita Zinage as per #13429
                        'Case "Solo"
                        '    ImgSolo.Visible = True
                        'Case "Switch"
                        '    ImgSwitch.Visible = True
                       'Sachin Sathe added below code
                   Case "MC"
                    ImgMasterCard.ImageUrl = MasterCardEnabled
                    ImgMasterCard.DataBind()
                    '' End of sachin's code
                    Case "Visa"
                        ImgVisa.Visible = True
                        'Added by Kavita Zinage as per #13429
                    Case "Visa Debit"
                        ''ImgVisaDebit.Visible = True
                End Select
            Next

        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'set control properties from XML file if needed
            SetProperties()

            'Anil B for Credit Card recognization Performance on 21/jun/2013
            If PaymentInfo IsNot Nothing Then
                m_oPaymentInfo = PaymentInfo
            End If
            Dim iDay As Integer
            Dim iYear As Integer
            'Anil B Issue 10254 on 07-03-2013
            'Set Enable Desabled Card and Error message 
            If IsPostBack AndAlso txtCCNumber.Text = String.Empty Then
                DisableCreditCard()
                lblError.Visible = False
                bFromSavePaymentMethod = False
                ''If Not Session("OrderuniqueGuid") Is Nothing AndAlso Convert.ToString(Session("OrderuniqueGuid")) <> "" Then
                ''    lblReceiptNo.Text = Convert.ToString(Session("OrderuniqueGuid"))
                ''    tdReceiptlbl.Visible = True
                ''    tdReceiptNo.Visible = True
                ''Else
                ''    tdReceiptlbl.Visible = False
                ''    tdReceiptNo.Visible = False
                ''End If


            End If
            If BillmelaterCheck Then
                tdReceiptlbl.Visible = True
                tdReceiptNo.Visible = True
            Else

                tdReceiptlbl.Visible = False
                tdReceiptNo.Visible = False
            End If
            'Anil B Issue 10254 on 07-03-2013
            'Set Javascript function to control
            txtCCNumber.Attributes.Add("onblur", "javascript:SecurityDesabled()")
            txtCCNumber.Attributes.Add(" onchange", "javascript:SecurityEnabled()")
            m_oGESPM = New CommonMethods(DataAction, AptifyApplication, User1, Database)

            Try

                If Not Me.IsPostBack Then
                    'RashmiP issue 6781
                    ' trPONum.Visible = False
                    hdnCCPartialNumber.Value = txtCCNumber.Text
                    chkBillMeLater.DataBind()
                    chkBillMeLater.Visible = False
                    lblBillMelater.Visible = False
                    lblBillMelater.Text = BillMeLaterDisplayText
                    ShowBillMeLater()
                    InitializeControlsValues()
                    LoadSavedPayments()
                    'Anil B for issue 10254 on 29-03-13
                    'set save for future use checkbox
                    chkSaveforFutureUse.Visible = False
                    BillmelaterCheck = chkBillMeLater.Checked
                End If
                ShowHideControls()  'Added By Sandeep for Issue 14671 on 20/02/2013
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub InitializeControlsValues()
            ' set default values
            'Anil B Issue 10254 on 20/04/2013
            ' For the year field, we load in the current year until +100 in the future
			dropdownYear.Items.Clear()
            For iYear = Now.Year To Now.Year + 7
                dropdownYear.Items.Add(iYear.ToString())
            Next
            CCExpireDate = DateAdd(DateInterval.Year, 1, Date.Now).ToShortDateString
            Me.txtCCNumber.Text = ""
            Me.txtCCSecurityNumber.Text = ""
            lblError.Text = ""
            cmbCreditCard.ClearSelection()
            lblSavedPayment.Visible = False
            cmbSavedPaymentMethod.Visible = False
            DisableCreditCard()
            cmbCreditCard.Visible = False
            chkSaveforFutureUse.Visible = False
        End Sub

        Private Sub vldExpirationDate_ServerValidate(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles vldExpirationDate.ServerValidate
            args.IsValid = IsDate(CCExpireDate)
        End Sub

        ''' <summary>
        ''' RashmiP issue 6781, 09/20/10
        ''' procedure set bill me later option true, if Company and User's credit Status is approved and credit limit is availabe 
        ''' contion check if payment type is Bill Me Later. Otherwise return False. 
        ''' </summary>
        Private Sub ShowBillMeLater()
            Dim sError As String
            Dim bSetVisible As Boolean
            'Suraj Issue 15014 4/24/13, if ATTRIBUTE_CONTORL_IsBillMeLaterDisable is True then remove the bill me later check box for fundraising page
            Try
                If m_iPOPaymentType > 0 Then
                    If CompanyCreditStatus = 2 AndAlso CompanyCreditLimit > 0 AndAlso CreditCheckLimit AndAlso ATTRIBUTE_CONTORL_IsBillMeLaterDisable = False Then
                        bSetVisible = True
                    ElseIf UserCreditStatus = 2 AndAlso UserCreditLimit > 0 AndAlso CreditCheckLimit AndAlso ATTRIBUTE_CONTORL_IsBillMeLaterDisable = False Then
                        bSetVisible = True
                    Else
                        bSetVisible = False
                    End If
                Else
                    bSetVisible = False
                End If
                chkBillMeLater.Visible = bSetVisible
                lblBillMelater.Visible = bSetVisible
            Catch ex As Exception

            End Try

        End Sub

        ''Rashmi, Issue 9024, Credit Card Validation
        'Anil B Issue 10254 on 20/04/2013
        Protected Sub txtCCNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                'Anil B Issue 10254 on 07-03-2013
                'If this Event call from Save payment dropedown then exit this event
                If bFromSavePaymentMethod OrElse cmbSavedPaymentMethod.SelectedIndex > 0 Then
                    Exit Sub
                End If
                Dim lPaymentID As Long
                Dim lCreditCard As Long
                lblError.Text = ""
                If txtCCNumber.Text = "" Then
                    cmbCreditCard.SelectedValue = "-1"
                    DisableCreditCard()
                Else
                    'Anil B for Credit Card recognization Performance on 21/jun/2013
                    If PaymentInfo IsNot Nothing AndAlso PaymentInfo.ValidateRange(Convert.ToString(txtCCNumber.Text), lPaymentID, lCreditCard) Then
                        'Author     :   Milind Sutar
                        'Date       :   01-July-2014
                        'Purpose    :   To get payment id by preferred currency type
                        Dim preferredCurrencyTypeID As String = IIf(String.IsNullOrEmpty(User1.GetValue("PreferredCurrencyTypeID")) = True, _
                                                                   0, User1.GetValue("PreferredCurrencyTypeID")).ToString()
                        Dim sql As String = "..spGetPaymentIdByCurrency__c @PreferredCurrencyTypeID=" & preferredCurrencyTypeID & ",@IINRange=" & "'" & Convert.ToString(txtCCNumber.Text) & "'"
                        Dim paymentID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sql))

                        'Anil B Issue 16167 on 07-05-2013
                        'Add Condition to check active payment card.
                        If paymentID > 0 AndAlso lCreditCard > 0 Then
                            cmbCreditCard.SelectedValue = CStr(paymentID)
                            SelectCardType(CStr(cmbCreditCard.SelectedItem.Text).Trim)
                            SaveForFutureUse(paymentID)
                            txtCCSecurityNumber.Focus()
                            lblError.Text = ""
                            'Anil B Issue 10254 on 20/04/2013
                            'Set CCNumber when card number is numeric
                            If IsNumeric(txtCCNumber.Text) Then
                                CCNumber = txtCCNumber.Text
                            End If
                            'cmbSavedPaymentMethod.SelectedIndex = 0
                        Else
                            cmbCreditCard.SelectedValue = "-1"
                            DisableCreditCard()
                            cmbCreditCard.Enabled = True
                            'Anil B Issue 16167 on 07-05-2013
                            'Displayed the error message
                            lblError.Text = "Invalid Card Type. Please try entering your card number again."
                            lblError.Visible = True
                        End If
                    Else
                        cmbCreditCard.SelectedValue = "-1"
                        SelectCardType("")
                        'Anil B Issue 10254 on 20/04/2013
                        'Displayed the error message
                        lblError.Text = "Card number does not match card type."
                        lblError.Visible = True
                        chkSaveforFutureUse.Visible = False
                    End If
                End If
                hdnCCPartialNumber.Value = txtCCNumber.Text
                upnlCreditCard.Update()
                upnlError.Update()
                txtCCSecurityNumber.Enabled = True
                ''Suraj Issue 16258 , 5/15/13 , set theCCSecurityNumber no
                If IsNumeric(txtCCSecurityNumber.Text) Then
                    CCSecurityNumber = txtCCSecurityNumber.Text
                End If
            Catch ex As Exception
            End Try
        End Sub

        'Anil B Issue 10254 on 07-03-2013
        'Function for Desabled credite card
        Private Sub DisableCreditCard()
            ImgAmex.ImageUrl = AmexDisabled
            'Commented by Kavita Zinage as per #13429
            'ImgDelta.ImageUrl = DeltaDisabled
            'ImgDiner.ImageUrl = DinerDisabled
            'ImgJcb.ImageUrl = JcbDisabled
            'ImgLaser.ImageUrl = LaserDisabled
            ImgMasterCard.ImageUrl = MasterCardDisabled
            'Commented by Kavita Zinage as per #13429
            'ImgSolo.ImageUrl = SoloDisabled
            'ImgSwitch.ImageUrl = SwitchDisabled
            ImgVisa.ImageUrl = VisaDisabled
            'Added by Kavita Zinage as per #13429
            ''ImgVisaDebit.ImageUrl = VisaDebitDisabled
        End Sub

        ''' <summary>
        ''' Rashmi P, Issue 10254, Apply Saved Payment Method in Ebusiness, 31/12/12
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' 
        Public Sub LoadSavedPayments()
            Dim DT As Data.DataTable
            Try


                DT = m_oGESPM.LoadSaveSavedPaymentMethods
                If Not DT Is Nothing AndAlso DT.Rows.Count > 0 Then
                    cmbSavedPaymentMethod.DataSource = DT
                    cmbSavedPaymentMethod.DataTextField = "DispalyName"
                    cmbSavedPaymentMethod.DataValueField = "ID"
                    cmbSavedPaymentMethod.DataBind()
                    'Anil B Issue 10254 
                    'Add Condition to set first blank item to savepayment dropdown
                    cmbSavedPaymentMethod.Items.Insert(0, New ListItem("- Select -", String.Empty))
                    lblSavedPayment.Visible = True
                    cmbSavedPaymentMethod.Visible = True
                    If cmbSavedPaymentMethod.Items.Count > 0 Then
                        cmbSavedPaymentMethod.SelectedIndex = 0
                        cmbSavedPaymentMethod_SelectedIndexChanged(Nothing, Nothing)
                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub cmbSavedPaymentMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSavedPaymentMethod.SelectedIndexChanged
            Try
                bFromSavePaymentMethod = True
                If cmbSavedPaymentMethod.Items.Count > 0 Then
                    lblError.Text = ""
                    If cmbSavedPaymentMethod.SelectedIndex > 0 Then
                        Dim oGESPM As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                        oGESPM = Me.AptifyApplication.GetEntityObject("PersonSavedPaymentMethods", CInt(cmbSavedPaymentMethod.SelectedValue))
                        If Not oGESPM Is Nothing Then
                            txtCCNumber.Text = CStr(oGESPM.GetValue("CCPartial"))
                            CCPartial = CStr(oGESPM.GetValue("CCPartial"))
                            cmbCreditCard.SelectedValue = CStr(oGESPM.GetValue("PaymentTypeID"))
                            'Anil B Issue 10254 on 20/04/2013
                            'Set payment typeid to property
                            PaymentTypeID = CLng((oGESPM.GetValue("PaymentTypeID")))
                            Dim sExpireDate As Date = CDate(oGESPM.GetValue("CCExpireDate"))
                            dropdownMonth.SelectedValue = CStr(sExpireDate.Month)
                            dropdownYear.SelectedValue = CStr(sExpireDate.Year)
                            CCNumber = CStr(oGESPM.GetValue("CCAccountNumber"))
                            'Anil B Issue 10254 on 07-03-2013
                            'Set Reference Transaction to the property
                            ReferenceTransactionNumber = CStr(oGESPM.GetValue("ReferenceTransactionNumber"))
                            'Anil B for issue 10254 on 29-03-13
                            'Check date is blank
                            If Not IsDBNull(oGESPM.GetValue("ReferenceExpiration")) AndAlso CStr(oGESPM.GetValue("ReferenceExpiration")).Trim <> "" Then
                                ReferenceExpiration = CDate(oGESPM.GetValue("ReferenceExpiration"))
                            End If
                            SelectCardType(cmbCreditCard.SelectedItem.Text.Trim)
                            SaveForFutureUse(CInt(oGESPM.GetValue("PaymentTypeID")))
                            SetEnableDesable(False)
                        End If
                    Else
                        'Anil B Issue 10254 on 07-03-2013
                        'Set Control
                        SetEnableDesable(True)
                        DisableCreditCard()
                        txtCCNumber.Text = ""
                        'Anil B Issue 10254 on 20/04/2013
                        'Reset control values
                        txtCCSecurityNumber.Text = ""
                        dropdownMonth.SelectedIndex = Today.Month - 1
                        dropdownYear.SelectedIndex = (Today.Year + 1) - Now.Year
                    End If
                End If
                hdnCCPartialNumber.Value = txtCCNumber.Text
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Anil B Issue 10254 on 07-03-2013
        'Set Control Enabled desabled
        Private Sub SetEnableDesable(ByVal Flag As Boolean)
            'Anil B Issue 10254 on 20/04/2013
            'Set CCnumber readonly
            txtCCNumber.ReadOnly = Not Flag
            dropdownMonth.Enabled = Flag
            dropdownYear.Enabled = Flag
            chkSaveforFutureUse.Visible = False
        End Sub
        ''' <summary>
        ''' Rashmi P, Issue 10737
        ''' Select Payment Type
        ''' </summary>
        ''' <param name="PaymentType"></param>
        ''' <remarks></remarks>
        Public Sub SelectCardType(ByVal PaymentType As String)

            ImgAmex.ImageUrl = AmexDisabled
            'Commented by Kavita Zinage 13/05/2016 - as per #13429
            'ImgDelta.ImageUrl = DeltaDisabled
            'ImgDiner.ImageUrl = DinerDisabled
            'ImgJcb.ImageUrl = JcbDisabled

            'ImgLaser.ImageUrl = LaserDisabled
            ImgMasterCard.ImageUrl = MasterCardDisabled

            'Commented by Kavita Zinage 13/05/2016 - as per #13429
            'ImgSolo.ImageUrl = SoloDisabled
            'ImgSwitch.ImageUrl = SwitchDisabled

            ImgVisa.ImageUrl = VisaDisabled

            'Added by Kavita Zinage 13/05/2016 - as per #13429
            ''ImgVisaDebit.ImageUrl = VisaDebitDisabled

            Select Case (PaymentType)
                Case "Amex"
                    ImgAmex.ImageUrl = AmexEnabled
                    ImgAmex.DataBind()

                    'Commented by Kavita Zinage 13/05/2016 - as per #13429
                    'Case "Delta"
                    '    ImgDelta.ImageUrl = DeltaEnabled
                    '    ImgDelta.DataBind()
                    'Case "Diners"
                    '    ImgDiner.ImageUrl = DinerEnabled
                    '    ImgDiner.DataBind()
                    'Case "Jcb"
                    '    ImgJcb.ImageUrl = JcbEnabled
                    '    ImgJcb.DataBind()

                    'Case "Laser"
                    '    ImgLaser.ImageUrl = LaserEnabled
                    '    ImgLaser.DataBind()
                Case "MasterCard"
                    ImgMasterCard.ImageUrl = MasterCardEnabled
                    ImgMasterCard.DataBind()
		 'Sachin Sathe made changes for redmine #15697
                Case "MC"
                    ImgMasterCard.ImageUrl = MasterCardEnabled
                    ImgMasterCard.DataBind()
                    'End Sachin's Code		


                    'Commented by Kavita Zinage 13/05/2016 - as per #13429
                    'Case "Solo"
                    '    ImgSolo.ImageUrl = SoloEnabled
                    '    ImgSolo.DataBind()
                    'Case "Switch"
                    '    ImgSwitch.ImageUrl = SwitchEnabled
                    '    ImgSwitch.DataBind()

                Case "Visa"
                    ImgVisa.ImageUrl = VisaEnabled
                    ImgVisa.DataBind()
                    'Added by Kavita Zinage 13/05/2016 - as per #13429
                Case "Visa Debit"
                    ''ImgVisaDebit.ImageUrl = VisaDebitEnabled
                    ''ImgVisaDebit.DataBind()

            End Select

        End Sub

        ''' <summary>
        ''' Rashmi P, 10737
        ''' Funtion check and Return True if AllowSaveforFutureUse is true for Payment Type
        ''' </summary>        
        Private Sub SaveForFutureUse(ByVal PaymentTypeID As Integer)
            Try
                Dim sSql As String
                Dim params(0) As IDataParameter
                Dim dt As DataTable
                sSql = Database & ".." & "spGetPaymentType"
                params(0) = Me.DataAction.GetDataParameter("@ID", SqlDbType.Int, PaymentTypeID)
                dt = Me.DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, params)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    'Anil B for issue 15374 on 02/04/2013
                    'Set save for future use checkbox invisible for some pages
                    If CBool(dt.Rows(0).Item("AllowSaveforFutureUse")) = True AndAlso SetchkSaveforFutureUse = True Then
                        chkSaveforFutureUse.Visible = True
                    Else
                        chkSaveforFutureUse.Visible = False
                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Added By Sandeep for Issue 14671 on 20/02/2013
        Protected Sub chkBillMeLater_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBillMeLater.CheckedChanged
            ShowHideControls()
            'Anil B for issue 10254 on 29-03-13
            'Set control value
            If chkBillMeLater.Checked = False Then
                'Anil B Issue 10254 on 20/04/2013
                'check if SPM dropdown have items
                If cmbSavedPaymentMethod.Items.Count > 0 Then
                    cmbSavedPaymentMethod.SelectedIndex = 0
                End If
                dropdownMonth.SelectedIndex = Today.Month - 1
                dropdownYear.SelectedIndex = (Today.Year + 1) - Now.Year
                dropdownMonth.Enabled = True
                dropdownYear.Enabled = True
                chkSaveforFutureUse.Checked = False
                tdReceiptlbl.Visible = False
                tdReceiptNo.Visible = False
            Else
                If Not Session("OrderuniqueGuid") Is Nothing AndAlso Convert.ToString(Session("OrderuniqueGuid")) <> "" Then
                    lblReceiptNo.Text = Convert.ToString(Session("OrderuniqueGuid"))
                    tdReceiptlbl.Visible = True
                    tdReceiptNo.Visible = True
                End If
            End If
            BillmelaterCheck = chkBillMeLater.Checked
        End Sub

        'Added By Sandeep for Issue 14671 on 20/02/2013
        Protected Sub ShowHideControls()
            If chkBillMeLater.Checked = True Then
                RequiredFieldValidator1.Enabled = False
                RequiredFieldValidator2.Enabled = False
                tblMain.Visible = False
                'Changed by Rajesh for Firm Portal Config
                'tblPONum.Visible = True
                tblPONum.Visible = False
                tdReceiptlbl.Visible = True
                tdReceiptNo.Visible = True
            Else
                tblMain.Visible = True
                tblPONum.Visible = False
                RequiredFieldValidator1.Enabled = True
                RequiredFieldValidator2.Enabled = True
                chkSaveforFutureUse.Visible = False
                tdReceiptlbl.Visible = False
                tdReceiptNo.Visible = False
            End If
        End Sub

    End Class
End Namespace
