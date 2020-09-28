<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ExternalApp__c"
    CodeFile="ExternalApp__c.ascx.vb" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../../Scripts/expand.js" type="text/javascript"></script>
<script src="../../Scripts/JScript.min.js" type="text/javascript"></script>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
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
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1760px;">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="content-container clearfix">
    <div>
        <div>
            <asp:Label ID="lblSuccessMsg" runat="server" Text=""></asp:Label>
        </div>
        <div align="center">
            <b>
                <asp:Label ID="lblFirstLastHeading" runat="server" Text="Name:"></asp:Label>
            </b>
            <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label>
            <b>
                <asp:Label ID="lblStudentNumberHeading" runat="server" Text="Student Number:"></asp:Label></b>
            <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label>
            <b>
                <asp:Label ID="lblAcademicCycleHeading" runat="server" Text="Academic Cycle:"></asp:Label></b>
            <asp:Label ID="lblAcademicCycle" runat="server" Text=""></asp:Label>
            <b>
                <asp:Label ID="lblStatusHeading" runat="server" Text="Status:"></asp:Label>
            </b>
            <asp:Label ID="lblStatus" runat="server" Text="In Progress"></asp:Label>
            <b>
                <asp:Label ID="lblCommentsHeading" runat="server" Text="Comments:"></asp:Label></b>
            <asp:Label ID="lblComments" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <div align="center">
                <asp:HyperLink ID="HyperLink1" runat="server" class="expand" onclick="CollapseAll(this)">Expand All</asp:HyperLink>
                <%--<h1 id="idExpandAll" class="expand" onclick="CollapseAll(this)"  align="center" > Exapnd All</h1>--%>
            </div>
            <div>
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
            </div>
            <div class="demo" runat="server" id="idDemodiv">
                <h2 class="expand" onclick="CollapseExpand('divHeaderPanel','hdnHeader',this)">
                    Enrollment Details</h2>
                <div class="collapse" id="divHeaderPanel">
                    <table width="100%">
                        <tr>
                            <td width="50%" valign="top">
                                <asp:Label ID="lblRoute" runat="server" Text="Route Of Entry :"></asp:Label>
                                &nbsp;<b><asp:Label ID="lblRouteOfEntry" runat="server" Text=""></asp:Label></b>
                            </td>
                            <td width="50%">
                                <asp:Label ID="lblFurtherInformation" runat="server" Text="Please provide any further information:"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtFurtherInfo" runat="server" TextMode="MultiLine" Width="450px"
                                    Style="resize: none"></asp:TextBox><br />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                </div>
                <h2 class="expand" onclick="CollapseExpand('divSummer','hdnSummer',this)">
                    Summer Exam Selection</h2>
                <div class="collapse" id="divSummer">
                    <br />
                    <asp:Label ID="lblSummerExamLocation" runat="server" Text="Exam Location :"></asp:Label>
                    <asp:DropDownList ID="drpSummerExamLocation" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                    <p>
                      <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" >
                            <ContentTemplate>
                        <Telerik:RadGrid ID="grdSummerExamSession" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="false" Visible="false" AllowSorting="false">
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
                                    <Telerik:GridTemplateColumn HeaderText="Summer Exam" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
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
                <h2 class="expand" onclick="CollapseExpand('divAutum','hdnAutum',this)">
                    Autumn Exam Selection</h2>
                <div class="collapse" id="divAutum">
                    <br />
                    <asp:Label ID="lblAutumnExamLocation" runat="server" Text="Exam Location :"></asp:Label>
                    <asp:DropDownList ID="drpAutumnExamLocation" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <p>
                        <b></b>
                        <br />
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                        <Telerik:RadGrid ID="grdAutumExamSession" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="false" Visible="false" AllowSorting="false">
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
                                    <Telerik:GridTemplateColumn HeaderText="Autumn Interim" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAutumINtrim" runat="server" AutoPostBack="true" OnCheckedChanged="chkAutumINtrim_CheckedChanged" />
                                            <asp:Label ID="lblAutumInterimProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumInterimProductID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblAutumInterimClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumInterimClassID") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Autumn Exam" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
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
                <h2 class="expand" onclick="CollapseExpand('divCourse','hdnCourse',this)">
                    Course Selection
                </h2>
                <div class="collapse" id="divCourse">
                    <br />
                    <asp:Label ID="lblCourseLocation" runat="server" Text="Course Location :"></asp:Label>
                    <asp:DropDownList ID="drpCourseLocation" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <p>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                        <Telerik:RadGrid ID="grdCourseSelection" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="false" Visible="false" AllowSorting="false">
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
                                    <Telerik:GridTemplateColumn HeaderText="Summer Revision Course" AllowFiltering="false"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSummerRevisionCourse" runat="server" AutoPostBack="true" OnCheckedChanged="chkSummerRevisionExam_CheckedChanged" />
                                        </ItemTemplate>
                                    </Telerik:GridTemplateColumn>
                                    <Telerik:GridTemplateColumn HeaderText="Autumn Revision Course" AllowFiltering="false"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAutumRevisionCourse" runat="server" AutoPostBack="true" OnCheckedChanged="chkAutumRevisionExam_CheckedChanged" />
                                            <asp:Label ID="lblAutumRevisionClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumRevisionClassID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblAutumRevisionProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AutumRevisionProductID") %>'
                                                Visible="false"></asp:Label>
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
                <h2 class="expand" onclick="CollapseExpand('divCreditCardDetails','hdnCreditCard',this)">
                    Credit Card Information</h2>
                <div class="collapse" id="divCreditCardDetails">
                   <br />
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                        <Telerik:RadGrid ID="radSummerPaymentSummery" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="false" Visible="false" AllowSorting="false" Width="150px">
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
                         
                  <b><asp:Label ID="lblAmount" runat="server" Text="Total Amount:" Visible="false"></asp:Label>  <asp:Label ID="lblTotalAmount" runat="server" Text="" Visible="false"></asp:Label> </b> 
                 <b> <asp:Label ID="lblTax" runat="server" Text="Tax Amount" Visible="false"></asp:Label> <asp:Label ID="lblTaxAmount" runat="server" Text="" Visible="false"></asp:Label> </b> 
                     </ContentTemplate>
                     </asp:UpdatePanel>
                    <p>
                    <uc1:CreditCard ID="CreditCard" runat="server" />
                    </p> 
                     
                </div>
            </div>
            <div align="center">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="OpenCreditCardDiv()" />
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick ="openReportWindow();" />
                <asp:HiddenField ID="hdnReportPath" runat="server" />
            </div>
            <cc1:User ID="User1" runat="server" />
            <Telerik:RadWindow ID="radMockTrial" runat="server" Width="350px" Height="120px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="External Application" Behavior="None">
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
            </Telerik:RadWindow>
        </div>
        <asp:HiddenField ID="hdnSummer" runat="server" Value="0" />
        <asp:HiddenField ID="hdnAutum" runat="server" Value="0" />
        <asp:HiddenField ID="hdnCourse" runat="server" Value="0" />
        <asp:HiddenField ID="hdnCreditCard" runat="server" Value="0" />
        <asp:HiddenField ID="hdnHeader" runat="server" Value="0" />
    </div>
</div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
