using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddFixationDefectToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FixationDefectId",
                table: "Tasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FixationWorkId",
                table: "Tasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FixationDefect",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixationDefect", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FixationWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixationWork", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FixationDefectId",
                table: "Tasks",
                column: "FixationDefectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FixationWorkId",
                table: "Tasks",
                column: "FixationWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_FixationDefect_FixationDefectId",
                table: "Tasks",
                column: "FixationDefectId",
                principalTable: "FixationDefect",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_FixationWork_FixationWorkId",
                table: "Tasks",
                column: "FixationWorkId",
                principalTable: "FixationWork",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_FixationDefect_FixationDefectId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_FixationWork_FixationWorkId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "FixationDefect");

            migrationBuilder.DropTable(
                name: "FixationWork");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_FixationDefectId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_FixationWorkId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "FixationDefectId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "FixationWorkId",
                table: "Tasks");
        }
    }
}
