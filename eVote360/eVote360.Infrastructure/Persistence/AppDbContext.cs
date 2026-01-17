using eVote360.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PartidoPolitico> PartidosPoliticos => Set<PartidoPolitico>();
        public DbSet<PuestoElectivo> PuestosElectivos => Set<PuestoElectivo>();
        public DbSet<Eleccion> Elecciones => Set<Eleccion>();
        public DbSet<Candidatura> Candidaturas => Set<Candidatura>();
        public DbSet<Voto> Votos => Set<Voto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // CONFIGURACIONES / INDICES
            // =========================
            modelBuilder.Entity<Candidatura>()
                .HasIndex(x => new { x.EleccionId, x.PuestoElectivoId, x.PartidoPoliticoId })
                .IsUnique();

            modelBuilder.Entity<Voto>()
                .HasIndex(x => new { x.EleccionId, x.PuestoElectivoId });

            // =========================
            // SEED BASE (PARTIDOS)
            // =========================
            modelBuilder.Entity<PartidoPolitico>().HasData(
                new PartidoPolitico
                {
                    Id = 1,
                    Nombre = "Partido Revolucionario Moderno",
                    Siglas = "PRM",
                    Descripcion = "Partido político (RD)",
                    IsActive = true,
                    LogoPath = null
                },
                new PartidoPolitico
                {
                    Id = 2,
                    Nombre = "Partido de la Liberación Dominicana",
                    Siglas = "PLD",
                    Descripcion = "Partido político (RD)",
                    IsActive = true,
                    LogoPath = null
                },
                new PartidoPolitico
                {
                    Id = 3,
                    Nombre = "Fuerza del Pueblo",
                    Siglas = "FP",
                    Descripcion = "Partido político (RD)",
                    IsActive = true,
                    LogoPath = null
                },
                new PartidoPolitico
                {
                    Id = 4,
                    Nombre = "Partido Revolucionario Dominicano",
                    Siglas = "PRD",
                    Descripcion = "Partido político (RD)",
                    IsActive = true,
                    LogoPath = null
                }
            );

            // =========================
            // SEED BASE (PUESTOS)
            // =========================
            modelBuilder.Entity<PuestoElectivo>().HasData(
                new PuestoElectivo { Id = 1, Nombre = "Presidencia", Descripcion = "Puesto electivo nacional", IsActive = true },
                new PuestoElectivo { Id = 2, Nombre = "Senaduría", Descripcion = "Representación provincial", IsActive = true },
                new PuestoElectivo { Id = 3, Nombre = "Diputación", Descripcion = "Cámara de Diputados", IsActive = true },
                new PuestoElectivo { Id = 4, Nombre = "Alcaldía", Descripcion = "Gobierno municipal", IsActive = true },

                new PuestoElectivo { Id = 5, Nombre = "Vicepresidencia", Descripcion = "Puesto nacional (acompaña candidatura presidencial)", IsActive = true },
                new PuestoElectivo { Id = 6, Nombre = "Vicealcaldía", Descripcion = "Puesto municipal (acompaña candidatura a alcaldía)", IsActive = true },
                new PuestoElectivo { Id = 7, Nombre = "Regiduría", Descripcion = "Concejo municipal (regidores/as)", IsActive = true },

                new PuestoElectivo { Id = 8, Nombre = "Dirección Distrito Municipal", Descripcion = "Director/a de Distrito Municipal", IsActive = true },
                new PuestoElectivo { Id = 9, Nombre = "Subdirección Distrito Municipal", Descripcion = "Subdirector/a de Distrito Municipal", IsActive = true },
                new PuestoElectivo { Id = 10, Nombre = "Vocalía Distrito Municipal", Descripcion = "Vocales del Distrito Municipal", IsActive = true },

                new PuestoElectivo { Id = 11, Nombre = "Diputación PARLACEN", Descripcion = "Representación ante el Parlamento Centroamericano (PARLACEN)", IsActive = true }
            );

            // =========================
            // SEED REALISTA (ELECCIÓN 2024)
            // =========================
            modelBuilder.Entity<Eleccion>().HasData(
                new Eleccion
                {
                    Id = 1,
                    Anio = 2024,
                    Tipo = "Presidenciales y Congresuales",
                    Fecha = new DateTime(2024, 5, 19),
                    IsActive = true
                }
            );

            // =========================
            // SEED REALISTA (CANDIDATURAS 2024 - BASE)
            // =========================
            modelBuilder.Entity<Candidatura>().HasData(
                // PRM
                new Candidatura
                {
                    Id = 1,
                    EleccionId = 1,
                    PuestoElectivoId = 1,
                    PartidoPoliticoId = 1,
                    NombreCompleto = "Luis Abinader",
                    NombreBoleta = "Luis Abinader",
                    FotoPath = null,
                    IsActive = true
                },
                new Candidatura
                {
                    Id = 2,
                    EleccionId = 1,
                    PuestoElectivoId = 5,
                    PartidoPoliticoId = 1,
                    NombreCompleto = "Raquel Peña",
                    NombreBoleta = "Raquel Peña",
                    FotoPath = null,
                    IsActive = true
                },

                // PLD
                new Candidatura
                {
                    Id = 3,
                    EleccionId = 1,
                    PuestoElectivoId = 1,
                    PartidoPoliticoId = 2,
                    NombreCompleto = "Abel Martínez",
                    NombreBoleta = "Abel Martínez",
                    FotoPath = null,
                    IsActive = true
                },
                new Candidatura
                {
                    Id = 4,
                    EleccionId = 1,
                    PuestoElectivoId = 5,
                    PartidoPoliticoId = 2,
                    NombreCompleto = "Zoraima Cuello",
                    NombreBoleta = "Zoraima Cuello",
                    FotoPath = null,
                    IsActive = true
                },

                // FP
                new Candidatura
                {
                    Id = 5,
                    EleccionId = 1,
                    PuestoElectivoId = 1,
                    PartidoPoliticoId = 3,
                    NombreCompleto = "Leonel Fernández",
                    NombreBoleta = "Leonel Fernández",
                    FotoPath = null,
                    IsActive = true
                },
                new Candidatura
                {
                    Id = 6,
                    EleccionId = 1,
                    PuestoElectivoId = 5,
                    PartidoPoliticoId = 3,
                    NombreCompleto = "Ingrid Mendoza",
                    NombreBoleta = "Ingrid Mendoza",
                    FotoPath = null,
                    IsActive = true
                }
            );
        }
    }
}