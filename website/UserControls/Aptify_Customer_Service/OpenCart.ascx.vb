Option Explicit On
Option Strict On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.Security
Imports System.Data
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class OpenCartControl
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_SAVED_CART_PAGE As String = "SavedCartURL"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "OpenCart"

#Region "MakePayment Specific Properties"
        ''' <summary>
        ''' SavedCart page url
        ''' </summary>
        Public Overridable Property SavedCartURL() As String
            Get
                If Not ViewState(ATTRIBUTE_SAVED_CART_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_SAVED_CART_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_SAVED_CART_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            'set control properties from XML file if needed
            SetProperties()
            Try
                If Not IsPostBack() Then
                    ''New Function created by Suvarna D for IssueID 12436 on Dec 1, 2011
                    ''To support paging separate grid bind fuction has been created
                    'Anil B for issue 15302 on 23/04/2013
                    LoadCarts()
                    ''End of Addition IssueID: 12436
                    'Suraj Issue 14450 3/22/13 ,this method use to apply the odrering of rad grid first column
                    AddExpression()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(SavedCartURL) Then
                SavedCartURL = Me.GetLinkValueFromXML(ATTRIBUTE_SAVED_CART_PAGE)
                If String.IsNullOrEmpty(SavedCartURL) Then
                    Me.grdSavedCarts.Enabled = False
                    Me.grdSavedCarts.ToolTip = "SavedCartURL property has not been set."
                End If
            End If
        End Sub

        ''New Function created by Suvarna D for IssueID 12436 on Dec 1, 2011
        ''To support paging separate grid bind function has been created
        Protected Sub LoadCarts()

            Dim sSQL As String
            Dim dt As DataTable

            Try

                ' changed to use Config Settings for virtual directory location
                'sSQL = "SELECT Name,Description, '" & ConfigurationSettings.AppSettings("virtualdir") & _
                '       "ProductCatalog/ViewCart.aspx?ShoppingCartID='+CONVERT(nvarchar(50),ID) 'ShoppingCartURL' " & _
                '       "FROM " & Database & "..vwWebShoppingCarts WHERE WebUserID = " & User1.UserID
                sSQL = "SELECT Name,Description, ID " & _
                       "FROM " & Database & "..vwWebShoppingCarts WHERE WebUserID = " & User1.UserID

                dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                'commented by Navin Prasad Issue 11032

                ' DirectCast(grdSavedCarts.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = Me.SavedCartURL & "?ShoppingCartID={0}"

                Dim dcolUrl As DataColumn = New DataColumn()
                dcolUrl.Caption = "ShoppingCartUrl"
                dcolUrl.ColumnName = "ShoppingCartUrl"

                dt.Columns.Add(dcolUrl)
                If dt.Rows.Count > 0 Then

                    For Each rw As DataRow In dt.Rows
                        rw("ShoppingCartUrl") = Me.SavedCartURL + "?ShoppingCartID=" + rw("ID").ToString
                    Next
                End If


                grdSavedCarts.DataSource = dt
                ''grdSavedCarts.DataBind()
                'Navin Prasad Issue 11032
                'Dim rowcounter As Integer = 0
                'For Each row As GridViewRow In grdSavedCarts.Items
                '    Dim lnk As HyperLink = CType(row.FindControl("lnkName"), HyperLink)
                '    lnk.NavigateUrl = String.Format(Me.SavedCartURL + "?ShoppingCartID={0}", dt.Rows((grdSavedCarts.PageIndex * grdSavedCarts.PageSize) + rowcounter)(2).ToString)
                '    rowcounter = rowcounter + 1
                'Next
                grdSavedCarts.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''End of Addition IssueID: 12436
        Protected Sub grdSavedCarts_PageIndexChanging(ByVal sender As Object, ByVal e As GridPageChangedEventArgs) Handles grdSavedCarts.PageIndexChanged
            '' grdSavedCarts.PageIndex = e.NewPageIndex
            LoadCarts()
        End Sub
        Protected Sub grdSavedCarts_PageIndexChanging(ByVal sender As Object, ByVal e As GridPageSizeChangedEventArgs) Handles grdSavedCarts.PageSizeChanged
            '' grdSavedCarts.PageIndex = e.NewPageIndex
            LoadCarts()
        End Sub
        Protected Sub grdMain_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdSavedCarts.NeedDataSource

            LoadCarts()
            ''grdStudents.Rebind()
        End Sub
        'Suraj Issue 14450 3/22/13 ,if the grid load first time By default the sorting will be Ascending for column Forum 
        Private Sub AddExpression()
            Dim expression1 As New GridSortExpression
            expression1.FieldName = "Name"
            expression1.SetSortOrder("Ascending")
            grdSavedCarts.MasterTableView.SortExpressions.AddSortExpression(expression1)
        End Sub
    End Class
End Namespace
