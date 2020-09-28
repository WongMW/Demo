

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
    Partial Class ProfileTest__c

        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_PROCESSING_IMAGE_URL As String = "ProcessingImageURL"
        Protected Const ATTRIBUTE_PERSON_IMAGE_URL As String = "PersonImageURL"
        Protected Const ATTRIBUTE_MEMBERSHIP_IMAGE_URL As String = "MembershipImageURL"
        Protected Const ATTRIBUTE_PWD_LENGTH As String = "minPwdLength"
        Protected Const ATTRIBUTE_PWD_UPPERCASE As String = "minPwdUpperCase"
        Protected Const ATTRIBUTE_PWD_LOWERCASE As String = "minPwdLowerCase"
        Protected Const ATTRIBUTE_PWD_NUMBERS As String = "minPwdNumbers"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "Profile"
        Protected Const ATTRIBUTE_PERSON_BLANK_IMG As String = "BlankImage"
        Protected ATTRIBUTE_IMAGEUPLOAD_PROFILE_TNCTEXT As String = "ImageUploadUserProfileText"
        Protected ATTRIBUTE_IMAGEUPLOAD_PROFILESAVE_TNCTEXT As String = "ImageUploadUserProfileSaveText"
        Protected Const ATTRIBUTE_PERSON_IMG_WIDTH As String = "ImageWidth"
        Protected Const ATTRIBUTE_PERSON_IMG_HEIGHT As String = "ImageHeight"
        Protected Const ATTRIBUTE_SYSTEM_NAME As String = "SocialNetworkSystemName"
        Protected Const ATTRIBUTE_PROFILE_PHOTO_FILENAME As String = "ProfilePhotoFileName"

        Protected Const ATTRIBUTE_PROFILE_ALERT_DUPLICATEPERSONVALIDATION As String = "DuplicatePersonValidation"

        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected m_bRemovePhoto As Boolean
        Protected m_sblankImage As String
        Dim m_lEntityID As Long
        Dim m_lRecordID As String
        Dim m_sEntityName As String = "Persons"
        Protected Const ATTRIBUTE_MEMBERSHIP_Page_URL As String = "MembershipPageURL"

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try

                regexEmailValid.ValidationExpression = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.EmailRegularExpressionValidator__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                SetProperties()
                If User1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If
                If Not IsPostBack Then
                    PopulateDropDowns()
                    LoadPendingRecord()
                    If User1.UserID <= 0 Then
                        divPendingChange.Visible = False
                    Else
                        divPendingChange.Visible = True
                        trUserID.Visible = True
                        divStatus.Visible = True
                        LoadUserInfo()
                        LoadBusinessAddresses()
                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub

#Region "Properties and Methods"
        Private Const m_c_sPrefix As String = "__aptify_shoppingCart_"
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
        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME

            MyBase.SetProperties()
            If String.IsNullOrEmpty(MembershipPage) Then
                MembershipPage = Me.GetLinkValueFromXML(ATTRIBUTE_MEMBERSHIP_Page_URL)
            End If

            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
        End Sub

        Private Sub LoadBusinessAddresses()

            Dim PersonCompanyTable As DataTable = Nothing
            Dim sSQL As String = Database & "..spGetPersonCompanyInformation__c"
            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.BigInt, User1.PersonID)
            PersonCompanyTable = DataAction.GetDataTableParametrized(sSQL, Data.CommandType.StoredProcedure, param)
            lstAddress.DataSource = PersonCompanyTable
            lstAddress.DataBind()
            'Dim lstItem As DataListItem
            'For Each lstItem In lstAddress.Controls
            '    If Not lstItem.FindControl("cmdUseAddress") Is Nothing Then

            '    End If

            'Next
        End Sub
#End Region


#Region "Load Data Methods"
        Private Sub LoadPendingRecord()
            PendingChangesDetails1.EntityName = "Persons"
            PendingChangesDetails1.RecordID = CType(User1.PersonID, Long)
            PendingChangesDetails1.PendingChangeType = "Approval Request"
            PendingChangesDetails1.LoadPendingChanges(PendingChangesDetails1.EntityName, PendingChangesDetails1.RecordID, PendingChangesDetails1.PendingChangeType)

        End Sub

        Private Sub PopulateDropDowns()
            Dim sSQL As String = String.Empty
            Try

                Dim ds As DataSet


                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, User1.PersonID)
                sSQL = AptifyApplication.GetEntityBaseDatabase("Functions") & "..SpGetDataonProfilePage__c"
                ds = DataAction.GetDataSetParametrized(sSQL, CommandType.StoredProcedure, param)


                '04/08/2017 - Dipali To set data to  combobox list
                'set PersonActiveJobTitle__c'
                cmbJobfunction.DataSource = ds.Tables(0)
                cmbJobfunction.DataTextField = "Name"
                cmbJobfunction.DataValueField = "Name"
                cmbJobfunction.DataBind()


                'Get EmploymentStatus List
                cmbempstatus.DataSource = ds.Tables(1)
                cmbempstatus.DataTextField = "Name"
                cmbempstatus.DataValueField = "ID"
                cmbempstatus.DataBind()

                'get prefix list

                cmbSalutation.DataSource = ds.Tables(2)
                cmbSalutation.DataTextField = "Prefix"
                cmbSalutation.DataValueField = "Prefix"
                cmbSalutation.DataBind()
                cmbSalutation.Items.Insert(0, "")


                'get Country lIst

                Dim dtCountry As New DataTable


                dtCountry = ds.Tables(3)
                ddlHomeAddCountry.DataSource = dtCountry
                ddlHomeAddCountry.DataTextField = "Country"
                ddlHomeAddCountry.DataValueField = "ID"
                ddlHomeAddCountry.DataBind()
                ddlHomeAddCountry.Items.Insert(0, "Select Country")



                SetComboValue(ddlHomeAddCountry, "Ireland")
                PopulateState(ddlHomeAddState, ddlHomeAddCountry)
                cmbAddCompCountry.DataSource = dtCountry
                cmbAddCompCountry.DataTextField = "Country"
                cmbAddCompCountry.DataValueField = "ID"
                cmbAddCompCountry.DataBind()
                cmbAddCompCountry.Items.Insert(0, "Select Country")

                SetComboValue(cmbAddCompCountry, "Ireland")
                PopulateState(cmbAddCompState, cmbAddCompCountry)


                'Commented by dipali
                'sSQL = AptifyApplication.GetEntityBaseDatabase("Functions") & "..spGetPersonActiveJobTitle__c"
                'cmbJobfunction.DataSource = DataAction.GetDataTable(sSQL)
                'cmbJobfunction.DataTextField = "Name"
                'cmbJobfunction.DataValueField = "Name"
                'cmbJobfunction.DataBind()


                'sSQL = AptifyApplication.GetEntityBaseDatabase("Addresses") & _
                '       "..spGetCountryList__c"
                'dt = DataAction.GetDataTable(sSQL)
                'cmbCountry.DataSource = dt
                'cmbCountry.DataTextField = "Country"
                'cmbCountry.DataValueField = "ID"
                'cmbCountry.DataBind()
                'cmbCountry.Items.Insert(0, "Select Country")


                'commented by dipali 04/08/2017
                'sSQL = AptifyApplication.GetEntityBaseDatabase("EmploymentStatus__c") & _
                ' "..SpGetEmploymentStatusProfileNew__c @PersonID=" & Convert.ToInt32(User1.PersonID)
                'cmbempstatus.DataSource = DataAction.GetDataTable(sSQL)
                'cmbempstatus.DataTextField = "Name"
                'cmbempstatus.DataValueField = "ID"
                'cmbempstatus.DataBind()

                'commented by dipali 04/08/2017
                'sSQL = AptifyApplication.GetEntityBaseDatabase("Prefix") & _
                '    "..spGetPrefixes"
                'Dim dtPrefix As New DataTable
                'dtPrefix = DataAction.GetDataTable(sSQL)
                'Dim R As DataRow = dtPrefix.NewRow
                'R("Prefix") = ""
                'dtPrefix.Rows.InsertAt(R, 0)
                'cmbSalutation.DataSource = dtPrefix
                'cmbSalutation.DataTextField = "Prefix"
                'cmbSalutation.DataValueField = "Prefix"
                'cmbSalutation.DataBind()


                'commented by dipali 04/08/2017
                'Dim dtCountry As New DataTable

                'sSQL = AptifyApplication.GetEntityBaseDatabase("Addresses") & _
                '      "..spGetCountryListProfile__c"
                'dtCountry = DataAction.GetDataTable(sSQL)
                'ddlHomeAddCountry.DataSource = dtCountry
                'ddlHomeAddCountry.DataTextField = "Country"
                'ddlHomeAddCountry.DataValueField = "ID"
                'ddlHomeAddCountry.DataBind()
                'ddlHomeAddCountry.Items.Insert(0, "Select Country")




            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub PopulateState(ByRef cmbPopulateState As DropDownList, ByRef cmbCurrentCountry As DropDownList)
            Try
                Dim sSQL As String
                sSQL = Database & "..spGetStateList @CountryID=" & cmbCurrentCountry.SelectedValue.ToString
                cmbPopulateState.DataSource = DataAction.GetDataTable(sSQL)
                cmbPopulateState.DataTextField = "State"
                cmbPopulateState.DataValueField = "State"
                cmbPopulateState.DataBind()
                cmbPopulateState.Items.Insert(0, "Select State")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
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
        Private Sub LoadUserInfo()
            Try
                Dim sSQL As String = String.Empty
                'Modified by Sheela as part of log #19300
                Dim param(1) As IDataParameter
                param(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, User1.PersonID)
                param(1) = DataAction.GetDataParameter("@WebUserID", SqlDbType.Int, User1.UserID)
                sSQL = "..spGetPersonDetailsForProfile__c"
                Dim dtProfile = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                If dtProfile IsNot Nothing AndAlso dtProfile.Rows.Count > 0 Then
                    txtFirstName.Text = Convert.ToString(dtProfile.Rows(0)("FirstName"))
                    txtLastName.Text = Convert.ToString(dtProfile.Rows(0)("LastName"))
                    txtJobTitle.Text = Convert.ToString(dtProfile.Rows(0)("Title"))
                    txtGender.Text = Convert.ToString(dtProfile.Rows(0)("Gender"))

                    If Convert.ToString(dtProfile.Rows(0)("BirthDay")).Trim <> "" AndAlso Convert.ToDateTime(Convert.ToString(dtProfile.Rows(0)("BirthDay"))) <> New System.DateTime(1900, 1, 1) Then
                        Dim d1 As DateTime = Convert.ToDateTime(dtProfile.Rows(0)("BirthDay").ToString().Trim())
                        txtDob.Text = (New Date(d1.Year, d1.Month, d1.Day).ToShortDateString())
                    End If
                    txtCountryoforigin.Text = Convert.ToString(dtProfile.Rows(0)("Country")).Trim
                    SetComboValue(cmbSalutation, Convert.ToString(dtProfile.Rows(0)("Prefix")).Trim)
                    txtPreferredSalutation.Text = Convert.ToString(dtProfile.Rows(0)("PreferredSalutation")).Trim
                    lblStatus.Text = Convert.ToString(dtProfile.Rows(0)("MemberType")).Trim
                    lblUserID.Text = Convert.ToString(dtProfile.Rows(0)("USERID")).Trim
                    If Convert.ToInt32(Convert.ToString(dtProfile.Rows(0)("EmploymentStatusID")).Trim) > 0 Then
                        cmbempstatus.SelectedValue = Convert.ToString(dtProfile.Rows(0)("EmploymentStatusID")).Trim
                    End If
                    txtEmail.Text = Convert.ToString(dtProfile.Rows(0)("Email1")).Trim
                    SetComboValue(cmbJobfunction, Convert.ToString(dtProfile.Rows(0)("Jobfunction")).Trim)
                    txtlandlineAreaCode.Text = Convert.ToString(dtProfile.Rows(0)("LandLineAreaCode")).Trim
                    txtlandlineCountryCode.Text = Convert.ToString(dtProfile.Rows(0)("CountryCode")).Trim
                    txtlandlineNumber.Text = Convert.ToString(dtProfile.Rows(0)("LandLinePhone")).Trim
                    txtMobileAreaCode.Text = Convert.ToString(dtProfile.Rows(0)("MobileAreaCode")).Trim
                    txtmobileCountryCode.Text = Convert.ToString(dtProfile.Rows(0)("MobileCountryCode")).Trim
                    txtMobileNumber.Text = Convert.ToString(dtProfile.Rows(0)("MobilePhone")).Trim

                    If Convert.ToString(dtProfile.Rows(0)("PreferredAddress")).Trim = "Home Address" Then
                        rblPAddress.SelectedValue = "Home"
                        lblPrefHome.Visible = True
                    ElseIf Convert.ToString(dtProfile.Rows(0)("PreferredAddress")).Trim = "Business Address" Then
                        rblPAddress.SelectedValue = "Work"
                        lblPrefBusiness.Visible = True
                    End If

                    Dim sb As StringBuilder = New StringBuilder
                    If Convert.ToString(dtProfile.Rows(0)("AHomeLine1")).Trim <> "" Then
                        sb.AppendLine(Convert.ToString(dtProfile.Rows(0)("AHomeLine1")).Trim + ",")
                    End If
                    If Convert.ToString(dtProfile.Rows(0)("AHomeLine2")).Trim <> "" Then
                        sb.AppendLine(Convert.ToString(dtProfile.Rows(0)("AHomeLine2")).Trim + ",")
                    End If
                    If Convert.ToString(dtProfile.Rows(0)("AHomeLine3")).Trim <> "" Then
                        sb.AppendLine(Convert.ToString(dtProfile.Rows(0)("AHomeLine3")).Trim + ",")
                    End If
                    If Convert.ToString(dtProfile.Rows(0)("AHomeCity")).Trim <> "" Then
                        sb.Append(Convert.ToString(dtProfile.Rows(0)("AHomeCity")).Trim + ", ")
                    End If
                    '  If Convert.ToString(dtProfile.Rows(0)("AHomeStateProvince")).Trim <> "" Then
                    'sb.Append(Convert.ToString(dtProfile.Rows(0)("AHomeStateProvince")).Trim + " ")
                    ' End If
                    If Convert.ToString(dtProfile.Rows(0)("AhomePostalCode")).Trim <> "" Then
                        sb.AppendLine(Convert.ToString(dtProfile.Rows(0)("AhomePostalCode")).Trim)
                    End If
                    If Convert.ToString(dtProfile.Rows(0)("AHomeCountry")).Trim <> "" Then
                        sb.AppendLine(Convert.ToString(dtProfile.Rows(0)("AHomeCountry")).Trim)
                    End If
                    txtHomeAddress.InnerHtml = sb.ToString().Replace(Environment.NewLine, "<br />")

                    txtHomeAddLine1.Text = Convert.ToString(dtProfile.Rows(0)("AHomeLine1")).Trim
                    txtHomeAddLine2.Text = Convert.ToString(dtProfile.Rows(0)("AHomeLine2")).Trim
                    txtHomeAddLine3.Text = Convert.ToString(dtProfile.Rows(0)("AHomeLine3")).Trim
                    txtHomeAddCity.Text = Convert.ToString(dtProfile.Rows(0)("AHomeCity")).Trim
                    txtHomeAddCounty.Text = Convert.ToString(dtProfile.Rows(0)("AhomeCounty")).Trim
                    txtHomeAddPostalCode.Text = Convert.ToString(dtProfile.Rows(0)("AhomePostalCode")).Trim
                    If Convert.ToInt32(Convert.ToString(dtProfile.Rows(0)("AhomeCountryID")).Trim) > 0 Then
                        ddlHomeAddCountry.SelectedValue = Convert.ToString(dtProfile.Rows(0)("AhomeCountryID")).Trim
                        PopulateState(ddlHomeAddState, ddlHomeAddCountry)
                    End If

                    If Convert.ToString(dtProfile.Rows(0)("AHomeStateProvince")).Trim <> "" Then
                        ' ddlHomeAddState.SelectedValue = Convert.ToString(dtProfile.Rows(0)("AHomeStateProvince")).Trim
                    End If
                    Dim sb2 As StringBuilder = New StringBuilder

                    If Convert.ToString(dtProfile.Rows(0)("CompanyName")).Trim <> "" Then
                        sb2.AppendLine(Convert.ToString(dtProfile.Rows(0)("CompanyName")).Trim + ",")
                    End If
                    If Convert.ToString(dtProfile.Rows(0)("BLine1")).Trim <> "" Then
                        sb2.AppendLine(Convert.ToString(dtProfile.Rows(0)("BLine1")).Trim + ",")
                    End If
                    If Convert.ToString(dtProfile.Rows(0)("BLine2")).Trim <> "" Then
                        sb2.AppendLine(Convert.ToString(dtProfile.Rows(0)("BLine2")).Trim + ",")
                    End If
                    If Convert.ToString(dtProfile.Rows(0)("BLine3")).Trim <> "" Then
                        sb2.AppendLine(Convert.ToString(dtProfile.Rows(0)("BLine3")).Trim + ",")
                    End If
                    If Convert.ToString(dtProfile.Rows(0)("BCity")).Trim <> "" Then
                        sb2.Append(Convert.ToString(dtProfile.Rows(0)("BCity")).Trim + ", ")
                    End If

                    '  If Convert.ToString(dtProfile.Rows(0)("BStateProvince")).Trim <> "" Then
                    'sb2.Append(Convert.ToString(dtProfile.Rows(0)("BStateProvince")).Trim + " ")
                    ' End If
                    If Convert.ToString(dtProfile.Rows(0)("BPostalCode")).Trim <> "" Then
                        sb2.AppendLine(Convert.ToString(dtProfile.Rows(0)("BPostalCode")).Trim)
                    End If
                    If Convert.ToString(dtProfile.Rows(0)("BCountry")).Trim <> "" Then
                        sb2.AppendLine(Convert.ToString(dtProfile.Rows(0)("BCountry")).Trim)
                    End If
                    If Convert.ToString(dtProfile.Rows(0)("BLine1")).Trim = "" AndAlso Convert.ToString(dtProfile.Rows(0)("BLine2")).Trim = "" AndAlso Convert.ToString(dtProfile.Rows(0)("BLine3")).Trim = "" AndAlso Convert.ToString(dtProfile.Rows(0)("BCity")).Trim = "" AndAlso Convert.ToString(dtProfile.Rows(0)("BStateProvince")).Trim = "" AndAlso Convert.ToString(dtProfile.Rows(0)("BPostalCode")).Trim = "" AndAlso Convert.ToString(dtProfile.Rows(0)("BCountry")).Trim = "" Then
                        sb2.AppendLine("N/A")
                    End If
                    divPrimarybusiness.InnerHtml = sb2.ToString().Replace(Environment.NewLine, "<br />")
                    hidCompanyIdOld.Value = Convert.ToString(dtProfile.Rows(0)("CompanyID")).Trim
                    'txtCompany.Text = Convert.ToString(dtProfile.Rows(0)("CompanyName")).Trim
                    'lblCurrentCompanyName.Text = Convert.ToString(dtProfile.Rows(0)("CompanyName")).Trim + "sample text text"
                    lblCurrentCompanyName.Text = sb2.ToString()

                    If Convert.ToString(dtProfile.Rows(0)("CompanyName")).Trim = "" Then
                        divPrimarybusiness.InnerHtml = "N/A"
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Function IsPasswordComplexPopup(ByVal password As String) As Boolean
            Dim result As Boolean = False

            lblerrorLength.Text = ""
            If password.Length >= MinPwdLength Then
                result = System.Text.RegularExpressions.Regex.IsMatch(password, "(?=(.*[A-Z].*){" & MinPwdUpperCase & ",})(?=(.*[a-z].*){" & MinPwdLowerCase & ",})(?=(.*\d.*){" & MinPwdNumbers & ",})")
            End If

            If Not result Then
                lblerrorLength.Text = "<span style='font-weight: bold; color:red; font-size:11px;'>The password criteria has not been met. Please try again.</span> " & "<br/>" &
                "<span style='font-style:italic; font-size: 9pt; font-weight: bold;'>Password must be a minimum length of " & MinPwdLength.ToString & " with at least " & _
                 MinPwdLowerCase & " lower-case letter(s) and " & MinPwdUpperCase & " upper-case letter(s) and " & MinPwdNumbers & " number(s).</span>"
            End If
            Return result
        End Function

        Private Sub SetPersonObjectAddress(ByRef oPersonAddress As AptifyGenericEntityBase, _
                                         ByVal sPrefix As String, _
                                         Optional ByVal AddressName As String = "")

            Dim bIsSubType As Boolean = False
            'Dim oAddress As AptifyGenericEntityBase
            Dim sError As String = "Its error"
            Try
                ''Commented By Pradip 2017-03-07 for red mine issue https://redmine.softwaredesign.ie/issues/13227
                'Dim oPerson2 As AptifyGenericEntityBase
                'oPerson2 = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                'If oPerson2.GetValue("HomeAddressLine1").ToString.Trim <> "" Then
                '    If String.Compare(oPersonAddress.EntityName, "Persons", True) = 0 Then
                '        oAddress = oPersonAddress.Fields(sPrefix & "AddressID").EmbeddedObject
                '    ElseIf String.Compare(oPersonAddress.EntityName, "PersonAddress", True) = 0 Then
                '        oAddress = oPersonAddress.Fields("AddressID").EmbeddedObject
                '        bIsSubType = True
                '        oPersonAddress.SetValue("Name", AddressName + Convert.ToString(Date.Now.Day) + Convert.ToString(Date.Now.Month) + Convert.ToString(Date.Now.Year))
                '    Else
                '        oAddress = oPersonAddress.Fields("AddressID").EmbeddedObject
                '    End If
                '    With oAddress
                '        .SetValue("AddressTypeID", 3)
                '        .SetValue("Line1", oPerson2.GetValue("HomeAddressLine1"))
                '        .SetValue("Line2", oPerson2.GetValue("HomeAddressLine2"))
                '        .SetValue("Line3", oPerson2.GetValue("HomeAddressLine3"))
                '        .SetValue("City", oPerson2.GetValue("HomeCity"))
                '        .SetValue("StateProvince", oPerson2.GetValue("HomeState"))
                '        .SetValue("PostalCode", oPerson2.GetValue("HomeZipCode"))
                '        .SetValue("CountryCodeID", oPerson2.GetValue("HomeCountryCodeID"))
                '        .SetValue("Country", oPerson2.GetValue("HomeCountry"))
                '        .SetValue("County", oPerson2.GetValue("HomeCounty"))
                '    End With
                '    If Not bIsSubType Then
                '        If Not oPersonAddress.Save(False) Then _
                '                Throw New ApplicationException("Unable to save the Person's Address changes.  Error: " & oPersonAddress.LastError)
                '    Else
                '        If Not oPersonAddress.ParentGE.Save(False) Then _
                '                Throw New ApplicationException("Unable to save the Person's Address changes.  Error: " & oPersonAddress.ParentGE.LastError)
                '    End If
                'End If
                Dim oPerson3 As AptifyGenericEntityBase
                oPerson3 = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                With oPerson3
                    .SetValue("HomeAddressLine1", txtHomeAddLine1.Text.Trim)
                    .SetValue("HomeAddressLine2", txtHomeAddLine2.Text.Trim)
                    .SetValue("HomeAddressLine3", txtHomeAddLine3.Text.Trim)
                    .SetValue("HomeCity", txtHomeAddCity.Text.Trim)
                    .SetValue("HomeCounty", txtHomeAddCounty.Text.Trim)
                    If ddlHomeAddState.SelectedValue.Trim <> "Select State" Then
                        '  .SetValue("HomeState", ddlHomeAddState.SelectedValue.Trim)
                    End If
                    .SetValue("HomeZipCode", txtHomeAddPostalCode.Text.Trim)
                    .SetValue("HomeCountryCodeID", ddlHomeAddCountry.SelectedValue)
                    .SetValue("HomeCountry", ddlHomeAddCountry.SelectedItem.Text.Trim)
                End With

                Dim sPendingChangesField As String = String.Empty
                If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Persons", "PendingChangesFields__c")) Then
                    sPendingChangesField = CStr(AptifyApplication.GetEntityAttribute("Persons", "PendingChangesFields__c"))
                End If
                Dim aPendingChanges() As String = sPendingChangesField.Split(New Char() {","c})
                Dim isCompanyChanged As Boolean = False
                Dim oPersonOld As AptifyGenericEntityBase
                oPersonOld = AptifyApplication.GetEntityObject("Persons", User1.PersonID)


                Dim oPersonWithPedingChanges As AptifyGenericEntityBase
                oPersonWithPedingChanges = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                For i As Integer = 0 To aPendingChanges.Count - 1
                    Dim sFieldName As String = aPendingChanges(i)
                    If sFieldName = "HomeAddressLine1" Or sFieldName = "HomeAddressLine2" Or sFieldName = "HomeAddressLine3" Or sFieldName = "HomeCity" Or sFieldName = "HomeCountry" Or sFieldName = "HomeState" Or sFieldName = "HomeZipCode" Or sFieldName = "HomeCounty" Or sFieldName = "HomeCountryCodeID" Then
                        Dim oldValue As String = CStr(oPersonOld.GetValue(sFieldName))
                        Dim newValue As String = CStr(oPerson3.GetValue(sFieldName))
                        ' If sFieldName = "Phone" Then
                        ' oldValue = oldValue.Replace("-", String.Empty)
                        ' newValue = newValue.Replace("-", String.Empty)
                        'End If
                        If (String.Compare(oldValue, newValue) <> 0) Then
                            ' Dim oPersonWithPedingChanges As AptifyGenericEntityBase
                            'oPersonWithPedingChanges = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                            oPersonWithPedingChanges.SetValue(sFieldName, newValue)
                            'Dim bPendingRecordSaved = oPersonWithPedingChanges.SaveAsPendingChange(3, Date.Now(), "changed from web")
                        End If
                    End If
                Next
                Dim bPendingRecordSaved = oPersonWithPedingChanges.SaveAsPendingChange(3, Date.Now(), "changed from web")

                ' If Not oPerson3.Save(False) Then
                '    Throw New ApplicationException("Unable to save the Person's Address changes.  Error: " & oPerson3.LastError)
                ' End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub AddAddressToPerson(ByRef oPerson As AptifyGenericEntityBase, ByVal AddressName As String)
            Try
                Me.SetPersonObjectAddress(oPerson.SubTypes("PersonAddress").Add, "", AddressName)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Public Function SaveWebUsercompany() As Boolean
            'Dim bFlag As Boolean = False
            'Dim strError As String = String.Empty
            'Try
            '    Dim oWebuserGE As AptifyGenericEntityBase
            '    Dim WebId As Integer
            '    Dim sSQLs As String
            '    Dim sSQL As String
            '    Dim param(2) As IDataParameter
            '    Dim oda As New DataAction
            '    Dim i As Integer
            '    Dim CompanyAddress As String = txtAddCompLine1.Text.Trim + " " + txtAddCompLine2.Text.Trim + " " + txtAddCompLine3.Text.Trim
            '    Dim Companyphone As String = txtAddCompPhoneAreaCode.Text.Trim + "-" + txtAddCompIntlCode.Text.Trim + "-" + txtAddCompPhone.Text.Trim

            '    If txtAddCompName.Text.Trim <> "" AndAlso CompanyAddress <> "" AndAlso txtAddCompCity.Text.Trim <> "" Then
            '        param(0) = oda.GetDataParameter("@City", SqlDbType.VarChar, txtAddCompCity.Text.Trim)
            '        param(1) = oda.GetDataParameter("@County", SqlDbType.VarChar, CompanyAddress)
            '        param(2) = oda.GetDataParameter("@Company", SqlDbType.NVarChar, txtAddCompName.Text.Trim)
            '        sSQL = "..GetCompanyData__c"
            '        i = CInt(oda.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, param))
            '        If i < 1 Then
            '            Dim dt As DataTable = New DataTable()
            '            sSQLs = Database & "..SpGetWebUserID__c @PersonID=" & User1.PersonID
            '            dt = Me.DataAction.GetDataTable(sSQLs)
            '            If dt.Rows.Count > 0 Then
            '                WebId = Convert.ToInt32(dt.Rows(0)("ID"))
            '            End If
            '            oWebuserGE = AptifyApplication.GetEntityObject("Web Users", CLng(WebId))
            '            If oWebuserGE IsNot Nothing Then
            '                With oWebuserGE
            '                    .SetValue("WebCompanyName__c", txtAddCompName.Text.Trim)
            '                    .SetValue("WebCompanyAddress__c", CompanyAddress)
            '                    .SetValue("WebCompanyCity__c", txtAddCompCity.Text.Trim)
            '                    .SetValue("WebCompanyCountry__c", cmbAddCompCountry.SelectedItem.Text.Trim)
            '                    .SetValue("WebCompanyState__c", cmbAddCompState.SelectedItem.Text.Trim)
            '                    .SetValue("WebCompanyPhone__c", Companyphone)
            '                    .SetValue("WebCompanyCounty__c", txtAddCompCounty.Text.Trim)
            '                    If .Save(False, strError) Then
            '                        bFlag = True
            '                    Else
            '                        bFlag = False
            '                    End If
            '                End With
            '            End If
            '        End If
            '    End If
            'Catch ex As Exception
            '    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            '    Return bFlag
            'End Try
            'Return bFlag
            Try

                Dim sSQL As String
                Dim param(2) As IDataParameter
                Dim oda As New DataAction
                Dim i As Integer
                Dim CompanyAddress As String = txtAddCompLine1.Text.Trim + " " + txtAddCompLine2.Text.Trim + " " + txtAddCompLine3.Text.Trim
                Dim Companyphone As String = txtAddCompPhoneAreaCode.Text.Trim + "-" + txtAddCompIntlCode.Text.Trim + "-" + txtAddCompPhone.Text.Trim

                If txtAddCompName.Text.Trim <> "" AndAlso CompanyAddress <> "" AndAlso txtAddCompCity.Text.Trim <> "" Then
                    param(0) = oda.GetDataParameter("@City", SqlDbType.VarChar, txtAddCompCity.Text.Trim)
                    param(1) = oda.GetDataParameter("@County", SqlDbType.VarChar, CompanyAddress)
                    param(2) = oda.GetDataParameter("@Company", SqlDbType.NVarChar, txtAddCompName.Text.Trim)
                    sSQL = "..GetCompanyData__c"
                    i = CInt(oda.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, param))
                    If i < 1 Then
                        Dim oCompany As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = Nothing
                        oCompany = AptifyApplication.GetEntityObject("Companies", -1)
                        oCompany.SetValue("Name", txtAddCompName.Text.Trim)
                        oCompany.SetValue("AddressLine1", txtAddCompLine1.Text.Trim)
                        oCompany.SetValue("AddressLine2", txtAddCompLine2.Text.Trim)
                        oCompany.SetValue("AddressLine3", txtAddCompLine3.Text.Trim)
                        oCompany.SetValue("City", txtAddCompCity.Text.Trim)
                        ' oCompany.SetValue("State", CStr(IIf(cmbAddCompState.SelectedIndex >= 0, cmbAddCompState.SelectedValue, "")))
                        ' commented and added new line by GM for Redmine #18656
                        ''oCompany.SetValue("Country", CStr(IIf(cmbAddCompCountry.SelectedIndex >= 0, cmbAddCompCountry.SelectedItem.Text.Trim, "")))
                        oCompany.SetValue("CountryCodeID", CStr(IIf(cmbAddCompCountry.SelectedIndex > 0, cmbAddCompCountry.SelectedValue, "")))
                        'End #18656
                        oCompany.SetValue("County", txtAddCompCounty.Text.Trim)
                        oCompany.SetValue("ZipCode", txtAddCompPostalCode.Text.Trim)
                        oCompany.SetValue("MainAreaCode", txtAddCompIntlCode.Text.Trim)
                        oCompany.SetValue("MainCountryCode", txtAddCompPhoneAreaCode.Text.Trim)
                        oCompany.SetValue("MainPhone", txtAddCompPhone.Text.Trim)
                        oCompany.SetValue("Status__c", "Pending")

                        'Added by Prachi on 08/20/2018 to Check the Company Dupe check for Redmine #15496
                        If Not oCompany Is Nothing AndAlso oCompany.IsDirty Then
                            Dim oCompanyDupeCheck As New Aptify.Consulting.Plugin.CompanyDupCheck__c.CompanyDupeCheck
                            Dim duplicateID() As Long = Nothing

                            If oCompanyDupeCheck.CheckForDuplicates(oCompany, duplicateID) = DuplicateCheckResult.Exact Then
                                oCompany = Nothing
                                'duplicateCompanyError.Style.Add("display", "block")
                                lblCompanyDupeCheck.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterCorrectCompanyNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                radCompanyDupeCheck.VisibleOnPageLoad = True

                            Else
                                If oCompany.Save(False) Then
                                    Dim newValue As String = Convert.ToString(oCompany.RecordID)
                                    Dim oPersonWithPedingChanges As AptifyGenericEntityBase
                                    oPersonWithPedingChanges = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                                    oPersonWithPedingChanges.SetValue("CompanyID", newValue)
                                    Dim bPendingRecordSaved = oPersonWithPedingChanges.SaveAsPendingChange(3, Date.Now(), "changed from web")
                                    Response.Redirect(Request.RawUrl)
                                End If
                            End If
                        End If

                        ' Below section commentd by Prachi for Redmin #15496
                        ' If oCompany.Save(False) Then
                        ' Dim newValue As String = Convert.ToString(oCompany.RecordID)
                        ' Dim oPersonWithPedingChanges As AptifyGenericEntityBase
                        ' oPersonWithPedingChanges = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                        ' oPersonWithPedingChanges.SetValue("CompanyID", newValue)
                        ' Dim bPendingRecordSaved = oPersonWithPedingChanges.SaveAsPendingChange(3, Date.Now(), "changed from web")
                        ' Response.Redirect(Request.RawUrl)
                        ' End If
                    Else

                    End If
                    ' End Redmine #15496
                End If
            Catch ex As Exception

            End Try
            Return False
        End Function
#End Region

#Region "Save Methods"
        Private Function DoSave_Edit() As Boolean
            Try
                Dim bNewUser As Boolean = False
                bNewUser = User1.UserID <= 0

                Dim oPerson As AptifyGenericEntityBase
                oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                Dim sPendingChangesField As String = String.Empty
                oPerson.SetValue("FirstName", txtFirstName.Text)
                oPerson.SetValue("LastName", txtLastName.Text.Trim)
                oPerson.SetValue("Title__c", txtJobTitle.Text.Trim)
                If cmbJobfunction.SelectedItem.Text <> "Select Job Title" Then
                    oPerson.SetValue("Title", cmbJobfunction.SelectedItem.Text.Trim)
                End If
                oPerson.SetValue("Email", txtEmail.Text.Trim)
                oPerson.SetAddValue("Email1", txtEmail.Text.Trim)
                oPerson.SetValue("PhoneCountryCode", txtlandlineCountryCode.Text.Trim)
                oPerson.SetValue("PhoneAreaCode", txtlandlineAreaCode.Text.Trim)
                oPerson.SetValue("Phone", txtlandlineNumber.Text.Trim)
                oPerson.SetValue("CellCountryCode", txtmobileCountryCode.Text.Trim)
                oPerson.SetValue("CellAreaCode", txtMobileAreaCode.Text.Trim)
                oPerson.SetValue("CellPhone", txtMobileNumber.Text.Trim)
                oPerson.SetValue("PreferredSalutation__c", txtPreferredSalutation.Text.Trim)
                oPerson.SetValue("Prefix", CStr(IIf(cmbSalutation.SelectedIndex >= 0, cmbSalutation.SelectedValue, "")))
                oPerson.SetValue("PrefPhoneType__c", "Mobile")
                If rblPAddress.SelectedValue = "Home" Then
                    oPerson.SetValue("PreferredAddress", "Home Address")
                    ''Added By Pradip 2017-02-21 for https://redmine.softwaredesign.ie/issues/16510
                    oPerson.SetValue("PreferredBillingAddress", "Home Address")
                    oPerson.SetValue("PreferredShippingAddress", "Home Address")
                ElseIf rblPAddress.SelectedValue = "Work" Then
                    oPerson.SetValue("PreferredAddress", "Business Address")
                    ''Added By Pradip 2017-02-21 for https://redmine.softwaredesign.ie/issues/16510
                    oPerson.SetValue("PreferredBillingAddress", "Business Address")
                    oPerson.SetValue("PreferredShippingAddress", "Business Address")
                End If
                If cmbempstatus.SelectedItem.Value IsNot Nothing Then
                    Dim statusID As Integer = CInt(IIf(cmbempstatus.SelectedIndex >= 0, cmbempstatus.SelectedValue, "-1"))
                    oPerson.SetValue("EmploymentStatusID__c", statusID)
                End If
                If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Persons", "PendingChangesFields__c")) Then
                    sPendingChangesField = CStr(AptifyApplication.GetEntityAttribute("Persons", "PendingChangesFields__c"))
                End If
                Dim aPendingChanges() As String = sPendingChangesField.Split(New Char() {","c})
                Dim isCompanyChanged As Boolean = False
                Dim oPersonOld As AptifyGenericEntityBase
                oPersonOld = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                For i As Integer = 0 To aPendingChanges.Count - 1
                    Dim sFieldName As String = aPendingChanges(i)
                    Dim oldValue As String = CStr(oPersonOld.GetValue(sFieldName))
                    Dim newValue As String = CStr(oPerson.GetValue(sFieldName))
                    If sFieldName = "Phone" Then
                        oldValue = oldValue.Replace("-", String.Empty)
                        newValue = newValue.Replace("-", String.Empty)
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

                        oPerson.SetValue(sFieldName, oPersonOld.GetValue(sFieldName))
                        If sFieldName = "CompanyID" Then
                            oPerson.SetValue(sFieldName, oldValue)
                            isCompanyChanged = True
                        End If
                    End If
                Next
                oPersonOld = Nothing
                Dim bRecordSaved As Boolean = False
                Dim sErr As String = String.Empty
                bRecordSaved = oPerson.Save(sErr)
                If bRecordSaved Then
                    'Commented as part of #20123
                    'If rblPAddress.SelectedValue = "Home" Then
                    '    User1.SetValue("PreferredBillingAddress", "Home Address")
                    '    User1.SetValue("PreferredShippingAddress", "Home Address")
                    '    User1.SetValue("PreferredAddress", "Home Address")
                    'ElseIf rblPAddress.SelectedValue = "Work" Then
                    '    User1.SetValue("PreferredBillingAddress", "Business Address")
                    '    User1.SetValue("PreferredShippingAddress", "Business Address")
                    '    User1.SetValue("PreferredAddress", "Business Address")
                    'End If
                    'User1.SetValue("Title", Convert.ToString(oPerson.GetValue("Title")))
                    'User1.Save()
                    Dim sError As String = ""
                    If SocialNetworkObject IsNot Nothing AndAlso SocialNetworkObject.IsConnected Then
                        SocialNetworkObject.UserProfile.EBusinessUser = User1
                        If SocialNetworkObject.UserProfile.SynchronizePersonExternalAccount(sError) Then
                            If bNewUser AndAlso SocialNetworkObject.UserProfile.SynchronizeProfile Then
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
                    Dim sOrderXML As String
                    If Session.Item(m_c_sPrefix & "OrderXML") IsNot Nothing Then
                        sOrderXML = Session.Item(m_c_sPrefix & "OrderXML").ToString

                        If sOrderXML.Length > 0 Then
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

        Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
            Try
                If DoSave_Edit() Then
                    WebUserLogin1.User.SaveValuesToSessionObject(Page.Session)
                    Session("UserLoggedIn") = True
                    If Request.QueryString("MemberShip") IsNot Nothing AndAlso Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("MemberShip"))) = "Y" Then
                        Response.Redirect(MembershipPage)
                    Else
                        Response.Redirect(Request.RawUrl)
                    End If
                Else
                    Response.Redirect(Request.RawUrl)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Dim intpassword As Integer
            If Not IsPasswordComplexPopup(txtNewPassword.Text) Then
                radwinPassword.VisibleOnPageLoad = True
                lblerrorLength.Visible = True
                CompareValidator.Enabled = True
                Exit Sub
            End If
            intpassword = WebUserLogin1.UpdateUserPassword(User1.WebUserStringID, txtoldpassword.Text, txtNewPassword.Text, Nothing, Nothing, Page.User)
            If (intpassword = 0) Then
                CompareValidator.Enabled = True
                lblPasswordsuccess.Text = "Password updated successfully!"
                radwinPassword.VisibleOnPageLoad = False
            End If
            If (intpassword = 1) Then
                radwinPassword.VisibleOnPageLoad = True
                CompareValidator.Enabled = True
                lblerrorLength.Text = "No user match or there is no access to the encryption key"
                Exit Sub
            End If
            If (intpassword = 2) Then
                radwinPassword.VisibleOnPageLoad = True
                CompareValidator.Enabled = False
                lblerrorLength.Text = "The current password you entered is incorrect. Please try again."
                Exit Sub
            End If
            If (intpassword = 4) Then
                radwinPassword.VisibleOnPageLoad = True
                CompareValidator.Enabled = True
                lblerrorLength.Text = "Current and new password should not same."
                Exit Sub
            End If
            If (intpassword = 3) Then
                radwinPassword.VisibleOnPageLoad = True
                CompareValidator.Enabled = True
                lblerrorLength.Text = "Password update failed."
                Exit Sub
            End If

        End Sub

        Protected Sub btnUpdateHomeAdd_Click(sender As Object, e As EventArgs) Handles btnUpdateHomeAdd.Click
            Try
                Dim oPerson As AptifyGenericEntityBase
                oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)

                ''Commented By Pradip 2017-04-06 to save pending changes and handle currency validation in Procss Flow
                'If Convert.ToString(oPerson.GetValue("HomeCountryCodeID")) <> CStr(IIf(ddlHomeAddCountry.SelectedIndex >= 0, ddlHomeAddCountry.SelectedValue, "")) Then
                '    Dim stringSql As String = AptifyApplication.GetEntityBaseDatabase("Persons") & "..spGetPreferredCurrencyTypeID__c @PersonID=" & User1.PersonID & ", @CompanyID=" & Convert.ToString(oPerson.GetValue("CompanyID")) '& ",@HomeCountryID =" & Convert.ToString(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, ""))
                '    Dim iResult As Integer = Convert.ToInt32(DataAction.ExecuteScalar(stringSql))

                '    If iResult > 0 Then

                '        ' If iResult <> Convert.ToInt32(oPerson.GetValue("PreferredCurrencyTypeID")) Then
                '        Dim strSql As String = AptifyApplication.GetEntityBaseDatabase("Persons") & "..spGetBalanceTotalForPerson__c @PersonID=" & User1.PersonID
                '        Dim iTotal As Decimal = CDec(DataAction.ExecuteScalar(strSql))
                '        If iTotal > 0 OrElse iTotal < 0 Then
                '            lblErrorHomeCountry.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Consulting.BalanceTotalValidation__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                '            LoadUserInfo()
                '            radUpdateHomeAddress.VisibleOnPageLoad = True
                '            Exit Sub
                '        End If
                '        'End If
                '    End If
                'End If
                AddAddressToPerson(oPerson, "Home")
                Response.Redirect(Request.RawUrl)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub cmbAddCompCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAddCompCountry.SelectedIndexChanged
            PopulateState(cmbAddCompState, cmbAddCompCountry)
        End Sub

        Protected Sub btnEditCompSave_Click(sender As Object, e As EventArgs) Handles btnEditCompSave.Click
            Try
                'Ashwini Junagade #18654 start
                If CDbl(hfCompanyID.Value) > 0 Then
                    'Ashwini Junagade #18654 end
                    If hidCheckFlag.Value.Trim = "Edit" Then
                        If hfCompanyID.Value <> hidCompanyIdOld.Value Then
                            Dim sPendingChangesField As String = String.Empty
                            If Not String.IsNullOrEmpty(AptifyApplication.GetEntityAttribute("Persons", "PendingChangesFields__c")) Then
                                sPendingChangesField = CStr(AptifyApplication.GetEntityAttribute("Persons", "PendingChangesFields__c"))
                            End If
                            Dim aPendingChanges() As String = sPendingChangesField.Split(New Char() {","c})
                            Dim isCompanyChanged As Boolean = False
                            Dim oldValue As String = CStr(hidCompanyIdOld.Value)
                            Dim newValue As String = CStr(hfCompanyID.Value)
                            Dim oPersonWithPedingChanges As AptifyGenericEntityBase
                            oPersonWithPedingChanges = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                            oPersonWithPedingChanges.SetValue("CompanyID", newValue)
                            Dim bPendingRecordSaved = oPersonWithPedingChanges.SaveAsPendingChange(3, Date.Now(), "changed from web")
                            isCompanyChanged = True
                            Response.Redirect(Request.RawUrl)
                        End If
                    ElseIf hidCheckFlag.Value.Trim = "Add" Then
                        Dim oPerson As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = Nothing
                        oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                        With oPerson.SubTypes("PersonCompanies").Add()
                            .SetValue("CompanyId", hfCompanyID.Value)
                            .SetValue("StartDate", New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day))
                            .SetValue("Title", "")
                        End With
                        If oPerson.Save(False) Then
                            Response.Redirect(Request.RawUrl)
                        End If
                    End If
                    'Ashwini Junagade #18654 start
                Else

                    radChangeCompany.VisibleOnPageLoad() = True
                    'lblChooseCompany.Text = "You must pick a company from the pre-existing list"
                    'lblChooseCompany.Visible = True
                End If
                'Ashwini Junagade #18654 end
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnAddNewCompany_Click(sender As Object, e As EventArgs) Handles btnAddNewCompany.Click
            Try
                If hidCheckFlag.Value.Trim = "Edit" Then
                    If SaveWebUsercompany() Then
                        Response.Redirect(Request.RawUrl)
                    End If
                ElseIf hidCheckFlag.Value.Trim = "Add" Then
                    Dim oCompany As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = Nothing
                    oCompany = AptifyApplication.GetEntityObject("Companies", -1)
                    oCompany.SetValue("Name", txtAddCompName.Text.Trim)
                    oCompany.SetValue("AddressLine1", txtAddCompLine1.Text.Trim)
                    oCompany.SetValue("AddressLine2", txtAddCompLine2.Text.Trim)
                    oCompany.SetValue("AddressLine3", txtAddCompLine3.Text.Trim)
                    oCompany.SetValue("City", txtAddCompCity.Text.Trim)
                    ' oCompany.SetValue("State", CStr(IIf(cmbAddCompState.SelectedIndex > 0, cmbAddCompState.SelectedValue, "")))
                    ' Start #18350
                    ''oCompany.SetValue("Country", CStr(IIf(cmbAddCompCountry.SelectedIndex > 0, cmbAddCompCountry.SelectedValue, "")))
                    'https://redmine.softwaredesign.ie/issues/18350 above line commented by Govind M and added below Country code ID line
                    oCompany.SetValue("CountryCodeID", CStr(IIf(cmbAddCompCountry.SelectedIndex > 0, cmbAddCompCountry.SelectedValue, "")))
                    'End #18350
                    oCompany.SetValue("County", txtAddCompCounty.Text.Trim)
                    oCompany.SetValue("ZipCode", txtAddCompPostalCode.Text.Trim)

                    oCompany.SetValue("MainAreaCode", txtAddCompPhoneAreaCode.Text.Trim)
                    oCompany.SetValue("MainCountryCode", txtAddCompIntlCode.Text.Trim)
                    oCompany.SetValue("MainPhone", txtAddCompPhone.Text.Trim)
                    oCompany.SetValue("Status__c", "Pending")
                    'Added by Prachi to check Company Dupe Check for Redmine #15496
                    If Not oCompany Is Nothing AndAlso oCompany.IsDirty Then
                        Dim oCompanyDupeCheck As New Aptify.Consulting.Plugin.CompanyDupCheck__c.CompanyDupeCheck

                        oCompany.SetValue("Name", txtAddCompName.Text)
                        oCompany.SetValue("AddressLine1", txtAddCompLine1.Text.Trim)

                        'oCompany.SetValue("MainEmail", "NewFirm@abc.com")
                        Dim duplicateID() As Long = Nothing

                        If oCompanyDupeCheck.CheckForDuplicates(oCompany, duplicateID) = DuplicateCheckResult.Exact Then
                            oCompany = Nothing
                            'duplicateCompanyError.Style.Add("display", "block")
                            lblCompanyDupeCheck.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLEnrolmentPage.EnterCorrectCompanyNameMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radCompanyDupeCheck.VisibleOnPageLoad = True
                        Else

                            oCompany.Save(False)
                        End If
                    End If
                    'oCompany.Save(False) ' Comment code by Prachi for Redmine #15496
                    ' End Redmine #15496
                    Dim oPerson As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = Nothing
                    oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                    With oPerson.SubTypes("PersonCompanies").Add()
                        .SetValue("CompanyId", oCompany.RecordID)
                        .SetValue("StartDate", New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day))
                        .SetValue("Title", "")
                        oPerson.Save(False)
                        Response.Redirect(Request.RawUrl)
                    End With
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Code added by Prachi for Redmine #15496
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Protected Sub btnCopanyDupeCheck_Click(sender As Object, e As System.EventArgs) Handles btnCopanyDupeCheck.Click
            Try
                radCompanyDupeCheck.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''Énd Redmine #15496
        Protected Sub btnDeleteCompanyPopup_Click(sender As Object, e As EventArgs) Handles btnDeleteCompanyPopup.Click
            Try
                Dim isCompanyChanged As Boolean = False
                Dim oldValue As String = CStr(hidCompanyIdOld.Value)
                Dim newValue As String = "-1"
                Dim oPersonWithPedingChanges As AptifyGenericEntityBase
                oPersonWithPedingChanges = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                oPersonWithPedingChanges.SetValue("CompanyID", newValue)
                Dim bPendingRecordSaved = oPersonWithPedingChanges.SaveAsPendingChange(3, Date.Now(), "changed from web")
                isCompanyChanged = True
                Response.Redirect(Request.RawUrl)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub lstAddress_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles lstAddress.ItemCommand
            If e.CommandName.ToString = "btnDeleteAddress" Then
                Dim oPerson As AptifyGenericEntityBase
                oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                Dim oPersonCompanyGE As AptifyGenericEntityBase
                oPersonCompanyGE = oPerson.SubTypes("PersonCompanies").Find("ID", e.CommandArgument.ToString)
                If oPersonCompanyGE IsNot Nothing Then
                    oPersonCompanyGE.SetValue("EndDate", New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day))
                    oPerson.Save()
                    Response.Redirect(Request.RawUrl)
                End If
            Else

            End If
        End Sub
        Protected Sub ddlHomeAddCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHomeAddCountry.SelectedIndexChanged
            PopulateState(ddlHomeAddState, ddlHomeAddCountry)
        End Sub
    End Class
End Namespace

