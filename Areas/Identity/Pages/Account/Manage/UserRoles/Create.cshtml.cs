using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShoppingCart.Areas.Identity.Pages.Account.Manage.UserRoles
{
    [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AsnetidentityContext _context;
        public DbSet<IdentityRole> aspnetRoles;
        public DbSet<IdentityUser> aspnetUsers;
        public CreateModel(AsnetidentityContext context)
        {
            _context = context;
//ViewData["aspnetusers"] = _context.Roles.ToList();//  new System.Web.Mvc.SelectList(_context.Roles.ToList()).;//, "DataValueField", "DataTextField");
            aspnetRoles = _context.Roles;
            aspnetUsers = _context.Users;
        }

        [Display(Name = "Users")]
        public string SelectedUser{ get; set; }
        [Display(Name = "Roles")]
        public IEnumerable<string> SelectedRole { get; set; }
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Users { get; set; }
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Roles { get; set; }

        //[BindProperty]
        //public Microsoft.AspNetCore.Identity.IdentityUserRole<string> AspNetUserRoles { get; set; }
        [BindProperty]
        public IdentityUserRole<string> aspNetUserRoles { get; set; }

        public IActionResult OnGet()
{
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> userLst = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            foreach (var user in  aspnetUsers)
            {
                var t = new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem();
                t.Text = user.Email;
                t.Value = user.Id;
                userLst.Add(t);
                SelectedUser = t.Value;
            }
            Users = userLst;
             

            List< Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> roleLst = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
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

  
        [HttpPost("PostAMethod")]
        public void OnPostAAsync(string id)
        {

        }
    
        public async Task<RedirectToPageResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                RedirectToPage("./Index");
            }
            else
            {
                var rls=   aspNetUserRoles.RoleId+"";
                if (rls.Trim().EndsWith(",")&& (rls.Length - 1 >0))
                {
                    rls = rls.Remove(rls.Length - 1);
                }

                string[] rolsArr = rls.Split(",");

                if (_context.UserRoles.Where(x => x.UserId == aspNetUserRoles.UserId).Count() > 0) // && x.RoleId == aspNetUserRoles.RoleId).Count() == 0)//Not Exist
                {
                    var userRoles = _context.UserRoles.Where(x => x.UserId == aspNetUserRoles.UserId).ToList();
                    foreach(var usr in userRoles)
                    {
                        foreach (string str in rolsArr)
                        {
                            if(usr.RoleId == str)
                            {
                                _context.Remove(usr);
                                await _context.SaveChangesAsync();//to be tested

                                aspNetUserRoles.RoleId = str;
                                await _context.AddAsync(aspNetUserRoles);
                                await _context.SaveChangesAsync();
                            }
                          
                        }
                        _context.Remove(usr);
                      
                    }
                   // IdentityUserRole<string> usrRole = _context.UserRoles.SelectMany(x => x.UserId == aspNetUserRoles.UserId) && x.RoleId == aspNetUserRoles.RoleId)
                   
                }
                else
                {
                    foreach(string str in rolsArr)
                    {
                        aspNetUserRoles.RoleId = str;
                        await _context.AddAsync(aspNetUserRoles);
                        await _context.SaveChangesAsync();
                    }
                }
            
                   


                //else //Exist
                //{
                //    _context.Remove(aspNetUserRoles);
                //   // _context.Remove(aspNetUserRoles);
                //    await _context.SaveChangesAsync();
                // //   _context.Add(aspNetUserRoles);
                //   // _context.Update(aspNetUserRoles);
                //    await _context.SaveChangesAsync();
                //}

            }
            return  RedirectToPage("Index");
        }
    }
}
