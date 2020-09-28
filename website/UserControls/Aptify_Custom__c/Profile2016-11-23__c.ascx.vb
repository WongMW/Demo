'Aptify e-Business 5.5.1, July 2013
'************************************** Class Summary ***********************************************
'Developer              Date Created/Modified               Summary
'Rajesh Kardile             03/14/2014                  To display the additional organization on user profile page.
'Abhishek Tapkir            02/18/2015                  Replaced Session variables by Application variables.
'Shital Jadhav              11/06/2015                  To add status as pedning if new person is created.
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
    Partial Class ProfileControl__c

        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced


        Protected Const ATTRIBUTE_PROCESSING_IMAGE_URL As String = "ProcessingImageURL"
        Protected Const ATTRIBUTE_PERSON_IMAGE_URL As String = "PersonImageURL"
        'Anil Changess for issue 12718
        Protected Const ATTRIBUTE_MEMBERSHIP_IMAGE_URL As String = "MembershipImageURL"
        'end
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
        ''Added BY Pradip 2016-03-21 To Redirect To Membership Application Page
        Public Overridable Property MembershipPage() As String
            Get
                If Not ViewState(ATTRIBUTE_MEMBERSHIP_Page_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_MEMBERSHIP_Page_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_MEMBERSHIP_Page_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Private Property PreferredAddress() As String
            Get
                If ViewState.Item("PreferredAddress") IsNot Nothing Then
                    Return ViewState.Item("PreferredAddress").ToString()
                Else
                    Return ""
                End If

            End Get
            Set(ByVal value As String)
                ViewState.Item("PreferredAddress") = value
            End Set
        End Property

        Public Property ProcessingImage() As String
            Get
                If ViewState.Item("ProcessingImage") IsNot Nothing Then
                    Return ViewState.Item("ProcessingImage").ToString()
                Else
                    Return ""
                End If

            End Get
            Set(ByVal value As String)
                ViewState.Item("ProcessingImage") = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        'Anil Changess for issue 12718
        Public Property MembershipImageURL() As String
            Get
                If ViewState.Item("MembershipImageURL") IsNot Nothing Then
                    Return ViewState.Item("MembershipImageURL").ToString()
                Else
                    Return ""
                End If

            End Get
            Set(ByVal value As String)
                ViewState.Item("MembershipImageURL") = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        'end


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
        ''' <summary>
        ''' Rashmi P
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PersonImageURL() As String
            Get
                If ViewState.Item("PersonImageURL") IsNot Nothing Then
                    Return ViewState.Item("PersonImageURL").ToString()
                Else
                    Return ""
                End If

            End Get
            Set(ByVal value As String)
                ViewState.Item("PersonImageURL") = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Property BlankImage() As String
            Get
                Return m_sblankImage
            End Get
            Set(ByVal value As String)
                m_sblankImage = value
            End Set
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

        ''Added By Pradip 2016-07-22 to Save County In Web User Company
        Public Property CompanyCounty() As String
            Get
                If ViewState.Item("CompanyCounty") IsNot Nothing Then
                    Return ViewState.Item("CompanyCounty").ToString()
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState.Item("CompanyCounty") = value
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
        Private Function IsPasswordComplexPopup(ByVal password As String) As Boolean
            Dim result As Boolean = False
            'Aparna for issue 12964 for showing password length validation
            lblerrorLength.Text = ""
            If password.Length >= MinPwdLength Then
                result = System.Text.RegularExpressions.Regex.IsMatch(password, "(?=(.*[A-Z].*){" & MinPwdUpperCase & ",})(?=(.*[a-z].*){" & MinPwdLowerCase & ",})(?=(.*\d.*){" & MinPwdNumbers & ",})")
            End If
            'Aparna for issue 12964 for showing password length validation
            If Not result Then
                'Neha, Issue 12591,03/05/12,Done Changes for showing ErrorMeassage
                lblerrorLength.Text = "<span style='font-weight: bold; color:red; font-size:11px;'>The password criteria has not been met. Please try again.</span> " & "<br/>" &
                "<span style='font-style:italic; font-size: 7pt; font-weight: bold;'>Password must be a minimum length of " & MinPwdLength.ToString & " with at least " & _
                 MinPwdLowerCase & " lower-case letter(s) and " & MinPwdUpperCase & " upper-case letter(s) and " & MinPwdNumbers & " number(s).</span>"
            End If
            Return result
        End Function

        ''' <summary>
        ''' funtion Compare the country name from Social Network to Local DB country name
        ''' then set combo value
        ''' </summary>
        Private Sub SetCountry(ByVal SocialNetworkCountryCode As String)
            Dim dt As DataTable
            Dim dr As Data.DataRow
            Dim sSql As String

            sSql = "SELECT COUNTRY FROM " & AptifyApplication.GetEntityBaseDatabase("Countries") & ".." _
                      & AptifyApplication.GetEntityBaseView("Countries") & " WHERE ISOCODE = '" & SocialNetworkCountryCode & "'"
            dt = DataAction.GetDataTable(sSql)
            If dt IsNot Nothing Then
                dr = dt.Rows.Item(0)
                SetComboValue(cmbCountry, CStr(dr("COUNTRY")))
            End If
        End Sub

        'Anil Add for Issuie 12835 
        Protected Overridable ReadOnly Property ImageUploadUserProfileSaveText() As String
            Get
                If Not Session.Item(ATTRIBUTE_IMAGEUPLOAD_PROFILESAVE_TNCTEXT) Is Nothing Then
                    Return CStr(Session.Item(ATTRIBUTE_IMAGEUPLOAD_PROFILESAVE_TNCTEXT))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_IMAGEUPLOAD_PROFILESAVE_TNCTEXT)
                    If Not String.IsNullOrEmpty(value) Then
                        Session.Item(ATTRIBUTE_IMAGEUPLOAD_PROFILESAVE_TNCTEXT) = value
                        Return value
                    Else
                        Return String.Empty
                    End If
                End If
            End Get

        End Property

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

#Region "Page Events"

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            ''For Dynamic Script loading By Shiwendra
            Dim js As HtmlGenericControl = New HtmlGenericControl("script")
            js.Attributes.Add("type", "text/javascript")
            js.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-1.7.1.min.js"))
            Me.Page.Header.Controls.Add(js)
        End Sub
        'Neha changes for issue 15312, 05/07/13
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            txtFirstName.Focus()
            txtCompany.Attributes.Add("onkeyup", "javascript:onTxtCompanyChange()")

            txtAddressLine1.Text = hidAddressLine1.Value
            txtAddressLine2.Text = hidAddressLine2.Value
            txtAddressLine3.Text = hidAddressLine3.Value
            txtCity.Text = hidCity.Value
            txtZipCode.Text = hidPostalCode.Value
            txtBillingCountry.Text = hidCounty.Value
            'Code added by Govind Mande on 12 May 2016
            regexEmailValid.ValidationExpression = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.EmailRegularExpressionValidator__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            ' End Code
            Me.Page.MaintainScrollPositionOnPostBack = True

            'Anil Changess for issue 12835 
            If IsPostBack AndAlso txtPassword.Text <> String.Empty Then
                txtPassword.Attributes("value") = txtPassword.Text
                txtRepeatPWD.Attributes("value") = txtRepeatPWD.Text
            End If
            'End

            SetProperties()
            m_lEntityID = CLng(AptifyApplication.GetEntityID("Persons"))

            'Javascript to show image when address type is changed...
            Dim imgURL As String
            imgProcessing.Src = ProcessingImage
            imgURL = Replace(tblMain.Parent.UniqueID, "$", "_") & "_" & imgProcessing.ID

            ddlAddressType.Attributes.Add("OnChange", "javascript:ShowImage('" & imgURL & "')")
            ' chkPrefAddress.Attributes.Add("OnClick", "javascript:ShowImage('" & imgURL & "')")

            btnRemovePhoto.Visible = False
            radImageEditor.Enabled = False
            ShowCropButton(False)

            If HiddenField1.Value = "1" Then
                m_bRemovePhoto = True
            End If

            'ImgMembershipe.ImageUrl = MembershipImageURL
            'end
            'By vaishali
            'If User1.UserID > 0 Then
            '    AddOrUpdateTopicCode()
            'End If
            '-----------------------
            AdditionalOrganization() 'Added By Rajesh k 03/14/2014
            If Not IsPostBack Then

                SetSocialNetworkControls()
                'ViewState("ShowImageLableIndicator") = ""
                LoadForm()
                LoadPersonCompanyDetails(CInt(User1.PersonID))
                'Changes By Ganesh On 25/03/2014               
                LoadPendingRecord()
                'Pending changes will not display for new user
                If User1.UserID <= 0 Then
                    divPendingChange.Visible = False
                Else
                    divPendingChange.Visible = True
                End If


                'LoadTopicCodes()
                loadmemberinfo()
                lblerrorLength.Text = ""
                lblPasswordsuccess.Text = ""
                ltlImageEditorStyle.Text = "<style type=""text/css""> #" & radImageEditor.ClientID & "_ToolsPanel { display: none !important; } #" & radwindowProfileImage.ClientID & "_C { overflow: hidden !important; }</style>"
            End If
            If Convert.ToInt32(hidCountry.Value) > 0 Then
                cmbCountry.SelectedValue = hidCountry.Value
                PopulateState(cmbState, cmbCountry)
            End If
            If Convert.ToString(hidState.Value) <> "0" Then
                cmbState.SelectedValue = Convert.ToString(hidState.Value).Trim
            End If
        End Sub

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
            If String.IsNullOrEmpty(ProcessingImage) Then
                ProcessingImage = Me.GetLinkValueFromXML(ATTRIBUTE_PROCESSING_IMAGE_URL)
            End If
            'Anil Changess for issue 12718
            If String.IsNullOrEmpty(MembershipImageURL) Then
                MembershipImageURL = Me.GetLinkValueFromXML(ATTRIBUTE_MEMBERSHIP_IMAGE_URL)
            End If
            'end
            If String.IsNullOrEmpty(PersonImageURL) Then
                PersonImageURL = Me.GetLinkValueFromXML(ATTRIBUTE_PERSON_IMAGE_URL)
            End If
            If String.IsNullOrEmpty(BlankImage) Then
                BlankImage = Me.GetPropertyValueFromXML(ATTRIBUTE_PERSON_BLANK_IMG)
            End If

            'Anil Add for issue 12835
            If Not String.IsNullOrEmpty(ImageUploadUserProfileText) Then
                LableImageUploadText.Text = ImageUploadUserProfileText
            End If

            If Not String.IsNullOrEmpty(ImageUploadUserProfileSaveText) Then
                LableImageSaveIndicator.Text = ImageUploadUserProfileSaveText
            End If

            If ImageWidth = -1 Then
                Dim sWidth As String = ""
                sWidth = Me.GetPropertyValueFromXML(ATTRIBUTE_PERSON_IMG_WIDTH)
                If IsNumeric(sWidth) Then
                    ImageWidth = CInt(sWidth)
                End If
            End If
            If ImageHeight = -1 Then
                Dim sHeight As String = ""
                sHeight = Me.GetPropertyValueFromXML(ATTRIBUTE_PERSON_IMG_HEIGHT)
                If IsNumeric(sHeight) Then
                    ImageHeight = CInt(sHeight)
                End If
            End If
            'end
            ''ISSUEID 3240 - Added SuvarnaD  
            If String.IsNullOrEmpty(DuplicatePersonValidation) Then
                DuplicatePersonValidation = Me.GetPropertyValueFromXML(ATTRIBUTE_PROFILE_ALERT_DUPLICATEPERSONVALIDATION)
                If Not String.IsNullOrEmpty(DuplicatePersonValidation) Then
                    lblAlert.Text = DuplicatePersonValidation
                End If
            End If

            ''Added By Pradip 2016-03-21 For Issue 5507
            If String.IsNullOrEmpty(MembershipPage) Then
                MembershipPage = Me.GetLinkValueFromXML(ATTRIBUTE_MEMBERSHIP_Page_URL)
            End If
        End Sub
        Protected Overridable Sub SetSocialNetworkControls()
            'Anil Changess for Issue 12835
            If SocialNetworkObject IsNot Nothing AndAlso SocialNetworkObject.IsConnected AndAlso SocialNetworkObject.UserProfile.SynchronizeProfile Then
                LoadSocialNetworkProfilePhoto()

            End If
        End Sub

#End Region

#Region "Load Data Methods"

        Private Sub LoadPendingRecord()
            'LoadPendingChanges() 

            ' PendingChangesDetails1.EntityID = AptifyApplication.GetEntityID("Persons")
            PendingChangesDetails1.EntityName = "Persons"
            PendingChangesDetails1.RecordID = CType(User1.PersonID, Long)
            PendingChangesDetails1.PendingChangeType = "Approval Request"
            PendingChangesDetails1.LoadPendingChanges(PendingChangesDetails1.EntityName, PendingChangesDetails1.RecordID, PendingChangesDetails1.PendingChangeType)

        End Sub

        Private Sub LoadForm()
            Try
                PopulateDropDowns()

                If User1.UserID > 0 Then
                    '  lblPageTitle.Text = "Edit Profile"
                    'lblProfileTitle.Text = "Edit User Profile"
                    'lnkCheckAvailable.Visible = False


                    LoadUserInfo()
                    trWebAccount.Visible = False
                    trUserID.Visible = True
                    valPWDMatch.Enabled = False
                    valPWDRequired.EnableClientScript = False
                    lblPWD.Visible = False
                    lblRepeatPWD.Visible = False
                    txtPassword.Visible = False
                    txtRepeatPWD.Visible = False
                    lblPasswordHintQuestion.Visible = False
                    lblPasswordHintAnswer.Visible = False
                    cmbPasswordQuestion.Visible = False
                    txtPasswordHintAnswer.Visible = False
                    valPasswordHintRequired.EnableClientScript = False
                    lblPendingChangesMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Profile.PendingChangesMessage")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

                ElseIf SocialNetworkObject IsNot Nothing AndAlso SocialNetworkObject.IsConnected Then
                    LoadSocialNetworkProfile()
                Else
                    '  lblPageTitle.Text = "New User Signup"
                    ' example of page-level default values
                    'Commented out default values for country
                    SetComboValue(cmbCountry, "Ireland") 'Modified by Kavita Zinage 13/05/2016 - Changed  Country ("United States" To "Ireland")
                    PopulateState(cmbState, cmbCountry) '11/27/07,Added by Tamasa,Issue 5222.
                    SetComboValue(cmbState, "LN")
                    'lblProfileTitle.Text = "New User Profile"
                    trWebAccount.Visible = True
                    trUserID.Visible = False

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
                ''IssueID -15385 - Condition added to remove "Company Administrator" function from drop down and also added <Select a Function> item to dropdown
                sSQL = AptifyApplication.GetEntityBaseDatabase("Functions") & "..spGetPersonActiveJobTitle__c"

                cmbJobTitle.DataSource = DataAction.GetDataTable(sSQL)
                cmbJobTitle.DataTextField = "Name"
                cmbJobTitle.DataValueField = "Name"
                cmbJobTitle.DataBind()

                cmbJobTitle11.DataSource = DataAction.GetDataTable(sSQL)
                cmbJobTitle11.DataTextField = "Name"
                cmbJobTitle11.DataValueField = "Name"
                cmbJobTitle11.DataBind()


                sSQL = AptifyApplication.GetEntityBaseDatabase("Addresses") & _
                       "..spGetCountryList__c"
                dt = DataAction.GetDataTable(sSQL)
                cmbCountry.DataSource = dt
                cmbCountry.DataTextField = "Country"
                cmbCountry.DataValueField = "ID"
                cmbCountry.DataBind()
                'Govind Mande added on 16 May 2016
                cmbCountry.Items.Insert(0, "Select Country")

                cmbCountryNew.DataSource = dt
                cmbCountryNew.DataTextField = "Country"
                cmbCountryNew.DataValueField = "ID"
                cmbCountryNew.DataBind()
                cmbCountryNew.Items.Insert(0, "Select Country")
                SetComboValue(cmbCountryNew, "Ireland")
                PopulateState(cmbStateNew, cmbCountryNew)


                ''Added BY Pradip 2016-09-22
                ddlAlternateCmpCountry.DataSource = dt
                ddlAlternateCmpCountry.DataTextField = "Country"
                ddlAlternateCmpCountry.DataValueField = "ID"
                ddlAlternateCmpCountry.DataBind()
                ddlAlternateCmpCountry.Items.Insert(0, "Select Country")
                SetComboValue(ddlAlternateCmpCountry, "Ireland")
                PopulateState(ddlAlternateCmpState, ddlAlternateCmpCountry)


                cmbHomeCountry.DataSource = dt
                cmbHomeCountry.DataTextField = "Country"
                cmbHomeCountry.DataValueField = "ID"
                cmbHomeCountry.DataBind()
                '               'Govind Mande added on 16 May 2016
                cmbHomeCountry.Items.Insert(0, "Select Country")

                cmbBillingCountry.DataSource = dt
                cmbBillingCountry.DataTextField = "Country"
                cmbBillingCountry.DataValueField = "ID"
                cmbBillingCountry.DataBind()
                'Govind Mande added on 16 May 2016
                cmbBillingCountry.Items.Insert(0, "Select Country")

                cmbPOBoxCountry.DataSource = dt
                cmbPOBoxCountry.DataTextField = "Country"
                cmbPOBoxCountry.DataValueField = "ID"
                cmbPOBoxCountry.DataBind()
                '             'Govind Mande added on 16 May 2016
                cmbPOBoxCountry.Items.Insert(0, "Select Country")
                '------------Added by asmita
                sSQL = AptifyApplication.GetEntityBaseDatabase("EmploymentStatus__c") & _
                 "..SpGetEmploymentStatusProfile__c"
                '   dt = DataAction.GetDataTable(sSQL)
                cmbempstatus.DataSource = DataAction.GetDataTable(sSQL)
                cmbempstatus.DataTextField = "Name"
                cmbempstatus.DataValueField = "ID"
                cmbempstatus.DataBind()
                '------------end  by asmita
                ' code added by Saurabh 6feb
                sSQL = AptifyApplication.GetEntityBaseDatabase("Prefix") & _
                    "..spGetPrefixes"

                Dim dtPrefix As New DataTable
                dtPrefix = DataAction.GetDataTable(sSQL)
                Dim R As DataRow = dtPrefix.NewRow
                R("Prefix") = ""
                dtPrefix.Rows.InsertAt(R, 0)
                cmbSalutation.DataSource = dtPrefix
                cmbSalutation.DataTextField = "Prefix"
                cmbSalutation.DataValueField = "Prefix"
                cmbSalutation.DataBind()
                ' code added by Saurabh 6feb

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadUserInfo()
            Try
                ''Asmita 
                ' txtPractice.Text = User1.GetValue("Practice__c")
                Dim oPerson As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                With oPerson
                    txtFirstName.Text = CStr(.GetValue("FirstName"))
                    txtLastName.Text = CStr(.GetValue("LastName"))
                    If Not String.IsNullOrEmpty(Convert.ToString(.GetValue("Title"))) Then
                        cmbJobTitle.SelectedValue = Convert.ToString(.GetValue("Title"))
                    End If
                    txtEmail.Text = CStr(.GetValue("Email"))
                    Try
                        If Not String.IsNullOrEmpty(Convert.ToString(.GetValue("Birthday"))) AndAlso Convert.ToDateTime(.GetValue("Birthday")) <> New System.DateTime(1777, 1, 1) Then
                            ' OrElse Convert.ToString(.GetValue("Birthday")) <> "1/1/1900") Then
                            txtDob.SelectedDate = CDate(.GetValue("Birthday"))
                        End If
                    Catch ex As Exception
                    End Try
                    'Author : Milind Sutar 
                    'Issue  : User session issue
                    hfCompanyID.Value = Convert.ToString(.GetValue("CompanyID"))
                    txtCompany.Text = CStr(.GetValue("Company"))
                    'txtCompany.Text = User1.GetValue("Company")
                    'txtPractice.Text = Convert.ToString(.GetValue("Practice__c"))
                    'Code added by Govind Mande 16 May 2016
                    txtIntlCode.Text = Convert.ToString(.GetValue("PhoneCountryCode"))

                    txtPhoneAreaCode.Text = Convert.ToString(.GetValue("PhoneAreaCode"))
                    txtPhone.Text = Convert.ToString(.GetValue("Phone"))
                    txtFaxAreaCode.Text = Convert.ToString(.GetValue("FaxAreaCode"))
                    txtFaxPhone.Text = Convert.ToString(.GetValue("FaxPhone"))


                    If Convert.ToString(oPerson.GetValue("PreferredAddress")) = "Home Address" Then
                        rblPAddress.SelectedValue = "Home Address"
                    ElseIf Convert.ToString(oPerson.GetValue("PreferredAddress")) = "Business Address" Then
                        rblPAddress.SelectedValue = "Business Address"

                    End If
                    'End
                End With
                'Asmita end '

                'Code added by Saurabh 6 feb
                txtOtherName.Text = Convert.ToString(oPerson.GetValue("OtherName__c"))
                txtPreferredSalutation.Text = Convert.ToString(oPerson.GetValue("PreferredSalutation__c"))
                If Not String.IsNullOrEmpty(Convert.ToString(oPerson.GetValue("Designation__c"))) Then
                    lblmembershipGradeval.Text = Convert.ToString(oPerson.GetValue("Designation__c"))
                Else
                    lblmembershipGradeval.Text = "N/A"
                End If
                'Code added by Govind 6 july
                Dim sSQL As String = Database & "..spGetPersonIsMember__c @MemberTypeID=" & Convert.ToInt32(oPerson.GetValue("MemberTypeID"))
                Dim iMemberTypeID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If iMemberTypeID > 0 Then
                    If Not String.IsNullOrEmpty(Convert.ToString(oPerson.GetValue("OldID"))) Then
                        lblMembershipNumber.Text = Convert.ToString(oPerson.GetValue("OldID"))
                    Else
                        'lblMembershipNumber.Text = "N/A"
                    End If
                End If
                cmbGender.SelectedValue = Convert.ToString(oPerson.GetValue("Gender"))
                'SetComboValue(cmbGender, Convert.ToString(User1.GetValue("Gender")))
                SetComboValue(cmbSalutation, Convert.ToString(oPerson.GetValue("Prefix")))


                'Load Address Info
                LoadAddresses()
                SetComboValue(ddlAddressType, User1.GetValue("PreferredAddress"))
                'DisplayAddress(User1.GetValue("PreferredAddress"))
                DisplayAddress("Home Address")
                Me.PreferredAddress = User1.GetValue("PreferredAddress")

                If txtPhoneAreaCode.Text.Trim = "" Then
                    txtPhoneAreaCode.Text = User1.GetValue("PhoneAreaCode")
                End If

                If txtPhone.Text.Trim = "" Then
                    txtPhone.Text = User1.GetValue("Phone")
                End If
                If txtFaxAreaCode.Text.Trim = "" Then
                    txtFaxAreaCode.Text = User1.GetValue("FaxAreaCode")
                End If
                txtFaxPhone.Text = User1.GetValue("FaxPhone")

                txtUserID.Text = User1.WebUserStringID
                lblUserID.Text = User1.WebUserStringID

                txtUserID.Enabled = False
                txtPassword.Text = User1.Password
                txtRepeatPWD.Text = txtPassword.Text

                ''IssueID -15385 - Condition added to remove "Company Administrator" function from drop down and also added <Select a Function> item to dropdown
                ''Following cluse is added to display the <Select a fucntion> in dropdown when in smart client primary function of the user is "Company Administrator" 
                'If String.Compare(User1.GetValue("PrimaryFunctionID"), "Company Administrator", True) = 0 Then
                '    SetComboValue(cmbPrimaryFunction, "-1")
                'Else
                '    SetComboValue(cmbPrimaryFunction, User1.GetValue("PrimaryFunctionID"))
                'End If

                '------------Added by asmita
                ' cmbempstatus.SelectedValue = User1.GetValue("EmploymentStatusID__c")
                SetComboValue(cmbempstatus, Convert.ToString(oPerson.GetValue("EmploymentStatusID__c")))
                '--------------------end 
                'Code added by Saurabh 6 feb
                ''RashmiP, Load user's picture
                'LoadProfilePicture(Nothing)

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' RashmiP, Date: 05/26/2011, Issue 11265:Show Photo on Profile Page and Allow Users to Upload Photos
        ''' </summary>
        ''' <remarks></remarks>
        'Private Sub LoadProfilePicture(ByVal ImageName As String)
        '    Dim sSQL As String = ""
        '    Dim dt As DataTable
        '    Dim sBaseURL As String
        '    m_lRecordID = User1.PersonID.ToString
        '    sSQL = "Select Photo from vwPersons Where ID = " & m_lRecordID

        '    dt = DataAction.GetDataTable(sSQL)
        '    ' If Not dt Is Nothing Then
        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso SocialNetworkObject.UserProfile.UseSocialMediaPhoto = False Then
        '        If Not IsDBNull(dt.Rows(0)("Photo")) Then
        '            'Nalini 11265:Added the delete functionality of last profile picture of that specific person and added Guid for the unique name of the profile image
        '            DeleteDownloadedPhotos()
        '            GetNewProfilePhotoFileName()
        '            Dim uniqueGuid As String = System.Guid.NewGuid.ToString
        '            Dim ImagePath As String = ProfilePhotoMapPath & ProfilePhotoFileName
        '            Dim newImgData() As Byte
        '            ImageData = DirectCast(dt.Rows(0)("Photo"), [Byte]())
        '            If ImageData.Length > 0 Then
        '                Dim client As New System.Net.WebClient
        '                client.UploadData(ImagePath, "POST", ImageData)
        '                'Anil Bisen issue 12835 
        '                UpdateImageSize(ImageData)



        '                'ViewState("RefreshImageURL") = imgProfile.ImageUrl
        '                'ViewState("ShowImageLableIndicator") = imgProfile.ImageUrl
        '                newImgData = ConvertImagetoByte(ImagePath, True)
        '                If CompareTwoImageBytes(newImgData) Then
        '                    btnRemovePhoto.Visible = False
        '                    radImageEditor.Enabled = False
        '                    ShowCropButton(False)
        '                Else
        '                    btnRemovePhoto.Visible = True
        '                    radImageEditor.Enabled = True
        '                End If
        '            Else
        '                SetBlankImage()
        '            End If
        '        Else
        '            SetBlankImage()
        '        End If
        '    Else
        '        SetBlankImage()
        '    End If
        '    'Anil End

        'End Sub
        'Anil Bisen issue 12835 
        Protected Overridable Sub SetBlankImage()

            'radImageEditor.ImageUrl = PersonImageURL & BlankImage
            'ViewState(ATTRIBUTE_PROFILE_PHOTO_FILENAME) = BlankImage

            'btnRemovePhoto.Visible = False
            'radImageEditor.Enabled = False
            'ShowCropButton(False)
        End Sub
        Protected Overridable Function UpdateImageSize(ByRef ImageByte As Byte()) As Boolean
            Try
                Dim NewImgeWidth As Integer
                Dim originalImage As System.Drawing.Image
                If ImageByte IsNot Nothing AndAlso ImageByte.Length > 0 Then

                    ' convert imageByte to virtual Image
                    Dim sMemstrm As MemoryStream = New MemoryStream(ImageByte)
                    If sMemstrm IsNot Nothing Then
                        originalImage = System.Drawing.Image.FromStream(sMemstrm)
                        If originalImage IsNot Nothing Then
                            Dim lVirtualImageHeight As Long
                            Dim lVirtualImageWidth As Long
                            lVirtualImageHeight = originalImage.Height  'original image height
                            lVirtualImageWidth = originalImage.Width
                            NewImgeWidth = originalImage.Width
                            If ImageWidth = originalImage.Width Then
                                NewImgeWidth = originalImage.Width - 1
                            End If
                            'original image width 
                            Dim commonMethods As New Aptify.Framework.Web.eBusiness.CommonMethods()
                            Dim aspratio As AspectRatio = New AspectRatio()
                            aspratio.WidthAndHeight(NewImgeWidth, originalImage.Height, ImageWidth, ImageHeight)
                            originalImage = commonMethods.byteArrayToImage(ImageByte)
                            ImageByte = commonMethods.resizeImageAndGetAsByte(originalImage, aspratio.Width, ImageHeight)
                            'If aspratio.Width = 0 Then
                            '    aspratio.Width = originalImage.Width
                            'End If
                            'If originalImage.Height < ImageHeight AndAlso originalImage.Width < ImageWidth Then

                        End If
                    End If
                End If
                Return True
            Catch ex As Exception
                ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try

        End Function
        'end


        ''' <summary>
        ''' Rashmi P, Convert Image into bytes 
        ''' </summary>
        ''' <param name="spath"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ConvertImagetoByte(ByVal spath As String, ByVal bIspageLoad As Boolean) As Byte()
            Try
                Dim sFile As String
                sFile = spath
                Dim fInfo As New FileInfo(sFile)
                If fInfo.Exists Then
                    Dim len As Long = fInfo.Length
                    Dim imgData() As Byte
                    Using Stream As New FileStream(sFile, FileMode.Open)
                        imgData = New Byte(Convert.ToInt32(len - 1)) {}
                        Stream.Read(imgData, 0, CInt(len))
                    End Using
                    ' User1.PersonPhoto = imgData
                    If IsPostBack Then
                        UpdateImageSize(imgData)
                    End If
                    Return imgData
                Else
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New Exception(sFile & " File does not exists."))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Function
        ''RashmiP, Compare two byte
        Private Function CompareTwoImageBytes(ByVal newImage As Byte()) As Boolean
            Try
                Dim blankimg As Byte()
                Dim strblankimg As String
                strblankimg = System.Web.HttpContext.Current.Server.MapPath(PersonImageURL) & BlankImage
                blankimg = ConvertImagetoByte(strblankimg, False)
                Dim bCompare As Boolean = True

                For i As Integer = 0 To newImage.Length - 1
                    If newImage(i) <> blankimg(i) Then
                        Return False
                    End If
                Next
                Return bCompare
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Function

        Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList, _
                                  ByVal sValue As String)
            Dim i As Integer

            Try
                For i = 0 To cmb.Items.Count - 1
                    If String.Compare(cmb.Items(i).Value, sValue, True) = 0 Then
                        cmb.Items(i).Selected = True
                        Exit Sub
                    End If
                    '11/27/07,Added by Tamasa,Issue 5222.
                    If String.Compare(cmb.Items(i).Text, sValue, True) = 0 Then
                        cmb.Items(i).Selected = True
                        Exit Sub
                    End If
                    'End
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overridable Sub LoadSocialNetworkProfilePhoto()
            If SocialNetworkObject.UserProfile IsNot Nothing Then
                With SocialNetworkObject.UserProfile
                    Dim spath1 As [String] = System.Web.HttpContext.Current.Server.MapPath(PersonImageURL)
                    If .PictureURL IsNot Nothing Then
                        'Navin Prasad Issue 10258
                        If .UseSocialMediaPhoto = True Then

                            'Anil Issue 10258
                            btnRemovePhoto.Visible = True

                            If Not .SynchronizeProfile AndAlso WebUserLogin1.User.UserID >= 0 Then
                                LableImageSaveIndicator.Visible = True
                            End If

                            DownloadSocialMediaPhoto(.PictureURL)
                        Else

                            btnRemovePhoto.Visible = False
                        End If
                    Else

                        btnRemovePhoto.Visible = False
                    End If
                End With
            End If
        End Sub


        'RashmiP Load data from LinkedIn profile.
        Private Sub LoadSocialNetworkProfile()
            Try

                m_lRecordID = User1.PersonID.ToString

                If SocialNetworkObject.IsConnected AndAlso SocialNetworkObject.UserProfile IsNot Nothing Then
                    With SocialNetworkObject.UserProfile
                        txtFirstName.Text = .FirstName
                        txtLastName.Text = .LastName
                        txtCompany.Text = .CurrentCompany
                        If Trim(Convert.ToString(.Title)) <> "" Then
                            cmbJobTitle.SelectedValue = .Title
                        Else
                            cmbJobTitle.SelectedValue = .Headline
                        End If
                        txtAddressLine1.Text = .Location.Street
                        txtCity.Text = .Location.City
                        txtZipCode.Text = .Location.ZipCode
                        txtUserID.Text = .EBusinessUser.WebUserStringID
                        LoadSocialNetworkProfilePhoto()
                        txtRepeatPWD.Text = txtPassword.Text
                        SetCountry(.Location.Country)
                        If cmbCountry.Text <> "" Then
                            PopulateState(cmbState, cmbCountry)
                        End If
                        SetComboValue(cmbState, .Location.State)
                    End With
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub RemovePersonsPhoto()
            Try
                Dim fileName As [String] = ""
                Dim sFile As [String] = ""
                If SocialNetworkObject IsNot Nothing Then
                    fileName = m_lEntityID.ToString() & "_" & SocialNetworkObject.UserProfile.ProfileID & ".jpg"
                    Dim spath1 As [String] = System.Web.HttpContext.Current.Server.MapPath(PersonImageURL)
                    sFile = spath1 + fileName
                    If File.Exists(sFile) Then
                        File.Delete(sFile)
                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub LoadTopicCodes()
            Try
                'Aparna issue 15141,14496 
                Dim sSQL As String = "Select TC.ID, TC.ParentID, TC.Name, TC.WebEnabled FROM " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " TC inner join " + AptifyApplication.GetEntityBaseDatabase("TopicCodeEntities") + ".." + AptifyApplication.GetEntityBaseView("TopicCodeEntities") + " TCE on TC.ID = TCE.TopicCodeID Where ISNULL(TC.ParentID,-1)=-1 AND TC.Status='Active' AND TCE.EntityID_Name='" & m_sEntityName & "' AND TCE.Status='Active' AND TCE.WebEnabled=1"
                Dim dt As DataTable = DataAction.GetDataTable(sSQL)
                If dt.Rows.Count > 0 Then
                    Dim litem As System.Web.UI.WebControls.ListItem
                    For Each dtRow As DataRow In dt.Rows
                        litem = New System.Web.UI.WebControls.ListItem
                        litem.Text = dtRow("Name").ToString
                        litem.Value = dtRow("ID").ToString
                        cblTopicofInterest.Items.Add(litem)
                    Next
                End If
                Dim icount As Long = 0
                Dim oLink As AptifyGenericEntityBase
                For Each itm As System.Web.UI.WebControls.ListItem In cblTopicofInterest.Items
                    With itm
                        sSQL = GetNodeCheckSQL(CLng(.Value))
                        icount = CInt(DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If icount > 0 Then
                            sSQL = GetNodeLinkSQL(CLng(.Value))
                            Dim lLinkID As Long = CLng(DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                            oLink = AptifyApplication.GetEntityObject("Topic Code Links", lLinkID)
                            If oLink IsNot Nothing And oLink.GetValue("Status") IsNot Nothing Then
                                itm.Selected = CBool(IIf(CStr(oLink.GetValue("Status")) = "Active", True, False))
                            End If
                        End If
                    End With
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region

#Region "Save Methods"

        Private Function DoSave() As Boolean
            ' This function will update the user information by passing
            ' in the data to the User object and requesting it to save
            'Dim bAddressChanged As Boolean = False

            Try
                'Navin Prasad Issue 12341
                Dim bNewUser As Boolean = False
                bNewUser = User1.UserID <= 0
                User1.SetValue("FirstName", txtFirstName.Text)
                User1.SetValue("LastName", txtLastName.Text)
                If cmbJobTitle.SelectedItem.Text <> "Select Job Title" Then
                    User1.SetValue("Title", cmbJobTitle.SelectedItem.Text)
                End If
                User1.SetValue("Email", txtEmail.Text)
                User1.SetAddValue("Email1", txtEmail.Text)
                ' User1.SetValue("Company", txtCompany.Text)

                SetUserPersonPhoto(ProfilePhotoMapPath & ProfilePhotoFileName)
                'If String.Compare(txtAddressLine1.Text, User1.GetValue("AddressLine1"), True) <> 0 OrElse _
                '        String.Compare(txtAddressLine2.Text, User1.GetValue("AddressLine2"), True) <> 0 OrElse _
                '        String.Compare(txtAddressLine3.Text, User1.GetValue("AddressLine3"), True) <> 0 OrElse _
                '        String.Compare(txtCity.Text, User1.GetValue("City"), True) <> 0 OrElse _
                '        String.Compare(CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")), User1.GetValue("State"), True) <> 0 OrElse _
                '        String.Compare(txtZipCode.Text, User1.GetValue("ZipCode"), True) <> 0 OrElse _
                '        String.Compare(CStr(IIf(cmbCountry.SelectedIndex >= 0, cmbCountry.SelectedItem.Text, "")), User1.GetValue("Country"), True) <> 0 Then
                '    bAddressChanged = True
                'End If

                ''RashmiP, Issue 5051, 8/3/2011. Set City, State, County according to Postal code.
                'Navin Prasad Issue 5051
                Dim sCounty As String = ""
                '02/09/2016 - commented as we don't need to update the business address
                'Me.DoPostalCodeLookup(txtZipCode.Text, CStr(IIf(cmbCountry.SelectedIndex >= 0, cmbCountry.SelectedValue, "")), sCounty, txtCity, cmbState)
                'User1.SetValue("AddressLine1", txtAddressLine1.Text)
                'User1.SetValue("AddressLine2", txtAddressLine2.Text)
                'User1.SetValue("AddressLine3", txtAddressLine3.Text)
                'User1.SetValue("City", txtCity.Text)
                'User1.SetValue("State", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedValue, "")))
                'User1.SetValue("ZipCode", txtZipCode.Text)
                'User1.SetValue("CountryCodeID", CLng(IIf(cmbCountry.SelectedIndex >= 0, cmbCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                'User1.SetValue("Country", CStr(IIf(cmbCountry.SelectedIndex >= 0, cmbCountry.SelectedValue, "")))

                'User1.SetAddValue("County", sCounty)
                'Navin Prasad Issue 5051
                sCounty = ""
                Me.DoPostalCodeLookup(txtHomeZipCode.Text, CStr(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, "")), sCounty, txtHomeCity, cmbHomeState)

                User1.SetValue("HomeAddressLine1", txtHomeAddressLine1.Text)
                User1.SetValue("HomeAddressLine2", txtHomeAddressLine2.Text)
                User1.SetValue("HomeAddressLine3", txtHomeAddressLine3.Text)
                User1.SetValue("HomeCity", txtHomeCity.Text)
                User1.SetValue("HomeState", CStr(IIf(cmbHomeState.SelectedIndex >= 0, cmbHomeState.SelectedValue, "")))
                User1.SetValue("HomeZipCode", txtHomeZipCode.Text)
                User1.SetValue("HomeCountryCodeID", CLng(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                User1.SetValue("HomeCountry", CStr(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, "")))
                User1.SetAddValue("HomeCounty", sCounty)
                User1.SetValue("HomeCounty", txtHomeCounty.Text)
                'Navin Prasad Issue 5051
                sCounty = ""
                Me.DoPostalCodeLookup(txtBillingZipCode.Text, CStr(IIf(cmbBillingCountry.SelectedIndex >= 0, cmbBillingCountry.SelectedValue, "")), sCounty, txtBillingCity, cmbBillingState)
                User1.SetValue("BillingAddressLine1", txtBillingAddressLine1.Text)
                User1.SetValue("BillingAddressLine2", txtBillingAddressLine2.Text)
                User1.SetValue("BillingAddressLine3", txtBillingAddressLine3.Text)
                User1.SetValue("BillingCity", txtBillingCity.Text)
                User1.SetValue("BillingState", CStr(IIf(cmbBillingState.SelectedIndex >= 0, cmbBillingState.SelectedValue, "")))
                User1.SetValue("BillingZipCode", txtBillingZipCode.Text)
                User1.SetValue("BillingCountryCodeID", CLng(IIf(cmbBillingCountry.SelectedIndex >= 0, cmbBillingCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                User1.SetValue("BillingCountry", CStr(IIf(cmbBillingCountry.SelectedIndex >= 0, cmbBillingCountry.SelectedValue, "")))
                User1.SetAddValue("BillingCounty", txtBillingCountry.Text)

                'Navin Prasad Issue 5051
                sCounty = ""
                Me.DoPostalCodeLookup(txtPOBoxZipCode.Text, CStr(IIf(cmbPOBoxCountry.SelectedIndex >= 0, cmbPOBoxCountry.SelectedValue, "")), sCounty, txtPOBoxCity, cmbPOBoxState)
                User1.SetValue("POBox", txtPOBoxAddressLine1.Text)
                User1.SetValue("POBoxLine2", txtPOBoxAddressLine2.Text)
                User1.SetValue("POBoxLine3", txtPOBoxAddressLine3.Text)
                User1.SetValue("POBoxCity", txtPOBoxCity.Text)
                User1.SetValue("POBoxState", CStr(IIf(cmbPOBoxState.SelectedIndex >= 0, cmbPOBoxState.SelectedValue, "")))
                User1.SetValue("POBoxZipCode", txtPOBoxZipCode.Text)
                User1.SetValue("POBoxCountryCodeID", CLng(IIf(cmbPOBoxCountry.SelectedIndex >= 0, cmbPOBoxCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                User1.SetValue("POBoxCountry", CStr(IIf(cmbPOBoxCountry.SelectedIndex >= 0, cmbPOBoxCountry.SelectedValue, "")))
                User1.SetAddValue("POBoxCounty", sCounty)

                'User1.SetValue("PreferredAddress", Me.PreferredAddress)
                'User1.SetValue("PreferredBillingAddress", Me.PreferredAddress)
                'User1.SetValue("PreferredShippingAddress", Me.PreferredAddress)
                'Sheetal Issue 21545
                If Me.PreferredAddress <> "" Then
                    User1.SetValue("PreferredAddress", Me.PreferredAddress)
                    User1.SetValue("PreferredBillingAddress", Me.PreferredAddress)
                    User1.SetValue("PreferredShippingAddress", Me.PreferredAddress)
                End If
                User1.SetValue("PhoneAreaCode", txtPhoneAreaCode.Text)
                User1.SetValue("Phone", txtPhone.TextWithLiterals)
                User1.SetValue("FaxAreaCode", txtFaxAreaCode.Text)
                User1.SetValue("FaxPhone", txtFaxPhone.TextWithLiterals)
                'Code added by Govind Mande 16 May 2016
                User1.SetValue("PhoneCountryCode", txtIntlCode.Text)
                ' End Code
                'User1.SetValue("WebCompanyName__c", Session("WebcompanyName").ToString)

                'User1.SetValue("Photo", )
                'CPirisino: If no functions are defined then don't try to save primary function

                ' code added by Saurabh for profile changes 6-2-2014

                User1.SetValue("OtherName__c", txtOtherName.Text)
                User1.SetValue("PreferredSalutation__c", txtPreferredSalutation.Text)
                User1.SetValue("Prefix", CStr(IIf(cmbSalutation.SelectedIndex >= 0, cmbSalutation.SelectedValue, "")))
                User1.SetValue("Gender", CStr(IIf(cmbGender.SelectedIndex >= 0, cmbGender.SelectedValue, "")))

                ' code end added by Saurabh for profile changes 6-2-2014
                'If cmbPrimaryFunction.SelectedItem.Value IsNot Nothing Then
                '    User1.SetValue("PrimaryFunctionID", CLng(IIf(cmbPrimaryFunction.SelectedIndex >= 0, cmbPrimaryFunction.SelectedItem.Value, "-1")))
                'End If

                If cmbempstatus.SelectedItem.Value IsNot Nothing Then
                    User1.SetValue("EmploymentStatusID__c", CLng(IIf(cmbempstatus.SelectedIndex >= 0, cmbempstatus.SelectedItem.Value, "-1")))
                End If

                User1.SetValue("WebUserStringID", txtUserID.Text)

                If User1.UserID <= 0 Then
                    User1.SetValue("Password", txtPassword.Text)
                    User1.SetValue("PasswordHint", cmbPasswordQuestion.SelectedItem.Text)
                    User1.SetValue("PasswordHintAnswer", txtPasswordHintAnswer.Text)

                End If
                User1.SaveValuesToSessionObject(Page.Session) ' need explicit call due to page redirect possibilities

                If User1.Save() Then

                    ''Asmita
                    Dim oPerson As AptifyGenericEntityBase
                    oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                    If Not String.IsNullOrEmpty(txtDob.SelectedDate.ToString().Trim()) Then
                        oPerson.SetValue("Birthday", txtDob.SelectedDate.ToString().Trim())

                        'oPerson.Save(False)  Commented by Shital as need to save other value also. 06 Nov 2015
                    End If
                    If cmbJobTitle.SelectedItem.Text <> "Select Job Title" Then
                        oPerson.SetValue("Title", cmbJobTitle.SelectedItem.Text)
                        User1.SetValue("Title", cmbJobTitle.SelectedItem.Text)
                    End If
                    oPerson.SetValue("CompanyID", hfCompanyID.Value)
                    'Added by Shital as need to save Status as pending if new user 06 Nov 2015
                    oPerson.SetValue("Status__c", "Pending")
                    oPerson.Save(False)

                    'oPerson.SaveAsPendingChange(-1, System.DateTime.Now, "", AptifyGenericEntityBase.PendingChangeUpdateType.Update, -1)
                    SaveWebUsercompany(False)
                    'Asmita end 
                    'RashmiP..LinkedIn Syncronize to PersonExternalAccount
                    Dim sError As String = ""
                    If SocialNetworkObject IsNot Nothing AndAlso SocialNetworkObject.IsConnected Then
                        SocialNetworkObject.UserProfile.EBusinessUser = User1

                        If SocialNetworkObject.UserProfile.SynchronizePersonExternalAccount(sError) Then
                            If bNewUser AndAlso SocialNetworkObject.UserProfile.SynchronizeProfile Then
                                'SKB Issue 12341 12/01/2011
                                Dim sSQL As String = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='Social Network User Profile Synchronization'"
                                Dim oProcessFlowID As Object = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache)
                                If oProcessFlowID IsNot Nothing AndAlso IsNumeric(oProcessFlowID) Then
                                    Dim lProcessFlowID As Long = CLng(oProcessFlowID)
                                    If lProcessFlowID > 0 Then
                                        Dim oContext As New AptifyContext
                                        Dim result As ProcessFlowResult
                                        oContext.Properties.AddProperty("PersonExternalAccountID", SocialNetworkObject.UserProfile.PersonExternalAccountID)
                                        result = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, oContext)
                                        If Not result.IsSuccess Then
                                            ExceptionManagement.ExceptionManager.Publish(New Exception("Unable to synchronize social network profile for user '" & User1.WebUserStringID & "'"))
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                    'AddUpdateTopicCode()

                    '2/4/08 RJK - If the Shopping Cart has started an Order, reset the Address based on the information provided.
                    'If bAddressChanged Then
                    Dim sOrderXML As String
                    If Session.Item(m_c_sPrefix & "OrderXML") IsNot Nothing Then
                        sOrderXML = Session.Item(m_c_sPrefix & "OrderXML").ToString

                        If sOrderXML.Length > 0 Then
                            '20090123 MAS: update the address based on preferred address

                            Dim prefShip As AptifyShoppingCart.PersonAddressType
                            Dim prefBill As AptifyShoppingCart.PersonAddressType

                            Dim UserPrefShip As String = User1.GetValue("PreferredShippingAddress").Trim.ToLower
                            If UserPrefShip.Contains("home") Then
                                prefShip = AptifyShoppingCart.PersonAddressType.Home
                            ElseIf UserPrefShip.Contains("business") Then
                                prefShip = AptifyShoppingCart.PersonAddressType.Main
                            ElseIf UserPrefShip.Contains("billing") Then
                                prefShip = AptifyShoppingCart.PersonAddressType.Billing
                            ElseIf UserPrefShip.Contains("po") Then
                                prefShip = AptifyShoppingCart.PersonAddressType.POBox
                            Else
                                prefShip = AptifyShoppingCart.PersonAddressType.Main
                            End If

                            Dim UserPrefBill As String = User1.GetValue("PreferredBillingAddress").Trim.ToLower
                            If UserPrefBill.Contains("home") Then
                                prefBill = AptifyShoppingCart.PersonAddressType.Home
                            ElseIf UserPrefBill.Contains("business") Then
                                prefBill = AptifyShoppingCart.PersonAddressType.Main
                            ElseIf UserPrefBill.Contains("billing") Then
                                prefBill = AptifyShoppingCart.PersonAddressType.Billing
                            ElseIf UserPrefBill.Contains("po") Then
                                prefBill = AptifyShoppingCart.PersonAddressType.POBox
                            Else
                                prefBill = AptifyShoppingCart.PersonAddressType.Main
                            End If

                            Me.ShoppingCart1.UpdateOrderAddress(AptifyShoppingCart.OrderAddressType.Shipping, prefShip, 0, Me.Session, Me.Application)
                            Me.ShoppingCart1.UpdateOrderAddress(AptifyShoppingCart.OrderAddressType.Billing, prefBill, 0, Me.Session, Me.Application)
                        End If
                    End If
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        '2/7/2014,Added by shubhangi
        Private Function DoSave_Edit() As Boolean
            ' This function will update the user information by passing
            ' in the data to the User object and requesting it to save
            'Dim bAddressChanged As Boolean = False

            Try
                'Navin Prasad Issue 12341
                Dim bNewUser As Boolean = False
                bNewUser = User1.UserID <= 0

                Dim oPerson As AptifyGenericEntityBase
                oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)

                'Amol Bule- Code writen for currency type validation from ebusiness.
                If Convert.ToString(oPerson.GetValue("HomeCountryCodeID")) <> CStr(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, "")) Then

                    Dim stringSql As String = AptifyApplication.GetEntityBaseDatabase("Persons") & "..spGetPreferredCurrencyTypeID__c @PersonID=" & User1.PersonID & ", @CompanyID=" & Convert.ToString(oPerson.GetValue("CompanyID")) '& ",@HomeCountryID =" & Convert.ToString(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, ""))
                    Dim iResult As Integer = Convert.ToInt32(DataAction.ExecuteScalar(stringSql))

                    If iResult > 0 Then

                        If iResult <> Convert.ToInt32(oPerson.GetValue("PreferredCurrencyTypeID")) Then
                            Dim strSql As String = AptifyApplication.GetEntityBaseDatabase("Persons") & "..spGetBalanceTotalForPerson__c @PersonID=" & User1.PersonID
                            Dim iTotal As Decimal = CDec(DataAction.ExecuteScalar(strSql))
                            If iTotal > 0 OrElse iTotal < 0 Then
                                lblErrorHomeCountry.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Consulting.BalanceTotalValidation__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                                Return False
                            End If
                        End If
                    End If
                End If


                Dim sPendingChangesField As String = String.Empty

                oPerson.SetValue("FirstName", txtFirstName.Text)
                oPerson.SetValue("LastName", txtLastName.Text)
                If cmbJobTitle.SelectedItem.Text <> "Select Job Title" Then
                    oPerson.SetValue("Title", cmbJobTitle.SelectedItem.Text)
                End If
                oPerson.SetValue("Email", txtEmail.Text)
                oPerson.SetAddValue("Email1", txtEmail.Text)

                'Dim companyName As String = txtCompany.Text
                'If companyName.Contains("\") And Not String.IsNullOrEmpty(companyName) Then
                '    companyName = companyName.Substring(0, companyName.IndexOf("\"))
                'End If
                'Dim lcompID As Long = AptifyApplication.GetEntityRecordIDFromRecordName("Companies", companyName)
                'oPerson.SetValue("CompanyID", lcompID)

                'Commented By Pradip 2016-09-29 to avoid business address update on person
                'oPerson.SetValue("CompanyID", hfCompanyID.Value)
                'Dim compId As Long
                'If hfCompanyID.Value <> String.Empty Then
                '    compId = CLng(Convert.ToInt32(hfCompanyID.Value))
                '    oPerson.SetValue("CompanyID", compId)
                '    'hfCompanyID.Value = String.Empty
                'End If

                SetUserPersonPhoto(ProfilePhotoMapPath & ProfilePhotoFileName)

                ''RashmiP, Issue 5051, 8/3/2011. Set City, State, County according to Postal code.
                'Navin Prasad Issue 5051
                Dim sCounty As String = String.Empty
                Me.DoPostalCodeLookup(txtZipCode.Text, CStr(IIf(cmbCountry.SelectedIndex >= 0, cmbCountry.SelectedValue, "")), sCounty, txtCity, cmbState)
                'AMol commented for TFS issue 5654
                'oPerson.SetValue("AddressLine1", txtAddressLine1.Text)
                'oPerson.SetValue("AddressLine2", txtAddressLine2.Text)
                'oPerson.SetValue("AddressLine3", txtAddressLine3.Text)
                'oPerson.SetValue("City", txtCity.Text)
                'oPerson.SetValue("State", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedValue, "")))
                'oPerson.SetValue("ZipCode", txtZipCode.Text)
                'oPerson.SetValue("CountryCodeID", CLng(IIf(cmbCountry.SelectedIndex >= 0, cmbCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                'oPerson.SetValue("Country", CStr(IIf(cmbCountry.SelectedIndex >= 0, cmbCountry.SelectedValue, "")))
                'oPerson.SetAddValue("County", sCounty)
                'Navin Prasad Issue 5051
                sCounty = ""
                Me.DoPostalCodeLookup(txtHomeZipCode.Text, CStr(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, "")), sCounty, txtHomeCity, cmbHomeState)

                oPerson.SetValue("HomeAddressLine1", txtHomeAddressLine1.Text)
                oPerson.SetValue("HomeAddressLine2", txtHomeAddressLine2.Text)
                oPerson.SetValue("HomeAddressLine3", txtHomeAddressLine3.Text)
                oPerson.SetValue("HomeCity", txtHomeCity.Text)
                oPerson.SetValue("HomeState", CStr(IIf(cmbHomeState.SelectedIndex >= 0, cmbHomeState.SelectedValue, "")))
                oPerson.SetValue("HomeZipCode", txtHomeZipCode.Text)
                oPerson.SetValue("HomeCountryCodeID", CLng(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                oPerson.SetValue("HomeCountry", CStr(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, "")))
                oPerson.SetAddValue("HomeCounty", sCounty)
                oPerson.SetValue("HomeCounty", txtHomeCounty.Text)
                'Navin Prasad Issue 5051
                sCounty = String.Empty

                Me.DoPostalCodeLookup(txtBillingZipCode.Text, CStr(IIf(cmbBillingCountry.SelectedIndex >= 0, cmbBillingCountry.SelectedValue, "")), sCounty, txtBillingCity, cmbBillingState)
                oPerson.SetValue("BillingAddressLine1", txtBillingAddressLine1.Text)
                oPerson.SetValue("BillingAddressLine2", txtBillingAddressLine2.Text)
                oPerson.SetValue("BillingAddressLine3", txtBillingAddressLine3.Text)
                oPerson.SetValue("BillingCity", txtBillingCity.Text)
                oPerson.SetValue("BillingState", CStr(IIf(cmbBillingState.SelectedIndex >= 0, cmbBillingState.SelectedValue, "")))
                oPerson.SetValue("BillingZipCode", txtBillingZipCode.Text)
                oPerson.SetValue("BillingCountryCodeID", CLng(IIf(cmbBillingCountry.SelectedIndex >= 0, cmbBillingCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                oPerson.SetValue("BillingCountry", CStr(IIf(cmbBillingCountry.SelectedIndex >= 0, cmbBillingCountry.SelectedValue, "")))
                oPerson.SetAddValue("BillingCounty", sCounty)

                'Navin Prasad Issue 5051
                sCounty = ""
                Me.DoPostalCodeLookup(txtPOBoxZipCode.Text, CStr(IIf(cmbPOBoxCountry.SelectedIndex >= 0, cmbPOBoxCountry.SelectedValue, "")), sCounty, txtPOBoxCity, cmbPOBoxState)
                oPerson.SetValue("POBox", txtPOBoxAddressLine1.Text)
                oPerson.SetValue("POBoxLine2", txtPOBoxAddressLine2.Text)
                oPerson.SetValue("POBoxLine3", txtPOBoxAddressLine3.Text)
                oPerson.SetValue("POBoxCity", txtPOBoxCity.Text)
                oPerson.SetValue("POBoxState", CStr(IIf(cmbPOBoxState.SelectedIndex >= 0, cmbPOBoxState.SelectedValue, "")))
                oPerson.SetValue("POBoxZipCode", txtPOBoxZipCode.Text)
                oPerson.SetValue("POBoxCountryCodeID", CLng(IIf(cmbPOBoxCountry.SelectedIndex >= 0, cmbPOBoxCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                oPerson.SetValue("POBoxCountry", CStr(IIf(cmbPOBoxCountry.SelectedIndex >= 0, cmbPOBoxCountry.SelectedValue, "")))
                oPerson.SetAddValue("POBoxCounty", sCounty)


                oPerson.SetValue("PreferredAddress", Me.PreferredAddress)
                oPerson.SetValue("PreferredBillingAddress", Me.PreferredAddress)
                oPerson.SetValue("PreferredShippingAddress", Me.PreferredAddress)
                '               'Code added by Govind Mande 16 May 2016
                oPerson.SetValue("PhoneCountryCode", txtIntlCode.Text)
                'End
                oPerson.SetValue("PhoneAreaCode", txtPhoneAreaCode.Text)
                oPerson.SetValue("Phone", txtPhone.TextWithLiterals)
                oPerson.SetValue("FaxAreaCode", txtFaxAreaCode.Text)
                oPerson.SetValue("FaxPhone", txtFaxPhone.TextWithLiterals)
                'User1.SetValue("WebCompanyName__c", Session("WebcompanyName").ToString)


                ' code added by Saurabh for profile changes 6-2-2014
                oPerson.SetValue("OtherName__c", txtOtherName.Text)
                oPerson.SetValue("PreferredSalutation__c", txtPreferredSalutation.Text)
                oPerson.SetValue("Prefix", CStr(IIf(cmbSalutation.SelectedIndex >= 0, cmbSalutation.SelectedValue, "")))
                oPerson.SetValue("Gender", CStr(IIf(cmbGender.SelectedIndex >= 0, cmbGender.SelectedValue, "")))
                ' oPerson.SetValue("Designation__C", lblmembershipGrade.Text)

                ' code added by Saurabh for profile changes 6-2-2014
                'If cmbPrimaryFunction.SelectedItem.Value IsNot Nothing Then
                '    oPerson.SetValue("PrimaryFunctionID", CLng(IIf(cmbPrimaryFunction.SelectedIndex >= 0, cmbPrimaryFunction.SelectedItem.Value, "-1")))
                '    If Convert.ToInt32(cmbPrimaryFunction.SelectedItem.Value) > 0 Then
                '        With oPerson.SubTypes("PersonFunctions").Add()
                '            .SetValue("FunctionID", oPerson.GetValue("PrimaryFunctionID"))
                '        End With
                '    End If
                'End If

                If cmbempstatus.SelectedItem.Value IsNot Nothing Then
                    ' AndAlso (User1.GetValue("EmploymentStatusID__c") <> String.Empty AndAlso cmbempstatus.SelectedItem.Value <> "-1") Then
                    Dim statusID As Integer = CInt(IIf(cmbempstatus.SelectedIndex >= 0, cmbempstatus.SelectedValue, "-1"))
                    oPerson.SetValue("EmploymentStatusID__c", statusID)
                End If
                If cmbJobTitle.SelectedItem.Text <> "Select Job Title" Then
                    oPerson.SetValue("Title", cmbJobTitle.SelectedItem.Text)
                End If
                'throwing error
                'oPerson.SetValue("WebUserStringID", txtUserID.Text)

                ''Asmita
                If txtDob.SelectedDate IsNot Nothing Then
                    oPerson.SetValue("Birthday", txtDob.SelectedDate)
                End If
                'Asmita End

                If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Persons", "PendingChangesFields__c")) Then
                    sPendingChangesField = CStr(AptifyApplication.GetEntityAttribute("Persons", "PendingChangesFields__c"))
                End If
                Dim aPendingChanges() As String = sPendingChangesField.Split(New Char() {","c})
                Dim isCompanyChanged As Boolean = False
                Dim oPersonOld As AptifyGenericEntityBase
                oPersonOld = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                For i As Integer = 0 To aPendingChanges.Count - 1
                    Dim sFieldName As String = aPendingChanges(i)

                    'Name       : Milind Sutar
                    'Issue ID   : group 1 issue id 50
                    Dim oldValue As String = CStr(oPersonOld.GetValue(sFieldName))
                    Dim newValue As String = CStr(oPerson.GetValue(sFieldName))



                    'If field is phone number remove hypen and then compare
                    If sFieldName = "Phone" Then
                        oldValue = oldValue.Replace("-", String.Empty)
                        newValue = newValue.Replace("-", String.Empty)
                    End If

                    If sFieldName = "CompanyID" Then
                        newValue = hfCompanyID.Value
                    End If
                    If (String.Compare(oldValue, newValue) <> 0) Then
                        Dim oPersonWithPedingChanges As AptifyGenericEntityBase
                        oPersonWithPedingChanges = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                        If sFieldName = "CompanyID" Then
                            oPersonWithPedingChanges.SetValue(sFieldName, newValue)
                        Else
                            oPersonWithPedingChanges.SetValue(sFieldName, oPerson.GetValue(sFieldName))
                        End If

                        Dim bPendingRecordSaved = oPersonWithPedingChanges.SaveAsPendingChange(3, Date.Now(), "changed from web")
                        'Exit For
                        oPerson.SetValue(sFieldName, oPersonOld.GetValue(sFieldName))
                        If sFieldName = "CompanyID" Then
                            oPerson.SetValue(sFieldName, oldValue)
                            'oPerson.SetValue("AddressLine1", CStr(oPersonOld.GetValue("AddressLine1")))
                            'oPerson.SetValue("AddressLine2", CStr(oPersonOld.GetValue("AddressLine2")))
                            'oPerson.SetValue("AddressLine3", CStr(oPersonOld.GetValue("AddressLine3")))
                            'oPerson.SetValue("City", CStr(oPersonOld.GetValue("City")))
                            'oPerson.SetValue("State", CStr(oPersonOld.GetValue("State")))
                            'oPerson.SetValue("ZipCode", CStr(oPersonOld.GetValue("ZipCode")))
                            'If Convert.ToInt32(CStr(oPersonOld.GetValue("CountryCodeID"))) > 0 Then
                            '    oPerson.SetValue("CountryCodeID", CStr(oPersonOld.GetValue("CountryCodeID")))
                            'Else
                            '    oPerson.SetValue("CountryCodeID", cmbHomeCountry.Items.FindByText("Ireland").Value())
                            'End If
                            'oPerson.SetValue("Country", CStr(oPersonOld.GetValue("Country")))
                            'oPerson.SetAddValue("County", CStr(oPersonOld.GetValue("County")))
                            isCompanyChanged = True
                        End If
                    End If
                Next
                oPersonOld = Nothing
                Dim bRecordSaved As Boolean = False

                'Name       : Milind Sutar
                'Issue ID   : group 1 issue id 50

                'User1.SaveValuesToSessionObject(Page.Session) ' need explicit call due to page redirect possibilities
                'If bValueChanged = True Then
                'bRecordSaved = oPerson.SaveAsPendingChange(3, Date.Now(), "changed from web")
                'Else



                Dim sErr As String = String.Empty
                bRecordSaved = oPerson.Save(sErr)
                If bRecordSaved Then
                    SaveWebUsercompany(isCompanyChanged)
                    'RashmiP..LinkedIn Syncronize to PersonExternalAccount
                    Dim sError As String = ""
                    If SocialNetworkObject IsNot Nothing AndAlso SocialNetworkObject.IsConnected Then
                        SocialNetworkObject.UserProfile.EBusinessUser = User1

                        If SocialNetworkObject.UserProfile.SynchronizePersonExternalAccount(sError) Then
                            If bNewUser AndAlso SocialNetworkObject.UserProfile.SynchronizeProfile Then
                                'SKB Issue 12341 12/01/2011
                                Dim sSQL As String = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='Social Network User Profile Synchronization'"
                                Dim oProcessFlowID As Object = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache)
                                If oProcessFlowID IsNot Nothing AndAlso IsNumeric(oProcessFlowID) Then
                                    Dim lProcessFlowID As Long = CLng(oProcessFlowID)
                                    If lProcessFlowID > 0 Then
                                        Dim oContext As New AptifyContext
                                        Dim result As ProcessFlowResult
                                        oContext.Properties.AddProperty("PersonExternalAccountID", SocialNetworkObject.UserProfile.PersonExternalAccountID)
                                        result = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, oContext)
                                        If Not result.IsSuccess Then
                                            ExceptionManagement.ExceptionManager.Publish(New Exception("Unable to synchronize social network profile for user '" & User1.WebUserStringID & "'"))
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                    'AddUpdateTopicCode()

                    '2/4/08 RJK - If the Shopping Cart has started an Order, reset the Address based on the information provided.
                    'If bAddressChanged Then
                    Dim sOrderXML As String
                    If Session.Item(m_c_sPrefix & "OrderXML") IsNot Nothing Then
                        sOrderXML = Session.Item(m_c_sPrefix & "OrderXML").ToString

                        If sOrderXML.Length > 0 Then
                            '20090123 MAS: update the address based on preferred address

                            Dim prefShip As AptifyShoppingCart.PersonAddressType
                            Dim prefBill As AptifyShoppingCart.PersonAddressType

                            Dim UserPrefShip As String = CStr(oPerson.GetValue("PreferredShippingAddress")).Trim.ToLower()
                            If UserPrefShip.Contains("home") Then
                                prefShip = AptifyShoppingCart.PersonAddressType.Home
                            ElseIf UserPrefShip.Contains("business") Then
                                prefShip = AptifyShoppingCart.PersonAddressType.Main
                            ElseIf UserPrefShip.Contains("billing") Then
                                prefShip = AptifyShoppingCart.PersonAddressType.Billing
                            ElseIf UserPrefShip.Contains("po") Then
                                prefShip = AptifyShoppingCart.PersonAddressType.POBox
                            Else
                                prefShip = AptifyShoppingCart.PersonAddressType.Main
                            End If

                            Dim UserPrefBill As String = CStr(User1.GetValue("PreferredBillingAddress")).Trim.ToLower()
                            If UserPrefBill.Contains("home") Then
                                prefBill = AptifyShoppingCart.PersonAddressType.Home
                            ElseIf UserPrefBill.Contains("business") Then
                                prefBill = AptifyShoppingCart.PersonAddressType.Main
                            ElseIf UserPrefBill.Contains("billing") Then
                                prefBill = AptifyShoppingCart.PersonAddressType.Billing
                            ElseIf UserPrefBill.Contains("po") Then
                                prefBill = AptifyShoppingCart.PersonAddressType.POBox
                            Else
                                prefBill = AptifyShoppingCart.PersonAddressType.Main
                            End If
                            Me.ShoppingCart1.UpdateOrderAddress(AptifyShoppingCart.OrderAddressType.Shipping, prefShip, 0, Me.Session, Me.Application)
                            Me.ShoppingCart1.UpdateOrderAddress(AptifyShoppingCart.OrderAddressType.Billing, prefBill, 0, Me.Session, Me.Application)
                        End If
                    End If
                    Return True
                Else
                    Return False
                End If


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
            Return True
        End Function


#End Region

#Region "Address Methods"

        '11/27/07,Added by Tamasa,Issue 5222.
        Private Sub PopulateState(ByRef cmbPopulateState As DropDownList, ByRef cmbCurrentCountry As DropDownList)
            Try
                Dim sSQL As String
                sSQL = Database & "..spGetStateList @CountryID=" & cmbCurrentCountry.SelectedValue.ToString
                cmbPopulateState.DataSource = DataAction.GetDataTable(sSQL)
                cmbPopulateState.DataTextField = "State"
                cmbPopulateState.DataValueField = "State"
                cmbPopulateState.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub cmbCountrynew_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCountryNew.SelectedIndexChanged
            PopulateState(cmbStateNew, cmbCountryNew)

        End Sub

        '11/27/07,Added by Tamasa,Issue 5222.
        Protected Sub cmbCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCountry.SelectedIndexChanged
            PopulateState(cmbState, cmbCountry)
            txtZipCode.Focus()
        End Sub

        '9/22/08, Added by CPirisino for multi address support
        Protected Sub cmbHomeCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHomeCountry.SelectedIndexChanged
            PopulateState(cmbHomeState, cmbHomeCountry)
        End Sub
        Protected Sub cmbBillingCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBillingCountry.SelectedIndexChanged
            PopulateState(cmbBillingState, cmbBillingCountry)
        End Sub
        Protected Sub cmbPOBoxCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPOBoxCountry.SelectedIndexChanged
            PopulateState(cmbPOBoxState, cmbPOBoxCountry)
        End Sub

        Private Sub DisplayAddress(ByVal sAddrType As String)
            Try

                Select Case sAddrType
                    Case "Business Address"

                        'trAddressLine1.Visible = True
                        'trAddressLine2.Visible = True
                        'trAddressLine3.Visible = True
                        'trCity.Visible = True
                        ''trState.Visible = True
                        ''trZipCode.Visible = True
                        'trCountry.Visible = True

                        '*** Uncomment if you want to default address to US. ***
                        If User1.PersonID < 1 Then
                            cmbCountry.ClearSelection()
                            SetComboValue(cmbCountry, "Ireland") 'Modified by Kavita Zinage 13/05/2016 - Changed  Country ("United States" To "Ireland")
                            PopulateState(cmbState, cmbCountry)
                        End If

                        trHomeAddressLine1.Visible = False
                        trHomeAddressLine2.Visible = False
                        trHomeAddressLine3.Visible = False
                        trHomeCity.Visible = False
                        trHomeCountry.Visible = False
                        'trHomeState.Visible = False
                        'trHomeZipCode.Visible = False

                        trBillingAddressLine1.Visible = False
                        trBillingAddressLine2.Visible = False
                        trBillingAddressLine3.Visible = False
                        trBillingCity.Visible = False
                        trBillingCountry.Visible = False
                        'trBillingState.Visible = False
                        'trBillingZipCode.Visible = False

                        trPOBoxAddressLine1.Visible = False
                        trPOBoxAddressLine2.Visible = False
                        trPOBoxAddressLine3.Visible = False
                        trPOBoxCity.Visible = False
                        trPOBoxCountry.Visible = False
                        'trPOBoxState.Visible = False
                        'trPOBoxZipCode.Visible = False

                    Case "Home Address"


                        trHomeAddressLine1.Visible = True
                        trHomeAddressLine2.Visible = True
                        trHomeAddressLine3.Visible = True
                        trHomeCity.Visible = True
                        trHomeCountry.Visible = True
                        'trHomeState.Visible = True
                        'trHomeZipCode.Visible = True

                        '*** Uncomment if you want to default address to US. ***
                        If User1.PersonID < 1 Then
                            cmbHomeCountry.ClearSelection()
                            SetComboValue(cmbHomeCountry, "Ireland") 'Modified by Kavita Zinage 13/05/2016 - Changed  Country ("United States" To "Ireland")
                            PopulateState(cmbHomeState, cmbHomeCountry)
                        End If


                        trBillingAddressLine1.Visible = False
                        trBillingAddressLine2.Visible = False
                        trBillingAddressLine3.Visible = False
                        trBillingCity.Visible = False
                        trBillingCountry.Visible = False
                        'trBillingState.Visible = False
                        'trBillingZipCode.Visible = False

                        trPOBoxAddressLine1.Visible = False
                        trPOBoxAddressLine2.Visible = False
                        trPOBoxAddressLine3.Visible = False
                        trPOBoxCity.Visible = False
                        trPOBoxCountry.Visible = False
                        'trPOBoxState.Visible = False
                        'trPOBoxZipCode.Visible = False
                    Case "Billing Address"


                        trHomeAddressLine1.Visible = False
                        trHomeAddressLine2.Visible = False
                        trHomeAddressLine3.Visible = False
                        trHomeCity.Visible = False
                        trHomeCountry.Visible = False
                        'trHomeState.Visible = False
                        'trHomeZipCode.Visible = False

                        trBillingAddressLine1.Visible = True
                        trBillingAddressLine2.Visible = True
                        trBillingAddressLine3.Visible = True
                        trBillingCity.Visible = True
                        trBillingCountry.Visible = True
                        'trBillingState.Visible = True
                        'trBillingZipCode.Visible = True

                        '*** Uncomment if you want to default address to US. ***
                        If User1.PersonID < 1 Then
                            cmbBillingCountry.ClearSelection()
                            SetComboValue(cmbBillingCountry, "Ireland") 'Modified by Kavita Zinage 13/05/2016 - Changed  Country ("United States" To "Ireland")
                            PopulateState(cmbBillingState, cmbBillingCountry)
                        End If


                        trPOBoxAddressLine1.Visible = False
                        trPOBoxAddressLine2.Visible = False
                        trPOBoxAddressLine3.Visible = False
                        trPOBoxCity.Visible = False
                        trPOBoxCountry.Visible = False
                        'trPOBoxState.Visible = False
                        'trPOBoxZipCode.Visible = False
                    Case "PO Box Address"


                        trHomeAddressLine1.Visible = False
                        trHomeAddressLine2.Visible = False
                        trHomeAddressLine3.Visible = False
                        trHomeCity.Visible = False
                        trHomeCountry.Visible = False
                        'trHomeState.Visible = False
                        'trHomeZipCode.Visible = False

                        trBillingAddressLine1.Visible = False
                        trBillingAddressLine2.Visible = False
                        trBillingAddressLine3.Visible = False
                        trBillingCity.Visible = False
                        trBillingCountry.Visible = False
                        'trBillingState.Visible = False
                        'trBillingZipCode.Visible = False

                        trPOBoxAddressLine1.Visible = True
                        trPOBoxAddressLine2.Visible = True
                        trPOBoxAddressLine3.Visible = True
                        trPOBoxCity.Visible = True
                        trPOBoxCountry.Visible = True
                        'trPOBoxState.Visible = True
                        'trPOBoxZipCode.Visible = True

                        '*** Uncomment if you want to default address to US. ***
                        If User1.PersonID < 1 Then
                            cmbPOBoxCountry.ClearSelection()
                            SetComboValue(cmbPOBoxCountry, "Ireland") 'Modified by Kavita Zinage 13/05/2016 - Changed  Country ("United States" To "Ireland")
                            PopulateState(cmbPOBoxState, cmbPOBoxCountry)
                        End If

                End Select

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadAddresses()
            Try
                'Author         :   Milind Sutar
                'Modified date  :   07-July-2014
                'Issue          :   User session issue
                Dim oPerson As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                With oPerson
                    If .GetValue("AddressID") IsNot Nothing Or .GetValue("AddressLine1") IsNot Nothing Then
                        txtAddressLine1.Text = CStr(.GetValue("AddressLine1"))
                        hidAddressLine1.value = CStr(.GetValue("AddressLine1"))
                        txtAddressLine2.Text = CStr(.GetValue("AddressLine2"))
                        hidAddressLine2.Value = CStr(.GetValue("AddressLine2"))
                        txtAddressLine3.Text = CStr(.GetValue("AddressLine3"))

                        hidAddressLine3.Value = CStr(.GetValue("AddressLine3"))

                        txtCity.Text = CStr(.GetValue("City"))
                        hidCity.Value = CStr(.GetValue("City"))
                        txtZipCode.Text = CStr(.GetValue("ZipCode"))
                        hidPostalCode.Value = CStr(.GetValue("ZipCode"))
                        txtBillingCountry.Text = CStr(.GetValue("County"))
                        hidCounty.Value = CStr(.GetValue("County"))
                    End If

                    'Put inside If statement if you don't want to default the address to US - Modified by Kavita Zinage 13/05/2016 - Changed  Country ("United States" To "Ireland")
                    SetComboValue(cmbCountry, IIf(CStr(.GetValue("Country")) = String.Empty, "Ireland", .GetValue("Country")).ToString)
                    hidCountry.value = cmbCountry.SelectedValue
                    PopulateState(cmbState, cmbCountry)
                    SetComboValue(cmbState, CStr(.GetValue("State")))
                    hidState.Value = cmbState.SelectedValue

                    If .GetValue("HomeAddressID") IsNot Nothing Or .GetValue("HomeAddressLine1") IsNot Nothing Then
                        txtHomeAddressLine1.Text = CStr(.GetValue("HomeAddressLine1"))
                        txtHomeAddressLine2.Text = CStr(.GetValue("HomeAddressLine2"))
                        txtHomeAddressLine3.Text = CStr(.GetValue("HomeAddressLine3"))
                        txtHomeCity.Text = CStr(.GetValue("HomeCity"))
                        txtHomeZipCode.Text = CStr(.GetValue("HomeZipCode"))
                        txtHomeCounty.Text = CStr(.GetValue("HomeCounty"))
                    End If

                    'Populate Home country or default to United States - Modified by Kavita Zinage 13/05/2016 - Changed  Country ("United States" To "Ireland")
                    SetComboValue(cmbHomeCountry, IIf(CStr(.GetValue("HomeCountry")) = "", "Ireland", CStr(.GetValue("HomeCountry"))).ToString)
                    PopulateState(cmbHomeState, cmbHomeCountry)
                    SetComboValue(cmbHomeState, CStr(.GetValue("HomeState")))

                    If CStr(.GetValue("BillingAddressID")) IsNot Nothing Or CStr(.GetValue("BillingAddressLine1")) IsNot Nothing Then
                        txtBillingAddressLine1.Text = CStr(.GetValue("BillingAddressLine1"))
                        txtBillingAddressLine2.Text = CStr(.GetValue("BillingAddressLine2"))
                        txtBillingAddressLine3.Text = CStr(.GetValue("BillingAddressLine3"))
                        txtBillingCity.Text = CStr(.GetValue("BillingCity"))
                        txtBillingZipCode.Text = CStr(.GetValue("BillingZipCode"))
                    End If

                    'Populate Billing country or default to United States - Modified by Kavita Zinage 13/05/2016 - Changed  Country ("United States" To "Ireland")
                    SetComboValue(cmbBillingCountry, IIf(CStr(.GetValue("BillingCountry")) = "", "Ireland", CStr(.GetValue("BillingCountry"))).ToString)
                    PopulateState(cmbBillingState, cmbBillingCountry)
                    SetComboValue(cmbBillingState, CStr(.GetValue("BillingState")))

                    If CStr(.GetValue("POBoxAddressID")) IsNot Nothing Or CStr(.GetValue("POBox")) IsNot Nothing Then
                        txtPOBoxAddressLine1.Text = CStr(.GetValue("POBox"))
                        txtPOBoxAddressLine2.Text = CStr(.GetValue("POBoxLine2"))
                        txtPOBoxAddressLine3.Text = CStr(.GetValue("POBoxLine3"))
                        txtPOBoxCity.Text = CStr(.GetValue("POBoxCity"))
                        txtPOBoxZipCode.Text = CStr(.GetValue("POBoxZipCode"))
                    End If

                    'Populate pobox country or default to united states - Modified by Kavita Zinage 13/05/2016 - Changed  Country ("United States" To "Ireland")
                    SetComboValue(cmbPOBoxCountry, IIf(CStr(.GetValue("POBoxCountry")) = "", "Ireland", CStr(.GetValue("POBoxCountry"))).ToString)
                    PopulateState(cmbPOBoxState, cmbPOBoxCountry)
                    SetComboValue(cmbPOBoxState, CStr(.GetValue("POBoxState")))
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub ddlAddressType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAddressType.SelectedIndexChanged

            Me.DisplayAddress(ddlAddressType.SelectedItem.Value)
            'lblCurrentAddress.Text = ddlAddressType.SelectedItem.Value
            'lblMePreferredAddress.Text = Me.PreferredAddress
            'If Me.PreferredAddress = ddlAddressType.SelectedItem.Value Then
            '    'chkPrefAddress.Checked = True
            '    'chkPrefAddress.Enabled = False
            '    'Anil Changess For Issue 12835
            '    'txtAddressLine1.Focus()
            'Else
            '    'chkPrefAddress.Checked = False
            '    'chkPrefAddress.Enabled = True
            '    ' chkPrefAddress.Focus()
            '    'End
            'End If

        End Sub




        'Navin Prasad Issue 5051
        ''' <summary>
        ''' RashmiP, Issue 5051, 8/3/2011. Set City, State, County according to Postal code.
        ''' </summary>
        ''' <param name="PostalCode"></param>
        ''' <param name="Country"></param>
        ''' 
        Protected Overridable Sub DoPostalCodeLookup(ByRef PostalCode As String, ByRef Country As String, ByRef County As String, ByRef txt As TextBox, ByRef cmb As DropDownList)
            Dim sAreaCode As String = Nothing, sCounty As String = Nothing, sCity As String = Nothing
            Dim sState As String = Nothing, sCongDist As String = Nothing, sCountry As String = Nothing
            Dim ISOCountry As String
            Try
                Dim oPostalCode As New Aptify.Framework.BusinessLogic.Address.AptifyPostalCode(Me.AptifyApplication.UserCredentials)
                Dim oAddressInfo As New Aptify.Framework.BusinessLogic.Address.AddressInfo(Me.AptifyApplication)

                ISOCountry = oAddressInfo.GetISOCode(CLng(Country))

                If oPostalCode.GetPostalCodeInfo(PostalCode, ISOCountry, _
                                        sCity, sState, _
                                        sAreaCode, , sCounty, , , , , , , , _
                                        sCongDist, sCountry, AllowGUI:=True) Then
                    If txt IsNot Nothing Then
                        If String.IsNullOrWhitespace(txt.Text) Then
                            txt.Text = sCity
                        End If

                    End If
                    If cmb IsNot Nothing And sState.Trim <> "" Then
                        cmb.SelectedValue = sState
                    End If

                    ''RashmiP, removed assigned Area code.
                    County = sCounty

                End If

            Catch ex As Exception

            End Try
        End Sub



#End Region

#Region "Button Events"

        Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click, btnSubmit2.Click
            Dim bNewUser As Boolean
            Try

                If Not ValidatePrefferedAddress() Or Not ValidateAddressInputes() Then
                    Exit Sub
                End If

                bNewUser = User1.UserID <= 0
                'Code modified by Jim on 2016-05-18 from --  Page.Validate() to Page.Validate("ProfileControl")
                Page.Validate()
                'By vaishali----------------------------
                Dim oPerson As AptifyGenericEntityBase = Nothing
                If Not bNewUser Then
                    oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                    'If User1.FirstName.ToLower() <> txtFirstName.Text.Trim().ToLower() OrElse User1.LastName.ToLower() <> txtLastName.Text.Trim().ToLower() OrElse _
                    '    User1.GetValue("Email1").ToLower() <> txtEmail.Text.Trim().ToLower() OrElse _
                    '      User1.GetValue("Birthday") <> txtDob.SelectedDate.ToString().Trim() Then
                    'End If
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
                        If oPersonDupeCheck.CheckForDuplicates(oPerson, duplicateID) = DuplicateCheckResult.Exact Then
                            oPerson = Nothing
                            radDuplicateUser.VisibleOnPageLoad = True
                            Exit Sub
                        End If
                    End If
                Catch ex As Exception
                End Try
                'By vaishali-------stock code
                'IssueId-3240 Suvarna D on 7-Feb-2013 - Validate if record coflicts in db
                ' If Not bNewUser Then

                'Dim sSQL As String = String.Empty
                'sSQL = "SELECT VP.Email1" & _
                '" FROM " & AptifyApplication.GetEntityBaseDatabase("Persons") & _
                '"..vwPersons VP " & _
                '" where VP.ID=" & User1.PersonID
                'Dim sPersonEmailID As String = Convert.ToString(Me.DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))

                'If Not String.IsNullOrEmpty(sPersonEmailID) Then
                '    If String.Compare(sPersonEmailID.ToUpper(), txtEmail.Text.Trim, True) <> 0 Then
                '        If Not ValidatePerson() Then
                '            radDuplicateUser.VisibleOnPageLoad = True
                '            Exit Sub
                '        End If
                '    End If
                'End If
                'Else
                'If Not ValidatePerson() Then
                '    radDuplicateUser.VisibleOnPageLoad = True
                '    Exit Sub
                'End If
                ' End If
                '-------------------------------------------

                lblError.Visible = False
                'Aparna for issue 12964 for showing password length validation
                lblpasswordlengthError.Visible = False
                If User1.UserID <= 0 Then
                    If txtPassword.Text <> txtRepeatPWD.Text Or Trim$(txtPassword.Text) = "" Then
                        lblError.Text = "Password fields must match and must not be blank."
                        lblError.Visible = True
                        lblError.ForeColor = Drawing.Color.Red
                        Exit Sub
                    Else
                        'HP Issue#9078: check if password meets complexity requirements
                        'Aparna for issue 12964 for showing password length validation
                        If Not IsPasswordComplex(txtPassword.Text) Then
                            lblpasswordlengthError.Visible = True
                            Exit Sub
                        End If
                    End If
                End If
                Me.PreferredAddress = rblPAddress.SelectedValue
                User1.SetValue("PreferredAddress", Me.PreferredAddress)
                'Added by shubhangi
                Dim bRecordSaved As Boolean = False
                If User1.UserID <= 0 Then
                    bRecordSaved = DoSave()
                Else
                    bRecordSaved = DoSave_Edit()
                    'AddOrUpdateTopicCode() 'Added By Shiwendra 02/15/2014
                End If

                'End by shubhangi
                If bRecordSaved Then
                    AddOrUpdateTopicCode() 'Added By Shiwendra 02/15/2014
                    ucAdditionalOrganization.SaveData() 'Added By Rajesh k 03/14/2014
                    Dim bOK As Boolean
                    Session("LoadSocialNetworkPhoto") = False
                    If bNewUser Then
                        bOK = WebUserLogin1.Login(User1.WebUserStringID, txtPassword.Text, Page.User)

                        ' make sure to persist changes to user, since many
                        ' applications will do a Response.Redirect after
                        ' this event is fired
                        WebUserLogin1.User.SaveValuesToSessionObject(Page.Session)

                        ' Sapna DJ 12/27/2011- Issue #12545 - Investigate Ways to Reduce Size of Session Objects
                        Session("UserLoggedIn") = True
                        AddOrUpdateTopicCode() 'Added By Shiwendra 02/15/2014
                    Else
                        bOK = True
                    End If
                    If bOK Then

                        ''RashmiP, Remove persons Photo with -1 ID.
                        Me.RemovePersonsPhoto()

                        If Len(Session("ReturnToPage")) > 0 Then
                            Dim sTemp As String
                            sTemp = CStr(Session("ReturnToPage"))
                            Session("ReturnToPage") = "" ' used only once
                            Response.Redirect(sTemp)
                        Else
                            'HP - Issue 8285, the 'virtualdir' setting is incorrect in web.config replacing with IIS internal mapping
                            'Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("virtualdir"))
                            'Response.Redirect(Request.ApplicationPath, False)
                            ' Response.Redirect(Request.ApplicationPath)
                            ''Added BY PRadip 2016-03-21 To Redirect To Membership Page Use When Click on Submit Button
                            If Request.QueryString("MemberShip") IsNot Nothing AndAlso Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("MemberShip"))) = "Y" Then
                                Response.Redirect(MembershipPage)
                            Else
                                ' Redmine Issue 14460
                                Response.Redirect(Request.RawUrl)
                                'Response.Redirect(Request.ApplicationPath)
                            End If
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
                    If lblError.Text.Trim <> "" Then
                        Response.Redirect(Request.RawUrl)
                    End If
                    ''Added By Pradip 2016-03-21
                    Exit Sub
                End If

                ''asmita --Topic code save
                '' Dim triview As Telerik.Web.UI.RadTreeView = DirectCast(TopicCodeControl1.FindControl("trvTopicCodes"), Telerik.Web.UI.RadTreeView)
                '' Dim sErrorString As String = String.Empty
                '' If Not TopicCodeControl1.RecursiveUpdateTopicCodes(triview.Nodes, sErrorString) Then
                'lblError.Text = "Failed to save topic of interests."
                'lblError.Visible = True
                '' Else
                ''lblError.Text = User1.GetLastError()
                ''If lblError.Text.IndexOf(lblError.Text, StringComparison.InvariantCultureIgnoreCase) >= 0 Then
                ''    lblError.Text &= "Try a different User ID."
                ''    lblError.ForeColor = Drawing.Color.Red
                ''End If
                'lblError.Visible = True
                'End If
                ' ---------------------------------asmita 
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub
        ''' <summary>
        ''' Added By Shiwendra 02/15/2014
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub AddOrUpdateTopicCode(Optional ByVal oGE As AptifyGenericEntityBase = Nothing)
            Dim triview As Telerik.Web.UI.RadTreeView = DirectCast(TopicCodeControl1.FindControl("trvTopicCodes"), Telerik.Web.UI.RadTreeView)
            Dim sErrorString As String = String.Empty
            'RS 02/17/2014
            If Not TopicCodeControl1.RecursiveUpdateTopicCodes(triview.Nodes, sErrorString, User1.PersonID) Then
                lblError.Text = "Failed to save topic of interests."
                lblError.Visible = True
            End If
        End Sub

        Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btnCancel2.Click
            'SKB Issue 13162 fixed the session object issue
            If User1.UserID <= 0 Then
                Session("SocialNetwork") = Nothing
            End If
            ' Response.Redirect(Page.Request.ApplicationPath)
            ' Added BY Pradip 2016-03-21 For Issue No 5507 in TFS
            If Request.QueryString("MemberShip") IsNot Nothing AndAlso Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("MemberShip"))) = "Y" Then
                Response.Redirect(MembershipPage)
            Else
                Response.Redirect(Request.ApplicationPath)
            End If
        End Sub

        Protected Overridable Sub UploadBlankPhoto()
            m_bRemovePhoto = True
            HiddenField1.Value = "1"
            btnRemovePhoto.Visible = False
            LableImageSaveIndicator.Visible = False
            If User1.UserID > 0 Then
                LableImageSaveIndicator.Visible = True
            End If

            radImageEditor.Enabled = False
            ShowCropButton(False)


            radImageEditor.ImageUrl = ProfilePhotoBlankImage
            ViewState(ATTRIBUTE_PROFILE_PHOTO_FILENAME) = BlankImage
            UseSocialMediaPhoto(False)

        End Sub

        Protected Sub btnRemovePhoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemovePhoto.Click
            UploadBlankPhoto()
            radwindowProfileImage.VisibleOnPageLoad = False
            SetUserPersonPhoto(radImageEditor.ImageUrl)
        End Sub

        Protected Overridable Sub UseSocialMediaPhoto(ByVal Flag As Boolean)
            If SocialNetworkObject IsNot Nothing AndAlso SocialNetworkObject.IsConnected Then
                SocialNetworkObject.UserProfile.UseSocialMediaPhoto = Flag

            End If
        End Sub

        Protected Sub lnkCheckAvailable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.TextChanged
            lblError.Text = ""
            lblError.Visible = False
            If txtUserID.Text IsNot String.Empty Then
                Dim sSQL As String = "select ID from vwwebusers where UserID='" + txtUserID.Text + "'"
                Dim dt As DataTable
                dt = DataAction.GetDataTable(sSQL)
                With lblError
                    If dt IsNot Nothing And dt.Rows.Count > 0 Then
                        .Text = "Not Available."
                        .ForeColor = Drawing.Color.Red

                        'AnilChangess for Issue 12835
                        txtPassword.Enabled = False
                        txtRepeatPWD.Enabled = False
                        txtPassword.Attributes("value") = ""
                        txtRepeatPWD.Attributes("value") = ""
                        txtPassword.Text = ""
                        txtRepeatPWD.Text = ""
                        txtUserID.Focus()
                    Else
                        .Text = "Available."
                        .ForeColor = Drawing.Color.Green
                        txtPassword.Enabled = True
                        txtRepeatPWD.Enabled = True
                        txtPassword.Focus()
                        'End
                    End If
                    .Visible = True
                    .Font.Bold = True
                End With
            End If
        End Sub

#End Region

#Region "Topic Codes Method"

        Private Function GetNodeCheckSQL(ByVal lTopicCodeID As Long) As String
            Return "SELECT COUNT(*) FROM " & _
                   GetNodeBaseSQL(lTopicCodeID)
        End Function

        Private Function GetNodeLinkSQL(ByVal lTopicCodeID As Long) As String
            Return "SELECT ID FROM " & _
                   GetNodeBaseSQL(lTopicCodeID)
        End Function
        Private Function GetNodeBaseSQL(ByVal lTopicCodeID As Long) As String
            Dim sSQL As String
            sSQL = Database & ".." & _
                   "vwTopicCodeLinks WHERE TopicCodeID=" & lTopicCodeID & _
                   " AND EntityID=(SELECT ID FROM " & _
                   Database & _
                   "..vwEntities WHERE Name='Persons') " & _
                   " AND RecordID=" & User1.PersonID
            Return sSQL
        End Function

        Private Function AddTopicCodeLink(ByVal TopicCodeID As Long) As Boolean
            Dim oLink As AptifyGenericEntityBase
            Try
                oLink = AptifyApplication.GetEntityObject("Topic Code Links", -1)
                oLink.SetValue("TopicCodeID", TopicCodeID)
                oLink.SetValue("RecordID", User1.PersonID)
                oLink.SetValue("EntityID", AptifyApplication.GetEntityID("Persons"))
                oLink.SetValue("Status", "Active")
                oLink.SetValue("Value", "Yes")
                oLink.SetValue("DateAdded", Date.Today)
                Return oLink.Save(False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        Private Function UpdateTopicCodeLink(ByVal TopicCodeID As Long, _
                                             ByVal Active As Boolean) As Boolean
            Dim sSQL As String
            Dim lLinkID As Long
            Dim oLink As AptifyGenericEntityBase

            Try
                sSQL = GetNodeLinkSQL(TopicCodeID)
                lLinkID = CLng(DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                oLink = AptifyApplication.GetEntityObject("Topic Code Links", lLinkID)
                If Active Then
                    oLink.SetValue("Status", "Active")
                    oLink.SetValue("Value", "Yes")
                Else
                    oLink.SetValue("Status", "Inactive")
                    oLink.SetValue("Value", "No")
                End If
                Return oLink.Save(False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function


#End Region

        'Anil Add Code for Issue 12835
        Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
            UploadProfilePhoto()
        End Sub
        'Anil End

        Protected Overridable Sub UploadProfilePhoto()

            If (radUploadProfilePhoto.UploadedFiles IsNot Nothing AndAlso radUploadProfilePhoto.UploadedFiles.Count <> 0) Then
                If SocialNetworkObject IsNot Nothing Then
                    SocialNetworkObject.UserProfile.UseSocialMediaPhoto = False
                End If
                DeleteDownloadedPhotos()
                GetNewProfilePhotoFileName()
                'Dim uniqueGuid As String = System.Guid.NewGuid.ToString
                Dim ImageFile As String = ProfilePhotoFileName
                Dim ImageFilewithPath As String = ProfilePhotoMapPath & ImageFile
                Try
                    radUploadProfilePhoto.UploadedFiles(0).SaveAs(ProfilePhotoMapPath & ImageFile)
                Catch ex As Exception
                End Try
                If SetUserPersonPhoto(ImageFilewithPath) Then

                    radImageEditor.ImageUrl = PersonImageURL & ImageFile
                    btnRemovePhoto.Visible = True
                    radImageEditor.Enabled = True

                    UseSocialMediaPhoto(False)
                    LableImageSaveIndicator.Visible = True
                Else
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New Exception(ImageFilewithPath & " File does not exists."))
                End If
            Else
                'If imgProfile.ImageUrl <> ProfilePhotoBlankImage AndAlso imgProfile.ImageUrl <> String.Empty Then
                '    ViewState("RefreshImageURL") = imgProfile.ImageUrl
                'End If
            End If

        End Sub

        Protected Overridable Function SetUserPersonPhoto(ByVal ImageFile As String) As Boolean
            Dim fInfo As New FileInfo(ImageFile)
            If fInfo.Exists AndAlso ProfilePhotoFileName <> BlankImage Then
                If ImageFile IsNot Nothing AndAlso Not String.IsNullOrEmpty(ImageFile) Then
                    User1.PersonPhoto = ConvertImagetoByte(ImageFile, False)
                    Return True
                End If
            Else
                User1.PersonPhoto = Nothing
            End If
        End Function

        Public ReadOnly Property ProfilePhotoBlankImage As String
            Get
                Return PersonImageURL & BlankImage
            End Get
        End Property

        Protected ReadOnly Property ProfilePhotoFileName() As String
            Get
                Return Convert.ToString(ViewState(ATTRIBUTE_PROFILE_PHOTO_FILENAME))
            End Get
        End Property

        Protected Overridable Function GetNewProfilePhotoFileName() As String
            Dim uniqueGuid As String = System.Guid.NewGuid.ToString
            If User1.PersonID <= 0 Then
                ViewState(ATTRIBUTE_PROFILE_PHOTO_FILENAME) = m_lEntityID & "_" & Me.Session.SessionID & "_" & uniqueGuid & ".jpg"
            Else
                ViewState(ATTRIBUTE_PROFILE_PHOTO_FILENAME) = m_lEntityID & "_" & User1.PersonID & "_" & uniqueGuid & ".jpg"
            End If
            Return Convert.ToString(ViewState(ATTRIBUTE_PROFILE_PHOTO_FILENAME))
        End Function

        Protected ReadOnly Property ProfilePhotoMapPath() As String
            Get
                Return Server.MapPath(PersonImageURL)
            End Get
        End Property

        Protected Overridable Function DownloadSocialMediaPhoto(ByVal PictureURL As String) As String
            DeleteDownloadedPhotos()
            GetNewProfilePhotoFileName()
            Dim wc As New System.Net.WebClient
            Dim ImagePath As String = ProfilePhotoMapPath & ProfilePhotoFileName
            wc.DownloadFile(PictureURL, ImagePath)
            'Anil Bisen issue 12835 
            ' Open a file that is to be loaded into a byte array
            Dim oFile As System.IO.FileInfo
            oFile = New System.IO.FileInfo(ImagePath)
            If oFile.Exists Then
                User1.PersonPhoto = ConvertImagetoByte(ImagePath, False)
            Else
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New Exception(ImagePath & " File does not exists."))
            End If
        End Function

        Protected Overridable Sub DeleteDownloadedPhotos()
            'Anil Changess for Issue 12835 
            Dim fileEntries As String() = Directory.GetFiles(Server.MapPath(PersonImageURL), m_lEntityID & "_" & User1.PersonID & "*.jpg", SearchOption.TopDirectoryOnly)
            For Each fileName As String In fileEntries
                File.Delete(fileName.ToString)
            Next
            'Anil Changess for Issue 12835 
            fileEntries = Directory.GetFiles(Server.MapPath(PersonImageURL), m_lEntityID & "_*" & Me.Session.SessionID + "*.jpg", SearchOption.TopDirectoryOnly)
            For Each fileName As String In fileEntries
                File.Delete(fileName.ToString)
            Next
        End Sub

        Private Function ClientScript() As Object
            Throw New NotImplementedException
        End Function

        Private Function this() As Object
            Throw New NotImplementedException
        End Function

        Private Function IsNothing() As Object
            Throw New NotImplementedException
        End Function

        Protected Sub lnkChangePwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkChangePwd.Click
            radwinPassword.VisibleOnPageLoad = True
            lblerrorLength.Text = ""
        End Sub

        'Neha for issue 12591 for Profile Page Errormessage
        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Dim intpassword As Integer
            If Not IsPasswordComplexPopup(txtNewPassword.Text) Then
                lblerrorLength.Visible = True
                CompareValidator.Enabled = True
                Exit Sub
            End If
            intpassword = WebUserLogin1.UpdateUserPassword(User1.WebUserStringID, txtoldpassword.Text, txtNewPassword.Text, Nothing, Nothing, Page.User)
            If (intpassword = 0) Then
                CompareValidator.Enabled = True
                lblPasswordsuccess.Text = "Password Updated Successfully!"
            End If
            If (intpassword = 1) Then
                CompareValidator.Enabled = True
                lblerrorLength.Text = "<span style='color:red'>No user match or there is no access to the encryption key.</span>"
                Exit Sub
            End If
            If (intpassword = 2) Then
                CompareValidator.Enabled = False
                lblerrorLength.Text = "<span style='color:red'>The Current Password you entered is incorrect. Please try again.</span>"
                Exit Sub
            End If
            If (intpassword = 4) Then
                CompareValidator.Enabled = True
                lblerrorLength.Text = "<span style='color:red'>Current and New Password should not same.</span>"
                Exit Sub
            End If
            If (intpassword = 3) Then
                CompareValidator.Enabled = True
                lblerrorLength.Text = "<span style='color:red'>Password updation failed!</span>"
                Exit Sub
            End If
            radwinPassword.VisibleOnPageLoad = False
        End Sub

        Protected Sub btnCancelpop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelpop.Click
            radwinPassword.VisibleOnPageLoad = False
        End Sub


        Protected Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click

            radDuplicateUser.VisibleOnPageLoad = False
        End Sub

        Private Sub loadmemberinfo()

            Dim objaptifyapp As New AptifyApplication()
            Dim sDB = objaptifyapp.GetEntityBaseDatabase("Persons")
            Dim sSQL = "select distinct VP.ID,VP.FirstName,VP.LastName,VP.Email,VP.MemberType,Case  When Convert(Varchar(12),VP.JoinDate,107)='Jan 01, 1900' Then 'N/A' When Convert(Varchar(12),VP.JoinDate,107)is null  Then 'N/A' else Convert(Varchar(12),VP.JoinDate,107)  end JoinDate,(isnull( Convert(Varchar (12),VP.DuesPaidThru,107),'N/A'))  DuesPaidThru " & _
            " from " & sDB & _
            " ..vwPersons VP " & _
            " where VP.ID=" & User1.PersonID

            Dim dt = Me.DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            Dim dcolstatus As DataColumn = New DataColumn()
            dcolstatus.Caption = "Status"
            dcolstatus.ColumnName = "Status"
            dt.Columns.Add(dcolstatus)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each rw As DataRow In dt.Rows
                    If rw("DuesPaidThru").ToString() = "N/A" OrElse rw("JoinDate").ToString() = "N/A" Then
                        rw("Status") = "Unavailable"
                    ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) = Convert.ToDateTime(rw("JoinDate").ToString()).AddDays(-1) Then
                        rw("DuesPaidThru") = rw("JoinDate")
                        rw("Status") = "Expired"
                    ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now().AddDays(90) Then
                        rw("Status") = "Active"
                    ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now() AndAlso Convert.ToDateTime(rw("DuesPaidThru").ToString()) < Date.Now().AddDays(90) Then
                        rw("Status") = "Expiring"
                    ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) < Date.Now() Then
                        rw("Status") = "Expired"
                    End If
                Next
            End If
            ''Membership Information
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                If dt.Rows.Item(0)("MemberType").ToString() = "" Then
                    lblmembershipType.Visible = False
                Else
                    lblmembershipType.Visible = True
                    lblMemberTypeVal.Text = ""
                    lblMemberTypeVal.Text = dt.Rows(0)("MemberType").ToString()
                    If lblMemberTypeVal.Text.Trim = "Non-Member" Then
                        lblMembershipNumber1.Visible = False
                    ElseIf lblMemberTypeVal.Text.Trim = "Student" Then
                        lblMembershipNumber1.Visible = True
                        lblMembershipNumber1.Text = "Student Number"
                    End If

                End If

                If dt.Rows.Item(0)("JoinDate").ToString() = "" Then
                    lblStartDate.Visible = False
                Else
                    ' lblStartDate.Visible = True
                    lblStartDateVal.Text = dt.Rows.Item(0)("JoinDate").ToString()
                End If

                If dt.Rows.Item(0)("JoinDate").ToString() = "" Then
                    lblEndDate.Visible = False
                Else
                    'lblEndDate.Visible = True
                    lblEndDateVal.Text = dt.Rows.Item(0)("DuesPaidThru").ToString()
                End If
                If dt.Rows.Item(0)("Status").ToString() = "" Then
                    lblStatus.Visible = False
                Else
                    'lblStatus.Visible = True
                    lblStatusVal.Text = dt.Rows.Item(0)("Status").ToString()
                End If
            Else
                trmemberinfo.Visible = False
            End If
        End Sub



        Protected Sub btnCancelProfileImage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelProfileImage.Click
            radwindowProfileImage.VisibleOnPageLoad = False
            'LoadProfilePicture(Nothing)
            UpdateImageSize(ImageData)

        End Sub

        Protected Sub btnSaveProfileImage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveProfileImage.Click
            Dim sExtension As String = System.IO.Path.GetExtension(PersonImageURL & ProfilePhotoFileName).ToString()
            radImageEditor.SaveEditableImage(ProfilePhotoFileName.Substring(0, ProfilePhotoFileName.Length - sExtension.Length), True)
            SetUserPersonPhoto(ProfilePhotoMapPath & ProfilePhotoFileName)

            radwindowProfileImage.VisibleOnPageLoad = False
        End Sub

        Protected Sub ShowCropButton(ByVal isVisible As Boolean)
            If isVisible = True Then
                btnCropImage.Style.Add("display", "inline")
            Else
                btnCropImage.Style.Add("display", "none")
            End If
        End Sub

        Protected Sub LinkBtnpopup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkBtnpopup.Click

            radAlert.VisibleOnPageLoad = True ' For Open Dialog Box
        End Sub
        'asmita
        Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click


            CompanyName = TxtComp.Text
            CompanyAddress = TextBox1.Text + " " + TextBox5.Text + " " + txtCompanyAddress3.Text
            CompanyCity = TextBox3.Text
            CompanyCountry = CStr(IIf(cmbCountryNew.SelectedIndex >= 0, cmbCountryNew.SelectedItem.Text, ""))
            CompanyCounty = txtFirmCounty.Text
            CompanyState = CStr(IIf(cmbStateNew.SelectedIndex >= 0, cmbStateNew.SelectedValue, ""))
            Companyphone = txtIntlCodeNew.Text + "-" + txtPhoneAreaCodeNew.Text + "-" + txtPhoneNew.Text
            txtCompany.Text = TxtComp.Text + "/" + TextBox3.Text + "/" + CStr(IIf(cmbCountryNew.SelectedIndex >= 0, cmbCountryNew.SelectedItem.Text, ""))
            '___AmolB__________________________________
            txtAddressLine1.Text = TextBox1.Text
            txtAddressLine2.Text = TextBox5.Text
            txtAddressLine3.Text = txtCompanyAddress3.Text
            txtCity.Text = TextBox3.Text
            hidCity.value = txtCity.Text.Trim
            hidAddressLine1.value = txtAddressLine1.Text.Trim
            hidAddressLine2.Value = txtAddressLine2.Text.Trim
            hidAddressLine3.Value = txtAddressLine3.Text.Trim
            txtBillingCountry.Text = txtFirmCounty.Text
            cmbCountry.SelectedValue = CStr(IIf(cmbCountryNew.SelectedIndex >= 0, cmbCountryNew.SelectedValue, ""))
            hidCountry.value = cmbCountry.SelectedValue
            PopulateState(cmbBillingState, cmbCountryNew)
            PopulateState(cmbState, cmbCountry)
            cmbState.SelectedValue = CStr(IIf(cmbStateNew.SelectedIndex >= 0, cmbStateNew.SelectedValue, ""))
            hidState.Value = cmbState.SelectedValue
            hidCounty.Value = txtBillingCountry.Text
            '__________________________________________

            radAlert.VisibleOnPageLoad = False
        End Sub
        'asmita
        Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
            CompanyName = ""
            CompanyAddress = ""
            CompanyCity = ""
            CompanyCountry = ""
            CompanyCounty = ""
            CompanyState = ""
            Companyphone = ""
            'SetComboValue(cmbCountryNew, "Ireland")
            radAlert.VisibleOnPageLoad = False
        End Sub

        Protected Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

        End Sub

        Public Function SaveWebUsercompany(ByVal isCompanyChanged As Boolean) As Boolean
            Dim bFlag As Boolean = False
            Dim strError As String = String.Empty
            Try

                'Dim chkstring As Boolean
                ' Dim oApp As New AptifyApplication(GE.UserCredentials)
                Dim oWebuserGE As AptifyGenericEntityBase
                Dim WebId As Integer
                Dim sSQLs As String
                Dim sSQL As String
                Dim param(2) As IDataParameter
                Dim oda As New DataAction
                Dim i As Integer
                Dim dtstate As DataTable = New DataTable()
                If CompanyName <> "" AndAlso CompanyAddress <> "" AndAlso CompanyCity <> "" Then
                    param(0) = oda.GetDataParameter("@City", SqlDbType.VarChar, CompanyCity)
                    param(1) = oda.GetDataParameter("@County", SqlDbType.VarChar, CompanyAddress)
                    param(2) = oda.GetDataParameter("@Company", SqlDbType.NVarChar, CompanyName)
                    sSQL = "..GetCompanyData__c"
                    i = CInt(oda.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, param))
                    If i < 1 Then
                        'If CompanyName <> "" Then
                        sSQL = CompanyName + "/" + CompanyCity + "/" + CompanyCountry
                        If sSQL = txtCompany.Text.Trim() Then
                            Dim dt As DataTable = New DataTable()
                            sSQLs = Database & "..SpGetWebUserID__c @PersonID=" & User1.PersonID
                            dt = Me.DataAction.GetDataTable(sSQLs)
                            If dt.Rows.Count > 0 Then
                                WebId = Convert.ToInt32(dt.Rows(0)("ID"))
                            End If
                            oWebuserGE = AptifyApplication.GetEntityObject("Web Users", CLng(WebId))
                            If oWebuserGE IsNot Nothing Then
                                With oWebuserGE

                                    .SetValue("WebCompanyName__c", CompanyName)

                                    .SetValue("WebCompanyAddress__c", CompanyAddress)

                                    .SetValue("WebCompanyCity__c", CompanyCity)

                                    .SetValue("WebCompanyCountry__c", CompanyCountry)

                                    .SetValue("WebCompanyState__c", CompanyState)

                                    .SetValue("WebCompanyPhone__c", Companyphone)

                                    .SetValue("WebCompanyCounty__c", CompanyCounty)
                                    If .Save(False, strError) Then
                                        bFlag = True
                                    Else
                                        bFlag = False
                                    End If
                                End With
                            End If
                        End If
                    End If
                Else
                    Dim sCompanyName As String = txtCompany.Text
                    Dim parts As String() = sCompanyName.Split(New Char() {"\"c})
                    Dim part As String
                    For Each part In parts
                        txtCompany.Text = part
                        Exit For
                    Next
                    Dim lcompID As Long = -1

                    If hfCompanyID.Value.Trim <> "" Then
                        lcompID = Convert.ToInt32(hfCompanyID.Value)
                        'hfCompanyID.Value = -1
                    End If

                    'AptifyApplication.GetEntityRecordIDFromRecordName("Companies", txtCompany.Text)
                    Dim oPerson As AptifyGenericEntityBase
                    oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                    If Not txtCompany.Text = String.Empty And isCompanyChanged = False Then
                        oPerson.SetValue("CompanyID", lcompID)
                        oPerson.Save(False)
                    End If

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return bFlag
            End Try
            Return bFlag
        End Function

        Protected Sub txtCompany_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompany.DataBinding

        End Sub

        Protected Sub txtCompany_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompany.Load

            'LoadAddresses()

        End Sub

        Protected Sub txtCompany_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompany.TextChanged
            ' LoadAddresses()
        End Sub

#Region "Custom Methods"

        ''' <summary>
        ''' Check if User is having Organization or Not
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub AdditionalOrganization()
            If User1.UserID > 0 Then
                Dim sSQLOrganization As String = Database & "..spGetPersonOrganizationID__c"
                Dim params(0) As System.Data.IDataParameter
                params(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.VarChar, Convert.ToString(User1.PersonID))
                Dim iOrganizationID As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSQLOrganization, CommandType.StoredProcedure, params))
                If iOrganizationID > 0 Then
                    trAdddOrganization.Visible = True
                    ucAdditionalOrganization.SaveButton = False
                End If
            End If
        End Sub
#End Region
        Function ValidatePrefferedAddress() As Boolean
            Dim flg As Boolean = True
            lblPAdressError.Text = ""
            Select Case rblPAddress.SelectedValue
                Case "Business Address"
                    If txtCompany.Text = "" Then

                        lblPAdressError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyConsulting.Person.NoCompanyUponSelectBusinessAddress__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        lblPAdressError.ForeColor = Drawing.Color.Red

                        flg = False
                    End If
                Case ""
                    lblPAdressError.Text = "Please select preffered address."
                    lblPAdressError.ForeColor = Drawing.Color.Red
                    flg = False
            End Select
            Return flg
        End Function


        Protected Sub rblPAddress_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblPAddress.SelectedIndexChanged

            Me.PreferredAddress = rblPAddress.SelectedValue

        End Sub



        Private Sub ClearData()
            txtCompany11.Text = String.Empty
            hfCompanyID11.Value = "-1"
            cmbJobTitle11.SelectedIndex = 0
        End Sub

        Public Sub LoadPersonCompanyDetails(ByVal PersonID As Long)
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
                        .Item("CompanyID") = drPersonEducation.Item("CompanyID")
                        .Item("CompanyName") = drPersonEducation.Item("CompnayName")

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

                    Dim oPerson As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = Nothing
                    oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                    With oPerson.SubTypes("PersonCompanies").Add()
                        If Convert.ToInt32(hfCompanyID11.Value) > 0 Then
                            .SetValue("CompanyId", hfCompanyID11.Value)
                            .SetValue("Title", cmbJobTitle11.SelectedItem.Text)
                        Else
                            Dim oCompany As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = Nothing
                            oCompany = AptifyApplication.GetEntityObject("Companies", -1)
                            oCompany.SetValue("Name", txtCompany11.Text.Trim)
                            oCompany.SetValue("AddressLine1", txtAddressLine1.Text.Trim)
                            oCompany.SetValue("AddressLine2", txtAddressLine2.Text.Trim)
                            oCompany.SetValue("AddressLine3", txtAddressLine3.Text.Trim)
                            oCompany.SetValue("City", txtAlternateCmpCity.Text.Trim)
                            oCompany.SetValue("State", CStr(IIf(ddlAlternateCmpState.SelectedIndex >= 0, ddlAlternateCmpState.SelectedValue, "")))
                            oCompany.SetValue("Country", CStr(IIf(ddlAlternateCmpCountry.SelectedIndex >= 0, ddlAlternateCmpCountry.SelectedValue, "")))
                            oCompany.SetValue("County", txtAlternateCmpCounty.Text.Trim)

                            oCompany.SetValue("MainAreaCode", txtAlternateCmpIntlCode.Text.Trim)
                            oCompany.SetValue("MainCountryCode", txtAlternateCmpAreaCode.Text.Trim)
                            oCompany.SetValue("MainPhone", txtAlternateCmpPhone.Text.Trim)
                            oCompany.SetValue("Status__c", "Pending")
                            oCompany.Save(False)
                            .SetValue("CompanyId", oCompany.RecordID)
                            .SetValue("Title", cmbJobTitle11.SelectedItem.Text)

                            txtAlternateCompName.Text = ""
                            txtAlternateCmpLine1.Text = ""
                            txtAlternateCmpLine2.Text = ""
                            txtAlternateCmpLine3.Text = ""
                            txtAlternateCmpCity.Text = ""
                            ddlAlternateCmpState.SelectedIndex = 0
                            ddlAlternateCmpCountry.SelectedIndex = 0
                            txtAlternateCmpCounty.Text = ""
                            txtAlternateCmpIntlCode.Text = ""
                            txtAlternateCmpAreaCode.Text = ""
                            txtAlternateCmpPhone.Text = ""
                        End If

                    End With
                    oPerson.Save(False)
                    ClearData()
                    LoadPersonCompanyDetails(User1.PersonID)
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
                            oPMembershipGE = oPerson.SubTypes("PersonCompanies").Find("ID", PersonCompanyTable.Rows.Find(sID)("EntID"))
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

        Function ValidateInputes() As Boolean
            'Dim rtn As Boolean = True
            lblDateError.Text = String.Empty

            Dim errmsg As String = "The Following Field(s) are Required: "
            If txtCompany11.Text = "" Then
                errmsg = errmsg & "Company Name,"
            End If

            If cmbJobTitle11.SelectedItem.Text = "Select Job Title" Then
                errmsg = errmsg & " Job title,"

            End If
            errmsg = errmsg.TrimEnd(CChar(","))
            If errmsg <> "The Following Field(s) are Required: " Then
                lblDateError.Text = errmsg
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "OnMyButtonClick();", True)
                Return False
            Else
                Return True
            End If

        End Function
        Protected Sub grvCompany_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grvCompany.RowDeleting

        End Sub



        'Protected Sub btnComCanlNew_Click(sender As Object, e As EventArgs) Handles btnComCanlNew.Click
        '    RadAddNew.VisibleOnPageLoad = False
        '    txtCompany11.Text = ""
        'End Sub
        Function ValidateAddressInputes() As Boolean
            Dim rtn As Boolean = True
            lblValidAddress.Text = String.Empty
            lblValidCity.Text = String.Empty
            lblValidCountry.Text = String.Empty
            'lblValidPostal.text = String.Empty
            'Dim rtn As Boolean = True
            If txtCompany.Text <> "" Then
                If hidAddressLine1.value = "" Then
                    lblValidAddress.Text = "Address Line 1 Required"
                    rtn = False
                End If
                If hidCity.value = "" Then
                    lblValidCity.Text = "City Required"
                    rtn = False
                End If
                If Convert.ToInt32(Convert.ToString(hidCountry.value)) < 1 Then
                    lblValidCountry.Text = "Country Required"
                    rtn = False
                End If
            End If
            Return rtn
        End Function

        Protected Sub ddlAlternateCmpCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAlternateCmpCountry.SelectedIndexChanged
            PopulateState(ddlAlternateCmpState, ddlAlternateCmpCountry)
        End Sub

        Protected Sub btnAltCmpOK_Click(sender As Object, e As EventArgs) Handles btnAltCmpOK.Click
            txtCompany11.Text = txtAlternateCompName.Text.Trim
            'hfCompanyID.Value = "-1"
            txtAlternateCompName.Text = String.Empty
            radAlternateCompany.VisibleOnPageLoad = False
        End Sub
    End Class
End Namespace
