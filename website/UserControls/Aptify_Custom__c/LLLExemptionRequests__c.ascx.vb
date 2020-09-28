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
    Partial Class LLLExemptionRequests__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "LLLExemptionRequests__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Protected Const ATTRIBUTE_CONTROL_BACKURL_NAME As String = "BackUrl"
        Protected Const ATTRIBUTE_CONTROL_EROLL_NAME As String = "Enroll"
        Protected Const ATTRIBUTE_SECURITYERROR_PAGE As String = "securityErrorPage"
        Dim ClassID As Integer
        Dim iVenueID As Integer

#Region "Page Load"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

            Try
                SetProperties()
                If AptifyEbusinessUser1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If


                If Not IsPostBack Then
                    lblDesc.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLRequestApp.OptionDesc__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    If Not Request.QueryString("ID") Is Nothing Then
                        pnlDetails.Visible = False
                        PnlExemption.Visible = False


                        Dim ClassID As Long = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))
                        LoadHeaderText()
                        LoadTypes()
                        LoadAccountancy()
                        raSupportLetter.AllowAdd = True
                        raSupportLetter.AllowDelete = True
                        raSupportLetter.AllowView = True
                        raSupportLetter.EntityID = AptifyApplication.GetEntityID("LLLRequestApplications__c")
                        raSupportLetter.CategoryID = AptifyApplication.GetEntityRecordIDFromRecordName(AptifyApplication.GetEntityID("Attachment Categories"), "CTC")

                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


#End Region

#Region "Properties"

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

#End Region


#Region "Private Functions"
        Private Sub LoadHeaderText()
            Try

                Dim sSqlPerson As String = Database & "..spGetPersonMemberType__c @PersonID=" & AptifyEbusinessUser1.PersonID
                Dim dtPerson As DataTable = DataAction.GetDataTable(sSqlPerson, IAptifyDataAction.DSLCacheSetting.BypassCache)
                lblStudentno.Text = Convert.ToString(dtPerson.Rows(0)("oldid"))
                lblStudentID.Text = Convert.ToString(dtPerson.Rows(0)("ID"))
                lblStudentName.Text = AptifyEbusinessUser1.FirstName + " " + AptifyEbusinessUser1.LastName
                lblStudentName.Text = AptifyEbusinessUser1.FirstName + " " + AptifyEbusinessUser1.LastName

                Dim sSqlCourse As String = Database & "..spGetLLLCloursefromClass__c @ClassID=" & Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))
                Dim dtCourse As DataTable = DataAction.GetDataTable(sSqlCourse, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtCourse Is Nothing AndAlso dtCourse.Rows.Count > 0 Then

                    lblQualificationID.Text = Convert.ToString(dtCourse.Rows(0)("CourseID"))
                    lblQualification.Text = Convert.ToString(dtCourse.Rows(0)("Name"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub CheckMemberType()
            Try
               
                Dim sSqMemberType As String = Database & "..spGetPersonMemberType__c @PersonID=" & AptifyEbusinessUser1.PersonID
                Dim dtMemberType As DataTable = DataAction.GetDataTable(sSqMemberType, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtMemberType Is Nothing AndAlso dtMemberType.Rows.Count > 0 Then
                    If (Convert.ToBoolean(dtMemberType.Rows(0)("IsMember")) = "1") Then
                        divIsmember.Visible = True
                        divIsnonsmember.Visible = False
                        divupload.Visible = True
                    Else
                        divIsmember.Visible = True
                        divIsnonsmember.Visible = True
                        divupload.Visible = True
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Function DoSave() As Boolean
            Try



                    Dim oLLLApplicationGE As AptifyGenericEntityBase
                    oLLLApplicationGE = AptifyApplication.GetEntityObject("LLLRequestApplications__c", -1)
                oLLLApplicationGE.SetValue("StudentID", Convert.ToInt32(lblStudentID.Text))
                    oLLLApplicationGE.SetValue("Qualification", Convert.ToInt32(lblQualificationID.Text))

                    oLLLApplicationGE.SetValue("Comment", txtcomment.Text)
                oLLLApplicationGE.SetValue("TypeID", ddlType.SelectedValue)
                If (drpAccountancy.SelectedIndex > 0) Then
                    oLLLApplicationGE.SetValue("Accountancybody", drpAccountancy.SelectedValue)
                Else
                    oLLLApplicationGE.SetValue("Accountancybody", 0)
                End If

                oLLLApplicationGE.SetValue("RequestDate", New Date(Today.Year, Today.Month, Today.Day).ToShortDateString())
                If (ddlType.SelectedItem.Text.ToUpper = "CTC FULL EXEMPTION") Then
                    oLLLApplicationGE.SetValue("Status", "Submitted to CAI")
                ElseIf (ddlType.SelectedItem.Text.ToUpper = "CTC PARTIAL EXEMPTION") Then

                    Dim sSqlCer As String = Database & "..spGetCheckforDiplomanadMasterEveCertificate__c @Student=" & Convert.ToInt16(lblStudentID.Text)
                    Dim dtCer As DataTable = DataAction.GetDataTable(sSqlCer, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dtCer Is Nothing AndAlso dtCer.Rows.Count > 0 Then
                        If (Convert.ToString(dtCer.Rows(0)("Curriculum")).ToUpper = "DIPLOMA IN TAXATION") Then
                            oLLLApplicationGE.SetValue("CompletedDiplomaInTaxation", 1)
                        ElseIf (Convert.ToString(dtCer.Rows(0)("Curriculum")).ToUpper = "MASTER EVENTS") Then
                            oLLLApplicationGE.SetValue("CompletedMastersEvents", 1)
                        End If
                        oLLLApplicationGE.SetValue("Status", "Submitted to CAI")
                    End If



                End If
                    Dim chkIsOtherTaxationBodyMembeval As Boolean
                    If (chkIsOtherTaxationBodyMember.Checked = True) Then
                        chkIsOtherTaxationBodyMembeval = 1
                    Else
                        chkIsOtherTaxationBodyMembeval = 0
                End If
                oLLLApplicationGE.SetValue("Status", "Submitted to CAI")
                oLLLApplicationGE.SetValue("IsOtherTaxationBodyMember", chkIsOtherTaxationBodyMembeval)
                
                 'Added By Kavita Zinage 21/03/2016
                If Not Request.QueryString("ID") Is Nothing Then
                    Dim iclassID As Integer = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))
                    If iclassID > 0 Then
                        oLLLApplicationGE.SetValue("ClassID", iclassID)
                    End If
                End If
                'Till Here

                Dim sError As String = String.Empty
                If oLLLApplicationGE.Save(False, sError) Then
                    oLLLApplicationGE.Save()
                    raSupportLetter.AllowAdd = True
                    raSupportLetter.AllowDelete = True
                    raSupportLetter.AllowView = True
                    raSupportLetter.EntityID = AptifyApplication.GetEntityID("LLLRequestApplications__c")
                    raSupportLetter.CategoryID = AptifyApplication.GetEntityRecordIDFromRecordName(AptifyApplication.GetEntityID("Attachment Categories"), "CTC")

                    raSupportLetter.RecordID = oLLLApplicationGE.RecordID
                    raSupportLetter.SaveAttachment()
                End If
             



                Return True


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        Private Function Validation() As Boolean
            Try

                lblsubmit.Visible = False
                lblErrorFile.Visible = False


                Dim sSql As String = Database & "..spCheckPersonIsRegisterForLLLReqApp__c @StudentID=" & AptifyEbusinessUser1.PersonID & ",@TypeID=" & ddlType.SelectedValue & ",@IsCTCRequest=" & 1
                Dim dt As DataTable = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    If (Convert.ToBoolean(dt.Rows(0)(0)) = True) Then
                        lblsubmit.Visible = True

                        lblsubmit.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLExemption.DuplicateLLLRequest__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        Return False
                    Else
                        lblsubmit.Visible = False
                        '  Return True
                    End If
                End If
                If (chkIsOtherTaxationBodyMember.Checked = False) Then
                    lblErrorFile.Visible = False
                End If
                If (chkIsOtherTaxationBodyMember.Checked = True) Then

                    If (raSupportLetter.grdcount > 0) Then
                        lblErrorFile.Visible = False

                    Else
                        lblErrorFile.Visible = True
                        lblErrorFile.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLExemption.ValidateUpload__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        Return False
                    End If
                Else

                    lblErrorFile.Visible = False


                End If


                Return True

            Catch ex As Exception
                Return False
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        Private Function CheckexistsValidation() As Boolean
            Try

                lblsubmit.Visible = False
                lblErrorFile.Visible = False


                Dim sSql As String = Database & "..spGetAlreadyexistLLLrequest__c @StudentID=" & AptifyEbusinessUser1.PersonID
                Dim dt As DataTable = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    If (Convert.ToBoolean(dt.Rows(0)(0)) = True) Then
                        lblsubmit.Visible = True

                        lblsubmit.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLExemption.DuplicateLLLRequest__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        lblsubmit.Visible = True
                        ddlType.SelectedValue = Convert.ToInt32(dt.Rows(0)(0))

                        Dim chk As Boolean = Convert.ToInt32(dt.Rows(0)(2))
                        If (chk = True) Then
                            chkIsOtherTaxationBodyMember.Checked = True
                        Else
                            chkIsOtherTaxationBodyMember.Checked = False
                        End If
                        txtcomment.Text = Convert.ToString(dt.Rows(0)(3))
                        Dim cate As String = AptifyApplication.GetEntityRecordIDFromRecordName(AptifyApplication.GetEntityID("Attachment Categories"), "CTC")
                        raSupportLetter.LoadAttachments("LLLRequestApplications__c", Convert.ToInt32(dt.Rows(0)(4)), Convert.ToInt32(cate))

                        'If (ddlType.SelectedItem.Text = "CTC Partial Exemption") Then
                        '    pnlDetails.Visible = True
                        '    divIsmember.Visible = False
                        '    divIsnonsmember.Visible = False
                        '    divupload.Visible = False
                        'Else
                        '    'pnlDetails.Visible = True
                        '    'divIsmember.Visible = False
                        '    'divIsnonsmember.Visible = True
                        '    'divupload.Visible = True
                        'End If
                        If (ddlType.SelectedItem.Text = "CTC Full Exemption") Then
                            Dim sSqMemberType As String = Database & "..spGetPersonMemberType__c @PersonID=" & AptifyEbusinessUser1.PersonID
                            Dim dtMemberType As DataTable = DataAction.GetDataTable(sSqMemberType, IAptifyDataAction.DSLCacheSetting.BypassCache)
                            If Not dtMemberType Is Nothing AndAlso dtMemberType.Rows.Count > 0 Then
                                If (Convert.ToBoolean(dtMemberType.Rows(0)("IsMember")) = "1") Then
                                    divIsmember.Visible = True
                                    divIsnonsmember.Visible = False
                                    divupload.Visible = True

                                    divIsmember.Disabled = True
                                    divIsnonsmember.Disabled = True
                                    divupload.Disabled = True
                                Else

                                    If Not (Convert.ToString(dt.Rows(0)(1))) Is Nothing Then
                                        drpAccountancy.SelectedValue = Convert.ToInt32(dt.Rows(0)(1))
                                    Else
                                        drpAccountancy.SelectedIndex = 0
                                    End If
                                    divIsmember.Visible = True
                                    divIsnonsmember.Visible = True
                                    divupload.Visible = True

                                    divIsmember.Disabled = True
                                    divIsnonsmember.Disabled = True
                                    divupload.Disabled = True
                                End If
                            End If
                        End If
                       



                        If (ddlType.SelectedItem.Text = "CTC Partial Exemption") Then
                            pnlDetails.Visible = True
                            divIsmember.Visible = False
                            divIsnonsmember.Visible = False
                            divupload.Visible = False
                        Else
                            'pnlDetails.Visible = True
                            'divIsmember.Visible = False
                            'divIsnonsmember.Visible = True
                            'divupload.Visible = True
                        End If

                        pnlDetails.Enabled = False
                        Eligibility.Disabled = True
                        divupload.Disabled = True
                        btnSubmit.Enabled = False


                        PnlExemption.Enabled = False

                        

                        Return False

                    Else
                        PnlExemption.Enabled = True
                        pnlDetails.Enabled = True
                        Eligibility.Disabled = False
                        divupload.Disabled = False
                        btnSubmit.Enabled = True


                        '  Return True
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
                Dim sSql As String = Database & "..spGetLLLRequestTypeForExemption__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ddlType.DataSource = dt
                    ddlType.DataTextField = "Name"
                    ddlType.DataValueField = "ID"
                    ddlType.DataBind()
                    ddlType.Items.Insert(0, "Select")

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadAccountancy()
            Try
                Dim sSql As String = Database & "..spGetAccountancybodyForExemption__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    drpAccountancy.DataSource = dt
                    drpAccountancy.DataTextField = "Name"
                    drpAccountancy.DataValueField = "ID"
                    drpAccountancy.DataBind()


                End If
                drpAccountancy.Items.Insert(0, "Select")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region

#Region "Events"


        Protected Sub rdException_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdException.CheckedChanged
            Try
                If (ddlType.SelectedItem.Text.ToUpper = "SELECT") Then
                    pnlDetails.Visible = True
                    PnlExemption.Visible = True
                    divIsmember.Visible = False
                    divIsnonsmember.Visible = False
                    divupload.Visible = False
                Else
                    PnlExemption.Visible = True
                    pnlDetails.Visible = True



                    CheckMemberType()
                    CheckexistsValidation()

                End If

              
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub rdEnroll_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdEnroll.CheckedChanged
            Try
                pnlDetails.Visible = False
                PnlExemption.Visible = False
                divIsmember.Visible = False
                divIsnonsmember.Visible = False

                Response.Redirect(EnrollURL & "?CID=" & Request.QueryString("ID"))

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try

                If (Validation() = True) Then
                    DoSave()
                    lblsuccess.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLExemptionRequests.SuccessMessage__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    RadAlert.VisibleOnPageLoad = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        


        Protected Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
            Try
                Response.Redirect(RedirectBackURL)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Response.Redirect(RedirectBackURL)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlType.SelectedIndexChanged
            Try
                If (ddlType.SelectedItem.Text.ToUpper = "CTC FULL EXEMPTION") Then
                    pnlDetails.Visible = True
                    lblsubmit.Visible = False
                    CheckMemberType()

                ElseIf (ddlType.SelectedItem.Text.ToUpper = "CTC PARTIAL EXEMPTION") Then
                    pnlDetails.Visible = True
                    divIsmember.Visible = False
                    divIsnonsmember.Visible = False
                    divupload.Visible = False
                    lblsubmit.Visible = False
                    ' CheckMemberType()
                ElseIf (ddlType.SelectedItem.Text.ToUpper = "SELECT") Then
                    pnlDetails.Visible = True
                    divIsmember.Visible = False
                    divIsnonsmember.Visible = False
                    divupload.Visible = False
                    lblsubmit.Visible = False

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

#End Region

        
    End Class
End Namespace
