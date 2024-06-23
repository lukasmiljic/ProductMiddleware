using System.Text.Json;
using ProductMiddleware.Models;

namespace ProductMiddleware.Services
{
    public class RestProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public RestProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://dummyjson.com/products");
            var productsResponse = JsonSerializer.Deserialize<ProductsResponse>(response);
            return productsResponse.Products;
        }
        private class ProductsResponse
        {
            public List<Product> Products { get; set; }
        }
    }
}