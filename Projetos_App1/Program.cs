using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Projetos_App1.Helper;
using Projetos_App1.Models;
using Projetos_App1.Models.Repositories;
using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.Models.Services;
using Projetos_App1.Models.Services.Interfaces;
using Projetos_App1.ViewModels;
using Rotativa.AspNetCore;
using System.Configuration;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Home/Index/"); //401 - Unauthorized
        options.AccessDeniedPath = new PathString("/Home/Index/"); //403 - Forbidden
    });



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IComplaintRepository, ComplaintRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICompaniesCategoryRepository, CompaniesCategoryRepository>();
builder.Services.AddTransient<ICompanyRelationRepository, CompanyRelationRepository>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IWhistleblowingRepository, WhistleblowingRepository>();
builder.Services.AddTransient<IAttachedFileRepository, AttachedFileRepository>();
builder.Services.AddTransient<IComplaintService, ComplaintService >();
builder.Services.AddTransient<IWhistleblowingService, WhistleblowingService >();
builder.Services.AddTransient<IAttachedFileService, AttachedFileService >();
builder.Services.AddTransient<IEmail, Email >();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Privacy}/{id?}");



RotativaConfiguration.Setup(app.Environment.WebRootPath); app.UseRotativa();


app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always
});

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.Run();
