using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Data;

namespace SitefinityWebApp.BusinessFacade.Interfaces.Html
{
    public interface ComboBoxInterface
    {
        Task<DropDownList> PopulateComboList(DataTable dT, DropDownList cmdRefered, string selectedByText, string orderByAttr = null, string orderByAsc = "asc");
    }
}
