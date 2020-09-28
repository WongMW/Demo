Option Explicit On
Option Strict On

Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic.ProcessPipeline

Namespace Aptify.Framework.Web.eBusiness.ProductCatalog
    Partial Class CheckOutControl
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage"
        Protected Const ATTRIBUTE_NEXT_BUTTON_PAGE As String = "NextButtonPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "CheckOutControl"
        Protected Const ATTRIBUTE_CONFIRMATION_PAGE As String = "OrderConfirmationPage"


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
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            Try
                If Not IsPostBack Then
                    If User1.UserID > 0 Then
                        ' There is a user logged in, go to the cart
                        LoadShipmentType()
                        RefreshGrid()

                    Else
                        ' No User is logged in, redirect to the
                        ' Login Page in Customer Service
                        'Suraj S Issue 15370, 3/29/13 change session variale to application variable
                        Session("ReturnToPage") = Request.RawUrl
                        Response.Redirect(LoginPage)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
         
        Private Sub RefreshGrid()
            Try
                CartGrid.RefreshGrid()
                'Navin Prasad Issue 11032
                If CartGrid.Grid.Items.Count > 0 Then
                    cmdUpdateCart.Visible = True
                    cmdNextStep.Enabled = True
                    tblRowMain.Visible = True
                    lblGotItems.Visible = True
                    lblNoItems.Visible = False
                Else
                    cmdUpdateCart.Visible = False
                    cmdNextStep.Enabled = False
                    tblRowMain.Visible = False
                    lblGotItems.Visible = False
                    lblNoItems.Visible = True
                End If
                With CartGrid.Cart
                    Me.OrderSummary1.Refresh()
                    If CartGrid.Grid.Items.Count > 0 AndAlso ShoppingCart.GrandTotal = 0 Then
                        cmdNextStep.Text = "Complete Order"
                        lblcheckoutMsg.Text = "Your default shipping address and other settings are shown below. Use the buttons to make any changes. When you're done, click the " & "<b>Complete Order</b> button."
                    Else
                        lblcheckoutMsg.Text = "Default shipping address and other settings are shown below. Use the buttons to make any changes. When you are done, click the " & " <b>Next Step </b>button."
                    End If
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
                With ShoppingCart.GetOrderObject(Session, Page.User, Application)
                    .SetValue("PayTypeID", iPOPaymentType)
                    ShoppingCart.SaveCart(Session)
                    lOrderID = ShoppingCart.PlaceOrder(Session, Application, Page.User, sError)
                End With
                If lOrderID > 0 Then
                    ' Navin Prasad Issue 9388
                    If PlaceDownloadProduct(lOrderID, sError) Then
                        SendReadyForDownloadEmail(lOrderID)
                    ElseIf Len(sError) > 0 Then
                        ProductDownloadFailed(sError)
                    End If
                    Response.Redirect(OrderConfirmationPage & "?ID=" & lOrderID)
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
            If ShoppingCart.GrandTotal > 0 Then
                Response.Redirect(NextButtonPage)
            Else
                Me.CompleteOrderforFreeProduct()
            End If
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
    End Class
End Namespace
