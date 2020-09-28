using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Data;
using SitefinityWebApp.BusinessFacade.Interfaces.Html;

namespace SitefinityWebApp.BusinessFacade.Services.Html
{
    public class ComboBoxService : ComboBoxInterface
    {
        public async Task<DropDownList> PopulateComboList(DataTable dT, DropDownList cmdRefered, string selectedByText, string orderByAttr = null, string orderByAsc = "asc")
        {
            if (orderByAttr != null)
            {
                //dT.DefaultView.Sort = "WebUserID asc";
                dT.DefaultView.Sort = string.Format("{0} {1}", orderByAttr, orderByAsc);
            }

            cmdRefered.DataSource = dT;
            cmdRefered.DataTextField = "WebUserID";
            cmdRefered.DataValueField = "ID";
            cmdRefered.DataBind();
            cmdRefered.SelectedValue = cmdRefered.Items.FindByText(selectedByText)?.Value?.ToString();

            return cmdRefered;
        }
    }
}