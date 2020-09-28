using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.CharteredConnect
{
    public partial class ConnectFormAuthCheck : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        public static string SECURED_COOKIE_NAME = "CHARTERED_CONNECT_AUTH";
        /// <summary>
        /// Secured URL that will be used to protect page with this control
        /// </summary>
        public String SecuredURL { get; set; }

        public static Boolean CheckSecurity(HttpSessionState session)
        {
            var securityPassed = false;
            // lets verify if form has been submitted
            if (session[SECURED_COOKIE_NAME] != null)
            {
                // assumption is that coming from the form, we may improve later on, but for now, just verify that it is GUID
                Guid g = Guid.Empty;
                if (Guid.TryParse(session[SECURED_COOKIE_NAME].ToString(), out g) && !Guid.Empty.Equals(g))
                {
                    securityPassed = true;
                }
            }

            return securityPassed;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsBackend())
            {
                var securityPassed = CheckSecurity(Session);
                
                // unauthorized, lets redirect to secured URL
                if (!securityPassed && !String.IsNullOrEmpty(SecuredURL))
                {
                    Response.Redirect(SecuredURL);
                }
            }
        }
    }
}