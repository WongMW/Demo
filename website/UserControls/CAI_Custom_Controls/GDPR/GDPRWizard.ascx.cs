using System;
using Aptify.Framework.Web.eBusiness;
using SoftwareDesign.GDPR;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR
{
    public partial class GdprWizard : BaseUserControlAdvanced
    {
        public int MonthsToHideAfterApproval;
        private const string ProfileUrl = "~/CustomerService/profile.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            var btnSave = findTopicCodeSaveButton();
            if (btnSave != null)
            {
                btnSave.Visible = false;
            }

            if(!UserProfile1.ConfirmButtonEnabled)
            {
                Confirm1.getConfirmButton().Enabled = false;
                String msg = System.Configuration.ConfigurationManager.AppSettings.Get("Gdpr.Message.Firm.Required");
                Confirm1.getConfirmLabel().Text = String.IsNullOrEmpty(msg) ? "Please update your firm details!" : msg;
            } else
            {
                Confirm1.getConfirmButton().Enabled = true;
                String msg = System.Configuration.ConfigurationManager.AppSettings.Get("Gdpr.Message.Details.Correct");
                Confirm1.getConfirmLabel().Text = String.IsNullOrEmpty(msg) ? "Are these details correct?" : msg;
            }

            if(!IsPostBack)
            {
                lbl_msg1.Visible = false;
                lbl_msg2.Visible = false;
                lbl_msg3.Visible = false;
                
                // depending on the status of the user GDPR, present correct message
                if(User1.PersonID > -1)
                {
                    p_message2.InnerText = GetCultureLocalString("SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.GdprWizard.p_message2");
                    p_message3.InnerText = GetCultureLocalString("SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.GdprWizard.p_message3");

                    RegisterOption opt = new RegisterOption(AptifyApplication, User1.PersonID);
                    var state = opt.GetLastSelectionGDPRState;
                    if (state.HasValue && state == TopicCodeOption.Confirmed)
                    {
                        // check if expired
                        var lastDate = opt.LastConfirmedDate();

                        if (lastDate != null)
                        {
                            // check if not expired
                            if (lastDate <= DateTime.Today.AddMonths(-1 * MonthsToHideAfterApproval))
                            {
                                // show message 3
                                lbl_msg3.Visible = true;
                            }
                        }
                    }
                    else if (state.HasValue && state == TopicCodeOption.Now)
                    {
                        lbl_msg2.Visible = true;
                    }

                    // checking if none of the messages shown, show message 1
                    if (!lbl_msg2.Visible && !lbl_msg3.Visible)
                    {
                        lbl_msg1.Visible = true;
                    }
                }
            }
        }

        protected void Wizard1_CancelButtonClick(object sender, EventArgs e)
        {
            new ClosedWizard()
                .RecordWizardIsClosed(Response);

            ReloadPage();
        }

        private void ReloadPage()
        {
            Response.Redirect(Request.Url.AbsolutePath, true);
        }

        protected void Confirm1_Confirm(object sender, EventArgs e)
        {
            new ClosedWizard()
                .RecordWizardIsClosed(Response);

            new RegisterOption(AptifyApplication, User1.PersonID)
                .SaveOption(TopicCodeOption.Confirmed);

            ReloadPage();
        }

        protected void Confirm1_UpdateLater(object sender, EventArgs e)
        {
            new ClosedWizard()
                .RecordWizardIsClosed(Response);

            new RegisterOption(AptifyApplication, User1.PersonID)
                .SaveOption(TopicCodeOption.Later);

            ReloadPage();
        }

        protected void Confirm1_UpdateNow(object sender, EventArgs e)
        {
            new ClosedWizard()
                .RecordWizardIsClosed(Response);

            new RegisterOption(AptifyApplication, User1.PersonID)
                .SaveOption(TopicCodeOption.Now);

            RedirectToEditProfile();
        }

        private void RedirectToEditProfile()
        {
            Response.Redirect(ProfileUrl, true);
        }

        private System.Web.UI.WebControls.Button findTopicCodeSaveButton()
        {
            // lets save topic code preferences
            var cntrl = (System.Web.UI.UserControl)this.Step1.FindControl("UserPreferences1");
            if (cntrl != null)
            {
                var cntrl2 = (System.Web.UI.UserControl)cntrl.FindControl("TopicCodeControl1");
                if (cntrl2 != null)
                {
                    var cntrl3 = (System.Web.UI.UserControl)cntrl2.FindControl("TopicCodeViewer");
                    if (cntrl3 != null)
                    {
                        var btnSave = (System.Web.UI.WebControls.Button)cntrl3.FindControl("cmdSave");
                        return btnSave;
                    }
                }
            }

            return null;
        }

        protected void Wizard1_NextButtonClick(object sender, System.Web.UI.WebControls.WizardNavigationEventArgs e)
        {
            if(e.CurrentStepIndex == 1)
            {
                // lets save topic code preferences
                var btnSave = findTopicCodeSaveButton();
                if (btnSave != null)
                {
                    System.Reflection.MethodInfo clickMethodInfo = typeof(System.Web.UI.WebControls.Button).GetMethod("OnClick", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                    clickMethodInfo.Invoke(btnSave, new object[] { EventArgs.Empty });
                }
            }
        }

        protected void btnBackToUserPreferences_Click(object sender, EventArgs e)
        {
            Wizard1.MoveTo(Step1);
        }

        private string GetCultureLocalString(string key)
        {
            return Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", key)),
            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials
            );
        }
    }

    
}
