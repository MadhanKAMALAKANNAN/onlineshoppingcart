using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
 
//using System.Web.Mvc;
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
        }
        public IConfiguration _config { get; }
        public IList<Product> Product { get; set; }
        //public IList<CartDetails> addedCartDetailsList { get; set; }
        //public IList<Product> addedProductList { get; set; }
        public string sideNavString { get; set; }
        public IList<ViewCart> viewCarttList { get; set; }
        public IList<ShippingOptions> shippingOptions { get; set; } 
        public string productPicUrl { set; get; }
        public IList<ProductCategories> productCategoriesParentNodes { get; set; }
        public IList<ProductCategories> productCategoriesLeafNodes { get; set; }
        public async Task OnGetAsync()
        {
            productPicUrl = _config["ProductPicUrl"];
            if(_context!=null&&_context.Product!=null&& _context.Product.Count()>0)
            {
                Product =  _context.Product.ToList();
            }
        
            var tempproductCategoriesParentNodes = _context.ProductCategories.Where(x => x.ParentId == null);
            productCategoriesParentNodes = await tempproductCategoriesParentNodes.CountAsync() > 0 ?await tempproductCategoriesParentNodes.ToListAsync() : null ;
            var temProductCateLeafNodeOnly =  _context.ProductCategories.Where(x => x.ParentId != null);
            productCategoriesLeafNodes = await temProductCateLeafNodeOnly.CountAsync() > 0 ? await temProductCateLeafNodeOnly.ToListAsync() : null ;

            ////
            //await OnGetConsturctSideNav();
            ////

        }

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
            foreach (var cateParentNode in productCategoriesParentNodes)
            { 
                var parentId = cateParentNode.Id + "";
                var navUrl = cateParentNode.NavUrl;
                navUrl = navUrl == null ? "#" : navUrl;
                parentNodesHtml.Append("<li id = 'li_" + parentId+"'>  <a href = '"+ navUrl + "' title = '"+ cateParentNode .CategoryDesc+ "' > "+ cateParentNode.CategoryName+ " </a>");              
                await RecursiveConstructSideNav(parentId,parentNodesHtml, navMenuIds, leafNodeCounts);
                var countNextLeaf = productCategoriesLeafNodes.Where(x => x.ParentId + "" == cateParentNode.Id + "").Count();
                if (countNextLeaf == 0)
                { parentNodesHtml.Append("</li>"); }
               
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
                            _parentNodesHtml.Append("<ul id='ul" + cateLeafNode.Id + "_" + Id + "'>  <li id = 'li_" + cateLeafNode.Id+"_"+ cateLeafNode.ParentId + "' > <a href = '"+ navUrl + "' title = '" + cateLeafNode.CategoryDesc + "' >" + cateLeafNode.CategoryName + "</a>");
                            flg = true;
                        }
                        else
                        {
                            flg = true;
                            _parentNodesHtml.Append("<li id = 'li_" + cateLeafNode.Id+"_"+ cateLeafNode.ParentId + "'> <a href = '"+navUrl + "' title = '" + cateLeafNode.CategoryDesc + "' >" + cateLeafNode.CategoryName + " </a>  ");
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
                    CartDetails cartDetails = await _context.CartDetails.Where(x => x.ProductId + "" == productId).FirstOrDefaultAsync();
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
                        //ViewData["ViewCarttList"] = viewCarttList;
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

                if (cart == null)
                {

                    //cart = new Cart();
                    //cart.UserId = user.Id;
                    //cart.DateTime = DateTime.Now;
                    //_context.Cart.Add(cart);
                    //_context.SaveChanges();   
                }
                // @System.Web.HttpUtility.HtmlDecode(@jsonInput1.ToString())
                if (jsonInput != "" && jsonInput != "0")
                {
                    var tempData = System.Text.Json.JsonSerializer.Deserialize<IList<ProductTemp>>(jsonInput);
                    foreach (var eachPrdTemp in tempData)
                    {
                        int prdId = int.Parse(eachPrdTemp.Id);
                        CartDetails cartDetails = await _context.CartDetails.Where(x => x.ProductId + "" == eachPrdTemp.Id).FirstOrDefaultAsync();
                        var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == prdId);
                        if (cartDetails == null)
                        {
                            //cartDetails = new CartDetails();

                            //cartDetails.ProductId = prdId;
                            //cartDetails.CartId = cart.Id;
                            //cartDetails.Qty = 1;
                            //cartDetails.TotalPrice = cartDetails.Qty * product.Price;
                            //_context.CartDetails.Add(cartDetails);
                            //_context.SaveChanges();
                        }
                        else
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

        public async Task  OnPostSearchProductsAsync(string searchWord)
        {
            productPicUrl = _config["ProductPicUrl"];
            var prdoCateIdList = _context.ProductCategories.Where(x => x.CategoryName.Contains(searchWord) || x.CategoryDesc.Contains(searchWord)).ToList();

            //Product = await _context.Product.Where(x=>x.Name.Contains(searchWord) || x.Description.Contains(searchWord)).ToListAsync();

            var allProducts = _context.Product;

            var searchProducts = from prds in allProducts where prds.Name.Contains(searchWord) || prds.Description.Contains(searchWord) || prdoCateIdList.Any(x => x.Id == prds.ProductCategoriesId)
                                 select prds;
          
            var tempproductCategoriesParentNodes = _context.ProductCategories.Where(x => x.ParentId == null);
            productCategoriesParentNodes = await tempproductCategoriesParentNodes.CountAsync() > 0 ? await tempproductCategoriesParentNodes.ToListAsync() : null;
            var temProductCateLeafNodeOnly = _context.ProductCategories.Where(x => x.ParentId != null);
            productCategoriesLeafNodes = await temProductCateLeafNodeOnly.CountAsync() > 0 ? await temProductCateLeafNodeOnly.ToListAsync() : null;
        }
    }
}
