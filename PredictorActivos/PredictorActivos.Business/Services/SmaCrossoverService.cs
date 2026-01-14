using PredictorActivos.Business.DTOs;

namespace PredictorActivos.Business.Services;

public class SmaCrossoverService : IPredictorService
{
    public PredictionResultDto Predict(List<PricePointDto> series)
    {
        // Asumimos que vienen 20 datos del más reciente al más antiguo.
        // Para SMA corta: 5 más recientes (los primeros 5).
        var smaShort = series.Take(5).Average(p => (double)p.Value);
        var smaLong = series.Average(p => (double)p.Value);

        var trend = smaShort > smaLong ? "Alcista" : "Bajista";

        return new PredictionResultDto
        {
            Mode = PredictionMode.SmaCrossover,
            Trend = trend,
            Summary = $"SMA corta (5) = {smaShort:F2} | SMA larga (20) = {smaLong:F2} → Tendencia {trend}",
            Metrics = new Dictionary<string, string>
            {
                { "SMA_Corta_5", smaShort.ToString("F2") },
                { "SMA_Larga_20", smaLong.ToString("F2") }
            }
        };
    }
}