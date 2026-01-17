using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eVote360.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedMorePuestosElectivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PuestosElectivos",
                columns: new[] { "Id", "Descripcion", "IsActive", "Nombre" },
                values: new object[,]
                {
                    { 5, "Puesto nacional (acompaña candidatura presidencial)", true, "Vicepresidencia" },
                    { 6, "Puesto municipal (acompaña candidatura a alcaldía)", true, "Vicealcaldía" },
                    { 7, "Concejo municipal (regidores/as)", true, "Regiduría" },
                    { 8, "Director/a de Distrito Municipal", true, "Dirección Distrito Municipal" },
                    { 9, "Subdirector/a de Distrito Municipal", true, "Subdirección Distrito Municipal" },
                    { 10, "Vocales del Distrito Municipal", true, "Vocalía Distrito Municipal" },
                    { 11, "Representación ante el Parlamento Centroamericano (PARLACEN)", true, "Diputación PARLACEN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
