using Aptify.Framework.Web.eBusiness;
using System.Threading.Tasks;
using System.Web.UI;

namespace SitefinityWebApp.BusinessFacade.Interfaces.Login
{
    public interface LoginInterface
    {
        Task<AptifyWebUserLogin> GetWebUserLogin1ByControl(Control control);
        Task<string> GetWebUserByPersonId(Control control);
        Task<string> CompleteWelcomeMessage(AptifyWebUserLogin WebUserLogin1, string welcomeMessage);
        Task<string> MakeWelcomeMessage(BaseUserControl bsUctl, AptifyWebUserLogin WebUserLogin1);
    }
}