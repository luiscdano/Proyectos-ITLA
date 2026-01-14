using PredictorActivos.Business.DTOs;

namespace PredictorActivos.Business.Services;

public class LinearRegressionService : IPredictorService
{
    public PredictionResultDto Predict(List<PricePointDto> series)
    {
        // x = 1..20 (día), y = valor
        // series viene: más reciente primero. Invertimos para que el tiempo vaya de antiguo a reciente.
        var ordered = series.AsEnumerable().Reverse().ToList();

        int n = ordered.Count;
        double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;

        for (int i = 0; i < n; i++)
        {
            double x = i + 1; // 1..20
            double y = (double)ordered[i].Value;

            sumX += x;
            sumY += y;
            sumXY += x * y;
            sumX2 += x * x;
        }

        // m = (n*sumXY - sumX*sumY) / (n*sumX2 - sumX^2)
        double denom = (n * sumX2 - sumX * sumX);
        double m = denom == 0 ? 0 : (n * sumXY - sumX * sumY) / denom;

        // b = (sumY - m*sumX) / n
        double b = (sumY - m * sumX) / n;

        // Predicción para día 21
        double y21 = m * 21 + b;

        // Comparar con último valor (día 20)
        double y20 = (double)ordered.Last().Value;

        var trend = y21 > y20 ? "Alcista" : "Bajista";

        return new PredictionResultDto
        {
            Mode = PredictionMode.LinearRegression,
            Trend = trend,
            Summary = $"Predicción (día 21) = {y21:F2} | Último (día 20) = {y20:F2} | Pendiente m = {m:F4} → Tendencia {trend}",
            Metrics = new Dictionary<string, string>
            {
                { "Pendiente_m", m.ToString("F4") },
                { "Intercepto_b", b.ToString("F4") },
                { "Valor_Dia_21", y21.ToString("F2") }
            }
        };
    }
}