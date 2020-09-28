<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AddManageNetwork__c.ascx.vb"
    Inherits="AddManageNetwork__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script>
    function ClientItemSelected(sender, e) {
        document.getElementById("<%=hdnPersonId.ClientID %>").value = e.get_value();
    }
    function fnClearHidden() {
        document.getElementById("<%=hdnPersonId.ClientID %>").value = '';

    }
</script>
<asp:HiddenField ID="hdnPersonId" runat="server" Value="" />
<asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server">
    <ContentTemplate>
        <div class="content-container clearfix">
            <div runat="server" id="tblMain" class="cai-form">
                <span class="form-title">Manage Your Network</span>
                <asp:Label ID="lblNetName" runat="server"></asp:Label>

                <div class="cai-form-content">
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Green"></asp:Label>

                    <div class="field-group">
                        <span class="label-title"><span class="RequiredField">*</span>Network Name</span>
                        <asp:TextBox ID="txtNetworkName" Width="300px" runat="server" CssClass="textbox" MaxLength="100" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNetworkName"
                            Display="Dynamic" ErrorMessage="Enter Network Name" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblTermID" runat="server" Visible="false" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <span class="label-title"><span class="RequiredField">*</span>Start Date</span>
                        <telerik:RadDatePicker ID="rdStartdate" runat="server" Calendar-ShowOtherMonthsDays="false"
                            MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Start Date"
                            ForeColor="Red" ControlToValidate="rdStartdate"></asp:RequiredFieldValidator>
                    </div>


                    <div class="field-group">
                        <span class="label-title">End Date</span>
                        <telerik:RadDatePicker ID="rdEnddate" runat="server" Calendar-ShowOtherMonthsDays="false"
                            MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                        </telerik:RadDatePicker>
                        <asp:CompareValidator runat="server" ID="cmpCalenders" ControlToValidate="rdEnddate"
                            ForeColor="Red" ControlToCompare="rdStartdate" Operator="GreaterThan" Type="Date"
                            ErrorMessage="End Date Should be greater than Start Date" SetFocusOnError="True" />
                    </div>


                    <div class="field-group">
                        <span class="label-title">Summary</span>
                        <asp:TextBox ID="txtSummary" runat="server" CssClass="textbox" MaxLength="100" TextMode="MultiLine"
                            Width="300px" ValidationGroup="grpPBE" Style="resize: none" Height="80px" />
                    </div>

                    <div class="field-group">
                        <span class="label-title">Person</span>
                        <asp:TextBox ID="txtPersonMember" runat="server" CssClass="textbox" MaxLength="100"
                            Width="300px" onFocus="fnClearHidden()" />
                        <ajaxToolkit:AutoCompleteExtender ID="autoPerson" runat="server" TargetControlID="txtPersonMember"
                            BehaviorID="auto1" ServicePath="~/WebServices/PersonService.asmx" EnableCaching="false"
                            MinimumPrefixLength="1" FirstRowSelected="true" ServiceMethod="SearchPersons"
                            UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientItemSelected"
                            CompletionListHighlightedItemCssClass="GridViewHeader" />
                    </div>

                    <div class="field-group actions">
                        <asp:Button ID="btnSubmit" runat="server" Text="Add to Network" class="submitBtn" />
                        <asp:Label ID="Label2" runat="server" Visible="False" Text="List of Members on this Network"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="title-holder">
                <h2>Network</h2>
            </div>
            <telerik:RadGrid ID="grdMain" AutoGenerateColumns="False" runat="server" SortingSettings-SortedDescToolTip="Sorted Descending"
                AllowPaging="true" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn="true"
                PageSize="5" PagerStyle-PageSizeLabelText="Records Per Page" CssClass="cai-table mobile-table">
                <PagerStyle CssClass="sd-pager" />
                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="CTMID" AllowFiltering="false" DataField="ID"
                            Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCTMID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="ID" DataField="CommitteeTermID" Visible="false" />
                        <telerik:GridTemplateColumn HeaderText="Name" DataField="MemberID_Name" Visible="True"
                            AllowFiltering="true" SortExpression="Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" >
                             <ItemTemplate>
                                <span class="mobile-label">Name:</span>
                                <asp:Label  CssClass="cai-table-data" runat="server" Text='<%# Eval("MemberID_Name")%>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="MemberID" ShowFilterIcon="false" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblMemberID" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MemberID") %>'></asp:Label>
                                <asp:Label ID="lblRoleID" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RoleID") %>'></asp:Label>
                                <asp:Label ID="lblStartDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StartDate") %>'></asp:Label>
                                <asp:Label ID="lblEndDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EndDate") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Role" DataField="Role" Visible="True" AllowFiltering="true"
                            SortExpression="Role" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">Role:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Role")%>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Start Date" DataField="StartDate" Visible="True"
                            AllowFiltering="true" SortExpression="StartDate" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">Start Date:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("StartDate") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="End Date" DataField="EndDate" Visible="True"
                            AllowFiltering="true" SortExpression="EndDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">End Date:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("EndDate") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ItemStyle-CssClass="remove-actions" HeaderText="Remove" AllowFiltering="false">
                            <ItemTemplate>
                                <span class="mobile-label">Remove:</span>
                                <asp:Button ID="btnRemove" class="submitBtn cai-table-data" runat="server" Text="Remove" AutoPostBack="true"
                                    OnClick="RemoveMeFromNetwork"></asp:Button>
                                <asp:Label ID="lblCTID" runat="server" Text='<%# Eval("CommitteeTermID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblctmmID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <asp:Label runat="server" ID="lblError"></asp:Label>
            <div class="actions">
                <asp:Button ID="btnSubmitCT" runat="server" Text="Submit" CssClass="submitBtn" />
                <asp:Button ID="btnbackNet" runat="server" Text="Go to Network" CausesValidation="false" CssClass="submitBtn"
                    Visible="false" />
            </div>
            <telerik:RadWindow ID="radAlert" runat="server" Width="350px" Height="100px" Modal="True"
                Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
                ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Remove from Network"
                Behavior="None" CssClass="pop-up">
                <ContentTemplate>
                    <table class="tblEditAtendee">
                        <tr>
                            <td>
                                <asp:Label ID="lblWarning" runat="server" Font-Bold="true" Text="Do you want to remove this member from network?"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="actions">
                                    <asp:Button ID="btnOk" runat="server" Text="Yes" class="submitBtn" ValidationGroup="ok" />
                                    <asp:Button ID="btnCancel" runat="server" Text="No" class="submitBtn" ValidationGroup="ok" />
                                    <asp:Label ID="lblComTID" runat="server" Text="-1" Visible="false"></asp:Label>
                                    <asp:Label ID="lblCTMID" runat="server" Text="-1" Visible="false"></asp:Label>
                                    <asp:Label ID="lblCurrentTableID" runat="server" Text="-1" Visible="false"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<cc1:User ID="User1" runat="server" />
