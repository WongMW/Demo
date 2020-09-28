using System;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Aptify.Framework.BusinessLogic.GenericEntity;
using Aptify.Framework.BusinessLogic.Security;
using Aptify.Framework.Web.eBusiness;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using Aptify.Framework.ExceptionManagement;
using System.Configuration;
using SitefinityWebApp.BusinessFacade.Interfaces.Login;
using SitefinityWebApp.BusinessFacade.Services.Login;
using SitefinityWebApp.BusinessFacade.Interfaces.Html;
using SitefinityWebApp.BusinessFacade.Services.Html;
using Aptify.Framework.DataServices;
using SitefinityWebApp.BusinessFacade.Resources;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c
{
    public partial class SDLoginSF : BaseUserControlAdvanced
    {
        private readonly LoginInterface _login = new LoginService();
        private readonly ComboBoxInterface _combo = new ComboBoxService();

        protected const String ATTRIBUTE_NEWUSER_PAGE = "NewUserPage";
        protected const String ATTRIBUTE_COURSE_ENROLLMENT_PAGE = "CourseEnrollment";
        protected const String ATTRIBUTE_MAXLOGINTRIES = "maxLoginTries";
        protected const String ATTRIBUTE_CONTORL_DEFAULT_NAME = "Login";
        protected const String ATTRIBUTE_HOME_CHANGEPWD = "ChangePassword";
        //Added BY Pradip
        protected const String ATTRIBUTE_CustDefault_PAGE = "CustDefault";
        protected const String ATTRIBUTE_StudCenter_PAGE = "StudentCenter";
        //Suraj Issue 14861, 5/8/13, declare property for get a number of days from the nav file for cookies expire
        protected const String ATTRIBUTE_NUM_COLUMNS = "TimeForExpiry";

        //Milind Sutar - Login on behalf
        private int m_iUserId = 0;
        private String m_sEmployeeId = String.Empty;
        private String m_sToken = String.Empty;
        private Boolean m_bIsOnBehalf = false;
        //Added as part of #20448
        protected string luserID = String.Empty;
        protected string lpassword = String.Empty;

        private int m_iTimeForExpiry;
        //Neha, Issue 14408,03/20/13,declare property for new WebUser 
        private Boolean CheckNewWebUser;

        private readonly SoftwareDesign.WEBAPI_PLUGINS.MoodlePlugin mclient = new SoftwareDesign.WEBAPI_PLUGINS.MoodlePlugin();

        AptifyWebUserLogin WebUserLogin1
        {
            get { return (AptifyWebUserLogin)this.FindControl("WebUserLogin1"); }
        }

        CheckBox chkAutoLogin
        {
            get { return (CheckBox)this.FindControl("chkAutoLogin"); }
        }

        LinkButton cmdNewUser
        {
            get { return (LinkButton)this.FindControl("cmdNewUser"); }
        }

        HtmlGenericControl ForgotDetails
        {
            get { return (HtmlGenericControl)this.FindControl("ForgotDetails"); }
        }

        TextBox txtUserID
        {
            get { return (TextBox)this.FindControl("txtUserID"); }
        }

        TextBox txtPassword
        {
            get { return (TextBox)this.FindControl("txtPassword"); }
        }

        Button cmdLogin
        {
            get { return (Button)this.FindControl("cmdLogin"); }
        }
        //Added as part of #20448
        Button btnLogin
        {
            get { return (Button)this.FindControl("btnLogin"); }
        }

        Button cmdLogOut
        {
            get { return (Button)this.FindControl("cmdLogOut"); }
        }

        HtmlGenericControl tblLogin
        {
            get { return (HtmlGenericControl)this.FindControl("tblLogin"); }
        }

        HtmlGenericControl tblWelcome
        {
            get { return (HtmlGenericControl)this.FindControl("tblWelcome"); }
        }

        // Added by Harish. Redmine #20968 Hide or show the link for the MentorDashboard
        HtmlGenericControl trMentorDashboard
        {
            get { return (HtmlGenericControl)this.FindControl("trMentorDashboard"); }
        }
        // End Code. by Harish. Redmine #20968 Hide or show the link for the MentorDashboard

        Literal litLoginLabel
        {
            get { return (Literal)this.FindControl("litLoginLabel"); }
        }

        Label lblWelcome
        {
            get { return (Label)this.FindControl("lblWelcome"); }
        }

        Label lblError
        {
            get { return (Label)this.FindControl("lblError"); }
        }

        AptifyShoppingCart ShoppingCartLogin
        {
            get { return (AptifyShoppingCart)this.FindControl("ShoppingCartLogin"); }
        }

        HiddenField clientOffSet
        {
            get { return (HiddenField)this.FindControl("clientOffSet"); }
        }

        RadWindow SecurityResetMsgModal
        {
            get { return (RadWindow)FindControl("SecurityResetMsgModal"); }
        }

        RadWindow EmailSentMsgModal
        {
            get { return (RadWindow)FindControl("EmailSentMsgModal"); }
        }

        HiddenField savedUserField
        {
            get { return (HiddenField)this.FindControl("savedUserField"); }
        }

        RadWindow ForgotUsernameModal
        {
            get { return (RadWindow)FindControl("ForgotUsernameModal"); }
        }

        RadWindow ForgotPasswordModal
        {
            get { return (RadWindow)FindControl("ForgotPasswordModal"); }
        }

        RadWindow MissingEmailMsgModal
        {
            get { return (RadWindow)FindControl("MissingEmailMsgModal"); }
        }

        LinkButton ForgotPassword
        {
            get { return (LinkButton)FindControl("ForgotPassword"); }
        }

        LinkButton ForgotUsername
        {
            get { return (LinkButton)FindControl("ForgotUsername"); }
        }

        TextBox ForgotUsernameEmail
        {
            get { return (TextBox)ForgotUsernameModal.ContentContainer.FindControl("ForgotUsernameEmail"); }
        }

        TextBox ForgotUsernameFirstName
        {
            get { return (TextBox)ForgotUsernameModal.ContentContainer.FindControl("ForgotUsernameFirstName"); }
        }

        TextBox ForgotUsernameLastName
        {
            get { return (TextBox)ForgotUsernameModal.ContentContainer.FindControl("ForgotUsernameLastName"); }
        }
        TextBox ForgotPasswordUsername
        {
            get { return (TextBox)ForgotPasswordModal.ContentContainer.FindControl("ForgotPasswordUsername"); }
        }


        Label emailField
        {
            get { return (Label)EmailSentMsgModal.ContentContainer.FindControl("emailField"); }
        }
        Label SmallPrint
        {
            get { return (Label)EmailSentMsgModal.ContentContainer.FindControl("SmallPrint"); }
        }

        //Sitefinity40SSO Sitefinity4xSSO1
        //{
        //    get { return (Sitefinity40SSO)this.FindControl("Sitefinity4xSSO1"); }
        //}
        //Added as part of #20448
        DropDownList cmdWebUser
        {
            get { return (DropDownList)FindControl("cmdWebUser"); }
        }
        //Label lblWebUser
        //{
        //    get { return (Label)this.FindControl("lblWebUser"); }
        //}
        Label lblMultiLogin
        {
            get { return (Label)this.FindControl("lblMultiLogin"); }
        }

        HtmlGenericControl MOODLELinkLi
        {
            get { return (HtmlGenericControl)this.FindControl("MOODLELinkLi"); }
        }

        HyperLink MOODLELinkA
        {
            get { return (HyperLink)this.FindControl("MOODLELinkA"); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //added comment by GOvind for testing
            SetProperties();
            Boolean CheckSessionValue;
            //Added by Harish Redmine #20968 - Mentor Dashboard (CA Diary) Link
            CheckDisplayMentorDashboardLink();
            //End Code. Added by Harish Redmine #20968 - Mentor Dashboard (CA Diary) Link
            if (!IsPostBack)
            {
                //Anil B for  Issue 13882 on 18-03-2013
                //Set Remember option for login
                if (Request.Browser.Cookies)
                {
                    //Check if the cookies with name LOGIN exist on user's machine
                    if (Request.Cookies["LOGIN"] != null && Request.Cookies["LOGIN"]["RememberMe"] != null)
                    {
                        chkAutoLogin.Checked = Convert.ToBoolean(Request.Cookies["LOGIN"]["RememberMe"]);
                    }
                }

                CleareCatche();

                string username = Request.QueryString["username"];
                if (username != null)
                {
                    txtUserID.Text = username;
                }
                //LoadWebUsers();
            }

            //redirecting to chngepassword.aspx  if session count = 0

            if (Session["CheckNewUser"] != null)
            {
                CheckSessionValue = Convert.ToBoolean(Session["CheckNewUser"]);
                if (CheckSessionValue)
                {
                    Session.Remove("CheckNewUser");
                    if (!(bool)Session["UserLoggedIn"])
                    {
                        savedUserField.Value = txtUserID.Text;
                        SecurityResetMsgModal.VisibleOnPageLoad = true;
                    }
                }
            }

            if (IsPostBack)
            {
                if (Request.Browser.Cookies)
                {
                    //Check if the cookie with name LOGIN exist on user's machine
                    //If (Request.Cookies("LOGIN") Is Nothing) Then
                    //Create a cookie with expiry of 30 days
                    Response.Cookies["LOGIN"].Expires = DateTime.Now.AddDays(30);
                    //Write username to the cookie
                    Response.Cookies["LOGIN"]["RememberMe"] = chkAutoLogin.Checked.ToString();
                }
            }
            //added by GM
            if (!IsPostBack)
            {
                LoginOnBehalf();
                LoadWebUsers();
            }

            ForgotPasswordModal.OpenerElementID = ForgotPassword.ClientID;
            ForgotUsernameModal.OpenerElementID = ForgotUsername.ClientID;

            if (Session["OnURL"] != null)
            {
                //Response.Redirect(Convert.ToString(Session["OnURL"]));
                try
                {
                    Response.Redirect(Convert.ToString(Session["OnURL"]), true);
                }
                catch (Exception ex)
                {
                    //sorry
                }
            }

        }



        // Added by Harish. Redmine #20968 Hide or show the link for the MentorDashboard
        private void CheckDisplayMentorDashboardLink()
        {
            try
            {
                String sSQL = Database + "..spCheckMentorDashboardLinkAccess__c @MentorID=" + WebUserLogin1.User.PersonID;
                long lID = Convert.ToInt32(DataAction.ExecuteScalar(sSQL));
                if (lID > 0)
                {
                    trMentorDashboard.Visible = true;
                }
                else
                {
                    trMentorDashboard.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        // End code.Added by Harish. Redmine #20968 Hide or show the link for the MentorDashboard

        /// <remarks>
        /// The UserID cookie is set here and in the LoginLogoutLink.ascx.cs and is used in SDHttpModule.cs
        /// </remarks>
        /// <remarks>
        /// https://stackoverflow.com/questions/2777105/why-response-redirect-causes-system-threading-threadabortexception
        /// </remarks>
        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            //Login to the Aptify Enterprise Web Architecture
            //Use the Aptify EWA .NET Login Component to do this
            //EDUARDO 21378 - BEGIN
            var bLoggedIn = false;
            var username = "";

            if (txtUserID != null && !string.IsNullOrEmpty(txtUserID.Text))
            {
                username = txtUserID.Text.Trim();
            }

            var password = "";

            if (txtPassword != null && !string.IsNullOrEmpty(txtPassword.Text))
            {
                password = txtPassword.Text.Trim();
            }

            //Added as part of #20448
            if (!string.IsNullOrEmpty(luserID) && !string.IsNullOrEmpty(lpassword))
            {
                username = luserID;
                password = lpassword;
            }
            //End of #20448
            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    Session["OffSetVal"] = clientOffSet.Value;
                    StringBuilder sql = new StringBuilder();
                    //'HP Issue#9078: add MaxLoginTries from config file
                    if (WebUserLogin1.Login(username, password, Page.User, MaxLoginTries))
                    {
                        var wUser = WebUserLogin1.User;
                        tblLogin.Visible = true;
                        litLoginLabel.Visible = true;
                        tblWelcome.Visible = true;
                        //lblWelcome.Text = "Welcome " + wUser.FirstName + " " + wUser.LastName + ", you are now logged in.";
                        lblError.Text = String.Empty;
                        //LoadWebUsers();
                        if (chkAutoLogin.Checked)
                        {
                            WebUserLogin1.AddAutoLoginCookie(Page.Response, username, password);
                        }
                        else
                        {
                            WebUserLogin1.ClearAutoLoginCookie(Page.Response);
                        }

                        // make sure to persist changes to user, since many
                        // applications will do a Response.Redirect after
                        //this event is fired
                        wUser.SaveValuesToSessionObject(Page.Session);
                        bLoggedIn = true;

                        ////string userId = wUser.UserID.ToString();
                        ////Response.Cache.SetCacheability(HttpCacheability.NoCache);// added by Govind M
                        ////HttpCookie cookie = new HttpCookie("UserID");
                        ////int minutes = int.Parse(WebConfigurationManager.AppSettings["userIdCookieExpiryMins"]);
                        ////cookie.Expires = DateTime.Now.AddMinutes(minutes);
                        ////cookie.Value = userId;
                        ////Response.SetCookie(cookie);
                    }
                    else
                    {
                        if (IsNewUser(txtUserID.Text.Trim()))
                        {
                            savedUserField.Value = username;
                            SecurityResetMsgModal.VisibleOnPageLoad = true;
                        }
                        else
                        {
                            if (WebUserLogin1.Disabled)
                            {
                                // SendAccountLockedEmail(WebUserLogin1.User.Email);

                                DataTable userResult = FindUserByUsername(txtUserID.Text.Trim());

                                if (userResult.Rows.Count > 0)
                                {
                                    var row = userResult.Rows[0];
                                    //  var email = row["Email"].ToString();
                                    SendAccountLockedEmail(row["Email"].ToString());
                                }
                                tblLogin.Visible = true;
                                litLoginLabel.Visible = true;
                                tblWelcome.Visible = false;

                                lblError.Text = LoginResource.GerenicWrongUserOrPasswordMessage;

                            }
                            else
                            {
                                tblLogin.Visible = true;
                                litLoginLabel.Visible = true;
                                tblWelcome.Visible = false;
                                lblError.Text = LoginResource.GerenicWrongUserOrPasswordMessage;

                            }
                        }


                    }
                }
                else
                {
                    tblLogin.Visible = true;
                    litLoginLabel.Visible = true;
                    tblWelcome.Visible = false;
                    lblError.Text = LoginResource.GerenicWrongUserOrPasswordMessage;
                }
                //EDUARDO 21378 - END
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            if (m_bIsOnBehalf)
            {
                Session["ReturnToPage"] = CourseEnrollment;
            }

            if (bLoggedIn)
            {
                //Code Added by Govind 1/10/2013
                ShowHideMenu();
                //Code End by Govind 1/10/2013

                string sRedirectLocation = "";

                bool returnToPageIsAdminOrderDetail = (Session["ReturnToPage"] != null) ?
                    Session["ReturnToPage"].ToString().Contains("AdminOrderDetail") : false;
                if (returnToPageIsAdminOrderDetail)
                {
                    Session["UserLoggedIn"] = true;
                    string url = Session["ReturnToPage"].ToString().Replace("~", "");
                    sRedirectLocation = url;
                    try
                    {
                        Response.Redirect(url, true);
                    }
                    catch (Exception ex)
                    {
                        //sorry
                    }
                }

                try
                {

                    EventHandler handler = UserLoggedIn;// line added by Govind M
                    Session["UserLoggedIn"] = true;

                    if (Session["ReturnToPage"] == null && Request.Cookies["ReturnToPage"] != null)
                    {
                        string sTemp = Request.Cookies["ReturnToPage"].Value;
                        Request.Cookies.Remove("ReturnToPage"); //used only once
                        Session["ReturnToPage"] = sTemp;
                    }

                    if (Session["ReturnToPage"] != null)
                    {
                        string sTemp = (string)(Session["ReturnToPage"]);
                        sRedirectLocation = sTemp;
                        if (WebUserLogin1.User.PersonID > 0)
                        {
                            Session["ReturnToPage"] = ""; // used only once
                            //Response.Redirect(sTemp, false);
                            Session["OnURL"] = sTemp;
                            //Added as part of #20589
                            if (!sTemp.Contains("Eid"))
                            {
                                if (string.IsNullOrEmpty(luserID))
                                {
                                    try
                                    {
                                        Response.Redirect(Request.RawUrl, false);
                                        return;
                                    }
                                    catch (Exception ex)
                                    {
                                        //sorry
                                    }
                                }
                            }

                        }

                        //Context.ApplicationInstance.CompleteRequest();
                    }
                    else if (!string.IsNullOrWhiteSpace(Request.QueryString["returnurl"]))
                    {
                        string sTemp = Request.QueryString["returnurl"];
                        Session["OnURL"] = sTemp;
                        try
                        {
                            Response.Redirect(Request.RawUrl, true);
                        }
                        catch (Exception ex)
                        {
                            //sorry
                        }
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

                if ((bool)Session["UserLoggedIn"] && CheckFirstLogin())
                {
                    CheckNewUser = true;
                    Session["CheckNewUser"] = true;
                }
                else
                {
                    CheckNewUser = false;
                }

                var sSql = Database + "..spGetPersonMemberTypeName__c @PersonID=" + WebUserLogin1.User.PersonID;
                string sStudent = Convert.ToString(DataAction.ExecuteScalar(sSql));

                if (Request.RawUrl.Contains("SecurityError.aspx"))
                {
                    if (sStudent.ToUpper() == "STUDENT")
                    {
                        try
                        {
                            Response.Redirect(StudentCenterPage, true);
                        }
                        catch (Exception ex)
                        {
                            //sorry
                        }
                    }
                    else
                    {
                        try
                        {
                            Response.Redirect("/Login.aspx", true);
                        }
                        catch (Exception ex)
                        {
                            //sorry
                        }
                    }
                }

                if (!m_bIsOnBehalf)
                {
                    if (Convert.ToString(Session["ReturnToPage"]).Trim() != "")
                    {
                        string url = Convert.ToString(Session["ReturnToPage"]).Trim();
                        Session["ReturnToPage"] = "";
                        try
                        {
                            Response.Redirect(url, true);
                        }
                        catch (Exception ex)
                        {
                            //sorry
                        }

                    }
                    else
                    {
                        try
                        {
                            Response.Redirect(Request.RawUrl, true);
                        }
                        catch (Exception ex)
                        {
                            //sorry
                        }
                    }
                }
                else
                {
                    // Session["ReturnToPage"] = ""; Commneted by Govind M For Log #18303

                }
            }
        }

        public void ShowErrorLogin()
        {
            tblLogin.Visible = true;
            litLoginLabel.Visible = true;
            tblWelcome.Visible = false;
            //HP Issue#9078: display message indicating disabled account
            if (WebUserLogin1.Disabled)
            {
                SendAccountLockedEmail(WebUserLogin1.User.Email);
            }

            lblError.Text = LoginResource.GerenicLoginErrorMessage;
        }

        private void SendAccountLockedEmail(string useremail)
        {
            try
            {
                var body = GetTemplate("AccountLockedEmail");
                string subject = "RE Chartered Accountants Ireland website disabled account.";

                //  SendEmail(subject, body, WebUserLogin1.User.Email);
                SendEmail(subject, body, useremail);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        // <summary>
        // Auto login - Get the Staff user id from query string 
        // </summary>
        public string Token
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["OT"]))
                {
                    m_sToken = Request.QueryString["OT"].ToString();
                }

                return m_sToken;
            }
            set { m_sToken = value; }
        }

        // <summary>
        // Auto login - Get the Staff user id from query string 
        // </summary>
        public string EmployeeID
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["EId"]))
                {
                    m_sEmployeeId = Request.QueryString["EId"].ToString();
                }

                return m_sEmployeeId;
            }
            set { m_sEmployeeId = value; }
        }

        // <summary>
        // Auto login - Gets user id from query string 
        // </summary>
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


        public string CustDefault
        {
            get
            {
                if (ViewState[ATTRIBUTE_CustDefault_PAGE] != null)
                {
                    return (string)ViewState[ATTRIBUTE_CustDefault_PAGE];
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState[ATTRIBUTE_CustDefault_PAGE] = this.FixLinkForVirtualPath(value); }
        }

        public string StudentCenterPage
        {
            get
            {
                if (ViewState[ATTRIBUTE_StudCenter_PAGE] != null)
                {
                    return (string)ViewState[ATTRIBUTE_StudCenter_PAGE];
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState[ATTRIBUTE_StudCenter_PAGE] = this.FixLinkForVirtualPath(value); }
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


        protected override void SetProperties()
        {
            if (string.IsNullOrEmpty(this.ID))
            {
                this.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME;
            }
            //call base method to set parent properties
            base.SetProperties();

            if (string.IsNullOrEmpty(NewUserPage))
            {
                //since value is the 'default' check the XML file for possible custom setting
                NewUserPage = this.GetLinkValueFromXML(ATTRIBUTE_NEWUSER_PAGE);
                if (string.IsNullOrEmpty(NewUserPage))
                {
                    this.cmdNewUser.Enabled = false;
                    this.cmdNewUser.ToolTip = "NewUserPage property has not been set.";
                }
            }

            if (string.IsNullOrEmpty(CustDefault))
            {
                //since value is the 'default' check the XML file for possible custom setting
                CustDefault = this.GetLinkValueFromXML(ATTRIBUTE_CustDefault_PAGE);
            }

            if (string.IsNullOrEmpty(StudentCenterPage))
            {
                //since value is the 'default' check the XML file for possible custom setting
                StudentCenterPage = this.GetLinkValueFromXML(ATTRIBUTE_StudCenter_PAGE);
            }

            if (string.IsNullOrEmpty(ChangePassword))
            {
                //since value is the 'default' check the XML file for possible custom setting
                ChangePassword = this.GetLinkValueFromXML(ATTRIBUTE_HOME_CHANGEPWD);
            }

            //Suraj Issue 14861, if TimeForExpiry value is 0 then take a value from nav file.
            if (TimeForExpiry == 0)
            {
                //since value is the 'default' check the XML file for possible custom setting
                if (!string.IsNullOrEmpty(this.GetPropertyValueFromXML(ATTRIBUTE_NUM_COLUMNS)))
                {
                    TimeForExpiry = int.Parse(this.GetPropertyValueFromXML(ATTRIBUTE_NUM_COLUMNS));
                }
            }

            if (string.IsNullOrEmpty(CourseEnrollment))
            {
                //since value is the 'default' check the XML file for possible custom setting
                CourseEnrollment = this.GetLinkValueFromXML(ATTRIBUTE_COURSE_ENROLLMENT_PAGE);
                if (string.IsNullOrEmpty(CourseEnrollment))
                {
                    this.cmdNewUser.ToolTip = "CourseEnrollment property has not been set.";
                }
            }
        }

        public int TimeForExpiry
        {
            get { return m_iTimeForExpiry; }
            set { m_iTimeForExpiry = value; }
        }

        public String ChangePassword
        {
            get
            {
                if (ViewState[ATTRIBUTE_HOME_CHANGEPWD] != null)
                {
                    return ViewState[ATTRIBUTE_HOME_CHANGEPWD].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }

            set { ViewState[ATTRIBUTE_HOME_CHANGEPWD] = this.FixLinkForVirtualPath(value); }
        }


        // <summary>
        // This event is raised whenever a user logs in
        // </summary>
        // <remarks></remarks>
        public event EventHandler UserLoggedIn;

        // <summary>
        // This event is raised whenever a user logs out
        // </summary>
        // <remarks></remarks>
        public event EventHandler UserLoggedOut;


        public string CourseEnrollment
        {
            get
            {
                if (ViewState[ATTRIBUTE_COURSE_ENROLLMENT_PAGE] != null)
                {
                    return (string)ViewState[ATTRIBUTE_COURSE_ENROLLMENT_PAGE];
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState[ATTRIBUTE_COURSE_ENROLLMENT_PAGE] = this.FixLinkForVirtualPath(value); }
        }

        // <summary>
        // NewUser page url
        // </summary>
        public string NewUserPage
        {
            get
            {
                if (ViewState[ATTRIBUTE_NEWUSER_PAGE] != null)
                {
                    return ViewState[ATTRIBUTE_NEWUSER_PAGE].ToString();
                }
                else
                {
                    return String.Empty;
                }
            }
            set { ViewState[ATTRIBUTE_NEWUSER_PAGE] = this.FixLinkForVirtualPath(value); }
        }


        //Neha, Issue 14408,03/20/13,set property for new webuser firstlogin 
        public Boolean CheckNewUser
        {
            get { return CheckNewWebUser; }
            set { CheckNewWebUser = value; }
        }

        // <summary>
        // MaxLoginTries
        // </summary>
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


        // <summary>
        // String to show in the title
        // </summary>
        // <value></value>
        // <returns></returns>
        // <remarks></remarks>
        //        <System.ComponentModel.DefaultValue("Login")> _
        public string TitleString
        {
            get
            {
                if (ViewState["TitleString"] != null)
                {
                    return (string)ViewState["TitleString"].ToString();
                }
                else
                {
                    return "Login";
                }
            }
            set { ViewState["TitleString"] = value; }
        }

        // <summary>
        // Defaults to true, shows the title of the control or not
        // </summary>
        // <value></value>
        // <returns></returns>
        // <remarks></remarks>
        public bool ShowTitle
        {
            get
            {
                if (ViewState["ShowTitle"] != null)
                {
                    return (bool)ViewState["ShowTitle"];
                }
                else
                {
                    return true;
                }
            }
            set { ViewState["ShowTitle"] = value; }
        }

        //RashmiP, Function clears cookies
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
                //Suraj Issue 14861, 5/8/13,  remove hard coded days from AddDays and add the dynamics days which is set from nav file
                //Expire the cookie
                Response.Cookies["LOGIN"].Expires = DateTime.Now.AddDays(-TimeForExpiry);
            }
        }

        // <summary>
        // To auto login through smart client
        // </summary>
        private void LoginOnBehalf()
        {
            try
            {
                if (this.WebUserID != 0)
                {
                    string userID;
                    string password;
                    string link;
                    string errorMessage = string.Empty;
                    string token;
                    Boolean result = false;
                    StringBuilder sql = new StringBuilder();

                    //Bug #20606

                    bool bLoggedOut = false;
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
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                    if (bLoggedOut)
                    {
                        if (Session != null)
                        {
                            Session["UserLoggedIn"] = false;
                        }
                    }
                    //End of #20606
                    sql.AppendFormat(SqlResource.GetWebUserDetailsByWebUserID, this.Database, this.WebUserID);
                    System.Data.DataTable dataTable = this.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        token = dataTable.Rows[0]["Token"].ToString();
                        if (token == this.Token)
                        {
                            //Clear security token - For one time URL access
                            AptifyGenericEntityBase webUser = this.AptifyApplication.GetEntityObject("Web Users", this.WebUserID);
                            webUser.SetValue("Token__c", String.Empty);
                            result = webUser.Save(ref errorMessage);

                            if (result)
                            {

                                userID = dataTable.Rows[0]["UserID"].ToString();
                                password = DecryptPassword(dataTable.Rows[0]["PWD"].ToString());
                                link = dataTable.Rows[0]["Link"].ToString();

                                //Set the corse enrollment url with employee id as query string
                                StringBuilder url = new StringBuilder();
                                url.AppendFormat("{0}?Eid={1}", link, this.EmployeeID);
                                this.CourseEnrollment = url.ToString();

                                m_bIsOnBehalf = true;
                                chkAutoLogin.Checked = true;

                                //Login explicitly by calling login button click
                                txtUserID.Text = userID;
                                txtPassword.Text = password;
                                this.cmdLogin_Click(this.cmdLogin, null);
                                //   this.resetPasswordBtn_Click(this.resetPasswordBtn, Nothing);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        // <summary>
        // Decrypt password
        // </summary>
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


        private void ShowHideMenu()
        {
            try
            {
                long lWebMenuID;
                AptifyGenericEntityBase oWebMenu;
                AptifyGenericEntityBase oWebUser;
                oWebUser = AptifyApplication.GetEntityObject("Web Users", WebUserLogin1.User.UserID);
                string sSqlWebMenu = AptifyApplication.GetEntityBaseDatabase("Web Menus") + "..spGetWebMenuID__c @Name='Membership Application'";
                lWebMenuID = Convert.ToInt32(DataAction.ExecuteScalar(sSqlWebMenu));

                string sWebGroupSQL = AptifyApplication.GetEntityBaseDatabase("Web Menus") + "..spGetWebGroup__c";
                long lWebGroupID = Convert.ToInt32(DataAction.ExecuteScalar(sWebGroupSQL));

                string sSQLPersonEligible = "..spGetPersonEligibleForMembership__c @PersonID=" + WebUserLogin1.User.PersonID;

                long sPersonID = Convert.ToInt32(DataAction.ExecuteScalar(sSQLPersonEligible));

                if (sPersonID > 0)
                {
                    if (!CheckAlreadyFillupMembershipApplication((int)WebUserLogin1.User.PersonID))
                    {
                        string sCheckAlreadyGroupAddedSql = AptifyApplication.GetEntityBaseDatabase("Web Menus") + "..spCheckAlreadyWebGroup__c @WebUserID=" + WebUserLogin1.User.UserID +
                            ",@WebGroupID=" + lWebGroupID;
                        long lWebAlreadyGrp = Convert.ToInt32(DataAction.ExecuteScalar(sCheckAlreadyGroupAddedSql));

                        if (lWebAlreadyGrp <= 0)
                        {
                            AptifyGenericEntityBase oWebusersGroup;
                            oWebusersGroup = AptifyApplication.GetEntityObject("WebUserGroups", -1);
                            oWebusersGroup.SetValue("WebUserID", WebUserLogin1.User.UserID);
                            oWebusersGroup.SetValue("WebGroupID", lWebGroupID);
                            oWebusersGroup.Save();
                        }
                    }
                    else
                    {
                        string sCheckAlreadyGroupAddedSql = AptifyApplication.GetEntityBaseDatabase("Web Menus") + "..spCheckAlreadyWebGroup__c @WebUserID=" + WebUserLogin1.User.UserID + ",@WebGroupID=" + lWebGroupID;
                        long lWebAlreadyGrp = Convert.ToInt32(DataAction.ExecuteScalar(sCheckAlreadyGroupAddedSql));
                        if (lWebAlreadyGrp > 0)
                        {
                            oWebUser.SubTypes["WebUserGroups"].Find("WebGroupID", lWebGroupID).Delete();
                            oWebUser.Save();
                        }
                    }

                }
                else
                {
                    string sCheckAlreadyGroupAddedSql = AptifyApplication.GetEntityBaseDatabase("Web Menus") + "..spCheckAlreadyWebGroup__c @WebUserID=" + WebUserLogin1.User.UserID + ",@WebGroupID=" + lWebGroupID;
                    long lWebAlreadyGrp = Convert.ToInt32(DataAction.ExecuteScalar(sCheckAlreadyGroupAddedSql));
                    if (lWebAlreadyGrp > 0)
                    {
                        oWebUser.SubTypes["WebUserGroups"].Find("WebGroupID", lWebGroupID).Delete();
                        oWebUser.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private Boolean CheckAlreadyFillupMembershipApplication(int PersonID)
        {
            try
            {
                var sSql = Database + "..spCheckAlradyFillupMembershipApp__c @PersonID=" + PersonID;
                string sDate = Convert.ToString(DataAction.ExecuteScalar(sSql));
                if (sDate != "" && sDate != null && sDate != "01/01/1900 00:00:00")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        private Boolean CheckFirstLogin()
        {
            try
            {
                string sSql = String.Empty;
                System.Data.DataTable dtChkFLogin = new System.Data.DataTable();
                bool bChkFLogin = false;
                sSql = "SELECT SessionCount FROM " + Database + ".." + AptifyApplication.GetEntityBaseView("Web Users") + " Where ID= " + WebUserLogin1.User.UserID;
                dtChkFLogin = DataAction.GetDataTable(sSql);
                if (dtChkFLogin != null && dtChkFLogin.Rows.Count > 0 && dtChkFLogin.Rows[0]["SessionCount"].ToString() == "1")
                {
                    bChkFLogin = true;
                }
                return bChkFLogin;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        protected void cmdLogOut_ServerClick(Object sender, System.EventArgs e)
        {
            bool bLoggedOut = false;
            try
            {
                if (WebUserLogin1.Logout())
                {
                    lblError.Text = "";
                    tblLogin.Visible = true;
                    litLoginLabel.Visible = true;
                    tblWelcome.Visible = false;
                    HttpSessionState oSession = this.Session;
                    ShoppingCartLogin.Clear(ref oSession);
                    WebUserLogin1.ClearAutoLoginCookie(Page.Response);
                    //HP Issue#9078: clear and delete session
                    Session.Clear();
                    Session.Abandon();
                    //Suraj Issue 15370,3/19/13 remove the application session after click on logout button
                    Session.Remove("ReturnToPage");
                    Request.Cookies.Remove("ReturnToPage");
                    bLoggedOut = true;
                    //RashmiP, Call Clear Cookies function
                    ClearCookies();
                    Session["SocialNetwork"] = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            if (bLoggedOut)
            {
                OnUserLoggedOut(e);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            try
            {
                base.OnPreRender(e);
                if (WebUserLogin1.User.UserID <= 0)
                {
                    tblLogin.Visible = true;
                    litLoginLabel.Visible = true;
                    tblWelcome.Visible = false;
                }
                else
                {
                    tblLogin.Visible = false;
                    litLoginLabel.Visible = false;
                    tblWelcome.Visible = true;

                    //Eduardo 06-11-2019
                    lblWelcome.Text = _login.MakeWelcomeMessage(this, WebUserLogin1).Result;

                    //lblWelcome.Text = "Welcome " + WebUserLogin1.User.FirstName + " " + WebUserLogin1.User.LastName + ", you are now logged in.";
                    WebUserLogin1.User.SaveValuesToSessionObject(Page.Session);

                    var jtwCheck = mclient.ConsumeJWTTokenEducation(WebUserLogin1.User.WebUserStringID);
                    if (!string.IsNullOrEmpty(jtwCheck))
                    {
                        MOODLELinkLi.Visible = true;
                        MOODLELinkA.NavigateUrl = jtwCheck;
                        MOODLELinkA.Target = "_blank";
                    }
                    else
                        MOODLELinkLi.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmdNewUser_Click(Object sender, EventArgs e)
        {
            Session["SocialNetwork"] = null;
            try
            {
                Response.Redirect(NewUserPage, true);
            }
            catch (Exception ex)
            {
                //sorry
            }
        }

        protected void OnUserLoggedOut(EventArgs e)
        {
            //UserLoggedOut(this, e);

            if (Session != null)
            {
                Session["UserLoggedIn"] = false;
            }

            try
            {
                Response.Redirect(Request.RawUrl, true);
            }
            catch (Exception ex)
            {
                //sorry
            }

        }

        protected void chkAutoLogin_CheckedChanged(Object sender, EventArgs e)
        {
            //'Anil B for  Issue 13882 on 18-03-2013
            //'Set Remember option for login
            // 'Modified by Dipali Story No:13882:-e-Business: Support Remember Me Option When Logging in Using LinkedIn
            //' lbl.Text = CStr(chkAutoLogin.Checked)
            //'Check if the browser support cookies
            if (Request.Browser.Cookies)
            {
                //'Check if the cookie with name LOGIN exist on user's machine
                //'If (Request.Cookies("LOGIN") Is Nothing) Then
                //'Create a cookie with expiry of 30 days
                Response.Cookies["LOGIN"].Expires = DateTime.Now.AddDays(30);
                //'Write username to the cookie
                Response.Cookies["LOGIN"]["RememberMe"] = chkAutoLogin.Checked.ToString();
            }
        }

        protected void confirmResetBtn_OnClick(object sender, EventArgs e)
        {
            //page load if token open modal
            //save firt time login if all successful
            EmailSentMsgModal.VisibleOnPageLoad = false;
        }

        protected void resetPasswordBtn_OnClick(object sender, EventArgs e)
        {
            DataTable userResult = FindUserByUsername(txtUserID.Text.Trim());

            if (userResult.Rows.Count > 0)
            {
                var row = userResult.Rows[0];
                var email = row["Email"].ToString();
                var userId = row["ID"].ToString();

                GenerateAndSendPasswordEmail(userId, email);
                SecurityResetMsgModal.VisibleOnPageLoad = false;
                EmailSentMsgModal.VisibleOnPageLoad = true;
                emailField.Text = "We have sent a password reset email to the email address we have on file for you, which is : <br> <b>" + HideEmailAddress(email) + "</b>";
                SmallPrint.Text = "If this email is incorrect please contact us at <br/>webmaster@charteredaccountants.ie <br />Tel: +353 1 637 7200 or +44 28 90435840";
            }
        }

        protected string HideEmailAddress(string email)
        {
            StringBuilder hiddenEmail = new StringBuilder(email);

            for (int i = 1; i < email.IndexOf('@'); i++)
            {
                hiddenEmail[i] = '*';
            }

            return hiddenEmail.ToString();
        }

        protected void GenerateAndSendPasswordEmail(string userId, string email)
        {
            DateTime expiry = DateTime.Today.AddHours(24);
            Guid token = Guid.NewGuid();

            //save to db
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("EXEC [Ebusiness].[dbo].[spCreateUserPasswordToken__cai]  @userId ='" + userId + "', @token ='" + token + "' , @expiry = '" + expiry.ToString("yyyy-dd-MM HH:mm:ss") + "'");
            DataAction.ExecuteNonQuery(sql.ToString());

            try
            {
                var link = "https://" + Request.Url.Host + GetGlobalAttributeValue("ResetPasswordPage") + "?token=" + token;
                var body = GetTemplate("ResetPasswordEmail");
                string amendedBody = body.Replace("{link}", link);
                string subject = "RE Chartered Accountants Ireland website password reset";

                SendEmail(subject, amendedBody, email);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        protected void GenerateAndSendUsernameEmail(string userId, string email, string username)
        {
            try
            {
                var login = GetGlobalAttributeValue("LoginPage");
                var link = "https://" + Request.Url.Host + "/" + login;

                var body = GetTemplate("ForgotUsernameEmail");
                body = body.Replace("{username}", username);
                body = body.Replace("{link}", link);
                string subject = "RE Chartered Accountants Ireland website username reminder";

                SendEmail(subject, body, email);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        protected void SendEmail(string subject, string body, string email)
        {
            try
            {
                //build the email message
                MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["MailFrom"], email);
                mail.BodyEncoding = Encoding.Default;
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Port = int.Parse(ConfigurationManager.AppSettings["MailPort"]);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = ConfigurationManager.AppSettings["UseDefaultCredentials"] == "true";
                if (client.UseDefaultCredentials)
                {
                    client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailUserName"], ConfigurationManager.AppSettings["MailPassword"]);
                    client.UseDefaultCredentials = true;
                }
                client.Host = ConfigurationManager.AppSettings["MailServer"];
                client.EnableSsl = ConfigurationManager.AppSettings["Mail.EnableSsl"] == "true";
                client.Send(mail);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        protected string GetTemplate(string template)
        {
            var resource = "SitefinityWebApp.SDEmailTemplates." + template + ".html";
            string data = "";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(
                        "Cannot get the email template, as the file is missing. This file should be marked as 'Embedded Resource' and be located at '{resource}'."
                    );
                }

                using (var reader = new StreamReader(stream))
                {
                    data = reader.ReadToEnd();
                }
            }

            return data;
        }

        protected void GetUserAndSendEmail(DataTable userResult, string email, string type)
        {
            if (userResult.Rows.Count > 0)
            {
                var row = userResult.Rows[0];
                var userId = row["ID"].ToString();

                if (type.Equals("password"))
                {
                    GenerateAndSendPasswordEmail(userId, email);
                }
                else if (type.Equals("username"))
                {
                    var usernames = "";
                    for (int i = 0; i < userResult.Rows.Count; i++)
                    {
                        var username = userResult.Rows[i]["UserID"].ToString();
                        usernames += username;
                        if (i < userResult.Rows.Count - 1)
                        {
                            usernames += ", ";
                        }
                        else
                        {
                            usernames += ".";
                        }
                    }

                    GenerateAndSendUsernameEmail(userId, email, usernames);
                }
            }
        }


        protected void ForgotPasswordButton_OnClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = ForgotPasswordUsername.Text.Trim();
                DataTable userResult = FindUserByUsername(username);

                if (userResult.Rows.Count > 0)
                {
                    var row = userResult.Rows[0];
                    var email = row["Email"].ToString();
                    GetUserAndSendEmail(userResult, email, "password");
                    EmailSentMsgModal.VisibleOnPageLoad = true;
                    emailField.Text = "We have sent a link to reset your password to :<br> <b> " + HideEmailAddress(email) + "</b>";
                    //SmallPrint.Text = "If this email is incorrect please contact us at<br/>webmaster@charteredaccountants.ie with your<br/>user ID, date of birth and the first line of your address.";
                    string webmasteremail = "mailto:webmaster@charteredaccountants.ie?Subject=Reset%20password%20request&body=To%20help%20us%20give%20you%20the%20right%20credentials%20as%20quickly%20as%20possible%20please%20include%20the%20following%20information:%0D%0A%0D%0AYour%20user%20ID/member%20number/student%20number;%0D%0AYour%20date%20of%20birth;%0D%0AThe%20first%20line%20of%20your%20address.";
                    string target = "_top";
                    //string urlcolor = "color: #003D51";
                    string underline = "text-decoration: underline";
                    string script = String.Format("<p>  <a  href='" + webmasteremail + "' target='" + target + "' style='" + underline + "'><strong>WEBMASTER</strong></a></p>");
                    //SmallPrint.Text = "If this email is incorrect please contact us at <br/>webmaster@charteredaccountants.ie <br />Tel: +353 1 637 7200 or +44 28 90435840";
                    SmallPrint.Text = "If this email address is incorrect please click to email<br/>" + script + "quoting your user ID/member no/student no. <br/>Tel: +353 1 637 7200 or +44 28 90435840";
                }
                else
                {
                    MissingEmailMsgModal.VisibleOnPageLoad = true;
                }
            }
        }

        protected void ForgotUsernameButton_OnClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string email = ForgotUsernameEmail.Text.Trim();
                string firstName = ForgotUsernameFirstName.Text.Trim();
                string lastName = ForgotUsernameLastName.Text.Trim();
                StringBuilder sqlCheckUser = new StringBuilder();
                //validate  firstname contain second name in DB
                DataTable userResult = FindUserByEmail(email);
                if (userResult.Rows.Count > 0)
                {
                    StringBuilder sqluser = new StringBuilder();
                    sqluser.AppendFormat("EXEC [Aptify].[dbo].[spGetWebUserByFnameLnameEmail__cai]  @wemail =\"" + email + "\", @wfname =\"" + firstName + "\" , @wlname = \"" + lastName + "\", @val=");

                    if (userResult.Rows[0]["FirstName"].ToString().Contains(" "))
                    {
                        sqluser.AppendFormat("2");
                    }
                    else
                    {
                        sqluser.AppendFormat("3");
                    }

                    DataTable dt = DataAction.GetDataTable(sqluser.ToString());
                    GetUserAndSendEmail(dt, email, "username");
                    EmailSentMsgModal.VisibleOnPageLoad = true;
                    emailField.Text = "We have sent your username to your email address.";
                    SmallPrint.Text = "If you continue to have problems accessing your account,<br/> contact us at webmaster@charteredaccountants.ie<br/> Tel: +353 1 637 7200 or + 44 28 90435840";

                }


                //sqlCheckUser.AppendFormat("select ID, UserID from Aptify.dbo.WebUser where Email = '{0}' and FirstName like '{1}%' and LastName like '%{2}%';", email, firstName, lastName.Replace(@"'", @"''"));
                //sqlCheckUser.AppendFormat("EXEC [dbo].[spGetWebUserByFnameLnameEmail__cai]  @wemail ='" + email + "', @wfname ='" + firstName + "' , @wlname = '" + lastName + "', @val=2");
                // DataTable userResult = DataAction.GetDataTable(sqlCheckUser.ToString());

                //if (userResult.Rows.Count > 0)
                //{
                //    GetUserAndSendEmail(userResult, email, "username");
                //    EmailSentMsgModal.VisibleOnPageLoad = true;
                //    emailField.Text = "We have sent your username to your email address.";
                //    SmallPrint.Text = "If you continue to have problems accessing your account,<br/> contact us at webmaster@charteredaccountants.ie<br/> Tel: +353 1 637 7200 or + 44 28 90435840";

                //}
                else
                {
                    MissingEmailMsgModal.VisibleOnPageLoad = true;
                }
            }
        }

        protected void MissingEmailOKBtn_OnClick(object sender, EventArgs e)
        {
            MissingEmailMsgModal.VisibleOnPageLoad = false;
        }

        protected DataTable FindUserByEmail(string email)
        {
            StringBuilder sqlCheckUser = new StringBuilder();
            sqlCheckUser.AppendFormat("EXEC [Aptify].[dbo].[spGetWebUserByFnameLnameEmail__cai] '" + email + "',NULL,NULL,1");
            DataTable dt1 = new DataTable();
            dt1 = DataAction.GetDataTable(sqlCheckUser.ToString());
            return dt1;
        }

        protected DataTable FindUserByUsername(string username)
        {
            StringBuilder sqlCheckUser = new StringBuilder();
            sqlCheckUser.AppendFormat("EXEC [Aptify].[dbo].[spGetWebUser] @UserId='" + username + "'");
            DataTable result = DataAction.GetDataTable(sqlCheckUser.ToString());
            return result;
        }

        private bool IsNewUser(string username)
        {

            DataTable dt = FindUserByUsername(username);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["SessionCount"].ToString()) > 0)
                {
                    return false;
                }
                else
                { return true; }
            }
            else

            { return false; }
        }
        //Added as part of #20448 to load the webusers
        public void LoadWebUsers()
        {
            StringBuilder sqlCheckUser = new StringBuilder();
            sqlCheckUser.AppendFormat(_login.GetWebUserByPersonId(this).Result.ToString());

            var dT = DataAction.GetDataTable(sqlCheckUser.ToString());
            _combo.PopulateComboList(dT, cmdWebUser, WebUserLogin1.User.WebUserStringID, "WebUserID", "asc");

            if (cmdWebUser.Items.Count <= 1)
            {
                //lblWebUser.Visible = false;
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

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(SqlResource.GetWebUserDetailsByWebUserID, this.Database, cmdWebUser.SelectedItem.Value);
            DataTable dataTable = this.DataAction.GetDataTable(sql.ToString(), IAptifyDataAction.DSLCacheSetting.BypassCache);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                AptifyGenericEntityBase webUser = this.AptifyApplication.GetEntityObject("Web Users", this.WebUserID);

                luserID = dataTable.Rows[0]["UserID"].ToString();
                lpassword = DecryptPassword(dataTable.Rows[0]["PWD"].ToString());

                m_bIsOnBehalf = true;
                chkAutoLogin.Checked = true;

                bool bLoggedOut = false;
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
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }

                if (bLoggedOut)
                {
                    OnUserLoggedOut(e);
                }
                cmdLogin_Click(sender, e);
            }
        }
        //End of #20448
    }
}
