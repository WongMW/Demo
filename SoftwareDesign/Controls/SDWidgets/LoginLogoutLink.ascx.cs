using Aptify.Framework.Web.eBusiness;
using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;

namespace SoftwareDesign.Controls.SDWidgets
{
    public partial class LoginLogoutLink : BaseUserControlAdvanced
    {
        public String LoginURL { get; set; }
        public String ProfileURL { get; set; }

        private bool IsLoggedIn
        {
            get
            {
                return cntrlUser != null && cntrlUser.UserID > 0;
            }
        }

        private User User
        {
            get
            {
                return cntrlUser;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && PageLink != null)
            {
                if (String.IsNullOrEmpty(LoginURL))
                {
                    LoginURL = WebConfigurationManager.AppSettings["loginPage"];
                }

                if (LoginURL.StartsWith("/"))
                {
                    LoginURL = "~" + LoginURL;
                }

                if (!IsLoggedIn)
                {
                    PageLink.Text = "Login";
                    Response.Cookies.Remove("UserID");
                }
                else
                {
                    string userId = cntrlUser.UserID.ToString();
                    HttpCookie cookie = new HttpCookie("UserID");
                    int minutes = int.Parse(WebConfigurationManager.AppSettings["userIdCookieExpiryMins"]);
                    cookie.Expires = DateTime.Now.AddMinutes(minutes);
                    cookie.Value = userId;
                    Response.SetCookie(cookie);

                    string name = cntrlUser.FirstName;

                    if (name.Length > 7)
                    {
                        name = name.Substring(0, 7);
                        name += "..";
                    }
                    PageLink.Text = "<i class='fa fa-user'></i> " + name;
                }
            }
        }

        public void LinkButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(LoginURL))
            {
                LoginURL = WebConfigurationManager.AppSettings["loginPage"];
            }

            if (IsLoggedIn)
            {
                if (dropdownList.Visible == true)
                {
                    dropdownList.Visible = false;
                }
                else
                {
                    dropdownList.Visible = true;
                }                
            }
            else
            {
                Response.Redirect(this.ResolveUrl(LoginURL));
            }            
        }

        public void OpenProfile(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ProfileURL))
            {
                ProfileURL = WebConfigurationManager.AppSettings["profilePage"];
            }
            Response.Redirect(this.ResolveUrl(ProfileURL));
        }

        public void LogoutClick(object sender, EventArgs e)
        {
            if (IsLoggedIn)
            {
                doLogout();
                Response.Redirect(this.ResolveUrl(LoginURL));
            }
        }

        public void doLogout()
        {
            WebUserLogin.Logout();
            WebUserLogin.ClearAutoLoginCookie(Page.Response);

            Session.RemoveAll();
            Clearecatche();
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            ClearCookies();
        }

        private void ClearCookies()
        {
            HttpCookie aCookie;
            for (int i = 0; i < Request.Cookies.Count - 1; i++)
            {
                aCookie = Request.Cookies.Get(i);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.SetCookie(aCookie);
            }
        }

        private void Clearecatche()
        {
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Cache-control", "no-store, must-revalidate, private,no-cache");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.ClearHeaders();
            Response.AppendHeader("Cache-Control", "no-cache");
            Response.AppendHeader("Cache-Control", "private");
            Response.AppendHeader("Cache-Control", "no-store");
            Response.AppendHeader("Cache-Control", "must-revalidate");
            Response.AppendHeader("Cache-Control", "max-stale=0");
            Response.AppendHeader("Cache-Control", "post-check=0");
            Response.AppendHeader("Cache-Control", "pre-check=0");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Keep-Alive", "timeout=3, max=993");
            Response.AppendHeader("Expires", "0");

            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
        }

        public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("SoftwareDesign.Controls.SDWidgets.LoginLogoutLink.ascx");
    }
}