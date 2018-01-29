using System;

namespace SIENN.Services.DTO
{
    public class ProductDTO : DTO
    {
        public string ProductDescription { get; set; }
        public double Price { get; set; }
        public string IsAvailable { get; set; }
        public string DeliveryDate { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public int UnitId { get; set; }
        public string Unit { get; set; }

        public int CategoryCount { get; set; }
    }
}