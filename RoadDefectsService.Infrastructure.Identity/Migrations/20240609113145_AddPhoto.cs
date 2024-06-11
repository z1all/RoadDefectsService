using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    FixationWorkId = table.Column<Guid>(type: "uuid", nullable: true),
                    FixationDefectId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_FixationDefect_FixationDefectId",
                        column: x => x.FixationDefectId,
                        principalTable: "FixationDefect",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Photos_FixationWork_FixationWorkId",
                        column: x => x.FixationWorkId,
                        principalTable: "FixationWork",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_FixationDefectId",
                table: "Photos",
                column: "FixationDefectId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_FixationWorkId",
                table: "Photos",
                column: "FixationWorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");
        }
    }
}
