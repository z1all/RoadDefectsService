using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class ChangConstrainOnOnewToManyInAssignmentForContractor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Assignments_ContractorId",
                table: "Assignments");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ContractorId",
                table: "Assignments",
                column: "ContractorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Assignments_ContractorId",
                table: "Assignments");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ContractorId",
                table: "Assignments",
                column: "ContractorId",
                unique: true);
        }
    }
}
