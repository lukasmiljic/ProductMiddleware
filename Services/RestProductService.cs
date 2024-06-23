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
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"{_baseUrl}/{id}");
            return JsonSerializer.Deserialize<Product>(response);
        }
        public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
        {
            var products = await GetProductsAsync();
            return products.Where(p => p.Title.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<IEnumerable<Product>> FilterProductsAsync(string category, decimal? minPrice, decimal? maxPrice)
        {
            var products = await GetProductsAsync();
            return products.Where(p =>
                (string.IsNullOrEmpty(category) || p.Category == category) &&
                (!minPrice.HasValue || p.Price >= minPrice) &&
                (!maxPrice.HasValue || p.Price <= maxPrice));
        }

        private class ProductsResponse
        {
            public List<Product> Products { get; set; }
        }
    }
}