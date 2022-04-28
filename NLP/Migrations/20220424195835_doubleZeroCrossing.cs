using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLP.Migrations
{
    public partial class doubleZeroCrossing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ZCR",
                table: "AudioFeatures",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ZCR",
                table: "AudioFeatures",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
