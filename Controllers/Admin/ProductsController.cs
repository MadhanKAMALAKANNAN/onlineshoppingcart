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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;
      

namespace OnlineShoppingCart.Controllers.Admin
{

    [Authorize(Roles = "Admin")]
    //[Route("Admin/Products/{action}")]
    public class ProductsController : Controller
    {
        private readonly AsnetidentityContext _context;

        int itemsPerPage = 10; int totalCount = 0;
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration = null;
        private IEnumerable<KeyValuePair<string, string>> kvPList;
        private KeyValuePair<string, string>[] arrayKP;
     
    public ProductsController(AsnetidentityContext context,IConfiguration configuration)
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
           // var temPara = System.Net.WebUtility.UrlDecode(otherParams);

            ViewData["productPicurl"] = arrayKP.FirstOrDefault(x => x.Key == "ProductPic:Url").Value;

            ViewData["id"] = id;
            id = id == null ? 1 : id;
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
          //  return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.ToListAsync());

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
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderBy(p => p.Name).Skip(tobeSkipped).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderByDescending(p => p.Name).Skip(tobeSkipped).Take(itemsPerPage).ToListAsync());
                }
                
            }
            else if (sortParam == "Description")
            {
                if (sortOrder == "Asc")
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderBy(p => p.Description).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderByDescending(p => p.Description).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }
            else if (sortParam == "ActivationCodes")
            {
                if (sortOrder == "Asc")
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderBy(p => p.ActivationCodes).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderByDescending(p => p.ActivationCodes).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }
            else if (sortParam == "Price")
            {
                if (sortOrder == "Asc")
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderBy(p => p.Price).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderByDescending(p => p.Price).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }
            else if (sortParam == "ProductPic")
            {
                if (sortOrder == "Asc")
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderBy(p => p.ProductPic).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderByDescending(p => p.ProductPic).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }
            else if (sortParam == "ShowtoSale")
            {
                if (sortOrder == "Asc")
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderBy(p => p.ShowtoSale).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderByDescending(p => p.ShowtoSale).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }
            else if (sortParam == "Quantity")
            {
                if (sortOrder == "Asc")
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderBy(p => p.Quantity).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderByDescending(p => p.Quantity).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
            }
            else
            {
                if (sortOrder == "Asc")
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderBy(p => p.Id).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
                }
                else
                {
                    return View("~/Views/Admin/Products/Index.cshtml", await _context.Product.OrderByDescending(p => p.Id).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync());
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
        public async Task<IActionResult> Edit([FromForm] int id, string name, string desc)//int id, [Bind("Id,Name,Description,Price,ProductPic,ActivationCodes")] Product product)
        {
            Product product = null;
            var id1 = RouteData.Values["Id"];// Request.Query["Id"];
            var name1 = RouteData.Values["Name"];
          return View("~/Views/Admin/Products/edit.cshtml", await _context.Product.FirstOrDefaultAsync(m => m.Id == id));
        }

 [HttpPost]
 //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSingleDataList(int id,string otherParams)//, string name,string desc,string productpic,string price,string activationcodes)//int id, [Bind("Id,Name,Description,Price,ProductPic,ActivationCodes")] Product product)
        {
           var temPara = System.Net.WebUtility.UrlDecode(otherParams);
            // var id1 = RouteData.Values["Id"];// Request.Query["Id"];
            var formData = Request.Form.ToArray();
            Product product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                if (formData.Count() == 7)
                {
                    product.Name = formData[0].Value;
                    product.Description = formData[1].Value;
                    product.Price = formData[2].Value != "" ? Convert.ToDecimal(formData[2].Value) : 0;  
                    product.ProductPic = formData[3].Value;
                    product.ActivationCodes = formData[4].Value + "";
                    product.ShowtoSale = formData[5].Value + "";
                    string qnt = formData[6].Value+"";
                    product.Quantity = qnt != ""?int.Parse(qnt):0;

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
            return RedirectToAction(nameof(Index));
        }
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
           return View("~/Views/Admin/Products/details.cshtml", products);

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

            return View("~/Views/Admin/Products/details.cshtml", product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View("~/Views/Admin/Products/create.cshtml");
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ProductPic,ActivationCodes")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Product.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
             return View("~/Views/Admin/Products/Create.cshtml", product);
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
            //return View(product);
            return View("~/Views/Admin/Products/edit.cshtml", product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAt(int id, [Bind("Id,Name,Description,Price,ProductPic,ActivationCodes")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Admin/Products/edit.cshtml", product);
        
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
