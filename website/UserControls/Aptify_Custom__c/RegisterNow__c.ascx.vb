'Aptify e-Business 5.5.1, July 2013
'************************************** Class Summary ***********************************************
'Developer              Date Created/Modified               Summary
'Pradip Chavhan         2016-06-17                      For Register Now Control
'****************************************************************************************************

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
Imports System.Drawing
Imports Aptify.Consulting.DuplicateCheck



Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class RegisterNow__c

        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_PWD_LENGTH As String = "minPwdLength"
        Protected Const ATTRIBUTE_PWD_UPPERCASE As String = "minPwdUpperCase"
        Protected Const ATTRIBUTE_PWD_LOWERCASE As String = "minPwdLowerCase"
        Protected Const ATTRIBUTE_PWD_NUMBERS As String = "minPwdNumbers"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "Profile"
        Protected Const ATTRIBUTE_PERSON_BLANK_IMG As String = "BlankImage"
        'Anil Add for issue 12835
        Protected ATTRIBUTE_IMAGEUPLOAD_PROFILE_TNCTEXT As String = "ImageUploadUserProfileText"
        Protected ATTRIBUTE_IMAGEUPLOAD_PROFILESAVE_TNCTEXT As String = "ImageUploadUserProfileSaveText"
        Protected Const ATTRIBUTE_PERSON_IMG_WIDTH As String = "ImageWidth"
        Protected Const ATTRIBUTE_PERSON_IMG_HEIGHT As String = "ImageHeight"
        Protected Const ATTRIBUTE_SYSTEM_NAME As String = "SocialNetworkSystemName"
        Protected Const ATTRIBUTE_PROFILE_PHOTO_FILENAME As String = "ProfilePhotoFileName"
        ''ISSUEID 3240 - Added SuvarnaD  
        Protected Const ATTRIBUTE_PROFILE_ALERT_DUPLICATEPERSONVALIDATION As String = "DuplicatePersonValidation"
        'end 
        Protected m_bRemovePhoto As Boolean
        Protected m_sblankImage As String
        Dim m_lEntityID As Long
        Dim m_lRecordID As String
        'Anil Bisen issue 12835 
        Protected m_iImageWidth As Integer = -1
        Protected m_iImageHeight As Integer = -1
        Protected m_sAlert As String = String.Empty
        Dim m_sEntityName As String = "Persons"
        Dim ImageData() As Byte
        ''Added BY Pradip 2016-03-21 To Redirect To Membership Application Page
        Protected Const ATTRIBUTE_MEMBERSHIP_Page_URL As String = "MembershipPageURL"

#Region "Properties and Methods"
        Private Const m_c_sPrefix As String = "__aptify_shoppingCart_"
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



        'Anil Bisen issue 12835 
        Public Property ImageWidth() As Integer
            Get
                Return m_iImageWidth
            End Get
            Set(ByVal value As Integer)
                m_iImageWidth = value
            End Set
        End Property
        Public Property ImageHeight() As Integer
            Get
                Return m_iImageHeight
            End Get
            Set(ByVal value As Integer)
                m_iImageHeight = value
            End Set
        End Property

        Public Property CompanyName() As String
            Get
                If ViewState.Item("CompanyName") IsNot Nothing Then
                    Return ViewState.Item("CompanyName").ToString()
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item("CompanyName") = value
            End Set
        End Property
        Public Property CompanyAddress() As String
            Get
                If ViewState.Item("CompanyAddress") IsNot Nothing Then
                    Return ViewState.Item("CompanyAddress").ToString()
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item("CompanyAddress") = value
            End Set
        End Property
        Public Property CompanyCity() As String
            Get
                If ViewState.Item("CompanyCity") IsNot Nothing Then
                    Return ViewState.Item("CompanyCity").ToString()
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item("CompanyCity") = value
            End Set
        End Property

        Public Property CompanyCountry() As String
            Get
                If ViewState.Item("CompanyCountry") IsNot Nothing Then
                    Return ViewState.Item("CompanyCountry").ToString()
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item("CompanyCountry") = value
            End Set
        End Property
        Public Property CompanyState() As String
            Get
                If ViewState.Item("CompanyState") IsNot Nothing Then
                    Return ViewState.Item("CompanyState").ToString()
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item("CompanyState") = value
            End Set
        End Property
        Public Property Companyphone() As String
            Get
                If ViewState.Item("Companyphone") IsNot Nothing Then
                    Return ViewState.Item("Companyphone").ToString()
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item("Companyphone") = value
            End Set
        End Property


        'HP Issue#9078: function added for password complexity
        Private Function IsPasswordComplex(ByVal password As String) As Boolean
            Dim result As Boolean = False
            'Aparna for issue 12964 for showing password length validation
            lblpasswordlengthError.Text = ""
            If password.Length >= MinPwdLength Then
                result = System.Text.RegularExpressions.Regex.IsMatch(password, "(?=(.*[A-Z].*){" & MinPwdUpperCase & ",})(?=(.*[a-z].*){" & MinPwdLowerCase & ",})(?=(.*\d.*){" & MinPwdNumbers & ",})")
            End If
            'Aparna for issue 12964 for showing password length validation
            If Not result Then
                lblpasswordlengthError.Text = "Password must be a minimum length of " & MinPwdLength.ToString & " with at least " & _
                                MinPwdLowerCase & " lower-case letter(s) and " & MinPwdUpperCase & " upper-case letter(s) and " & _
                                MinPwdNumbers & " number(s)."
            End If
            Return result
        End Function





        Protected Overridable ReadOnly Property ImageUploadUserProfileText() As String
            Get
                If Not Session.Item(ATTRIBUTE_IMAGEUPLOAD_PROFILE_TNCTEXT) Is Nothing Then
                    Return CStr(Session.Item(ATTRIBUTE_IMAGEUPLOAD_PROFILE_TNCTEXT))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_IMAGEUPLOAD_PROFILE_TNCTEXT)
                    If Not String.IsNullOrEmpty(value) Then
                        Session.Item(ATTRIBUTE_IMAGEUPLOAD_PROFILE_TNCTEXT) = value
                        Return value
                    Else
                        Return String.Empty
                    End If
                End If
            End Get

        End Property

        'Anil 10258 
        Public ReadOnly Property SocialNetworkObject() As SocialNetworkIntegrationBase
            Get
                If Session("SocialNetwork") IsNot Nothing Then
                    Return DirectCast(Session("SocialNetwork"), SocialNetworkIntegrationBase)
                Else
                    If SocialNetworkSystemName <> "" AndAlso WebUserLogin1.User.UserID > 0 Then
                        Session("SocialNetwork") = SocialNetwork.SocialNetworkInstance(SocialNetworkSystemName, AptifyApplication, WebUserLogin1.User.UserID, Nothing, False)
                    End If
                End If
                Return DirectCast(Session("SocialNetwork"), SocialNetworkIntegrationBase)
            End Get
        End Property
        Public Overridable ReadOnly Property SocialNetworkSystemName() As String
            Get
                If Not Session.Item(ATTRIBUTE_SYSTEM_NAME) Is Nothing Then
                    Return CStr(Session.Item(ATTRIBUTE_SYSTEM_NAME))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_SYSTEM_NAME)
                    If Not String.IsNullOrEmpty(value) Then
                        Session.Item(ATTRIBUTE_SYSTEM_NAME) = value
                        Return value
                    Else
                        Return String.Empty
                    End If
                End If
            End Get

        End Property
        'end
        ''' <summary>
        ''' ''' ''ISSUEID 3240 - Added SuvarnaD  
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Overridable Property DuplicatePersonValidation() As String
            Get
                Return m_sAlert
            End Get
            Set(ByVal value As String)
                m_sAlert = value
            End Set
        End Property
#End Region

#Region "Page Events"

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            Dim js As HtmlGenericControl = New HtmlGenericControl("script")
            js.Attributes.Add("type", "text/javascript")
            js.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-1.7.1.min.js"))
            Me.Page.Header.Controls.Add(js)
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            txtFirstName.Focus()
            Dim CheckSessionValue As Boolean
            If Session("UserLoggedIn") IsNot Nothing Then
                CheckSessionValue = Convert.ToBoolean(Session("UserLoggedIn"))
                If CheckSessionValue Then
                    registerForm.visible = False
                End If
            End If
            regexEmailValid.ValidationExpression = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.EmailRegularExpressionValidator__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            If IsPostBack AndAlso txtPassword2.Text <> String.Empty Then
                txtPassword2.Attributes("value") = txtPassword2.Text
                txtRepeatPWD2.Attributes("value") = txtRepeatPWD2.Text

            End If
            SetProperties()
            m_lEntityID = CLng(AptifyApplication.GetEntityID("Persons"))

            If Not IsPostBack Then
                PopulateDropDowns()
            End If

        End Sub

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
            'If String.IsNullOrEmpty(ProcessingImage) Then
            '    ProcessingImage = Me.GetLinkValueFromXML(ATTRIBUTE_PROCESSING_IMAGE_URL)
            'End If
            ''Anil Changess for issue 12718
            'If String.IsNullOrEmpty(MembershipImageURL) Then
            '    MembershipImageURL = Me.GetLinkValueFromXML(ATTRIBUTE_MEMBERSHIP_IMAGE_URL)
            'End If
            'end
            'If String.IsNullOrEmpty(PersonImageURL) Then
            '    PersonImageURL = Me.GetLinkValueFromXML(ATTRIBUTE_PERSON_IMAGE_URL)
            'End If
            'If String.IsNullOrEmpty(BlankImage) Then
            '    BlankImage = Me.GetPropertyValueFromXML(ATTRIBUTE_PERSON_BLANK_IMG)
            'End If
        End Sub

#End Region

#Region "Load Data Methods"

        ''Added by dipali to get country list
        Private Sub PopulateDropDowns()
            Dim dtCountry As New DataTable

            Dim sSQL As String = AptifyApplication.GetEntityBaseDatabase("Addresses") & _
                      "..spGetCountryListProfile__c"
            dtCountry = DataAction.GetDataTable(sSQL)
            ddlHomeAddCountry.DataSource = dtCountry
            ddlHomeAddCountry.DataTextField = "Country"
            ddlHomeAddCountry.DataValueField = "ID"
            ddlHomeAddCountry.DataBind()
            ddlHomeAddCountry.Items.Insert(0, "Select Country")
            SetComboValue(ddlHomeAddCountry, "Ireland")
        End Sub

        Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList, _
                               ByVal sValue As String)
            Dim i As Integer

            Try
                For i = 0 To cmb.Items.Count - 1
                    If String.Compare(cmb.Items(i).Value, sValue, True) = 0 Then
                        cmb.Items(i).Selected = True
                        Exit Sub
                    End If

                    If String.Compare(cmb.Items(i).Text, sValue, True) = 0 Then
                        cmb.Items(i).Selected = True
                        Exit Sub
                    End If

                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region

#Region "Save Methods"

        Private Function DoSave() As Boolean

            Try
                Dim bNewUser As Boolean = False
                bNewUser = User1.UserID <= 0
                User1.SetValue("FirstName", txtFirstName.Text)
                User1.SetValue("LastName", txtLastName.Text)
                User1.SetValue("Email", txtEmail.Text)
                User1.SetAddValue("Email1", txtEmail.Text)
                User1.SetValue("WebUserStringID", txtEmail.Text.Trim)

                If User1.UserID <= 0 Then
                    User1.SetValue("Password", txtPassword2.Text)
                    User1.SetValue("PasswordHint", txtEmail.Text.Trim)
                    User1.SetValue("PasswordHintAnswer", txtEmail.Text.Trim)
                End If
                User1.SaveValuesToSessionObject(Page.Session)
                If User1.Save() Then
                    Dim oPerson As AptifyGenericEntityBase
                    oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                    If Not String.IsNullOrEmpty(txtDob.SelectedDate.ToString().Trim()) Then
                        oPerson.SetValue("Birthday", txtDob.SelectedDate.ToString().Trim())
                    End If
                    oPerson.SetValue("Status__c", "Pending")
                    'Added By dipali to save country
                    oPerson.SetValue("HomeCountryCodeID", ddlHomeAddCountry.SelectedValue)
                    oPerson.SetValue("HomeCountry", ddlHomeAddCountry.SelectedItem.Text.Trim)
                    oPerson.SetValue("HomeAddressLine4", ".")
                    oPerson.Save(False)
                    Dim sError As String = ""
                    'If SocialNetworkObject IsNot Nothing AndAlso SocialNetworkObject.IsConnected Then
                    '    SocialNetworkObject.UserProfile.EBusinessUser = User1
                    '    If SocialNetworkObject.UserProfile.SynchronizePersonExternalAccount(sError) Then
                    '        If bNewUser AndAlso SocialNetworkObject.UserProfile.SynchronizeProfile Then
                    '            Dim sSQL As String = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='Social Network User Profile Synchronization'"
                    '            Dim oProcessFlowID As Object = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache)
                    '            If oProcessFlowID IsNot Nothing AndAlso IsNumeric(oProcessFlowID) Then
                    '                Dim lProcessFlowID As Long = CLng(oProcessFlowID)
                    '                If lProcessFlowID > 0 Then
                    '                    Dim oContext As New AptifyContext
                    '                    Dim result As ProcessFlowResult
                    '                    oContext.Properties.AddProperty("PersonExternalAccountID", SocialNetworkObject.UserProfile.PersonExternalAccountID)
                    '                    result = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, oContext)
                    '                    If Not result.IsSuccess Then
                    '                        ExceptionManagement.ExceptionManager.Publish(New Exception("Unable to synchronize social network profile for user '" & User1.WebUserStringID & "'"))
                    '                    End If
                    '                End If
                    '            End If
                    '        End If
                    '    End If
                    'End If
                    'Dim sOrderXML As String
                    'If Session.Item(m_c_sPrefix & "OrderXML") IsNot Nothing Then
                    '    sOrderXML = Session.Item(m_c_sPrefix & "OrderXML").ToString

                    '    If sOrderXML.Length > 0 Then
                    '        '20090123 MAS: update the address based on preferred address

                    '        Dim prefShip As AptifyShoppingCart.PersonAddressType
                    '        Dim prefBill As AptifyShoppingCart.PersonAddressType

                    '        Dim UserPrefShip As String = User1.GetValue("PreferredShippingAddress").Trim.ToLower
                    '        If UserPrefShip.Contains("home") Then
                    '            prefShip = AptifyShoppingCart.PersonAddressType.Home
                    '        ElseIf UserPrefShip.Contains("business") Then
                    '            prefShip = AptifyShoppingCart.PersonAddressType.Main
                    '        ElseIf UserPrefShip.Contains("billing") Then
                    '            prefShip = AptifyShoppingCart.PersonAddressType.Billing
                    '        ElseIf UserPrefShip.Contains("po") Then
                    '            prefShip = AptifyShoppingCart.PersonAddressType.POBox
                    '        Else
                    '            prefShip = AptifyShoppingCart.PersonAddressType.Main
                    '        End If

                    '        Dim UserPrefBill As String = User1.GetValue("PreferredBillingAddress").Trim.ToLower
                    '        If UserPrefBill.Contains("home") Then
                    '            prefBill = AptifyShoppingCart.PersonAddressType.Home
                    '        ElseIf UserPrefBill.Contains("business") Then
                    '            prefBill = AptifyShoppingCart.PersonAddressType.Main
                    '        ElseIf UserPrefBill.Contains("billing") Then
                    '            prefBill = AptifyShoppingCart.PersonAddressType.Billing
                    '        ElseIf UserPrefBill.Contains("po") Then
                    '            prefBill = AptifyShoppingCart.PersonAddressType.POBox
                    '        Else
                    '            prefBill = AptifyShoppingCart.PersonAddressType.Main
                    '        End If

                    '        Me.ShoppingCart1.UpdateOrderAddress(AptifyShoppingCart.OrderAddressType.Shipping, prefShip, 0, Me.Session, Me.Application)
                    '        Me.ShoppingCart1.UpdateOrderAddress(AptifyShoppingCart.OrderAddressType.Billing, prefBill, 0, Me.Session, Me.Application)
                    '    End If
                    'End If

                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        'Private Function DoSave_Edit() As Boolean

        '    Try
        '        Dim bNewUser As Boolean = False
        '        bNewUser = User1.UserID <= 0

        '        Dim oPerson As AptifyGenericEntityBase
        '        oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
        '        Dim sPendingChangesField As String = String.Empty
        '        oPerson.SetValue("FirstName", txtFirstName.Text)
        '        oPerson.SetValue("LastName", txtLastName.Text)
        '        oPerson.SetValue("Email", txtEmail.Text)
        '        oPerson.SetAddValue("Email1", txtEmail.Text)
        '        If txtDob.SelectedDate IsNot Nothing Then
        '            oPerson.SetValue("Birthday", txtDob.SelectedDate)
        '        End If
        '        Dim sErr As String = String.Empty
        '        If oPerson.Save(sErr) Then
        '            Return True
        '        Else
        '            Return False
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '        Return False
        '    End Try
        '    Return True
        'End Function


#End Region


#Region "Button Events"

        Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
            Dim bNewUser As Boolean
            'Begin of code for duplicate webuser #19819
            If (String.IsNullOrWhiteSpace(txtFirstName.Text.Trim) = False) And (String.IsNullOrWhiteSpace(txtLastName.Text.Trim) = False) And (String.IsNullOrWhiteSpace(txtDob.SelectedDate.ToString().Trim()) = False) Then
                Try
                    Dim sSql1 As String = String.Empty
                    Dim p(2) As IDataParameter
                    sSql1 = Database & "..spGetWebUserIDByFirstNameLastNamedob__cai"
                    p(0) = DataAction.GetDataParameter("@fname", SqlDbType.VarChar, txtFirstName.Text.Trim)
                    p(1) = DataAction.GetDataParameter("@lname", SqlDbType.VarChar, txtLastName.Text.Trim)
                    p(2) = DataAction.GetDataParameter("@dob", SqlDbType.NVarChar, txtDob.SelectedDate.ToString().Trim())
                    Dim wuid As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSql1, CommandType.StoredProcedure, p))
                    If wuid > 0 Then
                        lblError.Text = "You already have an account. Please login or reset your password."
                        lblError.Visible = True
                        lblError.ForeColor = Drawing.Color.Red
                        lblError.Attributes.Add("class", "action-error-msg")
                        'ResetCreateForm()
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#duModal').modal();", True)
                        Exit Sub
                    End If
                Catch ex As Exception
                    lblError.Text = ex.InnerException.Message
                End Try
            End If
            'End of code for duplicate webuser #19819

            Try
                If Not txtDob.IsEmpty Then 'Modified by Harish Redmine #20848 - If Condition Added by Govind M for Redmine #18909
                    bNewUser = User1.UserID <= 0
                    Page.Validate()
                    Dim oPerson As AptifyGenericEntityBase = Nothing
                    If Not bNewUser Then
                        oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                    Else
                        oPerson = AptifyApplication.GetEntityObject("Persons", -1)
                    End If
                    Try
                        If Not oPerson Is Nothing AndAlso oPerson.IsDirty Then
                            Dim oPersonDupeCheck As New PersonDupeCheck
                            oPerson.SetValue("FirstName", txtFirstName.Text)
                            oPerson.SetValue("LastName", txtLastName.Text)
                            If txtDob.SelectedDate IsNot Nothing Then
                                oPerson.SetValue("Birthday", txtDob.SelectedDate.ToString().Trim())
                            End If
                            oPerson.SetValue("Email1", txtEmail.Text)
                            Dim duplicateID() As Long = Nothing
                            'aptify code not working for duplicateCheck result
                            'If oPersonDupeCheck.CheckForDuplicates(oPerson, duplicateID) = DuplicateCheckResult.Exact Then
                            '    oPerson = Nothing
                            '    radDuplicateUser.VisibleOnPageLoad = True
                            '    Exit Sub
                            'End If



                            Dim sSql As String = String.Empty
                            Dim param(0) As IDataParameter
                            sSql = Database & "..spGetWebUserIDByName__c"
                            param(0) = DataAction.GetDataParameter("@UserName", SqlDbType.NVarChar, txtEmail.Text.Trim)
                            Dim UserID As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSql, CommandType.StoredProcedure, param))
                            If UserID > 0 Then
                                lblError.Text = "This email address is already registered. Please login or reset your password."
                                lblError.Visible = True
                                lblError.ForeColor = Drawing.Color.Red
                                Exit Sub
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                    lblError.Visible = False

                    lblpasswordlengthError.Visible = False
                    If User1.UserID <= 0 Then
                        If txtPassword2.Text <> txtRepeatPWD2.Text Or Trim$(txtPassword2.Text) = "" Then
                            lblError.Text = "Password fields must match and must not be blank."
                            lblError.Visible = True
                            lblError.ForeColor = Drawing.Color.Red
                            Exit Sub
                        Else
                            If Not IsPasswordComplex(txtPassword2.Text) Then
                                lblpasswordlengthError.Visible = True
                                Exit Sub
                            End If
                        End If
                    End If
                    Dim bRecordSaved As Boolean = False

                    Dim blinkTopicCaode As Boolean = False
                    If User1.UserID <= 0 Then
                        bRecordSaved = DoSave()
                        'START:ticket:#0000: Topic code Insert code /Topic code link  if contact me  check box is checked
                        If (con.Checked) Then

                            blinkTopicCaode = InsertTopicCode(txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text.Trim())

                        End If

                        'END:ticket:#0000

                        'Else
                        ' bRecordSaved = DoSave_Edit()
                    End If

                    If bRecordSaved Then
                        Dim bOK As Boolean
                        Session("LoadSocialNetworkPhoto") = False
                        If bNewUser Then
                            bOK = WebUserLogin1.Login(User1.WebUserStringID, txtPassword2.Text, Page.User)
                            WebUserLogin1.User.SaveValuesToSessionObject(Page.Session)
                            Session("UserLoggedIn") = True
                        Else
                            bOK = True
                        End If
                        If bOK Then
                            txtDob.SelectedDate = Nothing
                            txtFirstName.Text = ""
                            txtLastName.Text = ""
                            txtEmail.Text = ""
                            txtPassword2.Text = String.Empty
                            txtRepeatPWD2.Text = String.Empty

                            ' Response.Redirect("/home.aspx")
                            If Len(Session("ReturnToPage")) > 0 Then
                                Dim sTemp As String
                                sTemp = CStr(Session("ReturnToPage"))
                                Session("ReturnToPage") = "" ' used only once
                                Try
                                    Response.Redirect(sTemp, True)
                                Catch ex As Exception
                                    'Sorry
                                End Try
                            Else
                                Try
                                    Response.Redirect("/home.aspx", True)
                                Catch ex As Exception
                                    'Sorry
                                End Try
                            End If
                        Else
                            lblError.Text = "Error logging in"
                            lblError.Visible = True
                            lblError.ForeColor = Drawing.Color.Red
                            ''Added By Pradip 2016-03-21
                            Exit Sub
                        End If
                    Else
                        lblError.Text = User1.GetLastError()
                        If lblError.Text.IndexOf(lblError.Text, StringComparison.InvariantCultureIgnoreCase) >= 0 Then
                            lblError.Text &= "  Try a different User ID."
                            lblError.ForeColor = Drawing.Color.Red
                        End If
                        lblError.Visible = True

                        Exit Sub
                    End If
                Else                                          'Modified by Harish Redmine #20848 else Condition Added by Govind M for Redmine #18909
                    lblError.Text = "Date of birth required." 'Modified by Harish Redmine #20848
                    lblError.ForeColor = Drawing.Color.Red    'Modified by Harish Redmine #20848
                    lblError.Visible = True                   'Modified by Harish Redmine #20848
                    Exit Sub                                  'Modified by Harish Redmine #20848
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub
        Private Function InsertTopicCode(fn As String, ln As String, em As String) As Boolean
            Dim rvalue As Boolean = False
            Try
                ' call SP to check person Id 

                Dim sSql As String = String.Empty
                Dim rname As String = String.Empty
                rname = String.Concat(fn, " ", ln)
                Dim param(2) As IDataParameter
                sSql = Database & "..spGetPersonIdByFnameLnameEmail__cai"
                param(0) = DataAction.GetDataParameter("@fname", SqlDbType.NVarChar, fn)
                param(1) = DataAction.GetDataParameter("@lname", SqlDbType.NVarChar, ln)
                param(2) = DataAction.GetDataParameter("@email", SqlDbType.NVarChar, em)
                Dim pid As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSql, CommandType.StoredProcedure, param))
                If pid > 0 Then
                    ' Add topic code to link
                    rvalue = AddTopicCodeLink(pid, rname)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                rvalue = False
            End Try
            Return rvalue
        End Function
        ' Insert Topic code 
        Private Function AddTopicCodeLink(rid As Integer, rname As String) As Boolean
            Dim oLink As AptifyGenericEntityBase
            Dim TopicCodeID As Long = 1318
            Dim ErrorString As String = ""

            Try
                oLink = AptifyApplication.GetEntityObject("Topic Code Links", -1)
                oLink.SetValue("TopicCodeID", TopicCodeID)
                oLink.SetValue("RecordID", rid)
                oLink.SetValue("EntityID", AptifyApplication.GetEntityID("Persons"))
                oLink.SetValue("Status", "Active")
                oLink.SetValue("Value", "Yes")
                oLink.SetValue("DateAdded", Date.Today)
                Return oLink.Save(False, ErrorString)


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False

            End Try
        End Function

        'Private Function GetNodeCheckSQL(ByVal lTopicCodeID As Long, Optional ByRef lPersonID As Long = -1) As String
        '    Return "SELECT COUNT(*) FROM " &
        '           GetNodeBaseSQL(lTopicCodeID, lPersonID)
        'End Function
        'Private Function GetNodeLinkSQL(ByVal lTopicCodeID As Long, Optional ByRef lPersonID As Long = -1) As String
        '    Return "SELECT ID FROM " &
        '           GetNodeBaseSQL(lTopicCodeID, lPersonID)
        'End Function


#End Region




        Protected Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
            radDuplicateUser.VisibleOnPageLoad = False
        End Sub





        'Begin of code for duplicate webuser ##19819
        Sub ResetCreateForm()
            txtFirstName.Text = ""
            txtLastName.Text = ""
            ' ddlHomeAddCountry.Items.FindByText("Ireland").Selected = True
            txtDob.Clear()
            txtEmail.Text = ""
            txtPassword2.Text = ""
            txtRepeatPWD2.Text = ""
            lblError.Text = ""

        End Sub
        'End of code for duplicate webuser ##19819

    End Class
End Namespace
