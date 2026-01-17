using eVote360.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eVote360.Web.Services;

public static class IdentitySeeder
{
    private static readonly string[] Roles = { "Admin", "Votante" };

    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<SeedAdminOptions>>().Value;

        // 1) Roles base
        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var created = await roleManager.CreateAsync(new IdentityRole(role));
                if (!created.Succeeded)
                {
                    var errors = string.Join(" | ", created.Errors.Select(e => e.Description));
                    throw new Exception($"No se pudo crear el rol '{role}'. Detalles: {errors}");
                }
            }
        }

        // 2) Validación de config
        if (string.IsNullOrWhiteSpace(options.Email))
            return; // si no configuras SeedAdmin, no hace nada extra

        var email = options.Email.Trim().ToLowerInvariant();

        // 3) Buscar por email (principal) o por username
        var user = await userManager.FindByEmailAsync(email)
                   ?? await userManager.FindByNameAsync(email);

        // 4) Si no existe, lo creamos
        if (user is null)
        {
            if (string.IsNullOrWhiteSpace(options.Password))
                throw new Exception("SeedAdmin.Password está vacío. Se requiere para crear el Admin inicial.");

            user = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                FullName = options.FullName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var created = await userManager.CreateAsync(user, options.Password);
            if (!created.Succeeded)
            {
                var errors = string.Join(" | ", created.Errors.Select(e => e.Description));
                throw new Exception($"No se pudo crear el usuario Admin '{email}'. Detalles: {errors}");
            }
        }
        else
        {
            // Si existe, aseguro datos básicos
            var changed = false;

            if (!string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
            {
                user.Email = email;
                changed = true;
            }

            if (!string.Equals(user.UserName, email, StringComparison.OrdinalIgnoreCase))
            {
                user.UserName = email;
                changed = true;
            }

            if (!user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                changed = true;
            }

            if (!user.IsActive)
            {
                user.IsActive = true;
                changed = true;
            }

            if (string.IsNullOrWhiteSpace(user.FullName))
            {
                user.FullName = options.FullName;
                changed = true;
            }

            if (changed)
            {
                var updated = await userManager.UpdateAsync(user);
                if (!updated.Succeeded)
                {
                    var errors = string.Join(" | ", updated.Errors.Select(e => e.Description));
                    throw new Exception($"No se pudo actualizar el usuario '{email}'. Detalles: {errors}");
                }
            }
        }

        // 5) Garantizar rol Admin
        if (!await userManager.IsInRoleAsync(user, "Admin"))
        {
            var addRole = await userManager.AddToRoleAsync(user, "Admin");
            if (!addRole.Succeeded)
            {
                var errors = string.Join(" | ", addRole.Errors.Select(e => e.Description));
                throw new Exception($"No se pudo asignar rol Admin al usuario '{email}'. Detalles: {errors}");
            }
        }

        // 6) (Opcional) Garantiza que no esté también como Votante si no quieres doble rol
        //    Si quieres permitir ambos roles, comenta este bloque.
        if (await userManager.IsInRoleAsync(user, "Votante"))
        {
            await userManager.RemoveFromRoleAsync(user, "Votante");
        }
    }
}