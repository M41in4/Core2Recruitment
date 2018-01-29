using System;
using System.Collections.Generic;

namespace SIENN.DbAccess.Entities
{
    public class Product: Entity
    {
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }

        public ISet<ProductCategory> Categories { get; set; } = new HashSet<ProductCategory>();

        public int UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}