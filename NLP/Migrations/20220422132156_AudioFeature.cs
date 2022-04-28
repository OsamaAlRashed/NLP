using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLP.Migrations
{
    public partial class AudioFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AudioFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForType = table.Column<int>(type: "int", nullable: false),
                    RMS = table.Column<double>(type: "float", nullable: false),
                    ZCR = table.Column<int>(type: "int", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false),
                    SpectralCentroid = table.Column<int>(type: "int", nullable: false),
                    SpectralFlatness = table.Column<double>(type: "float", nullable: false),
                    SpectralFlux = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioFeatures", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioFeatures");
        }
    }
}
