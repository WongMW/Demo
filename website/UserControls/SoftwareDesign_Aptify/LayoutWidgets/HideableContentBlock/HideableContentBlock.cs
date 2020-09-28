using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.HideableContentBlock
{
    public class HideableContentBlock : ContentBlock
    {
        protected override void Render(HtmlTextWriter writer)
        {
            //always render in design / preview mode
            if (this.IsDesignMode() || this.IsPreviewMode())
            {
                base.Render(writer);
                return;
            }

            //exit if parameters are found
            var urlParam = this.GetUrlParameters();
            if (urlParam != null && urlParam.Length > 0) return;

            base.Render(writer);
        }
    }
}