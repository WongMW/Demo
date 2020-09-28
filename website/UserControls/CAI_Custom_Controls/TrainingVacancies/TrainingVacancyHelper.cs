using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.TrainingVacancies
{
    public enum TrainingVacancyActions { SendToApproval = 1, Accept = 2, Reject = 3, Close = 4, Delete = 5 };
    public class TrainingVacancyHelper
    {
        public static bool SendToApproval(int id)
        {
            bool result = true;

            return result;
        }

        public static bool AcceptTrainingVacancy(int id, string acceptedby)
        {
            bool result = true;

            return result;
        }

        public static bool RejectTrainingVacancy(int id)
        {
            bool result = true;

            return result;
        }

        public static bool CloseTrainingVacancy(int id)
        {
            bool result = true;

            return result;
        }

        public static bool DeleteTrainingVacancy(int id)
        {
            bool result = true;

            return result;
        }
    }

    [Serializable]
    public class Company
    {
        public int id { get; set;  }
        public string label { get; set; }
        public string value { get; set; }
        
    }

    [Serializable]
    public class FirmDetails
    {
        public int Fid { get; set; }
        public string Fname { get; set; }
        public bool IsParent { get; set; }
        public int ParentId { get; set; }
        public bool PurchaseAllowed { get; set; }
        public bool IsRenewal { get; set; }
        public DateTime? ApprovedEndDate { get; set; }
    }

    [Serializable]
    public class TrainingVacancy
    {
        public int TVID { get; set; }
        public string TVcompanyName { get; set; }
        public string TVJobTitle { get; set; }
        public string TVIsDraft { get; set; }
        public string TVWebStatus { get; set; }
        public string TVCreationDate { get; set; }
        public string TVClosingDate { get; set; }
        public string TVLastUpdateDate { get; set; }
    }
}