using eVote360.Application.Interfaces;
using eVote360.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PuestosElectivosController : Controller
    {
        private readonly IGenericService<PuestoElectivo> _service;

        public PuestosElectivosController(IGenericService<PuestoElectivo> service)
        {
            _service = service;
        }

        // GET: PuestosElectivos
        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            return View(items);
        }

        // GET: PuestosElectivos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: PuestosElectivos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PuestosElectivos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PuestoElectivo puestoElectivo)
        {
            if (!ModelState.IsValid) return View(puestoElectivo);

            await _service.AddAsync(puestoElectivo);
            return RedirectToAction(nameof(Index));
        }

        // GET: PuestosElectivos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: PuestosElectivos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PuestoElectivo puestoElectivo)
        {
            if (id != puestoElectivo.Id) return BadRequest();
            if (!ModelState.IsValid) return View(puestoElectivo);

            await _service.UpdateAsync(puestoElectivo);
            return RedirectToAction(nameof(Index));
        }

        // GET: PuestosElectivos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: PuestosElectivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _service.DeleteAsync(entity);
            return RedirectToAction(nameof(Index));
        }
    }
}