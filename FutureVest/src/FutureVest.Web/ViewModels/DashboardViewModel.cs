using System.Collections.Generic;

namespace FutureVest.Web.ViewModels;

public class DashboardViewModel
{
    // Cards
    public int CountriesCount { get; set; }
    public int MacroIndicatorsCount { get; set; }
    public int CountryIndicatorsCount { get; set; }
    public double MissingPercent { get; set; }
    public int LatestYear { get; set; }

    // Tabla Top 10
    public List<TopCountryScoreRow> TopCountries { get; set; } = new();
}

public class TopCountryScoreRow
{
    public string Country { get; set; } = string.Empty;
    public string Iso3 { get; set; } = string.Empty;
    public int Year { get; set; }
    public double Score { get; set; }
    public int IndicatorsUsed { get; set; }
}