using System.ComponentModel.DataAnnotations;

namespace PredictorActivos.Business.DTOs;

public enum PredictionMode
{
    [Display(Name = "Media Móvil Simple (SMA) Crossover")]
    SmaCrossover = 1,

    [Display(Name = "Regresión Lineal")]
    LinearRegression = 2,

    [Display(Name = "Momentum (ROC)")]
    MomentumRoc = 3
} 