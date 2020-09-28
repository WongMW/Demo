namespace SoftwareDesign.BusinessFacade.Resources
{
    public static class MoodleResource
    {
        public static string GetEducationTokenUrl
        {
            get
            {
                return "http://localhost:50311/api/Auth/TokenEdu";
            }
        }

        public static string GetCpdTokenUrl
        {
            get
            {
                return "http://localhost:50311/api/Auth/TokenCpd";
            }
        }
        public static string CpdMoodleUrl
        {
            get
            {
                return "https://moodle.charteredaccountants.ie/";
            }
        }

        public static string EducationMoodleUrl
        {
            get
            {
                return "https://ed-test.charteredaccountants.ie/";
            }
        }
    }
}
