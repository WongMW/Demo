'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Pradip Chavhan               09/07/2015                        Mentor Request Application
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Namespace Aptify.Framework.Web.eBusiness
    Partial Class MentorRequestApplication__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_SECURITYERROR_PAGE As String = "securityErrorPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MentorAppTermsPage As String = "MentorAppTermsPage"
        Dim sErrorMessage As String = String.Empty
        Dim sSQL As String = String.Empty
#Region "Property Setting"
        Public Overridable Property LoginPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        Public Overridable Property MentorAppTermsPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorAppTermsPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorAppTermsPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorAppTermsPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(MentorAppTermsPage) Then
                MentorAppTermsPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MentorAppTermsPage)
            End If
        End Sub

        Public Overridable ReadOnly Property SecurityErrorPage() As String
            Get
                If Not ViewState(ATTRIBUTE_SECURITYERROR_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_SECURITYERROR_PAGE))
                Else
                    Dim value As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("SecurityErrorPageURL"))
                    If Not String.IsNullOrEmpty(value) Then
                        ViewState(ATTRIBUTE_SECURITYERROR_PAGE) = value
                        Return value
                    Else
                        Return String.Empty
                    End If
                End If
            End Get
        End Property
#End Region

#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()

                hlTermsandconditions.Attributes.Add("onClick", "javascript:window.open('" + MentorAppTermsPage + "');return false;")

                If User1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                Else
                    If CheckAccessToPage() = False Then
                        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("virtualdir") & SecurityErrorPage & "?Message=Access to this Person is unauthorized.")
                    End If
                End If
                If Not IsPostBack Then
                    fillProfessionalStatus()
                    LoadYear()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
#Region "Public Function"
        Public Function SaveRequestForMentor() As Boolean
            Try
                Dim sErrorMessage As String = String.Empty
                Dim oDiaryGE As AptifyGenericEntityBase
                oDiaryGE = Me.AptifyApplication.GetEntityObject("MentorRequestApplication__c", -1)
                With oDiaryGE
                    .SetValue("Person", Convert.ToInt32(User1.PersonID))
                    .SetValue("MembershipNumber", txtMemberNumber.Text.Trim())
                    .SetValue("QualificationYear", ddlYear.SelectedValue.Trim())
                    .SetValue("ProfessionalStatusID", ddlProfessionalStatus.SelectedValue)
                    .SetValue("Status", "Pending")
                    .SetValue("Student", Convert.ToInt32(hidStudentID.Value))
                End With
                If oDiaryGE.Save(False, sErrorMessage) Then
                    Return True
                End If
                Return False
            Catch ex As System.Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
            Return True
        End Function

        Public Function Validate() As Boolean
            Try
                If CInt(hidStudentID.Value) <= 0 Then
                    radWindowValidation.VisibleOnPageLoad = True
                    lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorApp.NoStudentMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Return False
                End If
                If chkTerms.Checked = False Then
                    radWindowValidation.VisibleOnPageLoad = True
                    lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorApp.AcceptTermsAndCondition__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    '"Please Check Terms & Conditions"
                    Return False
                End If
                Return True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Method To Fill Professional Status Dropdown
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub fillProfessionalStatus()
            Try
                sSQL = String.Empty
                sSQL = Database & "..spGetAllProfessionalStatus__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                ddlProfessionalStatus.DataSource = dt
                ddlProfessionalStatus.DataTextField = "Name"
                ddlProfessionalStatus.DataValueField = "ID"
                ddlProfessionalStatus.DataBind()
                ddlProfessionalStatus.Items.Insert(0, "Select")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Method To Get Student Name as per OldID Enter by mentor
        ''' </summary>
        ''' <remarks></remarks>
        Private Function GetStudentName() As String
            Try
                sSQL = String.Empty
                sSQL = Database & "..spGetStudentName__c @StudentOldId='" & txtStudentNumber.Text.Trim() & "'"
                Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        hidStudentID.Value = Convert.ToString(dt(0)("ID"))
                        Return Convert.ToString(dt(0)("Name"))
                    End If
                End If
                Return ""
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return ""
            End Try
        End Function

        ''' <summary>
        ''' Method To Get 50 years in Year Dropdown
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadYear()
            Try
                Dim year As Integer = Now.Year - 50
                ddlYear.Items.Clear()
                For i As Integer = 0 To 50
                    Dim newyear As Integer = year + i
                    ddlYear.Items.Add(New WebControls.ListItem(Convert.ToString(newyear)))
                Next
                ddlYear.Items.Insert(0, "Select")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Adde by Pradip 2015-09-02 to check access for Mentor Application Request 
        Private Function CheckAccessToPage() As Boolean
            Try
                Dim sSQL As String = Database & "..spCheckIsMentor__c @PersonId=" & User1.PersonID
                Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                If lID > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

#End Region

#Region "ButtonEvent"
        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                If Validate() Then
                    If SaveRequestForMentor() Then
                        ddlProfessionalStatus.SelectedIndex = 0
                        ddlYear.SelectedIndex = 0
                        txtMemberNumber.Text = ""
                        txtStudentNumber.Text = ""
                        chkTerms.Checked = False
                        lblStudentName.Text = ""
                        radWindowValidation.VisibleOnPageLoad = True
                        lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorApp.SaveMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub txtStudentNumber_TextChanged(sender As Object, e As System.EventArgs) Handles txtStudentNumber.TextChanged
            Try
                lblStudentName.Text = ""
                hidStudentID.Value = "0"
                If txtStudentNumber.Text.Trim <> "" Then
                    Dim studentName As String = GetStudentName().Trim()
                    If studentName <> "" Then
                        lblStudentName.Text = studentName
                    Else
                        lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorApp.NoStudentMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        radWindowValidation.VisibleOnPageLoad = True
                        txtStudentNumber.Text = ""
                    End If
                Else
                    lblStudentName.Text = ""
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Protected Sub hlTermsandconditions_Click(sender As Object, e As System.EventArgs) Handles hlTermsandconditions.Click
        '    Try
        '        lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorApp.TermsAndCondition__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        '        radWindowValidation.VisibleOnPageLoad = True
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub
        Protected Sub btnOK_Click(sender As Object, e As System.EventArgs) Handles btnOK.Click
            radWindowValidation.VisibleOnPageLoad = False
        End Sub
#End Region

    End Class
End Namespace
