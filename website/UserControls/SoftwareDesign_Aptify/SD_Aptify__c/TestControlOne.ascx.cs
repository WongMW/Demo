using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aptify.Framework.Web.eBusiness;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c
{
    public partial class TestControlOne : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        public int MaxLoginTries
        {
            get { return 3; }
        }

        Button btnTest
        {
            get { return (Button) this.FindControl("btnTest"); }
        }

        AptifyWebUserLogin webUserLogin
        {
            get { return (AptifyWebUserLogin)this.FindControl("webUserControl1"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnTest_OnClick(object sender, EventArgs e)
        {
            var loggedIn = this.webUserLogin.Login("014509", "Test01", Page.User, MaxLoginTries);

            if (loggedIn)
            {
                btnTest.Text = "Logged In";
            }
            else
            {
                btnTest.Text = "NO Login";
            }
        }
    }
}