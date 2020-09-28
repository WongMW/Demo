using SitefinityWebApp.Model.EAssessment.Cirrus;
using SitefinityWebApp.Model.EAssessment.ProctorU;
using System.Threading.Tasks;

namespace SitefinityWebApp.BusinessFacade.Interfaces.EAssessment
{
    public interface EAssessmentInterface
    {
        Task<ProctoruRootObject> BuildProctorUHttpWebRequest(Aptify.Framework.Web.eBusiness.User user1, string classId);
        Task<ScheduleCreateResponseCirrus> BuildCirrusHttpWebRequest(string oldId, string classId);
    }
}
