using SitefinityWebApp.BusinessFacade.Interfaces.General;

namespace SitefinityWebApp.BusinessFacade.Services.General
{
    public class GeneralService : GeneralInterface
    {
        public string CorrectionForLessThanTen(int number)
        {
            if (number < 10)
                return "0" + number;

            return number.ToString();
        }
    }
}