<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ExternalApp__c"
    CodeFile="~/UserControls/Aptify_Custom__c/ExternalApp__c.ascx.vb" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/CreditCard__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .RED
    {
        width: 25px;
        height: 20px;
        background-color: RED;
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
</style>
<script type="text/javascript">
    $(document).ready(function () {
        var PanelState1 = $("#<%=hdnSummer.ClientID%>").val();
        var PanelState2 = $("#<%=hdnAutum.ClientID%>").val();
        var PanelState3 = $("#<%=hdnCourse.ClientID%>").val();
        var PanelState4 = $("#<%=hdnCreditCard.ClientID%>").val();
        var PanelState0 = $("#<%=hdnHeader.ClientID%>").val();


        if (PanelState4 == '1') {
            $('#divCreditCardDetails').removeClass("collapse").addClass("active");
        }
        if (PanelState3 == '1') {
            $('#divCourse').removeClass("collapse").addClass("active");
        }
        if (PanelState2 == '1') {
            $('#divAutum').removeClass("collapse").addClass("active");
        }
        if (PanelState1 == '1') {
            $('#divSummer').removeClass("collapse").addClass("active");
        }
        if (PanelState0 == '0') {
            $("#divHeaderPanel").show('slow');
            $("#divHeaderPanel").removeClass("collapse").addClass("active");
            SetPanelState('hdnHeader', 1)
        }
    });

    function CollapseExpand(me, HiddenPanelState, header) {

        var Panelstate = $('#' + me).attr("class");
        $('#' + me).slideToggle('slow');
        if (Panelstate == "collapse") {
            //  $(header).css("color","blue");
            $('#' + me).removeClass("collapse").addClass("active");
            SetPanelState(HiddenPanelState, 1)


        }
        else {
            //$(header).css("color","");
            $('#' + me).removeClass("active").addClass("collapse");
            SetPanelState(HiddenPanelState, 0)
            $("#<%=" + panel.clientID + "%>").val("0");
        }

    }
    function SetPanelState(HiddenPanelState, StateValue) {

        if (HiddenPanelState == 'hdnHeader') {
            $("#<%=hdnHeader.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnSummer') {
            $("#<%=hdnSummer.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnAutum') {
            $("#<%=hdnAutum.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnCourse') {
            $("#<%=hdnCourse.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnCreditCard') {
            $("#<%=hdnCreditCard.clientID %>").val(StateValue);
        }


    }





    //govind started code here for exapnd all

    function CollapseAll(hidnID) {

        //For Header  Div
        var FirstPanel = $("#divHeaderPanel").attr("class");
        $("#divHeaderPanel").show('slow');
        $("#divHeaderPanel").removeClass("collapse").addClass("active");
        SetPanelState('hdnHeader', 1)

        // Susan Wong 22/03/2017: Added code for PersonalDetails
        //For Personal Details Div
        var FifthPanel = $("#PersonalDetails").attr("class");
        $("#PersonalDetails").hide('slow');
        $("#PersonalDetails").removeClass("collapse").addClass("active");
        SetPanelState('hdnPersonalDetails', 1)

        //For First Div
        var FirstPanel = $("#divSummer").attr("class");
        $("#divSummer").show('slow');
        $("#divSummer").removeClass("collapse").addClass("active");
        SetPanelState('hdnSummer', 1)


        //For second Div
        var SecondPanel = $("#divAutum").attr("class");
        $("#divAutum").show('slow');
        $("#divAutum").removeClass("collapse").addClass("active");
        SetPanelState('hdnAutum', 1)

        //For Third Div
        var ThirdPanel = $("#divCourse").attr("class");
        $("#divCourse").show('slow');
        $("#divCourse").removeClass("collapse").addClass("active");
        SetPanelState('hdnCourse', 1)

        //For Fourth Div
        var FourthPanel = $("#divCreditCardDetails").attr("class");
        $("#divCreditCardDetails").show('slow');
        $("#divCreditCardDetails").removeClass("collapse").addClass("active");
        SetPanelState('hdnCreditCard', 1)


    }

    function OpenCreditCardDiv() {
        //For Fourth Div
        var FourthPanel = $("#divCreditCardDetails").attr("class");
        $("#divCreditCardDetails").show('slow');
        $("#divCreditCardDetails").removeClass("collapse").addClass("active");
        SetPanelState('hdnCreditCard', 1)
    }

    function myFunction() {
        //some code here

        //For Header  Div
        var FirstPanel = $("#divHeaderPanel").attr("class");
        $("#divHeaderPanel").hide('slow');
        $("#divHeaderPanel").removeClass("expand").addClass("inactive");
        SetPanelState('hdnHeader', 0)

        // Susan Wong 22/03/2017: Added code for PersonalDetails
        //For Personal Details Div
        var FifthPanel = $("#PersonalDetails").attr("class");
        $("#PersonalDetails").hide('slow');
        $("#PersonalDetails").removeClass("expand").addClass("inactive");
        SetPanelState('hdnPersonalDetails', 0)

        //For First Div
        var FirstPanel = $("#divSummer").attr("class");
        $("#divSummer").hide('slow');
        $("#divSummer").removeClass("expand").addClass("inactive");
        SetPanelState('hdnSummer', 0)


        //For second Div
        var SecondPanel = $("#divAutum").attr("class");
        $("#divAutum").hide('slow');
        $("#divAutum").removeClass("expand").addClass("inactive");
        SetPanelState('hdnAutum', 0)

        //For Third Div
        var ThirdPanel = $("#divCourse").attr("class");
        $("#divCourse").hide('slow');
        $("#divCourse").removeClass("expand").addClass("inactive");
        SetPanelState('hdnCourse', 0)

        //For Fourth Div
        var FourthPanel = $("#divCreditCardDetails").attr("class");
        $("#divCreditCardDetails").hide('slow');
        $("#divCreditCardDetails").removeClass("expand").addClass("inactive");
        SetPanelState('hdnCreditCard', 0)
    }
    //--><![CDATA[//><!--
    //    $(document).ready(function () {
    //        /*var firstDIv = $("#first");
    //        firstDIv.removeClass("collapse");
    //        firstDIv.addClass("expand");*/


    //        $("h2.expand").toggler();



    //        $("#content").expandAll({ trigger: "h2.expand", ref: "div.demo", localLinks: "p.top a" });
    //    });
    /*$(function () {
    // --- Using the default options:
    /*   $("#firstHead").bind("click", function (ev) {

    var firstDIv = $("#first");
    firstDIv.removeClass("expand");
    firstDIv.addClass("collapse");
    });
    $("h2.expand").toggler();


    // --- Other options:
    //$("h2.expand").toggler({method: "toggle", speed: 0});
    //$("h2.expand").toggler({method: "toggle"});
    //$("h2.expand").toggler({speed: "fast"});
    //$("h2.expand").toggler({method: "fadeToggle"});
    //$("h2.expand").toggler({method: "slideFadeToggle"});    
    $("#content").expandAll({ trigger: "h2.expand", ref: "div.demo", localLinks: "p.top a" });
    });*/
    //--><!]]>


    function openReportWindow() {

        window.open(window.document.getElementById('<%= hdnReportPath.ClientID %>').value);
    }


</script>

<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>This process can take a few minutes</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="content-container clearfix">
    <div>
        <div>
            <asp:Label ID="lblSuccessMsg" runat="server" Text=""></asp:Label>
        </div>
        <div class="info-data">
            <div class="row-div clearfix">
              <div class="label-div-left-align">
                 <b>
                     <asp:Label ID="lblFirstLastHeading" runat="server" Text="Name:"></asp:Label>
                 </b>
                     <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label></div>
            <div class="label-div-left-align">
                 <b>
                     <asp:Label ID="lblStudentNumberHeading" runat="server" Text="Student number:"></asp:Label></b>
                     <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label></div>
                <div class="label-div-left-align">
                    <b>
                     <asp:Label ID="lblAcademicCycleHeading" runat="server" Text="Academic cycle:"></asp:Label></b>
                    <asp:Label ID="lblAcademicCycle" runat="server" Text=""></asp:Label></div>
               <div class="label-div-left-align">
                    <b>
                     <asp:Label ID="lblStatusHeading" runat="server" Text="Status:"></asp:Label>
                     </b>
                     <asp:Label ID="lblStatus" runat="server" Text="In progress"></asp:Label></div>
                <div class="label-div-left-align">
                     <b>
                         <asp:Label ID="lblCommentsHeading" runat="server" Text="Comments:"></asp:Label></b>
                         <asp:Label ID="lblComments" runat="server" Text=""></asp:Label>
                        </div>
        </div>
    </div>
        <div>
            
            <div align="center" style="margin-bottom: 20px;">
                <asp:HyperLink ID="HyperLink1" runat="server" class="expand submitBtn" onclick="CollapseAll(this)">Expand all</asp:HyperLink>
                <%--<h1 id="idExpandAll" class="expand" onclick="CollapseAll(this)"  align="center" > Exapnd All</h1>--%>
            </div>
            <%--Added/Modified/Commented By Kavita Z Issue#17719--%>
            <%--<div>
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
            </div>--%>
            <div>
                <asp:UpdatePanel ID="updpanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <b>
                            <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="lblError" Visible="false"></asp:Label>
                        </b>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div runat="server" id="idDemodiv"> <%-- class="demo" --%>
                <div class="cai-form">
                <h2 class="expand form-title" onclick="CollapseExpand('divHeaderPanel','hdnHeader',this)">
                    Enrollment details</h2>
                <div class="collapse form-content" id="divHeaderPanel">
                    <table width="100%">
                        <div class="field-group"
                        <tr>
                            <td width="30%" valign="top">
                                <asp:Label ID="lblRoute" runat="server" Text="Route of entry :"></asp:Label>
                                &nbsp;<b><asp:Label ID="lblRouteOfEntry" runat="server" Text=""></asp:Label></b>
                            </td>
                            <td width="70%">
                                <asp:Label ID="lblDeclaration" runat="server" Text=""
                                    ></asp:Label>
                                    <br />
                                    <asp:CheckBox ID="chkTermsandconditions" Text="" TextAlign="Right" runat="server" />
                                
                                    <asp:LinkButton ID="hlTermsandconditions" runat="server" CausesValidation="false">I have read and agree to the attached Examinations and Appeals Regulations</asp:LinkButton>
<%--
                                <asp:Label ID="lblFurtherInformation" runat="server" Text="Please provide any further information:"
                                    Visible="false"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtFurtherInfo" runat="server" TextMode="MultiLine" Width="450px"
                                    Style="resize: none" Visible="false"></asp:TextBox><br />--%>
                                 <br />
                            </td>
                        </tr>
                         <tr>
                        <td>
                        Eligibility option : 
                         <asp:DropDownList ID="drpEligibilityOption" runat="server" Width="200px" AutoPostBack="true">
                                </asp:DropDownList>
                          <asp:RequiredFieldValidator InitialValue="Select eligibility option" ID="RequiredFieldValidator5"
                            Display="Dynamic"   runat="server" ControlToValidate="drpEligibilityOption"
                            Text="" ErrorMessage="Please select eligibility option" ForeColor="red"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                        </div>
                    </table>
                </div>
                    </div>
                <div>
                </div>

                  <div class="cai-form"> 
                   <h2 class="expand form-title" id="HeadPersonalDetails" onclick="CollapseExpand('PersonalDetails','hdnPersonalDetails')">
                        Personal details</h2>
                    <div id="PersonalDetails" class="collapse cai-form-content">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        &nbsp;</div>
                                      <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div class="label-div w20">
                                               <asp:LinkButton ID="lnkUpdate" runat="server" Visible="false" >PLEASE CLICK TO ADD MANDATORY INFORMATION</asp:LinkButton>
                                            </div>
                                            <div class="field-div1 w75">
                                              &nbsp;
                                            </div>
                                        </div>
                                    </div>
                                   <div class="row-div clearfix">
                                        <div class="label-div-left-align w40">
                                            <div class="label-div w20">
                                              <asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label> 
                                                  Prefix:
                                                <%--Salutation:--%>
                                            </div>
                                            <div class="field-div1 w75">
                                                
                                                    <asp:DropDownList ID="cmbSalutation" CssClass="txtBoxEditProfileForDropdown" ToolTip="Changes to prefix requires supporting documentation" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ControlToValidate="cmbSalutation" ID="reqPrefix"
                                                        ForeColor="Red" ErrorMessage="Prefix required"
                                                        InitialValue="" runat="server" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                             <%-- <asp:TextBox ID="txtPreferredSalutation" runat="server" Visible="false"   ></asp:TextBox>--%>
                                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPreferredSalutation"  ErrorMessage="Salutation required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                <%--<asp:Label ID="lblSalutation" runat="server" Text="Dear"></asp:Label>--%>
                                            </div>
                                        </div>
                                    </div>
                                      <div class="row-div clearfix">
                                        <div class="label-div-left-align w29">
                                            <div class="label-div w40">
                                               <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label> Gender:
                                            </div>
                                            <div class="field-div1 w75">
                                                 <asp:DropDownList ID="cmbGender" CssClass="txtBoxEditProfileForDropdown" runat="server"   >
                                                    <asp:ListItem Value="0">Male</asp:ListItem>
                                                    <asp:ListItem Value="1">Female</asp:ListItem>
                                                    <asp:ListItem Value="2" Selected="True">Undisclosed</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div-left-align w58">
                                            <div class="label-div w20">
                                                Home address:
                                            </div>
                                            <div class="field-div1 w78">
                                                <div class="info-data">
                                                    <div class="row-div clearfix">
                                                      <asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red"></asp:Label> Line 1: <asp:TextBox ID="txtHomeAddressLine1" CssClass="txtBoxEditProfile" runat="server"   ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHomeAddressLine1"  ErrorMessage="Home address line 1 required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                       <%--<asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red">--%></asp:Label> Line 2: <asp:TextBox ID="txtHomeAddressLine2" CssClass="txtBoxEditProfile" runat="server"  ></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHomeAddressLine2"  ErrorMessage="Home address line 2 required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="row-div clearfix" id="divLine3" runat="Server" visible="false" >
                                                       Line 3: <asp:TextBox ID="txtHomeAddressLine3" CssClass="txtBoxEditProfile" runat="server"  ></asp:TextBox>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                     <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label> City: <asp:TextBox ID="txtHomeCity" CssClass="txtUserProfileCity" runat="server"  ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorHomeCity" runat="server" ControlToValidate="txtHomeCity"  ErrorMessage="Home city required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="row-div clearfix" id="divState" runat="Server" visible="false">
                                                       State:  <asp:DropDownList ID="cmbHomeState" CssClass="cmbUserProfileState" runat="server" >
                                                         </asp:DropDownList>
                                                    </div>
                                                     <div class="row-div clearfix">
                                                      <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label> County: <asp:TextBox ID="txtHomeCounty" CssClass="txtUserProfileCity" runat="server"   ></asp:TextBox>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHomeCounty"  ErrorMessage="Home county required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                     <div class="row-div clearfix">
                                                      Post code: <asp:TextBox ID="txtHomeZipCode" CssClass="txtUserProfileZipCode" runat="server"   ></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtHomeZipCode"  ErrorMessage="Home post code required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                     <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label> Country:  <asp:DropDownList ID="cmbHomeCountry" CssClass="cmbUserProfileCountry" runat="server"  
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                                         <asp:RequiredFieldValidator InitialValue="Select country" ID="Req_ID"
                                    Display="Dynamic" runat="server" ControlToValidate="cmbHomeCountry"
                                    Text="" ErrorMessage="Home country required" CssClass="required-label" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>

                                                      <div class="row-div clearfix">
                                                     <asp:Label ID="Label10" runat="server" Text="*" ForeColor="Red"></asp:Label> Country of origin:  <asp:DropDownList ID="cmbCountryofOrigin" CssClass="cmbUserProfileCountry" runat="server"  
                                                >
                                            </asp:DropDownList>
                                                         <asp:RequiredFieldValidator InitialValue="Select country of origin" ID="RequiredFieldValidator7"
                                    Display="Dynamic" runat="server" ControlToValidate="cmbCountryofOrigin"
                                    Text="" ErrorMessage="Country of origin required" CssClass="required-label" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                      <div class="row-div clearfix">
                                        <div class="label-div-left-align w29">
                                            <div class="label-div w40">
                                              <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label> Mobile telephone number :
                                            </div>
                                            <div class="field-div1 w75">
                                             <span style="width:25%;display:inline-block;">(Country code)</span>
						                     <span style="width:25%;display:inline-block;">(Mobile prefix/area code)</span>
						                     <span style="width:48.666%;display:inline-block;">(Number)</span>
                                                  <rad:RadMaskedTextBox ID="txtIntlCode" CssClass="txtUserProfileAreaCodeSmall" runat="server"
                                                        Mask="(###)" Width="25%"  >
                                                    </rad:RadMaskedTextBox>
                                                    <rad:RadMaskedTextBox ID="txtPhoneAreaCode" CssClass="txtUserProfileAreaCodeSmall"
                                                        runat="server" Mask="(####)" Width="25%"  >
                                                    </rad:RadMaskedTextBox>
                                                    <rad:RadMaskedTextBox ID="txtPhone" CssClass="txtUserProfileAreaCode" runat="server"
                                                        Mask="###-####" Width="45%" >
                                                    </rad:RadMaskedTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPhone"  ErrorMessage="Mobile telephone number required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div>
                </div>
                <div class="cai-form">
                <h2 class="expand form-title" onclick="CollapseExpand('divSummer','hdnSummer',this)">
                    Summer exam selection</h2>
                 
                <div class="collapse form-content" id="divSummer">
                    <br />
                    <asp:Label ID="lblSummerExamLocation" runat="server" Text="Exam location :"></asp:Label>
                    <asp:DropDownList ID="drpSummerExamLocation" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                    <p>
                      <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" >
                            <ContentTemplate>
                        <Telerik:RadGrid ID="grdSummerExamSession" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="false" Visible="false" AllowSorting="false" CssClass="cai-table">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                <Columns>
                                    <Telerik:GridTemplateColumn HeaderText="Curriculum" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Curriculum") %>'></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Paper" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Summer exam" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSummerExam" runat="server" AutoPostBack="true" OnCheckedChanged="chkSummerExam_CheckedChanged"
                                                align="Center" />
                                            <asp:Label ID="lblSummerCourseID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurriculumID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblSummerIntrimClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SummerIntrimClassID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblSummerIntrimProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SummerIntrimProductID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                                                Visible="false"></asp:Label>
                                             <asp:Label ID="lblClassName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassName") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblSummerIntrimClassName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SummerIntrimClassName")%>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Comments" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Comments") %>'
                                                Width="400px"></asp:Label>
                                            <asp:Label ID="lblIsDEBK" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IsDEBK__c") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </Telerik:RadGrid>
                        <asp:Label ID="lblErrorSummerExamSelection" runat="server" Text="" ForeColor="Red"></asp:Label>
                         </ContentTemplate> 
                        </asp:UpdatePanel> 
                    </p>
                </div>
                      </div>
                <div class="cai-form">
                <h2 class="expand form-title" onclick="CollapseExpand('divAutum','hdnAutum',this)">
                    Autumn exam selection</h2>
                <div class="collapse form-content" id="divAutum">
                    <br />
                    <asp:Label ID="lblAutumnExamLocation" runat="server" Text="Exam location :"></asp:Label>
                    <asp:DropDownList ID="drpAutumnExamLocation" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <div style="color:red;" class="info-note">If you wish to enrol onto the CAP1 Taxation (ROI)/(NI) autumn 18/19 exam please 
                        contact <a href="mailto:cap1exam@charteredaccountants.ie">cap1exam@charteredaccountants.ie</a> who will assist you with your enrolment.</div> <%-- added by LeonaH for ticket 20731--%>
                    <p>
                        <b></b>
                        <br />
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate >
                        <Telerik:RadGrid ID="grdAutumExamSession" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="false" Visible="false" AllowSorting="false" CssClass="cai-table">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false" >
                                <Columns>
                                    <Telerik:GridTemplateColumn HeaderText="Curriculum" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurriculum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Curriculum") %>'></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Paper" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaper" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Autumn interim" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAutumINtrim" runat="server" AutoPostBack="true" OnCheckedChanged="chkAutumINtrim_CheckedChanged" />
                                            <asp:Label ID="lblAutumInterimProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumInterimProductID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblAutumInterimClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumInterimClassID") %>'
                                                Visible="false"></asp:Label>
                                              <asp:Label ID="lblAutumInterimClassName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumInterimClassName") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Autumn exam" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAutumExam" runat="server" AutoPostBack="true" OnCheckedChanged="chkAutumExam_CheckedChanged" />
                                            <asp:Label ID="lblAutumCourseID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblAutomExamCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurriculumID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumExamProductID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblIsDEBK" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IsDEBK__c") %>'
                                                Visible="false"></asp:Label>
                                             <asp:Label ID="lblAutumExamClassName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumExamClassName") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Comments" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Comments") %>'
                                                Width="400px"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </Telerik:RadGrid>
                        <asp:Label ID="lblAutumExamSession" runat="server" Text="" ForeColor="Red"></asp:Label>
                         </ContentTemplate>
                        </asp:UpdatePanel>
                    </p>
                </div>
               </div>
                <div class="cai-form">
                <h2 class="expand form-title" onclick="CollapseExpand('divCourse','hdnCourse',this)">
                    Course selection
                </h2>
                <div class="collapse form-content" id="divCourse">
                    <br />
                    <asp:Label ID="lblCourseLocation" runat="server" Text="Course location :"></asp:Label>
                    <asp:DropDownList ID="drpCourseLocation" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <p>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                        <Telerik:RadGrid ID="grdCourseSelection" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="false" Visible="false" AllowSorting="false" CssClass="cai-table">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                <Columns>
                                    <Telerik:GridTemplateColumn HeaderText="Curriculum" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurriculum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Curriculum") %>'></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Paper" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaper" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Summer revision course" AllowFiltering="false"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSummerRevisionCourse" runat="server" AutoPostBack="true" OnCheckedChanged="chkSummerRevisionExam_CheckedChanged" />
                                             <asp:Label ID="lblSummerClassName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SummerClassName") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Autumn revision course" AllowFiltering="false"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAutumRevisionCourse" runat="server" AutoPostBack="true" OnCheckedChanged="chkAutumRevisionExam_CheckedChanged" />
                                            <asp:Label ID="lblAutumRevisionClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumRevisionClassID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblAutumRevisionProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumRevisionProductID") %>'
                                                Visible="false"></asp:Label>
                                             <asp:Label ID="lblAutumRevisionClassName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumRevisionClassName") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Comments" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Comments") %>'
                                                Width="400px"></asp:Label>
                                            <asp:Label ID="lblSummerRevisionCourseID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SummerRevisionProductID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblIsDEBK" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IsDEBK__c") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurriculumID") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </Telerik:RadGrid>
                        <asp:Label ID="lblRevisionCourse" runat="server" Text="" ForeColor="Red"></asp:Label>
                         </ContentTemplate>
                        </asp:UpdatePanel>
                    </p>
                </div>
                </div>
                <div class="cai-form">
                <h2 class="expand form-title" onclick="CollapseExpand('divCreditCardDetails','hdnCreditCard',this)">
                    Credit card information</h2>
                <div class="collapse form-content" id="divCreditCardDetails">
                   <br />
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                        <Telerik:RadGrid ID="radSummerPaymentSummery" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="false" Visible="false" AllowSorting="false" Width="50%" CssClass="cai-table">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                <Columns>
                                    <Telerik:GridTemplateColumn HeaderText="Class" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClass" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Class") %>'></asp:Label>
                                             <asp:Label ID="lblPaymentProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                               
                                     <Telerik:GridTemplateColumn HeaderText="Price" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Price") %>'></asp:Label>
                                              &nbsp;
                                                    <asp:Label ID="lblTaxAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Tax") %>'
                                                        Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </Telerik:RadGrid>
                        <br />
                         
                  <b><asp:Label ID="lblAmount" runat="server" Text="Total amount:" Visible="false"></asp:Label>  <asp:Label ID="lblTotalAmount" runat="server" Text="" Visible="false"></asp:Label> </b> 
                 <b> <asp:Label ID="lblTax" runat="server" Text="Tax amount" Visible="false"></asp:Label> <asp:Label ID="lblTaxAmount" runat="server" Text="" Visible="false"></asp:Label> </b> 
                     </ContentTemplate>
                     </asp:UpdatePanel>
                    <div class="field-group"
                    <p>
                    <uc1:CreditCard ID="CreditCard" runat="server" />
                    </p> 
                   </div>  
                </div>
               </div>
            </div>
            <div align="center">
               <asp:UpdatePanel ID="Up1" runat="server">
                <ContentTemplate>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="OpenCreditCardDiv()" class="submitBtn"/>
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick ="openReportWindow();" class="submitBtn"/>
                <Telerik:RadWindow ID="radMockTrial" runat="server" Width="350px" Height="120px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="External application" Behavior="None">
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
                                <asp:Button ID="btnOKValidation" runat="server" Visible="false" Text="Ok" Width="70px"
                            class="submitBtn"  CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </Telerik:RadWindow>
            <Telerik:RadWindow ID="radEligibiltyOption" runat="server" Width="500px" Height="200px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="External application" Behavior="None">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblEligibilityText" runat="server" Font-Bold="true" Text=""></asp:Label><br />
                                <asp:LinkButton ID="lnkDownload" runat="server" Visible="false" CausesValidation="false">Download</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnEligiblity" runat="server" Text="Ok" Width="70px" class="submitBtn" CausesValidation="false"/>
                                
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </Telerik:RadWindow>
                </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="hdnReportPath" runat="server" />
            </div>
            <div class="actions">
    <span class="label-title">Use and protection of your personal information</span>
                The Institute will use the information which you have provided in this form to respond to your request or process your transaction and will hold and protect it 
                    in accordance with the Institute’s <a href="https://www.charteredaccountants.ie/Privacy-statement" target="_blank"><strong>privacy statement</strong></a>, which explains your rights in relation to your personal data.</td>
</div>
            <cc1:User ID="User1" runat="server" />
            
        </div>
        <asp:HiddenField ID="hdnSummer" runat="server" Value="0" />
        <asp:HiddenField ID="hdnAutum" runat="server" Value="0" />
        <asp:HiddenField ID="hdnCourse" runat="server" Value="0" />
        <asp:HiddenField ID="hdnCreditCard" runat="server" Value="0" />
        <asp:HiddenField ID="hdnHeader" runat="server" Value="0" />
    </div>
</div>

