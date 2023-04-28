/* Author: Madhan KAMALAKANNAN  , Madhan.KAMALAKANNAN@outlook.com
 Created : Created 29/Aug/2021
 Modified:  /Dec/2021  
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json.Linq;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;
      

namespace OnlineShoppingCart.Controllers
{
    //[AllowAnonymous]
   //[Authorize(Roles = "Admin")]
   [Route("/admin/Products/{action}")]
    public class ProductsController : Controller
    {
        private readonly AsnetidentityContext _context;
       
        int itemsPerPage = 10; int totalCount = 0;
        private readonly UserManager<IdentityUser> _userManager; 
   
        private IConfiguration _configuration = null;
        private IEnumerable<KeyValuePair<string, string>> kvPList;
        private KeyValuePair<string, string>[] arrayKP;

        public ProductsController(AsnetidentityContext context, IConfiguration configuration)
        {
        
            _context = context;
            _configuration = configuration;
            kvPList = _configuration.AsEnumerable();
            arrayKP = kvPList.ToArray();

            //ViewData["SortOrder"] = "Asc";
            //ViewData["SortParam"] = "Name";
            //totalCount = _context.Product.Count();
            //if(totalCount % itemsPerPage ==0)
            //{
            //    ViewData["TotalPages"] = totalCount / itemsPerPage ;
            //}
            //else
            //{
            //    ViewData["TotalPages"] = (totalCount / itemsPerPage) + 1;
            //}

        }

        //[Route("~/admin/products/index")]
        public async Task<IActionResult> Index(int? id,string sortOrder,string sortParam,int currentPage)
        {
            id = id == null ? 0 : id;
            ViewData["EditAll"] = id;
            ViewData["productPicurl"] = arrayKP.FirstOrDefault(x => x.Key == "ProductPic:Url").Value;

            ViewData["id"] = id;
         
            sortOrder = sortOrder == null ? "Asc" : sortOrder;

            sortParam = sortParam == null ? "Name" : sortParam;

            currentPage = currentPage == null ? 1 : currentPage;

            totalCount = _context.Product.Count();

            if (totalCount % itemsPerPage == 0)
            {
                ViewData["TotalPages"] = totalCount / itemsPerPage;
            }
            else
            {
                ViewData["TotalPages"] = (totalCount / itemsPerPage) + 1;
            }


            ViewData["SortParam"] = sortParam;
            ViewData["CurrentPage"] = ViewData["CurrentPage"] != "" ? currentPage : 1;

            int currentPageNo =Convert.ToInt32( ViewData["CurrentPage"]);
            ViewData["SortOrder"] = sortOrder+"".Trim();//== "Asc" ? "Desc" : "Asc";
            // ViewData["CurrentPage"] != "" ? Convert.ToInt32(ViewData["CurrentPage"]) : 1;
            int tobeSkipped = currentPageNo * itemsPerPage == totalCount ? (currentPageNo * itemsPerPage): currentPageNo * itemsPerPage;
            if (sortParam == "Name")
            {
                if(sortOrder=="Asc")
                {
                    return View(await _context.Product.OrderBy(p => p.Name).Skip(tobeSkipped).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View(await _context.Product.OrderByDescending(p => p.Name).Skip(tobeSkipped).Take(itemsPerPage).ToListAsync());
                }
                
            }
            else if (sortParam == "Description")
            {
                if (sortOrder == "Asc")
                {
                    return View(await _context.Product.OrderBy(p => p.Description).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View(await _context.Product.OrderByDescending(p => p.Description).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }
            else if (sortParam == "ActivationCodes")
            {
                if (sortOrder == "Asc")
                {
                    return View(await _context.Product.OrderBy(p => p.ActivationCodes).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View(await _context.Product.OrderByDescending(p => p.ActivationCodes).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }
            else if (sortParam == "Price")
            {
                if (sortOrder == "Asc")
                {
                    return View(await _context.Product.OrderBy(p => p.Price).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View(await _context.Product.OrderByDescending(p => p.Price).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }
            else if (sortParam == "ProductPic")
            {
                if (sortOrder == "Asc")
                {
                    return View(await _context.Product.OrderBy(p => p.ProductPic).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View(await _context.Product.OrderByDescending(p => p.ProductPic).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }
            else
            {
                if (sortOrder == "Asc")
                {
                    return View(await _context.Product.OrderBy(p => p.Id).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View(await _context.Product.OrderByDescending(p => p.Id).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }

        }


        //public async Task<IActionResult> Index(int? id, string sortOrder, string sortParam, int currentPage)
        //{
        //    ViewData["id"] = id;
        //    id = id == null ? 1 : id;
        //    sortOrder = sortOrder == null ? "Asc" : sortOrder;

        //    sortParam = sortParam == null ? "Name" : sortParam;

        //    currentPage = currentPage == null ? 1 : currentPage;

        //    totalCount = _context.Product.Count();

        //    if (totalCount % itemsPerPage == 0)
        //    {
        //        ViewData["TotalPages"] = totalCount / itemsPerPage;
        //    }
        //    else
        //    {
        //        ViewData["TotalPages"] = (totalCount / itemsPerPage) + 1;
        //    }


        //    ViewData["SortParam"] = sortParam;
        //    ViewData["CurrentPage"] = ViewData["CurrentPage"] != "" ? currentPage : 1;

        //    int currentPageNo = Convert.ToInt32(ViewData["CurrentPage"]);
        //    ViewData["SortOrder"] = sortOrder + "".Trim();//== "Asc" ? "Desc" : "Asc";
        //    // ViewData["CurrentPage"] != "" ? Convert.ToInt32(ViewData["CurrentPage"]) : 1;
        //    int tobeSkipped = currentPageNo * itemsPerPage == totalCount ? (currentPageNo * itemsPerPage) : currentPageNo * itemsPerPage;
        //    if (sortParam == "Name")
        //    {
        //        if (sortOrder == "Asc")
        //        {
        //            return View(await _context.Product.OrderBy(p => p.Name).Skip(tobeSkipped).Take(itemsPerPage).ToListAsync());
        //        }
        //        else
        //        {
        //            return View(await _context.Product.OrderByDescending(p => p.Name).Skip(tobeSkipped).Take(itemsPerPage).ToListAsync());
        //        }

        //    }
        //    else if (sortParam == "Description")
        //    {
        //        if (sortOrder == "Asc")
        //        {
        //            return View(await _context.Product.OrderBy(p => p.Description).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
        //        }
        //        else
        //        {
        //            return View(await _context.Product.OrderByDescending(p => p.Description).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
        //        }
        //    }
        //    else if (sortParam == "ActivationCodes")
        //    {
        //        if (sortOrder == "Asc")
        //        {
        //            return View(await _context.Product.OrderBy(p => p.ActivationCodes).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
        //        }
        //        else
        //        {
        //            return View(await _context.Product.OrderByDescending(p => p.ActivationCodes).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
        //        }
        //    }
        //    else if (sortParam == "Price")
        //    {
        //        if (sortOrder == "Asc")
        //        {
        //            return View(await _context.Product.OrderBy(p => p.Price).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
        //        }
        //        else
        //        {
        //            return View(await _context.Product.OrderByDescending(p => p.Price).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
        //        }
        //    }
        //    else if (sortParam == "ProductPic")
        //    {
        //        if (sortOrder == "Asc")
        //        {
        //            return View(await _context.Product.OrderBy(p => p.ProductPic).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
        //        }
        //        else
        //        {
        //            return View(await _context.Product.OrderByDescending(p => p.ProductPic).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
        //        }
        //    }
        //    else
        //    {
        //        if (sortOrder == "Asc")
        //        {
        //            return View(await _context.Product.OrderBy(p => p.Id).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
        //        }
        //        else
        //        {
        //            return View(await _context.Product.OrderByDescending(p => p.Id).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
        //        }
        //    }

        //}

        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edittest([FromForm] string id, string name, string desc)//int id, [Bind("Id,Name,Description,Price,ProductPic,ActivationCodes")] Product product)
        {
            Product product = null;
            var id1 = RouteData.Values["Id"];// Request.Query["Id"];
            var name1 = RouteData.Values["Name"];
            return View(product);
        }


       // [HttpPost]
       // [ValidateAntiForgeryToken]
       //public async Task<IActionResult> UniqueEditSingleDataList(int id)//,EditSingleDataListP string name,string desc,string productpic,string price,string activationcodes)//int id, [Bind("Id,Name,Description,Price,ProductPic,ActivationCodes")] Product product)
 
       // {
           
       // }


        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditList(IList<Product> products )
        {      
            if (ModelState.IsValid)
            {
                foreach (var product in products)
                {                    
                    try
                    {
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(products);

        }

            // GET: Products/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ProductPic,ActivationCodes")] Product product)
        {

            if (ModelState.IsValid && product!=null&& product.Name!=null&& product.Name!="")
            {
                _context.Product.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        //Modified On 20211219 By Madhan KAMALAKANNAN  
        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAt(int id)//, [Bind("Id,Name,Description,Price,ProductPic,ActivationCodes")] Product product)
        { 
            if (ModelState.IsValid)
            {
                ViewData["id"] = id;
                // var id1 = RouteData.Values["Id"];// Request.Query["Id"];
                var formData = Request.Form.ToDictionary(x => x.Key, x => x.Value);
                Product product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    if (formData.Count() > 0)
                    {
                        foreach (var kvp in formData)
                        {
                            if (kvp.Key.Contains("Name", StringComparison.OrdinalIgnoreCase))
                            {
                                product.Name = formData[kvp.Key]; //formData[0].Value;
                            }
                            if (kvp.Key.Contains("Description", StringComparison.OrdinalIgnoreCase))
                            {
                                product.Description = formData[kvp.Key]; //formData[0].Value;
                            }
                            if (kvp.Key.Contains("Quantity", StringComparison.OrdinalIgnoreCase))
                            {
                                product.Quantity = formData[kvp.Key] != "" ? int.Parse(formData[kvp.Key]) : 0;//formData[0].Value;
                            }
                            if (kvp.Key.Contains("Price", StringComparison.OrdinalIgnoreCase))
                            {
                                product.Price = formData[kvp.Key] != "" ? decimal.Parse(formData[kvp.Key]) : 0;//formData[0].Value;
                            }
                            if (kvp.Key.Contains("ProductPic", StringComparison.OrdinalIgnoreCase))
                            {
                                product.ProductPic = formData[kvp.Key]; //formData[0].Value;
                            }
                            if (kvp.Key.Contains("ActivationCodes", StringComparison.OrdinalIgnoreCase))
                            {
                                product.ActivationCodes = formData[kvp.Key]; //formData[0].Value;
                            }
                        }
                        try
                        {
                            _context.Update(product);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ProductExists(product.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                } 
               
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5

        //[ValidateAntiForgeryToken]
        public ActionResult<string> DeleteConfirmed(int id)
        {
            var product = _context.Product.Find(id);
            if(product != null)
            {  
                _context.Product.Remove(product);
                 _context.SaveChanges();
            }
            return "done";
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
