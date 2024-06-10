using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBaseModelForFixation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fixations");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationDefects_FixationId",
                table: "Photos",
                column: "FixationId",
                principalTable: "FixationDefects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationWorks_FixationId",
                table: "Photos",
                column: "FixationId",
                principalTable: "FixationWorks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationDefects_FixationId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationWorks_FixationId",
                table: "Photos");

            migrationBuilder.CreateTable(
                name: "Fixations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixations", x => x.Id);
                });
        }
    }
}
