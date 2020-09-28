'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande              12/12/2011                      Save Abatement Form
'Rajesh Kardile            07/10/2014                      Replace Hard code "Pound" value from entity attribute
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports System.Web.UI
Partial Class AbatmentsFormData__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "AbatmentsFormData__c"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURLs"
    Protected Const ATTRIBUTE_COMPANY_LOGO_IMAGE_URL As String = "CompanyLogoImage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage As String = "ProfilePage"
    Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage" ' added for #18752
    Private abyear As String = ""
    Protected AbatmentID As Long = -1
    Protected MembershipAppID As Long = -1
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
    Public Overridable Property RedirectURLs() As String
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

    Public Property AbatementYear() As String
        Get
            Return abyear
        End Get
        Set(ByVal value As String)
            abyear = value
        End Set
    End Property
    ' Added for #18752
    Public Overridable Property LoginPage() As String
        Get
            If Not ViewState(ATTRIBUTE_LOGIN_PAGE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_LOGIN_PAGE))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_LOGIN_PAGE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Protected Overrides Sub SetProperties()

        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME

        MyBase.SetProperties()
        If String.IsNullOrEmpty(RedirectURLs) Then
            Me.RedirectURLs = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
        End If
        If String.IsNullOrEmpty(ProfilePage) Then
            ProfilePage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_ProfilePage)
        End If
        If String.IsNullOrEmpty(LoginPage) Then
            'since value is the 'default' check the XML file for possible custom setting
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_LOGIN_PAGE)
        End If
    End Sub

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True 'added by GM for Redmine #20181
            MembershipAppID = Convert.ToInt32(Request.QueryString("AppID"))
            If Not IsPostBack Then
                SetProperties()
                If AptifyEbusinessUser1.PersonID > 0 Then
                    GetPrefferedCurrency()
                    raUploadDocs.AllowAdd = True
                    raUploadDocs.AllowDelete = True
                    raUploadDocs.AllowView = True
                    raUploadDocs.EntityID = AptifyApplication.GetEntityID("Abatements__c")
                    raUploadDocs.CategoryID = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName(AptifyApplication.GetEntityID("Attachment Categories"), "Abatements Document"))
                    GetLevelOfIncomeAsPerCurrency()
                    GetStatusWebName()
                    lblDate.Text = Now.ToShortDateString
                    AbatementTypes()
                    GetAbatementDetails()
                    LoadRecord()
                    lblAbtementFormBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.AbtementFormBottom")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                Else
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage + "?ReturnURL=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Request.RawUrl)))
                End If

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' COde added by Govind for redmine issue 14941
    Private Sub AbatementTypes()
        Try
            Dim sSql As String = Database & "..spGetAbatementTypeDetails__c @PersonID=" & AptifyEbusinessUser1.PersonID
            Dim dt As DataTable = DataAction.GetDataTable(sSql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ddlAbatementType.DataSource = dt
                ddlAbatementType.DataTextField = "Name"
                ddlAbatementType.DataValueField = "ID"
                ddlAbatementType.DataBind()
            End If
            ddlAbatementType.Items.Insert(0, "--Select--")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

        End Try
    End Sub
    Private Sub GetLevelOfIncomeAsPerCurrency()
        Try
            Dim sSql As String = Database & "..spGetLevelOfIncomeAsPerCurrency__c @CurrencyName=" & ViewState("CurrencyName") ' Redmine #17450
            Dim dt As DataTable = DataAction.GetDataTable(sSql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ddlstatus.DataSource = dt
                ddlstatus.DataTextField = "Name"
                ddlstatus.DataValueField = "ID"
                ddlstatus.DataBind()
            End If
            ddlstatus.Items.Insert(0, "--Select--")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetPrefferedCurrency()
        Try
            Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & AptifyEbusinessUser1.PersonID
            Dim dt As DataTable = DataAction.GetDataTable(sSql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                lblPrefeeredCurrency.Text = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                ViewState("CurrencyTypeID") = Convert.ToInt32(dt.Rows(0)("ID"))
                ViewState("CurrencyName") = Convert.ToString(dt.Rows(0)("Name"))
            End If


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetStatusWebName()
        Try
            Dim sSql As String = "..spGetAbatementStatusForInProgress__c @ID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AbatementStatus__c", "In Progress")
            Dim sWebName As String = Convert.ToString(DataAction.ExecuteScalar(sSql))
            If sWebName.Trim <> "" Then
                lblStatus.Text = sWebName
            End If
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Get the Abatment details as per person id
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub GetAbatementDetails()
        Try
            Me.raUploadDocs.Visible = True
            Dim EntityId As Long
            Dim ssql As String
            EntityId = CLng(Me.AptifyApplication.GetEntityID("Abatements__c"))
            If Not Session("EntityID") Is Nothing AndAlso Convert.ToInt32(Session("EntityID")) > 0 Then
                ssql = Database & "..spGetAbatementDetails__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@EntityID=" & Session("EntityID")
            Else
                Dim lEntityID As Long = CLng(Me.AptifyApplication.GetEntityID("MembershipApplication__c"))
                ssql = Database & "..spGetAbatementDetails__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@EntityID=" & lEntityID
            End If
            Dim dt As DataTable = DataAction.GetDataTable(ssql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                trRecordAttachment.Visible = True
                For Each dr As DataRow In dt.Rows
                    txtReason.Text = dr("AbatementReasons")
                    ViewState("AbatementTypeID") = Convert.ToInt32(dr("AbatementTypeID"))
                    'redmine issue 14941
                    If Convert.ToInt32(ViewState("AbatementTypeID")) > 0 Then
                        SetComboValue(ddlAbatementType, Convert.ToString(dr("AbatementType")))
                    End If
                    SetComboValue(ddlstatus, Convert.ToString(dr("LevelOfIncome")))
                    txtAnnualIncome.Text = Format(dr("AnnualIncome"), "0.00")
                    ' lblStatus.Text = Convert.ToString(dr("Name"))
                    lblfname.Text = Convert.ToString(dr("FirstName"))
                    lblsname.Text = Convert.ToString(dr("MiddleName"))
                    lbllname.Text = Convert.ToString(dr("LastName"))
                    lblsalutation.Text = Convert.ToString(dr("Salutation"))
                    TxtEmail.Text = Convert.ToString(dr("EmailAddress"))
                    lblDate.Text = Convert.ToString(dr("MemberSignatureDate"))
                    txtPhoneAreaCode.Text = Convert.ToString(dr("AreaCode"))
                    txtPhone.Text = Convert.ToString(dr("PhoneNumber"))
                    Dim sSqlAbatementStatus As String = "..spGetAbatementStatusForInProgress__c @ID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AbatementStatus__c", Convert.ToString(dr("Name")))
                    Dim sWebName As String = Convert.ToString(DataAction.ExecuteScalar(sSqlAbatementStatus))
                    If sWebName.Trim <> "" Then
                        lblStatus.Text = sWebName
                    End If
                    If Convert.ToBoolean(dr("UseAnyInformation")) Then
                        Chkinfo.Checked = True
                    End If
                    'RecordAttachments__c.AllowAdd = True
                    'RecordAttachments__c.AllowDelete = True
                    If Convert.ToString(dr("Name")).Trim.ToLower <> "in progress" Then
                        EnableFalseAllFileds() ' if status not Sumbitted then user can not edit the Abatement form
                    End If

                    AbatmentID = Convert.ToInt32(dr("ID"))
                    ViewState("AbatmentID") = AbatmentID
                    Dim AttachmentCategoryID As Integer = AptifyApplication.GetEntityRecordIDFromRecordName(AptifyApplication.GetEntityID("Attachment Categories"), "Abatements Document")
                    Me.raUploadDocs.LoadAttachments(EntityId, AbatmentID, AttachmentCategoryID)
                    If Convert.ToString(dr("RejectMessage")).Trim <> "" Then
                        lblRejectedReason.Visible = True
                        lblRejectedMessage.Visible = True
                        lblRejectedMessage.Text = Convert.ToString(dr("RejectMessage"))
                    End If
                    'Bind AbatementStatusReson
                    GetAbatementStatusReason(Convert.ToInt32(dr("ID")))
                    If Convert.ToString(dr("Name")).Trim.ToLower <> "in progress" Then
                        btnPrint.Visible = True
                    Else
                        btnPrint.Visible = False
                    End If
                    If Convert.ToBoolean(dr("Isnotbereimbursedbycompany")) Then
                        chkPartTimeEmp.Checked = True
                    End If
                    If Convert.ToBoolean(dr("ISunremunerated")) Then
                        chkAbatementBottom.Checked = True
                    End If
                Next

            Else
                'Modified as part of #20440
                'Dim sSQLPersons As String = Database & "..spGetPersonSalutation__c @PersonID=" & AptifyEbusinessUser1.PersonID
                'Dim dtPersonD As DataTable = DataAction.GetDataTable(sSQLPersons, IAptifyDataAction.DSLCacheSetting.BypassCache)
                'If Not dtPersonD Is Nothing AndAlso dtPersonD.Rows.Count > 0 Then
                '    lblfname.Text = Convert.ToString(dtPersonD.Rows(0)("firstname"))
                '    lbllname.Text = Convert.ToString(dtPersonD.Rows(0)("Lastname"))
                '    TxtEmail.Text = Convert.ToString(dtPersonD.Rows(0)("Email1"))
                '    lblsalutation.Text = Convert.ToString(dtPersonD.Rows(0)("Salutation"))
                '    txtPhoneAreaCode.Text = Convert.ToString(dtPersonD.Rows(0)("AreaCode"))
                '    txtPhone.Text = Convert.ToString(dtPersonD.Rows(0)("Phone"))
                'End If
                'btnPrint.Visible = False
                'btnPrint.Visible = False
                Panel1.Visible = False
                lblStatus.Visible = False
                Label2.Attributes.Add("class", "info-error")
                Label2.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoAccess")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'End of #20440 changes
            End If


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList,
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
    ''' <summary>
    ''' ' Pull the Abatement Status Reason
    ''' </summary>
    ''' <param name="AbatementID"></param>
    ''' <remarks></remarks>
    Protected Sub GetAbatementStatusReason(ByVal AbatementID As Integer)
        Try
            Dim sSql As String = Database & "..spGetAllAbatementStatusReason__c @AbatementID=" & AbatementID
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                trAbatementStatusReason.Visible = True
                grdAbatmentStatusReason.DataSource = dt
                grdAbatmentStatusReason.DataBind()
                grdAbatmentStatusReason.AllowSorting = False
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub EnableFalseAllFileds()
        Try

            ddlstatus.Enabled = False
            txtAnnualIncome.Enabled = False
            lblStatus.Enabled = False
            lblfname.Enabled = False
            lblsname.Enabled = False
            lbllname.Enabled = False
            TxtEmail.Enabled = False
            lblDate.Enabled = False
            txtPhoneAreaCode.Enabled = False
            txtPhone.Enabled = False
            btnsubmit.Enabled = False
            raUploadDocs.AllowAdd = False
            raUploadDocs.AllowDelete = False
            txtReason.Enabled = False
            Chkinfo.Enabled = False
            btnSave.Visible = False
            btnsubmit.Visible = False
            'redmine issue 
            ddlAbatementType.Enabled = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub
    ''' <summary>
    ''' ' Load Abatment data
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub LoadRecord()
        Try
            Dim abattypeId As Integer
            Dim sqlstr As String = String.Empty
            Dim dtabttypeID As System.Data.DataTable = New DataTable()
            'redmine 
            ''If Convert.ToInt32(ViewState("AbatementTypeID")) > 0 Then
            ''    sqlstr = Database & "..spGetAbetmentTypeID__c @PersonID=" & AptifyEbusinessUser1.PersonID.ToString() & ",@AbatmentTypeID=" & Convert.ToInt32(ViewState("AbatementTypeID"))
            ''Else
            ''    sqlstr = Database & "..spGetAbetmentTypeID__c @PersonID=" & AptifyEbusinessUser1.PersonID.ToString() & ",@AbatmentTypeID=0"

            ''End If
            '' dtabttypeID = Me.DataAction.GetDataTable(sqlstr, IAptifyDataAction.DSLCacheSetting.BypassCache)

            ''If dtabttypeID.Rows.Count > 0 Then
            ''    idAbatementType.Visible = True
            ''    abattypeId = Convert.ToInt32(dtabttypeID.Rows(0)("AbatementTypeID"))
            ''    Lblabatetypecheck.Text = Convert.ToString(dtabttypeID.Rows(0)("Name"))
            ''End If
            If ddlAbatementType.SelectedItem.Text <> "--Select--" Then
                abattypeId = ddlAbatementType.SelectedValue
                GetDataAsPerAbatementTypes(ViewState("CurrencyName"), ddlAbatementType.SelectedItem.Text)
            Else
                abattypeId = 0
            End If

            Dim sSQL As String = String.Empty
            Dim dtabetformdetails As System.Data.DataTable = New DataTable()
            Dim param(1) As IDataParameter
            Dim oda As New DataAction

            param(0) = oda.GetDataParameter("@AbatementTypeID", SqlDbType.Int, abattypeId)
            param(1) = oda.GetDataParameter("@Currecny", SqlDbType.VarChar, ViewState("CurrencyName"))
            sSQL = Database & "..spGetAbtmentdetails__c"
            dtabetformdetails = oda.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)

            If dtabetformdetails.Rows.Count > 0 Then
                lblABTName.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.Chartered Accountants Ireland Fee Reduction Declaration Euro-")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & CStr(ViewState("CurrencyName")) & " - " & Convert.ToString(dtabetformdetails.Rows(0)("Name"))
                lbldesc.Text = Convert.ToString(dtabetformdetails.Rows(0)("Description"))
                GetDataAsPerAbatementTypes(ViewState("CurrencyName"), Convert.ToString(dtabetformdetails.Rows(0)("Name")))
                'txtPhoneAreaCode.Text = Convert.ToString(dtabetformdetails.Rows(0)("AreaCode"))
                'txtPhone.Text = Convert.ToString(dtabetformdetails.Rows(0)("Phone"))
            End If

            Dim sSQLs As String = String.Empty
            Dim dtpersondetails As System.Data.DataTable = New DataTable()
            sSQLs = Database & "..SpSelectPersonDetails__c @PersonID=" & AptifyEbusinessUser1.PersonID.ToString()
            dtpersondetails = Me.DataAction.GetDataTable(sSQLs, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtpersondetails Is Nothing AndAlso dtpersondetails.Rows.Count > 0 Then
                lblfname.Text = Convert.ToString(dtpersondetails.Rows(0)("FirstName"))
                '+ "  " + (dtpersondetails.Rows(0)("MiddleName")) + " " + (dtpersondetails.Rows(0)("LastName"))
                lblsname.Text = Convert.ToString(dtpersondetails.Rows(0)("MiddleName"))
                lbllname.Text = Convert.ToString(dtpersondetails.Rows(0)("LastName"))
                lblsalutation.Text = Convert.ToString(dtpersondetails.Rows(0)("Salutation"))
                'Commented as part of #20288
                'txtPhoneAreaCode.Text = Convert.ToString(dtpersondetails.Rows(0)("AreaCode"))
                'txtPhone.Text = Convert.ToString(dtpersondetails.Rows(0)("Phone"))
            End If


            Dim SAbateAttribute As String = String.Empty
            Dim AttributeValue As String = String.Empty
            Dim dtSAbateAttribute As System.Data.DataTable = New DataTable()
            SAbateAttribute = Database & "..spGetAbatementAttribute__c "
            dtSAbateAttribute = Me.DataAction.GetDataTable(SAbateAttribute, IAptifyDataAction.DSLCacheSetting.BypassCache)
            AttributeValue = dtSAbateAttribute.Rows(0)("AttributeValue")
            lblAtrributevalue.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.Applications must be received by")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & dtSAbateAttribute.Rows(0)("AttributeValue") & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.in the year in which the subscription </br> applies  and no application will be considered in respect of previous years.")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)



            Dim SAbateYear As String = String.Empty

            Dim dtAbateYear As System.Data.DataTable = New DataTable()
            SAbateYear = Database & "..SpGetAbateYear__c  "
            dtAbateYear = Me.DataAction.GetDataTable(SAbateYear, IAptifyDataAction.DSLCacheSetting.BypassCache)
            'dtAbateYear = dtAbateYear.Rows(0)("abatementYear")
            'lblAtrributevalue.Text = "Applications must be received by " & dtSAbateAttribute.Rows(0)("AttributeValue") & " in the year in which the subscription </br> applies  and no application will be considered in respect of previous years."
            lblIwish.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.I wish to apply for a reduction in my")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & dtAbateYear.Rows(0)("abatementYear") & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.subscription on the following basis:")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblAbatementYear.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.Please Note:This subscription abatement applies to")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & dtAbateYear.Rows(0)("abatementYear") & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.only as the members circumstances may change from year to year")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblcriteria.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.Criteria For applications for a reduced subscription for")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & dtAbateYear.Rows(0)("abatementYear")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetDataAsPerAbatementTypes(ByVal CurrencyName As String, ByVal AbatementType As String)
        Try
            Dim sAbatementYear As String = AptifyApplication.GetEntityBaseDatabase("Persons") & "..SpGetAbateYear__c"
            Dim lAbatementYear As Long = Convert.ToInt32(DataAction.ExecuteScalar(sAbatementYear))
            If CurrencyName.Trim.ToLower() = "euro" And AbatementType.Trim.ToLower().Contains("unremunerated") Then
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.Euro-Unremunerated")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.UnremuneratedBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.UnremuneratedBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'RajeshK -071014
                chkAbatementBottom.Visible = True
                lblPartTimeEmp.Visible = False
                chkPartTimeEmp.Visible = False

            ElseIf Convert.ToString(ViewState("CurrencyName")) = AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c") And AbatementType.Trim.ToLower().Contains("unremunerated") Then
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.Pound-Unremunerated")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.UnremuneratedBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.UnremuneratedBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                chkAbatementBottom.Visible = True
            ElseIf AbatementType.Trim.ToLower() = "career break" Then
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                chkAbatementBottom.Visible = True
                lblPartTimeEmp.Visible = False
                chkPartTimeEmp.Visible = False

            ElseIf CurrencyName.Trim.ToLower() = "euro" And AbatementType.Trim.ToLower().Contains("early retirement") Then
                'lbldesc.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.EuroEarlyRetirementDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'updated LH 05-01-18
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AbatementEuroEarlyRetirementDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'RajeshK -071014
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Visible = True
                chkPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True
            ElseIf CurrencyName.Trim.ToLower() = AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c").Trim.ToLower() And AbatementType.Trim.ToLower().Contains("early retirement") Then
                'lbldesc.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.PoundEarlyRetirementDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) 
                'updated LH 05-01-18
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.PoundEarlyRetirementDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'RajeshK -071014
                lblPartTimeEmp.Visible = True
                chkPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True

            ElseIf CurrencyName.Trim.ToLower() = AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c").Trim.ToLower() And AbatementType.Trim.ToLower().Contains("full-time employment") Then
                ' lbldesc.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.PoundEarlyRetirementDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.PoundFullTimeEmploymentDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True
                chkPartTimeEmp.Visible = True
            ElseIf CurrencyName.Trim.ToLower() = "euro" And AbatementType.Trim.ToLower().Contains("full-time employment") Then
                ' lbldesc.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.PoundEarlyRetirementDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.EuroFullTimeEmploymentDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True
                chkPartTimeEmp.Visible = True
            ElseIf CurrencyName.Trim.ToLower() = "euro" And AbatementType.Trim.ToLower().Contains("full-time study") Then
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'RajeshK -071014
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Visible = True
                chkPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True
            ElseIf CurrencyName.Trim.ToLower() = AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c").Trim.ToLower() And AbatementType.Trim.ToLower().Contains("full-time study") Then
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Visible = True
                chkPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True
            ElseIf CurrencyName.Trim.ToLower() = "euro" And AbatementType.Trim.ToLower() = "retired income based" Then
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AbatementEuroRetiredDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Visible = True
                chkPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True
                'RajeshK -071014
            ElseIf CurrencyName.Trim.ToLower() = AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c").Trim.ToLower() And AbatementType.Trim.ToLower().Contains("retired income based") Then
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AbatementPoundRetiredDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Visible = True
                chkPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True
            ElseIf CurrencyName.Trim.ToLower() = "euro" And AbatementType.Trim.ToLower().Contains("maternity leave") Then
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.EuroMaternityLeaveDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "" & lAbatementYear
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblTick.Visible = False
                Chkinfo.Visible = False
                chkAbatementBottom.Visible = True
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                chkPartTimeEmp.Visible = True
                MaternityDes.Visible = True
                lblMoreDiscription.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.EuroMaternityLeaveMoreDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'RajeshK -071014
            ElseIf CurrencyName.Trim.ToLower() = AptifyApplication.GetEntityAttribute("Currency Types", "PoundCurrencyTypeName__c").Trim.ToLower() And AbatementType.Trim.ToLower().Contains("maternity leave") Then
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.PoundMaternityLeaveDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblTick.Visible = False
                Chkinfo.Visible = False
                chkAbatementBottom.Visible = True
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                chkPartTimeEmp.Visible = True
                MaternityDes.Visible = True
                lblMoreDiscription.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.EuroMaternityLeaveMoreDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            ElseIf AbatementType.Trim.ToLower().Contains("part-time employment") Then
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                chkPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True

            ElseIf AbatementType.Trim.ToLower().Contains("self employed") Then
                lblDescriptionText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.EuroSelfemployedDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & "" & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.EuroTravellingMoreDescription")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Visible = True
                chkPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True
                'lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'ElseIf AbatementType.Trim.ToLower().Contains("general") Then
                '    lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '    lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '    lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '    lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '    chkPartTimeEmp.Visible = True
            ElseIf AbatementType.Trim.ToLower().Contains("voluntary – pastoral work") Then
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart1")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lAbatementYear & " " & Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.NoteCommonPart2")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.FullTimeEmploymentNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblPartTimeEmp.Visible = True
                chkPartTimeEmp.Visible = True
                chkAbatementBottom.Visible = True
                'ElseIf CurrencyName.Trim.ToLower().Contains("us dollar") Then
                '    lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '    lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            Else
                lblBottomText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblBottomLastText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.CareerBreakBottomLastText")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
            '
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsubmit.Click
        Try
            If DoSave() Then
                btnPrint.Visible = True
                'btnsubmit.Enabled = False
                ' AddTopicCodeRecord()

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub SaveAttachment(ByVal RecordID As Long)
        Try
            raUploadDocs.AllowAdd = True
            raUploadDocs.AllowDelete = True
            raUploadDocs.AllowView = True
            raUploadDocs.EntityID = AptifyApplication.GetEntityID("Abatements__c")
            raUploadDocs.CategoryID = -1 'Updated by Govind M for #18933'AptifyApplication.GetEntityRecordIDFromRecordName(AptifyApplication.GetEntityID("Attachment Categories"), "Abatements Document")
            raUploadDocs.RecordID = RecordID
            Dim sSql As String = Database & "..spGetAttachmentfromEntity__c @EntityID=" & Convert.ToInt32(AptifyApplication.GetEntityID("Abatements__c")) & ",@RecordID=" & RecordID
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim oAttachmentGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Attachments", CInt(dr("ID")))
                    oAttachmentGE.Delete()
                Next
            End If
            raUploadDocs.SaveAttachment()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Save and update the record on Abatment entity
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function DoSave() As Boolean
        Dim oGE As AptifyGenericEntityBase
        Dim bRedirect As Boolean = False
        lblMsg.Text = ""
        'new label added by GM for Redmine #20181
        lblMsg1.Text = ""
        'End Redmine #20181
        Try
            If Convert.ToInt32(ViewState("AbatmentID")) > 0 Then
                oGE = AptifyApplication.GetEntityObject("Abatements__c", Convert.ToInt32(ViewState("AbatmentID")))
            Else
                oGE = AptifyApplication.GetEntityObject("Abatements__c", -1)
            End If
            With oGE
                .SetValue("PersonID", AptifyEbusinessUser1.PersonID)
                .SetValue("AbatementReasons", txtReason.Text.Trim)
                If Convert.ToString(ddlstatus.SelectedValue) <> "--Select--" Then
                    .SetValue("LevelOfIncome", ddlstatus.SelectedItem.Text)
                End If
                .SetValue("AnnualIncome", txtAnnualIncome.Text)
                .SetValue("AbatementStatusID", 1)
                .SetValue("MemberSignatureDate", Now.ToShortDateString)
                .SetValue("EmailAddress", TxtEmail.Text.Trim)
                .SetValue("AreaCode", txtPhoneAreaCode.Text)
                .SetValue("PhoneNumber", txtPhone.Text)
                .SetValue("CurrencyTypeID", Convert.ToInt32(ViewState("CurrencyTypeID")))
                If MembershipAppID > 0 Then
                    .SetValue("LinkedRecordID", MembershipAppID)
                    .SetValue("LinkedEntityID", Convert.ToInt32(AptifyApplication.GetEntityID("MembershipApplication__c")))
                End If

                .SetValue("AbatementStatusID", AptifyApplication.GetEntityRecordIDFromRecordName("AbatementStatus__c", "In Progress"))
                If Chkinfo.Checked = True Then
                    .SetValue("UseAnyInformation", 1)
                Else
                    .SetValue("UseAnyInformation", 0)
                End If

                If chkPartTimeEmp.Checked = True Then
                    .SetValue("Isnotbereimbursedbycompany", 1)
                Else
                    .SetValue("Isnotbereimbursedbycompany", 0)
                End If
                If chkAbatementBottom.Checked = True Then
                    .SetValue("ISunremunerated", 1)
                Else
                    .SetValue("ISunremunerated", 0)
                End If
                ' redmine 
                If ddlAbatementType.SelectedItem.Text <> "--Select--" Then
                    .SetValue("AbatementTypeID", ddlAbatementType.SelectedValue)
                Else
                    .SetValue("AbatementTypeID", -1)
                End If
            End With
            If oGE.Save Then

                bRedirect = True
                lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.SucessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'new label added by GM for Redmine #20181
                lblMsg1.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.SucessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'End Redmine #20181
                'trRecordAttachment.Visible = True
                ' Dim EntityId As Long
                ' EntityId = CLng(Me.AptifyApplication.GetEntityID("Abatements__c"))
                'Me.RecordAttachments__c.LoadAttachments(EntityId, oGE.RecordID)
                'If condition added by GM for Redmine #20181
                If Not Session("Submitted") Is Nothing AndAlso Session("Submitted") = "yes" Then
                Else
                    SaveAttachment(oGE.RecordID)
                End If
                'End Redmine #20181
                ViewState("AbatmentID") = Convert.ToString(oGE.RecordID)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return bRedirect
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Session("Submitting") = "yes" 'Added as part of #20308
            If DoSave() Then
                Dim oGE As AptifyGenericEntityBase
                Dim bRedirect As Boolean = False
                lblMsg.Text = ""
                'new label added by GM for Redmine #20181
                lblMsg1.Text = ""
                'End Redmine #20181
                If Convert.ToInt32(ViewState("AbatmentID")) > 0 Then
                    oGE = AptifyApplication.GetEntityObject("Abatements__c", Convert.ToInt32(ViewState("AbatmentID")))
                    oGE.SetValue("AbatementStatusID", AptifyApplication.GetEntityRecordIDFromRecordName("AbatementStatus__c", "Submitted"))
                    Session("Submitted") = "yes" 'Code added by GM for Redmine #20181
                    If oGE.Save() Then
                        btnSave.Visible = False
                        'Added as part of log #20350
                        raUploadDocs.AllowAdd = False
                        raUploadDocs.AllowDelete = False
                        raUploadDocs.AllowView = False
                        Dim EntityId As Long
                        EntityId = CLng(Me.AptifyApplication.GetEntityID("Abatements__c"))
                        Dim AttachmentCategoryID As Integer = AptifyApplication.GetEntityRecordIDFromRecordName(AptifyApplication.GetEntityID("Attachment Categories"), "Abatements Document")
                        Me.raUploadDocs.LoadAttachments(EntityId, oGE.RecordID, AttachmentCategoryID)
                        'End of #20350
                        btnsubmit.Visible = False
                        'SaveAttachment(oGE.RecordID)
                        lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Abatement.SucessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alert", "ShowPopup();", True)
                        Me.lblMessage.Text = "Your application form has been submitted. The Registry and Subscriptions Department will be in touch after they have processed your application."
                        'Response.Redirect(Request.RawUrl, False)
                        btnPrint.Visible = True
                    End If
                End If
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    'Susan Wong, #18954 Improve UI
    'Protected Sub lnkProfile_Click(sender As Object, e As EventArgs) Handles lnkProfile.Click
    'Response.Redirect(ProfilePage, False)
    'End Sub
    Protected Sub ddlAbatementType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAbatementType.SelectedIndexChanged
        Try
            LoadRecord()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class
