using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureVest.Data.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Iso3 = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    Region = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    IsEligible = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MacroIndicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    HigherIsBetter = table.Column<bool>(type: "INTEGER", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroIndicators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryIndicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    MacroIndicatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: true),
                    Missing = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryIndicators_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryIndicators_MacroIndicators_MacroIndicatorId",
                        column: x => x.MacroIndicatorId,
                        principalTable: "MacroIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Iso3",
                table: "Countries",
                column: "Iso3",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryIndicators_CountryId_MacroIndicatorId_Year",
                table: "CountryIndicators",
                columns: new[] { "CountryId", "MacroIndicatorId", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryIndicators_MacroIndicatorId",
                table: "CountryIndicators",
                column: "MacroIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MacroIndicators_Code",
                table: "MacroIndicators",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryIndicators");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "MacroIndicators");
        }
    }
}
