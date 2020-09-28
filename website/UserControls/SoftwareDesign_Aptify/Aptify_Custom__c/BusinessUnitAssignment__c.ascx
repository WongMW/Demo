<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/BusinessUnitAssignment__c.ascx.vb"
    Inherits="BusinessUnitAssignment__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<cc1:User runat="server" ID="User1" />
<script src="../../Ebusiness/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(document).ready(function () {
        var PanelState1 = $('#<%= hdnBUState.ClientID %>').val();
        //$("#hdnBUState").val();
        if (PanelState1 == '1') {
            $('#PannelFieldsList').removeClass("collapse").addClass("active");
            $('#' + 'divSearchbtn').removeClass("collapse").addClass("active");
            $('#' + 'divCollapsebtn').removeClass("collapse").addClass("active");
            // $('#<%= ImgHiddenFieldsList.ClientID %>').hide();
            //$("#div1").css("display","block");
        }

    });

    function Clear() {
        if ($('#<%= txtSearch.ClientID %>').val() == "Search...") {
            $('#<%= txtSearch.ClientID %>').val("");
        }
    }
    function SetText() {

        if ($('#<%= txtSearch.ClientID %>').val() == "") {
            $('#<%= txtSearch.ClientID %>').val("Search...");
        }
    }

    function CollapseExpand(me, HiddenPanelState) {
        var Panelstate = $('#' + me).attr("class");
        $('#' + me).slideToggle('slow');

        if (Panelstate == "collapse") {
            $('#' + me).removeClass("collapse").addClass("active");
            $('#' + 'divSearchbtn').removeClass("collapse").addClass("active");
            $('#' + 'divExpandbtn').removeClass("active").addClass("collapse");
            $('#' + 'divCollapsebtn').removeClass("collapse").addClass("active");
            SetPanelState(HiddenPanelState, 1)
        }
        else {

            $('#' + me).removeClass("active").addClass("collapse");
            $('#' + 'divSearchbtn').removeClass("active").addClass("collapse");
            $('#' + 'divCollapsebtn').removeClass("active").addClass("collapse");
            $('#' + 'divExpandbtn').removeClass("collapse").addClass("active");
            SetPanelState(HiddenPanelState, 0)
        }
    }
    function SetPanelState(HiddenPanelState, StateValue) {
        if (HiddenPanelState == 'hdnBUState') {
            $('#<%= hdnBUState.ClientID %>').val(StateValue);
        }
    }

</script>
<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="tdProcessing" style="vertical-align: middle">
                Please wait...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="main-container">
    <div class="cai-form">
        <span class="form-title">Business unit
             <span style="float: right;">
                 <span id="divExpandbtn" class="collapse">
                     <asp:Image ID="ImgHiddenFieldsList" runat="server" onclick="CollapseExpand('PannelFieldsList','hdnBUState')"
                         ImageUrl="~/Images/downarrow.jpg" />
                 </span>
                 <span id="divCollapsebtn" class="collapse">
                     <asp:Image ID="ImgHiddenFieldsList2" runat="server" onclick="CollapseExpand('PannelFieldsList','hdnBUState')"
                         ImageUrl="~/Images/uparrow.jpg" />
                 </span>
             </span>
            <asp:HiddenField ID="hdnBUState" Value="1" runat="server" />
        </span>

        <div id="PannelFieldsList" class="collapse cai-form-content">
            <div class="mentor-options">
                <asp:Button runat="server" ID="lnkShowAll" CssClass="submitBtn" Text="Show All" />

                <div class="form-search-box">
                    <asp:TextBox ID="txtSearch" Text="Search..." AutoPostBack="false"
                        onblur="SetText();" onclick="Clear();" runat="server"></asp:TextBox>

                    <div id="divSearchbtn" class="collapse">
                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" class="submitBtn" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>

                <asp:UpdatePanel ID="UpAreasOfExp" UpdateMode="Always" runat="server">
                    <ContentTemplate>
                        <telerik:RadListBox ID="radBusinessUnit" SelectionMode="Single" runat="server">
                        </telerik:RadListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
        </div>
    </div>

    <div class="cai-form">
        <span class="form-title">Students</span>

        <div class="cai-form-content">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                <ContentTemplate>
                    <div>
                        <div>
                            <asp:Label ID="lblError" ForeColor="#ff0000" runat="server" Visible="False" />
                        </div>

                        <div class="actions">
                            <asp:Button ID="btnThisBU" runat="server" Text="This unit"
                                class="submitBtn" />
                            Please click to show students assigned just to the business unit, as selected above
                        </div>

                        <div class="actions">
                            <asp:Button ID="btnShowAll" runat="server" Text="Show All" class="submitBtn" />
                            Please click to show all students
                        </div>

                        <div>
                            <telerik:RadGrid ID="radStudent" runat="server" AllowPaging="false" AllowSorting="True"
                                AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AutoGenerateColumns="false"
                                Visible="true" CssClass="cai-table mobile-table" ShowHeadersWhenNoRecords="true">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView AllowSorting="true" AllowNaturalSort="false" EnableNoRecordsTemplate="true"
                                    AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" TableLayout="Fixed">
                                    <NoRecordsTemplate>
                                        <asp:Label ID="lblNoRecord" runat="server" Text="No record found" Font-Bold="true"
                                            ForeColor="#ff0000"></asp:Label>
                                    </NoRecordsTemplate>
                                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column"
                                        Visible="True">
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn  HeaderText="" ItemStyle-HorizontalAlign="Left"
                                            ShowFilterIcon="false" AllowFiltering="false" UniqueName="SelectAll">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllStudent" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                    AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" OnCheckedChanged="ToggleRowSelection"
                                                    Checked='<%#IIf(Eval("Ischeck")=1,true,false) %>' />
                                                <asp:HiddenField runat="server" ID="hdBLID" Value='<%# Eval("BusinessUnitLinkID")%>' />
                                                <asp:HiddenField runat="server" ID="hidCompanyID" Value='<%# Eval("CompanyID")%>' />
                                                <asp:HiddenField runat="server" ID="hidStudentID" Value='<%# Eval("StudentNo")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                            DataField="OldID"  HeaderText="Student no." ItemStyle-HorizontalAlign="Left"
                                            ShowFilterIcon="false" SortExpression="OldID"  HeaderStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <span class="mobile-label">Student no#:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "OldID")%>'/>
                                             </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            DataField="FirstLast"   HeaderText="Student name"
                                            ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="FirstLast"
                                            HeaderStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <span class="mobile-label">Student name:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "FirstLast")%>'/>
                                             </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            DataField="email1"   HeaderText="Email"
                                            ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="email1"
                                            HeaderStyle-HorizontalAlign="left">
                                           <ItemTemplate>
                                                <span class="mobile-label">Email:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "email1")%>'/>
                                             </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            DataField="CompName"  HeaderText="Office" ItemStyle-HorizontalAlign="Left"
                                            ShowFilterIcon="false" SortExpression="CompName"  HeaderStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <span class="mobile-label">Office:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "CompName")%>'/>
                                             </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Bus. unit" DataField="BusinessUnit" SortExpression="BusinessUnit"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" >
                                            <ItemTemplate>
                                                <span class="mobile-label">Bus. unit:</span>
                                                <asp:DropDownList ID="cmbBusinessUnit" runat="server" CssClass="cai-table-data">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hidBusinessUnit" Value='<%# DataBinder.Eval(Container.DataItem,"BusinessUnitID") %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            DataField="MentorName"  HeaderText="Mentor" 
                                            ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="MentorName"
                                            HeaderStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <span class="mobile-label">Mentor:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"MentorName") %>'/>
                                             </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                        </EditColumn>
                                    </EditFormSettings>
                                </MasterTableView>
                                <FilterMenu EnableImageSprites="False">
                                </FilterMenu>
                            </telerik:RadGrid>
                        </div>

                        <div class="actions">
                            <asp:Button runat="server" ID="btnMainBack" CssClass="submitBtn" Text="Back" />
                            <asp:Button runat="server" ID="btnSubmit" CssClass="submitBtn" Text="Submit" />
                        </div>
                    </div>
                    <div>
                        <telerik:RadWindow ID="radWindowValidation" runat="server"  Modal="True"
                            VisibleStatusbar="False" Behaviors="None"
                            Title="Business unit assignment" Behavior="None" Height="150px">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblValidationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <br />
                                            </div>
                                            <div>
                                                <asp:Button ID="btnValidationOK" runat="server" Text="Ok"  class="submitBtn" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </telerik:RadWindow>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
