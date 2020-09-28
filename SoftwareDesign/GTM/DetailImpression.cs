using System;
using System.Web.UI;
using SoftwareDesign.GTM.Model;

namespace SoftwareDesign.GTM
{
   public class DetailImpression :GtmBase
    {
        private readonly ProductDto _productDto;

        public DetailImpression(Page page, ProductDto productDto) 
            : base(page)
        {
            if (productDto == null) throw new ArgumentNullException(nameof(productDto));
            _productDto = productDto;

            TemplateName = "_GTM-detail-impression.cshtml";
        }

        protected override object GetDto()
        {
            return _productDto;
        }
    }
}
