using System;
using Aptify.Framework.Web.eBusiness;
using SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.Designer;
using SoftwareDesign.GDPR;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR
{
    [ControlDesigner(typeof(GDPRModalDesigner))]
    public partial class GdprModal : BaseUserControlAdvanced
    {

        public int MonthsToHideAfterApproval { get; set; } = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowHideModal();
                GDPRWizard1.MonthsToHideAfterApproval = MonthsToHideAfterApproval;
            }
            RadWindow1.Title = GetCultureLocalString("SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.GdprModal.Title");
        }

        private string GetCultureLocalString(string key)
        {
            return Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", key)),
            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials
            );
        }

        private void ShowHideModal()
        {
            RadWindow1.VisibleOnPageLoad = ShowModalWindow();
        }

        private bool ShowModalWindow()
        {
            // For the current logged in user check if 
            // it is needed to show window

            if (User1.PersonID <= 0)
                return false;

            return !WizardClosedOnThisSession() &&
                   UserLastConfirmationIsExpired();
        }

        private bool WizardClosedOnThisSession()
        {
            return new ClosedWizard()
                .WizardClosedOnThisSession(Request);
        }

        private bool UserLastConfirmationIsExpired()
        {
            var lastDate = new RegisterOption(AptifyApplication, User1.PersonID)
                .LastConfirmedDate();

            if (lastDate == null)
                return true;

            return lastDate <= DateTime.Today.AddMonths(-1 * MonthsToHideAfterApproval);
        }
    }
}
