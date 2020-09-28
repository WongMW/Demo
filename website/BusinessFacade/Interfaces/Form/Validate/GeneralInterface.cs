using SitefinityWebApp.BusinessFacade.Resources.Enum;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace SitefinityWebApp.BusinessFacade.Interfaces.Form.Validate
{
    public interface GeneralInterface
    {
        Task<HtmlGenericControl> MarkAsError(HtmlGenericControl el, bool hasError);

        //#ADD = 1 REMOVE = 0
        Task<string> BuildResultMessage(bool addOrRemove, string errorMessage, string fieldName, GeneralEnum.ValidateMessages typeOfMessage, string htmlText = "");

        Task<string> FieldIsValid(string fieldName, string text, GeneralEnum.ValidateRegex typeOfRegex, GeneralEnum.ValidateMessages typeOfMessage, string errorMessage, HtmlGenericControl el, bool isNullable = false, string htmlText = "", int sMin = 0, int sMax = 0, int rMin = 0, int rMax = 0);
    }
}