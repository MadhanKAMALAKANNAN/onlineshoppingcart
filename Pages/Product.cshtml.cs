/* Author: Madhan KAMALAKANNAN  
 Created : Created 29/Aug/2021
 Modified: 15/Dec/2021  
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
 
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace OnlineShoppingCart.Pages
{
    public class ProductModel : PageModel
    {
        private readonly AsnetidentityContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
 
        public ProductModel(IConfiguration config, AsnetidentityContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            viewCarttList = new List<ViewCart>();
            shippingOptions = _context != null ? _context.ShippingOptions.ToList() : null;

            productPicUrl = _config["ProductPicUrl"];
            var temProductsPerPage = _config["productsPerPage"];
            if (_context != null && _context.Product != null)
            {
                var tempProd = _context.Product.Count() > 0? _context.Product.ToList():null;
                SetVariable(tempProd, temProductsPerPage);
            }
               
             var tempproductCategoriesParentNodes = _context.ProductCategories.Where(x => x.ParentId == null);
            productCategoriesParentNodes =   tempproductCategoriesParentNodes.Count() > 0 ?  tempproductCategoriesParentNodes.ToList() : null;
            var temProductCateLeafNodeOnly = _context.ProductCategories.Where(x => x.ParentId != null);
            productCategoriesLeafNodes =  temProductCateLeafNodeOnly.Count() > 0 ?  temProductCateLeafNodeOnly.ToList() : null;

        }
        public IConfiguration _config { get; }
        public IList<Product> Product { get; set; }
        
        public string sideNavString { get; set; }
        public IList<ViewCart> viewCarttList { get; set; }
        public IList<ShippingOptions> shippingOptions { get; set; } 
        public string productPicUrl { set; get; }
        public IList<ProductCategories> productCategoriesParentNodes { get; set; }
        public IList<ProductCategories> productCategoriesLeafNodes { get; set; }
        public int totalPages { set; get; }
        public int productsPerPage { set; get; }
        public async Task OnGetAsync()
        {
 
 

        }
        public void SetVariable(IList<Product> _Product,string temProductsPerPage)
        {
            Product = _Product;
            if (Product != null)
            {
                var totlProdCount = Product.Count();
                productsPerPage = temProductsPerPage != null ? int.Parse(temProductsPerPage) : 5;
              
                totalPages = 0;
                var reminder = totlProdCount % productsPerPage;
                totalPages = totlProdCount / productsPerPage;
                if (reminder != 0)
                {
                    totalPages = totalPages + 1;
                }  
               
            }
        }

        //,Udated: 06December2021
        public async Task<IActionResult> OnGetProductsAsync()//string jsonInput)
        {
           
            return Partial("_Products", this);
        }

        //-----------------------------------

        public async Task<IActionResult> OnGetConsturctSideNav()
        {
            var tempproductCategoriesParentNodes = _context.ProductCategories.Where(x => x.ParentId == null);
            productCategoriesParentNodes = await tempproductCategoriesParentNodes.CountAsync() > 0 ? await tempproductCategoriesParentNodes.ToListAsync() : null;
            var temProductCateLeafNodeOnly = _context.ProductCategories.Where(x => x.ParentId != null);
            productCategoriesLeafNodes = await temProductCateLeafNodeOnly.CountAsync() > 0 ? await temProductCateLeafNodeOnly.ToListAsync() : null;
            StringBuilder parentNodesHtml = new StringBuilder();
            List<string> navMenuIds = new List<string>();
            IDictionary<string, int> leafNodeCounts = new Dictionary<string,int>();
            parentNodesHtml.Append("<ul class='classCls'>");
            if (productCategoriesParentNodes != null)
            {
                foreach (var cateParentNode in productCategoriesParentNodes)
                {
                    var parentId = cateParentNode.Id + "";
                    var navUrl = cateParentNode.NavUrl;
                    navUrl = navUrl == null ? "#" : navUrl;
                    parentNodesHtml.Append("<li id = 'li_" + parentId + "'>  <a href_ = '" + navUrl + "' title = '" + cateParentNode.CategoryDesc + "'  onclick=\"CallToLoad(\'" + cateParentNode.Id + "\',\'"+ cateParentNode.CategoryName + "\')\"> " + cateParentNode.CategoryName + " </a>");
                    await RecursiveConstructSideNav(parentId, parentNodesHtml, navMenuIds, leafNodeCounts);
                    var countNextLeaf = productCategoriesLeafNodes.Where(x => x.ParentId + "" == cateParentNode.Id + "").Count();
                    if (countNextLeaf == 0)
                    { parentNodesHtml.Append("</li>"); }

                }
            }
            parentNodesHtml.Append("</ul>");
            sideNavString = parentNodesHtml.ToString();
            return Partial("_SideNav", sideNavString);

        }
        public async Task RecursiveConstructSideNav(string Id, StringBuilder _parentNodesHtml, List<string> _navMenuIds, IDictionary<string, int> _leafNodeCounts)
        {
            int noOfCounts = productCategoriesLeafNodes.Count;
            noOfCounts = productCategoriesLeafNodes.Where(x => x.ParentId+"" == Id).Count();
            
            int i = 0;
            foreach (var cateLeafNode in productCategoriesLeafNodes)
            {
                var navUrl = cateLeafNode.NavUrl;
                navUrl = navUrl == null ? "#" : navUrl;

                if (Id + "" != "")
                {
                    if (Id == cateLeafNode.ParentId + "")
                    {
                        var flg = false;
                        if (!_navMenuIds.Contains(Id + ""))
                        {
                            _navMenuIds.Add(Id + "");
                            _parentNodesHtml.Append("<ul id='ul" + cateLeafNode.Id + "_" + Id + "'>  <li id = 'li_" + cateLeafNode.Id+"_"+ cateLeafNode.ParentId + "' > <a href_ = '"+ navUrl + "' title = '" + cateLeafNode.CategoryDesc + "' onclick=\"CallToLoad(\'" + cateLeafNode.Id + "\',\'" + cateLeafNode.CategoryName + "\')\"> " + cateLeafNode.CategoryName + "</a>");
                            flg = true;
                        }
                        else
                        {
                            flg = true;
                            _parentNodesHtml.Append("<li id = 'li_" + cateLeafNode.Id+"_"+ cateLeafNode.ParentId + "'> <a href_ = '"+navUrl + "' title = '" + cateLeafNode.CategoryDesc + "'   onclick=\"CallToLoad(\'" + cateLeafNode.Id + "\',\'" + cateLeafNode.CategoryName + "\')\"> " + cateLeafNode.CategoryName + " </a>  ");
                        }
                        var countNextLeaf = productCategoriesLeafNodes.Where(x => x.ParentId + "" == cateLeafNode.Id+"").Count();
                        if (countNextLeaf == 0 && flg)
                        { _parentNodesHtml.Append("</li>"); }

                        await RecursiveConstructSideNav(cateLeafNode.Id + "", _parentNodesHtml, _navMenuIds, _leafNodeCounts);

                      
                        if (!_leafNodeCounts.Keys.Contains(Id))
                        {
                            _leafNodeCounts.Add(Id, ++i);
                        }
                        else
                        {
                            _leafNodeCounts[Id] = ++i;
                        }

                        if (_leafNodeCounts.ContainsKey(Id) && noOfCounts == _leafNodeCounts[Id]&& noOfCounts!=0)
                        {
                            _parentNodesHtml.Append("</ul></li>");
                        }
                    }
                }   
            }
         
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> OnPostAddtoCart(string productId)
        {

            return null;
        }


        public async Task<IActionResult> OnGetAddtoCartAsync(string productId)
        {
            productId = productId != "" ? productId : "0";


            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                Cart cart = await _context.Cart.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();

                if (cart == null)
                {
                    cart = new Cart();
                    cart.UserId = user.Id;
                    cart.DateTime = DateTime.Now;
                    _context.Cart.Add(cart);
                    _context.SaveChanges();
                }

                if (productId != "" && productId != "0")
                {
                    int prdId = int.Parse(productId);
                    CartDetails cartDetails = await _context.CartDetails.Where(x => x.ProductId + "" == productId && x.CartId == cart.Id).FirstOrDefaultAsync();
                    var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == prdId);
                    if (cartDetails == null)
                    {
                        cartDetails = new CartDetails();

                        cartDetails.ProductId = prdId;
                        cartDetails.CartId = cart.Id;
                        cartDetails.Qty = 1;
                        cartDetails.TotalPrice = cartDetails.Qty * product.Price;
                        _context.CartDetails.Add(cartDetails);
                        _context.SaveChanges();
                    }
                    else
                    {
                        cartDetails.Qty = cartDetails.Qty + 1;
                        cartDetails.TotalPrice = cartDetails.Qty * product.Price;

                        _context.CartDetails.Update(cartDetails);
                        _context.SaveChanges();
                    }
                }
                IEnumerable<CartDetails> cartDetailsList = await _context.CartDetails.Where(x => x.CartId == cart.Id).ToListAsync();

                foreach (CartDetails eachCartdetails in cartDetailsList)
                {
                    var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == eachCartdetails.ProductId);

                    ViewCart viewCart = new ViewCart();
                    viewCart.product = product;
                    viewCart.cartDetails = eachCartdetails;
                    if (!viewCarttList.Contains(viewCart))
                    {
                        viewCarttList.Add(viewCart); 
                    }
                }
            }

            return Partial("ViewCart", viewCarttList);
        }

        public async Task<IActionResult> OnGetUpdateCartAsync(string jsonInput)
        {
            jsonInput = jsonInput.Replace("/", "").Replace("},]", "}]");


            // string jsonInput1 = "[{\"Id\": \"2\", \"Quantity\": \"1\"},{\"Id\": \"1\", \"Quantity\": \"4\"},{\"Id\": \"3\", \"Quantity\": \"6\"}]";

            //productId = productId != "" ? productId : "0";
            var user = await _userManager.GetUserAsync(User);


            if (user != null)
            {
                Cart cart = await _context.Cart.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                var cartId = 0;
                if (cart != null) { cartId=cart.Id; }
                 
                if (jsonInput != "" && jsonInput != "0")
                {
                    var tempData = System.Text.Json.JsonSerializer.Deserialize<IList<ProductTemp>>(jsonInput);
                    foreach (var eachPrdTemp in tempData)
                    {
                        int prdId = int.Parse(eachPrdTemp.Id);
                        CartDetails cartDetails = await _context.CartDetails.Where(x => x.ProductId + "" == eachPrdTemp.Id && x.CartId == cartId).FirstOrDefaultAsync();
                        var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == prdId);
                        if (cartDetails != null)
                        { 
                            cartDetails.Qty = eachPrdTemp.Quantity != "" ? int.Parse(eachPrdTemp.Quantity) : 0;//cartDetails.Qty <= 0 ? 0 : cartDetails.Qty - 1;
                            if (cartDetails.Qty <= 0) { _context.CartDetails.Remove(cartDetails); }
                            else
                            {
                                cartDetails.TotalPrice = cartDetails.Qty * product.Price;
                                _context.CartDetails.Update(cartDetails);
                            }

                            _context.SaveChanges();
                        }
                    }
                }

                IEnumerable<CartDetails> cartDetailsList = await _context.CartDetails.Where(x => x.CartId == cart.Id).ToListAsync();

                foreach (CartDetails eachCartdetails in cartDetailsList)
                {
                    var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == eachCartdetails.ProductId);

                    ViewCart viewCart = new ViewCart();
                    viewCart.product = product;
                    viewCart.cartDetails = eachCartdetails;
                    if (!viewCarttList.Contains(viewCart))
                    {
                        viewCarttList.Add(viewCart);
                        //ViewData["ViewCarttList"] = viewCarttList;
                    }
                }

            }

            return Partial("_Cart", this);
        }

        public async Task<JsonResult> OnGetApplyCouponToCartAsync(string cartId, string couponName)
        {
            Coupon coupon = await _context.Coupon.FirstOrDefaultAsync(m => m.CouponName == couponName);
            string outp = string.Empty;
           
            if(coupon == null)
            { 
                    outp = "[{\"error\":\"" + _config["couponNotExists"] + "\"}]"; 
            }
            else
            {
                CartDetails cartDetails = await _context.CartDetails.FirstOrDefaultAsync(x => x.CartId.ToString() == cartId && x.CouponId == coupon.Id);
                if (cartDetails != null)
                {
                    outp = "[{\"error\":\"" + _config["couponUsed"] + "\"}]";
                }
                else
                {
                    outp = System.Text.Json.JsonSerializer.Serialize(coupon);
                }

            }

            JsonResult jsr = new JsonResult(outp);
            return jsr;
        }

        public async Task<IActionResult> OnGetSearchProductsAsync(string searchWord, string priceRange, string selectSortingOption, int currentPage,string searchMode)
        {

            //currentPage = 2;


            searchWord = searchWord + "";
            searchWord = searchWord.ToLower().Trim();
            int maxPriceRange = 0, minPriceRange = 0;
            TempData["searchWord"] = searchWord;
            productPicUrl = _config["ProductPicUrl"];
            StringComparison comp = StringComparison.OrdinalIgnoreCase;
            var allProducts = _context.Product.ToHashSet();
            if (searchWord != "")
            {
                var allProductCate = _context.ProductCategories.ToHashSet();

                HashSet<ProductCategories> prdoCateIdList;
                HashSet<Product> searchProducts;  

                if(searchMode == "1")
                {
                    prdoCateIdList = (from prds in allProductCate where prds.CategoryName.ToLower().Trim() == searchWord  select prds).ToHashSet(); 
                    searchProducts = (from prds in allProducts where prds.Name.Contains(searchWord, comp) || prds.Description.Contains(searchWord, comp) select prds).ToHashSet();
                }
                else
                {
                    searchProducts = new HashSet<Product>();
                    prdoCateIdList = (from prds in allProductCate where prds.Id+"" == searchWord select prds).ToHashSet();
                }

                if (prdoCateIdList.Count > 0)
                {
                    var searchProducts1 = (from prds in allProducts where prdoCateIdList.Any(x => prds.ProductCategoriesId == x.Id) select prds).ToHashSet();
                    foreach (var echPrd in searchProducts1)
                    {
                        searchProducts.Add(echPrd);
                    }
                }

                allProducts = searchProducts;
                Product = searchProducts.ToList(); 
              
            }

            if (priceRange != "" && priceRange != "0")
            {
                priceRange = priceRange.Replace("$", "");

                string[] priceRangeAr = priceRange.Split("-", StringSplitOptions.RemoveEmptyEntries);
                if (priceRangeAr.Length >= 2)
                {
                    minPriceRange = priceRangeAr[0] != null ? int.Parse(priceRangeAr[0]) : 0;
                    maxPriceRange = priceRangeAr[1] != null ? int.Parse(priceRangeAr[1]) : 0;
                }

                var searchRangeProducts = (from prds in allProducts where prds.Price != null && (prds.Price.Value >= minPriceRange && prds.Price.Value <= maxPriceRange) select prds).ToHashSet();
                Product = searchRangeProducts.ToList();
            } 

            if (Product != null)
            {
                if (selectSortingOption != "" && selectSortingOption.Trim().ToLower() == "Price: low to high".Trim().ToLower())
                { Product = Product.OrderBy(x => x.Price).ToList(); }
                else if (selectSortingOption != "")
                {
                    Product = Product.OrderByDescending(x => x.Price).ToList();
                }
            }

            SetVariable(Product, productsPerPage + "");
            var totalProds = Product != null ? Product.Count() : 0;
            var temCount = currentPage * productsPerPage;
            if(temCount>=productsPerPage )//totalProds >= temCount)
            {
                var startIndex = temCount - productsPerPage;
                var endIndex = startIndex + productsPerPage;

                if (startIndex > -1 && endIndex > -1)
                {
                    Range range = new Range(startIndex, endIndex);
                    Product = Product != null ? Product.Take(range).ToList() : null;
                }
            }

            return Partial("_Products", this);
        }
    }
}
