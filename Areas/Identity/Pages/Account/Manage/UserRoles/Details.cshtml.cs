using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;

namespace OnlineShoppingCart.Areas.Identity.Pages.Account.Manage.UserRoles
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        //private readonly OnlineShoppingCart.DB.AsnetidentityContext _context;

        //public DetailsModel(OnlineShoppingCart.DB.AsnetidentityContext context)
        //{
        //    _context = context;
        //}
        private readonly AsnetidentityContext _context;
        public DetailsModel(AsnetidentityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public  IdentityUserRole<string> aspNetUserRoles { get; set; }    
        public IdentityUser aspNetUSers { get; set; }
        public IdentityRole aspNetRoles { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return Page();
            }
             
            aspNetUserRoles = await _context.UserRoles.FirstOrDefaultAsync(m => m.UserId == id);

            aspNetUSers = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            aspNetRoles = aspNetUserRoles!=null?await _context.Roles.FirstOrDefaultAsync(m => m.Id == aspNetUserRoles.RoleId):null;

            if (aspNetUserRoles == null)
            {
                return Page();
            }
            return Page();
        }
    }
}
