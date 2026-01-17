using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eVote360.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReseedBaseAnd2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_Elecciones_EleccionId",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_PartidosPoliticos_PartidoPoliticoId",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_PuestosElectivos_PuestoElectivoId",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Candidaturas_CandidaturaId",
                table: "Votos");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Elecciones_EleccionId",
                table: "Votos");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_PuestosElectivos_PuestoElectivoId",
                table: "Votos");

            migrationBuilder.InsertData(
                table: "Elecciones",
                columns: new[] { "Id", "Anio", "Fecha", "IsActive", "Tipo" },
                values: new object[] { 1, 2024, new DateTime(2024, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Presidenciales y Congresuales" });

            migrationBuilder.InsertData(
                table: "PartidosPoliticos",
                columns: new[] { "Id", "Descripcion", "IsActive", "LogoPath", "Nombre", "Siglas" },
                values: new object[,]
                {
                    { 1, "Partido político (RD)", true, null, "Partido Revolucionario Moderno", "PRM" },
                    { 2, "Partido político (RD)", true, null, "Partido de la Liberación Dominicana", "PLD" },
                    { 3, "Partido político (RD)", true, null, "Fuerza del Pueblo", "FP" },
                    { 4, "Partido político (RD)", true, null, "Partido Revolucionario Dominicano", "PRD" }
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

            migrationBuilder.InsertData(
                table: "Candidaturas",
                columns: new[] { "Id", "EleccionId", "FotoPath", "IsActive", "NombreBoleta", "NombreCompleto", "PartidoPoliticoId", "PuestoElectivoId" },
                values: new object[,]
                {
                    { 1, 1, null, true, "Luis Abinader", "Luis Abinader", 1, 1 },
                    { 2, 1, null, true, "Raquel Peña", "Raquel Peña", 1, 5 },
                    { 3, 1, null, true, "Abel Martínez", "Abel Martínez", 2, 1 },
                    { 4, 1, null, true, "Zoraima Cuello", "Zoraima Cuello", 2, 5 },
                    { 5, 1, null, true, "Leonel Fernández", "Leonel Fernández", 3, 1 },
                    { 6, 1, null, true, "Ingrid Mendoza", "Ingrid Mendoza", 3, 5 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_Elecciones_EleccionId",
                table: "Candidaturas",
                column: "EleccionId",
                principalTable: "Elecciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_PartidosPoliticos_PartidoPoliticoId",
                table: "Candidaturas",
                column: "PartidoPoliticoId",
                principalTable: "PartidosPoliticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_PuestosElectivos_PuestoElectivoId",
                table: "Candidaturas",
                column: "PuestoElectivoId",
                principalTable: "PuestosElectivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Candidaturas_CandidaturaId",
                table: "Votos",
                column: "CandidaturaId",
                principalTable: "Candidaturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Elecciones_EleccionId",
                table: "Votos",
                column: "EleccionId",
                principalTable: "Elecciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_PuestosElectivos_PuestoElectivoId",
                table: "Votos",
                column: "PuestoElectivoId",
                principalTable: "PuestosElectivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_Elecciones_EleccionId",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_PartidosPoliticos_PartidoPoliticoId",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_PuestosElectivos_PuestoElectivoId",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Candidaturas_CandidaturaId",
                table: "Votos");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Elecciones_EleccionId",
                table: "Votos");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_PuestosElectivos_PuestoElectivoId",
                table: "Votos");

            migrationBuilder.DeleteData(
                table: "Candidaturas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Candidaturas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Candidaturas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Candidaturas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Candidaturas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Candidaturas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PartidosPoliticos",
                keyColumn: "Id",
                keyValue: 4);

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

            migrationBuilder.DeleteData(
                table: "Elecciones",
                keyColumn: "Id",
                keyValue: 1);

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
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PuestosElectivos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_Elecciones_EleccionId",
                table: "Candidaturas",
                column: "EleccionId",
                principalTable: "Elecciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_PartidosPoliticos_PartidoPoliticoId",
                table: "Candidaturas",
                column: "PartidoPoliticoId",
                principalTable: "PartidosPoliticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_PuestosElectivos_PuestoElectivoId",
                table: "Candidaturas",
                column: "PuestoElectivoId",
                principalTable: "PuestosElectivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Candidaturas_CandidaturaId",
                table: "Votos",
                column: "CandidaturaId",
                principalTable: "Candidaturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Elecciones_EleccionId",
                table: "Votos",
                column: "EleccionId",
                principalTable: "Elecciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_PuestosElectivos_PuestoElectivoId",
                table: "Votos",
                column: "PuestoElectivoId",
                principalTable: "PuestosElectivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
