'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande               01/10/2014                      Create ShowHideMenu function
'Milind Sutar               03/06/2014                      Login on behalf - Auto login through smart client 
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Imports Aptify.Framework.Web.eBusiness.SocialNetworkIntegration
Imports System.Xml
Imports Aptify.Framework.Integration
Imports Aptify.Framework.BusinessLogic
Imports System.Reflection

Namespace Aptify.Framework.Web.eBusiness
    ''' <summary>
    ''' Aptify e-Business Login ASP.NET User Control
    ''' </summary>
    ''' <remarks></remarks>
    Partial Class WebLogin
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_NEWUSER_PAGE As String = "NewUserPage"
        Protected Const ATTRIBUTE_FORGOTUID_PAGE As String = "ForgotUIDPage"
        Protected Const ATTRIBUTE_COURSE_ENROLLMENT_PAGE As String = "CourseEnrollment"
        Protected Const ATTRIBUTE_MAXLOGINTRIES As String = "maxLoginTries"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "Login"
        Protected Const ATTRIBUTE_HOME_CHANGEPWD As String = "ChangePassword"
        'Suraj Issue 14861, 5/8/13, declare property for get a number of days from the nav file for cookies expire
        Protected Const ATTRIBUTE_NUM_COLUMNS As String = "TimeForExpiry"
''Added BY Pradip
        ''Added BY Pradip
        Protected Const ATTRIBUTE_CustDefault_PAGE As String = "CustDefault"
        Protected Const ATTRIBUTE_StudCenter_PAGE As String = "StudentCenter"
        'Milind Sutar - Login on behalf
        Private m_iUserId As Integer = 0
        Private m_sEmployeeId As String = String.Empty
        Private m_sToken As String = String.Empty
        Private m_bIsOnBehalf As Boolean = False

        Private m_iTimeForExpiry As Integer
        'Neha, Issue 14408,03/20/13,declare property for new WebUser 
        Private CheckNewWebUser As Boolean

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            Dim CheckSessionValue As Boolean
            If Not IsPostBack Then
                'Anil B for  Issue 13882 on 18-03-2013
                'Set Remember option for login
                If Request.Browser.Cookies Then
                    'Check if the cookies with name LOGIN exist on user's machine
                    If Request.Cookies("LOGIN") IsNot Nothing AndAlso Request.Cookies("LOGIN").Item("RememberMe") IsNot Nothing Then
                        chkAutoLogin.Checked = CBool(Request.Cookies("LOGIN").Item("RememberMe"))
                    End If
                End If
                CleareCatche()
            End If
            If Session("CheckNewUser") IsNot Nothing Then
                CheckSessionValue = Convert.ToBoolean(Session("CheckNewUser"))
                If CheckSessionValue Then
                    Session.Remove("CheckNewUser")
                    Response.Redirect(ChangePassword)
                End If
            End If

            If IsPostBack Then
                If Request.Browser.Cookies Then
                    'Check if the cookie with name LOGIN exist on user's machine
                    'If (Request.Cookies("LOGIN") Is Nothing) Then
                    'Create a cookie with expiry of 30 days
                    Response.Cookies("LOGIN").Expires = DateTime.Now.AddDays(30)
                    'Write username to the cookie
                    Response.Cookies("LOGIN").Item("RememberMe") = chkAutoLogin.Checked
                End If

            End If
            If Not IsPostBack Then
                LoginOnBehalf()
            End If
        End Sub

        ''' <summary>
        ''' Auto login - Get the Staff user id from query string 
        ''' </summary>
        Public Property Token() As String
            Get
                If Not String.IsNullOrEmpty(CStr(Request.QueryString("OT"))) Then
                    m_sToken = CStr(Request.QueryString("OT"))
                End If
                Return m_sToken
            End Get
            Set(ByVal value As String)
                m_sToken = value
            End Set
        End Property


        ''' <summary>
        ''' Auto login - Get the Staff user id from query string 
        ''' </summary>
        Public Property EmployeeID() As String
            Get
                If Not String.IsNullOrEmpty(CStr(Request.QueryString("EId"))) Then
                    m_sEmployeeId = CInt(Request.QueryString("EId"))
                End If
                Return m_sEmployeeId
            End Get
            Set(ByVal value As String)
                m_sEmployeeId = value
            End Set
        End Property


        ''' <summary>
        ''' Auto login - Gets user id from query string 
        ''' </summary>
        Public Property WebUserID() As Integer
            Get
                If Not String.IsNullOrEmpty(CStr(Request.QueryString("Uid"))) Then
                    m_iUserId = CInt(Request.QueryString("Uid"))
                End If
                Return m_iUserId
            End Get
            Set(ByVal value As Integer)
                m_iUserId = value
            End Set
        End Property

        Private Sub CleareCatche()
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1))
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoStore()
            Response.Buffer = True
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
            Response.Expires = -1500

        End Sub
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


            If String.IsNullOrEmpty(ChangePassword) Then
                'since value is the 'default' check the XML file for possible custom setting
                ChangePassword = Me.GetLinkValueFromXML(ATTRIBUTE_HOME_CHANGEPWD)

            End If
            'Suraj Issue 14861, if TimeForExpiry value is 0 then take a value from nav file.
            If TimeForExpiry = 0 Then
                'since value is the 'default' check the XML file for possible custom setting
                If Not String.IsNullOrEmpty(Me.GetPropertyValueFromXML(ATTRIBUTE_NUM_COLUMNS)) Then
                    TimeForExpiry = CInt(Me.GetPropertyValueFromXML(ATTRIBUTE_NUM_COLUMNS))
                End If
            End If


            If String.IsNullOrEmpty(CourseEnrollment) Then
                'since value is the 'default' check the XML file for possible custom setting
                CourseEnrollment = Me.GetLinkValueFromXML(ATTRIBUTE_COURSE_ENROLLMENT_PAGE)
                If String.IsNullOrEmpty(CourseEnrollment) Then
                    Me.cmdNewUser.ToolTip = "CourseEnrollment property has not been set."
                End If

            End If

  If String.IsNullOrEmpty(CustDefault) Then
                CustDefault = Me.GetLinkValueFromXML(ATTRIBUTE_CustDefault_PAGE)
            End If
            If String.IsNullOrEmpty(StudentCenterPage) Then
                StudentCenterPage = Me.GetLinkValueFromXML(ATTRIBUTE_StudCenter_PAGE)
            End If
        End Sub
        'Suraj Issue 14861, 5/8/13, set and get the value of property 
        Public Overridable Property TimeForExpiry() As Integer
            Get
                Return m_iTimeForExpiry
            End Get
            Set(ByVal value As Integer)
                m_iTimeForExpiry = value
            End Set
        End Property
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
        ''' ForgotUID page url
        ''' </summary>
        Public Overridable Property CourseEnrollment() As String
            Get
                If Not ViewState(ATTRIBUTE_COURSE_ENROLLMENT_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_COURSE_ENROLLMENT_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_COURSE_ENROLLMENT_PAGE) = Me.FixLinkForVirtualPath(value)
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
  Public Overridable Property CustDefault() As String
            Get
                If Not ViewState(ATTRIBUTE_CustDefault_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CustDefault_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CustDefault_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        Public Overridable Property StudentCenterPage() As String
            Get
                If Not ViewState(ATTRIBUTE_StudCenter_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_StudCenter_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_StudCenter_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        'Neha, Issue 14408,03/20/13,set property for new webuser firstlogin 
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
            'Check If the cookies with name LOGIN exist on user's machine
            If (Request.Cookies("LOGIN") IsNot Nothing) Then
                'Suraj Issue 14861, 5/8/13,  remove hard coded days from AddDays and add the dynamics days which is set from nav file
                'Expire the cookie
                Response.Cookies("LOGIN").Expires = DateTime.Now.AddDays(-TimeForExpiry)
            End If
        End Sub

        ''' <summary>
        ''' To auto login through smart client
        ''' </summary>
        Private Sub LoginOnBehalf()
            Try
                If Me.WebUserID <> 0 Then
                    Dim userID As String = String.Empty
                    Dim password As String = String.Empty
                    Dim link As String = String.Empty
                    Dim errorMessage As String = String.Empty
                    Dim token As String = String.Empty
                    Dim result As Boolean = False
                    Dim sql As New StringBuilder()


                    sql.AppendFormat("{0}..spGetWebUserDetails__c @WebUserID={1}", Me.Database, Me.WebUserID)
                    Dim dataTable As System.Data.DataTable = Me.DataAction.GetDataTable(sql.ToString(), DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If dataTable IsNot Nothing Or dataTable.Rows.Count > 0 Then
                        token = dataTable.Rows.Item(0).Item("Token").ToString()
                        If token = Me.Token Then

                            'Clear security token - For one time URL access
                            Dim webUser As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = _
                                Me.AptifyApplication.GetEntityObject("Web Users", Me.WebUserID)
                            webUser.SetValue("Token__c", String.Empty)
                            result = webUser.Save(errorMessage)
                            If result = True Then
                                userID = dataTable.Rows.Item(0).Item("UserID").ToString()
                                password = DecryptPassword(dataTable.Rows.Item(0).Item("PWD").ToString())
                                link = dataTable.Rows.Item(0).Item("Link").ToString()

                                'Set the corse enrollment url with employee id as query string
                                Dim url As New StringBuilder()
                                url.AppendFormat("{0}?Eid={1}", link, Me.EmployeeID)
                                Me.CourseEnrollment = url.ToString()

                                m_bIsOnBehalf = True
                                chkAutoLogin.Checked = True

                                'Login explicitly by calling login button click
                                txtUserID.Text = userID
                                txtPassword.Text = password
                                Me.cmdLogin_Click(Me.cmdLogin, Nothing)
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Decrypt password
        ''' </summary>
        Private Function DecryptPassword(ByVal sPassword As String) As String
            Try
                Dim oSecurityKey As New Aptify.Framework.BusinessLogic.Security.AptifySecurityKey(Me.DataAction.UserCredentials)
                Dim password As String
                password = oSecurityKey.DecryptData("E Business Login Key", sPassword.Replace(CChar(" "), CChar("+")))
                Return password
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return Nothing
            End Try
        End Function

        Private Sub cmdLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
            ' Login to the Aptify Enterprise Web Architecture
            ' Use the Aptify EWA .NET Login Component to do this
            Dim bLoggedIn As Boolean = False
            Try
                Session("OffSetVal") = clientOffSet.Value
                With WebUserLogin1
                    'HP Issue#9078: add MaxLoginTries from config file
                    If .Login(txtUserID.Text, txtPassword.Text, Page.User, MaxLoginTries) Then

                        tblLogin.Visible = False
                        litLoginLabel.Visible = False
                        tblWelcome.Visible = True
                        lblWelcome.Text = "Welcome, " & _
                                          .User.FirstName & " " & _
                                          .User.LastName
                        lblError.Text = String.Empty

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
                        'SKB Issue 12066 call SSO for Sitefinity
                        Sitefinity4xSSO1.Sitefinity40SSO(txtUserID.Text, txtPassword.Text)
                    Else
                        tblLogin.Visible = True
                        litLoginLabel.Visible = True
                        tblWelcome.Visible = False
                        'HP Issue#9078: display message indicating disabled account
                        If .Disabled Then
                            lblError.Text = "Your account has been locked due to too many failed logins. Please contact webmaster for assistance."
                            hlinkForgotUID.Visible = False
                        Else
                            lblError.Text = "Error logging in"
                        End If
                    End If
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            If m_bIsOnBehalf = True Then
                Session("ReturnToPage") = CourseEnrollment
            End If

            'Neha, Issue 14408,03/20/13, Check user loggedin first time and redirect to changepassword page if firstlogin true
            If bLoggedIn Then
                'Code Added by Govind 1/10/2013
                ShowHideMenu()
                'Code End by Govind 1/10/2013
                OnUserLoggedIn()
                If Session("UserLoggedIn") = True AndAlso CheckFirstLogin() Then
                    CheckNewUser = True
                    Session("CheckNewUser") = True
                Else
                    CheckNewUser = False
                End If

                ''Added BY Pradip 2017-01-18 for Red mine issue https://redmine.softwaredesign.ie/issues/16160
                Dim sSQlStudent As String = Database & "..spGetPersonMemberTypeName__c @PersonID=" & WebUserLogin1.User.PersonID
                Dim sStudent As String = Convert.ToString(DataAction.ExecuteScalar(sSQlStudent)).Trim

                If Request.RawUrl.Contains("SecurityError.aspx") Then
                    If sStudent.ToUpper = "STUDENT" Then
                        Response.Redirect(StudentCenterPage)
                    Else
                        Response.Redirect(CustDefault)
                    End If
                End If

                If m_bIsOnBehalf = False Then
                    Response.Redirect(Request.RawUrl)
                End If
            End If
        End Sub
        'Code Started Govind
        ''' <summary>
        ''' Membership app web menu only available who are eligible
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ShowHideMenu()
            Try
                Dim lWebMenuID As Long
                Dim oWebMenu As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                Dim oWebUser As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                oWebUser = AptifyApplication.GetEntityObject("Web Users", WebUserLogin1.User.UserID)
                Dim sSqlWebMenu As String = AptifyApplication.GetEntityBaseDatabase("Web Menus") & "..spGetWebMenuID__c @Name='Membership Application'"
                lWebMenuID = Convert.ToInt32(DataAction.ExecuteScalar(sSqlWebMenu))


                Dim sWebGroupSQL As String = AptifyApplication.GetEntityBaseDatabase("Web Menus") & "..spGetWebGroup__c"
                Dim lWebGroupID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sWebGroupSQL))

                Dim sSQLPersonEligible As String = "..spGetPersonEligibleForMembership__c @PersonID=" & WebUserLogin1.User.PersonID
                Dim sPersonID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQLPersonEligible))

                If sPersonID > 0 Then
                    If CheckAlreadyFillupMembershipApplication(WebUserLogin1.User.PersonID) = False Then
                        Dim sCheckAlreadyGroupAddedSql As String = AptifyApplication.GetEntityBaseDatabase("Web Menus") & "..spCheckAlreadyWebGroup__c @WebUserID=" & WebUserLogin1.User.UserID & ",@WebGroupID=" & lWebGroupID
                        Dim lWebAlreadyGrp As Long = Convert.ToInt32(DataAction.ExecuteScalar(sCheckAlreadyGroupAddedSql))
                        If lWebAlreadyGrp > 0 Then
                        Else
                            Dim oWebusersGroup As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                            oWebusersGroup = AptifyApplication.GetEntityObject("WebUserGroups", -1)
                            oWebusersGroup.SetValue("WebUserID", WebUserLogin1.User.UserID)
                            oWebusersGroup.SetValue("WebGroupID", lWebGroupID)
                            oWebusersGroup.Save()
                        End If
                    Else
                        Dim sCheckAlreadyGroupAddedSql As String = AptifyApplication.GetEntityBaseDatabase("Web Menus") & "..spCheckAlreadyWebGroup__c @WebUserID=" & WebUserLogin1.User.UserID & ",@WebGroupID=" & lWebGroupID
                        Dim lWebAlreadyGrp As Long = Convert.ToInt32(DataAction.ExecuteScalar(sCheckAlreadyGroupAddedSql))
                        If lWebAlreadyGrp > 0 Then
                            oWebUser.SubTypes("WebUserGroups").Find("WebGroupID", lWebGroupID).Delete()
                            oWebUser.Save()
                        End If
                    End If

                Else
                    Dim sCheckAlreadyGroupAddedSql As String = AptifyApplication.GetEntityBaseDatabase("Web Menus") & "..spCheckAlreadyWebGroup__c @WebUserID=" & WebUserLogin1.User.UserID & ",@WebGroupID=" & lWebGroupID
                    Dim lWebAlreadyGrp As Long = Convert.ToInt32(DataAction.ExecuteScalar(sCheckAlreadyGroupAddedSql))
                    If lWebAlreadyGrp > 0 Then
                        oWebUser.SubTypes("WebUserGroups").Find("WebGroupID", lWebGroupID).Delete()
                        oWebUser.Save()
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Check User can alrady applied for the membership application
        ''' </summary>
        ''' <param name="PersonID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function CheckAlreadyFillupMembershipApplication(ByVal PersonID As Integer) As Boolean
            Try
                Dim sSql = Database & "..spCheckAlradyFillupMembershipApp__c @PersonID=" & PersonID
                Dim sDate As String = Convert.ToString(DataAction.ExecuteScalar(sSql))
                If sDate <> "" AndAlso Not sDate Is Nothing AndAlso sDate <> "01/01/1900 00:00:00" Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        'Code End Govind
        'Neha, Issue 14408,03/20/13, check firstlogin for user by tracking session count value
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
        'Nalini Issue 12734
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
                    WebUserLogin1.ClearAutoLoginCookie(Page.Response)
                    'HP Issue#9078: clear and delete session
                    Session.Clear()
                    Session.Abandon()
                    'Suraj Issue 15370,3/19/13 remove the application session after click on logout button
                    Session.Remove("ReturnToPage")
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
                    Else
                        tblLogin.Visible = False
                        litLoginLabel.Visible = False
                        tblWelcome.Visible = True
                        lblWelcome.Text = "Welcome, " & _
                                          .User.FirstName & " " & _
                                          .User.LastName
                        .User.SaveValuesToSessionObject(Page.Session)
                    End If
                End With
                'lblLogin.Visible = Me.ShowTitle
                'If lblLogin.Visible Then
                'lblLogin.InnerText = Me.TitleString
                'End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub cmdNewUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewUser.Click

            Try
                Session("SocialNetwork") = Nothing
                Response.Redirect(NewUserPage, False)
            Catch ex As Exception

            End Try

        End Sub

        Protected Overridable Sub OnUserLoggedIn()

            Dim sRedirectLocation As String = ""

            Try
                RaiseEvent UserLoggedIn()
                'Sapna - Issue #12582
                Session("UserLoggedIn") = True

                If Len(Session("ReturnToPage")) <> 0 Then
                    Dim sTemp As String
                    sTemp = CStr((Session("ReturnToPage")))
                    Session("ReturnToPage") = "" ' used only once
                    sRedirectLocation = sTemp
                    Response.Redirect(sTemp)
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
            'Sapna - Issue #12582
            If Not Session Is Nothing Then
                Session("UserLoggedIn") = False
            End If
            Response.Redirect(Request.RawUrl)
        End Sub

        Protected Sub chkAutoLogin_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAutoLogin.CheckedChanged
            'Anil B for  Issue 13882 on 18-03-2013
            'Set Remember option for login
            'Modified by Dipali Story No:13882:-e-Business: Support Remember Me Option When Logging in Using LinkedIn
            ' lbl.Text = CStr(chkAutoLogin.Checked)
            'Check if the browser support cookies
            If Request.Browser.Cookies Then
                'Check if the cookie with name LOGIN exist on user's machine
                'If (Request.Cookies("LOGIN") Is Nothing) Then
                'Create a cookie with expiry of 30 days
                Response.Cookies("LOGIN").Expires = DateTime.Now.AddDays(30)
                'Write username to the cookie
                Response.Cookies("LOGIN").Item("RememberMe") = chkAutoLogin.Checked
            End If
            'End If
        End Sub


    End Class
End Namespace