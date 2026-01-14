using FutureVest.Data.Entities;
using FutureVest.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FutureVest.Business.Services;

public class MacroIndicatorService
{
    private readonly FutureVestDbContext _db;

    public MacroIndicatorService(FutureVestDbContext db)
    {
        _db = db;
    }

    public async Task<List<MacroIndicator>> GetAllAsync()
    {
        return await _db.MacroIndicators
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<MacroIndicator?> GetByIdAsync(int id)
    {
        return await _db.MacroIndicators.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.MacroIndicators.AnyAsync(m => m.Id == id);
    }

    public async Task CreateAsync(MacroIndicator entity)
    {
        _db.MacroIndicators.Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(MacroIndicator entity)
    {
        _db.MacroIndicators.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null) return;

        _db.MacroIndicators.Remove(entity);
        await _db.SaveChangesAsync();
    }
}