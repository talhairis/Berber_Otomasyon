using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Berber_Otomasyon.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IslemTurleri",
                columns: table => new
                {
                    IslemTuruId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sure = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IslemTurleri", x => x.IslemTuruId);
                });

            migrationBuilder.CreateTable(
                name: "CalisanIslemler",
                columns: table => new
                {
                    CalisanIslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IslemTuruId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanIslemler", x => x.CalisanIslemId);
                    table.ForeignKey(
                        name: "FK_CalisanIslemler_AspNetUsers_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CalisanIslemler_IslemTurleri_IslemTuruId",
                        column: x => x.IslemTuruId,
                        principalTable: "IslemTurleri",
                        principalColumn: "IslemTuruId");
                });

            migrationBuilder.CreateTable(
                name: "IslemSepetleri",
                columns: table => new
                {
                    IslemSepetiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IslemTuruId = table.Column<int>(type: "int", nullable: false),
                    MusteriRandevuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IslemSepetleri", x => x.IslemSepetiId);
                    table.ForeignKey(
                        name: "FK_IslemSepetleri_IslemTurleri_IslemTuruId",
                        column: x => x.IslemTuruId,
                        principalTable: "IslemTurleri",
                        principalColumn: "IslemTuruId");
                    table.ForeignKey(
                        name: "FK_IslemSepetleri_MusteriRandevular_MusteriRandevuId",
                        column: x => x.MusteriRandevuId,
                        principalTable: "MusteriRandevular",
                        principalColumn: "MusteriRandevuId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalisanIslemler_CalisanId",
                table: "CalisanIslemler",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanIslemler_IslemTuruId",
                table: "CalisanIslemler",
                column: "IslemTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_IslemSepetleri_IslemTuruId",
                table: "IslemSepetleri",
                column: "IslemTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_IslemSepetleri_MusteriRandevuId",
                table: "IslemSepetleri",
                column: "MusteriRandevuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalisanIslemler");

            migrationBuilder.DropTable(
                name: "IslemSepetleri");

            migrationBuilder.DropTable(
                name: "IslemTurleri");
        }
    }
}
