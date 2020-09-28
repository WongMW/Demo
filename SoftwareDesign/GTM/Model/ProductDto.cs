using System;

namespace SoftwareDesign.GTM.Model
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public decimal? Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Variant { get; set; }
        public string List { get; set; }
        public int Position { get; set; }

        public Decimal Quantity { get; set; }

        public string Currency { get; set; }

        public string GetPrice()
        {
            return Price.HasValue ? Price.Value.ToString("0.00") : string.Empty;
        }
    }
}