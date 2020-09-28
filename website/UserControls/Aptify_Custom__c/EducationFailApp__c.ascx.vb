'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande               08/07/2014                      Display Education Result details as per student on web page
'Rahul Shende               02/21/2015                      Modified btnAddToCard method
'                                                           - Passed CombineLines parameter value = Fales
'                                                           - Removed the For loop, this is not required just we need to find last added orderline to set 
'                                                             the values
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Imports Aptify.Applications.OrderEntry
Imports Microsoft.Reporting.WebForms


Namespace Aptify.Framework.Web.eBusiness.Generated
    Partial Class EducationFailApp__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Public RecordId As Integer
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "EducationFailApp__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Protected Const ATTRIBUTE_CONTROL_ReportPage As String = "ReportPageURL"
        Protected Const ATTRIBUTE_CONTROL_EducationResultPage As String = "EResultPageURL"
        ''Added BY Pradip 2015-11-09 To Redirect To Qualification Status Page
        Protected Const ATTRIBUTE_CONTROL_LLLQualificationStatusPage As String = "LLLQualificationStatusPage"
        Protected Const ATTRIBUTE_ViewCart_PAGE As String = "ViewCartPage"
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
        Public Overridable Property ReportPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_ReportPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_ReportPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_ReportPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Overridable Property EResultPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_EducationResultPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_EducationResultPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_EducationResultPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''Added BY Pradip 2015-11-23 For View Cart Page
        Public Overridable Property ViewCartPage() As String
            Get
                If Not ViewState(ATTRIBUTE_ViewCart_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ViewCart_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ViewCart_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''Added BY Pradip 2015-11-09 To Redirect To Qualification Status Page
        Public Overridable Property LLLQualificationStatusPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_LLLQualificationStatusPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_LLLQualificationStatusPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_LLLQualificationStatusPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                SetProperties()
                CreditCard.LoadCreditCardInfo()
                GetPrefferedCurrency()
                LoadAppealsReason()
                ViewState("ClassRegistrationID") = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("CrId")))
                GetCurriculumID(ViewState("ClassRegistrationID"))
                SetComboValue(drpAppealReason, Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Type"))))
                GetFailFeeAppealProduct(Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Type"))))

                LoadAlreadyRequest()
                'Dim strCourseID As String = Request.QueryString("CourseID")
                'txtCourseAppeal.Text = AptifyApplication.GetEntityRecordName("Courses", CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(strCourseID)))
                'ViewState("CourseID") = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(strCourseID))
                'ViewState("ClassID") = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ClassID")))

                LoadClassRegistrationDetails(Convert.ToInt32(ViewState("ClassRegistrationID")))
                txtCourseAppeal.Text = AptifyApplication.GetEntityRecordName("Courses", CInt(ViewState("CourseID")))
                'ViewState("OrderID") = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("OrderID")))
                LoadAppealData()
                ChkAppealTypeReport()

            End If
        End Sub
        ''' <summary>
        ''' code added by govind for UAT Item G3-P 34
        ''' </summary>
        ''' <param name="ClassRegistrationID"></param>
        ''' <remarks></remarks>
        Private Sub GetCurriculumID(ByVal ClassRegistrationID As Integer)
            Try
                Dim sSql As String = Database & "..spGetCurriculumIDbyCR__c @ClassRegistrationID=" & ClassRegistrationID
                Dim CurriculumID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If CurriculumID > 0 Then
                    ViewState("CurriculumID") = CurriculumID
                Else
                    ViewState("CurriculumID") = 0
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadClassRegistrationDetails(ByVal ClassRegistrationID As Integer)
            Try
                Dim sSql As String = Database & "..spGetClassRegistrationDetailsAsPerID__c @ClassRegisreationID=" & ClassRegistrationID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("CourseID") = dt.Rows(0)("CourseID")
                    ViewState("ClassID") = dt.Rows(0)("ClassID")
                    lblExamNumber.Text = Convert.ToString(dt.Rows(0)("ExamNumber"))

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadAppealData()
            Try
                Dim EntityId As Long
                EntityId = CLng(Me.AptifyApplication.GetEntityID("AppealsApplicationDetail__c"))
                Dim sSql As String = Database & "..spGetAppealDetailsWithType__c @StudentID=" & User1.PersonID & ",@CourseID=" & Convert.ToInt32(ViewState("CourseID")) & _
                                   ",@ClassID=" & Convert.ToInt32(ViewState("ClassID")) & ",@ClassRegistrationID=" & Convert.ToInt32(ViewState("ClassRegistrationID")) & ",@AppealTypeID=" & drpAppealReason.SelectedValue & ",@CurriculumID=" & Convert.ToInt32(ViewState("CurriculumID"))
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    'trRecordAttachment.Visible = True
                    ViewState("FailApp") = Convert.ToInt32(dt.Rows(0)("ID"))
                    txtCourseAppeal.Text = Convert.ToString(dt.Rows(0)("Course"))
                    txtDescription.Text = Convert.ToString(dt.Rows(0)("Description"))
                     If Convert.ToString(dt.Rows(0)("Reason")) <> "" Then
                        drpAppealReason.ClearSelection()
                        SetComboValue(drpAppealReason, Convert.ToString(dt.Rows(0)("Reason")))
                    End If
                    lblStatus.Text = Convert.ToString(dt.Rows(0)("Status")).Trim
                    'RecordAttachments__c.AllowAdd = True
                    ' RecordAttachments__c.AllowDelete = True
                    If Convert.ToString(dt.Rows(0)("Status")).Trim.ToLower <> "in progress" Then
                        EnableFalseAllFileds() ' if status not Sumbitted then user can not edit the Abatement form
                    End If
                    ' Me.RecordAttachments__c.LoadAttachments(EntityId, Convert.ToInt32(ViewState("FailApp")))
                    If Convert.ToInt32(dt.Rows(0)("OrderID")) > 0 Then
                        'CreditCard.Visible = False
                        'btnPay.Visible = False
                        ViewState("OrderID") = dt.Rows(0)("OrderID")
                        btnPay.Text = "Save"
                    End If
                    lblTotalCost.Text = Convert.ToInt32(dt.Rows(0)("Price"))
                    Session("AppealData") = dt
                Else

                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub ChkAppealTypeReport()
            Try
                ''Commented BY Pradip 2016-01-04 For Group 3 Tracker G3-39
                ' Chk if Appeal Type is report then cant show Description
                ' '' ''Dim sAppealReportSQL As String = "..spGetReportProduct__c @AppealTypeID=" & drpAppealReason.SelectedValue
                ' '' ''Dim lReport As Long = Convert.ToInt32(DataAction.ExecuteScalar(sAppealReportSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                ' '' ''If lReport > 0 Then
                ' '' ''    idAppealDescription.Visible = False
                ' '' ''Else
                ' '' ''    idAppealDescription.Visible = True
                ' '' ''End If

                ''Added by Pradip 2016-01-04 For Group 3 Tracker G3-39
                If drpAppealReason.SelectedItem.Text.Trim().ToLower = "extenuating circumstances" Then
                    idAppealDescription.Visible = True
                Else
                    idAppealDescription.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub
        Private Sub EnableFalseAllFileds()
            Try
                txtCourseAppeal.Enabled = False
                txtDescription.Enabled = False
                drpAppealReason.Enabled = False
                'txtTotalCost.Enabled = False
                'RecordAttachments__c.AllowAdd = False
                ' RecordAttachments__c.AllowDelete = False
                btnPay.Visible = False
                btnSave.Visible = False
                CreditCard.Visible = False
                btnAddToCard.Visible = False
                btnPrint.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadAlreadyRequest()
            Try
                Dim oOrders As OrdersEntity
                oOrders = ShoppingCart1.GetOrderObject(Session, Page.User, Application)
                If oOrders.SubTypes("OrderLines") IsNot Nothing AndAlso oOrders.SubTypes("OrderLines").Count > 0 Then
                    For i As Integer = 0 To oOrders.SubTypes("OrderLines").Count - 1
                        If Convert.ToInt32(ViewState("ProductID")) = Convert.ToInt32(oOrders.SubTypes("OrderLines").Item(i).GetValue("ProductID")) AndAlso Convert.ToInt32(oOrders.SubTypes("OrderLines").Item(i).GetValue("ClassRegistrationID__c")) = Convert.ToInt32(ViewState("ClassRegistrationID")) Then
                            btnAddToCard.Visible = False
                            lblAlreadyProductAddedError.Visible = True
                            lblAlreadyProductAddedError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.AppealRequestExists")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            Exit For
                        Else
                            btnAddToCard.Visible = True
                            lblAlreadyProductAddedError.Visible = False
                        End If
                    Next
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

            End Try
        End Sub
        Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList, _
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
        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(Me.RedirectURLs) Then
                Me.RedirectURLs = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(ReportPage) Then
                ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_ReportPage)
            End If
            If String.IsNullOrEmpty(EResultPage) Then
                EResultPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_EducationResultPage)
            End If

            If String.IsNullOrEmpty(LLLQualificationStatusPage) Then
                LLLQualificationStatusPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_LLLQualificationStatusPage)
            End If
            If String.IsNullOrEmpty(ViewCartPage) Then
                ViewCartPage = Me.GetLinkValueFromXML(ATTRIBUTE_ViewCart_PAGE)
            End If
        End Sub
        Private Sub GetFailFeeAppealProduct(ByVal sAppeal As String)
            Try
                Dim sSql As String
                Dim dt As DataTable
                If Request.QueryString("IsLLL") IsNot Nothing And Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("IsLLL")) = "Y" Then
                    If sAppeal.ToLower.Contains("report") Then
                        sSql = Database & "..spGetLLLAppealsProduct__c @Type='Report'" & ",@Appeal='" & sAppeal & "'" & ",@CurriculumID=" & ViewState("CurriculumID")
                        dt = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            lblCurrency.Text = ViewState("CurrencyTypeID")
                            lblTotalCost.Text = GetPrice(dt.Rows(0)("ProductID"))
                            ViewState("ProductID") = dt.Rows(0)("ProductID")
                            drpAppealReason.ClearSelection()
                            drpAppealReason.SelectedValue = dt.Rows(0)("AppealReasonID")
                            Session("AppealReasonID")= dt.Rows(0)("AppealReasonID")
                        End If
                    Else
                        sSql = Database & "..spGetLLLAppealsProduct__c @Type='Appeal'" & ",@Appeal='" & sAppeal & "'" & ",@CurriculumID=" & ViewState("CurriculumID")
                        dt = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            lblCurrency.Text = ViewState("CurrencyTypeID")
                            lblTotalCost.Text = GetPrice(dt.Rows(0)("ProductID"))
                            ViewState("ProductID") = dt.Rows(0)("ProductID")
                            drpAppealReason.ClearSelection()
                            drpAppealReason.SelectedValue = dt.Rows(0)("AppealReasonID")
                            Session("AppealReasonID")= dt.Rows(0)("AppealReasonID")
                        End If
                    End If
                Else
                    If sAppeal.ToLower.Contains("report") Then
                        sSql = Database & "..spGetAppealsProduct__c @Type='Report'" & ",@Appeal='" & sAppeal & "'" & ",@CurriculumID=" & ViewState("CurriculumID")
                        dt = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            lblCurrency.Text = ViewState("CurrencyTypeID")
                            lblTotalCost.Text = GetPrice(dt.Rows(0)("ProductID"))
                            ViewState("ProductID") = dt.Rows(0)("ProductID")
                            Session("AppealReasonID")= dt.Rows(0)("AppealReasonID")
                        End If
                    Else
                        'sSql = Database & "..spGetAppealsProduct__c @Type='Appeal'" & ",@Appeal='" & sAppeal & "'" & ",@CurriculumID=" & ViewState("CurriculumID")
                        sSql = Database & "..spGetAppealsProduct__c @Type='Appeal'" & ",@Appeal='" & sAppeal & "'" & ",@CurriculumID=" & ViewState("CurriculumID") & " " & ",@CourseID =" & ViewState("CourseID") 'Swapnil-07/11/2017 - Redmie #18536
                        dt = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            lblCurrency.Text = ViewState("CurrencyTypeID")
                            lblTotalCost.Text = GetPrice(dt.Rows(0)("ProductID"))
                            ViewState("ProductID") = dt.Rows(0)("ProductID")
                            Session("AppealReasonID")= dt.Rows(0)("AppealReasonID")
                        End If
                    End If
                    SetComboValue(drpAppealReason, Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Type"))))

                End If
                'If sAppeal.ToLower.Contains("report") Then
                '    sSql = Database & "..spGetAppealsProduct__c @Type='Report'" & ",@Appeal='" & sAppeal & "'"
                '    dt = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                '    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                '        lblCurrency.Text = ViewState("CurrencyTypeID")
                '        lblTotalCost.Text = GetPrice(dt.Rows(0)("ProductID"))
                '        ViewState("ProductID") = dt.Rows(0)("ProductID")
                '    End If
                'Else
                '    sSql = Database & "..spGetAppealsProduct__c @Type='Appeal'" & ",@Appeal='" & sAppeal & "'"
                '    dt = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                '    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                '        lblCurrency.Text = ViewState("CurrencyTypeID")
                '        lblTotalCost.Text = GetPrice(dt.Rows(0)("ProductID"))
                '        ViewState("ProductID") = dt.Rows(0)("ProductID")
                '    End If

                'End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Function GetPrice(ByVal lProductID As Long) As String
            Try
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                Dim oOL As Aptify.Applications.OrderEntry.OrderLinesEntity
                'Here get the Top 1 Person ID whose MemberTypeID = 1 
                oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                oOrder.ShipToID = User1.PersonID
                oOrder.BillToID = User1.PersonID
                oOrder.AddProduct(lProductID, 1)

                If oOrder.SubTypes("OrderLines").Count > 0 Then
                    oOL = TryCast(oOrder.SubTypes("OrderLines").Item(0), OrderLinesEntity)
                    'Return Convert.ToString((Convert.ToDouble(oOL.Price), 2, TriState.True, TriState.True, TriState.True)
                    Return Format(CDec(oOL.Price), "0.00")

                    '  Return Convert.ToString(oOL.Price)
                Else
                    Return Convert.ToString(0)
                End If
            Catch ex As Exception
                Return Convert.ToString(0)
            End Try
        End Function
        Private Sub GetPrefferedCurrency()
            Try
                Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("CurrencyTypeID") = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                End If


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadAppealsReason()
            Try
                Dim ssql As String = Database & "..spGetAppealsReason__c"
                Dim dt As DataTable = DataAction.GetDataTable(ssql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    drpAppealReason.ClearSelection()
                    drpAppealReason.DataSource = dt
                    drpAppealReason.DataTextField = "Name"
                    drpAppealReason.DataValueField = "ID"
                    drpAppealReason.DataBind()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
            'Response.Redirect("~\Education\ERdetails.aspx?RID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("RID")))) & "&first=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("first")))) & "&second=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("second")))))
           ' Response.Redirect(EResultPage, False)
  ''Added By Pradip 2015-11-10 For Qualification Status Page.
            If Request.QueryString("IsLLL") IsNot Nothing And Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("IsLLL"))) = "Y" Then
                Dim strFilePath As String = LLLQualificationStatusPage & "?ClassID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(ViewState("ClassID")))) & "&CRID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(ViewState("ClassRegistrationID"))))
                Response.Redirect(strFilePath, False)
            Else
                Response.Redirect(EResultPage, False)
            End If
        End Sub

        Protected Sub btnPay_Click(sender As Object, e As System.EventArgs) Handles btnPay.Click
            Try

                btnSave.Visible = True
                DoSave("pay")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#Region "Save"
        Private Function DoSave(lBtn As String) As Boolean
            Try
                Dim oGE As AptifyGenericEntityBase
                Dim bRedirect As Boolean = False
                Dim lOrderID As Long = -1
                If Convert.ToInt32(ViewState("FailApp")) > 0 Then
                    oGE = AptifyApplication.GetEntityObject("AppealsApplicationDetail__c", Convert.ToInt32(ViewState("FailApp")))
                Else
                    oGE = AptifyApplication.GetEntityObject("AppealsApplicationDetail__c", -1)
                End If
                With oGE
                    .SetValue("ApplicantID", User1.PersonID)
                    .SetValue("CourseID", Convert.ToInt32(ViewState("CourseID")))
                    .SetValue("ClassID", Convert.ToInt32(ViewState("ClassID")))
                    .SetValue("ClassRegistrationID", Convert.ToInt32(ViewState("ClassRegistrationID")))
                    .SetValue("AppealReasonID", Convert.ToInt32(drpAppealReason.SelectedValue))
                    .SetValue("Description", Convert.ToString(txtDescription.Text.Trim))
                    .SetValue("Status", "In Progress")
                    If lBtn = "pay" Then
                        If Convert.ToInt32(ViewState("OrderID")) > 0 Then
                        Else
                            CreateOrder(lOrderID)
                            ViewState("OrderID") = lOrderID
                            If lOrderID > 0 Then
                                .SetValue("OrderID", lOrderID)

                                CreditCard.LoadCreditCardInfo()
                                btnPay.Text = "Save"
                            End If
                        End If
                        .SetValue("Status", lblStatus.Text)
                    Else
                        .SetValue("Status", "Submitted to CAI")
                    End If
                    If .Save() Then

                        'trRecordAttachment.Visible = True
                        'Dim EntityId As Long
                        'EntityId = CLng(Me.AptifyApplication.GetEntityID("AppealsApplicationDetail__c"))
                        'Me.RecordAttachments__c.LoadAttachments(EntityId, oGE.RecordID)
                        ViewState("FailApp") = oGE.RecordID
                        bRedirect = True
                        lblError.Text = "Record Save Successfully"
                    Else
                        bRedirect = False
                    End If
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        Private Sub CreateOrder(ByRef lOrderID As Long)
            Try
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                Dim sError As String = Nothing
                oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                oOrder.ShipToID = User1.PersonID
                oOrder.BillToID = User1.PersonID
                oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
                oOrder.SetValue("BillToSameAsShipTo", 1)
                oOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
                oOrder.AddProduct(Convert.ToInt32(ViewState("ProductID")), 1)
                oOrder.SetValue("OrderTypeID", Aptify.Applications.OrderEntry.OrderType.Regular)   ' Regular Order 
                oOrder.SetValue("PayTypeID", CreditCard.PaymentTypeID.ToString)
                'oOrder.SetValue("PayTypeID", 2)
                oOrder.SetValue("CCAccountNumber", CreditCard.CCNumber.ToString)
                oOrder.SetValue("CCExpireDate", CreditCard.CCExpireDate.ToString)
                If Not oOrder.Save(sError) Then
                    lblError.Text = "<ui><li>" + sError + "</li></ui>"
                    lblError.Visible = True
                Else
                    lOrderID = oOrder.RecordID
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

        Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
            Try
                DoSave("Submit")
                btnPay.Visible = False
                btnSave.Visible = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnAddToCard_Click(sender As Object, e As System.EventArgs) Handles btnAddToCard.Click
            Try
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                ShoppingCart1.AddToCart(Convert.ToInt32(ViewState("ProductID")), False, , 1)

                'RS : Start : 02/212015
                Dim iOrderLineCount As Integer = -1, ol As OrderLinesEntity = Nothing
                iOrderLineCount = oOrder.SubTypes("OrderLines").Count - 1
                ol = oOrder.SubTypes("OrderLines").Item(iOrderLineCount)
                If ol IsNot Nothing Then
                    ol.SetValue("ClassRegistrationID__c", Convert.ToInt32(ViewState("ClassRegistrationID")))
                    ol.SetValue("AppealTypeResonID__c", Convert.ToString(Session("AppealReasonID")))
                    ol.SetValue("Comments", txtDescription.Text)
                    ol.SetValue("Description", Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.AppealCourseFor")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & txtCourseAppeal.Text)
                End If

                'For Each ol As OrderLinesEntity In oOrder.SubTypes("OrderLines")
                '    If CLng(ol.GetValue("ProductID")) = CLng(ViewState("ProductID")) Then
                '        ol.SetValue("ClassRegistrationID__c", Convert.ToInt32(ViewState("ClassRegistrationID")))
                '        ol.SetValue("AppealTypeResonID__c", Convert.ToString(drpAppealReason.SelectedValue))
                '        ol.SetValue("Comments", txtDescription.Text)
                '        ol.SetValue("Description", Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.AppealCourseFor")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & txtCourseAppeal.Text)
                '    End If
                'Next
                'RS : End : 02/212015

                ShoppingCart1.SaveCart(Page.Session)
                If Request.QueryString("IsLLL") IsNot Nothing And Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("IsLLL")) = "Y" Then
                    ''If Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Type")).ToLower = "book repeat exam" Or Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Type")).ToLower = "tutorial report" Then
                        Response.Redirect(ViewCartPage, False)
                   '' Else
                       '' Dim strFilePath As String = LLLQualificationStatusPage & "?ClassID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(ViewState("ClassID")))) & "&CRID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(ViewState("ClassRegistrationID")))) & "&IsAdded=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt("Y"))
                       '' Response.Redirect(strFilePath, False)
                    ''End If
                    'Response.Redirect("~\ProductCatalog\ViewCart.aspx", False)
                Else
                    Response.Redirect(EResultPage, False)
                End If
                ' Response.Redirect("~\Education\ERdetails.aspx?RID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("RID")))) & "&first=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("first")))) & "&second=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("second")))))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
            Try
                     Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Appeal Fail Report"))
                Dim rptParam As New AptifyCrystalReport__c
                rptParam.ReportID = ReportID
                rptParam.Param1 = Convert.ToString(User1.PersonID)
                rptParam.Param2 = Convert.ToString(ViewState("CourseID"))
                rptParam.Param3 = Convert.ToString(ViewState("ClassID"))
                rptParam.Param4 = Convert.ToString(ViewState("ClassRegistrationID"))
                rptParam.Param5 = Convert.ToString(Session("AppealReasonID"))
                'rptParam.Param5 = Convert.ToString(drpAppealReason.SelectedValue)
                rptParam.Param6 = Convert.ToString(ViewState("OrderID"))
                rptParam.Param7 = Convert.ToString(ViewState("CurriculumID"))
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)

                'Response.Redirect("~\Education\AppealReport.aspx?sID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(User1.PersonID)) & "&CourseID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("CourseID")))) & _
                '                    "&ClassID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("ClassID")))) & "&CRID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("ClassRegistrationID")))) & "&AppealTypeID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(drpAppealReason.SelectedValue))) & "&OrderID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("OrderID")))))

                'ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('~\Education\AppealReport.aspx" & "?sID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(User1.PersonID)) & "&CourseID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("CourseID")))) & _
                '                    "&ClassID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("ClassID")))) & "&CRID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("ClassRegistrationID")))) & "&AppealTypeID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(drpAppealReason.SelectedValue))) & "&OrderID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("OrderID")))) & "')", True)

               ' ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "" & "?sID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(User1.PersonID)) & "&CourseID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("CourseID")))) & _
                   '                "&ClassID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("ClassID")))) & "&CRID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("ClassRegistrationID")))) & "&AppealTypeID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(drpAppealReason.SelectedValue))) & "&OrderID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToInt32(ViewState("OrderID")))) & "')", True)
                'DownloadFile(sReportFile)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Private Sub DownloadFile(ByVal ReportFileName As String)
        '    Try
        '        Dim doc As reportDocument
        '        'reportDocument1.RecordSelectionFormula = "{vwListDetail.ListID}=" & m_ListID.ToString
        '        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        '        Dim crParameterFieldDefinition As ParameterFieldDefinition
        '        Dim crParameterValues As New ParameterValues
        '        Dim crParameterDiscreteValue As New ParameterDiscreteValue

        '    Catch ex As Exception

        '    End Try
        'End Sub
    End Class
End Namespace
