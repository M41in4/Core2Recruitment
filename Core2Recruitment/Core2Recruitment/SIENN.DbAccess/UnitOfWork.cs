using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Entities;
using SIENN.DbAccess.Repositories;

namespace SIENN.DbAccess
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public UnitOfWork(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Type> Types { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public IProductsRepository ProductsRepository => new ProductsRepository(this);
        public ITypesRepository TypesRepository => new TypesRepository(this);
        public IUnitsRepository UnitsRepository => new UnitsRepository(this);
        public ICategoriesRepository CategoriesRepository => new CategoriesRepository(this);

        public IGenericRepository<TEntity> GetRepository<TEntity>(TEntity entity) where TEntity : Entity
        {
            return new GenericRepository<TEntity>(this);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var products = modelBuilder.Entity<Product>();
            products.HasMany(x => x.Categories).WithOne(x => x.Product);
            products.Property(x => x.Price).HasColumnType("money");

            var productCategory = modelBuilder.Entity<ProductCategory>();
            productCategory.HasKey(x => new {x.ProductId, x.CategoryId});
        }
    }
}