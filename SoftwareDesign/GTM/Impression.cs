using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using RazorEngine;
using SoftwareDesign.GTM.Helper;
using SoftwareDesign.GTM.Model;

namespace SoftwareDesign.GTM
{
    public class Impression : GtmBase
    {
        private readonly ImpressionDto _impressionDto;

        public Impression(Page page, ImpressionDto impressionDto)
            : base(page)
        {
            if (impressionDto == null) throw new ArgumentNullException(nameof(impressionDto));

            TemplateName = "_GTM-impression.cshtml";
            _impressionDto = impressionDto;
        }

        protected override object GetDto()
        {
            return _impressionDto;
        }


    }
}
