Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Serialization
Imports System.Data
Imports System.Web.Script.Services
Imports Aptify.Applications.OrderEntry

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class CourseEnrolments__c
    Inherits Aptify.Framework.Web.eBusiness.BaseWebService

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=False, XmlSerializeString:=False)>
    Public Function UpdatePriceInStaging(ByVal Status As String, ByVal CompanyID As String, ByVal BillToID As String) As String
        Dim sResult As String = String.Empty
        Dim js As JavaScriptSerializer = New JavaScriptSerializer()
        'Initializing session variable
        Session("UpdatePriceStatus") = ""
        Try
            Dim sSQL As String
            sSQL = Database & "..spGetEnrollmentStagingByOrderStatus__c"
            Dim param(1) As IDataParameter
            param(0) = DataAction.GetDataParameter("@Status", SqlDbType.VarChar, Status)
            param(1) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, CInt(CompanyID))
            Dim dtPending As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)

            If dtPending IsNot Nothing AndAlso dtPending.Rows.Count > 0 Then
                Dim TobeDistinct As String() = {"StudentGroupID", "AlternativeGroupID", "ClassID", "ProductID", "RouteOfEntryID"}
                Dim dtDistPending As DataTable = dtPending.DefaultView.ToTable(True, TobeDistinct)
                dtDistPending.Columns.Add("Price")
                ' dtDistPending.Columns.Add("ParentID")
                For Each dr As DataRow In dtDistPending.Rows
                    dr("Price") = GetPrice(CLng(dr("StudentGroupID").ToString()), CLng(dr("ProductID").ToString()), CLng(dr("AlternativeGroupID").ToString()), CLng(dr("ClassID").ToString()), CLng(dr("RouteOfEntryID").ToString()), CLng(dr("StudentGroupID").ToString()), CLng(BillToID))
                Next

                sSQL = Database & "..spUpdateEnrollmentStagingPriceNew__c"
                Dim param2(0) As IDataParameter
                For Each dr As DataRow In dtPending.Rows
                    Dim ParentId As Integer = 0
                    Dim filterRow() As DataRow = dtDistPending.Select("StudentGroupID='" & Convert.ToString(dr("StudentGroupID").ToString()) & "' AND AlternativeGroupID='" & Convert.ToString(dr("AlternativeGroupID")) & "'   AND ProductID='" & Convert.ToString(dr("ProductID")) & "' AND ClassID='" & Convert.ToString(dr("ClassID")) & "' AND RouteOfEntryID='" & Convert.ToString(dr("RouteOfEntryID")) & "' ")
                    Dim price As Double = Convert.ToDecimal(filterRow(0)("Price"))
                    dr("Price") = price
                    If dr("ClassType").ToString().Trim().ToLower() = "exam" Or dr("ClassType").ToString().Trim().ToLower() = "interim assessment" Then
                        If CInt(dr("StudentGroupID").ToString()) > 0 Then
                            ParentId = CInt(Convert.ToString(dr("ParentID")))
                            If ParentId > 0 Then
                                dr("StudentGroupID") = ParentId
                                dr("AlternativeGroupID") = 0
                            Else
                            End If
                        ElseIf CInt(dr("AlternativeGroupID").ToString()) > 0 Then
                            ParentId = CInt(Convert.ToString(dr("ParentID")))
                            If ParentId > 0 Then
                                dr("StudentGroupID") = 0
                                dr("AlternativeGroupID") = ParentId
                            Else
                            End If
                        End If
                    Else
                    End If
                    '  Dim recordupdate As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param2, 180)
                Next

                Dim TobeDistinct2 As String() = {"IDEstage", "StudentID", "StudentGroupID", "AlternativeGroupID", "Price"}
                Dim dtDistPending2 As DataTable = dtPending.DefaultView.ToTable(True, TobeDistinct2)

                param2(0) = DataAction.GetDataParameter("@Pendingdetail", SqlDbType.Structured, dtDistPending2)
                Dim recordupdate As Integer = Me.DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, param2, 180)
                sResult = "Success"
                'Setting session variable value on success
                Session("UpdatePriceStatus") = "Success"
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            sResult = "Failed"
            'Setting session variable value on failure
            Session("UpdatePriceStatus") = "Failed"
        End Try

        Return js.Serialize(sResult)
    End Function

    Private Function GetPrice(ByVal groupId As Long, ByVal ProductID As Long, ByVal AlternateGroupID As Long, ByVal ClassID As Long, ByVal RouteOfEntryID As Long, ByVal SelectedStudentID As Integer, ByVal BillToID As Integer) As Double
        Try
            Dim groupIdNew As Long
            Dim order As Aptify.Applications.OrderEntry.OrdersEntity
            Dim orderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
            order = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
            order.ShipToID = SelectedStudentID
            order.BillToSameAsShipTo = False
            order.BillToID = BillToID
            order.AddProduct(ProductID, 1)
            If AlternateGroupID > 0 Then
                groupIdNew = AlternateGroupID
            ElseIf groupId > 0 Then
                groupIdNew = groupId
            End If
            If order.SubTypes("OrderLines").Count > 0 Then
                orderLine = TryCast(order.SubTypes("OrderLines").Item(0), OrderLinesEntity)
                With orderLine
                    'BFP Performance: Commented unwanted set values
                    '.ExtendedOrderDetailEntity.SetValue("ClassID", ClassID)
                    '.ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                    '.ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", 1)
                    '.ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", 1)
                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", groupId)
                    '.ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", RouteOfEntryID)
                    .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                End With
                Return orderLine.Price
            Else
                Return 0.0
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Gets the product price with campaign.
    ''' </summary>
    ''' <param name="ProductID">The product identifier.</param>
    ''' <param name="CampaignID">The campaign identifier.</param>
    ''' <param name="shipToID">The ship to identifier.</param>
    ''' <param name="billToID">The bill to identifier.</param>
    ''' <returns></returns>
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=False, XmlSerializeString:=False)>
    Public Function GetProductPriceWithCampaign(ByVal ProductID As Long, ByVal CampaignID As Long, ByVal shipToID As Long, ByVal billToID As Long) As String

        Dim User1 As System.Security.Principal.IPrincipal = CType(Session("PageUser"), System.Security.Principal.IPrincipal)
        Dim ShoppingCart1 As Aptify.Framework.Web.eBusiness.AptifyShoppingCart = New Aptify.Framework.Web.eBusiness.AptifyShoppingCart()
        ShoppingCart1 = CType(Session("ShoppingCart"), Aptify.Framework.Web.eBusiness.AptifyShoppingCart)
        'ShoppingCart1.AddToCart(ProductID, -1)
        Dim sPrice As String = String.Empty
        Dim jsPrice As JavaScriptSerializer = New JavaScriptSerializer()
        Dim oOrder As OrdersEntity = New OrdersEntity()
        Dim sMemberPrice As String = String.Empty
        Dim oPriceCurrency As Integer = 2

        Try
            If ShoppingCart1 IsNot Nothing Then
                Dim dt As New System.Data.DataTable()
                Dim oPrice As Aptify.Applications.OrderEntry.IProductPrice.PriceInfo
                oPrice = ShoppingCart1.GetUserProductPrice(ProductID, 1)
                Dim sFormat As String = ShoppingCart1.GetCurrencyFormat(oPrice.CurrencyTypeID)
                oPriceCurrency = oPrice.CurrencyTypeID

                Try
                    If ProductID > 0 And CampaignID < 0 And oPrice IsNot Nothing Then
                        sPrice = Format(oPrice.Price, sFormat)

                        Dim MemberPersonId As Long = Convert.ToInt64(AptifyApplication.GetEntityAttribute("Persons", "MemberPersonID__c"))

                        billToID = MemberPersonId
                        shipToID = MemberPersonId

                        If billToID > 0 And shipToID > 0 Then
                            oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)

                            If oOrder IsNot Nothing Then
                                oOrder.BillToID = billToID
                                oOrder.ShipToID = shipToID
                                'oOrder.SetValue("CampaignCodeID", CampaignID)
                                oOrder.SetValue("CurrencyTypeID", oPriceCurrency)
                                oOrder.AddProduct(ProductID)
                                Dim ordeLinesCount As Integer = 0
                                ordeLinesCount = oOrder.SubTypes("OrderLines").Count()

                                If ordeLinesCount > 0 AndAlso (oPrice.Price < Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended")) And oOrder.SubTypes("OrderLines").Item(0).GetValue("PriceName") = "Euro") Then
                                    sMemberPrice = String.Empty
                                ElseIf Not User1 Is Nothing And oPrice.Price <> ShoppingCart1.GetSingleProductNonMemberCost(User1, ProductID) Then
                                    sMemberPrice = Format(ShoppingCart1.GetSingleProductMemberCost(User1, ProductID), sFormat)
                                    sPrice = Format(ShoppingCart1.GetSingleProductNonMemberCost(User1, ProductID), sFormat)
                                ElseIf oPriceCurrency = 3 And ordeLinesCount > 1 AndAlso (oOrder.SubTypes("OrderLines").Item(0).GetValue("ProductID") = ProductID And oPrice.Price > Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended"))) Then
                                    sMemberPrice = String.Empty
                                ElseIf oPriceCurrency = 2 And ordeLinesCount > 0 AndAlso (oOrder.SubTypes("OrderLines").Item(0).GetValue("ProductID") = ProductID And oPrice.Price > Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended"))) Then
                                    sMemberPrice = Format(Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended")), sFormat)
                                ElseIf oPrice.Price = 0 And ordeLinesCount = 1 Then
                                    sPrice = Format(Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended")), sFormat)
                                ElseIf oPrice.Price = 0 And ordeLinesCount > 1 Then
                                    'KIT PRODUCTS WITHOUT PRICE MAYBE STERLING
                                    Dim sumPrice As Decimal = 0.00

                                    For i = 0 To ordeLinesCount - 1
                                        If oOrder.SubTypes("OrderLines").Item(i).GetValue("ProductID") <> ProductID Then
                                            ShoppingCart1.AddToCart(oOrder.SubTypes("OrderLines").Item(i).GetValue("ProductID"))
                                        End If
                                    Next

                                    ShoppingCart1.FillCart(dt)

                                    If Not dt Is Nothing AndAlso dt.Rows.Count() > 0 Then

                                        For i = 0 To dt.Rows.Count() - 1
                                            Dim getCDecPrice = 0.00
                                            Dim sGetDecPrice = String.Empty
                                            sGetDecPrice = dt.Rows(i).Item("Extended").ToString.Replace("€", "").Replace("£", "")

                                            If oPrice.CurrencyTypeID = 2 Then

                                                getCDecPrice = Math.Round(CDec(sGetDecPrice), 2)

                                                sumPrice = getCDecPrice + sumPrice

                                            ElseIf oPrice.CurrencyTypeID = 3 Then

                                                getCDecPrice = Math.Round((CDec(sGetDecPrice) / dt.Rows.Count()), 2)

                                                sumPrice = getCDecPrice + sumPrice
                                                'sumPrice += sGetDecPrice

                                            End If
                                        Next
                                    End If
                                    sPrice = Format(sumPrice, sFormat)
                                Else
                                    ShoppingCart1.AddToCart(ProductID)
                                    ShoppingCart1.FillCart(dt)

                                    If Not dt Is Nothing AndAlso dt.Rows.Count() > 0 Then

                                        Dim getCDecPrice = 0
                                        Dim sGetDecPrice = String.Empty
                                        sGetDecPrice = dt.Rows(0).Item("Extended").ToString.Replace("€", "").Replace("£", "")
                                        getCDecPrice = CDec(sGetDecPrice)

                                        If Math.Round(oPrice.Price, 2) > getCDecPrice And (Not String.IsNullOrEmpty(dt.Rows(0).Item("ProductID").ToString()) AndAlso dt.Rows(0).Item("ProductID").ToString() = ProductID.ToString()) Then
                                            sMemberPrice = sGetDecPrice
                                        End If
                                    End If
                                End If

                                If sMemberPrice = sPrice Then
                                    sMemberPrice = String.Empty
                                End If

                                oOrder = Nothing
                            End If
                        End If
                    Else
                        oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                        If oOrder IsNot Nothing Then
                            oOrder.BillToID = billToID
                            oOrder.ShipToID = shipToID
                            oOrder.SetValue("CampaignCodeID", CampaignID)
                            oOrder.AddProduct(ProductID)
                            sPrice = Format(Convert.ToDecimal(oOrder.SubTypes("OrderLines").Item(0).GetValue("Extended")), sFormat)
                            oOrder = Nothing
                        End If
                    End If
                Catch ex As Exception
                    ShoppingCart1.AddToCart(ProductID)
                    ShoppingCart1.FillCart(dt)

                    If Not dt Is Nothing AndAlso dt.Rows.Count() > 0 Then

                        Dim getCDecPrice = 0
                        Dim sGetDecPrice = String.Empty
                        sGetDecPrice = dt.Rows(0).Item("Extended").ToString.Replace("€", "").Replace("£", "")
                        getCDecPrice = CDec(sGetDecPrice)

                        If Math.Round(oPrice.Price, 2) > getCDecPrice Then
                            sMemberPrice = sGetDecPrice
                        End If
                    End If
                    'sPrice = Format(0, sFormat)
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            End If
        Catch ex As Exception
            sPrice = Format(0, "€#,###,##0.00;(€#,###,##0.00)")
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

        If String.IsNullOrEmpty(sPrice) Then
            sPrice = Format(0, "€#,###,##0.00;(€#,###,##0.00)")
            sMemberPrice = String.Empty
        End If

        If CampaignID = -2 Then
            'Added as part of #20508
            If Not String.IsNullOrEmpty(sMemberPrice) Then
                sPrice = sPrice + "~" + CStr(ProductID) + "~(" + sMemberPrice + ")"
            Else
                sPrice = sPrice + "~" + CStr(ProductID)
            End If
        End If

        If CampaignID = -1 Then
            If Not String.IsNullOrEmpty(sMemberPrice) Then
                sPrice = sPrice + "~" + CStr(ProductID) + "~(" + sMemberPrice + ")"
            End If
        End If

        Return jsPrice.Serialize(sPrice)
    End Function
End Class
