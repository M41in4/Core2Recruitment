using System.Collections.Generic;
using System.Linq;
using SIENN.DbAccess;
using SIENN.DbAccess.Entities;
using SIENN.Services.DTO;
using SIENN.Services.Mapping;

namespace SIENN.Services.Managers
{
    public interface ICategoriesManager {
        IEnumerable<CategoryDTO> GetCategories();
        CategoryDTO GetCategory(int id);
        int Save(CategoryDTO category);
        void DeleteCategoryById(int id);
    }
    public class CategoriesManager : Manager, ICategoriesManager
    {
        public CategoriesManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return UnitOfWork.CategoriesRepository.GetAll().Select(Mapper.Map<CategoryDTO>).ToList();
        }

        public CategoryDTO GetCategory(int id)
        {
            var category = UnitOfWork.CategoriesRepository.Get(id);
            if (category == null) return null;
            return Mapper.Map<CategoryDTO>(category);
        }

        public int Save(CategoryDTO category)
        {
            return Save<Category>(category);
        }

        public void DeleteCategoryById(int id)
        {
            Delete<Category>(id);
        }
    }
}