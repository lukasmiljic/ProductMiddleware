using Microsoft.AspNetCore.Mvc;
using ProductMiddleware.Models;
using ProductMiddleware.Services;

namespace ProductMiddleware.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productService.GetProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<Product>> SearchProducts([FromQuery] string query)
        {
            return await _productService.SearchProductsAsync(query);
        }

        [HttpGet("filter")]
        public async Task<IEnumerable<Product>> FilterProducts([FromQuery] string category, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            return await _productService.GetProductsByFilterAsync(category, minPrice, maxPrice);
        }
    }
}