using eVote360.Application.Interfaces;
using eVote360.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PartidosPoliticosController : Controller
    {
        private readonly IGenericService<PartidoPolitico> _service;

        public PartidosPoliticosController(IGenericService<PartidoPolitico> service)
        {
            _service = service;
        }

        // GET: PartidosPoliticos
        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            return View(items);
        }

        // GET: PartidosPoliticos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: PartidosPoliticos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PartidosPoliticos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartidoPolitico partidoPolitico)
        {
            if (!ModelState.IsValid) return View(partidoPolitico);

            await _service.AddAsync(partidoPolitico);
            return RedirectToAction(nameof(Index));
        }

        // GET: PartidosPoliticos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: PartidosPoliticos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PartidoPolitico partidoPolitico)
        {
            if (id != partidoPolitico.Id) return BadRequest();
            if (!ModelState.IsValid) return View(partidoPolitico);

            await _service.UpdateAsync(partidoPolitico);
            return RedirectToAction(nameof(Index));
        }

        // GET: PartidosPoliticos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: PartidosPoliticos/Delete/5
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