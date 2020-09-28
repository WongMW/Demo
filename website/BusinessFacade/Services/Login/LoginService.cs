using Aptify.Framework.Web.eBusiness;
using SitefinityWebApp.BusinessFacade.Interfaces.Login;
using SitefinityWebApp.BusinessFacade.Resources;
using System.Threading.Tasks;
using System.Web.UI;
using System.Data;
using Aptify.Framework.DataServices;

namespace SitefinityWebApp.BusinessFacade.Services.Login
{
    public class LoginService : LoginInterface
    {
        public async Task<AptifyWebUserLogin> GetWebUserLogin1ByControl(Control control)
        {
            return (AptifyWebUserLogin)control.FindControl("WebUserLogin1");
        }

        public async Task<string> GetWebUserByPersonId(Control control)
        {
            string tempWUL = GetWebUserLogin1ByControl(control).Result.User.PersonID.ToString();

            return string.Format(SqlResource.GetWebUserIDByLinkID, tempWUL);
        }

        public async Task<string> CompleteWelcomeMessage(AptifyWebUserLogin WebUserLogin1, string welcomeMessage)
        {
            if (!string.IsNullOrEmpty(WebUserLogin1?.User?.WebUserStringID))
            {
                if (WebUserLogin1.User.WebUserStringID.ToUpper().Contains("_FA"))
                {
                    welcomeMessage += string.Format(LoginResource.WelcomeMessageContinue, "Firm Admin");
                }
                else if (WebUserLogin1.User.WebUserStringID.ToUpper().Contains("TP"))
                {
                    welcomeMessage += string.Format(LoginResource.WelcomeMessageContinue, "Training Partner");
                }
                else
                {
                    welcomeMessage += string.Format(LoginResource.WelcomeMessageContinue, WebUserLogin1.User.WebUserStringID);
                }
            }

            return welcomeMessage;
        }

        public async Task<string> MakeWelcomeMessage(BaseUserControl bsUctl, AptifyWebUserLogin WebUserLogin1)
        {
            string ssql = string.Empty;
            ssql = string.Format(SqlResource.GetPersonDetailsById, bsUctl.Database, WebUserLogin1.User.PersonID);

            DataTable dt = bsUctl.DataAction.GetDataTable(ssql, IAptifyDataAction.DSLCacheSetting.BypassCache);
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(dr["PreferredSalutation"].ToString()))
                        {
                            return string.Format(LoginResource.WelcomeMessage, string.Format("{0} {1}", dr["FirstName"].ToString(), dr["LastName"].ToString()));
                        }
                        else
                        {
                            return string.Format(LoginResource.WelcomeMessage, string.Format("{0} {1}", dr["PreferredSalutation"].ToString(), dr["LastName"].ToString()));
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        return string.Format(LoginResource.WelcomeMessage, string.Format("{0} {1}", dr["FirstName"].ToString(), dr["LastName"].ToString()));
                    }
                }
            }

            return "Welcome Member";
        }
    }
}