using FutureVest.Business.Services;
using FutureVest.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FutureVest.Web.Controllers;

public class CountriesController : Controller
{
    private readonly CountryService _service;

    public CountriesController(CountryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _service.GetAllAsync();
        return View(items);
    }

    public IActionResult Create()
    {
        return View(new Country { IsEligible = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Country model)
    {
        if (!ModelState.IsValid) return View(model);

        var (ok, error) = await _service.CreateAsync(model);
        if (!ok)
        {
            ModelState.AddModelError("", error);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Country model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        var (ok, error) = await _service.UpdateAsync(model);
        if (!ok)
        {
            ModelState.AddModelError("", error);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }
}