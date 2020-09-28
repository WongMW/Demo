using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;

namespace SitefinityWebApp.UpdateNews
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClaimsManager.GetCurrentIdentity() == null || !ClaimsManager.GetCurrentIdentity().IsAuthenticated || !ClaimsManager.GetCurrentIdentity().IsBackendUser)
            {
                Response.Redirect("/sitefinity");
                Response.End();
                return;
            }
        }
    }
}