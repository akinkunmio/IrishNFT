using IrishNFT.Models;
using IrishNFT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IrishNFT.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IProductService _productService;

        public ProductDetailsModel(IProductService productService)
        {
            _productService = productService;
        }
        public Product Product { get; set; } = new Product();
        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task OnGetAsync(int id)
        {
            Categories = await _productService.GetAllCategories();

            Product = await _productService.GetProductById(id);

            Product.Category = Categories.Where(c => c?.CategoryID == Product?.CategoryID).SingleOrDefault();

        }
    }
}
