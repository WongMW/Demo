''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date Created/Modified               Summary
'Govind                     9th Apr 2014                        Added code for training ticket pop-up
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On
Imports Aptify.Framework.DataServices
Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.ProductCatalog
    Partial Class ViewCartControl__c
        Inherits BaseUserControlAdvanced


        Protected Const ATTRIBUTE_CONTINUE_SHOPPING_BUTTON_PAGE As String = "ContinueShoppingButtonPage"
        Protected Const ATTRIBUTE_CHECKOUT_BUTTON_PAGE As String = "CheckOutButtonPage"
        Protected Const ATTRIBUTE_SAVE_CART_BUTTON_PAGE As String = "SaveCartButtonPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "ViewCartPage"
        Protected Const ATTRIBUTE_SAVING_MESSAGE As String = "SavingMessage"
        Dim ProductIdLIst As String = String.Empty


#Region "ViewCart Specific Properties"
        ''' <summary>
        ''' ContinueShoppingButton page url
        ''' </summary>
        Public Overridable Property ContinueShoppingButtonPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTINUE_SHOPPING_BUTTON_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTINUE_SHOPPING_BUTTON_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTINUE_SHOPPING_BUTTON_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' CheckOutButton page url
        ''' </summary>
        Public Overridable Property CheckOutButtonPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CHECKOUT_BUTTON_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CHECKOUT_BUTTON_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CHECKOUT_BUTTON_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' SaveCartButton page url
        ''' </summary>
        Public Overridable Property SaveCartButtonPage() As String
            Get
                If Not ViewState(ATTRIBUTE_SAVE_CART_BUTTON_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_SAVE_CART_BUTTON_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_SAVE_CART_BUTTON_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''RashmiP, issue 10021
        Dim m_sSavingMsg As String
        Public Overridable Property SavingMessage() As String
            Get
                Return m_sSavingMsg
            End Get
            Set(ByVal value As String)
                m_sSavingMsg = value
            End Set
        End Property

        Public Overridable Property ViewCartPage() As String
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

#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(ContinueShoppingButtonPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ContinueShoppingButtonPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTINUE_SHOPPING_BUTTON_PAGE)
                If String.IsNullOrEmpty(ContinueShoppingButtonPage) Then
                    Me.cmdShop.Enabled = False
                    Me.cmdShop.ToolTip = "ContinueShoppingButtonPage property has not been set."
                End If
            End If
            If String.IsNullOrEmpty(CheckOutButtonPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                CheckOutButtonPage = Me.GetLinkValueFromXML(ATTRIBUTE_CHECKOUT_BUTTON_PAGE)
                If String.IsNullOrEmpty(CheckOutButtonPage) Then
                    Me.cmdCheckOut.Enabled = False
                    Me.cmdCheckOut.ToolTip = "CheckOutButtonPage property has not been set."
                End If
            End If
            If String.IsNullOrEmpty(SaveCartButtonPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                SaveCartButtonPage = Me.GetLinkValueFromXML(ATTRIBUTE_SAVE_CART_BUTTON_PAGE)
                If String.IsNullOrEmpty(SaveCartButtonPage) Then
                    Me.cmdSaveCart.Enabled = False
                    Me.cmdSaveCart.ToolTip = "SaveCartButtonPage property has not been set."
                End If
            End If
            ''RashmiP
            If Not String.IsNullOrEmpty(Me.GetPropertyValueFromXML(ATTRIBUTE_SAVING_MESSAGE)) Then
                SavingMessage = Me.GetPropertyValueFromXML(ATTRIBUTE_SAVING_MESSAGE)
            End If
            If String.IsNullOrEmpty(ViewCartPage) Then
                ViewCartPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTORL_DEFAULT_NAME)
            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            Try
                If Not IsPostBack Then
                    SetProperties()
                    '  Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity

                    If CLng(Request.QueryString("ShoppingCartID")) > 0 Then
                        'HP Issue#9078: only load if the Cart belongs to the logged-in user
                        If CartBelongsToUser(CLng(Request.QueryString("ShoppingCartID"))) Then
                            '    'Load existing shopping cart.  
                            CartGrid.LoadCart(CLng(Request.QueryString("ShoppingCartID")))
                            'Commented by Dipali 04/07/2017-----MOve to checkout
                            'oOrder = CartGrid.Cart.GetOrderObject(Session, Page.User, Page.Application)
                            'Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                            'Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            'If iFirmAdmin > 0 Then
                            '    oOrder.SetValue("FirmPay__c", 1)
                            '    If oOrder.SubTypes("OrderLines") IsNot Nothing AndAlso oOrder.SubTypes("OrderLines").Count > 0 Then
                            '        For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                            '            oOrder.SubTypes("OrderLines").Item(i).SetValue("BillToCompanyID__c", oOrder.BillToCompanyID)
                            '        Next
                            '    End If

                            'End If
                            'CartGrid.Cart.SaveCart(Session)
                            '---------End
                            RefreshGrid()
                        Else
                            'Commented by Dipali 04/07/2017-----MOve to checkout
                            'oOrder = CartGrid.Cart.GetOrderObject(Session, Page.User, Page.Application)
                            'Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                            'Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            'If iFirmAdmin > 0 Then
                            '    oOrder.SetValue("FirmPay__c", 1)
                            '    If oOrder.SubTypes("OrderLines") IsNot Nothing AndAlso oOrder.SubTypes("OrderLines").Count > 0 Then
                            '        For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                            '            oOrder.SubTypes("OrderLines").Item(i).SetValue("BillToCompanyID__c", oOrder.BillToCompanyID)
                            '        Next
                            '    End If
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
                            'CartGrid.Cart.SaveCart(Session)
                            '---------End
                            RefreshGrid()
                        End If
                    Else
                        'Commented by Dipali 04/07/2017-----MOve to checkout
                        'oOrder = CartGrid.Cart.GetOrderObject(Session, Page.User, Page.Application)
                        'Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                        'Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        'If iFirmAdmin > 0 Then
                        '    oOrder.SetValue("FirmPay__c", 1)
                        '    If oOrder.SubTypes("OrderLines") IsNot Nothing AndAlso oOrder.SubTypes("OrderLines").Count > 0 Then
                        '        For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                        '            oOrder.SubTypes("OrderLines").Item(i).SetValue("BillToCompanyID__c", oOrder.BillToCompanyID)
                        '        Next
                        '    End If
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
                        'CartGrid.Cart.SaveCart(Session)
                        '---------End
                        RefreshGrid()
                    End If
                    UpdateCampaignDisplay()
                    LoadShipmentType()
                    'Added By dipali to get product list in cart
                    GetProductLIst()
                    'need to optimize
                    LoadTopupCampaign()
                    'Commented 
                    'lblTopupMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ViewCart.SelectToupCampaign")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
                ''Code added by GM for Redmine #19343 
                If TrainingTicketCampaignApply() = False Then
                    '' cmdRemoveCampaign_Click(sender, e) 'Commeted code by GM 
                    '' Updated by GM Start on 9 Oct 2018 for Redmine #19343
                    CartGrid.Cart.RemoveOrderCampaign(Page.Session)
                    RemoveAddTrainingProduct() ' Code added by govind
                    lblCampaignMsg.Text = ""
                    RefreshGrid()
                    UpdateCampaignDisplay()
                    '' End Redmine #19343
                End If
                ''END Redmine #19343
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'HP Issue#9078: return true if shoppingCart ID provided belongs to the logged-in user
        Private Function CartBelongsToUser(ByVal id As Long) As Boolean
            Dim sSQL As String
            Dim value As String

            sSQL = "SELECT Name FROM " & Database & "..vwWebShoppingCarts " &
                   "WHERE WebUserID = " & User1.UserID & " AND ID = " & CLng(Request.QueryString("ShoppingCartID"))
            value = CStr(DataAction.ExecuteScalar(sSQL))

            Return Not String.IsNullOrEmpty(value)
        End Function

        Private Sub RefreshGrid()
            Try
                CartGrid.RefreshGrid()
                'Navin Prasad Issue 11032
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                oOrder = CartGrid.Cart.GetOrderObject(Session, Page.User, Page.Application)
                If CartGrid.Grid.Items.Count > 0 Then
                    tblRowNoItems.Visible = False
                    cmdUpdateCart.Visible = True
                    cmdCheckOut.Visible = True
                    cmdSaveCart.Visible = True
                Else
                    tblRowNoItems.Visible = True
                    cmdUpdateCart.Visible = False
                    cmdCheckOut.Visible = False
                    cmdSaveCart.Visible = False
                    divTotals.Visible = False
                    divCampaign.Visible = False
                    'divhr.Visible = False
                    tblbuttons.Visible = False
                End If
                Dim sCurrencyFormat As String
                With CartGrid.Cart
                    sCurrencyFormat = .GetCurrencyFormat(.CurrencyTypeID)
                    lblSubTotal.Text = Format$(.SubTotal, sCurrencyFormat)
                    lblShipping.Text = Format$(.ShippingAndHandlingCharges, sCurrencyFormat)
                    lblTax.Text = Format$(.Tax, sCurrencyFormat)
                    lblGrandTotal.Text = Format$(.GrandTotal, sCurrencyFormat)
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
                    'Only check if savings exist if user is logged in
                    If User1.PersonID > 0 Then
                        '20090126 MAS: using different logic for calculating Member Savings since the original way was not properly
                        '              calculating complex pricing rules.
                        '              NOTE: member savings can only be calculated for a User that is logged into the website, 
                        '                    since pricing may be based on the User's address.
                        Dim dSavings As Decimal
                        dSavings = .GetCartMemberSavings(Page.Session, Page.User, Page.Application)
                        If dSavings > 0 Then
                            spnSavings.Visible = True
                            If Not String.IsNullOrEmpty(SavingMessage) Then
                                SavingMessage = SavingMessage.Replace("{0}", Format$(dSavings, sCurrencyFormat))
                                lblTotalSavings.Text = SavingMessage
                            Else
                                lblTotalSavings.Text = "You have saved " + Format$(dSavings, sCurrencyFormat) + "   in your shopping cart since you are a valued member!"
                            End If
                        Else
                            spnSavings.Visible = False
                        End If
                    Else
                        spnSavings.Visible = False
                    End If
                End With

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub cmdUpdateCart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdateCart.Click
            ' update the shopping cart object
            ' Iterate through all rows within shopping cart list
            Try

                CartGrid.UpdateCart()
                RefreshGrid()
                TrainingTicketRemoveOrderline()
                'Added by Dipali To get Product list 04/07/2017
                GetProductLIst()
                LoadTopupCampaign()
                MyBase.Response.Redirect(ViewCartPage, False)
                Return

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Added by Govind M
        Private Sub TrainingTicketRemoveOrderline()
            Try
                Dim bTRainingTicket As Boolean = False
                If Not Session("IsTrainingTicketCampaign") Is Nothing AndAlso Convert.ToBoolean(Session("IsTrainingTicketCampaign")) = True Then
                    Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                    oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
                    For Each oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrder.SubTypes("OrderLines")
                        Dim dUnitPrice As Decimal

                        Dim sSqlCheckProdCat As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductCategoryForTrainingTicket__c @ProductID=" & Convert.ToInt32(oOrderLine.GetValue("ProductID"))
                        Dim lProdCategoryID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSqlCheckProdCat, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If lProdCategoryID > 0 Then
                            bTRainingTicket = True
                            Exit For
                        End If
                    Next
                    If bTRainingTicket = False Then
                        CartGrid.Cart.RemoveOrderCampaign(Page.Session)
                        Session("IsTrainingTicketCampaign") = False
                        lblCampaignMsg.Text = ""
                        RefreshGrid()
                        lblCampaignMsg.Text = ""
                        cmdApplyCampaign.Visible = True
                        lblCampaignInstructions.Visible = True
                        cmdRemoveCampaign.Visible = False
                        txtCampaign.Visible = True
                    End If

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

            End Try
        End Sub
        Private Sub cmdShop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShop.Click
            Response.Redirect(ContinueShoppingButtonPage)
            Return
        End Sub

        Private Sub cmdCheckOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckOut.Click
            Try


                'Condition added by dipali  04/06/2017 f 
                If (txtCampaign.Text.Trim = "" Or CartGrid.Cart.CampaignCodeID = -1) Then
                    'Need to move in load method and set viewstate
                    If CheckTrainingTicketApplyTopupCode() Then
                        CartGrid.SaveCart() 'Stock code
                        Response.Redirect(CheckOutButtonPage, False)
                        Return
                    End If
                Else
                    If TrainingTicketCampaignApply() Then
                        ' check training ticket product top up campaign apply or not
                        If CheckTrainingTicketApplyTopupCode() Then
                            CartGrid.SaveCart() 'Stock code
                            Response.Redirect(CheckOutButtonPage, False) 'Stock code
                            Return
                        End If
                    Else
                        CartGrid.SaveCart() 'Stock code
                        Response.Redirect(CheckOutButtonPage, False) 'Stock code
                        Return
                    End If

                End If

                ''CartGrid.UpdateCart()
                '' Following Condition Added by Govind
                'If TrainingTicketCampaignApply() Then
                '    ' check training ticket product top up campaign apply or not
                '    If CheckTrainingTicketApplyTopupCode() Then
                '        CartGrid.SaveCart() 'Stock code
                '        Response.Redirect(CheckOutButtonPage, False) 'Stock code
                '    End If
                'End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub cmdSaveCart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveCart.Click
            Try
                If CLng(Request.QueryString("ShoppingCartID")) > 0 And CartBelongsToUser(CLng(Request.QueryString("ShoppingCartID"))) Then
                    'Response.Redirect("SaveCart.aspx?ShoppingCartID=" & Request.QueryString("ShoppingCartID"))
                    Response.Redirect(SaveCartButtonPage & "?ShoppingCartID=" & CStr(Request.QueryString("ShoppingCartID")))
                    Return
                ElseIf (CartGrid.Cart.CartID > 0) Then
                    Response.Redirect(SaveCartButtonPage & "?ShoppingCartID=" & CStr(CartGrid.Cart.CartID))
                    Return
                Else
                    Response.Redirect(SaveCartButtonPage)
                    Return
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub cmdApplyCampaign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApplyCampaign.Click
            Try
                If TrainingTicketCampaignApply() Then
                    If Not CartGrid.Cart.SetOrderCampaign(txtCampaign.Text, Page.Session) Then
                        lblCampaignMsg.Text = "Could not associate campaign code with order."
                    Else
                        lblCampaignMsg.Text = ""
                    End If
                    CartGrid.UpdateCart()
                    RefreshGrid()
                    'Added By dipali 04/07/2017 to get list of product in grid
                    GetProductLIst()
                    UpdateCampaignDisplay(True)

                    'Added by Dipali 04/07/2017
                    If (Session("IsTrainingTicket") IsNot Nothing) Then
                        If Convert.ToInt32(Session("IsTrainingTicket")) = 1 Then
                            Session("IsTrainingTicketCampaign") = True
                        Else
                            Session("IsTrainingTicketCampaign") = False
                        End If
                    Else
                        Session("IsTrainingTicketCampaign") = False
                    End If
                    'Commented by Dipali 04/07/2017
                    'If IsTrainingTicketProduct() Then
                    '    Session("IsTrainingTicketCampaign") = True
                    'Else
                    '    Session("IsTrainingTicketCampaign") = False
                    'End If
                    If CartGrid.Cart.CampaignCodeID > 0 Then
                        ChangeUnitQtyAsPerDiscount()
                    End If
                    'Redmine #13522
                    If lblCampaignMsg.Text.Trim = "Could not associate campaign code with order" Then
                        lblCampaignNotValidMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.CampaignNotValidMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        radCampanign.VisibleOnPageLoad = True
                    'Else
                        ' Refresh the control
                       ' MyBase.Response.Redirect(ViewCartPage, False)
                    End If
                 Else
                    If CBool(ViewState("PointNotsufficient")) = True Then ' Added By Govind 16/08/2017 for insufficient training ticket qty 
                    Else
                        If Not CartGrid.Cart.SetOrderCampaign(txtCampaign.Text, Page.Session) Then
                            lblCampaignMsg.Text = "Could not associate campaign code with order."
                        Else
                            lblCampaignMsg.Text = ""
                        End If
                        CartGrid.UpdateCart()
                        RefreshGrid()
                        MyBase.Response.Redirect(ViewCartPage, False)
                        Return
                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Function IsTrainingTicketProduct() As Boolean
            Try
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
                For Each oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrder.SubTypes("OrderLines")
                    Dim sSqlCheckProdCat As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductCategoryForTrainingTicket__c @ProductID=" & Convert.ToInt32(oOrderLine.GetValue("ProductID"))
                    Dim lProdCategoryID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSqlCheckProdCat, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lProdCategoryID > 0 Then
                        Return True
                        Exit For
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        Private Sub cmdRemoveCampaign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveCampaign.Click
            Try
                CartGrid.Cart.RemoveOrderCampaign(Page.Session)
                RemoveAddTrainingProduct() ' Code added by govind
                lblCampaignMsg.Text = ""
                RefreshGrid()
                UpdateCampaignDisplay()
                ' Refresh the control
                MyBase.Response.Redirect(ViewCartPage, False)
                Return
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub UpdateCampaignDisplay(Optional ByVal bShowError As Boolean = False)
            If CartGrid.Cart.CampaignCodeID <> -1 Then
                cmdApplyCampaign.Visible = False
                lblCampaignInstructions.Visible = False
                cmdRemoveCampaign.Visible = True
                'WongS, Modified as part of #20851 start
                lblCampaignMsg.Visible = False
                lblSuccessMsg.Visible = True
                lblSuccessMsg.Text = "Campaign code '" & CartGrid.Cart.CampaignCodeName & "' applied successfully."
                lblSuccessMsg.CssClass = "action-success-msg"
                'WongS, Modified as part of #20851 end
                txtCampaign.Visible = False
                txtCampaign.Text = CartGrid.Cart.CampaignCodeName

                'WongS, Modified as part of #20851
            Else
                cmdApplyCampaign.Visible = True
                lblCampaignInstructions.Visible = True
                cmdRemoveCampaign.Visible = False
                If bShowError Then
                    lblCampaignMsg.Visible = True
                End If
                txtCampaign.Visible = True
            End If
        End Sub
        ''' <summary>
        ''' Code added by Govind 9/1/2015 for when discount applied for Training Ticket product then update unit price
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ChangeUnitQtyAsPerDiscount()
            Try
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
                For Each oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrder.SubTypes("OrderLines")
                    Dim dUnitPrice As Decimal

                    Dim sSqlCheckProdCat As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductCategoryForTrainingTicket__c @ProductID=" & Convert.ToInt32(oOrderLine.GetValue("ProductID"))
                    Dim lProdCategoryID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSqlCheckProdCat, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lProdCategoryID > 0 Then
                        dUnitPrice = Convert.ToDecimal(oOrderLine.GetValue("Extended")) / Convert.ToDecimal(oOrderLine.GetValue("Quantity"))
                        oOrderLine.SetValue("Price", dUnitPrice)
                        oOrderLine.SetValue("UserPricingOverride", 1)
                        oOrderLine.SetValue("Extended", (dUnitPrice * Convert.ToDecimal(oOrderLine.GetValue("Quantity"))))
                    End If
                Next
                CartGrid.Cart.SaveCart(Session)
                RefreshGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub RemoveAddTrainingProduct()
            Try
                If CartGrid.Cart.CampaignCodeID <= 0 Then
                    Dim lTrainingTicketProduct As Long
                    Dim Qty As Decimal
                    Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                    oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
                    For Each oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrder.SubTypes("OrderLines")
                        Dim sSqlCheckProdCat As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductCategoryForTrainingTicket__c @ProductID=" & Convert.ToInt32(oOrderLine.GetValue("ProductID"))
                        Dim lProdCategoryID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSqlCheckProdCat, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If lProdCategoryID > 0 Then
                            lTrainingTicketProduct = Convert.ToInt32(oOrderLine.GetValue("ProductID"))
                            Qty = Convert.ToDecimal(oOrderLine.GetValue("Quantity"))
                            oOrder.RemoveOrderLine(oOrderLine)
                            Exit For
                        End If
                    Next
                    '' Updated by GM Start on 9 Oct 2018 for Redmine #19343
                    If lTrainingTicketProduct > 0 Then
                        oOrder.AddProduct(Convert.ToInt32(lTrainingTicketProduct))
                        Dim oOrderLineTraining As Aptify.Applications.OrderEntry.OrderLinesEntity
                        oOrderLineTraining = CType(oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1), Applications.OrderEntry.OrderLinesEntity)
                        oOrderLineTraining.SetValue("Quantity", Qty)
                        CartGrid.Cart.SaveCart(Session)
                        RefreshGrid()
                    End If
                    '' End Redmine #19343
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Rashmi P, Issue 5133, 12/6/12 Add ShipmentType Selection.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadShipmentType()

            Dim oShipmentTypes As New Aptify.Framework.Web.eBusiness.CommonMethods(DataAction, AptifyApplication, User1, Database)
            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
            oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
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

                If ddlShipmentType.Items.Count > 0 Then  'Added by sandeep for issue on 07/03/2013
                    If Me.CartGrid.Grid.Items.Count > 0 Then
                        If Not oOrder Is Nothing Then
                            ddlShipmentType.SelectedValue = CStr(oOrder.ShipTypeID)
                        Else
                            ddlShipmentType_SelectedIndexChanged(Nothing, Nothing)
                        End If
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

#Region "Custom Code-Govind"
        Private Function TrainingTicketCampaignApply() As Boolean
            Try
                Dim bCPDMeetings As Boolean = False
                Dim grdMain As RadGrid = DirectCast(CartGrid.FindControl("grdMain"), RadGrid)
                For Each row As GridDataItem In grdMain.Items
                    Dim lblProductID As Label = DirectCast(row.FindControl("lblProductID"), Label)
                    Dim sCPDMeetings = "..spGetCPDMeetingsProduct__c @ProductID=" & lblProductID.Text
                    Dim lCPDProdCat As Long = Convert.ToInt32(DataAction.ExecuteScalar(sCPDMeetings, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lCPDProdCat > 0 Then
                        bCPDMeetings = True
                        Exit For
                    End If
                Next
                If bCPDMeetings = True Then

                    If txtCampaign.Text.Trim <> "" Then
                        'Dim lblProductID As Label
                        'Dim row As GridDataItem
                        Dim bIsTrainingPointAvailable As Boolean = False
                        Dim dMaxPoint As Decimal = 0

                        'Commented by dipali 
                        '  spGetCurrentYearValidTrainingTicketCampaign__c
                        'Dim sCampainSql As String = Database & "..spValidTrainingTicketCampaign__c @Name='" & txtCampaign.Text.Trim & "'"
                        'Dim lCampaignID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sCampainSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        ' If lCampaignID > 0 Then

                        'Dim sSql As String = Database & "..spGetValidCampaignCurrentYr__c @CampaignID=" & lCampaignID
                        'Dim IsCurrentYr As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        'If IsCurrentYr > 0 Then


                        'Added by dipali 04/06/2017 to get current year valid campaign code Id
                        Dim sCampainSql As String = Database & "..spGetCurrentYearValidTrainingTicketCampaign__c @Name='" & txtCampaign.Text.Trim & "'"
                        Dim lCampaignID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sCampainSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        '--------------End

                        If lCampaignID > 0 Then
                            'commented by Dipali
                            'For Each row In grdMain.Items
                            '    lblProductID = DirectCast(row.FindControl("lblProductID"), Label)

                            '    ' Check Product is  on same campaign List
                            '    Dim sSqlProductInCampaign As String = Database & "..spCheckProductInCampaignList__c @CampaignID=" & lCampaignID & ", @ProductID=" & Convert.ToInt32(lblProductID.Text)
                            '    Dim lTrainingTicket As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlProductInCampaign, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            '    If lTrainingTicket > 0 Then
                            '        ' Get Training Ticket point on the product
                            '        'Dim sTrainingPoint As String = Database & "..spGetProductsTrainingTicketPoints__c @ProductID=" & lCampProductID
                            '        'Dim dTrainingPoint As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sTrainingPoint, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            '        'If dTrainingPoint > 0 Then
                            '        bIsTrainingPointAvailable = True
                            '        dMaxPoint = dMaxPoint + lTrainingTicket
                            '        'End If
                            '    End If
                            'Next

                            'Code added by GM for redmine #19343
                            For Each row As GridDataItem In grdMain.Items
                                Dim lblProductID As Label = DirectCast(row.FindControl("lblProductID"), Label)

                                Dim sTrainingPoint As String = Database & "..spGetProductsTrainingTicketPoints__c @ProductID=" & lblProductID.Text
                                Dim dTrainingPoint As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sTrainingPoint, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If dTrainingPoint > 0 Then
                                    bIsTrainingPointAvailable = True
                                    dMaxPoint = dMaxPoint + dTrainingPoint
                                End If
                            Next
                            'Code End by GM for redmine #19343
                            'below code commented by GM for redmine #19343
                            'Added By Dipali to get total training point 04/06/2017
                            ''GetProductLIst()  ' Added By Govind 16/08/2017 for insufficient training ticket qty 
                            ''Dim sTrainingPoint As String = Database & "..spGetTotalTrainingTicketPoints__c @ProductID= '" & ProductIdLIst & "'"
                            ''Dim dTrainingPoint As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sTrainingPoint, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            ''If dTrainingPoint > 0 Then
                            ''    bIsTrainingPointAvailable = True
                            ''    dMaxPoint = dTrainingPoint
                            ''End If

                            '------------------End
                            'End commented  by GM for redmine #19343

                            If bIsTrainingPointAvailable Then
                                ' check Maximum Point on Ebiz user in campaign List
                                Dim sSqlMaxQty As String = Database & "..spGetMaxQtyProspectList__c @CampaignID=" & lCampaignID & ",@PersonID=" & User1.PersonID
                                Dim dPersonMaxPoint As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sSqlMaxQty, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If dPersonMaxPoint > 0 Then
                                    ' Check Product line Training point and > than this person Training point then show msg
                                    If dMaxPoint <= dPersonMaxPoint Then
                                    Else
                                        ViewState("PointNotsufficient") = True ' Added By Govind 16/08/2017 for insufficient training ticket qty 
                                        lblWarningMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ViewCart.TrainingTicketPoints")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                        lblWarningMsg.Visible = True 'WongS, Modified as part of #20812
                                        Return False
                                    End If
                                Else
                                    ViewState("PointNotsufficient") = True ' Added By Govind 16/08/2017 for insufficient training ticket qty 
                                    lblWarningMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ViewCart.TrainingTicketPoints")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                    lblWarningMsg.Visible = True 'WongS, Modified as part of #20812
                                    Return False
                                End If
                            End If
                        Else
                            ''Commented below code by Govind on 1/12/2017 for MEMDISC Not working 
                            '' ViewState("PointNotsufficient") = True ' Added By Govind 16/08/2017 for insufficient training ticket qty 
                            ''  lblWarningMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ViewCart.CampaignCodeNotFound")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            ''Return False
                            Return True
                        End If


                    End If
                    ' End If
                Else
                    Return True
                End If
                Return True

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        ''' <summary>
        ''' 'Added By dipali 04/07/2017 to get list of product in grid
        ''' </summary>
        Private Sub GetProductLIst()
            ProductIdLIst = String.Empty
            Dim grdMain As RadGrid = DirectCast(CartGrid.FindControl("grdMain"), RadGrid)
            For Each row As GridDataItem In grdMain.Items
                Dim lblProductID As Label = DirectCast(row.FindControl("lblProductID"), Label)

                ProductIdLIst = ProductIdLIst + lblProductID.Text & ","
            Next
            ProductIdLIst = ProductIdLIst.TrimEnd(CChar(","))
        End Sub

        Private Sub LoadTopupCampaign()
            Try
                Dim bProductCat As Boolean = False
                Dim lProductID As Long
                Dim qty As Decimal
                'Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                'oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)

                'For Each oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrder.SubTypes("OrderLines")
                '    Dim sSqlCheckProdCat As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductCategoryForTrainingTicket__c @ProductID=" & CLng(oOrderLine.GetValue("ProductID"))
                '    Dim lProdCategoryID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSqlCheckProdCat, IAptifyDataAction.DSLCacheSetting.BypassCache))
                '    If lProdCategoryID > 0 Then
                '        qty = Convert.ToDecimal(oOrderLine.GetValue("Quantity"))
                '        lProductID = CLng(oOrderLine.GetValue("ProductID"))
                '        bProductCat = True
                '        Session("IsTrainingTicket") = 1
                '        Exit For
                '    End If
                'Next


                'Added by dipali 04/07/2017
                Dim sSqlCheckProdCat As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductListForTrainingTicket__c @ProductID= '" & ProductIdLIst & "'"
                Dim lProdCategoryID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSqlCheckProdCat, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lProdCategoryID > 0 Then
                    lProductID = lProdCategoryID
                    bProductCat = True
                    Session("IsTrainingTicket") = 1
                End If
                '------End Dipali
                If bProductCat = True Then
                    Dim sSql As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spGetToupCampaignWithPerson__c @PersonID=" & User1.PersonID & ",@ProductID=" & lProductID

                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        'Commented by dipali 04/07/2017
                        'Dim dtTopupCampaignTable As New DataTable
                        'dtTopupCampaignTable.Columns.Add("CampaignID")
                        'dtTopupCampaignTable.Columns.Add("Name")
                        'dtTopupCampaignTable.Columns.Add("MinQty__c")
                        'dtTopupCampaignTable.Columns.Add("MaxQty__c")
                        'Dim min As Decimal
                        'Dim max As Decimal
                        'For Each dr As DataRow In dt.Rows
                        '    'If qty >= Convert.ToDecimal(dr("MinQty__c")) Then
                        '    If min = 0 Then
                        '        min = Convert.ToDecimal(dr("MinQty__c"))
                        '    End If
                        '    'End If
                        '    max = Convert.ToDecimal(dr("MaxQty__c"))

                        'Next
                        'For Each dr As DataRow In dt.Rows
                        '    If qty >= min AndAlso qty <= max Then
                        '        Dim drTopupCampaignTable As DataRow = dtTopupCampaignTable.NewRow()
                        '        drTopupCampaignTable("CampaignID") = dr("CampaignID")
                        '        drTopupCampaignTable("Name") = dr("Name")
                        '        drTopupCampaignTable("MinQty__c") = dr("MinQty__c")
                        '        drTopupCampaignTable("MaxQty__c") = dr("MaxQty__c")
                        '        dtTopupCampaignTable.Rows.Add(drTopupCampaignTable)
                        '    ElseIf qty >= min AndAlso max <= 0 Then
                        '        Dim drTopupCampaignTable As DataRow = dtTopupCampaignTable.NewRow()
                        '        drTopupCampaignTable("CampaignID") = dr("CampaignID")
                        '        drTopupCampaignTable("Name") = dr("Name")
                        '        drTopupCampaignTable("MinQty__c") = dr("MinQty__c")
                        '        drTopupCampaignTable("MaxQty__c") = dr("MaxQty__c")
                        '        dtTopupCampaignTable.Rows.Add(drTopupCampaignTable)
                        '    End If
                        'Next


                        'For Each dr As DataRow In dt.Rows
                        '    If qty >= Convert.ToDecimal(dr("MinQty__c")) AndAlso (max <= Convert.ToDecimal(dr("MaxQty__c")) Or max <= 0) Then

                        '    End If
                        'Next
                        '
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            'grdTopupCampaignList.DataSource = dtTopupCampaignTable
                            grdTopupCampaignList.DataSource = dt
                            grdTopupCampaignList.DataBind()
                            grdTopupCampaignList.Visible = True
                            trToupCampaign.Visible = True
                            ViewState("TopupCampign") = True
                            CheckTopupCampaignQty()
                        Else
                            grdTopupCampaignList.Visible = False
                            trToupCampaign.Visible = False
                            ViewState("TopupCampign") = False
                        End If

                    Else
                        grdTopupCampaignList.Visible = False
                        trToupCampaign.Visible = False
                        ViewState("TopupCampign") = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub grdTopupCampaignList_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdTopupCampaignList.ItemCommand
            Try
                If e.CommandName = "CampaignName" Then
                    Dim CampaignName As String = Convert.ToString(e.CommandArgument)
                    txtCampaign.Text = CampaignName
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Function CheckTrainingTicketApplyTopupCode() As Boolean
            Try
                Dim bProductCat As Boolean = False
                'Dipali need to change
                'Dim qty As Decimal
                'Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                'oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
                'For Each oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrder.SubTypes("OrderLines")
                '    Dim sSqlCheckProdCat As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductCategoryForTrainingTicket__c @ProductID=" & CLng(oOrderLine.GetValue("ProductID"))
                '    Dim lProdCategoryID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSqlCheckProdCat, IAptifyDataAction.DSLCacheSetting.BypassCache))
                '    If lProdCategoryID > 0 Then
                '        bProductCat = True
                '        Session("IsTrainingTicket") = 1
                '        Exit For
                '    End If
                'Next
                '  If bProductCat = True Then

                If (Session("IsTrainingTicket") IsNot Nothing) Then
                    If Convert.ToInt32(Session("IsTrainingTicket")) = 1 Then
                        If CartGrid.Cart.CampaignCodeID > 0 Then
                            Return True
                        Else
                            If Convert.ToBoolean(ViewState("TopupCampign")) = True Then
                                lblTopupWarningMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ViewCart.AddTopupCode")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                lblTopupWarningMsg.Visible = True
                            Else
                                lblTopupWarningMsg.Visible = False
                                Return True
                            End If
                        End If
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Function
        Private Sub CheckTopupCampaignQty()
            Try
                Dim bIsTopup As Boolean = False
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
                For Each oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrder.SubTypes("OrderLines")
                    Dim sSqlCheckProdCat As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductCategoryForTrainingTicket__c @ProductID=" & CLng(oOrderLine.GetValue("ProductID"))
                    Dim lProdCategoryID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSqlCheckProdCat, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lProdCategoryID > 0 Then
                        For Each row As Telerik.Web.UI.GridItem In grdTopupCampaignList.Items
                            Dim lblMinQty As Label = DirectCast(row.FindControl("lblMinQty"), Label)
                            Dim lblMaxQty As Label = DirectCast(row.FindControl("lblMaxQty"), Label)
                            If Convert.ToDecimal(oOrderLine.GetValue("Quantity")) >= Convert.ToDecimal(lblMinQty.Text) AndAlso (Convert.ToDecimal(oOrderLine.GetValue("Quantity")) <= Convert.ToDecimal(lblMaxQty.Text) Or Convert.ToDecimal(lblMaxQty.Text) = 0) Then

                                bIsTopup = True
                                'Else
                                '    ViewState("TopupCampign") = False
                                '    lblTopupMsg.Text = "* Select Toup Campaign or create a new topup campaign"
                                '    'Exit For
                            End If

                        Next
                        If bIsTopup = False Then
                            ViewState("TopupCampign") = False
                            'lblTopupMsg.Text = "* Select Toup Campaign or create a new topup campaign"
                            lblTopupMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ViewCart.SelectToupCampaignOrCreateCampaign")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        Else
                            ViewState("TopupCampign") = True
                        End If
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Protected Sub grdTopupCampaignList_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdTopupCampaignList.ItemDataBound
        '    Try
        '        If TypeOf (e.Item) Is GridDataItem Then
        '            Dim lblMinQty As Label = DirectCast(e.Item.FindControl("lblMinQty"), Label)
        '            Dim lblMaxQty As Label = DirectCast(e.Item.FindControl("lblMaxQty"), Label)
        '            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
        '            oOrder = CartGrid.Cart.GetOrderObject(Page.Session, Page.User, Page.Application)
        '            For Each oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity In oOrder.SubTypes("OrderLines")
        '                Dim sSqlCheckProdCat As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spCheckProductCategoryForTrainingTicket__c @ProductID=" & CLng(oOrderLine.GetValue("ProductID"))
        '                Dim lProdCategoryID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSqlCheckProdCat, IAptifyDataAction.DSLCacheSetting.BypassCache))
        '                If lProdCategoryID > 0 Then
        '                    If Convert.ToDecimal(oOrderLine.GetValue("Quantity")) >= Convert.ToDecimal(lblMinQty.Text) AndAlso Convert.ToDecimal(oOrderLine.GetValue("Quantity")) <= Convert.ToDecimal(lblMaxQty.Text) Then
        '                        ViewState("TopupCampign") = True
        '                    Else
        '                        ViewState("TopupCampign") = False
        '                        lblTopupMsg.Text = "* Select Toup Campaign or create a new topup campaign"
        '                        Exit For
        '                    End If
        '                End If
        '            Next


        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub
#End Region

        Protected Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
            radCampanign.VisibleOnPageLoad = False
            ' Refresh the control
            MyBase.Response.Redirect(ViewCartPage, False)
            Return
        End Sub

    End Class
End Namespace
