using System.Net.Http;
using System.Security.Claims;
using l4Razor.Data;
using l4Razor.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
builder.Services.AddRazorPages(o =>
{
    o.Conventions.AuthorizePage("/PersonalPage");
    o.Conventions.AuthorizePage("/NewContent", "AdminPage");
    o.Conventions.AuthorizePage("/NewTag", "AdminPage");
    o.Conventions.AuthorizePage("/NewCollective", "AdminPage");
});

builder.Services.AddDbContext<NewDbContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Login";
        o.AccessDeniedPath = "/401";
    });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("AdminPage", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "2");
    });
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

/*app.Use(async (context, next) => 
{
    foreach (var cookie in context.Request.Cookies)
    {
        context.Response.Cookies.Delete(cookie.Key);
    }
    await next();
});*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
//106125836

//app.UseStatusCodePagesWithRedirects("{0}");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();

/*
 * Убрать привестивае, записат другой заголовок
 * padding в инпутах
 */
