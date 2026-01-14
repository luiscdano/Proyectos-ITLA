using PredictorActivos.Business.DTOs;

namespace PredictorActivos.Business.Services;

public interface IPredictorService
{
    PredictionResultDto Predict(List<PricePointDto> series);
}