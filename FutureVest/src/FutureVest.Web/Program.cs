using FutureVest.Business.Services;
using FutureVest.Data.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// DB
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<FutureVestDbContext>(opt => opt.UseSqlite(cs));

// Business services (DI)
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<MacroIndicatorService>();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Auto-migración (dev)
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<FutureVestDbContext>();
    db.Database.Migrate();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();