using System.ComponentModel.DataAnnotations;

namespace PredictorActivos.Web.ViewModels;

public class HomeInputViewModel
{
    [Required(ErrorMessage = "Debes ingresar exactamente 20 filas con formato YYYY-MM-DD, valor")]
    public string RawSeries { get; set; } = "";

    public string? ResultSummary { get; set; }

    public Dictionary<string, string>? Metrics { get; set; }

    // Para ROC
    public List<Dictionary<string, string>>? Rows { get; set; }

    public string? CurrentModeName { get; set; }
} 