'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                  04/04/2014                    Create Person network management
'Amol Bule                     04/09/2014                    Further Changes Made as per requirement.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Partial Class NetworkManagment__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "NetworkManagment__c"
    Protected Const ATTRIBUTE_CONTORL_ADDMANAGENETWORK_NAME As String = "AddManageNetwork"
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
    Public Overridable Property AddManageNetwork() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTORL_ADDMANAGENETWORK_NAME) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTORL_ADDMANAGENETWORK_NAME))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTORL_ADDMANAGENETWORK_NAME) = Me.FixLinkForVirtualPath(value)
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
        If String.IsNullOrEmpty(AddManageNetwork) Then
            'since value is the 'default' check the XML file for possible custom setting
            AddManageNetwork = Me.GetLinkValueFromXML(ATTRIBUTE_CONTORL_ADDMANAGENETWORK_NAME)
        End If

    End Sub
#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            SetProperties()
            If User1.UserID > 0 Then
                If Not IsPostBack Then
                    LoadNetworks()
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
    Protected Sub LoadNetworks()
        Try

            Dim sSql As String = Database & "..spGetNetworkCommittees__c"
            Dim param(0) As IDataParameter
            param(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.BigInt, User1.PersonID)
            Dim dt As DataTable = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                grdMain.DataSource = dt
                grdMain.DataBind()
                grdMain.Visible = True
            Else
                lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.NetworkManagement.NotFoundNetwork")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblError.Visible = True
                grdMain.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub RemoveMeFromNetwork(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim BtnSeletedRow As Button = TryCast(sender, Button)
            Dim row As Telerik.Web.UI.GridTableRow = CType(BtnSeletedRow.NamingContainer, Telerik.Web.UI.GridTableRow)
            Dim CTID As Label = DirectCast(row.FindControl("lblCTID"), Label)
            lblComTID.Text = CTID.Text
            Dim CID As Label = DirectCast(row.FindControl("lblCommitteeID"), Label)
            lblComTID.Text = CTID.Text
            Dim CTMID As Label = DirectCast(row.FindControl("lblctmmID"), Label)
            lblCTMID.Text = CTMID.Text

            radAlert.VisibleOnPageLoad = True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub GotoManageNetwork(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim BtnSeletedRow As Button = TryCast(sender, Button)
            Dim row As Telerik.Web.UI.GridTableRow = CType(BtnSeletedRow.NamingContainer, Telerik.Web.UI.GridTableRow)
            Dim CTID As Label = DirectCast(row.FindControl("lblCommitteeID"), Label)

            Response.Redirect(AddManageNetwork + "?CommitteeID=" + (CTID.Text))
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

    Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        Dim oGe As AptifyGenericEntityBase = Nothing
        Dim oCSGE As AptifyGenericEntityBase
        oGe = AptifyApplication.GetEntityObject("Committee Terms", lblComTID.Text)

        If oGe IsNot Nothing Then
            ' Code Written by Govind on 25th April 2014
            Dim sSql As String = Database & "..spGetCommitteeTermMember__c @CommitteeTermID=" & Convert.ToInt32(lblComTID.Text)
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Dim TodaysDate As Date = New DateTime(Year(Today), Convert.ToInt32(DatePart(DateInterval.Month, Today)), Today.Day)
                For Each dr As DataRow In dt.Rows
                    oCSGE = oGe.SubTypes("CommitteeTermMembers").Find("ID", Convert.ToInt32(dr("ID")))
                    ' First Check if Committee Term Member Start Date is Greater than todays date
                    If Convert.ToDateTime(oCSGE.GetValue("StartDate")) > Convert.ToDateTime(TodaysDate) Then
                        oCSGE.SetValue("EndDate", oCSGE.GetValue("StartDate"))
                    Else
                        oCSGE.SetValue("EndDate", Date.Now.Date)
                    End If
                Next
                ' check Committee Term Start Date
                If Convert.ToDateTime(oGe.GetValue("StartDate")) > Convert.ToDateTime(TodaysDate) Then
                    oGe.SetValue("EndDate", oGe.GetValue("StartDate"))
                Else
                    oGe.SetValue("EndDate", Date.Now.Date)
                End If
                oGe.Save()
            End If
            ' Code End 

            'oCSGE = oGe.SubTypes("CommitteeTermMembers").Find("ID", lblCTMID.Text)

            'If oCSGE IsNot Nothing Then
            '    oCSGE.SetValue("EndDate", Date.Now.Date)
            '    oGe.SetValue("EndDate", Date.Now.Date)

            '    oGe.Save()
            'End If

        End If
        radAlert.VisibleOnPageLoad = False
        LoadNetworks()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        radAlert.VisibleOnPageLoad = False
    End Sub

    Protected Sub btnAddNetwork_Click(sender As Object, e As System.EventArgs) Handles btnAddNetwork.Click
        Response.Redirect(AddManageNetwork + "?CommitteeID=-1")
    End Sub
    ''' <summary>
    ''' This method use is to calling Teleric filter 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdMain_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        Try
            LoadNetworks()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class
