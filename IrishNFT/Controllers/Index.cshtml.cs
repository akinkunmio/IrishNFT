using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IrishNFT.Data;
using IrishNFT.Models;

namespace IrishNFT.Controllers
{
    public class IndexModel : PageModel
    {
        private readonly IrishNFT.Data.ApplicationDbContext _context;

        public IndexModel(IrishNFT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Product != null)
            {
                Product = await _context.Product
                .Include(p => p.Category).ToListAsync();
            }
        }
    }
}
