using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.EventCalandar
{
    public partial class EC : System.Web.UI.UserControl
    {



        protected void Page_PreRender(object sender, EventArgs e)
        {
            var scriptManager = ScriptManager.GetCurrent(Page);

            if (scriptManager == null) return;
            //scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/FullEventCalender/fullcalendar-3.1.0/fullcalendar.min.js" });
            //scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/FullEventCalender/fullcalendar-3.1.0/lib/moment.min.js" });
            // scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/bootstrap/3.3.7/js/bootstrap.min.js" });
          
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}