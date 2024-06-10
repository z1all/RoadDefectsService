using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesToFixationModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RecordedDateTime",
                table: "FixationWorks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "WorkDone",
                table: "FixationWorks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CoordinatesX",
                table: "FixationDefects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoordinatesY",
                table: "FixationDefects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamagedCanvasSquareMeter",
                table: "FixationDefects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "DefectTypeId",
                table: "FixationDefects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ExactAddress",
                table: "FixationDefects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RecordedDateTime",
                table: "FixationDefects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "DefectTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FixationDefects_DefectTypeId",
                table: "FixationDefects",
                column: "DefectTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixationDefects_DefectTypes_DefectTypeId",
                table: "FixationDefects",
                column: "DefectTypeId",
                principalTable: "DefectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixationDefects_DefectTypes_DefectTypeId",
                table: "FixationDefects");

            migrationBuilder.DropTable(
                name: "DefectTypes");

            migrationBuilder.DropIndex(
                name: "IX_FixationDefects_DefectTypeId",
                table: "FixationDefects");

            migrationBuilder.DropColumn(
                name: "RecordedDateTime",
                table: "FixationWorks");

            migrationBuilder.DropColumn(
                name: "WorkDone",
                table: "FixationWorks");

            migrationBuilder.DropColumn(
                name: "CoordinatesX",
                table: "FixationDefects");

            migrationBuilder.DropColumn(
                name: "CoordinatesY",
                table: "FixationDefects");

            migrationBuilder.DropColumn(
                name: "DamagedCanvasSquareMeter",
                table: "FixationDefects");

            migrationBuilder.DropColumn(
                name: "DefectTypeId",
                table: "FixationDefects");

            migrationBuilder.DropColumn(
                name: "ExactAddress",
                table: "FixationDefects");

            migrationBuilder.DropColumn(
                name: "RecordedDateTime",
                table: "FixationDefects");
        }
    }
}
