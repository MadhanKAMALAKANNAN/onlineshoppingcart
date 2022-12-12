using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;

namespace OnlineShoppingCart.Areas.Identity.Pages.Account.Manage.UserRoles
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
    
        private readonly AsnetidentityContext _context;
        public DbSet<IdentityRole> aspnetRoles;
        public DbSet<IdentityUser> aspnetUsers;
        public EditModel(AsnetidentityContext context)
{
            _context = context;
            aspnetRoles = _context.Roles;
            aspnetUsers = _context.Users;
        }
        [BindProperty]
        public string SelectedUser { get; set; }
        [BindProperty]
        public IEnumerable<string> SelectedRole { get; set; }


        [BindProperty]
        public IdentityUser aspNetUSer { get; set; }
        [BindProperty]
        public IdentityRole aspNetRole { get; set; }

        [BindProperty]
        public Microsoft.AspNetCore.Identity.IdentityUserRole<string> aspNetUserRole { get; set; }

        [BindProperty]
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Users { get; set; }

        [BindProperty]
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {  
            
           aspNetUserRole = await _context.UserRoles.FirstOrDefaultAsync(m => m.UserId == id);
            aspNetUSer = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            aspNetRole = aspNetUserRole != null ? await _context.Roles.FirstOrDefaultAsync(m => m.Id == aspNetUserRole.RoleId) : null;

            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> userLst = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            foreach (var user in aspnetUsers)
            {
                var t = new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem();
                t.Text = user.Email;
                t.Value = user.Id;
                userLst.Add(t);
                SelectedUser = t.Value;
            }
            Users = userLst;


            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> roleLst = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            List<string> selectedRole = new List<string>();
            foreach (var role in aspnetRoles)
            {
                var t = new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem();
                t.Text = role.Name;
                t.Value = role.Id;
                roleLst.Add(t);
                selectedRole.Append(t.Value);
            }
            SelectedRole = selectedRole;
            Roles = roleLst;

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
            
         IEnumerable<IdentityUserRole<string>> identityUserRole = await _context.UserRoles.Where(i=>i.UserId== SelectedUser).ToListAsync();  

            try
            {    if (identityUserRole != null)
                {
                    _context.UserRoles.RemoveRange(identityUserRole);
                    await _context.SaveChangesAsync();


                    foreach (var vid in SelectedRole)
                    {
                        IdentityUserRole<string> nUsrRole = new IdentityUserRole<string>();
                        nUsrRole.RoleId = vid;
                        nUsrRole.UserId = SelectedUser;
                        _context.UserRoles.Add(nUsrRole);
                        await _context.SaveChangesAsync();
                    }     
                }
            }
            catch (DbUpdateConcurrencyException)
            {
               // if (!AspNetRolesExists(aspNetUserRoles.RoleId))
                {
                    return NotFound();
                }
              //  else
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
