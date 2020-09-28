

Imports System.Data
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.Chapters
    Partial Class MyChaptersControl
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "MyChapters"
        Protected Const ATTRIBUTE_DATATABLE_CHAPTER As String = "dtChapters"

        Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage"
        'suraj issue 13234 
#Region "Chapter Specific Properties"
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
#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not IsPostBack Then
                'suraj issue 13234 
                If User1.UserID > 0 Then
                    ''LoadChapters()
                Else
                    'Suraj S Issue 15370, 3/29/13 change session variale to application variable
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(Me.LoginPage)
                End If
                'Amruta Issue 14448
                AddExpression()
            End If
        End Sub

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(Me.RedirectURL) Then
                Me.grdChapters.Enabled = False
                Me.grdChapters.ToolTip = "RedirectURL property has not been set."
            End If

            'if values are not provide directly or from the XML file, set default values for inherited query
            'parameter properties since control requires them to properly function
            If String.IsNullOrEmpty(Me.RedirectIDParameterName) Then Me.RedirectIDParameterName = "ID"
            'Suraj Issue 13234
            If String.IsNullOrEmpty(Me.LoginPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                Me.LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_LOGIN_PAGE)
            End If

        End Sub

        Private Sub LoadChapters()
            ' this function loads up the data grid with chapter information
            Dim sSQL As String, dt As DataTable

            Try
                If ViewState(ATTRIBUTE_DATATABLE_CHAPTER) IsNot Nothing Then
                    grdChapters.DataSource = ViewState(ATTRIBUTE_DATATABLE_CHAPTER)
                Else
                    sSQL = Database & _
                           "..spGetPersonChapters @PersonID=" & User1.PersonID
                    dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

                    Dim dcolUrl As DataColumn = New DataColumn()
                    dcolUrl.Caption = "DataNavigateUrl"
                    dcolUrl.ColumnName = "DataNavigateUrl"

                    dt.Columns.Add(dcolUrl)
                    If dt.Rows.Count > 0 Then
                        For Each rw As DataRow In dt.Rows
                            Dim tempURL As String = Me.RedirectURL & "?" & Me.RedirectIDParameterName & "=" & rw("ID")


                            Dim index As Integer = tempURL.IndexOf("=")
                            Dim sValue As String = tempURL.Substring(index + 1)
                            Dim separator As String() = tempURL.Split(CChar("="))
                            Dim navigate As String = separator(0)
                            navigate = navigate & "="
                            navigate = navigate & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
                            rw("DataNavigateUrl") = navigate
                        Next
                    End If





                    'Navin Prasad Issue 11032
                    'Dim hlink As HyperLinkField = CType(grdChapters.Columns(0), HyperLinkField)
                    'hlink.DataNavigateUrlFormatString = Me.RedirectURL & "?" & Me.RedirectIDParameterName & "={0}"
                    'DirectCast(grdChapters.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = Me.RedirectURL & "?" & Me.RedirectIDParameterName & "={0}"

                    grdChapters.DataSource = dt
                    ViewState(ATTRIBUTE_DATATABLE_CHAPTER) = dt
                    'Commented by Dipali 
                    'grdChapters.DataBind()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdChapters_PageIndexChanging(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdChapters.PageIndexChanged
            'grdChapters.PageIndex = e.NewPageIndex
            LoadChapters()
        End Sub

        Protected Sub grdChapters_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdChapters.NeedDataSource
            LoadChapters()
        End Sub

        'Navin Prasad Issue 11032
        'Protected Sub grdChapters_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdChapters.ItemDataBound
        '    Try
        '        Dim type As ListItemType = e.Item.ItemType
        '        If e.Item.ItemType = ListItemType.Item Or _
        '                e.Item.ItemType = ListItemType.AlternatingItem Then
        '            Dim lnk As HyperLink
        '            Dim tempURL As String
        '            Dim index As Integer
        '            Dim sValue As String
        '            Dim separator As String()

        '            lnk = CType(e.Item.Cells(0).Controls(0), HyperLink)
        '            tempURL = lnk.NavigateUrl
        '            index = tempURL.IndexOf("=")
        '            sValue = tempURL.Substring(index + 1)
        '            separator = lnk.NavigateUrl.Split(CChar("="))
        '            lnk.NavigateUrl = separator(0)
        '            lnk.NavigateUrl = lnk.NavigateUrl & "="
        '            lnk.NavigateUrl = lnk.NavigateUrl & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try

        'End Sub


        'Protected Sub grdChapters_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdChapters.RowDataBound
        '    Try
        '        Dim type As ListItemType = CType(e.Row.RowType, ListItemType)
        '        If CType(e.Row.RowType, ListItemType) = ListItemType.Item Or _
        '                CType(e.Row.RowType, ListItemType) = ListItemType.AlternatingItem Then
        '            Dim lnk As HyperLink
        '            Dim tempURL As String
        '            Dim index As Integer
        '            Dim sValue As String
        '            Dim separator As String()

        '            lnk = CType(e.Row.Cells(0).Controls(0), HyperLink)
        '            tempURL = lnk.NavigateUrl
        '            index = tempURL.IndexOf("=")
        '            sValue = tempURL.Substring(index + 1)
        '            separator = lnk.NavigateUrl.Split(CChar("="))
        '            lnk.NavigateUrl = separator(0)
        '            lnk.NavigateUrl = lnk.NavigateUrl & "="
        '            lnk.NavigateUrl = lnk.NavigateUrl & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        'Amruta Issue 14448 ,4/9/13 ,if the grid load first time By default the sorting will be Ascending for column Chapter 
        Private Sub AddExpression()
            Dim expression1 As New GridSortExpression
            expression1.FieldName = "Name"
            expression1.SetSortOrder("Ascending")
            grdChapters.MasterTableView.SortExpressions.AddSortExpression(expression1)
        End Sub
    End Class
End Namespace
