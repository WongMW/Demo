<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AssignMentorTM__c.ascx.vb"
    Inherits="AssignMentorTM__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<cc1:User runat="server" ID="User1" />
<script src="../../Ebusiness/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />

<style type="text/css">
    .CompletionListCssClass {
        /*font-size: 15px;
        color: #000;
        padding: 3px 5px;
        border: 1px solid #999;
        background: #fff;
        width: 300px;
        float: left;*/
        z-index: 1;
        position: absolute;
        margin-left: 0px;
    }
</style>


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


    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
     

</script>

<asp:HiddenField ID="hfCompanyID" runat="server" Value="-1" />

<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1000px;">
                <span class="tdProcessing" style="vertical-align: middle">Please wait...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="welcome-message">
    <h2>Assign mentor</h2>
</div>
<div class="main-container">

    <!--Added by Swati-->
    <%-- <div class="row-div top-margin clearfix">--%>
    <div>
        Location:
    </div>

    <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>--%>
    <asp:DropDownList runat="server" ID="cmbCompany" AutoPostBack="True">
    </asp:DropDownList>
    <%-- </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmbCompany" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <%--<Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbCompany" />
                </Triggers>
            </asp:UpdatePanel>--%>


    <%-- </div>--%>
    <!--end Swati-->

    <div class="cai-form">
        <span class="form-title">Mentors
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
        </span>
        <%-- <div id="PannelFieldsList" class="collapse cai-form-content">--%>
        <div id="PannelFieldsList">
            <div class="mentor-options">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Button runat="server" ID="lnkShowAll" CssClass="submitBtn" Text="Show All" />

                        <div class="form-search-box">
                            <asp:TextBox ID="txtSearch" Text="Search..." AutoPostBack="false"
                                onblur="SetText();" onclick="Clear();" runat="server"></asp:TextBox>

                            <div id="divSearchbtn" class="collapse">

                                <asp:Button ID="btnSearch" runat="server" Text="Search" class="submitBtn" />

                            </div>
                        </div>
                        <div>
                            <telerik:RadListBox ID="radMentors" SelectionMode="Single" runat="server" Height="300" Width="200px">
                            </telerik:RadListBox>
                        </div>
                    </ContentTemplate>
                    <Triggers>

                        <asp:AsyncPostBackTrigger ControlID="lnkShowAll" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cmbCompany" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div>
            <asp:HiddenField ID="hdnBUState" Value="1" runat="server" />
        </div>
    </div>
</div>

<div class="cai-form">
    <span class="form-title">Students</span>
    <div class="cai-form-content">
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
                    <asp:Label ID="lblmsg" ForeColor="Blue" runat="server" Visible="False" />
                </div>

                <div class="actions">
                    <asp:Button ID="btnMentor" runat="server" Text="Mentor Only"
                        class="submitBtn" />
                    Please click to show students assigned just to this mentor
                </div>

                <div class="actions">
                    <asp:Button ID="btnShowAll" runat="server" Text="Show All"
                        class="submitBtn" />
                    Please click to show all students
                </div>

                <div>
                    From:
                    <asp:TextBox ID="txtFrom" runat="server" Text="1" Width="120px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter value" ControlToValidate="txtFrom" ForeColor="Red"></asp:RequiredFieldValidator>
                    To:
                    <asp:TextBox ID="txtTo" runat="server" Text="50" Width="120px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter value" ControlToValidate="txtTo" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Button ID="btnShow" runat="server" Text="Show"
                        class="submitBtn" />
                     <!--Added by Kavita Zinage #18180-->
                    Student no/name:
                    <asp:TextBox ID="txtStudent" runat="server" Text="" Width="120px"></asp:TextBox>
                    <asp:Button ID="btnfind" runat="server" Text="Find"
                        class="submitBtn" />
                    <asp:CheckBox ID="chknoneassign" runat="server" AutoPostBack="true" OnCheckedChanged="LoadNoneAssignedTrainee"  Text="None assigned trainee"/>
                    <!--Till Here Kavita Zinage #18180-->
                </div>


                <%--<asp:GridView ID="GridView1" runat="server"></asp:GridView>--%>
                <div class="cai-table">
                    <br />
                <asp:GridView ID="grdStudent" AutoGenerateColumns="false"
                    runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAllStudent" runat="server" AutoPostBack="true" OnCheckedChanged="ToggleSelectedState" Visible="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span class="mobile-label">Checkbox:</span>
                                <asp:CheckBox ID="chkStudent" CssClass="cai-table-data" runat="server" />
                                <asp:HiddenField runat="server" ID="hdnMentorID" Value='<%# Eval("MentorID")%>' />
                                <asp:HiddenField runat="server" ID="hdnCompanyID" Value='<%# Eval("CompanyID")%>' />
                                <asp:HiddenField runat="server" ID="hdnStudentID" Value='<%# Eval("StudentNo")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Student#">

                            <ItemTemplate>
                                <span class="mobile-label">Student#:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("OldID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Student name">

                            <ItemTemplate>
                                <span class="mobile-label">Student name:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("FirstLast")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email">

                            <ItemTemplate>
                                <span class="mobile-label">Email:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("email1")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Office">

                            <ItemTemplate>
                                <span class="mobile-label">Office:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("CompName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bus. unit">

                            <ItemTemplate>
                                <span class="mobile-label">Bus. unit:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("BusinessUnit")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mentor">
                            <ItemTemplate>
                                <span class="mobile-label">Mentor:</span>
                                <asp:DropDownList ID="ddlMentor" runat="server" CssClass="cai-table-data">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnMentor" Value='<%# Eval("MentorID")%>'
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Start date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtGvStartDate" Text='<%# If((Eval("StartDate") IsNot Nothing AndAlso TypeOf Eval("StartDate") Is DateTime), Convert.ToDateTime(Eval("StartDate")), CType(Nothing, System.Nullable(Of DateTime))) %>' runat="server"></asp:TextBox>
                                <Ajax:CalendarExtender ID="txtGvStartDateCalendarExtender" runat="server" TargetControlID="txtGvStartDate" Format="dd/MM/yyyy">
                                </Ajax:CalendarExtender>
                                <asp:HiddenField runat="server" ID="hdnGvStartDate" Value='<%# Eval("StartDate")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="End date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtGvEndDate" Text='<%# If((Eval("EndDate") IsNot Nothing AndAlso TypeOf Eval("EndDate") Is DateTime), Convert.ToDateTime(Eval("EndDate")), CType(Nothing, System.Nullable(Of DateTime)))%>' runat="server"></asp:TextBox>
                                <Ajax:CalendarExtender ID="txtGvEndDateCalendarExtender" runat="server" TargetControlID="txtGvEndDate" Format="dd/MM/yyyy">
                                </Ajax:CalendarExtender>
                                <asp:HiddenField runat="server" ID="hdnGvEndDate" Value='<%# Eval("EndDate")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecord" runat="server" Text="No record found" Font-Bold="true"
                            ForeColor="Red"></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>

                <div>
                    <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Modal="True"
                        BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                        Title="Business unit assignment" Behavior="None" Height="150px">
                        <ContentTemplate>
                            <table style="background-color: #f4f3f1; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
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
                                            <asp:Button ID="btnValidationOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </telerik:RadWindow>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmbCompany" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />

            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
<div class="actions">
    <asp:Button runat="server" ID="btnMainBack" CssClass="submitBtn" Text="Back" />
    <asp:Button runat="server" ID="btnSubmit" CssClass="submitBtn" Text="Submit" />

</div>
</div>
