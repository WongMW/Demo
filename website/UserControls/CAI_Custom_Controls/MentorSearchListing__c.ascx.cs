using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_CAI_Custom_Controls_MentorSearchListing__c : System.Web.UI.UserControl
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        var scriptManager = ScriptManager.GetCurrent(Page);

        if (scriptManager == null) return;

        scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/responsive-calendar.min.js" });
        scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/jquery.steps.min.js" });
        scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/sweetalert.min.js" });
        scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/jquery.pagination.min.js" });
        scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/select2.full.min.js" });
        scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/ammap.js" });
        scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/irelandHigh.js" });
        scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/bootstrap.min.js" });
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}