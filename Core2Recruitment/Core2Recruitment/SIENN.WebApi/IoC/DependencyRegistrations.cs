using Microsoft.Extensions.DependencyInjection;
using SIENN.DbAccess;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Managers;
using SIENN.Services.Mapping;

namespace SIENN.WebApi.IoC
{
    public static class DependencyRegistrations
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IUnitsRepository, UnitsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ITypesRepository, TypesRepository>();
        }

        public static void RegisterManager(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, Mapper>();
            services.AddScoped<IUnitsManager, UnitsManager>();
            services.AddScoped<ICategoriesManager, CategoriesManager>();
            services.AddScoped<IProductsManager, ProductsManager>();
        }
    }
}