using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museum.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class AddSouvenirs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Souvenirs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Souvenirs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SouvenirsPhoto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    SouvenirId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SouvenirsPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SouvenirsPhoto_Souvenirs_SouvenirId",
                        column: x => x.SouvenirId,
                        principalTable: "Souvenirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SouvenirsPhoto_SouvenirId",
                table: "SouvenirsPhoto",
                column: "SouvenirId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SouvenirsPhoto");

            migrationBuilder.DropTable(
                name: "Souvenirs");
        }
    }
}
