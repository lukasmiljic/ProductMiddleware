using System.Text.Json;
using ProductMiddleware.Models;

namespace ProductMiddleware.Services
{
    public class RestProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://dummyjson.com/products";

        public RestProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetStringAsync(_baseUrl);
            var productsResponse = JsonSerializer.Deserialize<ProductsResponse>(response);
            return productsResponse.Products;
        }
        private class ProductsResponse
        {
            public List<Product> Products { get; set; }
        }
    }
}