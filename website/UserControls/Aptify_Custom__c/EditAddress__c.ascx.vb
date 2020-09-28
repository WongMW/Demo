Option Explicit On

Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.DataServices

Namespace Aptify.Framework.Web.eBusiness.ProductCatalog
    Partial Class EditAddressControl
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CANCEL_BUTTON_PAGE As String = "CancelButtonPage"
        Protected Const ATTRIBUTE_ADD_EDIT_ADDRESS_BUTTON_PAGE As String = "AddEditAddressButtonPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "EditAddress__c"

#Region "EditAddress Specific Properties"
        ''' <summary>
        ''' CancelButton page url
        ''' </summary>
        Public Overridable Property CancelButtonPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CANCEL_BUTTON_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CANCEL_BUTTON_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CANCEL_BUTTON_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' AddEditAddressButton page url
        ''' </summary>
        Public Overridable Property AddEditAddressButtonPage() As String
            Get
                If Not ViewState(ATTRIBUTE_ADD_EDIT_ADDRESS_BUTTON_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ADD_EDIT_ADDRESS_BUTTON_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ADD_EDIT_ADDRESS_BUTTON_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(CancelButtonPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                CancelButtonPage = Me.GetLinkValueFromXML(ATTRIBUTE_CANCEL_BUTTON_PAGE)
                If String.IsNullOrEmpty(CancelButtonPage) Then
                    Me.cmdCancel.Enabled = False
                    Me.cmdCancel.ToolTip = "CancelButtonPage property has not been set."
                End If
            End If
            If String.IsNullOrEmpty(AddEditAddressButtonPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                AddEditAddressButtonPage = Me.GetLinkValueFromXML(ATTRIBUTE_ADD_EDIT_ADDRESS_BUTTON_PAGE)
                If String.IsNullOrEmpty(AddEditAddressButtonPage) Then
                    Me.cmdSave.Enabled = False
                    Me.cmdSave.ToolTip = "AddEditAddressButtonPage property has not been set."
                End If
            End If
        End Sub
        '<%--Nalini Issue#12578--%>
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            Try
                If Not IsPostBack Then
                    Dim sAction As String
                    LoadCombos()
                    sAction = Request.QueryString("Action")
                    If Len(sAction) > 0 Then
                        If sAction = "Edit" Then
                            cmdSave.Text = "Update"
                            lblAddressHeader.Text = "Edit Address"
                            LoadAddress()
                        End If
                    End If
                    If sAction = "New" Then

                        'Modified by Kavita Zinage 13/05/2016 - Changed  Country ("United States" To "Ireland"), State ("DC" To "LN")
                        SetComboValue(cmbCountry, "Ireland")
                        PopulateStateAsCountry(cmbState, cmbCountry)
                        SetComboValue(cmbState, "LN")
                        If Request.QueryString("IsHome") IsNot Nothing Then
                            If Convert.ToString(Request.QueryString("IsHome")) = "Y" Then
                                cmbType.Enabled = False
                                lblName.visible = False
                                txtName.visible = False
                                txtName.Text = "Home Address"
                                ' ddlCodesList.SelectedValue = ddlCodesList.Items.FindByText("Enrolment Request Exists").Value()
                                cmbType.selectedvalue = cmbType.Items.FindByText("Home Address").Value()
                                 cmbAddressName.SelectedItem.Text = "Home Address"
                                 cmbAddressName.Enabled = False
                             Else
                                cmbType.SelectedValue = cmbType.Items.FindByText("Other Address").Value()
                            End If
                        Else
                            cmbType.SelectedValue = cmbType.Items.FindByText("Other Address").Value()
                        End If

                    End If

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadAddress()
            ' load up the address
            Dim sType As String
            Dim iSequence As Integer

            Try
                sType = Request.QueryString("AddressType")
                iSequence = Request.QueryString("Sequence")
                If Len(sType) > 0 Then
                    trType.Visible = False
                    txtName.Enabled = False
                    Select Case sType
                        Case "Main"
                            SetAddress("", "Main Address")
                        Case "Billing"
                            SetAddress("Billing", "Billing Address")
                        Case "POBox"
                            SetAddress("POBox", "P.O. Box")
                        Case "Home"
                            SetAddress("Home", "Home Address")
                        Case "PersonAddress"
                            cmbType.Enabled = True
                            txtName.Enabled = True
                            LoadSubTypeAddress(iSequence)
                    End Select

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadSubTypeAddress(ByVal iSequence As Integer)
            Dim oPerson As AptifyGenericEntityBase
            Dim oItem As ListItem

            Try
                oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)

                With oPerson.SubTypes("PersonAddress").Item(iSequence - 1)
                    txtName.Text = .GetValue("Name")
                    txtAddressLine1.Text = .GetValue("AddressLine1")
                    txtAddressLine2.Text = .GetValue("AddressLine2")
                    txtAddressLine3.Text = .GetValue("AddressLine3")
                    txtCity.Text = .GetValue("City")
                    txtZipCode.Text = .GetValue("ZipCode")

                    oItem = cmbCountry.Items.FindByText(.GetValue("Country"))
                    If Not oItem Is Nothing Then
                        oItem.Selected = True
                    End If

                    PopulateState()

                    oItem = cmbState.Items.FindByText(.GetValue("State"))
                    If Not oItem Is Nothing Then
                        oItem.Selected = True
                    End If
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub SetAddress(ByVal sPrefix As String, ByVal sType As String)
            Dim oItem As ListItem

            Try
                txtName.Text = sType
                If sPrefix = "POBox" Then
                    txtAddressLine1.Text = User1.GetValue("POBoxLine1")
                    txtAddressLine2.Text = User1.GetValue("POBoxLine2")
                    txtAddressLine3.Text = User1.GetValue("POBoxLine3")
                Else
                    txtAddressLine1.Text = User1.GetValue(sPrefix & "AddressLine1")
                    txtAddressLine2.Text = User1.GetValue(sPrefix & "AddressLine2")
                    txtAddressLine3.Text = User1.GetValue(sPrefix & "AddressLine3")
                End If
                txtCity.Text = User1.GetValue(sPrefix & "City")
                txtZipCode.Text = User1.GetValue(sPrefix & "ZipCode")
                '11/27/07,Added by Tamasa,Issue 5222.
                oItem = cmbCountry.Items.FindByText(User1.GetValue(sPrefix & "Country"))
                If Not oItem Is Nothing Then
                    oItem.Selected = True
                End If
                PopulateState()
                'End
                oItem = cmbState.Items.FindByText(User1.GetValue(sPrefix & "State"))
                If Not oItem Is Nothing Then
                    oItem.Selected = True
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadCombos()
            Dim sSQL As String
            Try
                sSQL = Database & "..spGetCountryList"
                cmbCountry.DataSource = DataAction.GetDataTable(sSQL)
                cmbCountry.DataBind()


                sSQL = "SELECT ID,RTRIM(Name) Name FROM " & _
                       Database & "..vwAddressTypes " & _
                       " ORDER BY Name "
                cmbType.DataSource = DataAction.GetDataTable(sSQL)
                cmbType.DataBind()
                
                 sSQL =  Database & "..spGetAlternateAddressDetails__c @PersonID=" & User1.PersonID 
                cmbAddressName.DataSource = DataAction.GetDataTable(sSQL)
                cmbAddressName.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            If Request.QueryString("ReturnToPage") = "" Then
                Response.Redirect(CancelButtonPage & "?Type=" & Request.QueryString("Type"))
            Else
                If Not Request.QueryString("CA") Is Nothing Then
                    Response.Redirect(CancelButtonPage & "?Type=" & Request.QueryString("Type") & "&ReturnToPage=" & Request.QueryString("ReturnToPage") & "&CA=1")
                Else

                    Response.Redirect(CancelButtonPage & "?Type=" & Request.QueryString("Type") & "&ReturnToPage=" & Request.QueryString("ReturnToPage"))
                End If
            End If
        End Sub

        Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            Dim oPerson As AptifyGenericEntityBase
            Dim sType As String
            Dim iSequence As Integer
            Dim sAction As String

            Try
                sAction = Request.QueryString("Action")
                sType = Request.QueryString("AddressType")
                iSequence = Request.QueryString("Sequence")

                oPerson = AptifyApplication.GetEntityObject("Persons", User1.PersonID)

                If sAction = "Edit" Then
                    EditPersonAddress(oPerson, sType, iSequence)
                Else
                    ' AddAddressToPerson(oPerson, txtName.Text)
                    AddAddressToPerson(oPerson, cmbAddressName.SelectedItem.Text)
                End If

                If Request.QueryString("IsHome") IsNot Nothing Then
                    If Convert.ToString(Request.QueryString("IsHome")) = "Y" Then
                        User1.SaveValuesToSessionObject(Page.Session)
                        If Request.QueryString("ReturnToPage") = "" Then
                            Response.Redirect(AddEditAddressButtonPage & "?Type=" & Request.QueryString("Type"), False)
                            Exit Sub
                        Else
                            If Not Request.QueryString("CA") Is Nothing Then
                                Response.Redirect(AddEditAddressButtonPage & "?Type=" & Request.QueryString("Type") & "&ReturnToPage=" & Request.QueryString("ReturnToPage") & "&CA=1", False)
                                Exit Sub
                            Else
                                Response.Redirect(AddEditAddressButtonPage & "?Type=" & Request.QueryString("Type") & "&ReturnToPage=" & Request.QueryString("ReturnToPage"), False)
                                Exit Sub
                            End If
                        End If
                    Else
                        If oPerson.Save(False) Then
                            Select Case sType
                                Case "Home"
                                    SetUserObjectAddress("Home")
                                Case "Main"
                                    SetUserObjectAddress("Main")
                                Case "Billing"
                                    SetUserObjectAddress("Billing")
                                Case "POBox"
                                    SetUserObjectAddress("POBox")
                                Case Else
                                    ' do not need to do this for sub-type items
                                    ' only values stored in the user object are
                                    ' from the top-level entity
                            End Select
                            User1.SaveValuesToSessionObject(Page.Session)
                            If Request.QueryString("ReturnToPage") = "" Then
                                Response.Redirect(AddEditAddressButtonPage & "?Type=" & Request.QueryString("Type"), False)
                                Exit Sub
                            Else
                                If Not Request.QueryString("CA") Is Nothing Then
                                    Response.Redirect(AddEditAddressButtonPage & "?Type=" & Request.QueryString("Type") & "&ReturnToPage=" & Request.QueryString("ReturnToPage") & "&CA=1", False)
                                    Exit Sub
                                Else
                                    Response.Redirect(AddEditAddressButtonPage & "?Type=" & Request.QueryString("Type") & "&ReturnToPage=" & Request.QueryString("ReturnToPage"), False)
                                    Exit Sub
                                End If
                            End If
                        Else
                            lblError.Visible = True
                            lblError.Text = "Error Adding Address: " & oPerson.LastError
                        End If
                    End If
                Else
                    If Request.QueryString("ReturnToPage") = "" Then
                        Response.Redirect(AddEditAddressButtonPage & "?Type=" & Request.QueryString("Type"), False)
                        Exit Sub
                    Else
                        If Not Request.QueryString("CA") Is Nothing Then
                            Response.Redirect(AddEditAddressButtonPage & "?Type=" & Request.QueryString("Type") & "&ReturnToPage=" & Request.QueryString("ReturnToPage") & "&CA=1", False)
                            Exit Sub
                        Else
                            Response.Redirect(AddEditAddressButtonPage & "?Type=" & Request.QueryString("Type") & "&ReturnToPage=" & Request.QueryString("ReturnToPage"), False)
                            Exit Sub
                        End If
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub EditPersonAddress(ByRef oPerson As AptifyGenericEntityBase, _
                                      ByVal sType As String, _
                                      ByVal iSequence As Integer)
            Select Case sType
                Case "Home"
                    SetPersonObjectAddress(oPerson, "Home")
                Case "Main"
                    SetPersonObjectAddress(oPerson, "")
                Case "Billing"
                    SetPersonObjectAddress(oPerson, "Billing")
                Case "POBox"
                    SetPersonObjectAddress(oPerson, "POBox")
                Case Else
                    ' person address sub-type
                    SetPersonObjectAddress(oPerson.SubTypes("PersonAddress").Item(iSequence - 1), "", Me.txtName.Text)
            End Select
        End Sub

        Private Sub SetPersonObjectAddress(ByRef oPerson As AptifyGenericEntityBase, _
                                            ByVal sPrefix As String, _
                                            Optional ByVal AddressName As String = "")
            '01/22/08 Tamasa added for issue 5222(Bug Fixes).
            Dim bIsSubType As Boolean = False
            Dim oAddress As AptifyGenericEntityBase
            Dim sError As String = "Its error"
            Try
                If Request.QueryString("IsHome") IsNot Nothing Then
                    If Convert.ToString(Request.QueryString("IsHome")) = "Y" Then
                        Dim oPerson2 As AptifyGenericEntityBase
                        oPerson2 = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                        oPerson2.SetValue("HomeAddressLine1", txtAddressLine1.Text.Trim)
                        oPerson2.SetValue("HomeAddressLine2", txtAddressLine2.Text.Trim)
                        oPerson2.SetValue("HomeAddressLine3", txtAddressLine3.Text.Trim)
                        oPerson2.SetValue("HomeCity", txtCity.Text.Trim)
                        'oPerson2.SetValue("HomeState", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")))
                        oPerson2.SetValue("HomeZipCode", txtZipCode.Text.Trim)
                        oPerson2.SetValue("HomeCountryCodeID", cmbCountry.SelectedValue.ToString)
                        oPerson2.SetValue("HomeCountry", cmbCountry.SelectedItem.Text)
                        oPerson2.SetValue("PreferredAddress", cmbType.SelectedItem.Text)
                        oPerson2.SetValue("PreferredBillingAddress", cmbType.SelectedItem.Text)
                        oPerson2.SetValue("PreferredShippingAddress", cmbType.SelectedItem.Text)
                        'Redmine 16223
                        oPerson2.SetValue("HomeCounty", txtCounty.Text)
 Dim PreferredCurrencyTypeID As Integer = 0
                         If cmbCountry.SelectedIndex > 0 Then
                             Dim sSql As String = "..spGetPreferredCurrencyTypeIDByCountry__c @CountryID=" & Convert.ToInt32(cmbCountry.SelectedValue.ToString)
                             PreferredCurrencyTypeID  = Convert.ToInt32(DataAction.ExecuteScalar(sSql))
                             oPerson2.SetValue("PreferredCurrencyTypeID", PreferredCurrencyTypeID)
                             oPerson2.SetValue("CreditLimitCurrencyTypeID", PreferredCurrencyTypeID)
                         End If
                        If oPerson2.Save(False) Then
                            'Dim oWebUser As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                            'oWebUser = AptifyApplication.GetEntityObject("Web Users", User1.UserID)
                           User1.SetValue("HomeAddressLine1", txtAddressLine1.Text.Trim)
                            User1.SetValue("HomeAddressLine2", txtAddressLine2.Text.Trim)
                            User1.SetValue("HomeAddressLine3", txtAddressLine3.Text.Trim)
                            User1.SetValue("HomeCity", txtCity.Text.Trim)
                            'User1.SetValue("HomeState", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")))
                            User1.SetValue("HomeZipCode", txtZipCode.Text.Trim)
                            User1.SetValue("HomeCountryCodeID", cmbCountry.SelectedValue.ToString) '11/27/07,Added by Tamasa,Issue 5222.
                            User1.SetValue("HomeCountry", cmbCountry.SelectedItem.Text)
                            User1.SetValue("PreferredAddress", "Home Address")
                            User1.SetValue("PreferredBillingAddress", "Home Address")
                            User1.SetValue("PreferredShippingAddress", "Home Address")
                             'Redmine 16223
                            User1.SetValue("HomeCounty", txtCounty.Text)
If cmbCountry.SelectedIndex > 0 Then
                                User1.SetValue("PreferredCurrencyTypeID", PreferredCurrencyTypeID)
                                User1.SetValue("CreditLimitCurrencyTypeID", PreferredCurrencyTypeID)
                            End If
                            User1.Save()
                        End If
                    End If
                Else
                    If String.Compare(oPerson.EntityName, "Persons", True) = 0 Then
                        oAddress = oPerson.Fields(sPrefix & "AddressID").EmbeddedObject
                    ElseIf String.Compare(oPerson.EntityName, "PersonAddress", True) = 0 Then
                        oAddress = oPerson.Fields("AddressID").EmbeddedObject
                        bIsSubType = True
                        oAddress.SetValue("AddressTypeID", cmbType.SelectedItem.Value)
                        oPerson.SetValue("Name", AddressName)
                    Else
                        oAddress = oPerson.Fields("AddressID").EmbeddedObject
                    End If

                    With oAddress
                        .SetValue("Line1", txtAddressLine1.Text)
                        .SetValue("Line2", txtAddressLine2.Text)
                        .SetValue("Line3", txtAddressLine3.Text)
                        .SetValue("City", txtCity.Text)
                        '.SetValue("StateProvince", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")))
                        .SetValue("PostalCode", txtZipCode.Text)
                         'Redmine 16223
                        .SetValue("County", txtCounty.Text)
                        .SetValue("CountryCodeID", cmbCountry.SelectedValue.ToString)
                        .SetValue("Country", cmbCountry.SelectedItem.Text)
                    End With


                    If Not bIsSubType Then
                        If Not oPerson.Save(False) Then _
                                Throw New ApplicationException("Unable to save the Person's Address changes.  Error: " & oPerson.LastError)
                    Else
                        If Not oPerson.ParentGE.Save(False) Then _
                                Throw New ApplicationException("Unable to save the Person's Address changes.  Error: " & oPerson.ParentGE.LastError)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub SetUserObjectAddress(ByVal sPrefix As String)

            Try
                'Code Commented and added by Suvarna for IssueID- 13178
                If UCase(sPrefix) = UCase("POBox") Then
                    'AddressLine1 is "POBox" in table hence added. 
                    User1.SetValue("POBox", txtAddressLine1.Text)
                    User1.SetValue("POBoxLine2", txtAddressLine2.Text)
                    User1.SetValue("POBoxLine3", txtAddressLine3.Text)
                    User1.SetValue(sPrefix & "City", txtCity.Text)
                    User1.SetValue(sPrefix & "State", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")))
                    User1.SetValue(sPrefix & "ZipCode", txtZipCode.Text)
                    User1.SetValue(sPrefix & "Country", cmbCountry.SelectedItem.Text)
                    User1.SetAddValue(sPrefix & "CountryCodeID", cmbCountry.SelectedValue.ToString)
                ElseIf UCase(sPrefix) = UCase("Main") Then
                    User1.SetValue("AddressLine1", txtAddressLine1.Text)
                    User1.SetValue("AddressLine2", txtAddressLine2.Text)
                    User1.SetValue("AddressLine3", txtAddressLine3.Text)
                    User1.SetValue("City", txtCity.Text)
                    User1.SetValue("State", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")))
                    User1.SetValue("ZipCode", txtZipCode.Text)
                    User1.SetValue("Country", cmbCountry.SelectedItem.Text)
                    User1.SetAddValue("CountryCodeID", cmbCountry.SelectedValue.ToString)
                Else
                    User1.SetValue(sPrefix & "AddressLine1", txtAddressLine1.Text)
                    User1.SetValue(sPrefix & "AddressLine2", txtAddressLine2.Text)
                    User1.SetValue(sPrefix & "AddressLine3", txtAddressLine3.Text)
                    User1.SetValue(sPrefix & "City", txtCity.Text)
                    User1.SetValue(sPrefix & "State", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")))
                    User1.SetValue(sPrefix & "ZipCode", txtZipCode.Text)
                    User1.SetValue(sPrefix & "Country", cmbCountry.SelectedItem.Text)
                    User1.SetAddValue(sPrefix & "CountryCodeID", cmbCountry.SelectedValue.ToString)
                End If

                User1.Save()

                'If sPrefix = "POBox" Then
                '    User1.SetValue("POBoxLine1", txtAddressLine1.Text)
                '    User1.SetValue("POBoxLine2", txtAddressLine2.Text)
                '    User1.SetValue("POBoxLine3", txtAddressLine3.Text)
                'Else
                '    User1.SetValue(sPrefix & "AddressLine1", txtAddressLine1.Text)
                '    User1.SetValue(sPrefix & "AddressLine2", txtAddressLine2.Text)
                '    User1.SetValue(sPrefix & "AddressLine3", txtAddressLine3.Text)
                'End If
                'User1.SetValue(sPrefix & "City", txtCity.Text)
                'User1.SetValue(sPrefix & "State", CStr(IIf(cmbState.SelectedIndex >= 0, cmbState.SelectedItem.Text, "")))
                'User1.SetValue(sPrefix & "ZipCode", txtZipCode.Text)
                'User1.SetValue(sPrefix & "Country", cmbCountry.SelectedItem.Text)
                'User1.SetAddValue(sPrefix & "CountryCodeID", cmbCountry.SelectedValue.ToString)
                'User1.Save()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub AddAddressToPerson(ByRef oPerson As AptifyGenericEntityBase, ByVal AddressName As String)
            Try
                Me.SetPersonObjectAddress(oPerson.SubTypes("PersonAddress").Add, "", AddressName)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        '11/27/07,Added by Tamasa,Issue 5222.
        Private Sub PopulateState()
            Try
                Dim sSQL As String
                sSQL = Database & "..spGetStateList @CountryID=" & cmbCountry.SelectedValue.ToString
                cmbState.DataSource = DataAction.GetDataTable(sSQL)
                cmbState.DataTextField = "State"
                cmbState.DataValueField = "State"
                cmbState.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        '11/27/07,Added by Tamasa,Issue 5222.
        Protected Sub cmbCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCountry.SelectedIndexChanged
            PopulateState()
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
        Private Sub PopulateStateAsCountry(ByRef cmbState As DropDownList, ByRef cmbCurrentCountry As DropDownList)
            Try
                Dim sSQL As String
                sSQL = Database & "..spGetStateList @CountryID=" & cmbCurrentCountry.SelectedValue.ToString
                cmbState.DataSource = DataAction.GetDataTable(sSQL)
                cmbState.DataTextField = "State"
                cmbState.DataValueField = "State"
                cmbState.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class

End Namespace
