'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Amol Bule                     04/09/2014                       Created to add a new network or to add members to previously selected network
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI

Partial Class AddManageNetwork__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "AddManageNetwork__c"
    Protected Const ATTRIBUTE_CONTORL_NETWORKMANAGMENT_NAME As String = "NetworkManagment"
    Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage"
#Region "Control Properties"
    ''' <summary>
    ''' Login Page page url
    ''' </summary>
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
    Public Overridable Property NetworkManagment() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTORL_NETWORKMANAGMENT_NAME) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTORL_NETWORKMANAGMENT_NAME))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTORL_NETWORKMANAGMENT_NAME) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable Property CommitteeID() As Long
        Get
            If Not ViewState("CommitteeID") Is Nothing Then
                Return CLng(ViewState("CommitteeID"))
            Else
                Return -1
            End If
        End Get
        Set(ByVal value As Long)
            ViewState("CommitteeID") = value
        End Set
    End Property

    Public Property CurrentTable() As DataTable
        Get
            If Not ViewState("tblCTMEDetails") Is Nothing Then
                Return CType(ViewState("tblCTMEDetails"), DataTable)
            Else
                Dim tblPBEDetails As New DataTable("tblCTMEDetails")
                With tblPBEDetails
                    .Columns.Add("ID", Type.GetType("System.String"))
                    .PrimaryKey = New DataColumn() {.Columns("ID")}
                    .Columns.Add("MemberID", Type.GetType("System.String"))
                    .Columns.Add("MemberID_Name", Type.GetType("System.String"))
                    .Columns.Add("RoleID", Type.GetType("System.String"))
                    .Columns.Add("StartDate", Type.GetType("System.String"))
                    .Columns.Add("EndDate", Type.GetType("System.String"))
                    .Columns.Add("Role", Type.GetType("System.String"))
                    .Columns.Add("CommitteeTermID", Type.GetType("System.String"))

                End With
                Return tblPBEDetails
            End If
        End Get
        Set(ByVal value As DataTable)
            ViewState("tblCTMEDetails") = value
        End Set
    End Property



    Protected Overrides Sub SetProperties()

        If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        'call base method to set parent properties
        MyBase.SetProperties()
        If String.IsNullOrEmpty(LoginPage) Then
            'since value is the 'default' check the XML file for possible custom setting
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_LOGIN_PAGE)
        End If
        If String.IsNullOrEmpty(NetworkManagment) Then
            'since value is the 'default' check the XML file for possible custom setting
            NetworkManagment = Me.GetLinkValueFromXML(ATTRIBUTE_CONTORL_NETWORKMANAGMENT_NAME)
        End If

    End Sub
#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            SetProperties()
            If User1.UserID > 0 Then
                If Not IsPostBack Then
                    ViewState("tblCTMEDetails") = Nothing
                    Dim datatable As DataTable = Nothing
                    CurrentTable = datatable
                    autoPerson.ContextKey = User1.CompanyID
                    If Request.QueryString("CommitteeID") <> "-1" Or Request.QueryString("CommitteeID") <> String.Empty Then
                        
                        CommitteeID = Request.QueryString("CommitteeID")
                        LoadNetworksInfoAndMembers(Request.QueryString("CommitteeID"))

                    End If

                End If
            Else
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#Region "Load Methods"
    Private Sub LoadNetworksInfoAndMembers(ByVal CommitteeID As Integer)
        Try
            Dim ds As DataSet = Nothing
            Dim sSql As String = Database & "..spGetCommitteeTermMembers__c"
            Dim param(1) As IDataParameter
            param(0) = DataAction.GetDataParameter("@CommitteeID ", SqlDbType.BigInt, CommitteeID)
            param(1) = DataAction.GetDataParameter("@PersonID ", SqlDbType.Int, User1.PersonID)
            ds = DataAction.GetDataSetParametrized(sSql, CommandType.StoredProcedure, param)

            If ds IsNot Nothing Then
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    txtNetworkName.Text = ds.Tables(0).Rows(0)("Name")
                    rdStartdate.SelectedDate = ds.Tables(0).Rows(0)("StartDate")
                    If Convert.ToString(ds.Tables(0).Rows(0)("EndDate")) <> "01/01/1900 00:00:00" And Convert.ToString(ds.Tables(0).Rows(0)("EndDate")) <> "" Then
                        rdEnddate.SelectedDate = ds.Tables(0).Rows(0)("EndDate")
                    End If
                    txtSummary.Text = ds.Tables(0).Rows(0)("Goals")
                    lblTermID.Text = ds.Tables(0).Rows(0)("TermID")
                End If

                If ds.Tables(1) IsNot Nothing AndAlso ds.Tables(1).Rows.Count > 0 Then
                    grdMain.DataSource = ds.Tables(1)
                    grdMain.DataBind()
                    grdMain.Visible = True
                    btnSubmitCT.Visible = True
                    btnbackNet.Visible = True
                Else
                    grdMain.Visible = False
                    btnSubmitCT.Visible = False
                    btnbackNet.Visible = False
                End If

            Else
                lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.NetworkManagement.NotFoundNetwork")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''This show the confirmation box for droping member from committee.
    'Protected Sub RemoveCommitteeFriends_Checked(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        radAlert.VisibleOnPageLoad = True
    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    'End Sub



#End Region
    ' Following function is use to set the status of member to drop, We are seetting static value 3 
    Protected Sub RemoveMeFromNetwork(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim BtnSeletedRow As Button = TryCast(sender, Button)
            Dim row As Telerik.Web.UI.GridTableRow = CType(BtnSeletedRow.NamingContainer, Telerik.Web.UI.GridTableRow)
            Dim CTID As Label = DirectCast(row.FindControl("lblCTID"), Label)
            lblComTID.Text = CTID.Text
            Dim CTMID As Label = DirectCast(row.FindControl("lblctmmID"), Label)
            lblCTMID.Text = CTMID.Text
            Dim CurrentTableID As Label = DirectCast(row.FindControl("lblCTMID"), Label)
            lblCurrentTableID.Text = CurrentTableID.Text

            radAlert.VisibleOnPageLoad = True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub



    Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        Try
            If CommitteeID > 0 Then


                Dim Ge As AptifyGenericEntityBase = Nothing
                Dim oCSGE As AptifyGenericEntityBase
                Ge = AptifyApplication.GetEntityObject("Committee Terms", lblComTID.Text)

                If Ge IsNot Nothing Then
                    oCSGE = Ge.SubTypes("CommitteeTermMembers").Find("ID", lblCTMID.Text)

                    If oCSGE IsNot Nothing Then
                        Dim TodaysDate As Date = New DateTime(Year(Today), Convert.ToInt32(DatePart(DateInterval.Month, Today)), Today.Day)
                        If Convert.ToDateTime(oCSGE.GetValue("StartDate")) > Convert.ToDateTime(TodaysDate) Then
                            oCSGE.SetValue("EndDate", oCSGE.GetValue("StartDate"))
                        Else
                            oCSGE.SetValue("EndDate", Date.Now.Date)
                        End If
                        '  oCSGE.SetValue("EndDate", Date.Now.Date)
                        Ge.Save()
                    End If
                End If
                radAlert.VisibleOnPageLoad = False
                LoadNetworksInfoAndMembers(CommitteeID)
            Else
                Dim sID As String = lblCurrentTableID.Text
                If CurrentTable IsNot Nothing AndAlso Not CurrentTable.Rows.Find(sID) Is Nothing Then
                    CurrentTable.Rows.Find(sID).Delete()
                End If
              
                grdMain.DataSource = CurrentTable.Select(" Role <> 'Chair'")
                grdMain.DataBind()
                grdMain.Visible = True
                btnSubmitCT.Visible = True
                btnbackNet.Visible = True
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Try
            radAlert.VisibleOnPageLoad = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnbackNet_Click(sender As Object, e As System.EventArgs) Handles btnbackNet.Click
        Try
            Response.Redirect(NetworkManagment)

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Try
            If hdnPersonId.Value <> "" Then
                If CommitteeID > 0 Then
                    Dim Ge As AptifyGenericEntityBase = Nothing
                    Dim oCSGE As AptifyGenericEntityBase
                    Ge = AptifyApplication.GetEntityObject("Committee Terms", lblTermID.Text)

                    If Ge IsNot Nothing Then
                        With Ge.SubTypes("CommitteeTermMembers").Add()
                            .SetValue("MemberID", CInt(hdnPersonId.Value))
                            .SetValue("RoleID", AptifyApplication.GetEntityRecordIDFromRecordName("Committee Roles", "Member"))
                            Dim TodaysDate As Date = New DateTime(Year(Today), Convert.ToInt32(DatePart(DateInterval.Month, Today)), Today.Day)
                            If Convert.ToDateTime(Ge.GetValue("StartDate")) > Convert.ToDateTime(TodaysDate) Then
                                .SetValue("StartDate", Ge.GetValue("StartDate"))
                            Else
                                .SetValue("StartDate", Date.Now.Date)
                            End If
                            '.SetValue("StartDate", Ge.GetValue("StartDate"))
                        End With
                        Ge.Save()
                    End If
                    LoadNetworksInfoAndMembers(CommitteeID)
                Else

                    Dim dt As DataTable = CurrentTable
                    If dt.Rows.Count < 1 Then
                        Dim dr1 As DataRow = dt.NewRow
                        With dr1
                            .Item("ID") = Guid.NewGuid.ToString()
                            .Item("MemberID_Name") = User1.FirstName + " " + User1.LastName
                            .Item("MemberID") = User1.PersonID
                            .Item("RoleID") = AptifyApplication.GetEntityRecordIDFromRecordName("Committee Roles", "Chair")
                            .Item("StartDate") = Convert.ToDateTime(rdStartdate.SelectedDate).ToShortDateString
                            If Convert.ToDateTime(rdEnddate.SelectedDate).ToShortDateString <> "01/01/0001" Then
                                .Item("EndDate") = Convert.ToDateTime(rdEnddate.SelectedDate).ToShortDateString
                            End If
                            .Item("CommitteeTermID") = "-1"
                            .Item("Role") = "Chair"
                        End With
                        dt.Rows.Add(dr1)

                    End If

                    Dim dr As DataRow = dt.NewRow
                    With dr
                        .Item("ID") = Guid.NewGuid.ToString()
                        .Item("MemberID") = hdnPersonId.Value
                        .Item("MemberID_Name") = txtPersonMember.Text
                        .Item("RoleID") = AptifyApplication.GetEntityRecordIDFromRecordName("Committee Roles", "Member")
                        .Item("StartDate") = Convert.ToDateTime(rdStartdate.SelectedDate).ToShortDateString
                        If Convert.ToDateTime(rdEnddate.SelectedDate).ToShortDateString <> "01/01/0001" Then
                            .Item("EndDate") = Convert.ToDateTime(rdEnddate.SelectedDate).ToShortDateString
                        End If
                        .Item("CommitteeTermID") = "-1"
                        .Item("Role") = "Member"
                    End With
                    dt.Rows.Add(dr)
                    CurrentTable = dt
                    Dim selectFilter As String = " Role <> 'Chair'"

                    ' Dim dataRows As DataRow() = CType(grdMain.DataSource, DataTable).Select(selectFilter)
                    'Dim dataRows As DataRow() = dt.Select(selectFilter)
                    'Dim typeDataRow As DataRow

                    grdMain.DataSource = dt.Select(selectFilter)
                    grdMain.DataBind()
                    grdMain.Visible = True
                    btnSubmitCT.Visible = True
                    btnbackNet.Visible = True
                End If
                txtPersonMember.Text = ""
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub



    Protected Sub btnSubmitCT_Click(sender As Object, e As System.EventArgs) Handles btnSubmitCT.Click
        Try
            Dim strerr As String = String.Empty
            If CommitteeID < 0 Then
                Dim oCommittee As AptifyGenericEntityBase
                oCommittee = AptifyApplication.GetEntityObject("Committees", -1)
                With oCommittee
                    .SetValue("Name", txtNetworkName.Text)
                    .SetValue("DateFounded", rdStartdate.SelectedDate)

                    Dim EmpID As Integer = -1
                    EmpID = AptifyApplication.UserCredentials.GetUserRelatedRecordID("Employees")
                    If EmpID <> -1 Then
                        .SetValue("CoordinatorID", EmpID)
                    Else
                        .SetValue("CoordinatorID", 1)
                    End If
                    .SetValue("CommitteeTypeID", AptifyApplication.GetEntityRecordIDFromRecordName("Committee Types", "Network"))
                    .SetValue("OrganizationID", 1)
                    .SetValue("Goals", txtSummary.Text)
                    If Not .Save(False, strerr) Then
                        lblError.Text = strerr
                        Exit Sub
                    End If
                End With
                CommitteeID = oCommittee.RecordID
                'this code is creating new committee term in newly created committee
                Dim octGe As AptifyGenericEntityBase = Nothing
                octGe = AptifyApplication.GetEntityObject("Committee Terms", -1)
                octGe.SetValue("Name", txtNetworkName.Text)
                octGe.SetValue("StartDate", rdStartdate.SelectedDate)
                octGe.SetValue("EndDate", rdEnddate.SelectedDate)
                octGe.SetValue("CommitteeID", oCommittee.RecordID)
                octGe.SetValue("DirectorID", User1.PersonID)
                If octGe IsNot Nothing Then
                    If grdMain.MasterTableView.Items.Count > 0 Then
                        Dim MemberID As Integer
                        Dim RoleID As Integer
                        Dim StartDate As String
                        Dim EndDate As String

                        For Each dr As DataRow In CurrentTable.Rows
                            With octGe.SubTypes("CommitteeTermMembers").Add()
                                .SetValue("MemberID", dr("MemberID"))
                                .SetValue("RoleID", dr("RoleID"))
                                .SetValue("StartDate", dr("StartDate"))
                                .SetValue("EndDate", dr("EndDate"))
                            End With
                        Next

                        'code  commented by Govind
                        ''For Each dataItem As GridDataItem In grdMain.MasterTableView.Items
                        ''    If grdMain.Visible Then
                        ''        MemberID = DirectCast(dataItem.FindControl("lblMemberID"), Label).Text.Trim()
                        ''        RoleID = DirectCast(dataItem.FindControl("lblRoleID"), Label).Text.Trim()
                        ''        StartDate = DirectCast(dataItem.FindControl("lblStartDate"), Label).Text.Trim()
                        ''        EndDate = DirectCast(dataItem.FindControl("lblEndDate"), Label).Text.Trim()
                        ''        With octGe.SubTypes("CommitteeTermMembers").Add()
                        ''            .SetValue("MemberID", MemberID)
                        ''            .SetValue("RoleID", RoleID)
                        ''            .SetValue("StartDate", StartDate)
                        ''            .SetValue("EndDate", EndDate)
                        ''        End With
                        ''    End If
                        ''Next
                    End If
                End If
                If Not octGe.Save(False, strerr) Then
                    lblError.Text = strerr
                    lblMsg.Text = ""
                Else
                    Visible = True
                    lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.NetworkSubmit.Message")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    'lbl
                End If
                LoadNetworksInfoAndMembers(CommitteeID)

            Else
                Dim oCommittee As AptifyGenericEntityBase
                oCommittee = AptifyApplication.GetEntityObject("Committees", CommitteeID)
                With oCommittee
                    .SetValue("Name", txtNetworkName.Text)
                    .SetValue("DateFounded", rdStartdate.SelectedDate)
                    .SetValue("Goals", txtSummary.Text)
                    .Save()
                End With
                Visible = True
                lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.NetworkSubmit.Message")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

End Class
