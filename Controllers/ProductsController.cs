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

        /// <summary>
        /// Retrieves all the products
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productService.GetProductsAsync();
        }

        /// <summary>
        /// Retrieves product by id
        /// </summary>
        /// <param name="id">Product id</param>
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

        /// <summary>
        /// Searches products by query
        /// </summary>
        /// <param name="query">Search query</param>
        [HttpGet("search")]
        public async Task<IEnumerable<Product>> SearchProducts([FromQuery] string query)
        {
            return await _productService.SearchProductsAsync(query);
        }

        /// <summary>
        /// Filters products by category, min price and max price
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="minPrice">Minimum price</param>
        /// <param name="maxPrice">Maximum price</param>
        [HttpGet("filter")]
        public async Task<IEnumerable<Product>> FilterProducts([FromQuery] string category, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            return await _productService.GetProductsByFilterAsync(category, minPrice, maxPrice);
        }
    }
}