<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MasterScheduleDetails__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.MasterScheduleDetails__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>
<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<%--<script src="../../Scripts/jquery-ui-1.8.9.js" type="text/javascript"></script>--%>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-ui-1.11.2.min.js" type="text/javascript"></script>
<script src="../../Scripts/expand.js" type="text/javascript"></script>
<script src="../../Scripts/JScript.min.js" type="text/javascript"></script>
<style type="text/css">
    .confirmBox
    {
        display: none;
        background-color: #ccc;
        border: 2px solid #aaa;
        position: absolute;
        width: 500px;
        left: 30%;
        top: 20%;
        padding: 15px 15px 15px;
        box-sizing: border-box;
        text-align: center;
        z-index: 100;
    }
    
    #confirmOverlay
    {
        display: none;
        width: 100%;
        height: 100%;
        position: fixed;
        top: 0;
        left: 0; /* background-color: #bbbccc;*/
        z-index: 0; /* filter: alpha(opacity = 30);*/
    }
</style>
<script type="text/javascript">

    $(document).ready(function () {
        debugger;
        var PanelState1 = $("#<%=hdnPersonDetailsState.ClientID%>").val();
        var PanelState2 = $("#<%=hdnEmployementDetailsState.ClientID%>").val();
        // var PanelState3 = $("#<%=hdnExemptionsGrantedState.ClientID%>").val();
        var PanelState4 = $("#<%=hdnStatusReasonState.ClientID%>").val();
        var PanelState5 = $("#<%=hdnEligibilityState.ClientID%>").val();
        if (PanelState2 == '1') {
            $('#Employement').removeClass("collapse").addClass("active");
        }
        if (PanelState1 == '1') {
            $('#PersonalDetails').removeClass("collapse").addClass("active");
        }

        if (PanelState4 == '1') {
            $('#StatusReason').removeClass("collapse").addClass("active");
        }

    });

    function ShowTermsandcondtionPopup() {
        $("#saveMessage").show();
        $("#confirmOverlay").css("display", "block");
        return true;
    }


    function closePopup() {
        $("#saveMessage").hide();
        $("#confirmOverlay").css("display", "none");
        return true;
    }
    //    $('[id$=hlTermsandconditions]').live('click', function (event) {
    //        // alert("HI");
    //        ShowTermsandcondtionPopup();

    //        return false;

    //    });

    function CollapseExpand(me, HiddenPanelState) {

        var Panelstate = $('#' + me).attr("class");
        $('#' + me).slideToggle('slow');
        if (Panelstate == "collapse") {

            $('#' + me).removeClass("collapse").addClass("active");
            SetPanelState(HiddenPanelState, 1)
        }
        else {
            $('#' + me).removeClass("active").addClass("collapse");
            SetPanelState(HiddenPanelState, 0)
            $("#<%=" + panel.clientID + "%>").val("0");
        }

    }
    function SetPanelState(HiddenPanelState, StateValue) {
        if (HiddenPanelState == 'hdnPersonDetailsState') {
            $("#<%=hdnPersonDetailsState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnEmployementDetailsState') {
            $("#<%=hdnEmployementDetailsState.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnStatusReasonState') {
            $("#<%=hdnStatusReasonState.clientID %>").val(StateValue);
        }
    }


    function HideLabel(sender, args) {
        var input = args.get_fileName();
        var n = input.indexOf(".");
        var fileExtension = input.substr(n + 1);
        var extensionArrayList = new Array("png", "jpg", "txt", "doc", "docx", "pdf", "bmp", "gif");
        for (var i = 0; i < extensionArrayList.length; i++) {
            if (fileExtension == extensionArrayList[i]) {
                if (document.getElementById('<%=lblErrorFile.ClientID%>'))
                    document.getElementById('<%=lblErrorFile.ClientID%>').style.display = 'none';
                return false;
            }
        }
    }

    function openReportWindow() {

        window.open(window.document.getElementById('<%= hdnReportPath.ClientID %>').value);
    }
</script>
<script type="text/javascript">

   

    
</script>
<style type="text/css">
    .style1
    {
        width: 142px;
    }
    .active
    {
        display: block;
    }
    .inactive
    {
        display: none;
    }
    .collapse
    {
        display: none;
    }
    .expand
    {
        cursor: pointer;
    }
    .ui-draggable .ui-dialog-titlebar
    {
        background-image: none;
        background-color: rgb(231, 210, 182);
        color: Black;
        border: 1px solid #F4F3F1;
    }
    .ui-widget-content
    {
        border: 1px solid #F4F3F1;
    }
    .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default
    {
        background-image: none;
        background-color: blue;
        color: white;
        border: none;
    }
    .ui-state-default:hover
    {
        background-image: none;
        background-color: blue;
        color: white;
        border: none;
        font-weight: bolder;
    }
    .ui-dialog-content ui-widget-content
    {
        border: 1px solid #F4F3F1;
    }
    
    .ui-icon:hover
    {
        background-color: Blue;
    }
    
    .ui-dialog-buttonset
    {
        margin-right: 50%;
    }
</style>
<div id="divContent" runat="server">
    <asp:HiddenField ID="hdnPersonDetailsState" runat="server" Value="1" />
    <asp:HiddenField ID="hdnEmployementDetailsState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnExemptionsGrantedState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnStatusReasonState" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEligibilityState" runat="server" Value="0" />
    <table id="MainTable" style="width: 100%">
        <tr>
            <td>
                <div class="row-div clearfix">
                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <asp:Panel ID="pnlDetails" runat="server">
                    <div class="row-div">
                        <div class="row-div top-margin clearfix">
                            <div class="label-div1">
                                <asp:Button ID="btnPrint" CssClass="submit-Btn"  runat="server" Text="Print" />
                            </div>
                            <div style="text-align: center; width: 100%;">
                                <asp:Label ID="lblGuidance" runat="server" Style="font-weight: bold;" Text=""></asp:Label>
                                <asp:HiddenField ID="hdnReportPath" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="info-data">
                        <br />
                        <%--OnClientClick="openReportWindow();" --%>
                    </div>
                    <asp:Panel ID="Panel1" runat="server">
                        <h2 class="expand" id="HeadPersonalDetails" onclick="CollapseExpand('PersonalDetails','hdnPersonalDetailsState')">
                            Master Schedule Details:</h2>
                        <div id="PersonalDetails" class="collapse">
                            <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="row-div">
                                        <div class="row-div top-margin clearfix">
                                            <div class="label-div1">
                                                Master Schedule Training Contract Registration Number:
                                            </div>
                                            <div class="field-div2">
                                                <asp:TextBox ID="txtMSNumber" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                       <div class="info-data" runat="server" id="div1">
                                            Dear Sirs,
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div class="info-data" runat="server" id="divDeclaration">
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div class="info-data" runat="server" id="divDeclaration2">
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div class="info-data">
                                            <asp:Label ID="Label6" runat="server" Style="font-weight: bold;" Width="200px" Text="Firm Name:"></asp:Label>
                                            <asp:TextBox ID="txtFirmName" Enabled="false" Width="200px" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="info-data">
                                            <asp:Label ID="Label4" runat="server" Style="font-weight: bold;" Width="200px" Text="Status:"></asp:Label>
                                            <asp:TextBox ID="txtStatus" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="info-data">
                                            <asp:Label ID="Label5" runat="server" Style="font-weight: bold;" Width="200px" Text="Training Manager:"></asp:Label>
                                            <asp:TextBox ID="txtTrainingManager" Enabled="false" Width="200px" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlEmploymentDetails" runat="server">
                        <h2 class="expand" id="HeadEmploymentDetails" onclick="CollapseExpand('Employement','hdnEmployementDetailsState')">
                            Student Registrations:</h2>
                        <div id="Employement" class="collapse">
                            <div>
                                <br />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <div class="info-data">
                                        <div class="row-div clearfix">
                                            <telerik:RadGrid ID="grdContractDetails" runat="server" AllowPaging="true" AllowSorting="True"
                                                PageSize="10" AllowFilteringByColumn="false" CellSpacing="0" GridLines="None"
                                                AutoGenerateColumns="false" Width="99%" Visible="true" ShowHeadersWhenNoRecords="true">
                                                <MasterTableView ShowHeadersWhenNoRecords="true" AllowSorting="True" AllowNaturalSort="false"
                                                    NoMasterRecordsText="No Educational Contract Record Found.">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Student Number" DataField="OldID" SortExpression="OldID"
                                                            AutoPostBackOnFilter="true" AllowFiltering="false" FilterControlWidth="100%"
                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkStudentNo" runat="server" Enabled="false" Text='<%# DataBinder.Eval(Container.DataItem,"OldID") %>'>
                                                                </asp:HyperLink>
                                                                <asp:HiddenField ID="hidECID" Value='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="FirstName" AllowFiltering="false" HeaderText="First Name"
                                                            SortExpression="FirstName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ShowFilterIcon="false" />
                                                        <telerik:GridBoundColumn DataField="LastName" AllowFiltering="false" HeaderText="Last Name"
                                                            SortExpression="LastName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ShowFilterIcon="false" />
                                                        <telerik:GridBoundColumn DataField="Country" HeaderText="Student Country of Origin"
                                                            SortExpression="Country" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                        <telerik:GridBoundColumn DataField="TrainCmpName" HeaderText="Office Where Student Will Train"
                                                            SortExpression="TrainCmpName" AutoPostBackOnFilter="false" AllowFiltering="false"
                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                        <telerik:GridBoundColumn DataField="YearsOfExperience" HeaderText="Yrs.Of Exp. Required"
                                                            SortExpression="YearsOfExperience" AllowFiltering="false" AutoPostBackOnFilter="true"
                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                        <telerik:GridBoundColumn DataFormatString="{0:dd/MM/yyyy}" DataField="ContractStartDate"
                                                            HeaderText="Start Date" SortExpression="ContractStartDate" AllowFiltering="false"
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                        <telerik:GridBoundColumn DataFormatString="{0:dd/MM/yyyy}" DataField="ContractExpireDate"
                                                            HeaderText="End Date" SortExpression="ContractExpireDate" AllowFiltering="false"
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                                        <telerik:GridBoundColumn DataField="Status" HeaderText="Registration Status" SortExpression="Status"
                                                            AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ShowFilterIcon="false" />
<telerik:GridTemplateColumn HeaderText="Remove Association" DataField="OldID" SortExpression="OldID"
                                                            AutoPostBackOnFilter="true" AllowFiltering="false" FilterControlWidth="100%"
                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkRemoveAssociation" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                                    CommandName="RemoveAssociation" Text="Remove Association">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </telerik:GridTemplateColumn>
                                                        <%-- <telerik:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type"
                                                            AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ShowFilterIcon="false" />--%>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                            <asp:Label runat="server" ID="lblEmptyMsg" Text="No Educational Contract Record Found."
                                                Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div runat="server" id="Div4" class="info-data">
                                                <div class="row-div clearfix">
                                                    <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                                        <asp:Label ID="lblSubmittedDate" runat="server" Width="110px" Text="Signature Date:"></asp:Label>
                                                        <asp:TextBox ID="txtSubmitteddate" Enabled="false" Width="200px" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="label-div w58">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    <div class="label-div-left-align w100">
                                                        <div runat="server" id="Div5" class="info-data">
                                                            <div class="row-div clearfix">
                                                                <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                                                    Signature:
                                                                    <asp:TextBox ID="txtSignature" Enabled="false" Width="200px" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div id="divTermsandconditions" runat="server" class="row-div clearfix">
                                <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                    <asp:CheckBox ID="chkTermsandconditions" Text="" TextAlign="Right" runat="server" />
                                    <asp:LinkButton ID="hlTermsandconditions" runat="server">Terms & Condition</asp:LinkButton>
                                </div>
                            </div>
                            <div runat="server" id="divAttachments" style="font-size: 8pt; color: Black; font-style: normal;
                                font-weight: normal; width: 80%;">
                                <div>
                                    <span class="Error">
                                        <asp:Label ID="lblErrorFile" Visible="false" runat="server">Error</asp:Label></span></div>
                                <div>
                                    <uc2:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                                        AllowAdd="True" AllowDelete="false" />
                                </div>
                            </div>
                            <div>
                                <br />
                            </div>
                            <div style="text-align: center;">
                                <asp:Button ID="btnBack" runat="server" Text="Back" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlStatusReason" Visible="false" runat="server">
                        <h2 id="HeadStatusReason" class="expand" onclick="CollapseExpand('StatusReason','hdnStatusReasonState')">
                            Status Reason</h2>
                        <div id="StatusReason" class="collapse">
                            <div class="row-div clearfix">
                                <asp:Panel ID="Panel3" Style="padding-left: 30pt;" runat="server">
                                    <div>
                                        <br />
                                    </div>
                                    <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;
                                        vertical-align: middle;">
                                        Status Reason:
                                        <asp:TextBox ID="txtStatusReason" Enabled="false" Width="600px" Height="40px" Text=""
                                            TextMode="MultiLine" runat="server" Style="text-align: left; vertical-align: middle;"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>
<div id="confirmOverlay">
</div>
<div id="saveMessage" class="confirmBox">
    <div class="message1" id="message1" runat="server" style="margin: 10px">
    </div>
    <asp:LinkButton ID="btnOK1" OnClientClick="closePopup();" runat="server" Text="OK"></asp:LinkButton>
</div>
<div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
    <telerik:RadWindow ID="radMockTrial" runat="server" Width="350px" Height="120px"
        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Master Schedule Details" Behavior="None">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblWarning" runat="server" Font-Bold="true" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="radMockTrialSubmit" runat="server" Width="450px" Height="150px"
        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Master Schedule Details" Behavior="None">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblWarningSubmit" runat="server" Font-Bold="true" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnOKSubmit" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                        <asp:Button ID="btnCancleSubmit" runat="server" Text="Cancel" Width="70px" class="submitBtn" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="radMockTrialTermandCon" runat="server" Width="350px" Height="120px"
        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Master Schedule Details" Behavior="None">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblWarningTermandCon" runat="server" Font-Bold="true" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnOKTermandCon" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
</div>
