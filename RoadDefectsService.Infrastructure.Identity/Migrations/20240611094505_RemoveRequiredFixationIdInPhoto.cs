using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredFixationIdInPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationDefects_FixationDefectId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationWorks_FixationWorkId",
                table: "Photos");

            migrationBuilder.AlterColumn<Guid>(
                name: "FixationWorkId",
                table: "Photos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "FixationDefectId",
                table: "Photos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationDefects_FixationDefectId",
                table: "Photos",
                column: "FixationDefectId",
                principalTable: "FixationDefects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationWorks_FixationWorkId",
                table: "Photos",
                column: "FixationWorkId",
                principalTable: "FixationWorks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationDefects_FixationDefectId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationWorks_FixationWorkId",
                table: "Photos");

            migrationBuilder.AlterColumn<Guid>(
                name: "FixationWorkId",
                table: "Photos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FixationDefectId",
                table: "Photos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationDefects_FixationDefectId",
                table: "Photos",
                column: "FixationDefectId",
                principalTable: "FixationDefects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationWorks_FixationWorkId",
                table: "Photos",
                column: "FixationWorkId",
                principalTable: "FixationWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
