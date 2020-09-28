using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aptify.Framework.BusinessLogic.GenericEntity;
using Aptify.Framework.ExceptionManagement;
using Aptify.Framework.Application;
using Aptify.Framework.Web.eBusiness;
using ServiceStack;
using Telerik.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c
{
    public partial class SDNewPasswordControl : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        protected const string ATTRIBUTE_PWD_LENGTH = "minPwdLength";
        protected const string ATTRIBUTE_PWD_UPPERCASE = "minPwdUpperCase";
        protected const string ATTRIBUTE_PWD_LOWERCASE = "minPwdLowerCase";
        protected const string ATTRIBUTE_PWD_NUMBERS = "minPwdNumbers";

        AptifyWebUserLogin WebUserLogin1
        {
            get { return (AptifyWebUserLogin)this.FindControl("WebUserLogin1"); }
        }

        HiddenField SavedUserField
        {
            get { return (HiddenField)this.FindControl("savedUserField"); }
        }

        TextBox TxtPassword1
        {
            get { return (TextBox)this.FindControl("txtPassword1"); }
        }

        RadWindow SuccessWindow
        {
            get { return (RadWindow)FindControl("SuccessWindow"); }
        }

        Label LblError
        {
            get { return (Label)FindControl("lblError"); }
        }

        public int MinPwdLength
        {
            get
            {
                if (ViewState[ATTRIBUTE_PWD_LENGTH] != null)
                {
                    return (int)ViewState[ATTRIBUTE_PWD_LENGTH];
                }
                else
                {
                    int value = GetGlobalAttributeIntegerValue(ATTRIBUTE_PWD_LENGTH);
                    if (value != -1)
                    {
                        ViewState[ATTRIBUTE_PWD_LENGTH] = value;
                        return (int)value;
                    }
                }
                //default if nothing is in config
                return 6;
            }
        }

        public int MinPwdUpperCase
        {
            get
            {
                if (ViewState[ATTRIBUTE_PWD_UPPERCASE] != null)
                {
                    return (int)ViewState[ATTRIBUTE_PWD_UPPERCASE];
                }
                else
                {
                    int value = GetGlobalAttributeIntegerValue(ATTRIBUTE_PWD_UPPERCASE);
                    if (value != -1)
                    {
                        ViewState[ATTRIBUTE_PWD_UPPERCASE] = value;
                        return (int)value;
                    }
                }
                //default if nothing is in config
                return 1;
            }
        }

        public int MinPwdLowerCase
        {
            get
            {
                if (ViewState[ATTRIBUTE_PWD_LOWERCASE] != null)
                {
                    return (int)ViewState[ATTRIBUTE_PWD_LOWERCASE];
                }
                else
                {
                    int value = GetGlobalAttributeIntegerValue(ATTRIBUTE_PWD_LOWERCASE);
                    if (value != -1)
                    {
                        ViewState[ATTRIBUTE_PWD_LOWERCASE] = value;
                        return (int)value;
                    }
                }
                //default if nothing is in config
                return 1;
            }
        }

        public int MinPwdNumbers
        {
            get
            {
                if (ViewState[ATTRIBUTE_PWD_NUMBERS] != null)
                {
                    return (int)ViewState[ATTRIBUTE_PWD_NUMBERS];
                }
                else
                {
                    int value = GetGlobalAttributeIntegerValue(ATTRIBUTE_PWD_NUMBERS);
                    if (value != -1)
                    {
                        ViewState[ATTRIBUTE_PWD_NUMBERS] = value;
                        return (int)value;
                    }
                }
                //default if nothing is in config
                return 1;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                var userId = SavedUserField.Value;
                var pass = TxtPassword1.Text;
                if (IsPasswordComplex(pass))
                {
                    UpdateProfile(userId, pass);
                }
            }
            else
            {

                string token = Request.QueryString["token"];
                if (token != null)
                {
                    //get datetime 24 hours ago
                    DateTime acceptableExpiry = DateTime.UtcNow;

                    //retrieve the token from the db 
                    StringBuilder sql = new StringBuilder();
                    //match non-expired token
                    sql.AppendFormat("EXEC [Ebusiness].[dbo].[spFetchUserPasswordToken__cai]  @token ='" + token + "', @expiry ='" + acceptableExpiry.ToString("yyyy-dd-MM HH:mm:ss") + "'");
                    DataTable results = DataAction.GetDataTable(sql.ToString());

                    if (results.Rows.Count > 0)
                    {
                        var cols = results.Columns;
                        SavedUserField.Value = results.Rows[0]["UserID"].ToString();
                    }
                    else
                    {
                        var login = GetGlobalAttributeValue("LoginPage");
                        Response.Redirect("/" + login);
                    }
                }
            }
        }

        private void UpdateProfile(string userId, string password)
        {
            //The job of this function is to update a user profile with new password
            long longUserId = userId.ToInt64();

            try
            {
                AptifyGenericEntityBase oUser = AptifyApplication.GetEntityObject("Web Users", longUserId);
                //'Update the password, then save and load the user:
                oUser.SetValue("PWD", password);

                if (oUser.Save())
                {
                    bool disabled = Convert.ToBoolean(oUser.GetValue("Disabled"));

                    if (disabled)
                    {
                        oUser.SetValue("Disabled", 0);
                        oUser.SetValue("NumFailedLoginAttempts", 0);
                        oUser.Save();
                    }

                    string username = oUser.GetValue("UserID").ToString();

                    StringBuilder sql = new StringBuilder();
                    //delete tokens from db
                    sql.AppendFormat("EXEC [Ebusiness].[dbo].[spDeleteUserPasswordToken__cai]  @userId ='" + userId + "'");
                    DataAction.ExecuteNonQuery(sql.ToString());

                    SuccessWindow.VisibleOnPageLoad = true;
                }
                else
                {
                    LblError.Text = "Error saving new password. Please try again.";
                    LblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        protected void successBtn_OnClick(object sender, EventArgs e)
        {
            //redirect to login page
            var login = GetGlobalAttributeValue("LoginPage");
            Response.Redirect("/" + login);
        }

        private bool IsPasswordComplex(string password)
        {
            bool result = false;

            if (password.Length >= MinPwdLength)
            {
                result = System.Text.RegularExpressions.Regex.IsMatch(password,
                    @"(?=(.*[A-Z].*){" + MinPwdUpperCase + @",})(?=(.*[a-z].*){" + MinPwdLowerCase + @",})(?=(.*\d.*){" + MinPwdNumbers + @",})");
            }

            if (!result)
            {
                LblError.Text = "Password must be a minimum length of " + MinPwdLength + " with at least " +
                MinPwdLowerCase + " lower-case letter(s) and " + MinPwdUpperCase + " upper-case letter(s) and " +
                MinPwdNumbers + " number(s).";
                LblError.Visible = true;
            }

            return result;
        }
    }
}