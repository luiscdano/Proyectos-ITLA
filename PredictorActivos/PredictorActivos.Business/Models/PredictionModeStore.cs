using PredictorActivos.Business.DTOs;

namespace PredictorActivos.Business.Models;

public sealed class PredictionModeStore
{
    private static readonly Lazy<PredictionModeStore> _instance =
        new(() => new PredictionModeStore());

    public static PredictionModeStore Instance => _instance.Value;

    private PredictionModeStore() { }

    public PredictionMode CurrentMode { get; private set; } = PredictionMode.SmaCrossover;

    public void SetMode(PredictionMode mode) => CurrentMode = mode;
}