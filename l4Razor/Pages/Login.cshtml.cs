using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using l4Razor.Data;
using l4Razor.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace l4Razor.Pages
{
    public class Login : PageModel
    {
        private readonly IConfiguration configuration;

        public readonly NewDbContext _context;
        
        public Login(NewDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        [BindProperty]
        public User User { get; set; }
        
       
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var user = await _context.UserList.FirstOrDefaultAsync(u => u.UserName == User.UserName);
            if (user is null)
            {
                ModelState.AddModelError("IncorrectPassword", "Неправильный логин или пароль!");
                return Page();
            }
            if (VerifyHashedPassword(user.Password, User.Password))
            {
                await Authenticate(user.UserName, user.RoleId, user.FirstName, user.PictureUrl, user.DateOfRegistration);
            }
            else
            {
                ModelState.AddModelError("IncorrectPassword", "Неправильный логин или пароль!");
                return Page();
            }
            
            return RedirectToPage("Index");
        }
        
        async Task Authenticate(string userName, int? role, string firstName, string? pictureUrl, DateTime dateOfRegistration)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString()),
                new Claim(ClaimTypes.Surname, firstName),
                new Claim(ClaimTypes.Webpage, pictureUrl),
                new Claim(ClaimTypes.DateOfBirth, dateOfRegistration.ToString())
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType , ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return buffer3.SequenceEqual(buffer4);
        }
        
        
    }
}