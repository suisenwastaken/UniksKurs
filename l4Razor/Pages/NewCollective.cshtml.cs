using System;
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
    public class NewCollective : PageModel
    {
        private readonly NewDbContext _context;

        public NewCollective(NewDbContext context)
        {
            _context = context;
        }
        
        public IActionResult OnGet()
        {
            //var tags = Select(t => t);//from m in _context.TagsList select m;
            ThemeList = _context.CollectiveThemeList.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.ThemeName });
            InstituteList = _context.InstituteList.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.InstituteName });
            return Page();
        }

        public PageResult Page(IEnumerable<SelectListItem> themeList, IEnumerable<SelectListItem> instituteList)
        {
            ThemeList = themeList;
            InstituteList = instituteList;
            return new PageResult();
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                ThemeList = _context.CollectiveThemeList.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.ThemeName });
                InstituteList = _context.InstituteList.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.InstituteName });
                return Page(ThemeList, InstituteList);
            }

            Collective.ThemeOfCollective = _context.CollectiveThemeList.Find(SelectedTheme);
            Collective.InstituteOfCollective = _context.InstituteList.Find(SelectedInstitute);
            
            await _context.CollectiveList.AddAsync(Collective);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
        
        [BindProperty]
        public Collective Collective { get; set; }
        
        [BindProperty]
        public int SelectedTheme{ get; set; }
        
        [BindProperty]
        public int SelectedInstitute{ get; set; }
        
        public IEnumerable<SelectListItem> ThemeList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> InstituteList { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}