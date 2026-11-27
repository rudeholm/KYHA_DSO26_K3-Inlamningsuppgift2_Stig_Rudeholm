using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FailSafeWeatherMonitor.Data.Migrations;

/// <inheritdoc />
public partial class _20261127183750_AddIndexIsUnique : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_WeatherDataPoints_StationName_Timestamp",
            table: "WeatherDataPoints");

        migrationBuilder.CreateIndex(
            name: "IX_WeatherDataPoints_StationName_Timestamp",
            table: "WeatherDataPoints",
            columns: new[] { "StationName", "Timestamp" },
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_WeatherDataPoints_StationName_Timestamp",
            table: "WeatherDataPoints");

        migrationBuilder.CreateIndex(
            name: "IX_WeatherDataPoints_StationName_Timestamp",
            table: "WeatherDataPoints",
            columns: new[] { "StationName", "Timestamp" });
    }
}
