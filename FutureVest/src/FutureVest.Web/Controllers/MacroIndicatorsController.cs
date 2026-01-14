using FutureVest.Business.Services;
using FutureVest.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FutureVest.Web.Controllers;

public class MacroIndicatorsController : Controller
{
    private readonly MacroIndicatorService _service;

    public MacroIndicatorsController(MacroIndicatorService service)
    {
        _service = service;
    }

    // GET: MacroIndicators
    public async Task<IActionResult> Index()
    {
        var list = await _service.GetAllAsync();
        return View(list);
    }

    // GET: MacroIndicators/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _service.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        return View(entity);
    }

    // GET: MacroIndicators/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MacroIndicators/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MacroIndicator entity)
    {
        if (!ModelState.IsValid) return View(entity);

        await _service.CreateAsync(entity);
        return RedirectToAction(nameof(Index));
    }

    // GET: MacroIndicators/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _service.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        return View(entity);
    }

    // POST: MacroIndicators/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MacroIndicator entity)
    {
        if (id != entity.Id) return NotFound();

        if (!ModelState.IsValid) return View(entity);

        var exists = await _service.ExistsAsync(id);
        if (!exists) return NotFound();

        await _service.UpdateAsync(entity);
        return RedirectToAction(nameof(Index));
    }

    // GET: MacroIndicators/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _service.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        return View(entity);
    }

    // POST: MacroIndicators/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}