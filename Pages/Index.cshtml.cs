/* Author: Madhan KAMALAKANNAN  
 Created : Created 29/Aug/2021
 Modified: 12/Dec/2021  
 */

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
    public class IndexModel : PageModel
    {
        private readonly OnlineShoppingCart.DB.AsnetidentityContext _context;

        public IndexModel(OnlineShoppingCart.DB.AsnetidentityContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
            
        }
    }
}
