using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eVote360.Web.Models;
using eVote360.Web.Models.Home;
using eVote360.Infrastructure.Persistence;

namespace eVote360.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        // Puestos seed en tu DbContext:
        private const int PUESTO_PRESIDENCIA = 1;
        private const int PUESTO_VICEPRESIDENCIA = 5;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            // Partidos activos
            var partidos = await _db.PartidosPoliticos
                .AsNoTracking()
                .Where(p => p.IsActive)
                .OrderBy(p => p.Siglas)
                .ToListAsync();

            // Candidaturas de presidencia y vicepresidencia por partido
            var candidaturasPV = await _db.Candidaturas
                .AsNoTracking()
                .Where(c =>
                    c.IsActive &&
                    (c.PuestoElectivoId == PUESTO_PRESIDENCIA || c.PuestoElectivoId == PUESTO_VICEPRESIDENCIA))
                .Select(c => new
                {
                    c.PartidoPoliticoId,
                    c.PuestoElectivoId,
                    c.NombreBoleta,
                    c.NombreCompleto
                })
                .ToListAsync();

            // Conteo de votos por Partido (a través de la candidatura)
            var votosPorPartido = await _db.Votos
                .AsNoTracking()
                .Join(_db.Candidaturas.AsNoTracking(),
                      v => v.CandidaturaId,
                      c => c.Id,
                      (v, c) => new { c.PartidoPoliticoId })
                .GroupBy(x => x.PartidoPoliticoId)
                .Select(g => new
                {
                    PartidoPoliticoId = g.Key,
                    Votos = g.Count()
                })
                .ToListAsync();

            var totalVotos = votosPorPartido.Sum(x => x.Votos);

            // Dashboard
            var vm = new HomeDashboardViewModel
            {
                TotalVotos = totalVotos,
                TotalPartidos = partidos.Count,
                TotalCandidaturas = await _db.Candidaturas.AsNoTracking().CountAsync()
            };

            foreach (var p in partidos)
            {
                var pres = candidaturasPV.FirstOrDefault(x => x.PartidoPoliticoId == p.Id && x.PuestoElectivoId == PUESTO_PRESIDENCIA);
                var vice = candidaturasPV.FirstOrDefault(x => x.PartidoPoliticoId == p.Id && x.PuestoElectivoId == PUESTO_VICEPRESIDENCIA);

                var votos = votosPorPartido.FirstOrDefault(x => x.PartidoPoliticoId == p.Id)?.Votos ?? 0;

                var porcentaje = 0.0;
                if (totalVotos > 0)
                    porcentaje = (votos * 100.0) / totalVotos;

                vm.Partidos.Add(new PartyCardViewModel
                {
                    PartidoId = p.Id,
                    Nombre = p.Nombre,
                    Siglas = p.Siglas,
                    Presidente = pres?.NombreBoleta ?? pres?.NombreCompleto,
                    Vicepresidente = vice?.NombreBoleta ?? vice?.NombreCompleto,
                    Votos = votos,
                    Porcentaje = Math.Round(porcentaje, 1),
                    LogoPath = p.LogoPath
                });
            }

            // Orden por % desc
            vm.Partidos = vm.Partidos
                .OrderByDescending(x => x.Porcentaje)
                .ThenBy(x => x.Siglas)
                .ToList();

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}