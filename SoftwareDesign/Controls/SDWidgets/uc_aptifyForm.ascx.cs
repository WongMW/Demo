using Aptify.Framework.Web.eBusiness;
using System;
using Telerik.Sitefinity.Web.UI;

namespace SoftwareDesign.Controls.SDWidgets
{
    public partial class uc_aptifyForm : BaseUserControlAdvanced
    {
        public String XFormID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtPodcastListing.Text = "Generate Form based on Aptify for FormID " + XFormID;
        }

        public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("SoftwareDesign.Controls.SDWidgets.uc_aptifyForm.ascx");
    }
}