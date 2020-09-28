Option Explicit On
Option Strict On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.Security
Imports System.Data
Imports System.IO
Imports Aptify.Framework.Web.eBusiness.SocialNetworkIntegration
Imports Aptify.Framework.BusinessLogic.ProcessPipeline
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports System.Linq
Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class PersonCompanyInformation__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_VIEWCART_PAGE As String = "ViewCartPage"
        Protected Const ATTRIBUTE_Login_PAGE As String = "LoginPageURL"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "PersonOtherMemberships__c"


#Region "Properties and Methods"
        ''' <summary>
        ''' This is the exposed DropDownList for this control.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Overridable Property LoginPageURL() As String
            Get
                If Not ViewState(ATTRIBUTE_Login_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_Login_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_Login_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Overridable Property ViewCartPage() As String
            Get
                If Not ViewState(ATTRIBUTE_VIEWCART_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_VIEWCART_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_VIEWCART_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        
        Public Property PersonCompanyTable() As DataTable
            Get
                If Not ViewState("tblPCompany") Is Nothing Then
                    Return CType(ViewState("tblPCompany"), DataTable)
                Else
                    Dim tblPCompany As New DataTable("tblPCompany")
                    With tblPCompany
                        .Columns.Add("ID", System.Type.GetType("System.String"))
                        .PrimaryKey = New DataColumn() {.Columns("ID")}
                        .Columns.Add("CompanyID", System.Type.GetType("System.String"))
                        .Columns.Add("CompanyName", System.Type.GetType("System.String"))
                        .Columns.Add("JobTitle", System.Type.GetType("System.String"))
                        .Columns.Add("EntID", System.Type.GetType("System.String"))
                    End With
                    Return tblPCompany
                End If
            End Get
            Set(ByVal value As DataTable)
                ViewState("tblPCompany") = value
            End Set
        End Property
#End Region

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
            If String.IsNullOrEmpty(ViewCartPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ViewCartPage = Me.GetLinkValueFromXML(ATTRIBUTE_VIEWCART_PAGE)
            End If
            If String.IsNullOrEmpty(LoginPageURL) Then
                'since value is the 'default' check the XML file for possible custom setting
                LoginPageURL = Me.GetLinkValueFromXML(ATTRIBUTE_Login_PAGE)
            End If
        End Sub

        Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
            'Dim htmllink As HtmlLink = New HtmlLink()
            'htmllink.Href = ResolveUrl("~/include/AptifyCustom__c.css")
            'htmllink.Attributes.Add("type", "text/css")
            'htmllink.Attributes.Add("rel", "stylesheet")
            'Page.Header.Controls.Add(htmllink)

            'Dim js As HtmlGenericControl = New HtmlGenericControl("script")
            'js.Attributes.Add("type", "text/javascript")
            'js.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-1.7.1.min.js"))
            'Me.Page.Header.Controls.Add(js)
        End Sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                SetProperties()
                PopulateDropDowns()

                LoadPersonCompanyDetails(CInt(User1.PersonID))
                'If Me.Request.Form("__EVENTTARGET") = "btnAdd" Then
                '    btnAdd_Click(Me, New EventArgs())
                'End If
            End If
        End Sub
        Private Sub ClearData()
            txtCompany11.Text = String.Empty
            cmbJobTitle.SelectedIndex = 0
        End Sub

        Public Sub LoadPersonCompanyDetails(ByVal PersonID As Integer)
            Dim strUserID As String = String.Empty
            Dim sSQL As String = String.Empty
            Dim dtPersonCompany As DataTable = Nothing

            Dim param(0) As IDataParameter
            Try
                PersonCompanyTable = Nothing
                sSQL = Database & "..spGetPersonCompanyInformation__c"

                param(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.BigInt, PersonID)
                dtPersonCompany = DataAction.GetDataTableParametrized(sSQL, Data.CommandType.StoredProcedure, param)
                Dim dt As DataTable = PersonCompanyTable
                For Each drPersonEducation As DataRow In dtPersonCompany.Rows
                    Dim dr As DataRow = dt.NewRow
                    With dr
                        .Item("ID") = Guid.NewGuid.ToString()
                        .Item("CompanyID") = drPersonEducation.Item("CompnayID")
                        .Item("CompnayName") = drPersonEducation.Item("CompnayName")

                        .Item("JobTitle") = drPersonEducation.Item("Title")
                        .Item("EntID") = drPersonEducation.Item("ID")
                        dt.Rows.Add(dr)
                    End With
                Next

                grvCompany.DataSource = dt
                grvCompany.DataBind()
                PersonCompanyTable = dt
                ClearData()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

            End Try
        End Sub


        Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            Try
                If ValidateInputes() = False Then
                    Exit Sub
                Else

                    'lblMessage.Text = ""
                    'Dim dt As DataTable = PersonCompanyTable
                    'Dim dr As DataRow = dt.NewRow
                    'With dr
                    '    .Item("ID") = Guid.NewGuid.ToString()
                    '    .Item("CompanyID") = hfCompanyID.Value
                    '    .Item("CompanyName") = txtCompany11.Text
                    '    .Item("JobTitle") = cmbJobTitle.SelectedItem.Text
                    '    .Item("EntID") = -1
                    'End With
                    'dt.Rows.Add(dr)
                    'grvCompany.DataSource = dt
                    'grvCompany.DataBind()
                    'PersonCompanyTable = dt
                    Dim oPerson As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = Nothing
                    oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)


                    With oPerson.SubTypes("PersonCompanies").Add()
                        If Convert.ToInt16(hfCompanyID.Value) > 0 Then
                            .SetValue("CompanyId", hfCompanyID)
                            .SetValue("Title", cmbJobTitle.SelectedItem.Text)
                        Else
                            Dim oCompany As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = Nothing
                            oCompany = AptifyApplication.GetEntityObject("Companies", -1)

                            .SetValue("CompanyId", hfCompanyID)
                            .SetValue("Title", cmbJobTitle.SelectedItem.Text)
                        End If

                    End With
                    oPerson.Save(False)

                    ClearData()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub grvCompany_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvCompany.RowCommand
            Try
                If e.CommandName = "Delete" Then
                    Dim sID As String = Convert.ToString(e.CommandArgument)
                    Dim oPerson As AptifyGenericEntityBase
                    Dim oPMembershipGE As AptifyGenericEntityBase
                    If User1.PersonID > 0 Then
                        oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                        If PersonCompanyTable IsNot Nothing AndAlso Not PersonCompanyTable.Rows.Find(sID) Is Nothing Then
                            oPMembershipGE = oPerson.SubTypes("ColleagueInformation__c").Find("ID", PersonCompanyTable.Rows.Find(sID)("EntID"))
                            If oPMembershipGE IsNot Nothing Then
                                oPMembershipGE.Delete()
                                oPerson.Save()
                            End If
                            PersonCompanyTable.Rows.Find(sID).Delete()
                        End If
                        grvCompany.DataSource = PersonCompanyTable
                        grvCompany.DataBind()
                    End If

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Private Sub PopulateDropDowns()
            Dim sSQL As String
            Dim dt As DataTable
            ' Dim dtempstatus As DataTable
            Try

                sSQL = AptifyApplication.GetEntityBaseDatabase("Person") & "..spGetPersonActiveJobTitle__c"

                cmbJobTitle.DataSource = DataAction.GetDataTable(sSQL)
                cmbJobTitle.DataTextField = "Name"
                cmbJobTitle.DataValueField = "Name"
                cmbJobTitle.DataBind()

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Function ValidateInputes() As Boolean
            'Dim rtn As Boolean = True
            Dim errmsg As String = "The Following Field(s) are Required: "
            If txtCompany11.Text = "" Then
                errmsg = errmsg & "Company Name,"
            End If

            If cmbJobTitle.SelectedItem.Text = "Select job Title" Then
                errmsg = errmsg & " Job title,"

            End If
            errmsg = errmsg.TrimEnd(CChar(","))
            If errmsg <> "The Following Field(s) are Required: " Then
                lblMessage.Text = errmsg
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "OnMyButtonClick();", True)
                Return False
            Else
                Return True
            End If

        End Function
        Protected Sub grvCompany_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grvCompany.RowDeleting

        End Sub
        Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            hfCompanyID.Value = "-1"
            txtCompany11.Text = txtAddNew.Text
            txtAddNew.Text = String.Empty
            RadAddNew.VisibleOnPageLoad = False
        End Sub

        Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            RadAddNew.VisibleOnPageLoad = False
            txtCompany11.Text = ""
        End Sub


    End Class
End Namespace
