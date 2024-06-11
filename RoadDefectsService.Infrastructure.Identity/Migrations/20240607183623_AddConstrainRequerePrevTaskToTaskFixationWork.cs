using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddConstrainRequerePrevTaskToTaskFixationWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tasks_PrevTaskId",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PrevTaskId",
                table: "Tasks",
                column: "PrevTaskId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tasks_PrevTaskId",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PrevTaskId",
                table: "Tasks",
                column: "PrevTaskId");
        }
    }
}
