using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR
{
    public partial class GDPRConfirm : System.Web.UI.UserControl
    {
        public event EventHandler Confirm;
        public event EventHandler UpdateNow;
        public event EventHandler UpdateLater;

        public Button getConfirmButton()
        {
            return btnConfirm;
        }
        public Label getConfirmLabel()
        {
            return lblMessage;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            OnConfirm();
        }

        protected void btnUpdateNow_Click(object sender, EventArgs e)
        {
            OnUpdateNow();
        }

        protected void btnUpdateLater_Click(object sender, EventArgs e)
        {
            OnUpdateLater();
        }

        protected virtual void OnConfirm()
        {
            Confirm?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUpdateNow()
        {
            UpdateNow?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUpdateLater()
        {
            UpdateLater?.Invoke(this, EventArgs.Empty);
        }

        
    }
}