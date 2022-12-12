using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;

namespace OnlineShoppingCart.Areas.Identity.Pages.Account.Manage.Roles
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        //private readonly OnlineShoppingCart.DB.AsnetidentityContext _context;

        //public EditModel(OnlineShoppingCart.DB.AsnetidentityContext context)
        //{
        //    _context = context;
        //}
        private readonly AsnetidentityContext _context;
        public EditModel(AsnetidentityContext context)
        {
            _context = context;
        }
        //public IList<Microsoft.AspNetCore.Identity.IdentityRole> AspNetRoles1 { get; set; }

        [BindProperty]
        public Microsoft.AspNetCore.Identity.IdentityRole AspNetRoles { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //AspNetRoles = await _context.AspNetRoles.FirstOrDefaultAsync(m => m.Id == id);
            AspNetRoles = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);

            if (AspNetRoles == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AspNetRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetRolesExists(AspNetRoles.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AspNetRolesExists(string id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
