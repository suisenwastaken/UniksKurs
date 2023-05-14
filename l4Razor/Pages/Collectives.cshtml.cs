using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using l4Razor.Data;
using l4Razor.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace l4Razor.Pages
{
    public class Collectives : PageModel
    {
        
        private readonly NewDbContext _dbContext;

        public Collectives(NewDbContext context)
        {
            _dbContext = context;
        }
        
        public IEnumerable<Collective> CollectiveList { get; set; } = Enumerable.Empty<Collective>();
        public IEnumerable<CollectiveTheme> CollectiveThemes { get; set; } = Enumerable.Empty<CollectiveTheme>();
        public IEnumerable<Institute> Institutes { get; set; } = Enumerable.Empty<Institute>();

        public string? PictureUrl { get; set; }
        
        public void OnGet()
        {
            CollectiveList = _dbContext.CollectiveList.Include(i => i.InstituteOfCollective)
                .Include(t => t.ThemeOfCollective).OrderByDescending(n => n.CollectiveName).ToList();
            CollectiveThemes = _dbContext.CollectiveThemeList.ToList();
            Institutes = _dbContext.InstituteList.ToList();
            
            PictureUrl = HttpContext.User.Claims.IsNullOrEmpty() ? "https://api.dicebear.com/6.x/big-smile/svg?seed=Loki&scale=110&radius=50&accessories[]&eyes=normal&hair[]&hairColor[]&mouth=awkwardSmile&skinColor=efcc9f&backgroundColor=b6e3f4" : HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Webpage).Select(c => c.Value).SingleOrDefault();
            
        }
    }
}