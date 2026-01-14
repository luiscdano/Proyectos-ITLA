using FutureVest.Data.Entities;

namespace FutureVest.Data.Persistence;

public static class DbSeeder
{
    public static void Seed(FutureVestDbContext db)
    {
        // 1) Countries (10+)
        if (!db.Countries.Any())
        {
            db.Countries.AddRange(new List<Country>
            {
                new() { Name = "República Dominicana", Iso3 = "DOM", Region = "Caribbean", IsEligible = true },
                new() { Name = "Estados Unidos",        Iso3 = "USA", Region = "North America", IsEligible = true },
                new() { Name = "Canadá",               Iso3 = "CAN", Region = "North America", IsEligible = true },
                new() { Name = "México",               Iso3 = "MEX", Region = "North America", IsEligible = true },
                new() { Name = "Colombia",             Iso3 = "COL", Region = "South America", IsEligible = true },
                new() { Name = "Brasil",               Iso3 = "BRA", Region = "South America", IsEligible = true },
                new() { Name = "Chile",                Iso3 = "CHL", Region = "South America", IsEligible = true },
                new() { Name = "Argentina",            Iso3 = "ARG", Region = "South America", IsEligible = true },
                new() { Name = "España",               Iso3 = "ESP", Region = "Europe", IsEligible = true },
                new() { Name = "Francia",              Iso3 = "FRA", Region = "Europe", IsEligible = true },
                new() { Name = "Alemania",             Iso3 = "DEU", Region = "Europe", IsEligible = true },
                new() { Name = "Japón",                Iso3 = "JPN", Region = "Asia", IsEligible = true },
            });

            db.SaveChanges();
        }

        // 2) MacroIndicators (6)
        if (!db.MacroIndicators.Any())
        {
            db.MacroIndicators.AddRange(new List<MacroIndicator>
            {
                // (HigherIsBetter: true) => mientras más alto, mejor
                // (HigherIsBetter: false) => mientras más bajo, mejor
                new() { Name = "GDP Growth",          Code = "GDPG", Weight = 0.22, HigherIsBetter = true,  Unit = "%"   },
                new() { Name = "Inflation",           Code = "INFL", Weight = 0.20, HigherIsBetter = false, Unit = "%"   },
                new() { Name = "Unemployment",        Code = "UNEM", Weight = 0.18, HigherIsBetter = false, Unit = "%"   },
                new() { Name = "Debt-to-GDP",         Code = "DEBT", Weight = 0.18, HigherIsBetter = false, Unit = "%"   },
                new() { Name = "Policy Rate",         Code = "RATE", Weight = 0.12, HigherIsBetter = false, Unit = "%"   },
                new() { Name = "FX Reserves Index",   Code = "FXRS", Weight = 0.10, HigherIsBetter = true,  Unit = "idx" },
            });

            db.SaveChanges();
        }

        // 3) CountryIndicators (2020–2026, con Missing real)
        // Evitar duplicar: si ya hay data, no sembrar de nuevo
        if (db.CountryIndicators.Any())
            return;

        var countries = db.Countries.ToList();
        var macros = db.MacroIndicators.ToList();

        // Semilla reproducible para valores "realistas"
        var rnd = new Random(42);

        // Valores base por indicador (aprox)
        // Luego ajustamos por año y por país para que no sea todo igual
        var baseByCode = new Dictionary<string, double>
        {
            ["GDPG"] = 3.2,   // %
            ["INFL"] = 6.5,   // %
            ["UNEM"] = 7.8,   // %
            ["DEBT"] = 55.0,  // %
            ["RATE"] = 6.0,   // %
            ["FXRS"] = 50.0,  // index 0..100
        };

        // Factor país (para variar un poco)
        double CountryFactor(string iso3) => iso3 switch
        {
            "USA" => 1.10,
            "CAN" => 1.05,
            "DEU" => 1.03,
            "JPN" => 0.98,
            "FRA" => 1.00,
            "ESP" => 0.99,
            "BRA" => 0.95,
            "ARG" => 0.85,
            "CHL" => 1.01,
            "COL" => 0.97,
            "MEX" => 0.96,
            "DOM" => 0.94,
            _ => 1.00
        };

        // “Shock” 2020 y recuperación gradual (solo para darle vida)
        double YearShock(int year) => year switch
        {
            2020 => -2.0,
            2021 => +1.2,
            2022 => +0.6,
            2023 => +0.3,
            2024 => +0.2,
            2025 => +0.1,
            2026 => 0.0,
            _ => 0.0
        };

        var indicators = new List<CountryIndicator>();

        foreach (var c in countries)
        {
            var cf = CountryFactor(c.Iso3);

            for (int year = 2020; year <= 2026; year++)
            {
                foreach (var m in macros)
                {
                    // 7% missing aprox
                    var missing = rnd.NextDouble() < 0.07;

                    double? value = null;
                    if (!missing)
                    {
                        var baseVal = baseByCode[m.Code];

                        // ruido controlado por indicador
                        var noise = m.Code switch
                        {
                            "GDPG" => (rnd.NextDouble() * 1.8) - 0.9,   // ±0.9
                            "INFL" => (rnd.NextDouble() * 2.5) - 1.25, // ±1.25
                            "UNEM" => (rnd.NextDouble() * 2.0) - 1.0,  // ±1.0
                            "DEBT" => (rnd.NextDouble() * 6.0) - 3.0,  // ±3.0
                            "RATE" => (rnd.NextDouble() * 2.0) - 1.0,  // ±1.0
                            "FXRS" => (rnd.NextDouble() * 8.0) - 4.0,  // ±4.0
                            _ => (rnd.NextDouble() * 2.0) - 1.0
                        };

                        // Ajuste por año (GDP/Infl/Rate se sienten más con shock)
                        var shock = YearShock(year);
                        var yearAdj = m.Code switch
                        {
                            "GDPG" => shock,
                            "INFL" => -shock * 0.6,  // cuando GDP cae, inflación a veces sube (simulado)
                            "RATE" => -shock * 0.4,
                            "UNEM" => -shock * 0.7,  // si GDP cae, desempleo tiende a subir
                            _ => 0.0
                        };

                        // Ajuste por país
                        var countryAdj = m.Code switch
                        {
                            "GDPG" => (cf - 1.0) * 2.2,
                            "INFL" => (1.0 - cf) * 3.0,
                            "UNEM" => (1.0 - cf) * 2.5,
                            "DEBT" => (1.0 - cf) * 10.0,
                            "RATE" => (1.0 - cf) * 2.0,
                            "FXRS" => (cf - 1.0) * 12.0,
                            _ => 0.0
                        };

                        value = baseVal + noise + yearAdj + countryAdj;

                        // Limpieza de rangos razonables
                        value = m.Code switch
                        {
                            "GDPG" => Math.Round(Clamp(value.Value, -6.0, 9.0), 2),
                            "INFL" => Math.Round(Clamp(value.Value, 0.0, 25.0), 2),
                            "UNEM" => Math.Round(Clamp(value.Value, 2.0, 30.0), 2),
                            "DEBT" => Math.Round(Clamp(value.Value, 10.0, 160.0), 2),
                            "RATE" => Math.Round(Clamp(value.Value, 0.0, 20.0), 2),
                            "FXRS" => Math.Round(Clamp(value.Value, 10.0, 95.0), 2),
                            _ => Math.Round(value.Value, 2)
                        };
                    }

                    indicators.Add(new CountryIndicator
                    {
                        CountryId = c.Id,
                        MacroIndicatorId = m.Id,
                        Year = year,
                        Missing = missing,
                        Value = missing ? null : value
                    });
                }
            }
        }

        db.CountryIndicators.AddRange(indicators);
        db.SaveChanges();
    }

    private static double Clamp(double value, double min, double max)
        => value < min ? min : (value > max ? max : value);
}