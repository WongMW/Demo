Option Explicit On
Option Strict On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.Security
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.HttpApplicationState
Imports System.Web

Namespace Aptify.Framework.Web.eBusiness
    ''' <summary>
    ''' Aptify Credit Card User Control for e-Business
    ''' </summary>
    ''' <remarks></remarks>
    Partial Class NonDuesFirmbillingPaymentControl__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "CreditCard"
        Protected Const ATTRIBUTE_BILL_ME_LATER As String = "BillMeLaterDisplayText"


        Dim m_iPOPaymentType As Integer = 0
        Dim m_iUserCreditStatus As Integer = 0
        Dim m_lUserCreditLimit As Long = 0
        Dim m_iCompanyCreditStatus As Integer = 0
        Dim m_lCompanyCreditLimit As Long = 0
        Dim m_bCreditChecklimit As Boolean = False
        Dim PaymentID As Integer

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
            If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID")) Then
                m_iPOPaymentType = CInt(AptifyApplication.GetEntityAttribute("Web Shopping Carts", "POPaymentTypeID"))
            End If

        End Sub

#Region "Credit Card Control Properties"
        ''' <summary>
        ''' TransactionNumber
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TransactionNumber() As String
            Get
                Return txtTransactionNo.Text
            End Get
            Set(ByVal Value As String)
                txtTransactionNo.Text = Value
            End Set
        End Property
        ''' <summary>
        ''' Bank Name
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BankName() As String
            Get
                Return txtBank.Text
            End Get
            Set(ByVal Value As String)
                txtBank.Text = Value
            End Set
        End Property
        ''' <summary>
        ''' Account Number
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AccountNumber() As String
            Get
                Return txtAccountNo.Text
            End Get
            Set(ByVal Value As String)
                txtAccountNo.Text = Value
            End Set
        End Property
        ' ''' <summary>
        ' ''' Routing Number
        ' ''' </summary>
        ' ''' <value></value>
        ' ''' <returns></returns>
        ' ''' <remarks></remarks>
        'Public Property RoutingNumber() As String
        '    Get
        '        Return txtRoutingNo.Text
        '    End Get
        '    Set(ByVal Value As String)
        '        txtRoutingNo.Text = Value
        '    End Set
        'End Property
        ''' <summary>
        ''' Name of Branch
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BranchName() As String
            Get
                Return txtBranchName.Text
            End Get
            Set(ByVal Value As String)
                txtBranchName.Text = Value
            End Set
        End Property
        ''' <summary>
        ''' ABA
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ABA() As String
            Get
                Return txtABA.Text
            End Get
            Set(ByVal Value As String)
                txtABA.Text = Value
            End Set
        End Property
        ''' <summary>
        ''' Name of Account
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property NameOfAccount() As String
            Get
                Return txtNameOfAccount.Text
            End Get
            Set(ByVal Value As String)
                txtNameOfAccount.Text = Value
            End Set
        End Property
        ''' <summary>
        ''' Credit Card Number
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CCNumber() As String
            Get
                EnsureChildControls()
                Return txtCCNumber.Text
            End Get
            Set(ByVal Value As String)
                EnsureChildControls()
                txtCCNumber.Text = Value
            End Set
        End Property

        ''' <summary>
        ''' Credit Card Security Number
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
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
                dExpDate = CDate(dropdownMonth.SelectedValue & "/1/" & _
                                   dropdownYear.SelectedValue)
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

        ''' <summary>
        ''' Payment Type ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
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
#End Region

        Public Sub LoadCreditCardInfo()
            Dim sSQL As Text.StringBuilder = New Text.StringBuilder
            Dim dt As Data.DataTable
            Try

                'Ansar Shaikh - Issue 11986 - 12/27/2011
                sSQL.Append("select ID, Name, TYPE from vwPaymentTypes WHERE Active=1 AND WebEnabled=1  ORDER BY Name")

                dt = DataAction.GetDataTable(sSQL.ToString)
                cmbCreditCard.DataSource = dt
                cmbCreditCard.DataTextField = "Name"
                cmbCreditCard.DataValueField = "ID"
                cmbCreditCard.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Clear Method
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            cmbCreditCard.ClearSelection()
            txtTransactionNo.Text = ""
            txtAccountNo.Text = ""
            txtBank.Text = ""
            txtABA.Text = ""
            txtBranchName.Text = ""
            txtNameOfAccount.Text = ""
            '  txtRoutingNo.Text = ""
            PaymentTypeSelection1.Visible = False
            PaymentTypeSelection.Visible = False
        End Sub
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            Dim iDay As Integer
            Dim iYear As Integer

            Try

                If Not Me.IsPostBack Then
                    ' Load in base values to the expiration date combo boxes
                    'For iDay = 1 To 31
                    '    dropdownDay.Items.Add(iDay.ToString())
                    'Next

                    PaymentTypeSelection1.Visible = False
                    ' For the year field, we load in the current year until +15 in the future
                    For iYear = Now.Year To Now.Year + 15
                        dropdownYear.Items.Add(iYear.ToString())
                    Next

                    ' set default values
                    CCExpireDate = DateAdd(DateInterval.Year, 1, Date.Now).ToShortDateString
                    Me.txtCCNumber.Text = ""
                    Me.txtCCSecurityNumber.Text = ""
                    'RashmiP issue 6781
                    ' trPONum.Visible = False
                    chkBillMeLater.Attributes.Add("onclick", "ShowHideControls();")
                    chkBillMeLater.DataBind()
                    chkBillMeLater.Visible = False
                    lblBillMelater.Visible = False
                    lblBillMelater.Text = BillMeLaterDisplayText
                    ShowBillMeLater()

                    PaymentTypeSelection1.Visible = False
                    PaymentTypeSelection.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
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
            Dim sError As String = String.Empty
            Dim bSetVisible As Boolean

            Try
                If m_iPOPaymentType > 0 Then
                    If CompanyCreditStatus = 2 AndAlso CompanyCreditLimit > 0 AndAlso CreditCheckLimit Then
                        bSetVisible = True
                    ElseIf UserCreditStatus = 2 AndAlso UserCreditLimit > 0 AndAlso CreditCheckLimit Then
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
        Protected Sub cmbCreditCard_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCreditCard.SelectedIndexChanged

            Dim sqlStr As String = "spGetPayType"

            Try

                If cmbCreditCard.SelectedItem.Value = "---Select---" Then
                    PaymentTypeSelection1.Visible = False
                    PaymentTypeSelection.Visible = False
                Else
                    If Not cmbCreditCard.SelectedItem Is Nothing Then
                        PaymentID = CInt(cmbCreditCard.SelectedItem.Value)
                    End If
                    Dim param(0) As IDataParameter
                    param(0) = DataAction.GetDataParameter("ID", SqlDbType.Int, PaymentID)
                    Dim PayTye As String = Convert.ToString(DataAction.ExecuteScalarParametrized(sqlStr, CommandType.StoredProcedure, param))
                    If PayTye.ToLower = "wire transfer" Then
                        PaymentTypeSelection1.Visible = True
                        PaymentTypeSelection.Visible = False

                    Else
                        PaymentTypeSelection.Visible = True
                        PaymentTypeSelection1.Visible = False

                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

           
        End Sub
    End Class
End Namespace
