using System.ComponentModel.DataAnnotations;
using PredictorActivos.Business.DTOs;

namespace PredictorActivos.Web.ViewModels;

public class ModeViewModel
{
    [Required]
    public PredictionMode SelectedMode { get; set; } = PredictionMode.SmaCrossover;
}