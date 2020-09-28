'Aptify e-Business 5.5.1, July 2013
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer              Date Created/Modified               Summary
'Rajesh Kardile           04/19/2014                Gets/Sets the Attachment Category.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On
Imports System.Data
Imports AttachmentService__c
Namespace Aptify.Framework.Web.eBusiness

    ''' <summary>
    ''' This user control allows a web user to view, edit, add, and delete attachments
    ''' </summary>
    ''' <remarks></remarks>
    Partial Class LLLRecordAttachments__c
        Inherits eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_SMALL_ATTACHMENT_IMAGE_URL As String = "SmallAttachmentImage"
        Protected Const ATTRIBUTE_SMALL_DELETE_ATTACHMENT_IMAGE_URL As String = "SmallDeleteAttachmentImage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "RecordAttachments"


#Region "RecordAttachments Specific Properties"
        ''' <summary>
        ''' SmallAttachmentImage url
        ''' </summary>
        Public Overridable Property SmallAttachmentImage() As String
            Get
                If Not ViewState(ATTRIBUTE_SMALL_ATTACHMENT_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_SMALL_ATTACHMENT_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_SMALL_ATTACHMENT_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' SmallDeleteAttachmentImage url
        ''' </summary>
        Public Overridable Property SmallDeleteAttachmentImage() As String
            Get
                If Not ViewState(ATTRIBUTE_SMALL_DELETE_ATTACHMENT_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_SMALL_DELETE_ATTACHMENT_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_SMALL_DELETE_ATTACHMENT_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets/Sets the Entity ID
        ''' </summary>
        Public Overridable Property EntityID() As Long
            Get
                If Not ViewState("EntityID") Is Nothing Then
                    Return CLng(ViewState("EntityID"))
                Else
                    Return -1
                End If
            End Get
            Set(ByVal value As Long)
                ViewState("EntityID") = value
            End Set
        End Property

        ''' <summary>
        ''' Gets/Sets the Record ID
        ''' </summary>
        Public Overridable Property RecordID() As Long
            Get
                If Not ViewState("RecordID") Is Nothing Then
                    Return CLng(ViewState("RecordID"))
                Else
                    Return -1
                End If
            End Get
            Set(ByVal value As Long)
                ViewState("RecordID") = value
            End Set
        End Property

        ''' <summary>
        ''' Returns the Entity Name associated with the EntityID property
        ''' </summary>
        Public Overridable ReadOnly Property Entity() As String
            Get
                If EntityID > 0 Then
                    Return Me.AptifyApplication.GetEntityName(EntityID)
                Else
                    Return ""
                End If
            End Get
        End Property

        Public Overridable ReadOnly Property FoundAttachment() As Boolean
            Get
                If Not ViewState("FoundAttachment") Is Nothing AndAlso Convert.ToInt32(ViewState("FoundAttachment")) = 1 Then
                    Return True
                Else
                    Return False
                End If
            End Get

        End Property

        ''' <summary>
        ''' Determines if the user is allowed to view attachments or not
        ''' </summary>
        <System.ComponentModel.DefaultValue(True)> _
        Public Overridable Property AllowView() As Boolean
            Get
                If Not ViewState("_AllowView") Is Nothing Then
                    Return CBool(ViewState("_AllowView"))
                Else
                    Return True
                End If
            End Get
            Set(ByVal value As Boolean)
                ViewState("_AllowView") = value
                grdAttachments.Visible = value
            End Set
        End Property
        ''' <summary>
        ''' Determines if the user is allowed to add new attachments 
        ''' </summary>
        Public Overridable Property AllowAdd() As Boolean
            Get
                If Not ViewState("_AllowAdd") Is Nothing Then
                    Return CBool(ViewState("_AllowAdd"))
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                ViewState("_AllowAdd") = value
                trAdd.Visible = value
            End Set
        End Property

        ''' <summary>
        ''' Determines if the user is allowed to add new attachments 
        ''' </summary>
        Public Overridable Property ViewDescription() As Boolean
            Get
                If Not ViewState("_ViewDescription") Is Nothing Then
                    Return CBool(ViewState("_ViewDescription"))
                Else
                    Return True
                End If
            End Get
            Set(ByVal value As Boolean)
                ViewState("_ViewDescription") = value
            End Set
        End Property

        ''' <summary>
        ''' Determines if the user is allowed to delete existing attachments 
        ''' </summary>
        Public Overridable Property AllowDelete() As Boolean
            Get
                If Not ViewState("_AllowDelete") Is Nothing Then
                    Return CBool(ViewState("_AllowDelete"))
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                ViewState("_AllowDelete") = value
            End Set
        End Property

        ''' <summary>
        ''' Gets/Sets the Attachment Category
        ''' </summary>
        Public Overridable Property AttachmentCategory() As Long
            Get
                If Not ViewState("AttachmentCategory") Is Nothing Then
                    Return CLng(ViewState("AttachmentCategory"))
                Else
                    Return -1
                End If
            End Get
            Set(ByVal value As Long)
                ViewState("AttachmentCategory") = value
            End Set
        End Property

        Public Overridable Property ShowDownload() As Boolean
            Get
                If Not ViewState("_AllowDownLoad") Is Nothing Then
                    Return CBool(ViewState("_AllowDownLoad"))
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                ViewState("_AllowDownLoad") = value
            End Set
        End Property

#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(SmallAttachmentImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                SmallAttachmentImage = Me.GetLinkValueFromXML(ATTRIBUTE_SMALL_ATTACHMENT_IMAGE_URL)
            End If
            If String.IsNullOrEmpty(SmallDeleteAttachmentImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                SmallDeleteAttachmentImage = Me.GetLinkValueFromXML(ATTRIBUTE_SMALL_DELETE_ATTACHMENT_IMAGE_URL)
            End If
        End Sub

        ''' <summary>
        ''' This method will load up the all associated attachments for a given entity  
        ''' record based on the provided parameters.
        ''' </summary>
        ''' <param name="EntityName">Name of the Entity currently being used.</param>
        ''' <param name="RecordID">ID of the Entity's Record currently using.</param>
        ''' <remarks></remarks>
        Public Overridable Sub LoadAttachments(ByVal EntityName As String, ByVal RecordID As Long, Optional ByVal isCategoryMandatory As Boolean = False)
            Me.LoadAttachments(Me.AptifyApplication.GetEntityID(EntityName), RecordID, isCategoryMandatory)
        End Sub

        ''' <summary>
        ''' This method will load up the all associated attachments for a given entity  
        ''' record based on the provided parameters.
        ''' </summary>
        ''' <param name="EntityID">ID of the Entity currently being used.</param>
        ''' <param name="RecordID">ID of the Entity's Record currently using.</param>
        ''' <remarks></remarks>

        Public Overridable Sub LoadAttachments(ByVal EntityID As Long, ByVal RecordID As Long, Optional ByVal IsCategoryMandatory As Boolean = False)
            Try
                ' Changes made on 11-21-2007
                ' Vijay Sitlani integrated the changes made by Amith for Issue 5325 and also made minor changes.

                Me.SetProperties()

                Dim sSQL As String, dt As Data.DataTable
                Dim sBaseURL As String
                sBaseURL = "http://" & Request.Url.Host & ":" & Request.Url.Port & Request.ApplicationPath
                Me.EntityID = EntityID
                Me.RecordID = RecordID

                'CP Removed check for file size bc base view for attachments changed to vwAttachmentsWithoutBLob and vwAttachments only available to administrators
                'sSQL = "SELECT ID,Name,Description,ISNULL(DataLength(BlobData),0)/1024 FileSize,DateCreated,DateUpdated,'' EncryptedID,'' EncryptedURL FROM " & _
                sSQL = "SELECT ID,Name,Description,BlobSize * 0.0009765625 FileSize ,DateCreated,DateUpdated,'' EncryptedID,'' EncryptedURL FROM " & _
                        Database & "..vwAttachmentsWithoutBLOB " & _
                        " WHERE RecordID=" & RecordID & " " & _
                        " AND EntityID=" & EntityID

                'Rajesh Kardile - 04/19/2014 -If Attachment category is set.
                'Milind Sutar - 02/18/2015 - If category check is mandatory
                If (AttachmentCategory > 0) OrElse (IsCategoryMandatory = True AndAlso AttachmentCategory = -1) Then
                    sSQL = sSQL + " AND CategoryID = " & AttachmentCategory
                End If
                dt = Me.DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

                If Not dt Is Nothing Then
                    For Each dr As DataRow In dt.Rows
                        dr("EncryptedID") = HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(dr("ID").ToString))
                        dr("EncryptedURL") = "javascript:_do_window_open('" & sBaseURL & _
                            "/Handlers/AptifyAttachmentHandler.ashx?AttachmentID=" & _
                        dr("EncryptedID").ToString & "');"
                    Next

                    'Me.trGrid.Visible = dt.Rows.Count > 0
                    'Navin Prasad Issue 11032

                    'With CType(grdAttachments.Columns(2), HyperLinkField)
                    '    ' .DataTextFormatString = "<img src=""" & SmallAttachmentImage & """ alt=""View File"" border=""0"" />"
                    '    .HeaderImageUrl = SmallAttachmentImage

                    'End With
                    grdAttachments.Columns(0).Visible = False ' never show the ID column
                    ' grdAttachments.Columns(1).Visible = Me.AllowDelete
                    'Navin Prasad Issue 11032
                    grdAttachments.Columns(3).Visible = Me.ViewDescription
                    If Me.AllowDelete Then
                        'Anil B for issues 144499 on 05-04-2013
                        'Set Image to Grid Header
                        With CType(grdAttachments.Columns(6), Telerik.Web.UI.GridTemplateColumn)
                            '.Text = "<img src=""" & SmallDeleteAttachmentImage & """ alt=""Delete Attachment"" border=""0"" />"
                            .HeaderImageUrl = SmallDeleteAttachmentImage
                        End With
                    Else
                        grdAttachments.Columns(6).Visible = False
                    End If
                    grdAttachments.Columns(2).HeaderImageUrl = SmallAttachmentImage

                    grdAttachments.Columns(7).Visible = Me.ShowDownload

                    grdAttachments.DataSource = dt
                    grdAttachments.DataBind()

                    For Each row As Telerik.Web.UI.GridDataItem In grdAttachments.Items
                        Dim lnk As HyperLink = CType(row.FindControl("lblFileImage"), HyperLink)
                        lnk.ImageUrl = SmallAttachmentImage
                        'Dim btn As Telerik.Web.UI.RadButton = CType(row.FindControl("btn"), Telerik.Web.UI.RadButton)
                        'lnk.ImageUrl = SmallAttachmentImage
                    Next
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        ViewState("FoundAttachment") = 1
                    End If
                Else
                    'Me.trGrid.Visible = False
                    ViewState("FoundAttachment") = 0
                End If

                trAdd.Visible = Me.AllowAdd
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            Try
                If Validate() Then
                    If Session("SessionID").ToString() = ViewState("SessionID").ToString() Then
                        If FileUpload1.HasFile Then
                            Dim sFile As String, sName As String = GetFileName(FileUpload1.PostedFile.FileName)
                            Dim oAttach As New Aptify.Framework.Application.AptifyAttachments(Me.AptifyApplication, Me.Entity, Me.RecordID)
                            sFile = System.Environment.GetEnvironmentVariable("TEMP") & "\" & sName
                            KillFile(sFile)
                            FileUpload1.PostedFile.SaveAs(sFile)
                            Session("SessionID") = Server.UrlEncode(System.DateTime.Now.ToString())
                            If oAttach.AddAttachment(sFile, Me.AttachmentCategory, txtDescription.Text) Then
                                txtDescription.Text = String.Empty
                                Me.LoadAttachments(Me.EntityID, Me.RecordID)
                                KillFile(sFile)
                                lblMessage.Text = "Attachment Uploaded successfully."
                                chkCheck.Enabled = False
                                lblMessage.ForeColor = Drawing.Color.Green
                                If Me.Entity = "ClassRegistrationPartStatus" Then
                                    Dim partStatus = Me.AptifyApplication.GetEntityObject("ClassRegistrationPartStatus", Me.RecordID)
                                    partStatus.SetValue("Status", "Submitted Electronically")
                                    partStatus.SetValue("PlagiarismDeclaration__c", True)
                                    Dim result As Boolean = partStatus.Save()
                                End If
                            Else
                                lblMessage.Text = "Upload failed"
                                lblMessage.ForeColor = Drawing.Color.Red
                                Throw New Exception("Error Attaching File")
                            End If
                        End If
                    Else
                        txtDescription.Text = ""
                        Me.LoadAttachments(Me.EntityID, Me.RecordID)
                    End If
                    'Else
                    '    lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLAssignment.PlagiarizedCheck__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    '    lblMessage.ForeColor = Drawing.Color.Red
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' This method deletes the file that was temporarily uploaded to the server prior to being saved
        ''' in the database. This method wraps the System.IO.File.Delete() method with exception handling 
        ''' in the event that the method fails. The method returns True if the file was deleted succesfully,
        ''' and False if either the file didn't exist, or another exception occured during processing.
        ''' </summary>
        ''' <param name="Path">Path that was used to temporarily save the file before being copied to the database</param>
        ''' <remarks></remarks>
        Protected Overridable Function KillFile(ByVal Path As String) As Boolean
            If System.IO.File.Exists(Path) Then
                Try
                    System.IO.File.Delete(Path)
                    Return True
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    Return False
                End Try
            Else
                Return False
            End If
        End Function

        'This function is unnecessary. FileUpload1.FileName returns the filename only and does not
        'include the client's path: http://msdn2.microsoft.com/en-us/library/system.web.ui.webcontrols.fileupload.filename(VS.80).aspx
        Protected Overridable Function GetFileName(ByVal FullPath As String) As String
            If FullPath.Contains("\") Then
                Return FullPath.Substring(FullPath.LastIndexOf("\") + 1)
            ElseIf FullPath.Contains("/") Then
                Return FullPath.Substring(FullPath.LastIndexOf("/") + 1)
            Else
                Return FullPath
            End If
        End Function
        'Navin Prasad Issue 11032

        'Protected Sub grdAttachments_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdAttachments.DeleteCommand
        '    Try
        '        Dim lID As Long
        '        Dim oAttach As New Aptify.Framework.Application.AptifyAttachments(Me.AptifyApplication, Me.Entity, Me.RecordID)
        '        lID = CLng(e.Item.Cells(0).Text)
        '        oAttach.DeleteAttachment(lID)
        '        Me.LoadAttachments(Me.EntityID, Me.RecordID)
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not Page.IsPostBack Then
                'Issue 4348 - 12/06/2006 MAS
                'track this session to disable refresh by entering a unique identifier (timestamp) in the client-side session (cookie)
                Session("SessionID") = Server.UrlEncode(System.DateTime.Now.ToString())
            End If
        End Sub

        Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
            'Issue 4348 - 12/06/2006 MAS
            'Update the server-side SessionID
            ViewState("SessionID") = Session("SessionID")
        End Sub

        ' ''' <summary>
        ' ''' Nalini 12436 date:1/12/2011
        ' ''' </summary>
        ' ''' <remarks></remarks>
        'Protected Sub grdAttachments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAttachments.PageIndexChanging
        '    ''grdAttachments.PageIndex = e.NewPageIndex
        '    Me.LoadAttachments(Me.EntityID, Me.RecordID)
        'End Sub

        'Navin Prasad Issue 11032

        Protected Sub grdAttachments_RowCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdAttachments.ItemCommand
            If e.CommandName = "Download" Or e.CommandName = "DownloadFile" Then

                Dim data() As String

                data = e.CommandArgument.ToString().Split(CChar(";"))
                Dim fileName As String = data(0)
                Dim fileExtension As String = "." + fileName.Split(CChar(".")).Last.ToLower

                Dim dataTable As New DataTable()
                Dim sql As String = "..spGetBlobData__c @EntityID=" & Me.EntityID & ",@RecordID=" & Me.RecordID
                dataTable = DataAction.GetDataTable(sql)

                Dim FileData() As Byte
                FileData = CType(dataTable.Rows(0)("BlobData"), Byte())

                'Dim fs As New System.IO.FileStream(Server.MapPath("\ebusiness\Temp"), IO.FileMode.Create, IO.FileAccess.Write)
                'fs.Write(FileData, 0, FileData.Length)
                'fs.Close()               
                If Me.ContentType(fileExtension) <> String.Empty Then
                    Response.Buffer = True
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = ContentType(fileExtension)
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName)
                    Response.BinaryWrite(FileData)
                    Response.Flush()
                    Response.End()
                End If
            End If
            If e.CommandName = "Delete" Then
                Try
                    lblMessage.Text = ""
                    Dim lID As Long
                    Dim oAttach As New Aptify.Framework.Application.AptifyAttachments(Me.AptifyApplication, Me.Entity, Me.RecordID)
                    lID = CLng(CType(grdAttachments.Items(CInt(e.CommandArgument)).FindControl("lblID"), Label).Text)
                    oAttach.DeleteAttachment(lID)
                    Me.LoadAttachments(Me.EntityID, Me.RecordID)
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            End If
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
                Return String.Empty
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function


        Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
            lblMessage.Text = String.Empty
            Session("IsCancelled") = 1
        End Sub
        ''Added By Pradip To Validate Plagiarized Checkbox
        Public Function Validate() As Boolean
            Try
                If Not FileUpload1.HasFile Then
                    lblMessage.Text = "Please Select File To Upload"
                    lblMessage.ForeColor = Drawing.Color.Red
                    Return False
                End If

                If Me.AllowAdd And chkCheck.Checked = False Then
                    lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLAssignment.PlagiarizedCheck__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblMessage.ForeColor = Drawing.Color.Red
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return True
        End Function
    End Class
End Namespace