using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Berber_Otomasyon.Migrations
{
    /// <inheritdoc />
    public partial class FixCalisanRandevuTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "RandevuTarih",
                table: "CalisanRandevular",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandevuTarih",
                table: "CalisanRandevular");
        }
    }
}
