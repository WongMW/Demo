Option Explicit On
Imports Aptify.Framework.Web.eBusiness.SocialNetworkIntegration
Imports System.Xml

Namespace Aptify.Framework.Web.eBusiness
    ''' <summary>
    ''' Aptify e-Business Login ASP.NET User Control
    ''' </summary>
    ''' <remarks></remarks>
    Partial Class WebLogin
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_NUM_COLUMNS As String = "TimeForExpiry"
        Protected Const ATTRIBUTE_NEWUSER_PAGE As String = "NewUserPage"
        Protected Const ATTRIBUTE_FORGOTUID_PAGE As String = "ForgotUIDPage"
        Protected Const ATTRIBUTE_MAXLOGINTRIES As String = "maxLoginTries"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "Login"
        Protected Const ATTRIBUTE_USER_DISABLED_ERROR As String = "UserDisabledError" 'SKB Issue#10654: display message indicating disabled account
        Protected Const ATTRIBUTE_HOME_PAGE As String = "HomePage"
        'Neha Issue 14408,01/24/13,declare property for ChangePassword 
        Protected Const ATTRIBUTE_HOME_CHANGEPWD As String = "ChangePassword"
        Private m_iTimeForExpiry As Integer
        'Neha, Issue 14408,03/20/13,declare property for new WebUser 
        Private CheckNewWebUser As Boolean
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not IsPostBack Then

                'Anil B for  Issue 13882 on 18-03-2013
                'Set Remember option for login
                If Request.Browser.Cookies Then
                    'Check if the cookies with name LOGIN exist on user's machine
                    If Request.Cookies("LOGIN") IsNot Nothing AndAlso Request.Cookies("LOGIN").Item("RememberMe") IsNot Nothing Then
                        'Pass the user name and password to the VerifyLogin method
                        chkAutoLogin.Checked = CBool(Request.Cookies("LOGIN").Item("RememberMe"))
                    End If
                End If
                CleareCatche()
            End If

            'Anil B for  Issue 13882 on 18-03-2013
            'Set Remember option for login
            If IsPostBack Then
                If Request.Browser.Cookies Then
                    'Check if the cookie with name LOGIN exist on user's machine
                    'Create a cookie with expiry of 30 days
                    Response.Cookies("LOGIN").Expires = DateTime.Now.AddDays(30)
                    'Write username to the cookie
                    Response.Cookies("LOGIN").Item("RememberMe") = chkAutoLogin.Checked
                End If
            End If
        End Sub
        Private Sub CleareCatche()
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1))
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoStore()
            Response.Buffer = True
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
            Response.Expires = -1500
        End Sub
        Public Overridable Property TimeForExpiry() As Integer
            Get
                Return m_iTimeForExpiry
            End Get
            Set(ByVal value As Integer)
                m_iTimeForExpiry = value
            End Set
        End Property
        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(NewUserPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                NewUserPage = Me.GetLinkValueFromXML(ATTRIBUTE_NEWUSER_PAGE)
                If String.IsNullOrEmpty(NewUserPage) Then
                    Me.cmdNewUser.Enabled = False
                    Me.cmdNewUser.ToolTip = "NewUserPage property has not been set."
                End If

            End If
            If String.IsNullOrEmpty(ForgotUIDPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ForgotUIDPage = Me.GetLinkValueFromXML(ATTRIBUTE_FORGOTUID_PAGE)
                If String.IsNullOrEmpty(ForgotUIDPage) Then
                    Me.hlinkForgotUID.Enabled = False
                    Me.hlinkForgotUID.ToolTip = "ForgotUIDPage property has not been set."
                Else
                    Me.hlinkForgotUID.NavigateUrl = ForgotUIDPage
                End If
            Else
                Me.hlinkForgotUID.NavigateUrl = ForgotUIDPage
            End If
            'Neha Issue 14408,01/24/13,set property for ChangePassword 
            If String.IsNullOrEmpty(ChangePassword) Then
                'since value is the 'default' check the XML file for possible custom setting 
                ChangePassword = Me.GetLinkValueFromXML(ATTRIBUTE_HOME_CHANGEPWD)
                If String.IsNullOrEmpty(ChangePassword) Then
                    Me.cmdLogin.Enabled = False
                    Me.cmdLogin.ToolTip = "ChangePassword property has not been set."
                End If
            End If

            If String.IsNullOrEmpty(HomePage) Then
                'since value is the 'default' check the XML file for possible custom setting
                HomePage = Me.GetLinkValueFromXML(ATTRIBUTE_HOME_PAGE)
            End If

            If TimeForExpiry = 0 Then
                'since value is the 'default' check the XML file for possible custom setting
                If Not String.IsNullOrEmpty(Me.GetPropertyValueFromXML(ATTRIBUTE_NUM_COLUMNS)) Then
                    TimeForExpiry = CInt(Me.GetPropertyValueFromXML(ATTRIBUTE_NUM_COLUMNS))
                End If
            End If
        End Sub

        ''' <summary>
        ''' This event is raised whenever a user logs in
        ''' </summary>
        ''' <remarks></remarks>
        Public Event UserLoggedIn()

        ''' <summary>
        ''' This event is raised whenever a user logs out
        ''' </summary>
        ''' <remarks></remarks>
        Public Event UserLoggedOut()

        ''' <summary>
        ''' ForgotUID page url
        ''' </summary>
        Public Overridable Property ForgotUIDPage() As String
            Get
                If Not ViewState(ATTRIBUTE_FORGOTUID_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_FORGOTUID_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_FORGOTUID_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' NewUser page url
        ''' </summary>
        Public Overridable Property NewUserPage() As String
            Get
                If Not ViewState(ATTRIBUTE_NEWUSER_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_NEWUSER_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_NEWUSER_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' Home page url
        ''' </summary>
        Public Overridable Property HomePage() As String
            Get
                If Not ViewState(ATTRIBUTE_HOME_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_HOME_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_HOME_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        'Neha Issue 14408,01/24/13,added property for ChangePassword,added ChangePassword page url
        Public Overridable Property ChangePassword() As String
            Get
                If Not ViewState(ATTRIBUTE_HOME_CHANGEPWD) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_HOME_CHANGEPWD))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_HOME_CHANGEPWD) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        'Neha, Issue 14408,03/20/13,set property for new webuser  
        Public Property CheckNewUser As Boolean
            Get
                Return CheckNewWebUser
            End Get
            Set(ByVal value As Boolean)
                CheckNewWebUser = value
            End Set
        End Property
        ''' <summary>
        ''' MaxLoginTries
        ''' </summary>
        Public Overridable ReadOnly Property MaxLoginTries() As Integer
            Get
                Try
                    If Not String.IsNullOrEmpty(Me.GetGlobalAttributeValue(ATTRIBUTE_MAXLOGINTRIES)) Then
                        If Not IsNumeric(Me.GetGlobalAttributeValue(ATTRIBUTE_MAXLOGINTRIES)) Then
                            Throw New Exception("Incorrect entry for <Global>...<" & ATTRIBUTE_MAXLOGINTRIES & ">, a numeric value is required. " & _
                                                "Please confirm the entry is correctly input in the 'Aptify_UC_Navigation.config' file.")
                        Else
                            Return CInt(Me.GetGlobalAttributeValue(ATTRIBUTE_MAXLOGINTRIES))
                        End If
                    Else
                        Return 0
                    End If
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    Return 0
                End Try
            End Get
        End Property
        ''' <summary>
        ''' String to show in the title
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.ComponentModel.DefaultValue("Login")> _
        Public Overridable Property TitleString() As String
            Get
                If Not ViewState("TitleString") Is Nothing Then
                    Return CStr(ViewState("TitleString"))
                Else
                    Return "Login"
                End If
            End Get
            Set(ByVal value As String)
                ViewState("TitleString") = value
            End Set
        End Property
        ''' <summary>
        ''' Defaults to true, shows the title of the control or not
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Property ShowTitle() As Boolean
            Get
                If Not ViewState("ShowTitle") Is Nothing Then
                    Return CBool(ViewState("ShowTitle"))
                Else
                    Return True
                End If
            End Get
            Set(ByVal value As Boolean)
                ViewState("ShowTitle") = value
            End Set
        End Property

        'RashmiP, Function clears cookies
        Private Sub ClearCookies()
            Dim i As Integer
            Dim aCookie As HttpCookie

            Dim limit As Integer = Request.Cookies.Count - 1
            For i = 0 To limit
                aCookie = Request.Cookies(i)
                aCookie.Expires = DateTime.Now.AddDays(-1)
                Response.Cookies.Add(aCookie)
            Next

            'Check iIf the cookies with name LOGIN exist on user's machine
            If (Request.Cookies("LOGIN") IsNot Nothing) Then
                'Expire the cookie
                Response.Cookies("LOGIN").Expires = DateTime.Now.AddDays(-TimeForExpiry)
            End If

        End Sub

        Private Sub cmdLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
            ' Login to the Aptify Enterprise Web Architecture
            ' Use the Aptify EWA .NET Login Component to do this
            Dim bLoggedIn As Boolean = False
            Try
                With WebUserLogin1
                    'HP Issue#9078: add MaxLoginTries from config file
                    If .Login(txtUserID.Text, txtPassword.Text, Page.User, MaxLoginTries) Then

                        tblLogin.Visible = False
                        litLoginLabel.Visible = False
                        tblWelcome.Visible = True
                        lblWelcome.Text = "Welcome, " & _
                                          .User.FirstName & " " & _
                                          .User.LastName
                        lblError.Text = ""

                        If chkAutoLogin.Checked Then
                            .AddAutoLoginCookie(Page.Response, txtUserID.Text, txtPassword.Text)
                        Else
                            .ClearAutoLoginCookie(Page.Response)
                        End If

                        ' make sure to persist changes to user, since many
                        ' applications will do a Response.Redirect after
                        ' this event is fired
                        .User.SaveValuesToSessionObject(Page.Session)
                        bLoggedIn = True

                    Else
                        tblLogin.Visible = True
                        litLoginLabel.Visible = True
                        tblWelcome.Visible = False
                        'HP Issue#9078: display message indicating disabled account
                        If .Disabled Then
                            'lblError.Text = "Account has been disabled, please contact the administrator"
                            'SKB Issue#10654: display message indicating disabled account
                            lblError.Text = UserDisabledError
                            hlinkForgotUID.Visible = False
                        Else
                            lblError.Text = "Error logging in"
                        End If
                    End If
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            If bLoggedIn Then
                OnUserLoggedIn()
                'Neha Issue 14408,03/20/13,Added changepassword redirection on firstlogin
                If Session("UserLoggedIn") = True AndAlso CheckFirstLogin() Then
                    'Response.Redirect(ChangePassword, False)
                    CheckNewUser = True
                    Session("CheckNewUser") = True
                Else
                    CheckNewUser = False
                End If
                Response.Redirect(Request.RawUrl)
                Return
            End If
        End Sub
        'Neha Issue 14408,03/20/13,check firstlogin for user by tracking session count value
        Private Function CheckFirstLogin() As Boolean
            Try
                Dim sSql As String = String.Empty
                Dim dtChkFLogin As New System.Data.DataTable
                Dim bChkFLogin As Boolean = False
                sSql = "SELECT SessionCount FROM " & Database & _
                   ".." & AptifyApplication.GetEntityBaseView("Web Users") & " Where ID= " & WebUserLogin1.User.UserID
                dtChkFLogin = DataAction.GetDataTable(sSql)
                If Not dtChkFLogin Is Nothing AndAlso dtChkFLogin.Rows.Count > 0 AndAlso dtChkFLogin.Rows(0)("SessionCount") = "1" Then
                    bChkFLogin = True
                End If
                Return bChkFLogin
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        Protected Sub cmdLogOut_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogOut.Click
            ' Log Out to the Aptify Enterprise Web Architecture
            ' Use the AptifyWebLogin Component to do this
            Dim bLoggedOut As Boolean = False
            Try
                If WebUserLogin1.Logout() Then
                    lblError.Text = ""
                    tblLogin.Visible = True
                    litLoginLabel.Visible = True
                    tblWelcome.Visible = False
                    ShoppingCartLogin.Clear()
                    'Suraj Issue 15370,3/19/13 remove the application session after click on logout button
                    Session.Remove("ReturnToPage")
                    WebUserLogin1.ClearAutoLoginCookie(Page.Response)
                    'HP Issue#9078: clear and delete session
                    Session.Clear()
                    Session.Abandon()
                    bLoggedOut = True
                    'RashmiP, Call Clear Cookies function
                    ClearCookies()
                    Session("SocialNetwork") = Nothing
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

            If bLoggedOut Then
                OnUserLoggedOut()
            End If
        End Sub

        Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
            Try
                MyBase.OnPreRender(e)
                With WebUserLogin1
                    If .User.UserID <= 0 Then
                        tblLogin.Visible = True
                        litLoginLabel.Visible = True
                        tblWelcome.Visible = False
                        chkAutoLogin.Checked = False
                    Else
                        tblLogin.Visible = False
                        litLoginLabel.Visible = False
                        tblWelcome.Visible = True
                        lblWelcome.Text = "Welcome, " & _
                                          .User.FirstName & " " & _
                                          .User.LastName
                    End If
                End With
                'Anil B for  Issue 13882 on 18-03-2013
                'Set Remember option for login             
                If Request.Browser.Cookies Then
                    'Check if the cookies with name LOGIN exist on user's machine
                    If Request.Cookies("LOGIN") IsNot Nothing AndAlso Request.Cookies("LOGIN").Item("RememberMe") IsNot Nothing Then
                        'Pass the user name and password to the VerifyLogin method
                        chkAutoLogin.Checked = CBool(Request.Cookies("LOGIN").Item("RememberMe"))
                    End If
                End If
                'lblLogin.Visible = Me.ShowTitle
                'If lblLogin.Visible Then
                '    lblLogin.InnerText = Me.TitleString
                'End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub cmdNewUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewUser.Click

            Try
                Session("SocialNetwork") = Nothing
                Response.Redirect(NewUserPage, False)
                Return
            Catch ex As Exception

            End Try

        End Sub

        Protected Overridable Sub OnUserLoggedIn()

            Dim sRedirectLocation As String = ""

            Try
                RaiseEvent UserLoggedIn()
                'Sapna DJ Issue 12545
                Session("UserLoggedIn") = True
                'Suraj S Issue 15370, 3/29/13 change session variale to application variable
                If Len(Session("ReturnToPage")) <> 0 Then
                    Dim sTemp As String
                    sTemp = CStr((Session("ReturnToPage")))
                    Session("ReturnToPage") = "" ' used only once
                    sRedirectLocation = sTemp
                    Response.Redirect(sTemp)
                    Return
                Else
                    ' refresh current page
                    sRedirectLocation = Request.RawUrl
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

            'Response.Redirect(sRedirectLocation)
        End Sub

        Protected Overridable Sub OnUserLoggedOut()
            RaiseEvent UserLoggedOut()
            'Sapna DJ Issue 12545
            If Not Session Is Nothing Then
                Session("UserLoggedIn") = False
            End If
            Response.Redirect(HomePage)
            Return
        End Sub
        'SKB Issue#10654: display message indicating disabled account
        ''' <summary>
        '''Error displayed if user is disabled
        ''' </summary>
        Public Overridable ReadOnly Property UserDisabledError() As String
            Get
                If Not Session.Item(ATTRIBUTE_USER_DISABLED_ERROR) Is Nothing Then
                    Return CStr(Session.Item(ATTRIBUTE_USER_DISABLED_ERROR))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_USER_DISABLED_ERROR)
                    If Not String.IsNullOrEmpty(value) Then
                        Session.Item(ATTRIBUTE_USER_DISABLED_ERROR) = value
                        Return value
                    Else
                        Return String.Empty
                    End If
                End If
            End Get
        End Property
        Protected Sub chkAutoLogin_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAutoLogin.CheckedChanged
            'Anil B for  Issue 13882 on 18-03-2013
            'Set Remember option for login
            If Request.Browser.Cookies Then
                'Check if the cookie with name LOGIN exist on user's machine
                'Create a cookie with expiry of 30 days
                Response.Cookies("LOGIN").Expires = DateTime.Now.AddDays(30)
                'Write username to the cookie
                Response.Cookies("LOGIN").Item("RememberMe") = chkAutoLogin.Checked
            End If
        End Sub
    End Class
End Namespace
