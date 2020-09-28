<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.EducationFAEResultDetails__c"
    CodeFile="EducationFAEResultDetails__c.ascx.vb" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../../Scripts/expand.js" type="text/javascript"></script>
<script src="../../Scripts/JScript.min.js" type="text/javascript"></script>
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
      cursor:pointer;
    }
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        var PanelState1 = $("#<%=hdnCurricula.ClientID%>").val();
        var PanelState2 = $("#<%=hdnInterim.ClientID%>").val();
        var PanelState3 = $("#<%=hdnMock.ClientID%>").val();
        var PanelState4 = $("#<%=hdnCurrent.ClientID%>").val();
        //$('#divCurrent').parent().find("h2").css("color","blue");
        if (PanelState4 == '1') {
            $('#divCurrent').removeClass("collapse").addClass("active");
        }
        if (PanelState3 == '1') {
            $('#divMock').removeClass("collapse").addClass("active");
        }
        if (PanelState2 == '1') {
            $('#divInterim').removeClass("collapse").addClass("active");
        }
        if (PanelState1 == '1') {
            $('#first').removeClass("collapse").addClass("active");
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
        if (HiddenPanelState == 'hdnCurricula') {
            $("#<%=hdnCurricula.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnInterim') {
            $("#<%=hdnInterim.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnMock') {
            $("#<%=hdnMock.clientID %>").val(StateValue);
        }
        if (HiddenPanelState == 'hdnCurrent') {
            $("#<%=hdnCurrent.clientID %>").val(StateValue);
        }
    }





    //govind started code here for exapnd all

    function CollapseAll(hidnID) {

        //For First Div
        var FirstPanel = $("#first").attr("class");
        $("#first").show('slow');
        $("#first").removeClass("collapse").addClass("active");
        SetPanelState('hdnCurricula', 1)


        //For second Div
        var SecondPanel = $("#divInterim").attr("class");
        $("#divInterim").show('slow');
        $("#divInterim").removeClass("collapse").addClass("active");
        SetPanelState('hdnInterim', 1)

        //For Third Div
        var ThirdPanel = $("#divMock").attr("class");
        $("#divMock").show('slow');
        $("#divMock").removeClass("collapse").addClass("active");
        SetPanelState('hdnMock', 1)

        //For Third Div
        var FourthPanel = $("#divCurrent").attr("class");
        $("#divCurrent").show('slow');
        $("#divCurrent").removeClass("collapse").addClass("active");
        SetPanelState('hdnCurrent', 1)
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

</script>
<div class="content-container clearfix">
    <div>
        <div align="center">
            Name: <b>
                <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label></b> Student
            Number: <b>
                <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label></b>
            <%-- Academic Cycle:<b>
                <asp:Label ID="lblAcademicCycle" runat="server" Text=""></asp:Label></b></div>--%>
            <p>
            </p>
            <%--  <table width="100%">
                    <tr>
                        <td align="center">
                            Name: <b>
                                <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label></b> Student
                            Number: <b>
                                <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label></b> Academic
                            Cycle:<b>
                                <asp:Label ID="lblAcademicCycle" runat="server" Text=""></asp:Label></b>
                        </td>
                    </tr>
                </table>--%>
        </div>
        <div align="center" id="rdoAcademicCycle" runat="server" visible="false">
            <asp:RadioButton ID="rdoCurrentAcademicCycle" runat="server" GroupName="AcadmicCycle"
                Text="Current Academic Cycle: " AutoPostBack="true" Checked="true" />
            <asp:RadioButton ID="rdoNextAcadmicCycle" runat="server" GroupName="AcadmicCycle"
                Text="Next Academic Cycle: " AutoPostBack="true" />
                
        </div>

         <div align="center">
           Academic Year: <asp:DropDownList ID="ddlAcademicYear" 
                 runat="server" AutoPostBack="true" Width="8%" >
            </asp:DropDownList>
        </div>
        <div align="center" >
            <asp:HyperLink ID="HyperLink1" runat="server" class="expand" onclick="CollapseAll(this)">Exapnd All</asp:HyperLink>
            
           <%--<h1 id="idExpandAll" class="expand" onclick="CollapseAll(this)"  align="center" > Exapnd All</h1>--%>
                             
        </div>
        <table width="16%" align="right">
            <tr>
                <td>
                    <table  width="100%">
                        <tr>
                            <td>
                                <div class="RED" >
                                </div> 
                            </td>
                            <td>
                                <asp:Label ID="lblStatusMessage" runat="server" Text="" Font-Bold="true"></asp:Label><br />
                            </td>
                        </tr>
                      
                    </table>
                </td>
            </tr>
        </table>
    
       <%--  <div class="info-data" align="right" >
                    <div class="label-div w3">
                       <div class="RED">
                        </div>
                                   
                    </div>
                    <div class="field-div1 w50">
                         <asp:Label ID="lblStatusMessage" runat="server" Text="" Font-Bold="true" ></asp:Label>
                    </div>
                </div>--%>
        <div class="demo">
          <b><asp:Label ID="lblCommentsMessage" runat="server" Text=""></asp:Label></b> <br />
            <h2 id="firstHead" class="expand" onclick="CollapseExpand('first','hdnCurricula',this)">
                Completed And Attempted Curricula</h2>
            <div id="first" class="collapse">
                <p>
                    <asp:Repeater ID="rpEducationResult" runat="server">
                        <HeaderTemplate>
                            <table width="100%"  style="border: solid 1px gray;
                                background-color: #e9e8e8">
                                <tr>
                                    <th style="width: 10%" align="left" valign="middle">
                                        Curriculum
                                    </th>
                                    <th style="width: 10%" align="left" valign="middle">
                                        Status
                                    </th>
                                    <th style="width: 10%" align="left" valign="middle">
                                        Location
                                    </th>
                                    <th style="width: 10%;padding-left:10px;" align="left" valign="middle">
                                        Academic Year Started
                                    </th>
                                    <th style="width: 10%;padding-left:10px;" align="left" valign="middle">
                                        Academic Year Completed
                                    </th>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table width="100%"  style="border: solid 1px green;
                                background-color: #f0fff0">
                                <tr>
                                  <td style="width: 10%;padding-left:10px;" >
                                        <asp:Label ID="lblCurriculum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Curriculum") %>'></asp:Label>
                                          <asp:Label ID="lblCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td style="width: 10%;padding-left:10px;">
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'></asp:Label>
                                    </td>
                                    <td style="width: 10%;padding-left:10px;">
                                        <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                    </td>
                                    <td style="width: 10%;padding-left:10px;">
                                        <asp:Label ID="lblAcademicYearStarted" runat="server" Text='<%# Eval("AcademicYearStarted") %>'></asp:Label>
                                         <asp:Label ID="lblAcademicCycleID" runat="server" Text='<%# Eval("AcademicCycleID") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td style="width: 10%;padding-left:10px;">
                                        <asp:Label ID="lblAcademicYearCompleted" runat="server" Text='<%# Eval("AcademicYearCompleted") %>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<Telerik:RadGrid ID="grdEducationResult" runat="server" AutoGenerateColumns="False"
                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                    AllowFilteringByColumn="True">
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <Telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression="ID"
                                DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" Visible="False" />
                            <Telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <Telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <Telerik:GridBoundColumn DataField="Location" HeaderText="Location" SortExpression="Location"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                          
                            <Telerik:GridTemplateColumn HeaderText="Academic Year Started" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblAcademicYearStarted" runat="server" Text='<%# Eval("AcademicYearStarted") %>'></asp:Label>
                                    <asp:Label ID="lblAcademicYearStartedID" runat="server" Text='<%# Eval("AcademicYearStartedID") %>'
                                        Visible="false"></asp:Label>
                                </ItemTemplate>
                            </Telerik:GridTemplateColumn>
                          
                            <Telerik:GridTemplateColumn HeaderText="Academic Year Completed" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblAcademicYearCompleted" runat="server" Text='<%# Eval("AcademicYearCompleted") %>'></asp:Label>
                                </ItemTemplate>
                            </Telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </Telerik:RadGrid>--%>
                    <br />
                    <asp:Label ID="lblErrorCourseCompleted" runat="server" Text="" ForeColor="Red"></asp:Label></p>
            </div>
            <h2 class="expand" onclick="CollapseExpand('divInterim','hdnInterim',this)">
                Interim Assessments</h2>
            <div class="collapse" id="divInterim">
                <asp:Repeater ID="rptInterimAssessments" runat="server">
                    <ItemTemplate>
                        <table width="100%" cellpadding="2" cellspacing="0" style="background-color: white;">
                            <tr>
                                <th style="width: 15%" align="left">
                                    Curriculum:
                                    <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                    <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                        Visible="false" />
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    <asp:Repeater ID="rpInterimDetails" runat="server">
                                        <HeaderTemplate>
                                            <table width="100%" cellpadding="4" cellspacing="10" style="border: solid 1px gray;
                                                background-color: #e9e8e8">
                                                <tr>
                                                    <th style="width: 10%" align="left" valign="middle">
                                                        Course
                                                    </th>
                                                    <th style="width: 10%" align="left" valign="middle">
                                                        Location
                                                    </th>
                                                    <th style="width: 10%" align="left" valign="middle">
                                                        Session
                                                    </th>
                                                    <th id="ScorResult" style="width: 10%;padding-left:10px;" align="center" valign="middle" runat="server">
                                                        Score
                                                    </th>
                                                    <th id="FAEResult" style="width: 10%;padding-left:10px;" align="center" valign="middle" runat="server"
                                                        visible="false">
                                                        Result
                                                    </th>
                                                  <%--  <th style="width: 10%;padding-left:10px;" align="center" valign="middle">
                                                        Comments
                                                    </th>--%>
                                                    <th style="width: 10%;padding-left:10px;" align="center" valign="middle" id="topScore" runat="server" visible="false">
                                                        Top Scorer
                                                    </th>
                                                     <th id="thIntrimTopScorerBlank" style="width: 10%;padding-left:5px;" align="center" valign="middle" runat="server" visible="false">
                                                           &nbsp;
                                                        </th>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green;
                                                background-color: #f0fff0">
                                                <tr>
                                                    <td style="width: 10%;padding-left:10px;">
                                                        <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;padding-left:10px;">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;padding-left:10px;">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;padding-left:10px;" id="tdScore" visible="false" runat="server">
                                                        <asp:Label ID="lblScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score") %>' Tooltip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' ></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;padding-left:10px;" runat="server" id="tdFAEResult" visible="false">
                                                        <asp:Label ID="lblFAEStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FAEIAPublishedScore__c") %>' Tooltip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                                    </td>
                                                 <%--   <td style="width: 10%;padding-left:10px;">
                                                        <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Comments") %>'></asp:Label>
                                                    </td>--%>
                                                    <td style="width: 10%;padding-left:10px;" ID="tdTopScoreComments" runat="server" visible="false">
                                                        <asp:Label ID="lblTopScorere" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Top Scorer") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;padding-left:10px;" ID="tdTopScoreBlank" runat="server" visible="false">
                                                        &nbsp; 
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                    <SeparatorTemplate>
                        <hr />
                    </SeparatorTemplate>
                </asp:Repeater>
                <br />
                <asp:Label ID="lblInterimErrorMsg" runat="server" Text="" ForeColor="Red"></asp:Label></p>
            </div>
            <%--      <h2 class="expand">
                Resit Assessments</h2>
            <div class="collapse">
                <p>
                    <asp:Repeater ID="rpResitAssessment" runat="server">
                        <ItemTemplate>
                            <table width="100%" cellpadding="2" cellspacing="0" style="background-color: white;">
                                <tr>
                                    <th style="width: 15%" align="left">
                                        Curriculum:
                                        <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                        <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                            Visible="false" />
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        <asp:Repeater ID="rpResitAssessmentDetails" runat="server">
                                            <HeaderTemplate>
                                                <table width="100%" cellpadding="4" cellspacing="10" style="border: solid 1px gray;
                                                    background-color: #e9e8e8">
                                                    <tr>
                                                        <th style="width: 10%" align="left" valign="middle">
                                                            Course
                                                        </th>
                                                        <th style="width: 10%" align="left" valign="middle">
                                                            Location
                                                        </th>
                                                        <th style="width: 10%" align="left" valign="middle">
                                                            Session
                                                        </th>
                                                        <th style="width: 10%;padding-left:10px;" align="center" valign="middle">
                                                            Score
                                                        </th>
                                                        <th style="width: 10%;padding-left:10px;" align="center" valign="middle">
                                                            Comments
                                                        </th>
                                                        <th style="width: 10%;padding-left:10px;" align="center" valign="middle">
                                                            Top Scorer
                                                        </th>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green;
                                                    background-color: #f0fff0">
                                                    <tr>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="lblScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Comments") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="lblTopScorere" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Top Scorer") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <SeparatorTemplate>
                            <hr />
                        </SeparatorTemplate>
                    </asp:Repeater>
                    <br />
                    <asp:Label ID="lblResitAssError" runat="server" Text="" ForeColor="Red"></asp:Label></p>
            </div>--%>
            <h2 class="expand" onclick="CollapseExpand('divMock','hdnMock',this)">
                Mock Exam</h2>
            <div class="collapse" id="divMock">
                <p>
                    <asp:Repeater ID="rpMockExam" runat="server">
                        <ItemTemplate>
                            <table width="100%" cellpadding="2" cellspacing="0" style="background-color: white;">
                                <tr>
                                    <th style="width: 15%" align="left">
                                        Curriculum:
                                        <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                        <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                            Visible="false" />
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        <asp:Repeater ID="rpMockExamDetails" runat="server">
                                            <HeaderTemplate>
                                                <table width="100%" cellpadding="4" cellspacing="10" style="border: solid 1px gray;
                                                    background-color: #e9e8e8">
                                                    <tr>
                                                        <th style="width: 10%" align="left" valign="middle">
                                                            Course
                                                        </th>
                                                        <th style="width: 10%" align="left" valign="middle">
                                                            Location
                                                        </th>
                                                        <%-- <th style="width: 10%" align="left" valign="middle">
                                                                Session
                                                            </th>--%>
                                                        <th id="ScorResult" style="width: 10%;padding-left:10px;" align="center" valign="middle" runat="server">
                                                            Score
                                                        </th>
                                                        <th id="FAEResult" style="width: 10%;padding-left:10px;" align="center" valign="middle" runat="server"
                                                            visible="false">
                                                            Result
                                                        </th>
                                                       <%-- <th style="width: 10%;padding-left:10px;" align="center" valign="middle">
                                                            Comments
                                                        </th>--%>
                                                        <th style="width: 10%;padding-left:10px;" align="center" valign="middle" id="topScore" runat="server" visible="false">
                                                            Top Scorer
                                                        </th>
                                                        <th id="thMockTopScorerBlank" style="width: 10%;padding-left:5px;" align="center" valign="middle" runat="server" visible="false">
                                                           &nbsp;
                                                        </th>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green;
                                                    background-color: #f0fff0">
                                                    <tr>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="lblChapterLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                                        </td>
                                                        <%--<td style="width: 10%;padding-left:10px;">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'></asp:Label>
                                                            </td>--%>
                                                        <td style="width: 10%;padding-left:10px;" id="tdScore" visible="false" runat="server">
                                                            <asp:Label ID="lblScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score") %>' Tooltip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;" runat="server" id="tdFAEResult" visible="false">
                                                            <asp:Label ID="lblFAEStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FAEStatus") %>' Tooltip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' ></asp:Label>
                                                        </td>
                                                       <%-- <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Comments") %>'></asp:Label>
                                                        </td>--%>
                                                        <td style="width: 10%;padding-left:10px;" ID="tdTopScoreComments" runat="server" visible="false" >
                                                            <asp:Label ID="lblTopScorere" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Top Scorer") %>'></asp:Label>
                                                        </td>
                                                         <td style="width: 10%;padding-left:10px;" ID="tdMockTopScoreBlank" runat="server" visible="false">
                                                        &nbsp; 
                                                    </td>

                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <SeparatorTemplate>
                            <hr />
                        </SeparatorTemplate>
                    </asp:Repeater>
                    <br />
                    <asp:Label ID="lblErrorMockExam" runat="server" Text="" ForeColor="Red"></asp:Label></p>
            </div>
            <h2 class="expand" onclick="CollapseExpand('divCurrent','hdnCurrent',this)">
                Current Exam</h2>
            <div class="collapse" id="divCurrent">
            
                <p>
                    <Telerik:RadGrid ID="grdCurrentExam" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                        SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                        AllowFilteringByColumn="True" Visible="false">
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                            <Columns>
                                <%--<Telerik:GridHyperLinkColumn Text="CourseID" DataTextField="CourseID" HeaderText="CourseID" SortExpression= "CourseID" DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />--%>
                                <Telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="Course" HeaderText="Course" SortExpression="Course"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="Location" HeaderText="Location" SortExpression="Location"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <Telerik:GridBoundColumn DataField="Session" HeaderText="Session" SortExpression="Session"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                <%--<Telerik:GridBoundColumn DataField="Status" HeaderText="Status"   SortExpression= "Status"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />--%>
                                <Telerik:GridTemplateColumn HeaderText="Status" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        <asp:Label ID="lblIsReport" runat="server" Text='<%# Eval("IsReport") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </Telerik:GridTemplateColumn>
                                <Telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkInterimUpdateGroup" runat="server" Text="Request" ForeColor="Blue"
                                            CommandName="ChangeGroup" Font-Underline="true" CommandArgument='<%# Eval("CourseID")  & "," & Eval("ClassID") & "," & Eval("ClassRegistrationID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </Telerik:GridTemplateColumn>
                                <Telerik:GridTemplateColumn UniqueName="Select" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" HeaderText="Report">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpReports" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </Telerik:GridTemplateColumn>
                                <Telerik:GridTemplateColumn UniqueName="ReportDownload" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" HeaderText="Report Download">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Report Download" ForeColor="Blue"
                                            CommandName="DownloadReport" Font-Underline="true"></asp:LinkButton>
                                    </ItemTemplate>
                                </Telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </Telerik:RadGrid>
                    <asp:Repeater ID="rpCurrentExam" runat="server">
                        <ItemTemplate>
                            <table width="100%" cellpadding="2" cellspacing="0" style="background-color: white;">
                                <tr>
                                    <th style="width: 15%" align="left">
                                        Curriculum:
                                        <asp:Label ID="lblH1" Text='<%# Eval("Curriculum") %>' runat="server" />
                                        <asp:Label ID="lblCurriculumID" Text='<%# Eval("CurriculumID") %>' runat="server"
                                            Visible="false" />
                                       
                                                                          
                                    </th>
                                </tr>
                                <tr id="trSummerCommentsID"  visible='<%# IIf(Eval("OverallHighScore") = "", false, true)%>' >
                                <th>
                                 <asp:Label ID="lblSummerSessionComment" runat="server" Text='<%# Eval("OverallHighScore") %>' ForeColor="Green"></asp:Label>
                                </th>
                                </tr>
                                 <tr id="trAutumnCommentsID"   visible='<%# IIf(Eval("AutumnComments") = "", false, true)%>'>
                                <th>
                                  <asp:Label ID="lblAutumnComment" Text='<%# Eval("AutumnComments") %>' runat="server"  ForeColor="Green"/>
                                </th>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        <asp:Repeater ID="rpCurrentExamDetails" runat="server" OnItemCommand="rpCurrentExamDetails_ItemCommand">
                                            <HeaderTemplate>
                                                <table width="100%" cellpadding="4" cellspacing="10" style="border: solid 1px gray;
                                                    background-color: #e9e8e8">
                                                    <tr>
                                                        <th style="width: 10%" align="left" valign="middle">
                                                            Course
                                                        </th>
                                                        <th style="width: 10%" align="left" valign="middle">
                                                            Location
                                                        </th>
                                                        <th style="width: 10%" align="left" valign="middle">
                                                            Session
                                                        </th>
                                                        <th id="ScorResult" style="width: 10%;padding-left:10px;" align="center" valign="middle" runat="server">
                                                            Score
                                                        </th>
                                                         <th id="thBlank" style="width: 10%;padding-left:10px;" align="center" valign="middle" runat="server" visible="false">
                                                            
                                                        </th>
                                                        <th id="FAEResult" style="width: 10%;padding-left:10px;" align="center" valign="middle" runat="server"
                                                            visible="false">
                                                            Result
                                                        </th>
                                                        <th style="width: 10%;padding-left:10px;" align="center" valign="middle" id="ExamComments" runat="server" visible="false">
                                                            Comments
                                                        </th>
                                                        <th id="topScore" style="width: 10%;padding-left:10px;" align="center" valign="middle" runat="server" visible="false">
                                                            Top Scorer
                                                        </th>
                                                        <th id="thExamTopScorerBlank" style="width: 10%;padding-left:10px;" align="center" valign="middle" runat="server" visible="false">
                                                           &nbsp;
                                                        </th>
                                                        <th style="width: 10%;padding-left:10px;" align="center" valign="middle">
                                                            Status
                                                        </th>
                                                        <th style="width: 10%;padding-left:10px;" align="center" valign="middle">
                                                            Information Scheme Report
                                                        </th>
                                                        <th style="width: 10%;padding-left:10px;" align="center" valign="middle">
                                                            Extenuating Circumstances
                                                        </th>
                                                        <th style="width: 10%;padding-left:10px;" align="center" valign="middle">
                                                            Clerical Recheck
                                                        </th>
                                                        <%-- <th style="width: 10%;padding-left:10px;" align="center" valign="middle">
                                                                Appeal Clerical Recheck
                                                            </th>--%>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="100%" cellpadding="2" cellspacing="0" style="border: solid 1px green;
                                                    background-color: #f0fff0">
                                                    <tr>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="lblRowID" runat="server" Text='<%# Container.ItemIndex + 1 %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblCourse" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Course") %>'></asp:Label>
                                                            <asp:Label ID="lblCourseID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="lblClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassID") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="lblClassRegistrationID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassRegistrationID") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="lblSessionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SessionID") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="lblExamNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExamNumber") %>'
                                                                Visible="false"></asp:Label>
                                                                   <asp:Label ID="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderID") %>'
                                                                Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Session") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;" id="tdScore" visible="false" runat="server">
                                                            <asp:Label ID="lblScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score") %>' Tooltip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' ></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;" id="tdBlank" visible="false" runat="server">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Score") %>' Tooltip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;" runat="server" id="tdFAEResult" visible="false">
                                                            <asp:Label ID="lblFAEStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FAEStatus") %>' Tooltip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' ></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;" id="tdComments" runat="server" visible="false">
                                                            <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Comments") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;" visible="false" runat="server" id="tdTopScoreComments">
                                                            <asp:Label ID="lblTopScorere" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Top Scorer") %>'></asp:Label>
                                                        </td>
                                                         <td style="width: 10%;padding-left:10px;" ID="tdExamTopScoreBlank" runat="server" visible="false">
                                                        &nbsp; 
                                                    </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>' Tooltip='<%# DataBinder.Eval(Container.DataItem, "Comments")%>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:DropDownList ID="drpSchemeReport" runat="server" Width="80px">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblSchemeReportStatus" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkSchemeReportStatus" runat="server" Text="" CommandName="Scheme Report Status"
                                                                Visible="false"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" CommandName="Download" Visible="false">Download Report</asp:LinkButton>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:LinkButton ID="lnkCircumstances" runat="server" Text="Request" OnClick="lnkCircumstances_OnClick"
                                                                Visible="false"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkCircumstancess" runat="server" CommandName="Extenuating Circumstances"
                                                                Text="Request" Visible="false"></asp:LinkButton>
                                                            <asp:Label ID="lblCircumstancess" runat="server" Text=""  ForeColor="Red" ></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;padding-left:10px;">
                                                            <asp:LinkButton ID="lnkClericalRecheck" runat="server" Text="Request" CommandName="Clerical Recheck"></asp:LinkButton>
                                                            <asp:Label ID="lblClericalRecheck" runat="server" Text=""  ForeColor="Red" ></asp:Label>
                                                        </td>
                                                        <%--  <td style="width: 10%;padding-left:10px;">
                                                                 <asp:LinkButton ID="lnkAppealClericalRecheck" runat="server" Text="Request" CommandName="Appeal Clerical Recheck"></asp:LinkButton>
                                                            </td>--%>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <SeparatorTemplate>
                            <hr />
                        </SeparatorTemplate>
                    </asp:Repeater>
                </p>
                <br />
                    <b> <asp:Label ID="lblDownloadFormatMessage" runat="server" Text=""  ></asp:Label></b>
                <br />
                <asp:Label ID="lblRagisteredInterimAssessments" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
          <%--  <h2 class="expand">
                Web Course Material</h2>
            <div class="collapse">
                <asp:LinkButton ID="lnkWebMaterial" runat="server" CausesValidation="false">Web Course Material</asp:LinkButton>
            </div>--%>
        </div>
        <%--
     <div id="Genral" runat="server">

        <asp:Panel ID="pnlgeneral" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="tdPersonalInfo" style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td>
                                    
                                </td>
                                <td>
                                    <div style="float: right;">
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/uparrow.jpg"
                                            AlternateText="(Show Details...)" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlgeneral1" runat="server">
            <table style="width: 100%;" class="data-form">
                <tr>
                    <td>
                        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="Server" TargetControlID="pnlgeneral1"
            ExpandControlID="pnlgeneral" CollapseControlID="pnlgeneral" Collapsed="false"
            BehaviorID="cpeForGeneral" ImageControlID="Image1" ExpandedText="(Hide Details...)"
            CollapsedText="(Show Details...)" ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
    <div id="DivInterimAssessments" runat="server">
        <asp:Panel ID="pnlInterimAssessments" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="tdPersonalInfo" style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td>
                                    Intrerim Assessments
                                </td>
                                <td>
                                    <div style="float: right;">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/uparrow.jpg"
                                            AlternateText="(Show Details...)" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlInterimAssessmentsDetail" runat="server">
            <table style="width: 100%;" class="data-form">
                <tr>
                    <td>
                      
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server" TargetControlID="pnlInterimAssessmentsDetail"
            ExpandControlID="pnlInterimAssessments" CollapseControlID="pnlInterimAssessments"
            Collapsed="true" BehaviorID="cpeForpnlInterimAssessments" ImageControlID="Image1"
            ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)" ExpandedImage="~/Images/uparrow.jpg"
            CollapsedImage="~/Images/downarrow.jpg" SuppressPostBack="true" />
    </div>


    <div id="divResitAssessment" runat="server">
        <asp:Panel ID="pnlResitAssessment" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="tdPersonalInfo" style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td>
                                    Resit Assessments
                                </td>
                                <td>
                                    <div style="float: right;">
                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/uparrow.jpg"
                                            AlternateText="(Show Details...)" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlRessitAssessmentDetails" runat="server">
            <table style="width: 100%;" class="data-form">
                <tr>
                    <td>
                        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="Server" TargetControlID="pnlRessitAssessmentDetails"
            ExpandControlID="pnlRessitAssessment" CollapseControlID="pnlRessitAssessmentDetails"
            Collapsed="true" BehaviorID="cpeForpnlRessitAssessment" ImageControlID="Image1"
            ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)" ExpandedImage="~/Images/uparrow.jpg"
            CollapsedImage="~/Images/downarrow.jpg" SuppressPostBack="true" />
    </div>
    <div id="divMockExam" runat="server">
        <asp:Panel ID="pnlMockExam" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="tdPersonalInfo" style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td>
                                    Mock Exam
                                </td>
                                <td>
                                    <div style="float: right;">
                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/uparrow.jpg"
                                            AlternateText="(Show Details...)" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlMockExamDetails" runat="server">
            <table style="width: 100%;" class="data-form">
                <tr>
                    <td>
                        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="Server" TargetControlID="pnlMockExamDetails"
            ExpandControlID="pnlMockExam" CollapseControlID="pnlMockExam"
            Collapsed="true" BehaviorID="cpeForpnlMockExam" ImageControlID="Image1"
            ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)" ExpandedImage="~/Images/uparrow.jpg"
            CollapsedImage="~/Images/downarrow.jpg" SuppressPostBack="true" />
    </div>
    <div id="DivExam" runat="server">
        <asp:Panel ID="pnlExam" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="tdPersonalInfo" style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td>
                                    Current Exam
                                </td>
                                <td>
                                    <div style="float: right;">
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/uparrow.jpg"
                                            AlternateText="(Show Details...)" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlExamDetails" runat="server">
            <table style="width: 100%;" class="data-form">
                <tr>
                    <td>
                       
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="Server" TargetControlID="pnlExamDetails"
            ExpandControlID="pnlExam" CollapseControlID="pnlExam" Collapsed="true" BehaviorID="cpeForpnlExam"
            ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
            ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>--%>
        <div>
            <table width="100%">
                <%--   <tr>
                <td align="right">
                    <asp:CheckBox ID="chkShowFirm" runat="server" Text="Show result to firm" />
                </td>
            </tr>--%>
                <tr>
                    <td>
                        <asp:LinkButton ID="lbtnCourseMaterial" runat="server" Text="Course Material" Font-Bold="true" Font-Underline="true" ForeColor="Blue"></asp:LinkButton>
                    </td>
                    <td></td>
                    <td align="right">
                        <asp:Button ID="btnSubmitPay" runat="server" Text="Submit/Pay" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
        <cc1:User ID="User1" runat="server" />
        <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
    </div>
    <asp:HiddenField ID="hdnCurricula" runat="server" Value="0" />
    <asp:HiddenField ID="hdnInterim" runat="server" Value="0" />
    <asp:HiddenField ID="hdnMock" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCurrent" runat="server" Value="0" />
    </div> 