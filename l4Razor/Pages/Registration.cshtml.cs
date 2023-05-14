using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using l4Razor.Data;
using l4Razor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace l4Razor.Pages
{
    public class Registration : PageModel
    {
        public readonly NewDbContext _context;

        public Registration(NewDbContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        public string src { get; set; } = "https://api.dicebear.com/6.x/big-smile/svg?seed=Fluffy&scale=110&radius=50&backgroundColor=b6e3f4";

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public List<int> SelectedCollectives { get; set; }
        
        public IEnumerable<SelectListItem> CollectivesList { get; set; } = Enumerable.Empty<SelectListItem>();
        
        public PageResult Page(IEnumerable<SelectListItem> collectiveList)
        {
            CollectivesList = collectiveList;
            return new PageResult();
        }
        
        public async Task<IActionResult> OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                return Page(_context.CollectiveList.Select(t => new SelectListItem {Value = t.Id.ToString(), Text = t.CollectiveName}));
            }

            User user = await _context.UserList.FirstOrDefaultAsync(u => u.UserName == User.UserName);
            if (user == null)
            {
                //User.Role = _context.RoleList.FirstOrDefault(r => r.Name == "User");

                foreach (var col in SelectedCollectives)
                {
                    var temp = _context.CollectiveList.FirstOrDefault(c => c.Id == col);
                    User.CollectiveList.Add(temp);
                }

                User.PictureUrl = src.Contains("https://api.dicebear.com") ? src : "https://api.dicebear.com/6.x/big-smile/svg?seed=Fluffy&scale=110&radius=50&backgroundColor=b6e3f4";

                User.DateOfRegistration = DateTime.Now;
                User.RoleId = 1;

                User.Password = HashPassword(User.Password);

                await _context.UserList.AddAsync(User);
                await _context.SaveChangesAsync();
                return RedirectToPage("Login");
            }
            ModelState.AddModelError("UserExist", "Такой пользователь зарегистрирован!");
            
            return Page(_context.CollectiveList.Select(t => new SelectListItem {Value = t.Id.ToString(), Text = t.CollectiveName}));
        }

        public IActionResult OnGet()
        {
            CollectivesList = _context.CollectiveList.Select(t => new SelectListItem {Value = t.Id.ToString(), Text = t.CollectiveName});
            return Page();
        }
        
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

    }
}