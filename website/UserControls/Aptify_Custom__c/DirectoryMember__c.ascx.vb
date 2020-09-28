﻿'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande               4 Sept 2015                     Update and create add new person as per selected compay
'Sheela Jarali              20/06/2018                      Support #18494: Queries on Make a mentor page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.AttributeManagement
Imports System.Data
Imports Aptify.Framework.Web.eBusiness
Imports System.ComponentModel
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.ProcessPipeline
Imports System.IO
Imports System.Web.Services


Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class DirectoryMember__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_ADMIN_EDIT_PROFILE As String = "AdminEditprofileUrl"
        'Neha Issue 14810,03/09/13, Declared properties for RadBinaryimage
        Protected Const ATTRIBUTE_PERSON_BLANK_IMG As String = "RadBlankImage"
        Protected Const ATTRIBUTE_PROFILE_THUMBNAIL_WIDTH As String = "ProfileThumbNailWidth"
        Protected Const ATTRIBUTE_PROFILE_THUMBNAIL_HEIGHT As String = "ProfileThumbNailHeight"
        Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage" 'Added by Sandeep for Issue 15051 on 12/03/2013
        Protected Const ATTRIBUTE_ADMIN_AddNewPeople As String = "AddNewMember"
#Region "Group Admin Specific Edit Profile"
        ''' <summary>
        ''' Meeting page url
        ''' </summary>

        Public Overridable Property AdminEditProfile() As String
            Get
                If Not ViewState(ATTRIBUTE_ADMIN_EDIT_PROFILE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ADMIN_EDIT_PROFILE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ADMIN_EDIT_PROFILE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Overridable Property AdminAddNewPeople() As String
            Get
                If Not ViewState(ATTRIBUTE_ADMIN_AddNewPeople) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ADMIN_AddNewPeople))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ADMIN_AddNewPeople) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        'Neha, Issue 14810, 03/09/13, Overrided properties for Rdabinaryimage
        ''' <summary>
        ''' ProfileThumbNailWidth
        ''' </summary>
        Public Overridable ReadOnly Property ProfileThumbNailWidth() As Integer
            Get
                Try
                    If Not String.IsNullOrEmpty(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_WIDTH)) Then
                        If Not IsNumeric(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_WIDTH)) Then
                            Throw New Exception("Incorrect entry for <Global>...<" & ATTRIBUTE_PROFILE_THUMBNAIL_WIDTH & ">, a numeric value is required. " & _
                                                "Please confirm the entry is correctly input in the 'Aptify_UC_Navigation.config' file.")
                        Else
                            Return CInt(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_WIDTH))
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
        ''' ProfileThumbNailHeight
        ''' </summary>
        Public Overridable ReadOnly Property ProfileThumbNailHeight() As Integer
            Get
                Try
                    If Not String.IsNullOrEmpty(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_HEIGHT)) Then
                        If Not IsNumeric(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_HEIGHT)) Then
                            Throw New Exception("Incorrect entry for <Global>...<" & ATTRIBUTE_PROFILE_THUMBNAIL_HEIGHT & ">, a numeric value is required. " & _
                                                "Please confirm the entry is correctly input in the 'Aptify_UC_Navigation.config' file.")
                        Else
                            Return CInt(Me.GetGlobalAttributeValue(ATTRIBUTE_PROFILE_THUMBNAIL_HEIGHT))
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
        ''' BlankImage
        ''' </summary>
        Public Overridable Property RadBlankImage() As String
            Get
                If Not ViewState(ATTRIBUTE_PERSON_BLANK_IMG) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_PERSON_BLANK_IMG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_PERSON_BLANK_IMG) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        'Added by Sandeep for Issue 15051 on 12/03/2013
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
#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(AdminEditProfile) Then
                'since value is the 'default' check the XML file for possible custom setting
                AdminEditProfile = Me.GetLinkValueFromXML(ATTRIBUTE_ADMIN_EDIT_PROFILE)
                If String.IsNullOrEmpty(AdminEditProfile) Then
                    Me.grdmember.Enabled = False
                    Me.grdmember.ToolTip = "Admin Edit Profile property has not been set."
                End If
            End If
            If String.IsNullOrEmpty(AdminAddNewPeople) Then
                'since value is the 'default' check the XML file for possible custom setting
                AdminAddNewPeople = Me.GetLinkValueFromXML(ATTRIBUTE_ADMIN_AddNewPeople)
            End If
            If String.IsNullOrEmpty(RadBlankImage) Then
                RadBlankImage = Me.GetLinkValueFromXML(ATTRIBUTE_PERSON_BLANK_IMG)
            End If
            'Added by Sandeep for Issue 15051 on 12/03/2013
            If String.IsNullOrEmpty(LoginPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_LOGIN_PAGE)
            End If

            'call base method to set parent properties
            MyBase.SetProperties()
        End Sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            SetProperties()
            If Not IsPostBack Then
                LoadSubsidiariesCompanyList()
                LoadMember()
                'Added by Kavita ZInage #18296
                Me.grdmember.DataSource = LoadMember()
                Me.grdmember.DataBind()
                'Till Here
                AddExpression()
            End If
            If User1.UserID < 0 Then
                Response.Redirect(LoginPage) 'Added by Sandeep for Issue 15051 on 12/03/2013
            End If
        End Sub
        'Commented by Kavita ZInage for bind issue #18296
        'Private Sub LoadMember()
        '    Dim dt As DataTable, sSQL As String
        '    Try
        '        ' Govind Mande change inline query to store procedure
        '        sSQL = Database & "..spGetCompanyDirectoryAsPerCompany__c @CompanyID=" & cmbSubsidarisCompney.SelectedValue
        '        dt = Me.DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        '        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
        '            Dim dcolUrl As DataColumn = New DataColumn()

        '            Dim dcolstatus As DataColumn = New DataColumn()

        '            dcolUrl.Caption = "AdminEditprofileUrl"
        '            dcolUrl.ColumnName = "AdminEditprofileUrl"

        '            dcolstatus.Caption = "Status"
        '            dcolstatus.ColumnName = "Status"

        '            dt.Columns.Add(dcolUrl)
        '            dt.Columns.Add(dcolstatus)

        '            'Amruta Issue 14878, 03/11/2013, Added condition when redirecting from group admin dashboard to display member details
        '            Dim sMembershipStatus As String = Request.QueryString("MembershipStatus")
        '            'Anil B for issue 15387 on 05-04-2013
        '            'Encrypt the person ID
        '            Dim sTemUrl As String = ""
        '            Dim index As Integer
        '            Dim sValue As String = ""
        '            Dim sSeparator As String()
        '            Dim sNavigate As String = ""
        '            If dt.Rows.Count > 0 AndAlso sMembershipStatus IsNot Nothing Then
        '                For Each rw As DataRow In dt.Rows
        '                    'Anil B for issue 15387 on 05-04-2013
        '                    'Encrypt the person ID
        '                    sTemUrl = AdminEditProfile + "?ID=" + rw("ID").ToString()
        '                    index = sTemUrl.IndexOf("=")
        '                    sValue = sTemUrl.Substring(index + 1)
        '                    sSeparator = sTemUrl.Split(CChar("="))
        '                    sNavigate = sSeparator(0)
        '                    sNavigate = sNavigate & "="
        '                    sNavigate = sNavigate & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
        '                    rw("AdminEditprofileUrl") = sNavigate
        '                Next


        '                Dim iMonth As Integer = Request.QueryString("Month")
        '                Dim dtMembersDetails As New DataTable
        '                dtMembersDetails = dt.Clone()
        '                grdmember.MasterTableView.GetColumn("ID").Visible = True
        '                grdmember.MasterTableView.GetColumn("MemberName").Visible = True
        '                grdmember.MasterTableView.GetColumn("DirectoryMemberName").Visible = False
        '                grdmember.MasterTableView.GetColumn("MemberShipType").Visible = True
        '                grdmember.MasterTableView.GetColumn("StartDate").Visible = False
        '                grdmember.MasterTableView.GetColumn("EndDate").Visible = True
        '                grdmember.MasterTableView.GetColumn("Remove").Visible = False
        '                btnRmvCompLink.Visible = False
        '                If sMembershipStatus = "GoingToExpire" Then
        '                    For Each rw As DataRow In dt.Rows
        '                        If rw("DuesPaidThru").ToString() <> "N/A" Then
        '                            If Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now() AndAlso rw("DuesPaidThru") < Date.Now().AddDays(iMonth) AndAlso rw("DuesPaidThru") <> Convert.ToDateTime(rw("JoinDate").ToString()).AddDays(-1) Then
        '                                rw("Status") = "Expiring"
        '                                dtMembersDetails.ImportRow(rw)
        '                            End If
        '                        End If
        '                    Next
        '                    Me.grdmember.DataSource = dtMembersDetails
        '                ElseIf sMembershipStatus = "RemainsActive" Then
        '                    For Each rw As DataRow In dt.Rows
        '                        If rw("DuesPaidThru").ToString() <> "N/A" Then
        '                            If Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now().AddDays(iMonth) AndAlso rw("DuesPaidThru") <> Convert.ToDateTime(rw("JoinDate").ToString()).AddDays(-1) Then
        '                                rw("Status") = "Active"
        '                                dtMembersDetails.ImportRow(rw)
        '                            End If
        '                        End If
        '                    Next
        '                    Me.grdmember.DataSource = dtMembersDetails
        '                End If
        '            Else
        '                If dt.Rows.Count > 0 Then
        '                    For Each rw As DataRow In dt.Rows
        '                        'Anil B for issue 15387 on 05-04-2013
        '                        'Encrypt the person ID
        '                        sTemUrl = AdminEditProfile + "?ID=" + rw("ID").ToString()
        '                        index = sTemUrl.IndexOf("=")
        '                        sValue = sTemUrl.Substring(index + 1)
        '                        sSeparator = sTemUrl.Split(CChar("="))
        '                        sNavigate = sSeparator(0)
        '                        sNavigate = sNavigate & "="
        '                        sNavigate = sNavigate & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
        '                        rw("AdminEditprofileUrl") = sNavigate
        '                    Next
        '                    'Code commented by Suvarna for IssueId-14303 - show button when there is data in grid
        '                    btnRmvCompLink.Visible = False
        '                Else
        '                    'Code commented by Suvarna for IssueId-14303 - Do not show button when there is no data in grid
        '                    btnRmvCompLink.Visible = False
        '                End If



        '                If dt.Rows.Count > 0 Then
        '                    'Code commented by Suvarna for IssueId-14303 - Do not show button when there is Single record of logged in person
        '                    If dt.Rows.Count = 1 Then
        '                        If (dt.Rows(dt.Rows.Count - 1)("ID")) = Convert.ToString(User1.PersonID) Then
        '                            btnRmvCompLink.Visible = False
        '                        End If
        '                    End If
        '                    For Each rw As DataRow In dt.Rows


        '                        If rw("DuesPaidThru").ToString() = "N/A" Then
        '                            rw("Status") = "Unavailable"
        '                        ElseIf rw("JoinDate").ToString() <> "N/A" Then
        '                            If Convert.ToDateTime(rw("DuesPaidThru").ToString()) = Convert.ToDateTime(rw("JoinDate").ToString()).AddDays(-1) Then
        '                                rw("DuesPaidThru") = rw("JoinDate")
        '                                rw("Status") = "Expired"
        '                            ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now().AddDays(90) Then
        '                                rw("Status") = "Active"
        '                            ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now() AndAlso rw("DuesPaidThru") < Date.Now().AddDays(90) Then
        '                                rw("Status") = "Expiring"
        '                            ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) < Date.Now() Then
        '                                rw("Status") = "Expired"
        '                            End If
        '                        ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now().AddDays(90) Then
        '                            rw("Status") = "Active"
        '                        ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now() AndAlso rw("DuesPaidThru") < Date.Now().AddDays(90) Then
        '                            rw("Status") = "Expiring"
        '                        ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) < Date.Now() Then
        '                            rw("Status") = "Expired"
        '                        End If
        '                    Next
        '                End If

        '                'Code commented by Suvarna for IssueId-14303 - Display message when there is no data in grid
        '                'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
        '                Me.grdmember.DataSource = dt
        '                Me.grdmember.DataBind()

        '                'grdmember.Visible = True
        '                'Else
        '                'grdmember.Visible = False
        '                'End If
        '            End If
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub


        'Added by Kavita ZInage #18296
        Private Function LoadMember() As DataTable
            Dim dt As DataTable
            'BEGIN:loboJ updated for #19626
            Dim sSQL As String = ""
            'END:loboJ updated for #19626
            Try
                'Govind Mande change inline query to store procedure
                'BEGIN:loboJ updated for #19626
                If (Session("search") And ((ts.Text).Length > 0)) Then
                    sSQL = Database & "..spGetCompanyDirectoryAsPerCompanyByCompanyIdFirstLastName__cai @CompanyID=" & cmbSubsidarisCompney.SelectedValue & ",@sname='" & ts.Text.Trim() & "'"
                Else
                    sSQL = Database & "..spGetCompanyDirectoryAsPerCompany__c @CompanyID=" & cmbSubsidarisCompney.SelectedValue
                End If
                'END:loboJ updated for #19626
                dt = Me.DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim dcolUrl As DataColumn = New DataColumn()

                    Dim dcolstatus As DataColumn = New DataColumn()

                    dcolUrl.Caption = "AdminEditprofileUrl"
                    dcolUrl.ColumnName = "AdminEditprofileUrl"

                    dcolstatus.Caption = "Status"
                    dcolstatus.ColumnName = "Status"

                    dt.Columns.Add(dcolUrl)
                    dt.Columns.Add(dcolstatus)

                    'Amruta Issue 14878, 03/11/2013, Added condition when redirecting from group admin dashboard to display member details
                    Dim sMembershipStatus As String = Request.QueryString("MembershipStatus")
                    'Anil B for issue 15387 on 05-04-2013
                    'Encrypt the person ID
                    Dim sTemUrl As String = ""
                    Dim index As Integer
                    Dim sValue As String = ""
                    Dim sSeparator As String()
                    Dim sNavigate As String = ""
                    If dt.Rows.Count > 0 AndAlso sMembershipStatus IsNot Nothing Then
                        For Each rw As DataRow In dt.Rows
                            'Anil B for issue 15387 on 05-04-2013
                            'Encrypt the person ID
                            sTemUrl = AdminEditProfile + "?ID=" + rw("ID").ToString()
                            index = sTemUrl.IndexOf("=")
                            sValue = sTemUrl.Substring(index + 1)
                            sSeparator = sTemUrl.Split(CChar("="))
                            sNavigate = sSeparator(0)
                            sNavigate = sNavigate & "="
                            sNavigate = sNavigate & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
                            rw("AdminEditprofileUrl") = sNavigate
                        Next


                        Dim iMonth As Integer = Request.QueryString("Month")
                        Dim dtMembersDetails As New DataTable
                        dtMembersDetails = dt.Clone()
                        grdmember.MasterTableView.GetColumn("ID").Visible = True
                        grdmember.MasterTableView.GetColumn("MemberName").Visible = True
                        grdmember.MasterTableView.GetColumn("DirectoryMemberName").Visible = False
                        grdmember.MasterTableView.GetColumn("MemberShipType").Visible = True
                        grdmember.MasterTableView.GetColumn("StartDate").Visible = False
                        grdmember.MasterTableView.GetColumn("EndDate").Visible = True
                        grdmember.MasterTableView.GetColumn("Remove").Visible = False
                        btnRmvCompLink.Visible = False
                        If sMembershipStatus = "GoingToExpire" Then
                            For Each rw As DataRow In dt.Rows
                                If rw("DuesPaidThru").ToString() <> "N/A" Then
                                    If Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now() AndAlso rw("DuesPaidThru") < Date.Now().AddDays(iMonth) AndAlso rw("DuesPaidThru") <> Convert.ToDateTime(rw("JoinDate").ToString()).AddDays(-1) Then
                                        rw("Status") = "Expiring"
                                        'dtMembersDetails.ImportRow(rw)
                                    End If
                                End If
                            Next
                            Me.grdmember.DataSource = dtMembersDetails
                        ElseIf sMembershipStatus = "RemainsActive" Then
                            For Each rw As DataRow In dt.Rows
                                If rw("DuesPaidThru").ToString() <> "N/A" Then
                                    If Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now().AddDays(iMonth) AndAlso rw("DuesPaidThru") <> Convert.ToDateTime(rw("JoinDate").ToString()).AddDays(-1) Then
                                        rw("Status") = "Active"
                                        dtMembersDetails.ImportRow(rw)
                                    End If
                                End If
                            Next
                            Me.grdmember.DataSource = dtMembersDetails
                        End If
                    Else
                        If dt.Rows.Count > 0 Then
                            For Each rw As DataRow In dt.Rows
                                'Anil B for issue 15387 on 05-04-2013
                                'Encrypt the person ID
                                sTemUrl = AdminEditProfile + "?ID=" + rw("ID").ToString()
                                index = sTemUrl.IndexOf("=")
                                sValue = sTemUrl.Substring(index + 1)
                                sSeparator = sTemUrl.Split(CChar("="))
                                sNavigate = sSeparator(0)
                                sNavigate = sNavigate & "="
                                sNavigate = sNavigate & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(sValue))
                                rw("AdminEditprofileUrl") = sNavigate
                            Next
                            'Code commented by Suvarna for IssueId-14303 - show button when there is data in grid
                            btnRmvCompLink.Visible = False
                        Else
                            'Code commented by Suvarna for IssueId-14303 - Do not show button when there is no data in grid
                            btnRmvCompLink.Visible = False
                        End If



                        If dt.Rows.Count > 0 Then
                            'Code commented by Suvarna for IssueId-14303 - Do not show button when there is Single record of logged in person
                            If dt.Rows.Count = 1 Then
                                If (dt.Rows(dt.Rows.Count - 1)("ID")) = Convert.ToString(User1.PersonID) Then
                                    btnRmvCompLink.Visible = False
                                End If
                            End If
                            For Each rw As DataRow In dt.Rows


                                If rw("DuesPaidThru").ToString() = "N/A" Then
                                    rw("Status") = "Unavailable"
                                ElseIf rw("JoinDate").ToString() <> "N/A" Then
                                    If Convert.ToDateTime(rw("DuesPaidThru").ToString()) = Convert.ToDateTime(rw("JoinDate").ToString()).AddDays(-1) Then
                                        rw("DuesPaidThru") = rw("JoinDate")
                                        rw("Status") = "Expired"
                                    ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now().AddDays(90) Then
                                        rw("Status") = "Active"
                                    ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now() AndAlso rw("DuesPaidThru") < Date.Now().AddDays(90) Then
                                        rw("Status") = "Expiring"
                                    ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) < Date.Now() Then
                                        rw("Status") = "Expired"
                                    End If
                                ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now().AddDays(90) Then
                                    rw("Status") = "Active"
                                ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) > Date.Now() AndAlso rw("DuesPaidThru") < Date.Now().AddDays(90) Then
                                    rw("Status") = "Expiring"
                                ElseIf Convert.ToDateTime(rw("DuesPaidThru").ToString()) < Date.Now() Then
                                    rw("Status") = "Expired"
                                End If
                            Next
                        End If

                        'Code commented by Suvarna for IssueId-14303 - Display message when there is no data in grid
                        'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        'Me.grdmember.Rebind()
                        'grdmember.Visible = True
                        'Else
                        'grdmember.Visible = False
                        'End If
                    End If
                End If
                Return dt
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        Protected Sub grdmember_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdmember.NeedDataSource
            'LoadMember() 'Commented by Kavita ZInage #18296
            Me.grdmember.DataSource = LoadMember() 'Added by Kavita ZInage #18296
        End Sub
        'Neha, Issue 14810, 03/09/13,used Radbinaryimage 
        Protected Sub grdmember_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdmember.ItemDataBound
            Try
                Dim imagememberid As RadBinaryImage = Nothing
                Dim imgestatusID As Telerik.Web.UI.RadBinaryImage
                Dim chkRemovefrmComp As CheckBox
                Dim groupDataRow As DataRowView = DirectCast(e.Item.DataItem, DataRowView)
                Dim personID As Long = -1

                'Maulik Patel, 03/13/2013. Added null check.
                If Not groupDataRow Is Nothing AndAlso Not groupDataRow("ID") Is Nothing Then
                    personID = Convert.ToInt32(groupDataRow("ID").ToString())
                End If
                imgestatusID = CType(e.Item.FindControl("imgstaus"), Telerik.Web.UI.RadBinaryImage)
                chkRemovefrmComp = CType(e.Item.FindControl("chkRmvCompLink"), CheckBox)

                If chkRemovefrmComp IsNot Nothing AndAlso personID = User1.PersonID Then
                    chkRemovefrmComp.Enabled = False
                End If

                If e.Item Is Nothing OrElse e.Item.FindControl("imgmember") Is Nothing Then
                    Exit Sub
                End If
                imagememberid = CType(e.Item.FindControl("imgmember"), RadBinaryImage)
                'set the location of blankimage to display in radbinaryimage control
                imagememberid.ImageUrl = Me.FixLinkForVirtualPath(RadBlankImage)
                imagememberid.DataBind()
                'Neha Changes for issue 16001, 05/07/13
                'Resizes the passed Image according to the specified width and height and returns the resized Image
                If Not IsDBNull(DataBinder.Eval(e.Item.DataItem, "Photo")) Then
                    Dim commonMethods As New Aptify.Framework.Web.eBusiness.CommonMethods()
                    Dim profileImage As Drawing.Image = Nothing
                    Dim width As Integer = ProfileThumbNailWidth
                    Dim height As Integer = ProfileThumbNailHeight
                    Dim aspratioWidth As Integer

                    Dim profileImageByte As Byte() = DirectCast(DataBinder.Eval(e.Item.DataItem, "Photo"), Byte())
                    If profileImageByte IsNot Nothing AndAlso profileImageByte.Length > 0 Then
                        commonMethods.getResizedImageHeightandWidth(profileImage, profileImageByte, ProfileThumbNailWidth, ProfileThumbNailHeight, aspratioWidth)
                        profileImage = commonMethods.byteArrayToImage(profileImageByte)
                        profileImageByte = commonMethods.resizeImageAndGetAsByte(profileImage, aspratioWidth, height)
                        imagememberid.DataValue = profileImageByte
                        imagememberid.DataBind()
                    Else
                        imagememberid.ImageUrl = Me.FixLinkForVirtualPath(RadBlankImage)
                        imagememberid.DataBind()
                    End If
                End If
                ' Dim result As Boolean = commonMethods.Test(originalImage.Height, originalImage.Width, ProfileThumbNailHeight, ProfileThumbNailWidth, DataBinder.Eval(e.Item.DataItem, "Photo"))
                'End If
                If Not IsDBNull(DataBinder.Eval(e.Item.DataItem, "Status")) AndAlso Not (DataBinder.Eval(e.Item.DataItem, "Status")) Is Nothing Then

                    If DataBinder.Eval(e.Item.DataItem, "Status").ToString().Trim = "Active" Then
                        imgestatusID.Visible = True
                        imgestatusID.ImageUrl = "~/Images/active.png"
                    ElseIf DataBinder.Eval(e.Item.DataItem, "Status").ToString().Trim = "Expired" Then
                        imgestatusID.Visible = True
                        imgestatusID.ImageUrl = "~/Images/expire.png"
                    ElseIf DataBinder.Eval(e.Item.DataItem, "Status").ToString().Trim = "Expiring" Then
                        imgestatusID.Visible = True
                        imgestatusID.ImageUrl = "~/Images/Expiring-soon.png"

                    Else
                        imgestatusID.Visible = False
                    End If
                End If

                'Added by Kavita Zinage 07/09/2017 - #18296
                Dim chkIsMentor As CheckBox
                chkIsMentor = CType(e.Item.FindControl("chkIsMentor"), CheckBox)
                If Not IsDBNull(DataBinder.Eval(e.Item.DataItem, "MemberType")) AndAlso Not (DataBinder.Eval(e.Item.DataItem, "MemberType")) Is Nothing Then
                    If String.Compare(DataBinder.Eval(e.Item.DataItem, "MemberType"), "Student", True) = 0 Then
                        chkIsMentor.Enabled = False
                    Else
                        If Not IsDBNull(DataBinder.Eval(e.Item.DataItem, "IsMentor__c")) AndAlso Not (DataBinder.Eval(e.Item.DataItem, "IsMentor__c")) Is Nothing Then
                            If DataBinder.Eval(e.Item.DataItem, "IsMentor__c") Then
                                chkIsMentor.Enabled = False
                            Else
                                chkIsMentor.Enabled = True
                            End If
                        End If
                    End If
                End If
                'Till Here

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try


        End Sub

        Protected Sub btnRmvCompLink_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRmvCompLink.Click
            Dim sAlert As String = String.Empty
            SaveCheckedValues()
            If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                Dim userdetails As ArrayList = DirectCast(ViewState("CHECKED_ITEMS"), ArrayList)
                If userdetails.Count = 1 Then
                    sAlert = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.DirectoryMember.Remove1PersonMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                ElseIf userdetails.Count > 1 Then
                    sAlert = "Are you sure you want to remove the " & userdetails.Count & " persons you selected from the company?"
                ElseIf userdetails.Count = 0 Then
                    sAlert = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.DirectoryMember.RemoveNoPersonSelectedMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblRCValidation.Text = sAlert
                    radRCValidation.VisibleOnPageLoad = True
                    Exit Sub
                End If
            Else
                sAlert = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.DirectoryMember.RemoveNoPersonSelectedMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblRCValidation.Text = sAlert
                radRCValidation.VisibleOnPageLoad = True
                Exit Sub
            End If
            lblConfirm.Text = sAlert
            radConfirm.VisibleOnPageLoad = True
        End Sub

        Private Sub GetPersonIDs()
            Dim lPersonID As String = String.Empty
            Dim sSQL As String = String.Empty
            Dim iCnt As Int32

            If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                Dim userdetails As ArrayList = DirectCast(ViewState("CHECKED_ITEMS"), ArrayList)
                For i As Int32 = 0 To userdetails.Count - 1
                    lPersonID = userdetails(i).ToString
                    'Govind Mande change inline query to store procedure
                    sSQL = Database & "..spGetPersonSubscriptionCount__c @PersonID=" & lPersonID & ",@CompanyID=" & User1.CompanyID
                    iCnt = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                    RemoveCompanyLinkage(iCnt, lPersonID)
                Next
            End If

        End Sub

        Protected Function RemoveCompanyLinkage(ByVal iCnt As Int32, ByVal lpersonID As String) As Boolean
            Dim sSQL As String = String.Empty
            Dim dt As DataTable = Nothing
            ' Govind Mande change inline query to store procedure
            sSQL = Database & "..spGetPersonSubscriptionID__c @PersonID=" & lpersonID & ",@CompanyID=" & User1.CompanyID
            Dim oSubscription As AptifyGenericEntityBase, oPerson As AptifyGenericEntityBase
            oPerson = AptifyApplication.GetEntityObject("Persons", CLng(lpersonID))

            If iCnt >= 1 Then
                dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        oSubscription = AptifyApplication.GetEntityObject("Subscriptions", CLng(dt.Rows(i)("ID").ToString))
                        If oSubscription IsNot Nothing Then
                            oSubscription.SetValue("AutoRenew", 0)
                            oSubscription.SetValue("SubscriberID", CInt(lpersonID))
                            oSubscription.SetValue("SubscriberCompanyID", -1)
                            oSubscription.SetValue("RecipientCompanyID", -1)
                            If oSubscription.Save() Then
                                If oPerson IsNot Nothing Then
                                    oPerson.SetValue("CompanyID", -1)
                                    '' Send mail only once when there are multiple records
                                    If i = 0 Then
                                        SendWebCredentialsMail(CLng(lpersonID))
                                    End If
                                    If oPerson.Save() Then
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            Else
                oPerson.SetValue("CompanyID", -1)
                SendWebCredentialsMail(CLng(lpersonID))
                If oPerson.Save() Then
                    Return True
                End If
            End If
        End Function

        Protected Sub SendWebCredentialsMail(ByVal lPersonID As Long)
            Try
                Dim lProcessFlowID As Long
                Dim sProcessFlow As String = "Send Email Notification for Company Link Removal"
                'Get the Process Flow ID to be used for sending the Downloadable Order Confirmation Email
                'Dim sSQL As String = "SELECT ID FROM " & Database & "..vwProcessFlows WHERE Name='" & sProcessFlow & "'"
                'Govind Mande change inline query to store procedure
                Dim sSql As String = Database & "..spGetProcessFlowIDFromName__c @Name='" & sProcessFlow & "'"
                Dim oProcessFlowID As Object = DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.UseCache)
                If oProcessFlowID IsNot Nothing AndAlso IsNumeric(oProcessFlowID) Then
                    lProcessFlowID = CLng(oProcessFlowID)
                    Dim context As New AptifyContext
                    context.Properties.AddProperty("PersonID", lPersonID)
                    Dim oResult As ProcessFlowResult
                    oResult = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, lProcessFlowID, context)
                    If Not oResult.IsSuccess Then
                        ExceptionManagement.ExceptionManager.Publish(New Exception("Process flow to send Notifocation. Please refer event handler for more details."))
                    End If
                Else
                    ExceptionManagement.ExceptionManager.Publish(New Exception("Message Template to send remove company Linkage Email is not found in the system."))
                End If

            Catch ex As ArgumentException
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Neha Changes for issue 14972, 04/29/13
        'This method is used to populate the saved checkbox values
        Private Sub PopulateCheckedValues()
            Dim userdetails As ArrayList = DirectCast(ViewState("CHECKED_ITEMS"), ArrayList)
            Dim lblOrderID As Label = New Label
            Dim index As Long = -1
            If userdetails IsNot Nothing AndAlso userdetails.Count > 0 Then
                For Each item As GridDataItem In grdmember.MasterTableView.Items
                    lblOrderID = CType(item.FindControl("ID"), Label)
                    index = lblOrderID.Text
                    If userdetails.Contains(index) Then
                        Dim myCheckBox As CheckBox = DirectCast(item.FindControl("chkRmvCompLink"), CheckBox)
                        myCheckBox.Checked = True
                    End If
                Next
            End If
        End Sub

        'Neha Changes for issue 14972, 04/29/13
        'This method is used to save the checkedstate of values
        Private Sub SaveCheckedValues()
            Dim userdetails As New ArrayList()
            Dim index As Long = -1
            Dim lblOrderID As Label = New Label
            For Each item As GridDataItem In grdmember.MasterTableView.Items
                lblOrderID = CType(item.FindControl("lblPersonID"), Label)
                index = lblOrderID.Text
                Dim result As Boolean = DirectCast(item.FindControl("chkRmvCompLink"), CheckBox).Checked
                'Check in the ViewState
                If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                    userdetails = DirectCast(ViewState("CHECKED_ITEMS"), ArrayList)
                End If
                If result Then
                    If Not userdetails.Contains(index) Then
                        userdetails.Add(index)
                    End If
                Else
                    userdetails.Remove(index)
                End If
            Next

            If userdetails IsNot Nothing AndAlso userdetails.Count > 0 Then
                ViewState("CHECKED_ITEMS") = userdetails
            End If
        End Sub

        Protected Sub grdmember_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdmember.PageIndexChanged
            SaveCheckedValues()
            'PopulateCheckedValues()
        End Sub

        Protected Sub grdmember_GridItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs)
            'SaveCheckedValues()
            'PopulateCheckedValues()
            Dim chkRmvCompLink As CheckBox = DirectCast(e.Item.FindControl("chkRmvCompLink"), CheckBox)
            Dim lblPersonID As Label = DirectCast(e.Item.FindControl("lblPersonID"), Label)
            If chkRmvCompLink IsNot Nothing Then
                'Check in the ViewState
                If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                    Dim userdetails As ArrayList = DirectCast(ViewState("CHECKED_ITEMS"), ArrayList)

                    Dim dataItem As DataRowView = DirectCast(e.Item.DataItem, System.Data.DataRowView)

                    If dataItem IsNot Nothing Then
                        Dim personID As Long = dataItem("ID")
                        If userdetails.Contains(personID) = True Then
                            chkRmvCompLink.Checked = True
                        Else
                            chkRmvCompLink.Checked = False
                        End If
                    End If
                End If
            End If
        End Sub

        Protected Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
            radConfirm.VisibleOnPageLoad = False
            If ViewState("CHECKED_ITEMS") IsNot Nothing Then
                'Get ID of People to remove company Linkage
                GetPersonIDs()
                ' grdmember_NeedDataSource(Nothing, Nothing)
                grdmember.Rebind()
                ViewState("CHECKED_ITEMS") = Nothing
            End If
        End Sub

        Protected Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
            radConfirm.VisibleOnPageLoad = False
            radRCValidation.VisibleOnPageLoad = False
        End Sub

        Private Sub AddExpression()
            Dim ExpOrderSort As New GridSortExpression
            ExpOrderSort.FieldName = "FirstLast"
            ExpOrderSort.SetSortOrder("Ascending")
            grdmember.MasterTableView.SortExpressions.AddSortExpression(ExpOrderSort)
        End Sub
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Below code added by Govind M and above stock code
#Region "Load Events"
        Private Sub LoadSubsidiariesCompanyList()
            Try
                ' Commented code by GM and add new SP for Redmine #19626
                'Dim sSql As String = Database & "..spGetAllSubsidiaries__c @ParentCompanyId=" & User1.CompanyID
                Dim sSql As String = Database & "..spGetAllSubsidiariesWithCity__c @ParentCompanyId=" & User1.CompanyID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    cmbSubsidarisCompney.ClearSelection()
                    cmbSubsidarisCompney.DataSource = dt
                    'cmbSubsidarisCompney.DataTextField = "Name" '  Commented code by GM for Redmine #19626
                    cmbSubsidarisCompney.DataTextField = "FirmCity" ' code added by GM for Redmine #19626
                    cmbSubsidarisCompney.DataValueField = "ID"
                    cmbSubsidarisCompney.DataBind()
                End If
                SetComboValue(cmbSubsidarisCompney, Convert.ToString(AptifyApplication.GetEntityRecordName("Companies", User1.CompanyID)))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
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
        Protected Sub cmbSubsidarisCompney_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbSubsidarisCompney.SelectedIndexChanged
            Try
                'Added by Sheela as part of log #18494 to bind data to grid on company selection
                'LoadMember()
                'BEGIN:loboJ updated for #19626
                Session("search") = False
                ts.Text = ""
                'END:loboJ updated for #19626
                Me.grdmember.DataSource = LoadMember()
                Me.grdmember.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
        ''' <summary>
        ''' code added by Govind Mande
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>

        Protected Sub lnkAddPerson_Click(sender As Object, e As System.EventArgs) Handles lnkAddPerson.Click
            Try
                Dim lCompanyID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(cmbSubsidarisCompney.SelectedValue)
                Response.Redirect(AdminAddNewPeople & "?cID=" & System.Web.HttpUtility.UrlEncode(lCompanyID), False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        ''' <summary>
        ''' Called when [check change event]. #18296
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Protected Sub OnCheckChangeEvent(ByVal sender As Object, ByVal e As EventArgs)
            Try
                For Each dataItem As GridDataItem In grdmember.MasterTableView.Items
                    Dim lblPersonID = CType(dataItem.FindControl("lblPersonID"), Label)
                    If CType(dataItem.FindControl("chkIsMentor"), CheckBox).Checked = True Then
                        If UpdatePersonMentorFlag(Convert.ToInt32(lblPersonID.Text), 1) Then
                            CType(dataItem.FindControl("chkIsMentor"), CheckBox).Enabled = False
                        End If
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub
        ''' <summary>
        ''' Updates the person mentor flag on person record. #18296
        ''' </summary>
        ''' <param name="PersonID">The person identifier.</param>
        ''' <returns></returns>
        Public Function UpdatePersonMentorFlag(ByVal PersonID As Integer, ByVal optionVal As Integer) As Boolean
            Try
                Dim oPersonGE As AptifyGenericEntityBase
                oPersonGE = Me.AptifyApplication.GetEntityObject("Persons", PersonID)
                With oPersonGE
                    .SetValue("IsMentor__c", optionVal)
                End With
                Dim sErrorMessage As String = String.Empty
                If oPersonGE.Save(False, sErrorMessage) Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
        'BEGIN:loboJ updated for #19626
        Protected Sub bsearch_Click(sender As Object, e As EventArgs) Handles bsearch.Click
            Session("search") = True
            Me.grdmember.DataSource = LoadMember()
            Me.grdmember.DataBind()
        End Sub
        'END:loboJ updated for #19626

    End Class
End Namespace
