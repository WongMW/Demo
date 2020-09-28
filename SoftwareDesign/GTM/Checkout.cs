using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using SoftwareDesign.GTM.Model;

namespace SoftwareDesign.GTM
{
    public class Checkout : GtmBase
    {
        private readonly CheckoutDto _checkoutDto;

        public Checkout(Page page, CheckoutDto checkoutDto) 
            : base(page)
        {
            if (checkoutDto == null) throw new ArgumentNullException(nameof(checkoutDto));
            _checkoutDto = checkoutDto;

            TemplateName = "_GTM-checkout.cshtml";
        }

        protected override object GetDto()
        {
            return _checkoutDto;
        }
    }
}
