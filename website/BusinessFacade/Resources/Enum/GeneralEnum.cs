namespace SitefinityWebApp.BusinessFacade.Resources.Enum
{
    public static class GeneralEnum
    {
        public enum ValidateMessages
        {
            ErrorInvalidMessage = 0,
            ErrorInvalidOrRequiredMessage = 1,
            ErrorOnlyRequiredMessage = 2
        }

        public enum ValidateRegex
        {
            RegexWithoutSpace = 0,
            RegexWithSpaceAndSpecialCharacters = 1,
            RegexNumbers = 2,
            RegexEmail = 3,
            RegexDontNeed = 4
        }

        public enum ValidatecirrusMessages
        {
            Ok = 0,
            CirrusError = 1,
            InternalError = 2,
            ExistWithouLink = 3,
            RegistrationNotFound = 4
        }
    }
}