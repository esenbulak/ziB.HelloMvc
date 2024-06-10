using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sube2.HelloMvc.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    Dersid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dersad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kredi = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.Dersid);
                });

            migrationBuilder.CreateTable(
                name: "DersOgrenci",
                columns: table => new
                {
                    DerslerDersid = table.Column<int>(type: "int", nullable: false),
                    OgrencilerOgrenciid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersOgrenci", x => new { x.DerslerDersid, x.OgrencilerOgrenciid });
                    table.ForeignKey(
                        name: "FK_DersOgrenci_Dersler_DerslerDersid",
                        column: x => x.DerslerDersid,
                        principalTable: "Dersler",
                        principalColumn: "Dersid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersOgrenci_tblOgrenciler_OgrencilerOgrenciid",
                        column: x => x.OgrencilerOgrenciid,
                        principalTable: "tblOgrenciler",
                        principalColumn: "Ogrenciid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DersOgrenci_OgrencilerOgrenciid",
                table: "DersOgrenci",
                column: "OgrencilerOgrenciid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DersOgrenci");

            migrationBuilder.DropTable(
                name: "Dersler");
        }
    }
}
