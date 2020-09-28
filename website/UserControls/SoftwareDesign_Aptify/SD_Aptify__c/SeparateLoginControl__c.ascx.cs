/* Created by : Harish Bangosavi
 * Date : 30/09/2019
 * Purpose : The toggle control for logging in as firm admin or training partner or member - make available to drop on other pages.
 */

using System;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Aptify.Framework.BusinessLogic.GenericEntity;
using Aptify.Framework.BusinessLogic.Security;
using Aptify.Framework.Web.eBusiness;
using Aptify.Framework.ExceptionManagement;
using SitefinityWebApp.BusinessFacade.Services.Login;
using SitefinityWebApp.BusinessFacade.Interfaces.Login;
using SitefinityWebApp.BusinessFacade.Interfaces.Html;
using SitefinityWebApp.BusinessFacade.Services.Html;
using SitefinityWebApp.BusinessFacade.Resources;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c
{
    public partial class SeparateLoginControl__c : BaseUserControlAdvanced
    {
        private readonly LoginInterface _login = new LoginService();
        private readonly ComboBoxInterface _combo = new ComboBoxService();

        protected const String ATTRIBUTE_CONTORL_DEFAULT_NAME = "SeparateLoginControl__c";
        protected const String ATTRIBUTE_MAXLOGINTRIES = "maxLoginTries";
        protected const String ATTRIBUTE_NUM_COLUMNS = "TimeForExpiry";
        protected string luserID = String.Empty;
        protected string lpassword = String.Empty;
        private int m_iUserId = 0;
        private int m_iTimeForExpiry;

        #region Properties

        Button cmdLogin = new Button();
        AptifyWebUserLogin WebUserLogin1
        {
            get { return (AptifyWebUserLogin)this.FindControl("WebUserLogin1"); }
        }
        Label lblWelcome
        {
            get { return (Label)this.FindControl("lblWelcome"); }
        }
        DropDownList cmdWebUser
        {
            get { return (DropDownList)FindControl("cmdWebUser"); }
        }
        Label lblMultiLogin
        {
            get { return (Label)this.FindControl("lblMultiLogin"); }
        }


        Button btnLogin
        {
            get { return (Button)this.FindControl("btnLogin"); }
        }

        AptifyShoppingCart ShoppingCartLogin
        {
            get { return (AptifyShoppingCart)this.FindControl("ShoppingCartLogin"); }
        }


        public event EventHandler UserLoggedIn;

        public int TimeForExpiry
        {
            get { return m_iTimeForExpiry; }
            set { m_iTimeForExpiry = value; }
        }

        public int WebUserID
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Uid"]))
                {
                    m_iUserId = int.Parse(Request.QueryString["Uid"].ToString());
                }

                return m_iUserId;
            }
            set { m_iUserId = value; }
        }

        public int MaxLoginTries
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(this.GetGlobalAttributeValue(ATTRIBUTE_MAXLOGINTRIES)))
                    {
                        int n;
                        bool isNumeric = int.TryParse(this.GetGlobalAttributeValue(ATTRIBUTE_MAXLOGINTRIES), out n);
                        if (!isNumeric)
                        {
                            throw new Exception("Incorrect entry for <Global>...<" + ATTRIBUTE_MAXLOGINTRIES +
                                                ">, a numeric value is required. " +
                                                "Please confirm the entry is correctly input in the 'Aptify_UC_Navigation.config' file.");
                        }
                        else
                        {
                            return (int)this.GetGlobalAttributeIntegerValue(ATTRIBUTE_MAXLOGINTRIES);
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return 0;
                }
            }
        }

        #endregion Properties


        protected override void SetProperties()
        {
            if (string.IsNullOrEmpty(this.ID))
            {
                this.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME;
            }
            base.SetProperties();
            if (TimeForExpiry == 0)
            {
                //since value is the 'default' check the XML file for possible custom setting
                if (!string.IsNullOrEmpty(this.GetPropertyValueFromXML(ATTRIBUTE_NUM_COLUMNS)))
                {
                    TimeForExpiry = int.Parse(this.GetPropertyValueFromXML(ATTRIBUTE_NUM_COLUMNS));
                }
            }
        }

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CleareCatche(); //Load WebUserID, set values for DropDownList.
            }
        }

        public void LoadWebUsers()
        {
            StringBuilder sqlCheckUser = new StringBuilder();
            sqlCheckUser.AppendFormat(_login.GetWebUserByPersonId(this).Result.ToString());

            var dT = DataAction.GetDataTable(sqlCheckUser.ToString());
            _combo.PopulateComboList(dT, cmdWebUser, WebUserLogin1.User.WebUserStringID, "WebUserID", "asc");

            if (cmdWebUser.Items.Count <= 1)
            {
                lblMultiLogin.Visible = false;
                cmdWebUser.Visible = false;
                btnLogin.Visible = false;
            }
            else
            {
                lblMultiLogin.Text = LoginResource.MultiLoginMessage;
                lblMultiLogin.Visible = true;
            }

            lblWelcome.Text = _login.CompleteWelcomeMessage(WebUserLogin1, lblWelcome.Text).Result;
        }

        private void CleareCatche()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D);
            Response.Expires = -1500;
        }

        private string DecryptPassword(string sPassword)
        {
            try
            {
                AptifySecurityKey oSecurityKey = new AptifySecurityKey(this.DataAction.UserCredentials);
                string password;
                password = oSecurityKey.DecryptData("E Business Login Key", sPassword.Replace(" ", "+"));
                return password;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return System.String.Empty;
            }
        }

        private void ClearCookies()
        {
            HttpCookie aCookie;
            int limit = Request.Cookies.Count - 1;
            for (int i = 0; i < limit; i++)
            {
                aCookie = Request.Cookies[i];
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(aCookie);
            }

            //Check If the cookies with name LOGIN exist on user's machine
            if (Request.Cookies["LOGIN"] != null)
            {
                //Expire the cookie
                Response.Cookies["LOGIN"].Expires = DateTime.Now.AddDays(-TimeForExpiry);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            try
            {
                base.OnPreRender(e);
                lblWelcome.Text = _login.MakeWelcomeMessage(this, WebUserLogin1).Result;
                LoadWebUsers();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Boolean bLoggedIn = false;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(SqlResource.GetWebUserDetailsByWebUserID, this.Database, cmdWebUser.SelectedItem.Value);
            System.Data.DataTable dataTable = this.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                AptifyGenericEntityBase webUser = this.AptifyApplication.GetEntityObject("Web Users", this.WebUserID);
                luserID = dataTable.Rows[0]["UserID"].ToString();
                lpassword = DecryptPassword(dataTable.Rows[0]["PWD"].ToString());
                Boolean chkAutoLogin = true;
                try
                {
                    if (WebUserLogin1.Logout())
                    {
                        HttpSessionState oSession = this.Session;
                        ShoppingCartLogin.Clear(ref oSession);
                        WebUserLogin1.ClearAutoLoginCookie(Page.Response);
                        Session.Clear();
                        Session.Abandon();
                        Session.Remove("ReturnToPage");
                        Request.Cookies.Remove("ReturnToPage");
                        ClearCookies();
                        Session["SocialNetwork"] = null;
                    }
                    string username = string.Empty;
                    string password = string.Empty;
                    if (!string.IsNullOrEmpty(luserID))
                    {
                        username = luserID;
                        password = lpassword;
                    }
                    // add MaxLoginTries from config file
                    if (WebUserLogin1.Login(username, password, Page.User, MaxLoginTries))
                    {
                        var wUser = WebUserLogin1.User;
                        //LoadWebUsers();
                        if (chkAutoLogin)
                        {
                            WebUserLogin1.AddAutoLoginCookie(Page.Response, username, password);
                        }
                        else
                        {
                            WebUserLogin1.ClearAutoLoginCookie(Page.Response);
                        }
                        wUser.SaveValuesToSessionObject(Page.Session);
                        bLoggedIn = true;
                    }
                    else
                    {
                        // Response.Redirect("~/Login.aspx");
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            if (bLoggedIn)
            {
                string sRedirectLocation = "";
                try
                {
                    EventHandler handler = UserLoggedIn;
                    Session["UserLoggedIn"] = true;
                    if (Session["ReturnToPage"] == null && Request.Cookies["ReturnToPage"] != null)
                    {
                        string sTemp = Request.Cookies["ReturnToPage"].Value;
                        Request.Cookies.Remove("ReturnToPage");
                        Session["ReturnToPage"] = sTemp;
                    }

                    if (Session["ReturnToPage"] != null)
                    {
                        string sTemp = (string)(Session["ReturnToPage"]);
                        sRedirectLocation = sTemp;
                        if (WebUserLogin1.User.PersonID > 0)
                        {
                            Session["ReturnToPage"] = "";
                            Session["OnURL"] = sTemp;
                            if (!sTemp.Contains("Eid"))
                            {
                                if (string.IsNullOrEmpty(luserID))
                                    Response.Redirect(Request.RawUrl);
                            }

                        }

                    }
                    else if (!string.IsNullOrWhiteSpace(Request.QueryString["returnurl"]))
                    {
                        string sTemp = Request.QueryString["returnurl"];
                        Session["OnURL"] = sTemp;
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        // refresh current page
                        sRedirectLocation = Request.RawUrl;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }

            }
            Response.Redirect(Request.RawUrl);
        }
        #endregion Methods
    }
}