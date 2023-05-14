using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using l4Razor.Data;
using l4Razor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace l4Razor.Pages
{
    public class ContactUs : PageModel
    {

        private readonly NewDbContext _context;
        public string? UserName { get; set; }
        
       public ContactUs(NewDbContext context)
       {
           _context = context;
       }

       public void OnGet()
       {
           UserName = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Surname).Select(c => c.Value).SingleOrDefault();
       }

       public async Task<IActionResult> OnPost()
       {
           if (!ModelState.IsValid)
           {
               return Page();
           }
           
           Message.DateOfMessage = DateTime.Now;
           
           await _context.MessageList.AddAsync(Message);
           await _context.SaveChangesAsync();
           return RedirectToPage("Index");
       }
       
       [BindProperty]
       public ContuctUsMessage Message { get; set; }
       
    }
}