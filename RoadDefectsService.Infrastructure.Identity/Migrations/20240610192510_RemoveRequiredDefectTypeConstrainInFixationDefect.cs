using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredDefectTypeConstrainInFixationDefect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixationDefects_DefectTypes_DefectTypeId",
                table: "FixationDefects");

            migrationBuilder.AlterColumn<Guid>(
                name: "DefectTypeId",
                table: "FixationDefects",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_FixationDefects_DefectTypes_DefectTypeId",
                table: "FixationDefects",
                column: "DefectTypeId",
                principalTable: "DefectTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixationDefects_DefectTypes_DefectTypeId",
                table: "FixationDefects");

            migrationBuilder.AlterColumn<Guid>(
                name: "DefectTypeId",
                table: "FixationDefects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FixationDefects_DefectTypes_DefectTypeId",
                table: "FixationDefects",
                column: "DefectTypeId",
                principalTable: "DefectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
