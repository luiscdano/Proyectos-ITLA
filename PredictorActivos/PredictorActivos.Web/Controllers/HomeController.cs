using Microsoft.AspNetCore.Mvc;
using PredictorActivos.Business.DTOs;
using PredictorActivos.Business.Models;
using PredictorActivos.Business.Services;
using PredictorActivos.Web.ViewModels;
using System.Globalization;

namespace PredictorActivos.Web.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var mode = PredictionModeStore.Instance.CurrentMode;
        return View(new HomeInputViewModel
        {
            CurrentModeName = ModeToText(mode),
            RawSeries = Sample20Lines()
        });
    }

    [HttpPost]
    public IActionResult Index(HomeInputViewModel vm)
    {
        var mode = PredictionModeStore.Instance.CurrentMode;
        vm.CurrentModeName = ModeToText(mode);

        if (!ModelState.IsValid)
            return View(vm);

        var parseResult = TryParseSeries(vm.RawSeries);

        if (!parseResult.Success)
        {
            ModelState.AddModelError(nameof(vm.RawSeries), parseResult.ErrorMessage!);
            return View(vm);
        }

        if (parseResult.Series!.Count != 20)
        {
            ModelState.AddModelError(nameof(vm.RawSeries), "Debes ingresar exactamente 20 filas (ni más ni menos).");
            return View(vm);
        }

        var service = PredictorFactory.Create(mode);
        var result = service.Predict(parseResult.Series);

        vm.ResultSummary = result.Summary;
        vm.Metrics = result.Metrics;
        vm.Rows = result.Rows;

        return View(vm);
    }

    private static (bool Success, List<PricePointDto>? Series, string? ErrorMessage) TryParseSeries(string raw)
    {
        var lines = raw.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                       .Select(l => l.Trim())
                       .Where(l => !string.IsNullOrWhiteSpace(l))
                       .ToList();

        if (lines.Count != 20)
            return (false, null, "Debes ingresar exactamente 20 filas.");

        var series = new List<PricePointDto>();

        foreach (var line in lines)
        {
            var parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
                return (false, null, $"Formato inválido: '{line}'. Usa: YYYY-MM-DD, valor");

            if (!DateOnly.TryParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return (false, null, $"Fecha inválida: '{parts[0]}'. Usa YYYY-MM-DD");

            if (!decimal.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                return (false, null, $"Valor inválido: '{parts[1]}'. Usa número decimal (ej: 150.35)");

            series.Add(new PricePointDto { Date = date, Value = value });
        }

        // Validación adicional: valores > 0
        if (series.Any(p => p.Value <= 0))
            return (false, null, "Los valores deben ser mayores que 0.");

        return (true, series, null);
    }

    private static string ModeToText(PredictionMode mode) => mode switch
    {
        PredictionMode.SmaCrossover => "Media Móvil Simple (SMA) Crossover",
        PredictionMode.LinearRegression => "Regresión Lineal",
        PredictionMode.MomentumRoc => "Momentum (ROC)",
        _ => "Media Móvil Simple (SMA) Crossover"
    };

    private static string Sample20Lines()
    {
        // ejemplo para que se vea rápido en UI
        // (20 filas)
        return string.Join("\n", Enumerable.Range(1, 20)
            .Select(i => $"2025-05-{i:00}, {150 + i * 0.5:F2}"));
    }
}