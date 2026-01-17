using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using eVote360.Application.Interfaces;
using eVote360.Domain.Entities;
using eVote360.Infrastructure.Persistence;
using eVote360.Web.ViewModels;

namespace eVote360.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CandidaturasController : Controller
    {
        private readonly IGenericService<Candidatura> _candidaturas;
        private readonly IGenericService<Eleccion> _elecciones;
        private readonly IGenericService<PuestoElectivo> _puestos;
        private readonly IGenericService<PartidoPolitico> _partidos;

        // ✅ solo para Includes (lectura)
        private readonly AppDbContext _db;

        public CandidaturasController(
            IGenericService<Candidatura> candidaturas,
            IGenericService<Eleccion> elecciones,
            IGenericService<PuestoElectivo> puestos,
            IGenericService<PartidoPolitico> partidos,
            AppDbContext db)
        {
            _candidaturas = candidaturas;
            _elecciones = elecciones;
            _puestos = puestos;
            _partidos = partidos;
            _db = db;
        }

        // GET: Candidaturas
        public async Task<IActionResult> Index()
        {
            var list = await _db.Candidaturas
                .AsNoTracking()
                .Include(c => c.Eleccion)
                .Include(c => c.PuestoElectivo)
                .Include(c => c.PartidoPolitico)
                .OrderByDescending(c => c.Eleccion!.Anio)
                .ThenBy(c => c.PuestoElectivo!.Nombre)
                .ThenBy(c => c.PartidoPolitico!.Siglas)
                .ThenBy(c => c.NombreCompleto)
                .ToListAsync();

            return View(list);
        }

        // GET: Candidaturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var entity = await _db.Candidaturas
                .AsNoTracking()
                .Include(c => c.Eleccion)
                .Include(c => c.PuestoElectivo)
                .Include(c => c.PartidoPolitico)
                .FirstOrDefaultAsync(c => c.Id == id.Value);

            if (entity is null) return NotFound();

            return View(entity);
        }

        // GET: Candidaturas/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CandidaturaFormVM { IsActive = true };
            await LoadCombosAsync(vm);
            return View(vm);
        }

        // POST: Candidaturas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidaturaFormVM vm)
        {
            if (!ModelState.IsValid)
            {
                await LoadCombosAsync(vm);
                return View(vm);
            }

            var entity = new Candidatura
            {
                EleccionId = vm.EleccionId,
                PuestoElectivoId = vm.PuestoElectivoId,
                PartidoPoliticoId = vm.PartidoPoliticoId,
                NombreCompleto = vm.NombreCompleto.Trim(),
                NombreBoleta = string.IsNullOrWhiteSpace(vm.NombreBoleta) ? null : vm.NombreBoleta.Trim(),
                FotoPath = string.IsNullOrWhiteSpace(vm.FotoPath) ? null : vm.FotoPath.Trim(),
                IsActive = vm.IsActive
            };

            try
            {
                await _candidaturas.AddAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex) when (IsUniqueCandidaturaConstraint(ex))
            {
                ModelState.AddModelError(string.Empty,
                    "Ya existe una candidatura con la misma combinación (Elección + Puesto + Partido). Cambia alguno de los 3.");

                await LoadCombosAsync(vm);
                return View(vm);
            }
        }

        // GET: Candidaturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var entity = await _candidaturas.GetByIdAsync(id.Value);
            if (entity is null) return NotFound();

            var vm = new CandidaturaFormVM
            {
                Id = entity.Id,
                EleccionId = entity.EleccionId,
                PuestoElectivoId = entity.PuestoElectivoId,
                PartidoPoliticoId = entity.PartidoPoliticoId,
                NombreCompleto = entity.NombreCompleto,
                NombreBoleta = entity.NombreBoleta,
                FotoPath = entity.FotoPath,
                IsActive = entity.IsActive
            };

            await LoadCombosAsync(vm);
            return View(vm);
        }

        // POST: Candidaturas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CandidaturaFormVM vm)
        {
            if (id != vm.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                await LoadCombosAsync(vm);
                return View(vm);
            }

            var entity = await _candidaturas.GetByIdAsync(id);
            if (entity is null) return NotFound();

            entity.EleccionId = vm.EleccionId;
            entity.PuestoElectivoId = vm.PuestoElectivoId;
            entity.PartidoPoliticoId = vm.PartidoPoliticoId;
            entity.NombreCompleto = vm.NombreCompleto.Trim();
            entity.NombreBoleta = string.IsNullOrWhiteSpace(vm.NombreBoleta) ? null : vm.NombreBoleta.Trim();
            entity.FotoPath = string.IsNullOrWhiteSpace(vm.FotoPath) ? null : vm.FotoPath.Trim();
            entity.IsActive = vm.IsActive;

            try
            {
                await _candidaturas.UpdateAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex) when (IsUniqueCandidaturaConstraint(ex))
            {
                ModelState.AddModelError(string.Empty,
                    "Ya existe una candidatura con la misma combinación (Elección + Puesto + Partido). Cambia alguno de los 3.");

                await LoadCombosAsync(vm);
                return View(vm);
            }
        }

        // GET: Candidaturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var entity = await _candidaturas.GetByIdAsync(id.Value);
            if (entity is null) return NotFound();

            return View(entity);
        }

        // POST: Candidaturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _candidaturas.GetByIdAsync(id);
            if (entity is null) return NotFound();

            await _candidaturas.DeleteAsync(entity);
            return RedirectToAction(nameof(Index));
        }

        // ----------------- Helpers -----------------

        private async Task LoadCombosAsync(CandidaturaFormVM vm)
        {
            var elecciones = await _elecciones.GetAllAsync();
            var puestos = await _puestos.GetAllAsync();
            var partidos = await _partidos.GetAllAsync();

            vm.Elecciones = elecciones
                .OrderByDescending(e => e.Anio)
                .ThenByDescending(e => e.Fecha)
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = $"{e.Tipo} {e.Anio} ({e.Fecha:yyyy-MM-dd})"
                }).ToList();

            vm.Puestos = puestos
                .OrderBy(p => p.Nombre)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nombre
                }).ToList();

            vm.Partidos = partidos
                .Where(p => p.IsActive)
                .OrderBy(p => p.Siglas)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.Siglas} - {p.Nombre}"
                }).ToList();

            // placeholders
            vm.Elecciones.Insert(0, new SelectListItem { Value = "", Text = "— Seleccione —" });
            vm.Puestos.Insert(0, new SelectListItem { Value = "", Text = "— Seleccione —" });
            vm.Partidos.Insert(0, new SelectListItem { Value = "", Text = "— Seleccione —" });
        }

        private static bool IsUniqueCandidaturaConstraint(DbUpdateException ex)
        {
            var msg = ex.InnerException?.Message ?? ex.Message;
            return msg.Contains("UNIQUE constraint failed", StringComparison.OrdinalIgnoreCase)
                && msg.Contains("Candidaturas", StringComparison.OrdinalIgnoreCase);
        }
    }
}