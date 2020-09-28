using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_CAI_Custom_Controls_FirmDirectoryListItem__c : System.Web.UI.UserControl
{
    int itemID;

    protected void Page_Load(object sender, EventArgs e)
    {
        Uri myUri = Request.Url;
        string param1 = HttpUtility.ParseQueryString(myUri.Query).Get("param1");
    }

    public void SetData(string firmName, int id)
    {
        lblFirmName.Text = firmName;
        itemID = id;
    }
}