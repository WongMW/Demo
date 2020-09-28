using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SoftwareDesign.GTM
{
    public class GtmClick : GtmBase
    {
        public GtmClick(Page page) : base(page)
        {
            TemplateName = "_GTM-click.cshtml";
        }

        protected override object GetDto()
        {
            return null;
        }
    }
}
