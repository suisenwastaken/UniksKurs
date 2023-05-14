using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using l4Razor.Data;
using l4Razor.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace l4Razor.Pages
{
    public class PersonalPage : PageModel
    {
        private NewDbContext _dbContext;
        
        public PersonalPage(NewDbContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<ContuctUsMessage> Messages { get; set; } = Enumerable.Empty<ContuctUsMessage>();

        public IEnumerable<Collective> AllCollectives { get; set; } = Enumerable.Empty<Collective>();
        
        public List<Collective> Collectives { get; set; } = new List<Collective>();
        public IEnumerable<CollectiveTheme> CollectiveThemes { get; set; } = Enumerable.Empty<CollectiveTheme>();
        public IEnumerable<Institute> Institutes { get; set; } = Enumerable.Empty<Institute>();

        public User CurentUser { get; set; } = new User();
        public string? Login { get; set; } = String.Empty;
        public string? UserName { get; set; } = String.Empty;
        public string? DateOfregistration { get; set; }
        public string? RoleOfUser { get; set; }
        
        public string? PictureUrl { get; set; }
        
        
        public void OnGet()
        {
            RoleOfUser = User.Claims.Where(t => t.Type == ClaimsIdentity.DefaultRoleClaimType).Select(v => v.Value)
                .SingleOrDefault();
            
            AllCollectives = _dbContext.CollectiveList.Include(i => i.InstituteOfCollective)
                .Include(t => t.ThemeOfCollective).OrderByDescending(n => n.CollectiveName).ToList();
            CollectiveThemes = _dbContext.CollectiveThemeList.ToList();
            Institutes = _dbContext.InstituteList.ToList();
            

            Messages = _dbContext.MessageList.ToList();
            
            Login =  HttpContext.User.Claims.Where(c => c.Type == ClaimsIdentity.DefaultNameClaimType).Select(c => c.Value).SingleOrDefault();
            PictureUrl =  HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Webpage).Select(c => c.Value).SingleOrDefault();
            DateOfregistration =  HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.DateOfBirth).Select(c => c.Value).SingleOrDefault();
            UserName = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Surname).Select(c => c.Value).SingleOrDefault();
            
            CurentUser = _dbContext.UserList.Include(c => c.CollectiveList).FirstOrDefault(u => u.UserName == Login);
            foreach (var col in AllCollectives)
            {
                if (CurentUser.CollectiveList.Contains(col))
                {
                    Collectives.Add(col);
                }
            }
        }
        
        public string HelloUser()
        {
            if (UserName != null)
            {
                return ", " + UserName;
            }
            
            return String.Empty;
        }
        
        public async Task<IActionResult> OnPost()
        {
            await DeleteCookies();
            return RedirectToPage("Index");
        }

        private async Task DeleteCookies()
        {
            /*foreach (var cookie in HttpContext.Request.Cookies)
            {
                HttpContext.Response.Cookies.Delete(cookie.Key);
            }*/
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}