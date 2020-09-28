using System.Web;

namespace SitefinityWebApp.BusinessFacade.Resources
{
    public static class EAssessmentResource
    {
        public static string ProctoruApiUrl
        {
            get
            {
                //return "https://demo.proctoru.com/api/autoLogin";
                return "https://api.proctoru.com/api/autoLogin";
            }
        }

        public static string CirrusApiUrl
        {
            get
            {
                return "https://api.cirrusplatform.com/api/v1/integrations/schedule?type=json";
            }
        }

        public static string CirrusApiHost
        {
            get
            {
                return "cirrusplatform.com:443";
            }
        }

        public static string CirrusErrorMessageAlreadyExist
        {
            get
            {
                return "<p class='pctu-error'>We cannot generate another link exam for you, contact <a href='mailto:exams@charteredaccountants.ie'>exams@charteredaccountants.ie</a> and send your exam code {0}.</p>";
            }
        }

        public static string CirrusDefaultErrorMessage
        {
            get
            {
                return "<p class='pctu-error'>Oops, error '{0}', please contact <a href='mailto:exams@charteredaccountants.ie'>exams@charteredaccountants.ie</a>.</p>";
            }
        }

        public static string CirrusNotFoundMessage
        {
            get
            {
                return "<p class='pctu-error'>Oops, error '{0}', please contact <a href='mailto:exams@charteredaccountants.ie'>exams@charteredaccountants.ie</a>.</p>";
            }
        }

        public static string ProctoruAuthorizationToken
        {
            get
            {
                //return "b9714301-5627-4f05-ad02-90b146b0a88a";
                return "2a7de735-7255-41d6-b549-e2a8a69aed8b";
            }
        }

        public static string CirrusAuthorizationToken
        {
            get
            {
                //return "EAPIHrtBfOqeJX4qbbga5G2gB26dcULQCHff76lhzTkHWTgyjCmz";
                return "EAPIJCqHsnWW1Z2adfgzTUd2Q583YcZ4ABSAXY0WPhff5v5y17ZX";
            }
        }

        public static string PostString
        {
            get
            {
                return "time_sent={0}-{1}-{2}T{3}:{4}:01Z&student_id={5}&email={6}&first_name={7}&last_name={8}&time_zone_id=GMT";
            }
        }

        public static string ErrorMessageStale
        {
            get
            {
                return "<p class='pctu-error'>This link has expired. Please refresh the page to generate a new link.</p>";
            }
        }

        public static string ErrorMessageNotFound
        {
            get
            {
                return "<p class='pctu-error'>There appears to be a problem with your account. Check back later or contact <a href='mailto:exams@charteredaccountants.ie'>exams@charteredaccountants.ie</a></p>";
            }
        }

        public static string ErrorMessageDefault
        {
            get
            {
                return "<p class='pctu-error'>There was a problem with the connection. Please refresh the page to generate a new link, if the problem persists, contact <a href='mailto:exams@charteredaccountants.ie'>exams@charteredaccountants.ie</a>.</p>";
            }
        }

        public static string SuccessMessageDefault
        {
            get
            {
                return "<a href='{0}' target='_blank' class='redirectProcturUlink'> {1} </a>";
            }
        }
    }
}