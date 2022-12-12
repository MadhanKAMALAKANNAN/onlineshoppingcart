using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShoppingCart.Models;

namespace OnlineShoppingCart.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly OnlineShoppingCart.DB.AsnetidentityContext _context;
        int itemsPerPage = 10; int totalCount = 0;
        public IndexModel(OnlineShoppingCart.DB.AsnetidentityContext context, ILogger<IndexModel> logger)
        {
            _context = context;
        }
        public IEnumerable<Product> products { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string sortOrder, string sortParam, int currentPage)
        {
     
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


            ViewData["SortParam"] = sortParam;
            ViewData["CurrentPage"] = ViewData["CurrentPage"] != "" ? currentPage : 1;

            int currentPageNo = Convert.ToInt32(ViewData["CurrentPage"]);
            ViewData["SortOrder"] = sortOrder + "".Trim();//== "Asc" ? "Desc" : "Asc";
                                              // ViewData["CurrentPage"] != "" ? Convert.ToInt32(ViewData["CurrentPage"]) : 1;
            int tobeSkipped = currentPageNo * itemsPerPage == totalCount ? (currentPageNo * itemsPerPage) : currentPageNo * itemsPerPage;
            if (sortParam == "Name")
{
                if (sortOrder == "Asc")
                {
                     products = await _context.Product.OrderBy(p => p.Name).Skip(tobeSkipped).Take(itemsPerPage).ToListAsync();
                }
                else
                {
                     products = await _context.Product.OrderByDescending(p => p.Name).Skip(tobeSkipped).Take(itemsPerPage).ToListAsync();
                }

            }
            else if (sortParam == "Description")
{
                if (sortOrder == "Asc")
                {
                    products = await _context.Product.OrderBy(p => p.Description).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync();
                }
                else
                {
                    products = await _context.Product.OrderByDescending(p => p.Description).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync();
                }
            }
            else if (sortParam == "ActivationCodes")
            {
                if (sortOrder == "Asc")
                {
                    products = await _context.Product.OrderBy(p => p.ActivationCodes).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync();
                }
                else
                {
                    products = await _context.Product.OrderByDescending(p => p.ActivationCodes).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync();
                }
            }
            else if (sortParam == "Price")
            {
                if (sortOrder == "Asc")
{
                    products = await _context.Product.OrderBy(p => p.Price).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync();
                }
                else
                {
                    products = await _context.Product.OrderByDescending(p => p.Price).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync();
                }
            }
            else if (sortParam == "ProductPic")
            {
                if (sortOrder == "Asc")
{
                    products = await _context.Product.OrderBy(p => p.ProductPic).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync();
                }
                else
                {
                    products  = await _context.Product.OrderByDescending(p => p.ProductPic).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync();
                }
            }
            else
            {
                if (sortOrder == "Asc")
{
                    products = await _context.Product.OrderBy(p => p.Id).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync();
                }
                else
                {
                    products = await _context.Product.OrderByDescending(p => p.Id).Skip(currentPageNo * itemsPerPage).Take(itemsPerPage).ToListAsync();
                }
            }

            return Page();
        }
    }
}
