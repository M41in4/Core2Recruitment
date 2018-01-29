using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Entities;

namespace SIENN.DbAccess.Repositories
{
    public interface IUnitsRepository : IGenericRepository<Unit> { }

    public class UnitsRepository : GenericRepository<Unit>, IUnitsRepository
    {
        public UnitsRepository(DbContext context) : base(context)
        {
        }
    }
}