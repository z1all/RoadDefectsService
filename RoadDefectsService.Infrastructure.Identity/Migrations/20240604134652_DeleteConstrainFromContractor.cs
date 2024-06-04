using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConstrainFromContractor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Contractors_Email",
                table: "Contractors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Contractors_Email",
                table: "Contractors",
                column: "Email");
        }
    }
}
