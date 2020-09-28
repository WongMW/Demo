namespace SitefinityWebApp.BusinessFacade.Resources
{
    public static class LoginResource
    {
        public static string MultiLoginMessage
        {
            get
            {
                return "Select from the drop down below which user profile you wish to use.";
            }
        }

        public static string WelcomeMessage
        {
            get
            {
                return "Welcome {0}";
            }
        }

        public static string WelcomeMessageContinue
        {
            get
            {
                return ", you are logged in as <b>{0}</b>.";
            }
        }
        
        public static string GerenicWrongUserOrPasswordMessage
        {
            get
            {
                return "That combination of user ID and password are not recognised. Please try again or try to reset your password.";
            }
        }


        public static string GerenicLoginErrorMessage
        {
            get
            {
                return "Error logging in";
            }
        }
    }
}