using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.DB;
using OnlineShoppingCart.Models;
using Microsoft.AspNetCore.Identity;
namespace OnlineShoppingCart.Pages
{
    public class CartModel : PageModel
    {
        private readonly OnlineShoppingCart.DB.AsnetidentityContext _context;
    
        private readonly UserManager<IdentityUser> _userManager;
        public CartModel(OnlineShoppingCart.DB.AsnetidentityContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            viewCartList = new List<ViewCart>();
            _userManager = userManager;
        }

        public IList<Product> Product { get;set; }
        public List<ViewCart> viewCartList { set; get; }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                Cart cart = await _context.Cart.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                if (cart != null)
                {
                    IEnumerable<CartDetails> cartDetailsList = await _context.CartDetails.Where(x => x.CartId == cart.Id).ToListAsync();

                    foreach (CartDetails eachCartdetails in cartDetailsList)
                    {
                        var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == eachCartdetails.ProductId);

                        ViewCart viewCart = new ViewCart();
                        viewCart.product = product;
                        viewCart.cartDetails = eachCartdetails;
                        if (!viewCartList.Contains(viewCart))
                        {
                            viewCartList.Add(viewCart);
                        }
                    }
                }
            }
            return  Page();
        }
    }
}
