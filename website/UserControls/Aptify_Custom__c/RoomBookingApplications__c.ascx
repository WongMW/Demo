<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.RoomBookingApplications__cClass"
    CodeFile="RoomBookingApplications__c.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<style type="text/css">
    #Table1
    {
        width: 719px;
    }
    .table
    {
        border-spacing: 5px 5px;
    }
    .style5
    {
        width: 11%;
    }
</style>
<script type="text/javascript">

    function ClientItemSelected(sender, e) {
        document.getElementById("<%=hfPersonID.ClientID %>").value = e.get_value();
    }
    function HideShowPersonDetails() {
        var chkOther = document.getElementById("<%=chkOther.ClientID %>")
        var txtOnBehalf = document.getElementById("<%=txtOnBehalfOf.ClientID %>")
        if (chkOther.checked == true) {
            document.getElementById("personDetails").style.display = "inline";
            txtOnBehalf.value = '';
            txtOnBehalf.disabled = true
            ValidatorEnable(document.getElementById("<%=rfvFirstName.ClientID %>"), true);
            ValidatorEnable(document.getElementById("<%=rfvLastName.ClientID %>"), true);
        }
        else if (chkOther.checked == false) {
            document.getElementById("personDetails").style.display = "none";
            txtOnBehalf.disabled = false
            ValidatorEnable(document.getElementById("<%=rfvFirstName.ClientID %>"), false);
            ValidatorEnable(document.getElementById("<%=rfvLastName.ClientID %>"), false);
        }
    }
    function fnClearHidden() {
        document.getElementById("<%=hfPersonID.ClientID %>").value = '';
    }
    function compareDateTime(sender, args) {

        var start = document.getElementById("<%=rdStartdate.ClientID %>").value;
        var end = document.getElementById("<%=rdEnddate.ClientID %>").value;
        args.IsValid = (end >= start);
    }
    function compareTime(sender, args) {
        var start = document.getElementById("<%=Starttime.ClientID %>");
        var end = document.getElementById("<%=EndTime.ClientID %>");
        var starttime = new Date(0, 0, 0, start.value.substring(11, 13), start.value.substring(14, 16));
        var endtime = new Date(0, 0, 0, end.value.substring(11, 13), end.value.substring(14, 16));
        args.IsValid = (endtime >= starttime);
    }
</script>
<asp:HiddenField ID="hfPersonID" runat="server" Value="" />
<div class="content-container clearfix">
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>Room Booking Details</b><br />
                <table width="100%" class="table">
                    <tr>
                        <td width="5%">
                        </td>
                        <td width="10%" align="right">
                            Requester :
                        </td>
                        <td width="35%">
                            <asp:TextBox ID="txtRequesterName" runat="server" AptifyDataField="Requester" Width="98%"
                                Enabled="false" />
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td width="5%">
                        </td>
                        <td width="10%" align="right">
                            On Behalf Of :
                        </td>
                        <td width="35%">
                            <asp:TextBox ID="txtOnBehalfOf" runat="server" Width="98%" />
                            <ajax:AutoCompleteExtender ID="acePerson" runat="server" BehaviorID="autoComplete"
                                CompletionInterval="10" CompletionListElementID="divwidth" CompletionSetCount="12"
                                EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="SearchPersonsByCompany"
                                ServicePath="~/WebServices/PersonService.asmx" OnClientItemSelected="ClientItemSelected"
                                TargetControlID="txtOnBehalfOf">
                            </ajax:AutoCompleteExtender>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td width="5%">
                        </td>
                        <td width="10%" align="right">
                            Other :
                        </td>
                        <td width="35%">
                            <asp:CheckBox ID="chkOther" runat="server" onclick="HideShowPersonDetails();" />
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                </table>
                <div id="personDetails" style="display: none">
                    <table width="100%" class="table">
                        <tr>
                            <td width="6%">
                            </td>
                            <td width="9%" align="right">
                                <span class="RequiredField">*</span>First Name :
                            </td>
                            <td width="35%">
                                <asp:TextBox ID="txtFirstName" runat="server" Width="98%"></asp:TextBox>
                            </td>
                            <td width="50%">
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Please Enter First Name"
                                    Display="Dynamic" ControlToValidate="txtFirstName" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="6%">
                            </td>
                            <td width="9%" align="right">
                                <span class="RequiredField">*</span>Last Name :
                            </td>
                            <td width="35%">
                                <asp:TextBox ID="txtLastName" runat="server" Width="98%"></asp:TextBox>
                            </td>
                            <td width="50%">
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Please Enter Last Name"
                                    Display="Dynamic" ControlToValidate="txtFirstName" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="6%">
                            </td>
                            <td width="9%" align="right">
                                Email :
                            </td>
                            <td width="35%">
                                <asp:TextBox ID="txtEmail" runat="server" Width="98%"></asp:TextBox>
                            </td>
                            <td width="50%">
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table width="100%" class="table">
                        <tr>
                            <td width="6%">
                            </td>
                            <td width="9%" align="right">
                                <span class="RequiredField">*</span>Venue :
                            </td>
                            <td width="35%">
                                <asp:DropDownList ID="cmbVenueID" runat="server" Width="99%" />
                            </td>
                            <td width="50%" align="left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="--Select--"
                                    ErrorMessage="Please Select Venue" ForeColor="Red" ControlToValidate="cmbVenueID"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                <span class="RequiredField">*</span> Meeting Title :
                            </td>
                            <td>
                                <asp:TextBox ID="txtMeetingTitle" runat="server" AptifyDataField="MeetingTitle" Width="98%" />
                                <div>
                                </div>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Title"
                                    ForeColor="Red" ControlToValidate="txtMeetingTitle"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                <span class="RequiredField">*</span>Start Date :
                            </td>
                            <td>
                                <telerik:RadDateTimePicker ID="rdStartdate" runat="server" MinDate="01/01/1900" MaxDate="01/01/9999"
                                    Calendar-ShowOtherMonthsDays="false" Calendar-ShowRowHeaders="false" Width="100%">
                                </telerik:RadDateTimePicker>
                                <div>
                                </div>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Start Date"
                                    ForeColor="Red" ControlToValidate="rdStartdate"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                <span class="RequiredField">*</span>End Date :
                            </td>
                            <td>
                                <telerik:RadDateTimePicker ID="rdEnddate" runat="server" MinDate="01/01/1900" MaxDate="01/01/9999"
                                    Calendar-ShowOtherMonthsDays="false" Calendar-ShowRowHeaders="false" Width="100%">
                                </telerik:RadDateTimePicker>
                            </td>
                            <td>
                                <asp:CustomValidator ID="CustomValidator1" ControlToValidate="rdEnddate" Display="Dynamic"
                                    ErrorMessage="End Date Should be greater than Start Date " ForeColor="Red" runat="server"
                                    ClientValidationFunction="compareDateTime" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select End Date"
                                    ForeColor="Red" ControlToValidate="rdEnddate"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                Seats :
                            </td>
                            <td>
                                <asp:TextBox ID="txtSeats" runat="server" AptifyDataField="Seats" Width="98%" />
                            </td>
                            <td>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="GreaterThan"
                                    ControlToValidate="txtSeats" ForeColor="Red" Type="Integer" ErrorMessage="Must be &gt; 0"
                                    ValueToCompare="0"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                <span class="RequiredField">*</span> Room Type :
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbRoomTypeID" runat="server" Width="99%" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="--Select--"
                                    ErrorMessage="Please Select Room Type" ForeColor="Red" ControlToValidate="cmbRoomTypeID"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trStatus" runat="server" visible="true">
                            <td>
                            </td>
                            <td align="right">
                                Status :
                            </td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server" AptifyDataField="Status"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:UpdatePanel ID="upResources" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr id="idResourceInfo" runat="server" visible="false">
                                <td>
                                    <b>Resource Details</b><br />
                                    <div id="ForumDiv" runat="server">
                                        <table width="100%" class="table">
                                            <tr id="trResourceType" runat="server" visible="true">
                                                <td width="5%">
                                                </td>
                                                <td align="right">
                                                    <span class="RequiredField">*</span> Resource Type :
                                                </td>
                                                <td width="35%">
                                                    <asp:DropDownList ID="cmbResourceType" runat="server" AutoPostBack="true" Width="99%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="50%">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" InitialValue="--Select--"
                                                        runat="server" ErrorMessage="Please Select Resources Type" ForeColor="Red" ControlToValidate="cmbResourceType"
                                                        ValidationGroup="Resources"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <span class="RequiredField">*</span> Resources :
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="cmbResource" runat="server" Width="99%">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cmbResourceType" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="--Select--"
                                                        runat="server" ErrorMessage="Please Select Resources" ForeColor="Red" ControlToValidate="cmbResource"
                                                        ValidationGroup="Resources"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table width="100%" class="table">
                                        <tr>
                                            <td width="7%">
                                            </td>
                                            <td width="8%" align="right">
                                                <span class="RequiredField">*</span> Start Time :
                                            </td>
                                            <td width="35%">
                                                <telerik:RadTimePicker runat="server" ID="StartTime" Width="99%">
                                                </telerik:RadTimePicker>
                                            </td>
                                            <td width="50%">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Start Time"
                                                    ValidationGroup="Resources" ForeColor="Red" ControlToValidate="StartTime"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <span class="RequiredField">*</span> End Time :
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="EndTime" Width="99%">
                                                </telerik:RadTimePicker>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select End Time"
                                                    ValidationGroup="Resources" ForeColor="Red" ControlToValidate="EndTime"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="End time should be greater than Start"
                                                    ValidationGroup="Resources" ClientValidationFunction="compareTime" ControlToValidate="EndTime"
                                                    ForeColor="Red"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <span class="RequiredField">*</span> Quantity :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" Width="98%" runat="server" Text="0"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="GreaterThan"
                                                    ControlToValidate="txtQuantity" ValidationGroup="Resources" ForeColor="Red" Type="Integer"
                                                    ErrorMessage="Must be &gt; 0" ValueToCompare="0"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                Comments :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Style="resize: none;"
                                                    Height="60px" Width="99%"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add Resources" ValidationGroup="Resources" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <telerik:RadGrid ID="grdResourceDetails" AutoGenerateColumns="False" runat="server"
                                        SortingSettings-SortedDescToolTip="Sorted Descending" AllowPaging="true" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        AllowFilteringByColumn="true" PageSize="5" Skin="Sunset" Width="100%" PagerStyle-PageSizeLabelText="Records Per Page"
                                        Visible="false">
                                        <GroupingSettings CaseSensitive="false" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="ResourceDetails" AllowFiltering="false" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResourceTypeID" runat="server" Text='<%# Eval("ResourceTypeID")%>'></asp:Label>
                                                        <asp:Label ID="lblResourceID" runat="server" Text='<%# Eval("ResourceID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Resource Type" DataField="ResourceTypeName"
                                                    HeaderStyle-Width="15%" ItemStyle-Width="15%" />
                                                <telerik:GridBoundColumn HeaderText=" Resource" DataField="Name" Visible="True" AllowFiltering="true"
                                                    SortExpression="Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" HeaderStyle-Width="15%" ItemStyle-Width="15%" />
                                                <telerik:GridTemplateColumn HeaderText="Start Time" DataField="StartTime" Visible="True"
                                                    AllowFiltering="true" SortExpression="StartTime" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="10%"
                                                    ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStartTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StartTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="End Time" DataField="EndTime" Visible="True"
                                                    AllowFiltering="true" SortExpression="EndTime" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEndTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EndTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Quantity" DataField="Quantity" Visible="True"
                                                    AllowFiltering="true" SortExpression="Quantity" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# String.Format("{0:0.00}",DataBinder.Eval(Container.DataItem,"Quantity")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Comments" AllowFiltering="false" HeaderStyle-Width="30%"
                                                    ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Comments") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Remove" AllowFiltering="false" HeaderStyle-Width="10%"
                                                    ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnRemove" class="submitBtn" runat="server" Text="Remove" AutoPostBack="true"
                                                            CausesValidation="false" CommandName="Delete" CommandArgument='<%# Container.ItemIndex% %>'>
                                                        </asp:Button>
                                                        <%--<asp:Button ID="btnRemove" class="submitBtn" runat="server" Text="Remove" AutoPostBack="true"
                                        OnClick="RemoveCall" CommandArgument='<%# CType(Container,GridViewRow).RowIndex %>'></asp:Button>--%>
                                                        <%--  <asp:Label ID="lblResourceDetailID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr id="trAdditionalRe" runat="server" visible="false">
                                <td>
                                    Additional Resource :
                                    <asp:TextBox ID="txtAdditionalResource" runat="server" TextMode="MultiLine" Width="350px"
                                        Style="resize: none;" Height="60px" />
                                </td>
                            </tr>
                            <tr id="trNote" runat="server">
                                <td>
                                    <asp:Label ID="lblNote" runat="server" Text=""></asp:Label><br />
                                    <asp:CheckBox ID="chkTerms" runat="server" Text="Terms and Conditions" />
                                </td>
                            </tr>
                        </table>
                        <telerik:RadWindow ID="radAlert" runat="server" Width="350px" Height="100px" Modal="True"
                            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
                            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Remove from Resource list"
                            Behavior="None">
                            <ContentTemplate>
                                <table class="tblEditAtendee" width="100%" cellpadding="0" cellspacing="10">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblWarning" runat="server" Font-Bold="true" Text="Do you want to Delete?"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnOk" runat="server" Text="Yes" class="submitBtn" ValidationGroup="ok"
                                                Width="50px" />&nbsp;
                                            <asp:Button ID="btnNo" runat="server" Text="No" class="submitBtn" ValidationGroup="ok"
                                                Width="50px" />
                                            <asp:Label ID="lblCurrentTableID" runat="server" Text="-1" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </telerik:RadWindow>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="cmdSave" runat="server" Text="Save"></asp:Button>&nbsp;
                <asp:Button ID="cmdSubmit" runat="server" Text="Submit"></asp:Button>&nbsp;
                <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CausesValidation="false">
                </asp:Button>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
</div>
