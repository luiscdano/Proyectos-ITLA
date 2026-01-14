using PredictorActivos.Business.DTOs;

namespace PredictorActivos.Business.Services;

public class MomentumRocService : IPredictorService
{
    private const int N = 5; // fijo según documento

    public PredictionResultDto Predict(List<PricePointDto> series)
    {
        // Del más antiguo al más reciente
        var ordered = series.AsEnumerable().Reverse().ToList();

        var rows = new List<Dictionary<string, string>>();
        double? lastRoc = null;

        for (int t = 0; t < ordered.Count; t++)
        {
            string rocValue = "n/a";

            if (t >= N)
            {
                double vt = (double)ordered[t].Value;
                double vtn = (double)ordered[t - N].Value;

                double roc = (vt / vtn - 1) * 100;
                lastRoc = roc;
                rocValue = $"{roc:F2}%";
            }

            rows.Add(new Dictionary<string, string>
            {
                { "t", t.ToString() },
                { "Fecha", ordered[t].Date.ToString("yyyy-MM-dd") },
                { "Precio", ordered[t].Value.ToString("F2") },
                { "ROC(5)", rocValue }
            });
        }

        string trend = (lastRoc.HasValue && lastRoc.Value > 0)
            ? "Alcista"
            : "Bajista";

        return new PredictionResultDto
        {
            Mode = PredictionMode.MomentumRoc,
            Trend = trend,
            Summary = $"ROC(5) más reciente = {(lastRoc.HasValue ? lastRoc.Value.ToString("F2") + "%" : "n/a")} → Tendencia {trend}",
            Metrics = new Dictionary<string, string>
            {
                { "Periodo (n)", N.ToString() },
                { "ROC Último", lastRoc.HasValue ? lastRoc.Value.ToString("F2") + "%" : "n/a" }
            },
            Rows = rows
        };
    }
}