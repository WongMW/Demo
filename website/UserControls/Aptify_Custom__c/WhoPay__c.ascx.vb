'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande              11/21/2011                      As per selecting Firm Or Individual pay save the data into persons and companies Who pays sub entity
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Option Explicit On
Option Strict On

Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.AttributeManagement
Imports System.Data
Imports Aptify.Applications.OrderEntry
Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class WhoPay__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Private ClearPersonWhoPays As String = String.Empty
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            cmpCalenders.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.DateValidation")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            CompareValidator1.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.DateValidation")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            If Not IsPostBack Then
                BindProductCategory()
                LoadSubsidiariesCompanyList()

                BindCompanyPayData()
                BindLoadProducts()
                BindPersonList(Convert.ToInt32(cmbIndividualCompany.SelectedValue))
                BindIndividualPayData()
                LoadMemberType()
                LoadRole()
                CurrentIndividualPayDeleteTable = Nothing
            End If
        End Sub
#Region "Load Data"
        ''' <summary>
        ''' Get All Prodct List without selecting Product Category
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindLoadProducts()
            Try
                'Dim sProdctsList As String = "..spGetAllProductsList__c"
                'Dim dtProductList As DataTable = DataAction.GetDataTable(sProdctsList)
                'If Not dtProductList Is Nothing AndAlso dtProductList.Rows.Count > 0 Then
                '    cmbProduct.DataSource = dtProductList
                '    cmbProduct.DataTextField = "Name"
                '    cmbProduct.DataValueField = "ID"
                '    cmbProduct.DataBind()
                '    ' cmbProduct.Items.Insert(0, "-- Select Product --")
                '    cmbProduct.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))

                '    cmbProductList.DataSource = dtProductList
                '    cmbProductList.DataTextField = "Name"
                '    cmbProductList.DataValueField = "ID"
                '    cmbProductList.DataBind()
                '    cmbProductList.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                'Else
                '    cmbProduct.Items.Clear()
                '    cmbProductList.Items.Clear()
                'End If
                cmbProduct.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                cmbProductList.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
            Catch ex As Exception

            End Try
        End Sub
        ''' <summary>
        ''' Bind Product Catrgories
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindProductCategory()
            Try
                ' Get All Products Categories
                Dim sSQLProductCategory As String = "..spGetProductsCategory__c"
                Dim dtProductCategory As DataTable = DataAction.GetDataTable(sSQLProductCategory)
                If Not dtProductCategory Is Nothing AndAlso dtProductCategory.Rows.Count > 0 Then
                    cmbProductCategory.DataSource = dtProductCategory
                    cmbProductCategory.DataTextField = "Name"
                    cmbProductCategory.DataValueField = "ID"
                    cmbProductCategory.DataBind()
                    'cmbProductCategory.Items.Insert(0, "-- Select Product Category--")
                    cmbProductCategory.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                    ''If Convert.ToString(cmbProductCategory.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    ''    BindProductList(Convert.ToInt32(cmbProductCategory.SelectedValue))
                    ''Else
                    ''    'BindLoadProducts()
                    ''End If

                    ' Bind Individual Product Category

                    cmbProductCat.DataSource = dtProductCategory
                    cmbProductCat.DataTextField = "Name"
                    cmbProductCat.DataValueField = "ID"
                    cmbProductCat.DataBind()
                    cmbProductCat.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                    ''If Convert.ToString(cmbProductCat.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    ''    BindProductList(Convert.ToInt32(cmbProductCat.SelectedValue))
                    ''Else
                    ''    ' BindProductList(0)
                    ''    ' BindLoadProducts()
                    ''End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Dispay Firm pay Product list
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindCompanyPayData()
            Try
                ' If Data on CurrentCompanyPayTable Data table Property then bind to the grdCompanyPay
                If Not CurrentCompanyPayTable Is Nothing AndAlso CurrentCompanyPayTable.Rows.Count > 0 Then
                    grdCompanyPay.DataSource = CurrentCompanyPayTable
                    grdCompanyPay.DataBind()
                End If
                Dim SubsidariesCompanyDT As DataTable = DirectCast(ViewState("SubsidariesCompanyDT"), DataTable)
                Dim sCompanyID As String = String.Empty

                If Not SubsidariesCompanyDT Is Nothing AndAlso SubsidariesCompanyDT.Rows.Count > 0 Then
                    For Each dr As DataRow In SubsidariesCompanyDT.Rows
                        If sCompanyID = "" Then
                            sCompanyID = Convert.ToString(dr("ID"))
                        Else
                            sCompanyID = sCompanyID & "," & Convert.ToString(dr("ID"))
                        End If
                    Next

                End If
                ' if Data found in  CompanyPaysProductCategories__c entity then bind grdCompanyPay
                Dim sSql As String = "..spGetCompanyPaysProductCategories__c @CompanyID='" & sCompanyID & "'"
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    grdCompanyPay.DataSource = dt
                    grdCompanyPay.DataBind()
                    CurrentCompanyPayTable = dt
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Dispay Individual pay Product list
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindIndividualPayData()
            Try
                ' If Data on CurrentCompanyPayTable Data table Property then bind to the grdCompanyPay
                If Not CurrentIndividualPayTable Is Nothing AndAlso CurrentIndividualPayTable.Rows.Count > 0 Then
                    grdIndividualPay.DataSource = CurrentIndividualPayTable
                    grdIndividualPay.DataBind()
                End If
                Dim sCompanyID As String = String.Empty
                Dim SubsidariesCompanyDT As DataTable = DirectCast(ViewState("SubsidariesCompanyDT"), DataTable)

                If Not SubsidariesCompanyDT Is Nothing AndAlso SubsidariesCompanyDT.Rows.Count > 0 Then
                    For Each dr As DataRow In SubsidariesCompanyDT.Rows
                        If sCompanyID = "" Then
                            sCompanyID = Convert.ToString(dr("ID"))
                        Else
                            sCompanyID = sCompanyID & "," & Convert.ToString(dr("ID"))
                        End If
                    Next

                End If

                ' if Data found in  CompanyPaysProductCategories__c entity then bind grdCompanyPay
                Dim sSql As String = "..spGetIndividualPayData__c @CompanyID='" & sCompanyID & "'"
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    grdIndividualPay.DataSource = dt
                    grdIndividualPay.DataBind()
                    CurrentIndividualPayTable = dt
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Bind Person List as per Company 
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindPersonList(ByVal CompanyID As Integer)
            Try
                Dim sSqlPersonList = "..spGetPersonsListForWhoPays__c @CompanyID=" & CompanyID
                Dim dtPersonList As DataTable = DataAction.GetDataTable(sSqlPersonList)
                If Not dtPersonList Is Nothing AndAlso dtPersonList.Rows.Count > 0 Then
                    cmbPersons.DataSource = dtPersonList
                    cmbPersons.DataTextField = "FirstLast"
                    cmbPersons.DataValueField = "ID"
                    cmbPersons.DataBind()
                    cmbPersons.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Bind Product List as per product category
        ''' </summary>
        ''' <param name="ProductCategoryID"></param>
        ''' <remarks></remarks>
        Protected Sub BindProductList(ByVal ProductCategoryID As Long)
            Try
                ' Get All Products Categories
                Dim sSQLProductList As String = "..spGetProductsList__c @ProductCategoryID=" & ProductCategoryID
                Dim dtProductList As DataTable = DataAction.GetDataTable(sSQLProductList)
                If Not dtProductList Is Nothing AndAlso dtProductList.Rows.Count > 0 Then
                    'Bind Individual Product drop down
                    cmbProductList.DataSource = dtProductList
                    cmbProductList.DataTextField = "Name"
                    cmbProductList.DataValueField = "ID"
                    cmbProductList.DataBind()
                    cmbProductList.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                Else
                    cmbProductList.Items.Clear()
                    cmbProductList.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadCompanyProductList(ByVal ProductCategoryID As Long)
            Try
                ' Get All Products Categories
                Dim sSQLProductList As String = "..spGetProductsList__c @ProductCategoryID=" & ProductCategoryID
                Dim dtProductList As DataTable = DataAction.GetDataTable(sSQLProductList)
                If Not dtProductList Is Nothing AndAlso dtProductList.Rows.Count > 0 Then
                    cmbProduct.DataSource = dtProductList
                    cmbProduct.DataTextField = "Name"
                    cmbProduct.DataValueField = "ID"
                    cmbProduct.DataBind()
                    cmbProduct.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                Else
                    cmbProduct.Items.Clear()
                    cmbProduct.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Load Member Types
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadMemberType()
            Try
                Dim sSql As String = Database & "..spGetMemberTypes__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    cmbMemberType.DataSource = dt
                    cmbMemberType.DataTextField = "Name"
                    cmbMemberType.DataValueField = "ID"
                    cmbMemberType.DataBind()
                    cmbMemberType.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))


                    cmbMemberTypeIndi.DataSource = dt
                    cmbMemberTypeIndi.DataTextField = "Name"
                    cmbMemberTypeIndi.DataValueField = "ID"
                    cmbMemberTypeIndi.DataBind()
                    cmbMemberTypeIndi.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Load Primary functions
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadRole()
            Try
                Dim sSql As String = Database & "..spGetPrimaryFunction__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    cmbRole.DataSource = dt
                    cmbRole.DataTextField = "Name"
                    cmbRole.DataValueField = "ID"
                    cmbRole.DataBind()
                    cmbRole.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))

                    cmbRoleIndi.DataSource = dt
                    cmbRoleIndi.DataTextField = "Name"
                    cmbRoleIndi.DataValueField = "ID"
                    cmbRoleIndi.DataBind()
                    cmbRoleIndi.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadSubsidiariesCompanyList()
            Try
                Dim sSqlCompanyID As String = "..spCompanyIDForPerson @PersonID=" & User1.PersonID
                Dim lCompanyID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlCompanyID, IAptifyDataAction.DSLCacheSetting.BypassCache))
                Dim sSql As String = Database & "..spGetAllSubsidiariesWithCityName__c @ParentCompanyId=" & lCompanyID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    cmbSubsidarisCompney.ClearSelection()
                    cmbSubsidarisCompney.DataSource = dt
                    cmbSubsidarisCompney.DataTextField = "Name"
                    cmbSubsidarisCompney.DataValueField = "ID"
                    cmbSubsidarisCompney.DataBind()

                    cmbIndividualCompany.ClearSelection()
                    cmbIndividualCompany.DataSource = dt
                    cmbIndividualCompany.DataTextField = "Name"
                    cmbIndividualCompany.DataValueField = "ID"
                    cmbIndividualCompany.DataBind()
                    ViewState("SubsidariesCompanyDT") = dt
                Else
                    ViewState("SubsidariesCompanyDT") = Nothing
                End If
                SetComboValue(cmbSubsidarisCompney, Convert.ToString(AptifyApplication.GetEntityRecordName("Companies", lCompanyID)))
                SetComboValue(cmbIndividualCompany, Convert.ToString(AptifyApplication.GetEntityRecordName("Companies", lCompanyID)))

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList, _
                                  ByVal sValue As String)
            Dim i As Integer

            Try
                cmb.ClearSelection()
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
#End Region
#Region "Drop Down Events"
        ''' <summary>
        ''' Selection on Product Category bind Product list
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub cmbProductCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProductCategory.SelectedIndexChanged
            Try
                If Convert.ToString(cmbProductCategory.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    LoadCompanyProductList(Convert.ToInt32(cmbProductCategory.SelectedValue)) 'As Per product Category display Product List
                Else
                    '   BindLoadProducts() ' Display All product list
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub cmbProductCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProductCat.SelectedIndexChanged
            Try
                If Convert.ToString(cmbProductCat.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    BindProductList(Convert.ToInt32(cmbProductCat.SelectedValue)) 'As Per product Category display Product List
                Else
                    ' BindLoadProducts() ' Display All product list
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
#Region "Static Data Table"
        ''' <summary>
        ''' Create Company Pay Data table 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CurrentCompanyPayTable() As DataTable
            Get

                If Not Session("CompanyPayTable") Is Nothing Then
                    Return CType(Session("CompanyPayTable"), DataTable)
                Else
                    Dim dtCompanyPayTable As New DataTable
                    dtCompanyPayTable.Columns.Add("ID")
                    dtCompanyPayTable.Columns.Add("ProductCategoryID")
                    dtCompanyPayTable.Columns.Add("ProductID")
                    dtCompanyPayTable.Columns.Add("ProductCategory")
                    dtCompanyPayTable.Columns.Add("Product")
                    dtCompanyPayTable.Columns.Add("StartDate")
                    dtCompanyPayTable.Columns.Add("EndDate")
                    dtCompanyPayTable.Columns.Add("MemberTypeID")
                    dtCompanyPayTable.Columns.Add("MemberType")
                    dtCompanyPayTable.Columns.Add("RoleID")
                    dtCompanyPayTable.Columns.Add("Role")
                    dtCompanyPayTable.Columns.Add("CompanyID")
                    dtCompanyPayTable.Columns.Add("Company")
                    Return dtCompanyPayTable
                End If
            End Get
            Set(ByVal value As DataTable)
                Session("CompanyPayTable") = value
            End Set
        End Property
        ''' <summary>
        ''' Create Individual Pay Data table 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CurrentIndividualPayTable() As DataTable
            Get

                If Not Session("IndividualPayTable") Is Nothing Then
                    Return CType(Session("IndividualPayTable"), DataTable)
                Else
                    Dim dtIndividualPayTable As New DataTable
                    dtIndividualPayTable.Columns.Add("ID")
                    dtIndividualPayTable.Columns.Add("PayType")
                    dtIndividualPayTable.Columns.Add("PersonID")
                    dtIndividualPayTable.Columns.Add("Person")
                    dtIndividualPayTable.Columns.Add("ProductCategoryID")
                    dtIndividualPayTable.Columns.Add("ProductID")
                    dtIndividualPayTable.Columns.Add("ProductCategory")
                    dtIndividualPayTable.Columns.Add("Product")
                    dtIndividualPayTable.Columns.Add("StartDate")
                    dtIndividualPayTable.Columns.Add("EndDate")
                    dtIndividualPayTable.Columns.Add("MemberTypeID")
                    dtIndividualPayTable.Columns.Add("MemberType")
                    dtIndividualPayTable.Columns.Add("RoleID")
                    dtIndividualPayTable.Columns.Add("Role")
                    dtIndividualPayTable.Columns.Add("CompanyID")
                    dtIndividualPayTable.Columns.Add("Company")
                    Return dtIndividualPayTable
                End If
            End Get
            Set(ByVal value As DataTable)
                Session("IndividualPayTable") = value
            End Set
        End Property

        ''' <summary>
        ''' Create Deleted items Data table 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CurrentIndividualPayDeleteTable() As DataTable
            Get

                If Not Session("IndividualPayDeleteTable") Is Nothing Then
                    Return CType(Session("IndividualPayDeleteTable"), DataTable)
                Else
                    Dim dtIndividualPayDeleteTable As New DataTable
                    dtIndividualPayDeleteTable.Columns.Add("PayType")
                    dtIndividualPayDeleteTable.Columns.Add("PersonID")
                    dtIndividualPayDeleteTable.Columns.Add("ProductCategoryID")
                    dtIndividualPayDeleteTable.Columns.Add("ProductID")
                    dtIndividualPayDeleteTable.Columns.Add("StartDate")
                    dtIndividualPayDeleteTable.Columns.Add("EndDate")
                    dtIndividualPayDeleteTable.Columns.Add("MemberTypeID")
                    dtIndividualPayDeleteTable.Columns.Add("RoleID")
                    dtIndividualPayDeleteTable.Columns.Add("ID")
                    Return dtIndividualPayDeleteTable
                End If
            End Get
            Set(ByVal value As DataTable)
                Session("IndividualPayDeleteTable") = value
            End Set
        End Property
        Public Property CurrentCompanyPayDeleteTable() As DataTable
            Get

                If Not Session("CompanyPayDeleteTable") Is Nothing Then
                    Return CType(Session("CompanyPayDeleteTable"), DataTable)
                Else
                    Dim dtCompanyPayDeleteTable As New DataTable
                    dtCompanyPayDeleteTable.Columns.Add("ProductCategoryID")
                    dtCompanyPayDeleteTable.Columns.Add("ProductID")
                    dtCompanyPayDeleteTable.Columns.Add("StartDate")
                    dtCompanyPayDeleteTable.Columns.Add("EndDate")
                    dtCompanyPayDeleteTable.Columns.Add("MemberTypeID")
                    dtCompanyPayDeleteTable.Columns.Add("RoleID")
                    dtCompanyPayDeleteTable.Columns.Add("CompanyID")
                    dtCompanyPayDeleteTable.Columns.Add("ID")
                    Return dtCompanyPayDeleteTable
                End If
            End Get
            Set(ByVal value As DataTable)
                Session("CompanyPayDeleteTable") = value
            End Set
        End Property
#End Region
#Region "Button Click"
        ''' <summary>
        ''' When Click on Add button on Companys Pay then added into Company Who pays Grid and display on Web page
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnADD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnADD.Click
            Try
                lblSucMsg.Visible = False
                If Page.IsValid Then
                    If Convert.ToString(cmbProductCategory.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                        lblMsg.Visible = False
                        Dim dtCurrentCompanyPayTable As DataTable = CurrentCompanyPayTable
                        Dim drCurrentCompanyPayTable As DataRow = dtCurrentCompanyPayTable.NewRow()
                        drCurrentCompanyPayTable("ID") = 0
                        If Convert.ToString(cmbProductCategory.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            drCurrentCompanyPayTable("ProductCategoryID") = cmbProductCategory.SelectedValue
                            drCurrentCompanyPayTable("ProductCategory") = cmbProductCategory.SelectedItem
                        End If
                        If Convert.ToString(cmbProduct.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            drCurrentCompanyPayTable("ProductID") = cmbProduct.SelectedValue
                            drCurrentCompanyPayTable("Product") = cmbProduct.SelectedItem
                        End If
                        If Convert.ToString(cmbMemberType.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            drCurrentCompanyPayTable("MemberTypeID") = cmbMemberType.SelectedValue
                            drCurrentCompanyPayTable("MemberType") = cmbMemberType.SelectedItem
                        End If
                        If Convert.ToString(cmbRole.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            drCurrentCompanyPayTable("RoleID") = cmbRole.SelectedValue
                            drCurrentCompanyPayTable("Role") = cmbRole.SelectedItem
                        End If
                        drCurrentCompanyPayTable("CompanyID") = cmbSubsidarisCompney.SelectedValue
                        drCurrentCompanyPayTable("Company") = cmbSubsidarisCompney.SelectedItem
                        Try
                            If Not String.IsNullOrEmpty(txtStartDate.SelectedDate.ToString().Trim()) Then
                                '   drCurrentCompanyPayTable("StartDate") = Convert.ToDateTime(txtStartDate.Text).ToShortDateString()
                                drCurrentCompanyPayTable("StartDate") = txtStartDate.SelectedDate.Value.ToShortDateString()
                            End If
                        Catch ex As Exception
                            lblMsg.Visible = True
                            lblMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidStartDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                            Return
                        End Try
                        Try
                            If Not String.IsNullOrEmpty(txtEndDate.SelectedDate.ToString().Trim()) Then
                                drCurrentCompanyPayTable("EndDate") = txtEndDate.SelectedDate.Value.ToShortDateString()
                            End If
                        Catch ex As Exception
                            lblMsg.Visible = True
                            lblMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidEndDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                            Return
                        End Try
                        dtCurrentCompanyPayTable.Rows.Add(drCurrentCompanyPayTable)
                        CurrentCompanyPayTable = dtCurrentCompanyPayTable
                        grdCompanyPay.DataSource = CurrentCompanyPayTable
                        grdCompanyPay.DataBind()
                        grdCompanyPay.AllowPaging = True
                        grdCompanyPay.PageSize = 10
                        LoadSubsidiariesCompanyList()
                        BindProductCategory()
                        LoadCompanyProductList(0)
                        txtStartDate.SelectedDate = Nothing
                        txtEndDate.SelectedDate = Nothing
                        LoadMemberType()
                        LoadRole()

                    Else
                        lblMsg.Visible = True
                        lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategoryMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' When Click on Add button on Individual Pay then added into Individual Who pays Grid and display on Web page
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnIndiAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIndiAdd.Click
            Try
                lblSucMsg.Visible = False
                If Page.IsValid Then
                    If Convert.ToString(cmbProductCat.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                        If Convert.ToString(cmbPersons.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            lblIndivisualMsg.Visible = False

                            If Convert.ToString(cmbPersons.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectAll")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                Dim dtCurrentIndividualPayTable As DataTable = CurrentIndividualPayTable
                                Dim drCurrentIndividualPayTable As DataRow = dtCurrentIndividualPayTable.NewRow()
                                drCurrentIndividualPayTable("ID") = 0
                                drCurrentIndividualPayTable("PayType") = cmbPayType.SelectedValue.Trim
                                If Convert.ToString(cmbPersons.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                    drCurrentIndividualPayTable("PersonID") = cmbPersons.SelectedValue
                                    drCurrentIndividualPayTable("Person") = cmbPersons.SelectedItem
                                End If
                                If Convert.ToString(cmbProductCat.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                    drCurrentIndividualPayTable("ProductCategoryID") = cmbProductCat.SelectedValue
                                    drCurrentIndividualPayTable("ProductCategory") = cmbProductCat.SelectedItem
                                End If
                                If Convert.ToString(cmbProductList.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                    drCurrentIndividualPayTable("ProductID") = cmbProductList.SelectedValue
                                    drCurrentIndividualPayTable("Product") = cmbProductList.SelectedItem
                                End If
                                If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                    drCurrentIndividualPayTable("MemberTypeID") = cmbMemberTypeIndi.SelectedValue
                                    drCurrentIndividualPayTable("MemberType") = cmbMemberTypeIndi.SelectedItem
                                End If
                                If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                    drCurrentIndividualPayTable("RoleID") = cmbRoleIndi.SelectedValue
                                    drCurrentIndividualPayTable("Role") = cmbRoleIndi.SelectedItem
                                End If
                                drCurrentIndividualPayTable("CompanyID") = cmbIndividualCompany.SelectedValue
                                drCurrentIndividualPayTable("Company") = cmbIndividualCompany.SelectedItem
                                Try
                                    If Not String.IsNullOrEmpty(txtIndiStartDate.SelectedDate.ToString().Trim()) Then
                                        drCurrentIndividualPayTable("StartDate") = txtIndiStartDate.SelectedDate.Value.ToShortDateString()
                                    End If
                                Catch ex As Exception
                                    lblIndivisualMsg.Visible = True
                                    lblIndivisualMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidStartDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                                    Return
                                End Try
                                Try
                                    If Not String.IsNullOrEmpty(txtIndiEndDate.SelectedDate.ToString().Trim()) Then
                                        drCurrentIndividualPayTable("EndDate") = txtIndiEndDate.SelectedDate.Value.ToShortDateString()
                                    End If
                                Catch ex As Exception
                                    lblIndivisualMsg.Visible = True
                                    lblIndivisualMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidEndDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                                    Return
                                End Try
                                dtCurrentIndividualPayTable.Rows.Add(drCurrentIndividualPayTable)
                                CurrentIndividualPayTable = dtCurrentIndividualPayTable
                                grdIndividualPay.DataSource = CurrentIndividualPayTable
                                grdIndividualPay.DataBind()
                                grdIndividualPay.AllowPaging = True
                                grdIndividualPay.PageSize = 10
                            ElseIf Convert.ToString(cmbPersons.SelectedValue).Trim.ToLower = Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectAll")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                Dim dtAllPersons As DataTable = CType(ViewState("AllPersons"), DataTable)
                                If Not dtAllPersons Is Nothing AndAlso dtAllPersons.Rows.Count > 0 Then
                                    For Each dr As DataRow In dtAllPersons.Rows
                                        Dim dtCurrentIndividualPayTable As DataTable = CurrentIndividualPayTable
                                        Dim drCurrentIndividualPayTable As DataRow = dtCurrentIndividualPayTable.NewRow()
                                        drCurrentIndividualPayTable("ID") = 0
                                        drCurrentIndividualPayTable("PayType") = cmbPayType.SelectedValue.Trim
                                        If Convert.ToString(cmbPersons.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                            drCurrentIndividualPayTable("PersonID") = dr("ID")
                                            drCurrentIndividualPayTable("Person") = dr("FirstLast")
                                        End If
                                        If Convert.ToString(cmbProductCat.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                            drCurrentIndividualPayTable("ProductCategoryID") = cmbProductCat.SelectedValue
                                            drCurrentIndividualPayTable("ProductCategory") = cmbProductCat.SelectedItem
                                        End If
                                        If Convert.ToString(cmbProductList.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                            drCurrentIndividualPayTable("ProductID") = cmbProductList.SelectedValue
                                            drCurrentIndividualPayTable("Product") = cmbProductList.SelectedItem
                                        End If
                                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                            drCurrentIndividualPayTable("MemberTypeID") = cmbMemberTypeIndi.SelectedValue
                                            drCurrentIndividualPayTable("MemberType") = cmbMemberTypeIndi.SelectedItem
                                        End If
                                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                            drCurrentIndividualPayTable("RoleID") = cmbRoleIndi.SelectedValue
                                            drCurrentIndividualPayTable("Role") = cmbRoleIndi.SelectedItem
                                        End If
                                        drCurrentIndividualPayTable("CompanyID") = cmbIndividualCompany.SelectedValue
                                        drCurrentIndividualPayTable("Company") = cmbIndividualCompany.SelectedItem
                                        Try
                                            If Not String.IsNullOrEmpty(txtIndiStartDate.SelectedDate.ToString().Trim()) Then
                                                drCurrentIndividualPayTable("StartDate") = txtIndiStartDate.SelectedDate.Value.ToShortDateString()
                                            End If
                                        Catch ex As Exception
                                            lblIndivisualMsg.Visible = True
                                            lblIndivisualMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidStartDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                                            Return
                                        End Try
                                        Try
                                            If Not String.IsNullOrEmpty(txtIndiEndDate.SelectedDate.ToString().Trim()) Then
                                                drCurrentIndividualPayTable("EndDate") = txtIndiEndDate.SelectedDate.Value.ToShortDateString()
                                            End If
                                        Catch ex As Exception
                                            lblIndivisualMsg.Visible = True
                                            lblIndivisualMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidEndDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                                            Return
                                        End Try
                                        dtCurrentIndividualPayTable.Rows.Add(drCurrentIndividualPayTable)
                                        CurrentIndividualPayTable = dtCurrentIndividualPayTable
                                        grdIndividualPay.DataSource = CurrentIndividualPayTable
                                        grdIndividualPay.DataBind()
                                        grdIndividualPay.AllowPaging = True
                                        grdIndividualPay.PageSize = 10
                                    Next
                                End If
                            End If
                            LoadSubsidiariesCompanyList()
                            BindProductCategory()
                            BindProductList(0)
                            txtIndiStartDate.SelectedDate = Nothing
                            txtIndiEndDate.SelectedDate = Nothing
                            cmbPayType.SelectedIndex = 0
                            BindPersonList(Convert.ToInt32(cmbIndividualCompany.SelectedValue))
                            LoadMemberType()
                            LoadRole()



                        Else
                            lblIndivisualMsg.Visible = True
                            lblIndivisualMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPersonMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        End If
                    Else
                        lblIndivisualMsg.Visible = True
                        lblIndivisualMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategoryMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Click on Submit button then Save the record on Person and Companies who pays sub entities
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
            Try
                ViewState("AllPersons") = Nothing
                SaveCompanyPaysData() ' Save Company Who pays Data
                SavePersonsPayTypeData()
                lblSucMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WhoPays.SaveMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblSucMsg.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
#Region "Save Methods"
        ''' <summary>
        ''' Submit Company Pays data to the data base
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub SaveCompanyPaysData()
            Try
                Dim sError As String = Nothing
                Dim oCompanyGE As AptifyGenericEntityBase
                Dim dtCompanyDetailsDataTable As DataTable = New DataTable
                dtCompanyDetailsDataTable.Columns.Add("CompanyID")
                If Not CurrentCompanyPayTable Is Nothing AndAlso CurrentCompanyPayTable.Rows.Count > 0 Then
                    For Each dr As DataRow In CurrentCompanyPayTable.Rows
                        If Not dr.RowState = DataRowState.Deleted AndAlso Not dr.RowState = DataRowState.Detached Then
                            ' added all compnies with new datatable for next time we have get all companies records 
                            Dim drCompanyDetailsDataTable As DataRow = dtCompanyDetailsDataTable.NewRow()
                            drCompanyDetailsDataTable("CompanyID") = dr("CompanyID")
                            dtCompanyDetailsDataTable.Rows.Add(drCompanyDetailsDataTable)
                        End If
                    Next
                End If

                If Not CurrentCompanyPayTable Is Nothing AndAlso CurrentCompanyPayTable.Rows.Count > 0 Then
                    For Each dr As DataRow In CurrentCompanyPayTable.Rows
                        oCompanyGE = Me.AptifyApplication.GetEntityObject("Companies", CLng(dr("CompanyID")))
                        ' oCompanyGE.SubTypes("CompanyPaysProductCategories__c").Clear()
                        ClearCompanyData()
                        If Convert.ToInt32(dr("ID")) = 0 Then
                            With oCompanyGE.SubTypes("CompanyPaysProductCategories__c").Add()
                                If IsDBNull(dr("ProductCategoryID")) = False Then
                                    .SetValue("ProductCategoryID", Convert.ToInt32(dr("ProductCategoryID")))
                                End If
                                If IsDBNull(dr("ProductID")) = False Then
                                    .SetValue("ProductID", Convert.ToInt32(dr("ProductID")))
                                End If
                                .SetValue("StartDate", dr("StartDate"))
                                .SetValue("EndDate", dr("EndDate"))
                                .SetValue("MemberTypeID", dr("MemberTypeID"))
                                .SetValue("PrimaryFunctionID", dr("RoleID"))
                            End With
                        End If
                        CheckQuatationOrderUpdateCompanyData()
                        If Not oCompanyGE.Save(sError) Then
                            lblMsg.Text = sError
                        End If
                    Next
                Else
                    ClearCompanyData()
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' If Person Select All Company then we add record Parant as well as Child Company Sub type
        ''' Also Disucss with Rahul regarding Performance issue
        ''' </summary>
        ''' <param name="ProductCatID"></param>
        ''' <param name="ProductID"></param>
        ''' <param name="StartDate"></param>
        ''' <param name="EndDate"></param>
        ''' <remarks></remarks>
        Protected Sub AllCompanySaveInfo(ByVal ProductCatID As Integer, ByVal ProductID As Integer, ByVal StartDate As String, ByVal EndDate As String)
            Try
                Dim sError As String = String.Empty
                Dim dtCompanyData As DataTable = CType(Session("CompanyList"), DataTable)
                If Not dtCompanyData Is Nothing AndAlso dtCompanyData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtCompanyData.Rows
                        Dim oCompanyGE As AptifyGenericEntityBase
                        oCompanyGE = Me.AptifyApplication.GetEntityObject("Companies", CInt(dr("ID")))
                        oCompanyGE.SubTypes("CompanyPaysProductCategories__c").Clear()
                        With oCompanyGE.SubTypes("CompanyPaysProductCategories__c").Add()
                            If ProductCatID > 0 Then
                                .SetValue("ProductCategoryID", ProductCatID)
                            End If
                            If ProductID > 0 Then
                                .SetValue("ProductID", ProductID)
                            End If
                            .SetValue("StartDate", StartDate)
                            .SetValue("EndDate", EndDate)
                        End With
                        If Not oCompanyGE.Save(sError) Then
                            lblMsg.Text = sError
                        End If
                    Next
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub CheckQuatationOrderUpdateCompanyData()
            Try
                If Not CurrentCompanyPayTable Is Nothing AndAlso CurrentCompanyPayTable.Rows.Count > 0 Then
                    For Each drCurrentCompanyPay As DataRow In CurrentCompanyPayTable.Rows
                        Dim sISProductId As String = String.Empty
                        If IsDBNull(drCurrentCompanyPay("ProductID")) = False Then
                            sISProductId = Convert.ToString(drCurrentCompanyPay("ProductID"))
                        Else
                            sISProductId = "0"
                        End If
                        'Dim sIsStartDate As String = String.Empty
                        'Dim sIsEndDate As String = String.Empty
                        'If Convert.ToString(drCurrentCompanyPay("StartDate")) <> "" AndAlso Convert.ToString(drCurrentCompanyPay("StartDate")) <> "1900-1-1" Then
                        '    sIsStartDate = Convert.ToString(drCurrentCompanyPay("StartDate"))
                        'End If
                        'If Convert.ToString(drCurrentCompanyPay("EndDate")) <> "" AndAlso Convert.ToString(drCurrentCompanyPay("EndDate")) <> "1900-1-1" Then
                        '    sIsEndDate = Convert.ToString(drCurrentCompanyPay("EndDate"))
                        'End If
                        '--------------------------------------
                        Dim sIsStartDate As String = String.Empty
                        If Not drCurrentCompanyPay("StartDate") Is Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(drCurrentCompanyPay("StartDate"))) AndAlso Convert.ToString(drCurrentCompanyPay("StartDate")) <> "1900-1-1" Then
                            sIsStartDate = Convert.ToString(drCurrentCompanyPay("StartDate"))
                        End If
                        Dim sIsEndDate As String = String.Empty
                        If Not drCurrentCompanyPay("EndDate") Is Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(drCurrentCompanyPay("EndDate"))) AndAlso Convert.ToString(drCurrentCompanyPay("EndDate")) <> "1900-1-1" Then
                            sIsEndDate = Convert.ToString(drCurrentCompanyPay("EndDate"))
                        End If
                        Dim sIsMemberTypeID As String = String.Empty
                        If Not drCurrentCompanyPay("MemberTypeID") Is Nothing AndAlso IsDBNull(drCurrentCompanyPay("MemberTypeID")) = False Then
                            sIsMemberTypeID = Convert.ToString(drCurrentCompanyPay("MemberTypeID"))
                        End If
                        Dim sRoleID As String = String.Empty
                        If Not drCurrentCompanyPay("RoleID") Is Nothing AndAlso IsDBNull(drCurrentCompanyPay("RoleID")) = False Then
                            sRoleID = Convert.ToString(drCurrentCompanyPay("RoleID"))
                        End If
                        Dim params(6) As System.Data.IDataParameter
                        params(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.VarChar, Convert.ToString(drCurrentCompanyPay("CompanyID")))
                        params(1) = DataAction.GetDataParameter("@ProductCategoryID", SqlDbType.VarChar, Convert.ToString(drCurrentCompanyPay("ProductCategoryID")))
                        params(2) = DataAction.GetDataParameter("@ProductID", SqlDbType.VarChar, sISProductId)

                        If Not String.IsNullOrEmpty(sIsStartDate) Then
                            params(3) = DataAction.GetDataParameter("@StartDate", SqlDbType.VarChar, New System.DateTime(Year(CDate(sIsStartDate)), Month(CDate(sIsStartDate)), Day(CDate(sIsStartDate))))
                        End If
                        If Not String.IsNullOrEmpty(sIsEndDate) Then
                            params(4) = DataAction.GetDataParameter("@EndDate", SqlDbType.VarChar, New System.DateTime(Year(CDate(sIsEndDate)), Month(CDate(sIsEndDate)), Day(CDate(sIsEndDate))))
                        Else
                            params(4) = DataAction.GetDataParameter("@EndDate", SqlDbType.VarChar, Nothing)
                        End If
                        If Not String.IsNullOrEmpty(sIsMemberTypeID) Then
                            params(5) = DataAction.GetDataParameter("@MemberTypeID", SqlDbType.VarChar, sIsMemberTypeID)
                        Else
                            params(5) = DataAction.GetDataParameter("@MemberTypeID", SqlDbType.VarChar, Nothing)
                        End If
                        If Not String.IsNullOrEmpty(sRoleID) Then
                            params(6) = DataAction.GetDataParameter("@RoleID", SqlDbType.VarChar, sRoleID)
                        Else
                            params(6) = DataAction.GetDataParameter("@RoleID", SqlDbType.VarChar, Nothing)
                        End If
                        Dim sSQL As String = Database & "..spUpdateWhoPayCompanyOrder__c"
                        'Dim sSQL As String = "..spUpdateWhoPayCompanyOrder__c @CompanyID=" & User1.CompanyID & ",@ProductCategoryID=" & Convert.ToString(drCurrentCompanyPay("ProductCategoryID")) & ",@ProductID=" & sISProductId & ",@StartDate='" & New DateTime(Year(CDate(sIsStartDate)), Month(CDate(sIsStartDate)), Day(CDate(sIsStartDate))) & "',@EndDate='" & New DateTime(Year(CDate(sIsEndDate)), Month(CDate(sIsEndDate)), Day(CDate(sIsEndDate))) & "'"
                        'Dim dt As DataTable = DataAction.GetDataTable(sSQL)
                        Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                        '--------------------------------------
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            
                            For Each updateDr As DataRow In dt.Rows
                                Dim oOrderGE As AptifyGenericEntityBase
                                oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(updateDr("ID")))
                                oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", drCurrentCompanyPay("CompanyID"))
                                oOrderGE.Save()
                                ''  If oOrderGE.SubTypes("OrderLines").Count > 1 Then

                                ''    ' Code added by govind for redmine issue create seprate order whoes having Bill TO Company
                                ''     ' Redmine issue split orders
                                ''      Dim oNewOrderGE As Aptify.Applications.OrderEntry.OrdersEntity
                                ''   oNewOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                                ''     oNewOrderGE.ShipToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                                ''     oNewOrderGE.BillToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                                ''    ' oNewOrderGE.SetValue("FirmPay__c", 1)
                                ''     oNewOrderGE.AddProduct(CLng(updateDr("ProductID")))

                                ''     oNewOrderGE.SubTypes("OrderLines").Item(oNewOrderGE.SubTypes("OrderLines").Count - 1).SetValue("BillToCompanyID__c", drCurrentCompanyPay("CompanyID"))
                                ''     oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).Delete()
                                ''      If oNewOrderGE.SubTypes("OrderLines").Count > 0 Then
                                ''         oNewOrderGE.OrderType = OrderType.Quotation
                                ''         oNewOrderGE.Save()
                                ''      End If
                                ''   Dim sError As String = String.Empty
                                ''     If oOrderGE.Save(False, sError) Then
                                ''          oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                ''          oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                ''          oOrderGE.SetValue("BillToCompanyID", -1)
                                ''          oOrderGE.SetValue("FirmPay__c", 0)
                                ''        If oOrderGE.Save(False, sError) Then
                                ''     ' Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New Exception(oOrderGE.LastUserError))
                                ''        End If
                                '' End If
                                ''Else
                                ''     oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", drCurrentCompanyPay("CompanyID"))
                                ''        Dim sSQLBillingContact As String = "..spGetBillingContactID__c @CompanyID=" & Convert.ToInt32(drCurrentCompanyPay("CompanyID"))
                                ''     Dim iBillingContactID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQLBillingContact))
                                ''     If iBillingContactID > 0 Then
                                ''         oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                ''         oOrderGE.SetValue("BillToID", iBillingContactID)
                                ''     End If
                                ''        Dim sError As String = String.Empty
                                ''      If oOrderGE.Save(False, sError) Then

                                ''      End If
                                '' End If
                               
                                
                            Next
                            
                        End If
                    Next
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub ClearCompanyData()
            Try
                ' first check deleted into the current subtype in grid
                If Not CurrentCompanyPayDeleteTable Is Nothing AndAlso CurrentCompanyPayDeleteTable.Rows.Count > 0 Then
                    For Each dr As DataRow In CurrentCompanyPayDeleteTable.Rows
                        Dim IsMatched As Boolean = False
                        If Not CurrentCompanyPayTable Is Nothing AndAlso CurrentCompanyPayTable.Rows.Count > 0 Then
                            For Each drCurrentCompanyPay As DataRow In CurrentCompanyPayTable.Rows
                                If Convert.ToInt32(dr("ProductCategoryID")) = Convert.ToInt32(drCurrentCompanyPay("ProductCategoryID")) AndAlso Convert.ToInt32(dr("ProductID")) = Convert.ToInt32(drCurrentCompanyPay("ProductID")) AndAlso Convert.ToString(dr("StartDate")).Trim.ToLower = Convert.ToString(drCurrentCompanyPay("StartDate")).Trim.ToLower AndAlso Convert.ToString(dr("EndDate")).Trim.ToLower = Convert.ToString(drCurrentCompanyPay("EndDate")).Trim.ToLower AndAlso Convert.ToString(dr("CompanyID")) = Convert.ToString(drCurrentCompanyPay("CompanyID")) Then
                                    IsMatched = True
                                    Exit For
                                End If
                            Next
                        End If
                        If IsMatched = False Then
                            ' then update the order details
                            Dim sISProductId As String = String.Empty
                            If Not dr("ProductID") Is Nothing AndAlso Convert.ToInt32(dr("ProductID")) > 0 Then
                                sISProductId = Convert.ToString(dr("ProductID"))
                            Else
                                sISProductId = "0"
                            End If
                            '------------By Vaishali-------------------------------------------
                            'Dim sIsStartDate As String = String.Empty
                            'If Convert.ToString(dr("StartDate")) <> "" AndAlso Convert.ToString(dr("StartDate")) <> "1900-1-1" Then
                            '    sIsStartDate = Convert.ToString(dr("StartDate"))
                            'End If
                            'Dim sIsEndDate As String = String.Empty
                            'If Convert.ToString(dr("EndDate")) <> "" AndAlso Convert.ToString(dr("EndDate")) <> "1900-1-1" Then
                            '    sIsEndDate = Convert.ToString(dr("EndDate"))
                            'End If
                            'Dim sSQL As String = "..spUpdateWhoPayCompanyOrder__c @CompanyID=" & User1.CompanyID & ",@ProductCategoryID=" & Convert.ToString(dr("ProductCategoryID")) & ",@ProductID=" & sISProductId & ",@StartDate='" & New DateTime(Year(CDate(sIsStartDate)), Month(CDate(sIsStartDate)), Day(CDate(sIsStartDate))) & "',@EndDate='" & New DateTime(Year(CDate(sIsEndDate)), Month(CDate(sIsEndDate)), Day(CDate(sIsEndDate))) & "'"
                            'Dim dt As DataTable = DataAction.GetDataTable(sSQL)
                            Dim sIsStartDate As String = String.Empty
                            If Not dr("StartDate") Is Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(dr("StartDate"))) AndAlso Convert.ToString(dr("StartDate")) <> "1900-1-1" Then
                                sIsStartDate = Convert.ToString(dr("StartDate"))
                            End If
                            Dim sIsEndDate As String = String.Empty
                            If Not dr("EndDate") Is Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(dr("EndDate"))) AndAlso Convert.ToString(dr("EndDate")) <> "1900-1-1" Then
                                sIsEndDate = Convert.ToString(dr("EndDate"))
                            End If
                            Dim sIsMemberTypeID As String = String.Empty
                            If Not dr("MemberTypeID") Is Nothing AndAlso IsDBNull(dr("MemberTypeID")) = False Then
                                sIsMemberTypeID = Convert.ToString(dr("MemberTypeID"))
                            End If
                            Dim sRoleID As String = String.Empty
                            If Not dr("RoleID") Is Nothing AndAlso IsDBNull(dr("RoleID")) = False Then
                                sRoleID = Convert.ToString(dr("RoleID"))
                            End If
                            Dim sCompanyID As String = String.Empty
                            If Not dr("CompanyID") Is Nothing AndAlso Convert.ToInt32(dr("CompanyID")) > 0 Then
                                sCompanyID = Convert.ToString(dr("CompanyID"))
                            Else
                                sCompanyID = "0"
                            End If
                            Dim params(6) As System.Data.IDataParameter
                            params(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.VarChar, Convert.ToString(sCompanyID))
                            params(1) = DataAction.GetDataParameter("@ProductCategoryID", SqlDbType.VarChar, Convert.ToString(dr("ProductCategoryID")))
                            params(2) = DataAction.GetDataParameter("@ProductID", SqlDbType.VarChar, sISProductId)

                            If Not String.IsNullOrEmpty(sIsStartDate) Then
                                params(3) = DataAction.GetDataParameter("@StartDate", SqlDbType.VarChar, New System.DateTime(Year(CDate(sIsStartDate)), Month(CDate(sIsStartDate)), Day(CDate(sIsStartDate))))
                            End If
                            If Not String.IsNullOrEmpty(sIsEndDate) Then
                                params(4) = DataAction.GetDataParameter("@EndDate", SqlDbType.VarChar, New System.DateTime(Year(CDate(sIsEndDate)), Month(CDate(sIsEndDate)), Day(CDate(sIsEndDate))))
                            Else
                                params(4) = DataAction.GetDataParameter("@EndDate", SqlDbType.VarChar, Nothing)
                            End If
                            If Not String.IsNullOrEmpty(sIsMemberTypeID) Then
                                params(5) = DataAction.GetDataParameter("@MemberTypeID", SqlDbType.VarChar, sIsMemberTypeID)
                            Else
                                params(5) = DataAction.GetDataParameter("@MemberTypeID", SqlDbType.VarChar, Nothing)
                            End If
                            If Not String.IsNullOrEmpty(sRoleID) Then
                                params(6) = DataAction.GetDataParameter("@RoleID", SqlDbType.VarChar, sRoleID)
                            Else
                                params(6) = DataAction.GetDataParameter("@RoleID", SqlDbType.VarChar, Nothing)
                            End If
                            Dim sSQL As String = Database & "..spUpdateWhoPayCompanyOrder__c"
                            Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                            '-------------------------------------------------------
                            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                                For Each updateDr As DataRow In dt.Rows
                                    Dim oOrderGE As AptifyGenericEntityBase
                                    oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(updateDr("ID")))
                                    If Convert.ToInt32(updateDr("BillToCompanyID__c")) > 0 Then
                                        oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", -1)
                                         If oOrderGE.Save() Then
                                            oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                            oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                            oOrderGE.SetValue("BillToCompanyID", -1)
                                            oOrderGE.SetValue("FirmPay__c", 0)
                                            oOrderGE.Save()
                                        End If
                                    End If
                                Next
                            End If
                            ' clear company data
                            If Convert.ToInt32(dr("ID")) > 0 Then
                                Dim oCompanyGE As AptifyGenericEntityBase
                                oCompanyGE = Me.AptifyApplication.GetEntityObject("Companies", Convert.ToInt32(dr("CompanyID")))
                                oCompanyGE.SubTypes("CompanyPaysProductCategories__c").Find("ID", dr("ID")).Delete()
                                Dim sErrorCompany As String = String.Empty
                                If oCompanyGE.Save(False, sErrorCompany) Then
                                Else
                                    Dim sErrorCompanys As String = sErrorCompany
                                End If
                            End If
                        End If

                    Next
                End If
                CurrentCompanyPayDeleteTable = Nothing
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Save Person Who pays
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub SavePersonsPayTypeData()
            Try
                Dim sError As String = Nothing
                Dim oPersonGE As AptifyGenericEntityBase
                '  ClearWhoPaysPersonRecord()
                DeletePersonRecord()
                CheckQuatationOrderUpdateIndividualData()
                If Not CurrentIndividualPayTable Is Nothing AndAlso CurrentIndividualPayTable.Rows.Count > 0 Then
                    For Each dr As DataRow In CurrentIndividualPayTable.Rows
                        If Not dr.RowState = DataRowState.Deleted AndAlso Not dr.RowState = DataRowState.Detached Then
                            oPersonGE = Me.AptifyApplication.GetEntityObject("Persons", Convert.ToInt32(dr("PersonID")))
                            ' oPersonGE.SubTypes("PersonWhoPays__c").Clear()
                            For Each dr1 As DataRow In CurrentIndividualPayTable.Rows
                                If Not dr1.RowState = DataRowState.Deleted AndAlso Not dr1.RowState = DataRowState.Detached Then
                                    If oPersonGE.RecordID = Convert.ToInt64(dr1("PersonID")) Then
                                        If Convert.ToInt32(dr1("ID")) = 0 Then
                                            dr1("ID") = 1
                                            ' CurrentIndividualPayStudentTable.Rows(dr
                                            CurrentIndividualPayTable.AcceptChanges()
                                            With oPersonGE.SubTypes("PersonWhoPays__c").Add()
                                                .SetValue("WhoPays", dr1("PayType"))
                                                .SetValue("ProductCategoryID", Convert.ToInt32(dr1("ProductCategoryID")))
                                                If IsDBNull(dr1("ProductID")) = False Then
                                                    .SetValue("ProductID", Convert.ToInt32(dr1("ProductID")))
                                                End If
                                                .SetValue("StartDate", dr1("StartDate"))
                                                .SetValue("EndDate", dr1("EndDate"))
                                                .SetValue("MemberTypeID", dr1("MemberTypeID"))
                                                .SetValue("PrimaryFunctionID", dr1("RoleID"))
                                                .SetValue("CompanyID", dr1("CompanyID"))
                                            End With
                                        End If
                                    End If
                                End If
                            Next
                            sError = String.Empty
                            If Not oPersonGE.Save(sError) Then
                                lblMsg.Text = sError
                            End If
                        End If
                    Next
                    'For Each dr As DataRow In CurrentIndividualPayTable.Rows
                    '    If Not dr.RowState = DataRowState.Deleted AndAlso Not dr.RowState = DataRowState.Detached Then
                    '        oPersonGE = Me.AptifyApplication.GetEntityObject("Persons", Convert.ToInt32(dr("PersonID")))
                    '        'oPersonGE.SubTypes("PersonWhoPays__c").Clear()
                    '        With oPersonGE.SubTypes("PersonWhoPays__c").Add()
                    '            .SetValue("WhoPays", dr("PayType"))
                    '            .SetValue("ProductCategoryID", Convert.ToInt32(dr("ProductCategoryID")))
                    '            If IsDBNull(dr("ProductID")) = False Then
                    '                .SetValue("ProductID", Convert.ToInt32(dr("ProductID")))
                    '            End If
                    '            .SetValue("StartDate", dr("StartDate"))
                    '            .SetValue("EndDate", dr("EndDate"))
                    '        End With
                    '        sError = String.Empty
                    '        If Not oPersonGE.Save(sError) Then
                    '            lblMsg.Text = sError
                    '        End If
                    '    End If
                    'Next
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub CheckQuatationOrderUpdateIndividualData()
            Try
                If Not CurrentIndividualPayTable Is Nothing AndAlso CurrentIndividualPayTable.Rows.Count > 0 Then
                    For Each dr As DataRow In CurrentIndividualPayTable.Rows

                        ' then update the order details
                        Dim sISProductId As String = String.Empty
                        If IsDBNull(dr("ProductID")) = False Then
                            sISProductId = Convert.ToString(dr("ProductID"))
                        Else
                            sISProductId = "0"
                        End If
                        Dim sIsStartDate As String = String.Empty
                        If Not dr("StartDate") Is Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(dr("StartDate"))) AndAlso Convert.ToString(dr("StartDate")) <> "1900-1-1" Then
                            sIsStartDate = Convert.ToString(dr("StartDate"))
                        End If
                        Dim sIsEndDate As String = String.Empty
                        If Not dr("EndDate") Is Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(dr("EndDate"))) AndAlso Convert.ToString(dr("EndDate")) <> "1900-1-1" Then
                            sIsEndDate = Convert.ToString(dr("EndDate"))
                        End If
                        Dim sMemberTypeId As String = String.Empty
                        If IsDBNull(dr("MemberTypeID")) = False Then
                            sMemberTypeId = Convert.ToString(dr("MemberTypeID"))
                        Else
                            sMemberTypeId = "0"
                        End If
                        Dim sRoleId As String = String.Empty
                        If IsDBNull(dr("RoleID")) = False Then
                            sRoleId = Convert.ToString(dr("RoleID"))
                        Else
                            sRoleId = "0"
                        End If
                        Dim params(6) As System.Data.IDataParameter
                        params(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.VarChar, Convert.ToString(dr("PersonID")))
                        params(1) = DataAction.GetDataParameter("@ProductCategoryID", SqlDbType.VarChar, Convert.ToString(dr("ProductCategoryID")))
                        params(2) = DataAction.GetDataParameter("@ProductID", SqlDbType.VarChar, sISProductId)

                        If Not String.IsNullOrEmpty(sIsStartDate) Then
                            params(3) = DataAction.GetDataParameter("@StartDate", SqlDbType.VarChar, New System.DateTime(Year(CDate(sIsStartDate)), Month(CDate(sIsStartDate)), Day(CDate(sIsStartDate))))
                        End If
                        If Not String.IsNullOrEmpty(sIsEndDate) Then
                            params(4) = DataAction.GetDataParameter("@EndDate", SqlDbType.VarChar, New System.DateTime(Year(CDate(sIsEndDate)), Month(CDate(sIsEndDate)), Day(CDate(sIsEndDate))))
                        Else
                            params(4) = DataAction.GetDataParameter("@EndDate", SqlDbType.VarChar, Nothing)
                        End If

                        If Not String.IsNullOrEmpty(sMemberTypeId) Then
                            params(5) = DataAction.GetDataParameter("@MemberTypeID", SqlDbType.VarChar, sMemberTypeId)
                        Else
                            params(5) = DataAction.GetDataParameter("@MemberTypeID", SqlDbType.VarChar, Nothing)
                        End If
                        If Not String.IsNullOrEmpty(sRoleId) Then
                            params(6) = DataAction.GetDataParameter("@RoleId", SqlDbType.VarChar, sRoleId)
                        Else
                            params(6) = DataAction.GetDataParameter("@EndDate", SqlDbType.VarChar, Nothing)
                        End If
                        'Dim sSQL As String = "..spUpdateWhoPayOrderDetail__c @PersonID=" & Convert.ToString(dr("PersonID")) & ",@ProductCategoryID=" & Convert.ToString(dr("ProductCategoryID")) & ",@ProductID='" & sISProductId & "',@StartDate='" & sIsStartDate & "',@EndDate='" & sIsEndDate & "'"
                        Dim sSQL As String = Database & "..spUpdateWhoPayOrderDetail__c"
                        ' @PersonID=" & Convert.ToString(dr("PersonID")) & ",@ProductCategoryID=" & Convert.ToString(dr("ProductCategoryID")) & ",@ProductID='" & sISProductId & "',@StartDate='" & New DateTime(Year(CDate(sIsStartDate)), Month(CDate(sIsStartDate)), Day(CDate(sIsStartDate))) & "',@EndDate='" & New DateTime(Year(CDate(sIsEndDate)), Month(CDate(sIsEndDate)), Day(CDate(sIsEndDate))) & "'"
                        Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                             'Redmine issue split orders
                           
                            For Each updateDr As DataRow In dt.Rows
                                Dim oOrderGE As AptifyGenericEntityBase
                                oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(updateDr("ID")))
                                If Convert.ToString(dr("PayType")).Trim.ToLower = "firm pays" Then
                                    oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", dr("CompanyID"))
                                Else
                                    oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", -1)
                                End If
                                oOrderGE.Save()
                                'Split Order functionality 
                                ''If Convert.ToString(dr("PayType")).Trim.ToLower = "firm pays" Then
                                ''    If oOrderGE.SubTypes("OrderLines").Count > 1 Then
                                ''        ' Code added by govind for redmine issue create seprate order whoes having Bill TO Company
                                ''         Dim oNewOrderGE As Aptify.Applications.OrderEntry.OrdersEntity
                                ''          oNewOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                                ''        oNewOrderGE.ShipToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                                ''        oNewOrderGE.BillToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                                ''        'oNewOrderGE.SetValue("FirmPay__c", 1)
                                ''        oNewOrderGE.AddProduct(CLng(updateDr("ProductID")))

                                ''        'oOrderGE.SetValue("BillToCompanyID", -1)
                                ''        'oOrderGE.SetValue("FirmPay__c", 0)
                                ''        oNewOrderGE.SubTypes("OrderLines").Item(oNewOrderGE.SubTypes("OrderLines").Count - 1).SetValue("BillToCompanyID__c", dr("CompanyID"))
                                ''        oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).Delete()
                                ''        If oNewOrderGE.SubTypes("OrderLines").Count > 0 Then
                                ''            oNewOrderGE.OrderType = OrderType.Quotation
                                ''            oNewOrderGE.Save()
                                ''        End If
                                ''        If oOrderGE.Save() Then
                                ''            oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                ''            oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                ''            oOrderGE.SetValue("BillToCompanyID", -1)
                                ''            oOrderGE.SetValue("FirmPay__c", 0)
                                ''            oOrderGE.Save()
                                ''        End If
                                ''    Else
                                ''        oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", dr("CompanyID"))
                                ''           Dim sSQLBillingContact As String = "..spGetBillingContactID__c @CompanyID=" & Convert.ToInt32(dr("CompanyID"))
                                ''        Dim iBillingContactID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQLBillingContact))
                                ''        If iBillingContactID > 0 Then
                                ''            oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                ''            oOrderGE.SetValue("BillToID", iBillingContactID)
                                ''        End If
                                ''         oOrderGE.Save()
                                ''    End If
                                ''Else
                                ''    oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", -1)
                                ''     If oOrderGE.Save() Then
                                ''            oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                ''            oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                ''            oOrderGE.SetValue("BillToCompanyID", -1)
                                ''            oOrderGE.SetValue("FirmPay__c", 0)
                                ''            oOrderGE.Save()
                                ''        End If
                                ''End If
                                
                            Next
                        
                        End If
                    Next
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub ClearWhoPaysPersonRecord()
            Try
                If Convert.ToString(ViewState("ClearPersonWhoPays")) <> "" Then
                    Dim m_PersonRecord() As String = Convert.ToString(ViewState("ClearPersonWhoPays")).Split(CChar(","))
                    Dim sError As String = Nothing
                    For i As Integer = 0 To m_PersonRecord.Count - 1
                        Dim oPersonGE As AptifyGenericEntityBase
                        oPersonGE = Me.AptifyApplication.GetEntityObject("Persons", Convert.ToInt32(m_PersonRecord(i)))
                        oPersonGE.SubTypes("PersonWhoPays__c").Clear()
                        If Not oPersonGE.Save(sError) Then
                            lblMsg.Text = sError
                        End If
                    Next
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub DeletePersonRecord()
            Try
                ' first check deleted into the current subtype in grid
                If Not CurrentIndividualPayDeleteTable Is Nothing AndAlso CurrentIndividualPayDeleteTable.Rows.Count > 0 Then
                    For Each dr As DataRow In CurrentIndividualPayDeleteTable.Rows
                        Dim IsMatched As Boolean = False
                        Dim sProductID As String
                        If IsDBNull(dr("ProductID")) = False Then
                            sProductID = Convert.ToString(dr("ProductID"))
                        Else
                            sProductID = "0"
                        End If


                        If Not CurrentIndividualPayTable Is Nothing AndAlso CurrentIndividualPayTable.Rows.Count > 0 Then
                            For Each drCurrentIndividualPay As DataRow In CurrentIndividualPayTable.Rows
                                If Convert.ToString(dr("PayType")).Trim.ToLower = Convert.ToString(drCurrentIndividualPay("PayType")).Trim.ToLower AndAlso Convert.ToInt32(dr("PersonID")) = Convert.ToInt32(drCurrentIndividualPay("PersonID")) AndAlso Convert.ToInt32(dr("ProductCategoryID")) = Convert.ToInt32(drCurrentIndividualPay("ProductCategoryID")) AndAlso Convert.ToInt32(sProductID) = Convert.ToInt32(drCurrentIndividualPay("ProductID")) AndAlso Convert.ToString(dr("StartDate")).Trim.ToLower = Convert.ToString(drCurrentIndividualPay("StartDate")).Trim.ToLower AndAlso Convert.ToString(dr("EndDate")).Trim.ToLower = Convert.ToString(drCurrentIndividualPay("EndDate")).Trim.ToLower Then
                                    IsMatched = True
                                    Exit For
                                End If
                            Next
                        End If
                        If IsMatched = False Then
                            ' then update the order details
                            Dim sISProductId As String = String.Empty
                            If Convert.ToInt32(dr("ProductID")) > 0 Then
                                sISProductId = Convert.ToString(dr("ProductID"))
                            Else
                                sISProductId = "0"
                            End If
                            Dim sIsStartDate As String = String.Empty
                            If Not dr("StartDate") Is Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(dr("StartDate"))) AndAlso Convert.ToString(dr("StartDate")) <> "1900-1-1" Then
                                sIsStartDate = Convert.ToString(dr("StartDate"))
                            End If
                            Dim sIsEndDate As String = String.Empty
                            If Not dr("EndDate") Is Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(dr("EndDate"))) AndAlso Convert.ToString(dr("EndDate")) <> "1900-1-1" Then
                                sIsEndDate = Convert.ToString(dr("EndDate"))
                            End If
                            Dim sMemberTypeId As String = String.Empty
                            If Convert.ToInt32(dr("MemberTypeID")) > 0 Then
                                sMemberTypeId = Convert.ToString(dr("MemberTypeID"))
                            Else
                                sMemberTypeId = "0"
                            End If
                            Dim sRoleId As String = String.Empty
                            If Convert.ToInt32(dr("RoleID")) > 0 Then
                                sRoleId = Convert.ToString(dr("RoleID"))
                            Else
                                sRoleId = "0"
                            End If
                            Dim params(6) As System.Data.IDataParameter
                            params(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.VarChar, Convert.ToString(dr("PersonID")))
                            params(1) = DataAction.GetDataParameter("@ProductCategoryID", SqlDbType.VarChar, Convert.ToString(dr("ProductCategoryID")))
                            params(2) = DataAction.GetDataParameter("@ProductID", SqlDbType.VarChar, sISProductId)

                            If Not String.IsNullOrEmpty(sIsStartDate) Then
                                Dim dStartDate As Date = CDate(sIsStartDate)
                                params(3) = DataAction.GetDataParameter("@StartDate", SqlDbType.VarChar, New System.DateTime(Year(dStartDate), Month(dStartDate), Day(dStartDate)))
                            End If
                            If Not String.IsNullOrEmpty(sIsEndDate) Then
                                Dim dEndDate As Date = CDate(sIsEndDate)
                                params(4) = DataAction.GetDataParameter("@EndDate", SqlDbType.VarChar, New System.DateTime(Year(dEndDate), Month(dEndDate), Day(dEndDate)))
                            Else
                                params(4) = DataAction.GetDataParameter("@EndDate", SqlDbType.VarChar, Nothing)
                            End If
                            If Not String.IsNullOrEmpty(sMemberTypeId) Then
                                params(5) = DataAction.GetDataParameter("@MemberTypeID", SqlDbType.VarChar, sMemberTypeId)
                            Else
                                params(5) = DataAction.GetDataParameter("@MemberTypeID", SqlDbType.VarChar, Nothing)
                            End If
                            If Not String.IsNullOrEmpty(sRoleId) Then
                                params(6) = DataAction.GetDataParameter("@RoleId", SqlDbType.VarChar, sRoleId)
                            Else
                                params(6) = DataAction.GetDataParameter("@EndDate", SqlDbType.VarChar, Nothing)
                            End If
                            'Dim sSQL As String = "..spUpdateWhoPayOrderDetail__c @PersonID=" & Convert.ToString(dr("PersonID")) & ",@ProductCategoryID=" & Convert.ToString(dr("ProductCategoryID")) & ",@ProductID='" & sISProductId & "',@StartDate='" & sIsStartDate & "',@EndDate='" & sIsEndDate & "'"
                            Dim sSQL As String = Database & "..spUpdateWhoPayOrderDetail__c"
                            ' @PersonID=" & Convert.ToString(dr("PersonID")) & ",@ProductCategoryID=" & Convert.ToString(dr("ProductCategoryID")) & ",@ProductID='" & sISProductId & "',@StartDate='" & New DateTime(Year(CDate(sIsStartDate)), Month(CDate(sIsStartDate)), Day(CDate(sIsStartDate))) & "',@EndDate='" & New DateTime(Year(CDate(sIsEndDate)), Month(CDate(sIsEndDate)), Day(CDate(sIsEndDate))) & "'"
                            Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                                For Each updateDr As DataRow In dt.Rows
                                    Dim oOrderGE As AptifyGenericEntityBase
                                    oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(updateDr("ID")))
                                    If Convert.ToInt32(updateDr("BillToCompanyID__c")) > 0 Then
                                        oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", -1)
                                        If oOrderGE.Save() Then
                                            oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                            oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                            oOrderGE.SetValue("BillToCompanyID", -1)
                                            oOrderGE.SetValue("FirmPay__c", 0)
                                            oOrderGE.Save()
                                        End If
                                    End If
                                Next
                            End If
                        End If
                        If Convert.ToInt32(dr("ID")) > 0 Then
                            Dim oPersonGE As AptifyGenericEntityBase
                            oPersonGE = Me.AptifyApplication.GetEntityObject("Persons", Convert.ToInt32(dr("PersonID")))
                            oPersonGE.SubTypes("PersonWhoPays__c").Find("ID", dr("ID")).Delete()
                            Dim sError As String = String.Empty
                            If Not oPersonGE.Save(sError) Then

                            End If
                        End If
                    Next
                End If
                CurrentIndividualPayDeleteTable = Nothing
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
#Region "Grid Events"
        Protected Sub grdCompanyPay_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdCompanyPay.RowDeleting
        End Sub
        ''' <summary>
        ''' User Click Delete button then row deleted from grid
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub grdCompanyPay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdCompanyPay.RowCommand
            Try
                If e.CommandName = "Delete" Then
                    Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                    Dim obj(8) As Object
                    obj(0) = commandArgs(0)
                    obj(1) = commandArgs(1)
                    obj(2) = commandArgs(2)
                    obj(3) = commandArgs(3)
                    obj(4) = commandArgs(4)
                    If commandArgs(5) = "" OrElse commandArgs(5) Is Nothing Then
                        obj(5) = 0
                    Else
                        obj(5) = commandArgs(5)
                    End If
                    If commandArgs(6) = "" OrElse commandArgs(6) Is Nothing Then
                        obj(6) = 0
                    Else
                        obj(6) = commandArgs(6)
                    End If

                    If Convert.ToString(obj(2)) = "" Then
                        obj(2) = 0
                    End If
                    If Convert.ToString(obj(5)) = "" Then
                        obj(5) = 0
                    End If
                    If Convert.ToString(obj(6)) = "" Then
                        obj(6) = 0
                    End If
                    obj(7) = commandArgs(7)
                    obj(8) = commandArgs(8)
                    BindDeleteCompanyData(Convert.ToInt32(obj(1)), Convert.ToInt32(obj(2)), Convert.ToString(obj(3)), Convert.ToString(obj(4)), Convert.ToInt32(obj(5)), Convert.ToInt32(obj(6)), Convert.ToInt32(obj(7)), Convert.ToInt32(obj(8)))

                    'CurrentCompanyPayTable.Rows.RemoveAt(CInt(commandArgs(0)))
                    Dim dr() As DataRow = CurrentCompanyPayTable.Select("CompanyID=" & Convert.ToInt32(obj(7)) & "AND ProductCategoryID=" & Convert.ToInt32(obj(1)) & "AND ProductID=" & Convert.ToInt32(obj(2)) & "AND MemberTypeID=" & Convert.ToInt32(obj(5)) & "AND RoleID=" & Convert.ToInt32(obj(6)))
                    If dr.Length > 0 Then
                        For i As Integer = 0 To dr.Length - 1
                            CurrentCompanyPayTable.Rows.Remove(dr(i))
                            CurrentCompanyPayTable.AcceptChanges()
                        Next
                    End If
                    grdCompanyPay.DataSource = CurrentCompanyPayTable
                    grdCompanyPay.DataBind()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub grdCompanyPay_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdCompanyPay.RowEditing
        End Sub
        Protected Sub grdIndividualPay_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdIndividualPay.RowDeleting
        End Sub
        ''' <summary>
        ''' On Click on Delete on Individual grid then call below method
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub grdIndividualPay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdIndividualPay.RowCommand
            Try
                If e.CommandName = "Delete" Then
                    Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                    Dim obj(10) As Object
                    obj(0) = commandArgs(0)
                    obj(1) = commandArgs(1)
                    obj(2) = commandArgs(2)
                    obj(3) = commandArgs(3)
                    obj(4) = commandArgs(4)
                    obj(5) = commandArgs(5)
                    obj(6) = commandArgs(6)
                    obj(7) = commandArgs(7)
                    obj(8) = commandArgs(8)
                    If Convert.ToString(obj(3)) = "" Then
                        obj(3) = 0
                    End If
                    If Convert.ToString(obj(7)) = "" Then
                        obj(7) = 0
                    End If
                    If Convert.ToString(obj(8)) = "" Then
                        obj(8) = 0
                    End If
                    obj(9) = commandArgs(9)
                    obj(10) = commandArgs(10)
                    BindDeleteIndividualData(Convert.ToInt32(obj(1)), Convert.ToInt32(obj(2)), Convert.ToInt32(obj(3)), Convert.ToString(obj(4)), Convert.ToString(obj(5)), Convert.ToString(obj(6)), Convert.ToInt32(obj(7)), Convert.ToInt32(obj(8)), Convert.ToInt32(obj(9)))

                    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    ' below logic for get the person id for clear sub types record
                    If Convert.ToString(ViewState("ClearPersonWhoPays")) = "" Then
                        ViewState("ClearPersonWhoPays") = Convert.ToString(obj(1))
                    Else
                        ViewState("ClearPersonWhoPays") = Convert.ToString(ViewState("ClearPersonWhoPays")) & "," & Convert.ToString(obj(1))
                    End If
                    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    ' CurrentIndividualPayTable.Rows.RemoveAt(CInt(commandArgs(0)))
                    Dim dr() As DataRow = CurrentIndividualPayTable.Select("PersonID=" & Convert.ToInt32(obj(1)) & "AND ProductCategoryID=" & Convert.ToInt32(obj(2)) & "AND ProductID=" & Convert.ToInt32(obj(3)) & "AND Paytype='" & Convert.ToString(obj(6)) & "' AND MemberTypeID=" & Convert.ToInt32(obj(7)) & "AND RoleID=" & Convert.ToInt32(obj(8)) & "AND CompanyID=" & Convert.ToInt32(obj(10)))
                    If dr.Length > 0 Then
                        For i As Integer = 0 To dr.Length - 1
                            CurrentIndividualPayTable.Rows.Remove(dr(i))
                            CurrentIndividualPayTable.AcceptChanges()

                        Next
                    End If
                    grdIndividualPay.DataSource = CurrentIndividualPayTable
                    grdIndividualPay.DataBind()
                    UpdatePanel1.Update()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub BindDeleteIndividualData(ByVal PersonID As Integer, ByVal ProductCateID As Integer, ByVal ProductID As Integer, ByVal StartDate As String, ByVal EndDate As String, ByVal Paytype As String, ByVal MemberTypeID As Integer, ByVal RoleID As Integer, ByVal RecordID As Integer)
            Try
                Dim dtCurrentIndividualPayDeleteTable As DataTable = CurrentIndividualPayDeleteTable
                Dim dr As DataRow = dtCurrentIndividualPayDeleteTable.NewRow()
                dr("PayType") = Paytype
                dr("PersonID") = PersonID
                dr("ProductCategoryID") = ProductCateID
                dr("ProductID") = ProductID
                dr("StartDate") = StartDate
                dr("EndDate") = EndDate
                dr("MemberTypeID") = MemberTypeID
                dr("RoleID") = RoleID
                dr("ID") = RecordID
                dtCurrentIndividualPayDeleteTable.Rows.Add(dr)
                CurrentIndividualPayDeleteTable = dtCurrentIndividualPayDeleteTable
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub
        Protected Sub BindDeleteCompanyData(ByVal ProductCateID As Integer, ByVal ProductID As Integer, ByVal StartDate As String, ByVal EndDate As String, ByVal MemberTypeID As Integer, ByVal RoleId As Integer, ByVal CompanyID As Integer, ByVal RecordID As Integer)
            Try
                Dim dtCurrentCompanyPayDeleteTable As DataTable = CurrentCompanyPayDeleteTable
                Dim dr As DataRow = dtCurrentCompanyPayDeleteTable.NewRow()
                dr("ProductCategoryID") = ProductCateID
                dr("ProductID") = ProductID
                dr("StartDate") = StartDate
                dr("EndDate") = EndDate
                dr("MemberTypeID") = MemberTypeID
                dr("RoleID") = RoleId
                dr("CompanyID") = CompanyID
                dr("ID") = RecordID
                dtCurrentCompanyPayDeleteTable.Rows.Add(dr)
                CurrentCompanyPayDeleteTable = dtCurrentCompanyPayDeleteTable
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub
        ''' <summary>
        ''' on Page changeing bind the data grid
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub grdCompanyPay_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCompanyPay.PageIndexChanging
            Try
                grdCompanyPay.PageIndex = e.NewPageIndex
                grdCompanyPay.DataSource = CurrentCompanyPayTable
                grdCompanyPay.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub grdIndividualPay_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdIndividualPay.PageIndexChanging
            Try
                grdIndividualPay.PageIndex = e.NewPageIndex
                grdIndividualPay.DataSource = CurrentIndividualPayTable
                grdIndividualPay.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

        Protected Sub cmbMemberTypeIndi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMemberTypeIndi.SelectedIndexChanged
            Try
                Dim sSql As String
                If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    'If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    If Convert.ToInt32(cmbRoleIndi.SelectedValue) > 0 Then
                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                ",@RoleID=" & cmbRoleIndi.SelectedValue
                        Else
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & _
                                                                               ",@RoleID=" & cmbRoleIndi.SelectedValue
                        End If

                    Else
                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue
                        Else
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=0"

                        End If
                    End If
                Else

                    If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                ",@RoleID=" & cmbRoleIndi.SelectedValue
                        Else
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue
                        End If
                    Else
                        BindPersonList(Convert.ToInt32(cmbIndividualCompany.SelectedValue))
                    End If
                    'If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    '    sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & User1.CompanyID & ",@RoleID=" & cmbRoleIndi.SelectedValue
                    'Else
                    '    BindPersonList()
                    'End If
                    'sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & User1.CompanyID & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue
                End If

                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("AllPersons") = dt
                    cmbPersons.DataSource = Nothing
                    cmbPersons.DataSource = dt
                    cmbPersons.DataTextField = "FirstLast"
                    cmbPersons.DataValueField = "ID"
                    cmbPersons.DataBind()
                    cmbPersons.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectAll")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                Else
                    ViewState("AllPersons") = Nothing
                    cmbPersons.DataSource = Nothing
                    cmbPersons.Items.Clear()
                    cmbPersons.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub cmbRoleIndi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbRoleIndi.SelectedIndexChanged
            Try
                Dim sSql As String

                If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    If Convert.ToInt32(cmbMemberTypeIndi.SelectedValue) > 0 Then
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                 ",@RoleID=" & cmbRoleIndi.SelectedValue
                        Else
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue
                        End If

                    Else
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=" & cmbRoleIndi.SelectedValue
                        Else
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=0"
                        End If
                    End If
                Else

                    If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                ",@RoleID=" & cmbRoleIndi.SelectedValue
                        Else
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=" & cmbRoleIndi.SelectedValue
                        End If
                    Else
                        BindPersonList(Convert.ToInt32(cmbIndividualCompany.SelectedValue))
                    End If


                    'If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    '    sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & User1.CompanyID & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue
                    'Else
                    '    BindPersonList()
                    'End If
                    'sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & User1.CompanyID & ",@RoleID=" & cmbRoleIndi.SelectedValue
                End If

                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("AllPersons") = dt
                    cmbPersons.DataSource = Nothing
                    cmbPersons.DataSource = dt
                    cmbPersons.DataTextField = "FirstLast"
                    cmbPersons.DataValueField = "ID"
                    cmbPersons.DataBind()
                    cmbPersons.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectAll")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                Else
                    ViewState("AllPersons") = Nothing
                    cmbPersons.DataSource = Nothing
                    cmbPersons.Items.Clear()
                    cmbPersons.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub cmbIndividualCompany_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbIndividualCompany.SelectedIndexChanged
            Try
                Dim sSql As String
                If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    'If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    If Convert.ToInt32(cmbRoleIndi.SelectedValue) > 0 Then
                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                ",@RoleID=" & cmbRoleIndi.SelectedValue
                        Else
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & _
                                                                               ",@RoleID=" & cmbRoleIndi.SelectedValue
                        End If

                    Else
                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue
                        Else
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=0"

                        End If
                    End If
                Else

                    If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                ",@RoleID=" & cmbRoleIndi.SelectedValue
                        Else
                            sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue
                        End If
                    Else
                        BindPersonList(Convert.ToInt32(cmbIndividualCompany.SelectedValue))
                    End If
                    'If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    '    sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & User1.CompanyID & ",@RoleID=" & cmbRoleIndi.SelectedValue
                    'Else
                    '    BindPersonList()
                    'End If
                    'sSql = Database & "..spGetPersonsAsPerMemberTypeAndRole__c @CompanyID=" & User1.CompanyID & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue
                End If

                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("AllPersons") = dt
                    cmbPersons.DataSource = Nothing
                    cmbPersons.DataSource = dt
                    cmbPersons.DataTextField = "FirstLast"
                    cmbPersons.DataValueField = "ID"
                    cmbPersons.DataBind()
                    cmbPersons.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectAll")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                Else
                    ViewState("AllPersons") = Nothing
                    cmbPersons.DataSource = Nothing
                    cmbPersons.Items.Clear()
                    cmbPersons.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace



