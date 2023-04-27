using IrishNFT.Models;

namespace IrishNFT.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<List<Category>> GetAllCategories();
        Task<Product> GetProductById(int id);
        Task SetProductStatus(int productId, Status status);
        Task<bool> IsProductAvailable(int productId);
        Task<Category> GetCategoryById(int id);
        Task<List<Product>> GetProductsByCategoryId(int categoryId);

    }
}
