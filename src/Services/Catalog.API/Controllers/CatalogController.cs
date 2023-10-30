using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductBy(string id) 
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogError($"Product with id {id} , not fornd.");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("GetProductsByCategory/{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var product = await _repository.GetProductsByCategoryAsync(category);
            if (product == null)
            {
                _logger.LogError($"Product with category {category} , not fornd.");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]Product product)
        {
            await _repository.CreateProductAsync(product);

            return CreatedAtRoute("GetProductById", new {id = product.Id}, product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
           var response = await _repository.UpdateProductAsync(product);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var response = await _repository.DeleteProductAsync(id);
            return Ok(response);
        }
    }
}
