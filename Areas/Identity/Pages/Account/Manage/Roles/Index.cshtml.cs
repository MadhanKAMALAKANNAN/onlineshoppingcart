using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;

namespace OnlineShoppingCart.Areas.Identity.Pages.Account.Manage.Roles
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        // private readonly OnlineShoppingCart.DB.AsnetidentityContext _context;
        private readonly AsnetidentityContext _context;
        public IndexModel(AsnetidentityContext context)
        {
            _context = context;
        }
        public IList<Microsoft.AspNetCore.Identity.IdentityRole> AspNetRoles1 { get; set; }
        public IList<AspNetRoles> AspNetRoles { get;set; }
 
        public async Task OnGetAsync()        
        {
            AspNetRoles1 = await _context.Roles.ToListAsync();
        }
    }
}
