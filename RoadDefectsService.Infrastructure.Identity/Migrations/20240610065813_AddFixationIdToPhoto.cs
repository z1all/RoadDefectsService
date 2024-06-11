using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddFixationIdToPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationDefects_FixationId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationWorks_FixationId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_FixationId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "FixationId",
                table: "Photos",
                newName: "FixationWorkId");

            migrationBuilder.AddColumn<Guid>(
                name: "FixationDefectId",
                table: "Photos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_FixationDefectId",
                table: "Photos",
                column: "FixationDefectId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_FixationWorkId_FixationDefectId",
                table: "Photos",
                columns: new[] { "FixationWorkId", "FixationDefectId" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_ModelC_SingleReference",
                table: "Photos",
                sql: "(\"FixationWorkId\" IS NULL OR \"FixationDefectId\" IS NULL)");

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

            migrationBuilder.DropIndex(
                name: "IX_Photos_FixationDefectId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_FixationWorkId_FixationDefectId",
                table: "Photos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ModelC_SingleReference",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "FixationDefectId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "FixationWorkId",
                table: "Photos",
                newName: "FixationId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_FixationId",
                table: "Photos",
                column: "FixationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationDefects_FixationId",
                table: "Photos",
                column: "FixationId",
                principalTable: "FixationDefects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationWorks_FixationId",
                table: "Photos",
                column: "FixationId",
                principalTable: "FixationWorks",
                principalColumn: "Id");
        }
    }
}
