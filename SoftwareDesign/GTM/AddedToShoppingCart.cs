using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using SoftwareDesign.GTM.Model;

namespace SoftwareDesign.GTM
{
    public class AddedToShoppingCart : GtmBase
    {
        private readonly ImpressionDto _impressionDto;

        public AddedToShoppingCart(Page page, ImpressionDto impressionDto) 
            : base(page)
        {
            if (impressionDto == null) throw new ArgumentNullException(nameof(impressionDto));
            _impressionDto = impressionDto;

            TemplateName = "_GTM-added-to-shopping-cart.cshtml";
        }

        protected override object GetDto()
        {
            return _impressionDto;
        }
    }
}
