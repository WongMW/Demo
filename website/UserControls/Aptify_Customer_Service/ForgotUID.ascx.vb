Option Explicit On
Option Strict On

Imports Aptify.Framework.DataServices
Imports Aptify.Framework.Application
Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity

Namespace Aptify.Framework.Web.eBusiness
    Partial Class ForgotUIDControl
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_SERVICE_EMAIL_ADDRESS As String = "OrganizationServiceEmail"
        Protected Const ATTRIBUTE_PWD_LENGTH As String = "minPwdLength"
        Protected Const ATTRIBUTE_PWD_UPPERCASE As String = "minPwdUpperCase"
        Protected Const ATTRIBUTE_PWD_LOWERCASE As String = "minPwdLowerCase"
        Protected Const ATTRIBUTE_PWD_NUMBERS As String = "minPwdNumbers"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "ForgotUID"

#Region "ForgotUID Specific Properties"
        ''' <summary>
        ''' ServiceEmailAddress address
        ''' </summary>
        Public Overridable Property ServiceEmailAddress() As String
            Get
                If Not ViewState("ServiceEmailAddress") Is Nothing Then
                    Return CStr(ViewState("ServiceEmailAddress"))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState("ServiceEmailAddress") = value
            End Set
        End Property
        ''' <summary>
        ''' Minimum password length as provided in config file, default value is 6 when config file is not set
        ''' </summary>
        Public Overridable ReadOnly Property MinPwdLength() As Integer
            Get
                If ViewState.Item(ATTRIBUTE_PWD_LENGTH) IsNot Nothing Then
                    Return CInt(ViewState.Item(ATTRIBUTE_PWD_LENGTH))
                Else
                    Dim value As Integer = Me.GetGlobalAttributeIntegerValue(ATTRIBUTE_PWD_LENGTH)
                    If value <> -1 Then
                        ViewState.Item(ATTRIBUTE_PWD_LENGTH) = value
                        Return CInt(value)
                    Else
                        Return 6 'default value when nothing is set in config file
                    End If
                End If
            End Get
        End Property
        ''' <summary>
        ''' Minimum number of UpperCase letters required in password as provided in config file, default value is 1 when config file is not set
        ''' </summary>
        Public Overridable ReadOnly Property MinPwdUpperCase() As Integer
            Get
                If ViewState.Item(ATTRIBUTE_PWD_UPPERCASE) IsNot Nothing Then
                    Return CInt(ViewState.Item(ATTRIBUTE_PWD_UPPERCASE))
                Else
                    Dim value As Integer = Me.GetGlobalAttributeIntegerValue(ATTRIBUTE_PWD_UPPERCASE)
                    If value <> -1 Then
                        ViewState.Item(ATTRIBUTE_PWD_UPPERCASE) = value
                        Return CInt(value)
                    Else
                        Return 1 'default value when nothing is set in config file
                    End If
                End If
            End Get
        End Property
        ''' <summary>
        ''' Minimum number of LowerCase letters required in password as provided in config file, default value is 1 when config file is not set
        ''' </summary>
        Public Overridable ReadOnly Property MinPwdLowerCase() As Integer
            Get
                If ViewState.Item(ATTRIBUTE_PWD_LOWERCASE) IsNot Nothing Then
                    Return CInt(ViewState.Item(ATTRIBUTE_PWD_LOWERCASE))
                Else
                    Dim value As Integer = Me.GetGlobalAttributeIntegerValue(ATTRIBUTE_PWD_LOWERCASE)
                    If value <> -1 Then
                        ViewState.Item(ATTRIBUTE_PWD_LOWERCASE) = value
                        Return CInt(value)
                    Else
                        Return 1 'default value when nothing is set in config file
                    End If
                End If
            End Get
        End Property
        ''' <summary>
        ''' Minimum number of numeric letters required in password as provided in config file, default value is 1 when config file is not set
        ''' </summary>
        Public Overridable ReadOnly Property MinPwdNumbers() As Integer
            Get
                If ViewState.Item(ATTRIBUTE_PWD_NUMBERS) IsNot Nothing Then
                    Return CInt(ViewState.Item(ATTRIBUTE_PWD_NUMBERS))
                Else
                    Dim value As Integer = Me.GetGlobalAttributeIntegerValue(ATTRIBUTE_PWD_NUMBERS)
                    If value <> -1 Then
                        ViewState.Item(ATTRIBUTE_PWD_NUMBERS) = value
                        Return CInt(value)
                    Else
                        Return 1 'default value when nothing is set in config file
                    End If
                End If
            End Get
        End Property
        'HP Issue#9078: function added for password complexity
        Private Function IsPasswordComplex(ByVal password As String) As Boolean
            Dim result As Boolean = False

            If password.Length >= MinPwdLength Then
                result = System.Text.RegularExpressions.Regex.IsMatch(password, "(?=(.*[A-Z].*){" & MinPwdUpperCase & ",})(?=(.*[a-z].*){" & MinPwdLowerCase & ",})(?=(.*\d.*){" & MinPwdNumbers & ",})")
            End If

            If Not result Then
                lblError.Text = "Password must be a minimum length of " & MinPwdLength.ToString & " with at least " & _
                                MinPwdLowerCase & " lower-case letter(s) and " & MinPwdUpperCase & " upper-case letter(s) and " & _
                                MinPwdNumbers & " number(s)."
            End If
            Return result
        End Function
#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()

            'This function shows/hides page elements appropriately based on the 
            'state of the request.  RN 4/9/2003.
            Try
                Dim sUID As String
                If Not IsPostBack() Then
                    ' Changes made for to allow encrypting and decrypting the URL.
                    ' Changes made by Hrushikesh Jog

                    sUID = Request.QueryString("p1")
                    sUID = Aptify.Framework.Web.Common.WebCryptography.Decrypt(sUID)
                    If Len(sUID) = 0 Then
                        '3/7/08 RJK - ChangePasswordOK Session variable now stores the UserID
                        'that has been validated.  Only that UserID's password can be changed. 
                        Session.Remove("ChangePasswordOK")

                        tblUserName.Visible = True
                        tblPWDHint.Visible = False
                        tblPasswordChange.Visible = False
                    Else
                        '3/7/08 RJK - ChangePasswordOK Session variable now stores the UserID
                        'that has been validated.  Only that UserID's password can be changed. 
                        If Session.Item("ChangePasswordOK") IsNot Nothing AndAlso _
                                String.Compare(Session.Item("ChangePasswordOK").ToString, sUID, True) = 0 Then
                            tblUserName.Visible = False
                            tblPWDHint.Visible = False
                            tblPasswordChange.Visible = True
                            lblUID.Text = sUID
                        Else
                            '3/7/08 RJK - Want to make sure that the ChangePasswordOK is removed so
                            'that, if it exists, the old Web User ID is cleared.
                            Session.Remove("ChangePasswordOK")
                            lblHint.Text = GetPasswordQuestion(sUID)
                            tblUserName.Visible = False
                            tblPWDHint.Visible = True
                            tblPasswordChange.Visible = False
                        End If
                    End If
                End If
            Catch ex As FormatException
                ' changed code to use config settings instead of hardcoded paths
                Response.Redirect(Me.GetSecurityErrorPageFromXML & "?Message=" & Server.UrlEncode("User not available"))


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(ServiceEmailAddress) Then
                ServiceEmailAddress = Me.GetLinkValueFromXML(ATTRIBUTE_SERVICE_EMAIL_ADDRESS)
                If String.IsNullOrEmpty(ServiceEmailAddress) Then
                    Me.mailAddress.Enabled = False
                    Me.mailAddress.ToolTip = "ServiceEmailAddress property has not been set."
                    Me.mailAddress.Text = "<u>(Email Address has not been set)</u>"
                Else
                    Me.mailAddress.NavigateUrl = "mailto:" & ServiceEmailAddress
                    Me.mailAddress.Text = ServiceEmailAddress
                End If
            Else
                Me.mailAddress.NavigateUrl = "mailto:" & ServiceEmailAddress
                Me.mailAddress.Text = ServiceEmailAddress
            End If

        End Sub

        Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
            Try

                'When the user clicks submit, figure out the stage of the process and proceed
                'with the appropriate step, RN 4/9/03.

                ' Changes made for to allow encrypting and decrypting the URL.
                ' Changes made by Hrushikesh Jog

                Dim sUID As String
                sUID = Request.QueryString("p1")
                sUID = Aptify.Framework.Web.Common.WebCryptography.Decrypt(sUID)
                If sUID <> "" Then
                    If Session.Item("ChangePasswordOK") IsNot Nothing AndAlso _
                            String.Compare(Session.Item("ChangePasswordOK").ToString, sUID, True) = 0 _
                            AndAlso Not IsWebUserDisabled(sUID) Then

                        If txtPWD.Text = txtRepeatPWD.Text Then
                            'HP Issue#9078: check if password meets complexity requirements
                            If Not IsPasswordComplex(txtPWD.Text) Then
                                lblError.Visible = True
                                Exit Sub
                            End If
                            If UpdateProfile(sUID, txtPWD.Text) Then
                                '3/7/08 RJK - ChangePasswordOK Session variable is a security hole
                                'if we don't remove it.
                                Session.Remove("ChangePasswordOK")
                                Session("UserLoggedIn") = True
                                Response.Redirect(Request.ApplicationPath)
                            Else
                                lblError.Text = "The new password could not be updated."
                            End If
                        Else
                            lblError.Text = "The passwords entered do not match."
                        End If
                    Else
                        If Not IsWebUserDisabled(sUID) AndAlso ValidateAnswer() Then
                            '3/7/08 RJK - ChangePasswordOK Session variable now contains the UID, 
                            'so that the Password can only be changed for the Web User where the correct
                            'Password Hint answer has been provided.
                            Session.Add("ChangePasswordOK", sUID)
                            ' Changes made for to allow encrypting and decrypting the URL.
                            ' Changes made by Hrushikesh Jog
                            sUID = Request.QueryString("p1")
                            sUID = Aptify.Framework.Web.Common.WebCryptography.Decrypt(sUID)
                            Response.Redirect(Me.Request.Url.AbsolutePath & "?p1=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sUID)))
                            'error message will be set by conditional functions
                            'Else
                            '    lblError.Text = "Incorrect Password Hint Answer."
                        End If
                    End If
                Else
                    If DoesWebUserExist(txtUID.Text) AndAlso Not IsWebUserDisabled(txtUID.Text) Then
                        If GetPasswordQuestion(txtUID.Text) <> "" Then
                            Response.Redirect(Me.Request.Url.AbsolutePath & "?p1=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(txtUID.Text)))
                        Else
                            lblError.Text = "The User entered (" & txtUID.Text & ") did not set up a password hint."
                        End If
                        'error message will be set by conditional functions
                        'Else
                        '    lblError.Text = "User does not exist."
                    End If
                End If
            Catch ex As FormatException
                ' changed code to use config settings instead of hardcoded paths
                Response.Redirect(Me.GetSecurityErrorPageFromXML & "?Message=" & Server.UrlEncode("User not available"))


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'HP Issue#9078: function added to insure a disabled account cannot changed their password
        Private Function IsWebUserDisabled(ByVal sUID As String) As Boolean
            'Returns true if the user is disabled, false otherwise.
            Dim dt As DataTable
            Dim sSQL As String
            Dim colParams(0) As Data.IDataParameter
            Dim UserID As Data.IDataParameter = Nothing

            Try
                sSQL = Database & "..spGetWebuser"

                UserID = DataAction.GetDataParameter("@UserID", SqlDbType.NVarChar, sUID)
                colParams(0) = UserID
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, colParams)

                If dt.Rows.Count > 0 Then
                    lblError.Text = "Account is disabled, please contact the administrator."
                    IsWebUserDisabled = CBool(dt.Rows(0)("Disabled"))
                Else
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        Private Function ValidateAnswer() As Boolean
            'Job of this function is to ensure that the password hint answer is correct.
            'RN 4/9/03.
            Dim dt As DataTable
            Dim sSQL As String
            Dim sUID As String
            Dim colParams(0) As Data.IDataParameter
            Dim UserID As Data.IDataParameter = Nothing

            Try
                ' Changes made for to allow encrypting and decrypting the URL.
                ' Changes made by Hrushikesh Jog

                sUID = Request.QueryString("p1")
                sUID = Aptify.Framework.Web.Common.WebCryptography.Decrypt(sUID)
                UserID = DataAction.GetDataParameter("@UserID", SqlDbType.NVarChar, sUID)
                colParams(0) = UserID
                sSQL = Database & "..spGetWebUser "
                'dt = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                ' Changes made by Hrushikesh Jog
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, colParams)

                If dt.Rows.Count > 0 Then
                    If String.Compare(Trim$(CStr(dt.Rows(0).Item("PasswordHintAnswer"))), _
                                      Trim$(txtAnswer.Text), True) = 0 Then
                        Return True
                    Else
                        lblError.Text = "Incorrect Password Hint Answer."
                        Return False
                    End If
                Else
                    lblError.Text = "Incorrect Password Hint Answer."
                    Return False
                End If

            Catch ex As FormatException
                ' changed code to use config settings instead of hardcoded paths
                Response.Redirect(Me.GetSecurityErrorPageFromXML & "?Message=" & Server.UrlEncode("User not available"))


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                lblError.Text = "Incorrect Password Hint Answer."
                Return False
            End Try
        End Function

        Private Function DoesWebUserExist(ByVal sUID As String) As Boolean
            'Returns true if the user exists, false otherwise.  RN 4/9/03.
            Dim dt As DataTable
            Dim sSQL As String
            Dim colParams(0) As Data.IDataParameter
            Dim UserID As Data.IDataParameter = Nothing

            Try
                sSQL = Database & "..spGetWebuser"

                ' Changes made by Hrushikesh Jog
                UserID = DataAction.GetDataParameter("@UserID", SqlDbType.NVarChar, sUID)
                colParams(0) = UserID
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, colParams)

                If dt.Rows.Count > 0 Then
                    DoesWebUserExist = True
                Else
                    lblError.Text = "User does not exist."
                    DoesWebUserExist = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        Private Function GetPasswordQuestion(ByVal sUID As String) As String
            'Job of this function is to retrieve the password question for the user. RN 4/9/03.
            Dim dt As DataTable
            Dim sSQL As String
            Dim colParams(0) As Data.IDataParameter
            Dim UserID As Data.IDataParameter = Nothing

            Try

                sSQL = "SELECT PasswordHint FROM " & Database & _
                       "..vwWebUsers WHERE UserID=@UserID"

                UserID = DataAction.GetDataParameter("@UserID", SqlDbType.NVarChar, sUID)
                colParams(0) = UserID
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.Text, colParams)

                If dt.Rows.Count > 0 Then
                    Return CStr(dt.Rows(0).Item(0))
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return Nothing
            End Try
        End Function

        Private Function GetUserIDFromName(ByVal sUID As String) As Long
            'Obtain the Web User Record ID corresponding to the web user name.
            'Uniqueness is assumed.  RN 4/9/03.
            Dim dt As DataTable
            Dim sSQL As String
            Dim colParams(0) As Data.IDataParameter
            Dim UserID As Data.IDataParameter = Nothing

            Try
                sSQL = "SELECT ID FROM " & Database & "..vwWebUsers WHERE UserID=@UserID"

                UserID = DataAction.GetDataParameter("@UserID", SqlDbType.NVarChar, sUID)
                colParams(0) = UserID
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.Text, colParams)

                If dt.Rows.Count > 0 Then
                    Return CLng(dt.Rows(0).Item(0))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        Private Function UpdateProfile(ByVal sUID As String, ByVal sPWD As String) As Boolean
            'The job of this function is to update a user profile with new password
            'information.  RN April 9, 2003.
            Dim oUser As AptifyGenericEntityBase
            Dim lUserID As Long

            Try
                lUserID = GetUserIDFromName(sUID)
                oUser = AptifyApplication.GetEntityObject("Web Users", lUserID)

                'Update the password, then save and load the user:
                oUser.SetValue("PWD", sPWD)

                If oUser.Save Then
                    Return WebUserLogin1.Login(sUID, sPWD, Page.User)
                Else
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
    End Class
End Namespace
