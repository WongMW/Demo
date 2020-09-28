using Aptify.Framework.Web.eBusiness;
using System;
using Telerik.Sitefinity.Web.UI;

namespace SoftwareDesign.Controls.SDWidgets
{
    public partial class uc_rootPageListing : BaseUserControlAdvanced
    {
        public String ListCount { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("SoftwareDesign.Controls.SDWidgets.uc_rootPageListing.ascx");
    }
}