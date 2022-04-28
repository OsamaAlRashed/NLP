using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLP.Migrations
{
    public partial class timeDomainFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpectralCentroid",
                table: "AudioFeatures");

            migrationBuilder.DropColumn(
                name: "SpectralFlatness",
                table: "AudioFeatures");

            migrationBuilder.DropColumn(
                name: "SpectralFlux",
                table: "AudioFeatures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpectralCentroid",
                table: "AudioFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SpectralFlatness",
                table: "AudioFeatures",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SpectralFlux",
                table: "AudioFeatures",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
