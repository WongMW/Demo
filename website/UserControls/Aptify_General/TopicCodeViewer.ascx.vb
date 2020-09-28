Option Explicit On
Option Strict On

Imports Aptify.Framework.BusinessLogic.GenericEntity

Imports Aptify.Framework
Imports Aptify.Framework.DataServices
Imports System.Data
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness
    Partial Class TopicCodeViewer
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "TopicCodeViewer"
        Protected Const GLOBAL_OPTOUT_TOPIC_CODE_NAME As String = "suppression list"
        Protected Const GLOBAL_OPTOUT_SSQL As String = "SELECT ID,ParentID,Name,WebEnabled " +
                                         "FROM {0}..{1} WHERE Name like '%{4}%' AND ( Status='Active'  AND (ID IN (" +
                                         "SELECT TopicCodeID " +
                                         "FROM {0}..{2} " +
                                         "WHERE EntityID_Name ='{3}' AND Status = 'Active' AND WebEnabled =1)))"

        Dim globalOptOutID As Integer = -1

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            lblUser.Text = User1.FirstName & " " & User1.LastName & ", "
            SetProperties()
            Try

                If Not IsPostBack() Then
                    If Val(lblEntityID.Text) > 0 Then
                        '' HierarchyTree1.LoadTree()
                        ''CheckLevel(HierarchyTree1.Nodes)
                        LoadTree()

                        trvTopicCodes.ExpandAllNodes()
                    End If
                    If _ButtonDisplay = True Then
                        cmdSave.Visible = False
                        cmdCheckAll.Visible = False
                        cmdClearAll.Visible = False
                    End If
                    If _lbldispaly = False Then
                        tdtopic.Visible = False
                        tdtopiccode.Visible = False
                        lblDescription.Visible = False
                    End If
                End If
                ''HierarchyTree1.DataBind()
                lblDescription.Text = ""
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub



#Region "Public Members"
        ''' <summary>
        ''' Entity Name
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property EntityName() As String
            Get
                Return lblEntityName.Text
            End Get
            Set(ByVal Value As String)
                Try
                    lblEntityName.Text = Value
                    lblEntityID.Text = CStr(AptifyApplication.GetEntityID(Value))
                    ''HierarchyTree1.HiddenFilter = " Status='Active' AND (ID IN (SELECT TopicCodeID FROM " & _
                    'Database & "..vwTopicCodeEntities WHERE EntityID_Name = '" & _
                    'lblEntityName.Text & "' AND Status = 'Active'))"
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            End Set
        End Property

        Public Property RecordID() As Long
            Get
                Return CLng(lblRecordID.Text)
            End Get
            Set(ByVal Value As Long)
                lblRecordID.Text = CStr(Value)
            End Set
        End Property
        Private _ButtonDisplay As Boolean
        Public Property ButtonDisplay As Boolean
            Get
                Return _ButtonDisplay
            End Get
            Set(ByVal value As Boolean)
                _ButtonDisplay = value
            End Set
        End Property
        Private _lbldispaly As Boolean = True
        Public Property lbldispaly As Boolean
            Get
                Return _lbldispaly
            End Get
            Set(ByVal value As Boolean)
                _lbldispaly = value
            End Set
        End Property

        Private _LastError As String
        Public Property LastError As String
            Get
                Return _LastError
            End Get
            Set(ByVal value As String)
                _LastError = value
            End Set
        End Property
        Private _TopicFilterCommaList As String
        Public Property TopicFilterCommaList As String
            Get
                Return _TopicFilterCommaList
            End Get
            Set(value As String)
                _TopicFilterCommaList = value
            End Set
        End Property

        Public Property CheckChildNodes As Boolean
            Get
                Return trvTopicCodes.CheckChildNodes
            End Get
            Set(value As Boolean)
                trvTopicCodes.CheckChildNodes = value
            End Set
        End Property


        ''''''''''''''''''''''''''''''''''''''''''''''

        Public Sub LoadTree()
            'Amruta,Issue 14307,To clear node after check and uncheck in AdminEditProfile Topic code of interest edit pop-up 
            trvTopicCodes.Nodes.Clear()
            Try
                Dim dbName = AptifyApplication.GetEntityBaseDatabase("Topic Codes")
                Dim tcView = AptifyApplication.GetEntityBaseView("Topic Codes")
                Dim tcEntitiesView = AptifyApplication.GetEntityBaseView("TopicCodeEntities")

                ' loading global optout checkbox
                Dim globalOptOutSSQL = String.Format(GLOBAL_OPTOUT_SSQL, dbName, tcView, tcEntitiesView, EntityName, GLOBAL_OPTOUT_TOPIC_CODE_NAME)
                Dim globalOptOutODA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                Dim globalOptOutODT As DataTable = globalOptOutODA.GetDataTable(globalOptOutSSQL)
                If (globalOptOutODT.Rows.Count > 0) Then
                    Dim globalOptOutRow = globalOptOutODT.Rows(0)
                    Dim oNode As RadTreeNode = New RadTreeNode()
                    oNode.Value = globalOptOutRow("ID").ToString
                    SetNodeCheckedStatus(oNode)

                    chkGlobalOptOut.Checked = oNode.Checked
                End If
                ' end of global optout checkbox info

                Dim sSQL As String
                If (String.IsNullOrWhiteSpace(TopicFilterCommaList)) Then
                    sSQL = String.Format("SELECT ID,ParentID,Name,WebEnabled " +
                                         "FROM {0}..{1} WHERE ISNULL(ParentID,-1)=-1 AND ( Status='Active'  AND (ID IN (" +
                                         "SELECT TopicCodeID " +
                                         "FROM {0}..{2} " +
                                         "WHERE EntityID_Name ='{3}' AND Status = 'Active' AND WebEnabled =1)))",
                                         dbName, tcView, tcEntitiesView, EntityName)
                Else
                    sSQL = String.Format("SELECT ID,ParentID,Name,WebEnabled " +
                                                   "FROM {0}..{1} WHERE ISNULL(ParentID,-1)=-1 AND ( Status='Active'  AND (ID IN (" +
                                                   "SELECT TopicCodeID " +
                                                   "FROM {0}..{2} " +
                                                   "WHERE EntityID_Name ='{3}' AND Status = 'Active' AND WebEnabled =1)) AND " +
                                            "Name in ({4}) )",
                                                   dbName, tcView, tcEntitiesView, EntityName, GetCommaList(TopicFilterCommaList))
                    Dim listOfTopicCodes = TopicFilterCommaList
                    Dim _substrings() As String = TopicFilterCommaList.Split({","}, StringSplitOptions.RemoveEmptyEntries)
                    sSQL += " ORDER BY ( Case Name "
                    Dim _index = 1
                    For Each subString As String In _substrings
                        Dim s = subString.Trim().Trim("'"c)
                        sSQL += " WHEN '" + s + "' THEN " + _index.ToString()
                        _index += 1
                    Next
                    sSQL += " END )"

                End If

                Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                Dim odt As DataTable = oDA.GetDataTable(sSQL)
                Dim hdt As List(Of String) = New List(Of String)

                If odt IsNot Nothing AndAlso odt.Rows.Count > 0 Then
                    For Each rw As DataRow In odt.Rows
                        Dim oNode As RadTreeNode = New RadTreeNode()
                        oNode.NodeTemplate = trvTopicCodes.NodeTemplate
                        oNode.Value = rw("ID").ToString

                        Dim sSQL_sub As String = "SELECT ID,ParentID,Name,WebEnabled FROM " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ( Status='Active' AND (ID IN (SELECT TopicCodeID FROM " + AptifyApplication.GetEntityBaseDatabase("TopicCodeEntities") + ".." + AptifyApplication.GetEntityBaseView("TopicCodeEntities") + " WHERE EntityID_Name = '" + EntityName + "'  AND Status = 'Active' AND WebEnabled =1))) and ParentID='" + oNode.Value + "'"
                        Dim oDA_sub As DataAction = New DataAction(AptifyApplication.UserCredentials)
                        Dim odt_sub As DataTable = oDA_sub.GetDataTable(sSQL_sub)
                        If odt_sub IsNot Nothing AndAlso odt_sub.Rows.Count > 0 Then
                            trvTopicCodes.Nodes.Add(oNode)

                            Dim lbl As Label = CType(oNode.Controls.Item(1), Label)
                            lbl.Text = rw("Name").ToString
                            hdt.Add(lbl.Text)

                            SetNodeCheckedStatus(oNode)
                            oNode = CustomNode(oNode)
                            LoadChildrenNodes(oNode)
                        End If
                    Next

                End If

                trvTopicCodesRepeater.DataSource = hdt
                trvTopicCodesRepeater.DataBind()

                If (trvTopicCodes.IsEmpty) Then
                    tdtopiccode.Visible = False
                    tdtopic.Visible = False
                    MsgBlankTopiccode.Visible = True
                Else
                    tdtopiccode.Visible = True
                    tdtopic.Visible = True
                    MsgBlankTopiccode.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Function GetCommaList(topicFilterCommaList As String) As Object
            Dim substrings() As String = topicFilterCommaList.Split({","}, StringSplitOptions.RemoveEmptyEntries)
            Dim stringList = New List(Of String)

            For Each subString As String In substrings
                stringList.Add(String.Format("'{0}'", subString.Trim().Trim("'"c)))
            Next

            Return String.Join(",", stringList)

        End Function

        Protected Overridable Function SetNodeCheckedStatus(ByRef oNode As RadTreeNode) As Boolean
            Dim sSQL As String
            Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
            sSQL = "SELECT Status from  " + AptifyApplication.GetEntityBaseDatabase("Topic Code Links") + ".." + AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID='" + oNode.Value + "' and EntityID='" + Convert.ToString(AptifyApplication.GetEntityID(EntityName.ToString())) + "' and RecordID='" + Convert.ToString(RecordID) + "'"
            Dim Status As Object = ""
            Status = oDA.ExecuteScalar(sSQL)
            If Not String.IsNullOrEmpty(CStr(Status)) Then
                If CStr(Status) = "Active" Then
                    oNode.Checked = True
                    Return True
                Else
                    oNode.Checked = False
                    Return False
                End If
            End If
            oNode.Checked = False
            Return False
        End Function

        Protected Overridable Function CustomNode(ByRef oNode As RadTreeNode) As RadTreeNode
            Try
                Dim ddl As DropDownList = CType(oNode.Controls.Item(3), DropDownList)
                Dim txt As TextBox = CType(oNode.Controls.Item(5), TextBox)
                Dim sSQL As String = "SELECT ID,ValueType from  " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ID='" + oNode.Value + "'"
                Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                Dim odt As DataTable = oDA.GetDataTable(sSQL)
                If odt IsNot Nothing AndAlso odt.Rows.Count > 0 Then
                    If odt.Rows(0)("ValueType") IsNot Nothing Then
                        If ddl IsNot Nothing Then
                            ddl.Items.Clear()
                            ddl.Visible = False
                        End If
                        If txt IsNot Nothing Then
                            txt.Text = String.Empty
                            txt.Visible = False
                        End If
                        If oNode.Checked = True Then
                            Select Case Convert.ToString(odt.Rows(0)("ValueType"))
                                'Case "Yes/No"
                                '    Dim sVal As String = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID(EntityName.ToString())), Convert.ToString(RecordID))
                                '    If sVal = "Yes" Then
                                '        oNode.Checked = True
                                '    ElseIf sVal = "No" Then
                                '        oNode.Checked = False
                                '    End If
                                Case "Multiple Choice"
                                    If ddl IsNot Nothing Then
                                        RenderDropDownloadList(oNode.Value, ddl)
                                        Dim sVal As String = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID(EntityName.ToString())), Convert.ToString(RecordID))
                                        If sVal IsNot Nothing And Not String.IsNullOrEmpty(sVal) AndAlso ddl.Items.Contains(New ListItem(sVal)) Then
                                            ddl.SelectedValue = sVal
                                        End If
                                        ddl.Visible = True
                                    End If
                                Case "Numeric"
                                    If txt IsNot Nothing Then
                                        txt.Text = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID(EntityName.ToString())), Convert.ToString(RecordID))
                                        txt.Visible = True
                                    End If
                                Case "Text"
                                    If txt IsNot Nothing Then
                                        txt.Text = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID(EntityName.ToString())), Convert.ToString(RecordID))
                                        txt.Visible = True
                                    End If
                            End Select
                        End If
                    End If
                End If
            Catch ex As Exception
                ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return oNode
        End Function

        Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            Try
                'Navin Prasad Issue 6448
                SaveTopicCode()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Public Sub SaveTopicCode()
            Dim sErrorString As String = ""

            If RecursiveUpdateTopicCodes(trvTopicCodes.Nodes, sErrorString) Then
                ' updating global opt out
                Dim dbName = AptifyApplication.GetEntityBaseDatabase("Topic Codes")
                Dim tcView = AptifyApplication.GetEntityBaseView("Topic Codes")
                Dim tcEntitiesView = AptifyApplication.GetEntityBaseView("TopicCodeEntities")

                Dim globalOptOutSSQL = String.Format(GLOBAL_OPTOUT_SSQL, dbName, tcView, tcEntitiesView, EntityName, GLOBAL_OPTOUT_TOPIC_CODE_NAME)
                Dim globalOptOutODA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                Dim globalOptOutODT As DataTable = globalOptOutODA.GetDataTable(globalOptOutSSQL)
                If (globalOptOutODT.Rows.Count > 0) Then
                    Dim globalOptOutRow = globalOptOutODT.Rows(0)
                    Dim oNode As RadTreeNode = New RadTreeNode()
                    oNode.Value = globalOptOutRow("ID").ToString
                    oNode.Checked = chkGlobalOptOut.Checked

                    Dim optOutSSQL = GetNodeCheckSQL(CLng(oNode.Value))
                    Dim iCount = CInt(DataAction.ExecuteScalar(optOutSSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                    Dim ErrorString As String = ""
                    If (oNode.Checked And ((iCount = 0) Or (globalOptOutRow.ItemArray.Contains("Suppression list")))) Then
                        AddTopicCodeLink(CLng(oNode.Value), GetInputValue(oNode), ErrorString)
                    ElseIf (iCount <> 0) Then
                        UpdateTopicCodeLink(CLng(oNode.Value), oNode.Checked, GetInputValue(oNode), ErrorString)
                    End If

                End If
                ' ----

                'lblDescription.Text = "Your preferences have been saved"
                'lblDescription.ForeColor = System.Drawing.Color.Blue
                lblDescription.Visible = False
                'lblDescription.Attributes.Add("class", "action-success-msg")
                Me.LastError = ""
                If _lbldispaly = False Then
                    lblDescription.Visible = False
                End If
            Else
                'lblDescription.Text = "Error updating preferences." + Environment.NewLine + sErrorString
                'lblDescription.ForeColor = System.Drawing.Color.Red
                lblDescription.Visible = False
                'lblDescription.Attributes.Add("class", "action-error-msg")
                'Me.LastError = sErrorString
            End If
        End Sub

        Public Overridable Function GetInputValue(ByRef oNode As RadTreeNode) As String
            Dim sSQL As String = "SELECT ID,ValueType from  " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ID='" + oNode.Value + "'"
            Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
            Dim odt As DataTable = oDA.GetDataTable(sSQL)
            If odt IsNot Nothing And odt.Rows.Count > 0 Then
                Select Case odt.Rows(0)("ValueType").ToString
                    Case "Yes/No"
                        If oNode.Checked = True Then
                            Return "Yes"
                        Else
                            Return "No"
                        End If
                    Case "Multiple Choice"
                        ' ddl exists
                        Dim ddl As DropDownList = CType(oNode.Controls.Item(3), DropDownList)
                        Return ddl.SelectedValue
                    Case "Numeric"
                        Dim txt As TextBox = CType(oNode.Controls.Item(5), TextBox)
                        Return txt.Text
                    Case "Text"
                        Dim txt As TextBox = CType(oNode.Controls.Item(5), TextBox)
                        Return txt.Text
                End Select
            End If
            Return String.Empty
        End Function

        Public Overridable Function RenderDropDownloadList(ByVal sTopicCodeID As String, ByRef ddl As DropDownList) As Boolean
            Dim sSQL As String = "SELECT ID,Value from  " + AptifyApplication.GetEntityBaseDatabase("TopicCodePossibleValues") + ".." + AptifyApplication.GetEntityBaseView("TopicCodePossibleValues") + " WHERE TopicCodeID='" + sTopicCodeID + "'"
            Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
            Dim odt As DataTable = oDA.GetDataTable(sSQL)
            If odt IsNot Nothing And odt.Rows.Count > 0 Then
                For Each rw As DataRow In odt.Rows
                    ddl.Items.Add(rw("Value").ToString)
                Next
            End If
            Return True
        End Function

        Public Overridable Function GetSavedValue(ByVal sTopicCodeID As String, ByVal sEntityID As String, ByVal sRecordID As String) As String
            Dim sSQL As String = "SELECT ID,Value from  " + AptifyApplication.GetEntityBaseDatabase("Topic Code Links") + ".." + AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID='" + sTopicCodeID + "' and EntityID='" + sEntityID + "' and RecordID='" + sRecordID + "'"
            Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
            Dim odt As DataTable = oDA.GetDataTable(sSQL)
            If odt IsNot Nothing AndAlso odt.Rows.Count > 0 Then
                If odt.Rows(0)("Value") IsNot Nothing AndAlso odt.Rows(0)("Value") IsNot DBNull.Value Then
                    Return odt.Rows(0)("Value").ToString
                End If
            End If
            Return String.Empty
        End Function

        Public Sub cmdClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearAll.Click
            ' clear all topic codes from this tree
            RecursiveSetCheckBoxes(trvTopicCodes.Nodes, False)
        End Sub
        Public Sub cmdCheckAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCheckAll.Click
            ' check off all topic codes from this tree
            RecursiveSetCheckBoxes(trvTopicCodes.Nodes, True)
        End Sub

        Public Overridable Sub RecursiveSetCheckBoxes(ByRef oNodes As Telerik.Web.UI.RadTreeNodeCollection, ByVal bChecked As Boolean)
            Dim l As Integer
            Try
                For l = 0 To oNodes.Count - 1
                    oNodes(l).Checked = bChecked
                    CustomNode(oNodes(l))
                    RecursiveSetCheckBoxes(oNodes(l).Nodes, bChecked)
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Public Sub trvTopicCodes_NodeCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles trvTopicCodes.NodeCheck
            chkGlobalOptOut.Checked = False

            checkSuppressionList(trvTopicCodes.Nodes)

            'CustomNode(e.Node)
            'e.Node.Focus() 'Added by sandeep for issue 14761
        End Sub

#Region "Save Methods"
        'Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        '    Try
        '        'Navin Prasad Issue 6448
        '        Dim sErrorString As String = ""
        '        If RecursiveUpdateTopicCodes(trvTopicCodes.Nodes, sErrorString) Then
        '            'lblDescription.Text = "Topic Codes Updated Successfully!"
        '            'lblDescription.ForeColor = System.Drawing.Color.Blue
        '            'lblDescription.Visible = True
        '        Else
        '            'lblDescription.Text = "Error updating topic codes." + Environment.NewLine + sErrorString
        '            'lblDescription.ForeColor = System.Drawing.Color.Red
        '            'lblDescription.Visible = True
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try

        'End Sub

        Public Overridable Function RecursiveUpdateTopicCodes(ByVal Nodes As Telerik.Web.UI.RadTreeNodeCollection, ByRef ErrorString As String) As Boolean
            Dim i As Integer
            Dim sSQL As String
            Dim iCount As Long
            Dim bOK As Boolean
            Try
                bOK = True
                For i = 0 To Nodes.Count - 1
                    With Nodes(i)
                        sSQL = GetNodeCheckSQL(CLng(.Value))
                        iCount = CInt(DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                        Dim checkBoxText As String = DirectCast(.Controls(1), System.Web.UI.WebControls.Label).Text
                        ' changing all checked values to false if global opt out is turned on
                        If (chkGlobalOptOut.Checked) Then
                            If (checkBoxText = "Suppression list") Then
                                .Checked = True
                            Else
                                .Checked = False
                            End If
                        End If
                        ' --------------------

                        If ((chkGlobalOptOut.Checked Or .Checked) And iCount = 0) Then
                            ' This executes only if the node is checked and the db
                            ' does not have this turned on
                            bOK = bOK And AddTopicCodeLink(CLng(.Value), GetInputValue(Nodes(i)), ErrorString)
                        ElseIf (iCount <> 0 AndAlso (chkGlobalOptOut.Checked Or Not .Checked Or .Checked)) Then
                            ' if the node is not checked or checked and the DB does have the topic code 
                            ' turned on - update an existing topic code link.
                            bOK = bOK And UpdateTopicCodeLink(CLng(.Value), .Checked, GetInputValue(Nodes(i)), ErrorString)
                        End If
                    End With
                    bOK = bOK And RecursiveUpdateTopicCodes(Nodes(i).Nodes, ErrorString)
                Next
                Return bOK
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        Private Function GetNodeCheckSQL(ByVal lTopicCodeID As Long) As String
            Return "SELECT COUNT(*) FROM " &
                   GetNodeBaseSQL(lTopicCodeID)
        End Function
        Private Function GetNodeLinkSQL(ByVal lTopicCodeID As Long) As String
            Return "SELECT ID FROM " &
                   GetNodeBaseSQL(lTopicCodeID)
        End Function
        Protected Overridable Function GetNodeBaseSQL(ByVal lTopicCodeID As Long) As String
            Dim sSQL As String
            sSQL = Database & ".." &
                   AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID=" & lTopicCodeID &
                   " AND EntityID=(SELECT ID FROM " &
                   Database & ".." &
                   AptifyApplication.GetEntityBaseView("Entities") + " WHERE Name='" + EntityName + "') " &
                   " AND RecordID=" & RecordID

            Return sSQL
        End Function

        Protected Overridable Function AddTopicCodeLink(ByVal TopicCodeID As Long, ByVal Value As String, ByRef ErrorString As String) As Boolean
            Dim oLink As AptifyGenericEntityBase
            Try
                oLink = AptifyApplication.GetEntityObject("Topic Code Links", -1)
                oLink.SetValue("TopicCodeID", TopicCodeID)
                oLink.SetValue("RecordID", RecordID)
                oLink.SetValue("EntityID", AptifyApplication.GetEntityID(EntityName.ToString()))
                oLink.SetValue("Status", "Active")
                oLink.SetValue("Value", Value)
                oLink.SetValue("DateAdded", Date.Today)
                Return oLink.Save(ErrorString)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        Protected Overridable Function UpdateTopicCodeLink(ByVal TopicCodeID As Long,
                                             ByVal Active As Boolean, ByVal Value As String, ByRef ErrorString As String) As Boolean
            Dim sSQL As String
            Dim lLinkID As Long
            Dim oLink As AptifyGenericEntityBase
            Try
                sSQL = GetNodeLinkSQL(TopicCodeID)
                lLinkID = CLng(DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                oLink = AptifyApplication.GetEntityObject("Topic Code Links", lLinkID)
                oLink.SetValue("Value", Value)
                If Active Then
                    oLink.SetValue("Status", "Active")
                Else
                    oLink.SetValue("Status", "Inactive")
                End If
                Return oLink.Save(ErrorString)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

#End Region

        Protected Overridable Sub LoadChildrenNodes(ByVal oNode As RadTreeNode)
            Dim sSQL As String = "SELECT ID,ParentID,Name,WebEnabled FROM " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ( Status='Active' AND (ID IN (SELECT TopicCodeID FROM " + AptifyApplication.GetEntityBaseDatabase("TopicCodeEntities") + ".." + AptifyApplication.GetEntityBaseView("TopicCodeEntities") + " WHERE EntityID_Name = '" + EntityName + "'  AND Status = 'Active' AND WebEnabled =1))) and ParentID='" + oNode.Value + "' ORDER BY Name ASC"
            Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
            Dim odt As DataTable = oDA.GetDataTable(sSQL)
            If odt IsNot Nothing AndAlso odt.Rows.Count > 0 Then
                For Each rw As DataRow In odt.Rows
                    Dim oChildNode As RadTreeNode = New RadTreeNode()
                    oNode.NodeTemplate = trvTopicCodes.NodeTemplate
                    oChildNode.Value = rw("ID").ToString
                    oNode.Nodes.Add(oChildNode)
                    Dim lbl As Label = CType(oChildNode.Controls.Item(1), Label)
                    lbl.Text = rw("Name").ToString
                    SetNodeCheckedStatus(oChildNode)
                    oChildNode = CustomNode(oChildNode)
                    LoadChildrenNodes(oChildNode)
                Next
            End If
        End Sub

#End Region

        'Protected Function getSuppressionListId() As Integer
        '    Dim sSQL As String = "SELECT ID FROM " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + " WHERE Name like 'Suppression list' "
        '    Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
        '    Dim odt As DataTable = oDA.GetDataTable(sSQL)
        '    If odt IsNot Nothing AndAlso odt.Rows.Count > 0 Then
        '        Dim row As DataRow = odt.Rows.Item(0)
        '        Return CInt(row("ID"))
        '    End If
        '    Return -1
        'End Function


        Sub chkGlobalOptOut_RecursiveUnCheck(ByRef Nodes As Telerik.Web.UI.RadTreeNodeCollection)

            Dim i As Integer
            For i = 0 To Nodes.Count - 1
                With Nodes(i)
                    ' changing all checked values to false if global opt out is turned on
                    Dim checkBoxText As String = DirectCast(.Controls(1), System.Web.UI.WebControls.Label).Text
                    If (chkGlobalOptOut.Checked) Then
                        If (checkBoxText = "Suppression list") Then
                            .Checked = True
                        Else
                            .Checked = False
                        End If
                    Else
                        If (checkBoxText = "Suppression list") Then
                            .Checked = False
                        End If

                    End If

                    ' --------------------

                End With

                chkGlobalOptOut_RecursiveUnCheck(Nodes(i).Nodes)
            Next
        End Sub

        Protected Sub chkGlobalOptOut_CheckedChanged(sender As Object, e As EventArgs)
            chkGlobalOptOut_RecursiveUnCheck(trvTopicCodes.Nodes)
        End Sub

        Sub checkSuppressionList(ByRef Nodes As Telerik.Web.UI.RadTreeNodeCollection)

            Dim i As Integer
            For i = 0 To Nodes.Count - 1
                With Nodes(i)
                    ' changing all checked values to false if global opt out is turned on
                    If (DirectCast(.Controls(1), System.Web.UI.WebControls.Label).Text = "Suppression list") Then
                        chkGlobalOptOut.Checked = .Checked
                        If (.Checked) Then
                            chkGlobalOptOut_RecursiveUnCheck(trvTopicCodes.Nodes)
                        End If

                    End If
                    ' --------------------

                End With
                checkSuppressionList(Nodes(i).Nodes)
            Next
        End Sub


    End Class
End Namespace
