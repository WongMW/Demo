<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GDPRWizard.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.GdprWizard" %>
<%@ Register TagPrefix="gdpr" TagName="UserPreferences" Src="~/UserControls/CAI_Custom_Controls/GDPR/UserTopicPreferences.ascx" %>
<%@ Register TagPrefix="gdpr" TagName="UserProfile" Src="~/UserControls/CAI_Custom_Controls/GDPR/UserProfile.ascx" %>
<%@ Register TagPrefix="gdpr" TagName="Confirm" Src="~/UserControls/CAI_Custom_Controls/GDPR/GDPRConfirm.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>



<asp:Wizard runat="server" ID="Wizard1" DisplaySideBar="False" DisplayCancelButton="True" CancelButtonText="Close"
    OnCancelButtonClick="Wizard1_CancelButtonClick" OnNextButtonClick="Wizard1_NextButtonClick" CssClass="GDPR-modal">
    <WizardSteps>
        <asp:WizardStep Title="Information" ID="Step0" StepType="Start">
            <div runat="server" id="lbl_msg1" class="GDPR-msg">
                <p style="margin-top: 20px;">
                    Your feedback through recent member research has informed us that you would like more tailored communications.
                    Did you know that you can control the relevance of content you get from the Institute?
                </p>
                <p style="margin-top: 20px;">
                    All you need to do is a <b>3 step</b> information update: 
                </p>

                <ul>
                    <li>Topics of interest</li>
                    <li>Industry sectors </li>
                    <li>Your basic information including job function and job title.</li>
                </ul>

                <p style="margin-top: 20px;">
                    <b>Doing this will only take a minute and will really help us give you the best membership experience possible.</b>
                </p>

                <p style="margin-top: 20px;">
                    <b>Don’t forget to press “confirm” when you’re finished which will store these until you next need to change.
                    </b>
                    You can update at any time by pressing the “preferences” tab in the sidebar menu, or “manage your online profile”.
               
                </p>
            </div>

            <div runat="server" id="lbl_msg2" class="GDPR-msg">
                <p runat="server" id="p_message2" style="margin-top: 20px;"></p>
            </div>

            <div runat="server" id="lbl_msg3" class="GDPR-msg">
                <p runat="server" id="p_message3" style="margin-top: 20px;"></p>
            </div>
        </asp:WizardStep>
        <asp:WizardStep Title="User Preferences" ID="Step1" StepType="Step">
            <gdpr:UserPreferences runat="server" ID="UserPreferences1" />
        </asp:WizardStep>
        <asp:WizardStep Title="User Profile" ID="Step2" StepType="Complete">
            <asp:Button runat="server" ID="btnBackToUserPreferences" CssClass="back-user-pref-btn" Text="&larr; Back to user preferences" OnClick="btnBackToUserPreferences_Click" />
            <gdpr:UserProfile runat="server" ID="UserProfile1" />
            <gdpr:Confirm runat="server" ID="Confirm1"
                OnConfirm="Confirm1_Confirm"
                OnUpdateLater="Confirm1_UpdateLater"
                OnUpdateNow="Confirm1_UpdateNow" />
        </asp:WizardStep>
    </WizardSteps>
    <StartNavigationTemplate>
        <div class="fixed-btn-position-outter">
            <div class="fixed-btn-position">
                <div class="actions">
                    <asp:Button ID="btnMoveNext" runat="server" CommandName="MoveNext" Text="Next" CssClass="cai-btn cai-btn-navy middle-btn" />
                </div>
            </div>
        </div>
    </StartNavigationTemplate>
    <StepNavigationTemplate>
        <div class="fixed-btn-position-outter">
            <div class="fixed-btn-position">
                <%--<div class="actions">
                    <asp:Button runat="server" CommandName="Cancel" Text="Close" CssClass="cai-btn cai-btn-navy-inverse side-by-side-btns" Width="100%" />
                </div> --%>
                <div class="actions">
                    <asp:Button ID="btnMoveNext" OnClientClick="javascript:btnMoveNextClientClick(event);" runat="server" CommandName="MoveNext" Text="Next" CssClass="cai-btn cai-btn-navy middle-btn" />
                </div>
                <script type="text/javascript">
                    function btnMoveNextClientClick(e) {
                        var nextTabOpened = false;
                        $(".trvTopcCodesRepeaterLink").each(function () {
                            if (!$(this).data('opened')) {
                                nextTabOpened = true;
                                $(this).click();
                            }
                        });

                        if (nextTabOpened) {
                            e.stopPropagation();
                            e.preventDefault();
                        }
                    }
                </script>
            </div>
        </div>
    </StepNavigationTemplate>

</asp:Wizard>
<cc1:User runat="server" ID="User1" />
<script>
    $('.middle-btn').parent().addClass("flexi-actions-btn");
    $('.flex-modal h1').each(function () {
        $(this).replaceWith(function () {
            return "<h2>" + $(this).html() + "</h2>";
        });
    });
</script>
