using Microsoft.AspNetCore.Mvc;
using ProductMiddleware.Models;
using ProductMiddleware.Services;
using Swashbuckle.AspNetCore.Annotations;

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
        /// <response code="200">Returns the list of products</response>
        /// <response code="404">No products found</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            if (!products.Any())
            {
                return NotFound("No products found");
            }
            return Ok(products);
        }

        /// <summary>
        /// Retrieves product by id
        /// </summary>
        /// <response code="200">Returns the requested product</response>
        /// <response code="404">If no product is found with the provided id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound($"Product with id {id} not found");
            }

            return Ok(product);
        }

        /// <summary>
        /// Searches products by query
        /// </summary>
        /// <param name="query">Search query</param>
        /// <response code="200">Returns the filtered products</response>
        /// <response code="404">No products found matching the criteria</response>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string query)
        {
            var filteredProducts = await _productService.SearchProductsAsync(query);
            if (!filteredProducts.Any())
            {
                return NotFound($"No products found matching the query {query}");
            }
            return Ok(filteredProducts);
        }

        /// <summary>
        /// Filters products by category, min price and max price
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="minPrice">Minimum price</param>
        /// <param name="maxPrice">Maximum price</param>
        /// <response code="200">Returns the filtered products</response>
        /// <response code="404">No products found matching the criteria</response>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 60, VaryByQueryKeys = ["category", "minPrice", "maxPrice"], Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<IEnumerable<Product>>> FilterProducts([FromQuery] string category, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var FilterProducts = await _productService.GetProductsByFilterAsync(category, minPrice, maxPrice);
            if (!FilterProducts.Any())
            {
                return NotFound("No products found matching the query");
            }
            return Ok(FilterProducts);
        }
    }
}

