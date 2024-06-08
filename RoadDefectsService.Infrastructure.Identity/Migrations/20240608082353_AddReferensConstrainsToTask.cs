using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddReferensConstrainsToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tasks_FixationDefectId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_FixationWorkId",
                table: "Tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskFixationWorkId",
                table: "FixationWork",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "FixationDefect",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FixationDefectId",
                table: "Tasks",
                column: "FixationDefectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FixationWorkId",
                table: "Tasks",
                column: "FixationWorkId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tasks_FixationDefectId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_FixationWorkId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskFixationWorkId",
                table: "FixationWork");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "FixationDefect");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FixationDefectId",
                table: "Tasks",
                column: "FixationDefectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FixationWorkId",
                table: "Tasks",
                column: "FixationWorkId");
        }
    }
}
