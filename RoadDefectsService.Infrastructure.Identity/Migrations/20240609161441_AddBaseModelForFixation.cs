using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadDefectsService.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseModelForFixation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationDefect_FixationDefectId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_FixationWork_FixationWorkId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_FixationDefect_FixationDefectId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_FixationWork_FixationWorkId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Photos_FixationDefectId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FixationWork",
                table: "FixationWork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FixationDefect",
                table: "FixationDefect");

            migrationBuilder.DropColumn(
                name: "FixationDefectId",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "FixationWork",
                newName: "FixationWorks");

            migrationBuilder.RenameTable(
                name: "FixationDefect",
                newName: "FixationDefects");

            migrationBuilder.RenameColumn(
                name: "FixationWorkId",
                table: "Photos",
                newName: "FixationId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_FixationWorkId",
                table: "Photos",
                newName: "IX_Photos_FixationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FixationWorks",
                table: "FixationWorks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FixationDefects",
                table: "FixationDefects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Fixations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixations", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_FixationDefects_FixationDefectId",
                table: "Tasks",
                column: "FixationDefectId",
                principalTable: "FixationDefects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_FixationWorks_FixationWorkId",
                table: "Tasks",
                column: "FixationWorkId",
                principalTable: "FixationWorks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_FixationDefects_FixationDefectId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_FixationWorks_FixationWorkId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Fixations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FixationWorks",
                table: "FixationWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FixationDefects",
                table: "FixationDefects");

            migrationBuilder.RenameTable(
                name: "FixationWorks",
                newName: "FixationWork");

            migrationBuilder.RenameTable(
                name: "FixationDefects",
                newName: "FixationDefect");

            migrationBuilder.RenameColumn(
                name: "FixationId",
                table: "Photos",
                newName: "FixationWorkId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_FixationId",
                table: "Photos",
                newName: "IX_Photos_FixationWorkId");

            migrationBuilder.AddColumn<Guid>(
                name: "FixationDefectId",
                table: "Photos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FixationWork",
                table: "FixationWork",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FixationDefect",
                table: "FixationDefect",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_FixationDefectId",
                table: "Photos",
                column: "FixationDefectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationDefect_FixationDefectId",
                table: "Photos",
                column: "FixationDefectId",
                principalTable: "FixationDefect",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_FixationWork_FixationWorkId",
                table: "Photos",
                column: "FixationWorkId",
                principalTable: "FixationWork",
                principalColumn: "Id");

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
    }
}
