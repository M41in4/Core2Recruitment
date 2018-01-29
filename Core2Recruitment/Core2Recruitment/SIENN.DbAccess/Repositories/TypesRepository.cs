using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Entities;

namespace SIENN.DbAccess.Repositories
{
    public interface ITypesRepository : IGenericRepository<Type> { }

    public class TypesRepository : GenericRepository<Type>, ITypesRepository
    {
        public TypesRepository(DbContext context) : base(context)
        {
        }
    }
}