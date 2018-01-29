using System;
using SIENN.DbAccess.Entities;
using SIENN.DbAccess.Repositories;

namespace SIENN.DbAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IProductsRepository ProductsRepository { get; }
        ITypesRepository TypesRepository { get; }
        IUnitsRepository UnitsRepository { get; }
        ICategoriesRepository CategoriesRepository { get; }

        int SaveChanges();
        IGenericRepository<TEntity> GetRepository<TEntity>(TEntity entity) where TEntity : Entity;
    }
}