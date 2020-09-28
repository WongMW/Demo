using SitefinityWebApp.BusinessFacade.Interfaces.EAssessment;
using SitefinityWebApp.BusinessFacade.Interfaces.General;
using SitefinityWebApp.BusinessFacade.Resources;
using SitefinityWebApp.BusinessFacade.Services.General;
using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SitefinityWebApp.Model.EAssessment.ProctorU;
using SitefinityWebApp.Model.EAssessment.Cirrus;
using Newtonsoft.Json;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SoftwareDesign;
using SitefinityWebApp.BusinessFacade.Interfaces.SQL;
using SitefinityWebApp.BusinessFacade.Services.SQL;
using Telerik.Sitefinity.DropboxLibraries.RestSharp;
using Newtonsoft.Json.Linq;

namespace SitefinityWebApp.BusinessFacade.Services.EAssessment
{
    public class EAssessmentService : EAssessmentInterface
    {
        private readonly GeneralInterface _helper = new GeneralService();
        private readonly SqlInterface _sql = new SqlService();
        private readonly GeneralInterface _general = new GeneralService();

        public async Task<ProctoruRootObject> BuildProctorUHttpWebRequest(Aptify.Framework.Web.eBusiness.User user1, string classId)
        {
            string proctorUUrl = EAssessmentResource.ProctoruApiUrl;

            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(proctorUUrl);

            WebHeaderCollection myWebHeaderCollection = webRequest.Headers;

            SslProtocols _Tls12;
            _Tls12 = (SslProtocols)0xC00;
            SecurityProtocolType Tls12;
            Tls12 = (SecurityProtocolType)_Tls12;
            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            //Add header configuration
            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization-Token", EAssessmentResource.ProctoruAuthorizationToken);
            webRequest.ContentType = "application/x-www-form-urlencoded";

            var UTCTimezone = DateTime.UtcNow;
            var userOldId = user1.WebUserStringID.Trim();

            if (user1.PersonDataRow?.ItemArray?.Length > 0 && user1.PersonDataRow[0] != null && user1.WebUserDataRow[1].ToString().Trim() == user1.Email.ToString().Trim() && (user1.PersonDataRow[0].ToString().Trim() == userOldId || user1.PersonDataRow[0].ToString().Trim() == user1.PersonID.ToString().Trim()))
            {
                userOldId = user1.PersonDataRow[0].ToString().Trim();
            }

            var postString = string.Format(EAssessmentResource.PostString, UTCTimezone.Year, _helper.CorrectionForLessThanTen(UTCTimezone.Month), _helper.CorrectionForLessThanTen(UTCTimezone.Day), _helper.CorrectionForLessThanTen(UTCTimezone.Hour), _helper.CorrectionForLessThanTen(UTCTimezone.Minute), userOldId, user1.Email.Trim(), user1.FirstName.Trim(), user1.LastName.Trim());

            // Convert the post string to a byte array
            byte[] bytedata = Encoding.UTF8.GetBytes(postString);
            webRequest.ContentLength = bytedata.Length;

            //aditional configuration
            webRequest.AllowWriteStreamBuffering = true;
            webRequest.KeepAlive = true;
            webRequest.SendChunked = true;
            webRequest.Accept = "*/*";
            webRequest.CachePolicy = noCachePolicy;

            // Create the stream
            Stream requestStream = webRequest.GetRequestStream();
            requestStream.Write(bytedata, 0, bytedata.Length);

            // Get the response from remote server
            HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();

            //convert response to string (UTF-8)
            StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8);

            //convert the string to proctorU object type
            JavaScriptSerializer jss = new JavaScriptSerializer();
                ProctoruRootObject dict = jss.Deserialize<ProctoruRootObject>(readStream.ReadToEnd());

            httpWebResponse.Close();
            readStream.Close();
            requestStream.Close();

            return dict;
        }

        public async Task<ScheduleCreateResponseCirrus> BuildCirrusHttpWebRequest(string oldId, string classId)
        {
            var scheduleToReturn = new ScheduleCreateResponseCirrus();

            try
            {
                string cirrusUrl = EAssessmentResource.CirrusApiUrl;
                string cirrusHost = EAssessmentResource.CirrusApiHost;
                string cirusAuth = EAssessmentResource.CirrusAuthorizationToken;

                SslProtocols _Tls12;
                _Tls12 = (SslProtocols)0xC00;
                SecurityProtocolType Tls12;
                Tls12 = (SecurityProtocolType)_Tls12;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072 | ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                
                var createRequestObj = new ScheduleCreateRequestCirrus();

                var pm = PageManager.GetManager();
                var context = (pm.Provider as IOpenAccessDataProvider).GetContext();
                //var getPageNodeQuery = context.ExecuteQuery<ScheduleCreateRequestCirrus>(@"exec spGetExamSacheduledByStudentId__c @StudentID = 137126, @CourseId = 1579");

                var sql = @"exec spGetExamScheduledByStudentId__c @StudentID = '" + oldId + "', @CourseId = ' " + classId + "'";
                var dtCirrusScheduule = new DataTable();

                //classId = classId.Replace("1578", "1713");

                using (SqlConnection con = new SqlConnection(Helper.GetAptifyEntitiesConnectionString()))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dtCirrusScheduule);
                    con.Close();
                    da.Dispose();
                }

                if (dtCirrusScheduule.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dtCirrusScheduule.Rows[0]["CirrusReconnectURL"].ToString()))
                    {
                        var scheduleObj = new ScheduleCirrus()
                        {
                            AssessmentExtId = dtCirrusScheduule.Rows[0]["AssessmentExtID"].ToString().Trim(),
                            StartDateTime = Convert.ToDateTime(dtCirrusScheduule.Rows[0]["StartDateTime"].ToString().Trim()),
                            GroupExtId = dtCirrusScheduule.Rows[0]["GroupExtId"].ToString().Trim(),
                            GroupName = dtCirrusScheduule.Rows[0]["GroupName"].ToString().Trim(),
                            Title = dtCirrusScheduule.Rows[0]["Title"].ToString().Trim(),
                            LockExamOnConnectionLoss = Convert.ToBoolean(dtCirrusScheduule.Rows[0]["LockExamOnConnectionLoss"].ToString().Trim()),
                            ScheduleExtId = dtCirrusScheduule.Rows[0]["ScheduleExtId"].ToString().Trim()
                        };

                        if (scheduleObj.StartDateTime == null || DateTime.Now > scheduleObj.StartDateTime.Value)
                        {
                            scheduleObj.StartDateTime = DateTime.UtcNow;
                        }
                        else
                        {
                            scheduleObj.StartDateTime = TimeZoneInfo.ConvertTimeToUtc(scheduleObj.StartDateTime.Value);
                        }
                        //need to remove
                        scheduleObj.StartDateTime = Convert.ToDateTime(dtCirrusScheduule.Rows[0]["StartDateTime"].ToString().Trim());

                        var candidateobj = new CandidatesCirrus()
                        {
                            FirstName = dtCirrusScheduule.Rows[0]["FirstName"].ToString().Trim(),
                            LastName = dtCirrusScheduule.Rows[0]["LastName"].ToString().Trim(),
                            Email = dtCirrusScheduule.Rows[0]["Email"].ToString().Trim(),
                            CandidateExtId = dtCirrusScheduule.Rows[0]["CandidateExtId"].ToString().Trim()
                        };

                        var ListOfCandidates = new List<CandidatesCirrus>();
                        ListOfCandidates.Add(candidateobj);

                        createRequestObj = new ScheduleCreateRequestCirrus()
                        {
                            Schedule = scheduleObj,
                            Candidates = ListOfCandidates
                        };


                        string json = JsonConvert.SerializeObject(createRequestObj);

                        //string json = "{'Schedule':{'AssessmentExtId':5050,'StartDateTime':'2020 - 05 - 19T10: 30:00Z','GroupExtId':'1907','GroupName':' * **Chartered Accountants Ireland***','Title':'Mock Exam Testing','LockExamOnConnectionLoss':false,'ScheduleExtId':888991},'Candidates':[{'CandidateExtId':999880,'FirstName':'Joe','LastName':'Bloggs','Email':'joe @bloggs.com'}]}";
                        
                        var clientT2 = new RestClient(cirrusUrl);

                        //--service timeout in milliseconds
                        //--If not set, the default timeout for HttpWebRequest is used(100000ms)
                        int serviceTimeout;
                        if (Int32.TryParse("100000", out serviceTimeout))
                            clientT2.Timeout = serviceTimeout;

                        var requestT2 = new RestRequest(cirrusUrl, Method.POST);
                        requestT2.RequestFormat = DataFormat.Json;

                        requestT2.AddHeader("Content-type", "application/json");
                        requestT2.AddHeader("Host", cirrusHost);
                        requestT2.AddHeader("Authorization", cirusAuth);
                        //requestT2.AddJsonBody(JObject.Parse(json));
                        requestT2.AddParameter("application/json", JObject.Parse(json), ParameterType.RequestBody);

                        IRestResponse responseT2 = clientT2.Execute(requestT2);
                        JObject content = new JObject();

                        if (responseT2.ResponseStatus == Telerik.Sitefinity.DropboxLibraries.RestSharp.ResponseStatus.Completed)
                        {
                            if (responseT2 != null && responseT2.Content != string.Empty)
                            {
                                content = JObject.Parse(responseT2.Content);
                            }
                        }
                        //content = JObject.Parse("{'Result':'Schedule[888991] created successfully','Schedule':{'Title':'Mock Exam Testing','AssessmentExtId':'5050','ScheduleExtId':'888991','StartDateTime':'2020 - 05 - 19T10: 30:00Z','GroupExtId':'1907','GroupName':' * **Chartered Accountants Ireland***','ScheduleGroupExtId':null,'ScheduleGroupName':null,'PIN':null,'ExtraTime':null,'Owner':'cai98276'},'Candidates':[{'CandidateExtId':'999880','StartupLink':' / delivery / external - login ? session = 28F0B08CFE1649EAA90DF0F5F7F60DDB37AD6C0471374E53ADB80C43D225845D','LaunchUrl':'https://test-cai.cirrusplatform.com/delivery/external-login?session=28F0B08CFE1649EAA90DF0F5F7F60DDB37AD6C0471374E53ADB80C43D225845D','FirstName':'Joe','LastName':'Bloggs','Email':'joe@bloggs.com','Password':null}]}");
                        scheduleToReturn = JsonConvert.DeserializeObject<ScheduleCreateResponseCirrus>(JsonConvert.SerializeObject(content));
                        
                        if (responseT2.StatusCode == HttpStatusCode.OK && (scheduleToReturn.Message == null || !scheduleToReturn.Message.Contains("already exists")))
                        {
                            var parameters = new List<SqlParameter>();

                            var launchUrl = "";

                            if (scheduleToReturn != null && scheduleToReturn.Candidates.Count > 0  && !string.IsNullOrEmpty(scheduleToReturn.Candidates[0].LaunchUrl))
                            {
                                launchUrl = scheduleToReturn.Candidates[0].LaunchUrl;

                                parameters.Add(new SqlParameter("@StudentID", dtCirrusScheduule.Rows[0]["PersonID"].ToString().Trim()));
                                parameters.Add(new SqlParameter("@ClassRegistrationID", dtCirrusScheduule.Rows[0]["ClassRegistrationID"].ToString()));
                                parameters.Add(new SqlParameter("@Url", launchUrl));

                                var execReturn = _sql.ExecuteSPUpdateWithoutResultSqlCommand("spUpdateCirrusReconnectURL__c", parameters);

                                scheduleToReturn.Result = launchUrl;
                                scheduleToReturn.Status = Resources.Enum.GeneralEnum.ValidatecirrusMessages.Ok;
                            }
                            else
                            {
                                scheduleToReturn.Result = string.Format(EAssessmentResource.CirrusDefaultErrorMessage, "'Exam link not setted'");
                                scheduleToReturn.Status = Resources.Enum.GeneralEnum.ValidatecirrusMessages.CirrusError;
                            }
                        }
                        else if ((responseT2.StatusCode == HttpStatusCode.BadRequest || responseT2.StatusCode == HttpStatusCode.OK) && scheduleToReturn.Message != null && scheduleToReturn.Message.Contains("already exists"))
                        {
                            var cirrusCode = CirrusExistingIdExtraction(scheduleToReturn.Message).Result;
                            scheduleToReturn.Result = string.Format(EAssessmentResource.CirrusErrorMessageAlreadyExist, cirrusCode);
                            scheduleToReturn.Status = Resources.Enum.GeneralEnum.ValidatecirrusMessages.ExistWithouLink;
                        }
                        else
                        {
                            scheduleToReturn.Result = string.Format(EAssessmentResource.CirrusDefaultErrorMessage, scheduleToReturn.Message);
                            scheduleToReturn.Status = Resources.Enum.GeneralEnum.ValidatecirrusMessages.CirrusError;
                        }
                    }
                    else
                    {
                        scheduleToReturn.Result = dtCirrusScheduule.Rows[0]["CirrusReconnectURL"].ToString();
                        scheduleToReturn.Status = Resources.Enum.GeneralEnum.ValidatecirrusMessages.Ok;
                    }
                }
                else
                {
                    scheduleToReturn.Result = "";
                    scheduleToReturn.Status = Resources.Enum.GeneralEnum.ValidatecirrusMessages.RegistrationNotFound;
                }

            }
            catch (Exception ex)
            {
                scheduleToReturn.Status = Resources.Enum.GeneralEnum.ValidatecirrusMessages.InternalError;
                scheduleToReturn.Result = string.Format(EAssessmentResource.CirrusDefaultErrorMessage, ex.Message.ToString());
            }

            return scheduleToReturn;
        }

        public async Task<string> CirrusExistingIdExtraction (string message)
        {
            string output =string.Empty;

            if (message.Split('|').Length > 1)
            {
                output = message.Split('|')[message.Split('|').Length - 1].Split('[', ']')[1];
            }
            else
            {
                output = message.Split('[', ']')[1];
            }
            return output;
        }
    }
}