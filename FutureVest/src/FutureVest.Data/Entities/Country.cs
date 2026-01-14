using System.ComponentModel.DataAnnotations;

namespace FutureVest.Data.Entities;

public class Country
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = "";

    [Required, StringLength(3)]
    public string Iso3 { get; set; } = ""; // USA, SGP, DOM...

    [StringLength(80)]
    public string? Region { get; set; }

    public bool IsEligible { get; set; } = true;

    public ICollection<CountryIndicator> Indicators { get; set; } = new List<CountryIndicator>();
} 