using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;

namespace OnlineShoppingCart.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly OnlineShoppingCart.DB.AsnetidentityContext _context;

        public ProductDetailsModel(OnlineShoppingCart.DB.AsnetidentityContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
