<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StudentDashboard__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_StudentDashboard__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<cc1:User runat="server" ID="User1" />
<%--<link href="../../CSS/Div_Data_form.css" rel="stylesheet" type="text/css" />--%>
<style type="text/css">
    .GridViewHeaderNew
    {
        /* background: #B94D0A;*/
        font-weight: bold;
        color: Black;
        font-size: 12px;
        text-align: left !important;
        background-color: #B94D0A;
        height: auto;
        padding-left: 0px;
    }
    
    .caption, th
    {
        text-align: left;
        font-weight: bold;
        padding-left: 2px;
    }
    .GridFooterNew
    {
        background-color: White;
        color: Black;
        font-weight: bold;
    }
    
    .GridItemStyleNew
    {
        height: auto; /*padding: 10px 10px 10px 10px !important;
        border-color :White;*/
    }
    
    .GridCellStyleNew
    {
        height: auto; /*padding: 10px 10px 10px 10px !important;*/
        border-right-color: White;
    }
    
    .FirstCellText
    {
        display: block;
        height: 128px;
        width: 128px;
        vertical-align: bottom;
        -webkit-transform: rotate(90deg);
        -moz-transform: rotate(90deg);
        -ms-transform: rotate(-90deg);
        -o-transform: rotate(90deg);
        transform: rotate(-90deg);
        -webkit-transform-origin: 50% 50%;
        -moz-transform-origin: 50% 50%;
        -ms-transform-origin: 50% 50%;
        -o-transform-origin: 50% 50%;
        transform-origin: 60% 60%;
        filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=1); /* writing-mode: tb-rl;
        filter: fliph() flipV();*/ /* display: block;
        height: 128px;
        width: 128px;
        vertical-align: bottom;
        -webkit-transform: rotate(-90deg);
        -moz-transform: rotate(-90deg);
        -o-transform: rotate(-90deg);
        transform: rotate(-90deg);
        filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=1);*/
    }
    
    .main-container
    {
        margin: 0 1% 0 1%;
        width: 98%;
        word-wrap: break-word;
        overflow: hidden;
    }
    .container-left
    {
        float: left;
        overflow: hidden;
        width: 54%;
    }
    .container-right
    {
        float: right;
        overflow: hidden;
        width: 46%;
    }
    .lable-c
    {
        float: left;
        text-align: left;
        margin-right: 1%;
        font-weight: bold;
        font-size: 12px;
        overflow: hidden;
    }
    .w42
    {
        width: 42.5%;
    }
</style>
<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>
<div>
    <asp:UpdatePanel ID="UpAreasOfExp" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="main-container" style="width: 1000px;">
                <div>
                    <div class="row-div">
                        <div class=" container-left">
                            <div class="row-div clearfix">
                                <div class="lable-c w42">
                                    Profile Information:
                                </div>
                                <div class="field-div1">
                                    <asp:LinkButton ID="lnkHistoryProfileInfo" Style="text-decoration: underline;" runat="server">History of Profile Information</asp:LinkButton>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="lable-c w91">
                                    Student#:
                                    <asp:Label ID="lblStudentName" Width="172px" runat="server" Text=""></asp:Label>
                                </div>
                                <%-- <div class="field-div1 w75">
                        
                    </div>--%>
                                <div class="lable-c w9">
                                    Mentor:
                                </div>
                                <div class="field-div1 w75">
                                    <asp:Label ID="lblMentorName" Width="150px" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hidMetorID" Value="0" runat="server" />
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="lable-c w91">
                                    Company:
                                    <asp:Label ID="lblCompanyName" Style="font-weight: normal;" Width="170px" runat="server"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="hidCompany" Value="0" runat="server" />
                                    <asp:HiddenField ID="hidECCompanyID" Value="0" runat="server" />
                                </div>
                                <div class="lable-c w23">
                                    Date of Registration:
                                </div>
                                <div class="field-div1 w75">
                                    <asp:Label ID="lblRegDate" Width="150px" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="lable-c w91">
                                    Route of Entry:
                                    <asp:Label ID="lblRouteOfEntry" Style="font-weight: normal;" Width="140px" runat="server"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="hidRouteOfEntry" Value="0" runat="server" />
                                </div>
                                <div class="lable-c w23">
                                    Date of Completion:
                                </div>
                                <div class="field-div1 w75">
                                    <asp:Label ID="lblDateOfCompletion" Width="150px" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class=" container-right">
                            <div id="divMenu" runat="server" class="row-div clearfix">
                                <div class="lable-c">
                                    MENU:
                                </div>
                                <div>
                                    <br />
                                </div>
                                <div class="row-div clearfix">
                                    <div style="width: 240px;" class="field-div1 w225">
                                        <div>
                                            <asp:LinkButton ID="lnkCreateNewDiaryEntry" Style="text-decoration: underline;" runat="server">Create a New Diary Entry</asp:LinkButton></div>
                                        <div>
                                            <asp:LinkButton ID="lnkModifyDiaryEntry" Style="text-decoration: underline;" runat="server">Modify an Existing Diary Entry</asp:LinkButton></div>
                                        <div>
                                            <asp:LinkButton ID="lnkReqMentorReview" Style="text-decoration: underline;" runat="server">Request a 6 monthly Mentor Review</asp:LinkButton></div>
                                        <div>
                                            <asp:LinkButton ID="lnkSubmitQuerytoCAI" Style="text-decoration: underline;" runat="server">Submit Query to CAI</asp:LinkButton>
                                        </div>
                                        <div>
                                            <asp:LinkButton ID="lnkCADiaryReport" Style="text-decoration: underline;" runat="server">Generate CA Diary Report</asp:LinkButton></div>
                                        <div>
                                            <asp:LinkButton ID="lnkAdmissiontoMembership" Style="text-decoration: underline;"
                                                runat="server">Admission to Membership Request</asp:LinkButton></div>
                                    </div>
                                    <div class="field-div1 w40">
                                        <div>
                                            &nbsp;
                                        </div>
                                        <div>
                                            <asp:LinkButton ID="lnkViewMentorReview" Style="text-decoration: underline;" runat="server">View Mentor Review</asp:LinkButton>
                                        </div>
                                        <div>
                                            <asp:LinkButton ID="lnkRequestforFinalReview" Style="text-decoration: underline;"
                                                runat="server">Request for Final Review</asp:LinkButton>
                                        </div>
                                        <div>
                                            <asp:LinkButton ID="lnkCADiaryGuidelines" Style="text-decoration: underline;" runat="server">CA Diary Guidelines</asp:LinkButton>
                                        </div>
                                        <div>
                                            <asp:LinkButton ID="lnkCAIUpdates" Style="text-decoration: underline;" runat="server">CAI Updates</asp:LinkButton>
                                        </div>
                                        <div>
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="main-container" style="width: 1000px;">
                <div>
                    <div class="row-div">
                        <div class="row-div clearfix">
                            <div style="width: 550px;" class="field-div1 w550">
                                <div>
                                    <b>Mandatory Core Competencies : </b>
                                </div>
                                <div>
                                    &nbsp;</div>
                            </div>
                            <div style="width: 450px;" class="field-div1 w450">
                                <div>
                                    <b>Area of Experience :</b>
                                </div>
                                <div>
                                    <asp:DropDownList ID="ddlAreasOfExp" AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <%-- <div runat="server" id="divAreaofExpText" style="width: 450px;">
                    </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="row-div">
                        <div class="row-div clearfix">
                            <div style="width: 550px;" class="field-div1 w450">
                                <div style="width: 500px;">
                                    <asp:Label ID="lblNoMandatoryCoreCompetencies" Visible="false" runat="server" Style="color: Red;"
                                        Text=""></asp:Label>
                                    <asp:GridView ID="grdMandatoryCoreCompetencies" GridLines="Both" AllowPaging="false"
                                        AutoGenerateColumns="False" runat="server" Style="text-align: left;" OnDataBound="OnMandatoryCoreCompetenciesDataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Category" HeaderStyle-HorizontalAlign="Left" ItemStyle-BackColor="White"
                                                ItemStyle-HorizontalAlign="left" ItemStyle-Width="20%" ItemStyle-Font-Bold="true"
                                                DataField="CompetancyCategory" />
                                            <asp:BoundField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-BackColor="White"
                                                ItemStyle-Width="7%" DataField="CompetencyCode" />
                                            <asp:BoundField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BackColor="White"
                                                DataField="CompetencyName" ItemStyle-Width="30%" />
                                            <asp:BoundField HeaderText="Understand" HeaderStyle-HorizontalAlign="Left" ItemStyle-BackColor="White"
                                                DataField="Understand" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Apply" HeaderStyle-HorizontalAlign="Left" ItemStyle-BackColor="White"
                                                DataField="Apply" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Integrate" HeaderStyle-HorizontalAlign="Left" ItemStyle-BackColor="White"
                                                DataField="Integrate" ItemStyle-Width="10%" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div>
                                    <br />
                                </div>
                                <div>
                                    <b>Guidance:</b></div>
                                <div style="text-align: left;" class="row-div clearfix">
                                    <div style="text-align: left; vertical-align: middle; margin-top: -4px; font-size: large;
                                        font-weight: bold;" class="field-div1 w4">
                                        .</div>
                                    <div runat="server" id="divCompetenciesGuidence1">
                                    </div>
                                </div>
                                <div style="text-align: left;" class="row-div clearfix">
                                    <div style="text-align: left; vertical-align: middle; margin-top: -4px; font-size: large;
                                        font-weight: bold;" class="field-div1 w4">
                                        .</div>
                                    <div runat="server" id="divCompetenciesGuidence2">
                                    </div>
                                </div>
                                <div style="text-align: left;" class="row-div clearfix">
                                    <div style="text-align: left; vertical-align: middle; margin-top: -4px; font-size: large;
                                        font-weight: bold;" class="field-div1 w4">
                                        .</div>
                                    <div runat="server" id="divCompetenciesGuidence3">
                                    </div>
                                </div>
                                <%-- <div runat="server" id="divCompetenciesGuidence2">
                    </div>
                    <div runat="server" id="divCompetenciesGuidence3">
                    </div>--%>
                            </div>
                            <div style="width: 450px;" class="field-div1 w450">
                                <div>
                                    <div style="width: 420px;">
                                        <asp:Label ID="lblNoAreasofExperienceCompetencies" Visible="false" runat="server"
                                            Style="color: Red;"></asp:Label>
                                        <asp:GridView ID="grdAreasofExperience" GridLines="Both" Width="150px" AllowPaging="false"
                                            AutoGenerateColumns="False" runat="server" OnDataBound="OnAreasofExperienceDataBound">
                                            <AlternatingRowStyle BackColor="White" />
                                            <RowStyle VerticalAlign="Top" Height="5px" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Code" HeaderStyle-HorizontalAlign="NotSet" ItemStyle-BackColor="White"
                                                    ItemStyle-Width="30px" DataField="CompetencyCode" />
                                                <asp:BoundField HeaderText="Name" ItemStyle-BackColor="White" DataField="CompetencyName"
                                                    ItemStyle-Width="180px" />
                                                <asp:BoundField HeaderText="Understand" ItemStyle-BackColor="White" DataField="Understand"
                                                    ItemStyle-Width="40px" />
                                                <asp:BoundField HeaderText="Apply" ItemStyle-BackColor="White" DataField="Apply"
                                                    ItemStyle-Width="30px" />
                                                <asp:BoundField HeaderText="Integrate" ItemStyle-BackColor="White" DataField="Integrate"
                                                    ItemStyle-Width="40px" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                                <div>
                                    <b>Guidance:</b></div>
                                <div runat="server" style="width: 420px;" id="divAreaofExpGuidance">
                                </div>
                                <div runat="server" id="div2">
                                    <br />
                                </div>
                                <div runat="server" id="div3">
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="row-div">
                        <div class="row-div clearfix">
                            <div style="width: 550px;" class="field-div1 w550">
                                <div>
                                    <b>Regulated Experience in a Practice Environment : </b>
                                </div>
                                <div>
                                    <asp:Panel ID="Panel2" BorderWidth="1" Width="500px" Style="border: 1; background: white;"
                                        runat="server">
                                        <div class="row-div
clearfix">
                                            <div class="field-div1 w5">
                                                <div>
                                                    &nbsp;
                                                </div>
                                            </div>
                                            <div class="field-div1 w40">
                                                <div>
                                                    &nbsp;
                                                </div>
                                            </div>
                                            <div class="field-div1 w15">
                                                <div style="text-align: right;">
                                                    <b>Achived To Date</b>
                                                </div>
                                            </div>
                                            <div class="field-div1 w30">
                                                <div style="text-align: right;">
                                                    <b>Minimum Required for Auditing Requirement </b>&nbsp; <b>(Weeks)</b>
                                                </div>
                                            </div>
                                            <%-- <div style="vertical-align: bottom;">
                                    <b>(Weeks) </b>
                                </div>--%>
                                        </div>
                                        <div class="row-div clearfix">
                                            <div class="field-div1 w5">
                                                <div>
                                                    &nbsp;
                                                </div>
                                            </div>
                                            <div class="field-div1 w40">
                                                <div>
                                                    Company Audit
                                                </div>
                                                <div>
                                                    Other Audit
                                                </div>
                                                <div>
                                                    <b>Audit Experience Total Weeks </b>
                                                </div>
                                            </div>
                                            <div class="field-div1 w15">
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblCompanyAuditday" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblOtherAuditday" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotalAuditday" runat="server" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="field-div1 w30">
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblMinimumCompanyAuditday" Style="font-weight: bold;" runat="server"
                                                        Text=""></asp:Label>
                                                </div>
                                                <div style="text-align: right;">
                                                    &nbsp;
                                                </div>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblMinimumOtherAuditday" runat="server" Style="font-weight: bold;"
                                                        Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div style="width: 450px;" class="field-div1 w450">
                                <div>
                                    <b>Required Experience :</b>
                                </div>
                                <div>
                                    <div>
                                        <asp:Panel ID="Panel3" BorderWidth="1" Width="420px" Style="border: 1; background: white;"
                                            runat="server">
                                            <div>
                                                <div class="row-div
clearfix">
                                                    <div class="field-div1 w5">
                                                        <div>
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w40">
                                                        <div>
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                    <div class="field-div1
w20">
                                                        <div style="text-align: right;">
                                                            <b>Days Recorded To Date</b>
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w30">
                                                        <div style="text-align: right;">
                                                            <b>Required Days</b>
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w5">
                                                        <div>
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row-div
clearfix">
                                                    <div class="field-div1 w5">
                                                        <div>
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w40">
                                                        <div>
                                                            Prior Work Experience
                                                        </div>
                                                        <div>
                                                            Required Experience
                                                        </div>
                                                        <div>
                                                            <b>Total Experience</b>
                                                        </div>
                                                    </div>
                                                    <div class="field-div1
w20">
                                                        <div style="text-align: right;">
                                                            <asp:Label ID="lblPriorWorkExperienceToDate" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                        <div style="text-align: right;">
                                                            <asp:Label ID="lblReqExperienceToDate" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                        <div style="text-align: right;">
                                                            <asp:Label ID="lblTotalExperienceToDate" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w30">
                                                        <div>
                                                            &nbsp;
                                                            <%--<asp:Label ID="lblPriorWorkExperienceReqdays" Style="font-weight: bold;" runat="server"
                                                Text="23"></asp:Label>--%>
                                                        </div>
                                                        <div style="text-align: right;">
                                                            <asp:Label ID="lblReqExperienceReqdays" Style="font-weight: bold;" runat="server"
                                                                Text=""></asp:Label>
                                                        </div>
                                                        <div style="text-align: right;">
                                                            <asp:Label ID="lblTotalExperienceReqdays" runat="server" Style="font-weight: bold;"
                                                                Text=""></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w5">
                                                        <div>
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="row-div clearfix">
                                                    <div class="field-div1 w5">
                                                        <div>
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                    <div class="field-div1 w250">
                                                        <div>
                                                            <u><b>Out of Office</b></u>
                                                        </div>
                                                        <div style="width: 265px;">
                                                            <asp:GridView ID="grdOutofOffice" GridLines="None" ShowHeader="false" ShowFooter="true"
                                                                AllowPaging="false" AutoGenerateColumns="False" runat="server">
                                                                <HeaderStyle BackColor="White" BorderColor="Black" Height="10px" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <RowStyle VerticalAlign="Top" CssClass="GridItemStyleNew" />
                                                                <FooterStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="" ItemStyle-BackColor="White" DataField="Name" FooterText="Total -Out of Office" />
                                                                    <asp:BoundField HeaderText="" ItemStyle-BackColor="White" DataField="LeaveDay" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="row-div">
                        <div class="row-div clearfix">
                            <div style="width: 550px;" class="field-div1 w550">
                                <div>
                                    <b>Progress Summary : </b>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <asp:Panel ID="Panel1" Width="975px" BorderWidth="1" Style="border: 1; background: white;"
                        runat="server">
                        <div class="row-div">
                            <div class="row-div clearfix">
                                <div style="width: 555px;" class="field-div1 w500">
                                    <div>
                                        <div class="row-div clearfix">
                                            <div class="field-div1 w5">
                                                <div>
                                                    &nbsp;
                                                </div>
                                            </div>
                                            <div class="field-div1 w40">
                                                <div>
                                                    <u><b>Diary Entries</b></u>
                                                </div>
                                                <div>
                                                    In Progress:
                                                </div>
                                                <div>
                                                    Submitted to Mentor for Review:
                                                </div>
                                                <div>
                                                    Reviewed(Locked):
                                                </div>
                                                <div>
                                                    <b>Total Diary Entries</b>
                                                </div>
                                            </div>
                                            <div class="field-div1 w15">
                                                <div>
                                                    &nbsp;
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblInProgressDiaryEntry" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblMentorDiaryEntry" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblReviewDiaryEntry" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblTotalDiaryEntry" runat="server" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 420px;" class="field-div1 w450">
                                    <div>
                                        <div class="row-div clearfix">
                                            <div class="field-div1 w40">
                                                <div>
                                                    <u><b>Mentor Review</b></u>
                                                </div>
                                                <div>
                                                    Number of Mentor Reviews:
                                                </div>
                                                <div>
                                                    Date of Last Review:
                                                </div>
                                                <div>
                                                    Target next Review Date:
                                                </div>
                                            </div>
                                            <div class="field-div1 w25">
                                                <div>
                                                    &nbsp;</div>
                                                <div>
                                                    <asp:Label ID="lblNumbeofMentorReviews" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblDateofLastReview" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblDateofNextReview" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div>
                <br />
                <br />
            </div>
            <div align="right">
                <asp:Button ID="btnMainBack" runat="server" Visible="false" Text="Back" class="submitBtn" />
            </div>
            <telerik:RadWindow ID="radwindowHistory" runat="server" VisibleOnPageLoad="false"
                Height="390px" Title=" History of Profile Information" Width="550px" BackColor="#f4f3f1"
                VisibleStatusbar="false" Behaviors="None" ForeColor="#BDA797">
                <ContentTemplate>
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <asp:Label ID="lblHistoryMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="row-div clearfix">
                            <rad:RadGrid ID="grdHistoryProfileInfo" runat="server" AutoGenerateColumns="false"
                                ShowHeader="true" Style="margin-top: 13px; overflow: auto;" CellSpacing="0" GridLines="None"
                                Height="250px">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                                    </Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowHeadersWhenNoRecords="true">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="Start Date" ItemStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartDate" runat="server" Width="80px" Text='<%#DataBinder.Eval(Container.DataItem,"ContractStartDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="EndDate" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEndDate" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"ContractExpireDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Route of Entry">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRouteofEntry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RouteOfEntry")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CompanyName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                            <div>
                                <br />
                            </div>
                            <div runat="server" id="divStartEndDateNote">
                            </div>
                        </div>
                        <div align="right">
                            <asp:Button ID="btnBack" runat="server" Text="Back" class="submitBtn" Width="20%" />
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow ID="radWindowReqMentorReview" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Monthly Review From Mentor" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblReviewFromMentor" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div>
                                    <br />
                                </div>
                                <div>
                                    <asp:Button ID="btnReviewYes" runat="server" Text="Yes" Width="70px" class="submitBtn" />
                                    <asp:Button ID="btnReviewNo" runat="server" Text="No" Width="70px" class="submitBtn" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow ID="radWindowSubmitaQuerytoCAI" runat="server" Width="450px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Submit a Query to CAI" Behavior="None" Height="320px">
                <ContentTemplate>
                    <div>
                        <div>
                            <div>
                                <br />
                                <div class="row-div clearfix">
                                    <div class="lable-c w30" style="text-align: right;">
                                        &nbsp;
                                    </div>
                                    <div class="field-div1 w175">
                                        <div style="text-align: left;">
                                            <%--<asp:Label ID="lblValidationQuerytoCAI" runat="server" Style="color: Red;" Text=""></asp:Label>--%>
                                            <div>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlQuestionCategory"
                                                    ErrorMessage="Please select Question Category" CssClass="required-label" Operator="NotEqual"
                                                    ValidationGroup="S" ValueToCompare="Select" SetFocusOnError="true"></asp:CompareValidator>
                                            </div>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuestion"
                                                    ErrorMessage="Please enter Question" ValidationGroup="S" SetFocusOnError="true"
                                                    Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="lable-c w30" style="text-align: right;">
                                        <span style="color: Red;">*</span> Question Category:
                                    </div>
                                    <div class="field-div1 w175">
                                        <asp:DropDownList ID="ddlQuestionCategory" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row-div clearfix">
                                    <div class="lable-c w30" style="text-align: right;">
                                        <span style="color: Red;">*</span>Question:
                                    </div>
                                    <div class="field-div1 w375">
                                        <asp:TextBox ID="txtQuestion" Style="resize: none;" TextMode="MultiLine" Width="250px"
                                            runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                    <br />
                                </div>
                                <div style="text-align: center;">
                                    <asp:Button ID="btnSubmitQuestion" ValidationGroup="S" runat="server" Text="Submit"
                                        Width="70px" class="submitBtn" />
                                    <asp:Button ID="btnCancelQuestion" runat="server" Text="Cancel" Width="70px" class="submitBtn" />
                                </div>
                                <div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblValidationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
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
            <telerik:RadWindow ID="radWindowReqFinalReview" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Request For Final Review" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblReqFinalReview" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div>
                                    <br />
                                </div>
                                <div>
                                    <asp:Button ID="btnReqFinalReviewYes" runat="server" Text="Yes" Width="70px" class="submitBtn" />
                                    <asp:Button ID="btnReqFinalReviewNo" runat="server" Text="No" Width="70px" class="submitBtn" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
