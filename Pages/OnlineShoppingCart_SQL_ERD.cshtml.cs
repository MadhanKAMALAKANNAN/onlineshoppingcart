using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineShoppingCart.Pages
{
    [AllowAnonymous]
    public class Index1Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
