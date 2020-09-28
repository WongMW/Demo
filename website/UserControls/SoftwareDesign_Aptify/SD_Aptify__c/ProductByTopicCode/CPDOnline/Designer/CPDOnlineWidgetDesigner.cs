using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPD.Designer;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline.Designer
{
    public class CPDOnlineWidgetDesigner : CPDWidgetDesigner
    {
        protected override string TopicCodeSettingName => "CPD.Online.TopicCodes";
        protected override string ScriptReference => 
            "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ProductByTopicCode/CPDOnline/Designer/CPDOnlineWidgetDesigner.js";
    }
}