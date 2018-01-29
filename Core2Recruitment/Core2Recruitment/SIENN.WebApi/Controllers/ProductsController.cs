using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Entities;
using SIENN.Services.DTO;
using SIENN.Services.Managers;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsManager _productsManager;

        public ProductsController(IProductsManager productsManager)
        {
            _productsManager = productsManager;
        }

        [HttpGet, ProducesResponseType(typeof(PaginationResult<ProductDTO>), 200)]
        public IActionResult GetAvailableProducts(int? page = 1, int? pageSize = 100)
        {
            var result = _productsManager.GetAvailableProducts(page.GetValueOrDefault(), pageSize.GetValueOrDefault());
            return Ok(result);
        }

        [HttpGet("{id}"), ProducesResponseType(typeof(ProductDTO), 200)]
        public IActionResult GetProduct(int id)
        {
            var product = _productsManager.GetProductById(id);
            return product != null ? (IActionResult) Ok(product) : NotFound();
        }

        [HttpPost]
        public IActionResult SaveProduct([FromBody] Product product)
        {
            var id = _productsManager.Save(product);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _productsManager.DeleteProductById(id);
            return Ok(id);
        }

        [HttpGet("Search"), ProducesResponseType(typeof(IEnumerable<ProductDTO>), 200)]
        public IActionResult FilterProducts(int? type, int? category)
        {
            var products = _productsManager.SearchProducts(type, category);
            return Ok(products);
        }

        [HttpGet("UnavailableForDelivery"), ProducesResponseType(typeof(IEnumerable<ProductDTO>), 200)]
        public IActionResult FilterProducts()
        {
            var products = _productsManager.GetUnavailableProductsForDelivery();
            return Ok(products);
        }
    }
}