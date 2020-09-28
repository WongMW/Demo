'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande               19 March 2015                     As per selecting Firm Or Individual pay save the data into persons and companies Who pays sub entity
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Option Explicit On
Option Strict On

Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.AttributeManagement
Imports System.Data
Imports Aptify.Framework.BusinessLogic.ProcessPipeline
Imports Aptify.Applications.OrderEntry
Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class WhoPaysETD__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Private ClearPersonWhoPays As String = String.Empty
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            cmpCalenders.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.DateValidation")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            CompareValidator1.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.DateValidation")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            If Not IsPostBack Then
                ViewState("SubsidariesCompanyDT") = Nothing
                BindProductCategory()
                BindLoadRouteOfEntry()
                LoadSubsidiariesCompanyList()
                BindCompanyPayData()
                BindLoadProducts()
                BindPersonList()
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

                cmbProduct.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                cmbProductList.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Bind Product Catrgories
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindProductCategory()
            Try
                ' Get All Products Categories
                Dim sSQLProductCategory As String = "..spGetProductsCategoryForEducation__c"
                Dim dtProductCategory As DataTable = DataAction.GetDataTable(sSQLProductCategory)
                If Not dtProductCategory Is Nothing AndAlso dtProductCategory.Rows.Count > 0 Then
                    cmbProductCategory.DataSource = dtProductCategory
                    cmbProductCategory.DataTextField = "Name"
                    cmbProductCategory.DataValueField = "ID"
                    cmbProductCategory.DataBind()
                    'cmbProductCategory.Items.Insert(0, "-- Select Product Category--")
                    cmbProductCategory.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))


                    ' Bind Individual Product Category

                    cmbProductCat.DataSource = dtProductCategory
                    cmbProductCat.DataTextField = "Name"
                    cmbProductCat.DataValueField = "ID"
                    cmbProductCat.DataBind()
                    cmbProductCat.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))

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
                ' If Data on CurrentCompanyPayStudentTable Data table Property then bind to the grdCompanyPay
                If Not CurrentCompanyPayStudentTable Is Nothing AndAlso CurrentCompanyPayStudentTable.Rows.Count > 0 Then
                    grdCompanyPay.DataSource = CurrentCompanyPayStudentTable
                    grdCompanyPay.DataBind()
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
                Dim sSql As String = "..spGetCompanyPaysProductCategoriesForEducation__c @CompanyID='" & sCompanyID & "'"
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    grdCompanyPay.DataSource = dt
                    grdCompanyPay.DataBind()
                    CurrentCompanyPayStudentTable = dt
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
                ' If Data on CurrentCompanyPayStudentTable Data table Property then bind to the grdCompanyPay
                If Not CurrentIndividualPayStudentTable Is Nothing AndAlso CurrentIndividualPayStudentTable.Rows.Count > 0 Then
                    grdIndividualPay.DataSource = CurrentIndividualPayStudentTable
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
                Dim sSql As String = "..spGetIndividualPayDataForEducation__c @CompanyID='" & sCompanyID & "'"
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    grdIndividualPay.DataSource = dt
                    grdIndividualPay.DataBind()
                    CurrentIndividualPayStudentTable = dt
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Bind Person List as per Company 
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindPersonList()
            Try
                Dim RouteOfEntryID As Integer = 0
                If cmbIndividualRouteOfEntry.SelectedIndex > 0 Then
                    RouteOfEntryID = Convert.ToInt32(cmbIndividualRouteOfEntry.SelectedValue)
                End If
                Dim sSqlPersonList = "..spGetPersonsListForETDWhoPays__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                Dim dtPersonList As DataTable = DataAction.GetDataTable(sSqlPersonList)
                If Not dtPersonList Is Nothing AndAlso dtPersonList.Rows.Count > 0 Then
                    cmbPersons.DataSource = dtPersonList
                    cmbPersons.DataTextField = "FirstLast"
                    cmbPersons.DataValueField = "ID"
                    cmbPersons.DataBind()
                    cmbPersons.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))

                Else
                    cmbPersons.ClearSelection()
                    cmbPersons.Items.Clear()
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
        Protected Sub LoadIndivisualProductList(ByVal ProductCategoryID As Long)
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
                cmbProduct.Items.Clear()
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



        ' Govind Code started 17/3/2015
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
                    ' BindLoadProducts() ' Display All product list
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub cmbProductCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProductCat.SelectedIndexChanged
            Try
                If Convert.ToString(cmbProductCat.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    LoadIndivisualProductList(Convert.ToInt32(cmbProductCat.SelectedValue)) 'As Per product Category display Product List
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
        Public Property CurrentCompanyPayStudentTable() As DataTable
            Get

                If Not Session("CurrentCompanyPayStudentTable") Is Nothing Then
                    Return CType(Session("CurrentCompanyPayStudentTable"), DataTable)
                Else
                    Dim dtCurrentCompanyPayStudentTable As New DataTable
                    dtCurrentCompanyPayStudentTable.Columns.Add("ID")
                    dtCurrentCompanyPayStudentTable.Columns.Add("ProductCategoryID")
                    dtCurrentCompanyPayStudentTable.Columns.Add("ProductID")
                    dtCurrentCompanyPayStudentTable.Columns.Add("ProductCategory")
                    dtCurrentCompanyPayStudentTable.Columns.Add("Product")
                    dtCurrentCompanyPayStudentTable.Columns.Add("StartDate")
                    dtCurrentCompanyPayStudentTable.Columns.Add("EndDate")
                    dtCurrentCompanyPayStudentTable.Columns.Add("MemberTypeID")
                    dtCurrentCompanyPayStudentTable.Columns.Add("MemberType")
                    dtCurrentCompanyPayStudentTable.Columns.Add("RoleID")
                    dtCurrentCompanyPayStudentTable.Columns.Add("Role")
                    dtCurrentCompanyPayStudentTable.Columns.Add("CompanyID")
                    dtCurrentCompanyPayStudentTable.Columns.Add("Company")
                    dtCurrentCompanyPayStudentTable.Columns.Add("RouteOfEntryID")
                    dtCurrentCompanyPayStudentTable.Columns.Add("RouteOfEntry")
                    dtCurrentCompanyPayStudentTable.Columns.Add("Attempts")
                    dtCurrentCompanyPayStudentTable.Columns.Add("NonEditableForFirm")
                    Return dtCurrentCompanyPayStudentTable
                End If
            End Get
            Set(ByVal value As DataTable)
                Session("CurrentCompanyPayStudentTable") = value
            End Set
        End Property
        ''' <summary>
        ''' Create Individual Pay Data table 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CurrentIndividualPayStudentTable() As DataTable
            Get

                If Not Session("CurrentIndividualPayStudentTable") Is Nothing Then
                    Return CType(Session("CurrentIndividualPayStudentTable"), DataTable)
                Else
                    Dim dtCurrentIndividualPayStudentTable As New DataTable
                    dtCurrentIndividualPayStudentTable.Columns.Add("ID")
                    dtCurrentIndividualPayStudentTable.Columns.Add("PayType")
                    dtCurrentIndividualPayStudentTable.Columns.Add("PersonID")
                    dtCurrentIndividualPayStudentTable.Columns.Add("Person")
                    dtCurrentIndividualPayStudentTable.Columns.Add("ProductCategoryID")
                    dtCurrentIndividualPayStudentTable.Columns.Add("ProductID")
                    dtCurrentIndividualPayStudentTable.Columns.Add("ProductCategory")
                    dtCurrentIndividualPayStudentTable.Columns.Add("Product")
                    dtCurrentIndividualPayStudentTable.Columns.Add("StartDate")
                    dtCurrentIndividualPayStudentTable.Columns.Add("EndDate")
                    dtCurrentIndividualPayStudentTable.Columns.Add("MemberTypeID")
                    dtCurrentIndividualPayStudentTable.Columns.Add("MemberType")
                    dtCurrentIndividualPayStudentTable.Columns.Add("RoleID")
                    dtCurrentIndividualPayStudentTable.Columns.Add("Role")
                    dtCurrentIndividualPayStudentTable.Columns.Add("CompanyID")
                    dtCurrentIndividualPayStudentTable.Columns.Add("Company")
                    dtCurrentIndividualPayStudentTable.Columns.Add("RouteOfEntryID")
                    dtCurrentIndividualPayStudentTable.Columns.Add("RouteOfEntry")
                    dtCurrentIndividualPayStudentTable.Columns.Add("Attempts")
                    dtCurrentIndividualPayStudentTable.Columns.Add("NonEditableForFirm")

                    Return dtCurrentIndividualPayStudentTable
                End If
            End Get
            Set(ByVal value As DataTable)
                Session("CurrentIndividualPayStudentTable") = value
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
                    dtIndividualPayDeleteTable.Columns.Add("ID")
                    dtIndividualPayDeleteTable.Columns.Add("PayType")
                    dtIndividualPayDeleteTable.Columns.Add("PersonID")
                    dtIndividualPayDeleteTable.Columns.Add("ProductCategoryID")
                    dtIndividualPayDeleteTable.Columns.Add("ProductID")
                    dtIndividualPayDeleteTable.Columns.Add("StartDate")
                    dtIndividualPayDeleteTable.Columns.Add("EndDate")
                    dtIndividualPayDeleteTable.Columns.Add("MemberTypeID")
                    dtIndividualPayDeleteTable.Columns.Add("RoleID")
                    dtIndividualPayDeleteTable.Columns.Add("RouteOfEntryID")
                    dtIndividualPayDeleteTable.Columns.Add("RouteOfEntry")
                    dtIndividualPayDeleteTable.Columns.Add("Attempts")
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
                    dtCompanyPayDeleteTable.Columns.Add("ID")
                    dtCompanyPayDeleteTable.Columns.Add("ProductCategoryID")
                    dtCompanyPayDeleteTable.Columns.Add("ProductID")
                    dtCompanyPayDeleteTable.Columns.Add("StartDate")
                    dtCompanyPayDeleteTable.Columns.Add("EndDate")
                    dtCompanyPayDeleteTable.Columns.Add("MemberTypeID")
                    dtCompanyPayDeleteTable.Columns.Add("RoleID")
                    dtCompanyPayDeleteTable.Columns.Add("RouteOfEntryID")
                    dtCompanyPayDeleteTable.Columns.Add("Attempts")
                    dtCompanyPayDeleteTable.Columns.Add("CompanyID")

                    Return dtCompanyPayDeleteTable
                End If
            End Get
            Set(ByVal value As DataTable)
                Session("CompanyPayDeleteTable") = value
            End Set
        End Property

        ' New Code added 
        Protected Sub BindLoadRouteOfEntry()
            Try
                Dim sRouteOfEntry As String = "..spGetRouteOFEntryForCACurriculum__c"
                Dim dtRouteOfEntry As DataTable = DataAction.GetDataTable(sRouteOfEntry)
                If Not dtRouteOfEntry Is Nothing AndAlso dtRouteOfEntry.Rows.Count > 0 Then
                    cmbRouteOfEntry.DataSource = dtRouteOfEntry
                    cmbRouteOfEntry.DataTextField = "Name"
                    cmbRouteOfEntry.DataValueField = "ID"
                    cmbRouteOfEntry.DataBind()
                    cmbRouteOfEntry.Items.Insert(0, "-- Select route of entry --")


                    cmbIndividualRouteOfEntry.DataSource = dtRouteOfEntry
                    cmbIndividualRouteOfEntry.DataTextField = "Name"
                    cmbIndividualRouteOfEntry.DataValueField = "ID"
                    cmbIndividualRouteOfEntry.DataBind()
                    cmbIndividualRouteOfEntry.Items.Insert(0, "-- Select route of entry --")

                Else
                    cmbRouteOfEntry.Items.Clear()
                    cmbIndividualRouteOfEntry.Items.Clear()
                End If

            Catch ex As Exception

            End Try
        End Sub

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
                        Dim dtCurrentCompanyPayStudentTable As DataTable = CurrentCompanyPayStudentTable
                        Dim drCurrentCompanyPayStudentTable As DataRow = dtCurrentCompanyPayStudentTable.NewRow()

                        drCurrentCompanyPayStudentTable("ID") = 0
                        If Convert.ToString(cmbProductCategory.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            drCurrentCompanyPayStudentTable("ProductCategoryID") = cmbProductCategory.SelectedValue
                            drCurrentCompanyPayStudentTable("ProductCategory") = cmbProductCategory.SelectedItem
                        End If
                        If Convert.ToString(cmbProduct.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            drCurrentCompanyPayStudentTable("ProductID") = cmbProduct.SelectedValue
                            drCurrentCompanyPayStudentTable("Product") = cmbProduct.SelectedItem
                        Else
                            drCurrentCompanyPayStudentTable("ProductID") = 0
                            drCurrentCompanyPayStudentTable("Product") = ""
                        End If
                        If Convert.ToString(cmbMemberType.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            drCurrentCompanyPayStudentTable("MemberTypeID") = cmbMemberType.SelectedValue
                            drCurrentCompanyPayStudentTable("MemberType") = cmbMemberType.SelectedItem
                        Else
                            drCurrentCompanyPayStudentTable("MemberTypeID") = 0
                            drCurrentCompanyPayStudentTable("MemberType") = ""
                        End If
                        If Convert.ToString(cmbRole.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            drCurrentCompanyPayStudentTable("RoleID") = cmbRole.SelectedValue
                            drCurrentCompanyPayStudentTable("Role") = cmbRole.SelectedItem
                        Else
                            drCurrentCompanyPayStudentTable("RoleID") = 0
                            drCurrentCompanyPayStudentTable("Role") = ""
                        End If
                        drCurrentCompanyPayStudentTable("CompanyID") = cmbSubsidarisCompney.SelectedValue
                        drCurrentCompanyPayStudentTable("Company") = cmbSubsidarisCompney.SelectedItem

                        Try
                            If Not String.IsNullOrEmpty(txtStartDate.SelectedDate.ToString().Trim()) Then
                                '   drCurrentCompanyPayStudentTable("StartDate") = Convert.ToDateTime(txtStartDate.Text).ToShortDateString()
                                drCurrentCompanyPayStudentTable("StartDate") = txtStartDate.SelectedDate.Value.ToShortDateString()
                            End If
                        Catch ex As Exception
                            lblMsg.Visible = True
                            lblMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidStartDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                            Return
                        End Try
                        Try
                            If Not String.IsNullOrEmpty(txtEndDate.SelectedDate.ToString().Trim()) Then
                                drCurrentCompanyPayStudentTable("EndDate") = txtEndDate.SelectedDate.Value.ToShortDateString()
                            Else

                            End If
                        Catch ex As Exception
                            lblMsg.Visible = True
                            lblMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidEndDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                            Return
                        End Try

                        If Convert.ToInt32(cmbRouteOfEntry.SelectedValue) > 0 Then
                            drCurrentCompanyPayStudentTable("RouteOfEntryID") = cmbRouteOfEntry.SelectedValue
                            drCurrentCompanyPayStudentTable("RouteOfEntry") = cmbRouteOfEntry.SelectedItem
                        End If
                        If Convert.ToString(cmbCompanyAttempts.SelectedValue).Trim.ToLower <> "-- select attempt --" Then
                            drCurrentCompanyPayStudentTable("Attempts") = cmbCompanyAttempts.SelectedItem
                        End If
                        drCurrentCompanyPayStudentTable("NonEditableForFirm") = False
                        dtCurrentCompanyPayStudentTable.Rows.Add(drCurrentCompanyPayStudentTable)
                        CurrentCompanyPayStudentTable = dtCurrentCompanyPayStudentTable
                        grdCompanyPay.DataSource = CurrentCompanyPayStudentTable
                        grdCompanyPay.DataBind()
                        grdCompanyPay.AllowPaging = True
                        grdCompanyPay.PageSize = 10
                        BindProductCategory()
                        LoadCompanyProductList(0)
                        txtStartDate.SelectedDate = Nothing
                        txtEndDate.SelectedDate = Nothing
                        LoadMemberType()
                        LoadRole()
                        BindLoadRouteOfEntry()
                        cmbCompanyAttempts.SelectedIndex = 0
                        ' cmbSubsidarisCompney.SelectedIndex = 0
                        LoadSubsidiariesCompanyList()
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
                    If ValidateNonEditableFirm() Then
                        If Convert.ToString(cmbProductCat.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            If Convert.ToString(cmbPersons.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                lblIndivisualMsg.Visible = False

                                If Convert.ToString(cmbPersons.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectAll")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                    Dim dtCurrentIndividualPayStudentTable As DataTable = CurrentIndividualPayStudentTable
                                    Dim drCurrentIndividualPayStudentTable As DataRow = dtCurrentIndividualPayStudentTable.NewRow()
                                    drCurrentIndividualPayStudentTable("PayType") = cmbPayType.SelectedValue.Trim
                                    If Convert.ToString(cmbPersons.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                        drCurrentIndividualPayStudentTable("PersonID") = cmbPersons.SelectedValue
                                        drCurrentIndividualPayStudentTable("Person") = cmbPersons.SelectedItem
                                    Else
                                        drCurrentIndividualPayStudentTable("PersonID") = 0
                                    End If
                                    If Convert.ToString(cmbProductCat.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                        drCurrentIndividualPayStudentTable("ProductCategoryID") = cmbProductCat.SelectedValue
                                        drCurrentIndividualPayStudentTable("ProductCategory") = cmbProductCat.SelectedItem
                                    End If
                                    If Convert.ToString(cmbProductList.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                        drCurrentIndividualPayStudentTable("ProductID") = cmbProductList.SelectedValue
                                        drCurrentIndividualPayStudentTable("Product") = cmbProductList.SelectedItem
                                    Else
                                        drCurrentIndividualPayStudentTable("ProductID") = 0
                                    End If
                                    If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                        drCurrentIndividualPayStudentTable("MemberTypeID") = cmbMemberTypeIndi.SelectedValue
                                        drCurrentIndividualPayStudentTable("MemberType") = cmbMemberTypeIndi.SelectedItem
                                    Else
                                        drCurrentIndividualPayStudentTable("MemberTypeID") = 0
                                    End If
                                    If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                        drCurrentIndividualPayStudentTable("RoleID") = cmbRoleIndi.SelectedValue
                                        drCurrentIndividualPayStudentTable("Role") = cmbRoleIndi.SelectedItem
                                    Else
                                        drCurrentIndividualPayStudentTable("RoleID") = 0
                                    End If
                                    drCurrentIndividualPayStudentTable("CompanyID") = cmbIndividualCompany.SelectedValue
                                    drCurrentIndividualPayStudentTable("Company") = cmbIndividualCompany.SelectedItem
                                    Try
                                        If Not String.IsNullOrEmpty(txtIndiStartDate.SelectedDate.ToString().Trim()) Then
                                            drCurrentIndividualPayStudentTable("StartDate") = txtIndiStartDate.SelectedDate.Value.ToShortDateString()
                                        End If
                                    Catch ex As Exception
                                        lblIndivisualMsg.Visible = True
                                        lblIndivisualMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidStartDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                                        Return
                                    End Try
                                    Try
                                        If Not String.IsNullOrEmpty(txtIndiEndDate.SelectedDate.ToString().Trim()) Then
                                            drCurrentIndividualPayStudentTable("EndDate") = txtIndiEndDate.SelectedDate.Value.ToShortDateString()
                                        End If
                                    Catch ex As Exception
                                        lblIndivisualMsg.Visible = True
                                        lblIndivisualMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidEndDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                                        Return
                                    End Try
                                    If Convert.ToInt32(cmbIndividualRouteOfEntry.SelectedValue) > 0 Then
                                        drCurrentIndividualPayStudentTable("RouteOfEntryID") = cmbIndividualRouteOfEntry.SelectedValue
                                        drCurrentIndividualPayStudentTable("RouteOfEntry") = cmbIndividualRouteOfEntry.SelectedItem
                                    End If
                                    If Convert.ToString(cmbIndivisualAttempts.SelectedValue).Trim.ToLower <> "-- select attempt --" Then
                                        drCurrentIndividualPayStudentTable("Attempts") = cmbIndivisualAttempts.SelectedItem
                                    End If
                                    drCurrentIndividualPayStudentTable("NonEditableForFirm") = False
                                    drCurrentIndividualPayStudentTable("ID") = 0

                                    dtCurrentIndividualPayStudentTable.Rows.Add(drCurrentIndividualPayStudentTable)
                                    CurrentIndividualPayStudentTable = dtCurrentIndividualPayStudentTable
                                    grdIndividualPay.DataSource = CurrentIndividualPayStudentTable
                                    grdIndividualPay.DataBind()
                                    grdIndividualPay.AllowPaging = True
                                    grdIndividualPay.PageSize = 10
                                ElseIf Convert.ToString(cmbPersons.SelectedValue).Trim.ToLower = Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectAll")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                    Dim dtAllPersons As DataTable = CType(ViewState("AllPersons"), DataTable)
                                    If Not dtAllPersons Is Nothing AndAlso dtAllPersons.Rows.Count > 0 Then
                                        For Each dr As DataRow In dtAllPersons.Rows
                                            Dim dtCurrentIndividualPayStudentTable As DataTable = CurrentIndividualPayStudentTable
                                            Dim drCurrentIndividualPayStudentTable As DataRow = dtCurrentIndividualPayStudentTable.NewRow()
                                            drCurrentIndividualPayStudentTable("PayType") = cmbPayType.SelectedValue.Trim
                                            If Convert.ToString(cmbPersons.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPerson")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                                drCurrentIndividualPayStudentTable("PersonID") = dr("ID")
                                                drCurrentIndividualPayStudentTable("Person") = dr("FirstLast")
                                            Else
                                                drCurrentIndividualPayStudentTable("PersonID") = 0
                                            End If
                                            If Convert.ToString(cmbProductCat.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategory")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                                drCurrentIndividualPayStudentTable("ProductCategoryID") = cmbProductCat.SelectedValue
                                                drCurrentIndividualPayStudentTable("ProductCategory") = cmbProductCat.SelectedItem
                                            End If
                                            If Convert.ToString(cmbProductList.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                                drCurrentIndividualPayStudentTable("ProductID") = cmbProductList.SelectedValue
                                                drCurrentIndividualPayStudentTable("Product") = cmbProductList.SelectedItem
                                            Else
                                                drCurrentIndividualPayStudentTable("ProductID") = 0
                                            End If
                                            If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                                drCurrentIndividualPayStudentTable("MemberTypeID") = cmbMemberTypeIndi.SelectedValue
                                                drCurrentIndividualPayStudentTable("MemberType") = cmbMemberTypeIndi.SelectedItem
                                            Else
                                                drCurrentIndividualPayStudentTable("MemberTypeID") = 0
                                            End If
                                            If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                                                drCurrentIndividualPayStudentTable("RoleID") = cmbRoleIndi.SelectedValue
                                                drCurrentIndividualPayStudentTable("Role") = cmbRoleIndi.SelectedItem
                                            Else
                                                drCurrentIndividualPayStudentTable("RoleID") = 0
                                            End If
                                            Try
                                                If Not String.IsNullOrEmpty(txtIndiStartDate.SelectedDate.ToString().Trim()) Then
                                                    drCurrentIndividualPayStudentTable("StartDate") = txtIndiStartDate.SelectedDate.Value.ToShortDateString()
                                                End If
                                            Catch ex As Exception
                                                lblIndivisualMsg.Visible = True
                                                lblIndivisualMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidStartDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                                                Return
                                            End Try
                                            Try
                                                If Not String.IsNullOrEmpty(txtIndiEndDate.SelectedDate.ToString().Trim()) Then
                                                    drCurrentIndividualPayStudentTable("EndDate") = txtIndiEndDate.SelectedDate.Value.ToShortDateString()
                                                End If
                                            Catch ex As Exception
                                                lblIndivisualMsg.Visible = True
                                                lblIndivisualMsg.Text = "<ui><li>" & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ValidEndDate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "</li></ui>"
                                                Return
                                            End Try
                                            If Convert.ToInt32(cmbIndividualRouteOfEntry.SelectedValue) > 0 Then
                                                drCurrentIndividualPayStudentTable("RouteOfEntryID") = cmbIndividualRouteOfEntry.SelectedValue
                                                drCurrentIndividualPayStudentTable("RouteOfEntry") = cmbIndividualRouteOfEntry.SelectedItem
                                            End If
                                            If Convert.ToString(cmbIndivisualAttempts.SelectedValue).Trim.ToLower <> "select attempt" Then
                                                drCurrentIndividualPayStudentTable("Attempts") = cmbIndivisualAttempts.SelectedItem
                                            End If
                                            drCurrentIndividualPayStudentTable("NonEditableForFirm") = False
                                            drCurrentIndividualPayStudentTable("ID") = 0
                                            drCurrentIndividualPayStudentTable("CompanyID") = cmbIndividualCompany.SelectedValue
                                            drCurrentIndividualPayStudentTable("Company") = cmbIndividualCompany.SelectedItem
                                            dtCurrentIndividualPayStudentTable.Rows.Add(drCurrentIndividualPayStudentTable)
                                            CurrentIndividualPayStudentTable = dtCurrentIndividualPayStudentTable
                                            grdIndividualPay.DataSource = CurrentIndividualPayStudentTable
                                            grdIndividualPay.DataBind()
                                            grdIndividualPay.AllowPaging = True
                                            grdIndividualPay.PageSize = 10
                                        Next
                                    End If
                                End If

                                BindProductCategory()
                                LoadIndivisualProductList(0)
                                txtIndiStartDate.SelectedDate = Nothing
                                txtIndiEndDate.SelectedDate = Nothing
                                cmbPayType.SelectedIndex = 0
                                'cmbIndividualCompany.SelectedIndex = 0
                                LoadSubsidiariesCompanyList()
                                BindPersonList()
                                LoadMemberType()
                                LoadRole()
                                BindLoadRouteOfEntry()

                                cmbIndivisualAttempts.SelectedIndex = 0
                                cmbProductList.SelectedIndex = 0


                            Else
                                lblIndivisualMsg.Visible = True
                                lblIndivisualMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPersonMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            End If
                        Else
                            lblIndivisualMsg.Visible = True
                            lblIndivisualMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProductCategoryMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        End If
                    Else
                        lblIndivisualMsg.Visible = True
                        lblIndivisualMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WhoPaysForEducation.NonEditableForFirm")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Function ValidateNonEditableFirm() As Boolean
            Try
                If cmbPayType.SelectedValue = "Member Pays" Then
                    Dim sSql As String = Database & "..spValidateNonEditableForPersonPay__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@ProductCategoryID=" & cmbProductCat.SelectedValue & ",@RouteOfEntryID=" & cmbIndividualRouteOfEntry.SelectedValue & ",@Attempt='" & cmbIndivisualAttempts.SelectedValue & "'"
                    Dim iCompanyWhoPayID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If iCompanyWhoPayID > 0 Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
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
                ' UpdatePanel1.Update()
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
                If Not CurrentCompanyPayStudentTable Is Nothing AndAlso CurrentCompanyPayStudentTable.Rows.Count > 0 Then
                    For Each dr As DataRow In CurrentCompanyPayStudentTable.Rows
                        If Not dr.RowState = DataRowState.Deleted AndAlso Not dr.RowState = DataRowState.Detached Then
                            ' added all compnies with new datatable for next time we have get all companies records 
                            Dim drCompanyDetailsDataTable As DataRow = dtCompanyDetailsDataTable.NewRow()
                            drCompanyDetailsDataTable("CompanyID") = dr("CompanyID")
                            dtCompanyDetailsDataTable.Rows.Add(drCompanyDetailsDataTable)
                        End If
                    Next
                    If Not CurrentCompanyPayStudentTable Is Nothing AndAlso CurrentCompanyPayStudentTable.Rows.Count > 0 Then
                        ''For Each drCompanyDetails As DataRow In dtCompanyDetailsDataTable.Rows
                        For Each drCompanyDetails As DataRow In CurrentCompanyPayStudentTable.Rows

                            '  'Dim result() As DataRow = CurrentCompanyPayStudentTable.Select("CompanyID=" & Convert.ToInt32(drCompanyDetails("CompanyID")))
                            oCompanyGE = Me.AptifyApplication.GetEntityObject("Companies", Convert.ToInt32(drCompanyDetails("CompanyID")))

                            ClearCompanyData()
                            '; For Each row As DataRow In result
                            If Convert.ToInt32(drCompanyDetails("ID")) = 0 Then
                                With oCompanyGE.SubTypes("CompanyPaysProductCategories__c").Add()
                                    If IsDBNull(drCompanyDetails("ProductCategoryID")) = False Then
                                        .SetValue("ProductCategoryID", Convert.ToInt32(drCompanyDetails("ProductCategoryID")))
                                    End If
                                    If IsDBNull(drCompanyDetails("ProductID")) = False Then
                                        .SetValue("ProductID", Convert.ToInt32(drCompanyDetails("ProductID")))
                                    End If
                                    .SetValue("StartDate", drCompanyDetails("StartDate"))
                                    .SetValue("EndDate", drCompanyDetails("EndDate"))
                                    .SetValue("MemberTypeID", drCompanyDetails("MemberTypeID"))
                                    .SetValue("PrimaryFunctionID", drCompanyDetails("RoleID"))
                                    .SetValue("RouteOfEntryID", drCompanyDetails("RouteOfEntryID"))
                                    .SetValue("Attempts", drCompanyDetails("Attempts"))
                                    .SetValue("NonEditableForFirm", drCompanyDetails("NonEditableForFirm"))
                                End With
                            End If
                            'Next
                            'CheckQuatationOrderUpdateCompanyData()
                            ''Added BY Pradip 2016-01-10 For Group 3 Tracker G3-48
                            'If CheckApprovedEE() Then
                            CheckQuatationOrderUpdateCompanyData()
                            'End If
                            If Not oCompanyGE.Save(False, sError) Then
                                lblMsg.Text = sError
                            End If
                        Next
                    End If
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
                If Not CurrentCompanyPayStudentTable Is Nothing AndAlso CurrentCompanyPayStudentTable.Rows.Count > 0 Then
                    For Each drCurrentCompanyPay As DataRow In CurrentCompanyPayStudentTable.Rows
                        Dim sISProductId As String = String.Empty
                        If IsDBNull(drCurrentCompanyPay("ProductID")) = False Then
                            sISProductId = Convert.ToString(drCurrentCompanyPay("ProductID"))
                        Else
                            sISProductId = "0"
                        End If

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

                        Dim sRouteOfEntry As String = String.Empty
                        If Not drCurrentCompanyPay("RouteOfEntryID") Is Nothing AndAlso Convert.ToInt32(drCurrentCompanyPay("RouteOfEntryID")) > 0 Then
                            sRouteOfEntry = Convert.ToString(drCurrentCompanyPay("RouteOfEntryID"))
                        Else
                            sRouteOfEntry = "0"
                        End If
                        Dim sAttempts As String = String.Empty
                        If Not drCurrentCompanyPay("Attempts") Is Nothing AndAlso IsDBNull(drCurrentCompanyPay("Attempts")) = False Then
                            sAttempts = Convert.ToString(drCurrentCompanyPay("RouteOfEntryID"))
                        End If

                        Dim params(8) As System.Data.IDataParameter
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
                        params(7) = DataAction.GetDataParameter("@Attempts", SqlDbType.VarChar, sAttempts)
                        params(8) = DataAction.GetDataParameter("@RouteOfEntryID", SqlDbType.VarChar, sRouteOfEntry)

                        Dim sSQL As String = Database & "..spUpdateWhoPayCompanyOrderForEducation__c"
                        'Dim sSQL As String = "..spUpdateWhoPayCompanyOrder__c @CompanyID=" & User1.CompanyID & ",@ProductCategoryID=" & Convert.ToString(drCurrentCompanyPay("ProductCategoryID")) & ",@ProductID=" & sISProductId & ",@StartDate='" & New DateTime(Year(CDate(sIsStartDate)), Month(CDate(sIsStartDate)), Day(CDate(sIsStartDate))) & "',@EndDate='" & New DateTime(Year(CDate(sIsEndDate)), Month(CDate(sIsEndDate)), Day(CDate(sIsEndDate))) & "'"
                        'Dim dt As DataTable = DataAction.GetDataTable(sSQL)
                        Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                        '--------------------------------------
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            '' Dim oNewOrderGE As Aptify.Applications.OrderEntry.OrdersEntity
                            '''oNewOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                            For Each updateDr As DataRow In dt.Rows
                                Dim oOrderGE As AptifyGenericEntityBase
                                oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(updateDr("ID")))
                                oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", drCurrentCompanyPay("CompanyID"))
                                oOrderGE.Save()
                                ''  If oOrderGE.SubTypes("OrderLines").Count > 1 Then
                                ''    ' Code added by govind for redmine issue create seprate order whoes having Bill TO Company
                                ''    oNewOrderGE.ShipToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                                ''    oNewOrderGE.BillToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                                ''    '  If CBool(oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).GetValue("ProducID", CBool(Convert.ToInt32(updateDr("ID"))))) Then
                                ''    oNewOrderGE.AddProduct(CLng(updateDr("ProductID")))
                                ''    oNewOrderGE.SubTypes("OrderLines").Item(oNewOrderGE.SubTypes("OrderLines").Count - 1).SetValue("BillToCompanyID__c", User1.CompanyID)
                                ''    oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).Delete()
                                ''      Dim sError As String = String.Empty
                                ''     If oOrderGE.Save(False, sError) Then
                                ''        oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                ''        oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                ''        oOrderGE.SetValue("BillToCompanyID", -1)
                                ''        oOrderGE.SetValue("FirmPay__c", 0)
                                ''        oOrderGE.Save()
                                ''    End If
                                ''Else
                                ''    oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", User1.CompanyID)
                                ''      Dim sSQLBillingContact As String = "..spGetBillingContactID__c @CompanyID=" & User1.CompanyID
                                ''        Dim iBillingContactID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQLBillingContact))
                                ''        If iBillingContactID > 0 Then
                                ''            oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                ''            oOrderGE.SetValue("BillToID", iBillingContactID)
                                ''        End If
                                ''     Dim sError As String = String.Empty
                                ''     If oOrderGE.Save(False, sError) Then

                                ''     End If
                                ''End If
                               
                            Next
                            ''If oNewOrderGE.SubTypes("OrderLines").Count > 0 Then
                            ''    oNewOrderGE.OrderType = OrderType.Quotation
                            ''    oNewOrderGE.Save()
                            ''End If
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
                        If Not CurrentCompanyPayStudentTable Is Nothing AndAlso CurrentCompanyPayStudentTable.Rows.Count > 0 Then
                            For Each drCurrentCompanyPay As DataRow In CurrentCompanyPayStudentTable.Rows
                                If Convert.ToInt32(dr("ProductCategoryID")) = Convert.ToInt32(drCurrentCompanyPay("ProductCategoryID")) AndAlso Convert.ToInt32(dr("ProductID")) = Convert.ToInt32(drCurrentCompanyPay("ProductID")) AndAlso Convert.ToString(dr("StartDate")).Trim.ToLower = Convert.ToString(drCurrentCompanyPay("StartDate")).Trim.ToLower AndAlso Convert.ToString(dr("EndDate")).Trim.ToLower = Convert.ToString(drCurrentCompanyPay("EndDate")).Trim.ToLower AndAlso Convert.ToInt32(dr("RouteOfEntryID")) = Convert.ToInt32(drCurrentCompanyPay("RouteOfEntryID")) AndAlso Convert.ToString(dr("Attempts")) = Convert.ToString(drCurrentCompanyPay("Attempts")) AndAlso Convert.ToString(dr("CompanyID")) = Convert.ToString(drCurrentCompanyPay("CompanyID")) Then
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

                            Dim sRouteOfEntry As String = String.Empty
                            If Not dr("RouteOfEntryID") Is Nothing AndAlso Convert.ToInt32(dr("RouteOfEntryID")) > 0 Then
                                sRouteOfEntry = Convert.ToString(dr("RouteOfEntryID"))
                            Else
                                sRouteOfEntry = "0"
                            End If
                            Dim sAttempts As String = String.Empty
                            If Not dr("Attempts") Is Nothing AndAlso IsDBNull(dr("Attempts")) = False Then
                                sAttempts = Convert.ToString(dr("Attempts"))
                            End If
                            Dim sCompanyID As String = String.Empty
                            If Not dr("CompanyID") Is Nothing AndAlso Convert.ToInt32(dr("CompanyID")) > 0 Then
                                sCompanyID = Convert.ToString(dr("CompanyID"))
                            Else
                                sCompanyID = "0"
                            End If


                            Dim params(8) As System.Data.IDataParameter
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
                            params(7) = DataAction.GetDataParameter("@Attempts", SqlDbType.VarChar, sAttempts)
                            params(8) = DataAction.GetDataParameter("@RouteOfEntryID", SqlDbType.VarChar, sRouteOfEntry)

                            Dim sSQL As String = Database & "..spUpdateWhoPayCompanyOrderForEducation__c"
                            Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                            '-------------------------------------------------------
                            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                                For Each updateDr As DataRow In dt.Rows
                                    Dim oOrderGE As AptifyGenericEntityBase
                                    oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(updateDr("ID")))
                                    If Convert.ToInt32(updateDr("BillToCompanyID__c")) > 0 Then
                                        oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", -1)
                                          If oOrderGE.Save Then
                                            oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                            oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                            oOrderGE.SetValue("BillToCompanyID", -1)
                                            oOrderGE.SetValue("FirmPay__c", 0)
                                            'oOrderGE.Save()
                                        End If
                                        Dim sError As String = String.Empty
                                        If oOrderGE.Save(False, sError) Then
                                            'Get the Process Flow ID to be used for sending the Firm Rejected Payment 
                                            Dim sSQLPF As String = Database & "..spGetProcessFlowIDFromName__c @Name='Send Email For Rejected Firm For Who Pays Elevation Students__c'"
                                            Dim lProcessFlowID As Long = CLng(DataAction.ExecuteScalar(sSQLPF, IAptifyDataAction.DSLCacheSetting.UseCache))
                                            Dim context As New AptifyContext
                                            context.Properties.AddProperty("MessageTemplateID", CLng(AptifyApplication.GetEntityRecordIDFromRecordName("Message Templates", "Notification - Firm Rejected Who Pays")))
                                            context.Properties.AddProperty("PersonID", Convert.ToInt32(updateDr("PersonID")))
                                            context.Properties.AddProperty("ClassName", Convert.ToString(updateDr("ClassTitle")))
                                            Dim result As ProcessFlowResult
                                            result = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
                                            If result.IsSuccess Then
                                            End If
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
                ''Added By Pradip 2016-01-12 For Group 3 Tracker G3-48
                ' CheckQuatationOrderUpdateIndividualData()
                If CheckApprovedEE() Then
                    CheckQuatationOrderUpdateIndividualData()
                End If
                If Not CurrentIndividualPayStudentTable Is Nothing AndAlso CurrentIndividualPayStudentTable.Rows.Count > 0 Then
                    For Each dr As DataRow In CurrentIndividualPayStudentTable.Rows
                        If Not dr.RowState = DataRowState.Deleted AndAlso Not dr.RowState = DataRowState.Detached Then
                            oPersonGE = Me.AptifyApplication.GetEntityObject("Persons", Convert.ToInt32(dr("PersonID")))
                            ' oPersonGE.SubTypes("PersonWhoPays__c").Clear()
                            For Each dr1 As DataRow In CurrentIndividualPayStudentTable.Rows
                                If Not dr1.RowState = DataRowState.Deleted AndAlso Not dr1.RowState = DataRowState.Detached Then
                                    If oPersonGE.RecordID = Convert.ToInt64(dr1("PersonID")) Then
                                        If Convert.ToInt32(dr1("ID")) = 0 Then
                                            dr1("ID") = 1
                                            ' CurrentIndividualPayStudentTable.Rows(dr
                                            CurrentIndividualPayStudentTable.AcceptChanges()
                                            'CurrentIndividualPayStudentTable.Rows(
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
                                                .SetValue("RouteOfEntryID", dr1("RouteOfEntryID"))
                                                .SetValue("Attempts", dr1("Attempts"))
                                                .SetValue("CompanyID", dr1("CompanyID"))
                                                .SetValue("NonEditableForFirm", dr1("NonEditableForFirm"))
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
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub CheckQuatationOrderUpdateIndividualData()
            Try
                If Not CurrentIndividualPayStudentTable Is Nothing AndAlso CurrentIndividualPayStudentTable.Rows.Count > 0 Then
                    For Each dr As DataRow In CurrentIndividualPayStudentTable.Rows

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

                        Dim sRouteOfEntry As String = String.Empty
                        If Not dr("RouteOfEntryID") Is Nothing AndAlso Convert.ToInt32(dr("RouteOfEntryID")) > 0 Then
                            sRouteOfEntry = Convert.ToString(dr("RouteOfEntryID"))
                        Else
                            sRouteOfEntry = "0"
                        End If
                        Dim sAttempts As String = String.Empty
                        If Not dr("Attempts") Is Nothing AndAlso IsDBNull(dr("Attempts")) = False Then
                            sAttempts = Convert.ToString(dr("Attempts"))
                        End If

                        Dim params(8) As System.Data.IDataParameter
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
                        params(7) = DataAction.GetDataParameter("@Attempts", SqlDbType.VarChar, sAttempts)
                        params(8) = DataAction.GetDataParameter("@RouteOfEntryID", SqlDbType.VarChar, sRouteOfEntry)

                        'Dim sSQL As String = "..spUpdateWhoPayOrderDetail__c @PersonID=" & Convert.ToString(dr("PersonID")) & ",@ProductCategoryID=" & Convert.ToString(dr("ProductCategoryID")) & ",@ProductID='" & sISProductId & "',@StartDate='" & sIsStartDate & "',@EndDate='" & sIsEndDate & "'"
                        Dim sSQL As String = Database & "..spUpdateWhoPayOrderDetailForEducation__c"
                        ' @PersonID=" & Convert.ToString(dr("PersonID")) & ",@ProductCategoryID=" & Convert.ToString(dr("ProductCategoryID")) & ",@ProductID='" & sISProductId & "',@StartDate='" & New DateTime(Year(CDate(sIsStartDate)), Month(CDate(sIsStartDate)), Day(CDate(sIsStartDate))) & "',@EndDate='" & New DateTime(Year(CDate(sIsEndDate)), Month(CDate(sIsEndDate)), Day(CDate(sIsEndDate))) & "'"
                        Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                           'Redmine issue split orders
                            '' Dim oNewOrderGE As Aptify.Applications.OrderEntry.OrdersEntity
                            '' oNewOrderGE = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                            For Each updateDr As DataRow In dt.Rows
                                Dim oOrderGE As AptifyGenericEntityBase
                                oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(updateDr("ID")))
                                Dim bRemoveBillToCompany As Boolean = False
                                If Convert.ToString(dr("PayType")).Trim.ToLower = "firm pays" Then
                                    oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", User1.CompanyID)
                                    '' If oOrderGE.SubTypes("OrderLines").Count > 1 Then
                                    ''    ' Code added by govind for redmine issue create seprate order whoes having Bill TO Company
                                    ''    oNewOrderGE.ShipToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                                    ''    oNewOrderGE.BillToID = Convert.ToInt32(oOrderGE.GetValue("ShipToID"))
                                    ''    oNewOrderGE.AddProduct(CLng(updateDr("ProductID")))
                                    ''    oNewOrderGE.SubTypes("OrderLines").Item(oNewOrderGE.SubTypes("OrderLines").Count - 1).SetValue("BillToCompanyID__c", User1.CompanyID)
                                    ''    oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).Delete()
                                    ''     If oOrderGE.Save() Then
                                    ''             oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                    ''        oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                    ''        oOrderGE.SetValue("BillToCompanyID", -1)
                                    ''        oOrderGE.SetValue("FirmPay__c", 0)
                                    ''     End If
                                    ''Else
                                    ''    oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", User1.CompanyID)
                                    ''      Dim sSQLBillingContact As String = "..spGetBillingContactID__c @CompanyID=" & Convert.ToInt32(dr("CompanyID"))
                                    ''    Dim iBillingContactID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQLBillingContact))
                                    ''    If iBillingContactID > 0 Then
                                    ''        oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                    ''        oOrderGE.SetValue("BillToID", iBillingContactID)
                                    ''    End If  
                                    ''End If
                                Else
                                    oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", -1)
                                    ''If oOrderGE.Save Then
                                    ''    oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                    ''    oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                    ''    oOrderGE.SetValue("BillToCompanyID", -1)
                                    ''    oOrderGE.SetValue("FirmPay__c", 0)
                                    ''    'oOrderGE.Save()
                                    ''End If
                                    bRemoveBillToCompany = True
                                End If
                                Dim sError As String = String.Empty
                                If oOrderGE.Save(False, sError) Then
                                    If bRemoveBillToCompany = True Then
                                        'Get the Process Flow ID to be used for sending the Firm Rejected Payment 
                                        Dim sSQLPF As String = Database & "..spGetProcessFlowIDFromName__c @Name='Send Email For Rejected Firm For Who Pays Elevation Students__c'"
                                        Dim lProcessFlowID As Long = CLng(DataAction.ExecuteScalar(sSQLPF, IAptifyDataAction.DSLCacheSetting.UseCache))
                                        Dim context As New AptifyContext
                                        context.Properties.AddProperty("MessageTemplateID", CLng(AptifyApplication.GetEntityRecordIDFromRecordName("Message Templates", "Notification - Firm Rejected Who Pays")))
                                        context.Properties.AddProperty("PersonID", Convert.ToInt32(updateDr("PersonID")))
                                        context.Properties.AddProperty("ClassName", Convert.ToString(updateDr("ClassTitle")))
                                        Dim result As ProcessFlowResult
                                        result = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
                                        If result.IsSuccess Then
                                        End If
                                    End If
                                End If
                            Next
                            ''If oNewOrderGE.SubTypes("OrderLines").Count > 0 Then
                            ''    oNewOrderGE.OrderType = OrderType.Quotation
                            ''    oNewOrderGE.Save()
                            ''End If
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


                        If Not CurrentIndividualPayStudentTable Is Nothing AndAlso CurrentIndividualPayStudentTable.Rows.Count > 0 Then
                            For Each drCurrentIndividualPay As DataRow In CurrentIndividualPayStudentTable.Rows
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

                            Dim sRouteOfEntry As String = String.Empty
                            If Not dr("RouteOfEntryID") Is Nothing AndAlso Convert.ToInt32(dr("RouteOfEntryID")) > 0 Then
                                sRouteOfEntry = Convert.ToString(dr("RouteOfEntryID"))
                            Else
                                sRouteOfEntry = "0"
                            End If
                            Dim sAttempts As String = String.Empty
                            If Not dr("Attempts") Is Nothing AndAlso IsDBNull(dr("Attempts")) = False Then
                                sAttempts = Convert.ToString(dr("Attempts"))
                            End If

                            Dim params(8) As System.Data.IDataParameter
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
                            params(7) = DataAction.GetDataParameter("@Attempts", SqlDbType.VarChar, sAttempts)
                            params(8) = DataAction.GetDataParameter("@RouteOfEntryID", SqlDbType.VarChar, sRouteOfEntry)
                            'Dim sSQL As String = "..spUpdateWhoPayOrderDetail__c @PersonID=" & Convert.ToString(dr("PersonID")) & ",@ProductCategoryID=" & Convert.ToString(dr("ProductCategoryID")) & ",@ProductID='" & sISProductId & "',@StartDate='" & sIsStartDate & "',@EndDate='" & sIsEndDate & "'"
                            Dim sSQL As String = Database & "..spUpdateWhoPayOrderDetailForEducations__c"
                            ' @PersonID=" & Convert.ToString(dr("PersonID")) & ",@ProductCategoryID=" & Convert.ToString(dr("ProductCategoryID")) & ",@ProductID='" & sISProductId & "',@StartDate='" & New DateTime(Year(CDate(sIsStartDate)), Month(CDate(sIsStartDate)), Day(CDate(sIsStartDate))) & "',@EndDate='" & New DateTime(Year(CDate(sIsEndDate)), Month(CDate(sIsEndDate)), Day(CDate(sIsEndDate))) & "'"
                            Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                                For Each updateDr As DataRow In dt.Rows
                                    Dim oOrderGE As AptifyGenericEntityBase
                                    oOrderGE = AptifyApplication.GetEntityObject("Orders", Convert.ToInt32(updateDr("ID")))
                                    If Convert.ToInt32(updateDr("BillToCompanyID__c")) > 0 Then
                                        oOrderGE.SubTypes("OrderLines").Item(Convert.ToInt32(updateDr("Sequence")) - 1).SetValue("BillToCompanyID__c", -1)
                                            If oOrderGE.Save Then
                                            oOrderGE.SetValue("BillToSameAsShipTo", 0)
                                            oOrderGE.SetValue("BillToID", oOrderGE.GetValue("ShipToID"))
                                            oOrderGE.SetValue("BillToCompanyID", -1)
                                            oOrderGE.SetValue("FirmPay__c", 0)
                                            'oOrderGE.Save()
                                        End If
                                        Dim sError As String = String.Empty
                                        If oOrderGE.Save(False, sError) Then
                                            'Get the Process Flow ID to be used for sending the Firm Rejected Payment 
                                            Dim sSQLPF As String = Database & "..spGetProcessFlowIDFromName__c @Name='Send Email For Rejected Firm For Who Pays Elevation Students__c'"
                                            Dim lProcessFlowID As Long = CLng(DataAction.ExecuteScalar(sSQLPF, IAptifyDataAction.DSLCacheSetting.UseCache))
                                            Dim context As New AptifyContext
                                            context.Properties.AddProperty("MessageTemplateID", CLng(AptifyApplication.GetEntityRecordIDFromRecordName("Message Templates", "Notification - Firm Rejected Who Pays")))
                                            context.Properties.AddProperty("PersonID", Convert.ToInt32(updateDr("PersonID")))
                                            context.Properties.AddProperty("ClassName", Convert.ToString(updateDr("ClassTitle")))
                                            Dim result As ProcessFlowResult
                                            result = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
                                            If result.IsSuccess Then
                                            End If
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

        ''' <summary>
        ''' Added By Pradip 2016-01-10 to Check Approve EE For Person (For G3 Tracker G3-48)
        ''' </summary>
        ''' <remarks></remarks>
        Private Function CheckApprovedEE() As Boolean
            Try
                Dim oParams(0) As IDataParameter
                oParams(0) = Me.DataAction.GetDataParameter("@PersonId", SqlDbType.BigInt, User1.PersonID)
                Dim sSQL As String = Database & "..spCheckApprovedEE__c"
                Dim iCheck As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, oParams))
                If iCheck > 0 Then
                    Return True
                Else
                    Return False
                End If
                Return False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function


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
                    Dim obj(10) As Object
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
                    obj(9) = commandArgs(9)
                    obj(10) = commandArgs(10)
                    BindDeleteCompanyData(Convert.ToInt32(obj(1)), Convert.ToInt32(obj(2)), Convert.ToString(obj(3)), Convert.ToString(obj(4)), Convert.ToInt32(obj(5)), Convert.ToInt32(obj(6)), Convert.ToInt32(obj(7)), Convert.ToString(obj(8)), Convert.ToInt32(obj(9)), Convert.ToInt32(obj(10)))

                    'CurrentCompanyPayStudentTable.Rows.RemoveAt(CInt(commandArgs(0)))

                    Dim dr() As DataRow = CurrentCompanyPayStudentTable.Select("CompanyID=" & Convert.ToInt32(obj(9)) & "AND ProductCategoryID=" & Convert.ToInt32(obj(1)) & "AND ProductID=" & Convert.ToInt32(obj(2)) & " AND MemberTypeID=" & Convert.ToInt32(obj(5)) & "AND RoleID=" & Convert.ToInt32(obj(6)) & "AND RouteOfEntryID=" & Convert.ToInt32(obj(7)) & "AND Attempts='" & Convert.ToString(obj(8)) & "'")
                    If dr.Length > 0 Then
                        For i As Integer = 0 To dr.Length - 1
                            CurrentCompanyPayStudentTable.Rows.Remove(dr(i))
                            CurrentCompanyPayStudentTable.AcceptChanges()
                        Next
                    End If

                    grdCompanyPay.DataSource = CurrentCompanyPayStudentTable
                    grdCompanyPay.DataBind()
                    UpdatePanel1.Update()
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
                    Dim obj(12) As Object
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
                    obj(11) = commandArgs(11)
                    obj(12) = commandArgs(12)
                    BindDeleteIndividualData(Convert.ToInt32(obj(1)), Convert.ToInt32(obj(2)), Convert.ToInt32(obj(3)), Convert.ToString(obj(4)), Convert.ToString(obj(5)), Convert.ToString(obj(6)), Convert.ToInt32(obj(7)), Convert.ToInt32(obj(8)), Convert.ToInt32(obj(9)), Convert.ToString(obj(10)), Convert.ToInt32(obj(11)), Convert.ToInt32(obj(12)))

                    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    ' below logic for get the person id for clear sub types record
                    If Convert.ToString(ViewState("ClearPersonWhoPays")) = "" Then
                        ViewState("ClearPersonWhoPays") = Convert.ToString(obj(12))
                    Else
                        ViewState("ClearPersonWhoPays") = Convert.ToString(ViewState("ClearPersonWhoPays")) & "," & Convert.ToString(obj(12))
                    End If
                    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    ' CurrentIndividualPayStudentTable.Rows.RemoveAt(CInt(commandArgs(0)))
                    Dim dr() As DataRow = CurrentIndividualPayStudentTable.Select("PersonID=" & Convert.ToInt32(obj(1)) & "AND ProductCategoryID=" & Convert.ToInt32(obj(2)) & "AND ProductID=" & Convert.ToInt32(obj(3)) & "AND Paytype='" & Convert.ToString(obj(6)) & "' AND MemberTypeID=" & Convert.ToInt32(obj(7)) & "AND RoleID=" & Convert.ToInt32(obj(8)) & "AND RouteOfEntryID=" & Convert.ToInt32(obj(9)) & "And Attempts='" & Convert.ToString(obj(10)) & "' AND CompanyID=" & Convert.ToInt32(obj(11)))
                    If dr.Length > 0 Then
                        For i As Integer = 0 To dr.Length - 1
                            CurrentIndividualPayStudentTable.Rows.Remove(dr(i))
                            CurrentIndividualPayStudentTable.AcceptChanges()
                        Next
                    End If
                    grdIndividualPay.DataSource = CurrentIndividualPayStudentTable
                    grdIndividualPay.DataBind()
                    UpdatePanel1.Update()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub BindDeleteIndividualData(ByVal PersonID As Integer, ByVal ProductCateID As Integer, ByVal ProductID As Integer, ByVal StartDate As String, ByVal EndDate As String, ByVal Paytype As String, ByVal MemberTypeID As Integer, ByVal RoleID As Integer, ByVal RouteOfEntryID As Integer, ByVal Attempts As String, ByVal CompanyID As Integer, ByVal RecordID As Integer)
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
                dr("RouteOfEntryID") = RouteOfEntryID
                dr("Attempts") = Attempts
                dr("ID") = RecordID
                dtCurrentIndividualPayDeleteTable.Rows.Add(dr)
                CurrentIndividualPayDeleteTable = dtCurrentIndividualPayDeleteTable
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub
        Protected Sub BindDeleteCompanyData(ByVal ProductCateID As Integer, ByVal ProductID As Integer, ByVal StartDate As String, ByVal EndDate As String, ByVal MemberTypeID As Integer, ByVal RoleId As Integer, ByVal RouteOfEntryID As Integer, ByVal Attempt As String, ByVal CompanyId As Integer, ByVal CompanyWhoPayID As Integer)
            Try
                Dim dtCurrentCompanyPayDeleteTable As DataTable = CurrentCompanyPayDeleteTable
                Dim dr As DataRow = dtCurrentCompanyPayDeleteTable.NewRow()
                dr("ProductCategoryID") = ProductCateID
                dr("ProductID") = ProductID
                dr("StartDate") = StartDate
                dr("EndDate") = EndDate
                dr("MemberTypeID") = MemberTypeID
                dr("RoleID") = RoleId
                dr("RouteOfEntryID") = RouteOfEntryID
                dr("Attempts") = Attempt
                dr("CompanyID") = CompanyId
                dr("ID") = CompanyWhoPayID
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
                grdCompanyPay.DataSource = CurrentCompanyPayStudentTable
                grdCompanyPay.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub grdIndividualPay_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdIndividualPay.PageIndexChanging
            Try
                grdIndividualPay.PageIndex = e.NewPageIndex
                grdIndividualPay.DataSource = CurrentIndividualPayStudentTable
                grdIndividualPay.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

        Protected Sub cmbMemberTypeIndi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMemberTypeIndi.SelectedIndexChanged
            Try
                Dim RouteOfEntryID As Integer = 0
                If cmbIndividualRouteOfEntry.SelectedIndex > 0 Then
                    RouteOfEntryID = Convert.ToInt32(cmbIndividualRouteOfEntry.SelectedValue)
                End If
                Dim sSql As String

                If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    'If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    If Convert.ToInt32(cmbRoleIndi.SelectedValue) > 0 Then
                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & _
                                                                               ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If

                    Else
                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=0" & ",@RouteOfEntryID=" & RouteOfEntryID

                        End If
                    End If
                Else

                    If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If
                    Else
                        BindPersonList()
                    End If
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
                Dim RouteOfEntryID As Integer = 0
                If cmbIndividualRouteOfEntry.SelectedIndex > 0 Then
                    RouteOfEntryID = Convert.ToInt32(cmbIndividualRouteOfEntry.SelectedValue)
                End If
                Dim sSql As String

                If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    If Convert.ToInt32(cmbMemberTypeIndi.SelectedValue) > 0 Then
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                 ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If

                    Else
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=0" & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If
                    End If
                Else

                    If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If
                    Else
                        BindPersonList()
                    End If
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
                Dim RouteOfEntryID As Integer = 0
                If cmbIndividualRouteOfEntry.SelectedIndex > 0 Then
                    RouteOfEntryID = Convert.ToInt32(cmbIndividualRouteOfEntry.SelectedValue)
                End If
                Dim sSql As String

                If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    If Convert.ToInt32(cmbMemberTypeIndi.SelectedValue) > 0 Then
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                 ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If

                    Else
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=0" & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If
                    End If
                Else

                    If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If
                    Else
                        BindPersonList()
                    End If
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

        Protected Sub cmbIndividualRouteOfEntry_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbIndividualRouteOfEntry.SelectedIndexChanged
            Try
                Dim RouteOfEntryID As Integer = 0
                If cmbIndividualRouteOfEntry.SelectedIndex > 0 Then
                    RouteOfEntryID = Convert.ToInt32(cmbIndividualRouteOfEntry.SelectedValue)
                End If
                Dim sSql As String

                If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                    If Convert.ToInt32(cmbMemberTypeIndi.SelectedValue) > 0 Then
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                 ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If

                    Else
                        If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=0" & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If
                    End If
                Else

                    If Convert.ToString(cmbRoleIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectPrimaryRole")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                        If Convert.ToString(cmbMemberTypeIndi.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectMemberType")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@MemberTypeID=" & cmbMemberTypeIndi.SelectedValue & _
                                                                                ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        Else
                            sSql = Database & "..spGetContractPersonsAsPerMemberTypeAndRole__c @CompanyID=" & cmbIndividualCompany.SelectedValue & ",@RoleID=" & cmbRoleIndi.SelectedValue & ",@RouteOfEntryID=" & RouteOfEntryID
                        End If
                    Else
                        BindPersonList()
                    End If
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



