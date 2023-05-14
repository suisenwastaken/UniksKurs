using System;
using System.Threading.Tasks;
using l4Razor.Data;
using l4Razor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace l4Razor.Pages.Shared
{
    public class NewTag : PageModel
    {
        private readonly NewDbContext _context;

        public NewTag(NewDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page(); 
            
            await _context.TagsList.AddAsync(Tag);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
        
        [BindProperty]
        public Tag Tag { get; set; }
    }
}