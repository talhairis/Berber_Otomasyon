using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Berber_Otomasyon.Migrations
{
    /// <inheritdoc />
    public partial class FixRegisterationService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalisanIslemler_AspNetUsers_MusteriId",
                table: "CalisanIslemler");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanRandevular_AspNetUsers_MusteriId",
                table: "CalisanRandevular");

            migrationBuilder.DropForeignKey(
                name: "FK_MusteriRandevular_AspNetUsers_CalisanId",
                table: "MusteriRandevular");

            migrationBuilder.DropIndex(
                name: "IX_MusteriRandevular_CalisanId",
                table: "MusteriRandevular");

            migrationBuilder.DropIndex(
                name: "IX_CalisanRandevular_MusteriId",
                table: "CalisanRandevular");

            migrationBuilder.DropIndex(
                name: "IX_CalisanIslemler_MusteriId",
                table: "CalisanIslemler");

            migrationBuilder.DropColumn(
                name: "CalisanId",
                table: "MusteriRandevular");

            migrationBuilder.DropColumn(
                name: "MusteriId",
                table: "CalisanRandevular");

            migrationBuilder.DropColumn(
                name: "MusteriId",
                table: "CalisanIslemler");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CalisanId",
                table: "MusteriRandevular",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MusteriId",
                table: "CalisanRandevular",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MusteriId",
                table: "CalisanIslemler",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriRandevular_CalisanId",
                table: "MusteriRandevular",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanRandevular_MusteriId",
                table: "CalisanRandevular",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanIslemler_MusteriId",
                table: "CalisanIslemler",
                column: "MusteriId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanIslemler_AspNetUsers_MusteriId",
                table: "CalisanIslemler",
                column: "MusteriId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanRandevular_AspNetUsers_MusteriId",
                table: "CalisanRandevular",
                column: "MusteriId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriRandevular_AspNetUsers_CalisanId",
                table: "MusteriRandevular",
                column: "CalisanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
