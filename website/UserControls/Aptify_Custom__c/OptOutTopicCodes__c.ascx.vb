''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date Created/Modified               Summary
'Siddharth                  11th Mar 2014                       Display and save the topic codes 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework
Imports Aptify.Framework.DataServices
Imports System.Data
Imports Telerik.Web.UI


Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class OptOutTopicCodes
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "OptOutTopicCodes"
        Protected Const ATTRIBUTE_CONTORL_LOGIN_REDIRECT As String = "LoginRedirect"

        Public Overridable Property RedirectPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTORL_LOGIN_REDIRECT) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTORL_LOGIN_REDIRECT))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTORL_LOGIN_REDIRECT) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(RedirectPage) Then
                RedirectPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTORL_LOGIN_REDIRECT)
            End If
        End Sub

        Private sTopicCodeId As String = ""
        Private sOrgId As String = ""

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.Page.MaintainScrollPositionOnPostBack = True            

            If User1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect("~/Login.aspx", False)
            End If

            SetProperties()

            If Not IsPostBack Then
                Dim oSecurityKey As New Aptify.Framework.BusinessLogic.Security.AptifySecurityKey(DataAction.UserCredentials)
                sTopicCodeId = Request.QueryString("TopicCodeId")
                sTopicCodeId = oSecurityKey.DecryptData("E Business Login Key", sTopicCodeId)
                sOrgId = Request.QueryString("OrgId")
                sOrgId = oSecurityKey.DecryptData("E Business Login Key", sOrgId)
                If Not sTopicCodeId = "" AndAlso sOrgId <> "" Then
                    Dim bIsOrganizationMember As Boolean = IsPersonOfOrganization(User1.PersonID, Convert.ToInt32(sOrgId))
                    If bIsOrganizationMember Then
                        LoadTree()
                        trTopicCode.Visible = True
                        lblError.Visible = False
                    Else
                        trTopicCode.Visible = False
                        lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.CommunicationOptOut.NotFromOrganization")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        lblError.Visible = True
                    End If
                Else
                    trTopicCode.Visible = False
                End If
            End If
        End Sub
        Public Overridable Sub LoadTree()
            Try
                Dim oParam(1) As Data.IDataParameter
                oParam(0) = DataAction.GetDataParameter("@ParentID", SqlDbType.VarChar, sTopicCodeId)
                oParam(1) = DataAction.GetDataParameter("@OrganizationID", SqlDbType.VarChar, sOrgId)
                Dim sSQL As String = Database & "..spGetTopicCodeByParentID__c"
                'Dim sSQL As String = "SELECT ID,ParentID,Name,WebEnabled, Description FROM " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ISNULL(ParentID,-1)=-1 AND ( Status='Active' AND WebEnabled = 'Global' OR ( WebEnabled = 'By Service' AND (ID IN (SELECT TopicCodeID FROM " + AptifyApplication.GetEntityBaseDatabase("TopicCodeEntities") + ".." + AptifyApplication.GetEntityBaseView("TopicCodeEntities") + " WHERE EntityID_Name = 'Persons' AND Status = 'Active' AND WebEnabled =1)))) ORDER By Name ASC"
                Dim dtTopicCodes As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, oParam)
                If dtTopicCodes IsNot Nothing AndAlso dtTopicCodes.Rows.Count > 0 Then
                    For Each rw As DataRow In dtTopicCodes.Rows
                        Dim oNode As RadTreeNode = New RadTreeNode()
                        oNode.NodeTemplate = trvTopicCodes.NodeTemplate
                        oNode.Value = rw("ID").ToString
                        trvTopicCodes.Nodes.Add(oNode)
                        Dim lbl As Label = CType(oNode.Controls.Item(1), Label)
                        lbl.Text = rw("Name").ToString
                        SetNodeCheckedStatus(oNode)
                        oNode = CustomNode(oNode)
                        LoadChildrenNodes(oNode)
                    Next
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overridable Sub LoadChildrenNodes(ByVal oNode As RadTreeNode)
            Try
                'Dim sSQL As String = "SELECT ID,ParentID,Name,WebEnabled FROM " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ( Status='Active' AND WebEnabled = 'Global' OR ( WebEnabled = 'By Service' AND (ID IN (SELECT TopicCodeID FROM " + AptifyApplication.GetEntityBaseDatabase("TopicCodeEntities") + ".." + AptifyApplication.GetEntityBaseView("TopicCodeEntities") + " WHERE EntityID_Name = 'Persons' AND Status = 'Active' AND WebEnabled =1)))) and ParentID='" + oNode.Value + "'" + "order by Name"
                Dim oParam(0) As Data.IDataParameter
                oParam(0) = DataAction.GetDataParameter("@ParentNode", SqlDbType.Int, Convert.ToInt32(oNode.Value))
                Dim sSQL As String = Database & "..spGetChildTopicCodesOfPersons__c"
                'Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                Dim iCount As Integer = -1, bIsOther As Boolean = False, iOtherIndex As Integer = -1
                Dim dtTopicCode As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, oParam)
                If dtTopicCode IsNot Nothing AndAlso dtTopicCode.Rows.Count > 0 Then

                    For Each rwTopicCode As DataRow In dtTopicCode.Rows
                        iCount = iCount + 1                     '10/21/2012
                        If rwTopicCode("Name").ToString.ToUpper() = "OTHER" Then
                            iOtherIndex = iCount
                            bIsOther = True
                        Else
                            Dim oChildNode As RadTreeNode = New RadTreeNode()
                            oNode.NodeTemplate = trvTopicCodes.NodeTemplate
                            oChildNode.Value = rwTopicCode("ID").ToString
                            oNode.Nodes.Add(oChildNode)
                            Dim lbl As Label = CType(oChildNode.Controls.Item(1), Label)
                            lbl.Text = rwTopicCode("Name").ToString
                            SetNodeCheckedStatus(oChildNode)
                            oChildNode = CustomNode(oChildNode)
                            LoadChildrenNodes(oChildNode)
                        End If
                    Next
                    '10/21/2012
                    If bIsOther = True Then
                        LoadOtherChilderenNodes(dtTopicCode, iOtherIndex, oNode)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        '10/21/2012
        Private Sub LoadOtherChilderenNodes(ByVal dt As DataTable, ByVal iOtherIndex As Integer, ByVal oNode As RadTreeNode)
            Try
                Dim oChildNode As RadTreeNode = New RadTreeNode()
                oNode.NodeTemplate = trvTopicCodes.NodeTemplate
                oChildNode.Value = dt.Rows(iOtherIndex)("ID").ToString
                oNode.Nodes.Add(oChildNode)
                Dim lbl As Label = CType(oChildNode.Controls.Item(1), Label)
                lbl.Text = dt.Rows(iOtherIndex)("Name").ToString
                SetNodeCheckedStatus(oChildNode)
                oChildNode = CustomNode(oChildNode)
                LoadChildrenNodes(oChildNode)
            Catch ex As Exception
                ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Function SetNodeCheckedStatus(ByRef oNode As RadTreeNode) As Boolean
            Try                
                If User1.PersonID > 0 Then
                    'Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                    Dim oParam(2) As Data.IDataParameter
                    oParam(0) = DataAction.GetDataParameter("@ParentNode", SqlDbType.Int, Convert.ToInt32(oNode.Value))
                    oParam(1) = DataAction.GetDataParameter("@EntityId", SqlDbType.Int, AptifyApplication.GetEntityID("Persons"))
                    oParam(2) = DataAction.GetDataParameter("@PersonId", SqlDbType.Int, Convert.ToInt32(User1.PersonID))
                    Dim sSQL As String = Database & "..spGetStatusofTopicCodes__c"
                    'sSQL = "SELECT Status from  " + AptifyApplication.GetEntityBaseDatabase("Topic Code Links") + ".." + AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID='" + oNode.Value + "' and EntityID='" + Convert.ToString(AptifyApplication.GetEntityID("Persons")) + "' and RecordID='" + Convert.ToString(User1.PersonID) + "'"
                    Dim Status As Object = ""
                    Status = DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, oParam)

                    If Not String.IsNullOrEmpty(CStr(Status)) Then
                        If CStr(Status) = "Active" Then
                            oNode.Checked = True
                            Return True
                        Else
                            oNode.Checked = False
                            Return False
                        End If
                    End If

                End If
                oNode.Checked = False
                Return False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function


        Protected Overridable Function CustomNode(ByRef oNode As RadTreeNode) As RadTreeNode
            Try
                Dim ddl As DropDownList = CType(oNode.Controls.Item(3), DropDownList)
                Dim txt As TextBox = CType(oNode.Controls.Item(5), TextBox)
                'Dim sSQL As String = "SELECT ID,ValueType from  " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ID='" + oNode.Value + "'"
                Dim oParam(0) As Data.IDataParameter
                oParam(0) = DataAction.GetDataParameter("@NodeId", SqlDbType.Int, Convert.ToInt32(oNode.Value))
                Dim sSQL As String = Database & "..spGetValueTypeofTopicCodes__c"

                'Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                Dim dtValueTypes As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, oParam)
                If dtValueTypes IsNot Nothing AndAlso dtValueTypes.Rows.Count > 0 Then
                    If dtValueTypes.Rows(0)("ValueType") IsNot Nothing Then
                        If ddl IsNot Nothing Then
                            ddl.Items.Clear()
                            ddl.Visible = False
                        End If
                        If txt IsNot Nothing Then
                            txt.Text = String.Empty
                            txt.Visible = False
                        End If
                        If oNode.Checked = True Then
                            Select Case Convert.ToString(dtValueTypes.Rows(0)("ValueType"))
                                'Case "Yes/No"
                                '    Dim sVal As String = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID("Persons")), Convert.ToString(User1.PersonID))
                                '    If sVal = "Yes" Then
                                '        oNode.Checked = True
                                '    ElseIf sVal = "No" Then
                                '        oNode.Checked = False
                                '    End If
                                Case "Multiple Choice"
                                    If ddl IsNot Nothing Then
                                        RenderDropDownloadList(oNode.Value, ddl)
                                        Dim sVal As String = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID("Persons")), Convert.ToString(User1.PersonID))
                                        If sVal IsNot Nothing And Not String.IsNullOrEmpty(sVal) AndAlso ddl.Items.Contains(New ListItem(sVal)) Then
                                            ddl.SelectedValue = sVal
                                        End If
                                        ddl.Visible = True
                                    End If
                                Case "Numeric"
                                    If txt IsNot Nothing Then
                                        txt.Text = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID("Persons")), Convert.ToString(User1.PersonID))
                                        txt.Visible = True
                                    End If
                                Case "Text"
                                    If txt IsNot Nothing Then
                                        txt.Text = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID("Persons")), Convert.ToString(User1.PersonID))
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

        Protected Overridable Function GetInputValue(ByRef oNode As RadTreeNode) As String
            Try
                'Dim sSQL As String = "SELECT ID,ValueType from  " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ID='" + oNode.Value + "'"
                Dim oParam(0) As Data.IDataParameter
                oParam(0) = DataAction.GetDataParameter("@NodeId", SqlDbType.Int, Convert.ToInt32(oNode.Value))
                Dim sSQL As String = Database & "..spGetValueTypeofTopicCodes__c"
                'Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                Dim dtValueTypes As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, oParam)
                If dtValueTypes IsNot Nothing And dtValueTypes.Rows.Count > 0 Then
                    Select Case dtValueTypes.Rows(0)("ValueType").ToString
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
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return String.Empty

        End Function

        Protected Overridable Function RenderDropDownloadList(ByVal sTopicCodeID As String, ByRef ddl As DropDownList) As Boolean
            Try
                'Dim sSQL As String = "SELECT ID,Value from  " + AptifyApplication.GetEntityBaseDatabase("TopicCodePossibleValues") + ".." + AptifyApplication.GetEntityBaseView("TopicCodePossibleValues") + " WHERE TopicCodeID='" + sTopicCodeID + "'"
                Dim oParam(0) As Data.IDataParameter
                oParam(0) = DataAction.GetDataParameter("@TopicCodeID", SqlDbType.VarChar, sTopicCodeID)
                Dim sSQL As String = Database & "..spGetTopicCodePossibleValues__c"

                'Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                Dim dtPossibleValues As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, oParam)
                If dtPossibleValues IsNot Nothing And dtPossibleValues.Rows.Count > 0 Then
                    For Each rwValues As DataRow In dtPossibleValues.Rows
                        ddl.Items.Add(rwValues("Value").ToString)
                    Next
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return True
        End Function

        Protected Overridable Function GetSavedValue(ByVal sTopicCodeID As String, ByVal sEntityID As String, ByVal sRecordID As String) As String
            'Dim sSQL As String = "SELECT ID,Value from  " + AptifyApplication.GetEntityBaseDatabase("Topic Code Links") + ".." + AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID='" + sTopicCodeID + "' and EntityID='" + sEntityID + "' and RecordID='" + sRecordID + "'"
            Dim oParam(2) As Data.IDataParameter
            oParam(0) = DataAction.GetDataParameter("@TopicCodeID", SqlDbType.Int, Convert.ToInt32(sTopicCodeID))
            oParam(1) = DataAction.GetDataParameter("@EntityId", SqlDbType.Int, Convert.ToInt32(sEntityID))
            oParam(2) = DataAction.GetDataParameter("@PersonId", SqlDbType.Int, Convert.ToInt32(sRecordID))
            Dim sSQL As String = Database & "..spGetSavedTopicCodes__c"
            'Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
            Dim dtSavedValue As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, oParam)
            If dtSavedValue IsNot Nothing AndAlso dtSavedValue.Rows.Count > 0 Then
                If dtSavedValue.Rows(0)("Value") IsNot Nothing AndAlso dtSavedValue.Rows(0)("Value") IsNot DBNull.Value Then
                    Return dtSavedValue.Rows(0)("Value").ToString
                End If
            End If
            Return String.Empty
        End Function

#Region "Save Methods"
        Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            Try
                'Navin Prasad Issue 6448
                Dim sErrorString As String = ""
                If RecursiveUpdateTopicCodes(trvTopicCodes.Nodes, sErrorString) Then
                    lblDescription.Text = "Topic Codes Updated Successfully!"
                    lblDescription.ForeColor = System.Drawing.Color.Blue
                    'lblDescription.Visible = False
                Else
                    lblDescription.Text = "Error updating topic codes." + Environment.NewLine + sErrorString
                    lblDescription.ForeColor = System.Drawing.Color.Red
                    'lblDescription.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Public Overridable Function RecursiveUpdateTopicCodes(ByVal Nodes As Telerik.Web.UI.RadTreeNodeCollection, ByRef ErrorString As String, Optional ByRef lPersonID As Long = -1) As Boolean
            Dim i As Integer
            Dim sSQL As String
            Dim iCount As Long
            Dim bOK As Boolean
            Try
                bOK = True
                For i = 0 To Nodes.Count - 1
                    With Nodes(i)
                        sSQL = GetNodeCheckSQL(CLng(.Value), lPersonID)
                        iCount = CInt(DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If (.Checked And iCount = 0) Then
                            ' This executes only if the node is checked and the db
                            ' does not have this turned on
                            If User1.PersonID > 0 Then
                                bOK = bOK And AddTopicCodeLink(CLng(.Value), GetInputValue(Nodes(i)), ErrorString)
                            Else
                                bOK = bOK And AddTopicCodeLink(CLng(.Value), GetInputValue(Nodes(i)), ErrorString, lPersonID)
                            End If

                        ElseIf (iCount <> 0 AndAlso (Not .Checked Or .Checked)) Then
                            ' if the node is not checked or checked and the DB does have the topic code 
                            ' turned on - update an existing topic code link.
                            bOK = bOK And UpdateTopicCodeLink(CLng(.Value), .Checked, GetInputValue(Nodes(i)), ErrorString)
                        End If
                    End With
                    bOK = bOK And RecursiveUpdateTopicCodes(Nodes(i).Nodes, ErrorString, lPersonID)
                Next
                Return bOK
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        Private Function GetNodeCheckSQL(ByVal lTopicCodeID As Long, Optional ByRef lPersonID As Long = -1) As String
            Return "SELECT COUNT(*) FROM " & _
                   GetNodeBaseSQL(lTopicCodeID, lPersonID)
        End Function
        Private Function GetNodeLinkSQL(ByVal lTopicCodeID As Long, Optional ByRef lPersonID As Long = -1) As String
            Return "SELECT ID FROM " & _
                   GetNodeBaseSQL(lTopicCodeID, lPersonID)
        End Function
        Protected Overridable Function GetNodeBaseSQL(ByVal lTopicCodeID As Long, Optional ByRef lPersonID As Long = -1) As String
            Dim sSQL As String

            If User1.PersonID > 0 Then
                sSQL = Database & ".." & _
                  AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID=" & lTopicCodeID & _
                  " AND EntityID=(SELECT ID FROM " & _
                  Database & ".." & _
                  AptifyApplication.GetEntityBaseView("Entities") + " WHERE Name='Persons') " & _
                  " AND RecordID=" & User1.PersonID

                Return sSQL
            Else
                sSQL = Database & ".." & _
                  AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID=" & lTopicCodeID & _
                  " AND EntityID=(SELECT ID FROM " & _
                  Database & ".." & _
                  AptifyApplication.GetEntityBaseView("Entities") + " WHERE Name='Persons') " & _
                  " AND RecordID=" & lPersonID

                Return sSQL
            End If

        End Function

        Protected Overridable Function AddTopicCodeLink(ByVal TopicCodeID As Long, ByVal Value As String, ByRef ErrorString As String, Optional ByRef lPersonID As Long = -1) As Boolean
            Dim oLink As AptifyGenericEntityBase
            Try
                oLink = AptifyApplication.GetEntityObject("Topic Code Links", -1)
                oLink.SetValue("TopicCodeID", TopicCodeID)

                If User1.PersonID > 0 Then
                    oLink.SetValue("RecordID", User1.PersonID)
                Else
                    oLink.SetValue("RecordID", lPersonID)
                End If

                oLink.SetValue("EntityID", AptifyApplication.GetEntityID("Persons"))
                oLink.SetValue("Status", "Active")
                oLink.SetValue("Value", Value)
                oLink.SetValue("DateAdded", Date.Today)
                Return oLink.Save(ErrorString)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        Protected Overridable Function UpdateTopicCodeLink(ByVal TopicCodeID As Long, _
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

        Private Sub cmdClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearAll.Click
            ' clear all topic codes from this tree
            RecursiveSetCheckBoxes(trvTopicCodes.Nodes, False)
        End Sub
        Private Sub cmdCheckAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCheckAll.Click
            ' check off all topic codes from this tree
            RecursiveSetCheckBoxes(trvTopicCodes.Nodes, True)
        End Sub

        Protected Overridable Sub RecursiveSetCheckBoxes(ByRef oNodes As Telerik.Web.UI.RadTreeNodeCollection, ByVal bChecked As Boolean)
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

        Protected Sub trvTopicCodes_NodeCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles trvTopicCodes.NodeCheck
            Me.Page.MaintainScrollPositionOnPostBack = True
            CustomNode(e.Node)
        End Sub

        Private Function IsPersonOfOrganization(ByVal iPersonId As Long, ByVal iOrganizationId As Integer) As Boolean
            Dim bResult As Boolean = False
            Try
                Dim sPersonID As String = "0"
                Dim oParam(1) As Data.IDataParameter
                oParam(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, iPersonId)
                oParam(1) = DataAction.GetDataParameter("@OrganizationID", SqlDbType.Int, iOrganizationId)
                Dim sSQL As String = AptifyApplication.GetEntityBaseDatabase("Persons") & "..spCheckIsUserFromOrganization__c"
                sPersonID = DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, oParam).ToString()
                If sPersonID <> "0" AndAlso sPersonID IsNot Nothing Then
                    bResult = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
            Return bResult
        End Function

    End Class


End Namespace
