using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareDesign.GTM.Model
{
    public class CheckoutDto
    {
        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();

        public int StepNumber { get; set; }
        public string Currency { get; set; }
        public string CallbackUrl { get; set; }
    }
}