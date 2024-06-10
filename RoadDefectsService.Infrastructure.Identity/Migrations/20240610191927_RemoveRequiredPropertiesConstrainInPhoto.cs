using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredPropertiesConstrainInPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_OwnerId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationDefects_FixationDefectId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationWorks_FixationWorkId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_OwnerId",
                table: "Photos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ModelC_SingleReference",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "OwnerId",
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

            migrationBuilder.AlterColumn<bool>(
                name: "WorkDone",
                table: "FixationWorks",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "ExactAddress",
                table: "FixationDefects",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<double>(
                name: "DamagedCanvasSquareMeter",
                table: "FixationDefects",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "CoordinatesY",
                table: "FixationDefects",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "CoordinatesX",
                table: "FixationDefects",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ModelC_SingleReference",
                table: "Photos",
                sql: "(\"FixationWorkId\" IS NULL     AND \"FixationDefectId\" IS NOT NULL OR \"FixationWorkId\" IS NOT NULL AND \"FixationDefectId\" IS NULL)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationDefects_FixationDefectId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationWorks_FixationWorkId",
                table: "Photos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ModelC_SingleReference",
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

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Photos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<bool>(
                name: "WorkDone",
                table: "FixationWorks",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExactAddress",
                table: "FixationDefects",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DamagedCanvasSquareMeter",
                table: "FixationDefects",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CoordinatesY",
                table: "FixationDefects",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CoordinatesX",
                table: "FixationDefects",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_OwnerId",
                table: "Photos",
                column: "OwnerId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ModelC_SingleReference",
                table: "Photos",
                sql: "(\"FixationWorkId\" IS NULL OR \"FixationDefectId\" IS NULL)");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_OwnerId",
                table: "Photos",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
