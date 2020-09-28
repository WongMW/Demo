
Imports System.Data
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.Chapters
    Partial Class ChapterProximitySearchControl
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "ChapterProximitySearch"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()

            If Not IsPostBack Then
                ' start out not showing the table results
                Dim oItem As ListItem

                trResults.Visible = False
                oItem = New ListItem("5", "5")
                cmbMiles.Items.Add(oItem)
                oItem = New ListItem("10", "10")
                cmbMiles.Items.Add(oItem)
                oItem = New ListItem("25", "25")
                cmbMiles.Items.Add(oItem)
                oItem = New ListItem("50", "50")
                cmbMiles.Items.Add(oItem)
                oItem = New ListItem("100", "100")
                cmbMiles.Items.Add(oItem)
                oItem = New ListItem("200", "200")
                cmbMiles.Items.Add(oItem)
                oItem = New ListItem("500", "500")
                cmbMiles.Items.Add(oItem)
                oItem = New ListItem("1000", "1000")
                cmbMiles.Items.Add(oItem)
                oItem = New ListItem("3000", "3000")
                cmbMiles.Items.Add(oItem)
                oItem = New ListItem("5000", "5000")
                cmbMiles.Items.Add(oItem)
                AddExpression()
            End If
        End Sub

        Private Sub AddExpression()
            Dim expression1 As New GridSortExpression
            expression1.FieldName = "Name"
            expression1.SetSortOrder("Ascending")
            grdProxResults.MasterTableView.SortExpressions.AddSortExpression(expression1)
        End Sub


        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(Me.RedirectURL) Then
                Me.grdProxResults.Enabled = False
                Me.grdProxResults.ToolTip = "RedirectURL property has not been set."
            End If

            'if values are not provide directly or from the XML file, set default values for inherited query
            'parameter properties since control requires them to properly function
            If String.IsNullOrEmpty(Me.RedirectIDParameterName) Then Me.RedirectIDParameterName = "ID"

        End Sub

        Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
            ''New Function created by Suvarna D for IssueID 12436 on Dec 1, 2011
            ''To support paging separate grid bind function has been created
            '' LoadProxResults()
            ''End of addition by Suvarna D IssueId 12436
            'Amruta IssueID 14448
            AddExpression()
            grdProxResults.Rebind()
        End Sub

        Protected Sub grdProxResults_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdProxResults.NeedDataSource
            LoadProxResults()
        End Sub

        Protected Sub grdProxResults_PageIndexChanging(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdProxResults.PageIndexChanged
            ''grdProxResults.PageIndex = e.NewPageIndex
            LoadProxResults()
        End Sub
        'Navin Prasad Issue 11032
        'Protected Sub grdProxResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdProxResults.ItemDataBound
        '    Try
        '        If Me.EncryptQueryStringValue Then
        '            Dim type As ListItemType = e.Item.ItemType
        '            If e.Item.ItemType = ListItemType.Item Or _
        '                    e.Item.ItemType = ListItemType.AlternatingItem Then
        '                Dim lnk As HyperLink
        '                Dim tempURL As String
        '                Dim index As Integer
        '                Dim sValue As String = "0"
        '                Dim separator As String()

        '                lnk = CType(e.Item.Cells(0).Controls(0), HyperLink)
        '                tempURL = lnk.NavigateUrl
        '                index = tempURL.IndexOf("=")
        '                sValue = tempURL.Substring(index + 1)
        '                separator = lnk.NavigateUrl.Split(CChar("="))
        '                lnk.NavigateUrl = separator(0)
        '                lnk.NavigateUrl = lnk.NavigateUrl & "="
        '                lnk.NavigateUrl = lnk.NavigateUrl & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
        '            End If
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        'Protected Sub grdProxResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProxResults.RowDataBound
        '    Try
        '        If Me.EncryptQueryStringValue Then
        '            Dim type As ListItemType = CType(e.Row.RowType, ListItemType)
        '            If CType(e.Row.RowType, ListItemType) = ListItemType.Item Or _
        '                    CType(e.Row.RowType, ListItemType) = ListItemType.AlternatingItem Then
        '                Dim lnk As HyperLink
        '                Dim tempURL As String
        '                Dim index As Integer
        '                Dim sValue As String = "0"
        '                Dim separator As String()

        '                lnk = CType(e.Row.Cells(0).Controls(0), HyperLink)
        '                tempURL = lnk.NavigateUrl
        '                index = tempURL.IndexOf("=")
        '                sValue = tempURL.Substring(index + 1)
        '                separator = lnk.NavigateUrl.Split(CChar("="))
        '                lnk.NavigateUrl = separator(0)
        '                lnk.NavigateUrl = lnk.NavigateUrl & "="
        '                lnk.NavigateUrl = lnk.NavigateUrl & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
        '            End If
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub
        ''New Function created by Suvarna D for IssueID 12436 on Dec 1, 2011
        ''To support paging separate grid bind function has been created
        Protected Sub LoadProxResults()
            ' search by name, category and 
            Dim dDistance As Decimal
            Dim sZipCode As String

            Try
                dDistance = CDec(cmbMiles.SelectedItem.Value)
                sZipCode = Trim$(txtZipCode.Text)
                If Len(sZipCode) > 0 Then
                    Dim sSQL As String = Database & "..spGetChapterProximityList"
                    Dim params(2) As IDataParameter
                    Dim dt As DataTable

                    params(0) = Me.DataAction.GetDataParameter("@PersonID", SqlDbType.BigInt, User1.PersonID)
                    params(1) = Me.DataAction.GetDataParameter("@PostalCode", SqlDbType.NVarChar, 20, sZipCode)
                    params(2) = Me.DataAction.GetDataParameter("@Distance", SqlDbType.Decimal, dDistance)
                    dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)

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
                    ' DirectCast(grdProxResults.Columns(0), HyperLinkColumn).DataNavigateUrlFormatString = Me.RedirectURL & "?" & Me.RedirectIDParameterName & "={0}"
                    'Dim hlink As HyperLinkField = CType(grdProxResults.Columns(0), HyperLinkField)
                    'hlink.DataNavigateUrlFormatString = Me.RedirectURL & "?" & Me.RedirectIDParameterName & "={0}"
                    If dt.Rows.Count > 0 Then
                        grdProxResults.DataSource = dt
                        trResults.Visible = True
                        lblError.Visible = False
                    Else
                        lblError.Text = "No records found."
                        lblError.Visible = True
                        trResults.Visible = False
                    End If
                Else
                    trResults.Visible = False
                    lblError.Text = "Please enter a Zip Code"
                    lblError.Visible = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''End of Addition IssueID: 12436
        'Protected Sub grdProxResults_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdProxResults.RowEditing

        'End Sub
    End Class
End Namespace
