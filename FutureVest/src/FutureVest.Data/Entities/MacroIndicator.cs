using System.ComponentModel.DataAnnotations;

namespace FutureVest.Data.Entities;

public class MacroIndicator
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = ""; // PIB, Inflación, etc.

    [Required, StringLength(40)]
    public string Code { get; set; } = ""; // GDP_GROWTH_PCT, INFLATION_PCT...

    [Range(0, 1)]
    public double Weight { get; set; } // Debe sumar 1 entre todos

    public bool HigherIsBetter { get; set; } = true; // Inflación/Desempleo normalmente false

    [StringLength(40)]
    public string? Unit { get; set; } // %, puntos, etc.

    public ICollection<CountryIndicator> CountryIndicators { get; set; } = new List<CountryIndicator>();
}