using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SIENN.Services.DTO;
using SIENN.Services.Managers;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductTypesController : Controller
    {
        private readonly IProductsManager _productsManager;

        public ProductTypesController(IProductsManager productsManager)
        {
            _productsManager = productsManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductTypeDTO>), 200)]
        public IActionResult GetProductTypes()
        {
            var types = _productsManager.GetProductTypes();
            return Ok(types);
        }

        [HttpGet("{id}"), ProducesResponseType(typeof(ProductTypeDTO), 200)]
        public IActionResult GetProductType(int id)
        {
            var type = _productsManager.GetProductType(id);
            return type != null ? (IActionResult) Ok(type) : NotFound();
        }

        [HttpPost, ProducesResponseType(typeof(int), 200)]
        public IActionResult Save([FromBody] ProductTypeDTO productType)
        {
            var id = _productsManager.Save(productType);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productsManager.DeleteProductTypeById(id);
            return Ok(id);
        }
    }
}