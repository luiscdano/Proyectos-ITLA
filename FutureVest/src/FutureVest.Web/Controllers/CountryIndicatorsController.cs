using System.Linq;
using System.Threading.Tasks;
using FutureVest.Data.Entities;
using FutureVest.Data.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureVest.Web.Controllers;

public class CountryIndicatorsController : Controller
{
    private readonly FutureVestDbContext _db;

    public CountryIndicatorsController(FutureVestDbContext db)
    {
        _db = db;
    }

    // GET: CountryIndicators
    public async Task<IActionResult> Index(int? countryId, int? macroIndicatorId, int? year)
    {
        var query = _db.CountryIndicators
            .AsNoTracking()
            .Include(ci => ci.Country)
            .Include(ci => ci.MacroIndicator)
            .AsQueryable();

        if (countryId.HasValue)
            query = query.Where(ci => ci.CountryId == countryId.Value);

        if (macroIndicatorId.HasValue)
            query = query.Where(ci => ci.MacroIndicatorId == macroIndicatorId.Value);

        if (year.HasValue)
            query = query.Where(ci => ci.Year == year.Value);

        var list = await query
            .OrderByDescending(ci => ci.Year)
            .ThenBy(ci => ci.Country!.Name)
            .ThenBy(ci => ci.MacroIndicator!.Name)
            .ToListAsync();

        // Filtros para la vista
        ViewBag.Countries = new SelectList(
            await _db.Countries.AsNoTracking().OrderBy(c => c.Name).ToListAsync(),
            "Id", "Name", countryId);

        ViewBag.MacroIndicators = new SelectList(
            await _db.MacroIndicators.AsNoTracking().OrderBy(m => m.Name).ToListAsync(),
            "Id", "Name", macroIndicatorId);

        ViewBag.Years = new SelectList(
            Enumerable.Range(2020, 7).Select(y => new { Id = y, Name = y.ToString() }),
            "Id", "Name", year);

        return View(list);
    }

    // GET: CountryIndicators/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var entity = await _db.CountryIndicators
            .AsNoTracking()
            .Include(ci => ci.Country)
            .Include(ci => ci.MacroIndicator)
            .FirstOrDefaultAsync(ci => ci.Id == id.Value);

        if (entity == null) return NotFound();

        return View(entity);
    }

    // GET: CountryIndicators/Create
    public async Task<IActionResult> Create()
    {
        await LoadDropdownsAsync();
        return View(new CountryIndicator { Year = 2026, Missing = false });
    }

    // POST: CountryIndicators/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CountryIndicator model)
    {
        // Si Missing = true => Value debe ser null
        if (model.Missing) model.Value = null;

        // Validación del índice único (CountryId + MacroIndicatorId + Year)
        var exists = await _db.CountryIndicators.AnyAsync(ci =>
            ci.CountryId == model.CountryId &&
            ci.MacroIndicatorId == model.MacroIndicatorId &&
            ci.Year == model.Year);

        if (exists)
            ModelState.AddModelError(string.Empty, "Ya existe un registro para ese Country + MacroIndicator + Year.");

        if (ModelState.IsValid)
        {
            _db.CountryIndicators.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        await LoadDropdownsAsync(model.CountryId, model.MacroIndicatorId, model.Year);
        return View(model);
    }

    // GET: CountryIndicators/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var entity = await _db.CountryIndicators.FindAsync(id.Value);
        if (entity == null) return NotFound();

        await LoadDropdownsAsync(entity.CountryId, entity.MacroIndicatorId, entity.Year);
        return View(entity);
    }

    // POST: CountryIndicators/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CountryIndicator model)
    {
        if (id != model.Id) return NotFound();

        if (model.Missing) model.Value = null;

        // Validación del índice único excluyendo el mismo Id
        var exists = await _db.CountryIndicators.AnyAsync(ci =>
            ci.Id != model.Id &&
            ci.CountryId == model.CountryId &&
            ci.MacroIndicatorId == model.MacroIndicatorId &&
            ci.Year == model.Year);

        if (exists)
            ModelState.AddModelError(string.Empty, "Ya existe un registro para ese Country + MacroIndicator + Year.");

        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(model);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                var stillExists = await _db.CountryIndicators.AnyAsync(ci => ci.Id == model.Id);
                if (!stillExists) return NotFound();
                throw;
            }
        }

        await LoadDropdownsAsync(model.CountryId, model.MacroIndicatorId, model.Year);
        return View(model);
    }

    // GET: CountryIndicators/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var entity = await _db.CountryIndicators
            .AsNoTracking()
            .Include(ci => ci.Country)
            .Include(ci => ci.MacroIndicator)
            .FirstOrDefaultAsync(ci => ci.Id == id.Value);

        if (entity == null) return NotFound();

        return View(entity);
    }

    // POST: CountryIndicators/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await _db.CountryIndicators.FindAsync(id);
        if (entity != null)
        {
            _db.CountryIndicators.Remove(entity);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropdownsAsync(int? countryId = null, int? macroIndicatorId = null, int? year = null)
    {
        ViewBag.Countries = new SelectList(
            await _db.Countries.AsNoTracking().OrderBy(c => c.Name).ToListAsync(),
            "Id", "Name", countryId);

        // MacroIndicators: muestro "Name (Code)" en dropdown
        var macro = await _db.MacroIndicators.AsNoTracking()
            .OrderBy(m => m.Name)
            .Select(m => new { m.Id, Text = m.Name + " (" + m.Code + ")" })
            .ToListAsync();

        ViewBag.MacroIndicators = new SelectList(macro, "Id", "Text", macroIndicatorId);

        ViewBag.Years = new SelectList(
            Enumerable.Range(2020, 7).Select(y => new { Id = y, Name = y.ToString() }),
            "Id", "Name", year);
    }
}