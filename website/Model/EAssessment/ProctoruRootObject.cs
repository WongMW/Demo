using System;

namespace SitefinityWebApp.Model.EAssessment
{
    public class ProctoruRootObject
    {
        public DateTime time_sent { get; set; }
        public  int response_code { get; set; }
        public string message { get; set; }
        public ProctoruData data { get; set; }
    }
}