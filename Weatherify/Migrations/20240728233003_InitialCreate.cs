using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weatherify.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
          
          migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Latitude = table.Column<double>(type: "REAL", precision: 17, scale: 10, nullable: true),
                    Longitude = table.Column<double>(type: "REAL", precision: 17, scale: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });
            

          migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", precision: 17, scale: 10, nullable: true),
                    Longitude = table.Column<double>(type: "REAL", precision: 17, scale: 10, nullable: true),
                    GenerationTimeMs = table.Column<float>(type: "REAL", precision: 28, scale: 7, nullable: true),
                    UtcOffsetSeconds = table.Column<double>(type: "REAL", precision: 28, scale: 7, nullable: true),
                    TimezoneAbbr = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Elevation = table.Column<double>(type: "REAL", precision: 18, scale: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
