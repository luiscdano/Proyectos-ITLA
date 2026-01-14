using FutureVest.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FutureVest.Data.Persistence;

public class FutureVestDbContext : DbContext
{
    public FutureVestDbContext(DbContextOptions<FutureVestDbContext> options) : base(options) { }

    public DbSet<Country> Countries => Set<Country>();
    public DbSet<MacroIndicator> MacroIndicators => Set<MacroIndicator>();
    public DbSet<CountryIndicator> CountryIndicators => Set<CountryIndicator>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Iso3)
            .IsUnique();

        modelBuilder.Entity<MacroIndicator>()
            .HasIndex(m => m.Code)
            .IsUnique();

        // Evita duplicados por país+indicador+año
        modelBuilder.Entity<CountryIndicator>()
            .HasIndex(ci => new { ci.CountryId, ci.MacroIndicatorId, ci.Year })
            .IsUnique();
    }
}