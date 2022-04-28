using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLP.Migrations
{
    public partial class AddPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Energy",
                table: "AudioFeatures",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "AudioFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "AudioFeatures");

            migrationBuilder.AlterColumn<int>(
                name: "Energy",
                table: "AudioFeatures",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
