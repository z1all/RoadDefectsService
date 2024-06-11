using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddPrevTaskToTaskFixationWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrevTaskId",
                table: "Tasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PrevTaskId",
                table: "Tasks",
                column: "PrevTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tasks_PrevTaskId",
                table: "Tasks",
                column: "PrevTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_PrevTaskId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_PrevTaskId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "PrevTaskId",
                table: "Tasks");
        }
    }
}
