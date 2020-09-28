using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aptify.Framework.BusinessLogic.GenericEntity;
using Aptify.Framework.Application;
using Aptify.Framework.BusinessLogic;
using Aptify.Framework.DataServices;
using Aptify.Framework.AttributeManagement;
using System.Data;
using Telerik.Web.UI;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.RedirectLogin
{

   
    public partial class redirectlogin : System.Web.UI.UserControl
    {
       
    
        protected void Page_Load(object sender, EventArgs e)
        {

            string Loginpage = "/Login.aspx";

			if (User1.PersonID <= 0)
			{
				Session["ReturnToPage"] = Request.RawUrl;

				Response.Redirect(Loginpage);
            }
			
				{ }

        }
    }
}
