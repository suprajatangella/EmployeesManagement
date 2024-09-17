using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeesManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColumnDataTypeChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "approvalEntries",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_approvalEntries_LastModifiedById",
                table: "approvalEntries",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_approvalEntries_AspNetUsers_LastModifiedById",
                table: "approvalEntries",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_approvalEntries_AspNetUsers_LastModifiedById",
                table: "approvalEntries");

            migrationBuilder.DropIndex(
                name: "IX_approvalEntries_LastModifiedById",
                table: "approvalEntries");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedById",
                table: "approvalEntries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
