using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.EAssessment
{
    public partial class ModalControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            modalContent.Modal = RadWindow1;

            if (!Page.IsBackend() && !Page.IsPostBack)
            {
                ShowHideModal();
            }
        }

        private Boolean CanModalBeShown()
        {
            // checking if not logged in, then modal should not be shown
            if(User1.PersonID <= 0)
            {
                return false;
            }

            // lets check if previously skipped and recorded in the session
            if (Request.QueryString["showConsent"] == null &&
                (Page.Session[modalContent.SkipSessionKey] != null && Page.Session[modalContent.SkipSessionKey].ToString().Equals("1")))
            {
                return false;
            }

            // lets get existing consent, and check if already agreed
            var consent = modalContent.GetConsent();

            // checking if not submitted or skipped before
            if(consent == null)
            {
                return true;
            }

            // checking if all three consents are agreed
            if(Boolean.Parse(consent["AcceptRulesRegs"].ToString()) 
                && Boolean.Parse(consent["ConfirmNames"].ToString())
                && Boolean.Parse(consent["ConfirmPhones"].ToString())
                /*&& Boolean.Parse(consent["ConsentRecording"].ToString())*/)
            {
                return false;
            }

            return true;
        }

        private void ShowHideModal()
        {
            var toBeShown = CanModalBeShown();

            RadWindow1.VisibleOnPageLoad = toBeShown;
        }
    }
}