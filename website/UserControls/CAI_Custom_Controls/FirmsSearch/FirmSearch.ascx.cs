using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using DocumentFormat.OpenXml.InkML;
using Newtonsoft.Json;


namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{
    public partial class FirmSearch : System.Web.UI.UserControl
    {

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var scriptManager = ScriptManager.GetCurrent(Page);

            if (scriptManager == null) return;

        //    scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/Datatables.net/1.10.12/jquery_3.1.0/jquery.min.js"});
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/Datatables.net/1.10.12/js/jquery.dataTables.min.js" });
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/Datatables.net/1.10.12/js/dataTables.searchHighlight.min.js" });
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/Datatables.net/1.10.12/js/jquery.highlight.js" });
            //bottstrap for collapse control
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/bootstrap/3.3.7/js/bootstrap.min.js" });
        }


        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // web method 


    }
}