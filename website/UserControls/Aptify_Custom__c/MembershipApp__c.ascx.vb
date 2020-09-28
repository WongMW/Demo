'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande              12/16/2011                      Create Membership Application and place the order for Membership App
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Partial Class MembershipApp__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "MembershipApp__c"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_COMPANY_LOGO_IMAGE_URL As String = "CompanyLogoImage"
    Protected Const ATTRIBUTE_MembershipAppAbatmentsForm As String = "MembershipAppAbatmentsForm"
    Protected Const ATTRIBUTE_AbatementForm As String = "AbatementForm"
    Protected Const ATTRIBUTE_ViewCartPage As String = "ViewCart"
    Protected Const ATTRIBUTE_SaveButtonText As String = "MembershipAppSave"
    Protected Const ATTRIBUTE_SubmitBtnText As String = "MembershipAppBtnSubmit"
    Protected Const ATTRIBUTE_SubmitAndPay As String = "MembershipAppSubmitAndPay"
    Protected Const ATTRIBUTE_MakePaymentFormPage As String = "MakePaymentForm"
    ''Added BY Pradip 2016-03-15 For G1-77 Tracker Item
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage As String = "ProfilePage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage As String = "ReportPage"
    Dim lApplicationID As Long
#Region "Property Setting"
    Public Overridable Property MakePaymentFormPage() As String
        Get
            If Not ViewState(ATTRIBUTE_MakePaymentFormPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_MakePaymentFormPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_MakePaymentFormPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property ViewCart() As String
        Get
            If Not ViewState(ATTRIBUTE_ViewCartPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_ViewCartPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_ViewCartPage) = Me.FixLinkForVirtualPath(value)
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
    Protected Overridable ReadOnly Property MembershipAppAbatmentsForm() As String
        Get
            If Not Session.Item(ATTRIBUTE_MembershipAppAbatmentsForm) Is Nothing Then
                Return CStr(Session.Item(ATTRIBUTE_MembershipAppAbatmentsForm))
            Else
                Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_MembershipAppAbatmentsForm)
                If Not String.IsNullOrEmpty(value) Then
                    Session.Item(ATTRIBUTE_MembershipAppAbatmentsForm) = value
                    Return value
                Else
                    Return String.Empty
                End If
            End If
        End Get

    End Property
    Protected Overridable ReadOnly Property MembershipAppSaveButtonText() As String
        Get
            If Not Session.Item(ATTRIBUTE_SaveButtonText) Is Nothing Then
                Return CStr(Session.Item(ATTRIBUTE_SaveButtonText))
            Else
                Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_SaveButtonText)
                If Not String.IsNullOrEmpty(value) Then
                    Session.Item(ATTRIBUTE_SaveButtonText) = value
                    Return value
                Else
                    Return String.Empty
                End If
            End If
        End Get

    End Property
    Protected Overridable ReadOnly Property MembershipAppSubmitButtonText() As String
        Get
            If Not Session.Item(ATTRIBUTE_SubmitBtnText) Is Nothing Then
                Return CStr(Session.Item(ATTRIBUTE_SubmitBtnText))
            Else
                Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_SubmitBtnText)
                If Not String.IsNullOrEmpty(value) Then
                    Session.Item(ATTRIBUTE_SubmitBtnText) = value
                    Return value
                Else
                    Return String.Empty
                End If
            End If
        End Get

    End Property
    Protected Overridable ReadOnly Property MembershipAppSubmitAndPayButtonText() As String
        Get
            If Not Session.Item(ATTRIBUTE_SubmitAndPay) Is Nothing Then
                Return CStr(Session.Item(ATTRIBUTE_SubmitAndPay))
            Else
                Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_SubmitAndPay)
                If Not String.IsNullOrEmpty(value) Then
                    Session.Item(ATTRIBUTE_SubmitAndPay) = value
                    Return value
                Else
                    Return String.Empty
                End If
            End If
        End Get

    End Property
    Public Overridable Property CompanyLogoImage() As String
        Get
            If Not ViewState(ATTRIBUTE_COMPANY_LOGO_IMAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_COMPANY_LOGO_IMAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_COMPANY_LOGO_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property AbatementFormPage() As String
        Get
            If Not ViewState(ATTRIBUTE_AbatementForm) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_AbatementForm))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_AbatementForm) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    ''Added BY Pradip 2016-03-15 For G1-77
    Public Overridable Property ProfilePage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property ReportPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property


    Protected Overrides Sub SetProperties()

        If String.IsNullOrEmpty(CompanyLogoImage) Then
            CompanyLogoImage = Me.GetLinkValueFromXML(ATTRIBUTE_COMPANY_LOGO_IMAGE_URL)
            Me.companyLogo.Src = CompanyLogoImage
        End If
        If String.IsNullOrEmpty(AbatementFormPage) Then
            AbatementFormPage = Me.GetLinkValueFromXML(ATTRIBUTE_AbatementForm)
        End If
        If String.IsNullOrEmpty(ViewCart) Then
            ViewCart = Me.GetLinkValueFromXML(ATTRIBUTE_ViewCartPage)
        End If
        If String.IsNullOrEmpty(MakePaymentFormPage) Then
            MakePaymentFormPage = Me.GetLinkValueFromXML(ATTRIBUTE_MakePaymentFormPage)
        End If
        If String.IsNullOrEmpty(LoginPage) Then
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
        End If
        ''Added BY Pradip 2016-03-15 For G1-77
        If String.IsNullOrEmpty(ProfilePage) Then
            ProfilePage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage)
        End If

        If String.IsNullOrEmpty(ReportPage) Then
            ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_ReportPage)
        End If

    End Sub
#End Region

#Region "Page Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If AptifyEbusinessUser1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            End If
            ''Added By Pradip 2016-06-30 For Redmine Issue https://redmine.softwaredesign.ie/issues/14557
            regexEmailValid.ValidationExpression = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.EmailRegularExpressionValidator__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            LoadCultureStringMessage()
            btnPrint.Visible = False

            If Not IsPostBack Then
                If AptifyEbusinessUser1.PersonID > 0 Then
                    SetProperties()
                    LoadCountryDropDown()
                    GetMembershipStatus()
                    DisplayMembershipDetails()
                    LoadRecord()
                    LoadTopicCodes("IT Programme", dlITProgramme)
                    LoadTopicCodes("Company Law Module", dlCompanyLawModule)
                    LoadTopicCodes("Recognised Experience For Qualification", dlREQ)
                    SetITProgrammeTopicCodes("IT Programme")
                    SetCompanyLawModuleTopicCodes("Company Law Module")
                    SetRecognisedExperienceForQualificationTopicCodes("Recognised Experience For Qualification")
                    DisplayDataAsPerApplicationType()
                    GetPrefferedCurrency()
                    LoadResiprocalQuatationAmount()
                    ''''''Added BY Pradip 2016-03-05 For G1-77
                    LoadEducationContractCompanyAddress()
                    ''Added BY PRadip 2016-03-16 For Disabling Submit and Pay button if Pending Changes Exits For Person
                    CheckPendingChanges()

                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub
#End Region

#Region "Load Data"
    Private Sub LoadResiprocalQuatationAmount()
        Try
            Dim oGE As AptifyGenericEntityBase
            oGE = AptifyApplication.GetEntityObject("Persons", AptifyEbusinessUser1.PersonID)
            Dim lApplicationID As Long = Convert.ToInt32(oGE.GetValue("ApplicationTypeID__c"))

            Dim sSql As String = "..spGetMembershipProduct__c @ApplicationID=" & lApplicationID
            Dim ProductID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql))
            If ProductID > 0 Then
                Dim sOrderIDSQL As String = Database & "..spGetMembershipProductFromGroupProductID__c @GroupProductID=" & ProductID & ",@ShipToID=" & oGE.GetValue("ID")
                Dim lOrderID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sOrderIDSQL))
                If lOrderID > 0 Then
                    Dim sSqlResiprocalQuat As String = Database & "..spGetReciprocalQuotationAmt__c @OrderID=" & lOrderID
                    Dim dQuatAmount As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sSqlResiprocalQuat, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If dQuatAmount > 0 Then
                        txtRemittanceAmount.Text = Format(dQuatAmount, "0.00")
                    Else

                    End If
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetPrefferedCurrency()
        Try
            Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & AptifyEbusinessUser1.PersonID
            'Dim sSymbol As String = Convert.ToString(DataAction.ExecuteScalar(sSql))
            'If sSymbol <> "" Then
            '    lblPrefeeredCurrency.Text = sSymbol
            'End If
            Dim dt As DataTable = DataAction.GetDataTable(sSql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                lblPrefeeredCurrency.Text = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                ViewState("CurrencyTypeID") = Convert.ToInt32(dt.Rows(0)("ID"))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetMembershipStatus()
        Try
            Dim sSQL As String = Database & "..spGetMembershipAppStatus__c @Id=" & AptifyApplication.GetEntityRecordIDFromRecordName("MembershipApplicationStatus__c", "In Progress")
            Dim sWebName As String = Convert.ToString(DataAction.ExecuteScalar(sSQL))
            If sWebName.Trim <> "" Then
                lblApplicationType.Text = sWebName
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DisplayDataAsPerApplicationType()
        Try
            Dim oGE As AptifyGenericEntityBase
            oGE = AptifyApplication.GetEntityObject("Persons", AptifyEbusinessUser1.PersonID)
            lApplicationID = Convert.ToInt32(oGE.GetValue("ApplicationTypeID__c"))
            ViewState("ApplicationID") = lApplicationID
            If Convert.ToString(AptifyApplication.GetEntityRecordName("ApplicationTypes__c", lApplicationID)).Trim.ToLower = "contract" Then
                idREQ.Visible = False
                'idTraining.Visible = True
                'idTrainingCompleted.Visible = True
                'idTrainingExpiryDate.Visible = True
                'idTrainingFirmName.Visible = True
                'idTrainingAddressLine1.Visible = True
                'idTrainingAddressLine2.Visible = True
                'idTrainingAddressLine3.Visible = True
                'idTrainingCountry.Visible = True
                'idTrainingCityState.Visible = True
                'idTrainingTelephoneNo.Visible = True
                ''Added BY Pradip 2016-07-11 For Red Mine Issue https://redmine.softwaredesign.ie/issues/14617
                idTraining.Visible = False
                idTrainingCompleted.Visible = False
                idTrainingExpiryDate.Visible = False
                idTrainingFirmName.Visible = False
                idTrainingAddressLine1.Visible = False
                idTrainingAddressLine2.Visible = False
                idTrainingAddressLine3.Visible = False
                idTrainingCountry.Visible = False
                idTrainingCityState.Visible = False
                idTrainingTelephoneNo.Visible = False
                idChkExpSummary.Visible = False
                'idChkExpSummary.Visible = True
                idExamDetails.Visible = True
                idEvevationProg.Visible = False
                trTrainingInBusiness.Visible = True
                trTrainingInBusinessText.Visible = True
                trTrainingInBusinessStudSign.Visible = True
                trTrainingInBusinessDate.Visible = True
                trTrainingInBusinessBottomLine.Visible = True
            End If
            If Convert.ToString(AptifyApplication.GetEntityRecordName("ApplicationTypes__c", lApplicationID)).Trim.ToLower = "elevation" Then
                idREQ.Visible = True
                idEvevationProg.Visible = True
                '  lblTrainingAndCAIDiary.Text = "CA DIARY DETAILS"
                idTraining.Visible = False
                idTrainingCompleted.Visible = False
                idTrainingExpiryDate.Visible = False
                idTrainingFirmName.Visible = False
                idTrainingAddressLine1.Visible = False
                idTrainingAddressLine2.Visible = False
                idTrainingAddressLine3.Visible = False
                idTrainingCountry.Visible = False
                idTrainingCityState.Visible = False
                idTrainingTelephoneNo.Visible = False
                idChkExpSummary.Visible = False
                idExamDetails.Visible = False
                trTrainingInBusiness.Visible = False
                trTrainingInBusinessText.Visible = False
                trTrainingInBusinessStudSign.Visible = False
                trTrainingInBusinessDate.Visible = False
                trTrainingInBusinessBottomLine.Visible = False
            End If
            If Convert.ToString(AptifyApplication.GetEntityRecordName("ApplicationTypes__c", lApplicationID)).Trim.ToLower = "reciprocal (a)" Or Convert.ToString(AptifyApplication.GetEntityRecordName("ApplicationTypes__c", lApplicationID)).Trim.ToLower = "reciprocal (a) – aca" Or Convert.ToString(AptifyApplication.GetEntityRecordName("ApplicationTypes__c", lApplicationID)).Trim.ToLower = "reciprocal (a) – fca" Then
                ''Addfed BY Pradip 2016-07-11 For Red Mine Issue https://redmine.softwaredesign.ie/issues/14707
                pnleducation.Visible = False
                pnleducation1.Visible = False
                ''End Addfed BY Pradip 2016-07-11 For Red Mine Issue https://redmine.softwaredesign.ie/issues/14707
                Trainning.Visible = False
                idREQ.Visible = False
                idExamDetails.Visible = False
                'idCertificateDetails.Visible = False
                idFAE.Visible = False
                idCurrentMembership.Visible = True

                trNameOfCertificateSignedText.Visible = False
                trNameOfCertificateMemberInst.Visible = False
                trNameOfCertificateStudent.Visible = False
                trNameOfCertificateFromToDate.Visible = False
                trNameOfCertificateAdmittedText.Visible = False
                trNameOfCertificatesignCA.Visible = False
                trNameOfCertificatesignStuDate.Visible = False
                trTrainingInBusiness.Visible = False
                trTrainingInBusinessText.Visible = False
                trTrainingInBusinessStudSign.Visible = False
                trTrainingInBusinessDate.Visible = False
                trTrainingInBusinessBottomLine.Visible = False

                lblAssociateMemberText.Visible = False
                lblResiprocalText.Visible = True
                lblResiprocalLoweerText.Visible = True
                ''If Convert.ToInt32(ViewState("Orderid")) > 0 Then
                ''    Dim sSql As String = Database & "..spGetReciprocalQuotationAmt__c @OrderID=" & Convert.ToInt32(ViewState("Orderid"))
                ''    Dim dQuatAmount As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                ''    If dQuatAmount > 0 Then
                ''        lblQuationAmount.Text = Format(dQuatAmount, "0.00")
                ''        trQuatAmt.Visible = True
                ''    Else
                ''        trQuatAmt.Visible = False
                ''    End If
                ''End If
            End If
            If Convert.ToString(AptifyApplication.GetEntityRecordName("ApplicationTypes__c", lApplicationID)).Trim.ToLower = "reciprocal (b)" Or Convert.ToString(AptifyApplication.GetEntityRecordName("ApplicationTypes__c", lApplicationID)).Trim.ToLower = "reciprocal (b) – aca" Or Convert.ToString(AptifyApplication.GetEntityRecordName("ApplicationTypes__c", lApplicationID)).Trim.ToLower = "reciprocal (b) – fca" Then

                ''Addfed BY Pradip 2016-07-11 For Red Mine Issue https://redmine.softwaredesign.ie/issues/14707
                pnleducation.Visible = False
                pnleducation1.Visible = False
                ''End Addfed BY Pradip 2016-07-11 For Red Mine Issue https://redmine.softwaredesign.ie/issues/14707
                Trainning.Visible = False
                idREQ.Visible = False
                idExamDetails.Visible = False
                'idCertificateDetails.Visible = False
                idFAE.Visible = False
                idCurrentMembership.Visible = True
                lblCurrentMemUpperText.Visible = False
                lblCurrentMembershipResiB.Visible = True
                trTrainingInBusiness.Visible = False
                trTrainingInBusinessText.Visible = False
                trTrainingInBusinessStudSign.Visible = False
                trTrainingInBusinessDate.Visible = False
                trTrainingInBusinessBottomLine.Visible = False
                trNameOfCertificateSignedText.Visible = False
                trNameOfCertificateMemberInst.Visible = False
                trNameOfCertificateStudent.Visible = False
                trNameOfCertificateFromToDate.Visible = False
                trNameOfCertificateAdmittedText.Visible = False
                trNameOfCertificatesignCA.Visible = False
                trNameOfCertificatesignStuDate.Visible = False
                trTrainingInBusiness.Visible = False
                trTrainingInBusinessText.Visible = False
                trTrainingInBusinessStudSign.Visible = False
                trTrainingInBusinessDate.Visible = False
                trTrainingInBusinessBottomLine.Visible = False
                lblAssociateMemberText.Visible = False
                lblResiprocalText.Visible = True
                lblResiprocalLoweerText.Visible = False
                lblResiprocalBLowerText.Visible = True
                ''If Convert.ToInt32(ViewState("Orderid")) > 0 Then
                ''    Dim sSql As String = Database & "..spGetReciprocalQuotationAmt__c @OrderID=" & Convert.ToInt32(ViewState("Orderid"))
                ''    Dim dQuatAmount As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                ''    If dQuatAmount > 0 Then
                ''        lblQuationAmount.Text = Format(dQuatAmount, "0.00")
                ''        trQuatAmt.Visible = True
                ''    Else
                ''        trQuatAmt.Visible = False
                ''    End If
                ''End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Get ALl Culture Message
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadCultureStringMessage()
        Try
            ' RegularExpressionValidator6.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.EnterCorrectDateformat")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            '  RegularExpressionValidator7.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.EnterCorrectDateformat")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            ' RegularExpressionValidator8.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.EnterCorrectDateformat")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            '  RegularExpressionValidator9.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.EnterCorrectDateformat")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            ' RegularExpressionValidator10.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.EnterCorrectDateformat")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            'RegularExpressionValidator4.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.EnterCorrectDateformat")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            ' RegularExpressionValidator5.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.EnterCorrectDateformat")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblAssociateMemberText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.AssociateMemberText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblAdmited.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.CertificateDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblTrainingInBusiness.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.TrainingInBusiness")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblEducation.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.EducationText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            cmpCalenders.ErrorMessage = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.DateValidation")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblRegistryDept.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.RegistryDept")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            chkFinalTrainingReviewEnclosed.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.CADiaryFinalTrainingReview")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            chkExpSummaryFormEnclosed.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.CADiarySummaryFormEnclose")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblEvevationProgramme.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.ElevationProgramme")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblEvlevationText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.ElevationProgrammeText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblinBusinessTextLine.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.TraininginBusinessTextBelow")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblCurrentMemUpperText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.CurrentMembershipReciprocalAText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblCuuentMembershipLowerText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.CurrentMembershipReciprocalBelowText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblRemittanceUpperText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.CurrentMembershipRemittanceUpperText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblRemittanceLowerText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.CurrentMembershipRemittanceLowerText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblCurrentMembershipResiB.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.CurrentMembershipReciprocalBText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblResiprocalText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.ApplicationResiprocalAText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '
            lblResiprocalLoweerText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.ApplicationResiprocalALowerText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '
            lblResiprocalBLowerText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.ApplicationResiprocalBLowerText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '
            ' For button Text call Navigation file text
            If MembershipAppSaveButtonText <> "" Then
                cmdSave.Text = MembershipAppSaveButtonText
            End If
            If MembershipAppSubmitButtonText <> "" Then
                btnSubmit.Text = MembershipAppSubmitButtonText
            End If
            If MembershipAppSubmitAndPayButtonText <> "" Then
                btnSubmitAndPay.Text = MembershipAppSubmitAndPayButtonText
            End If
            lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.PopupText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '
            lblSubmitPayMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.PopupText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load Orgnization data
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub LoadRecord()
        Try
            GetOrganisationDetails()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load Country Drop down 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadCountryDropDown()
        Try
            cmbCountry.ClearSelection()
            cmbTrainingFirmCountry.ClearSelection()
            cmbFirmCountry.ClearSelection()
            Dim sSql As String
            Dim dt As DataTable
            sSql = AptifyApplication.GetEntityBaseDatabase("Addresses") & _
                       "..spGetCountryList"
            dt = DataAction.GetDataTable(sSql)
            cmbCountry.DataSource = dt
            cmbCountry.DataTextField = "Country"
            cmbCountry.DataValueField = "ID"
            cmbCountry.DataBind()

            cmbTrainingFirmCountry.DataSource = dt
            cmbTrainingFirmCountry.DataTextField = "Country"
            cmbTrainingFirmCountry.DataValueField = "ID"
            cmbTrainingFirmCountry.DataBind()

            cmbFirmCountry.DataSource = dt
            cmbFirmCountry.DataTextField = "Country"
            cmbFirmCountry.DataValueField = "ID"
            cmbFirmCountry.DataBind()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Populate State Drop down
    ''' </summary>
    ''' <param name="cmbPopulateState"></param>
    ''' <param name="cmbCurrentCountry"></param>
    ''' <remarks></remarks>
    Private Sub PopulateState(ByRef cmbPopulateState As DropDownList, ByRef cmbCurrentCountry As DropDownList)
        Try
            cmbPopulateState.ClearSelection()

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
    ''' <summary>
    ''' Set Country and State field
    ''' </summary>
    ''' <param name="cmb"></param>
    ''' <param name="sValue"></param>
    ''' <remarks></remarks>
    Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList, _
                                  ByVal sValue As String)
        Dim i As Integer

        Try
            cmb.ClearSelection()
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
    ''' <summary>
    ''' Get Organisation detail and set the label
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub GetOrganisationDetails()
        Try
            Dim sqlstr As String = String.Empty
            Dim dtOrganisationDetails As System.Data.DataTable = New DataTable()
            sqlstr = Database & "..SpGetOrganisationDetails__c"
            dtOrganisationDetails = Me.DataAction.GetDataTable(sqlstr)
            ''Added BY PRadip 2016-03-17 To Avoid Exception if No Data in datatable
            If dtOrganisationDetails IsNot Nothing AndAlso dtOrganisationDetails.Rows.Count > 0 Then
                lblAddress.Text = ((dtOrganisationDetails.Rows(0)("Line1")) + " " + (dtOrganisationDetails.Rows(0)("Line2")) + " " + (dtOrganisationDetails.Rows(0)("Line3")) + " " + (dtOrganisationDetails.Rows(0)("Line4")) + " " + (dtOrganisationDetails.Rows(0)("City")) + " " + (dtOrganisationDetails.Rows(0)("PostalCode")) + " " + (dtOrganisationDetails.Rows(0)("Expr1")))
            End If
            'lblAddress.Text = ((dtOrganisationDetails.Rows(0)("Line1")) + " " + (dtOrganisationDetails.Rows(0)("Line2")) + " " + (dtOrganisationDetails.Rows(0)("Line3")) + " " + (dtOrganisationDetails.Rows(0)("Line4")) + " " + (dtOrganisationDetails.Rows(0)("City")) + " " + (dtOrganisationDetails.Rows(0)("PostalCode")) + " " + (dtOrganisationDetails.Rows(0)("Expr1")))
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' get Person details
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub GetPersonDetails()
        Try
            Dim sSQL As String = String.Empty
            Dim dtpersondetails As System.Data.DataTable = New DataTable()
            sSQL = Database & "..SpSelectPersonDetails__c @PersonID=" & AptifyEbusinessUser1.PersonID.ToString()
            dtpersondetails = Me.DataAction.GetDataTable(sSQL)
            If Not dtpersondetails Is Nothing AndAlso dtpersondetails.Rows.Count > 0 Then
                lblfname.Text = Convert.ToString(dtpersondetails.Rows(0)("FirstName"))
                lblsname.Text = Convert.ToString(dtpersondetails.Rows(0)("MiddleName"))
                lbllname.Text = Convert.ToString(dtpersondetails.Rows(0)("LastName"))
                TxtLine1.Text = Convert.ToString(dtpersondetails.Rows(0)("Line1"))
                TxtLine2.Text = Convert.ToString(dtpersondetails.Rows(0)("Line2"))
                TxtLine3.Text = Convert.ToString(dtpersondetails.Rows(0)("Line3"))

                SetComboValue(cmbCountry, IIf(dtpersondetails.Rows(0)("Country") = "", "Ireland", dtpersondetails.Rows(0)("Country")).ToString)
                PopulateState(cmbState, cmbCountry)
                SetComboValue(cmbState, Convert.ToString(dtpersondetails.Rows(0)("StateProvince")))
                txtCity.Text = Convert.ToString(dtpersondetails.Rows(0)("City"))
                txtZipCode.Text = Convert.ToString(dtpersondetails.Rows(0)("PostalCode"))
                TxtEmail.Text = Convert.ToString(dtpersondetails.Rows(0)("Email1")).Trim
                TxtLandlineNo.Text = Convert.ToString(dtpersondetails.Rows(0)("Phone"))
                Txtpmobileno.Text = dtpersondetails.Rows(0)("cellphone").ToString.Trim


                If Convert.ToString(dtpersondetails.Rows(0)("Birthday")).Trim <> "" Then
                    If Not Convert.ToString(dtpersondetails.Rows(0)("Birthday")) Is Nothing AndAlso IsDBNull(Convert.ToString(dtpersondetails.Rows(0)("Birthday"))) = False AndAlso Convert.ToString(dtpersondetails.Rows(0)("Birthday")) <> "01/01/1777 00:00:00" Then
                        rdpBirthdate.SelectedDate = dtpersondetails.Rows(0)("Birthday")
                    End If
                End If
                rdpformentrydate.Text = Date.Today
                GetCompanyDetails(AptifyEbusinessUser1.CompanyID)
                SetComboValue(cmbTrainingFirmCountry, "Ireland")
                txtTrainingFirmTelephone.Text = 1
                ''Added By Pradip 2016-06-30 For Redmine Issue https://redmine.softwaredesign.ie/issues/14557
                hidCompanyID.Value = Convert.ToString(AptifyEbusinessUser1.CompanyID)
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetCompanyDetails(ByVal CompanyID As Long)
        Try
            Dim sSQL As String = Database & "..spGetMembershipCompanyDetails__c @CompanyID=" & CompanyID
            Dim dt As DataTable = DataAction.GetDataTable(sSQL)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                txtFirmName.Text = Convert.ToString(dt.Rows(0)("Name"))
                txtFirmLine1.Text = Convert.ToString(dt.Rows(0)("AddressLine1"))
                txtFirmLine2.Text = Convert.ToString(dt.Rows(0)("AddressLine2"))
                txtFirmLine3.Text = Convert.ToString(dt.Rows(0)("AddressLine3"))
                SetComboValue(cmbFirmCountry, IIf(dt.Rows(0)("Country") = "", "Ireland", dt.Rows(0)("Country")).ToString)
                PopulateState(cmbFirmState, cmbFirmCountry)
                SetComboValue(cmbFirmState, dt.Rows(0)("State"))
                txtFirmZipCode.Text = Convert.ToString(dt.Rows(0)("ZipCode"))
                txtFirmCity.Text = Convert.ToString(dt.Rows(0)("City"))
                txtFirmTelephoneNo.Text = Convert.ToString(dt.Rows(0)("MainPhone"))

                txtFirmFaxNo.Text = Convert.ToString(dt.Rows(0)("MainFax"))
                If txtFirmTelephoneNo.Text.Trim = "" Then
                    txtFirmTelephoneNo.Text = 1

                End If
            Else
                SetComboValue(cmbFirmCountry, "Ireland")
                PopulateState(cmbFirmState, cmbFirmCountry)
                If txtFirmTelephoneNo.Text.Trim = "" Then
                    txtFirmTelephoneNo.Text = 1
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' get membership detilas
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub DisplayMembershipDetails()
        Try
            'redmine 17199
            btnPrint.Visible = False
            Dim oGE As AptifyGenericEntityBase
            oGE = AptifyApplication.GetEntityObject("Persons", AptifyEbusinessUser1.PersonID)
            If Convert.ToString(oGE.GetValue("DateEligibleToBill__c")) <> "" AndAlso Convert.ToString(oGE.GetValue("DateEligibleToBill__c")) <> "1900-1-1" Then
                btnSubmitAndPay.Visible = True
            Else
                btnSubmitAndPay.Visible = False
            End If
            Dim lApplicationId As Long = Convert.ToInt32(oGE.GetValue("ApplicationTypeID__c"))
            GetWebApplicationTypeName(lApplicationId)
            Dim sSql As String = Database & "..spGetMembershipDetails__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@AppTypeID=" & lApplicationId
            ' Dim IsAbatement As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSql))
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ViewState("MembershipAppID") = Convert.ToInt32(dt.Rows(0)("ID"))
                Dim isAbatement As Boolean = Convert.ToBoolean(dt.Rows(0)("IsEligibleForAbatement"))
                If isAbatement Then
                    If MembershipAppAbatmentsForm <> "" Then
                        pnlAbatment.Visible = True
                        pnlAbatementDetail.Visible = True
                        lblAbatement.Text = MembershipAppAbatmentsForm
                    End If
                    ' Display Abatement form
                End If
                lblfname.Text = AptifyEbusinessUser1.FirstName
                lbllname.Text = AptifyEbusinessUser1.LastName
                TxtLine1.Text = dt.Rows(0)("PersonLine1")
                TxtLine2.Text = dt.Rows(0)("PersonLine2")
                TxtLine3.Text = dt.Rows(0)("PersonLine3")
                SetComboValue(cmbCountry, IIf(dt.Rows(0)("PersonCountry") = "", "Ireland", dt.Rows(0)("PersonCountry")).ToString)
                PopulateState(cmbState, cmbCountry)
                SetComboValue(cmbState, dt.Rows(0)("PersonState"))
                txtZipCode.Text = dt.Rows(0)("PersonPostalCode")
                TxtEmail.Text = Convert.ToString(dt.Rows(0)("PrimayPersonEamil")).Trim
                txtCity.Text = dt.Rows(0).Item("PersonCity")
                If Not dt.Rows(0)("PersonDateOfBirth") Is Nothing AndAlso Convert.ToString(dt.Rows(0)("PersonDateOfBirth")) <> "" AndAlso IsDBNull(Convert.ToString(dt.Rows(0).Item("PersonDateOfBirth"))) = False AndAlso Convert.ToString(dt.Rows(0).Item("PersonDateOfBirth")) <> "01/01/1777" Then
                    rdpBirthdate.SelectedDate = Convert.ToDateTime(dt.Rows(0)("PersonDateOfBirth")).ToShortDateString
                End If
                If Not dt.Rows(0)("PersonTelephoneNo") Is Nothing AndAlso Convert.ToString(dt.Rows(0)("PersonTelephoneNo")) <> "" Then
                    TxtLandlineNo.Text = dt.Rows(0)("PersonTelephoneNo")
                End If
                If Not dt.Rows(0)("PersonMobileNumber") Is Nothing AndAlso Convert.ToString(dt.Rows(0)("PersonMobileNumber")) <> "" Then
                    Txtpmobileno.Text = dt.Rows(0)("PersonMobileNumber")
                End If
                If Not dt.Rows(0).Item("BusinessCategory") Is Nothing AndAlso Convert.ToString(dt.Rows(0).Item("BusinessCategory")) <> "" Then
                    cmbFirmBusinessCategory.SelectedValue = dt.Rows(0).Item("BusinessCategory")
                End If
                '
                If Convert.ToBoolean(dt.Rows(0).Item("InBusiness")) Then
                    chkIsBusiness.Checked = True
                Else
                    chkIsBusiness.Checked = False
                End If
                If Convert.ToBoolean(dt.Rows(0).Item("PractisingOffice")) Then
                    chkIsPracticeOffice.Checked = True
                Else
                    chkIsPracticeOffice.Checked = False
                End If
                'Training Certificate Details

                txtCertifiedBy.Text = Convert.ToString(dt.Rows(0).Item("NameForCertificate"))
                If Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(0).Item("CertificationStartDate"))) AndAlso IsDBNull(Convert.ToString(dt.Rows(0).Item("CertificationStartDate"))) = False AndAlso Convert.ToString(dt.Rows(0).Item("CertificationStartDate")) <> "01/01/1777" Then
                    rdStartdate.SelectedDate = Convert.ToString(dt.Rows(0).Item("CertificationStartDate"))
                End If
                If Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(0).Item("CertificationEndDate"))) AndAlso IsDBNull(Convert.ToString(dt.Rows(0).Item("CertificationEndDate"))) = False AndAlso Convert.ToString(dt.Rows(0).Item("CertificationEndDate")) <> "01/01/1777" Then
                    rdEndDate.SelectedDate = Convert.ToString(dt.Rows(0).Item("CertificationEndDate"))
                End If
                txtPeriod.Text = Convert.ToString(dt.Rows(0).Item("CertificationDuration"))

                'If Not String.IsNullOrEmpty(dt.Rows(0).Item("TrainingExpiryDate")) Then
                If Not dt.Rows(0)("TrainingExpiryDate") Is Nothing AndAlso Convert.ToString(dt.Rows(0)("TrainingExpiryDate")) <> "" AndAlso IsDBNull(Convert.ToString(dt.Rows(0).Item("TrainingExpiryDate"))) = False AndAlso Convert.ToString(dt.Rows(0).Item("TrainingExpiryDate")) <> "01/01/1777" Then
                    RdpExpirydate.SelectedDate = Convert.ToString(dt.Rows(0).Item("TrainingExpiryDate"))
                End If

                txtCompany.Text = Convert.ToString(dt.Rows(0).Item("TrainingFirmID_Name"))
                If txtCompany.Text.Trim = "" Then
                    txtCompany.Text = Convert.ToString(dt.Rows(0).Item("CurrentEmploymentName"))
                End If
                txtTrainingFirmLine1.Text = Convert.ToString(dt.Rows(0).Item("TrainingFirmLine1"))
                txtTrainingFirmLine2.Text = Convert.ToString(dt.Rows(0).Item("TrainingFirmLine2"))
                txtTrainingFirmLine3.Text = Convert.ToString(dt.Rows(0).Item("TrainingFirmLine3"))
                SetComboValue(cmbTrainingFirmCountry, IIf(Convert.ToString(dt.Rows(0)("TrainingFirmCountry")) = "", "Ireland", Convert.ToString(dt.Rows(0)("TrainingFirmCountry")).ToString))
                PopulateState(cmbTrainingFirmState, cmbTrainingFirmCountry)
                SetComboValue(cmbTrainingFirmState, Convert.ToString(dt.Rows(0)("TrainingFirmState")))
                txtTrainingFirmZipCode.Text = Convert.ToString(dt.Rows(0)("TrainingFirmZipCode"))
                txtTrainingFirmCity.Text = Convert.ToString(dt.Rows(0)("TrainingFirmCity"))
                txtTrainingFirmTelephone.Text = Convert.ToString(dt.Rows(0)("TrainingFirmPhone"))


                txtFirmName.Text = Convert.ToString(dt.Rows(0).Item("CurrentEmployerID_Name"))
                txtFirmLine1.Text = Convert.ToString(dt.Rows(0).Item("CurrentEmploymentLine1"))
                txtFirmLine2.Text = Convert.ToString(dt.Rows(0).Item("CurrentEmploymentLine2"))
                txtFirmLine3.Text = Convert.ToString(dt.Rows(0).Item("CurrentEmploymentLine3"))
                SetComboValue(cmbFirmCountry, IIf(Convert.ToString(dt.Rows(0)("CurrentEmploymentCountry")) = "", "Ireland", Convert.ToString(dt.Rows(0)("CurrentEmploymentCountry")).ToString))
                PopulateState(cmbFirmState, cmbFirmCountry)
                SetComboValue(cmbFirmState, Convert.ToString(dt.Rows(0)("CurrentEmploymentState")))
                txtFirmZipCode.Text = Convert.ToString(dt.Rows(0)("CurrentEmploymentZipCode"))
                txtFirmCity.Text = Convert.ToString(dt.Rows(0)("CurrentEmploymentCity"))
                txtTrainingFirmTelephone.Text = Convert.ToString(dt.Rows(0)("TrainingFirmPhone"))
                txtFirmTelephoneNo.Text = Convert.ToString(dt.Rows(0)("CurrentEmployerPhone"))
                rdpformentrydate.Text = Date.Today

                txtFirmFaxNo.Text = Convert.ToString(dt.Rows(0)("CurrentEmploymentMainFaxID"))
                txtFirmJobTitle.Text = Convert.ToString(dt.Rows(0)("FirmJobTitle"))
                If Convert.ToString(dt.Rows(0).Item("FirmJobStartDate")) <> "" AndAlso IsDBNull(Convert.ToString(dt.Rows(0).Item("FirmJobStartDate"))) = False AndAlso Convert.ToString(dt.Rows(0).Item("FirmJobStartDate")) <> "01/01/1777 00:00:00" Then
                    rdpJobStartDate.SelectedDate = Convert.ToString(dt.Rows(0)("FirmJobStartDate"))
                End If
                If Convert.ToBoolean(dt.Rows(0)("ChangeOfName")) Then
                    chkChangeName.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("SignatureOfStudent")) Then
                    chkSignatureOfStudent.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("SignatureOfCharteredAccountsIrelandMember")) Then
                    chkSignatureOfIrelandMember.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("FinalReviewCA")) Then
                    chkFinalReview.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("StudentSignatureForOfficerGroup")) Then
                    chkStudentSignatureForOfficerGroup.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("IsEmployed")) Then
                    chkCurrentEmployer.Checked = True
                End If
                GetMembershipStatusReson(dt.Rows(0)("ID"))
                If Convert.ToInt32(dt.Rows(0)("MembershipApplicationStatusID")) >= 0 AndAlso Convert.ToInt32(dt.Rows(0)("MembershipApplicationStatusID")) <> AptifyApplication.GetEntityRecordIDFromRecordName("MembershipApplicationStatus__c", "In Progress") Then
                    EnabledAllFileds()
                End If
                If Convert.ToBoolean(dt.Rows(0)("ExpSummaryFormEnclosed")) Then
                    chkExpSummaryFormEnclosed.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("FinalTrainingReviewEnclosed")) Then
                    chkFinalTrainingReviewEnclosed.Checked = True
                End If
                If Not dt.Rows(0)("FAEPassDate") Is Nothing AndAlso Convert.ToString(dt.Rows(0)("FAEPassDate")) <> "" Then
                    txtFAEDate.SelectedDate = Convert.ToString(dt.Rows(0)("FAEPassDate"))
                End If
                If Convert.ToBoolean(dt.Rows(0)("ElevationProgramme")) Then
                    chkElevationProgramme.Checked = True
                End If
                'Code commented by Govind M for performance
                If Convert.ToString(dt.Rows(0)("WebName")) <> "" Then
                    lblApplicationType.Text = Convert.ToString(dt.Rows(0)("WebName"))
                    'redmine 17199 by Shweta K
                    If Convert.ToString(dt.Rows(0)("WebName")).ToLower() = "submitted to cai" Then
                        btnPrint.Visible = True
                    End If
                End If
                'If Convert.ToInt32(dt.Rows(0)("MembershipApplicationStatusID")) > 0 Then
                '    Dim sApplicationStatus As String = "..spGetMembershipAppStatus__c @Id=" & CInt(dt.Rows(0)("MembershipApplicationStatusID"))
                '    Dim sApplicationWebName As String = Convert.ToString(DataAction.ExecuteScalar(sApplicationStatus))
                '    If sApplicationWebName <> "" Then
                '        lblApplicationType.Text = sApplicationWebName
                '    End If

                'End If
                If Not dt.Rows(0)("ElevationProgramDate") Is Nothing AndAlso Convert.ToString(dt.Rows(0)("ElevationProgramDate")) <> "" Then
                    txtElevationDate.SelectedDate = Convert.ToString(dt.Rows(0)("ElevationProgramDate"))
                End If
                If Convert.ToInt32(dt.Rows(0)("OrderID")) > 0 Then
                    Dim sSqlOrderBal As String = Database & "..spGetOrderBalance__c @OrderID=" & Convert.ToInt32(dt.Rows(0)("OrderID"))
                    Dim lOrderBal As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sSqlOrderBal))
                    If lOrderBal <= 0 Then
                        btnSubmitAndPay.Visible = False
                    Else
                        btnSubmitAndPay.Visible = True
                    End If



                    ViewState("Orderid") = Convert.ToInt32(dt.Rows(0)("OrderID"))
                End If

                If Not dt.Rows(0)("DateOfAdmission") Is Nothing AndAlso Convert.ToString(dt.Rows(0)("DateOfAdmission")) <> "" Then
                    txtDateOfAdmission.SelectedDate = Convert.ToString(dt.Rows(0)("DateOfAdmission"))
                End If
                txtMembershipNumber.Text = Convert.ToString(dt.Rows(0)("MembershipNumber"))
                If Convert.ToBoolean(dt.Rows(0)("FutureCorrespondenceHome")) Then
                    chkHome.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("FutureCorrespondenceOffice")) Then
                    chkOffice.Checked = True
                End If
                txtRemittanceAmount.Text = Format(dt.Rows(0)("RemittanceAmount"), "0.00")
                If Not dt.Rows(0)("RemittanceDate") Is Nothing AndAlso Convert.ToString(dt.Rows(0)("RemittanceDate")) <> "" Then
                    txtRemitatanceDate.SelectedDate = Convert.ToString(dt.Rows(0)("RemittanceDate"))
                End If

                If Convert.ToBoolean(dt.Rows(0)("IsApplicantSignatureForCouncil")) Then
                    chkApplicantSignatureForCouncil.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("IsFutureCorrespondence")) Then
                    chkFutureCorrespondence.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("IsCertificateName")) Then
                    chkCertificateName.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("IsRemittance")) Then
                    chkRemittance.Checked = True
                End If
                If Convert.ToBoolean(dt.Rows(0)("IsCurrentMembership")) Then
                    chkCurrentMembership.Checked = True
                End If
                ''Added BY Pradip 2016-06-30 For https://redmine.softwaredesign.ie/issues/14557
                hidCompanyID.Value = Convert.ToString(dt.Rows(0)("CurrentEmployerID"))
            Else
                GetPersonDetails()
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetWebApplicationTypeName(ByVal ApplicationID As Integer)
        Try
            Dim sSql As String = Database & "..spGetWebApplicationTypeName__c @AppID=" & ApplicationID
            Dim sWebName As String = Convert.ToString(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If sWebName <> "" Then
                lblApplicationWebName.Text = sWebName
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetMembershipStatusReson(ByVal MembershipAppID As Integer)
        Try
            Dim sSql As String = Database & "..spGetMembershipStatusReson__c @MembershipAppID=" & MembershipAppID
            Dim dtMembershipStatusReson As DataTable = DataAction.GetDataTable(sSql)
            If Not dtMembershipStatusReson Is Nothing AndAlso dtMembershipStatusReson.Rows.Count > 0 Then
                divStatusPnl.Visible = True
                grdMembershipStatusReson.DataSource = dtMembershipStatusReson
                grdMembershipStatusReson.DataBind()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' if Status is other than In-Progress then not modified ay fields 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EnabledAllFileds()
        Try
            TxtLine1.Enabled = False
            TxtLine1.BorderWidth = 0
            TxtLine2.Enabled = False
            TxtLine2.BorderWidth = 0
            TxtLine3.Enabled = False
            TxtLine3.BorderWidth = 0
            cmbCountry.Enabled = False
            cmbCountry.BorderWidth = 0
            txtZipCode.Enabled = False
            txtZipCode.BorderWidth = 0
            txtCity.Enabled = False
            txtCity.BorderWidth = 0
            TxtLandlineNo.Enabled = False
            TxtLandlineNo.BorderWidth = 0
            Txtpmobileno.Enabled = False
            Txtpmobileno.BorderWidth = 0
            TxtEmail.Enabled = False
            TxtEmail.BorderWidth = 0
            rdpBirthdate.Enabled = False
            rdpBirthdate.BorderWidth = 0
            TextBox12.Enabled = False
            TextBox12.BorderWidth = 0
            rdpformentrydate.Enabled = False
            rdpformentrydate.BorderWidth = 0
            RdpExpirydate.Enabled = False
            RdpExpirydate.BorderWidth = 0
            txtCompany.Enabled = False
            txtCompany.BorderWidth = 0
            txtTrainingFirmLine1.Enabled = False
            txtTrainingFirmLine1.BorderWidth = 0
            txtTrainingFirmLine2.Enabled = False
            txtTrainingFirmLine2.BorderWidth = 0
            txtTrainingFirmLine3.Enabled = False
            txtTrainingFirmLine3.BorderWidth = 0
            cmbTrainingFirmCountry.Enabled = False
            cmbTrainingFirmCountry.BorderWidth = 0
            txtTrainingFirmZipCode.Enabled = False
            txtTrainingFirmZipCode.BorderWidth = 0
            txtTrainingFirmCity.Enabled = False
            txtTrainingFirmCity.BorderWidth = 0
            txtTrainingFirmTelephone.Enabled = False
            txtTrainingFirmTelephone.BorderWidth = 0
            dlITProgramme.Enabled = False
            dlCompanyLawModule.Enabled = False
            txtCertifiedBy.Enabled = False
            txtCertifiedBy.BorderWidth = 0
            TextBox20.Enabled = False
            TextBox20.BorderWidth = 0
            TextBox1.Enabled = False
            TextBox1.BorderWidth = 0
            rdStartdate.Enabled = False
            rdStartdate.BorderWidth = 0
            rdEndDate.Enabled = False
            rdEndDate.BorderWidth = 0
            txtPeriod.Enabled = False
            txtPeriod.BorderWidth = 0
            TextBox27.Enabled = False
            TextBox27.BorderWidth = 0
            RadDatePicker3.Enabled = False
            RadDatePicker3.BorderWidth = 0
            TextBox29.Enabled = False
            TextBox29.BorderWidth = 0
            RadDatePicker4.Enabled = False
            RadDatePicker4.BorderWidth = 0
            txtFirmName.Enabled = False
            txtFirmName.BorderWidth = 0
            txtFirmLine1.Enabled = False
            txtFirmLine1.BorderWidth = 0
            txtFirmLine2.Enabled = False
            txtFirmLine2.BorderWidth = 0
            txtFirmLine3.Enabled = False
            txtFirmLine3.BorderWidth = 0
            cmbFirmCountry.Enabled = False
            cmbFirmCountry.BorderWidth = 0
            txtFirmZipCode.Enabled = False
            txtFirmZipCode.BorderWidth = 0
            txtFirmCity.Enabled = False
            txtFirmCity.BorderWidth = 0
            cmbFirmState.Enabled = False
            cmbFirmState.BorderWidth = 0
            txtFirmTelephoneNo.Enabled = False
            txtFirmTelephoneNo.BorderWidth = 0
            txtFirmFaxNo.Enabled = False
            txtFirmFaxNo.BorderWidth = 0
            txtFirmJobTitle.Enabled = False
            txtFirmJobTitle.BorderWidth = 0
            rdpJobStartDate.Enabled = False
            rdpJobStartDate.BorderWidth = 0
            chkIsBusiness.Enabled = False
            chkIsPracticeOffice.Enabled = False
            cmbFirmBusinessCategory.Enabled = False
            chkStudentSignatureForOfficerGroup.Enabled = False
            chkFinalReview.Enabled = False
            chkChangeName.Enabled = False
            chkCurrentEmployer.Enabled = False
            chkSignatureOfIrelandMember.Enabled = False
            chkSignatureOfStudent.Enabled = False
            cmbTrainingFirmState.Enabled = False
            cmbTrainingFirmState.BorderWidth = 0
            chkFinalTrainingReviewEnclosed.Enabled = False
            txtFAEDate.Enabled = False
            cmbState.Enabled = False
            cmbState.BorderWidth = 0
            dlREQ.Enabled = False
            cmdSave.Visible = False
            btnSubmit.Visible = False
            TextBox2.Enabled = False
            TextBox2.BorderWidth = 0
            txtElevationDate.Enabled = False
            txtElevationDate.BorderWidth = 0
            chkElevationProgramme.Enabled = False
            chkApplicantSignatureForCouncil.Enabled = False
            chkFutureCorrespondence.Enabled = False
            chkCertificateName.Enabled = False
            chkRemittance.Enabled = False
            chkCurrentMembership.Enabled = False
            txtRemitatanceDate.Enabled = False
            txtRemitatanceDate.BorderWidth = 0
            TextBox4.Enabled = False
            TextBox4.BorderWidth = 0
            txtRemittanceAmount.Enabled = False
            txtRemittanceAmount.BorderWidth = 0
            chkHome.Enabled = False
            chkOffice.Enabled = False
            txtMembershipNumber.Enabled = False
            txtMembershipNumber.BorderWidth = 0
            txtDateOfAdmission.Enabled = False
            txtDateOfAdmission.BorderWidth = 0
            chkExpSummaryFormEnclosed.Enabled = False
            chkFinalTrainingReviewEnclosed.Enabled = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Added By Pradip 20106-03-15 For G1-77 Tracker Item
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub LoadEducationContractCompanyAddress()
        Try
            Dim sSQL As String = Database & "..spGetContractCompanyStreetAddress__c"
            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, AptifyEbusinessUser1.PersonID)
            Dim dtAddress = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
            If dtAddress IsNot Nothing AndAlso dtAddress.Rows.Count > 0 Then
                txtCompany.Text = Convert.ToString(dtAddress.Rows(0)("Name"))
                ''Added By Pradip 2016-06-30 For https://redmine.softwaredesign.ie/issues/14557
                hidTrainingFirmID.Value = Convert.ToString(dtAddress.Rows(0)("CompanyID"))
                txtTrainingFirmLine1.Text = Convert.ToString(dtAddress.Rows(0)("Line1"))
                txtTrainingFirmLine2.Text = Convert.ToString(dtAddress.Rows(0)("Line2"))
                txtTrainingFirmLine3.Text = Convert.ToString(dtAddress.Rows(0)("Line3"))
                txtTrainingFirmZipCode.Text = dtAddress.Rows(0)("PostalCode")
                txtTrainingFirmTelephone.Text = Convert.ToString(dtAddress.Rows(0)("Telephone"))
                txtTrainingFirmCity.Text = dtAddress.Rows(0).Item("City")
                If Not dtAddress.Rows(0)("ContractExpireDate") Is Nothing AndAlso Convert.ToString(dtAddress.Rows(0)("ContractExpireDate")) <> "" AndAlso IsDBNull(Convert.ToString(dtAddress.Rows(0).Item("ContractExpireDate"))) = False AndAlso Convert.ToString(dtAddress.Rows(0).Item("ContractExpireDate")) <> "01/01/1777" Then
                    RdpExpirydate.SelectedDate = Convert.ToString(dtAddress.Rows(0).Item("ContractExpireDate"))
                End If
                If Convert.ToString(dtAddress.Rows(0)("CountryID")).Trim <> "" Then
                    cmbTrainingFirmCountry.SelectedValue = Convert.ToString(dtAddress.Rows(0)("CountryID"))
                    PopulateState(cmbTrainingFirmState, cmbTrainingFirmCountry)
                    SetComboValue(cmbTrainingFirmState, dtAddress.Rows(0)("StateProvince"))
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Added By Pradip For To Check Person Pending Changes Exits or Not.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckPendingChanges()
        Try
            Dim ssSQL As String = Database & "..spCheckPersonPendingChanges__c"
            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, AptifyEbusinessUser1.PersonID)
            Dim iCheck As Integer = DataAction.ExecuteScalarParametrized(ssSQL, CommandType.StoredProcedure, param)
            If iCheck > 0 Then
                lblDisabledBtn.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Consulting..Person.PendingChangesMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                btnSubmit.Enabled = False
                btnSubmitAndPay.Enabled = False
                cmdSave.Enabled = False
            Else
                lblDisabledBtn.Text = ""
                btnSubmit.Enabled = True
                btnSubmitAndPay.Enabled = True
                cmdSave.Enabled = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

#Region "Load And Set Topic code"
    Private Sub LoadTopicCodes(ByVal sParentName As String, _
                                       ByRef dtList As System.Web.UI.WebControls.DataList)
        Try
            Dim dtTopicCodes As DataTable = DataAction.GetDataTable("Exec Aptify..spGetTopLevelTopicCodes__c '" & sParentName & "'")
            If Not dtTopicCodes Is Nothing AndAlso dtTopicCodes.Rows.Count > 0 Then
                dtList.DataSource = dtTopicCodes
                dtList.DataBind()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub SetITProgrammeTopicCodes(ByVal sParentName As String)

        Try
            Dim dtTopicCodes As DataTable = DataAction.GetDataTable(Me.Database & "..spGetTopicCodeLinks__c '" & sParentName & "'," & Convert.ToInt32(ViewState("MembershipAppID")) & "," & Convert.ToInt32(AptifyApplication.GetEntityID("MembershipApplication__c")))
            For Each i As DataListItem In Me.dlITProgramme.Items
                Dim chkITProgramme As CheckBox = CType(i.FindControl("chkITProgramme"), CheckBox)
                Dim lblITProgrammeTopicCodeID As Label = CType(i.FindControl("lblITProgrammeTopicCodeID"), Label)
                Dim rows() As DataRow = dtTopicCodes.Select("ID=" & lblITProgrammeTopicCodeID.Text & " and checkedValue='True'")
                If rows.Length > 0 Then
                    chkITProgramme.Checked = True
                End If

            Next

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub SetCompanyLawModuleTopicCodes(ByVal sParentName As String)
        Try
            Dim dtTopicCodes As DataTable = DataAction.GetDataTable(Me.Database & "..spGetTopicCodeLinks__c '" & sParentName & "'," & Convert.ToInt32(ViewState("MembershipAppID")) & "," & Convert.ToInt32(AptifyApplication.GetEntityID("MembershipApplication__c")))
            For Each i As DataListItem In Me.dlCompanyLawModule.Items
                Dim chkCompanyLawModule As CheckBox = CType(i.FindControl("chkCompanyLawModule"), CheckBox)
                Dim lblCompanyLawModuleTopicCodeID As Label = CType(i.FindControl("lblCompanyLawModuleTopicCodeID"), Label)
                Dim rows() As DataRow = dtTopicCodes.Select("ID=" & lblCompanyLawModuleTopicCodeID.Text & " and checkedValue='True'")
                If rows.Length > 0 Then
                    chkCompanyLawModule.Checked = True
                End If
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub SetRecognisedExperienceForQualificationTopicCodes(ByVal sParentName As String)
        Try
            Dim dtTopicCodes As DataTable = DataAction.GetDataTable(Me.Database & "..spGetTopicCodeLinks__c '" & sParentName & "'," & Convert.ToInt32(ViewState("MembershipAppID")) & "," & Convert.ToInt32(AptifyApplication.GetEntityID("MembershipApplication__c")))
            For Each i As DataListItem In Me.dlREQ.Items
                Dim chkREQ As CheckBox = CType(i.FindControl("chkREQ"), CheckBox)
                Dim lblREQTopicCodeID As Label = CType(i.FindControl("lblREQTopicCodeID"), Label)
                Dim rows() As DataRow = dtTopicCodes.Select("ID=" & lblREQTopicCodeID.Text & " and checkedValue='True'")
                If rows.Length > 0 Then
                    chkREQ.Checked = True
                End If
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Function AddTopicCodeLink(ByVal TopicCodeID As Long, ByVal TopicCodeLinkRecordId As Long) As Boolean
        Dim oLink As AptifyGenericEntityBase
        Dim sError As String = Nothing
        Try
            oLink = AptifyApplication.GetEntityObject("Topic Code Links", TopicCodeLinkRecordId)
            oLink.SetValue("TopicCodeID", TopicCodeID)
            oLink.SetValue("RecordID", Convert.ToInt32(ViewState("IsMembApp")))
            oLink.SetValue("EntityID", AptifyApplication.GetEntityID("MembershipApplication__c"))
            oLink.SetValue("Status", "Active")
            oLink.SetValue("Value", "Yes")
            oLink.SetValue("DateAdded", Date.Today)
            If oLink.Save(sError) Then
                Return True
            Else
                Return False
            End If
            'Return oLink.Save(False)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function
    Private Function AddUnCheckTopicCodeLink(ByVal TopicCodeID As Long, ByVal TopicCodeLinkRecordId As Long) As Boolean
        Dim oLink As AptifyGenericEntityBase
        Dim sError As String = Nothing
        Try
            oLink = AptifyApplication.GetEntityObject("Topic Code Links", TopicCodeLinkRecordId)
            oLink.SetValue("TopicCodeID", TopicCodeID)
            oLink.SetValue("RecordID", Convert.ToInt32(ViewState("IsMembApp")))
            oLink.SetValue("EntityID", AptifyApplication.GetEntityID("MembershipApplication__c"))
            oLink.SetValue("Status", "Inactive")
            oLink.SetValue("Value", "No")
            oLink.SetValue("DateAdded", Date.Today)
            If oLink.Save(sError) Then
                Return True
            Else
                Return False
            End If
            'Return oLink.Save(False)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function
#End Region

#Region "Button Click"
    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click
        Try
            If Page.IsValid Then
                ' radMockTrial.VisibleOnPageLoad = True
                If Session("SaveButtonClick") = "" Then
                    Session("SaveButtonClick") = "Yes"
                    DoSave()
                End If
            End If
            ' lblMsg.Text=
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub DoSave()
        Dim oGE As AptifyGenericEntityBase
        Dim bRedirect As Boolean = False
        Try
            If AptifyEbusinessUser1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            Else
                If Convert.ToInt32(ViewState("MembershipAppID")) > 0 Then
                    oGE = AptifyApplication.GetEntityObject("MembershipApplication__c", Convert.ToInt32(ViewState("MembershipAppID")))
                Else
                    oGE = AptifyApplication.GetEntityObject("MembershipApplication__c", -1)
                End If

                ' oGE.SetValue("CompanyName", txtCompany.Text)
                oGE.SetValue("PersonID", CLng(AptifyEbusinessUser1.PersonID))
                Dim sCounty As String = ""
                Me.DoPostalCodeLookup(txtZipCode.Text, CStr(IIf(cmbCountry.SelectedIndex >= 0, cmbCountry.SelectedValue, "")), sCounty, txtCity, cmbState)
                oGE.SetValue("PersonLine1", TxtLine1.Text.Trim)
                oGE.SetValue("PersonLine2", TxtLine2.Text.Trim)
                oGE.SetValue("PersonLine3", TxtLine3.Text.Trim)
                oGE.SetValue("PersonCity", txtCity.Text)
                oGE.SetValue("PersonState", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedValue, "")))
                oGE.SetValue("PersonPostalCode", txtZipCode.Text)
                oGE.SetValue("PersonCountryCodeID", CLng(IIf(cmbCountry.SelectedIndex >= 0, cmbCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                oGE.SetValue("PersonCountry", CStr(IIf(cmbCountry.SelectedIndex >= 0, cmbCountry.SelectedValue, "")))
                oGE.SetValue("PersonDateOfBirth", Convert.ToDateTime(rdpBirthdate.SelectedDate).ToShortDateString)
                oGE.SetValue("PersonMobileNumber", Txtpmobileno.Text.Trim)
                oGE.SetValue("PersonTelephoneNo", TxtLandlineNo.Text.Trim)
                oGE.SetValue("ApplicationSubmissionDate", rdpformentrydate.Text)
                oGE.SetValue("trainingExpirydate", RdpExpirydate.SelectedDate)
                oGE.SetValue("NameForCertificate", txtCertifiedBy.Text)
                oGE.SetValue("CertificationStartDate", rdStartdate.SelectedDate)
                oGE.SetValue("CertificationEndDate", rdEndDate.SelectedDate)
                oGE.SetValue("CertificationDuration", txtPeriod.Text)



                ' Save Trining Address on Company
                Dim sCompanyName() As String = txtCompany.Text.Trim.Split("\")
                '  If CInt(AptifyApplication.GetEntityRecordIDFromRecordName("Companies", sCompanyName(0))) <> -1 Then
                If CInt(hidTrainingFirmID.Value) > 0 Then
                    'oGE.SetValue("TrainingFirmID", CInt(AptifyApplication.GetEntityRecordIDFromRecordName("Companies", sCompanyName(0))))
                    oGE.SetValue("TrainingFirmID", CInt(hidTrainingFirmID.Value))
                    oGE.SetValue("TrainingFirmLine1", txtTrainingFirmLine1.Text.Trim)
                    oGE.SetValue("TrainingFirmLine2", txtTrainingFirmLine2.Text.Trim)
                    oGE.SetValue("TrainingFirmLine3", txtTrainingFirmLine3.Text.Trim)
                    oGE.SetValue("TrainingFirmCity", txtTrainingFirmCity.Text.Trim)
                    oGE.SetValue("TrainingFirmState", CStr(IIf(cmbTrainingFirmState.SelectedIndex >= 0, cmbTrainingFirmState.SelectedValue, "")))
                    oGE.SetValue("TrainingFirmZipCode", txtTrainingFirmZipCode.Text.Trim)
                    oGE.SetValue("TrainingFirmCountryCodeID", CLng(IIf(cmbTrainingFirmCountry.SelectedIndex >= 0, cmbTrainingFirmCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                    oGE.SetValue("TrainingFirmCountry", CStr(IIf(cmbTrainingFirmCountry.SelectedIndex >= 0, cmbTrainingFirmCountry.SelectedValue, "")))
                    oGE.SetValue("TrainingFirmPhone", txtTrainingFirmTelephone.Text.Trim)
                Else
                    oGE.SetValue("TrainingFirmID", -1)
                    oGE.SetValue("CurrentEmploymentName", txtCompany.Text.Trim)
                    oGE.SetValue("TrainingFirmLine1", txtTrainingFirmLine1.Text.Trim)
                    oGE.SetValue("TrainingFirmLine2", txtTrainingFirmLine2.Text.Trim)
                    oGE.SetValue("TrainingFirmLine3", txtTrainingFirmLine3.Text.Trim)
                    oGE.SetValue("TrainingFirmCity", txtTrainingFirmCity.Text.Trim)
                    oGE.SetValue("TrainingFirmState", CStr(IIf(cmbTrainingFirmState.SelectedIndex >= 0, cmbTrainingFirmState.SelectedValue, "")))
                    oGE.SetValue("TrainingFirmZipCode", txtTrainingFirmZipCode.Text.Trim)
                    oGE.SetValue("TrainingFirmCountryCodeID", CLng(IIf(cmbTrainingFirmCountry.SelectedIndex >= 0, cmbTrainingFirmCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                    oGE.SetValue("TrainingFirmCountry", CStr(IIf(cmbTrainingFirmCountry.SelectedIndex >= 0, cmbTrainingFirmCountry.SelectedValue, "")))
                    oGE.SetValue("TrainingFirmPhone", txtTrainingFirmTelephone.Text.Trim)

                End If
                If cmbTrainingFirmCountry.SelectedValue <= 0 Then
                    SetComboValue(cmbTrainingFirmCountry, "Ireland")
                    oGE.SetValue("TrainingFirmCountryCodeID", CLng(IIf(cmbTrainingFirmCountry.SelectedIndex >= 0, cmbTrainingFirmCountry.SelectedItem.Value, "")))
                End If
                If txtTrainingFirmTelephone.Text.Trim = "" Then
                    oGE.SetValue("TrainingFirmPhone", "1")
                End If

                'If CInt(AptifyApplication.GetEntityRecordIDFromRecordName("Companies", txtFirmName.Text)) <> -1 Then
                If Convert.ToInt32(hidCompanyID.Value) > 0 Then
                    'oGE.SetValue("CurrentEmployerID", CInt(AptifyApplication.GetEntityRecordIDFromRecordName("Companies", txtFirmName.Text)))
                    oGE.SetValue("CurrentEmployerID", Convert.ToInt32(hidCompanyID.Value))
                    ''Commented By Pradip 2016-07-01 For Issue https://redmine.softwaredesign.ie/issues/14557
                    ' ''oGE.SetValue("CurrentEmploymentLine1", txtFirmLine1.Text.Trim)
                    ' ''oGE.SetValue("CurrentEmploymentLine2", txtFirmLine2.Text.Trim)
                    ' ''oGE.SetValue("CurrentEmploymentLine3", txtFirmLine3.Text.Trim)
                    ' ''oGE.SetValue("CurrentEmploymentCity", txtFirmCity.Text.Trim)
                    ' ''oGE.SetValue("CurrentEmploymentState", CStr(IIf(cmbFirmState.SelectedIndex >= 0, cmbFirmState.SelectedValue, "")))
                    ' ''oGE.SetValue("CurrentEmploymentZipCode", txtFirmZipCode.Text.Trim)
                    ' ''oGE.SetValue("CurrentEmployerCountryCode", CLng(IIf(cmbFirmCountry.SelectedIndex >= 0, cmbFirmCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                    ' ''oGE.SetValue("CurrentEmploymentCountry", CStr(IIf(cmbFirmCountry.SelectedIndex >= 0, cmbFirmCountry.SelectedValue, "")))
                    '''' oGE.SetValue("CurrentEmployerPhone", txtFirmTelephoneNo.Text.Trim)
                    ''oGE.SetValue("CurrentEmploymentMainFaxID", txtFirmFaxNo.Text)
                    If Convert.ToString(cmbFirmBusinessCategory.SelectedValue) <> "Please Select Business Category" Then
                        oGE.SetValue("BusinessCategory", cmbFirmBusinessCategory.SelectedValue)
                    End If
                    If chkIsBusiness.Checked Then
                        oGE.SetValue("InBusiness", 1)
                    Else
                        oGE.SetValue("InBusiness", 0)
                    End If
                    If chkIsPracticeOffice.Checked Then
                        oGE.SetValue("PractisingOffice", 1)
                    Else
                        oGE.SetValue("PractisingOffice", 0)
                    End If
                    oGE.SetValue("FirmJobTitle", txtFirmJobTitle.Text.Trim)
                    oGE.SetValue("FirmJobStartDate", rdpJobStartDate.SelectedDate)

                End If

                ' '' ''If cmbTrainingFirmCountry.SelectedValue <= 0 Then
                ' '' ''    SetComboValue(cmbFirmCountry, "United States")
                ' '' ''    oGE.SetValue("CurrentEmployerCountryCode", CLng(IIf(cmbFirmCountry.SelectedIndex >= 0, cmbFirmCountry.SelectedItem.Value, "")))
                ' '' ''End If
                'If txtTrainingFirmTelephone.Text.Trim = "" Then
                '    oGE.SetValue("CurrentEmployerPhone", "1")
                'End If
                If chkCurrentEmployer.Checked Then
                    oGE.SetValue("IsEmployed", 1)
                Else
                    oGE.SetValue("IsEmployed", 0)
                End If
                If chkStudentSignatureForOfficerGroup.Checked Then
                    oGE.SetValue("StudentSignatureForOfficerGroup", 1)
                Else
                    oGE.SetValue("StudentSignatureForOfficerGroup", 0)
                End If
                If chkFinalReview.Checked Then
                    oGE.SetValue("FinalReviewCA", 1)
                Else
                    oGE.SetValue("FinalReviewCA", 0)
                End If
                If chkChangeName.Checked Then
                    oGE.SetValue("ChangeOfName", 1)
                Else
                    oGE.SetValue("ChangeOfName", 0)
                End If
                If chkSignatureOfIrelandMember.Checked Then
                    oGE.SetValue("SignatureOfCharteredAccountsIrelandMember", 1)
                Else
                    oGE.SetValue("SignatureOfCharteredAccountsIrelandMember", 0)
                End If
                If chkSignatureOfStudent.Checked Then
                    oGE.SetValue("SignatureOfStudent", 1)
                Else
                    oGE.SetValue("SignatureOfStudent", 0)
                End If
                If Convert.ToInt32(oGE.GetValue("MembershipApplicationStatusID")) <= 0 OrElse Convert.ToInt32(oGE.GetValue("MembershipApplicationStatusID")) = AptifyApplication.GetEntityRecordIDFromRecordName("MembershipApplicationStatus__c", "In Progress") Then
                    oGE.SetValue("MembershipApplicationStatusID", Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("MembershipApplicationStatus__c", "In Progress")))
                End If
                If chkFinalTrainingReviewEnclosed.Checked Then
                    oGE.SetValue("FinalTrainingReviewEnclosed", 1)
                Else
                    oGE.SetValue("FinalTrainingReviewEnclosed", 0)
                End If
                If chkExpSummaryFormEnclosed.Checked Then
                    oGE.SetValue("ExpSummaryFormEnclosed", 1)
                Else
                    oGE.SetValue("ExpSummaryFormEnclosed", 0)
                End If
                If chkElevationProgramme.Checked = True Then
                    oGE.SetValue("ElevationProgramme", 1)
                Else                    oGE.SetValue("ElevationProgramme", 0)
                End If
                oGE.SetValue("FAEPassDate", txtFAEDate.SelectedDate)
                oGE.SetValue("ElevationProgramDate", txtElevationDate.SelectedDate)
                oGE.SetValue("DateOfAdmission", txtDateOfAdmission.SelectedDate)
                oGE.SetValue("MembershipNumber", txtMembershipNumber.Text)
                If chkHome.Checked = True Then
                    oGE.SetValue("FutureCorrespondenceHome", 1)
                Else
                    oGE.SetValue("FutureCorrespondenceHome", 0)
                End If
                If chkOffice.Checked = True Then
                    oGE.SetValue("FutureCorrespondenceOffice", 1)
                Else
                    oGE.SetValue("FutureCorrespondenceOffice", 0)
                End If
                oGE.SetValue("RemittanceAmount", txtRemittanceAmount.Text)
                oGE.SetValue("RemittanceDate", txtRemitatanceDate.SelectedDate)

                If chkApplicantSignatureForCouncil.Checked Then
                    oGE.SetValue("IsApplicantSignatureForCouncil", 1)
                Else
                    oGE.SetValue("IsApplicantSignatureForCouncil", 0)
                End If

                If chkFutureCorrespondence.Checked Then
                    oGE.SetValue("IsFutureCorrespondence", 1)
                Else
                    oGE.SetValue("IsFutureCorrespondence", 0)
                End If
                If chkCertificateName.Checked Then
                    oGE.SetValue("IsCertificateName", 1)
                Else
                    oGE.SetValue("IsCertificateName", 0)
                End If
                If chkRemittance.Checked Then
                    oGE.SetValue("IsRemittance", 1)
                Else
                    oGE.SetValue("IsRemittance", 0)
                End If
                If chkCurrentMembership.Checked Then
                    oGE.SetValue("IsCurrentMembership", 1)
                Else
                    oGE.SetValue("IsCurrentMembership", 0)
                End If
                oGE.SetValue("PersonCurrencyTypeID", Convert.ToInt32(ViewState("CurrencyTypeID")))
                oGE.SetValue("ApplicationType", Convert.ToInt32(ViewState("ApplicationID")))
                If oGE.Save(False) Then
                    bRedirect = True
                    ViewState("IsMembApp") = oGE.RecordID
                    ViewState("MembershipAppID") = oGE.RecordID
                    Dim sApplicationStatus As String = "..spGetMembershipAppStatus__c @Id=" & CInt(oGE.GetValue("MembershipApplicationStatusID"))
                    Dim sApplicationWebName As String = Convert.ToString(DataAction.ExecuteScalar(sApplicationStatus))
                    If sApplicationWebName <> "" Then
                        lblApplicationType.Text = sApplicationWebName
                    End If
                    SaveITProgrammeTopiccodes()
                    SaveCompanyLawModuleeTopiccodes()
                    SavRecognisedExperienceForQualificationTopiccodes()
                    lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MembershipApp.SucessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                Else
                    lblError.Visible = True
                    lblError.Text = oGE.LastError()
                End If
            End If
            ' End If
            'If rdpBirthdate.SelectedDate.Value.ToShortDateString Then

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub btnAbatementPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbatementPage.Click
        Try
            Session("EntityID") = Me.AptifyApplication.GetEntityID("MembershipApplication__c")
            Response.Redirect(AbatementFormPage & "?AppID=" & Convert.ToInt32(ViewState("MembershipAppID")))
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If AptifyEbusinessUser1.PersonID <= 0 Then
                Response.Redirect(LoginPage, False)
                Exit Sub
            End If
            If Page.IsValid Then
                radMockTrial.VisibleOnPageLoad = True
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            SaveStatusReson()
            Response.Redirect(Request.RawUrl)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub btnSubmitAndPay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitAndPay.Click
        Try
            If AptifyEbusinessUser1.PersonID <= 0 Then
                Response.Redirect(LoginPage, False)
                Exit Sub
            End If
            If Page.IsValid Then
                If lblApplicationType.Text <> "Submitted to Chartered Accountants Ireland" OrElse lblApplicationType.Text <> "Application with applicant" Then
                    If Session("SubmitPayButtonClick") = "" Then
                        Session("SubmitPayButtonClick") = "Yes"
                        DoSave()
                        SetOrderInMembershipApp()
                        If Convert.ToInt32(ViewState("OrderID")) > 0 Then
                            Response.Redirect(MakePaymentFormPage, False)
                        End If
                    End If

                Else
                    radSubmitPay.VisibleOnPageLoad = True

                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Check box Events"
    'Protected Sub ChkOffice_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkOffice.CheckedChanged
    '    If ChkOffice.Checked = True Then
    '        ChkOffice.Enabled = True
    '        ChkHome.Enabled = False
    '    Else
    '        ChkHome.Enabled = True
    '    End If
    'End Sub
    'Protected Sub ChkHome_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkHome.CheckedChanged
    '    If ChkHome.Checked = True Then
    '        ChkOffice.Enabled = False
    '        ChkHome.Enabled = True
    '    Else
    '        ChkOffice.Enabled = True
    '    End If
    'End Sub
#End Region

#Region "Save"
    Protected Sub SaveStatusReson()
        Try
            Dim StatusResonID As Long

            For iStatusResonID As Integer = 0 To grdMembershipStatusReson.Rows.Count - 1
                Dim lblStatusResonID As Label = CType(grdMembershipStatusReson.Rows(iStatusResonID).FindControl("lblStatusResonID"), Label)
                If Convert.ToInt32(lblStatusResonID.Text) > 0 Then
                    StatusResonID = Convert.ToInt32(lblStatusResonID.Text)
                    Exit For
                End If
            Next
            Dim oGEMembershipStatusReson As AptifyGenericEntityBase
            oGEMembershipStatusReson = AptifyApplication.GetEntityObject("MembershipApplicationStatusReasons__c", StatusResonID)
            oGEMembershipStatusReson.SetValue("StatusResponse", txtStatusResonse.Text.Trim)
            If oGEMembershipStatusReson.Save() Then
                txtStatusResonse.Text = ""
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    Protected Sub SaveITProgrammeTopiccodes()
        Try
            Dim dtITProgramme As DataTable
            Dim sSQL As String
            Dim params(2) As IDataParameter
            For Each i As DataListItem In Me.dlITProgramme.Items
                Dim chkITProgramme As CheckBox = CType(i.FindControl("chkITProgramme"), CheckBox)
                Dim lblITProgrammeTopicCodeID As Label = CType(i.FindControl("lblITProgrammeTopicCodeID"), Label)
                'sSQL = Database & "..spCheckDuplicateTopicCode @RecordID=" + lPersonId.ToString + ",@TopicCodeID=" + lblAreaIntTopicCodeID.Text.ToString + ",@EntityID=" + oPersonGE.EntityID().ToString
                sSQL = Database & _
                       "..spCheckDuplicateTopicCode__c"
                params(0) = Me.DataAction.GetDataParameter("@RecordID", SqlDbType.BigInt, Convert.ToInt32(ViewState("IsMembApp")))
                params(1) = Me.DataAction.GetDataParameter("@TopicCodeID", SqlDbType.BigInt, lblITProgrammeTopicCodeID.Text)
                params(2) = Me.DataAction.GetDataParameter("@EntityID", SqlDbType.BigInt, Convert.ToInt32(AptifyApplication.GetEntityID("MembershipApplication__c")))
                dtITProgramme = Me.DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                If dtITProgramme.Rows.Count > 0 Then
                    If chkITProgramme.Checked = True Then
                        AddTopicCodeLink(CLng(lblITProgrammeTopicCodeID.Text), Convert.ToInt32(dtITProgramme.Rows(0)("ID")))
                    Else
                        AddUnCheckTopicCodeLink(CLng(lblITProgrammeTopicCodeID.Text), Convert.ToInt32(dtITProgramme.Rows(0)("ID")))
                    End If

                Else
                    If chkITProgramme.Checked = True Then
                        AddTopicCodeLink(CLng(lblITProgrammeTopicCodeID.Text), -1)
                    End If
                End If
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

        End Try
    End Sub
    Protected Sub SaveCompanyLawModuleeTopiccodes()
        Try
            Dim dtCompanyLawModule As DataTable
            Dim sSQL As String
            Dim params(2) As IDataParameter
            For Each i As DataListItem In Me.dlCompanyLawModule.Items
                Dim chkCompanyLawModule As CheckBox = CType(i.FindControl("chkCompanyLawModule"), CheckBox)
                Dim lblCompanyLawModuleTopicCodeID As Label = CType(i.FindControl("lblCompanyLawModuleTopicCodeID"), Label)
                'sSQL = Database & "..spCheckDuplicateTopicCode @RecordID=" + lPersonId.ToString + ",@TopicCodeID=" + lblAreaIntTopicCodeID.Text.ToString + ",@EntityID=" + oPersonGE.EntityID().ToString
                sSQL = Database & _
                       "..spCheckDuplicateTopicCode__c"
                params(0) = Me.DataAction.GetDataParameter("@RecordID", SqlDbType.BigInt, Convert.ToInt32(ViewState("MembershipAppID")))
                params(1) = Me.DataAction.GetDataParameter("@TopicCodeID", SqlDbType.BigInt, lblCompanyLawModuleTopicCodeID.Text)
                params(2) = Me.DataAction.GetDataParameter("@EntityID", SqlDbType.BigInt, Convert.ToInt32(AptifyApplication.GetEntityID("MembershipApplication__c")))
                dtCompanyLawModule = Me.DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                If dtCompanyLawModule.Rows.Count > 0 Then
                    If chkCompanyLawModule.Checked = True Then
                        AddTopicCodeLink(CLng(lblCompanyLawModuleTopicCodeID.Text), Convert.ToInt32(dtCompanyLawModule.Rows(0)("ID")))
                    Else
                        AddUnCheckTopicCodeLink(CLng(lblCompanyLawModuleTopicCodeID.Text), Convert.ToInt32(dtCompanyLawModule.Rows(0)("ID")))
                    End If

                Else
                    If chkCompanyLawModule.Checked = True Then
                        AddTopicCodeLink(CLng(lblCompanyLawModuleTopicCodeID.Text), -1)
                    End If
                End If
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub SavRecognisedExperienceForQualificationTopiccodes()
        Try
            Dim dtCompanyLawModule As DataTable
            Dim sSQL As String
            Dim params(2) As IDataParameter
            For Each i As DataListItem In Me.dlREQ.Items
                Dim chkREQ As CheckBox = CType(i.FindControl("chkREQ"), CheckBox)
                Dim lblREQTopicCodeID As Label = CType(i.FindControl("lblREQTopicCodeID"), Label)
                'sSQL = Database & "..spCheckDuplicateTopicCode @RecordID=" + lPersonId.ToString + ",@TopicCodeID=" + lblAreaIntTopicCodeID.Text.ToString + ",@EntityID=" + oPersonGE.EntityID().ToString
                sSQL = Database & _
                       "..spCheckDuplicateTopicCode__c"
                params(0) = Me.DataAction.GetDataParameter("@RecordID", SqlDbType.BigInt, Convert.ToInt32(ViewState("MembershipAppID")))
                params(1) = Me.DataAction.GetDataParameter("@TopicCodeID", SqlDbType.BigInt, lblREQTopicCodeID.Text)
                params(2) = Me.DataAction.GetDataParameter("@EntityID", SqlDbType.BigInt, Convert.ToInt32(AptifyApplication.GetEntityID("MembershipApplication__c")))
                dtCompanyLawModule = Me.DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                If dtCompanyLawModule.Rows.Count > 0 Then
                    If chkREQ.Checked = True Then
                        AddTopicCodeLink(CLng(lblREQTopicCodeID.Text), Convert.ToInt32(dtCompanyLawModule.Rows(0)("ID")))
                    Else
                        AddUnCheckTopicCodeLink(CLng(lblREQTopicCodeID.Text), Convert.ToInt32(dtCompanyLawModule.Rows(0)("ID")))
                    End If

                Else
                    If chkREQ.Checked = True Then
                        AddTopicCodeLink(CLng(lblREQTopicCodeID.Text), -1)
                    End If
                End If
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub SetOrderInMembershipApp()
        Try
            Dim oMembershipAppGE As AptifyGenericEntityBase
            oMembershipAppGE = AptifyApplication.GetEntityObject("MembershipApplication__c", Convert.ToInt32(ViewState("IsMembApp")))
            If oMembershipAppGE.GetValue("MembershipApplicationStatusID") <> Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("MembershipApplicationStatus__c", "Submitted")) Then
                oMembershipAppGE.SetValue("MembershipApplicationStatusID", Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("MembershipApplicationStatus__c", "Submitted")))
                oMembershipAppGE.Save()
            End If

            If oMembershipAppGE.GetValue("MembershipApplicationStatusID") = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("MembershipApplicationStatus__c", "Submitted")) Then
                btnSubmit.Visible = False
                cmdSave.Visible = False
                Dim sApplicationStatus As String = "..spGetMembershipAppStatus__c @Id=" & CInt(oMembershipAppGE.GetValue("MembershipApplicationStatusID"))
                Dim sApplicationWebName As String = Convert.ToString(DataAction.ExecuteScalar(sApplicationStatus))
                If sApplicationWebName <> "" Then
                    lblApplicationType.Text = sApplicationWebName
                End If
            End If
            Dim oGE As AptifyGenericEntityBase
            oGE = AptifyApplication.GetEntityObject("Persons", AptifyEbusinessUser1.PersonID)
            Dim lApplicationID As Long = Convert.ToInt32(oGE.GetValue("ApplicationTypeID__c"))

            Dim sSql As String = "..spGetMembershipProduct__c @ApplicationID=" & lApplicationID
            Dim ProductID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql))
            If ProductID > 0 Then

                'If ShoppingCart1.AddToCart(ProductID, False) Then
                '    ShoppingCart1.SaveCart(Page.Session)
                '    Session("IsMembApp") = ViewState("IsMembApp")
                '    Response.Redirect(ViewCart, False)
                'End If
                ' get Quatation Order ID
                Dim sOrderIDSQL As String = Database & "..spGetMembershipProductFromGroupProductID__c @GroupProductID=" & ProductID & ",@ShipToID=" & oGE.GetValue("ID")
                Dim lOrderID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sOrderIDSQL))
                If lOrderID > 0 Then
                    ViewState("OrderID") = lOrderID
                    oMembershipAppGE.SetValue("OrderID", lOrderID)
                    oMembershipAppGE.Save()
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

        End Try
    End Sub
#End Region

#Region "Others"
    Protected Sub cmbCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCountry.SelectedIndexChanged
        Try

            PopulateState(cmbState, cmbCountry)
            txtZipCode.Focus()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
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
                If cmb IsNot Nothing And Not cmb.Items.FindByValue(sState) Is Nothing Then
                    cmb.ClearSelection()
                    cmb.SelectedValue = sState
                End If

                ''RashmiP, removed assigned Area code.
                County = sCounty

            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub txtCompany_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompany.TextChanged
        Try
            Dim sCompanyName() As String = txtCompany.Text.Trim.Split("\")
            Dim SSql As String = "spGetCompanyAddressDetails__c @CompanyName='" & sCompanyName(0) & "'"
            Dim dt As DataTable = DataAction.GetDataTable(SSql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ' if data found then display Address
                txtTrainingFirmLine1.Text = Convert.ToString(dt.Rows(0).Item("AddressLine1"))
                txtTrainingFirmLine2.Text = Convert.ToString(dt.Rows(0).Item("AddressLine2"))
                txtTrainingFirmLine3.Text = Convert.ToString(dt.Rows(0).Item("AddressLine3"))
                txtTrainingFirmCity.Text = Convert.ToString(dt.Rows(0).Item("City"))
                txtTrainingFirmTelephone.Text = Convert.ToString(dt.Rows(0).Item("MainPhone"))
                'SetComboValue(cmbTrainingFirmCountry, IIf(Convert.ToString(dt.Rows(0)("Country")) = "", "United States", Convert.ToString(dt.Rows(0)("Country")).ToString))
                'PopulateState(cmbTrainingFirmState, cmbTrainingFirmCountry)
                'SetComboValue(cmbTrainingFirmState, Convert.ToString(dt.Rows(0)("State")))
            Else
                txtTrainingFirmLine1.Enabled = False
                txtTrainingFirmLine1.Text = ""
                txtTrainingFirmLine2.Enabled = False
                txtTrainingFirmLine2.Text = ""
                txtTrainingFirmLine3.Enabled = False
                txtTrainingFirmLine3.Text = ""
                txtTrainingFirmCity.Enabled = False
                txtTrainingFirmCity.Text = ""
                txtTrainingFirmTelephone.Enabled = False
                txtTrainingFirmTelephone.Text = 1
                cmbTrainingFirmCountry.Enabled = False
                SetComboValue(cmbTrainingFirmCountry, "Ireland")
                cmbTrainingFirmState.Enabled = False
                PopulateState(cmbTrainingFirmState, cmbTrainingFirmCountry)
                txtTrainingFirmZipCode.Enabled = False
                txtTrainingFirmZipCode.Text = ""
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub cmbTrainingFirmCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTrainingFirmCountry.SelectedIndexChanged
        Try
            PopulateState(cmbTrainingFirmState, cmbTrainingFirmCountry)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub cmbFirmCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFirmCountry.SelectedIndexChanged
        Try
            PopulateState(cmbFirmState, cmbFirmCountry)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub txtFirmName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFirmName.TextChanged
        Try
            Dim sCompanyName() As String = txtCompany.Text.Trim.Split("\")
            Dim SSql As String = "spGetCompanyAddressDetails__c @CompanyName='" & sCompanyName(0) & "'"
            Dim dt As DataTable = DataAction.GetDataTable(SSql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ' if data found then display Address
                txtFirmLine1.Text = Convert.ToString(dt.Rows(0).Item("AddressLine1"))
                txtFirmLine2.Text = Convert.ToString(dt.Rows(0).Item("AddressLine2"))
                txtFirmLine3.Text = Convert.ToString(dt.Rows(0).Item("AddressLine3"))
                txtFirmCity.Text = Convert.ToString(dt.Rows(0).Item("City"))
                txtFirmTelephoneNo.Text = Convert.ToString(dt.Rows(0).Item("MainPhone"))
                'SetComboValue(cmbFirmCountry, IIf(Convert.ToString(dt.Rows(0)("Country")) = "", "United States", Convert.ToString(dt.Rows(0)("Country")).ToString))
                'PopulateState(cmbFirmState, cmbFirmCountry)
                'SetComboValue(cmbFirmState, Convert.ToString(dt.Rows(0)("State")))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region
#Region "Popup Button Click"
    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Try
            radMockTrial.VisibleOnPageLoad = False
            If Session("YesButtonClick") = "" Then
                Session("YesButtonClick") = "Yes"
                DoSave()
                SetOrderInMembershipApp()
                btnPrint.Visible = True
                Response.Redirect(Request.RawUrl, False)
                Exit Sub
            Else
                Response.Redirect(Request.RawUrl, False)
                Exit Sub
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Try
            radMockTrial.VisibleOnPageLoad = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub btnPayOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPayOk.Click
        Try
            radSubmitPay.VisibleOnPageLoad = False
            If Session("PayButtonClick") = "" Then
                Session("PayButtonClick") = "Yes"
                DoSave()
                SetOrderInMembershipApp()
                If Convert.ToInt32(ViewState("OrderID")) > 0 Then
                    Response.Redirect(MakePaymentFormPage, False)
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnPayNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPayNo.Click
        Try
            radSubmitPay.VisibleOnPageLoad = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region


    'Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

    '    CollapsiblePanelExtender3.Collapsed = True

    '    CollapsiblePanelExtender4.Collapsed = True
    '    CollapsiblePanelExtender5.Collapsed = True
    '    CollapsiblePanelExtender8.Collapsed = True

    '    CollapsiblePanelExtender9.Collapsed = True
    '    CollapsiblePanelExtender7.Collapsed = True

    'End Sub

    Protected Sub imbCollaps_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbCollaps.Click
        CollapsiblePanelExtender1.Collapsed = True
        CollapsiblePanelExtender1.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender2.Collapsed = True
        CollapsiblePanelExtender2.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender3.Collapsed = True
        CollapsiblePanelExtender3.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender4.Collapsed = True
        CollapsiblePanelExtender4.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender5.Collapsed = True
        CollapsiblePanelExtender5.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender6.Collapsed = True
        CollapsiblePanelExtender6.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender7.Collapsed = True
        CollapsiblePanelExtender7.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender8.Collapsed = True
        CollapsiblePanelExtender8.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender9.Collapsed = True
        CollapsiblePanelExtender9.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender10.Collapsed = True
        CollapsiblePanelExtender10.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender11.Collapsed = True
        CollapsiblePanelExtender11.ClientState = True.ToString().ToLower()
        CollapsiblePanelExtender12.Collapsed = True
        CollapsiblePanelExtender12.ClientState = True.ToString().ToLower()
    End Sub

    Protected Sub imbExpand_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbExpand.Click
        CollapsiblePanelExtender1.Collapsed = False
        CollapsiblePanelExtender1.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender2.Collapsed = False
        CollapsiblePanelExtender2.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender3.Collapsed = False
        CollapsiblePanelExtender3.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender4.Collapsed = False
        CollapsiblePanelExtender4.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender5.Collapsed = False
        CollapsiblePanelExtender5.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender6.Collapsed = False
        CollapsiblePanelExtender6.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender7.Collapsed = False
        CollapsiblePanelExtender7.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender8.Collapsed = False
        CollapsiblePanelExtender8.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender9.Collapsed = False
        CollapsiblePanelExtender9.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender10.Collapsed = False
        CollapsiblePanelExtender10.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender11.Collapsed = False
        CollapsiblePanelExtender11.ClientState = False.ToString().ToLower()
        CollapsiblePanelExtender12.Collapsed = False
        CollapsiblePanelExtender12.ClientState = False.ToString().ToLower()
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'lnkChangeContactDetails.Visible = False
        ' lnkChangeEmploymentDetails.Visible = False
        'CollapsiblePanelExtender1.Collapsed = False
        'CollapsiblePanelExtender1.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender2.Collapsed = False
        'CollapsiblePanelExtender2.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender3.Collapsed = False
        'CollapsiblePanelExtender3.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender4.Collapsed = False
        'CollapsiblePanelExtender4.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender5.Collapsed = False
        'CollapsiblePanelExtender5.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender6.Collapsed = False
        'CollapsiblePanelExtender6.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender7.Collapsed = False
        'CollapsiblePanelExtender7.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender8.Collapsed = False
        'CollapsiblePanelExtender8.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender9.Collapsed = False
        'CollapsiblePanelExtender9.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender10.Collapsed = False
        'CollapsiblePanelExtender10.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender11.Collapsed = False
        'CollapsiblePanelExtender11.ClientState = False.ToString().ToLower()
        'CollapsiblePanelExtender12.Collapsed = False
        'CollapsiblePanelExtender12.ClientState = False.ToString().ToLower()
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "script1", "javascript:printDiv();", True)
        Try
            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "AppToMembershipForm_WEB"))
            Dim rptParam As New AptifyCrystalReport__c
            rptParam.ReportID = ReportID
            rptParam.Param1 = Convert.ToString(AptifyEbusinessUser1.PersonID)
            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Added BY Pradip 2016-06-03 For G1-77 Item
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub lnkChangeEmploymentDetails_Click(sender As Object, e As System.EventArgs) Handles lnkChangeEmploymentDetails.Click
        Try
            'Response.Redirect(ProfilePage, False)
            Dim strFilePath As String = ProfilePage & "?MemberShip=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt("Y"))
            Response.Redirect(strFilePath, False)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub lnkChangeContactDetails_Click(sender As Object, e As System.EventArgs) Handles lnkChangeContactDetails.Click
        Try
            ' Response.Redirect(ProfilePage, False)
            Dim strFilePath As String = ProfilePage & "?MemberShip=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt("Y"))
            Response.Redirect(strFilePath, False)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub lnkPD2_Click(sender As Object, e As EventArgs) Handles lnkPD2.Click
        Try
            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "CA Diary PD2 Report"))
            Dim rptParam As New AptifyCrystalReport__c
            rptParam.ReportID = ReportID
            rptParam.Param1 = Convert.ToString(AptifyEbusinessUser1.PersonID)
            rptParam.SubParam1 = Convert.ToString(AptifyEbusinessUser1.PersonID)
            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class
