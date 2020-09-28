using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using SoftwareDesign.GTM.Model;

namespace SoftwareDesign.GTM
{
    public class GtmPurchase : GtmBase
    {
        private readonly PurchaseDto _purchaseDto;

        public GtmPurchase(Page page, PurchaseDto purchaseDto) : base(page)
        {
            if (purchaseDto == null) throw new ArgumentNullException(nameof(purchaseDto));
            _purchaseDto = purchaseDto;

            TemplateName = "_GTM-purchase.cshtml";
        }

        protected override object GetDto()
        {
            return _purchaseDto;
        }
    }
}
