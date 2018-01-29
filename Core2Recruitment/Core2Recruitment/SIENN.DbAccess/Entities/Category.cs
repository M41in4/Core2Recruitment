using System.Collections.Generic;

namespace SIENN.DbAccess.Entities
{
    public class Category : Entity
    {
        public ISet<ProductCategory> Products { get; set; } = new HashSet<ProductCategory>();
    }
}