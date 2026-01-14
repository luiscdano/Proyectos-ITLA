using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FutureVest.Data.Persistence;
using FutureVest.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutureVest.Web.Controllers;

public class DashboardController : Controller
{
    private readonly FutureVestDbContext _db;

    public DashboardController(FutureVestDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var vm = new DashboardViewModel();

        vm.CountriesCount = await _db.Countries.AsNoTracking().CountAsync();
        vm.MacroIndicatorsCount = await _db.MacroIndicators.AsNoTracking().CountAsync();
        vm.CountryIndicatorsCount = await _db.CountryIndicators.AsNoTracking().CountAsync();

        var missingCount = await _db.CountryIndicators.AsNoTracking().CountAsync(x => x.Missing);
        vm.MissingPercent = vm.CountryIndicatorsCount == 0
            ? 0
            : Math.Round((missingCount * 100.0) / vm.CountryIndicatorsCount, 2);

        vm.LatestYear = await _db.CountryIndicators
            .AsNoTracking()
            .MaxAsync(x => (int?)x.Year) ?? 0;

        // Si no hay data aún, devolvemos solo las cards.
        var year = vm.LatestYear;
        if (year == 0)
        {
            vm.TopCountries = new List<TopCountryScoreRow>();
            return View(vm);
        }

        // Cargar macros (weights + HigherIsBetter)
        var macros = await _db.MacroIndicators.AsNoTracking().ToListAsync();
        if (macros.Count == 0)
        {
            vm.TopCountries = new List<TopCountryScoreRow>();
            return View(vm);
        }

        // Traer data del año más reciente
        var data = await _db.CountryIndicators.AsNoTracking()
            .Where(x => x.Year == year && !x.Missing && x.Value != null)
            .Include(x => x.Country)
            .Include(x => x.MacroIndicator)
            .ToListAsync();

        if (data.Count == 0)
        {
            vm.TopCountries = new List<TopCountryScoreRow>();
            return View(vm);
        }

        // min/max por indicador
        var minMax = data
            .GroupBy(x => x.MacroIndicatorId)
            .ToDictionary(
                g => g.Key,
                g => new MinMax(
                    g.Min(v => v.Value!.Value),
                    g.Max(v => v.Value!.Value)
                )
            );

        // Score por país
        // Score = sum(weight * norm) donde norm in [0..1], invertido si HigherIsBetter=false
        var scores = new Dictionary<int, CountryScoreAccum>();

        foreach (var row in data)
        {
            var cId = row.CountryId;
            var m = row.MacroIndicator!;
            if (!minMax.TryGetValue(row.MacroIndicatorId, out var mm))
                continue;

            var range = mm.Max - mm.Min;
            if (range <= 0.000001)
                continue; // evita división por cero

            var raw = row.Value!.Value;

            // normalized 0..1
            var norm = m.HigherIsBetter
                ? (raw - mm.Min) / range
                : (mm.Max - raw) / range;

            if (!scores.TryGetValue(cId, out var acc))
                acc = new CountryScoreAccum();

            acc.Score += (m.Weight * norm);
            acc.Used += 1;

            scores[cId] = acc;
        }

        // map id->country
        var countries = await _db.Countries.AsNoTracking().ToDictionaryAsync(c => c.Id);

        vm.TopCountries = scores
            .Where(kvp => countries.ContainsKey(kvp.Key))
            .Select(kvp =>
            {
                var c = countries[kvp.Key];
                return new TopCountryScoreRow
                {
                    Country = c.Name,
                    Iso3 = c.Iso3,
                    Year = year,
                    Score = Math.Round(kvp.Value.Score * 100.0, 2), // 0..100
                    IndicatorsUsed = kvp.Value.Used
                };
            })
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x => x.IndicatorsUsed)
            .ThenBy(x => x.Country)
            .Take(10)
            .ToList();

        return View(vm);
    }

    private readonly record struct MinMax(double Min, double Max);

    private sealed class CountryScoreAccum
    {
        public double Score { get; set; }
        public int Used { get; set; }
    }
}