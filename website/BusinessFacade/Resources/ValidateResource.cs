using System.Text.RegularExpressions;

namespace SitefinityWebApp.BusinessFacade.Resources
{
    public static class ValidateResource
    {
        public static string ErrorInvalidMessage
        {
            get
            {
                return "The {0} field is not valid;{1}";
            }
        }

        public static string ErrorInvalidOrRequiredMessage
        {
            get
            {
                return "The {0} field is required or not valid;{1}";
            }
        }

        public static string ErrorOnlyRequiredMessage
        {
            get
            {
                return "The {0} field is required;{1}";
            }
        }

        //#######REGEXS

        public static Regex RegexWithoutSpace
        {
            get
            {
                return new Regex(@"^[a-zA-Z]*$");
            }
        }


        public static Regex RegexWithSpaceAndSpecialCharacters
        {
            get
            {
                return new Regex(@"^[a-zA-Z0-9 ?#!.\n]*$");
            }
        }


        public static Regex RegexNumbers
        {
            get
            {
                return new Regex(@"^[0-9]*$");
            }
        }


        public static Regex RegexEmail
        {
            get
            {
                return new Regex(@"^([\w\.\-\+]+)@([\w\-]+)((\.(\w){2,3})+)$");
            }
        }
    }
}