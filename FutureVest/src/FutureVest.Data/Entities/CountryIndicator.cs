using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutureVest.Data.Entities;

public class CountryIndicator
{
    public int Id { get; set; }

    [Required]
    public int CountryId { get; set; }
    public Country? Country { get; set; }

    [Required]
    public int MacroIndicatorId { get; set; }
    public MacroIndicator? MacroIndicator { get; set; }

    [Range(2020, 2026)]
    public int Year { get; set; }

    [Column(TypeName = "REAL")]
    public double? Value { get; set; } // null si missing

    public bool Missing { get; set; } = false;
}