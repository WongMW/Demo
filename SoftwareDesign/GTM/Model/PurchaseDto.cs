using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareDesign.GTM.Model
{
    public class PurchaseDto
    {
        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();

        public string TransactionId { get; set; }
        public string Affiliattion { get; set; }
        public Decimal Revenue { get; set; }
        public Decimal Tax { get; set; }
        public Decimal Shipping { get; set; }
        public string Coupon { get; set; }

        public String Currency { get; set; }    
    }
}