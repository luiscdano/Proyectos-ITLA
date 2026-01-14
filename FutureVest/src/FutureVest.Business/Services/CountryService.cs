using FutureVest.Data.Entities;
using FutureVest.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FutureVest.Business.Services;

public class CountryService
{
    private readonly FutureVestDbContext _db;

    public CountryService(FutureVestDbContext db)
    {
        _db = db;
    }

    public async Task<List<Country>> GetAllAsync()
    {
        return await _db.Countries
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Country?> GetByIdAsync(int id)
    {
        return await _db.Countries.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<(bool ok, string error)> CreateAsync(Country country)
    {
        country.Name = country.Name.Trim();
        country.Iso3 = country.Iso3.Trim().ToUpper();

        // Validaciones mínimas pro
        if (country.Name.Length < 2) return (false, "Name demasiado corto.");
        if (country.Iso3.Length != 3) return (false, "Iso3 debe tener 3 letras.");
        if (await _db.Countries.AnyAsync(c => c.Iso3 == country.Iso3))
            return (false, "Iso3 ya existe.");

        _db.Countries.Add(country);
        await _db.SaveChangesAsync();
        return (true, "");
    }

    public async Task<(bool ok, string error)> UpdateAsync(Country country)
    {
        country.Name = country.Name.Trim();
        country.Iso3 = country.Iso3.Trim().ToUpper();

        if (country.Name.Length < 2) return (false, "Name demasiado corto.");
        if (country.Iso3.Length != 3) return (false, "Iso3 debe tener 3 letras.");

        if (await _db.Countries.AnyAsync(c => c.Iso3 == country.Iso3 && c.Id != country.Id))
            return (false, "Iso3 ya existe en otro país.");

        _db.Countries.Update(country);
        await _db.SaveChangesAsync();
        return (true, "");
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Countries.FirstOrDefaultAsync(c => c.Id == id);
        if (entity == null) return;

        _db.Countries.Remove(entity);
        await _db.SaveChangesAsync();
    }
}