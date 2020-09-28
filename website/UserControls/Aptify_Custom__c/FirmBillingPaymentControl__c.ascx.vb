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
    Partial Class FirmBillingPaymentControl__c
        Inherits BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "FirmBillingPaymentControl__c"
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
        ''' Payment Type ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PaymentTypeID() As Long
            Get
                EnsureChildControls()

                If cmbFirmBillingPayment.SelectedItem Is Nothing Then
                    Return -1
                Else
                    Return CLng(cmbFirmBillingPayment.SelectedItem.Value)
                End If

            End Get
            Set(ByVal Value As Long)
                Try
                    EnsureChildControls()
                    Dim oItem As System.Web.UI.WebControls.ListItem
                    oItem = cmbFirmBillingPayment.Items.FindByValue(CStr(Value))
                    If Not oItem Is Nothing Then
                        oItem.Selected = True
                    End If
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            End Set
        End Property
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
        ''' Check Number
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CheckNumber() As String
            Get
                Return txtCheckNo.Text
            End Get
            Set(ByVal Value As String)
                txtCheckNo.Text = Value
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
        Public Property NameOfAccount() As String
            Get
                Return txtNameOfAccount.Text
            End Get
            Set(ByVal Value As String)
                txtNameOfAccount.Text = Value
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
#End Region
        Public Sub LoadPaymentTypeInfo()
            Dim sSQL As String = String.Empty
            Dim dt As Data.DataTable
            Try
                ' parag bodhe - 10/19/2012 - used to bind the payment type data to dropdownbox.
                sSQL = "select ID, Name, TYPE from vwPaymentTypes WHERE Active=1 AND WebEnabled=1  ORDER BY Name"
                dt = DataAction.GetDataTable(sSQL)
                cmbFirmBillingPayment.DataSource = dt
                cmbFirmBillingPayment.DataTextField = "Name"
                cmbFirmBillingPayment.DataValueField = "ID"
                cmbFirmBillingPayment.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Clear Method
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            cmbFirmBillingPayment.ClearSelection()
            txtTransactionNo.Text = ""
            txtCheckNo.Text = ""
            txtAccountNo.Text = ""
            txtBank.Text = ""
            txtABA.Text = ""
            txtBranchName.Text = ""
            txtNameOfAccount.Text = ""
            '   txtRoutingNo.Text = ""
            trTranctions.Visible = False
            trCheckDetails.Visible = False
            trBnak.Visible = False
            trAccName.Visible = False
            '   trRouteDetails.Visible = False
            trAccountInfo.Visible = False
            trBranchDetails.Visible = False
            trABA.Visible = False
        End Sub
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not IsPostBack Then
                LoadPaymentTypeInfo()
            End If
            Try

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub cmbFirmBillingPayment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            If cmbFirmBillingPayment.SelectedIndex > 0 Then
                Dim sqlStr As String = "spGetPayType"
                If Not cmbFirmBillingPayment.SelectedItem Is Nothing Then
                    PaymentID = CInt(cmbFirmBillingPayment.SelectedItem.Value)
                End If
                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("ID", SqlDbType.Int, PaymentID)
                Dim PayTye As String = Convert.ToString(DataAction.ExecuteScalarParametrized(sqlStr, CommandType.StoredProcedure, param))

                If PayTye.ToLower = "wire transfer" Then
                    trTranctions.Visible = True
                    trCheckDetails.Visible = False
                    trAccountInfo.Visible = True
                    '   trRouteDetails.Visible = True
                    trAccName.Visible = True
                    trBranchDetails.Visible = True
                    trABA.Visible = True
                    trBnak.Visible = True
                ElseIf PayTye.ToLower = "check" Then
                    trTranctions.Visible = False
                    trCheckDetails.Visible = True

                    trBnak.Visible = True
                    trAccountInfo.Visible = True
                    '  trRouteDetails.Visible = False
                    trAccName.Visible = False
                    trBranchDetails.Visible = True
                    trABA.Visible = True
                ElseIf PayTye.ToLower = "purchase order" Then
                    trTranctions.Visible = False
                    trCheckDetails.Visible = False

                    trBnak.Visible = False
                    trAccountInfo.Visible = False
                    '  trRouteDetails.Visible = False
                    trAccName.Visible = False
                    trBranchDetails.Visible = False
                    trABA.Visible = False

                ElseIf PayTye.ToLower = "visa" Then
                    trTranctions.Visible = False
                    trCheckDetails.Visible = False

                    trBnak.Visible = False
                    trAccountInfo.Visible = True
                    '  trRouteDetails.Visible = False
                    trAccName.Visible = True
                    trBranchDetails.Visible = False
                    trABA.Visible = False
                End If
            Else
                Clear()

            End If
           
           
        End Sub
    End Class
End Namespace
