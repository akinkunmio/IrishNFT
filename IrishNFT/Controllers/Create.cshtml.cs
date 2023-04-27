using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IrishNFT.Data;
using IrishNFT.Models;

namespace IrishNFT.Controllers
{
    public class CreateModel : PageModel
    {
        private readonly IrishNFT.Data.ApplicationDbContext _context;

        public CreateModel(IrishNFT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryID");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Product == null || Product == null)
            {
                return Page();
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
