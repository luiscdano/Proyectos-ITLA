using PredictorActivos.Business.DTOs;

namespace PredictorActivos.Business.Services;

public static class PredictorFactory
{
    public static IPredictorService Create(PredictionMode mode) =>
        mode switch
        {
            PredictionMode.SmaCrossover => new SmaCrossoverService(),
            PredictionMode.LinearRegression => new LinearRegressionService(),
            PredictionMode.MomentumRoc => new MomentumRocService(),
            _ => new SmaCrossoverService()
        };
} 