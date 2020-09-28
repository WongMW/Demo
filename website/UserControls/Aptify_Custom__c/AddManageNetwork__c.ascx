<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AddManageNetwork__c.ascx.vb"
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
            <table runat="server" id="tblMain" class="data-form">
                <tr>
                    <td>
                        <b>Manage Your Network :
                            <asp:Label ID="lblNetName" runat="server"></asp:Label>
                        </b>
                    </td>
                </tr>
            </table>
              <table >
            <tr>
                <td colspan="2" align="left">
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Green"></asp:Label>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td align="right" valign="top" nowrap="nowrap">
                    <%--  <asp:Label ID="Label17" runat="server" Text="Network Name"></asp:Label>--%>
                    <span class="RequiredField">*</span> Network Name :
                </td>
                <td align="left" valign="top">
                    <asp:TextBox ID="txtNetworkName" runat="server" CssClass="textbox" MaxLength="100"
                        Width="220px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNetworkName"
                        Display="Dynamic" ErrorMessage="Enter Network Name" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblTermID" runat="server" Visible="false" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td align="right" valign="top">
                    <%-- <asp:Label ID="Label12" runat="server" Text="Start Date"></asp:Label>--%>
                    <span class="RequiredField">*</span> Start Date :
                </td>
                <td align="left" valign="top">
                    <%--  <asp:TextBox ID="txtStartDate" runat="server" Height="16px" CssClass="textbox" />
                <asp:ImageButton ID="ibSDPH" AlternateText="Calendar" runat="server" ImageAlign="AbsMiddle"
                    CausesValidation="false" ImageUrl="~/Images/btn_calendar.gif" Height="19px" Width="25px" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true"
                    PopupButtonID="ibSDPH"  TargetControlID="txtStartDate" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>--%>
                    <Telerik:RadDatePicker ID="rdStartdate" runat="server" Calendar-ShowOtherMonthsDays="false"
                        MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                    </Telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Start Date"
                        ForeColor="Red" ControlToValidate="rdStartdate"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td align="right" valign="top">
                    <%--<asp:Label ID="Label3" runat="server" Text="End Date"></asp:Label>--%>
                    End Date :
                </td>
                <td align="left" valign="top">
                    <%--  <asp:TextBox ID="txtEndDate" runat="server" Height="16px" CssClass="textbox" />
                <asp:ImageButton ID="ibSDPH1" AlternateText="Calendar" runat="server" ImageAlign="AbsMiddle"
                    CausesValidation="false" ImageUrl="~/Images/btn_calendar.gif" Height="19px" Width="25px" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true"
                    PopupButtonID="ibSDPH1"  TargetControlID="txtEndDate" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>--%>
                    <Telerik:RadDatePicker ID="rdEnddate" runat="server" Calendar-ShowOtherMonthsDays="false"
                        MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                    </Telerik:RadDatePicker>
                    <asp:CompareValidator runat="server" ID="cmpCalenders" ControlToValidate="rdEnddate"
                        ForeColor="Red" ControlToCompare="rdStartdate" Operator="GreaterThan" Type="Date"
                        ErrorMessage="End Date Should be greater than Start Date" SetFocusOnError="True" />
                </td>
            </tr>
            <tr style="height: 40px;">
                <td align="right" valign="top">
                    <%-- <asp:Label ID="Label5"  runat="server" Text="Summary"></asp:Label>--%>
                    Summary :
                </td>
                <td align="left" valign="top">
                    <asp:TextBox ID="txtSummary" runat="server" CssClass="textbox" MaxLength="100" TextMode="MultiLine"
                        Width="220px" ValidationGroup="grpPBE" Style="resize: none"  height="80px"/><br /><br />
                </td>
            </tr>
            <tr style="height: 25px;">
                <td align="right" valign="top">
                    <%-- <asp:Label ID="Label1" runat="server" text="Select Person to add" ></asp:Label>--%>
                    Person :
                </td>
                <td align="left" valign="top">
                    <asp:TextBox ID="txtPersonMember" runat="server" CssClass="textbox" MaxLength="100"
                        Width="220px" onFocus="fnClearHidden()" />
                    <ajaxToolkit:AutoCompleteExtender ID="autoPerson" runat="server" TargetControlID="txtPersonMember"
                        BehaviorID="auto1" ServicePath="~/WebServices/PersonService.asmx" EnableCaching="false"
                        MinimumPrefixLength="1" FirstRowSelected="true" ServiceMethod="SearchPersons"
                        UseContextKey="true" CompletionSetCount="10" OnClientItemSelected="ClientItemSelected"
                        CompletionListHighlightedItemCssClass="GridViewHeader" />
                </td>
            </tr>
            <tr style="height: 25px;">
                <td>
                </td>
                <td align="Left">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add to Network" class="submitBtn" />
                </td>
            </tr>
            <tr style="height: 25px;">
                <td>
                    <asp:Label ID="Label2" runat="server" Visible="False" Text="List of Members on this Network"></asp:Label>
                </td>
            </tr>
        </table>
        <Telerik:RadGrid ID="grdMain" AutoGenerateColumns="False" runat="server" SortingSettings-SortedDescToolTip="Sorted Descending"
            AllowPaging="true" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn="true"
            PageSize="5" Skin="Sunset" Width="100%" PagerStyle-PageSizeLabelText="Records Per Page">
            <GroupingSettings CaseSensitive="false" />
            <GroupingSettings CaseSensitive="false" />
            <GroupingSettings CaseSensitive="false" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                <Columns>
                    <Telerik:GridTemplateColumn HeaderText="CTMID" AllowFiltering="false" DataField="ID"
                        Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCTMID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                        </ItemTemplate>
                    </Telerik:GridTemplateColumn>
                    <Telerik:GridBoundColumn HeaderText="ID" DataField="CommitteeTermID" Visible="false" />
                    <Telerik:GridBoundColumn HeaderText="Name" DataField="MemberID_Name" Visible="True"
                        AllowFiltering="true" SortExpression="Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        ShowFilterIcon="false" HeaderStyle-Width="25%" ItemStyle-Width="25%" />
                    <Telerik:GridTemplateColumn HeaderText="MemberID" ShowFilterIcon="false" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblMemberID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MemberID") %>'></asp:Label>
                            <asp:Label ID="lblRoleID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RoleID") %>'></asp:Label>
                            <asp:Label ID="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StartDate") %>'></asp:Label>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EndDate") %>'></asp:Label>
                        </ItemTemplate>
                    </Telerik:GridTemplateColumn>
                    <Telerik:GridBoundColumn HeaderText="Role" DataField="Role" Visible="True" AllowFiltering="true"
                        SortExpression="Role" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        ShowFilterIcon="false" HeaderStyle-Width="14%" ItemStyle-Width="14%" />
                    <Telerik:GridBoundColumn HeaderText="Start Date" DataField="StartDate" Visible="True"
                        AllowFiltering="true" SortExpression="StartDate" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-Width="8%" />
                    <Telerik:GridBoundColumn HeaderText="End Date" DataField="EndDate" Visible="True"
                        AllowFiltering="true" SortExpression="EndDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        ShowFilterIcon="false" ItemStyle-Width="8%" />
                    <Telerik:GridTemplateColumn HeaderText="Remove" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnRemove" class="submitBtn" runat="server" Text="Remove" AutoPostBack="true"
                                OnClick="RemoveMeFromNetwork"></asp:Button>
                            <asp:Label ID="lblCTID" runat="server" Text='<%# Eval("CommitteeTermID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblctmmID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </Telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </Telerik:RadGrid>
        <asp:Label runat="server" ID="lblError"></asp:Label>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSubmitCT" runat="server" Text="Submit" />
                </td>
                <td>
                    <asp:Button ID="btnbackNet" runat="server" Text="Go to Network" CausesValidation="false"
                        Visible="false" />
                </td>
            </tr>
        </table>
        <Telerik:RadWindow ID="radAlert" runat="server" Width="350px" Height="100px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Remove from Network"
            Behavior="None">
            <ContentTemplate>
                <table class="tblEditAtendee" width="100%" cellpadding="0" cellspacing="10">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblWarning" runat="server" Font-Bold="true" Text="Do you want to remove this member from network?"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnOk" runat="server" Text="Yes" class="submitBtn" ValidationGroup="ok"
                                Width="50px" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="No" class="submitBtn" ValidationGroup="ok"
                                Width="50px" />
                            <asp:Label ID="lblComTID" runat="server" Text="-1" Visible="false"></asp:Label>
                            <asp:Label ID="lblCTMID" runat="server" Text="-1" Visible="false"></asp:Label>
                            <asp:Label ID="lblCurrentTableID" runat="server" Text="-1" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </Telerik:RadWindow>
        </div>
      
    </ContentTemplate>
</asp:UpdatePanel>
  
<cc1:User ID="User1" runat="server" />
