using System;

namespace SitefinityWebApp.Model.EAssessment.Cirrus
{
    public class ScheduleCirrus
    {
        #region RequestRegion
        public string Title { get; set; }
        public bool? LockExamOnConnectionLoss { get; set; }
        public string GroupName { get; set; }
        public string GroupExtId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string AssessmentExtId { get; set; }
        public string ScheduleExtId { get; set; }
        #endregion

        public string ScheduleGroupExtId { get; set; }
        public string ScheduleGroupName { get; set; }
        public string PIN { get; set; }
        public int? ExtraTime { get; set; }
        public string Owner { get; set; }
    }
}