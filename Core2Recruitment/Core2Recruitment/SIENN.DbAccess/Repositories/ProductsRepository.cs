using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Entities;

namespace SIENN.DbAccess.Repositories
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetAvailableProducts(int take, int skip);
        IEnumerable<Product> GetProducts(int? productType, int? category);
        int CountOfAvailableProducts();
        Product GetProductById(int id);
        IEnumerable<Product> GetUnavailableProductsInCurrentMonth();
    }

    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        public ProductsRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetAvailableProducts(int take, int skip)
        {
            return Entities.AsNoTracking()
                           .Include(x => x.Unit).Include(x => x.Type).Include(x => x.Categories)
                           .Where(x => x.IsAvailable)
                           .OrderBy(x => x.Id)
                           .Skip(skip)
                           .Take(take);
        }

        public IEnumerable<Product> GetProducts(int? productType, int? category)
        {
            var products = Entities.Include(x => x.Unit).Include(x => x.Type).Include(x => x.Categories)
                                   .Where(x => x.IsAvailable);
            if (productType.HasValue)
            {
                products = products.Where(x => x.TypeId == productType);
            }

            if (category.HasValue)
            {
                products = products.Where(p => p.Categories.Any(x => x.CategoryId == category));
            }

            return products.ToList();
        }

        public int CountOfAvailableProducts()
        {
            return Find(x => x.IsAvailable).Count();
        }

        public Product GetProductById(int id)
        {
            return Entities.Include(x => x.Unit).Include(x => x.Type).Include(x => x.Categories)
                           .SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetUnavailableProductsInCurrentMonth()
        {
            return Context.Set<Product>().FromSql("EXEC GetUnavailableProductsInCurrentMonth");
        }
    }
}