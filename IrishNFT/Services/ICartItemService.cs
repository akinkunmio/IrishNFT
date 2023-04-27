using IrishNFT.Models;

namespace IrishNFT.Services
{
    public interface ICartItemService
    {
        Task<List<CartItem>> GetCartItemsByUsername(string username);
        Task AddCartItem(int productId, string username);
        Task DeleteCartItemId(int id);
        Task<CartItem> GetyCartItemById(int id);
    }
}
