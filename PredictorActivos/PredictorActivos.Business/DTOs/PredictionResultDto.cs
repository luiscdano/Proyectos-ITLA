using System.Collections.Generic;

namespace PredictorActivos.Business.DTOs;

public class PredictionResultDto
{
    public PredictionMode Mode { get; set; }

    public string Trend { get; set; } = "";

    public string Summary { get; set; } = "";

    public Dictionary<string, string> Metrics { get; set; } = new();

    // Para mostrar tablas (ROC completo)
    public List<Dictionary<string, string>> Rows { get; set; } = new();
}