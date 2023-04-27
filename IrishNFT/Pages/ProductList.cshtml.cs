using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IrishNFT.Models;
using IrishNFT.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IrishNFT.Pages
{
    public class ProductListModel : PageModel
    {
        private readonly IProductService _productService;
        public ProductListModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<Product> Products { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public async Task OnGetAsync(int? id)
        {
            Categories = await _productService.GetAllCategories();

            if (id == null)
                Products = await _productService.GetAllProducts();
            else
                Products = await _productService.GetProductsByCategoryId((int) id);

             Products.ForEach(p => { p.Category = Categories.Where(c => c?.CategoryID == p?.CategoryID).SingleOrDefault(); });
           Products = Products.Where(p => p.Status == (int)Status.Available).ToList();
        }

        public IActionResult AddToCart(int? productId)
        {
            return RedirectToPage("");
        }
        public async Task OnPostAsync() { }

        public void SortByCategory(int categoryId)
        {
            Products = Products.Where(p => p.CategoryID == categoryId).ToList();
        }
        
       public void AddToCart(Product p)
        {

        }


    }
}
