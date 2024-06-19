using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class MoveCoordinatesFromFixationDefectToTaskEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoordinatesX",
                table: "FixationDefects");

            migrationBuilder.DropColumn(
                name: "CoordinatesY",
                table: "FixationDefects");

            migrationBuilder.DropColumn(
                name: "ExactAddress",
                table: "FixationDefects");

            migrationBuilder.RenameColumn(
                name: "ApproximateAddress",
                table: "Tasks",
                newName: "Address");

            migrationBuilder.AddColumn<double>(
                name: "CoordinateX",
                table: "Tasks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CoordinateY",
                table: "Tasks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CacheAddress",
                table: "FixationDefects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoordinateX",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CoordinateY",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CacheAddress",
                table: "FixationDefects");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Tasks",
                newName: "ApproximateAddress");

            migrationBuilder.AddColumn<double>(
                name: "CoordinatesX",
                table: "FixationDefects",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CoordinatesY",
                table: "FixationDefects",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExactAddress",
                table: "FixationDefects",
                type: "text",
                nullable: true);
        }
    }
}
