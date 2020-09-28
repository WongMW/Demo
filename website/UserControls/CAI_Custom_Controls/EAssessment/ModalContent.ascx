<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalContent.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.EAssessment.ModalContent" %>
<%@ Register TagPrefix="modal" TagName="Wrapper" Src="~/UserControls/CAI_Custom_Controls/EAssessment/ModalContentVBWrapper.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<modal:Wrapper runat="server" ID="wrapper"></modal:Wrapper>
<cc1:User runat="server" ID="User1" />

<style>
    .bold {
        font-weight: bold;
    }
    .spacer {
        display: inline-block;
        margin-top: 20px;
        width: 100%;
    }
</style>

<div class="spacer"></div>
<div class="sfContentBlock">
    <p runat="server" id="lblEAssessment_str2">
        Students must accept these Rules & Regulations in order to be allowed sit the Institute's examinations.
        <br />
        To review these Rules & Regulation please click <a href='#' target="_blank">here</a>
    </p>
    <p>
        <asp:CheckBox runat="server" ID="chkRule1" />
        <label for="chkRule1" runat="server" id="lblEAssessment_str1" class="bold">
            Please confirm your acceptance of the Chartered Accounts Ireland student Rules & Regulations:
        </label>
    </p>

    <hr />

    <%--
    <p runat="server" id="lblEAssessment_str3" class="bold">
        CAP1 First students in 2019 will take their examinations using the Chartered Accountants Ireland e-assessment platform.
        The following options apply to you if you are a CAP1 student.
    </p>


    <div class="spacer"></div>

    <p runat="server" id="lblEAssessment_str4">
        Each E-Assessment will be invigilated by an on-line invigilator enhanced by an AI algorithm. Each assessment will be video
        recorded and stored for 15 working days after the assessment/examination so as to be able to address any immediate queries
        relating to that specific assessment/examination. Once this period has elapsed and any potential query has been concluded the
        video recordings will be deleted in line with the examinations retention <a href="#" target="_blank">policy</a>. The recording will be held by the third party
        invigilation provider. It can be reviewed at any time during the retention period by either the third party invigilation provider or by
        Chartered Accountants Ireland Examination Executives and if required, the Head of Assessment
    </p>

    <div class="spacer"></div>

    <p>
        <span runat="server" id="lblEAssessment_str5">
            Please tick the following box to confirm your consent to the collection of this video and audio during your examinations
        </span>

        <asp:CheckBox runat="server" ID="chkRule2" />
    </p>

    <div class="spacer"></div>
    <div class="spacer"></div>
    --%>
    <p>
        <asp:CheckBox runat="server" ID="chkRule4" />
        <label for="chkRule4" runat="server" id="lblEAssessment_str10" class="bold">
          Please confirm that [Phone] is the correct mobile number to reach you at.
        </label>
    </p>
    <hr />

    <p runat="server" id="lblEAssessment_str6">
        In order to access your examinations you will need to provide the Proctor with a Goverment Issued ID in the same name as you
        have registered with. You can view a video on the e-assessment process and learn more about that <a href="#" target="_blank">here</a>.
    </p>
    <p runat="server" id="lblEAssessment_str7">
        You have registered with Chartered Accountants Ireland with the following name: [Display Name]
    </p>
    <p>
        <asp:CheckBox runat="server" ID="chkRule3" />
        <label for="chkRule3" runat="server" id="lblEAssessment_str8" class="bold">
            Please confirm that [Display Name] is the same name as appears on your Goverment issued Identification documents
        </label>
    </p>
    <p style="float: right">
        <span id="lblError_str9" runat="server" visible="false" style="padding-right: 20px; color: #8C1D40;">You must check all three checkboxes to proceed</span>
        <asp:Button runat="server" Text="Done" ID="btnDone" OnClick="btnDone_Click"  CssClass="cai-btn cai-btn-red" />
    </p>

    <p style="float: left">
        <asp:Button runat="server" Text="Skip" ID="btnSkip" OnClick="btnSkip_Click"  CssClass="cai-btn cai-btn-navy" />
    </p>
</div>