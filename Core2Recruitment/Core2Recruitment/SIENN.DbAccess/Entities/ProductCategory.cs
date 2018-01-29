namespace SIENN.DbAccess.Entities
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        protected bool Equals(ProductCategory other)
        {
            return ProductId == other.ProductId && CategoryId == other.CategoryId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProductCategory) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ProductId * 397) ^ CategoryId;
            }
        }

        public static bool operator ==(ProductCategory left, ProductCategory right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProductCategory left, ProductCategory right)
        {
            return !Equals(left, right);
        }
    }
}