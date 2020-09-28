'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                 13/10/2015                         create LLL LLL Course Catalog Page and redirects to LLL Enrolment page
'Shital Jadhav                28/10/2015                         Modified as per new requirement
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Applications.OrderEntry
Imports Telerik.Web.UI
Imports System.Net
Imports System.IO
Imports System.Text

Namespace Aptify.Framework.Web.eBusiness
    Partial Class LLLCourseCatalog__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "LLLCourseCatalog__c"
        Protected Const ATTRIBUTE_CONTROL_EnrolmentPage_NAME As String = "EnrolmentPage"
        Protected Const ATTRIBUTE_CONTROL_ViewcartPage_NAME As String = "ViewcartPage"
        Protected Const ATTRIBUTE_SECURITYERROR_PAGE As String = "securityErrorPage"
        Protected Const ATTRIBUTE_CONTROL_CTC_NAME As String = "CTC"
        Dim ClassID As Integer
#Region "Page Load"
        Public Overloads Property EnrolmentPageURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_EnrolmentPage_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_EnrolmentPage_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_EnrolmentPage_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Overloads Property CTCPageURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_CTC_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_CTC_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_CTC_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        Public Overloads Property RedirectViewCartURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_ViewcartPage_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_ViewcartPage_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_ViewcartPage_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                SetProperties()

                If Not IsPostBack Then
                    'Call function for bind LLL Categories Courses
                    BindCourseDetails()
                End If
            Catch ex As Exception

            End Try
        End Sub
        Protected Overrides Sub SetProperties()
            Try
                Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
                MyBase.SetProperties()
                If String.IsNullOrEmpty(EnrolmentPageURL) Then
                    Me.EnrolmentPageURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_EnrolmentPage_NAME)
                End If
                If String.IsNullOrEmpty(RedirectViewCartURL) Then
                    Me.RedirectViewCartURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_ViewcartPage_NAME)
                End If

                If String.IsNullOrEmpty(CTCPageURL) Then
                    Me.CTCPageURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_CTC_NAME)
                End If

            Catch ex As Exception

            End Try
        End Sub

#End Region


#Region "Grid Events"

        Private Sub GetPrefferedCurrency()
            Try
                Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    'ViewState("CurrencyTypeID") = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                    'lblCurrency.Text = ViewState("CurrencyTypeID")
                End If


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub BindClassDetails(ByVal CourseID As String)
            Try
                Dim sSql As String = Database & "..spGetClassDetailsforCatelog__c @CourseID=" & CourseID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    sSql = Database & "..spGetLLLCourseClassRegistration__c @CourseID=" & CourseID & ", @StudentID=" & User1.PersonID
                    Dim RegCount As Int32
                    RegCount = DataAction.ExecuteScalar(sSql)
                    If RegCount > 0 Then
                        ViewState("VisibleEnrollLink") = False
                    Else
                        ViewState("VisibleEnrollLink") = True
                    End If
                    radClassDetails.DataSource = dt
                    radClassDetails.DataBind()
                    radClassDetails.Visible = True
                    hearderclass.Visible = True
                Else
                    radClassDetails.Visible = False
                    hearderclass.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub BindCourseDetails()
            Try

                Dim sSql As String = Database & "..spGetLLLCoursesforCatelog__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    radCourseDetails.DataSource = dt
                    radCourseDetails.DataBind()
                    radCourseDetails.Visible = True
                Else
                    radCourseDetails.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub radClassDetails_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radClassDetails.ItemCommand
            'Try
            If e.CommandName.ToUpper = "EnrollLink".ToUpper Then
                Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                Dim enrolltype As String
                enrolltype = commandArgs(0)

                Dim lClassID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(commandArgs(1))

                'If enrolltype.ToUpper = "Enroll".ToUpper Then
                'Changed by Shweta on 30-05-2017 for issue #17373
                'Caption changed from Enroll to Application
                If enrolltype.ToUpper = "Enrol".ToUpper Then
                    Response.Redirect(EnrolmentPageURL & "?cid=" & System.Web.HttpUtility.UrlEncode(lClassID), False)
                ElseIf enrolltype = "CTC" Then
                    Response.Redirect(CTCPageURL & "?id=" & System.Web.HttpUtility.UrlEncode(lClassID))
                End If
            ElseIf e.CommandName.ToUpper = "Schedule".ToUpper Then
                Dim commandArgs As String = e.CommandArgument.ToString()
                ViewState("ClassID") = commandArgs
                Dim sSql As String = Database & "..spGetLLLClassParts__c @ClassID=" & commandArgs
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    radgridClassPart.DataSource = dt
                    radgridClassPart.DataBind()
                    radgridClassPart.Visible = True
                Else
                    radgridClassPart.Visible = False
                End If
            ElseIf e.CommandName.ToUpper = "Attachment".ToUpper Then
                'Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                'Dim filePath As String = commandArgs(0)
                'Dim fileExtension As String = "." + filePath.Split(CChar(".")).Last.ToLower
                'Dim dataTable As New DataTable()
                'Dim sql As String = "..spGetBlobData__c @EntityID=" & AptifyApplication.GetEntityID("Classes") & ",@RecordID=" & Convert.ToString(commandArgs(1))
                'dataTable = DataAction.GetDataTable(sql)
                'Dim FileData() As Byte
                'FileData = CType(dataTable.Rows(0)("BlobData"), Byte())

                'If ContentType(fileExtension) <> "" Then
                '    Response.Buffer = True
                '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                '    Response.ContentType = ContentType(fileExtension)
                '    Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(filePath))
                '    Response.BinaryWrite(FileData)
                '    Response.Flush()
                '    Response.End()

                'End If
                radDownloadDocuments.VisibleOnPageLoad = True
                Dim data As String() = e.CommandArgument.ToString().Split(CChar(";"))
                Dim recordID As Integer = Convert.ToInt64(data(0))
                Dim entityID As Integer = Convert.ToInt64(Me.AptifyApplication.GetEntityID("Classes"))
                ucDownload.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "PrePurchase Downloads")
                ucDownload.LoadAttachments(entityID, recordID, True)
            ElseIf e.CommandName.ToUpper = "DescOfCourseStructure".ToUpper Then
                radWindow.VisibleOnPageLoad = True
                lblMsg.Text = Convert.ToString(e.CommandArgument)
                radWindow.Title = "Web Description"
            ElseIf e.CommandName.ToUpper = "EntryCriteria".ToUpper Then
                radWindow.VisibleOnPageLoad = True
                lblMsg.Text = Convert.ToString(e.CommandArgument)
                radWindow.Title = "Entry Criteria"
            End If
            'Catch ex As Exception
            '    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            'End Try
        End Sub

        Protected Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
            radDownloadDocuments.VisibleOnPageLoad = False
        End Sub

        Protected Sub btnOK_Click(sender As Object, e As System.EventArgs) Handles btnOK.Click
            Try
                radWindow.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Function ContentType(ByVal fileExtension As String) As String
            Try
                Dim d As New Dictionary(Of String, String)
                'Images'
                d.Add(".bmp", "image/bmp")
                d.Add(".gif", "image/gif")
                d.Add(".jpeg", "image/jpeg")
                d.Add(".jpg", "image/jpeg")
                d.Add(".png", "image/png")
                d.Add(".tif", "image/tiff")
                d.Add(".tiff", "image/tiff")
                'Documents'
                d.Add(".doc", "application/msword")
                d.Add(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                d.Add(".pdf", "application/pdf")
                'Slideshows'
                d.Add(".ppt", "application/vnd.ms-powerpoint")
                d.Add(".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation")
                'Data'
                d.Add(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                d.Add(".xls", "application/vnd.ms-excel")
                d.Add(".csv", "text/csv")
                d.Add(".xml", "text/xml")
                d.Add(".txt", "text/plain")
                'Compressed Folders'
                d.Add(".zip", "application/zip")
                'Audio'
                d.Add(".ogg", "application/ogg")
                d.Add(".mp3", "audio/mpeg")
                d.Add(".wma", "audio/x-ms-wma")
                d.Add(".wav", "audio/x-wav")
                'Video'
                d.Add(".wmv", "audio/x-ms-wmv")
                d.Add(".swf", "application/x-shockwave-flash")
                d.Add(".avi", "video/avi")
                d.Add(".mp4", "video/mp4")
                d.Add(".mpeg", "video/mpeg")
                d.Add(".mpg", "video/mpeg")
                d.Add(".qt", "video/quicktime")
                ' Crystal Report
                d.Add(".rpt", "application/rpt")
                Return d(fileExtension)

            Catch ex As Exception
                Return ""
            End Try

        End Function

        Protected Sub BindOfficcerInfo_click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                radCalssParts.VisibleOnPageLoad = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub BtnWinOk_Click(sender As Object, e As System.EventArgs) Handles BtnWinOk.Click
            Try
                radCalssParts.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub radClassDetails_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles radClassDetails.ItemDataBound
            Try
                Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If TypeOf e.Item Is GridDataItem Then
                    Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        'populate the controls with data, in this case Im populating some text with the id of the parent row
                        TryCast(item.FindControl("lblCurrency"), Label).Text = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                    Else
                        TryCast(item.FindControl("lblCurrency"), Label).Text = "€"
                    End If

                    If ViewState("VisibleEnrollLink") = False Then
                        TryCast(item.FindControl("lnkEnrollment"), LinkButton).Visible = False
                    Else
                        TryCast(item.FindControl("lnkEnrollment"), LinkButton).Visible = True
                        If Not String.IsNullOrEmpty(item("ClosingDate").Text) Then
                            If item("ClosingDate").Text = "&nbsp;" Then
                                TryCast(item.FindControl("lnkEnrollment"), LinkButton).Visible = False
                            Else
                                If Convert.ToDateTime(item("ClosingDate").Text) >= Date.Today Then
                                    TryCast(item.FindControl("lnkEnrollment"), LinkButton).Visible = True
                                    ' Code added by Govind on 19th Jan 2016
                                    If TryCast(item.FindControl("lblCTC"), Label).Text.Trim.ToLower = "ctc integrated tax" Then
                                        Dim sSqlCTC As String = Database & "..spChkStudentCTCStageCertificate__c @StudentID=" & User1.PersonID
                                        Dim bCTCCertificate As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSqlCTC, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                        If bCTCCertificate = False Then
                                            TryCast(item.FindControl("lnkEnrollment"), LinkButton).Visible = False
                                        Else
                                            TryCast(item.FindControl("lnkEnrollment"), LinkButton).Visible = True
                                        End If
                                    End If
                                Else
                                    TryCast(item.FindControl("lnkEnrollment"), LinkButton).Visible = False
                                End If
                            End If
                        End If
                    End If
                    'If TryCast(item.FindControl("lblDesc"), Label).Text.Trim = "" Or TryCast(item.FindControl("lblDesc"), Label).Text.Trim = "&nbsp;" Then
                    '    TryCast(e.Item.FindControl("lnkDescOfCourseStructure"), LinkButton).Visible = False
                    '    TryCast(e.Item.FindControl("lblDesc"), Label).Visible = False
                    'Else
                    '    TryCast(e.Item.FindControl("lnkDescOfCourseStructure"), LinkButton).Visible = True
                    '    TryCast(e.Item.FindControl("lblDesc"), Label).Visible = False
                    'End If
                    If Not (item.FindControl("lblEntryCriteria") Is Nothing) Then
                        If TryCast(item.FindControl("lblEntryCriteria"), Label).Text.Trim = "" Or TryCast(item.FindControl("lblEntryCriteria"), Label).Text.Trim = "&nbsp;" Then

                            If Not (item.FindControl("lnkEntryCriteria") Is Nothing) Then
                                TryCast(e.Item.FindControl("lnkEntryCriteria"), LinkButton).Visible = False
                            End If

                            TryCast(e.Item.FindControl("lblEntryCriteria"), Label).Visible = False
                        Else
                            If Not (item.FindControl("lnkEntryCriteria") Is Nothing) Then
                                TryCast(e.Item.FindControl("lnkEntryCriteria"), LinkButton).Visible = True
                            End If

                            TryCast(e.Item.FindControl("lblEntryCriteria"), Label).Visible = False
                        End If
                        'ElseIf Not (item.FindControl("lnkEntryCriteria") Is Nothing) Then
                        '    TryCast(e.Item.FindControl("lnkEntryCriteria"), LinkButton).Visible = True
                    End If

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub radClassDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radClassDetails.NeedDataSource
            Try
                BindClassDetails(Convert.ToString(ViewState("CourseID")))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Function GetPrice(ByVal lProductID As Long) As String
            Try
                Dim oProductPriceINfo As Aptify.Applications.OrderEntry.IProductPrice.PriceInfo
                oProductPriceINfo = ShoppingCart1.GetUserProductPrice(lProductID, 1)
                Return Decimal.Round(oProductPriceINfo.Price, 2)

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return Convert.ToString(0)
            End Try
        End Function

        'Protected Function GetAttachment(ByVal lClassID As Long) As String
        '    Try
        '        Dim dataTable As New DataTable()
        '        Dim sql As String = "..spGetAllEntityRecordAttachments @EntityID=" & AptifyApplication.GetEntityID("Classes") & ",@RecordID=" & Convert.ToString(lClassID)
        '        dataTable = DataAction.GetDataTable(sql)



        '    Catch ex As Exception

        '    End Try
        'End Function

        Protected Sub radCourseDetails_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radCourseDetails.ItemCommand
            Try
                If e.CommandName.ToUpper = "Course".ToUpper Then
                    Dim commandArgs As String = e.CommandArgument.ToString()
                    ViewState("CourseID") = commandArgs
                    BindClassDetails(commandArgs)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub radCourseDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radCourseDetails.NeedDataSource
            Try
                BindCourseDetails()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub radgridClassPart_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radgridClassPart.NeedDataSource
            Try
                Dim sSql As String = Database & "..spGetLLLClassParts__c @ClassID=" & ViewState("ClassID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    radgridClassPart.DataSource = dt
                    radgridClassPart.DataBind()
                    radgridClassPart.Visible = True
                Else
                    radgridClassPart.Visible = False
                End If
            Catch ex As Exception

            End Try
        End Sub
#End Region


    End Class
End Namespace
