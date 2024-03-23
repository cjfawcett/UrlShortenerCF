using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twtr.UrlShortener.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TLDs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TLD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeedNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLDs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TLDId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortenedUrl = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Urls_TLDs_TLDId",
                        column: x => x.TLDId,
                        principalTable: "TLDs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TLDId_ShortenedUrl",
                table: "Urls",
                columns: new[] { "TLDId", "ShortenedUrl" },
                unique: true,
                filter: "[TLDId] IS NOT NULL AND [ShortenedUrl] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urls");

            migrationBuilder.DropTable(
                name: "TLDs");
        }
    }
}
