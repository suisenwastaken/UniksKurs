using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using l4Razor.Data;
using l4Razor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace l4Razor.Pages.Shared
{
    public class NewContent : PageModel
    {
        private readonly NewDbContext _context;

        public NewContent(NewDbContext context)
        {
            _context = context;
        }

        public PageResult Page(IEnumerable<SelectListItem> _tagList)
        {
            TagList = _tagList;
            return new PageResult();
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(_context.TagsList.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.TagName }));
            }

            foreach (var item in SelectedTags)
            {
                var temp = _context.TagsList.FirstOrDefault(t => t.Id == item);
                New.TagsList.Add(temp);
                
            }

            if (New.NewsType == 0)
            {
                New.TakePlace = DateTime.MinValue;
            }
            

            New.Created = DateTime.Now;
            await _context.News.AddAsync(New);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
        
        [BindProperty]
        public New New { get; set; }
        
        [BindProperty]
        public List<int> SelectedTags { get; set; }
        
        public IEnumerable<SelectListItem> TagList { get; set; } = Enumerable.Empty<SelectListItem>();
        
        public IActionResult OnGet()
        {
            //var tags = Select(t => t);//from m in _context.TagsList select m;
            TagList = _context.TagsList.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.TagName });
            return Page();
        }
        
    }
}