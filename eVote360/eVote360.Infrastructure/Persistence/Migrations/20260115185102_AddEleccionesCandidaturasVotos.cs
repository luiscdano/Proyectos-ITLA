using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eVote360.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEleccionesCandidaturasVotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PartidosPoliticos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PartidosPoliticos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PartidosPoliticos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PartidosPoliticos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.CreateTable(
                name: "Elecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Anio = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elecciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidaturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EleccionId = table.Column<int>(type: "INTEGER", nullable: false),
                    PuestoElectivoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PartidoPoliticoId = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreCompleto = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    NombreBoleta = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    FotoPath = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidaturas_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidaturas_PartidosPoliticos_PartidoPoliticoId",
                        column: x => x.PartidoPoliticoId,
                        principalTable: "PartidosPoliticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidaturas_PuestosElectivos_PuestoElectivoId",
                        column: x => x.PuestoElectivoId,
                        principalTable: "PuestosElectivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EleccionId = table.Column<int>(type: "INTEGER", nullable: false),
                    PuestoElectivoId = table.Column<int>(type: "INTEGER", nullable: false),
                    CandidaturaId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TokenVoto = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votos_Candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "Candidaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votos_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votos_PuestosElectivos_PuestoElectivoId",
                        column: x => x.PuestoElectivoId,
                        principalTable: "PuestosElectivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_EleccionId_PuestoElectivoId_PartidoPoliticoId",
                table: "Candidaturas",
                columns: new[] { "EleccionId", "PuestoElectivoId", "PartidoPoliticoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_PartidoPoliticoId",
                table: "Candidaturas",
                column: "PartidoPoliticoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_PuestoElectivoId",
                table: "Candidaturas",
                column: "PuestoElectivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_CandidaturaId",
                table: "Votos",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_EleccionId_PuestoElectivoId",
                table: "Votos",
                columns: new[] { "EleccionId", "PuestoElectivoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Votos_PuestoElectivoId",
                table: "Votos",
                column: "PuestoElectivoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votos");

            migrationBuilder.DropTable(
                name: "Candidaturas");

            migrationBuilder.DropTable(
                name: "Elecciones");

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
                    { 4, "Gobierno municipal", true, "Alcaldía" },
                    { 5, "Puesto nacional (acompaña candidatura presidencial)", true, "Vicepresidencia" },
                    { 6, "Puesto municipal (acompaña candidatura a alcaldía)", true, "Vicealcaldía" },
                    { 7, "Concejo municipal (regidores/as)", true, "Regiduría" },
                    { 8, "Director/a de Distrito Municipal", true, "Dirección Distrito Municipal" },
                    { 9, "Subdirector/a de Distrito Municipal", true, "Subdirección Distrito Municipal" },
                    { 10, "Vocales del Distrito Municipal", true, "Vocalía Distrito Municipal" },
                    { 11, "Representación ante el Parlamento Centroamericano (PARLACEN)", true, "Diputación PARLACEN" }
                });
        }
    }
}
