using eVote360.Application.Interfaces;
using eVote360.Domain.Entities;
using eVote360.Infrastructure.Persistence;
using eVote360.Infrastructure.Services;
using eVote360.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==============================
// MVC
// ==============================
builder.Services.AddControllersWithViews();

// ==============================
// DbContext (SQLite)
// ==============================
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ==============================
// Identity
// ==============================
builder.Services
    .AddIdentity<AppUser, IdentityRole>(options =>
    {
        // Password policy
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;

        // User / SignIn
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedAccount = false;

        // Lockout (recomendado)
        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
});

// ==============================
// ✅ Clean DI (Generic Service)
// Application depende de Interface.
// Infrastructure implementa con EF.
// ==============================
builder.Services.AddScoped(typeof(IGenericService<>), typeof(EfGenericService<>));

// ==============================
// Servicios propios Web
// ==============================
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();

// ==============================
// Seed Admin (por config)
// ==============================
builder.Services.Configure<SeedAdminOptions>(
    builder.Configuration.GetSection(SeedAdminOptions.SectionName)
);

var app = builder.Build();

// ==============================
// ✅ Seed roles + Admin garantizado
// ==============================
await IdentitySeeder.SeedAsync(app.Services);

// ==============================
// Middleware pipeline
// ==============================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// ==============================
// Routes
// ==============================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();