using eVote360.Domain.Entities;
using eVote360.Infrastructure.Persistence;
using eVote360.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Web.Controllers
{
    [Authorize]
    public class VotosController : Controller
    {
        private readonly AppDbContext _db;

        public VotosController(AppDbContext db)
        {
            _db = db;
        }

        // GET: Votos
        public async Task<IActionResult> Index()
        {
            var votos = await _db.Votos
                .AsNoTracking()
                .Include(v => v.Eleccion)
                .Include(v => v.PuestoElectivo)
                .Include(v => v.Candidatura)
                    .ThenInclude(c => c!.PartidoPolitico)
                .OrderByDescending(v => v.FechaHora)
                .ToListAsync();

            return View(votos);
        }

        // GET: Votos/Create
        public async Task<IActionResult> Create()
        {
            var vm = new VotoFormVM();
            await LoadCombosAsync(vm);
            return View(vm);
        }

        // POST: Votos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VotoFormVM vm)
        {
            if (!ModelState.IsValid)
            {
                await LoadCombosAsync(vm);
                return View(vm);
            }

            var candidatura = await _db.Candidaturas
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == vm.CandidaturaId);

            if (candidatura == null)
            {
                ModelState.AddModelError(nameof(vm.CandidaturaId), "La candidatura seleccionada no existe.");
                await LoadCombosAsync(vm);
                return View(vm);
            }

            var voto = new Voto
            {
                EleccionId = vm.EleccionId,
                PuestoElectivoId = vm.PuestoElectivoId,
                CandidaturaId = vm.CandidaturaId,
                TokenVoto = string.IsNullOrWhiteSpace(vm.TokenVoto) ? Guid.NewGuid().ToString("N") : vm.TokenVoto.Trim(),
                FechaHora = DateTime.Now
            };

            _db.Votos.Add(voto);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Votos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var voto = await _db.Votos.FindAsync(id);
            if (voto == null) return NotFound();

            var vm = new VotoFormVM
            {
                EleccionId = voto.EleccionId,
                PuestoElectivoId = voto.PuestoElectivoId,
                CandidaturaId = voto.CandidaturaId,
                TokenVoto = voto.TokenVoto
            };

            await LoadCombosAsync(vm);
            return View(vm);
        }

        // POST: Votos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VotoFormVM vm)
        {
            var voto = await _db.Votos.FindAsync(id);
            if (voto == null) return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadCombosAsync(vm);
                return View(vm);
            }

            voto.EleccionId = vm.EleccionId;
            voto.PuestoElectivoId = vm.PuestoElectivoId;
            voto.CandidaturaId = vm.CandidaturaId;
            voto.TokenVoto = string.IsNullOrWhiteSpace(vm.TokenVoto) ? voto.TokenVoto : vm.TokenVoto.Trim();

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadCombosAsync(VotoFormVM vm)
        {
            vm.Elecciones = await _db.Elecciones
                .AsNoTracking()
                .OrderByDescending(e => e.Anio)
                .ThenByDescending(e => e.Fecha)
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = $"{e.Tipo} {e.Anio} ({e.Fecha:yyyy-MM-dd})"
                })
                .ToListAsync();

            vm.Puestos = await _db.PuestosElectivos
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nombre
                })
                .ToListAsync();

            vm.Candidaturas = await _db.Candidaturas
                .AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.NombreCompleto)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.NombreBoleta ?? c.NombreCompleto
                })
                .ToListAsync();

            vm.Elecciones.Insert(0, new SelectListItem { Value = "", Text = "— Seleccione —" });
            vm.Puestos.Insert(0, new SelectListItem { Value = "", Text = "— Seleccione —" });
            vm.Candidaturas.Insert(0, new SelectListItem { Value = "", Text = "— Seleccione —" });
        }
    }
}