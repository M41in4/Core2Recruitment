using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Edm.Validation;
using Microsoft.Extensions.DependencyInjection;
using SIENN.DbAccess;
using SIENN.DbAccess.Entities;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Managers;
using Type = SIENN.DbAccess.Entities.Type;

namespace SIENN.WebApi
{
    public static class Helpers
    {
        public static void SeedDatabase(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using(var scope = scopeFactory.CreateScope())
            using (var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>())
            {
                if (unitOfWork.ProductsRepository.Count() > 0) return;
                var units = new List<Unit>
                {
                    new Unit() {Code = "pc", Description = "Piece"},
                    new Unit() {Code = "gr", Description = "Gram"},
                    new Unit() {Code = "lit", Description = "Liter"}
                };
                unitOfWork.UnitsRepository.AddRange(units);

                var types = new List<Type>
                {
                    new Type() {Code = "FD", Description = "Food"},
                    new Type() {Code = "DR", Description = "Drinks"},
                    new Type() {Code = "TO", Description = "Toys"}
                };
                unitOfWork.TypesRepository.AddRange(types);

                var categories = new List<Category>()
                {
                    new Category() {Code = "CAT01", Description = "Category 01"},
                    new Category() {Code = "CAT02", Description = "Category 02"},
                    new Category() {Code = "CAT03", Description = "Category 03"},
                    new Category() {Code = "CAT04", Description = "Category 04"},
                };
                unitOfWork.CategoriesRepository.AddRange(categories);

                var randomizer = new Random(DateTime.Now.Millisecond);
                var products = Enumerable.Range(0, 1000).Select(x =>
                {
                    var product = new Product()
                    {
                        Unit = units[randomizer.Next(0, units.Count - 1)],
                        Type = types[randomizer.Next(0, types.Count - 1)],
                        IsAvailable = randomizer.Next(0, 3) != 0,
                        Code = x.ToString("0000"),
                        Description = $"Product {x}",
                        Price = (decimal) Math.Round(100 * randomizer.NextDouble(), 2),
                        DeliveryDate = DateTime.Now.AddDays(randomizer.Next(0, 120)),
                    };

                    var productCategories = Enumerable.Range(0, randomizer.Next(0, categories.Count - 1)).Select(i => new ProductCategory()
                    {
                        Product = product,
                        Category = categories[i]
                    });
                    product.Categories = new HashSet<ProductCategory>(productCategories);

                    return product;
                });

                unitOfWork.ProductsRepository.AddRange(products);

                unitOfWork.SaveChanges();
            }
        }
    }
}