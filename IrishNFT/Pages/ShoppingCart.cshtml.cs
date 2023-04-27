using IrishNFT.Models;
using IrishNFT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IrishNFT.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly ICartItemService _cartItemService;
        private readonly IProductService _productService;
        public ShoppingCartModel(ICartItemService cartItemService, IProductService productService)
        {
            _cartItemService = cartItemService;
            _productService = productService;
        }
        public string Error { get; set; }
        public double Total { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<CartItem> CartItems { get; set; } = default!;
        public async Task OnGet(int? id = null)
        {
            string username = HttpContext?.User?.Identity?.Name;

            if(id != null)
            {
                bool isAvailable = await _productService.IsProductAvailable((int)id);
                if (isAvailable)
                {
                    await _cartItemService.AddCartItem((int)id, username);
                    await _productService.SetProductStatus((int) id, Status.NotAvailable);
                }
            }

            Categories = await _productService.GetAllCategories();

            CartItems = await _cartItemService.GetCartItemsByUsername(username) ?? new List<CartItem>();
            foreach(var cartItem in CartItems)
            {
                cartItem.Product = await _productService.GetProductById(cartItem.ProductId);
            }

            Total = CartItems.Sum(c => (double) c.Product?.UnitPrice);
        }

        public async Task<IActionResult> OnPost(int? id = null)
        {
            if(id != null)
            {
                var cart = await _cartItemService.GetyCartItemById((int)id);
                await _cartItemService.DeleteCartItemId((int)id);
                await _productService.SetProductStatus((int)cart.ProductId, Status.Available);
            }

            return RedirectToPage("/ShoppingCart");
        }

       
    }
}
