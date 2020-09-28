'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Namrata Nimkar                 26/10/2015                         create LLL Exemption 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Applications.OrderEntry

Namespace Aptify.Framework.Web.eBusiness
    Partial Class LLLPostEnrolmentRequest__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "LLLPostEnrolmentRequest__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Protected Const ATTRIBUTE_CONTROL_BACKURL_NAME As String = "BackUrl"
        Protected Const ATTRIBUTE_CONTROL_EROLL_NAME As String = "Enroll"
        Protected Const ATTRIBUTE_SECURITYERROR_PAGE As String = "securityErrorPage"
        Dim ClassID As Integer
        Dim iVenueID As Integer

#Region "Page Load"

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    
        End Sub

        Public Overloads Property RedirectURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

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

        Public Overloads Property RedirectBackURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_BACKURL_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_BACKURL_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_BACKURL_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        Public Overloads Property EnrollURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_EROLL_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_EROLL_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_EROLL_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try

                SetProperties()
                If AptifyEbusinessUser1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If


                If Not IsPostBack Then

                    If Not Request.QueryString("ClassID") Is Nothing Then
                        pnlDetails.Visible = True

                        Dim ClassID As Long = CLng(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ClassID")))
                        LoadHeaderText()
                        LoadTypes()


                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Overrides Sub SetProperties()
            Try

                Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
                MyBase.SetProperties()
                If String.IsNullOrEmpty(RedirectURL) Then
                    Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
                End If
                If String.IsNullOrEmpty(RedirectBackURL) Then
                    Me.RedirectBackURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_BACKURL_NAME)
                End If
                If String.IsNullOrEmpty(LoginPage) Then
                    LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
                End If
                If String.IsNullOrEmpty(EnrollURL) Then
                    Me.EnrollURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_EROLL_NAME)
                End If



            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


#End Region

#Region "Private Functions"
        Private Sub LoadHeaderText()
            Try

                Dim sSqlPerson As String = Database & "..spGetPersonMemberType__c @PersonID=" & AptifyEbusinessUser1.PersonID
                Dim dtPerson As DataTable = DataAction.GetDataTable(sSqlPerson, IAptifyDataAction.DSLCacheSetting.BypassCache)
                lblStudentno.Text = dtPerson.Rows(0)("oldid").ToString()
                lblStudentName.Text = AptifyEbusinessUser1.FirstName + " " + AptifyEbusinessUser1.LastName
                lblStudentID.Text = Convert.ToString(dtPerson.Rows(0)("ID"))

                Dim sSqlCourse As String = Database & "..spGetLLLCloursefromClass__c @ClassID=" & CLng(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ClassID")))
                Dim dtCourse As DataTable = DataAction.GetDataTable(sSqlCourse, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtCourse Is Nothing AndAlso dtCourse.Rows.Count > 0 Then

                    lblQualificationID.Text = dtCourse.Rows(0)("CourseID").ToString()
                    lblQualification.Text = dtCourse.Rows(0)("Name").ToString()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Private Function DoSave() As Boolean
            Try



                Dim oLLLApplicationGE As AptifyGenericEntityBase
                oLLLApplicationGE = AptifyApplication.GetEntityObject("LLLRequestApplications__c", -1)
                oLLLApplicationGE.SetValue("StudentID", Convert.ToInt16(lblStudentID.Text))
                oLLLApplicationGE.SetValue("Qualification", Convert.ToInt16(lblQualificationID.Text))
                oLLLApplicationGE.SetValue("Comment", txtcomment.Text)
                oLLLApplicationGE.SetValue("TypeID", ddlType.SelectedValue)
                oLLLApplicationGE.SetValue("RequestDate", Today.Date)
                oLLLApplicationGE.SetValue("Status", "Submitted to CAI status")
                oLLLApplicationGE.SetValue("Status", "Submitted to CAI status")
                oLLLApplicationGE.SetValue("Accountancybody", 0)

                Dim sError As String = String.Empty
                oLLLApplicationGE.Save(False, sError)





                Return True


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        Private Function Validation() As Boolean
            Try

                lbltypecomp.Visible = False
                lblsubmit.Visible = False
                If (ddlType.SelectedIndex = 0) Then
                    lbltypecomp.Visible = True
                    lbltypecomp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLPostEnrollment.ValidateType__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Return False
                Else
                    lbltypecomp.Visible = False
                End If


                Dim sSql As String = Database & "..spCheckPersonIsRegisterForLLLReqApp__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@TypeID=" & ddlType.SelectedValue & ",@IsCTCRequest=" & 0
                Dim dt As DataTable = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If (dt.Rows.Count > 0) Then
                    If (Convert.ToBoolean(dt.Rows(0)(0)) = True) Then
                        lblsubmit.Visible = True
                        lblsubmit.Visible = True

                        lblsubmit.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLPostEnrollment.DuplicateLLLRequest__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        Return False
                    Else
                        lblsubmit.Visible = False
                        'Return True
                    End If
                End If
                Return True

            Catch ex As Exception
                Return False
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
#End Region

#Region "Load Drop Downs"

        Private Sub LoadTypes()
            Try
                Dim sSql As String = Database & "..spGetLLLRequestTypeForCTCRequestExemption__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ddlType.DataSource = dt
                    ddlType.DataTextField = "Name"
                    ddlType.DataValueField = "ID"
                    ddlType.DataBind()


                End If
                ddlType.Items.Insert(0, "Select")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        

#End Region

#Region "Events"


      

        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                If (Validation() = True) Then
                    DoSave()
                    RadAlert.VisibleOnPageLoad = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

      
        Protected Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
            Try
                Response.Redirect(RedirectURL)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Response.Redirect(RedirectBackURL & "?ClassID=" & Request.QueryString("ClassID"))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region

       
    End Class
End Namespace
