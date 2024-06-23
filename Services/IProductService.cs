using ProductMiddleware.Models;

namespace ProductMiddleware.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByFilterAsync(string category, decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<Product>> SearchProductsAsync(string query);
    }
}