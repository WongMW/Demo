Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Namespace Aptify.Framework.Web.eBusiness.Generated

   ''' <summary>
   ''' Generated ASP.NET User Control for the MembershipApplication__c entity.
   ''' Description: Staging entity used for a Membership Enrollment Wizard: This entity has been created a an aggregate of various fields from several entities. This is used by the Membership Enrollment Meta-Data Wizard as an easy way to access and update fields from several entities from a single Form Template.
   ''' </summary>
   ''' <remarks></remarks>
    Partial Class MembershipApplication__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "MembershipApplication__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Protected Const ATTRIBUTE_COMPANY_LOGO_IMAGE_URL As String = "CompanyLogoImage"
        Protected Const ATTRIBUTE_ApplicationImage_URL As String = "ApplicationImage"
        Protected Const ATTRIBUTE_COMPANY_LOGO_IMAGE_URL1 As String = "CompanyLogoImage0"
        Dim fname, lname, mname As String
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

        Public Overridable Property ApplicationImage() As String
            Get
                If Not ViewState(ATTRIBUTE_ApplicationImage_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ApplicationImage_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ApplicationImage_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Overridable Property CompanyLogoImage0() As String
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
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                SetProperties()
                LoadRecord()
                LoadCompany()
            End If
        End Sub
        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(CompanyLogoImage) Then
                CompanyLogoImage = Me.GetLinkValueFromXML(ATTRIBUTE_COMPANY_LOGO_IMAGE_URL)
                Me.companyLogo.Src = CompanyLogoImage
                Me.companyLogo0.Src = CompanyLogoImage
            End If

            If String.IsNullOrEmpty(ApplicationImage) Then
                ApplicationImage = Me.GetLinkValueFromXML(ATTRIBUTE_ApplicationImage_URL)
                Me.ApplicationHeadings.Src = ApplicationImage
            End If

            If String.IsNullOrEmpty(CompanyLogoImage0) Then
                CompanyLogoImage0 = Me.GetLinkValueFromXML(ATTRIBUTE_COMPANY_LOGO_IMAGE_URL)
                Me.companyLogo0.Src = CompanyLogoImage0
            End If

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(Me.RedirectURL) Then
                Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(Me.RedirectURL) Then
            End If
            If String.IsNullOrEmpty(Me.RedirectIDParameterName) Then Me.RedirectIDParameterName = "ID"
            If String.IsNullOrEmpty(Me.AppendRecordIDToRedirectURL) Then Me.AppendRecordIDToRedirectURL = "true"
            If String.IsNullOrEmpty(Me.EncryptQueryStringValue) Then Me.EncryptQueryStringValue = "true"
            If String.IsNullOrEmpty(Me.QueryStringRecordIDParameter) Then Me.QueryStringRecordIDParameter = "ID"
        End Sub
        Protected Overridable Sub LoadRecord()
            Try
                'If Me.SetControlRecordIDFromParam() Then
                '    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("MembershipApplication__c", ControlRecordID))
                'Else
                '    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("MembershipApplication__c", -1))
                'End If
                Dim sSQL As String = String.Empty
                Dim dtpersondetails As System.Data.DataTable = New DataTable()


                sSQL = Database & "..SpSelectPersonDetails__c @PersonID=" & AptifyEbusinessUser1.PersonID.ToString()
                dtpersondetails = Me.DataAction.GetDataTable(sSQL)
                dtpersondetails = DataAction.GetDataTable(sSQL)

                TxtPerson.Text = (dtpersondetails.Rows(0)("FirstName")) + "  " + (dtpersondetails.Rows(0)("MiddleName")) + " " + (dtpersondetails.Rows(0)("LastName"))
                TxtLine1.Text = dtpersondetails.Rows(0)("Line1")
                TxtLine2.Text = dtpersondetails.Rows(0)("Line2")
                TxtLine3.Text = dtpersondetails.Rows(0)("Line3")
                TxtLine4.Text = dtpersondetails.Rows(0)("Line4")
                txtcitystae.Text = (dtpersondetails.Rows(0)("City")) + "   Country:- " + (dtpersondetails.Rows(0)("County"))
                TxtEmail.Text = dtpersondetails.Rows(0)("Email1")
                TxtLandlineNo.Text = dtpersondetails.Rows(0)("Phone")
                Txtpmobileno.Text = dtpersondetails.Rows(0)("cellphone")
                rdpBirthdate.SelectedDate = dtpersondetails.Rows(0)("Birthday")
                rdpformentrydate.SelectedDate = Date.Today
                Txttelephone.Text = dtpersondetails.Rows(0)("Phone")
                TextBox13.Text = dtpersondetails.Rows(0)("FirstName").ToString()
                TextBox14.Text = dtpersondetails.Rows(0)("MiddleName").ToString()
                TextBox15.Text = dtpersondetails.Rows(0)("LastName").ToString()
                TextBox13.Visible = True
                TextBox14.Visible = True
                TextBox15.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub SaveRecord(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            Dim oGE As AptifyGenericEntityBase
            Dim bRedirect As Boolean = False

            Try
                If Me.ControlRecordID > 0 Then
                    oGE = AptifyApplication.GetEntityObject("MembershipApplication__c", Me.ControlRecordID)
                Else
                    oGE = AptifyApplication.GetEntityObject("MembershipApplication__c", -1)
                    oGE.SetValue("CompanyID", CLng(ddlcompany.SelectedValue))
                    oGE.SetValue("CompanyName", ddlcompany.Text)
                    oGE.SetValue("PersonID", CLng(AptifyEbusinessUser1.PersonID))
                    oGE.SetValue("PersonFirstName", TextBox13.Text.Trim())
                    oGE.SetValue("PersonMiddleName", TextBox14.Text.Trim())
                    oGE.SetValue("PersonLastName", TextBox15.Text.Trim())
                    oGE.SetValue("PersonLastName", TextBox15.Text.Trim())
                    oGE.SetValue("MembershipDuesProductID", 1)
                    oGE.SetValue("ApplicationSubmissionDate", rdpformentrydate.SelectedDate)
                    oGE.SetValue("trainingExpirydate", RdpExpirydate.SelectedDate)
                    'oGE.SetValue("
                    If ChkFTR.Checked = True Then
                        oGE.SetValue("FinalTrainigReview", 1)
                    Else
                        oGE.SetValue("FinalTrainigReview", 0)
                    End If
                    If ChkFTR.Checked = True Then
                        oGE.SetValue("RecordSummaryForm", 1)
                    Else
                        oGE.SetValue("RecordSummaryForm", 0)
                    End If

                    If ChkHome.Checked = True Then
                        oGE.SetValue("correspondenceTo", "Home")
                    Else
                        oGE.SetValue("correspondenceTo", "Office")
                    End If


                End If
                If oGE.Save(False) Then
                    bRedirect = True
                Else
                    lblError.Visible = True
                    lblError.Text = oGE.LastError()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try



        End Sub
        Private Sub LoadCompany()
            Try
                Dim sSQL As String = String.Empty
                Dim dtcompany As System.Data.DataTable = New DataTable()
                If ddlcompany.Items.Count = 0 Then

                    sSQL = Database & "..spGetCompanydetails__c"
                    dtcompany = Me.DataAction.GetDataTable(sSQL)
                    dtcompany = DataAction.GetDataTable(sSQL)
                    ddlcompany.DataSource = dtcompany
                    ddlcompany.DataTextField = "Name"
                    ddlcompany.DataValueField = "ID"
                    ddlcompany.DataBind()

                    ddlcompany.Items.Insert(0, New System.Web.UI.WebControls.ListItem("--Select Range--", "-1"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub ddlcompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcompany.SelectedIndexChanged
            'companyTelephone__c
            Dim sSQL As String = String.Empty
            Dim dtPhn As System.Data.DataTable = New DataTable()
            sSQL = Database & "..companyTelephone__c @CompanyID=" & ddlcompany.SelectedValue.ToString()
            dtPhn = Me.DataAction.GetDataTable(sSQL)
            TxtCompphn.Text = dtPhn.Rows(0)("Phone")
        End Sub

        Protected Sub ChkOffice_CheckedChanged(sender As Object, e As EventArgs) Handles ChkOffice.CheckedChanged
            If ChkOffice.Checked = True Then
                ChkOffice.Enabled = True
                ChkHome.Enabled = False
            Else
                ChkHome.Enabled = True
            End If

        End Sub

        Protected Sub ChkHome_CheckedChanged(sender As Object, e As EventArgs) Handles ChkHome.CheckedChanged
            If ChkHome.Checked = True Then
                ChkOffice.Enabled = False
                ChkHome.Enabled = True
            Else
                ChkOffice.Enabled = True
            End If
        End Sub



        Protected Sub ChkFTR_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFTR.CheckedChanged
            If ChkFTR.Checked = True Then
                Chksummaryform.Enabled = False
                ChkFTR.Enabled = True
            Else
                Chksummaryform.Enabled = True
            End If
        End Sub
    End Class
End Namespace
