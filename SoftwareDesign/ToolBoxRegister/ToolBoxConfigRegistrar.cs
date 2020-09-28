using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace SoftwareDesign.ToolBoxRegister
{
    class ToolBoxConfigRegistrar
    {
        private readonly string _virtualBasePath;
        private readonly ToolboxesConfig _config;
        private readonly Dictionary<string, string> _sectionMapping;
        private readonly Dictionary<string, string> _fileMapping;

        public ToolBoxConfigRegistrar(string virtualBasePath)
        {
            if (String.IsNullOrWhiteSpace(virtualBasePath))
                throw new ArgumentException("Argument is null or whitespace", "virtualBasePath");
            _virtualBasePath = virtualBasePath;

            var configManager = ConfigManager.GetManager();
            _config = configManager.GetSection<ToolboxesConfig>();

            _sectionMapping = GetCustomSectionMapping();
            _fileMapping = GetCustomFileMapping();
        }

        public void RegisterPath(string filePath)
        {
            var toolPathRegistar = new ToolPathRegistar(_virtualBasePath, filePath, _config, _sectionMapping, _fileMapping);
            toolPathRegistar.RegisterOrUpdateFile();
        }

        public void Save()
        {
            var configManager = ConfigManager.GetManager();
            configManager.SaveSection(_config);
        }

        private static Dictionary<string, string> GetCustomSectionMapping()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"Aptify_Group_Admin", "Aptify GroupAdmin"},
                {"Aptify_Custom__c", "Aptify Custom"}
            };

            return dictionary;
        }


        private static Dictionary<string, string> GetCustomFileMapping()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"SynchProfile", "SynchProfileSF4"},
                {"SecurityError", "Security Error"},
                {"Cases__c","Cases"},
                {"ExpoRegistration__c","ExpoRegistration"},
                {"ProductCategory__c","ProductCategory"},
                {"MakePayment__c","MakePayment"},
                {"MakePaymentSummary__c","MakePaymentSummary"},
                {"CheckOutControl__c","CheckOutControl__c.ascx"},
                {"ViewCart__c","ViewCart__c.ascx"},
                {"MyCommittees__c","MyCommittees"},
                {"RoomBookingApplications__c","Room Booking Applications"},
                {"MainCourseScheduleCalendar__c","MainCourseScheduleCalendar__c.ascx"},
                {"TestPivotGrid","TestPivotGrid__c"},
                {"ExemptionApplication__c","ExemptionApplications__c"},
                {"WhoPaysETD__c","WhoPaysETD"},
                {"WebRemittanceHistory__c","WebRemittanceHistory"},
                {"AppealReportViewer__c","AppealReportViewer"},
                {"WebRemittanceDetails__c","WebRemittanceDetails"},
                {"StudentEnrollment__c","StudentEnrollment"},
                {"FirmCourseEnrollment__c","FirmCourseEnrollment"},
                {"FirmCourseEnrollmentSummary__c","FirmCourseEnrollmentSummary"},
                {"Counties__c","Counties"},
                {"MyMeetings"," MyMeetings "}
                
            };


            return dictionary;
        }
    }
}
