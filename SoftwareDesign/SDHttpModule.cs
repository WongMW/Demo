using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using Aptify.Framework.DataServices;

namespace SoftwareDesign
{
    public class SDHttpModule : IHttpModule, IRequiresSessionState
    {
        private HttpSessionState Session1;
        private HttpContext context;
        private HttpApplication application;
        public SDHttpModule()
        {
        }

        public string ModuleName
        {
            get { return "SDHttpModule"; }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            application = (HttpApplication)source;
            context = application.Context;

            var path = context.Request.Path;
            var pathParts = path.Split('/');
            var roleKey = "security:" + pathParts[1].ToLower();
            var vals = WebConfigurationManager.AppSettings[roleKey];

            if (vals.IsNullOrEmpty()) return;


            var acceptedRoles = vals.Split(',');
            var cookies = context.Request.Cookies;
            var cookie = cookies.Get("UserID");
            var userId = cookie != null ? cookie.Value : null;

            if (!userId.IsNullOrEmpty())
            {
                StringBuilder sqlCheckUser = new StringBuilder();
                sqlCheckUser.AppendFormat("EXEC [dbo].[spGetWebGroupForUser__cai] @WebUserId = {0}", userId);
                DataAction action = new DataAction();
                DataTable userResult = action.GetDataTable(sqlCheckUser.ToString());

                bool hasRole = false;
                if (userResult.Rows.Count > 0)
                {
                    for (var i = 0; i < userResult.Rows.Count; i++)
                    {
                        string userRole = userResult.Rows[i][0].ToString();

                        foreach (string role in acceptedRoles)
                        {
                            if (role.ToLower().Equals(userRole.ToLower()))
                            {
                                hasRole = true;
                                break;
                            }
                        }

                        if (hasRole)
                        {
                            break;
                        }
                    }
                }

                if (!hasRole)
                {
                    context.Response.Redirect(WebConfigurationManager.AppSettings["unauthorizedPage"]);
                }
                else
                {
                    LogUserLoggedInEvent(path, userId/*, GetUserName(userId)*/);
                }
            }
            else
            {
                HttpCookie c = new HttpCookie("ReturnToPage");
                c.Expires = DateTime.Now.AddMinutes(15);
                c.Value = context.Request.Url.ToString();
                context.Response.SetCookie(c);
                context.Response.Redirect(WebConfigurationManager.AppSettings["loginPage"]);
            }
        }

        /*private static string GetUserName(string userId)
        {
            var sqlCheckUser = new StringBuilder();
            sqlCheckUser.AppendFormat("EXEC [dbo].[spGetEBusinessWebUserDetails__SD] @WebUserId = {0}", userId);
            var action = new DataAction();
            var userResult = action.GetDataTable(sqlCheckUser.ToString());
            if (userResult.Rows.Count == 0) return string.Empty;
            var firstName = userResult.Rows[0]["FirstName"]?.ToString().Trim();
            var lastName = userResult.Rows[0]["LastName"]?.ToString().Trim();
            var webUserId = userResult.Rows[0]["WebUserStringID"]?.ToString().Trim();

            return string.Format("{0} {1}({2})", firstName, lastName, webUserId);


        }*/

        private void LogUserLoggedInEvent(string urlPath, string userId/*, string username*/)
        {
            var logDir = WebConfigurationManager.AppSettings["chariot.LogFolder"];
            if (string.IsNullOrEmpty(logDir)) return;
            if (!Directory.Exists(logDir)) return;
            var fileName = string.Format("{0:yyyyMMdd}.log", DateTime.Today);
            var filePath = Path.Combine(logDir, fileName);

            using (var streamWriter = File.AppendText(filePath))
            {
                //streamWriter.WriteLine("User: {0}/{1} accessed {2}", userId, username, urlPath);
                streamWriter.WriteLine("User: {0} accessed {1}", userId, urlPath);
            }
        }
    }
}
