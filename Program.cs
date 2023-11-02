using Microsoft.EntityFrameworkCore;
using pimfo.data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true; // Pode exigir consentimento do usuário
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
        builder.Configuration.GetConnectionString("DbPath")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
