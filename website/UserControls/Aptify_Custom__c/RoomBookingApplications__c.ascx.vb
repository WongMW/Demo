'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                19/04/2014                     Created or Updated Room booking Information
'Rajesh Kardile              04/24/2014                     Change the basic logic as per requirment
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Namespace Aptify.Framework.Web.eBusiness.Generated

    ''' <summary>
    ''' Generated ASP.NET User Control for the RoomBookingApplications__c entity.
    ''' </summary>
    ''' <remarks></remarks>
    Partial Class RoomBookingApplications__cClass
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "RoomBookingApplications__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Protected Const ATTRIBUTE_Home_PAGE As String = "HomePage"
        Dim ReportID As Integer
        Dim RowID As Integer

#Region "Property"

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

        Public Property ResourcesTableDetails() As DataTable
            Get

                If Not ViewState("TmpResourceDetails") Is Nothing Then
                    Return CType(ViewState("TmpResourceDetails"), DataTable)
                Else
                    Dim TmpResourceDetails As New DataTable
                    TmpResourceDetails.Columns.Add("ResourceTypeID")
                    TmpResourceDetails.Columns.Add("ResourceTypeName")
                    TmpResourceDetails.Columns.Add("ResourceID")
                    TmpResourceDetails.Columns.Add("Name")
                    TmpResourceDetails.Columns.Add("Description")
                    TmpResourceDetails.Columns.Add("Quantity")
                    TmpResourceDetails.Columns.Add("Comments")
                    TmpResourceDetails.Columns.Add("StartTime")
                    TmpResourceDetails.Columns.Add("EndTime")
                    Dim ID As DataColumn = New DataColumn
                    ID.DataType = System.Type.GetType("System.Int32")
                    ID.AutoIncrement = True
                    ID.AutoIncrementSeed = 1000
                    ID.AutoIncrementStep = 10
                    TmpResourceDetails.Columns.Add(ID)
                    Return TmpResourceDetails
                End If
            End Get
            Set(ByVal value As DataTable)
                ViewState("TmpResourceDetails") = value
            End Set
        End Property

#End Region

#Region "Sub Routine"

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(Me.RedirectURL) Then
                Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(HomePage) Then
                HomePage = Me.GetLinkValueFromXML(ATTRIBUTE_Home_PAGE)
            End If
            If String.IsNullOrEmpty(Me.RedirectIDParameterName) Then Me.RedirectIDParameterName = "ID"
            If String.IsNullOrEmpty(Me.AppendRecordIDToRedirectURL) Then Me.AppendRecordIDToRedirectURL = "true"
            If String.IsNullOrEmpty(Me.EncryptQueryStringValue) Then Me.EncryptQueryStringValue = "true"
            If String.IsNullOrEmpty(Me.QueryStringRecordIDParameter) Then Me.QueryStringRecordIDParameter = "ID"
        End Sub

        Protected Overridable Sub LoadRecord()
            Try
                ReportID = Convert.ToInt32(Request.QueryString("ReportID"))
                If ReportID > 0 Then
                    ViewState("ReportID") = ReportID
                    Dim sSql As String = Database & "..spGetResourceRequestDetails__c @ReportID=" & ReportID
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        txtRequesterName.Text = Convert.ToString(dt.Rows(0)("Requester"))
                        txtOnBehalfOf.Text = Convert.ToString(dt.Rows(0)("FirstLast"))
                        hfPersonID.Value = Convert.ToString(dt.Rows(0)("OnBehalfOf"))
                        txtMeetingTitle.Text = Convert.ToString(dt.Rows(0)("MeetingTitle"))
                        rdStartdate.SelectedDate = Convert.ToString(dt.Rows(0)("StartDate"))
                        rdEnddate.SelectedDate = Convert.ToString(dt.Rows(0)("EndDate"))
                        txtSeats.Text = Convert.ToString(dt.Rows(0)("Seats"))
                        lblStatus.Text = Convert.ToString(dt.Rows(0)("Status"))
                        If Convert.ToBoolean(dt.Rows(0)("IsProcessed")) = True Then
                            cmdSave.Visible = False
                            trAdditionalRe.Visible = True
                            txtAdditionalResource.Enabled = False
                        End If

                        If lblStatus.Text.Trim.ToLower = "submitted to CAI" Or
                            lblStatus.Text.Trim.ToLower = "provisionally booked" Or
                            lblStatus.Text.Trim.ToLower = "quote sent to customer" Then
                            txtAdditionalResource.Text = Convert.ToString(dt.Rows(0)("AdditionalResource"))
                            trAdditionalRe.Visible = True
                        Else
                            cmdSave.Visible = False
                            trAdditionalRe.Visible = False
                        End If

                        If lblStatus.Text.Trim.ToLower = "not submitted" Then
                            trStatus.Visible = False
                            idResourceInfo.Visible = True
                            cmdSubmit.Visible = True
                            trNote.Visible = True
                            cmdSave.Visible = True
                        Else
                            EnabledAllFileds()
                        End If

                        SetComboValue(cmbVenueID, dt.Rows(0)("Venue"))
                        SetComboValue(cmbRoomTypeID, dt.Rows(0)("RoomType"))
                        LoadResourceDetails(ReportID)
                    Else
                        LoadDataFromGE(Me.AptifyApplication.GetEntityObject("RoomBookingApplications__c", -1))
                        txtRequesterName.Text = AptifyEbusinessUser1.FirstName & " " & AptifyEbusinessUser1.LastName
                    End If
                    cmdCancel.Text = "Back"
                Else
                    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("RoomBookingApplications__c", -1))
                    txtRequesterName.Text = AptifyEbusinessUser1.FirstName & " " & AptifyEbusinessUser1.LastName
                    lblStatus.Text = "Not Submitted"
                    trStatus.Visible = False
                    idResourceInfo.Visible = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadResourceDetails(ByVal RoomBookingID As Integer)
            Try
                If Not ResourcesTableDetails Is Nothing AndAlso ResourcesTableDetails.Rows.Count > 0 Then
                    grdResourceDetails.DataSource = ResourcesTableDetails
                    grdResourceDetails.DataBind()
                    grdResourceDetails.Visible = True
                End If
                Dim sSql As String = Database & "..spGetRequestResourcesDetails__c @RoomBookingID=" & RoomBookingID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    grdResourceDetails.DataSource = dt
                    grdResourceDetails.DataBind()
                    ResourcesTableDetails = dt
                    grdResourceDetails.Visible = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList,
                              ByVal sValue As String)
            Dim i As Integer
            Try
                cmb.ClearSelection()
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

        Protected Overridable Sub SaveRecord(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            Try
                doSave()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub BindResourceDetails()
            Try
                Dim dtResourceTable As DataTable = ResourcesTableDetails
                Dim drResourceTable As DataRow = dtResourceTable.NewRow()
                drResourceTable("ResourceTypeID") = cmbResourceType.SelectedValue
                drResourceTable("ResourceTypeName") = cmbResourceType.SelectedItem.Text
                drResourceTable("ResourceID") = cmbResource.SelectedValue
                drResourceTable("Name") = cmbResource.SelectedItem.Text
                drResourceTable("StartTime") = StartTime.SelectedTime
                drResourceTable("EndTime") = EndTime.SelectedTime
                drResourceTable("Comments") = txtComments.Text
                drResourceTable("Quantity") = txtQuantity.Text
                dtResourceTable.Rows.Add(drResourceTable)
                ResourcesTableDetails = dtResourceTable
                grdResourceDetails.DataSource = ResourcesTableDetails
                grdResourceDetails.DataBind()
                grdResourceDetails.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub ClearResoureseDetails()
            Try
                StartTime.Clear()
                EndTime.Clear()
                txtQuantity.Text = ""
                txtComments.Text = ""
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub RemoveCall(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim LnkSeletedRow As Button = TryCast(sender, Button)
                Dim row As Telerik.Web.UI.GridTableRow = CType(LnkSeletedRow.NamingContainer, Telerik.Web.UI.GridTableRow)
                Dim lblResourceDetailID As Label = DirectCast(row.FindControl("lblResourceDetailID"), Label)
                lblCurrentTableID.Text = lblResourceDetailID.Text
                radAlert.VisibleOnPageLoad = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub EnabledAllFileds()
            Try
                txtRequesterName.Enabled = False
                txtOnBehalfOf.Enabled = False
                txtOnBehalfOf.Enabled = False
                cmbVenueID.Enabled = False
                txtMeetingTitle.Enabled = False
                rdStartdate.Enabled = False
                rdEnddate.Enabled = False
                txtSeats.Enabled = False
                cmbRoomTypeID.Enabled = False
                cmbResourceType.Enabled = False
                cmbResource.Enabled = False
                StartTime.Enabled = False
                EndTime.Enabled = False
                txtQuantity.Enabled = False
                txtComments.Enabled = False
                btnAdd.Enabled = False
                cmdSubmit.Visible = False
                trNote.Visible = False
                chkOther.Enabled = False
                grdResourceDetails.DataSource = ResourcesTableDetails
                grdResourceDetails.DataBind()
                grdResourceDetails.Columns(7).Visible = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub doSave()
            Dim oGE As AptifyGenericEntityBase
            Dim person As AptifyGenericEntityBase
            Dim bRedirect As Boolean = False
            Dim strErrorString As String = String.Empty
            Dim personId As Long
            Try
                If hfPersonID.Value <> String.Empty Then
                    personId = CLng(hfPersonID.Value)
                End If
                If chkOther.Checked = True Then
                    person = AptifyApplication.GetEntityObject("Persons", -1)
                    person.SetValue("FirstName", txtFirstName.Text)
                    person.SetValue("LastName", txtLastName.Text)
                    person.SetValue("Email1", txtEmail.Text)
                    person.SetValue("Status__c", "Pending")
                    person.Save()
                    personId = CLng(person.GetValue("ID"))
                End If
                If Convert.ToInt32(ViewState("ReportID")) > 0 Then
                    oGE = AptifyApplication.GetEntityObject("RoomBookingApplications__c", Convert.ToInt32(ViewState("ReportID")))
                Else
                    oGE = AptifyApplication.GetEntityObject("RoomBookingApplications__c", -1)
                End If
                If lblStatus.Text.Trim.ToLower = "not submitted" Then
                    With oGE
                        .SetValue("RequesterID", AptifyEbusinessUser1.PersonID)
                        .SetValue("OnBehalfOf", personId)
                        .SetValue("VenueID", cmbVenueID.SelectedValue)
                        .SetValue("MeetingTitle", txtMeetingTitle.Text)
                        .SetValue("StartDate", rdStartdate.SelectedDate)
                        .SetValue("EndDate", rdEnddate.SelectedDate)
                        .SetValue("Seats", txtSeats.Text)
                        .SetValue("RoomTypeID", cmbRoomTypeID.SelectedValue)
                        .SetValue("Status", lblStatus.Text)
                        .SetValue("RoomTypeID", cmbRoomTypeID.SelectedValue)
                        .SetValue("Source", "External")
                    End With
                    ' Update Sub Type
                    oGE.SubTypes("RoomBookApplResources__c").Clear()
                    If Not ResourcesTableDetails Is Nothing AndAlso ResourcesTableDetails.Rows.Count > 0 Then
                        For Each dr As DataRow In ResourcesTableDetails.Rows
                            If Not dr.RowState = DataRowState.Deleted AndAlso Not dr.RowState = DataRowState.Detached Then
                                With oGE.SubTypes("RoomBookApplResources__c").Add()

                                    Dim sSQLQuery As String = Database & "..spCheckIsChargeableProductID__c @ResourceID =" & Convert.ToInt32(dr("ResourceID"))

                                    If IsDBNull(dr("ResourceID")) = False Then
                                        .SetValue("ResourceID", Convert.ToInt32(dr("ResourceID")))
                                    End If
                                    .SetValue("StartTime", dr("StartTime"))
                                    .SetValue("EndTime", dr("EndTime"))
                                    .SetValue("Quantity", dr("Quantity"))
                                    .SetValue("Comments", dr("Comments"))
                                    .SetValue("Chargeable", DataAction.ExecuteScalar(sSQLQuery))
                                End With
                            End If
                        Next
                    End If
                    If oGE.Save(False, strErrorString) Then
                        ViewState("ReportID") = oGE.GetValue("ID")
                        bRedirect = True
                        Response.Redirect(Me.RedirectURL, False)
                    Else
                        lblError.Visible = True
                        lblError.Text = oGE.LastError()
                    End If
                Else
                    If lblStatus.Text.Trim.ToLower = "submitted to CAI" Or
                            lblStatus.Text.Trim.ToLower = "provisionally booked" Or
                            lblStatus.Text.Trim.ToLower = "quote sent to customer" Then
                        oGE.SetValue("AdditionalResource", txtAdditionalResource.Text.Trim)
                        If oGE.Save(False, strErrorString) Then
                            lblMessage.Visible = True
                            lblMessage.Text = "* Additional Resource added successfully"
                        Else
                            lblError.Visible = True
                            lblError.Text = oGE.LastError()
                        End If
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Load Data"
        Protected Sub LoadVenus()
            Try
                Dim sSql As String = Database & "..spGetVenuDetails__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                cmbVenueID.DataSource = dt
                cmbVenueID.DataTextField = "Name"
                cmbVenueID.DataValueField = "ID"
                cmbVenueID.DataBind()
                cmbVenueID.Items.Insert(0, "--Select--")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub LoadRoomType()
            Try
                Dim sSql As String = Database & "..spGetRoomTypeDetails__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                cmbRoomTypeID.DataSource = dt
                cmbRoomTypeID.DataTextField = "Name"
                cmbRoomTypeID.DataValueField = "ID"
                cmbRoomTypeID.DataBind()
                cmbRoomTypeID.Items.Insert(0, "--Select--")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub LoadResourceType()
            Try
                Dim sSql As String = Database & "..spGetResourceType__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                cmbResourceType.DataSource = dt
                cmbResourceType.DataTextField = "Name"
                cmbResourceType.DataValueField = "ID"
                cmbResourceType.DataBind()
                cmbResourceType.Items.Insert(0, "--Select--")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub LoadResourcesAsPerResourceType(ByVal ResourceTypeID As Integer)
            Try
                Dim sSql As String = Database & "..spGetResourceAsPerType__c @ResourceTypeID=" & ResourceTypeID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    cmbResource.DataSource = dt
                    cmbResource.DataTextField = "Name"
                    cmbResource.DataValueField = "ID"
                    cmbResource.DataBind()
                    cmbResource.Items.Insert(0, "--Select--")
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Page Event"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                If AptifyEbusinessUser1.UserID > 0 Then
                    lblError.Visible = False
                    If Not IsPostBack Then
                        acePerson.ContextKey = AptifyEbusinessUser1.PersonID.ToString & ";" & AptifyEbusinessUser1.CompanyID.ToString()
                        SetProperties()
                        LoadVenus()
                        LoadRoomType()
                        LoadResourceType()
                        LoadRecord()
                        lblNote.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.RoomBooking.Note")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                Else
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect("~/Home.aspx" + "?ReturnURL=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Request.RawUrl)))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub cmbResourceType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbResourceType.SelectedIndexChanged
            Try
                If cmbResourceType.SelectedItem.Text <> "--Select--" Then
                    LoadResourcesAsPerResourceType(cmbResourceType.SelectedValue)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub cmdSubmit_Click(sender As Object, e As System.EventArgs) Handles cmdSubmit.Click
            Try
                If chkTerms.Checked = True Then
                    doSave()
                    Dim oGE As AptifyGenericEntityBase
                    If Convert.ToInt32(ViewState("ReportID")) > 0 Then
                        oGE = AptifyApplication.GetEntityObject("RoomBookingApplications__c", Convert.ToInt32(ViewState("ReportID")))
                        oGE.SetValue("Status", "Submitted to CAI")
                        oGE.Save()
                    End If
                Else
                    lblError.Visible = True
                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.RoomBooking.Terms&ConditionMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
            Try
                Dim balanceQuantity As Integer
                Dim Params(5) As System.Data.IDataParameter
                Params(0) = DataAction.GetDataParameter("@ResourceID", SqlDbType.Int, cmbResource.SelectedValue)
                Params(1) = DataAction.GetDataParameter("@StartDate", SqlDbType.Date, rdStartdate.SelectedDate)
                Params(2) = DataAction.GetDataParameter("@StartTime", SqlDbType.Time, StartTime.SelectedTime)
                Params(3) = DataAction.GetDataParameter("@EndTime", SqlDbType.Time, EndTime.SelectedTime)
                Params(4) = DataAction.GetDataParameter("@OldValue", SqlDbType.Int, 0)
                Params(5) = DataAction.GetDataParameter("@ID", -1)
                balanceQuantity = DataAction.ExecuteScalarParametrized(Database & "..spGetRoomBookApplResourceBalQuantity__c", CommandType.StoredProcedure, Params)
                If balanceQuantity <> -9999 Then
                    If System.Convert.ToInt32(txtQuantity.Text) < balanceQuantity Then
                        BindResourceDetails()
                        ClearResoureseDetails()
                    Else
                        lblError.Visible = True
                        lblError.Text = String.Format((Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.ProcessFlow.RoomBookingResourceQuantityValidation")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)), cmbResource.SelectedItem.Text)
                    End If
                Else
                    BindResourceDetails()
                    ClearResoureseDetails()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
            Try
                ResourcesTableDetails.Rows.RemoveAt(Convert.ToInt32(ViewState("RowID")))
                grdResourceDetails.DataSource = ResourcesTableDetails
                grdResourceDetails.DataBind()
                grdResourceDetails.Visible = True
                radAlert.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnNo_Click(sender As Object, e As System.EventArgs) Handles btnNo.Click
            Try
                grdResourceDetails.DataSource = ResourcesTableDetails
                grdResourceDetails.DataBind()
                grdResourceDetails.Visible = True
                radAlert.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdResourceDetails_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdResourceDetails.DeleteCommand
            Try
                If e.CommandName = "Delete" Then
                    Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                    ViewState("RowID") = CInt(commandArgs(0))
                    radAlert.VisibleOnPageLoad = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub cmdCancel_Click(sender As Object, e As System.EventArgs) Handles cmdCancel.Click
            Try
                If cmdCancel.Text = "Back" Then
                    Response.Redirect(Me.RedirectURL)
                Else
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(HomePage + "?ReturnURL=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Request.RawUrl)))
                End If
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
        Protected Sub grdResourceDetails_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdResourceDetails.NeedDataSource
            Try
                grdResourceDetails.DataSource = ResourcesTableDetails
                grdResourceDetails.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

    End Class

End Namespace
