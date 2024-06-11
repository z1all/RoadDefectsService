using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveConstrainRequeredRoadInspectorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_RoadInspectors_RoadInspectorId",
                table: "Tasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoadInspectorId",
                table: "Tasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_RoadInspectors_RoadInspectorId",
                table: "Tasks",
                column: "RoadInspectorId",
                principalTable: "RoadInspectors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_RoadInspectors_RoadInspectorId",
                table: "Tasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoadInspectorId",
                table: "Tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_RoadInspectors_RoadInspectorId",
                table: "Tasks",
                column: "RoadInspectorId",
                principalTable: "RoadInspectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
