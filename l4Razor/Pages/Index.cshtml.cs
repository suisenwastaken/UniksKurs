using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using l4Razor.Data;
using l4Razor.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace l4Razor.Pages
{
    public class IndexModel : PageModel
    {
       
        private readonly NewDbContext _dbContext;

        public IndexModel(NewDbContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<New> News { get; set; } = Enumerable.Empty<New>();
        public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>();
        public string? UserName { get; set; } = String.Empty;
        public string? PictureUrl { get; set; }

        public ContuctUsMessage FeedbackMessage { get; set; } = new ContuctUsMessage();
        
        [BindProperty]
        public string UserMessage { get; set; }
        
        public  void OnGet()
        {
            News = _dbContext.News.Include(n => n.TagsList).OrderByDescending(o => o.Created).ToList();
            Tags = _dbContext.TagsList.ToList();
            //UserName = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // UserName = HttpContext.User.Identity?.Name;
            PictureUrl = HttpContext.User.Claims.IsNullOrEmpty() ? "https://api.dicebear.com/6.x/big-smile/svg?seed=Loki&scale=110&radius=50&accessories[]&eyes=normal&hair[]&hairColor[]&mouth=awkwardSmile&skinColor=efcc9f&backgroundColor=b6e3f4" : HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Webpage).Select(c => c.Value).SingleOrDefault();
            
            UserName = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Surname).Select(c => c.Value).SingleOrDefault();
        }
        
        public string HelloUser()
        {
            return UserName ?? "Гость";
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("Index");
            }
            
            UserName = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Surname).Select(c => c.Value).SingleOrDefault();

            FeedbackMessage.Name = UserName ?? "Гость";

            FeedbackMessage.Theme = "Что нового";
            FeedbackMessage.MessageText = UserMessage;
            FeedbackMessage.DateOfMessage = DateTime.Now;;

            await _dbContext.MessageList.AddAsync(FeedbackMessage);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("Index");
        }
        
    }
}