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
    public class IndexModel : PageModel
    {
 
        private readonly AsnetidentityContext _context;
        public IndexModel(AsnetidentityContext context)
        {
            _context = context;
         
        }
        public IList<IdentityUserRole<string>> aspNetUSerRoles1 { get; set; }
        public IList<IdentityUser> aspNetUSers { get; set; }
        public IList<IdentityRole> aspNetRoles { get;set; }

        public async Task LoadData()
        {
            aspNetUSerRoles1 = await _context.UserRoles.ToListAsync();

            aspNetUSers = await _context.Users.ToListAsync();

            aspNetRoles = await _context.Roles.ToListAsync();

        }
    public async Task OnGetAsync()  
        {
            LoadData().Wait();     
        }
        public void OnGetProfile(int attendeeId)
        {
            ViewData["AttendeeId"] = attendeeId;

            // code omitted for brevity
        }
        [HttpGet]
        public async Task<IActionResult> OnGetDelete(string userId, string roleId)
        {
            IdentityUserRole<string> uroles = _context.UserRoles.Where(ur => ur.UserId == userId && ur.RoleId == roleId).FirstOrDefault();
           // if (uroles != null) { _context.UserRoles.Remove(uroles);   _context.SaveChanges(); }
           

            LoadData().Wait();

            return Page();
        }
    }
}
