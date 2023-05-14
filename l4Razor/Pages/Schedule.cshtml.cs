using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace l4Razor.Pages
{
    public class Schedule : PageModel
    {
        public string? PictureUrl { get; set; }
        public string? UserName { get; set; }
        
        public void OnGet()
        {
            UserName = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Surname).Select(c => c.Value).SingleOrDefault();
            PictureUrl = HttpContext.User.Claims.IsNullOrEmpty() ? "https://api.dicebear.com/6.x/big-smile/svg?seed=Loki&scale=110&radius=50&accessories[]&eyes=normal&hair[]&hairColor[]&mouth=awkwardSmile&skinColor=efcc9f&backgroundColor=b6e3f4" : HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Webpage).Select(c => c.Value).SingleOrDefault();
        }
        
        public string HelloUser()
        {
            return UserName ?? "Гость";
        }
    }
}