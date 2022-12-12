using System;
using System.Collections.Generic;
using System.Data;
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
    public class DeleteModel : PageModel
    {
        //private readonly OnlineShoppingCart.DB.AsnetidentityContext _context;

        //public DeleteModel(OnlineShoppingCart.DB.AsnetidentityContext context)
        //{
        //    _context = context;
        //}

        //[BindProperty]
        //public AspNetRoles AspNetRoles { get; set; }
        private readonly AsnetidentityContext _context;
        public DeleteModel(AsnetidentityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Microsoft.AspNetCore.Identity.IdentityRole AspNetRoles { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetRoles = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);

            if (AspNetRoles == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetRoles = await _context.Roles.FindAsync(id);

            if (AspNetRoles != null)
            {
                _context.Roles.Remove(AspNetRoles);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
