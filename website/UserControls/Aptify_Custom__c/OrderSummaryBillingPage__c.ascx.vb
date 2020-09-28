'-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
'DEVELOPER                              DATE                                        Comments
'Ganesh I                               23/03/2014                              For changing label Tax to VAT
'----------------------------------------------------------------------------------------------------------------------------------------------------------------------

'Aptify e-Business 5.5.1, July 2013
Option Explicit On
Option Strict On

Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.Application

Namespace Aptify.Framework.Web.eBusiness.ProductCatalog
    Partial Class OrderSummaryBillingControl
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "OrderSummaryBillingControl"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
        End Sub
        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
        End Sub

        Private Property ShoppingCart() As Aptify.Framework.Web.eBusiness.AptifyShoppingCart
            Get
                If Context.Items("ShoppingCartCtrl") IsNot Nothing Then
                    Return TryCast(Context.Items("ShoppingCartCtrl"), Aptify.Framework.Web.eBusiness.AptifyShoppingCart)
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal value As Aptify.Framework.Web.eBusiness.AptifyShoppingCart)
                Context.Items("ShoppingCartCtrl") = value
            End Set
        End Property

        Public Overridable Sub Refresh()
            Try
                Dim sc As Aptify.Framework.Web.eBusiness.AptifyShoppingCart
                If Me.ShoppingCart IsNot Nothing Then
                    sc = Me.ShoppingCart
                Else
                    sc = ShoppingCart1
                End If
                  Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                oOrder = sc.GetOrderObject(Session, Page.User, Page.Application)
                With sc
                    lblSubTotal.Text = Format$(.SubTotal, .GetCurrencyFormat(.CurrencyTypeID))
                    lblShipping.Text = Format$(.ShippingAndHandlingCharges, .GetCurrencyFormat(.CurrencyTypeID))
                    lblTax.Text = Format$(.Tax, .GetCurrencyFormat(.CurrencyTypeID))
                    lblTotal.Text = Format$(.GrandTotal, .GetCurrencyFormat(.CurrencyTypeID))
                    Dim iSalestax As Integer = Convert.ToInt32(oOrder.SalesTaxAmounts.Keys(0))
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
                        lblTax.Text = Format$((.Tax - dSalesTaxAmt), .GetCurrencyFormat(.CurrencyTypeID))
                        lblShipping.Text = Format$((.ShippingAndHandlingCharges + dSalesTaxAmt), .GetCurrencyFormat(.CurrencyTypeID))
                    End If
                     ' Below coded added for web shopping cart changes
                    ''Dim sSQL As String = Database & "..spCheckCompanyExemptTax__c @CompanyID=" & User1.CompanyID
                    ''Dim bCompanyExempt As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    ''If bCompanyExempt Then
                        ''lblShipping.Text = Format$((.ShippingAndHandlingCharges + .Tax), .GetCurrencyFormat(.CurrencyTypeID))
                        ''lblTax.Text = Format$(0, .GetCurrencyFormat(.CurrencyTypeID))
                    ''End If
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
            'Anil B for Credit Card recognization Performance on 21/jun/2013
            If Not IsPostBack Then
                Refresh()
            End If
        End Sub
    End Class
End Namespace
