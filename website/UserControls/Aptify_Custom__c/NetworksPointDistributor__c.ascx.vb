'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                04/11/2014                          Network Owner can Distributor Point
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports System.Web.UI
Imports Telerik.Web.UI
Partial Class NetworksPointDistributor__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_Home_PAGE As String = "HomePage"
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "NetworksPointDistributor__c"
    ''' <summary>
    ''' Login Page page url
    ''' </summary>
    Public Overridable Property HomePage() As String
        Get
            If Not ViewState(ATTRIBUTE_Home_PAGE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_Home_PAGE))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_Home_PAGE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Protected Overrides Sub SetProperties()
        If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        'call base method to set parent properties
        MyBase.SetProperties()
        If String.IsNullOrEmpty(HomePage) Then
            'since value is the 'default' check the XML file for possible custom setting
            HomePage = Me.GetLinkValueFromXML(ATTRIBUTE_Home_PAGE)
        End If

    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            SetProperties()
            If Not IsPostBack Then
                LoadCampaign(2)
                GetTotalsPoint()
                LoadPersonCompanyList()

                lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.TrainingDistributed.Note")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#Region "Load Data"
    ''' <summary>
    ''' Load Campaign Record 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCampaign(ByVal CampignFor As Integer)
        Try
            Dim sSql As String = Database & "..spGetCamapignsList__c @PersonID=" & User1.PersonID & ",@CampignFor=" & CampignFor
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                cmbCampaign.DataSource = dt
                cmbCampaign.DataTextField = "Name"
                cmbCampaign.DataValueField = "ID"
                cmbCampaign.DataBind()
                tblMain.Visible = True
            Else
                tblMain.Visible = False
                lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.PointDistribution.Message")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' if User Select My Company Network then show user company person record
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadPersonCompanyList()
        Try
            Dim sSql As String = Database & "..spGetCompanyNetworkPersons__c @PersonID=" & User1.PersonID & ",@CompanyID=" & User1.CompanyID & _
                                             ",@CampaignID=" & cmbCampaign.SelectedValue
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                grdCompanyPersonList.DataSource = dt
                grdCompanyPersonList.DataBind()
                grdCompanyPersonList.Visible = True
                rdoMyCompany.Checked = True
                idNote.Visible = True
            Else
                lblErrorNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", " AptifyEbusiness.PointDistribution.NoCompanyFound")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                grdCompanyPersonList.Visible = False
                idNote.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetTotalsPoint()
        Try
            Dim SSql As String = Database & "..spGetTotalPointsOwner__c @CampaignID=" & cmbCampaign.SelectedValue & ",@PersonID=" & User1.PersonID
            Dim dt As DataTable = DataAction.GetDataTable(SSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                lblTotalPoints.Text = Convert.ToString(dt.Rows(0)("TotalOwnerPoint"))
                lblDistributedPoints.Text = Convert.ToString(dt.Rows(0)("TotalAssigned"))
                lblOwnerUsedQty.Text = Convert.ToString(dt.Rows(0)("UsedQty"))
                GetNetworksPoints()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetNetworksPoints()
        Try
            Dim sSql As String = Database & "..spGetNetworksPoints__c @CampaignID=" & cmbCampaign.SelectedValue
            Dim dNetworkPt As Decimal = Convert.ToDecimal(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            lblDistributedNW.Text = dNetworkPt
            lblRemainingPoints.Text = Convert.ToDecimal(lblTotalPoints.Text) - (Convert.ToDecimal(lblDistributedPoints.Text) + Convert.ToDecimal(lblDistributedNW.Text) + Convert.ToDecimal(lblOwnerUsedQty.Text))

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load Committee's
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCommittee()
        Try
            Dim sSql As String = Database & "..spGetCampaignList__c @PersonID=" & User1.PersonID
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                cmbCommittees.DataSource = dt
                cmbCommittees.DataTextField = "Name"
                cmbCommittees.DataValueField = "ID"
                cmbCommittees.DataBind()
                cmbCommittees.Visible = True
                trNW.Visible = True
            Else
                trNW.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

    Protected Sub rdoMyCompany_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoMyCompany.CheckedChanged
        Try
            lblMsg.Text = ""
            LoadCampaign(2)
            GetTotalsPoint()
            LoadPersonCompanyList()
            trNW.Visible = False
            lblErrorNotFound.Text = ""
            GetSelectedNetworkPoints()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' This method use is to calling Teleric filter 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdCompanyPersonList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdCompanyPersonList.NeedDataSource
        Try
            LoadPersonCompanyList()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' if any qty update check Owner Points is grater than total point distribution
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Public Sub txtQuantityAssigned_Change(sender As Object, e As EventArgs)
    '    Try
    '        Dim dTotalPointDistribted As Decimal
    '        Dim dUsedQty As Decimal
    '        Dim lblUsedQty As Label
    '        Dim txtQuantityAssigned As TextBox
    '        For Each row As Telerik.Web.UI.GridItem In grdCompanyPersonList.Items
    '            txtQuantityAssigned = DirectCast(row.FindControl("txtQuantityAssigned"), TextBox)
    '            lblUsedQty = DirectCast(row.FindControl("lblQtyUsed"), Label)
    '            dTotalPointDistribted = dTotalPointDistribted + Convert.ToDecimal(txtQuantityAssigned.Text)
    '            dUsedQty = dUsedQty + Convert.ToDecimal(lblUsedQty.Text)
    '        Next
    '        If dTotalPointDistribted > 0 Then
    '            dTotalPointDistribted = (dTotalPointDistribted + dUsedQty) + Convert.ToDecimal(lblDistributedNW.Text) + Convert.ToDecimal(lblDistributedPoints.Text) + Convert.ToDecimal(lblOwnerUsedQty.Text)

    '            If dTotalPointDistribted > Convert.ToDecimal(lblTotalPoints.Text) Then
    '                '  LoadPersonCompanyList()
    '                lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ViewCart.TrainingTicketPoints")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
    '                radMockTrial.VisibleOnPageLoad = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Get Total Point of campaign for displaying Summary
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmbCampaign_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbCampaign.SelectedIndexChanged
        Try
            If rdoMyCompany.Checked Then
                '   LoadCampaign(2)
                trNW.Visible = False
                rdoMyNetwork.Checked = False
                rdoMyCompany.Checked = True
                GetTotalsPoint()
                LoadPersonCompanyList()
            ElseIf rdoMyNetwork.Checked Then
                'LoadCampaign(1)
                GetTotalsPoint()
                lblErrorNotFound.Text = ""
                LoadCommittee()
                LoadNetworkData()
                GetSelectedNetworkPoints()
            End If


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Its Click on ok popup will be close and load the Person company list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        Try
            radMockTrial.VisibleOnPageLoad = False
            If rdoMyCompany.Checked = True Then
                LoadPersonCompanyList()
                GetTotalsPoint()
            Else
                LoadNetworkData()
                GetNetworksPoints()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' on Submit Add or update prospect list as per data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim bWarning As Boolean = False
            Dim dTotalPointDistribted As Decimal
            Dim dUsedQty As Decimal
            Dim lblUsedQty As Label
            Dim txtQuantityAssigned As TextBox
            For Each row As Telerik.Web.UI.GridItem In grdCompanyPersonList.Items
                txtQuantityAssigned = DirectCast(row.FindControl("txtQuantityAssigned"), TextBox)
                lblUsedQty = DirectCast(row.FindControl("lblQtyUsed"), Label)
                dTotalPointDistribted = dTotalPointDistribted + Convert.ToDecimal(txtQuantityAssigned.Text)
                dUsedQty = dUsedQty + Convert.ToDecimal(lblUsedQty.Text)
            Next
            If dTotalPointDistribted > 0 Then
                dTotalPointDistribted = (dTotalPointDistribted + dUsedQty) + Convert.ToDecimal(lblDistributedNW.Text) + Convert.ToDecimal(lblDistributedPoints.Text) + Convert.ToDecimal(lblOwnerUsedQty.Text)

                If dTotalPointDistribted > Convert.ToDecimal(lblTotalPoints.Text) Then
                    '  LoadPersonCompanyList()
                    bWarning = True
                    lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ViewCart.TrainingTicketPoints")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radMockTrial.VisibleOnPageLoad = True
                End If
            End If
            If bWarning = False Then
                doSave()

            End If
            '  Response.Redirect(HomePage)

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' save selected persons on campaign prospect list
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doSave()
        Try
            Dim oCampaignProspectListGE As AptifyGenericEntityBase
            Dim chkSelect As CheckBox
            Dim txtQuantityAssigned As TextBox
            Dim lblPersonID As Label
            Dim lblAssignedQty As Label
            Dim sSql As String
            Dim lProspectListID As Long
            Dim dTotalQtyAssigned As Decimal
            For Each row As Telerik.Web.UI.GridItem In grdCompanyPersonList.Items
                chkSelect = DirectCast(row.FindControl("chkSelect"), CheckBox)
                txtQuantityAssigned = DirectCast(row.FindControl("txtQuantityAssigned"), TextBox)
                lblPersonID = DirectCast(row.FindControl("lblPersonID"), Label)
                lblAssignedQty = DirectCast(row.FindControl("lblAssignedQty"), Label)
                ' checked is selected or not
                If chkSelect.Checked = True AndAlso txtQuantityAssigned.Text > 0 Then
                    dTotalQtyAssigned = dTotalQtyAssigned + Convert.ToDecimal(txtQuantityAssigned.Text)
                    ' Now First check this person is already on same campaign prospect list, if found then update the Max Qty else create new prospect list record
                    sSql = Database & "..spGetProspectListPersonDetails__c @CampaignID=" & cmbCampaign.SelectedValue & ",@PersonID=" & Convert.ToInt32(lblPersonID.Text)
                    lProspectListID = Convert.ToInt64(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lProspectListID > 0 Then
                        ' it meanse this person already present on prospect list
                        oCampaignProspectListGE = AptifyApplication.GetEntityObject("Campaign List Detail", lProspectListID)
                    Else
                        oCampaignProspectListGE = AptifyApplication.GetEntityObject("Campaign List Detail", -1)
                    End If
                    With oCampaignProspectListGE
                        .SetValue("CampaignID", cmbCampaign.SelectedValue)
                        .SetValue("PersonID", Convert.ToInt32(lblPersonID.Text))
                        .SetValue("MaxQuantity__c", (Convert.ToDecimal(txtQuantityAssigned.Text) + Convert.ToDecimal(lblAssignedQty.Text)))
                        '  .SetValue("OriginalMaxQty__c", (Convert.ToDecimal(txtQuantityAssigned.Text) + Convert.ToDecimal(lblAssignedQty.Text)))

                        If rdoMyNetwork.Checked = True Then
                            .SetValue("IsNetwork__c", 1)
                        End If
                    End With
                    oCampaignProspectListGE.Save()
                End If
            Next
            ' Update Owner Prospect List Max Qty
            sSql = Database & "..spGetProspectListPersonDetails__c @CampaignID=" & cmbCampaign.SelectedValue & ",@PersonID=" & User1.PersonID
            lProspectListID = Convert.ToInt64(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If lProspectListID > 0 Then
                oCampaignProspectListGE = AptifyApplication.GetEntityObject("Campaign List Detail", lProspectListID)
                Dim dCampaignMaxQty As Decimal = Convert.ToDecimal(oCampaignProspectListGE.GetValue("MaxQuantity__c"))
                dCampaignMaxQty = dCampaignMaxQty - dTotalQtyAssigned
                With oCampaignProspectListGE
                    .SetValue("MaxQuantity__c", dCampaignMaxQty)
                    '  .SetValue("OriginalMaxQty__c", dCampaignMaxQty)

                    .Save()
                    lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.TrainingTicketPoints.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radMockTrial.VisibleOnPageLoad = True
                    'lblMsg.Text = "Record Save Successfully"
                End With
            End If


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub deallocatedSave()
        Try
            Dim oCampaignProspectListGE As AptifyGenericEntityBase
            Dim chkSelect As CheckBox
            Dim txtQuantityAssigned As TextBox
            Dim lblPersonID As Label
            Dim lblAssignedQty As Label
            Dim sSql As String
            Dim lProspectListID As Long
            Dim dTotalQtyAssigned As Decimal
            For Each row As Telerik.Web.UI.GridItem In grdCompanyPersonList.Items
                chkSelect = DirectCast(row.FindControl("chkSelect"), CheckBox)
                txtQuantityAssigned = DirectCast(row.FindControl("txtQuantityAssigned"), TextBox)
                lblPersonID = DirectCast(row.FindControl("lblPersonID"), Label)
                lblAssignedQty = DirectCast(row.FindControl("lblAssignedQty"), Label)
                ' checked is selected or not
                If chkSelect.Checked = True AndAlso txtQuantityAssigned.Text > 0 Then
                    dTotalQtyAssigned = dTotalQtyAssigned + Convert.ToDecimal(txtQuantityAssigned.Text)
                    ' Now First check this person is already on same campaign prospect list, if found then update the Max Qty else create new prospect list record
                    sSql = Database & "..spGetProspectListPersonDetails__c @CampaignID=" & cmbCampaign.SelectedValue & ",@PersonID=" & Convert.ToInt32(lblPersonID.Text)
                    lProspectListID = Convert.ToInt64(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lProspectListID > 0 Then
                        ' it meanse this person already present on prospect list
                        oCampaignProspectListGE = AptifyApplication.GetEntityObject("Campaign List Detail", lProspectListID)
                    Else
                        oCampaignProspectListGE = AptifyApplication.GetEntityObject("Campaign List Detail", -1)
                    End If
                    With oCampaignProspectListGE
                        .SetValue("CampaignID", cmbCampaign.SelectedValue)
                        .SetValue("PersonID", Convert.ToInt32(lblPersonID.Text))
                        .SetValue("MaxQuantity__c", Convert.ToDecimal(lblAssignedQty.Text) - Convert.ToDecimal(txtQuantityAssigned.Text))
                        '  .SetValue("OriginalMaxQty__c", (Convert.ToDecimal(txtQuantityAssigned.Text) + Convert.ToDecimal(lblAssignedQty.Text)))

                        If rdoMyNetwork.Checked = True Then
                            .SetValue("IsNetwork__c", 1)
                        End If
                    End With
                    oCampaignProspectListGE.Save()
                End If
            Next
            ' Update Owner Prospect List Max Qty
            sSql = Database & "..spGetProspectListPersonDetails__c @CampaignID=" & cmbCampaign.SelectedValue & ",@PersonID=" & User1.PersonID
            lProspectListID = Convert.ToInt64(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If lProspectListID > 0 Then
                oCampaignProspectListGE = AptifyApplication.GetEntityObject("Campaign List Detail", lProspectListID)
                Dim dCampaignMaxQty As Decimal = Convert.ToDecimal(oCampaignProspectListGE.GetValue("MaxQuantity__c"))
                dCampaignMaxQty = dCampaignMaxQty + dTotalQtyAssigned
                With oCampaignProspectListGE
                    .SetValue("MaxQuantity__c", dCampaignMaxQty)
                    '  .SetValue("OriginalMaxQty__c", dCampaignMaxQty)

                    .Save()
                    lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.TrainingTicketPoints.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radMockTrial.VisibleOnPageLoad = True
                    'lblMsg.Text = "Record Save Successfully"
                End With
            End If


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub rdoMyNetwork_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoMyNetwork.CheckedChanged
        Try
            lblMsg.Text = ""
            LoadCampaign(1)
            GetTotalsPoint()
            lblErrorNotFound.Text = ""
            LoadCommittee()
            LoadNetworkData()
            GetSelectedNetworkPoints()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#Region "Load Networks"
    Private Sub LoadNetworkData()
        Try
            If cmbCommittees.SelectedValue <> "" Then
                Dim sSql As String = Database & "..spGetMyCommitteesPersons__c @PersonID=" & User1.PersonID & ",@CommitteID=" & cmbCommittees.SelectedValue & _
                                          ",@CampaignID=" & cmbCampaign.SelectedValue
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    grdCompanyPersonList.DataSource = dt
                    grdCompanyPersonList.DataBind()
                    lblErrorNotFound.Text = ""
                    grdCompanyPersonList.Visible = True
                    idNote.Visible = True
                End If
            Else
                grdCompanyPersonList.Visible = False
                idNote.Visible = False
                lblErrorNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.PointDistribution.NoNWFound")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If


            '
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub cmbCommittees_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbCommittees.SelectedIndexChanged
        Try
            LoadNetworkData()
            GetSelectedNetworkPoints()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetSelectedNetworkPoints()
        Try
            Dim dTotalPointDistribted As Decimal
            Dim dUsedQty As Decimal
            Dim lblUsedQty As Label
            Dim lblQuantityAssigned As Label
            For Each row As Telerik.Web.UI.GridItem In grdCompanyPersonList.Items
                lblQuantityAssigned = DirectCast(row.FindControl("lblAssignedQty"), Label)
                lblUsedQty = DirectCast(row.FindControl("lblQtyUsed"), Label)
                dTotalPointDistribted = dTotalPointDistribted + Convert.ToDecimal(lblQuantityAssigned.Text)
                dUsedQty = dUsedQty + Convert.ToDecimal(lblUsedQty.Text)
            Next
            lblNetworkPoint.Text = "Total Network Points Assigned: " & (dTotalPointDistribted + dUsedQty)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Public Sub chkSelect_CheckedChanged(sender As Object, e As EventArgs)
        Dim lblChecked As CheckBox
        Dim txtQuantityAssigned As TextBox
        For Each row As Telerik.Web.UI.GridItem In grdCompanyPersonList.Items
            lblChecked = DirectCast(row.FindControl("chkSelect"), CheckBox)
            txtQuantityAssigned = DirectCast(row.FindControl("txtQuantityAssigned"), TextBox)
            If lblChecked.Checked Then
                txtQuantityAssigned.Enabled = True
            Else
                txtQuantityAssigned.Enabled = False
            End If
        Next
    End Sub

#End Region



    Protected Sub btnDeallocate_Click(sender As Object, e As System.EventArgs) Handles btnDeallocate.Click
        Try
            Dim bWarning As Boolean = False
            Dim lblUsedQty As Label
            Dim txtQuantityAssigned As TextBox
            Dim lblAssignedQty As Label
            For Each row As Telerik.Web.UI.GridItem In grdCompanyPersonList.Items
                txtQuantityAssigned = DirectCast(row.FindControl("txtQuantityAssigned"), TextBox)
                lblUsedQty = DirectCast(row.FindControl("lblQtyUsed"), Label)
                lblAssignedQty = DirectCast(row.FindControl("lblAssignedQty"), Label)
                If Convert.ToDecimal(txtQuantityAssigned.Text.Trim) <= Convert.ToDecimal(lblAssignedQty.Text.Trim) Then
                    '  MinusQty = MinusQty + Convert.ToDecimal(txtQuantityAssigned.Text.Substring(1, txtQuantityAssigned.Text.Trim.Length - 1))
                Else
                    bWarning = True
                    lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.TrainingTicketPoints.NegativeBalance")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radMockTrial.VisibleOnPageLoad = True
                End If
                'Else
                'dTotalPointDistribted = dTotalPointDistribted + Convert.ToDecimal(txtQuantityAssigned.Text)
                'dUsedQty = dUsedQty + Convert.ToDecimal(lblUsedQty.Text)
                'End If

            Next
            If bWarning = False Then
                deallocatedSave()
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class
