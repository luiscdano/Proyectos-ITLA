using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eVote360.Infrastructure.Persistence;
using eVote360.Domain.Entities;

namespace eVote360.Web.Controllers
{
    public class EleccionesController : Controller
    {
        private readonly AppDbContext _context;

        public EleccionesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Elecciones
        public async Task<IActionResult> Index()
        {
            var items = await _context.Elecciones
                .AsNoTracking()
                .OrderByDescending(e => e.Anio)
                .ThenBy(e => e.Fecha)
                .ToListAsync();

            return View(items);
        }

        // GET: /Elecciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var eleccion = await _context.Elecciones
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (eleccion == null) return NotFound();

            return View(eleccion);
        }

        // GET: /Elecciones/Create
        public IActionResult Create()
        {
            // Valores por defecto útiles
            var model = new Eleccion
            {
                Anio = DateTime.Now.Year,
                Fecha = DateTime.Today,
                Tipo = "Presidenciales y Congresuales",
                IsActive = true
            };

            return View(model);
        }

        // POST: /Elecciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Anio,Tipo,Fecha,IsActive")] Eleccion eleccion)
        {
            if (!ModelState.IsValid) return View(eleccion);

            _context.Add(eleccion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Elecciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var eleccion = await _context.Elecciones.FindAsync(id);
            if (eleccion == null) return NotFound();

            return View(eleccion);
        }

        // POST: /Elecciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Anio,Tipo,Fecha,IsActive")] Eleccion eleccion)
        {
            if (id != eleccion.Id) return NotFound();
            if (!ModelState.IsValid) return View(eleccion);

            try
            {
                _context.Update(eleccion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EleccionExists(eleccion.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Elecciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var eleccion = await _context.Elecciones
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (eleccion == null) return NotFound();

            return View(eleccion);
        }

        // POST: /Elecciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eleccion = await _context.Elecciones.FindAsync(id);
            if (eleccion != null)
            {
                _context.Elecciones.Remove(eleccion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EleccionExists(int id)
        {
            return _context.Elecciones.Any(e => e.Id == id);
        }
    }
}