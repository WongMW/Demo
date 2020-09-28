using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace SoftwareDesign
{
    public class Helper
    {
        public static void LogApplicationLevelException(String source, String eventname, Exception ex)
        {
            string log;
            log = "Application";
            eventname = eventname + Environment.NewLine + ex.StackTrace;

            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, log);

            EventLog.WriteEntry(source, eventname);
            EventLog.WriteEntry(source, eventname, EventLogEntryType.Error);
        }

        public static String GetAptifyEntitiesConnectionString(bool? trusted = false)
        {
            var aptifyDBServer = ConfigurationManager.AppSettings["AptifyDBServer"];
            var aptifyEntitiesDB = ConfigurationManager.AppSettings["AptifyEntitiesDB"];
            var aptifyEBusinessSQLLogin = ConfigurationManager.AppSettings["AptifyEBusinessSQLLogin"];
            var aptifyEBusinessSQLPWD = ConfigurationManager.AppSettings["AptifyEBusinessSQLPWD"];
            var aptifyEBusinessSQLTimeOut = ConfigurationManager.AppSettings["Aptify.Framework.DataServices.Constants.QueryTimeOut"];
            var aptifyEBusinessSQLPoolMaxSize = ConfigurationManager.AppSettings["Aptify.Framework.LoginServices.AptifyLogin.ConnectionPoolMaxSize"];

            if ((String.IsNullOrEmpty(aptifyEBusinessSQLLogin) || String.IsNullOrEmpty(aptifyEBusinessSQLPWD)) || trusted.Value)
            {
                return "Server=" + aptifyDBServer + ";Database=" + aptifyEntitiesDB + ";Trusted_Connection=True;MultipleActiveResultSets=True;Connect Timeout= " + aptifyEBusinessSQLTimeOut + ";Max Pool Size= " + aptifyEBusinessSQLPoolMaxSize + ";";
            }

            return "Data Source=" + aptifyDBServer + ";" +
                "Initial Catalog=" + aptifyEntitiesDB + ";" +
                "Persist Security Info=True;MultipleActiveResultSets=True;" +
                "User ID=" + aptifyEBusinessSQLLogin + ";" +
                "Password=" + aptifyEBusinessSQLPWD + ";" +
                "Connect Timeout=" + aptifyEBusinessSQLTimeOut + ";" +
                "Max Pool Size=" + aptifyEBusinessSQLPoolMaxSize + ";";
        }

        public static String GetCategoryTitle(Object _cats)
        {
            Telerik.OpenAccess.TrackedList<System.Guid> cats = (Telerik.OpenAccess.TrackedList<System.Guid>)_cats;
            string[] ignoreCats = new string[]
            {
                "Chartered Accountants Ireland",
                "Accountancy Ireland",
                "RTP News",
                "CPD",
                "Public Sector",
                "enews",
                "Education",
                "Overseas Member",
                "RTP",
                "RTPeNews",
                "Student News",
                "Tax Representations"
            };

            if (cats.Count == 0)
            {
                return "";
            }

            var foundTaxon = "";

            for (var i = 0; i < cats.Count; i++)
            {
                Guid firstCat = cats[i];

                var taxonomyManager = TaxonomyManager.GetManager();
                var taxon = taxonomyManager.GetTaxa<HierarchicalTaxon>().Where(t => t.Id.Equals(firstCat)).SingleOrDefault();

                if (taxon != null && !ignoreCats.Contains(taxon.Title.ToString()))
                {
                    foundTaxon = taxon.Title;
                    break;
                }
            }

            return foundTaxon;
        }

        public static String GetNewsArticleImage(Object _img, Object _cats)
        {
            Telerik.OpenAccess.TrackedList<System.Guid> cats = (Telerik.OpenAccess.TrackedList<System.Guid>)_cats;
            Image img = (Image)_img;

            String imgUrl = "~/Images/CAITheme/Taxation-Audits.png";

            if (img != null)
            {
                return img.Url;
            }
            else if (cats.Count > 0)
            {
                // lets retrieve all images assigned under categories
                List<Image> imgs = new List<Image>();
                foreach (var cat in cats)
                {
                    var content = App.WorkWith().Images().Where(ci => ci.GetValue<IList<Guid>>("Category").Contains(cat));
                    content.ForEach(a => imgs.Add(a));
                }

                if (imgs.Count > 0)
                {
                    Random rnd = new Random(DateTime.Now.Millisecond);
                    imgUrl = imgs[rnd.Next(0, imgs.Count)].Url;
                }
            }

            return imgUrl;
        }

        public static void SendEmail(String subject, String body, String email)
        {
                //build the email message
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(ConfigurationManager.AppSettings["MailFrom"], email);
                mail.BodyEncoding = Encoding.Default;
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Port = int.Parse(ConfigurationManager.AppSettings["MailPort"]);
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = ConfigurationManager.AppSettings["UseDefaultCredentials"] == "true";
                if (client.UseDefaultCredentials)
            {
                client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["MailUserName"], ConfigurationManager.AppSettings["MailPassword"]);
                //client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["MailUserName"], ConfigurationManager.AppSettings["MailPassword"]);
                client.UseDefaultCredentials = true;
                }
                client.Host = ConfigurationManager.AppSettings["MailServer"];
                client.EnableSsl = ConfigurationManager.AppSettings["Mail.EnableSsl"] == "true";
                client.Send(mail);
        }
    }
}
