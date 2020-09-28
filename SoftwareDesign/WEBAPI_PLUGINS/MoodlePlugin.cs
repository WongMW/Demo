using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using Aptify.Framework.ExceptionManagement;
using SoftwareDesign.BusinessFacade.Resources;

namespace SoftwareDesign.WEBAPI_PLUGINS
{
    public class MoodlePlugin
    {
        private readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        public string ConsumeJWTToken(string username)
        {
            string url = MoodleResource.GetCpdTokenUrl;

            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                //var data = "webuser";

                if ((!string.IsNullOrEmpty(url)))
                {
                    try
                    {

                        var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("userid", username) });
                        System.Net.Http.HttpResponseMessage responseMessage = null;
                        responseMessage = httpClient.PostAsync(url, content).Result;
                        responseMessage.EnsureSuccessStatusCode();
                        string responJsonText = responseMessage.Content.ReadAsStringAsync().Result;
                        //  var result = JsonConvert.DeserializeObject(responJsonText);
                        // string jwttoken = "rabcd.aghtshshs.yutrt2";
                        if (responJsonText != null && !responJsonText.Contains("https://") && !responJsonText.Contains("http://"))
                        {
                            //  string moodleurl = "https://moodle.charteredaccountants.ie/login/index.php?manual_withjwt=" + result;
                            // uat website 
                            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('http://charteredaccountants.staging.synergy-learning.com/login/index.php?manual_withjwt=" + responJsonText + "')", true);

                            // live website 
                            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('https://moodle.charteredaccountants.ie/login/index.php?manual_withjwt=" + responJsonText + "')",true);
                            return responJsonText;
                        }
                        else
                        {
                            // Redirect to moodle  manual login 
                            //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('http://charteredaccountants.staging.synergy-learning.com/login/index.php');", true);
                            return "false";
                        }



                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                        ///return ex.ToString();
                        // Redirect to moodle  manual login 
                        //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('http://charteredaccountants.staging.synergy-learning.com/login/index.php');", true);
                        return "false";
                    }

                }
                else { return "false"; }
            }



        }

        public string ConsumeJWTTokenEducation(string username)
        {
            try
            {
                username = "16c4536";
                string url = MoodleResource.GetEducationTokenUrl;

                using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
                {
                    if ((!string.IsNullOrEmpty(url)))
                    {
                        var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("userid", username) });
                        System.Net.Http.HttpResponseMessage responseMessage = null;
                        responseMessage = httpClient.PostAsync(url, content).Result;
                        responseMessage.EnsureSuccessStatusCode();
                        string responJsonText = responseMessage.Content.ReadAsStringAsync().Result;

                        if (responJsonText != null && !responJsonText.Contains("https://") && !responJsonText.Contains("http://"))
                        {
                            return MoodleResource.EducationMoodleUrl + "login/index.php?manual_withjwt=" + responJsonText;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        // Check userid is exist in moodle 
        public int GetMoodleUserID(string username)
        {


            if ((!string.IsNullOrEmpty(username)))
            {
                // UAT Moodle URL Get User   
                //string url = "http://charteredaccountants.staging.synergy-learning.com/webservice/rest/server.php?wstoken=c764061c436e477aad4e1fe7ed838f05&wsfunction=core_user_get_users&moodlewsrestformat=json";

                //Live Moodle URL for Get User :
                string url = MoodleResource.CpdMoodleUrl + "webservice/rest/server.php?wstoken=f766a540f361b81f2e275762d848bc21&wsfunction=core_user_get_users&moodlewsrestformat=json";

                using (HttpClient httpClient = new HttpClient())
                {
                    var content1 = new FormUrlEncodedContent(new[]
                    {
                     new KeyValuePair<string, string>("criteria[0][key]", "username"),
                     new KeyValuePair<string, string>("criteria[0][value]", username ) });

                    HttpResponseMessage response = null;
                    using (response = httpClient.PostAsync(url, content1).Result)
                    {
                        try
                        {
                            string responJson = "";
                            response.EnsureSuccessStatusCode(); //200 Ok
                            responJson = response.Content.ReadAsStringAsync().Result;
                            JObject res = JObject.Parse(responJson);
                            if (res["users"].Count() > 0)
                            {
                                var uid = (int)res["users"][0]["id"];
                                if (uid > 0)
                                {
                                    return uid;
                                }
                                else
                                { return 0; }
                            }
                            else
                            { return 0; }
                        }
                        catch (HttpRequestException)
                        {
                            if (response.StatusCode == HttpStatusCode.NotFound) // 404
                            {
                                return 0;
                            }
                            else
                            {
                                return 0;
                            }

                        }

                    }



                }

            }
            { return 0; }
        }

        // Create userid in Moodle
        public int CreateMoodleWebuser(string un, string fn, string ln, string em)
        {
            if ((!string.IsNullOrEmpty(un)))
            {
                //UAT Create user :
                //string url = "http://charteredaccountants.staging.synergy-learning.com/webservice/rest/server.php?wstoken=c764061c436e477aad4e1fe7ed838f05&wsfunction=core_user_create_users&moodlewsrestformat=json";

                // LIVE Create user:
                string url = MoodleResource.CpdMoodleUrl + "webservice/rest/server.php?wstoken=f766a540f361b81f2e275762d848bc21&wsfunction=core_user_create_users&moodlewsrestformat=json";
                //https://moodle.charteredaccountants.ie/webservice/rest/server.php?wstoken=f766a540f361b81f2e275762d848bc21&wsfunction=core_user_create_users&moodlewsrestformat=json&users[0][username]=99a0258&users[0][password]=Pass1word!&users[0][firstname]=Thomas&users[0][lastname]=Murphy&users[0][email]=thomas@inspection.ie&users[0][auth]=manualwithjwt


                using (HttpClient httpClient = new HttpClient())
                {

                    // users[0][username] = test1 & users[0][password] = Cromac01!&users[0][firstname] = Test & users[0][lastname] = One & users[0][email] = test1@null.com & users[0][auth] = manualwithjwt
                    var content2 = new FormUrlEncodedContent(new[]
                    {
                         new KeyValuePair<string, string>("users[0][username]", un.ToLower()),
                         new KeyValuePair<string, string>("users[0][password]", "Pass1word/"),
                         new KeyValuePair<string, string>("users[0][firstname]", fn),
                         new KeyValuePair<string, string>("users[0][lastname]", ln),
                         new KeyValuePair<string, string>("users[0][email]", em.ToLower()),
                         new KeyValuePair<string, string>("users[0][auth]", "manualwithjwt"),
                         new KeyValuePair<string, string>("users[0][idnumber]", ""),
                         new KeyValuePair<string, string>("users[0][lang]", "en"),
                         new KeyValuePair<string, string>("users[0][calendartype]", "gregorian")
                    });

                    HttpResponseMessage response = null;
                    using (response = httpClient.PostAsync(url, content2).Result)
                    {
                        try
                        {
                            string JsonText = "";
                            response.EnsureSuccessStatusCode(); //200 Ok
                            JsonText = response.Content.ReadAsStringAsync().Result;

                            JObject objJ = JObject.Parse(JsonText);

                            if (objJ.First.ToString().Contains("exception"))
                            {
                                return 0;
                            }
                            else
                            {
                                JArray ares = JArray.Parse(JsonText);

                                int id = ares.First["id"].Value<int>();
                                if (id > 0)
                                    return id;
                                else
                                    return 0;
                            }
                        }
                        catch (HttpRequestException)
                        {
                            if (response.StatusCode == HttpStatusCode.NotFound) // 404
                            {
                                return 0;
                            }
                            else
                            {
                                return 0;
                            }

                        }

                    }

                }
            }
            { return 0; }
        }

        //  Manual Enrol user
        public bool ManualEnrolUser(string userid, string courseid, DateTime timestart, DateTime timeend)
        {

            if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(courseid))
            {

                //DateTime expdate = DateTime.ParseExact(timeend, "YYYY-MM-DD HH:mm:ss", CultureInfo.InvariantCulture);

                DateTime uts = timestart;
                var epoch1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var utimestart = (uts.ToUniversalTime() - epoch1).TotalSeconds.ToString();


                // exp date convert to UNIXTIMESTAMP
                DateTime ute = timeend;
                var epoch2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var utimeend = (ute.ToUniversalTime() - epoch2).TotalSeconds.ToString();

                //UAT URL
                //string url = "http://charteredaccountants.staging.synergy-learning.com/webservice/rest/server.php?wstoken=c764061c436e477aad4e1fe7ed838f05&wsfunction=enrol_manual_enrol_users&moodlewsrestformat=json";

                // LIVE URL 
                string url = MoodleResource.CpdMoodleUrl + "webservice/rest/server.php?wstoken=f766a540f361b81f2e275762d848bc21&wsfunction=enrol_manual_enrol_users&moodlewsrestformat=json";


                using (HttpClient httpClient = new HttpClient())
                {


                    HttpResponseMessage response = null;
                    if ((utimestart.Contains("-")) && (utimeend.Contains("-")))
                    {
                        var content3 = new FormUrlEncodedContent(new[]
                          {
                             new KeyValuePair<string, string>("enrolments[0][roleid]", "5" ),
                             new KeyValuePair<string, string>("enrolments[0][userid]", userid ),
                             new KeyValuePair<string, string>("enrolments[0][courseid]", courseid ),
                            // new KeyValuePair<string, string>("enrolments[0][timestart]", utimeend  ),
                             //new KeyValuePair<string, string>("enrolments[0][timeend]", utimestart)
                            });
                        using (response = httpClient.PostAsync(url, content3).Result)
                        {
                            string rJson = "";
                            try
                            {
                                response.EnsureSuccessStatusCode(); //200 Ok
                                rJson = response.Content.ReadAsStringAsync().Result;
                                // JObject res = JObject.Parse(responJsonText);
                                //var uid = (int)res["users"][0]["id"];
                                //if (uid > 0)
                                //{
                                //    return uid;
                                //}

                                if (rJson.Equals("null"))
                                {
                                    return true;
                                }
                            }
                            catch (HttpRequestException)
                            {
                                if (response.StatusCode == HttpStatusCode.NotFound) // 404
                                {
                                    return false;
                                }


                            }


                            return true;
                        }




                    }
                    else
                    {

                        var content4 = new FormUrlEncodedContent(new[]
                               {
                             new KeyValuePair<string, string>("enrolments[0][roleid]", "5" ),
                             new KeyValuePair<string, string>("enrolments[0][userid]", userid ),
                             new KeyValuePair<string, string>("enrolments[0][courseid]", courseid ),
                             new KeyValuePair<string, string>("enrolments[0][timestart]",utimestart ),
                             new KeyValuePair<string, string>("enrolments[0][timeend]", utimeend)
                            });


                        using (response = httpClient.PostAsync(url, content4).Result)
                        {
                            try
                            {
                                response.EnsureSuccessStatusCode(); //200 Ok
                                string responJsonText = response.Content.ReadAsStringAsync().Result;
                                // JObject res = JObject.Parse(responJsonText);
                                //var uid = (int)res["users"][0]["id"];
                                //if (uid > 0)
                                //{
                                //    return uid;
                                //}

                                if (responJsonText.Equals("null"))
                                {
                                    return true;
                                }
                            }
                            catch (HttpRequestException)
                            {
                                if (response.StatusCode == HttpStatusCode.NotFound) // 404
                                {
                                    return false;
                                }


                            }


                            return true;
                        }

                    }



                }

            }
            else { return false; }
        }

        // Update User in Moodle 
        public bool UpdateMoodleUser(string userid, string username, string fn, string ln, string em)
        {
            if ((!string.IsNullOrEmpty(userid)))
            {
                //UAT URL
                //string url = "http://charteredaccountants.staging.synergy-learning.com/webservice/rest/server.php?wstoken=c764061c436e477aad4e1fe7ed838f05&wsfunction=core_user_update_users&moodlewsrestformat=json";

                //LIVE URL
                string url = MoodleResource.CpdMoodleUrl + "webservice/rest/server.php?wstoken=f766a540f361b81f2e275762d848bc21&wsfunction=core_user_update_users&moodlewsrestformat=json";


                using (HttpClient httpClient = new HttpClient())
                {

                    // users[0][username] = test1 & users[0][password] = Cromac01!&users[0][firstname] = Test & users[0][lastname] = One & users[0][email] = test1@null.com & users[0][auth] = manualwithjwt
                    var content2 = new FormUrlEncodedContent(new[]
                    {
                     new KeyValuePair<string, string>("users[0][id]", userid),
                     new KeyValuePair<string, string>("users[0][username]", username),
                     new KeyValuePair<string, string>("users[0][password]", "Pass1word/"),
                     new KeyValuePair<string, string>("users[0][firstname]", fn),
                     new KeyValuePair<string, string>("users[0][lastname]", ln),
                     new KeyValuePair<string, string>("users[0][email]", em),
                     new KeyValuePair<string, string>("users[0][auth]", "manualwithjwt") });




                    HttpResponseMessage response = null;
                    using (response = httpClient.PostAsync(url, content2).Result)
                    {
                        try
                        {
                            string JText = "";
                            response.EnsureSuccessStatusCode(); //200 Ok
                            JText = response.Content.ReadAsStringAsync().Result;
                            if (JText.Equals("null"))
                            {
                                return true;
                            }

                        }
                        catch (HttpRequestException)
                        {
                            if (response.StatusCode == HttpStatusCode.NotFound) // 404
                            {
                                return false;
                            }
                            else
                            {
                                return false;
                            }

                        }

                    }

                }
            }
            { return false; }
        }

    }
}


