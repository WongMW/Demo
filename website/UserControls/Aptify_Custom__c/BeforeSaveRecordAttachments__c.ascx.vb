'Aptify e-Business 5.5.1, July 2013
Option Explicit On
Option Strict On
Imports System.Data
Imports System.IO

Namespace Aptify.Framework.Web.eBusiness

    ''' <summary>
    ''' This user control allows a web user to view, edit, add, and delete attachments
    ''' </summary>
    ''' <remarks></remarks>
    Partial Class BeforeSaveRecordAttachments__c
        Inherits eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_SMALL_ATTACHMENT_IMAGE_URL As String = "SmallAttachmentImage"
        Protected Const ATTRIBUTE_SMALL_DELETE_ATTACHMENT_IMAGE_URL As String = "SmallDeleteAttachmentImage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "RecordAttachments__c"

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

        Public Overridable Property Filepath() As String
            Get
                If Not ViewState("filepath") Is Nothing Then
                    Return CStr(ViewState("filepath"))
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState("filepath") = value
            End Set
        End Property
        Public Overridable Property Discription() As String
            Get
                If Not ViewState("Discription") Is Nothing Then
                    Return CStr(ViewState("Discription"))
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState("Discription") = value
            End Set
        End Property
        Public Overridable Property ValidationGroup() As String
            Get
                If Not ViewState("ValidationGroup") Is Nothing Then
                    Return CStr(ViewState("ValidationGroup"))
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                ViewState("ValidationGroup") = value
                'cvUpload.ValidationGroup = value  ' code commented by GM for Redmine #20181
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

        Public Overridable Property CategoryID() As Long
            Get
                If Not ViewState("CategoryID") Is Nothing Then
                    Return CLng(ViewState("CategoryID"))
                Else
                    Return -1
                End If
            End Get
            Set(ByVal value As Long)
                ViewState("CategoryID") = value
            End Set
        End Property

        Public Overridable Property NewMember() As Boolean
            Get
                If Not ViewState("_NewMember") Is Nothing Then
                    Return CBool(ViewState("_NewMember"))
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                ViewState("_NewMember") = value
                '  trNewGrid.Visible = value
            End Set
        End Property

        Public Overridable ReadOnly Property grdcount() As Integer
            Get
                Return CInt(grdAttachments.Rows.Count)
            End Get
        End Property

#End Region



        ''' <summary>
        ''' This method will load up the all associated attachments for a given entity  
        ''' record based on the provided parameters.
        ''' </summary>
        ''' <param name="EntityName">Name of the Entity currently being used.</param>
        ''' <param name="RecordID">ID of the Entity's Record currently using.</param>
        ''' <remarks></remarks>
        Public Overridable Sub LoadAttachments(ByVal EntityName As String, ByVal RecordID As Long, ByVal CategoryID As Long)
            Me.LoadAttachments(Me.AptifyApplication.GetEntityID(EntityName), RecordID, CategoryID)
        End Sub

        Public Function SaveAttachment() As Boolean
            Dim Success As Boolean = False
            Try
                Dim i As Integer
                ' If NewMember = True Then

                If grdAttachments.Rows.Count > 0 Then
                    If Session("SessionID").ToString() = ViewState("SessionID").ToString() Then
                        Dim sName As String = ""
                        'If FileUpload1.HasFile Then
                        '  Dim sName As String = GetFileName(FileUpload1.PostedFile.FileName)
                        Dim oAttach As New Aptify.Framework.Application.AptifyAttachments(Me.AptifyApplication, Me.Entity, Me.RecordID)
                        Dim sFile As String = ""
                        ' sFile = System.Environment.GetEnvironmentVariable("TEMP") & "\" & sName
                        For i = 0 To grdAttachments.Rows.Count - 1
                            sName = grdAttachments.Rows(i).Cells(2).Text
                            sFile = System.Environment.GetEnvironmentVariable("TEMP") & "\" & sName
                            '' KillFile(sFile)
                            '  Dim oAttach As New Aptify.Framework.Application.AptifyAttachments((Me.AptifyApplication, Me.Entity, Me.RecordID)
                            ''  FileUpload1.PostedFile.SaveAs(sFile)

                            'Issue 4348 - 12/06/2006 MAS
                            'reassign the session a new timestamp
                            Session("SessionID") = Server.UrlEncode(System.DateTime.Now.ToString())
                            'DeleteAttachment()
                            If oAttach.AddAttachment(sFile, CategoryID, "") Then
                            End If
                            'Added If consition as part of #20288
                            If Not Session("Submitting") Is Nothing AndAlso Session("Submitting") Is "yes" Then
                                KillFile(sFile) '' As part of log #20308
                            End If
                        Next
                    Else
                        Me.LoadAttachments(Me.EntityID, Me.RecordID, CategoryID)
                    End If

                End If
                DeleteRecords()
                'Else

                'End If
                ' Return Success
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Success = False
            End Try
            'Return Success
        End Function
        ''' <summary>
        ''' This method will load up the all associated attachments for a given entity  
        ''' record based on the provided parameters.
        ''' </summary>
        ''' <param name="EntityID">ID of the Entity currently being used.</param>
        ''' <param name="RecordID">ID of the Entity's Record currently using.</param>
        ''' <remarks></remarks>
        ''' 
        Public Function SetValues() As Boolean
            Dim Success As Boolean = False
            Try


                If FileUpload1.HasFile Then
                    Dim sFile As String, sName As String = GetFileName(FileUpload1.PostedFile.FileName)
                    'Dim oAttach As New Aptify.Framework.Application.AptifyAttachments(Me.AptifyApplication, Me.Entity, Me.RecordID)

                    sFile = System.Environment.GetEnvironmentVariable("TEMP") & "\" & sName
                    'KillFile(sFile)
                    FileUpload1.PostedFile.SaveAs(sFile)
                    Filepath = sFile
                    Discription = ""
                    'Issue 4348 - 12/06/2006 MAS
                    'reassign the session a new timestamp
                    Session("SessionID") = Server.UrlEncode(System.DateTime.Now.ToString())

                End If


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

            End Try

        End Function

        Public Overridable Sub LoadAttachments(ByVal EntityID As Long, ByVal RecordID As Long, ByVal CategoryID As Long)
            Try
                ' Changes made on 11-21-2007
                ' Vijay Sitlani integrated the changes made by Amith for Issue 5325 and also made minor changes.

                Me.SetProperties()

                Dim sSQL As String, dt As Data.DataTable
                Dim sBaseURL As String
                sBaseURL = "https://" & Request.Url.Host & ":" & Request.Url.Port & Request.ApplicationPath
                Me.EntityID = EntityID
                Me.RecordID = RecordID
                Me.CategoryID = CategoryID
                'CP Removed check for file size bc base view for attachments changed to vwAttachmentsWithoutBLob and vwAttachments only available to administrators
                'sSQL = "SELECT ID,Name,Description,ISNULL(DataLength(BlobData),0)/1024 FileSize,DateCreated,DateUpdated,'' EncryptedID,'' EncryptedURL FROM " & _
                sSQL = "SELECT ID,Name,Description,BlobSize * 0.0009765625 FileSize ,DateCreated,DateUpdated,'' EncryptedID,'' EncryptedURL FROM " & _
                        Database & "..vwAttachmentsWithoutBLOB " & _
                        " WHERE RecordID=" & RecordID & " " & _
                        " AND EntityID=" & EntityID & " " & " AND CategoryID=" & CategoryID
                dt = Me.DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

                If Not dt Is Nothing Then
                    For Each dr As DataRow In dt.Rows
                        dr("EncryptedID") = HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(dr("ID").ToString))
                        dr("EncryptedURL") = "javascript:_do_window_open('" & sBaseURL & _
                            "/Handlers/AptifyAttachmentHandler.ashx?AttachmentID=" & _
                        dr("EncryptedID").ToString & "');"
                    Next

                    Me.trGrid.Visible = dt.Rows.Count > 0
                    'Navin Prasad Issue 11032

                    'With CType(grdAttachments.Columns(2), HyperLinkField)
                    '    ' .DataTextFormatString = "<img src=""" & SmallAttachmentImage & """ alt=""View File"" border=""0"" />"
                    '    .HeaderImageUrl = SmallAttachmentImage

                    'End With

                    grdAttachments.Columns(0).Visible = False ' never show the ID column
                    grdAttachments.Columns(1).Visible = Me.AllowDelete
                    'Navin Prasad Issue 11032

                    'If Me.AllowDelete Then
                    '    'Anil B for issues 144499 on 05-04-2013
                    '    'Set Image to Grid Header
                    '    With CType(grdAttachments.Columns(6), Telerik.Web.UI.GridTemplateColumn)
                    '        '.Text = "<img src=""" & SmallDeleteAttachmentImage & """ alt=""Delete Attachment"" border=""0"" />"
                    '        .HeaderImageUrl = SmallDeleteAttachmentImage
                    '    End With
                    'Else
                    '    grdAttachments.Columns(6).Visible = False
                    'End If
                    grdAttachments.Columns(2).HeaderImageUrl = SmallAttachmentImage
                    If Me.AllowDelete Then
                        With CType(grdAttachments.Columns(1), ButtonField)
                            .Text = "Delete" '"<img src=""" & SmallDeleteAttachmentImage & """ alt=""Delete"" border=""0"" />"
                            '.HeaderImageUrl = SmallDeleteAttachmentImage
                        End With
                    End If
                    grdAttachments.DataSource = dt
                    grdAttachments.DataBind()

                    'For Each row As Telerik.Web.UI.GridDataItem In grdAttachments.Items
                    '    Dim lnk As HyperLink = CType(row.FindControl("lblFileImage"), HyperLink)
                    '    lnk.ImageUrl = SmallAttachmentImage
                    '    'Dim btn As Telerik.Web.UI.RadButton = CType(row.FindControl("btn"), Telerik.Web.UI.RadButton)
                    '    'lnk.ImageUrl = SmallAttachmentImage
                    'Next


                Else
                    Me.trGrid.Visible = False
                End If

                trAdd.Visible = Me.AllowAdd
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub



        Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            Try
                'Issue 4348 - 12/06/2006 MAS
                'make sure this is a valid entry and not a refresh
                If Session("SessionID").ToString() = ViewState("SessionID").ToString() Then
                    If FileUpload1.HasFile Then
                        Dim sFile As String, sName As String = GetFileName(FileUpload1.PostedFile.FileName)
                        ' If Condition added by GM for Redmine #20181
                        If Me.Entity = "Abatements__c" Then
                            Me.RecordID = -1
                        End If
                        'End  Redmine #20181
                        Dim oAttach As New Aptify.Framework.Application.AptifyAttachments(Me.AptifyApplication, Me.Entity, Me.RecordID)

                        sFile = System.Environment.GetEnvironmentVariable("TEMP") & "\" & sName
                        ''KillFile(sFile)
                        FileUpload1.PostedFile.SaveAs(sFile)

                        Session("SessionID") = Server.UrlEncode(System.DateTime.Now.ToString())

                        sName = "'" & sName & "'"

                        Dim sSql As String = Database & "..spGetInfoforAttachment__c @EntityID=" & Me.EntityID & ",@RecordID=" & RecordID & ",@Name=" & sName
                        ' Dim sSql As String = Database & "..spGetInfoforAttachment__c @EntityID=" & Me.EntityID & ",@RecordID=" & RecordID & ",@Name=" & sName
                        Dim dt As DataTable = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

                        If dt.Rows.Count <= 0 Then
                            If oAttach.AddAttachment(sFile, Me.CategoryID, "") Then
                                ' txtDescription.Text = ""
                                Me.LoadAttachments(Me.EntityID, Me.RecordID, CategoryID)
                                '' KillFile(sFile)
                            Else
                                Throw New Exception("Error Attaching File")
                            End If

                            ' End If
                            lblFileExist.Visible = False
                        Else
                            ' If Condition added by GM for Redmine #20181
                            If Me.Entity = "Abatements__c" Then
                                If oAttach.AddAttachment(sFile, Me.CategoryID, "") Then
                                    ' txtDescription.Text = ""
                                    Me.LoadAttachments(Me.EntityID, Me.RecordID, CategoryID)
                                    '' KillFile(sFile)
                                Else
                                    Throw New Exception("Error Attaching File")
                                End If
                            Else
                                lblFileExist.Visible = True
                                lblFileExist.Text = "File already exists, please use different name."
                                lblFileExist.ForeColor = Drawing.Color.Red
                            End If
                            'End  Redmine #20181
                        End If
                        Session("SessionID") = Server.UrlEncode(System.DateTime.Now.ToString())

                    End If
                Else
                    Me.LoadAttachments(Me.EntityID, Me.RecordID, CategoryID)
                End If

                ' Page.Form.DefaultFocus = trAdd.FindControl("FileUpload1").ClientID
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
            'If grdAttachments.Rows.Count > 0 Then
            '    RequiredFieldValidator1.Enabled = False
            'Else
            '    RequiredFieldValidator1.Enabled = True
            'End If
            If Not Page.IsPostBack Then
                'Issue 4348 - 12/06/2006 MAS
                'track this session to disable refresh by entering a unique identifier (timestamp) in the client-side session (cookie)
                'If grdAttachments.Rows.Count > 0 Then
                '    RequiredFieldValidator1.Enabled = False
                'Else
                '    RequiredFieldValidator1.Enabled = True
                'End If

                Session("SessionID") = Server.UrlEncode(System.DateTime.Now.ToString())
                CategoryID = Me.CategoryID
                '  DeleteRecords()
                If Me.RecordID > 0 Then
                    Me.LoadAttachments(Me.EntityID, Me.RecordID, CategoryID)
                Else
                    DeleteRecords()
                End If

            End If
        End Sub

        Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
            'Issue 4348 - 12/06/2006 MAS
            'Update the server-side SessionID
            ViewState("SessionID") = Session("SessionID")
        End Sub
        ''' <summary>
        ''' Nalini 12436 date:1/12/2011
        ''' </summary>
        ''' <remarks></remarks>
        'Protected Sub grdAttachments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAttachments.PageIndexChanging
        '    ''grdAttachments.PageIndex = e.NewPageIndex
        '    Me.LoadAttachments(Me.EntityID, Me.RecordID)
        'End Sub

        'Navin Prasad Issue 11032

        'Protected Sub grdAttachments_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAttachments.RowCommand
        '    If e.CommandName = "Delete" Then
        '        Try
        '            Dim lID As Long
        '            Dim oAttach As New Aptify.Framework.Application.AptifyAttachments(Me.AptifyApplication, Me.Entity, Me.RecordID)
        '            lID = CLng(CType(grdAttachments.Rows(CInt(e.CommandArgument)).FindControl("lblID"), Label).Text)
        '            oAttach.DeleteAttachment(lID)
        '            If Me.CategoryID >= -1 Then
        '                Me.LoadAttachments(Me.EntityID, Me.RecordID, Me.CategoryID)
        '                'Else
        '                '    Me.LoadAttachments(Me.EntityID, Me.RecordID)
        '            End If
        '        Catch ex As Exception
        '            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '        End Try
        '    End If
        'End Sub

        Private Function GetAttcahmentCategoryID(Name As String) As Long
            Dim sSQL As String = String.Empty
            Dim params(0) As IDataParameter
            Dim dt As DataTable = Nothing
            Dim CategoryID As Long = -1
            Try
                sSQL = Database & "..spGetAttchmentCategoryID__c"
                params(0) = Me.DataAction.GetDataParameter("@Name", SqlDbType.VarChar, Name)
                dt = Me.DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, params)
                If dt.Rows.Count > 0 Then
                    CategoryID = CLng(dt.Rows(0)("ID"))
                End If
                Return CategoryID
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return CategoryID
            End Try
        End Function

        Public Sub DeleteRecords()
            '  Dim sSql As String = Database & "..spGetDeleteAttachmentID__c @EntityID=3121,@CategoryID=40,@RecordID=-1"
            Dim sSql As String = Database & "..spGetInfoforDeleteAttachment__c @EntityID=" & Me.EntityID & ",@CategoryID=" & CategoryID & ",@Record=" & -1
            Dim dt As DataTable = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            '  Dim sSql As String = "select ID from vwAttachmentsWithoutBLOB where EntityID=2131 and CategoryID=41 and RecordID=-1"
            ' Dim dt As DataTable = DataAction.GetDataTable(sSql, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            Dim i As Integer = 0
            Dim oAttach As New Aptify.Framework.Application.AptifyAttachments(Me.AptifyApplication, Me.Entity, -1)
            For i = 0 To dt.Rows.Count - 1

                oAttach.DeleteAttachment(CLng(dt.Rows(i)("ID")))

                Me.LoadAttachments(Me.EntityID, Me.RecordID, CategoryID)
            Next


        End Sub

        'Protected Sub grdAttachData_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdAttachData.RowDeleting
        '    Try
        '        ' Me.lblRegistrationResult.Visible = False
        '        If Session("dtAttachData") IsNot Nothing Then
        '            Dim dtTemp As DataTable = CType(Session("dtAttachData"), DataTable)
        '            dtTemp.Rows.RemoveAt(e.RowIndex)


        '            If Session("dtAttachData") Is Nothing Then
        '                Session.Add("dtAttachData", dtTemp)
        '            Else
        '                Session("dtAttachData") = dtTemp
        '            End If

        '            If dtTemp.Rows.Count > 0 Then

        '                grdAttachData.DataSource = dtTemp
        '                grdAttachData.DataBind()
        '            Else
        '                grdAttachData.DataSource = Nothing
        '                grdAttachData.DataBind()
        '            End If
        '        Else
        '            grdAttachData.DataSource = Nothing
        '            grdAttachData.DataBind()

        '        End If
        '    Catch ex As Exception

        '    End Try
        'End Sub

        Protected Sub grdAttachments_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAttachments.RowCommand
            If e.CommandName = "Delete" Then
                Try
                    Dim lID As Long
                    Dim oAttach As New Aptify.Framework.Application.AptifyAttachments(Me.AptifyApplication, Me.Entity, Me.RecordID)
                    lID = CLng(CType(grdAttachments.Rows(CInt(e.CommandArgument)).FindControl("lblID"), Label).Text)
                    'Added as part of log #20308
                    Dim sFile As String = ""
                    Dim sName As String = grdAttachments.Rows(CInt(e.CommandArgument)).Cells(2).Text
                    sFile = System.Environment.GetEnvironmentVariable("TEMP") & "\" & sName
                    KillFile(sFile)
                    'End Of log #20308 changes
                    Dim b As Boolean = oAttach.DeleteAttachment(lID)
                    If Me.CategoryID > -1 Then
                        Me.LoadAttachments(Me.EntityID, Me.RecordID, Me.CategoryID)
                        'Else
                        '    Me.LoadAttachments(Me.EntityID, Me.RecordID)
                    End If
                    ' Code added by GM for Redmine #20181
                    Me.LoadAttachments(Me.EntityID, Me.RecordID, -1)
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            End If
        End Sub
        Protected Sub grdAttachments_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdAttachments.RowDeleting

        End Sub
        Protected Sub FileUpload1_Validate(ByVal source As Object, ByVal args As ServerValidateEventArgs)
            Dim rowCount As Integer
            rowCount = grdAttachments.Rows.Count
            If rowCount = 0 Then
                args.IsValid = False
            End If
        End Sub
    End Class
End Namespace
