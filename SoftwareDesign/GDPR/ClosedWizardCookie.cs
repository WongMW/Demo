using System.Web;

namespace SoftwareDesign.GDPR
{
    /// <summary>
    /// Uses a http only session cookie to track
    /// whether wizard needs to be opened
    /// <remarks>
    /// We only want to open the radwindow when the user accesses the page for the first time.
    /// This cookie will remain in the browser for as long as the user is navigating on the same browser.
    /// Once the users closes the browser and opens it again the cookie will be missing.
    /// </remarks>
    /// </summary>
    public class ClosedWizard
    {
        private const string CookieName = "GDPRWizardClosed";
        public void RecordWizardIsClosed(HttpResponse response)
        {
            var cookie = new HttpCookie(CookieName){HttpOnly = true};
            response.Cookies.Add(cookie);
        }

        public bool WizardClosedOnThisSession(HttpRequest request)
        {
            return request.Cookies[CookieName] != null;
        }
    }
}