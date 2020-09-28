using System;
using System.Web.UI;
using SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline.Designer;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline
{
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(CPDOnlineWidgetDesigner))]
    public partial class CPDOnlineWidget : UserControl
    {
        private string _topicCodes = string.Empty;
        public string TopicCodes
        {
            get { return _topicCodes; }
            set
            {
                _topicCodes = value;
            }
        }

        private int _productCount = 10;
        public int ProductCount
        {
            get { return _productCount; }
            set
            {
                _productCount = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CPDWidget1.HideLocationColumn();
            CPDWidget1.SetTitle("Recomended CPD online courses");
            CPDWidget1.TopicCodes = TopicCodes;
            CPDWidget1.ProductCount = ProductCount;
        }

        protected void CPDWidget1_Init(object sender, EventArgs e)
        {
            CPDWidget1.UpdateStoredProcedureName("spGetOnlineCPDInfoForTopicCode__sd");
        }
    }
}
