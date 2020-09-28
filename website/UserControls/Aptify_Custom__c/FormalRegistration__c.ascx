<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FormalRegistration__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.FormalRegistration__c" %>
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
    .active {
        display: block;
    }

    .inactive {
        display: none;
    }

    .collapse {
        display: none;
    }

    .expand {
        cursor: pointer;
    }
</style>
<script type="text/javascript">

    $(document).ready(function () {

        var PanelState1 = $("#<%=hdnPersonDetailsState.ClientID%>").val();
        var PanelState2 = $("#<%=hdnEmployementDetailsState.ClientID%>").val();
        var PanelState3 = $("#<%=hdnExemptionsGrantedState.ClientID%>").val();
        var PanelState4 = $("#<%=hdnStatusReasonState.ClientID%>").val();
        var PanelState5 = $("#<%=hdnEligibilityState.ClientID%>").val();

        if (PanelState1 == '1') {
            $('#PersonalDetails').removeClass("collapse").addClass("active");
        }
        if (PanelState3 == '1') {
            $('#ExemptionsGranted').removeClass("collapse").addClass("active");
        }
        if (PanelState2 == '1') {
            $('#Employement').removeClass("collapse").addClass("active");
        }

        if (PanelState4 == '1') {
            $('#StatusReason').removeClass("collapse").addClass("active");
        }

        if (PanelState5 == '1') {
            $('#Eligibility').removeClass("collapse").addClass("active");
        }
    });

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
        if (HiddenPanelState == 'hdnExemptionsGrantedState') {
            $("#<%=hdnExemptionsGrantedState.clientID %>").val(StateValue);
        }

        if (HiddenPanelState == 'hdnStatusReasonState') {
            $("#<%=hdnStatusReasonState.clientID %>").val(StateValue);
        }

        if (HiddenPanelState == 'hdnEligibilityState') {
            $("#<%=hdnEligibilityState.clientID %>").val(StateValue);
        }

    }


  <%--  function HideLabel(sender, args) {
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
    }--%>

    function openReportWindow() {

        window.open(window.document.getElementById('<%= hdnReportPath.ClientID %>').value);
    }
</script>
<style type="text/css">
    .style1 {
        width: 142px;
    }

    .active {
        display: block;
    }

    .inactive {
        display: none;
    }

    .collapse {
        display: none;
    }

    .expand {
        cursor: pointer;
    }

    .ui-draggable .ui-dialog-titlebar {
        background-image: none;
        background-color: rgb(231, 210, 182);
        color: Black;
        border: 1px solid #F4F3F1;
    }

    .ui-widget-content {
        border: 1px solid #F4F3F1;
    }

        .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default {
            background-image: none;
            background-color: blue;
            color: white;
            border: none;
        }

            .ui-state-default:hover {
                background-image: none;
                background-color: blue;
                color: white;
                border: none;
                font-weight: bolder;
            }

    .ui-dialog-content ui-widget-content {
        border: 1px solid #F4F3F1;
    }

    .ui-icon:hover {
        background-color: Blue;
    }

    .ui-dialog-buttonset {
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
                <asp:Panel ID="pnlData" runat="server">
                    <div class="info-data">
                        <%--<div class="row-div clearfix">
                            <asp:Button ID="btnNew" runat="server" Width="20%" Text="Start New Application" />
                        </div>--%>
                        <asp:Label ID="lblExemptionsNotFound" runat="server"></asp:Label>
                        <div class="row-div clearfix">
                            <rad:RadGrid ID="grdFormalReg" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                                AllowSorting="false" SortingSettings-SortedAscToolTip="Sorted Ascending">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="Registration Type" DataField="Type" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkRegType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>'
                                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'></asp:HyperLink>
                                                <%-- NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'--%>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Registration Status" DataField="Status" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Start Date" DataField="ContractStartDate" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractStartDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="End Date" DataField="ContractExpireDate" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractExpireDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlDetails" runat="server">
                    <div class="info-data">
                        <%--OnClientClick="openReportWindow();" --%>
                        <asp:Button ID="btnPrint" runat="server" Text="Print" />
                        <asp:HiddenField ID="hdnReportPath" runat="server" />
                        <asp:HiddenField ID="hidDateRegistered" runat="server" />
                        <asp:HiddenField ID="hidFormalID" runat="server" />
                        <asp:HiddenField ID="hidEERouteOfEntry" runat="server" />
                    </div>
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-div-left-align">
                                Status: <b>
                                    <asp:Label ID="lblStatus" Width="250px" runat="server" Text=""></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w16">
                                Type: <b>
                                    <asp:Label ID="lblType" Width="250px" runat="server" Text=""></asp:Label></b>
                            </div>
                            <div class="label-div-left-align w16">
                                Route Of Entry:<b>
                                    <asp:Label ID="lblRouteOfEntry" Width="250px" runat="server" Text=""></asp:Label></b>
                                <asp:HiddenField ID="hidRouteOfEntry" runat="server" />
                            </div>
                        </div>
                    </div>
                    <h2 class="expand" id="HeadPersonalDetails" onclick="CollapseExpand('PersonalDetails','hdnPersonDetailsState')">Personal Details:</h2>
                    <div id="PersonalDetails" class="collapse">
                        <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div runat="server" id="EduDetails" class="info-data">
                                                <div class="row-div clearfix">
                                                    &nbsp;
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="field-div1 w75">
                                                        Student Number:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtStudentNo" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Student Name:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtStudentName" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Address 1:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtAddress1" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Address 3:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtAddress3" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Postal Code:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtPostalCode" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div w25">
                                                        Email:
                                                    </div>
                                                    <div class="field-div1 w75">
                                                        <asp:TextBox ID="txtEmail" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>


                                                <div id="divRecognizedexperience" visible ="false" runat="server" class="field-group">
                                                                <div class="label-title ">Recognized experience:</div>
                                                                <asp:TextBox ID="txtRecognizedexperience" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                            </div>
                                        </div>
                                        <div class="label-div w58">
                                            <div class="info-data">
                                                <div class="row-div clearfix">
                                                    &nbsp;
                                                </div>
                                                <div class="row-div clearfix">
                                                    <div class="label-div-left-align w100">
                                                        <div runat="server" id="Div1" class="info-data">
                                                            <div class="row-div clearfix">
                                                                <div class="label-div w25">
                                                                </div>
                                                                <div class="field-div1 w75">
                                                                </div>
                                                            </div>
                                                            <div class="row-div clearfix">
                                                                <div class="label-div w25">
                                                                    Gender:
                                                                </div>
                                                                <div class="field-div1 w75">
                                                                    <asp:TextBox ID="txtGender" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row-div clearfix">
                                                                <div class="label-div w25">
                                                                    Address 2:
                                                                </div>
                                                                <div class="field-div1 w75">
                                                                    <asp:TextBox ID="txtAddress2" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row-div clearfix">
                                                                <div class="label-div w25">
                                                                    City:
                                                                </div>
                                                                <div class="field-div1 w75">
                                                                    <asp:TextBox ID="txtCity" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row-div clearfix">
                                                                <div class="label-div w25">
                                                                    Country Of Origin:
                                                                </div>
                                                                <div class="field-div1 w75">
                                                                    <asp:TextBox ID="txtCountryOrigin" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row-div clearfix">
                                                                <div class="label-div w25">
                                                                    Mobile:
                                                                </div>
                                                                <div class="field-div1 w75">
                                                                    <asp:TextBox ID="txtMobile" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>


                                                             <div class="row-div clearfix">
                                                                <div class="label-div w25">
                                                                    County:
                                                                </div>
                                                                <div class="field-div1 w75">
                                                                    <asp:TextBox ID="txtCounty" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>


                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- row-div clearfix--%>
                                                <%--<div style="text-align: center;" class="field-div1 w75">
                                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" />
                                                </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:Panel ID="pnlEmploymentDetails" runat="server">
                        <h2 class="expand" id="HeadEmploymentDetails" onclick="CollapseExpand('Employement','hdnEmployementDetailsState')">Employement Details:</h2>
                        <div id="Employement" class="collapse">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <div class="info-data">
                                        <div class="row-div clearfix">
                                            <div class="label-div-left-align w40">
                                                <div runat="server" id="Div2" class="info-data">
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w25">
                                                        </div>
                                                        <div class="field-div1 w75">
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w25">
                                                            Firm Name:
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <asp:TextBox ID="txtFirmName" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w25">
                                                            Address 1:
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <asp:TextBox ID="txtfirmAddress1" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w25">
                                                            Address 3:
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <asp:TextBox ID="txtfirmAddress3" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w25">
                                                            Postal Code:
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <asp:TextBox ID="txtfirmPostalCode" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row-div clearfix">
                                                                <div class="label-div w25">
                                                                    County:
                                                                </div>
                                                                <div class="field-div1 w75">
                                                                    <asp:TextBox ID="txtfirmCounty" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w25">
                                                            Start Date:
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <asp:TextBox ID="txtfirmStartDate" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w25">
                                                            &nbsp;
                                                        </div>
                                                        <div style="text-align: right; vertical-align: bottom;">
                                                            Contract Duration:
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="label-div w58">
                                                <div class="info-data">
                                                    <div class="row-div clearfix">
                                                        &nbsp;
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div-left-align w100">
                                                            <div runat="server" id="Div3" class="info-data">
                                                                <div class="row-div clearfix">
                                                                    <div class="label-div w25">
                                                                        Address 2:
                                                                    </div>
                                                                    <div class="field-div1 w75">
                                                                        <asp:TextBox ID="txtfirmAddress2" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row-div clearfix">
                                                                    <div class="label-div w25">
                                                                        City:
                                                                    </div>
                                                                    <div class="field-div1 w75">
                                                                        <asp:TextBox ID="txtfirmCity" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row-div clearfix">
                                                                    <div class="label-div w25">
                                                                        Country:
                                                                    </div>
                                                                    <div class="field-div1 w75">
                                                                        <asp:TextBox ID="txtfirmCountry" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row-div clearfix">
                                                                    <div class="label-div w25">
                                                                        End Date:
                                                                    </div>
                                                                    <div class="field-div1 w75">
                                                                        <asp:TextBox ID="txtfirmEndDate" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="text-align: left;">
                                                    <asp:TextBox ID="txtfirmContractDur" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlEligibility" runat="server">
                        <h2 id="H1" class="expand" onclick="CollapseExpand('Eligibility','hdnEligibilityState')">Eligibility For Registration:</h2>
                        <div id="Eligibility" style="vertical-align: top;" class="collapse">
                            <div>
                                <br />
                            </div>
                            <asp:Panel BorderWidth="0" Style="padding-left: 30pt; vertical-align: top;" ID="Panel1"
                                runat="server">
                                <div class="row-div clearfix">
                                    <div runat="server" id="divRoute" style="font-size: 8pt; font-weight: bold; color: Black; font-style: normal;">
                                        <%-- <b>These are two entry to studying the Flexible Option(previouslt known as Elevation
                                            Programme).</b><br />
                                        <b>Please select which route is appropriate to you.</b>--%>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <asp:Panel ID="Panel2" BorderWidth="1" Style="padding-left: 30pt; width: 650px" runat="server">
                                        <div runat="server" id="divRoutelevel" style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                        </div>
                                        <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                            <asp:RadioButton ID="chkRouteA" runat="server" GroupName="Group1" />
                                            &nbsp;&nbsp;&nbsp;<b><u>Route A</u> </b>With four year's work experience(any discipline)
                                        </div>
                                        <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                            <asp:RadioButton ID="chkRouteB" runat="server" GroupName="Group1" />&nbsp;&nbsp;&nbsp; <b><u>Route B</u>
                                            </b>Without four year's work experience(any discipline)
                                        </div>
                                        <div runat="server" id="divEligibility1" style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div id="divEligibility2" style="font-size: 8pt; color: Black; font-style: normal; font-weight: bold;"
                                    runat="server" class="row-div clearfix">
                                    <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: bold;">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                    <h2 id="HeadDeclaration" class="expand" onclick="CollapseExpand('ExemptionsGranted','hdnExemptionsGrantedState')">Declaration:</h2>
                    <div id="ExemptionsGranted" style="vertical-align: top;" class="collapse">
                        <div>
                            <br />
                        </div>
                        <asp:Panel BorderWidth="0" Style="padding-left: 30pt; vertical-align: top;" ID="pnlCotractDeclaration"
                            runat="server">
                            <div id="divFlexibleDeclaration" visible="false" runat="server" class="row-div clearfix">
                                <div id="divFlexibleDeclarationText" runat="server" style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                </div>
                            </div>
                            <div id="divContractDeclaration" visible="false" runat="server" class="row-div clearfix">
                                <div id="divContractDeclaration2" runat="server" style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div-left-align w40">
                                    <div runat="server" id="Div4" class="info-data">
                                        <div class="row-div clearfix">
                                            <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                                <asp:Label ID="lblSubmittedDate" runat="server" Width="110px" Text="Submitted Date:"></asp:Label>
                                                <asp:TextBox ID="txtSubmitteddate" Enabled="false" Width="200px" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div id="divWitnessName" runat="server" class="row-div clearfix">
                                            <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                                <asp:Label ID="Label1" runat="server" Width="110px" Text="Witness Name(Block):"></asp:Label>
                                                <asp:TextBox ID="txtWitnessName" Enabled="false" Width="200px" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div id="divWitnessSign" runat="server" class="row-div clearfix">
                                            <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                                <asp:Label ID="Label2" runat="server" Width="110px" Text="Witness Signature:"></asp:Label>
                                                <asp:TextBox ID="txtWitnessSign" Enabled="false" Width="200px" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- <div id="divTermsandconditions" runat="server" class="row-div clearfix">
                                            <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                                <asp:CheckBox ID="chkTermsandconditions" Text="" TextAlign="Right" runat="server" />
                                                <asp:HyperLink ID="hlTermsandconditions" runat="server">Terms & Condition</asp:HyperLink>
                                            </div>
                                        </div>--%>
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
                            <div id="divTermsandconditions" runat="server" class="row-div clearfix">
                                <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                    <asp:CheckBox ID="chkTermsandconditions" Text="" TextAlign="Right" runat="server" />
                                    <%--<asp:HyperLink ID="hlTermsandconditions" runat="server">Terms & Condition</asp:HyperLink>--%>
                                    <asp:LinkButton ID="hlTermsandconditions" runat="server">Terms & Condition</asp:LinkButton>
                                </div>
                            </div>
                            <%--<div runat="server" id="divAttachments" visible="false" style="font-size: 8pt; color: Black;
                                font-style: normal; font-weight: normal; width: 80%;">
                                <div>
                                    <span class="Error">
                                        <asp:Label ID="lblErrorFile" Visible="false" runat="server">Error</asp:Label></span></div>
                                <div>
                                    <uc2:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                                        AllowAdd="True" AllowDelete="false" />
                                </div>
                            </div>--%>
                            <div>
                                <br />
                            </div>
                            <%--  <div style="text-align: center;">
                                <asp:Button ID="btnCreate" runat="server" Text="Create" />
                            </div>--%>
                            <div id="divContractNotice" visible="false" runat="server" class="row-div clearfix">
                                <div id="divContractNotice2" runat="server" style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal;">
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:Panel ID="pnlStatusReason" Visible="false" runat="server">
                        <h2 id="HeadStatusReason" class="expand" onclick="CollapseExpand('StatusReason','hdnStatusReasonState')">Status Reason</h2>
                        <div id="StatusReason" class="collapse">
                            <div class="row-div clearfix">
                                <asp:Panel ID="Panel3" Style="padding-left: 30pt;" runat="server">
                                    <div>
                                        <br />
                                    </div>
                                    <div style="font-size: 8pt; color: Black; font-style: normal; font-weight: normal; vertical-align: middle;">
                                        Status Reason:
                                        <asp:TextBox ID="txtStatusReason" Enabled="false" Width="600px" Height="40px" Text=""
                                            TextMode="MultiLine" runat="server" Style="text-align: left; vertical-align: middle;"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                    <div style="text-align: center;">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" CssClass="submit-Btn" Text="Submit & Print" Visible="false" />
                        <asp:Button ID="btnBack" Visible="false" CssClass="submit-Btn" runat="server" Text="Back" />
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>
<div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
    <telerik:RadWindow ID="radMockTrial" runat="server" Width="350px" Height="120px"
        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Formal Registration" Behavior="None">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblWarning" runat="server" Font-Bold="true" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                        <asp:Button ID="btnOKValidation" runat="server" Visible="false" Text="Ok" Width="70px"
                            class="submitBtn" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="radCreateMsg" runat="server" Width="350px" Height="120px"
        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Formal Registration" Behavior="None">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCreate" runat="server" Font-Bold="true" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnCreateOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
</div>
