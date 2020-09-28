'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Siddharth Kavitake         6/9/2014                            Curriculum Application
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Applications.OrderEntry

Partial Class CurriculumApplication__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

    Private _ordersEntity As New OrdersEntity()
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_MAXLOGINTRIES As String = "maxLoginTries"
    Private _employeeId As Integer = 0

#Region "Property Setting"


    Public Property EmployeeID() As Integer
        Get
            If Not String.IsNullOrEmpty(CInt(Request.QueryString("Eid"))) Then
                _employeeId = CInt(Request.QueryString("Eid"))
            End If
            Return _employeeId
        End Get
        Set(ByVal value As Integer)
            _employeeId = value
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

    Protected Overrides Sub SetProperties()
        If String.IsNullOrEmpty(LoginPage) Then
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
        End If
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If AptifyEbusinessUser1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            End If

            If Not IsPostBack Then
                LoadDropDowns()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub LoadDropDowns()
        Try
            'cmbCurriculum.ClearSelection()

            Dim sSQL As String
            sSQL = Database & "..spGetAllCurriculumForCA__c"
            cmbCurriculum.DataSource = DataAction.GetDataTable(sSQL)
            cmbCurriculum.DataTextField = "Name"
            cmbCurriculum.DataValueField = "ID"
            cmbCurriculum.DataBind()

            sSQL = Database & "..spGetAllApplicationTypes__c"
            cmbRoutes.DataSource = DataAction.GetDataTable(sSQL)
            cmbRoutes.DataTextField = "Name"
            cmbRoutes.DataValueField = "ID"
            cmbRoutes.DataBind()

            sSQL = Database & "..spGetVenues__c"
            Dim dtLocations As DataTable = DataAction.GetDataTable(sSQL)
            cmbLocation.DataSource = dtLocations
            cmbLocation.DataTextField = "Name"
            cmbLocation.DataValueField = "ID"
            cmbLocation.DataBind()

            'cmbExamLocation.DataSource = dtLocations
            'cmbExamLocation.DataTextField = "Name"
            'cmbExamLocation.DataValueField = "ID"
            'cmbExamLocation.DataBind()

            'cmbAssessmentLocation.DataSource = dtLocations
            'cmbAssessmentLocation.DataTextField = "Name"
            'cmbAssessmentLocation.DataValueField = "ID"
            'cmbAssessmentLocation.DataBind()

            sSQL = Database & "..spGetAllTimeTables__c"
            cmbTimeTable.DataSource = DataAction.GetDataTable(sSQL)
            cmbTimeTable.DataTextField = "Name"
            cmbTimeTable.DataValueField = "ID"
            cmbTimeTable.DataBind()

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Place order
    ''' </summary>
    Private Sub PlaceOrder()
        Try
            Dim orderId As Integer = CInt(Me.AptifyApplication.GetEntityAttribute("ExemptionApplication__c", "OrderID__c"))
            _ordersEntity = Me.AptifyApplication.GetEntityObject("Orders", orderId)
            _ordersEntity.SetValue("OnBehalfEmployeeID__c", Me.EmployeeID)
            Dim errorMessage As String = String.Empty
            If _ordersEntity.Save(errorMessage) Then

            Else

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnPlaceOrder_Click(sender As Object, e As System.EventArgs) Handles btnPlaceOrder.Click
        PlaceOrder()
    End Sub

End Class
