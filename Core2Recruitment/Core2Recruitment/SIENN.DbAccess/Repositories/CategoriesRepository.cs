using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Entities;

namespace SIENN.DbAccess.Repositories
{
    public interface ICategoriesRepository : IGenericRepository<Category> { }

    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(DbContext context) : base(context)
        {
        }
    }
}