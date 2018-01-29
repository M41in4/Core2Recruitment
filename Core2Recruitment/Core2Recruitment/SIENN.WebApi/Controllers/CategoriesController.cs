using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SIENN.Services.DTO;
using SIENN.Services.Managers;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesManager _categoriesManager;

        public CategoriesController(ICategoriesManager categoriesManager)
        {
            _categoriesManager = categoriesManager;
        }

        [HttpGet, ProducesResponseType(typeof(IEnumerable<CategoryDTO>), 200)]
        public IActionResult GetCategories()
        {
            var categories = _categoriesManager.GetCategories();
            return new ObjectResult(categories);
        }

        [HttpGet, Route("{id}"), ProducesResponseType(typeof(CategoryDTO), 200)]
        public IActionResult GetCategory(int id)
        {
            var category = _categoriesManager.GetCategory(id);
            return category != null ? (IActionResult) new ObjectResult(category) : new NotFoundResult();
        }

        [HttpPost, ProducesResponseType(typeof(int), 200)]
        public IActionResult SaveCategory([FromBody] CategoryDTO category)
        {
            var id = _categoriesManager.Save(category);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveCategory(int id)
        {
            _categoriesManager.DeleteCategoryById(id);
            return Ok(id);
        }
    }
}