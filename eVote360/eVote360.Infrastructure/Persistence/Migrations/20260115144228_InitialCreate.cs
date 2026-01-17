using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eVote360.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartidosPoliticos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Siglas = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    LogoPath = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartidosPoliticos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PuestosElectivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuestosElectivos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PartidosPoliticos",
                columns: new[] { "Id", "Descripcion", "IsActive", "LogoPath", "Nombre", "Siglas" },
                values: new object[,]
                {
                    { 1, "Partido de gobierno y mayoritario", true, null, "Partido Revolucionario Moderno", "PRM" },
                    { 2, "Fuerza histórica de oposición", true, null, "Partido de la Liberación Dominicana", "PLD" },
                    { 3, "Partido emergente con crecimiento significativo", true, null, "Fuerza del Pueblo", "FP" },
                    { 4, "Estructura tradicional con reconocimiento", true, null, "Partido Revolucionario Dominicano", "PRD" }
                });

            migrationBuilder.InsertData(
                table: "PuestosElectivos",
                columns: new[] { "Id", "Descripcion", "IsActive", "Nombre" },
                values: new object[,]
                {
                    { 1, "Puesto electivo nacional", true, "Presidencia" },
                    { 2, "Representación provincial", true, "Senaduría" },
                    { 3, "Cámara de Diputados", true, "Diputación" },
                    { 4, "Gobierno municipal", true, "Alcaldía" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartidosPoliticos");

            migrationBuilder.DropTable(
                name: "PuestosElectivos");
        }
    }
}
