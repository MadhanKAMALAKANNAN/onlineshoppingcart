using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;

namespace OnlineShoppingCart.Areas.Identity.Pages.Account.Manage.Roles
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AsnetidentityContext _context;
        public CreateModel(AsnetidentityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Microsoft.AspNetCore.Identity.IdentityRole AspNetRoles { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }



        //[BindProperty]
        //public AspNetRoles AspNetRoles { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.AspNetRoles.Add(AspNetRoles);
            _context.Roles.Add(AspNetRoles);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
