using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class MoveAddressFromTaskFixationDefectToTaskEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApproximateAddress",
                table: "Tasks",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApproximateAddress",
                table: "Tasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
