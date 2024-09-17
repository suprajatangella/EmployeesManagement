using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeesManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApprovalEntriesTblChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "approvalEntries");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById1",
                table: "approvalEntries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_approvalEntries_LastModifiedById1",
                table: "approvalEntries",
                column: "LastModifiedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_approvalEntries_AspNetUsers_LastModifiedById1",
                table: "approvalEntries",
                column: "LastModifiedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_approvalEntries_AspNetUsers_LastModifiedById1",
                table: "approvalEntries");

            migrationBuilder.DropIndex(
                name: "IX_approvalEntries_LastModifiedById1",
                table: "approvalEntries");

            migrationBuilder.DropColumn(
                name: "LastModifiedById1",
                table: "approvalEntries");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedBy",
                table: "approvalEntries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
