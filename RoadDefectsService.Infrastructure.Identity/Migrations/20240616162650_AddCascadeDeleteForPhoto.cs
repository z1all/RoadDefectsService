using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteForPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationDefects_FixationDefectId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationWorks_FixationWorkId",
                table: "Photos");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationDefects_FixationDefectId",
                table: "Photos",
                column: "FixationDefectId",
                principalTable: "FixationDefects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationWorks_FixationWorkId",
                table: "Photos",
                column: "FixationWorkId",
                principalTable: "FixationWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationDefects_FixationDefectId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationWorks_FixationWorkId",
                table: "Photos");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationDefects_FixationDefectId",
                table: "Photos",
                column: "FixationDefectId",
                principalTable: "FixationDefects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationWorks_FixationWorkId",
                table: "Photos",
                column: "FixationWorkId",
                principalTable: "FixationWorks",
                principalColumn: "Id");
        }
    }
}
