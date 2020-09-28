'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Kavita Zinage             16/12/2015                        MarketPlace Listing Search Page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.MarketPlace
    Partial Class Search__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_VIEW_LISTING_PAGE As String = "ViewListingPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "Search__c"
        Protected Const ATTRIBUTE_CUSTOMERSERVICESEARCHDT_VIEWSTATE As String = "CustomerServiceSearchdt"


#Region "MarketPlaceSearch Specific Properties"
        ''' <summary>
        ''' ViewListing page url
        ''' </summary>
        Public Overridable Property ViewListingPage() As String
            Get
                If Not ViewState(ATTRIBUTE_VIEW_LISTING_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_VIEW_LISTING_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_VIEW_LISTING_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(ViewListingPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ViewListingPage = Me.GetLinkValueFromXML(ATTRIBUTE_VIEW_LISTING_PAGE)
                If String.IsNullOrEmpty(ViewListingPage) Then
                    Me.cmdSearch.Enabled = False
                    Me.cmdSearch.ToolTip = "ViewListingPage property has not been set."
                End If
            End If

        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'set control properties from XML file if needed
            'Suraj issue14454 4/5/13 ,this method use to apply the odrering of rad grid first column
            If Not IsPostBack Then
                AddExpressionMarketPlaceSerch()
            End If
            SetProperties()
            grdListings.Visible = False
        End Sub
        ''' <summary>
        ''' Nalini 12436 date:1/12/2011
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmdSearch_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) _
                                    Handles cmdSearch.Click
            AddExpressionMarketPlaceSerch()
            grdListings.Visible = True
            'FillgrdListings()
            'Suraj issue 14454 , 3/5/13 , Here we assign nothing to the view state because after click on search button result will be a diffrent.
            ViewState(ATTRIBUTE_CUSTOMERSERVICESEARCHDT_VIEWSTATE) = Nothing
            grdListings.Rebind()
        End Sub

        Protected Sub grdListings_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdListings.NeedDataSource
            FillgrdListings()
        End Sub
        ''' <summary>
        ''' Nalini 12436 date:1/12/2011
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub FillgrdListings()
            Dim sSQL As String
            Dim dt As DataTable
            Dim iFilters As Integer

            'HP Issue#6594: parameterize input for defense against SQL injections
            Dim pListing As Data.IDataParameter = Nothing
            Dim pVendor As Data.IDataParameter = Nothing
            Dim pDescription As Data.IDataParameter = Nothing

            'Added By Kavita
            Dim iPosteddym As Data.IDataParameter = Nothing
            Dim iATI As Data.IDataParameter = Nothing
            Dim iGraduate As Data.IDataParameter = Nothing
            Dim iSChoolleaver As Data.IDataParameter = Nothing
            Dim sCounty As Data.IDataParameter = Nothing

            Dim colParams(7) As Data.IDataParameter
            pListing = DataAction.GetDataParameter("@Listing", SqlDbType.NVarChar, txtName.Text.Trim)
            pVendor = DataAction.GetDataParameter("@Vendor", SqlDbType.NVarChar, txtVendor.Text.Trim)
            pDescription = DataAction.GetDataParameter("@Description", SqlDbType.NVarChar, txtDescription.Text.Trim)

            'Added by Kavita Zinage
            iPosteddym = DataAction.GetDataParameter("@Posteddmy", SqlDbType.Int, Convert.ToInt32(cmbRecency.SelectedItem.Value))

            If chkAti.Checked Then
                iATI = DataAction.GetDataParameter("@ChkATI", SqlDbType.Int, 1)
            Else
                iATI = DataAction.GetDataParameter("@ChkATI", SqlDbType.Int, 0)
            End If

            If chkGraduate.Checked Then
                iGraduate = DataAction.GetDataParameter("@ChkGraduate", SqlDbType.Int, 1)
            Else
                iGraduate = DataAction.GetDataParameter("@ChkGraduate", SqlDbType.Int, 0)
            End If

            If chkSchoolLeaver.Checked Then
                iSChoolleaver = DataAction.GetDataParameter("@ChkSChoolleaver", SqlDbType.Int, 1)
            Else
                iSChoolleaver = DataAction.GetDataParameter("@ChkSChoolleaver", SqlDbType.Int, 0)
            End If

            sCounty = DataAction.GetDataParameter("@County", SqlDbType.NVarChar, txtCounty.Text.Trim)

            colParams(0) = pListing
            colParams(1) = pVendor
            colParams(2) = pDescription

            'Added By Kavita
            colParams(3) = iPosteddym
            colParams(4) = iATI
            colParams(5) = iGraduate
            colParams(6) = iSChoolleaver
            colParams(7) = sCounty
            Try
                'Suraj issue 14454 2/15/13 , check the view state is nothing or not if the page load first time viewstate will be nothing but after bostback view state will conatin the datatable
                If ViewState(ATTRIBUTE_CUSTOMERSERVICESEARCHDT_VIEWSTATE) Is Nothing Then
                    
                    'Added By Kavita 17/12/2015
                    sSQL = Database & "..spGetMarketListingSearchDetails__c"
                    If txtName.Text.Length > 0 Or txtVendor.Text.Length > 0 Or txtDescription.Text.Length > 0 Or txtCounty.Text.Length > 0 Then
                        iFilters = 1
                    End If
                    If iFilters > 0 Then
                        'HP Issue#6594: parameterize input for defense against SQL injections
                        dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, colParams)

                        Dim dcolUrl As DataColumn = New DataColumn()
                        dcolUrl.Caption = "DataNavigateUrl"
                        dcolUrl.ColumnName = "DataNavigateUrl"

                        dt.Columns.Add(dcolUrl)
                        If dt.Rows.Count > 0 Then
                            For Each rw As DataRow In dt.Rows
                                Dim tempURL As String = ViewListingPage & "?ID" & "=" & rw("ID")


                                Dim index As Integer = tempURL.IndexOf("=")
                                Dim sValue As String = tempURL.Substring(index + 1)
                                Dim separator As String() = tempURL.Split(CChar("="))
                                Dim navigate As String = separator(0)
                                navigate = navigate & "="
                                navigate = navigate & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
                                rw("DataNavigateUrl") = navigate
                            Next
                        End If
                        If dt.Rows.Count > 0 Then
                            lblError.Visible = False
                            trResults.Visible = True
                            'Navin Prasad Issue 11032
                            ' DirectCast(grdListings.Columns(1), HyperLinkColumn).DataNavigateUrlFormatString = ViewListingPage & "?ID={0}"

                            grdListings.DataSource = dt
                            'Suraj issue 14454 2/15/13 , if when page first time load here we store the datatable in to a viewstate
                            ViewState(ATTRIBUTE_CUSTOMERSERVICESEARCHDT_VIEWSTATE) = dt
                            'grdListings.DataBind()
                            grdListings.Visible = True
                        Else
                            lblError.Visible = True
                            lblError.Text = "No Results Match Search Criteria"
                            trResults.Visible = False
                            grdListings.Visible = False

                        End If
                    Else
                        lblError.Visible = True
                        trResults.Visible = False
                        lblError.Text = "Please enter one or more search criteria above"
                    End If
                Else
                    'Suraj issue 14454 2/15/13 , after postback viewstate will assign for gridview
                    grdListings.DataSource = ViewState(ATTRIBUTE_CUSTOMERSERVICESEARCHDT_VIEWSTATE)
                    lblError.Visible = False
                    trResults.Visible = True
                    grdListings.Visible = True
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Nalini 12436 date:1/12/2011
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub grdListings_PageIndexChanging(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdListings.PageIndexChanged
            ''grdListings.PageIndex = e.NewPageIndex
            FillgrdListings()
        End Sub
        Protected Sub grdListings_PageIndexChanging(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles grdListings.PageSizeChanged
            ''grdListings.PageIndex = e.NewPageIndex
            FillgrdListings()
        End Sub
        'Suraj Issue 14454 4/5/13 ,if the grid load first time By default the sorting will be Ascending for column  
        Private Sub AddExpressionMarketPlaceSerch()
            Dim expressionCompanyMembership As New GridSortExpression
            expressionCompanyMembership.FieldName = "Company"
            expressionCompanyMembership.SetSortOrder("Ascending")
            grdListings.MasterTableView.SortExpressions.AddSortExpression(expressionCompanyMembership)
        End Sub
    End Class
End Namespace
