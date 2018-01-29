using System.Collections.Generic;

namespace SIENN.DbAccess.Entities
{
    public class Type : Entity
    {
        public ISet<Product> Products { get; set; } = new HashSet<Product>();
    }
}