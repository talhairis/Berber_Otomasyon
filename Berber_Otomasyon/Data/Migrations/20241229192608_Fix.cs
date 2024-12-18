using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Berber_Otomasyon.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToplamSure",
                table: "IslemSepetleri",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ToplamUcret",
                table: "IslemSepetleri",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToplamSure",
                table: "IslemSepetleri");

            migrationBuilder.DropColumn(
                name: "ToplamUcret",
                table: "IslemSepetleri");
        }
    }
}
