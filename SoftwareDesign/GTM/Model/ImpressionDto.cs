using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesign.GTM.Model
{
    public class ImpressionDto
    {
        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
        public string Currency { get; set; }
    }
}
