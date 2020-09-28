using System.Collections.Generic;
using static SitefinityWebApp.BusinessFacade.Resources.Enum.GeneralEnum;

namespace SitefinityWebApp.Model.EAssessment.Cirrus
{
    public class ScheduleCreateRequestCirrus
    {
        public ScheduleCirrus Schedule { get; set; }
        public IList<CandidatesCirrus> Candidates { get; set; }
    }

    public class ScheduleCreateResponseCirrus
    {
        public string Result { get; set; }
        public ScheduleCirrus Schedule { get; set; }
        public IList<CandidatesCirrus> Candidates { get; set; }
        public string Message { get; set; }
        public ValidatecirrusMessages Status { get; set; }
    }
}