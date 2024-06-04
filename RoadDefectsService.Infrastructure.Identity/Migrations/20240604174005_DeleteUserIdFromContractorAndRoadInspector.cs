using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUserIdFromContractorAndRoadInspector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operators_AspNetUsers_UserId",
                table: "Operators");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadInspectors_AspNetUsers_UserId",
                table: "RoadInspectors");

            migrationBuilder.DropIndex(
                name: "IX_RoadInspectors_UserId",
                table: "RoadInspectors");

            migrationBuilder.DropIndex(
                name: "IX_Operators_UserId",
                table: "Operators");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RoadInspectors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Operators");

            migrationBuilder.AddForeignKey(
                name: "FK_Operators_AspNetUsers_Id",
                table: "Operators",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadInspectors_AspNetUsers_Id",
                table: "RoadInspectors",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operators_AspNetUsers_Id",
                table: "Operators");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadInspectors_AspNetUsers_Id",
                table: "RoadInspectors");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "RoadInspectors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Operators",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RoadInspectors_UserId",
                table: "RoadInspectors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operators_UserId",
                table: "Operators",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operators_AspNetUsers_UserId",
                table: "Operators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadInspectors_AspNetUsers_UserId",
                table: "RoadInspectors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
