/* Author: Madhan KAMALAKANNAN   , Madhan.KAMALAKANNAN@outlook.com
 Created : Created 29/Aug/2021
 Modified:  /Dec/2021  
 */


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingCart.Controllers
{

    //[Authorize(Roles = "Admin")]
    //[AllowAnonymous]
    public class AdminController :Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AsnetidentityContext _context;

        public DbSet<IdentityRole> aspnetRoles { get; set; }
        public DbSet<IdentityUser> aspnetUsers { get; set; }
        public DbSet<IdentityUserRole<string>> aspnetUserRoles { get; set; }
        public AdminController(AsnetidentityContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            return View();
        }
        // GET: AdminController/GetRoles
        [ValidateAntiForgeryToken]
        public ActionResult<IList> GetUserRoles() 
{
            aspnetUserRoles = _context.UserRoles;
            return aspnetUserRoles != null ? aspnetUserRoles.ToList() : null;   
        }
        [ValidateAntiForgeryToken]
        public ActionResult Details(int roleId,string userId)
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
 
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            //return View();
            return RedirectToAction(nameof(Index));
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
  
        [HttpGet]
        public ActionResult DeleteGUser12(string id,string p2)
        {
            return RedirectToAction(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        [HttpGet]
        public async Task<IdentityUserRole<string>> Delete(string userId, string roleId)
        {
            IdentityUserRole<string> uroles = await _context.UserRoles.Where(ur => ur.UserId == userId && ur.RoleId == roleId).FirstOrDefaultAsync();
            if (uroles != null) { _context.UserRoles.Remove(uroles);  _context.SaveChanges(); }
            return  uroles;/*//  // RedirectToAction(nameof(Index)); */
        }

        [ValidateAntiForgeryToken]
        [HttpGet]
        public async Task<IdentityRole<string>> DeleteRole(string roleId)
        {
            IdentityRole uroles = null;
            if( _context.UserRoles.Where(ur =>   ur.RoleId == roleId).Count()==0)//if userrole not exist then delete
            {
                uroles = await _context.Roles.Where(ur => ur.Id == roleId).FirstOrDefaultAsync();
                if (uroles != null) { _context.Roles.Remove(uroles); _context.SaveChanges(); }
            }

            return uroles;/*;//  // RedirectToAction(nameof(Index)); */
        }
    }
}
